_excess_parking
----------
SELECT [_union_parking_ics_post_sum].*, mdl_dirsc_ac.PkAreaFt, [ParkingControlled]-[PkAreaFt] AS excess
FROM _union_parking_ics_post_sum INNER JOIN mdl_dirsc_ac ON ([_union_parking_ics_post_sum].DivideID = mdl_dirsc_ac.DivideID) AND ([_union_parking_ics_post_sum].ParcelID = mdl_dirsc_ac.ParcelID)
WHERE ((([ParkingControlled]-[PkAreaFt])>0));


_excess_roof
----------
SELECT [_union_roof_ics_post_sum].*, mdl_dirsc_ac.RfAreaFt, [RoofControlled]-[RfAreaFt] AS excess
FROM _union_roof_ics_post_sum INNER JOIN mdl_dirsc_ac ON ([_union_roof_ics_post_sum].DivideID = mdl_dirsc_ac.DivideID) AND ([_union_roof_ics_post_sum].ParcelID = mdl_dirsc_ac.ParcelID)
WHERE ((([RoofControlled]-[RfAreaFt])>0));


_filter_parking
----------
SELECT IC_ParkingTargets.*
FROM IC_ParkingTargets
WHERE ((([IC_ParkingTargets].[BuildModelIC])=True) or (Constructed > 0));


_filter_roof
----------
SELECT IC_RoofTargets.*
FROM IC_RoofTargets
WHERE ((([IC_RoofTargets].[BuildModelIC])=True) or (Constructed > 0));


_filter_street
----------
SELECT IC_StreetTargets.*
FROM IC_StreetTargets
WHERE (((IC_StreetTargets.BuildModelIC)=True) AND ((IC_StreetTargets.Evaluation)<>"N" And (IC_StreetTargets.Evaluation)<>"L")) OR (((IC_StreetTargets.Constructed)>0));


_IC_00:clear mdl_DirSC_NGTOs
----------
DELETE mdl_DirSC_NGTOs.*
FROM mdl_DirSC_NGTOs;


_IC_00:clear_excess_parking
----------
DELETE [_tbl_excess_parking].*
FROM _tbl_excess_parking;


_IC_00:clear_excess_roof
----------
DELETE [_tbl_excess_roof].*
FROM _tbl_excess_roof;


_IC_00:clear_ICT_Drywell
----------
UPDATE mdl_ic_DryWell_ac SET mdl_ic_DryWell_ac.SqFt = 0, mdl_ic_DryWell_ac.Comment = "OLD ICT", mdl_ic_DryWell_ac.assumeKey = "DELE"
WHERE (((mdl_ic_DryWell_ac.assumeKey)="ICT"));


_IC_00:clear_ICT_GrnRoof
----------
UPDATE mdl_ic_GrnRoof_ac SET mdl_ic_GrnRoof_ac.SqFt = 0, mdl_ic_GrnRoof_ac.Comment = "OLD ICT", mdl_ic_GrnRoof_ac.assumeKey = "DELE"
WHERE (((mdl_ic_GrnRoof_ac.assumeKey)="ICT"));


_IC_00:clear_ICT_Infilt
----------
UPDATE mdl_ic_Infilt_ac SET mdl_ic_Infilt_ac.SqFt = 0, mdl_ic_Infilt_ac.Comment = "OLD ICT", mdl_ic_Infilt_ac.assumeKey = "DELE"
WHERE (((mdl_ic_Infilt_ac.assumeKey)="ICT"));


_IC_00:clear_ICT_NgtoRedir
----------
UPDATE mdl_ic_NgtoRedir_ac SET mdl_ic_NgtoRedir_ac.SurfSCID = -1, mdl_ic_NgtoRedir_ac.ngto = "DELETE";


_IC_00:clear_ICT_SWplnt
----------
UPDATE mdl_ic_SWPlnt_ac SET mdl_ic_SWPlnt_ac.SqFt = 0, mdl_ic_SWPlnt_ac.Comment = "OLD ICT", mdl_ic_SWPlnt_ac.assumeKey = "DELE"
WHERE (((mdl_ic_SWPlnt_ac.assumeKey)="ICT"));


_IC_00:clear_ICT_Veg
----------
UPDATE mdl_ic_DiscoVeg_ac SET mdl_ic_DiscoVeg_ac.SqFt = 0, mdl_ic_DiscoVeg_ac.Comment = "OLD ICT", mdl_ic_DiscoVeg_ac.assumeKey = "DELE"
WHERE (((mdl_ic_DiscoVeg_ac.assumeKey)="ICT"));


_IC_00_Clear_IC_ParkEx
----------
DELETE ICEXPark.*
FROM ICEXPark;


_IC_00_Clear_IC_RoofEx
----------
DELETE ICExRoof.*
FROM ICExRoof;


_IC_00_ClearICTargetPark
----------
DELETE ICTargetPark.*
FROM ICTargetPark;


_IC_00_ClearICTargetRoof
----------
DELETE ICTargetRoof.*
FROM ICTargetRoof;


_IC_01:mdl_DirSC_NGTOs populate
----------
INSERT INTO mdl_DirSC_NGTOs ( ParcelID, DivideID, NGTOSan, NGTOStorm, NGTODirect )
SELECT mdl_DirSC_ac.ParcelID, mdl_DirSC_ac.DivideID, mdl_DirSC_ac.NGTOSan, mdl_DirSC_ac.NGTOStorm, mdl_DirSC_ac.NGTOSan AS NGTODirect
FROM mdl_DirSC_ac;


_IC_01_Populate_Targets_park
----------
INSERT INTO ICTargetPark ( Mst_targetMIID, PkAreaFtEx, ParcelID, DivideID, PkAreaFt, OrigTarget_Pk )
SELECT [_filter_parking].MAPINFO_ID, [_filter_parking].EXPkAreaFt, mdl_dirsc_ac.ParcelID, mdl_dirsc_ac.DivideID, mdl_dirsc_ac.PkAreaFt, IIf(IsNull([ParkingTargetArea]),0,[ParkingTargetArea]) AS nzParking
FROM mdl_dirsc_ac INNER JOIN _filter_parking ON (mdl_dirsc_ac.ParcelID = [_filter_parking].ParcelID) AND (mdl_dirsc_ac.DivideID = [_filter_parking].DivideID)
WHERE (((IIf(IsNull([ParkingTargetArea]),0,[ParkingTargetArea]))<>0));


_IC_01_Populate_Targets_Roof
----------
INSERT INTO ICTargetRoof ( Mst_targetMIID, RfAreaFtEx, ParcelID, DivideID, RfAreaFt, OrigTarget_Rf )
SELECT [_filter_roof].MAPINFO_ID, [_filter_roof].EXRfAreaFt, mdl_dirsc_ac.ParcelID, mdl_dirsc_ac.DivideID, mdl_dirsc_ac.RfAreaFt, IIf(IsNull([EXRfAreaFt]),0,[EXRfAreaFt]) AS nzRoof
FROM mdl_dirsc_ac INNER JOIN _filter_roof ON (mdl_dirsc_ac.ParcelID = [_filter_roof].ParcelID) AND (mdl_dirsc_ac.DivideID = [_filter_roof].DivideID)
WHERE (((IIf(IsNull([EXRfAreaFt]),0,[EXRfAreaFt]))<>0))
ORDER BY mdl_dirsc_ac.ParcelID, mdl_dirsc_ac.DivideID;


_IC_02:mdl_DirSC_NGTOs Fold-in NGTOStorm
----------
UPDATE mdl_DirSC_NGTOs SET mdl_DirSC_NGTOs.NGTODirect = [NGTOStorm]
WHERE (((mdl_DirSC_NGTOs.NGTOStorm)<>"0"));


_IC_02:Park_Drywell
----------
INSERT INTO mdl_ic_DryWell_ac ( ParcelID, DivideID, RoofRPark, assumeKey, TimeFrame, ApplyAreaTF, SqFt, Comment, AppendDate )
SELECT [_IC_ParkingToDrywell].ParcelID, [_IC_ParkingToDrywell].DivideID, [_IC_ParkingToDrywell].RorP, [_IC_ParkingToDrywell].AssumeKey, [_IC_ParkingToDrywell].TF, [_IC_ParkingToDrywell].ApplyTF, [_IC_ParkingToDrywell].SqFt_Drywell, [_IC_ParkingToDrywell].C, [_IC_ParkingToDrywell].AppendDate
FROM _IC_ParkingToDrywell;


_IC_02:Park_Infilt
----------
INSERT INTO mdl_ic_Infilt_ac ( ParcelID, DivideID, RoofRPark, assumeKey, TimeFrame, ApplyAreaTF, SqFt, Comment, AppendDate )
SELECT [_IC_ParkingToBioRetention].ParcelID, [_IC_ParkingToBioRetention].DivideID, [_IC_ParkingToBioRetention].RorP, [_IC_ParkingToBioRetention].AssumeKey, [_IC_ParkingToBioRetention].TF, [_IC_ParkingToBioRetention].ApplyTF, [_IC_ParkingToBioRetention].SqFt_Bioret, [_IC_ParkingToBioRetention].C, [_IC_ParkingToBioRetention].AppendDate
FROM _IC_ParkingToBioRetention;


_IC_02:Park_Veg_Surface
----------
INSERT INTO mdl_ic_DiscoVeg_ac ( ParcelID, DivideID, RoofRPark, assumeKey, TimeFrame, ApplyAreaTF, SqFt, Effectiveness, Comment, AppendDate )
SELECT [_IC_ParkingToSurface].ParcelID, [_IC_ParkingToSurface].DivideID, [_IC_ParkingToSurface].RorP, [_IC_ParkingToSurface].AssumeKey, [_IC_ParkingToSurface].TF, [_IC_ParkingToSurface].ApplyTF, [_IC_ParkingToSurface].SqFt_Surf, 0 AS Expr1, [_IC_ParkingToSurface].C, [_IC_ParkingToSurface].AppendDate
FROM _IC_ParkingToSurface;


_IC_02:Park_Veg_SurfaceStreetOnly
----------
INSERT INTO mdl_ic_DiscoVeg_ac ( ParcelID, DivideID, RoofRPark, assumeKey, TimeFrame, ApplyAreaTF, SqFt, Effectiveness, Comment, AppendDate )
SELECT [_IC_ParkingToSurfaceStreetOnly].ParcelID, [_IC_ParkingToSurfaceStreetOnly].DivideID, [_IC_ParkingToSurfaceStreetOnly].RorP, [_IC_ParkingToSurfaceStreetOnly].AssumeKey, [_IC_ParkingToSurfaceStreetOnly].TF, [_IC_ParkingToSurfaceStreetOnly].ApplyTF, [_IC_ParkingToSurfaceStreetOnly].SQFt_stOnlyFract, 0 AS Expr1, [_IC_ParkingToSurfaceStreetOnly].C, [_IC_ParkingToSurfaceStreetOnly].AppendDate
FROM _IC_ParkingToSurfaceStreetOnly;


_IC_02:Roof_Drywell
----------
INSERT INTO mdl_ic_DryWell_ac ( ParcelID, DivideID, RoofRPark, assumeKey, TimeFrame, ApplyAreaTF, SqFt, Comment, AppendDate )
SELECT [_IC_RoofToDrywell].ParcelID, [_IC_RoofToDrywell].DivideID, [_IC_RoofToDrywell].RorP, [_IC_RoofToDrywell].AssumeKey, [_IC_RoofToDrywell].TF, [_IC_RoofToDrywell].ApplyTF, [_IC_RoofToDrywell].SqFt_Drywell, [_IC_RoofToDrywell].C, [_IC_RoofToDrywell].AppendDate
FROM _IC_RoofToDrywell;


_IC_02:Roof_EcoRoof
----------
INSERT INTO mdl_ic_DiscoVeg_ac ( ParcelID, DivideID, RoofRPark, assumeKey, TimeFrame, ApplyAreaTF, SqFt, Effectiveness, Comment, AppendDate )
SELECT [_IC_RoofToEcoRoof].ParcelID, [_IC_RoofToEcoRoof].DivideID, [_IC_RoofToEcoRoof].RorP, [_IC_RoofToEcoRoof].AssumeKey, [_IC_RoofToEcoRoof].TF, [_IC_RoofToEcoRoof].ApplyTF, [_IC_RoofToEcoRoof].SqFt_Eco, [_Globals].EcoRoofEff, [_IC_RoofToEcoRoof].C, [_IC_RoofToEcoRoof].AppendDate
FROM _IC_RoofToEcoRoof, _Globals;


