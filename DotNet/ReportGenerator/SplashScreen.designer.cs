namespace SystemsAnalysis.Reporting
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
          this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
          this.tmrSplashScreen = new System.Windows.Forms.Timer(this.components);
          this.SuspendLayout();
          // 
          // ultraLabel1
          // 
          appearance1.BackColor = System.Drawing.Color.Transparent;
          appearance1.TextHAlignAsString = "Right";
          this.ultraLabel1.Appearance = appearance1;
          this.ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
          this.ultraLabel1.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.None;
          this.ultraLabel1.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ultraLabel1.Location = new System.Drawing.Point(286, 248);
          this.ultraLabel1.Margin = new System.Windows.Forms.Padding(2);
          this.ultraLabel1.Name = "ultraLabel1";
          this.ultraLabel1.Size = new System.Drawing.Size(160, 29);
          this.ultraLabel1.TabIndex = 0;
          this.ultraLabel1.Text = "Version X.X, January 1st 1996";
          // 
          // tmrSplashScreen
          // 
          this.tmrSplashScreen.Interval = 2000;
          this.tmrSplashScreen.Tick += new System.EventHandler(this.timer1_Tick);
          // 
          // SplashScreen
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackgroundImage = global::SystemsAnalysis.Reporting.Properties.Resources.SplashGReMLIn;
          this.ClientSize = new System.Drawing.Size(467, 346);
          this.ControlBox = false;
          this.Controls.Add(this.ultraLabel1);
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
          this.Margin = new System.Windows.Forms.Padding(2);
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "SplashScreen";
          this.ShowInTaskbar = false;
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Text = "SplashScreen";
          this.TopMost = true;
          this.Load += new System.EventHandler(this.SplashScreen_Load);
          this.Click += new System.EventHandler(this.SplashScreen_Click);
          this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private System.Windows.Forms.Timer tmrSplashScreen;


    }
}