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
    public partial class FormAddView : Form
    {
        private int _MapNo;
        private int _SurveyPage;
        private int _Watershed;
        private int _Subwatershed;
        private FormMain _MyParentForm;

        public FormAddView()
        {
            InitializeComponent();
        }

        public int SurveyPage
        {
            get { return _SurveyPage; }
            set { _SurveyPage = value; }
        }

        public int MapNo
        {
            get { return _MapNo; }
            set { _MapNo = value; }
        }

        public int Watershed
        {
            get { return _Watershed; }
            set { _Watershed = value; }
        }

        public int Subwatershed
        {
            get { return _Subwatershed; }
            set { _Subwatershed = value; }
        }

        public FormMain MyParentForm
        {
            get { return _MyParentForm; }
            set { _MyParentForm = value; }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                SWI_2.SANDBOXDataSetTableAdapters.SWSP_VIEWTableAdapter taV = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_VIEWTableAdapter();
                taV.Insert((int)comboBoxSubwatershed.SelectedValue, (int)numericUpDownAddView.Value, "");
                
                SWI_2.SANDBOXDataSetTableAdapters.SWSP_SURVEY_PAGETableAdapter taS = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_SURVEY_PAGETableAdapter();
                taS.Insert(taV.ScalarQuery(), (int)numericUpDownAddFirstSurveyPage.Value, System.DateTime.Today, "", "");
                //_MyParentForm is for base users, so they need to be told what they were doing.
                //homefully administrators can remember.
                if (_MyParentForm != null)
                {
                    _MyParentForm.CurrentView = (int)numericUpDownAddView.Value;
                    _MyParentForm.CurrentSurveyPage = (int)numericUpDownAddFirstSurveyPage.Value;
                    _MyParentForm.CurrentWatershed = (int)comboBoxWatershed.SelectedValue;
                    _MyParentForm.CurrentSubwatershed = (int)comboBoxSubwatershed.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("View already exists!");
            }
            this.Close();
        }

        private void FormAddView_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_WATERSHED' table. You can move, or remove it, as needed.
            this.sWSP_WATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_WATERSHED);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SUBWATERSHED' table. You can move, or remove it, as needed.
            this.sWSP_SUBWATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_SUBWATERSHED);

            //_MyParentForm is for base users, so they need to be told what they were doing.
            //homefully administrators can remember.
            if (_MyParentForm != null)
            {
                while (_MyParentForm.CurrentWatershed != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
                {
                    sWSPWATERSHEDBindingSource.MoveNext();
                }
                while (_MyParentForm.CurrentSubwatershed != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
                {
                    fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
                }
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
