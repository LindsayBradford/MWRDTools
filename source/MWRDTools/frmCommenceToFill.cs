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

public partial class frmCommenceToFill : Form
{
    private IApplication _application;
    private IMap _map;
    private IFeatureWorkspace _featureWorkspace;
    private IFeatureLayer _featureLayer;
    private ListViewColumnSorter _lvColumnSorter;
    private ListViewColumnSorter _lvColumnSorterGauge;

    public frmCommenceToFill(IApplication pApplication)
    {
        InitializeComponent();
        _application = pApplication;
        IMxDocument pMxDocument = _application.Document as IMxDocument;
        _map = pMxDocument.FocusMap;
        _lvColumnSorter = new ListViewColumnSorter();
        _lvColumnSorterGauge = new ListViewColumnSorter();
        lv.ListViewItemSorter = _lvColumnSorter;
        lvGauge.ListViewItemSorter = _lvColumnSorterGauge;
    }

    private void FillCombos()
    {
        //wagga flow query levels
        cboWagga.Items.Clear();
        cboWagga.Items.Add("35,000");
        cboWagga.Items.Add("47,000");
        cboWagga.Items.Add("> 47,000");

        cboGauge.Items.Clear();
        ICursor pCursor = DataAccess.GetGauges(_featureWorkspace);
        IRow pRow = pCursor.NextRow();
        while (pRow != null)
        {
            cboGauge.Items.Add(Common.GetValueAsString(pRow, "GaugeName"));
            pRow = pCursor.NextRow();
        }
    }

    private bool SetFeatureWorkspace()
    {
        _featureWorkspace = Common.GetFeatureWorkspace(Constants.LayerName.WetLands, _map, ref _featureLayer);
        return (_featureWorkspace != null);
    }

    private void RunFlowQuery()
    {
        err.Clear();
        lv.Items.Clear();
        if (ValidateFlowQueryParameters())
        {
            string param = cboWagga.Text;
            IFeatureCursor pFeatureCursor = DataAccess.GetWetlandsByFlow(_featureLayer, param);
            Common.FeatureCursorToListView(pFeatureCursor, ref lv, "");
        }
    }

    private bool ValidateFlowQueryParameters()
    {
        if (cboWagga.Text == "")
        {
            err.SetError(cboWagga, "A flow value must be selected");
            return false;
        }
        return true;

    }

    private void RunGaugeQuery()
    {
        err.Clear();
        lvGauge.Items.Clear();
        if (ValidateGaugeQueryParameters())
        {
            string gauge = cboGauge.Text;
            double flow = 0.0;
            flow = Convert.ToDouble(txtFlow.Text);
            ICursor pCursor = DataAccess.GetWetlandsByFlowAndGauge(_featureWorkspace, _featureLayer, gauge, flow);
            Common.CursorToListView(pCursor, ref lvGauge, "MCMAWetlands.ObjectID");
        }
    }

    public bool ValidateGaugeQueryParameters()
    {
        if (cboGauge.Text == "")
        {
            err.SetError(cboGauge, "A gauge must be selected");
            return false;
        }
        try
        {
            Convert.ToDouble(txtFlow.Text);
        }
        catch
        {
            err.SetError(txtFlow, "A valid numeric value is required");
            return false;
        }
        return true;
    }

    private void ZoomToSelectedFeature()
    {
        if (lv.SelectedItems != null)
        {
            if (lv.SelectedItems.Count > 0)
            {
                //int oid = Convert.ToInt32(lv.SelectedItems[0].Tag.ToString());
                //Common.ZoomToFeature(oid, _featureLayer, _map);
                int[] oid = new int[lv.SelectedItems.Count];
                for (int i = 0; i < lv.SelectedItems.Count; i++)
                {
                    oid[i] = Convert.ToInt32(lv.SelectedItems[i].Tag.ToString());
                }
                Common.ZoomToFeatures(oid, _featureLayer, _map);
            }
        }
    }

    private void ZoomToSelectedFeatureGauge()
    {
        if (lvGauge.SelectedItems != null)
        {
            if (lvGauge.SelectedItems.Count > 0)
            {
                int[] oid = new int[lvGauge.SelectedItems.Count];
                for (int i = 0; i < lvGauge.SelectedItems.Count; i++)
                {
                    oid[i] = Convert.ToInt32(lvGauge.SelectedItems[i].Tag.ToString());
                }
                Common.ZoomToFeatures(oid, _featureLayer, _map);
            }
        }
    }

    private void SelectFeature()
    {
        if (lv.SelectedItems != null)
        {
            if (lv.SelectedItems.Count > 0)
            {
                //int oid = Convert.ToInt32(lv.SelectedItems[0].Tag.ToString());
                //Common.SelectFeature(oid, _featureLayer, _map);
                int[] oid = new int[lv.SelectedItems.Count];
                for (int i = 0; i < lv.SelectedItems.Count; i++)
                {
                    oid[i] = Convert.ToInt32(lv.SelectedItems[i].Tag.ToString());
                }
                Common.SelectFeatures(oid, _featureLayer, _map);
            }
        }
    }

