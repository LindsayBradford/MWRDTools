using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.Framework;

using MWRDTools;

namespace MWRDTools.View
{
  class CommenceToFillButton : AbstractFormLaunchButton
  {
    protected override void OnClick()
    {
      frmCommenceToFill form = new frmCommenceToFill(getAppHook());
      form.Show(
        getParentWindow()
      );
    }
  }
}
