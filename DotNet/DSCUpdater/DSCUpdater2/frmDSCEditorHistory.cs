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
    public partial class frmDSCEditorHistory : Form
    {
        public frmDSCEditorHistory()
        {
            InitializeComponent();
        }

        private void frmDSCEditorHistory_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'projectDataSet.SESSION' table. You can move, or remove it, as needed.
            this.sESSIONTableAdapter.Fill(this.projectDataSet.SESSION);

        }

        private void btnCloseDSCEditorHistory_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoadSelected_Click(object sender, EventArgs e)
        {

        }
    }
}
