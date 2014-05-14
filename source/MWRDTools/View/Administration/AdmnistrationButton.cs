using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MWRDTools.View
{
  class AdministrationButton : AbstractFormLaunchButton
  {
    public AdministrationButton() : base() {
      form = AdministrationFormBuilder.build(getAppHook());
    }
  }
}
