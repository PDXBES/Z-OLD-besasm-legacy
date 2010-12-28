using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StormwaterInterface
{
    public partial class FormSearchByNodeNo : Form
    {
        private string _Message;

        public FormSearchByNodeNo()
        {
            InitializeComponent();
        }

        private void buttonSeachForThisNode_Click(object sender, EventArgs e)
        {
            this._Message = textBox1.Text;
            this.Close();
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

    }
}
