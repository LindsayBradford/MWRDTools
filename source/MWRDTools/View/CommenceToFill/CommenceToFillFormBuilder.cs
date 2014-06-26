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
  class CommenceToFillFormBuilder {

    public static CommenceToFillForm build(IApplication appHook) {

      CommenceToFillForm form = new CommenceToFillForm();

      IMapUtils mapUtils = new MapUtils();
      mapUtils.setApplication(appHook);

      ModelBuilder.SetApplication(
        appHook,
        mapUtils.GetMapDatabase()
      );


      ICommenceToFillPresenter presenter = new CommenceToFillPresenter();

      presenter.setCARMScenarioModel(
        ModelBuilder.GetCARMScenarioModel()
      );

      presenter.setFileBridge(
        ModelBuilder.GetFileSystemBridge()
      );

      presenter.setWetlandsModel(
        ModelBuilder.GetWetlandsModel()  
      );

      presenter.setMapUtils(mapUtils);

      (form as ICommenceToFillView).setPresenter(presenter);

      return form;
    }

  }
}
