using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Reflection;

namespace FFEUpdater
{
    public partial class frmSplashScreen : Form
    {
        public frmSplashScreen(string versionText, string dateText)
        {
            InitializeComponent();
            lblVersionText.Text = "Version " + versionText + "\n" + dateText;
            tmrSplashScreen.Start();
        }

        private void SplashScreen_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //  Do nothing here!
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;
            Assembly prgAssembly = Assembly.GetExecutingAssembly();
            Stream stream = prgAssembly.GetManifestResourceStream("FFEUpdater.SplashFFEUpdater.png");
            Image SplashScreen = Image.FromStream(stream);
            gfx.DrawImage(SplashScreen, new Rectangle(0, 0, this.Width, this.Height));
        }

        private void tmrSplashScreen_Tick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public DialogResult ShowDialog(Boolean wait)
        {
            if (wait)
            {
                tmrSplashScreen.Stop();
            }
            return this.ShowDialog();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
