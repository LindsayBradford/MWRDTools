using System;
using System.ComponentModel;
using System.Data;
using MWRDTools.Model;

namespace MWRDTools.Model
{
  public interface ICARMScenarioModel
  {
    event EventHandler<ProgressChangedEventArgs> StatusChanged;
    event EventHandler<CARMScenarioModelEventArgs> ModelChanged;

    void setDatabaseBridge(IGeodatabaseBridge bridge);
    void setWetlandsModel(IWetlandsModel wetlandsModel);

    void WriteScenarioData(params DataTable[] scenarioTables);

    bool ScenarioExists(string scenarioName);
    string[] GetScenarios();
    DataTable GetWetlandsInundated(string scenarioName);
  }
}
