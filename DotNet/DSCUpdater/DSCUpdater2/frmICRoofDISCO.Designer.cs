namespace DSCUpdater2
{
    partial class frmICRoofDISCO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmICRoofDISCO));
            this.btnOKRoofDISCO = new System.Windows.Forms.Button();
            this.txtNewRoofDISCOICArea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.btnCancelRoofDISCO = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewRoofDISCOICArea)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOKRoofDISCO
            // 
            this.btnOKRoofDISCO.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOKRoofDISCO.Location = new System.Drawing.Point(15, 52);
            this.btnOKRoofDISCO.Name = "btnOKRoofDISCO";
            this.btnOKRoofDISCO.Size = new System.Drawing.Size(75, 23);
            this.btnOKRoofDISCO.TabIndex = 0;
            this.btnOKRoofDISCO.Text = "OK";
            this.btnOKRoofDISCO.UseVisualStyleBackColor = true;
            // 
            // txtNewRoofDISCOICArea
            // 
            this.txtNewRoofDISCOICArea.Location = new System.Drawing.Point(15, 25);
            this.txtNewRoofDISCOICArea.Name = "txtNewRoofDISCOICArea";
            this.txtNewRoofDISCOICArea.NullText = "Enter roof DISCO IC area";
            appearance1.FontData.ItalicAsString = "True";
            this.txtNewRoofDISCOICArea.NullTextAppearance = appearance1;
            this.txtNewRoofDISCOICArea.Size = new System.Drawing.Size(142, 21);
            this.txtNewRoofDISCOICArea.TabIndex = 1;
            // 
            // btnCancelRoofDISCO
            // 
            this.btnCancelRoofDISCO.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelRoofDISCO.Location = new System.Drawing.Point(15, 81);
            this.btnCancelRoofDISCO.Name = "btnCancelRoofDISCO";
            this.btnCancelRoofDISCO.Size = new System.Drawing.Size(75, 23);
            this.btnCancelRoofDISCO.TabIndex = 2;
            this.btnCancelRoofDISCO.Text = "Cancel";
            this.btnCancelRoofDISCO.UseVisualStyleBackColor = true;
            // 
            // frmICRoofDISCO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 108);
            this.Controls.Add(this.btnCancelRoofDISCO);
            this.Controls.Add(this.txtNewRoofDISCOICArea);
            this.Controls.Add(this.btnOKRoofDISCO);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmICRoofDISCO";
            this.Text = "DSCUpdater: Roof DISCO";
            this.Load += new System.EventHandler(this.frmICRoofDISCO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtNewRoofDISCOICArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOKRoofDISCO;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNewRoofDISCOICArea;
        private System.Windows.Forms.Button btnCancelRoofDISCO;
    }
}