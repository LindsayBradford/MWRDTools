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

    private void deleteScenario(string scenarioName) {
      List<string> groupIdList, scenarioChildIdList, timeSeriesList;

      groupIdList = bridge.GetColValuesForQuery<string>(
        Constants.TableName.CARM_time_series_group,
         String.Format("name = '{0}'", scenarioName),
         "id"
      );

      scenarioChildIdList = bridge.GetColValuesForQuery<string>(
        Constants.TableName.CARM_time_series_group,
        String.Format("parent_id = '{0}'", groupIdList[0]),
        "id"
      );

      groupIdList.AddRange(scenarioChildIdList);

      raiseStatusEvent(
        String.Format(" Deleting scenario '{0}' from CARM time-series tables...", scenarioName)  
      ); 

      foreach (string groupId in groupIdList) {
        bridge.DeleteTableContent(
          Constants.TableName.CARM_time_series_group,
          String.Format("id = '{0}'", groupId)
        );
      }

      timeSeriesList = new List<string>();      

      foreach (string childId in scenarioChildIdList) {

        List<string> currChildList = bridge.GetColValuesForQuery<string>(
          Constants.TableName.CARM_time_series,
          String.Format("group_id = '{0}'", childId),
          "id"
        );

        timeSeriesList.AddRange(currChildList);

        bridge.DeleteTableContent(
          Constants.TableName.CARM_time_series,
          String.Format("group_id = '{0}'", childId)
        );
      }

      foreach (string timeSeriesId in timeSeriesList) {
        bridge.DeleteTableContent(
          Constants.TableName.CARM_time_series_value,
          String.Format("time_series_id = '{0}'", timeSeriesId)
        );
      }
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
      return bridge.GetCursorForTableQuery(
        Constants.TableName.CARM_time_series_group, 
        String.Format("name = '{0}'", scenarioName)
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
