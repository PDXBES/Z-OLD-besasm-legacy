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
using System.Runtime.InteropServices;

namespace SystemsAnalysis.Utils.ArcObjects.MapControl
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("199FD437-CC7C-44c5-AC99-8AE9134F4A4C")]

	public class FixedZoomIn : ICommand
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

		private bool m_enabled;
		private System.Drawing.Bitmap m_bitmap;
		private IntPtr m_hBitmap;
		private IHookHelper m_pHookHelper;

		public FixedZoomIn()
		{  
			string[] res = GetType().Assembly.GetManifestResourceNames();
			if(res.GetLength(0) > 0)
			{
				m_bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("SystemsAnalysis.Utils.ArcObjects.MapControl.zoominfxd.bmp"));
				if(m_bitmap != null)
				{
					m_bitmap.MakeTransparent(m_bitmap.GetPixel(0,0));
					m_hBitmap = m_bitmap.GetHbitmap();
				}
			}
			m_pHookHelper = new HookHelperClass ();
		}

		~FixedZoomIn()
		{
			if(m_hBitmap.ToInt32() != 0)
				DeleteObject(m_hBitmap);
		}
	
		#region ICommand Members

		public void OnClick()
		{
			//Get IActiveView interface
			IActiveView pActiveView = (IActiveView) m_pHookHelper.FocusMap;

			//Get IEnvelope interface
			IEnvelope pEnvelope = (IEnvelope) pActiveView.Extent;

			//Expand envelope and refresh the view
			pEnvelope.Expand (0.75, 0.75, true);
			pActiveView.Extent = pEnvelope;
			pActiveView.Refresh();

		}

		public string Message
		{
			get
			{
				return "Zoom in on the center of the map";
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
					m_enabled = true;
				}
			}
		}

		public string Caption
		{
			get
			{
				return "Fixed Zoom In";
			}
		}

		public string Tooltip
		{
			get
			{
				return "Fixed Zoom In";
			}
		}

		public int HelpContextID
		{
			get
			{
				// TODO:  Add FixedZoomIn.HelpContextID getter implementation
				return 0;
			}
		}

		public string Name
		{
			get
			{
				return "Sample_Pan/FixedZoomIn";
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
				return m_enabled;
			}
		}

		public string HelpFile
		{
			get
			{
				// TODO:  Add FixedZoomIn.HelpFile getter implementation
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

