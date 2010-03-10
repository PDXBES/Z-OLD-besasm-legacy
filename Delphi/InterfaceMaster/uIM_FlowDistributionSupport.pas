unit uIM_FlowDistributionSupport;

interface

uses SysUtils, Classes, Contnrs, IniFiles, uIM_Tracing, StStrms, uIM_InterfaceFiles;

type
	TLink = class(TInterfacedObject, ITraceable)
	private
		fID: String;
		fUS: String;
		fDS: String;
		fVisited: Boolean;
		fLength: Double;
		fUSIE: Double;
		fDSIE: Double;
	protected
		// ITraceable implementation
		function GetID: String;
		procedure SetID(AValue: String);
		function GetUS: String;
		procedure SetUS(AValue: String);
		function GetDS: String;
		procedure SetDS(AValue: String);
		function GetVisited: Boolean;
		procedure SetVisited(AValue: Boolean);
		function GetSlope: Double;
		function GetVelocity: Double;
		function GetFlowTravelTime: Double;
	public
		// ITraceable implementation
		property ID: String read GetID write SetID;
		property US: String read GetUS write SetUS;
		property DS: String read GetDS write SetDS;
		property Visited: Boolean read GetVisited write SetVisited;
		property Length: Double read fLength write fLength;
		property USIE: Double read fUSIE write fUSIE;
		property DSIE: Double read fDSIE write fDSIE;
		property Slope: Double read GetSlope;
		property Velocity: Double read GetVelocity;
		property FlowTravelTime: Double read GetFlowTravelTime;
	end;

	TNodeFlow = record
		ID: String;
		Flow: Double;
		StartTime: TDateTime;
	end;

	TFlowDistribution = class
	private
		fPath: TFilename;
		fFile: TFileStream;
		fTextStream: TStAnsiTextStream;
		fMultiplier: Double;
		fVelocityFactor: Double;
		fHeaderLines: Integer;
		fDateFormat: String;
		fTimeFormat: String;
		fDelimiters: String;
		fNodeFlows: array of TNodeFlow;
		fStartTime: TDateTime;
		fCurrentTime: TDateTime;
		fFlow: Double;
		fDateTimeFormatSettings: TFormatSettings;
	protected
		function GetFlowAtNode(AIndex: String): Double;
	public
		constructor Create(AFile: TFileName);
		destructor Destroy; override;
		procedure Read;
		procedure DistributeFlow(AFlow: Double);
		procedure Distribute;
		property FlowAtNode[AIndex: String]: Double read GetFlowAtNode;
		property CurrentTime: TDateTime read fCurrentTime write fCurrentTime;
		function IsEOF: Boolean;
	end;

	TAggregateNode = class
	private
		fID: String;
		// List of nodes that add up to the aggregate
	public
		constructor Create;
		function Count: Integer;
		function Add(ANode: TNodeFlow): Integer;
	end;

	TFlowDistributionCombiner = class
	private
		fFlowDistributions: array of TFlowDistribution;
		fCurrentTime: TDateTime;
	protected
		// Adds up the lagged flows at a node from all distributions
		function GetFlowAtNode(AIndex: String): Double;
		function GetFlowDistribution(AIndex: Integer): TFlowDistribution;
	public
		constructor Create;
		function Add(ADistribution: TFlowDistribution): Integer;
		function Count: Integer;
		property Distribution[AIndex: Integer]: TFlowDistribution
			read GetFlowDistribution;
		property FlowAtNode[AIndex: String]: Double read GetFlowAtNode;
		property CurrentTime: TDateTime read fCurrentTime write fCurrentTime;
	end;

	TModelNetwork = class
	private
		{ private declarations }
		fMultiplier: Double;
		fNodeField: String;
		fDistributionField: String;
		fVelocityFactor: Double;
		TempTableList: TStringList;
		TempViewList: TStringList;
		fLinkList: THashedStringList;
		fNetwork: TTraceableNetwork;
		fCombiner: TFlowDistributionCombiner;
		fOutputPath: TFileName;
		fOutputInterface: IIM_InterfaceFile;
	protected
		{ protected declarations }
		function ReadFlow(AIndex: String): Double;
		procedure SetFlow(AIndex: String; AFlow: Double);
	public
		{ public declarations }
		constructor Create(AModelDir: String);
		destructor Destroy; override;
		procedure DistributeFlow(AFlow: Double);
		procedure ResetLinkListVisits;
		procedure WriteInterface;
		property Flow[AIndex: String]: Double read ReadFlow write SetFlow;
		property Multiplier: Double read fMultiplier write fMultiplier;
		property NodeField: String read fNodeField write fNodeField;
		property DistributionField: String read fDistributionField write fDistributionField;
		property VelocityFactor: Double read fVelocityFactor write fVelocityFactor;
		property LinkList: THashedStringList read fLinkList;
		property Combiner: TFlowDistributionCombiner read fCombiner;
		property OutputPath: TFileName read fOutputPath write fOutputPath;
		property OutputInterface: IIM_InterfaceFile read fOutputInterface;
	end;

