using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsAnalysis.Modeling
{
    public interface InflowControl
    {

        int ID
        {
            get;
            set;
        }

        Types.Enumerators.InflowControlTypes ICType
        {
            get;
        }

        bool InModel
        {
            get;
        }
    }
}
