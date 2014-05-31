using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling.ModelUtils.ResultsExtractor;
using System.Data;

namespace ResultsExtractorTestBench
{
    class Program
    {
        static void Main(string[] args)
        {
          //XPSWMMResults xpResults = new XPSWMMResults(@"C:\Data\Projects\41800023-1 BES_DataManagement\TGD_FU_2011\sim\25");
          XPSWMMResults xpResults = new XPSWMMResults(@"c:\temp\BEE_NP-Pipe_FU_4S6.out");

            //TableE19DataSet.TableE19DataTable tableE19 = xpResults.GetTableE19();

            //foreach (TableE19DataSet.TableE19Row row in tableE19)
            //{
            //    Console.WriteLine(row.NodeName);
            //}

            xpResults.WriteToAccessDatabase(@"C:\temp\XPSWMMResults.mdb");
        }
    }
}
