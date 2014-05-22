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

    private const string SCIENTIFIC_NAME = "ScientificName";

    private SpeciesFIlterForm speciesFilterForm = new SpeciesFIlterForm();
    private DatePickerForm datePickerForm;
    private bool wetlandsLoaded = false;

    private DataTable WetlandsForSpeciesTable, SpeciesForWetlandsTable;

    private IThreatenedSpeciesPresenter presenter;

    public ThreatenedSpeciesForm(IApplication pApplication) {
      InitializeComponent();

      SpeciesListView.ListViewItemSorter = new ListViewColumnSorter();
      FilteredWetlandsListView.ListViewItemSorter = new ListViewColumnSorter();

      AllWetlandsListView.ListViewItemSorter = new ListViewColumnSorter();
      FilteredSpeciesListView.ListViewItemSorter = new ListViewColumnSorter();

      speciesFilterForm.Load += new EventHandler(_frmFilter_Load);
      speciesFilterForm.FormClosed += new FormClosedEventHandler(_frmFilter_FormClosed);

      datePickerForm = new DatePickerForm();
      datePickerForm.FormClosed += new FormClosedEventHandler(_frmDatePicker_FormClosed);

      //_spDataAccess.ProgressEvent += new SpatialDataAccess.ProgressEventHandler(_spDataAccess_ProgressEvent);
    }

    public void setPresenter(IThreatenedSpeciesPresenter presenter) {
      this.presenter = presenter;
    }

    //void _spDataAccess_ProgressEvent(object sender, ProgressEventArgs e) {
    //  switch (e.Activity) {
    //    case ProgressEventEnums.eProgress.start: {
    //      pb.Minimum = 0;
    //      pb.Maximum = e.Step;
    //      break;
    //    }
    //    case ProgressEventEnums.eProgress.update: {
    //      pb.Value = e.Step;
    //      break;
    //    }
    //    case ProgressEventEnums.eProgress.finish: {
    //      pb.Value = pb.Maximum;
    //      pb.Value = pb.Minimum;
    //      break;
    //    }
    //  }
    //}

    void IThreatenedSpeciesView.ApplySpeciesFilter(DataTable species) {
      ViewUtilities.DataTableToListView(
        species,
        SpeciesListView,
        SCIENTIFIC_NAME
      );
    }

    void IThreatenedSpeciesView.SetWetlands(DataTable wetlands) {
      ViewUtilities.DataTableToListView(
        wetlands,
        AllWetlandsListView,
        "MBCMA_wetland"
      );
    }

    void IThreatenedSpeciesView.ShowWetlandsForSpecies(DataTable wetlands) {
      this.WetlandsForSpeciesTable = wetlands;
      ViewUtilities.DataTableToListView(
        wetlands,
        FilteredWetlandsListView,
        "OID"
      );
    }

    void IThreatenedSpeciesView.ShowSpeciesForWetlands(DataTable species) {
      this.SpeciesForWetlandsTable = species;
      ViewUtilities.DataTableToListView(
        species,
        FilteredSpeciesListView,
        "OID"
      );
    }

    private void ShowFilter() {
      speciesFilterForm.ShowDialog();
    }

    private void ThreatenedSpeciesForm_Load(object sender, EventArgs e) {
      //if (!SetFeatureWorkspace()) {
      //  MessageBox.Show(
      //    String.Format(
      //      "Unable to open the data source. The application was looking for a layer called {0}.", 
      //      Constants.LayerName.WetLands
      //    ), 
      //    Application.ProductName
      //  );
      //  this.Dispose();
      //}
    }

    private void ThreatenedSpeciesForm_FormClosing(object sender, FormClosingEventArgs e) {
      e.Cancel = true;
      this.Hide();
    }

    private void SpeciesListView_ColumnClick(object sender, ColumnClickEventArgs e) {
      ViewUtilities.ProcessColumnClickEvent(SpeciesListView, e);
    }

    private void btnFind_Click(object sender, EventArgs e) {

      if (SpeciesListView.SelectedItems.Count == 0) {
        return;
      }
      
      this.Cursor = Cursors.WaitCursor;

      double buffer = 0.0;
      if (!(cboBuffer.Text == "None") && !(cboBuffer.Text == "")) {
        buffer = Convert.ToDouble(cboBuffer.Text);
      }

      presenter.FindWetlandsBySpecies(
        SpeciesListView.SelectedItems[0].Tag.ToString(), 
        buffer,
        convertTextBoxToDateTime(txtAfterDate),
        convertTextBoxToDateTime(txtBeforeDate)
      );

      this.Cursor = Cursors.Default;
    }

    private DateTime? convertTextBoxToDateTime(ToolStripTextBox textbox) {
      DateTime? dt = null;

      try {
        dt = new DateTime?(
          Convert.ToDateTime(textbox.Text)
        );
      } catch { }

      return dt;
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

    void _frmFilter_Load(object sender, EventArgs e) {
      if (speciesFilterForm.FilterSettingsLoaded) {
        return;
      }

      speciesFilterForm.SetSpeciesClasses(
        presenter.GetSpeciesClasses()
      );

      speciesFilterForm.SetSpeciesStatuses(
        presenter.GetSpeciesStatuses()
      );
    }

    void _frmFilter_FormClosed(object sender, FormClosedEventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      if (speciesFilterForm.DialogResult != DialogResult.Cancel) {
        presenter.SpeciesFilterApplied(
           speciesFilterForm.GetSelectedSpeciesClasses(),
           speciesFilterForm.GetSelectedSpeciesStatuses()
        );      
      }

      this.Cursor = Cursors.Default;
    }

    private void btnAfterDate_Click(object sender, EventArgs e) {
      datePickerForm.DateType = "After";
      datePickerForm.ShowDialog();
    }

    void _frmDatePicker_FormClosed(object sender, FormClosedEventArgs e)
    {
        if(datePickerForm.DialogResult != DialogResult.Cancel)
        {
            switch (datePickerForm.DateType)
            {
                case "After":
                    {
                        txtAfterDate.Text = datePickerForm.Result.ToShortDateString();
                        break;
                    }
                case "Before":
                    {
                        txtBeforeDate.Text = datePickerForm.Result.ToShortDateString();
                        break;
                    }
                case "After1":
                    {
                        txtAfter1.Text = datePickerForm.Result.ToShortDateString();
                        break;
                    }
                case "Before1":
                    {
                        txtBefore1.Text = datePickerForm.Result.ToShortDateString();
                        break;
                    }
            }
        }
    }

    private void btnBeforeDate_Click(object sender, EventArgs e) {
      datePickerForm.DateType = "Before";
      datePickerForm.ShowDialog();
    }
        
    private void mnuExit_Click(object sender, EventArgs e) {
      this.Close();
    }

    private void AllWetlandsListView_ColumnClick(object sender, ColumnClickEventArgs e) {
      ViewUtilities.ProcessColumnClickEvent(AllWetlandsListView, e);
    }

    private void ThreatenedSpeciesTab_SelectedIndexChanged(object sender, EventArgs e) {
      if (ThreatenedSpeciesTab.SelectedIndex == 1 && wetlandsLoaded == false) {
        this.Cursor = Cursors.WaitCursor;
 
        presenter.SpeciesByWetlandsTabSelected();
        wetlandsLoaded = true;
  
        this.Cursor = Cursors.Default;
      }
    }

    private void FindSpeciesByWetland_Click(object sender, EventArgs e) {
      if (AllWetlandsListView.SelectedItems.Count == 0) {
        return;
      }

      this.Cursor = Cursors.WaitCursor;

      double buffer = 0.0;
      if (!(cboBuffer1.Text == "None") && !(cboBuffer1.Text == "")) {
        buffer = Convert.ToDouble(cboBuffer1.Text);
      }

      presenter.FindSpeciesByWetlands(
        AllWetlandsListView.SelectedItems[0].Tag.ToString(),
        speciesFilterForm.GetSelectedSpeciesClasses(),
        speciesFilterForm.GetSelectedSpeciesStatuses(),
        buffer,
        convertTextBoxToDateTime(txtAfter1),
        convertTextBoxToDateTime(txtBefore1)
      );

      this.Cursor = Cursors.Default;
    }

    private void FilteredSpeciesListView_ColumnClick(object sender, ColumnClickEventArgs e) {
      ViewUtilities.ProcessColumnClickEvent(FilteredSpeciesListView, e);
    }

    private void btnAfter1_Click(object sender, EventArgs e) {
      datePickerForm.DateType = "After1";
      datePickerForm.ShowDialog();
    }

    private void btnBefore1_Click(object sender, EventArgs e) {
      datePickerForm.DateType = "Before1";
      datePickerForm.ShowDialog();
    }

    private void btnHighlight1_Click(object sender, EventArgs e) {
      presenter.HighlightSpecies(
        ViewUtilities.GetSelectedFeatures(FilteredSpeciesListView)
      );
    }

    private void btnZoom1_Click(object sender, EventArgs e) {
      presenter.ZoomToSpecies(
        ViewUtilities.GetSelectedFeatures(FilteredSpeciesListView)
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
      ExportFeatures(
        FilteredWetlandsListView, 
        WetlandsForSpeciesTable
      );
    }

    private void btnExport1_Click(object sender, EventArgs e) {
      ExportFeatures(
        FilteredSpeciesListView,
        SpeciesForWetlandsTable
      );
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