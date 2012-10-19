using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;

namespace ExtnHelpers
{
    /// <summary>
    /// This is a logging wrapper class for use in the DocClicker application.
    /// It wraps both the MS Enterprise Library logging mechanism
    /// and an ArcMap dockable window that will display things.
    /// Supports variable detail levels.
    /// </summary>
    public class Logger
    {
        private IApplication _ArcMap;       // private copy of pointer to the application
        private IDockableWindowManager _WinManager;
        private IDockableWindow _WinLog;    // our logging dockable window
        private const String LogWindowGUID = "{714f8459-7fdc-4cbd-8ddc-1af76e361075}";
        private int _LogLevel = 0;
        public TOCDocClicker TOCcl;

        // constructor
        public Logger(IApplication appContext) {
            _ArcMap = appContext;

            if (_WinLog == null)
            {
                _WinManager = _ArcMap as IDockableWindowManager;
                if (_WinManager != null)
                {
                    UID winID = new UIDClass();
                    winID.Value = LogWindowGUID; // the GUID of the user control that is the window...
                    _WinLog = _WinManager.GetDockableWindow(winID);
                }
            }
             // TOCDocClicker TOCcl;
                ESRI.ArcGIS.ArcMapUI.IMxDocument pMxDoc;
                pMxDoc = (ESRI.ArcGIS.ArcMapUI.IMxDocument) _ArcMap.Document;
                
                //Find the contents view tab to put the log message in.  
                for (int i = 0; i <= pMxDoc.ContentsViewCount; i++)
                {
                    //todo: replace the string with a var
                    string tempStr = pMxDoc.get_ContentsView(i).Name;
                    if (tempStr == "DocClicker")
                    {
                        TOCcl = (TOCDocClicker)pMxDoc.get_ContentsView(i);
                        // TOCcl.AddItemToLog(msg);
                        break;
                    }
                }
        }
        /// <summary>
        ///  The basic logging mechanism.
        /// </summary>
        /// <param name="msg"> The message to be logged.</param>
        /// <param name="severity">The severity of this message; whether the message gets logged depends on the current severity level.</param>
        public void PopulateGrid(DataTable DT)
        {
            TOCcl.PopulateGeoInfo(DT); //get the geoinfo for documents returned in the tabular search.
            TOCcl.BindToGrid(DT);
        }
        public void AddProperty(string msg)
        {
            TOCcl.AddItemToProperty(msg);
        }
        public void Log(String msg, int severity)
        {
            if (severity > _LogLevel)
            {
                LogToScreen(msg);
                // LogToEntLib(msg);
            }
        }
        /// <summary>
        /// Show the logging window
        /// </summary>
        //public void Show()
        //{
        //    if (_WinManager != null)
        //    {
        //        UID u = new UID();
        //        u.Value = LogWindowGUID;
        //        _WinLog = _WinManager.GetDockableWindow(u);
        //        _WinLog.Show(true);
        //    }
        //    return;
        //}
        /// <summary>
        /// Hide the logging window
        /// </summary>
        //public void Hide() 
        //{
        //    if (_WinManager != null)
        //    {
        //        UID u = new UID();
        //        u.Value = LogWindowGUID;
        //        _WinLog = _WinManager.GetDockableWindow(u);
        //        _WinLog.Show(false);
        //    }
        //    return;
        //}
        /// <summary>
        /// Property that exposes the current logging level of this Logger instance.
        /// </summary>
        public int logLevel
        {
            get { return _LogLevel; }
            set { 
                if (value >= 100) { _LogLevel = 100; }
                else if (value < 0) { _LogLevel = 0; }
                else { _LogLevel = value; } 
            }
        }
        private void LogToScreen(String msg)
        {
            if (_WinLog != null)
            {
                ctlLog cl;
                //ListBox lb;
                //lb = (ListBox)_WinLog.UserData;
                //lf = (LogForm)_WinLog.UserData;
                cl = (ctlLog)_WinLog.UserData;
                //lb.Items.Add(msg);
                // Old stuff from Dave
                cl.AddItemToLog(msg);
                // New stuff
                TOCcl.AddItemToLog(msg);
                //lf.AddItemToLog(msg);
                //LogWindow docWin = (LogWindow) _WinLog;
                //docWin.AddMsg(msg);

                //// TOCDocClicker TOCcl;
                //ESRI.ArcGIS.ArcMapUI.IMxDocument pMxDoc;
                //pMxDoc = (ESRI.ArcGIS.ArcMapUI.IMxDocument) _ArcMap.Document;
                
                ////Find the contents view tab to put the log message in.  
                //for (int i = 0; i <= pMxDoc.ContentsViewCount; i++){
                //  //todo: replace the string with a var
                //  string tempStr = pMxDoc.get_ContentsView(i).Name;
                //  if (tempStr == "DocClickerTab") { 
                //    TOCcl = (TOCDocClicker)pMxDoc.get_ContentsView(i);
                //    // TOCcl.AddItemToLog(msg);
                //    break;
                //  }
                //}

                  
                
            }
        }
    }
}
