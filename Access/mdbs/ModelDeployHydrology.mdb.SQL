__01_SumHCardsBySource
----------
SELECT [_sourcetype].Source, Sum(IIf(IsNull([Area]),0,[area])) AS TotalArea, Sum(IIf(IsNull([Area]),0,[area])*IIf(IsNull([pctimp]),0,[pctimp])/100) AS imparea
FROM R_Data_H1 RIGHT JOIN _sourcetype ON R_Data_H1.Source = [_sourcetype].Source
GROUP BY [_sourcetype].Source, [_sourcetype].order
ORDER BY [_sourcetype].order;


__02_Total_RoofandParkingInGIS
----------
SELECT Sum([PkAreaFt]/43560) AS dd_P, Sum([RFAreaFt]/43560) AS dd_R
FROM sc_dd;


__03_SummaryICVeg
----------
SELECT "Total IC Veg, "+[RorP] AS [Desc], Sum([SqFt]/43560) AS [Total Area], Sum([SqFt]*(1-[effectiveness])/43560) AS [IMP Area]
FROM _RorP LEFT JOIN mdl_ic_discoveg_ac ON [_RorP].RorP = mdl_ic_discoveg_ac.RoofRPark
GROUP BY "Total IC Veg, "+[RorP];


__04_Total_SurfaceAreaInGIS
----------
SELECT Sum(mdl_SurfSC_ac.totalgrossacres) AS SumOftotalgrossacres, Sum(mdl_SurfSC_ac.PaveTreeAcres) AS SumOfPaveTreeAcres, Sum(mdl_SurfSC_ac.c_RFgrossacres) AS SumOfc_RFgrossacres, Sum(mdl_SurfSC_ac.c_PKgrossacres) AS SumOfc_PKgrossacres, Sum(mdl_SurfSC_ac.c_RFtosurfacres) AS SumOfc_RFtosurfacres, Sum(mdl_SurfSC_ac.c_PKtosurfacres) AS SumOfc_PKtosurfacres
FROM mdl_SurfSC_ac;


__05_Impervious_SurfaceAreaInGIS
----------
SELECT Sum(mdl_SurfSC_ac.paveacres) AS SumOfpaveacres, Sum(mdl_SurfSC_ac.SWacres) AS SumOfSWacres, Sum(mdl_SurfSC_ac.c_RFtosurfIMPacres) AS SumOfc_RFtosurfIMPacres, Sum(mdl_SurfSC_ac.c_PKtosurfIMPacres) AS SumOfc_PKtosurfIMPacres, Sum(mdl_SurfSC_ac.ROWAcres) AS SumOfROWAcres, Sum(mdl_SurfSC_ac.PaveTreeAcres) AS SumOfPaveTreeAcres, Sum(mdl_SurfSC_ac.c_netIMPacres) AS SumOfc_netIMPacres
FROM mdl_SurfSC_ac;


__06_AdjustmentIA_Error
----------
SELECT Sum(getSSC_A.acres) AS SumOfacres, Sum(getSSC_A.impacres) AS SumOfimpacres, Count(getSSC_A.acres) AS [Number of SSCs]
FROM getSSC_A
WHERE ((([acres]-[impacres]<0)=True));


_DEL_MultiSCNodes
----------
DELETE [_MultiSCNodes].*
FROM _MultiSCNodes;


_DEL_R_Data_H1
----------
DELETE R_Data_H1.*
FROM R_Data_H1;


_DEL_T_data_E1
----------
DELETE T_Data_E1.*
FROM T_Data_E1;


_DEL_T_data_G1
----------
DELETE T_Data_G1.*
FROM T_Data_G1;


_DEL_T_data_G2
----------
DELETE T_Data_G2.*
FROM T_Data_G2;


_DEL_T_data_G4
----------
DELETE T_Data_G4.*
FROM T_Data_G4;


_DEL_T_data_G5
----------
DELETE T_Data_G5.*
FROM T_Data_G5;


_discotype
----------
SELECT mdl_dirSC_ac.PipeFlowTypeSan, mdl_dirSC_ac.PipeFlowTypeStorm, GetdiscoClass([pipeflowtypesan],[pipeflowtypestorm]) AS Expr1
FROM mdl_dirSC_ac
GROUP BY mdl_dirSC_ac.PipeFlowTypeSan, mdl_dirSC_ac.PipeFlowTypeStorm, GetdiscoClass([pipeflowtypesan],[pipeflowtypestorm])
ORDER BY mdl_dirSC_ac.PipeFlowTypeSan, mdl_dirSC_ac.PipeFlowTypeStorm;


_dsc_direct
----------
SELECT mdl_DirSC_ac.ParcelID, mdl_DirSC_ac.DivideID, mdl_DirSC_ac.RfAreaFt, mdl_DirSC_ac.ICFtRoof, [RfAreaFt]-[icftroof] AS RFdirect, mdl_DirSC_ac.PkAreaFt, mdl_DirSC_ac.ICFtParking, [PkAreaFt]-[icftparking] AS PKdirect
FROM mdl_DirSC_ac;


_dsc_direct_add_infilt_ICP
----------
INSERT INTO mdl_ic_Infilt_ac ( ParcelID, DivideID, SqFt, assumeKey, RoofRPark )
SELECT [_dsc_direct].ParcelID, [_dsc_direct].DivideID, [Pkdirect]*0.1 AS Expr1, "DJC1" AS Expr2, "P" AS Expr3
FROM _dsc_direct;


_dsc_direct_add_infilt_ICR
----------
INSERT INTO mdl_ic_Infilt_ac ( ParcelID, DivideID, SqFt, assumeKey, RoofRPark )
SELECT [_dsc_direct].ParcelID, [_dsc_direct].DivideID, [RFdirect]*0.25 AS Expr1, "DJC1" AS Expr2, "R" AS Expr3
FROM _dsc_direct;


_dsc_direct_add_SWplnt_ICP
----------
INSERT INTO mdl_ic_SWPlnt_ac ( ParcelID, DivideID, SqFt, assumeKey, RoofRPark )
SELECT [_dsc_direct].ParcelID, [_dsc_direct].DivideID, [Pkdirect]*0.1 AS Expr1, "DJC1" AS Expr2, "P" AS Expr3
FROM _dsc_direct;


_dsc_direct_add_SWplnt_ICR
----------
INSERT INTO mdl_ic_SWPlnt_ac ( ParcelID, DivideID, SqFt, assumeKey, RoofRPark )
SELECT [_dsc_direct].ParcelID, [_dsc_direct].DivideID, [RFdirect]*0.25 AS Expr1, "DJC1" AS Expr2, "R" AS Expr3
FROM _dsc_direct;


_dsc_GT_one
----------
SELECT mdl_DirSC_ac.ParcelID, mdl_DirSC_ac.DivideID, Count(mdl_DirSC_ac.MAPINFO_ID) AS CountOfMAPINFO_ID
FROM mdl_DirSC_ac
GROUP BY mdl_DirSC_ac.ParcelID, mdl_DirSC_ac.DivideID
HAVING (((Count(mdl_DirSC_ac.MAPINFO_ID))>1));


_getVortexValve
----------
SELECT [NGTO]+"C" AS Cnode, sc_surf.NGTO AS ExtNGTO, sc_surf.SurfSCID, CStr([surfscid])+"V" AS Vnode, CStr([surfscid])+"W" AS Vpipe, sc_surf.Vflow, sc_surf.Vfract
FROM sc_surf
WHERE (((sc_surf.Vfract)>0))
ORDER BY [NGTO]+"C", CStr([surfscid])+"V";


_PRE_NegSlopeOverride
----------
UPDATE mdl_SurfSC_ac, _R_Globals SET mdl_SurfSC_ac.Slope = [minslope]
WHERE (((mdl_SurfSC_ac.Slope)<[minslope]));


_SETdiscotype
----------
UPDATE mdl_dirSC_ac SET mdl_dirSC_ac.DiscoClass = GetdiscoClass([pipeflowtypesan],[pipeflowtypestorm]);


AverageBaseFlow
----------
SELECT sc_DD.NGTO, Sum(sc_DD.BaseFlow) AS SumOfBaseFlow
FROM sc_DD
GROUP BY sc_DD.NGTO;


Check:DuplicateNameW
----------
SELECT R_Data_H1.NAMEW, Count(R_Data_H1.NAMEW) AS CountOfNAMEW
FROM R_Data_H1
GROUP BY R_Data_H1.NAMEW
HAVING (((Count(R_Data_H1.NAMEW))>1));


