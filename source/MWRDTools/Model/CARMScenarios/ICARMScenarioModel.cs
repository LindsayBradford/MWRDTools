/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

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