_IC_02:Roof_Infilt
----------
INSERT INTO mdl_ic_Infilt_ac ( ParcelID, DivideID, RoofRPark, assumeKey, TimeFrame, ApplyAreaTF, SqFt, Comment, AppendDate )
SELECT [_IC_RoofToBioRetention].ParcelID, [_IC_RoofToBioRetention].DivideID, [_IC_RoofToBioRetention].RorP, [_IC_RoofToBioRetention].AssumeKey, [_IC_RoofToBioRetention].TF, [_IC_RoofToBioRetention].ApplyTF, [_IC_RoofToBioRetention].SqFt_Bioret, [_IC_RoofToBioRetention].C, [_IC_RoofToBioRetention].AppendDate
FROM _IC_RoofToBioRetention
WHERE ((([_IC_RoofToBioRetention].SqFt_Bioret)>0));


_IC_02:Roof_Planter
----------
INSERT INTO mdl_ic_SWPlnt_ac ( ParcelID, DivideID, RoofRPark, assumeKey, TimeFrame, ApplyAreaTF, SqFt, Comment, AppendDate )
SELECT [_IC_RoofToPlanter].ParcelID, [_IC_RoofToPlanter].DivideID, [_IC_RoofToPlanter].RorP, [_IC_RoofToPlanter].AssumeKey, [_IC_RoofToPlanter].TF, [_IC_RoofToPlanter].ApplyTF, [_IC_RoofToPlanter].SqFt_Plntr, [_IC_RoofToPlanter].C, [_IC_RoofToPlanter].AppendDate
FROM _IC_RoofToPlanter;


_IC_02:Roof_Veg
----------
INSERT INTO mdl_ic_DiscoVeg_ac ( ParcelID, DivideID, RoofRPark, assumeKey, TimeFrame, ApplyAreaTF, SqFt, Effectiveness, Comment, AppendDate )
SELECT [_IC_RoofToVeg].ParcelID, [_IC_RoofToVeg].DivideID, [_IC_RoofToVeg].RorP, [_IC_RoofToVeg].AssumeKey, [_IC_RoofToVeg].TF, [_IC_RoofToVeg].ApplyTF, [_IC_RoofToVeg].SqFt_Veg, 0.7 AS Expr1, [_IC_RoofToVeg].C, [_IC_RoofToVeg].AppendDate
FROM _IC_RoofToVeg;


_IC_02:Surf_NGTOREDIR
----------
INSERT INTO mdl_ic_NgtoRedir_ac ( SurfSCID, ngto )
SELECT StreetTargets_ac.SurfSCID, StreetTargets_ac.XPSWMM_Name
FROM StreetTargets_ac;


_IC_02_ICEXPark_Populate
----------
INSERT INTO ICEXPark ( ParcelID, DivideID, ParkingControlled, Veg, Dry, Bio, Pla )
SELECT [_union_parking_EX_ic].ParcelID, [_union_parking_EX_ic].DivideID, Sum([_union_parking_EX_ic].ParkingControlled) AS ParkingControlled, Sum([_union_parking_EX_ic].Veg) AS Veg, Sum([_union_parking_EX_ic].Dry) AS Dry, Sum([_union_parking_EX_ic].Bio) AS Bio, Sum([_union_parking_EX_ic].Pla) AS Pla
FROM _union_parking_EX_ic
GROUP BY [_union_parking_EX_ic].ParcelID, [_union_parking_EX_ic].DivideID;


_IC_02_ICExRoof_Populate
----------
INSERT INTO ICEXRoof ( ParcelID, DivideID, RoofControlled, Veg, Dry, Bio, Pla, Grn )
SELECT [_union_roof_EX_ic].ParcelID, [_union_roof_EX_ic].DivideID, Sum([_union_roof_EX_ic].RoofControlled) AS RoofControlled, Sum([_union_roof_EX_ic].Veg) AS Veg, Sum([_union_roof_EX_ic].Dry) AS Dry, Sum([_union_roof_EX_ic].Bio) AS Bio, Sum([_union_roof_EX_ic].Pla) AS Pla, Sum([_union_roof_EX_ic].Grn) AS Grn
FROM _union_roof_EX_ic
GROUP BY [_union_roof_EX_ic].ParcelID, [_union_roof_EX_ic].DivideID;


_IC_03:AppendExcessParking
----------
INSERT INTO _tbl_excess_parking ( ParcelID, DivideID, Veg, Dry, Bio, Pla, excess )
SELECT [_excess_parking].ParcelID, [_excess_parking].DivideID, [_excess_parking].Veg, [_excess_parking].Dry, [_excess_parking].Bio, [_excess_parking].Pla, [_excess_parking].excess
FROM _excess_parking;


_IC_03:AppendExcessRoof
----------
INSERT INTO _tbl_excess_roof ( ParcelID, DivideID, RoofControlled, Veg, Dry, Bio, Pla, Grn, RfAreaFt, excess )
SELECT [_excess_roof].ParcelID, [_excess_roof].DivideID, [_excess_roof].RoofControlled, [_excess_roof].Veg, [_excess_roof].Dry, [_excess_roof].Bio, [_excess_roof].Pla, [_excess_roof].Grn, [_excess_roof].RfAreaFt, [_excess_roof].excess
FROM _excess_roof;


_IC_03_ICTargetPark_OtherIC
----------
UPDATE ICTargetPark INNER JOIN ICEXPark ON (ICTargetPark.DivideID = ICEXPark.DivideID) AND (ICTargetPark.ParcelID = ICEXPark.ParcelID) SET ICTargetPark.OtherIC_Pk = [parkingControlled];


_IC_03_ICTargetRoof_OtherIC
----------
UPDATE ICTargetRoof INNER JOIN ICExRoof ON (ICTargetRoof.ParcelID = ICExRoof.ParcelID) AND (ICTargetRoof.DivideID = ICExRoof.DivideID) SET ICTargetRoof.OtherIC_Rf = [roofControlled];


_IC_04:correct_ICT_P_drywell
----------
UPDATE _tbl_excess_parking INNER JOIN mdl_ic_DryWell_ac ON ([_tbl_excess_parking].DivideID = mdl_ic_DryWell_ac.DivideID) AND ([_tbl_excess_parking].ParcelID = mdl_ic_DryWell_ac.ParcelID) SET mdl_ic_DryWell_ac.SqFt = 0, mdl_ic_DryWell_ac.assumeKey = "DELE"
WHERE (((mdl_ic_DryWell_ac.assumeKey)="ICT") AND ((mdl_ic_DryWell_ac.RoofRPark)="P") AND (([_tbl_excess_parking].excess)>10));


_IC_04:correct_ICT_P_infilt
----------
UPDATE _tbl_excess_parking INNER JOIN mdl_ic_Infilt_ac ON ([_tbl_excess_parking].DivideID = mdl_ic_Infilt_ac.DivideID) AND ([_tbl_excess_parking].ParcelID = mdl_ic_Infilt_ac.ParcelID) SET mdl_ic_Infilt_ac.SqFt = 0, mdl_ic_Infilt_ac.assumeKey = "DELE"
WHERE (((mdl_ic_Infilt_ac.assumeKey)="ICT") AND ((mdl_ic_Infilt_ac.RoofRPark)="P") AND (([_tbl_excess_parking].excess)>10));


_IC_04:correct_ICT_P_veg
----------
UPDATE _tbl_excess_parking INNER JOIN mdl_ic_DiscoVeg_ac ON ([_tbl_excess_parking].DivideID = mdl_ic_DiscoVeg_ac.DivideID) AND ([_tbl_excess_parking].ParcelID = mdl_ic_DiscoVeg_ac.ParcelID) SET mdl_ic_DiscoVeg_ac.SqFt = 0, mdl_ic_DiscoVeg_ac.assumeKey = "DELE"
WHERE (((mdl_ic_DiscoVeg_ac.assumeKey)="ICT") AND ((mdl_ic_DiscoVeg_ac.RoofRPark)="P") AND (([_tbl_excess_parking].excess)>10));


_IC_04:correct_ICT_R_drywell
----------
UPDATE _tbl_excess_roof INNER JOIN mdl_ic_DryWell_ac ON ([_tbl_excess_roof].DivideID = mdl_ic_DryWell_ac.DivideID) AND ([_tbl_excess_roof].ParcelID = mdl_ic_DryWell_ac.ParcelID) SET mdl_ic_DryWell_ac.SqFt = 0, mdl_ic_DryWell_ac.assumeKey = "DELE"
WHERE (((mdl_ic_DryWell_ac.assumeKey)="ICT") AND ((mdl_ic_DryWell_ac.RoofRPark)="R") AND (([_tbl_excess_roof].excess)>10));


_IC_04:correct_ICT_R_GrnRoof
----------
UPDATE mdl_ic_GrnRoof_ac INNER JOIN _tbl_excess_roof ON (mdl_ic_GrnRoof_ac.DivideID = [_tbl_excess_roof].DivideID) AND (mdl_ic_GrnRoof_ac.ParcelID = [_tbl_excess_roof].ParcelID) SET mdl_ic_GrnRoof_ac.SqFt = 0, mdl_ic_GrnRoof_ac.assumeKey = "DELE"
WHERE (((mdl_ic_GrnRoof_ac.assumeKey)="ICT") AND ((mdl_ic_GrnRoof_ac.RoofRPark)="R") AND (([_tbl_excess_roof].excess)>10));


_IC_04:correct_ICT_R_infilt
----------
UPDATE _tbl_excess_roof INNER JOIN mdl_ic_Infilt_ac ON ([_tbl_excess_roof].DivideID = mdl_ic_Infilt_ac.DivideID) AND ([_tbl_excess_roof].ParcelID = mdl_ic_Infilt_ac.ParcelID) SET mdl_ic_Infilt_ac.SqFt = 0, mdl_ic_Infilt_ac.assumeKey = "DELE"
WHERE (((mdl_ic_Infilt_ac.assumeKey)="ICT") AND ((mdl_ic_Infilt_ac.RoofRPark)="R") AND (([_tbl_excess_roof].excess)>10));


_IC_04:correct_ICT_R_SWplnt
----------
UPDATE mdl_ic_SWPlnt_ac INNER JOIN _tbl_excess_roof ON (mdl_ic_SWPlnt_ac.DivideID = [_tbl_excess_roof].DivideID) AND (mdl_ic_SWPlnt_ac.ParcelID = [_tbl_excess_roof].ParcelID) SET mdl_ic_SWPlnt_ac.SqFt = 0, mdl_ic_SWPlnt_ac.assumeKey = "DELE"
WHERE (((mdl_ic_SWPlnt_ac.assumeKey)="ICT") AND ((mdl_ic_SWPlnt_ac.RoofRPark)="R") AND (([_tbl_excess_roof].excess)>10));


_IC_04:correct_ICT_R_veg
----------
UPDATE _tbl_excess_roof INNER JOIN mdl_ic_DiscoVeg_ac ON ([_tbl_excess_roof].DivideID = mdl_ic_DiscoVeg_ac.DivideID) AND ([_tbl_excess_roof].ParcelID = mdl_ic_DiscoVeg_ac.ParcelID) SET mdl_ic_DiscoVeg_ac.SqFt = 0, mdl_ic_DiscoVeg_ac.assumeKey = "DELE"
WHERE (((mdl_ic_DiscoVeg_ac.assumeKey)="ICT") AND ((mdl_ic_DiscoVeg_ac.RoofRPark)="R") AND (([_tbl_excess_roof].excess)>10));


_IC_04_ICTargetPark_FinalTarget
----------
UPDATE ICTargetPark SET ICTargetPark.FinalTarget_Pk = IIf([origtarget_pk]<(IIf([pkareaftex]<[pkareaft],[pkareaftex],[pkareaft])-[otheriC_pk]),[origtarget_pk],(IIf([pkareaftex]<[pkareaft],[pkareaftex],[pkareaft])-[otheriC_pk]));


_IC_04_ICTargetRoof_FinalTarget
----------
UPDATE ICTargetRoof SET ICTargetRoof.FinalTarget_Rf = IIf([rfareaftex]<[Rfareaft],[Rfareaftex],[rfareaft])-[otheriC_rf];


_IC_05:dele_drywell
----------
DELETE mdl_ic_DryWell_ac.SqFt, mdl_ic_DryWell_ac.assumeKey
FROM mdl_ic_DryWell_ac
WHERE (((mdl_ic_DryWell_ac.SqFt)<0)) OR (((mdl_ic_DryWell_ac.assumeKey)="DELE"));


_IC_05:dele_grnroof
----------
DELETE mdl_ic_GrnRoof_ac.SqFt, mdl_ic_GrnRoof_ac.assumeKey
FROM mdl_ic_GrnRoof_ac
WHERE (((mdl_ic_GrnRoof_ac.SqFt)<0)) OR (((mdl_ic_GrnRoof_ac.assumeKey)="DELE"));


