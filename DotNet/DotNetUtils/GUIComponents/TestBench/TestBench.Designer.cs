namespace TestBench
{
    partial class TestBench
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
            this.fileSelector1 = new SystemsAnalysis.Utils.FileSelector.FileSelector();
            this.SuspendLayout();
            // 
            // fileSelector1
            // 
            this.fileSelector1.Description = "";
            this.fileSelector1.DirectoryMode = false;
            this.fileSelector1.Location = new System.Drawing.Point(233, 12);
            this.fileSelector1.MRUCount = 4;
            this.fileSelector1.Name = "fileSelector1";
            this.fileSelector1.Size = new System.Drawing.Size(589, 52);
            this.fileSelector1.TabIndex = 0;
            //this.fileSelector1.XmlFile = "";
            // 
            // TestBench
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 77);
            this.Controls.Add(this.fileSelector1);
            this.Name = "TestBench";
            this.Text = "TestBench";
            this.ResumeLayout(false);

        }

        #endregion

        private SystemsAnalysis.Utils.FileSelector.FileSelector fileSelector1;
    }
}