using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Modeling.ModelResults;
using ESRI.ArcGIS.Carto;

namespace BREPrototype
{
    public partial class Form1 : Form
    {
        public const int yearEX = 2005; //Existing conditions
        public const int yearFU = 2040; //Future condition model
        public const int yearPlanningHorizon = 2105;
        public const int bfBREID = 4;   
        public const int bfCost = 5000;
        public const int bfDeltaHGLThreshold = 8;

        private Links mstLinks;

        SelectPipesViaTrace selectPipesViaTrace;            

        //IDictionary<int, DscHydraulics> dscResults;
        int[] modelScenarios = { 1, 2, 3, 4 };

        private class DscResult
        {
            public int dscID;
            public int mLinkID;
            public double deltaHGL_EX02, deltaHGL_EX05, deltaHGL_EX25, deltaHGL_FU25;
            public DscResult(int dscID, int mLinkID)
            {
                this.dscID = dscID;
                this.mLinkID = mLinkID;
                deltaHGL_EX02 = deltaHGL_EX05 = deltaHGL_EX25 = deltaHGL_FU25 = Double.MaxValue;
            }

        }

        public Form1()
        {
            InitializeComponent();
            Cursor = Cursors.WaitCursor;
            SystemsAnalysis.DataAccess.SAMasterDataSetTableAdapters.MstLinksTableAdapter mstLinksTA;
            mstLinksTA = new SystemsAnalysis.DataAccess.SAMasterDataSetTableAdapters.MstLinksTableAdapter();
            mstLinksTA.Fill(saMasterDS.MstLinks);
            mstLinks = new Links(saMasterDS.MstLinks);
            breDS.CostPerLevelOfService.AddCostPerLevelOfServiceRow(0, yearEX, 0);
            //breDS.CostPerLevelOfService.AddCostPerLevelOfServiceRow(0, yearPlanningHorizon, 0);
            breDS.CostPerComponent.AddCostPerComponentRow(0, 0, yearEX, 0);
            //breDS.CostPerComponent.AddCostPerComponentRow(0, 0, yearPlanningHorizon, 0);
            selectPipesViaTrace = new SelectPipesViaTrace(mstLinks);
            
            Cursor = Cursors.Default;
        }

        private void CalculateBRE(Links links, bool byComponent)
        {          
            SystemsAnalysis.DataAccess.SAMasterDataSetTableAdapters.MstDscTableAdapter mstDscTA;
            mstDscTA = new SystemsAnalysis.DataAccess.SAMasterDataSetTableAdapters.MstDscTableAdapter();
            string connString = mstDscTA.Connection.ConnectionString;
            System.Data.SqlClient.SqlDataReader dscHydraulicsReader;
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connString);
            System.Data.SqlClient.SqlCommand selCmd;
            string sql;
            conn.Open();

            //double deltaHGL_EX02, deltaHGL_EX05, deltaHGL_EX25, deltaHGL_FU25;
            //deltaHGL_EX02 = deltaHGL_EX05 = deltaHGL_EX25 = deltaHGL_FU25 = Double.MaxValue;
            string mLinkList = GetMLinkIDList(links);

            sql = "SELECT ModelAdmin.MST_LINKS.MLinkID, ModelAdmin.SP_DSCHYDRAULICS.DSCHYDRAULICSID, ModelAdmin.SP_DSCHYDRAULICS.SCENARIOID, " +
                    "ModelAdmin.SP_DSCHYDRAULICS.DSCID, ModelAdmin.SP_DSCHYDRAULICS.MODELID, ModelAdmin.SP_DSCHYDRAULICS.HGL, " +
                    "ModelAdmin.SP_DSCHYDRAULICS.DELTAHGL, ModelAdmin.SP_DSCHYDRAULICS.SURCHARGE " +
                    "FROM  ModelAdmin.MST_LINKS INNER JOIN " +
                    "ModelAdmin.MST_DSC ON ModelAdmin.MST_LINKS.MLinkID = ModelAdmin.MST_DSC.TOMLINKSAN INNER JOIN " +
                    "ModelAdmin.SP_DSCHYDRAULICS ON ModelAdmin.MST_DSC.DSCID = ModelAdmin.SP_DSCHYDRAULICS.DSCID Where MLinkID IN (" + mLinkList + ")";
            selCmd = new System.Data.SqlClient.SqlCommand(sql, conn);
            selCmd.CommandType = System.Data.CommandType.Text;
            dscHydraulicsReader = selCmd.ExecuteReader();
            Dictionary<int, DscResult> DscResults;
            DscResults = new Dictionary<int, DscResult>();

