using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Utils.AccessUtils;
using SystemsAnalysis.Modeling.ModelUtils.ResultsExtractor;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.ModelConstruction;
using System.Data.Linq;
using System.IO;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Linq;

namespace SystemsAnalysis.Reporting.ReportLibraries
{
  public class DesignDiversionFlowReport : AlternativeReport //ReportBase
  {
    const double GPM_PER_CFS = 0.00222800926;

    private string modelPath;
    private string alternativePath;
    private string swmmOutputFile;

    public AlternativeReport alternativeReport;

    public DesignDiversionFlowReport()
    {

    }
   }
}
  
