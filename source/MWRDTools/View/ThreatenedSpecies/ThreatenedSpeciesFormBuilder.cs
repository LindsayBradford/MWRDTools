/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

using ESRI.ArcGIS.Framework;

using MWRDTools.Presenter;
using MWRDTools.Model;

namespace MWRDTools.View
{
  class ThreatenedSpeciesFormBuilder {
    public static ThreatenedSpeciesForm build(IApplication appHook) {

      IMapUtils mapUtils = new MapUtils();
      mapUtils.setApplication(appHook);

      ModelBuilder.SetApplication(
        appHook,
        mapUtils.GetMapDatabase()
      );

      ThreatenedSpeciesForm form = new ThreatenedSpeciesForm();


      IThreatenedSpeciesPresenter presenter = new ThreatenedSpeciesPresenter();

      presenter.setWetlandsModel(
        ModelBuilder.GetWetlandsModel()
      );

      presenter.setThreatenedSpeciesModel(
        ModelBuilder.GetThreatenedSpeciesModel()  
      );

      presenter.setFileBridge(
        ModelBuilder.GetFileSystemBridge()
      );

      presenter.setMapUtils(mapUtils);

      form.setPresenter(presenter);

      return form;
    }
  }
}
