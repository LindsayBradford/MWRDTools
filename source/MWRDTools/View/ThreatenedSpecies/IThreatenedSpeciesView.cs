using System.Data;

using MWRDTools.Presenter;

namespace MWRDTools.View {
  public interface IThreatenedSpeciesView {
    void setPresenter(IThreatenedSpeciesPresenter presenter);

    void SetWetlands(DataTable wetlands);

    void ApplySpeciesFilter(DataTable species);

    void ShowWetlandsForSpecies(DataTable wetlands);

    void ShowSpeciesForWetlands(DataTable species);
  }
}
