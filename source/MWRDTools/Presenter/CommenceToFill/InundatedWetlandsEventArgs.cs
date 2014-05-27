using System;
using System.Data;

namespace MWRDTools.Presenter {

  public enum InundatedWetlandsType {
    WaggaGaugeThreshold,
    FlowAtGauge,
    CARMScenario
  }

  public class InundatedWetlandsEventArgs : EventArgs {

    private InundatedWetlandsType type;
    private DataTable wetlandsTable;

    public InundatedWetlandsEventArgs(InundatedWetlandsType type, DataTable wetlandsTable) {
      this.type = type;
      this.wetlandsTable = wetlandsTable;
    }

    public InundatedWetlandsType Type {
      get {
        return this.type;
      }
    }

    public DataTable Wetlands {
      get {
        return this.wetlandsTable;
      }
    }
  }
}
