// Project: Classes, File: ReportItem.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.Classes, Class: 
// Path: C:\Development\DotNet\CostEstimator\Classes, Author: Arnel
// Code lines: 17, Size of file: 326 Bytes
// Creation date: 6/6/2008 5:20 PM
// Last modified: 7/14/2008 10:01 AM

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  public enum ReportItemType
  {
    Unassigned,
    Generic,
    Summary,
    Pipe,
    InflowControl
  } // enum ReportItemType

  /// <summary>
  /// Common functions for all report items
  /// </summary>
  public interface ReportItem
  {
    void WriteXML(XmlWriter xw);
  }
}
