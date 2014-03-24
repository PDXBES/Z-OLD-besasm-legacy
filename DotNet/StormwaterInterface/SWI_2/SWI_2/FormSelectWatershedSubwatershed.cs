using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SWI_2
{
    public partial class FormSelectWatershedSubwatershed : Form
    {
        private FormMain _MyParentForm;

        public FormSelectWatershedSubwatershed()
        {
            InitializeComponent();
        }

        public FormMain MyParentForm
        {
            get { return _MyParentForm; }
            set { _MyParentForm = value; }
        }

        private void FormSelectWatershedSubwatershed_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SUBWATERSHED' table. You can move, or remove it, as needed.
            this.sWSP_SUBWATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_SUBWATERSHED);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_WATERSHED' table. You can move, or remove it, as needed.
            this.sWSP_WATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_WATERSHED);

        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (_MyParentForm != null)
                {
                    _MyParentForm.SelectedWatershed = (string)listBoxWatershed.SelectedValue;
                    _MyParentForm.SelectedSubwatershed = (string)listBoxSubwatershed.SelectedValue;
                    _MyParentForm.UserClickedCancel = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Such Watershed/Subwatershed combinations do not exist!");
                _MyParentForm.UserClickedCancel = true;
            }

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _MyParentForm.UserClickedCancel = true;
        }
    }
}
