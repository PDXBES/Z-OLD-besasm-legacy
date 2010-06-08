// Project: SystemsAnalysis.Modeling.Alternatives, File: AlternativePackage.cs
// Namespace: SystemsAnalysis.Modeling.Alternatives, Class: AlternativePackage
// Path: C:\Development\Alternatives, Author: Arnel
// Code lines: 392, Size of file: 10.49 KB
// Creation date: 9/7/2007 11:37 PM
// Last modified: 6/4/2008 1:26 AM
// Generated with Commenter by abi.exDream.com

#region Using directives
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Modeling.Alternatives;
using SystemsAnalysis.Types;
#endregion

namespace SystemsAnalysis.Modeling.Alternatives
{
  public class AlternativePackage
  {
    private AltLinks altLinks;
    private AltNodes altNodes;
    private AltDscs altDscs;
    private Dictionary<int, ParkingTarget> altParkingTargets;
    private Dictionary<int, RoofTarget> altRoofTargets;
    private Dictionary<int, StreetTarget> altStreetTargets;
    private Dictionary<int, AltPipXP> altConflicts;
    private List<string> focusAreaList;
    private Model baseModel;
    private string alternativePath;
    private AlternativeConfiguration altConfiguration;
    private string alternativeName;

    /// <summary>
    /// Returns the path to a base model
    /// </summary>
    /// <param name="alternativePath">The path to an alternative</param>
    /// <returns>The path to the base model containing the provided alternative</returns>
    public static string BaseModelPathFromAlternativePath(string alternativePath)
    {
      string baseModelPath;
      baseModelPath = System.IO.Path.GetFullPath(alternativePath + @"\..\..");
      return baseModelPath;
    }

    /// <summary>
    /// Creates a new AlternativePackage for the alternative located at the specified path
    /// </summary>
    /// <param name="alternativePath">The path to the alternative for which to create alternative objects</param>
    public AlternativePackage(string alternativePath)
    {
      altConfiguration = new AlternativeConfiguration(alternativePath + (alternativePath.EndsWith("\\") ? "" : "\\") + "alternative_configuration.xml");
      if (altConfiguration.AlternativeVersion != AlternativeConfiguration.MasterVersion)
      {
        throw new Exception("Error reading alternative: Supported version is " + AlternativeConfiguration.MasterVersion + "; Provided version is " + altConfiguration.AlternativeVersion + ". Update using the AlternativesToolkit.");
      }

      try
      {
        baseModel = new Model(BaseModelPathFromAlternativePath(alternativePath));
        this.altLinks = new AltLinks(alternativePath);
        this.altNodes = new AltNodes(alternativePath);
        this.altDscs = new AltDscs(alternativePath);
        LoadAltParkingTargets(alternativePath);

        List<int> missingAltRoofTargets;
        missingAltRoofTargets = ValidateAltRoofTargets(alternativePath);
        if (missingAltRoofTargets.Count != 0)
        {
          string badICs = "Found ICs that are not contained in the base model: ";
          foreach (int i in missingAltRoofTargets)
          {
            badICs += i + " ";
          }
          throw new Exception(badICs);
        }

        LoadAltRoofTargets(alternativePath);
        LoadAltStreetTargets(alternativePath);
        LoadFocusAreaList();
        this.alternativePath = alternativePath;
        string[] blah = alternativePath.Split(new char[] { '\\' });
        alternativeName = blah[blah.Length - 1];
      }
      catch (Exception ex)
      {
        throw new Exception("Unable to load alternative '" + alternativePath + "': " + ex.Message);
      }

      try
      {
        //LoadAltConflicts(alternativePath);
      }
      catch (Exception e)
      {
        throw new Exception("Problem found reading alternative. Check for duplicate alternative IDs\n" +
          e.Message);
      } // catch (Exception)

      this.alternativePath = alternativePath;
    }

    public String AlternativePath
    {
      get { return this.alternativePath; }
    }

    public IList FocusAreaList
    {
      get { return focusAreaList; }
    }
    private void LoadFocusAreaList()
    {
      focusAreaList = new List<string>();
      foreach (AltLink altLink in ModelAltLinks.Values)
      {
        if (!focusAreaList.Contains(altLink.FocusArea))
        {
          focusAreaList.Add(altLink.FocusArea);
        }
      }
      foreach (RoofTarget rt in ModelAltRoofTargets.Values)
      {
        if (!focusAreaList.Contains(rt.FocusArea))
        {
          focusAreaList.Add(rt.FocusArea);
        }
      }
      foreach (StreetTarget st in ModelAltStreetTargets.Values)
      {
        if (!focusAreaList.Contains(st.FocusArea))
        {
          focusAreaList.Add(st.FocusArea);
        }
      }
      foreach (ParkingTarget pt in ModelAltParkingTargets.Values)
      {
        if (!focusAreaList.Contains(pt.FocusArea))
        {
          focusAreaList.Add(pt.FocusArea);
        }
      }
      focusAreaList.Sort();
    }

