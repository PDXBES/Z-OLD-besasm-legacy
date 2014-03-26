using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Utils.Events;
using SystemsAnalysis.Types;

namespace SystemsAnalysis.Reporting.ReportLibraries
{
  public class PumpStationReport : ReportBase
  {
    private PumpStations pumpStations;
    private Links links;
    private Nodes nodes;

    /// <summary>
    /// Creates a PumpReport from a collections of Links, Nodes and Dscs
    /// </summary>
    /// <param name="links">A Links collection to report on</param>
    /// <param name="nodes">A Nodes collection to report on</param>
    /// <param name="dscs">A Dscs collection to report on</param>
    public PumpStationReport(Links links, Nodes nodes)
    {
      this.links = links;
      this.nodes = nodes;
      this.OnStatusChanged(new StatusChangedArgs("Creating pump station objects."));
      pumpStations = new PumpStations(nodes);
    }
    /// <summary>
    /// Returns the name of a pump station
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID</param>
    /// <returns>The name of a pump station</returns>
    public string Name(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      PumpStation ps;
      ps = pumpStations[psID];
      return ps.Name + " Pump Station";
    }
    /// <summary>
    /// Returns the flow type of a pump station (C=Combined, S=Sanitary or D=Storm)
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID</param>
    /// <returns>The flow type of a pump station</returns>
    public string Type(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      PumpStation ps;
      ps = pumpStations[psID];
      return ps.Type;
    }
    /// <summary>
    /// Returns the address of a pump station
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID</param>
    /// <returns>The address of a pump station</returns>
    public string Location(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      PumpStation ps;
      ps = pumpStations[psID];
      return ps.Address;
    }
    public string NameAndLocation(IDictionary<string, Parameter> parameters)
    {
      return this.Name(parameters) + "; " + this.Location(parameters);
    }
    /// <summary>
    /// Returns the firm capacity (capacity with the largest pump offline) of a pump station in gpm
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID</param>
    /// <returns>The firm capacity of a pump station in gpm</returns>
    public int FirmCapacity(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      PumpStation ps;
      ps = pumpStations[psID];
      return (int)ps.FirmCapacity;
    }
    /// <summary>
    /// Returns the full capacity (capacity with all pumps running) of a pump station in gpm
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID</param>
    /// <returns>The full capacity of a pump station in gpm</returns>
    public int FullCapacity(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      PumpStation ps;
      ps = pumpStations[psID];
      return ps.FullCapacity;
    }
    /// <summary>
    /// Returns a description of the pumps operating sequence (ie Lead, Lag, Lag Lag) as modeled
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID and PumpIndex</param>
    /// <returns>Returns a description of the pumps operating sequence as modeled</returns>
    public string PumpNumber(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      int pumpIndex;
      pumpIndex = parameters["PumpIndex"].ValueAsInt;

      Pump pump = (Pump)pumpStations[psID].Pumps[pumpIndex];
      switch (pump.PumpNumber)
      {
        case 1:
          return "Lead Pump";
        case 2:
          return "Lag Pump";
        case 3:
          return "Lag Lag Pump";
        default:
          return "Lag " + (pump.PumpNumber - 1) + " Pump";
      }
    }
    /// <summary>
    /// Returns the motor speed of a pump in rpm
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID and PumpIndex</param>
    /// <returns>The motor speed of a pump in rpm</returns>
    public double PumpMotorSpeed(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      int pumpIndex;
      pumpIndex = parameters["PumpIndex"].ValueAsInt;

      Pump pump = (Pump)pumpStations[psID].Pumps[pumpIndex];
      return pump.MotorSpeed;
    }
    /// <summary>
    /// Returns the horse power of a pump
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID and PumpIndex</param>
    /// <returns>The horse power of a pump in HP</returns>
    public double PumpHP(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      int pumpIndex;
      pumpIndex = parameters["PumpIndex"].ValueAsInt;

      Pump pump = (Pump)pumpStations[psID].Pumps[pumpIndex];
      return pump.HorsePower;
    }
    /// <summary>
    /// Returns the pumping capacity of an individual pump in gpm
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID and PumpIndex</param>
    /// <returns>The pumping capacity of an individual pump in gpm</returns>
    public double PumpCapacity(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      int pumpIndex;
      pumpIndex = parameters["PumpIndex"].ValueAsInt;

      Pump pump = (Pump)pumpStations[psID].Pumps[pumpIndex];
      return pump.Capacity;
    }
    /// <summary>
    /// Return the index of a force main
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID and ForceMainIndex</param>
    /// <returns>The index of a force main</returns>
    public int ForceMainIndex(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      int forceMainIndex;
      forceMainIndex = parameters["ForceMainIndex"].ValueAsInt;

      return forceMainIndex;
    }
    /// <summary>
    /// Returns a the diameter of a force main in inches
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID and ForceMainIndex</param>
    /// <returns>The diameter of a force main in inches</returns>
    public double ForceMainDiameter(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      int forceMainIndex;
      forceMainIndex = parameters["ForceMainIndex"].ValueAsInt;

      ForceMain fm = (ForceMain)pumpStations[psID].ForceMains[forceMainIndex];
      return links.FindByMLinkID(fm.MLinkID).Diameter;
    }
    /// <summary>
    /// Returns a the length of a force main in ft
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID and ForceMainIndex</param>
    /// <returns>The length of a force main in ft</returns>
    public double ForceMainLength(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      int forceMainIndex;
      forceMainIndex = parameters["ForceMainIndex"].ValueAsInt;

      ForceMain fm = (ForceMain)pumpStations[psID].ForceMains[forceMainIndex];
      return fm.Length;
    }
    /// <summary>
    /// Returns a the static head of a force main in ft
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID and ForceMainIndex</param>
    /// <returns>The static head of a force main in ft</returns>
    public double ForceMainHead(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      int forceMainIndex;
      forceMainIndex = parameters["ForceMainIndex"].ValueAsInt;

      ForceMain fm = (ForceMain)pumpStations[psID].ForceMains[forceMainIndex];
      return fm.StaticHead;
    }
    /// <summary>
    /// Returns a the number of a wet well within a pump station
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID and WetWellIndex</param>
    /// <returns>The number of a wet well within a pump station</returns>
    public int WetWellNum(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      int wetWellIndex;
      wetWellIndex = parameters["WetWellIndex"].ValueAsInt;

      WetWell ww = (WetWell)pumpStations[psID].WetWells[wetWellIndex];
      return ww.Number;
    }
    /// <summary>
    /// Returns a string describing the shape a wet well
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID and WetWellIndex</param>
    /// <returns>A string describing the shape a wet well</returns>
    public string WetWellDimensions(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      int wetWellIndex;
      wetWellIndex = parameters["WetWellIndex"].ValueAsInt;

      WetWell ww = (WetWell)pumpStations[psID].WetWells[wetWellIndex];
      if (ww.Shape == "Rectangular")
      {
        return ww.Shape + " ("
        + ww.Dimension1 + " ft x "
        + ww.Dimension2 + " ft)";
      }
      else if (ww.Shape == "Circular")
      {
        return ww.Shape
        + " (diam. = " + ww.Dimension1 + " ft)";
      }
      else
      {
        return "Irregular";
      }
    }
    /// <summary>
    /// Returns the volume of a wet well in ft^3/ft
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID and WetWellIndex</param>
    /// <returns>The volume of a wet well in ft^3/ft</returns>
    public double WetWellVolume(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      int wetWellIndex;
      wetWellIndex = parameters["WetWellIndex"].ValueAsInt;

      WetWell ww = (WetWell)pumpStations[psID].WetWells[wetWellIndex];
      return ww.Volume;
    }
    /// <summary>
    /// Returns the a string describing the bypass location of a wet well
    /// </summary>
    /// <param name="parameters">Parameter collection containing a PumpStationID and WetWellIndex</param>
    /// <returns>The bypass location of a wet well</returns>
    public string WetWellBypass(IDictionary<string, Parameter> parameters)
    {
      int psID;
      psID = parameters["PumpStationID"].ValueAsInt;
      int wetWellIndex;
      wetWellIndex = parameters["WetWellIndex"].ValueAsInt;

      WetWell ww = (WetWell)pumpStations[psID].WetWells[wetWellIndex];
      return ww.ByPass;
    }

