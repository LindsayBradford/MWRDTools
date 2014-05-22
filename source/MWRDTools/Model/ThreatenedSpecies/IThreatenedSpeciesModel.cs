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

    DataTable GetWetlandsBySpecies(
      string speciesScientificName, 
      double buffer, 
      DateTime? afterDate, 
      DateTime? beforeDate
    );

    DataTable GetSpeciesByWetlands(
      string wetlandsID,
      string[] speciesClasses,
      string[] speciesStatuses,
      double buffer,
      DateTime? afterDate,
      DateTime? beforeDate
    );
  }
}
