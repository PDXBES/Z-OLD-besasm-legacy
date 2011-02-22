using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SystemsAnalysis.Grid.GridAnalysis
{
    public partial class FormUserIDAndPasswordEntry : Form
    {
        public event EventHandler InfoFormClosed; 
        public string UserID="";
        public string Password="";
        public string Domain="";
        public bool useTrustedConnection=false;

        public FormUserIDAndPasswordEntry()
        {
            InitializeComponent();

            textBoxUserID.Text = UserID;
            textBoxPassword.Text = Password;
            textBoxDomain.Text = Domain;
            checkBoxUseTrustedConnection.Checked = useTrustedConnection;
        }

        private void FormUserIDAndPasswordEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            UserID = textBoxUserID.Text;
            Password = textBoxPassword.Text;
            Domain = textBoxDomain.Text;
            useTrustedConnection = checkBoxUseTrustedConnection.Checked;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
