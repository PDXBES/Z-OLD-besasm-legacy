using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using SystemsAnalysis.Utils.AccessUtils;
using SystemsAnalysis.Utils.DataMobility;

namespace SWI_2
{
    public partial class FormMain : Form
    {
        private int _CurrentSurveyPage;
        private int _CurrentView;
        private int _lastGlobalID;
        private int _CurrentWatershed;
        private int _CurrentSubwatershed;
        private string _SelectedWatershed;
        private string _SelectedSubwatershed;
        private enum _lastSearchVar { ditch, culvert, pipe };
        private _lastSearchVar _lastSearch;
        private bool _returningFromForm;
        private bool _UserClickedCancel;

        public FormMain()
        {
            InitializeComponent();
        }

        public int CurrentSurveyPage
        {
            get { return _CurrentSurveyPage; }
            set { _CurrentSurveyPage = value; }
        }

        public int CurrentView
        {
            get { return _CurrentView; }
            set { _CurrentView = value; }
        }

        public int CurrentWatershed
        {
            get { return _CurrentWatershed; }
            set { _CurrentWatershed = value; }
        }

        public int CurrentSubwatershed
        {
            get { return _CurrentSubwatershed; }
            set { _CurrentSubwatershed = value; }
        }

        public string SelectedSubwatershed
        {
            get { return _SelectedSubwatershed; }
            set { _SelectedSubwatershed = value; }
        }

        public string SelectedWatershed
        {
            get { return _SelectedWatershed; }
            set { _SelectedWatershed = value; }
        }

