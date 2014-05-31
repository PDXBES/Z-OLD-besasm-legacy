namespace SWI_2
{
    partial class FormAddNewView
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
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownNewViewNumber = new System.Windows.Forms.NumericUpDown();
            this.buttonAddView = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNewViewNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "New View Number:";
            // 
            // numericUpDownNewViewNumber
            // 
            this.numericUpDownNewViewNumber.Location = new System.Drawing.Point(133, 5);
            this.numericUpDownNewViewNumber.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownNewViewNumber.Name = "numericUpDownNewViewNumber";
            this.numericUpDownNewViewNumber.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownNewViewNumber.TabIndex = 1;
            // 
            // buttonAddView
            // 
            this.buttonAddView.Location = new System.Drawing.Point(200, 49);
            this.buttonAddView.Name = "buttonAddView";
            this.buttonAddView.Size = new System.Drawing.Size(126, 23);
            this.buttonAddView.TabIndex = 2;
            this.buttonAddView.Text = "Add View";
            this.buttonAddView.UseVisualStyleBackColor = true;
            this.buttonAddView.Click += new System.EventHandler(this.buttonAddView_Click);
            // 
            // FormAddNewView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 84);
            this.Controls.Add(this.buttonAddView);
            this.Controls.Add(this.numericUpDownNewViewNumber);
            this.Controls.Add(this.label1);
            this.Name = "FormAddNewView";
            this.Text = "Add New View";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNewViewNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownNewViewNumber;
        private System.Windows.Forms.Button buttonAddView;
    }
}