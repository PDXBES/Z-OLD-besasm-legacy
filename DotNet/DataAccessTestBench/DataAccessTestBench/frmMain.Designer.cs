namespace DataAccessTestBench
{
  partial class frmMain
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.btnExecuteQueriesDirect = new System.Windows.Forms.Button();
      this.btnUseReportLibrary = new System.Windows.Forms.Button();
      this.btnBSBRTest = new System.Windows.Forms.Button();
      this.btnCloseApplication = new Infragistics.Win.Misc.UltraButton();
      this.SuspendLayout();
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(12, 12);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.textBox1.Size = new System.Drawing.Size(312, 315);
      this.textBox1.TabIndex = 0;
      // 
      // btnExecuteQueriesDirect
      // 
      this.btnExecuteQueriesDirect.Location = new System.Drawing.Point(330, 12);
      this.btnExecuteQueriesDirect.Name = "btnExecuteQueriesDirect";
      this.btnExecuteQueriesDirect.Size = new System.Drawing.Size(164, 23);
      this.btnExecuteQueriesDirect.TabIndex = 1;
      this.btnExecuteQueriesDirect.Text = "Execute queries directly";
      this.btnExecuteQueriesDirect.UseVisualStyleBackColor = true;
      this.btnExecuteQueriesDirect.Click += new System.EventHandler(this.btnExecuteQueriesDirect_Click);
      // 
      // btnUseReportLibrary
      // 
      this.btnUseReportLibrary.Location = new System.Drawing.Point(330, 41);
      this.btnUseReportLibrary.Name = "btnUseReportLibrary";
      this.btnUseReportLibrary.Size = new System.Drawing.Size(164, 23);
      this.btnUseReportLibrary.TabIndex = 2;
      this.btnUseReportLibrary.Text = "Use Report Library";
      this.btnUseReportLibrary.UseVisualStyleBackColor = true;
      this.btnUseReportLibrary.Click += new System.EventHandler(this.btnUseReportLibrary_Click);
      // 
      // btnBSBRTest
      // 
      this.btnBSBRTest.Location = new System.Drawing.Point(330, 70);
      this.btnBSBRTest.Name = "btnBSBRTest";
      this.btnBSBRTest.Size = new System.Drawing.Size(164, 23);
      this.btnBSBRTest.TabIndex = 3;
      this.btnBSBRTest.Text = "BSBR Test";
      this.btnBSBRTest.UseVisualStyleBackColor = true;
      this.btnBSBRTest.Click += new System.EventHandler(this.btnBSBRTest_Click);
      // 
      // btnCloseApplication
      // 
      this.btnCloseApplication.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
      this.btnCloseApplication.Location = new System.Drawing.Point(451, 304);
      this.btnCloseApplication.Name = "btnCloseApplication";
      this.btnCloseApplication.Size = new System.Drawing.Size(75, 23);
      this.btnCloseApplication.TabIndex = 4;
      this.btnCloseApplication.Text = "Close";
      this.btnCloseApplication.Click += new System.EventHandler(this.btnCloseApplication_Click);
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(538, 339);
      this.Controls.Add(this.btnCloseApplication);
      this.Controls.Add(this.btnBSBRTest);
      this.Controls.Add(this.btnUseReportLibrary);
      this.Controls.Add(this.btnExecuteQueriesDirect);
      this.Controls.Add(this.textBox1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmMain";
      this.Text = "Data Access Test Bench";
      this.Load += new System.EventHandler(this.frmMain_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button btnExecuteQueriesDirect;
    private System.Windows.Forms.Button btnUseReportLibrary;
    private System.Windows.Forms.Button btnBSBRTest;
    private Infragistics.Win.Misc.UltraButton btnCloseApplication;

  }
}

