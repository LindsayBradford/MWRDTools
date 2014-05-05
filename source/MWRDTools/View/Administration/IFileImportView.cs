using System;

using ESRI.ArcGIS.Framework;

namespace MWRDTools.View
{
  // Note: Interface inheritance breaks polymorhhism in C#. 
  // As a work-around, I''m duplicating the intterface per
  // file import view type instead of extending a base interface
  // with a common method signature.
  // See: http://www.roxolan.com/2009/04/interface-inheritance-in-c.html

  public interface ICARMScenarioImportView {

    IApplication Application { get; set; }

    void ImportDirectory(string directoryPath);
    void ShowStatusString(string statusString, Boolean progresesStatus = true);
  }

  public interface INSWAtlasWildlifeImportView {

    IApplication Application { get; set; }

    void ImportFiles(params string[] files);
    void ShowStatusString(string statusString, Boolean progressesStatus = true);
  }
}
