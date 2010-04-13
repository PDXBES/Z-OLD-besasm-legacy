// Project: SystemsAnalysis.Modeling, File: Model.cs
// Namespace: SystemsAnalysis.Modeling, Class: Model
// Path: C:\Development\DotNet\Modeling, Author: Arnel
// Code lines: 647, Size of file: 21.94 KB
// Creation date: 6/17/2008 3:16 PM
// Last modified: 7/1/2008 12:27 AM

#region Using directives
using System;
using System.Data;
using SystemsAnalysis;
using System.Collections.Generic;
using SystemsAnalysis.ModelConstruction;
#endregion

namespace SystemsAnalysis.Modeling
{
  /// <summary>
  /// Summary description for Class1.
  /// </summary>
  /// 
  public class Model
  {
    private string modelPath;
    private DataAccess.ModelDataSet modelDS;
    private Nodes _modelnodes;
    private Links _modellinks;
    private Dscs _modeldscs;
    private Dictionary<int, RoofTarget> modelRoofTargets;
    private Dictionary<int, ParkingTarget> modelParkingTargets;
    private Dictionary<int, StreetTarget> modelStreetTargets;
    private Dictionary<int, PipXP> modelConflicts;
    private string version;

    private SystemsAnalysis.Utils.INIFile iniFile;

    public Model(string modelPath)
    {
      this.modelPath = modelPath;
      try
      {
        modelDS = new DataAccess.ModelDataSet();
      }
      catch (Exception e)
      {
        throw new Exception("Could not create model: '" + e.ToString() + "'.", e);
      }

      if (!System.IO.File.Exists(modelPath + "\\Model.ini"))
      {
        throw new System.IO.FileNotFoundException();
      }
      iniFile = new SystemsAnalysis.Utils.INIFile(modelPath + "\\Model.ini");
      try
      {
        version = iniFile.GetINIString("Admin", "Version", "0.0");
      }
      catch
      {
        //It is useful to read start/stop links from 2.1 version models.
        //If reading from a 2.2 ini file fails, attempt to read from a 2.1 ini file
        try
        {
          version = iniFile.GetINIString("control", "Version", "0.0");
        }
        catch
        {
          throw new Exception("Could not determine model version.");
        }
      }
      ModelBuilder.RefreshDataAccess(modelPath, true);

      //This needs to occur only after data access has been refreshed, since links will be updated in case model has been moved
      try
      {
        //TODO: Uncomment this and make it work prior to release (jhb 12/22/09)
        //LoadConflicts(modelPath);
      } // try
      catch
      {
      } // catch
      return;
    }

