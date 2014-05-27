using ESRI.ArcGIS.Framework;

using MWRDTools.Presenter;
using MWRDTools.Model;
namespace MWRDTools.View
{
  class ThreatenedSpeciesFormBuilder : AbstractFormBuilder {
    public static ThreatenedSpeciesForm build(IApplication appHook) {

      ThreatenedSpeciesForm form = new ThreatenedSpeciesForm(appHook);

      IGeodatabaseBridge dbBridge = AbstractFormBuilder.buildDatabaseBridge(appHook);

      IWetlandsModel wetlandsModel = new WetlandsModel();
      wetlandsModel.setDatabaseBridge(dbBridge);

      IThreatenedSpeciesModel threatenedSpeciesModel = new ThreatenedSpeciesModel();
      threatenedSpeciesModel.setDatabaseBridge(dbBridge);

      IThreatenedSpeciesPresenter presenter = new ThreatenedSpeciesPresenter();
      presenter.Application = appHook;
      presenter.setWetlandsModel(wetlandsModel);
      presenter.setThreatenedSpeciesModel(threatenedSpeciesModel);
      presenter.setFileBridge(
        new FileSystemBridge()
      );

      form.setPresenter(presenter);

      return form;
    }
  }
}
