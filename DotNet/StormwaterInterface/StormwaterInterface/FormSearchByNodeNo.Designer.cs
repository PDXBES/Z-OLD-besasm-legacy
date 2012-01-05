namespace StormwaterInterface
{
    partial class FormSearchByNodeNo
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
            this.buttonSeachForThisNode = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonSeachForThisNode
            // 
            this.buttonSeachForThisNode.Location = new System.Drawing.Point(12, 33);
            this.buttonSeachForThisNode.Name = "buttonSeachForThisNode";
            this.buttonSeachForThisNode.Size = new System.Drawing.Size(187, 22);
            this.buttonSeachForThisNode.TabIndex = 1;
            this.buttonSeachForThisNode.Text = "Search for this node";
            this.buttonSeachForThisNode.UseVisualStyleBackColor = true;
            this.buttonSeachForThisNode.Click += new System.EventHandler(this.buttonSeachForThisNode_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(187, 20);
            this.textBox1.TabIndex = 2;
            // 
            // FormSearchByNodeNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(211, 58);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonSeachForThisNode);
            this.Name = "FormSearchByNodeNo";
            this.Text = "Search By Node No";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSeachForThisNode;
        private System.Windows.Forms.TextBox textBox1;
    }
}