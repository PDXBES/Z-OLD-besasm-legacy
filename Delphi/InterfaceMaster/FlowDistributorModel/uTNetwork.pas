{*******************************************************}
{*
{* uTNetwork.pas
{* Delphi Implementation of the Class TNetwork
{* Generated by Enterprise Architect
{* Created on:      03-May-2006 11:45:49 AM
{* Original author: Arnel Mandilag
{*
{*******************************************************}

unit uTNetwork;

interface

uses SysUtils, Contnrs, Classes, Math,
	uTLink, uTLinksList, utNode, utNodesList;

type

	TNetwork = class
	private
		Links: TLinksList;
		Nodes: TNodesList;
	public
		procedure ClearVisits;
		function FindDownstreamLinks(ALink: TLink): TLinksList;
		function FindUpstreamLinks(ALink: TLink): TLinksList;
		function LinkByID(AID: Integer): TLink;
		function VisitedNodes: TStringList;
		property NodesList: TNodesList read Nodes;
		constructor Create(AModelPath: TFileName); overload;
		destructor Destroy; override;
		constructor Create; overload;
	end;

implementation

uses dmIM_FlowDistributionSupport, fProgress, Forms;

{implementation of TNetwork}

constructor TNetwork.Create;
begin
	inherited Create;
end;

procedure TNetwork.ClearVisits;
var
	i: Integer;
begin
	for i := 0 to Links.Count - 1 do
		Links[i].Unvisit;
end;

function TNetwork.FindDownstreamLinks(ALink: TLink): TLinksList;
var
	i: Integer;
begin
	Result := TLinksList.Create(False);
	for i := 0 to Links.Count - 1 do
	begin
		if Links[i].USNode = ALink.DSNode then
			Result.Add(Links[i]);
	end;
	if Result.Count = 0 then
		FreeAndNil(Result);
end;

function TNetwork.FindUpstreamLinks(ALink: TLink): TLinksList;
var
  i: Integer;
begin
	Result := TLinksList.Create(False);
	for i := 0 to Links.Count - 1 do
	begin
		if Links[i].DSNode = ALink.USNode then
			Result.Add(Links[i]);
	end;
	if Result.Count = 0 then
		FreeAndNil(Result);
end;

function TNetwork.LinkByID(AID: Integer): TLink;
var
	i: Integer;
begin
	Result := nil;
	for i := 0 to Links.Count - 1 do
		if Links[i].LinkID = AID then
		begin
			Result := Links[i];
			Exit;
		end;

end;

function TNetwork.VisitedNodes: TStringList;
var
  i: Integer;
begin
	Result := TStringList.Create;
	Result.Sorted := True;
	Result.Duplicates := dupIgnore;
	for i := 0 to Links.Count - 1 do
	begin
		if Links[i].HasBeenVisited then
		begin
			Result.Add(Links[i].USNode.NodeID);
			Result.Add(Links[i].DSNode.NodeID);
		end;
  end;
end;

constructor TNetwork.Create(AModelPath: TFileName);
var
	ALink: TLink;
	ANode: TNode;
	USNode: TNode;
	DSNode: TNode;
begin
	Links := TLinksList.Create(True);
	Nodes := TNodesList.Create(True);

	// Open connection to nodes database
	dmFlowDistributionSupport.adoConnNodes.ConnectionString :=
		'Provider=Microsoft.Jet.OLEDB.4.0;'+
		'User ID=Admin;'+
		'Data Source='+ExtractFileDir(AModelPath)+'\nodes\mdl_nodes_ac.mdb;'+
		'Mode=Share Deny None;'+
		'Extended Properties="";'+
		'Jet OLEDB:System database="";'+
		'Jet OLEDB:Registry Path="";'+
		'Jet OLEDB:Database Password="";'+
		'Jet OLEDB:Engine Type=4;'+
		'Jet OLEDB:Database Locking Mode=0;'+
		'Jet OLEDB:Global Partial Bulk Ops=2;'+
		'Jet OLEDB:Global Bulk Transactions=1;'+
		'Jet OLEDB:New Database Password="";'+
		'Jet OLEDB:Create System Database=False;'+
		'Jet OLEDB:Encrypt Database=False;'+
		'Jet OLEDB:Don''t Copy Locale on Compact=False;'+
		'Jet OLEDB:Compact Without Replica Repair=False;'+
		'Jet OLEDB:SFP=False';
	dmFlowDistributionSupport.adoConnNodes.Connected := True;

	// Loop through nodes table and create internal list of nodes
	dmFlowDistributionSupport.adoNodes.Open;
	dmFlowDistributionSupport.adoNodes.First;
	frmProgress.prgProgress.TotalParts := dmFlowDistributionSupport.adoNodes.RecordCount;
	frmProgress.prgProgress.PartsComplete := 0;
	frmProgress.lblProgress.Caption := 'Reading nodes';
	frmProgress.Show;
	while not dmFlowDistributionSupport.adoNodes.EOF do
	begin
		ANode := TNode.Create(
			dmFlowDistributionSupport.adoNodes.FieldValues['Node'],
			0,
			0,
			0);
		Nodes.Add(ANode);
		dmFlowDistributionSupport.adoNodes.Next;
		frmProgress.prgProgress.IncPartsByOne;
		Application.ProcessMessages;
	end;
	dmFlowDistributionSupport.adoNodes.Close;
	frmProgress.Hide;

	// Open connection to links database
	dmFlowDistributionSupport.adoConnLinks.ConnectionString :=
		'Provider=Microsoft.Jet.OLEDB.4.0;'+
		'User ID=Admin;'+
		'Data Source='+ExtractFileDir(AModelPath)+'\links\mdl_links_ac.mdb;'+
		'Mode=Share Deny None;'+
		'Extended Properties="";'+
		'Jet OLEDB:System database="";'+
		'Jet OLEDB:Registry Path="";'+
		'Jet OLEDB:Database Password="";'+
		'Jet OLEDB:Engine Type=4;'+
		'Jet OLEDB:Database Locking Mode=0;'+
		'Jet OLEDB:Global Partial Bulk Ops=2;'+
		'Jet OLEDB:Global Bulk Transactions=1;'+
		'Jet OLEDB:New Database Password="";'+
		'Jet OLEDB:Create System Database=False;'+
		'Jet OLEDB:Encrypt Database=False;'+
		'Jet OLEDB:Don''t Copy Locale on Compact=False;'+
		'Jet OLEDB:Compact Without Replica Repair=False;'+
		'Jet OLEDB:SFP=False';
	dmFlowDistributionSupport.adoConnLinks.Connected := True;

	// Loop through links table and create internal list of links for tracing
	dmFlowDistributionSupport.adoLinks.Open;
	dmFlowDistributionSupport.adoLinks.First;
	frmProgress.prgProgress.TotalParts := dmFlowDistributionSupport.adoLinks.RecordCount;
	frmProgress.prgProgress.PartsComplete := 0;
	frmProgress.lblProgress.Caption := 'Reading links';
	frmProgress.Show;
	while not dmFlowDistributionSupport.adoLinks.EOF do
	begin
		USNode := Nodes.FindByID(dmFlowDistributionSupport.adoLinks['USNode']);
		DSNode := Nodes.FindByID(dmFlowDistributionSupport.adoLinks['DSNode']);
		ALink := TLink.Create(
			dmFlowDistributionSupport.adoLinks['LinkID'],
			USNode,
			DSNode,
			IfThen(dmFlowDistributionSupport.adoLinks['DiamWidth'] = 0, 1,
      	dmFlowDistributionSupport.adoLinks['DiamWidth']),
			IfThen(dmFlowDistributionSupport.adoLinks['Length'] = 0, 1,
      	dmFlowDistributionSupport.adoLinks['Length']),
			dmFlowDistributionSupport.adoLinks['USIE'],
			dmFlowDistributionSupport.adoLinks['DSIE']);
		Links.Add(ALink);
		dmFlowDistributionSupport.adoLinks.Next;
		frmProgress.prgProgress.IncPartsByOne;
		Application.ProcessMessages;
	end;
	dmFlowDistributionSupport.adoLinks.Close;
	frmProgress.Hide;

end;

destructor TNetwork.Destroy;
begin
	Links.Free;
end;

end.