using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling;
using System.Collections;
using System.Collections.Specialized;
using SystemsAnalysis.Utils.Events;
using SystemsAnalysis.Types;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Reporting;
using SystemsAnalysis.Reporting.ReportLibraries;
using System.IO;
using SystemsAnalysis.Utils.AccessUtils;

namespace SystemsAnalysis.Reporting.ReportLibraries
{
  public class FecQaReport : FecReport
  {
    /// <summary>
    /// Creates a AlternativeReport from a collections of Links, Nodes and Dscs
    /// </summary>
    /// <param name="links">A Links collection to report on</param>
    /// <param name="nodes">A Nodes collection to report on</param>
    /// <param name="dscs">A Dscs collection to report on</param>        
    public FecQaReport(Links links, Nodes nodes, Dscs dscs)
      : base(links, nodes, dscs)
    {

    }

    private string qaDatabase = "";    

    public new class ReportInfo : ReportBase.ReportInfo
    {
      private Dictionary<string, Parameter> auxilaryData;
      private Dictionary<string, string> auxilaryDataDescription;
      public ReportInfo(string reportName)
        : base(reportName)
      {
        SetAuxilaryDataDescription();
        auxilaryData = new Dictionary<string, Parameter>();
      }
      public override Dictionary<string, string> AuxilaryDataDescription
      {
        get
        {
          return auxilaryDataDescription;
        }
      }

      private void SetAuxilaryDataDescription()
      {
        auxilaryDataDescription = new Dictionary<string, string>();
        auxilaryDataDescription.Add("QADatabase", @"\\cassio\systems_planning\7900_SanitaryFacPlan\Docs\TMs\5.06 Characterization\BasinCharacterizations\_auto_generated_reports\_qa_database.mdb");        
      }
      public override bool RequiresAuxilaryData
      {
        get
        {
          return true;
        }
      }
      public override Dictionary<string, Parameter> AuxilaryData
      {
        get
        {
          return auxilaryData;
        }
        set
        {
          auxilaryData = value;
        }
      }
    }

    public override void LoadAuxilaryData(Dictionary<string, Parameter> AuxilaryData)
    {
      try
      {
        this.qaDatabase = AuxilaryData["QADatabase"].Value;        
        if (!File.Exists(qaDatabase)) AccessHelper.CreateNewMdb(qaDatabase);// System.IO.File.Copy(qaDatabaseTemplate, qaDatabase);
      }
      catch (Exception ex)
      {
        throw new Exception("Could not read auxilary data 'QADatabase': " + ex.Message, ex);
      }
    }

    public string WriteFecQaDatabase(IDictionary<string, Parameter> parameters)
    {
      AccessHelper accessHelper = new AccessHelper(qaDatabase);
      try
      {
        string studyArea = parameters["studyArea"].Value;

        string tempFile = System.IO.Path.GetTempFileName();
        StreamWriter sw = new StreamWriter(tempFile);

        sw.WriteLine("studyArea,fecID,fecName,fecBasin,dscID,area,sewerable,source");

        if (!accessHelper.TableExists("_fecQaDsc"))
        {
          sw.Close();
          accessHelper.ImportTable(tempFile, "_fecQaDsc", AccessHelper.FileType.CSV);
          sw = new StreamWriter(tempFile, true);
        }

        string timeStamp = DateTime.Now.ToString("MMddyy_hhmm");
        string tempTableName = studyArea + "_qaDsc_" + timeStamp;

        foreach (FlowEstimationCatchment fec in this.fecs.Values)
        {
          int fecID = fec.FecID;
          string basin = fec.BasinID;
          string fecName = fec.FecName;
          foreach (Dsc dsc in FecDscs[fecID].Values)
          {
            sw.Write(studyArea + ","); sw.Write(fecID + ","); sw.Write(fecName + ","); sw.Write(basin + ",");
            sw.Write(dsc.DscID + ","); sw.Write(dsc.Area + ","); sw.Write(dsc.Sewerable.ToString() + ",");
            sw.WriteLine(tempTableName);
          }
        }
        sw.Close();

        accessHelper.ImportTable(tempFile, tempTableName, AccessHelper.FileType.CSV);
        accessHelper.AppendTable(tempTableName, "_fecQaDsc");

        //do links
        tempFile = System.IO.Path.GetTempFileName();
        sw = new StreamWriter(tempFile);

        sw.WriteLine("studyArea,fecID,fecName,fecBasin,mLinkID,source");

        if (!accessHelper.TableExists("_fecQaLink"))
        {
          sw.Close();
          accessHelper.ImportTable(tempFile, "_fecQaLink", AccessHelper.FileType.CSV);
          sw = new StreamWriter(tempFile, true);
        }

        tempTableName = studyArea + "_qaLink_" + timeStamp;
        
        foreach (FlowEstimationCatchment fec in this.fecs.Values)
        {
          int fecID = fec.FecID;
          string basin = fec.BasinID;
          string fecName = fec.FecName;
          foreach (Link link in FecLinks[fecID].Values)
          {
            sw.Write(studyArea + ","); sw.Write(fecID + ","); sw.Write(fecName + ","); sw.Write(basin + ",");
            sw.Write(link.MLinkID + ",");
            sw.WriteLine(tempTableName);
          }
        }
        sw.Close();

        accessHelper.ImportTable(tempFile, tempTableName, AccessHelper.FileType.CSV);
        accessHelper.AppendTable(tempTableName, "_fecQaLink");
      }
      catch (Exception ex)
      {
      }
      finally
      {
        if (accessHelper != null)
        {
          accessHelper.Dispose();
          accessHelper = null;
        }
      }
      return qaDatabase;
    }

  }
}
