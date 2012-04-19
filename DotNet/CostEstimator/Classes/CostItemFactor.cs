// Project: Classes, File: CostItemFactor.cs
// Namespace: CostEstimator.Classes, Class: CostItemFactor
// Path: C:\Development\CostEstimatorV2\Classes, Author: Arnel
// Code lines: 31, Size of file: 560 Bytes
// Creation date: 3/1/2008 3:37 PM
// Last modified: 11/5/2008 3:21 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// A CostItemFactor combines a CostItem and zero or more CostFactors, in
  /// addition to and any number of child CostItemFactors to form a line item cost.
  /// </summary>
  public class CostItemFactor
  {
    #region Variables
    private static int _CurrentId = 0;
    private double _Quantity = 1;
    private decimal _MinCost = decimal.MinValue / 2;
    private List<CostFactor> _CostFactors =  new List<CostFactor>();
    private List<CostItemFactor> _ChildCostItemFactors = new List<CostItemFactor>();
    private List<CostItemFactor> _ParentCostItemFactors = new List<CostItemFactor>();
    private object _Data = null;
    private ReportItemType _ReportItemType = ReportItemType.Unassigned;
    #endregion

    #region Constructors
    /// <summary>
    /// Create cost item factor
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="costItem">Cost item</param>
    /// <param name="costFactor">Cost factor</param>
    public CostItemFactor(string name, CostItem costItem, CostFactor costFactor)
    {
      ID = _CurrentId;
      _CurrentId++;

      Name = name;
      CostItem = costItem;

      if (costFactor != null)
        _CostFactors.Add(costFactor);
    } // CostItemFactor(name, costItem, costFactor)

    /// <summary>
    /// Create cost item factor
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="costItem">Cost item</param>
    /// <param name="costFactor">Cost factor</param>
    /// <param name="quantity">Quantity</param>
    public CostItemFactor(string name, CostItem costItem, CostFactor costFactor, double quantity)
    {
      ID = _CurrentId;
      _CurrentId++;

      Name = name;
      CostItem = costItem;
      _Quantity = quantity;

      if (costFactor != null)
        _CostFactors.Add(costFactor);
    } // CostItemFactor(name, costItem, costFactor)

    /// <summary>
    /// Create cost item factor
    /// </summary>
    /// <param name="name">Name</param>
    public CostItemFactor(string name)
    {
      ID = _CurrentId;
      _CurrentId++;

      Name = name;
    } // CostItemFactor(name)

    /// <summary>
    /// Create cost item factor
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="name">Name</param>
    /// <param name="comment">Comment</param>
    /// <param name="quantity">Quantity</param>
    /// <param name="minCost">Min cost</param>
    public CostItemFactor(int id, string name, string comment, double quantity, decimal minCost)
    {
      ID = id;
      Name = name;
      Comment = comment;
      _Quantity = quantity;
      _MinCost = minCost;
    } // CostItemFactor(id, name, comment)
    #endregion

    #region Properties
    /// <summary>
    /// ID
    /// </summary>
    /// <returns>Int</returns>
    public int ID { get; // get
      private set; } // ID

    /// <summary>
    /// Cost
    /// </summary>
    /// <returns>Decimal</returns>
    public decimal Cost
    {
      get
      {
        decimal cost = (decimal)((double)(ParentCost + ChildCost) * Factor * Quantity);
        return cost < MinCost ? MinCost : cost;
      } // get
    } // Cost

    /// <summary>
    /// Prefactored cost (does not prefactor child costs, however)
    /// </summary>
    /// <returns>Decimal</returns>
    public decimal PrefactoredCost
    {
      get
      {
        return (decimal)((double)(ParentCost + ChildCost) * Quantity);
      }
    } // PrefactoredCost

    /// <summary>
    /// Parent cost
    /// </summary>
    /// <returns>Decimal</returns>
    public decimal ParentCost
    {
      get
      {
        return CostItem == null ? 0m : CostItem.Cost;
      } // get
    } // ParentCost

    /// <summary>
    /// Parent cost item factor
    /// </summary>
    /// <returns>Cost item factor</returns>
    public CostItemFactor ParentCostItemFactor
    {
      get
      {
        return _ParentCostItemFactors.Count > 0 ? _ParentCostItemFactors[0] : null;
      }
    } // ParentCostItemFactor

    /// <summary>
    /// Sum of child CostItemFactors
    /// </summary>
    /// <returns>Decimal</returns>
    public decimal ChildCost
    {
      get
      {
        if (_ChildCostItemFactors.Count > 0)
        {
          decimal totalChildCost = 0;
          foreach (CostItemFactor costItemFactor in _ChildCostItemFactors)
          {
            totalChildCost += costItemFactor.Cost;
          } // foreach  (costItemFactor)

          return totalChildCost;
        } // if
        else
          return 0m;
      } // get
    } // ChildCost

    /// <summary>
    /// Number of child cost item factors
    /// </summary>
    /// <returns>Int</returns>
    public int ChildCostItemFactorCount
    {
      get
      {
        return _ChildCostItemFactors.Count;
      } // get
    } // ChildCostItemFactorCount

    /// <summary>
    /// Parent cost item factor count
    /// </summary>
    /// <returns>Int</returns>
    public int ParentCostItemFactorCount
    {
      get
      {
        return _ParentCostItemFactors.Count;
      } // get
    } // ParentCostItemFactorCount

    /// <summary>
    /// Cost factors count
    /// </summary>
    /// <returns>Int</returns>
    public int CostFactorsCount
    {
      get
      {
        return _CostFactors.Count;
      } // get
    } // CostFactorsCount

    /// <summary>
    /// Aggregated factor
    /// </summary>
    /// <returns>Double</returns>
    public double Factor
    {
      get
      {
        if (_CostFactors.Count == 0)
          return 1;
        else
        {
          double indirectAdditiveFactors = 1;
          double indirectSimpleFactors = 0;

          double currentFactor = 1;
          for (int i = 0; i < _CostFactors.Count; i++)
          {
            switch (_CostFactors[i].FactorType)
            {
              case CostFactorType.Additive:
                currentFactor *= (1 + _CostFactors[i].FactorValue);
                if (indirectAdditiveFactors != 1)
                {
                  currentFactor *= indirectAdditiveFactors;
                  indirectAdditiveFactors = 1;
                } // if
                if (indirectSimpleFactors != 0)
                {
                  currentFactor *= indirectSimpleFactors;
                  indirectSimpleFactors = 0;
                } // if
                break;
              case CostFactorType.IndirectAdditive:
                indirectAdditiveFactors += _CostFactors[i].FactorValue;
                break;
              case CostFactorType.Simple:
                currentFactor *= _CostFactors[i].FactorValue;
                if (indirectAdditiveFactors != 1)
                {
                  currentFactor *= indirectAdditiveFactors;
                  indirectAdditiveFactors = 1;
                } // if
                if (indirectSimpleFactors != 0)
                {
                  currentFactor *= indirectSimpleFactors;
                  indirectSimpleFactors = 0;
                } // if
                break;
              case CostFactorType.IndirectSimple:
                indirectSimpleFactors += _CostFactors[i].FactorValue;
                break;
            }
          }
          if (indirectAdditiveFactors != 1)
            currentFactor *= indirectAdditiveFactors;
          if (indirectSimpleFactors != 0)
            currentFactor *= indirectSimpleFactors;

          return currentFactor;
        }
      } // get
    } // Factor

    /// <summary>
    /// Quantity
    /// </summary>
    /// <returns>Double</returns>
    public double Quantity
    {
      get
      {
        return _Quantity;
      } // get
      set
      {
        _Quantity = value;
      } // set
    } // Quantity

    /// <summary>
    /// Has children
    /// </summary>
    /// <returns>Bool</returns>
    public bool HasChildren
    {
      get
      {
        return ChildCostItemFactorCount > 0;
      } // get
    } // HasChildren

    /// <summary>
    /// Name
    /// </summary>
    /// <returns>String</returns>
    public string Name { get; // get
      set; // set
    } // Name

    /// <summary>
    /// Cost item
    /// </summary>
    /// <returns>Cost item</returns>
    public CostItem CostItem { get;
      set; // set
    } // CostItem

    /// <summary>
    /// Min cost
    /// </summary>
    /// <returns>Decimal</returns>
    public decimal MinCost
    {
      get
      {
        return _MinCost;
      }
      set
      {
        _MinCost = value;
      }
    } // MinCost

    /// <summary>
    /// Comment
    /// </summary>
    /// <returns>String</returns>
    public string Comment { get; // get
      set; // set
    } // Comment

    /// <summary>
    /// Data
    /// </summary>
    /// <returns>Object</returns>
    public object Data
    {
      get
      {
        return _Data;
      } // get

      set
      {
        _Data = value;
      } // set
    } // Data

    /// <summary>
    /// Child cost item factors
    /// </summary>
    /// <returns>List</returns>
    public List<CostItemFactor> ChildCostItemFactors
    {
      get
      {
        return _ChildCostItemFactors;
      } // get
    } // ChildCostItemFactors

    /// <summary>
    /// Cost factors
    /// </summary>
    /// <returns>List</returns>
    public List<CostFactor> CostFactors
    {
      get
      {
        return _CostFactors;
      } // get
    } // CostFactors

    /// <summary>
    /// Report item type
    /// </summary>
    /// <returns>Report item type</returns>
    public ReportItemType ReportItemType
    {
      get
      {
        return _ReportItemType;
      }

      set
      {
        _ReportItemType = value;
      }
    } // ReportItemType
    #endregion

    #region Methods
    /// <summary>
    /// Reset ID generator
    /// </summary>
    public static void ResetIDGenerator()
    {
      _CurrentId = 0;
    } // ResetIDGenerator()

    /// <summary>
    /// Revert ID
    /// </summary>
    /// <returns>Int</returns>
    public static int RevertID()
    {
      _CurrentId--;
      if (_CurrentId == 0)
        _CurrentId = 0;
      return _CurrentId;
    } // RevertID()

    /// <summary>
    /// Set current ID
    /// </summary>
    /// <param name="currentID">Current ID</param>
    public static void SetCurrentID(int currentID)
    {
      _CurrentId = currentID;
    } // SetCurrentID(currentID)

    /// <summary>
    /// Assign ID
    /// </summary>
    public void AssignID()
    {
      ID = _CurrentId;
      _CurrentId++;
    } // AssignID()

    /// <summary>
    /// Add cost item factor
    /// </summary>
    /// <param name="costItemFactor">Cost item factor</param>
    /// <returns>Int</returns>
    public int AddCostItemFactor(CostItemFactor costItemFactor)
    {
      if (this == costItemFactor || costItemFactor.IsAParentOf(this))
        return -1;

      _ChildCostItemFactors.Add(costItemFactor);
      costItemFactor._ParentCostItemFactors.Add(this);
      return _ChildCostItemFactors.Count - 1;
    } // AddCostItemFactor(costItemFactor)

    /// <summary>
    /// Get cost item factor
    /// </summary>
    /// <param name="index">Index</param>
    /// <returns>Cost item factor</returns>
    public CostItemFactor ChildCostItemFactor(int index)
    {
      if (index > _ChildCostItemFactors.Count - 1)
        return null;
      else
        return _ChildCostItemFactors[index];
    } // GetCostItemFactor(index)

    /// <summary>
    /// Child cost item factor
    /// </summary>
    /// <param name="index">Index</param>
    /// <returns>Cost item factor</returns>
    public CostItemFactor ChildCostItemFactor(string index)
    {
      return _ChildCostItemFactors.Find(item => item.Name == index);
    } // ChildCostItemFactor(index)

    /// <summary>
    /// Add factor
    /// </summary>
    /// <param name="factor">Factor</param>
    public void AddFactor(CostFactor factor)
    {
      _CostFactors.Add(factor);
    }// class CostItemFactor

    /// <summary>
    /// Insert factor
    /// </summary>
    /// <param name="factor">Factor</param>
    /// <param name="atIndex">At index</param>
    public void InsertFactor(int atIndex, CostFactor factor)
    {
      _CostFactors.Insert(atIndex, factor);
    } // InsertFactor(factor, atIndex)

    /// <summary>
    /// Delete factor
    /// </summary>
    /// <param name="factor">Factor</param>
    public void DeleteFactor(CostFactor factor)
    {
      _CostFactors.Remove(factor);
    } // DeleteFactor(factor)

    /// <summary>
    /// Delete factor
    /// </summary>
    /// <param name="costFactorID">Cost factor ID</param>
    public void DeleteFactor(int costFactorID)
    {
      CostFactor factorToDelete = null;
      for (int i = 0; i < _CostFactors.Count; i++)
      {
        if (_CostFactors[i].ID == costFactorID)
        {
          factorToDelete = _CostFactors[i];
          break;
        }
      } // for

      if (factorToDelete != null)
        _CostFactors.Remove(factorToDelete);
    } // DeleteFactor(costFactorID)

    /// <summary>
    /// Index of factor
    /// </summary>
    /// <param name="factor">Factor</param>
    /// <returns>Int</returns>
    public int IndexOfFactor(CostFactor factor)
    {
      return _CostFactors.IndexOf(factor);
    } // IndexOfFactor(factor)

    /// <summary>
    /// Index of cost item factor
    /// </summary>
    /// <param name="CostItemFactor">Cost item factor</param>
    /// <returns>Int</returns>
    public int IndexOfCostItemFactor(CostItemFactor costItemFactor)
    {
      return _ChildCostItemFactors.IndexOf(costItemFactor);
    } // IndexOfCostItemFactor(CostItemFactor)

    /// <summary>
    /// Child cost item factor by ID
    /// </summary>
    /// <param name="index">Index</param>
    /// <returns>Cost item factor</returns>
    public CostItemFactor ChildCostItemFactorByID(int index)
    {
      foreach (CostItemFactor costItemFactor in _ChildCostItemFactors)
      {
        if (costItemFactor.ID == index)
          return costItemFactor;
      } // foreach  (costItemFactor)
      return null;
    } // ChildCostItemFactorByID(index)

    /// <summary>
    /// Clear factors
    /// </summary>
    public void ClearFactors()
    {
      _CostFactors.Clear();
    } // ClearFactors()

    /// <summary>
    /// Add as parent
    /// </summary>
    /// <param name="costItemFactor">Cost item factor</param>
    public void AddAsParent(CostItemFactor costItemFactor)
    {
      _ParentCostItemFactors.Add(costItemFactor);
    } // AddAsParent(costItemFactor)

    /// <summary>
    /// Is a parent of
    /// </summary>
    /// <param name="costItemFactor">Cost item factor</param>
    /// <returns>Bool</returns>
    public bool IsAParentOf(CostItemFactor costItemFactor)
    {
      if (costItemFactor == null)
        return false;

      Stack<CostItemFactor> parents = new Stack<CostItemFactor>();
      foreach (CostItemFactor parent in costItemFactor._ParentCostItemFactors)
        parents.Push(parent);

      bool isParent;
      while (parents.Count > 0)
      {
        CostItemFactor parent = parents.Pop();
        if (this != parent)
          isParent = IsAParentOf(parent);
        else
          isParent = true;

        if (isParent)
          return true;
      }
      return false;
    } // IsAParentOf(costItemFactor)

    /// <summary>
    /// Cost item factors as strings
    /// </summary>
    /// <returns>List</returns>
    public List<string> CostItemFactorsAsStrings()
    {
      List<string> returnList = new List<string>();

      returnList.Add(string.Format("[{0}] Total Cost-{1:C}, Quantity-{2:F2}, ParentCost-{3:C}, ChildCost-{4:C}, Factor-{5:F5}",
      Name,
      Cost,
      Quantity,
      ParentCost,
      ChildCost,
      Factor));
      foreach (CostItemFactor costItemFactor in _ChildCostItemFactors)
      {
        if (costItemFactor.HasChildren)
          returnList.AddRange(costItemFactor.CostItemFactorsAsStrings());
        else
          returnList.Add(string.Format("[{0}] Total Cost-{1:C}, Quantity-{2:F2}, ParentCost-{3:C}, ChildCost-{4:C}, Factor-{5:F5}",
          costItemFactor.Name,
          costItemFactor.Cost,
          costItemFactor.Quantity,
          costItemFactor.ParentCost,
          costItemFactor.ChildCost,
          costItemFactor.Factor));
      } // foreach  (costItemFactor)

      return returnList;
    } // CostItemFactorsAsStrings()

    /// <summary>
    /// Cost factor
    /// </summary>
    /// <param name="index">Index</param>
    /// <returns>Cost factor</returns>
    public CostFactor CostFactor(int index)
    {
      return _CostFactors[index];
    } // CostFactor(index)

    /// <summary>
    /// Cost factor
    /// </summary>
    /// <param name="index">Index</param>
    /// <returns>Cost factor</returns>
    public CostFactor CostFactor(string index)
    {
      foreach (CostFactor costFactor in _CostFactors)
      {
        if (costFactor.Name == index)
          return costFactor;
      } // foreach  (costFactor)

      return null;
    } // CostFactor(index)

    /// <summary>
    /// Cost factor index
    /// </summary>
    /// <param name="costFactor">Cost factor</param>
    /// <returns>Int</returns>
    public int CostFactorIndex(CostFactor costFactor)
    {
      for (int i = 0; i < _CostFactors.Count; i++)
      {
        if (_CostFactors[i] == costFactor)
          return i;
      }
      return -1;
    } // CostFactorIndex(costFactor)

    /// <summary>
    /// Cost factor value
    /// </summary>
    /// <param name="index">Index</param>
    public decimal CostFactorValue(int index)
    {
      decimal prefactoredCost = PrefactoredCost;
      decimal previousCost = 0;
      decimal currentCost = prefactoredCost;
      double currentFactor = 1;
      double currentIndirectAdditiveFactor = 1;
      double currentIndirectSimpleFactor = 0;
      for (int i = 0; i <= index; i++)
      {
        switch (_CostFactors[i].FactorType)
        {
          case CostFactorType.Additive:
            currentFactor *= (1 + _CostFactors[i].FactorValue);
            if (currentIndirectAdditiveFactor != 1)
            {
              currentFactor *= currentIndirectAdditiveFactor;
              currentIndirectAdditiveFactor = 1;
            } // if
            if (currentIndirectSimpleFactor != 0)
            {
              currentFactor *= currentIndirectSimpleFactor;
              currentIndirectSimpleFactor = 0;
            } // if
            previousCost = currentCost;
            currentCost = (decimal)((double)prefactoredCost * currentFactor);
            break;
          case CostFactorType.IndirectAdditive:
            currentIndirectAdditiveFactor += _CostFactors[i].FactorValue;
            break;
          case CostFactorType.Simple:
            currentFactor *= _CostFactors[i].FactorValue;
            if (currentIndirectAdditiveFactor != 1)
            {
              currentFactor *= currentIndirectAdditiveFactor;
              currentIndirectAdditiveFactor = 1;
            } // if
            if (currentIndirectSimpleFactor != 0)
            {
              currentFactor *= currentIndirectSimpleFactor;
              currentIndirectSimpleFactor = 0;
            } // if
            previousCost = currentCost;
            currentCost = (decimal)((double)prefactoredCost * currentFactor);
            break;
          case CostFactorType.IndirectSimple:
            currentIndirectSimpleFactor += _CostFactors[i].FactorValue;
            break;
        }
      }

      if (_CostFactors[index].FactorType == CostFactorType.IndirectAdditive ||
      _CostFactors[index].FactorType == CostFactorType.IndirectSimple)
        return currentCost = (decimal)((double)currentCost * _CostFactors[index].FactorValue);
      else
        return currentCost - previousCost;
    } // CostFactorValue(index)

    /// <summary>
    /// Delete cost item factor
    /// </summary>
    /// <param name="index">Index</param>
    public void DeleteCostItemFactor(int index)
    {
      if (ChildCostItemFactorByID(index)._ParentCostItemFactors.IndexOf(this) > -1)
        ChildCostItemFactorByID(index)._ParentCostItemFactors.Remove(this);
      _ChildCostItemFactors.Remove(ChildCostItemFactorByID(index));
    } // DeleteCostItemFactor(index)

    /// <summary>
    /// Write XML
    /// </summary>
    /// <param name="xw">Xw</param>
    public void WriteXML(XmlWriter xw)
    {
      xw.WriteStartElement("CostItemFactor");

      xw.WriteStartAttribute("reportItemType");
      xw.WriteValue(_ReportItemType.ToString());
      xw.WriteEndAttribute();

      xw.WriteStartAttribute("id");
      xw.WriteValue(ID);
      xw.WriteEndAttribute();

      xw.WriteStartElement("name");
      xw.WriteValue(Name);
      xw.WriteEndElement();

      xw.WriteStartElement("comment");
      if (Comment != null)
        xw.WriteValue(Comment);
      xw.WriteEndElement();

      xw.WriteStartElement("quantity");
      xw.WriteValue(_Quantity);
      xw.WriteEndElement();

      xw.WriteStartElement("mincost");
      xw.WriteValue(_MinCost);
      xw.WriteEndElement();

      xw.WriteStartElement("CostItemId");
      if (CostItem != null)
        xw.WriteValue(CostItem.ID);
      xw.WriteEndElement();

      xw.WriteStartElement("data");
      switch (_ReportItemType)
      {
        case ReportItemType.Pipe:
          (_Data as ReportPipeItem).WriteXML(xw);
          break;
        case ReportItemType.InflowControl:
          (_Data as ReportInflowControlItem).WriteXML(xw);
          break;
        case ReportItemType.Generic:
          (_Data as ReportGenericItem).WriteXML(xw);
          break;
        case ReportItemType.Summary:
          (_Data as ReportSummaryItem).WriteXML(xw);
          break;
      }
      xw.WriteEndElement();

      xw.WriteStartElement("CostFactors");
      foreach (CostFactor costFactor in _CostFactors)
      {
        xw.WriteStartElement("id");
        xw.WriteValue(costFactor.ID);
        xw.WriteEndElement();
      } // foreach  (costFactor)
      xw.WriteEndElement();

      xw.WriteStartElement("ChildCostItemFactors");
      foreach (CostItemFactor costItemFactor in _ChildCostItemFactors)
      {
        xw.WriteStartElement("id");
        xw.WriteValue(costItemFactor.ID);
        xw.WriteEndElement();
      } // foreach  (costFactor)
      xw.WriteEndElement();

      xw.WriteEndElement();
    } // WriteXML()

    /// <summary>
    /// To string
    /// </summary>
    /// <returns>String</returns>
    public override string ToString()
    {
      return string.Format("{1}", ID, Name);
    } // ToString()
    #endregion
  }
}
