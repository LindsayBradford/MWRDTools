using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;

using MWRDTools.View;
using MWRDTools.Presenter;

public partial class ThreatenedSpeciesForm : Form, IThreatenedSpeciesView
{
    private IApplication _application;
    private IMap _map;
    private IFeatureWorkspace _featureWorkspace;
    private IFeatureLayer _threatenedSpeciesFL;
    private IFeatureLayer _wetlandsFL;

  SpatialDataAccess _spDataAccess;
    frmFilter _frmFilter;
    frmDatePicker _frmDatePicker;
    private string _speciesWhereClause;
    private bool _wetlandsLoaded = false;

    private IThreatenedSpeciesPresenter presenter;

    public ThreatenedSpeciesForm(IApplication pApplication) {
      InitializeComponent();

      _application = pApplication;
      IMxDocument pMxDocument = _application.Document as IMxDocument;
      _map = pMxDocument.FocusMap;

      SpeciesListView.ListViewItemSorter = new ListViewColumnSorter();
      FilteredWetlandsListView.ListViewItemSorter = new ListViewColumnSorter();

      AllWetlandsListView.ListViewItemSorter = new ListViewColumnSorter();
      FilteredSpeciesListView.ListViewItemSorter = new ListViewColumnSorter();

       _spDataAccess = new SpatialDataAccess();
      _spDataAccess.ProgressEvent += new SpatialDataAccess.ProgressEventHandler(_spDataAccess_ProgressEvent);
      _frmDatePicker = new frmDatePicker();
      _frmDatePicker.FormClosed += new FormClosedEventHandler(_frmDatePicker_FormClosed);
    }

    public void setPresenter(IThreatenedSpeciesPresenter presenter) {
      this.presenter = presenter;
    }

    void _spDataAccess_ProgressEvent(object sender, ProgressEventArgs e)
    {
        switch (e.Activity)
        {
            case ProgressEventEnums.eProgress.start:
                {
                    pb.Minimum = 0;
                    pb.Maximum = e.Step;
                    break;
                }
            case ProgressEventEnums.eProgress.update:
                {
                    pb.Value = e.Step;
                    break;
                }
            case ProgressEventEnums.eProgress.finish:
                {
                    pb.Value = pb.Maximum;
                    pb.Value = pb.Minimum;
                    break;
                }
        }
    }

    private bool SetFeatureWorkspace()
    {
        _featureWorkspace = Common.GetFeatureWorkspace(Constants.LayerName.WetLands, _map, ref _wetlandsFL);
        _threatenedSpeciesFL = Common.GetFeatureLayer(_map, Constants.LayerName.ThreatenedSpecies);
        return (_featureWorkspace != null);
    }

    private void LoadSpecies()
    {
        SpeciesListView.Items.Clear();
        ICursor pCursor = DataAccess.GetSpeciesNames(_featureWorkspace, _speciesWhereClause);
        IRow pRow = pCursor.NextRow();
        while (pRow != null)
        {
            AddSpeciesListItem(pRow, SpeciesListView);
            pRow = pCursor.NextRow();
        }
    }

    private void AddSpeciesListItem(IRow pRow, ListView lv)
    {
        ListViewItem li = new ListViewItem();
        li.Tag = Common.GetValueAsString(pRow, ColumnNames.ScientificName);
        li.Text = Common.GetValueAsString(pRow, ColumnNames.CommonName);
        li.SubItems.Add(Common.GetValueAsString(pRow, ColumnNames.ClassName));
        li.SubItems.Add(Common.GetValueAsString(pRow, ColumnNames.FamilyName));
        li.SubItems.Add(Common.GetValueAsString(pRow, ColumnNames.ScientificName));
        li.SubItems.Add(Common.GetValueAsString(pRow, ColumnNames.LegalStatus));
        li.SubItems.Add(Common.GetValueAsString(pRow, ColumnNames.SpeciesCode));
        lv.Items.Add(li);
    }

    void IThreatenedSpeciesView.SetWetlands(DataTable wetlands) {
      ViewUtilities.DataTableToListView(
        wetlands,
        AllWetlandsListView
      );
    }

    private void AddWetlandsListItem(IRow pRow, ListView lv)
    {
        //todo: this screen supplies a cut-down number of wetland columns. cater for this. 
        ListViewItem li = new ListViewItem();
        string id = Common.GetValueAsString(pRow, "MBCMA_wetland");
        li.Tag = id;
        li.Text = Common.GetValueAsString(pRow, "Wetland_Name");
        li.SubItems.Add(Common.GetValueAsString(pRow, "Complex_name"));
        li.SubItems.Add(Common.GetValueAsString(pRow, "Flow"));
        li.SubItems.Add(id);
        lv.Items.Add(li);
    }

