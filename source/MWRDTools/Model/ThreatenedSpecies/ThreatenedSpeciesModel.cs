using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Text;

using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace MWRDTools.Model
{
  class ThreatenedSpeciesModel : AbstractMWRDModel, IThreatenedSpeciesModel {

    private const string LATITUDE_COL_NAME = "Easting";
    private const string LONGITUDE_COL_NAME = "Northing";

    private static string[] UNIQUE_SPECIES_COLUMNS = { 
       "SpeciesCode", "KingdomName", "ClassName", "FamilyName", 
       "ScientificName", "Exotic", "CommonName", "NSWStatus"
    };

    public delegate void ProgressEventHandler(object sender, ProgressEventArgs e);

    public event ProgressEventHandler ProgressEvent;

    public void OverwriteSightingData(DataTable speciesSightings) {

      DataTable uniqueSpeciesTable = DeriveUniqueThreatenedSpecies(speciesSightings);

      bridge.BeginTransaction();

      raiseStatusEvent(" Deleting content of table " + Constants.TableName.ThreatenedSpecies + "..."); 

      bridge.DeleteTableContent(
        Constants.TableName.ThreatenedSpecies
      );

      raiseStatusEvent(" Deleting content of table " + Constants.TableName.ThreatenedSpeciesUnique + "..."); 

      bridge.DeleteTableContent(
        Constants.TableName.ThreatenedSpeciesUnique
      );

      raiseStatusEvent(" Buffering new content for table " + Constants.TableName.ThreatenedSpecies + "..."); 

      bridge.WriteDataTableAsPoints(
        Constants.TableName.ThreatenedSpecies,
        speciesSightings,
        speciesSightings.Columns.IndexOf(LATITUDE_COL_NAME),
        speciesSightings.Columns.IndexOf(LONGITUDE_COL_NAME)
      );

      raiseStatusEvent(" Buffering new content for table " + Constants.TableName.ThreatenedSpeciesUnique + "...");

      bridge.WriteDataTable(
        Constants.TableName.ThreatenedSpeciesUnique,
        DeriveUniqueThreatenedSpecies(speciesSightings)
      );

      raiseStatusEvent(" Committing database changes (pleaese wait)...");

      bridge.EndTransaction();
    }

    private DataTable DeriveUniqueThreatenedSpecies(DataTable speciesSightings) {
      DataTable uniqueSpeciesTable = new DataTable();
      foreach (string columnName in UNIQUE_SPECIES_COLUMNS) {
        uniqueSpeciesTable.Columns.Add(columnName);
      }

      uniqueSpeciesTable.TableName = Constants.TableName.ThreatenedSpeciesUnique;

      IEnumerable<DataRow> query = (
        from rows in speciesSightings.AsEnumerable()
        group rows by rows.Field<String>("ScientificName") into grp
          select grp.First()
      );

      foreach (DataRow uniqueSpeciesRow in query) {
        DataRow newRow = uniqueSpeciesTable.NewRow();

        foreach (string header in UNIQUE_SPECIES_COLUMNS) {
          newRow[header] = uniqueSpeciesRow[header];
        }

        uniqueSpeciesTable.Rows.Add(newRow);
      }

      return uniqueSpeciesTable;
    }

    public string[] GetSpeciesClassNames() {
      List<string> names = bridge.GetUniqueColValuesForQuery<string>(
        Constants.TableName.ThreatenedSpeciesUnique,
        null,
        "ClassName"
      );
      names.Sort();
      return names.ToArray();
    }

    public string[] GetSpeciesStatuses() {
      List<string> statuses = bridge.GetUniqueColValuesForQuery<string>(
        Constants.TableName.ThreatenedSpeciesUnique,
        null,
        "NSWStatus"
      );

      // Original dataset fuses a number of statuses as a comma separated list,
      // below, we split them up as unique entries.

      List<string> uniqueStatuses = new List<string>();
      foreach (string status in statuses) {
        if (status.IndexOf(",") != -1) {
          string[] splitStatus = status.Split(',');
          foreach (string uniqueStatus in splitStatus) {
            if (!uniqueStatuses.Contains(uniqueStatus)) {
              uniqueStatuses.Add(uniqueStatus);
            }
          }
        } else {
            if (!uniqueStatuses.Contains(status)) {
              uniqueStatuses.Add(status);
           }
        }
      }

      uniqueStatuses.Sort();

      return uniqueStatuses.ToArray();
    }

    public DataTable GetSelectedSpecies(string[] columnNames, string[] classesSelected, string[] statusesSelected) {

      DataTable species = new DataTable();

      using (ComReleaser comReleaser = new ComReleaser()) {

        ICursor cursor = bridge.GetCursorForQuery(
          Constants.TableName.ThreatenedSpeciesUnique,
          buildSelectedSpeciesClause(
            classesSelected, 
            statusesSelected
          ),
          string.Join(",", columnNames)
        );

        comReleaser.ManageLifetime(cursor);

        species = Common.CursorToDataTable(columnNames, cursor);

      } // using comReleaser

      return species;
    }

    private string buildSelectedSpeciesClause(string[] classesSelected, string[] statusesSelected) {
      StringBuilder sb = new StringBuilder();

      if (statusesSelected.Length > 0) {
        sb.Append("(");
        for (int i = 0; i < statusesSelected.Length; i++) {
          if (i > 0) {
            sb.Append(" OR ");
          }
          sb.Append("NSWStatus LIKE '%");
          sb.Append(statusesSelected[i]);
          sb.Append("%'");
        }
        sb.Append(") ");
      }
      if (classesSelected.Length > 0) {
        if (sb.Length > 0) {
          sb.Append(" AND ");
        }
        sb.Append("(");
        for (int i = 0; i < classesSelected.Length; i++) {
          if (i > 0) {
            sb.Append(" OR ");
          }
          sb.Append("ClassName ='");
          sb.Append(classesSelected[i]);
          sb.Append("'");
        }
        sb.Append(")");
      }

      return sb.ToString();
    }

    public DataTable GetWetlandsBySpecies(string scientificName, double buffer, DateTime? afterDate, DateTime? beforeDate) {
      return Common.FeatureListToDataTable(
        GetWetlandsBySpeciesList(
          scientificName, 
          buffer, 
          afterDate, 
          beforeDate
        )
      );
    }

    private List<IFeature> GetWetlandsBySpeciesList(string scientificName, double buffer, DateTime? afterDate, DateTime? beforeDate) {
      ProgressEventArgs pe = new ProgressEventArgs(ProgressEventEnums.eProgress.start, 0);

      int steps = 0;
      int step = 0;
 
      StringBuilder speciesQuery = new StringBuilder();
      speciesQuery.Append("ScientificName = '");
      speciesQuery.Append(scientificName);
      speciesQuery.Append("'");

      speciesQuery.Append(
        buildDateQuery(afterDate, beforeDate)
      );

      List<IFeature> species = new List<IFeature>();

      using (ComReleaser comReleaser = new ComReleaser()) {

        IFeatureCursor speciesCursor = bridge.GetCursorForFeatureClassQuery(
          Constants.LayerName.ThreatenedSpecies,
          speciesQuery.ToString(),
          null
        );

        comReleaser.ManageLifetime(speciesCursor);

        IFeature speciesFeature;
        while ((speciesFeature = speciesCursor.NextFeature()) != null) {
          steps++;
          species.Add(speciesFeature);
        }
        pe.Step = steps;
        //ProgressEvent(this, pe);
        //pe.Activity = ProgressEventEnums.eProgress.update;
      }

      List<IFeature> result = new List<IFeature>();

      foreach (IFeature currentSpecies in species) {

        using (ComReleaser comReleaser = new ComReleaser()) {
          IFeatureCursor wetlandsCursor = bridge.GetIntersectionCursor(
            Constants.TableName.MCMAWetlands,
            currentSpecies,
            buffer
          );

          comReleaser.ManageLifetime(wetlandsCursor);

          IFeature wetlandsFeature;
          while ((wetlandsFeature = wetlandsCursor.NextFeature()) != null) {
            AddUniqueFeature(wetlandsFeature, result);
          }
        } // using comReleaser

        step++;
        pe.Step = step;
        //ProgressEvent(this, pe);
      }

      pe.Activity = ProgressEventEnums.eProgress.finish;
      //ProgressEvent(this, pe);
      return result;
    }
      
    private static void AddUniqueFeature(IFeature newFeature, List<IFeature> result) {
      // result.Contains() fails to catch duplicates.
      foreach (IFeature feature in result) {
        if (feature.OID == newFeature.OID) {
          return;
        }
      }
      result.Add(newFeature);

    }

    //public IFeatureCursor GetSpeciesByWetland(string wetlandID, double buffer, DateTime? afterDate, DateTime? beforeDate, string speciesFilter) {
    //  IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
    //  IQueryFilter pQueryFilter = new QueryFilterClass();
    //  pQueryFilter.WhereClause = string.Format("MBCMA_wetland = {0}", wetlandID);
    //  IFeatureCursor pFeatureCursor = pFeatureClass.Search(pQueryFilter, false);
    //  IFeature pFeature = pFeatureCursor.NextFeature();
    //  ITopologicalOperator pTopo = (ITopologicalOperator)pFeature.Shape;
    //  IGeometry pGeometry = pTopo.Buffer(buffer);
    //  if (pFeature != null) {
    //    IFeatureClass speciesFC = pFeatureWorkspace.OpenFeatureClass(Constants.LayerName.ThreatenedSpecies);
    //    ISpatialFilter pSpatialFilter = new SpatialFilterClass();
    //    pSpatialFilter.GeometryField = speciesFC.ShapeFieldName;
    //    pSpatialFilter.Geometry = pGeometry;
    //    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
    //    StringBuilder sb = new StringBuilder();

    //    sb.Append(speciesFilter);
    //    sb.Append(
    //      buildDateQuery(afterDate, beforeDate)
    //    );

    //    pSpatialFilter.WhereClause = sb.ToString();
    //    IFeatureCursor speciesCursor = speciesFC.Search(pSpatialFilter, false);
    //    return speciesCursor;
    //  }
    //  return null;
    //}

    public static string DateFormat(DateTime val) {
      return string.Format(
        "{0} 00:00:00", val.ToString("yyyy-MM-dd")
      );
    }

    private string buildDateQuery(DateTime? afterDate, DateTime? beforeDate) {
      StringBuilder sb = new StringBuilder();

      if (afterDate.HasValue) {
        if (sb.Length > 0) {
          sb.Append(" AND ");
        }
        sb.Append('"');
        sb.Append("DateFirst");
        sb.Append('"');
        sb.Append("> date '");
        sb.Append(DateFormat(afterDate.Value));
        sb.Append("'");
      }
      if (beforeDate.HasValue) {
        if (sb.Length > 0) {
          sb.Append(" AND ");
        }
        sb.Append('"');
        sb.Append("DateLast");
        sb.Append('"');
        sb.Append("< date '");
        sb.Append(DateFormat(beforeDate.Value));
        sb.Append("'");
      }

      return sb.ToString();
    }

  }
}
