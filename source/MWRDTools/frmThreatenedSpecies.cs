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

public partial class frmThreatenedSpecies : Form
{
    private IApplication _application;
    private IMap _map;
    private IFeatureWorkspace _featureWorkspace;
    private IFeatureLayer _threatenedSpeciesFL;
    private IFeatureLayer _wetlandsFL;
    private ListViewColumnSorter _lvColumnSorter;
    private ListViewColumnSorter _lvColumnSorter1;
    private ListViewColumnSorter _lvWetlandsColumnSorter;
    private ListViewColumnSorter _lvWetlandsColumnSorter1;
    SpatialDataAccess _spDataAccess;
    frmFilter _frmFilter;
    frmDatePicker _frmDatePicker;
    private string _speciesWhereClause;
    private bool _wetlandsLoaded = false;


    public frmThreatenedSpecies(IApplication pApplication)
    {
        InitializeComponent();

        _application = pApplication;
        IMxDocument pMxDocument = _application.Document as IMxDocument;
        _map = pMxDocument.FocusMap;
        _lvColumnSorter = new ListViewColumnSorter();
        lvSpecies.ListViewItemSorter = _lvColumnSorter;
        _lvWetlandsColumnSorter = new ListViewColumnSorter();
        lvWetlands.ListViewItemSorter = _lvWetlandsColumnSorter;
        _lvWetlandsColumnSorter1 = new ListViewColumnSorter();
        lvWetlands1.ListViewItemSorter = _lvWetlandsColumnSorter1;
        _lvColumnSorter1 = new ListViewColumnSorter();
        lvSpecies1.ListViewItemSorter = _lvColumnSorter1;
        _spDataAccess = new SpatialDataAccess();
        _spDataAccess.ProgressEvent += new SpatialDataAccess.ProgressEventHandler(_spDataAccess_ProgressEvent);
        _frmDatePicker = new frmDatePicker();
        _frmDatePicker.FormClosed += new FormClosedEventHandler(_frmDatePicker_FormClosed);
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
        lvSpecies.Items.Clear();
        ICursor pCursor = DataAccess.GetSpeciesNames(_featureWorkspace, _speciesWhereClause);
        IRow pRow = pCursor.NextRow();
        while (pRow != null)
        {
            AddSpeciesListItem(pRow, lvSpecies);
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

    private void LoadWetlands()
    {
        lvWetlands1.Items.Clear();
        ICursor pCursor = DataAccess.GetWetlands(_wetlandsFL);
        IRow pRow = pCursor.NextRow();
        while (pRow != null)
        {
            AddWetlandsListItem(pRow, lvWetlands1);
            pRow = pCursor.NextRow();
        }
    }

    private void AddWetlandsListItem(IRow pRow, ListView lv)
    {
        ListViewItem li = new ListViewItem();
        string id = Common.GetValueAsString(pRow, "MBCMA_wetland");
        li.Tag = id;
        li.Text = Common.GetValueAsString(pRow, "Wetland_Name");
        li.SubItems.Add(Common.GetValueAsString(pRow, "Complex_name"));
        li.SubItems.Add(Common.GetValueAsString(pRow, "Flow"));
        li.SubItems.Add(id);
        lv.Items.Add(li);
    }

    private void ZoomToWetlandFeatures()
    {
        ZoomToSelectedFeatures(lvWetlands, _wetlandsFL);
    }

    private void ZoomToSpeciesFeatures()
    {
        ZoomToSelectedFeatures(lvSpecies1, _threatenedSpeciesFL);
    }

    private void ZoomToSelectedFeatures(ListView lv, IFeatureLayer pFeatureLayer)
    {
        if (lv.SelectedItems != null)
        {
            if (lv.SelectedItems.Count > 0)
            {
                int[] oid = new int[lv.SelectedItems.Count];
                for (int i = 0; i < lv.SelectedItems.Count; i++)
                {
                    oid[i] = Convert.ToInt32(lv.SelectedItems[i].Tag.ToString());
                }
                Common.ZoomToFeatures(oid, pFeatureLayer, _map);
            }
        }
    }

    private void SelectWetlandFeatures()
    {
        SelectFeatures(lvWetlands, _wetlandsFL );
    }

    private void SelectSpeciesFeatures()
    {
        SelectFeatures(lvSpecies1, _threatenedSpeciesFL);
    }

    private void SelectFeatures(ListView lv, IFeatureLayer pFeatureLayer)
    {
        if (lv.SelectedItems != null)
        {
            if (lv.SelectedItems.Count > 0)
            {
                int[] oid = new int[lv.SelectedItems.Count];
                for (int i = 0; i < lv.SelectedItems.Count; i++)
                {
                    oid[i] = Convert.ToInt32(lv.SelectedItems[i].Tag.ToString());
                }
                Common.SelectFeatures(oid, pFeatureLayer, _map);
            }
        }
    }

    private void FlashWetlandFeatures()
    {
        FlashFeatures(lvWetlands, _wetlandsFL);
    }

    private void FlashSpeciesFeatures()
    {
        FlashFeatures(lvSpecies1, _threatenedSpeciesFL);
    }

    private void FlashFeatures(ListView lv, IFeatureLayer pFeatureLayer)
    {
        if (lv.SelectedItems != null)
        {
            if (lv.SelectedItems.Count > 0)
            {
                int[] oid = new int[lv.SelectedItems.Count];
                for (int i = 0; i < lv.SelectedItems.Count; i++)
                {
                    oid[i] = Convert.ToInt32(lv.SelectedItems[i].Tag.ToString());
                }
                Common.FlashFeatures(oid, pFeatureLayer, _map);
            }
        }
    }

    private void ShowFilter()
    {
        IEnumerator pEnumerator = DataAccess.GetClassNames(_featureWorkspace);
        _frmFilter = new frmFilter(_speciesWhereClause, pEnumerator);
        _frmFilter.FormClosed += new FormClosedEventHandler(_frmFilter_FormClosed);
        _frmFilter.ShowDialog();

    }

    private void frmThreatenedSpecies_Load(object sender, EventArgs e)
    {
        try
        {
            if (!SetFeatureWorkspace())
            {
                MessageBox.Show(String.Format("Unable to open the data source. The application was looking for a layer called {0}.", Constants.LayerName.WetLands), Application.ProductName);
                this.Dispose();
            }
            //LoadSpecies();

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void lvSpecies_ColumnClick(object sender, ColumnClickEventArgs e)
    {
        try
        {
            if (e.Column == _lvColumnSorter.ColunmToSort)
            {
                if (_lvColumnSorter.SortOrder == "ascending")
                {
                    _lvColumnSorter.SortOrder = "descending";
                }
                else
                {
                    _lvColumnSorter.SortOrder = "ascending";
                }
            }
            else
            {
                _lvColumnSorter.ColunmToSort = e.Column;
                _lvColumnSorter.SortOrder = "ascending";
            }
            lvSpecies.Sort();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnFind_Click(object sender, EventArgs e)
    {
        try
        {
            this.Cursor = Cursors.WaitCursor;
            lvWetlands.Items.Clear();
            if (lvSpecies.SelectedItems != null)
            {
                if (lvSpecies.SelectedItems.Count > 0)
                {
                    double buffer = 0.0;
                    if (!(cboBuffer.Text == "None") && !(cboBuffer.Text == ""))
                    {
                        buffer = Convert.ToDouble(cboBuffer.Text);
                    }
                    string afterDate = txtAfterDate.Text;
                    string beforeDate = txtBeforeDate.Text;
                    ArrayList wetlands = _spDataAccess.GetWetlandsBySpecies(_featureWorkspace, _wetlandsFL, lvSpecies.SelectedItems[0].Tag.ToString(), buffer, afterDate, beforeDate);
                    Common.ArrayListToListView(wetlands, ref lvWetlands, "");
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

    private void btnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            SelectWetlandFeatures();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnZoom_Click(object sender, EventArgs e)
    {
        try
        {
            ZoomToWetlandFeatures();

        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, Application.ProductName);
        }
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            dlg.OverwritePrompt = true;
            dlg.Filter = "*.csv|*.csv";
            if (dlg.ShowDialog() != DialogResult.Cancel)
            {
                Common.ExportListView(dlg.FileName, lvWetlands);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void lvWetlands_ColumnClick(object sender, ColumnClickEventArgs e)
    {
        try
        {
            if (e.Column == _lvWetlandsColumnSorter.ColunmToSort)
            {
                if (_lvWetlandsColumnSorter.SortOrder == "ascending")
                {
                    _lvWetlandsColumnSorter.SortOrder = "descending";
                }
                else
                {
                    _lvWetlandsColumnSorter.SortOrder = "ascending";
                }
            }
            else
            {
                _lvWetlandsColumnSorter.ColunmToSort = e.Column;
                _lvWetlandsColumnSorter.SortOrder = "ascending";
            }
            lvWetlands.Sort();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnFlash_Click(object sender, EventArgs e)
    {
        try
        {
            FlashWetlandFeatures();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void mnuFilter_Click(object sender, EventArgs e)
    {
        try
        {
            ShowFilter();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
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

    private void btnAfterDate_Click(object sender, EventArgs e)
    {
        try
        {
            _frmDatePicker.DateType = "After";
            _frmDatePicker.ShowDialog();

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
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

    private void btnBeforeDate_Click(object sender, EventArgs e)
    {
        try
        {
            _frmDatePicker.DateType = "Before";
            _frmDatePicker.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }
        
    private void mnuExit_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    private void lvWetlands1_ColumnClick(object sender, ColumnClickEventArgs e)
    {
        try
        {
            if (e.Column == _lvWetlandsColumnSorter1.ColunmToSort)
            {
                if (_lvWetlandsColumnSorter1.SortOrder == "ascending")
                {
                    _lvWetlandsColumnSorter1.SortOrder = "descending";
                }
                else
                {
                    _lvWetlandsColumnSorter1.SortOrder = "ascending";
                }
            }
            else
            {
                _lvWetlandsColumnSorter1.ColunmToSort = e.Column;
                _lvWetlandsColumnSorter1.SortOrder = "ascending";
            }
            lvWetlands1.Sort();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void tab_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (tab.SelectedIndex == 1 && _wetlandsLoaded == false)
            {
                this.Cursor = Cursors.WaitCursor;
                LoadWetlands();
                _wetlandsLoaded = true;
                this.Cursor = Cursors.Default;
            }
        }
        catch (Exception ex)
        {
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnFind1_Click(object sender, EventArgs e)
    {
        try
        {
            this.Cursor = Cursors.WaitCursor;
            lvSpecies1.Items.Clear();
            if (lvWetlands1.SelectedItems != null)
            {
                if (lvWetlands1.SelectedItems.Count > 0)
                {
                    double buffer = 0.0;
                    if (!(cboBuffer1.Text == "None") && !(cboBuffer1.Text == ""))
                    {
                        buffer = Convert.ToDouble(cboBuffer1.Text);
                    }
                    IFeatureCursor species = _spDataAccess.GetSpeciesByWetland(_featureWorkspace, _wetlandsFL, lvWetlands1.SelectedItems[0].Tag.ToString(), buffer, txtAfter1.Text, txtBefore1.Text, _speciesWhereClause);
                    Common.FeatureCursorToListView(species, ref lvSpecies1, "");
                }
            }
            this.Cursor = Cursors.Default;

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void lvSpecies1_ColumnClick(object sender, ColumnClickEventArgs e)
    {
        try
        {
            if (e.Column == _lvColumnSorter1.ColunmToSort)
            {
                if (_lvColumnSorter1.SortOrder == "ascending")
                {
                    _lvColumnSorter1.SortOrder = "descending";
                }
                else
                {
                    _lvColumnSorter1.SortOrder = "ascending";
                }
            }
            else
            {
                _lvColumnSorter1.ColunmToSort = e.Column;
                _lvColumnSorter1.SortOrder = "ascending";
            }
            lvSpecies1.Sort();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnAfter1_Click(object sender, EventArgs e)
    {
        try
        {
            _frmDatePicker.DateType = "After1";
            _frmDatePicker.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnBefore1_Click(object sender, EventArgs e)
    {
        try
        {
            _frmDatePicker.DateType = "Before1";
            _frmDatePicker.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnHighlight1_Click(object sender, EventArgs e)
    {
        try
        {
            SelectSpeciesFeatures();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnZoom1_Click(object sender, EventArgs e)
    {
        try
        {
            ZoomToSpeciesFeatures();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnFlash1_Click(object sender, EventArgs e)
    {
        try
        {
            FlashSpeciesFeatures();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnExport1_Click(object sender, EventArgs e)
    {
        try
        {
            dlg.OverwritePrompt = true;
            dlg.Filter = "*.csv|*.csv";
            if (dlg.ShowDialog() != DialogResult.Cancel)
            {
                Common.ExportListView(dlg.FileName, lvSpecies1);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void lvWetlands_KeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            if ((e.KeyCode == Keys.A) && e.Control)
            {
                for (int i = 0; i < lvWetlands.Items.Count; i++)
                {
                    ListViewItem li = lvWetlands.Items[i];
                    li.Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void lvSpecies1_KeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            if ((e.KeyCode == Keys.A) && e.Control)
            {
                for (int i = 0; i < lvSpecies1.Items.Count; i++)
                {
                    ListViewItem li = lvSpecies1.Items[i];
                    li.Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void mnuAbout_Click(object sender, EventArgs e)
    {
        try
        {
            frmAboutBox frm = new frmAboutBox();
            frm.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
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
