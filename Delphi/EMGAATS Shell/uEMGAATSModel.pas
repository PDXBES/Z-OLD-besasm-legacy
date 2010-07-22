unit uEMGAATSModel;

interface

uses Windows, SysUtils, Classes, uEMGAATSModelConfig, RzErrHnd, uEMGAATSTypes, StdCtrls,
  RzPrgres;

const
  RunoffLines : array[0..13] of string = (
  '@del runoff.out',
  '@del runoff.int',
  '@del transport.out',
  '@del transport.int',
  '',
  '@echo runoff.rdt > in.ro',
  '@echo runoff.out >> in.ro',
  '',
  '',
  '',
  '',
  '',
	'\\cassio\modeling\model_programs\swmm\xswmm95y2k < in.ro',
  '');

  TransportLines : array[0..13] of string = (
  '@del runoff.out',
  '@del runoff.int',
  '@del transport.out',
  '@del transport.int',
  '',
  '@echo runoff.rdt > in.ro',
  '@echo runoff.out >> in.ro',
  '',
  '@echo transport.tdt > in.tr',
  '@echo transport.out >> in.tr',
  '',
	'\\cassio\modeling\model_programs\swmm\xswmm95y2k < in.ro',
	'\\cassio\modeling\model_programs\swmm\trans95a < in.tr',
  '');

type
  TEMGAATSModelStats = record
    NumLinks: Integer;
    NumNodes: Integer;
    NumDSCs: Integer;
    NumSurfSCs: Integer;
    NumConstructedRoofTargets: Integer;
    NumConstructedParkingTargets: Integer;
    NumConstructedStreetTargets: Integer;
    NumBuildRoofTargets: Integer;
    NumBuildParkingTargets: Integer;
    NumBuildStreetTargets: Integer;
  end;

  TEMGAATSModel = class
  private
    fConfig: TModelConfig;
    fPath: String;
    fName: String;
    fDescription: String;
    fIsDirty: Boolean;
//    fErrHandler: TRzErrorHandler;
    fErrLog: TStringList;
    fModelStats: TEMGAATSModelStats;
    fSilent: Boolean;
    fTimeFrame: TTimeFrame;
    fProgressLabel: TLabel;
    fProgressBar: TRzProgressBar;
    procedure SetPath(const Value: String);
    procedure SetSilent(const Value: Boolean);
    function CreateNameValuePair(AString: String): String;
    function GetMDB(Index: String): String;
    function GetQCWorkbookFileName: String;
    function GetMapFileName(Index: String): String;
  public
    // Creates/Opens an existing model
    constructor Create(APath: String; IsNewModel: Boolean = False);
    destructor Destroy; override;

    // Builds the entire model
    procedure Build;

    // Doesn't recreate the directory
    procedure BuildOverExistingModel;

    // Model build parts
    procedure BuildEmptyModel;
    procedure ConfigureModelForBuilding;
    procedure BuildNetwork;
    procedure BuildDirectSubcatchments;
    procedure BuildSurfaceSubcatchments;
    procedure CalculateHydrology;
    procedure RemoveNetwork;

    // Deployment parts
    procedure DeployRunoffFile;
    procedure CreateModelBatchFile(Block: String);
    procedure CreateReconciliationSpreadsheet;
    procedure DeployEngineFile;

    // Model boundary assignment routines
    procedure AssignRoots(AList: TStrings);
    procedure AssignStops(AList: TStrings);

    // Dirty flag (indicates if model was changed in session)
    procedure MakeDirty;
    // Clean up file (make it not dirty)
    procedure Update;

    // Model creation tasks
    procedure CreateModelConfigFile;
    procedure CreateStandardDirectories;
    procedure CopyMasterTemplate;
    procedure CreateEmptyModelData;
    procedure DeleteModelMDB(AFileName: TFileName);
    procedure CreateDataAccessFile;

    // Model open tasks
    procedure OpenModelAndMasterData;
    procedure CheckModelLocation;
    procedure RelinkModelData;

    // Error log routines
    procedure ClearErrorLog;
    procedure AddError(AError: TEMGAATSError);
    procedure TransferErrors(AList: TStrings);

    // Model Statistics
    procedure GetModelStats;
    procedure TransferStats(AList: TStrings);

    // Location of model
    property Path: String read fPath write SetPath;

    // Flag whether to show messages
    property Silent: Boolean read fSilent write SetSilent;

    // TimeFrame of model
    property TimeFrame: TTimeFrame read fTimeFrame;

    // Progress info
    property ProgressLabel: TLabel read fProgressLabel write fProgressLabel;
    property ProgressBar: TRzProgressBar read fProgressBar write fProgressBar;
    procedure ShowProgressMessage(AMessage: String);
    procedure SetProgressParts(NumSteps: Integer);
    procedure IncProgress;

    // Copy trace params from another model
    procedure CopyBoundariesFromModel(AModelINIFile: TFileName);

    // Model information
    procedure GetRootLinks(AList: TStrings; AErrHandler: TRzErrorHandler = nil);
    procedure GetStopLinks(AList: TStrings; AErrHandler: TRzErrorHandler = nil);
    procedure GetForcedRootLinks(AList: TStrings; AErrHandler: TRzErrorHandler = nil);
    procedure GetForcedStopLinks(AList: TStrings; AErrHandler: TRzErrorHandler = nil);
    property MDBs[Index: String]: String read GetMDB;
    property MapFileNames[Index: String]: String read GetMapFileName;
    property QCWorkbookFileName: String read GetQCWorkbookFileName;
    property Config: TModelConfig read fConfig;
  end;

var
  CurrentModel: TEMGAATSModel;

implementation

uses uEMGAATSSystemConfig, StStrL, Forms, StrUtils, uUtilities,
  uEMGWorkbenchManager, uMSAccessManager, ADOX_TLB, uEMGAATSModelTemplateConfig,
  dmTracer, Dialogs, IniFiles, StStrms, dmHydroStats, GlobalConstants,
  fBuildReport, fMain;

  { TEMGAATSModel }

procedure TEMGAATSModel.AddError(AError: TEMGAATSError);
begin
  fErrLog.AddObject(AError.Msg, AError);
end;

procedure TEMGAATSModel.AssignRoots(AList: TStrings);
var
  i: Integer;
  HashedRoots: THashedStringList;
  RootToAdd: Integer;