    public AltLinks ModelAltLinks
    {
      get
      {
        return altLinks;
      }
    }

    public AltNodes ModelAltNodes
    {
      get
      {
        return altNodes;
      }
    }

    public AltDscs ModelAltDscs
    {
      get
      {
        return altDscs;
      }
    }

    public Dictionary<int, ParkingTarget> ModelAltParkingTargets
    {
      get
      {
        return altParkingTargets;
      }
    }

    public Dictionary<int, RoofTarget> ModelAltRoofTargets
    {
      get
      {
        return altRoofTargets;
      }
    }

    public Dictionary<int, StreetTarget> ModelAltStreetTargets
    {
      get
      {
        return altStreetTargets;
      }
    }

    /// <summary>
    /// Model alt conflicts
    /// </summary>
    /// <returns>Dictionary</returns>
    public Dictionary<int, AltPipXP> ModelAltConflicts
    {
      get
      {
        return altConflicts;
      }
    } // ModelAltConflicts

    /// <summary>
    /// Alt conflict from alt link
    /// </summary>
    /// <param name="altLink">Alt link</param>
    /// <returns>Alt pip XP</returns>
    public AltPipXP AltConflictFromAltLink(AltLink altLink)
    {
      foreach (KeyValuePair<int, AltPipXP> kvPair in ModelAltConflicts)
      {
        if (kvPair.Value.AltLinkID == altLink.AltLinkID)
          return kvPair.Value;
      } // foreach  (altPipXP)
      return null;
    } // AltConflictFromAltLink(altLink)

    public Model BaseModel
    {
      get
      {
        return baseModel;
      }
    }

    public string AlternativeName
    {
      get { return this.alternativeName; }
    }

    /// <summary>
    /// Copies the Alternative to a new Base Model. Model IDs are updated to be consistent with the new Base Model. Any elements which
    /// can't be matched to a Link in the new Base Model will receive MdlLinkId = -1;
    /// </summary>
    /// <param name="newModel"></param>
    public void Copy(Model newModel)
    {
      Links baseModelLinks = baseModel.ModelLinks;
      Links newModelLinks = newModel.ModelLinks;
      foreach (AltLink altLink in this.ModelAltLinks)
      {
        //ADD operation does not need to be updated, because ADDs have no reference to an existing link
        if (altLink.Operation == Enumerators.AlternativeOperation.ADD)
        {
          continue;
        }
        Link oldMdlLink;
        Link newMdlLink;
        int mstLinkId;

        oldMdlLink = baseModelLinks[altLink.LinkID];
        mstLinkId = oldMdlLink.MLinkID;

        newMdlLink = newModelLinks.FindByMLinkID(mstLinkId);
        if (newMdlLink == null)
        {
          altLink.LinkID = -1;
        }

        altLink.LinkID = newMdlLink.LinkID;
      }

      Dscs baseModelDscs = baseModel.ModelDscs;
      Dscs newModelDscs = baseModel.ModelDscs;
      foreach (AltDsc altDsc in this.ModelAltDscs)
      {
        Dsc oldMdlDsc;
        Dsc newMdlDsc;

        Link oldMdlLink;
        Link newMdlLink;

        oldMdlDsc = baseModelDscs[altDsc.DscId];
        newMdlDsc = newModelDscs[altDsc.DscId];

        if (altDsc.LinkSourceSan == "mdl")
        {
          int mstLinkId = oldMdlDsc.ToMLinkSan;


          newMdlLink = newModelLinks.FindByMLinkID(mstLinkId);
          if (newMdlLink == null)
          {
            altDsc.LinkIDSan = -1;
          }

          altDsc.LinkIDSan = newMdlLink.LinkID;
        }

        if (altDsc.LinkSourceStorm == "mdl")
        {
        }

      }
    }

