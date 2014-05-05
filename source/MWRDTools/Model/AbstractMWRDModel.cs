using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWRDTools.Model {
  class AbstractMWRDModel {

    public event EventHandler<ModelStatusEventArgs> StatusChanged;

    protected IGeodatabaseBridge bridge = FileGeodatabaseBridge.getInstance();

    public AbstractMWRDModel() {
      bridge.StatusChanged += new EventHandler<ModelStatusEventArgs>(this.HandleBridgeStatusEvent);
    }

    protected void raiseStatusEvent(string status) {
      ModelStatusEventArgs statusArgs = new ModelStatusEventArgs();
      statusArgs.Status = status;
      statusArgs.progressesStatus = true;

      if (StatusChanged != null) {
        StatusChanged(this, statusArgs);
      }
    }

    public void HandleBridgeStatusEvent(object sender, ModelStatusEventArgs args) {
      if (StatusChanged != null) {
        StatusChanged(this, args);
      }
    }
  }
}
