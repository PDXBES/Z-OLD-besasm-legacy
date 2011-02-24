__test_DHI_REGULATORS
----------
SELECT DHITabularDataLinks.LinkID, 4 AS TypeNo, DHILinks.FromNode AS ControlNodeA, DHILinks.ToNode AS ControlNodeB, DHITabularDataLinks.LinkID AS DataSetID
FROM DHITabularDataLinks INNER JOIN DHILinks ON DHITabularDataLinks.LinkID = DHILinks.LinkID;


_ALLNodeIEFromLink
----------
SELECT  * from _getminupelev
union select * from _getminupelev_spec 
UNION SELECT  * from _getmindnelev;


_ALLXPSpecialLinks
----------
SELECT isSpecLink, LinkID, MLinkID, CompKey, USNode, DSNode
FROM _getParallelLinkKeys

UNION SELECT isSpecLink, LinkID, MLinkID, CompKey, USNode, DSNode
FROM _getSpecialLinkKeys;


_BaseFlowComponents
----------
SELECT mdl_DirSC_ac.NGTOSan, Round(Sum(CLng([baseflow]*10000000)/10000000),6) AS avesanflow, Round(Sum(CLng(([AreaFT]/43560)*10000000)/10000000),6) AS IIArea, Round(Sum(CLng(([AreaFT]/43560)*0.00000155*[iigpad]*10000000)/10000000),6) AS DMII
FROM mdl_DirSC_ac, _Globals
WHERE (((mdl_DirSC_ac.PipeFlowTypeSan)="C" Or (mdl_DirSC_ac.PipeFlowTypeSan)="S"))
GROUP BY mdl_DirSC_ac.NGTOSan
HAVING (((Sum(mdl_DirSC_ac.BaseFlow))>0.0000001));


_dhi_ALLNodeIEFromLink
----------
SELECT  * from _dhi_getminupelev
union select * from _dhi_getminupelev_spec 
UNION SELECT  * from _dhi_getmindnelev;


_dhi_append_baseflow
----------
INSERT INTO DHIBaseFLow ( Ltype, Dlocation, Part_ID, Item_ID, Group_ID, Group_Name, Cmethod, EFraction, Bvalue, DWF_Units, SMflag, RID )
SELECT 3 AS LTYPE, [_BaseFlowComponents].NGTOSan AS DLOCATION, 1 AS PART_ID, 1 AS ITEM_ID, 1 AS GROUP_ID, "ADWF" AS GROUP_NAME, 1 AS CMETHOD, 100 AS EFRACTION, [_BaseFlowComponents].avesanflow AS BVALUE, "[cfs]" AS DWF_UNITS, "" AS SMFLAG, "" AS RID
FROM _BaseFlowComponents;


_dhi_append_links
----------
INSERT INTO DHILinks ( LinkID, FromNode, ToNode, TypeNo, CRSID, ScalingTYPENO, [Diameter/Width], ScaleOrWidth, Height, MaterialNo, UpLevel, DwLevel, Infiltration, SpecifiedLength, RMapproachNO )
SELECT xlinks.explink AS simlink, IIf(IsNull([_tXPSpecialLinks].[toLinkID]),[xlinks].[usnode],[_tXPSpecialLinks].[DSnode]) AS USNODE, xlinks.DSNode, IIf(IsNull([DHITypeNO]),0,[DHITypeNO]) AS xTypeNo, IIf(IsNull([_pipeShapes].[shape]),"ERR",IIf([DHITYPENO]=2,[_pipeshapes].[shape]+"_"+Format$([Diamwidth],"0")+"_"+Format$([height],"0"),Null)) AS xDHICRSID, 1 AS STno, CLng(1000*[DiamWidth]/12)/1000 AS DW, IIf(IsNull([DHITypeNO]),1,IIf([DHITypeNO]>2,CLng(1000*[Height]/12)/1000,1)) AS SoeW, CLng(1000*[Height]/12)/1000 AS HGT, IIf(IsNull([DHITypeno]),3,IIf([DHITypeno]=1,2,3)) AS MatNo, xlinks.USIE, xlinks.DSIE, 0 AS infl, CLng([Length]) AS xlen, IIf([PipeFlowType]="F",2,1) AS GorF
FROM (xlinks LEFT JOIN _PipeShapes ON xlinks.PipeShape = [_PipeShapes].Shape) LEFT JOIN _tXPSpecialLinks ON xlinks.LinkID = [_tXPSpecialLinks].toLinkID;


_dhi_append_metadata
----------
INSERT INTO _DHIMetaData ( Created, FullPath )
SELECT Date()+Time() AS Expr1, [Currentdb].[name] AS Expr2
FROM _Globals;


_dhi_append_nodes
----------
INSERT INTO DHINodes ( NodeID, TypeNo, X, Y, Diameter, InvertLevel, GroundLevel, WaterLevel, OutletShapeNo, DatasetID, RMNodeTypeNo, TOPTypeNo, BufferPressureLevel, RELWeirCoeff, SMFlag )
SELECT XPNodes.Node, IIf(IsNull([_Outfalls].[Node]),1,3) AS xtype, XPNodes.XCoord, XPNodes.YCoord, 4 AS defadia, Format$([NodeIE],"Fixed") AS IE, CLng([GrndElev]*100)/100 AS xgrelev, Format$([NodeiE],"Fixed") AS WaterLevel, 4 AS OSN, "N/A" AS DSID, IIf(IsNull([_getPressureNodes].[node]),0,IIf([_getPressureNodes].[Outlink]>0,1,2)) AS RMNodeTypeNo, IIf(IsNull([_getPressureNodes].[node]),1,IIf([_getPressureNodes].[Outlink]>0,2,1)) AS TOPtypeNo, 0 AS BufferPressureLevel, 1 AS RELWeirCoeff, 0 AS SMFlag
FROM (XPNodes LEFT JOIN _getPressureNodes ON XPNodes.Node = [_getPressureNodes].Node) LEFT JOIN _Outfalls ON XPNodes.Node = [_Outfalls].Node;


_dhi_append_Orifi
----------
INSERT INTO DHIOrifi ( OrificeID, FromNode, ToNode, TypeNo, CRSID, ScalingTypeNo, Diameter, ScaleorWidth, Height, InvertLevel, ControlTypeNo )
SELECT [_dhi_Orifi_AllData].OrificeName, [_dhi_Orifi_AllData].FromNode, [_dhi_Orifi_AllData].TONode, [_dhi_Orifi_AllData].TypeNo, [_dhi_Orifi_AllData].CRSID, [_dhi_Orifi_AllData].ScalingTypeNo, [_dhi_Orifi_AllData].OrificeDiameter, [_dhi_Orifi_AllData].ScaleorWidth, [_dhi_Orifi_AllData].Height, [_dhi_Orifi_AllData].InvertLevel, [_dhi_Orifi_AllData].ControlTypeNO
FROM _dhi_Orifi_AllData;


_dhi_append_Orifi_dhQLinks_DELETE
----------
INSERT INTO DHITabularDataLinks ( LinkID, TYPE, DataSetID )
SELECT [_dhi_Orifi_dummypipes].OrificeName, 0 AS Expr1, [_dhi_Orifi_dummypipes].OrificeName
FROM _dhi_Orifi_dummypipes;


_dhi_append_Orifi_dhQLinksRegulators
----------
INSERT INTO DHIRegulators ( LinkID, TypeNo, ControlNodeA, ControlNodeB, DataSetID )
SELECT [_dhi_Orifi_AllData].OrificeName, 4 AS TypeNo, [_dhi_Orifi_AllData].FromNode AS ControlNodeA, [_dhi_Orifi_AllData].TONode AS ControlNodeB, [_dhi_Orifi_AllData].OrificeName AS DataSetID
FROM _dhi_Orifi_AllData;


_dhi_append_Orifi_dhQNames
----------
INSERT INTO DHITabularDataNames ( DataSetID, TypeNo )
SELECT [_dhi_Orifi_AllData].OrificeName, 5 AS QdhType
FROM _dhi_Orifi_AllData;


_dhi_append_Orifi_dhQValues
----------
INSERT INTO DHITabularDataValues ( DataSetID, X, Y )
SELECT [_dhi_Orifi_dhQmax].OrificeName, [_dhi_Orifi_dhQmax].dh, [_dhi_Orifi_dhQmax].Qmax
FROM _dhi_Orifi_dhQmax;


_dhi_append_Orifi_pipes
----------
INSERT INTO DHILinks ( LinkID, FromNode, ToNode, TypeNo, CRSID, ScalingTYPENO, [Diameter/Width], ScaleOrWidth, Height, MaterialNo, UpLevel, DwLevel, Infiltration, SpecifiedLength, RMapproachNO )
SELECT [_dhi_Orifi_AllData].OrificeName, [_dhi_Orifi_AllData].FromNode, [_dhi_Orifi_AllData].TONode, 1 AS xTypeNo, [_dhi_Orifi_AllData].CRSID, [_dhi_Orifi_AllData].ScalingTypeNo, [_dhi_Orifi_AllData].OrificeDiameter, [_dhi_Orifi_AllData].ScaleorWidth, [_dhi_Orifi_AllData].Height, 1 AS MatNo, [_dhi_Orifi_AllData].InvertLevel AS USIE, [_dhi_Orifi_AllData].InvertLevel, 0 AS infl, 10 AS xlen, 1 AS GorF
FROM _dhi_Orifi_AllData;


_dhi_append_pumpcurveData
----------
INSERT INTO DHITabularDataValues ( DataSetID, X, Y )
SELECT [_PumpCurves].DataSetID1, [_PumpCurves].WetWellWaterEl_ft, [_PumpCurves].Flow_cfs
FROM _PumpCurves;


_dhi_append_pumpCurveNames
----------
INSERT INTO DHITabularDataNames ( DataSetID, TypeNo )
SELECT [_dhi_Pumps_Pcurve].Pcurve, 1 AS Expr1
FROM _dhi_Pumps_Pcurve
GROUP BY [_dhi_Pumps_Pcurve].Pcurve, 1;


_dhi_append_pumps
----------
INSERT INTO DHIPumps ( PumpID, Location, FlowTo, TypeNo, DataSetID1, StartLevel, StopLevel, ControlTypeNo )
SELECT [_dhi_Pump_AllData].PumpID, [_dhi_Pump_AllData].Location, [_dhi_Pump_AllData].FlowTO, [_dhi_Pump_AllData].TypeNo, [_dhi_Pump_AllData].DataSetID1, [_dhi_Pump_AllData].Start, [_dhi_Pump_AllData].Stop, 1 AS NoContriol
FROM _dhi_Pump_AllData;


_dhi_append_shapegeom
----------
INSERT INTO DHIShapeGeometry ( CRSID, Hx, Wz )
SELECT IIf([DHITYPENO]=2,[mdl_links_ac].[pipeshape]+"_"+Format$([Diamwidth],"0")+"_"+Format$([mdl_links_ac].[height],"0"),"N/A") AS Expr1, [_UserDefinedGeometry].Depth, [_UserDefinedGeometry].Surface
FROM (mdl_Links_ac INNER JOIN _UserDefinedGeometry ON (mdl_Links_ac.Height = [_UserDefinedGeometry].Height) AND (mdl_Links_ac.DiamWidth = [_UserDefinedGeometry].Width) AND (mdl_Links_ac.PipeShape = [_UserDefinedGeometry].Shape)) INNER JOIN _PipeShapes ON mdl_Links_ac.PipeShape = [_PipeShapes].Shape
WHERE ((([_PipeShapes].DHITypeNO)=2))
GROUP BY [_UserDefinedGeometry].Depth, [_UserDefinedGeometry].Surface, IIf([DHITYPENO]=2,[mdl_links_ac].[PipeShape]+"_"+Format$([Diamwidth],"0")+"_"+Format$([mdl_links_ac].[height],"0"),"N/A"), mdl_Links_ac.PipeShape, mdl_Links_ac.DiamWidth, mdl_Links_ac.Height, [_UserDefinedGeometry].Index
ORDER BY mdl_Links_ac.PipeShape, [_UserDefinedGeometry].Depth;


