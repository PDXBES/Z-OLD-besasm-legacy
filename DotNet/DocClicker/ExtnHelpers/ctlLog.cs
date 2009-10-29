using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ExtnHelpers
{
    public partial class ctlLog : UserControl
    {
        public ctlLog()
        {
            InitializeComponent();
        }
        #region List box methods and properties
        /// <summary>
        /// Adds a new item to the form's list box
        /// </summary>
        /// <param name="item">Object/string to add to the list box</param>
        public void AddItemToLog(object item)
        {
            this.lbLog.Items.Add(item.ToString());
            lbLog.Refresh();
        }
        

        /// <summary>
        /// Clears all items that are currently in the list box.
        /// </summary>
        public void ClearLog()
        {
            this.lbLog.Items.Clear();
        }

        /// <summary>
        /// Forces a refresh of list box contents whenever list box is resized
        /// </summary>
        /// <param name="sender">Object sending the event</param>
        /// <param name="e">Event arguments</param>
        private void lbLog_Resize(object sender, System.EventArgs e)
        {
            this.lbLog.Refresh();
        }

        /// <summary>
        /// Returns a reference to the list box on the form.
        /// </summary>
        public ListBox LogListBox
        {
            get
            {
                return this.lbLog;
            }
        }
        #endregion

        private void label1_Resize(object sender, EventArgs e)
        {
            this.label1.Refresh();
        }
    }
}
