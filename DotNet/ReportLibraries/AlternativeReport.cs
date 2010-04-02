using ESRI.ArcGIS.Utility;

using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling.Alternatives;
using SystemsAnalysis.Modeling;
using System.Collections;
using System.Collections.Specialized;
using SystemsAnalysis.Utils.Events;
using SystemsAnalysis.Types;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Reporting;
using SystemsAnalysis.Reporting.ReportLibraries;

namespace SystemsAnalysis.Reporting.ReportLibraries
{
  public class AlternativeReport : ReportBase
  {
    private AlternativePackage altPackage;
    private PipeInspectionDataSet piDS;

    /// <summary>
    /// Creates a AlternativeReport from a collections of Links, Nodes and Dscs
    /// </summary>
    /// <param name="links">A Links collection to report on</param>
    /// <param name="nodes">A Nodes collection to report on</param>
    /// <param name="dscs">A Dscs collection to report on</param>        
    public AlternativeReport()
    //: base(links, nodes, dscs)
    {
      this.OnStatusChanged(new StatusChangedArgs("Creating alternative objects."));

      piDS = new PipeInspectionDataSet();
      DataAccess.PipeInspectionDataSetTableAdapters.PipeInspectionGradesTableAdapter piTA;
      piTA = new DataAccess.PipeInspectionDataSetTableAdapters.PipeInspectionGradesTableAdapter();
      piTA.Fill(piDS.PipeInspectionGrades);
    }

