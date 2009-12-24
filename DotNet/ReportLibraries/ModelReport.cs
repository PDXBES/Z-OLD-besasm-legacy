using ESRI.ArcGIS.Utility;

using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Modeling.ModelResults;
using System.Collections;
using System.Collections.Specialized;
using SystemsAnalysis.Utils.Events;
using SystemsAnalysis.Types;
using SystemsAnalysis.DataAccess;

namespace SystemsAnalysis.Reporting.ReportLibraries
{
    public class ModelReport : ReportBase
    {
        private Links links;
        private Nodes nodes;
        private Dscs dscs;
        private PipeInspectionDataSet _pipeInspectionDS;

        private PipeInspectionDataSet PipeInspectionDS
        {
            get
            {
                if (_pipeInspectionDS == null)
                {
                    InitPipeInspectionDataSet();
                }
                return _pipeInspectionDS;
            }
        }
        private void InitPipeInspectionDataSet()
        {
            _pipeInspectionDS = new PipeInspectionDataSet();
            DataAccess.PipeInspectionDataSetTableAdapters.PipeInspectionGradesTableAdapter piTA;
            piTA = new DataAccess.PipeInspectionDataSetTableAdapters.PipeInspectionGradesTableAdapter();
            piTA.Fill(_pipeInspectionDS.PipeInspectionGrades);
        }

        /// <summary>
        /// Creates a ModelReport from a collections of Links, Nodes and Dscs
        /// </summary>
        /// <param name="links">A Links collection to report on</param>
        /// <param name="nodes">A Nodes collection to report on</param>
        /// <param name="dscs">A Dscs collection to report on</param>
        public ModelReport(Links links, Nodes nodes, Dscs dscs)
        //: base(links, nodes, dscs)
        {
            this.links = links;
            this.nodes = nodes;
            this.dscs = dscs;
        }
        /// <summary>
        /// Creates a ModelReport from a Model
        /// </summary>
        /// <param name="model">A Model to report on</param>
        public ModelReport(Model model)
            : this(model.ModelLinks, model.ModelNodes, model.ModelDscs)
        {
        }

        public string DscAreaByZoning(IDictionary<string, Parameter> parameters)
        {
            int count = 0;
            bool isFraction = parameters.ContainsKey("IsFraction") && parameters["IsFraction"].ValueAsBool;
            Enumerators.ZoningTypes zoning;
            zoning = Enumerators.GetZoningEnum(parameters["Zoning"].Value);

            if (zoning != Enumerators.ZoningTypes.OTH)
            {
                count = dscs.AreaByZoning(zoning,
                Enumerators.GetTimeFrameEnum(parameters["TimeFrame"].Value));
            }
            else
            {
                int nonOTHCount = 0;
                foreach (Enumerators.ZoningTypes zo in Enum.GetValues(typeof(Enumerators.ZoningTypes)))
                {
                    nonOTHCount += dscs.AreaByZoning(zo,
                    Enumerators.GetTimeFrameEnum(parameters["TimeFrame"].Value));
                }
                count = dscs.Values.Count - nonOTHCount;
                return "Not Implemented";
            }
            return isFraction
            ? Convert.ToString(
            Math.Round((double)count / dscs.Count, 2) * 100 + "%")
            : count.ToString("N0");
        }

