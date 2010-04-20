using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

using BESGISUtils;

namespace DocClickHelpers
{
    /// <summary>
    /// Summary description for DocSelectTool.
    /// </summary>
    [Guid("4ba3c02c-f643-49f9-90c9-c4ea0fcafbac")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("DocClickHelpers.DocSelectTool")]
    public sealed class DocSelectTool : BaseTool
    {
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
            MxCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper = null;
        private IApplication m_application;
        private IMxDocument MXD;
        private INewEnvelopeFeedback NewEnvelopeFeedback;
        private IEnvelope feedbackEnv;
        private IElement feedbackElement;
        private IScreenDisplay feedbackScreenDisplay;
        private ISimpleLineSymbol feedbackLineSymbol;
        private ESRI.ArcGIS.Geometry.Point feedbackStartPoint;
        private ESRI.ArcGIS.Geometry.Point feedbackMovePoint;

        public DocSelectTool()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "BES Document Managment"; //localizable text 
            base.m_caption = "Document Selector";  //localizable text 
            base.m_message = "Select an item to see documents";  //localizable text
            base.m_toolTip = "Get Documents related to the selected item";  //localizable text
            base.m_name = "BESDocManager_Selector";   //unique id, non-localizable (e.g. "MyCategory_ArcMapTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            m_application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
            {
                base.m_enabled = true;
                MXD = (IMxDocument)m_application.Document;
            }
            else
                base.m_enabled = false;

            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                {
                    m_hookHelper = null;
                }
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            //Don't need to do anything; mouse_down will handle it.
        }
        
        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            //initialize all the variables.
            feedbackEnv = new EnvelopeClass();
            feedbackStartPoint = new ESRI.ArcGIS.Geometry.PointClass();
            feedbackMovePoint = new ESRI.ArcGIS.Geometry.PointClass();
            feedbackLineSymbol = new SimpleLineSymbolClass();
            feedbackScreenDisplay = m_hookHelper.ActiveView.ScreenDisplay;

            feedbackLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;

            //initialize a new Envelope feedback class and pass it the symbol and display
            NewEnvelopeFeedback = new NewEnvelopeFeedbackClass();
            NewEnvelopeFeedback.Display = feedbackScreenDisplay;
            NewEnvelopeFeedback.Symbol = feedbackLineSymbol as ISymbol;


            //pass the startpoint from the mouse position, transforming it to an appropriate map point.
            feedbackStartPoint = feedbackScreenDisplay.DisplayTransformation.ToMapPoint(X, Y) as ESRI.ArcGIS.Geometry.Point;
            NewEnvelopeFeedback.Start(feedbackStartPoint);

            
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            //only pass the point if the mouse button is down
            if (Button == 1)
            {
                //pass X and Y to feedbackMovePoint to transfer to NewEnvelopeFeedback
                feedbackMovePoint = feedbackScreenDisplay.DisplayTransformation.ToMapPoint(X, Y) as ESRI.ArcGIS.Geometry.Point;
                NewEnvelopeFeedback.MoveTo(feedbackMovePoint);
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            IActiveView AV;

            //when mouse comes up, end the new envelope and pass it to feedbackEnv.
            feedbackEnv = NewEnvelopeFeedback.Stop();
            AV = (IActiveView)MXD.FocusMap;
            if (feedbackEnv.IsEmpty)
            {
                IPoint point;
                IGeometry geo;

                point = AV.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                geo = BESGISUtils.MakeCircleAtPoint.GetCircularGeometry(point, AV.Extent.Width / 100.0);
                MXD.FocusMap.SelectByShape(geo, null, false);
            }
            else
            {
                MXD.FocusMap.SelectByShape(feedbackEnv, null, false);
            }
            AV.Refresh();
            
            ////initialize a new RectangleElementClass
            //feedbackElement = new RectangleElementClass();

            ////pass the new rectangleelement the geometry defined by our feedback object
            //feedbackElement.Geometry = feedbackEnv;

            ////make sure the element is activated in teh current view
            //feedbackElement.Activate(feedbackScreenDisplay);

            ////now add the newly created element to the activeview with default symbology.
            //m_hookHelper.ActiveView.GraphicsContainer.AddElement(feedbackElement, 0);


            ////and refresh the view so we can see the changes.
            //m_hookHelper.ActiveView.Refresh();
        }
        #endregion
    }
}
