namespace DataAccessTestBench
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.btnCloseApplication = new Infragistics.Win.Misc.UltraButton();
      this.SuspendLayout();
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(12, 12);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.textBox1.Size = new System.Drawing.Size(312, 315);
      this.textBox1.TabIndex = 0;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(330, 12);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(164, 23);
      this.button1.TabIndex = 1;
      this.button1.Text = "Execute queries directly";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(330, 41);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(164, 23);
      this.button2.TabIndex = 2;
      this.button2.Text = "Use Report Library";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(330, 70);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(164, 23);
      this.button3.TabIndex = 3;
      this.button3.Text = "BSBR Test";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // btnCloseApplication
      // 
      this.btnCloseApplication.ButtonStyle = Infragistics.Win.UIElementButtonStyle.VisualStudio2005Button;
      this.btnCloseApplication.Location = new System.Drawing.Point(451, 304);
      this.btnCloseApplication.Name = "btnCloseApplication";
      this.btnCloseApplication.Size = new System.Drawing.Size(75, 23);
      this.btnCloseApplication.TabIndex = 4;
      this.btnCloseApplication.Text = "Close";
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(538, 339);
      this.Controls.Add(this.btnCloseApplication);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.textBox1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "frmMain";
      this.Text = "Data Access Test Bench";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;
    private Infragistics.Win.Misc.UltraButton btnCloseApplication;

  }
}

