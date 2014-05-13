using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;

namespace MWRDTools.Model
{
  class CARMScenarioModel : AbstractMWRDModel, ICARMScenarioModel
  {

    public void WriteScenarioData(params DataTable[] scenarioTables)
    {
      for (int i = 0; i < scenarioTables.Length; i++) {
        if (scenarioTables[i] == null) {
          raiseStatusEvent(" Error: Could not load CARM scenario files. Import failed. ");
        }
      }

      string[] scenarioNames = deriveScenarioNames(scenarioTables);

      bridge.BeginTransaction();

      deleteScenariosFromDatabase(scenarioNames);

      updateDatabase(scenarioTables);

      raiseStatusEvent(" Committing database changes (pleaese wait)...");

      bridge.EndTransaction();
    }

    private string[] deriveScenarioNames(DataTable[] scenarioTables) {
      List<string> scenarioNames = new List<string>();

      DataTable groupTable = getGroupTable(scenarioTables);

      IEnumerable<string> query = (
        from rows in groupTable.AsEnumerable()
        where rows.Field<String>("parent_id").Equals("")
        select rows.Field<String>("name")
      );

      foreach (string name in query) {
        scenarioNames.Add(name);
      }

      return scenarioNames.ToArray();
    }

    private DataTable getGroupTable(DataTable[] scenarioTables) {
      foreach (DataTable table in scenarioTables) {
        if (table.TableName.Equals(Constants.TableName.CARM_time_series_group)) {
          return table;
        }
      }
      return null;
    }

    private void deleteScenariosFromDatabase(string[] scenarioNames) {
      foreach (string name in scenarioNames) {
        if (scenarioExistsInDatabase(name)) {
          deleteScenario(name);
        }
      }
    }

    private void deleteScenariosWithPredjudice(string[] scenarioNames) {
      bridge.DeleteTableContent(
        Constants.TableName.CARM_time_series_group
      );
      bridge.DeleteTableContent(
        Constants.TableName.CARM_time_series
      );
      bridge.DeleteTableContent(
        Constants.TableName.CARM_time_series_value
      );
    }

    private void deleteScenario(string scenarioName) {
      List<string> idListToDelete = new List<string>();

      ICursor cursor = getGroupTableCursorForScenario(scenarioName);

      IRow groupRow = cursor.NextRow();

      idListToDelete.Add(
        bridge.GetValueForRowColumnName<string>(
          groupRow, 
          "id"
        )
      );

      string childWhereClause = String.Format("parent_id = '{0}'", idListToDelete[0]);

      ICursor childRows = bridge.GetCursorForTableQuery(
        Constants.TableName.CARM_time_series_group,
        childWhereClause
      );

      IRow childRow;
      while((childRow = childRows.NextRow()) != null) {
        idListToDelete.Add(
          bridge.GetValueForRowColumnName<string>(
            childRow,
            "id"
          )
        );
      } // while there are more childRows

      raiseStatusEvent(
        String.Format(" Deleting scenario '{0}' from CARM time-series tables...", scenarioName)  
      ); 

      foreach (string id in idListToDelete) {
        string deleteGroupClause = String.Format("id = '{0}'", id);

        bridge.DeleteTableContent(
          Constants.TableName.CARM_time_series_group,
          deleteGroupClause
        );

        string deleteTimeSeriesClause = String.Format("group_id = '{0}'", id);

        bridge.DeleteTableContent(
          Constants.TableName.CARM_time_series,
          deleteTimeSeriesClause
        );

        string deleteValueClause = String.Format("time_series_id = '{0}'", id);

        bridge.DeleteTableContent(
          Constants.TableName.CARM_time_series_value,
          deleteValueClause
        );

      } // for each id to delete

    }

    public bool scenarioExistsInDatabase(string scenarioName)
    {
      ICursor cursor = getGroupTableCursorForScenario(scenarioName);

      if (cursor.NextRow() != null) {
        return true;
      }
      return false;
    }

    private ICursor getGroupTableCursorForScenario(string scenarioName) {
      string whereClause = String.Format("name = '{0}'", scenarioName);

      return bridge.GetCursorForTableQuery(
        Constants.TableName.CARM_time_series_group, 
        whereClause
      );
    }

    private void updateDatabase(DataTable[] scenarioTables) {
      foreach (DataTable table in scenarioTables) {
        updateDatabase(table);
      }
    }

    private void updateDatabase(DataTable scenarioTable) {

      raiseStatusEvent(" Buffering new content for table " + scenarioTable.TableName + "...");
      
      bridge.WriteDataTable(
          scenarioTable.TableName, 
          scenarioTable
       );
    }
  }
}
