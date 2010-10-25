using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using SystemsAnalysis.Tracer;
using System;
using System.Runtime.InteropServices;

namespace SystemsAnalysis.Tracer
{		
	public delegate void OnClickEventHandler(object sender, EventArgs e);
	public delegate void OnMouseDownEventHandler(object sender, ClickEventArgs e);
	
	[Guid("cfbcb1df-74f6-4d72-aef8-cf7648d166ff")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("SystemsAnalysis.FeatureClassTracer.TracerExtension")]
	public class TracerExtension : IExtension, IExtensionConfig, IDocumentEvents
	{	
		private ESRI.ArcGIS.ArcMapUI.IMxApplication m_MxApp;
		private ESRI.ArcGIS.ArcMapUI.IMxDocument m_MxDoc;
		private ESRI.ArcGIS.Framework.IApplication m_App;
		private ESRI.ArcGIS.esriSystem.IExtension m_Ext;		
		private ESRI.ArcGIS.esriSystem.esriExtensionState m_ExtState;

        private IMap map;
	
		private SystemsAnalysis.Tracer.TracerGUI tracerGUI;
		
		private ESRI.ArcGIS.Framework.ICommandBars cmdBars;		
		private SelectStartLinkTool selectStartLinkTool;
		private SelectStopLinkTool selectStopLinkTool;
		private DisplaySettingsCommand displaySettingsCommand;
		private ClearLinksCommand clearLinksCommand;
		private bool commandBarLoaded;

		private IFeatureLayer traceFL;
		private string usField;
		private string dsField;

		private Network traceNetwork;
		private IGraphEdges startEdges;
		private IGraphEdges stopEdges;
		
