unit uEMGAATSModelCommands;

interface

uses Windows, SysUtils, uEMGAATSModel, Contnrs, Classes, StdCtrls, RzPrgres;

type
  ///<summary>Used by fBuild to abort a command in progress</summary>
  TEMGAATSCommandAbortSignal = class
  public
    Abort: Boolean;
  end;

  IEMGAATSModelCommand = interface
    ['{D5290667-C875-4A33-A10C-68D6A61F0236}']
    function GetName: String;
    procedure SetName(Value: String);
    function GetDescription: String;
    procedure SetDescription(Value: String);
    function GetCommandCount: Integer;
    function GetEnabledCommandCount: Integer;
    function GetEnabled: Boolean;
    procedure SetEnabled(Value: Boolean);
    procedure Execute;
    property Name: String read GetName write SetName;
    property Description: String read GetDescription write SetDescription;
    property CommandCount: Integer read GetCommandCount;
    property EnabledCommandCount: Integer read GetEnabledCommandCount;
    property Enabled: Boolean read GetEnabled write SetEnabled;
  end;

  TEMGAATSModelCommandList = class
  private
    fList: TInterfaceList;
    function Get(Index: Integer): IEMGAATSModelCommand;
    function GetCapacity: Integer;
    function GetCount: Integer;
    procedure Put(Index: Integer; const Value: IEMGAATSModelCommand);
    procedure SetCapacity(const Value: Integer);
    procedure SetCount(const Value: Integer);
    function GetCommandCount: Integer;
    function GetEnabledCommandCount: Integer;
  public
    constructor Create;
    destructor Destroy; override;
    procedure Clear;
    procedure Delete(Index: Integer);
    procedure Exchange(Index1, Index2: Integer);
    function First: IEMGAATSModelCommand;
