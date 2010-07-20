_ALLNodeIEFromLink
----------
SELECT  * from _getminupelev
UNION SELECT  * from _getmindnelev;


***_getmaxdianodedn
----------
SELECT xLinks.DSNode, Max(IIf([Pipeshape]="CIRC",[DiamWidth],[Height])) AS MaxOfxHeight
FROM xLinks
GROUP BY xLinks.DSNode;


***_getmaxdianodeus
----------
SELECT xLinks.USNode, Max(IIf([PipeShape]="CIRC",[DiamWidth],[Height])) AS MaxOfxHeight
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


_minNodeIEfromLink
----------
SELECT [_ALLNodeIEFromLink].Node, Min([_ALLNodeIEFromLink].Z) AS MinOfZ
FROM _ALLNodeIEFromLink
GROUP BY [_ALLNodeIEFromLink].Node;


Qcheck01C: Conduits Missing Nodes
----------
SELECT xLinks.explink, xLinks.LinkID, xLinks.MLinkID, xLinks.HLinkID, xLinks.USNode, xLinks.DSNode
FROM (xLinks LEFT JOIN xNodes AS xNodes_1 ON xLinks.DSNode = xNodes_1.Node) LEFT JOIN xNodes ON xLinks.USNode = xNodes.Node
WHERE (((xNodes.Node) Is Null)) OR (((xNodes_1.Node) Is Null));


Qcheck01C: Conduits with no DS Conduit
----------
SELECT xLinks.explink, xLinks.LinkID, xLinks.MLinkID, xLinks.HLinkID, xLinks.USNode, xLinks.DSNode
FROM xLinks LEFT JOIN xLinks AS xLinks_1 ON xLinks.DSNode = xLinks_1.USNode
WHERE (((xLinks_1.DSNode) Is Null))
GROUP BY xLinks.explink, xLinks.LinkID, xLinks.MLinkID, xLinks.HLinkID, xLinks.USNode, xLinks.DSNode;


***Qcheck01C: NonCircularPipes
----------
SELECT xLinks.LinkID, xLinks.MLinkID, xLinks.HLinkID, xLinks.Height, xLinks.DiamWidth, xLinks.PipeShape, xLinks.Material
FROM xLinks
WHERE (((xLinks.PipeShape)<>"CIRC" Or (xLinks.PipeShape) Is Null) AND ((xLinks.IsSpecLink)=False)) OR (((xLinks.DiamWidth)<>[HEIGHT]) AND ((xLinks.PipeShape)<>"CIRC" Or (xLinks.PipeShape) Is Null) AND ((xLinks.IsSpecLink)=False));


QCheck01C: Parallel Pipes
----------
SELECT First(xLinks.explink) AS FirstOfexplink, First(xLinks.LinkID) AS FirstOfLinkID, xLinks.USNode, xLinks.DSNode, Count(xLinks.LinkID) AS CountOfLinkID
FROM xLinks
GROUP BY xLinks.USNode, xLinks.DSNode
HAVING (((Count(xLinks.LinkID))>1));


Qcheck02C: Zero Up/Down Elev (no SL)
----------
SELECT xLinks.explink, xLinks.LinkID, xLinks.MLinkID, xLinks.HLinkID, xLinks.DiamWidth, xLinks.Height, xLinks.USNode, xLinks.DSNode, xLinks.USIE, xLinks.DSIE
FROM xLinks
WHERE (((xLinks.USIE)=0 Or (xLinks.USIE) Is Null) AND ((xLinks.IsSpecLink)=False)) OR (((xLinks.DSIE)=0 Or (xLinks.DSIE) Is Null) AND ((xLinks.IsSpecLink)=False))
ORDER BY xLinks.HLinkID;


Qcheck02N: Node data with Null/0 Grelev
----------
SELECT xNodes.Node, xLinks.explink AS USxplink, xLinks_1.explink AS DSxplink, xNodes.GrndElev, CLng([Xcoord]*10)/10 AS XC, CLng([YCoord]*10)/10 AS YC
FROM (xNodes LEFT JOIN xLinks AS xLinks_1 ON xNodes.Node = xLinks_1.USNode) LEFT JOIN xLinks ON xNodes.Node = xLinks.DSNode
WHERE (((xNodes.GrndElev) Is Null Or (xNodes.GrndElev)=0))
ORDER BY xNodes.Node;