        public double DscArea(IDictionary<string, Parameter> parameters)
        {
            double area = 0;

            bool filteredByZoning;
            Enumerators.ZoningTypes zoning = Enumerators.ZoningTypes.UNK;
            Enumerators.TimeFrames timeFrame = Enumerators.TimeFrames.EX;
            filteredByZoning = parameters.ContainsKey("Zoning");
            if (filteredByZoning)
            {
                zoning = Enumerators.GetZoningEnum(parameters["Zoning"].Value);
                timeFrame = Enumerators.GetTimeFrameEnum(parameters["TimeFrame"].Value);
            }

            bool filteredBySewerable;
            Enumerators.Sewerable sewerable = Enumerators.Sewerable.NoDetermination;
            filteredBySewerable = parameters.ContainsKey("Sewerable");
            if (filteredBySewerable)
            {
                sewerable = (Enumerators.Sewerable)parameters["Sewerable"].ValueAsInt;
            }

            bool filteredByDevelopmentState;
            Enumerators.DevelopmentState devState = Enumerators.DevelopmentState.Unspecified;
            filteredByDevelopmentState = parameters.ContainsKey("DevelopmentState");
            if (filteredByDevelopmentState)
            {
                devState = Enumerators.GetDevelopmentStateEnum(parameters["DevelopmentState"].Value);
            }

            bool filteredByConnectionAssumption;
            Enumerators.ConnectionAssumption connectionAssumption = Enumerators.ConnectionAssumption.Unspecified;
            filteredByConnectionAssumption = parameters.ContainsKey("ConnectionAssumption");
            if (filteredByConnectionAssumption)
            {
                connectionAssumption = Enumerators.GetConnectionAssumptionEnum(parameters["ConnectionAssumption"].Value);
            }

            foreach (Dsc dsc in this.dscs.Values)
            {
                if ((!filteredBySewerable || dsc.Sewerable == sewerable) &&
                (!filteredByZoning ||
                (timeFrame == Enumerators.TimeFrames.EX ? dsc.GenZoneEX : dsc.GenZoneCP) == zoning) &&
                (!filteredByDevelopmentState || dsc.DevelopmentState == devState) &&
                (!filteredByConnectionAssumption || dsc.ConnectionAssumption == connectionAssumption))
                {
                    area += dsc.Area / 43560.0;
                }
            }
            return area;
        }
        public string DscAreaSummary(IDictionary<string, Parameter> parameters)
        {
            double fraction;
            double area;

            area = DscArea(parameters);
            fraction = area / dscs.Area() * 100.0;

            return area.ToString("N1") + " (" + fraction.ToString("N0") + "%)";
        }

