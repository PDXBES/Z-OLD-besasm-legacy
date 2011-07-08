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
        #region events
        private void frmSplashScreen_Load(object sender, EventArgs e)
        {

        }

        private void tmrSplashScreen_Tick(object sender, EventArgs e)
        {
          DialogResult = DialogResult.OK;
        }

        private void frmSplashScreen_Click(object sender, EventArgs e)
        {
          DialogResult = DialogResult.OK;
        }
        #endregion

        #region methods
        protected override void OnPaint(PaintEventArgs e)
        {
          //Do nothing here
        }  
      
        protected override void OnPaintBackground(PaintEventArgs e)
          {
            Graphics gfx = e.Graphics;
            Assembly assemb = Assembly.GetExecutingAssembly();
            Stream imageStream = assemb.GetManifestResourceStream("DSCUpdater.DSCUpdaterSplash.png");
            Image imgSplashScreen = Image.FromStream(imageStream);

            gfx.DrawImage(imgSplashScreen, new Rectangle(0, 0, this.Width, this.Height));
          }

        public DialogResult ShowDialog(Boolean wait)
        {
          if (wait)
          {
            tmrSplashScreen.Stop();
          }
          return this.ShowDialog();
        }
        #endregion

    }
}
