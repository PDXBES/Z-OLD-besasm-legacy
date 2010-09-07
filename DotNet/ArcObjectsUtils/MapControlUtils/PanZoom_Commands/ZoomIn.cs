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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Utility;
using System.Runtime.InteropServices;

namespace SystemsAnalysis.Utils.ArcObjects.MapControl
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("2C214724-BFA2-4e8c-BC5D-775C67FA6F51")]

	public class ZoomIn : ICommand, ITool
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
		private INewEnvelopeFeedback m_feedBack;
		private IPoint m_point;
		private Boolean m_isMouseDown;
		private System.Windows.Forms.Cursor m_zoomInCur;
		private System.Windows.Forms.Cursor m_moveZoomInCur;

		
		public ZoomIn()
		{
			//Load resources
			string[] res = GetType().Assembly.GetManifestResourceNames();
			if(res.GetLength(0) > 0)
			{
				m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("SystemsAnalysis.Utils.ArcObjects.MapControl.ZoomIn.bmp"));
				if(m_bitmap != null)
				{
					m_bitmap.MakeTransparent(m_bitmap.GetPixel(1,1));
					m_hBitmap = m_bitmap.GetHbitmap();
				}
			}
			m_pHookHelper = new HookHelperClass ();
		}
	
		~ZoomIn()
		{
			if(m_hBitmap.ToInt32() != 0)
				DeleteObject(m_hBitmap);
			
			m_pHookHelper = null;
			m_zoomInCur = null;
			m_moveZoomInCur = null;
		}

		#region ICommand Members

		public void OnClick()
		{
		}

		public string Message
		{
			get
			{
				return "zooms the Display In By Rectangle or Single Click";
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
			m_zoomInCur = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("SystemsAnalysis.Utils.ArcObjects.MapControl.ZoomIn.cur"));
			m_moveZoomInCur = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("SystemsAnalysis.Utils.ArcObjects.MapControl.MoveZoomIn.cur"));
		}

		public string Caption
		{
			get
			{
				return "Zoom In";
			}
		}

		public string Tooltip
		{
			get
			{
				return "Zoom In";
			}
		}

		public int HelpContextID
		{
			get
			{
				// TODO:  Add ZoomIn.HelpContextID getter implementation
				return 0;
			}
		}

		public string Name
		{
			get
			{
				return "Sample_Pan/Zoom_Zoom In";
			}
		}

		public bool Checked
		{
			get
			{
				return false;
			}
		}

		public bool Enabled
		{
			get
			{
				if(m_pHookHelper.FocusMap == null) return false;

				return true;
			}
		}

		public string HelpFile
		{
			get
			{
				// TODO:  Add ZoomIn.HelpFile getter implementation
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
			if(m_pHookHelper.ActiveView == null) return;

			//If the active view is a page layout
			if(m_pHookHelper.ActiveView is IPageLayout)
			{
				//Create a point in map coordinates
				IPoint pPoint = (IPoint) m_pHookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

				//Get the map if the point is within a data frame
				IMap pMap = m_pHookHelper.ActiveView.HitTestMap(pPoint);

				if(pMap == null) return;

				//Set the map to be the page layout's focus map
				if(pMap != m_pHookHelper.FocusMap)
				{
					m_pHookHelper.ActiveView.FocusMap = pMap;
					m_pHookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
				}
			}
			//Create a point in map coordinates
			IActiveView pActiveView = (IActiveView) m_pHookHelper.FocusMap;
			m_point = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);

			m_isMouseDown = true;

		}

		public void OnMouseMove(int button, int shift, int x, int y)
		{
			if(!m_isMouseDown) return;

			//Get the focus map
			IActiveView pActiveView = (IActiveView) m_pHookHelper.FocusMap;

			//Start an envelope feedback
			if(m_feedBack == null)
			{
				m_feedBack = new NewEnvelopeFeedbackClass();
				m_feedBack.Display = pActiveView.ScreenDisplay;
				m_feedBack.Start(m_point);
			}

			//Move the envelope feedback
			m_feedBack.MoveTo(pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y));
		}

		public void OnMouseUp(int button, int shift, int x, int y)
		{
			if(!m_isMouseDown) return;

			//Get the focus map
			IActiveView pActiveView = (IActiveView) m_pHookHelper.FocusMap;

			//If an envelope has not been tracked
			IEnvelope pEnvelope;

			if(m_feedBack == null)
			{
				//Zoom in from mouse click
				pEnvelope = pActiveView.Extent;
				pEnvelope.Expand(0.5, 0.5, true);
				pEnvelope.CenterAt(m_point);
			}
			else
			{
				//Stop the envelope feedback
				pEnvelope = m_feedBack.Stop();

				//Exit if the envelope height or width is 0
				if(pEnvelope.Width ==0 || pEnvelope.Height == 0)
				{
					m_feedBack = null;
					m_isMouseDown = false;
				}
			}

			//Set the new extent
			pActiveView.Extent = pEnvelope;

			//Refresh the active view
			pActiveView.Refresh();
			m_feedBack = null;
			m_isMouseDown = false;

		}

		public void OnKeyDown(int keyCode, int shift)
		{
			if(m_isMouseDown)
			{
				if(keyCode == 27)
				{
					m_isMouseDown = false;
					m_feedBack = null;
					m_pHookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
				}
			}
		}

		public void OnKeyUp(int keyCode, int shift)
		{
			// TODO:  Add ZoomIn.OnKeyUp implementation
		}

		public int Cursor
		{
			get
			{
				if(m_isMouseDown)
					return m_moveZoomInCur.Handle.ToInt32();
				else
					return m_zoomInCur.Handle.ToInt32();
			}
		}
		
		public bool OnContextMenu(int x, int y)
		{
			// TODO:  Add ZoomIn.OnContextMenu implementation
			return false;
		}

		public bool Deactivate()
		{
			return true;
		}

		public void Refresh(int hdc)
		{
			// TODO:  Add ZoomIn.Refresh implementation
		}

		public void OnDblClick()
		{
			// TODO:  Add ZoomIn.OnDblClick implementation
		}

		#endregion
	}
}

