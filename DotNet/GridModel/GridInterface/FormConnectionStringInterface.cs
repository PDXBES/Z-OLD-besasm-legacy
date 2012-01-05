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
    public partial class FormConnectionStringInterface : Form
    {
        public string Database = "";
        public string Server = "";
        public string UserID = "";
        public string Password = "";
        public string Domain = "";
        public string previousConnectionString = "";
        public bool useTrustedConnection = false;
        public bool SQLisSource = false;

        public FormConnectionStringInterface(string thePreviousConnectionString)
        {
            InitializeComponent();

            textBoxDatabase.Text = Database;
            textBoxServer.Text = Server;
            textBoxUserID.Text = UserID;
            textBoxPassword.Text = Password;
            textBoxDomain.Text = Domain;
            checkBoxUseTrustedConnection.Checked = useTrustedConnection;
            previousConnectionString = thePreviousConnectionString;

            //this constructor should be passed a connection string, even an empty one.
            //the connection string must be parsed for the current values so that the
            //user can at least know what the current state of the connection string is.
            //Also, in case the user changes the connection string to something that doesn't
            //work, then we can use the passed connection string to undo the changes
            //made by the user.
        }

        private void checkBoxUseTrustedConnection_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUseTrustedConnection.Checked == true)
            {
                textBoxUserID.ReadOnly = true;
                textBoxPassword.ReadOnly = true;
                textBoxDomain.ReadOnly = true;
            }
            else
            {
                textBoxUserID.ReadOnly = false;
                textBoxPassword.ReadOnly = false;
                textBoxDomain.ReadOnly = false;
            }
        }

        private void FormConnectionStringInterface_FormClosing(object sender, FormClosingEventArgs e)
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