    void LoadAltParkingTargets(string alternativePath)
    {
      try
      {
        DataAccess.AlternativeDataSetTableAdapters.AltParkingTargetsTableAdapter altParkingTargetsAdapter =
          new SystemsAnalysis.DataAccess.AlternativeDataSetTableAdapters.AltParkingTargetsTableAdapter(alternativePath);
        DataAccess.AlternativeDataSet.AltParkingTargetsDataTable altParkingTargetsTable =
          new SystemsAnalysis.DataAccess.AlternativeDataSet.AltParkingTargetsDataTable();

        altParkingTargetsAdapter.Fill(altParkingTargetsTable);

        altParkingTargets = new Dictionary<int, ParkingTarget>();
        foreach (DataAccess.AlternativeDataSet.AltParkingTargetsRow row in altParkingTargetsTable)
        {
          altParkingTargets.Add(row.ICID, new ParkingTarget(baseModel.ModelParkingTargets[row.ICID],
            row.IsFocusAreaNull() ? "" : row.FocusArea));
          altParkingTargets[row.ICID].ToBeBuilt = row.BuildModelIC; //Arnel, we need to copy [BuildModelIC] from altParkingTargets_ac.tab to the ParkingTargets object
          altParkingTargets[row.ICID].Constructed = row.Constructed;
        }
      }
      catch (Exception e)
      {
        throw new Exception("Error loading AltParkingTargets collection: '" +
                    e.Message + "'.", e);
      }
    }

    public List<int> ValidateAltRoofTargets(string alternativePath)
    {
      List<int> missingAltRoofTargets = new List<int>();
      DataAccess.AlternativeDataSetTableAdapters.AltRoofTargetsTableAdapter altRoofTargetsAdapter =
          new SystemsAnalysis.DataAccess.AlternativeDataSetTableAdapters.AltRoofTargetsTableAdapter(alternativePath);
        DataAccess.AlternativeDataSet.AltRoofTargetsDataTable altRoofTargetsTable =
          new SystemsAnalysis.DataAccess.AlternativeDataSet.AltRoofTargetsDataTable();

        altRoofTargetsAdapter.Fill(altRoofTargetsTable);

        altRoofTargets = new Dictionary<int, RoofTarget>();
        foreach (DataAccess.AlternativeDataSet.AltRoofTargetsRow row in altRoofTargetsTable)
        {
          if (!baseModel.ModelRoofTargets.ContainsKey(row.ICID))
          {
            missingAltRoofTargets.Add(row.ICID);
          }          
        }
        return missingAltRoofTargets;
    }
    void LoadAltRoofTargets(string alternativePath)
    {
      try
      {
        DataAccess.AlternativeDataSetTableAdapters.AltRoofTargetsTableAdapter altRoofTargetsAdapter =
          new SystemsAnalysis.DataAccess.AlternativeDataSetTableAdapters.AltRoofTargetsTableAdapter(alternativePath);
        DataAccess.AlternativeDataSet.AltRoofTargetsDataTable altRoofTargetsTable =
          new SystemsAnalysis.DataAccess.AlternativeDataSet.AltRoofTargetsDataTable();

        altRoofTargetsAdapter.Fill(altRoofTargetsTable);

        altRoofTargets = new Dictionary<int, RoofTarget>();
        foreach (DataAccess.AlternativeDataSet.AltRoofTargetsRow row in altRoofTargetsTable)
        {
          if (!baseModel.ModelRoofTargets.ContainsKey(row.ICID))
          {
            throw new Exception("AltRoofTarget with ICID '" + row.ICID + "' not found in Base Model.");       
          }
          altRoofTargets.Add(row.ICID, new RoofTarget(baseModel.ModelRoofTargets[row.ICID], row.IsFocusAreaNull() ? "" : row.FocusArea));
          altRoofTargets[row.ICID].ToBeBuilt = row.BuildModelIC;
          altRoofTargets[row.ICID].Constructed = row.Constructed;
        }
      }
      catch (Exception e)
      {
        throw new Exception("Error loading AltRoofTargets collection: '" +
          e.Message + "'.", e);
      }
    }

    void LoadAltStreetTargets(string alternativePath)
    {
      try
      {
        DataAccess.AlternativeDataSetTableAdapters.AltStreetTargetsTableAdapter altStreetTargetsAdapter =
          new SystemsAnalysis.DataAccess.AlternativeDataSetTableAdapters.AltStreetTargetsTableAdapter(alternativePath);
        DataAccess.AlternativeDataSet.AltStreetTargetsDataTable altStreetTargetsTable =
          new SystemsAnalysis.DataAccess.AlternativeDataSet.AltStreetTargetsDataTable();

        altStreetTargetsAdapter.Fill(altStreetTargetsTable);

        altStreetTargets = new Dictionary<int, StreetTarget>();
        foreach (DataAccess.AlternativeDataSet.AltStreetTargetsRow row in altStreetTargetsTable)
        {
          if(altStreetTargets.ContainsKey(row.ICID))
          {
            throw new Exception(("AltStreetTarget with ICID '" + row.ICID + "' has a duplicate ICID in the Base Model."));       
          }       
          altStreetTargets.Add(row.ICID, new StreetTarget(baseModel.ModelStreetTargets[row.ICID], row.IsFocusAreaNull() ? "" : row.FocusArea));
          altStreetTargets[row.ICID].ToBeBuilt = row.BuildModelIC;
          altStreetTargets[row.ICID].Constructed = row.Constructed;
        }
      }
      catch (Exception e)
      {
        throw new Exception("Error loading AltStreetTargets collection: '" +
                    e.Message + "'.", e);
      }
    }

