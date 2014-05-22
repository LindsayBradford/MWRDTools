using System;
using System.ComponentModel;
using System.Data;

namespace MWRDTools.Model 
{
  public interface IThreatenedSpeciesModel
  {
    event EventHandler<ProgressChangedEventArgs> StatusChanged;
    void setDatabaseBridge(IGeodatabaseBridge bridge);
    void OverwriteSightingData(DataTable data);

    string[] GetSpeciesClassNames();
    string[] GetSpeciesStatuses();

    DataTable GetSelectedSpecies(
      string[] columnNames, 
      string[] classesSelected, 
      string[] statusesSelected
    );
  }
}
