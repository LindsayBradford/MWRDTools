/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;

public partial class SpeciesFilterForm : Form 
{
    private bool _filterSettingsLoaded = false;
  
    public SpeciesFilterForm() {
      InitializeComponent();
    }

    public bool FilterSettingsLoaded {
      get { return _filterSettingsLoaded; }
    }

    public string[] GetSelectedSpeciesClasses() {
      List<string> classes = new List<string>();

      foreach (string item in lboTaxa.CheckedItems) {
        classes.Add(item);
      }

      return classes.ToArray();
    }
  
    public void SetSpeciesClasses(string[] speciesClasses) {
      lboTaxa.Items.Clear();
      foreach(string speciesClass in speciesClasses) {
        lboTaxa.Items.Add(speciesClass);
      }
      _filterSettingsLoaded = true;
    }

    public string[] GetSelectedSpeciesStatuses() {
      List<string> statuses = new List<string>();

      foreach (string item in lboStatus.CheckedItems) {
        statuses.Add(item);
      }

      return statuses.ToArray();
    }

    public void SetSpeciesStatuses(string[] speciesStatuses) {
      lboStatus.Items.Clear();
      foreach (string speciesStatus in speciesStatuses) {
        lboStatus.Items.Add(speciesStatus);
      }
      _filterSettingsLoaded = true;
    }

    private void btnCancel_Click(object sender, EventArgs e) {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void btnOK_Click(object sender, EventArgs e) {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

}