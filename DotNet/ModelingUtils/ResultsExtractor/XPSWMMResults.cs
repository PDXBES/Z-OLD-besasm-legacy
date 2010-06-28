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
    //TO-DO: implement private TableE03aDataSet.TableE03aDataTable tableE03a;
    //TO-DO: implement private TableE03bDataSet.TableE03bDataTable tableE03b;
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
    //TO-DO: implement private TableE22DataSet.TableE22DataTable tableE22;

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
      tableE09 = new TableE09DataSet.TableE09DataTable();
      tableE10 = new TableE10DataSet.TableE10DataTable();
      tableE18 = new TableE18DataSet.TableE18DataTable();
      tableE19 = new TableE19DataSet.TableE19DataTable();
      tableE20 = new TableE20DataSet.TableE20DataTable();

      ExtractTableE01();
      ExtractTableE09();
      ExtractTableE10();
      ExtractTableE18();
      ExtractTableE19();
      ExtractTableE20();
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
          //storageVolumeCuFt = Convert.ToDouble(tokens[4]);

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
  }
}
