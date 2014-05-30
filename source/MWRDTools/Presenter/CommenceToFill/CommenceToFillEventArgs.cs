using System;
using System.Data;

namespace MWRDTools.Presenter {

  public enum CommenceToFillEventType {
    WaggaGaugeThresholdWetlandsChanged,
    FlowAtGaugeWetlandsChanged,
    CARMScenarioWetlandsChanged,
    CARMScenarioListChanged
  }

  public class CommenceToFillEventArgs : EventArgs {

    private CommenceToFillEventType type;
    private DataTable wetlandsTable;
    private string[] carmScenarioList;

    public CommenceToFillEventArgs(CommenceToFillEventType type, DataTable wetlandsTable) {
      this.type = type;
      this.wetlandsTable = wetlandsTable;
    }

    public CommenceToFillEventArgs(string[] CARMScenarioList) {
      this.type = CommenceToFillEventType.CARMScenarioListChanged;
      this.carmScenarioList = CARMScenarioList;
    }

    public CommenceToFillEventType Type {
      get {
        return this.type;
      }
    }

    public DataTable Wetlands {
      get {
        return this.wetlandsTable;
      }
    }

    public string[] CARMScenarioList {
      get {
        return this.carmScenarioList;
      }
    }
  }
}
