using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemsAnalysis.Grid.GridAnalysis
{
    public class GridProcessGroup
    {
        string groupName;
        string description;
        double groupOrder;
        List<GridProcess> gridProcesses;

        public GridProcessGroup(string groupName, double groupOrder, string description)
            : this()
        {
            this.groupName = groupName;
            this.groupOrder = groupOrder;
            this.description = description;
        }
        public GridProcessGroup()
        {
            gridProcesses = new List<GridProcess>();
        }

        public string GroupName { get { return groupName; } set { groupName = value; } }
        public string Description { get { return description; } set { description = value; } }
        public double GroupOrder { get { return groupOrder; } set { groupOrder = value; } }
        public List<GridProcess> GridProcesses { get { return gridProcesses; } set { gridProcesses = value; } }
    }
}
