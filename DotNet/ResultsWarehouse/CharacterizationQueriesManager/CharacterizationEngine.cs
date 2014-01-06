using System;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using SystemsAnalysis;
using SystemsAnalysis.Types;
using SystemsAnalysis.Tracer;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Utils.Events;

namespace SystemsAnalysis.Characterization
{
	/// <summary>
	/// A class to perform characterization queries for collections of Model
	/// components. Characterization operations are assumed to take place against
	/// component collections, not a Model or StartLinks/StopLinks collections.
	/// </summary>
	public class CharacterizationEngine
	{
		private Links charLinks;
		private Nodes charNodes;
		private Dscs charDscs;
        private FlowEstimationCatchments fecs;

		private ListDictionary linkResults;		
		private ListDictionary nodeResults;		
		private ListDictionary dscResults;
		private string studyArea;

		private string reportTemplate;
		private string outputFile;
        				
		private string psDB;
		private string uicDB;
		private string mstSurfSC_FC;

        private XmlDocument xmlDoc;

		/// <summary>
		/// Constructor for the CharacterizationEngine.
		/// </summary>
		/// <param name="charLinks">A Links collection to be characterized.</param>		
		/// <param name="settings">A Settings DataSet that contains information regarding
		/// the source files for the characterization engine.</param>
		/// <param name="studyArea">A title for the study area used when creating table headers.</param>
		public CharacterizationEngine(Links charLinks, string studyArea, Settings settings)
		{			
			this.OnStatusChanged(new StatusChangedArgs("Creating the characterization engine."));
			this.charLinks = charLinks; 
			this.studyArea = studyArea;

			//this.agMasterDB = settings.DataSource.FindByName("AGMasterDB").DataSource_Text;
			//this.resultsWarehouseDB = settings.DataSource.FindByName("ResultsWarehouseDB").DataSource_Text;
			this.psDB = settings.DataSource.FindByName("PumpStationDB").DataSource_Text;
			//this.template = settings.DataSource.FindByName("CharTemplate").DataSource_Text;			
			this.uicDB = settings.DataSource.FindByName("UICDB").DataSource_Text;
			this.mstSurfSC_FC = settings.DataSource.FindByName("MstSSC").DataSource_Text;
			//this.outputFile = settings.DataSource.FindByName("OutputDirectory").DataSource_Text + "Char_";

			/*foreach (char c in studyArea)
			{
				if (Char.IsLetterOrDigit(c))
				{
					outputFile += c;
				}
			}
			outputFile += ".xml";

			if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(outputFile)))
				//|| outputFile.IndexOfAny(System.IO.Path.InvalidPathChars) >= 0)
			{
				throw new Exception("Could not create output file. Verify output path and study area name are valid: '"
					+ outputFile + "'.");						
			}*/
		}

		/// <summary>
		/// Loads the AGMaster and Results Warehouse DataSets.
		/// </summary>
		public void LoadCharacterizationTables()
		{			
			this.OnStatusChanged(new StatusChangedArgs("Loading the SAMaster DataSet."));		
				
			this.OnStatusChanged(new StatusChangedArgs("Creating Node objects."));
			this.charNodes = new Nodes();
			this.charNodes.StatusChanged += new OnStatusChangedEventHandler(this.OnStatusChanged);
			this.charNodes.Load(charLinks);				

			this.OnStatusChanged(new StatusChangedArgs("Creating Dsc objects."));			
			this.charDscs = new Dscs();
			this.charDscs.StatusChanged += new OnStatusChangedEventHandler(this.OnStatusChanged);
			this.charDscs.Load(charLinks);
						
		    this.OnStatusChanged(new StatusChangedArgs("Loading the Results Catalog DataSet."));

            ModelCatalogDataSet modelCatalogDS;
            modelCatalogDS = new DataAccess.ModelCatalogDataSet();
            DataAccess.ModelCatalogDataSetTableAdapters.StormCatalogTableAdapter scTA;
            scTA = new SystemsAnalysis.DataAccess.ModelCatalogDataSetTableAdapters.StormCatalogTableAdapter();
            DataAccess.ModelCatalogDataSetTableAdapters.ModelCatalogTableAdapter mcTA;
            mcTA = new SystemsAnalysis.DataAccess.ModelCatalogDataSetTableAdapters.ModelCatalogTableAdapter();
            DataAccess.ModelCatalogDataSetTableAdapters.ModelScenarioTableAdapter msTA;
            msTA = new SystemsAnalysis.DataAccess.ModelCatalogDataSetTableAdapters.ModelScenarioTableAdapter();
            scTA.Fill(modelCatalogDS.StormCatalog);
            msTA.Fill(modelCatalogDS.ModelScenario);
            mcTA.Fill(modelCatalogDS.ModelCatalog);
	
			this.OnStatusChanged(new StatusChangedArgs("Creating hydraulics results objects for all model scenarios."));
			//For convenience unique results objects are created for each scenario 
			//and stored in a list dictionary accessible via the scenarioID. This 
			//allows loading/accessing all model scenarios by simply iterating
			//through the dictionary
			linkResults = new ListDictionary();
			nodeResults = new ListDictionary();
			dscResults = new ListDictionary();
			foreach (DataAccess.ModelCatalogDataSet.ModelScenarioRow scenario
				in modelCatalogDS.ModelScenario)
			{
                int scenarioID = scenario.ScenarioID;
				this.OnStatusChanged(new StatusChangedArgs("Scenario " + scenarioID + ": '" + scenario.Description + "'."));
                this.OnStatusChanged(new StatusChangedArgs("    Loading links..."));                           
                linkResults.Add(scenarioID, new LinkHydraulics(charLinks, scenarioID));
                this.OnStatusChanged(new StatusChangedArgs("    Loading nodes..."));                           
				nodeResults.Add(scenarioID, new NodeHydraulics(charNodes, scenarioID));
                this.OnStatusChanged(new StatusChangedArgs("    Loading dscs..."));                           
				dscResults.Add(scenarioID, new DscHydraulics(charDscs, scenarioID));                
			}					
		}

