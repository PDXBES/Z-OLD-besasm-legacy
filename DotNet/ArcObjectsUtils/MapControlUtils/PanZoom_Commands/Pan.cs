/*
 Copyright 1995-2005 ESRI

 All rights reserved under the copyright laws of the United States.

 You may freely redistribute and use this sample code, with or without modification.

 Disclaimer: THE SAMPLE CODE IS PROVIDED "AS IS" AND ANY EXPRESS OR IMPLIED 
 WARRANTIES, INCLUDING THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
 FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL ESRI OR 
 CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
 OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
 SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
 INTERRUPTION) SUSTAINED BY YOU OR A THIRD PARTY, HOWEVER CAUSED AND ON ANY 
 THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT ARISING IN ANY 
 WAY OUT OF THE USE OF THIS SAMPLE CODE, EVEN IF ADVISED OF THE POSSIBILITY OF 
 SUCH DAMAGE.

 For additional information contact: Environmental Systems Research Institute, Inc.

 Attn: Contracts Dept.

 380 New York Street

 Redlands, California, U.S.A. 92373 

 Email: contracts@esri.com
*/

/* Copyright 1995-2005 ESRI

 All rights reserved under the copyright laws of the United States.

 You may freely redistribute and use this sample code, with or without modification.

 Disclaimer: THE SAMPLE CODE IS PROVIDED "AS IS" AND ANY EXPRESS OR IMPLIED
 WARRANTIES, INCLUDING THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
 FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL ESRI OR
 CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY,
 OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 INTERRUPTION) SUSTAINED BY YOU OR A THIRD PARTY, HOWEVER CAUSED AND ON ANY
 THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT ARISING IN ANY
 WAY OUT OF THE USE OF THIS SAMPLE CODE, EVEN IF ADVISED OF THE POSSIBILITY OF
 SUCH DAMAGE.

 For additional information contact: Environmental Systems Research Institute, Inc.

 Attn: Contracts Dept.

 380 New York Street

 Redlands, California, U.S.A. 92373

 Email: contracts@esri.com
 */

using System;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Utility;
using System.Runtime.InteropServices;

