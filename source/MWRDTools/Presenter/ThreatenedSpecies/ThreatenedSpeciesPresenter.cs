using System;
using System.Data;
using System.Linq;

using MWRDTools.View;
using MWRDTools.Model;

using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;

namespace MWRDTools.Presenter {
  public class ThreatenedSpeciesPresenter : AbstractMWRDPresenter, IThreatenedSpeciesPresenter {

    private static string[] WETLAND_COLUMNS = { "MBCMA_wetland", "Wetland_Name", "Complex_name", "Flow" };
    private static string[] SPECIES_COLUMNS = { "ScientificName", "CommonName", "ClassName", "FamilyName", "NSWStatus", "SpeciesCode" };

    private IThreatenedSpeciesView view;

    public void setView(IThreatenedSpeciesView view) {
      this.view = view;
    }

    public void SpeciesByWetlandsTabSelected() {
      view.SetWetlands(
        wetlandsModel.GetAllWetlands(WETLAND_COLUMNS)
      );
    }

    public string[] GetSpeciesClasses() {
      return threatenedSpeciesModel.GetSpeciesClassNames();
    }

    public string[] GetSpeciesStatuses() {
      return threatenedSpeciesModel.GetSpeciesStatuses();
    }

    public void SpeciesFilterApplied(string[] classesSelected, string[] statusesSelected) {
      view.ApplySpeciesFilter(
        threatenedSpeciesModel.GetSelectedSpecies(
          SPECIES_COLUMNS,
          classesSelected,
          statusesSelected
        )
      );
    }

    public void FindWetlandsBySpecies(string speciesScientificName, double buffer, DateTime? afterDate, DateTime? beforeDate) {
      view.ShowWetlandsForSpecies(
        threatenedSpeciesModel.GetWetlandsBySpecies(
          speciesScientificName, 
          buffer, 
          afterDate, 
          beforeDate
        )
      );
    }

    public void FindSpeciesByWetlands(string wetlandsID, string[] speciesClasses, string[] speciesStatuses,
                                      double buffer, DateTime? afterDate, DateTime? beforeDate) {
      view.ShowSpeciesForWetlands(
        threatenedSpeciesModel.GetSpeciesByWetlands(
          wetlandsID,
          speciesClasses,
          speciesStatuses,
          buffer,
          afterDate,
          beforeDate
        )
      );
    }


    #region Wetlands Model

    private IWetlandsModel wetlandsModel;

    public void setWetlandsModel(IWetlandsModel model) {
      this.wetlandsModel = model;
    }

    #endregion

    #region Threatened Species Model

    private IThreatenedSpeciesModel threatenedSpeciesModel;

    public void setThreatenedSpeciesModel(IThreatenedSpeciesModel model) {
      this.threatenedSpeciesModel = model;
    }

    #endregion

    public void HighlightSpecies(int[] speciesIDs) {
      HighlightFeatures(
        speciesIDs,
        Constants.LayerName.ThreatenedSpecies
      );
    }

    public void ZoomToSpecies(int[] speciesIDs) {
      ZoomToFeatures(
        speciesIDs,
        Constants.LayerName.ThreatenedSpecies
      );
    }

    public void FlashSpecies(int[] speciesIDs) {
      FlashFeatures(
        speciesIDs,
        Constants.LayerName.ThreatenedSpecies
      );
    }

  }
}