implementation

uses dmIM_FlowDistributionSupport, ComObj, ADOX_TLB, DB, StStrL, PDXDateUtils,
	uIM_SWMM_XP_InterfaceFiles;

resourcestring
	strDirectSubcatchmentTableName = 'mdl_DirSC_ac';
	strLateralTableName = 'mdl_Laterals_ac';

{ TModelNetwork }

destructor TModelNetwork.Destroy;
var
	i: Integer;
	ACatalog: Catalog;
begin
	// Delete temporary views created for this analysis
	ACatalog := CoCatalog.Create;
	ACatalog.Set_ActiveConnection(dmFlowDistributionSupport.adoConnLinks.ConnectionObject);
	for i := TempViewList.Count - 1 downto 0 do
		ACatalog.Views.Delete(ACatalog.Views.Item[TempViewList[i]]);
	TempViewList.Free;

	// Delete temporary tables created for this analysis
	for i := TempTableList.Count - 1 downto 0 do
		ACatalog.Tables.Delete(ACatalog.Tables.Item[TempTablelist[i]]);
	TempTableList.Free;

	fLinkList.Free;
  
  dmFlowDistributionSupport.adoConnLinks.Connected := False;
  inherited;
end;

procedure TModelNetwork.DistributeFlow(AFlow: Double);
begin

end;

constructor TModelNetwork.Create(AModelDir: String);
var
	LinksMDBLocation: String;
	DSCsMDBLocation: String;
	ACatalog: Catalog;
	i, j: Integer;
	ALink: ITraceable;
	Starts: TTraceableList;
	Ends: TTraceableList;
	StartKeys: TStringList;
	EndKeys: TStringList;
	IniFile: TMemIniFile;
  NumDistributions: Integer;
  DistributionTableName: string;
