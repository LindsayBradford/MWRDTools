﻿using ESRI.ArcGIS.Framework;

using MWRDTools.Presenter;
using MWRDTools.Model;

namespace MWRDTools.View
{
  class CommenceToFillFormBuilder : AbstractFormBuilder {

    public static CommenceToFillForm build(IApplication appHook) {

      CommenceToFillForm form = new CommenceToFillForm();

      IGeodatabaseBridge dbBridge = AbstractFormBuilder.buildDatabaseBridge(appHook);

      IWetlandsModel wetlandsModel = new WetlandsModel();
      wetlandsModel.setDatabaseBridge(dbBridge);

      ICARMScenarioModel carmModel = new CARMScenarioModel();
      carmModel.setDatabaseBridge(dbBridge);
      carmModel.setWetlandsModel(wetlandsModel);

      ICommenceToFillPresenter presenter = new CommenceToFillPresenter();
      presenter.Application = appHook;
      presenter.setCARMScenarioModel(carmModel);
      presenter.setWetlandsModel(wetlandsModel);
      presenter.setFileBridge(
        new FileSystemBridge()
      );

      (form as ICommenceToFillView).setPresenter(presenter);

      return form;
    }

  }
}
