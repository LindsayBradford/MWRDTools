using System;
using System.Data;
using System.Windows.Forms;

using MWRDTools.Presenter;
using MWRDTools.View;

public partial class CommenceToFillForm : Form, ICommenceToFillView
{
    private ICommenceToFillPresenter presenter;

    private DataTable WaggaWetlands, GaugeWetlands, CARMWetlands;

    public CommenceToFillForm() {
      InitializeComponent();

      WaggaListView.ListViewItemSorter = new ListViewColumnSorter();
      GaugeListView.ListViewItemSorter = new ListViewColumnSorter();
      CARMListView.ListViewItemSorter = new ListViewColumnSorter();
    }

    public void setPresenter(ICommenceToFillPresenter presenter) {
      this.presenter = presenter;
      this.presenter.InundatedWetlandsChanged += 
        new EventHandler<InundatedWetlandsEventArgs>(this.HandleInundatedWetlandsEvent);
    }

    public void HandleInundatedWetlandsEvent(object sender, InundatedWetlandsEventArgs args) {
      switch (args.Type) {
        case InundatedWetlandsType.WaggaGaugeThreshold: 
            this.WaggaWetlands = args.Wetlands;
            ViewUtilities.DataTableToListView(
              args.Wetlands,
              WaggaListView,
              "OID"
            );
            break;
        case InundatedWetlandsType.FlowAtGauge: 
          this.GaugeWetlands = args.Wetlands;
          ViewUtilities.DataTableToListView(
            args.Wetlands,
            GaugeListView,
            "OID"
          );
          break;
        case InundatedWetlandsType.CARMScenario: 
          this.CARMWetlands = args.Wetlands;
          ViewUtilities.DataTableToListView(
            args.Wetlands,
            CARMListView,
            "OID"
          );
          break;
      }
    }

    private void FillCombos() {
      fillComboBox(
        cboWagga, 
        presenter.GetWaggaFlowThresholds()
      );

      fillComboBox(
        cboGauge,
        presenter.GetGaugeNames()
      ); 

      fillComboBox(
        cboCARMScenario,
        presenter.GetScenarios()
      ); 
    }

    private void fillComboBox(ComboBox box, string[] values) {
      box.Items.Clear();
      box.Items.AddRange(values);
    }
  
    private void RunWaggaFlowQuery() {
      err.Clear();
      if (ValidateFlowQueryParameters()) {
        presenter.WaggaGaugeThresholdSelected(
          cboWagga.SelectedItem.ToString()
        );
      }
    }

    private bool ValidateFlowQueryParameters() {
      if (cboWagga.Text == "") {
        err.SetError(cboWagga, "A flow value must be selected");
        return false;
      }
      return true;
    }

    private void RunGaugeQuery() {
      err.Clear();
      if (ValidateGaugeQueryParameters()) {
        presenter.GaugeAndFlowSelected(
          cboGauge.Text,
          Convert.ToDouble(txtFlow.Text)
        );
      }
    }

    public bool ValidateGaugeQueryParameters() {
      if (cboGauge.Text == "") {
        err.SetError(cboGauge, "A gauge must be selected");
        return false;
      }
      try {
        Convert.ToDouble(txtFlow.Text);
      }
      catch {
        err.SetError(txtFlow, "A valid numeric value is required");
        return false;
      }
      return true;
    }

     private void HighlightFeatures(ListView view) {
      presenter.HighlightWetlands(
        ViewUtilities.GetSelectedFeatures(view)
      );
    }

    private void ZoomToSelectedFeatures(ListView view) {
      presenter.ZoomToWetlands(
        ViewUtilities.GetSelectedFeatures(view)
      );
    }

    private void FlashFeatures(ListView view) {
      presenter.FlashWetlands(
        ViewUtilities.GetSelectedFeatures(view)
      );
    }

    private void ExportFeatures(ListView view, DataTable features) {

      int[] selectedFeatures = ViewUtilities.GetSelectedFeatures(view);
      if (selectedFeatures.Length == 0) return;
      
      dlg.OverwritePrompt = true;
      dlg.Filter = "*.csv|*.csv";
      if (dlg.ShowDialog() != DialogResult.Cancel) {
        presenter.ExportFeatures(
          dlg.FileName, 
          features, 
          selectedFeatures
        );
      }
    }

