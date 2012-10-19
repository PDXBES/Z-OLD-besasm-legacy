// Project: SystemsAnalysis.Modeling, File: PipeConflict.cs
// Namespace: SystemsAnalysis.Modeling, Class: 
// Path: C:\Development\DotNet\Modeling, Author: Arnel
// Code lines: 12, Size of file: 208 Bytes
// Creation date: 6/30/2008 4:46 PM
// Last modified: 8/26/2008 11:44 AM

using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Types;

namespace SystemsAnalysis.Modeling
{
  public interface PipeConflict
  {
    #region Properties
    /// <summary>
    /// Num water crossings
    /// </summary>
    /// <returns>Int</returns>
    int NumWaterCrossings { get; } // NumWaterCrossings

    /// <summary>
    /// Smallest water crossing diameter inches
    /// </summary>
    /// <returns>Int</returns>
    int SmallestWaterCrossingDiameterInches { get; } // SmallestWaterCross

    /// <summary>
    /// Largest water crossing diameter inches
    /// </summary>
    /// <returns>Int</returns>
    int LargestWaterCrossingDiameterInches { get; } // LargestWaterCrossin

    /// <summary>
    /// Has water parallel
    /// </summary>
    /// <returns>Bool</returns>
    bool HasWaterParallel { get; } // HasWaterParallel

    /// <summary>
    /// Largest water parallel diameter inches
    /// </summary>
    /// <returns>Int</returns>
    int LargestWaterParallelDiameterInches { get; } // LargestWaterParalle

    /// <summary>
    /// Dist to water parallel feet
    /// </summary>
    /// <returns>Int</returns>
    int DistToWaterParallelFeet { get; } // DistToWaterParallelFeet

    /// <summary>
    /// Num sewer crossings
    /// </summary>
    /// <returns>Int</returns>
    int NumSewerCrossings { get; } // NumSewerCrossings

    /// <summary>
    /// Smallest sewer crossing diameter inches
    /// </summary>
    /// <returns>Int</returns>
    int SmallestSewerCrossingDiameterInches { get; } // SmallestSewerCross

    /// <summary>
    /// Largest sewer crossing diameter inches
    /// </summary>
    /// <returns>Int</returns>
    int LargestSewerCrossingDiameterInches { get; } // LargestSewerCrossin

    /// <summary>
    /// Has sewer parallel
    /// </summary>
    /// <returns>Bool</returns>
    bool HasSewerParallel { get; } // HasSewerParallel

    /// <summary>
    /// Largest sewer parallel diameter inches
    /// </summary>
    /// <returns>Int</returns>
    int LargestSewerParallelDiameterInches { get; } // LargestSewerParalle

    /// <summary>
    /// Dist to sewer parallel feet
    /// </summary>
    /// <returns>Int</returns>
    int DistToSewerParallelFeet { get; } // DistToSewerParallelFeet

    /// <summary>
    /// Num street crossings
    /// </summary>
    /// <returns>Int</returns>
    int NumStreetCrossings { get; } // NumStreetCrossings

    /// <summary>
    /// Num arterial crossings
    /// </summary>
    /// <returns>Int</returns>
    int NumArterialCrossings { get; } // NumArterialCrossings

    /// <summary>
    /// Num major arterial crossings
    /// </summary>
    /// <returns>Int</returns>
    int NumMajorArterialCrossings { get; } // NumMajorArterialCrossings

    /// <summary>
    /// Num freeway crossings
    /// </summary>
    /// <returns>Int</returns>
    int NumFreewayCrossings { get; } // NumFreewayCrossings

    /// <summary>
    /// Is in street
    /// </summary>
    /// <returns>Bool</returns>
    bool IsInStreet { get; } // IsInStreet

    /// <summary>
    /// Street type
    /// </summary>
    /// <returns>Street type kind</returns>
    Enumerators.StreetTypeKind StreetType { get; } // StreetType

    /// <summary>
    /// Dist to street centerline feet
    /// </summary>
    /// <returns>Int</returns>
    int DistToStreetCenterlineFeet { get; } // DistToStreetCenterlineFeet

