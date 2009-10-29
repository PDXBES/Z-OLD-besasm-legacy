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
    public partial class frmICParkDrywell : Form
    {
        public frmMain MyParentForm;

        public string ParkDrywellICArea
        {
            get
            {
                return txtNewParkDrywellICArea.Text;
            }
        }
        
        public frmICParkDrywell()
        {
            InitializeComponent();
        }

        private void frmICParkDrywell_Load(object sender, EventArgs e)
        {
            txtNewParkDrywellICArea.Text = "";
        }
    }
}