Check:getUniqueQS
----------
SELECT R_Data_H1.JK
FROM R_Data_H1
GROUP BY R_Data_H1.JK;


Check:ImpAreaPerNode
----------
SELECT R_Data_H1.NGTO, Sum([Area]*[pctimp]/100) AS ImpArea
FROM R_Data_H1
GROUP BY R_Data_H1.NGTO
HAVING (((Sum([Area]*[pctimp]/100))>0));


E_export_A1
----------
SELECT "A1" AS ID, Chr(34) & [TitleText] & Chr(34) AS Title
FROM E_Control_A1;


E_export_B0
----------
SELECT "B0" AS ID, E_Control_B0.ISOL, E_Control_B0.KSUPER
FROM E_Control_B0;


E_Export_B1
----------
SELECT "B1" AS ID, E_Control_B1.NTCYC, E_Control_B1.DELT, E_Control_B1.TZERO, E_Control_B1.NSTART, E_Control_B1.INTER, E_Control_B1.JINTER, E_Control_B1.REDO
FROM E_Control_B1;


E_Export_B2
----------
SELECT "B2" AS ID, E_Control_B2.METRIC, E_Control_B2.NEQUAL, E_Control_B2.AMEN, E_Control_B2.ITMAX, E_Control_B2.SURTOL
FROM E_Control_B2;


E_export_B3
----------
SELECT "B3" AS ID, E_Control_B3.NHPRT, E_Control_B3.NQPRT, E_Control_B3.NPLT, E_Control_B3.LPLT, E_Control_B3.NJSW
FROM E_Control_B3;


E_export_BB
----------
SELECT "BB" AS ID, E_Control_BB.JELEV, E_Control_BB.JDOWN
FROM E_Control_BB;


E_export_C1
----------
SELECT "C1" AS ID, tblConduitData.Conduit_Name AS NCOND, Chr(34) & [Node_US] & Chr(34) AS Njunc1, Chr(34) & [Node_DS] & Chr(34) AS Njunc2, 0 AS Q, tblConduitData.Link_Type AS NKlass, 0 AS Afull, tblConduitData.Depth_or_Diameter AS Deep, tblConduitData.Width AS Wide, tblConduitData.Length AS Len, [Link_Up_Elevation]-[tblschematicdata].[node_invert_elevation] AS zp1, [Link_Down_Elevation]-[tblschematicdata_1].[node_invert_elevation] AS zp2, tblConduitData.Roughness AS Rough, 0 AS Stheta, 0 AS Sphi
FROM (tblConduitData INNER JOIN tblSchematicData ON tblConduitData.Node_US = tblSchematicData.Node) INNER JOIN tblSchematicData AS tblSchematicData_1 ON tblConduitData.Node_DS = tblSchematicData_1.Node;


E_export_Command
----------
SELECT E_Control_Command.Command
FROM E_Control_Command;


E_export_D1
----------
SELECT "D1" AS ID, Chr(34) & [Node] & Chr(34) AS Expr1, tblSchematicData.Ground_Elevation, tblSchematicData.Node_Invert_elevation, 0 AS Qinst, 0 AS Y, CLng([X_Coordinate]*100)/100 AS X_C, CLng([Y_Coordinate]*100)/100 AS Y_C
FROM tblSchematicData;


getDSC_I
----------
SELECT Count(sc_DD.MAPINFO_ID) AS Rcount, First(sc_DD.ddGageID) AS FirstOfGageID, CStr([NGTO]) AS ExtNGTO, mdl_ic_Infilt_ac.RoofRPark, Sum(CLng(([sqft])/43560*10000)/10000) AS area, Avg(([areaFT])^0.5) AS LengthBasis, 0 AS pervarea
FROM sc_DD INNER JOIN mdl_ic_Infilt_ac ON (sc_DD.DivideID = mdl_ic_Infilt_ac.DivideID) AND (sc_DD.ParcelID = mdl_ic_Infilt_ac.ParcelID)
GROUP BY CStr([NGTO]), mdl_ic_Infilt_ac.RoofRPark
HAVING (((Sum(CLng(([sqft])/43560*10000)/10000))>0));


getDSC_L
----------
SELECT Count(sc_DD.MAPINFO_ID) AS Rcount, First(sc_DD.ddGageID) AS FirstOfGageID, CStr([NGTO]) AS ExtNGTO, mdl_ic_SWPlnt_ac.RoofRPark, Sum(CLng(([sqft])/43560*10000)/10000) AS area, Avg(([areaFT])^0.5) AS LengthBasis, 0 AS pervarea
FROM sc_DD INNER JOIN mdl_ic_SWPlnt_ac ON (sc_DD.DivideID = mdl_ic_SWPlnt_ac.DivideID) AND (sc_DD.ParcelID = mdl_ic_SWPlnt_ac.ParcelID)
GROUP BY CStr([NGTO]), mdl_ic_SWPlnt_ac.RoofRPark
HAVING (((Sum(CLng(([sqft])/43560*10000)/10000))>0));


getDSC_PX
----------
SELECT Count(sc_DD.MAPINFO_ID) AS Rcount, First(sc_DD.ddGageID) AS FirstOfGageID, First([NGTO] & "PX") AS SC_Name, CStr([NGTO]) AS ExtNGTO, Sum(CLng(([pkareaft]-[sc_dd].[icftparking])/43560*10000)/10000) AS area, Avg(([areaFT])^0.5) AS LengthBasis, 0 AS pervarea
FROM sc_DD
GROUP BY CStr([NGTO])
HAVING (((Sum(CLng(([pkareaft]-[sc_dd].[icftparking])/43560*10000)/10000))>0));


getDSC_RX
----------
SELECT Count(sc_DD.MAPINFO_ID) AS Rcount, First(sc_DD.ddGageID) AS FirstOfGageID, First([NGTO] & "RX") AS SC_Name, CStr([NGTO]) AS ExtNGTO, Sum(CLng(([rfareaft]-[sc_DD].[ICFtRoof])/43560*10000)/10000) AS area, Avg(([areaFT])^0.5) AS LengthBasis, 0 AS pervarea
FROM sc_DD
GROUP BY CStr([NGTO])
HAVING (((Sum(CLng(([rfareaft]-[sc_DD].[ICFtRoof])/43560*10000)/10000))>0));


getDSC_W
----------
SELECT Count(sc_DD.MAPINFO_ID) AS Rcount, First(sc_DD.ddGageID) AS FirstOfGageID, First([NGTO] & [RoofRPark] & "W") AS SC_Name, CStr([NGTO]) AS ExtNGTO, mdl_ic_DryWell_ac.RoofRPark, Sum(CLng(([sqft])/43560*10000)/10000) AS area, Avg(([areaFT])^0.5) AS LengthBasis, 0 AS pervarea
FROM sc_DD INNER JOIN mdl_ic_DryWell_ac ON (sc_DD.DivideID = mdl_ic_DryWell_ac.DivideID) AND (sc_DD.ParcelID = mdl_ic_DryWell_ac.ParcelID)
GROUP BY CStr([NGTO]), mdl_ic_DryWell_ac.RoofRPark
HAVING (((Sum(CLng(([sqft])/43560*10000)/10000))>0));


getG01:Area
----------
SELECT Count(sc_DD.MAPINFO_ID) AS Rcount, First(sc_DD.GageID) AS FirstOfGageID, First([NGTO] & "G") AS SC_Name, CStr([NGTO]) AS ExtNGTO, Sum(CLng([rfgreen]*[roofacres]*100000)/100000) AS area
FROM sc_DD LEFT JOIN [SC_DD:errors] ON sc_DD.MAPINFO_ID = [SC_DD:errors].MAPINFO_ID
WHERE ((([SC_DD:errors].MAPINFO_ID) Is Null) AND (([rfgreen]*[roofacres])<>0))
GROUP BY CStr([NGTO]);


getSSC_A
----------
SELECT 1 AS Rcount, sc_surf.scgage, sc_surf.SurfSCID, Trim$(CStr([NGTO])) AS xNGTO, CLng([c_netacres]*100000)/100000 AS acres, CLng([c_netImpacres]*100000)/100000 AS impacres, CLng([width_subcatch]) AS w_sc, sc_surf.issumped, sc_surf.totalgrossacres, sc_surf.Slope AS SC_Slope_ft_ft, sc_surf.Imp_n, sc_surf.Per_n, sc_surf.Imp_sto, sc_surf.Per_sto, sc_surf.Suct, sc_surf.Hydcon, sc_surf.SMDMax
FROM sc_surf;


