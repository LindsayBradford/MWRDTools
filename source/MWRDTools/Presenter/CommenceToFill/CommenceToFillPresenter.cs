using MWRDTools.View;
using MWRDTools.Model;

namespace MWRDTools.Presenter {
  public class CommenceToFillPresenter : ICommenceToFillPresenter {

    private ICommenceToFillView view;

    public void setView(ICommenceToFillView view) {
      this.view = view;
    }

    public void CARMScenarioSelected(string scenarioName) {
      view.SetCARMScenarioInundatedWetlands(
        carmScenarioModel.GetWetlandsInundated(scenarioName)
      );
    }

    public void GaugeAndFlowSelected(string gaugeName, double flowAtGauge) {
      view.SetFlowAtGaugeInundatedWetlands(
        wetlandsModel.GetInundatedWetlandsByFlowAtGauge(
          gaugeName, 
          flowAtGauge
         )
      );
    }

    public void WaggaGaugeThresholdSelected(string flowThreshold) {
      view.SetWaggaGaugeThresholdInundatedWetlands(
        wetlandsModel.GetInundatedWetlandsByWaggaFlowThreshold(
          flowThreshold
         )
      );
    }
    
    #region Wetlands Model

    private IWetlandsModel wetlandsModel;

    public void setWetlandsModel(IWetlandsModel model) {
      this.wetlandsModel = model;
    }

    public string[] GetGaugeNames() {
      return wetlandsModel.getGaugeNames();
    }

    public string[] GetWaggaFlowThresholds() {
      return wetlandsModel.getWaggaFlowThresholds();
    }

    #endregion

    #region CARMS Model

    private ICARMScenarioModel carmScenarioModel;
    
    public void setCARMScenarioModel(ICARMScenarioModel model) {
      this.carmScenarioModel = model;
    }

    public string[] GetScenarios() {
      return carmScenarioModel.GetScenarios();
    }

    #endregion
  }
}
