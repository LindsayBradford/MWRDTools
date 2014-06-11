using ESRI.ArcGIS.Framework;

using MWRDTools.Presenter;
using MWRDTools.Model;

namespace MWRDTools.View
{
  class CommenceToFillFormBuilder {

    public static CommenceToFillForm build(IApplication appHook) {

      CommenceToFillForm form = new CommenceToFillForm();

      IMapUtils mapUtils = new MapUtils();
      mapUtils.setApplication(appHook);

      ModelBuilder.SetApplication(
        appHook,
        mapUtils.GetMapDatabase()
      );


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