        public bool UserClickedCancel
        {
            get { return _UserClickedCancel; }
            set { _UserClickedCancel = value; }
        }
   
      
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_GLOBAL_ID' table. You can move, or remove it, as needed.
            this.sWSP_GLOBAL_IDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_GLOBAL_ID);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_PIPE' table. You can move, or remove it, as needed.
            this.sWSP_PIPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_PIPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SHAPE_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_SHAPE_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SHAPE_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_CULVERT' table. You can move, or remove it, as needed.
            this.sWSP_CULVERTTableAdapter.Fill(this.sANDBOXDataSet.SWSP_CULVERT);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_PHOTO' table. You can move, or remove it, as needed.
            this.sWSP_PHOTOTableAdapter.Fill(this.sANDBOXDataSet.SWSP_PHOTO);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_MATERIAL_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_MATERIAL_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_MATERIAL_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_FACING_TYPE' table. You can move, or remove it, as needed.
            this.sWSP_FACING_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_FACING_TYPE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_DITCH' table. You can move, or remove it, as needed.
            this.sWSP_DITCHTableAdapter.Fill(this.sANDBOXDataSet.SWSP_DITCH);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SURVEY_PAGE_EVALUATOR' table. You can move, or remove it, as needed.
            this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE_EVALUATOR);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_EVALUATOR' table. You can move, or remove it, as needed.
            this.sWSP_EVALUATORTableAdapter.Fill(this.sANDBOXDataSet.SWSP_EVALUATOR);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SURVEY_PAGE' table. You can move, or remove it, as needed.
            this.sWSP_SURVEY_PAGETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_VIEW' table. You can move, or remove it, as needed.
            this.sWSP_VIEWTableAdapter.Fill(this.sANDBOXDataSet.SWSP_VIEW);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_SUBWATERSHED' table. You can move, or remove it, as needed.
            this.sWSP_SUBWATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_SUBWATERSHED);
            // TODO: This line of code loads data into the 'sANDBOXDataSet.SWSP_WATERSHED' table. You can move, or remove it, as needed.
            this.sWSP_WATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_WATERSHED);

            _returningFromForm = false;
            _lastGlobalID = 0;
            _lastSearch = _lastSearchVar.culvert; 
        }



        private void surveyViewToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            FormFieldSurveyView child = new FormFieldSurveyView();

            _returningFromForm = true;
            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;

            this.sWSP_GLOBAL_IDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_GLOBAL_ID);
            this.sWSP_PIPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_PIPE);
            this.sWSP_SHAPE_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SHAPE_TYPE);
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE);
            this.sWSP_CULVERTTableAdapter.Fill(this.sANDBOXDataSet.SWSP_CULVERT);
            this.sWSP_PHOTOTableAdapter.Fill(this.sANDBOXDataSet.SWSP_PHOTO);
            this.sWSP_MATERIAL_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_MATERIAL_TYPE);
            this.sWSP_FACING_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_FACING_TYPE);
            this.sWSP_DITCHTableAdapter.Fill(this.sANDBOXDataSet.SWSP_DITCH);
            this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE_EVALUATOR);
            this.sWSP_EVALUATORTableAdapter.Fill(this.sANDBOXDataSet.SWSP_EVALUATOR);
            this.sWSP_SURVEY_PAGETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE);
            this.sWSP_VIEWTableAdapter.Fill(this.sANDBOXDataSet.SWSP_VIEW);
            this.sWSP_SUBWATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_SUBWATERSHED);
            this.sWSP_WATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_WATERSHED);

            _lastGlobalID = 0;
            _lastSearch = _lastSearchVar.culvert;
            _returningFromForm = false;

        }

        private void dataAdministratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSWSPFieldDataAdministration child = new FormSWSPFieldDataAdministration();

            //child.GlobalID = (int)((System.Data.DataRowView)fKPIPESURVEYPAGEBindingSource.Current)["global_id"];
            _returningFromForm = true;
            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;

            this.sWSP_GLOBAL_IDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_GLOBAL_ID);
            this.sWSP_PIPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_PIPE);
            this.sWSP_SHAPE_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SHAPE_TYPE);
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE);
            this.sWSP_CULVERTTableAdapter.Fill(this.sANDBOXDataSet.SWSP_CULVERT);
            this.sWSP_PHOTOTableAdapter.Fill(this.sANDBOXDataSet.SWSP_PHOTO);
            this.sWSP_MATERIAL_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_MATERIAL_TYPE);
            this.sWSP_FACING_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_FACING_TYPE);
            this.sWSP_DITCHTableAdapter.Fill(this.sANDBOXDataSet.SWSP_DITCH);
            this.sWSP_EVALUATORTableAdapter.Fill(this.sANDBOXDataSet.SWSP_EVALUATOR);
            this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE_EVALUATOR);
            this.sWSP_SURVEY_PAGETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE);
            this.sWSP_VIEWTableAdapter.Fill(this.sANDBOXDataSet.SWSP_VIEW);
            this.sWSP_SUBWATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_SUBWATERSHED);
            this.sWSP_WATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_WATERSHED);
            

            _lastGlobalID = 0;
            _lastSearch = _lastSearchVar.culvert;
            _returningFromForm = false;
        }

        //This function is usually called during the search procedure, it allows the datatables
        //to reload themselves and be viewed properly by the interface.
        private void RefreshInterfaceTables()
        {
            sWSPWATERSHEDBindingSource.MoveFirst();
            while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID)[0])["watershed_id"] != (int)((System.Data.DataRowView)sWSPWATERSHEDBindingSource.Current)["watershed_id"])
            {
                sWSPWATERSHEDBindingSource.MoveNext();
            }
            fKSUBWATERSHEDWATERSHEDBindingSource.MoveFirst();
            while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["subwatershed_id"] != (int)((System.Data.DataRowView)fKSUBWATERSHEDWATERSHEDBindingSource.Current)["subwatershed_id"])
            {
                fKSUBWATERSHEDWATERSHEDBindingSource.MoveNext();
            }
            this.Refresh();
            fKVIEWSUBWATERSHEDBindingSource.MoveFirst();
            while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["view_id"] != (int)((System.Data.DataRowView)fKVIEWSUBWATERSHEDBindingSource.Current)["view_id"])
            {
                fKVIEWSUBWATERSHEDBindingSource.MoveNext();
            }
            this.Refresh();
            fKSURVEYPAGEVIEWBindingSource.MoveFirst();
            while ((int)(this.relationalIDsTableAdapter.GetDataByDitchID(_lastGlobalID))[0]["survey_page_id"] != (int)((System.Data.DataRowView)fKSURVEYPAGEVIEWBindingSource.Current)["survey_page_id"])
            {
                fKSURVEYPAGEVIEWBindingSource.MoveNext();
            }
        }

        private void exportReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog DialogSave = new SaveFileDialog();
            DialogSave.DefaultExt = "csv";
            DialogSave.Filter = "Text file (*.csv)|*.csv|XML file (*.xml)|*.xml|All files (*.*)|*.*";
            DialogSave.AddExtension = true;
            DialogSave.RestoreDirectory = true;
            DialogSave.Title = "Where do you want to save the file?";
            DialogSave.InitialDirectory = @"C:/";
            if (DialogSave.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    //All the pipes
                    SANDBOXDataSetTableAdapters.SWSP_PIPESTableAdapter thePipesTableAdapter =
                        new SWI_2.SANDBOXDataSetTableAdapters.SWSP_PIPESTableAdapter();
                    SANDBOXDataSet.SWSP_PIPESDataTable thePipesDataTable =
                        new SANDBOXDataSet.SWSP_PIPESDataTable();

                    /*thePipesTableAdapter.Fill(thePipesDataTable);
                    CreateCSVFile(thePipesDataTable, DialogSave.FileName, "All of the pipes in the stormwater database");
                    thePipesDataTable.Clear();*/
                    thePipesTableAdapter.FillByBadNoDSNode(thePipesDataTable);
                    CreateCSVFile(thePipesDataTable, DialogSave.FileName, "Pipes with no DS Node");
                    thePipesDataTable.Clear();
                    thePipesTableAdapter.FillByBadNoDSNodeMatch(thePipesDataTable);
                    CreateCSVFile(thePipesDataTable, DialogSave.FileName, "Pipes with no matching pipe in mst_links with the same downstream node");
                    thePipesDataTable.Clear();
                    thePipesTableAdapter.FillByBadNoInsideDiamIn(thePipesDataTable);
                    CreateCSVFile(thePipesDataTable, DialogSave.FileName, "Pipes with no inside diameter");
                    thePipesDataTable.Clear();
                    thePipesTableAdapter.FillByBadNoUSDSMatch(thePipesDataTable);
                    CreateCSVFile(thePipesDataTable, DialogSave.FileName, "Pipes with no matching pipe in mst_links with the same upstream and downstream nodes");
                    thePipesDataTable.Clear();
                    thePipesTableAdapter.FillByBadNoUSNode(thePipesDataTable);
                    CreateCSVFile(thePipesDataTable, DialogSave.FileName, "Pipes with no US node");
                    thePipesDataTable.Clear();
                    thePipesTableAdapter.FillByBadNoUSNodeMatch(thePipesDataTable);
                    CreateCSVFile(thePipesDataTable, DialogSave.FileName, "Pipes with no matching pipe in mst_links with the same upstream node");
                    thePipesDataTable.Clear();
                    thePipesTableAdapter.FillByPipesOK(thePipesDataTable);
                    CreateCSVFile(thePipesDataTable, DialogSave.FileName, "Pipes that have completely valid data");

                    //All the ditches
                    SANDBOXDataSetTableAdapters.SWSP_DITCHESTableAdapter theDitchesTableAdapter =
                        new SWI_2.SANDBOXDataSetTableAdapters.SWSP_DITCHESTableAdapter();
                    SANDBOXDataSet.SWSP_DITCHESDataTable theDitchesDataTable =
                        new SANDBOXDataSet.SWSP_DITCHESDataTable();

                    /*theDitchesDataTable.Clear();
                    theDitchesTableAdapter.Fill(theDitchesDataTable);
                    CreateCSVFile(theDitchesDataTable, DialogSave.FileName, "All of the ditches in the stormwater database");*/
                    theDitchesTableAdapter.FillByBadDepthLT1(theDitchesDataTable);
                    CreateCSVFile(theDitchesDataTable, DialogSave.FileName, "Ditches with depth less than one inch");
                    theDitchesDataTable.Clear();
                    theDitchesTableAdapter.FillByBadNoDSFacingMatch(theDitchesDataTable);
                    CreateCSVFile(theDitchesDataTable, DialogSave.FileName, "Ditches with no matching link in mst_links with the same downstream node");
                    theDitchesDataTable.Clear();
                    theDitchesTableAdapter.FillByBadNoFacing(theDitchesDataTable);
                    CreateCSVFile(theDitchesDataTable, DialogSave.FileName, "Ditches without a facing");
                    theDitchesDataTable.Clear();
                    theDitchesTableAdapter.FillByBadNoNode(theDitchesDataTable);
                    CreateCSVFile(theDitchesDataTable, DialogSave.FileName, "Ditches without a node");
                    theDitchesDataTable.Clear();
                    theDitchesTableAdapter.FillByBadNoUSFacingMatch(theDitchesDataTable);
                    CreateCSVFile(theDitchesDataTable, DialogSave.FileName, "Ditches with no matching link in mst_links with the same upstream node");
                    theDitchesDataTable.Clear();
                    theDitchesTableAdapter.FillByBadWidthsImproper(theDitchesDataTable);
                    CreateCSVFile(theDitchesDataTable, DialogSave.FileName, "Ditches with top width less than bottom width or no widths");
                    theDitchesDataTable.Clear();
                    theDitchesTableAdapter.FillByDitchesOK(theDitchesDataTable);
                    CreateCSVFile(theDitchesDataTable, DialogSave.FileName, "Ditches that have completely valid data");

                    //All the culverts
                    SANDBOXDataSetTableAdapters.SWSP_CULVERTSTableAdapter theCulvertsTableAdapter =
                        new SWI_2.SANDBOXDataSetTableAdapters.SWSP_CULVERTSTableAdapter();
                    SANDBOXDataSet.SWSP_CULVERTSDataTable theCulvertsDataTable =
                        new SANDBOXDataSet.SWSP_CULVERTSDataTable();

                    /*theCulvertsTableAdapter.Fill(theCulvertsDataTable);
                    CreateCSVFile(theCulvertsDataTable, DialogSave.FileName, "All of the culverts in the stormwater database");
                    theCulvertsDataTable.Clear();*/
                    theCulvertsTableAdapter.FillByBadDiameterNotStandard(theCulvertsDataTable);
                    CreateCSVFile(theCulvertsDataTable, DialogSave.FileName, "Culverts with non-standard diameters");
                    theCulvertsDataTable.Clear();
                    theCulvertsTableAdapter.FillByBadNoDimension(theCulvertsDataTable);
                    CreateCSVFile(theCulvertsDataTable, DialogSave.FileName, "Culverts with no valid measurments");
                    theCulvertsDataTable.Clear();
                    theCulvertsTableAdapter.FillByBadNoDSFacingMatch(theCulvertsDataTable);
                    CreateCSVFile(theCulvertsDataTable, DialogSave.FileName, "Culverts with no matching link in mst_links with the same downstream node");
                    theCulvertsDataTable.Clear();
                    theCulvertsTableAdapter.FillByBadNoFacing(theCulvertsDataTable);
                    CreateCSVFile(theCulvertsDataTable, DialogSave.FileName, "Culverts without a facing");
                    theCulvertsDataTable.Clear();
                    theCulvertsTableAdapter.FillByBadNoNode(theCulvertsDataTable);
                    CreateCSVFile(theCulvertsDataTable, DialogSave.FileName, "Culverts without a node");
                    theCulvertsDataTable.Clear();
                    theCulvertsTableAdapter.FillByBadNoUSFacingMatch(theCulvertsDataTable);
                    CreateCSVFile(theCulvertsDataTable, DialogSave.FileName, "Culverts with no matching link in mst_links with the same upstream node");
                    theCulvertsDataTable.Clear();
                    theCulvertsTableAdapter.FillByCulvertsOK(theCulvertsDataTable);
                    CreateCSVFile(theCulvertsDataTable, DialogSave.FileName, "Culverts that have completely valid data");
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Arrow;
                }
 
                this.Cursor = Cursors.Arrow;
            }
            DialogSave.Dispose();
            DialogSave = null;
        }

        public void CreateCSVFile(DataTable dt, string strFilePath, string strTableDescription)
        {

            #region Export SWI to CSV
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
                        sw.Write(dr[i].ToString().Replace(",", "/"));
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

        private void updateMstlinksacToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            UserClickedCancel = true;
            FormSelectWatershedSubwatershed child = new FormSelectWatershedSubwatershed();
            child.MyParentForm = this;
            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;

            if (UserClickedCancel == false)
            {
                Cursor.Current = Cursors.WaitCursor;
                string mst_linksDB = /*"E:\\MstLinksCopy\\mst_links_ac.mdb";*/"\\\\Cassio\\modeling\\SAMaster_22\\Links\\mst_links_ac.mdb" ;
                //copy the good tables to the mst_links database
                DataMobility.AccessCopySQLTable("GoodPipes_x", /*"ODBC;DSN=SIRTOBY;DATABASE = SWI;Trusted_Connection=yes"*/"ODBC;Driver={SQL Native Client};Server=SIRTOBY;Database=SWI;Trusted_Connection=yes;", "SWSP_PIPES_OK", mst_linksDB);
                AccessHelper.AccessCopyTable("GoodPipes", "GoodPipes_x", mst_linksDB);

                //create join query for mst_links_ac and GoodPipes, call the join query "theMatches"
                string theMatches = "SELECT mst_links_ac.MLinkID, GoodPipes.inside_diam_in, GoodPipes.inside_width_in, GoodPipes.shape, GoodPipes.material AS MatNew, GoodPipes.length_ft, mst_links_ac.PipeShape, mst_links_ac.Length, mst_links_ac.DiamWidth, mst_links_ac.Height, mst_links_ac.Material AS MatOld, mst_links_ac.DataQual FROM mst_links_ac INNER JOIN GoodPipes ON (mst_links_ac.USNode=GoodPipes.us_node) AND (mst_links_ac.DSNode=GoodPipes.ds_node) WHERE GoodPipes.Watershed = '"
                    + SelectedWatershed + "' and GoodPipes.Subwatershed = '" + SelectedSubwatershed + "';";

                //send the join query to access
                AccessHelper.AccessCreateQuery("theMatches", theMatches, mst_linksDB);


                //update mst_links based on usnode and dsnode matches in the good pipe tables
                //Put a valid string in the dataqual field.  This is a necessary step
                string theQuery =
                    "update theMatches " +
                    "set DataQual = \"????????\" " +
                    "where Len(Trim(DataQual)) <> 8" +
                    "";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update mst_links based on usnode and dsnode matches in the good pipe tables
                //update the diamwidth field if there is no value for width in SWSP(diam in the SWSP database refers to height)
                theQuery =
                    "update theMatches " +
                    "set diamwidth = inside_diam_in, DataQual = \"L\" & Right(DataQual, 7)" +
                    "where " +
                    "inside_diam_in <> 0 and " +
                    "inside_diam_in is not null and " +
                    "(inside_width_in = 0 or inside_width_in is null)";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update the diamwidth field if there is a value for width in SWSP
                theQuery =
                    "update theMatches " +
                    "set diamwidth = inside_width_in, DataQual = \"L\" & Right(DataQual, 7) " +
                    "where " +
                    "inside_width_in <> 0 and " +
                    "inside_width_in is not null";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update the height field if there is a value for width in SWSP
                theQuery =
                    "update theMatches " +
                    "set Height = inside_diam_in , DataQual = \"L\" & Right(DataQual, 7)" +
                    "where " +
                    "inside_width_in <> 0 and " +
                    "inside_width_in is not null";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update the shape field
                theQuery =
                    "update theMatches " +
                    "set PipeShape = shape , DataQual = Left(DataQual, 1) & \"L\" & Right(DataQual, 6)";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update the material field
                theQuery =
                    "update theMatches " +
                    "set MatOld = MatNew , DataQual = Left(DataQual, 2) & \"L\" & Right(DataQual, 5)";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update the length field
                theQuery =
                    "update theMatches " +
                    "set Length = length_ft , DataQual = Left(DataQual, 6) &  \"L\" & Right(DataQual, 1)" +
                    "WHERE length_ft <>0 AND length_ft is not null";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //delete copied tables
                //AccessHelper.AccessCopyTable("Pipes", "GoodPipes_x", mst_linksDB);
                AccessHelper.AccessDropTable("GoodPipes_x", mst_linksDB);
                AccessHelper.AccessDropQuery("theQuery", mst_linksDB);


                //copy the good culvert tables to the mst_links database
                DataMobility.AccessCopySQLTable("GoodPipes_x", /*"ODBC;DSN=SIRTOBY;DATABASE = SWI;Trusted_Connection=yes"*/"ODBC;Driver={SQL Native Client};Server=SIRTOBY;Database=SWI;Trusted_Connection=yes;", "SWSP_CULVERTS_OK", mst_linksDB);
                AccessHelper.AccessCopyTable("GoodPipes", "GoodPipes_x", mst_linksDB);

                //create join query for mst_links_ac and GoodPipes, call the join query "theMatches"
                theMatches = "SELECT mst_links_ac.MLinkID, GoodPipes.full_diam_in, GoodPipes.full_width_in, GoodPipes.shape, GoodPipes.material AS MatNew, GoodPipes.length_ft, mst_links_ac.PipeShape, mst_links_ac.Length, mst_links_ac.DiamWidth, mst_links_ac.Height, mst_links_ac.Material AS MatOld, mst_links_ac.DataQual FROM mst_links_ac INNER JOIN GoodPipes ON (mst_links_ac.USNode=GoodPipes.us_node) AND (mst_links_ac.DSNode=GoodPipes.ds_node) WHERE GoodPipes.Watershed = '"
                    + SelectedWatershed + "' and GoodPipes.Subwatershed = '" + SelectedSubwatershed + "';";

                //send the join query to access
                AccessHelper.AccessCreateQuery("theMatches", theMatches, mst_linksDB);
                //Put a valid string in the dataqual field.  This is a necessary step
                theQuery =
                    "update theMatches " +
                    "set DataQual = \"????????\" " +
                    "where Len(Trim(DataQual)) <> 8" +
                    "";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update mst_links based on usnode and dsnode matches in the good ditch tables
                //update the diamwidth field if there is no value for width in SWSP(diam in the SWSP database refers to height)
                theQuery =
                    "update theMatches " +
                    "set diamwidth = full_diam_in, DataQual = \"L\" & Right(DataQual, 7) " +
                    "where " +
                    "full_diam_in <> 0 and " +
                    "full_diam_in is not null and " +
                    "(full_width_in = 0 or full_width_in is null)";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update the diamwidth field if there is a value for width in SWSP
                theQuery =
                    "update theMatches " +
                    "set diamwidth = full_width_in, DataQual = \"L\" & Right(DataQual, 7) " +
                    "where " +
                    "full_width_in <> 0 and " +
                    "full_width_in is not null";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update the height field if there is a value for width in SWSP
                theQuery =
                    "update theMatches " +
                    "set Height = full_diam_in, DataQual = \"L\" & Right(DataQual, 7) " +
                    "where " +
                    "full_width_in <> 0 and " +
                    "full_width_in is not null";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update the shape field
                theQuery =
                    "update theMatches " +
                    "set PipeShape = shape , DataQual = Left(DataQual, 1) & \"L\" & Right(DataQual, 6)";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update the material field
                theQuery =
                    "update theMatches " +
                    "set MatOld = MatNew , DataQual = Left(DataQual, 2) & \"L\" & Right(DataQual, 5)";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update the length field
                theQuery =
                    "update theMatches " +
                    "set Length = length_ft , DataQual = Left(DataQual, 6) & \"L\" & Right(DataQual, 1)" +
                    "WHERE length_ft <>0 AND length_ft is not null";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //delete copied tables
                //AccessHelper.AccessCopyTable("Culverts", "GoodPipes_x", mst_linksDB);
                AccessHelper.AccessDropTable("GoodPipes_x", mst_linksDB);
                AccessHelper.AccessDropQuery("theQuery", mst_linksDB);




                //copy the good ditch tables to the mst_links database
                DataMobility.AccessCopySQLTable("GoodPipes_x", /*"ODBC;DSN=SIRTOBY;DATABASE = SWI;Trusted_Connection=yes"*/"ODBC;Driver={SQL Native Client};Server=SIRTOBY;Database=SWI;Trusted_Connection=yes;", "SWSP_DITCHES_OK", mst_linksDB);
                AccessHelper.AccessCopyTable("GoodPipes", "GoodPipes_x", mst_linksDB);

                //create join query for mst_links_ac and GoodPipes, call the join query "theMatches"
                theMatches = "SELECT mst_links_ac.MLinkID, GoodPipes.bottom_width_in, GoodPipes.depth_in, GoodPipes.material AS MatNew, GoodPipes.length_ft, mst_links_ac.PipeShape, mst_links_ac.Length, mst_links_ac.DiamWidth, mst_links_ac.Height, mst_links_ac.Material AS MatOld, mst_links_ac.DataQual FROM mst_links_ac INNER JOIN GoodPipes ON (mst_links_ac.USNode=GoodPipes.us_node) AND (mst_links_ac.DSNode=GoodPipes.ds_node)WHERE GoodPipes.Watershed = '"
                    + SelectedWatershed + "' and GoodPipes.Subwatershed = '" + SelectedSubwatershed + "';";

                //send the join query to access
                AccessHelper.AccessCreateQuery("theMatches", theMatches, mst_linksDB);
                //Put a valid string in the dataqual field.  This is a necessary step
                theQuery =
                    "update theMatches " +
                    "set DataQual = \"????????\" " +
                    "where Len(Trim(DataQual)) <> 8" +
                    "";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update mst_links based on usnode and dsnode matches in the good ditch tables
                //update the diamwidth field if there is no value for width in SWSP(diam in the SWSP database refers to height)
                theQuery =
                    "update theMatches " +
                    "set diamwidth = bottom_width_in, DataQual = \"L\" & Right(DataQual, 7)  ";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update the height field if there is a value for width in SWSP
                theQuery =
                    "update theMatches " +
                    "set Height = depth_in, DataQual = \"L\" & Right(DataQual, 7)  ";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update the material field
                theQuery =
                    "update theMatches " +
                    "set MatOld = MatNew, DataQual = Left(DataQual, 2) & \"L\" & Right(DataQual, 5) ";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update the length field
                theQuery =
                    "update theMatches " +
                    "set Length = length_ft, DataQual = Left(DataQual, 6) & \"L\" & Right(DataQual, 1) " +
                    "WHERE length_ft <>0 AND length_ft is not null";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //update the shape field - yes this should be done
                theQuery =
                    "update theMatches " +
                    "set PipeShape = 'TR31', DataQual = Left(DataQual, 1) & \"L\" & Right(DataQual, 6) ";
                AccessHelper.AccessCreateQuery("theQuery", theQuery, mst_linksDB);
                AccessHelper.AccessExecuteActionQuery("theQuery", mst_linksDB);
                //delete copied tables
                //AccessHelper.AccessCopyTable("Ditches", "GoodPipes_x", mst_linksDB);
                AccessHelper.AccessDropTable("GoodPipes_x", mst_linksDB);
                AccessHelper.AccessDropQuery("theQuery", mst_linksDB);
                AccessHelper.AccessDropTable("GoodPipes", mst_linksDB);
                AccessHelper.AccessDropQuery("theMatches", mst_linksDB);

                Cursor.Current = Cursors.Default;
            }
        }

        private void newSurveyViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SWIInputForm child = new SWIInputForm();

            _returningFromForm = true;
            this.Enabled = false;
            child.ShowDialog();
            this.Enabled = true;

            this.sWSP_GLOBAL_IDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_GLOBAL_ID);
            this.sWSP_PIPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_PIPE);
            this.sWSP_SHAPE_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SHAPE_TYPE);
            this.sWSP_CULVERT_OPENING_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_CULVERT_OPENING_TYPE);
            this.sWSP_CULVERTTableAdapter.Fill(this.sANDBOXDataSet.SWSP_CULVERT);
            this.sWSP_PHOTOTableAdapter.Fill(this.sANDBOXDataSet.SWSP_PHOTO);
            this.sWSP_MATERIAL_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_MATERIAL_TYPE);
            this.sWSP_FACING_TYPETableAdapter.Fill(this.sANDBOXDataSet.SWSP_FACING_TYPE);
            this.sWSP_DITCHTableAdapter.Fill(this.sANDBOXDataSet.SWSP_DITCH);
            this.sWSP_SURVEY_PAGE_EVALUATORTableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE_EVALUATOR);
            this.sWSP_EVALUATORTableAdapter.Fill(this.sANDBOXDataSet.SWSP_EVALUATOR);
            this.sWSP_SURVEY_PAGETableAdapter.Fill(this.sANDBOXDataSet.SWSP_SURVEY_PAGE);
            this.sWSP_VIEWTableAdapter.Fill(this.sANDBOXDataSet.SWSP_VIEW);
            this.sWSP_SUBWATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_SUBWATERSHED);
            this.sWSP_WATERSHEDTableAdapter.Fill(this.sANDBOXDataSet.SWSP_WATERSHED);

            _lastGlobalID = 0;
            _lastSearch = _lastSearchVar.culvert;
            _returningFromForm = false;
        }
    }
}