getSSC_T
----------
SELECT 1 AS Rcount, sc_surf.scgage, sc_surf.SurfSCID, Trim$(CStr([NGTO])) AS ExtNGTO, CLng([pavetreeacres]*100000)/100000 AS area, CLng(([pavetreeacres]*43560)^0.5/2) AS w_sc, sc_surf.issumped
FROM sc_surf
WHERE (((CLng([pavetreeacres]*100000)/100000)>0));


HCard_sums
----------
SELECT R_Data_H1.NGTO, Sum(R_Data_H1.Area) AS SumOfArea, Sum(R_Data_H1.PctImp) AS SumOfPctImp
FROM R_Data_H1
GROUP BY R_Data_H1.NGTO;


Integ01:parking fractions >1
----------
SELECT sc_DD.pkingacres, sc_DD.pkingtree, sc_DD.pkdrywell, sc_DD.pkdet, sc_DD.pkret, sc_DD.pkveg, [pkdrywell]+[pkdet]+[pkret]+[pkveg] AS SUMpkfract
FROM sc_DD
WHERE ((([pkdrywell]+[pkdet]+[pkret]+[pkveg])>1));


Integ02:roof fractions >1
----------
SELECT sc_DD.rnumber, sc_DD.LandUse, sc_DD.Comp_Gen, sc_DD.Comp_zoning, sc_DD.ObjectID, CInt([roofacres]*100000)/100000 AS rroofacres, CInt([rfgreen]*1000)/1000 AS rrfgreen, CInt([rfdrywell]*1000)/1000 AS rrfdrywell, CInt([rfdet]*1000)/1000 AS rrfdet, CInt([rfret]*1000)/1000 AS rrfret, CInt([rfveg]*1000)/1000 AS rrfveg, CInt(([rfdrywell]+[rfdet]+[rfret]+[rfveg]+[rfgreen])*1000)/1000 AS SUMrffract
FROM sc_DD
WHERE (((CInt(([rfdrywell]+[rfdet]+[rfret]+[rfveg]+[rfgreen])*1000)/1000)>1));


Integ03:dupNameW
----------
SELECT R_Data_H1.NAMEW, Count(R_Data_H1.NAMEW) AS CountOfNAMEW, Sum(R_Data_H1.Area) AS SumOfArea
FROM R_Data_H1
GROUP BY R_Data_H1.NAMEW
HAVING (((Count(R_Data_H1.NAMEW))>1));


R_build_H1_D_Detention
----------
INSERT INTO R_Data_H1 ( Source, Rcount, NAMEW, ExtNGTO, NGTO, Area, PctImp, Width )
SELECT "D" AS Source, [getD01:Area].Rcount, [getD01:Area].SC_Name, [getD01:Area].ExtNGTO, [ExtNGTO] & "D" AS Expr2, [getD01:Area].area, CLng((([area]-[pervarea])/[area])*10000)/100 AS pctinp, CInt(([area]*43560)^0.5/2) AS Expr1
FROM [getD01:Area]
WHERE ((([getD01:Area].area)>0));


R_build_H1_DSC_I
----------
INSERT INTO R_Data_H1 ( Source, JK, Rcount, NAMEW, ExtNGTO, NGTO, Area, PctImp, Width )
SELECT [RoofRPark] & "I" AS Source, getDSC_I.FirstOfGageID, getDSC_I.Rcount, [ExtNGTO] & [roofRpark] & "I" AS Expr2, getDSC_I.ExtNGTO, [ExtNGTO] & "I" AS Expr3, CLng([area]*1000)/1000 AS Expr1, CLng((([area]-[pervarea])/[area])*10000)/100 AS pctinp, CLng([area]*43560/(25+[LengthBasis]/6)) AS width
FROM getDSC_I
WHERE (((CLng([area]*1000)/1000)>0));


R_build_H1_DSC_L
----------
INSERT INTO R_Data_H1 ( Source, JK, Rcount, NAMEW, ExtNGTO, NGTO, Area, PctImp, Width )
SELECT [RoofRPark] & "L" AS Source, getDSC_L.FirstOfGageID, getDSC_L.Rcount, [ExtNGTO] & [roofRpark] & "L" AS Expr2, getDSC_L.ExtNGTO, [ExtNGTO] & "L" AS Expr3, CLng([area]*1000)/1000 AS Expr1, CLng((([area]-[pervarea])/[area])*10000)/100 AS pctinp, CLng([area]*43560/(25+[LengthBasis]/6)) AS width
FROM getDSC_L
WHERE (((CLng([area]*1000)/1000)>0));


R_build_H1_DSC_PX
----------
INSERT INTO R_Data_H1 ( Source, JK, Rcount, NAMEW, ExtNGTO, NGTO, Area, PctImp, Width )
SELECT "PX" AS Source, getDSC_PX.FirstOfGageID, getDSC_PX.Rcount, getDSC_PX.SC_Name, getDSC_PX.ExtNGTO, getDSC_PX.ExtNGTO, CLng([area]*1000)/1000 AS Expr1, CLng((([area]-[pervarea])/[area])*10000)/100 AS pctinp, CLng([area]*43560/(25+[LengthBasis]/6)) AS width
FROM getDSC_PX
WHERE (((CLng([area]*1000)/1000)>0));


R_build_H1_DSC_RX
----------
INSERT INTO R_Data_H1 ( Source, JK, Rcount, NAMEW, ExtNGTO, NGTO, Area, PctImp, Width )
SELECT "RX" AS Source, getDSC_RX.FirstOfGageID, getDSC_RX.Rcount, getDSC_RX.SC_Name, getDSC_RX.ExtNGTO, getDSC_RX.ExtNGTO, CLng([area]*1000)/1000 AS Expr1, CLng((([area]-[pervarea])/[area])*10000)/100 AS pctinp, CLng([area]*43560/(25+[LengthBasis]/6)) AS width
FROM getDSC_RX
WHERE (((CLng([area]*1000)/1000)>0));


R_build_H1_DSC_W
----------
INSERT INTO R_Data_H1 ( Source, JK, Rcount, NAMEW, ExtNGTO, NGTO, Area, PctImp, Width )
SELECT [RoofRPark] & "W" AS Source, getDSC_W.FirstOfGageID, getDSC_W.Rcount, getDSC_W.SC_Name, getDSC_W.ExtNGTO, [ExtNGTO] & "W" AS xNGTO, getDSC_W.area, CLng((([area]-[pervarea])/[area])*10000)/100 AS pctinp, CLng([area]*43560/(25+[LengthBasis]/6)) AS width
FROM getDSC_W
WHERE (((getDSC_W.area)>0));


R_build_H1_G_GreenRoof
----------
INSERT INTO R_Data_H1 ( Source, Rcount, NAMEW, ExtNGTO, NGTO, Area, PctImp, Width )
SELECT "G" AS Source, [getG01:Area].Rcount, [getG01:Area].SC_Name, [getG01:Area].ExtNGTO, [getG01:Area].ExtNGTO AS Expr2, [getG01:Area].area, 0 AS pctinp, CInt(([area]*43560)^0.5/2) AS Expr1
FROM [getG01:Area]
WHERE ((([getG01:Area].area)>0));


R_Build_H1_ICredirect
----------
UPDATE mdl_ic_NgtoRedir_ac INNER JOIN mdl_SurfSC_ac ON mdl_ic_NgtoRedir_ac.SurfSCID = mdl_SurfSC_ac.SurfSCID SET mdl_SurfSC_ac.ngto = [mdl_ic_NgtoRedir_ac].[ngto];


R_build_H1_R_Retention
----------
INSERT INTO R_Data_H1 ( Source, Rcount, NAMEW, ExtNGTO, NGTO, Area, PctImp, Width )
SELECT "R" AS Source, [getR01:Area].Rcount, [getR01:Area].SC_Name, [getR01:Area].ExtNGTO, [extNGTO] & "R" AS Expr2, CInt([area]*1000)/1000 AS Expr3, CLng((([area]-[pervarea])/[area])*10000)/100 AS pctinp, CInt(([area]*43560)^0.5/2) AS Expr1
FROM [getR01:Area]
WHERE (((CInt([area]*1000)/1000)>0));


