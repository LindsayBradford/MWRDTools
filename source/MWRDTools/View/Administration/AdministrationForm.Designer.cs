namespace MWRDTools.View
{
  partial class AdministrationForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdministrationForm));
      this.AdminTabControl = new System.Windows.Forms.TabControl();
      this.AtlasNSWWildlifeTabPage = new System.Windows.Forms.TabPage();
      this.AtlasFaunaFileButton = new System.Windows.Forms.Button();
      this.AtlasFloraFileButton = new System.Windows.Forms.Button();
      this.AtlasFaunaTextBox = new System.Windows.Forms.TextBox();
      this.AtlasFloraTextBox = new System.Windows.Forms.TextBox();
      this.AtlasFloraLabel = new System.Windows.Forms.Label();
      this.AtlasFaunaLabel = new System.Windows.Forms.Label();
      this.AtlasImportButton = new System.Windows.Forms.Button();
      this.CarmImportTabPage = new System.Windows.Forms.TabPage();
      this.CarmScenarioSelectButton = new System.Windows.Forms.Button();
      this.CarmScenarioPathTextBox = new System.Windows.Forms.TextBox();
      this.CarmScenarioLabel = new System.Windows.Forms.Label();
      this.CarmImportButton = new System.Windows.Forms.Button();
      this.CarmScenarioFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.AdministrationstatusStrip = new System.Windows.Forms.StatusStrip();
      this.AdminStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.AdminStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
      this.OpenFloraFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.OpenFaunaFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.AtlasImportBackgroundWorker = new System.ComponentModel.BackgroundWorker();
      this.CARMImportBackgroundWorker = new System.ComponentModel.BackgroundWorker();
      this.AdminTabControl.SuspendLayout();
      this.AtlasNSWWildlifeTabPage.SuspendLayout();
      this.CarmImportTabPage.SuspendLayout();
      this.AdministrationstatusStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // AdminTabControl
      // 
      this.AdminTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.AdminTabControl.Controls.Add(this.AtlasNSWWildlifeTabPage);
      this.AdminTabControl.Controls.Add(this.CarmImportTabPage);
      this.AdminTabControl.Location = new System.Drawing.Point(3, 3);
      this.AdminTabControl.Name = "AdminTabControl";
      this.AdminTabControl.Padding = new System.Drawing.Point(3, 3);
      this.AdminTabControl.SelectedIndex = 0;
      this.AdminTabControl.Size = new System.Drawing.Size(408, 166);
      this.AdminTabControl.TabIndex = 0;
      // 
      // AtlasNSWWildlifeTabPage
      // 
      this.AtlasNSWWildlifeTabPage.Controls.Add(this.AtlasFaunaFileButton);
      this.AtlasNSWWildlifeTabPage.Controls.Add(this.AtlasFloraFileButton);
      this.AtlasNSWWildlifeTabPage.Controls.Add(this.AtlasFaunaTextBox);
      this.AtlasNSWWildlifeTabPage.Controls.Add(this.AtlasFloraTextBox);
      this.AtlasNSWWildlifeTabPage.Controls.Add(this.AtlasFloraLabel);
      this.AtlasNSWWildlifeTabPage.Controls.Add(this.AtlasFaunaLabel);
      this.AtlasNSWWildlifeTabPage.Controls.Add(this.AtlasImportButton);
      this.AtlasNSWWildlifeTabPage.Location = new System.Drawing.Point(4, 22);
      this.AtlasNSWWildlifeTabPage.Name = "AtlasNSWWildlifeTabPage";
      this.AtlasNSWWildlifeTabPage.Padding = new System.Windows.Forms.Padding(3);
      this.AtlasNSWWildlifeTabPage.Size = new System.Drawing.Size(400, 140);
      this.AtlasNSWWildlifeTabPage.TabIndex = 1;
      this.AtlasNSWWildlifeTabPage.Text = "Import Atlas of NSW Wilidlife";
      this.AtlasNSWWildlifeTabPage.UseVisualStyleBackColor = true;
      this.AtlasNSWWildlifeTabPage.Click += new System.EventHandler(this.AtlasNSWWildlifeTabPage_Click);
      // 
      // AtlasFaunaFileButton
      // 
      this.AtlasFaunaFileButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
      this.AtlasFaunaFileButton.Image = global::MWRDTools.Properties.Resources.openfolderHS;
      this.AtlasFaunaFileButton.Location = new System.Drawing.Point(358, 57);
      this.AtlasFaunaFileButton.Name = "AtlasFaunaFileButton";
      this.AtlasFaunaFileButton.Size = new System.Drawing.Size(29, 26);
      this.AtlasFaunaFileButton.TabIndex = 3;
      this.AtlasFaunaFileButton.UseVisualStyleBackColor = true;
      this.AtlasFaunaFileButton.Click += new System.EventHandler(this.AtlasFaunaFileButton_Click);
      // 
      // AtlasFloraFileButton
      // 
      this.AtlasFloraFileButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
      this.AtlasFloraFileButton.Image = global::MWRDTools.Properties.Resources.openfolderHS;
      this.AtlasFloraFileButton.Location = new System.Drawing.Point(358, 25);
      this.AtlasFloraFileButton.Name = "AtlasFloraFileButton";
      this.AtlasFloraFileButton.Size = new System.Drawing.Size(29, 26);
      this.AtlasFloraFileButton.TabIndex = 2;
      this.AtlasFloraFileButton.UseVisualStyleBackColor = true;
      this.AtlasFloraFileButton.Click += new System.EventHandler(this.AtlasFloraFileButton_Click);
      // 
      // AtlasFaunaTextBox
      // 
      this.AtlasFaunaTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.AtlasFaunaTextBox.Location = new System.Drawing.Point(86, 61);
      this.AtlasFaunaTextBox.Name = "AtlasFaunaTextBox";
      this.AtlasFaunaTextBox.ReadOnly = true;
      this.AtlasFaunaTextBox.Size = new System.Drawing.Size(266, 20);
      this.AtlasFaunaTextBox.TabIndex = 0;
      this.AtlasFaunaTextBox.TabStop = false;
      this.AtlasFaunaTextBox.TextChanged += new System.EventHandler(this.AtlasFaunaTextBox_TextChanged);
      // 
      // AtlasFloraTextBox
      // 
      this.AtlasFloraTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.AtlasFloraTextBox.Location = new System.Drawing.Point(86, 29);
      this.AtlasFloraTextBox.Name = "AtlasFloraTextBox";
      this.AtlasFloraTextBox.ReadOnly = true;
      this.AtlasFloraTextBox.Size = new System.Drawing.Size(266, 20);
      this.AtlasFloraTextBox.TabIndex = 8;
      this.AtlasFloraTextBox.TabStop = false;
      this.AtlasFloraTextBox.TextChanged += new System.EventHandler(this.AtlasFloraTextBox_TextChanged);
      // 
      // AtlasFloraLabel
      // 
      this.AtlasFloraLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this.AtlasFloraLabel.AutoSize = true;
      this.AtlasFloraLabel.Location = new System.Drawing.Point(28, 32);
      this.AtlasFloraLabel.Name = "AtlasFloraLabel";
      this.AtlasFloraLabel.Size = new System.Drawing.Size(52, 13);
      this.AtlasFloraLabel.TabIndex = 7;
      this.AtlasFloraLabel.Text = "Flora File:";
      this.AtlasFloraLabel.UseMnemonic = false;
      // 
      // AtlasFaunaLabel
      // 
      this.AtlasFaunaLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this.AtlasFaunaLabel.AutoSize = true;
      this.AtlasFaunaLabel.Location = new System.Drawing.Point(21, 64);
      this.AtlasFaunaLabel.Name = "AtlasFaunaLabel";
      this.AtlasFaunaLabel.Size = new System.Drawing.Size(59, 13);
      this.AtlasFaunaLabel.TabIndex = 0;
      this.AtlasFaunaLabel.Text = "Fauna File:";
      // 
      // AtlasImportButton
      // 
      this.AtlasImportButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.AtlasImportButton.Enabled = false;
      this.AtlasImportButton.Location = new System.Drawing.Point(168, 110);
      this.AtlasImportButton.Name = "AtlasImportButton";
      this.AtlasImportButton.Size = new System.Drawing.Size(75, 23);
      this.AtlasImportButton.TabIndex = 4;
      this.AtlasImportButton.Text = "&Import";
      this.AtlasImportButton.UseVisualStyleBackColor = true;
      this.AtlasImportButton.Click += new System.EventHandler(this.AtlasImportButton_Click);
      // 
      // CarmImportTabPage
      // 
      this.CarmImportTabPage.Controls.Add(this.CarmScenarioSelectButton);
      this.CarmImportTabPage.Controls.Add(this.CarmScenarioPathTextBox);
      this.CarmImportTabPage.Controls.Add(this.CarmScenarioLabel);
      this.CarmImportTabPage.Controls.Add(this.CarmImportButton);
      this.CarmImportTabPage.Location = new System.Drawing.Point(4, 22);
      this.CarmImportTabPage.Margin = new System.Windows.Forms.Padding(0);
      this.CarmImportTabPage.Name = "CarmImportTabPage";
      this.CarmImportTabPage.Padding = new System.Windows.Forms.Padding(3);
      this.CarmImportTabPage.Size = new System.Drawing.Size(400, 140);
      this.CarmImportTabPage.TabIndex = 0;
      this.CarmImportTabPage.Text = "Import CARM Scenario";
      this.CarmImportTabPage.UseVisualStyleBackColor = true;
      this.CarmImportTabPage.Click += new System.EventHandler(this.CarmImportTabPage_Click);
      // 
      // CarmScenarioSelectButton
      // 
      this.CarmScenarioSelectButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
      this.CarmScenarioSelectButton.Image = global::MWRDTools.Properties.Resources.openfolderHS;
      this.CarmScenarioSelectButton.Location = new System.Drawing.Point(364, 48);
      this.CarmScenarioSelectButton.Name = "CarmScenarioSelectButton";
      this.CarmScenarioSelectButton.Size = new System.Drawing.Size(29, 26);
      this.CarmScenarioSelectButton.TabIndex = 1;
      this.CarmScenarioSelectButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.CarmScenarioSelectButton.UseVisualStyleBackColor = true;
      this.CarmScenarioSelectButton.Click += new System.EventHandler(this.CarmScenarioSelectButton_Click);
      // 
      // CarmScenarioPathTextBox
      // 
      this.CarmScenarioPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
      this.CarmScenarioPathTextBox.Location = new System.Drawing.Point(119, 52);
      this.CarmScenarioPathTextBox.Name = "CarmScenarioPathTextBox";
      this.CarmScenarioPathTextBox.ReadOnly = true;
      this.CarmScenarioPathTextBox.Size = new System.Drawing.Size(239, 20);
      this.CarmScenarioPathTextBox.TabIndex = 0;
      this.CarmScenarioPathTextBox.TabStop = false;
      this.CarmScenarioPathTextBox.TextChanged += new System.EventHandler(this.CarmScenarioPathTextBox_TextChanged);
      // 
      // CarmScenarioLabel
      // 
      this.CarmScenarioLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this.CarmScenarioLabel.AutoSize = true;
      this.CarmScenarioLabel.Location = new System.Drawing.Point(6, 55);
      this.CarmScenarioLabel.Name = "CarmScenarioLabel";
      this.CarmScenarioLabel.Size = new System.Drawing.Size(108, 13);
      this.CarmScenarioLabel.TabIndex = 0;
      this.CarmScenarioLabel.Text = "CARM Scenario Path";
      this.CarmScenarioLabel.UseMnemonic = false;
      // 
      // CarmImportButton
      // 
      this.CarmImportButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.CarmImportButton.Enabled = false;
      this.CarmImportButton.Location = new System.Drawing.Point(165, 111);
      this.CarmImportButton.Name = "CarmImportButton";
      this.CarmImportButton.Size = new System.Drawing.Size(75, 23);
      this.CarmImportButton.TabIndex = 2;
      this.CarmImportButton.Text = "&Import";
      this.CarmImportButton.UseVisualStyleBackColor = true;
      this.CarmImportButton.Click += new System.EventHandler(this.CarmImportButton_Click);
      // 
      // CarmScenarioFolderDialog
      // 
      this.CarmScenarioFolderDialog.Description = "Select CARM Scenario Direcrtory:";
      this.CarmScenarioFolderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
      this.CarmScenarioFolderDialog.ShowNewFolderButton = false;
      // 
      // AdministrationstatusStrip
      // 
      this.AdministrationstatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AdminStripLabel,
            this.AdminStripProgressBar});
      this.AdministrationstatusStrip.Location = new System.Drawing.Point(0, 171);
      this.AdministrationstatusStrip.Name = "AdministrationstatusStrip";
      this.AdministrationstatusStrip.Size = new System.Drawing.Size(412, 22);
      this.AdministrationstatusStrip.TabIndex = 1;
      // 
      // AdminStripLabel
      // 
      this.AdminStripLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
      this.AdminStripLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
      this.AdminStripLabel.Name = "AdminStripLabel";
      this.AdminStripLabel.Size = new System.Drawing.Size(295, 17);
      this.AdminStripLabel.Spring = true;
      this.AdminStripLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // AdminStripProgressBar
      // 
      this.AdminStripProgressBar.Name = "AdminStripProgressBar";
      this.AdminStripProgressBar.Size = new System.Drawing.Size(100, 16);
      this.AdminStripProgressBar.Step = 1;
      // 
      // OpenFloraFileDialog
      // 
      this.OpenFloraFileDialog.Filter = "Text files|*.txt";
      // 
      // OpenFaunaFileDialog
      // 
      this.OpenFaunaFileDialog.DefaultExt = "txt";
      this.OpenFaunaFileDialog.Filter = "Text files|*.txt";
      // 
      // AtlasImportBackgroundWorker
      // 
      this.AtlasImportBackgroundWorker.WorkerReportsProgress = true;
      // 
      // CARMImportBackgroundWorker
      // 
      this.CARMImportBackgroundWorker.WorkerReportsProgress = true;
      // 
      // AdministrationForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
      this.ClientSize = new System.Drawing.Size(412, 193);
      this.Controls.Add(this.AdminTabControl);
      this.Controls.Add(this.AdministrationstatusStrip);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(420, 220);
      this.Name = "AdministrationForm";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "MWRD Administration";
      this.TopMost = true;
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdministrationForm_Closing);
      this.AdminTabControl.ResumeLayout(false);
      this.AtlasNSWWildlifeTabPage.ResumeLayout(false);
      this.AtlasNSWWildlifeTabPage.PerformLayout();
      this.CarmImportTabPage.ResumeLayout(false);
      this.CarmImportTabPage.PerformLayout();
      this.AdministrationstatusStrip.ResumeLayout(false);
      this.AdministrationstatusStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TabControl AdminTabControl;
    private System.Windows.Forms.TabPage CarmImportTabPage;
    private System.Windows.Forms.TabPage AtlasNSWWildlifeTabPage;
    private System.Windows.Forms.TextBox CarmScenarioPathTextBox;
    private System.Windows.Forms.Label CarmScenarioLabel;
    private System.Windows.Forms.Button CarmImportButton;
    private System.Windows.Forms.Button CarmScenarioSelectButton;
    private System.Windows.Forms.FolderBrowserDialog CarmScenarioFolderDialog;
    private System.Windows.Forms.StatusStrip AdministrationstatusStrip;
    private System.Windows.Forms.ToolStripStatusLabel AdminStripLabel;
    private System.Windows.Forms.ToolStripProgressBar AdminStripProgressBar;
    private System.Windows.Forms.Button AtlasImportButton;
    private System.Windows.Forms.Label AtlasFaunaLabel;
    private System.Windows.Forms.TextBox AtlasFaunaTextBox;
    private System.Windows.Forms.TextBox AtlasFloraTextBox;
    private System.Windows.Forms.Label AtlasFloraLabel;
    private System.Windows.Forms.Button AtlasFaunaFileButton;
    private System.Windows.Forms.Button AtlasFloraFileButton;
    private System.Windows.Forms.OpenFileDialog OpenFloraFileDialog;
    private System.Windows.Forms.OpenFileDialog OpenFaunaFileDialog;
    private System.ComponentModel.BackgroundWorker AtlasImportBackgroundWorker;
    private System.ComponentModel.BackgroundWorker CARMImportBackgroundWorker;
  }
}