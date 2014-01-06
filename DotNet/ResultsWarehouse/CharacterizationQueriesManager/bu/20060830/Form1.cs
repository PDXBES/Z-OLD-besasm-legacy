using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace CharacterizationQueriesManager
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class CharacterizationForm : System.Windows.Forms.Form
	{
		private System.Data.OleDb.OleDbConnection ModelCatalogConnection1;
		private System.Data.OleDb.OleDbDataAdapter ModelCatalogDataAdapter1;
		private System.Data.OleDb.OleDbCommand ModelCatalogSelectCommand1;
		private System.Data.OleDb.OleDbCommand ModelCatalogInsertCommand1;
		private System.Data.OleDb.OleDbCommand ModelCatalogUpdateCommand1;
		private System.Data.OleDb.OleDbCommand ModelCatalogDeleteCommand1;
		private System.Data.OleDb.OleDbDataAdapter DSCHydraulicsDataAdapter1;
		private System.Data.OleDb.OleDbCommand DSCHydraulicsSelectCommand1;
		private System.Data.OleDb.OleDbCommand DSCHydraulicsInsertCommand1;
		private System.Data.OleDb.OleDbCommand DSCHydraulicsUpdateCommand1;
		private System.Data.OleDb.OleDbCommand DSCHydraulicsDeleteCommand1;
		private System.Data.OleDb.OleDbDataAdapter LinkHydraulicsDataAdapter1;
		private System.Data.OleDb.OleDbCommand LinkHydraulicsSelectCommand1;
		private System.Data.OleDb.OleDbCommand LinkHydraulicsInsertCommand1;
		private System.Data.OleDb.OleDbCommand LinkHydraulicsUpdateCommand1;
		private System.Data.OleDb.OleDbCommand LinkHydraulicsDeleteCommand1;
		private System.Data.OleDb.OleDbDataAdapter ModelScenarioDataAdapter1;
		private System.Data.OleDb.OleDbCommand ModelScenarioSelectCommand1;
		private System.Data.OleDb.OleDbCommand ModelScenarioInsertCommand1;
		private System.Data.OleDb.OleDbCommand ModelScenarioUpdateCommand1;
		private System.Data.OleDb.OleDbCommand ModelScenarioDeleteCommand1;
		private System.Data.OleDb.OleDbDataAdapter NodeHydraulicsDataAdapter1;
		private System.Data.OleDb.OleDbCommand NodeHydraulicsSelectCommand1;
		private System.Data.OleDb.OleDbCommand NodeHydraulicsInsertCommand1;
		private System.Data.OleDb.OleDbCommand NodeHydraulicsUpdateCommand1;
		private System.Data.OleDb.OleDbCommand NodeHydraulicsDeleteCommand1;
		private System.Data.OleDb.OleDbDataAdapter StormCatalogDataAdapter1;
		private System.Data.OleDb.OleDbCommand StormCatalogSelectCommand1;
		private System.Data.OleDb.OleDbCommand StormCatalogInsertCommand1;
		private System.Data.OleDb.OleDbCommand StormCatalogUpdateCommand1;
		private System.Data.OleDb.OleDbCommand StormCatalogDeleteCommand1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CharacterizationForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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
			this.ModelCatalogConnection1 = new System.Data.OleDb.OleDbConnection();
			this.ModelCatalogDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();
			this.ModelCatalogSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.ModelCatalogInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.ModelCatalogUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			this.ModelCatalogDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			this.DSCHydraulicsDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();
			this.DSCHydraulicsSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.DSCHydraulicsInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.DSCHydraulicsUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			this.DSCHydraulicsDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			this.LinkHydraulicsDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();
			this.LinkHydraulicsSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.LinkHydraulicsInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.LinkHydraulicsUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			this.LinkHydraulicsDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			this.ModelScenarioDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();
			this.ModelScenarioSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.ModelScenarioInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.ModelScenarioUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			this.ModelScenarioDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			this.NodeHydraulicsDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();
			this.NodeHydraulicsSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.NodeHydraulicsInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.NodeHydraulicsUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			this.NodeHydraulicsDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			this.StormCatalogDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();
			this.StormCatalogSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.StormCatalogInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.StormCatalogUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			this.StormCatalogDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			// 
			// ModelCatalogConnection1
			// 
			this.ModelCatalogConnection1.ConnectionString = @"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Jet OLEDB:Database Password=;Data Source=""\\Cassio\modeling\Model_Programs\ResultsWarehouse\ModelResultsWarehouse.mdb"";Password=;Jet OLEDB:Engine Type=5;Jet OLEDB:Global Bulk Transactions=1;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False";
			// 
			// ModelCatalogDataAdapter1
			// 
			this.ModelCatalogDataAdapter1.DeleteCommand = this.ModelCatalogDeleteCommand1;
			this.ModelCatalogDataAdapter1.InsertCommand = this.ModelCatalogInsertCommand1;
			this.ModelCatalogDataAdapter1.SelectCommand = this.ModelCatalogSelectCommand1;
			this.ModelCatalogDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																											   new System.Data.Common.DataTableMapping("Table", "ModelCatalog", new System.Data.Common.DataColumnMapping[] {
																																																							   new System.Data.Common.DataColumnMapping("isUploaded", "isUploaded"),
																																																							   new System.Data.Common.DataColumnMapping("modelDescription", "modelDescription"),
																																																							   new System.Data.Common.DataColumnMapping("modelID", "modelID"),
																																																							   new System.Data.Common.DataColumnMapping("modelName", "modelName"),
																																																							   new System.Data.Common.DataColumnMapping("modelOutputFile", "modelOutputFile"),
																																																							   new System.Data.Common.DataColumnMapping("modelPath", "modelPath"),
																																																							   new System.Data.Common.DataColumnMapping("modelType", "modelType"),
																																																							   new System.Data.Common.DataColumnMapping("scenarioID", "scenarioID"),
																																																							   new System.Data.Common.DataColumnMapping("timeFrame", "timeFrame"),
																																																							   new System.Data.Common.DataColumnMapping("uploadDate", "uploadDate"),
																																																							   new System.Data.Common.DataColumnMapping("userName", "userName")})});
			this.ModelCatalogDataAdapter1.UpdateCommand = this.ModelCatalogUpdateCommand1;
			// 
			// ModelCatalogSelectCommand1
			// 
			this.ModelCatalogSelectCommand1.CommandText = "SELECT isUploaded, modelDescription, modelID, modelName, modelOutputFile, modelPa" +
				"th, modelType, scenarioID, timeFrame, uploadDate, userName FROM ModelCatalog";
			this.ModelCatalogSelectCommand1.Connection = this.ModelCatalogConnection1;
			// 
			// ModelCatalogInsertCommand1
			// 
			this.ModelCatalogInsertCommand1.CommandText = "INSERT INTO ModelCatalog(isUploaded, modelDescription, modelName, modelOutputFile" +
				", modelPath, modelType, scenarioID, timeFrame, uploadDate, userName) VALUES (?, " +
				"?, ?, ?, ?, ?, ?, ?, ?, ?)";
			this.ModelCatalogInsertCommand1.Connection = this.ModelCatalogConnection1;
			this.ModelCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("isUploaded", System.Data.OleDb.OleDbType.Boolean, 2, "isUploaded"));
			this.ModelCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("modelDescription", System.Data.OleDb.OleDbType.VarWChar, 255, "modelDescription"));
			this.ModelCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("modelName", System.Data.OleDb.OleDbType.VarWChar, 50, "modelName"));
			this.ModelCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("modelOutputFile", System.Data.OleDb.OleDbType.VarWChar, 255, "modelOutputFile"));
			this.ModelCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("modelPath", System.Data.OleDb.OleDbType.VarWChar, 255, "modelPath"));
			this.ModelCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("modelType", System.Data.OleDb.OleDbType.VarWChar, 3, "modelType"));
			this.ModelCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"));
			this.ModelCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("timeFrame", System.Data.OleDb.OleDbType.VarWChar, 2, "timeFrame"));
			this.ModelCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("uploadDate", System.Data.OleDb.OleDbType.DBDate, 0, "uploadDate"));
			this.ModelCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("userName", System.Data.OleDb.OleDbType.VarWChar, 50, "userName"));
			// 
			// ModelCatalogUpdateCommand1
			// 
			this.ModelCatalogUpdateCommand1.CommandText = @"UPDATE ModelCatalog SET isUploaded = ?, modelDescription = ?, modelName = ?, modelOutputFile = ?, modelPath = ?, modelType = ?, scenarioID = ?, timeFrame = ?, uploadDate = ?, userName = ? WHERE (modelID = ?) AND (isUploaded = ?) AND (modelDescription = ? OR ? IS NULL AND modelDescription IS NULL) AND (modelName = ? OR ? IS NULL AND modelName IS NULL) AND (modelOutputFile = ? OR ? IS NULL AND modelOutputFile IS NULL) AND (modelPath = ? OR ? IS NULL AND modelPath IS NULL) AND (modelType = ? OR ? IS NULL AND modelType IS NULL) AND (scenarioID = ?) AND (timeFrame = ? OR ? IS NULL AND timeFrame IS NULL) AND (uploadDate = ? OR ? IS NULL AND uploadDate IS NULL) AND (userName = ? OR ? IS NULL AND userName IS NULL)";
			this.ModelCatalogUpdateCommand1.Connection = this.ModelCatalogConnection1;
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("isUploaded", System.Data.OleDb.OleDbType.Boolean, 2, "isUploaded"));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("modelDescription", System.Data.OleDb.OleDbType.VarWChar, 255, "modelDescription"));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("modelName", System.Data.OleDb.OleDbType.VarWChar, 50, "modelName"));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("modelOutputFile", System.Data.OleDb.OleDbType.VarWChar, 255, "modelOutputFile"));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("modelPath", System.Data.OleDb.OleDbType.VarWChar, 255, "modelPath"));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("modelType", System.Data.OleDb.OleDbType.VarWChar, 3, "modelType"));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("timeFrame", System.Data.OleDb.OleDbType.VarWChar, 2, "timeFrame"));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("uploadDate", System.Data.OleDb.OleDbType.DBDate, 0, "uploadDate"));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("userName", System.Data.OleDb.OleDbType.VarWChar, 50, "userName"));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelID", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_isUploaded", System.Data.OleDb.OleDbType.Boolean, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "isUploaded", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelDescription", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelDescription", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelDescription1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelDescription", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelName", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelName", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelOutputFile", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelOutputFile", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelOutputFile1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelOutputFile", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelPath", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelPath", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelPath1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelPath", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelType", System.Data.OleDb.OleDbType.VarWChar, 3, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelType", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelType1", System.Data.OleDb.OleDbType.VarWChar, 3, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelType", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "scenarioID", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeFrame", System.Data.OleDb.OleDbType.VarWChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeFrame", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeFrame1", System.Data.OleDb.OleDbType.VarWChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeFrame", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_uploadDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "uploadDate", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_uploadDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "uploadDate", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_userName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "userName", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_userName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "userName", System.Data.DataRowVersion.Original, null));
			// 
			// ModelCatalogDeleteCommand1
			// 
			this.ModelCatalogDeleteCommand1.CommandText = @"DELETE FROM ModelCatalog WHERE (modelID = ?) AND (isUploaded = ?) AND (modelDescription = ? OR ? IS NULL AND modelDescription IS NULL) AND (modelName = ? OR ? IS NULL AND modelName IS NULL) AND (modelOutputFile = ? OR ? IS NULL AND modelOutputFile IS NULL) AND (modelPath = ? OR ? IS NULL AND modelPath IS NULL) AND (modelType = ? OR ? IS NULL AND modelType IS NULL) AND (scenarioID = ?) AND (timeFrame = ? OR ? IS NULL AND timeFrame IS NULL) AND (uploadDate = ? OR ? IS NULL AND uploadDate IS NULL) AND (userName = ? OR ? IS NULL AND userName IS NULL)";
			this.ModelCatalogDeleteCommand1.Connection = this.ModelCatalogConnection1;
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelID", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_isUploaded", System.Data.OleDb.OleDbType.Boolean, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "isUploaded", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelDescription", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelDescription", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelDescription1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelDescription", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelName", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelName", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelOutputFile", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelOutputFile", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelOutputFile1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelOutputFile", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelPath", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelPath", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelPath1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelPath", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelType", System.Data.OleDb.OleDbType.VarWChar, 3, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelType", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelType1", System.Data.OleDb.OleDbType.VarWChar, 3, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelType", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "scenarioID", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeFrame", System.Data.OleDb.OleDbType.VarWChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeFrame", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeFrame1", System.Data.OleDb.OleDbType.VarWChar, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeFrame", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_uploadDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "uploadDate", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_uploadDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "uploadDate", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_userName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "userName", System.Data.DataRowVersion.Original, null));
			this.ModelCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_userName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "userName", System.Data.DataRowVersion.Original, null));
			// 
			// DSCHydraulicsDataAdapter1
			// 
			this.DSCHydraulicsDataAdapter1.DeleteCommand = this.DSCHydraulicsDeleteCommand1;
			this.DSCHydraulicsDataAdapter1.InsertCommand = this.DSCHydraulicsInsertCommand1;
			this.DSCHydraulicsDataAdapter1.SelectCommand = this.DSCHydraulicsSelectCommand1;
			this.DSCHydraulicsDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																												new System.Data.Common.DataTableMapping("Table", "DSCHydraulics", new System.Data.Common.DataColumnMapping[] {
																																																								 new System.Data.Common.DataColumnMapping("deltaHGL", "deltaHGL"),
																																																								 new System.Data.Common.DataColumnMapping("DSCHydraulicsID", "DSCHydraulicsID"),
																																																								 new System.Data.Common.DataColumnMapping("DSCID", "DSCID"),
																																																								 new System.Data.Common.DataColumnMapping("HGL", "HGL"),
																																																								 new System.Data.Common.DataColumnMapping("modelID", "modelID"),
																																																								 new System.Data.Common.DataColumnMapping("scenarioID", "scenarioID"),
																																																								 new System.Data.Common.DataColumnMapping("surcharge", "surcharge")})});
			this.DSCHydraulicsDataAdapter1.UpdateCommand = this.DSCHydraulicsUpdateCommand1;
			// 
			// DSCHydraulicsSelectCommand1
			// 
			this.DSCHydraulicsSelectCommand1.CommandText = "SELECT deltaHGL, DSCHydraulicsID, DSCID, HGL, modelID, scenarioID, surcharge FROM" +
				" DSCHydraulics";
			this.DSCHydraulicsSelectCommand1.Connection = this.ModelCatalogConnection1;
			// 
			// DSCHydraulicsInsertCommand1
			// 
			this.DSCHydraulicsInsertCommand1.CommandText = "INSERT INTO DSCHydraulics(deltaHGL, DSCID, HGL, modelID, scenarioID, surcharge) V" +
				"ALUES (?, ?, ?, ?, ?, ?)";
			this.DSCHydraulicsInsertCommand1.Connection = this.ModelCatalogConnection1;
			this.DSCHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("deltaHGL", System.Data.OleDb.OleDbType.Double, 0, "deltaHGL"));
			this.DSCHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DSCID", System.Data.OleDb.OleDbType.Integer, 0, "DSCID"));
			this.DSCHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("HGL", System.Data.OleDb.OleDbType.Double, 0, "HGL"));
			this.DSCHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("modelID", System.Data.OleDb.OleDbType.Integer, 0, "modelID"));
			this.DSCHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"));
			this.DSCHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("surcharge", System.Data.OleDb.OleDbType.Double, 0, "surcharge"));
			// 
			// DSCHydraulicsUpdateCommand1
			// 
			this.DSCHydraulicsUpdateCommand1.CommandText = @"UPDATE DSCHydraulics SET deltaHGL = ?, DSCID = ?, HGL = ?, modelID = ?, scenarioID = ?, surcharge = ? WHERE (DSCHydraulicsID = ?) AND (DSCID = ?) AND (HGL = ? OR ? IS NULL AND HGL IS NULL) AND (deltaHGL = ? OR ? IS NULL AND deltaHGL IS NULL) AND (modelID = ? OR ? IS NULL AND modelID IS NULL) AND (scenarioID = ?) AND (surcharge = ? OR ? IS NULL AND surcharge IS NULL)";
			this.DSCHydraulicsUpdateCommand1.Connection = this.ModelCatalogConnection1;
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("deltaHGL", System.Data.OleDb.OleDbType.Double, 0, "deltaHGL"));
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DSCID", System.Data.OleDb.OleDbType.Integer, 0, "DSCID"));
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("HGL", System.Data.OleDb.OleDbType.Double, 0, "HGL"));
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("modelID", System.Data.OleDb.OleDbType.Integer, 0, "modelID"));
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"));
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("surcharge", System.Data.OleDb.OleDbType.Double, 0, "surcharge"));
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DSCHydraulicsID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DSCHydraulicsID", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DSCID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DSCID", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_HGL", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "HGL", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_HGL1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "HGL", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_deltaHGL", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "deltaHGL", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_deltaHGL1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "deltaHGL", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelID", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelID1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelID", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "scenarioID", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_surcharge", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "surcharge", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_surcharge1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "surcharge", System.Data.DataRowVersion.Original, null));
			// 
			// DSCHydraulicsDeleteCommand1
			// 
			this.DSCHydraulicsDeleteCommand1.CommandText = @"DELETE FROM DSCHydraulics WHERE (DSCHydraulicsID = ?) AND (DSCID = ?) AND (HGL = ? OR ? IS NULL AND HGL IS NULL) AND (deltaHGL = ? OR ? IS NULL AND deltaHGL IS NULL) AND (modelID = ? OR ? IS NULL AND modelID IS NULL) AND (scenarioID = ?) AND (surcharge = ? OR ? IS NULL AND surcharge IS NULL)";
			this.DSCHydraulicsDeleteCommand1.Connection = this.ModelCatalogConnection1;
			this.DSCHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DSCHydraulicsID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DSCHydraulicsID", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DSCID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DSCID", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_HGL", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "HGL", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_HGL1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "HGL", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_deltaHGL", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "deltaHGL", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_deltaHGL1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "deltaHGL", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelID", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelID1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelID", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "scenarioID", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_surcharge", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "surcharge", System.Data.DataRowVersion.Original, null));
			this.DSCHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_surcharge1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "surcharge", System.Data.DataRowVersion.Original, null));
			// 
			// LinkHydraulicsDataAdapter1
			// 
			this.LinkHydraulicsDataAdapter1.DeleteCommand = this.LinkHydraulicsDeleteCommand1;
			this.LinkHydraulicsDataAdapter1.InsertCommand = this.LinkHydraulicsInsertCommand1;
			this.LinkHydraulicsDataAdapter1.SelectCommand = this.LinkHydraulicsSelectCommand1;
			this.LinkHydraulicsDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																												 new System.Data.Common.DataTableMapping("Table", "LinkHydraulics", new System.Data.Common.DataColumnMapping[] {
																																																								   new System.Data.Common.DataColumnMapping("linkHydraulicsID", "linkHydraulicsID"),
																																																								   new System.Data.Common.DataColumnMapping("maxDSElev", "maxDSElev"),
																																																								   new System.Data.Common.DataColumnMapping("maxQ", "maxQ"),
																																																								   new System.Data.Common.DataColumnMapping("maxUSElev", "maxUSElev"),
																																																								   new System.Data.Common.DataColumnMapping("maxV", "maxV"),
																																																								   new System.Data.Common.DataColumnMapping("MLinkID", "MLinkID"),
																																																								   new System.Data.Common.DataColumnMapping("modelID", "modelID"),
																																																								   new System.Data.Common.DataColumnMapping("QqRatio", "QqRatio"),
																																																								   new System.Data.Common.DataColumnMapping("scenarioID", "scenarioID"),
																																																								   new System.Data.Common.DataColumnMapping("timeOfMaxQ", "timeOfMaxQ"),
																																																								   new System.Data.Common.DataColumnMapping("timeOfMaxV", "timeOfMaxV")})});
			this.LinkHydraulicsDataAdapter1.UpdateCommand = this.LinkHydraulicsUpdateCommand1;
			// 
			// LinkHydraulicsSelectCommand1
			// 
			this.LinkHydraulicsSelectCommand1.CommandText = "SELECT linkHydraulicsID, maxDSElev, maxQ, maxUSElev, maxV, MLinkID, modelID, QqRa" +
				"tio, scenarioID, timeOfMaxQ, timeOfMaxV FROM LinkHydraulics";
			this.LinkHydraulicsSelectCommand1.Connection = this.ModelCatalogConnection1;
			// 
			// LinkHydraulicsInsertCommand1
			// 
			this.LinkHydraulicsInsertCommand1.CommandText = "INSERT INTO LinkHydraulics(maxDSElev, maxQ, maxUSElev, maxV, MLinkID, modelID, Qq" +
				"Ratio, scenarioID, timeOfMaxQ, timeOfMaxV) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)" +
				"";
			this.LinkHydraulicsInsertCommand1.Connection = this.ModelCatalogConnection1;
			this.LinkHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("maxDSElev", System.Data.OleDb.OleDbType.Double, 0, "maxDSElev"));
			this.LinkHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("maxQ", System.Data.OleDb.OleDbType.Double, 0, "maxQ"));
			this.LinkHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("maxUSElev", System.Data.OleDb.OleDbType.Double, 0, "maxUSElev"));
			this.LinkHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("maxV", System.Data.OleDb.OleDbType.Double, 0, "maxV"));
			this.LinkHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("MLinkID", System.Data.OleDb.OleDbType.Integer, 0, "MLinkID"));
			this.LinkHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("modelID", System.Data.OleDb.OleDbType.Integer, 0, "modelID"));
			this.LinkHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("QqRatio", System.Data.OleDb.OleDbType.Double, 0, "QqRatio"));
			this.LinkHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"));
			this.LinkHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("timeOfMaxQ", System.Data.OleDb.OleDbType.DBDate, 0, "timeOfMaxQ"));
			this.LinkHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("timeOfMaxV", System.Data.OleDb.OleDbType.DBDate, 0, "timeOfMaxV"));
			// 
			// LinkHydraulicsUpdateCommand1
			// 
			this.LinkHydraulicsUpdateCommand1.CommandText = @"UPDATE LinkHydraulics SET maxDSElev = ?, maxQ = ?, maxUSElev = ?, maxV = ?, MLinkID = ?, modelID = ?, QqRatio = ?, scenarioID = ?, timeOfMaxQ = ?, timeOfMaxV = ? WHERE (linkHydraulicsID = ?) AND (MLinkID = ?) AND (QqRatio = ? OR ? IS NULL AND QqRatio IS NULL) AND (maxDSElev = ? OR ? IS NULL AND maxDSElev IS NULL) AND (maxQ = ? OR ? IS NULL AND maxQ IS NULL) AND (maxUSElev = ? OR ? IS NULL AND maxUSElev IS NULL) AND (maxV = ? OR ? IS NULL AND maxV IS NULL) AND (modelID = ? OR ? IS NULL AND modelID IS NULL) AND (scenarioID = ?) AND (timeOfMaxQ = ? OR ? IS NULL AND timeOfMaxQ IS NULL) AND (timeOfMaxV = ? OR ? IS NULL AND timeOfMaxV IS NULL)";
			this.LinkHydraulicsUpdateCommand1.Connection = this.ModelCatalogConnection1;
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("maxDSElev", System.Data.OleDb.OleDbType.Double, 0, "maxDSElev"));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("maxQ", System.Data.OleDb.OleDbType.Double, 0, "maxQ"));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("maxUSElev", System.Data.OleDb.OleDbType.Double, 0, "maxUSElev"));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("maxV", System.Data.OleDb.OleDbType.Double, 0, "maxV"));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("MLinkID", System.Data.OleDb.OleDbType.Integer, 0, "MLinkID"));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("modelID", System.Data.OleDb.OleDbType.Integer, 0, "modelID"));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("QqRatio", System.Data.OleDb.OleDbType.Double, 0, "QqRatio"));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("timeOfMaxQ", System.Data.OleDb.OleDbType.DBDate, 0, "timeOfMaxQ"));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("timeOfMaxV", System.Data.OleDb.OleDbType.DBDate, 0, "timeOfMaxV"));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_linkHydraulicsID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "linkHydraulicsID", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_MLinkID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "MLinkID", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_QqRatio", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "QqRatio", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_QqRatio1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "QqRatio", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxDSElev", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxDSElev", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxDSElev1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxDSElev", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxQ", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxQ", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxQ1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxQ", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxUSElev", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxUSElev", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxUSElev1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxUSElev", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxV", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxV", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxV1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxV", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelID", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelID1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelID", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "scenarioID", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeOfMaxQ", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeOfMaxQ", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeOfMaxQ1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeOfMaxQ", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeOfMaxV", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeOfMaxV", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeOfMaxV1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeOfMaxV", System.Data.DataRowVersion.Original, null));
			// 
			// LinkHydraulicsDeleteCommand1
			// 
			this.LinkHydraulicsDeleteCommand1.CommandText = @"DELETE FROM LinkHydraulics WHERE (linkHydraulicsID = ?) AND (MLinkID = ?) AND (QqRatio = ? OR ? IS NULL AND QqRatio IS NULL) AND (maxDSElev = ? OR ? IS NULL AND maxDSElev IS NULL) AND (maxQ = ? OR ? IS NULL AND maxQ IS NULL) AND (maxUSElev = ? OR ? IS NULL AND maxUSElev IS NULL) AND (maxV = ? OR ? IS NULL AND maxV IS NULL) AND (modelID = ? OR ? IS NULL AND modelID IS NULL) AND (scenarioID = ?) AND (timeOfMaxQ = ? OR ? IS NULL AND timeOfMaxQ IS NULL) AND (timeOfMaxV = ? OR ? IS NULL AND timeOfMaxV IS NULL)";
			this.LinkHydraulicsDeleteCommand1.Connection = this.ModelCatalogConnection1;
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_linkHydraulicsID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "linkHydraulicsID", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_MLinkID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "MLinkID", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_QqRatio", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "QqRatio", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_QqRatio1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "QqRatio", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxDSElev", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxDSElev", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxDSElev1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxDSElev", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxQ", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxQ", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxQ1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxQ", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxUSElev", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxUSElev", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxUSElev1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxUSElev", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxV", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxV", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxV1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxV", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelID", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelID1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelID", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "scenarioID", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeOfMaxQ", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeOfMaxQ", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeOfMaxQ1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeOfMaxQ", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeOfMaxV", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeOfMaxV", System.Data.DataRowVersion.Original, null));
			this.LinkHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeOfMaxV1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeOfMaxV", System.Data.DataRowVersion.Original, null));
			// 
			// ModelScenarioDataAdapter1
			// 
			this.ModelScenarioDataAdapter1.DeleteCommand = this.ModelScenarioDeleteCommand1;
			this.ModelScenarioDataAdapter1.InsertCommand = this.ModelScenarioInsertCommand1;
			this.ModelScenarioDataAdapter1.SelectCommand = this.ModelScenarioSelectCommand1;
			this.ModelScenarioDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																												new System.Data.Common.DataTableMapping("Table", "ModelScenario", new System.Data.Common.DataColumnMapping[] {
																																																								 new System.Data.Common.DataColumnMapping("description", "description"),
																																																								 new System.Data.Common.DataColumnMapping("scenarioID", "scenarioID"),
																																																								 new System.Data.Common.DataColumnMapping("stormID", "stormID")})});
			this.ModelScenarioDataAdapter1.UpdateCommand = this.ModelScenarioUpdateCommand1;
			// 
			// ModelScenarioSelectCommand1
			// 
			this.ModelScenarioSelectCommand1.CommandText = "SELECT description, scenarioID, stormID FROM ModelScenario";
			this.ModelScenarioSelectCommand1.Connection = this.ModelCatalogConnection1;
			// 
			// ModelScenarioInsertCommand1
			// 
			this.ModelScenarioInsertCommand1.CommandText = "INSERT INTO ModelScenario(description, stormID) VALUES (?, ?)";
			this.ModelScenarioInsertCommand1.Connection = this.ModelCatalogConnection1;
			this.ModelScenarioInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("description", System.Data.OleDb.OleDbType.VarWChar, 50, "description"));
			this.ModelScenarioInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("stormID", System.Data.OleDb.OleDbType.Integer, 0, "stormID"));
			// 
			// ModelScenarioUpdateCommand1
			// 
			this.ModelScenarioUpdateCommand1.CommandText = "UPDATE ModelScenario SET description = ?, stormID = ? WHERE (scenarioID = ?) AND " +
				"(description = ? OR ? IS NULL AND description IS NULL) AND (stormID = ?)";
			this.ModelScenarioUpdateCommand1.Connection = this.ModelCatalogConnection1;
			this.ModelScenarioUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("description", System.Data.OleDb.OleDbType.VarWChar, 50, "description"));
			this.ModelScenarioUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("stormID", System.Data.OleDb.OleDbType.Integer, 0, "stormID"));
			this.ModelScenarioUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "scenarioID", System.Data.DataRowVersion.Original, null));
			this.ModelScenarioUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_description", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "description", System.Data.DataRowVersion.Original, null));
			this.ModelScenarioUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_description1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "description", System.Data.DataRowVersion.Original, null));
			this.ModelScenarioUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_stormID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "stormID", System.Data.DataRowVersion.Original, null));
			// 
			// ModelScenarioDeleteCommand1
			// 
			this.ModelScenarioDeleteCommand1.CommandText = "DELETE FROM ModelScenario WHERE (scenarioID = ?) AND (description = ? OR ? IS NUL" +
				"L AND description IS NULL) AND (stormID = ?)";
			this.ModelScenarioDeleteCommand1.Connection = this.ModelCatalogConnection1;
			this.ModelScenarioDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "scenarioID", System.Data.DataRowVersion.Original, null));
			this.ModelScenarioDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_description", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "description", System.Data.DataRowVersion.Original, null));
			this.ModelScenarioDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_description1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "description", System.Data.DataRowVersion.Original, null));
			this.ModelScenarioDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_stormID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "stormID", System.Data.DataRowVersion.Original, null));
			// 
			// NodeHydraulicsDataAdapter1
			// 
			this.NodeHydraulicsDataAdapter1.DeleteCommand = this.NodeHydraulicsDeleteCommand1;
			this.NodeHydraulicsDataAdapter1.InsertCommand = this.NodeHydraulicsInsertCommand1;
			this.NodeHydraulicsDataAdapter1.SelectCommand = this.NodeHydraulicsSelectCommand1;
			this.NodeHydraulicsDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																												 new System.Data.Common.DataTableMapping("Table", "NodeHydraulics", new System.Data.Common.DataColumnMapping[] {
																																																								   new System.Data.Common.DataColumnMapping("floodedTime", "floodedTime"),
																																																								   new System.Data.Common.DataColumnMapping("freeboard", "freeboard"),
																																																								   new System.Data.Common.DataColumnMapping("maxElevation", "maxElevation"),
																																																								   new System.Data.Common.DataColumnMapping("modelID", "modelID"),
																																																								   new System.Data.Common.DataColumnMapping("nodeHydraulicsID", "nodeHydraulicsID"),
																																																								   new System.Data.Common.DataColumnMapping("nodeName", "nodeName"),
																																																								   new System.Data.Common.DataColumnMapping("scenarioID", "scenarioID"),
																																																								   new System.Data.Common.DataColumnMapping("surcharge", "surcharge"),
																																																								   new System.Data.Common.DataColumnMapping("surchargeTime", "surchargeTime"),
																																																								   new System.Data.Common.DataColumnMapping("timeOfMaxElev", "timeOfMaxElev")})});
			this.NodeHydraulicsDataAdapter1.UpdateCommand = this.NodeHydraulicsUpdateCommand1;
			// 
			// NodeHydraulicsSelectCommand1
			// 
			this.NodeHydraulicsSelectCommand1.CommandText = "SELECT floodedTime, freeboard, maxElevation, modelID, nodeHydraulicsID, nodeName," +
				" scenarioID, surcharge, surchargeTime, timeOfMaxElev FROM NodeHydraulics";
			this.NodeHydraulicsSelectCommand1.Connection = this.ModelCatalogConnection1;
			// 
			// NodeHydraulicsInsertCommand1
			// 
			this.NodeHydraulicsInsertCommand1.CommandText = "INSERT INTO NodeHydraulics(floodedTime, freeboard, maxElevation, modelID, nodeNam" +
				"e, scenarioID, surcharge, surchargeTime, timeOfMaxElev) VALUES (?, ?, ?, ?, ?, ?" +
				", ?, ?, ?)";
			this.NodeHydraulicsInsertCommand1.Connection = this.ModelCatalogConnection1;
			this.NodeHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("floodedTime", System.Data.OleDb.OleDbType.Double, 0, "floodedTime"));
			this.NodeHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("freeboard", System.Data.OleDb.OleDbType.Double, 0, "freeboard"));
			this.NodeHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("maxElevation", System.Data.OleDb.OleDbType.Double, 0, "maxElevation"));
			this.NodeHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("modelID", System.Data.OleDb.OleDbType.Integer, 0, "modelID"));
			this.NodeHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("nodeName", System.Data.OleDb.OleDbType.VarWChar, 6, "nodeName"));
			this.NodeHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"));
			this.NodeHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("surcharge", System.Data.OleDb.OleDbType.Double, 0, "surcharge"));
			this.NodeHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("surchargeTime", System.Data.OleDb.OleDbType.Double, 0, "surchargeTime"));
			this.NodeHydraulicsInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("timeOfMaxElev", System.Data.OleDb.OleDbType.DBDate, 0, "timeOfMaxElev"));
			// 
			// NodeHydraulicsUpdateCommand1
			// 
			this.NodeHydraulicsUpdateCommand1.CommandText = @"UPDATE NodeHydraulics SET floodedTime = ?, freeboard = ?, maxElevation = ?, modelID = ?, nodeName = ?, scenarioID = ?, surcharge = ?, surchargeTime = ?, timeOfMaxElev = ? WHERE (nodeHydraulicsID = ?) AND (floodedTime = ? OR ? IS NULL AND floodedTime IS NULL) AND (freeboard = ? OR ? IS NULL AND freeboard IS NULL) AND (maxElevation = ? OR ? IS NULL AND maxElevation IS NULL) AND (modelID = ?) AND (nodeName = ?) AND (scenarioID = ?) AND (surcharge = ? OR ? IS NULL AND surcharge IS NULL) AND (surchargeTime = ? OR ? IS NULL AND surchargeTime IS NULL) AND (timeOfMaxElev = ? OR ? IS NULL AND timeOfMaxElev IS NULL)";
			this.NodeHydraulicsUpdateCommand1.Connection = this.ModelCatalogConnection1;
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("floodedTime", System.Data.OleDb.OleDbType.Double, 0, "floodedTime"));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("freeboard", System.Data.OleDb.OleDbType.Double, 0, "freeboard"));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("maxElevation", System.Data.OleDb.OleDbType.Double, 0, "maxElevation"));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("modelID", System.Data.OleDb.OleDbType.Integer, 0, "modelID"));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("nodeName", System.Data.OleDb.OleDbType.VarWChar, 6, "nodeName"));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("scenarioID", System.Data.OleDb.OleDbType.Integer, 0, "scenarioID"));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("surcharge", System.Data.OleDb.OleDbType.Double, 0, "surcharge"));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("surchargeTime", System.Data.OleDb.OleDbType.Double, 0, "surchargeTime"));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("timeOfMaxElev", System.Data.OleDb.OleDbType.DBDate, 0, "timeOfMaxElev"));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_nodeHydraulicsID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "nodeHydraulicsID", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_floodedTime", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "floodedTime", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_floodedTime1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "floodedTime", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_freeboard", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "freeboard", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_freeboard1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "freeboard", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxElevation", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxElevation", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxElevation1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxElevation", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelID", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_nodeName", System.Data.OleDb.OleDbType.VarWChar, 6, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "nodeName", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "scenarioID", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_surcharge", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "surcharge", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_surcharge1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "surcharge", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_surchargeTime", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "surchargeTime", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_surchargeTime1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "surchargeTime", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeOfMaxElev", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeOfMaxElev", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeOfMaxElev1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeOfMaxElev", System.Data.DataRowVersion.Original, null));
			// 
			// NodeHydraulicsDeleteCommand1
			// 
			this.NodeHydraulicsDeleteCommand1.CommandText = @"DELETE FROM NodeHydraulics WHERE (nodeHydraulicsID = ?) AND (floodedTime = ? OR ? IS NULL AND floodedTime IS NULL) AND (freeboard = ? OR ? IS NULL AND freeboard IS NULL) AND (maxElevation = ? OR ? IS NULL AND maxElevation IS NULL) AND (modelID = ?) AND (nodeName = ?) AND (scenarioID = ?) AND (surcharge = ? OR ? IS NULL AND surcharge IS NULL) AND (surchargeTime = ? OR ? IS NULL AND surchargeTime IS NULL) AND (timeOfMaxElev = ? OR ? IS NULL AND timeOfMaxElev IS NULL)";
			this.NodeHydraulicsDeleteCommand1.Connection = this.ModelCatalogConnection1;
			this.NodeHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_nodeHydraulicsID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "nodeHydraulicsID", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_floodedTime", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "floodedTime", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_floodedTime1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "floodedTime", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_freeboard", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "freeboard", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_freeboard1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "freeboard", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxElevation", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxElevation", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_maxElevation1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "maxElevation", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_modelID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "modelID", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_nodeName", System.Data.OleDb.OleDbType.VarWChar, 6, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "nodeName", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_scenarioID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "scenarioID", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_surcharge", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "surcharge", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_surcharge1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "surcharge", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_surchargeTime", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "surchargeTime", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_surchargeTime1", System.Data.OleDb.OleDbType.Double, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "surchargeTime", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeOfMaxElev", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeOfMaxElev", System.Data.DataRowVersion.Original, null));
			this.NodeHydraulicsDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeOfMaxElev1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeOfMaxElev", System.Data.DataRowVersion.Original, null));
			// 
			// StormCatalogDataAdapter1
			// 
			this.StormCatalogDataAdapter1.DeleteCommand = this.StormCatalogDeleteCommand1;
			this.StormCatalogDataAdapter1.InsertCommand = this.StormCatalogInsertCommand1;
			this.StormCatalogDataAdapter1.SelectCommand = this.StormCatalogSelectCommand1;
			this.StormCatalogDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																											   new System.Data.Common.DataTableMapping("Table", "StormCatalog", new System.Data.Common.DataColumnMapping[] {
																																																							   new System.Data.Common.DataColumnMapping("description", "description"),
																																																							   new System.Data.Common.DataColumnMapping("duration", "duration"),
																																																							   new System.Data.Common.DataColumnMapping("interfaceFile", "interfaceFile"),
																																																							   new System.Data.Common.DataColumnMapping("recurrenceInterval", "recurrenceInterval"),
																																																							   new System.Data.Common.DataColumnMapping("startDate", "startDate"),
																																																							   new System.Data.Common.DataColumnMapping("stormID", "stormID"),
																																																							   new System.Data.Common.DataColumnMapping("stormName", "stormName"),
																																																							   new System.Data.Common.DataColumnMapping("timeStep", "timeStep")})});
			this.StormCatalogDataAdapter1.UpdateCommand = this.StormCatalogUpdateCommand1;
			// 
			// StormCatalogSelectCommand1
			// 
			this.StormCatalogSelectCommand1.CommandText = "SELECT description, duration, interfaceFile, recurrenceInterval, startDate, storm" +
				"ID, stormName, timeStep FROM StormCatalog";
			this.StormCatalogSelectCommand1.Connection = this.ModelCatalogConnection1;
			// 
			// StormCatalogInsertCommand1
			// 
			this.StormCatalogInsertCommand1.CommandText = "INSERT INTO StormCatalog(description, duration, interfaceFile, recurrenceInterval" +
				", startDate, stormName, timeStep) VALUES (?, ?, ?, ?, ?, ?, ?)";
			this.StormCatalogInsertCommand1.Connection = this.ModelCatalogConnection1;
			this.StormCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("description", System.Data.OleDb.OleDbType.VarWChar, 255, "description"));
			this.StormCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("duration", System.Data.OleDb.OleDbType.Integer, 0, "duration"));
			this.StormCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("interfaceFile", System.Data.OleDb.OleDbType.VarWChar, 255, "interfaceFile"));
			this.StormCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("recurrenceInterval", System.Data.OleDb.OleDbType.Integer, 0, "recurrenceInterval"));
			this.StormCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("startDate", System.Data.OleDb.OleDbType.DBDate, 0, "startDate"));
			this.StormCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("stormName", System.Data.OleDb.OleDbType.VarWChar, 50, "stormName"));
			this.StormCatalogInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("timeStep", System.Data.OleDb.OleDbType.Integer, 0, "timeStep"));
			// 
			// StormCatalogUpdateCommand1
			// 
			this.StormCatalogUpdateCommand1.CommandText = @"UPDATE StormCatalog SET description = ?, duration = ?, interfaceFile = ?, recurrenceInterval = ?, startDate = ?, stormName = ?, timeStep = ? WHERE (stormID = ?) AND (description = ? OR ? IS NULL AND description IS NULL) AND (duration = ? OR ? IS NULL AND duration IS NULL) AND (interfaceFile = ? OR ? IS NULL AND interfaceFile IS NULL) AND (recurrenceInterval = ? OR ? IS NULL AND recurrenceInterval IS NULL) AND (startDate = ? OR ? IS NULL AND startDate IS NULL) AND (stormName = ? OR ? IS NULL AND stormName IS NULL) AND (timeStep = ? OR ? IS NULL AND timeStep IS NULL)";
			this.StormCatalogUpdateCommand1.Connection = this.ModelCatalogConnection1;
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("description", System.Data.OleDb.OleDbType.VarWChar, 255, "description"));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("duration", System.Data.OleDb.OleDbType.Integer, 0, "duration"));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("interfaceFile", System.Data.OleDb.OleDbType.VarWChar, 255, "interfaceFile"));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("recurrenceInterval", System.Data.OleDb.OleDbType.Integer, 0, "recurrenceInterval"));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("startDate", System.Data.OleDb.OleDbType.DBDate, 0, "startDate"));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("stormName", System.Data.OleDb.OleDbType.VarWChar, 50, "stormName"));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("timeStep", System.Data.OleDb.OleDbType.Integer, 0, "timeStep"));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_stormID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "stormID", System.Data.DataRowVersion.Original, null));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_description", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "description", System.Data.DataRowVersion.Original, null));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_description1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "description", System.Data.DataRowVersion.Original, null));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_duration", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "duration", System.Data.DataRowVersion.Original, null));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_duration1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "duration", System.Data.DataRowVersion.Original, null));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_interfaceFile", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "interfaceFile", System.Data.DataRowVersion.Original, null));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_interfaceFile1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "interfaceFile", System.Data.DataRowVersion.Original, null));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_recurrenceInterval", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "recurrenceInterval", System.Data.DataRowVersion.Original, null));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_recurrenceInterval1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "recurrenceInterval", System.Data.DataRowVersion.Original, null));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_startDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "startDate", System.Data.DataRowVersion.Original, null));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_startDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "startDate", System.Data.DataRowVersion.Original, null));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_stormName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "stormName", System.Data.DataRowVersion.Original, null));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_stormName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "stormName", System.Data.DataRowVersion.Original, null));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeStep", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeStep", System.Data.DataRowVersion.Original, null));
			this.StormCatalogUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeStep1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeStep", System.Data.DataRowVersion.Original, null));
			// 
			// StormCatalogDeleteCommand1
			// 
			this.StormCatalogDeleteCommand1.CommandText = @"DELETE FROM StormCatalog WHERE (stormID = ?) AND (description = ? OR ? IS NULL AND description IS NULL) AND (duration = ? OR ? IS NULL AND duration IS NULL) AND (interfaceFile = ? OR ? IS NULL AND interfaceFile IS NULL) AND (recurrenceInterval = ? OR ? IS NULL AND recurrenceInterval IS NULL) AND (startDate = ? OR ? IS NULL AND startDate IS NULL) AND (stormName = ? OR ? IS NULL AND stormName IS NULL) AND (timeStep = ? OR ? IS NULL AND timeStep IS NULL)";
			this.StormCatalogDeleteCommand1.Connection = this.ModelCatalogConnection1;
			this.StormCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_stormID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "stormID", System.Data.DataRowVersion.Original, null));
			this.StormCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_description", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "description", System.Data.DataRowVersion.Original, null));
			this.StormCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_description1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "description", System.Data.DataRowVersion.Original, null));
			this.StormCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_duration", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "duration", System.Data.DataRowVersion.Original, null));
			this.StormCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_duration1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "duration", System.Data.DataRowVersion.Original, null));
			this.StormCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_interfaceFile", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "interfaceFile", System.Data.DataRowVersion.Original, null));
			this.StormCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_interfaceFile1", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "interfaceFile", System.Data.DataRowVersion.Original, null));
			this.StormCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_recurrenceInterval", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "recurrenceInterval", System.Data.DataRowVersion.Original, null));
			this.StormCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_recurrenceInterval1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "recurrenceInterval", System.Data.DataRowVersion.Original, null));
			this.StormCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_startDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "startDate", System.Data.DataRowVersion.Original, null));
			this.StormCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_startDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "startDate", System.Data.DataRowVersion.Original, null));
			this.StormCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_stormName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "stormName", System.Data.DataRowVersion.Original, null));
			this.StormCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_stormName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "stormName", System.Data.DataRowVersion.Original, null));
			this.StormCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeStep", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeStep", System.Data.DataRowVersion.Original, null));
			this.StormCatalogDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_timeStep1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "timeStep", System.Data.DataRowVersion.Original, null));
			// 
			// CharacterizationForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(496, 333);
			this.Name = "CharacterizationForm";
			this.Text = "Characterization Queries Manager";

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
	}
}
