using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RXI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'rEHABDataSet.Constants' table. You can move, or remove it, as needed.
            this.constantsTableAdapter.PrepFill(this.rEHABDataSet.Constants);

            // TODO: This line of code loads data into the 'rEHABDataSet.REHAB10FTSEGS' table. You can move, or remove it, as needed.
            this.rEHAB10FTSEGSTableAdapter.FillByCK(this.rEHABDataSet.REHAB10FTSEGS, -1);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            try
            {
                this.rEHAB10FTSEGSTableAdapter.FillByCompkey(this.rEHABDataSet.REHAB10FTSEGS, int.Parse(textBoxGetPipeByCompkey.Text));

                

                this.rEHAB10FTSEGSTableAdapter.USP_REHAB_1CREATECONVERSIONGIS_SINGLE();
                //update the consequence of failure/replacement cost if those have been provided
                if (textBoxReplacementCost.Enabled && int.Parse(textBoxReplacementCost.Text) > -1)
                {
                    String query = "";
                    query = "UPDATE GIS.REHAB_RedundancyTable_SINGLE SET Replacement_Cost = " + textBoxReplacementCost.Text;
                    string connectionString = global::RXI.Properties.Settings.Default.REHABConnectionString;
                    using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connectionString))
                    {
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                if (textBoxConsequenceOfFailure.Enabled && int.Parse(textBoxConsequenceOfFailure.Text) > -1)
                {
                    String query = "";
                    query = "UPDATE GIS.REHAB_RedundancyTable_SINGLE SET Consequence_Failure = " + textBoxConsequenceOfFailure.Text;
                    string connectionString = global::RXI.Properties.Settings.Default.REHABConnectionString;
                    using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connectionString))
                    {
                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                this.rEHAB10FTSEGSTableAdapter.USP_REHAB_2IDENTIFYSPOTREPAIRSFASTER_SINGLE();
                this.rEHAB10FTSEGSTableAdapter.USP_REHAB_3PREPARETRANSFERTABLEGIS_SINGLE();
                this.rEHAB10FTSEGSTableAdapter.USP_REHAB_4PREPARETRANSFERTABLEWHOLE_SINGLE();
                this.rEHAB10FTSEGSTableAdapter.USP_REHAB_5UPDATEFROMTRANSFERTABLE_1_SINGLE();
                this.rEHAB10FTSEGSTableAdapter.USP_REHAB_6UPDATEFROMTRANSFERTABLE_2_SINGLE();
                this.rEHAB10FTSEGSTableAdapter.USP_REHAB_7UPDATEEXCEPTIONS_SINGLE();
                this.rEHAB10FTSEGSTableAdapter.USP_REHAB_8SegFuture_SINGLE();
                this.rEHAB10FTSEGSTableAdapter.USP_REHAB_9HIDEDATATHATGETSUSEDINCORRECTLY_SINGLE();
                this.rEHAB10FTSEGSTableAdapter.FillRefresh(this.rEHABDataSet.REHAB10FTSEGS);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing request: \n" + ex.ToString());
            }
            Cursor.Current = System.Windows.Forms.Cursors.Arrow;
        }

        private void UpdateDatabase(object sender, EventArgs e)
        {
            TextBox senderTextBox;   
 
            if(sender is TextBox)
            {
                senderTextBox = (TextBox)sender;
                String query = "";

                switch (senderTextBox.Name)
                {
                   case "textBoxBasementFloodingCost":
                        query = "UPDATE GIS.Constants_SINGLE SET BasementFloodingCost = " + textBoxBasementFloodingCost.Text;
                        break;
                   case "textBoxEmergencyMaintenanceLateralRepairCost":
                        query = "UPDATE GIS.Constants_SINGLE SET EMLateralRepairCost  = " + textBoxEmergencyMaintenanceLateralRepairCost.Text;
                        break;
                   case "textBoxEmergencyMaintenanceRepairFactor":
                        query = "UPDATE GIS.Constants_SINGLE SET EMRepairFactor = " + textBoxEmergencyMaintenanceRepairFactor.Text;
                        break;
                   case "textBoxEstimateENR":
                        query = "UPDATE GIS.Constants_SINGLE SET EstimateENR = " + textBoxEstimateENR.Text;
                        break;
                   case "textBoxIllicitSpillFarFromSchool":
                        query = "UPDATE GIS.Constants_SINGLE SET IllicitSpillFarSchool = " + textBoxIllicitSpillFarFromSchool.Text;
                        break;
                   case "textBoxIllicitSpillNearSchool":
                        query = "UPDATE GIS.Constants_SINGLE SET IllicitSpillNearSchool = " + textBoxIllicitSpillNearSchool.Text;
                        break;
                   case "textBoxManholeFloodFarFromSchool":
                        query = "UPDATE GIS.Constants_SINGLE SET MHFloodFarSchoolCost = " + textBoxManholeFloodFarFromSchool.Text;
                        break;
                   case "textBoxManholeFloodFreewayCleanup":
                        query = "UPDATE GIS.Constants_SINGLE SET MHFloodCleanupFrwy = " + textBoxManholeFloodFreewayCleanup.Text;
                        break;
                   case "textBoxManholeFloodMajorArterialCleanup":
                        query = "UPDATE GIS.Constants_SINGLE SET MHFloodCleanupMajArt = " + textBoxManholeFloodMajorArterialCleanup.Text;
                        break;
                   case "textBoxManholeFloodNearSchool":
                        query = "UPDATE GIS.Constants_SINGLE SET MHFloodNearSchoolCost = " + textBoxManholeFloodNearSchool.Text;
                        break;
                   case "textBoxManholeFloodStreetCleanup":
                        query = "UPDATE GIS.Constants_SINGLE SET MHFloodCleanupStreet = " + textBoxManholeFloodStreetCleanup.Text;
                        break;
                   case "textBoxPublicBuildingProximityCost":
                        query = "UPDATE GIS.Constants_SINGLE SET PublicBldgProximityCost = " + textBoxPublicBuildingProximityCost.Text;
                        break;
                   case "textBoxRawCostENR":
                        query = "UPDATE GIS.Constants_SINGLE SET RawCostENR = " + textBoxRawCostENR.Text;
                        break;
                   case "textBoxRegulatoryFine":
                        query = "UPDATE GIS.Constants_SINGLE SET RegulatoryFine = " + textBoxRegulatoryFine.Text;
                        break;
                   case "textBoxSinkholeFreewayCost":
                        query = "UPDATE GIS.Constants_SINGLE SET SinkholeFreewayCost = " + textBoxSinkholeFreewayCost.Text;
                        break;
                   case "textBoxSinkholeMajorArterialCost":
                        query = "UPDATE GIS.Constants_SINGLE SET SinkholeMajorArterialCost = " + textBoxSinkholeMajorArterialCost.Text;
                        break;
                   case "textBoxSinkholeResidentialCost":
                        query = "UPDATE GIS.Constants_SINGLE SET SinkholeResidentialCost = " + textBoxSinkholeResidentialCost.Text;
                        break;
                   case "textBoxTrafficCostPerVehiclePerDay":
                        query = "UPDATE GIS.Constants_SINGLE SET TrafficImpactCostPerVehiclePerDay = " + textBoxTrafficCostPerVehiclePerDay.Text;
                        break;
                   case "textBoxUtilityCrossingCost":
                        query = "UPDATE GIS.Constants_SINGLE SET UtilityCrossingCost = " + textBoxUtilityCrossingCost.Text;
                        break;
                   default:
                        query = "";
                        break;
                }
                        

                string connectionString = global::RXI.Properties.Settings.Default.REHABConnectionString;
                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                   System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);

                   conn.Open();
                   cmd.ExecuteNonQuery();
                   conn.Close();
                }
           }
        }

        private void checkBoxChangeReplacementCost_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxChangeReplacementCost.Checked == true)
            {
                textBoxReplacementCost.Enabled = true;
            }
            else
            {
                textBoxReplacementCost.Enabled = false;
            }
        }

        private void checkBoxChangeConsequenceOfFailure_CheckedChanged(object sender, EventArgs e)
        {
            if ( checkBoxChangeConsequenceOfFailure.Checked == true)
            {
                textBoxConsequenceOfFailure.Enabled = true;
            }
            else
            {
                textBoxConsequenceOfFailure.Enabled = false;
            }
        }

        private void resetCostsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'rEHABDataSet.Constants' table. You can move, or remove it, as needed.
            this.constantsTableAdapter.PrepFill(this.rEHABDataSet.Constants);

            // TODO: This line of code loads data into the 'rEHABDataSet.REHAB10FTSEGS' table. You can move, or remove it, as needed.
            button1_Click(null, null);
        }
    }
}
