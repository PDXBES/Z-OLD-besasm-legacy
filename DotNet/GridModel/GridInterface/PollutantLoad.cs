using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemsAnalysis.Grid.GridAnalysis
{
    public class PollutantLoad
    {
        private string name;
        private double load;

        public PollutantLoad(string name, double load)
            :this()
        {
            this.name = name;
            this.load = load;
        }
        public PollutantLoad()
        {
        }

        public string Name
        { get { return name; } set { name = value; } }
        public double Load
        { get { return load; } set { load = value; } }

    }
}
