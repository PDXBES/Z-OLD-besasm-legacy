using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
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
            int place = Int32.Parse(bindingNavigator1.PositionItem.Text) - 1;
            try
            {
                //DataTable dt = this.fieldWorkDBDataSet.Tables["DitchesCulverts"];
                
                bindingNavigator1.BindingSource.MoveFirst();
                bindingNavigator1.Refresh();
                
                this.evaluatorPageTableAdapter.Update(this.fieldWorkDBDataSet.EvaluatorPage);
                this.ditchesCulvertsTableAdapter.Update(this.fieldWorkDBDataSet.DitchesCulverts);
                bindingNavigator1.BindingSource.Position = place;
                bindingNavigator1.Refresh();
                //this.dataGridView1.BindingContext[dt].EndCurrentEdit();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("\nInput in improper format!  Errors as follows: \n\n\n" + ex.ToString());
                bindingNavigator1.BindingSource.Position = place;
                bindingNavigator1.Refresh();
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
            bindingNavigator1.BindingSource.MoveFirst();
            bindingNavigator1.Refresh();

            this.evaluatorPageTableAdapter.Update(this.fieldWorkDBDataSet.EvaluatorPage);
            this.ditchesCulvertsTableAdapter.Update(this.fieldWorkDBDataSet.DitchesCulverts);
            bindingNavigator1.Refresh();



            this.evaluatorPageTableAdapter.Insert("Willamette", "Stephens Creek", null, null, null, null, null, null, null);
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
                //bindingNavigator1.BindingSource.MoveFirst();
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
            try
            {
                this.ditchesCulvertsBindingSource.RemoveCurrent();
                this.ditchesCulvertsTableAdapter.Update(this.fieldWorkDBDataSet.DitchesCulverts);
                //this.BindingContext[this.fieldWorkDBDataSet.EvaluatorPage, ""].EndCurrentEdit();
                this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(textBoxPageID.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot delete selected node!");
            }
        }

        /*private void buttonSearchByNodeNo_Click(object sender, EventArgs e)
        {
            int place = Int32.Parse(bindingNavigator1.PositionItem.Text) - 1;
            try
            {
                //bindingNavigator1.BindingSource.MoveFirst();
                this.BindingContext[this.fieldWorkDBDataSet.EvaluatorPage].EndCurrentEdit();
                this.evaluatorPageTableAdapter.Update(this.fieldWorkDBDataSet.EvaluatorPage);
                this.ditchesCulvertsTableAdapter.Update(this.fieldWorkDBDataSet.DitchesCulverts);
                //.Position = (int)this.ditchesCulvertsTableAdapter.ScalarQuery(textBoxSearchByNodeNo.Text);
                bindingNavigator1.BindingSource.Position = bindingNavigator1.BindingSource.Find("EvaluatorPageID", (int)this.ditchesCulvertsTableAdapter.ScalarQuery(textBoxSearchByNodeNo.Text));
                bindingNavigator1.Refresh();

                this.ditchesCulvertsTableAdapter.FillByNodeNo(this.fieldWorkDBDataSet.DitchesCulverts, textBoxSearchByNodeNo.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("\nNode not found!  Errors as follows: \n\n\n" + ex.ToString());
                bindingNavigator1.BindingSource.Position = place;
                bindingNavigator1.Refresh();
                this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(textBoxPageID.Text));
            }
        }*/

        /*private void buttonExportComparativeList_Click(object sender, EventArgs e)
        {
            FieldWorkDBDataSetTableAdapters._VSI_ComparableMStLinkCulvertsTransferableTableAdapter theTransferableCulvertsTableAdapter = 
                new StormwaterInterface.FieldWorkDBDataSetTableAdapters._VSI_ComparableMStLinkCulvertsTransferableTableAdapter();
            FieldWorkDBDataSet._VSI_ComparableMStLinkCulvertsTransferableDataTable theTransferableCulvertsTable =
                new FieldWorkDBDataSet._VSI_ComparableMStLinkCulvertsTransferableDataTable();

            
            theTransferableCulvertsTableAdapter.Fill(theTransferableCulvertsTable);
            //theTransferableCulvertsTable.WriteXmlSchema(@"C:\\theTextSchema.xml");
            theTransferableCulvertsTable.WriteXml(@"C:\\theText.xml", XmlWriteMode.WriteSchema);
        }*/

        void bindingNavigatorPositionItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    //using the number entered by the user, fill the 
                    bindingNavigator1.BindingSource.Position = Int32.Parse(bindingNavigatorPositionItem.Text) - 1;
                    bindingNavigator1.Refresh();
                    this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(textBoxPageID.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("\nPage not found!  Errors as follows: \n\n\n" + ex.ToString());
                    bindingNavigator1.BindingSource.Position = 1;
                    bindingNavigator1.Refresh();
                    this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(textBoxPageID.Text));
                }
            }
        }


        private void ToolStripMenuItemExportComparativeList_Click(object sender, EventArgs e)
        {
            //Get the location that the user wants to put the output file
            // Create new SaveFileDialog object
            SaveFileDialog DialogSave = new SaveFileDialog();
            DialogSave.DefaultExt = "csv";
            DialogSave.Filter = "Text file (*.csv)|*.csv|XML file (*.xml)|*.xml|All files (*.*)|*.*";
            DialogSave.AddExtension = true;
            DialogSave.RestoreDirectory = true;
            DialogSave.Title = "Where do you want to save the file?";
            DialogSave.InitialDirectory = @"C:/";
            if (DialogSave.ShowDialog() == DialogResult.OK)
            {
                //////////////////
                //////////////////
                //////////////////Transferable Culverts
                FieldWorkDBDataSetTableAdapters._VSI_ComparableMStLinkCulvertsTransferableTableAdapter theTransferableCulvertsTableAdapter =
                    new StormwaterInterface.FieldWorkDBDataSetTableAdapters._VSI_ComparableMStLinkCulvertsTransferableTableAdapter();
                FieldWorkDBDataSet._VSI_ComparableMStLinkCulvertsTransferableDataTable theTransferableCulvertsTable =
                    new FieldWorkDBDataSet._VSI_ComparableMStLinkCulvertsTransferableDataTable();

                theTransferableCulvertsTableAdapter.Fill(theTransferableCulvertsTable);
                //Pass the file creation method the name of the file (let the user select this), the tables to be transferred, and a description per table.
                CreateCSVFile(theTransferableCulvertsTable, DialogSave.FileName, "Transferable Culverts");
                //////////////////
                //////////////////
                //////////////////Transferable Ditches
                FieldWorkDBDataSetTableAdapters._VSI_ComparableMstLinkDitchesTransferableTableAdapter theTransferableDitchesTableAdapter =
                    new StormwaterInterface.FieldWorkDBDataSetTableAdapters._VSI_ComparableMstLinkDitchesTransferableTableAdapter();
                FieldWorkDBDataSet._VSI_ComparableMstLinkDitchesTransferableDataTable theTransferableDitchesTable =
                    new FieldWorkDBDataSet._VSI_ComparableMstLinkDitchesTransferableDataTable();

                theTransferableDitchesTableAdapter.Fill(theTransferableDitchesTable);
                //Pass the file creation method the name of the file (let the user select this), the tables to be transferred, and a description per table.
                CreateCSVFile(theTransferableDitchesTable, DialogSave.FileName, "Transferable Ditches");
                //////////////////
                //////////////////
                //////////////////Culverts with new depth different from the old depth by more than 6 inches
                FieldWorkDBDataSetTableAdapters._VSI_ComparableMstLinkCulvertsNewDepthDeltaOldDepthGT6TableAdapter theCulvertsNewDepthDeltaOldDepthGT6TableAdapter =
                    new StormwaterInterface.FieldWorkDBDataSetTableAdapters._VSI_ComparableMstLinkCulvertsNewDepthDeltaOldDepthGT6TableAdapter();
                FieldWorkDBDataSet._VSI_ComparableMstLinkCulvertsNewDepthDeltaOldDepthGT6DataTable theCulvertsNewDepthDeltaOldDepthGT6Table =
                    new FieldWorkDBDataSet._VSI_ComparableMstLinkCulvertsNewDepthDeltaOldDepthGT6DataTable();

                theCulvertsNewDepthDeltaOldDepthGT6TableAdapter.Fill(theCulvertsNewDepthDeltaOldDepthGT6Table);
                //Pass the file creation method the name of the file (let the user select this), the tables to be transferred, and a description per table.
                CreateCSVFile(theCulvertsNewDepthDeltaOldDepthGT6Table, DialogSave.FileName, "Culverts with new depth different from the old depth by more than 6 inches");
                //////////////////
                //////////////////
                //////////////////Culverts with new diameter not standard
                FieldWorkDBDataSetTableAdapters._VSI_ComparableMstLinkCulvertsNewDiameterNotStandardTableAdapter theCulvertsNewDiameterNotStandardTableAdapter =
                    new StormwaterInterface.FieldWorkDBDataSetTableAdapters._VSI_ComparableMstLinkCulvertsNewDiameterNotStandardTableAdapter();
                FieldWorkDBDataSet._VSI_ComparableMstLinkCulvertsNewDiameterNotStandardDataTable theCulvertsNewDiameterNotStandardTable =
                    new FieldWorkDBDataSet._VSI_ComparableMstLinkCulvertsNewDiameterNotStandardDataTable();

                theCulvertsNewDiameterNotStandardTableAdapter.Fill(theCulvertsNewDiameterNotStandardTable);
                //Pass the file creation method the name of the file (let the user select this), the tables to be transferred, and a description per table.
                CreateCSVFile(theCulvertsNewDiameterNotStandardTable, DialogSave.FileName, "Culverts with new diameter not standard");
                //////////////////
                //////////////////
                //////////////////Culverts that cannot be uniquely identified in mst_links
                FieldWorkDBDataSetTableAdapters._VSI_DifficultCulvertsTableAdapter theDifficultCulvertsTableAdapter =
                    new StormwaterInterface.FieldWorkDBDataSetTableAdapters._VSI_DifficultCulvertsTableAdapter();
                FieldWorkDBDataSet._VSI_DifficultCulvertsDataTable theDifficultCulvertsTable =
                    new FieldWorkDBDataSet._VSI_DifficultCulvertsDataTable();

                theDifficultCulvertsTableAdapter.Fill(theDifficultCulvertsTable);
                //Pass the file creation method the name of the file (let the user select this), the tables to be transferred, and a description per table.
                CreateCSVFile(theDifficultCulvertsTable, DialogSave.FileName, "Culverts that cannot be uniquely identified in mst_links");
                //////////////////
                //////////////////
                //////////////////Ditches with new depth less than one
                FieldWorkDBDataSetTableAdapters._VSI_ComparableMstLinkDitchesNewDepthLT1TableAdapter theDitchesNewDepthLT1TableAdapter =
                    new StormwaterInterface.FieldWorkDBDataSetTableAdapters._VSI_ComparableMstLinkDitchesNewDepthLT1TableAdapter();
                FieldWorkDBDataSet._VSI_ComparableMstLinkDitchesNewDepthLT1DataTable theDitchesNewDepthLT1Table =
                    new FieldWorkDBDataSet._VSI_ComparableMstLinkDitchesNewDepthLT1DataTable();

                theDitchesNewDepthLT1TableAdapter.Fill(theDitchesNewDepthLT1Table);
                //Pass the file creation method the name of the file (let the user select this), the tables to be transferred, and a description per table.
                CreateCSVFile(theDitchesNewDepthLT1Table, DialogSave.FileName, "Ditches with new depth less than one");
                //////////////////
                //////////////////
                //////////////////Ditches with no downstream node entry
                FieldWorkDBDataSetTableAdapters._VSI_ComparableMstLinkDItchesNoDSNodeTableAdapter theDItchesNoDSNodeTableAdapter =
                    new StormwaterInterface.FieldWorkDBDataSetTableAdapters._VSI_ComparableMstLinkDItchesNoDSNodeTableAdapter();
                FieldWorkDBDataSet._VSI_ComparableMstLinkDItchesNoDSNodeDataTable theDItchesNoDSNodeTable =
                    new FieldWorkDBDataSet._VSI_ComparableMstLinkDItchesNoDSNodeDataTable();

                theDItchesNoDSNodeTableAdapter.Fill(theDItchesNoDSNodeTable);
                //Pass the file creation method the name of the file (let the user select this), the tables to be transferred, and a description per table.
                CreateCSVFile(theDItchesNoDSNodeTable, DialogSave.FileName, "Ditches with no downstream node entry");
                //////////////////
                //////////////////
                //////////////////Ditches with no downstream node entry
                FieldWorkDBDataSetTableAdapters._VSI_ComparableMstLinkDitchesNoUSNodeTableAdapter theDItchesNoUSNodeTableAdapter =
                    new StormwaterInterface.FieldWorkDBDataSetTableAdapters._VSI_ComparableMstLinkDitchesNoUSNodeTableAdapter();
                FieldWorkDBDataSet._VSI_ComparableMstLinkDitchesNoUSNodeDataTable theDItchesNoUSNodeTable =
                    new FieldWorkDBDataSet._VSI_ComparableMstLinkDitchesNoUSNodeDataTable();

                theDItchesNoUSNodeTableAdapter.Fill(theDItchesNoUSNodeTable);
                //Pass the file creation method the name of the file (let the user select this), the tables to be transferred, and a description per table.
                CreateCSVFile(theDItchesNoUSNodeTable, DialogSave.FileName, "Ditches with no upstream node entry");
                //////////////////
                //////////////////
                //////////////////Ditches with the top width less than the bottom width
                FieldWorkDBDataSetTableAdapters._VSI_ComparableMstLinkDitchesWidthTopLTBottomTableAdapter theDitchesWidthTopLTBottomTableAdapter =
                    new StormwaterInterface.FieldWorkDBDataSetTableAdapters._VSI_ComparableMstLinkDitchesWidthTopLTBottomTableAdapter();
                FieldWorkDBDataSet._VSI_ComparableMstLinkDitchesWidthTopLTBottomDataTable theDitchesWidthTopLTBottomTable =
                    new FieldWorkDBDataSet._VSI_ComparableMstLinkDitchesWidthTopLTBottomDataTable();

                theDitchesWidthTopLTBottomTableAdapter.Fill(theDitchesWidthTopLTBottomTable);
                //Pass the file creation method the name of the file (let the user select this), the tables to be transferred, and a description per table.
                CreateCSVFile(theDitchesWidthTopLTBottomTable, DialogSave.FileName, "Ditches with the top width less than the bottom width");
                //////////////////
                //////////////////
                //////////////////Ditches that cannot be uniquely identified in mst_links
                FieldWorkDBDataSetTableAdapters._VSI_DifficultDitchesTableAdapter theDifficultDitchesTableAdapter =
                    new StormwaterInterface.FieldWorkDBDataSetTableAdapters._VSI_DifficultDitchesTableAdapter();
                FieldWorkDBDataSet._VSI_DifficultDitchesDataTable theDifficultDitchesTable =
                    new FieldWorkDBDataSet._VSI_DifficultDitchesDataTable();

                theDifficultDitchesTableAdapter.Fill(theDifficultDitchesTable);
                //Pass the file creation method the name of the file (let the user select this), the tables to be transferred, and a description per table.
                CreateCSVFile(theDifficultDitchesTable, DialogSave.FileName, "Ditches that cannot be uniquely identified in mst_links");
            }
            DialogSave.Dispose();
            DialogSave = null;
        }

        public void CreateCSVFile(DataTable dt, string strFilePath, string strTableDescription)
        {

            #region Export Grid to CSV
            //Create the CSV file to which grid data will be exported.
            StreamWriter sw = new StreamWriter(strFilePath, true);
            //write the table description
            sw.Write(strTableDescription);
            sw.Write(sw.NewLine);
            //write the headers.
            int iColCount = dt.Columns.Count;

            for (int i = 0; i < iColCount; i++)
            {
                sw.Write(dt.Columns[i]);
                if (i < iColCount - 1)
                {
                    sw.Write(",");
                }
            }

            sw.Write(sw.NewLine);
            //Now write all the rows.
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString().Replace(",", "`"));
                    }

                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Write(sw.NewLine);

            sw.Close();
            #endregion
        }

        private void ToolStripMenuItemSearchByNodeNo_Click(Object sender, EventArgs e)
        {
            FormSearchByNodeNo child = new FormSearchByNodeNo();
            child.Message = "";
            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;
            string newMessage = child.Message;
            //MessageBox.Show(newMessage);

            int place = Int32.Parse(bindingNavigator1.PositionItem.Text) - 1;
            try
            {
                //bindingNavigator1.BindingSource.MoveFirst();
                this.BindingContext[this.fieldWorkDBDataSet.EvaluatorPage].EndCurrentEdit();
                this.evaluatorPageTableAdapter.Update(this.fieldWorkDBDataSet.EvaluatorPage);
                this.ditchesCulvertsTableAdapter.Update(this.fieldWorkDBDataSet.DitchesCulverts);
                //.Position = (int)this.ditchesCulvertsTableAdapter.ScalarQuery(textBoxSearchByNodeNo.Text);
                bindingNavigator1.BindingSource.Position = bindingNavigator1.BindingSource.Find("EvaluatorPageID", (int)this.ditchesCulvertsTableAdapter.ScalarQuery(newMessage));
                bindingNavigator1.Refresh();

                this.ditchesCulvertsTableAdapter.FillByNodeNo(this.fieldWorkDBDataSet.DitchesCulverts, newMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("\nNode not found!  Errors as follows: \n\n\n" + ex.ToString());
                bindingNavigator1.BindingSource.Position = place;
                bindingNavigator1.Refresh();
                this.ditchesCulvertsTableAdapter.FillByPageID(this.fieldWorkDBDataSet.DitchesCulverts, Int32.Parse(textBoxPageID.Text));
            }
        }
    }
}
