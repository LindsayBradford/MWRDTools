using System.Data;

using MWRDTools.View;
using MWRDTools.Model;

using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Carto;

namespace MWRDTools.Presenter {
  public interface ICommenceToFillPresenter {

    IApplication Application {get; set;}
    IMap Map { get; }

    void setView(ICommenceToFillView view);
    void setFileBridge(IFileSystemBridge fileBridge);

    void HigilightWetlands(int[] wetlandIDs);
    void ZoomToWetlands(int[] wetlandIDs);
    void FlashWetlands(int[] wetlandIDs);
    void ExportWetlands(string filename, DataTable wetlands, int[] wetlandIDs);

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
