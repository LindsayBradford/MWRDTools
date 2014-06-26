/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

using System.IO;

using ESRI.ArcGIS.Framework;

namespace MWRDTools.Model {

  public class ModelBuilder {

    private const string RELATIVE_DB_PATH = "MWRD_File_Geodatabase\\MWRD.gdb";

    private static IApplication application;
    private static string databaseServer;

    private static IFileSystemBridge fileSystemBridge;
    private static IGeodatabaseBridge geodatabaseBridge;

    private static WetlandsModel wetlandsModel;
    private static ThreatenedSpeciesModel threatenedSpeciesModel;
    private static CARMScenarioModel carmScenarioModel;

    public static void SetApplication(IApplication application, string databaseServer) {
      ModelBuilder.application = application;
      ModelBuilder.databaseServer = databaseServer;
    }

    public static IFileSystemBridge GetFileSystemBridge() {
      if (fileSystemBridge == null) {
        fileSystemBridge = new FileSystemBridge();
      }

      return fileSystemBridge as IFileSystemBridge;
    }

    public static IGeodatabaseBridge GetGeodatabaseBridge() {
      if (geodatabaseBridge == null) {

        if (databaseServer == null) {
          FileGeodatabaseBridge fileGeoBridge = new FileGeodatabaseBridge();
          string documentPath = Path.GetDirectoryName(getDocumentPath(application));
          string databasePath = Path.Combine(documentPath, RELATIVE_DB_PATH);
          fileGeoBridge.DatabasePath = databasePath;
          geodatabaseBridge = fileGeoBridge;
        } else {
          ArcSDEGeodatabaseBridge sdeGeoBridge = new ArcSDEGeodatabaseBridge();
          sdeGeoBridge.EstablishConnection(
            databaseServer
          );
          geodatabaseBridge = sdeGeoBridge;
        }
      }

      return geodatabaseBridge;
    }

    public static IWetlandsModel GetWetlandsModel() {
      if (wetlandsModel == null) {
        wetlandsModel = new WetlandsModel();

        wetlandsModel.setDatabaseBridge(
          GetGeodatabaseBridge()  
        );
      }

      return wetlandsModel as IWetlandsModel;
    }

    public static IThreatenedSpeciesModel GetThreatenedSpeciesModel() {
      if (threatenedSpeciesModel == null) {
        threatenedSpeciesModel = new ThreatenedSpeciesModel();

        threatenedSpeciesModel.setDatabaseBridge(
          GetGeodatabaseBridge()
        );
      }

      return threatenedSpeciesModel as IThreatenedSpeciesModel;
    }

    public static ICARMScenarioModel GetCARMScenarioModel() {
      if (carmScenarioModel == null) {
        carmScenarioModel = new CARMScenarioModel();

        carmScenarioModel.setDatabaseBridge(
          GetGeodatabaseBridge()
        );

        carmScenarioModel.setWetlandsModel(
          GetWetlandsModel()
        );
      }

      return carmScenarioModel as ICARMScenarioModel;
    }

    private static string getDocumentPath(IApplication appHook) {
      // Apparently, the last template is always the document's path.
      // http://help.arcgis.com/en/sdk/10.0/arcobjects_net/componenthelp/index.html#/Get_Document_Path_Snippet/00490000009n000000/

      ITemplates templates = appHook.Templates;
      return templates.get_Item(templates.Count - 1);
    }

  }
}
