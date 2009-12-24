namespace DSCUpdater
{
    partial class frmICParkDrywell
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmICParkDrywell));
            this.btnOKParkDrywell = new System.Windows.Forms.Button();
            this.txtNewParkDrywellICArea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.btnCancelParkDrywell = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewParkDrywellICArea)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOKParkDrywell
            // 
            this.btnOKParkDrywell.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOKParkDrywell.Location = new System.Drawing.Point(15, 52);
            this.btnOKParkDrywell.Name = "btnOKParkDrywell";
            this.btnOKParkDrywell.Size = new System.Drawing.Size(75, 23);
            this.btnOKParkDrywell.TabIndex = 1;
            this.btnOKParkDrywell.Text = "OK";
            this.btnOKParkDrywell.UseVisualStyleBackColor = true;
            // 
            // txtNewParkDrywellICArea
            // 
            this.txtNewParkDrywellICArea.Location = new System.Drawing.Point(15, 25);
            this.txtNewParkDrywellICArea.Name = "txtNewParkDrywellICArea";
            this.txtNewParkDrywellICArea.NullText = "Enter parking drywell IC area";
            appearance1.FontData.ItalicAsString = "True";
            this.txtNewParkDrywellICArea.NullTextAppearance = appearance1;
            this.txtNewParkDrywellICArea.Size = new System.Drawing.Size(158, 21);
            this.txtNewParkDrywellICArea.TabIndex = 2;
            // 
            // btnCancelParkDrywell
            // 
            this.btnCancelParkDrywell.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelParkDrywell.Location = new System.Drawing.Point(15, 81);
            this.btnCancelParkDrywell.Name = "btnCancelParkDrywell";
            this.btnCancelParkDrywell.Size = new System.Drawing.Size(75, 23);
            this.btnCancelParkDrywell.TabIndex = 3;
            this.btnCancelParkDrywell.Text = "Cancel";
            this.btnCancelParkDrywell.UseVisualStyleBackColor = true;
            // 
            // frmICParkDrywell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 108);
            this.Controls.Add(this.btnCancelParkDrywell);
            this.Controls.Add(this.txtNewParkDrywellICArea);
            this.Controls.Add(this.btnOKParkDrywell);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmICParkDrywell";
            this.Text = "DSCUpdater: Park Drywell";
            this.Load += new System.EventHandler(this.frmICParkDrywell_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtNewParkDrywellICArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOKParkDrywell;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNewParkDrywellICArea;
        private System.Windows.Forms.Button btnCancelParkDrywell;
    }
}