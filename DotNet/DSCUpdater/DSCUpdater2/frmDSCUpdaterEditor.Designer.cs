namespace DSCUpdater2
{
    partial class frmDSCUpdaterEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDSCUpdaterEditor));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dgvUpdaterEditor = new System.Windows.Forms.DataGridView();
            this.bindNavUpdaterEditor = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.txtNewRoofArea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtNewParkArea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtNewICRoofArea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtNewICParkArea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblRNO = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkICRoofDISCO = new System.Windows.Forms.CheckBox();
            this.chkICRoofDrywell = new System.Windows.Forms.CheckBox();
            this.chkICParkDISCO = new System.Windows.Forms.CheckBox();
            this.chkICParkDrywell = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpdaterEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindNavUpdaterEditor)).BeginInit();
            this.bindNavUpdaterEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewRoofArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewParkArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewICRoofArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewICParkArea)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(425, 132);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(165, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(549, 250);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(93, 23);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Submit Changes";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // dgvUpdaterEditor
            // 
            this.dgvUpdaterEditor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUpdaterEditor.Location = new System.Drawing.Point(12, 24);
            this.dgvUpdaterEditor.Name = "dgvUpdaterEditor";
            this.dgvUpdaterEditor.Size = new System.Drawing.Size(240, 150);
            this.dgvUpdaterEditor.TabIndex = 2;
            // 
            // bindNavUpdaterEditor
            // 
            this.bindNavUpdaterEditor.AddNewItem = null;
            this.bindNavUpdaterEditor.CountItem = this.bindingNavigatorCountItem;
            this.bindNavUpdaterEditor.DeleteItem = null;
            this.bindNavUpdaterEditor.Dock = System.Windows.Forms.DockStyle.None;
            this.bindNavUpdaterEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.bindNavUpdaterEditor.Location = new System.Drawing.Point(12, 177);
            this.bindNavUpdaterEditor.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindNavUpdaterEditor.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindNavUpdaterEditor.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindNavUpdaterEditor.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindNavUpdaterEditor.Name = "bindNavUpdaterEditor";
            this.bindNavUpdaterEditor.PositionItem = this.bindingNavigatorPositionItem;
            this.bindNavUpdaterEditor.Size = new System.Drawing.Size(239, 25);
            this.bindNavUpdaterEditor.TabIndex = 3;
            this.bindNavUpdaterEditor.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // txtNewRoofArea
            // 
            this.txtNewRoofArea.Location = new System.Drawing.Point(425, 24);
            this.txtNewRoofArea.Name = "txtNewRoofArea";
            this.txtNewRoofArea.NullText = "Type to enter new roof Area";
            appearance1.FontData.ItalicAsString = "True";
            this.txtNewRoofArea.NullTextAppearance = appearance1;
            this.txtNewRoofArea.Size = new System.Drawing.Size(165, 21);
            this.txtNewRoofArea.TabIndex = 4;
            // 
            // txtNewParkArea
            // 
            this.txtNewParkArea.Location = new System.Drawing.Point(425, 51);
            this.txtNewParkArea.Name = "txtNewParkArea";
            this.txtNewParkArea.NullText = "Type to enter new park area";
            appearance2.FontData.ItalicAsString = "True";
            this.txtNewParkArea.NullTextAppearance = appearance2;
            this.txtNewParkArea.Size = new System.Drawing.Size(165, 21);
            this.txtNewParkArea.TabIndex = 5;
            // 
            // txtNewICRoofArea
            // 
            this.txtNewICRoofArea.Location = new System.Drawing.Point(425, 78);
            this.txtNewICRoofArea.Name = "txtNewICRoofArea";
            this.txtNewICRoofArea.NullText = "Type to enter new ICRoof area";
            appearance3.FontData.ItalicAsString = "True";
            this.txtNewICRoofArea.NullTextAppearance = appearance3;
            this.txtNewICRoofArea.Size = new System.Drawing.Size(165, 21);
            this.txtNewICRoofArea.TabIndex = 6;
            // 
            // txtNewICParkArea
            // 
            this.txtNewICParkArea.Location = new System.Drawing.Point(425, 105);
            this.txtNewICParkArea.Name = "txtNewICParkArea";
            this.txtNewICParkArea.NullText = "Type to enter new ICPark area";
            appearance4.FontData.ItalicAsString = "True";
            this.txtNewICParkArea.NullTextAppearance = appearance4;
            this.txtNewICParkArea.Size = new System.Drawing.Size(165, 21);
            this.txtNewICParkArea.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(648, 250);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblRNO
            // 
            this.lblRNO.AutoSize = true;
            this.lblRNO.Location = new System.Drawing.Point(258, 9);
            this.lblRNO.Name = "lblRNO";
            this.lblRNO.Size = new System.Drawing.Size(34, 13);
            this.lblRNO.TabIndex = 9;
            this.lblRNO.Text = "RNO:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Current roof area: (sq ft)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(258, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Current park area (sq ft)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(258, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Current roof IC area: (sq ft)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(258, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Current park IC area: (sq ft)";
            // 
            // chkICRoofDISCO
            // 
            this.chkICRoofDISCO.AutoSize = true;
            this.chkICRoofDISCO.Location = new System.Drawing.Point(597, 82);
            this.chkICRoofDISCO.Name = "chkICRoofDISCO";
            this.chkICRoofDISCO.Size = new System.Drawing.Size(59, 17);
            this.chkICRoofDISCO.TabIndex = 14;
            this.chkICRoofDISCO.Text = "DISCO";
            this.chkICRoofDISCO.UseVisualStyleBackColor = true;
            this.chkICRoofDISCO.CheckedChanged += new System.EventHandler(this.chkICRoofDISCO_CheckedChanged);
            // 
            // chkICRoofDrywell
            // 
            this.chkICRoofDrywell.AutoSize = true;
            this.chkICRoofDrywell.Location = new System.Drawing.Point(662, 82);
            this.chkICRoofDrywell.Name = "chkICRoofDrywell";
            this.chkICRoofDrywell.Size = new System.Drawing.Size(60, 17);
            this.chkICRoofDrywell.TabIndex = 15;
            this.chkICRoofDrywell.Text = "Drywell";
            this.chkICRoofDrywell.UseVisualStyleBackColor = true;
            this.chkICRoofDrywell.CheckedChanged += new System.EventHandler(this.chkICRoofDrywell_CheckedChanged);
            // 
            // chkICParkDISCO
            // 
            this.chkICParkDISCO.AutoSize = true;
            this.chkICParkDISCO.Location = new System.Drawing.Point(597, 108);
            this.chkICParkDISCO.Name = "chkICParkDISCO";
            this.chkICParkDISCO.Size = new System.Drawing.Size(59, 17);
            this.chkICParkDISCO.TabIndex = 16;
            this.chkICParkDISCO.Text = "DISCO";
            this.chkICParkDISCO.UseVisualStyleBackColor = true;
            this.chkICParkDISCO.CheckedChanged += new System.EventHandler(this.chkICParkDISCO_CheckedChanged);
            // 
            // chkICParkDrywell
            // 
            this.chkICParkDrywell.AutoSize = true;
            this.chkICParkDrywell.Location = new System.Drawing.Point(662, 108);
            this.chkICParkDrywell.Name = "chkICParkDrywell";
            this.chkICParkDrywell.Size = new System.Drawing.Size(60, 17);
            this.chkICParkDrywell.TabIndex = 17;
            this.chkICParkDrywell.Text = "Drywell";
            this.chkICParkDrywell.UseVisualStyleBackColor = true;
            this.chkICParkDrywell.CheckedChanged += new System.EventHandler(this.chkICParkDrywell_CheckedChanged);
            // 
            // frmDSCUpdaterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 285);
            this.Controls.Add(this.chkICParkDrywell);
            this.Controls.Add(this.chkICParkDISCO);
            this.Controls.Add(this.chkICRoofDrywell);
            this.Controls.Add(this.chkICRoofDISCO);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblRNO);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtNewICParkArea);
            this.Controls.Add(this.txtNewICRoofArea);
            this.Controls.Add(this.txtNewParkArea);
            this.Controls.Add(this.txtNewRoofArea);
            this.Controls.Add(this.bindNavUpdaterEditor);
            this.Controls.Add(this.dgvUpdaterEditor);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnClear);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDSCUpdaterEditor";
            this.Text = "DSCUpdater: Updater Editor";
            this.Load += new System.EventHandler(this.frmDSCUpdaterEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpdaterEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindNavUpdaterEditor)).EndInit();
            this.bindNavUpdaterEditor.ResumeLayout(false);
            this.bindNavUpdaterEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewRoofArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewParkArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewICRoofArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewICParkArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.DataGridView dgvUpdaterEditor;
        private System.Windows.Forms.BindingNavigator bindNavUpdaterEditor;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNewRoofArea;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNewParkArea;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNewICRoofArea;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNewICParkArea;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblRNO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkICRoofDISCO;
        private System.Windows.Forms.CheckBox chkICRoofDrywell;
        private System.Windows.Forms.CheckBox chkICParkDISCO;
        private System.Windows.Forms.CheckBox chkICParkDrywell;
    }
}