begin
  HashedRoots := THashedStringList.Create;
  try
    // Process the Strings list if strings don't have an =
    HashedRoots.AddStrings(AList);
    for i := 0 to HashedRoots.Count - 1 do
      HashedRoots[i] := CreateNameValuePair(HashedRoots[i]);

    fConfig.ClearRootLinks;
    for i := 0 to AList.Count - 1 do
      if not TryStrToInt(HashedRoots.Names[i], RootToAdd) then
        AddError(TEMGAATSError.Create(
          Format('Could not add root link "%s"; check format: must be an integer',
            [HashedRoots.Names[i]]), eetError))
      else
        fConfig.AddRootLink(RootToAdd, HashedRoots.Values[HashedRoots.Names[i]]);
  finally
    HashedRoots.Free;
  end;
  fConfig.Update;
end;

procedure TEMGAATSModel.AssignStops(AList: TStrings);
var
  i: Integer;
  HashedStops: THashedStringList;
  StopToAdd: Integer;
begin
  HashedStops := THashedStringList.Create;
  try
    // Process the Strings list if strings don't have an =
    HashedStops.AddStrings(AList);
    for i := 0 to HashedStops.Count - 1 do
      HashedStops[i] := CreateNameValuePair(HashedStops[i]);

    fConfig.ClearStopLinks;
    for i := 0 to AList.Count - 1 do
    begin
      if not TryStrToInt(HashedStops.Names[i], StopToAdd) then
        AddError(TEMGAATSError.Create(
          Format('Could not add stop link "%s"; check format: must be an integer',
            [HashedStops.Names[i]]), eetError))
      else
        fConfig.AddStopLink(StopToAdd, HashedStops.Values[HashedStops.Names[i]]);
    end;
  finally
    HashedStops.Free;
  end;
  fConfig.Update;
end;

procedure TEMGAATSModel.Build;
begin
  SetProgressParts(19);
  BuildEmptyModel;
  ConfigureModelForBuilding;
  BuildNetwork;
  BuildDirectSubcatchments;
  BuildSurfaceSubcatchments;
  CalculateHydrology;
  DeployRunoffFile;
  DeployEngineFile;
end;

procedure TEMGAATSModel.BuildDirectSubcatchments;
begin
  IncProgress;
  ShowProgressMessage('Building direct subcatchments');

  if fConfig.HasNetwork then
  begin
    EMGWorkbenchManager.TraceParcels;

    // Bring in the ICs
    MSAccessManager.OpenDatabase(fConfig.MasterMDBFilesByName['HydInitDSC']);
    MSAccessManager.Run('executequerytable', ['listexecuteInitDSC', 'Block' , 'TDSC_ALL' , 0]);

    // Initialize the DSC hydrology
    if fConfig.TimeFrame = tfFuture then
    begin
      MSAccessManager.Run('setFBfactoredbaseflow', [0.5]);
      MSAccessManager.Run('executequerytable', ['listexecuteInitDSC', 'Block' , 'TDSC_FB' , 0]);
    end;
  end
  else
    AddError(TEMGAATSError.Create('Network not available.  Cannot build direct subcatchments',
     eetError));
end;

//procedure TEMGAATSModel.BuildPart(ModelPart: IModelPart);
//begin
//  ModelPart.Build;
//end;

procedure TEMGAATSModel.BuildEmptyModel;
begin
  IncProgress;
  ShowProgressMessage('Creating standard directories');
  CreateStandardDirectories;
  // Copy over master tables, lookups, maps/workspaces, files
  IncProgress;
  ShowProgressMessage('Copying master template');
  CopyMasterTemplate;
  // Create model tables (standard + inflow control)
  IncProgress;
  ShowProgressMessage('Creating skeleton model');
  CreateEmptyModelData;

  // Open model and master tables
  IncProgress;
  ShowProgressMessage('Opening model and master data');
  OpenModelAndMasterData;
  // Notify if model location was changed from previous session (i.e. move/copy)
  IncProgress;
  ShowProgressMessage('Checking model location');
  CheckModelLocation;
  // Relink Access MDB files
  IncProgress;
  ShowProgressMessage('Relinking model databases');
  RelinkModelData;
end;

procedure TEMGAATSModel.BuildNetwork;
begin
  // Trace the network
  IncProgress;
  ShowProgressMessage('Building network');
  datmodTracer.TraceNetwork(fConfig, Self);

  // Check to continue if necessary

  // Build mappable pipes
  IncProgress;
  ShowProgressMessage('Creating mappable pipes');
  EMGWorkbenchManager.CreateMappablePipes;
  // Assemble model special links
  IncProgress;
  ShowProgressMessage('Getting special links');
  MSAccessManager.OpenDatabase(fConfig.MasterMDBFilesByName['ModelAssemble']);
  EMGWorkbenchManager.GetSpecialLinksData;
end;

procedure TEMGAATSModel.BuildOverExistingModel;
begin
  SetProgressParts(13);
  ConfigureModelForBuilding;
  BuildNetwork;
  BuildDirectSubcatchments;
  BuildSurfaceSubcatchments;
  CalculateHydrology;
  DeployRunoffFile;
  DeployEngineFile;
end;

procedure TEMGAATSModel.BuildSurfaceSubcatchments;
begin
  IncProgress;
  ShowProgressMessage('Building surface subcatchments');

  if fConfig.HasNetwork then
  begin
    EMGWorkbenchManager.TraceSurfaceSubcatchments;
    EMGWorkbenchManager.CreateSurfaceSubcatchmentPointers;
  end
  else
    AddError(TEMGAATSError.Create('Network not available.  Cannot build surface subcatchments',
      eetError));
end;

