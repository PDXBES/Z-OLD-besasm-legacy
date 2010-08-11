using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Data.SqlClient ;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Carto;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using DocManager;
using DocClicker;

namespace ExtnHelpers
{
  [Guid("b9d6a018-3bd6-42f3-ace4-7a0fe5a02d6b")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("ExtnHelpers.TOCDocClicker")]
  public partial class TOCDocClicker : UserControl, IContentsView
  {
      #region "Global Variables defined"


      private IApplication m_application;
      private IMap m_map;
      private bool m_visible;
      private object m_contextItem;
      private CheckedListBox clbSearchLayers;
      private TextBox txtName;
      private Label lblDocName;
      private Label lblEndDate;
      private Label lblStartDate;
      private DateTimePicker dateTimePicker1;
      private DateTimePicker dateTimePicker2;
      private ListBox lbLog;
      private DataGridView grdResults;
      private Label lblResultsGrid;
      private Label lblSearchCategories;
      private System.Windows.Forms.Button btnSearch;
      private System.Windows.Forms.Button btnOpen;
      private System.Windows.Forms.Button btnDownload;
      private System.Windows.Forms.Button btnLocate;
      private BindingSource bindingSource1;
      private IContainer components;
      private bool m_isProcessEvents;
      private DataGridViewCheckBoxColumn ID;
      private DocManager.DocMgrSearch m_docSearch;
      private DocManager.clsData m_DataClass;
      private FolderBrowserDialog folderBrowserDialog1;
      private Logger m_Logger;
      private ArcGisClass GISClass;
      private dbConnection m_Connection;
      private System.Data.SqlClient.SqlDataReader m_DataSet;
      private DataSet m_DS;
      private DataTable m_DT;
      private DataTable m_dtIndex;
      private DataTable m_dtDoc;
      private System.Windows.Forms.Button btnIndex;
      private TabControl tabControl1;
      private TabPage tabPage1;
      private TabPage tabPage2;
      private ListBox lbDoc;
      private System.Windows.Forms.Button btnClear;
      private Label lblContent;
      private TextBox txtContent;
      private DataTable m_TempIndex;

      #endregion

      #region COM Registration Function(s)
      [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ContentsViews.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ContentsViews.Unregister(regKey);

        }

        #endregion
        #endregion


        public TOCDocClicker()
        {
            InitializeComponent();
            if (m_docSearch == null)
            {
                m_docSearch = new DocMgrSearch(m_Logger);
                
                
            }

        }


        #region "IContentsView Implementations"

        bool IContentsView.Visible
        {
            get { return m_visible; }
            set { m_visible = value; }
        }
        string IContentsView.Name { get { return "DocClicker"; } }
        int IContentsView.hWnd { get { return this.Handle.ToInt32(); } }

        object IContentsView.ContextItem //last right-clicked item
        {
            get { return m_contextItem; }
            set { }//Not implemented
        }

        void IContentsView.Activate(int parentHWnd, IMxDocument Document)
        {
            if (m_application == null)
            {
                m_application = ((IDocument)Document).Parent;
            }
        }
        void IContentsView.Refresh(object item)
        {
            if (item != this)
            {

            }
        }
        void IContentsView.Deactivate()
        {
            //Any clean up? 
        }

