namespace DSCUpdater2
{
    partial class frmICRoofDrywell
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmICRoofDrywell));
            this.txtNewRoofDrywellICArea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.btnOKRoofDrywell = new System.Windows.Forms.Button();
            this.btnCancelRoofDrywell = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewRoofDrywellICArea)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNewRoofDrywellICArea
            // 
            this.txtNewRoofDrywellICArea.Location = new System.Drawing.Point(15, 25);
            this.txtNewRoofDrywellICArea.Name = "txtNewRoofDrywellICArea";
            this.txtNewRoofDrywellICArea.NullText = "Enter roof drywell IC area";
            appearance1.FontData.ItalicAsString = "True";
            this.txtNewRoofDrywellICArea.NullTextAppearance = appearance1;
            this.txtNewRoofDrywellICArea.Size = new System.Drawing.Size(142, 21);
            this.txtNewRoofDrywellICArea.TabIndex = 1;
            // 
            // btnOKRoofDrywell
            // 
            this.btnOKRoofDrywell.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOKRoofDrywell.Location = new System.Drawing.Point(15, 52);
            this.btnOKRoofDrywell.Name = "btnOKRoofDrywell";
            this.btnOKRoofDrywell.Size = new System.Drawing.Size(75, 23);
            this.btnOKRoofDrywell.TabIndex = 1;
            this.btnOKRoofDrywell.Text = "OK";
            this.btnOKRoofDrywell.UseVisualStyleBackColor = true;
            // 
            // btnCancelRoofDrywell
            // 
            this.btnCancelRoofDrywell.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelRoofDrywell.Location = new System.Drawing.Point(15, 82);
            this.btnCancelRoofDrywell.Name = "btnCancelRoofDrywell";
            this.btnCancelRoofDrywell.Size = new System.Drawing.Size(75, 23);
            this.btnCancelRoofDrywell.TabIndex = 2;
            this.btnCancelRoofDrywell.Text = "Cancel";
            this.btnCancelRoofDrywell.UseVisualStyleBackColor = true;
            // 
            // frmICRoofDrywell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 108);
            this.Controls.Add(this.btnCancelRoofDrywell);
            this.Controls.Add(this.btnOKRoofDrywell);
            this.Controls.Add(this.txtNewRoofDrywellICArea);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmICRoofDrywell";
            this.Text = "DSCUpdater: Roof Drywell";
            this.Load += new System.EventHandler(this.frmICRoofDrywell_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtNewRoofDrywellICArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNewRoofDrywellICArea;
        private System.Windows.Forms.Button btnOKRoofDrywell;
        private System.Windows.Forms.Button btnCancelRoofDrywell;
    }
}