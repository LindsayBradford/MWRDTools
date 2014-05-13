using System;
using System.ComponentModel;
using System.Data;

namespace MWRDTools.Model
{
  public interface ICARMScenarioModel
  {
    event EventHandler<ProgressChangedEventArgs> StatusChanged;

    void setDatabaseBridge(IGeodatabaseBridge bridge);
    void WriteScenarioData(params DataTable[] scenarioTables);
  }
}
