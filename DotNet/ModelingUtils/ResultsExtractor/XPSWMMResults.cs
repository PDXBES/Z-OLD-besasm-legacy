using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.Odbc;
using System.Data.OleDb;
using SystemsAnalysis.Utils.AccessUtils;
using System.Globalization;

namespace SystemsAnalysis.Modeling.ModelUtils.ResultsExtractor
{
  /// <summary>
  /// Extracts results from a version 11.2-12.0 (Retail 9.0-10.6, Engine 8.87-10.60)
  /// XP-SWMM output file. Will extract Table E09, Table E10, Table E18, Table E19 and Table E20. Output
  /// can be obtained through the GetTableXX() functions, which return strongly-typed 
  /// DataSets included in this assembly, or by calling WriteToAccessDatabase() which
  /// places the output tables in the provided database.
  /// </summary>
  public class XPSWMMResults
  {
    private string outputFile;
    private StreamReader outputReader;

    private TableE01DataSet.TableE01DataTable tableE01;
    //TO-DO: implement private TableE02DataSet.TableE02DataTable tableE02;
    private TableE03aDataSet.TableE03aDataTable tableE03a;
    private TableE03bDataSet.TableE03bDataTable tableE03b;
    //TO-DO: implement private TableE04DataSet.TableE04DataTable tableE04;
    //TO-DO: implement private TableE04aDataSet.TableE04aDataTable tableE04a;
    //TO-DO: implement private TableE04bDataSet.TableE04bDataTable tableE04b;
    //TO-DO: implement private TableE05DataSet.TableE05DataTable tableE05;
    //TO-DO: implement private TableE05aDataSet.TableE05aDataTable tableE05a;
    //TO-DO: implement private TableE06DataSet.TableE06DataTable tableE06;
    //TO-DO: implement private TableE07DataSet.TableE07DataTable tableE07;
    //TO-DO: implement private TableE08DataSet.TableE08DataTable tableE08;
    private TableE09DataSet.TableE09DataTable tableE09;
    private TableE10DataSet.TableE10DataTable tableE10;
    //TO-DO: implement private TableE11DataSet.TableE11DataTable tableE11;
    //TO-DO: implement private TableE12DataSet.TableE12DataTable tableE12;
    //TO-DO: implement private TableE13DataSet.TableE13DataTable tableE13;
    //TO-DO: implement private TableE13aDataSet.TableE13aDataTable tableE13a;
    //TO-DO: implement private TableE14DataSet.TableE14DataTable tableE14;
    //TO-DO: implement private TableE14aDataSet.TableE14aDataTable tableE14a;
    //TO-DO: implement private TableE14bDataSet.TableE14bDataTable tableE14b;
    //TO-DO: implement private TableE15DataSet.TableE15DataTable tableE15;
    //TO-DO: implement private TableE15aDataSet.TableE15aDataTable tableE15a;
    //TO-DO: implement private TableE16DataSet.TableE16DataTable tableE16;
    //TO-DO: implement private TableE17DataSet.TableE17DataTable tableE17;
    private TableE18DataSet.TableE18DataTable tableE18;
    private TableE19DataSet.TableE19DataTable tableE19;
    private TableE20DataSet.TableE20DataTable tableE20;
    //TO-DO: implement private TableE21DataSet.TableE21DataTable tableE21;
    private TableE22DataSet.TableE22DataTable tableE22;

    private DateTime beginDateTime;

    /// <summary>
    /// Creates a new XPSWMMResults processor.
    /// </summary>
    /// <param name="outputFile">The output file to extract results from</param>
    public XPSWMMResults(string outputFile)
    {
      outputReader = new StreamReader(outputFile);

      ValidateOutputFile();

      this.outputFile = outputFile;
      this.beginDateTime = ExtractStartDateTime();

      tableE01 = new TableE01DataSet.TableE01DataTable();
      //tableE02 = new TableE02DataSet.TableE02DataTable();
      tableE03a = new TableE03aDataSet.TableE03aDataTable();
      tableE03b = new TableE03bDataSet.TableE03bDataTable();
      //tableE04 = new TableE04DataSet.TableE04DataTable();
      //tableE05 = new TableE05DataSet.TableE05DataTable();
      //tableE05a = new TableE05aDataSet.TableE05aDataTable();
      //tableE06 = new TableE06DataSet.TableE06DataTable();
      //tableE07 = new TableE07DataSet.TableE07DataTable();
      //tableE08 = new TableE08DataSet.TableE08DataTable();
      tableE09 = new TableE09DataSet.TableE09DataTable();
      tableE10 = new TableE10DataSet.TableE10DataTable();
      //tableE11 = new TableE11DataSet.TableE11DataTable();
      //tableE12 = new TableE12DataSet.TableE12DataTable();
      //tableE13 = new TableE13DataSet.TableE13DataTable();
      //tableE13a = new TableE13aDataSet.TableE13aDataTable();
      //tableE14 = new TableE14DataSet.TableE14DataTable();
      //tableE14a = new TableE14aDataSet.TableE14aDataTable();
      //tableE14b = new TableE14bDataSet.TableE14bDataTable();
      //tableE15 = new TableE15DataSet.TableE15DataTable();
      //tableE15a = new TableE15DataSet.TableE15aDataTable();
      //tableE16 = new TableE16DataSet.TableE16DataTable();
      //tableE17 = new TableE17DataSet.TableE17DataTable();
      tableE18 = new TableE18DataSet.TableE18DataTable();
      tableE19 = new TableE19DataSet.TableE19DataTable();
      tableE20 = new TableE20DataSet.TableE20DataTable();
      //tableE21 = new TableE21DataSet.TableE21DataTable();
      tableE22 = new TableE22DataSet.TableE22DataTable();

      ExtractTableE01();
      //ExtractTableE02();
      ExtractTableE03a();
      ExtractTableE03b();
      //ExtractTableE04();
      //ExtractTableE04a();
      //ExtractTableE04b();
      //ExtractTableE05();
      //ExtractTableE05a();
      //ExtractTableE06();
      //ExtractTableE07();
      //ExtractTableE08();
      ExtractTableE09();
      ExtractTableE10();
      //ExtractTableE11();
      //ExtractTableE12();
      //ExtractTableE13();
      //ExtractTableE13a();
      //ExtractTableE14();
      //ExtractTableE14a();
      //ExtractTableE14b();
      //ExtractTableE15();
      //ExtractTableE16();
      //ExtractTableE17();
      ExtractTableE18();
      ExtractTableE19();
      ExtractTableE20();
      //ExtractTableE21();
      ExtractTableE22();
    }

    private void ValidateOutputFile()
    {
      string currentLine;
      string[] tokens;
      do
      {
        currentLine = outputReader.ReadLine();
        if (outputReader.EndOfStream)
        {
          throw new Exception("Output file is not valid. Could not determine file version.");
        }
      } while (!currentLine.Contains("Data File Version"));

      tokens = currentLine.Split(new char[] { ' ', '|' }, StringSplitOptions.RemoveEmptyEntries);
      if (Convert.ToDouble(tokens[4]) < 11.2 || Convert.ToDouble(tokens[4]) > 12.0)
      {
        throw new Exception("Output file was version " + tokens[4] + ". Expected 11.2-11.9.");
      }
      return;
    }

