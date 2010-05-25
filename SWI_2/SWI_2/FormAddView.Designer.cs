namespace SWI_2
{
    partial class FormAddView
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.numericUpDownAddFirstSurveyPage = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAddView = new System.Windows.Forms.NumericUpDown();
            this.labelAddFirstSurveyPage = new System.Windows.Forms.Label();
            this.labelAddView = new System.Windows.Forms.Label();
            this.labelSubwatershed = new System.Windows.Forms.Label();
            this.labelWatershed = new System.Windows.Forms.Label();
            this.comboBoxWatershed = new System.Windows.Forms.ComboBox();
            this.comboBoxSubwatershed = new System.Windows.Forms.ComboBox();
            this.sANDBOXDataSet = new SWI_2.SANDBOXDataSet();
            this.sWSPWATERSHEDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSP_WATERSHEDTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_WATERSHEDTableAdapter();
            this.fKSUBWATERSHEDWATERSHEDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sWSP_SUBWATERSHEDTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_SUBWATERSHEDTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAddFirstSurveyPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAddView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANDBOXDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPWATERSHEDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSUBWATERSHEDWATERSHEDBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(199, 154);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(70, 28);
            this.buttonCancel.TabIndex = 19;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(105, 156);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(76, 27);
            this.buttonOK.TabIndex = 18;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // numericUpDownAddFirstSurveyPage
            // 
            this.numericUpDownAddFirstSurveyPage.Location = new System.Drawing.Point(138, 112);
            this.numericUpDownAddFirstSurveyPage.Name = "numericUpDownAddFirstSurveyPage";
            this.numericUpDownAddFirstSurveyPage.Size = new System.Drawing.Size(54, 20);
            this.numericUpDownAddFirstSurveyPage.TabIndex = 17;
            // 
            // numericUpDownAddView
            // 
            this.numericUpDownAddView.Location = new System.Drawing.Point(139, 75);
            this.numericUpDownAddView.Name = "numericUpDownAddView";
            this.numericUpDownAddView.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownAddView.TabIndex = 16;
            // 
            // labelAddFirstSurveyPage
            // 
            this.labelAddFirstSurveyPage.AutoSize = true;
            this.labelAddFirstSurveyPage.Location = new System.Drawing.Point(23, 114);
            this.labelAddFirstSurveyPage.Name = "labelAddFirstSurveyPage";
            this.labelAddFirstSurveyPage.Size = new System.Drawing.Size(106, 13);
            this.labelAddFirstSurveyPage.TabIndex = 13;
            this.labelAddFirstSurveyPage.Text = "Add first survey page";
            // 
            // labelAddView
            // 
            this.labelAddView.AutoSize = true;
            this.labelAddView.Location = new System.Drawing.Point(23, 77);
            this.labelAddView.Name = "labelAddView";
            this.labelAddView.Size = new System.Drawing.Size(51, 13);
            this.labelAddView.TabIndex = 12;
            this.labelAddView.Text = "Add view";
            // 
            // labelSubwatershed
            // 
            this.labelSubwatershed.AutoSize = true;
            this.labelSubwatershed.Location = new System.Drawing.Point(23, 43);
            this.labelSubwatershed.Name = "labelSubwatershed";
            this.labelSubwatershed.Size = new System.Drawing.Size(75, 13);
            this.labelSubwatershed.TabIndex = 11;
            this.labelSubwatershed.Text = "Subwatershed";
            // 
            // labelWatershed
            // 
            this.labelWatershed.AutoSize = true;
            this.labelWatershed.Location = new System.Drawing.Point(23, 16);
            this.labelWatershed.Name = "labelWatershed";
            this.labelWatershed.Size = new System.Drawing.Size(59, 13);
            this.labelWatershed.TabIndex = 10;
            this.labelWatershed.Text = "Watershed";
            // 
            // comboBoxWatershed
            // 
            this.comboBoxWatershed.DataSource = this.sWSPWATERSHEDBindingSource;
            this.comboBoxWatershed.DisplayMember = "watershed";
            this.comboBoxWatershed.FormattingEnabled = true;
            this.comboBoxWatershed.Location = new System.Drawing.Point(169, 9);
            this.comboBoxWatershed.Name = "comboBoxWatershed";
            this.comboBoxWatershed.Size = new System.Drawing.Size(114, 21);
            this.comboBoxWatershed.TabIndex = 20;
            this.comboBoxWatershed.ValueMember = "watershed_id";
            // 
            // comboBoxSubwatershed
            // 
            this.comboBoxSubwatershed.DataSource = this.fKSUBWATERSHEDWATERSHEDBindingSource;
            this.comboBoxSubwatershed.DisplayMember = "subwatershed";
            this.comboBoxSubwatershed.FormattingEnabled = true;
            this.comboBoxSubwatershed.Location = new System.Drawing.Point(168, 37);
            this.comboBoxSubwatershed.Name = "comboBoxSubwatershed";
            this.comboBoxSubwatershed.Size = new System.Drawing.Size(114, 21);
            this.comboBoxSubwatershed.TabIndex = 21;
            this.comboBoxSubwatershed.ValueMember = "subwatershed_id";
            // 
            // sANDBOXDataSet
            // 
            this.sANDBOXDataSet.DataSetName = "SANDBOXDataSet";
            this.sANDBOXDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sWSPWATERSHEDBindingSource
            // 
            this.sWSPWATERSHEDBindingSource.DataMember = "SWSP_WATERSHED";
            this.sWSPWATERSHEDBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // sWSP_WATERSHEDTableAdapter
            // 
            this.sWSP_WATERSHEDTableAdapter.ClearBeforeFill = true;
            // 
            // fKSUBWATERSHEDWATERSHEDBindingSource
            // 
            this.fKSUBWATERSHEDWATERSHEDBindingSource.DataMember = "FK_SUBWATERSHED_WATERSHED";
            this.fKSUBWATERSHEDWATERSHEDBindingSource.DataSource = this.sWSPWATERSHEDBindingSource;
            // 
            // sWSP_SUBWATERSHEDTableAdapter
            // 
            this.sWSP_SUBWATERSHEDTableAdapter.ClearBeforeFill = true;
            // 
            // FormAddView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 196);
            this.Controls.Add(this.comboBoxSubwatershed);
            this.Controls.Add(this.comboBoxWatershed);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.numericUpDownAddFirstSurveyPage);
            this.Controls.Add(this.numericUpDownAddView);
            this.Controls.Add(this.labelAddFirstSurveyPage);
            this.Controls.Add(this.labelAddView);
            this.Controls.Add(this.labelSubwatershed);
            this.Controls.Add(this.labelWatershed);
            this.Name = "FormAddView";
            this.Text = "Add view";
            this.Load += new System.EventHandler(this.FormAddView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAddFirstSurveyPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAddView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANDBOXDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPWATERSHEDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fKSUBWATERSHEDWATERSHEDBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.NumericUpDown numericUpDownAddFirstSurveyPage;
        private System.Windows.Forms.NumericUpDown numericUpDownAddView;
        private System.Windows.Forms.Label labelAddFirstSurveyPage;
        private System.Windows.Forms.Label labelAddView;
        private System.Windows.Forms.Label labelSubwatershed;
        private System.Windows.Forms.Label labelWatershed;
        private System.Windows.Forms.ComboBox comboBoxWatershed;
        private System.Windows.Forms.ComboBox comboBoxSubwatershed;
        private SANDBOXDataSet sANDBOXDataSet;
        private System.Windows.Forms.BindingSource sWSPWATERSHEDBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_WATERSHEDTableAdapter sWSP_WATERSHEDTableAdapter;
        private System.Windows.Forms.BindingSource fKSUBWATERSHEDWATERSHEDBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_SUBWATERSHEDTableAdapter sWSP_SUBWATERSHEDTableAdapter;
    }
}