R_Build_H1_rerouteSPL00
----------
DELETE [_RerouteSPL].*
FROM _RerouteSPL;


R_build_H1_rerouteSPL01
----------
INSERT INTO _RerouteSPL ( NAMEW, OLD_NGTO, USNode, ExtNGTO, NewNGTO )
SELECT R_Data_H1.NAMEW, R_Data_H1.NGTO AS OLD_NGTO, mdl_links_ac.USNode, R_Data_H1.ExtNGTO, [USNODE] & IIf(Mid$([R_DATA_H1.source],2,1)="W","W","") AS NewNGTO
FROM mdl_links_ac INNER JOIN R_Data_H1 ON mdl_links_ac.DSNode = R_Data_H1.ExtNGTO
WHERE (((R_Data_H1.ExtNGTO) Like "*SPL"));


R_Build_H1_rerouteSPL02
----------
UPDATE _RerouteSPL INNER JOIN R_Data_H1 ON [_RerouteSPL].NAMEW = R_Data_H1.NAMEW SET R_Data_H1.NGTO = [NewNGTO];


R_build_H1_SSC_A
----------
INSERT INTO R_Data_H1 ( Source, Rcount, NAMEW, JK, ExtNGTO, NGTO, Area, PctImp, Width, Slope, Imp_n, Per_n, Imp_Sto, Per_Sto, Suct, Hydcon, SMDmax )
SELECT "A" & IIf([issumped],"W","U") AS source, getSSC_A.Rcount, [SurfSCID] & IIf([issumped],"AW","AU") AS NameW, getSSC_A.scgage, getSSC_A.xNGTO, IIf([issumped],[xNGTO] & "W",[xNGTO]) AS ModelNGTO, getSSC_A.acres, CInt(IIf((([impacres])/[acres])*100>100,100,(([impacres])/[acres])*100)*100)/100 AS pctinp, getSSC_A.w_sc, CInt([SC_Slope_ft_ft]*10000)/10000 AS Expr3, getSSC_A.Imp_n, getSSC_A.Per_n, getSSC_A.Imp_sto, getSSC_A.Per_sto, getSSC_A.Suct, getSSC_A.Hydcon, getSSC_A.SMDMax
FROM getSSC_A
WHERE (((getSSC_A.acres)>0));


R_build_H1_SSC_T
----------
INSERT INTO R_Data_H1 ( Source, Rcount, JK, NAMEW, ExtNGTO, NGTO, Area, PctImp, Width )
SELECT "T" & IIf([issumped],"W","U") AS Source, getSSC_T.Rcount, getSSC_T.scgage, [SurfSCID] & IIf([issumped],"TW","TU") AS NameW, getSSC_T.ExtNGTO, IIf([issumped],[EXtNGTO] & "W",[EXtNGTO]) AS ModelNGTO, getSSC_T.area, 0 AS Pctimp, getSSC_T.w_sc
FROM getSSC_T
WHERE (((getSSC_T.area)>0));


R_build_H1_Z_Parameters
----------
UPDATE RunoffParameters INNER JOIN R_Data_H1 ON RunoffParameters.Source = R_Data_H1.Source SET R_Data_H1.Slope = [RunoffParameters].[Slope], R_Data_H1.Imp_n = [RunoffParameters].[Imp_n], R_Data_H1.Per_n = [RunoffParameters].[Per_n], R_Data_H1.Imp_Sto = [RunoffParameters].[Imp_Sto], R_Data_H1.Per_Sto = [RunoffParameters].[Per_Sto], R_Data_H1.Suct = [RunoffParameters].[Suct], R_Data_H1.Hydcon = [RunoffParameters].[Hydcon], R_Data_H1.SMDmax = [RunoffParameters].[SMDmax];


R_export_A1
----------
SELECT "A1 '" & [TitleText] & "'" AS Title
FROM R_control_A1;


R_export_B1
----------
SELECT "B1" AS ID, R_Control_B1.Metric, R_Control_B1.Isnow, IIf([designstorm?],1,[countofjk]) AS NRGAG, R_Control_B1.INFILM, R_Control_B1.KWALTY, R_Control_B1.IVAP, R_Control_B1.NHR, R_Control_B1.NMN, R_Control_B1.NDAY, R_Control_B1.MONTH, R_Control_B1.IYRSTR
FROM R_Control_B1, R_export_B1_02NRGAG;


R_export_B1_01UniqueRG
----------
SELECT R_Data_H1.JK
FROM R_Data_H1, _R_Globals
WHERE ((([exportW?] Or Mid$([Source],2,1)<>"W")=True))
GROUP BY R_Data_H1.JK;


R_export_B1_02NRGAG
----------
SELECT Count(R_export_B1_01UniqueRG.JK) AS CountOfJK
FROM R_export_B1_01UniqueRG;


R_export_B2
----------
SELECT "B2" AS ID, R_Control_B2.IPRN1, R_Control_B2.IPRN2, R_Control_B2.IPRN3
FROM R_Control_B2;


R_export_B3
----------
SELECT "B3" AS ID, R_Control_B3.Wet, R_Control_B3.WetDry, R_Control_B3.Dry, R_Control_B3.LUNIT, R_Control_B3.Long
FROM R_Control_B3;


R_export_B4
----------
SELECT "B4" AS ID, R_Control_B4.PCTZER, R_Control_B4.REGEN
FROM R_Control_B4;


R_export_Command
----------
SELECT R_Control_Command.Command
FROM R_Control_Command
ORDER BY R_Control_Command.Order;


R_export_D1
----------
SELECT "D1" AS ID, R_Control_D1.Ropt
FROM R_Control_D1;


R_export_E1
----------
SELECT "E1" AS ID, R_Control_E1.KTYPE, R_Control_E1.KINC, R_Control_E1.KPRINT, R_Control_E1.KTHIS, R_Control_E1.KTIME, R_Control_E1.KPREP, R_Control_E1.NHISTO, R_Control_E1.THISTO, R_Control_E1.TZRAIN
FROM R_Control_E1, R_Control_D1
WHERE (((R_Control_D1.Ropt)=0));


R_export_E3
----------
TRANSFORM Avg(R_Precip_Data.Precip) AS AvgOfPrecip
SELECT "E3" & Mid$(Space(8) & CStr([interval]),Len(Space(8) & CStr([interval]))-8,9) AS [ID Interval]
FROM R_Precip_Data, R_Control_D1
WHERE (((R_Control_D1.Ropt)=0))
GROUP BY "E3" & Mid$(Space(8) & CStr([interval]),Len(Space(8) & CStr([interval]))-8,9), R_Precip_Data.Interval
PIVOT "RG" & CStr([Gauge]);


R_export_END
----------
SELECT R_End.Command
FROM R_End
ORDER BY R_End.Order;


R_export_F1
----------
SELECT "F1" AS ID, R_Control_F1.VAP01, R_Control_F1.VAP02, R_Control_F1.VAP03, R_Control_F1.VAP04, R_Control_F1.VAP05, R_Control_F1.VAP06, R_Control_F1.VAP07, R_Control_F1.VAP08, R_Control_F1.VAP09, R_Control_F1.VAP10, R_Control_F1.VAP11, R_Control_F1.VAP12
FROM R_Control_F1;


R_export_Files
----------
SELECT "@ " & CStr([FILENUM]) & " '" & [FILENAM] & "'" AS Files
FROM R_Control_Files;


R_export_H1
----------
SELECT IIf(Not [exportW?] And (Mid$([Source],2,1)="W" Or Left$([NAMEW],7)="EXTERNS"),"*","") & " H1" AS ID, IIf([designstorm?],1,[jk]) AS JJK, "'" & [NAMEW] & "'" AS NNAMEW, "'" & [NGTO] & "'" AS NNGTO, R_Data_H1.Width, R_Data_H1.Area, R_Data_H1.PctImp, R_Data_H1.Slope, R_Data_H1.Imp_n, R_Data_H1.Per_n, R_Data_H1.Imp_Sto, R_Data_H1.Per_Sto, R_Data_H1.Suct, R_Data_H1.Hydcon, R_Data_H1.SMDmax
FROM R_Data_H1, R_Control_B1, _R_Globals
ORDER BY R_Data_H1.ExtNGTO, R_Data_H1.Source;



R_export_M1
----------
SELECT "M1" AS ID, R_Print_M1.NPRINT, R_Print_M1.ITNERV
FROM R_Print_M1;