    /// <summary>
    /// Num streets if us node in intersection
    /// </summary>
    /// <returns>Int</returns>
    int NumStreetsIfUsNodeInIntersection { get; } // NumStreetsIfUsNodeInI

    /// <summary>
    /// Dist US node to intersection feet
    /// </summary>
    /// <returns>Int</returns>
    int DistUSNodeToIntersectionFeet { get; } // DistUSNodeToIntersectionF

    /// <summary>
    /// Num streets if DS node in intersection
    /// </summary>
    /// <returns>Int</returns>
    int NumStreetsIfDSNodeInIntersection { get; } // NumStreetsIfDSNodeInI

    /// <summary>
    /// Dist DS node to intersection feet
    /// </summary>
    /// <returns>Int</returns>
    int DistDSNodeToIntersectionFeet { get; } // DistDSNodeToIntersectionF

    /// <summary>
    /// Num railroad crossings
    /// </summary>
    /// <returns>Int</returns>
    int NumRailroadCrossings { get; } // NumRailroadCrossings

    /// <summary>
    /// Has railroad parallel
    /// </summary>
    /// <returns>Bool</returns>
    bool HasRailroadParallel { get; } // HasRailroadParallel

    /// <summary>
    /// Dist to railroad parallel feet
    /// </summary>
    /// <returns>Int</returns>
    int DistToRailroadParallelFeet { get; } // DistToRailroadParallelFeet

    /// <summary>
    /// Num light rail crossings
    /// </summary>
    /// <returns>Int</returns>
    int NumLightRailCrossings { get; } // NumLightRailCrossings

    /// <summary>
    /// Has light rail parallel
    /// </summary>
    /// <returns>Bool</returns>
    bool HasLightRailParallel { get; } // HasLightRailParallel

    /// <summary>
    /// Dist to light rail parallel feet
    /// </summary>
    /// <returns>Int</returns>
    int DistToLightRailParallelFeet { get; } // DistToLightRailParallelFeet

    /// <summary>
    /// Num fiber optic crossings
    /// </summary>
    /// <returns>Int</returns>
    int NumFiberOpticCrossings { get; } // NumFiberOpticCrossings

    /// <summary>
    /// Has fiber optic parallel
    /// </summary>
    /// <returns>Bool</returns>
    bool HasFiberOpticParallel { get; } // HasFiberOpticParallel

    /// <summary>
    /// Dist to fiber optic parallel feet
    /// </summary>
    /// <returns>Int</returns>
    int DistToFiberOpticParallelFeet { get; } // DistToFiberOpticParallelF

    /// <summary>
    /// Num gas crossings
    /// </summary>
    /// <returns>Int</returns>
    int NumGasCrossings { get; } // NumGasCrossings

    /// <summary>
    /// Has gas parallel
    /// </summary>
    /// <returns>Bool</returns>
    bool HasGasParallel { get; } // HasGasParallel

    /// <summary>
    /// Dist to gas parallel
    /// </summary>
    /// <returns>Int</returns>
    int DistToGasParallel { get; } // DistToGasParallel

    /// <summary>
    /// Is in conservation zone
    /// </summary>
    /// <returns>Bool</returns>
    bool IsInConservationZone { get; } // IsInConservationZone

    /// <summary>
    /// Length in conservation zone feet
    /// </summary>
    /// <returns>Int</returns>
    int LengthInConservationZoneFeet { get; } // LengthInConservationZoneF

    /// <summary>
    /// Is in preservation zone
    /// </summary>
    /// <returns>Bool</returns>
    bool IsInPreservationZone { get; } // IsInPreservationZone

    /// <summary>
    /// Length in preservation zone feet
    /// </summary>
    /// <returns>Int</returns>
    int LengthInPreservationZoneFeet { get; } // LengthInPreservationZoneF

    /// <summary>
    /// Area conservation zone sq ft
    /// </summary>
    /// <returns>Int</returns>
    int AreaConservationZoneSqFt { get; } // AreaConservationZoneSqFt

    /// <summary>
    /// Area preservation zone sq ft
    /// </summary>
    /// <returns>Int</returns>
    int AreaPreservationZoneSqFt { get; } // AreaPreservationZoneSqFt

