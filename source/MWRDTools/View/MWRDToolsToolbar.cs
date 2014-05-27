using System;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace MWRDTools
{
  /// <summary>
  /// Summary description for MRDBToolsToolbar.
  /// </summary>
  [Guid("950af1a9-9710-4a12-b227-a3ad43cf95f8")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("MWRDTools.MWRDToolsToolbar")]
  public sealed class MWRDToolsToolbar : BaseToolbar
  {
    #region COM Registration Function(s)
    [ComRegisterFunction()]
    [ComVisible(false)]
    static void RegisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType);

      //
      // TODO: Add any COM registration code here
      //
    }

    [ComUnregisterFunction()]
    [ComVisible(false)]
    static void UnregisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType);

      //
      // TODO: Add any COM unregistration code here
      //
    }

    #region ArcGIS Component Category Registrar generated code
    /// <summary>
    /// Required method for ArcGIS Component Category registration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryRegistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxCommandBars.Register(regKey);
    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxCommandBars.Unregister(regKey);
    }

    #endregion
    #endregion

    public MWRDToolsToolbar(){}

    public override string Caption {
      get {
        return "MWRD Decision Support Toolbar";
      }
    }
    public override string Name {
      get {
        return "MWRDToolsToolbar";
      }
    }
  }
}