R_export_M2
----------
SELECT "M2" AS ID, R_Print_M2.NDET, R_Print_M2.STARTP01, R_Print_M2.STOPPR01, R_Print_M2.STARTP02, R_Print_M2.STOPPR02, R_Print_M2.STARTP03, R_Print_M2.STOPPR03, R_Print_M2.STARTP04, R_Print_M2.STOPPR04, R_Print_M2.STARTP05, R_Print_M2.STOPPR05, R_Print_M2.STARTP06, R_Print_M2.STOPPR06, R_Print_M2.STARTP07, R_Print_M2.STOPPR07, R_Print_M2.STARTP08, R_Print_M2.STOPPR08, R_Print_M2.STARTP09, R_Print_M2.STOPPR09, R_Print_M2.STARTP10, R_Print_M2.STOPPR10
FROM R_Print_M2;


R_export_MM
----------
SELECT "MM" AS ID, R_Control_MM.NITCH, R_Control_MM.NSCRAT1, R_Control_MM.NSCRAT2, R_Control_MM.NSCRAT3, R_Control_MM.NSCRAT4, R_Control_MM.NSCRAT5, R_Control_MM.NSCRAT6, R_Control_MM.NSCRAT7
FROM R_Control_MM;


R_export_SW
----------
SELECT "SW" AS ID, R_Control_SW.Nblock, R_Control_SW.JIN1, R_Control_SW.JOUT1, R_Control_SW.JIN2, R_Control_SW.JOUT2
FROM R_Control_SW;


sc_dd
----------
SELECT IIf([discoclass]="D",IIf(IsNull([Ngtostorm]),"EXTERNS",[NgtoStorm]),IIf(IsNull([Ngtosan]),"EXTERNS",[Ngtosan])) AS NGTO, IIf([discoclass]="D",IIf(IsNumeric([mdl_nodes_ac_1].[gageid]),IIf(IsNull([mdl_nodes_ac_1].[gageid]),0,[mdl_nodes_ac_1].[gageid]),0),IIf(IsNumeric([mdl_nodes_ac].[gageid]),IIf(IsNull([mdl_nodes_ac].[gageid]),0,[mdl_nodes_ac].[gageid]),0)) AS ddGageID, mdl_dirSC_ac.*
FROM (mdl_dirSC_ac LEFT JOIN mdl_nodes_ac ON mdl_dirSC_ac.NGTOSan = mdl_nodes_ac.Node) LEFT JOIN mdl_nodes_ac AS mdl_nodes_ac_1 ON mdl_dirSC_ac.NGTOStorm = mdl_nodes_ac_1.Node;


SC_DD:errors
----------
SELECT sc_DD.ParcelID, sc_DD.MAPINFO_ID, sc_DD.ICFtRoof, sc_DD.ICFtParking, sc_DD.RfAreaFt, sc_DD.PkAreaFt, IIf([ICFtRoof]-[rfareaft]>1,[ICFtRoof]-[rfareaft],0) AS drf, IIf([ICFtParking]-[pkareaft]>0,[ICFtParking]-[pkareaft],0) AS dpk
FROM sc_DD
WHERE (((sc_DD.ICFtRoof)>[RFareaFt]) AND ((IIf([ICFtRoof]-[rfareaft]>1,[ICFtRoof]-[rfareaft],0))>1)) OR (((sc_DD.ICFtParking)>[PKareaFt]) AND ((IIf([ICFtParking]-[pkareaft]>0,[ICFtParking]-[pkareaft],0))>1));


sc_surf
----------
SELECT CLng(IIf([flowlengthft]<100,([totalgrossacres]*43560)^0.5,[totalgrossacres]*43560/[flowlengthft])) AS width_subcatch, CLng(IIf(IsNumeric([gageid]),IIf(IsNull([gageid]),0,[gageid]),0)) AS scgage, mdl_Surfsc_ac.SurfSCID, IIf(IsNull([mdl_ic_Ngtoredir_ac].[ngto]),[mdl_surfsc_ac].[ngto],[mdl_ic_Ngtoredir_ac].[ngto]) AS Ngto, mdl_Surfsc_ac.Slope, mdl_Surfsc_ac.FlowLengthFt, mdl_Surfsc_ac.dH, mdl_Surfsc_ac.Imp_n, mdl_Surfsc_ac.Per_n, mdl_Surfsc_ac.Imp_sto, mdl_Surfsc_ac.Per_sto, mdl_Surfsc_ac.Suct, mdl_Surfsc_ac.Hydcon, mdl_Surfsc_ac.SMDMax, mdl_Surfsc_ac.issumped, mdl_Surfsc_ac.totalgrossacres, mdl_Surfsc_ac.paveacres, mdl_Surfsc_ac.SWacres, mdl_Surfsc_ac.SWAcresOther, mdl_Surfsc_ac.SWfactor, mdl_Surfsc_ac.PaveTreeAcres, mdl_Surfsc_ac.TraceVisit, mdl_Surfsc_ac.UpdateAtts, mdl_Surfsc_ac.DataSource, mdl_Surfsc_ac.ROWAcres, mdl_Surfsc_ac.ROWAcresOther, mdl_Surfsc_ac.c_RFgrossacres, mdl_Surfsc_ac.c_RFtosurfacres, mdl_Surfsc_ac.c_RFtosurfIMPacres, mdl_Surfsc_ac.c_PKgrossacres, mdl_Surfsc_ac.c_PKtosurfacres, mdl_Surfsc_ac.c_PKtosurfIMPacres, mdl_Surfsc_ac.c_netacres, mdl_Surfsc_ac.c_netIMPacres
FROM (mdl_Surfsc_ac LEFT JOIN mdl_nodes_ac ON mdl_Surfsc_ac.NGTO = mdl_nodes_ac.Node) LEFT JOIN mdl_ic_NgtoRedir_ac ON mdl_Surfsc_ac.SurfSCID = mdl_ic_NgtoRedir_ac.SurfSCID;


SC_surf:errors
----------
SELECT sc_surf.MAPINFO_ID, sc_surf.SurfSCID AS ID, sc_surf.NGTO AS NGTOerror, sc_surf.totalgrossacres, sc_surf.c_netacres AS nettotalacreserror
FROM sc_surf
WHERE (((sc_surf.NGTO)="0" Or (sc_surf.NGTO) Is Null Or (sc_surf.NGTO)="") AND ((sc_surf.c_netacres)<=0)) OR (((sc_surf.NGTO)="0" Or (sc_surf.NGTO) Is Null Or (sc_surf.NGTO)="") AND ((sc_surf.c_netacres)<=0));


SC_surf:errors_save
----------
SELECT sc_surf.MAPINFO_ID, sc_surf.SurfSCID AS ID, sc_surf.NGTO AS NGTOerror, sc_surf.nettotalacres AS nettotalacreserror, [Slope]*100 AS pct_slope
FROM sc_surf
WHERE (((sc_surf.NGTO)="0" Or (sc_surf.NGTO) Is Null Or (sc_surf.NGTO)="")) OR (((sc_surf.nettotalacres)<=0));


SC_SURF_string
----------
SELECT Trim(Str$([NGTO])) AS strngto, sc_surf.*
FROM sc_surf;


sc_surf_sums
----------
SELECT SC_SURF_string.strngto, SC_SURF_string.issumped_base, SC_SURF_string.isvortexvalvebase, SC_SURF_string.Basin, Sum(SC_SURF_string.totalgrossacres) AS SumOftotalgrossacres, Count(SC_SURF_string.strngto) AS CountOfstrngto, SC_SURF_string.paveacres, Sum(SC_SURF_string.pavetreeac_curr) AS SumOfpavetreeac_curr, Sum(SC_SURF_string.rfareagross) AS SumOfrfareagross, Sum(SC_SURF_string.rfareasurface) AS SumOfrfareasurface, Sum(SC_SURF_string.rfareaimp) AS SumOfrfareaimp, Sum(SC_SURF_string.pkingareagross) AS SumOfpkingareagross, Sum(SC_SURF_string.pkingareasurface) AS SumOfpkingareasurface, Sum(SC_SURF_string.pkingareaimp) AS SumOfpkingareaimp, Sum(SC_SURF_string.netimpervarea) AS SumOfnetimpervarea, Sum(SC_SURF_string.nettotalacres) AS SumOfnettotalacres
FROM SC_SURF_string
GROUP BY SC_SURF_string.strngto, SC_SURF_string.issumped_base, SC_SURF_string.isvortexvalvebase, SC_SURF_string.Basin, SC_SURF_string.paveacres;


