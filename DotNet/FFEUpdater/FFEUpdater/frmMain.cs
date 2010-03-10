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
            InitializeComponent();      
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            statusStrip1.Text = "Initializing...";
        }

        public void FnPrBar()
        {
            //progressBar1.Increment(1);
            //statusStrip1.Text = "Running FFE Update " + progressBar1.Value.ToString() + "% Complete";
            //if (progressBar1.Value == progressBar1.Maximum)
            //{
            //    timer1.Stop();
            //    MessageBox.Show("FFE Update Complete");
            //    this.Close();
            //    timer1.Stop();
            //}
        }

        private void btnUpdateFFE_Click(object sender, EventArgs e)
        {
            try
            {
                // Object for missing (or optional) arguments.
                object oMissing = System.Reflection.Missing.Value;

                // Create an instance of Microsoft Access, make it visible,
                // and open mst_DSC_ac.mdb
                Access.ApplicationClass oAccess = new Access.ApplicationClass();
                oAccess.Visible = true;
                oAccess.OpenCurrentDatabase("W:\\SAMaster_22\\Parcels_Divides\\mst_DSC_ac.mdb", false, "");

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
                throw ex;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fi;
            fi=@"\\Cassio\asm_apps\Apps\FFEUpdater\publish.htm";
            System.Diagnostics.Process.Start("IEXPLORE.EXE", fi);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FnPrBar();
        }

        private void RunMacro(object oApp, object[] oRunArgs)
        {
            oApp.GetType().InvokeMember("Run",
                System.Reflection.BindingFlags.Default |
                System.Reflection.BindingFlags.InvokeMethod,
                null, oApp, oRunArgs);
        }
    }
}
