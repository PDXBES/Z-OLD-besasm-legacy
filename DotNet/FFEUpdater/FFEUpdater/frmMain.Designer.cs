namespace FFEUpdater
{
    partial class frmMain
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
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem ultraExplorerBarItem1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tabPageMain = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolTipUpdateDSCs = new System.Windows.Forms.ToolTip(this.components);
            this.btnUpdateFFE = new Infragistics.Win.Misc.UltraButton();
            this.lblDescription = new System.Windows.Forms.Label();
            this.expBarMain = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar();
            this.tabControlMain = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.btnCloseFFEUpdater = new Infragistics.Win.Misc.UltraButton();
            this.tabPageMain.SuspendLayout();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.expBarMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlMain)).BeginInit();
            this.tabControlMain.SuspendLayout();
            this.ultraTabSharedControlsPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.btnUpdateFFE);
            this.tabPageMain.Location = new System.Drawing.Point(2, 24);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Size = new System.Drawing.Size(324, 282);
            // 
            // menuStrip
            // 
            this.menuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip.Size = new System.Drawing.Size(555, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 381);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(555, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolTipUpdateDSCs
            // 
            this.toolTipUpdateDSCs.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // btnUpdateFFE
            // 
            this.btnUpdateFFE.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsXPCommandButton;
            this.btnUpdateFFE.Location = new System.Drawing.Point(75, 121);
            this.btnUpdateFFE.Name = "btnUpdateFFE";
            this.btnUpdateFFE.Size = new System.Drawing.Size(175, 41);
            this.btnUpdateFFE.TabIndex = 7;
            this.btnUpdateFFE.Text = "Update FFE Data";
            this.toolTipUpdateDSCs.SetToolTip(this.btnUpdateFFE, "Updates Master DSC data\r\nafter manual data entry\r\n to the FFE database");
            this.btnUpdateFFE.Click += new System.EventHandler(this.btnUpdateFFE_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(12, 43);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(0, 15);
            this.lblDescription.TabIndex = 6;
            // 
            // expBarMain
            // 
            ultraExplorerBarItem1.Key = "RunFFEUpdate";
            appearance1.Image = global::FFEUpdater.Properties.Resources.Buildings;
            ultraExplorerBarItem1.Settings.AppearancesLarge.Appearance = appearance1;
            ultraExplorerBarItem1.Text = "Run FFE Update";
            ultraExplorerBarGroup1.Items.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem[] {
            ultraExplorerBarItem1});
            ultraExplorerBarGroup1.Text = "Update FFE Data";
            this.expBarMain.Groups.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup[] {
            ultraExplorerBarGroup1});
            this.expBarMain.GroupSettings.Style = Infragistics.Win.UltraWinExplorerBar.GroupStyle.LargeImagesWithText;
            this.expBarMain.ItemSettings.Style = Infragistics.Win.UltraWinExplorerBar.ItemStyle.Button;
            this.expBarMain.Location = new System.Drawing.Point(0, 27);
            this.expBarMain.Name = "expBarMain";
            this.expBarMain.Size = new System.Drawing.Size(223, 351);
            this.expBarMain.Style = Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarStyle.VisualStudio2005Toolbox;
            this.expBarMain.TabIndex = 8;
            this.expBarMain.ItemClick += new Infragistics.Win.UltraWinExplorerBar.ItemClickEventHandler(this.expBarMain_ItemClick);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.ultraTabSharedControlsPage1);
            this.tabControlMain.Controls.Add(this.tabPageMain);
            this.tabControlMain.Location = new System.Drawing.Point(227, 27);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SharedControls.AddRange(new System.Windows.Forms.Control[] {
            this.btnUpdateFFE});
            this.tabControlMain.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.tabControlMain.Size = new System.Drawing.Size(328, 308);
            this.tabControlMain.TabIndex = 9;
            ultraTab1.TabPage = this.tabPageMain;
            ultraTab1.Text = "Run FFE Update";
            this.tabControlMain.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1});
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Controls.Add(this.btnUpdateFFE);
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(324, 282);
            // 
            // btnCloseFFEUpdater
            // 
            this.btnCloseFFEUpdater.ButtonStyle = Infragistics.Win.UIElementButtonStyle.WindowsVistaButton;
            this.btnCloseFFEUpdater.Location = new System.Drawing.Point(413, 339);
            this.btnCloseFFEUpdater.Name = "btnCloseFFEUpdater";
            this.btnCloseFFEUpdater.Size = new System.Drawing.Size(140, 39);
            this.btnCloseFFEUpdater.TabIndex = 11;
            this.btnCloseFFEUpdater.Text = "Close FFE Updater";
            this.btnCloseFFEUpdater.Click += new System.EventHandler(this.btnCloseFFEUpdater_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 403);
            this.Controls.Add(this.btnCloseFFEUpdater);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.expBarMain);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "frmMain";
            this.Text = "FFE Updater";
            this.toolTipUpdateDSCs.SetToolTip(this, "Updates FloodRefElev in\r\nmst_DSC_acusing finish floor \r\nelevations (FFE\'s) from \r" +
                    "\nsurvey data");
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabPageMain.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.expBarMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlMain)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.ultraTabSharedControlsPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolTip toolTipUpdateDSCs;
        private System.Windows.Forms.Label lblDescription;
        private Infragistics.Win.Misc.UltraButton btnUpdateFFE;
        private Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar expBarMain;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl tabControlMain;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabPageMain;
        private Infragistics.Win.Misc.UltraButton btnCloseFFEUpdater;
    }
}