            while (dscHydraulicsReader.Read())
            {
                int scenarioID = (int)dscHydraulicsReader["SCENARIOID"];
                int mLinkID = (int)dscHydraulicsReader["MLINKID"];
                int dscID = (int)dscHydraulicsReader["DSCID"];
                double deltaHGL = Convert.ToDouble(dscHydraulicsReader["DeltaHGL"]);
                
                DscResult dscResult;
                if (!DscResults.ContainsKey(dscID))
                {
                    dscResult = new DscResult(dscID, mLinkID);
                    DscResults.Add(dscID, dscResult);
                }
                else
                {
                    dscResult = DscResults[dscID];
                }
               
                switch (scenarioID)
                {
                    case 1:
                        dscResult.deltaHGL_EX02 = deltaHGL;
                        break;
                    case 2:
                        dscResult.deltaHGL_EX05 = deltaHGL;
                        break;
                    case 3:
                        dscResult.deltaHGL_EX25 = deltaHGL;
                        break;
                    case 4:
                        dscResult.deltaHGL_FU25 = deltaHGL;
                        break;
                }                
            }

            breDS.CostPerComponent.Clear();
            breDS.CostPerLevelOfService.Clear();
            foreach (DscResult dscResult in DscResults.Values)
            {
                int mLinkID = dscResult.mLinkID;

                for (int yearFromEX = 0; yearFromEX <= yearPlanningHorizon - yearEX; yearFromEX += GetYearDelta(yearFromEX))
                {
                    double breCost;
                    breCost = CalculateBasementFloodingBRE(dscResult, yearFromEX);//deltaHGL_EX02, deltaHGL_EX05, deltaHGL_EX25, deltaHGL_FU25, yearFromEX);

                    int year;
                    year = yearEX + yearFromEX;
                    if (byComponent)
                    {
                        BREDataSet.CostPerComponentRow componentRow = breDS.CostPerComponent.FindByMLinkIDLevelOfServiceIDYear(mLinkID, bfBREID, year);
                        if (componentRow == null)
                        {
                            breDS.CostPerComponent.AddCostPerComponentRow(mLinkID, bfBREID, year, breCost);
                        }
                        else
                        {
                            componentRow.BRECost += breCost;
                        }
                    }
                    BREDataSet.CostPerLevelOfServiceRow losRow = breDS.CostPerLevelOfService.FindByLevelOfServiceIDYear(bfBREID, year);
                    if (losRow == null)
                    {
                        breDS.CostPerLevelOfService.AddCostPerLevelOfServiceRow(bfBREID, year, breCost);
                    }
                    else
                    {
                        losRow.BRECost += breCost;
                    }                    
                }
            }        
            chartByComponent.Refresh();                                             
            return;
        }
        private double CalculateBasementFloodingBRE(DscResult dscResult, int yearFromEX)
        {
            double breCost = 0;
            double deltaHGLIncrease = Math.Max((dscResult.deltaHGL_EX25 - dscResult.deltaHGL_FU25) / (yearFU - yearEX), 0) 
                * Math.Min(yearFromEX, yearFU - yearEX);            

            if ((dscResult.deltaHGL_EX02 - deltaHGLIncrease) <= bfDeltaHGLThreshold)
            {
                breCost = 0.5 * bfCost;
            }
            else if ((dscResult.deltaHGL_EX05 - deltaHGLIncrease) <= bfDeltaHGLThreshold)
            {
                breCost = 0.2 * bfCost;
            }
            else if ((dscResult.deltaHGL_EX25 - deltaHGLIncrease) <= bfDeltaHGLThreshold)
            {
                breCost = 0.04 * bfCost;
            }
            return breCost;
        }