		#region IDocumentEvents Event Handlers	
		private static IDocumentEvents_OpenDocumentEventHandler odHandler;
		private static IDocumentEvents_NewDocumentEventHandler ndHandler;
		private static IDocumentEvents_MapsChangedEventHandler mcHandler;
		private static IDocumentEvents_OnContextMenuEventHandler cmHandler;
		private static IDocumentEvents_ActiveViewChangedEventHandler avHandler;
		private static IDocumentEvents_BeforeCloseDocumentEventHandler bcHandler;
		private static IDocumentEvents_CloseDocumentEventHandler cdHandler;
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
			MxExtension.Register(regKey);

		}
		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			MxExtension.Unregister(regKey);

		}

		#endregion
		#endregion
		
		public TracerExtension()
		{
			this.traceNetwork = new Network();
			this.startEdges = new GraphEdges();
			this.stopEdges = new GraphEdges();	
			this.traceFL = null;	
			this.commandBarLoaded = false;
		}
		
		private void ShowSettings(object sender, EventArgs e)
		{
			IDocumentDatasets docDatasets;	
			
			try
			{
				docDatasets = (IDocumentDatasets)m_MxDoc;
				if (docDatasets.Datasets == null) 
				{
					throw new Exception("No DataSets loaded.");
				}								
				this.tracerGUI.SetupForm(docDatasets.Datasets, this.startEdges, this.stopEdges);				
				
				if (this.tracerGUI.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{		
					IMouseCursor mouseCursor;
					mouseCursor = new MouseCursorClass();
					mouseCursor.SetCursor(2);
										
					if (this.tracerGUI.DirtyNetwork)
					{
						#region Rebuild Network based on newly select FeatureClass
						traceFL = this.tracerGUI.TraceFL;
						usField = this.tracerGUI.USField;
						dsField = this.tracerGUI.DSField;

						int featureCount = traceFL.FeatureClass.FeatureCount(null);						
						
						int[] edgeIds = new int[featureCount];
						int edgeIdIndex = traceFL.FeatureClass.FindField(traceFL.FeatureClass.OIDFieldName);
						string[] sourceEdges = new string[featureCount];
						int sourceEdgeIndex = traceFL.FeatureClass.FindField(usField);						
						string[] sinkEdges = new string[featureCount];
						int sinkEdgeIndex = traceFL.FeatureClass.FindField(dsField);						

						IStepProgressor pbar;
						pbar = m_App.StatusBar.ProgressBar;
						new FloatingProgressBar(featureCount);	
                        
						m_App.StatusBar.ProgressAnimation.Show();
						m_App.StatusBar.ProgressAnimation.Play(0, -1, -1);
						m_App.StatusBar.ProgressAnimation.Message = "Building Network...";
						pbar.MinRange = 0;
						pbar.MaxRange = featureCount;
						pbar.Message = "Building Network...";
						pbar.Show();
												
						try
						{
                            IFeatureCursor cursor = traceFL.Search(null, true);
                            IFeature f = cursor.NextFeature();

                            GraphEdges graphEdges = new GraphEdges();
                            graphEdges.isSorted = false;
                            int badEdgeCount = 0;

                            while (f != null)
                            {
                                try
                                {
                                    string sourceEdge = (string)f.get_Value(sourceEdgeIndex);
                                    string sinkEdge = (string)f.get_Value(sinkEdgeIndex);
                                    //Stupid DME pipes have a whitespace character appended to the FRM_NODE
                                    sourceEdge = sourceEdge.Trim();
                                    sinkEdge = sinkEdge.Trim();
                                    //TODO: DME pipes with unknown up- or downstream nodes have
                                    //XXXX. A better implemntation would not have this hardcoded.
                                    if (sourceEdge.StartsWith("XXXX") ||
                                        sinkEdge.StartsWith("XXXX"))
                                    {
                                        throw new Exception("Invalid Node (XXXX)");
                                    }
                                    else
                                    {
                                        IGraphEdge graphEdge;
                                        graphEdge = new GraphEdge(f.OID,
                                            sourceEdge, sinkEdge);
                                        graphEdges.Add(graphEdge);
                                    }
                                }
                                catch
                                {
                                    badEdgeCount++;
                                }
                                finally
                                {
                                    pbar.Step();
                                    f = cursor.NextFeature();
                                }
                            }
                            graphEdges.isSorted = true;
                            traceNetwork = new Network(graphEdges);	
								
							System.Windows.Forms.MessageBox.Show("Tracer configured: FeatureClass='"
								+ traceFL.Name + "'. US Field='" + usField + "'. DS Field='" + dsField + "'. Found " + badEdgeCount + " bad links.");
						}
						catch (Exception ex)
						{
							throw new Exception("Could not create Network. " + ex.ToString());
						}
						finally
						{
							mouseCursor = null;
                            m_App.StatusBar.PlayProgressAnimation(false); // ProgressAnimation.Stop();
							m_App.StatusBar.ProgressAnimation.Hide();
							pbar.Hide();
						}
						#endregion
					}
															
					if (this.tracerGUI.DirtyTrace)
					{
						this.startEdges = this.tracerGUI.StartEdges;
						this.stopEdges = this.tracerGUI.StopEdges;
						this.Retrace();
					}										
				}
				else
				{					
				}				
			}
			catch (Exception ex)
			{
				if (this.tracerGUI.Visible)
				{ this.tracerGUI.Hide(); }
				System.Windows.Forms.MessageBox.Show(ex.ToString(), "FeatureClass Tracer Error!");
			}	
		}
                        
		private void SelectLink(object sender, ClickEventArgs e)
		{			
			m_App.StatusBar.set_Message(0, "Selecting Link...");
			
			if (this.traceFL == null)
			{
				System.Windows.Forms.MessageBox.Show("No FeatureLayer Selected.\nClick 'Settings' to choose a PolyLine FeatureLayer.");
				return;
			}
			int button = e.Button;
			int shift = e.Shift;
			int x = e.X;
			int y = e.Y;

			ESRI.ArcGIS.Carto.IActiveView activeView;
			activeView = (ESRI.ArcGIS.Carto.IActiveView)m_MxDoc.FocusMap;

			ESRI.ArcGIS.Carto.IMap map;

			IPoint point;			

			//If the active view is the page layout view, this will change
			//the current map to the current (x,y) coordinates.
			//TODO: This could be placed in a library.
			if (activeView is ESRI.ArcGIS.Carto.PageLayout)
			{			
				point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
				
				map = activeView.HitTestMap(point);
				if (map == null) { return; }
				if (map != activeView.FocusMap)
				{
					activeView.FocusMap = map;
					activeView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGraphics, null, null);
				}
			}

			point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

			//Create a circular geometry to use with SpatialFilter
			map = activeView.FocusMap;		
			IGeometry searchGeom;
			searchGeom = this.GetCircularGeometry(point, activeView.Extent.Width / 100);
			
			//Give feedback to the user showing the area used for selecting by the SpatialFilter.
			this.FlashGeometry(searchGeom, map);

			//Create the SpatialFilter
			ISpatialFilter filter;
			filter = new SpatialFilterClass();			
			filter.Geometry = searchGeom; 
			filter.GeometryField = traceFL.FeatureClass.ShapeFieldName;
			filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
			
			//Create a FeatureCursor and iterate over all Features in the FeatureClass
			//that intersect the SpatialFilter geometry.
			IFeatureCursor featureCursor;
			featureCursor = this.traceFL.Search(filter, false);			

			IFeature feature;									
			int i = 0;

			feature = featureCursor.NextFeature();
			IFeature firstFeature = feature;
			while (feature != null && i < 2)
			{				
				this.FlashGeometry(feature.Shape, map);
				i++;
				feature = featureCursor.NextFeature();											
			}
			feature = firstFeature;

			if (i == 0)
			{				
				return;
			}
			else if (i > 1)
			{
				System.Windows.Forms.MessageBox.Show("Error: Multiple features selected. Select only one.");
				return;
			}
			
			if (sender.GetType().ToString() == "SystemsAnalysis.Tracer.SelectStartLinkTool")
			{				
				if (System.Windows.Forms.MessageBox.Show("Use feature " + feature.OID + " as Start Link?",
					"Add Start Link", System.Windows.Forms.MessageBoxButtons.OKCancel)
					== System.Windows.Forms.DialogResult.OK)
				{
					IGraphEdge edge = traceNetwork.getEdge(feature.OID);					
					this.startEdges.Add(edge);					
				}
			}
			else if (sender.GetType().ToString() == "SystemsAnalysis.Tracer.SelectStopLinkTool")
			{
				if (System.Windows.Forms.MessageBox.Show("Use feature " + feature.OID + " as Stop Link?",
					"Add Stop Link", System.Windows.Forms.MessageBoxButtons.OKCancel)
					== System.Windows.Forms.DialogResult.OK)
				{
					IGraphEdge edge = traceNetwork.getEdge(feature.OID);					
					this.stopEdges.Add(edge);	
				}
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("What the heck? Unknown tool: " + sender.ToString() + ".");
				return;
			}
			System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);
			this.Retrace();	
		
			m_App.CurrentTool = null;	
			
			return;
		}
		
		private void Retrace()
		{								
			m_App.StatusBar.ProgressAnimation.Show();
			m_App.StatusBar.ProgressAnimation.Play(0, -1, -1);
			m_App.StatusBar.ProgressAnimation.Message = "Tracing...";			

			IMouseCursor mouseCursor;
			mouseCursor = new MouseCursorClass();
			mouseCursor.SetCursor(2);
			
			ESRI.ArcGIS.Carto.IActiveView activeView;
			activeView = (ESRI.ArcGIS.Carto.IActiveView)m_MxDoc.FocusMap;

			activeView.Refresh();

			ESRI.ArcGIS.Carto.IMap map;
			map = activeView.FocusMap;				

			IFeatureLayer featureLayer = null;
			IEnumLayer enumLayer;

			enumLayer = map.get_Layers(null, true);	
			ILayer l = (ILayer)enumLayer.Next();			
			while (l != null)
			{		
				if (l is IFeatureLayer)
				{
					featureLayer = (IFeatureLayer)l;			
					if (featureLayer == traceFL) { break; }													
				}
				l = (ILayer)enumLayer.Next();	
			}
			if (featureLayer == null)
			{
				System.Windows.Forms.MessageBox.Show("Could not find '" + traceFL.Name + "' in TOC.");
				return;
			}			
			this.traceNetwork.ClearSubNetwork();
			this.traceNetwork.SelectSubNetwork(startEdges, stopEdges);
			int[] selectedEdgeIDs = traceNetwork.GetSelectedEdgeIDArray();
			
			IFeatureSelection featureSelection;
			featureSelection = (IFeatureSelection)featureLayer;
			featureSelection.Clear();
			featureSelection.SelectionSet.Refresh();

			if (selectedEdgeIDs.Length <= 0)
			{				
				activeView.Refresh();
				return;
			}
			
			featureSelection.SelectionSet.AddList(selectedEdgeIDs.Length, ref selectedEdgeIDs[0]);
			
			mouseCursor = null;

			if (this.tracerGUI.ZoomToTrace)
			{
				ESRI.ArcGIS.Geometry.IGeometryFactory pGeomFactory;
				pGeomFactory = new ESRI.ArcGIS.Geometry.GeometryEnvironmentClass();
			
				ESRI.ArcGIS.Geometry.IEnumGeometry pEnumGeom;
				ESRI.ArcGIS.Geodatabase.IEnumGeometryBind pEnumGeomBind;
				pEnumGeom = new ESRI.ArcGIS.Geodatabase.EnumFeatureGeometry();
				pEnumGeomBind = (ESRI.ArcGIS.Geodatabase.IEnumGeometryBind)pEnumGeom;
				pEnumGeomBind.BindGeometrySource(null, featureSelection.SelectionSet);	

				ESRI.ArcGIS.Geometry.IGeometry pGeom;
				pGeom = pGeomFactory.CreateGeometryFromEnumerator(pEnumGeom);	
				pGeom.Envelope.Expand(1.2, 1.2, true);				
				activeView.Extent = pGeom.Envelope;				
			}

			m_App.StatusBar.ProgressAnimation.Stop();
			m_App.StatusBar.ProgressAnimation.Hide();

			activeView.Refresh();

			return;
		}

		private void ClearLinks(object sender, EventArgs e)
		{
			this.startEdges = new GraphEdges();
			this.stopEdges = new GraphEdges();
			this.Retrace();
		}

		//TODO: This should go in a library
		private void FlashGeometry(IGeometry geom, IMap map)
		{
			ISymbol symbol;
			ILineSymbol lineSymbol;
			ISimpleFillSymbol fillSymbol;
			IMarkerSymbol markerSymbol;			
			IScreenDisplay display;
			IRgbColor color;
			IActiveView activeView;

			color = new RgbColor();
			color.Red = 50;
			color.Blue = 175;
			color.Green = 50;

			activeView = (IActiveView)map;
			display = activeView.ScreenDisplay;

			display.StartDrawing(0, -1);

			int sleepTime = 150;
								
			switch (geom.GeometryType)
			{
				case esriGeometryType.esriGeometryEnvelope:
					object missing = Type.Missing;
					Polygon poly;
					poly = new PolygonClass();										
					poly.AddPoint(geom.Envelope.UpperLeft, ref missing, ref missing);
					poly.AddPoint(geom.Envelope.UpperRight, ref missing, ref missing);
					poly.AddPoint(geom.Envelope.LowerRight, ref missing, ref missing);
					poly.AddPoint(geom.Envelope.LowerLeft, ref missing, ref missing);
					poly.AddPoint(geom.Envelope.UpperLeft, ref missing, ref missing);					
					fillSymbol = new SimpleFillSymbolClass();
					symbol = (ISymbol)fillSymbol;
					symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;					
					fillSymbol.Color = color;
					display.SetSymbol(symbol);
					display.DrawPolygon((IGeometry)poly);
					System.Threading.Thread.Sleep(sleepTime);
					display.DrawPolygon((IGeometry)poly);			
					break;
				case esriGeometryType.esriGeometryPolyline:
					lineSymbol = new SimpleLineSymbolClass();
					symbol = (ISymbol)lineSymbol;
					symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
					lineSymbol.Width = 4;
					lineSymbol.Color = color;
					display.SetSymbol(symbol);
					display.DrawPolyline(geom);
					System.Threading.Thread.Sleep(sleepTime);
					display.DrawPolyline(geom);	
					break;
				case esriGeometryType.esriGeometryPolygon:
					fillSymbol = new SimpleFillSymbolClass();
					symbol = (ISymbol)fillSymbol;
					symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;					
					fillSymbol.Color = color;
					ILineSymbol outlineSymbol;
					outlineSymbol = new SimpleLineSymbolClass();
					outlineSymbol.Color = color;
					outlineSymbol.Width = 0;
					fillSymbol.Outline = (ILineSymbol)outlineSymbol; 
					display.SetSymbol(symbol);
					display.DrawPolygon(geom);
					System.Threading.Thread.Sleep(sleepTime);
					display.DrawPolygon(geom);
					break;
				case esriGeometryType.esriGeometryPoint:
					markerSymbol = new SimpleMarkerSymbolClass();
					symbol = (ISymbol)markerSymbol;
					symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
					markerSymbol.Color = color;
					display.SetSymbol(symbol);
					display.DrawPolygon(geom);
					System.Threading.Thread.Sleep(sleepTime);
					display.DrawPolygon(geom);
					break;
				case esriGeometryType.esriGeometryMultipoint:
					markerSymbol = new SimpleMarkerSymbolClass();
					symbol = (ISymbol)markerSymbol;
					symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
					markerSymbol.Color = color;
					display.SetSymbol(symbol);
					display.DrawMultipoint(geom);
					System.Threading.Thread.Sleep(sleepTime);
					display.DrawMultipoint(geom);
					break;
				default:
					break;
			}
			activeView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGraphics, null, null);
			display.FinishDrawing();
						
			return;
		}

		//TODO: This should go in a library
		private IGeometry GetCircularGeometry(IPoint point, double radius)
		{
			IConstructCircularArc circularArc;
			circularArc = new CircularArcClass();
			circularArc.ConstructCircle(point, radius, true);

			IPolygon polygon;
			polygon = new PolygonClass();
			polygon.SpatialReference = point.SpatialReference;
			
			ISegmentCollection segmentCollection;
			segmentCollection = (ISegmentCollection)polygon;
			object missing = Type.Missing;
			segmentCollection.AddSegment((ISegment)circularArc, ref missing, ref missing);

			return (IGeometry)polygon;
		}

		#region IExtensionConfig Implementations
		public ESRI.ArcGIS.esriSystem.esriExtensionState State
		{
			get
			{				
				return m_ExtState;
			}
			set
			{
				m_ExtState = value;
			}
		}

		public string Description
		{
			get
			{				
				return "Systems Analysis Feature Class Tracer. Provides an extension to perform up- and downstream tracing on a network.";
			}
		}

		public string ProductName
		{
			get
			{				
				return "Feature Class Tracer";
			}
		}
		#endregion
	
		#region IExtension Implementations
		public void Shutdown()
		{
			m_MxApp = null;			
		}

		public string Name
		{
			get
			{				
				return "SystemsAnalysis.Tracer";
			}
		}

		public void Startup(ref object initializationData)
		{
			m_MxApp = (ESRI.ArcGIS.ArcMapUI.IMxApplication)initializationData;
			m_App = (IApplication)m_MxApp;
			
			m_MxDoc = (IMxDocument)m_App.Document;

			odHandler = new IDocumentEvents_OpenDocumentEventHandler(OpenDocument);
			((ESRI.ArcGIS.ArcMapUI.IDocumentEvents_Event)m_MxDoc).OpenDocument += odHandler;

			ndHandler = new IDocumentEvents_NewDocumentEventHandler(NewDocument);
			((ESRI.ArcGIS.ArcMapUI.IDocumentEvents_Event)m_MxDoc).NewDocument += ndHandler;

			mcHandler = new IDocumentEvents_MapsChangedEventHandler(MapsChanged);
			((IDocumentEvents_Event)m_MxDoc).MapsChanged += mcHandler;

			cmHandler = new IDocumentEvents_OnContextMenuEventHandler(OnContextMenu);
			((IDocumentEvents_Event)m_MxDoc).OnContextMenu += cmHandler;

			avHandler = new IDocumentEvents_ActiveViewChangedEventHandler(ActiveViewChanged);
			((IDocumentEvents_Event)m_MxDoc).ActiveViewChanged += avHandler;

			bcHandler = new IDocumentEvents_BeforeCloseDocumentEventHandler(BeforeCloseDocument);
			((IDocumentEvents_Event)m_MxDoc).BeforeCloseDocument += bcHandler;

			cdHandler = new IDocumentEvents_CloseDocumentEventHandler(CloseDocument);
			((IDocumentEvents_Event)m_MxDoc).CloseDocument += cdHandler;
									
		}
		#endregion

		#region IDocumentEvents Members

		public void NewDocument()
		{		
			if (this.commandBarLoaded) { return; }
						
			IDocument m_Doc;
			m_Doc = (IDocument)m_MxDoc;
			cmdBars = m_Doc.CommandBars;			

			ESRI.ArcGIS.esriSystem.UID u;
			ESRI.ArcGIS.Framework.ICommandItem cmdItem;			

			u = new ESRI.ArcGIS.esriSystem.UIDClass();
			u.Value = "SystemsAnalysis.FeatureClassTracer.SelectStartLinkTool";
									
			cmdItem = cmdBars.Find(u, false, false);
			this.selectStartLinkTool = (SelectStartLinkTool)cmdItem.Command;
			
			u = new ESRI.ArcGIS.esriSystem.UIDClass();
			u.Value = "SystemsAnalysis.FeatureClassTracer.SelectStopLinkTool";

			cmdItem = cmdBars.Find(u, false, false);
			this.selectStopLinkTool = (SelectStopLinkTool)cmdItem.Command;

			u = new ESRI.ArcGIS.esriSystem.UIDClass();
			u.Value = "SystemsAnalysis.Tracer.FeatureClassTracer.ClearLinksCommand";
									
			cmdItem = cmdBars.Find(u, false, false);
			this.clearLinksCommand = (ClearLinksCommand)cmdItem.Command;

			u = new ESRI.ArcGIS.esriSystem.UIDClass();
			u.Value = "SystemsAnalysis.FeatureClassTracer.DisplaySettingsCommand";

			cmdItem = cmdBars.Find(u, false, false);
			this.displaySettingsCommand= (DisplaySettingsCommand)cmdItem.Command;

			tracerGUI = new TracerGUI();
			this.displaySettingsCommand.Click += new OnClickEventHandler(this.ShowSettings);			
			this.clearLinksCommand.Click += new OnClickEventHandler(this.ClearLinks);
			this.selectStopLinkTool.MouseDown += new OnMouseDownEventHandler(this.SelectLink);
			this.selectStartLinkTool.MouseDown += new OnMouseDownEventHandler(this.SelectLink);		

			this.commandBarLoaded = true;

			return;
		}

		public void ActiveViewChanged()
		{
			// TODO:  Add Class1.ActiveViewChanged implementation
		}

		public void OpenDocument()
		{
			//Treat opening an existing document the same of creating a new one
			this.NewDocument();
			return;
		}

		public void OnContextMenu(int x, int y, out bool handled)
		{
			// TODO:  Add Class1.OnContextMenu implementation
			handled = false;
		}

		public void MapsChanged()
		{
			// TODO:  Add Class1.MapsChanged implementation
		}

		public bool BeforeCloseDocument()
		{
			// TODO:  Add Class1.BeforeCloseDocument implementation
			return false;
		}

		public void CloseDocument()
		{
			// TODO:  Add Class1.CloseDocument implementation
		}

		#endregion

        public void SetupTracer(Network network, IFeatureLayer fl, string usField, string dsField, IMap map)
        {
            this.traceNetwork = network;
            this.traceFL = fl;
            this.usField = usField;
            this.dsField = dsField;
            this.map = map;            
        }

        public IGraphEdge SelectLinkExternal(object sender, ClickEventArgs e)
        {
            if (this.traceFL == null)
            {
                throw new Exception("No FeatureLayer Selected.");
            }
            int button = e.Button;
            int shift = e.Shift;
            int x = e.X;
            int y = e.Y;

            ESRI.ArcGIS.Carto.IActiveView activeView;
            activeView = (ESRI.ArcGIS.Carto.IActiveView)map;

            //ESRI.ArcGIS.Carto.IMap map;

            IPoint point;            
            point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

            //Create a circular geometry to use with SpatialFilter
            //map = activeView.FocusMap;
            IGeometry searchGeom;
            searchGeom = this.GetCircularGeometry(point, activeView.Extent.Width / 100);

            //Give feedback to the user showing the area used for selecting by the SpatialFilter.
            this.FlashGeometry(searchGeom, map);

            //Create the SpatialFilter
            ISpatialFilter filter;
            filter = new SpatialFilterClass();
            filter.Geometry = searchGeom;
            filter.GeometryField = traceFL.FeatureClass.ShapeFieldName;
            filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

            //Create a FeatureCursor and iterate over all Features in the FeatureClass
            //that intersect the SpatialFilter geometry.
            IFeatureCursor featureCursor;
            featureCursor = this.traceFL.Search(filter, false);

            IFeature feature;
            int i = 0;

            feature = featureCursor.NextFeature();
            IFeature firstFeature = feature;
            while (feature != null && i < 2)
            {
                this.FlashGeometry(feature.Shape, map);
                i++;
                feature = featureCursor.NextFeature();
            }
            feature = firstFeature;

            if (i == 0)
            {
                throw new Exception("No link selected");
            }
            else if (i > 1)
            {
                throw new Exception("Error: Multiple features selected. Select only one.");                
            }

            if (sender.GetType().ToString() == "SystemsAnalysis.Tracer.SelectStartLinkTool")
            {                
                IGraphEdge edge = traceNetwork.getEdge(feature.OID);
                return edge;             
            }
            else if (sender.GetType().ToString() == "SystemsAnalysis.Tracer.SelectStopLinkTool")
            {                
                IGraphEdge edge = traceNetwork.getEdge(feature.OID);
                return edge;             
            }
            else
            {
                throw new Exception ("What the heck? Unknown tool: " + sender.ToString() + ".");                
            }            
        }
	}
}
