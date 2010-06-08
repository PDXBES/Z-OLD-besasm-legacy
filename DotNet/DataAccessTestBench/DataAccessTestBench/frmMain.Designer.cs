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
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabExecuteTestQueries = new System.Windows.Forms.TabPage();
      this.tabE18TableData = new System.Windows.Forms.TabPage();
      this.btnLoadE18Data = new Infragistics.Win.Misc.UltraButton();
      this.dgvTableE18 = new System.Windows.Forms.DataGridView();
      this.tabE19TableData = new System.Windows.Forms.TabPage();
      this.btnLoadE19Data = new Infragistics.Win.Misc.UltraButton();
      this.dgvTableE19 = new System.Windows.Forms.DataGridView();
      this.tabSetDataSources = new System.Windows.Forms.TabPage();
      this.btnLoadRpReport = new Infragistics.Win.Misc.UltraButton();
      this.btnChooseTestSwmmOutput = new Infragistics.Win.Misc.UltraButton();
      this.btnChooseTestAltPath = new Infragistics.Win.Misc.UltraButton();
      this.btnChooseTestModelPath = new Infragistics.Win.Misc.UltraButton();
      this.txtSwmmOutputPath = new System.Windows.Forms.TextBox();
      this.txtTestAltPath = new System.Windows.Forms.TextBox();
      this.txtTestModelPath = new System.Windows.Forms.TextBox();
      this.ofdModelDataSource = new System.Windows.Forms.OpenFileDialog();
      this.lblBaseModelPath = new Infragistics.Win.Misc.UltraLabel();
      this.lblAlternativePath = new Infragistics.Win.Misc.UltraLabel();
      this.lblModelOutputPath = new Infragistics.Win.Misc.UltraLabel();
      this.tabControl1.SuspendLayout();
      this.tabExecuteTestQueries.SuspendLayout();
      this.tabE18TableData.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvTableE18)).BeginInit();
      this.tabE19TableData.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvTableE19)).BeginInit();
      this.tabSetDataSources.SuspendLayout();
      this.SuspendLayout();
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(6, 8);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.textBox1.Size = new System.Drawing.Size(324, 275);
      this.textBox1.TabIndex = 0;
      // 
      // btnExecuteQueriesDirect
      // 
      this.btnExecuteQueriesDirect.Location = new System.Drawing.Point(336, 6);
      this.btnExecuteQueriesDirect.Name = "btnExecuteQueriesDirect";
      this.btnExecuteQueriesDirect.Size = new System.Drawing.Size(164, 23);
      this.btnExecuteQueriesDirect.TabIndex = 1;
      this.btnExecuteQueriesDirect.Text = "Execute queries directly";
      this.btnExecuteQueriesDirect.UseVisualStyleBackColor = true;
      this.btnExecuteQueriesDirect.Click += new System.EventHandler(this.btnExecuteQueriesDirect_Click);
      // 
      // btnUseReportLibrary
      // 
      this.btnUseReportLibrary.Location = new System.Drawing.Point(336, 35);
      this.btnUseReportLibrary.Name = "btnUseReportLibrary";
      this.btnUseReportLibrary.Size = new System.Drawing.Size(164, 23);
      this.btnUseReportLibrary.TabIndex = 2;
      this.btnUseReportLibrary.Text = "Use Report Library";
      this.btnUseReportLibrary.UseVisualStyleBackColor = true;
      this.btnUseReportLibrary.Click += new System.EventHandler(this.btnUseReportLibrary_Click);
      // 
      // btnBSBRTest
      // 
      this.btnBSBRTest.Location = new System.Drawing.Point(336, 64);
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
      this.btnCloseApplication.Location = new System.Drawing.Point(491, 338);
      this.btnCloseApplication.Name = "btnCloseApplication";
      this.btnCloseApplication.Size = new System.Drawing.Size(75, 23);
      this.btnCloseApplication.TabIndex = 4;
      this.btnCloseApplication.Text = "Close";
      this.btnCloseApplication.Click += new System.EventHandler(this.btnCloseApplication_Click);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabExecuteTestQueries);
      this.tabControl1.Controls.Add(this.tabE18TableData);
      this.tabControl1.Controls.Add(this.tabE19TableData);
      this.tabControl1.Controls.Add(this.tabSetDataSources);
      this.tabControl1.Location = new System.Drawing.Point(12, 12);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(514, 315);
      this.tabControl1.TabIndex = 5;
      // 
      // tabExecuteTestQueries
      // 
      this.tabExecuteTestQueries.Controls.Add(this.textBox1);
      this.tabExecuteTestQueries.Controls.Add(this.btnExecuteQueriesDirect);
      this.tabExecuteTestQueries.Controls.Add(this.btnUseReportLibrary);
      this.tabExecuteTestQueries.Controls.Add(this.btnBSBRTest);
      this.tabExecuteTestQueries.Location = new System.Drawing.Point(4, 22);
      this.tabExecuteTestQueries.Name = "tabExecuteTestQueries";
      this.tabExecuteTestQueries.Padding = new System.Windows.Forms.Padding(3);
      this.tabExecuteTestQueries.Size = new System.Drawing.Size(506, 289);
      this.tabExecuteTestQueries.TabIndex = 0;
      this.tabExecuteTestQueries.Text = "Execute Test Queries";
      this.tabExecuteTestQueries.UseVisualStyleBackColor = true;
      // 
      // tabE18TableData
      // 
      this.tabE18TableData.Controls.Add(this.btnLoadE18Data);
      this.tabE18TableData.Controls.Add(this.dgvTableE18);
      this.tabE18TableData.Location = new System.Drawing.Point(4, 22);
      this.tabE18TableData.Name = "tabE18TableData";
      this.tabE18TableData.Padding = new System.Windows.Forms.Padding(3);
      this.tabE18TableData.Size = new System.Drawing.Size(506, 289);
      this.tabE18TableData.TabIndex = 1;
      this.tabE18TableData.Text = "Table E18 Data";
      this.tabE18TableData.UseVisualStyleBackColor = true;
      // 
      // btnLoadE18Data
      // 
      this.btnLoadE18Data.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
      this.btnLoadE18Data.Location = new System.Drawing.Point(404, 260);
      this.btnLoadE18Data.Name = "btnLoadE18Data";
      this.btnLoadE18Data.Size = new System.Drawing.Size(96, 23);
      this.btnLoadE18Data.TabIndex = 1;
      this.btnLoadE18Data.Text = "Load Table E18";
      this.btnLoadE18Data.Click += new System.EventHandler(this.btnLoadE18Data_Click);
      // 
      // dgvTableE18
      // 
      this.dgvTableE18.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvTableE18.Location = new System.Drawing.Point(6, 6);
      this.dgvTableE18.Name = "dgvTableE18";
      this.dgvTableE18.Size = new System.Drawing.Size(392, 277);
      this.dgvTableE18.TabIndex = 0;
      // 
      // tabE19TableData
      // 
      this.tabE19TableData.Controls.Add(this.btnLoadE19Data);
      this.tabE19TableData.Controls.Add(this.dgvTableE19);
      this.tabE19TableData.Location = new System.Drawing.Point(4, 22);
      this.tabE19TableData.Name = "tabE19TableData";
      this.tabE19TableData.Padding = new System.Windows.Forms.Padding(3);
      this.tabE19TableData.Size = new System.Drawing.Size(506, 289);
      this.tabE19TableData.TabIndex = 2;
      this.tabE19TableData.Text = "Table E19 Data";
      this.tabE19TableData.UseVisualStyleBackColor = true;
      // 
      // btnLoadE19Data
      // 
      this.btnLoadE19Data.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
      this.btnLoadE19Data.Location = new System.Drawing.Point(406, 260);
      this.btnLoadE19Data.Name = "btnLoadE19Data";
      this.btnLoadE19Data.Size = new System.Drawing.Size(94, 23);
      this.btnLoadE19Data.TabIndex = 1;
      this.btnLoadE19Data.Text = "Load Table E19";
      this.btnLoadE19Data.Click += new System.EventHandler(this.btnLoadE19Data_Click);
      // 
      // dgvTableE19
      // 
      this.dgvTableE19.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvTableE19.Location = new System.Drawing.Point(6, 6);
      this.dgvTableE19.Name = "dgvTableE19";
      this.dgvTableE19.Size = new System.Drawing.Size(394, 277);
      this.dgvTableE19.TabIndex = 0;
      // 
      // tabSetDataSources
      // 
      this.tabSetDataSources.Controls.Add(this.lblModelOutputPath);
      this.tabSetDataSources.Controls.Add(this.lblAlternativePath);
      this.tabSetDataSources.Controls.Add(this.lblBaseModelPath);
      this.tabSetDataSources.Controls.Add(this.btnLoadRpReport);
      this.tabSetDataSources.Controls.Add(this.btnChooseTestSwmmOutput);
      this.tabSetDataSources.Controls.Add(this.btnChooseTestAltPath);
      this.tabSetDataSources.Controls.Add(this.btnChooseTestModelPath);
      this.tabSetDataSources.Controls.Add(this.txtSwmmOutputPath);
      this.tabSetDataSources.Controls.Add(this.txtTestAltPath);
      this.tabSetDataSources.Controls.Add(this.txtTestModelPath);
      this.tabSetDataSources.Location = new System.Drawing.Point(4, 22);
      this.tabSetDataSources.Name = "tabSetDataSources";
      this.tabSetDataSources.Padding = new System.Windows.Forms.Padding(3);
      this.tabSetDataSources.Size = new System.Drawing.Size(506, 289);
      this.tabSetDataSources.TabIndex = 3;
      this.tabSetDataSources.Text = "Load RP Report";
      this.tabSetDataSources.UseVisualStyleBackColor = true;
      // 
      // btnLoadRpReport
      // 
      this.btnLoadRpReport.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
      this.btnLoadRpReport.Location = new System.Drawing.Point(366, 260);
      this.btnLoadRpReport.Name = "btnLoadRpReport";
      this.btnLoadRpReport.Size = new System.Drawing.Size(134, 23);
      this.btnLoadRpReport.TabIndex = 6;
      this.btnLoadRpReport.Text = "Load RP Report";
      this.btnLoadRpReport.Click += new System.EventHandler(this.btnLoadRpReport_Click);
      // 
      // btnChooseTestSwmmOutput
      // 
      this.btnChooseTestSwmmOutput.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
      this.btnChooseTestSwmmOutput.Location = new System.Drawing.Point(386, 138);
      this.btnChooseTestSwmmOutput.Name = "btnChooseTestSwmmOutput";
      this.btnChooseTestSwmmOutput.Size = new System.Drawing.Size(75, 20);
      this.btnChooseTestSwmmOutput.TabIndex = 5;
      this.btnChooseTestSwmmOutput.Text = "Browse";
      this.btnChooseTestSwmmOutput.Click += new System.EventHandler(this.btnChooseTestSwmmOutput_Click);
      // 
      // btnChooseTestAltPath
      // 
      this.btnChooseTestAltPath.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
      this.btnChooseTestAltPath.Location = new System.Drawing.Point(386, 92);
      this.btnChooseTestAltPath.Name = "btnChooseTestAltPath";
      this.btnChooseTestAltPath.Size = new System.Drawing.Size(75, 20);
      this.btnChooseTestAltPath.TabIndex = 4;
      this.btnChooseTestAltPath.Text = "Browse";
      this.btnChooseTestAltPath.Click += new System.EventHandler(this.btnChooseTestAltPath_Click);
      // 
      // btnChooseTestModelPath
      // 
      this.btnChooseTestModelPath.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
      this.btnChooseTestModelPath.Location = new System.Drawing.Point(386, 45);
      this.btnChooseTestModelPath.Name = "btnChooseTestModelPath";
      this.btnChooseTestModelPath.Size = new System.Drawing.Size(75, 20);
      this.btnChooseTestModelPath.TabIndex = 3;
      this.btnChooseTestModelPath.Text = "Browse";
      this.btnChooseTestModelPath.Click += new System.EventHandler(this.btnChooseTestModelPath_Click);
      // 
      // txtSwmmOutputPath
      // 
      this.txtSwmmOutputPath.Location = new System.Drawing.Point(5, 138);
      this.txtSwmmOutputPath.Name = "txtSwmmOutputPath";
      this.txtSwmmOutputPath.Size = new System.Drawing.Size(373, 20);
      this.txtSwmmOutputPath.TabIndex = 2;
      // 
      // txtTestAltPath
      // 
      this.txtTestAltPath.Location = new System.Drawing.Point(5, 92);
      this.txtTestAltPath.Name = "txtTestAltPath";
      this.txtTestAltPath.Size = new System.Drawing.Size(374, 20);
      this.txtTestAltPath.TabIndex = 1;
      // 
      // txtTestModelPath
      // 
      this.txtTestModelPath.Location = new System.Drawing.Point(6, 45);
      this.txtTestModelPath.Name = "txtTestModelPath";
      this.txtTestModelPath.Size = new System.Drawing.Size(373, 20);
      this.txtTestModelPath.TabIndex = 0;
      // 
      // lblBaseModelPath
      // 
      this.lblBaseModelPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.lblBaseModelPath.Location = new System.Drawing.Point(6, 25);
      this.lblBaseModelPath.Name = "lblBaseModelPath";
      this.lblBaseModelPath.Size = new System.Drawing.Size(123, 14);
      this.lblBaseModelPath.TabIndex = 7;
      this.lblBaseModelPath.Text = "Load Base Model Path";
      // 
      // lblAlternativePath
      // 
      this.lblAlternativePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.lblAlternativePath.Location = new System.Drawing.Point(5, 71);
      this.lblAlternativePath.Name = "lblAlternativePath";
      this.lblAlternativePath.Size = new System.Drawing.Size(139, 15);
      this.lblAlternativePath.TabIndex = 8;
      this.lblAlternativePath.Text = "Load Alternative Path";
      // 
      // lblModelOutputPath
      // 
      this.lblModelOutputPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.lblModelOutputPath.Location = new System.Drawing.Point(6, 118);
      this.lblModelOutputPath.Name = "lblModelOutputPath";
      this.lblModelOutputPath.Size = new System.Drawing.Size(121, 14);
      this.lblModelOutputPath.TabIndex = 9;
      this.lblModelOutputPath.Text = "Load Model Results";
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(578, 373);
      this.Controls.Add(this.btnCloseApplication);
      this.Controls.Add(this.tabControl1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmMain";
      this.Text = "Data Access Test Bench";
      this.Load += new System.EventHandler(this.frmMain_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabExecuteTestQueries.ResumeLayout(false);
      this.tabExecuteTestQueries.PerformLayout();
      this.tabE18TableData.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvTableE18)).EndInit();
      this.tabE19TableData.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvTableE19)).EndInit();
      this.tabSetDataSources.ResumeLayout(false);
      this.tabSetDataSources.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button btnExecuteQueriesDirect;
    private System.Windows.Forms.Button btnUseReportLibrary;
    private System.Windows.Forms.Button btnBSBRTest;
    private Infragistics.Win.Misc.UltraButton btnCloseApplication;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabExecuteTestQueries;
    private System.Windows.Forms.TabPage tabE18TableData;
    private System.Windows.Forms.TabPage tabE19TableData;
    private System.Windows.Forms.DataGridView dgvTableE19;
    private Infragistics.Win.Misc.UltraButton btnLoadE18Data;
    private System.Windows.Forms.DataGridView dgvTableE18;
    private Infragistics.Win.Misc.UltraButton btnLoadE19Data;
    private System.Windows.Forms.TabPage tabSetDataSources;
    private Infragistics.Win.Misc.UltraButton btnChooseTestSwmmOutput;
    private Infragistics.Win.Misc.UltraButton btnChooseTestAltPath;
    private Infragistics.Win.Misc.UltraButton btnChooseTestModelPath;
    private System.Windows.Forms.TextBox txtSwmmOutputPath;
    private System.Windows.Forms.TextBox txtTestAltPath;
    private System.Windows.Forms.TextBox txtTestModelPath;
    private System.Windows.Forms.OpenFileDialog ofdModelDataSource;
    private Infragistics.Win.Misc.UltraButton btnLoadRpReport;
    private Infragistics.Win.Misc.UltraLabel lblModelOutputPath;
    private Infragistics.Win.Misc.UltraLabel lblAlternativePath;
    private Infragistics.Win.Misc.UltraLabel lblBaseModelPath;

  }
}