_dhi_append_shapenames
----------
INSERT INTO DHIShapeNames ( CRSID, TypeNo )
SELECT IIf(IsNull([_pipeShapes].[shape]),"ERR",IIf([DHITYPENO]=2,[_pipeshapes].[shape]+"_"+Format$([Diamwidth],"0")+"_"+Format$([height],"0"),"N/A")) AS Expr1, 1 AS Expr2
FROM mdl_Links_ac INNER JOIN _PipeShapes ON mdl_Links_ac.PipeShape = [_PipeShapes].Shape
WHERE ((([_PipeShapes].DHITypeNO)=2))
GROUP BY IIf(IsNull([_pipeShapes].[shape]),"ERR",IIf([DHITYPENO]=2,[_pipeshapes].[shape]+"_"+Format$([Diamwidth],"0")+"_"+Format$([height],"0"),"N/A")), 1;


_dhi_append_weirs
----------
INSERT INTO DHIWeirs ( WEIRID, Location, FlowTo, CrestLevel, MethodNo, DataSetID, CrestTypeNo, CrestWidth, WeirFlowAngle, WeirCoefficient, ControlTypeNo )
SELECT [_dhi_Weirs_AllData].WeirID, [_dhi_Weirs_AllData].Location, [_dhi_Weirs_AllData].FlowTO, [_dhi_Weirs_AllData].Crestlevel, [_dhi_Weirs_AllData].MethodNo, [_dhi_Weirs_AllData].DatasetID, [_dhi_Weirs_AllData].CrestTypeNo, [_dhi_Weirs_AllData].CrestWidth, [_dhi_Weirs_AllData].WeirFlowAngle, [_dhi_Weirs_AllData].Coefficient, [_dhi_Weirs_AllData].ControlTypeNo
FROM _dhi_Weirs_AllData;


_dhi_getmindnelev
----------
SELECT mdl_links_ac.DSNode AS Node, Min(mdl_links_ac.DSIE) AS Z
FROM mdl_links_ac
WHERE (((mdl_links_ac.IsSpecLink)=False))
GROUP BY mdl_links_ac.DSNode;


_dhi_getminupelev
----------
SELECT mdl_links_ac.USNode AS Node, Min(mdl_links_ac.USIE) AS Z
FROM mdl_links_ac
WHERE (((mdl_links_ac.IsSpecLink)=False))
GROUP BY mdl_links_ac.USNode;


_dhi_getminUpelev_spec
----------
SELECT mdl_links_ac.DSNode AS Node, Min(mdl_links_ac_2.USIE) AS MinOfUSIE
FROM mdl_links_ac INNER JOIN (mdl_links_ac AS mdl_links_ac_1 INNER JOIN mdl_links_ac AS mdl_links_ac_2 ON mdl_links_ac_1.DSNode = mdl_links_ac_2.USNode) ON mdl_links_ac.DSNode = mdl_links_ac_1.USNode
WHERE (((mdl_links_ac_1.IsSpecLink)=True))
GROUP BY mdl_links_ac.DSNode;


_dhi_minNodeIEfromLink
----------
SELECT [_dhi_ALLNodeIEFromLink].Node, Min([_dhi_ALLNodeIEFromLink].Z) AS MinOfZ
FROM _dhi_ALLNodeIEFromLink
GROUP BY [_dhi_ALLNodeIEFromLink].Node;


_dhi_Orifi
----------
SELECT mdl_speclinkdata_ac.TValue AS OrificeName, mdl_speclinks_ac.Node, mdl_speclinks_ac.ToLinkID, mdl_speclinks_ac.ToLinkNDX, mdl_speclinks_ac.SLNode AS TONode, mdl_speclinks_ac.SpecLinkType
FROM (mdl_speclinks_ac INNER JOIN mdl_links_ac ON mdl_speclinks_ac.ToLinkID = mdl_links_ac.LinkID) INNER JOIN mdl_speclinkdata_ac ON (mdl_speclinks_ac.Node = mdl_speclinkdata_ac.Node) AND (mdl_speclinks_ac.ToLinkNDX = mdl_speclinkdata_ac.ToLinkNDX) AND (mdl_speclinks_ac.ToLinkID = mdl_speclinkdata_ac.ToLinkID)
GROUP BY mdl_speclinkdata_ac.TValue, mdl_speclinks_ac.Node, mdl_speclinks_ac.ToLinkID, mdl_speclinks_ac.ToLinkNDX, mdl_speclinks_ac.SLNode, mdl_speclinks_ac.SpecLinkType, mdl_speclinkdata_ac.Tag
HAVING (((mdl_speclinks_ac.SpecLinkType)="OB" Or (mdl_speclinks_ac.SpecLinkType)="OS") AND ((mdl_speclinkdata_ac.Tag)="ORIFNAME"));


_dhi_Orifi_AllData
----------
SELECT [_dhi_Orifi].OrificeName, [_dhi_Orifi].Node AS FromNode, [_dhi_Orifi].TONode, 1 AS TypeNo, "N/A" AS CRSID, 0 AS ScalingTypeNo, [_dhi_Orifi_Diameter].OrificeDiameter, 0 AS ScaleorWidth, 0 AS Height, Min(DHINodes.InvertLevel) AS InvertLevel, 1 AS ControlTypeNO
FROM ((_dhi_Orifi LEFT JOIN _dhi_Orifi_Coefficient ON ([_dhi_Orifi].ToLinkNDX = [_dhi_Orifi_Coefficient].ToLinkNDX) AND ([_dhi_Orifi].ToLinkID = [_dhi_Orifi_Coefficient].ToLinkID) AND ([_dhi_Orifi].Node = [_dhi_Orifi_Coefficient].Node)) LEFT JOIN _dhi_Orifi_Diameter ON ([_dhi_Orifi].ToLinkNDX = [_dhi_Orifi_Diameter].ToLinkNDX) AND ([_dhi_Orifi].ToLinkID = [_dhi_Orifi_Diameter].ToLinkID) AND ([_dhi_Orifi].Node = [_dhi_Orifi_Diameter].Node)) LEFT JOIN DHINodes ON [_dhi_Orifi].Node = DHINodes.NodeID
GROUP BY [_dhi_Orifi].OrificeName, [_dhi_Orifi].Node, [_dhi_Orifi].TONode, 1, "N/A", 0, [_dhi_Orifi_Diameter].OrificeDiameter, 0, 0, 1;


_dhi_Orifi_Coefficient
----------
SELECT [_dhi_Orifi].OrificeName, [_dhi_Orifi].Node, [_dhi_Orifi].ToLinkID, [_dhi_Orifi].ToLinkNDX, mdl_speclinkdata_ac.Tag, mdl_speclinkdata_ac.TValue
FROM _dhi_Orifi INNER JOIN mdl_speclinkdata_ac ON ([_dhi_Orifi].ToLinkNDX = mdl_speclinkdata_ac.ToLinkNDX) AND ([_dhi_Orifi].ToLinkID = mdl_speclinkdata_ac.ToLinkID) AND ([_dhi_Orifi].Node = mdl_speclinkdata_ac.Node)
WHERE (((mdl_speclinkdata_ac.Tag)="CORIF"));


_dhi_Orifi_dhQmax
----------
SELECT [_dhi_Orifi_AllData].OrificeName, [_orifice_depths].Depth AS dh, Max(IIf([depth]<0,-CLng(((64.4*-[depth])^0.5*CDbl([tvalue])*3.14*[orificediameter]^2/4)*100)/100,CLng(((64.4*[depth])^0.5*CDbl([tvalue])*3.14*[orificediameter]^2/4)*100)/100)) AS Qmax
FROM _orifice_depths, _dhi_Orifi_AllData INNER JOIN _dhi_Orifi_Coefficient ON [_dhi_Orifi_AllData].OrificeName = [_dhi_Orifi_Coefficient].OrificeName
GROUP BY [_dhi_Orifi_AllData].OrificeName, [_orifice_depths].Depth
ORDER BY [_dhi_Orifi_AllData].OrificeName, [_orifice_depths].Depth;


_dhi_Orifi_dhQmax_old
----------
SELECT [_dhi_Orifi_dummypipes].OrificeName, [_orifice_depths].Depth AS dh, Max(IIf([depth]<0,-CLng(((64.4*-[depth])^0.5*CDbl([tvalue])*3.14*[orificediameter]^2/4)*100)/100,CLng(((64.4*[depth])^0.5*CDbl([tvalue])*3.14*[orificediameter]^2/4)*100)/100)) AS Qmax
FROM _orifice_depths, _dhi_Orifi_dummypipes INNER JOIN _dhi_Orifi_Coefficient ON [_dhi_Orifi_dummypipes].OrificeName = [_dhi_Orifi_Coefficient].OrificeName
GROUP BY [_dhi_Orifi_dummypipes].OrificeName, [_orifice_depths].Depth
ORDER BY [_dhi_Orifi_dummypipes].OrificeName, [_orifice_depths].Depth;


_dhi_Orifi_Diameter
----------
SELECT [_dhi_Orifi].OrificeName, [_dhi_Orifi].Node, [_dhi_Orifi].ToLinkID, [_dhi_Orifi].ToLinkNDX, mdl_speclinkdata_ac.Tag, IIf(IsNull([TValue]) Or Trim$([tvalue])="" Or CDbl([tvalue])<=0,0,CLng(1000*(4*(CDbl([tvalue])/3.14))^0.5)/1000) AS OrificeDiameter
FROM _dhi_Orifi INNER JOIN mdl_speclinkdata_ac ON ([_dhi_Orifi].ToLinkNDX = mdl_speclinkdata_ac.ToLinkNDX) AND ([_dhi_Orifi].ToLinkID = mdl_speclinkdata_ac.ToLinkID) AND ([_dhi_Orifi].Node = mdl_speclinkdata_ac.Node)
WHERE (((mdl_speclinkdata_ac.Tag)="AORIF"));


_dhi_Orifi_dummypipes
----------
SELECT [_dhi_Orifi_AllData].OrificeName, mdl_Links_ac.USNode, mdl_Links_ac.DSNode, 1 AS xTypeNo, "N/A" AS CRSID, 1 AS STno, [_dhi_Orifi_AllData].OrificeDiameter, 1 AS SoeW, 0 AS HGT, 1 AS MatNo, [_dhi_Orifi_AllData].InvertLevel AS USIE, DHINodes_1.InvertLevel, 0 AS infl, 10 AS xlen, 1 AS GorF
FROM ((((mdl_Links_ac INNER JOIN _dhi_Orifi ON (mdl_Links_ac.DSNode = [_dhi_Orifi].TONode) AND (mdl_Links_ac.USNode = [_dhi_Orifi].Node)) INNER JOIN mdl_speclinks_ac ON ([_dhi_Orifi].ToLinkNDX = mdl_speclinks_ac.ToLinkNDX) AND ([_dhi_Orifi].ToLinkID = mdl_speclinks_ac.ToLinkID) AND ([_dhi_Orifi].Node = mdl_speclinks_ac.Node)) INNER JOIN DHINodes ON mdl_Links_ac.USNode = DHINodes.NodeID) INNER JOIN DHINodes AS DHINodes_1 ON mdl_Links_ac.DSNode = DHINodes_1.NodeID) INNER JOIN _dhi_Orifi_AllData ON (mdl_Links_ac.DSNode = [_dhi_Orifi_AllData].TONode) AND (mdl_Links_ac.USNode = [_dhi_Orifi_AllData].FromNode)
WHERE (((mdl_Links_ac.IsSpecLink)=True));


_dhi_Orifi_ISQRND
----------
SELECT [_dhi_Orifi].OrificeName, [_dhi_Orifi].Node, [_dhi_Orifi].ToLinkID, [_dhi_Orifi].ToLinkNDX, mdl_speclinkdata_ac.Tag, mdl_speclinkdata_ac.TValue
FROM _dhi_Orifi INNER JOIN mdl_speclinkdata_ac ON ([_dhi_Orifi].ToLinkNDX = mdl_speclinkdata_ac.ToLinkNDX) AND ([_dhi_Orifi].ToLinkID = mdl_speclinkdata_ac.ToLinkID) AND ([_dhi_Orifi].Node = mdl_speclinkdata_ac.Node)
WHERE (((mdl_speclinkdata_ac.Tag)="ISQRND"));