    private DateTime ExtractStartDateTime()
    {
      string currentLine;
      string[] tokens;
      int beginYear, beginMonth, beginDay, beginHour, beginMinute, beginSecond;

      do
      {
        currentLine = outputReader.ReadLine();
        if (outputReader.EndOfStream)
        {
          throw new Exception("Could not find output Start Time.");
        }
      } while (!currentLine.Contains("Time Control from Hydraulics Job Control"));

      currentLine = outputReader.ReadLine();
      tokens = currentLine.Split(new char[] { ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);
      beginYear = Convert.ToInt32(tokens[1]);
      beginYear += beginYear < 100 ? 1900 : 0;
      beginMonth = Convert.ToInt32(tokens[3]);

      currentLine = outputReader.ReadLine();
      tokens = currentLine.Split(new char[] { ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);
      beginDay = Convert.ToInt32(tokens[1]);
      beginHour = Convert.ToInt32(tokens[3]);

      currentLine = outputReader.ReadLine();
      tokens = currentLine.Split(new char[] { ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);
      beginMinute = Convert.ToInt32(tokens[1]);
      beginSecond = Convert.ToInt32(tokens[3]);

      beginDateTime = new DateTime(beginYear, beginMonth, beginDay, beginHour, beginMinute, beginSecond);

      return beginDateTime;
    }

    /// <summary>
    /// Gets the output file associated with this instance of XPSWMMResults.
    /// </summary>
    public string OutputFile
    {
      get { return this.outputFile; }
    }

    /// <summary>
    /// Gets the date and time that the XP-SWMM output starts at.
    /// </summary>
    public DateTime BeginDateTime
    {
      get { return this.beginDateTime; }
    }

    private int ExtractTableE01()
    {
      string currentLine = "";
      string[] tokens;
      string condName;
      double length;
      string lenString;
      string condClass;
      double area;
      string areaString;
      double manningsN;
      string mannNString;
      double maxWidth;
      string maxWidthString;
      double depth;
      string depthString;

      do
      {
        currentLine = outputReader.ReadLine();
        if (outputReader.EndOfStream)
        {
          throw new Exception("Table E01 not found within " + this.outputFile);
        }
      } while (!currentLine.Contains("Table E1 - Conduit Data"));

      try
      {
        for (int i = 0; i < 7; i++)
        {
          currentLine = outputReader.ReadLine();
        }

        while (!currentLine.Contains("Total length"))
        {
          tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

          condName = tokens[1];
          lenString = tokens[2];
          //length = Convert.ToDouble(tokens[2]);
          length = Double.Parse(lenString, NumberStyles.Any, CultureInfo.InvariantCulture);
          condClass = tokens[3];
          if (condClass == "Closd")
          {
            areaString = tokens[5];
            //area = Convert.ToDouble(tokens[5]);
            area = Double.Parse(areaString, NumberStyles.Any, CultureInfo.InvariantCulture);
            mannNString = tokens[6];
            //manningsN = Convert.ToDouble(tokens[6]);
            manningsN = Double.Parse(mannNString, NumberStyles.Any, CultureInfo.InvariantCulture);
            maxWidthString = tokens[7];
            //maxWidth = Convert.ToDouble(tokens[7]);
            maxWidth = Double.Parse(maxWidthString, NumberStyles.Any, CultureInfo.InvariantCulture);
            depthString = tokens[8];
            //depth = Convert.ToDouble(tokens[8]);
            depth = Double.Parse(depthString, NumberStyles.Any, CultureInfo.InvariantCulture);
          }
          else
          {
            areaString = tokens[4];
            //area = Convert.ToDouble(tokens[4]);
            area = Double.Parse(areaString, NumberStyles.Any, CultureInfo.InvariantCulture);
            mannNString = tokens[5];
            //manningsN = Convert.ToDouble(tokens[5]);
            manningsN = Double.Parse(mannNString, NumberStyles.Any, CultureInfo.InvariantCulture);
            maxWidthString = tokens[6];
            //maxWidth = Convert.ToDouble(tokens[6]);
            maxWidth = Double.Parse(maxWidthString, NumberStyles.Any, CultureInfo.InvariantCulture);
            depthString = tokens[7];
            //depth = Convert.ToDouble(tokens[7]);
            depth = Double.Parse(depthString, NumberStyles.Any, CultureInfo.InvariantCulture);
          }

          tableE01.AddTableE01Row(condName, length, condClass, area, manningsN, maxWidth, depth);

          currentLine = outputReader.ReadLine();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Error parsing Table E01: " + ex.Message);
      }

      return tableE01.Count;
    }

    private int ExtractTableE02()
    {
      return 0;
    }

    private int ExtractTableE03a()
    {
      string currentLine = "";
      string[] tokens;
      string nodeName;
      string grndElString;
      double groundElevFt;
      string crownElString;
      double crownElevFt;
      string invElString;
      double invertElevFt;
      string qInstString;
      double qInstCfs;
      string initDepthString;
      double initialDepthFt;
      string intFlowString;
      double interfaceFlowPct;

      do
      {
        currentLine = outputReader.ReadLine();
        if(outputReader.EndOfStream)
        {
          throw new Exception("Table E03a not found within " + this.outputFile);
        }
      } while (!currentLine.Contains("Table E3a - Junction Data"));

      try
      {
        for (int i =0; i < 6; i++)
        {
          currentLine = outputReader.ReadLine();
        }

      while (currentLine != "")
      {
        tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        nodeName = tokens[1];
        grndElString = tokens[2];
        crownElString = tokens[3];
        invElString = tokens[4];
        qInstString = tokens[5];
        initDepthString = tokens[6];
        intFlowString = tokens[7];

        groundElevFt = Double.Parse(grndElString,NumberStyles.Any, CultureInfo.InvariantCulture);
        crownElevFt = Double.Parse(crownElString,NumberStyles.Any, CultureInfo.InvariantCulture);
        invertElevFt = Double.Parse(invElString,NumberStyles.Any, CultureInfo.InvariantCulture);
        qInstCfs = Double.Parse(qInstString,NumberStyles.Any,CultureInfo.InvariantCulture);
        initialDepthFt=Double.Parse(initDepthString,NumberStyles.Any,CultureInfo.InvariantCulture);
        interfaceFlowPct=Double.Parse(intFlowString,NumberStyles.Any,CultureInfo.InvariantCulture);

        tableE03a.AddTableE03aRow(nodeName,groundElevFt,crownElevFt,invertElevFt,qInstCfs,initialDepthFt,interfaceFlowPct);

        currentLine=outputReader.ReadLine();
      }
      }
      catch (Exception ex)
      {
        throw new Exception("Error parsing Table E03a: " + ex.Message);
      }
      return tableE03a.Count;
    }

    private int ExtractTableE03b()
    {
      string currentLine = "";
      string[] tokens;
      string nodeName;
      string xCoordString;
      double xCoordFt;
      string yCoordString;
      double yCoordFt;
      string manholeType;
      string inletType;
      string maxCapacityString;
      double maxCapacityCfs;
      string pavementShape;
      string pvmtSlopeString;
      double pavementSlopePct;

      do
      {
        currentLine = outputReader.ReadLine();
        if (outputReader.EndOfStream)
        {
          throw new Exception("Table E03b not found within " + this.outputFile);
        }
      } while (!currentLine.Contains("Table E3b - Junction Data"));

      try
      {
        for (int i=0; i< 6; i++)
        {
          currentLine = outputReader.ReadLine();
        }

        while (currentLine != "")
        {
          tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
          nodeName = tokens[1];
          xCoordString = tokens[2];
          yCoordString = tokens[3];
          //manholeType = tokens[4];
          //inletType = tokens[5];
          //pavementShape = tokens[6];
          //pvmtSlopeString = tokens[7];

          xCoordFt = Double.Parse(xCoordString,NumberStyles.Any,CultureInfo.InvariantCulture);
          yCoordFt = Double.Parse(yCoordString,NumberStyles.Any,CultureInfo.InvariantCulture);
          //pavementSlopePct = Double.Parse(pvmtSlopeString,NumberStyles.Any,CultureInfo.InvariantCulture);

          tableE03b.AddTableE03bRow(nodeName,xCoordFt,yCoordFt);

          currentLine=outputReader.ReadLine();
        }

      }

      catch (Exception ex)
      {
        throw new Exception("Error parsing Table E03b: " + ex.Message);
      }

      return tableE03b.Count;

    }

    private int ExtractTableE04()
    {
      string currentLine = "";
      string[] tokens;
      string condName;
      string usNodeName;
      string dsNodeName;
      string usElevString;
      string dsElevString;
      double usElevationFt;
      double dsElevationFt;

      do
      {
        currentLine = outputReader.ReadLine();
        if (outputReader.EndOfStream)
        {
          throw new Exception("Table E04 not found within " + this.outputFile);
        }
      } while (!currentLine.Contains("Table E4 - Conduit Connectivity"));

      try
      {
        for (int i = 0; i < 6; i++)
        {
          currentLine = outputReader.Read();
        }

        while(!currentLine.Contains("*==========================================*"))
        {
          tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmtryEntries);
          condName = tokens[1];
          usNodeName = tokens[2];
          dsNodeName = tokens[3];
          usElevString = tokens[4];
          dsElevString = tokens[5];

          usElevationFt = Double.Parse(usElevString,NumberStyles.Any,CultureInfo.InvariantCulture);
          dsElevationFt = Double.Parse(dsElevString,NumberStyles.Any,CultureInfo.InvariantCulture);

          tableE04.AddTableE04Row(condName,usNodeName,dsNodeName,usElevationFt,dsElevationFt);
          currentLine = outputReader.ReadLine();
        }
      }
      catch(Exception ex)
      {
        throw new Exception("Error parsing Table E04: " + ex.Message);
      }

      return tableE04.Count;
    }

    private int ExtractTableE04a()
    {
      return 0;
    }

    private int ExtractTableE04b()
    {
      return 0;
    }

    private int ExtractTableE05()
    {
      string currentLine = "";
      string[] tokens;
      string nodeName;
      string junctLimitTimeString;
      double junctionLimitTimeSeconds;

      do
      {
        currentLine = outputReader.ReadLine();
        if (outputReader.EndOfStream)
        {
          throw new Exception("Table E05 not found within " + this.uutputFile);
        }
      } while(!currentLine.Contains("Table E5 - Junction Time Limitation Summary"));
 
      try
      {
        for (int i=0; i<11; i++)
        {
          currentLine = outputReader.ReadLine();
        }

        while(!currentLine.Contains("The junction requiring the smallest time step was"))
        {
          tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
          nodeName = tokens[0];
          junctLimitTimeString = tokens[3];
          junctionLimitTimeSeconds = Double.Parse(junctLimitTimeString,NumberStyles.Any, CultureInfo.InvariantCulture);

          tableE05.AddTableE05Row(nodeName,junctionLimitationTimeSeconds);
        }
      }

      catch (Exception ex)
      {
        throw new Exception ("Error parsing Table E05: " + ex.Message);
      }
      return tableE05.Count;
    }

    private int ExtractTableE05a()
    {
      string currentLine = "";
      string[] tokens;
      string condName;
      string courantTimeStepString;
      string explTimeStepMinCourFactString;
      string implicitTimeStepString;
      string minCondTimeStepString;
      string maxQChangeString;
      string wobbleString;
      string typeOfSolution;

      double courantTimeStep;
      double explicitTimeStepMinCourantFactor;
      double implicitTimeStepSeconds;
      double minimumConduitTimestepSeconds;
      double maxQChangeCfs;
      double wobble;

      do
      {
        currentLine = outputReader.ReadLine();
        if (outputReader.EndOfStream)
        {
          throw new Exception("Table E05a not found within " + this.outputFile);
        }
      } while (!currentLine.Contains("Table E5a - Conduit Explicit Condition Summary"));

      try
      {
        for (int i = 0; i < 31; i++)
        {
          currentLine = outputReader.ReadLine();
        }

        while (!currentLine.Contains("The conduit with the smallest time step limitation was"))
        {
          tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
          condName = tokens[0];
          courantTimeStepString = tokens[1];
          courantTimeStep = Double.Parse(courantTimeStepString, NumberStyles, CultureInfo.InvariantCulture);
          explTimeStepMinCourFactString = tokens[2];
          explicitTimeStepMinCourantFactor = Double.Parse(explTimeStepMinCourFactString, NumberStyles.Any, CultureInfo.InvariantCulture);
          implicitTimeStepString = tokens[3];
          implicitTimeStepSeconds = Double.Parse(implicitTimeStepString, NumberStyles.Any, CultureInfo.InvariantCulture);
          minCondTimeStepString = tokens[4];
          minimumConduitTimestepSeconds = Double.Parse(minCondTimeStepString, NumberStyles.Any, CultureInfo.InvariantCulture);
          maxQChangeString = tokens[5];
          maxQChangeCfs = Double.Parse(maxQChangeString, NumberStyles.Any, CultureInfo.InvariantCulture);
          wobbleString = tokens[6];
          wobble = Double.Parse(wobbleString, NumberStyles.Any, CultureInfo.InvariantCulture);
          typeOfSolution = tokens[7];

          tableE05a.AddTableE05aRow(condName, courantTimeStep, explicitTimeStepMinCourantFactor, implicitTimeStepString, minimumConduitTimestepSeconds, maxQChangeCfs, wobble, typeOfSolution);
          currentLine = outputReader.ReadLine();
        }
      }

      catch (Exception ex)
      {
        throw new Exception("Error parsing Table E05a: " + ex.Message);
      }

      return tableE05a.Count;
    }
    
    private int ExtractTableE06()
    {
      return 0;
    }

    private int ExtractTableE07()
    {
      return 0;
    }

    private int ExtractTableE08()
    {
      return 0;
    }

    private int ExtractTableE09()
    {
      string currentLine = "";
      string[] tokens;
      string nodeName;
      double grElev;
      string grElString;
      double maxCrown;
      string maxCrownString;
      double maxJElev;
      string maxJElString;
      double Hour;
      double Min;
      System.DateTime timeOfMax;
      double surcharge;
      string surchString;
      double freeboard;
      string freeBoardString;
      double maxArea;
      string maxAreaString;

      do
      {
        currentLine = outputReader.ReadLine();
        if (outputReader.EndOfStream)
        {
          throw new Exception("Table E09 not found within " + this.outputFile);
        }
      } while (!currentLine.Contains("Table E9 - JUNCTION SUMMARY STATISTICS"));

      try
      {
        for (int i = 0; i < 10; i++)
        {
          currentLine = outputReader.ReadLine();
        }

        while (currentLine != "")
        {
          tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

          nodeName = tokens[0];
          grElString = tokens[1];
          //grElev = Convert.ToDouble(tokens[1]);
          grElev = Double.Parse(grElString, NumberStyles.Any, CultureInfo.InvariantCulture);
          maxCrownString = tokens[2];
          //maxCrown = Convert.ToDouble(tokens[2]);
          maxCrown = Double.Parse(maxCrownString, NumberStyles.Any, CultureInfo.InvariantCulture);
          maxJElString = tokens[3];
          //maxJElev = Convert.ToDouble(tokens[3]);
          maxJElev = Double.Parse(maxJElString, NumberStyles.Any, CultureInfo.InvariantCulture);
          Hour = Convert.ToDouble(tokens[4]);
          Min = Convert.ToDouble(tokens[5]);
          timeOfMax = this.beginDateTime.AddHours(Hour + Min / 60);
          surchString = tokens[6];
          //surcharge = Convert.ToDouble(tokens[6]);
          surcharge = Double.Parse(surchString, NumberStyles.Any, CultureInfo.InvariantCulture);
          freeBoardString = tokens[7];
          //freeboard = Convert.ToDouble(tokens[7]);
          freeboard = Double.Parse(freeBoardString, NumberStyles.Any, CultureInfo.InvariantCulture);
          maxAreaString = tokens[8];
          //maxArea = Convert.ToDouble(tokens[8]);
          maxArea = Double.Parse(maxAreaString, NumberStyles.Any, CultureInfo.InvariantCulture);

          tableE09.AddTableE09Row(nodeName, grElev, maxCrown, maxJElev, timeOfMax, surcharge, freeboard, maxArea);

          currentLine = outputReader.ReadLine();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Error parsing Table E09: "+ex.Message);
      }

      return tableE09.Count;
    }

    private int ExtractTableE10()
    {
      string currentLine = "";
      string[] tokens;
      string condName;
      string designQString;
      double designQ;
      string designVString;
      double designV;
      string maxDString;
      double maxD;
      string maxQString;
      double maxQ;
      string hourString;
      double Hour;
      string minString;
      double Min;
      System.DateTime timeMaxQ;
      string maxVString;
      double maxV;
      System.DateTime timeMaxV;
      string qQRatioString;
      double qQRatio;
      string maxUsElString;
      double maxUsElev;
      string maxDsElString;
      double maxDsElev;

      do
      {
        currentLine = outputReader.ReadLine();
        if (outputReader.EndOfStream)
        {
          throw new Exception("Table E10 not found within " + this.outputFile);
        }
      } while (!currentLine.Contains("Table E10 - CONDUIT SUMMARY STATISTICS"));

      try
      {
        for (int i = 0; i < 14; i++)
        {
          currentLine = outputReader.ReadLine();
        }

        while (currentLine != "")
        {
          tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

          switch (tokens.Length)
          {
            case 7: // Orifice, Weir                           
              break;
            case 8: //Free #
              break;
            case 9: //Free #
              break;
            case 15:
              condName = tokens[0];
              designQString = tokens[1];
              //designQ = Convert.ToDouble(tokens[1]);
              designQ = Double.Parse(designQString, NumberStyles.Any, CultureInfo.InvariantCulture);
              designVString = tokens[2];
              //designV = Convert.ToDouble(tokens[2]);
              designV = Double.Parse(designVString, NumberStyles.Any, CultureInfo.InvariantCulture);
              maxDString = tokens[3];
              //maxD = Convert.ToDouble(tokens[3]);
              maxD = Double.Parse(maxDString, NumberStyles.Any, CultureInfo.InvariantCulture);
              maxQString = tokens[4];
              //maxQ = Convert.ToDouble(tokens[4]);
              maxQ = Double.Parse(maxQString, NumberStyles.Any, CultureInfo.InvariantCulture);
              Hour = Convert.ToInt32(tokens[5]);
              Min = Convert.ToInt32(tokens[6]);
              timeMaxQ = this.beginDateTime.AddHours(Hour + Min / 60);
              maxVString = tokens[7];
              //maxV = Convert.ToDouble(tokens[7]);
              maxV = Double.Parse(maxVString, NumberStyles.Any, CultureInfo.InvariantCulture);
              Hour = Convert.ToInt32(tokens[8]);
              Min = Convert.ToInt32(tokens[9]);
              timeMaxV = this.beginDateTime.AddHours(Hour + Min / 60);
              qQRatioString = tokens[10];
              //qQRatio = Convert.ToDouble(tokens[10]);
              qQRatio = Double.Parse(qQRatioString, NumberStyles.Any, CultureInfo.InvariantCulture);
              maxUsElString = tokens[11];
              //maxUsElev = Convert.ToDouble(tokens[11]);
              maxUsElev = Double.Parse(maxUsElString, NumberStyles.Any, CultureInfo.InvariantCulture);
              maxDsElString = tokens[12];
              //maxDsElev = Convert.ToDouble(tokens[12]);
              maxDsElev = Double.Parse(maxDsElString, NumberStyles.Any, CultureInfo.InvariantCulture);

              tableE10.AddTableE10Row(condName, designQ, designV, maxD, maxQ, timeMaxQ, maxV, timeMaxV, qQRatio, maxUsElev, maxDsElev);
              break;
            default:
              throw new Exception();
          }
          currentLine = outputReader.ReadLine();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Error parsing Table E10: " + ex.Message);
      }

      return tableE10.Count;
    }

    private int ExtractTableE11()
    {
      return 0;
    }

    private int ExtractTableE12()
    {
      return 0;
    }

    private int ExtractTableE13()
    {
      return 0;
    }

    private int ExtractTableE14()
    {
      return 0;
    }

    private int ExtractTableE14a()
    {
      return 0;
    }

    private int ExtractTableE14b()
    {
      return 0;
    }

    private int ExtractTableE15()
    {
      return 0;
    }

    private int ExtractTableE15a()
    {
      return 0;
    }

    private int ExtractTableE16()
    {
      return 0;
    }

    private int ExtractTableE17()
    {
      return 0;
    }

    private int ExtractTableE18()
    {
      string currentLine = "";
      string[] tokens;
      string nodeName;
      string stoVolString;
      double storageVolumeCuFt;

      do
      {
        currentLine = outputReader.ReadLine();
        if (outputReader.EndOfStream)
        {
          throw new Exception("Table E18 not found within " + this.outputFile);
        }

      } while (!currentLine.Contains("Table E18 - Junction Continuity Error"));

      try
      {
        for (int i = 0; i < 18; i++)
        {
          currentLine = outputReader.ReadLine();
        }

        while (!currentLine.Contains("The total continuity error was"))
        {
          tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
          nodeName = tokens[0];
          stoVolString = tokens[4];
          storageVolumeCuFt = Double.Parse(stoVolString, NumberStyles.Any, CultureInfo.InvariantCulture);
          
          tableE18.AddTableE18Row(nodeName, storageVolumeCuFt);
          currentLine = outputReader.ReadLine();
        }
      }

      catch (Exception ex)
      {
        throw new Exception("Error parsing Table E18: "+ ex.Message);
      }

      return tableE18.Count;
    }

    private int ExtractTableE19()
    {
      string currentLine = "";
      string[] tokens;
      string nodeName;
      string interfaceVolString;
      string infilVolString;
      double interfaceInflowCuFt;
      double infiltrationVolumeCuFt;

      do
      {
        currentLine = outputReader.ReadLine();
        if (outputReader.EndOfStream)
        {
          throw new Exception("Table E19 not found within " + this.outputFile);
        }

      } while (!currentLine.Contains("Table E19 - Junction Inflow & Outflow Listing"));

      try
      {
        for (int i = 0; i < 9; i++)
        {
          currentLine = outputReader.ReadLine();
        }

        while (currentLine != "")
        {
          tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
          nodeName = tokens[0];
          interfaceVolString = tokens[3];
          infilVolString = tokens[7];
          interfaceInflowCuFt = Double.Parse(interfaceVolString, NumberStyles.Any, CultureInfo.InvariantCulture);
          infiltrationVolumeCuFt = Double.Parse(infilVolString, NumberStyles.Any,CultureInfo.InvariantCulture);

          tableE19.AddTableE19Row(nodeName,interfaceInflowCuFt, infiltrationVolumeCuFt);
          currentLine = outputReader.ReadLine();
        }
      }

      catch (Exception ex)
      {
        throw new Exception("Error Parsing Table E19: " + ex.Message);
      }
      finally
      {

      }
      return tableE19.Count;
      
    }

    private int ExtractTableE20()
    {
      string currentLine = "";
      string[] tokens;

      string nodeName;
      string surchTimeString;
      double surchargeTime;
      string floodTimeString;
      double floodedTime;
      string floodVolString;
      double floodVol;
      string maxStorVolString;
      double maxStoredVol;
      string pondVolString;
      double pondingVol;

      do
      {
        currentLine = outputReader.ReadLine();
        if (outputReader.EndOfStream)
        {
          throw new Exception("Table E20 not found within " + this.outputFile);
        }
      } while (!currentLine.Contains("Table E20 - Junction Flooding and Volume Listing"));

      try
      {
        for (int i = 0; i < 18; i++)
        {
          currentLine = outputReader.ReadLine();
        }

        while (currentLine != "")
        {
          tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

          nodeName = tokens[0];
          surchTimeString = tokens[1];
          surchargeTime = Double.Parse(surchTimeString, NumberStyles.Any, CultureInfo.InvariantCulture);
          //surchargeTime = Convert.ToDouble(tokens[1]);
          floodTimeString = tokens[2];
          floodedTime = Double.Parse(floodTimeString, NumberStyles.Any, CultureInfo.InvariantCulture);
          //floodedTime = Convert.ToDouble(tokens[2]);
          floodVolString = tokens[3];
          floodVol = Double.Parse(floodVolString, NumberStyles.Any, CultureInfo.InvariantCulture);
          //floodVol = Convert.ToDouble(tokens[3]);
          maxStorVolString = tokens[4];
          maxStoredVol = Double.Parse(maxStorVolString, NumberStyles.Any, CultureInfo.InvariantCulture);
          //maxStoredVol = Convert.ToDouble(tokens[4]);
          pondVolString = tokens[5];
          pondingVol = Double.Parse(pondVolString, NumberStyles.Any, CultureInfo.InvariantCulture);
          //pondingVol = Convert.ToDouble(tokens[5]);

          tableE20.AddTableE20Row(nodeName, surchargeTime, floodedTime, floodVol, maxStoredVol, pondingVol);

          currentLine = outputReader.ReadLine();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Error parsing Table E20: "+ex.Message);
      }

      return tableE20.Count;
    }

    private int ExtractTableE21()
    {
      string currentLine = "";
      string[] tokens;

      string nodeName;
      string inflowVolString;
      double inflowVolumeCuFt;
      string avgInflowString;
      double averageInflowCfs;

      do
      {
        currentLine = outputReader.ReadLine();
        if (outputReader.EndOfStream)
        {
          throw new Exception("Table E21 not found within " + this.outputFile);
        }
      } while (!currentLine.Contains("Table E21. Continuity balance at the end of the simulation"));

      try
      {
        for (int i = 0; i < 8; i++)
        {
          currentLine = outputReader.ReadLine();
        }

        while (currentLine!="")
        {
          tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
          nodeName = tokens[0];
          inflowVolString = tokens[1];
          inflowVolumeCuFt = Double.Parse(inflowVolString,NumberStyles.Any,CultureInfo.InvariantCulture);
          avgInflowString = tokens[2];
          averageInflowCfs = Double.Parse(avgInflowString,NumberStyles.Any,CultureInfo.InvariantCulture);

          tableE21.AddTableE21Row(nodeName,inflowVolumeCuFt,averageInflowCfs);
          currentLine = outputReader.ReadLine();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Error parsing Table E21: " + ex.Message);
      }
      return tableE21.Count;
    }

    private int ExtractTableE22()
    {
      string currentLine = "";
      string[] tokens;
      string overallErrString;
      double overallErrorPct;
      string worstNodeErrString;
      double worstNodeErrorPct;
      string worstNodalErrorNode;
      string totalInflowLossString;
      double totalInflowLossPct;
      string overallContinuityError;
      string efficiency;
      string effPctString;
      double efficiencyPct;
      string mostNodeNonConvergeString;
      int mostNodeNonConvergences;
      string totalNonConvergeStrin;
      int totalNonConvergences;
      string totalNonConvergedNodesString;
      int totalNonConvergedNodes;

      do
      {
        currentLine = outputReader.ReadLine();
        if (outputReader.EndOfStream)
        {
          throw new Exception("Table E22 not found within " + this.outputFile);
        }
      } while (!currentLine.Contains("Table E22. Numerical Model judgement section"));

      try
      {
        //for (int i = 0; i < 3; i++)
        //{
        //  currentLine = outputReader.ReadLine();
        //}
        
        //while (currentLine != "Overall error was (minimum of Table E18 & E21)")
        //{
        //  tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //  overallErrString = tokens[9];
        //  overallErrorPct = Double.Parse(overallErrString, NumberStyles.Any, CultureInfo.InvariantCulture);
        //}

        //  while (i == 4)
        //  {
        //    tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //    worstNodalErrorNode = tokens[6];
        //    worstNodeErrString = tokens[8];
        //    worstNodeErrorPct = Double.Parse(worstNodeErrString, NumberStyles.Any, CultureInfo.InvariantCulture);
        //  }

        //  while (i == 5)
        //  {
        //    tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //    totalInflowLossString = tokens[7];
        //    totalInflowLossPct = Double.Parse(totalInflowLossString, NumberStyles.Any, CultureInfo.InvariantCulture);
        //  }

        //  while (i == 6)
        //  {
        //    tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //    overallContinuityError = tokens[5];
        //  }

        //  while (i == 7)
        //  {
        //    tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //    efficiency = tokens[1];
        //  }

        //  while (i == 8)
        //  {
        //    tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //    effPctString = tokens[4];
        //    efficiencyPct = Double.Parse(effPctString, NumberStyles.Any, CultureInfo.InvariantCulture);
        //  }

        //  while (i == 9)
        //  {
        //    tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //    mostNodeNonConvergeString = tokens[8];
        //    mostNodeNonConvergences = Int32.Parse(mostNodeNonConvergeString, NumberStyles.Any, CultureInfo.InvariantCulture);
        //  }

        //  while (i == 10)
        //  {
        //    tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //    totalNonConvergeStrin = tokens[7];
        //    totalNonConvergences = Int32.Parse(totalNonConvergeStrin, NumberStyles.Any, CultureInfo.InvariantCulture);
        //  }

        //  while (i == 11)
        //  {
        //    tokens = currentLine.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //    totalNonConvergedNodesString = tokens[7];
        //    totalNonConvergedNodes = Int32.Parse(totalNonConvergedNodesString, NumberStyles.Any, CultureInfo.InvariantCulture);
        //  }
        //}
      }
      catch (Exception ex)
      {
        throw new Exception("Error parsing Table E22: " + ex.Message);
      }
      return tableE22.Count;
    }

    /// <summary>
    /// Gets a strongly typed DataSet containing the contents of
    /// Table E01.
    /// </summary>
    /// <returns>A strongly typed TableE01DataSet.TableE01DataTable
    /// containing the contents of Table E01</returns>
    public TableE01DataSet.TableE01DataTable GetTableE01()
    {
      return this.tableE01;
    }

    /// <summary>
    /// Gets a strongly typed DataSet containing the contents of
    /// Table E02.
    /// </summary>
    /// <returns>A strongly typed TableE02DataSet.TableE02DataTable
    /// containing the contents of Table E02</returns>
    //public TableE02DataSet.TableE02DataTable GetTableE02()
    //{
    //  return this.tableE02;
    //}

    /// <summary>
    /// Gets a strongly typed DataSet containing the contents of
    /// Table E03a.
    /// </summary>
    /// <returns>A strongly typed TableE03aDataSet.TableE03aDataTable
    /// containing the contents of Table E03a</returns>
    public TableE03aDataSet.TableE03aDataTable GetTableE03a()
    {
      return this.tableE03a;
    }

    /// <summary>
    /// Gets a strongly typed DataSet containing the contents of
    /// Table E03b.
    /// </summary>
    /// <returns>A strongly typed TableE03bDataSet.TableE03bDataTable
    /// containing the contents of Table E03b</returns>
    public TableE03bDataSet.TableE03bDataTable GetTableE03b()
    {
      return this.tableE03b;
    }

    /// <summary>
    /// Gets a strongly typed DataSet containing the contents of 
    /// Table E04
    /// </summary>
    /// <returns>A stronly typed TableE04DataSet.TableE04DataTable
    /// containing the contents of Table E04</returns>
    //public TableE04DataSet.TableE04DataTable GetTableE04()
    //{
    //  return this.tableE04;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E04a
    ///</summary>
    ///<returns>A strongly typed TableE04aDataSet.TableE04aDataTable
    ///containing the contents of Table E04a
    ///</returns>  
    //public TableE04aDataSet.TableE04aDataTable GetTableE04a()
    //{
    //  return this.tableE04a;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E04a
    ///</summary>
    ///<returns>A strongly typed TableE04aDataSet.TableE04aDataTable
    ///containing the contents of Table E04a
    ///</returns>
    //public TableE04aDataSet.TableE04aDataTable GetTableE04a()
    //{
    //  return this.tableE04a;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E04b
    ///</summary>
    ///<returns>A strongly typed TableE04bDataSet.TableE04bDataTable
    ///containing the contents of Table E04b
    ///</returns>
    //public TableE04bDataSet.TableE04bbDataTable GetTableE04b()
    //{
    //  return this.tableE04b;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E05
    ///</summary>
    ///<returns>A strongly typed TableE05DataSet.TableE05DataTable
    ///containing the contents of Table E05
    ///</returns>
    //public TableE05DataSet.TableE05DataTable GetTableE05()
    //{
    //  return this.tableE05;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E05a
    ///</summary>
    ///<returns>A strongly typed TableE05aDataSet.TableE05aDataTable
    ///containing the contents of Table E05a
    ///</returns>
    //public TableE05aDataSet.TableE05aDataTable GetTableE05a()
    //{
    //  return this.tableE05a;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E06
    ///</summary>
    ///<returns>A strongly typed TableE06DataSet.TableE06DataTable
    ///containing the contents of Table E06
    ///</returns>
    //public TableE06DataSet.TableE06DataTable GetTableE06()
    //{
    //  return this.tableE06;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E07
    ///</summary>
    ///<returns>A strongly typed TableE07DataSet.TableE07DataTable
    ///containing the contents of Table E07
    ///</returns>
    //public TableE07DataSet.TableE07DataTable GetTableE07()
    //{
    //  return this.tableE07;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E08
    ///</summary>
    ///<returns>A strongly typed TableE08DataSet.TableE08DataTable
    ///containing the contents of Table E08
    ///</returns>
    //public TableE08DataSet.TableE08DataTable GetTableE08()
    //{
    //  return this.tableE08;
    //}
       
    /// <summary>
    /// Gets a strongly typed DataSet containing the contents of
    /// Table E09.
    /// </summary>
    /// <returns>A strongly typed TableE09DataSet.TableE09DataTable
    /// containing the contents of Table E09</returns>
    public TableE09DataSet.TableE09DataTable GetTableE09()
    {
      return this.tableE09;
    }

    /// <summary>
    /// Gets a strongly typed DataSet containing the contents of
    /// Table E10.
    /// </summary>
    /// <returns>A strongly typed TableE10DataSet.TableE10DataTable
    /// containing the contents of Table E10</returns>
    public TableE10DataSet.TableE10DataTable GetTableE10()
    {
      return this.tableE10;
    }

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E11
    ///</summary>
    ///<returns>A strongly typed TableE11DataSet.TableE11DataTable
    ///containing the contents of Table E11
    ///</returns>
    //public TableE11DataSet.TableE11DataTable GetTableE11()
    //{
    //  return this.tableE11;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E12
    ///</summary>
    ///<returns>A strongly typed TableE12DataSet.TableE12DataTable
    ///containing the contents of Table E12
    ///</returns>
    //public TableE12DataSet.TableE12DataTable GetTableE12()
    //{
    //  return this.tableE12;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E13
    ///</summary>
    ///<returns>A strongly typed TableE13DataSet.TableE13DataTable
    ///containing the contents of Table E13
    ///</returns>
    //public TableE13DataSet.TableE13DataTable GetTableE13()
    //{
    //  return this.tableE13;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E13a
    ///</summary>
    ///<returns>A strongly typed TableE13aDataSet.TableE13aDataTable
    ///containing the contents of Table E13a
    ///</returns>
    //public TableE13aDataSet.TableE13aDataTable GetTableE13a()
    //{
    //  return this.tableE13a;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E14
    ///</summary>
    ///<returns>A strongly typed TableE14DataSet.TableE14DataTable
    ///containing the contents of Table E14
    ///</returns>
    //public TableE14DataSet.TableE14DataTable GetTableE14()
    //{
    //  return this.tableE14;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E14a
    ///</summary>
    ///<returns>A strongly typed TableE14aDataSet.TableE14aDataTable
    ///containing the contents of Table E14a
    ///</returns>
    //public TableE14aDataSet.TableE14aDataTable GetTableE14a()
    //{
    //  return this.tableE14a;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E14b
    ///</summary>
    ///<returns>A strongly typed TableE14bDataSet.TableE14bDataTable
    ///containing the contents of Table E14b
    ///</returns>
    //public TableE14bDataSet.TableE14bDataTable GetTableE14b()
    //{
    //  return this.tableE14b;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E15
    ///</summary>
    ///<returns>A strongly typed TableE15DataSet.TableE15DataTable
    ///containing the contents of Table E15
    ///</returns>
    //public TableE15DataSet.TableE15DataTable GetTableE15()
    //{
    //  return this.tableE15;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E15a
    ///</summary>
    ///<returns>A strongly typed TableE15aDataSet.TableE15aDataTable
    ///containing the contents of Table E15a
    ///</returns>
    //public TableE15aDataSet.TableE15aDataTable GetTableE15a()
    //{
    //  return this.tableE15a;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E16
    ///</summary>
    ///<returns>A strongly typed TableE16DataSet.TableE16DataTable
    ///containing the contents of Table E16
    ///</returns>
    //public TableE16DataSet.TableE16DataTable GetTableE16()
    //{
    //  return this.tableE16;
    //}

    ///<summary>
    ///Gets a strongly typed DataSet containing the contents of
    ///Table E17
    ///</summary>
    ///<returns>A strongly typed TableE17DataSet.TableE17DataTable
    ///containing the contents of Table E17
    ///</returns>
    //public TableE17DataSet.TableE17DataTable GetTableE17()
    //{
    //  return this.tableE17;
    //}
    
    /// <summary>
    /// Gets a strongly typed DataSet containing the contents of
    /// Table E18.
    /// </summary>
    /// <returns>A strongly typed TableE18DataSet.TableE18DataTable
    /// containing the contents of Table E18</returns>
    public TableE18DataSet.TableE18DataTable GetTableE18()
    {
      return this.tableE18;
    }

    /// <summary>
    /// Gets a strongly typed DataSet containing the contents of
    /// Table E19.
    /// </summary>
    /// <returns>A strongly typed TableE19DataSet.TableE19DataTable
    /// containing the contents of Table E19</returns>
    public TableE19DataSet.TableE19DataTable GetTableE19()
    {
      return this.tableE19;
    }

    /// <summary>
    /// Gets a strongly typed DataSet containing the contents of
    /// Table E20.
    /// </summary>
    /// <returns>A strongly typed TableE20DataSet.TableE20DataTable
    /// containing the contents of Table E20</returns>
    public TableE20DataSet.TableE20DataTable GetTableE20()
    {
      return this.tableE20;
    }

    /// <summary>
    /// Gets a strongly typed DataSet containing the contents of
    /// Table E21.
    /// </summary>
    /// <returns>A strongly typed TableE21DataSet.TableE21DataTable
    /// containing the contents of Table E21</returns>
    public TableE21DataSet.TableE21DataTable GetTableE21()
    {
      return this.tableE21;
    }

    /// <summary>
    /// Gets a strongly typed DataSet containing the contents of
    /// Table E22.
    /// </summary>
    /// <returns>A strongly typed TableE22DataSet.TableE22DataTable
    /// containing the contents of Table E22</returns>
    public TableE22DataSet.TableE22DataTable GetTableE22()
    {
      return this.tableE22;
    }

    /// <summary>
    /// Writes the output tables to an Access database. The database
    /// must already exist. Any pre-existingtables named "TableE09",
    /// "TableE10", or "TableE20" will be deleted from the database and
    /// replaced with new copies.
    /// </summary>
    /// <param name="database">The full path to an existing Microsoft
    /// Access database</param>
    public void WriteToAccessDatabase(string database)
    {
      if (!File.Exists(database))
      {
        AccessHelper.CreateNewMdb(database);        
      }
      AccessHelper accessHelper = new AccessHelper(database);

      try
      {
        
        CreateAndWriteTableE01(database, accessHelper);

        CreateAndWriteTableE03a(database, accessHelper);

        CreateAndWriteTableE09(database, accessHelper);

        CreateAndWriteTableE10(database, accessHelper);

        CreateAndWriteTableE18(database, accessHelper);

        CreateAndWriteTableE19(database, accessHelper);

        CreateAndWriteTableE20(database, accessHelper);
        
      }
      finally
      {
        accessHelper.Dispose();
      }

      return;
    }

    private void CreateAndWriteTableE01(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE01"))
        {
          accessHelper.DeleteTable("TableE01");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE01 (CondName Text, Length Double, CondClass Text, Area Double, ManningsN Double, MaxWidth Double, Depth Double)";
      command.ExecuteNonQuery();
      TableE01DataSetTableAdapters.TableE01TableAdapter tableE01Adapter;
      tableE01Adapter = new TableE01DataSetTableAdapters.TableE01TableAdapter();
      tableE01Adapter.Connection = connection;
      tableE01Adapter.Update(tableE01);
    }

    private void CreateAndWriteTableE03a(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE03a"))
        {
          accessHelper.DeleteTable("TableE03a");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE03a (NodeName Text, GroundElevFt Double, CrownElevFt Double, InvertElevFt Double, QInstCfs Double, InitialDepthFt Double, InterfaceFlowPct Double)";
      command.ExecuteNonQuery();
      TableE03aDataSetTableAdapters.TableE03aTableAdapter tableE03aAdapter;
      tableE03aAdapter = new TableE03aDataSetTableAdapters.TableE03aTableAdapter();
      tableE03aAdapter.Connection = connection;
      tableE03aAdapter.Update(tableE03a);
    }

    private void CreateAndWriteTableE03b(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE03b"))
        {
          accessHelper.DeleteTable("TableE03b");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE03b (NodeName Text, XCoordFt Double, YCoordFt Double)";
      command.ExecuteNonQuery();
      TableE03bDataSetTableAdapters.TableE03bTableAdapter tableE03bAdapter;
      tableE03bAdapter = new TableE03bDataSetTableAdapters.TableE03bTableAdapter();
      tableE03bAdapter.Connection = connection;
      tableE03bAdapter.Update(tableE03b);
    }

    private void CreateAndWriteTableE04(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE04"))
        {
          accessHelper.DeleteTable("TableE04");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE04 (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE04DataSetTableAdapters.TableE04TableAdapter tableE04Adapter;
      tableE04Adapter = new TableE04DataSetTableAdapters.TableE04TableAdapter();
      tableE04Adapter.Connection = connection;
      tableE04Adapter.Update(tableE04);
    }

    private void CreateAndWriteTableE04a(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE04a"))
        {
          accessHelper.DeleteTable("TableE04a");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE04a (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE04aDataSetTableAdapters.TableE04aTableAdapter tableE04aAdapter;
      tableE04aAdapter = new TableE04aDataSetTableAdapters.TableE04aTableAdapter();
      tableE04aAdapter.Connection = connection;
      tableE04aAdapter.Update(tableE04a);
    }

    private void CreateAndWriteTableE04b(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE04b"))
        {
          accessHelper.DeleteTable("TableE04b");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE04b (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE04bDataSetTableAdapters.TableE04bTableAdapter tableE04bAdapter;
      tableE04bAdapter = new TableE04bDataSetTableAdapters.TableE04bTableAdapter();
      tableE04bAdapter.Connection = connection;
      tableE04bAdapter.Update(tableE04b);
    }

    private void CreateAndWriteTableE05(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE05"))
        {
          accessHelper.DeleteTable("TableE05");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE05 (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE05DataSetTableAdapters.TableE05TableAdapter tableE05Adapter;
      tableE05Adapter = new TableE05DataSetTableAdapters.TableE05TableAdapter();
      tableE05Adapter.Connection = connection;
      tableE05Adapter.Update(tableE05);
    }

    private void CreateAndWriteTableE05a(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE05a"))
        {
          accessHelper.DeleteTable("TableE05a");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE05a (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE05aDataSetTableAdapters.TableE05aTableAdapter tableE05aAdapter;
      tableE05aAdapter = new TableE05aDataSetTableAdapters.TableE05aTableAdapter();
      tableE05aAdapter.Connection = connection;
      tableE05aAdapter.Update(tableE05a);
    }

    private void CreateAndWriteTableE06(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE06"))
        {
          accessHelper.DeleteTable("TableE06");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE06 (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE06DataSetTableAdapters.TableE06TableAdapter tableE06Adapter;
      tableE06Adapter = new TableE06DataSetTableAdapters.TableE06TableAdapter();
      tableE06Adapter.Connection = connection;
      tableE06Adapter.Update(tableE06);
    }

    private void CreateAndWriteTableE07(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE07"))
        {
          accessHelper.DeleteTable("TableE07");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE07 (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE07DataSetTableAdapters.TableE07TableAdapter tableE07Adapter;
      tableE07Adapter = new TableE07DataSetTableAdapters.TableE07TableAdapter();
      tableE07Adapter.Connection = connection;
      tableE07Adapter.Update(tableE07);
    }

    private void CreateAndWriteTableE08(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE08"))
        {
          accessHelper.DeleteTable("TableE08");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE08 (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE08DataSetTableAdapters.TableE08TableAdapter tableE08Adapter;
      tableE08Adapter = new TableE08DataSetTableAdapters.TableE08TableAdapter();
      tableE08Adapter.Connection = connection;
      tableE08Adapter.Update(tableE08);
    }

    private void CreateAndWriteTableE09(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();
      
      try
      {
        if (accessHelper.TableExists("TableE09"))
        {
          accessHelper.DeleteTable("TableE09");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE09 (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE09DataSetTableAdapters.TableE09TableAdapter tableE09Adapter;
      tableE09Adapter = new TableE09DataSetTableAdapters.TableE09TableAdapter();
      tableE09Adapter.Connection = connection;
      tableE09Adapter.Update(tableE09);
    }

    private void CreateAndWriteTableE10(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();
      try
      {
        if (accessHelper.TableExists("TableE10"))
        {
          accessHelper.DeleteTable("TableE10");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE10 (CondName Text, DesignQ Double, DesignV Double, MaxD Double, MaxQ DateTime, TimeMaxQ DateTime, MaxV Double, TimeMaxV DateTime, QqRatio Double, MaxUsElev Double, MaxDsElev Double)";
      command.ExecuteNonQuery();

      TableE10DataSetTableAdapters.TableE10TableAdapter tableE10Adapter;
      tableE10Adapter = new TableE10DataSetTableAdapters.TableE10TableAdapter();

      tableE10Adapter.Connection = connection;
      tableE10Adapter.Update(tableE10);
    }

    private void CreateAndWriteTableE11(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE11"))
        {
          accessHelper.DeleteTable("TableE11");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE11 (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE11DataSetTableAdapters.TableE11TableAdapter tableE11Adapter;
      tableE11Adapter = new TableE11DataSetTableAdapters.TableE11TableAdapter();
      tableE11Adapter.Connection = connection;
      tableE11Adapter.Update(tableE11);
    }

    private void CreateAndWriteTableE12(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE12"))
        {
          accessHelper.DeleteTable("TableE12");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE12 (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE12DataSetTableAdapters.TableE12TableAdapter tableE12Adapter;
      tableE12Adapter = new TableE12DataSetTableAdapters.TableE12TableAdapter();
      tableE12Adapter.Connection = connection;
      tableE12Adapter.Update(tableE12);
    }

    private void CreateAdWriteTableE13(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE13"))
        {
          accessHelper.DeleteTable("TableE13");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE13 (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE13DataSetTableAdapters.TableE13TableAdapter tableE13Adapter;
      tableE13Adapter = new TableE13DataSetTableAdapters.TableE13TableAdapter();
      tableE13Adapter.Connection = connection;
      tableE13Adapter.Update(tableE13);
    }

    private void CreateAndWriteTableE13a(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE13a"))
        {
          accessHelper.DeleteTable("TableE13a");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE13a (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE13aDataSetTableAdapters.TableE13aTableAdapter tableE13aAdapter;
      tableE13aAdapter = new TableE13aDataSetTableAdapters.TableE13aTableAdapter();
      tableE13aAdapter.Connection = connection;
      tableE13aAdapter.Update(tableE13a);
    }

    private void CreateAndWriteTableE14(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE09"))
        {
          accessHelper.DeleteTable("TableE09");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE09 (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE09DataSetTableAdapters.TableE09TableAdapter tableE09Adapter;
      tableE09Adapter = new TableE09DataSetTableAdapters.TableE09TableAdapter();
      tableE09Adapter.Connection = connection;
      tableE09Adapter.Update(tableE09);
    }

    private void CreateAneWriteTableE14a(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE14a"))
        {
          accessHelper.DeleteTable("TableE14a");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE14a (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE14aDataSetTableAdapters.TableE14aTableAdapter tableE14aAdapter;
      tableE14aAdapter = new TableE14aDataSetTableAdapters.TableE14aTableAdapter();
      tableE14aAdapter.Connection = connection;
      tableE14aAdapter.Update(tableE14a);
    }

    private void CreateAndWriteTableE14b(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE14b"))
        {
          accessHelper.DeleteTable("TableE14b");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE14b (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE14bDataSetTableAdapters.TableE14bTableAdapter tableE14bAdapter;
      tableE14bAdapter = new TableE14bDataSetTableAdapters.TableE14bTableAdapter();
      tableE14bAdapter.Connection = connection;
      tableE14bAdapter.Update(tableE14b);
    }

    private void CreateAndWriteTableE15(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE15"))
        {
          accessHelper.DeleteTable("TableE15");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE15 (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE15DataSetTableAdapters.TableE15TableAdapter tableE15Adapter;
      tableE15Adapter = new TableE15DataSetTableAdapters.TableE15TableAdapter();
      tableE15Adapter.Connection = connection;
      tableE15Adapter.Update(tableE15);
    }

    private void CreateAndWriteTableE15a(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE15a"))
        {
          accessHelper.DeleteTable("TableE15a");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE15a (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE15aDataSetTableAdapters.TableE15aTableAdapter tableE15aAdapter;
      tableE15aAdapter = new TableE15aDataSetTableAdapters.TableE15aTableAdapter();
      tableE15aAdapter.Connection = connection;
      tableE15aAdapter.Update(tableE15a);
    }

    private void CreateAndWriteTableE16(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE16"))
        {
          accessHelper.DeleteTable("TableE16");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE16 (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE16DataSetTableAdapters.TableE16TableAdapter tableE16Adapter;
      tableE16Adapter = new TableE16DataSetTableAdapters.TableE16TableAdapter();
      tableE16Adapter.Connection = connection;
      tableE16Adapter.Update(tableE16);
    }

    private void CreateAndWriteTableE17(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE17"))
        {
          accessHelper.DeleteTable("TableE17");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE17 (NodeName Text, GrElev Double, MaxCrown Double, MaxJElev Double, TimeOfMax DateTime, Surcharge Double, Freeboard Double, MaxArea Double)";
      command.ExecuteNonQuery();
      TableE17DataSetTableAdapters.TableE17TableAdapter tableE17Adapter;
      tableE17Adapter = new TableE17DataSetTableAdapters.TableE17TableAdapter();
      tableE17Adapter.Connection = connection;
      tableE17Adapter.Update(tableE17);
    }

    private void CreateAndWriteTableE18(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();
      try
      {
        if (accessHelper.TableExists("TableE18"))
        {
          accessHelper.DeleteTable("TableE18");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE18 (nodeName String, storageVolumeCuFt Double)";
      command.ExecuteNonQuery();

      TableE18DataSetTableAdapters.TableE18TableAdapter tableE18Adapter;
      tableE18Adapter = new TableE18DataSetTableAdapters.TableE18TableAdapter();

      tableE18Adapter.Connection = connection;
      tableE18Adapter.Update(tableE18);
    }

    private void CreateAndWriteTableE19(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE19"))
        {
          accessHelper.DeleteTable("TableE19");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE19 (nodeName String, infiltrationVolumeCuFt Double)";
      command.ExecuteNonQuery();

      TableE19DataSetTableAdapters.TableE19TableAdapter tableE19Adapter;
      tableE19Adapter = new TableE19DataSetTableAdapters.TableE19TableAdapter();

      tableE19Adapter.Connection = connection;
      tableE19Adapter.Update(tableE19);
    }

    private void CreateAndWriteTableE20(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE20"))
        {
          accessHelper.DeleteTable("TableE20");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE20 (NodeName String, SurchargeTime Double, FloodedTime Double, FloodVol Double, MaxStoredVol Double, PondingVol Double)";
      command.ExecuteNonQuery();

      TableE20DataSetTableAdapters.TableE20TableAdapter tableE20Adapter;
      tableE20Adapter = new TableE20DataSetTableAdapters.TableE20TableAdapter();

      tableE20Adapter.Connection = connection;
      tableE20Adapter.Update(tableE20);
    }

    private void CreateAndWriteTableE21(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE21"))
        {
          accessHelper.DeleteTable("TableE21");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE21 (NodeName Strint, InflowVolumeCuFt Double, AverageInflowCfs Double)";
      command.ExecuteNonQuery();

      TableE21DataSetTableAdapters.TableE21TableAdapter tableE21Adapter;
      tableE21Adapter = new TableE21DataSetTableAdapters.TableE21TableAdapter();

      tableE21Adapter.Connection = connection;
      tableE21Adapter.Update(tableE21);
    }

    private void CreateAndWriteTableE22(string database, AccessHelper accessHelper)
    {
      OleDbConnection connection;
      OleDbCommand command;
      connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);
      command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      try
      {
        if (accessHelper.TableExists("TableE22"))
        {
          accessHelper.DeleteTable("TableE22");
        }
      }
      catch (OleDbException ex)
      {
        if (!ex.Message.Contains("does not exist."))
        {
          throw;
        }
      }

      command.CommandText = "Create Table TableE22 (OverallErrorPct Double, WorstNodalError Double, WorstNodalErrorNode String, TotalInflowLossPct Double, OverallContinuityError String, Efficiency String, EfficienctPct Double, MostNodeNonConvergences Int, TotalNonConvergences Int, TotalNonConvergedNodes Int)";
      command.ExecuteNonQuery();

      TableE22DataSetTableAdapters.TableE22TableAdapter tableE22Adapter;
      tableE22Adapter = new TableE22DataSetTableAdapters.TableE22TableAdapter();

      tableE22Adapter.Connection = connection;
      tableE22Adapter.Update(tableE22);
    }
  }
}
