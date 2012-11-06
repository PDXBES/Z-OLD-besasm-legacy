using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemsAnalysis.Tracer;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.DataAccess;

namespace SystemsAnalysis.Tracer
{
    public partial class FrmTestTracerInterface : Form
    {
        private FrmTraceLinks frmTraceLinks;

        public FrmTestTracerInterface()
        {
            InitializeComponent();

            Links mstLinks;
            mstLinks = new Links();
            DataAccess.SAMasterDataSetTableAdapters.MstLinksTableAdapter mstLinksTA;
            mstLinksTA = new DataAccess.SAMasterDataSetTableAdapters.MstLinksTableAdapter();
            DataAccess.SAMasterDataSet.MstLinksDataTable mstLinksDataTable;
            mstLinksDataTable = new DataAccess.SAMasterDataSet.MstLinksDataTable();
            mstLinksTA.Fill(mstLinksDataTable);
            mstLinks = new Links(mstLinksDataTable);
            
            frmTraceLinks = new FrmTraceLinks(mstLinks,
                Application.StartupPath + "\\lyr\\mst_links.lyr", "USNode", "DSNode");

            frmTraceLinks.AddReferenceLayer(Application.StartupPath + "\\lyr\\cgis_streets.lyr", 1);
            frmTraceLinks.AddReferenceLayer(Application.StartupPath + "\\lyr\\sewer_basins.lyr", 2);
            frmTraceLinks.AddReferenceLayer(Application.StartupPath + "\\lyr\\mst_nodes.lyr", 3);
            
        }

        private void btnTraceLinks_Click(object sender, EventArgs e)
        {
            frmTraceLinks.ShowDialog();
            if (frmTraceLinks.DialogResult == DialogResult.OK)
            {
                MessageBox.Show("Traced " + frmTraceLinks.GetTracedLinks().Count + " links!");
            }
            else
            {
                MessageBox.Show("Trace canceled.");
            }
        }
    }
}