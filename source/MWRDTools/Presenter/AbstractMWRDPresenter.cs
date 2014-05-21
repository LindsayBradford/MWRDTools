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

    public void ExportWetlands(string filename, DataTable wetlands, int[] wetlandIDs) {

      if (wetlands == null || wetlands.Rows.Count == 0) return;
      if (wetlandIDs == null || wetlandIDs.Count() == 0) return;

      DataTable exportableWetlands = wetlands.Clone();

      foreach (DataRow wetlandsRow in wetlands.Rows) {
        int index = Array.IndexOf(wetlandIDs, Convert.ToInt32(wetlandsRow["OID"]));
        if (index != -1) {
          exportableWetlands.ImportRow(wetlandsRow);
        }
      }

      this.fileBridge.DataTableToCSV(
        exportableWetlands,
        filename
      );
    }
  }
}
