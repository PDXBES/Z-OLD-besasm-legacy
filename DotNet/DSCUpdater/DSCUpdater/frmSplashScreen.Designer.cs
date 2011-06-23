namespace DSCUpdater
{
    partial class frmSplashScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
          this.components = new System.ComponentModel.Container();
          Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplashScreen));
          this.tmrSplashScreen = new System.Windows.Forms.Timer(this.components);
          this.lblVersionInfo = new Infragistics.Win.Misc.UltraLabel();
          this.SuspendLayout();
          // 
          // tmrSplashScreen
          // 
          this.tmrSplashScreen.Tick += new System.EventHandler(this.tmrSplashScreen_Tick);
          // 
          // lblVersionInfo
          // 
          appearance2.BackColor = System.Drawing.Color.Transparent;
          this.lblVersionInfo.Appearance = appearance2;
          this.lblVersionInfo.Font = new System.Drawing.Font("Arial", 8.25F);
          this.lblVersionInfo.Location = new System.Drawing.Point(178, 237);
          this.lblVersionInfo.Name = "lblVersionInfo";
          this.lblVersionInfo.Size = new System.Drawing.Size(172, 23);
          this.lblVersionInfo.TabIndex = 0;
          this.lblVersionInfo.Text = "Version X.X, January 1st, 2000";
          // 
          // frmSplashScreen
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
          this.ClientSize = new System.Drawing.Size(362, 285);
          this.ControlBox = false;
          this.Controls.Add(this.lblVersionInfo);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "frmSplashScreen";
          this.ShowInTaskbar = false;
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "SplashScreen";
          this.TopMost = true;
          this.Load += new System.EventHandler(this.frmSplashScreen_Load);
          this.Click += new System.EventHandler(this.frmSplashScreen_Click);
          this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrSplashScreen;
        private Infragistics.Win.Misc.UltraLabel lblVersionInfo;
    }
}