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
      Assert.That(_pipeCoster.OutsideDiameterFromInsideDiameter(PipeMaterial.Concrete, 5, out outsideTable), Is.EqualTo(8).Within(0.01d));
      Assert.That(outsideTable, Is.True);
    } // OutsideDiameterFromInsideDiameter_5CSP_Is8()

    /// <summary>
    /// Outside diameter from inside diameter _6 concrete _ is 8
    /// </summary>
    [Test]
    public void OutsideDiameterFromInsideDiameter_6Concrete_Is8()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.OutsideDiameterFromInsideDiameter(PipeMaterial.Concrete, 6, out outsideTable), Is.EqualTo(8).Within(0.01d));
      Assert.That(outsideTable, Is.False);
    } // OutsideDiameterFromInsideDiameter_6Concrete_Is8()

    /// <summary>
    /// Outside diameter from inside diameter _7 concrete _ is 10
    /// </summary>
    [Test]
    public void OutsideDiameterFromInsideDiameter_7Concrete_Is10()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.OutsideDiameterFromInsideDiameter(PipeMaterial.Concrete, 7, out outsideTable), Is.EqualTo(10).Within(0.01d));
      Assert.That(outsideTable, Is.False);
    } // OutsideDiameterFromInsideDiameter_7Concrete_Is10()

    /// <summary>
    /// Outside diameter from inside diameter _103 concrete _ is 122_5
    /// </summary>
    [Test]
    public void OutsideDiameterFromInsideDiameter_103Concrete_Is122_5()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.OutsideDiameterFromInsideDiameter(PipeMaterial.Concrete, 103, out outsideTable), Is.EqualTo(122.5).Within(0.01d));
      Assert.That(outsideTable, Is.True);
    } // OutsideDiameterFromInsideDiameter_103Concrete_Is122_5()

    /// <summary>
    /// Outside diameter from inside diameter _7PVCHDPE_ is 8_4
    /// </summary>
    [Test]
    public void OutsideDiameterFromInsideDiameter_7PVCHDPE_Is8_4()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.OutsideDiameterFromInsideDiameter(PipeMaterial.PVCHDPE, 7, out outsideTable), Is.EqualTo(10).Within(0.01d));
      Assert.That(outsideTable, Is.False);
    } // OutsideDiameterFromInsideDiameter_7PVCHDPE_Is8_4()

    /// <summary>
    /// Manhole diameter from inside diameter _5_ is 48
    /// </summary>
    [Test]
    public void ManholeDiameterFromInsideDiameter_5_Is48()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.ManholeDiameterFromInsideDiameter(5, out outsideTable), Is.EqualTo(48).Within(0.01d));
      Assert.That(outsideTable, Is.True);
    } // ManholeDiameterFromInsideDiameter_5_Is48()

    /// <summary>
    /// Manhole diameter from inside diameter _6_ is 48
    /// </summary>
    [Test]
    public void ManholeDiameterFromInsideDiameter_6_Is48()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.ManholeDiameterFromInsideDiameter(6, out outsideTable), Is.EqualTo(48).Within(0.01d));
      Assert.That(outsideTable, Is.False);
    } // ManholeDiameterFromInsideDiameter_6_Is48()

    /// <summary>
    /// Manhole diameter from inside diameter _7_ is 48
    /// </summary>
    [Test]
    public void ManholeDiameterFromInsideDiameter_7_Is48()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.ManholeDiameterFromInsideDiameter(7, out outsideTable), Is.EqualTo(48).Within(0.01d));
      Assert.That(outsideTable, Is.False);
    } // ManholeDiameterFromInsideDiameter_7_Is48()

    /// <summary>
    /// Manhole diameter from inside diameter _28_ is 60
    /// </summary>
    [Test]
    public void ManholeDiameterFromInsideDiameter_28_Is60()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.ManholeDiameterFromInsideDiameter(28, out outsideTable), Is.EqualTo(60).Within(0.01d));
      Assert.That(outsideTable, Is.False);
    } // ManholeDiameterFromInsideDiameter_28_Is60()

    /// <summary>
    /// Manhole diameter from inside diameter _103_ is 132
    /// </summary>
    [Test]
    public void ManholeDiameterFromInsideDiameter_103_Is132()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.ManholeDiameterFromInsideDiameter(103, out outsideTable), Is.EqualTo(132).Within(0.01d));
      Assert.That(outsideTable, Is.True);
    } // ManholeDiameterFromInsideDiameter_103_Is132()

    /// <summary>
    /// Trench width from outside diameter _7_ is 30
    /// </summary>
    [Test]
    public void TrenchWidthFromOutsideDiameter_7_Is2_5()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.TrenchWidthFromOutsideDiameter(7, out outsideTable), Is.EqualTo(2.5).Within(0.01d));
      Assert.That(outsideTable, Is.True);
    } // TrenchWidthFromOutsideDiameter_7_Is2_5()

    /// <summary>
    /// Trench width from outside diameter _8_ is 30
    /// </summary>
    [Test]
    public void TrenchWidthFromOutsideDiameter_8_Is2_5()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.TrenchWidthFromOutsideDiameter(8, out outsideTable), Is.EqualTo(2.5).Within(0.01d));
      Assert.That(outsideTable, Is.False);
    } // TrenchWidthFromOutsideDiameter_8_Is2_5()

    /// <summary>
    /// Trench width from outside diameter _13_ is 42
    /// </summary>
    [Test]
    public void TrenchWidthFromOutsideDiameter_13_Is2_5()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.TrenchWidthFromOutsideDiameter(13, out outsideTable), Is.EqualTo(3.0).Within(0.01d));
      Assert.That(outsideTable, Is.False);
    } // TrenchWidthFromOutsideDiameter_13_Is2_5()


    /// <summary>
    /// Trench width from outside diameter _123_ is 12.5
    /// </summary>
    [Test]
    public void TrenchWidthFromOutsideDiameter_123_Is12_5()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.TrenchWidthFromOutsideDiameter(123, out outsideTable), Is.EqualTo(14.0).Within(0.01d));
      Assert.That(outsideTable, Is.True);
    } // TrenchWidthFromOutsideDiameter_123_Is12_5()

    /// <summary>
    /// Pipe cost _5 concrete _ is 16.71
    /// </summary>
    [Test]
    public void PipeCost_5Concrete_Is16_71()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.PipeCost(PipeMaterial.Concrete, 5, out outsideTable), Is.EqualTo(16.71m).Within(0.01m));
      Assert.That(outsideTable, Is.True);
    } // PipeCost_5Concrete_Is16_71()

    /// <summary>
    /// Pipe cost _6 concrete _ is 16_71
    /// </summary>
    [Test]
    public void PipeCost_6Concrete_Is16_71()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.PipeCost(PipeMaterial.Concrete, 6, out outsideTable), Is.EqualTo(16.71m).Within(0.01m));
      Assert.That(outsideTable, Is.False);
    } // PipeCost_6Concrete_Is16_71()

    /// <summary>
    /// Pipe cost _25 concrete _ is 82.77
    /// </summary>
    [Test]
    public void PipeCost_25Concrete_Is82_77()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.PipeCost(PipeMaterial.Concrete, 25, out outsideTable), Is.EqualTo(82.77m).Within(0.01m));
      Assert.That(outsideTable, Is.False);
    } // PipeCost_25Concrete_Is82_77()

    /// <summary>
    /// Pipe cost _103 concrete _ is 718.32
    /// </summary>
    [Test]
    public void PipeCost_103Concrete_Is718_32()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.PipeCost(PipeMaterial.Concrete, 103, out outsideTable), Is.EqualTo(718.32m).Within(0.01m));
      Assert.That(outsideTable, Is.True);
    } // PipeCost_103Concrete_Is718_32()

    /// <summary>
    /// Pipe cost _10CIPP_ is 82.28
    /// </summary>
    [Test]
    public void PipeCost_10CIPP_Is82_28()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.PipeCost(PipeMaterial.CIPP, 10, out outsideTable), Is.EqualTo(82.28m).Within(0.01m));
      Assert.That(outsideTable, Is.False);
    } // PipeCost_10CIPP_Is82_28()

    /// <summary>
    /// Lateral cost _5_7_ is 91.05
    /// </summary>
    [Test]
    public void LateralCost_5_7_Is91_05()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.LateralCost(5, 7, out outsideTable), Is.EqualTo(91.05m).Within(0.01m));
      Assert.That(outsideTable, Is.True);
    } // LateralCost_5_7_Is91_05()

    /// <summary>
    /// Lateral cost _6_7_ is 91.05
    /// </summary>
    [Test]
    public void LateralCost_6_7_Is91_05()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.LateralCost(6, 7, out outsideTable), Is.EqualTo(91.05m).Within(0.01m));
      Assert.That(outsideTable, Is.True);
    } // LateralCost_6_7_Is91_05()

    /// <summary>
    /// Lateral cost _5_8_ is 91.05
    /// </summary>
    [Test]
    public void LateralCost_5_8_Is91_05()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.LateralCost(5, 8, out outsideTable), Is.EqualTo(91.05m).Within(0.01m));
      Assert.That(outsideTable, Is.True);
    } // LateralCost_5_8_Is8()

    /// <summary>
    /// Lateral cost _6_8_ is 91.05
    /// </summary>
    [Test]
    public void LateralCost_6_8_Is91_05()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.LateralCost(6, 8, out outsideTable), Is.EqualTo(91.05m).Within(0.01m));
      Assert.That(outsideTable, Is.False);
    } // LateralCost_6_8_Is91_05()

    /// <summary>
    /// Lateral cost _7_7_ is 9
    /// </summary>
    [Test]
    public void LateralCost_7_7_Is91_05()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.LateralCost(7, 7, out outsideTable), Is.EqualTo(91.05m).Within(0.01m));
      Assert.That(outsideTable, Is.True);
    } // LateralCost_7_7_Is91_05()

    /// <summary>
    /// Lateral cost _5_9_ is 91.05
    /// </summary>
    [Test]
    public void LateralCost_5_9_Is91_05()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.LateralCost(5, 9, out outsideTable), Is.EqualTo(91.05m).Within(0.01m));
      Assert.That(outsideTable, Is.True);
    } // LateralCost_5_9_Is91_05()

    /// <summary>
    /// Lateral cost _9_31_ is 91.05
    /// </summary>
    [Test]
    public void LateralCost_9_31_Is91_05()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.LateralCost(9, 31, out outsideTable), Is.EqualTo(91.05m).Within(0.01m));
      Assert.That(outsideTable, Is.True);
    } // LateralCost_9_31_Is91_05()

    /// <summary>
    /// Lateral cost _9_8_ is 91.05
    /// </summary>
    [Test]
    public void LateralCost_9_8_Is91_05()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.LateralCost(9, 8, out outsideTable), Is.EqualTo(91.05m).Within(0.01d));
      Assert.That(outsideTable, Is.True);
    } // LateralCost_9_8_Is91_05()

    /// <summary>
    /// Lateral cost _8_31_ is 91.05
    /// </summary>
    [Test]
    public void LateralCost_8_31_Is91_05()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.LateralCost(8, 31, out outsideTable), Is.EqualTo(91.05m).Within(0.01d));
      Assert.That(outsideTable, Is.True);
    } // LateralCost_8_31_Is91_05()

    /// <summary>
    /// Manhole cost _48_8_ is 2646
    /// </summary>
    [Test]
    public void ManholeCost_48_8_Is2464()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.ManholeCost(48, 8, out outsideTable), Is.EqualTo(2646.0).Within(1.0m));
      Assert.That(outsideTable, Is.False);
    } // ManholeCost_48_8_Is2646()

    /// <summary>
    /// ManholeCost _24_9_ Is 2522
    /// </summary>
    [Test]
    public void ManholeCost_24_9_Is2522()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.ManholeCost(24, 9, out outsideTable), Is.EqualTo(2522.00m).Within(1.0m));
      Assert.That(outsideTable, Is.False);
    } // ManholeCost_48_9_Is2522()

    [Test]
    public void ManholeCost_12_8_Is2205()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.ManholeCost(12, 8, out outsideTable), Is.EqualTo(2205.00m).Within(1.0m));
      Assert.That(outsideTable, Is.True);
    } // ManholeCost_12_7_Is2205()

    [Test]
    public void ManholeCost_12_7_Is2205()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.ManholeCost(12, 7, out outsideTable), Is.EqualTo(2205.00m).Within(1.0m));
      Assert.That(outsideTable, Is.True);
    } // ManholeCost_12_8_Is2205()

    [Test]
    public void ManholeCost_133_8_Is10097()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.ManholeCost(133, 8, out outsideTable), Is.EqualTo(10097.00m).Within(1.0m));
      Assert.That(outsideTable, Is.True);
    } // ManholeCost_133_8_Is10097()

    /// <summary>
    /// Flow diversion cost _7_ is 11_50
    /// </summary>
    [Test]
    public void FlowDiversionCost_7_Is11_50()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.FlowDiversionCost(7, out outsideTable), Is.EqualTo(11.50m).Within(0.01m));
      Assert.That(outsideTable, Is.True);
    } // FlowDiversionCost_7_Is11_50()

    /// <summary>
    /// Flow diversion cost _8_ is 11_50
    /// </summary>
    [Test]
    public void FlowDiversionCost_8_Is11_50()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.FlowDiversionCost(8, out outsideTable), Is.EqualTo(11.50m).Within(0.01m));
      Assert.That(outsideTable, Is.False);
    } // FlowDiversionCost_8_Is11_50()

    /// <summary>
    /// Flow diversion cost _11_ is 12
    /// </summary>
    [Test]
    public void FlowDiversionCost_11_Is12()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.FlowDiversionCost(11, out outsideTable), Is.EqualTo(12.00m).Within(0.01m));
      Assert.That(outsideTable, Is.False);
    } // FlowDiversionCost_11_Is12()

    /// <summary>
    /// Flow diversion cost _49_ is 56
    /// </summary>
    [Test]
    public void FlowDiversionCost_49_Is56()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.FlowDiversionCost(49, out outsideTable), Is.EqualTo(56.00m).Within(0.01m));
      Assert.That(outsideTable, Is.True);
    } // FlowDiversionCost_49_Is56()

    /// <summary>
    /// CIP p cost _7_ is 62.88
    /// </summary>
    [Test]
    public void CIPPCost_7_Is62_88()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.CIPPPipeCost(7, out outsideTable), Is.EqualTo(62.88m).Within(0.01m));
      Assert.That(outsideTable, Is.True);
    } // CIPPCost_7_Is52()

    /// <summary>
    /// CIP p cost _8_ is 62.88
    /// </summary>
    [Test]
    public void CIPPCost_8_Is62_88()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.CIPPPipeCost(8, out outsideTable), Is.EqualTo(62.88m).Within(0.01m));
      Assert.That(outsideTable, Is.False);
    } // CIPPCost_8_Is62_88()

    /// <summary>
    /// CIP p cost _9_ is 82.28
    /// </summary>
    [Test]
    public void CIPPCost_9_Is82_28()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.CIPPPipeCost(9, out outsideTable), Is.EqualTo(82.28m).Within(0.01m));
      Assert.That(outsideTable, Is.False);
    } // CIPPCost_9_Is82_28()

    /// <summary>
    /// CIP p cost _49_ is 569.13
    /// </summary>
    [Test]
    public void CIPPCost_49_Is569_13()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.CIPPPipeCost(49, out outsideTable), Is.EqualTo(569.13m).Within(0.01m));
      Assert.That(outsideTable, Is.True);
    } // CIPPCost_49_Is5693_13()

    /// <summary>
    /// Pipeburst cost _7_ is 308.04
    /// </summary>
    [Test]
    public void PipeburstCost_7_Is308_04()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.PipeburstPipeCost(7, out outsideTable), Is.EqualTo(308.04m).Within(0.01m));
      Assert.That(outsideTable, Is.True);
    } // PipeburstCost_7_Is308._4()

    /// <summary>
    /// Pipeburst cost _8_ is 308.04
    /// </summary>
    [Test]
    public void PipeburstCost_8_Is308_04()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.PipeburstPipeCost(8, out outsideTable), Is.EqualTo(308.04m).Within(0.01m));
      Assert.That(outsideTable, Is.False);
    } // PipeburstCost_8_Is308_04()

    /// <summary>
    /// Pipeburst cost _9_ is 334.35
    /// </summary>
    [Test]
    public void PipeburstCost_9_Is334_35()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.PipeburstPipeCost(9, out outsideTable), Is.EqualTo(334.35m));
      Assert.That(outsideTable, Is.False);
    } // PipeburstCost_9_Is334_35()

    /// <summary>
    /// Pipeburst cost _25_ is 457.09
    /// </summary>
    [Test]
    public void PipeburstCost_25_Is457_09()
    {
      bool outsideTable;
      Assert.That(_pipeCoster.PipeburstPipeCost(25, out outsideTable), Is.EqualTo(457.09m));
      Assert.That(outsideTable, Is.True);
    } // PipeburstCost_25_Is457_09()

    /// <summary>
    /// Pipe cost _PVCHDPE_6_8_ is 188.44
    /// </summary>
    [Test]
    public void DirectConstructionCost_PVCHDPE_6_8_Is188_44()
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

      Assert.That(_pipeCoster.DirectConstructionCost, Is.EqualTo(188.44m).Within(5.0m));
    } // PipeCost_PVCHDPE_6_8_Is188_44()

    /// <summary>
    /// Direct construction cost _PVCHDPE_8_8_ is 190.69
    /// </summary>
    [Test]
    public void DirectConstructionCost_PVCHDPE_8_8_Is190_69()
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

      Assert.That(_pipeCoster.DirectConstructionCost, Is.EqualTo(190.69m).Within(5.0m));
    } // DirectConstructionCost_PVCHDPE_8_8_Is190_69()

    /// <summary>
    /// Direct construction cost _PVCHDPE_15_18_ is 299.33
    /// </summary>
    [Test]
    public void DirectConstructionCost_PVCHDPE_15_18_Is299_33()
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

      Assert.That(_pipeCoster.DirectConstructionCost, Is.EqualTo(299.33m).Within(5.0m));
    } // DirectConstructionCost_PVCHDPE_15_18_Is299_33()

    /// <summary>
    /// Direct construction cost _ concrete _6_8_ is 199.21
    /// </summary>
    [Test]
    public void DirectConstructionCost_Concrete_6_8_Is199_21()
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

      Assert.That(_pipeCoster.DirectConstructionCost, Is.EqualTo(199.21).Within(5.0m));
    } // DirectConstructionCost_Concrete_6_8_Is199_21()

    /// <summary>
    /// Base ENR_ is 8090
    /// </summary>
    [Test]
    public void BaseENR_Is8090()
    {
      Assert.That(_pipeCoster.BaseENR.Value, Is.EqualTo(8090));
    } // BaseENR_Is8090()
  }
}
