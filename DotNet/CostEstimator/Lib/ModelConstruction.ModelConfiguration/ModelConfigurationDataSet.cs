using System.Collections.Generic;
using System.IO;

namespace SystemsAnalysis.ModelConstruction
{

    partial class ModelConfigurationDataSet
    {
        private string modelConfigFile;
        private string modelRoot;

        /// <summary>
        /// Constructor for the ConfigurationHelper class. If a model configuration
        /// file (Model.xml) exists in the model root directory, it is opened for
        /// use by the ConfigurationHelper. If it does not exist, it is created
        /// from the default configuration file embedded in this assembly.
        /// </summary>
        /// <param name="modelPath">A full path to the root directory of a model</param>        
        public ModelConfigurationDataSet(string modelRoot)
            : this()
        {
            if (!modelRoot.EndsWith("\\"))
            {
                modelRoot += "\\";
            }

            if (!Directory.Exists(modelRoot))
            {
                throw new DirectoryNotFoundException();
            }

            this.modelRoot = modelRoot;
            this.modelConfigFile = modelRoot + "Model.xml";


            //this = new ModelConfigurationDataSet();

            if (File.Exists(modelConfigFile))
            {
                this.ReadXml(modelConfigFile);
            }
            else
            {
                CreateNewConfigFileFromTemplate();
            }

            if (this.DataAccess.Count == 0)
            {
                Stream configStream;

                configStream = GetType().Assembly.GetManifestResourceStream(
                    "SystemsAnalysis.ModelConstruction.ModelConfigurationTemplate.xml");

                ModelConfigurationDataSet templateDS = new ModelConfigurationDataSet();
                templateDS.ReadXml(configStream);

                this.DataAccess.ImportRow(templateDS.DataAccess[0]);
                foreach (ModelConfigurationDataSet.DataSourceRow dr in templateDS.DataAccess[0].GetDataSourceRows())
                {
                    this.DataSource.ImportRow(dr);
                }
                this.WriteXml(modelRoot + "Model.xml");
            }
        }

        /// <summary>
        /// Overwrites an existing configuration file (Model.xml) with the current 
        /// version, which is stored as embedded content in this assembly.
        /// </summary>
        public void CreateNewConfigFileFromTemplate()
        {
            if (File.Exists(modelConfigFile))
            {
                File.Delete(modelConfigFile);
            }

            Stream configStream;

            configStream = GetType().Assembly.GetManifestResourceStream(
                "SystemsAnalysis.ModelConstruction.ModelConfigurationTemplate.xml");

            this.Clear();
            this.ReadXml(configStream);
            this.WriteXml(modelConfigFile);
        }

        /// <summary>
        /// Gets the names of all the required model databases as read from the Model.xml
        /// configuration file.
        /// </summary>
        /// <returns>A StringDictionary containing a collection of Database Names and 
        /// Database Paths. 
        public Dictionary<string, string> GetRequiredDatabases()
        {
            Dictionary<string, string> requiredDatabases;
            requiredDatabases = new Dictionary<string, string>();

            foreach (ModelConfigurationDataSet.ModelDatabaseRow accessDBRow in this.RequiredModelDatabases[0].GetModelDatabaseRows())
            {
                string databaseName = accessDBRow.DatabaseName;
                string databasePath = modelRoot + "mdbs\\" + databaseName + ".mdb";
                requiredDatabases.Add(databaseName, databasePath);
            }
            return requiredDatabases;
        }

        /// <summary>
        /// Gets the names and paths of all the linked tables in an EMGAATS database. 
        /// </summary>
        /// <param name="databaseName">The name of an EMGAATS database. The name should not include
        /// the path or the file extension. This database must be specified in the Model.xml file
        /// and the database will typically be located in a models \mdbs directory.</param>
        /// <returns>A StringDictionary containing a collection of Table Names and Database Paths. 
        /// The Table Name is the Key and the Database Path is the Value. The Table Name refers to
        /// the actual name of a table in it's source database, and the Database Path refers to the\
        /// full path of the source database containing the linked table.
        /// </returns>
        public Dictionary<string, string> GetLinkedTableSources(string databaseName)
        {
            Dictionary<string, string> linkedFiles;
            linkedFiles = new Dictionary<string, string>();

            //accessHelper.OpenDatabase(databaseName);
            foreach (ModelConfigurationDataSet.ModelDatabaseRow accessDBRow in this.RequiredModelDatabases[0].GetModelDatabaseRows())
            {
                if (accessDBRow.DatabaseName.ToUpper() != databaseName.ToUpper())
                {
                    continue;
                }
                foreach (ModelConfigurationDataSet.LinkedTableRow linkRow in accessDBRow.GetLinkedTableRows())
                {
                    string tableName = linkRow.Link;
                    string linkDatabase = modelRoot;

                    linkDatabase += linkRow.ModelDataSourceRow.RelativePath;
                    linkDatabase += linkRow.ModelDataSourceRow.Database;

                    linkedFiles.Add(tableName, linkDatabase);
                }
            }
            return linkedFiles;
        }

        public int AddAlternativeRow(string alternativeName, string baseModel)
        {
            if (this.IncludedAlternatives.Count == 0)
            {
                this.IncludedAlternatives.AddIncludedAlternativesRow();
            }

            //Copy alternative info from base model
            //ConfigurationHelper baseModelHelper = new ConfigurationHelper(baseModel);
            //foreach (ModelConfigurationDataSet.AlternativeRow row in baseModelHelper.GetIncludedAlternatives())
            //{
            //    this.Alternative.AddAlternativeRow(row.Name, row.BaseModel, this.IncludedAlternatives[0]);
            //}

            //Add alternative info from the current model
            this.Alternative.AddAlternativeRow(alternativeName, baseModel, this.IncludedAlternatives[0]);
            ModelConfigurationDataSet.AlternativeRow[] rows;
            this.WriteXml(modelConfigFile);
            rows = this.IncludedAlternatives[0].GetAlternativeRows();

            return rows[rows.Length - 1].AltID;
        }

        public ModelConfigurationDataSet.AlternativeRow[] GetIncludedAlternatives()
        {
            ModelConfigurationDataSet.AlternativeRow[] rows;
            rows = new ModelConfigurationDataSet.AlternativeRow[this.Alternative.Count];
            int i = 0;
            foreach (System.Data.DataRow row in this.Alternative.Rows)
            {
                rows[i] = (ModelConfigurationDataSet.AlternativeRow)row;
                i++;
            }
            return (rows);
        }

        public string GetDataAccessFileName()
        {
            return this.DataAccess[0].FileName;
        }

        public Dictionary<string, string[]> GetDataAccessTables()
        {
            ModelConfigurationDataSet.DataSourceRow[] rows;
            rows = this.DataAccess[0].GetDataSourceRows();
            Dictionary<string, string[]> dataAccessDictionary = new Dictionary<string, string[]>();

            foreach (ModelConfigurationDataSet.DataSourceRow row in rows)
            {
                dataAccessDictionary.Add(row.LinkName, new string[] { row.TableName, row.TableLocation });
            }
            return dataAccessDictionary;
        }
    }
}
