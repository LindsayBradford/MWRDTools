using System;
using System.Data;

using ESRI.ArcGIS.Framework;

using MWRDTools.View;
using MWRDTools.Model;

namespace MWRDTools.Presenter
{
  // Note: Interface inheritance breaks polymorhhism in C#. 
  // As a work-around, I''m duplicating the intterface per
  // file import view type instead of extending a base interface
  // with a common method signature.
  // See: http://www.roxolan.com/2009/04/interface-inheritance-in-c.html

  public interface ICARMScenarioImportPresenter
  {
    void ImportDirectory(string directoryPath);
    void WriteImportedData(params DataTable[] scenarioTables);

    void setView(ICARMScenarioImportView view);
    void setFileBridge(IFileSystemBridge bridge);
    void setModel(ICARMScenarioModel model);
  }

  public interface INSWAtlasWildlifeImportPresenter
  {

    void setView(INSWAtlasWildlifeImportView view);
    void setFileBridge(IFileSystemBridge bridge);
    void setModel(IThreatenedSpeciesModel model);

    void ImportFiles(params String[] files);
    void WriteImportedData(params DataTable[] sightingTables);
  }
}