T_Build_B1_01:initNNYN,NNPE,NOUTS
----------
UPDATE T_Control_B1 SET T_Control_B1.NNYN = 0, T_Control_B1.NNPE = 0, T_Control_B1.NOUTS = 0;


T_build_E1_ALL:CreateNonSumpedExtran
----------
INSERT INTO T_Data_E1 ( NOE, ExNGTO, Ntype, Source, ExportOrder )
SELECT R_Data_H1.ExtNGTO, First(R_Data_H1.ExtNGTO) AS FirstOfExtNGTO, First(19) AS Expr1, First("-") AS Expr2, 9 AS Expr3
FROM R_Data_H1
WHERE (((R_Data_H1.Source)<>"S"))
GROUP BY R_Data_H1.ExtNGTO, 9
ORDER BY R_Data_H1.ExtNGTO;


T_Build_E1_ALL:CreateSumpedDeadEnds
----------
INSERT INTO T_Data_E1 ( NOE, ExNGTO, Ntype, Source, ExportOrder )
SELECT [ExtNGTO]+"S" AS Expr3, First(R_Data_H1.ExtNGTO) AS FirstOfExtNGTO, First(19) AS Expr1, First("S") AS Expr2, 9 AS Expr4
FROM R_Data_H1
WHERE (((R_Data_H1.Source)="S"))
GROUP BY [ExtNGTO]+"S", 9
ORDER BY [ExtNGTO]+"S";


T_build_E1_D01:CreateDetNode
----------
INSERT INTO T_Data_E1 ( Source, ExNGTO, NOE, Ntype, ExportOrder )
SELECT R_Data_H1.Source, R_Data_H1.ExtNGTO, R_Data_H1.NGTO, 19 AS Expr1, 4 AS Expr2
FROM R_Data_H1
WHERE (((R_Data_H1.Source)="D"));


T_build_E1_D02:DetentionPipe
----------
INSERT INTO T_Data_E1 ( Source, ExNGTO, NOE, NUE1, Ntype, Dist, GEOM1, Slope, Rough, ExportOrder )
SELECT First(R_Data_H1.Source) AS FirstOfSource, First(R_Data_H1.ExtNGTO) AS FirstOfExtNGTO, First([ExtNGTO] & "E") AS Expr3, R_Data_H1.NGTO, First(1) AS type, First(100) AS Expr1, CLng(Sum([Area]*([pctimp]/100)*0.52)^0.375*10000)/10000 AS Dia, First(1) AS PipeSlopePct, First(0.014) AS Rough, 5 AS Expr2
FROM R_Data_H1
GROUP BY R_Data_H1.NGTO, 5
HAVING (((First(R_Data_H1.Source))="D"));


T_build_E1_D03:OverflowSetNUE3
----------
UPDATE T_Data_E1 INNER JOIN T_Data_E1 AS T_Data_E1_1 ON T_Data_E1.ExNGTO = T_Data_E1_1.ExNGTO SET T_Data_E1.NUE3 = [T_Data_E1_1].[NOE]
WHERE (((T_Data_E1.Source)="-") AND ((T_Data_E1_1.Source)="D") AND ((T_Data_E1_1.Ntype)=1));


T_build_E1_R01:CreateRetNode
----------
INSERT INTO T_Data_E1 ( Source, ExNGTO, NOE, Ntype, ExportOrder )
SELECT R_Data_H1.Source, R_Data_H1.ExtNGTO, R_Data_H1.NGTO, 22 AS Expr1, 1 AS Expr2
FROM R_Data_H1
WHERE (((R_Data_H1.Source)="R"));


T_build_E1_R02:PumpToGroundNode
----------
INSERT INTO T_Data_E1 ( ExNGTO, NOE, NUE1, Ntype, Source, ExportOrder )
SELECT R_Data_H1.ExtNGTO, [ExtNGTO] & "Z" AS Expr2, R_Data_H1.NGTO, 19 AS Expr1, "Z" AS Expr3, 2 AS Expr4
FROM R_Data_H1
WHERE (((R_Data_H1.Source)="R"));


T_build_E1_R03:OverflowSetNUE2
----------
UPDATE T_Data_E1 INNER JOIN T_Data_E1 AS T_Data_E1_1 ON T_Data_E1.ExNGTO = T_Data_E1_1.ExNGTO SET T_Data_E1.NUE2 = [T_DATA_E1_1].[NOE]
WHERE (((T_Data_E1.Source)="-") AND ((T_Data_E1.Ntype)=19) AND ((T_Data_E1_1.Ntype)=22) AND ((T_Data_E1_1.Source)="R"));


T_build_E1_V04:CreateVortexNode
----------
INSERT INTO T_Data_E1 ( Source, ExNGTO, NOE, Ntype, ExportOrder )
SELECT "V" AS Expr1, [_getVortexValve].ExtNGTO, [_getVortexValve].Vnode, 19 AS Expr3, 3 AS Expr2
FROM _getVortexValve;


T_build_E1_V05:VortexPipe
----------
INSERT INTO T_Data_E1 ( Source, ExNGTO, NOE, NUE1, Ntype, Dist, GEOM1, Slope, Rough, ExportOrder )
SELECT "V" AS src, [_getVortexValve].ExtNGTO, [_getVortexValve].Vpipe, [_getVortexValve].Vnode, 1 AS type, 100 AS dist, CLng(([vflow]/3.317224)^0.375*1000)/1000 AS Dia, 1 AS PipeSlopePct, 0.014 AS Rough, 4 AS Expr1
FROM _getVortexValve;


T_build_E1_V09:CreateCombineNode
----------
INSERT INTO T_Data_E1 ( Source, ExNGTO, NOE, Ntype, ExportOrder )
SELECT "V" AS Expr1, [_getVortexValve].ExtNGTO, [_getVortexValve].Cnode, 19 AS Expr2, 5 AS Expr3
FROM _getVortexValve
GROUP BY [_getVortexValve].ExtNGTO, [_getVortexValve].Cnode, 19, 5;


T_Build_E1_V10:LinkCombineNode
----------
UPDATE _SurfVVScID INNER JOIN T_Data_E1 ON [_SurfVVScID].NOE = T_Data_E1.NOE SET T_Data_E1.NUE1 = [_SurfVVScID].[NUE1], T_Data_E1.NUE2 = [_SurfVVScID].[NUE2], T_Data_E1.NUE3 = [_SurfVVScID].[NUE3];


T_build_E1_V99:CheckSetNUE1
----------
SELECT T_Data_E1_1.NOE, T_Data_E1_1.Source, T_Data_E1.ExNGTO, T_Data_E1.Source, [_SurfVVScID].NOE
FROM (T_Data_E1 INNER JOIN _SurfVVScID ON T_Data_E1.NOE = [_SurfVVScID].NOE) INNER JOIN T_Data_E1 AS T_Data_E1_1 ON T_Data_E1.ExNGTO = T_Data_E1_1.ExNGTO
WHERE (((T_Data_E1_1.Source)="-") AND ((T_Data_E1.Source)="V"));


T_build_E1_V99:PassThruSetNUE1
----------
UPDATE (T_Data_E1 INNER JOIN _SurfVVScID ON T_Data_E1.NOE = [_SurfVVScID].NOE) INNER JOIN T_Data_E1 AS T_Data_E1_1 ON T_Data_E1.ExNGTO = T_Data_E1_1.ExNGTO SET T_Data_E1_1.NUE1 = [_SurfVVScID].[NOE]
WHERE (((T_Data_E1_1.Source)="-") AND ((T_Data_E1.Source)="V"));


T_build_G1_R01:3Pumps
----------
INSERT INTO T_Data_G1 ( Source, STORENOE, LOUT )
SELECT T_Data_E1.Source, T_Data_E1.NOE, 4 AS Expr1
FROM T_Data_E1
WHERE (((T_Data_E1.Source)="R"));


