namespace SystemsAnalysis.DataAccess {
    
    
    public partial class AltCompilerDataSet 
    {
      public void InitAltCompilerDataSet()
      {
        AltCompilerDataSet altCompilerDS;
        altCompilerDS = new AltCompilerDataSet();
        AltCompilerDataSetTableAdapters.SP_RP_BSBRTableAdapter rpBsbrTA;
        rpBsbrTA = new AltCompilerDataSetTableAdapters.SP_RP_BSBRTableAdapter();
        rpBsbrTA.Fill(altCompilerDS.SP_RP_BSBR);
      }
    }
}

namespace SystemsAnalysis.DataAccess.AltCompilerDataSetTableAdapters {
    
    
    public partial class SP_ALT_BSBRTableAdapter {
    }
}
