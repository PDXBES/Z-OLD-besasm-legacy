namespace SystemsAnalysis.DataAccess {
    
    
    public partial class RetrofitsDataSet
    {
      public void InitRetroDataSet()
      {
        RetrofitsDataSetTableAdapters.SITE_ASSESSMENTTableAdapter siteAssessmentTA;
        siteAssessmentTA = new RetrofitsDataSetTableAdapters.SITE_ASSESSMENTTableAdapter();
        siteAssessmentTA.Fill(tableSITE_ASSESSMENT);

        RetrofitsDataSetTableAdapters.ASSESSMENT_TYPETableAdapter assessmentTypeTA;
        assessmentTypeTA = new RetrofitsDataSetTableAdapters.ASSESSMENT_TYPETableAdapter();
        assessmentTypeTA.Fill(tableASSESSMENT_TYPE);

        RetrofitsDataSetTableAdapters.SITE_OPPORTUNITYTableAdapter siteOpportunityTA;
        siteOpportunityTA = new RetrofitsDataSetTableAdapters.SITE_OPPORTUNITYTableAdapter();
        siteOpportunityTA.Fill(tableSITE_OPPORTUNITY);
        
        RetrofitsDataSetTableAdapters.PROJECTTableAdapter projectTA;
        projectTA = new RetrofitsDataSetTableAdapters.PROJECTTableAdapter();
        projectTA.Fill(tablePROJECT);

        RetrofitsDataSetTableAdapters.PROJECT_STATUSTableAdapter projectStatusTA;
        projectStatusTA = new RetrofitsDataSetTableAdapters.PROJECT_STATUSTableAdapter();
        projectStatusTA.Fill(tablePROJECT_STATUS);

        RetrofitsDataSetTableAdapters.SITETableAdapter siteTA;
        siteTA = new RetrofitsDataSetTableAdapters.SITETableAdapter();
        siteTA.Fill(tableSITE);

        RetrofitsDataSetTableAdapters.DESTINATIONTableAdapter destinationTA;
        destinationTA = new RetrofitsDataSetTableAdapters.DESTINATIONTableAdapter();
        destinationTA.Fill(tableDESTINATION);

        RetrofitsDataSetTableAdapters.FACILITY_TYPETableAdapter facilityTypeTA;
        facilityTypeTA = new RetrofitsDataSetTableAdapters.FACILITY_TYPETableAdapter();
        facilityTypeTA.Fill(tableFACILITY_TYPE);

        RetrofitsDataSetTableAdapters.IMPERVIOUS_AREA_TYPETableAdapter imperviousAreaTypeTA;
        imperviousAreaTypeTA = new RetrofitsDataSetTableAdapters.IMPERVIOUS_AREA_TYPETableAdapter();
        imperviousAreaTypeTA.Fill(tableIMPERVIOUS_AREA_TYPE);

        RetrofitsDataSetTableAdapters.OPPORTUNITY_FEASIBILITYTableAdapter opportunityFeasibilityTA;
        opportunityFeasibilityTA = new RetrofitsDataSetTableAdapters.OPPORTUNITY_FEASIBILITYTableAdapter();
        opportunityFeasibilityTA.Fill(tableOPPORTUNITY_FEASIBILITY);
        
        RetrofitsDataSetTableAdapters.RETRO_BASINTableAdapter retroBasinTA;
        retroBasinTA = new RetrofitsDataSetTableAdapters.RETRO_BASINTableAdapter();
        retroBasinTA.Fill(tableRETRO_BASIN); 

        RetrofitsDataSetTableAdapters.SEWER_BASINTableAdapter sewerBasinTA;
        sewerBasinTA = new RetrofitsDataSetTableAdapters.SEWER_BASINTableAdapter();
        sewerBasinTA.Fill(tableSEWER_BASIN);
      }
    }
}
