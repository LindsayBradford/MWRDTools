using System;
using System.Data;

using MWRDTools.Model;

namespace MWRDTools.Presenter {
  public class CommenceToFillPresenter : AbstractMWRDPresenter , ICommenceToFillPresenter {

    public event EventHandler<InundatedWetlandsEventArgs> InundatedWetlandsChanged;

    public void CARMScenarioSelected(string scenarioName) {
      raiseInundatedWetlandsEvent(
        InundatedWetlandsType.CARMScenario, 
        carmScenarioModel.GetWetlandsInundated(scenarioName)
      );
    }

    public void GaugeAndFlowSelected(string gaugeName, double flowAtGauge) {
      raiseInundatedWetlandsEvent(
        InundatedWetlandsType.FlowAtGauge,
        wetlandsModel.GetInundatedWetlandsByFlowAtGauge(
          gaugeName, 
          flowAtGauge
         )
      );
    }

    public void WaggaGaugeThresholdSelected(string flowThreshold) {
      raiseInundatedWetlandsEvent(
        InundatedWetlandsType.WaggaGaugeThreshold,
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

    private void raiseInundatedWetlandsEvent(InundatedWetlandsType type, DataTable inundatedWetlands) {
      InundatedWetlandsChanged(
        this, 
        new InundatedWetlandsEventArgs(
          type, 
          inundatedWetlands
        )
      );
    }
  }
}
