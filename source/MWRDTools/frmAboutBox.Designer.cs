partial class frmAboutBox
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAboutBox));
        this.cmdOK = new System.Windows.Forms.Button();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.pictureBox2 = new System.Windows.Forms.PictureBox();
        this.labelProductName = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.labelVersion = new System.Windows.Forms.Label();
        this.labelCopyright = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
        this.SuspendLayout();
        // 
        // cmdOK
        // 
        this.cmdOK.Location = new System.Drawing.Point(515, 458);
        this.cmdOK.Name = "cmdOK";
        this.cmdOK.Size = new System.Drawing.Size(75, 23);
        this.cmdOK.TabIndex = 0;
        this.cmdOK.Text = "&OK";
        this.cmdOK.UseVisualStyleBackColor = true;
        this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
        // 
        // pictureBox1
        // 
        this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
        this.pictureBox1.Location = new System.Drawing.Point(0, 0);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(604, 240);
        this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        this.pictureBox1.TabIndex = 1;
        this.pictureBox1.TabStop = false;
        // 
        // groupBox1
        // 
        this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
        this.groupBox1.Location = new System.Drawing.Point(12, 439);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(578, 10);
        this.groupBox1.TabIndex = 2;
        this.groupBox1.TabStop = false;
        // 
        // pictureBox2
        // 
        this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
        this.pictureBox2.Location = new System.Drawing.Point(12, 345);
        this.pictureBox2.Name = "pictureBox2";
        this.pictureBox2.Size = new System.Drawing.Size(364, 88);
        this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        this.pictureBox2.TabIndex = 3;
        this.pictureBox2.TabStop = false;
        // 
        // labelProductName
        // 
        this.labelProductName.AutoSize = true;
        this.labelProductName.Location = new System.Drawing.Point(5, 256);
        this.labelProductName.Name = "labelProductName";
        this.labelProductName.Size = new System.Drawing.Size(346, 13);
        this.labelProductName.TabIndex = 4;
        this.labelProductName.Text = "Murrimbidgee Catchment Management Authority Decision Support Tools";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(5, 315);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(174, 13);
        this.label2.TabIndex = 5;
        this.label2.Text = "Developed by Parsons Brinckerhoff";
        // 
        // labelVersion
        // 
        this.labelVersion.AutoSize = true;
        this.labelVersion.Location = new System.Drawing.Point(5, 284);
        this.labelVersion.Name = "labelVersion";
        this.labelVersion.Size = new System.Drawing.Size(88, 13);
        this.labelVersion.TabIndex = 6;
        this.labelVersion.Text = "Version: Beta 1.0";
        // 
        // labelCopyright
        // 
        this.labelCopyright.AutoSize = true;
        this.labelCopyright.Location = new System.Drawing.Point(12, 463);
        this.labelCopyright.Name = "labelCopyright";
        this.labelCopyright.Size = new System.Drawing.Size(35, 13);
        this.labelCopyright.TabIndex = 7;
        this.labelCopyright.Text = "label1";
        // 
        // frmAboutBox
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(605, 513);
        this.Controls.Add(this.labelCopyright);
        this.Controls.Add(this.labelVersion);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.labelProductName);
        this.Controls.Add(this.pictureBox2);
        this.Controls.Add(this.groupBox1);
        this.Controls.Add(this.pictureBox1);
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
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdOK;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.Label labelProductName;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label labelVersion;
    private System.Windows.Forms.Label labelCopyright;

}