    private void SelectFeatureGauge()
    {
        if (lvGauge.SelectedItems != null)
        {
            if (lvGauge.SelectedItems.Count > 0)
            {
                //int oid = Convert.ToInt32(lvGauge.SelectedItems[0].Tag.ToString());
                //Common.SelectFeature(oid, _featureLayer, _map);
                int[] oid = new int[lvGauge.SelectedItems.Count];
                for (int i = 0; i < lvGauge.SelectedItems.Count; i++)
                {
                    oid[i] = Convert.ToInt32(lvGauge.SelectedItems[i].Tag.ToString());
                }
                Common.SelectFeatures(oid, _featureLayer, _map);
            }
        }
    }

    private void FlashFeature()
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
                Common.FlashFeatures(oid, _featureLayer, _map);
            }
        }
    }

    private void FlashFeatureGauge()
    {
        if (lvGauge.SelectedItems != null)
        {
            if (lvGauge.SelectedItems.Count > 0)
            {
                int[] oid = new int[lvGauge.SelectedItems.Count];
                for (int i = 0; i < lvGauge.SelectedItems.Count; i++)
                {
                    oid[i] = Convert.ToInt32(lvGauge.SelectedItems[i].Tag.ToString());
                }
                Common.FlashFeatures(oid, _featureLayer, _map);
            }
        }

    }

    private void frmCommenceToFill_Load(object sender, EventArgs e)
    {
        try
        {
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
            FillCombos();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
              ex.Message, 
              Application.ProductName
            );
        }
    }

    private void lv_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void btnWaggaFind_Click(object sender, EventArgs e)
    {
        try
        {
            RunFlowQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void cboWagga_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void btnZoom_Click(object sender, EventArgs e)
    {
        try
        {
            ZoomToSelectedFeature();

        }
        catch(Exception exception)
        {
            MessageBox.Show(exception.Message, Application.ProductName);
        }
    }

    private void btnHighlight_Click(object sender, EventArgs e)
    {
        try
        {
            SelectFeature();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void lv_ColumnClick(object sender, ColumnClickEventArgs e)
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
            lv.Sort();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnFindGauge_Click(object sender, EventArgs e)
    {
        try
        {
            RunGaugeQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnSelectGauge_Click(object sender, EventArgs e)
    {
        try
        {
            SelectFeatureGauge();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnZoomGauge_Click(object sender, EventArgs e)
    {
        try
        {
            ZoomToSelectedFeatureGauge();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void lvGauge_ColumnClick(object sender, ColumnClickEventArgs e)
    {
        try
        {
            if (e.Column == _lvColumnSorterGauge.ColunmToSort)
            {
                if (_lvColumnSorterGauge.SortOrder == "ascending")
                {
                    _lvColumnSorterGauge.SortOrder = "descending";
                }
                else
                {
                    _lvColumnSorterGauge.SortOrder = "ascending";
                }
            }
            else
            {
                _lvColumnSorterGauge.ColunmToSort = e.Column;
                _lvColumnSorterGauge.SortOrder = "ascending";
            }
            lvGauge.Sort();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
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
                Common.ExportListView(dlg.FileName, lv);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnExportGauge_Click(object sender, EventArgs e)
    {
        try
        {
            dlg.OverwritePrompt = true;
            dlg.Filter = "*.csv|*.csv";
            if (dlg.ShowDialog() != DialogResult.Cancel)
            {
                Common.ExportListView(dlg.FileName, lvGauge);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void tab_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tab.SelectedIndex == 0)
        {
            this.AcceptButton = this.btnWaggaFind;
        }
        else
        {
            this.AcceptButton = this.btnFindGauge;
        }
    }

    private void mnu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {

    }

    private void mnuExit_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    private void mnuHelp_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    HelpNavigator navigator = HelpNavigator.TableOfContents;
        //    Help.ShowHelp(this, "mspaint.chm", navigator, "");
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(ex.Message, Application.ProductName);
        //}
    }

    private void btnFlash_Click(object sender, EventArgs e)
    {
        try
        {
            FlashFeature();

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void lv_KeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            if ((e.KeyCode == Keys.A) && e.Control)
            {
                for (int i = 0; i < lv.Items.Count; i++)
                {
                    ListViewItem li = lv.Items[i];
                    li.Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void btnFlashGauge_Click(object sender, EventArgs e)
    {
        try
        {
            FlashFeatureGauge();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void txtFlow_KeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            if (e.KeyCode == Keys.Enter)
            {
                RunGaugeQuery();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void lvGauge_KeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            if ((e.KeyCode == Keys.A) && e.Control)
            {
                for (int i = 0; i < lvGauge.Items.Count; i++)
                {
                    ListViewItem li = lvGauge.Items[i];
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