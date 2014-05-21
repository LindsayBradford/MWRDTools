using System;
using System.Data;
using System.Linq;

using MWRDTools.Model;

using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;

namespace MWRDTools.Presenter {
  public abstract class AbstractMWRDPresenter {

    protected IFileSystemBridge fileBridge;
    private IApplication application;

    public void setFileBridge(IFileSystemBridge fileBridge) {
      this.fileBridge = fileBridge;
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

    public void HighlightFeatures(int[] featureIDs, string layerName) {
      if (featureIDs == null) return;

      Common.HighlightFeatures(
        featureIDs,
        Common.GetFeatureLayer(
          Map,
          layerName
        ),
        Map
      );
    }

    public void ZoomToFeatures(int[] featuretIDs, string layerName) {
      if (featuretIDs == null) return;

      Common.ZoomToFeatures(
        featuretIDs,
        Common.GetFeatureLayer(
          Map,
          layerName
        ),
        Map
      );
    }

    public void FlashFeatures(int[] featuretIDs, string layerName) {
      if (featuretIDs == null) return;

      Common.FlashFeatures(
        featuretIDs,
        Common.GetFeatureLayer(
          Map,
          layerName
        ),
        Map
      );
    }

    public void HighlightWetlands(int[] wetlandIDs) {
      HighlightFeatures(
        wetlandIDs,
        Constants.LayerName.WetLands
      );
    }

    public void FlashWetlands(int[] wetlandIDs) {
      FlashFeatures(
        wetlandIDs,
        Constants.LayerName.WetLands
      );
    }

    public void ZoomToWetlands(int[] wetlandIDs) {
      ZoomToFeatures(
        wetlandIDs, 
        Constants.LayerName.WetLands
      );
    }

    public void ExportFeatures(string filename, DataTable featureTable, int[] featureIDs) {

      if (featureTable == null || featureTable.Rows.Count == 0) return;
      if (featureIDs == null || featureIDs.Count() == 0) return;

      DataTable exportableWetlands = featureTable.Clone();

      foreach (DataRow featureRow in featureTable.Rows) {
        int index = Array.IndexOf(featureIDs, Convert.ToInt32(featureRow["OID"]));
        if (index != -1) {
          exportableWetlands.ImportRow(featureRow);
        }
      }

      this.fileBridge.DataTableToCSV(
        exportableWetlands,
        filename
      );
    }
  }
}
