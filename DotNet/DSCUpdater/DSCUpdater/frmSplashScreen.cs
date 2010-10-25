using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.IO;

namespace DSCUpdater
{
    public partial class frmSplashScreen : Form
    {
        public frmSplashScreen(string versionText, string dateText)
        {
            InitializeComponent();

            lblVersionInfo.Text = "Version " + versionText + "\n" + dateText;
            tmrSplashScreen.Start();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {

        }

    }
}
