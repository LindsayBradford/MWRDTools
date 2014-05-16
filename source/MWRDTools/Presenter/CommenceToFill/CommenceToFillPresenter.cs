using MWRDTools.View;
using MWRDTools.Model;

using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;

namespace MWRDTools.Presenter {
  public class CommenceToFillPresenter : ICommenceToFillPresenter {

    private ICommenceToFillView view;
    private IApplication application;

    public void setView(ICommenceToFillView view) {
      this.view = view;
    }

    public IApplication Application {
      get { return this.application; }
      set { this.application = value; }
    }

    public IMap Map {
      get {
        return (application.Document as IMxDocument).FocusMap; 
      }
    }

    public void HigilightWetlands(int[] wetlandIDs) {
      if (wetlandIDs == null) return;

      Common.HighlightFeatures(
        wetlandIDs, 
        Common.GetFeatureLayer(
          Map, 
          Constants.LayerName.WetLands
        ), 
        Map
      );
    }

    public void ZoomToWetlands(int[] wetlandIDs) {
      if (wetlandIDs == null) return;

      Common.ZoomToFeatures(
        wetlandIDs,
        Common.GetFeatureLayer(
          Map,
          Constants.LayerName.WetLands
        ),
        Map
      );
    }

    public void FlashWetlands(int[] wetlandIDs) {
      if (wetlandIDs == null) return;

      Common.FlashFeatures(
        wetlandIDs,
        Common.GetFeatureLayer(
          Map,
          Constants.LayerName.WetLands
        ),
        Map
      );
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
