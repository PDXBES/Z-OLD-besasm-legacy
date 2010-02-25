//Application Name:  FFEUpdater
//Author: J. Ruben Gonzalez-Baird
//Version: see \\Cassio\asm_apps\Apps\FFEUpdater\publish.htm
//Version Date: see \\Cassio\asm_apps\Apps\FFEUpdater\publish.htm
//Purpose: this application is intended to serve as the interface for use when updating the 
//         mst_DSC_ac database based on changes in the FFE database.  The program runs a macro
//         named UpdateFFE in the DSC database, which in turn executes two queries to update
//         the pertinent data in the FloodRefElev ans HasBasement fields as necessary. 

//Required References: Access, Microsoft Access, ADODB
//Release Notes:

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ADODB;
using System.Reflection;
using Access = Microsoft.Office.Interop.Access;
using Microsoft.Office.Core;
using System.Threading;
using System.IO;

namespace FFEUpdater
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            System.Threading.Thread th = new System.Threading.Thread(DoSplash);
            th.Start();
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            statusStrip.Text = "Initializing...";
            tabControlMain.Visible = false;
            //tabControlMain.Tabs.Remove(tabPageMain);
        }

        /// <summary>
        /// Displays splash screen (for multithreading)
        /// </summary>
        private static void DoSplash()
        {
            DoSplash(false);
        } // DoSplash()

        /// <summary>
        /// Loads the splash screen during start-up
        /// </summary>
        private static void DoSplash(bool waitForClick)
        {
            string versionText = "x.x.x.x";
            string dateText = "1/1/1900";
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                Version v = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                versionText = v.Major + "." + v.Minor + "." + v.Build + "." + v.Revision;
                //debug msg
                //MessageBox.Show("is network deployed");
                //MessageBox.Show("Major=" + v.Major + "Minor=" + v.Minor + "Build=" + v.Build + "Revision=" + v.Revision);
                dateText = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString("MMMM dd yyyy");
            } // if
            else
            {
                System.Reflection.AssemblyName assemblyName =
                System.Reflection.Assembly.GetExecutingAssembly().GetName(false);
                int minorVersion = assemblyName.Version.Minor;
                int majorVersion = assemblyName.Version.Major;
                int build = assemblyName.Version.Build;
                int revision = assemblyName.Version.Revision;
                versionText = string.Format("{0}.{1}.{2}.{3}", majorVersion, minorVersion, build, revision);
                //debug msg
                //MessageBox.Show("not network deployed");
                //MessageBox.Show("Major=" + majorVersion + "Minor=" + minorVersion + "Build=" + build + "Revision=" + revision);
                FileInfo fi = new FileInfo("FFEUpdater.exe");
                dateText = fi.CreationTime.Date.ToString("MMMM dd yyyy");
            } // else

            using (frmSplashScreen sp = new frmSplashScreen(versionText, dateText))
            {
                sp.ShowDialog(waitForClick);
            }
        } // DoSplash(waitForClick)

        private void btnUpdateFFE_Click(object sender, EventArgs e)
        {
            try
            {
                // Object for missing (or optional) arguments.
                object oMissing = System.Reflection.Missing.Value;

                // Create an instance of Microsoft Access, make it visible,
                // and open Survey_FFE.mdb
                Access.ApplicationClass oAccess = new Access.ApplicationClass();
                oAccess.Visible = true;
                oAccess.OpenCurrentDatabase("W:\\SAMaster_22\\Parcels_Divides\\Survey_FFE.mdb", false, "");

                //Debug message
                //MessageBox.Show("Access open");

                //Run the macro.
                RunMacro(oAccess, new Object[] { "UpdateFFE" });

                //Quit Access and clean up.
                oAccess.DoCmd.Quit(Access.AcQuitOption.acQuitSaveNone);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oAccess);
                oAccess = null;

                MessageBox.Show("All update processes ran successfully", "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "FFEUpdater: Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fi;
            fi = @"\\Cassio\asm_apps\Apps\FFEUpdater\publish.htm";
            System.Diagnostics.Process.Start("IEXPLORE.EXE", fi);
        }

        private void RunMacro(object oApp, object[] oRunArgs)
        {
            oApp.GetType().InvokeMember("Run",
                System.Reflection.BindingFlags.Default |
                System.Reflection.BindingFlags.InvokeMethod,
                null, oApp, oRunArgs);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCloseFFEUpdater_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void expBarMain_ItemClick(object sender, Infragistics.Win.UltraWinExplorerBar.ItemEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                switch (e.Item.Key)
                {
                    case "RunFFEUpdate":
                        RunFFEUpdate();
                        break;
                    default:
                        return;
                } //switch
                return;
            } //try
            finally
            {
                Cursor = Cursors.Default;
            }//finally
        } //expBarMain_ItemClick (sender, e)

        private void RunFFEUpdate()
        {
            //debug msg
            //MessageBox.Show("Running RunFFEUpdate");
            tabControlMain.Visible = true;
        }
    }
}