_dhi_Orifi_ONClass
----------
SELECT [_dhi_Orifi].OrificeName, [_dhi_Orifi].Node, [_dhi_Orifi].ToLinkID, [_dhi_Orifi].ToLinkNDX, mdl_speclinkdata_ac.Tag, mdl_speclinkdata_ac.TValue
FROM _dhi_Orifi INNER JOIN mdl_speclinkdata_ac ON ([_dhi_Orifi].ToLinkNDX = mdl_speclinkdata_ac.ToLinkNDX) AND ([_dhi_Orifi].ToLinkID = mdl_speclinkdata_ac.ToLinkID) AND ([_dhi_Orifi].Node = mdl_speclinkdata_ac.Node)
WHERE (((mdl_speclinkdata_ac.Tag)="ONCLASS"));


_dhi_Pump_AllData
----------
SELECT [_dhi_Pumps].PumpName AS PumpID, [_dhi_Pumps].Node AS Location, [_dhi_Pumps].TONode AS FlowTO, 1 AS TypeNo, [_dhi_Pumps_Pcurve].Pcurve AS DataSetID1, [_dhi_Pumps_Pon].Start, [_dhi_Pumps_Poff].Stop
FROM ((_dhi_Pumps LEFT JOIN _dhi_Pumps_Poff ON ([_dhi_Pumps].ToLinkNDX = [_dhi_Pumps_Poff].ToLinkNDX) AND ([_dhi_Pumps].ToLinkID = [_dhi_Pumps_Poff].ToLinkID) AND ([_dhi_Pumps].Node = [_dhi_Pumps_Poff].Node)) LEFT JOIN _dhi_Pumps_Pon ON ([_dhi_Pumps].ToLinkNDX = [_dhi_Pumps_Pon].ToLinkNDX) AND ([_dhi_Pumps].ToLinkID = [_dhi_Pumps_Pon].ToLinkID) AND ([_dhi_Pumps].Node = [_dhi_Pumps_Pon].Node)) LEFT JOIN _dhi_Pumps_Pcurve ON ([_dhi_Pumps].ToLinkNDX = [_dhi_Pumps_Pcurve].ToLinkNDX) AND ([_dhi_Pumps].ToLinkID = [_dhi_Pumps_Pcurve].ToLinkID) AND ([_dhi_Pumps].Node = [_dhi_Pumps_Pcurve].Node);


_dhi_Pumps
----------
SELECT mdl_speclinkdata_ac.TValue AS PumpName, mdl_speclinks_ac.Node, mdl_speclinks_ac.ToLinkID, mdl_speclinks_ac.ToLinkNDX, mdl_speclinks_ac.SLNode AS TONode, mdl_speclinks_ac.SpecLinkType
FROM (mdl_speclinks_ac INNER JOIN mdl_links_ac ON mdl_speclinks_ac.ToLinkID = mdl_links_ac.LinkID) INNER JOIN mdl_speclinkdata_ac ON (mdl_speclinks_ac.Node = mdl_speclinkdata_ac.Node) AND (mdl_speclinks_ac.ToLinkNDX = mdl_speclinkdata_ac.ToLinkNDX) AND (mdl_speclinks_ac.ToLinkID = mdl_speclinkdata_ac.ToLinkID)
GROUP BY mdl_speclinkdata_ac.TValue, mdl_speclinks_ac.Node, mdl_speclinks_ac.ToLinkID, mdl_speclinks_ac.ToLinkNDX, mdl_speclinks_ac.SLNode, mdl_speclinks_ac.SpecLinkType, mdl_speclinkdata_ac.Tag
HAVING (((mdl_speclinks_ac.SpecLinkType)="PS") AND ((mdl_speclinkdata_ac.Tag)="PNAME"));


_dhi_Pumps_Pcurve
----------
SELECT [_dhi_Pumps].PumpName, [_dhi_Pumps].Node, [_dhi_Pumps].ToLinkID, [_dhi_Pumps].ToLinkNDX, mdl_speclinkdata_ac.Tag, mdl_speclinkdata_ac.TValue AS Pcurve
FROM _dhi_Pumps INNER JOIN mdl_speclinkdata_ac ON ([_dhi_Pumps].ToLinkNDX = mdl_speclinkdata_ac.ToLinkNDX) AND ([_dhi_Pumps].ToLinkID = mdl_speclinkdata_ac.ToLinkID) AND ([_dhi_Pumps].Node = mdl_speclinkdata_ac.Node)
WHERE (((mdl_speclinkdata_ac.Tag)="DHI_Pcurve"));


_dhi_Pumps_Poff
----------
SELECT [_dhi_Pumps].PumpName, [_dhi_Pumps].Node, [_dhi_Pumps].ToLinkID, [_dhi_Pumps].ToLinkNDX, mdl_speclinkdata_ac.Tag, mdl_speclinkdata_ac.TValue AS Stop
FROM _dhi_Pumps INNER JOIN mdl_speclinkdata_ac ON ([_dhi_Pumps].ToLinkNDX = mdl_speclinkdata_ac.ToLinkNDX) AND ([_dhi_Pumps].ToLinkID = mdl_speclinkdata_ac.ToLinkID) AND ([_dhi_Pumps].Node = mdl_speclinkdata_ac.Node)
WHERE (((mdl_speclinkdata_ac.Tag)="POFF"));


_dhi_Pumps_Pon
----------
SELECT [_dhi_Pumps].PumpName, [_dhi_Pumps].Node, [_dhi_Pumps].ToLinkID, [_dhi_Pumps].ToLinkNDX, mdl_speclinkdata_ac.Tag, mdl_speclinkdata_ac.TValue AS Start
FROM _dhi_Pumps INNER JOIN mdl_speclinkdata_ac ON ([_dhi_Pumps].ToLinkNDX = mdl_speclinkdata_ac.ToLinkNDX) AND ([_dhi_Pumps].ToLinkID = mdl_speclinkdata_ac.ToLinkID) AND ([_dhi_Pumps].Node = mdl_speclinkdata_ac.Node)
WHERE (((mdl_speclinkdata_ac.Tag)="PON"));


_dhi_Weir_Coefficient
----------
SELECT [_dhi_Weirs].TValue, [_dhi_Weirs].Node, [_dhi_Weirs].ToLinkID, [_dhi_Weirs].ToLinkNDX, mdl_speclinkdata_ac.Tag, mdl_speclinkdata_ac.TValue AS Coefficient
FROM _dhi_Weirs INNER JOIN mdl_speclinkdata_ac ON ([_dhi_Weirs].ToLinkNDX = mdl_speclinkdata_ac.ToLinkNDX) AND ([_dhi_Weirs].ToLinkID = mdl_speclinkdata_ac.ToLinkID) AND ([_dhi_Weirs].Node = mdl_speclinkdata_ac.Node)
WHERE (((mdl_speclinkdata_ac.Tag)="COEFF"));


_dhi_Weir_CrestLevel
----------
SELECT [_dhi_Weirs].TValue, [_dhi_Weirs].Node, [_dhi_Weirs].ToLinkID, [_dhi_Weirs].ToLinkNDX, mdl_speclinkdata_ac.Tag, mdl_speclinkdata_ac.TValue AS YCrest
FROM _dhi_Weirs INNER JOIN mdl_speclinkdata_ac ON ([_dhi_Weirs].ToLinkNDX = mdl_speclinkdata_ac.ToLinkNDX) AND ([_dhi_Weirs].ToLinkID = mdl_speclinkdata_ac.ToLinkID) AND ([_dhi_Weirs].Node = mdl_speclinkdata_ac.Node)
WHERE (((mdl_speclinkdata_ac.Tag)="YCrest"));


_dhi_Weir_CrestWidth
----------
SELECT [_dhi_Weirs].TValue, [_dhi_Weirs].Node, [_dhi_Weirs].ToLinkID, [_dhi_Weirs].ToLinkNDX, mdl_speclinkdata_ac.Tag, mdl_speclinkdata_ac.TValue AS CrestWidth
FROM _dhi_Weirs INNER JOIN mdl_speclinkdata_ac ON ([_dhi_Weirs].ToLinkNDX = mdl_speclinkdata_ac.ToLinkNDX) AND ([_dhi_Weirs].ToLinkID = mdl_speclinkdata_ac.ToLinkID) AND ([_dhi_Weirs].Node = mdl_speclinkdata_ac.Node)
WHERE (((mdl_speclinkdata_ac.Tag)="WLEN"));


_dhi_Weir_FlowAngle
----------
SELECT [_dhi_Weirs].TValue, [_dhi_Weirs].Node, [_dhi_Weirs].ToLinkID, [_dhi_Weirs].ToLinkNDX, IIf([speclinktype]="WT",90,0) AS FlowAngle
FROM _dhi_Weirs;


_dhi_Weir_Ytop
----------
SELECT [_dhi_Weirs].TValue, [_dhi_Weirs].Node, [_dhi_Weirs].ToLinkID, [_dhi_Weirs].ToLinkNDX, mdl_speclinkdata_ac.Tag, mdl_speclinkdata_ac.TValue AS CrestLevel
FROM _dhi_Weirs INNER JOIN mdl_speclinkdata_ac ON ([_dhi_Weirs].ToLinkNDX = mdl_speclinkdata_ac.ToLinkNDX) AND ([_dhi_Weirs].ToLinkID = mdl_speclinkdata_ac.ToLinkID) AND ([_dhi_Weirs].Node = mdl_speclinkdata_ac.Node)
WHERE (((mdl_speclinkdata_ac.Tag)="Ytop"));


_dhi_Weirs
----------
SELECT mdl_speclinkdata_ac.TValue, mdl_speclinks_ac.Node, mdl_speclinks_ac.ToLinkID, mdl_speclinks_ac.ToLinkNDX, mdl_speclinks_ac.SLNode AS USNode, mdl_speclinks_ac.SpecLinkType
FROM (mdl_speclinks_ac INNER JOIN mdl_links_ac ON mdl_speclinks_ac.ToLinkID = mdl_links_ac.LinkID) INNER JOIN mdl_speclinkdata_ac ON (mdl_speclinks_ac.Node = mdl_speclinkdata_ac.Node) AND (mdl_speclinks_ac.ToLinkNDX = mdl_speclinkdata_ac.ToLinkNDX) AND (mdl_speclinks_ac.ToLinkID = mdl_speclinkdata_ac.ToLinkID)
GROUP BY mdl_speclinkdata_ac.TValue, mdl_speclinks_ac.Node, mdl_speclinks_ac.ToLinkID, mdl_speclinks_ac.ToLinkNDX, mdl_speclinks_ac.SLNode, mdl_speclinks_ac.SpecLinkType, mdl_speclinkdata_ac.Tag
HAVING (((mdl_speclinks_ac.SpecLinkType)="WT" Or (mdl_speclinks_ac.SpecLinkType)="WS") AND ((mdl_speclinkdata_ac.Tag)="WEIRNAME"));


_dhi_Weirs_AllData
----------
SELECT [_dhi_Weirs].TValue AS WeirID, First([_dhi_Weirs].Node) AS Location, First([_dhi_Weirs].USNode) AS FlowTO, First([_dhi_Weir_CrestLevel].YCrest) AS Crestlevel, First(2) AS MethodNo, "N/A" AS DatasetID, 1 AS CrestTypeNo, [_dhi_Weir_CrestWidth].CrestWidth AS CrestWidth, [_dhi_Weir_FlowAngle].FlowAngle AS WeirFlowAngle, [_dhi_Weir_Coefficient].Coefficient, 1 AS ControlTypeNo
FROM (((_dhi_Weirs LEFT JOIN _dhi_Weir_CrestLevel ON ([_dhi_Weirs].ToLinkNDX = [_dhi_Weir_CrestLevel].ToLinkNDX) AND ([_dhi_Weirs].ToLinkID = [_dhi_Weir_CrestLevel].ToLinkID) AND ([_dhi_Weirs].Node = [_dhi_Weir_CrestLevel].Node)) LEFT JOIN _dhi_Weir_CrestWidth ON ([_dhi_Weirs].ToLinkNDX = [_dhi_Weir_CrestWidth].ToLinkNDX) AND ([_dhi_Weirs].ToLinkID = [_dhi_Weir_CrestWidth].ToLinkID) AND ([_dhi_Weirs].Node = [_dhi_Weir_CrestWidth].Node)) LEFT JOIN _dhi_Weir_Coefficient ON ([_dhi_Weirs].ToLinkNDX = [_dhi_Weir_Coefficient].ToLinkNDX) AND ([_dhi_Weirs].ToLinkID = [_dhi_Weir_Coefficient].ToLinkID) AND ([_dhi_Weirs].Node = [_dhi_Weir_Coefficient].Node)) LEFT JOIN _dhi_Weir_FlowAngle ON ([_dhi_Weirs].ToLinkNDX = [_dhi_Weir_FlowAngle].ToLinkNDX) AND ([_dhi_Weirs].ToLinkID = [_dhi_Weir_FlowAngle].ToLinkID) AND ([_dhi_Weirs].Node = [_dhi_Weir_FlowAngle].Node)
GROUP BY [_dhi_Weirs].TValue, "N/A", 1, [_dhi_Weir_CrestWidth].CrestWidth, [_dhi_Weir_FlowAngle].FlowAngle, [_dhi_Weir_Coefficient].Coefficient, 1;


