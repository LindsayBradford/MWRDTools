using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using ESRI.ArcGIS.Framework;

using MWRDTools.Model;

namespace MWRDTools.View {
  public abstract class AbstractFormBuilder
  {

    private const string RELATIVE_DB_PATH = "MWRD_File_Geodatabase\\MWRD.gdb";

    protected static IGeodatabaseBridge buildDatabaseBridge(IApplication appHook) {
      string documentPath = Path.GetDirectoryName(getDocumentPath(appHook));

      string databasePath = Path.Combine(documentPath, RELATIVE_DB_PATH);

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
