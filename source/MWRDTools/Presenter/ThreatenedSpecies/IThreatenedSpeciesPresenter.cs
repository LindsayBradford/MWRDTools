using System.Data;

using MWRDTools.View;
using MWRDTools.Model;

using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Carto;

namespace MWRDTools.Presenter {
  public interface IThreatenedSpeciesPresenter {

    IApplication Application {get; set;}
    IMap Map { get; }

    void setView(IThreatenedSpeciesView view);
    void SpeciesByWetlandsTabSelected();

    void setFileBridge(IFileSystemBridge fileBridge);

    void HigilightWetlands(int[] wetlandIDs);
    void ZoomToWetlands(int[] wetlandIDs);
    void FlashWetlands(int[] wetlandIDs);
    void ExportWetlands(string filename, DataTable wetlands, int[] wetlandIDs);

    void setWetlandsModel(IWetlandsModel model);

    void setThreatenedSpeciesModel(IThreatenedSpeciesModel model);

  }
}