_false_parallel_links
----------
SELECT xlinks.LinkID, [_tXPSpecialLinks.DSNode] AS USNode, [_parallel_links].DsNode
FROM (_tXPSpecialLinks INNER JOIN xlinks ON [_tXPSpecialLinks].toLinkID = xlinks.LinkID) INNER JOIN _parallel_links ON xlinks.LinkID = [_parallel_links].LinkID

UNION SELECT xlinks.LinkID, [_false_parallel_links_without_speciallink].USNode, [_false_parallel_links_without_speciallink].DsNode
FROM (xlinks INNER JOIN _false_parallel_links_without_speciallink ON xlinks.LinkID = [_false_parallel_links_without_speciallink].LinkID) INNER JOIN (_tXPSpecialLinks INNER JOIN xlinks AS xlinks_1 ON [_tXPSpecialLinks].toLinkID = xlinks_1.LinkID) ON ([_false_parallel_links_without_speciallink].DsNode = xlinks_1.DSNode) AND ([_false_parallel_links_without_speciallink].USNode = xlinks_1.USNode);


_false_parallel_links_without_speciallink
----------
SELECT [_parallel_links].*
FROM _parallel_links LEFT JOIN _tXPSpecialLinks ON [_parallel_links].LinkID = [_tXPSpecialLinks].toLinkID
WHERE ((([_tXPSpecialLinks].toLinkID) Is Null));


_getmaxdianodedn
----------
SELECT xLinks.DSNode, Max(xLinks.xHeight) AS MaxOfxHeight
FROM xLinks
GROUP BY xLinks.DSNode;


_getmaxdianodeus
----------
SELECT xLinks.USNode, Max(xLinks.xHeight) AS MaxOfxHeight
FROM xLinks
GROUP BY xLinks.USNode;


_getmindnelev
----------
SELECT xLinks.DSNode AS Node, Min(xLinks.DSIE) AS Z
FROM xLinks
WHERE (((xLinks.IsSpecLink)=False))
GROUP BY xLinks.DSNode;


_getminupelev
----------
SELECT xLinks.USNode AS Node, Min(xLinks.USIE) AS Z
FROM xLinks
WHERE (((xLinks.IsSpecLink)=False))
GROUP BY xLinks.USNode;


_getMinUpElev_spec
----------
SELECT [_tXPSpecialLinks].DSNode AS Node, Min(xlinks_2.USIE) AS MinOfUSIE
FROM (xlinks INNER JOIN _tXPSpecialLinks ON xlinks.DSNode = [_tXPSpecialLinks].USNode) INNER JOIN xlinks AS xlinks_2 ON [_tXPSpecialLinks].toLinkID = xlinks_2.LinkID
WHERE ((([_tXPSpecialLinks].SpecLinkCase)="V"))
GROUP BY [_tXPSpecialLinks].DSNode;


_getParallelEndPoints
----------
SELECT xLinks.USNode, xLinks.DSNode, Count(xLinks.explink) AS Nlinks
FROM xLinks
GROUP BY xLinks.USNode, xLinks.DSNode
HAVING (((Count(xLinks.explink))>1));


_getParallelLinks
----------
SELECT xLinks.*
FROM _getParallelEndPoints INNER JOIN xLinks ON ([_getParallelEndPoints].DSNode = xLinks.DSNode) AND ([_getParallelEndPoints].USNode = xLinks.USNode);


_getPressureNodes
----------
SELECT mdl_links_ac.DSNode AS Node, Sum(IIf([mdl_links_ac].[PipeFlowType]="F",1,0)) AS InLink, Sum(IIf([mdl_links_ac_1].[PipeFlowType]="F",1,0)) AS OutLink
FROM mdl_links_ac INNER JOIN mdl_links_ac AS mdl_links_ac_1 ON mdl_links_ac.DSNode = mdl_links_ac_1.USNode
GROUP BY mdl_links_ac.DSNode
HAVING (((Sum(IIf([mdl_links_ac].[PipeFlowType]="F",1,0)))>0)) OR (((Sum(IIf([mdl_links_ac_1].[PipeFlowType]="F",1,0)))>0));


_junk_getWS
----------
SELECT mdl_speclinks_ac.Node, mdl_speclinks_ac.ToLinkID, mdl_speclinks_ac.ToLinkNDX, mdl_speclinks_ac.SpecLinkType, mdl_speclinks_ac.MSpecLinkID
FROM mdl_speclinks_ac
WHERE (((mdl_speclinks_ac.SpecLinkType)="WS"));


_minNodeIEfromLink
----------
SELECT [_ALLNodeIEFromLink].Node, Min([_ALLNodeIEFromLink].Z) AS MinOfZ
FROM _ALLNodeIEFromLink
GROUP BY [_ALLNodeIEFromLink].Node;


_Outfalls
----------
SELECT xLinks.DSNode AS Node
FROM xLinks LEFT JOIN xLinks AS xLinks_1 ON xLinks.DSNode = xLinks_1.USNode
WHERE (((xLinks_1.USNode) Is Null))
GROUP BY xLinks.DSNode;


_uniqueUSnode
----------
SELECT mdl_links_ac.USNode
FROM mdl_links_ac
GROUP BY mdl_links_ac.USNode;


_UpdateSimLinkID
----------
UPDATE xLinks INNER JOIN mdl_links_ac ON xLinks.LinkID = mdl_links_ac.LinkID SET mdl_links_ac.SimLinkID = [explink]
WHERE (((mdl_links_ac.IsSpecLink)=False));


_XPParallelLinks
----------
SELECT [_parallel_links].PLinkID, [_parallel_links].USNode, [_parallel_links].DsNode
FROM _parallel_links
GROUP BY [_parallel_links].PLinkID, [_parallel_links].USNode, [_parallel_links].DsNode;


_XPSpecialLinkData
----------
SELECT [_XPSpecialLinks].USNode, mdl_SpecLinks_ac.ToLinkID, mdl_links_ac.MLinkID AS ToMlinkID, [_XPSpecialLinks].SpecLinkID, mdl_SpecLinks_ac.ToLinkNDX, mdl_SpecLinkData_ac.Tag, mdl_SpecLinkData_ac.TValue, [_TagDefinition].isXPtag
FROM (((mdl_SpecLinks_ac INNER JOIN _XPSpecialLinks ON ([_XPSpecialLinks].toLinkID = mdl_SpecLinks_ac.ToLinkID) AND (mdl_SpecLinks_ac.Node = [_XPSpecialLinks].USNode)) INNER JOIN mdl_SpecLinkData_ac ON (mdl_SpecLinks_ac.ToLinkNDX = mdl_SpecLinkData_ac.ToLinkNDX) AND (mdl_SpecLinks_ac.ToLinkID = mdl_SpecLinkData_ac.ToLinkID) AND (mdl_SpecLinks_ac.Node = mdl_SpecLinkData_ac.Node)) INNER JOIN _TagDefinition ON mdl_SpecLinkData_ac.Tag = [_TagDefinition].Tag) INNER JOIN mdl_links_ac ON [_XPSpecialLinks].toLinkID = mdl_links_ac.LinkID
WHERE ((([_TagDefinition].isXPtag)=True));


_XPSpecialLinks
----------
SELECT [_tXPSpecialLinks].SpecLinkID, [_tXPSpecialLinks].USNode, [_tXPSpecialLinks].DSNode, [_tXPSpecialLinks].toLinkID, [_tXPSpecialLinks].toLinkID AS LinkID, [_tXPSpecialLinks].SpecLinkCase
FROM _tXPSpecialLinks
GROUP BY [_tXPSpecialLinks].SpecLinkID, [_tXPSpecialLinks].USNode, [_tXPSpecialLinks].DSNode, [_tXPSpecialLinks].toLinkID, [_tXPSpecialLinks].toLinkID, [_tXPSpecialLinks].SpecLinkCase;


_XPSpecialParallelLinks_DELETE
----------
SELECT [_parallel_links].PLinkID, [_parallel_links].USNode, [_parallel_links].DsNode
FROM _parallel_links
GROUP BY [_parallel_links].PLinkID, [_parallel_links].USNode, [_parallel_links].DsNode;


Check01:SumBaseFlowComponents
----------
SELECT Sum(XPXDataBF_QINST.DMII) AS SumOfDMII, Sum(XPXDataBF_SEWAGE.flow) AS SumOfflow
FROM XPXDataBF_QINST INNER JOIN XPXDataBF_SEWAGE ON XPXDataBF_QINST.Node = XPXDataBF_SEWAGE.Node;


