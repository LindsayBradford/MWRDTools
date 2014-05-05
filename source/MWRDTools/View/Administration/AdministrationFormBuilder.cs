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
      bootstrapDatabaseBridge(appHook);
      
      AdministrationForm form = new AdministrationForm();
      form.Application = appHook;

      FileSystemBridge fileBridge = new FileSystemBridge();

      form.setCarmImportPresenter(
        buildCarmPresenter(
          form,
          fileBridge,
          new CARMScenarioModel()
        )
      );

      form.setAtlasImportPresenter(
        buildAtlasPresenter(
          form,
          fileBridge,
          new ThreatenedSpeciesModel()
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

    private static void bootstrapDatabaseBridge(IApplication appHook) {
      string documentPath = Path.GetDirectoryName(getDocumentPath(appHook));

      string relativeDBPath = "MWRD_File_Geodatabase\\MWRD.gdb";

      string databasePath = Path.Combine(documentPath, relativeDBPath); 

      FileGeodatabaseBridge.getInstance().DatabasePath = databasePath;
    }

    private static string getDocumentPath(IApplication appHook) {
      // Apparently, the last template is always the document's path.
      // http://help.arcgis.com/en/sdk/10.0/arcobjects_net/componenthelp/index.html#/Get_Document_Path_Snippet/00490000009n000000/

      ITemplates templates = appHook.Templates;
      return templates.get_Item(templates.Count - 1);

    }
  }
}
