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
  class ThreatenedSpeciesButton : AbstractFormLaunchButton
  {
    protected override void OnClick()
    {
      frmThreatenedSpecies form = new frmThreatenedSpecies(getAppHook());
      form.Show(getParentWindow());
    }
  }
}
