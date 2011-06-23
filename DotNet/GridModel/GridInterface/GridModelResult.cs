using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SystemsAnalysis.Grid.GridAnalysis
{
    public class GridModelResult
    {
        private string resultFile;
        private string area, subArea, modelDescription, timeStepDescription;
        private List<PollutantLoad> pollutantLoads;
        
        public GridModelResult(string resultFile, string area, string subArea, string modelDescription, string timeStepDescription)
            :this()
        {
            this.resultFile = resultFile;
            this.area = area;
            this.subArea = subArea;
            this.modelDescription = modelDescription;
            this.timeStepDescription = timeStepDescription;            
        }
        public GridModelResult()
        {            
            this.pollutantLoads = new List<PollutantLoad>();
        }
        
        public string Area { get { return area; } set { area = value; } }
        public string SubArea { get { return subArea; } set { subArea = value; }  }
        public string TimeStepDescription { get { return timeStepDescription; } set { timeStepDescription = value; } }
        public string ModelDescription { get { return modelDescription; } set { modelDescription = value; } }
        public string ResultFile { get { return resultFile; } set { resultFile = value; } }
        public List<PollutantLoad> PollutantLoads { get { return pollutantLoads; }  set { pollutantLoads =value; } }
    }
}
