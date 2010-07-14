namespace FFEUpdater
{
    partial class frmAbout
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
            this.btnCloseAboutForm = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCloseAboutForm
            // 
            this.btnCloseAboutForm.Location = new System.Drawing.Point(151, 106);
            this.btnCloseAboutForm.Name = "btnCloseAboutForm";
            this.btnCloseAboutForm.Size = new System.Drawing.Size(75, 23);
            this.btnCloseAboutForm.TabIndex = 1;
            this.btnCloseAboutForm.Text = "Close";
            this.btnCloseAboutForm.UseVisualStyleBackColor = true;
            this.btnCloseAboutForm.Click += new System.EventHandler(this.btnCloseAboutForm_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(34, 25);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(52, 13);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "lblVersion";
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 140);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.btnCloseAboutForm);
            this.Name = "frmAbout";
            this.Text = "About FFE Updater";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCloseAboutForm;
        private System.Windows.Forms.Label lblVersion;
    }
}