        void IContentsView.AddToSelectedItems(object item) { }
        object IContentsView.SelectedItem
        {
          get
          {
            return null;
          }
          set
          {
            //Not implemented
          }
        }
        bool IContentsView.ProcessEvents { set { m_isProcessEvents = value; } }
        void IContentsView.RemoveFromSelectedItems(object item) { }
        bool IContentsView.ShowLines
        {
          get { return false; }//tvwLayer.ShowLines; }
          set { }//tvwLayer.ShowLines = value; }
        }
        #endregion



    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.clbSearchLayers = new System.Windows.Forms.CheckedListBox();
        this.txtName = new System.Windows.Forms.TextBox();
        this.lblDocName = new System.Windows.Forms.Label();
        this.lblEndDate = new System.Windows.Forms.Label();
        this.lblStartDate = new System.Windows.Forms.Label();
        this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
        this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
        this.lbLog = new System.Windows.Forms.ListBox();
        this.grdResults = new System.Windows.Forms.DataGridView();
        this.ID = new System.Windows.Forms.DataGridViewCheckBoxColumn();
        this.lblResultsGrid = new System.Windows.Forms.Label();
        this.lblSearchCategories = new System.Windows.Forms.Label();
        this.btnSearch = new System.Windows.Forms.Button();
        this.btnOpen = new System.Windows.Forms.Button();
        this.btnDownload = new System.Windows.Forms.Button();
        this.btnLocate = new System.Windows.Forms.Button();
        this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
        this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
        this.btnIndex = new System.Windows.Forms.Button();
        this.tabControl1 = new System.Windows.Forms.TabControl();
        this.tabPage1 = new System.Windows.Forms.TabPage();
        this.lbDoc = new System.Windows.Forms.ListBox();
        this.tabPage2 = new System.Windows.Forms.TabPage();
        this.btnClear = new System.Windows.Forms.Button();
        this.lblContent = new System.Windows.Forms.Label();
        this.txtContent = new System.Windows.Forms.TextBox();
        ((System.ComponentModel.ISupportInitialize)(this.grdResults)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
        this.tabControl1.SuspendLayout();
        this.tabPage1.SuspendLayout();
        this.tabPage2.SuspendLayout();
        this.SuspendLayout();
        // 
        // clbSearchLayers
        // 
        this.clbSearchLayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.clbSearchLayers.FormattingEnabled = true;
        this.clbSearchLayers.Items.AddRange(new object[] {
            "Polygons",
            "Points"});
        this.clbSearchLayers.Location = new System.Drawing.Point(0, 141);
        this.clbSearchLayers.Name = "clbSearchLayers";
        this.clbSearchLayers.Size = new System.Drawing.Size(292, 79);
        this.clbSearchLayers.TabIndex = 4;
        // 
        // txtName
        // 
        this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.txtName.Location = new System.Drawing.Point(6, 16);
        this.txtName.Name = "txtName";
        this.txtName.Size = new System.Drawing.Size(282, 20);
        this.txtName.TabIndex = 0;
        // 
        // lblDocName
        // 
        this.lblDocName.AutoSize = true;
        this.lblDocName.Location = new System.Drawing.Point(3, -1);
        this.lblDocName.Name = "lblDocName";
        this.lblDocName.Size = new System.Drawing.Size(87, 13);
        this.lblDocName.TabIndex = 26;
        this.lblDocName.Text = "Document Name";
        // 
        // lblEndDate
        // 
        this.lblEndDate.AutoSize = true;
        this.lblEndDate.Location = new System.Drawing.Point(114, 80);
        this.lblEndDate.Name = "lblEndDate";
        this.lblEndDate.Size = new System.Drawing.Size(52, 13);
        this.lblEndDate.TabIndex = 27;
        this.lblEndDate.Text = "End Date";
        // 
        // lblStartDate
        // 
        this.lblStartDate.AutoSize = true;
        this.lblStartDate.Location = new System.Drawing.Point(3, 80);
        this.lblStartDate.Name = "lblStartDate";
        this.lblStartDate.Size = new System.Drawing.Size(55, 13);
        this.lblStartDate.TabIndex = 28;
        this.lblStartDate.Text = "Start Date";
        // 
        // dateTimePicker1
        // 
        this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        this.dateTimePicker1.Location = new System.Drawing.Point(6, 96);
        this.dateTimePicker1.Name = "dateTimePicker1";
        this.dateTimePicker1.Size = new System.Drawing.Size(100, 20);
        this.dateTimePicker1.TabIndex = 2;
        // 
        // dateTimePicker2
        // 
        this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        this.dateTimePicker2.Location = new System.Drawing.Point(117, 96);
        this.dateTimePicker2.Name = "dateTimePicker2";
        this.dateTimePicker2.Size = new System.Drawing.Size(96, 20);
        this.dateTimePicker2.TabIndex = 3;
        // 
        // lbLog
        // 
        this.lbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.lbLog.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lbLog.FormattingEnabled = true;
        this.lbLog.ItemHeight = 14;
        this.lbLog.Location = new System.Drawing.Point(3, 4);
        this.lbLog.Name = "lbLog";
        this.lbLog.Size = new System.Drawing.Size(275, 200);
        this.lbLog.TabIndex = 12;
        // 
        // grdResults
        // 
        this.grdResults.AllowUserToAddRows = false;
        this.grdResults.AllowUserToDeleteRows = false;
        this.grdResults.AllowUserToOrderColumns = true;
        this.grdResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.grdResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.grdResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID});
        this.grdResults.Location = new System.Drawing.Point(0, 268);
        this.grdResults.Name = "grdResults";
        this.grdResults.RowHeadersWidth = 10;
        this.grdResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.grdResults.Size = new System.Drawing.Size(292, 139);
        this.grdResults.TabIndex = 6;
        this.grdResults.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdResults_CellMouseClick);
        this.grdResults.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdResults_CellDoubleClick);
        // 
        // ID
        // 
        this.ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
        this.ID.FalseValue = "0";
        this.ID.HeaderText = "";
        this.ID.Name = "ID";
        this.ID.TrueValue = "1";
        this.ID.Width = 5;
        // 
        // lblResultsGrid
        // 
        this.lblResultsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.lblResultsGrid.AutoSize = true;
        this.lblResultsGrid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblResultsGrid.Location = new System.Drawing.Point(3, 252);
        this.lblResultsGrid.Name = "lblResultsGrid";
        this.lblResultsGrid.Size = new System.Drawing.Size(76, 13);
        this.lblResultsGrid.TabIndex = 29;
        this.lblResultsGrid.Text = "Results Grid";
        // 
        // lblSearchCategories
        // 
        this.lblSearchCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.lblSearchCategories.AutoSize = true;
        this.lblSearchCategories.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblSearchCategories.Location = new System.Drawing.Point(3, 123);
        this.lblSearchCategories.Name = "lblSearchCategories";
        this.lblSearchCategories.Size = new System.Drawing.Size(89, 13);
        this.lblSearchCategories.TabIndex = 17;
        this.lblSearchCategories.Text = "Search Layers";
        // 
        // btnSearch
        // 
        this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnSearch.Location = new System.Drawing.Point(150, 226);
        this.btnSearch.Name = "btnSearch";
        this.btnSearch.Size = new System.Drawing.Size(63, 23);
        this.btnSearch.TabIndex = 5;
        this.btnSearch.Text = "Search";
        this.btnSearch.UseVisualStyleBackColor = true;
        this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
        // 
        // btnOpen
        // 
        this.btnOpen.Location = new System.Drawing.Point(4, 413);
        this.btnOpen.Name = "btnOpen";
        this.btnOpen.Size = new System.Drawing.Size(63, 23);
        this.btnOpen.TabIndex = 7;
        this.btnOpen.Text = "Open";
        this.btnOpen.UseVisualStyleBackColor = true;
        this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
        // 
        // btnDownload
        // 
        this.btnDownload.Location = new System.Drawing.Point(75, 413);
        this.btnDownload.Name = "btnDownload";
        this.btnDownload.Size = new System.Drawing.Size(63, 23);
        this.btnDownload.TabIndex = 8;
        this.btnDownload.Text = "Download";
        this.btnDownload.UseVisualStyleBackColor = true;
        this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
        // 
        // btnLocate
        // 
        this.btnLocate.Location = new System.Drawing.Point(151, 413);
        this.btnLocate.Name = "btnLocate";
        this.btnLocate.Size = new System.Drawing.Size(63, 23);
        this.btnLocate.TabIndex = 9;
        this.btnLocate.Text = "Locate";
        this.btnLocate.UseVisualStyleBackColor = true;
        this.btnLocate.Click += new System.EventHandler(this.btnLocate_Click);
        // 
        // btnIndex
        // 
        this.btnIndex.Location = new System.Drawing.Point(225, 413);
        this.btnIndex.Name = "btnIndex";
        this.btnIndex.Size = new System.Drawing.Size(63, 23);
        this.btnIndex.TabIndex = 10;
        this.btnIndex.Text = "Index";
        this.btnIndex.UseVisualStyleBackColor = true;
        this.btnIndex.Click += new System.EventHandler(this.btnIndex_Click);
        // 
        // tabControl1
        // 
        this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.tabControl1.Controls.Add(this.tabPage1);
        this.tabControl1.Controls.Add(this.tabPage2);
        this.tabControl1.Location = new System.Drawing.Point(0, 442);
        this.tabControl1.Name = "tabControl1";
        this.tabControl1.SelectedIndex = 0;
        this.tabControl1.Size = new System.Drawing.Size(292, 237);
        this.tabControl1.TabIndex = 11;
        this.tabControl1.TabStop = false;
        // 
        // tabPage1
        // 
        this.tabPage1.Controls.Add(this.lbDoc);
        this.tabPage1.Location = new System.Drawing.Point(4, 22);
        this.tabPage1.Name = "tabPage1";
        this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage1.Size = new System.Drawing.Size(284, 211);
        this.tabPage1.TabIndex = 0;
        this.tabPage1.Text = "Document";
        this.tabPage1.UseVisualStyleBackColor = true;
        // 
        // lbDoc
        // 
        this.lbDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.lbDoc.FormattingEnabled = true;
        this.lbDoc.HorizontalScrollbar = true;
        this.lbDoc.Location = new System.Drawing.Point(0, 6);
        this.lbDoc.Name = "lbDoc";
        this.lbDoc.ScrollAlwaysVisible = true;
        this.lbDoc.Size = new System.Drawing.Size(284, 186);
        this.lbDoc.TabIndex = 11;
        // 
        // tabPage2
        // 
        this.tabPage2.Controls.Add(this.lbLog);
        this.tabPage2.Location = new System.Drawing.Point(4, 22);
        this.tabPage2.Name = "tabPage2";
        this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
        this.tabPage2.Size = new System.Drawing.Size(284, 211);
        this.tabPage2.TabIndex = 1;
        this.tabPage2.Text = "Logging";
        this.tabPage2.UseVisualStyleBackColor = true;
        // 
        // btnClear
        // 
        this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnClear.Location = new System.Drawing.Point(225, 226);
        this.btnClear.Name = "btnClear";
        this.btnClear.Size = new System.Drawing.Size(63, 23);
        this.btnClear.TabIndex = 6;
        this.btnClear.Text = "Clear";
        this.btnClear.UseVisualStyleBackColor = true;
        this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
        // 
        // lblContent
        // 
        this.lblContent.AutoSize = true;
        this.lblContent.Location = new System.Drawing.Point(3, 39);
        this.lblContent.Name = "lblContent";
        this.lblContent.Size = new System.Drawing.Size(96, 13);
        this.lblContent.TabIndex = 26;
        this.lblContent.Text = "Document Content";
        // 
        // txtContent
        // 
        this.txtContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.txtContent.Location = new System.Drawing.Point(6, 56);
        this.txtContent.Name = "txtContent";
        this.txtContent.Size = new System.Drawing.Size(282, 20);
        this.txtContent.TabIndex = 1;
        // 
        // TOCDocClicker
        // 
        this.Controls.Add(this.lblContent);
        this.Controls.Add(this.txtContent);
        this.Controls.Add(this.btnClear);
        this.Controls.Add(this.tabControl1);
        this.Controls.Add(this.btnIndex);
        this.Controls.Add(this.btnLocate);
        this.Controls.Add(this.btnDownload);
        this.Controls.Add(this.btnOpen);
        this.Controls.Add(this.btnSearch);
        this.Controls.Add(this.lblSearchCategories);
        this.Controls.Add(this.lblResultsGrid);
        this.Controls.Add(this.grdResults);
        this.Controls.Add(this.dateTimePicker2);
        this.Controls.Add(this.dateTimePicker1);
        this.Controls.Add(this.lblStartDate);
        this.Controls.Add(this.lblEndDate);
        this.Controls.Add(this.lblDocName);
        this.Controls.Add(this.txtName);
        this.Controls.Add(this.clbSearchLayers);
        this.Name = "TOCDocClicker";
        this.Size = new System.Drawing.Size(295, 682);
        this.Load += new System.EventHandler(this.TOCDocClicker_Load);
        ((System.ComponentModel.ISupportInitialize)(this.grdResults)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
        this.tabControl1.ResumeLayout(false);
        this.tabPage1.ResumeLayout(false);
        this.tabPage2.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #region "Form load"

    private void TOCDocClicker_Load(object sender, EventArgs e)
      {
          try
          {
              // Create Context menu and items for Layers in the Checked Listbox of Search Layers
              System.Windows.Forms.ContextMenu contextMenu1;
              contextMenu1 = new System.Windows.Forms.ContextMenu();
              System.Windows.Forms.MenuItem menuItem1;
              menuItem1 = new System.Windows.Forms.MenuItem();
              System.Windows.Forms.MenuItem menuItem2;
              menuItem2 = new System.Windows.Forms.MenuItem();
              System.Windows.Forms.MenuItem menuItem3;
              menuItem3 = new System.Windows.Forms.MenuItem();
              contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { menuItem1, menuItem2, menuItem3 });
              menuItem1.Index = 0;
              menuItem1.Text = "Add a Layer";
              menuItem2.Index = 1;
              menuItem2.Text = "Remove a Layer";
              menuItem3.Index = 2;
              menuItem3.Text = "Edit a Layer";
              // Hook the context menu to the Searh Layer Listbox.
              this.clbSearchLayers.ContextMenu = contextMenu1;
              // Make connection to SQL Database
              DBConnection();
              // Create a data class in DocManager so it will create data table or insert a row. 
              m_DataClass = new DocManager.clsData();
              // Clear the Search Layers
              this.clbSearchLayers.Items.Clear();
              // Create a ArcGis Class so it can call an ArcObject subroutine
              GISClass = new ArcGisClass();
              // get the current map.
              m_map = GISClass.GetMap(m_application);
              // Loop throu all the layers in the map and if it has the indexes so it will display on the list box
              GISClass.LoopThroughLayers(m_dtIndex, clbSearchLayers, m_map, "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}");
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }
      }

    #endregion

      
      #region "Database Connection"
     
      /// <summary>
      /// SqlDataReader - Not currently used.
      /// </summary>
      /// <param name="item"></param>
      public System.Data.SqlClient.SqlDataReader DB_data
      {
          get { return m_DataSet; }
      }


      /// <summary>
      /// DBConnection - Make connection to SQL database and returns 2 tables in Dataset
      /// </summary>
      /// <param name="item"></param>
      void DBConnection()
      {
          try
          {

              // Create a new class of dbConnection class
              m_DS = null;
              if (m_Connection == null)
              {
                  m_Connection = new dbConnection();

              }
              // Get the dataset
              m_DS = m_Connection.sqlDataSet();
              // Set the table to the global variable.
              m_dtIndex = m_DS.Tables["SpatialIndexLayerCatalog"];
              m_dtDoc = m_DS.Tables["DocumentSpatialRelations"];
          }

          catch (Exception ex)
          {
              System.Windows.Forms.MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }
      }

      #endregion


      #region List box methods and properties
      /// <summary>
      /// Adds a new item to the form's list box
      /// </summary>
      /// <param name="item">Object/string to add to the list box</param>
      public void AddItemToLog(object item)
      {
          this.lbLog.Items.Add(item.ToString());
          lbLog.Refresh();
      }
      public void AddItemToProperty(object item)
      {
          this.lbDoc.Items.Add(item.ToString());
          lbDoc.Refresh();
      }

      /// <summary>
      /// Clears all items that are currently in the list box.
      /// </summary>
      public void ClearLog()
      {
          this.lbLog.Items.Clear();
      }

      /// <summary>
      /// Forces a refresh of list box contents whenever list box is resized
      /// </summary>
      /// <param name="sender">Object sending the event</param>
      /// <param name="e">Event arguments</param>
      private void lbLog_Resize(object sender, System.EventArgs e)
      {
          this.lbLog.Refresh();
      }

      /// <summary>
      /// Returns a reference to the list box on the form.
      /// </summary>
      public ListBox LogListBox
      {
          get
          {
              return this.lbLog;
          }
      }

      public string docName
      {
          get
          {
              return this.txtName.Text;
          }
      }

      public string startDate
      {
          get
          {
              return this.dateTimePicker1.Value.ToString();
          }
      }

      public string endDate
      {
          get
          {
              return this.dateTimePicker2.Value.ToString();
          }
      }

      #endregion


      #region "Grid Results Events"

      /// <summary>
      /// fileGridRow - Add or get a row to the Grid
      /// </summary>
      /// <param name="item"></param>
      public DataGridViewRow fileGridRow
      {
          get
          {
              return this.grdResults.CurrentRow;
          }
          set
          {
              this.grdResults.Rows.Add(value);
          }
      }

      /// <summary>
      /// BindToGrid - Bind the data table to the GRID and set up the columns.
      /// </summary>
      /// <param name="item"></param>
      public void BindToGrid(DataTable DT)
      {
          try
          {
              this.grdResults.DataSource = this.bindingSource1;
              this.bindingSource1.DataSource = DT;
              this.grdResults.Columns["URI"].Width = 30;
              this.grdResults.Columns["URI"].ReadOnly = true;
              this.grdResults.Columns["Name"].ReadOnly = true;
              this.grdResults.Columns["Geoinfo"].Visible = true;
              this.grdResults.Refresh();
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }
      }

      /// <summary>
      /// grdResults_CellMouseClick - Click on the row and return the properties of the selected document.
      /// </summary>
      /// <param name="item"></param>
      private void grdResults_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
      {
          string sURI;
          // No Row selected then bail out.
          try
          {
              if (e.RowIndex == -1)
              {
                  return;
              }
              // Display a busy hourglass
              GISClass.BusyMouse(true);
              // Get the URI column in the seleted row
              sURI = (String)this.grdResults.Rows[e.RowIndex].Cells["URI"].Value;
              // Clear the current property display in the listbox Document
              lbDoc.Items.Clear();
              // Get the Properties and display them on the Listbox.
              m_docSearch.retrievePropertyfromURI(sURI);
              GISClass.BusyMouse(false);
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }
      }

      /// <summary>
      /// grdResults_CellDoubleClick - DoubleClick on the row to download and open the selected document.
      /// </summary>
      /// <param name="item"></param>
      private void grdResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
      {
          string sPath;
          string sURI;
          string sFilename;
          try
          {
              // If no row selected and get out.
              if (e.RowIndex == -1)
              {
                  return;
              }
              GISClass.BusyMouse(true);
              // Get the cells value in the doubleclicked row
              sURI = (String)this.grdResults.Rows[e.RowIndex].Cells["URI"].Value;
              sFilename = (String)this.grdResults.Rows[e.RowIndex].Cells["Name"].Value;
              // Set a path to Temp folder so it can download
              sPath = "c:\\temp\\";
              // Download a document
              m_docSearch.DocDownload(sURI, sFilename, sPath);
              // Add to check if the file exit and open
              if (File.Exists(sPath + sFilename))
              {
                  // Open the Document now.
                  Process.Start(sPath + sFilename);
              }
              else
              {
                  // Display the complaint message
                  System.Windows.Forms.MessageBox.Show("No Document is related to URI: " + sURI);
              }
              GISClass.BusyMouse(false);
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }

      }

