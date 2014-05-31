// Project: Classes, File: ReportSummaryItem.cs
// Namespace: CostEstimator.Classes, Class: ReportSummaryItem
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 25, Size of file: 358 Bytes
// Creation date: 4/29/2008 6:19 PM
// Last modified: 4/30/2008 12:48 AM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// A summary reporting item
  /// </summary>
  public class ReportSummaryItem : ReportItem
  {
    #region Variables
    private string _Name;
    private decimal _Cost;
    private string _Group;
    private string _SummaryGroup;
    #endregion

    #region Properties
    /// <summary>
    /// Name
    /// </summary>
    /// <returns>String</returns>
    public string Name
    {
      get
      {
        return _Name;
      } // get

      set
      {
        _Name = value;
      } // set
    } // Name

    /// <summary>
    /// Cost
    /// </summary>
    /// <returns>Decimal</returns>
    public decimal Cost
    {
      get
      {
        return _Cost;
      } // get

      set
      {
        _Cost = value;
      } // set
    } // Cost

    /// <summary>
    /// Group
    /// </summary>
    /// <returns>Int</returns>
    public string Group
    {
      get
      {
        return _Group;
      } // get

      set
      {
        _Group = value;
      } // set
    } // Group

    /// <summary>
    /// Summary group
    /// </summary>
    /// <returns>String</returns>
    public string SummaryGroup
    {
      get
      {
        return _SummaryGroup;
      } // get

      set
      {
        _SummaryGroup = value;
      } // set
    } // SummaryGroup
    #endregion

    #region Methods
    /// <summary>
    /// Write XML
    /// </summary>
    /// <param name="xw">Xw</param>
    public void WriteXML(XmlWriter xw)
    {
      xw.WriteStartElement("name");
      xw.WriteValue(_Name);
      xw.WriteEndElement();

      xw.WriteStartElement("cost");
      xw.WriteValue(_Cost);
      xw.WriteEndElement();

      xw.WriteStartElement("group");
      xw.WriteValue(_Group);
      xw.WriteEndElement();

      xw.WriteStartElement("summaryGroup");
      xw.WriteValue(_SummaryGroup);
      xw.WriteEndElement();
    } // WriteXML()
    #endregion
  }
}
