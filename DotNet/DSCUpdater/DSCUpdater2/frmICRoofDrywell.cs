using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSCUpdater
{
    public partial class frmICRoofDrywell : Form
    {
        public frmMain MyParentForm;

        public string RoofDrywellICArea
        {
            get
            {
                return txtNewRoofDrywellICArea.Text;
            }
        }

        public frmICRoofDrywell()
        {
            InitializeComponent();
        }

        private void frmICRoofDrywell_Load(object sender, EventArgs e)
        {
            txtNewRoofDrywellICArea.Text = "";
        }
    }
}