begin
	LinksMDBLocation := AModelDir + '\links\mdl_Links_ac.mdb';
	DSCsMDBLocation := AModelDir + '\dsc\mdl_DirSC_ac.mdb';
	IniFile := TMemIniFile.Create(AModelDir + '\model.ini');

	// Connect to links
	dmFlowDistributionSupport.adoConnLinks.Connected := False;
	dmFlowDistributionSupport.adoConnLinks.ConnectionString :=
		'Provider=Microsoft.Jet.OLEDB.4.0;'+
		'User ID=Admin;'+
		'Data Source='+LinksMDBLocation+';'+
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
	try
		dmFlowDistributionSupport.adoConnLinks.Connected := True;

		ACatalog := CoCatalog.Create;
		ACatalog.Set_ActiveConnection(dmFlowDistributionSupport.adoConnLinks.ConnectionObject);

		TempViewList := TStringList.Create;
		// Add in view to direct subcatchments
		dmFlowDistributionSupport.adoCommand.CommandText :=
			'SELECT * FROM mdl_DirSC_ac IN ' +
			'''C:\Model_Programs\Interface Master\RedistributionFiles\Model\dsc\mdl_DirSC_ac.mdb''';
		ACatalog.Views.Append(strDirectSubcatchmentTableName, dmFlowDistributionSupport.adoCommand.CommandObject);
		TempViewList.Add(strDirectSubcatchmentTableName);

		// Add in view to laterals
		dmFlowDistributionSupport.adoCommand.CommandText :=
			'SELECT * FROM mdl_Laterals_ac IN ' +
			'''C:\Model_Programs\Interface Master\RedistributionFiles\Model\Laterals\mdl_Laterals_ac.mdb''';
		ACatalog.Views.Append(strLateralTableName, dmFlowDistributionSupport.adoCommand.CommandObject);
		TempViewList.Add(strLateralTableName);

		// Transfer the links internally for tracing
		dmFlowDistributionSupport.adoLinks.First;
		while not dmFlowDistributionSupport.adoLinks.Eof do
		begin
			ALink := TLink.Create;
			ALink.ID := dmFlowDistributionSupport.adoLinks.FieldValues['LinkID'];
			ALink.US := dmFlowDistributionSupport.adoLinks.FieldValues['USNode'];
			ALink.DS := dmFlowDistributionSupport.adoLinks.FieldValues['DSNode'];
			ALink.Visited := False;
			fNetwork.Add(ALink);
			dmFlowDistributionSupport.adoLinks.Next;
		end;

		// Trace each of the distribution networks and add views
		TempTableList := TStringList.Create;
		// Set up starts and ends for a distribution
		StartKeys := TStringList.Create;
		EndKeys := TStringList.Create;
		NumDistributions := IniFile.ReadInteger('main', 'ndistribs', 0);
		for i := 1 to NumDistributions do
		begin
			try
				IniFile.ReadSection('rootlinks' + Format('', [i]), StartKeys);
				for j := 0 to StartKeys.Count - 1 do
					Starts.Add(fNetwork.ItemsByName[StartKeys[j]]);
				IniFile.ReadSection('stoplinks' + Format('', [i]), EndKeys);
				for j := 0 to EndKeys.Count - 1 do
					Ends.Add(fNetwork.ItemsByName[EndKeys[j]]);
			finally
				StartKeys.Free;
				EndKeys.Free;
			end;

			fNetwork.Trace(Starts, Ends, tdUp);

			// Create table of links that belong to the trace
			DistributionTableName := Format('IM_Distribution%.3d', [i]);
			TempTableList.Add(DistributionTableName);
			dmFlowDistributionSupport.adoCommand.CommandText :=
				'CREATE TABLE ' + DistributionTableName +
				' (LinkID Integer)';
			dmFlowDistributionSupport.adoCommand.Execute;
			for j := 0 to fNetwork.CountTraced - 1 do
			begin
				dmFlowDistributionSupport.adoCommand.CommandText :=
					'INSERT INTO ' + DistributionTablename +
					' VALUES (' + fNetwork.Traced[j].ID + ')';
				dmFlowDistributionSupport.adoCommand.Execute;
			end;

		end;

		// Add in view to provide distribution-related sums
		dmFlowDistributionSupport.adoCommand.CommandText :=
			'SELECT mdl_Links_ac.USNode, Sum(mdl_dirSC_ac.AreaFt) AS SumOfAreaFt ' +
			'FROM (mdl_dirSC_ac INNER JOIN (mdl_Laterals_ac INNER JOIN mdl_Links_ac ON mdl_Laterals_ac.LinkID = mdl_Links_ac.LinkID) ON mdl_dirSC_ac.LateralIDSan = mdl_Laterals_ac.LateralID) INNER JOIN IM_TraceNodes001 ON mdl_Links_ac.USNode = IM_TraceNodes001.NodeID' +
			'GROUP BY mdl_Links_ac.USNode;';

		ACatalog.Views.Append('FlowDistributionByNode', dmFlowDistributionSupport.adoCommand.CommandObject);

	except on E: EOleException do
	end;

	Starts.Free;
	Ends.Free;
	StartKeys.Free;
	EndKeys.Free;
	IniFile.Free;
end;

function TModelNetwork.ReadFlow(AIndex: String): Double;
begin

end;

procedure TModelNetwork.ResetLinkListVisits;
var
	i: Integer;
