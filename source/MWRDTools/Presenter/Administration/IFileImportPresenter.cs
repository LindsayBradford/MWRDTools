using System;
using System.ComponentModel;
using System.Data;

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
    event EventHandler<ProgressChangedEventArgs> StatusChanged;

    void ImportDirectory(string directoryPath);
    void WriteImportedData(params DataTable[] scenarioTables);

    void setFileBridge(IFileSystemBridge bridge);
    void setModel(ICARMScenarioModel model);
  }

  public interface INSWAtlasWildlifeImportPresenter
  {
    event EventHandler<ProgressChangedEventArgs> StatusChanged;

    void setFileBridge(IFileSystemBridge bridge);
    void setModel(IThreatenedSpeciesModel model);

    void ImportFiles(params String[] files);
    void WriteImportedData(params DataTable[] sightingTables);
  }

  public interface IMapDatabasePresenter {
    void setFileBridge(IFileSystemBridge bridge);
    void setGeodatabaseBridge(IGeodatabaseBridge bridge);

    void SetDatabaseServer(string databaseServer);
    string GetDatabaseServer();
  }

}
