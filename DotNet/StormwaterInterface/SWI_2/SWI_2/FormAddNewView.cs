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
    public partial class FormAddNewView : Form
    {
        int number = 0;

        public int GetNumber()
        {
            return number;
        }

        public FormAddNewView()
        {
            InitializeComponent();
        }

        private void buttonAddView_Click(object sender, EventArgs e)
        {
            number = (int)this.numericUpDownNewViewNumber.Value;
            this.Close();
        }
    }
}
