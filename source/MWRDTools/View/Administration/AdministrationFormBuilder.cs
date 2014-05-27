using ESRI.ArcGIS.Framework;

using MWRDTools.Presenter;
using MWRDTools.Model;

namespace MWRDTools.View
{
  class AdministrationFormBuilder : AbstractFormBuilder
  {

    public static AdministrationForm build(IApplication appHook) {
      
      AdministrationForm form = new AdministrationForm();
      form.Application = appHook;

      FileSystemBridge fileBridge = new FileSystemBridge();
      IGeodatabaseBridge dbBridge = AbstractFormBuilder.buildDatabaseBridge(appHook);

      ICARMScenarioModel carmModel = new CARMScenarioModel();
      carmModel.setDatabaseBridge(dbBridge);

      form.setCarmImportPresenter(
        buildCarmPresenter(
          fileBridge,
          carmModel
        )
      );

      IThreatenedSpeciesModel speciesModel = new ThreatenedSpeciesModel();
      speciesModel.setDatabaseBridge(dbBridge);

      form.setAtlasImportPresenter(
        buildAtlasPresenter(
          fileBridge,
          speciesModel
        )
      );

      return form;
    }

    private static ICARMScenarioImportPresenter buildCarmPresenter(IFileSystemBridge fileBridge, ICARMScenarioModel model)
    {
      CARMImportPresenter presenter = new CARMImportPresenter();

      presenter.setFileBridge(fileBridge);
      presenter.setModel(model);

      return presenter;
    }

    private static INSWAtlasWildlifeImportPresenter buildAtlasPresenter(IFileSystemBridge fileBridge, IThreatenedSpeciesModel model)
    {
      AtlasImportPresenter presenter = new AtlasImportPresenter();

      presenter.setFileBridge(fileBridge);
      presenter.setModel(model);

      return presenter;
    }
  }
}
