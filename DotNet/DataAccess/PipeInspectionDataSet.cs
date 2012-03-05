using System.Collections;

namespace SystemsAnalysis.DataAccess {

    partial class PipeInspectionDataSet
    {
        partial class PipeInspectionGradesDataTable
        {
            private Hashtable mLinkIDList;

            public PipeInspectionDataSet.PipeInspectionGradesRow FindByMLinkID(int MLinkID)
            {
                if (mLinkIDList == null)
                {
                    mLinkIDList = new Hashtable();
                    foreach (PipeInspectionDataSet.PipeInspectionGradesRow row in this.Rows)
                    {
                        if (!mLinkIDList.Contains(MLinkID) && !row.IsMLinkIDNull())
                        {                            
                            mLinkIDList.Add(row.MLinkID, row);
                        }
                    }
                }
                return ((PipeInspectionDataSet.PipeInspectionGradesRow)mLinkIDList[MLinkID]);
            }            
        }
    }
}