_IC_05:dele_infilt
----------
DELETE mdl_ic_Infilt_ac.SqFt, mdl_ic_Infilt_ac.assumeKey
FROM mdl_ic_Infilt_ac
WHERE (((mdl_ic_Infilt_ac.SqFt)<0)) OR (((mdl_ic_Infilt_ac.assumeKey)="DELE"));


_IC_05:dele_NgtoRedir
----------
DELETE mdl_ic_NgtoRedir_ac.ngto
FROM mdl_ic_NgtoRedir_ac
WHERE (((mdl_ic_NgtoRedir_ac.ngto)="DELETE"));


_IC_05:dele_SWPlnt
----------
DELETE mdl_ic_SWPlnt_ac.SqFt, mdl_ic_SWPlnt_ac.assumeKey
FROM mdl_ic_SWPlnt_ac
WHERE (((mdl_ic_SWPlnt_ac.SqFt)<0)) OR (((mdl_ic_SWPlnt_ac.assumeKey)="DELE"));


_IC_05:dele_veg
----------
DELETE mdl_ic_DiscoVeg_ac.SqFt, mdl_ic_DiscoVeg_ac.assumeKey
FROM mdl_ic_DiscoVeg_ac
WHERE (((mdl_ic_DiscoVeg_ac.SqFt)<0)) OR (((mdl_ic_DiscoVeg_ac.assumeKey)="DELE"));


_IC_ParkingToBioRetention
----------
SELECT [_mdl_parkingtargets].ParcelID, [_mdl_parkingtargets].DivideID, "P" AS RorP, "ICT" AS AssumeKey, "FU" AS TF, "EX" AS ApplyTF, [_mdl_parkingtargets].SqFt_Bioret, "EICP Alt" AS C, Format$(Date(),"yyyymmdd") AS AppendDate
FROM _mdl_parkingtargets
WHERE ((([_mdl_parkingtargets].SqFt_Bioret)>435.6) AND (([_mdl_parkingtargets].Controllable)<>"N") AND (([_mdl_parkingtargets].FractionControllable)>0) AND (([_mdl_parkingtargets].Difficulty)>0));


_IC_ParkingToDrywell
----------
SELECT [_mdl_parkingtargets].ParcelID, [_mdl_parkingtargets].DivideID, "P" AS RorP, "ICT" AS AssumeKey, "FU" AS TF, "EX" AS ApplyTF, [_mdl_parkingtargets].SqFt_Drywell, "EICP Alt" AS C, Format$(Date(),"yyyymmdd") AS AppendDate
FROM _mdl_parkingtargets
WHERE ((([_mdl_parkingtargets].SqFt_Drywell)>435.6) AND (([_mdl_parkingtargets].Controllable)<>"N") AND (([_mdl_parkingtargets].FractionControllable)>0) AND (([_mdl_parkingtargets].Difficulty)>0));


_IC_ParkingToSurface
----------
SELECT [_mdl_parkingtargets].ParcelID, [_mdl_parkingtargets].DivideID, "P" AS RorP, "ICT" AS AssumeKey, "FU" AS TF, "EX" AS ApplyTF, [_mdl_parkingtargets].SqFt_Surf, "EICP Alt" AS C, Format$(Date(),"yyyymmdd") AS AppendDate
FROM _mdl_parkingtargets
WHERE ((([_mdl_parkingtargets].SqFt_Surf)>100) AND (([_mdl_parkingtargets].Controllable)<>"N") AND (([_mdl_parkingtargets].Inlets)=0) AND (([_mdl_parkingtargets].FractionControllable)>0 And ([_mdl_parkingtargets].FractionControllable)<1) AND (([_mdl_parkingtargets].Difficulty)>0));


_IC_ParkingToSurfaceStreetOnly
----------
SELECT [_mdl_parkingtargets].ParcelID, [_mdl_parkingtargets].DivideID, "P" AS RorP, "ICT" AS AssumeKey, "FU" AS TF, "EX" AS ApplyTF, [SQFt_stOnly]*(1-[FractionControllable]) AS SQFt_stOnlyFract, "EICP Alt" AS C, Format$(Date(),"yyyymmdd") AS AppendDate
FROM _mdl_parkingtargets
WHERE ((([SQFt_stOnly]*(1-[FractionControllable]))>100) AND (([_mdl_parkingtargets].Controllable)="N") AND (([_mdl_parkingtargets].Inlets)=0) AND (([_mdl_parkingtargets].Difficulty)=0))
ORDER BY [SQFt_stOnly]*(1-[FractionControllable]);


_IC_RoofToBioRetention
----------
SELECT [_mdl_RoofTargets].ParcelID, [_mdl_RoofTargets].DivideID, "R" AS RorP, "ICT" AS AssumeKey, "FU" AS TF, "EX" AS ApplyTF, [_mdl_RoofTargets].SqFt_Bioret, "EICP Alt" AS C, Format$(Date(),"yyyymmdd") AS AppendDate
FROM _mdl_RoofTargets
WHERE ((([_mdl_RoofTargets].DStoBioretention)>0) AND (([_mdl_RoofTargets].ExternalDS)=-1) AND (([_mdl_RoofTargets].DSTotal)>0) AND (([EXRfAreaFt]-[EXICFTroof])>435.6) AND (([_mdl_RoofTargets].Difficulty)>0));


_IC_RoofToDrywell
----------
SELECT [_mdl_RoofTargets].ParcelID, [_mdl_RoofTargets].DivideID, "R" AS RorP, "ICT" AS AssumeKey, "FU" AS TF, "EX" AS ApplyTF, [_mdl_RoofTargets].SqFt_Drywell, "EICP Alt" AS C, Format$(Date(),"yyyymmdd") AS AppendDate
FROM _mdl_RoofTargets
WHERE ((([DStoBioretention]+[DStoPlanter]+[DStoDrywell])>0) AND (([_mdl_RoofTargets].ExternalDS)=-1) AND (([_mdl_RoofTargets].DSTotal)>0) AND (([EXRfAreaFt]-[EXICFTroof])>435.6) AND (([_mdl_RoofTargets].Difficulty)>0));


_IC_RoofToEcoRoof
----------
SELECT [_mdl_RoofTargets].ParcelID, [_mdl_RoofTargets].DivideID, "R" AS RorP, "ICT" AS AssumeKey, "FU" AS TF, "EX" AS ApplyTF, [_mdl_RoofTargets].SqFt_Eco, "EICP Eco R" AS C, Format$(Date(),"yyyymmdd") AS AppendDate
FROM _mdl_RoofTargets
WHERE ((([_mdl_RoofTargets].DStoEcoroof)>0) AND (([EXRfAreaFt]-[EXICFTroof])>0) AND (([_mdl_RoofTargets].Difficulty)>0));


_IC_RoofToPlanter
----------
SELECT [_mdl_RoofTargets].ParcelID, [_mdl_RoofTargets].DivideID, "R" AS RorP, "ICT" AS AssumeKey, "FU" AS TF, "EX" AS ApplyTF, [_mdl_RoofTargets].SqFt_Plntr, "EICP Alt" AS C, Format$(Date(),"yyyymmdd") AS AppendDate
FROM _mdl_RoofTargets
WHERE ((([_mdl_RoofTargets].DStoPlanter)>0) AND (([_mdl_RoofTargets].ExternalDS)=-1) AND (([EXRfAreaFt]-[EXICFTroof])>0) AND (([_mdl_RoofTargets].Difficulty)>0));


_IC_RoofToVeg
----------
SELECT [_mdl_RoofTargets].ParcelID, [_mdl_RoofTargets].DivideID, "R" AS RorP, "ICT" AS AssumeKey, "FU" AS TF, "EX" AS ApplyTF, [_mdl_RoofTargets].SqFt_Veg, "EICP Alt" AS C, Format$(Date(),"yyyymmdd") AS AppendDate
FROM _mdl_RoofTargets
WHERE ((([_mdl_RoofTargets].DStoVeg)>0) AND (([_mdl_RoofTargets].ExternalDS)=-1) AND (([EXRfAreaFt]-[EXICFTroof])>0) AND (([_mdl_RoofTargets].Difficulty)>0));


_IC_StreetControls_NGTORedir
----------
SELECT StreetTargets_ac.SurfSCID, Trim$([XPSWMM_Name]) AS NGTORedir
FROM StreetTargets_ac
WHERE (((StreetTargets_ac.Evaluation)<>"N"));


_mdl_ParkingTargets
----------
SELECT ICTargetPark.Mst_targetMIID, [_filter_parking].*, CLng([finalTarget_pk]*[FractionControllable]*(1.2-[Difficulty]*0.2)) AS SqFt_Bioret, CLng(0) AS SqFt_Drywell, CLng([finalTarget_pk]*(1-[FractionControllable])*(1.2-[Difficulty]*0.2)) AS SqFt_Surf, ICTargetPark.FinalTarget_Pk AS SqFt_StOnly
FROM (mdl_dirsc_ac INNER JOIN _filter_parking ON (mdl_dirsc_ac.DivideID = [_filter_parking].DivideID) AND (mdl_dirsc_ac.ParcelID = [_filter_parking].ParcelID)) INNER JOIN ICTargetPark ON [_filter_parking].MAPINFO_ID = ICTargetPark.Mst_targetMIID
WHERE ((([_filter_parking].Source)<>"FlowReductionIC"))

UNION SELECT ICTargetPark.Mst_targetMIID, [_filter_parking].*, CLng(0) AS SqFt_Bioret, CLng([finalTarget_pk]*[Fraction2DryWell]*(1.2-[Difficulty]*0.2)) AS SqFt_Drywell, CLng(0) AS SqFt_Surf, ICTargetPark.FinalTarget_Pk AS SqFt_StOnly
FROM (mdl_dirsc_ac INNER JOIN _filter_parking ON (mdl_dirsc_ac.DivideID = [_filter_parking].DivideID) AND (mdl_dirsc_ac.ParcelID = [_filter_parking].ParcelID)) INNER JOIN ICTargetPark ON [_filter_parking].MAPINFO_ID = ICTargetPark.Mst_targetMIID
WHERE ((([_filter_parking].Source)="FlowReductionIC"));


_mdl_roofTargets
----------
SELECT [_filter_roof].*, CLng([finalTarget_rf]*[FractionDrained]*[DStoBioretention]/[DSTotal]*(1.2-[Difficulty]*0.2)) AS SqFt_Bioret, CLng([finaltarget_rf]*[FractionDrained]*([DStoDryWell]/[DSTotal])*(1.2-[Difficulty]*0.2)) AS SqFt_Drywell, CLng([finaltarget_rf]*[FractionDrained]*([DStoVeg]/[DSTotal])*(1.2-[Difficulty]*0.2)) AS SqFt_Veg, CLng([finalTarget_rf]*[FractionDrained]*[DStoEcoroof]/[DSTotal]*(1.2-[Difficulty]*0.2)) AS SqFt_Eco, CLng([finaltarget_rf]*[FractionDrained]*[DStoPlanter]/[DSTotal]*(1.2-[Difficulty]*0.2)) AS SqFt_Plntr
FROM (mdl_dirsc_ac INNER JOIN _filter_roof ON (mdl_dirsc_ac.ParcelID = [_filter_roof].ParcelID) AND (mdl_dirsc_ac.DivideID = [_filter_roof].DivideID)) INNER JOIN ICTargetRoof ON ([_filter_roof].DivideID = ICTargetRoof.DivideID) AND ([_filter_roof].ParcelID = ICTargetRoof.ParcelID);


_union_parking_EX_ic
----------
SELECT mdl_ic_Drywell_ac.ParcelID, mdl_ic_Drywell_ac.DivideID, Sum(mdl_ic_Drywell_ac.SqFt) AS ParkingControlled, Sum(0) AS Veg, Sum(mdl_ic_Drywell_ac.SqFt) AS Dry, Sum(0) AS Bio, Sum(0) AS Pla
FROM mdl_ic_Drywell_ac
WHERE (((mdl_ic_Drywell_ac.RoofRPark)="P") and (AssumeKey <>"ICT")and (AssumeKey <>"_SAN") and (ApplyAreaTF ="EX"))
GROUP BY mdl_ic_Drywell_ac.ParcelID, mdl_ic_Drywell_ac.DivideID;

UNION SELECT mdl_ic_DiscoVeg_ac.ParcelID, mdl_ic_DiscoVeg_ac.DivideID, Sum(mdl_ic_DiscoVeg_ac.SqFt) AS ParkingControlled, Sum(mdl_ic_DiscoVeg_ac.SqFt) AS Veg, Sum(0) AS Dry, Sum(0) AS Bio, Sum(0) AS Pla
FROM mdl_ic_DiscoVeg_ac
WHERE (((mdl_ic_DiscoVeg_ac.RoofRPark)="P") and (AssumeKey <>"ICT")and (AssumeKey <>"_SAN") and (ApplyAreaTF ="EX"))
GROUP BY mdl_ic_DiscoVeg_ac.ParcelID, mdl_ic_DiscoVeg_ac.DivideID;