    public int[] PumpStationIDList
    {
      get
      {
        int i = 0;
        int[] psIDList;
        psIDList = new int[pumpStations.Count];
        foreach (PumpStation ps in pumpStations.Values)
        {
          psIDList[i] = ps.PSID;
          i++;
        }
        return psIDList;
      }
    }
    public int[] WetWellIndexList(int psID)
    {
      PumpStation ps = pumpStations[psID];
      int[] wetWellIDList;
      wetWellIDList = new int[ps.WetWells.Count];

      for (int i = 0; i < ps.WetWells.Count; i++)
      {
        wetWellIDList[i] = i;
      }
      return wetWellIDList;
    }
    public int[] ForceMainIndexList(int psID)
    {
      PumpStation ps = pumpStations[psID];
      int[] forceMainIDList;
      forceMainIDList = new int[ps.ForceMains.Count];
      for (int i = 0; i < ps.ForceMains.Count; i++)
      {
        forceMainIDList[i] = i;
      }

      return forceMainIDList;
    }
    public int[] PumpIndexList(int psID)
    {
      PumpStation ps = pumpStations[psID];
      int[] pumpIDList;
      pumpIDList = new int[ps.Pumps.Count];
      for (int i = 0; i < ps.Pumps.Count; i++)
      {
        pumpIDList[i] = i;
      }

      return pumpIDList;
    }

  }
}
