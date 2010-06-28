// Project: UnitTest, File: CostEngineTests.cs
// Namespace: CostEstimator.UnitTests, Class: CostEngineTests
// Path: C:\Development\CostEstimatorV2\UnitTest, Author: Arnel
// Code lines: 18, Size of file: 285 Bytes
// Creation date: 3/1/2008 2:33 PM
// Last modified: 3/1/2008 2:55 PM

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
	public class CostEngineTests
	{
		private CostEngine _costEngine;

		/// <summary>
		/// Set up
		/// </summary>
		[SetUp]
		public void SetUp()
		{
			_costEngine = new CostEngine();
		} // SetUp()

		[TearDown]
		public void TearDown()
		{
		} // class CostEngineTests

		/// <summary>
		/// Test add project
		/// </summary>
		[Test]
		public void TestAddProject()
		{
			Project project = _costEngine.AddProject();
			Assert.That(project, Is.Not.Null);
			Assert.That(_costEngine.ProjectCount, Is.EqualTo(1));
		} // TestAddProject()
	}
}