    private void CommenceToFillForm_VisibileChanged(object sender, EventArgs e) {
      if (this.Visible) {
        FillCombos();
      }
    }

    private void CommenceToFillForm_FormClosing(object sender, FormClosingEventArgs e) {
      e.Cancel = true;
      this.Hide();
    }

    private void tab_SelectedIndexChanged(object sender, EventArgs e) {
      switch (tab.SelectedIndex) {
        case 0:
          this.AcceptButton = this.btnFindWaggaFlow; break;
        case 1:
          this.AcceptButton = this.btnFindGaugeFlow; break;
        case 2:
          this.AcceptButton = this.btnFindCarmScenario; break;
      }
    }

    private void btnWaggaFind_Click(object sender, EventArgs e) {
      RunWaggaFlowQuery();
    }

    private void WaggaListView_KeyDown(object sender, KeyEventArgs e) {
      if ((e.KeyCode == Keys.A) && e.Control) {
        ViewUtilities.SelectAllFeatures(WaggaListView);
      }
    }

    private void WaggaListView_ColumnClick(object sender, ColumnClickEventArgs e) {
      ViewUtilities.ProcessColumnClickEvent(WaggaListView, e);
    }

    private void btnWaggaZoom_Click(object sender, EventArgs e) {
      ZoomToSelectedFeatures(WaggaListView);
    }

    private void btnWaggaHighlight_Click(object sender, EventArgs e) {
      HighlightFeatures(WaggaListView);
    }

    private void btnWaggaFlash_Click(object sender, EventArgs e) {
      FlashFeatures(WaggaListView);
    }

    private void btnWaggaExport_Click(object sender, EventArgs e) {
      ExportFeatures(WaggaListView, WaggaWetlands);
    }

    private void GaugeListView_KeyDown(object sender, KeyEventArgs e) {
      if ((e.KeyCode == Keys.A) && e.Control) {
        ViewUtilities.SelectAllFeatures(GaugeListView);
      }
    }

    private void GaugeListView_ColumnClick(object sender, ColumnClickEventArgs e) {
      ViewUtilities.ProcessColumnClickEvent(GaugeListView, e);
    }

    private void btnFindGauge_Click(object sender, EventArgs e) {
      RunGaugeQuery();
    }

    private void btnSelectGauge_Click(object sender, EventArgs e) {
      HighlightFeatures(GaugeListView);
    }

    private void btnZoomGauge_Click(object sender, EventArgs e) {
      ZoomToSelectedFeatures(GaugeListView);
    }

    private void btnFlashGauge_Click(object sender, EventArgs e) {
      FlashFeatures(GaugeListView);
    }

    private void btnExportGauge_Click(object sender, EventArgs e) {
      ExportFeatures(GaugeListView, GaugeWetlands);
    }

    private void txtFlow_KeyDown(object sender, KeyEventArgs e) {
      if (e.KeyCode == Keys.Enter) {
        RunGaugeQuery();
      }
    }

    private void btnFindCarmScenario_Click(object sender, EventArgs e) {
      presenter.CARMScenarioSelected(
        cboCARMScenario.SelectedItem as string
      );
    }

    private void CARMListView_KeyDown(object sender, KeyEventArgs e) {
      if ((e.KeyCode == Keys.A) && e.Control) {
        ViewUtilities.SelectAllFeatures(CARMListView);
      }
    }

    private void CARMListView_ColumnClick(object sender, ColumnClickEventArgs e) {
      ViewUtilities.ProcessColumnClickEvent(CARMListView, e);
    }

    private void btnHilightCarmScenario_Click(object sender, EventArgs e) {
      HighlightFeatures(CARMListView);
    }

    private void btnZoomCarm_Click(object sender, EventArgs e) {
      ZoomToSelectedFeatures(CARMListView);
    }

    private void btnFlashCarm_Click(object sender, EventArgs e) {
      FlashFeatures(CARMListView);
    }

    private void btnExportCarm_Click(object sender, EventArgs e) {
      ExportFeatures(CARMListView, CARMWetlands);
    }

    private void mnuAbout_Click(object sender, EventArgs e) {
      AboutDialog frm = new AboutDialog();
      frm.ShowDialog();
    }

}