        public string DscAreaBySewerable(IDictionary<string, Parameter> parameters)
        {
            Enumerators.Sewerable sewerable;
            sewerable = (Enumerators.Sewerable)parameters["Sewerable"].ValueAsInt;
            return (dscs.AreaBySewerable(sewerable)).ToString("N0");
        }
        public string DscCountBySewerable(IDictionary<string, Parameter> parameters)
        {
            Enumerators.Sewerable sewerable;
            sewerable = (Enumerators.Sewerable)parameters["Sewerable"].ValueAsInt;
            return (dscs.CountBySewerable(sewerable)).ToString("N0");
        }
        public string DscAreaBySewerableZoning(IDictionary<string, Parameter> parameters)
        {
            int area = 0;
            Enumerators.TimeFrames timeFrame;
            timeFrame = Enumerators.GetTimeFrameEnum(parameters["TimeFrame"].Value);
            Enumerators.Sewerable sewerable;
            sewerable = (Enumerators.Sewerable)parameters["Sewerable"].ValueAsInt;
            Enumerators.ZoningTypes zoning;
            zoning = Enumerators.GetZoningEnum(parameters["Zoning"].Value);
            if (timeFrame == Enumerators.TimeFrames.EX)
            {
                foreach (Dsc dsc in dscs.Values)
                {

                    if (dsc.Sewerable == sewerable &&
                    dsc.GenZoneEX == zoning)
                    {
                        area += dsc.Area;
                    }
                }
            }
            else if (timeFrame == Enumerators.TimeFrames.FU)
            {
                foreach (Dsc dsc in dscs.Values)
                {

                    if (dsc.Sewerable == sewerable &&
                    dsc.GenZoneCP == zoning)
                    {
                        area += dsc.Area;
                    }
                }
            }


            return (area / 43560.0).ToString("N0");
        }
        /// <summary>
        /// Returns the number of gravity pipe segments within a Model
        /// </summary>
        /// <param name="parameters">A parameter collection containing optional values to filter the results by, including Material, MinDiameter, MaxDiameter</param>
        /// <returns>The number of gravity pipe segments within the Model</returns>
        public int PipeCount(IDictionary<string, Parameter> parameters)
        {
            int count = 0;

            bool filteredByMinYearBuilt;
            int minYearBuilt = 0;
            filteredByMinYearBuilt = parameters.ContainsKey("MinYearBuilt");
            if (filteredByMinYearBuilt)
            {
                minYearBuilt = parameters["MinYearBuilt"].ValueAsInt;
            }

            bool filteredByMaxYearBuilt;
            int maxYearBuilt = 0;
            filteredByMaxYearBuilt = parameters.ContainsKey("MaxYearBuilt");
            if (filteredByMaxYearBuilt)
            {
                maxYearBuilt = parameters["MaxYearBuilt"].ValueAsInt;
            }

            bool yearBuiltIsNull;
            yearBuiltIsNull = parameters.ContainsKey("YearBuiltIsNull") ? parameters["YearBuiltIsNull"].ValueAsBool : false;

            bool filteredByMinDiameter;
            double minDiameter = 0;
            filteredByMinDiameter = parameters.ContainsKey("MinDiameter");
            if (filteredByMinDiameter)
            {
                minDiameter = parameters["MinDiameter"].ValueAsDouble;
            }

            bool filteredByMaxDiameter;
            double maxDiameter = 0;
            filteredByMaxDiameter = parameters.ContainsKey("MaxDiameter");
            if (filteredByMaxDiameter)
            {
                maxDiameter = parameters["MaxDiameter"].ValueAsDouble;
            }

            bool filteredByMaterial;
            string material = "";
            filteredByMaterial = parameters.ContainsKey("Material");
            if (filteredByMaterial)
            {
                material = parameters["Material"].Value;
            }

            bool filteredByLinkType;
            Enumerators.LinkTypes linkType = Enumerators.LinkTypes.UNKNOWN;
            filteredByLinkType = parameters.ContainsKey("LinkType");
            if (filteredByLinkType)
            {
                linkType = Enumerators.GetLinkTypeEnum(parameters["LinkType"].Value);
            }

            foreach (Link l in links.Values)
            {
                int yearBuilt = l.InstallDate.Year;
                double diameter = l.Diameter;
                if ((!filteredByMinYearBuilt || yearBuilt >= minYearBuilt) &&
                (!filteredByMaxYearBuilt || yearBuilt <= maxYearBuilt) &&
                (!yearBuiltIsNull || yearBuilt == 1) &&
                (!filteredByMinDiameter || diameter >= minDiameter) &&
                (!filteredByMaxDiameter || diameter <= maxDiameter) &&
                (!filteredByMaterial || l.Material == material) &&
                (!filteredByLinkType || l.LinkType == linkType) &&
                l.IsGravityFlow)
                {
                    count++;
                }
            }

            return count;
        }
        /// <summary>
        /// Returns the length of gravity pipe within a model in miles
        /// </summary>
        /// <param name="parameters">A parameter collection containing optional values to filter the results by, including Material, MinDiameter, MaxDiameter</param>
        /// <returns>The length of pipe within the Model in miles</returns>
        public double PipeLength(IDictionary<string, Parameter> parameters)
        {
            double length = 0;

            bool filteredByMinYearBuilt;
            int minYearBuilt = 0;
            filteredByMinYearBuilt = parameters.ContainsKey("MinYearBuilt");
            if (filteredByMinYearBuilt)
            {
                minYearBuilt = parameters["MinYearBuilt"].ValueAsInt;
            }

            bool filteredByMaxYearBuilt;
            int maxYearBuilt = 0;
            filteredByMaxYearBuilt = parameters.ContainsKey("MaxYearBuilt");
            if (filteredByMaxYearBuilt)
            {
                maxYearBuilt = parameters["MaxYearBuilt"].ValueAsInt;
            }

            bool yearBuiltIsNull;
            yearBuiltIsNull = parameters.ContainsKey("YearBuiltIsNull") ? parameters["YearBuiltIsNull"].ValueAsBool : false;

            bool filteredByMinDiameter;
            double minDiameter = 0;
            filteredByMinDiameter = parameters.ContainsKey("MinDiameter");
            if (filteredByMinDiameter)
            {
                minDiameter = parameters["MinDiameter"].ValueAsDouble;
            }

            bool filteredByMaxDiameter;
            double maxDiameter = 0;
            filteredByMaxDiameter = parameters.ContainsKey("MaxDiameter");
            if (filteredByMaxDiameter)
            {
                maxDiameter = parameters["MaxDiameter"].ValueAsDouble;
            }

            bool filteredByMaterial;
            string material = "";
            filteredByMaterial = parameters.ContainsKey("Material");
            if (filteredByMaterial)
            {
                material = parameters["Material"].Value;
            }

            bool filteredByLinkType;
            Enumerators.LinkTypes linkType = Enumerators.LinkTypes.UNKNOWN;
            filteredByLinkType = parameters.ContainsKey("LinkType");
            if (filteredByLinkType)
            {
                linkType = Enumerators.GetLinkTypeEnum(parameters["LinkType"].Value);
            }

            foreach (Link l in links.Values)
            {
                int yearBuilt = l.InstallDate.Year;
                double diameter = l.Diameter;
                if ((!filteredByMinYearBuilt || yearBuilt >= minYearBuilt) &&
                (!filteredByMaxYearBuilt || yearBuilt <= maxYearBuilt) &&
                (!yearBuiltIsNull || yearBuilt == 1) &&
                (!filteredByMinDiameter || diameter >= minDiameter) &&
                (!filteredByMaxDiameter || diameter <= maxDiameter) &&
                (!filteredByMaterial || l.Material == material) &&
                (!filteredByLinkType || l.LinkType == linkType) &&
                l.IsGravityFlow)
                {
                    length += l.Length;
                }
            }

            return length / 5280.0;
        }
        public int PipeMinDiameter(IDictionary<string, Parameter> parameters)
        {
            return links.GetMinPipeDiam(
            Enumerators.GetLinkTypeEnum(parameters["LinkType"].Value));
        }
        public int PipeMaxDiameter(IDictionary<string, Parameter> parameters)
        {
            return links.GetMaxPipeDiam(
            Enumerators.GetLinkTypeEnum(parameters["LinkType"].Value));
        }
        public string PipeDiameterRange(IDictionary<string, Parameter> parameters)
        {
            if (parameters.ContainsKey("LinkType"))
            {
                Enumerators.LinkTypes linkType;
                linkType = Enumerators.GetLinkTypeEnum(parameters["LinkType"].Value);
                return
                Convert.ToString(links.GetMinPipeDiam(linkType) +
                "\" to " +
                Convert.ToString(links.GetMaxPipeDiam(linkType)) + "\"");
            }
            else
            {
                return
                Convert.ToString(links.GetMinPipeDiam() +
                "\" to " +
                Convert.ToString(links.GetMaxPipeDiam()) + "\"");
            }
        }
        public string PipeInstallDateRange(IDictionary<string, Parameter> parameters)
        {
            return
            Convert.ToString(links.GetOldestPipeInstallYear()) +
            " to " +
            Convert.ToString(links.GetNewestPipeInstallYear());
        }
        public int DscCount(IDictionary<string, Parameter> parameters)
        {
            int count = 0;

            bool filteredByZoning;
            Enumerators.ZoningTypes zoning = Enumerators.ZoningTypes.UNK;
            Enumerators.TimeFrames timeFrame = Enumerators.TimeFrames.EX;
            filteredByZoning = parameters.ContainsKey("Zoning");
            if (filteredByZoning)
            {
                zoning = Enumerators.GetZoningEnum(parameters["Zoning"].Value);
                timeFrame = Enumerators.GetTimeFrameEnum(parameters["TimeFrame"].Value);
            }

            bool filteredBySewerable;
            Enumerators.Sewerable sewerable = Enumerators.Sewerable.NoDetermination;
            filteredBySewerable = parameters.ContainsKey("Sewerable");
            if (filteredBySewerable)
            {
                sewerable = (Enumerators.Sewerable)parameters["Sewerable"].ValueAsInt;
            }

            bool filteredByDevelopmentState;
            Enumerators.DevelopmentState devState = Enumerators.DevelopmentState.Unspecified;
            filteredByDevelopmentState = parameters.ContainsKey("DevelopmentState");
            if (filteredByDevelopmentState)
            {
                devState = Enumerators.GetDevelopmentStateEnum(parameters["DevelopmentState"].Value);
            }

            foreach (Dsc dsc in dscs.Values)
            {
                if ((!filteredBySewerable || dsc.Sewerable == sewerable) &&
                (!filteredByZoning ||
                (timeFrame == Enumerators.TimeFrames.EX ? dsc.GenZoneEX : dsc.GenZoneCP) == zoning) &&
                (!filteredByDevelopmentState || dsc.DevelopmentState == devState))
                {
                    count++;
                }
            }
            return count;
        }

