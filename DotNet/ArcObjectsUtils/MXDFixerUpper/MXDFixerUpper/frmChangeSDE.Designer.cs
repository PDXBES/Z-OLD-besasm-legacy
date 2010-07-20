namespace SystemsAnalysis.GIS.SDETools
    {
    partial class frmChangeSDE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( frmChangeSDE ) );
            this.openMXDdialog = new System.Windows.Forms.OpenFileDialog ();
            this.label1 = new System.Windows.Forms.Label ();
            this.textMXDfile = new System.Windows.Forms.TextBox ();
            this.label2 = new System.Windows.Forms.Label ();
            this.label3 = new System.Windows.Forms.Label ();
            this.listSDEConnections = new System.Windows.Forms.ListBox ();
            this.btnGo = new System.Windows.Forms.Button ();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip ();
            this.connectionStatus = new System.Windows.Forms.ToolStripStatusLabel ();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel ();
            this.listExistingSDE = new System.Windows.Forms.ListBox ();
            this.label4 = new System.Windows.Forms.Label ();
            this.btnOpenMXDdialog = new System.Windows.Forms.Button ();
            this.btnExit = new System.Windows.Forms.Button ();
            this.statusStrip1.SuspendLayout ();
            this.SuspendLayout ();
            // 
            // openMXDdialog
            // 
            this.openMXDdialog.FileName = "openMXDdialog";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point ( 13, 19 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size ( 50, 13 );
            this.label1.TabIndex = 1;
            this.label1.Text = "MXD File";
            // 
            // textMXDfile
            // 
            this.textMXDfile.Location = new System.Drawing.Point ( 70, 12 );
            this.textMXDfile.Name = "textMXDfile";
            this.textMXDfile.Size = new System.Drawing.Size ( 470, 20 );
            this.textMXDfile.TabIndex = 2;
            this.textMXDfile.TextChanged += new System.EventHandler ( this.textMXDfile_TextChanged );
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ) );
            this.label2.Location = new System.Drawing.Point ( 31, 63 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size ( 498, 13 );
            this.label2.TabIndex = 4;
            this.label2.Text = "MXD SDE Connections";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ) );
            this.label3.Location = new System.Drawing.Point ( 534, 63 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size ( 226, 13 );
            this.label3.TabIndex = 6;
            this.label3.Text = "Available SDE Connections";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listSDEConnections
            // 
            this.listSDEConnections.FormattingEnabled = true;
            this.listSDEConnections.Location = new System.Drawing.Point ( 534, 79 );
            this.listSDEConnections.Name = "listSDEConnections";
            this.listSDEConnections.Size = new System.Drawing.Size ( 226, 173 );
            this.listSDEConnections.TabIndex = 5;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point ( 604, 278 );
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size ( 75, 23 );
            this.btnGo.TabIndex = 7;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler ( this.btnGo_Click );
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange ( new System.Windows.Forms.ToolStripItem[] {
            this.connectionStatus} );
            this.statusStrip1.Location = new System.Drawing.Point ( 0, 304 );
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size ( 772, 22 );
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // connectionStatus
            // 
            this.connectionStatus.BackColor = System.Drawing.SystemColors.Control;
            this.connectionStatus.Name = "connectionStatus";
            this.connectionStatus.Size = new System.Drawing.Size ( 0, 17 );
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size ( 688, 17 );
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listExistingSDE
            // 
            this.listExistingSDE.FormattingEnabled = true;
            this.listExistingSDE.HorizontalScrollbar = true;
            this.listExistingSDE.Location = new System.Drawing.Point ( 13, 79 );
            this.listExistingSDE.Name = "listExistingSDE";
            this.listExistingSDE.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listExistingSDE.Size = new System.Drawing.Size ( 515, 173 );
            this.listExistingSDE.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font ( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ) );
            this.label4.Location = new System.Drawing.Point ( 12, 264 );
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size ( 165, 13 );
            this.label4.TabIndex = 12;
            this.label4.Text = "Use CTRL to select multiple items";
            // 
            // btnOpenMXDdialog
            // 
            this.btnOpenMXDdialog.Image = ( ( System.Drawing.Image ) ( resources.GetObject ( "btnOpenMXDdialog.Image" ) ) );
            this.btnOpenMXDdialog.Location = new System.Drawing.Point ( 546, 10 );
            this.btnOpenMXDdialog.Name = "btnOpenMXDdialog";
            this.btnOpenMXDdialog.Size = new System.Drawing.Size ( 23, 23 );
            this.btnOpenMXDdialog.TabIndex = 0;
            this.btnOpenMXDdialog.UseVisualStyleBackColor = true;
            this.btnOpenMXDdialog.Click += new System.EventHandler ( this.btnOpenMXDdialog_Click );
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point ( 685, 278 );
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size ( 75, 23 );
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler ( this.btnExit_Click );
            // 
            // frmChangeSDE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MintCream;
            this.ClientSize = new System.Drawing.Size ( 772, 326 );
            this.Controls.Add ( this.btnExit );
            this.Controls.Add ( this.label4 );
            this.Controls.Add ( this.listExistingSDE );
            this.Controls.Add ( this.statusStrip1 );
            this.Controls.Add ( this.btnGo );
            this.Controls.Add ( this.label3 );
            this.Controls.Add ( this.listSDEConnections );
            this.Controls.Add ( this.label2 );
            this.Controls.Add ( this.textMXDfile );
            this.Controls.Add ( this.label1 );
            this.Controls.Add ( this.btnOpenMXDdialog );
            this.Icon = ( ( System.Drawing.Icon ) ( resources.GetObject ( "$this.Icon" ) ) );
            this.Name = "frmChangeSDE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change MXD Connection Properties";
            this.TopMost = true;
            this.Activated += new System.EventHandler ( this.frmChangeSDE_Activated );
            this.statusStrip1.ResumeLayout ( false );
            this.statusStrip1.PerformLayout ();
            this.ResumeLayout ( false );
            this.PerformLayout ();

            }

        #endregion

        private System.Windows.Forms.OpenFileDialog openMXDdialog;
        private System.Windows.Forms.Button btnOpenMXDdialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textMXDfile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listSDEConnections;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listExistingSDE;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ToolStripStatusLabel connectionStatus;
        }
    }

