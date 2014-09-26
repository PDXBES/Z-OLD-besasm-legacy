using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
  public class LoadRehabPackage
  {
    public string SegmentsDBFileName { get; private set; }
    public string SegmentsTableName { get; private set; }
    public string ConflictsDBFileName { get; private set; }
    public string ConflictsTableName { get; private set; }
    public int NumRecords { get; private set; }

    public LoadRehabPackage(
      string segmentsDBFileName,
      string segmentsTableName,
      string conflictsDBFileName,
      string conflictsTableName,
      int numRecords)
    {
      SegmentsDBFileName = segmentsDBFileName;
      SegmentsTableName = segmentsTableName;
      ConflictsDBFileName = conflictsDBFileName;
      ConflictsTableName = conflictsTableName;
      NumRecords = numRecords;
    }
  }
}
