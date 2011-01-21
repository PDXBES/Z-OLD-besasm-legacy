namespace DSCUpdater
{
    partial class SplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.tmrSplashScreen = new System.Windows.Forms.Timer(this.components);
            this.lblVersionInfo = new Infragistics.Win.Misc.UltraLabel();
            this.SuspendLayout();
            // 
            // lblVersionInfo
            // 
            appearance1.BackColor = System.Drawing.Color.Transparent;
            appearance1.TextHAlignAsString = "Right";
            this.lblVersionInfo.Appearance = appearance1;
            this.lblVersionInfo.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
            this.lblVersionInfo.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.None;
            this.lblVersionInfo.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersionInfo.Location = new System.Drawing.Point(176, 236);
            this.lblVersionInfo.Margin = new System.Windows.Forms.Padding(2);
            this.lblVersionInfo.Name = "lblVersionInfo";
            this.lblVersionInfo.Size = new System.Drawing.Size(160, 29);
            this.lblVersionInfo.TabIndex = 0;
            this.lblVersionInfo.Text = "Version X.X, January 1st, 2000";
            // 
            // SplashScreen
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
            this.Name = "SplashScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrSplashScreen;
        private Infragistics.Win.Misc.UltraLabel lblVersionInfo;
    }
}