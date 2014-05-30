using ESRI.ArcGIS.Framework;

using MWRDTools.Presenter;
using MWRDTools.Model;

namespace MWRDTools.View
{
  class AdministrationFormBuilder 
  {

    public static AdministrationForm build(IApplication appHook) {

      ModelBuilder.SetApplication(appHook);

      AdministrationForm form = new AdministrationForm();

      IMapUtils mapUtils = new MapUtils();
      mapUtils.setApplication(appHook);

      CARMImportPresenter carmPresenter = new CARMImportPresenter();

      carmPresenter.setFileBridge(
        ModelBuilder.GetFileSystemBridge()
      );

      carmPresenter.setModel(
        ModelBuilder.GetCARMScenarioModel()
      );

      form.setCarmImportPresenter(
        carmPresenter
      );

      AtlasImportPresenter atlasPresenter = new AtlasImportPresenter();

      atlasPresenter.setFileBridge(
        ModelBuilder.GetFileSystemBridge()
      );

      atlasPresenter.setModel(
        ModelBuilder.GetThreatenedSpeciesModel()
      );

      atlasPresenter.setMapUtils(mapUtils);

      form.setAtlasImportPresenter(
        atlasPresenter
      );

      return form;
    }
  }
}
