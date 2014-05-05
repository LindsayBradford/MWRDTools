using System;
using System.Data;

namespace MWRDTools.Model
{
  public interface ICARMScenarioModel
  {
    void WriteScenarioData(params DataTable[] scenarioTables);
    bool ScenarioExists(string scenarioName);
  }
}
