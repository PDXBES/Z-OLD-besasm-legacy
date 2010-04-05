using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.Odbc;
using System.Data.OleDb;
using SystemsAnalysis.Utils.AccessUtils;


namespace SystemsAnalysis.Modeling.ModelUtils.ResultsExtractor
{
  /// <summary>
  /// Extracts results from a version 11.2-11.6 (Retail 9.0-10.6, Engine 8.87-10.60)
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
    private TableE09DataSet.TableE09DataTable tableE09;
    private TableE10DataSet.TableE10DataTable tableE10;
    private TableE18DataSet.TableE18DataTable tableE18;
    private TableE19DataSet.TableE19DataTable tableE19;
    private TableE20DataSet.TableE20DataTable tableE20;

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
      string condClass;
      double area;
      double manningsN;
      double maxWidth;
      double depth;

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
          length = Convert.ToDouble(tokens[2]);
          condClass = tokens[3];
          if (condClass == "Closd")
          {
            area = Convert.ToDouble(tokens[5]);
            manningsN = Convert.ToDouble(tokens[6]);
            maxWidth = Convert.ToDouble(tokens[7]);
            depth = Convert.ToDouble(tokens[8]);
          }
          else
          {
            area = Convert.ToDouble(tokens[4]);
            manningsN = Convert.ToDouble(tokens[5]);
            maxWidth = Convert.ToDouble(tokens[6]);
            depth = Convert.ToDouble(tokens[7]);
          }

          tableE01.AddTableE01Row(condName, length, condClass, area, manningsN, maxWidth, depth);

          currentLine = outputReader.ReadLine();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Error parsing Table E01: " + ex.ToString());
      }

      return tableE01.Count;
    }

    private int ExtractTableE09()
    {
      string currentLine = "";
      string[] tokens;
      string nodeName;
      double grElev;
      double maxCrown;
      double maxJElev;
      double Hour;
      double Min;
      System.DateTime timeOfMax;
      double surcharge;
      double freeboard;
      double maxArea;

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
          grElev = Convert.ToDouble(tokens[1]);
          maxCrown = Convert.ToDouble(tokens[2]);
          maxJElev = Convert.ToDouble(tokens[3]);
          Hour = Convert.ToDouble(tokens[4]);
          Min = Convert.ToDouble(tokens[5]);
          timeOfMax = this.beginDateTime.AddHours(Hour + Min / 60);
          surcharge = Convert.ToDouble(tokens[6]);
          freeboard = Convert.ToDouble(tokens[7]);
          maxArea = Convert.ToDouble(tokens[8]);

          tableE09.AddTableE09Row(nodeName, grElev, maxCrown, maxJElev, timeOfMax, surcharge, freeboard, maxArea);

          currentLine = outputReader.ReadLine();
        }
      }
      catch (Exception)
      {
        throw new Exception("Error parsing Table E09");
      }

      return tableE09.Count;
    }

    private int ExtractTableE10()
    {
      string currentLine = "";
      string[] tokens;
      string condName;
      double designQ;
      double designV;
      double maxD;
      double maxQ;
      double Hour;
      double Min;
      System.DateTime timeMaxQ;
      double maxV;
      System.DateTime timeMaxV;
      double qQRatio;
      double maxUsElev;
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
              designQ = Convert.ToDouble(tokens[1]);
              designV = Convert.ToDouble(tokens[2]);
              maxD = Convert.ToDouble(tokens[3]);
              maxQ = Convert.ToDouble(tokens[4]);
              Hour = Convert.ToInt32(tokens[5]);
              Min = Convert.ToInt32(tokens[6]);
              timeMaxQ = this.beginDateTime.AddHours(Hour + Min / 60);
              maxV = Convert.ToDouble(tokens[7]);
              Hour = Convert.ToInt32(tokens[8]);
              Min = Convert.ToInt32(tokens[9]);
              timeMaxV = this.beginDateTime.AddHours(Hour + Min / 60);
              qQRatio = Convert.ToDouble(tokens[10]);
              maxUsElev = Convert.ToDouble(tokens[11]);
              maxDsElev = Convert.ToDouble(tokens[12]);

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
          storageVolumeCuFt = Convert.ToDouble(tokens[4]);

          tableE18.AddTableE18Row(nodeName, storageVolumeCuFt);
          currentLine = outputReader.ReadLine();
        }
      }

      catch (Exception)
      {
        throw new Exception("Error parsing Table E18");
      }

      return tableE18.Count;
    }

    private int ExtractTableE19()
    {
      string currentLine = "";
      string[] tokens;
      string nodeName;
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
          infiltrationVolumeCuFt = Convert.ToDouble(tokens[7]);

          tableE19.AddTableE19Row(nodeName, infiltrationVolumeCuFt);
          currentLine = outputReader.ReadLine();
        }
      }

      catch (Exception)
      {
        throw new Exception("Error parsing Table E19");
      }

      return tableE19.Count;
    }

    private int ExtractTableE20()
    {
      string currentLine = "";
      string[] tokens;

      string nodeName;
      double surchargeTime;
      double floodedTime;
      double floodVol;
      double maxStoredVol;
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
          surchargeTime = Convert.ToDouble(tokens[1]);
          floodedTime = Convert.ToDouble(tokens[2]);
          floodVol = Convert.ToDouble(tokens[3]);
          maxStoredVol = Convert.ToDouble(tokens[4]);
          pondingVol = Convert.ToDouble(tokens[5]);

          tableE20.AddTableE20Row(nodeName, surchargeTime, floodedTime, floodVol, maxStoredVol, pondingVol);

          currentLine = outputReader.ReadLine();
        }
      }
      catch (Exception)
      {
        throw new Exception("Error parsing Table E20");
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

      OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database);

      OleDbCommand command = new OleDbCommand();
      command.Connection = connection;
      connection.Open();

      #region Create and Write Table E01
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
      #endregion

      #region Create and Write Table E09
      try
      {
        command.CommandText = "Drop Table TableE09";
        command.ExecuteNonQuery();
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
      #endregion

      #region Create and Write Table E10
      try
      {
        command.CommandText = "Drop Table TableE10";
        command.ExecuteNonQuery();
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
      #endregion

      #region Create and Write Table E18
      try
      {
        command.CommandText = "Drop Table TableE18";
        command.ExecuteNonQuery();
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
      #endregion

      #region Create and Write Table E19
      try
      {
        command.CommandText = "Drop Table TableE19";
        command.ExecuteNonQuery();
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
      #endregion

      #region Create and Write Table E20
      try
      {
        command.CommandText = "Drop Table TableE20";
        command.ExecuteNonQuery();
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
      #endregion

      return;
    }

  }
}
