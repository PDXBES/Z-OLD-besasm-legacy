// Project: UI, File: Engine.cs
// Namespace: ArcSpooler.UI, Class: Engine
// Path: D:\Development\ArcSpooler\UI, Author: arnelm
// Code lines: 21, Size of file: 322 Bytes
// Creation date: 11/2/2008 11:48 PM
// Last modified: 3/6/2009 1:55 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Display;
//using map2pdf_esri;
using log4net;
using log4net.Config;
#endregion

namespace ArcSpooler.UI
{
	/// <summary>
	/// Engine
	/// </summary>
	public class Engine
	{
		#region Fields
		private ArcLicenser _ArcLicenser = new ArcLicenser();
		private SpoolerConfig _Config;
		private DataSet _SourceDataset;
		private DataTable _SourceTable;
		private List<string> _SourceKeys = new List<string>();
		private List<string> _ProcessKeys = new List<string>();
		private List<string> _ErrorKeys = new List<string>();

		private static readonly log4net.ILog log = LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		#endregion

		#region Constructors
		/// <summary>
		/// Create engine
		/// </summary>
		/// <param name="fileName">File name</param>
		public Engine(string fileName)
		{
			_Config = new SpoolerConfig(fileName);

			InitializeDataSource();

			if (_Config.SelectionSetNames.Count > 0)
				SetSelection(_Config.SelectionSetNames[0]);

			if (_Config.CreateJobLog)
			{
				string jobLogFileName = System.IO.Path.GetDirectoryName(fileName) +
					System.IO.Path.DirectorySeparatorChar + 
					System.IO.Path.GetFileNameWithoutExtension(fileName) +
					".log";

				XmlConfigurator.Configure(new System.IO.FileInfo("Logging.xml"));
			} // if
			else
			{
				//log4net.ThreadContext.Properties["LogLevel"] = "OFF";
			} // else
			//log4net.Config.XmlConfigurator.Configure();
		} // Engine(fileName)
		#endregion

		#region Properties
		/// <summary>
		/// Has data source
		/// </summary>
		/// <returns>Bool</returns>
		public bool HasDataSource
		{
			get
			{
				return _SourceDataset != null;
			} // get
		} // HasDataSource

		/// <summary>
		/// Num data records
		/// </summary>
		/// <returns>Int</returns>
		public int NumDataRecords
		{
			get
			{
				return _SourceKeys.Count();
			} // get
		} // NumDataRecords

		/// <summary>
		/// _Source keys
		/// </summary>
		/// <returns>List</returns>
		public List<string> SourceKeys
		{
			get
			{
				return _SourceKeys;
			} // get
		} // SourceKeys

		/// <summary>
		/// Process keys
		/// </summary>
		/// <returns>List</returns>
		public List<string> ProcessKeys
		{
			get
			{
				return _ProcessKeys;
			} // get
		} // ProcessKeys

		/// <summary>
		/// Error keys
		/// </summary>
		/// <returns>List</returns>
		public List<string> ErrorKeys
		{
			get
			{
				return _ErrorKeys;
			} // get
		} // ErrorKeys

		/// <summary>
		/// Selection set names
		/// </summary>
		/// <returns>IEnumerable</returns>
		public IEnumerable<string> SelectionSetNames
		{
			get
			{
				foreach (string item in _Config.SelectionSetNames)
					yield return item;
			} // get
		} // SelectionSetNames

		/// <summary>
		/// Engine template file
		/// </summary>
		/// <returns>String</returns>
		public IEnumerable<string> EngineTemplateNamedElements
		{
			get
			{
				if (!_Config.TemplateDocumentLoaded)
					_Config.ReloadTemplateFile();
				MapDocumentHelper mdocHelper = new MapDocumentHelper(_Config.TemplateDocument);
				foreach (IElement item in mdocHelper.NamedElements)
					yield return (item as IElementProperties).Name;
			} // get
		} // EngineTemplateNamedElements
		#endregion

		#region Methods
		/// <summary>
		/// Initialize data source
		/// </summary>
		public void InitializeDataSource()
		{
			OleDbConnection conn = new OleDbConnection(_Config.SourceConnectionString);
			OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM " + _Config.SourceTable, conn);
			OleDbCommandBuilder cmd = new OleDbCommandBuilder(adapter);
			conn.Open();
			_SourceDataset = new DataSet();
			adapter.Fill(_SourceDataset, "SourceTable");
			conn.Close();
			DataTableCollection tables = _SourceDataset.Tables;
			_SourceTable = tables["SourceTable"];
			var rows = _SourceTable.AsEnumerable();
			var sourceKeys = from row in rows
											 group row by row.Field<string>(_Config.SourceField) into g
											 select new { key = g.Key };
			foreach (var item in sourceKeys)
			{
				_SourceKeys.Add(item.key);
			} // foreach  (item)
		} // InitializeDataSource()

