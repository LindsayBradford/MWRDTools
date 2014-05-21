using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

public partial class frmFilter : Form
{
    private string _whereClause;
    public frmFilter()
    {
        InitializeComponent();
    }

    public frmFilter(string whereClause, string[] speciesClasses) {
      InitializeComponent();
      _whereClause = whereClause;
      SetSpeciesClasses(speciesClasses);
    }

    public string WhereClause
    {
        get
        {
            return _whereClause;
        }
        set
        {
            _whereClause = value;
        }
    }

    private void ParseWhereClause()
    {
        //(First_LEGAL_STAT ='E1' OR First_LEGAL_STAT = 'E2') AND (First_CLASS_NAME ='AVES'); 
        CheckItems(lboStatus);
        CheckItems(lboTaxa);
    }

    private void CheckItems(CheckedListBox lbo)
    {
        for (int i = 0; i < lbo.Items.Count; i++)
        {
            string val = string.Format("'{0}'", lbo.Items[i].ToString());
            if (_whereClause.IndexOf(val) > 0)
            {
                lbo.SetItemChecked(i, true);
            }
        }
    }

    private void BuildWhereClause()
    {
        StringBuilder sb = new StringBuilder();
            
        if (lboStatus.CheckedItems.Count > 0)
        {
            sb.Append("(");
            for (int i = 0; i < lboStatus.CheckedItems.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(" OR ");
                }
                sb.Append("NSWStatus LIKE '%");
                sb.Append(lboStatus.CheckedItems[i].ToString());
                sb.Append("%'");
            }
            sb.Append(") ");
        }
        if (lboTaxa.CheckedItems.Count > 0)
        {
            if (sb.Length > 0)
            {
                sb.Append(" AND ");
            }
            sb.Append("(");
            for (int i = 0; i < lboTaxa.CheckedItems.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(" OR ");
                }
                sb.Append("ClassName ='");
                sb.Append(lboTaxa.CheckedItems[i].ToString());
                sb.Append("'");
            }
            sb.Append(")");
        }
        _whereClause = sb.ToString();
    }

    private void SetSpeciesClasses(string[] speciesClasses) {
      lboTaxa.Items.Clear();
      foreach(string speciesClass in speciesClasses) {
        lboTaxa.Items.Add(speciesClass);
      }
    }

    private void frmFilter_Load(object sender, EventArgs e) {
      if (_whereClause != "" && _whereClause != null) {
        ParseWhereClause();
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            this.DialogResult = DialogResult.OK;
            BuildWhereClause();
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName);
        }
    }

    private void lboStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void tabPage2_Click(object sender, EventArgs e)
    {

    }
}