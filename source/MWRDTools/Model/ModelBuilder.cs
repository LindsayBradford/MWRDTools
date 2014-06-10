using System.IO;

using ESRI.ArcGIS.Framework;

namespace MWRDTools.Model {

  public class ModelBuilder {

    private const string RELATIVE_DB_PATH = "MWRD_File_Geodatabase\\MWRD.gdb";

    private static IApplication application;

    private static IFileSystemBridge fileSystemBridge;
    private static IGeodatabaseBridge geodatabaseBridge;
    
    private static WetlandsModel wetlandsModel;
    private static ThreatenedSpeciesModel threatenedSpeciesModel;
    private static CARMScenarioModel carmScenarioModel;

    public static void SetApplication(IApplication application) {
      ModelBuilder.application = application;
    }

    public static IFileSystemBridge GetFileSystemBridge() {
      if (fileSystemBridge == null) {
        fileSystemBridge = new FileSystemBridge();
      }

      return fileSystemBridge as IFileSystemBridge;
    }

    public static IGeodatabaseBridge GetGeodatabaseBridge() {
      if (geodatabaseBridge == null) {
        //geodatabaseBridge = new FileGeodatabaseBridge();
        //string documentPath = Path.GetDirectoryName(getDocumentPath(application));
        //string databasePath = Path.Combine(documentPath, RELATIVE_DB_PATH);
        //geodatabaseBridge.DatabasePath = databasePath;

        ArcSDEGeodatabaseBridge bridge = new ArcSDEGeodatabaseBridge();
        bridge.EstablishConnection();
        geodatabaseBridge = bridge;
      }

      return geodatabaseBridge as IGeodatabaseBridge;
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
