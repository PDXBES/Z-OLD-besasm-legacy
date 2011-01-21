// Project: UnitTest, File: CostItemFactorTests.cs
// Namespace: CostEstimator.UnitTests, Class: CostItemFactorTests
// Path: C:\Development\CostEstimatorV2\UnitTest, Author: Arnel
// Code lines: 20, Size of file: 321 Bytes
// Creation date: 3/2/2008 12:20 AM
// Last modified: 3/26/2008 10:44 AM

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
	public class CostItemFactorTests
	{
		private CostItemFactor _item1, _item2, _item2child1, _item2child2, _item3, _item4, 
			_item5, _item6;

		/// <summary>
		/// Setup
		/// </summary>
		[SetUp]
		public void Setup()
		{
			_item1 = new CostItemFactor("Item 1", new CostItem("Item 1", 100, 20m, "ft"), new CostFactor("Item 1 Factor", 0.5, CostFactorType.Additive));
			_item2 = new CostItemFactor("Item 2", new CostItem("Item 2", 200, 20m, "ft"), new CostFactor("Item 2 Factor", 0.5, CostFactorType.Simple));
			_item2child1 = new CostItemFactor("Item 2 Child 1", new CostItem("Item 2 Child 1", 300, 20m, "ft"), new CostFactor("Item 2 Child 1 Factor", 0.5, CostFactorType.Additive));
			_item2child2 = new CostItemFactor("Item 2 Child 2", new CostItem("Item 2 Child 2", 400, 20m, "ft"), new CostFactor("Item 2 Child 2 Factor", 0.5, CostFactorType.Additive));

			_item2.AddCostItemFactor(_item2child1);
			_item2.AddCostItemFactor(_item2child2);

			_item3 = new CostItemFactor("Item 3", new CostItem("Item 3", 500, 20m, "ft"), new CostFactor("Item 3 Factor", 0.5, CostFactorType.Additive));
			_item3.AddFactor(new CostFactor("Item 3 Factor", 0.5, CostFactorType.Additive));

			_item4 = new CostItemFactor("Item 4", new CostItem("Item 4", 600, 20m, "ft"), new CostFactor("Item 4 Factor", 0.25, CostFactorType.IndirectAdditive));
			_item4.AddFactor(new CostFactor("Item 4 Factor", 0.1, CostFactorType.IndirectAdditive));

			_item5 = new CostItemFactor("Item 5");
			_item6 = new CostItemFactor("Item 6");
			_item5.AddCostItemFactor(_item6);
		} // Setup()

		/// <summary>
		/// Tear down
		/// </summary>
		[TearDown]
		public void TearDown()
		{
		} // TearDown()

		/// <summary>
		/// Cost item factor count _ is 2
		/// </summary>
		[Test]
		public void Item2CostItemFactorCount_Is2()
		{
			Assert.That(_item2.ChildCostItemFactorCount, Is.EqualTo(2));
		} // CostItemFactorCount_Is2()

		/// <summary>
		/// Item 1 cost _ is 3000
		/// </summary>
		[Test]
		public void Item1Cost_Is3000()
		{
			Assert.That(_item1.Cost, Is.EqualTo(3000m));
		} // Item1Cost_Is1000()

		/// <summary>
		/// Item 2 cost _ is 12500
		/// </summary>
		[Test]
		public void Item2Cost_Is12500()
		{
			Assert.That(_item2.Cost, Is.EqualTo(12500m));
		} // Item2Cost_Is12500()

		/// <summary>
		/// Add cost item factor _ already has cost item _ returns minus one
		/// </summary>
		[Test]
		public void AddCostItemFactor_SameCostItemFactor_ReturnsMinusOne()
		{
			int indexOfNewItem = _item1.AddCostItemFactor(_item1);
			Assert.That(indexOfNewItem, Is.EqualTo(-1));
		} // AddCostItemFactor_AlreadyHasCostItem_ReturnsMinusOne()

		/// <summary>
		/// Add cost item factor _ already a parent _ return minus one
		/// </summary>
		[Test]
		public void AddCostItemFactor_AlreadyAParent_ReturnMinusOne()
		{
			Assert.That(_item2.ChildCostItemFactorCount, Is.EqualTo(2));
			int indexOfNewItem = _item2child1.AddCostItemFactor(_item2);
			Assert.That(indexOfNewItem, Is.EqualTo(-1));
		} // AddCostItemFactor_AlreadyAParent_ReturnMinusOne()

		/// <summary>
		/// Is a parent of _ child asks parent _ exception
		/// </summary>
		[Test]
		public void IsAParentOf_ChildAsksParent_False()
		{
			bool isParent = false;
			isParent = _item6.IsAParentOf(_item5);
			Assert.That(isParent, Is.False);
		} // IsAParentOf_ChildAsksParent_Exception()

		/// <summary>
		/// Item 3 cost _ is 22500
		/// </summary>
		[Test]
		public void Item3Cost_Is22500()
		{
			Assert.That(_item3.Cost, Is.EqualTo(22500m));
		} // Item3Cost_Is2250()

		/// <summary>
		/// Indirect additive _ item 4 cost _ is 16200
		/// </summary>
		[Test]
		public void IndirectAdditive_Item4Cost_Is16200()
		{
			Assert.That(_item4.Cost, Is.EqualTo(16200m));
		} // IndirectAdditive_Item4Cost_Is16200()

		/// <summary>
		/// Indirect additive _ item 4 cost _ with 4 units _ is 64800
		/// </summary>
		[Test]
		public void IndirectAdditive_Item4Cost_With4Units_Is64800()
		{
			_item4.Quantity = 4;
			Assert.That(_item4.Cost, Is.EqualTo(64800m));
		} // IndirectAdditive_Item4Cost_With4Units_Is64800()
	}
}
