using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.IO;

namespace SystemsAnalysis.Characterization
{
    public partial class SplashScreen : Form
    {
        public SplashScreen(string versionText, string dateText)
        {
            InitializeComponent();
            
            ultraLabel1.Text = "Version " + versionText + "\n" + dateText;
            //'Bitmap b = new Bitmap(this.BackgroundImage);
            //'b.MakeTransparent(b.GetPixel(1, 1));
            //this.BackgroundImage = b; 
            timer1.Start();
        }

        private void SplashScreen_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void ultraPictureBox1_Click(object sender, EventArgs e)
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
            Assembly ass = Assembly.GetExecutingAssembly();
            Stream stream = ass.GetManifestResourceStream("SystemsAnalysis.Characterization.SplashRepGen.png");
            Image SplashScreen = Image.FromStream(stream);

            gfx.DrawImage(SplashScreen, new Rectangle(0, 0, this.Width, this.Height));

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public DialogResult ShowDialog(Boolean wait)
        {
            if (wait)
            {
                timer1.Stop();
            }
            return this.ShowDialog();
        }
    }
}