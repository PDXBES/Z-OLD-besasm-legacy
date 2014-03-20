// Project: UnitTest, File: ProjectTests.cs
// Namespace: CostEstimator.UnitTests, Class: ProjectTests
// Path: C:\Development\CostEstimatorV2\UnitTest, Author: Arnel
// Code lines: 19, Size of file: 312 Bytes
// Creation date: 3/6/2008 2:10 PM
// Last modified: 3/6/2008 4:10 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SystemsAnalysis.Analysis.CostEstimator.Classes;

#endregion

namespace CostEstimator.UnitTests
{
  //[TestFixture]
  public class ProjectTests
  {
    private Project _project;

    /// <summary>
    /// Set up
    /// </summary>
    [SetUp]
    public void SetUp()
    {
      _project = new Project();
    } // SetUp()

    /// <summary>
    /// Tear down
    /// </summary>
    [TearDown]
    public void TearDown()
    {
    } // TearDown()
  }
}
