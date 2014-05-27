using System;
using System.ComponentModel;
using System.Data;

using MWRDTools.Model;

namespace MWRDTools.Presenter {
  public class ThreatenedSpeciesPresenter : AbstractMWRDPresenter, IThreatenedSpeciesPresenter {

    private static string[] WETLAND_COLUMNS = { 
      "MBCMA_wetland", "Wetland_Name", "Complex_name", "Flow" 
    };

    private static string[] SPECIES_COLUMNS = { 
      "ScientificName", "CommonName", "ClassName", "FamilyName", "NSWStatus", "SpeciesCode"
    };

    public event EventHandler<ThreatenedSpeciesEventArgs> ThreatenedSpeciesPresentationChanged;
    public event EventHandler<ProgressChangedEventArgs> StatusChanged;

    public string[] GetSpeciesClasses() {
      return threatenedSpeciesModel.GetSpeciesClassNames();
    }

    public string[] GetSpeciesStatuses() {
      return threatenedSpeciesModel.GetSpeciesStatuses();
    }

    public void SpeciesByWetlandsTabSelected() {
      raisePresentationEvent(
        ThreatenedSpeciesEventType.AllWetlands,
        wetlandsModel.GetAllWetlands(WETLAND_COLUMNS)
      );
    }

    public void SpeciesFilterApplied(string[] classesSelected, string[] statusesSelected) {
      raisePresentationEvent(
        ThreatenedSpeciesEventType.FilteredSpecies,
        threatenedSpeciesModel.GetSelectedSpecies(
          SPECIES_COLUMNS,
          classesSelected,
          statusesSelected
        )
      );
    }

    public void FindWetlandsBySpecies(string speciesScientificName, double buffer, DateTime? afterDate, DateTime? beforeDate) {
      raisePresentationEvent(
        ThreatenedSpeciesEventType.WetlandsForSpecies,
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
      raisePresentationEvent(
        ThreatenedSpeciesEventType.SpeciesForWetlands,
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
      model.StatusChanged += new EventHandler<ProgressChangedEventArgs>(this.HandleModelStatusEvent);
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

    private void HandleModelStatusEvent(object sender, ProgressChangedEventArgs args) {
      if (StatusChanged != null) {
        StatusChanged(this, args);
      }
    }

    private void raisePresentationEvent(ThreatenedSpeciesEventType type, DataTable eventData) {
      ThreatenedSpeciesPresentationChanged(
        this,
        new ThreatenedSpeciesEventArgs(
          type,
          eventData
        )
      );
    }
  }
}