		/// <summary>
		/// Run
		/// </summary>
		public void Run(BackgroundWorker bw)
		{
			try
			{
				WriteStartJob();
				WriteJobList();

				int jobCounter = 0;
				_ErrorKeys.Clear();

				foreach (string key in _ProcessKeys)
				{
					if (bw != null && bw.CancellationPending)
						break;
					jobCounter++;
					if (bw != null)
						bw.ReportProgress(jobCounter);
					ProduceOutput(key);
					GC.Collect();
					GC.WaitForPendingFinalizers();
				} // foreach  (key)

				WriteErrorJobs();
			} // try
			finally
			{
				WriteEndJob();
			} // finally
		} // Run()

		/// <summary>
		/// Produce output
		/// </summary>
		public void ProduceOutput(string key)
		{
			IMapDocument doc = _Config.TemplateDocument;
			_Config.ReloadTemplateFile();

			try
			{
				WriteJob(key);

				RenderContent(doc, key);
				if (_Config.CreateMXD)
				{
					SaveMXD(key, doc);
					WriteJobOutput(MXDFileName(key));
				} // if

				if (_Config.CreatePDF)
				{
						SavePDF(key, doc, _Config.CreateGeoPDF);
						WriteJobOutput(PDFFileName(key));
				} // if
			} // try
			catch (Exception e)
			{
				// TODO: add another list or redef the _ErrorKeys list to include error message in list for display
				_ErrorKeys.Add(key);
			} // catch
		} // ProduceOutput(key)

		/// <summary>
		/// Render content
		/// </summary>
		/// <param name="doc">Doc</param>
		public void RenderContent(IMapDocument doc, string key)
		{
			MapDocumentHelper mdocHelper = new MapDocumentHelper(doc);
			ShiftMapFrames(key, mdocHelper);
			RenderTextFields(mdocHelper, key);
			RenderDynamicTextFields(mdocHelper, key);
			RenderTables(mdocHelper, key);
			RenderGroups(mdocHelper, key);
		} // RenderContent(doc, key)

		/// <summary>
		/// Shift map frames
		/// </summary>
		/// <param name="key">Key</param>
		/// <param name="mdocHelper">Mdoc helper</param>
		private void ShiftMapFrames(string key, MapDocumentHelper mdocHelper)
		{
			try
			{
				mdocHelper.ShiftMapFrame(_Config.MasterDataFrame.Name,
					_Config.MasterDataFrame.LayerToShift, _Config.MasterDataFrame.LayerKeyField,
					key, _Config.MasterDataFrame.ZoomToObject, _Config.MasterDataFrame.RotateMap,
					_Config.MasterDataFrame.ScaleInterval);
			} // try
			catch (ArgumentException e)
			{
				throw;
			} // catch

			foreach (HighlightLayer item in _Config.MasterDataFrame.HighlightLayers)
			{
				item.Value = key;
				mdocHelper.ChangeDefinitionQuery(mdocHelper.Map(_Config.MasterDataFrame.Name),
					item.LayerName, item.DefQuery);
			} // foreach  (item)

			foreach (DataFrame frame in _Config.DataFrames)
			{
				mdocHelper.ShiftMapFrame(_Config.MasterDataFrame.Name, frame.Name,
					frame.MatchMasterZoom, _Config.MasterDataFrame.RotateMap);

				foreach (HighlightLayer item in frame.HighlightLayers)
				{
					item.Value = key;
					mdocHelper.ChangeDefinitionQuery(mdocHelper.Map(frame.Name),
						item.LayerName, item.DefQuery);
				} // foreach  (item)
			} // foreach  (frame)
		} // ShiftMapFrames(key, mdocHelper)

		/// <summary>
		/// Render text fields
		/// </summary>
		/// <param name="mdocHelper">Mdoc helper</param>
		private void RenderTextFields(MapDocumentHelper mdocHelper, string key)
		{
			foreach (TextField item in _Config.TextFields)
			{
				ITextElement textField = mdocHelper.TextElement(item.Name);
				string specialFieldContent = string.Empty;
				if (item.ModifyTo == "$FILENAME")
					specialFieldContent = MXDFileName(key);
				else if (item.ModifyTo == "$DATE")
					specialFieldContent = string.Format("{0:d}", DateTime.Now);
				textField.Text = (specialFieldContent == string.Empty) ? item.ModifyTo : specialFieldContent;

				if (item.BoundaryFrame.Length > 0)
				{
					IRectangleElement boundingRect = mdocHelper.RectangleElement(item.BoundaryFrame);
					IEnvelope rectBounds = (boundingRect as IElement).Geometry.Envelope;
					rectBounds.Expand(-item.BorderXFromBoundary, -item.BorderYFromBoundary, false);
					(textField as IElement).Geometry = rectBounds;
				} // if
			} // foreach  (item)
		} // RenderTextFields(mdocHelper, key)