		/// <summary>
		/// Generates an XML characterization report using the parameters specified in the constructor.
		/// </summary>
		/// <returns>The name of the .xml file containing the characterization information.</returns>
		public string Characterize(string reportTemplate, string reportOutput, string studyArea)
		{				
			this.OnStatusChanged(new StatusChangedArgs("Beginning characterization process."));
            try
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(reportTemplate);

                xmlDoc.GetElementsByTagName("ReportGenerator")[0].Attributes["studyArea"].Value = studyArea;

                //parseEmbeddedMultiTables();
                parseEmbeddedMultiRows();
                parseEmbeddedFunctions();

                xmlDoc.Save(reportOutput);
            }
            catch (Exception ex)
            {
                this.OnStatusChanged(new StatusChangedArgs(ex.Message));
            }
            #region old xml parser
            /*while (xmlReader.Read()) 
			{				
				if (xmlReader.NodeType == System.Xml.XmlNodeType.EndElement)
				{
					xmlWriter.WriteEndElement();
					continue;
				}
				else if (xmlReader.NodeType == System.Xml.XmlNodeType.Whitespace)
				{ continue; }
				else if (xmlReader.NodeType == System.Xml.XmlNodeType.XmlDeclaration ||
					xmlReader.NodeType == System.Xml.XmlNodeType.ProcessingInstruction)
				{
					xmlWriter.WriteProcessingInstruction(xmlReader.Name, xmlReader.Value);
					continue;
				}

				switch (xmlReader.Name)
				{
					case "ReportGenerator":
						xmlWriter.WriteStartElement(xmlReader.Name);
						xmlWriter.WriteAttributeString("studyArea", this.studyArea);
						break;								
					case "Cell":					
						if (xmlReader.GetAttribute("cellType") == "FUNCTION")
						{
							xmlWriter.WriteStartElement(xmlReader.Name);							
							while (xmlReader.MoveToNextAttribute())
							{	
								if (xmlReader.Name != "data")
								{
									xmlWriter.WriteAttributeString(xmlReader.Name, xmlReader.Value);									
								}								
							}
							xmlReader.MoveToElement();
							string functionValue;
							try
							{
								functionValue = this.parseEmbeddedFunction(
									xmlReader.ReadInnerXml(), xmlReader);
							}
							catch
							{
								functionValue = "FUNCTION ERROR";
							}

							xmlWriter.WriteAttributeString("data", functionValue);
							xmlWriter.WriteEndElement();
							break;
						}
						else { goto default; }						
					case "Function":
						//Should not get here. <Function> should be handled 
						//internal to <Cell> by parseEmbeddedFunction
						break;  
					case "Parameter":
						//Should not get here. <Parameter> should be handled 
						//internal to <Cell> by parseEmbeddedFunction						
						break;
					case "CharacterizationTable":
						if (xmlReader.GetAttribute("isMultiTable") == "true")
						{
							xmlReader.MoveToElement();
							parseEmbeddedMultiTable(
								xmlReader.ReadOuterXml(), xmlReader, xmlWriter);							
							break;
						}
						else
						{ goto default; }						
					case "Row":
						if (xmlReader.GetAttribute("isMultiRow") == "true")
						{
							xmlReader.MoveToElement();							
							parseEmbeddedMultiRow(
								xmlReader.ReadOuterXml(), xmlReader, xmlWriter);							
							break;
						}							
						else
						{ goto default;	}
					case "Footer":
						goto default;
                    //Default case is simply to write the input tag to the output file
					default:
						xmlWriter.WriteStartElement(xmlReader.Name);
						while (xmlReader.MoveToNextAttribute())
						{	
							xmlWriter.WriteAttributeString(xmlReader.Name, xmlReader.Value);
						}						
						break;							
				}								
			}
			xmlWriter.Flush();
			xmlWriter.Close();*/
            #endregion

            return reportOutput;
		}

