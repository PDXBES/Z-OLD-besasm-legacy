using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CA5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet11.ForceMains' table. You can move, or remove it, as needed.
            this.forceMainsTableAdapter.Fill(this.dataSet11.ForceMains);
            // TODO: This line of code loads data into the 'dataSet11.Wetwells' table. You can move, or remove it, as needed.
            this.wetwellsTableAdapter1.Fill(this.dataSet11.Wetwells);
            // TODO: This line of code loads data into the 'dataSet11.PumpSchedule' table. You can move, or remove it, as needed.
            this.pumpScheduleTableAdapter.Fill(this.dataSet11.PumpSchedule);
            // TODO: This line of code loads data into the 'dataSet11.PumpData' table. You can move, or remove it, as needed.
            this.pumpDataTableAdapter.Fill(this.dataSet11.PumpData);
            // TODO: This line of code loads data into the 'dataSet11.Pumpstations' table. You can move, or remove it, as needed.
            this.pumpstationsTableAdapter1.Fill(this.dataSet11.Pumpstations);
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Validate();
                //this.pUMPSTATIONSBindingSource.EndEdit();
               // this.pUMPSTATIONSTableAdapter.Update(this.pumpStationDBDataSet.PUMPSTATIONS);
                //MessageBox.Show("Update successful");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Update failed: " + ex.ToString());
            }
        }
    }
}