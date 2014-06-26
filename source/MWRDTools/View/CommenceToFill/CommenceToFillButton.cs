/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

namespace MWRDTools.View
{
  class CommenceToFillButton : AbstractFormLaunchButton
  {
    public CommenceToFillButton() : base() {
      form = CommenceToFillFormBuilder.build(getAppHook());
    }
  }
}
