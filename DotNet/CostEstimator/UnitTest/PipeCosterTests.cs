// Project: UnitTest, File: PipeCosterTests.cs
// Namespace: CostEstimator.UnitTests, Class: PipeCosterTests
// Path: C:\Development\CostEstimatorV2\UnitTest, Author: Arnel
// Code lines: 19, Size of file: 315 Bytes
// Creation date: 3/4/2008 1:48 PM
// Last modified: 3/6/2008 11:27 AM

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
	public class PipeCosterTests
	{
		private PipeCoster _pipeCoster;

		/// <summary>
		/// Set up
		/// </summary>
		[SetUp]
		public void SetUp()
		{
			_pipeCoster = new PipeCoster();
		} // SetUp()

		/// <summary>
		/// Tear down
		/// </summary>
		[TearDown]
		public void TearDown()
		{
		} // TearDown()

		/// <summary>
		/// Outside diameter from inside diameter _5Concrete_ is 8
		/// </summary>
		[Test]
		public void OutsideDiameterFromInsideDiameter_5Concrete_Is8()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.OutsideDiameterFromInsideDiameter(PipeMaterial.Concrete, 5, out outsideTable), Is.EqualTo(8));
			Assert.That(outsideTable, Is.True);
		} // OutsideDiameterFromInsideDiameter_5CSP_Is8()

		/// <summary>
		/// Outside diameter from inside diameter _6 concrete _ is 8
		/// </summary>
		[Test]
		public void OutsideDiameterFromInsideDiameter_6Concrete_Is8()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.OutsideDiameterFromInsideDiameter(PipeMaterial.Concrete, 6, out outsideTable), Is.EqualTo(8));
			Assert.That(outsideTable, Is.False);
		} // OutsideDiameterFromInsideDiameter_6Concrete_Is8()

		/// <summary>
		/// Outside diameter from inside diameter _7 concrete _ is 10
		/// </summary>
		[Test]
		public void OutsideDiameterFromInsideDiameter_7Concrete_Is10()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.OutsideDiameterFromInsideDiameter(PipeMaterial.Concrete, 7, out outsideTable), Is.EqualTo(10));
			Assert.That(outsideTable, Is.False);
		} // OutsideDiameterFromInsideDiameter_7Concrete_Is10()

		/// <summary>
		/// Outside diameter from inside diameter _103 concrete _ is 122_5
		/// </summary>
		[Test]
		public void OutsideDiameterFromInsideDiameter_103Concrete_Is122_5()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.OutsideDiameterFromInsideDiameter(PipeMaterial.Concrete, 103, out outsideTable), Is.EqualTo(122.5));
			Assert.That(outsideTable, Is.True);
		} // OutsideDiameterFromInsideDiameter_103Concrete_Is122_5()

		/// <summary>
		/// Outside diameter from inside diameter _7PVCHDPE_ is 8_4
		/// </summary>
		[Test]
		public void OutsideDiameterFromInsideDiameter_7PVCHDPE_Is8_4()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.OutsideDiameterFromInsideDiameter(PipeMaterial.PVCHDPE, 7, out outsideTable), Is.EqualTo(8.4));
			Assert.That(outsideTable, Is.False);
		} // OutsideDiameterFromInsideDiameter_7PVCHDPE_Is8_4()

		/// <summary>
		/// Manhole diameter from inside diameter _5_ is 48
		/// </summary>
		[Test]
		public void ManholeDiameterFromInsideDiameter_5_Is48()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.ManholeDiameterFromInsideDiameter(5, out outsideTable), Is.EqualTo(48));
			Assert.That(outsideTable, Is.True);
		} // ManholeDiameterFromInsideDiameter_5_Is48()

		/// <summary>
		/// Manhole diameter from inside diameter _6_ is 48
		/// </summary>
		[Test]
		public void ManholeDiameterFromInsideDiameter_6_Is48()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.ManholeDiameterFromInsideDiameter(6, out outsideTable), Is.EqualTo(48));
			Assert.That(outsideTable, Is.False);
		} // ManholeDiameterFromInsideDiameter_6_Is48()

		/// <summary>
		/// Manhole diameter from inside diameter _7_ is 48
		/// </summary>
		[Test]
		public void ManholeDiameterFromInsideDiameter_7_Is48()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.ManholeDiameterFromInsideDiameter(7, out outsideTable), Is.EqualTo(48));
			Assert.That(outsideTable, Is.False);
		} // ManholeDiameterFromInsideDiameter_7_Is48()

		/// <summary>
		/// Manhole diameter from inside diameter _28_ is 60
		/// </summary>
		[Test]
		public void ManholeDiameterFromInsideDiameter_28_Is60()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.ManholeDiameterFromInsideDiameter(28, out outsideTable), Is.EqualTo(60));
			Assert.That(outsideTable, Is.False);
		} // ManholeDiameterFromInsideDiameter_28_Is60()

		/// <summary>
		/// Manhole diameter from inside diameter _103_ is 132
		/// </summary>
		[Test]
		public void ManholeDiameterFromInsideDiameter_103_Is132()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.ManholeDiameterFromInsideDiameter(103, out outsideTable), Is.EqualTo(132));
			Assert.That(outsideTable, Is.True);
		} // ManholeDiameterFromInsideDiameter_103_Is132()

		/// <summary>
		/// Trench width from outside diameter _7_ is 30
		/// </summary>
		[Test]
		public void TrenchWidthFromOutsideDiameter_7_Is2_5()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.TrenchWidthFromOutsideDiameter(7, out outsideTable), Is.EqualTo(2.5));
			Assert.That(outsideTable, Is.True);
		} // TrenchWidthFromOutsideDiameter_7_Is2_5()

		/// <summary>
		/// Trench width from outside diameter _8_ is 30
		/// </summary>
		[Test]
		public void TrenchWidthFromOutsideDiameter_8_Is2_5()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.TrenchWidthFromOutsideDiameter(8, out outsideTable), Is.EqualTo(2.5));
			Assert.That(outsideTable, Is.False);
		} // TrenchWidthFromOutsideDiameter_8_Is2_5()

		/// <summary>
		/// Trench width from outside diameter _13_ is 42
		/// </summary>
		[Test]
		public void TrenchWidthFromOutsideDiameter_13_Is2_5()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.TrenchWidthFromOutsideDiameter(13, out outsideTable), Is.EqualTo(2.5));
			Assert.That(outsideTable, Is.False);
		} // TrenchWidthFromOutsideDiameter_13_Is2_5()


		/// <summary>
		/// Trench width from outside diameter _123_ is 156
		/// </summary>
		[Test]
		public void TrenchWidthFromOutsideDiameter_123_Is12_5()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.TrenchWidthFromOutsideDiameter(123, out outsideTable), Is.EqualTo(12.5));
			Assert.That(outsideTable, Is.True);
		} // TrenchWidthFromOutsideDiameter_123_Is12_5()

		/// <summary>
		/// Pipe cost _5 concrete _ is 12_60
		/// </summary>
		[Test]
		public void PipeCost_5Concrete_Is12_60()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.PipeCost(PipeMaterial.Concrete, 5, out outsideTable), Is.EqualTo(12.60m));
			Assert.That(outsideTable, Is.True);
		} // PipeCost_5Concrete_Is12_60()

		/// <summary>
		/// Pipe cost _6 concrete _ is 12_60
		/// </summary>
		[Test]
		public void PipeCost_6Concrete_Is12_60()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.PipeCost(PipeMaterial.Concrete, 6, out outsideTable), Is.EqualTo(12.60m));
			Assert.That(outsideTable, Is.False);
		} // PipeCost_6Concrete_Is12_60()

		/// <summary>
		/// Pipe cost _25 concrete _ is 60
		/// </summary>
		[Test]
		public void PipeCost_25Concrete_Is60()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.PipeCost(PipeMaterial.Concrete, 25, out outsideTable), Is.EqualTo(60.00m));
			Assert.That(outsideTable, Is.False);
		} // PipeCost_25Concrete_Is60()

		/// <summary>
		/// Pipe cost _103 concrete _ is 1525
		/// </summary>
		[Test]
		public void PipeCost_103Concrete_Is1525()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.PipeCost(PipeMaterial.Concrete, 103, out outsideTable), Is.EqualTo(525.00m));
			Assert.That(outsideTable, Is.True);
		} // PipeCost_103Concrete_Is1525()

		/// <summary>
		/// Pipe cost _10CIPP_ is 56
		/// </summary>
		[Test]
		public void PipeCost_10CIPP_Is56()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.PipeCost(PipeMaterial.CIPP, 10, out outsideTable), Is.EqualTo(56.00m));
			Assert.That(outsideTable, Is.False);
		} // PipeCost_10CIPP_Is56()

		/// <summary>
		/// Lateral cost _5_7_ is 8
		/// </summary>
		[Test]
		public void LateralCost_5_7_Is8()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.LateralCost(5, 7, out outsideTable), Is.EqualTo(8.00m));
			Assert.That(outsideTable, Is.True);
		} // LateralCost_5_7_Is8()

		/// <summary>
		/// Lateral cost _6_7_ is 8
		/// </summary>
		[Test]
		public void LateralCost_6_7_Is8()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.LateralCost(6, 7, out outsideTable), Is.EqualTo(8.00m));
			Assert.That(outsideTable, Is.True);
		} // LateralCost_6_7_Is8()

		/// <summary>
		/// Lateral cost _5_8_ is 8
		/// </summary>
		[Test]
		public void LateralCost_5_8_Is8()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.LateralCost(5, 8, out outsideTable), Is.EqualTo(8.00m));
			Assert.That(outsideTable, Is.True);
		} // LateralCost_5_8_Is8()

		/// <summary>
		/// Lateral cost _6_8_ is 8
		/// </summary>
		[Test]
		public void LateralCost_6_8_Is8()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.LateralCost(6, 8, out outsideTable), Is.EqualTo(8.00m));
			Assert.That(outsideTable, Is.False);
		} // LateralCost_6_8_Is8()

		/// <summary>
		/// Lateral cost _7_7_ is 9
		/// </summary>
		[Test]
		public void LateralCost_7_7_Is9()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.LateralCost(7, 7, out outsideTable), Is.EqualTo(9.00m));
			Assert.That(outsideTable, Is.True);
		} // LateralCost_7_7_Is9()

		/// <summary>
		/// Lateral cost _5_9_ is 9
		/// </summary>
		[Test]
		public void LateralCost_5_9_Is9()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.LateralCost(5, 9, out outsideTable), Is.EqualTo(9.00m));
			Assert.That(outsideTable, Is.True);
		} // LateralCost_5_9_Is9()

		/// <summary>
		/// Lateral cost _9_31_ is 17
		/// </summary>
		[Test]
		public void LateralCost_9_31_Is17()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.LateralCost(9, 31, out outsideTable), Is.EqualTo(17.00m));
			Assert.That(outsideTable, Is.True);
		} // LateralCost_9_31_Is17()

		/// <summary>
		/// Lateral cost _9_8_ is 9
		/// </summary>
		[Test]
		public void LateralCost_9_8_Is9()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.LateralCost(9, 8, out outsideTable), Is.EqualTo(9.00m));
			Assert.That(outsideTable, Is.True);
		} // LateralCost_9_8_Is9()

		/// <summary>
		/// Lateral cost _8_31_ is 17
		/// </summary>
		[Test]
		public void LateralCost_8_31_Is17()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.LateralCost(8, 31, out outsideTable), Is.EqualTo(17.00m));
			Assert.That(outsideTable, Is.True);
		} // LateralCost_8_31_Is17()

		/// <summary>
		/// Manhole cost _48_8_ is 1865
		/// </summary>
		[Test]
		public void ManholeCost_48_8_Is1865()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.ManholeCost(48, 8, out outsideTable), Is.EqualTo(1865.00m));
			Assert.That(outsideTable, Is.False);
		} // ManholeCost_48_8_Is1865()

		[Test]
		public void ManholeCost_48_9_Is2182()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.ManholeCost(48, 9, out outsideTable), Is.EqualTo(2182.00m));
			Assert.That(outsideTable, Is.False);
		} // ManholeCost_48_8_Is1865()

		[Test]
		public void ManholeCost_47_8_Is1865()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.ManholeCost(47, 8, out outsideTable), Is.EqualTo(1865.00m));
			Assert.That(outsideTable, Is.True);
		} // ManholeCost_48_8_Is1865()

		[Test]
		public void ManholeCost_47_7_Is1865()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.ManholeCost(47, 7, out outsideTable), Is.EqualTo(1865.00m));
			Assert.That(outsideTable, Is.True);
		} // ManholeCost_48_8_Is1865()

		[Test]
		public void ManholeCost_133_8_Is8440()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.ManholeCost(133, 8, out outsideTable), Is.EqualTo(8440.00m));
			Assert.That(outsideTable, Is.True);
		} // ManholeCost_48_8_Is1865()

		/// <summary>
		/// Flow diversion cost _7_ is 11_50
		/// </summary>
		[Test]
		public void FlowDiversionCost_7_Is11_50()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.FlowDiversionCost(7, out outsideTable), Is.EqualTo(11.50m));
			Assert.That(outsideTable, Is.True);
		} // FlowDiversionCost_7_Is11_50()

		/// <summary>
		/// Flow diversion cost _8_ is 11_50
		/// </summary>
		[Test]
		public void FlowDiversionCost_8_Is11_50()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.FlowDiversionCost(8, out outsideTable), Is.EqualTo(11.50m));
			Assert.That(outsideTable, Is.False);
		} // FlowDiversionCost_8_Is11_50()

		/// <summary>
		/// Flow diversion cost _11_ is 12
		/// </summary>
		[Test]
		public void FlowDiversionCost_11_Is12()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.FlowDiversionCost(11, out outsideTable), Is.EqualTo(12.00m));
			Assert.That(outsideTable, Is.False);
		} // FlowDiversionCost_11_Is12()

		/// <summary>
		/// Flow diversion cost _49_ is 56
		/// </summary>
		[Test]
		public void FlowDiversionCost_49_Is56()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.FlowDiversionCost(49, out outsideTable), Is.EqualTo(56.00m));
			Assert.That(outsideTable, Is.True);
		} // FlowDiversionCost_49_Is56()

		/// <summary>
		/// CIP p cost _7_ is 52
		/// </summary>
		[Test]
		public void CIPPCost_7_Is52()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.CIPPPipeCost(7, out outsideTable), Is.EqualTo(52.00m));
			Assert.That(outsideTable, Is.True);
		} // CIPPCost_7_Is52()

		/// <summary>
		/// CIP p cost _8_ is 52
		/// </summary>
		[Test]
		public void CIPPCost_8_Is52()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.CIPPPipeCost(8, out outsideTable), Is.EqualTo(52.00m));
			Assert.That(outsideTable, Is.False);
		} // CIPPCost_8_Is52()

		/// <summary>
		/// CIP p cost _9_ is 56
		/// </summary>
		[Test]
		public void CIPPCost_9_Is56()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.CIPPPipeCost(9, out outsideTable), Is.EqualTo(56.00m));
			Assert.That(outsideTable, Is.False);
		} // CIPPCost_9_Is56()

		/// <summary>
		/// CIP p cost _49_ is 294
		/// </summary>
		[Test]
		public void CIPPCost_49_Is294()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.CIPPPipeCost(49, out outsideTable), Is.EqualTo(294.00m));
			Assert.That(outsideTable, Is.True);
		} // CIPPCost_49_Is294()

		/// <summary>
		/// Pipeburst cost _7_ is 80
		/// </summary>
		[Test]
		public void PipeburstCost_7_Is80()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.PipeburstPipeCost(7, out outsideTable), Is.EqualTo(80.00m));
			Assert.That(outsideTable, Is.True);
		} // PipeburstCost_7_Is80()

		/// <summary>
		/// Pipeburst cost _8_ is 80
		/// </summary>
		[Test]
		public void PipeburstCost_8_Is80()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.PipeburstPipeCost(8, out outsideTable), Is.EqualTo(80.00m));
			Assert.That(outsideTable, Is.False);
		} // PipeburstCost_8_Is80()

		/// <summary>
		/// Pipeburst cost _9_ is 86
		/// </summary>
		[Test]
		public void PipeburstCost_9_Is86()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.PipeburstPipeCost(9, out outsideTable), Is.EqualTo(86.00m));
			Assert.That(outsideTable, Is.False);
		} // PipeburstCost_9_Is86()

		/// <summary>
		/// Pipeburst cost _25_ is 148
		/// </summary>
		[Test]
		public void PipeburstCost_25_Is148()
		{
			bool outsideTable;
			Assert.That(_pipeCoster.PipeburstPipeCost(25, out outsideTable), Is.EqualTo(148.00m));
			Assert.That(outsideTable, Is.True);
		} // PipeburstCost_25_Is148()

		/// <summary>
		/// Pipe cost _PVCHDPE_6_8_ is 93_51
		/// </summary>
		[Test]
		public void DirectConstructionCost_PVCHDPE_6_8_Is93_51()
		{
			_pipeCoster.InsideDiameter = 6;
			_pipeCoster.Depth = 8;
			_pipeCoster.Material = PipeMaterial.PVCHDPE;
			_pipeCoster.AssignDirectConstructionCostItems();

			Console.WriteLine();
			Console.WriteLine("Asphalt removal q {0}", _pipeCoster.AsphaltRemovalSurfaceArea / 9);
			Console.WriteLine("Trench excavation q {0}", _pipeCoster.ExcavationVolume);
			Console.WriteLine("Truck haul excavation spoils q {0}", _pipeCoster.SpoilsVolume);
			Console.WriteLine("Trench shoring q {0}", _pipeCoster.Shoring);
			Console.WriteLine("Pipe zone backfill q {0}", _pipeCoster.PipeZoneVolume);
			Console.WriteLine("Above zone fill q {0}", _pipeCoster.AboveZoneVolume);
			Console.WriteLine("Asphalt trench patch base course q {0}", _pipeCoster.AsphaltBaseVolume);
			Console.WriteLine("Asphalt trench patch q {0}", _pipeCoster.AsphaltPatchSurfaceArea / 9);
			Console.WriteLine();
			foreach (KeyValuePair<string, UnitCost> kvpair in _pipeCoster.DirectConstructionCostItems)
				Console.WriteLine("{0} {1:C}", kvpair.Key, kvpair.Value.CostPerUnit);

			Assert.That(_pipeCoster.DirectConstructionCost, Is.EqualTo(93.51m).Within(25.0m));
		} // PipeCost_PVCHDPE_6_8_Is93_50()

		/// <summary>
		/// Direct construction cost _PVCHDPE_8_8_ is 96_52
		/// </summary>
		[Test]
		public void DirectConstructionCost_PVCHDPE_8_8_Is96_52()
		{
			_pipeCoster.InsideDiameter = 8;
			_pipeCoster.Depth = 8;
			_pipeCoster.Material = PipeMaterial.PVCHDPE;
			_pipeCoster.AssignDirectConstructionCostItems();

			Console.WriteLine();
			Console.WriteLine("Asphalt removal q {0}", _pipeCoster.AsphaltRemovalSurfaceArea / 9);
			Console.WriteLine("Trench excavation q {0}", _pipeCoster.ExcavationVolume);
			Console.WriteLine("Truck haul excavation spoils q {0}", _pipeCoster.SpoilsVolume);
			Console.WriteLine("Trench shoring q {0}", _pipeCoster.Shoring);
			Console.WriteLine("Pipe zone backfill q {0}", _pipeCoster.PipeZoneVolume);
			Console.WriteLine("Above zone fill q {0}", _pipeCoster.AboveZoneVolume);
			Console.WriteLine("Asphalt trench patch base course q {0}", _pipeCoster.AsphaltBaseVolume);
			Console.WriteLine("Asphalt trench patch q {0}", _pipeCoster.AsphaltPatchSurfaceArea / 9);
			Console.WriteLine();
			foreach (KeyValuePair<string, UnitCost> kvpair in _pipeCoster.DirectConstructionCostItems)
				Console.WriteLine("{0} {1:C}", kvpair.Key, kvpair.Value.CostPerUnit);

			Assert.That(_pipeCoster.DirectConstructionCost, Is.EqualTo(96.52m).Within(25.0m));
		} // DirectConstructionCost_PVCHDPE_8_8_Is96_52()

		/// <summary>
		/// Direct construction cost _PVCHDPE_15_18_ is 183_85
		/// </summary>
		[Test]
		public void DirectConstructionCost_PVCHDPE_15_18_Is183_85()
		{
			_pipeCoster.InsideDiameter = 15;
			_pipeCoster.Depth = 18;
			_pipeCoster.Material = PipeMaterial.PVCHDPE;
			_pipeCoster.AssignDirectConstructionCostItems();

			Console.WriteLine();
			Console.WriteLine("Asphalt removal q {0}", _pipeCoster.AsphaltRemovalSurfaceArea / 9);
			Console.WriteLine("Trench excavation q {0}", _pipeCoster.ExcavationVolume);
			Console.WriteLine("Truck haul excavation spoils q {0}", _pipeCoster.SpoilsVolume);
			Console.WriteLine("Trench shoring q {0}", _pipeCoster.Shoring);
			Console.WriteLine("Pipe zone backfill q {0}", _pipeCoster.PipeZoneVolume);
			Console.WriteLine("Above zone fill q {0}", _pipeCoster.AboveZoneVolume);
			Console.WriteLine("Asphalt trench patch base course q {0}", _pipeCoster.AsphaltBaseVolume);
			Console.WriteLine("Asphalt trench patch q {0}", _pipeCoster.AsphaltPatchSurfaceArea / 9);
			Console.WriteLine();
			foreach (KeyValuePair<string, UnitCost> kvpair in _pipeCoster.DirectConstructionCostItems)
				Console.WriteLine("{0} {1:C}", kvpair.Key, kvpair.Value.CostPerUnit);

			Assert.That(_pipeCoster.DirectConstructionCost, Is.EqualTo(183.85m).Within(25.0m));
		} // DirectConstructionCost_PVCHDPE_15_18_Is183_85()

		/// <summary>
		/// Direct construction cost _ concrete _6_8_ is 100_75
		/// </summary>
		[Test]
		public void DirectConstructionCost_Concrete_6_8_Is100_75()
		{
			_pipeCoster.InsideDiameter = 6;
			_pipeCoster.Depth = 8;
			_pipeCoster.Material = PipeMaterial.Concrete;
			_pipeCoster.AssignDirectConstructionCostItems();

			Console.WriteLine();
			Console.WriteLine("Asphalt removal q {0}", _pipeCoster.AsphaltRemovalSurfaceArea / 9);
			Console.WriteLine("Trench excavation q {0}", _pipeCoster.ExcavationVolume);
			Console.WriteLine("Truck haul excavation spoils q {0}", _pipeCoster.SpoilsVolume);
			Console.WriteLine("Trench shoring q {0}", _pipeCoster.Shoring);
			Console.WriteLine("Pipe zone backfill q {0}", _pipeCoster.PipeZoneVolume);
			Console.WriteLine("Above zone fill q {0}", _pipeCoster.AboveZoneVolume);
			Console.WriteLine("Asphalt trench patch base course q {0}", _pipeCoster.AsphaltBaseVolume);
			Console.WriteLine("Asphalt trench patch q {0}", _pipeCoster.AsphaltPatchSurfaceArea / 9);
			Console.WriteLine();
			foreach (KeyValuePair<string, UnitCost> kvpair in _pipeCoster.DirectConstructionCostItems)
				Console.WriteLine("{0} {1:C}", kvpair.Key, kvpair.Value.CostPerUnit);

			Assert.That(_pipeCoster.DirectConstructionCost, Is.EqualTo(100.75).Within(25.0m));
		} // DirectConstructionCost_Concrete_6_8_Is100_75()

		/// <summary>
		/// Base ENR_ is 6580
		/// </summary>
		[Test]
		public void BaseENR_Is6580()
		{
			Assert.That(_pipeCoster.BaseENR, Is.EqualTo(6580));
		} // BaseENR_Is6580()
	}
}
