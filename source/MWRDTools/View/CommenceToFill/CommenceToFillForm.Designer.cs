partial class CommenceToFillForm
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommenceToFillForm));
      this.tab = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnFindWaggaFlow = new System.Windows.Forms.Button();
      this.cboWagga = new System.Windows.Forms.ComboBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.WaggaListView = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.WaggaToolStrip = new System.Windows.Forms.ToolStrip();
      this.btnWaggaHighlight = new System.Windows.Forms.ToolStripButton();
      this.btnWaggaZoom = new System.Windows.Forms.ToolStripButton();
      this.btnWaggaFlash = new System.Windows.Forms.ToolStripButton();
      this.btnWaggaExport = new System.Windows.Forms.ToolStripButton();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.GaugeListView = new System.Windows.Forms.ListView();
      this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.GaugeToolStrip = new System.Windows.Forms.ToolStrip();
      this.btnHighlightGauge = new System.Windows.Forms.ToolStripButton();
      this.btnZoomGauge = new System.Windows.Forms.ToolStripButton();
      this.btnFlashGauge = new System.Windows.Forms.ToolStripButton();
      this.btnExportGauge = new System.Windows.Forms.ToolStripButton();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.txtFlow = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.btnFindGaugeFlow = new System.Windows.Forms.Button();
      this.cboGauge = new System.Windows.Forms.ComboBox();
      this.CTFbyCARMScenarioTabPage = new System.Windows.Forms.TabPage();
      this.groupBox5 = new System.Windows.Forms.GroupBox();
      this.CARMListView = new System.Windows.Forms.ListView();
      this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.CARMToolStrip = new System.Windows.Forms.ToolStrip();
      this.btnHilightCarm = new System.Windows.Forms.ToolStripButton();
      this.btnZoomCarm = new System.Windows.Forms.ToolStripButton();
      this.btnFlashCarm = new System.Windows.Forms.ToolStripButton();
      this.btnExportCarm = new System.Windows.Forms.ToolStripButton();
      this.groupBox6 = new System.Windows.Forms.GroupBox();
      this.ScenarioLabel = new System.Windows.Forms.Label();
      this.btnFindCarmScenario = new System.Windows.Forms.Button();
      this.cboCARMScenario = new System.Windows.Forms.ComboBox();
      this.dlg = new System.Windows.Forms.SaveFileDialog();
      this.menuStrip2 = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
      this.err = new System.Windows.Forms.ErrorProvider(this.components);
      this.tab.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.WaggaToolStrip.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.GaugeToolStrip.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.CTFbyCARMScenarioTabPage.SuspendLayout();
      this.groupBox5.SuspendLayout();
      this.CARMToolStrip.SuspendLayout();
      this.groupBox6.SuspendLayout();
      this.menuStrip2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
      this.SuspendLayout();
      // 
      // tab
      // 
      this.tab.Controls.Add(this.tabPage1);
      this.tab.Controls.Add(this.tabPage2);
      this.tab.Controls.Add(this.CTFbyCARMScenarioTabPage);
      this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tab.Location = new System.Drawing.Point(0, 24);
      this.tab.Name = "tab";
      this.tab.SelectedIndex = 0;
      this.tab.Size = new System.Drawing.Size(495, 432);
      this.tab.TabIndex = 0;
      this.tab.SelectedIndexChanged += new System.EventHandler(this.tab_SelectedIndexChanged);
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.groupBox2);
      this.tabPage1.Controls.Add(this.groupBox1);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(487, 406);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "CTF by flow at Wagga";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Controls.Add(this.btnFindWaggaFlow);
      this.groupBox2.Controls.Add(this.cboWagga);
      this.groupBox2.Location = new System.Drawing.Point(9, 3);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(470, 74);
      this.groupBox2.TabIndex = 3;
      this.groupBox2.TabStop = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(18, 31);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(82, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Flow at Wagga:";
      // 
      // btnFindWaggaFlow
      // 
      this.btnFindWaggaFlow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnFindWaggaFlow.Image = ((System.Drawing.Image)(resources.GetObject("btnFindWaggaFlow.Image")));
      this.btnFindWaggaFlow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnFindWaggaFlow.Location = new System.Drawing.Point(258, 26);
      this.btnFindWaggaFlow.Name = "btnFindWaggaFlow";
      this.btnFindWaggaFlow.Size = new System.Drawing.Size(75, 23);
      this.btnFindWaggaFlow.TabIndex = 2;
      this.btnFindWaggaFlow.Text = "    &Find";
      this.btnFindWaggaFlow.UseVisualStyleBackColor = true;
      this.btnFindWaggaFlow.Click += new System.EventHandler(this.btnWaggaFind_Click);
      // 
      // cboWagga
      // 
      this.cboWagga.FormattingEnabled = true;
      this.cboWagga.Location = new System.Drawing.Point(106, 27);
      this.cboWagga.Name = "cboWagga";
      this.cboWagga.Size = new System.Drawing.Size(131, 21);
      this.cboWagga.TabIndex = 0;
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.WaggaListView);
      this.groupBox1.Controls.Add(this.WaggaToolStrip);
      this.groupBox1.Location = new System.Drawing.Point(9, 81);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(470, 313);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      // 
      // WaggaListView
      // 
      this.WaggaListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
      this.WaggaListView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.WaggaListView.FullRowSelect = true;
      this.WaggaListView.HideSelection = false;
      this.WaggaListView.Location = new System.Drawing.Point(3, 41);
      this.WaggaListView.Name = "WaggaListView";
      this.WaggaListView.Size = new System.Drawing.Size(464, 269);
      this.WaggaListView.TabIndex = 1;
      this.WaggaListView.UseCompatibleStateImageBehavior = false;
      this.WaggaListView.View = System.Windows.Forms.View.Details;
      this.WaggaListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.WaggaListView_ColumnClick);
      this.WaggaListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WaggaListView_KeyDown);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Name";
      this.columnHeader1.Width = 227;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Flow";
      this.columnHeader2.Width = 99;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "MBCMA ID";
      this.columnHeader3.Width = 122;
      // 
      // WaggaToolStrip
      // 
      this.WaggaToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.WaggaToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnWaggaHighlight,
            this.btnWaggaZoom,
            this.btnWaggaFlash,
            this.btnWaggaExport});
      this.WaggaToolStrip.Location = new System.Drawing.Point(3, 16);
      this.WaggaToolStrip.Name = "WaggaToolStrip";
      this.WaggaToolStrip.Size = new System.Drawing.Size(464, 25);
      this.WaggaToolStrip.TabIndex = 0;
      // 
      // btnWaggaHighlight
      // 
      this.btnWaggaHighlight.Image = ((System.Drawing.Image)(resources.GetObject("btnWaggaHighlight.Image")));
      this.btnWaggaHighlight.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnWaggaHighlight.Name = "btnWaggaHighlight";
      this.btnWaggaHighlight.Size = new System.Drawing.Size(68, 22);
      this.btnWaggaHighlight.Text = "Highlight";
      this.btnWaggaHighlight.Click += new System.EventHandler(this.btnWaggaHighlight_Click);
      // 
      // btnWaggaZoom
      // 
      this.btnWaggaZoom.Image = ((System.Drawing.Image)(resources.GetObject("btnWaggaZoom.Image")));
      this.btnWaggaZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnWaggaZoom.Name = "btnWaggaZoom";
      this.btnWaggaZoom.Size = new System.Drawing.Size(53, 22);
      this.btnWaggaZoom.Text = "Zoom";
      this.btnWaggaZoom.Click += new System.EventHandler(this.btnWaggaZoom_Click);
      // 
      // btnWaggaFlash
      // 
      this.btnWaggaFlash.Image = ((System.Drawing.Image)(resources.GetObject("btnWaggaFlash.Image")));
      this.btnWaggaFlash.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnWaggaFlash.Name = "btnWaggaFlash";
      this.btnWaggaFlash.Size = new System.Drawing.Size(52, 22);
      this.btnWaggaFlash.Text = "Flash";
      this.btnWaggaFlash.Click += new System.EventHandler(this.btnWaggaFlash_Click);
      // 
      // btnWaggaExport
      // 
      this.btnWaggaExport.Image = ((System.Drawing.Image)(resources.GetObject("btnWaggaExport.Image")));
      this.btnWaggaExport.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnWaggaExport.Name = "btnWaggaExport";
      this.btnWaggaExport.Size = new System.Drawing.Size(59, 22);
      this.btnWaggaExport.Text = "&Export";
      this.btnWaggaExport.Click += new System.EventHandler(this.btnWaggaExport_Click);
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.groupBox4);
      this.tabPage2.Controls.Add(this.groupBox3);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(487, 406);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "CTF by flow at gauge";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // groupBox4
      // 
      this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox4.Controls.Add(this.GaugeListView);
      this.groupBox4.Controls.Add(this.GaugeToolStrip);
      this.groupBox4.Location = new System.Drawing.Point(9, 81);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(470, 313);
      this.groupBox4.TabIndex = 5;
      this.groupBox4.TabStop = false;
      // 
      // GaugeListView
      // 
      this.GaugeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
      this.GaugeListView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.GaugeListView.FullRowSelect = true;
      this.GaugeListView.HideSelection = false;
      this.GaugeListView.Location = new System.Drawing.Point(3, 41);
      this.GaugeListView.Name = "GaugeListView";
      this.GaugeListView.Size = new System.Drawing.Size(464, 269);
      this.GaugeListView.TabIndex = 1;
      this.GaugeListView.UseCompatibleStateImageBehavior = false;
      this.GaugeListView.View = System.Windows.Forms.View.Details;
      this.GaugeListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.GaugeListView_ColumnClick);
      this.GaugeListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GaugeListView_KeyDown);
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "Name";
      this.columnHeader4.Width = 221;
      // 
      // columnHeader5
      // 
      this.columnHeader5.Text = "Flow";
      // 
      // columnHeader6
      // 
      this.columnHeader6.Text = "MBCMA ID";
      this.columnHeader6.Width = 87;
      // 
      // GaugeToolStrip
      // 
      this.GaugeToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.GaugeToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnHighlightGauge,
            this.btnZoomGauge,
            this.btnFlashGauge,
            this.btnExportGauge});
      this.GaugeToolStrip.Location = new System.Drawing.Point(3, 16);
      this.GaugeToolStrip.Name = "GaugeToolStrip";
      this.GaugeToolStrip.Size = new System.Drawing.Size(464, 25);
      this.GaugeToolStrip.TabIndex = 0;
      this.GaugeToolStrip.Text = "tsbGauge";
      // 
      // btnHighlightGauge
      // 
      this.btnHighlightGauge.Image = ((System.Drawing.Image)(resources.GetObject("btnHighlightGauge.Image")));
      this.btnHighlightGauge.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnHighlightGauge.Name = "btnHighlightGauge";
      this.btnHighlightGauge.Size = new System.Drawing.Size(68, 22);
      this.btnHighlightGauge.Text = "Highlight";
      this.btnHighlightGauge.Click += new System.EventHandler(this.btnSelectGauge_Click);
      // 
      // btnZoomGauge
      // 
      this.btnZoomGauge.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomGauge.Image")));
      this.btnZoomGauge.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnZoomGauge.Name = "btnZoomGauge";
      this.btnZoomGauge.Size = new System.Drawing.Size(53, 22);
      this.btnZoomGauge.Text = "&Zoom";
      this.btnZoomGauge.Click += new System.EventHandler(this.btnZoomGauge_Click);
      // 
      // btnFlashGauge
      // 
      this.btnFlashGauge.Image = ((System.Drawing.Image)(resources.GetObject("btnFlashGauge.Image")));
      this.btnFlashGauge.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnFlashGauge.Name = "btnFlashGauge";
      this.btnFlashGauge.Size = new System.Drawing.Size(52, 22);
      this.btnFlashGauge.Text = "&Flash";
      this.btnFlashGauge.Click += new System.EventHandler(this.btnFlashGauge_Click);
      // 
      // btnExportGauge
      // 
      this.btnExportGauge.Image = ((System.Drawing.Image)(resources.GetObject("btnExportGauge.Image")));
      this.btnExportGauge.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnExportGauge.Name = "btnExportGauge";
      this.btnExportGauge.Size = new System.Drawing.Size(59, 22);
      this.btnExportGauge.Text = "&Export";
      this.btnExportGauge.Click += new System.EventHandler(this.btnExportGauge_Click);
      // 
      // groupBox3
      // 
      this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox3.Controls.Add(this.txtFlow);
      this.groupBox3.Controls.Add(this.label3);
      this.groupBox3.Controls.Add(this.label2);
      this.groupBox3.Controls.Add(this.btnFindGaugeFlow);
      this.groupBox3.Controls.Add(this.cboGauge);
      this.groupBox3.Location = new System.Drawing.Point(9, 3);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(470, 74);
      this.groupBox3.TabIndex = 4;
      this.groupBox3.TabStop = false;
      // 
      // txtFlow
      // 
      this.txtFlow.Location = new System.Drawing.Point(278, 28);
      this.txtFlow.Name = "txtFlow";
      this.txtFlow.Size = new System.Drawing.Size(88, 20);
      this.txtFlow.TabIndex = 4;
      this.txtFlow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow_KeyDown);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(240, 31);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(32, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "Flow:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 32);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(42, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Gauge:";
      // 
      // btnFindGaugeFlow
      // 
      this.btnFindGaugeFlow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnFindGaugeFlow.Image = ((System.Drawing.Image)(resources.GetObject("btnFindGaugeFlow.Image")));
      this.btnFindGaugeFlow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnFindGaugeFlow.Location = new System.Drawing.Point(386, 27);
      this.btnFindGaugeFlow.Name = "btnFindGaugeFlow";
      this.btnFindGaugeFlow.Size = new System.Drawing.Size(75, 23);
      this.btnFindGaugeFlow.TabIndex = 2;
      this.btnFindGaugeFlow.Text = "    &Find";
      this.btnFindGaugeFlow.UseVisualStyleBackColor = true;
      this.btnFindGaugeFlow.Click += new System.EventHandler(this.btnFindGauge_Click);
      // 
      // cboGauge
      // 
      this.cboGauge.FormattingEnabled = true;
      this.cboGauge.Location = new System.Drawing.Point(54, 28);
      this.cboGauge.Name = "cboGauge";
      this.cboGauge.Size = new System.Drawing.Size(165, 21);
      this.cboGauge.TabIndex = 0;
      // 
      // CTFbyCARMScenarioTabPage
      // 
      this.CTFbyCARMScenarioTabPage.Controls.Add(this.groupBox5);
      this.CTFbyCARMScenarioTabPage.Controls.Add(this.groupBox6);
      this.CTFbyCARMScenarioTabPage.Location = new System.Drawing.Point(4, 22);
      this.CTFbyCARMScenarioTabPage.Name = "CTFbyCARMScenarioTabPage";
      this.CTFbyCARMScenarioTabPage.Size = new System.Drawing.Size(487, 406);
      this.CTFbyCARMScenarioTabPage.TabIndex = 0;
      this.CTFbyCARMScenarioTabPage.Text = "CTF by CARM Scenario";
      this.CTFbyCARMScenarioTabPage.UseVisualStyleBackColor = true;
      // 
      // groupBox5
      // 
      this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox5.Controls.Add(this.CARMListView);
      this.groupBox5.Controls.Add(this.CARMToolStrip);
      this.groupBox5.Location = new System.Drawing.Point(9, 81);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(470, 313);
      this.groupBox5.TabIndex = 7;
      this.groupBox5.TabStop = false;
      // 
      // CARMListView
      // 
      this.CARMListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
      this.CARMListView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.CARMListView.FullRowSelect = true;
      this.CARMListView.HideSelection = false;
      this.CARMListView.Location = new System.Drawing.Point(3, 41);
      this.CARMListView.Name = "CARMListView";
      this.CARMListView.Size = new System.Drawing.Size(464, 269);
      this.CARMListView.TabIndex = 1;
      this.CARMListView.UseCompatibleStateImageBehavior = false;
      this.CARMListView.View = System.Windows.Forms.View.Details;
      this.CARMListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.CARMListView_ColumnClick);
      this.CARMListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CARMListView_KeyDown);
      // 
      // columnHeader7
      // 
      this.columnHeader7.Text = "Name";
      this.columnHeader7.Width = 221;
      // 
      // columnHeader8
      // 
      this.columnHeader8.Text = "Flow";
      // 
      // columnHeader9
      // 
      this.columnHeader9.Text = "MBCMA ID";
      this.columnHeader9.Width = 87;
      // 
      // CARMToolStrip
      // 
      this.CARMToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.CARMToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnHilightCarm,
            this.btnZoomCarm,
            this.btnFlashCarm,
            this.btnExportCarm});
      this.CARMToolStrip.Location = new System.Drawing.Point(3, 16);
      this.CARMToolStrip.Name = "CARMToolStrip";
      this.CARMToolStrip.Size = new System.Drawing.Size(464, 25);
      this.CARMToolStrip.TabIndex = 0;
      this.CARMToolStrip.Text = "tsbGauge";
      // 
      // btnHilightCarm
      // 
      this.btnHilightCarm.Image = ((System.Drawing.Image)(resources.GetObject("btnHilightCarm.Image")));
      this.btnHilightCarm.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnHilightCarm.Name = "btnHilightCarm";
      this.btnHilightCarm.Size = new System.Drawing.Size(68, 22);
      this.btnHilightCarm.Text = "Highlight";
      this.btnHilightCarm.Click += new System.EventHandler(this.btnHilightCarmScenario_Click);
      // 
      // btnZoomCarm
      // 
      this.btnZoomCarm.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomCarm.Image")));
      this.btnZoomCarm.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnZoomCarm.Name = "btnZoomCarm";
      this.btnZoomCarm.Size = new System.Drawing.Size(53, 22);
      this.btnZoomCarm.Text = "&Zoom";
      this.btnZoomCarm.Click += new System.EventHandler(this.btnZoomCarm_Click);
      // 
      // btnFlashCarm
      // 
      this.btnFlashCarm.Image = ((System.Drawing.Image)(resources.GetObject("btnFlashCarm.Image")));
      this.btnFlashCarm.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnFlashCarm.Name = "btnFlashCarm";
      this.btnFlashCarm.Size = new System.Drawing.Size(52, 22);
      this.btnFlashCarm.Text = "&Flash";
      this.btnFlashCarm.Click += new System.EventHandler(this.btnFlashCarm_Click);
      // 
      // btnExportCarm
      // 
      this.btnExportCarm.Image = ((System.Drawing.Image)(resources.GetObject("btnExportCarm.Image")));
      this.btnExportCarm.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnExportCarm.Name = "btnExportCarm";
      this.btnExportCarm.Size = new System.Drawing.Size(59, 22);
      this.btnExportCarm.Text = "&Export";
      this.btnExportCarm.Click += new System.EventHandler(this.btnExportCarm_Click);
      // 
      // groupBox6
      // 
      this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox6.Controls.Add(this.ScenarioLabel);
      this.groupBox6.Controls.Add(this.btnFindCarmScenario);
      this.groupBox6.Controls.Add(this.cboCARMScenario);
      this.groupBox6.Location = new System.Drawing.Point(9, 3);
      this.groupBox6.Name = "groupBox6";
      this.groupBox6.Size = new System.Drawing.Size(470, 74);
      this.groupBox6.TabIndex = 6;
      this.groupBox6.TabStop = false;
      // 
      // ScenarioLabel
      // 
      this.ScenarioLabel.AutoSize = true;
      this.ScenarioLabel.Location = new System.Drawing.Point(12, 32);
      this.ScenarioLabel.Name = "ScenarioLabel";
      this.ScenarioLabel.Size = new System.Drawing.Size(52, 13);
      this.ScenarioLabel.TabIndex = 1;
      this.ScenarioLabel.Text = "&Scenario:";
      // 
      // btnFindCarmScenario
      // 
      this.btnFindCarmScenario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnFindCarmScenario.Image = ((System.Drawing.Image)(resources.GetObject("btnFindCarmScenario.Image")));
      this.btnFindCarmScenario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnFindCarmScenario.Location = new System.Drawing.Point(382, 27);
      this.btnFindCarmScenario.Name = "btnFindCarmScenario";
      this.btnFindCarmScenario.Size = new System.Drawing.Size(75, 23);
      this.btnFindCarmScenario.TabIndex = 2;
      this.btnFindCarmScenario.Text = "    &Find";
      this.btnFindCarmScenario.UseVisualStyleBackColor = true;
      this.btnFindCarmScenario.Click += new System.EventHandler(this.btnFindCarmScenario_Click);
      // 
      // cboCARMScenario
      // 
      this.cboCARMScenario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cboCARMScenario.FormattingEnabled = true;
      this.cboCARMScenario.Location = new System.Drawing.Point(63, 28);
      this.cboCARMScenario.Name = "cboCARMScenario";
      this.cboCARMScenario.Size = new System.Drawing.Size(313, 21);
      this.cboCARMScenario.TabIndex = 0;
      // 
      // menuStrip2
      // 
      this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuHelp});
      this.menuStrip2.Location = new System.Drawing.Point(0, 0);
      this.menuStrip2.Name = "menuStrip2";
      this.menuStrip2.Size = new System.Drawing.Size(495, 24);
      this.menuStrip2.TabIndex = 1;
      this.menuStrip2.Text = "menuStrip2";
      // 
      // mnuFile
      // 
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExit});
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(35, 20);
      this.mnuFile.Text = "&File";
      // 
      // mnuExit
      // 
      this.mnuExit.Name = "mnuExit";
      this.mnuExit.Size = new System.Drawing.Size(92, 22);
      this.mnuExit.Text = "E&xit";
      // 
      // mnuHelp
      // 
      this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
      this.mnuHelp.Name = "mnuHelp";
      this.mnuHelp.Size = new System.Drawing.Size(40, 20);
      this.mnuHelp.Text = "&Help";
      // 
      // mnuAbout
      // 
      this.mnuAbout.Name = "mnuAbout";
      this.mnuAbout.Size = new System.Drawing.Size(115, 22);
      this.mnuAbout.Text = "&About...";
      this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
      // 
      // err
      // 
      this.err.ContainerControl = this;
      // 
      // CommenceToFillForm
      // 
      this.AcceptButton = this.btnFindWaggaFlow;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(495, 456);
      this.Controls.Add(this.tab);
      this.Controls.Add(this.menuStrip2);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimumSize = new System.Drawing.Size(503, 483);
      this.Name = "CommenceToFillForm";
      this.Text = "MWRD Commence to Fill";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CommenceToFillForm_FormClosing);
      this.Load += new System.EventHandler(this.CommenceToFillForm_VisibileChanged);
      this.VisibleChanged += new System.EventHandler(this.CommenceToFillForm_VisibileChanged);
      this.tab.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.WaggaToolStrip.ResumeLayout(false);
      this.WaggaToolStrip.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.GaugeToolStrip.ResumeLayout(false);
      this.GaugeToolStrip.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.CTFbyCARMScenarioTabPage.ResumeLayout(false);
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      this.CARMToolStrip.ResumeLayout(false);
      this.CARMToolStrip.PerformLayout();
      this.groupBox6.ResumeLayout(false);
      this.groupBox6.PerformLayout();
      this.menuStrip2.ResumeLayout(false);
      this.menuStrip2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TabControl tab;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cboWagga;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ToolStrip WaggaToolStrip;
    private System.Windows.Forms.ToolStripButton btnWaggaHighlight;
    private System.Windows.Forms.ToolStripButton btnWaggaZoom;
    private System.Windows.Forms.Button btnFindWaggaFlow;
    private System.Windows.Forms.ListView WaggaListView;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnFindGaugeFlow;
    private System.Windows.Forms.ComboBox cboGauge;
    private System.Windows.Forms.TextBox txtFlow;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.ToolStrip GaugeToolStrip;
    private System.Windows.Forms.ToolStripButton btnHighlightGauge;
    private System.Windows.Forms.ToolStripButton btnZoomGauge;
    private System.Windows.Forms.ListView GaugeListView;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.ColumnHeader columnHeader5;
    private System.Windows.Forms.ColumnHeader columnHeader6;
    private System.Windows.Forms.ToolStripButton btnWaggaExport;
    private System.Windows.Forms.SaveFileDialog dlg;
    private System.Windows.Forms.ToolStripButton btnExportGauge;
    private System.Windows.Forms.MenuStrip menuStrip2;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuExit;
    private System.Windows.Forms.ToolStripMenuItem mnuHelp;
    private System.Windows.Forms.ToolStripButton btnWaggaFlash;
    private System.Windows.Forms.ErrorProvider err;
    private System.Windows.Forms.ToolStripButton btnFlashGauge;
    private System.Windows.Forms.ToolStripMenuItem mnuAbout;
    private System.Windows.Forms.TabPage CTFbyCARMScenarioTabPage;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.ListView CARMListView;
    private System.Windows.Forms.ColumnHeader columnHeader7;
    private System.Windows.Forms.ColumnHeader columnHeader8;
    private System.Windows.Forms.ColumnHeader columnHeader9;
    private System.Windows.Forms.ToolStrip CARMToolStrip;
    private System.Windows.Forms.ToolStripButton btnHilightCarm;
    private System.Windows.Forms.ToolStripButton btnZoomCarm;
    private System.Windows.Forms.ToolStripButton btnFlashCarm;
    private System.Windows.Forms.ToolStripButton btnExportCarm;
    private System.Windows.Forms.GroupBox groupBox6;
    private System.Windows.Forms.Label ScenarioLabel;
    private System.Windows.Forms.Button btnFindCarmScenario;
    private System.Windows.Forms.ComboBox cboCARMScenario;
}