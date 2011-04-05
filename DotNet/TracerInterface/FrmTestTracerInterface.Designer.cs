namespace SystemsAnalysis.Tracer
{
    partial class FrmTestTracerInterface
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
            this.btnTraceLinks = new Infragistics.Win.Misc.UltraButton();
            this.SuspendLayout();
            // 
            // btnTraceLinks
            // 
            this.btnTraceLinks.Location = new System.Drawing.Point(83, 202);
            this.btnTraceLinks.Name = "btnTraceLinks";
            this.btnTraceLinks.Size = new System.Drawing.Size(138, 23);
            this.btnTraceLinks.TabIndex = 0;
            this.btnTraceLinks.Text = "Trace Links";
            this.btnTraceLinks.Click += new System.EventHandler(this.btnTraceLinks_Click);
            // 
            // FrmTestTracerInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.btnTraceLinks);
            this.Name = "FrmTestTracerInterface";
            this.Text = "FrmTestTracerInterface";
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraButton btnTraceLinks;
    }
}