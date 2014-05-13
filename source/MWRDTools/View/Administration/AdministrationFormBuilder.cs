using System;
using System.Windows.Forms;
using System.Collections;
using System.IO;

using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;

using MWRDTools.Presenter;
using MWRDTools.Model;

namespace MWRDTools.View
{
  class AdministrationFormBuilder
  {

    public static AdministrationForm build(IApplication appHook)
    {
      
      AdministrationForm form = new AdministrationForm();
      form.Application = appHook;

      FileSystemBridge fileBridge = new FileSystemBridge();
      IGeodatabaseBridge dbBridge = buildDatabaseBridge(appHook);

      ICARMScenarioModel carmModel = new CARMScenarioModel();
      carmModel.setDatabaseBridge(dbBridge);

      form.setCarmImportPresenter(
        buildCarmPresenter(
          form,
          fileBridge,
          carmModel
        )
      );

      IThreatenedSpeciesModel speciesModel = new ThreatenedSpeciesModel();
      speciesModel.setDatabaseBridge(dbBridge);

      form.setAtlasImportPresenter(
        buildAtlasPresenter(
          form,
          fileBridge,
          speciesModel
        )
      );

      return form;
    }

    private static ICARMScenarioImportPresenter buildCarmPresenter(
                      ICARMScenarioImportView view, IFileSystemBridge fileBridge, ICARMScenarioModel model)
    {
      CARMImportPresenter presenter = new CARMImportPresenter();

      presenter.setView(view);
      presenter.setFileBridge(fileBridge);
      presenter.setModel(model);

      return presenter;
    }

    private static INSWAtlasWildlifeImportPresenter buildAtlasPresenter(
                      INSWAtlasWildlifeImportView view, IFileSystemBridge fileBridge, IThreatenedSpeciesModel model)
    {
      AtlasImportPresenter presenter = new AtlasImportPresenter();

      presenter.setView(view);
      presenter.setFileBridge(fileBridge);
      presenter.setModel(model);

      return presenter;
    }

    private static IGeodatabaseBridge buildDatabaseBridge(IApplication appHook) {
      string documentPath = Path.GetDirectoryName(getDocumentPath(appHook));

      string relativeDBPath = "MWRD_File_Geodatabase\\MWRD.gdb";

      string databasePath = Path.Combine(documentPath, relativeDBPath);

      FileGeodatabaseBridge bridge = new FileGeodatabaseBridge();

      bridge.DatabasePath = databasePath;

      return bridge as IGeodatabaseBridge;
    }

    private static string getDocumentPath(IApplication appHook) {
      // Apparently, the last template is always the document's path.
      // http://help.arcgis.com/en/sdk/10.0/arcobjects_net/componenthelp/index.html#/Get_Document_Path_Snippet/00490000009n000000/

      ITemplates templates = appHook.Templates;
      return templates.get_Item(templates.Count - 1);

    }
  }
}
