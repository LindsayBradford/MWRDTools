using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

public partial class frmDatePicker : Form
{
    private string _dateType;
       
    public frmDatePicker()
    {
        InitializeComponent();
    }
    public frmDatePicker(string dateType)
    {
        InitializeComponent();
        _dateType = dateType;
    }

    public string DateType
    {
        get
        {
            return _dateType;
        }
        set
        {
            _dateType = value;
        }
    }

    public DateTime Result
    {
        get
        {
            return dateTimePicker1.Value;
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}