    public class ReportInfo : ReportBase.ReportInfo
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
        auxilaryDataDescription.Add("AlternativePath", "Alternative Package (alternative_package.mdb)");
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
        string alternativePath;
        alternativePath = AuxilaryData["AlternativePath"].Value;
        alternativePath = System.IO.Path.GetDirectoryName(alternativePath);
        altPackage = new AlternativePackage(alternativePath);
      }
      catch (Exception ex)
      {
        throw new Exception("Could not read auxilary data 'AlternativePath': " + ex.Message, ex);
      }
    }

    public IList FocusAreaList()
    {
      return altPackage.FocusAreaList;
    }

    public string FocusAreaName(IDictionary<string, Parameter> parameters)
    {
      return parameters["FocusArea"].Value;
    }

    public int NewAndUpsizedPipeCount(IDictionary<string, Parameter> parameters)
    {
      bool filterByFocusArea = parameters.ContainsKey("FocusArea");
      int pipeCount = 0;

      string focusArea = "";
      if (filterByFocusArea)
      {
        focusArea = parameters["FocusArea"].Value;
      }
      foreach (AltLink altLink in altPackage.ModelAltLinks.Values)
      {
        if (filterByFocusArea && altLink.FocusArea != focusArea)
        {
          continue;
        }
        if (altLink.Operation == Enumerators.AlternativeOperation.UPD ||
        altLink.Operation == Enumerators.AlternativeOperation.ADD)
        {
          pipeCount++;
        }
      }
      return (Int32)pipeCount;
    }
    /// <summary>
    /// Returns the Pipe Length in miles
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>       
    public double NewAndUpsizedPipeLength(IDictionary<string, Parameter> parameters)
    {
      bool filterByFocusArea = parameters.ContainsKey("FocusArea");
      double pipeLength = 0;

      string focusArea = "";
      if (filterByFocusArea)
      {
        focusArea = parameters["FocusArea"].Value;
      }

      foreach (AltLink altLink in altPackage.ModelAltLinks.Values)
      {
        if (filterByFocusArea && altLink.FocusArea != focusArea)
        {
          continue;
        }
        if (altLink.Operation == Enumerators.AlternativeOperation.UPD ||
        altLink.Operation == Enumerators.AlternativeOperation.ADD)
        {
          pipeLength += altLink.Length;
        }
      }

      return pipeLength;
    }

    public int PoorConditionPipeCount(IDictionary<string, Parameter> parameters)
    {
      bool filterByFocusArea = parameters.ContainsKey("FocusArea");
      int pipeCount = 0;

      string focusArea = "";
      if (filterByFocusArea)
      {
        focusArea = parameters["FocusArea"].Value;
      }

      Links links = altPackage.BaseModel.ModelLinks;
      foreach (AltLink altLink in altPackage.ModelAltLinks.Values)
      {
        if (filterByFocusArea && altLink.FocusArea != focusArea)
        {
          continue;
        }
        if (altLink.Operation != Enumerators.AlternativeOperation.UPD &&
        altLink.Operation != Enumerators.AlternativeOperation.RIK)
        {
          continue;
        }

        PipeInspectionDataSet.PipeInspectionGradesRow inspectionRow;
        int linkId = altLink.LinkID;
        Link mdlLink = links[linkId];
        if (mdlLink.MLinkID == 0)
        {
          continue;
        }

        inspectionRow = piDS.PipeInspectionGrades.FindByMLinkID(mdlLink.MLinkID);
        if (inspectionRow == null || inspectionRow.IsGradeNull())
        {
          continue;
        }

        if (inspectionRow.Grade == 4 || inspectionRow.Grade == 5)
        {
          pipeCount++;
        }
      }
      return pipeCount;
    }
    public double PoorConditionPipeLength(IDictionary<string, Parameter> parameters)
    {
      bool filterByFocusArea = parameters.ContainsKey("FocusArea");
      double pipeLength = 0;

      string focusArea = "";
      if (filterByFocusArea)
      {
        focusArea = parameters["FocusArea"].Value;
      }

      Links links = altPackage.BaseModel.ModelLinks;
      foreach (AltLink altLink in altPackage.ModelAltLinks.Values)
      {
        if (filterByFocusArea && altLink.FocusArea != focusArea)
        {
          continue;
        }
        if (altLink.Operation != Enumerators.AlternativeOperation.UPD &&
        altLink.Operation != Enumerators.AlternativeOperation.RIK)
        {
          continue;
        }

        PipeInspectionDataSet.PipeInspectionGradesRow inspectionRow;
        int linkId = altLink.LinkID;
        Link mdlLink = links[linkId];
        if (mdlLink.MLinkID == 0)
        {
          continue;
        }

        inspectionRow = piDS.PipeInspectionGrades.FindByMLinkID(mdlLink.MLinkID);
        if (inspectionRow == null || inspectionRow.IsGradeNull())
        {
          continue;
        }

        if (inspectionRow.Grade == 4 || inspectionRow.Grade == 5)
        {
          pipeLength += altLink.Length;
        }
      }
      return pipeLength;
    }

    public int UpsizedPoorConditionPipeCount(IDictionary<string, Parameter> parameters)
    {
      bool filterByFocusArea = parameters.ContainsKey("FocusArea");
      int pipeCount = 0;

      string focusArea = "";
      if (filterByFocusArea)
      {
        focusArea = parameters["FocusArea"].Value;
      }

      Links links = altPackage.BaseModel.ModelLinks;
      foreach (AltLink altLink in altPackage.ModelAltLinks.Values)
      {
        if (filterByFocusArea && altLink.FocusArea != focusArea)
        {
          continue;
        }
        if (altLink.Operation != Enumerators.AlternativeOperation.UPD)
        {
          continue;
        }

        PipeInspectionDataSet.PipeInspectionGradesRow inspectionRow;
        int linkId = altLink.LinkID;
        Link mdlLink = links[linkId];
        if (mdlLink.MLinkID == 0)
        {
          continue;
        }

        inspectionRow = piDS.PipeInspectionGrades.FindByMLinkID(mdlLink.MLinkID);
        if (inspectionRow == null || inspectionRow.IsGradeNull())
        {
          continue;
        }

        if (inspectionRow.Grade == 4 || inspectionRow.Grade == 5)
        {
          pipeCount++;
        }
      }
      return pipeCount;
    }
    public double UpsizedPoorConditionPipeLength(IDictionary<string, Parameter> parameters)
    {
      bool filterByFocusArea = parameters.ContainsKey("FocusArea");
      double pipeLength = 0;

      string focusArea = "";
      if (filterByFocusArea)
      {
        focusArea = parameters["FocusArea"].Value;
      }

      Links links = altPackage.BaseModel.ModelLinks;
      foreach (AltLink altLink in altPackage.ModelAltLinks.Values)
      {
        if (filterByFocusArea && altLink.FocusArea != focusArea)
        {
          continue;
        }
        if (altLink.Operation != Enumerators.AlternativeOperation.UPD)
        {
          continue;
        }

        PipeInspectionDataSet.PipeInspectionGradesRow inspectionRow;
        int linkId = altLink.LinkID;
        Link mdlLink = links[linkId];
        if (mdlLink.MLinkID == 0)
        {
          continue;
        }

        inspectionRow = piDS.PipeInspectionGrades.FindByMLinkID(mdlLink.MLinkID);
        if (inspectionRow == null || inspectionRow.IsGradeNull())
        {
          continue;
        }

        if (inspectionRow.Grade == 4 || inspectionRow.Grade == 5)
        {
          pipeLength += altLink.Length;
        }
      }
      return pipeLength;
    }

    public int RIKPipeCount(IDictionary<string, Parameter> parameters)
    {
      bool filterByFocusArea = parameters.ContainsKey("FocusArea");
      int pipeCount = 0;

      string focusArea = "";
      if (filterByFocusArea)
      {
        focusArea = parameters["FocusArea"].Value;
      }
      foreach (AltLink altLink in altPackage.ModelAltLinks.Values)
      {
        if (filterByFocusArea && altLink.FocusArea != focusArea)
        {
          continue;
        }
        if (altLink.Operation == Enumerators.AlternativeOperation.RIK)
        {
          pipeCount++;
        }
      }
      return pipeCount;
    }
    public double RIKPipeLength(IDictionary<string, Parameter> parameters)
    {
      bool filterByFocusArea = parameters.ContainsKey("FocusArea");
      double pipeLength = 0;

      string focusArea = "";
      if (filterByFocusArea)
      {
        focusArea = parameters["FocusArea"].Value;
      }

      foreach (AltLink altLink in altPackage.ModelAltLinks.Values)
      {
        if (filterByFocusArea && altLink.FocusArea != focusArea)
        {
          continue;
        }
        if (altLink.Operation == Enumerators.AlternativeOperation.RIK)
        {
          pipeLength += altLink.Length;
        }
      }
      return pipeLength;
    }
    /// <summary>
    /// Returns the count of Parking Control Targets identified to to include in the alternative, optionally filtered by focus area.
    /// </summary>
    /// <param name="parameters">Optional and required parameters for this method.  Optional parameters include FocusArea.</param>
    /// <returns>Count of Parking Control Targets indentified to include in the alternative, optionally filtered by FocusArea.</returns>
    public int ParkingControlCount(IDictionary<string, Parameter> parameters)
    {
      int count = 0;

      bool filterByFocusArea = parameters.ContainsKey("FocusArea");
      string focusArea = "";
      if (filterByFocusArea)
      {
        focusArea = parameters["FocusArea"].Value;
      }

      bool filterByConstructed = parameters.ContainsKey("Constructed");
      int constructed = 0;
      if (filterByConstructed)
      {
        constructed = parameters["Constructed"].ValueAsInt;
      }

      foreach (ParkingTarget pt in altPackage.ModelAltParkingTargets.Values)
      {
        if (filterByFocusArea && pt.FocusArea != focusArea)
        {
          continue;
        }
        if (!pt.InModel ||
        (filterByConstructed && pt.Constructed != constructed))
        {
          continue;
        }
        count++;
      }
      return count;
    }
    /// <summary>
    /// Returns the count of Roof Control Targets identified to to include in the alternative, optionally filtered by focus area.
    /// </summary>
    /// <param name="parameters">Optional and required parameters for this method.  Optional parameters include FocusArea.</param>
    /// <returns>Count of Roof Control Targets indentified to include in the alternative, optionally filtered by FocusArea.</returns>
    public int RoofControlCount(IDictionary<string, Parameter> parameters)
    {
      bool filterByFocusArea = parameters.ContainsKey("FocusArea");
      int count = 0;

      string focusArea = "";
      if (filterByFocusArea)
      {
        focusArea = parameters["FocusArea"].Value;
      }

      bool filterByConstructed = parameters.ContainsKey("Constructed");
      int constructed = 0;
      if (filterByConstructed)
      {
        constructed = parameters["Constructed"].ValueAsInt;
      }

      foreach (RoofTarget rt in altPackage.ModelAltRoofTargets.Values)
      {
        if (filterByFocusArea && rt.FocusArea != focusArea)
        {
          continue;
        }
        if (!rt.InModel ||
        (filterByConstructed && rt.Constructed != constructed))
        {
          continue;
        }
        count++;
      }
      return count;
    }
    /// <summary>
    /// Returns the count of Street Control Targets identified to to include in the alternative, optionally filtered by focus area.
    /// </summary>
    /// <param name="parameters">Optional and required parameters for this method.  Optional parameters include FocusArea.</param>
    /// <returns>Count of Street Control Targets indentified to include in the alternative, optionally filtered by FocusArea.</returns>
    public int StreetControlCount(IDictionary<string, Parameter> parameters)
    {
      bool filterByFocusArea = parameters.ContainsKey("FocusArea");
      int count = 0;

      string focusArea = "";
      if (filterByFocusArea)
      {
        focusArea = parameters["FocusArea"].Value;
      }

      bool filterByConstructed = parameters.ContainsKey("Constructed");
      int constructed = 0;
      if (filterByConstructed)
      {
        constructed = parameters["Constructed"].ValueAsInt;
      }

      foreach (StreetTarget st in altPackage.ModelAltStreetTargets.Values)
      {
        if (filterByFocusArea && st.FocusArea != focusArea)
        {
          continue;
        }
        if (!st.InModel ||
        (filterByConstructed && st.Constructed != constructed))
        {
          continue;
        }
        count++;
      }
      return count;
    }

  }
}
