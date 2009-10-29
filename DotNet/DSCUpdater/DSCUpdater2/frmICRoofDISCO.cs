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
    public partial class frmICRoofDISCO : Form
    {
        public frmMain MyParentForm;

        public string RoofDISCOICArea
        {
            get
            {
                return txtNewRoofDISCOICArea.Text;
            }
        }

        public frmICRoofDISCO()
        {
            InitializeComponent();
        }

        private void frmICRoofDISCO_Load(object sender, EventArgs e)
        {
            txtNewRoofDISCOICArea.Text = "";
        }
    }
}
