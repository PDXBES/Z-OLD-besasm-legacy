using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StormwaterInterface
{
    public partial class FormStormwaterInterface : Form
    {
        public FormStormwaterInterface()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'fieldWorkDBDataSet.EvaluatorPage' table. You can move, or remove it, as needed.
            this.evaluatorPageTableAdapter.Fill(this.fieldWorkDBDataSet.EvaluatorPage);
            // TODO: This line of code loads data into the 'fieldWorkDBDataSet.DitchesCulverts' table. You can move, or remove it, as needed.
            //this.ditchesCulvertsTableAdapter.Fill(this.fieldWorkDBDataSet.DitchesCulverts);
            this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, 1);
            // TODO: This line of code loads data into the 'fieldWorkDBDataSet.View_CulvertsDitches' table. You can move, or remove it, as needed.
            //this.view_CulvertsDitchesTableAdapter.Fill(this.fieldWorkDBDataSet.View_CulvertsDitches);
            this.dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonUpdateDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable dt = this.fieldWorkDBDataSet.Tables["DitchesCulverts"];
                //this.fieldWorkDBDataSet.EvaluatorPage.AcceptChanges();
                int place = Int32.Parse(bindingNavigator1.PositionItem.Text) - 1;
                bindingNavigator1.BindingSource.MoveFirst();
                this.BindingContext[this.fieldWorkDBDataSet.EvaluatorPage].EndCurrentEdit();
                this.evaluatorPageTableAdapter.Update(this.fieldWorkDBDataSet.EvaluatorPage);
                this.ditchesCulvertsTableAdapter.Update(this.fieldWorkDBDataSet.DitchesCulverts);
                bindingNavigator1.BindingSource.Position = place;
                bindingNavigator1.Refresh();
                //this.dataGridView1.BindingContext[dt].EndCurrentEdit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("\nInput in improper format!  Errors as follows: \n\n\n" + ex.ToString());
            }
        }

        private void ditchesCulvertsBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            //this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(bindingNavigator1.PositionItem.Text));
            this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(textBoxPageID.Text));
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            //this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(bindingNavigator1.PositionItem.Text));
            this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(textBoxPageID.Text));
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            //this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(bindingNavigator1.PositionItem.Text));
            this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(textBoxPageID.Text));
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            //this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(bindingNavigator1.PositionItem.Text));
            this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(textBoxPageID.Text));
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if ((this.fieldWorkDBDataSet.DitchesCulverts.Select("EvaluatorPageID = " + textBoxPageID.Text)).Length == 0)
            {
                if (MessageBox.Show("Delete this entire page?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.evaluatorPageBindingSource.RemoveCurrent();
                    this.evaluatorPageTableAdapter.Update(this.fieldWorkDBDataSet.EvaluatorPage);
                    this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(textBoxPageID.Text));
                }
            }
            else { MessageBox.Show("Delete all nodes before deleting the page!"); }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            this.evaluatorPageTableAdapter.Insert("Willamette", "Johnson Creek", null, null, null, null, null, null, null);
            this.evaluatorPageTableAdapter.Update(this.fieldWorkDBDataSet.EvaluatorPage);
            this.evaluatorPageTableAdapter.Fill(this.fieldWorkDBDataSet.EvaluatorPage);
            this.evaluatorPageBindingSource.MoveLast();
            this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(textBoxPageID.Text));
        }

        private void buttonAddANode_Click(object sender, EventArgs e)
        {
            int place = Int32.Parse(bindingNavigator1.PositionItem.Text) - 1;
            try
            {
                //UpdateDatabase code here (just to keep from having to click update before adding a node).
                bindingNavigator1.BindingSource.MoveFirst();
                this.BindingContext[this.fieldWorkDBDataSet.EvaluatorPage].EndCurrentEdit();
                this.evaluatorPageTableAdapter.Update(this.fieldWorkDBDataSet.EvaluatorPage);
                this.ditchesCulvertsTableAdapter.Update(this.fieldWorkDBDataSet.DitchesCulverts);
                bindingNavigator1.BindingSource.Position = place;
                bindingNavigator1.Refresh();

                this.ditchesCulvertsTableAdapter.Insert(null, "NewNode", null, null, null, null, null, null, null, null, null, null, null, Int32.Parse(textBoxPageID.Text));
                this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(textBoxPageID.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("\nInput in improper format!  Errors as follows: \n\n\n" + ex.ToString());
                bindingNavigator1.BindingSource.Position = place;
                bindingNavigator1.Refresh();
                this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(textBoxPageID.Text));
            }
        }

        private void buttonDeleteSelectedNode_Click(object sender, EventArgs e)
        {
            this.ditchesCulvertsBindingSource.RemoveCurrent();
            this.ditchesCulvertsTableAdapter.Update(this.fieldWorkDBDataSet.DitchesCulverts);
            //this.BindingContext[this.fieldWorkDBDataSet.EvaluatorPage, ""].EndCurrentEdit();
            this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(textBoxPageID.Text));
        }
    }
}
