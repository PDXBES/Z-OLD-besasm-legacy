// Project: UnitTest, File: CostItemTests.cs
// Namespace: CostEstimator.UnitTests, Class: CostItemTests
// Path: C:\Development\CostEstimatorV2\UnitTest, Author: Arnel
// Code lines: 21, Size of file: 317 Bytes
// Creation date: 3/1/2008 4:15 PM
// Last modified: 3/1/2008 11:44 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SystemsAnalysis.Analysis.CostEstimator.Classes;

#endregion

namespace CostEstimator.UnitTests
{
  [TestFixture]
  public class CostItemTests
  {
    private CostItem _costItem;

    /// <summary>
    /// Set up
    /// </summary>
    [SetUp]
    public void SetUp()
    {
      _costItem = new CostItem("Test cost item", 20, 500m, "ft");
    } // SetUp()

    /// <summary>
    /// Tear down
    /// </summary>
    [TearDown]
    public void TearDown()
    {
    } // TearDown()

    /// <summary>
    /// Cost _ name is test cost item
    /// </summary>
    [Test]
    public void Name_IsTestCostItem()
    {
      Assert.That(_costItem.Name, Is.EqualTo("Test cost item"));
    } // Cost_NameIsTestCostItem()

    /// <summary>
    /// Cost _ quantity is 20
    /// </summary>
    [Test]
    public void Quantity_Is20()
    {
      Assert.That(_costItem.Quantity, Is.EqualTo(20));
    } // Cost_QuantityIs20()

    /// <summary>
    /// Unit cost _ is 500
    /// </summary>
    [Test]
    public void UnitCost_Is500()
    {
      Assert.That(_costItem.UnitCost, Is.EqualTo(500m));
    } // UnitCost_Is20()

    /// <summary>
    /// Unit name _ is ft
    /// </summary>
    public void UnitName_IsFt()
    {
      Assert.That(_costItem.UnitName, Is.EqualTo("ft"));
    } // UnitName_IsFt()

    /// <summary>
    /// Cost _ extension is 1000
    /// </summary>
    [Test]
    public void Cost_ExtensionIs10000()
    {
      Assert.That(_costItem.Cost, Is.EqualTo(10000m));
    } // Cost_ExtensionIs1000()

  }
}