procedure TEMGAATSModel.CalculateHydrology;
begin
  IncProgress;
  ShowProgressMessage('Calculating total IC area for parcels');

  if fConfig.HasDirectSubcatchments then
  begin
    // IC Calculations
    MSAccessManager.OpenDatabase(fConfig.MasterMDBFilesByName['HydInitDSC']);
    MSAccessManager.Run('ExecuteQueryTable', ['listexecuteInitDSC', 'Block', 'INIT_HYD', 0]);

    // May need to sleep(5000) here

    IncProgress;
    ShowProgressMessage('Applying virtual gages');
    EMGWorkbenchManager.RunCommand('Add Column ' + #34 + 'mdl_nodes' + #34 +
      ' (GageID ) From mst_Quartersections Set To QS Where contains');
    EMGWorkbenchManager.RunCommand('commit table mdl_nodes');

    IncProgress;
    ShowProgressMessage('Performing direct and surface subcatchment GIS queries for hydrology');
    EMGWorkbenchManager.RelateDSCtoSurfSC;

    IncProgress;
    ShowProgressMessage('Checking for duplicate direct subcatchments');
    EMGWorkbenchManager.CheckForDuplicateDSCs;

    IncProgress;
    ShowProgressMessage('Checking for excessive IC areas');
    EMGWorkbenchManager.CheckForExcessiveICAreas;;
  end
  else
    AddError(TEMGAATSError.Create('Direct subcatchments not available.  Cannot' +
      'calculate hydrology', eetError));

end;

procedure TEMGAATSModel.CheckModelLocation;
var
  PathParts: TStringList;
  CurrentModelName: string;
