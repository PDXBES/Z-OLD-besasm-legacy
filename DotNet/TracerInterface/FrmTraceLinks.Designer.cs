namespace SystemsAnalysis.Tracer
{
    partial class FrmTraceLinks
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
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTraceLinks));
            this.pnlMapControls = new System.Windows.Forms.Panel();
            this.btnSelectStopLink = new Infragistics.Win.Misc.UltraButton();
            this.btnSelectStartLink = new Infragistics.Win.Misc.UltraButton();
            this.btnPan = new Infragistics.Win.Misc.UltraButton();
            this.btnZoomOut = new Infragistics.Win.Misc.UltraButton();
            this.btnZoomIn = new Infragistics.Win.Misc.UltraButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.axMapControl = new ESRI.ArcGIS.Controls.AxMapControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkZoomToTrace = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.btnRefreshTrace = new Infragistics.Win.Misc.UltraButton();
            this.btnCancel = new Infragistics.Win.Misc.UltraButton();
            this.btnExportLinks = new Infragistics.Win.Misc.UltraButton();
            this.grpboxStartLinks = new Infragistics.Win.Misc.UltraGroupBox();
            this.lstStartLinks = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnRemoveStartLink = new Infragistics.Win.Misc.UltraButton();
            this.btnAddStartLink = new Infragistics.Win.Misc.UltraButton();
            this.grpboxStopLinks = new Infragistics.Win.Misc.UltraGroupBox();
            this.lstStopLinks = new System.Windows.Forms.ListBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnRemoveStopLink = new Infragistics.Win.Misc.UltraButton();
            this.btnAddStopLink = new Infragistics.Win.Misc.UltraButton();
            this.pnlMapControls.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpboxStartLinks)).BeginInit();
            this.grpboxStartLinks.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpboxStopLinks)).BeginInit();
            this.grpboxStopLinks.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMapControls
            // 
            this.pnlMapControls.Controls.Add(this.btnSelectStopLink);
            this.pnlMapControls.Controls.Add(this.btnSelectStartLink);
            this.pnlMapControls.Controls.Add(this.btnPan);
            this.pnlMapControls.Controls.Add(this.btnZoomOut);
            this.pnlMapControls.Controls.Add(this.btnZoomIn);
            this.pnlMapControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlMapControls.Location = new System.Drawing.Point(477, 23);
            this.pnlMapControls.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlMapControls.Name = "pnlMapControls";
            this.pnlMapControls.Size = new System.Drawing.Size(76, 311);
            this.pnlMapControls.TabIndex = 5;
            // 
            // btnSelectStopLink
            // 
            this.btnSelectStopLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectStopLink.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnSelectStopLink.Location = new System.Drawing.Point(4, 165);
            this.btnSelectStopLink.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSelectStopLink.Name = "btnSelectStopLink";
            this.btnSelectStopLink.Size = new System.Drawing.Size(67, 43);
            this.btnSelectStopLink.TabIndex = 4;
            this.btnSelectStopLink.Text = "Select Stop Link";
            this.btnSelectStopLink.Click += new System.EventHandler(this.btnSelectStopLink_Click);
            // 
            // btnSelectStartLink
            // 
            this.btnSelectStartLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectStartLink.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnSelectStartLink.Location = new System.Drawing.Point(4, 119);
            this.btnSelectStartLink.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSelectStartLink.Name = "btnSelectStartLink";
            this.btnSelectStartLink.Size = new System.Drawing.Size(67, 41);
            this.btnSelectStartLink.TabIndex = 3;
            this.btnSelectStartLink.Text = "Select Start Link";
            this.btnSelectStartLink.Click += new System.EventHandler(this.btnSelectStartLink_Click);
            // 
            // btnPan
            // 
            this.btnPan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPan.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnPan.Location = new System.Drawing.Point(4, 81);
            this.btnPan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPan.Name = "btnPan";
            this.btnPan.Size = new System.Drawing.Size(67, 33);
            this.btnPan.TabIndex = 2;
            this.btnPan.Text = "Pan";
            this.btnPan.Click += new System.EventHandler(this.btnPan_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomOut.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnZoomOut.Location = new System.Drawing.Point(4, 43);
            this.btnZoomOut.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(67, 33);
            this.btnZoomOut.TabIndex = 1;
            this.btnZoomOut.Text = "Zoom Out";
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomIn.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnZoomIn.Location = new System.Drawing.Point(4, 5);
            this.btnZoomIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(67, 33);
            this.btnZoomIn.TabIndex = 0;
            this.btnZoomIn.Text = "Zoom In";
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.ultraLabel1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(550, 20);
            this.panel5.TabIndex = 25;
            // 
            // ultraLabel1
            // 
            appearance2.TextVAlignAsString = "Middle";
            this.ultraLabel1.Appearance = appearance2;
            this.ultraLabel1.Location = new System.Drawing.Point(2, 0);
            this.ultraLabel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(213, 19);
            this.ultraLabel1.TabIndex = 0;
            this.ultraLabel1.Text = "Pipe System Preview";
            // 
            // axMapControl
            // 
            this.axMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl.Location = new System.Drawing.Point(3, 23);
            this.axMapControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.axMapControl.Name = "axMapControl";
            this.axMapControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl.OcxState")));
            this.axMapControl.Size = new System.Drawing.Size(474, 311);
            this.axMapControl.TabIndex = 26;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkZoomToTrace);
            this.panel2.Controls.Add(this.btnRefreshTrace);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnExportLinks);
            this.panel2.Controls.Add(this.grpboxStartLinks);
            this.panel2.Controls.Add(this.grpboxStopLinks);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 334);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(550, 157);
            this.panel2.TabIndex = 27;
            // 
            // chkZoomToTrace
            // 
            this.chkZoomToTrace.Checked = true;
            this.chkZoomToTrace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkZoomToTrace.Location = new System.Drawing.Point(256, 48);
            this.chkZoomToTrace.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkZoomToTrace.Name = "chkZoomToTrace";
            this.chkZoomToTrace.Size = new System.Drawing.Size(214, 16);
            this.chkZoomToTrace.TabIndex = 26;
            this.chkZoomToTrace.Text = "Zoom to Trace";
            // 
            // btnRefreshTrace
            // 
            this.btnRefreshTrace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshTrace.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnRefreshTrace.Location = new System.Drawing.Point(256, 5);
            this.btnRefreshTrace.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRefreshTrace.Name = "btnRefreshTrace";
            this.btnRefreshTrace.Size = new System.Drawing.Size(289, 33);
            this.btnRefreshTrace.TabIndex = 25;
            this.btnRefreshTrace.Text = "Refresh Trace";
            this.btnRefreshTrace.Click += new System.EventHandler(this.btnRefreshTrace_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(256, 114);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(289, 33);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExportLinks
            // 
            this.btnExportLinks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportLinks.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnExportLinks.Location = new System.Drawing.Point(256, 76);
            this.btnExportLinks.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExportLinks.Name = "btnExportLinks";
            this.btnExportLinks.Size = new System.Drawing.Size(289, 33);
            this.btnExportLinks.TabIndex = 23;
            this.btnExportLinks.Text = "Export Trace";
            this.btnExportLinks.Click += new System.EventHandler(this.btnExportLinks_Click);
            // 
            // grpboxStartLinks
            // 
            this.grpboxStartLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpboxStartLinks.Controls.Add(this.lstStartLinks);
            this.grpboxStartLinks.Controls.Add(this.panel3);
            this.grpboxStartLinks.Location = new System.Drawing.Point(2, 5);
            this.grpboxStartLinks.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpboxStartLinks.Name = "grpboxStartLinks";
            this.grpboxStartLinks.Size = new System.Drawing.Size(122, 144);
            this.grpboxStartLinks.TabIndex = 22;
            this.grpboxStartLinks.Text = "Start Links";
            this.grpboxStartLinks.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
            // 
            // lstStartLinks
            // 
            this.lstStartLinks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstStartLinks.Location = new System.Drawing.Point(2, 18);
            this.lstStartLinks.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lstStartLinks.Name = "lstStartLinks";
            this.lstStartLinks.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstStartLinks.Size = new System.Drawing.Size(118, 69);
            this.lstStartLinks.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnRemoveStartLink);
            this.panel3.Controls.Add(this.btnAddStartLink);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(2, 97);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(118, 45);
            this.panel3.TabIndex = 1;
            // 
            // btnRemoveStartLink
            // 
            this.btnRemoveStartLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveStartLink.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnRemoveStartLink.Location = new System.Drawing.Point(54, 7);
            this.btnRemoveStartLink.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRemoveStartLink.Name = "btnRemoveStartLink";
            this.btnRemoveStartLink.Size = new System.Drawing.Size(60, 26);
            this.btnRemoveStartLink.TabIndex = 1;
            this.btnRemoveStartLink.Text = "Remove";
            this.btnRemoveStartLink.Click += new System.EventHandler(this.btnRemoveStartLink_Click);
            // 
            // btnAddStartLink
            // 
            this.btnAddStartLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddStartLink.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnAddStartLink.Location = new System.Drawing.Point(4, 7);
            this.btnAddStartLink.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddStartLink.Name = "btnAddStartLink";
            this.btnAddStartLink.Size = new System.Drawing.Size(46, 26);
            this.btnAddStartLink.TabIndex = 0;
            this.btnAddStartLink.Text = "Add";
            this.btnAddStartLink.Click += new System.EventHandler(this.btnAddStartLink_Click);
            // 
            // grpboxStopLinks
            // 
            this.grpboxStopLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpboxStopLinks.Controls.Add(this.lstStopLinks);
            this.grpboxStopLinks.Controls.Add(this.panel4);
            this.grpboxStopLinks.Location = new System.Drawing.Point(130, 5);
            this.grpboxStopLinks.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpboxStopLinks.Name = "grpboxStopLinks";
            this.grpboxStopLinks.Size = new System.Drawing.Size(122, 144);
            this.grpboxStopLinks.TabIndex = 21;
            this.grpboxStopLinks.Text = "Stop Links";
            this.grpboxStopLinks.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
            // 
            // lstStopLinks
            // 
            this.lstStopLinks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstStopLinks.Location = new System.Drawing.Point(2, 18);
            this.lstStopLinks.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.lstStopLinks.Name = "lstStopLinks";
            this.lstStopLinks.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstStopLinks.Size = new System.Drawing.Size(118, 69);
            this.lstStopLinks.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnRemoveStopLink);
            this.panel4.Controls.Add(this.btnAddStopLink);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(2, 97);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(118, 45);
            this.panel4.TabIndex = 1;
            // 
            // btnRemoveStopLink
            // 
            this.btnRemoveStopLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveStopLink.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnRemoveStopLink.Location = new System.Drawing.Point(52, 7);
            this.btnRemoveStopLink.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRemoveStopLink.Name = "btnRemoveStopLink";
            this.btnRemoveStopLink.Size = new System.Drawing.Size(60, 26);
            this.btnRemoveStopLink.TabIndex = 3;
            this.btnRemoveStopLink.Text = "Remove";
            this.btnRemoveStopLink.Click += new System.EventHandler(this.btnRemoveStopLink_Click);
            // 
            // btnAddStopLink
            // 
            this.btnAddStopLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddStopLink.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
            this.btnAddStopLink.Location = new System.Drawing.Point(2, 7);
            this.btnAddStopLink.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddStopLink.Name = "btnAddStopLink";
            this.btnAddStopLink.Size = new System.Drawing.Size(46, 26);
            this.btnAddStopLink.TabIndex = 2;
            this.btnAddStopLink.Text = "Add";
            this.btnAddStopLink.Click += new System.EventHandler(this.btnAddStopLink_Click);
            // 
            // FrmTraceLinks
            // 
            this.AcceptButton = this.btnExportLinks;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(556, 494);
            this.ControlBox = false;
            this.Controls.Add(this.axMapControl);
            this.Controls.Add(this.pnlMapControls);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmTraceLinks";
            this.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Links Via Trace";
            this.pnlMapControls.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpboxStartLinks)).EndInit();
            this.grpboxStartLinks.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpboxStopLinks)).EndInit();
            this.grpboxStopLinks.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMapControls;
        private Infragistics.Win.Misc.UltraButton btnSelectStopLink;
        private Infragistics.Win.Misc.UltraButton btnSelectStartLink;
        private Infragistics.Win.Misc.UltraButton btnPan;
        private Infragistics.Win.Misc.UltraButton btnZoomOut;
        private Infragistics.Win.Misc.UltraButton btnZoomIn;
        private System.Windows.Forms.Panel panel5;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl;
        private System.Windows.Forms.Panel panel2;
        private Infragistics.Win.Misc.UltraGroupBox grpboxStartLinks;
        private System.Windows.Forms.ListBox lstStartLinks;
        private System.Windows.Forms.Panel panel3;
        private Infragistics.Win.Misc.UltraButton btnRemoveStartLink;
        private Infragistics.Win.Misc.UltraButton btnAddStartLink;
        private Infragistics.Win.Misc.UltraGroupBox grpboxStopLinks;
        private System.Windows.Forms.ListBox lstStopLinks;
        private System.Windows.Forms.Panel panel4;
        private Infragistics.Win.Misc.UltraButton btnRemoveStopLink;
        private Infragistics.Win.Misc.UltraButton btnAddStopLink;
        private Infragistics.Win.Misc.UltraButton btnCancel;
        private Infragistics.Win.Misc.UltraButton btnExportLinks;
        private Infragistics.Win.Misc.UltraButton btnRefreshTrace;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor chkZoomToTrace;
    }
}