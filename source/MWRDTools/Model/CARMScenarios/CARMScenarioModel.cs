using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;

namespace MWRDTools.Model
{
  class CARMScenarioModel : AbstractMWRDModel, ICARMScenarioModel
  {

    private const string DISCHARGE_GROUP_NAME = "Discharge";

    // CARM reports discharge in metres^3/second. LLS in Megalitres/day.
    // conversion factor below used to move between the two scales.
    
    private const double DISCHARGE_CONVERSION_FACTOR = 86.4;

    private IWetlandsModel wetlandsModel;

    public void setWetlandsModel(IWetlandsModel model) {
      this.wetlandsModel = model;
    }

    struct DischargeAtGauge {
      public long gaugeId;
      public string dischargeId;
      public double maxDischarge;
    };

    public bool ScenarioExists(string scenarioName) {
      ICursor cursor = getGroupTableCursorForScenario(scenarioName);

      if (cursor.NextRow() != null) {
        return true;
      }
      return false;
    }

    public string[] GetScenarios() {
      List<string> scenarios = bridge.GetColValuesForQuery<string>(
        Constants.TableName.CARM_time_series_group,
        "parent_id IS NULL",
        "name"
      );

      return scenarios.ToArray();
    }

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
        if (ScenarioExists(name)) {
          deleteScenario(name);
        }
      }
    }

    private void deleteScenario(string scenarioName) {
      List<string> groupIdList, outputIdList, timeSeriesList;

      string scenarioId = getIdForScenario(scenarioName);

      string outputId = getGroupId("Output", scenarioId);

      string mike11OutputId = getGroupId("MIKE11 Output", outputId);

      outputIdList = bridge.GetColValuesForQuery<string>(
        Constants.TableName.CARM_time_series_group,
        String.Format("parent_id = '{0}'", mike11OutputId),
        "id"
      );

      groupIdList = new List<String>(outputIdList);

      groupIdList.Add(scenarioId);
      groupIdList.Add(outputId);
      groupIdList.Add(mike11OutputId);

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

      foreach (string childId in outputIdList) {

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

    private ICursor getGroupTableCursorForScenario(string scenarioName) {
      return bridge.GetCursorForQuery(
        Constants.TableName.CARM_time_series_group, 
        String.Format("name = '{0}'", scenarioName)
      );
    }

    private void updateDatabase(DataTable[] scenarioTables) {
      foreach (DataTable table in scenarioTables) {
        updateDatabase(table);
      }
    }

    private string getIdForScenario(string scenarioName) {
      return bridge.GetFirstColValueForQuery<string>(
        Constants.TableName.CARM_time_series_group,
         String.Format("name = '{0}' AND parent_id IS NULL", scenarioName),
         "id"
      );
    }

    private string getGroupId(string name, string parentId) {
      return bridge.GetFirstColValueForQuery<string>(
        Constants.TableName.CARM_time_series_group,
        String.Format(
          "name = '{0}' AND parent_id = '{1}'",
          name,
          parentId
        ),
        "id"
      );
    }

    private string getScenarioGroupId(string scenarioName, string groupName) {
      string scenarioId = getIdForScenario(scenarioName);

      string outputId = getGroupId("Output", scenarioId);

      string mike11OutputId = getGroupId("MIKE11 Output", outputId);

      return getGroupId("Discharge", mike11OutputId);
    }

    private void updateDatabase(DataTable scenarioTable) {

      raiseStatusEvent(" Buffering new content for table " + scenarioTable.TableName + "...");
      
      bridge.WriteDataTable(
          scenarioTable.TableName, 
          scenarioTable
       );
    }

    public DataTable GetWetlandsInundated(string scenarioName) {

      string dischargeId = getScenarioGroupId(scenarioName, DISCHARGE_GROUP_NAME);

      List<DischargeAtGauge> discharges = new List<DischargeAtGauge>();

      using (ComReleaser comReleaser = new ComReleaser()) {

        ICursor dischargeCursor = bridge.GetCursorForQuery(
          string.Format(
            "{0}, {1}",
            Constants.TableName.CARM_time_series,
            Constants.TableName.CARM_Locations_Gauges
          ),
          string.Format(
            "{0}.name = {1}.CARM_name AND {0}.group_id = '{2}'",
            Constants.TableName.CARM_time_series,
            Constants.TableName.CARM_Locations_Gauges,
            dischargeId
          ),
          string.Format(
            "{0}.id, {1}.GaugeID",
            Constants.TableName.CARM_time_series,
            Constants.TableName.CARM_Locations_Gauges
          )
        );

        comReleaser.ManageLifetime(dischargeCursor);

        IRow row;
        while ((row = dischargeCursor.NextRow()) != null) {

          DischargeAtGauge newDischarge = new DischargeAtGauge();

          newDischarge.dischargeId = row.get_Value(0) as string;

          newDischarge.gaugeId = (long.Parse(row.get_Value(1).ToString()));

          discharges.Add(newDischarge);
        }
      } // using comReleaser

      for (int i = 0; i < discharges.Count; i++) {
        DischargeAtGauge discharge = discharges[i];

        using (ComReleaser comReleaser = new ComReleaser()) {

          ICursor maxDischargeCursor = bridge.GetCursorForQuery(
            Constants.TableName.CARM_time_series_value,
            string.Format(
              "time_series_id = '{0}'",
              discharge.dischargeId
            )
          );

          comReleaser.ManageLifetime(maxDischargeCursor);

          double maxDischarge = 0;

          IRow row;
          while((row = maxDischargeCursor.NextRow()) != null) {
            try {
              int dataIndex = row.Fields.FindField("data_value");
              double rowDischarge = DISCHARGE_CONVERSION_FACTOR * double.Parse(row.get_Value(dataIndex).ToString());
              maxDischarge = Math.Max(rowDischarge, maxDischarge);
            } catch (Exception e) {
              MessageBox.Show(e.Message);
            }
          }

          discharge.maxDischarge = maxDischarge;

          discharges[i] = discharge;

        } // using comReleaser
      } //  for each discharge

      DataTable wetlandsTable = null;

      foreach (DischargeAtGauge discharge in discharges) {
        DataTable dischargeTable = wetlandsModel.GetInundatedWetlandsByFlowAtGauge(
            discharge.gaugeId,
            discharge.maxDischarge
          );

        if (dischargeTable == null || dischargeTable.Rows.Count == 0) {
          continue;
        }

        if (wetlandsTable == null) {
          wetlandsTable = dischargeTable;
          continue;
        }

        //Add new rows only to wetlandsTable

        IEnumerable<string> currentWetlandsQuery = (
          from rows in wetlandsTable.AsEnumerable()
          select rows.Field<String>("WetlandsID")
        );

        List<string> currentWetlands = new List<string>(currentWetlandsQuery);

        IEnumerable<DataRow> newRowsQuery = (
          from rows in dischargeTable.AsEnumerable()
          where !currentWetlands.Contains(rows["WetlandsID"] as string)
          select rows
        );

        if (!Enumerable.Any(newRowsQuery)) {
          continue;
        }

        wetlandsTable.Merge(
          newRowsQuery.CopyToDataTable()  
        );

      }

      return wetlandsTable;
    }
  }
}
