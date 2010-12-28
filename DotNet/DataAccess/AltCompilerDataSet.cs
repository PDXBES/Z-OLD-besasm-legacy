namespace SystemsAnalysis.DataAccess 
{
      
    public partial class AltCompilerDataSet 
    {
      partial class SP_RP_BSBRDataTable
      {
      }
    
      public void InitAltCompilerDataSet()
      {
        AltCompilerDataSetTableAdapters.SP_RP_BSBRTableAdapter rpBsbrTA;
        rpBsbrTA = new AltCompilerDataSetTableAdapters.SP_RP_BSBRTableAdapter();
        rpBsbrTA.Fill(this.SP_RP_BSBR);
      }
    }
}

namespace SystemsAnalysis.DataAccess.AltCompilerDataSetTableAdapters {
    
    public partial class SP_ALT_BSBRTableAdapter 
    {

    }
}
