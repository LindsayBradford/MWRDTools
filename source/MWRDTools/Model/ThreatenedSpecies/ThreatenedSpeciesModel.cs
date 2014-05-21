using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

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
      return names.ToArray();
    }

    public DataTable GetSpeciesWhere(string[] columnNames, string whereClause) {

      DataTable species = new DataTable();

      using (ComReleaser comReleaser = new ComReleaser()) {

        ICursor cursor = bridge.GetCursorForQuery(
          Constants.TableName.ThreatenedSpeciesUnique,
          whereClause,
          string.Join(",", columnNames)
        );

        comReleaser.ManageLifetime(cursor);

        species = Common.CursorToDataTable(cursor);

      } // using comReleaser

      return species;
    }

  }
}
