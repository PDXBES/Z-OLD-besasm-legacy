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
        public string Database = "GRIDMODEL";
        public string Server = "SIRTOBY";
        public string UserID = "";
        public string Password = "";
        public string Domain = "";
        public bool useTrustedConnection = true;
        public bool SQLisSource = true;


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
            radioButtonSQLServer.Checked = SQLisSource;
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
            }
        }

        private void radioButtonAccessDB_CheckedChanged(object sender, EventArgs e)
        {
            //if the user wants to use an access database, they must still supply a
            //SQL server to copy the database to.  
        }

        private void checkBoxUseTrustedConnection_CheckedChanged(object sender, EventArgs e)
        {

            textBoxPassword.Enabled = !textBoxPassword.Enabled;
            textBoxDomain.Enabled = !textBoxDomain.Enabled;
            textBoxUserID.Enabled = !textBoxUserID.Enabled;
        }
    }
}
