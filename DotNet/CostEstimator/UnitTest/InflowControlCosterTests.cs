// Project: UnitTest, File: InflowControlCosterTests.cs
// Namespace: CostEstimator.UnitTests, Class: InflowControlCosterTests
// Path: C:\Development\CostEstimatorV2\UnitTest, Author: Arnel
// Code lines: 19, Size of file: 324 Bytes
// Creation date: 3/6/2008 12:54 PM
// Last modified: 3/6/2008 1:24 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using SystemsAnalysis.Analysis.CostEstimator.Classes;
#endregion

namespace CostEstimator.UnitTests
{
	[TestFixture]
	public class InflowControlCosterTests
	{
		private InflowControlCoster _icCoster;

		/// <summary>
		/// Set up
		/// </summary>
		[SetUp]
		public void SetUp()
		{
			_icCoster = new InflowControlCoster();
		} // SetUp()

		/// <summary>
		/// Tear down
		/// </summary>
		[TearDown]
		public void TearDown()
		{
		} // TearDown()

		/// <summary>
		/// Cost _ green street planter 100 0sqft _ is 1483
		/// </summary>
		[Test]
		public void Cost_GreenStreetPlanter1000sqft_Is1483()
		{
			_icCoster.DrainageAreaSqFt = 1000;
			_icCoster.InflowControl = InflowControl.GreenStreetPlanter;
			Assert.That(_icCoster.Cost, Is.EqualTo(1483.00m).Within(5.00m));
		} // Cost_GreenStreetPlanter1000sqft_Is1483()

		/// <summary>
		/// Cost _ infiltration planter 100 0sqft _ is 598
		/// </summary>
		[Test]
		public void Cost_InfiltrationPlanter1000sqft_Is598()
		{
			_icCoster.DrainageAreaSqFt = 1000;
			_icCoster.InflowControl = InflowControl.InfiltrationPlanter;
			Assert.That(_icCoster.Cost, Is.EqualTo(598.00m).Within(5.00m));
		} // Cost_InfiltrationPlanter1000sqft_Is598()

		/// <summary>
		/// Cost _ vegetated infiltration basin 100 0sqft _ is 2778
		/// </summary>
		[Test]
		public void Cost_VegetatedInfiltrationBasin1000sqft_Is2778()
		{
			_icCoster.DrainageAreaSqFt = 1000;
			_icCoster.InflowControl = InflowControl.VegetatedInfiltrationBasin;
			Assert.That(_icCoster.Cost, Is.EqualTo(2778.00m).Within(5.00m));
		} // Cost_VegetatedInfiltrationBasin1000sqft_Is2778()

		/// <summary>
		/// Cost _ downspout disconnection 100 0sqft _ is 67
		/// </summary>
		[Test]
		public void Cost_DownspoutDisconnection1000sqft_Is67()
		{
			_icCoster.DrainageAreaSqFt = 1000;
			_icCoster.InflowControl = InflowControl.DownspoutDisconnection;
			Assert.That(_icCoster.Cost, Is.EqualTo(67.00m).Within(5.00m));
		} // Cost_DownspoutDisconnection1000sqft_Is67()

		/// <summary>
		/// Cost _ ecoroof 100 0sqft _ is 6566
		/// </summary>
		[Test]
		public void Cost_Ecoroof1000sqft_Is6566()
		{
			_icCoster.DrainageAreaSqFt = 1000;
			_icCoster.InflowControl = InflowControl.Ecoroof;
			Assert.That(_icCoster.Cost, Is.EqualTo(6566.00m).Within(5.00m));
		} // Cost_Ecoroof1000sqft_Is6566()

		/// <summary>
		/// Cost _ curb extension 5 0cuft _ is 3700
		/// </summary>
		[Test]
		public void Cost_CurbExtension50cuft_Is3700()
		{
			_icCoster.FacilitySizeCuFt = 50;
			_icCoster.InflowControl = InflowControl.CurbExtension;
			Assert.That(_icCoster.Cost, Is.EqualTo(3700.00m).Within(5.00m));
		} // Cost_CurbExtension50cuft_Is3700()

		/// <summary>
		/// Cost _ curb extension 50 0cuft _ is 24500
		/// </summary>
		[Test]
		public void Cost_CurbExtension500cuft_Is24500()
		{
			_icCoster.FacilitySizeCuFt = 500;
			_icCoster.InflowControl = InflowControl.CurbExtension;
			Assert.That(_icCoster.Cost, Is.EqualTo(24500.00m).Within(5.00m));
		} // Cost_CurbExtension500cuft_Is24500()

		[Test]
		public void Cost_StormwaterPlanter30cuft_Is5600()
		{
			_icCoster.FacilitySizeCuFt = 30;
			_icCoster.InflowControl = InflowControl.StormwaterPlanter;
			Assert.That(_icCoster.Cost, Is.EqualTo(5600.00m).Within(5.00m));
		} // Cost_CurbExtension50cuft_Is3700()

		/// <summary>
		/// Cost _ curb extension 30 0cuft _ is 36300
		/// </summary>
		[Test]
		public void Cost_CurbExtension300cuft_Is36300()
		{
			_icCoster.FacilitySizeCuFt = 300;
			_icCoster.InflowControl = InflowControl.StormwaterPlanter;
			Assert.That(_icCoster.Cost, Is.EqualTo(36300.00m).Within(5.00m));
		} // Cost_CurbExtension300cuft_Is36300()

		/// <summary>
		/// Cost _ flow restrictor _ is 22500
		/// </summary>
		[Test]
		public void Cost_FlowRestrictor_Is22500()
		{
			_icCoster.DrainageAreaSqFt = 50;
			_icCoster.FacilitySizeCuFt = 50;
			_icCoster.InflowControl = InflowControl.FlowRestrictor;
			Assert.That(_icCoster.Cost, Is.EqualTo(22500.00m).Within(5.00m));
		} // Cost_FlowRestrictor_Is22500()

		/// <summary>
		/// Base ENR_ is 6580
		/// </summary>
		[Test]
		public void BaseENR_Is6580()
		{
			Assert.That(_icCoster.BaseENR, Is.EqualTo(6580));
		} // BaseENR_Is6580()
	}
}
