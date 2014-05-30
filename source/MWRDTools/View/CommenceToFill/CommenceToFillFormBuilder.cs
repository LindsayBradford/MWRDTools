using ESRI.ArcGIS.Framework;

using MWRDTools.Presenter;
using MWRDTools.Model;

namespace MWRDTools.View
{
  class CommenceToFillFormBuilder {

    public static CommenceToFillForm build(IApplication appHook) {

      CommenceToFillForm form = new CommenceToFillForm();

      ModelBuilder.SetApplication(appHook);

      IMapUtils mapUtils = new MapUtils();
      mapUtils.setApplication(appHook);

      ICommenceToFillPresenter presenter = new CommenceToFillPresenter();

      presenter.setCARMScenarioModel(
        ModelBuilder.GetCARMScenarioModel()
      );

      presenter.setFileBridge(
        ModelBuilder.GetFileSystemBridge()
      );

      presenter.setWetlandsModel(
        ModelBuilder.GetWetlandsModel()  
      );

      presenter.setMapUtils(mapUtils);

      (form as ICommenceToFillView).setPresenter(presenter);

      return form;
    }

  }
}
