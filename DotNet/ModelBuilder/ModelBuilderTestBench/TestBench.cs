using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.ModelConstruction;
using SystemsAnalysis.Utils.AccessUtils;
using SystemsAnalysis.ModelConstruction.TestBench;
using System.IO;

namespace SystemsAnalysis.ModelConstruction.TestBench
{
    class TestBench
    {      
        static void Main(string[] args)
        {
            try
            {
                TestClass test;
                Console.WriteLine("Entered Testbench");
                test = new TestClass();
                Console.WriteLine("Created TestClass");
                test.TestLinkTables();
                Console.WriteLine("Returned from TestLinkTables");

                test = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());                
            }
            finally
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }

        }

       
    }
}