#endregion


      #region "Buttons Click"

      /// <summary>
      /// btnSearch_Click - Search document on TRIM or Spatial map and display on the GRID.
      /// </summary>
      /// <param name="item"></param>
      private void btnSearch_Click(object sender, EventArgs e)
      {
          try
          {
              // Make a log and keep track of it
              if (m_Logger == null)
              {
                  m_Logger = new Logger(m_application);
              }
              // Add the track info in the Log
              m_Logger.Log("Search Tool Created", 5);

              // Make a new Serach
              m_docSearch = new DocMgrSearch(m_Logger);

              // Clear the GRID to get the new dataset
              if (m_DT != null) // If there is the old data table display on the GRID
              {
                  m_DT.Clear(); // Clear it
                  BindToGrid(m_DT); // Bind in the GRID to display nothing
              }
              else // otherwise Create a new data table
              {
                  m_DT = m_DataClass.CreateDataTable();
              }
              // Make a busy mouse 
              GISClass.BusyMouse(true);
              // There is some text search then Search TRIM
              if (this.txtName.Text != "")
              {
                  m_docSearch.DoSearch(this.txtName.Text, dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString() );
              }
              else // Otherwise the spatial search
              {
                  // Loop throu Search layers in checkbox list to see any selected feature
                  getFeaturesInMap();
                  // If there is any return data then ..
                  if (m_DT != null)
                  {
                      // Bind the data table to the GRID
                      BindToGrid(m_DT);
                      // and Sort the URI column.
                      DataGridViewColumn sortcolumn = grdResults.Columns["URI"];
                      grdResults.Sort(sortcolumn, ListSortDirection.Ascending);
                  }
              }
              GISClass.BusyMouse(false);
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }
      }

      /// <summary>
      /// Clear the existing search results from the grid.
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void btnClear_Click(object sender, EventArgs e)
      {
          try
          {
              if (m_DT != null) // If there is the old data table display on the GRID
              {
                  m_DT.Clear(); // Clear it
                  BindToGrid(m_DT); // Bind in the GRID to display nothing
              }
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }
      }
      /// <summary>
      /// btnOpen_Click - Download and open a document associated with URI number.
      /// </summary>
      /// <param name="item"></param>
      private void btnOpen_Click(object sender, EventArgs e)
      {
          try
          {
              // Path for download document
              string sPath = "c:\\temp\\";
              // Loop throu all rows
              for (int i = 0; i < this.grdResults.Rows.Count; i++)
              {
                  // If the row is selected
                  if ((string)this.grdResults.Rows[i].Cells[0].Value == "1")
                  {
                      // Download and open that URI.
                      GetFiles((string)this.grdResults.Rows[i].Cells["URI"].Value, (string)this.grdResults.Rows[i].Cells["Name"].Value, sPath, true);

                  }
              }
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }
      }

      /// <summary>
      /// btnDownload_Click - Download  a document to the specified folder.
      /// </summary>
      /// <param name="item"></param>
      private void btnDownload_Click(object sender, EventArgs e)
      {
          try
          {
              // Open the folder browser
              this.folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
              DialogResult result = this.folderBrowserDialog1.ShowDialog();

              if (result == DialogResult.OK)
              {
                  // the code here will be executed if the user presses Open in
                  // the dialog.
                  // Get the folder path
                  string sPath = this.folderBrowserDialog1.SelectedPath + "\\";
                  // Loop throu all rows
                  for (int i = 0; i < this.grdResults.Rows.Count; i++)
                  {
                      // If the row is selected then
                      if ((string)this.grdResults.Rows[i].Cells[0].Value == "1")
                      {
                          // Download it but not open.
                          GetFiles((string)this.grdResults.Rows[i].Cells["URI"].Value, (string)this.grdResults.Rows[i].Cells["Name"].Value, sPath, false);

                      }
                  }

              }
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }

      }

      /// <summary>
      /// btnLocate_Click - Locate the spatial feature associates with the Document. After that flash and select that spatial.
      /// </summary>
      /// <param name="item"></param>
      private void btnLocate_Click(object sender, EventArgs e)
      {
          try
          {
              // Create temp table so it will capture all features link to the located URI.
              if (m_TempIndex == null)
              {
                  m_TempIndex = CreateTempTable();
              }
              else
              {
                  m_TempIndex.Clear();

              }
              bool bFeature;
              // Get all features and Flash it if any and return false if not any.
              bFeature = GetIndexforDocumentSearch();

              // Select all Features in m_TempIndex if any
              if (bFeature)
              {
                  SelectLocatedIndex();
              }
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }

      }

      /// <summary>
      /// btnIndex_Click - Link the spatial feature(s) to the Document.
      /// </summary>
      /// <param name="item"></param>
      private void btnIndex_Click(object sender, EventArgs e)
      {
          try
          {
              GISClass.BusyMouse(true);
              string indexReport = string.Empty;
              // Loop through the selected Documents to link to the currently selected spatial features.
              for (int i = 0; i < this.grdResults.Rows.Count; i++)
              {
                  string DocumentURI = "";
                  string DocumentURL = "";
                  if ((string)this.grdResults.Rows[i].Cells[0].Value == "1")
                  {
                      DocumentURI = this.grdResults.Rows[i].Cells["URI"].Value.ToString();
                      DocumentURL = this.grdResults.Rows[i].Cells["Name"].Value.ToString();
                      string where = "";
                      DataRow[] aRows = null;


                      foreach (object itemChecked in this.clbSearchLayers.CheckedItems)
                      {
                          string LayerName = itemChecked.ToString();
                          string LayerIndex = "";
                          // Get the layer index
                          where = "LayerName = '" + LayerName + "'";
                          aRows = m_dtIndex.Select(where);
                          foreach (DataRow row in aRows)
                          {
                              LayerIndex = row["SICID"].ToString();
                          }
                          // Set up an array to hold all the selected features
                          IArray pArray = new ArrayClass();
                          pArray = GetSelectedFeature(LayerName, m_application);

                          // Loop throu the indexed features to add in the Database

                          for (int j = 0; j < pArray.Count; j++)
                          {
                              IFeature feature = (IFeature)pArray.get_Element(j);
                              string columnname = GetIndexColumnName(LayerName);
                              string Indexvalue = feature.get_Value(feature.Fields.FindField(columnname)).ToString();
                              // Make sure the record is not already in the dataset.
                              where = "DocumentURI=" + DocumentURI + " AND DocumentURL = '" + DocumentURL + "' AND SICID_FK = " + LayerIndex + " AND SICKeyvalue = '" + Indexvalue + "'";
                              aRows = m_dtDoc.Select(where);
                              // Add a record here if it's not in database.
                              if (aRows.Length < 1)
                              {
                                  InsertIndex(DocumentURI, DocumentURL, LayerIndex, Indexvalue);
                                  indexReport = indexReport + "Document: " + DocumentURL + " Linked to a feature in layer: " + LayerName + Environment.NewLine;
                              }


                          }
                      }
                  }
              }
              if (indexReport != string.Empty) 
              {
                  MessageBox.Show(indexReport, "Index Successful");
              }
              // Reload ADO.NET.
              DBConnection();
              GISClass.BusyMouse(false);
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }
      }