    #region Properties for retrieving start/stop links
    /// <summary>
    ///  The start links associated with a model, as read from Model.ini.		
    /// </summary>
    /// <remarks>
    /// Each link is guaranteed to be a fully populated link object (including
    /// [LinkID], [MLinkID], [USNode], and [DSNode]) that exists within mdl_links.
    /// </remarks>
    public Links StartLinks
    {
      get
      {
        Links startLinks = new Links();
        if (iniFile.GetINIKeys("RootLinks") != null)
        {
          foreach (string s in iniFile.GetINIKeys("RootLinks"))
          {
            try
            {
              int mLinkID = Convert.ToInt32(s);
              // Start links are required to have an US node.
              // The best way to find this is to check the model
              // and create a full link object. If the start link
              // is not found in the model we will ignore it
              Link startLink = this.ModelLinks.FindByMLinkID(mLinkID);
              if (startLink != null)
              {
                startLinks.Add(startLink);
              }
              else
              {
                startLink = null;
                //TODO: Log start link missing from model
              }
            }
            catch (FormatException e)
            {
              //TODO: Log malformated start link
            }
          }
        }
        if (iniFile.GetINIKeys("forcedstartlinks") != null)
        {
          foreach (string s in iniFile.GetINIKeys("forcedstartlinks"))
          {
            try
            {
              int mLinkID = Convert.ToInt32(s);
              Link startLink = new Link(mLinkID, mLinkID, "", "");
              startLinks.Add(startLink);
            }
            catch (FormatException e)
            {
              //TODO: Log malformated stop link
            }
          }
        }
        return startLinks;
      }
    }
    /// <summary>
    /// The stop links associated with a model, as read from Model.ini.  
    /// </summary>
    /// <remarks>
    /// Each link is guaranteed to be a fully populated link object (including
    /// [LinkID], [MLinkID], [USNode], and [DSNode]) that exists within mdl_links.
    /// </remarks>		
    public Links StopLinks
    {
      get
      {
        Links stopLinks = new Links();
        if (iniFile.GetINIKeys("StopLinks") != null)
        {
          foreach (string s in iniFile.GetINIKeys("StopLinks"))
          {
            try
            {
              int mLinkID = Convert.ToInt32(s);
              Link stopLink = this.ModelLinks.FindByMLinkID(mLinkID);
              if (stopLink != null)
              {
                stopLinks.Add(this.ModelLinks.FindByMLinkID(mLinkID));
              }
              else
              {
                stopLink = null;
              }
              //TODO: Log stop link missing from model
            }
            catch (FormatException e)
            {
              //TODO: Log malformated stop link
            }
          }
        }
        if (iniFile.GetINIKeys("ForcedStopLinks") != null)
        {
          foreach (string s in iniFile.GetINIKeys("ForcedStopLinks"))
          {
            try
            {
              int mLinkID = Convert.ToInt32(s);
              Link stopLink = new Link(mLinkID, mLinkID, "", "");
              stopLinks.Add(stopLink);
            }
            catch (FormatException e)
            {
              //TODO: Log malformated stop link
            }
          }
        }
        return stopLinks;
      }
    }


    #endregion

    /// <summary>
    /// Returns the Links collection associated with this Model.
    /// </summary>
    public Links ModelLinks
    {
      get
      {
        if (_modellinks == null)
        {
          this.InitModelLinks();
        }
        return this._modellinks;
      }
    }
    /// <summary>
    /// Returns the Nodes collection associated with this Model.
    /// </summary>
    public Nodes ModelNodes
    {
      get
      {
        if (_modelnodes == null)
        {
          this.InitModelNodes();
        }
        return this._modelnodes;
      }
    }
    /// <summary>
    /// Returns the Dscs collection associated with this Model.
    /// </summary>
    public Dscs ModelDscs
    {
      get
      {
        if (_modeldscs == null)
        {
          this.InitModelDscs();
        }
        return this._modeldscs;
      }
    }
    /// <summary>
    /// Returns the RoofTargets dictionary associated with this Model.
    /// </summary>
    public Dictionary<int, RoofTarget> ModelRoofTargets
    {
      get
      {
        if (modelRoofTargets == null)
        {
          LoadRoofTargets();
        }

        return modelRoofTargets;
      }
    }
    /// <summary>
    /// Returns the ParkingTargets dictionary associated with this Model.
    /// </summary>
    public Dictionary<int, ParkingTarget> ModelParkingTargets
    {
      get
      {
        if (modelParkingTargets == null)
        {
          LoadParkingTargets();
        }

        return modelParkingTargets;
      }
    }
    /// <summary>
    /// Returns the StreetTargets dictionary associated with this Model.
    /// </summary>
    public Dictionary<int, StreetTarget> ModelStreetTargets
    {
      get
      {
        if (modelStreetTargets == null)
        {
          LoadStreetTargets();
        }

        return modelStreetTargets;
      }
    }
    /// <summary>
    /// Gets the path of the underlying model files.
    /// </summary>
    public string Path
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get { return this.modelPath; }
    }

