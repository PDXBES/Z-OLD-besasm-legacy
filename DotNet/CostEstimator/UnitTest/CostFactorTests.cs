// Project: UnitTest, File: CostFactorTests.cs
// Namespace: CostEstimator.UnitTests, Class: CostFactorTests
// Path: C:\Development\CostEstimatorV2\UnitTest, Author: Arnel
// Code lines: 20, Size of file: 317 Bytes
// Creation date: 3/1/2008 11:47 PM
// Last modified: 3/2/2008 12:27 AM

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
	public class CostFactorTests
	{
		private CostFactor _costFactor;

		/// <summary>
		/// Set up
		/// </summary>
		[SetUp]
		public void SetUp()
		{
			_costFactor = new CostFactor("Test factor", 0.5);
		} // SetUp()

		/// <summary>
		/// Tear down
		/// </summary>
		[TearDown]
		public void TearDown()
		{
		} // TearDown()

		/// <summary>
		/// Name _ is _ test factor
		/// </summary>
		[Test]
		public void Name_Is_TestFactor()
		{
			Assert.That(_costFactor.Name, Is.EqualTo("Test factor"));
		} // Name_Is_TestFactor()

		/// <summary>
		/// Factor value _ is _0_5
		/// </summary>
		[Test]
		public void FactorValue_Is_0_5()
		{
			Assert.That(_costFactor.FactorValue, Is.EqualTo(0.5));
		} // FactorValue_Is_0_5()

		/// <summary>
		/// Factor type _ is _ additive
		/// </summary>
		[Test]
		public void FactorType_Is_Additive()
		{
			Assert.That(_costFactor.FactorType, Is.EqualTo(CostFactorType.Additive));
		} // FactorType_Is_Additive()
	}
}
