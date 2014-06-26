/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

using System;
using System.Windows.Forms;

public partial class DatePickerForm : Form
{
    private string _dateType;
       
    public DatePickerForm()
    {
        InitializeComponent();
    }
    public DatePickerForm(string dateType)
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