		/// <summary>
		/// Render dynamic text fields
		/// </summary>
		/// <param name="mdocHelper">Mdoc helper</param>
		private void RenderDynamicTextFields(MapDocumentHelper mdocHelper, string key)
		{
			foreach (DynamicTextField item in _Config.DynamicTextFields)
			{
				ITextElement textField = mdocHelper.TextElement(item.Name);
				var rows = _SourceTable.AsEnumerable();
				var selectedRow = from row in rows
													where row.Field<string>(_Config.SourceField) == key
													select row;
				item.ReplaceString = selectedRow.First().Field<string>(item.ModifyToField);
				textField.Text = item.ModifyTo;

				if (item.BoundaryFrame.Length > 0)
				{
					IRectangleElement boundingRect = mdocHelper.RectangleElement(item.BoundaryFrame);
					IEnvelope rectBounds = (boundingRect as IElement).Geometry.Envelope;
					rectBounds.Expand(-item.BorderXFromBoundary, -item.BorderYFromBoundary, false);
					(textField as IElement).Geometry = rectBounds;
				} // if
			} // foreach  (item)
		} // RenderDynamicTextFields(mdocHelper, key)

		/// <summary>
		/// Render tables
		/// </summary>
		/// <param name="MapDocumentHelper">Map document helper</param>
		/// <param name="key">Key</param>
		private void RenderTables(MapDocumentHelper mdocHelper, string key)
		{
			foreach (Table table in _Config.Tables)
			{
				IMap masterMap = (from item in mdocHelper.Maps
												 where item.Name == _Config.MasterDataFrame.Name
												 select item).First();
				mdocHelper.AddTableToLayout(masterMap, table.LayerName, table.Name, table.OrderByField);
			} // foreach  (table)
		} // RenderTables(mdocHelper, key)

