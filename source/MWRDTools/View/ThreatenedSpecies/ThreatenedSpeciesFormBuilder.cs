using ESRI.ArcGIS.Framework;

using MWRDTools.Presenter;
using MWRDTools.Model;

namespace MWRDTools.View
{
  class ThreatenedSpeciesFormBuilder {
    public static ThreatenedSpeciesForm build(IApplication appHook) {

      ModelBuilder.SetApplication(appHook);

      ThreatenedSpeciesForm form = new ThreatenedSpeciesForm();

      IMapUtils mapUtils = new MapUtils();
      mapUtils.setApplication(appHook);

      IThreatenedSpeciesPresenter presenter = new ThreatenedSpeciesPresenter();

      presenter.setWetlandsModel(
        ModelBuilder.GetWetlandsModel()
      );

      presenter.setThreatenedSpeciesModel(
        ModelBuilder.GetThreatenedSpeciesModel()  
      );

      presenter.setFileBridge(
        ModelBuilder.GetFileSystemBridge()
      );

      presenter.setMapUtils(mapUtils);

      form.setPresenter(presenter);

      return form;
    }
  }
}