    /// <summary>
    /// Return the model version number
    /// </summary>
    public string Version
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get { return this.version; }
    }

    /// <summary>
    ///	Finds a Link in the current model by the LinkID. If the model
    ///	has not yet loaded it's Links collection this method will first
    ///	load the Links collection and set LinksLoaded = true.
    /// </summary>
    /// <param name="linkID">The LinkID of the Link object to find.</param>
    /// <returns>The Link with the given LinkID.</returns>
    public Link FindLink(int linkID)
    {
      return this.ModelLinks[linkID];
    }

    #region Functions for loading model components
    /// <summary>
    /// Loads this Model's Links collection with Link objects from a
    /// previous trace. These Links will be created from an existing
    /// mdl_links_ac table. 
    /// </summary>		
    private void InitModelLinks()
    {
      try
      {
        this._modellinks = new Links(this.modelPath);
      }
      catch (Exception e)
      {
        throw new Exception("Error loading Links collection: '" +
        e.Message + "'.", e);
      }
    }

    /// <summary>
    /// Loads this Model's Nodes collection with Node objects from a
    /// previous trace. These Nodes will be created from an existing
    /// mdl_nodes_ac table.
    /// </summary>
    private void InitModelNodes()
    {
      try
      {
        _modelnodes = new Nodes();
        _modelnodes.LoadFromModel(this.modelPath);
      }
      catch (Exception e)
      {
        throw new Exception("Error loading Nodes collection: '" +
        e.Message + "'.", e);
      }
    }

    /// <summary>
    /// Loads this Model's Dscs collection with DSC objects from a
    /// previous trace. These Dscs will be created from an existing
    /// mdl_DirSC_ac table.
    /// </summary>
    public void InitModelDscs()
    {
      try
      {
        _modeldscs = new Dscs();
        _modeldscs.LoadFromModel(this.modelPath);
      }
      catch (Exception e)
      {
        throw new Exception("Error loading DSCs collection: '" +
        e.Message + "'.", e);
      }
    }
    /// <summary>
    /// Loads this Model's RoofTargets dictionary with RoofTargets from a
    /// previous trace.  These RoofTargets will be created from an existing
    /// mst_rooftargets_ac table.
    /// </summary>
    /// <returns>An &lt;int,RoofTargets&gt; dictionary containing RoofTargets
    /// read from this Model's mst_rooftargets_ac table</returns>
    public Dictionary<int, RoofTarget> LoadRoofTargets()
    {
      try
      {
        DataAccess.ModelDataSetTableAdapters.MdlRoofTargetsTableAdapter mdlRoofTargetsAdapter =
        new SystemsAnalysis.DataAccess.ModelDataSetTableAdapters.MdlRoofTargetsTableAdapter(modelPath);
        mdlRoofTargetsAdapter.Fill(modelDS.MdlRoofTargets);

        modelRoofTargets = new Dictionary<int, RoofTarget>();
        foreach (DataAccess.ModelDataSet.MdlRoofTargetsRow row in modelDS.MdlRoofTargets)
        {
          modelRoofTargets.Add(row.ICID, new RoofTarget(row));
        }
      }
      catch (Exception e)
      {
        throw new Exception("Error loading RoofTargets collection: '" +
        e.Message + "'.", e);
      }

      return modelRoofTargets;
    }

    /// <summary>
    /// Loads this Model's ParkingTargets dictionary with ParkingTargets from a
    /// previous trace.  These ParkingTargets will be created from an existing
    /// mst_parkingtargets_ac table.
    /// </summary>
    /// <returns>An &lt;int,ParkingTargets&gt; dictionary containing ParkingTargets
    /// read from this Model's mst_parkingtargets_ac table</returns>
    public Dictionary<int, ParkingTarget> LoadParkingTargets()
    {
      try
      {
        DataAccess.ModelDataSetTableAdapters.MdlParkingTargetsTableAdapter mdlParkingTargetsAdapter =
        new SystemsAnalysis.DataAccess.ModelDataSetTableAdapters.MdlParkingTargetsTableAdapter(modelPath);
        mdlParkingTargetsAdapter.Fill(modelDS.MdlParkingTargets);

        modelParkingTargets = new Dictionary<int, ParkingTarget>();
        foreach (DataAccess.ModelDataSet.MdlParkingTargetsRow row in modelDS.MdlParkingTargets)
        {
          modelParkingTargets.Add(row.ICID, new ParkingTarget(row));
        }
      }
      catch (Exception e)
      {
        throw new Exception("Error loading ParkingTargets collection: '" +
        e.Message + "'.", e);
      }

      return modelParkingTargets;
    }

    /// <summary>
    /// Loads this Model's StreetTargets dictionary with StreetTargets from a
    /// previous trace.  These StreetTargets will be created from an existing
    /// mst_streettargets_ac table.
    /// </summary>
    /// <returns>An &lt;int,StreetTargets&gt; dictionary containing StreetTargets
    /// read from this Model's mst_streettargets_ac table</returns>
    public Dictionary<int, StreetTarget> LoadStreetTargets()
    {
      try
      {
        DataAccess.ModelDataSetTableAdapters.MdlStreetTargetsTableAdapter mdlStreetTargetsAdapter =
        new SystemsAnalysis.DataAccess.ModelDataSetTableAdapters.MdlStreetTargetsTableAdapter(modelPath);
        mdlStreetTargetsAdapter.Fill(modelDS.MdlStreetTargets);

        modelStreetTargets = new Dictionary<int, StreetTarget>();
        foreach (DataAccess.ModelDataSet.MdlStreetTargetsRow row in modelDS.MdlStreetTargets)
        {
          modelStreetTargets.Add(row.ICID, new StreetTarget(row));
        }
      }
      catch (Exception e)
      {
        throw new Exception("Error loading StreetTargets collection: '" +
        e.Message + "'.", e);
      }

      return modelStreetTargets;
    }

    #endregion

    /// <summary>
    /// Load conflicts
    /// </summary>
    /// <param name="modelPath">Model path</param>
    public void LoadConflicts(string modelPath)
    {
      try
      {
        DataAccess.ModelDataSetTableAdapters.MdlPipXPTableAdapter mdlPipXPAdapter =
            new SystemsAnalysis.DataAccess.ModelDataSetTableAdapters.MdlPipXPTableAdapter(modelPath);

        DataAccess.ModelDataSet.MdlPipXPDataTable mdlPipXPTable =
            new SystemsAnalysis.DataAccess.ModelDataSet.MdlPipXPDataTable();

        mdlPipXPAdapter.Fill(mdlPipXPTable);

        modelConflicts = new Dictionary<int, PipXP>();
        foreach (DataAccess.ModelDataSet.MdlPipXPRow row in mdlPipXPTable)
        {
          modelConflicts.Add(row.MLinkID, new PipXP(row));
        } // foreach  (row)
      } // try
      catch (Exception e)
      {
        throw new Exception("Error loading pipe conflicts collection: '" +
            e.ToString() + "'.", e);
      } // catch
    } // LoadConflicts(modelPath)

    /// <summary>
    /// Returns the average pipe depth of a Link in a Model
    /// </summary>
    /// <param name="link">A link in a Model</param>
    /// <returns>The link's average pipe depth</returns>
    public double PipeDepth(Link link)
    {
      Node usNode = ModelNodes[link.USNodeName];
      Node dsNode = ModelNodes[link.DSNodeName];

      return ((usNode.GroundElevation - link.USIE) + (dsNode.GroundElevation - link.DSIE)) / 2;
    }

    /// <summary>
    /// Returns the number of street targets in a Model
    /// </summary>
    public int CountStreetTargetsInModel
    {
      get
      {
        int numInModel = 0;
        foreach (KeyValuePair<int, StreetTarget> target in modelStreetTargets)
        {
          if (target.Value.InModel)
            numInModel++;
        }
        return numInModel;
      }
    }

    /// <summary>
    /// Returns the number of street targets to be built
    /// </summary>
    public int CountStreetTargetsToBeBuilt
    {
      get
      {
        int numToBeBuilt = 0;
        foreach (KeyValuePair<int, StreetTarget> target in modelStreetTargets)
        {
          if (target.Value.ToBeBuilt)
            numToBeBuilt++;
        }
        return numToBeBuilt;
      }
    }

    /// <summary>
    /// Returns the number of roof targets in a Model
    /// </summary>
    public int CountRoofTargetsInModel
    {
      get
      {
        int numInModel = 0;
        foreach (KeyValuePair<int, RoofTarget> target in modelRoofTargets)
        {
          if (target.Value.InModel)
            numInModel++;
        }
        return numInModel;
      }
    }

    /// <summary>
    /// Returns the number of roof targets to be built
    /// </summary>
    public int CountRoofTargetsToBeBuilt
    {
      get
      {
        int numToBeBuilt = 0;
        foreach (KeyValuePair<int, RoofTarget> target in modelRoofTargets)
        {
          if (target.Value.ToBeBuilt)
            numToBeBuilt++;
        }
        return numToBeBuilt;
      }
    }

    /// <summary>
    /// Returns the number of parking targets in model
    /// </summary>
    public int CountParkingTargetsInModel
    {
      get
      {
        int numInModel = 0;
        foreach (KeyValuePair<int, ParkingTarget> target in modelParkingTargets)
        {
          if (target.Value.InModel)
            numInModel++;
        }
        return numInModel;
      }
    }

    /// <summary>
    /// Returns the number of parking targets to be built
    /// </summary>
    public int CountParkingTargetsToBeBuilt
    {
      get
      {
        int numToBeBuilt = 0;
        foreach (KeyValuePair<int, ParkingTarget> target in modelParkingTargets)
        {
          if (target.Value.ToBeBuilt)
            numToBeBuilt++;
        }
        return numToBeBuilt;
      }
    }

    public double TotalLengthOfPipes
    {
      get
      {
        double length = 0;
        foreach (Link link in ModelLinks.Values)
          length += link.Length;
        return length;
      }
    }

    public double AverageDepthOfPipes
    {
      get
      {
        double averageDepth = 0;
        foreach (Link link in ModelLinks.Values)
          averageDepth += PipeDepth(link);
        averageDepth /= ModelLinks.Count;
        return averageDepth;
      }
    }

    public double AverageDiameterOfPipes
    {
      get
      {
        double averageDiameter = 0;
        foreach (Link link in ModelLinks.Values)
          averageDiameter += link.Diameter;
        averageDiameter /= ModelLinks.Count;
        return averageDiameter;
      }
    }
    public double LengthOfPipesLessThanEqualDiameter(double diameter)
    {
      double length = 0;
      foreach (Link link in ModelLinks.Values)
      {
        if (link.Diameter <= diameter)
          length += link.Length;
      }
      return length;
    }

    public double LengthOfPipesGreaterThanDiameter(double diameter)
    {
      double length = 0;
      foreach (Link link in ModelLinks.Values)
      {
        if (link.Diameter > diameter)
          length += link.Length;
      }
      return length;
    }

    public double LengthOfPipesLessThanEqualDepth(double depth)
    {
      double length = 0;
      foreach (Link link in ModelLinks.Values)
      {
        if (PipeDepth(link) <= depth)
          length += link.Length;
      }
      return length;
    }

    public double LengthOfPipesGreaterThanDepth(double depth)
    {
      double length = 0;
      foreach (Link link in ModelLinks.Values)
      {
        if (PipeDepth(link) > depth)
          length += link.Length;
      }
      return length;
    }

    /// <summary>
    /// Conflict from link
    /// </summary>
    /// <param name="link">Link</param>
    /// <returns>Pip XP</returns>
    public PipXP ConflictFromLink(Link link)
    {
      foreach (KeyValuePair<int, PipXP> kvPair in modelConflicts)
      {
        if (kvPair.Value.MstLinkID == link.MLinkID)
          return kvPair.Value;
      } // foreach  (kvPair)
      return null;
    } // ConflictFromLink(link)
  }
}
