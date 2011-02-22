namespace FFEUpdater
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplashScreen));
            this.tmrSplashScreen = new System.Windows.Forms.Timer(this.components);
            this.lblVersionText = new Infragistics.Win.Misc.UltraLabel();
            this.SuspendLayout();
            // 
            // tmrSplashScreen
            // 
            this.tmrSplashScreen.Interval = 5000;
            this.tmrSplashScreen.Tick += new System.EventHandler(this.tmrSplashScreen_Tick);
            // 
            // lblVersionText
            // 
            appearance1.BackColor = System.Drawing.Color.Transparent;
            appearance1.TextHAlignAsString = "Right";
            this.lblVersionText.Appearance = appearance1;
            this.lblVersionText.AutoSize = true;
            this.lblVersionText.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
            this.lblVersionText.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.None;
            this.lblVersionText.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersionText.Location = new System.Drawing.Point(265, 200);
            this.lblVersionText.Margin = new System.Windows.Forms.Padding(2);
            this.lblVersionText.Name = "lblVersionText";
            this.lblVersionText.Size = new System.Drawing.Size(157, 14);
            this.lblVersionText.TabIndex = 0;
            this.lblVersionText.Text = "Version X.X, January 1st 2000";
            // 
            // frmSplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(380, 280);
            this.ControlBox = false;
            this.Controls.Add(this.lblVersionText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSplashScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            this.Click += new System.EventHandler(this.SplashScreen_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrSplashScreen;
        private Infragistics.Win.Misc.UltraLabel lblVersionText;
    }
}