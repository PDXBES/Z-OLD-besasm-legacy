namespace DSCUpdater
{
    partial class frmICParkDISCO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmICParkDISCO));
            this.txtNewParkDISCOICArea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.btnOKParkDISCO = new System.Windows.Forms.Button();
            this.btnCancelParkDISCO = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewParkDISCOICArea)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNewParkDISCOICArea
            // 
            this.txtNewParkDISCOICArea.Location = new System.Drawing.Point(15, 25);
            this.txtNewParkDISCOICArea.Name = "txtNewParkDISCOICArea";
            this.txtNewParkDISCOICArea.NullText = "Enter parking DISCO IC area";
            appearance1.FontData.ItalicAsString = "True";
            this.txtNewParkDISCOICArea.NullTextAppearance = appearance1;
            this.txtNewParkDISCOICArea.Size = new System.Drawing.Size(159, 21);
            this.txtNewParkDISCOICArea.TabIndex = 1;
            // 
            // btnOKParkDISCO
            // 
            this.btnOKParkDISCO.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOKParkDISCO.Location = new System.Drawing.Point(15, 52);
            this.btnOKParkDISCO.Name = "btnOKParkDISCO";
            this.btnOKParkDISCO.Size = new System.Drawing.Size(75, 23);
            this.btnOKParkDISCO.TabIndex = 1;
            this.btnOKParkDISCO.Text = "OK";
            this.btnOKParkDISCO.UseVisualStyleBackColor = true;
            // 
            // btnCancelParkDISCO
            // 
            this.btnCancelParkDISCO.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelParkDISCO.Location = new System.Drawing.Point(15, 82);
            this.btnCancelParkDISCO.Name = "btnCancelParkDISCO";
            this.btnCancelParkDISCO.Size = new System.Drawing.Size(75, 23);
            this.btnCancelParkDISCO.TabIndex = 2;
            this.btnCancelParkDISCO.Text = "Cancel";
            this.btnCancelParkDISCO.UseVisualStyleBackColor = true;
            // 
            // frmICParkDISCO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 108);
            this.Controls.Add(this.btnCancelParkDISCO);
            this.Controls.Add(this.btnOKParkDISCO);
            this.Controls.Add(this.txtNewParkDISCOICArea);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmICParkDISCO";
            this.Text = "DSCUpdater: Park DISCO";
            this.Load += new System.EventHandler(this.frmICParkDISCO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtNewParkDISCOICArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNewParkDISCOICArea;
        private System.Windows.Forms.Button btnOKParkDISCO;
        private System.Windows.Forms.Button btnCancelParkDISCO;
    }
}