_ComputeHGL
----------
SELECT mdl_DirSC_ac.ParcelID, mdl_DirSC_ac.DivideID, tableE09.MaxJElev AS USJelev, tableE09_1.MaxJElev AS DSJelev, mdl_Laterals_ac.Frac2SwrBeg, CDbl(Format$([tableE09].[maxjelev]-([Frac2swrbeg]*([tableE09].[maxjelev]-[tableE09_1].[maxjelev])),"0.00")) AS HGL, CDbl(Format$([FloodRefElev]-([tableE09].[maxjelev]-([Frac2swrbeg]*([tableE09].[maxjelev]-[tableE09_1].[maxjelev]))),"0.00")) AS dHGL
FROM (((mdl_DirSC_ac INNER JOIN mdl_Laterals_ac ON mdl_DirSC_ac.LateralIDSan = mdl_Laterals_ac.LateralID) INNER JOIN mdl_Links_ac ON mdl_Laterals_ac.LinkID = mdl_Links_ac.LinkID) INNER JOIN tableE09 ON mdl_Links_ac.USNode = tableE09.NodeName) INNER JOIN tableE09 AS tableE09_1 ON mdl_Links_ac.DSNode = tableE09_1.NodeName;


Query01_initialize
----------
UPDATE mdl_DirSC_ac SET mdl_DirSC_ac.HGL = Null, mdl_DirSC_ac.DeltaHGL = 10;


Query02_update
----------
UPDATE _ComputeHGL INNER JOIN mdl_DirSC_ac ON ([_ComputeHGL].DivideID = mdl_DirSC_ac.DivideID) AND ([_ComputeHGL].ParcelID = mdl_DirSC_ac.ParcelID) SET mdl_DirSC_ac.HGL = [_computehgl].[hgl], mdl_DirSC_ac.DeltaHGL = [_computehgl].[dhgl];


Query1
----------
SELECT mdl_DirSC_ac.*
FROM mdl_DirSC_ac LEFT JOIN _ComputeHGL ON (mdl_DirSC_ac.DivideID = [_ComputeHGL].DivideID) AND (mdl_DirSC_ac.ParcelID = [_ComputeHGL].ParcelID)
WHERE ((([_ComputeHGL].ParcelID) Is Not Null));