    private void ShowFilter()
    {
        IEnumerator pEnumerator = DataAccess.GetClassNames(_featureWorkspace);
        _frmFilter = new frmFilter(_speciesWhereClause, pEnumerator);
        _frmFilter.FormClosed += new FormClosedEventHandler(_frmFilter_FormClosed);
        _frmFilter.ShowDialog();

    }

    private void ThreatenedSpeciesForm_Load(object sender, EventArgs e) {
      if (!SetFeatureWorkspace()) {
        MessageBox.Show(
          String.Format(
            "Unable to open the data source. The application was looking for a layer called {0}.", 
            Constants.LayerName.WetLands
          ), 
          Application.ProductName
        );
        this.Dispose();
      }
    }

    private void ThreatenedSpeciesForm_FormClosing(object sender, FormClosingEventArgs e) {
      e.Cancel = true;
      this.Hide();
    }

    private void SpeciesListView_ColumnClick(object sender, ColumnClickEventArgs e) {
      ViewUtilities.ProcessColumnClickEvent(SpeciesListView, e);
    }

    private void btnFind_Click(object sender, EventArgs e)
    {
        try
        {
            this.Cursor = Cursors.WaitCursor;

            FilteredWetlandsListView.Items.Clear();
            if (SpeciesListView.SelectedItems != null)
            {
                if (SpeciesListView.SelectedItems.Count > 0)
                {
                    double buffer = 0.0;
                    if (!(cboBuffer.Text == "None") && !(cboBuffer.Text == ""))
                    {
                        buffer = Convert.ToDouble(cboBuffer.Text);
                    }
                    string afterDate = txtAfterDate.Text;
                    string beforeDate = txtBeforeDate.Text;
                    ArrayList wetlands = _spDataAccess.GetWetlandsBySpecies(_featureWorkspace, _wetlandsFL, SpeciesListView.SelectedItems[0].Tag.ToString(), buffer, afterDate, beforeDate);
                    Common.ArrayListToListView(wetlands, ref FilteredWetlandsListView, "");
                }
            }
            this.Cursor = Cursors.Default;
        }
        catch (Exception ex)
        {
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnSelect_Click(object sender, EventArgs e) {
      presenter.HighlightWetlands(
        ViewUtilities.GetSelectedFeatures(FilteredWetlandsListView)
      );
    }

    private void btnZoom_Click(object sender, EventArgs e) {
      presenter.ZoomToWetlands(
        ViewUtilities.GetSelectedFeatures(FilteredWetlandsListView)
      );
    }

    private void FilteredWetlandsListView_ColumnClick(object sender, ColumnClickEventArgs e) {
      ViewUtilities.ProcessColumnClickEvent(FilteredWetlandsListView, e);
    }

    private void btnFlash_Click(object sender, EventArgs e) {
      presenter.FlashWetlands(
        ViewUtilities.GetSelectedFeatures(FilteredWetlandsListView)
      );
    }

    private void mnuFilter_Click(object sender, EventArgs e) {
      ShowFilter();
    }

    void _frmFilter_FormClosed(object sender, FormClosedEventArgs e)
    {
        try
        {
            this.Cursor = Cursors.WaitCursor;
            if (_frmFilter.DialogResult != DialogResult.Cancel)
            {
                _speciesWhereClause = _frmFilter.WhereClause;
                LoadSpecies();
            }
            _frmFilter.FormClosed -= new FormClosedEventHandler(_frmFilter_FormClosed);
            lblSpeciesFilter.Text = _speciesWhereClause;
            this.Cursor = Cursors.Default;
        }
        catch (Exception ex)
        {
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnAfterDate_Click(object sender, EventArgs e) {
      _frmDatePicker.DateType = "After";
      _frmDatePicker.ShowDialog();
    }

    void _frmDatePicker_FormClosed(object sender, FormClosedEventArgs e)
    {
        if(_frmDatePicker.DialogResult != DialogResult.Cancel)
        {
            switch (_frmDatePicker.DateType)
            {
                case "After":
                    {
                        txtAfterDate.Text = _frmDatePicker.Result.ToShortDateString();
                        break;
                    }
                case "Before":
                    {
                        txtBeforeDate.Text = _frmDatePicker.Result.ToShortDateString();
                        break;
                    }
                case "After1":
                    {
                        txtAfter1.Text = _frmDatePicker.Result.ToShortDateString();
                        break;
                    }
                case "Before1":
                    {
                        txtBefore1.Text = _frmDatePicker.Result.ToShortDateString();
                        break;
                    }
            }
        }
    }

    private void btnBeforeDate_Click(object sender, EventArgs e) {
      _frmDatePicker.DateType = "Before";
      _frmDatePicker.ShowDialog();
    }
        
    private void mnuExit_Click(object sender, EventArgs e) {
      this.Close();
    }

    private void AllWetlandsListView_ColumnClick(object sender, ColumnClickEventArgs e) {
      ViewUtilities.ProcessColumnClickEvent(AllWetlandsListView, e);
    }

    private void ThreatenedSpeciesTab_SelectedIndexChanged(object sender, EventArgs e) {
      if (ThreatenedSpeciesTab.SelectedIndex == 1 && _wetlandsLoaded == false) {
        this.Cursor = Cursors.WaitCursor;
 
        presenter.SpeciesByWetlandsTabSelected();
        _wetlandsLoaded = true;
  
        this.Cursor = Cursors.Default;
      }
    }

    private void btnFind1_Click(object sender, EventArgs e)
    {
        try
        {
            this.Cursor = Cursors.WaitCursor;
            FilteredSpeciesListView.Items.Clear();
            if (AllWetlandsListView.SelectedItems != null)
            {
                if (AllWetlandsListView.SelectedItems.Count > 0)
                {
                    double buffer = 0.0;
                    if (!(cboBuffer1.Text == "None") && !(cboBuffer1.Text == ""))
                    {
                        buffer = Convert.ToDouble(cboBuffer1.Text);
                    }
                    IFeatureCursor species = _spDataAccess.GetSpeciesByWetland(_featureWorkspace, _wetlandsFL, AllWetlandsListView.SelectedItems[0].Tag.ToString(), buffer, txtAfter1.Text, txtBefore1.Text, _speciesWhereClause);
                    Common.FeatureCursorToListView(species, ref FilteredSpeciesListView, "");
                }
            }
            this.Cursor = Cursors.Default;

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void FilteredSpeciesListView_ColumnClick(object sender, ColumnClickEventArgs e) {
      ViewUtilities.ProcessColumnClickEvent(FilteredSpeciesListView, e);
    }

    private void btnAfter1_Click(object sender, EventArgs e) {
      _frmDatePicker.DateType = "After1";
      _frmDatePicker.ShowDialog();
    }

    private void btnBefore1_Click(object sender, EventArgs e) {
      _frmDatePicker.DateType = "Before1";
      _frmDatePicker.ShowDialog();
    }

    private void btnHighlight1_Click(object sender, EventArgs e) {
      presenter.HighlightSpecies(
        ViewUtilities.GetSelectedFeatures(FilteredSpeciesListView)
      );
    }

    private void btnZoom1_Click(object sender, EventArgs e) {
      presenter.ZoomToSpecies(
        ViewUtilities.GetSelectedFeatures(FilteredWetlandsListView)
      );
    }

    private void btnFlash1_Click(object sender, EventArgs e) {
      presenter.FlashSpecies(
        ViewUtilities.GetSelectedFeatures(FilteredSpeciesListView)
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

    private void btnExport_Click(object sender, EventArgs e) {
      // ExportFeatures(FilteredWetlandsListView, FilteredWetlandsTable);

      try {
        dlg.OverwritePrompt = true;
        dlg.Filter = "*.csv|*.csv";
        if (dlg.ShowDialog() != DialogResult.Cancel) {
          Common.ExportListView(dlg.FileName, FilteredWetlandsListView);
        }
      } catch (Exception ex) {
        MessageBox.Show(ex.Message, Application.ProductName);
      }
    }

    private void btnExport1_Click(object sender, EventArgs e)
    {
      // ExportFeatures(FilteredSpeciesListView, FilteredSpeciesTable);
      try
        {
            dlg.OverwritePrompt = true;
            dlg.Filter = "*.csv|*.csv";
            if (dlg.ShowDialog() != DialogResult.Cancel)
            {
                Common.ExportListView(dlg.FileName, FilteredSpeciesListView);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void FilteredWetlandsListView_KeyDown(object sender, KeyEventArgs e) {
      if ((e.KeyCode == Keys.A) && e.Control) {
        ViewUtilities.SelectAllFeatures(FilteredWetlandsListView);
      }
    }

    private void FilteredSpeciesListView_KeyDown(object sender, KeyEventArgs e) {
      if ((e.KeyCode == Keys.A) && e.Control) {
        ViewUtilities.SelectAllFeatures(FilteredSpeciesListView);
      }
    }

    private void mnuAbout_Click(object sender, EventArgs e) {
      AboutDialog frm = new AboutDialog();
      frm.ShowDialog();
    }
}

static class ColumnNames
{
  public const string ScientificName = "ScientificName";
  public const string CommonName = "CommonName";
  public const string ClassName = "ClassName";
  public const string FamilyName = "FamilyName";
  public const string LegalStatus = "NSWStatus";
  public const string SpeciesCode = "SpeciesCode";
}
