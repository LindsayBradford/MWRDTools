/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

using System;
using System.Data;

using MWRDTools.Model;

namespace MWRDTools.Presenter {
  public class CommenceToFillPresenter : AbstractMWRDPresenter , ICommenceToFillPresenter {

    public event EventHandler<CommenceToFillEventArgs> CommenceToFillPresenterChanged;

    public void CARMScenarioSelected(string scenarioName) {
      raiseEvent(
        CommenceToFillEventType.CARMScenarioWetlandsChanged, 
        carmScenarioModel.GetWetlandsInundated(scenarioName)
      );
    }

    public void GaugeAndFlowSelected(string gaugeName, double flowAtGauge) {
      raiseEvent(
        CommenceToFillEventType.FlowAtGaugeWetlandsChanged,
        wetlandsModel.GetInundatedWetlandsByFlowAtGauge(
          gaugeName, 
          flowAtGauge
         )
      );
    }

    public void WaggaGaugeThresholdSelected(string flowThreshold) {
      raiseEvent(
        CommenceToFillEventType.WaggaGaugeThresholdWetlandsChanged,
        wetlandsModel.GetInundatedWetlandsByWaggaFlowThreshold(
          flowThreshold
         )
      );
    }
    
    #region Wetlands Model

    private IWetlandsModel wetlandsModel;

    public void setWetlandsModel(IWetlandsModel model) {
      this.wetlandsModel = model;
    }

    public string[] GetGaugeNames() {
      return wetlandsModel.getGaugeNames();
    }

    public string[] GetWaggaFlowThresholds() {
      return wetlandsModel.getWaggaFlowThresholds();
    }

    #endregion

    #region CARMS Model

    private ICARMScenarioModel carmScenarioModel;
    
    public void setCARMScenarioModel(ICARMScenarioModel model) {
      this.carmScenarioModel = model;
      this.carmScenarioModel.ModelChanged += new EventHandler<CARMScenarioModelEventArgs>(this.HandleModelChangedEvent);
    }

    public string[] GetScenarios() {
      return carmScenarioModel.GetScenarios();
    }

    #endregion

    private void raiseEvent(CommenceToFillEventType type, DataTable inundatedWetlands) {
      CommenceToFillPresenterChanged(
        this, 
        new CommenceToFillEventArgs(
          type, 
          inundatedWetlands
        )
      );
    }

    private void raiseEvent(string[] scenarioNames) {
      CommenceToFillPresenterChanged(
        this,
        new CommenceToFillEventArgs(
          scenarioNames
        )
      );
    }

    private void HandleModelChangedEvent(object sender, CARMScenarioModelEventArgs args) {
      raiseEvent(args.ScenarioList);
    }
  }
}