begin
	for i := 0 to LinkList.Count - 1 do
		(LinkList.Objects[i] as TLink).Visited := False;
end;

procedure TModelNetwork.SetFlow(AIndex: String; AFlow: Double);
begin

end;

procedure TModelNetwork.WriteInterface;
var
	OutputHeader: IIM_InterfaceHeader;
begin
	fOutputInterface := T_SWMM_XP_StandardInterfaceFile.Create(fOutputPath,
		fmCreate);
	OutputHeader := fOutputInterface.GetHeader;
	OutputHeader.HeaderValue[IM_IFHDR_TITLE1] := '';
	OutputHeader.HeaderValue[IM_IFHDR_TITLE2] := '';
	OutputHeader.HeaderValue[IM_IFHDR_TITLE3] := '';
	OutputHeader.HeaderValue[IM_IFHDR_TITLE4] := '';
	OutputHeader.HeaderValue[IM_IFHDR_SOURCE] := 'IM_MultiCombine';
	OutputHeader.HeaderValue[IM_IFHDR_AREA] := 0.001;
	OutputHeader.HeaderValue[IM_IFHDR_MULTIPLIER] := 1.00;
	EarliestSourceTime := GetEarliestSourceTime;
	OutputHeader.HeaderValue[IM_IFHDR_STARTDATE] := EarliestSourceTime;
	for m := 0 to Length(fDestSpecs)-1 do
	begin
		OutputHeader.AddIndexedHeaderValue(fDestSpecs[m].ID);
	end;
	OutputHeader.WriteHeader;


	while not fCombiner.Distribution[0].IsEOF do
	begin
		fOutputInterface.WriteInterface;
  end;


	fOutputInterface := nil;
end;

{ TLink }

function TLink.GetDS: String;
begin
	Result := fDS;
end;

function TLink.GetID: String;
begin
	Result := fID;
end;

function TLink.GetUS: String;
begin
	Result := fUS;
end;

function TLink.GetVisited: Boolean;
begin
	Result := fVisited;
end;

procedure TLink.SetDS(AValue: String);
begin
	if fDS <> AValue then
		fDS := AValue;
end;

procedure TLink.SetID(AValue: String);
begin
	if fID <> AValue then
		fID := AValue;
end;

procedure TLink.SetUS(AValue: String);
begin
	if fUS <> AValue then
		fUS := AValue;
end;

procedure TLink.SetVisited(AValue: Boolean);
begin
	if fVisited <> AValue then
		fVisited := AValue;
end;

{ TFlowDistribution }

constructor TFlowDistribution.Create(AFile: TFileName);
begin
	fPath := AFile;
	fFile := TFileStream.Create(fPath, fmOpenRead or fmShareDenyWrite);
	fTextStream := TStAnsiTextStream.Create(fFile);
end;

destructor TFlowDistribution.Destroy;
begin
	fTextStream.Free;
	fFile.Free;
	inherited;
end;

procedure TFlowDistribution.Distribute;
begin
	// Rifle through all nodes for trace and determine amount of flow
end;

procedure TFlowDistribution.DistributeFlow(AFlow: Double);
begin

end;

function TFlowDistribution.GetFlowAtNode(AIndex: String): Double;
begin

end;

procedure TFlowDistribution.Read;
var
	Tokens: TStringList;
	CurrentLine: String;
	ReadDate: TDateTime;
	ReadTime: TDateTime;
begin
	Tokens := TStringList.Create;
	try
		if fTextStream.AtEndOfStream then
		begin
			fCurrentTime := MaxDateTime;
			Exit;
		end;
		CurrentLine := fTextStream.ReadLine;
		ExtractTokensL(CurrentLine, ' ', '''', False, Tokens);
		ReadDate := StrToDate(Tokens[0], fDateTimeFormatSettings);
		ReadTime := StrToTime(Tokens[1], fDateTimeFormatSettings);
		fCurrentTime := ReadDate + ReadTime;
		fFlow := StrToFloat(Tokens[2]);

		{ TODO -oAMM -cDistributeFlows : Distribute flows from stream to nodes }
		Distribute;
	finally
		Tokens.Free;
	end;
end;

end.