T_Build_G2_R01:RetStorageOne
----------
INSERT INTO T_Data_G2 ( Source, NAMEW, STORENOE, ExNGTO, TSDEP, TSAREA, TSTORE, TSQOU )
SELECT R_Data_H1.Source, R_Data_H1.NAMEW, R_Data_H1.NGTO, R_Data_H1.ExtNGTO, 0 AS depth1, [Area]*43560*[unitsa_acre] AS Area1, 0 AS stor1, 0 AS q1
FROM R_Data_H1, _T_Globals
WHERE (((R_Data_H1.Source)="R"))
ORDER BY R_Data_H1.NAMEW;


T_Build_G2_R02:RetStorageTwo
----------
INSERT INTO T_Data_G2 ( Source, NAMEW, STORENOE, ExNGTO, TSDEP, TSAREA, TSTORE, TSQOU )
SELECT R_Data_H1.Source, R_Data_H1.NAMEW, R_Data_H1.NGTO, R_Data_H1.ExtNGTO, [_T_Globals].Depth_ft AS depth, [Area]*43560*[UnitSA_acre] AS SurfArea, 0 AS storage, 0 AS qout
FROM R_Data_H1, _T_Globals
WHERE (((R_Data_H1.Source)="R"))
ORDER BY R_Data_H1.NAMEW;


T_Build_G4_R01:PumpToGroundRates
----------
INSERT INTO T_Data_G4 ( Source, NAMEW, STORENOE, ExNGTO, TDSTAR1, TDSTAR2, TDSTAR3, TDPUMP1, TDPUMP2, TDPUMP3, TDSTOP, ovrflo )
SELECT R_Data_H1.Source, R_Data_H1.NAMEW, R_Data_H1.NGTO, R_Data_H1.ExtNGTO, 0.05 AS depth1, 0.5*[depth_ft] AS depth2, [_T_Globals].Depth_ft AS depth3, IIf(CLng(1000000*[area]*[unitSA_acre]*43560*[infrate_IPH]/(12*1440))/1000000=0,0.000001,CLng(1000000*[area]*[unitSA_acre]*43560*[infrate_IPH]/(12*1440))/1000000) AS q1, IIf(CLng(1000000*[area]*[unitSA_acre]*43560*[infrate_IPH]/(12*1440))/1000000=0,0.000001,CLng(1000000*[area]*[unitSA_acre]*43560*[infrate_IPH]/(12*1440))/1000000) AS q2, IIf(CLng(1000000*[area]*[unitSA_acre]*43560*[infrate_IPH]/(12*1440))/1000000=0,0.000001,CLng(1000000*[area]*[unitSA_acre]*43560*[infrate_IPH]/(12*1440))/1000000) AS q3, 0 AS stop, R_Data_H1.ExtNGTO
FROM R_Data_H1, _T_Globals
WHERE (((R_Data_H1.Source)="R"))
ORDER BY R_Data_H1.NAMEW;


T_build_G5_R01:InitialStore
----------
INSERT INTO T_Data_G5 ( Source, STORENOE, NAMEW, ExNGTO, STORL )
SELECT R_Data_H1.Source, R_Data_H1.NGTO, R_Data_H1.NAMEW, R_Data_H1.ExtNGTO, 0 AS Expr1
FROM R_Data_H1
WHERE (((R_Data_H1.Source)="R"))
ORDER BY R_Data_H1.NAMEW;


T_export_A1
----------
SELECT "A1 '" & [TitleText] & "'" AS Title
FROM T_Control_A1;


T_export_B1
----------
SELECT "B1" AS ID, T_Control_B1.NDT, T_Control_B1.NINPUT, IIf([t_getnnyn].[CountNOE]=1,0,[t_getnnyn].[countnoe]) AS C_NNYN, IIf([t_getnnpe].[CountNOE]=1,0,[t_getnnpe].[CountNOE]) AS C_NNPE, T_getNouts.CountOfchrExtNGTO AS CountNOUTS, T_Control_B1.NPRINT, T_Control_B1.NPOLL, T_Control_B1.NITER, T_Control_B1.IDATEZ, T_Control_B1.METRIC, T_Control_B1.INTPRT
FROM T_Control_B1, T_getNouts, T_getNNYN, T_getNNPE;


T_export_B2
----------
SELECT "B2" AS ID, T_Control_B2.DT, T_Control_B2.EPSIL, T_Control_B2.DWDAYS, T_Control_B2.TZERO, T_Control_B2.GNU, T_Control_B2.TRIBA
FROM T_Control_B2;


T_export_B3
----------
SELECT "B3" AS ID, T_Control_B3.NCNTRL, T_Control_B3.NINFIL, T_Control_B3.NFILTH, T_Control_B3.NDESN
FROM T_Control_B3;


T_export_C1
----------
SELECT "C1" AS ID, T_Control_C1.NKLASS, T_Control_C1.KPRINT
FROM T_Control_C1;


T_export_Command
----------
SELECT T_Control_Command.Command
FROM T_Control_Command
ORDER BY T_Control_Command.Order;


T_export_E1
----------
SELECT "E1" AS ID, "'" & [NOE] & "'" AS CNOE, IIf(IsNull([NUE1]),Chr(39) & " " & Chr(39),"'" & [NUE1] & "'") AS CNUE1, IIf(IsNull([NUE2]),Chr(39) & " " & Chr(39),"'" & [NUE2] & "'") AS CNUE2, IIf(IsNull([NUE3]),Chr(39) & " " & Chr(39),"'" & [NUE3] & "'") AS CNUE3, T_Data_E1.Ntype, T_Data_E1.Dist, T_Data_E1.GEOM1, T_Data_E1.Slope, T_Data_E1.Rough, T_Data_E1.GEOM2, T_Data_E1.Barrel, T_Data_E1.GEOM3, T_Data_E1.KGEOM
FROM T_Data_E1
ORDER BY T_Data_E1.ExNGTO, T_Data_E1.ExportOrder;


T_export_E1_notsoold
----------
SELECT "E1" AS ID, "'" & [NOE] & "'" AS CNOE, IIf(IsNull([NUE1]),Chr(39) & " " & Chr(39),"'" & [NUE1] & "'") AS CNUE1, IIf(IsNull([NUE2]),Chr(39) & " " & Chr(39),"'" & [NUE2] & "'") AS CNUE2, IIf(IsNull([NUE3]),Chr(39) & " " & Chr(39),"'" & [NUE3] & "'") AS CNUE3, T_Data_E1.Ntype
FROM T_Data_E1
ORDER BY "'" & [NOE] & "'";


T_export_E1_old
----------
SELECT "E1" AS ID, T_Data_E1.NOE, T_Data_E1.NUE1, T_Data_E1.NUE2, T_Data_E1.NUE3, T_Data_E1.Ntype
FROM T_Data_E1
ORDER BY T_Data_E1.NOE;


T_export_files
----------
SELECT "@ " & CStr([FILENUM]) & " '" & [FILENAM] & "'" AS Files
FROM T_Control_Files;


T_export_G1245
----------
SELECT T_export_unionG1245.ID, T_export_unionG1245.CardData
FROM T_export_unionG1245;


T_export_getG1
----------
SELECT T_Data_E1.NOE, "G1" AS ID, T_Data_G1.LOUT AS CardData
FROM T_Data_E1 INNER JOIN T_Data_G1 ON (T_Data_E1.Source = T_Data_G1.Source) AND (T_Data_E1.NOE = T_Data_G1.STORENOE)
WHERE (((T_Data_E1.Ntype)=22))
ORDER BY T_Data_E1.NOE;


T_export_getG2
----------
SELECT T_Data_E1.NOE, "G2" AS ID, [TSDEP] & " " & [tsarea] & " " & [tstore] & " " & [Tsqou] AS CardData
FROM (T_Data_G1 INNER JOIN T_Data_E1 ON (T_Data_G1.Source = T_Data_E1.Source) AND (T_Data_G1.STORENOE = T_Data_E1.NOE)) INNER JOIN T_Data_G2 ON (T_Data_E1.Source = T_Data_G2.Source) AND (T_Data_E1.NOE = T_Data_G2.STORENOE)
WHERE (((T_Data_G1.LOUT)>2) AND ((T_Data_E1.Ntype)=22))
ORDER BY T_Data_E1.NOE, T_Data_G2.TSAREA;


