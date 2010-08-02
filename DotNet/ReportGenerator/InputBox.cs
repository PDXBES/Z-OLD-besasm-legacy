using ESRI.ArcGIS.Utility;

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SystemsAnalysis.Reporting
{
/// <summary>
/// A simple input box form. This should be moved to a utilities class.
/// </summary>
public class InputBox : System.Windows.Forms.Form
{
/// <summary>
/// The OK button for this form.
/// </summary>
protected System.Windows.Forms.Button buttonOK;
/// <summary>
/// The Cancel button for this form.
/// </summary>
protected System.Windows.Forms.Button buttonCancel;
/// <summary>
/// The label for this form.
/// </summary>
protected System.Windows.Forms.Label labelPrompt;
/// <summary>
/// The text to be read by this form.
/// </summary>
protected System.Windows.Forms.TextBox textBoxText;
/// <summary>
/// Error provider for this form.
/// </summary>
    protected System.Windows.Forms.ErrorProvider errorProviderText;
    private IContainer components;

/// <summary>
/// Delegate used to validate the object
/// </summary>
private InputBoxValidatingHandler _validator;

private InputBox()
{
//
// Required for Windows Form Designer support
//
InitializeComponent();

//
// TODO: Add any constructor code after InitializeComponent call
//
}

/// <summary>
/// Clean up any resources being used.
/// </summary>
protected override void Dispose( bool disposing )
{
if( disposing )
{
if (components != null) 
{
components.Dispose();
}
}
base.Dispose( disposing );
}

#region Windows Form Designer generated code
/// <summary>
/// Required method for Designer support - do not modify
/// the contents of this method with the code editor.
/// </summary>
private void InitializeComponent()
{
    this.components = new System.ComponentModel.Container();
    this.buttonOK = new System.Windows.Forms.Button();
    this.buttonCancel = new System.Windows.Forms.Button();
    this.textBoxText = new System.Windows.Forms.TextBox();
    this.labelPrompt = new System.Windows.Forms.Label();
    this.errorProviderText = new System.Windows.Forms.ErrorProvider(this.components);
    ((System.ComponentModel.ISupportInitialize)(this.errorProviderText)).BeginInit();
    this.SuspendLayout();
    // 
    // buttonOK
    // 
    this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
    this.buttonOK.Location = new System.Drawing.Point(8, 46);
    this.buttonOK.Name = "buttonOK";
    this.buttonOK.Size = new System.Drawing.Size(90, 27);
    this.buttonOK.TabIndex = 2;
    this.buttonOK.Text = "OK";
    this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
    // 
    // buttonCancel
    // 
    this.buttonCancel.CausesValidation = false;
    this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
    this.buttonCancel.Location = new System.Drawing.Point(104, 46);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new System.Drawing.Size(90, 27);
    this.buttonCancel.TabIndex = 3;
    this.buttonCancel.Text = "Cancel";
    this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
    // 
    // textBoxText
    // 
    this.textBoxText.Location = new System.Drawing.Point(10, 18);
    this.textBoxText.Name = "textBoxText";
    this.textBoxText.Size = new System.Drawing.Size(172, 22);
    this.textBoxText.TabIndex = 1;
    this.textBoxText.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxText_Validating);
    this.textBoxText.TextChanged += new System.EventHandler(this.textBoxText_TextChanged);
    // 
    // labelPrompt
    // 
    this.labelPrompt.AutoSize = true;
    this.labelPrompt.Location = new System.Drawing.Point(10, 0);
    this.labelPrompt.Name = "labelPrompt";
    this.labelPrompt.Size = new System.Drawing.Size(52, 17);
    this.labelPrompt.TabIndex = 0;
    this.labelPrompt.Text = "prompt";
    // 
    // errorProviderText
    // 
    this.errorProviderText.ContainerControl = this;
    this.errorProviderText.DataMember = "";
    // 
    // InputBox
    // 
    this.AcceptButton = this.buttonOK;
    this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
    this.AutoSize = true;
    this.CancelButton = this.buttonCancel;
    this.ClientSize = new System.Drawing.Size(202, 85);
    this.Controls.Add(this.labelPrompt);
    this.Controls.Add(this.textBoxText);
    this.Controls.Add(this.buttonCancel);
    this.Controls.Add(this.buttonOK);
    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = "InputBox";
    this.Text = "Title";
    this.Load += new System.EventHandler(this.InputBox_Load);
    ((System.ComponentModel.ISupportInitialize)(this.errorProviderText)).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();

}
#endregion

private void buttonCancel_Click(object sender, System.EventArgs e) 
{
this.Validator = null;
this.Close();
}

private void buttonOK_Click(object sender, System.EventArgs e) 
{
this.Close();
}

/// <summary>
/// Displays a prompt in a dialog box, waits for the user to input text or click a button.
/// </summary>
/// <param name="prompt">String expression displayed as the message in the dialog box</param>
/// <param name="title">String expression displayed in the title bar of the dialog box</param>
/// <param name="defaultResponse">String expression displayed in the text box as the default response</param>
/// <param name="validator">Delegate used to validate the text</param>
/// <param name="xpos">Numeric expression that specifies the distance of the left edge of the dialog box from the left edge of the screen.</param>
/// <param name="ypos">Numeric expression that specifies the distance of the upper edge of the dialog box from the top of the screen</param>
/// <returns>An InputBoxResult object with the Text and the OK property set to true when OK was clicked.</returns>
public static InputBoxResult Show(string prompt, string title, string defaultResponse, InputBoxValidatingHandler validator, int xpos, int ypos) 
{
using (InputBox form = new InputBox()) 
{
form.labelPrompt.Text = prompt;
form.Text = title;
form.textBoxText.Text = defaultResponse;
if (xpos >= 0 && ypos >= 0) 
{
form.StartPosition = FormStartPosition.Manual;
form.Left = xpos;
form.Top = ypos;
}
form.Validator = validator;

DialogResult result = form.ShowDialog();

InputBoxResult retval = new InputBoxResult();
if (result == DialogResult.OK) 
{
retval.Text = form.textBoxText.Text;
retval.OK = true;
}
return retval;
}
}

/// <summary>
/// Displays a prompt in a dialog box, waits for the user to input text or click a button.
/// </summary>
/// <param name="prompt">String expression displayed as the message in the dialog box</param>
/// <param name="title">String expression displayed in the title bar of the dialog box</param>
/// <param name="defaultText">String expression displayed in the text box as the default response</param>
/// <param name="validator">Delegate used to validate the text</param>
/// <returns>An InputBoxResult object with the Text and the OK property set to true when OK was clicked.</returns>
public static InputBoxResult Show(string prompt, string title, string defaultText, InputBoxValidatingHandler validator) 
{
return Show(prompt, title, defaultText, validator, -1, -1);
}


/// <summary>
/// Reset the ErrorProvider
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void textBoxText_TextChanged(object sender, System.EventArgs e) 
{
errorProviderText.SetError(textBoxText, "");
}

/// <summary>
/// Validate the Text using the Validator
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void textBoxText_Validating(object sender, System.ComponentModel.CancelEventArgs e) 
{
if (Validator != null) 
{
InputBoxValidatingArgs args = new InputBoxValidatingArgs();
args.Text = textBoxText.Text;
Validator(this, args);
if (args.Cancel) 
{
e.Cancel = true;
errorProviderText.SetError(textBoxText, args.Message);
}
}
}

private void InputBox_Load(object sender, System.EventArgs e)
{

}

protected InputBoxValidatingHandler Validator 
{
get 
{
return (this._validator);
}
set 
{
this._validator = value;
}
}
}

/// <summary>
/// Class used to store the result of an InputBox.Show message.
/// </summary>
public class InputBoxResult 
{
public bool OK;
public string Text;
}

/// <summary>
/// EventArgs used to Validate an InputBox
/// </summary>
public class InputBoxValidatingArgs : EventArgs 
{
public string Text;
public string Message;
public bool Cancel;
}

/// <summary>
/// Delegate used to Validate an InputBox
/// </summary>
public delegate void InputBoxValidatingHandler(object sender, InputBoxValidatingArgs e);

}