namespace SystemsAnalysis.Utils.ArcObjects.MapControl
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("4BD2DDAE-AA6F-4718-AC2E-F39C5618895C")]

	public class Pan :  ICommand, ITool
	{

		#region Component Category Registration
  
		[ComRegisterFunction()]
		[ComVisible(false)]
		static void RegisterFunction(String sKey)
		{
			string fullKey = sKey.Remove(0, 18) + @"\Implemented Categories";
			Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(fullKey, true);
			if (regKey != null)
			{
				regKey.CreateSubKey("{B56A7C42-83D4-11D2-A2E9-080009B6F22B}");
			}
		}
  
		[ComUnregisterFunction()]
		[ComVisible(false)]
		static void UnregisterFunction(String sKey)
		{
			string fullKey = sKey.Remove(0, 18) + @"\Implemented Categories";
			Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(fullKey, true);
			if (regKey != null)
			{
				regKey.DeleteSubKey("{B56A7C42-83D4-11D2-A2E9-080009B6F22B}");
			}
		}
  
		#endregion#

		[DllImport("gdi32.dll")]
		static extern bool DeleteObject(IntPtr hObject);

		private System.Drawing.Bitmap m_bitmap;
		private IntPtr m_hBitmap;
		private IHookHelper m_pHookHelper;
		private bool m_enabled;
		//private IMxDocument m_mxDoc;
		//private IAppDisplay m_appDisplay;
		private IScreenDisplay m_focusScreenDisplay;
		private bool m_PanOperation;
		private bool m_check;
		private System.Windows.Forms.Cursor m_cursor;
		private System.Windows.Forms.Cursor m_cursorMove;
				
		public Pan()
		{
			//Load resources
			string[] res = GetType().Assembly.GetManifestResourceNames();
			if(res.GetLength(0) > 0)
			{
				m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("SystemsAnalysis.Utils.ArcObjects.MapControl.Pan.bmp"));
				if(m_bitmap != null)
				{
					m_bitmap.MakeTransparent(m_bitmap.GetPixel(1,1));
					m_hBitmap = m_bitmap.GetHbitmap();
				}
			}
			m_pHookHelper = new HookHelperClass ();
		}

		~Pan()
		{
			if(m_hBitmap.ToInt32() != 0)
				DeleteObject(m_hBitmap);
			m_check = false;
			m_pHookHelper = null;

		}
	   
		#region ICommand Members

		public void OnClick()
		{
		}

		public string Message
		{
			get
			{
				return "Pans the Display by Grabbing";
			}
		}

		public int Bitmap
		{
			get
			{
				return m_hBitmap.ToInt32();
			}
		}

		public void OnCreate(object hook)
		{
			if (hook != null)
			{
				if(hook is IMxApplication)
				{
					m_pHookHelper.Hook = hook;
				}
			}
			m_pHookHelper.Hook = hook;
			/*IApplication m_app = hook as IApplication;
			IMxApplication mxApp = m_app as IMxApplication;
			m_mxDoc = m_app.Document as IMxDocument;
			m_appDisplay = mxApp.Display;*/
			
			m_enabled = true;
			m_check = false;
			m_cursor = new System.Windows.Forms.Cursor(
				GetType().Assembly.GetManifestResourceStream ("SystemsAnalysis.Utils.ArcObjects.MapControl.Hand.cur"));
			m_cursorMove = new System.Windows.Forms.Cursor(
				GetType().Assembly.GetManifestResourceStream("SystemsAnalysis.Utils.ArcObjects.MapControl.MoveHand.cur"));
		}

		public string Caption
		{
			get
			{
				return "Pan";
			}
		}

		public string Tooltip
		{
			get
			{
				return "Pan by Grab";
			}
		}

		public int HelpContextID
		{
			get
			{
				// TODO:  Add Pan.HelpContextID getter implementation
				return 0;
			}
		}

		public string Name
		{
			get
			{
				return "Sample_Pan/Zoom_Pan";
			}
		}

		public bool Checked
		{
			get
			{
				return m_check;
			}
		}

		public bool Enabled
		{
			get
			{
				return m_enabled;
			}
		}

		public string HelpFile
		{
			get
			{
				// TODO:  Add Pan.HelpFile getter implementation
				return null;
			}
		}

		public string Category
		{
			get
			{
				return "Sample_Pan/Zoom";
			}
		}

		#endregion
	

		#region ITool Members

		public void OnMouseDown(int button, int shift, int x, int y)
		{
			if (button != 1) return;

			IActiveView activeView = m_pHookHelper.ActiveView;
			IPoint point;
			point = activeView.ScreenDisplay.DisplayTransformation.ToMapPoint(x,y);						

			//If in PageLayout view, find the closest map
			if(!(activeView is IMap))
			{
				IMap hitMap = activeView.HitTestMap(point);

				//Exit if no map found
				if(hitMap == null)
					return;
	
				//if(activeView != m_mxDoc.FocusMap)
					activeView.FocusMap = hitMap;

			}
			//Start the pan operation
			m_focusScreenDisplay = getFocusDisplay();
			IPoint pPanStart;
			pPanStart = new PointClass();
			
			pPanStart.X = point.X; //(m_pHookHelper.ActiveView.Extent.XMax + m_pHookHelper.ActiveView.Extent.XMin) / 2;
			pPanStart.Y = point.Y; //(m_pHookHelper.ActiveView.Extent.YMax + m_pHookHelper.ActiveView.Extent.YMin) / 2;
			m_focusScreenDisplay.PanStart(pPanStart);

			m_PanOperation = true;
		}

	    private IScreenDisplay getFocusDisplay()
		{
			//Get the ScreenDisplay that has focus
			IActiveView activeView;
			activeView = m_pHookHelper.ActiveView.FocusMap as IActiveView; //layout view
			return activeView.ScreenDisplay;			
		}

		public void OnMouseMove(int button, int shift, int x, int y)
		{
			if(button != 1) return;

			if(! m_PanOperation) return;

			IPoint point = m_focusScreenDisplay.DisplayTransformation.ToMapPoint(x,y);
			m_focusScreenDisplay.PanMoveTo(point);
		}
		
		public void OnMouseUp(int button, int shift, int x, int y)
		{
			if(button != 1) return;

			if(! m_PanOperation) return;

			IEnvelope extent = m_focusScreenDisplay.PanStop();

			//Check if user dragged a rectangle or just clicked.
			//If a rectangle was dragged, m_ipFeedback in non-NULL
			if(extent != null)
			{
				m_focusScreenDisplay.DisplayTransformation.VisibleBounds = extent;
				m_focusScreenDisplay.Invalidate(null, true, (short)esriScreenCache.esriAllScreenCaches);
			}

			m_PanOperation = false;
		}

		public void OnKeyDown(int keyCode, int shift)
		{
			// TODO:  Add Pan.OnKeyDown implementation
		}

		public void OnKeyUp(int keyCode, int shift)
		{
			// TODO:  Add Pan.OnKeyUp implementation
		}

		public int Cursor
		{
			get
			{
				if(m_PanOperation)
          return m_cursorMove.Handle.ToInt32();
				else
					return m_cursor.Handle.ToInt32();
			}
		}

		public bool OnContextMenu(int x, int y)
		{
			// TODO:  Add Pan.OnContextMenu implementation
			return false;
		}

		public bool Deactivate()
		{
			// TODO:  Add Pan.Deactivate implementation
			return true;
		}

		public void Refresh(int hdc)
		{
			// TODO:  Add Pan.Refresh implementation
		}

		public void OnDblClick()
		{
			// TODO:  Add Pan.OnDblClick implementation
		}

		#endregion
	}
}

