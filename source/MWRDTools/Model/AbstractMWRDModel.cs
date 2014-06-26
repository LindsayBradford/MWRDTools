/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

using System;
using System.ComponentModel;

namespace MWRDTools.Model {
  public class AbstractMWRDModel {

    public event EventHandler<ProgressChangedEventArgs> StatusChanged;

    protected IGeodatabaseBridge bridge;

    public void setDatabaseBridge(IGeodatabaseBridge bridge) {
      this.bridge = bridge;
      bridge.StatusChanged += new EventHandler<ProgressChangedEventArgs>(this.HandleBridgeStatusEvent);
    }

    protected void raiseStatusEvent(int percentComplete) {
      raiseStatusEvent(null, percentComplete);
    }

    protected void raiseStatusEvent(string status) {
      raiseStatusEvent(status, 0);
    }

    protected void raiseStatusEvent(string status, int percentComplete) {
      ProgressChangedEventArgs statusArgs = new ProgressChangedEventArgs(percentComplete, status);

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
