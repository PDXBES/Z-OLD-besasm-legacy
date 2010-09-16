using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Access;

namespace DicretizeIF
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        string FiveFtSegsDBLocation = "";
        DialogResult FiveFtSegsDBDialogResult = DialogResult.Abort;
        DialogResult SaveFileDialogOutputDBDialogResult = DialogResult.Abort;
        string outputLocation = "";

        string ConversionDBLocation = "";
        DialogResult ConversionDBDialogResult = DialogResult.Abort;

        string MortalityDBLocation = "";
        DialogResult MortalityDBDialogResult = DialogResult.Abort;

        string ConstructionDBLocation = "";
        DialogResult ConstructionDBDialogResult = DialogResult.Abort;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSegmentDBPath_Click(object sender, EventArgs e)
        {
            FiveFtSegsDBDialogResult = this.openFileDialog1.ShowDialog();
            if (FiveFtSegsDBDialogResult == DialogResult.OK)
            {
                MessageBox.Show(openFileDialog1.FileName);
                FiveFtSegsDBLocation = openFileDialog1.FileName;
                textBoxSegmentDBPath.Text = FiveFtSegsDBLocation;
            }
            MainForm.ActiveForm.Cursor = Cursors.WaitCursor;
            MainForm.ActiveForm.Enabled = false;

            // Get the application configuration file.
            System.Configuration.Configuration config =
                    ConfigurationManager.OpenExeConfiguration(
                    ConfigurationUserLevel.None);

            // Create a connection string element and
            // save it to the configuration file.

            // Create a connection string element.
            string conStrg = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                openFileDialog1.FileName + "";

            ConnectionStringSettings csSettings =
                    new ConnectionStringSettings("DiscretizeInterface.Properties.Settings.MLA_05FtBrk_acConnectionString",
                    conStrg, "System.Data.OleDb");

            ConnectionStringSettings csSettingsOld =
                    new ConnectionStringSettings("DiscretizeInterface.Properties.Settings.MLA_05FtBrk_acConnectionString",
                    "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\\Cassio\\SystemsPlanning\\7977_Rehab\\WorkingDatabase\\MLA_05FtBrk_ac.mdb", "System.Data.OleDb");

            // Get the connection strings section.
            ConnectionStringsSection csSection =
                config.ConnectionStrings;

            // Add the new element.
            csSection.ConnectionStrings.Remove(csSettingsOld);
            csSection.ConnectionStrings.Add(csSettings);

            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);

            try
            {
                this.mLA_05FtBrk_acTableAdapter.Connection.ConnectionString = conStrg;
            }
            catch (System.Data.OleDb.OleDbException exp)
            {
                MessageBox.Show(exp.Message.ToString());
            }
            this.mLA_05FtBrk_acTableAdapter.Fill(this.mLA_05FtBrk_acDataSet.MLA_05FtBrk_ac);
            this.dataGridView1.Refresh();

            MainForm.ActiveForm.Cursor = Cursors.Default;
            MainForm.ActiveForm.Enabled = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mLA_05FtBrk_acDataSet.MLA_05FtBrk_ac' table. You can move, or remove it, as needed.
            this.mLA_05FtBrk_acTableAdapter.Fill(this.mLA_05FtBrk_acDataSet.MLA_05FtBrk_ac);
        }

        private void btnOutputLocation_Click(object sender, EventArgs e)
        {
            SaveFileDialogOutputDBDialogResult = this.saveFileDialogOutputDB.ShowDialog();

            if (SaveFileDialogOutputDBDialogResult == DialogResult.OK)
            {
                MessageBox.Show(saveFileDialogOutputDB.FileName);

                File.Copy("\\\\Cassio\\SystemsPlanning\\7977_Rehab\\WorkingDatabase\\SegsOutput.mdb", saveFileDialogOutputDB.FileName);
                outputLocation = saveFileDialogOutputDB.FileName;
                textBoxOutputLocation.Text = outputLocation;
            }
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {

            MainForm.ActiveForm.Cursor = Cursors.WaitCursor;
            MainForm.ActiveForm.Enabled = false;
            //Add a link from the original MLA5ftsegs db to the output db
            Microsoft.Office.Interop.Access.Application oAccess = null;

            oAccess = new Microsoft.Office.Interop.Access.ApplicationClass();

            // Open a database in exclusive mode:
            oAccess.OpenCurrentDatabase(
               outputLocation, //filepath
               true, //Exclusive
               ""//no password
               );

            dao.Database dBase = oAccess.CurrentDb();
            dao.TableDef tDef = dBase.CreateTableDef("MLA_05FtBrk_ac_Origin", Type.Missing, Type.Missing, Type.Missing);
            tDef.Connect = ";DATABASE=" + FiveFtSegsDBLocation;
            tDef.SourceTableName = "MLA_05FtBrk_ac";
            dBase.TableDefs.Append(tDef);

            dao.Recordset rstLinked = dBase.OpenRecordset("MLA_05FtBrk_ac_Origin", Type.Missing, Type.Missing, Type.Missing);

            //Take the relevant entries in MLA05ftBrk table and place them into MLA_05FtBrk_ac
            dao.QueryDef qDef = dBase.CreateQueryDef("FillQuery", "INSERT INTO MLA_05FtBrk_ac_Local (LinkID, UsNode, DsNode, MLinkID, CompKey, Length, DiamWidth, Height, PipeShape, Material, Instdate, IsSpecLink, HServStat, IsActive, OLD_MLID, OLD_Len, SplitID, SplitTyp, CutNO, FM, TO, SegLen) SELECT LinkID, UsNode, DsNode, MLinkID, CompKey, Length, DiamWidth, Height, Shape, Material, Instdate, IsSpecLink, HServStat, IsActive, OLD_MLID, OLD_Len, SplitID, SplitTyp, CutNO, FM, TO, SegLen FROM MLA_05FtBrk_ac_Origin");
            qDef.Execute(Type.Missing);

            //query out the entries in the CHGDETL table so that only the changes that refer to material
            //changes exist in the new table "material_changes"
            dao.TableDef tDef2 = dBase.CreateTableDef("Conversion", Type.Missing, Type.Missing, Type.Missing);
            tDef2.Connect = ";DATABASE=" + ConversionDBLocation;
            tDef2.SourceTableName = "Conversion";
            dBase.TableDefs.Append(tDef2);

            dao.Recordset rstLinked2 = dBase.OpenRecordset("Conversion", Type.Missing, Type.Missing, Type.Missing);

            //Mortality table should consist of an MLinkID and a Total_Consequence field, in a table called "MortalityExport"
            //Construction table should consist of an MLinkID and a TotalConstructionCost field, in a table called
            //"ConstructionExport".  The MLinkID should refer to the artificial MLinkID of the segments, and for
            //situations where construction or mortality are based on whole pipes, the values should just
            //be repeated across all MLinkIDs associated with any one CompKey.

            //Identify mortality source table 
            dao.TableDef tDef3 = dBase.CreateTableDef("MortalityExport", Type.Missing, Type.Missing, Type.Missing);
            tDef3.Connect = ";DATABASE=" + MortalityDBLocation;
            tDef3.SourceTableName = "MortalityExport";
            dBase.TableDefs.Append(tDef3);

            dao.Recordset rstLinked3 = dBase.OpenRecordset("MortalityExport", Type.Missing, Type.Missing, Type.Missing);

            //Identify construction source table
            dao.TableDef tDef4 = dBase.CreateTableDef("ConstructionExport", Type.Missing, Type.Missing, Type.Missing);
            tDef4.Connect = ";DATABASE=" + ConstructionDBLocation;
            tDef4.SourceTableName = "ConstructionExport";
            dBase.TableDefs.Append(tDef4);

            dao.Recordset rstLinked4 = dBase.OpenRecordset("ConstructionExport", Type.Missing, Type.Missing, Type.Missing);

            //call queries based on the values currently held.
            //The queries should just be called in order.  For now, just run the program to create
            //the copy database and see where it goes from there.
            //point defect select_grp_5
            qDef = dBase.QueryDefs["point defect select_grp_5"];
            qDef.Execute(Type.Missing);
            //linear defect select_grp_5
            qDef = dBase.QueryDefs["linear defect select_grp_5"];
            qDef.Execute(Type.Missing);
            //Update_point_defect_5
            qDef = dBase.QueryDefs["Update_point_defect_5"];
            qDef.Execute(Type.Missing);
            //Update linear defect_5
            qDef = dBase.QueryDefs["Update linear defect_5"];
            qDef.Execute(Type.Missing);
            //Update_Null_PointDefect_5
            qDef = dBase.QueryDefs["Update_Null_PointDefect_5"];
            qDef.Execute(Type.Missing);
            //Update_Null_LinearDefect_5
            qDef = dBase.QueryDefs["Update_Null_LinearDefect_5"];
            qDef.Execute(Type.Missing);
            //Update total defect score_5
            qDef = dBase.QueryDefs["Update total defect score_5"];
            qDef.Execute(Type.Missing);
            //Total_Defectsx15_5
            qDef = dBase.QueryDefs["Total_Defectsx15_5"];
            qDef.Execute(Type.Missing);
            //Update_TV_Inspection_Date
            qDef = dBase.QueryDefs["Update_TV_Inspection_Date"];
            qDef.Execute(Type.Missing);
            //Update_Year_Since_TV_Inspection
            qDef = dBase.QueryDefs["Update_Year_Since_TV_Inspection"];
            qDef.Execute(Type.Missing);
            //update_material_smn
            qDef = dBase.QueryDefs["update_material_smn"];
            qDef.Execute(Type.Missing);
            //update_material_stmn
            qDef = dBase.QueryDefs["update_material_stmn"];
            qDef.Execute(Type.Missing);
            //Curr_SMN_1
            qDef = dBase.QueryDefs["Curr_SMN_1"];
            qDef.Execute(Type.Missing);
            //Curr_STMN_1
            qDef = dBase.QueryDefs["Curr_STMN_1"];
            qDef.Execute(Type.Missing);
            //Curr_SMN_2
            qDef = dBase.QueryDefs["Curr_SMN_2"];
            qDef.Execute(Type.Missing);
            //Curr_STMN_2
            qDef = dBase.QueryDefs["Curr_STMN_2"];
            qDef.Execute(Type.Missing);
            //Curr_SMN_3
            qDef = dBase.QueryDefs["Curr_SMN_3"];
            qDef.Execute(Type.Missing);
            //Curr_STMN_3
            qDef = dBase.QueryDefs["Curr_STMN_3"];
            qDef.Execute(Type.Missing);
            //Curr_SMN_4
            qDef = dBase.QueryDefs["Curr_SMN_4"];
            qDef.Execute(Type.Missing);
            //Curr_STMN_4
            qDef = dBase.QueryDefs["Curr_STMN_4"];
            qDef.Execute(Type.Missing);
            //MLA_05FtBrk_Varies
            qDef = dBase.QueryDefs["MLA_05FtBrk_Varies"];
            qDef.Execute(Type.Missing);
            //CREATE_Material_Changes
            qDef = dBase.QueryDefs["CREATE_Material_Changes"];
            qDef.Execute(Type.Missing);
            //SET_IMPROPER_LENGTHS_MATERIAL_TO_UNKNOWN
            qDef = dBase.QueryDefs["SET_IMPROPER_LENGTHS_MATERIAL_TO_UNKNOWN"];
            qDef.Execute(Type.Missing);
            //AA_Records_With_VSP_Create
            qDef = dBase.QueryDefs["AA_Records_With_VSP_Create"];
            qDef.Execute(Type.Missing);
            //AB_Records_That_Patch_VSP_Create
            qDef = dBase.QueryDefs["AB_Records_That_Patch_VSP_Create"];
            qDef.Execute(Type.Missing);
            //AC_Remaining_Records_Create
            qDef = dBase.QueryDefs["AC_Remaining_Records_Create"];
            qDef.Execute(Type.Missing);
            //AD_Records_With_No_VSP_Create
            qDef = dBase.QueryDefs["AD_Records_With_No_VSP_Create"];
            qDef.Execute(Type.Missing);
            //Update_05_ft_Brk_VSP_Nominal
            qDef = dBase.QueryDefs["Update_05_ft_Brk_VSP_Nominal"];
            qDef.Execute(Type.Missing);
            //Update_05_ft_Brk_VSP_Patch
            qDef = dBase.QueryDefs["Update_05_ft_Brk_VSP_Patch"];
            qDef.Execute(Type.Missing);
            //Longest_Material_per_Compkey_Query
            qDef = dBase.QueryDefs["Longest_Material_per_Compkey_Query"];
            qDef.Execute(Type.Missing);
            //NON_VSP_PRIMARY_QUERY
            qDef = dBase.QueryDefs["NON_VSP_PRIMARY_QUERY"];
            qDef.Execute(Type.Missing);
            //UPDATE_MLA_NON_VSP_PRIMARIES
            qDef = dBase.QueryDefs["UPDATE_MLA_NON_VSP_PRIMARIES"];
            qDef.Execute(Type.Missing);
            //UPDATE_MLA_NON_VSP_PATCHES
            qDef = dBase.QueryDefs["UPDATE_MLA_NON_VSP_PATCHES"];
            qDef.Execute(Type.Missing);
            //Update_MLA_05FtBrkac_Local
            qDef = dBase.QueryDefs["Update_MLA_05FtBrkac_Local"];
            qDef.Execute(Type.Missing);
            //RemainingYears
            qDef = dBase.QueryDefs["RemainingYears"];
            qDef.Execute(Type.Missing);
            //Update_Failure_Year
            qDef = dBase.QueryDefs["Update_Failure_Year"];
            qDef.Execute(Type.Missing);
            //Update_Std_dev
            qDef = dBase.QueryDefs["Update_Std_dev"];
            qDef.Execute(Type.Missing);
            //UpdateConsequences
            qDef = dBase.QueryDefs["UpdateConsequences"];
            qDef.Execute(Type.Missing);
            //UpdateConstructionCost
            qDef = dBase.QueryDefs["UpdateConstructionCost"];
            qDef.Execute(Type.Missing);


            //Close the database and set variables to null.  Should also trap errors sometime in the body,
            //because if any errors happen to force the algorithm to stop, the MSACCESS process continues 
            //running in the background.
            dBase.Close();
            dBase = null;

            MainForm.ActiveForm.Cursor = Cursors.Default;
            MainForm.ActiveForm.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonConversionsDatabase_Click(object sender, EventArgs e)
        {
            //conversions database must be opened as a link for this process to complete correctly?
            //The point of the conversions table is to ensure which of the pipes is up to date in
            //its rating(except for material changes).  Which means that yes, the conversions table
            //is important.
            ConversionDBDialogResult = this.openFileDialog1.ShowDialog();
            if (ConversionDBDialogResult == DialogResult.OK)
            {
                MessageBox.Show(openFileDialog1.FileName);
                ConversionDBLocation = openFileDialog1.FileName;
                textBoxConversionsDatabase.Text = ConversionDBLocation;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonMortalityDatabase_Click(object sender, EventArgs e)
        {
            //Locate the Mortality Database.  The mortality table and the
            //construction table may share the same database.
            MortalityDBDialogResult = this.openFileDialog1.ShowDialog();
            if (MortalityDBDialogResult == DialogResult.OK)
            {
                MessageBox.Show(openFileDialog1.FileName);
                MortalityDBLocation = openFileDialog1.FileName;
                textBoxMortalityDatabase.Text = MortalityDBLocation;
            }
        }

        private void buttonConstructionDatabase_Click(object sender, EventArgs e)
        {
            //Locate the Construction Database.  The construction table and
            //the mortality table may share the same database.
            ConstructionDBDialogResult = this.openFileDialog1.ShowDialog();
            if (ConstructionDBDialogResult == DialogResult.OK)
            {
                MessageBox.Show(openFileDialog1.FileName);
                ConstructionDBLocation = openFileDialog1.FileName;
                textBoxConstructionDatabase.Text = ConstructionDBLocation;
            }
        }
    }
}
