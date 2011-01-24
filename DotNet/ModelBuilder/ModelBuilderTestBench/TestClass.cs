using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.ModelConstruction;
using SystemsAnalysis.Utils.AccessUtils;
using SystemsAnalysis.ModelConstruction.TestBench;
using System.IO;

namespace SystemsAnalysis.ModelConstruction.TestBench
{
    class TestClass
    {        
        public void TestLinkTables()
        {
            AccessHelper accessHelper;
            accessHelper = new AccessHelper();

            Console.WriteLine("Pre-testing AccessHelper");
            string modelRoot = @"D:\Temp\SE_FU_Int_60pct\";
            Console.WriteLine("Open DB...");
            accessHelper.OpenDatabase(modelRoot + @"mdbs\ModelDeployHydraulics.mdb");
            Console.WriteLine("Link Table...");
            accessHelper.LinkTable("mdl_links_ac", modelRoot + @"links\mdl_links_ac.mdb");
            accessHelper = null;

            Console.WriteLine("Deleting Model.xml");
            File.Delete(modelRoot + "Model.xml");
            ModelBuilder modelBuilder;
            Console.WriteLine("Creating new ModelBuilder: " + modelRoot);
            modelBuilder = new ModelBuilder(modelRoot);
            Console.WriteLine("Relinking Databases");
            modelBuilder.RelinkModel();
                                    
        }
    }
}