#endregion

      #region "Supported Subroutines"

      /// <summary>
      /// getFeaturesInMap - Search the selected spatial feature(s) to find the Document
      /// to be associated. This function to support SPATIAL SEARCH.
      /// </summary>
      /// <param name="item"></param>
      private void getFeaturesInMap()
      {
          try
          {
              //Loop throu the selected Layers in Search Layer checked box list
              foreach (object itemChecked in this.clbSearchLayers.CheckedItems)
              {
                  // Get a selected layer name
                  string LayName = itemChecked.ToString();
                  // Set up an array to hold all the selected features in that layer
                  IArray pArray = new ArrayClass();
                  pArray = GetSelectedFeature(LayName, m_application);

                  if (pArray.Count == 0)
                  {
                      if (MessageBox.Show("No features selected in layer: " + LayName + ".  Do you want to search all features?", "Search Documents", MessageBoxButtons.YesNo) == DialogResult.Yes)
                      {
                          pArray = GetAllFeatures(LayName, m_application);
                      }
                  }
                  // Loop throu all the selected features

                  for (int i = 0; i < pArray.Count; i++)
                  {
                      // Get a feature
                      IFeature feature = (IFeature)pArray.get_Element(i);
                      // Get the index of that layer
                      string columnname = GetIndexColumnName(LayName);
                      //Get SICID for the layer
                      string SICID = GetSICIDForLayer(LayName);
                      // Get an index value
                      string value = feature.get_Value(feature.Fields.FindField(columnname)).ToString();
                      // Go to add a document associated with that feature
                      AddItemtoGrid(SICID, value);

                  }

              }
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }
      }

      /// <summary>
      /// AddItemtoGrid - Add a document associate to the selected feature.
      /// This function to support SPATIAL SEARCH.
      /// </summary>
      /// <param name="item">A Selected Feature in the map</param>
      /// <param name="item">A Index value of that selected feature</param>
      private void AddItemtoGrid(string SICID, string IndexValue)
      {
          try
          {
              string val = "";
              // Loop throu the Document table in the database
              foreach (DataRow dtrow in m_dtDoc.Rows)
              {
                  // Find out if any index of the selected feature in the spatial doc. table ?
                  if (dtrow["SICKeyvalue"].ToString() == IndexValue && dtrow["SICID_FK"].ToString() == SICID)
                  {
                      // If so then get that ROW
                      DataRow dr = GettableIndexRow(dtrow["SICID_FK"].ToString());
                      val = dr["layerName"].ToString() + ":" + dr["IndexColumnName"].ToString() + ":" + dtrow["SICKeyvalue"].ToString();
                      // Check make sure if there is not URI yet and then Add otherwise ignore it. 
                      if (!isURIinGRID(dtrow["DocumentURI"].ToString()))
                      {
                          m_DataClass.AddDataToTable(dtrow["DocumentURI"].ToString(), dtrow["DocumentURL"].ToString(), DateTime.Now, m_DT, val);
                          break;
                      }
                  }
              }
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }

      }

      /// <summary>
      /// isURIinGRID - Check in data table for the GRID if the passing URI
      /// is currently in the GRID
      /// </summary>
      /// <param name="item">URI</param>
      /// <param name="item">Return Boolean if that URI in the GRID</param>
      private bool isURIinGRID(string URI)
      {
          bool bRet = false;
          try
          {
          // Check it in Data table 
          if (m_DT.Rows.Count > 0)
          {
              foreach (DataRow dr in m_DT.Rows)
              {
                  if (dr["URI"].ToString() == URI)
                  {
                      bRet = true;
                      break;
                  }
              }
          }
          return bRet;
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
              return bRet;
          }
      }

      /// <summary>
      /// GettableIndexRow - Get a row in Spatial index table from Dataset.
      /// </summary>
      /// <param name="item">Index Value</param>
      /// <param name="item">Return a row with that Index</param>
      private DataRow GettableIndexRow(string SICID)
      {
          DataRow drow = null;
          try
          {
          string where = "SICID =" + SICID;
          DataRow[] aRows = m_dtIndex.Select(where);
          foreach (DataRow dr in aRows)
          {


              if (dr != null)
              {
                  drow = dr;
                  break;
              }
          }
          return drow;
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
              return drow;
          }
      }

      /// <summary>
      /// GetIndexColumnName - Get a row in Spatial index table from Dataset.
      /// </summary>
      /// <param name="item">Layername</param>
      /// <param name="item">Return a index column name</param>
      private string GetIndexColumnName(string layerName)
      {
          string val = "";
          try
          {
          string where = "LayerName = '" + layerName + "'";
          DataRow[] aRows = m_dtIndex.Select(where);
          foreach (DataRow dr in aRows)
          {
              if (dr["LayerName"].ToString() == layerName)
              {
                  val = dr["IndexColumnName"].ToString();

                  break;
              }
          }

          return val;
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
              return val;
          }
      }

      private string GetSICIDForLayer(string layerName)
      {
          string val = "";
          try
          {
              string where = "LayerName = '" + layerName + "'";
              DataRow[] aRows = m_dtIndex.Select(where);
              foreach (DataRow dr in aRows)
              {
                  if (dr["LayerName"].ToString() == layerName)
                  {
                      val = dr["SICID"].ToString();

                      break;
                  }
              }

              return val;
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
              return val;
          }
      }

      /// <summary>
      /// GetSelectedFeature - Get the selected feature in a layer.
      /// </summary>
      /// <param name="item">Layername</param>
      /// <param name="item">Map application in ArcGIS</param>
      /// Return: A features array
      private IArray GetSelectedFeature(string layName, IApplication pApp)
      {
          IArray pArray = new ArrayClass();
          try
          {
          IFeatureLayer pFtrLyr;
          // get Active View
          ESRI.ArcGIS.Carto.IActiveView pactview;
          pactview = GISClass.GetActiveView(m_application);
          // Get an index number of the layer
          int layIndex = GISClass.GetIndexNumberOfLayerName(pactview, layName);
          // Get that layer
          pFtrLyr = GISClass.GetIFeatureLayerFromLayerIndexNumber(pactview, layIndex);
          // Define cursor selection set for that layer
          IFeatureCursor pFCur;
          ICursor pCur;
          IFeatureSelection pFeatureSelection;
          ISelectionSet pSelectionSet;
          IFeature pFeature;

          // Get the feature selection 
          pFeatureSelection = (IFeatureSelection)pFtrLyr;
          // into the set
          pSelectionSet = pFeatureSelection.SelectionSet;
          // Make a cursor 
          pSelectionSet.Search(null, false, out pCur); // Here we go!

          pFCur = (IFeatureCursor)pCur; // Q.I
          pFeature = pFCur.NextFeature(); // Can now access NextFeature method
          // Loop throu the set and add in the array
          while (!(pFeature == null))
          {
              pArray.Add(pFeature);
              pFeature = pFCur.NextFeature();

          }

          // Clear the selected features in this layer
          GISClass.ClearSelectedMapFeatures(pactview, pFtrLyr);

          return pArray;
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
              return pArray;
          }

      }

        /// <summary>
        /// Get all feature in a layer and return in an array
        /// </summary>
        /// <param name="layName"></param>
        /// <param name="pApp"></param>
        /// <returns></returns>
      private IArray GetAllFeatures(string layName, IApplication pApp)
      {
          IArray pArray = new ArrayClass();
          try
          {
              IFeatureLayer pFtrLyr;
              // get Active View
              ESRI.ArcGIS.Carto.IActiveView pactview;
              pactview = GISClass.GetActiveView(m_application);
              // Get an index number of the layer
              int layIndex = GISClass.GetIndexNumberOfLayerName(pactview, layName);
              // Get that layer
              pFtrLyr = GISClass.GetIFeatureLayerFromLayerIndexNumber(pactview, layIndex);
              // Define cursor selection set for that layer
              IFeatureCursor pFCur;
              IFeature pFeature;

              IQueryFilter pQFilter = new QueryFilterClass();
              pQFilter.WhereClause = ""; //return all 
              pFCur = pFtrLyr.Search(pQFilter, false);

              pFeature = pFCur.NextFeature(); // Can now access NextFeature method
              // Loop throu the set and add in the array
              while (!(pFeature == null))
              {
                  pArray.Add(pFeature);
                  pFeature = pFCur.NextFeature();

              }

              return pArray;
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
              return pArray;
          }

      }


      /// <summary>
      /// GetFiles - Download and open a file from TRIM if boolen open is true.
      /// </summary>
      /// <param name="item">URI</param>
      /// <param name="item">filename</param>
      /// <param name="item">sPath of the filename</param>
      /// <param name="item">Open the download file if it's true</param>
      private void GetFiles(string URI, string filename, string sPath, bool Open)
      {
          try
          {
              GISClass.BusyMouse(true);
              // Download a file
              m_docSearch.DocDownload(URI, filename, sPath);
              if (Open)
              {
                  if (File.Exists(sPath + filename))
                  {
                      // Open the file
                      Process.Start(sPath + filename);
                  }
                  else
                  {
                      // No file file to download
                      System.Windows.Forms.MessageBox.Show("No Document is related to URI: " + URI);
                  }

              }
              GISClass.BusyMouse(false);
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }
      }

      /// <summary>
      /// SelectLocatedIndex - Select all features associate with a Document
      /// when the locate button is clicked.
      /// Use a datatable which stores all the features with their layernames and OID numbers
      /// </summary>
      private void SelectLocatedIndex()
      {

          try
          {
              string Layername = "";
              // Create a dataview from that table in order to sort the layername
              DataView vue = new DataView(m_TempIndex);
              vue.Sort = "LayerName ASC";
              DataRowView row;
              IFeatureLayer pFtrLyr;
              IFeatureSelection pFeatureSelection;
              ISelectionSet pSelectionSet;
              // get Active View
              ESRI.ArcGIS.Carto.IActiveView pactview;
              pactview = GISClass.GetActiveView(m_application);
              // Get a first row
              if (vue.Count == 0)
              {
                  MessageBox.Show("No features in the current layers have been indexed to this document.");
                  return;
              }

              row = vue[0];
              // Get the layer in the first row
              Layername = row["LayerName"].ToString();
              int layIndex = GISClass.GetIndexNumberOfLayerName(pactview, Layername);
              pFtrLyr = GISClass.GetIFeatureLayerFromLayerIndexNumber(pactview, layIndex);
              // Get that layer selection
              pFeatureSelection = (IFeatureSelection)pFtrLyr;
              pSelectionSet = pFeatureSelection.SelectionSet;
              // Add the select feature in the set
              pSelectionSet.Add(Convert.ToInt32(row["OID"].ToString()));
              // Continue to drill down and add the selected features
              // until see the layername and switch to that layer and continue until 
              // end of the dataview
              for (int i = 1; i < vue.Count; i++)
              {
                  row = vue[i];
                  // Switch to different layer if the layer name is changed in Dataview
                  // and create a selection set for the new layer
                  if (Layername != row["LayerName"].ToString())
                  {
                      Layername = row["LayerName"].ToString();
                      layIndex = GISClass.GetIndexNumberOfLayerName(pactview, Layername);
                      pFtrLyr = GISClass.GetIFeatureLayerFromLayerIndexNumber(pactview, layIndex);
                      pFeatureSelection = (IFeatureSelection)pFtrLyr;
                      pSelectionSet = pFeatureSelection.SelectionSet;
                  }
                  // Continue to add a selected feature
                  pSelectionSet.Add(Convert.ToInt32(row["OID"].ToString()));

              }
              // Screen refresh
              pactview.Refresh();
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }
      }

      /// <summary>
      /// GetIndexforDocumentSearch - Get all the features associate to the Document
      /// When a locate button is clicked. 
      /// and save each feature with its layername and OID number in order to 
      /// use in SelectLocatedIndex subroutine and flash that feature to identify
      ///
      /// </summary>
      /// Return True if there is a feature associated with the Document.
      private bool GetIndexforDocumentSearch()
      {

          bool bFeature = false;
          // Loop through and get all the selected URI
          try
          {
              for (int i = 0; i < this.grdResults.Rows.Count; i++)
              {
                  if ((string)this.grdResults.Rows[i].Cells[0].Value == "1")
                  {
                      // Get URI number from the selected document want to be located.
                      string search = "DocumentURI = '" + this.grdResults.Rows[i].Cells["URI"].Value.ToString() + "'";
                      // Get all rows which associate with that URI in Dataset from spatial database
                      DataRow[] Doc_Rows = m_dtDoc.Select(search);
                      // Loop through those rows for each feature associate with that document
                      foreach (DataRow Doc_dr in Doc_Rows)
                      {
                          // Get a row from Spatial Index table in the dataset 
                          DataRow row = GettableIndexRow(Doc_dr["SICID_FK"].ToString());
                          // prepare Geo information for that feature
                          string geoinfo = row["LayerName"].ToString() + ":" + row["IndexColumnName"].ToString() + ":" + Doc_dr["SICKeyvalue"].ToString();
                          // Get that feature
                          IFeature pFeature = GetFeatureinGRID(geoinfo);
                          if (pFeature == null)
                          {
                              continue;
                          }
                          // Create a temporary table to save all the features associated with the Document
                          // To use it in the SelectLocatedIndex sub. so it can select them all
                          DataRow dr;
                          dr = m_TempIndex.NewRow();
                          // Just save a layername and OID.
                          dr["LayerName"] = row["LayerName"].ToString();
                          dr["OID"] = pFeature.OID;
                          m_TempIndex.Rows.Add(dr);
                          // Get the shape of that feature
                          ESRI.ArcGIS.Geometry.IGeometry pGeo = pFeature.Shape;
                          IActiveView pActiveview = GISClass.GetActiveView(m_application);
                          ESRI.ArcGIS.Display.IDisplay pDisplay = pActiveview.ScreenDisplay;
                          ESRI.ArcGIS.Display.IRgbColor pRGBColor = new ESRI.ArcGIS.Display.RgbColorClass();
                          pRGBColor.Red = 255;
                          pRGBColor.Green = 0;
                          pRGBColor.Blue = 0;
                          // Flash to identify the feature
                          GISClass.FlashGeometry(pGeo, pRGBColor, pDisplay, 300);


                      }
                      // If there is not any and warning
                      if (Doc_Rows.Length < 1)
                      {
                          System.Windows.Forms.MessageBox.Show("No Spatial Index for this Document");
                          bFeature = false;

                      }
                      else
                      {
                          bFeature = true;

                      }

                  }

              }
              return bFeature;
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
              return bFeature;
          }

      }


      public void PopulateGeoInfo(DataTable DT)
      {
          string geoinfo = string.Empty;
          try
          {   
              string geoInfoSearch = "geoinfo = ''";
              DataRow[] trimRows = DT.Select(geoInfoSearch);
              foreach (DataRow trimRow in trimRows)
              {

                  // Get URI number from the selected document want to be located.
                  string search = "DocumentURI = '" + trimRow["URI"].ToString() + "'";
                  // Get all rows which associate with that URI in Dataset from spatial database
                  DataRow[] Doc_Rows = m_dtDoc.Select(search);
                  // Loop through those rows for each feature associate with that document
                  foreach (DataRow Doc_dr in Doc_Rows)
                  {
                      // Get a row from Spatial Index table in the dataset 
                      DataRow row = GettableIndexRow(Doc_dr["SICID_FK"].ToString());
                      // prepare Geo information for that feature

                      if (this.clbSearchLayers.Items.IndexOf(row["LayerName"].ToString()) > -1)
                      {
                          geoinfo = row["LayerName"].ToString() + ":" + row["IndexColumnName"].ToString() + ":" + Doc_dr["SICKeyvalue"].ToString();
                          trimRow["geoinfo"] = geoinfo;
                      }
                  }
                  
              }
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }
      }

      /// <summary>
      /// GetFeatureinGRID - Get a feature with some Geo-information.
      /// The Geo information is saved in one string with delimiter :
      /// </summary>
      /// <param name="item">Geo-information like Layername, Index and index column</param>
      /// <param name="item">Return a Feature</param>
      private IFeature GetFeatureinGRID(string geoinfo)
      {
          // Parse out the string
          char[] delimiterChars = { ':' };
          string[] words = geoinfo.Split(delimiterChars);
          // Get the layername
          string layername = words[0];
          // Column name
          string IndexColumnName = words[1];
          // Index of that column name
          string SICKeyvalue = words[2];

          IFeatureLayer pFtrLyr;
          IFeatureCursor pFCur = null;
          try
          {
              // get Active View
              ESRI.ArcGIS.Carto.IActiveView pactview;
              pactview = GISClass.GetActiveView(m_application);
              int layIndex = GISClass.GetIndexNumberOfLayerName(pactview, layername);

              if (layIndex == -1)
              {
                  return null;
              }
              // Get layer
              pFtrLyr = GISClass.GetIFeatureLayerFromLayerIndexNumber(pactview, layIndex);
              // Feature clasee
              IFeatureClass pFtClass = pFtrLyr.FeatureClass;
              IFields pFields = pFtClass.Fields;
              // Get the field of that column name
              IField pField = pFields.get_Field(pFields.FindField(IndexColumnName));
              string where = "";
              // Create a where clause to query
              if (pField.Type == esriFieldType.esriFieldTypeString)
              {
                  where = IndexColumnName + " = '" + SICKeyvalue + "'";// "city_name = 'Redlands'"
              }
              else
              {
                  where = IndexColumnName + " = " + SICKeyvalue;
              }

              // Get the table from that layer
              pFtrLyr = GISClass.GetIFeatureLayerFromLayerIndexNumber(pactview, layIndex);
              ITable pTable = (ITable)pFtrLyr;

              // query that feature
              ICursor pCur = GISClass.AttributeQuery(pTable, where);
              pFCur = (IFeatureCursor)pCur;
              // Return that feature
              return pFCur.NextFeature();
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
              return pFCur.NextFeature();
          }
              

          
      }

      /// <summary>
      /// CreateTempTable - Creat a temporary data table to save.
      /// all features associate with the Doc.
      /// </summary>
      private DataTable CreateTempTable()
      {
          DataTable myDataTable = new DataTable();
          try
          {
              DataColumn myDataColumn;

              myDataColumn = new DataColumn();
              myDataColumn.DataType = Type.GetType("System.String");
              myDataColumn.ColumnName = "LayerName";

              myDataTable.Columns.Add(myDataColumn);

              myDataColumn = new DataColumn();
              myDataColumn.DataType = Type.GetType("System.String");
              myDataColumn.ColumnName = "OID";
              myDataTable.Columns.Add(myDataColumn);

            
              return myDataTable;
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
              return myDataTable;
          }
      }

      /// <summary>
      /// InsertIndex - Write the spatial index feature to the database.
      /// when a user link a document to the selected features
      /// </summary>
      /// <param name="item">URI</param>
      /// <param name="item">URL - Filename</param>
      /// <param name="item">Layer - SICID_FK</param>
      /// <param name="item">Index value - SICKeyvalue</param>
      private void InsertIndex(string URI, string URL, string SICID_FK, string SICKeyvalue)
      {
          try
          {

              if (m_Connection == null)
              {
                  m_Connection = new dbConnection();

              }
              m_Connection.InsertLinkIndex(URI, URL, SICID_FK, SICKeyvalue);

          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.Message + Environment.NewLine.ToString() + ex.StackTrace);
          }
      }

      #endregion



  }
}
