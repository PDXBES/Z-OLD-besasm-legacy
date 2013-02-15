namespace SanitaryAnalysisTools
    {
    partial class LayerManagerGUI
        {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
            {
            if ( disposing && ( components != null ) )
                {
                components.Dispose ();
                }
            base.Dispose ( disposing );
            }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
            {
            this.cboDSCLayer = new System.Windows.Forms.ComboBox ();
            this.label1 = new System.Windows.Forms.Label ();
            this.cboDiscoTable = new System.Windows.Forms.ComboBox ();
            this.label2 = new System.Windows.Forms.Label ();
            this.okChanges = new System.Windows.Forms.Button ();
            this.cancelButton = new System.Windows.Forms.Button ();
            this.ckJoinByDSC = new System.Windows.Forms.CheckBox ();
            this.SuspendLayout ();
            // 
            // cboDSCLayer
            // 
            this.cboDSCLayer.FormattingEnabled = true;
            this.cboDSCLayer.Location = new System.Drawing.Point ( 83, 12 );
            this.cboDSCLayer.Name = "cboDSCLayer";
            this.cboDSCLayer.Size = new System.Drawing.Size ( 246, 21 );
            this.cboDSCLayer.TabIndex = 0;
            this.cboDSCLayer.SelectedIndexChanged += new System.EventHandler ( this.cboDSCLayer_SelectedIndexChanged );
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point ( 12, 12 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size ( 29, 13 );
            this.label1.TabIndex = 1;
            this.label1.Text = "DSC";
            // 
            // cboDiscoTable
            // 
            this.cboDiscoTable.FormattingEnabled = true;
            this.cboDiscoTable.Location = new System.Drawing.Point ( 83, 49 );
            this.cboDiscoTable.Name = "cboDiscoTable";
            this.cboDiscoTable.Size = new System.Drawing.Size ( 246, 21 );
            this.cboDiscoTable.TabIndex = 2;
            this.cboDiscoTable.SelectedIndexChanged += new System.EventHandler ( this.cboDiscoTable_SelectedIndexChanged );
            // 
            // label2
            // 
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point ( 12, 49 );
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size ( 63, 26 );
            this.label2.TabIndex = 3;
            this.label2.Text = "Stormwater Data Table";
            this.label2.UseWaitCursor = true;
            // 
            // okChanges
            // 
            this.okChanges.Location = new System.Drawing.Point ( 83, 152 );
            this.okChanges.Name = "okChanges";
            this.okChanges.Size = new System.Drawing.Size ( 75, 23 );
            this.okChanges.TabIndex = 4;
            this.okChanges.Text = "OK";
            this.okChanges.UseVisualStyleBackColor = true;
            this.okChanges.Click += new System.EventHandler ( this.okChanges_Click );
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point ( 174, 152 );
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size ( 75, 23 );
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler ( this.cancelButton_Click );
            // 
            // ckJoinByDSC
            // 
            this.ckJoinByDSC.AutoSize = true;
            this.ckJoinByDSC.Location = new System.Drawing.Point ( 83, 90 );
            this.ckJoinByDSC.Name = "ckJoinByDSC";
            this.ckJoinByDSC.Size = new System.Drawing.Size ( 101, 17 );
            this.ckJoinByDSC.TabIndex = 6;
            this.ckJoinByDSC.Text = "Join by DSCID?";
            this.ckJoinByDSC.UseVisualStyleBackColor = true;
            // 
            // LayerManagerGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size ( 341, 187 );
            this.Controls.Add ( this.ckJoinByDSC );
            this.Controls.Add ( this.cancelButton );
            this.Controls.Add ( this.okChanges );
            this.Controls.Add ( this.label2 );
            this.Controls.Add ( this.cboDiscoTable );
            this.Controls.Add ( this.label1 );
            this.Controls.Add ( this.cboDSCLayer );
            this.Name = "LayerManagerGUI";
            this.Text = "LayerManagerGUI";
            this.ResumeLayout ( false );
            this.PerformLayout ();

            }

        #endregion

        private System.Windows.Forms.ComboBox cboDSCLayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDiscoTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button okChanges;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox ckJoinByDSC;
        }
    }