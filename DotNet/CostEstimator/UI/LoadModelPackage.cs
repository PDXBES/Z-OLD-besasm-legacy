// Project: UI, File: LoadModelPackage.cs
// Namespace: SystemsAnalysis.Analysis.CostEstimator.UI, Class: LoadModelPackage
// Path: C:\Development\DotNet\CostEstimator\UI, Author: Arnel
// Code lines: 26, Size of file: 397 Bytes
// Creation date: 7/1/2008 2:20 PM
// Last modified: 7/1/2008 2:22 PM

using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsAnalysis.Analysis.CostEstimator.UI
{
  public class LoadModelPackage
  {
    private string _ModelPath;
    private List<int> _LinksToEstimate;

    /// <summary>
    /// Model path
    /// </summary>
    /// <returns>String</returns>
    public string ModelPath
    {
      get
      {
        return _ModelPath;
      } // get

      set
      {
        _ModelPath = value;
      } // set
    } // ModelPath

    /// <summary>
    /// Links to estimate
    /// </summary>
    /// <returns>List</returns>
    public List<int> LinksToEstimate
    {
      get
      {
        return _LinksToEstimate;
      } // get

      set
      {
        _LinksToEstimate = value;
      } // set
    } // LinksToEstimate
  }
}
