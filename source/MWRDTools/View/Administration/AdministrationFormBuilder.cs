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
  class AdministrationFormBuilder 
  {

    public static AdministrationForm build(IApplication appHook) {

      IMapUtils mapUtils = new MapUtils();
      mapUtils.setApplication(appHook);

      ModelBuilder.SetApplication(
        appHook,
        mapUtils.GetMapDatabase()
      );

      AdministrationForm form = new AdministrationForm();

      form.setCarmImportPresenter(
        buildCARMImportPresenter()
      );

      form.setAtlasImportPresenter(
        buildAtlasPresenter(mapUtils)
      );

      return form;
    }

    private static CARMImportPresenter buildCARMImportPresenter() {
      CARMImportPresenter carmPresenter = new CARMImportPresenter();

      carmPresenter.setFileBridge(
        ModelBuilder.GetFileSystemBridge()
      );

      carmPresenter.setModel(
        ModelBuilder.GetCARMScenarioModel()
      );

      return carmPresenter;
    }

    private static AtlasImportPresenter buildAtlasPresenter(IMapUtils mapUtils) {

      AtlasImportPresenter atlasPresenter = new AtlasImportPresenter();

      atlasPresenter.setFileBridge(
        ModelBuilder.GetFileSystemBridge()
      );

      atlasPresenter.setModel(
        ModelBuilder.GetThreatenedSpeciesModel()
      );

      atlasPresenter.setMapUtils(mapUtils);

      return atlasPresenter;
    }
  }
}
