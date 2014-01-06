namespace SystemsAnalysis.GIS.SDETools
    {
    partial class frmSDEconnection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( frmSDEconnection ) );
            this.listSDEConnections = new System.Windows.Forms.ListBox ();
            this.label1 = new System.Windows.Forms.Label ();
            this.btnGo = new System.Windows.Forms.Button ();
            this.btnExit = new System.Windows.Forms.Button ();
            this.listCurrentConnections = new System.Windows.Forms.ListBox ();
            this.statusStripSDE = new System.Windows.Forms.StatusStrip ();
            this.connectionStatus = new System.Windows.Forms.ToolStripStatusLabel ();
            this.labelCurrentConnections = new System.Windows.Forms.Label ();
            this.statusStripSDE.SuspendLayout ();
            this.SuspendLayout ();
            // 
            // listSDEConnections
            // 
            this.listSDEConnections.FormattingEnabled = true;
            this.listSDEConnections.Location = new System.Drawing.Point ( 7, 26 );
            this.listSDEConnections.Name = "listSDEConnections";
            this.listSDEConnections.Size = new System.Drawing.Size ( 300, 147 );
            this.listSDEConnections.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point ( 7, 9 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size ( 300, 14 );
            this.label1.TabIndex = 1;
            this.label1.Text = "Currently Available SDE Connections";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point ( 627, 265 );
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size ( 60, 23 );
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler ( this.btnGo_Click );
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point ( 693, 265 );
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size ( 60, 23 );
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler ( this.btnExit_Click );
            // 
            // listCurrentConnections
            // 
            this.listCurrentConnections.FormattingEnabled = true;
            this.listCurrentConnections.Location = new System.Drawing.Point ( 338, 26 );
            this.listCurrentConnections.Name = "listCurrentConnections";
            this.listCurrentConnections.Size = new System.Drawing.Size ( 300, 147 );
            this.listCurrentConnections.TabIndex = 5;
            // 
            // statusStripSDE
            // 
            this.statusStripSDE.Items.AddRange ( new System.Windows.Forms.ToolStripItem[] {
            this.connectionStatus} );
            this.statusStripSDE.Location = new System.Drawing.Point ( 0, 304 );
            this.statusStripSDE.Name = "statusStripSDE";
            this.statusStripSDE.Size = new System.Drawing.Size ( 772, 22 );
            this.statusStripSDE.TabIndex = 6;
            this.statusStripSDE.Text = "statusStrip1";
            // 
            // connectionStatus
            // 
            this.connectionStatus.BackColor = System.Drawing.SystemColors.Control;
            this.connectionStatus.Name = "connectionStatus";
            this.connectionStatus.Size = new System.Drawing.Size ( 757, 17 );
            this.connectionStatus.Spring = true;
            this.connectionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCurrentConnections
            // 
            this.labelCurrentConnections.Location = new System.Drawing.Point ( 335, 9 );
            this.labelCurrentConnections.Name = "labelCurrentConnections";
            this.labelCurrentConnections.Size = new System.Drawing.Size ( 300, 14 );
            this.labelCurrentConnections.TabIndex = 7;
            this.labelCurrentConnections.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSDEconnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size ( 772, 326 );
            this.Controls.Add ( this.labelCurrentConnections );
            this.Controls.Add ( this.statusStripSDE );
            this.Controls.Add ( this.listCurrentConnections );
            this.Controls.Add ( this.btnExit );
            this.Controls.Add ( this.btnGo );
            this.Controls.Add ( this.label1 );
            this.Controls.Add ( this.listSDEConnections );
            this.Icon = ( ( System.Drawing.Icon ) ( resources.GetObject ( "$this.Icon" ) ) );
            this.Name = "frmSDEconnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SDE Connection Manager";
            this.TopMost = true;
            this.Activated += new System.EventHandler ( this.frmSDEconnection_Activated );
            this.statusStripSDE.ResumeLayout ( false );
            this.statusStripSDE.PerformLayout ();
            this.ResumeLayout ( false );
            this.PerformLayout ();

            }

        #endregion

        private System.Windows.Forms.ListBox listSDEConnections;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ListBox listCurrentConnections;
        private System.Windows.Forms.StatusStrip statusStripSDE;
        private System.Windows.Forms.ToolStripStatusLabel connectionStatus;
        private System.Windows.Forms.Label labelCurrentConnections;
        }
    }