    /// <summary>
    /// Load alt conflicts
    /// </summary>
    /// <param name="alternativePath">Alternative path</param>
    private void LoadAltConflicts(string alternativePath)
    {
      try
      {
        DataAccess.AlternativeDataSetTableAdapters.AltPipXPTableAdapter altPipXPAdapter =
          new SystemsAnalysis.DataAccess.AlternativeDataSetTableAdapters.AltPipXPTableAdapter(alternativePath);
        DataAccess.AlternativeDataSet.AltPipXPDataTable altPipXPTable =
          new SystemsAnalysis.DataAccess.AlternativeDataSet.AltPipXPDataTable();

        altPipXPAdapter.Fill(altPipXPTable);

        altConflicts = new Dictionary<int, AltPipXP>();
        foreach (DataAccess.AlternativeDataSet.AltPipXPRow row in altPipXPTable)
        {
          altConflicts.Add(row.AltLinkID, new AltPipXP(row));
        } // foreach  (row)
      } // try
      catch (Exception e)
      {
        throw new Exception("Error loading pipe conflicts collection: '" +
          e.ToString() + "'.", e);
      } // catch
    } // LoadAltConflicts(alternativePath)

    public double PipeDepth(AltLink link)
    {
      AltNode usNode = altNodes[link.USNodeName];
      Node baseUSNode;
      double usGround;
      if (usNode == null)
      {
        baseUSNode = baseModel.ModelNodes[link.USNodeName];
        usGround = baseUSNode.GroundElevation;
      }
      else
        usGround = usNode.GroundElevation;

      AltNode dsNode = altNodes[link.DSNodeName];
      Node baseDSNode;
      double dsGround;
      if (dsNode == null)
      {
        baseDSNode = baseModel.ModelNodes[link.DSNodeName];
        dsGround = baseDSNode.GroundElevation;
      }
      else
        dsGround = dsNode.GroundElevation;

      return ((usGround - link.USIE) + (dsGround - link.DSIE)) / 2;
    }

    public double TotalLengthOfPipes
    {
      get
      {
        double length = 0;
        foreach (AltLink link in altLinks.Values)
        {
          if (link.Operation == Enumerators.AlternativeOperation.DEL || link.Operation == Enumerators.AlternativeOperation.SPL)
            continue;
          length += link.Length;
        }
        return length;
      }
    }

    public double TotalLengthOfPipesByFocusArea(string FocusArea)
    {
      double length = 0;
      foreach (AltLink link in altLinks.Values)
      {
        if (link.Operation == Enumerators.AlternativeOperation.DEL || link.Operation == Enumerators.AlternativeOperation.SPL || (link.FocusArea.ToUpper() != FocusArea.ToUpper()))
          continue;
        length += link.Length;
      }
      return length;
    }

    public double AverageDepthOfPipes
    {
      get
      {
        double averageDepth = 0;
        foreach (AltLink link in altLinks.Values)
        {
          if (link.Operation == Enumerators.AlternativeOperation.DEL || link.Operation == Enumerators.AlternativeOperation.SPL)
            continue;
          averageDepth += PipeDepth(link);
        }
        averageDepth /= altLinks.Count;
        return averageDepth;
      }
    }

    public double AverageDepthOfPipesByFocusArea(string FocusArea)
    {
      double averageDepth = 0;
      int numLinksForFocusArea = 0;
      foreach (AltLink link in altLinks.Values)
      {
        if (link.Operation == Enumerators.AlternativeOperation.DEL || link.Operation == Enumerators.AlternativeOperation.SPL || (link.FocusArea.ToUpper() != FocusArea.ToUpper()))
          continue;
        averageDepth += PipeDepth(link);
        numLinksForFocusArea += 1;
      }

      if (numLinksForFocusArea == 0)
        return 0;
      else
      {
        averageDepth /= numLinksForFocusArea;
        return averageDepth;
      }
    }