T_export_getG4
----------
SELECT T_Data_E1.NOE, "G4" AS ID, [T_Data_G4].[TDSTAR1] & " " & [T_Data_G4].[TDSTAR2] & " " & IIf([lout]>3,[TDSTAR3],"") & " " & IIf([lout]>4,[TDSTAR4],"") & " " & IIf([lout]>5,[TDSTAR5],"") & " " & [T_Data_G4].[TDPUMP1] & " " & [T_Data_G4].[TDPUMP2] & " " & IIf([lout]>3,[TDPUMP3],"") & " " & IIf([lout]>4,[TDPUMP4],"") & " " & IIf([lout]>5,[TDPUMP5],"") & " " & [T_Data_G4].[TDSTOP] & " " & [T_Data_G4].[ovrflo] AS CardData
FROM T_Data_G1 INNER JOIN (T_Data_E1 INNER JOIN T_Data_G4 ON (T_Data_E1.NOE = T_Data_G4.STORENOE) AND (T_Data_E1.Source = T_Data_G4.Source)) ON (T_Data_G1.STORENOE = T_Data_E1.NOE) AND (T_Data_G1.Source = T_Data_E1.Source)
WHERE (((T_Data_G1.LOUT)>2) AND ((T_Data_E1.Ntype)=22))
ORDER BY T_Data_E1.NOE;


T_export_getG4_old
----------
SELECT "G4" AS ID, T_Data_G4.TDSTAR1, T_Data_G4.TDSTAR2, IIf([lout]>3,[TDSTAR3],"") AS STAR3, IIf([lout]>4,[TDSTAR4],"") AS STAR4, IIf([lout]>5,[TDSTAR5],"") AS STAR5, T_Data_G4.TDPUMP1, T_Data_G4.TDPUMP2, IIf([lout]>3,[TDPUMP3],"") AS PUMP3, IIf([lout]>4,[TDPUMP4],"") AS PUMP4, IIf([lout]>5,[TDPUMP5],"") AS PUMP5, T_Data_G4.TDSTOP
FROM T_Data_G1 INNER JOIN (T_Data_E1 INNER JOIN T_Data_G4 ON (T_Data_E1.ExNGTO = T_Data_G4.ExNGTO) AND (T_Data_E1.NAMEW = T_Data_G4.NAMEW) AND (T_Data_E1.Source = T_Data_G4.Source)) ON (T_Data_G1.ExNGTO = T_Data_E1.ExNGTO) AND (T_Data_G1.NAMEW = T_Data_E1.NAMEW) AND (T_Data_G1.Source = T_Data_E1.Source)
WHERE (((T_Data_G1.LOUT)>2) AND ((T_Data_E1.Ntype)=22))
ORDER BY T_Data_E1.NOE;


T_export_getG5
----------
SELECT T_Data_E1.NOE, "G5" AS ID, T_Data_G5.STORL AS CardData
FROM T_Data_E1 INNER JOIN T_Data_G5 ON (T_Data_E1.Source = T_Data_G5.Source) AND (T_Data_E1.NOE = T_Data_G5.STORENOE)
WHERE (((T_Data_E1.Ntype)=22))
ORDER BY T_Data_E1.NOE;


T_export_H1_First
----------
SELECT "H1" AS ID, First(T_getchrExtNGTO.chrExtNGTO) AS FirstOfchrExtNGTO
FROM T_getchrExtNGTO
GROUP BY "H1";


T_export_H1_Remaining
----------
SELECT "  " AS Expr1, T_getchrExtNGTO.chrExtNGTO
FROM T_getchrExtNGTO LEFT JOIN T_export_H1_First ON T_getchrExtNGTO.chrExtNGTO = T_export_H1_First.FirstOfchrExtNGTO
WHERE (((T_export_H1_First.FirstOfchrExtNGTO) Is Null))
GROUP BY "  ", T_getchrExtNGTO.chrExtNGTO
ORDER BY T_getchrExtNGTO.chrExtNGTO;


T_export_J1_First
----------
SELECT "J1" AS ID, First(T_getInputVRD.NOE) AS FirstOfNOE
FROM T_getInputVRD
GROUP BY "J1";


T_export_J1_Remaining
----------
SELECT "  " AS Expr1, T_getInputVRD.NOE
FROM T_getInputVRD LEFT JOIN T_export_J1_First ON T_getInputVRD.NOE = T_export_J1_First.FirstOfNOE
WHERE (((T_export_J1_First.FirstOfNOE) Is Null))
GROUP BY "  ", T_getInputVRD.NOE
ORDER BY T_getInputVRD.NOE;


T_export_J2_First
----------
SELECT "J2" AS ID, First(T_getOutputVRD.NOE) AS FirstOfNOE
FROM T_getOutputVRD
GROUP BY "J2";


T_export_J2_Remaining
----------
SELECT "  " AS Expr1, T_getOutputVRD.NOE
FROM T_getOutputVRD LEFT JOIN T_export_J2_First ON T_getOutputVRD.NOE = T_export_J2_First.FirstOfNOE
WHERE (((T_export_J2_First.FirstOfNOE) Is Null))
GROUP BY "  ", T_getOutputVRD.NOE
ORDER BY T_getOutputVRD.NOE;


T_export_MM
----------
SELECT "MM" AS ID, T_Control_MM.NITCH, T_Control_MM.NSCRAT1, T_Control_MM.NSCRAT2, T_Control_MM.NSCRAT3, T_Control_MM.NSCRAT4, T_Control_MM.NSCRAT5, T_Control_MM.NSCRAT6, T_Control_MM.NSCRAT7
FROM T_Control_MM;


T_export_SW
----------
SELECT "SW" AS ID, T_Control_SW.Nblock, T_Control_SW.JIN1, T_Control_SW.JOUT1, T_Control_SW.JIN2, T_Control_SW.JOUT2
FROM T_Control_SW;


T_export_unionG1245
----------
SELECT * from T_export_getG1
UNION SELECT * from T_export_getG2;
UNION SELECT * from T_export_getG4;
UNION SELECT * from T_export_getG5;


T_getchrExtNGTO
----------
SELECT "'" & [ExtNGTO] & "'" AS chrExtNGTO
FROM R_Data_H1
WHERE (((R_Data_H1.Source)<>"S"));


T_getInputVRD
----------
SELECT T_Data_E1.NOE
FROM T_Data_E1
WHERE (((T_Data_E1.Source)="R" Or (T_Data_E1.Source)="V" Or (T_Data_E1.Source)="W"))
GROUP BY T_Data_E1.NOE;


T_getNNPE
----------
SELECT 1 AS Recno, 1+Count([T_export_J2_Remaining].[NOE]) AS CountNOE
FROM T_export_J2_Remaining;


T_getNNYN
----------
SELECT 1 AS Recno, 1+Count([T_export_J1_Remaining].[NOE]) AS CountNOE
FROM T_export_J1_Remaining;


T_getNouts
----------
SELECT 1 AS Recno, 1+Count([T_export_H1_Remaining].[chrExtNGTO]) AS CountOfchrExtNGTO
FROM T_export_H1_Remaining;


T_getOutputVRD
----------
SELECT T_Data_E1.NOE
FROM (T_Data_E1 LEFT JOIN T_Data_E1 AS T_Data_E1_2 ON T_Data_E1.NUE2 = T_Data_E1_2.NOE) LEFT JOIN T_Data_E1 AS T_Data_E1_1 ON T_Data_E1.NUE1 = T_Data_E1_1.NOE
GROUP BY T_Data_E1.NOE, T_Data_E1_1.NOE, T_Data_E1_2.NOE
HAVING (((T_Data_E1_1.NOE) Is Not Null)) OR (((T_Data_E1_2.NOE) Is Not Null));


tblSchematicData
----------
SELECT AllNodeFromLink.Node, Avg(AllNodeFromLink.X) AS X_Coordinate, Avg(AllNodeFromLink.Y) AS Y_Coordinate, Max(AllNodeFromLink.GRELEV) AS Ground_Elevation, Min(AllNodeFromLink.Z) AS Node_Invert_elevation, 134 AS Node_Type, Count(AllNodeFromLink.Node) AS CountOfNode
FROM AllNodeFromLink
GROUP BY AllNodeFromLink.Node, 134
ORDER BY AllNodeFromLink.Node;


UpNodeFromLink
----------
SELECT mdl_sewers_traced_ac.Unitid AS Node, mdl_sewers_traced_ac.From_x AS X, mdl_sewers_traced_ac.From_y AS Y, CLng([Ups_Grnd_Elev]*100)/100 AS GRELEV, CLng([Upselev]*100)/100 AS Z
FROM mdl_sewers_traced_ac
ORDER BY mdl_sewers_traced_ac.Unitid;