        public int RoofDiscoCount(IDictionary<string, Parameter> parameters)
        {
            bool filteredByMinConnectionRate;
            double minConnectionRate = 0;
            filteredByMinConnectionRate = parameters.ContainsKey("MinConnectionRate");
            if (filteredByMinConnectionRate)
            {
                minConnectionRate = parameters["MinConnectionRate"].ValueAsDouble;
            }

            bool filteredByMaxConnectionRate;
            double maxConnectionRate = 0;
            filteredByMaxConnectionRate = parameters.ContainsKey("MaxConnectionRate");
            if (filteredByMaxConnectionRate)
            {
                maxConnectionRate = parameters["MaxConnectionRate"].ValueAsDouble;
            }

            int count = 0;
            foreach (Dsc dsc in dscs.Values)
            {
                double discoFraction;
                discoFraction = (dsc.EICParkEX + dsc.EICRoofEX) /
                (dsc.ParkAreaEX + dsc.RoofAreaEX + System.Double.Epsilon);
                double connectedFraction = 1 - discoFraction;

                //TODO: dsc.DiscoClass should be replaced with an enumerated type
                if ((!filteredByMinConnectionRate || connectedFraction >= minConnectionRate) &&
                    (!filteredByMaxConnectionRate || connectedFraction <= maxConnectionRate) &&
                    (dsc.DiscoClass != "S"))
                {                    
                    count++;
                }
            }
            return count;                        
        }
        public double RoofDiscoPercent(IDictionary<string, Parameter> parameters)
        {
            return (double)RoofDiscoCount(parameters) / dscs.Count * 100;
        }
        public string RoofDiscoCountByZoning(IDictionary<string, Parameter> parameters)
        {
            int count;
            bool isFraction = parameters["IsFraction"].ValueAsBool;
            Enumerators.ZoningTypes zoning;
            zoning = Enumerators.GetZoningEnum(parameters["Zoning"].Value);
            if (zoning != Enumerators.ZoningTypes.OTH)
            {
                if (dscs.CountByZoning(zoning,
                Enumerators.GetTimeFrameEnum(parameters["TimeFrame"].Value)) == 0)
                {
                    return "N/A";
                }
                count = dscs.RoofDiscoCountByZoning(zoning,
                Enumerators.GetTimeFrameEnum(parameters["TimeFrame"].Value));
                return isFraction
                ? Convert.ToString(
                Math.Round((double)count /
                dscs.CountByZoning(
                Enumerators.GetZoningEnum(parameters["Zoning"].Value),
                Enumerators.GetTimeFrameEnum(parameters["TimeFrame"].Value)),
                2) * 100)
                : Convert.ToString(count);
            }
            else
            {
                int nonOTHCount = 0;
                int nonOTHDiscoCount = 0;
                foreach (Enumerators.ZoningTypes zo in Enum.GetValues(typeof(Enumerators.ZoningTypes)))
                {
                    nonOTHDiscoCount += dscs.RoofDiscoCountByZoning(zo,
                    Enumerators.GetTimeFrameEnum(parameters["TimeFrame"].Value));
                    nonOTHCount += dscs.CountByZoning(zo,
                    Enumerators.GetTimeFrameEnum(parameters["TimeFrame"].Value));
                }

                if (nonOTHCount == 0) return "N/A";
                return isFraction
                ? Convert.ToString(
                Math.Round((double)nonOTHDiscoCount /
                nonOTHCount,
                2) * 100)
                : Convert.ToString(nonOTHDiscoCount);
            }

        }
        public int DiscoClassCount(IDictionary<string, Parameter> parameters)
        {
            string discoClass = parameters["DiscoClass"].Value;
            int count = 0;
            foreach (Dsc dsc in dscs.Values)
            {
                //TODO: dsc.DiscoClass should be replaced with an enumerated type
                if (dsc.DiscoClass == discoClass)
                {
                    count++;
                }
            }
            return count;
        }
        public double DiscoClassPercent(IDictionary<string, Parameter> parameters)
        {
            return (double)DiscoClassCount(parameters) / dscs.Count * 100;
        }

