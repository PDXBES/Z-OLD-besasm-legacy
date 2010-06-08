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
    public partial class FormAddSurvey : Form
    {
        private int _MapNo;
        private int _SurveyPage;
        private FormMain _MyParentForm;

        public FormAddSurvey()
        {
            InitializeComponent();
        }

        public FormMain MyParentForm
        {
            get { return _MyParentForm; }
            set { _MyParentForm = value; }
        }

        public int MapNo
        {
            get { return _MapNo; }
            set { _MapNo = value; }
        }

        public int SurveyPage
        {
            get { return _SurveyPage; }
            set { _SurveyPage = value; }
        }

        private void FormAddSurvey_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_VIEW' table. You can move, or remove it, as needed.
            this.sWSP_VIEWTableAdapter.Fill(this.sANDBOXDataSet.SWSP_VIEW);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SUBWATERSHED' table. You can move, or remove it, as needed.
            this.sWSP_SUBWATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_SUBWATERSHED);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_WATERSHED' table. You can move, or remove it, as needed.
            this.sWSP_WATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_WATERSHED);
            numericUpDownView.Value = MapNo;
            numericUpDownAddPage.Value = SurveyPage;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                SWI_2.SANDBOXDataSetTableAdapters.SWSP_VIEWTableAdapter taV = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_VIEWTableAdapter();

                SWI_2.SANDBOXDataSetTableAdapters.SWSP_SURVEY_PAGETableAdapter taS = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_SURVEY_PAGETableAdapter();
                taS.Insert(taV.ScalarQueryViewIDByViewNumber((int)numericUpDownView.Value), (int)numericUpDownAddPage.Value, System.DateTime.Today, "", "");

                _MyParentForm.CurrentView = (int)numericUpDownView.Value;
                _MyParentForm.CurrentSurveyPage = (int)numericUpDownAddPage.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Survey page already exists!");
            }
            
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
