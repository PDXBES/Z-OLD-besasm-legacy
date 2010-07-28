using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SystemsAnalysis.GIS.SDETools
    {
    public partial class MainForm : Form
        {
        public MainForm ()
            {
            InitializeComponent ();

            }

        private void toolExit_Click ( object sender, EventArgs e )
            {
            // Close the application
            this.Close ();
            }

        private void sDEConnectionsToolStripMenuItem_Click ( object sender, EventArgs e )
            {
            frmSDEconnection sdeConnectionForm = new frmSDEconnection (  );
            sdeConnectionForm.MdiParent = this;
            sdeConnectionForm.Show ();
            mainFormStatusLabel.Text = "Add or Update SDE Connections";

            MaximizeAll ();
            }

        private void mxdFixerUpperToolStripMenuItem_Click ( object sender, EventArgs e )
            {
            frmChangeSDE changeSDEForm = new frmChangeSDE (  );
            changeSDEForm.MdiParent = this;
            changeSDEForm.Show ();
            mainFormStatusLabel.Text = "Change SDE Connections in MXD";
            MaximizeAll ();
            }

        private void MaximizeAll ()
            {
            //Gets forms that represent the MDI child forms 
            //that are parented to this form in an array 
            Form[] charr = this.MdiChildren;

            //For each child form set the window state to Maximized 
            foreach ( Form chform in charr )
                chform.WindowState = FormWindowState.Maximized;
            }

        private void MainForm_Activated ( object sender, EventArgs e )
            {
            mainFormStatusLabel.Text = "SDE Utilities";
            }
        }
    }