NodesInXPLinks
----------
SELECT NODE
FROM [SELECT XPLinks.USNODE AS Node
FROM XPLinks
GROUP BY XPLinks.USNODE

UNION SELECT XPLinks.DSNode AS Node
FROM XPLinks
GROUP BY XPLinks.DSNode]. AS [%$##@_Alias]
GROUP BY Node;


Query1
----------
SELECT [_tXPSpecialLinks].DSNode, xlinks.LinkID, xlinks.USIE, xlinks.USNode, xlinks.DSNode
FROM _tXPSpecialLinks INNER JOIN xlinks ON [_tXPSpecialLinks].toLinkID = xlinks.LinkID
WHERE ((([_tXPSpecialLinks].DSNode)="011SPL"));


xlinks
----------
SELECT IIf([isspeclink],"S"+CStr([linkid]),IIf([mlinkid]=0,"F"+CStr([linkid]),"M"+CStr([mlinkid]))) AS explink, mdl_Links_ac.LinkID, mdl_Links_ac.MLinkID, mdl_Links_ac.CompKey AS HLinkID, mdl_Links_ac.USNode, mdl_Links_ac.DSNode, mdl_Links_ac.PipeShape, mdl_Links_ac.USIE, mdl_Links_ac.DSIE, mdl_Links_ac.Length, mdl_Links_ac.Height, mdl_Links_ac.DiamWidth, Format$(IIf([length]=0,-99,(([usie]-[dsie])/[length])),"0.00000") AS xSlope, mdl_Links_ac.Material, mdl_Links_ac.AsBuilt, IIf(IsNull([_pipeshapes].[shape]),0.014,[_pipeshapes].[roughness]) AS Roughness, mdl_Links_ac.LinkType, mdl_Links_ac.LinkReach, mdl_Links_ac.ReachElement, mdl_Links_ac.IsSpecLink, mdl_Links_ac.PipeFlowType
FROM mdl_Links_ac LEFT JOIN _PipeShapes ON mdl_Links_ac.PipeShape = [_PipeShapes].Shape;


xNodes
----------
SELECT mdl_nodes_ac.*
FROM mdl_nodes_ac;


XPLinks
----------
SELECT xLinks.MLinkID, IIf(IsNull([_parallel_links].[linkID]),[explink],IIf(IsNull([_false_parallel_links].[linkID]),[PlinkID],[explink])) AS xplink, IIf(IsNull([_parallel_links].[linkID]),0,IIf(IsNull([_false_parallel_links].[linkID]),[Plinkndx],0)) AS instance, IIf(IsNull([_tXPSpecialLinks].[toLinkID]),[xlinks].[usnode],[_tXPSpecialLinks].[DSnode]) AS USNODE, xLinks.DSNode, xLinks.PipeShape, IIf(IsNull([XPNKLASS]),100,[XPNKLASS]) AS xNKLass, xLinks.USIE, xLinks.DSIE, xLinks.Length, IIf(IsNull([_pipeshapes].[xpnklass]),86,IIf([_pipeshapes].[xpnklass]=1,[diamwidth],[xlinks].[height])) AS xHeight, IIf(IsNull([_pipeshapes].[xpnklass]),86,IIf([_pipeshapes].[xpnklass]=1,0,[xlinks].[diamwidth])) AS xWidth, xLinks.xSlope, [_PipeShapes].Roughness, xLinks.Material, IIf(IsNull([_parallel_links].[linkID]),"F",IIf(IsNull([_false_parallel_links].[LinkID]),"T","F")) AS isparallel, xLinks.PipeFlowType
FROM (((xLinks LEFT JOIN _parallel_links ON xLinks.LinkID = [_parallel_links].LinkID) LEFT JOIN _PipeShapes ON xLinks.PipeShape = [_PipeShapes].Shape) LEFT JOIN _tXPSpecialLinks ON xLinks.LinkID = [_tXPSpecialLinks].toLinkID) LEFT JOIN _false_parallel_links ON xLinks.LinkID = [_false_parallel_links].LinkID;


XPNodes
----------
SELECT mdl_nodes_ac.Node, mdl_nodes_ac.XCoord, mdl_nodes_ac.YCoord, mdl_nodes_ac.GrndElev, IIf(IsNull([MinOfZ]),-99,Format$([minofZ],"Fixed")) AS NodeIE
FROM mdl_nodes_ac LEFT JOIN _minNodeIEfromLink ON mdl_nodes_ac.Node = [_minNodeIEfromLink].Node;

UNION SELECT [_tXPVirtNodes].Node, [_tXPVirtNodes].xcoord, [_tXPVirtNodes].ycoord, [_tXPVirtNodes].GrndElev, IIf(IsNull([MinOfZ]),-99,Format$([minofZ],"Fixed")) AS NodeIE
FROM _tXPVirtNodes LEFT JOIN _minNodeIEfromLink ON [_tXPVirtNodes].Node = [_minNodeIEfromLink].Node;


xport_MakeXPXSelected
----------
SELECT [Xport List].dumporder, [Xport List].QueryName, [Xport List].ExecuteXPX INTO SelectedXPX
FROM [Xport List]
WHERE ((([Xport List].ExecuteXPX)=True));


XPX_EXTRAN_JOBCONTROL
----------
SELECT "DATA" AS Expr3, JobControlExtran.Tag, Chr(34) & " " & Chr(34) AS Expr2, "0 1" AS Instance, IIf([texttag?],Chr(34),"") & [TValue] & IIf([texttag?],Chr(34),"") AS MyValue, "/*  " & [Description] & "  */" AS Expr1
FROM JobControlExtran
ORDER BY JobControlExtran.Order;


XPX_Global_EL
----------
TRANSFORM First(mst_xsection_data.elevation) AS FirstOfelevation
SELECT "GLDBDATA EL " & Chr(34) & "Natural Section Shape" & Chr(34) AS Expr1, Chr(34) & [xplink] & Chr(34) AS Expr2, Count(mst_xsection_data.index) AS CountOfindex
FROM XPLinks INNER JOIN (mst_xsections INNER JOIN mst_xsection_data ON mst_xsections.mst_xsections_id = mst_xsection_data.mst_xsections_id) ON XPLinks.MLinkID = mst_xsections.mlinkid
GROUP BY "GLDBDATA EL " & Chr(34) & "Natural Section Shape" & Chr(34), Chr(34) & [xplink] & Chr(34)
ORDER BY mst_xsection_data.index
PIVOT mst_xsection_data.index;


XPX_Global_NaturalChannel
----------
SELECT DISTINCTROW "GLDBITEM " & Chr(34) & "Natural Section Shape" & Chr(34) AS EXPR1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2
FROM xpLinks INNER JOIN mst_xsections ON xpLinks.MLinkID = mst_xsections.mlinkid
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPX_Global_NSGD_STCHL
----------
SELECT DISTINCTROW "GLDBDATA NSGD_STCHL " & Chr(34) & "Natural Section Shape" & Chr(34) AS EXPR1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, " 1" AS Expr3, mst_xsections.station_left, "/* station of left bank */" AS Expr5
FROM xpLinks INNER JOIN mst_xsections ON xpLinks.MLinkID = mst_xsections.mlinkid
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPX_Global_NSGD_STCHR
----------
SELECT DISTINCTROW "GLDBDATA NSGD_STCHR " & Chr(34) & "Natural Section Shape" & Chr(34) AS EXPR1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, " 1" AS Expr3, mst_xsections.station_right, "/* right bank roughness */" AS Expr5
FROM xpLinks INNER JOIN mst_xsections ON xpLinks.MLinkID = mst_xsections.mlinkid
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPX_Global_NSGD_XNCH
----------
SELECT DISTINCTROW "GLDBDATA NSGD_XNCH " & Chr(34) & "Natural Section Shape" & Chr(34) AS EXPR1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, " 1" AS Expr3, mst_xsections.roughness_main, "/* main channel roughness */" AS Expr5
FROM xpLinks INNER JOIN mst_xsections ON xpLinks.MLinkID = mst_xsections.mlinkid
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPX_Global_NSGD_XNL
----------
SELECT DISTINCTROW "GLDBDATA NSGD_XNL " & Chr(34) & "Natural Section Shape" & Chr(34) AS EXPR1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, " 1" AS Expr3, mst_xsections.roughness_left, "/* left bank roughness */" AS Expr5
FROM xpLinks INNER JOIN mst_xsections ON xpLinks.MLinkID = mst_xsections.mlinkid
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPX_Global_NSGD_XNR
----------
SELECT DISTINCTROW "GLDBDATA NSGD_XNR " & Chr(34) & "Natural Section Shape" & Chr(34) AS EXPR1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, " 1" AS Expr3, mst_xsections.roughness_right, "/*right bank roughness */" AS Expr5
FROM xpLinks INNER JOIN mst_xsections ON xpLinks.MLinkID = mst_xsections.mlinkid
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPX_Global_PRATEX
----------
TRANSFORM Max([_PumpCurves].Rating) AS [The Value]
SELECT "GLDBDATA PRATEX " & Chr(34) & "Pump Ratings" & Chr(34) AS Expr1, Chr(34) & [PumpType] & Chr(34) AS Expr2, Max([_PumpCurves].Index) AS MaxOfIndex
FROM _PumpCurves INNER JOIN mdl_speclinkdata_ac ON [_PumpCurves].PumpType = mdl_speclinkdata_ac.TValue
WHERE (((mdl_speclinkdata_ac.Tag)="PSEL"))
GROUP BY "GLDBDATA PRATEX " & Chr(34) & "Pump Ratings" & Chr(34), Chr(34) & [PumpType] & Chr(34), [_PumpCurves].PumpType, mdl_speclinkdata_ac.Tag
PIVOT [_PumpCurves].Index;


XPX_Global_PRATEY
----------
TRANSFORM Max([_PumpCurves].Flow) AS [The Value]
SELECT "GLDBDATA PRATEY " & Chr(34) & "Pump Ratings" & Chr(34) AS Expr1, Chr(34) & [PumpType] & Chr(34) AS Expr2, Max([_PumpCurves].Index) AS MaxOfIndex
FROM _PumpCurves INNER JOIN mdl_speclinkdata_ac ON [_PumpCurves].PumpType = mdl_speclinkdata_ac.TValue
WHERE (((mdl_speclinkdata_ac.Tag)="PSEL"))
GROUP BY "GLDBDATA PRATEY " & Chr(34) & "Pump Ratings" & Chr(34), Chr(34) & [PumpType] & Chr(34), [_PumpCurves].PumpType, mdl_speclinkdata_ac.Tag
PIVOT [_PumpCurves].Index;


XPX_Global_PumpRatings
----------
SELECT "GLDBITEM " & Chr(34) & "Pump Ratings" & Chr(34) AS Expr1, Chr(34) & [TValue] & Chr(34) AS Expr2
FROM _PumpCurves INNER JOIN _xpSpecialLinkData ON [_PumpCurves].PumpType = [_xpSpecialLinkData].TValue
WHERE ((([_xpSpecialLinkData].Tag)="PSEL") AND (([_PumpCurves].Index)=1))
ORDER BY Chr(34) & [TValue] & Chr(34);


XPX_Global_STA
----------
TRANSFORM First(mst_xsection_data.station) AS FirstOfstation
SELECT "GLDBDATA STA " & Chr(34) & "Natural Section Shape" & Chr(34) AS Expr1, Chr(34) & [xplink] & Chr(34) AS Expr2, Count(mst_xsection_data.index) AS CountOfindex
FROM XPLinks INNER JOIN (mst_xsections INNER JOIN mst_xsection_data ON mst_xsections.mst_xsections_id = mst_xsection_data.mst_xsections_id) ON XPLinks.MLinkID = mst_xsections.mlinkid
GROUP BY "GLDBDATA STA " & Chr(34) & "Natural Section Shape" & Chr(34), Chr(34) & [xplink] & Chr(34)
ORDER BY mst_xsection_data.index
PIVOT mst_xsection_data.index;


XPX_Global_TDV
----------
SELECT "DATA T_DV" AS Expr1, Chr(34)+" "+Chr(34) AS Expr2, [_SanPattern].Instance, 7 AS [Index], [_SanPattern].D1, [_SanPattern].D2, [_SanPattern].D3, [_SanPattern].D4, [_SanPattern].D5, [_SanPattern].D6, [_SanPattern].D7, "/* " & [Name] & " */" AS Expr3
FROM _SanPattern;


XPX_Global_TemporalVariations
----------
SELECT "GLDB" AS Expr1, Chr(34) & "Temporal Variations" & Chr(34) AS Expr2, [_SanPattern].Instance, Chr(34) & [Name] & Chr(34) AS Expr3
FROM _SanPattern;


XPX_Global_THV
----------
SELECT "DATA T_HV" AS Expr1, Chr(34)+" "+Chr(34) AS Expr2, [_SanPattern].Instance, 24 AS [Index], [_SanPattern].H1, [_SanPattern].H2, [_SanPattern].H3, [_SanPattern].H4, [_SanPattern].H5, [_SanPattern].H6, [_SanPattern].H7, [_SanPattern].H8, [_SanPattern].H9, [_SanPattern].H10, [_SanPattern].H11, [_SanPattern].H12, [_SanPattern].H13, [_SanPattern].H14, [_SanPattern].H15, [_SanPattern].H16, [_SanPattern].H17, [_SanPattern].H18, [_SanPattern].H19, [_SanPattern].H20, [_SanPattern].H21, [_SanPattern].H22, [_SanPattern].H23, [_SanPattern].H24, "/* " & [Name] & " */" AS Expr3
FROM _SanPattern;


XPXDataA1A
----------
SELECT "DATA A1A" AS EXPR1, Chr(34) & Trim([tblConduitData].[Node_DS]) & Chr(34) AS EXPR2, " 0 1 20" AS Expr3
FROM tblConduitData LEFT JOIN tblConduitData AS tblConduitData_1 ON tblConduitData.Node_DS = tblConduitData_1.Node_US
WHERE (((tblConduitData_1.Node_US) Is Null))
GROUP BY "DATA A1A", Chr(34) & Trim([tblConduitData].[Node_DS]) & Chr(34), " 0 1 20", tblConduitData.Node_DS;


XPXDataAORIF
----------
SELECT DISTINCTROW "DATA AORIF" AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, [ToLinkNDX] & " 1" AS Expr4, [_XPSpecialLinkData].TValue
FROM _XPSpecialLinkData
WHERE ((([_XPSpecialLinkData].Tag)="AORIF"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataASTORE
----------
SELECT "DATA ASTORE" AS Expr1, Chr(34) & [USNode] & Chr(34) AS EXPR2, "0 1" AS Expr3, [_xpSpecialLinkData].TValue
FROM _xpSpecialLinkData
WHERE ((([_xpSpecialLinkData].Tag) Like "ASTORE"))
ORDER BY Chr(34) & [USNode] & Chr(34);


XPXDataBF_DRY_WTH_InitOFF
----------
SELECT "DATA DRY_WTH " & Chr$(34) & [Node] & Chr$(34) AS Expr3, "0 1 0" AS Expr1
FROM mdl_nodes_ac
GROUP BY "DATA DRY_WTH " & Chr$(34) & [Node] & Chr$(34), "0 1 0";


XPXDataBF_DRY_WTH_ON
----------
SELECT "DATA DRY_WTH " & Chr$(34) & [NGTOsan] & Chr$(34) AS Node, "0 1 1" AS instance
FROM _BaseFlowComponents;


XPXDataBF_HDR_PFAC
----------
SELECT "DATA HDR_PFAC " & Chr$(34) & [NGTOsan] & Chr$(34) AS Expr3, "0 1 1" AS Expr1
FROM _BaseFlowComponents
GROUP BY "DATA HDR_PFAC " & Chr$(34) & [NGTOsan] & Chr$(34), "0 1 1";


XPXDataBF_HDR_SEWAGE_CFS
----------
SELECT "DATA HDR_SEWAGE " AS Tag, Chr$(34) & [NGTOsan] & Chr$(34) AS Node, "0 1" AS instance, [_BaseFlowComponents].avesanflow AS flow
FROM _BaseFlowComponents
ORDER BY Chr$(34) & [NGTOsan] & Chr$(34);


XPXDataBF_HDR_SEWAGE_InitZero
----------
SELECT "DATA HDR_SEWAGE " & Chr$(34) & [Node] & Chr$(34) AS Expr3, "0 1 0" AS Expr1
FROM mdl_nodes_ac;


XPXDataBF_QINST
----------
SELECT "DATA QINST " AS Tag, Chr$(34) & [NGTOSan] & Chr$(34) AS Node, "0 1" AS Instance, [_BaseFlowComponents].DMII
FROM _BaseFlowComponents;


XPXDataBF_QINST_InitZero
----------
SELECT "DATA QINST " & Chr$(34) & [Node] & Chr$(34) AS Expr3, "0 1 0" AS Expr1
FROM mdl_nodes_ac;


XPXDataCName
----------
SELECT DISTINCTROW "DATA CNAME" & [Plinkndx] AS EXPR1, Chr(34) & CStr([PlinkID]) & Chr(34) AS EXPR2, "0 1" AS EXPR3, Chr(34) & CStr([MlinkID]) & Chr(34) AS EXPR4
FROM _parallel_links LEFT JOIN _false_parallel_links ON [_parallel_links].LinkID = [_false_parallel_links].LinkID
WHERE ((([_false_parallel_links].LinkID) Is Null));


XPXDataCNTLS
----------
SELECT "DATA CNTLS" AS Expr1, Chr(34) & [USNode] & Chr(34) AS Expr2, [ToLinkNDX] & " 1" AS Expr4, [_xpSpecialLinkData].TValue AS Expr3
FROM _xpSpecialLinkData
WHERE ((([_xpSpecialLinkData].Tag) Like "CNTLS"))
ORDER BY Chr(34) & [USNode] & Chr(34);


XPXDataCOEFF
----------
SELECT DISTINCTROW "DATA COEFF" AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, [ToLinkNDX] & " 1" AS Expr4, [_XPSpecialLinkData].TValue
FROM _XPSpecialLinkData
WHERE ((([_XPSpecialLinkData].Tag)="COEFF"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataCond
----------
SELECT DISTINCTROW "DATA COND" & [INSTANCE] AS Expr1, Chr(34) & [conduit_name] & Chr(34) AS EXPR2, "0 1" AS EXPR3, IIf(IsNull([Link_Type]) Or [Link_type]="X",0,1) AS EXPR4
FROM tblConduitData
WHERE (((tblConduitData.Link_Type) Is Not Null) AND ((tblConduitData.Multi_or_Single)="138"))
ORDER BY Chr(34) & [conduit_name] & Chr(34);


XPXDataCORIF
----------
SELECT DISTINCTROW "DATA CORIF" AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, [ToLinkNDX] & " 1" AS Expr4, [_XPSpecialLinkData].TValue
FROM _XPSpecialLinkData
WHERE ((([_XPSpecialLinkData].Tag)="CORIF"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataDeep
----------
SELECT DISTINCTROW "DATA DEEP" AS EXPR1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, [instance] & " 1" AS Expr3, IIf([xHeight]=0,19,Format$([xHeight]/12,"0.000")) AS Expr4
FROM xpLinks
WHERE (([xNKlass]<>8))
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPXDataDORIF
----------
SELECT DISTINCTROW "DATA DORIF" AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, [ToLinkNDX] & " 1" AS Expr4, [_XPSpecialLinkData].TValue
FROM _XPSpecialLinkData
WHERE ((([_XPSpecialLinkData].Tag)="DORIF"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataFLGOUTF
----------
SELECT "DATA FLGOUTF" AS EXPR1, Chr(34) & Trim([xlinks].[DSNode]) & Chr(34) AS EXPR2, " 0 1 1" AS Expr3, First("/* mlinkid: " & [xlinks].[mlinkid] & "  */") AS Expr4
FROM xLinks LEFT JOIN xLinks AS xLinks_1 ON xLinks.DSNode = xLinks_1.USNode
WHERE (((xLinks_1.USNode) Is Null))
GROUP BY "DATA FLGOUTF", Chr(34) & Trim([xlinks].[DSNode]) & Chr(34), " 0 1 1", xLinks.DSNode;


XPXDataGRELEV
----------
SELECT DISTINCTROW "DATA GRELEV" AS EXPR1, Chr(34) & Trim([Node]) & Chr(34) AS EXPR2, "0 1" AS EXPR3, Format$([GrndElev],"0.0") AS Expr4
FROM xpnodes
ORDER BY Chr(34) & Trim([Node]) & Chr(34);


XPXDataGWATERSEL
----------
SELECT "DATA R_GWATERSEL" AS [Data Field], Chr$(34) & [NGTO] & Chr$(34) AS NGTOtxt, tblSubCatchments.NGTO_Instance, 1 AS XPindex, Chr$(34) & "GWCalibration" & Chr$(34) AS GWSel
FROM tblSubCatchments
WHERE (((tblSubCatchments.NGTO_Instance)<5))
ORDER BY Chr$(34) & [NGTO] & Chr$(34), tblSubCatchments.NGTO_Instance;


XPXDataGWFLAG
----------
SELECT "DATA R_GWFLAG" AS [Data Field], Chr$(34) & [NGTO] & Chr$(34) AS NGTOtxt, tblSubCatchments.NGTO_Instance, 1 AS XPindex, 0 AS Expr1
FROM tblSubCatchments
WHERE (((tblSubCatchments.NGTO_Instance)<5))
ORDER BY Chr$(34) & [NGTO] & Chr$(34), tblSubCatchments.NGTO_Instance;


XPXDataGWTAG
----------
SELECT "DATA R_GWTAG" AS [Data Field], Chr$(34) & [NGTO] & Chr$(34) AS NGTOtxt, tblSubCatchments.NGTO_Instance, 1 AS XPindex, 1 AS Expr1
FROM tblSubCatchments
WHERE (((tblSubCatchments.NGTO_Instance)<5))
ORDER BY Chr$(34) & [NGTO] & Chr$(34), tblSubCatchments.NGTO_Instance;


XPXDataGWToNode
----------
SELECT "DATA R_CNAMEDRAIN" AS [Data Field], Chr$(34) & [NGTO] & Chr$(34) AS NGFromtxt, tblSubCatchments.NGTO_Instance, 1 AS XPindex, Chr$(34) & [NGTO] & Chr$(34) AS NGTOtxt
FROM tblSubCatchments
ORDER BY Chr$(34) & [NGTO] & Chr$(34);


XPXDataINTFLG_ON
----------
SELECT DISTINCTROW "DATA USEINTFLG" AS EXPR1, Chr(34) & Trim([NODE]) & Chr(34) AS EXPR2, "0 1" AS EXPR3, "1" AS EXPR4
FROM mdl_nodes_ac
ORDER BY Chr(34) & Trim([NODE]) & Chr(34);


XPXDataINTPERCENT
----------
SELECT DISTINCTROW "DATA INTPERCENT" AS EXPR1, Chr(34) & Trim([Node]) & Chr(34) AS EXPR2, "0 1 100.0" AS EXPR3
FROM xpnodes
ORDER BY Chr(34) & Trim([Node]) & Chr(34);


XPXDataIPTYP
----------
SELECT "DATA IPTYP" AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, [ToLinkNDX] & " 1" AS Expr4, [_xpSpecialLinkData].TValue
FROM _xpSpecialLinkData
WHERE ((([_xpSpecialLinkData].Tag) Like "IPTYP"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataISQRND
----------
SELECT DISTINCTROW "DATA ISQRND" AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, [ToLinkNDX] & " 1" AS Expr4, [_XPSpecialLinkData].TValue
FROM _XPSpecialLinkData
WHERE ((([_XPSpecialLinkData].Tag)="ISQRND"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataKWEIR
----------
SELECT DISTINCTROW "DATA KWEIR" AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, [ToLinkNDX] & " 1" AS Expr4, [_XPSpecialLinkData].TValue
FROM _XPSpecialLinkData
WHERE ((([_XPSpecialLinkData].Tag)="KWEIR"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataLen
----------
SELECT DISTINCTROW "DATA LEN" AS EXPR1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, [instance] & " 1" AS Expr3, Format$([length],"0") AS Expr4
FROM xpLinks
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPXDataNATS_SHAPE
----------
SELECT DISTINCTROW "DATA NATS_SHAPE" AS EXPR1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, [instance] & " 1" AS Expr3, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR4, "/* associates link with natural channel */" AS Expr5
FROM xpLinks INNER JOIN mst_xsections ON xpLinks.MLinkID = mst_xsections.mlinkid
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPXDataNKCTL
----------
SELECT DISTINCTROW "DATA NKCTL" AS EXPR1, Chr(34) & [xplink] & Chr(34) AS EXPR2, [instance] & " 1" AS Expr3, [_PipeShapes].XLNKCTL AS Expr4, "/* " & [xplinks].[PipeShape] & "*/" AS comment
FROM xpLinks INNER JOIN _PipeShapes ON xpLinks.PipeShape = [_PipeShapes].Shape
WHERE ((([_PipeShapes].XLNKCTL) Is Not Null And ([_PipeShapes].XLNKCTL)<>0));


XPXDataNKLass
----------
SELECT DISTINCTROW "DATA NKLASS" AS expr1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, [instance] & " 1" AS Expr3, xpLinks.xNKLass AS Expr4, "/* " & [PipeShape] & "*/" AS Expr5
FROM xpLinks
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPXDataNode
----------
SELECT DISTINCTROW "NODE " & [Node_Type] AS EXPR1, Chr(34) & trim([Node]) & Chr(34) AS EXPR2, tblSchematicData.[X_Coordinate], tblSchematicData.[Y_Coordinate]
FROM tblSchematicData;


XPXDataNODST
----------
SELECT "DATA NODST" AS Expr1, Chr(34) & [USNode] & Chr(34) AS EXPR2, "0 1" AS Expr3, [_xpSpecialLinkData].TValue
FROM _xpSpecialLinkData
WHERE ((([_xpSpecialLinkData].Tag) Like "NODST"))
ORDER BY Chr(34) & [USNode] & Chr(34);


XPXDataNPHI
----------
SELECT DISTINCTROW "DATA NPHI" AS EXPR1, Chr(34) & Trim([conduit_name]) & Chr(34) AS EXPR2, tblConduitData.Instance, "1" AS Expr3, Format$(IIf([Link_Up_Elevation]-[Link_Down_Elevation]>0,(([Link_Up_Elevation]-[Link_Down_Elevation])/[length])*100,0.01),"0.00000") AS Expr4
FROM tblConduitData
WHERE (((tblConduitData.Link_Type)="8"))
ORDER BY Chr(34) & Trim([conduit_name]) & Chr(34), tblConduitData.Instance;


XPXDataNTIDE
----------
SELECT "DATA NTIDE" AS Tag, Chr(34) & Trim([Node]) & Chr(34) AS NODEID, " 0 1 1" AS Expr3, "/* MLINKID  " & CStr([mlinkid]) & "  */" AS comment
FROM _Outfalls LEFT JOIN mdl_links_ac ON [_Outfalls].Node = mdl_links_ac.DSNode
GROUP BY "DATA NTIDE", Chr(34) & Trim([Node]) & Chr(34), " 0 1 1", "/* MLINKID  " & CStr([mlinkid]) & "  */";


XPXDataONKLASS
----------
SELECT DISTINCTROW "DATA ONKLASS" AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, [ToLinkNDX] & " 1" AS Expr4, [_XPSpecialLinkData].TValue
FROM _XPSpecialLinkData
WHERE ((([_XPSpecialLinkData].Tag)="ONKLASS"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataORIF
----------
SELECT DISTINCTROW "DATA ORIF" & Format$([tolinkndx],"0") AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, "0 1" AS Expr4, [_XPSpecialLinkData].TValue
FROM _XPSpecialLinkData
WHERE ((([_XPSpecialLinkData].Tag)="ORIF1"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataORIFNAME
----------
SELECT DISTINCTROW "DATA ORIFNAME" AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, [ToLinkNDX] & " 1" AS Expr4, [_XPSpecialLinkData].TValue
FROM _XPSpecialLinkData
WHERE ((([_XPSpecialLinkData].Tag) Like "ORIFNAME"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataPIDEP
----------
SELECT "DATA PIDEP" AS Expr1, Chr(34) & [speclinkid] & Chr(34) AS Expr2, [ToLinkNDX] & " 1" AS Expr3, 0 AS Expr5, "/* Sets initial depth to 0 */" AS Expr4
FROM _xpSpecialLinkData
WHERE ((([_xpSpecialLinkData].Tag)="PON"))
ORDER BY Chr(34) & [speclinkid] & Chr(34);


XPXDataPNAME
----------
SELECT "DATA PNAME" AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, [ToLinkNDX] & " 1" AS Expr4, [_xpSpecialLinkData].TValue
FROM _xpSpecialLinkData
WHERE ((([_xpSpecialLinkData].Tag) Like "PNAME"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataPOFF
----------
SELECT "DATA POFF" AS Expr1, Chr(34) & [speclinkid] & Chr(34) AS Expr2, [ToLinkNDX] & " 1" AS Expr3, [_xpSpecialLinkData].TValue, "/* Turns pump on */" AS Expr4
FROM _xpSpecialLinkData
WHERE ((([_xpSpecialLinkData].Tag)="POFF"))
ORDER BY Chr(34) & [speclinkid] & Chr(34);


XPXDataPON
----------
SELECT "DATA PON" AS Expr1, Chr(34) & [speclinkid] & Chr(34) AS Expr2, [ToLinkNDX] & " 1" AS Expr3, [_xpSpecialLinkData].TValue, "/* Turns pump on */" AS Expr4
FROM _xpSpecialLinkData
WHERE ((([_xpSpecialLinkData].Tag)="PON"))
ORDER BY Chr(34) & [speclinkid] & Chr(34);


XPXDataPSel
----------
SELECT "DATA PSEL" AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, [ToLinkNDX] & " 1" AS Expr4, [_xpSpecialLinkData].TValue
FROM _xpSpecialLinkData
WHERE ((([_xpSpecialLinkData].Tag) Like "PSEL"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataPump
----------
SELECT "DATA PUMP" & Format$([tolinkndx],"0") AS Expr1, Chr(34) & [speclinkid] & Chr(34) AS Expr2, "0 1" AS Expr3, [_xpSpecialLinkData].TValue, "/* Turns pump on */" AS Expr4
FROM _xpSpecialLinkData
WHERE ((([_xpSpecialLinkData].Tag)="PUMP1"))
ORDER BY Chr(34) & [speclinkid] & Chr(34);


XPXDataQCURA
----------
SELECT "DATA QCURA" AS Expr1, Chr(34) & [USNODE] & Chr(34) AS Expr2, 0 AS Expr4, [_xpSpecialLinkData].TValue AS Expr3
FROM _xpSpecialLinkData
WHERE ((([_xpSpecialLinkData].Tag) Like "QCURA"))
ORDER BY Chr(34) & [USNODE] & Chr(34);


XPXDataQCURRL
----------
SELECT "DATA QCURRL" AS Expr1, Chr(34) & [USNODE] & Chr(34) AS Expr2, 0 AS Expr4, [_xpSpecialLinkData].TValue AS Expr3
FROM _xpSpecialLinkData
WHERE ((([_xpSpecialLinkData].Tag) Like "QCURRL"))
ORDER BY Chr(34) & [USNODE] & Chr(34);


XPXDataQinst_ZeroAll
----------
SELECT "DATA QINST " & Chr$(34) & [Node] & Chr$(34) AS Expr3, "0 1 0" AS Expr1
FROM tblSchematicData
GROUP BY "DATA QINST " & Chr$(34) & [Node] & Chr$(34), "0 1 0";


XPXDataQinstNetAcre
----------
SELECT "DATA QINST " & Chr$(34) & [NGTO] & Chr$(34) AS Expr3, "0 1" AS Expr1, Sum(CLng([area_acres]*0.00155*10000000)/10000000) AS Expr2
FROM sc_DD
GROUP BY "DATA QINST " & Chr$(34) & [NGTO] & Chr$(34), "0 1";


XPXDataRough
----------
SELECT DISTINCTROW "DATA ROUGH" AS XPCMD, Chr(34) & Trim([xplink]) & Chr(34) AS link, xpLinks.instance, 1 AS Expr3, IIf(IsNull([Roughness]),100,[roughness]) AS n
FROM xpLinks;


XPXDataSecno
----------
SELECT DISTINCTROW "DATA SECNO" AS expr1, Chr(34) & Trim([conduit_name]) & Chr(34) AS EXPR2, (IIf([tblConduitData]![Multi_or_Single]="136","0",[instance])) & " 1" AS Expr3, tblConduitData.River_Mile
FROM tblConduitData
WHERE (((tblConduitData.River_Mile)>0))
ORDER BY Chr(34) & Trim([conduit_name]) & Chr(34);


XPXDataSFlood
----------
SELECT DISTINCTROW "DATA SFLOOD" AS EXPR1, Chr(34) & Trim([NODE]) & Chr(34) AS EXPR2, "0 1" AS EXPR3, "1" AS EXPR4
FROM mdl_nodes_ac
ORDER BY Chr(34) & Trim([NODE]) & Chr(34);


XPXDataSFloodFM
----------
SELECT "DATA SFLOOD" AS Expr1, Chr(34) & [USNode] & Chr(34) AS Expr2, " 0 " & " 1" & " 2 " AS Expr3, "/* Seals the Forcemain manholes */" AS Expr4
FROM XPlinks
WHERE (((XPlinks.PipeFlowType)="F"))
ORDER BY Chr(34) & [USNode] & Chr(34);


XPXDataSlope
----------
SELECT DISTINCTROW "DATA SLOPE" AS xpCMD, Chr(34) & Trim([xplink]) & Chr(34) AS link, xpLinks.instance, 1 AS ndx, Format$((([USIE]-[DSIE])/[length])*100,"0.00000") AS pctslope
FROM xpLinks
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPXDataStorageZ
----------
SELECT DISTINCTROW "DATA Z" AS EXPR1, Chr(34) & Trim([Node]) & Chr(34) AS Expr2, "0 1" AS EXPR3, Min([TValue]-0.2) AS Expr4
FROM mdl_speclinkdata_ac
WHERE (((mdl_speclinkdata_ac.Tag)="POFF"))
GROUP BY Chr(34) & Trim([Node]) & Chr(34);


XPXDataTMPV
----------
SELECT "DATA HDR_TMPV " & Chr$(34) & [NGTOSAN] & Chr$(34) AS Expr3, "0 1" AS Expr1, Chr(34) & "Default" & Chr(34) AS Expr2
FROM _BaseFlowComponents
WHERE ((([_BaseFlowComponents].avesanflow)<>0));


XPXDataTPhi
----------
SELECT DISTINCTROW "DATA TPHI" AS EXPR1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, [INSTANCE] & " 1" AS EXPR3, IIf([PipeShape]="TR31",3,-1) AS Expr4
FROM XPLinks
WHERE (((XPLinks.xNKLass)=6));


XPXDataTTheta
----------
SELECT DISTINCTROW "DATA TTHETA" AS EXPR1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, [INSTANCE] & " 1" AS EXPR3, IIf([PipeShape]="TR31",3,-1) AS Expr4
FROM XPLinks
WHERE (((XPLinks.xNKLass)=6));


XPXDataWeir
----------
SELECT DISTINCTROW "DATA WEIR" & Format$([tolinkndx],"0") AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, "0 1" AS Expr4, [_XPSpecialLinkData].TValue
FROM _XPSpecialLinkData
WHERE ((([_XPSpecialLinkData].Tag) Like "WEIR?"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataWEIRNAME
----------
SELECT DISTINCTROW "DATA WEIRNAME" AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, [ToLinkNDX] & " 1" AS Expr4, [_XPSpecialLinkData].TValue
FROM _XPSpeciallinkData
WHERE ((([_XPSpeciallinkData].Tag) Like "WEIRNAME"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataWide
----------
SELECT DISTINCTROW "DATA WIDE" AS EXPR1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, [instance] & " 1" AS Expr3, Format$([xWidth]/12,"0.000") AS Expr4
FROM xpLinks
WHERE (((xpLinks.xWidth)<>0) AND ((xpLinks.xNKLass)<>8))
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPXDataWLEN
----------
SELECT DISTINCTROW "DATA WLEN" AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, [ToLinkNDX] & " 1" AS Expr4, [_XPSpecialLinkData].TValue
FROM _XPSpecialLinkData
WHERE ((([_XPSpecialLinkData].Tag) Like "WLEN"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataXNLEN_L
----------
SELECT DISTINCTROW "DATA XNLEN_L" AS EXPR1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, [instance] & " 1" AS Expr3, Format$([length]*[length_factor_left],"0") AS Expr4, "/* left bank length factor: " & [length_factor_left] & " */" AS Expr5
FROM xpLinks INNER JOIN mst_xsections ON xpLinks.MLinkID = mst_xsections.mlinkid
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPXDataXNLEN_R
----------
SELECT DISTINCTROW "DATA XNLEN_R" AS EXPR1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, [instance] & " 1" AS Expr3, Format$([length]*[length_factor_right],"0") AS Expr4, "/* right bank length factor: " & [length_factor_left] & " */" AS Expr5
FROM xpLinks INNER JOIN mst_xsections ON xpLinks.MLinkID = mst_xsections.mlinkid
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPXDataYCOEFF
----------
SELECT DISTINCTROW "DATA COEFF" AS expr1, Chr(34) & Trim([conduit_name]) & Chr(34) AS EXPR2, [INSTANCE] & " 1" AS EXPR3, tblConduitData.Cd
FROM tblConduitData
WHERE (((tblConduitData.Cd)>0));


XPXDataYCREST
----------
SELECT DISTINCTROW "DATA YCREST" AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, [ToLinkNDX] & " 1" AS Expr4, [_XPSpecialLinkData].TValue AS Expr3
FROM _XPSpecialLinkData
WHERE ((([_XPSpecialLinkData].Tag) Like "YCREST"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataYTOP
----------
SELECT DISTINCTROW "DATA YTOP" AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, [ToLinkNDX] & " 1" AS Expr4, [_XPSpecialLinkData].TValue
FROM _XPSpecialLinkData
WHERE ((([_XPSpecialLinkData].Tag) Like "YTOP"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataZ
----------
SELECT DISTINCTROW "DATA Z" AS EXPR1, Chr(34) & Trim([node]) & Chr(34) AS EXPR2, "0 1" AS EXPR3, NodeIE AS Expr4
FROM xpnodes;


XPXDataZP
----------
SELECT DISTINCTROW "DATA ZP" AS Expr1, Chr(34) & [speclinkID] & Chr(34) AS EXPR2, [ToLinkNDX] & " 1" AS Expr4, [_XPSpecialLinkData].TValue AS Expr3
FROM _XPSpecialLinkData
WHERE ((([_XPSpecialLinkData].[Tag]) Like "ZP"))
ORDER BY Chr(34) & [speclinkID] & Chr(34);


XPXDataZP1
----------
SELECT DISTINCTROW "DATA ZP1" AS EXPR1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, [instance] & " 1" AS Expr3, Format$([USIE],"0.00") AS Expr4
FROM xpLinks
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPXDataZP2
----------
SELECT DISTINCTROW "DATA ZP2" AS EXPR1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, [instance] & " 1" AS Expr3, Format$([DSIE],"0.00") AS Expr4
FROM xpLinks
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPXDataZTop
----------
SELECT DISTINCTROW "DATA ZTOP" AS EXPR1, Chr(34) & [tblNodeStorageData]![NODE] & Chr(34) AS expr7, "0 1" AS EXPR3, [tblSchematicData]![Ground_Elevation]+1 AS Expr2
FROM tblSchematicData INNER JOIN tblNodeStorageData ON tblSchematicData.Node = tblNodeStorageData.Node
ORDER BY Chr(34) & [tblNodeStorageData]![NODE] & Chr(34);


XPXHydroOn
----------
TRANSFORM Max("1") AS Expr2
SELECT "DATA R_RFCMNT" AS Expr1, tblSubCatchments.NGTO AS Node, "0" AS Instance, Max([NGTO_Instance])+1 AS [Index]
FROM tblSubCatchments
GROUP BY "DATA R_RFCMNT", tblSubCatchments.NGTO, "0"
ORDER BY tblSubCatchments.NGTO
PIVOT tblSubCatchments.NGTO_Instance;


XPXHydroRainfallGrp
----------
SELECT DISTINCTROW "DATA R_RAINSEL" AS expr1, tblSubCatchments.NGTO AS tonode, tblSubCatchments.NGTO_Instance AS tonodeinstance, 1 AS ndx, tblSubCatchments.RainGage
FROM tblSubCatchments;


XPXHydroSlope
----------
TRANSFORM Max(tblSubCatchments.Slope) AS MaxOfSlope
SELECT "DATA R_WSLOPE" AS Expr1, tblSubCatchments.NGTO AS Node, "0" AS Instance, Max([NGTO_Instance])+1 AS [Index]
FROM tblSubCatchments
GROUP BY "DATA R_WSLOPE", tblSubCatchments.NGTO, "0"
ORDER BY tblSubCatchments.NGTO
PIVOT tblSubCatchments.NGTO_Instance;


XPXLen
----------
SELECT DISTINCTROW "DATA LEN" AS EXPR1, Chr(34) & Trim([conduit_name]) & Chr(34) AS EXPR2, (IIf([tblConduitData]![Multi_or_Single]="138",[INSTANCE],"0")) & " 1" AS EXPR3, tblConduitData.Length
FROM tblConduitData
WHERE (((tblConduitData.Length)>0));


XPXLink
----------
SELECT DISTINCTROW "LINK 136" AS expr1, Chr(34) & Trim([xplink]) & Chr(34) AS EXPR2, Chr(34) & Trim([xplinks].[USnode]) & Chr(34) AS EXPR3, Chr(34) & Trim([xplinks].[DSNODE]) & Chr(34) AS EXPR4
FROM XPlinks
WHERE (((XPlinks.isparallel)="F"))
GROUP BY "LINK 136", Chr(34) & Trim([xplink]) & Chr(34), Chr(34) & Trim([xplinks].[USnode]) & Chr(34), Chr(34) & Trim([xplinks].[DSNODE]) & Chr(34)
ORDER BY Chr(34) & Trim([xplink]) & Chr(34);


XPXLinkMulti
----------
SELECT DISTINCTROW "LINK 138" AS expr1, Chr(34) & Trim([SpeclinkID]) & Chr(34) AS EXPR2, Chr(34) & Trim([USnode]) & Chr(34) AS EXPR3, Chr(34) & Trim([DSNODE]) & Chr(34) AS EXPR4
FROM _XPSpecialLinks
ORDER BY Chr(34) & Trim([SpeclinkID]) & Chr(34);

UNION SELECT "LINK 138" AS expr1, Chr(34) & Trim([_XPParallelLinks].[PLinkID]) & Chr(34) AS expr2, Chr(34) & Trim([_XPParallelLinks].[USNode]) & Chr(34) AS expr3, Chr(34) & Trim$([_XPParallelLinks].[dsNode]) & Chr(34) AS expr4
FROM _false_parallel_links RIGHT JOIN (_XPParallelLinks INNER JOIN _parallel_links ON [_XPParallelLinks].PLinkID = [_parallel_links].PLinkID) ON [_false_parallel_links].LinkID = [_parallel_links].LinkID
WHERE ((([_false_parallel_links].LinkID) Is Null));


XPXNode
----------
SELECT DISTINCTROW "NODE " & "134" AS EXPR1, Chr(34) & Trim([Node]) & Chr(34) AS EXPR2, xpNodes.XCoord, xpNodes.YCoord
FROM xpNodes
ORDER BY Chr(34) & Trim([Node]) & Chr(34);


XPXNodeColor
----------
SELECT DISTINCTROW "DATA _COLOR" AS EXPR1, Chr(34) & Trim([CONDUIT_NAME]) & Chr(34) AS ID, "0 1" AS EXPR2, tblConduitData.Color
FROM tblConduitData
WHERE (((tblConduitData.Color) Is Not Null));


XPXT_NTYPE
----------
SELECT DISTINCTROW "DATA T_NTYPE" AS EXPR1, Chr(34) & Trim([node]) & Chr(34) AS EXPR2, "0 1 " AS EXPR3, Chr(34) & "19" & Chr(34) AS Expr4
FROM tblSchematicData;


XPXUserAREA
----------
TRANSFORM Min([_UserDefinedGeometry].Area) AS MinOfArea
SELECT "DATA AREA" AS Expr1, Chr(34) & [xplink] & Chr(34) AS Expr2, XPlinks.instance, Max([_UserDefinedGeometry].Index) AS MaxOfIndex
FROM XPlinks INNER JOIN _UserDefinedGeometry ON (XPlinks.PipeShape = [_UserDefinedGeometry].Shape) AND (XPlinks.xHeight = [_UserDefinedGeometry].Height) AND (XPlinks.xWidth = [_UserDefinedGeometry].Width)
GROUP BY XPlinks.PipeShape, "DATA AREA", Chr(34) & [xplink] & Chr(34), XPlinks.instance
ORDER BY Chr(34) & [xplink] & Chr(34), XPlinks.PipeShape
PIVOT [_UserDefinedGeometry].Index;


XPXUserDEP
----------
TRANSFORM Min([_UserDefinedGeometry].Depth) AS MinOfDepth
SELECT "DATA DEP" AS Expr1, Chr(34) & [xplink] & Chr(34) AS Expr2, XPlinks.instance, Max([_UserDefinedGeometry].Index) AS MaxOfIndex
FROM XPlinks INNER JOIN _UserDefinedGeometry ON (XPlinks.PipeShape = [_UserDefinedGeometry].Shape) AND (XPlinks.xHeight = [_UserDefinedGeometry].Height) AND (XPlinks.xWidth = [_UserDefinedGeometry].Width)
GROUP BY XPlinks.PipeShape, "DATA DEP", Chr(34) & [xplink] & Chr(34), XPlinks.instance
ORDER BY Chr(34) & [xplink] & Chr(34), XPlinks.PipeShape
PIVOT [_UserDefinedGeometry].Index;


XPXUserSW
----------
TRANSFORM Min([_UserDefinedGeometry].Surface) AS MinOfSurface
SELECT "DATA SW" AS Expr1, Chr(34) & [xplink] & Chr(34) AS Expr2, XPlinks.instance, Max([_UserDefinedGeometry].Index) AS MaxOfIndex
FROM XPlinks INNER JOIN _UserDefinedGeometry ON (XPlinks.PipeShape = [_UserDefinedGeometry].Shape) AND (XPlinks.xHeight = [_UserDefinedGeometry].Height) AND (XPlinks.xWidth = [_UserDefinedGeometry].Width)
GROUP BY XPlinks.PipeShape, "DATA SW", Chr(34) & [xplink] & Chr(34), XPlinks.instance
ORDER BY Chr(34) & [xplink] & Chr(34), XPlinks.PipeShape
PIVOT [_UserDefinedGeometry].Index;


XPXUserWP
----------
TRANSFORM Min([_UserDefinedGeometry].WP) AS MinOfWP
SELECT "DATA WP" AS Expr1, Chr(34) & [xplink] & Chr(34) AS Expr2, XPlinks.instance, Max([_UserDefinedGeometry].Index) AS MaxOfIndex
FROM XPlinks INNER JOIN _UserDefinedGeometry ON (XPlinks.PipeShape = [_UserDefinedGeometry].Shape) AND (XPlinks.xHeight = [_UserDefinedGeometry].Height) AND (XPlinks.xWidth = [_UserDefinedGeometry].Width)
GROUP BY XPlinks.PipeShape, "DATA WP", Chr(34) & [xplink] & Chr(34), XPlinks.instance
ORDER BY Chr(34) & [xplink] & Chr(34), XPlinks.PipeShape
PIVOT [_UserDefinedGeometry].Index;


zXPXHydroArea
----------
TRANSFORM Max(tblSubcatchments.Area) AS MaxOfArea
SELECT "DATA R_WAREA" AS Expr1, [NGTO]+" 0" AS Expr2, Max([NGTO_Instance])+1 AS [Index]
FROM tblSubcatchments
GROUP BY "DATA R_WAREA", [NGTO]+" 0"
ORDER BY "DATA R_WAREA", [NGTO]+" 0"
PIVOT tblSubcatchments.NGTO_Instance;


zXPXHydroImpervious
----------
TRANSFORM Max(tblSubcatchments.Pct_impervious) AS MaxOfPct_impervious
SELECT "DATA R_WIMP" AS Expr1, tblSubcatchments.NGTO AS Node, "0" AS Instance, Max([NGTO_Instance])+1 AS [Index]
FROM tblSubcatchments
GROUP BY "DATA R_WIMP", tblSubcatchments.NGTO, "0"
ORDER BY tblSubcatchments.NGTO
PIVOT tblSubcatchments.NGTO_Instance;


zXPXSWMMCrossSectionElevation
----------
TRANSFORM Max(tblCrossSectionData.Elevation) AS MaxOfElevation
SELECT "DATA EL" AS Expr1, Chr(34) & Trim([tblConduitData]![Conduit_Name]) & Chr(34) AS Expr2, (IIf([tblConduitData]![Multi_or_Single]="138",[tblCrossSectionData]![Instance],"0")) AS Instance, Max(tblCrossSectionData.Index) AS MaxOfIndex
FROM tblConduitData INNER JOIN tblCrossSectionData ON (tblConduitData.Instance = tblCrossSectionData.Instance) AND (tblConduitData.Conduit_Name = tblCrossSectionData.Conduit_Name)
WHERE (((tblConduitData.Link_Type)="8"))
GROUP BY "DATA EL", Chr(34) & Trim([tblConduitData]![Conduit_Name]) & Chr(34), (IIf([tblConduitData]![Multi_or_Single]="138",[tblCrossSectionData]![Instance],"0")), tblConduitData.Conduit_Name
PIVOT tblCrossSectionData.Index;


zXPXSWMMCrossSectionStation
----------
TRANSFORM Max(tblCrossSectionData.Station) AS MaxOfStation
SELECT "DATA STA" AS Expr1, Chr(34) & Trim([tblConduitData]![Conduit_Name]) & Chr(34) AS Expr2, (IIf([tblConduitData]![Multi_or_Single]="138",[tblCrossSectionData]![Instance],"0")) AS Instance, Max(tblCrossSectionData.Index) AS MaxOfIndex
FROM tblConduitData INNER JOIN tblCrossSectionData ON (tblConduitData.Instance = tblCrossSectionData.Instance) AND (tblConduitData.Conduit_Name = tblCrossSectionData.Conduit_Name)
WHERE (((tblConduitData.Link_Type)="8"))
GROUP BY "DATA STA", Chr(34) & Trim([tblConduitData]![Conduit_Name]) & Chr(34), (IIf([tblConduitData]![Multi_or_Single]="138",[tblCrossSectionData]![Instance],"0")), tblConduitData.Conduit_Name
PIVOT tblCrossSectionData.Index;


zzgetspeclinks
----------
SELECT mst_SpecLinks_ac.Node, mst_SpecLinks_ac.ToMLinkID, mst_SpecLinks_ac.ToLinkNDX, mst_SpecLinks_ac.SpecLinkType
FROM mst_SpecLinks_ac INNER JOIN mdl_Links_ac ON (mst_SpecLinks_ac.Node = mdl_Links_ac.USNode) AND (mst_SpecLinks_ac.ToMLinkID = mdl_Links_ac.MLinkID);


