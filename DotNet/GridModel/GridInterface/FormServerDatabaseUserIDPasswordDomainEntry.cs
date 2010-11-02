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


        public FormServerDatabaseUserIDPasswordDomainEntry()
        {
            InitializeComponent();

            textBoxDatabase.Text = Database;
            textBoxServer.Text = Server;
            textBoxUserID.Text = UserID;
            textBoxPassword.Text = Password;
            textBoxDomain.Text = Domain;
            checkBoxUseTrustedConnection.Checked = useTrustedConnection;
        }

        private void FormServerDatabaseUserIDPasswordDomainEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            UserID = textBoxUserID.Text;
            Password = textBoxPassword.Text;
            Domain = textBoxDomain.Text;
            Server = textBoxServer.Text;
            Database = textBoxDatabase.Text;
            useTrustedConnection = checkBoxUseTrustedConnection.Checked;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
