﻿using System;
using System.Data;

using MWRDTools.Model;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;

namespace MWRDTools.Presenter {
  public interface ICommenceToFillPresenter {

    event EventHandler<CommenceToFillEventArgs> CommenceToFillPresenterChanged;

    void setFileBridge(IFileSystemBridge fileBridge);
    void setMapUtils(IMapUtils mapUtils);

    void HighlightWetlands(int[] wetlandIDs);
    void ZoomToWetlands(int[] wetlandIDs);
    void FlashWetlands(int[] wetlandIDs);

    void ExportFeatures(string filename, DataTable features, int[] featureIDs);

    void WaggaGaugeThresholdSelected(string flowThreshold);
    void GaugeAndFlowSelected(string gaugeName, double flow);
    void CARMScenarioSelected(string scenarioName);

    void setWetlandsModel(IWetlandsModel model);
    string[] GetScenarios();
    string[] GetWaggaFlowThresholds();
    
    void setCARMScenarioModel(ICARMScenarioModel model);
    string[] GetGaugeNames();

  }
}
