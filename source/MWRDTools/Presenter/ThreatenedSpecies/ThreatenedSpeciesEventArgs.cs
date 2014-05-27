using System;
using System.Data;

namespace MWRDTools.Presenter {

  public enum ThreatenedSpeciesEventType {
    AllWetlands,
    FilteredSpecies,
    WetlandsForSpecies,
    SpeciesForWetlands
  }

  public class ThreatenedSpeciesEventArgs : EventArgs {

    private ThreatenedSpeciesEventType type;
    private DataTable eventTable;

    public ThreatenedSpeciesEventArgs(ThreatenedSpeciesEventType type, DataTable eventTable) {
      this.type = type;
      this.eventTable = eventTable;
    }

    public ThreatenedSpeciesEventType Type {
      get {
        return this.type;
      }
    }

    public DataTable EventTable {
      get {
        return this.eventTable;
      }
    }
  }
}
