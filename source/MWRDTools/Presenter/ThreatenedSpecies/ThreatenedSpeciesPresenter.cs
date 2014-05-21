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

    private IThreatenedSpeciesView view;

    public void setView(IThreatenedSpeciesView view) {
      this.view = view;
    }

    public void SpeciesByWetlandsTabSelected() {
      view.SetWetlands(
        wetlandsModel.GetAllWetlands()
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
  }
}
