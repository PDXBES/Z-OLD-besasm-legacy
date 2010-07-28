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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Utility;
using System.Runtime.InteropServices;

namespace SystemsAnalysis.Utils.ArcObjects.MapControl
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("139290CC-94E0-4d70-AF52-EBA664801E49")]

	public class PanLeft : ICommand
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

		public PanLeft()
		{
			string[] res = GetType().Assembly.GetManifestResourceNames();
			if(res.GetLength(0) > 0)
			{
				m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("SystemsAnalysis.Utils.ArcObjects.MapControl.PanLeft.bmp"));
				if(m_bitmap != null)
				{
					m_bitmap.MakeTransparent(m_bitmap.GetPixel(1,1));
					m_hBitmap = m_bitmap.GetHbitmap();
				}
			}
			m_pHookHelper = new HookHelperClass ();
		}

		~PanLeft()
		{
			if(m_hBitmap.ToInt32() != 0)
				DeleteObject(m_hBitmap);
		}
	
		#region ICommand Members

		public void OnClick()
		{
			if(m_pHookHelper == null) return;

			//Get the active view
			IActiveView pActiveView = (IActiveView) m_pHookHelper.FocusMap;

			//Get the extent
			IEnvelope pEnvelope = (IEnvelope) pActiveView.Extent;

			//Create a point to pan to
			IPoint pPoint;
			pPoint = new PointClass();
			pPoint.X = ((pEnvelope.XMin + pEnvelope.XMax) / 2) - (pEnvelope.Height / (100 / GetPanFactor()));
			pPoint.Y = ((pEnvelope.YMin + pEnvelope.YMax) / 2); 

			//Center the envelope on the point
			pEnvelope.CenterAt(pPoint);

			//Set the new extent
			pActiveView.Extent = pEnvelope;

			//Refresh the active view
			pActiveView.Refresh();
		}

		private long GetPanFactor()
		{
			return 50;
		}

		public string Message
		{
			get
			{
				return "Pan display left by the pan factor percentage";
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
		}

		public string Caption
		{
			get
			{
				return "Pan Left";
			}
		}

		public string Tooltip
		{
			get
			{
				return "Pan Left";
			}
		}

		public int HelpContextID
		{
			get
			{
				// TODO:  Add PanLeft.HelpContextID getter implementation
				return 0;
			}
		}

		public string Name
		{
			get
			{
				return "Sample_Pan/Zoom_Pan Left";
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
				// TODO:  Add PanLeft.HelpFile getter implementation
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
	}
}

