namespace SystemsAnalysis.Utils.FileSelector
{
    partial class FileSelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            this.BrowserButton = new Infragistics.Win.Misc.UltraButton();
            this.SelectorLabel = new Infragistics.Win.Misc.UltraLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SelectorComboBox = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.mruList1 = new SystemsAnalysis.Utils.FileSelector.MRUList();
            ((System.ComponentModel.ISupportInitialize)(this.SelectorComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mruList1)).BeginInit();
            this.SuspendLayout();
            // 
            // BrowserButton
            // 
            this.BrowserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowserButton.Location = new System.Drawing.Point(508, 25);
            this.BrowserButton.Margin = new System.Windows.Forms.Padding(10);
            this.BrowserButton.Name = "BrowserButton";
            this.BrowserButton.Size = new System.Drawing.Size(77, 24);
            this.BrowserButton.TabIndex = 41;
            this.BrowserButton.Text = "Browse...";
            this.BrowserButton.Click += new System.EventHandler(this.BrowserButton_Click);
            // 
            // SelectorLabel
            // 
            appearance2.TextVAlignAsString = "Bottom";
            this.SelectorLabel.Appearance = appearance2;
            this.SelectorLabel.Location = new System.Drawing.Point(3, 0);
            this.SelectorLabel.Name = "SelectorLabel";
            this.SelectorLabel.Size = new System.Drawing.Size(604, 19);
            this.SelectorLabel.TabIndex = 42;
            this.SelectorLabel.Text = "Selector Description";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // SelectorComboBox
            // 
            this.SelectorComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectorComboBox.DataMember = "MRUFiles";
            this.SelectorComboBox.DataSource = this.mruList1;
            this.SelectorComboBox.DisplayMember = "File";
            this.SelectorComboBox.Location = new System.Drawing.Point(3, 25);
            this.SelectorComboBox.Name = "SelectorComboBox";
            this.SelectorComboBox.Size = new System.Drawing.Size(501, 24);
            this.SelectorComboBox.TabIndex = 40;
            this.SelectorComboBox.ValueMember = "File";
            this.SelectorComboBox.SelectionChangeCommitted += new System.EventHandler(this.SelectorComboBox_SelectionChangeCommitted);
            this.SelectorComboBox.ValueChanged += new System.EventHandler(this.SelectorComboBox_ValueChanged);
            // 
            // mruList1
            // 
            this.mruList1.DataSetName = "MRUList";
            this.mruList1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // FileSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SelectorComboBox);
            this.Controls.Add(this.BrowserButton);
            this.Controls.Add(this.SelectorLabel);
            this.Name = "FileSelector";
            this.Size = new System.Drawing.Size(589, 52);
            ((System.ComponentModel.ISupportInitialize)(this.SelectorComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mruList1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.Misc.UltraButton BrowserButton;
        private Infragistics.Win.Misc.UltraLabel SelectorLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        public Infragistics.Win.UltraWinEditors.UltraComboEditor SelectorComboBox;
        private SystemsAnalysis.Utils.FileSelector.MRUList mruList1;
    }
}
