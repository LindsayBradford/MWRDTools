﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

using MWRDTools.Presenter;

namespace MWRDTools.View
{
  
  public partial class AdministrationForm : Form, INSWAtlasWildlifeImportView, ICARMScenarioImportView
  {

    private IApplication application;

    private ICARMScenarioImportPresenter carmImportPresenter;
    private INSWAtlasWildlifeImportPresenter atlasImportPresenter;

#region Setup
    public AdministrationForm() : base()
    {
      InitializeComponent();
    }

    public IApplication Application {
      get { return this.application; }
      set { this.application = value; }
    }

    public void setCarmImportPresenter(ICARMScenarioImportPresenter presenter)
    {
      carmImportPresenter = presenter;
      presenter.StatusChanged += new EventHandler<ProgressChangedEventArgs>(this.StatusChangedHandler);
    }

    public void setAtlasImportPresenter(INSWAtlasWildlifeImportPresenter presenter)
    {
      atlasImportPresenter = presenter;
      presenter.StatusChanged += new EventHandler<ProgressChangedEventArgs>(this.StatusChangedHandler);
    }

    new public void Show() {
      this.AdminStripProgressBar.Value = 0;
      this.Refresh();
      base.Show();
    }

#endregion

#region InterfaceMethods
    void ICARMScenarioImportView.ImportDirectory(string direcoryPath) {
      this.carmImportPresenter.ImportDirectory(direcoryPath);
    }

    void INSWAtlasWildlifeImportView.ImportFiles(params String[] files) {
      ILayer layer = MapUtils.GetFeatureLayer(
        application, 
        Constants.LayerName.ThreatenedSpecies
      );

      layer.Visible = false;
      
      this.atlasImportPresenter.ImportFiles(files);

      MapUtils.RefreshFeatureLayer(application, layer);

      layer.Visible = true;
    }

    private void showStatusString(String status)
    {
      this.AdminStripLabel.Invalidate();
      this.AdminStripLabel.Text = status;
      this.Refresh();
    }

    private void showPercentComplete(int percentComplete) {
      this.AdminStripProgressBar.Invalidate();
      this.AdminStripProgressBar.Value = percentComplete;
      this.Refresh();
    }
#endregion

#region EvaentHandlers
    private void CarmImportTabPage_Click(object sender, EventArgs e)
    {
      this.AcceptButton = CarmImportButton;
    }

    private void CarmScenarioSelectButton_Click(object sender, EventArgs e)
    {
      CarmScenarioFolderDialog.ShowDialog();
      CarmScenarioPathTextBox.Text = CarmScenarioFolderDialog.SelectedPath;
    }

    private void CarmImportButton_Click(object sender, EventArgs e)
    {
      this.AdminStripProgressBar.Value = 0;
      this.CarmImportButton.Enabled = false;
      (this as ICARMScenarioImportView).ImportDirectory(CarmScenarioPathTextBox.Text);
      this.CarmImportButton.Enabled = true;
    }

    private void AtlasNSWWildlifeTabPage_Click(object sender, EventArgs e)
    {
      this.AcceptButton = AtlasImportButton;
    }

    private void AtlasFloraFileButton_Click(object sender, EventArgs e)
    {
      OpenFloraFileDialog.ShowDialog();
      AtlasFloraTextBox.Text = OpenFloraFileDialog.FileName;
    }

    private void AtlasFaunaFileButton_Click(object sender, EventArgs e)
    {
      OpenFaunaFileDialog.ShowDialog();
      AtlasFaunaTextBox.Text = OpenFaunaFileDialog.FileName;
    }

    private void AtlasImportButton_Click(object sender, EventArgs e) {
      this.showPercentComplete(0);
      this.AtlasImportButton.Enabled = false;
      (this as INSWAtlasWildlifeImportView).ImportFiles(
        AtlasFaunaTextBox.Text, AtlasFloraTextBox.Text
      );
      this.AtlasImportButton.Enabled = true;
    }

    private void AtlasFloraTextBox_TextChanged(object sender, EventArgs e)
    {
      checkAtlasImportButtonEnablable();
    }

    private void AtlasFaunaTextBox_TextChanged(object sender, EventArgs e)
    {
      checkAtlasImportButtonEnablable();
    }

    private void checkAtlasImportButtonEnablable() {
      if (AtlasFloraTextBox.Text.Length > 0 && AtlasFaunaTextBox.Text.Length > 0)
      {
        if (AtlasFloraTextBox.Text.Equals(AtlasFaunaTextBox.Text)) {
          AtlasImportButton.Enabled = false;
        } else {
          AtlasImportButton.Enabled = true;
        }
      }
      else  {
        AtlasImportButton.Enabled = false;
      }
    }

    private void CarmScenarioPathTextBox_TextChanged(object sender, EventArgs e)
    {
      checkCarmImportButtonEnablable();
    }

    private void checkCarmImportButtonEnablable()
    {
      if (CarmScenarioPathTextBox.Text.Length > 0)
      {
        CarmImportButton.Enabled = true;
      }
      else
      {
        CarmImportButton.Enabled = false;
      }
    }

    private void AdministrationForm_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
      e.Cancel = true;
      this.Hide();
    }

    public void StatusChangedHandler(object sender, ProgressChangedEventArgs args) {
      this.showStatusString(args.UserState as string);
      this.showPercentComplete(args.ProgressPercentage);
    }
  }
#endregion
}
