namespace SystemsAnalysis.ModelConstruction.AlternativesToolkit {


    partial class AltEngineConfiguration
    {
        private string configPath;

        public AltEngineConfiguration(string configPath)
        {
            this.configPath = configPath;
            
            ReadXml(configPath);            
        }

        public string UpdateVersionPatchMBX
        {
            get { return this.ProgramSettings[0].UpdateVersionPatchMBX; }
        }


        #region Manage Recently Used Files
        public void UpdateBaseModelHistory(string baseModel)
        {
            string newHistory = baseModel;
            string newDate = System.DateTime.Now.ToString("G");

            int historyCount = System.Math.Min(
                this.FileHistory[0].HistoryCount,
                this.BaseModel.Count + 1);

            for (int i = 0; i < historyCount; i++)
            {
                if (this.BaseModel.Count <= i)
                {
                    this.BaseModel.AddBaseModelRow("", "", this.FileHistory[0]);
                }

                AltEngineConfiguration.BaseModelRow historyRow;
                historyRow = this.BaseModel[i];
                string tempHistory = historyRow.IsNull("BaseModel_text")
                    ? "" : historyRow.BaseModel_text;
                string tempDate = historyRow.IsDateNull()
                    ? "" : historyRow.Date;

                if (tempHistory == baseModel)
                {
                    historyRow.Delete();
                    historyCount--;
                    i--;
                    continue;
                }

                historyRow.BaseModel_text = newHistory;
                historyRow.Date = newDate;

                newHistory = tempHistory;
                newDate = tempDate;
            }
            return;
        }

        public void UpdateOutputModelHistory(string outputModel)
        {
            string newHistory = outputModel;
            string newDate = System.DateTime.Now.ToString("G");

            int historyCount = System.Math.Min(
                this.FileHistory[0].HistoryCount,
                this.OutputModel.Count + 1);

            for (int i = 0; i < historyCount; i++)
            {
                if (this.OutputModel.Count <= i)
                {
                    this.OutputModel.AddOutputModelRow("", "", this.FileHistory[0]);
                }

                AltEngineConfiguration.OutputModelRow historyRow;
                historyRow = this.OutputModel[i];
                string tempHistory = historyRow.IsNull("OutputModel_text")
                    ? "" : historyRow.OutputModel_text;
                string tempDate = historyRow.IsDateNull()
                    ? "" : historyRow.Date;

                if (tempHistory == outputModel)
                {
                    historyRow.Delete();
                    historyCount--;
                    i--;
                    continue;
                }

                historyRow.OutputModel_text = newHistory;
                historyRow.Date = newDate;

                newHistory = tempHistory;
                newDate = tempDate;
            }
            return;
        }

        #endregion


        

    }
}