UNION SELECT mdl_ic_Infilt_ac.ParcelID, mdl_ic_Infilt_ac.DivideID, Sum(mdl_ic_Infilt_ac.SqFt) AS ParkingControlled, Sum(0) AS Veg, Sum(0) AS Dry, Sum(mdl_ic_Infilt_ac.SqFt) AS Bio, Sum(0) AS Pla
FROM mdl_ic_Infilt_ac
WHERE (((mdl_ic_Infilt_ac.RoofRPark)="P") and (AssumeKey <>"ICT")and (AssumeKey <>"_SAN") and (ApplyAreaTF ="EX"))
GROUP BY mdl_ic_Infilt_ac.ParcelID, mdl_ic_Infilt_ac.DivideID;

UNION SELECT mdl_ic_SWplnt_ac.ParcelID, mdl_ic_SWplnt_ac.DivideID, Sum(mdl_ic_SWplnt_ac.SqFt) AS ParkingControlled, Sum(0) AS Veg, Sum(0) AS Dry, Sum(0) AS Bio, Sum(mdl_ic_SWplnt_ac.SqFt) AS Pla
FROM mdl_ic_SWplnt_ac
WHERE (((mdl_ic_SWplnt_ac.RoofRPark)="P") and (AssumeKey <>"ICT")and (AssumeKey <>"_SAN") and (ApplyAreaTF ="EX"))
GROUP BY mdl_ic_SWplnt_ac.ParcelID, mdl_ic_SWplnt_ac.DivideID;


_union_parking_EX_ic_sum
----------
SELECT [_union_parking_EX_ic].ParcelID, [_union_parking_EX_ic].DivideID, Sum([_union_parking_EX_ic].ParkingControlled) AS ParkingControlled, Sum([_union_parking_EX_ic].Veg) AS Veg, Sum([_union_parking_EX_ic].Dry) AS Dry, Sum([_union_parking_EX_ic].Bio) AS Bio, Sum([_union_parking_EX_ic].Pla) AS Pla
FROM _union_parking_EX_ic
GROUP BY [_union_parking_EX_ic].ParcelID, [_union_parking_EX_ic].DivideID;


