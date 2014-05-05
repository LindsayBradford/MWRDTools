using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MWRDTools.Model {
  public class ModelStatusEventArgs : EventArgs {
    public string Status;
    public Boolean progressesStatus = false;
  }
}
