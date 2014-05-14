using System;
using System.Windows.Forms;
using System.Collections;
using System.IO;

using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;

using MWRDTools.Presenter;
using MWRDTools.Model;

namespace MWRDTools.View
{
  class CommenceToFillFormBuilder : AbstractFormBuilder {

    public static CommenceToFillForm build(IApplication appHook) {

      CommenceToFillForm form = new CommenceToFillForm();
      form.Application = appHook;

      IGeodatabaseBridge dbBridge = AbstractFormBuilder.buildDatabaseBridge(appHook);

      IWetlandsModel wetlandsModel = new WetlandsModel();
      wetlandsModel.setDatabaseBridge(dbBridge);

      ICARMScenarioModel carmModel = new CARMScenarioModel();
      carmModel.setDatabaseBridge(dbBridge);
      carmModel.setWetlandsModel(wetlandsModel);

      ICommenceToFillPresenter presenter = new CommenceToFillPresenter();
      presenter.setCARMScenarioModel(carmModel);
      presenter.setWetlandsModel(wetlandsModel);
      presenter.setView(form);

      (form as ICommenceToFillView).setPresenter(presenter);

      return form;
    }

  }
}
