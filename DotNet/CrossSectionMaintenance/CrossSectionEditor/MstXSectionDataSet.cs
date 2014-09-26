namespace SystemsAnalysis.EMGAATS.CrossSectionEditor 
{
    
    
    public partial class MstXSectionDataSet 
    {
      public partial class mst_xsectionsDataTable
      {
        /// <summary>
        /// Returns a cross-section row that matches the provided mlinkid, or null if not found.
        /// </summary>
        /// <param name="mlinkid"></param>
        /// <returns></returns>
        public mst_xsectionsRow FindByMLinkId(int mlinkid)
        {          
          foreach (MstXSectionDataSet.mst_xsectionsRow row in this)
          {
            if (row.RowState != System.Data.DataRowState.Deleted && row.mlinkid == mlinkid)
              return row;
          }
          return null;
        }
      }
    }
}