        /*private void AddNewBREEntry(Link l)
        {
            SystemsAnalysis.DataAccess.SAMasterDataSetTableAdapters.MstDscTableAdapter mstDscTA;
            mstDscTA = new SystemsAnalysis.DataAccess.SAMasterDataSetTableAdapters.MstDscTableAdapter();
            string connString = mstDscTA.Connection.ConnectionString;
            System.Data.SqlClient.SqlDataReader dscHydraulicsReader;
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connString);
            System.Data.SqlClient.SqlCommand selCmd;
            string sql;
            conn.Open();

            double deltaHGL_EX02, deltaHGL_EX05, deltaHGL_EX25, deltaHGL_FU25;
            deltaHGL_EX02 = deltaHGL_EX05 = deltaHGL_EX25 = deltaHGL_FU25 = Double.MaxValue;

            sql = "SELECT ModelAdmin.MST_LINKS.MLinkID, ModelAdmin.SP_DSCHYDRAULICS.DSCHYDRAULICSID, ModelAdmin.SP_DSCHYDRAULICS.SCENARIOID, " +
                    "ModelAdmin.SP_DSCHYDRAULICS.DSCID, ModelAdmin.SP_DSCHYDRAULICS.MODELID, ModelAdmin.SP_DSCHYDRAULICS.HGL, " +
                    "ModelAdmin.SP_DSCHYDRAULICS.DELTAHGL, ModelAdmin.SP_DSCHYDRAULICS.SURCHARGE " +
                    "FROM  ModelAdmin.MST_LINKS INNER JOIN " +
                    "ModelAdmin.MST_DSC ON ModelAdmin.MST_LINKS.MLinkID = ModelAdmin.MST_DSC.TOMLINKSAN INNER JOIN " +
                    "ModelAdmin.SP_DSCHYDRAULICS ON ModelAdmin.MST_DSC.DSCID = ModelAdmin.SP_DSCHYDRAULICS.DSCID Where MLinkID = " + l.MLinkID;
            selCmd = new System.Data.SqlClient.SqlCommand(sql, conn);
            selCmd.CommandType = System.Data.CommandType.Text;

            dscHydraulicsReader = selCmd.ExecuteReader();
            try
            {                
                while (dscHydraulicsReader.Read())
                {
                    int scenarioID = (int)dscHydraulicsReader["SCENARIOID"];
                    int mLinkID = (int)dscHydraulicsReader["MLINKID"];
                    int dscID = (int)dscHydraulicsReader["DSCID"];
                    double deltaHGL = Convert.ToDouble(dscHydraulicsReader["DeltaHGL"]);
                    switch (scenarioID)
                    {
                        case 1:
                            deltaHGL_EX02 = deltaHGL;
                            break;
                        case 2:
                            deltaHGL_EX05 = deltaHGL;
                            break;
                        case 3:
                            deltaHGL_EX25 = deltaHGL;
                            break;
                        case 4:
                            deltaHGL_FU25 = deltaHGL;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                dscHydraulicsReader.Close();
            }
                        
            for (int yearFromEX = 0; yearFromEX <= yearFU - yearEX; yearFromEX++)
            {
                double breCost;
                breCost = CalculateBasementFloodingBRE(deltaHGL_EX02, deltaHGL_EX05, deltaHGL_EX25, deltaHGL_FU25, yearFromEX);

                int year;
                year = yearEX + yearFromEX;

                BREDataSet.CostPerComponentRow componentRow = breDS.CostPerComponent.FindByMLinkIDLevelOfServiceIDYear(l.MLinkID, bfBREID, year);
                if (componentRow == null)
                {
                    breDS.CostPerComponent.AddCostPerComponentRow(l.MLinkID, bfBREID, year, breCost);
                }
                else
                {
                    componentRow.BRECost += breCost;
                }
            }
        }*/        

        private void btnComputeComponent_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Links links = new Links();
                foreach (Infragistics.Win.UltraWinGrid.UltraGridRow row in ultraGrid1.Selected.Rows)
                {
                    int mlinkID = (int)row.Cells["MLinkID"].Value;
                    Link l = mstLinks.FindByMLinkID(mlinkID);
                    if (l != null) links.Add(l);
                }
                CalculateBRE(links, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            return;
        }

        private void btnComputeLOS_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Links links = new Links();
                foreach (Infragistics.Win.UltraWinGrid.UltraGridRow row in ultraGrid1.Selected.Rows)
                {
                    int mlinkID = (int)row.Cells["MLinkID"].Value;
                    Link l = mstLinks.FindByMLinkID(mlinkID);
                    if (l != null) links.Add(l);
                }
                CalculateBRE(links, false);
                tabChartSelect.SelectedTab = tabChartSelect.Tabs[1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            return;
        }
        
        private string GetMLinkIDList(Links links)
        {
            string mLinkList = "";
            DataRow[] rows;
            foreach (Link l in links.Values)
            {
                mLinkList = mLinkList + l.MLinkID + ", ";
            }
            mLinkList = mLinkList.TrimEnd(", ".ToCharArray());
            return mLinkList;
        }

        private int GetYearDelta(int yearFromEx)
        {
            if (yearFromEx < 10)
            {
                return 1;
            }
            else
            {
                return 10;
            }
        }

        private void btnTrace_Click(object sender, EventArgs e)
        {
            if (selectPipesViaTrace.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Cursor = Cursors.WaitCursor;
            Links links = selectPipesViaTrace.GetTrace();

            string mLinkIDList = GetMLinkIDList(links);
            foreach (Infragistics.Win.UltraWinGrid.UltraGridRow row in ultraGrid1.Rows)
            {
                int mlinkID = (int)row.Cells["MLinkID"].Value;
                if (links.FindByMLinkID(mlinkID) != null)
                {
                    row.Selected = true;
                }
                else
                {
                    row.Selected = false;
                }
            }
            ultraGrid1.Refresh();
            Cursor = Cursors.Default;
        }        

        private void chartByComponent_FillSceneGraph(object sender, Infragistics.UltraChart.Shared.Events.FillSceneGraphEventArgs e)
        {
            int count = breDS.CostPerComponent.Count;
            if (count > 0)
            {
                chartByComponent.Axis.Y.RangeMax = breDS.CostPerComponent[count - 1].BRECost;
            }
        }


    }
}