    /// <summary>
    /// Is near contamination site
    /// </summary>
    /// <returns>Bool</returns>
    bool IsNearContaminationSite { get; } // IsNearContaminationSite

    /// <summary>
    /// Dist to nearest ecsi feet
    /// </summary>
    /// <returns>Int</returns>
    int DistToNearestEcsiFeet { get; } // DistToNearestEcsiFeet

    /// <summary>
    /// Vol hazardous materials cu yd
    /// </summary>
    /// <returns>Int</returns>
    int VolHazardousMaterialsCuYd { get; } // VolHazardousMaterialsCuYd

    /// <summary>
    /// Is near school
    /// </summary>
    /// <returns>Bool</returns>
    bool IsNearSchool { get; } // IsNearSchool

    /// <summary>
    /// Dist to school feet
    /// </summary>
    /// <returns>Int</returns>
    int DistToSchoolFeet { get; } // DistToSchoolFeet

    /// <summary>
    /// Is near hospital
    /// </summary>
    /// <returns>Bool</returns>
    bool IsNearHospital { get; } // IsNearHospital

    /// <summary>
    /// Dist to hospital feet
    /// </summary>
    /// <returns>Int</returns>
    int DistToHospitalFeet { get; } // DistToHospitalFeet

    /// <summary>
    /// Is near police station
    /// </summary>
    /// <returns>Bool</returns>
    bool IsNearPoliceStation { get; } // IsNearPoliceStation

    /// <summary>
    /// Dist to police station feet
    /// </summary>
    /// <returns>Int</returns>
    int DistToPoliceStationFeet { get; } // DistToPoliceStationFeet

    /// <summary>
    /// Is near fire station
    /// </summary>
    /// <returns>Bool</returns>
    bool IsNearFireStation { get; } // IsNearFireStation

    /// <summary>
    /// Dist to fire station feet
    /// </summary>
    /// <returns>Int</returns>
    int DistToFireStationFeet { get; } // DistToFireStationFeet

    /// <summary>
    /// Num emergency route crossings
    /// </summary>
    /// <returns>Int</returns>
    int NumEmergencyRouteCrossings { get; } // NumEmergencyRouteCrossings

    /// <summary>
    /// Is in emergency route
    /// </summary>
    /// <returns>Bool</returns>
    bool IsInEmergencyRoute { get; } // IsInEmergencyRoute

    /// <summary>
    /// Dist to emergency route centerline feet
    /// </summary>
    /// <returns>Int</returns>
    int DistToEmergencyRouteCenterlineFeet { get; } // DistToEmergencyRout

    /// <summary>
    /// USNode in MS4
    /// </summary>
    /// <returns>Bool</returns>
    bool USNodeInMS4 { get; } // USNodeInMS4

    /// <summary>
    /// USNode in UIC
    /// </summary>
    /// <returns>Bool</returns>
    bool USNodeInUIC { get; } // USNodeInUIC

    /// <summary>
    /// USNode depth
    /// </summary>
    /// <returns>Double</returns>
    double USNodeDepth { get; } // USNodeDepth

    /// <summary>
    /// DSNode depth
    /// </summary>
    /// <returns>Double</returns>
    double DSNodeDepth { get; } // DSNodeDepth

    /// <summary>
    /// Surface slope pct
    /// </summary>
    /// <returns>Int</returns>
    int SurfaceSlopePct { get; } // SurfaceSlopePct

    /// <summary>
    /// Is near building
    /// </summary>
    /// <returns>Bool</returns>
    bool IsNearBuilding { get; } // IsNearBuilding

    /// <summary>
    /// Dist to building feet
    /// </summary>
    /// <returns>Int</returns>
    int DistToBuildingFeet { get; } // DistToBuildingFeet

    /// <summary>
    /// Is near hydrant
    /// </summary>
    /// <returns>Bool</returns>
    bool IsNearHydrant { get; } // IsNearHydrant

    /// <summary>
    /// Dist to hydrant
    /// </summary>
    /// <returns>Int</returns>
    int DistToHydrant { get; } // DistToHydrant

    bool IsHardArea { get; }
    #endregion
  }
}
