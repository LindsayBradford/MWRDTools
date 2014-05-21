using System.Data;

namespace MWRDTools.Model {
  public interface IWetlandsModel {
    void setDatabaseBridge(IGeodatabaseBridge bridge);

    string[] getGaugeNames();
    string GetGaugeNameForId(long gaugeId);
    string[] getWaggaFlowThresholds();

    DataTable GetAllWetlands();
    DataTable GetInundatedWetlandsByWaggaFlowThreshold(string flowThreshold);
    DataTable GetInundatedWetlandsByFlowAtGauge(string gaugeName, double flow);
    DataTable GetInundatedWetlandsByFlowAtGauge(long gaugeId, double flow);
  }
}
