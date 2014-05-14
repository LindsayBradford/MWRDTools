using System;
using System.ComponentModel;
using System.Data;

namespace MWRDTools.Model
{
  public interface ICARMScenarioModel
  {
    event EventHandler<ProgressChangedEventArgs> StatusChanged;

    void setDatabaseBridge(IGeodatabaseBridge bridge);
    void setWetlandsModel(IWetlandsModel wetlandsModel);

    void WriteScenarioData(params DataTable[] scenarioTables);

    bool ScenarioExists(string scenarioName);
    string[] GetScenarios();
    DataTable GetWetlandsInundated(string scenarioName);
  }
}