//    function GetEnumerator: TInterfaceListEnumerator;
    function IndexOf(const Item: IEMGAATSModelCommand): Integer;
    function Add(const Item: IEMGAATSModelCommand): Integer;
    procedure Insert(Index: Integer; const Item: IEMGAATSModelCommand);
    function Last: IEMGAATSModelCommand;
    function Remove(const Item: IEMGAATSModelCommand): Integer;
    procedure Lock;
    procedure Unlock;
    property Capacity: Integer read GetCapacity write SetCapacity;
    property Count: Integer read GetCount write SetCount;
    property Items[Index: Integer]: IEMGAATSModelCommand read Get write Put; default;
    property CommandCount: Integer read GetCommandCount;
    property EnabledCommandCount: Integer read GetEnabledCommandCount;
    procedure Execute(AbortSignal: TEMGAATSCommandAbortSignal);
  end;

  TSimpleCommand = class(TInterfacedObject, IEMGAATSModelCommand)
  private
    fName: String;
    fDescription: String;
    fModel: TEMGAATSModel;
    fProgressLabel: TLabel;
    fProgressBar: TRzProgressBar;
    fEnabled: Boolean;
    fAbortSignal: TEMGAATSCommandAbortSignal;
    function GetName: string;
    procedure SetName(Value: string);
    function GetDescription: String;
    procedure SetDescription(Value: String);
    function GetCommandCount: Integer;
    function GetEnabledCommandCount: Integer;
    function GetEnabled: Boolean;
    procedure SetEnabled(Value: Boolean);
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); virtual;
    procedure Execute; virtual;
    property Name: String read GetName write SetName;
    property Description: String read GetDescription write SetDescription;
    property CommandCount: Integer read GetCommandCount;
    property Enabled: Boolean read GetEnabled write SetEnabled;
    property EnabledCommandCount: Integer read GetEnabledCommandCount;
  end;

  TMacroCommand = class(TInterfacedObject, IEMGAATSModelCommand)
  private
    fCommandList: TEMGAATSModelCommandList;
    fName: String;
    fDescription: String;
    fModel: TEMGAATSModel;
    fProgressLabel: TLabel;
    fProgressBar: TRzProgressBar;
    fEnabled: Boolean;
    fAbortSignal: TEMGAATSCommandAbortSignal;
    function GetName: string;
    procedure SetName(Value: string);
    function GetDescription: String;
    procedure SetDescription(Value: String);
    function GetCommandCount: Integer;
    function GetEnabled: Boolean;
    procedure SetEnabled(Value: Boolean);
    function GetEnabledCommandCount: Integer;
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); virtual;
    destructor Destroy; override;
    procedure Add(ACommand: IEMGAATSModelCommand);
    procedure Execute; virtual;
    property Name: String read GetName write SetName;
    property Description: String read GetDescription write SetDescription;
    property CommandCount: Integer read GetCommandCount;
    property Enabled: Boolean read GetEnabled write SetEnabled;
    property EnabledCommandCount: Integer read GetEnabledCommandCount;
  end;

  // Model building

  TBuildNewModelCommand = class(TMacroCommand)
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  TBuildOverExistingModel = class(TMacroCommand)
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
  end;

  TBuildEmptyModelCommand = class(TMacroCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
  end;

  TClearModelDirectoryCommand = class(TSimpleCommand)
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  TConfigureModelForBuildingCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  TBuildNetworkCommand = class(TMacroCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

    TTraceNetworkCommand = class(TSimpleCommand)
    public
      constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
      procedure Execute; override;
    end;

    TCreateMappablePipesCommand = class(TSimpleCommand)
    public
      constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
        AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
      procedure Execute; override;
    end;

    TAssembleSpecialLinksCommand = class(TSimpleCommand)
    public
      constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
        AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
      procedure Execute; override;
    end;

  TBuildDirectSubcatchmentsCommand = class(TMacroCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

    TTraceParcelsCommand = class(TSimpleCommand)
    public
      constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
        AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
      procedure Execute; override;
    end;

    TInitDSCICsCommand = class(TSimpleCommand)
    public
      constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
        AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
      procedure Execute; override;
    end;

    TInitFutureDSCHydrologyCommand = class(TSimpleCommand)
    public
      constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
        AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
      procedure Execute; override;
    end;

  TBuildSurfaceSubcatchmentsCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

    TBuildStudyAreaBoundaryCommand = class(TSimpleCommand)
    public
      constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
        AProgressBar: TRzProgressBar = nil;
        AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
      procedure Execute; override;
    end;

  TCalculateHydrologyCommand = class(TMacroCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
  end;

    TInitializeICsCommand = class(TSimpleCommand)
    public
      constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
        AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
      procedure Execute; override;
    end;

    TApplyVirtualGagesCommand = class(TSimpleCommand)
    public
      constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
        AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
      procedure Execute; override;
    end;

    TPerformGISHydroQueriesCommand = class(TSimpleCommand)
    public
      constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
        AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
      procedure Execute; override;
    end;

    TCheckForDuplicateDSCsCommand = class(TSimpleCommand)
    public
      constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
        AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
      procedure Execute; override;
    end;

    TCheckForExcessiveICAreasCommand = class(TSimpleCommand)
    public
      constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
        AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
      procedure Execute; override;
    end;

    TRemoveRunoffFilesCommand = class(TSimpleCommand)
    public
      constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
        AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
      procedure Execute; override;
    end;

  TRunAllStormwaterControlsCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
    AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  TRunStreetStormwaterControlsCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
    AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  TMergeStormwaterControlsCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
    AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  TBuildAllStormwaterControlsCommand = class(TMacroCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
    AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
  end;

  TBuildStreetStormwaterControlsCommand = class(TMacroCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
    AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
  end;

  TCreateModelConfigCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  TCreateStandardDirectoriesCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  TCopyMasterTemplateCommand = class(TMacroCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
  end;

  TCopyMasterDatabasesCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  TCopyMasterICAltDatabasesCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  TCreateEmptyModelDataCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  TCreateDataAccessFileCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  TOpenMasterModelDataCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  TRelinkModelDataCommand  = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  TDeployRunoffCommand = class(TMacroCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

    TPrepareRunoffFileCommand = class(TSimpleCommand)
    public
      constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
        AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
      procedure Execute; override;
    end;

    TCreateRunoffBatchFileCommand = class(TSimpleCommand)
    public
      constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
        AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
      procedure Execute; override;
    end;

    TCreateReconciliationSpreadsheetCommand = class(TSimpleCommand)
    public
      constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
        AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
      procedure Execute; override;
    end;

  TDeployEngineFileCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  // Quality control
  TCreateQCWorkspacesCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  // Standard copies
  TCopyStandardMapInfoWorkspacesCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  TCopyStandardMXDsCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

  TCalculateHydraulicsCommand = class(TSimpleCommand)
  public
    constructor Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
      AProgressBar: TRzProgressBar = nil;
      AbortSignal: TEMGAATSCommandAbortSignal = nil); override;
    procedure Execute; override;
  end;

implementation

uses uEMGAATSSystemConfig, GlobalConstants, uUtilities, dmTracer,
  uEMGWorkbenchManager, uMSAccessManager, uEMGAATSTypes, StrUtils, StStrL,
  ADOX_TLB, StStrms, dmHydroStats, CodeSiteLogging, fMain, ADODB, Forms, DateUtils,
  uMapInfoManager, uQCWorkspaceManager, Math, fBuild;

{ TSimpleCommand }

constructor TSimpleCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  fName := 'Default command name';
  fDescription := 'Default command description';
  fProgressLabel := AProgressLabel;
  fProgressBar := AProgressBar;
  fModel := AModel;
  fEnabled := True;
  fAbortSignal := AbortSignal;
end;

procedure TSimpleCommand.Execute;
begin
  if fEnabled and Assigned(fProgressLabel) then
  begin
    fProgressLabel.Caption := fDescription;
    fProgressLabel.Update;
    fModel.AddError(TEMGAATSError.Create('Executing: ' + fName, eetRunCommand));
  end;
  if fEnabled and Assigned(fProgressBar) then
  begin
    fProgressBar.IncPartsByOne;
    fProgressBar.Update;
  end;
end;

function TSimpleCommand.GetCommandCount: Integer;
begin
  Result := 1
end;

function TSimpleCommand.GetDescription: String;
begin
  Result := fDescription;
end;

function TSimpleCommand.GetEnabled: Boolean;
begin
  Result := fEnabled;
end;

function TSimpleCommand.GetEnabledCommandCount: Integer;
begin
  if fEnabled then
    Result := 1
  else
    Result := 0;
end;

function TSimpleCommand.GetName: string;
begin
  Result := fName;
end;

procedure TSimpleCommand.SetDescription(Value: String);
begin
  if fDescription <> Value then
    fDescription := Value;
end;

procedure TSimpleCommand.SetEnabled(Value: Boolean);
begin
  if fEnabled <> Value then
    fEnabled := Value;
end;

procedure TSimpleCommand.SetName(Value: string);
begin
  if fName <> Value then
    fName := Value;
end;

{ TMacroCommand }

procedure TMacroCommand.Add(ACommand: IEMGAATSModelCommand);
begin
  fCommandList.Add(ACommand);
end;

constructor TMacroCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  fCommandList := TEMGAATSModelCommandList.Create;
  fAbortSignal := AbortSignal;
  fName := 'Default macro command name';
  fDescription := 'Default macro description';
  fProgressLabel := AProgressLabel;
  fProgressBar := AProgressBar;
  fModel := AModel;
  fEnabled := True;
end;

destructor TMacroCommand.Destroy;
begin
  fCommandList.Free;
  inherited;
end;

procedure TMacroCommand.Execute;
var
  i: Integer;
  CurrentCommand: IEMGAATSModelCommand;
begin
  CodeSite.EnterMethod('TMacroCommand.Execute: ' + fName);
  if fEnabled then
    fCommandList.Execute(fAbortSignal);
  CodeSite.ExitMethod('TMacroCommand.Execute: ' + fName);
end;

function TMacroCommand.GetCommandCount: Integer;
var
  i: Integer;
begin
  Result := 0;
  for i := 0 to fCommandList.Count - 1 do
    Inc(Result, fCommandList[i].CommandCount);
end;

function TMacroCommand.GetDescription: String;
begin
  Result := fDescription;
end;

function TMacroCommand.GetEnabled: Boolean;
begin
  Result := fEnabled;
end;

function TMacroCommand.GetEnabledCommandCount: Integer;
var
  i: Integer;
begin
  Result := 0;
  if fEnabled then
    for i := 0 to fCommandList.Count - 1 do
      Inc(Result, fCommandList[i].EnabledCommandCount);
end;

function TMacroCommand.GetName: string;
begin
  Result := fName;
end;

procedure TMacroCommand.SetDescription(Value: String);
begin
  if fDescription <> Value then
    fDescription := Value;
end;

procedure TMacroCommand.SetEnabled(Value: Boolean);
begin
  if fEnabled <> Value then
    fEnabled := Value;
end;

procedure TMacroCommand.SetName(Value: string);
begin
  fName := Value;
end;

{ TBuildEmptyModelCommand }

constructor TBuildEmptyModelCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Build empty model';
  fDescription := 'Building empty model';
  Add(TClearModelDirectoryCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
// Interferes with setting of timeframe; just rely on TEMGAATSModel.Create
//  Add(TCreateModelConfigCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCreateStandardDirectoriesCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCopyMasterTemplateCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCreateEmptyModelDataCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCreateDataAccessFileCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TOpenMasterModelDataCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
//  CheckModelLocation;
  Add(TRelinkModelDataCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TConfigureModelForBuildingCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
end;

{ TConfigureModelForBuildingCommand }

constructor TConfigureModelForBuildingCommand.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel = nil; AProgressBar: TRzProgressBar = nil;
  AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Configure model for building';
  fDescription := 'Configuring model for building';
  fModel := AModel;
  fProgressLabel := AProgressLabel;
end;

procedure TConfigureModelForBuildingCommand.Execute;
begin
  CodeSite.EnterMethod('TConfigureModelForBuildingCommand.Execute');
  inherited;
  if not fEnabled then
    Exit;
  // Basic treeverse parameters
  if fModel.Config.TraceFileName = '' then
    fModel.Config.TraceFileName := IncludeTrailingPathDelimiter(fModel.Config.Path) +
      'mdltrace.txt';

  fModel.Config.TraceSourceDatabase := IncludeTrailingPathDelimiter(SystemConfig.MDBsByName['root']) +
    SystemConfig.MDBsByName['TraceSourceDatabase'];

  if fModel.Config.TraceSourceTable = '' then
    if fModel.Config.TraceStormwater then
      fModel.Config.TraceSourceTable := 'mst_links_allex'
    else
      fModel.Config.TraceSourceTable := 'mst_links_existing';
  if fModel.Config.LinkField = '' then
    fModel.Config.LinkField := 'MLinkID';
  if fModel.Config.USNodeField = '' then
    fModel.Config.USNodeField := 'USNode';
  if fModel.Config.DSNodeField = '' then
    fModel.Config.DSNodeField := 'DSNode';
  if fModel.Config.ReachField = '' then
    fModel.Config.ReachField := 'LinkReach';
  if fModel.Config.ElementField = '' then
    fModel.Config.ElementField := 'ReachElement';
  if fModel.Config.DebugFileName = '' then
    fModel.Config.DebugFileName := IncludeTrailingPathDelimiter(GetApplicationUserConfigPath) +
      'tracedebug.txt';
  if fModel.Config.LinkExistingTable = '' then
    fModel.Config.LinkExistingTable := 'mst_links_existing';
  if fModel.Config.LinkFutureTable = '' then
    fModel.Config.LinkFutureTable := 'mst_links_future';

  fModel.Config.Update;
  CodeSite.ExitMethod('TConfigureModelForBuildingCommand.Execute');
end;

{ TBuildNetworkCommand }

constructor TBuildNetworkCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Build network';
  fDescription := 'Building network';
  Add(TTraceNetworkCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCreateMappablePipesCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TAssembleSpecialLinksCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
end;

procedure TBuildNetworkCommand.Execute;
begin
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TBuildNetworkCommand.Execute');
  if fModel.Config.HasStandardDirectories and fModel.Config.HasSkeletonModel then
  begin
    fModel.RemoveNetwork;
    try
      inherited;
      fModel.Config.HasNetwork := True;
      fModel.Config.Update;
    except
      fModel.AddError(TEMGAATSError.Create('Network build failed', eetError));
    end;
  end
  else
  begin
    if not fModel.Config.HasStandardDirectories then
      fModel.AddError(TEMGAATSError.Create('Cannot build network. ' +
        'Standard directories not present.', eetError));
    if not fModel.Config.HasSkeletonModel then
      fModel.AddError(TEMGAATSError.Create('Cannot build network. ' +
        'Skeleton model not present.', eetError))
  end;
  CodeSite.ExitMethod('TBuildNetworkCommand.Execute');
end;

{ TTraceNetworkCommand }

constructor TTraceNetworkCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Trace network';
  fDescription := 'Tracing network';
end;

procedure TTraceNetworkCommand.Execute;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TTraceNetworkCommand.Execute');
  datmodTracer.TraceNetwork(fModel.Config, fModel);
  if Assigned(frmBuild) then
    PostMessage(frmBuild.Handle, EMG_UPDATE_STATS, datmodTracer.NumTracedLinks, 0);
  CodeSite.ExitMethod('TTraceNetworkCommand.Execute');
end;

{ TCreateMappablePipesCommand }

constructor TCreateMappablePipesCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Create mappable pipes';
  fDescription := 'Creating mappable pipes';
end;

procedure TCreateMappablePipesCommand.Execute;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TCreateMappablePipesCommand.Execute');
  EMGWorkbenchManager.CreateMappablePipes;
  CodeSite.ExitMethod('TCreateMappablePipesCommand.Execute');
end;

{ TAssembleSpecialLinksCommand }

constructor TAssembleSpecialLinksCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Assemble special links';
  fDescription := 'Assembling special links';
end;

procedure TAssembleSpecialLinksCommand.Execute;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TAssembleSpecialLinksCommand.Execute');
  MSAccessManager.OpenDatabase(fModel.Config.MasterMDBFilesByName['ModelAssemble']);
  EMGWorkbenchManager.GetSpecialLinksData;
  CodeSite.ExitMethod('TAssembleSpecialLinksCommand.Execute');
end;

{ TBuildDirectSubcatchmentsCommand }

constructor TBuildDirectSubcatchmentsCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Build direct subcatchments';
  fDescription := 'Building direct subcatchments';
  Add(TTraceParcelsCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TInitDSCICsCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TInitFutureDSCHydrologyCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
end;

procedure TBuildDirectSubcatchmentsCommand.Execute;
begin

  if not fEnabled then
  Exit;

  CodeSite.EnterMethod('TBuildDirectSubcatchmentsCommand.Execute');
  if fModel.Config.HasNetwork then
  begin
    inherited;
    fModel.Config.HasDirectSubcatchments := True;
    fModel.Config.Update;
  end
  else
    fModel.AddError(TEMGAATSError.Create('Cannot build direct subcatchments. ' +
      'Network not available.', eetError));
  CodeSite.ExitMethod('TBuildDirectSubcatchmentsCommand.Execute');
end;

{ TTraceParcelsCommand }

constructor TTraceParcelsCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Trace parcels';
  fDescription := 'Tracing parcels';
end;

procedure TTraceParcelsCommand.Execute;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TTraceParcelsCommand.Execute');
  EMGWorkbenchManager.TraceParcels;
  CodeSite.ExitMethod('TTraceParcelsCommand.Execute');
end;

{ TInitDSCICsCommand }

constructor TInitDSCICsCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Initialize inflow controls for direct subcatchments';
  fDescription := 'Initializing inflow controls for direct subcatchments'
end;

procedure TInitDSCICsCommand.Execute;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TInitDSCICsCommand.Execute');
  MSAccessManager.OpenDatabase(fModel.Config.MasterMDBFilesByName['HydInitDSC']);
  MSAccessManager.Run('executequerytable', ['listexecuteInitDSC', 'Block' , 'TDSC_ALL' , 0]);
  CodeSite.ExitMethod('TInitDSCICsCommand.Execute');
end;

{ TInitFutureDSCHydrologyCommand }

constructor TInitFutureDSCHydrologyCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Initialize future DSC hHydrology';
  fDescription := 'Initializing future DSC hydrology';
end;

procedure TInitFutureDSCHydrologyCommand.Execute;
begin
  inherited;
  if not fEnabled then
    Exit;

  if fModel.Config.TimeFrame <> tfFuture then
    Exit;

  CodeSite.EnterMethod('TInitFutureDSCHydrologyCommand.Execute');
  MSAccessManager.Run('setFBfactoredbaseflow', [0.5]);
  MSAccessManager.Run('executequerytable', ['listexecuteInitDSC', 'Block' , 'TDSC_FB' , 0]);
  CodeSite.ExitMethod('TInitFutureDSCHydrologyCommand.Execute');
end;

{ TBuildSurfaceSubcatchmentsCommand }

constructor TBuildSurfaceSubcatchmentsCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Build surface subcatchments';
  fDescription := 'Building surface subcatchments';
end;

procedure TBuildSurfaceSubcatchmentsCommand.Execute;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TBuildSurfaceSubcatchmentsCommand.Execute');
  if fModel.Config.HasNetwork then
  begin
    EMGWorkbenchManager.TraceSurfaceSubcatchments;
    EMGWorkbenchManager.CreateSurfaceSubcatchmentPointers;

    fModel.Config.HasSurfaceSubcatchments := True;
    fModel.Config.Update;
  end
  else
    fModel.AddError(TEMGAATSError.Create('Could not build surface subcatchments. ' +
      'Network not available.', eetError));
  CodeSite.ExitMethod('TBuildSurfaceSubcatchmentsCommand.Execute');
end;

{ TCalculateHydrologyCommand }

constructor TCalculateHydrologyCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Calculate hydrology';
  fDescription := 'Calculating hydrology';
  Add(TInitializeICsCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TApplyVirtualGagesCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TPerformGISHydroQueriesCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCheckForDuplicateDSCsCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCheckForExcessiveICAreasCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TRemoveRunoffFilesCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
end;

{ TInitializeICsCommand }

constructor TInitializeICsCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Initialize ICs';
  fDescription := 'Initializing inflow controls';
end;

procedure TInitializeICsCommand.Execute;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TInitializeICsCommand.Execute');
  MSAccessManager.OpenDatabase(fModel.Config.MasterMDBFilesByName['HydInitDSC']);
  MSAccessManager.Run('ExecuteQueryTable', ['listexecuteInitDSC', 'Block', 'INIT_HYD', 0]);
  CodeSite.ExitMethod('TInitializeICsCommand.Execute');
end;

{ TApplyVirtualGagesCommand }

constructor TApplyVirtualGagesCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Apply virtual gages';
  fDescription := 'Applying virtual gages';
end;

procedure TApplyVirtualGagesCommand.Execute;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TApplyVirtualGagesCommand.Execute');
  try
    EMGWorkbenchManager.RunCommand('Add Column ' + #34 + 'mdl_nodes' + #34 +
      ' (GageID ) From mst_Quartersections Set To QS Where contains');
    EMGWorkbenchManager.RunCommand('commit table mdl_nodes');
  except on E: Exception do
    fModel.AddError(TEMGAATSError.Create('Error applying virtual gages. ' +
      'Error: ' + E.Message, eetError));
  end;
  CodeSite.ExitMethod('TApplyVirtualGagesCommand.Execute');
end;

{ TPerformGISHydroQueriesCommand }

constructor TPerformGISHydroQueriesCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Perform GIS hydrology queries for DSCs and SSCs';
  fDescription := 'Performing GIS hydrology queries for DSCs and SSCs';
end;

procedure TPerformGISHydroQueriesCommand.Execute;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TPerformGISHydroQueriesCommand.Execute');
  try
    EMGWorkbenchManager.RelateDSCtoSurfSC;
  except on E: Exception do
    fModel.AddError(TEMGAATSError.Create('Could not perform GIS Hydro Queries. ' +
      'Error: ' + E.Message, eetError));
  end;
  CodeSite.ExitMethod('TPerformGISHydroQueriesCommand.Execute');
end;

{ TCheckForDuplicateDSCsCommand }

constructor TCheckForDuplicateDSCsCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Check for duplicate DSCs';
  fDescription := 'Checking for duplicate DSCs';
end;

procedure TCheckForDuplicateDSCsCommand.Execute;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TCheckForDuplicateDSCsCommand.Execute');
  try
    EMGWorkbenchManager.CheckForDuplicateDSCs;
  except on E: Exception do
    fModel.AddError(TEMGAATSError.Create('Could not perform Check for Duplicate DSCs. ' +
      'Error: ' + E.Message, eetError));
  end;
  CodeSite.ExitMethod('TCheckForDuplicateDSCsCommand.Execute');
end;

{ TCheckForExcessiveICAreasCommand }

constructor TCheckForExcessiveICAreasCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Check for excessing IC areas';
  fDescription := 'Checking for excessing IC areas';
end;

procedure TCheckForExcessiveICAreasCommand.Execute;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TCheckForExcessiveICAreasCommand.Execute');
  try
    EMGWorkbenchManager.CheckForExcessiveICAreas;;
  except on E: Exception do
    fModel.AddError(TEMGAATSError.Create('Could not perform Check for Excessive IC areas. ' +
      'Error: ' + E.Message, eetError));
  end;
  CodeSite.ExitMethod('TCheckForExcessiveICAreasCommand.Execute');
end;

{ TCreateModelConfigCommand }

constructor TCreateModelConfigCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Create model config file';
  fDescription := 'Creating model configuration file';
end;

procedure TCreateModelConfigCommand.Execute;
begin
  inherited;
  if not fEnabled then
    Exit;


  CodeSite.EnterMethod('TCreateModelConfigCommand.Execute');
  // Copy in the following sections from the SystemConfig
  //   Admin, MasterFiles, MDBs, Code, StandardDirectories, DataAccess

  // Copy in standard sections from the ModelTemplate
  fModel.Config.CopyConfigFromSystem;
  fModel.Config.CopyConfigFromModelTemplate;
  fModel.Config.Update;

  // Copy in the following sections from the UserConfig
  //   Simulation
  CodeSite.ExitMethod('TCreateModelConfigCommand.Execute');
end;

{ TCreateStandardDirectoriesCommand }

constructor TCreateStandardDirectoriesCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Create standard directories';
  fDescription := 'Creating standard directories';
end;

procedure TCreateStandardDirectoriesCommand.Execute;
var
  i: Integer;
  PathToCreate: String;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TCreateStandardDirectoriesCommand.Execute');
  fModel.Config.HasStandardDirectories := False;
  fModel.Config.Update;
  // Standard directories are specified in the EMGAATS SystemConfig file
  for i := 0 to SystemConfig.StandardDirectoryCount - 1 do
  begin
    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    PathToCreate := IncludeTrailingPathDelimiter(fModel.Path) +
      SystemConfig.StandardDirectoriesID[i];
    if not DirectoryExists(PathToCreate) then
      if not CreateDir(PathToCreate) then
      begin
        fModel.AddError(TEMGAATSError.Create('Could not create standard directory ' +
          PathToCreate, eetError));
      end;
  end;
  fModel.Config.HasStandardDirectories := True;
  fModel.Config.Update;
  CodeSite.ExitMethod('TCreateStandardDirectoriesCommand.Execute');
end;

{ TCopyMasterTemplateCommand }

constructor TCopyMasterTemplateCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Copy master template to model';
  fDescription := 'Copying master template to model';
  Add(TCopyMasterDatabasesCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCopyMasterICAltDatabasesCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
end;

{ TCreateEmptyModelDataCommand }

constructor TCreateEmptyModelDataCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Create skeleton model';
  fDescription := 'Creating skeleton model';
end;

procedure TCreateEmptyModelDataCommand.Execute;
var
  ICDirectoryPath: String;
  CommandText: String;
  MDBsPath: String;
  ModelPath: String;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TCreateEmptyModelDataCommand.Execute');
  fModel.Config.HasSkeletonModel := False;
  fModel.Config.Update;

  try
    try
      EMGWorkBenchManager.CreateModelTables;
    except on E: Exception do
      begin
        fModel.AddError(TEMGAATSError.Create('Could not create base mapinfo tables. ' +
          'MapInfo Error: ' + E.Message, eetError));
        raise;
      end;
    end;

    // Create IC Tables (Delete first if they exist!)
    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    ICDirectoryPath := IncludeTrailingPathDelimiter(fModel.Path) + 'ic\';
    fModel.DeleteModelMDB(ICDirectoryPath + 'mdl_ic_discoveg_ac.mdb');
    fModel.DeleteModelMDB(ICDirectoryPath + 'mdl_ic_drywell_ac.mdb');
    fModel.DeleteModelMDB(ICDirectoryPath + 'mdl_ic_grnroof_ac.mdb');
    fModel.DeleteModelMDB(ICDirectoryPath + 'mdl_ic_infilt_ac.mdb');
    fModel.DeleteModelMDB(ICDirectoryPath + 'mdl_ic_swplnt_ac.mdb');
    fModel.DeleteModelMDB(ICDirectoryPath + 'mdl_ic_store_ac.mdb');
    fModel.DeleteModelMDB(ICDirectoryPath + 'mdl_ic_ngtoredir_ac.mdb');
    fModel.DeleteModelMDB(IncludeTrailingPathDelimiter(fModel.Path) + 'mdbs\DataAccess.mdb');

    // DiscoVeg
    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    try
      MSAccessManager.CreateDatabase(ICDirectoryPath + 'mdl_ic_discoveg_ac.mdb');
      CommandText := 'CREATE TABLE mdl_ic_discoveg_ac (' +
        'ICID Autoincrement PRIMARY KEY,' +
        'ParcelID Integer,' +
        'DivideID Integer,' +
        'DSCID Integer,' +
        'RoofRPark Text(1),' +
        'AssumeKey Text(4),' +
        'TimeFrame Text(2),' +
        'ApplyAreaTF Text(2),' +
        'SqFt Integer,' +
        'Effectiveness Float,' +
        'Comment Text(16),' +
        'AppendDate Text(8),' +
        'IsActive Logical' +
        ')';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxParcelID ON mdl_ic_discoveg_ac (ParcelID)';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxDivideID ON mdl_ic_discoveg_ac (DivideID)';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxDSCID ON mdl_ic_discoveg_ac (DSCID)';
      MSAccessManager.SendCommand(CommandText);
    except on E: Exception do
      begin
        fModel.AddError(TEMGAATSError.Create('Error in creating mdl_ic_discoveg_ac table. ' +
          'Access error: ' + E.Message, eetError));
        raise;
      end;
    end;

    // DryWell
    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    try
      MSAccessManager.CreateDatabase(ICDirectoryPath + 'mdl_ic_drywell_ac.mdb');
      CommandText := 'CREATE TABLE mdl_ic_drywell_ac (' +
        'ICID Autoincrement PRIMARY KEY,' +
        'ParcelID Integer,' +
        'DivideID Integer,' +
        'DSCID Integer,' +
        'RoofRPark Text(1),' +
        'AssumeKey Text(4),' +
        'TimeFrame Text(2),' +
        'ApplyAreaTF Text(2),' +
        'SqFt Integer,' +
        'Comment Text(16),' +
        'AppendDate Text(8),' +
        'IsActive Logical' +
        ')';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxParcelID ON mdl_ic_drywell_ac (ParcelID)';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxDivideID ON mdl_ic_drywell_ac (DivideID)';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxDSCID ON mdl_ic_drywell_ac (DSCID)';
      MSAccessManager.SendCommand(CommandText);
    except on E: Exception do
      begin
        fModel.AddError(TEMGAATSError.Create('Error in creating mdl_ic_drywell_ac table. ' +
          'Access error: ' + E.Message, eetError));
        raise;
      end;
    end;

    // GreenRoof
    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    try
      MSAccessManager.CreateDatabase(ICDirectoryPath + 'mdl_ic_grnroof_ac.mdb');
      CommandText := 'CREATE TABLE mdl_ic_grnroof_ac (' +
        'ICID Autoincrement PRIMARY KEY,' +
        'ParcelID Integer,' +
        'DivideID Integer,' +
        'DSCID Integer,' +
        'RoofRPark Text(1),' +
        'AssumeKey Text(4),' +
        'TimeFrame Text(2),' +
        'ApplyAreaTF Text(2),' +
        'SqFt Integer,' +
        'Comment Text(16),' +
        'AppendDate Text(8),' +
        'IsActive Logical' +
        ')';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxParcelID ON mdl_ic_grnroof_ac (ParcelID)';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxDivideID ON mdl_ic_grnroof_ac (DivideID)';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxDSCID ON mdl_ic_grnroof_ac (DSCID)';
      MSAccessManager.SendCommand(CommandText);
    except on E: Exception do
      begin
        fModel.AddError(TEMGAATSError.Create('Error in creating mdl_ic_grnroof_ac table. ' +
          'Access error: ' + E.Message, eetError));
        raise;
      end;
    end;

    // Infiltration
    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    try
      MSAccessManager.CreateDatabase(ICDirectoryPath + 'mdl_ic_infilt_ac.mdb');
      CommandText := 'CREATE TABLE mdl_ic_infilt_ac (' +
        'ICID Autoincrement PRIMARY KEY,' +
        'ParcelID Integer,' +
        'DivideID Integer,' +
        'DSCID Integer,' +
        'RoofRPark Text(1),' +
        'AssumeKey Text(4),' +
        'TimeFrame Text(2),' +
        'ApplyAreaTF Text(2),' +
        'SqFt Integer,' +
        'Comment Text(16),' +
        'AppendDate Text(8),' +
        'IsActive Logical' +
        ')';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxParcelID ON mdl_ic_infilt_ac (ParcelID)';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxDivideID ON mdl_ic_infilt_ac (DivideID)';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxDSCID ON mdl_ic_infilt_ac (DSCID)';
      MSAccessManager.SendCommand(CommandText);
    except on E: Exception do
      begin
        fModel.AddError(TEMGAATSError.Create('Error in creating mdl_ic_infilt_ac table. ' +
          'Access error: ' + E.Message, eetError));
        raise;
      end;
    end;

    // Stormwater Planters
    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    try
      MSAccessManager.CreateDatabase(ICDirectoryPath + 'mdl_ic_swplnt_ac.mdb');
      CommandText := 'CREATE TABLE mdl_ic_swplnt_ac (' +
        'ICID Autoincrement PRIMARY KEY,' +
        'ParcelID Integer,' +
        'DivideID Integer,' +
        'DSCID Integer,' +
        'RoofRPark Text(1),' +
        'AssumeKey Text(4),' +
        'TimeFrame Text(2),' +
        'ApplyAreaTF Text(2),' +
        'SqFt Integer,' +
        'Comment Text(16),' +
        'AppendDate Text(8),' +
        'IsActive Logical' +
        ')';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxParcelID ON mdl_ic_swplnt_ac (ParcelID)';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxDivideID ON mdl_ic_swplnt_ac (DivideID)';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxDSCID ON mdl_ic_swplnt_ac (DSCID)';
      MSAccessManager.SendCommand(CommandText);
    except on E: Exception do
      begin
        fModel.AddError(TEMGAATSError.Create('Error in creating mdl_ic_swplnt_ac table. ' +
          'Access error: ' + E.Message, eetError));
        raise;
      end;
    end;

    // Storage
    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    try
      MSAccessManager.CreateDatabase(ICDirectoryPath + 'mdl_ic_store_ac.mdb');
      CommandText := 'CREATE TABLE mdl_ic_store_ac (' +
        'ICID Autoincrement PRIMARY KEY,' +
        'ParcelID Integer,' +
        'DivideID Integer,' +
        'DSCID Integer,' +
        'RoofRPark Text(1),' +
        'AssumeKey Text(4),' +
        'TimeFrame Text(2),' +
        'ApplyAreaTF Text(2),' +
        'SqFt Integer,' +
        'StoreNodeName Text(6),' +
        'StoreAreaBottomSqFt Float,' +
        'StoreAreaTopSqFt Float,' +
        'StorageDepth Float,' +
        'Orifice1Area Float,' +
        'Orifice1ConnNode Text(6),' +
        'Orifice2Areae Float,' +
        'Orifice2ConnNode Text(6),' +
        'OrificeElAboveBottom Float,' +
        'OFlowConnection Text(1),' +
        'InfiltrationRateInHr Float,' +
        'Comment Text(16),' +
        'AppendDate Text(8),' +
        'IsActive Logical' +
        ')';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxParcelID ON mdl_ic_store_ac (ParcelID)';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxDivideID ON mdl_ic_store_ac (DivideID)';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxDSCID ON mdl_ic_store_ac (DSCID)';
      MSAccessManager.SendCommand(CommandText);
    except on E: Exception do
      begin
        fModel.AddError(TEMGAATSError.Create('Error in creating mdl_ic_store_ac table. ' +
          'Access error: ' + E.Message, eetError));
        raise;
      end;
    end;

    // NGTORedir
    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    try
      MSAccessManager.CreateDatabase(ICDirectoryPath + 'mdl_ic_ngtoredir_ac.mdb');
      CommandText := 'CREATE TABLE mdl_ic_ngtoredir_ac (' +
        'ICID Autoincrement PRIMARY KEY,' +
        'SurfSCID Integer,' +
        'NGTO Text(10)' +
        ')';
      MSAccessManager.SendCommand(CommandText);
      CommandText := 'CREATE INDEX IdxSurfSCID ON mdl_ic_ngtoredir_ac (SurfSCID)';
      MSAccessManager.SendCommand(CommandText);
    except on E: Exception do
      begin
        fModel.AddError(TEMGAATSError.Create('Error in creating mdl_ic_ngtoredir_ac table. ' +
          'Access error: ' + E.Message, eetError));
        raise;
      end;
    end;

    fModel.Config.HasSkeletonModel := True;
    fModel.Config.Update;
  except on E: Exception do
    begin
      fModel.Config.HasSkeletonModel := False;
      fModel.Config.Update;;
    end;
  end;

  CodeSite.ExitMethod('TCreateEmptyModelDataCommand.Execute');
end;

{ TCreateDataAccessFileCommand }

constructor TCreateDataAccessFileCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Create data access file';
  fDescription := 'Creating data access file';
end;

procedure TCreateDataAccessFileCommand.Execute;
var
  DataAccessFileName: TFileName;
  i: Integer;
  Tokens: TStringList;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TCreateDataAccessFileName.Execute');
  DataAccessFileName := IncludeTrailingPathDelimiter(fModel.Path) +
    SystemConfig.DataAccessName;

  MSAccessManager.CreateDatabase(DataAccessFileName);

  // Link in tables from data from config
  Tokens := TStringList.Create;
  try
    for i := 0 to SystemConfig.DataAccessSourceCount - 1 do
    begin
      Application.ProcessMessages;
      if fAbortSignal.Abort then
        Exit;
      ExtractTokensL(SystemConfig.DataAccessSources[i], ';', '"', false, Tokens);
      // Ignore improperly formatted entries, like the "Filename" entry
      if Tokens.Count < 2 then
        Continue;

      MSAccessManager.Link(SystemConfig.DataAccessID[i], Tokens[0],
        IncludeTrailingPathDelimiter(fModel.Path) + Tokens[1]);
    end;
  finally
    Tokens.Free;
  end;
  CodeSite.ExitMethod('TCreateDataAccessFileName.Execute');
end;

{ TOpenMasterModelDataCommand }

constructor TOpenMasterModelDataCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Open master and model tables';
  fDescription := 'Opening master and model tables';
end;

procedure TOpenMasterModelDataCommand.Execute;
var
  RefreshMDBsCommand: IEMGAATSModelCommand;
  CopyICAltsCommand: IEMGAATSModelCommand;
  MIManager: TMapInfoDisplayManager;

  function QdesAvailable: Boolean;
  var
    ColCount: Integer;
    ColName: string;
    i: Integer;
  begin
    MIManager := TMapInfoDisplayManager.Create;
    try
      Result := False;
      if FileExists(fModel.Path + '\links\mdl_links_ac.tab') then
      begin
        MIManager.OpenTable(fModel.Path + '\links\mdl_links_ac.tab', False, True);
        ColCount := MIManager.NumCols('mdl_links_ac');
        for i := 1 to ColCount do
        begin
          ColName := UpperCase(MIManager.ColName('mdl_links_ac', i));
          if ColName = 'QDES' then
          begin
            Result := True;
            Break;
          end;
        end;
      end
    finally
      MIManager.Free;
      Sleep(1000);
    end;
  end;

begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TOpenMasterModelDataCommand.Execute');

  // Update the model.ini file from pertinent system ini sections
  fModel.Config.CopyConfigFromSystem;
  fModel.Config.SetMDBRootToModel;
  fModel.Config.Update;

  // We need to check if the model.ini's MasterFiles.root exists; if not, replace
  // with EMGAATSSystem.ini's version.
  if not DirectoryExists(fModel.Config.MasterRootPath) then
  begin
    fModel.Config.MasterRootPath := ExcludeTrailingPathDelimiter(SystemConfig.MasterFilesByName['root']);
    fModel.Config.Update;
  end;

  // Copy over system code and lookup databases if models want
  if fModel.Config.ForceMDBRefresh then
  begin
    RefreshMDBsCommand := TCopyMasterDatabasesCommand.Create(fModel, fProgressLabel, nil, fAbortSignal);
    RefreshMDBsCommand.Execute;

    // Special check: icalt directory may not exist in some models
    if not DirectoryExists(fModel.Config.Path + '\icalt') then
    begin
      CopyICAltsCommand := TCopyMasterICAltDatabasesCommand.Create(fModel, fProgressLabel,
        nil, fAbortSignal);
      CopyICAltsCommand.Execute;
      fModel.AddError(TEMGAATSError.Create('Did not find icalt directory. Created ' +
        'this directory and copied over the ICalt tables.', eetInfo));
    end;
  end;

  EMGWorkbenchManager.RunCommand('Close All');

  // If necessary, alter the mdl_links table to include Qdes field
  if not QdesAvailable then
  begin
    if FileExists(fModel.Path + '\links\mdl_links_ac.tab') then
    begin
      MIManager := TMapInfoDisplayManager.Create;
      MIManager.OpenTable(fModel.Path + '\links\mdl_links_ac.tab', False, True);
      MIManager.AddColumn('mdl_links_ac', 'Qdes', 'Float');
      MIManager.SaveTable('mdl_links_ac');
      fModel.AddError(TEMGAATSError.Create('Modified mdl_links_ac to include Qdes ',
        eetInfo));
      MIManager.Free;
      Sleep(1000);
    end;
  end;

  EMGWorkbenchManager.OpenTables;

  CodeSite.ExitMethod('TOpenMasterModelDataCommand.Execute');
end;

{ TRelinkModelDataCommand }

constructor TRelinkModelDataCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Relink model data';
  fDescription := 'Relinking model data';
end;

procedure TRelinkModelDataCommand.Execute;
var
  ModelPath: String;
  ModelMDBPath: String;
  Tokens: TStringList;
  i: Integer;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TRelinkModelDataCommand');

  ModelPath := IncludeTrailingPathDelimiter(fModel.Config.Path);
  ModelMDBPath := ModelPath + 'mdbs\';

  // ModelAssemble
  Application.ProcessMessages;
  if fAbortSignal.Abort then
    Exit;
  try
    if not FileExists(ModelMDBPath + SystemConfig.MDBsByName['ModelAssemble']) then
      fModel.AddError(TEMGAATSError.Create('ModelAssemble database does not exist.',
        eetError))
    else
    begin
      MSAccessManager.OpenDatabase(ModelMDBPath + 'ModelAssemble.mdb');
      MSAccessManager.Relink('mdl_nodes_ac', 'mdl_nodes_ac', ModelPath + 'nodes\mdl_nodes_ac.mdb');
      MSAccessManager.Relink('mdl_links_ac', 'mdl_links_ac', ModelPath + 'links\mdl_links_ac.mdb');
      MSAccessManager.Relink('mdl_speclinkdata_ac', 'mdl_speclinkdata_ac', ModelPath + 'links\mdl_speclinkdata_ac.mdb');
      MSAccessManager.Relink('mdl_speclinks_ac', 'mdl_speclinks_ac', ModelPath + 'links\mdl_speclinks_ac.mdb');
      MSAccessManager.Relink('mst_speclinkdata_ac', 'mst_speclinkdata_ac', SystemConfig.MasterFilesByName['speclinkdata']);
      MSAccessManager.Relink('mst_speclinks_ac', 'mst_speclinks_ac', SystemConfig.MasterFilesByName['speclinks']);
    end;
  except on E: Exception do
    fModel.AddError(TEMGAATSError.Create('Error relinking ModelAssemble database tables. ' +
      'Error: ' + E.Message, eetError));
  end;

  // LinkQAQC
  Application.ProcessMessages;
  if fAbortSignal.Abort then
    Exit;
  try
    if not FileExists(ModelMDBPath + SystemConfig.MDBsByName['LinkQAQC']) then
      fModel.AddError(TEMGAATSError.Create('LinkQAQC database does not exist.',
        eetError))
    else
    begin
      MSAccessManager.OpenDatabase(ModelMDBPath + 'LinkQAQC.mdb');
      MSAccessManager.Relink('mdl_nodes_ac', 'mdl_nodes_ac', ModelPath + 'nodes\mdl_nodes_ac.mdb');
      MSAccessManager.Relink('mdl_links_ac', 'mdl_links_ac', ModelPath + 'links\mdl_links_ac.mdb');
      MSAccessManager.Relink('mdl_SpecLinks_ac', 'mdl_speclinks_ac', ModelPath + 'links\mdl_SpecLinks_ac.mdb');
    end;
  except on E: Exception do
    fModel.AddError(TEMGAATSError.Create('Error relinking LinkQAQC database tables. ' +
      'Error: ' + E.Message, eetError));
  end;

  // HydInitDSC
  Application.ProcessMessages;
  if fAbortSignal.Abort then
    Exit;
  try
    if not FileExists(ModelMDBPath + SystemConfig.MDBsByName['HydInitDSC']) then
      fModel.AddError(TEMGAATSError.Create('HydInitDSC database does not exist.',
        eetError))
    else
    begin
      MSAccessManager.OpenDatabase(ModelMDBPath + 'HydInitDSC.mdb');
      MSAccessManager.Relink('mdl_dirsc_ac', 'mdl_dirsc_ac', ModelPath + 'dsc\mdl_dirsc_ac.mdb');
      MSAccessManager.Relink('mdl_ic_discoveg_ac', 'mdl_ic_discoveg_ac', ModelPath + 'ic\mdl_ic_discoveg_ac.mdb');
      MSAccessManager.Relink('mdl_ic_drywell_ac', 'mdl_ic_drywell_ac', ModelPath + 'ic\mdl_ic_drywell_ac.mdb');
      MSAccessManager.Relink('mdl_ic_grnroof_ac', 'mdl_ic_grnroof_ac', ModelPath + 'ic\mdl_ic_grnroof_ac.mdb');
      MSAccessManager.Relink('mdl_ic_store_ac', 'mdl_ic_store_ac', ModelPath + 'ic\mdl_ic_store_ac.mdb');
      MSAccessManager.Relink('mdl_ic_infilt_ac', 'mdl_ic_infilt_ac', ModelPath + 'ic\mdl_ic_infilt_ac.mdb');
      MSAccessManager.Relink('mdl_ic_swplnt_ac', 'mdl_ic_swplnt_ac', ModelPath + 'ic\mdl_ic_swplnt_ac.mdb');
      MSAccessManager.Relink('mdl_ic_ngtoredir_ac', 'mdl_ic_discoveg_ac', ModelPath + 'ic\mdl_ic_discoveg_ac.mdb');
      MSAccessManager.Relink('mst_ic_discoveg_ac', 'mst_ic_discoveg_ac', SystemConfig.MasterFilesByName['ic_discoveg']);
      MSAccessManager.Relink('mst_ic_drywell_ac', 'mst_ic_drywell_ac', SystemConfig.MasterFilesByName['ic_drywell']);
      MSAccessManager.Relink('mst_ic_grnroof_ac', 'mst_ic_grnroof_ac', SystemConfig.MasterFilesByName['ic_grnroof']);
      MSAccessManager.Relink('mst_ic_store_ac', 'mst_ic_store_ac', SystemConfig.MasterFilesByName['ic_store']);
    end;
  except on E: Exception do
    fModel.AddError(TEMGAATSError.Create('Error relinking HydInitDSC database tables. ' +
      'Error: ' + E.Message, eetError));
  end;

  // ModelDeployHydraulics
  Application.ProcessMessages;
  if fAbortSignal.Abort then
    Exit;
  try
    if not FileExists(ModelMDBPath + SystemConfig.MDBsByName['ModelDeployHydraulics']) then
      fModel.AddError(TEMGAATSError.Create('ModelDeployHydraulics database does not exist.',
        eetError))
    else
    begin
      MSAccessManager.OpenDatabase(ModelMDBPath + 'ModelDeployHydraulics.mdb');
      MSAccessManager.Run('ReferenceFromFile', [ModelPath + 'mdbs\EMGAATSCode.mdb',
        'EMGAATSCode']);
      MSAccessManager.Relink('_PipeShapes', 'PipeShapes', ModelPath + 'mdbs\LookupTables.mdb');
      MSAccessManager.Relink('_SanPattern', 'SanPattern', ModelPath + 'mdbs\LookupTables.mdb');
      MSAccessManager.Relink('_UserDefinedGeometry', 'UserDefinedGeometry', ModelPath + 'mdbs\LookupTables.mdb');
      MSAccessManager.Relink('_PumpCurves', 'PumpCurves', ModelPath + 'mdbs\LookupTables.mdb');
      MSAccessManager.Relink('_TagDefinition', 'TagDefinition', ModelPath + 'mdbs\LookupTables.mdb');
      MSAccessManager.Relink('mdl_dirsc_ac', 'mdl_dirsc_ac', ModelPath + 'dsc\mdl_dirsc_ac.mdb');
      MSAccessManager.Relink('mdl_nodes_ac', 'mdl_nodes_ac', ModelPath + 'nodes\mdl_nodes_ac.mdb');
      MSAccessManager.Relink('mdl_links_ac', 'mdl_links_ac', ModelPath + 'links\mdl_links_ac.mdb');
      MSAccessManager.Relink('mdl_speclinkdata_ac', 'mdl_speclinkdata_ac', ModelPath + 'links\mdl_speclinkdata_ac.mdb');
      MSAccessManager.Relink('mdl_speclinks_ac', 'mdl_speclinks_ac', ModelPath + 'links\mdl_speclinks_ac.mdb');
    end;
  except on E: Exception do
    fModel.AddError(TEMGAATSError.Create('Error relinking ModelDeployHydraulics database tables. ' +
      'Error: ' + E.Message, eetError));
  end;

  // ModelDeployHydrology
  Application.ProcessMessages;
  if fAbortSignal.Abort then
    Exit;
  try
    if not FileExists(ModelMDBPath + SystemConfig.MDBsByName['ModelDeployHydrology']) then
      fModel.AddError(TEMGAATSError.Create('ModelDeployHydrology database does not exist.',
        eetError))
    else
    begin
      MSAccessManager.OpenDatabase(ModelMDBPath + 'ModelDeployHydrology.mdb');
      MSAccessManager.Run('ReferenceFromFile', [ModelPath + 'mdbs\EMGAATSCode.mdb',
        'EMGAATSCode']);
      MSAccessManager.Relink('mdl_dirsc_ac', 'mdl_dirsc_ac', ModelPath + 'dsc\mdl_dirsc_ac.mdb');
      MSAccessManager.Relink('mdl_nodes_ac', 'mdl_nodes_ac', ModelPath + 'nodes\mdl_nodes_ac.mdb');
      MSAccessManager.Relink('mdl_links_ac', 'mdl_links_ac', ModelPath + 'links\mdl_links_ac.mdb');
      MSAccessManager.Relink('mdl_surfsc_ac', 'mdl_surfsc_ac', ModelPath + 'surfsc\mdl_surfsc_ac.mdb');
      MSAccessManager.Relink('mdl_ic_discoveg_ac', 'mdl_ic_discoveg_ac', ModelPath + 'ic\mdl_ic_discoveg_ac.mdb');
      MSAccessManager.Relink('mdl_ic_drywell_ac', 'mdl_ic_drywell_ac', ModelPath + 'ic\mdl_ic_drywell_ac.mdb');
      MSAccessManager.Relink('mdl_ic_grnroof_ac', 'mdl_ic_grnroof_ac', ModelPath + 'ic\mdl_ic_grnroof_ac.mdb');
      MSAccessManager.Relink('mdl_ic_store_ac', 'mdl_ic_store_ac', ModelPath + 'ic\mdl_ic_store_ac.mdb');
      MSAccessManager.Relink('mdl_ic_infilt_ac', 'mdl_ic_infilt_ac', ModelPath + 'ic\mdl_ic_infilt_ac.mdb');
      MSAccessManager.Relink('mdl_ic_swplnt_ac', 'mdl_ic_swplnt_ac', ModelPath + 'ic\mdl_ic_swplnt_ac.mdb');
      MSAccessManager.Relink('mdl_ic_ngtoredir_ac', 'mdl_ic_ngtoredir_ac', ModelPath + 'ic\mdl_ic_ngtoredir_ac');
    end;
  except on E: Exception do
    fModel.AddError(TEMGAATSError.Create('Error relinking ModelDeployHydrology database tables. ' +
      'Error: ' + E.Message, eetError));
  end;

  // DataAccess
  Application.ProcessMessages;
  if fAbortSignal.Abort then
    Exit;
  try
    if not FileExists(ModelMDBPath + 'DataAccess.mdb') then
      fModel.AddError(TEMGAATSError.Create('DataAccess database does not exist.',
        eetError))
    else
    begin
      MSAccessManager.OpenDatabase(ModelMDBPath + 'DataAccess.mdb');
      Tokens := TStringList.Create;
      try
        for i := 0 to SystemConfig.DataAccessSourceCount - 1 do
        begin
          Application.ProcessMessages;
          if fAbortSignal.Abort then
            Exit;
          ExtractTokensL(SystemConfig.DataAccessSources[i], ';', '"', false, Tokens);
          // Ignore improperly formatted entries, like the "Filename" entry
          if Tokens.Count < 2 then
            Continue;

          try
            // If the object doesn't exist in DataAccess.mdb, link it.  Otherwise relink.
            if MSAccessManager.TableExists(SystemConfig.DataAccessID[i]) then
              MSAccessManager.Relink(SystemConfig.DataAccessID[i], Tokens[0],
                IncludeTrailingPathDelimiter(fModel.Path) + Tokens[1])
            else
            begin
              MSAccessManager.Link(SystemConfig.DataAccessID[i], Tokens[0],
                IncludeTrailingPathDelimiter(fModel.Path) + Tokens[1]);
              fModel.AddError(TEMGAATSError.Create('Linked in new table to DataAccess: ' +
                SystemConfig.DataAccessID[i], eetInfo));
            end;
          except on E: Exception do
            fModel.AddError(TEMGAATSError.Create('Error linking DataAccess table ' +
              SystemConfig.DataAccessID[i] + ' to table ' + Tokens[0] + ' of ' +
              IncludeTrailingPathDelimiter(fModel.Path) + Tokens[1] +
              '. Error: ' + E.Message, eetError));
          end;
        end;
      finally
        Tokens.Free;
      end;
    end;
  except on E: Exception do
    fModel.AddError(TEMGAATSError.Create('Error relinking DataAccess database tables. ' +
      'Error: ' + E.Message, eetError));
  end;

  // ModelResults
  Application.ProcessMessages;
  if fAbortSignal.Abort then
    Exit;
  try
    if not FileExists(ModelMDBPath + SystemConfig.MDBsByName['ModelResults']) then
      fModel.AddError(TEMGAATSError.Create('ModelResults database does not exist.',
        eetError))
    else
    begin
      MSAccessManager.OpenDatabase(ModelMDBPath + 'ModelResults.mdb');
      MSAccessManager.Relink('mdl_dirsc_ac', 'mdl_dirsc_ac', ModelPath + 'dsc\mdl_dirsc_ac.mdb');
      MSAccessManager.Relink('mdl_links_ac', 'mdl_links_ac', ModelPath + 'links\mdl_links_ac.mdb');
    end;
  except on E: Exception do
    fModel.AddError(TEMGAATSError.Create('Error relinking ModelResults database tables. ' +
      'Error: ' + E.Message, eetError));
  end;

  CodeSite.ExitMethod('TRelinkModelDataCommand');
end;

{ TDeployRunoffCommand }

constructor TDeployRunoffCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Deploy runoff file';
  fDescription := 'Deploying runoff file';
  Add(TPrepareRunoffFileCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCreateRunoffBatchFileCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCreateReconciliationSpreadsheetCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
end;

procedure TDeployRunoffCommand.Execute;
begin
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TDeployRunoffCommand.Execute');
  if fModel.Config.HasDirectSubcatchments or fModel.Config.HasSurfaceSubcatchments then
  begin
    inherited;
    fModel.Config.RunoffDeployDate := Now;
  end
  else
  begin
    fModel.AddError(TEMGAATSError.Create('Could not create runoff file.  No ' +
      'direct or surface subcatchments available.', eetError));
    Exit;
  end;

  if not fModel.Config.HasDirectSubcatchments then
    fModel.AddError(TEMGAATSError.Create('No direct subcatchments available.',
      eetWarning))
  else if not fModel.Config.HasSurfaceSubcatchments then
    fModel.AddError(TEMGAATSError.Create('No surface subcatchments available.',
      eetWarning));

  CodeSite.ExitMethod('TDeployRunoffCommand.Execute');
end;

{ TPrepareRunoffFileCommand }

constructor TPrepareRunoffFileCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Prepare runoff file';
  fDescription := 'Preparing runoff file';
end;

procedure TPrepareRunoffFileCommand.Execute;
var
  RunoffFileName: String;
  StormUpdateQueryText: String;
  RunoffTitle1QueryText: String;
  RunoffTitle1: String;
  RunoffTitle2QueryText: String;
  RunoffTitle2: String;
  TimeFrameText: String;
  RainfallFile: String;
  InterfaceFileQueryText: String;
  i: Integer;
  adoConnection: TADOConnection;
  adoQuery: TADOQuery;
  StartDate: TDateTime;
  EndDate: TDateTime;
  TimeStep: Integer;
  StormControlB1Text: String;
  StormControlB3Text: String;
  RainfallPath: String;
  IsDesignStorm: Boolean;
  NumGages: Integer;
  StartYearOverride: Integer;
  EndYearOverride: Integer;
  StartDateToWrite: Integer;
  DestPath: String;
begin
  inherited;
  if not fEnabled then
    Exit;

  // Prepare runoff data to be ready for export
  MSAccessManager.OpenDatabase(fModel.Config.MasterMDBFilesByName['ModelDeployHydrology']);

  // Prepare tables
  Application.ProcessMessages;
  if fAbortSignal.Abort then
    Exit;
  MSAccessManager.Run('ExecuteQueryTable', ['ListExecuteModelBuild', 'Block', 'P', 0]);

  Application.ProcessMessages;
  if fAbortSignal.Abort then
    Exit;
  MSAccessManager.Run('ExecuteQueryTable', ['ListExecuteModelBuild', 'Block', 'R', 0]);

  // If we don't have any storms, don't build any runoff files
  if fModel.Config.StormsToBuildCount = 0 then
    Exit;

  try
    adoConnection := TADOConnection.Create(Application);
    adoConnection.ConnectionString := 'Provider=Microsoft.Jet.OLEDB.4.0;' +
      'Data Source=' + IncludeTrailingPathDelimiter(SystemConfig.MDBsByName['root']) +
        SystemConfig.MDBsByName['LookupTables'] + ';' +
      'Persist Security Info=False';
    adoConnection.Open;

    adoQuery := TADOQuery.Create(Application);
    adoQuery.Connection := adoConnection;

    try
      for i := 0 to fModel.Config.StormsToBuildCount - 1 do
      begin
        Application.ProcessMessages;
        if fAbortSignal.Abort then
          Exit;

        // Write file
        ForceDirectories(IncludeTrailingPathDelimiter(fModel.Path) + 'sim\' +
          fModel.Config.StormsToBuild[i]);
        RunoffFileName := IncludeTrailingPathDelimiter(fModel.Path) + 'sim\' +
          fModel.Config.StormsToBuild[i] + '\' + fModel.Config.RunoffFileName;

        // Choose storm type (calibration or design)
        adoQuery.SQL.Clear;
        adoQuery.SQL.Add('SELECT Type, NumGages FROM Storms WHERE (Abbreviation = "' +
          fModel.Config.StormsToBuild[i] + '")');
        adoQuery.Open;
        IsDesignStorm := adoQuery.FieldValues['Type'] = 'D';
        NumGages := adoQuery.FieldValues['NumGages'];
        adoQuery.Close;
        StormUpdateQueryText := 'Update R_Control_B1 SET R_Control_B1.[DesignStorm?] = ' +
          BoolToStr(IsDesignStorm, True) +', R_Control_B1.NRGAG = ' + IntToStr(NumGages);
        MSAccessManager.RunQuery(StormUpdateQueryText);

        // Update title of runoff
        RunoffTitle1 := '"Model: ' + Ellipsize(fModel.Config.Path, 70) + '"';
        RunoffTitle1QueryText := 'UPDATE R_control_A1 SET R_control_A1.TitleText = ' +
          RunoffTitle1 + ' WHERE (((R_control_A1.TwoRecordsOnly)=1))';
        MSAccessManager.RunQuery(RunoffTitle1QueryText);

        // Update interface files
        adoQuery.SQL.Clear;
        adoQuery.SQL.Add('SELECT Filename FROM Storms WHERE (Abbreviation = "' +
          fModel.Config.StormsToBuild[i] + '")');
        adoQuery.Open;
        RainfallFile := adoQuery.FieldValues['Filename'];
        adoQuery.Close;
        InterfaceFileQueryText := 'UPDATE R_Control_Files SET R_Control_Files.FILENAM = ' +
          QuotedStr(RainfallFile) + ' WHERE ((R_Control_Files.FILENUM = 11))';
        MSAccessManager.RunQuery(InterfaceFileQueryText);

        // Update storm controls
        adoQuery.SQL.Clear;
        adoQuery.SQL.Add('SELECT StartDate, EndDate, TimeStepMinutes, StartYearOverride, ' +
          'EndYearOverride, WetSeconds, WetDrySeconds, DrySeconds FROM Storms '+
          'WHERE (Abbreviation = "' + fModel.Config.StormsToBuild[i] + '")');
        adoQuery.Open;
        StartDate := adoQuery.FieldValues['StartDate'];
        EndDate := adoQuery.FieldValues['EndDate'];
        StartYearOverride := adoQuery.FieldValues['StartYearOverride'];
        EndYearOverride := adoQuery.FieldValues['EndYearOverride'];
        StartDateToWrite := IfThen(StartYearOverride = 0, YearOf(StartDate),
          StartYearOverride);
        TimeStep := adoQuery.FieldValues['TimeStepMinutes'];

        Assert(EndDate > StartDate, Format('Start date of storm %s = %s is later than ' +
          'end date of storm = %s',
          [fModel.Config.StormsToBuild[i], FormatDateTime('', StartDate),
          FormatDateTime('', EndDate)]));
        StormControlB1Text := 'UPDATE R_Control_B1 SET ' +
          'R_Control_B1.NHR = ' + IntToStr(HourOf(StartDate)) + ', ' +
          'R_Control_B1.NMN = ' + IntToStr(MinuteOf(StartDate)) + ', ' +
          'R_Control_B1.NDAY = ' + IntToStr(DayOf(StartDate)) + ', ' +
          'R_Control_B1.[MONTH] = ' + IntToStr(MonthOf(StartDate)) + ', ' +
          'R_Control_B1.IYRSTR = ' + IntToStr(StartDateToWrite) + ';';
        MSAccessManager.RunQuery(StormControlB1Text);

        StormControlB3Text := 'UPDATE R_Control_B3 SET ' +
          'R_Control_B3.Wet = ' + IntToStr(adoQuery.FieldValues['WetSeconds']) + ', ' +
          'R_Control_B3.WetDry = ' + IntToStr(adoQuery.FieldValues['WetDrySeconds']) + ', ' +
          'R_Control_B3.Dry = ' + IntToStr(adoQuery.FieldValues['DrySeconds']) + ', ' +
          'R_Control_B3.LUNIT = 2, ' + // 2 = hours
          'R_Control_B3.[Long] = ' + Format('%.0f', [HourSpan(EndDate, StartDate)]) + ';';
        MSAccessManager.RunQuery(StormControlB3Text);
        adoQuery.Close;

        // Copy the rainfall file
        RainfallPath := IncludeTrailingPathDelimiter(SystemConfig.MasterFilesByName['root']) +
          'rainfall\';
        CodeSite.Send('Attempting to copy ' + RainfallPath + RainfallFile + ' to '+
          IncludeTrailingPathDelimiter(fModel.Path) + 'sim\' +
          fModel.Config.StormsToBuild[i] + '\' + RainfallFile);
        DestPath := IncludeTrailingPathDelimiter(fModel.Path) + 'sim\' +
            fModel.Config.StormsToBuild[i] + '\' + RainfallFile;
        if not FileExists(DestPath) then
          if not CopyFile(RainfallPath + RainfallFile, DestPath) then
            fModel.AddError(TEMGAATSError.Create('Could not copy rainfall ' + RainfallFile,
              eetError));

        // Write the runoff file
        case fModel.Config.TimeFrame of
          tfExisting: TimeFrameText := 'Existing';
          tfFuture: TimeFrameText := 'Future';
        end;
        RunoffTitle2 := '"TimeFrame: ' + TimeFrameText +
          ', Design Storm Model Created:' + FormatDateTime('m/d/yyyy h:mm', Now) + '"';
        RunoffTitle2QueryText := 'UPDATE R_control_A1 SET R_control_A1.TitleText = ' +
          RunoffTitle2 + 'WHERE (((R_control_A1.TwoRecordsOnly)=2))';
        MSAccessManager.RunQuery(RunoffTitle2QueryText);
        MSAccessManager.Run('ExportQueryTable', [RunoffFileName, 'ListExportModelPDX',
          'Block', 'R', 0]);
        fModel.AddError(TEMGAATSError.Create('Created runoff file for storm '+
          fModel.Config.StormsToBuild[i], eetInfo));
        fModel.AddError(TEMGAATSError.Create('Run runoff file for storm '+
          fModel.Config.StormsToBuild[i], eetToDo));
      end;
    finally
      adoQuery.Free;
      adoConnection.Close;
      adoConnection.Free;
    end;
  except on E: Exception do
    fModel.AddError(TEMGAATSError.Create('Could not prepare runoff file', eetError));
  end;
end;

{ TCreateRunoffBatchFileCommand }

constructor TCreateRunoffBatchFileCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Create runoff batch file';
  fDescription := 'Creating runoff batch file';
end;

procedure TCreateRunoffBatchFileCommand.Execute;
var
  OutputFileStream: TFileStream;
  OutputFile: TStAnsiTextStream;
  OutputFileName: String;
  i, CurrentRunoffLine : integer;
  OutputLine: String;
begin
  inherited;
  if not fEnabled then
    Exit;

  try
    for i := 0 to fModel.Config.StormsToBuildCount - 1 do
    begin
      Application.ProcessMessages;
      if fAbortSignal.Abort then
        Exit;
      ForceDirectories(IncludeTrailingPathDelimiter(fModel.Path) + 'sim\' +
        fModel.Config.StormsToBuild[i]);
      OutputFileName := IncludeTrailingPathDelimiter(fModel.Config.Path) + 'sim\' +
        fModel.Config.StormsToBuild[i] +'\pdxrun.bat';
      OutputFileStream := TFileStream.Create(OutputFileName, fmCreate);
      OutputFile := TStAnsiTextStream.Create(OutputFileStream);
      try
        for CurrentRunoffLine := 0 to High(RunoffLines) do
        begin
          OutputLine := RunoffLines[CurrentRunoffLine];
          OutputFile.WriteLine(OutputLine);
        end;
      finally
        OutputFile.Free;
        OutputFileStream.Free;
      end;
    end;
  except on E: Exception do
    fModel.AddError(TEMGAATSError.Create('Could not write runoff batch file', eetError));
  end;
end;

{ TCreateReconciliationSpreadsheetCommand }

constructor TCreateReconciliationSpreadsheetCommand.Create(
  AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Create reconciliation spreadsheet';
  fDescription := 'Creating reconciliation spreadsheet';
end;

procedure TCreateReconciliationSpreadsheetCommand.Execute;
begin
  inherited;
  if not fEnabled then
    Exit;

  Application.ProcessMessages;
  if fAbortSignal.Abort then
    Exit;
  try
    datmodHydroStats.Initialize(fModel);
  except
    fModel.AddError(TEMGAATSError.Create('One or more reconciliation checks failed.',
      eetError));
  end;

  Application.ProcessMessages;
  if fAbortSignal.Abort then
    Exit;
  try
    datmodHydroStats.Calculate(fModel.QCWorkbookFileName);
    if (FileExists(fModel.QCWorkbookFileName)) and
      (FileDateToDateTime(FileAge(fModel.QCWorkbookFileName)) >
      fModel.Config.RunoffDeployDate) then
      fModel.AddError(TEMGAATSError.Create('Hydrologic quality control: ' +
        fModel.Config.HydroQCFileName + ' created.', eetInfo))
    else
      fModel.AddError(TEMGAATSError.Create('Hydrologic quality control file was not ' +
      'created.  Check Excel macro security settings.', eetError));
  except
    on E: Exception do
      fModel.AddError(TEMGAATSError.Create('Problem creating hydrologic quality control: ' +
        fModel.Config.HydroQCFileName + ' was not created.'#13'Exception: ' +
        E.Message , eetError));
  end;

  // Send this to the report page
  fModel.AddError(TEMGAATSError.Create('---------------------------', eetInfo));
  fModel.AddError(TEMGAATSError.Create('Reconciliation checks', eetInfo));
  fModel.AddError(TEMGAATSError.Create(Format('Surface subcatchments total area checksum: %.2f',
    [Double(datmodHydroStats.csHCardTA)]), eetInfo));
  fModel.AddError(TEMGAATSError.Create(Format('Surface subcatchments impervious area checksum: %.2f',
    [Double(datmodHydroStats.csHCardIA)]), eetInfo));
  fModel.AddError(TEMGAATSError.Create(Format('Direct subcatchments total area checksum: %.2f',
    [Double(datmodHydroStats.csdscTA)]), eetInfo));
  fModel.AddError(TEMGAATSError.Create(Format('Direct subcatchments impervious area checksum: %.2f',
    [Double(datmodHydroStats.csdscTA)]), eetInfo));
  fModel.AddError(TEMGAATSError.Create('Hydrologic quality control: ' +
    fModel.Config.HydroQCFileName + ' created.', eetInfo));

  if (Abs(double(datmodHydroStats.csHCardTA)) > RSSwarn) or
    (Abs(double(datmodHydroStats.csHCardIA)) > RSSwarn) or
    (Abs(double(datmodHydroStats.csdscTA)) > RSSwarn) or
    (Abs(double(datmodHydroStats.csdscIA)) > RSSwarn) then
    fModel.AddError(TEMGAATSError.Create(Format('One or more reconciliation checks exceed %.2f threshold.',
      [RSSWarn]), eetError))
  else
    fModel.AddError(TEMGAATSError.Create(Format('All reconciliation checks within %.2f threshold.',
      [RSSWarn]), eetInfo));
  fModel.AddError(TEMGAATSError.Create('---------------------------', eetInfo));

end;

{ TDeployEngineFileCommand }

constructor TDeployEngineFileCommand.Create(AModel: TEMGAATSModel; AProgressLabel: TLabel = nil;
  AProgressBar: TRzProgressBar = nil; AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Deploy engine file';
  fDescription := 'Deploying engine file';
end;

procedure TDeployEngineFileCommand.Execute;
var
  EngineFileName: String;
  EngineTitle1QueryText: String;
  EngineTitle1: String;
  TimeFrameText: String;
  DHIFileName: String;
  UpdateBaseflowSettingsText: String;
begin
  inherited;
  if not fEnabled then
    Exit;

  try
    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    MSAccessManager.OpenDatabase(fModel.Config.MasterMDBFilesByName['ModelDeployHydraulics']);

    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    MSAccessManager.Run('XP_Parallel_Links', ['_getParallelLinks']);

    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    MSAccessManager.Run('XP_Special_Links', []);

    case fModel.Config.TimeFrame of
      tfExisting: TimeFrameText := 'Existing';
      tfFuture: TimeFrameText := 'Future';
    end;

    EngineTitle1 := '"TimeFrame: ' + TimeFrameText +
      ', Model Created:' + FormatDateTime('m/d/yyyy h:mm', Now) + '"';
    EngineTitle1QueryText := 'UPDATE JobControlExtran SET JobControlExtran.TValue = ' +
      EngineTitle1 + ' WHERE (((JobControlExtran.Tag)="ALPHB")) ';

    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    MSAccessManager.RunQuery(EngineTitle1QueryText);

    EngineFileName := IncludeTrailingPathDelimiter(fModel.Path) + 'sim\' +
      fModel.Config.EngineExportFileName;

    UpdateBaseflowSettingsText := 'UPDATE XPExportQueryTable SET XPExportQueryTable.[Execute?] = ' +
      BoolToStr(fModel.Config.UseBaseflow) +
      ' WHERE (((XPExportQueryTable.Class)="BaseFlow"));';
    MSAccessManager.RunQuery(UpdateBaseflowSettingsText);

    MSAccessManager.RunQuery('_UpdateSimLinkID');
    MSAccessManager.Run('ExportQueryTable',[EngineFileName, 'XPExportQueryTable',
      'Class', 'ALL', 0]);

    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    DHIFileName := IncludeTrailingPathDelimiter(fModel.Config.Path) + 'sim\' +
      'DHImodel.mdb';
    MSAccessManager.Run('CreateDHIDatabase', [DHIFileName,'DHI_Export']);
  except on E: Exception do
    fModel.AddError(TEMGAATSError.Create('Could not create engine file. ' +
      'MS Access Error: ' + E.Message, eetError));
  end;
end;

{ TBuildNewModelCommand }

constructor TBuildNewModelCommand.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel = nil; AProgressBar: TRzProgressBar = nil;
  AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Build new model';
  fDescription := 'Building new model';
  Add(TBuildEmptyModelCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TBuildNetworkCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TBuildDirectSubcatchmentsCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TBuildSurfaceSubcatchmentsCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TBuildStudyAreaBoundaryCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TRunAllStormwaterControlsCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCalculateHydrologyCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TDeployRunoffCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCalculateHydraulicsCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TDeployEngineFileCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TMergeStormwaterControlsCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCreateQCWorkspacesCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCreateQCWorkspacesCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCopyStandardMapInfoWorkspacesCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
end;

procedure TBuildNewModelCommand.Execute;
begin
  fModel.Config.HasNetwork := False;
  fModel.Config.HasDirectSubcatchments := False;
  fModel.Config.HasSurfaceSubcatchments := False;
  fModel.Config.Update;
  inherited;
  fModel.AddError(TEMGAATSError.Create('Run your storms'' runoff files by '+
    'executing pdxrun.bat in each sim\storm directory.', eetToDo));
  fModel.AddError(TEMGAATSError.Create('Import the XPExtran.XPX file '+
    'in the sim directory into an XP model. Copy that XP model into each storm directory '+
    'and change the job control and interface file accordingly.', eetToDo));
  fModel.AddError(TEMGAATSError.Create('After running the XP models, you ' +
    'can prepare results by running EMGAATS and clicking on Review.', eetToDo));
  fModel.Config.ModelCreateDate := Now;
  fModel.Config.HasNetwork := True;
  fModel.Config.HasDirectSubcatchments := True;
  fModel.Config.HasSurfaceSubcatchments := True;
  fModel.Config.Update;
end;

{ TBuildOverExistingModel }

constructor TBuildOverExistingModel.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel = nil; AProgressBar: TRzProgressBar = nil;
  AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Build over existing model';
  fDescription := 'Building over existing model';
  Add(TConfigureModelForBuildingCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TBuildNetworkCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TBuildDirectSubcatchmentsCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TBuildSurfaceSubcatchmentsCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TBuildStudyAreaBoundaryCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCalculateHydrologyCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TDeployRunoffCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TDeployEngineFileCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCreateQCWorkspacesCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
end;

{ TEMGAATSModelCommandList }

function TEMGAATSModelCommandList.Add(const Item: IEMGAATSModelCommand): Integer;
begin
  Result := fList.Add(Item);
end;

procedure TEMGAATSModelCommandList.Clear;
begin
  fList.Clear;
end;

constructor TEMGAATSModelCommandList.Create;
begin
  fList := TInterfaceList.Create;
end;

procedure TEMGAATSModelCommandList.Delete(Index: Integer);
begin
  fList.Delete(Index);
end;

destructor TEMGAATSModelCommandList.Destroy;
begin
  fList.Free;
  inherited;
end;

procedure TEMGAATSModelCommandList.Exchange(Index1, Index2: Integer);
var
  SwapEnabledBit: Boolean;
begin
  fList.Exchange(Index1, Index2);
end;

procedure TEMGAATSModelCommandList.Execute(AbortSignal: TEMGAATSCommandAbortSignal);
var
  i: Integer;
  CurrentCommand: IEMGAATSModelCommand;
begin
  for i := 0 to fList.Count - 1 do
  begin
    Assert(Assigned(AbortSignal), 'AbortSignal is nil');
    if AbortSignal.Abort then
      Exit;
    CurrentCommand := IEMGAATSModelCommand(fList[i]);
    CodeSite.Send('Executing ' + CurrentCommand.Name);
    CurrentCommand.Execute;
    Application.ProcessMessages;
  end;
end;

function TEMGAATSModelCommandList.First: IEMGAATSModelCommand;
begin
  Result := IEMGAATSModelCommand(fList.First);
end;

function TEMGAATSModelCommandList.Get(Index: Integer): IEMGAATSModelCommand;
begin
  Result := IEMGAATSModelCommand(fList[Index]);
end;

function TEMGAATSModelCommandList.GetCapacity: Integer;
begin
  Result := fList.Capacity;
end;

function TEMGAATSModelCommandList.GetCommandCount: Integer;
var
  i: Integer;
  CurrentCommand: IEMGAATSModelCommand;
begin
  Result := 0;
  for i := 0 to fList.Count - 1 do
  begin
    CurrentCommand := IEMGAATSModelCommand(fList[i]);
    Inc(Result, CurrentCommand.CommandCount);
  end;
end;

function TEMGAATSModelCommandList.GetCount: Integer;
begin
  Result := fList.Count;
end;

function TEMGAATSModelCommandList.GetEnabledCommandCount: Integer;
var
  i: Integer;
  CurrentCommand: IEMGAATSModelCommand;
begin
  CodeSite.EnterMethod('TEMGAATSModelCommandList.GetEnabledCommandCount');
  Result := 0;
  for i := 0 to fList.Count - 1 do
  begin
    CurrentCommand := IEMGAATSModelCommand(fList[i]);
    Inc(Result, CurrentCommand.EnabledCommandCount);
    CodeSite.SendFmtMsg('i=%d, EnabledCommandCount=%d',
      [i, CurrentCommand.EnabledCommandCount]);
  end;
  CodeSite.ExitMethod('TEMGAATSModelCommandList.GetEnabledCommandCount');
end;

//function TEMGAATSModelCommandList.GetEnumerator: TInterfaceListEnumerator;
//begin
//
//end;
//
function TEMGAATSModelCommandList.IndexOf(const Item: IEMGAATSModelCommand): Integer;
begin
  Result := fList.IndexOf(Item);
end;

procedure TEMGAATSModelCommandList.Insert(Index: Integer;
  const Item: IEMGAATSModelCommand);
begin
  fList.Insert(Index, Item);
end;

function TEMGAATSModelCommandList.Last: IEMGAATSModelCommand;
begin
  Result := IEMGAATSModelCommand(fList.Last);
end;

procedure TEMGAATSModelCommandList.Lock;
begin
  fList.Lock;
end;

procedure TEMGAATSModelCommandList.Put(Index: Integer; const Value: IEMGAATSModelCommand);
begin
  fList[Index] := Value;
end;

function TEMGAATSModelCommandList.Remove(const Item: IEMGAATSModelCommand): Integer;
begin
  Result := fList.Remove(Item);
end;

procedure TEMGAATSModelCommandList.SetCapacity(const Value: Integer);
begin
  fList.Capacity := Value;
end;

procedure TEMGAATSModelCommandList.SetCount(const Value: Integer);
begin
  fList.Count := Value;
end;

procedure TEMGAATSModelCommandList.Unlock;
begin
  fList.Unlock;
end;

{ TClearModelDirectoryCommand }

constructor TClearModelDirectoryCommand.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel = nil; AProgressBar: TRzProgressBar = nil;
  AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Clear model directory';
  fDescription := 'Clearing model directory';
end;

procedure TClearModelDirectoryCommand.Execute;
var
  i: Integer;
  WasDeleted: Boolean;
  DirectoryToDelete: String;
begin
  inherited;
  if not fEnabled then
    Exit;

  // Reset the engines

  CodeSite.EnterMethod('TClearModelDirectoryCommand.Execute');
  try
    try
      frmMain.FinalizeGlobalObjects;
      frmMain.InitializeGlobalObjects;
      EMGWorkbenchManager.Initialize(SystemConfig.EMGWorkbench);
      EMGWorkbenchManager.SetModel(fModel);
      fModel.Config.SetMDBRootToSystem;

      // Delete the standard directories
      CodeSite.Send('fModel.Config.StandardDirectoryCount',
        fModel.Config.StandardDirectoryCount);
      for i := 0 to fModel.Config.StandardDirectoryCount - 1 do
      begin
        Application.ProcessMessages;
        if fAbortSignal.Abort then
          Exit;
        DirectoryToDelete := IncludeTrailingPathDelimiter(fModel.Path) +
          SystemConfig.StandardDirectoriesID[i];
        CodeSite.Send('Deleting ' + DirectoryToDelete);
        if DirectoryExists(DirectoryToDelete) then
          WasDeleted := DelTree(IncludeTrailingPathDelimiter(fModel.Path) +
            SystemConfig.StandardDirectoriesID[i]);
        if not WasDeleted then
          fModel.AddError(TEMGAATSError.Create('Could not delete ' +
            DirectoryToDelete + ' while clearing model', eetError));
      end;
    except  on E: Exception do
      fModel.AddError(TEMGAATSError.Create('Could not clear model directory properly. ' +
        E.Message, eetError));
    end;
  finally
    CodeSite.ExitMethod('TClearModelDirectoryCommand.Execute');
  end;
end;

{ TCreateQCWorkspacesCommand }

constructor TCreateQCWorkspacesCommand.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel = nil; AProgressBar: TRzProgressBar = nil;
  AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Create QC workspaces';
  fDescription := 'Creating QC workspaces';
end;

procedure TCreateQCWorkspacesCommand.Execute;
var
  OutputReportFileName: String;
  OutputQueryLocation: String;
begin
  inherited;
  if not fEnabled then
    Exit;


  CodeSite.EnterMethod('TCreateQCWorkspacesCommand.Execute');
  try
    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    try
      // Run the queries that produce the text files that Albert's MBX will bite on
      OutputReportFileName := IncludeTrailingPathDelimiter(fModel.Path) +
        'qc\LinkQAQC.txt';
      OutputQueryLocation := IncludeTrailingPathDelimiter(fModel.Path) + 'qc';
      MSAccessManager.OpenDatabase(fModel.MDBs['LinkQAQC']);
      MSAccessManager.Run('ExportQCReport', ['Qchec', OutputReportFileName, 0]);
      MSAccessManager.Run('ExportQCTables', [OutputQueryLocation, 'QAQCQueryList', 0]);
    except on E: Exception do
      fModel.AddError(TEMGAATSError.Create('Could not complete QC queries. ' +
        'MSAccess: ' + E.Message, eetError));
    end;

    // Run QCWorkspace MBX
    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    if Assigned(QCWorkspaceManager) then
      FreeAndNil(QCWorkspaceManager)
    else
      QCWorkspaceManager := TQCWorkspaceManager.Create;

    try
      QCWorkspaceManager.Initialize(SystemConfig.QCWorkspace);
      QCWorkspaceManager.SetModel(fModel);
      QCWorkspaceManager.Run;
    finally
      FreeAndNil(QCWorkspaceManager);
    end;

    // Copy over Qc_FB_modelbuild_v21.wor
    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    CopyFile(
      IncludeTrailingPathDelimiter(SystemConfig.MasterFilesByName['master_wors']) +
        'Qc_modelbuild_v22.wor',
      IncludeTrailingPathDelimiter(fModel.Path) + 'wors\Qc_modelbuild_v22.wor');
    if not FileExists(IncludeTrailingPathDelimiter(fModel.Path) +
      'wors\Qc_modelbuild_v22.wor') then
      fModel.AddError(TEMGAATSError.Create('Could not copy Qc_modelbuild_v22.wor.',
        eetError));
  finally
    CodeSite.ExitMethod('TCreateQCWorkspacesCommand.Execute');
  end;
end;

{ TBuildStudyAreaBoundaryCommand }

constructor TBuildStudyAreaBoundaryCommand.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel = nil; AProgressBar: TRzProgressBar = nil;
  AbortSignal: TEMGAATSCommandAbortSignal = nil);
begin
  inherited;
  fName := 'Build study area boundary';
  fDescription := 'Building study area boundary';
end;

procedure TBuildStudyAreaBoundaryCommand.Execute;
begin
  inherited;
  if not Enabled then
    Exit;

  CodeSite.EnterMethod('TBuildStudyAreaBoundaryCommand.Execute');
  try
    try
      if fModel.Config.HasDirectSubcatchments or
        fModel.Config.HasSurfaceSubcatchments then
        EMGWorkbenchManager.CreateStudyAreaBoundary;
    except on E: Exception do
      fModel.AddError(TEMGAATSError.Create('Could not create study area boundary. ' +
        'MapInfo Error: ' + E.Message, eetError));
    end;
  finally
    CodeSite.ExitMethod('TBuildStudyAreaBoundaryCommand.Execute');
  end;
end;

{ TCopyStandardWorkspacesCommand }

constructor TCopyStandardMapInfoWorkspacesCommand.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel; AProgressBar: TRzProgressBar;
  AbortSignal: TEMGAATSCommandAbortSignal);
begin
  inherited;
  fName := 'Copy standard MapInfo workspaces';
  fDescription := 'Copying standard MapInfo workspaces';
end;

procedure TCopyStandardMapInfoWorkspacesCommand.Execute;
begin
  inherited;
  if not Enabled then
    Exit;

  CodeSite.EnterMethod('TCopyStandardMapInfoWorkspacesCommand.Execute');
  try
    try
      if not CopyFileExtensionGroup('*.wor', SystemConfig.MasterFilesByName['wors'],
        IncludeTrailingPathDelimiter(fModel.Path) + 'wors') then
        fModel.AddError(TEMGAATSError.Create('Could not complete copying standard ' +
          'MapInfo workspaces', eetError));
    except on E: Exception do
      fModel.AddError(TEMGAATSError.Create('Could not complete copying standard ' +
        'MapInfo workspaces', eetError));
    end;
  finally
    CodeSite.ExitMethod('TCopyStandardMapInfoWorkspacesCommand.Execute');
  end;
end;

{ TCopyStandardMXDsCommand }

constructor TCopyStandardMXDsCommand.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel; AProgressBar: TRzProgressBar;
  AbortSignal: TEMGAATSCommandAbortSignal);
begin
  inherited;
  fName := 'Copy standard ArcGIS map documents';
  fDescription := 'Copying standard ArcGIS map documents';
end;

procedure TCopyStandardMXDsCommand.Execute;
begin
  inherited;
  if not Enabled then
    Exit;

  CodeSite.EnterMethod('TCopyStandardMXDsCommand.Execute');
  try
    try
      if not CopyFileExtensionGroup('*.mxd', SystemConfig.MasterFilesByName['mxds'],
        IncludeTrailingPathDelimiter(fModel.Path) + 'mxds') then
        fModel.AddError(TEMGAATSError.Create('Could not complete copying standard ' +
          'ArcGIS map documents', eetError));
    except on E: Exception do
      fModel.AddError(TEMGAATSError.Create('Could not complete copying standard ' +
        'ArcGIS map documents', eetError));
    end;
  finally
    CodeSite.ExitMethod('TCopyStandardMXDsCommand.Execute');
  end;
end;

{ TRunStormwaterControlsCommand }

constructor TRunAllStormwaterControlsCommand.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel; AProgressBar: TRzProgressBar;
  AbortSignal: TEMGAATSCommandAbortSignal);
begin
  inherited;
  fName := 'Run all stormwater controls alternatives calculations';
  fDescription := 'Running all stormwater controls alternatives calculations';
end;

procedure TRunAllStormwaterControlsCommand.Execute;
begin
  inherited;
  if not Enabled then
    Exit;

  CodeSite.EnterMethod('TRunAllStormwaterControlsCommand.Execute');
  try
    try
      MSAccessManager.OpenDatabase(fModel.Config.MasterMDBFilesByName['StormwaterControls']);
      MSAccessManager.Run('BuildInflowControls', ['All', False]);

      fModel.AddError(TEMGAATSError.Create('Import the IC_ALL_FAC.XPX file '+
        'into your XP model(s).', eetToDo));
    except on E: Exception do
      fModel.AddError(TEMGAATSError.Create('Could not complete BuildInflowControls command. ' +
        'MSAccess: ' + E.Message, eetError));
    end;
  finally
    CodeSite.ExitMethod('TRunAllStormwaterControlsCommand.Execute');
  end;
end;

{ TMergeStormwaterControlsCommand }

constructor TMergeStormwaterControlsCommand.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel; AProgressBar: TRzProgressBar;
  AbortSignal: TEMGAATSCommandAbortSignal);
begin
  inherited;
  fName := 'Merge stormwater controls calculations';
  fDescription := 'Merging stormwater controls calculations';
end;

procedure TMergeStormwaterControlsCommand.Execute;
var
  OriginalXPXFileName: TFileName;
  OriginalXPXStream: TFileStream;
  OriginalXPXTextStream: TStAnsiTextStream;
  ICXPXFileName: TFileName;
  ICXPXStream: TFileStream;
  ICXPXTextStream: TStAnsiTextStream;
  CurrentLine: String;

  procedure AppendXPX(AXPXFile: String);
  begin
    OriginalXPXFileName := fModel.Path + '\sim\XPExtran.xpx';
    ICXPXFileName := fModel.Path + '\sim\' + AXPXFile;
    if FileExists(OriginalXPXFileName) and FileExists(ICXPXFileName) then
    begin
      try
        OriginalXPXStream := TFileStream.Create(OriginalXPXFileName, fmOpenReadWrite
          or fmShareExclusive);
        OriginalXPXTextStream := TStAnsiTextStream.Create(OriginalXPXStream);
        ICXPXStream := TFileStream.Create(ICXPXFileName, fmOpenRead or fmShareDenyWrite);
        ICXPXTextStream := TStAnsiTextStream.Create(ICXPXStream);

        OriginalXPXTextStream.Seek(0, soFromEnd);
        while not ICXPXTextStream.AtEndOfStream do
        begin
          CurrentLine := ICXPXTextStream.ReadLine;
          OriginalXPXTextStream.WriteLine(CurrentLine);
        end;

      finally
        OriginalXPXTextStream.Free;
        OriginalXPXStream.Free;
        ICXPXTextStream.Free;
        ICXPXStream.Free;
      end;
    end
    else
    begin
      if not FileExists(OriginalXPXFileName) then
        fModel.AddError(TEMGAATSError.Create('Did not find ' + OriginalXPXFileName +
          ' to perform Stormwater Controls file merge', eetError));
      if not FileExists(ICXPXFileName) then
        fModel.AddError(TEMGAATSError.Create('Did not find ' + ICXPXFileName +
          ' to perform Stormwater Controls file merge', eetInfo));
    end;
  end;

begin
  inherited;
  if not Enabled then
    Exit;

  CodeSite.EnterMethod('TMergeStormwaterControlsCommand.Execute');
  try
    try
      AppendXPX('IC_ALL_FAC.xpx');
      AppendXPX('IC_CURB_FAC.xpx');
    except on E: Exception do
      fModel.AddError(TEMGAATSError.Create('Could not complete BuildInflowControls command. ' +
        'MSAccess: ' + E.Message, eetError));
    end;
  finally
    CodeSite.ExitMethod('TMergeStormwaterControlsCommand.Execute');
  end;
end;

{ TBuildStormwaterControlsCommand }

constructor TBuildAllStormwaterControlsCommand.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel; AProgressBar: TRzProgressBar;
  AbortSignal: TEMGAATSCommandAbortSignal);
begin
  inherited;
  fName := 'Build all stormwater controls';
  fDescription := 'Building all stormwater controls';
  Add(TRunAllStormwaterControlsCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCalculateHydrologyCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
end;

{ TCalculateHydraulicsCommand }

constructor TCalculateHydraulicsCommand.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel; AProgressBar: TRzProgressBar;
  AbortSignal: TEMGAATSCommandAbortSignal);
begin
  inherited;
  fName := 'Calculate pipe hydraulics';
  fDescription := 'Calculating pipe hydraulics';
end;

procedure TCalculateHydraulicsCommand.Execute;
var
  QueryText: String;
begin
  inherited;
  if not Enabled then
    Exit;

  CodeSite.EnterMethod('TCalculateHydraulicsCommand.Execute');

  MSAccessManager.OpenDatabase(IncludeTrailingPathDelimiter(fModel.Path) + 'links\mdl_links_ac.mdb');

  // Recalculate Qdes for each pipe (circulars only, assume n=0.013 for now)
  Application.ProcessMessages;
  if fAbortSignal.Abort then
    Exit;
  QueryText :=
    'UPDATE mdl_Links_ac SET mdl_Links_ac.Qdes = Iif(([USIE]<[DSIE]),' +
      '-1,0.464/0.013*([DiamWidth]/12)^(8/3)*(Abs([USIE]-[DSIE])/[Length])^(1/2)) ' +
    'WHERE (((mdl_Links_ac.PipeShape)="CIRC"));';
  MSAccessManager.RunQuery(QueryText);

  // Recalculate Qdes for noncircular pipes (set to -1)
  Application.ProcessMessages;
  if fAbortSignal.Abort then
    Exit;
  QueryText :=
    'UPDATE mdl_Links_ac SET mdl_Links_ac.Qdes = -1 ' +
    'WHERE (((mdl_Links_ac.PipeShape)<>"CIRC"));';
  MSAccessManager.RunQuery(QueryText);

  MSAccessManager.CloseDatabase;

  CodeSite.ExitMethod('TCalculateHydraulicsCommand.Execute');

end;

{ TCopyMasterDatabasesCommand }

constructor TCopyMasterDatabasesCommand.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel; AProgressBar: TRzProgressBar;
  AbortSignal: TEMGAATSCommandAbortSignal);
begin
  inherited;
  fName := 'Copy master code and lookup databases';
  fDescription := 'Copying master code and lookup databases';
end;

procedure TCopyMasterDatabasesCommand.Execute;
var
  i: Integer;
  CopySucceeded: Boolean;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TCopyMasterDatabasesCommand.Execute');

  fModel.Config.SetMDBRootToSystem;

  for i := 0 to fModel.Config.MasterMDBFilesCount - 1 do
  begin
    Application.ProcessMessages;
    if fAbortSignal.Abort then
      Exit;
    // skip empty paths, but provide warning
		if Length(fModel.Config.MasterMDBFiles[i]) = 0 then
    begin
      fModel.AddError(TEMGAATSError.Create('Missing filename for master MDB ' +
        fModel.Config.MasterMDBFileIDs[i], eetError));
      Continue;
    end;

    // skip commented paths; otherwise check if file exists
 		if LeftStr(fModel.Config.MasterMDBFileIDs[i],1) = '#' then
			Continue
		else
		begin
      if fModel.Config.MasterMDBFileIDs[i] = 'root' then
        Continue;
			if not FileExists(fModel.Config.MasterMDBFiles[i]) then
			begin
        // if location/file can't be found, warn user and comment out the
        // database in the config
        fModel.AddError(TEMGAATSError.Create(Format('Master MDB file not found: %s; path=%s',
          [fModel.Config.MasterMDBFileIDs[i], fModel.Config.MasterMDBFiles[i]]), eetWarning));
        fModel.AddError(TEMGAATSError.Create('Check the ' + SystemConfig.MDBsByName['root'] +
          ' directory and ensure the ' + ExtractFileName(fModel.Config.MasterMDBFiles[i]) +
          ' file exists. If it exists, ensure the file is not locked and then try rerunning ' +
          'your build again.', eetHint));
        fModel.Config.CommentOutMasterMDB(i);
				Continue;
			end;
		end;

    // Check if ID matches filename
    // AMM 1/23/2007 Removed check as it doesn't really make sense
{    if UpperCase(fModel.Config.MasterMDBFileIDs[i]) <>
      UpperCase(JustNameL(fModel.Config.MasterMDBFiles[i])) then
    begin
      fModel.AddError(TEMGAATSError.Create(Format('Master MDB file doesn''t match config key: %s; path=%s',
        [fModel.Config.MasterMDBFileIDs[i], fModel.Config.MasterMDBFiles[i]]), eetWarning));
    end;}

		// Copy over the mdbs
    DeleteFile(IncludeTrailingPathDelimiter(fModel.Config.Path) + 'mdbs\' +
      fModel.Config.MasterMDBFileIDs[i] + '.mdb');
//    CodeSite.Send('Copying ' + fModel.Config.MasterMDBFiles[i]);
    CopySucceeded := CopyFile(fModel.Config.MasterMDBFiles[i],
      IncludeTrailingPathDelimiter(fModel.Config.Path) + 'mdbs\' +
      JustNameL(fModel.Config.MasterMDBFiles[i]) + '.mdb');
    if not CopySucceeded then
    begin
      fModel.AddError(TEMGAATSError.Create(Format('Master MDB file could not be copied: %s; path=%s',
        [fModel.Config.MasterMDBFileIDs[i], fModel.Config.MasterMDBFiles[i]]), eetError));
    end;
//    else
//      CodeSite.Send('Copied ' + fModel.Config.MasterMDBFiles[i]);
  end;

  fModel.Config.SetMDBRootToModel;

  fModel.Config.SystemMDBRefreshDate := Now;
  fModel.Config.Update;

  CodeSite.ExitMethod('TCopyMasterDatabasesCommand.Execute');
end;

{ TCopyMasterICAltDatabasesCommand }

constructor TCopyMasterICAltDatabasesCommand.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel; AProgressBar: TRzProgressBar;
  AbortSignal: TEMGAATSCommandAbortSignal);
begin
  inherited;
  fName := 'Copy master inflow control alternative databases';
  fDescription := 'Copying master inflow control alternative databases';
end;

procedure TCopyMasterICAltDatabasesCommand.Execute;
var
  ClearBuildModelICText: string;
begin
  inherited;
  if not fEnabled then
    Exit;

  CodeSite.EnterMethod('TCopyMasterICAltDatabasesCommand.Execute');

  CopyDir(SystemConfig.MasterFilesByName['icalt'], fModel.Path + 'icalt');

  // Reset BuildICModel fields for all target tables to False (0) since
  // that's what's expected
  MSAccessManager.OpenDatabase(fModel.Path + 'icalt\mst_ic_ParkingTargets_ac.mdb');
  ClearBuildModelICText := 'UPDATE mst_ic_ParkingTargets_ac SET mst_ic_ParkingTargets_ac.BuildModelIC = 0;';
  MSAccessManager.RunQuery(ClearBuildModelICText);

  MSAccessManager.OpenDatabase(fModel.Path + 'icalt\mst_ic_RoofTargets_ac.mdb');
  ClearBuildModelICText := 'UPDATE mst_ic_RoofTargets_ac SET mst_ic_RoofTargets_ac.BuildModelIC = 0;';
  MSAccessManager.RunQuery(ClearBuildModelICText);

  MSAccessManager.OpenDatabase(fModel.Path + 'icalt\mst_ic_StreetTargets_ac.mdb');
  ClearBuildModelICText := 'UPDATE mst_ic_StreetTargets_ac SET mst_ic_StreetTargets_ac.BuildModelIC = 0;';
  MSAccessManager.RunQuery(ClearBuildModelICText);

  MSAccessManager.CloseDatabase;

  CodeSite.ExitMethod('TCopyMasterICAltDatabasesCommand.Execute');
end;

{ TBuildStreetStormwaterControlsCommand }

constructor TBuildStreetStormwaterControlsCommand.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel; AProgressBar: TRzProgressBar;
  AbortSignal: TEMGAATSCommandAbortSignal);
begin
  inherited;
  fName := 'Build street stormwater controls';
  fDescription := 'Building street stormwater controls';
  Add(TRunStreetStormwaterControlsCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
  Add(TCalculateHydrologyCommand.Create(AModel, AProgressLabel, AProgressBar, AbortSignal));
end;

{ TRunStreetStormwaterControlsCommand }

constructor TRunStreetStormwaterControlsCommand.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel; AProgressBar: TRzProgressBar;
  AbortSignal: TEMGAATSCommandAbortSignal);
begin
  inherited;
  fName := 'Run street stormwater controls alternatives calculations';
  fDescription := 'Running street stormwater controls alternatives calculations';
end;

procedure TRunStreetStormwaterControlsCommand.Execute;
begin
  inherited;
  if not Enabled then
    Exit;

  CodeSite.EnterMethod('TRunStreetStormwaterControlsCommand.Execute');
  try
    try
      MSAccessManager.OpenDatabase(fModel.Config.MasterMDBFilesByName['StormwaterControls']);
      MSAccessManager.Run('BuildInflowControls', ['Curb', False]);

      fModel.AddError(TEMGAATSError.Create('Import the IC_CURB_FAC.XPX file '+
        'into your XP model(s).', eetToDo));

    except on E: Exception do
      fModel.AddError(TEMGAATSError.Create('Could not complete BuildInflowControls command. ' +
        'MSAccess: ' + E.Message, eetError));
    end;
  finally
    CodeSite.ExitMethod('TRunStreetStormwaterControlsCommand.Execute');
  end;
end;

{ TRemoveRunoffFilesCommand }

constructor TRemoveRunoffFilesCommand.Create(AModel: TEMGAATSModel;
  AProgressLabel: TLabel; AProgressBar: TRzProgressBar;
  AbortSignal: TEMGAATSCommandAbortSignal);
begin
  inherited;
  fName := 'Remove runoff files';
  fDescription := 'Removing runoff files';
end;

procedure TRemoveRunoffFilesCommand.Execute;
var
  RunoffFileName: TFileName;
  RunoffInterfaceFileName: TFileName;
  i: Integer;
begin
  inherited;
  if not Enabled then
    Exit;

  CodeSite.EnterMethod('TRemoveRunoffFilesCommand.Execute');
  try
    for i := 0 to fModel.Config.StormsToBuildCount - 1 do
    begin
      RunoffFileName := fModel.Path + 'sim\' + fModel.Config.StormsToBuild[i] + '\' +
        fModel.Config.RunoffFileName;
      if FileExists(RunoffFileName) then
      begin
        DeleteFile(RunoffFileName);
        fModel.AddError(TEMGAATSError.Create('Runoff file for storm ' +
          fModel.Config.StormsToBuild[i] + ' was deleted.', eetWarning));
        RunoffInterfaceFileName := fModel.Path + 'sim\' + fModel.Config.StormsToBuild[i] + '\' +
          JustNameL(fModel.Config.RunoffFileName) + '.int';
        if FileExists(RunoffInterfaceFileName) then
        begin
          DeleteFile(RunoffInterfaceFileName);
          fModel.AddError(TEMGAATSError.Create('Runoff interface file for storm ' +
            fModel.Config.StormsToBuild[i] + ' was deleted.', eetWarning));
          fModel.AddError(TEMGAATSError.Create('Rerun runoff for ' +
            fModel.Config.StormsToBuild[i] + ' to get interface file for current hydrology.',
            eetToDo));
        end
      end;

    end;
  finally
    CodeSite.ExitMethod('TRemoveRunoffFilesCommand.Execute');
  end;
end;

end.