    public double AverageDiameterOfPipes
    {
      get
      {
        double averageDiameter = 0;
        foreach (AltLink link in altLinks.Values)
        {
          if (link.Operation == Enumerators.AlternativeOperation.DEL || link.Operation == Enumerators.AlternativeOperation.SPL)
            continue;
          averageDiameter += link.Diameter;
        }

        averageDiameter /= altLinks.Count;
        return averageDiameter;
      }
    }

    public double AverageDiameterOfPipesByFocusArea(string FocusArea)
    {
      double averageDiameter = 0;
      int numLinksForFocusArea = 0;
      foreach (AltLink link in altLinks.Values)
      {
        if (link.Operation == Enumerators.AlternativeOperation.DEL || link.Operation == Enumerators.AlternativeOperation.SPL || (link.FocusArea.ToUpper() != FocusArea.ToUpper()))
          continue;
        averageDiameter += link.Diameter;
        numLinksForFocusArea += 1;
      }

      if (numLinksForFocusArea == 0)
        return 0;
      else
      {
        averageDiameter /= numLinksForFocusArea;
        return averageDiameter;
      }
    }

    public double LengthOfPipesLessThanEqualDiameter(double diameter)
    {
      double length = 0;
      foreach (AltLink link in altLinks.Values)
      {
        if (link.Operation == Enumerators.AlternativeOperation.DEL || link.Operation == Enumerators.AlternativeOperation.SPL)
          continue;
        if (link.Diameter <= diameter)
          length += link.Length;
      }
      return length;
    }

    public double LengthOfPipesLessThanEqualDiameterByFocusArea(double diameter, string FocusArea)
    {
      double length = 0;
      foreach (AltLink link in altLinks.Values)
      {
        if (link.Operation == Enumerators.AlternativeOperation.DEL || link.Operation == Enumerators.AlternativeOperation.SPL || (link.FocusArea.ToUpper() != FocusArea.ToUpper()))
          continue;
        if (link.Diameter <= diameter)
          length += link.Length;
      }
      return length;
    }

    public double LengthOfPipesGreaterThanDiameter(double diameter)
    {
      double length = 0;
      foreach (AltLink link in altLinks.Values)
      {
        if (link.Operation == Enumerators.AlternativeOperation.DEL || link.Operation == Enumerators.AlternativeOperation.SPL)
          continue;
        if (link.Diameter > diameter)
          length += link.Length;
      }
      return length;
    }

    public double LengthOfPipesGreaterThanDiameterByFocusArea(double diameter, string FocusArea)
    {
      double length = 0;
      foreach (AltLink link in altLinks.Values)
      {
        if (link.Operation == Enumerators.AlternativeOperation.DEL || link.Operation == Enumerators.AlternativeOperation.SPL || (link.FocusArea.ToUpper() != FocusArea.ToUpper()))
          continue;
        if (link.Diameter > diameter)
          length += link.Length;
      }
      return length;
    }

    public double LengthOfPipesLessThanEqualDepth(double depth)
    {
      double length = 0;
      foreach (AltLink link in altLinks.Values)
      {
        if (link.Operation == Enumerators.AlternativeOperation.DEL || link.Operation == Enumerators.AlternativeOperation.SPL)
          continue;
        if (PipeDepth(link) <= depth)
          length += link.Length;
      }
      return length;
    }

    public double LengthOfPipesLessThanEqualDepthByFocusArea(double depth, string FocusArea)
    {
      double length = 0;
      foreach (AltLink link in altLinks.Values)
      {
        if (link.Operation == Enumerators.AlternativeOperation.DEL || link.Operation == Enumerators.AlternativeOperation.SPL || (link.FocusArea.ToUpper() != FocusArea.ToUpper()))
          continue;
        if (PipeDepth(link) <= depth)
          length += link.Length;
      }
      return length;
    }

    public double LengthOfPipesGreaterThanDepth(double depth)
    {
      double length = 0;
      foreach (AltLink link in altLinks.Values)
      {
        if (link.Operation == Enumerators.AlternativeOperation.DEL || link.Operation == Enumerators.AlternativeOperation.SPL)
          continue;
        if (PipeDepth(link) > depth)
          length += link.Length;
      }
      return length;
    }

    public double LengthOfPipesGreaterThanDepthByFocusArea(double depth, string FocusArea)
    {
      double length = 0;
      foreach (AltLink link in altLinks.Values)
      {
        if (link.Operation == Enumerators.AlternativeOperation.DEL || link.Operation == Enumerators.AlternativeOperation.SPL || (link.FocusArea.ToUpper() != FocusArea.ToUpper()))
          continue;
        if (PipeDepth(link) > depth)
          length += link.Length;
      }
      return length;
    }
  }
}
