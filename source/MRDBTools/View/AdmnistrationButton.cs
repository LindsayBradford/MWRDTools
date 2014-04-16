using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Desktop.AddIns;
using ESRI.ArcGIS.Framework;

namespace MRDBTools
{
  class AdministrationButton : AbstractFormLaunchButton
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