***Qcheck03C: Crown > US Grelev
----------
SELECT xNodes.Node, CLng([xnodes].[GrndElev]*100)/100 AS USGrelev, Format$([USIE]+IIf([PipeShape]="circ" Or [PipeShape]="unk",[DIAMWIDTH],[HEIGHT])/12,"0.00") AS USCrown
FROM xNodes INNER JOIN (xLinks LEFT JOIN xNodes AS xNodes_1 ON xLinks.DSNode = xNodes_1.Node) ON xNodes.Node = xLinks.USNode
WHERE (((CLng([xnodes].[GrndElev]*100)/100)<>0 And (CLng([xnodes].[GrndElev]*100)/100) Is Not Null) AND ((Format$([USIE]+IIf([PipeShape]="circ" Or [PipeShape]="unk",[DIAMWIDTH],[HEIGHT])/12,"0.00"))>[xNodes].[Grndelev]) AND ((xLinks.IsSpecLink)=False))
ORDER BY xNodes.Node;


Qcheck03N: Deep Manholes (>20 feet)
----------
SELECT xNodes.Node, xLinks.explink AS USxplink, xLinks.explink AS DSxplink, xNodes.GrndElev, CLng((numnull([GrndElev])-numnull([NodeIE]))*100)/100 AS DepthToIE, CLng([Xcoord]*10)/10 AS XC, CLng([YCoord]*10)/10 AS YC
FROM (xNodes INNER JOIN xLinks AS xLinks_1 ON xNodes.Node = xLinks_1.USNode) INNER JOIN xLinks ON xNodes.Node = xLinks.DSNode
WHERE (((CLng((numnull([GrndElev])-numnull([NodeIE]))*100)/100)>20));


Qcheck04C: Negative Slope
----------
SELECT xLinks.explink, xLinks.LinkID, xLinks.MLinkID, xLinks.HLinkID, xLinks.Length, CLng(([usie]-[dsie])*10000)/10000 AS dH, xLinks.xSlope
FROM xLinks
WHERE (((xLinks.Length)<>0) AND ((CLng(([usie]-[dsie])*10000)/10000)<0) AND ((xLinks.USIE)<>0) AND ((xLinks.DSIE)<>0) AND ((xLinks.IsSpecLink)=False));


Qcheck05C: Absolute Jumps
----------
SELECT [_getminupelev].Node AS Node, xLinks.MLinkID AS USMLinkID, [_getmindnelev].Z AS Lowest_in, [_getminupelev].Z AS Lowest_out
FROM xLinks INNER JOIN (_getminupelev INNER JOIN _getmindnelev ON [_getminupelev].Node = [_getmindnelev].Node) ON (xLinks.DSIE = [_getmindnelev].Z) AND (xLinks.DSNode = [_getmindnelev].Node)
GROUP BY [_getminupelev].Node, xLinks.MLinkID, [_getmindnelev].Z, [_getminupelev].Z
HAVING ((([_getminupelev].Z)>[_getmindnelev].[z]));


Qcheck06C: Any Jump
----------
SELECT xNodes.Node, xLinks_1.MLinkID AS USMlinkID, Min(xLinks_1.DSIE) AS USLinkDnElev, Min(xLinks.USIE) AS DSLinkUpElev
FROM (xLinks AS xLinks_1 INNER JOIN xNodes ON xLinks_1.DSNode = xNodes.Node) INNER JOIN xLinks ON xNodes.Node = xLinks.USNode
WHERE (((xLinks_1.DSIE)<[xlinks].[USIE]) AND ((xLinks_1.IsSpecLink)=False) AND ((xLinks.IsSpecLink)=False))
GROUP BY xNodes.Node, xLinks_1.MLinkID;


