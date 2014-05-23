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

        species = ModelUtils.CursorToDataTable(columnNames, cursor);

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
      return ModelUtils.FeatureListToDataTable(
        GetWetlandsBySpeciesList(
          scientificName, 
          buffer, 
          afterDate, 
          beforeDate
        )
      );
    }

    private List<IFeature> GetWetlandsBySpeciesList(string scientificName, double buffer, DateTime? afterDate, DateTime? beforeDate) {

      StringBuilder speciesQuery = new StringBuilder();
      speciesQuery.Append("ScientificName = '");
      speciesQuery.Append(scientificName);
      speciesQuery.Append("'");

      speciesQuery.Append(
        buildDateQuery(afterDate, beforeDate)
      );

      int steps = 0;

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
      }

      int step = 0;
      raiseStatusEvent(0); 

      List<IFeature> result = new List<IFeature>();

      foreach (IFeature currentSpecies in species) {

        using (ComReleaser comReleaser = new ComReleaser()) {
          IFeatureCursor wetlandsCursor = bridge.GetIntersectionCursor(
            Constants.TableName.MCMAWetlands,
            null,
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

        raiseStatusEvent((int)step/steps * 100); 
      }

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

    public DataTable GetSpeciesByWetlands(string wetlandID, string[] speciesClasses, string[] speciesStatuses, 
                                          double buffer, DateTime? afterDate, DateTime? beforeDate) {
      DataTable speciesTable;

      IFeature wetlandFeature;
      using (ComReleaser comReleaser = new ComReleaser()) {

        IFeatureCursor wetlandsCursor = bridge.GetCursorForFeatureClassQuery(
          Constants.TableName.MCMAWetlands,
          string.Format("MBCMA_wetland = {0}", wetlandID),
          null
        );

        comReleaser.ManageLifetime(wetlandsCursor);

        wetlandFeature = wetlandsCursor.NextFeature();
      }

      if (wetlandFeature == null) {
        return null;
      }

      StringBuilder speciesWhereClause = new StringBuilder();

      speciesWhereClause.Append(
        buildSelectedSpeciesClause(
          speciesClasses, 
          speciesStatuses
        )
      );

      string dateQuery = buildDateQuery(afterDate, beforeDate);

      if (speciesWhereClause.Length > 0 && dateQuery.Length > 0) {
        speciesWhereClause.Append(" AND ");
        speciesWhereClause.Append(dateQuery);
      }

      if (speciesWhereClause.Length == 0) {
        speciesWhereClause.Append(dateQuery);
      }

      using (ComReleaser comReleaser = new ComReleaser()) {

        List<IFeature> speciesList = new List<IFeature>();

        IFeatureCursor speciesCursor = bridge.GetContainsCursor(
          Constants.TableName.ThreatenedSpecies,
          speciesWhereClause.ToString(),
          wetlandFeature,
          buffer
        );

        comReleaser.ManageLifetime(speciesCursor);

        IFeature speciesFeature;
        while ((speciesFeature = speciesCursor.NextFeature()) != null) {
          speciesList.Add(speciesFeature);
        }

        speciesTable = ModelUtils.FeatureListToDataTable(speciesList);
      } // using comReleaser

      return speciesTable;
    }

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
