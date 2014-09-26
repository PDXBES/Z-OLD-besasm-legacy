using System.Collections.Generic;

namespace PumpCycleDataAnalyzer
{


    partial class PumpCycleDataSet
    {
        partial class CycleSummaryDataTable
        {
        }
    
        partial class DistributedDataTable
        {
        }

        partial class CalculatedDataTable
        {
        }

        public double getPump1Average()
        {
            double sum = 0;
            double count = 0;
            for (int i = 0; i < Calculated.Count; i++)
            {
                if (Calculated[i].PumpNum == 1)
                {
                    sum += Calculated[i].PumpRate;
                    count++;
                }
            }
            return count == 0 ? 0 : sum / count;
        }
        public double getPump2Average()
        {
            double sum = 0;
            double count = 0;
            for (int i = 0; i < Calculated.Count; i++)
            {
                if (Calculated[i].PumpNum == 2)
                {
                    sum += Calculated[i].PumpRate;
                    count++;
                }
            }
            return count == 0 ? 0 : sum / count;
        }
        public double getPump3Average()
        {
            double sum = 0;
            double count = 0;
            for (int i = 0; i < Calculated.Count; i++)
            {
                if (Calculated[i].PumpNum == 3)
                {
                    sum += Calculated[i].PumpRate;
                    count++;
                }
            }
            return count == 0 ? 0 : sum / count;
        }
        public double getPump4Average()
        {
            double sum = 0;
            double count = 0;
            for (int i = 0; i < Calculated.Count; i++)
            {
                if (Calculated[i].PumpNum == 4)
                {
                    sum += Calculated[i].PumpRate;
                    count++;
                }
            }
            return count == 0 ? 0 : sum / count;
        }

        public double getInflowAverage()
        {
            double sum = 0;
            double count = 0;
            for (int i = 0; i < Calculated.Count; i++)
            {
                if (!Calculated[i].Flagged && !Calculated[i].Multi)
                {
                    sum += Calculated[i].InflowRate;
                    count++;
                }
            }
            return count == 0 ? 0 : sum / count;
        }

        public partial class PumpDataTable
        {
            public List<int> StationIDList()
            {
                List<int> stationIDList = new List<int>();
                foreach (PumpCycleDataSet.PumpRow pumpRow in this)
                {
                    if (!stationIDList.Contains(pumpRow.StationID))
                    {
                        stationIDList.Add(pumpRow.StationID);
                    }
                }
                return stationIDList;
            }
        }
        public partial class StationDataTable
        {
            public List<int> StationIDList()
            {
                List<int> stationIDList = new List<int>();
                foreach (PumpCycleDataSet.StationRow stationRow in this)
                {
                    if (!stationIDList.Contains(stationRow.StationID))
                    {
                        stationIDList.Add(stationRow.StationID);
                    }
                }
                return stationIDList;
            }

        }
    }
}
