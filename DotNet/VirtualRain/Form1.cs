using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
//using System.Core;
using System.Data.DataSetExtensions;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data.Linq;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.MapControl;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Utility;
using ESRI.ArcGIS.Catalog;
using System.Reflection;
using ESRI.ArcGIS.Editor;

namespace VirtualRain
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMapEditing : System.Windows.Forms.Form
    {
        private bool m_bLayoutCalled = false;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.IContainer components;
        private DateTime m_dt;

		private System.Windows.Forms.Label lblInstructions1;
		private IMapControl3 m_MapControl; 
		private IMap m_pMap;
		private IAoInitialize ArcLicense;
		private ILayer m_pCurrentLayer;
		//private IFeature m_pEditFeature;
		//private IPoint m_pPoint;
		//private IDisplayFeedback m_pFeedback;
		//private IPointCollection m_pPointCollection;
		//private bool m_bInUse;
		//private bool m_bEditingFtr;
		//private bool m_bSketching;
		//private bool m_bSelecting;
		private System.Windows.Forms.Button btnProcess;
        SortedList QuarterSectionList = new SortedList();
        private SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.TextBox txtDestination;
		private System.Windows.Forms.Button btnSaveFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dtpStartDate;
		private System.Windows.Forms.DateTimePicker dtpEndDate;
		private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
		gageinfo[][] QuarterSectionGageInfo;
		bool FileLocationChosen = false;
		private VirtualRain.RainDSClass rainDS;
		private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Windows.Forms.Label label4;
		private System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1;
		private System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		private System.Data.OleDb.OleDbConnection oleDbConnection2;
		private VirtualRain.qsDS qsDS1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		long NumberOfGages; //number of gages
		DataTable graphData = new DataTable();
		DataSet theSet = new DataSet();
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.PictureBox pictureBox2;
		//System.Drawing.Bitmap graphBitmap;
		long NumberOfTimeStepsBetweenStartAndEnd = 0; //number of 5 minute intervals between sdate and edate
		DateTime d1;
		DateTime d2;
		double[][] largeRainArray;
		//float lastResult;
		//float thisResult;
		//int smoother;
		//int workingGagesB = 0;
		long[]h2B = new long[3];
		float[]rainB = new float[3];
		double[]distanceB = new double[3];
		float[]xCoord = new float[3];
		float[]yCoord = new float[3];
		//GraphCreator graph = null;
		//DataTable theTable;
        //IEditor m_Editor;
        RainTools rainTools = new RainTools();


		//int[] indices;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.Button buttonZoomIn;
		private System.Windows.Forms.Button buttonZoomOut;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.CheckBox checkSelectByBasin;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private Label label5;
        private TextBox txtQSBulkInput;
        private CheckBox checkBinaryOutput;
        private TabPage tpRainfallTab;
        private NumericUpDown nudTimeStep;
        private Label label7;
        private CheckBox cbReportZeroRain;
        private Button btnRealRainProcess;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private TextBox textBox1;
        private DateTimePicker dtpStartDay;
        private DateTimePicker dtpEndDay;
        private Label label6;
        private DateTimePicker dateTimePicker2;
        private Label label8;
        private DataGridView dataGridView1;
        private BindingSource nEPTUNEDataSetBindingSource;
        private NEPTUNEDataSet nEPTUNEDataSet;
        private BindingSource rAINSENSORBindingSource;
        private VirtualRain.NEPTUNEDataSetTableAdapters.RAIN_SENSORTableAdapter rAIN_SENSORTableAdapter;
        private BindingSource sTATIONBindingSource;
        private VirtualRain.NEPTUNEDataSetTableAdapters.STATIONTableAdapter sTATIONTableAdapter;
        private DataGridViewTextBoxColumn stationidDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn h2numberDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn stationnameDataGridViewTextBoxColumn;
        private Button btnExportToExcel;


		private esriLicenseStatus CheckOutArcLicense(esriLicenseProductCode productCode)
		{
			ArcLicense = new AoInitializeClass();
			/*esriLicenseStatus licenseStatus =
				ArcLicense.IsProductCodeAvailable(productCode);
			if (licenseStatus == esriLicenseStatus.esriLicenseAvailable)*/
            esriLicenseStatus licenseStatus = ArcLicense.Initialize(productCode);
		
			return licenseStatus;
		}

		/// <summary>
		// Class used to store layer name and file attributes
		/// </summary>
		class layerInfo
		{
			public string layerName;
			public long layerAttr;
			public override string ToString() { return layerName; }
		}

		[DllImport("kernel32.dll")]
        public static extern long GetFileAttributes(string fileName);

        //private IContainer components;

		public frmMapEditing()
		{
			//
			// Required for Windows Form Designer support
			//

			InitializeComponent();

            SplashScreen.SetStatus("Getting Arc License.");
			esriLicenseStatus licenseStatus = 
				CheckOutArcLicense(esriLicenseProductCode.esriLicenseProductCodeArcView);
			/*if (licenseStatus == esriLicenseStatus.esriLicenseUnavailable)
				MessageBox.Show("Could not check out ArcView license.");*/
            SplashScreen.SetStatus("Prepping Controls.");

            tabControl1.TabPages.Remove(tabControl1.TabPages[2]);
            tabControl1.TabPages.Remove(tabControl1.TabPages[1]);

            //SplashScreen.CloseForm();
			//this.Show();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			
			if( disposing )
			{
				if (components != null) 
				{
					ArcLicense.Shutdown();
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            // 
            // timer1
            // 
            this.timer1.Interval = 100;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(440, 380);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Form1_Layout);

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMapEditing));
            this.lblInstructions1 = new System.Windows.Forms.Label();
            this.btnProcess = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonZoomIn = new System.Windows.Forms.Button();
            this.buttonZoomOut = new System.Windows.Forms.Button();
            this.oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbConnection2 = new System.Data.OleDb.OleDbConnection();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtQSBulkInput = new System.Windows.Forms.TextBox();
            this.checkBinaryOutput = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.checkSelectByBasin = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tpRainfallTab = new System.Windows.Forms.TabPage();
            this.nudTimeStep = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.cbReportZeroRain = new System.Windows.Forms.CheckBox();
            this.btnRealRainProcess = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dtpStartDay = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDay = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.stationidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.h2numberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sTATIONBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nEPTUNEDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nEPTUNEDataSet = new VirtualRain.NEPTUNEDataSet();
            this.rAINSENSORBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rainDS = new VirtualRain.RainDSClass();
            this.qsDS1 = new VirtualRain.qsDS();
            this.rAIN_SENSORTableAdapter = new VirtualRain.NEPTUNEDataSetTableAdapters.RAIN_SENSORTableAdapter();
            this.sTATIONTableAdapter = new VirtualRain.NEPTUNEDataSetTableAdapters.STATIONTableAdapter();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tpRainfallTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sTATIONBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nEPTUNEDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nEPTUNEDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rAINSENSORBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rainDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qsDS1)).BeginInit();
            this.SuspendLayout();
            
            // 
            // lblInstructions1
            // 
            this.lblInstructions1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInstructions1.Location = new System.Drawing.Point(244, 437);
            this.lblInstructions1.Name = "lblInstructions1";
            this.lblInstructions1.Size = new System.Drawing.Size(232, 32);
            this.lblInstructions1.TabIndex = 8;
            this.lblInstructions1.Text = "QS list: ";
            this.lblInstructions1.Visible = false;
            // 
            // btnProcess
            // 
            this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnProcess.Location = new System.Drawing.Point(501, 419);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(176, 48);
            this.btnProcess.TabIndex = 12;
            this.btnProcess.Text = "Process";
            this.btnProcess.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // txtDestination
            // 
            this.txtDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDestination.Enabled = false;
            this.txtDestination.Location = new System.Drawing.Point(496, 313);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(176, 20);
            this.txtDestination.TabIndex = 13;
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveFile.Location = new System.Drawing.Point(496, 337);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(176, 24);
            this.btnSaveFile.TabIndex = 14;
            this.btnSaveFile.Text = "File Browser...";
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(228, 389);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(104, 20);
            this.dtpStartDate.TabIndex = 15;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(332, 389);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(112, 20);
            this.dtpEndDate.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(252, 373);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Start Date";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(356, 373);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "End Date";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpEndTime.CustomFormat = "HH:mm";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(332, 413);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.ShowUpDown = true;
            this.dtpEndTime.Size = new System.Drawing.Size(112, 20);
            this.dtpEndTime.TabIndex = 19;
            this.dtpEndTime.Value = new System.DateTime(2006, 11, 27, 0, 0, 0, 0);
            this.dtpEndTime.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpStartTime.CustomFormat = "HH:mm";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(228, 413);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.ShowUpDown = true;
            this.dtpStartTime.Size = new System.Drawing.Size(104, 20);
            this.dtpStartTime.TabIndex = 20;
            this.dtpStartTime.Value = new System.DateTime(2006, 11, 27, 0, 0, 0, 0);
            this.dtpStartTime.ValueChanged += new System.EventHandler(this.dtpStartTime_ValueChanged);
            // 
            // sqlDataAdapter1
            // 
            this.sqlDataAdapter1.InsertCommand = this.sqlInsertCommand1;
            this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
            this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "V_MODEL_RAIN_FIVE_MINUTE", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("h2_number", "h2_number"),
                        new System.Data.Common.DataColumnMapping("five_minute_date_time", "five_minute_date_time"),
                        new System.Data.Common.DataColumnMapping("five_minute_sum_inches", "five_minute_sum_inches")})});
            this.sqlDataAdapter1.RowUpdated += new System.Data.SqlClient.SqlRowUpdatedEventHandler(this.sqlDataAdapter1_RowUpdated);
            // 
            // sqlInsertCommand1
            // 
            this.sqlInsertCommand1.CommandText = resources.GetString("sqlInsertCommand1.CommandText");
            this.sqlInsertCommand1.Connection = this.sqlConnection1;
            this.sqlInsertCommand1.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] {
            new System.Data.SqlClient.SqlParameter("@h2_number", System.Data.SqlDbType.Int, 4, "h2_number"),
            new System.Data.SqlClient.SqlParameter("@five_minute_date_time", System.Data.SqlDbType.DateTime, 8, "five_minute_date_time"),
            new System.Data.SqlClient.SqlParameter("@five_minute_sum_inches", System.Data.SqlDbType.Decimal, 17, System.Data.ParameterDirection.Input, false, ((byte)(38)), ((byte)(3)), "five_minute_sum_inches", System.Data.DataRowVersion.Current, null)});
            // 
            // sqlConnection1
            // 
            this.sqlConnection1.ConnectionString = "workstation id=BES4712;packet size=4096;integrated security=SSPI;data source=Repo" +
                "rtsio;persist security info=False;initial catalog=NEPTUNE";
            this.sqlConnection1.FireInfoMessageEventOnUserErrors = false;
            this.sqlConnection1.InfoMessage += new System.Data.SqlClient.SqlInfoMessageEventHandler(this.sqlConnection1_InfoMessage);
            // 
            // sqlSelectCommand1
            // 
            this.sqlSelectCommand1.CommandText = "SELECT h2_number, five_minute_date_time, five_minute_sum_inches FROM dbo.V_MODEL_" +
                "RAIN_FIVE_MINUTE";
            this.sqlSelectCommand1.Connection = this.sqlConnection1;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Location = new System.Drawing.Point(496, 297);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 16);
            this.label4.TabIndex = 23;
            this.label4.Text = "Output file location:";
            
            // 
            // buttonZoomIn
            // 
            this.buttonZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("buttonZoomIn.Image")));
            this.buttonZoomIn.Location = new System.Drawing.Point(300, 299);
            this.buttonZoomIn.Name = "buttonZoomIn";
            this.buttonZoomIn.Size = new System.Drawing.Size(32, 32);
            this.buttonZoomIn.TabIndex = 26;
            this.buttonZoomIn.Click += new System.EventHandler(this.buttonZoomIn_Click);
            // 
            // buttonZoomOut
            // 
            this.buttonZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("buttonZoomOut.Image")));
            this.buttonZoomOut.Location = new System.Drawing.Point(332, 299);
            this.buttonZoomOut.Name = "buttonZoomOut";
            this.buttonZoomOut.Size = new System.Drawing.Size(32, 32);
            this.buttonZoomOut.TabIndex = 27;
            this.buttonZoomOut.Click += new System.EventHandler(this.buttonZoomOut_Click);
            // 
            // oleDbDataAdapter1
            // 
            this.oleDbDataAdapter1.SelectCommand = this.oleDbSelectCommand1;
            this.oleDbDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
            new System.Data.Common.DataTableMapping("Table", "QS_Data", new System.Data.Common.DataColumnMapping[] {
                        new System.Data.Common.DataColumnMapping("QTRNO", "QTRNO"),
                        new System.Data.Common.DataColumnMapping("Qs_x", "Qs_x"),
                        new System.Data.Common.DataColumnMapping("Qs_y", "Qs_y")})});
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT QTRNO, Qs_x, Qs_y FROM QS_Data ORDER BY QTRNO";
            this.oleDbSelectCommand1.Connection = this.oleDbConnection2;
            // 
            // oleDbConnection2
            // 
            this.oleDbConnection2.ConnectionString = resources.GetString("oleDbConnection2.ConnectionString");
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tpRainfallTab);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(697, 502);
            this.tabControl1.TabIndex = 28;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtQSBulkInput);
            this.tabPage1.Controls.Add(this.btnProcess);
            this.tabPage1.Controls.Add(this.dtpStartTime);
            this.tabPage1.Controls.Add(this.checkBinaryOutput);
            this.tabPage1.Controls.Add(this.dtpEndTime);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.axMapControl1);
            this.tabPage1.Controls.Add(this.dtpEndDate);
            this.tabPage1.Controls.Add(this.buttonZoomIn);
            this.tabPage1.Controls.Add(this.dtpStartDate);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.checkSelectByBasin);
            this.tabPage1.Controls.Add(this.lblInstructions1);
            this.tabPage1.Controls.Add(this.buttonZoomOut);
            this.tabPage1.Controls.Add(this.btnSaveFile);
            this.tabPage1.Controls.Add(this.txtDestination);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(689, 476);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Virtual Rainfall Generator";
            // 
            // txtQSBulkInput
            // 
            this.txtQSBulkInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtQSBulkInput.Location = new System.Drawing.Point(3, 322);
            this.txtQSBulkInput.Multiline = true;
            this.txtQSBulkInput.Name = "txtQSBulkInput";
            this.txtQSBulkInput.Size = new System.Drawing.Size(216, 145);
            this.txtQSBulkInput.TabIndex = 31;
            // 
            // checkBinaryOutput
            // 
            this.checkBinaryOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBinaryOutput.Location = new System.Drawing.Point(284, 337);
            this.checkBinaryOutput.Name = "checkBinaryOutput";
            this.checkBinaryOutput.Size = new System.Drawing.Size(104, 16);
            this.checkBinaryOutput.TabIndex = 32;
            this.checkBinaryOutput.Text = "Binary output";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 306);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Quartersection List:";
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(8, 12);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(671, 273);
            this.axMapControl1.TabIndex = 0;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            // 
            // checkSelectByBasin
            // 
            this.checkSelectByBasin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkSelectByBasin.Location = new System.Drawing.Point(284, 355);
            this.checkSelectByBasin.Name = "checkSelectByBasin";
            this.checkSelectByBasin.Size = new System.Drawing.Size(104, 16);
            this.checkSelectByBasin.TabIndex = 29;
            this.checkSelectByBasin.Text = "Select by basin";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pictureBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(689, 476);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Graphic Output";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.Location = new System.Drawing.Point(8, 8);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(664, 296);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.trackBar1);
            this.tabPage4.Controls.Add(this.pictureBox2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(689, 476);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Visualization";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(16, 296);
            this.trackBar1.Maximum = 1000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(656, 42);
            this.trackBar1.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(16, 16);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(656, 264);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // tpRainfallTab
            // 
            this.tpRainfallTab.Controls.Add(this.btnExportToExcel);
            this.tpRainfallTab.Controls.Add(this.nudTimeStep);
            this.tpRainfallTab.Controls.Add(this.label7);
            this.tpRainfallTab.Controls.Add(this.cbReportZeroRain);
            this.tpRainfallTab.Controls.Add(this.btnRealRainProcess);
            this.tpRainfallTab.Controls.Add(this.label3);
            this.tpRainfallTab.Controls.Add(this.dateTimePicker1);
            this.tpRainfallTab.Controls.Add(this.textBox1);
            this.tpRainfallTab.Controls.Add(this.dtpStartDay);
            this.tpRainfallTab.Controls.Add(this.dtpEndDay);
            this.tpRainfallTab.Controls.Add(this.label6);
            this.tpRainfallTab.Controls.Add(this.dateTimePicker2);
            this.tpRainfallTab.Controls.Add(this.label8);
            this.tpRainfallTab.Controls.Add(this.dataGridView1);
            this.tpRainfallTab.Location = new System.Drawing.Point(4, 22);
            this.tpRainfallTab.Name = "tpRainfallTab";
            this.tpRainfallTab.Padding = new System.Windows.Forms.Padding(3);
            this.tpRainfallTab.Size = new System.Drawing.Size(689, 476);
            this.tpRainfallTab.TabIndex = 4;
            this.tpRainfallTab.Text = "Real Rainfall Extractor";
            this.tpRainfallTab.UseVisualStyleBackColor = true;
            // 
            // nudTimeStep
            // 
            this.nudTimeStep.Location = new System.Drawing.Point(141, 190);
            this.nudTimeStep.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.nudTimeStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimeStep.Name = "nudTimeStep";
            this.nudTimeStep.Size = new System.Drawing.Size(59, 20);
            this.nudTimeStep.TabIndex = 26;
            this.nudTimeStep.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Time Step, Minutes:";
            // 
            // cbReportZeroRain
            // 
            this.cbReportZeroRain.AutoSize = true;
            this.cbReportZeroRain.Location = new System.Drawing.Point(306, 192);
            this.cbReportZeroRain.Name = "cbReportZeroRain";
            this.cbReportZeroRain.Size = new System.Drawing.Size(142, 17);
            this.cbReportZeroRain.TabIndex = 24;
            this.cbReportZeroRain.Text = "Report times with no rain";
            this.cbReportZeroRain.UseVisualStyleBackColor = true;
            // 
            // btnRealRainProcess
            // 
            this.btnRealRainProcess.Location = new System.Drawing.Point(324, 241);
            this.btnRealRainProcess.Name = "btnRealRainProcess";
            this.btnRealRainProcess.Size = new System.Drawing.Size(174, 43);
            this.btnRealRainProcess.TabIndex = 21;
            this.btnRealRainProcess.Text = "Export To CSV File";
            this.btnRealRainProcess.UseVisualStyleBackColor = true;
            this.btnRealRainProcess.Click += new System.EventHandler(this.btnRealRainProcess_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(209, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Output Location:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "HH:mm";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(243, 165);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 18;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(306, 215);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(192, 20);
            this.textBox1.TabIndex = 22;
            // 
            // dtpStartDay
            // 
            this.dtpStartDay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDay.Location = new System.Drawing.Point(37, 139);
            this.dtpStartDay.Name = "dtpStartDay";
            this.dtpStartDay.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDay.TabIndex = 15;
            // 
            // dtpEndDay
            // 
            this.dtpEndDay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDay.Location = new System.Drawing.Point(243, 139);
            this.dtpEndDay.Name = "dtpEndDay";
            this.dtpEndDay.Size = new System.Drawing.Size(200, 20);
            this.dtpEndDay.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(240, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "End Time:";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "HH:mm";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(37, 165);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.ShowUpDown = true;
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 123);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Start Time:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stationidDataGridViewTextBoxColumn,
            this.h2numberDataGridViewTextBoxColumn,
            this.stationnameDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.sTATIONBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(8, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(484, 114);
            this.dataGridView1.TabIndex = 1;
            // 
            // stationidDataGridViewTextBoxColumn
            // 
            this.stationidDataGridViewTextBoxColumn.DataPropertyName = "station_id";
            this.stationidDataGridViewTextBoxColumn.HeaderText = "station_id";
            this.stationidDataGridViewTextBoxColumn.Name = "stationidDataGridViewTextBoxColumn";
            this.stationidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // h2numberDataGridViewTextBoxColumn
            // 
            this.h2numberDataGridViewTextBoxColumn.DataPropertyName = "h2_number";
            this.h2numberDataGridViewTextBoxColumn.HeaderText = "h2_number";
            this.h2numberDataGridViewTextBoxColumn.Name = "h2numberDataGridViewTextBoxColumn";
            // 
            // stationnameDataGridViewTextBoxColumn
            // 
            this.stationnameDataGridViewTextBoxColumn.DataPropertyName = "station_name";
            this.stationnameDataGridViewTextBoxColumn.HeaderText = "station_name";
            this.stationnameDataGridViewTextBoxColumn.Name = "stationnameDataGridViewTextBoxColumn";
            this.stationnameDataGridViewTextBoxColumn.Width = 200;
            // 
            // sTATIONBindingSource
            // 
            this.sTATIONBindingSource.DataMember = "STATION";
            this.sTATIONBindingSource.DataSource = this.nEPTUNEDataSetBindingSource;
            // 
            // nEPTUNEDataSetBindingSource
            // 
            this.nEPTUNEDataSetBindingSource.DataSource = this.nEPTUNEDataSet;
            this.nEPTUNEDataSetBindingSource.Position = 0;
            // 
            // nEPTUNEDataSet
            // 
            this.nEPTUNEDataSet.DataSetName = "NEPTUNEDataSet";
            this.nEPTUNEDataSet.EnforceConstraints = false;
            this.nEPTUNEDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rAINSENSORBindingSource
            // 
            this.rAINSENSORBindingSource.DataMember = "RAIN_SENSOR";
            this.rAINSENSORBindingSource.DataSource = this.nEPTUNEDataSetBindingSource;
            // 
            // rainDS
            // 
            this.rainDS.DataSetName = "RainDSClass";
            this.rainDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.rainDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // qsDS1
            // 
            this.qsDS1.DataSetName = "qsDS";
            this.qsDS1.Locale = new System.Globalization.CultureInfo("en-US");
            this.qsDS1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rAIN_SENSORTableAdapter
            // 
            this.rAIN_SENSORTableAdapter.ClearBeforeFill = true;
            // 
            // sTATIONTableAdapter
            // 
            this.sTATIONTableAdapter.ClearBeforeFill = true;
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Location = new System.Drawing.Point(324, 290);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(173, 43);
            this.btnExportToExcel.TabIndex = 27;
            this.btnExportToExcel.Text = "Export To Excel File";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // frmMapEditing
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(697, 502);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMapEditing";
            this.Text = "Rainfall Interface";
            this.Load += new System.EventHandler(this.frmMapEditing_Load);
            this.Closed += new System.EventHandler(this.frmMapEditing_Unload);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tpRainfallTab.ResumeLayout(false);
            this.tpRainfallTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sTATIONBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nEPTUNEDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nEPTUNEDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rAINSENSORBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rainDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qsDS1)).EndInit();
            this.ResumeLayout(false);
		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            SplashScreen.ShowSplashScreen();
            Application.DoEvents();
            SplashScreen.SetStatus("Loading...");
            
            for (int h = 0; h < 1000000; h++)
            {
                SplashScreen.SetStatus("Loading...");
                SplashScreen.SetStatus("XXXXXXXXXX");
                
                
            }
            Application.Run(new frmMapEditing());
		}

        private void Form1_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
        {
            if (m_bLayoutCalled == false)
            {
                m_bLayoutCalled = true;
                m_dt = DateTime.Now;
                if (SplashScreen.SplashForm != null)
                    SplashScreen.SplashForm.Owner = this;
                this.Activate();
                SplashScreen.CloseForm();
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            TimeSpan ts = DateTime.Now.Subtract(m_dt);
            if (ts.TotalSeconds > 2) { }
                //this.Close();
        }

		private DataTable GetColumnData()
		{
			return graphData;
		}

		private static void AddText(System.IO.FileStream fs, string value) 
		{
			byte[] info = new UTF8Encoding(true).GetBytes(value);
			fs.Write(info, 0, info.Length);
		}
        private static void AddBinaryString(System.IO.FileStream fs, string value)
        {
            BinaryWriter bw = new BinaryWriter(fs);
            foreach (char c in value.ToCharArray())
            {
                bw.Write(c);
            }
        }
        private static void AddBinaryInt32(System.IO.FileStream fs, Int32 value)
        {
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(value);
        }
        private static void AddBinaryInt16(System.IO.FileStream fs, Int16 value)
        {
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(value);
        }
        private static void AddBinarySingle(System.IO.FileStream fs, Single value)
        {
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(value);
        }
		private void frmMapEditing_Load(object sender, System.EventArgs e)
		{
            SplashScreen.CloseForm();
            // TODO: This line of code loads data into the 'nEPTUNEDataSet.STATION' table. You can move, or remove it, as needed.
            this.sTATIONTableAdapter.Fill(this.nEPTUNEDataSet.STATION);
            // TODO: This line of code loads data into the 'nEPTUNEDataSet.RAIN_SENSOR' table. You can move, or remove it, as needed.
            this.rAIN_SENSORTableAdapter.Fill(this.nEPTUNEDataSet.RAIN_SENSOR);
			// Setup the application
			m_MapControl = (IMapControl3) axMapControl1.GetOcx();
			m_pMap = m_MapControl.Map;
			LoadLayers();
		}
		private void frmMapEditing_Unload(object sender, System.EventArgs e)
		{
			//Release COM objects 
			ESRI.ArcGIS.Utility.COMSupport.AOUninitialize.Shutdown();
		}

		private void OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
		{
		}
		private void OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
		{
		}
		private void OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
		{
		}
		private void LoadLayers()
		{
            string sAssemblyPath = Assembly.GetExecutingAssembly().Location;
            string sAssemblyFolder = System.IO.Path.GetDirectoryName(sAssemblyPath);
			string sFilePath1 = sAssemblyFolder + "\\..\\..\\lyr\\Quarter_Sections.lyr";
            string sFilePath2 = sAssemblyFolder + "\\..\\..\\lyr\\Sewer_Basins_PDX.lyr";

			ESRI.ArcGIS.Catalog.GxLayer sscGxLayer1;
			ESRI.ArcGIS.Catalog.GxLayer sscGxLayer2;
			sscGxLayer1 = new ESRI.ArcGIS.Catalog.GxLayerClass();
			sscGxLayer2 = new ESRI.ArcGIS.Catalog.GxLayerClass();

			ESRI.ArcGIS.Catalog.GxFile sscGxFile1;
			ESRI.ArcGIS.Catalog.GxFile sscGxFile2;
			sscGxFile1 = (ESRI.ArcGIS.Catalog.GxFile)sscGxLayer1;
			sscGxFile2 = (ESRI.ArcGIS.Catalog.GxFile)sscGxLayer2;
			sscGxFile1.Path = sFilePath1;
			sscGxFile2.Path = sFilePath2;

			//Which can QI to an IFeatureLayer...
			ESRI.ArcGIS.Carto.IFeatureLayer sscFL1;
			ESRI.ArcGIS.Carto.IFeatureLayer sscFL2;
			sscFL1 = (ESRI.ArcGIS.Carto.IFeatureLayer)sscGxLayer1.Layer;
			sscFL2 = (ESRI.ArcGIS.Carto.IFeatureLayer)sscGxLayer2.Layer;

			m_pMap.AddLayer(sscFL1);
			m_pMap.AddLayer(sscFL2);
			m_pCurrentLayer = m_pMap.get_Layer(1);
		}
        
        //SelectMouseDown is called when the user is selecing quartersections individually
        //with the mouse.
        private void SelectMouseDown(int x, int y)
		{
            ISelectionSet ArcQuarterSections;
			m_pCurrentLayer = m_pMap.get_Layer(1);
			// Searches the map for features at the given point in the current layer
			// and adds them to the selection (or removes them if they are already selected).
			//if we dont have a current layer, exit this function
			if (m_pCurrentLayer == null) return;
			//if there are no geometry features in the current layer, exit this function
			if ((IGeoFeatureLayer)m_pCurrentLayer == null) return;
      
			// Get the feature layer and class of the current layer
			IFeatureLayer pFeatureLayer = (IFeatureLayer)m_pCurrentLayer;
			IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
			//if there are no featureclasses in the featurelayer, exit this function
			if (pFeatureClass == null) return;
      
			// Get the mouse down position in map coordinates
			IActiveView pActiveView = (IActiveView)m_pMap;
			IPoint pPoint  = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
			IGeometry pGeometry = pPoint;

            //call up a Filter specific to this layer  
			ISpatialFilter pSpatialFilter = new SpatialFilter();
			pSpatialFilter.Geometry = pGeometry;
            pSpatialFilter.GeometryField = "SHAPE";

			pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

			//the geometry field will apparently be the name of the shapefield
			//query all of the objects with the shapetype
			IQueryFilter pFilter = pSpatialFilter;
     
			// place all of the objects of this shapetype into the cursor
			IFeatureCursor pCursor = pFeatureLayer.Search(pFilter, false);

			//load up the first object
			IFeature pFeature = pCursor.NextFeature();

			//as long as there are real objects in the cursor
			while (pFeature != null)
			{
				//if the shape/object now in pCursor is also in the quartersectionlist
                //then the qtrno must be removed from the quartersection list.
				if(QuarterSectionList.Contains(pFeature.get_Value(4).ToString()))
				{
					QuarterSectionList.Remove(pFeature.get_Value(4).ToString());
                }
                //if the shape now in the pCursor is not in the quartersection list
                //then the qtrno must be added to the quartersection list.
                else
                {
                    QuarterSectionList.Add(pFeature.get_Value(4).ToString(),pFeature.get_Value(4).ToString());
                }

                txtQSBulkInputEntry(int.Parse((string)pFeature.get_Value(4)));
                pFeature = pCursor.NextFeature();
            }
            m_pMap.ClearSelection();
            IFeature pFeature2;
            pCursor.Flush();
            string searchString = "QTRNO = '999999' ";
            foreach (string s in QuarterSectionList.Values)
            {
                searchString = searchString + " OR QTRNO = '" + s + "'";
            }
            IQueryFilter queryFilter2 = new QueryFilterClass();
            queryFilter2.SubFields = "QTRNO";
            queryFilter2.WhereClause = searchString;

            pCursor = pFeatureLayer.Search(queryFilter2, false);
            pFeature2 = pCursor.NextFeature();
            while (pFeature2 != null)
            {
                m_pMap.SelectFeature(m_pCurrentLayer, pFeature2);

                pFeature2 = pCursor.NextFeature();
            }
			pActiveView.Refresh();
		}                                                     

		private IFeatureCursor GetSelectedFeatures()
		{
			if (m_pCurrentLayer == null) return null;
      
			// If there are no features selected let the user know
			IFeatureSelection pFeatSel = (IFeatureSelection)m_pCurrentLayer;
			ISelectionSet pSelectionSet = pFeatSel.SelectionSet;
			if (pSelectionSet.Count == 0)
			{
				MessageBox.Show("No features are selected in the '" + m_pCurrentLayer.Name + "' layer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return null;
			}
			// Otherwise get all of the features back from the selection
			ICursor pCursor;
			pSelectionSet.Search(null, false, out pCursor);
			return (IFeatureCursor)pCursor;
		}
    
		private bool TestGeometryHit(double tolerance, IPoint pPoint, IFeature pFeature, ref IPoint pHitPoint, ref double hitDist, ref int partIndex, ref int vertexIndex, ref int vertexOffset, ref bool vertexHit)
		{
			// Function returns true if a feature's shape is hit and further defines
			// if a vertex lies within the tolerance
			bool bRetVal = false;
			IGeometry pGeom = (IGeometry)pFeature.Shape;
        
			IHitTest pHitTest = (IHitTest)pGeom;
			pHitPoint = new  ESRI.ArcGIS.Geometry.Point();
			bool bTrue  = true;
			// First check if a vertex was hit
			if (pHitTest.HitTest(pPoint, tolerance, esriGeometryHitPartType.esriGeometryPartVertex, pHitPoint, ref hitDist, ref partIndex, ref vertexIndex, ref bTrue))
			{
				bRetVal = true;
				vertexHit = true;
			}
				// Secondly check if a boundary was hit
			else if (pHitTest.HitTest(pPoint, tolerance, esriGeometryHitPartType.esriGeometryPartBoundary, pHitPoint, ref hitDist, ref partIndex, ref vertexIndex, ref bTrue))
			{
				bRetVal = true;
				vertexHit = false;
			}

			// Calculate offset to vertexIndex for multipatch geometries
			if (partIndex > 0 )
			{
				IGeometryCollection pGeomColn = (IGeometryCollection)pGeom;
				vertexOffset = 0;
				for (int i = 0; i < partIndex; i++)
				{
					IPointCollection pPointColn = (IPointCollection)pGeomColn.get_Geometry(i);
					vertexOffset = vertexOffset + pPointColn.PointCount;
				}
			}

			return bRetVal;
		}

		private double ConvertPixelsToMapUnits(IActiveView pActiveView , double pixelUnits)
		{
			// Uses the ratio of the size of the map in pixels to map units to do the conversion
			IPoint p1 = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.UpperLeft;
			IPoint p2 = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.UpperRight;
			int x1, x2, y1, y2;
			pActiveView.ScreenDisplay.DisplayTransformation.FromMapPoint(p1, out x1, out y1);
			pActiveView.ScreenDisplay.DisplayTransformation.FromMapPoint(p2, out x2, out y2);
			double pixelExtent = x2 - x1;
			double realWorldDisplayExtent = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width;
			double sizeOfOnePixel = realWorldDisplayExtent / pixelExtent;
			return pixelUnits * sizeOfOnePixel;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			d1 = dtpStartDate.Value.Date.AddMinutes(dtpStartTime.Value.TimeOfDay.TotalMinutes - dtpStartTime.Value.TimeOfDay.Minutes%5);
			d2 = dtpEndDate.Value.Date.AddMinutes(dtpEndTime.Value.TimeOfDay.TotalMinutes + (5-dtpEndTime.Value.TimeOfDay.Minutes%5));
			//smoother = 0;

			if(d1 > d2)
			{
				MessageBox.Show("Please make sure your start date is earlier than your end date.");
			}
			else
			{
				if(FileLocationChosen == false)
				{
					MessageBox.Show("Please choose an output file.");
				}
				else
				{
                    txtQSBulkInputParse();
					if(QuarterSectionList.Count == 0)
					{
						MessageBox.Show("Please select at least one quartersection.");
					}
					else
					{
						this.Cursor = Cursors.WaitCursor;
						rainTools.virtmain(d1, 
                                            d2, 
                                            NumberOfGages, 
                                            NumberOfTimeStepsBetweenStartAndEnd,
                                            QuarterSectionList,
                                            QuarterSectionGageInfo,
                                            lblInstructions1.Text,
                                            oleDbConnection2,
                                            oleDbDataAdapter1,
                                            sqlConnection1,
                                            sqlDataAdapter1,
                                            rainDS,
                                            qsDS1,
                                            txtDestination.Text,
                                            largeRainArray,
                                            checkBinaryOutput.Checked,
                                            graphData);
                        this.Cursor = Cursors.Default;
					}
				}
			}
		}

		private void btnSaveFile_Click(object sender, System.EventArgs e)
		{
			saveFileDialog1.Title = "Specify Destination Filename";
			saveFileDialog1.Filter = "Text Files|*.txt";
			saveFileDialog1.FilterIndex = 1;
			saveFileDialog1.OverwritePrompt = true;	

			if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
			{
				txtDestination.Text = saveFileDialog1.FileName;
				FileLocationChosen = true;
			}
		}
		private void dateTimePicker1_ValueChanged(object sender, System.EventArgs e)
		{
		}
		private void dtpStartTime_ValueChanged(object sender, System.EventArgs e)
		{
		}
		private void sqlConnection1_InfoMessage(object sender, System.Data.SqlClient.SqlInfoMessageEventArgs e)
		{
		}
		private void sqlDataAdapter1_RowUpdated(object sender, System.Data.SqlClient.SqlRowUpdatedEventArgs e)
		{
		}
		private void buttonZoomOut_Click(object sender, System.EventArgs e)
		{
			// Zoom out
			IActiveView pActiveView = (IActiveView)m_pMap; 
			IEnvelope pEnvelope = pActiveView.Extent; 
			ESRI.ArcGIS.Geometry.Point pnt = new ESRI.ArcGIS.Geometry.Point();
			IPoint iPnt = pnt;
			iPnt.X = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.XMin +(pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width/2);
			iPnt.Y = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.YMin + (pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Height/2);
			pEnvelope.CenterAt(iPnt);
			pEnvelope.Expand(1.1, 1.1, true);
			pActiveView.Extent = pEnvelope;
			pActiveView.Refresh();
		}

		private void buttonZoomIn_Click(object sender, System.EventArgs e)
		{
			// Zoom in
			IActiveView pActiveView = (IActiveView)m_pMap; 
			IEnvelope pEnvelope = pActiveView.Extent; 
			ESRI.ArcGIS.Geometry.Point pnt = new ESRI.ArcGIS.Geometry.Point();
			IPoint iPnt = pnt;
			iPnt.X = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.XMin +(pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width/2);
			iPnt.Y = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.YMin + (pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Height/2);
			pEnvelope.CenterAt(iPnt);
			pEnvelope.Expand(.9, .9, true);
			pActiveView.Extent = pEnvelope;
			pActiveView.Refresh();
		}

		private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
		{
			//SelectMouseDown(e.x, e.y);

            if (!checkSelectByBasin.Checked)
                SelectMouseDown(e.x, e.y);
            else
                SelectMouseDownByBasin(e.x, e.y);
		}

		private void SelectMouseDownByBasin(int x, int y)
		{
            ISelectionSet ArcQuarterSections;
            //layer 0 should be the layer of basins
            m_pCurrentLayer = m_pMap.get_Layer(0);
            // Searches the map for features at the given point in the current layer
            // and adds them to the selection (or removes them if they are already selected).
            //if we dont have a current layer, exit this function
            if (m_pCurrentLayer == null) return;
            //if there are no geometry features in the current layer, exit this function
            if ((IGeoFeatureLayer)m_pCurrentLayer == null) return;

            // Get the feature layer and class of the current layer
            IFeatureLayer pFeatureLayer = (IFeatureLayer)m_pCurrentLayer;
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            //if there are no featureclasses in the featurelayer, exit this function
            if (pFeatureClass == null) return;

            // Get the mouse down position in map coordinates
            IActiveView pActiveView = (IActiveView)m_pMap;
            IPoint pPoint = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            IGeometry pGeometry = pPoint;

            //call up a Filter specific to this layer  
            ISpatialFilter pSpatialFilter = new SpatialFilter();
            pSpatialFilter.Geometry = pGeometry;
            pSpatialFilter.GeometryField = "SHAPE";

            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

            //the geometry field will apparently be the name of the shapefield
            //query all of the objects with the shapetype
            IQueryFilter pFilter = pSpatialFilter;

            // place all of the objects of this shapetype into the cursor
            IFeatureCursor pCursor = pFeatureLayer.Search(pFilter, false);

            //load up the first object
            IFeature pFeature = pCursor.NextFeature();


            ///This is where the quartersections that intersect the basin should be selected.
            ///****************************************
            ///****************************************
            m_pCurrentLayer = m_pMap.get_Layer(1);
            // Searches the map for features at the given point in the current layer
            // and adds them to the selection (or removes them if they are already selected).
            //if we dont have a current layer, exit this function
            if (m_pCurrentLayer == null) return;
            //if there are no geometry features in the current layer, exit this function
            if ((IGeoFeatureLayer)m_pCurrentLayer == null) return;

            // Get the feature layer and class of the current layer
            IFeatureLayer pFeatureLayer2 = (IFeatureLayer)m_pCurrentLayer;
            IFeatureClass pFeatureClass2 = pFeatureLayer2.FeatureClass;
            //if there are no featureclasses in the featurelayer, exit this function
            if (pFeatureClass2 == null) return;

            // Get the mouse down position in map coordinates
            //IActiveView pActiveView = (IActiveView)m_pMap;
            //ESRI.ArcGIS.Geometry.IPolygon pPolygon = pFeature.Shape;
            IGeometry pGeometry2 = pFeature.Shape;//pPolygon;

            //call up a Filter specific to this layer  
            ISpatialFilter pSpatialFilter2 = new SpatialFilter();
            pSpatialFilter2.Geometry = pGeometry2;
            pSpatialFilter2.GeometryField = "SHAPE";

            pSpatialFilter2.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

            //the geometry field will apparently be the name of the shapefield
            //query all of the objects with the shapetype
            IQueryFilter pFilter2 = pSpatialFilter2;

            // place all of the objects of this shapetype into the cursor
            IFeatureCursor pCursor2 = pFeatureLayer2.Search(pFilter2, false);

            //load up the first object
            IFeature pFeature2 = pCursor2.NextFeature();
            ///*********************************************************
            ///*********************************************************


            //as long as there are real objects in the cursor
            while (pFeature2 != null)
            {
                //if the shape/object now in pCursor is also in the quartersectionlist
                //then the qtrno must be removed from the quartersection list.
                if (QuarterSectionList.Contains(pFeature2.get_Value(4).ToString()))
                {
                    QuarterSectionList.Remove(pFeature2.get_Value(4).ToString());
                }
                //if the shape now in the pCursor is not in the quartersection list
                //then the qtrno must be added to the quartersection list.
                else
                {
                    QuarterSectionList.Add(pFeature2.get_Value(4).ToString(), pFeature2.get_Value(4).ToString());
                }

                txtQSBulkInputEntry(int.Parse((string)pFeature2.get_Value(4)));
                pFeature2 = pCursor2.NextFeature();
            }
            m_pMap.ClearSelection();
            IFeature pFeature3;
            pCursor2.Flush();
            string searchString = "QTRNO = '999999' ";
            foreach (string s in QuarterSectionList.Values)
            {
                searchString = searchString + " OR QTRNO = '" + s + "'";
            }
            IQueryFilter queryFilter3 = new QueryFilterClass();
            queryFilter3.SubFields = "QTRNO";
            queryFilter3.WhereClause = searchString;

            pCursor2 = pFeatureLayer2.Search(queryFilter3, false);
            pFeature3 = pCursor2.NextFeature();
            while (pFeature3 != null)
            {
                m_pMap.SelectFeature(m_pCurrentLayer, pFeature3);

                pFeature3 = pCursor2.NextFeature();
            }
            pActiveView.Refresh();
		}

        public void txtQSBulkInputParse()
        {
            //get the numbers from txtQSBulkInput and place them
            //into QuartersectionList.  The numbers should be allowed
            //to be seperated by any non-digit character, so
            //commas, newlines, spaces should all be allowed in
            //whatever combination.  Simply search for the next 
            //set of digits and assume they are in proper quartersection
            //format.
            //get the number of characters contained in txtQSBulkInput
            string sParseString = txtQSBulkInput.Text;
            string sQuartersectionString = "";
            int iNumberOfCharactersToParse = sParseString.Length;
            int iQuartersection = 0;
            bool bFirstNumberFound = false;

            lblInstructions1.Text = "";

            QuarterSectionList.Clear();

            for (int i = 0; i < iNumberOfCharactersToParse; i++)
            {
                //If we have found a digit, then it must be the start of a
                //Quartersection identifier
                if (Char.IsDigit(sParseString[i]))
                {
                    //parse four digits into an integer and place the integer
                    //into QuartersectionList.  Also, we must advance the
                    //variable 'i' 4 places to intercept the next number.
                    sQuartersectionString = sParseString.Substring(i,4);
                    iQuartersection = int.Parse(sQuartersectionString);
                    QuarterSectionList.Add(sQuartersectionString, sQuartersectionString);
                    i = i + 3;
                    if (bFirstNumberFound == false)
                    {
                        lblInstructions1.Text = "QTRNO = '" + sQuartersectionString + "' ";
                    }
                    else
                    {
                        lblInstructions1.Text = lblInstructions1.Text + " OR QTRNO = '" + sQuartersectionString + "' ";
                    }

                    bFirstNumberFound = true;
                }
                //if the character is not a digit, then nothing needs to be done.
                //the loop parameters will advance i and check the next character
                //to see if it is a digit.

            }
            return;
        }

        private void txtQSBulkInputEntry(int QSNumber)
        {
            //get the number of characters contained in txtQSBulkInput
            string sParseString = txtQSBulkInput.Text;

            //Search the main string for a match of the QSNumber
            //If the number already exists, get rid of it.
            //If it doesnt exist, add it
            if (sParseString.Contains("," + QSNumber.ToString() + ","))
            {
                sParseString = sParseString.Replace(","+QSNumber.ToString()+",", ",");
            }
            else if (sParseString.Contains(QSNumber.ToString() + ","))
            {
                sParseString = sParseString.Replace(QSNumber.ToString() + ",", "");
            }
            else if (sParseString.Contains("," + QSNumber.ToString()))
            {
                sParseString = sParseString.Replace("," + QSNumber.ToString(), "");
            }
            else if (sParseString.Contains(QSNumber.ToString()))
            {
                sParseString = sParseString.Replace(QSNumber.ToString(), "");
            }
            else if (sParseString.Length < 1)
            {
                sParseString = QSNumber.ToString();
            }
            else
            {
                sParseString = sParseString + "," + QSNumber.ToString();
            }

            txtQSBulkInput.Text = sParseString;

            return;
        }

        private void button1_Click_1(object sender, EventArgs e)
		{
            if (txtQSBulkInput.Text == "") return;
			if (m_pCurrentLayer == null) return;
			if ((IGeoFeatureLayer)m_pCurrentLayer == null) return;
      
			// Get the feature layer and class of the current layer
            //parse txtQSBulkInput.Text, empty QuartersectionList and
            //fill QuartersectionList with the values from txtQSBulkInput.Text
            txtQSBulkInputParse();
		}

        /// <summary>
        /// setZeroRainPeriod will place zero values for rainfall during the time indicated
        /// if there is no more downtime associated with a raingage, no matching record 
        /// in the rainfall that has been registered, and the current time is still before
        /// the end time of the raingage
        /// </summary>
        /// <param name="DownTimeIndex">This identifies the position of the static raingage array we are looking at</param>
        /// <param name="thisRow">thisRow is an integer that identifies the current rainfall record we are looking at </param>
        /// <param name="x">x is the time inteval, which is a start date and end date, of a period of downtime</param>
        /// <param name="h2Index">h2Index is an integer which identifies which gage we are considering.  This 
        /// value identifies the index of the gage in the h2List query, and not the h2Number itself</param>
        /// <param name="currentGagesInfo">currentgagesInfo is a gageinfo object which is simply used as a 
        /// memory trick to save access times and to make the code look cleaner</param>
        bool setZeroRainPeriod(ref int DownTimeIndex, ref int thisRow, ref TimeInterval x, ref int h2Index, ref gageinfo currentGagesInfo)
        {
            bool returnValue = false;

            if (DownTimeIndex + 1 >= currentGagesInfo.DownTimeList.Count)
            {
                largeRainArray[h2Index][thisRow] = 0.0;
                thisRow++;
                DownTimeIndex = 0;
                returnValue = true; ;
            }
            else
            {
                x = (TimeInterval)currentGagesInfo.DownTimeList.GetByIndex(++DownTimeIndex);
            }

            return returnValue;
        }

        void basicDownTimeSearch(ref DateTime dateTimeIterator, ref gageinfo currentGagesInfo, ref int h2Index, ref int thisRow)
        {
            int DownTimeIndex = 0;
            TimeInterval x;

            x = (TimeInterval)currentGagesInfo.DownTimeList.GetByIndex(DownTimeIndex);
            //if the date is not between the start date and end dates of the gage, then set the reading to 200
            if (dateTimeIterator < currentGagesInfo.startDate || dateTimeIterator > currentGagesInfo.endDate)
            {
                largeRainArray[h2Index][thisRow] = 200.0;
                thisRow++;
            }
            else
            {
                //if the gage cannot be down (no reading, but the downtime index is 
                //greater than the number of downtimes) then the reading must be zero.
                if (DownTimeIndex >= currentGagesInfo.DownTimeList.Count)
                {
                    largeRainArray[h2Index][thisRow] = 0.0;
                    thisRow++;
                }

                //if the gage might be down, check the downtime list for that gage
                //and if the date falls during a downtime, then set the reading to 200.
                //Otherwise, set the reading to zero.
                intenseDownTimeSearch(ref DownTimeIndex, ref currentGagesInfo, ref dateTimeIterator, ref x, ref h2Index, ref thisRow);
            }
        }

        /// <summary>
        /// intenseDownTimeSearch should probably be renamed.  This function does a full search of the
        /// downtimes for the gage in question and applies zeros (for no rain) or 200's (an identifier for downtime)
        /// to the largeRainArray.
        /// </summary>
        /// <param name="DownTimeIndex">This identifies the position of the static raingage array we are looking at</param>
        /// <param name="currentGagesInfo">currentgagesInfo is a gageinfo object which is simply used as a 
        /// memory trick to save access times and to make the code look cleaner</param>
        /// <param name="dateTimeIterator">dateTimeIterator is a dateTime value that represents the current time step.</param>
        /// <param name="x">x is the time inteval, which is a start date and end date, of a period of downtime</param>
        /// <param name="h2Index">h2Index is an integer which identifies which gage we are considering.  This 
        /// value identifies the index of the gage in the h2List query, and not the h2Number itself</param>
        /// <param name="thisRow">thisRow is an integer that identifies the current rainfall record we are looking at</param>
        void intenseDownTimeSearch(ref int DownTimeIndex, ref gageinfo currentGagesInfo, ref DateTime dateTimeIterator, ref TimeInterval x, ref int h2Index, ref int thisRow)
        {
            while (DownTimeIndex < currentGagesInfo.DownTimeList.Count)
            {
                //if the current time step is during a downtime, enter 200 into the array position
                if (dateTimeIterator >= x.IntervalStart)
                {
                    if (dateTimeIterator <= x.IntervalEnd)
                    {
                        largeRainArray[h2Index][thisRow] = 200.0;
                        thisRow++;
                        break;
                    }
                    // or keep looking if the dateTimeIterator is greater than the downtime
                    else
                    {
                        //if there aren't any other downtimes, then this is a zero rain period
                        if (setZeroRainPeriod(ref DownTimeIndex, ref thisRow, ref x, ref h2Index, ref currentGagesInfo) == true)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    largeRainArray[h2Index][thisRow] = 0.0;
                    DownTimeIndex = 0;
                    thisRow++;
                    break;
                }
            }
        }

        private void btnRealRainProcess_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DateTime d1 = dtpStartDay.Value.Date.AddMinutes(dtpStartTime.Value.TimeOfDay.TotalMinutes - dtpStartTime.Value.TimeOfDay.Minutes % 5);
                DateTime d2 = dtpEndDay.Value.Date.AddMinutes(dtpEndTime.Value.TimeOfDay.TotalMinutes + (5 - dtpEndTime.Value.TimeOfDay.Minutes % 5));

                if (d1 > d2)
                {
                    MessageBox.Show("Please make sure your start date is earlier than your end date.");
                }
                else
                {
                        int test = 0;
                        this.Cursor = Cursors.WaitCursor;
                        try
                        {
                            //see if the user has selected a row for the sensor
                            test = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show("Please select a valid station.  Click on the grey box to the left of the station_id column and ensure that the entire row is highlighted.");
                            test = -1;               
                        }
                        if (test != -1)
                        {
                            try
                            {
                                //get rid of the 'seconds' portion of the datetime
                                CreateRainReport(saveFileDialog1.FileName, d1.AddSeconds(-d1.Second), d2.AddSeconds(-d2.Second));
                                MessageBox.Show("Process complete!");
                            }
                            catch (Exception exp)
                            {
                                MessageBox.Show("An error occurred while attempting to load the file. The error is:"
                                                + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
                            }
                        }
                        this.Cursor = Cursors.Arrow;
                }
            }
        }

        private void CreateRainReport(string path1, DateTime sdate, DateTime edate)
        {
            //Data set and table adapter for the variable minute rain gauges
            NEPTUNEDataSetTableAdapters.DataTable1TableAdapter theRainTableAdapter =
                new VirtualRain.NEPTUNEDataSetTableAdapters.DataTable1TableAdapter();
            NEPTUNEDataSet.DataTable1DataTable theRainTable =
                new NEPTUNEDataSet.DataTable1DataTable();
            int timeStep = (int)nudTimeStep.Value;

            sdate = sdate.AddMinutes(-(sdate.Minute % timeStep)); 

            NEPTUNEDataSetTableAdapters.V_MODEL_RAIN_DOWNTIMETableAdapter theDownTimeTableAdapter =
                new VirtualRain.NEPTUNEDataSetTableAdapters.V_MODEL_RAIN_DOWNTIMETableAdapter();
            NEPTUNEDataSet.V_MODEL_RAIN_DOWNTIMEDataTable theDownTimeTable =
                new NEPTUNEDataSet.V_MODEL_RAIN_DOWNTIMEDataTable();

            theRainTableAdapter.Fill(theRainTable, edate, timeStep, (int)dataGridView1.SelectedRows[0].Cells[0].Value, sdate);
            theDownTimeTableAdapter.FillByStationID(theDownTimeTable, (int)dataGridView1.SelectedRows[0].Cells[0].Value, sdate, edate);

            DateTime DateStep;
            int RainRow = 0;
            int DownRow = 0;
            int maxRows = theRainTable.Rows.Count;
            int DownRows = theDownTimeTable.Rows.Count;
            bool DataFound = false;
            System.Data.DataRow theRainRow;
            System.Data.DataRow theDownRow;

            if (File.Exists(path1))
            {
                File.Delete(path1);
            }

            System.IO.FileStream fs = File.Create(path1);

            //for each time step
            DateStep = sdate;

            System.TimeSpan diff1 = edate.Subtract(sdate);
            long NumberOfTimeStepsBetweenStartAndEnd = (((long)diff1.TotalMinutes) / timeStep) + 1;

            if (maxRows != 0)
            {
                theRainRow = theRainTable.Rows[0];
                for (int j = 0; j < NumberOfTimeStepsBetweenStartAndEnd; j++)
                {
                    DataFound = false;
                    //if the datetime in the rainDS is the same as the datetime in DateStep
                    //then get the rainfall sum and increment DateStep
                    if (RainRow < maxRows)
                    {
                        theRainRow = theRainTable.Rows[RainRow];
                    }
                    //if the current DateStep value is during one of the station's
                    //downtimes, then print "DOWN" as the value of the rainfall
                    //if 'ThereIsDownTime' == true, then print "DOWN"
                    //This means I will need to select the downtime for this
                    //raingage and place it in a new dataset?

                    if (String.Compare((string.Format("{0:MM/dd/yyyy HH:mm},", theRainRow["date_time"])), (string.Format("{0:MM/dd/yyyy HH:mm},", DateStep))) == 0)
                    {
                        
                        AddText(fs, (string.Format("{0:MM/dd/yyyy HH:mm},", DateStep))
                            + theRainRow["sum_inches"].ToString() + ",\r\n");

                        RainRow++;
                        DataFound = true;
                    }
                    else if (DownRows > 0)
                    {
                        for (DownRow = 0; DownRow < DownRows; DownRow++)
                        {
                            if (DateTime.Compare(DateStep, (DateTime)theDownTimeTable.Rows[DownRow]["start_date"]) >= 0 &&
                                DateTime.Compare(DateStep, (DateTime)theDownTimeTable.Rows[DownRow]["end_date"]) <= 0 &&
                                DataFound == false)
                            {
                                AddText(fs, (string.Format("{0:MM/dd/yyyy HH:mm},", DateStep))
                                    + "DOWN,\r\n");
                                DataFound = true;
                            }
                        }
                    }
                    if(DataFound == false)
                    {
                        if (cbReportZeroRain.Checked)
                        {
                        AddText(fs, (string.Format("{0:MM/dd/yyyy HH:mm},", DateStep))
                            + "0,\r\n");
                        }
                    }
                    DateStep = DateStep.AddMinutes(timeStep);
                }
                fs.Close();

                //CreateExcelFile(sdate, edate, maxRows, path1);
            }
            else 
            {
                MessageBox.Show("There is no rainfall recorded by that monitor during the specified interval");
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            //frmMapEditing.textBox2.Text = frmMapEditing.saveFileDialog1.FileName;
            textBox1.Text = saveFileDialog1.FileName;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DateTime d1 = dtpStartDay.Value.Date.AddMinutes(dtpStartTime.Value.TimeOfDay.TotalMinutes - dtpStartTime.Value.TimeOfDay.Minutes % 5);
                DateTime d2 = dtpEndDay.Value.Date.AddMinutes(dtpEndTime.Value.TimeOfDay.TotalMinutes + (5 - dtpEndTime.Value.TimeOfDay.Minutes % 5));

                if (d1 > d2)
                {
                    MessageBox.Show("Please make sure your start date is earlier than your end date.");
                }
                else
                {
                    int test = 0;
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        //see if the user has selected a row for the sensor
                        test = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Please select a valid station.  Click on the grey box to the left of the station_id column and ensure that the entire row is highlighted.");
                        test = -1;
                    }
                    if (test != -1)
                    {
                        try
                        {
                            //get rid of the 'seconds' portion of the datetime
                            VirtualRain.ExcelHelper.CreateRainReport(saveFileDialog1.FileName, d1.AddSeconds(-d1.Second), d2.AddSeconds(-d2.Second), cbReportZeroRain.Checked, (int)nudTimeStep.Value, dataGridView1);
                            MessageBox.Show("Process complete!");
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show("An error occurred while attempting to load the file. The error is:"
                                            + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
                        }
                    }
                    this.Cursor = Cursors.Arrow;
                }
            }
        }
	}
}

