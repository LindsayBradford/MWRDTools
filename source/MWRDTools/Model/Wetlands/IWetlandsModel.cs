/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

using System.Data;

namespace MWRDTools.Model {
  public interface IWetlandsModel {
    void setDatabaseBridge(IGeodatabaseBridge bridge);

    string[] getGaugeNames();
    string GetGaugeNameForId(long gaugeId);
    string[] getWaggaFlowThresholds();

    DataTable GetAllWetlands(string[] columnNames);
    DataTable GetInundatedWetlandsByWaggaFlowThreshold(string flowThreshold);
    DataTable GetInundatedWetlandsByFlowAtGauge(string gaugeName, double flow);
    DataTable GetInundatedWetlandsByFlowAtGauge(long gaugeId, double flow);
  }
}
