using MWRDTools.View;
using MWRDTools.Model;

namespace MWRDTools.Presenter {
  public interface ICommenceToFillPresenter {

    void setView(ICommenceToFillView view);

    void WaggaGaugeThresholdSelected(string flowThreshold);
    void GaugeAndFlowSelected(string gaugeName, double flow);
    void CARMScenarioSelected(string scenarioName);

    void setWetlandsModel(IWetlandsModel model);
    string[] GetScenarios();
    string[] GetWaggaFlowThresholds();
    
    
    string[] GetGaugeNames();
    void setCARMScenarioModel(ICARMScenarioModel model);
  }
}