        public int FalseBFRiskCount(IDictionary<string, Parameter> parameters)
        {            
            int count;
            count = dscs.CountByFalseBFRisk();
            return count;
        }
        public double FalseBFRiskPercent(IDictionary<string, Parameter> parameters)
        {
            return (double)FalseBFRiskCount(parameters) / dscs.Count * 100;
        }


        public int PipeGradeCount(IDictionary<string, Parameter> parameters)
        {
            int count = 0;
            int pipeGrade;
            pipeGrade = parameters["PipeGrade"].ValueAsInt;
            foreach (Link l in links.Values)
            {
                PipeInspectionDataSet.PipeInspectionGradesRow pipeGradeRow;
                pipeGradeRow = PipeInspectionDS.PipeInspectionGrades.FindByMLinkID(l.MLinkID);
                if (pipeGradeRow == null || pipeGradeRow.IsGradeNull())
                {
                    continue;
                }
                if (pipeGradeRow.Grade == pipeGrade)
                {
                    count++;
                }
            }
            return count;
        }
        public int PipesWithoutGradeCount(IDictionary<string, Parameter> parameters)
        {
            int count = 0;
            
            foreach (Link l in links.Values)
            {
                PipeInspectionDataSet.PipeInspectionGradesRow pipeGradeRow;
                pipeGradeRow = PipeInspectionDS.PipeInspectionGrades.FindByMLinkID(l.MLinkID);
                if (pipeGradeRow == null || pipeGradeRow.IsGradeNull() || pipeGradeRow.Grade == 0)
                {
                    count++;
                }
                else { }
            }
            return count;
        }
    }
}
