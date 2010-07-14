using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemsAnalysis.Grid.GridAnalysis
{
    public class GridModelTimeStep
    {
        double rainfall;
        int runOrder;
        string comment;

        public GridModelTimeStep(double rainfall, int runOrder, string comment)
            : this()
        {
            this.rainfall = rainfall;
            this.runOrder = runOrder;
            this.comment = comment;
        }
        public GridModelTimeStep()
        {
        }

        #region Accessors for Xml serialization
        public double Rainfall { get { return rainfall; } set { rainfall = value; } }
        public int RunOrder { get { return runOrder; } set { runOrder = value; } }
        public string Comment { get { return comment; } set { comment = value; } }
        #endregion


    }
}