        private void parseEmbeddedFunctions()
        {
            System.Xml.XmlNodeList xmlNodeList;
            
            xmlNodeList = xmlDoc.GetElementsByTagName("Cell");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (xmlNode.Attributes["cellType"].Value != "FUNCTION")
                {
                    continue;
                }
                string functionValue;
                functionValue = this.parseEmbeddedFunction(xmlNode.InnerXml);

                xmlNode.RemoveAll();
                xmlNode.Attributes.Append(xmlDoc.CreateAttribute("cellType"));
                xmlNode.Attributes.Append(xmlDoc.CreateAttribute("data"));
                xmlNode.Attributes["cellType"].Value = "FUNCTION";
                xmlNode.Attributes["data"].Value = functionValue;

            }
        }
		private string parseEmbeddedFunction(string xmlFragment)
		{
			string functionName = "";
			
	        /* Problem: when reading parameters from an xml file, each parameter is of
             * type String. It is necessary to pass the parameters to their respective
             * handler functions as the correct type. This is handled using switch/case
             * statements to create the appropriate typed parameters. */
			ListDictionary rawParams = new ListDictionary();
			ListDictionary typedParams = new ListDictionary();
			
            XmlReader functionReader = XmlReader.Create(new StringReader(xmlFragment));
            //Read raw parameters as objects into ListDictionary rawParams
			while (functionReader.Read())
			{
				if (functionReader.NodeType==System.Xml.XmlNodeType.EndElement)
				{
					continue;
				}
				switch (functionReader.Name)
				{
					case "Function":
						functionName = functionReader.GetAttribute("functionName");							
						break;
					case "Parameter":
						rawParams.Add(functionReader.GetAttribute("parameterName"), 
							functionReader.GetAttribute("parameterValue"));
						break;
				}
			}	
			try
			{
                //Parse raw parameters and create typed parameters in typedParams ListDictionary
                typedParams = ExtractTypedParams(rawParams);
			    return HandleFunction(functionName, typedParams);            
			}
			catch (Exception ex)
			{
				//If any of the embedded functions was invalid, had improper parameters,
				//or simply failed, write "PARSE ERROR" and keep move onto the next.								
				this.OnStatusChanged(new StatusChangedArgs("Parse embedded XML function error! " + ex.ToString()));				
				this.OnStatusChanged(new StatusChangedArgs("Function name: '" + functionName + "'. Parameters:"));				
				IDictionaryEnumerator paramEnum = typedParams.GetEnumerator();
				while (paramEnum.MoveNext())
				{
					this.OnStatusChanged(new StatusChangedArgs("	" + (string)paramEnum.Key + 
						"=" + Convert.ToString(paramEnum.Value) + 
						" (" + paramEnum.Value.GetType().ToString() + ")"));
				}				
				return "PARSE ERROR";
			}			
		}
		
        private ListDictionary ExtractTypedParams(ListDictionary rawParams)
        {
            ListDictionary typedParams;
            typedParams = new ListDictionary();
            foreach (string paramName in rawParams.Keys)
            {
                string paramValue = (string)rawParams[paramName];
                //Create typed parameters from raw xml data in <Parameter> elements
                switch (paramName)
                {
                    case "MaxValue":
                        double maxValue;
                        maxValue = paramValue == "MAX" //"MAX" is a flag that indicates a parameters has no maximum value
                            ? System.Double.MaxValue
                            : System.Convert.ToDouble(paramValue);
                        typedParams.Add(paramName, maxValue);
                        break;
                    case "MinValue":
                        double minValue;
                        minValue = paramValue == "MIN" //"MIN" is a flag that indicates a parameters has no maximum value
                            ? System.Double.MinValue
                            : System.Convert.ToDouble(paramValue);
                        typedParams.Add(paramName, minValue);
                        break;
                    case "ScenarioID":
                        int scenarioID;
                        scenarioID = System.Convert.ToInt32(paramValue);
                        typedParams.Add(paramName, scenarioID);
                        break;
                    case "TimeFrame":
                        Enumerators.TimeFrames timeFrame;
                        timeFrame = Enumerators.GetTimeFrameEnum(paramValue);
                        typedParams.Add(paramName, timeFrame);
                        break;
                    case "Zoning":
                        Enumerators.ZoningTypes zoning;
                        zoning = Enumerators.GetZoningEnum(paramValue);
                        typedParams.Add(paramName, zoning);
                        break;
                    case "LinkType":
                        Enumerators.LinkTypes linkType;
                        linkType = Enumerators.GetLinkTypeEnum(paramValue);
                        typedParams.Add(paramName, linkType);
                        break;
                    case "IsFraction":
                        bool isFraction;
                        isFraction = Convert.ToBoolean(paramValue);
                        typedParams.Add(paramName, isFraction);
                        break;
                    case "DiscoClass":
                        string discoClass;
                        discoClass = Convert.ToString(paramValue);
                        typedParams.Add(paramName, discoClass);
                        break;
                    case "MinPipeSize":
                        double minPipeSize;
                        minPipeSize = paramValue == "MIN"
                            ? System.Double.MinValue
                            : System.Convert.ToDouble(paramValue);
                        typedParams.Add(paramName, minPipeSize);
                        break;
                    case "MaxPipeSize":
                        double maxPipeSize;
                        maxPipeSize = paramValue == "MAX"
                            ? System.Double.MaxValue
                            : System.Convert.ToDouble(paramValue);
                        typedParams.Add(paramName, maxPipeSize);
                        break;
                    default:
                        break;
                }                
            }
            return typedParams;
        }
        private string HandleFunction(string functionName, ListDictionary typedParams)
        {
            switch (functionName)
            {
                case "PipeSurchargeCount":
                    return this.handlePipeSurchargeCount(typedParams);
                case "QQRatio":
                    return this.handleQQRatio(typedParams);
                case "QQRatioSummary":
                    return this.handleQQRatioSummary(typedParams);
                case "AreaByLanduse":
                    return this.handleAreaByLanduse(typedParams);
                case "SewerBackupRisk":
                    return this.handleSewerBackupRisk(typedParams);
                case "FalseBFRiskCount":
                    return this.handleFalseBFRiskCount(typedParams);
                case "PipeSegmentCount":
                    return this.handlePipeSegmentCount(typedParams);
                case "PipeSegmentLength":
                    return this.handlePipeSegmentLength(typedParams);
                case "MinPipeDiameter":
                    return this.handleMinPipeDiameter(typedParams);
                case "MaxPipeDiameter":
                    return this.handleMaxPipeDiameter(typedParams);
                case "PipeDiameterRange":
                    return this.handlePipeDiameterRange(typedParams);
                case "DSCCount":
                    return this.handleDscCount(typedParams);
                case "RoofDiscoCount":
                    return this.handleRoofDiscoCount(typedParams);
                case "RoofDiscoCountByLanduse":
                    return this.handleRoofDiscoCountByZoning(typedParams);
                case "PipeSurchargeSummary":
                    return this.handlePipeSurchargeSummary(typedParams);
                case "PipeSurchargeLength":
                    return this.handlePipeSurchargeLength(typedParams);
                case "PipeSurchargeTimeSummary":
                    return this.handlePipeSurchargeTimeSummary(typedParams);
                case "PipeSurchargeTimeLength":
                    return this.handlePipeSurchargeTimeLength(typedParams);
                case "CountByDiscoClass":
                    return this.handleCountByDiscoClass(typedParams);
                case "UICCount":
                    return this.handleUICCount(typedParams);
                case "UICForReviewCount":
                    return this.handleUICForReviewCount(typedParams);
                default:
                    return "UNKNOWN FUNCTION";
            }
        }
        #region Handlers for embedded xml functions - All return string
        private string handlePipeSurchargeCount(ListDictionary typedParams)
        {
            LinkHydraulics linkHyd;
            int count;
            bool isFraction = (bool)typedParams["IsFraction"];
            linkHyd = (LinkHydraulics)linkResults[(int)typedParams["ScenarioID"]];
            count = linkHyd.PipeSurchargeCount(
                (double)typedParams["MinValue"],
                (double)typedParams["MaxValue"]);
            return isFraction
                ? Convert.ToString(
                Math.Round((double)count / this.charLinks.Count, 2) * 100 + "%")
                : Convert.ToString(count);
        }
        private string handleQQRatio(ListDictionary typedParams)
        {
            LinkHydraulics linkHyd;
            int count;
            bool isFraction = (bool)typedParams["IsFraction"];
            linkHyd = (LinkHydraulics)linkResults[(int)typedParams["ScenarioID"]];
            count = linkHyd.CountByQQRatio(
                (double)typedParams["MinValue"],
                (double)typedParams["MaxValue"]);
            return isFraction
                ? Convert.ToString(
                Math.Round((double)count / this.charLinks.Count, 2) * 100 + "%")
                : Convert.ToString(count);
        }
        private string handleQQRatioSummary(ListDictionary typedParams)
        {
            LinkHydraulics linkHyd;
            double length;
            int count;
            linkHyd = (LinkHydraulics)linkResults[(int)typedParams["ScenarioID"]];
            length = Math.Round(linkHyd.CountByQQRatioLength(
                (double)typedParams["MinValue"],
                (double)typedParams["MaxValue"],
                (double)typedParams["MinPipeSize"],
                (double)typedParams["MaxPipeSize"]) / 5280.0, 1);
            count = linkHyd.CountByQQRatio(
                (double)typedParams["MinValue"],
                (double)typedParams["MaxValue"],
                (double)typedParams["MinPipeSize"],
                (double)typedParams["MaxPipeSize"]);
            return this.ToFixedDecimalString(length, 1) + " ("
                + Convert.ToString(count) + ")";
        }
        private string handleAreaByLanduse(ListDictionary typedParams)
        {
            int count = 0;
            bool isFraction = (bool)typedParams["IsFraction"];
            Enumerators.ZoningTypes zoning;
            zoning = (Enumerators.ZoningTypes)typedParams["Zoning"];
            if (zoning != Enumerators.ZoningTypes.OTH)
            {
                count = this.charDscs.AreaByZoning(zoning,
                    (Enumerators.TimeFrames)typedParams["TimeFrame"]);
            }
            else
            {
                int nonOTHCount = 0;
                foreach (Enumerators.ZoningTypes zo in Enum.GetValues(typeof(Enumerators.ZoningTypes)))
                {
                    nonOTHCount += this.charDscs.AreaByZoning(zo,
                        (Enumerators.TimeFrames)typedParams["TimeFrame"]);
                }
                count = this.charDscs.Values.Count - nonOTHCount;
                return "Not Implemented";
            }
            return isFraction
                ? Convert.ToString(
                Math.Round((double)count / this.charDscs.Count, 2) * 100 + "%")
                : Convert.ToString(count);
        }
        private string handleSewerBackupRisk(ListDictionary typedParams)
        {
            DscHydraulics dscHyd;
            int count;
            bool isFraction = (bool)typedParams["IsFraction"];
            dscHyd = (DscHydraulics)dscResults[(int)typedParams["ScenarioID"]];
            count = dscHyd.CountBySewerBackupRisk(
                (double)typedParams["MinValue"],
                (double)typedParams["MaxValue"]);
            return isFraction
                ? Convert.ToString(
                Math.Round((double)count / this.charDscs.Count, 2) * 100 + "%")
                : Convert.ToString(count);
        }
        private string handleFalseBFRiskCount(ListDictionary typedParams)
        {
            bool isFraction = (bool)typedParams["IsFraction"];
            int count;
            count = this.charDscs.CountByFalseBFRisk();
            return isFraction
                ? Convert.ToString(
                Math.Round((double)count / this.charDscs.Count, 2) * 100 + "%")
                : Convert.ToString(count);
        }
        private string handlePipeSegmentCount(ListDictionary typedParams)
        {
            int count;
            bool isFraction = (bool)typedParams["IsFraction"];
            count = this.charLinks.PipeSegmentCount(
                (Enumerators.LinkTypes)typedParams["LinkType"]);
            return isFraction
                ? Convert.ToString(
                Math.Round((double)count / this.charLinks.Count, 2) * 100 + "%")
                : Convert.ToString(count);
        }
        private string handlePipeSegmentLength(ListDictionary typedParams)
        {
            double length;
            bool isFraction = (bool)typedParams["IsFraction"];
            Enumerators.LinkTypes linkType = (Enumerators.LinkTypes)typedParams["LinkType"];
            length = (double)this.charLinks.PipeSegmentLength(linkType);

            return isFraction
                ? Convert.ToString(
                Math.Round(length / this.charLinks.PipeSegmentLength(linkType), 2) * 100 + "%")
                : this.ToFixedDecimalString(length / 5280, 1);
        }
        private string handleMinPipeDiameter(ListDictionary typedParams)
        {
            return Convert.ToString(
                this.charLinks.GetMinPipeDiam(
                (Enumerators.LinkTypes)typedParams["LinkType"]));
        }
        private string handleMaxPipeDiameter(ListDictionary typedParams)
        {
            return Convert.ToString(
                this.charLinks.GetMaxPipeDiam(
                (Enumerators.LinkTypes)typedParams["LinkType"])) + "\"";
        }
        private string handlePipeDiameterRange(ListDictionary typedParams)
        {
            return
                Convert.ToString(charLinks.GetMinPipeDiam(
                (Enumerators.LinkTypes)typedParams["LinkType"])) +
                "\" to " +
                Convert.ToString(charLinks.GetMaxPipeDiam(
                (Enumerators.LinkTypes)typedParams["LinkType"]) + "\"");
        }
        private string handleDscCount(ListDictionary typedParams)
        {
            return Convert.ToString(this.charDscs.Count);
        }
        private string handleRoofDiscoCount(ListDictionary typedParams)
        {
            int count;
            bool isFraction = (bool)typedParams["IsFraction"];
            count = this.charDscs.RoofDiscoCount(
                (double)typedParams["MinValue"],
                (double)typedParams["MaxValue"]);
            return isFraction
                ? Convert.ToString(
                Math.Round((double)count / this.charDscs.Count, 2) * 100 + "%")
                : Convert.ToString(count);
        }
        private string handleRoofDiscoCountByZoning(ListDictionary typedParams)
        {
            int count;
            bool isFraction = (bool)typedParams["IsFraction"];
            Enumerators.ZoningTypes zoning;
            zoning = (Enumerators.ZoningTypes)typedParams["Zoning"];
            if (zoning != Enumerators.ZoningTypes.OTH)
            {
                if (this.charDscs.CountByZoning(zoning,
                    (Enumerators.TimeFrames)typedParams["TimeFrame"]) == 0)
                {
                    return "N/A";
                }
                count = this.charDscs.RoofDiscoCountByZoning(zoning,
                    (Enumerators.TimeFrames)typedParams["TimeFrame"]);
                return isFraction
                    ? Convert.ToString(
                    Math.Round((double)count /
                    this.charDscs.CountByZoning(
                    (Enumerators.ZoningTypes)typedParams["Zoning"],
                    (Enumerators.TimeFrames)typedParams["TimeFrame"]),
                    2) * 100 + "%")
                    : Convert.ToString(count);
            }
            else
            {
                int nonOTHCount = 0;
                int nonOTHDiscoCount = 0;
                foreach (Enumerators.ZoningTypes zo in Enum.GetValues(typeof(Enumerators.ZoningTypes)))
                {
                    nonOTHDiscoCount += this.charDscs.RoofDiscoCountByZoning(zo,
                        (Enumerators.TimeFrames)typedParams["TimeFrame"]);
                    nonOTHCount += this.charDscs.CountByZoning(zo,
                        (Enumerators.TimeFrames)typedParams["TimeFrame"]);
                }

                if (nonOTHCount == 0) return "N/A";
                return isFraction
                    ? Convert.ToString(
                    Math.Round((double)nonOTHDiscoCount /
                    nonOTHCount,
                    2) * 100 + "%")
                    : Convert.ToString(nonOTHDiscoCount);
            }

        }
        private string handleCountByDiscoClass(ListDictionary typedParams)
        {
            string discoClass = (string)typedParams["DiscoClass"];
            int count = this.charDscs.CountByDiscoClass(discoClass);
            bool isFraction = (bool)typedParams["IsFraction"];
            return isFraction
                ? Convert.ToString(
                Math.Round((double)count / this.charDscs.Count, 2) * 100 + "%")
                : Convert.ToString(count);
        }
        private string handlePipeSurchargeSummary(ListDictionary typedParams)
        {
            LinkHydraulics linkHyd;
            int count;
            double length;
            linkHyd = (LinkHydraulics)linkResults[(int)typedParams["ScenarioID"]];
            length = Math.Round(linkHyd.PipeSurchargeLength(
                (double)typedParams["MinValue"],
                (double)typedParams["MaxValue"]) / 5280.0, 1);
            count = (int)linkHyd.PipeSurchargeCount(
                (double)typedParams["MinValue"],
                (double)typedParams["MaxValue"]);
            return this.ToFixedDecimalString(length, 1) + " ("
                + Convert.ToString(count) + ")";
        }
        private string handlePipeSurchargeLength(ListDictionary typedParams)
        {
            LinkHydraulics linkHyd;
            double surchargeLength;
            double totalLength;

            bool isFraction = (bool)typedParams["IsFraction"];

            linkHyd = (LinkHydraulics)linkResults[(int)typedParams["ScenarioID"]];
            surchargeLength = linkHyd.PipeSurchargeLength(
                (double)typedParams["MinValue"],
                (double)typedParams["MaxValue"]) / 5280.0;
            totalLength = linkHyd.PipeSurchargeLength(
                System.Double.MinValue, System.Double.MaxValue) / 5280.0;

            return isFraction
                ? Convert.ToString(
                Math.Round(
                surchargeLength /
                (charLinks.PipeSegmentLength() / 5280.0)
                , 2) * 100) + "%"
                : this.ToFixedDecimalString(surchargeLength / 5280, 1);
        }
        private string handlePipeSurchargeTimeSummary(ListDictionary typedParams)
        {
            double length = 0;
            int count = 0;
            double min;
            double max;
            min = (double)typedParams["MinValue"];
            max = (double)typedParams["MaxValue"];

            NodeHydraulics nodeHyd;
            nodeHyd = (NodeHydraulics)nodeResults[(int)typedParams["ScenarioID"]];

            foreach (Link l in this.charLinks.Values)
            {
                double usSurchargeTime = 0;
                double dsSurchargeTime = 0;
                if (nodeHyd.Contains(l.USNodeName))
                {
                    usSurchargeTime = nodeHyd[l.USNodeName].SurchargeTime;
                }
                if (nodeHyd.Contains(l.DSNodeName))
                {
                    dsSurchargeTime = nodeHyd[l.DSNodeName].SurchargeTime;
                }
                if ((usSurchargeTime >= min && usSurchargeTime <= max) ||
                     (dsSurchargeTime >= min && dsSurchargeTime <= max))
                {
                    length += l.Length;
                    count++;
                }
            }
            return this.ToFixedDecimalString(length / 5280, 1) + " (" + Convert.ToString(count) + ")";
        }
        private string handlePipeSurchargeTimeLength(ListDictionary typedParams)
        {
            double length = 0;

            double min;
            double max;
            min = (double)typedParams["MinValue"];
            max = (double)typedParams["MaxValue"];

            bool isFraction = (bool)typedParams["IsFraction"];

            NodeHydraulics nodeHyd;
            nodeHyd = (NodeHydraulics)nodeResults[(int)typedParams["ScenarioID"]];

            foreach (Link l in this.charLinks.Values)
            {
                double usSurchargeTime = 0;
                double dsSurchargeTime = 0;
                if (nodeHyd.Contains(l.USNodeName))
                {
                    usSurchargeTime = nodeHyd[l.USNodeName].SurchargeTime;
                }
                if (nodeHyd.Contains(l.DSNodeName))
                {
                    dsSurchargeTime = nodeHyd[l.DSNodeName].SurchargeTime;
                }
                if ((usSurchargeTime >= min && usSurchargeTime <= max) ||
                    (dsSurchargeTime >= min && dsSurchargeTime <= max))
                {
                    length += l.Length;
                }
            }
            return isFraction
                ? Convert.ToString(
                Math.Round(((double)length / this.charLinks.PipeSegmentLength()), 2) * 100 + "%")
                : this.ToFixedDecimalString(length / 5280, 1);
        }
        private string handleUICCount(ListDictionary typedParams)
        {
            //TODO: Move some of this AO code to a utils library...
            //Open a .lyr file as a GxFile, which can QI to GxLayer...
            ESRI.ArcGIS.Catalog.GxLayer sscGxLayer;
            sscGxLayer = new ESRI.ArcGIS.Catalog.GxLayerClass();
            ESRI.ArcGIS.Catalog.GxFile sscGxFile;
            sscGxFile = (ESRI.ArcGIS.Catalog.GxFile)sscGxLayer;
            sscGxFile.Path = this.mstSurfSC_FC;
            //Which can QI to an IFeatureLayer...
            ESRI.ArcGIS.Carto.IFeatureLayer sscFL;
            sscFL = (ESRI.ArcGIS.Carto.IFeatureLayer)sscGxLayer.Layer;
            //Which can QI to an IFeatureSelection, which will allow 
            //for selecting individual objects...
            ESRI.ArcGIS.Carto.IFeatureSelection sscFeatureSelection;
            sscFeatureSelection = (ESRI.ArcGIS.Carto.IFeatureSelection)sscFL;
            //And IFeatureLayer can also provide an IFeatureCursor for iterating...
            ESRI.ArcGIS.Geodatabase.IFeatureCursor sscCursor;
            sscCursor = sscFL.Search(null, false);

            ESRI.ArcGIS.Geodatabase.IFeature sscFeature;
            sscFeature = sscCursor.NextFeature();
            while (sscFeature != null)
            {
                string ngto;
                ngto = Convert.ToString(
                    sscFeature.get_Value(sscFeature.Fields.FindField("NGTO_EX")));
                if (this.charNodes.Contains(ngto))
                {
                    sscFeatureSelection.SelectionSet.Add(sscFeature.OID);
                }
                sscFeature = sscCursor.NextFeature();
            }

            //Gnarly ArcObjects code... the intent is to generate an IEnumGeometry
            //containing all the geometries from the feature selection created above.
            //The IEnumGeometry contains a collection of geometries and can be used 
            //to output a single unified geometry via 
            //GeometryFactory.CreateGeometryFromEnumerator.
            ESRI.ArcGIS.Geometry.IEnumGeometry enumGeom;
            ESRI.ArcGIS.Geodatabase.IEnumGeometryBind enumGeomBind;
            enumGeom = new ESRI.ArcGIS.Geodatabase.EnumFeatureGeometryClass();
            enumGeomBind = (ESRI.ArcGIS.Geodatabase.IEnumGeometryBind)enumGeom;
            enumGeomBind.BindGeometrySource(null, sscFeatureSelection.SelectionSet);

            ESRI.ArcGIS.Geometry.IGeometryFactory geomFactory;
            geomFactory = new ESRI.ArcGIS.Geometry.GeometryEnvironmentClass();

            ESRI.ArcGIS.Geometry.IGeometry geometryFilter;
            geometryFilter = geomFactory.CreateGeometryFromEnumerator(enumGeom);

            //Now that we have a Geometry representing the selected SSC objects
            //we can generate a spatial filter for use in spatial selection.
            ESRI.ArcGIS.Geodatabase.ISpatialFilter sscFilter;
            sscFilter = new ESRI.ArcGIS.Geodatabase.SpatialFilterClass();
            sscFilter.Geometry = geometryFilter;
            sscFilter.GeometryField = sscFL.FeatureClass.ShapeFieldName;
            sscFilter.SpatialRel = ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum.esriSpatialRelContains;

            //Finally, create the UIC layer...
            ESRI.ArcGIS.Catalog.GxLayer uicGxLayer;
            uicGxLayer = new ESRI.ArcGIS.Catalog.GxLayerClass();
            ESRI.ArcGIS.Catalog.GxFile uicGxFile;
            uicGxFile = (ESRI.ArcGIS.Catalog.GxFile)uicGxLayer;
            uicGxFile.Path = this.uicDB;

            ESRI.ArcGIS.Carto.IFeatureLayer uicFL;
            uicFL = (ESRI.ArcGIS.Carto.IFeatureLayer)uicGxLayer.Layer;
            //.. and define an IFeatureSelection...
            ESRI.ArcGIS.Carto.IFeatureSelection uicFeatureSelection;
            uicFeatureSelection = (ESRI.ArcGIS.Carto.IFeatureSelection)uicFL;
            //...and select from it using the spatial filter
            uicFeatureSelection.SelectFeatures(sscFilter,
                ESRI.ArcGIS.Carto.esriSelectionResultEnum.esriSelectionResultNew, false);
            //...and return the count of the UIC selection.
            return Convert.ToString(uicFeatureSelection.SelectionSet.Count);
            //TODO: Take some tylenol and put this in a library.
        }
        private string handleUICForReviewCount(ListDictionary typedParams)
        {
            //Open a .lyr file as a GxFile, which can QI to GxLayer...
            ESRI.ArcGIS.Catalog.GxLayer sscGxLayer;
            sscGxLayer = new ESRI.ArcGIS.Catalog.GxLayerClass();
            ESRI.ArcGIS.Catalog.GxFile sscGxFile;
            sscGxFile = (ESRI.ArcGIS.Catalog.GxFile)sscGxLayer;
            sscGxFile.Path = this.mstSurfSC_FC;
            //Which can QI to an IFeatureLayer...
            ESRI.ArcGIS.Carto.IFeatureLayer sscFL;
            sscFL = (ESRI.ArcGIS.Carto.IFeatureLayer)sscGxLayer.Layer;
            //Which can QI to an IFeatureSelection, which will allow 
            //for selecting individual objects...
            ESRI.ArcGIS.Carto.IFeatureSelection sscFeatureSelection;
            sscFeatureSelection = (ESRI.ArcGIS.Carto.IFeatureSelection)sscFL;
            //And IFeatureLayer can also provide an IFeatureCursor for iterating...
            ESRI.ArcGIS.Geodatabase.IFeatureCursor sscCursor;
            sscCursor = sscFL.Search(null, false);

            ESRI.ArcGIS.Geodatabase.IFeature sscFeature;
            sscFeature = sscCursor.NextFeature();
            while (sscFeature != null)
            {
                string ngto;
                ngto = Convert.ToString(
                    sscFeature.get_Value(sscFeature.Fields.FindField("NGTO_EX")));
                if (this.charNodes.Contains(ngto))
                {
                    sscFeatureSelection.SelectionSet.Add(sscFeature.OID);
                }
                sscFeature = sscCursor.NextFeature();
            }

            //Gnarly ArcObjects code... the intent is to generate an IEnumGeometry
            //containing all the geometries from the feature selection created above.
            //The IEnumGeometry contains a collection of geometries and can be used 
            //to output a single unified geometry via 
            //GeometryFactory.CreateGeometryFromEnumerator.
            ESRI.ArcGIS.Geometry.IEnumGeometry enumGeom;
            ESRI.ArcGIS.Geodatabase.IEnumGeometryBind enumGeomBind;
            enumGeom = new ESRI.ArcGIS.Geodatabase.EnumFeatureGeometryClass();
            enumGeomBind = (ESRI.ArcGIS.Geodatabase.IEnumGeometryBind)enumGeom;
            enumGeomBind.BindGeometrySource(null, sscFeatureSelection.SelectionSet);

            ESRI.ArcGIS.Geometry.IGeometryFactory geomFactory;
            geomFactory = new ESRI.ArcGIS.Geometry.GeometryEnvironmentClass();

            ESRI.ArcGIS.Geometry.IGeometry geometryFilter;
            geometryFilter = geomFactory.CreateGeometryFromEnumerator(enumGeom);

            //Now that we have a Geometry representing the selected SSC objects
            //we can generate a spatial filter for use in spatial selection.
            ESRI.ArcGIS.Geodatabase.ISpatialFilter sscFilter;
            sscFilter = new ESRI.ArcGIS.Geodatabase.SpatialFilterClass();
            sscFilter.Geometry = geometryFilter;
            sscFilter.GeometryField = sscFL.FeatureClass.ShapeFieldName;
            sscFilter.SpatialRel = ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum.esriSpatialRelContains;

            //Finally, create the UIC layer...
            ESRI.ArcGIS.Catalog.GxLayer uicGxLayer;
            uicGxLayer = new ESRI.ArcGIS.Catalog.GxLayerClass();
            ESRI.ArcGIS.Catalog.GxFile uicGxFile;
            uicGxFile = (ESRI.ArcGIS.Catalog.GxFile)uicGxLayer;
            uicGxFile.Path = this.uicDB;

            ESRI.ArcGIS.Carto.IFeatureLayer uicFL;
            uicFL = (ESRI.ArcGIS.Carto.IFeatureLayer)uicGxLayer.Layer;
            //.. and define an IFeatureSelection...
            ESRI.ArcGIS.Carto.IFeatureSelection uicFeatureSelection;
            uicFeatureSelection = (ESRI.ArcGIS.Carto.IFeatureSelection)uicFL;

            //...and select from it using the spatial filter
            uicFeatureSelection.SelectFeatures(sscFilter,
                ESRI.ArcGIS.Carto.esriSelectionResultEnum.esriSelectionResultNew, false);

            int count = 0;
            //Iterate through the UIC selection to find UICs matching the review criteria.
            ESRI.ArcGIS.Geodatabase.ICursor uicCursor;
            uicFeatureSelection.SelectionSet.Search(sscFilter, false, out uicCursor);
            ESRI.ArcGIS.Geodatabase.IRow uicRow;
            uicRow = uicCursor.NextRow();
            while (uicRow != null)
            {
                double gw_sepdist;
                double nr_wellmin;
                double in_tot;
                gw_sepdist = Convert.ToDouble(uicRow.get_Value(uicRow.Fields.FindField("GW_SEPDIST")));
                nr_wellmin = Convert.ToDouble(uicRow.get_Value(uicRow.Fields.FindField("nr_wellmin")));
                in_tot = Convert.ToDouble(uicRow.get_Value(uicRow.Fields.FindField("in_tot")));
                if (nr_wellmin <= 500 || in_tot == 1 || gw_sepdist <= 10)
                {
                    count++;
                }
                uicRow = uicCursor.NextRow();
            }

            bool isFraction = (bool)typedParams["IsFraction"];
            return isFraction ? Convert.ToString(
                Math.Round(count /
                (uicFeatureSelection.SelectionSet.Count + System.Double.Epsilon),
                2) * 100) + "%"
                : Convert.ToString(count);


        }
        #endregion

        private void parseEmbeddedMultiRows()
        {
            System.Xml.XmlNodeList xmlNodeList;

            xmlNodeList = xmlDoc.GetElementsByTagName("Row");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (xmlNode.Attributes["isMultiRow"] == null || xmlNode.Attributes["isMultiRow"].Value != "true")
                {
                    continue;
                }                                
                
                XmlNode[] multiRowResultsNodes = parseEmbeddedMultiRow(xmlNode);

                foreach (XmlNode newNode in multiRowResultsNodes)
                {
                    xmlNode.ParentNode.AppendChild(newNode);
                }
                xmlNode.ParentNode.RemoveChild(xmlNode);                              
            }
        }
		private XmlNode[] parseEmbeddedMultiRow(XmlNode xmlNode)
		{
            string multiRowKey = xmlNode.Attributes["multiRowKey"].Value;            
						            					
			switch (multiRowKey)
			{
                case "DiversionMultiRow":
                    return handleDiversionMultiRow(xmlNode);															
                case "SewerSystemByFEC":                    
                case "ZoningByFEC":                    
				default:
                    return null;					
			}						
		}

        private XmlNode[] handleDiversionMultiRow(XmlNode xmlNode)
		{
			DiversionStructures divStructs;
			divStructs = new DiversionStructures(charNodes);

            XmlNode[] diversionXmlNodes = new XmlNode[divStructs.Count];            
            int i = 0;
            foreach (DiversionStructure div in divStructs.Values)
            {
                XmlNode diversionXmlNode = diversionXmlNodes[i] = xmlNode.Clone();
                foreach (XmlNode diversionInfoXmlNode in diversionXmlNode.ChildNodes)
                {
                    string diversionAttribute;
                    if (diversionInfoXmlNode.Attributes["cellType"].Value != "FUNCTION")
                    {
                        continue;
                    }
                    switch (diversionInfoXmlNode.ChildNodes[0].Attributes["functionName"].Value)
                    {
                        case "DiversionLocation":
                            diversionAttribute = div.Address;
                            break;
                        case "DiversionNodeName":
                            diversionAttribute = div.NodeName;
                            break;
                        case "DiversionID":
                            diversionAttribute = div.DiversionName;
                            break;
                        case "DiversionNodeType":
                            diversionAttribute = "";
                            break;
                        case "DiversionStatus":
                            diversionAttribute = div.Status;
                            break;
                        case "DiversionOutfall":
                            if (div.Outfall == String.Empty)
                            {
                                diversionAttribute = String.Empty;
                            }
                            else if (Char.IsDigit(div.Outfall[0]))
                            {
                                diversionAttribute = "OF " + div.Outfall;
                            }
                            else
                            {
                                diversionAttribute = div.Outfall;
                            }
                            break;
                        default:
                            diversionAttribute = "";
                            break;
                    }
                    diversionInfoXmlNode.RemoveAll();
                    diversionInfoXmlNode.Attributes.Append(xmlDoc.CreateAttribute("cellType"));
                    diversionInfoXmlNode.Attributes.Append(xmlDoc.CreateAttribute("data"));
                    diversionInfoXmlNode.Attributes["cellType"].Value = "FUNCTION";
                    diversionInfoXmlNode.Attributes["data"].Value = diversionAttribute;
                }
                i++;
            }
            return diversionXmlNodes;		
		}
        private XmlNode[] handleSewerSystemByFEC(XmlNode xmlNode)
        {
            if (fecs == null)
            {
                fecs = new FlowEstimationCatchments();
            }
            XmlNode[] fecXmlNodes = new XmlNode[fecs.Count];
            int i = 0;
            foreach (FlowEstimationCatchment fec in fecs.Values)
            {
                XmlNode fecXmlNode = fecXmlNodes[i] = xmlNode.Clone();
                foreach (XmlNode fecInfoXmlNode in fecXmlNode.ChildNodes)
                {
                }
                i++;
            }
            return null;
        }
        private XmlNode[] handleZoningByFEC(XmlNode xmlNode)
        {
            if (fecs == null)
            {
                fecs = new FlowEstimationCatchments(charLinks);
            }
            XmlNode[] fecXmlNodes = new XmlNode[fecs.Count];

            Links stopLinks = new Links();
            foreach (FlowEstimationCatchment fec in fecs.Values)
            {
                stopLinks.Add(charLinks.FindByMLinkID(fec.MLinkID));
            }
            int i = 0;
            foreach (FlowEstimationCatchment fec in fecs.Values)
            {
                Link startLink = charLinks.FindByMLinkID(fec.MLinkID);
                Links startLinks = new Links();
                Links fecLinks;
                Dscs fecDscs;

                if (startLink == null)
                {
                    continue;
                }
                startLinks.Add(startLink);
                stopLinks.Remove(startLink.LinkID);
                charLinks.SelectSubNetwork(startLinks, stopLinks);
                fecLinks = charLinks.GetSubNetwork();
                fecDscs = new Dscs();
                fecDscs.Load(fecLinks);
                
                XmlNode fecXmlNode = fecXmlNodes[i] = xmlNode.Clone();
                foreach (XmlNode fecInfoXmlNode in fecXmlNode.ChildNodes)
                {
                    string fecAttribute;
                    if (fecInfoXmlNode.Attributes["cellType"].Value != "FUNCTION")
                    {
                        continue;
                    }
                    switch (fecXmlNode.ChildNodes[0].Attributes["functionName"].Value)
                    {
                        case "FECName":
                            fecAttribute = fec.FecName;
                            break;
                        case "FEMethod":
                            fecAttribute = Convert.ToString(fec.FEMethod);
                            break;
                        case "FECTotalArea":
                            fecAttribute = Convert.ToString(fecDscs.Area());
                            break;
                        case "FECSeweredArea":
                            fecAttribute = Convert.ToString(fecDscs.AreaBySewerable(1));
                            break;
                        case "FECSeweredCount":
                            fecAttribute = Convert.ToString(fecDscs.CountBySewerable(1));
                            break; 
                        case "FECSFRArea":
                            fecAttribute = Convert.ToString(fecDscs.AreaByZoning(Enumerators.ZoningTypes.SFR, Enumerators.TimeFrames.EX));
                            break;
                        case "FECMFRArea":
                            fecAttribute = Convert.ToString(fecDscs.AreaByZoning(Enumerators.ZoningTypes.MFR, Enumerators.TimeFrames.EX));
                            break;
                        case "FECINDArea":
                            fecAttribute = Convert.ToString(fecDscs.AreaByZoning(Enumerators.ZoningTypes.IND, Enumerators.TimeFrames.EX));
                            break;
                        case "FECCOMArea":
                            fecAttribute = Convert.ToString(fecDscs.AreaByZoning(Enumerators.ZoningTypes.COM, Enumerators.TimeFrames.EX));
                            break;
                        case "FECPOSArea":
                            fecAttribute = Convert.ToString(fecDscs.AreaByZoning(Enumerators.ZoningTypes.POS, Enumerators.TimeFrames.EX));
                            break;
                        default:
                            fecAttribute = "";
                            break;
                    }
                    fecInfoXmlNode.RemoveAll();
                    fecInfoXmlNode.Attributes.Append(xmlDoc.CreateAttribute("cellType"));
                    fecInfoXmlNode.Attributes.Append(xmlDoc.CreateAttribute("data"));
                    fecInfoXmlNode.Attributes["cellType"].Value = "FUNCTION";
                    fecInfoXmlNode.Attributes["data"].Value = fecAttribute;
                }

                stopLinks.Add(startLink);
                i++;
            }
            return null;
        }


        private void parseEmbeddedMultiTables()
        {
        }
		private void parseEmbeddedMultiTable(string xmlFragment, XmlReader xmlReader, XmlWriter xmlWriter)
		{
			string multiTableKey = "";			

			XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlReader.NameTable);									
			//Create the XmlParserContext.
			XmlParserContext context = new XmlParserContext(xmlReader.NameTable, nsmgr, null, XmlSpace.None);
			XmlTextReader functionReader = new XmlTextReader(xmlFragment, XmlNodeType.Element, context);

			while (functionReader.Read())
			{
				if (functionReader.Name == "CharacterizationTable")
				{
					multiTableKey = functionReader.GetAttribute("multiTableKey");
					break;
				}
			}			
			switch (multiTableKey)
			{
				case "PumpStationList":
					handlePumpStationMultiTable(functionReader.ReadOuterXml(), functionReader, xmlWriter);										
					break;
				default:
					break;
			}			
			//return returnFragment;
		}
		
        /*Pump Station is a special case. A study area may have any number of pump stations,
         * which may have any number of wetwells, forcemains and pumps.
         * Wetwell, forcemain and pump multirows are different from the multirow functions called by
         * parseEmbeddedMultiRow since they require a PumpStation object */                
		private void handlePumpStationMultiTable(string xmlFragment, XmlTextReader functionReader, XmlWriter xmlWriter)
		{
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(functionReader.NameTable);									
			//Create the XmlParserContext.
			XmlParserContext context = new XmlParserContext(functionReader.NameTable, nsmgr, null, XmlSpace.None);			

			PumpStations pumpStations;
			pumpStations = new PumpStations(charNodes, this.psDB);
	
			int psCount = pumpStations.Count;
			int psNum = 1;
			if (psCount == 0) //TODO: Write "This study area has no pump stations"
			{ 
				functionReader = new XmlTextReader(xmlFragment, XmlNodeType.Element, context);			
				functionReader.MoveToContent();
				string colCount = functionReader.GetAttribute("columnCount");
				xmlWriter.WriteStartElement("CharacterizationTable");				
				xmlWriter.WriteAttributes(functionReader, true);
				xmlWriter.WriteStartElement("Row");
				xmlWriter.WriteStartElement("Cell");
				xmlWriter.WriteAttributeString("cellType", "COLUMNNAME");
				xmlWriter.WriteAttributeString("data", "There are no pump stations present in this study area.");							
				xmlWriter.WriteAttributeString("colSpan", colCount);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				return;
			}
			foreach (PumpStation ps in pumpStations.Values)
			{	
				string tableQualifier = "";				
				if (psCount != 1)
				{
					char tableID = (char)(psNum+96);
					tableQualifier=Convert.ToString(tableID);
					psNum++;
				}
				functionReader = new XmlTextReader(xmlFragment, XmlNodeType.Element, context);				
				while (functionReader.Read())
				{
					if (functionReader.NodeType == System.Xml.XmlNodeType.Whitespace)
					{ continue; }
					else if (functionReader.NodeType == System.Xml.XmlNodeType.EndElement)
					{
						xmlWriter.WriteEndElement();
					}
					else if (functionReader.Name == "CharacterizationTable" &&
						functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
					{
						xmlWriter.WriteStartElement("CharacterizationTable");
						while (functionReader.MoveToNextAttribute())
						{
							if (functionReader.Name == "tableNumber")
							{
								xmlWriter.WriteAttributeString(functionReader.Name, functionReader.Value + tableQualifier);
							}
							else
							{
								xmlWriter.WriteAttributeString(functionReader.Name, functionReader.Value);
							}
						}

					}
					else if (functionReader.Name == "Row" && functionReader.GetAttribute("isMultiRow") == "true" &&
						functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
					{																				
						switch(functionReader.GetAttribute("multiRowKey"))
						{
							case "PSWetWellList":
								handleWetWellMultiRow(ps, functionReader.ReadInnerXml(), functionReader, xmlWriter);
								break;
							case "PSForceMainList":								
								handleForceMainMultiRow(ps, functionReader.ReadInnerXml(), functionReader, xmlWriter);
								break;
							case "PSPumpList":								
								handlePumpMultiRow(ps, functionReader.ReadInnerXml(), functionReader, xmlWriter);
								break;
							default:
								break;
						}						
						//functionReader.Skip();
					}
					else if (functionReader.Name == "Cell" && functionReader.GetAttribute("cellType") == "FUNCTION" &&
						functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
					{
						xmlWriter.WriteStartElement("Cell");													
						while (functionReader.MoveToNextAttribute())
						{	
							if (functionReader.Name != "data")
							{
								xmlWriter.WriteAttributeString(functionReader.Name, functionReader.Value);									
							}								
						}
					}
					else if (functionReader.Name == "Function" && 
						functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
					{
						string functionValue;
						switch(functionReader.GetAttribute("functionName"))
						{
							case "PSName":
								functionValue = ps.Name + " Pump Station";
								break;
							case "PSType":
								functionValue = ps.Type;
								break;
							case "PSLocation":
								functionValue = ps.Address;
								break;
							case "PSFirmCapacity":
								functionValue = Convert.ToString(ps.FirmCapacity) + " gpm";
								break;
							case "PSFullCapacity":
								functionValue = Convert.ToString(ps.FullCapacity) + " gpm";
								break;
							default:
								functionValue = "";
								break;
						}
						xmlWriter.WriteAttributeString("data", functionValue);
						functionReader.Skip();										
					}
					else 
					{
						xmlWriter.WriteStartElement(functionReader.Name);
						while (functionReader.MoveToNextAttribute())
						{
							xmlWriter.WriteAttributeString(functionReader.Name, functionReader.Value);
						}												
					}

				}															
			}			
		}
        private void handleWetWellMultiRow(PumpStation ps, string xmlFragment, XmlReader functionReader, XmlWriter xmlWriter)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(functionReader.NameTable);
            //Create the XmlParserContext.
            XmlParserContext context = new XmlParserContext(functionReader.NameTable, nsmgr, null, XmlSpace.None);

            foreach (WetWell ww in ps.WetWells)
            {
                functionReader = new XmlTextReader(xmlFragment, XmlNodeType.Element, context);
                xmlWriter.WriteStartElement("Row");

                while (functionReader.Read())
                {
                    if (functionReader.Name == "Cell" &&
                        functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
                    {
                        xmlWriter.WriteStartElement("Cell");
                        while (functionReader.MoveToNextAttribute())
                        {
                            if (functionReader.Name != "data")
                            {
                                xmlWriter.WriteAttributeString(functionReader.Name, functionReader.Value);
                            }
                        }
                    }
                    else if (functionReader.Name == "Function" &&
                        functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
                    {
                        string functionValue;
                        string functionName = functionReader.GetAttribute("functionName");
                        switch (functionName)
                        {
                            case "PSWetWellNum":
                                if (ps.WetWells.Count > 1)
                                {
                                    functionValue = Convert.ToString(ww.Number);
                                }
                                else
                                {
                                    functionValue = "";
                                }
                                break;
                            case "PSWetWellDimensions":
                                if (ww.Shape == "Rectangular")
                                {
                                    functionValue = ww.Shape + " ("
                                        + ww.Dimension1 + " ft x "
                                        + ww.Dimension2 + " ft)";
                                }
                                else if (ww.Shape == "Circular")
                                {
                                    functionValue = ww.Shape
                                        + " (diam. = " + ww.Dimension1 + " ft)";
                                }
                                else
                                {
                                    functionValue = "Irregular";
                                }
                                break;
                            case "PSWetWellVolume":
                                functionValue = Convert.ToString(ww.Volume) + " ft^3/ft";
                                break;
                            case "PSWetWellBypass":
                                functionValue = ww.ByPass;
                                break;
                            default:
                                functionValue = "INVALID WetWell FUNCTION";
                                break;

                        }
                        xmlWriter.WriteAttributeString("data", functionValue);
                        functionReader.Skip();
                    }
                    else if (functionReader.NodeType == System.Xml.XmlNodeType.EndElement)
                    {
                        xmlWriter.WriteEndElement();
                    }
                }
                xmlWriter.WriteEndElement();
            }
        }
        private void handleForceMainMultiRow(PumpStation ps, string xmlFragment, XmlReader functionReader, XmlWriter xmlWriter)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(functionReader.NameTable);
            //Create the XmlParserContext.
            XmlParserContext context = new XmlParserContext(functionReader.NameTable, nsmgr, null, XmlSpace.None);

            int fmNum = 1;
            foreach (ForceMain fm in ps.ForceMains)
            {
                functionReader = new XmlTextReader(xmlFragment, XmlNodeType.Element, context);
                xmlWriter.WriteStartElement("Row");

                while (functionReader.Read())
                {
                    if (functionReader.Name == "Cell" &&
                        functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
                    {
                        xmlWriter.WriteStartElement("Cell");
                        while (functionReader.MoveToNextAttribute())
                        {
                            if (functionReader.Name != "data")
                            {
                                xmlWriter.WriteAttributeString(functionReader.Name, functionReader.Value);
                            }
                        }
                    }
                    else if (functionReader.Name == "Function" &&
                        functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
                    {
                        string functionValue;
                        string functionName = functionReader.GetAttribute("functionName");
                        switch (functionName)
                        {
                            case "PSForceMainID":
                                if (ps.ForceMains.Count > 1)
                                {
                                    functionValue = Convert.ToString(fmNum);
                                    fmNum++;
                                }
                                else
                                {
                                    functionValue = "";
                                }
                                break;
                            case "PSForceMainDiameter":
                                if (charLinks.FindByMLinkID(fm.MLinkID) != null)
                                {
                                    functionValue = Convert.ToString(
                                        charLinks.FindByMLinkID(fm.MLinkID).Diameter) + " in";
                                }
                                else
                                {
                                    functionValue = "Unknown";
                                }
                                break;
                            case "PSForceMainLength":
                                functionValue = Convert.ToString(fm.Length) + " ft";
                                break;
                            case "PSForceMainHead":
                                functionValue = Convert.ToString(fm.StaticHead) + " ft";
                                break;
                            default:
                                functionValue = "INVALID ForceMain FUNCTION";
                                break;

                        }
                        xmlWriter.WriteAttributeString("data", functionValue);
                        functionReader.Skip();
                    }
                    else if (functionReader.NodeType == System.Xml.XmlNodeType.EndElement)
                    {
                        xmlWriter.WriteEndElement();
                    }
                }
                xmlWriter.WriteEndElement();
            }
        }
        private void handlePumpMultiRow(PumpStation ps, string xmlFragment, XmlReader functionReader, XmlWriter xmlWriter)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(functionReader.NameTable);
            //Create the XmlParserContext.
            XmlParserContext context = new XmlParserContext(functionReader.NameTable, nsmgr, null, XmlSpace.None);

            foreach (Pump pmp in ps.Pumps)
            {
                functionReader = new XmlTextReader(xmlFragment, XmlNodeType.Element, context);
                xmlWriter.WriteStartElement("Row");

                while (functionReader.Read())
                {
                    if (functionReader.Name == "Cell" &&
                        functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
                    {
                        xmlWriter.WriteStartElement("Cell");
                        while (functionReader.MoveToNextAttribute())
                        {
                            if (functionReader.Name != "data")
                            {
                                xmlWriter.WriteAttributeString(functionReader.Name, functionReader.Value);
                            }
                        }
                    }
                    else if (functionReader.Name == "Function" &&
                        functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
                    {
                        string functionValue;
                        string functionName = functionReader.GetAttribute("functionName");
                        switch (functionName)
                        {
                            case "PSPumpNumber":
                                if (pmp.PumpNumber == 1)
                                {
                                    functionValue = "Lead Pump";
                                }
                                else if (pmp.PumpNumber == 2)
                                {
                                    functionValue = "Lag Pump";
                                }
                                else
                                {
                                    functionValue = "Lag "
                                        + Convert.ToString(pmp.PumpNumber - 1)
                                        + " Pump";
                                }
                                break;
                            case "PSPumpMotorSpeed":
                                functionValue = Convert.ToString(
                                    pmp.MotorSpeed) + " rpm";
                                break;
                            case "PSPumpHP":
                                functionValue = Convert.ToString(pmp.HorsePower) + " hp";
                                break;
                            case "PSPumpCapacity":
                                functionValue = Convert.ToString(pmp.Capacity) + " gpm";
                                break;
                            default:
                                functionValue = "INVALID ForceMain FUNCTION";
                                break;

                        }
                        xmlWriter.WriteAttributeString("data", functionValue);
                        functionReader.Skip();
                    }
                    else if (functionReader.NodeType == System.Xml.XmlNodeType.EndElement)
                    {
                        xmlWriter.WriteEndElement();
                    }
                }
                xmlWriter.WriteEndElement();
            }

        }
				
		/// <summary>
		/// Event that occurs when CharacterizationEngine reports that it's status has changed.
		/// </summary>
		public event OnStatusChangedEventHandler StatusChanged;
		
		/// <summary>
		/// Internally called function that fires the OnStatusChanged event.
		/// </summary>
		/// <param name="e">Parameter that contains the string describing the new state.</param>
		protected virtual void OnStatusChanged(StatusChangedArgs e) 
		{
			if (StatusChanged != null)
				StatusChanged(e);
		}		
	
		private string ToFixedDecimalString(double d, int i)
		{
			string s = Convert.ToString(Math.Round(d, i));
			if (s.IndexOf(".") == -1)
			{
				s += ".";
			}
			string integerString = s.Substring(0, s.IndexOf("."));
			string decimalString = s.Substring(s.IndexOf(".") + 1);
			
			while (decimalString.Length < i)
			{
				decimalString += "0";
			}
			s = integerString + "." + decimalString;
			return s;
		}
	}
}
