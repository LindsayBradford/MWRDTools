/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

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
