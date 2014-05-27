using System;
using System.Data;

using MWRDTools.Model;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;

namespace MWRDTools.Presenter {
  public interface ICommenceToFillPresenter {

    event EventHandler<InundatedWetlandsEventArgs> InundatedWetlandsChanged;

    IApplication Application {get; set;}
    IMap Map { get; }

    void setFileBridge(IFileSystemBridge fileBridge);

    void HighlightWetlands(int[] wetlandIDs);
    void ZoomToWetlands(int[] wetlandIDs);
    void FlashWetlands(int[] wetlandIDs);

    void ExportFeatures(string filename, DataTable wetlands, int[] featureIDs);

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
