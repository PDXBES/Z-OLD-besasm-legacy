qryAppendSpecLinkData
----------
INSERT INTO mdl_SpecLinkData_ac ( Node, ToLinkID, ToLinkNDX, Tag, TValue, NValue, SLID )
SELECT mdl_SpecLinks_ac.Node, mdl_SpecLinks_ac.ToLinkID, mdl_SpecLinks_ac.ToLinkNDX, mst_SpecLinkData_ac.Tag, mst_SpecLinkData_ac.TValue, mst_SpecLinkData_ac.NValue, mst_SpecLinkData_ac.SLID
FROM mdl_SpecLinks_ac INNER JOIN (mst_SpecLinks_ac INNER JOIN mst_SpecLinkData_ac ON mst_SpecLinks_ac.SLID = mst_SpecLinkData_ac.SLID) ON mdl_SpecLinks_ac.SLID = mst_SpecLinks_ac.SLID;


qrySpecLinkGageID
----------
UPDATE (mdl_nodes_ac INNER JOIN mdl_links_ac ON mdl_nodes_ac.Node = mdl_links_ac.DSNode) INNER JOIN mdl_nodes_ac AS mdl_nodes_ac_1 ON mdl_links_ac.USNode = mdl_nodes_ac_1.Node SET mdl_nodes_ac.GageID = [mdl_nodes_ac_1].[gageid]
WHERE (((mdl_nodes_ac.GageID) Is Null Or (mdl_nodes_ac.GageID)="") AND ((mdl_nodes_ac_1.GageID) Is Not Null And (mdl_nodes_ac_1.GageID)<>"") AND ((mdl_links_ac.IsSpecLink)=True));


zzqryAppendSpecLinkData
----------
INSERT INTO mdl_SpecLinkData_ac ( Node, ToLinkID, ToLinkNDX, Tag, TValue )
SELECT mdl_SpecLinks_ac.Node, mdl_SpecLinks_ac.ToLinkID, mdl_SpecLinks_ac.ToLinkNDX, mst_SpecLinkData_ac.Tag, mst_SpecLinkData_ac.TValue
FROM (mdl_Links_ac INNER JOIN mdl_SpecLinks_ac ON mdl_Links_ac.LinkID = mdl_SpecLinks_ac.ToLinkID) INNER JOIN mst_SpecLinkData_ac ON (mdl_Links_ac.MLinkID = mst_SpecLinkData_ac.ToMLinkID) AND (mdl_SpecLinks_ac.ToLinkNDX = mst_SpecLinkData_ac.ToLinkNDX);


