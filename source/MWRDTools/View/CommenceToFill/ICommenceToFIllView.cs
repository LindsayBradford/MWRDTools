using System.Data;

using MWRDTools.Presenter;

namespace MWRDTools.View {
  public interface ICommenceToFillView {
    void setPresenter(ICommenceToFillPresenter presenter);

    void SetWaggaGaugeThresholdInundatedWetlands(DataTable wetlands);
    void SetCARMScenarioInundatedWetlands(DataTable wetlands);
    void SetFlowAtGaugeInundatedWetlands(DataTable wetlands);
  }
}
