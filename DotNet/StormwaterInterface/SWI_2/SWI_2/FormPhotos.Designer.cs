namespace SWI_2
{
    partial class FormPhotos
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
            this.dataGridViewPhotos = new System.Windows.Forms.DataGridView();
            this.photoidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.globalidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sWSPPHOTOBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sANDBOXDataSet = new SWI_2.SANDBOXDataSet();
            this.buttonDone = new System.Windows.Forms.Button();
            this.labelGlobalIDInfo = new System.Windows.Forms.Label();
            this.buttonUpdatePhoto = new System.Windows.Forms.Button();
            this.buttonDeletePhoto = new System.Windows.Forms.Button();
            this.buttonAddRow = new System.Windows.Forms.Button();
            this.sWSP_PHOTOTableAdapter = new SWI_2.SANDBOXDataSetTableAdapters.SWSP_PHOTOTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPhotos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPPHOTOBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANDBOXDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPhotos
            // 
            this.dataGridViewPhotos.AllowUserToAddRows = false;
            this.dataGridViewPhotos.AllowUserToDeleteRows = false;
            this.dataGridViewPhotos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPhotos.AutoGenerateColumns = false;
            this.dataGridViewPhotos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPhotos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.photoidDataGridViewTextBoxColumn,
            this.globalidDataGridViewTextBoxColumn,
            this.locationDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn});
            this.dataGridViewPhotos.DataSource = this.sWSPPHOTOBindingSource;
            this.dataGridViewPhotos.Location = new System.Drawing.Point(12, 36);
            this.dataGridViewPhotos.Name = "dataGridViewPhotos";
            this.dataGridViewPhotos.Size = new System.Drawing.Size(988, 230);
            this.dataGridViewPhotos.TabIndex = 0;
            // 
            // photoidDataGridViewTextBoxColumn
            // 
            this.photoidDataGridViewTextBoxColumn.DataPropertyName = "photo_id";
            this.photoidDataGridViewTextBoxColumn.HeaderText = "photo_id";
            this.photoidDataGridViewTextBoxColumn.Name = "photoidDataGridViewTextBoxColumn";
            this.photoidDataGridViewTextBoxColumn.ReadOnly = true;
            this.photoidDataGridViewTextBoxColumn.Visible = false;
            // 
            // globalidDataGridViewTextBoxColumn
            // 
            this.globalidDataGridViewTextBoxColumn.DataPropertyName = "global_id";
            this.globalidDataGridViewTextBoxColumn.HeaderText = "global_id";
            this.globalidDataGridViewTextBoxColumn.Name = "globalidDataGridViewTextBoxColumn";
            this.globalidDataGridViewTextBoxColumn.Visible = false;
            // 
            // locationDataGridViewTextBoxColumn
            // 
            this.locationDataGridViewTextBoxColumn.DataPropertyName = "location";
            this.locationDataGridViewTextBoxColumn.HeaderText = "File Path";
            this.locationDataGridViewTextBoxColumn.Name = "locationDataGridViewTextBoxColumn";
            this.locationDataGridViewTextBoxColumn.Width = 600;
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "comment";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            this.commentDataGridViewTextBoxColumn.Width = 300;
            // 
            // sWSPPHOTOBindingSource
            // 
            this.sWSPPHOTOBindingSource.DataMember = "SWSP_PHOTO";
            this.sWSPPHOTOBindingSource.DataSource = this.sANDBOXDataSet;
            // 
            // sANDBOXDataSet
            // 
            this.sANDBOXDataSet.DataSetName = "SANDBOXDataSet";
            this.sANDBOXDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // buttonDone
            // 
            this.buttonDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDone.Location = new System.Drawing.Point(838, 268);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(162, 36);
            this.buttonDone.TabIndex = 1;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // labelGlobalIDInfo
            // 
            this.labelGlobalIDInfo.AutoSize = true;
            this.labelGlobalIDInfo.Location = new System.Drawing.Point(13, 9);
            this.labelGlobalIDInfo.Name = "labelGlobalIDInfo";
            this.labelGlobalIDInfo.Size = new System.Drawing.Size(72, 13);
            this.labelGlobalIDInfo.TabIndex = 2;
            this.labelGlobalIDInfo.Text = "Global ID Info";
            // 
            // buttonUpdatePhoto
            // 
            this.buttonUpdatePhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdatePhoto.Location = new System.Drawing.Point(578, 268);
            this.buttonUpdatePhoto.Name = "buttonUpdatePhoto";
            this.buttonUpdatePhoto.Size = new System.Drawing.Size(162, 36);
            this.buttonUpdatePhoto.TabIndex = 3;
            this.buttonUpdatePhoto.Text = "Update Photo";
            this.buttonUpdatePhoto.UseVisualStyleBackColor = true;
            this.buttonUpdatePhoto.Click += new System.EventHandler(this.buttonUpdatePhoto_Click);
            // 
            // buttonDeletePhoto
            // 
            this.buttonDeletePhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeletePhoto.Location = new System.Drawing.Point(290, 268);
            this.buttonDeletePhoto.Name = "buttonDeletePhoto";
            this.buttonDeletePhoto.Size = new System.Drawing.Size(162, 36);
            this.buttonDeletePhoto.TabIndex = 4;
            this.buttonDeletePhoto.Text = "Delete Photo";
            this.buttonDeletePhoto.UseVisualStyleBackColor = true;
            this.buttonDeletePhoto.Click += new System.EventHandler(this.buttonDeletePhoto_Click);
            // 
            // buttonAddRow
            // 
            this.buttonAddRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddRow.Location = new System.Drawing.Point(12, 268);
            this.buttonAddRow.Name = "buttonAddRow";
            this.buttonAddRow.Size = new System.Drawing.Size(162, 36);
            this.buttonAddRow.TabIndex = 5;
            this.buttonAddRow.Text = "Add Photo";
            this.buttonAddRow.UseVisualStyleBackColor = true;
            this.buttonAddRow.Click += new System.EventHandler(this.buttonAddRow_Click);
            // 
            // sWSP_PHOTOTableAdapter
            // 
            this.sWSP_PHOTOTableAdapter.ClearBeforeFill = true;
            // 
            // FormPhotos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 316);
            this.Controls.Add(this.buttonAddRow);
            this.Controls.Add(this.buttonDeletePhoto);
            this.Controls.Add(this.buttonUpdatePhoto);
            this.Controls.Add(this.labelGlobalIDInfo);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.dataGridViewPhotos);
            this.Name = "FormPhotos";
            this.Text = "Photos";
            this.Load += new System.EventHandler(this.FormPhotos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPhotos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sWSPPHOTOBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sANDBOXDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPhotos;
        private SANDBOXDataSet sANDBOXDataSet;
        private System.Windows.Forms.BindingSource sWSPPHOTOBindingSource;
        private SWI_2.SANDBOXDataSetTableAdapters.SWSP_PHOTOTableAdapter sWSP_PHOTOTableAdapter;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.Label labelGlobalIDInfo;
        private System.Windows.Forms.Button buttonUpdatePhoto;
        private System.Windows.Forms.Button buttonDeletePhoto;
        private System.Windows.Forms.Button buttonAddRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn photoidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn globalidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
    }
}