using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemsAnalysis.Grid.GridAnalysis
{
    public class GridProcess
    {
        string processName;
        bool critical;
        double processOrder;

        public GridProcess(string processName, bool critical, double processOrder)
            : this()
        {
            this.processName = processName;
            this.critical = critical;
            this.processOrder = processOrder;
        }
        public GridProcess()
        {
        }

        public string ProcessName { get { return processName; } set { processName = value; } }
        public bool Critical { get { return critical; } set { critical = value; } }
        public double ProcessOrder { get { return processOrder; } set { processOrder = value; } }
        

    }
}
