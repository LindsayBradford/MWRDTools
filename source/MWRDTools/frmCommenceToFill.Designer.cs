partial class frmCommenceToFill
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCommenceToFill));
        this.tab = new System.Windows.Forms.TabControl();
        this.tabPage1 = new System.Windows.Forms.TabPage();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.label1 = new System.Windows.Forms.Label();
        this.btnWaggaFind = new System.Windows.Forms.Button();
        this.cboWagga = new System.Windows.Forms.ComboBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.lv = new System.Windows.Forms.ListView();
        this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
        this.tsb = new System.Windows.Forms.ToolStrip();
        this.btnHighlight = new System.Windows.Forms.ToolStripButton();
        this.btnZoom = new System.Windows.Forms.ToolStripButton();
        this.btnFlash = new System.Windows.Forms.ToolStripButton();
        this.btnExport = new System.Windows.Forms.ToolStripButton();
        this.tabPage2 = new System.Windows.Forms.TabPage();
        this.groupBox4 = new System.Windows.Forms.GroupBox();
        this.lvGauge = new System.Windows.Forms.ListView();
        this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
        this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
        this.toolStrip1 = new System.Windows.Forms.ToolStrip();
        this.btnSelectGauge = new System.Windows.Forms.ToolStripButton();
        this.btnZoomGauge = new System.Windows.Forms.ToolStripButton();
        this.btnFlashGauge = new System.Windows.Forms.ToolStripButton();
        this.btnExportGauge = new System.Windows.Forms.ToolStripButton();
        this.groupBox3 = new System.Windows.Forms.GroupBox();
        this.txtFlow = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.btnFindGauge = new System.Windows.Forms.Button();
        this.cboGauge = new System.Windows.Forms.ComboBox();
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
        this.tsb.SuspendLayout();
        this.tabPage2.SuspendLayout();
        this.groupBox4.SuspendLayout();
        this.toolStrip1.SuspendLayout();
        this.groupBox3.SuspendLayout();
        this.menuStrip2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
        this.SuspendLayout();
        // 
        // tab
        // 
        this.tab.Controls.Add(this.tabPage1);
        this.tab.Controls.Add(this.tabPage2);
        this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tab.Location = new System.Drawing.Point(0, 24);
        this.tab.Name = "tab";
        this.tab.SelectedIndex = 0;
        this.tab.Size = new System.Drawing.Size(524, 434);
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
        this.tabPage1.Size = new System.Drawing.Size(516, 408);
        this.tabPage1.TabIndex = 0;
        this.tabPage1.Text = "CTF by flow at Wagga";
        this.tabPage1.UseVisualStyleBackColor = true;
        // 
        // groupBox2
        // 
        this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.groupBox2.Controls.Add(this.label1);
        this.groupBox2.Controls.Add(this.btnWaggaFind);
        this.groupBox2.Controls.Add(this.cboWagga);
        this.groupBox2.Location = new System.Drawing.Point(8, 3);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(505, 73);
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
        // btnWaggaFind
        // 
        this.btnWaggaFind.Image = ((System.Drawing.Image)(resources.GetObject("btnWaggaFind.Image")));
        this.btnWaggaFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnWaggaFind.Location = new System.Drawing.Point(258, 26);
        this.btnWaggaFind.Name = "btnWaggaFind";
        this.btnWaggaFind.Size = new System.Drawing.Size(75, 23);
        this.btnWaggaFind.TabIndex = 2;
        this.btnWaggaFind.Text = "    &Find";
        this.btnWaggaFind.UseVisualStyleBackColor = true;
        this.btnWaggaFind.Click += new System.EventHandler(this.btnWaggaFind_Click);
        // 
        // cboWagga
        // 
        this.cboWagga.FormattingEnabled = true;
        this.cboWagga.Location = new System.Drawing.Point(106, 27);
        this.cboWagga.Name = "cboWagga";
        this.cboWagga.Size = new System.Drawing.Size(131, 21);
        this.cboWagga.TabIndex = 0;
        this.cboWagga.SelectedIndexChanged += new System.EventHandler(this.cboWagga_SelectedIndexChanged);
        // 
        // groupBox1
        // 
        this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.groupBox1.Controls.Add(this.lv);
        this.groupBox1.Controls.Add(this.tsb);
        this.groupBox1.Location = new System.Drawing.Point(8, 75);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(505, 316);
        this.groupBox1.TabIndex = 0;
        this.groupBox1.TabStop = false;
        // 
        // lv
        // 
        this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
        this.columnHeader1,
        this.columnHeader2,
        this.columnHeader3});
        this.lv.Dock = System.Windows.Forms.DockStyle.Fill;
        this.lv.FullRowSelect = true;
        this.lv.HideSelection = false;
        this.lv.Location = new System.Drawing.Point(3, 41);
        this.lv.Name = "lv";
        this.lv.Size = new System.Drawing.Size(499, 272);
        this.lv.TabIndex = 1;
        this.lv.UseCompatibleStateImageBehavior = false;
        this.lv.View = System.Windows.Forms.View.Details;
        this.lv.SelectedIndexChanged += new System.EventHandler(this.lv_SelectedIndexChanged);
        this.lv.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_ColumnClick);
        this.lv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lv_KeyDown);
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
        // tsb
        // 
        this.tsb.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
        this.tsb.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.btnHighlight,
        this.btnZoom,
        this.btnFlash,
        this.btnExport});
        this.tsb.Location = new System.Drawing.Point(3, 16);
        this.tsb.Name = "tsb";
        this.tsb.Size = new System.Drawing.Size(499, 25);
        this.tsb.TabIndex = 0;
        // 
        // btnHighlight
        // 
        this.btnHighlight.Image = ((System.Drawing.Image)(resources.GetObject("btnHighlight.Image")));
        this.btnHighlight.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.btnHighlight.Name = "btnHighlight";
        this.btnHighlight.Size = new System.Drawing.Size(68, 22);
        this.btnHighlight.Text = "Highlight";
        this.btnHighlight.Click += new System.EventHandler(this.btnHighlight_Click);
        // 
        // btnZoom
        // 
        this.btnZoom.Image = ((System.Drawing.Image)(resources.GetObject("btnZoom.Image")));
        this.btnZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.btnZoom.Name = "btnZoom";
        this.btnZoom.Size = new System.Drawing.Size(53, 22);
        this.btnZoom.Text = "Zoom";
        this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
        // 
        // btnFlash
        // 
        this.btnFlash.Image = ((System.Drawing.Image)(resources.GetObject("btnFlash.Image")));
        this.btnFlash.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.btnFlash.Name = "btnFlash";
        this.btnFlash.Size = new System.Drawing.Size(52, 22);
        this.btnFlash.Text = "Flash";
        this.btnFlash.Click += new System.EventHandler(this.btnFlash_Click);
        // 
        // btnExport
        // 
        this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
        this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.btnExport.Name = "btnExport";
        this.btnExport.Size = new System.Drawing.Size(59, 22);
        this.btnExport.Text = "&Export";
        this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
        // 
        // tabPage2
        // 
        this.tabPage2.Controls.Add(this.groupBox4);
        this.tabPage2.Controls.Add(this.groupBox3);
        this.tabPage2.Location = new System.Drawing.Point(4, 22);
        this.tabPage2.Name = "tabPage2";
        this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage2.Size = new System.Drawing.Size(516, 408);
        this.tabPage2.TabIndex = 1;
        this.tabPage2.Text = "CTF by flow at gauge";
        this.tabPage2.UseVisualStyleBackColor = true;
        // 
        // groupBox4
        // 
        this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.groupBox4.Controls.Add(this.lvGauge);
        this.groupBox4.Controls.Add(this.toolStrip1);
        this.groupBox4.Location = new System.Drawing.Point(8, 75);
        this.groupBox4.Name = "groupBox4";
        this.groupBox4.Size = new System.Drawing.Size(505, 314);
        this.groupBox4.TabIndex = 5;
        this.groupBox4.TabStop = false;
        // 
        // lvGauge
        // 
        this.lvGauge.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
        this.columnHeader4,
        this.columnHeader5,
        this.columnHeader6});
        this.lvGauge.Dock = System.Windows.Forms.DockStyle.Fill;
        this.lvGauge.FullRowSelect = true;
        this.lvGauge.HideSelection = false;
        this.lvGauge.Location = new System.Drawing.Point(3, 41);
        this.lvGauge.Name = "lvGauge";
        this.lvGauge.Size = new System.Drawing.Size(499, 270);
        this.lvGauge.TabIndex = 1;
        this.lvGauge.UseCompatibleStateImageBehavior = false;
        this.lvGauge.View = System.Windows.Forms.View.Details;
        this.lvGauge.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvGauge_ColumnClick);
        this.lvGauge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvGauge_KeyDown);
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
        // toolStrip1
        // 
        this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
        this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.btnSelectGauge,
        this.btnZoomGauge,
        this.btnFlashGauge,
        this.btnExportGauge});
        this.toolStrip1.Location = new System.Drawing.Point(3, 16);
        this.toolStrip1.Name = "toolStrip1";
        this.toolStrip1.Size = new System.Drawing.Size(499, 25);
        this.toolStrip1.TabIndex = 0;
        this.toolStrip1.Text = "tsbGauge";
        // 
        // btnSelectGauge
        // 
        this.btnSelectGauge.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectGauge.Image")));
        this.btnSelectGauge.ImageTransparentColor = System.Drawing.Color.Magenta;
        this.btnSelectGauge.Name = "btnSelectGauge";
        this.btnSelectGauge.Size = new System.Drawing.Size(68, 22);
        this.btnSelectGauge.Text = "Highlight";
        this.btnSelectGauge.Click += new System.EventHandler(this.btnSelectGauge_Click);
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
        this.groupBox3.Controls.Add(this.btnFindGauge);
        this.groupBox3.Controls.Add(this.cboGauge);
        this.groupBox3.Location = new System.Drawing.Point(8, 3);
        this.groupBox3.Name = "groupBox3";
        this.groupBox3.Size = new System.Drawing.Size(505, 73);
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
        // btnFindGauge
        // 
        this.btnFindGauge.Image = ((System.Drawing.Image)(resources.GetObject("btnFindGauge.Image")));
        this.btnFindGauge.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnFindGauge.Location = new System.Drawing.Point(396, 27);
        this.btnFindGauge.Name = "btnFindGauge";
        this.btnFindGauge.Size = new System.Drawing.Size(75, 23);
        this.btnFindGauge.TabIndex = 2;
        this.btnFindGauge.Text = "    &Find";
        this.btnFindGauge.UseVisualStyleBackColor = true;
        this.btnFindGauge.Click += new System.EventHandler(this.btnFindGauge_Click);
        // 
        // cboGauge
        // 
        this.cboGauge.FormattingEnabled = true;
        this.cboGauge.Location = new System.Drawing.Point(54, 28);
        this.cboGauge.Name = "cboGauge";
        this.cboGauge.Size = new System.Drawing.Size(165, 21);
        this.cboGauge.TabIndex = 0;
        // 
        // menuStrip2
        // 
        this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuFile,
        this.mnuHelp});
        this.menuStrip2.Location = new System.Drawing.Point(0, 0);
        this.menuStrip2.Name = "menuStrip2";
        this.menuStrip2.Size = new System.Drawing.Size(524, 24);
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
        this.mnuExit.Size = new System.Drawing.Size(152, 22);
        this.mnuExit.Text = "E&xit";
        this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
        // 
        // mnuHelp
        // 
        this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.mnuAbout});
        this.mnuHelp.Name = "mnuHelp";
        this.mnuHelp.Size = new System.Drawing.Size(40, 20);
        this.mnuHelp.Text = "&Help";
        this.mnuHelp.Click += new System.EventHandler(this.mnuHelp_Click);
        // 
        // mnuAbout
        // 
        this.mnuAbout.Name = "mnuAbout";
        this.mnuAbout.Size = new System.Drawing.Size(152, 22);
        this.mnuAbout.Text = "&About...";
        this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
        // 
        // err
        // 
        this.err.ContainerControl = this;
        // 
        // frmCommenceToFill
        // 
        this.AcceptButton = this.btnWaggaFind;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(524, 458);
        this.Controls.Add(this.tab);
        this.Controls.Add(this.menuStrip2);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Name = "frmCommenceToFill";
        this.Text = "Commence to Fill";
        this.Load += new System.EventHandler(this.frmCommenceToFill_Load);
        this.tab.ResumeLayout(false);
        this.tabPage1.ResumeLayout(false);
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.tsb.ResumeLayout(false);
        this.tsb.PerformLayout();
        this.tabPage2.ResumeLayout(false);
        this.groupBox4.ResumeLayout(false);
        this.groupBox4.PerformLayout();
        this.toolStrip1.ResumeLayout(false);
        this.toolStrip1.PerformLayout();
        this.groupBox3.ResumeLayout(false);
        this.groupBox3.PerformLayout();
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
    private System.Windows.Forms.ToolStrip tsb;
    private System.Windows.Forms.ToolStripButton btnHighlight;
    private System.Windows.Forms.ToolStripButton btnZoom;
    private System.Windows.Forms.Button btnWaggaFind;
    private System.Windows.Forms.ListView lv;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnFindGauge;
    private System.Windows.Forms.ComboBox cboGauge;
    private System.Windows.Forms.TextBox txtFlow;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnSelectGauge;
    private System.Windows.Forms.ToolStripButton btnZoomGauge;
    private System.Windows.Forms.ListView lvGauge;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.ColumnHeader columnHeader5;
    private System.Windows.Forms.ColumnHeader columnHeader6;
    private System.Windows.Forms.ToolStripButton btnExport;
    private System.Windows.Forms.SaveFileDialog dlg;
    private System.Windows.Forms.ToolStripButton btnExportGauge;
    private System.Windows.Forms.MenuStrip menuStrip2;
    private System.Windows.Forms.ToolStripMenuItem mnuFile;
    private System.Windows.Forms.ToolStripMenuItem mnuExit;
    private System.Windows.Forms.ToolStripMenuItem mnuHelp;
    private System.Windows.Forms.ToolStripButton btnFlash;
    private System.Windows.Forms.ErrorProvider err;
    private System.Windows.Forms.ToolStripButton btnFlashGauge;
    private System.Windows.Forms.ToolStripMenuItem mnuAbout;
}