begin
  if fConfig.ModelName <> fConfig.Path then
  PathParts := TStringList.Create;
  try
    ExtractTokensL(fConfig.Path, '\', '''', False, PathParts);
    CurrentModelName := PathParts[PathParts.Count-1];
  finally
    PathParts.Free;
  end;

  if UpperCase(CurrentModelName) <> UpperCase(fConfig.ModelName) then
  begin
//    fErrHandler.Add(Format('This model was previously opened as %s', [fConfig.ModelName]),
//      etWarning);
    AddError(TEMGAATSError.Create(Format('This model was previously opened as %s', [fConfig.ModelName]),
      eetInfo));
  end;
end;

procedure TEMGAATSModel.ClearErrorLog;
begin
  fErrLog.Clear;
end;

procedure TEMGAATSModel.ConfigureModelForBuilding;
begin
  // Basic treeverse parameters
  if fConfig.TraceFileName = '' then
    fConfig.TraceFileName := IncludeTrailingPathDelimiter(fConfig.Path) +
      'mdltrace.txt';

  fConfig.TraceSourceDatabase := IncludeTrailingPathDelimiter(SystemConfig.MDBsByName['root']) +
    SystemConfig.MDBsByName['TraceSourceDatabase'];

  if fConfig.TraceSourceTable = '' then
    fConfig.TraceSourceTable := 'mst_links_existing';
  if fConfig.LinkField = '' then
    fConfig.LinkField := 'MLinkID';
  if fConfig.USNodeField = '' then
    fConfig.USNodeField := 'USNode';
  if fConfig.DSNodeField = '' then
    fConfig.DSNodeField := 'DSNode';
  if fConfig.ReachField = '' then
    fConfig.ReachField := 'LinkReach';
  if fConfig.ElementField = '' then
    fConfig.ElementField := 'ReachElement';
  if fConfig.DebugFileName = '' then
    fConfig.DebugFileName := IncludeTrailingPathDelimiter(GetApplicationUserConfigPath) +
      'tracedebug.txt';
  if fConfig.LinkExistingTable = '' then
    fConfig.LinkExistingTable := 'mst_links_existing';
  if fConfig.LinkFutureTable = '' then
    fConfig.LinkFutureTable := 'mst_links_future';

  fConfig.Update;
end;

procedure TEMGAATSModel.CreateReconciliationSpreadsheet;
begin
  if fConfig.HasDirectSubcatchments and fConfig.HasSurfaceSubcatchments then
  begin
    datmodHydroStats.Initialize(Self);
    datmodHydroStats.Calculate(GetQCWorkbookFileName);

    // Send this to the report page
    try
      AddError(TEMGAATSError.Create('Reconciliation checks', eetInfo));
      AddError(TEMGAATSError.Create(Format('Surface subcatchments total area: %.2f',
        [Double(datmodHydroStats.csHCardTA)]), eetInfo));
      AddError(TEMGAATSError.Create(Format('Surface subcatchments impervious area: %.2f',
        [Double(datmodHydroStats.csHCardIA)]), eetInfo));
      AddError(TEMGAATSError.Create(Format('Direct subcatchments total area: %.2f',
        [Double(datmodHydroStats.csdscTA)]), eetInfo));
      AddError(TEMGAATSError.Create(Format('Direct subcatchments impervious area: %.2f',
        [Double(datmodHydroStats.csdscTA)]), eetInfo));
      AddError(TEMGAATSError.Create('Hydrologic quality control: ' +
        fConfig.HydroQCFileName + ' created.', eetInfo));

      if (Abs(double(datmodHydroStats.csHCardTA)) > RSSwarn) or
        (Abs(double(datmodHydroStats.csHCardIA)) > RSSwarn) or
        (Abs(double(datmodHydroStats.csdscTA)) > RSSwarn) or
        (Abs(double(datmodHydroStats.csdscIA)) > RSSwarn) then
        AddError(TEMGAATSError.Create(Format('One or more reconciliation checks exceed %.2f threshold.',
          [RSSWarn]), eetError))
      else
        AddError(TEMGAATSError.Create(Format('All reconciliation checks within %.2f threshold.',
          [RSSWarn]), eetInfo));

    except
      AddError(TEMGAATSError.Create('One or more reconciliation checks failed.', eetError));
      AddError(TEMGAATSError.Create('Hydrologic quality control: ' + fConfig.HydroQCFileName +
        ' created.', eetInfo));
    end;
  end
  else
  begin
    if not fConfig.HasDirectSubcatchments then
      AddError(TEMGAATSError.Create('Cannot create reconciliation spreadsheet. ' +
        'Direct subcatchments were not built.', eetError));
    if not fConfig.HasSurfaceSubcatchments then
      AddError(TEMGAATSError.Create('Cannot create reconciliation spreadsheet. ' +
        'Surface subcatchments were not built.', eetError));
  end;
end;

procedure TEMGAATSModel.CopyBoundariesFromModel(AModelINIFile: TFileName);
begin
  fConfig.CopyBoundariesFromModel(AModelIniFile);
end;

procedure TEMGAATSModel.CopyMasterTemplate;
var
  i: Integer;
  CopySucceeded: Boolean;
begin
  for i := 0 to fConfig.MasterMDBFilesCount - 1 do
  begin
    // skip empty paths, but provide warning
		if Length(fConfig.MasterMDBFiles[i]) = 0 then
    begin
//      fErrHandler.Add('Missing filename for master MDB ' + fConfig.MasterMDBFileIDs[i],
//        etCritical);
      AddError(TEMGAATSError.Create('Missing filename for master MDB ' +
        fConfig.MasterMDBFileIDs[i], eetError));
      Continue;
    end;

    // skip commented paths; otherwise check if file exists
 		if LeftStr(fConfig.MasterMDBFileIDs[i],1) = '#' then
			Continue
		else
		begin
      if fConfig.MasterMDBFileIDs[i] = 'root' then
        Continue;
			if not FileExists(fConfig.MasterMDBFiles[i]) then
			begin
        // if location/file can't be found, warn user and comment out the
        // database in the config
//				fErrHandler.Add(Format('Master MDB file not found: %s; path=%s',
//          [fConfig.MasterMDBFileIDs[i], fConfig.MasterMDBFiles[i]]), etWarning);
        AddError(TEMGAATSError.Create(Format('Master MDB file not found: %s; path=%s',
          [fConfig.MasterMDBFileIDs[i], fConfig.MasterMDBFiles[i]]), eetWarning));
        fConfig.CommentOutMasterMDB(i);
				Continue;
			end;
		end;

    // Check if ID matches filename
    if UpperCase(fConfig.MasterMDBFileIDs[i]) <>
      UpperCase(JustNameL(fConfig.MasterMDBFiles[i])) then
    begin
//      fErrHandler.Add(Format('Master MDB file doesn''t match config key: %s; path=%s',
//        [fConfig.MasterMDBFileIDs[i], fConfig.MasterMDBFiles[i]]), etWarning);
      AddError(TEMGAATSError.Create(Format('Master MDB file doesn''t match config key: %s; path=%s',
        [fConfig.MasterMDBFileIDs[i], fConfig.MasterMDBFiles[i]]), eetWarning));
    end;

		// Copy over the mdbs
    DeleteFile(IncludeTrailingPathDelimiter(fConfig.Path) + 'mdbs\' +
      fConfig.MasterMDBFileIDs[i] + '.mdb');
    CopySucceeded := CopyFile(fConfig.MasterMDBFiles[i],
      IncludeTrailingPathDelimiter(fConfig.Path) + 'mdbs\' +
      JustNameL(fConfig.MasterMDBFiles[i]) + '.mdb');
    if not CopySucceeded then
    begin
//      fErrHandler.Add(Format('Master MDB file could not be copied: %s; path=%s',
//        [fConfig.MasterMDBFileIDs[i], fConfig.MasterMDBFiles[i]]), etCritical);
      AddError(TEMGAATSError.Create(Format('Master MDB file could not be copied: %s; path=%s',
        [fConfig.MasterMDBFileIDs[i], fConfig.MasterMDBFiles[i]]), eetError));
    end;

  end;
  fConfig.SetMDBRootToModel
  // Write out copy time to config file
//  modelini.WriteString('Admin','mdbdate',DateTimeToStr(Date + Time));
end;

procedure TEMGAATSModel.CreateEmptyModelData;
var
  ICDirectoryPath: String;
  CommandText: String;
  MDBsPath: String;
  ModelPath: String;
begin
  // Create empty base model tables (links, nodes, dscs, surfscs0
  try
    EMGWorkBenchManager.CreateModelTables;
  except on E: Exception do
    AddError(TEMGAATSError.Create('Could not create base model tables. ' +
      'MapInfo error: ' + E.Message, eetError));
  end;

  // Create IC Tables (Delete first if they exist!)
  ICDirectoryPath := IncludeTrailingPathDelimiter(Path) + 'ic\';
  DeleteModelMDB(ICDirectoryPath + 'mdl_ic_discoveg_ac.mdb');
  DeleteModelMDB(ICDirectoryPath + 'mdl_ic_drywell_ac.mdb');
  DeleteModelMDB(ICDirectoryPath + 'mdl_ic_grnroof_ac.mdb');
  DeleteModelMDB(ICDirectoryPath + 'mdl_ic_infilt_ac.mdb');
  DeleteModelMDB(ICDirectoryPath + 'mdl_ic_swplnt_ac.mdb');
  DeleteModelMDB(ICDirectoryPath + 'mdl_ic_store_ac.mdb');
  DeleteModelMDB(ICDirectoryPath + 'mdl_ic_ngtoredir_ac.mdb');
  DeleteModelMDB(IncludeTrailingPathDelimiter(Path) + 'mdbs\DataAccess.mdb');

  // DiscoVeg
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
    AddError(TEMGAATSError.Create('Error in creating mdl_ic_discoveg_ac table. ' +
      'Access error: ' + E.Message, eetError));
  end;

  // DryWell
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
    AddError(TEMGAATSError.Create('Error in creating mdl_ic_drywell_ac table. ' +
      'Access error: ' + E.Message, eetError));
  end;

  // GreenRoof
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
    AddError(TEMGAATSError.Create('Error in creating mdl_ic_grnroof_ac table. ' +
      'Access error: ' + E.Message, eetError));
  end;

  // Infiltration
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
    AddError(TEMGAATSError.Create('Error in creating mdl_ic_infilt_ac table. ' +
      'Access error: ' + E.Message, eetError));
  end;

  // Stormwater Planters
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
    AddError(TEMGAATSError.Create('Error in creating mdl_ic_swplnt_ac table. ' +
      'Access error: ' + E.Message, eetError));
  end;

  // Storage
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
    AddError(TEMGAATSError.Create('Error in creating mdl_ic_store_ac table. ' +
      'Access error: ' + E.Message, eetError));
  end;

  // NGTORedir
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
    AddError(TEMGAATSError.Create('Error in creating mdl_ic_ngtoredir_ac table. ' +
      'Access error: ' + E.Message, eetError));
  end;

  // Create Data Access file
  try
    ModelPath := IncludeTrailingPathDelimiter(fConfig.Path);
    MDBsPath := IncludeTrailingPathDelimiter(Path) + 'mdbs\';
    MSAccessManager.CreateDatabase(MDBsPath + 'DataAccess.mdb');

    MSAccessManager.Link('mdl_links', 'mdl_links_ac', ModelPath + 'links\mdl_links_ac.mdb');
    MSAccessManager.Link('mdl_nodes', 'mdl_nodes_ac', ModelPath + 'nodes\mdl_nodes_ac.mdb');
    MSAccessManager.Link('mdl_dirsc', 'mdl_dirsc_ac', ModelPath + 'dsc\mdl_dirsc_ac.mdb');
    MSAccessManager.Link('mdl_surfsc', 'mdl_surfsc_ac', ModelPath + 'surfsc\mdl_surfsc_ac.mdb');
    MSAccessManager.Link('mdl_speclinks', 'mdl_speclinks_ac', ModelPath + 'links\mdl_speclinks_ac.mdb');
    MSAccessManager.Link('mdl_speclinkdata', 'mdl_speclinkdata_ac', ModelPath + 'links\mdl_speclinkdata_ac.mdb');
  except on E: Exception do
    AddError(TEMGAATSError.Create('Error in creating Data Access database. ' +
      'Access error: ' + E.Message, eetError));
  end;

end;

procedure TEMGAATSModel.CreateModelBatchFile(Block: String);
var
  OutputFileStream: TFileStream;
  OutputFile: TStAnsiTextStream;
  OutputFileName: String;
  i : integer;
  OutputLine: String;
begin
  try
    OutputFileName := IncludeTrailingPathDelimiter(fConfig.Path) + 'sim\pdxrun.bat';
    OutputFileStream := TFileStream.Create(OutputFileName, fmCreate);
    OutputFile := TStAnsiTextStream.Create(OutputFileStream);
    try
      if Uppercase(Block) = 'RUNOFF' then
      begin
        for i := 0 to High(RunoffLines) do
        begin
          OutputLine := RunoffLines[i];
          OutputFile.WriteLine(OutputLine);
        end;
      end;
    finally
      OutputFile.Free;
      OutputFileStream.Free;
    end;
  except on E: Exception do
    AddError(TEMGAATSError.Create('Error creating batch file for block ' + Block + '. ' +
      'Error: ' + E.Message, eetError))
  end;
end;

procedure TEMGAATSModel.CreateModelConfigFile;
begin
  // Copy in the following sections from the SystemConfig
  //   Admin, MasterFiles, MDBs, Code, StandardDirectories, DataAccess

  // Copy in standard sections from the ModelTemplate
  fConfig.CopyConfigFromSystem;
  fConfig.CopyConfigFromModelTemplate;
  fConfig.Update;

  // Copy in the following sections from the UserConfig
  //   Simulation
end;

function TEMGAATSModel.CreateNameValuePair(AString: String): String;
begin
  if Pos('=', AString) = 0 then
    Result := AString + '='
  else
    Result := AString;
end;

procedure TEMGAATSModel.CreateStandardDirectories;
var
  i: Integer;
  PathToCreate: String;
begin
  // Standard directories are specified in the EMGAATS SystemConfig file
  for i := 0 to SystemConfig.StandardDirectoryCount - 1 do
  begin
    PathToCreate := IncludeTrailingPathDelimiter(fPath) + SystemConfig.StandardDirectoriesID[i];
    if not DirectoryExists(PathToCreate) then
      if not CreateDir(PathToCreate) then
      begin
        AddError(TEMGAATSError.Create('Could not create standard directory ' + PathToCreate,
          eetError));
      end;
  end;
end;

constructor TEMGAATSModel.Create(APath: String; IsNewModel: Boolean = false);
begin

  if not DirectoryExists(APath) then
    ForceDirectories(APath);

  fPath := IncludeTrailingPathDelimiter(APath);
  fConfig := TModelConfig.Create(fPath + 'model.ini');

  // If this is a new model, create the full config file
  if IsNewModel then
    CreateModelConfigFile;

//  fErrHandler := TRzErrorHandler.Create(Application);
  fErrLog := TStringList.Create;

  { TODO -oAMM : Need to determine time frame up-front }
  fTimeFrame := fConfig.TimeFrame;

  EMGWorkbenchManager.SetModel(Self);

  ClearErrorLog;

end;

procedure TEMGAATSModel.CreateDataAccessFile;
var
  DataAccessFileName: TFileName;
  i: Integer;
  Tokens: TStringList;
begin
  DataAccessFileName := SystemConfig.DataAccessName;

  MSAccessManager.CreateDatabase(DataAccessFileName);

  // Link in tables from data from config
  Tokens := TStringList.Create;
  try
    for i := 0 to SystemConfig.DataAccessSourceCount - 1 do
    begin
      ExtractTokensL(SystemConfig.DataAccessSources[i], ';', '"', false, Tokens);
      MSAccessManager.Link(SystemConfig.DataAccessID[i], Tokens[0],
        IncludeTrailingPathDelimiter(Path) + Tokens[1]);
    end;
  finally
    Tokens.Free;
  end;

end;

//procedure TEMGAATSModel.Deploy(Engine: IEngineDeployStrategy);
//begin
//  Engine.Deploy;
//end;

procedure TEMGAATSModel.DeleteModelMDB(AFileName: TFileName);
var
  FileWasDeleted: Boolean;
begin
  if FileExists(AFileName) then
     FileWasDeleted := DeleteFile(AFileName);
  if not FileWasDeleted then
    AddError(TEMGAATSError.Create('Could not delete ' + AFileName, eetError));
end;

procedure TEMGAATSModel.DeployEngineFile;
var
  EngineFileName: String;
  EngineTitle1QueryText: String;
  EngineTitle1: String;
  TimeFrameText: String;
  DHIFileName: String;
begin
  IncProgress;
  ShowProgressMessage('Deploying engine file');

  try
    MSAccessManager.OpenDatabase(fConfig.MasterMDBFilesByName['ModelDeployHydraulics']);
  
    MSAccessManager.Run('XP_Parallel_Links', ['_getParallelLinks']);
    MSAccessManager.Run('XP_Special_Links', []);
  
    case fConfig.TimeFrame of
      tfExisting: TimeFrameText := 'Existing';
      tfFuture: TimeFrameText := 'Future';
    end;
  
    EngineTitle1 := '"TimeFrame: ' + TimeFrameText +
      ', Model Created:' + FormatDateTime('m/d/yyyy h:mm', Now) + '"';
    EngineTitle1QueryText := 'UPDATE JobControlExtran SET JobControlExtran.TValue = ' +
      EngineTitle1 + ' WHERE (((JobControlExtran.Tag)="ALPHB")) ';
  
    MSAccessManager.RunQuery(EngineTitle1QueryText);
  
    EngineFileName := IncludeTrailingPathDelimiter(fConfig.Path) + 'sim\' +
      fConfig.EngineExportFileName;
    MSAccessManager.Run('ExportQueryTable',[EngineFileName, 'XPExportQueryTable',
      'Class', 'ALL', 0]);
    MSAccessManager.RunQuery('_UpdateSimLinkID');
  
    DHIFileName := IncludeTrailingPathDelimiter(fConfig.Path) + 'sim\' +
      'DHImodel.mdb';
    MSAccessManager.Run('CreateDHIDatabase', [DHIFileName,'DHI_Export']);

  except on E: Exception do
    AddError(TEMGAATSError.Create('Error deploying engine file. ' +
      'Error: ' + E.Message, eetError));
  end;

end;

procedure TEMGAATSModel.DeployRunoffFile;
var
  RunoffFileName: String;
  StormUpdateQueryText: String;
  RunoffTitle1QueryText: String;
  RunoffTitle1: String;
  RunoffTitle2QueryText: String;
  RunoffTitle2: String;
  TimeFrameText: String;
begin
  IncProgress;
  ShowProgressMessage('Deploying runoff file');

  try
    MSAccessManager.OpenDatabase(fConfig.MasterMDBFilesByName['ModelDeployHydrology']);
  
    // Prepare tables
    MSAccessManager.Run('ExecuteQueryTable', ['ListExecuteModelBuild', 'Block', 'P', 0]);
    MSAccessManager.Run('ExecuteQueryTable', ['ListExecuteModelBuild', 'Block', 'R', 0]);
  
    // Write file
    RunoffFileName := IncludeTrailingPathDelimiter(fConfig.Path) + 'sim\' +
      fConfig.RunoffFileName;
  
    // Choose storm type (calibration or design)
    { TODO : Allow user to choose storms from build screen }
    StormUpdateQueryText := 'Update R_Control_B1 SET R_Control_B1.[DesignStorm?] = TRUE';
    MSAccessManager.RunQuery(StormUpdateQueryText);
  
    // Update title of runoff
    RunoffTitle1 := '"Model: ' + Ellipsize(fConfig.Path, 70) + '"';
    RunoffTitle1QueryText := 'UPDATE R_control_A1 SET R_control_A1.TitleText = ' +
      RunoffTitle1 + 'WHERE (((R_control_A1.TwoRecordsOnly)=1))';
    MSAccessManager.RunQuery(RunoffTitle1QueryText);
  
    case fConfig.TimeFrame of
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

  except on E: Exception do
    AddError(TEMGAATSError.Create('Error deploying runoff file. ' +
      'Error: ' + E.Message, eetError));
  end;

  CreateModelBatchFile('Runoff');

  CreateReconciliationSpreadsheet;
end;

destructor TEMGAATSModel.Destroy;
begin
  fConfig.Free;
//  fErrHandler.Free;
  fErrLog.Free;
  inherited;
end;

procedure TEMGAATSModel.GetForcedRootLinks(AList: TStrings; AErrHandler: TRzErrorHandler = nil);
var
  i: Integer;
begin
  AList.Clear;
  for i := 0 to fConfig.ForcedRootLinksCount - 1 do
    try
      AList.Add(Format('%d=%s', [fConfig.ForcedRootLinks[i], fConfig.ForcedRootLinkComments[i]]));
    except on E: Exception do
      if Assigned(AErrHandler) then
        if Length(fConfig.ForcedRootLinksAsString[i]) = 0 then
          AErrHandler.Add('Blank entry "=". ' +
            'Ignored forced root link entry.', etWarning)
        else
          AErrHandler.Add('Error reading "' + fConfig.ForcedRootLinksAsString[i] +
            '" as an integer. Ignored forced root link entry.', etWarning);
    end;
end;

procedure TEMGAATSModel.GetForcedStopLinks(AList: TStrings; AErrHandler: TRzErrorHandler = nil);
var
  i: Integer;
begin
  AList.Clear;
  for i := 0 to fConfig.ForcedStopLinksCount - 1 do
    try
      AList.Add(Format('%d=%s', [fConfig.ForcedStopLinks[i], fConfig.ForcedStopLinkComments[i]]));
    except on E: Exception do
      if Assigned(AErrHandler) then
        if Length(fConfig.ForcedStopLinksAsString[i]) = 0 then
          AErrHandler.Add('Blank entry "=". ' +
            'Ignored forced stop link entry.', etWarning)
        else
          AErrHandler.Add('Error reading "' + fConfig.ForcedStopLinksAsString[i] +
            '" as an integer. Ignored forced stop link entry.', etWarning);
    end;
end;

function TEMGAATSModel.GetMapFileName(Index: String): String;
begin
  // I don't like the way this is being retrieved; please fix!
  Result := IncludeTrailingPathDelimiter(fConfig.Path) +
    IncludeTrailingPathDelimiter(JustPathNameL(SystemConfig.DataAccessFileByName[Index])) +
    JustNameL(SystemConfig.DataAccessFileByName[Index]) + '.tab';
end;

function TEMGAATSModel.GetMDB(Index: String): String;
begin
  Result := fConfig.MasterMDBFilesByName[Index];
end;

procedure TEMGAATSModel.GetModelStats;
var
  DataAccessFileName: TFileName;
  QueryText: String;
begin
  DataAccessFileName := IncludeTrailingPathDelimiter(fConfig.Path) +
    SystemConfig.DataAccessName;

  try
    if FileExists(DataAccessFileName) then
    begin
      MSAccessManager.OpenDatabase(DataAccessFileName);

      if fConfig.HasNetwork then
      begin
        fModelStats.NumLinks := MSAccessManager.RecordCount('mdl_links');
        fModelStats.NumNodes := MSAccessManager.RecordCount('mdl_nodes');
      end;

      if fConfig.HasDirectSubcatchments then
        fModelStats.NumDSCs := MSAccessManager.RecordCount('mdl_dirsc');

      if fConfig.HasSurfaceSubcatchments then
        fModelStats.NumSurfSCs := MSAccessManager.RecordCount('mdl_surfsc');

      // Count up constructed inflow controls
      QueryText := 'SELECT mdl_dirsc.DSCID ' +
        'FROM mst_ic_parkingtargets_ac INNER JOIN mdl_dirsc ON '+
          '(mst_ic_parkingtargets_ac.DivideID = mdl_dirsc.DivideID) '+
          'AND (mst_ic_parkingtargets_ac.ParcelID = mdl_dirsc.ParcelID) '+
        'WHERE (((mst_ic_parkingtargets_ac.Constructed)>0));';
      fModelStats.NumConstructedParkingTargets := MSAccessManager.RecordSetCount(QueryText);
      QueryText := 'SELECT mdl_dirsc.DSCID ' +
        'FROM mst_ic_rooftargets_ac INNER JOIN mdl_dirsc ON '+
          '(mst_ic_rooftargets_ac.DivideID = mdl_dirsc.DivideID) '+
          'AND (mst_ic_rooftargets_ac.ParcelID = mdl_dirsc.ParcelID) '+
        'WHERE (((mst_ic_rooftargets_ac.Constructed)>0));';
      fModelStats.NumConstructedRoofTargets := MSAccessManager.RecordSetCount(QueryText);
      QueryText := 'SELECT mdl_surfsc.SurfSCID, mst_ic_streettargets_ac.Constructed '+
        'FROM mst_ic_streettargets_ac INNER JOIN mdl_surfsc ON '+
          'mst_ic_streettargets_ac.SurfSCID = mdl_surfsc.SurfSCID '+
        'WHERE (((mst_ic_streettargets_ac.Constructed)>0));';
      fModelStats.NumConstructedStreetTargets := MSAccessManager.RecordSetCount(QueryText);

      // Count up inflow controls to build (alternatives)
      QueryText := 'SELECT mdl_dirsc.DSCID '+
        'FROM mst_ic_parkingtargets_ac INNER JOIN mdl_dirsc ON '+
          '(mst_ic_parkingtargets_ac.DivideID = mdl_dirsc.DivideID) AND '+
          '(mst_ic_parkingtargets_ac.ParcelID = mdl_dirsc.ParcelID) '+
        'WHERE (((mst_ic_parkingtargets_ac.Constructed)=0) AND ((mst_ic_parkingtargets_ac.BuildModelIC)=True));';
      fModelStats.NumBuildParkingTargets := MSAccessManager.RecordSetCount(QueryText);
      QueryText := 'SELECT mdl_dirsc.DSCID '+
        'FROM mst_ic_rooftargets_ac INNER JOIN mdl_dirsc ON '+
          '(mst_ic_rooftargets_ac.DivideID = mdl_dirsc.DivideID) AND '+
          '(mst_ic_rooftargets_ac.ParcelID = mdl_dirsc.ParcelID) '+
        'WHERE (((mst_ic_rooftargets_ac.Constructed)=0) AND ((mst_ic_rooftargets_ac.BuildModelIC)=True));';
      fModelStats.NumBuildRoofTargets := MSAccessManager.RecordSetCount(QueryText);
      QueryText := 'SELECT mdl_surfsc.SurfSCID '+
        'FROM mst_ic_streettargets_ac INNER JOIN mdl_surfsc ON '+
          'mst_ic_streettargets_ac.SurfSCID = mdl_surfsc.SurfSCID '+
        'WHERE (((mst_ic_streettargets_ac.Constructed)=0) AND ((mst_ic_streettargets_ac.BuildModelIC)=True));';
      fModelStats.NumBuildStreetTargets := MSAccessManager.RecordSetCount(QueryText);

      MSAccessManager.CloseDatabase;
    end;
  except on E: Exception do
    AddError(TEMGAATSError.Create('Error retrieving model statistics ' +
      'Error: ' + E.Message, eetError));

  end;
end;

function TEMGAATSModel.GetQCWorkbookFileName: String;
begin
  Result := IncludeTrailingPathDelimiter(fConfig.Path) + 'qc\' + fConfig.HydroQCFileName;
end;

procedure TEMGAATSModel.GetRootLinks(AList: TStrings; AErrHandler: TRzErrorHandler = nil);
var
  i: Integer;
begin
  AList.Clear;
  for i := 0 to fConfig.RootLinksCount - 1 do
    try
      AList.Add(Format('%d=%s', [fConfig.RootLinks[i], fConfig.RootLinkComments[i]]));
    except on E: Exception do
      if Assigned(AErrHandler) then
        if Length(fConfig.RootLinksAsString[i]) = 0 then
          AErrHandler.Add('Blank entry "=". ' +
            'Ignored root link entry.', etWarning)
        else
          AErrHandler.Add('Error reading "' + fConfig.RootLinksAsString[i] +
            '" as an integer. Ignored root link entry.', etWarning);
    end;
end;

procedure TEMGAATSModel.GetStopLinks(AList: TStrings; AErrHandler: TRzErrorHandler = nil);
var
  i: Integer;
begin
  AList.Clear;
  for i := 0 to fConfig.StopLinksCount - 1 do
    try
      AList.Add(Format('%d=%s', [fConfig.StopLinks[i], fConfig.StopLinkComments[i]]));
    except on E: Exception do
      if Assigned(AErrHandler) then
        if Length(fConfig.StopLinksAsString[i]) = 0 then
          AErrHandler.Add('Blank entry "=". ' +
            'Ignored stop link entry.', etWarning)
        else
          AErrHandler.Add('Error reading "' + fConfig.StopLinksAsString[i] +
            '" as an integer. Ignored stop link entry.', etWarning);
    end;
end;

procedure TEMGAATSModel.IncProgress;
begin
  if Assigned(fProgressBar) then
  begin
    fProgressBar.IncPartsByOne;
    fProgressBar.Update;
  end;
end;

procedure TEMGAATSModel.MakeDirty;
begin
  fIsDirty := True;
end;

procedure TEMGAATSModel.OpenModelAndMasterData;
begin
  try
    EMGWorkbenchManager.OpenTables;
  except on E: Exception do
    AddError(TEMGAATSError.Create('Error opening model and master data. ' +
      'Error: ' + E.Message, eetError));
  end;
end;

procedure TEMGAATSModel.RelinkModelData;
var
  ModelPath: String;
  ModelMDBPath: String;
begin
  ModelPath := IncludeTrailingPathDelimiter(fConfig.Path);
  ModelMDBPath := ModelPath + 'mdbs\';

  // ModelAssemble
  try
    MSAccessManager.OpenDatabase(ModelMDBPath + 'ModelAssemble.mdb');
    MSAccessManager.Relink('mdl_nodes_ac', 'mdl_nodes_ac', ModelPath + 'nodes\mdl_nodes_ac.mdb');
    MSAccessManager.Relink('mdl_links_ac', 'mdl_links_ac', ModelPath + 'links\mdl_links_ac.mdb');
    MSAccessManager.Relink('mdl_speclinkdata_ac', 'mdl_speclinkdata_ac', ModelPath + 'links\mdl_speclinkdata_ac.mdb');
    MSAccessManager.Relink('mdl_speclinks_ac', 'mdl_speclinks_ac', ModelPath + 'links\mdl_speclinks_ac.mdb');
    MSAccessManager.Relink('mst_speclinkdata_ac', 'mst_speclinkdata_ac', SystemConfig.MasterFilesByName['speclinkdata']);
    MSAccessManager.Relink('mst_speclinks_ac', 'mst_speclinks_ac', SystemConfig.MasterFilesByName['speclinks']);
  except on E: Exception do
    AddError(TEMGAATSError.Create('Error relinking ModelAssemble database tables. ' +
      'Error: ' + E.Message, eetError));
  end;

  // LinkQAQC
  try
    MSAccessManager.OpenDatabase(ModelMDBPath + 'LinkQAQC.mdb');
    MSAccessManager.Relink('mdl_nodes_ac', 'mdl_nodes_ac', ModelPath + 'nodes\mdl_nodes_ac.mdb');
    MSAccessManager.Relink('mdl_links_ac', 'mdl_links_ac', ModelPath + 'links\mdl_links_ac.mdb');
  except on E: Exception do
    AddError(TEMGAATSError.Create('Error relinking LinkQAQC database tables. ' +
      'Error: ' + E.Message, eetError));
  end;

  // HydInitDSC
  try
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
  except on E: Exception do
    AddError(TEMGAATSError.Create('Error relinking HydInitDSC database tables. ' +
      'Error: ' + E.Message, eetError));
  end;

  // ModelDeployHydraulics
  try
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
  except on E: Exception do
    AddError(TEMGAATSError.Create('Error relinking ModelDeployHydraulics database tables. ' +
      'Error: ' + E.Message, eetError));
  end;

  // ModelDeployHydrology
  try
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
  except on E: Exception do
    AddError(TEMGAATSError.Create('Error relinking ModelDeployHydrology database tables. ' +
      'Error: ' + E.Message, eetError));
  end;

  // ModelResults
  try
    MSAccessManager.OpenDatabase(ModelMDBPath + 'ModelResults.mdb');
    MSAccessManager.Relink('mdl_dirsc_ac', 'mdl_dirsc_ac', ModelPath + 'dsc\mdl_dirsc_ac.mdb');
    MSAccessManager.Relink('mdl_links_ac', 'mdl_links_ac', ModelPath + 'links\mdl_links_ac.mdb');
  except on E: Exception do
    AddError(TEMGAATSError.Create('Error relinking ModelResults database tables. ' +
      'Error: ' + E.Message, eetError));
  end;

end;

procedure TEMGAATSModel.RemoveNetwork;
begin
  if fConfig.HasNetwork then
    try
      EMGWorkbenchManager.ClearNetwork;
    except on E: Exception do
      AddError(TEMGAATSError.Create('Error clearing network. ' +
        'MapInfo error: ' + E.Message, eetError));
    end;

  if fConfig.HasDirectSubcatchments then
    try
      EMGWorkbenchManager.ClearDirectSubcatchments;
    except on E: Exception do
      AddError(TEMGAATSError.Create('Error clearing direct subcatchments. ' +
        'MapInfo error: ' + E.Message, eetError));
    end;

  if fConfig.HasSurfaceSubcatchments then
    try
      EMGWorkbenchManager.ClearSurfaceSubcatchments;
    except on E: Exception do
      AddError(TEMGAATSError.Create('Error clearing surface subcatchments. ' +
        'MapInfo error: ' + E.Message, eetError));
    end;

  fConfig.HasNetwork := False;
  fConfig.HasDirectSubcatchments := False;
  fConfig.HasSurfaceSubcatchments := False;
  fConfig.Update;
end;

procedure TEMGAATSModel.SetPath(const Value: String);
begin
  fPath := Value;
end;

procedure TEMGAATSModel.SetProgressParts(NumSteps: Integer);
begin
  if Assigned(fProgressBar) then
  begin
    fProgressBar.TotalParts := NumSteps;
    fProgressBar.PartsComplete := 0;
    fProgressBar.Update;
  end;
end;

procedure TEMGAATSModel.SetSilent(const Value: Boolean);
begin
  fSilent := Value;
end;

procedure TEMGAATSModel.ShowProgressMessage(AMessage: String);
begin
  if Assigned(fProgressLabel) then
  begin
    fProgressLabel.Caption := AMessage;
    fProgressLabel.Update;
  end;
end;

procedure TEMGAATSModel.TransferErrors(AList: TStrings);
begin
  if fErrLog.Count = 0 then
    AList.Add('No build messages')
  else
    AList.AddStrings(fErrLog);
end;

procedure TEMGAATSModel.TransferStats(AList: TStrings);
begin
  GetModelStats;
  AList.Add(Format('Number of links = %d', [fModelStats.NumLinks]));
  AList.Add(Format('Number of nodes = %d', [fModelStats.NumNodes]));
  AList.Add(Format('Number of direct subcatchments = %d', [fModelStats.NumDSCs]));
  AList.Add(Format('Number of surface subcatchments = %d', [fModelStats.NumSurfSCs]));
  AList.Add(Format('Number of constructed roof target controls = %d', [fModelStats.NumConstructedRoofTargets]));
  AList.Add(Format('Number of constructed parking target controls = %d', [fModelStats.NumConstructedParkingTargets]));
  AList.Add(Format('Number of constructed street target controls = %d', [fModelStats.NumConstructedStreetTargets]));
  AList.Add(Format('Number of roof target controls to build = %d', [fModelStats.NumBuildRoofTargets]));
  AList.Add(Format('Number of parking target controls to build = %d', [fModelStats.NumBuildParkingTargets]));
  AList.Add(Format('Number of street target controls to build = %d', [fModelStats.NumBuildStreetTargets]));
end;

procedure TEMGAATSModel.Update;
begin
  // Do update of model
  fIsDirty := False;
end;

end.
