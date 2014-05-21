partial class AboutDialog
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
      this.cmdOK = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.labelProductName = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.labelVersion = new System.Windows.Forms.Label();
      this.labelCopyright = new System.Windows.Forms.Label();
      this.GrifffithUniversityPictureBox = new System.Windows.Forms.PictureBox();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.GrifffithUniversityPictureBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      this.SuspendLayout();
      // 
      // cmdOK
      // 
      this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdOK.Location = new System.Drawing.Point(317, 190);
      this.cmdOK.Name = "cmdOK";
      this.cmdOK.Size = new System.Drawing.Size(75, 23);
      this.cmdOK.TabIndex = 0;
      this.cmdOK.Text = "&OK";
      this.cmdOK.UseVisualStyleBackColor = true;
      this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.groupBox1.Location = new System.Drawing.Point(12, 168);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(382, 13);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      // 
      // labelProductName
      // 
      this.labelProductName.AutoSize = true;
      this.labelProductName.Location = new System.Drawing.Point(12, 91);
      this.labelProductName.Name = "labelProductName";
      this.labelProductName.Size = new System.Drawing.Size(334, 13);
      this.labelProductName.TabIndex = 4;
      this.labelProductName.Text = "Murrumbidgee Wetlands Relational Database Decision Support Tools";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 140);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(371, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "Developed originally by Parsons Brinckerhoff. Enhanced by Griffith University.";
      // 
      // labelVersion
      // 
      this.labelVersion.AutoSize = true;
      this.labelVersion.Location = new System.Drawing.Point(12, 115);
      this.labelVersion.Name = "labelVersion";
      this.labelVersion.Size = new System.Drawing.Size(63, 13);
      this.labelVersion.TabIndex = 6;
      this.labelVersion.Text = "Version: 2.1";
      // 
      // labelCopyright
      // 
      this.labelCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.labelCopyright.AutoSize = true;
      this.labelCopyright.Location = new System.Drawing.Point(12, 195);
      this.labelCopyright.Name = "labelCopyright";
      this.labelCopyright.Size = new System.Drawing.Size(35, 13);
      this.labelCopyright.TabIndex = 7;
      this.labelCopyright.Text = "label1";
      // 
      // GrifffithUniversityPictureBox
      // 
      this.GrifffithUniversityPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("GrifffithUniversityPictureBox.Image")));
      this.GrifffithUniversityPictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("GrifffithUniversityPictureBox.InitialImage")));
      this.GrifffithUniversityPictureBox.Location = new System.Drawing.Point(240, 22);
      this.GrifffithUniversityPictureBox.Name = "GrifffithUniversityPictureBox";
      this.GrifffithUniversityPictureBox.Size = new System.Drawing.Size(143, 59);
      this.GrifffithUniversityPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.GrifffithUniversityPictureBox.TabIndex = 8;
      this.GrifffithUniversityPictureBox.TabStop = false;
      // 
      // pictureBox2
      // 
      this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
      this.pictureBox2.Location = new System.Drawing.Point(12, 12);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(175, 59);
      this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox2.TabIndex = 10;
      this.pictureBox2.TabStop = false;
      // 
      // frmAboutBox
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.ClientSize = new System.Drawing.Size(406, 239);
      this.Controls.Add(this.pictureBox2);
      this.Controls.Add(this.GrifffithUniversityPictureBox);
      this.Controls.Add(this.labelCopyright);
      this.Controls.Add(this.labelVersion);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.labelProductName);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.cmdOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmAboutBox";
      this.Padding = new System.Windows.Forms.Padding(9);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "About MCMA Decision Support Tools";
      ((System.ComponentModel.ISupportInitialize)(this.GrifffithUniversityPictureBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdOK;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label labelProductName;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label labelVersion;
    private System.Windows.Forms.Label labelCopyright;
    private System.Windows.Forms.PictureBox GrifffithUniversityPictureBox;
    private System.Windows.Forms.PictureBox pictureBox2;

}