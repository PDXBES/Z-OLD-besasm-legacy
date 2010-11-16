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
    public partial class FormServerDatabaseUserIDPasswordDomainEntry : Form
    {
        public event EventHandler BigInfoFormClosed;
        public string Database = "";
        public string Server = "";
        public string UserID = "";
        public string Password = "";
        public string Domain = "";
        public bool useTrustedConnection = false;
        public bool SQLisSource = false;


        public FormServerDatabaseUserIDPasswordDomainEntry()
        {
            InitializeComponent();

            textBoxDatabase.Text = Database;
            textBoxServer.Text = Server;
            textBoxUserID.Text = UserID;
            textBoxPassword.Text = Password;
            textBoxDomain.Text = Domain;
            checkBoxUseTrustedConnection.Checked = useTrustedConnection;
            radioButtonAccessDB.Checked = true;
        }

        private void FormServerDatabaseUserIDPasswordDomainEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            UserID = textBoxUserID.Text;
            Password = textBoxPassword.Text;
            Domain = textBoxDomain.Text;
            Server = textBoxServer.Text;
            Database = textBoxDatabase.Text;
            useTrustedConnection = checkBoxUseTrustedConnection.Checked;
            SQLisSource = radioButtonSQLServer.Checked;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButtonSQLServer_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSQLServer.Checked == true)
            {
                textBoxServer.Enabled = true;
                textBoxUserID.Enabled = true;
                textBoxPassword.Enabled = true;
                textBoxDomain.Enabled = true;
                textBoxDatabase.Enabled = true;
            }
        }

        private void radioButtonAccessDB_CheckedChanged(object sender, EventArgs e)
        {
            textBoxServer.Enabled = false;
            textBoxUserID.Enabled = false;
            textBoxPassword.Enabled = false;
            textBoxDomain.Enabled = false;
            textBoxDatabase.Enabled = false;
        }
    }
}
