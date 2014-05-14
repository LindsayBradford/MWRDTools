using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MWRDTools.Model {
  public class AbstractMWRDModel {

    public event EventHandler<ProgressChangedEventArgs> StatusChanged;

    protected IGeodatabaseBridge bridge;

    public void setDatabaseBridge(IGeodatabaseBridge bridge) {
      this.bridge = bridge;
      bridge.StatusChanged += new EventHandler<ProgressChangedEventArgs>(this.HandleBridgeStatusEvent);
    }

    protected void raiseStatusEvent(string status) {
      ProgressChangedEventArgs statusArgs = new ProgressChangedEventArgs(0, status);

      if (StatusChanged != null) {
        StatusChanged(this, statusArgs);
      }
    }

    public void HandleBridgeStatusEvent(object sender, ProgressChangedEventArgs args) {
      if (StatusChanged != null) {
        StatusChanged(this, args);
      }
    }
  }
}