Qcheck06C: delta length and CoordLen > 20
----------
SELECT xLinks.explink, xLinks.LinkID, xLinks.MLinkID, xLinks.HLinkID, xLinks.USNode, xLinks.DSNode, xLinks.Length, CLng((([xnodes].[XCoord]-[xnodes_1].[XCoord])^2+([xnodes].[YCoord]-[xnodes_1].[YCoord])^2)^0.5) AS Pythaglen, Abs([Length]-CLng((([xnodes].[XCoord]-[xnodes_1].[XCoord])^2+([xnodes].[YCoord]-[xnodes_1].[YCoord])^2)^0.5)) AS absLenDiff1, xLinks.IsSpecLink
FROM (xLinks INNER JOIN xNodes AS xNodes_1 ON xLinks.DSNode = xNodes_1.Node) INNER JOIN xNodes ON xLinks.USNode = xNodes.Node
WHERE (((xLinks.USNode) Is Not Null) AND ((xLinks.DSNode) Is Not Null) AND ((Abs([Length]-CLng((([xnodes].[XCoord]-[xnodes_1].[XCoord])^2+([xnodes].[YCoord]-[xnodes_1].[YCoord])^2)^0.5)))>20) AND ((xLinks.IsSpecLink)=False))
ORDER BY Abs([Length]-CLng((([xnodes].[XCoord]-[xnodes_1].[XCoord])^2+([xnodes].[YCoord]-[xnodes_1].[YCoord])^2)^0.5)) DESC;


***Qcheck06C: downsizediameter
----------
SELECT xLinks.explink AS exp_IN, xLinks.HLinkID AS Cond_IN, [_getmaxdianodeus].USNode AS Node, xLinks.PipeShape AS frmShape, xLinks_1.PipeShape AS toShape, [_getmaxdianodedn].MaxOfxHeight AS Max_IN_in, [_getmaxdianodeus].MaxOfxHeight AS Max_OUT_in
FROM (xLinks INNER JOIN (_getmaxdianodedn INNER JOIN _getmaxdianodeus ON [_getmaxdianodedn].DSNode = [_getmaxdianodeus].USNode) ON xLinks.DSNode = [_getmaxdianodedn].DSNode) INNER JOIN xLinks AS xLinks_1 ON [_getmaxdianodeus].USNode = xLinks_1.USNode
WHERE (((xLinks.IsSpecLink)=False) AND ((xLinks_1.IsSpecLink)=False) AND ((IIf([xlinks_1].[PipeShape]="CIRC",[xlinks_1].[DiamWidth],[xlinks_1].[Height]))=[_getmaxdianodeus].[maxofxheight]) AND ((xLinks_1.IsSpecLink)=False))
GROUP BY xLinks.explink, xLinks.HLinkID, [_getmaxdianodeus].USNode, xLinks.PipeShape, xLinks_1.PipeShape, [_getmaxdianodedn].MaxOfxHeight, [_getmaxdianodeus].MaxOfxHeight
HAVING ((([_getmaxdianodeus].MaxOfxHeight)<[_getmaxdianodedn].[maxofxheight]));


***Qcheck07C: Missing Material
----------
SELECT xLinks.explink, xLinks.LinkID, xLinks.MLinkID, xLinks.HLinkID, xLinks.Height, xLinks.DiamWidth, xLinks.PipeShape
FROM xLinks
WHERE (((xLinks.Material) Is Null Or (xLinks.Material)="") AND ((xLinks.IsSpecLink)=False));


***xLinks
----------
SELECT exportkey([compkey],[linkid]) AS explink, mdl_Links_ac.LinkID, mdl_Links_ac.MLinkID, mdl_Links_ac.CompKey AS HLinkID, mdl_Links_ac.USNode, mdl_Links_ac.DSNode, mdl_Links_ac.PipeShape, mdl_Links_ac.USIE, mdl_Links_ac.DSIE, mdl_Links_ac.Length, mdl_Links_ac.Height, mdl_Links_ac.DiamWidth, Format$(IIf([length]=0,-99,(([usie]-[dsie])/[length])),"0.00000") AS xSlope, mdl_Links_ac.Material, mdl_Links_ac.AsBuilt, mdl_Links_ac.Roughness, mdl_Links_ac.LinkType, mdl_Links_ac.LinkReach, mdl_Links_ac.ReachElement, mdl_Links_ac.IsSpecLink
FROM mdl_Links_ac;


xNodes
----------
SELECT mdl_nodes_ac.*, numnull([MinOfZ]) AS NODEIE
FROM mdl_nodes_ac LEFT JOIN _minNodeIEfromLink ON mdl_nodes_ac.Node = [_minNodeIEfromLink].Node;


