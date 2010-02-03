using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling.ModelResults;

namespace ResultsExtractorTestBench
{
    class Program
    {
        static void Main(string[] args)
        {
            XPSWMMResults xpResults = new XPSWMMResults(@"\\Cassio\Modeling\Projects\9049_62nd&MorrisonRHB\sim\25\9049_25FU_Base.out");

            TableE09DataSet.TableE09DataTable tableE09 = xpResults.GetTableE09();

            foreach (TableE09DataSet.TableE09Row row in tableE09)
            {
                Console.WriteLine(row.NodeName);
            }

            xpResults.WriteToAccessDatabase(@"C:\Results.mdb");
        }
    }
}
