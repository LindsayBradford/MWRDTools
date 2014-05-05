using System;
using System.Data;

using System.Windows.Forms;

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

      // BeginTransaction
      // DeriveScenarioName
      // DeleteScenarioFromDatabaseIfNeeded
      // UpdateDatabaseWithThisScenario.
      // EndTransaction.
      
    }

    public bool ScenarioExists(string scenarioName)
    {
      // TODO: Check that scenario exists before write.
      return false;
    }

  }
}
