using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWRDTools.Model {
  public enum CARMScenarioEventType {
    ScenariosChanged
  }

  public class CARMScenarioModelEventArgs : EventArgs {
    private CARMScenarioEventType type;
    private string[] scenarioList;

    public CARMScenarioModelEventArgs(string[] scenarioList) {
      this.type = CARMScenarioEventType.ScenariosChanged;
      this.scenarioList = scenarioList;
    }

    public CARMScenarioEventType Type {
      get {
        return this.type;
      }
    }

    public string[] ScenarioList {
      get {
        return this.scenarioList;
      }
    }
  }
}
