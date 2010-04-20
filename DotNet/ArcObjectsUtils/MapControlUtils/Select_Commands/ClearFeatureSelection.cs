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
using System;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.Runtime.InteropServices;

namespace SystemsAnalysis.Utils.ArcObjects.MapControl
{
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("b737395c-7d7f-4cef-b38e-63b13498b079")]
	public sealed class ClearFeatureSelection : BaseCommand
	{
		private IHookHelper m_HookHelper = new HookHelperClass();

		#region Component Category Registration
		[ComRegisterFunction()]
		[ComVisible(false)]
		static void RegisterFunction(String regKey)
		{
			ControlsCommands.Register(regKey);
		}
  
		[ComUnregisterFunction()]
		[ComVisible(false)]
		static void UnregisterFunction(String regKey)
		{
			ControlsCommands.Unregister(regKey);
		}
		#endregion#	

		public ClearFeatureSelection()
		{
			//Create an IHookHelper object
			m_HookHelper = new HookHelperClass();

			//Set the tool properties
			base.m_caption = "Clear Feature Selection";
			base.m_category = "Sample_Select(C#)";
			base.m_name = "Sample_Select(C#)_Clear Feature Selection";
			base.m_message = "Clear Current Feature Selection";
			base.m_toolTip = "Clear Feature Selection";
			base.m_bitmap = new System.Drawing.Bitmap(
				GetType().Assembly.GetManifestResourceStream("SystemsAnalysis.Utils.ArcObjects.MapControl.ClearSelection.bmp"));
		}
	
		//Destructor
		~ClearFeatureSelection()
		{
			m_HookHelper = null;
		}

		public override void OnCreate(object hook)
		{
			m_HookHelper.Hook = hook;
		}
	
		public override bool Enabled
		{
			get
			{
				if (m_HookHelper.FocusMap == null) return false;
				return (m_HookHelper.FocusMap.SelectionCount > 0);			
			}
		}
	
		public override void OnClick()
		{
			//Clear selection
			m_HookHelper.FocusMap.ClearSelection();

			//Get the IActiveView of the FocusMap
			IActiveView activeView = (IActiveView) m_HookHelper.FocusMap;

			//Get the visible extent of the display
			IEnvelope bounds = activeView.ScreenDisplay.DisplayTransformation.FittedBounds;

			//Refresh the visible extent of the display
			activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, bounds);
		}
	
	}
}