		/// <summary>
		/// Render groups
		/// </summary>
		/// <param name="mdocHelper">Mdoc helper</param>
		private void RenderGroups(MapDocumentHelper mdocHelper, string key)
		{
			foreach (Group item in _Config.Groups)
			{
				OleDbConnection conn = new OleDbConnection(_Config.SourceConnectionString);
				OleDbDataAdapter adapter = new OleDbDataAdapter(string.Format(item.Sql, key), conn);
				OleDbCommandBuilder cmd = new OleDbCommandBuilder(adapter);
				conn.Open();
				DataSet groupDataset = new DataSet();
				adapter.Fill(groupDataset, "GroupTable");
				conn.Close();
				DataTableCollection tables = groupDataset.Tables;
				DataTable groupTable = tables["GroupTable"];
				var rows = groupTable.AsEnumerable();

				var groupRows = from row in rows
												 orderby item.Slot
												 select row;

				foreach (var groupRow in groupRows)
				{
					for (int i = 0; i < item.NumDynamicTextFields; i++)
					{
						DynamicTextField dynamicTextField = item.DynamicTextField(i);
						dynamicTextField.Slot = groupRow.Field<int>(item.Slot);
						dynamicTextField.ReplaceString = groupRow.Field<string>(dynamicTextField.ModifyToField);
						ITextElement textField = mdocHelper.TextElement(dynamicTextField.Name);
						textField.Text = dynamicTextField.ModifyTo;
					} // for

					for (int i = 0; i < item.NumDynamicPictureFields; i++)
					{
						DynamicPictureField dynamicPictureField = item.DynamicPictureField(i);
						dynamicPictureField.Slot = groupRow.Field<int>(item.Slot);
						IRectangleElement rectangle = mdocHelper.RectangleElement(dynamicPictureField.Name);
						IEnvelope rectBounds = (rectangle as IElement).Geometry.Envelope;
						
						string pathToPicture = groupRow.Field<string>(dynamicPictureField.ModifyToField).Trim(new char[] {'#'});

						Image pic = new Bitmap(pathToPicture);
						Size picSize = pic.Size;
						IPageLayout layout = _Config.TemplateDocument.PageLayout;
						IGraphicsContainer graphicsContainer = layout as IGraphicsContainer;
						IPictureElement bitmap = null;
						if (pic.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
							bitmap = new BmpPictureElementClass();
						else if (pic.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
							bitmap = new GifPictureElementClass();
						else if (pic.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
							bitmap = new JpgPictureElementClass();
						else if (pic.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
							bitmap = new PngPictureElementClass();
						else if (pic.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
							bitmap = new TifPictureElementClass();

						if (bitmap != null)
						{
							bitmap.ImportPictureFromFile(pathToPicture);
							bitmap.SavePictureInDocument = false;
							bitmap.MaintainAspectRatio = true;

							double rectAspectRatio = rectBounds.Width / rectBounds.Height;
							double picAspectRatio = bitmap.PictureAspectRatio;
							if (picAspectRatio > rectAspectRatio)
								rectBounds.Expand(0, -(rectBounds.Height - rectBounds.Width / picAspectRatio) / 2, false);
							else
								rectBounds.Expand(-(rectBounds.Width - rectBounds.Height * picAspectRatio) / 2, 0, false);
							(bitmap as IElement).Geometry = rectBounds;

							graphicsContainer.AddElement(bitmap as IElement, 0);
						} // if
					} // for
				} // foreach  (groupRow)
			} // foreach  (item)
		}	// RenderGroups(mdocHelper)

		private void SaveMXD(string key, IMapDocument doc)
		{
			string outputName = MXDFileName(key);
			doc.SaveAs(outputName, false, true);
		} // SaveMXD(key, doc)

		/// <summary>
		/// MXD file name
		/// </summary>
		/// <param name="key">Key</param>
		/// <returns>String</returns>
		private string MXDFileName(string key)
		{
			string outputName = _Config.OutputPath.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()) ?
											_Config.OutputPath + key + ".mxd" :
											_Config.OutputPath + System.IO.Path.DirectorySeparatorChar + key + ".mxd";
			return outputName;
		} // MXDFileName(key)

		/// <summary>
		/// Save PDF
		/// </summary>
		/// <param name="key">Key</param>
		/// <param name="doc">Doc</param>
		private void SavePDF(string key, IMapDocument doc)
		{
			SavePDF(key, doc, false);
		} // SavePDF(key, doc)

		/// <summary>
		/// Save PDF
		/// </summary>
		/// <param name="key">Key</param>
		/// <param name="doc">Doc</param>
		/// <param name="geoPDF">Doc</param>
		private void SavePDF(string key, IMapDocument doc, bool createGeoPDF)
		{
			//IExport pdfExporter = createGeoPDF ?
			//	new ExportGeoPDFClass() { } as IExport :
			//	new ExportPDFClass() { EmbedFonts = true } as IExport;
			IExport pdfExporter = new ExportPDFClass() { EmbedFonts = true } as IExport;

			int screenResolution = GetScreenResolution();
			int exportResolution = _Config.PDFResolution;
			double convertResFactor = exportResolution / screenResolution;

			double pageWidth;
			double pageHeight;
			doc.PageLayout.Page.QuerySize(out pageWidth, out pageHeight);

			IEnvelope physicalPageExtent = new EnvelopeClass();
			physicalPageExtent.PutCoords(0, 0, pageWidth, pageHeight);

			IEnvelope pdfPageExtent = new EnvelopeClass();
			pdfPageExtent.PutCoords(physicalPageExtent.XMin * exportResolution,
				physicalPageExtent.YMin * exportResolution,
				physicalPageExtent.XMax * exportResolution,
				physicalPageExtent.YMax * exportResolution);

			IActiveView pageLayoutView = doc.PageLayout as IActiveView;

			//IExportGeoPDF2 geoPDFExporter = null;
			//if (createGeoPDF)
			//{
			//	geoPDFExporter = pdfExporter as IExportGeoPDF2;
			//	geoPDFExporter.ActiveView = pageLayoutView;
			//}

			tagRECT exportBounds;
			exportBounds.left = Convert.ToInt32(physicalPageExtent.XMin * exportResolution);
			exportBounds.right = Convert.ToInt32(physicalPageExtent.XMax * exportResolution);
			exportBounds.top = Convert.ToInt32(physicalPageExtent.YMin * exportResolution);
			exportBounds.bottom = Convert.ToInt32(physicalPageExtent.YMax * exportResolution);

			IEnvelope visibleBounds = new EnvelopeClass();
			visibleBounds.PutCoords(pageLayoutView.ExportFrame.left * convertResFactor,
				pageLayoutView.ExportFrame.top * convertResFactor,
				pageLayoutView.ExportFrame.right * convertResFactor,
				pageLayoutView.ExportFrame.bottom * convertResFactor);

			string outputName = PDFFileName(key);
			pdfExporter.ExportFileName = outputName;
			pdfExporter.PixelBounds = pdfPageExtent;
			pdfExporter.Resolution = exportResolution;

			int hDC = pdfExporter.StartExporting();
			if (!createGeoPDF)
				pageLayoutView.Output(hDC, exportResolution, ref exportBounds,
					null, null);
			pdfExporter.FinishExporting();
			pdfExporter.Cleanup();
		} // SavePDF(key, doc)

		private string PDFFileName(string key)
		{
			string outputName = _Config.OutputPath.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()) ?
											_Config.OutputPath + key + ".pdf" :
											_Config.OutputPath + System.IO.Path.DirectorySeparatorChar + key + ".pdf";
			return outputName;
		}

		/// <summary>
		/// Get screen resolution
		/// </summary>
		/// <returns>Int</returns>
		public int GetScreenResolution()
		{
			System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(IntPtr.Zero);
			return Convert.ToInt16(g.DpiX);
		} // GetScreenResolution()

		/// <summary>
		/// Selection set name present
		/// </summary>
		/// <param name="name">Name</param>
		/// <returns>Bool</returns>
		public bool SelectionSetNamePresent(string name)
		{
			return ((from item in SelectionSetNames where item == name select item).Count() > 0);
				
		} // SelectionSetNamePresent(name)

		/// <summary>
		/// Set selection
		/// </summary>
		/// <param name="name">Name</param>
		public void SetSelection(string name)
		{
			_ProcessKeys.Clear();
			foreach (string selection in _Config.SelectionSet(name))
			{
				var isInSource = from item in _SourceKeys
												 where item == selection
												 select item;
				if (isInSource.Count() > 0)
					_ProcessKeys.Add(selection);
			} // foreach  (selection)
		} // SetSelection(name)

		/// <summary>
		/// Save selection
		/// </summary>
		/// <param name="name">Name</param>
		/// <param name="selectionSet">Selection set</param>
		public void SaveSelection(string name, List<string> selectionSet)
		{
			_Config.SaveSelectionSet(name, selectionSet);
		} // SaveSelection(name, selectionSet)

		/// <summary>
		/// Delete selection
		/// </summary>
		/// <param name="name">Name</param>
		public void DeleteSelection(string name)
		{
			_Config.DeleteSelectionSet(name);
		} // DeleteSelection(name)

		#region Logging functions
		/// <summary>
		/// Write start job
		/// </summary>
		public void WriteStartJob()
		{
			if (log.IsDebugEnabled)
				log.DebugFormat("Start job: {0}", _Config.ConfigFileName);
		} // WriteStartJob()

		/// <summary>
		/// Write job list
		/// </summary>
		public void WriteJobList()
		{
			if (log.IsDebugEnabled)
			{
				log.Debug("Job:");
				log.Debug("Job:");
				log.Debug("  Template MXD: " + _Config.TemplateFileName);
				log.Debug("  Output path: " + _Config.OutputPath);
				log.Debug("  Source database: " + _Config.SourceConnectionString);
				log.Debug("  Source table: " + _Config.SourceTable);
				log.Debug("  Source field: " + _Config.SourceField);
				log.Debug("  Job items:");
				foreach (string item in _ProcessKeys)
					log.Debug("    " + item);
			}
		} // WriteJobList()

		/// <summary>
		/// Write job
		/// </summary>
		public void WriteJob(string key)
		{
			if (log.IsDebugEnabled)
				log.DebugFormat("Job ID {0}", key);
		} // WriteJob()

		/// <summary>
		/// Write job output
		/// </summary>
		/// <param name="filename">Filename</param>
		public void WriteJobOutput(string fileName)
		{
			if (log.IsDebugEnabled)
				log.DebugFormat("  file {0}", fileName);
		} // WriteJobOutput(filename)

		/// <summary>
		/// Write error jobs
		/// </summary>
		public void WriteErrorJobs()
		{
			if (log.IsDebugEnabled && _ErrorKeys.Count > 0)
			{
				log.Debug("Error items:");
				foreach (string item in _ErrorKeys)
					log.Debug("  " + item);
			}
		} // WriteErrorJobs()

		/// <summary>
		/// Write end job
		/// </summary>
		public void WriteEndJob()
		{
			if (log.IsDebugEnabled)
				log.Debug("End job");
		} // WriteEndJob()
		#endregion

		#endregion
	}
}
