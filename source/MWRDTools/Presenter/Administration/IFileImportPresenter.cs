/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

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
}