_union_parking_ics
----------
SELECT mdl_ic_Drywell_ac.ParcelID, mdl_ic_Drywell_ac.DivideID, Sum(mdl_ic_Drywell_ac.SqFt) AS ParkingControlled, Sum(0) AS Veg, Sum(mdl_ic_Drywell_ac.SqFt) AS Dry, Sum(0) AS Bio, Sum(0) AS Pla
FROM mdl_ic_Drywell_ac
WHERE (((mdl_ic_Drywell_ac.RoofRPark)="P") and (AssumeKey <>"ICT")and (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_Drywell_ac.ParcelID, mdl_ic_Drywell_ac.DivideID;

UNION SELECT mdl_ic_DiscoVeg_ac.ParcelID, mdl_ic_DiscoVeg_ac.DivideID, Sum(mdl_ic_DiscoVeg_ac.SqFt) AS ParkingControlled, Sum(mdl_ic_DiscoVeg_ac.SqFt) AS Veg, Sum(0) AS Dry, Sum(0) AS Bio, Sum(0) AS Pla
FROM mdl_ic_DiscoVeg_ac
WHERE (((mdl_ic_DiscoVeg_ac.RoofRPark)="P") and (AssumeKey <>"ICT")and (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_DiscoVeg_ac.ParcelID, mdl_ic_DiscoVeg_ac.DivideID;

UNION SELECT mdl_ic_Infilt_ac.ParcelID, mdl_ic_Infilt_ac.DivideID, Sum(mdl_ic_Infilt_ac.SqFt) AS ParkingControlled, Sum(0) AS Veg, Sum(0) AS Dry, Sum(mdl_ic_Infilt_ac.SqFt) AS Bio, Sum(0) AS Pla
FROM mdl_ic_Infilt_ac
WHERE (((mdl_ic_Infilt_ac.RoofRPark)="P") and (AssumeKey <>"ICT")and (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_Infilt_ac.ParcelID, mdl_ic_Infilt_ac.DivideID;

UNION SELECT mdl_ic_SWplnt_ac.ParcelID, mdl_ic_SWplnt_ac.DivideID, Sum(mdl_ic_SWplnt_ac.SqFt) AS ParkingControlled, Sum(0) AS Veg, Sum(0) AS Dry, Sum(0) AS Bio, Sum(mdl_ic_SWplnt_ac.SqFt) AS Pla
FROM mdl_ic_SWplnt_ac
WHERE (((mdl_ic_SWplnt_ac.RoofRPark)="P") and (AssumeKey <>"ICT")and (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_SWplnt_ac.ParcelID, mdl_ic_SWplnt_ac.DivideID;


_union_parking_ics_post
----------
SELECT mdl_ic_Drywell_ac.ParcelID, mdl_ic_Drywell_ac.DivideID, Sum(mdl_ic_Drywell_ac.SqFt) AS ParkingControlled, Sum(0) AS Veg, Sum(mdl_ic_Drywell_ac.SqFt) AS Dry, Sum(0) AS Bio, Sum(0) AS Pla
FROM mdl_ic_Drywell_ac
WHERE (((mdl_ic_Drywell_ac.RoofRPark)="P") and  (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_Drywell_ac.ParcelID, mdl_ic_Drywell_ac.DivideID;

UNION SELECT mdl_ic_DiscoVeg_ac.ParcelID, mdl_ic_DiscoVeg_ac.DivideID, Sum(mdl_ic_DiscoVeg_ac.SqFt) AS ParkingControlled, Sum(mdl_ic_DiscoVeg_ac.SqFt) AS Veg, Sum(0) AS Dry, Sum(0) AS Bio, Sum(0) AS Pla
FROM mdl_ic_DiscoVeg_ac
WHERE (((mdl_ic_DiscoVeg_ac.RoofRPark)="P") and  (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_DiscoVeg_ac.ParcelID, mdl_ic_DiscoVeg_ac.DivideID;

UNION SELECT mdl_ic_Infilt_ac.ParcelID, mdl_ic_Infilt_ac.DivideID, Sum(mdl_ic_Infilt_ac.SqFt) AS ParkingControlled, Sum(0) AS Veg, Sum(0) AS Dry, Sum(mdl_ic_Infilt_ac.SqFt) AS Bio, Sum(0) AS Pla
FROM mdl_ic_Infilt_ac
WHERE (((mdl_ic_Infilt_ac.RoofRPark)="P") and (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_Infilt_ac.ParcelID, mdl_ic_Infilt_ac.DivideID;

UNION SELECT mdl_ic_SWplnt_ac.ParcelID, mdl_ic_SWplnt_ac.DivideID, Sum(mdl_ic_SWplnt_ac.SqFt) AS ParkingControlled, Sum(0) AS Veg, Sum(0) AS Dry, Sum(0) AS Bio, Sum(mdl_ic_SWplnt_ac.SqFt) AS Pla
FROM mdl_ic_SWplnt_ac
WHERE (((mdl_ic_SWplnt_ac.RoofRPark)="P") and (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_SWplnt_ac.ParcelID, mdl_ic_SWplnt_ac.DivideID;


_union_parking_ics_post_sum
----------
SELECT [_union_parking_ics_post].ParcelID, [_union_parking_ics_post].DivideID, Sum([_union_parking_ics_post].ParkingControlled) AS ParkingControlled, Sum([_union_parking_ics_post].Veg) AS Veg, Sum([_union_parking_ics_post].Dry) AS Dry, Sum([_union_parking_ics_post].Bio) AS Bio, Sum([_union_parking_ics_post].Pla) AS Pla
FROM _union_parking_ics_post
GROUP BY [_union_parking_ics_post].ParcelID, [_union_parking_ics_post].DivideID;


_union_parking_sum
----------
SELECT [_union_parking_ics].ParcelID, [_union_parking_ics].DivideID, Sum([_union_parking_ics].ParkingControlled) AS ParkingControlled, Sum([_union_parking_ics].Veg) AS Veg, Sum([_union_parking_ics].Dry) AS Dry, Sum([_union_parking_ics].Bio) AS Bio, Sum([_union_parking_ics].Pla) AS Pla
FROM _union_parking_ics
GROUP BY [_union_parking_ics].ParcelID, [_union_parking_ics].DivideID;


_union_roof_EX_ic
----------
SELECT mdl_ic_Drywell_ac.ParcelID, mdl_ic_Drywell_ac.DivideID, Sum(mdl_ic_Drywell_ac.SqFt) AS RoofControlled, Sum(0) AS Veg, Sum(mdl_ic_Drywell_ac.SqFt) AS Dry, Sum(0) AS Bio, Sum(0) AS Pla,  Sum(0) AS Grn
FROM mdl_ic_Drywell_ac
WHERE (((mdl_ic_Drywell_ac.RoofRPark)="R")  and (AssumeKey <>"ICT") and (AssumeKey <>"_SAN") and (ApplyAreaTF ="EX"))
GROUP BY mdl_ic_Drywell_ac.ParcelID, mdl_ic_Drywell_ac.DivideID;

UNION SELECT mdl_ic_DiscoVeg_ac.ParcelID, mdl_ic_DiscoVeg_ac.DivideID, Sum(mdl_ic_DiscoVeg_ac.SqFt) AS RoofControlled, Sum(mdl_ic_DiscoVeg_ac.SqFt) AS Veg, Sum(0) AS Dry, Sum(0) AS Bio, Sum(0) AS Pla,  Sum(0) AS Grn
FROM mdl_ic_DiscoVeg_ac
WHERE (((mdl_ic_DiscoVeg_ac.RoofRPark)="R")  and (AssumeKey <>"ICT")and (AssumeKey <>"_SAN")and (ApplyAreaTF ="EX"))
GROUP BY mdl_ic_DiscoVeg_ac.ParcelID, mdl_ic_DiscoVeg_ac.DivideID;

UNION SELECT mdl_ic_Infilt_ac.ParcelID, mdl_ic_Infilt_ac.DivideID, Sum(mdl_ic_Infilt_ac.SqFt) AS RoofControlled, Sum(0) AS Veg, Sum(0) AS Dry, Sum(mdl_ic_Infilt_ac.SqFt) AS Bio, Sum(0) AS Pla,  Sum(0) AS Grn
FROM mdl_ic_Infilt_ac
WHERE (((mdl_ic_Infilt_ac.RoofRPark)="R")  and (AssumeKey <>"ICT")and (AssumeKey <>"_SAN")and (ApplyAreaTF ="EX"))
GROUP BY mdl_ic_Infilt_ac.ParcelID, mdl_ic_Infilt_ac.DivideID;

UNION SELECT mdl_ic_SWplnt_ac.ParcelID, mdl_ic_SWplnt_ac.DivideID, Sum(mdl_ic_SWplnt_ac.SqFt) AS RoofControlled, Sum(0) AS Veg, Sum(0) AS Dry, Sum(0) AS Bio, Sum(mdl_ic_SWplnt_ac.SqFt) AS Pla,  Sum(0) AS Grn
FROM mdl_ic_SWplnt_ac
WHERE (((mdl_ic_SWplnt_ac.RoofRPark)="R")  and (AssumeKey <>"ICT")and (AssumeKey <>"_SAN")and (ApplyAreaTF ="EX"))
GROUP BY mdl_ic_SWplnt_ac.ParcelID, mdl_ic_SWplnt_ac.DivideID;

UNION SELECT mdl_ic_GrnRoof_ac.ParcelID, mdl_ic_GrnRoof_ac.DivideID, Sum(mdl_ic_GrnRoof_ac.SqFt) AS RoofControlled, Sum(0) AS Veg, Sum(0) AS Dry, Sum(0) AS Bio, Sum(0) AS Pla,  Sum(mdl_ic_GrnRoof_ac.SqFt) AS Grn
FROM mdl_ic_GrnRoof_ac
WHERE (((mdl_ic_GrnRoof_ac.RoofRPark)="R")  and (AssumeKey <>"ICT")and (AssumeKey <>"_SAN")and (ApplyAreaTF ="EX"))
GROUP BY mdl_ic_GrnRoof_ac.ParcelID, mdl_ic_GrnRoof_ac.DivideID;


_union_roof_EX_ic_sum
----------
SELECT [_union_roof_EX_ic].ParcelID, [_union_roof_EX_ic].DivideID, Sum([_union_roof_EX_ic].RoofControlled) AS RoofControlled, Sum([_union_roof_EX_ic].Veg) AS Veg, Sum([_union_roof_EX_ic].Dry) AS Dry, Sum([_union_roof_EX_ic].Bio) AS Bio, Sum([_union_roof_EX_ic].Pla) AS Pla, Sum([_union_roof_EX_ic].Grn) AS Grn
FROM _union_roof_EX_ic
GROUP BY [_union_roof_EX_ic].ParcelID, [_union_roof_EX_ic].DivideID;


_union_roof_ics
----------
SELECT mdl_ic_Drywell_ac.ParcelID, mdl_ic_Drywell_ac.DivideID, Sum(mdl_ic_Drywell_ac.SqFt) AS RoofControlled, Sum(0) AS Veg, Sum(mdl_ic_Drywell_ac.SqFt) AS Dry, Sum(0) AS Bio, Sum(0) AS Pla,  Sum(0) AS Grn
FROM mdl_ic_Drywell_ac
WHERE (((mdl_ic_Drywell_ac.RoofRPark)="R")  and (AssumeKey <>"ICT") and (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_Drywell_ac.ParcelID, mdl_ic_Drywell_ac.DivideID;

UNION SELECT mdl_ic_DiscoVeg_ac.ParcelID, mdl_ic_DiscoVeg_ac.DivideID, Sum(mdl_ic_DiscoVeg_ac.SqFt) AS RoofControlled, Sum(mdl_ic_DiscoVeg_ac.SqFt) AS Veg, Sum(0) AS Dry, Sum(0) AS Bio, Sum(0) AS Pla,  Sum(0) AS Grn
FROM mdl_ic_DiscoVeg_ac
WHERE (((mdl_ic_DiscoVeg_ac.RoofRPark)="R")  and (AssumeKey <>"ICT")and (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_DiscoVeg_ac.ParcelID, mdl_ic_DiscoVeg_ac.DivideID;

UNION SELECT mdl_ic_Infilt_ac.ParcelID, mdl_ic_Infilt_ac.DivideID, Sum(mdl_ic_Infilt_ac.SqFt) AS RoofControlled, Sum(0) AS Veg, Sum(0) AS Dry, Sum(mdl_ic_Infilt_ac.SqFt) AS Bio, Sum(0) AS Pla,  Sum(0) AS Grn
FROM mdl_ic_Infilt_ac
WHERE (((mdl_ic_Infilt_ac.RoofRPark)="R")  and (AssumeKey <>"ICT")and (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_Infilt_ac.ParcelID, mdl_ic_Infilt_ac.DivideID;

UNION SELECT mdl_ic_SWplnt_ac.ParcelID, mdl_ic_SWplnt_ac.DivideID, Sum(mdl_ic_SWplnt_ac.SqFt) AS RoofControlled, Sum(0) AS Veg, Sum(0) AS Dry, Sum(0) AS Bio, Sum(mdl_ic_SWplnt_ac.SqFt) AS Pla,  Sum(0) AS Grn
FROM mdl_ic_SWplnt_ac
WHERE (((mdl_ic_SWplnt_ac.RoofRPark)="R")  and (AssumeKey <>"ICT")and (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_SWplnt_ac.ParcelID, mdl_ic_SWplnt_ac.DivideID;

UNION SELECT mdl_ic_GrnRoof_ac.ParcelID, mdl_ic_GrnRoof_ac.DivideID, Sum(mdl_ic_GrnRoof_ac.SqFt) AS RoofControlled, Sum(0) AS Veg, Sum(0) AS Dry, Sum(0) AS Bio, Sum(0) AS Pla,  Sum(mdl_ic_GrnRoof_ac.SqFt) AS Grn
FROM mdl_ic_GrnRoof_ac
WHERE (((mdl_ic_GrnRoof_ac.RoofRPark)="R")  and (AssumeKey <>"ICT")and (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_GrnRoof_ac.ParcelID, mdl_ic_GrnRoof_ac.DivideID;


_union_roof_ics_post
----------
SELECT mdl_ic_Drywell_ac.ParcelID, mdl_ic_Drywell_ac.DivideID, Sum(mdl_ic_Drywell_ac.SqFt) AS RoofControlled, Sum(0) AS Veg, Sum(mdl_ic_Drywell_ac.SqFt) AS Dry, Sum(0) AS Bio, Sum(0) AS Pla,  Sum(0) AS Grn
FROM mdl_ic_Drywell_ac
WHERE (((mdl_ic_Drywell_ac.RoofRPark)="R")  and (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_Drywell_ac.ParcelID, mdl_ic_Drywell_ac.DivideID;

UNION SELECT mdl_ic_DiscoVeg_ac.ParcelID, mdl_ic_DiscoVeg_ac.DivideID, Sum(mdl_ic_DiscoVeg_ac.SqFt) AS RoofControlled, Sum(mdl_ic_DiscoVeg_ac.SqFt) AS Veg, Sum(0) AS Dry, Sum(0) AS Bio, Sum(0) AS Pla,  Sum(0) AS Grn
FROM mdl_ic_DiscoVeg_ac
WHERE (((mdl_ic_DiscoVeg_ac.RoofRPark)="R")  and  (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_DiscoVeg_ac.ParcelID, mdl_ic_DiscoVeg_ac.DivideID;

UNION SELECT mdl_ic_Infilt_ac.ParcelID, mdl_ic_Infilt_ac.DivideID, Sum(mdl_ic_Infilt_ac.SqFt) AS RoofControlled, Sum(0) AS Veg, Sum(0) AS Dry, Sum(mdl_ic_Infilt_ac.SqFt) AS Bio, Sum(0) AS Pla,  Sum(0) AS Grn
FROM mdl_ic_Infilt_ac
WHERE (((mdl_ic_Infilt_ac.RoofRPark)="R")  and  (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_Infilt_ac.ParcelID, mdl_ic_Infilt_ac.DivideID;

UNION SELECT mdl_ic_SWplnt_ac.ParcelID, mdl_ic_SWplnt_ac.DivideID, Sum(mdl_ic_SWplnt_ac.SqFt) AS RoofControlled, Sum(0) AS Veg, Sum(0) AS Dry, Sum(0) AS Bio, Sum(mdl_ic_SWplnt_ac.SqFt) AS Pla,  Sum(0) AS Grn
FROM mdl_ic_SWplnt_ac
WHERE (((mdl_ic_SWplnt_ac.RoofRPark)="R")  and (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_SWplnt_ac.ParcelID, mdl_ic_SWplnt_ac.DivideID;

UNION SELECT mdl_ic_GrnRoof_ac.ParcelID, mdl_ic_GrnRoof_ac.DivideID, Sum(mdl_ic_GrnRoof_ac.SqFt) AS RoofControlled, Sum(0) AS Veg, Sum(0) AS Dry, Sum(0) AS Bio, Sum(0) AS Pla,  Sum(mdl_ic_GrnRoof_ac.SqFt) AS Grn
FROM mdl_ic_GrnRoof_ac
WHERE (((mdl_ic_GrnRoof_ac.RoofRPark)="R")  and (AssumeKey <>"_SAN"))
GROUP BY mdl_ic_GrnRoof_ac.ParcelID, mdl_ic_GrnRoof_ac.DivideID;


_union_roof_ics_post_sum
----------
SELECT [_union_roof_ics_post].ParcelID, [_union_roof_ics_post].DivideID, Sum([_union_roof_ics_post].RoofControlled) AS RoofControlled, Sum([_union_roof_ics_post].Veg) AS Veg, Sum([_union_roof_ics_post].Dry) AS Dry, Sum([_union_roof_ics_post].Bio) AS Bio, Sum([_union_roof_ics_post].Pla) AS Pla, Sum([_union_roof_ics_post].Grn) AS Grn
FROM _union_roof_ics_post
GROUP BY [_union_roof_ics_post].ParcelID, [_union_roof_ics_post].DivideID;


_union_roof_sum
----------
SELECT [_union_roof_ics].ParcelID, [_union_roof_ics].DivideID, Sum([_union_roof_ics].RoofControlled) AS RoofControlled, Sum([_union_roof_ics].Veg) AS Veg, Sum([_union_roof_ics].Dry) AS Dry, Sum([_union_roof_ics].Bio) AS Bio, Sum([_union_roof_ics].Pla) AS Pla, Sum([_union_roof_ics].Grn) AS Grn
FROM _union_roof_ics
GROUP BY [_union_roof_ics].ParcelID, [_union_roof_ics].DivideID;


_UnionIC
----------
SELECT  ParcelID,  DivideID, RoofRPark,  assumeKey,  SqFt,  Effectiveness, "Veg" as ICTYPE, Comment
FROM mdl_ic_DiscoVeg_ac;

union

SELECT  ParcelID,  DivideID,  RoofRPark,  assumeKey,  SqFt,  1.0 as Effectiveness, "DW" as ICTYPE, Comment
FROM mdl_ic_Drywell_ac;

union

SELECT  ParcelID,  DivideID,  RoofRPark,  assumeKey,  SqFt,  1.0 as Effectiveness, "Infilt" as ICTYPE, Comment
FROM mdl_ic_Infilt_ac;

UNION SELECT  ParcelID,  DivideID,  RoofRPark,  assumeKey,  SqFt,  1.0 as Effectiveness, "SWPlnt" as ICTYPE, Comment
FROM mdl_ic_SWPlnt_ac;


_UnionICSum
----------
SELECT [_UnionIC].assumeKey, [_UnionIC].RoofRPark, [_UnionIC].ICTYPE, [_UnionIC].Comment, Sum(CLng([SqFt]*[effectiveness])) AS EffArea
FROM _UnionIC
GROUP BY [_UnionIC].assumeKey, [_UnionIC].RoofRPark, [_UnionIC].ICTYPE, [_UnionIC].Comment
HAVING ((([_UnionIC].assumeKey)="ICT"));


_x_all
----------
SELECT DISTINCTROW Trim$([XPSWMM_Name]) AS NodeName, UCase$(Mid$([TypeCode],1,1)) AS FacType
FROM mdl_nodes_ac INNER JOIN StreetTargets_ac ON mdl_nodes_ac.Node = StreetTargets_ac.NGTOStreet;


union

SELECT Trim$([Node] & "I") AS NodeName, "B" AS Factype
FROM (mdl_nodes_ac INNER JOIN mdl_DirSC_NGTOs ON mdl_nodes_ac.Node = mdl_DirSC_NGTOs.NGTODirect) INNER JOIN mdl_ic_Infilt_ac ON (mdl_DirSC_NGTOs.DivideID = mdl_ic_Infilt_ac.DivideID) AND (mdl_DirSC_NGTOs.ParcelID = mdl_ic_Infilt_ac.ParcelID)
GROUP BY Trim$([Node] & "I"), "B";

UNION SELECT DISTINCTROW Trim$([Node] & "L") AS NodeName, "P" AS FacType
FROM (mdl_ic_SWPlnt_ac INNER JOIN mdl_DirSC_NGTOs ON (mdl_ic_SWPlnt_ac.DivideID = mdl_DirSC_NGTOs.DivideID) AND (mdl_ic_SWPlnt_ac.ParcelID = mdl_DirSC_NGTOs.ParcelID)) INNER JOIN mdl_nodes_ac ON mdl_DirSC_NGTOs.NGTODirect = mdl_nodes_ac.Node
GROUP BY Trim$([Node] & "L"), "P";


_x_bio
----------
SELECT Trim$([Node] & "I") AS NodeName, "B" AS Factype
FROM (mdl_nodes_ac INNER JOIN mdl_DirSC_NGTOs ON mdl_nodes_ac.Node = mdl_DirSC_NGTOs.NGTODirect) INNER JOIN mdl_ic_Infilt_ac ON (mdl_DirSC_NGTOs.DivideID = mdl_ic_Infilt_ac.DivideID) AND (mdl_DirSC_NGTOs.ParcelID = mdl_ic_Infilt_ac.ParcelID)
GROUP BY Trim$([Node] & "I"), "B";


_x_Planter
----------
SELECT DISTINCTROW Trim$([Node] & "L") AS NodeName, "P" AS FacType
FROM (mdl_ic_SWPlnt_ac INNER JOIN mdl_DirSC_NGTOs ON (mdl_ic_SWPlnt_ac.ParcelID = mdl_DirSC_NGTOs.ParcelID) AND (mdl_ic_SWPlnt_ac.DivideID = mdl_DirSC_NGTOs.DivideID)) INNER JOIN mdl_nodes_ac ON mdl_DirSC_NGTOs.NGTODirect = mdl_nodes_ac.Node
GROUP BY Trim$([Node] & "L"), "P";


_x_street
----------
SELECT DISTINCTROW Trim$([XPSWMM_Name]) AS NodeName, UCase$(Mid$([TypeCode],1,1)) AS FacType
FROM mdl_nodes_ac INNER JOIN StreetTargets_ac ON mdl_nodes_ac.Node = StreetTargets_ac.NGTOStreet;


IC_dhi_Export_LinkVI
----------
INSERT INTO DHILinks ( LinkID, FromNode, ToNode, TypeNo, CRSID, ScalingTYPENO, [Diameter/Width], ScaleOrWidth, Height, MaterialNo, UpLevel, DwLevel, Infiltration, SpecifiedLength, RMapproachNO )
SELECT K_dhi_VI_Link.simlink, K_dhi_VI_Link.FacNode, K_dhi_VI_Link.OutfallNode, K_dhi_VI_Link.xTypeNo, K_dhi_VI_Link.xDHICRSID, K_dhi_VI_Link.STno, K_dhi_VI_Link.DW, K_dhi_VI_Link.SoeW, K_dhi_VI_Link.HGT, K_dhi_VI_Link.MatNo, K_dhi_VI_Link.UpLevel, K_dhi_VI_Link.DwLevel, K_dhi_VI_Link.infl, K_dhi_VI_Link.xlen, K_dhi_VI_Link.GorF
FROM K_dhi_VI_Link;


IC_dhi_Export_LinkVIManning
----------
INSERT INTO DHIRoughness ( LinkID, Manning, Ppipe, SMFlag, RID )
SELECT "z" & Trim$([facnode]) AS simlink, Format$(IIf(IsNull([roughness]),1000,IIf([roughness]=0,1000,1/[Roughness])),"Fixed") AS ManningInv, 0 AS Expr1, 0 AS Expr2, 0 AS Expr3
FROM ICNode, _Globals
WHERE (((ICNode.Include)="OK") AND ((ICNode.Factype)<>"V"));


IC_dhi_Export_NodeFac
----------
INSERT INTO DHINodes ( NodeID, TypeNo, X, Y, Diameter, InvertLevel, GroundLevel, WaterLevel, OutletShapeNo, DatasetID, RMNodeTypeNo, TOPTypeNo, BufferPressureLevel, RELWeirCoeff, SMFlag )
SELECT K_dhi_NodeFac.NodeName, K_dhi_NodeFac.xtype, K_dhi_NodeFac.XCoord, K_dhi_NodeFac.YCoord, K_dhi_NodeFac.defadia, K_dhi_NodeFac.NodeIE, K_dhi_NodeFac.xgrelev, K_dhi_NodeFac.WaterLevel, K_dhi_NodeFac.OSN, K_dhi_NodeFac.DSID, K_dhi_NodeFac.RMNodeTypeNo, K_dhi_NodeFac.TOPtypeNo, K_dhi_NodeFac.BufferPressureLevel, K_dhi_NodeFac.RELWeirCoeff, K_dhi_NodeFac.SMFlag
FROM K_dhi_NodeFac;


IC_dhi_Export_NodeFac_StoreNames
----------
INSERT INTO DHITabularDataNames ( DataSetID, TypeNo )
SELECT ICNode.FacNode, 31 AS Type
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE (((ICNode.Factype)="V") AND ((ICNode.Include)="OK"));


IC_dhi_Export_NodeFac_StoreValues
----------
INSERT INTO DHITabularDataValues ( DataSetID, X, Y, Z )
SELECT K_dhi_Node_Storage.FacNode, K_dhi_Node_Storage.elev, K_dhi_Node_Storage.x_Ac, K_dhi_Node_Storage.x_As
FROM K_dhi_Node_Storage;


IC_dhi_Export_NodeVI
----------
INSERT INTO DHINodes ( NodeID, TypeNo, X, Y, Diameter, InvertLevel, GroundLevel, WaterLevel, OutletShapeNo, DatasetID, RMNodeTypeNo, TOPTypeNo, BufferPressureLevel, RELWeirCoeff, SMFlag )
SELECT K_dhi_VI_Node.NodeName, K_dhi_VI_Node.xtype, K_dhi_VI_Node.XCoord, K_dhi_VI_Node.YCoord, K_dhi_VI_Node.defadia, K_dhi_VI_Node.NodeIE, K_dhi_VI_Node.xgrelev, K_dhi_VI_Node.WaterLevel, K_dhi_VI_Node.OSN, K_dhi_VI_Node.DSID, K_dhi_VI_Node.RMNodeTypeNo, K_dhi_VI_Node.TOPtypeNo, K_dhi_VI_Node.BufferPressureLevel, K_dhi_VI_Node.RELWeirCoeff, K_dhi_VI_Node.SMFlag
FROM K_dhi_VI_Node;


IC_dhi_Export_Orifice1
----------
INSERT INTO DHIOrifi ( OrificeID, FromNode, ToNode, TypeNo, CRSID, ScalingTypeNo, Diameter, ScaleorWidth, Height, InvertLevel, ControlTypeNo )
SELECT K_dhi_orifice1.OrificeName, K_dhi_orifice1.FromNode, K_dhi_orifice1.TONode, K_dhi_orifice1.TypeNo, K_dhi_orifice1.CRSID, K_dhi_orifice1.ScalingTypeNo, K_dhi_orifice1.oDiam, K_dhi_orifice1.ScaleorWidth, K_dhi_orifice1.Height, K_dhi_orifice1.InvertLevel, K_dhi_orifice1.ControlTypeNO
FROM K_dhi_orifice1;


IC_dhi_Export_Weir1
----------
INSERT INTO DHIWeirs ( WEIRID, Location, FlowTo, CrestLevel, MethodNo, DataSetID, CrestTypeNo, CrestWidth, WeirFlowAngle, WeirCoefficient, ControlTypeNo )
SELECT K_dhi_Weir1.WeirID, K_dhi_Weir1.Location, K_dhi_Weir1.FlowTO, K_dhi_Weir1.Crestlevel, K_dhi_Weir1.MethodNo, K_dhi_Weir1.DatasetID, K_dhi_Weir1.CrestTypeNo, K_dhi_Weir1.CrestWidth, K_dhi_Weir1.WeirFlowAngle, K_dhi_Weir1.Coefficient, K_dhi_Weir1.ControlTypeNo
FROM K_dhi_Weir1;


IC_dhi_Export_Weir2
----------
INSERT INTO DHIWeirs ( WEIRID, Location, FlowTo, CrestLevel, MethodNo, DataSetID, CrestTypeNo, CrestWidth, WeirFlowAngle, WeirCoefficient, ControlTypeNo )
SELECT K_dhi_Weir2.WeirID, K_dhi_Weir2.Location, K_dhi_Weir2.FlowTO, K_dhi_Weir2.Crestlevel, K_dhi_Weir2.MethodNo, K_dhi_Weir2.DatasetID, K_dhi_Weir2.CrestTypeNo, K_dhi_Weir2.CrestWidth, K_dhi_Weir2.WeirFlowAngle, K_dhi_Weir2.Coefficient, K_dhi_Weir2.ControlTypeNo
FROM K_dhi_Weir2;


K_dhi_Node_Storage
----------
SELECT ICNode.FacNode, [RefGrelev]+[NodeZPRelAdjusT]+0 AS elev, 0 AS x_Ac, 12 AS x_As
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE (((ICNode.Factype)="V") AND ((ICNode.Include)="OK"))

union 

SELECT ICNode.FacNode, [RefGrelev]+[NodeZPRelAdjusT]+3 AS elev, 4*3 AS x_Ac,12 AS x_As
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE (((ICNode.Factype)="V") AND ((ICNode.Include)="OK"))

union 


SELECT ICNode.FacNode, [RefGrelev]+[NodeZPRelAdjusT]+3.01 AS elev, 4*3 +.1 AS x_Ac, ICNode.PondingCuFt AS x_As
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE (((ICNode.Factype)="V") AND ((ICNode.Include)="OK"));

UNION SELECT ICNode.FacNode, [RefGrelev]+[NodeZPRelAdjusT]+4 AS elev, Format$([PondingCuFt]^0.5+ 4*3,"Fixed") AS x_Ac, ICNode.PondingCuFt AS x_As
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE (((ICNode.Factype)="V") AND ((ICNode.Include)="OK"));


K_dhi_NodeFac
----------
SELECT Trim([FacNode]) AS NodeName, IIf([icnode].[Factype]="V",2,1) AS xtype, ICNode.FacX AS XCoord, ICNode.FacY AS YCoord, (4*[FacVolCuFt]/(22/7))^0.5 AS defadia, [RefGrelEv]+[NodeZRelAdjust] AS NodeIE, [RefGrelEv]+[grelevRelAdjust] AS xgrelev, [RefGrelEv]+[NodeZRelAdjust] AS WaterLevel, 4 AS OSN, IIf([icnode].[Factype]="V",[facnode],Null) AS DSID, 2 AS RMNodeTypeNo, 1 AS TOPtypeNo, 0 AS BufferPressureLevel, 1 AS RELWeirCoeff, 0 AS SMFlag
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE (((ICNode.Include)="OK"))
ORDER BY Trim([FacNode]);


K_dhi_orifice1
----------
SELECT "o" & [FacNode] AS OrificeName, ICNode.FacNode AS FromNode, ICNode.RefNode AS TONode, 1 AS TypeNo, Null AS CRSID, 0 AS ScalingTypeNo, Format(IIf([icnode].[Factype]="P",(4*0.0873/(22/7))^0.5,(4*0.12*[vFlowControl]/(22/7))^0.5),"0.000") AS oDiam, 0 AS ScaleorWidth, 0 AS Height, [RefGrelev]+[NodeZPRelAdjusT] AS InvertLevel, 1 AS ControlTypeNO
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Orifice)=True) AND ((ICNode.Include)="OK"));


K_dhi_VI_Link
----------
SELECT "z" & Trim$([facnode]) AS simlink, ICNode.FacNode, ICNode.OutfallNode, 1 AS xTypeNo, Null AS xDHICRSID, 1 AS STno, Format$([Diameter],"Fixed") AS DW, 1 AS SoeW, 0 AS HGT, 8 AS MatNo, [RefGrelev]-1 AS UpLevel, [RefGrelev]-(1+[OFLength]*[OFslope]) AS DwLevel, 0 AS infl, [_Globals].OFLength AS xlen, 1 AS GorF, ICNode.Include
FROM ICNode, _Globals
WHERE (((ICNode.Include)="OK") AND ((ICNode.Factype)<>"V"));


K_dhi_VI_Node
----------
SELECT Trim([outfallNode]) AS NodeName, 3 AS xtype, [FacX]+5 AS XCoord, [FacY]+5 AS YCoord, 4 AS defadia, [RefGrelev]-(1+[OFLength]*[OFslope]) AS NodeIE, [RefGrelEv]+[grelevRelAdjust] AS xgrelev, [RefGrelEv]+[NodeZRelAdjust] AS WaterLevel, 4 AS OSN, Null AS DSID, 2 AS RMNodeTypeNo, 1 AS TOPtypeNo, 0 AS BufferPressureLevel, 1 AS RELWeirCoeff, 0 AS SMFlag, ICNode.Factype
FROM _Globals, ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE (((ICNode.Factype)<>"V") AND ((ICNode.Include)="OK"))
ORDER BY Trim([outfallNode]);


K_dhi_Weir1
----------
SELECT "w" & [FacNode] AS WeirID, ICNode.FacNode AS Location, ICNode.RefNode AS FlowTO, [RefGrelev]+[YCrestRelAdjust] AS Crestlevel, 2 AS MethodNo, Null AS DatasetID, 1 AS CrestTypeNo, [_IC_Global_Facility_Vars].WLen1 AS CrestWidth, 90 AS WeirFlowAngle, 2.7 AS Coefficient, 1 AS ControlTypeNo, ICNode.Include
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE (((ICNode.Include)="OK") AND (([_IC_Global_Facility_Vars].Weir1)=True));


K_dhi_Weir2
----------
SELECT "s" & [FacNode] AS WeirID, ICNode.FacNode AS Location, ICNode.RefNode AS FlowTO, [RefGrelev]+[YCrest2RelAdjust] AS Crestlevel, 2 AS MethodNo, Null AS DatasetID, 1 AS CrestTypeNo, [_IC_Global_Facility_Vars].WLen2 AS CrestWidth, 90 AS WeirFlowAngle, 2.7 AS Coefficient, 1 AS ControlTypeNo, ICNode.Include
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE (((ICNode.Include)="OK") AND (([_IC_Global_Facility_Vars].Weir2)=True));


K_XPX_Fac_A_Link
----------
SELECT DISTINCTROW "LINK 138" AS xpx, Chr(34) & "x" & [Facnode] & Chr(34) AS linkname, Chr(34) & Trim([Facnode]) & Chr(34) AS frmnode, Chr(34) & Trim([refnode]) & Chr(34) AS tonode
FROM ICNode
WHERE (((ICNode.Include)="OK"))
GROUP BY "LINK 138", Chr(34) & "x" & [Facnode] & Chr(34), Chr(34) & Trim([Facnode]) & Chr(34), Chr(34) & Trim([refnode]) & Chr(34)
ORDER BY Chr(34) & "x" & [Facnode] & Chr(34);


K_XPX_Fac_A_Node
----------
SELECT "NODE 133" AS XPCode, Chr(34) & Trim([FacNode]) & Chr(34) AS NodeName, ICNode.FacX AS FacilityX, ICNode.FacY AS FacilityY
FROM ICNode
WHERE (((ICNode.Include)="OK"))
ORDER BY ICNode.Factype;


K_XPX_Fac_L_AORIF
----------
SELECT DISTINCTROW "DATA AORIF" AS XPX, "x" & [FacNode] AS Link, "1 1" AS EXPR3, IIf([icnode].[Factype]="P",0.0873,0.12*[vFlowControl]) AS Aorif
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Orifice)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_L_CORIF
----------
SELECT DISTINCTROW "DATA CORIF" AS XPX, "x" & [FacNode] AS Link, "1 1 0.6" AS EXPR3
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Orifice)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_L_ONKLASS
----------
SELECT DISTINCTROW "DATA ONKLASS" AS XPX, "x" & [FacNode] AS Link, "1 1 1" AS EXPR3
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Orifice)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_L_ORIF1
----------
SELECT DISTINCTROW "DATA ORIF1" AS XPX, "x" & [FacNode] AS Link, "1 1 1" AS EXPR3
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Orifice)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_L_ORIFName
----------
SELECT DISTINCTROW "DATA ORIFNAME" AS XPX, Chr(34) & "x" & [FacNode] & Chr(34) AS Link, "1 1" AS EXPR3, Chr(34) & "o" & [FacNode] & Chr(34) AS Expr1
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Orifice)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_FAC_L_ZP
----------
SELECT DISTINCTROW "DATA ZP" AS XPX, Chr$(34) & "x" & [facnode] & Chr$(34) AS Link, "1 1" AS EXPR3, [RefGrelev]+[NodeZPRelAdjusT] AS Expr7
FROM _IC_Global_Facility_Vars INNER JOIN ICNode ON [_IC_Global_Facility_Vars].FacType = ICNode.Factype
WHERE ((([_IC_Global_Facility_Vars].Orifice)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_LW1_CNTLW
----------
SELECT DISTINCTROW "DATA CNTLW" AS XPX, "x" & [FacNode] AS LINK, "1 1 1" AS EXPR3
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Weir1)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_LW1_COEFF
----------
SELECT DISTINCTROW "DATA COEFF" AS XPX, Chr(34) & "x" & [facnode] & Chr(34) AS Link, " 1 1 2.7" AS EXPR3
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Weir1)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_LW1_KWEIR
----------
SELECT DISTINCTROW "DATA KWEIR" AS XPX, Chr(34) & "x" & [Facnode] & Chr(34) AS LINK, " 1 1 1" AS EXPR3
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Weir1)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


k_XPX_Fac_LW1_WEIR1
----------
SELECT DISTINCTROW "DATA WEIR1" AS XPX, Chr(34) & "x" & [FacNode] & Chr(34) AS LINK, "0 1 1" AS EXPR3
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Weir1)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_LW1_WEIRNAME
----------
SELECT "DATA WEIRNAME" AS xpx, Chr$(34) & "x" & [FacNode] & Chr$(34) AS link, "1 1" AS Expr3, Chr$(34) & "w" & [FacNode] & Chr$(34) AS name
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Weir1)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_LW1_WLEN
----------
SELECT DISTINCTROW "DATA WLEN" AS XPX, Chr(34) & "x" & [facnode] & Chr(34) AS EXPR4, " 1 1 " AS EXPR3, [_IC_Global_Facility_Vars].WLen1
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Weir1)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_LW1_YCREST
----------
SELECT DISTINCTROW "DATA YCREST" AS XPX, Chr(34) & "x" & [facnode] & Chr(34) AS EXPR3, " 1 1" AS EXPR5, [RefGrelev]+[YCrestRelAdjust] AS YCREST
FROM _IC_Global_Facility_Vars INNER JOIN ICNode ON [_IC_Global_Facility_Vars].FacType = ICNode.Factype
WHERE ((([_IC_Global_Facility_Vars].Weir1)=True) AND ((ICNode.Include)="OK"))
ORDER BY [_IC_Global_Facility_Vars].FacType, [_IC_Global_Facility_Vars].FacType;


K_XPX_Fac_LW1_YTOP
----------
SELECT DISTINCTROW "DATA YTOP" AS XPX, Chr(34) & "x" & [facnode] & Chr(34) AS EXPR3, " 1 1" AS EXPR5, [RefGrelev]+[YTopRelAdjust] AS YTop
FROM _IC_Global_Facility_Vars INNER JOIN ICNode ON [_IC_Global_Facility_Vars].FacType = ICNode.Factype
WHERE ((([_IC_Global_Facility_Vars].Weir1)=True) AND ((ICNode.Include)="OK"))
ORDER BY [_IC_Global_Facility_Vars].FacType, [_IC_Global_Facility_Vars].FacType;


K_XPX_Fac_LW2_CNTLW
----------
SELECT DISTINCTROW "DATA CNTLW" AS XPX, "x" & [FacNode] AS LINK, "2 1 1" AS EXPR3
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Weir2)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_LW2_COEFF
----------
SELECT DISTINCTROW "DATA COEFF" AS XPX, Chr(34) & "x" & [facnode] & Chr(34) AS Link, " 2 1 2.7" AS EXPR3
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Weir2)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_LW2_KWEIR
----------
SELECT DISTINCTROW "DATA KWEIR" AS XPX, Chr(34) & "x" & [Facnode] & Chr(34) AS LINK, " 2 1 1" AS EXPR3
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Weir2)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


k_XPX_Fac_LW2_WEIR2
----------
SELECT DISTINCTROW "DATA WEIR2" AS XPX, Chr(34) & "x" & [FacNode] & Chr(34) AS LINK, "0 1 1" AS EXPR3
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Weir2)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_LW2_WEIRNAME
----------
SELECT "DATA WEIRNAME" AS xpx, Chr$(34) & "x" & [FacNode] & Chr$(34) AS link, "2 1" AS Expr3, Chr$(34) & "s" & [FacNode] & Chr$(34) AS name
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Weir2)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_LW2_WLEN
----------
SELECT DISTINCTROW "DATA WLEN" AS XPX, Chr(34) & "x" & [facnode] & Chr(34) AS EXPR4, " 2 1 " AS EXPR3, [_IC_Global_Facility_Vars].WLen2
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE ((([_IC_Global_Facility_Vars].Weir2)=True) AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_LW2_YCREST
----------
SELECT DISTINCTROW "DATA YCREST" AS XPX, Chr(34) & "x" & [facnode] & Chr(34) AS Link, " 2 1" AS EXPR5, [RefGrelev]+[YCrest2RelAdjust] AS YCREST
FROM _IC_Global_Facility_Vars INNER JOIN ICNode ON [_IC_Global_Facility_Vars].FacType = ICNode.Factype
WHERE ((([_IC_Global_Facility_Vars].Weir2)=True) AND ((ICNode.Include)="OK"))
ORDER BY [_IC_Global_Facility_Vars].FacType, [_IC_Global_Facility_Vars].FacType;


K_XPX_Fac_LW2_YTOP
----------
SELECT DISTINCTROW "DATA YTOP" AS XPX, Chr(34) & "x" & [facnode] & Chr(34) AS Link, " 2 1" AS EXPR5, [RefGrelev]+[YTopRelAdjust] AS YTop
FROM _IC_Global_Facility_Vars INNER JOIN ICNode ON [_IC_Global_Facility_Vars].FacType = ICNode.Factype
WHERE ((([_IC_Global_Facility_Vars].Weir2)=True) AND ((ICNode.Include)="OK"))
ORDER BY [_IC_Global_Facility_Vars].FacType, [_IC_Global_Facility_Vars].FacType;


K_XPX_Fac_N_AStore
----------
SELECT DISTINCTROW "DATA ASTORE" AS EXPR1, ICNode.FacNode, "0 1" AS EXPR3, ICNode.FacVolCuFt
FROM ICNode
WHERE (((ICNode.Include)="OK"));


K_XPX_Fac_N_CNTLS
----------
SELECT DISTINCTROW "DATA CNTLS" AS XPX, Chr(34) & [FacNode] & Chr(34) AS facility, 0 AS EXPR3, 1 AS EXPR4, IIf([Factype]="V",3,1) AS ctltype
FROM ICNode
WHERE (((ICNode.Include)="OK"))
ORDER BY ICNode.Factype;


K_XPX_Fac_N_GRELEV
----------
SELECT DISTINCTROW "DATA GRELEV" AS XPX, Chr(34) & Trim([facNODE]) & Chr(34) AS EXPR2, "0 1" AS EXPR3, [RefGrelEv]+[grelevRelAdjust] AS Expr4
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE (((ICNode.Include)="OK"))
ORDER BY [_IC_Global_Facility_Vars].FacType, ICNode.FacNode;


K_XPX_Fac_N_NODST
----------
SELECT DISTINCTROW "DATA NODST" AS XPX, ICNode.FacNode, "0 1" AS EXPR3, "1" AS EXPR4
FROM ICNode
WHERE (((ICNode.Include)="OK"));


K_XPX_Fac_N_QCURA
----------
SELECT DISTINCTROW "DATA QCURA" AS XPX, Chr(34) & Trim([FacNode]) & Chr(34) AS Facility, "0 4 2.89e-4 2.89e-4" AS EXPR3, [PondingCUFt]/43560 AS PV1, [PondingCuFt]/43560 AS PV2
FROM ICNode
WHERE (((ICNode.Factype)="V") AND ((ICNode.Include)="OK"))
ORDER BY Chr(34) & Trim([FacNode]) & Chr(34);


K_XPX_Fac_N_QCURRL
----------
SELECT DISTINCTROW "DATA QCURRL" AS XPX, Chr(34) & Trim([FacNode]) & Chr(34) AS EXPR2, "0 4 0. 3. 3.01 4.0" AS EXPR3
FROM ICNode
WHERE (((ICNode.Factype)="V") AND ((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_N_SFLOOD
----------
SELECT DISTINCTROW "DATA SFLOOD" AS XPX, ICNode.FacNode, "0 1 1" AS EXPR3
FROM ICNode
WHERE (((ICNode.Include)="OK"))
ORDER BY ICNode.Factype, ICNode.FacNode;


K_XPX_Fac_N_Z
----------
SELECT DISTINCTROW "DATA Z" AS XPX, ICNode.FacNode, "0 1" AS EXPR3, [RefGrelEv]+[NodeZRelAdjust] AS EXPR4
FROM _IC_Global_Facility_Vars INNER JOIN ICNode ON [_IC_Global_Facility_Vars].FacType = ICNode.Factype
WHERE (((ICNode.Include)="OK"));


QAQC:Sum_ssc_acres_to_curbs
----------
SELECT ICNode.FacNode, Sum(ICNode.FacVolCuFt) AS SumOfFacVolCuFt, Sum(mdl_SurfSC_ac.c_netacres) AS SumOfc_netacres, Sum(mdl_SurfSC_ac.c_netIMPacres) AS SumOfc_netIMPacres, Sum(ICNode.Infilcfs) AS SumOfInfilcfs
FROM ICNode INNER JOIN (mdl_ic_NgtoRedir_ac INNER JOIN mdl_SurfSC_ac ON mdl_ic_NgtoRedir_ac.SurfSCID = mdl_SurfSC_ac.SurfSCID) ON ICNode.FacNode = mdl_ic_NgtoRedir_ac.ngto
GROUP BY ICNode.FacNode
ORDER BY ICNode.FacNode;


QAQC_01:Local_dup_NgtoRdirs
----------
SELECT StreetTargets_ac.SurfSCID, Count(StreetTargets_ac.MAPINFO_ID) AS Nrecs
FROM StreetTargets_ac INNER JOIN _filter_street ON StreetTargets_ac.SurfSCID = [_filter_street].SurfSCID
WHERE (((StreetTargets_ac.Evaluation)<>"N"))
GROUP BY StreetTargets_ac.SurfSCID
HAVING (((Count(StreetTargets_ac.MAPINFO_ID))>1));


QAQC_02:NullRedirs
----------
SELECT StreetTargets_ac.SurfSCID, StreetTargets_ac.XPSWMM_Name
FROM StreetTargets_ac
WHERE (((StreetTargets_ac.XPSWMM_Name) Is Null Or (StreetTargets_ac.XPSWMM_Name)=""));


QAQC_03:DupStreetXPSWMMName
----------
SELECT DISTINCTROW Trim$([XPSWMM_Name]) AS NodeName, Count(StreetTargets_ac.MAPINFO_ID) AS NDups
FROM mdl_nodes_ac INNER JOIN StreetTargets_ac ON mdl_nodes_ac.Node = StreetTargets_ac.NGTOStreet
GROUP BY Trim$([XPSWMM_Name])
HAVING (((Count(StreetTargets_ac.MAPINFO_ID))>1));


-qcduppark
----------
SELECT ICTargetPark.ParcelID, ICTargetPark.DivideID, Count(ICTargetPark.Mst_targetMIID) AS CountOfMst_targetMIID
FROM ICTargetPark
GROUP BY ICTargetPark.ParcelID, ICTargetPark.DivideID
HAVING (((Count(ICTargetPark.Mst_targetMIID))>1));


Query1
----------
SELECT ICNode.FacNode, [RefGrelev]+[NodeZPRelAdjusT]+0 AS elev, 0.000289 AS AAs
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE (((ICNode.Factype)="V"));

union 

SELECT ICNode.FacNode, [RefGrelev]+[NodeZPRelAdjusT]+3 AS elev, 0.000289 AS AAs
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE (((ICNode.Factype)="V"));

UNION SELECT ICNode.FacNode, [RefGrelev]+[NodeZPRelAdjusT]+3 AS elev, 0.000289 AS AAs
FROM ICNode INNER JOIN _IC_Global_Facility_Vars ON ICNode.Factype = [_IC_Global_Facility_Vars].FacType
WHERE (((ICNode.Factype)="V"));


Query2
----------
SELECT ic_RoofTargets.ParcelID AS Expr1, ic_RoofTargets.DivideID AS Expr2, Count(ic_RoofTargets.MAPINFO_ID) AS CountOfMAPINFO_ID
FROM ic_RoofTargets
GROUP BY ic_RoofTargets.ParcelID, ic_RoofTargets.DivideID
HAVING (((Count([ic_RoofTargets].[MAPINFO_ID]))>1));


StreetTargets_ac
----------
SELECT [_filter_street].*
FROM _filter_street INNER JOIN mdl_SurfSC_ac ON [_filter_street].SurfSCID = mdl_SurfSC_ac.SurfSCID;


VI_00_clear_ICNODE
----------
DELETE ICNode.*
FROM ICNode;


VI_01_add_Node_Bioret
----------
INSERT INTO ICNode ( RefNode, RefX, RefY, RefGrelev, FacNode, Factype, FacX, FacY, FacVolCuFt )
SELECT mdl_nodes_ac.Node, mdl_nodes_ac.XCoord, mdl_nodes_ac.YCoord, Format$([GrndElev],"Fixed") AS Expr3, Trim$([Node] & "I") AS NodeName, "B" AS Expr1, First([XCoord]-16) AS FacilityX, First([YCoord]+3) AS FacilityY, Sum([SqFt]*0.09*0.83) AS Expr2
FROM (mdl_DirSC_NGTOs INNER JOIN mdl_ic_Infilt_ac ON (mdl_DirSC_NGTOs.DivideID = mdl_ic_Infilt_ac.DivideID) AND (mdl_DirSC_NGTOs.ParcelID = mdl_ic_Infilt_ac.ParcelID)) INNER JOIN mdl_nodes_ac ON mdl_DirSC_NGTOs.NGTODirect = mdl_nodes_ac.Node
GROUP BY mdl_nodes_ac.Node, mdl_nodes_ac.XCoord, mdl_nodes_ac.YCoord, Format$([GrndElev],"Fixed"), Trim$([Node] & "I"), "B"
HAVING (((Sum([SqFt]*0.09*0.83))>0));


VI_01_add_Node_Planter
----------
INSERT INTO ICNode ( RefNode, RefX, RefY, RefGrelev, FacNode, Factype, FacX, FacY, FacVolCuFt )
SELECT DISTINCTROW mdl_nodes_ac.Node, mdl_nodes_ac.XCoord, mdl_nodes_ac.YCoord, Format$([GrndElev],"Fixed") AS Expr3, Trim$([Node] & "L") AS NodeName, "P" AS Expr1, First([XCoord]+16) AS FacilityX, First([YCoord]-3) AS FacilityY, Sum([SqFt]*0.06) AS Expr2
FROM (mdl_ic_SWPlnt_ac INNER JOIN mdl_DirSC_NGTOs ON (mdl_ic_SWPlnt_ac.DivideID = mdl_DirSC_NGTOs.DivideID) AND (mdl_ic_SWPlnt_ac.ParcelID = mdl_DirSC_NGTOs.ParcelID)) INNER JOIN mdl_nodes_ac ON mdl_DirSC_NGTOs.NGTODirect = mdl_nodes_ac.Node
GROUP BY mdl_nodes_ac.Node, mdl_nodes_ac.XCoord, mdl_nodes_ac.YCoord, Format$([GrndElev],"Fixed"), Trim$([Node] & "L"), "P"
HAVING (((Sum([SqFt]*0.06))>0))
ORDER BY Trim$([Node] & "L");


VI_01_add_StreetNodes
----------
INSERT INTO ICNode ( RefNode, RefX, RefY, RefGrelev, FacNode, FacX, FacY, vFlowControl, Factype, FacVolCuFt, PondingCuFt )
SELECT DISTINCTROW mdl_nodes_ac.Node, mdl_nodes_ac.XCoord, mdl_nodes_ac.YCoord, Format$([GrndElev],"Fixed") AS Expr3, Trim$([XPSWMM_Name]) AS KeyField, StreetTargets_ac.XCoord, StreetTargets_ac.YCoord, StreetTargets_ac.FlowControl, UCase$(Mid$([TypeCode],1,1)) AS Expr2, StreetTargets_ac.CurbExtensionVol, StreetTargets_ac.PondingVol
FROM mdl_nodes_ac INNER JOIN (StreetTargets_ac INNER JOIN mdl_ic_NgtoRedir_ac ON StreetTargets_ac.XPSWMM_Name = mdl_ic_NgtoRedir_ac.ngto) ON mdl_nodes_ac.Node = StreetTargets_ac.NGTOStreet
ORDER BY Trim$([XPSWMM_Name]);


VI_02_set_infiltrationFactors
----------
UPDATE ICNode SET ICNode.InfilFactor = IIf(Mid$([factype],1,1)="B",Format$(0.66/0.83,"Fixed"),IIf(Mid$([factype],1,1)="P",0.36,IIf(Mid$([factype],1,1)="C",1.6,0)));


VI_02_set_infiltrationNode
----------
UPDATE ICNode SET ICNode.OutfallNode = "B" & [facnode];


VI_03_set_infiltrationFlow
----------
UPDATE ICNode, _Globals SET ICNode.Infilcfs = [InfilFactor]*[facvolcuft]*[infilrate]/12/3600;


VI_04_set_roughness
----------
UPDATE ICNode, _Globals SET ICNode.Roughness = IIf(Round(0.46438*[diameter]^(8/3)/[Infilcfs]*[OFSlope]^0.5,6)=0,0.000001,Round(0.46438*[diameter]^(8/3)/[Infilcfs]*[OFSlope]^0.5,6))
WHERE (((ICNode.Infilcfs)>0));


VI_XPX_VirtInfilt_A_Link
----------
SELECT DISTINCTROW "LINK 136" AS expr1, Chr(34) & "z" & Trim$([facnode]) & Chr(34) AS EXPR2, Chr(34) & Trim$([facnode]) & Chr(34) AS EXPR3, Chr(34) & [OutfallNode] & Chr(34) AS EXPR4
FROM ICNode
WHERE (((ICNode.Factype)<>"V"))
ORDER BY Chr(34) & "z" & Trim$([facnode]) & Chr(34);


VI_XPX_VirtInfilt_A_Node
----------
SELECT DISTINCTROW "NODE 134" AS EXPR1, Chr(34) & Trim([outfallnode]) & Chr(34) AS EXPR2, [FacX]+5 AS EXPR3, [FacY]+5 AS EXPR4
FROM ICNode
WHERE (((ICNode.Factype)<>"V"))
ORDER BY Chr(34) & Trim([outfallnode]) & Chr(34);


VI_XPX_VirtInfilt_L_Diameter
----------
SELECT DISTINCTROW "DATA DEEP" AS EXPR1, Chr(34) & "z" & Trim([facnode]) & Chr(34) AS EXPR2, "0 1" AS EXPR3, Format$([Diameter],"Fixed") AS Expr4
FROM _Globals, ICNode
WHERE (((ICNode.Factype)<>"V"))
ORDER BY Chr(34) & "z" & Trim([facnode]) & Chr(34);


VI_XPX_VirtInfilt_L_Length
----------
SELECT DISTINCTROW "DATA LEN" AS expr1, Chr(34) & "z" & [Facnode] & Chr(34) AS EXPR2, "0 1" AS Expr3, [_Globals].OFLength
FROM ICNode, _Globals
WHERE (((ICNode.Factype)<>"V"))
ORDER BY Chr(34) & "z" & [Facnode] & Chr(34);


VI_XPX_VirtInfilt_L_Rough
----------
SELECT DISTINCTROW "DATA Rough" AS expr1, Chr(34) & "z" & [Facnode] & Chr(34) AS EXPR2, "0 1" AS Expr3, ICNode.Roughness
FROM ICNode
WHERE (((ICNode.Factype)<>"V"))
ORDER BY Chr(34) & "z" & [Facnode] & Chr(34);


VI_XPX_VirtInfilt_L_shape
----------
SELECT DISTINCTROW "DATA NKLASS" AS EXPR1, Chr(34) & "z" & Trim([facnode]) & Chr(34) AS EXPR2, "0 1 1" AS EXPR3
FROM ICNode
WHERE (((ICNode.Factype)<>"V"))
ORDER BY Chr(34) & "z" & Trim([facnode]) & Chr(34);


VI_XPX_VirtInfilt_L_Slope
----------
SELECT DISTINCTROW "DATA Slope" AS expr1, Chr(34) & "z" & [Facnode] & Chr(34) AS EXPR2, "0 1" AS Expr3, [_Globals].OFSlope
FROM ICNode, _Globals
WHERE (((ICNode.Factype)<>"V"))
ORDER BY Chr(34) & "z" & [Facnode] & Chr(34);


VI_XPX_VirtInfilt_L_ZP1
----------
SELECT DISTINCTROW "DATA ZP1" AS EXPR1, Chr(34) & "z" & Trim([Facnode]) & Chr(34) AS EXPR2, "0 1" AS EXPR3, [RefGrelev]-1 AS Expr4
FROM ICNode
WHERE (((ICNode.Factype)<>"V"))
ORDER BY Chr(34) & "z" & Trim([Facnode]) & Chr(34);


VI_XPX_VirtInfilt_L_ZP2
----------
SELECT DISTINCTROW "DATA ZP2" AS EXPR1, Chr(34) & "z" & Trim([Facnode]) & Chr(34) AS EXPR2, "0 1" AS EXPR3, [RefGrelev]-(1+[OFLength]*[OFslope]) AS Expr4
FROM ICNode, _Globals
WHERE (((ICNode.Factype)<>"V"))
ORDER BY Chr(34) & "z" & Trim([Facnode]) & Chr(34);


VI_XPX_VirtInfilt_N_Outfall_Flag
----------
SELECT DISTINCTROW "DATA FLGOUTF" AS EXPR1, Chr(34) & Trim([outfallnode]) & Chr(34) AS EXPR2, "0 1 1" AS Expr3
FROM ICNode
WHERE (((ICNode.Factype)<>"V"))
ORDER BY Chr(34) & Trim([outfallnode]) & Chr(34);


VI_XPX_VirtInfilt_N_Outfall_Grelev
----------
SELECT DISTINCTROW "DATA GRELEV" AS EXPR1, Chr(34) & Trim([outfallnode]) & Chr(34) AS EXPR2, "0 1" AS Expr3, ICNode.RefGrelev
FROM ICNode
WHERE (((ICNode.Factype)<>"V"))
ORDER BY ICNode.Factype, ICNode.FacNode;


VI_XPX_VirtInfilt_N_Outfall_Type
----------
SELECT DISTINCTROW "DATA NTIDE" AS EXPR1, Chr(34) & Trim([outfallnode]) & Chr(34) AS EXPR2, "0 1 1" AS Expr3
FROM ICNode
WHERE (((ICNode.Factype)<>"V"))
ORDER BY Chr(34) & Trim([outfallnode]) & Chr(34);


VI_XPX_VirtInfilt_N_Outfall_Z
----------
SELECT DISTINCTROW "DATA Z" AS EXPR1, Chr(34) & Trim([outfallnode]) & Chr(34) AS EXPR2, "0 1" AS EXPR3, [RefGrelev]-(1+[OFLength]*[OFslope]) AS Expr4
FROM ICNode, _Globals
WHERE (((ICNode.Factype)<>"V"))
ORDER BY Chr(34) & Trim([outfallnode]) & Chr(34);


