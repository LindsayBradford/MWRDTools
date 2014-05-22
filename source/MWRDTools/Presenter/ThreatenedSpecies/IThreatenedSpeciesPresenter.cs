using System;
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
    string[] GetSpeciesClasses();
    string[] GetSpeciesStatuses();

    void SpeciesByWetlandsTabSelected();

    void SpeciesFilterApplied(
      string[] classesSelected, 
      string[] statusesSelected
    );

    void setFileBridge(IFileSystemBridge fileBridge);

    void HighlightWetlands(int[] wetlandIDs);
    void ZoomToWetlands(int[] wetlandIDs);
    void FlashWetlands(int[] wetlandIDs);

    void HighlightSpecies(int[] speciesIDs);
    void ZoomToSpecies(int[] speciesIDs);
    void FlashSpecies(int[] speciesIDs);

    void ExportFeatures(string filename, DataTable wetlands, int[] wetlandIDs);

    void setWetlandsModel(IWetlandsModel model);

    void setThreatenedSpeciesModel(IThreatenedSpeciesModel model);

    void FindWetlandsBySpecies(
      string speciesScientificName, 
      double buffer, 
      DateTime? afterDate, 
      DateTime? beforeDate
    );

     void FindSpeciesByWetlands(
      string wetlandsID,
      string[] speciesClasses,
      string[] speciesStatuses,
      double buffer,
      DateTime? afterDate,
      DateTime? beforeDate
    );
  }
}
