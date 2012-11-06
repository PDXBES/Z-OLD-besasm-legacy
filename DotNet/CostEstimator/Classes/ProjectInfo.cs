// Project: Classes, File: ProjectInfo.cs
// Namespace: CostEstimator.Classes, Class: ProjectInfo
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 35, Size of file: 653 Bytes
// Creation date: 4/28/2008 7:12 AM
// Last modified: 8/21/2009 12:31 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  /// <summary>
  /// Cost Estimate Project metadata
  /// </summary>
  public class ProjectInfo
  {
    #region Variables
    private string _ProjectTitle = "";
    private string _ProjectNumber = "";
    private string _ProjectDescription = "";
    private string _ProjectManager = "";
    private string _CostEstimator = "";
    private DateTime _PreparedDate = DateTime.Now;
    private string _Source = "";
    private ENR _ENR = new ENR(0, 1, 2000);
    private ENR _BaseENR = new ENR(0, 1, 2000);
    private bool _isDirty = false;
    #endregion

    #region Properties
    /// <summary>
    /// Project title
    /// </summary>
    /// <returns>String</returns>
    public string ProjectTitle
    {
      get
      {
        return _ProjectTitle;
      } // get

      set
      {
        _ProjectTitle = value;
        _isDirty = true;
      } // set
    } // ProjectTitle

    public string ProjectNumber
    {
      get
      {
        return _ProjectNumber;
      } // get

      set
      {
        _ProjectNumber = value;
        _isDirty = true;
      } // set
    } // ProjectNumber

    /// <summary>
    /// Project description
    /// </summary>
    /// <returns>String</returns>
    public string ProjectDescription
    {
      get
      {
        return _ProjectDescription;
      } // get

      set
      {
        _ProjectDescription = value;
        _isDirty = true;
      } // set
    } // ProjectDescription

    /// <summary>
    /// Project manager
    /// </summary>
    /// <returns>String</returns>
    public string ProjectManager
    {
      get
      {
        return _ProjectManager;
      } // get

      set
      {
        _ProjectManager = value;
        _isDirty = true;
      } // set
    } // ProjectManager

    /// <summary>
    /// Cost estimator
    /// </summary>
    /// <returns>String</returns>
    public string CostEstimator
    {
      get
      {
        return _CostEstimator;
      } // get

      set
      {
        _CostEstimator = value;
        _isDirty = true;
      } // set
    } // CostEstimator

    public DateTime PreparedDate
    {
      get
      {
        return _PreparedDate;
      }

      set
      {
        _PreparedDate = value;
        _isDirty = true;
      } // set
    }

    /// <summary>
    /// Source
    /// </summary>
    /// <returns>String</returns>
    public string Source
    {
      get
      {
        return _Source;
      } // get

      set
      {
        _Source = value;
        _isDirty = true;
      } // set
    } // Source

    /// <summary>
    /// ENT r
    /// </summary>
    /// <returns>Int</returns>
    public ENR ENR
    {
      get
      {
        return _ENR;
      } // get

      set
      {
        _ENR = value;
        _isDirty = true;
      } // set
    } // ENTR

    /// <summary>
    /// Base ENR
    /// </summary>
    /// <returns>Int</returns>
    public ENR BaseENR
    {
      get
      {
        return _BaseENR;
      } // get

      set
      {
        _BaseENR = value;
        _isDirty = true;
      } // set
    } // BaseENR

    /// <summary>
    /// Is dirty
    /// </summary>
    /// <returns>Bool</returns>
    public bool IsDirty
    {
      get
      {
        return _isDirty;
      } // get
    } // IsDirty
    #endregion

    /// <summary>
    /// Create project info
    /// </summary>
    public ProjectInfo()
    {
      _isDirty = false;
    } // ProjectInfo()

    #region Methods
    /// <summary>
    /// Write XML
    /// </summary>
    /// <param name="xw">Xw</param>
    public void WriteXML(XmlWriter xw)
    {
      xw.WriteStartElement("ProjectTitle");
      xw.WriteValue(_ProjectTitle);
      xw.WriteEndElement();

      xw.WriteStartElement("ProjectNumber");
      xw.WriteValue(_ProjectNumber);
      xw.WriteEndElement();

      xw.WriteStartElement("ProjectDescription");
      xw.WriteValue(_ProjectDescription);
      xw.WriteEndElement();

      xw.WriteStartElement("ProjectManager");
      xw.WriteValue(_ProjectManager);
      xw.WriteEndElement();

      xw.WriteStartElement("CostEstimator");
      xw.WriteValue(_CostEstimator);
      xw.WriteEndElement();

      xw.WriteStartElement("PreparedDate");
      xw.WriteValue(_PreparedDate);
      xw.WriteEndElement();

      xw.WriteStartElement("Source");
      xw.WriteValue(_Source);
      xw.WriteEndElement();

      xw.WriteStartElement("ENR");
      xw.WriteStartAttribute("month");
      xw.WriteValue(_ENR.Month);
      xw.WriteEndAttribute();
      xw.WriteStartAttribute("year");
      xw.WriteValue(_ENR.Year);
      xw.WriteEndAttribute();

      xw.WriteValue(_ENR.Value);
      xw.WriteEndElement();

      xw.WriteStartElement("BaseENR");

      xw.WriteStartAttribute("month");
      xw.WriteValue(_BaseENR.Month);
      xw.WriteEndAttribute();
      xw.WriteStartAttribute("year");
      xw.WriteValue(_BaseENR.Year);
      xw.WriteEndAttribute();

      xw.WriteValue(_BaseENR.Value);
      xw.WriteEndElement();
    } // WriteXML()

    /// <summary>
    /// Clean
    /// </summary>
    public void Clean()
    {
      _isDirty = false;
    } // Clean()
    #endregion
  }
}
