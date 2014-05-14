using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;

using MWRDTools.Presenter;
using MWRDTools.View;

public partial class CommenceToFillForm : Form, ICommenceToFillView
{
    private IApplication _application;
    private IMap _map;
    private IFeatureWorkspace _featureWorkspace;
    private IFeatureLayer _featureLayer;

    private ICommenceToFillPresenter presenter;

    public CommenceToFillForm() {
        InitializeComponent();

        WaggaListView.ListViewItemSorter = new ListViewColumnSorter();
        GaugeListView.ListViewItemSorter = new ListViewColumnSorter();
        CARMListView.ListViewItemSorter = new ListViewColumnSorter();
    }

    public IApplication Application {
      get { return this._application; }
      set { this._application = value; initialseApplication(); }
    }

    private void initialseApplication() {
      _map = (_application.Document as IMxDocument).FocusMap;
    }

    private bool SetFeatureWorkspace() {
      _featureWorkspace = Common.GetFeatureWorkspace(Constants.LayerName.WetLands, _map, ref _featureLayer);
      return (_featureWorkspace != null);
    }

    void ICommenceToFillView.setPresenter(ICommenceToFillPresenter presenter) {
      this.presenter = presenter;
    }

    void ICommenceToFillView.SetWaggaGaugeThresholdInundatedWetlands(DataTable wetlands) {
      fillListViewFromDataTable(
        WaggaListView,
        wetlands
      );
    }

    void ICommenceToFillView.SetFlowAtGaugeInundatedWetlands(DataTable wetlands) {
      fillListViewFromDataTable(
        GaugeListView,
        wetlands
      );
    }

    void ICommenceToFillView.SetCARMScenarioInundatedWetlands(DataTable wetlands) {
      fillListViewFromDataTable(
        CARMListView,
        wetlands
      );
    }

    private void fillListViewFromDataTable(ListView view, DataTable table) {
      view.Columns.Clear();
      view.Items.Clear();

      foreach (DataColumn column in table.Columns)  {
        view.Columns.Add(
          column.ColumnName
        );
      }

      foreach (DataRow row in table.Rows) {
        ListViewItem item = new ListViewItem(row[Constants.OID].ToString());

        foreach (DataColumn column in table.Columns) {
          if (column.ColumnName.Equals(Constants.OID)) {
            continue;
          }
          item.SubItems.Add(
            row[column.ColumnName].ToString()
          );
        }

        item.Tag = row[Constants.OID];

        view.Items.Add(item);
      }

      view.AutoResizeColumns(
        ColumnHeaderAutoResizeStyle.HeaderSize
      );
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

    private void SelectAllFeatures(ListView view) {
      for (int i = 0; i < view.Items.Count; i++) {
        view.Items[i].Selected = true;
      }
    }

    private int[] getSelectedFeatures(ListView view) {
      if (view.SelectedItems == null || view.SelectedItems.Count == 0) {
        return null;
      }
      int[] featureTags = new int[view.SelectedItems.Count];
      for (int i = 0; i < view.SelectedItems.Count; i++) {
        featureTags[i] = Convert.ToInt32(view.SelectedItems[i].Tag.ToString());
      }
      return featureTags;
    }

    private void HighlightFeatures(ListView view) {
      int[] features = getSelectedFeatures(view);
      if (features != null) {
        Common.HighlightFeatures(features, _featureLayer, _map);
      }
    }

    private void ZoomToSelectedFeatures(ListView view) {
      int[] features = getSelectedFeatures(view);
      if (features != null) {
        Common.ZoomToFeatures(features, _featureLayer, _map);
      }
    }

    private void FlashFeatures(ListView view) {
      int[] features = getSelectedFeatures(view);
      if (features != null) {
        Common.FlashFeatures(features, _featureLayer, _map);
      }
    }

    private void ExportFeatures(ListView view) {
      dlg.OverwritePrompt = true;
      dlg.Filter = "*.csv|*.csv";
      if (dlg.ShowDialog() != DialogResult.Cancel) {
        Common.ExportListView(dlg.FileName, view);
      }
    }

    private void ProcessColumnClickEvent(ListView view, ColumnClickEventArgs e) {
      ListViewColumnSorter sorter = view.ListViewItemSorter as ListViewColumnSorter;
      if (e.Column == sorter.ColunmToSort) {
        if (sorter.SortOrder == "ascending") {
          sorter.SortOrder = "descending";
        } else {
          sorter.SortOrder = "ascending";
        }
      } else {
        sorter.ColunmToSort = e.Column;
        sorter.SortOrder = "ascending";
      }
      view.Sort();
    }

    private void frmCommenceToFill_Load(object sender, EventArgs e) {
      try {
        if (!SetFeatureWorkspace()) {
          MessageBox.Show(
            String.Format(
              "Unable to open the data source. The application was looking for a layer called {0}.",
              Constants.LayerName.WetLands
            ),
            System.Windows.Forms.Application.ProductName
          );
          this.Dispose();
        }
        FillCombos();
      } catch (Exception ex) {
        MessageBox.Show(
          ex.Message,
          System.Windows.Forms.Application.ProductName
        );
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
        SelectAllFeatures(WaggaListView);
      }
    }

    private void WaggaListView_ColumnClick(object sender, ColumnClickEventArgs e) {
      ProcessColumnClickEvent(WaggaListView, e);
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
      ExportFeatures(WaggaListView);
    }

    private void GaugeListView_KeyDown(object sender, KeyEventArgs e) {
      if ((e.KeyCode == Keys.A) && e.Control) {
        SelectAllFeatures(GaugeListView);
      }
    }

    private void GaugeListView_ColumnClick(object sender, ColumnClickEventArgs e) {
      ProcessColumnClickEvent(GaugeListView, e);
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
      ExportFeatures(GaugeListView);
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
        SelectAllFeatures(CARMListView);
      }
    }

    private void CARMListView_ColumnClick(object sender, ColumnClickEventArgs e) {
      ProcessColumnClickEvent(CARMListView, e);
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
      ExportFeatures(CARMListView);
    }

    private void mnuAbout_Click(object sender, EventArgs e) {
      frmAboutBox frm = new frmAboutBox();
      frm.ShowDialog();
    }

}