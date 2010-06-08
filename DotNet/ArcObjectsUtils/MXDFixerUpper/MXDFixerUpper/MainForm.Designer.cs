namespace SystemsAnalysis.GIS.SDETools
    {
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( MainForm ) );
            this.menuStrip1 = new System.Windows.Forms.MenuStrip ();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.toolExit = new System.Windows.Forms.ToolStripMenuItem ();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.sDEConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.mxdFixerUpperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip ();
            this.mainFormStatusLabel = new System.Windows.Forms.ToolStripStatusLabel ();
            this.menuStrip1.SuspendLayout ();
            this.statusStrip1.SuspendLayout ();
            this.SuspendLayout ();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange ( new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.windowsToolStripMenuItem,
            this.helpToolStripMenuItem} );
            this.menuStrip1.Location = new System.Drawing.Point ( 0, 0 );
            this.menuStrip1.MdiWindowListItem = this.windowsToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size ( 792, 24 );
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange ( new System.Windows.Forms.ToolStripItem[] {
            this.toolExit} );
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size ( 35, 20 );
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolExit
            // 
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size ( 160, 22 );
            this.toolExit.Text = "E&xit      Alt + F4";
            this.toolExit.Click += new System.EventHandler ( this.toolExit_Click );
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange ( new System.Windows.Forms.ToolStripItem[] {
            this.sDEConnectionsToolStripMenuItem,
            this.mxdFixerUpperToolStripMenuItem} );
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size ( 41, 20 );
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // sDEConnectionsToolStripMenuItem
            // 
            this.sDEConnectionsToolStripMenuItem.Name = "sDEConnectionsToolStripMenuItem";
            this.sDEConnectionsToolStripMenuItem.Size = new System.Drawing.Size ( 166, 22 );
            this.sDEConnectionsToolStripMenuItem.Text = "SDE Connections";
            this.sDEConnectionsToolStripMenuItem.Click += new System.EventHandler ( this.sDEConnectionsToolStripMenuItem_Click );
            // 
            // mxdFixerUpperToolStripMenuItem
            // 
            this.mxdFixerUpperToolStripMenuItem.Name = "mxdFixerUpperToolStripMenuItem";
            this.mxdFixerUpperToolStripMenuItem.Size = new System.Drawing.Size ( 166, 22 );
            this.mxdFixerUpperToolStripMenuItem.Text = "MXD Fixer Upper";
            this.mxdFixerUpperToolStripMenuItem.Click += new System.EventHandler ( this.mxdFixerUpperToolStripMenuItem_Click );
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size ( 44, 20 );
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size ( 62, 20 );
            this.windowsToolStripMenuItem.Text = "Windows";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size ( 40, 20 );
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange ( new System.Windows.Forms.ToolStripItem[] {
            this.mainFormStatusLabel} );
            this.statusStrip1.Location = new System.Drawing.Point ( 0, 364 );
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size ( 792, 22 );
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mainFormStatusLabel
            // 
            this.mainFormStatusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.mainFormStatusLabel.Name = "mainFormStatusLabel";
            this.mainFormStatusLabel.Size = new System.Drawing.Size ( 777, 17 );
            this.mainFormStatusLabel.Spring = true;
            this.mainFormStatusLabel.Text = "SDE Utilities";
            this.mainFormStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGreen;
            this.ClientSize = new System.Drawing.Size ( 792, 386 );
            this.Controls.Add ( this.statusStrip1 );
            this.Controls.Add ( this.menuStrip1 );
            this.Icon = ( ( System.Drawing.Icon ) ( resources.GetObject ( "$this.Icon" ) ) );
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SDE Utilities";
            this.Activated += new System.EventHandler ( this.MainForm_Activated );
            this.menuStrip1.ResumeLayout ( false );
            this.menuStrip1.PerformLayout ();
            this.statusStrip1.ResumeLayout ( false );
            this.statusStrip1.PerformLayout ();
            this.ResumeLayout ( false );
            this.PerformLayout ();

            }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolExit;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sDEConnectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mxdFixerUpperToolStripMenuItem;
        public System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel mainFormStatusLabel;
        }
    }