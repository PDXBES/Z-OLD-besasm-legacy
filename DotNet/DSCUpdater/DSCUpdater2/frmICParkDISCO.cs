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
    public partial class frmICParkDISCO : Form
    {
        public frmMain MyParentForm;

        public string ParkDISCOICArea
        {
            get
            {
                return txtNewParkDISCOICArea.Text;
            }
        }

        public frmICParkDISCO()
        {
            InitializeComponent();
        }

        private void frmICParkDISCO_Load(object sender, EventArgs e)
        {
            txtNewParkDISCOICArea.Text = "";
        }
    }
}
