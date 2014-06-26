/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

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
