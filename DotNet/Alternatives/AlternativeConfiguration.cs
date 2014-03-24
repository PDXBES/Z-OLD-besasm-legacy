using System.IO;
//using SystemsAnalysis.Utils.MapInfoUtils;

namespace SystemsAnalysis.Modeling.Alternatives {

    partial class AlternativeConfiguration
    {        
        private string configPath;
        public static string MasterVersion = "1.3";

        /// <summary>
        /// Create a new instance of an alternative configuration file. The configuration file will be read from alternative_configuration.xml
        /// </summary>
        /// <param name="configPath"></param>
        public AlternativeConfiguration(string configPath)
            : this()
        {
            this.configPath = configPath;
            string configText;
            StreamReader sr = new StreamReader(configPath);
            configText = sr.ReadToEnd();
            sr.Close();
            if (configText.Contains("AlternativesBuilder")) PatchConfig_1();
            if (configText.Contains("AlternativesToolkit")) PatchConfig_2();
            
            this.ReadXml(configPath, System.Data.XmlReadMode.InferSchema);               
        }

        public void UpdateVersion(string newVersion)
        {            
            this.tableAlternativeConfiguration[0].Version = newVersion;            
            this.AddHistory("Converted to version " + newVersion);
            this.WriteXml(configPath);
        }

        private void PatchConfig_1()
        {
            //Patch code - AlternativeConfiguration has been refactored to SystemsAnalysis.Modeling.Alternatives.
            //Any old config files should have their schema reference updated to the new namespace.   
            StreamReader sr = new StreamReader(configPath);
            string configText = sr.ReadToEnd();
            
            System.Collections.Specialized.StringCollection sc;
            sc = new System.Collections.Specialized.StringCollection();
            while (true)
            {
                string s = sr.ReadLine();
                if (s == null)
                {
                    break;
                }
                sc.Add(s);
            }
            sr.Close();

            StreamWriter configWriter = new StreamWriter(configPath);

            foreach (string s in sc)
            {
                if (s.Contains("<Name>"))
                {
                    configWriter.WriteLine("    <Version>1.1</Version>");
                }
                if (s.Contains("AlternativesBuilder"))
                {
                    configWriter.WriteLine(s.Replace("AlternativesBuilder", "AlternativesToolkit"));
                }
                else
                {
                    configWriter.WriteLine(s);
                }
            }
            configWriter.Close();
            this.ReadXml(configPath, System.Data.XmlReadMode.InferSchema);
            this.AddHistory("Patched NameSpace from AlternativesBuilder to AlternativesToolkit");
        }
        private void PatchConfig_2()
        {
            //Patch code - AlternativeConfiguration has been refactored to SystemsAnalysis.Modeling.Alternatives.
            //Any old config files should have their schema reference updated to the new namespace.    
            StreamReader sr = new StreamReader(configPath);

            System.Collections.Specialized.StringCollection sc;
            sc = new System.Collections.Specialized.StringCollection();
            while (true)
            {
                string s = sr.ReadLine();
                if (s == null)
                {
                    break;
                }
                sc.Add(s);
            }
            sr.Close();

            StreamWriter configWriter = new StreamWriter(configPath);

            foreach (string s in sc)
            {
                if (s.Contains("SystemsAnalysis.ModelConstruction.AlternativesToolkit.AlternativeConfiguration"))
                {
                    configWriter.WriteLine(s.Replace("SystemsAnalysis.ModelConstruction.AlternativesToolkit.AlternativeConfiguration", "SystemsAnalysis.Modeling.Alternatives"));
                }
                else
                {
                    configWriter.WriteLine(s);
                }
            }
            configWriter.Close();
            this.ReadXml(configPath, System.Data.XmlReadMode.InferSchema);
            this.AddHistory("Updated configuration namespace to SystemsAnalysis.Modeling.Alternatives.");
        }


        /// <summary>
        /// Create a new instance of an alternative configuration file. The configuration file will be created from scratch if it does not exist and written to alternative_configuration.xml
        /// </summary>
        /// <param name="alternativeName"></param>
        /// <param name="baseModel"></param>
        public AlternativeConfiguration(string alternativeName, string baseModel)
            : this()
        {
            this.configPath = baseModel + "\\alternatives\\" + alternativeName + "\\alternative_configuration.xml";
            if (File.Exists(configPath))
            {
                this.ReadXml(configPath, System.Data.XmlReadMode.InferSchema);
                if (baseModel != this.tableAlternativeConfiguration[0].BaseModel)
                {
                    this.AddHistory("Path changed from: " + this.tableAlternativeConfiguration[0].BaseModel + " to: " + baseModel);
                    this.tableAlternativeConfiguration[0].BaseModel = baseModel;
                    this.WriteXml(configPath);
                }
                if (alternativeName != this.tableAlternativeConfiguration[0].Name)
                {
                    this.AddHistory("Alternative copied from: " + this.tableAlternativeConfiguration[0].Name + " to: " + alternativeName);
                    this.tableAlternativeConfiguration[0].Name = alternativeName;
                    this.WriteXml(configPath);
                }

            }
            else
            {
                this.tableAlternativeConfiguration.AddAlternativeConfigurationRow(alternativeName, baseModel, "", "1.3");//AlternativeConfiguration.Version);
                this.WriteXml(configPath);
            }
            /*this.AlternativeConfiguration.AddAlternativeConfigurationRow(
                    alternativeName, baseModel, this.txtDefaultNodeSuffix.Text);*/

        }

        public AlternativeConfiguration(string alternativeName, string baseModel, string DefaultNodeSuffix)
            : this(alternativeName, baseModel)
        {
            this.DefaultNodeSuffix = DefaultNodeSuffix;
        }

        public void AddHistory(string action)
        {
            this.History.AddHistoryRow(
                System.DateTime.Now.ToString("G"),
                System.Environment.UserName,
                action);
            this.WriteXml(configPath);
        }

        public string DefaultNodeSuffix
        {
            get
            {
                if (this.tableAlternativeConfiguration.Count != 0)
                {
                    return this.tableAlternativeConfiguration[0].DefaultNodeSuffix;
                }
                else
                {
                    return (string)this.tableAlternativeConfiguration.DefaultNodeSuffixColumn.DefaultValue;
                }
            }
            set
            {
                if (this.tableAlternativeConfiguration.Count != 0)
                {
                    this.tableAlternativeConfiguration[0].DefaultNodeSuffix = value;
                    this.WriteXml(configPath);
                }

            }
            /*alternativePath += alternativePath.EndsWith("\\") ? "" : "\\";
            if (File.Exists(alternativePath + "alternative_configuration.xml"))
            {
                //AltConfigFile altConfig = new AltConfigFile();
                //altConfig.ReadXml(alternativePath + "alternative_configuration.xml");
                return altConfig
            }
            return "";*/
        }

        public string BaseModel
        {
            get { return this.tableAlternativeConfiguration[0].BaseModel; }
            //set { this.baseModel = value; }
        }

        public string AlternativeName
        {
            get { return this.tableAlternativeConfiguration[0].Name; }
            //set { this.baseModel = value; }
        }

        public string AlternativeVersion
        {
            get { return this.tableAlternativeConfiguration[0].Version; }
        }

        public class AlternativeVersionChangedException : System.Exception
        {

        }

    }
}
