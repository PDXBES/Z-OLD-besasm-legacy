using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSCUpdater2
{
    public partial class frmDSCUpdaterEditor : Form
    {
        public int mstDSCRoofArea;
        public int mstDSCParkArea;

        public frmDSCUpdaterEditor()
        {
            InitializeComponent();
        }

        public void FindEditHistory(int editID)
        {

        }

        private void frmDSCUpdaterEditor_Load(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtNewRoofArea.Text = "";
            txtNewParkArea.Text = "";
            txtNewICRoofArea.Text = "";
            txtNewICParkArea.Text = "";
            chkICRoofDISCO.Checked=false;
            chkICRoofDrywell.Checked=false;
            chkICParkDISCO.Checked=false;
            chkICParkDrywell.Checked=false;
        }

        private void chkICRoofDISCO_CheckedChanged(object sender, EventArgs e)
        {
            if (chkICRoofDISCO.Checked == true)
            {
                //Create instance of Roof IC DISCO Form
                frmICRoofDISCO fm = new frmICRoofDISCO();
                //Display the form
                fm.Show();
                //MessageBox.Show("Show IC Roof DISCO Form");
            }
        }

        private void chkICRoofDrywell_CheckedChanged(object sender, EventArgs e)
        {
            if (chkICRoofDrywell.Checked == true)
            {   //Create instance of Roof IC Drywell Form
                frmICRoofDrywell fm = new frmICRoofDrywell();
                //Display the form
                fm.Show();
                //MessageBox.Show("Show IC Roof Drywell Form");
            }
        }

        private void chkICParkDISCO_CheckedChanged(object sender, EventArgs e)
        {
            if (chkICParkDISCO.Checked == true)
            {
                //Create instance of Park IC DISCO Form
                frmICParkDISCO fm = new frmICParkDISCO();
                //Display the form
                fm.Show();
                //MessageBox.Show("Show IC Park DISCO Form");
            }
        }

        private void chkICParkDrywell_CheckedChanged(object sender, EventArgs e)
        {
            if (chkICParkDrywell.Checked == true)
            {
                //Create instance of Park IC Drywell Form
                frmICParkDrywell fm = new frmICParkDrywell();
                //Display the form
                fm.Show();
                //MessageBox.Show("Show IC Park Drywell Form");
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            //Ask Arnel how to do this
            if (Convert.ToInt32(txtNewICRoofArea.Text) > 0)
            {
                MessageBox.Show("Warning! Sum of roof drywell and DISCO areas greater than total roof IC areas!", "Roof IC Area Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            //Ask Arnel
            if (Convert.ToInt32(txtNewICParkArea.Text) > 0)
            {
                MessageBox.Show("Warning! Sum of parking drywell and DISCO areas greater than total parking IC areas!", "Parking IC Area Error",MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

            //Arnel
            if (Convert.ToInt32(txtNewICRoofArea.Text) > mstDSCRoofArea)
            {
                MessageBox.Show("Warning! Roof IC area cannot be greater than total roof area.  Revise roof and/or IC areas and re-submit.", "Roof IC Area Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Arnel
            if (Convert.ToInt32(txtNewICParkArea.Text) > mstDSCParkArea)
            {
                MessageBox.Show("Warning! Parking IC area cannot be greater than total parking area.  Revise parking and/or IC areas and re-submit.", "Parking IC Area Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
