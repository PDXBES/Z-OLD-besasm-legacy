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
    public partial class FormSWSPFieldDataAdministration : Form
    {
        public FormSWSPFieldDataAdministration()
        {
            InitializeComponent();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _TextBrush;
            Brush _FillBrush;

            // Get the item from the collection.
            TabPage _TabPage = tabControl1.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _TabBounds = tabControl1.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {
                // Draw a different background color, and don't paint a focus rectangle.
                _TextBrush = new SolidBrush(Color.Black);
                _FillBrush = new SolidBrush(this.BackColor);
                g.FillRectangle((Brush)_FillBrush /*Brushes.LightGray*/, e.Bounds);
            }
            else
            {
                _TextBrush = new System.Drawing.SolidBrush(Color.LightGray/*e.ForeColor*/);
                e.DrawBackground();
            }

            // Use our own font. Because we CAN.
            Font _TabFont = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _StringFlags = new StringFormat();
            _StringFlags.Alignment = StringAlignment.Center;
            _StringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_TabPage.Text, _TabFont, _TextBrush,
                         _TabBounds, new StringFormat(_StringFlags));
        }
        private void buttonUpdateDatabase_Click(object sender, EventArgs e)
        {
            FormSWSPFieldDataAdministration child = new FormSWSPFieldDataAdministration();

            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;
        }

        private void buttonAddView_Click(object sender, EventArgs e)
        {
            FormAddView child = new FormAddView();

            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;
        }

        private void buttonAddSurveyPage_Click(object sender, EventArgs e)
        {
            FormAddSurvey child = new FormAddSurvey();

            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;
        }

        private void FormSWSPFieldDataAdministration_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SUBWATERSHED' table. You can move, or remove it, as needed.
            this.sWSP_SUBWATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_SUBWATERSHED);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_WATERSHED' table. You can move, or remove it, as needed.
            this.sWSP_WATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_WATERSHED);

        }
    }
}
