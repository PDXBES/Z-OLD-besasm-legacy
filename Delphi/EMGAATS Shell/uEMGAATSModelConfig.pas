unit uEMGAATSModelConfig;

interface

uses SysUtils, Classes, IniFiles, uConfigFiles, uEMGAATSTypes;

type
  TModelConfig = class
  private
    fConfig: IConfigFile;
    fMDBFiles: THashedStringList;
    fStandardDirectories: THashedStringList;
    fRootLinks: THashedStringList;
    fStopLinks: THashedStringList;
    fForcedRootLinks: THashedStringList;
    fForcedStopLinks: THashedStringList;
    fStormsToBuild: TStringList;
    fTraceStormwater: Boolean;
    fRemoveSanitaryUponDeploy: Boolean;
    function GetAndUpdateString(SectionName: String; KeyName: String;
      DefaultValue: String): String;
    function GetAndUpdateBool(SectionName: String; KeyName: String;
      DefaultValue: Boolean): Boolean;
    function GetAndUpdateDateTime(SectionName: String; KeyName: String;
      DefaultValue: TDateTime): TDateTime;
    function GetAndUpdateInt(SectionName: String; KeyName: String;
      DefaultValue: Integer): Integer;
    function GetHasDirectSubcatchments: Boolean;
    function GetHasNetwork: Boolean;
    function GetHasSurfaceSubcatchments: Boolean;
    function GetModelCreateDate: TDateTime;
    function GetModelModifiedDate: TDateTime;
    function GetModelName: String;
    function GetVersion: String;
    function GetMasterMDBFiles(index: Integer): String;
    function GetMasterMDBFilesCount: Integer;
    function GetMasterMDBFileIDs(index: Integer): String;
    function GetFileName: String;
    function GetPath: String;
    function GetStandardDirectory(Index: Integer): String;
    function GetStandardDirectoryByName(Index: String): String;
    function GetStandardDirectoryCount: Integer;
    function GetDebugFileName: TFileName;
    function GetDebugTrace: Boolean;
    function GetDSNodeField: String;
    function GetElementField: String;
    function GetEMGWorkbenchPath: String;
    function GetEngineExportFileName: TFileName;
    function GetForceMDBRefresh: Boolean;
    function GetLinkExistingTable: String;
    function GetLinkField: String;
    function GetLinkFutureTable: String;
    function GetMasterLinksPath: String;
    function GetMasterNodesPath: String;
    function GetMasterParcelsPath: String;
    function GetMasterQuartersectionPath: String;
    function GetMasterRootPath: String;
    function GetMasterSpecLinksDataPath: String;
    function GetMasterSpecLinksPath: String;
    function GetMasterSurfacePath: String;
    function GetRootLink(Index: Integer): Integer;
    function GetRootLinkComment(Index: Integer): String;
    function GetRootLinksCount: Integer;
    function GetRunoffDeployDate: TDateTime;
    function GetRunoffFileName: TFileName;
    function GetStopLink(Index: Integer): Integer;
    function GetStopLinkComment(Index: Integer): String;
    function GetStopLinksCount: Integer;
    function GetSystemMDBRefreshDate: TDateTime;
    function GetTimeFrame: TTimeFrame;
    function GetTraceFileName: TFileName;
    function GetTraceSourceDatabase: TFileName;
    function GetTraceSourceTable: String;
    function GetTransportFileName: TFileName;
    function GetUSNodeField: String;
    procedure SetDebugFileName(const Value: TFileName);
    procedure SetDebugTrace(const Value: Boolean);
    procedure SetDSNodeField(const Value: String);
    procedure SetElementField(const Value: String);
    procedure SetEMGWorkbenchPath(const Value: String);
    procedure SetEngineExportFileName(const Value: TFileName);
    procedure SetForceMDBRefresh(const Value: Boolean);
    procedure SetHasDirectSubcatchments(const Value: Boolean);
    procedure SetHasNetwork(const Value: Boolean);
    procedure SetHasSurfaceSubcatchments(const Value: Boolean);
    procedure SetLinkExistingTable(const Value: String);
    procedure SetLinkField(const Value: String);
    procedure SetLinkFutureTable(const Value: String);
    procedure SetMasterLinksPath(const Value: String);
    procedure SetMasterNodesPath(const Value: String);
    procedure SetMasterParcelsPath(const Value: String);
    procedure SetMasterQuartersectionPath(const Value: String);
    procedure SetMasterRootPath(const Value: String);
    procedure SetMasterSpecLinksDataPath(const Value: String);
    procedure SetMasterSpecLinksPath(const Value: String);
    procedure SetMasterSurfacePath(const Value: String);
    procedure SetModelCreateDate(const Value: TDateTime);
    procedure SetModelModifiedDate(const Value: TDateTime);
    procedure SetRootLink(Index: Integer; const Value: Integer);
    procedure SetRootLinkComment(Index: Integer; const Value: String);
    procedure SetRunoffDeployDate(const Value: TDateTime);
    procedure SetRunoffFileName(const Value: TFileName);
    procedure SetStopLink(Index: Integer; const Value: Integer);
    procedure SetStopLinkComment(Index: Integer; const Value: String);
    procedure SetTraceFileName(const Value: TFileName);
    procedure SetTraceSourceDatabase(const Value: TFileName);
    procedure SetTraceSourceTable(const Value: String);
    procedure SetTransportFileName(const Value: TFileName);
    procedure SetUSNodeField(const Value: String);
    function GetReachField: String;
    procedure SetReachField(const Value: String);
    function GetMasterMDBFilesByName(index: String): String;
    function GetHydroQCFileName: string;
    procedure SetHydroQCFileName(const Value: string);
    function GetForcedRootLink(Index: Integer): Integer;
    function GetForcedRootLinkComment(Index: Integer): String;
    function GetForcedRootLinksCount: Integer;
    function GetForcedStopLink(Index: Integer): Integer;
    function GetForcedStopLinkComment(Index: Integer): String;
    function GetForcedStopLinksCount: Integer;
    procedure SetForcedRootLink(Index: Integer; const Value: Integer);
    procedure SetForcedRootLinkComment(Index: Integer; const Value: String);
    procedure SetForcedStopLink(Index: Integer; const Value: Integer);
    procedure SetForcedStopLinkComment(Index: Integer; const Value: String);
    function GetHasSkeletonModel: Boolean;
    procedure SetHasSkeletonModel(const Value: Boolean);
    function GetHasStandardDirectories: Boolean;
    procedure SetHasStandardDirectories(const Value: Boolean);
    function GetStormsToBuild(Index: Integer): String;
    function GetStormsToBuildCount: Integer;
    procedure GetStormsToBuildFromConfig;
    function GetStormsToBuildList: String;
    procedure SetStormsToBuildList(const Value: String);
    procedure SetTimeFrame(const Value: TTimeFrame);
    function GetDescription: String;
    function GetTitle: String;
    procedure SetDescription(const Value: String);
    procedure SetTitle(const Value: String);
    function GetRootLinkAsString(Index: Integer): String;
    function GetStopLinkAsString(Index: Integer): String;
    function GetForcedRootLinkAsString(Index: Integer): String;
    function GetForcedStopLinkAsString(Index: Integer): String;
    function GetCreatedVia: String;
    function GetResultViewsProjectDescription: String;
    function GetResultViewsProjectNumber: String;
    function GetResultViewsStudyAreaID: String;
    procedure SetResultViewsProjectDescription(const Value: String);
    procedure SetResultViewsProjectNumber(const Value: String);
    procedure SetResultViewsStudyAreaID(const Value: String);
    function GetUseBaseflow: Boolean;
    procedure SetUseBaseflow(const Value: Boolean);
    function GetModelEnteredDate: TDateTime;
    procedure SetModelEnteredDate(const Value: TDateTime);
    procedure SetSystemMDBRefreshDate(const Value: TDateTime);
    procedure SetTraceStormwater(const Value: Boolean);
    function GetTraceStormwater: Boolean;
    function GetRemoveSanitaryUponDeploy: Boolean;
    procedure SetRemoveSanitaryUponDeploy(const Value: Boolean);
  protected
    procedure GetMDBFiles;
    procedure GetStandardDirectories;
    procedure GetRootLinks;
    procedure GetStopLinks;
    procedure GetForcedRootLinks;
    procedure GetForcedStopLinks;
  public
    constructor Create(AFileName: TFileName);
    destructor Destroy; override;

    // Master MDB section
    property MasterMDBFileIDs[index: Integer]: String read GetMasterMDBFileIDs;
    property MasterMDBFiles[index: Integer]: String read GetMasterMDBFiles;
    property MasterMDBFilesCount: Integer read GetMasterMDBFilesCount;
    property MasterMDBFilesByName[index: String]: String read GetMasterMDBFilesByName;
    procedure CommentOutMasterMDB(Index: Integer);

    // Control section
    property Version: String read GetVersion;

    // ModelState section
    property TimeFrame: TTimeFrame read GetTimeFrame write SetTimeFrame;
    property HasStandardDirectories: Boolean read GetHasStandardDirectories
      write SetHasStandardDirectories;
    property HasSkeletonModel: Boolean read GetHasSkeletonModel write SetHasSkeletonModel;
    property HasNetwork: Boolean read GetHasNetwork write SetHasNetwork;
    property HasDirectSubcatchments: Boolean read GetHasDirectSubcatchments
      write SetHasDirectSubcatchments;
    property HasSurfaceSubcatchments: Boolean read GetHasSurfaceSubcatchments
      write SetHasSurfaceSubcatchments;

    // Admin section
    property ForceMDBRefresh: Boolean read GetForceMDBRefresh write SetForceMDBRefresh;
    property SystemMDBRefreshDate: TDateTime read GetSystemMDBRefreshDate
      write SetSystemMDBRefreshDate;
    property ModelName: String read GetModelName;
    property ModelCreateDate: TDateTime read GetModelCreateDate
      write SetModelCreateDate;
    property ModelEnteredDate: TDateTime read GetModelEnteredDate
      write SetModelEnteredDate;
    property ModelModifiedDate: TDateTime read GetModelModifiedDate
      write SetModelModifiedDate;
    property RunoffDeployDate: TDateTime read GetRunoffDeployDate
      write SetRunoffDeployDate;
    property FileName: String read GetFileName;
    property Path: String read GetPath;
    property Title: String read GetTitle write SetTitle;
    property Description: String read GetDescription write SetDescription;

    // QC section
    property HydroQCFileName: string read GetHydroQCFileName write SetHydroQCFileName;

    // MasterFiles section
    property MasterRootPath: String read GetMasterRootPath
      write SetMasterRootPath;
    property MasterLinksPath: String read GetMasterLinksPath
      write SetMasterLinksPath;
    property MasterSpecLinksDataPath: String read GetMasterSpecLinksDataPath
      write SetMasterSpecLinksDataPath;
    property MasterSpecLinksPath: String read GetMasterSpecLinksPath
      write SetMasterSpecLinksPath;
    property MasterNodesPath: String read GetMasterNodesPath
      write SetMasterNodesPath;
    property MasterParcelsPath: String read GetMasterParcelsPath
      write SetMasterParcelsPath;
    property MasterSurfacePath: String read GetMasterSurfacePath
      write SetMasterSurfacePath;
    property MasterQuartersctionPath: String read GetMasterQuartersectionPath
      write SetMasterQuartersectionPath;

    // Code section
    property EMGWorkbenchPath: String read GetEMGWorkbenchPath
      write SetEMGWorkbenchPath;

     // StandardDirectories section
    property StandardDirectories[Index: Integer]: String read GetStandardDirectory;
    property StandardDirectoriesByName[Index: String]: String read GetStandardDirectoryByName;
    property StandardDirectoryCount: Integer read GetStandardDirectoryCount;

    // Treeverse section
    property TraceFileName: TFileName read GetTraceFileName
      write SetTraceFileName;
    property TraceSourceDatabase: TFileName read GetTraceSourceDatabase
      write SetTraceSourceDatabase;
    property TraceSourceTable: String read GetTraceSourceTable
      write SetTraceSourceTable;
    property LinkField: String read GetLinkField write SetLinkField;
    property USNodeField: String read GetUSNodeField write SetUSNodeField;
    property DSNodeField: String read GetDSNodeField write SetDSNodeField;
    property ReachField: String read GetReachField write SetReachField;
    property ElementField: String read GetElementField write SetElementField;
    property DebugTrace: Boolean read GetDebugTrace write SetDebugTrace;
    property DebugFileName: TFileName read GetDebugFileName write SetDebugFileName;
    property LinkExistingTable: String read GetLinkExistingTable write SetLinkExistingTable;
    property LinkFutureTable: String read GetLinkFutureTable write SetLinkFutureTable;

    // Simulation section
    property RunoffFileName: TFileName read GetRunoffFileName write SetRunoffFileName;
    property TransportFileName: TFileName read GetTransportFileName write SetTransportFileName;
    property EngineExportFileName: TFileName read GetEngineExportFileName
      write SetEngineExportFileName;
    property StormsToBuildCount: Integer read GetStormsToBuildCount;
    property StormsToBuild[Index: Integer]: String read GetStormsToBuild;
    function AddStormToBuild(AStormCode: String): Integer;
    property StormsToBuildList: String read GetStormsToBuildList write SetStormsToBuildList;
    procedure ClearStormsToBuild;
    property UseBaseflow: Boolean read GetUseBaseflow write SetUseBaseflow;
    property RemoveSanitaryUponDeploy: Boolean read GetRemoveSanitaryUponDeploy write SetRemoveSanitaryUponDeploy;

    // Trace params
    property RootLinks[Index: Integer]: Integer read GetRootLink write SetRootLink;
    property RootLinksAsString[Index: Integer]: String read GetRootLinkAsString;
    property RootLinkComments[Index: Integer]: String read GetRootLinkComment
      write SetRootLinkComment;
    property RootLinksCount: Integer read GetRootLinksCount;
    procedure AddRootLink(ARoot: Integer; Comment: String = '');
    procedure RemoveRootLinkByIndex(Index: Integer);
    procedure RemoveRootLink(ARoot: Integer);
    procedure ClearRootLinks;

    property ForcedRootLinks[Index: Integer]: Integer read GetForcedRootLink write SetForcedRootLink;
    property ForcedRootLinksAsString[Index: Integer]: String read GetForcedRootLinkAsString;
    property ForcedRootLinkComments[Index: Integer]: String read GetForcedRootLinkComment
      write SetForcedRootLinkComment;
    property ForcedRootLinksCount: Integer read GetForcedRootLinksCount;

    property StopLinks[Index: Integer]: Integer read GetStopLink write SetStopLink;
    property StopLinksAsString[Index: Integer]: String read GetStopLinkAsString;
    property StopLinkComments[Index: Integer]: String read GetStopLinkComment
      write SetStopLinkComment;
    property StopLinksCount: Integer read GetStopLinksCount;
    procedure AddStopLink(AStop: Integer; Comment: String = '');
    procedure RemoveStopLinkByIndex(Index: Integer);
    procedure RemoveStopLink(AStop: Integer);
    procedure ClearStopLinks;

    property ForcedStopLinks[Index: Integer]: Integer read GetForcedStopLink write SetForcedStopLink;
    property ForcedStopLinksAsString[Index: Integer]: String read GetForcedStopLinkAsString;
    property ForcedStopLinkComments[Index: Integer]: String read GetForcedStopLinkComment
      write SetForcedStopLinkComment;
    property ForcedStopLinksCount: Integer read GetForcedStopLinksCount;

    property CreatedVia: String read GetCreatedVia;

    procedure CopyBoundariesFromModel(AModelPath: String);

    property TraceStormwater: Boolean read GetTraceStormwater write SetTraceStormwater;

    // Operations
    // Copies standard entries from model template file
    procedure CopyConfigFromModelTemplate;
    // Copies standard entries from system config and user config files
    procedure CopyConfigFromSystem;
    // Updates the physical file
    procedure Update;
    // Reverts config to physical file contents
    procedure Revert;
    // Called after master template is copied over or if model directory shifted
    procedure SetMDBRootToModel;
    // Called when resetting the model
    procedure SetMDBRootToSystem;
    // Called if the workbench file can't be found
    procedure SetEMGWorkbenchToSystem;

    // Result View Settings
    property ResultViewsProjectNumber: String read GetResultViewsProjectNumber
      write SetResultViewsProjectNumber;
    property ResultViewsProjectDescription: String read GetResultViewsProjectDescription
      write SetResultViewsProjectDescription;
    property ResultViewsStudyAreaID: String read GetResultViewsStudyAreaID
      write SetResultViewsStudyAreaID;
  end;

implementation

uses uEMGAATSModelTemplateConfig, uEMGAATSSystemConfig, CodeSiteLogging, StStrL;

resourcestring
  // Config Sections
  ControlSection = 'Control';
  ModelStateSection = 'ModelState';
  AdminSection = 'Admin';
  MasterFilesSection = 'MasterFiles';
  CodeSection = 'Code';
  MDBsSection = 'MDBs';
  TreeVerseSection = 'TreeVerse';
  SimulationSection = 'Simulation';
  RootLinksSection = 'RootLinks';
  StopLinksSection = 'StopLinks';
  ForcedRootLinksSection = 'ForcedStartLinks';
  ForcedStopLinksSection = 'ForcedStopLinks';
  QCSection = 'QC';
  ResultViewsSection = 'ResultViews';

  // Control Config Keys
  VersionKey = 'Version';

  // ModelState Config Keys
  TimeFrameKey = 'TimeFrame';
  HasStandardDirectoriesKey = 'HasStandardDirectories';
  HasSkeletonModelKey = 'HasSkeletonModel';
  HasNetworkKey = 'HasNetwork';
  HasDirectSubcatchmentsKey = 'HasParcels';
  HasSurfaceSubcatchmentsKey = 'HasSurfaceSubcatchments';

  // Admin Config Keys
  ForceMDBRefreshKey = 'RefreshMDBs';
  NameKey = 'Name';
  TitleKey = 'Title';
  DescriptionKey = 'Description';
  CreatedKey = 'Created';
  SystemMDBUpdateDateKey = 'MDBDate';
  RunoffDeployedKey = 'RunoffDeployed';
  ModifiedKey = 'Modified';
  EnteredKey = 'Entered';
  CreatedViaKey = 'CreatedVia';


  // QC Config Keys
  HydroQCFileNameKey = 'HydroQCFileName';
  DefaultHydroQCFileName = 'HydroQC.xls';

  // Master Files Paths Config Keys
  MasterRootKey = 'Root';
  MasterLinksKey = 'Links';
  MasterSpecLinksDataKey = 'SpecLinkData';
  MasterSpecLinksKey = 'SpecLinks';
  MasterNodesKey = 'Nodes';
  MasterParcelsKey = 'Parcels';
  MasterSurfaceKey = 'Surface';
  MasterQuartersectionsKey = 'Quartersections';

  // Code Config Keys
  EMGWorkbenchKey = 'MIWorkbench';

  // MDBs Config Keys
  LinkQAQCKey = 'LinkQAQC';
  HydInitDSCKey = 'HydInitDSC';
  ModelAssembleKey = 'ModelAssembble';
  ModelDeployHydraulicsKey = 'ModelDeployHydraulics';
  ModelDeployHydrologyKey = 'ModelDeployHydrology';
  ModelResultsKey = 'ModelResults';
  LookupTablesKey = 'LookupTables';
  EMGAATSCodeKey = 'EmgaatsCode';

  // TreeVerse Config Keys
  TraceFileKey = 'TraceFile';
  SourceDatabaseKey = 'SourceDatabase';
  SourceTableKey = 'SourceTable';
  LinkKey = 'Link';
  USNodeKey = 'USNode';
  DSNodeKey = 'DSNode';
  ReachKey = 'Reach';
  ElementKey = 'Element';
  DebugTraceKey = 'IsDebugOn';
  DebugFileKey = 'DebugFile';
  EnableMessagesKey = 'EnableMessages';
  LinkExistingKey = 'LinkExisting';
  LinkFutureKey = 'LinkFuture';
  TraceStormwaterKey = 'TraceStormwater';

  // Simulation Config Keys
  RunoffFileKey = 'RunoffFile';
  TransportFileKey = 'TransportFile';
  EngineFileKey = 'EngineFile';
  DefaultEngineFileValue = 'xpextran.xpx';
  StormsToBuildKey = 'StormsToBuild';
  UseBaseflowKey = 'UseBaseflow';
  RemoveSanitaryUponDeployKey = 'RemoveSanitaryUponDeploy';

  // Result Views Keys
  ProjectNumberKey = 'ProjectNumber';
  ProjectDescriptionKey = 'ProjectDescription';
  StudyAreaIDKey = 'StudyAreaID';

{ TModelConfig }

procedure TModelConfig.AddRootLink(ARoot: Integer; Comment: String);
begin
  fRootLinks.Add(IntToStr(ARoot) + '=' + Comment);
end;

procedure TModelConfig.AddStopLink(AStop: Integer; Comment: String);
begin
  fStopLinks.Add(IntToStr(AStop) + '=' + Comment);
end;

function TModelConfig.AddStormToBuild(AStormCode: String): Integer;
begin

end;

procedure TModelConfig.ClearRootLinks;
begin
  fRootLinks.Clear;
end;

procedure TModelConfig.ClearStopLinks;
begin
  fStopLinks.Clear;
end;

procedure TModelConfig.ClearStormsToBuild;
begin
  fStormsToBuild.Clear;
end;

procedure TModelConfig.CommentOutMasterMDB(Index: Integer);
begin
  // Write the sharped version of the mdb=path line
  // Delete the unsharped version
{				ModelIni.WriteString(MDBSSectionString, '#'+MDBSToCopy[i],
					ModelIni.ReadString(MDBSSectionString, MDBSToCopy[i],'unknown'));
				ModelIni.DeleteKey(MDBSSectionString, MDBSToCopy[i]);
}
end;

procedure TModelConfig.CopyBoundariesFromModel(AModelPath: String);
var
  CopyFromINI: TMemIniFile;

  procedure CopySectionFromINI(SectionName: String);
  var
    i: Integer;
    SectionItems: THashedStringList;
  begin
    SectionItems := THashedStringList.Create;
    try
      CopyFromIni.ReadSectionValues('RootLinks', SectionItems);
      for i := 0 to SectionItems.Count - 1 do
        fConfig.WriteString('RootLinks', SectionItems.Names[i],
          SectionItems.Values[SectionItems.Names[i]]);
    finally
      SectionItems.Free;
    end;
  end;

  procedure UpdateInternalList(SectionName: String; List: THashedStringList);
  var
    i: Integer;
    SectionItems: THashedStringList;
  begin
    SectionItems := THashedStringList.Create;
    try
      fConfig.ReadSectionItems(SectionName, SectionItems);
      for i := 0 to SectionItems.Count - 1 do
        List.Add(SectionItems[i]);
    finally
      SectionItems.Free;
    end;
  end;

begin
  CopyFromINI := TMemIniFile.Create(AModelPath);
  try
    fConfig.EraseSection('RootLinks');
    fConfig.EraseSection('StopLinks');
    fConfig.EraseSection('ForcedStartLinks');
    fConfig.EraseSection('ForcedStopLinks');

    CopySectionFromINI('RootLinks');
    CopySectionFromINI('StopLinks');
    CopySectionFromINI('ForcedStartLinks');
    CopySectionFromINI('ForcedStopLinks');

    fConfig.UpdateFile;

    // Update internal roots and stops
    fRootLinks.Clear;
    UpdateInternalList('RootLinks', fRootLinks);
    fStopLinks.Clear;
    UpdateInternalList('StopLinks', fStopLinks);
  finally
    CopyFromIni.Free;
  end;
end;

procedure TModelConfig.CopyConfigFromModelTemplate;
begin
  ModelTemplateConfig.CopyTemplateTo(fConfig);
end;

procedure TModelConfig.CopyConfigFromSystem;
begin
  SystemConfig.CopySystemToModelConfig(fConfig);
  GetMDBFiles;
  GetStandardDirectories;
end;

constructor TModelConfig.Create(AFileName: TFileName);
begin
  fConfig := TIniConfigFile.Create(AFileName);
  fConfig.WriteString(AdminSection, NameKey, ExtractFileDir(AFileName));

  // Get MDB Files
  fMDBFiles := THashedStringList.Create;
  GetMDBFiles;

  // Set MDB files root to current path
  SetMDBRootToModel;

  // Set code entries to system paths
  // Check to see if the path exist, and if not, revert to system path

  // Set master entries to system paths
  // Check to see if the path exists, and if not, revert to system path

  // Get Standard Directories
  fStandardDirectories := THashedStringList.Create;
  GetStandardDirectories;

  fRootLinks := THashedStringList.Create;
  GetRootLinks;

  fForcedRootLinks := THashedStringList.Create;
  GetForcedRootLinks;

  fStopLinks := THashedStringList.Create;
  GetStopLinks;

  fForcedStopLinks := THashedStringList.Create;
  GetForcedStopLinks;

  fStormsToBuild := TStringList.Create;
  fStormsToBuild.Delimiter := ';';
  GetStormsToBuildFromConfig;
end;

destructor TModelConfig.Destroy;
begin
  fMDBFiles.Free;
  fStandardDirectories.Free;
  fRootLinks.Free;
  fStopLinks.Free;
  fStormsToBuild.Free;
  inherited;
end;

function TModelConfig.GetAndUpdateString(SectionName: String; KeyName: String;
  DefaultValue: String): String;
begin
  Result := fConfig.ReadString(SectionName, KeyName, DefaultValue);
  fConfig.WriteString(SectionName, KeyName, Result);
  fConfig.UpdateFile;
end;

function TModelConfig.GetAndUpdateBool(SectionName: String; KeyName: String;
  DefaultValue: Boolean): Boolean;
begin
  Result := fConfig.ReadBool(SectionName, KeyName, DefaultValue);
  fConfig.WriteBool(SectionName, KeyName, Result);
  fConfig.UpdateFile;
end;

function TModelConfig.GetAndUpdateDateTime(SectionName: String; KeyName: String;
  DefaultValue: TDateTime): TDateTime;
begin
  Result := fConfig.ReadDateTime(SectionName, KeyName, DefaultValue);
  fConfig.WriteDateTime(SectionName, KeyName, Result);
  fConfig.UpdateFile;
end;

function TModelConfig.GetAndUpdateInt(SectionName: String; KeyName: String;
  DefaultValue: Integer): Integer;
begin
  Result := fConfig.ReadInteger(SectionName, KeyName, DefaultValue);
  fConfig.WriteInteger(SectionName, KeyName, Result);
  fConfig.UpdateFile;
end;

function TModelConfig.GetCreatedVia: String;
begin
  Result := GetAndUpdateString(AdminSection, CreatedViaKey, '');
end;

function TModelConfig.GetDebugFileName: TFileName;
begin
  Result := GetAndUpdateString(TreeVerseSection, DebugFileKey, '');
end;

function TModelConfig.GetDebugTrace: Boolean;
begin
  Result := GetAndUpdateBool(TreeVerseSection, DebugTraceKey, False);
end;

function TModelConfig.GetDescription: String;
begin
  Result := GetAndUpdateString(AdminSection, DescriptionKey, '');
end;

function TModelConfig.GetDSNodeField: String;
begin
  Result := GetAndUpdateString(TreeVerseSection, DSNodeKey, '');
end;

function TModelConfig.GetElementField: String;
begin
  Result := GetAndUpdateString(TreeVerseSection, ElementKey, '');
end;

function TModelConfig.GetEMGWorkbenchPath: String;
begin
  Result := GetAndUpdateString(CodeSection, EMGWorkbenchKey, '');
end;

function TModelConfig.GetEngineExportFileName: TFileName;
begin
  Result := GetAndUpdateString(SimulationSection, EngineFileKey, DefaultEngineFileValue);
end;

function TModelConfig.GetFileName: String;
begin
  Result := fConfig.FileName;
end;

function TModelConfig.GetForcedRootLink(Index: Integer): Integer;
begin
  Result := StrToInt(fForcedRootLinks.Names[Index]);
end;

function TModelConfig.GetForcedRootLinkAsString(Index: Integer): String;
begin
  Result := fForcedRootLinks.Names[Index];
end;

function TModelConfig.GetForcedRootLinkComment(Index: Integer): String;
begin
  Result := fForcedRootLinks.Values[fForcedRootLinks.Names[Index]];
end;

procedure TModelConfig.GetForcedRootLinks;
begin
  fConfig.ReadSectionItems(ForcedRootLinksSection, fForcedRootLinks);
end;

function TModelConfig.GetForcedRootLinksCount: Integer;
begin
  Result := fForcedRootLinks.Count;
end;

function TModelConfig.GetForcedStopLink(Index: Integer): Integer;
begin
  Result := StrToInt(fForcedStopLinks.Names[Index]);
end;

function TModelConfig.GetForcedStopLinkAsString(Index: Integer): String;
begin
  Result := fForcedStopLinks.Names[Index];
end;

function TModelConfig.GetForcedStopLinkComment(Index: Integer): String;
begin
  Result := fForcedStopLinks.Values[fForcedStopLinks.Names[Index]];
end;

procedure TModelConfig.GetForcedStopLinks;
begin
  fConfig.ReadSectionItems(ForcedStopLinksSection, fForcedStopLinks);
end;

function TModelConfig.GetForcedStopLinksCount: Integer;
begin
  Result := fForcedStopLinks.Count;
end;

function TModelConfig.GetForceMDBRefresh: Boolean;
begin
  Result := GetAndUpdateBool(AdminSection, ForceMDBRefreshKey, True);
end;

function TModelConfig.GetHasDirectSubcatchments: Boolean;
begin
  Result := GetAndUpdateBool(ModelStateSection, HasDirectSubcatchmentsKey, False);
end;

function TModelConfig.GetHasNetwork: Boolean;
begin
  Result := GetAndUpdateBool(ModelStateSection, HasNetworkKey, False);
end;

function TModelConfig.GetHasSkeletonModel: Boolean;
begin
  Result := GetAndUpdateBool(ModelStateSection, HasSkeletonModelKey, False)
end;

function TModelConfig.GetHasStandardDirectories: Boolean;
begin
  Result := GetAndUpdateBool(ModelStateSection, HasStandardDirectoriesKey, False)
end;

function TModelConfig.GetHasSurfaceSubcatchments: Boolean;
begin
  Result := GetAndUpdateBool(ModelStateSection, HasSurfaceSubcatchmentsKey, False);
end;

function TModelConfig.GetHydroQCFileName: string;
begin
  Result := GetAndUpdateString(QCSection, HydroQCFileNameKey, DefaultHydroQCFileName);
end;

function TModelConfig.GetLinkExistingTable: String;
begin
  Result := GetAndUpdateString(TreeVerseSection, LinkExistingKey, '');
end;

function TModelConfig.GetLinkField: String;
begin
  Result := GetAndUpdateString(TreeVerseSection, LinkKey, '');
end;

function TModelConfig.GetLinkFutureTable: String;
begin
  Result := GetAndUpdateString(TreeVerseSection, LinkFutureKey, '');
end;

function TModelConfig.GetMasterLinksPath: String;
begin
  Result := GetAndUpdateString(MasterFilesSection, MasterLinksKey, '');
end;

function TModelConfig.GetMasterMDBFileIDs(index: Integer): String;
begin
  Result := fMDBFiles.Names[index];
end;

function TModelConfig.GetMasterMDBFiles(index: Integer): String;
begin
  Result := IncludeTrailingPathDelimiter(fMDBFiles.Values['root']) +
    fMDBFiles.Values[fMDBFiles.Names[index]];
end;

function TModelConfig.GetMasterMDBFilesByName(index: String): String;
var
  i: Integer;
begin
  Result := '';
  for i := 0 to fMDBFiles.Count - 1 do
    if UpperCase(fMDBFiles.Names[i]) = UpperCase(Index) then
    begin
      Result := IncludeTrailingPathDelimiter(fMDBFiles.Values['root']) +
        fMDBFiles.Values[fMDBFiles.Names[i]];
      Exit;
    end;
end;

function TModelConfig.GetMasterMDBFilesCount: Integer;
begin
  Result := fMDBFiles.Count;
end;

function TModelConfig.GetMasterNodesPath: String;
begin
  Result := GetAndUpdateString(MasterFilesSection, MasterNodesKey, '');
end;

function TModelConfig.GetMasterParcelsPath: String;
begin
  Result := GetAndUpdateString(MasterFilesSection, MasterParcelsKey, '');
end;

function TModelConfig.GetMasterQuartersectionPath: String;
begin
  Result := GetAndUpdateString(MasterFilesSection, MasterQuartersectionsKey, '');
end;

function TModelConfig.GetMasterRootPath: String;
begin
  Result := GetAndUpdateString(MasterFilesSection, MasterRootKey, '');
end;

function TModelConfig.GetMasterSpecLinksDataPath: String;
begin
  Result := GetAndUpdateString(MasterFilesSection, MasterSpecLinksDataKey, '')
end;

function TModelConfig.GetMasterSpecLinksPath: String;
begin
  Result := GetAndUpdateString(MasterFilesSection, MasterSpecLinksKey, '')
end;

function TModelConfig.GetMasterSurfacePath: String;
begin
  Result := GetAndUpdateString(MasterFilesSection, MasterSurfaceKey, '')
end;

procedure TModelConfig.GetMDBFiles;
var
  MDBsItems: TStringList;
  i: Integer;
begin
  MDBsItems := TStringList.Create;
  fMDBFiles.Clear;
  try
    fConfig.ReadSectionItems('MDBs', MDBsItems);
    for i := 0 to MDBsItems.Count - 1 do
      fMDBFiles.Add(MDBsItems[i]);
  finally
    MDBsItems.Free;
  end;
end;

function TModelConfig.GetModelCreateDate: TDateTime;
begin
  Result := GetAndUpdateDateTime(AdminSection, CreatedKey, MinDateTime);
end;

function TModelConfig.GetModelEnteredDate: TDateTime;
begin
  Result := GetAndUpdateDateTime(AdminSection, EnteredKey, MinDateTime);
end;

function TModelConfig.GetModelModifiedDate: TDateTime;
begin
  Result := GetAndUpdateDateTime(AdminSection, ModifiedKey, MinDateTime);
  { TODO -oAMM -cFile consistency : Check if modified date > created date and provide warning if not }
end;

function TModelConfig.GetModelName: String;
begin
  Result := GetAndUpdateString(AdminSection, NameKey, '');
end;

function TModelConfig.GetPath: String;
begin
  Result := ExtractFileDir(FileName);
end;

function TModelConfig.GetReachField: String;
begin
  Result := GetAndUpdateString(TreeVerseSection, ReachKey, '');
end;

function TModelConfig.GetRemoveSanitaryUponDeploy: Boolean;
begin
  Result := GetAndUpdateBool(SimulationSection, RemoveSanitaryUponDeployKey, False);
end;

function TModelConfig.GetResultViewsProjectDescription: String;
begin
  Result := GetAndUpdateString(ResultViewsSection, ProjectDescriptionKey, '');
end;

function TModelConfig.GetResultViewsProjectNumber: String;
begin
  Result := GetAndUpdateString(ResultViewsSection, ProjectNumberKey, '');
end;

function TModelConfig.GetResultViewsStudyAreaID: String;
begin
  Result := GetAndUpdateString(ResultViewsSection, StudyAreaIDKey, '');
end;

function TModelConfig.GetRootLink(Index: Integer): Integer;
begin
  Result := StrToInt(fRootLinks.Names[Index]);
end;

function TModelConfig.GetRootLinkAsString(Index: Integer): String;
begin
  Result := fRootLinks.Names[Index];
end;

function TModelConfig.GetRootLinkComment(Index: Integer): String;
begin
  Result := fRootLinks.Values[fRootLinks.Names[Index]];
end;

procedure TModelConfig.GetRootLinks;
begin
  fConfig.ReadSectionItems(RootLinksSection, fRootLinks);
end;

function TModelConfig.GetRootLinksCount: Integer;
begin
  Result := fRootLinks.Count;
end;

function TModelConfig.GetRunoffDeployDate: TDateTime;
begin
  Result := GetAndUpdateDateTime(AdminSection, RunoffDeployedKey, MinDateTime);
end;

function TModelConfig.GetRunoffFileName: TFileName;
begin
  Result := GetAndUpdateString(SimulationSection, RunoffFileKey, '');
end;

procedure TModelConfig.GetStandardDirectories;
var
  StandardDirectoriesItems: TStringList;
  i: Integer;
begin
  StandardDirectoriesItems := TStringList.Create;
  fStandardDirectories.Clear;
  try
    fConfig.ReadSectionItems('StandardDirectories', StandardDirectoriesItems);
    for i := 0 to StandardDirectoriesItems.Count - 1 do
      fStandardDirectories.Add(StandardDirectoriesItems[i]);
    CodeSite.Send('StandardDirectoriesItems.Count', StandardDirectoriesItems.Count);
  finally
    StandardDirectoriesItems.Free;
  end;
end;

function TModelConfig.GetStandardDirectory(Index: Integer): String;
begin
  Result := fStandardDirectories.Values[fStandardDirectories.Names[Index]];
end;

function TModelConfig.GetStandardDirectoryByName(Index: String): String;
var
  i: Integer;
begin
  Result := '';
  for i := 0 to fStandardDirectories.Count - 1 do
    if UpperCase(fStandardDirectories.Names[i]) = UpperCase(Index) then
    begin
      Result := fStandardDirectories.Values[fStandardDirectories.Names[i]];
      Exit;
    end;
end;

function TModelConfig.GetStandardDirectoryCount: Integer;
begin
  Result := fStandardDirectories.Count;
end;

function TModelConfig.GetStopLink(Index: Integer): Integer;
begin
  Result := StrToInt(fStopLinks.Names[Index]);
end;

function TModelConfig.GetStopLinkAsString(Index: Integer): String;
begin
  Result := fStopLinks.Names[Index];
end;

function TModelConfig.GetStopLinkComment(Index: Integer): String;
begin
  Result := fStopLinks.Values[fStopLinks.Names[Index]];
end;

procedure TModelConfig.GetStopLinks;
begin
  fConfig.ReadSectionItems(StopLinksSection, fStopLinks);
end;

function TModelConfig.GetStopLinksCount: Integer;
begin
  Result := fStopLinks.Count;
end;

function TModelConfig.GetStormsToBuild(Index: Integer): String;
begin
  if Index >= fStormsToBuild.Count then
    Result := ''
  else
    Result := fStormsToBuild[Index];
end;

function TModelConfig.GetStormsToBuildCount: Integer;
begin
  Result := fStormsToBuild.Count;
end;

procedure TModelConfig.GetStormsToBuildFromConfig;
var
  StormsToBuildItem: String;
begin
  StormsToBuildItem := fConfig.ReadString(SimulationSection, StormsToBuildKey, '');
  fStormsToBuild.DelimitedText := StormsToBuildItem;
end;

function TModelConfig.GetStormsToBuildList: String;
begin
  Result := fStormsToBuild.DelimitedText;
end;

function TModelConfig.GetSystemMDBRefreshDate: TDateTime;
begin
  Result := GetAndUpdateDateTime(AdminSection, SystemMDBUpdateDateKey, MinDateTime);
end;

function TModelConfig.GetTimeFrame: TTimeFrame;
var
  TimeFrameFromConfig: String;
begin
  { TODO -oAMM -cFile consistency : Time frame in config file needs to be fixed (do we still need to address more than EX and FU?) }
  TimeFrameFromConfig := fConfig.ReadString(ModelStateSection, TimeFrameKey, '');
  if UpperCase(TimeFrameFromConfig) = 'EX' then
    Result := tfExisting
  else
    Result := tfFuture;
end;

function TModelConfig.GetTitle: String;
begin
  Result := GetAndUpdateString(AdminSection, TitleKey, '');
end;

function TModelConfig.GetTraceFileName: TFileName;
begin
  Result := GetAndUpdateString(TreeVerseSection, TraceFileKey, '');
end;

function TModelConfig.GetTraceSourceDatabase: TFileName;
begin
  Result := GetAndUpdateString(TreeVerseSection, SourceDatabaseKey, '');
end;

function TModelConfig.GetTraceSourceTable: String;
begin
  Result := GetAndUpdateString(TreeVerseSection, SourceTableKey, '');
end;

function TModelConfig.GetTraceStormwater: Boolean;
begin
  Result := GetAndUpdateBool(TreeVerseSection, TraceStormwaterKey, False);
end;

function TModelConfig.GetTransportFileName: TFileName;
begin
  Result := GetAndUpdateString(SimulationSection, TransportFileKey, '');
end;

function TModelConfig.GetUseBaseflow: Boolean;
begin
  Result := GetAndUpdateBool(SimulationSection, UseBaseflowKey, True);
end;

function TModelConfig.GetUSNodeField: String;
begin
  Result := GetAndUpdateString(TreeVerseSection, USNodeKey, '');
end;

function TModelConfig.GetVersion: String;
begin
  Result := GetAndUpdateString(ControlSection, VersionKey, '');
end;

procedure TModelConfig.Revert;
begin
  fConfig.RevertFile;
end;

procedure TModelConfig.RemoveRootLink(ARoot: Integer);
var
  IndexToDelete: Integer;
begin
  IndexToDelete := fRootLinks.IndexOf(IntToStr(ARoot));
  if IndexToDelete > -1 then
    fRootLinks.Delete(IndexToDelete);
end;

procedure TModelConfig.RemoveRootLinkByIndex(Index: Integer);
begin
  fRootLinks.Delete(Index);
end;

procedure TModelConfig.RemoveStopLink(AStop: Integer);
var
  IndexToDelete: Integer;
begin
  IndexToDelete := fRootLinks.IndexOf(IntToStr(AStop));
  if IndexToDelete > -1 then
    fStopLinks.Delete(IndexToDelete);
end;

procedure TModelConfig.RemoveStopLinkByIndex(Index: Integer);
begin
  fStopLinks.Delete(Index);
end;

procedure TModelConfig.SetDebugFileName(const Value: TFileName);
begin
  fConfig.WriteString(TreeVerseSection, DebugFileKey, Value);
end;

procedure TModelConfig.SetDebugTrace(const Value: Boolean);
begin
  fConfig.WriteBool(TreeVerseSection, DebugTraceKey, Value);
end;

procedure TModelConfig.SetDescription(const Value: String);
begin
  fConfig.WriteString(AdminSection, DescriptionKey, Value);
end;

procedure TModelConfig.SetDSNodeField(const Value: String);
begin
  fConfig.WriteString(TreeVerseSection, DSNodeKey, Value);
end;

procedure TModelConfig.SetElementField(const Value: String);
begin
  fConfig.WriteString(TreeVerseSection, ElementKey, Value);
end;

procedure TModelConfig.SetEMGWorkbenchPath(const Value: String);
begin
  fConfig.WriteString(CodeSection, EMGWorkbenchKey, Value);
end;

procedure TModelConfig.SetEMGWorkbenchToSystem;
begin
  fConfig.WriteString(CodeSection, EMGWorkbenchKey, SystemConfig.EMGWorkbench);
end;

procedure TModelConfig.SetEngineExportFileName(const Value: TFileName);
begin
  fConfig.WriteString(SimulationSection, EngineFileKey, Value);
end;

procedure TModelConfig.SetForcedRootLink(Index: Integer; const Value: Integer);
begin
  fForcedRootLinks[Index] := Format('%d=%s', [Value, fForcedRootLinks.Values[fForcedRootLinks.Names[Index]]]);
end;

procedure TModelConfig.SetForcedRootLinkComment(Index: Integer;
  const Value: String);
begin
  fForcedRootLinks[Index] := Format('%s=%s', [fForcedRootLinks.Names[Index], Value]);
end;

procedure TModelConfig.SetForcedStopLink(Index: Integer; const Value: Integer);
begin
  fForcedStopLinks[Index] := Format('%d=%s', [Value, fForcedStopLinks.Values[fForcedStopLinks.Names[Index]]]);
end;

procedure TModelConfig.SetForcedStopLinkComment(Index: Integer;
  const Value: String);
begin
  fForcedStopLinks[Index] := Format('%s=%s', [fForcedStopLinks.Names[Index], Value]);
end;

procedure TModelConfig.SetForceMDBRefresh(const Value: Boolean);
begin
  fConfig.WriteBool(AdminSection, ForceMDBRefreshKey, Value);
end;

procedure TModelConfig.SetHasDirectSubcatchments(const Value: Boolean);
begin
  fConfig.WriteBool(ModelStateSection, HasDirectSubcatchmentsKey, Value);
end;

procedure TModelConfig.SetHasNetwork(const Value: Boolean);
begin
  fConfig.WriteBool(ModelStateSection, HasNetworkKey, Value);
end;

procedure TModelConfig.SetHasSkeletonModel(const Value: Boolean);
begin
  fConfig.WriteBool(ModelStateSection, HasSkeletonModelKey, Value);
end;

procedure TModelConfig.SetHasStandardDirectories(const Value: Boolean);
begin
  fConfig.WriteBool(ModelStateSection, HasStandardDirectoriesKey, Value);
end;

procedure TModelConfig.SetHasSurfaceSubcatchments(const Value: Boolean);
begin
  fConfig.WriteBool(ModelStateSection, HasSurfaceSubcatchmentsKey, Value);
end;

procedure TModelConfig.SetHydroQCFileName(const Value: string);
begin
  fConfig.WriteString(QCSection, HydroQCFileNameKey, Value);
end;

procedure TModelConfig.SetLinkExistingTable(const Value: String);
begin
  fConfig.WriteString(TreeVerseSection, LinkExistingKey, Value);
end;

procedure TModelConfig.SetLinkField(const Value: String);
begin
  fConfig.WriteString(TreeVerseSection, LinkKey, Value);
end;

procedure TModelConfig.SetLinkFutureTable(const Value: String);
begin
  fConfig.WriteString(TreeVerseSection, LinkFutureKey, Value);
end;

procedure TModelConfig.SetMasterLinksPath(const Value: String);
begin
  fConfig.WriteString(MasterFilesSection, MasterLinksKey, Value);
end;

procedure TModelConfig.SetMasterNodesPath(const Value: String);
begin
  fConfig.WriteString(MasterFilesSection, MasterNodesKey, Value);
end;

procedure TModelConfig.SetMasterParcelsPath(const Value: String);
begin
  fConfig.WriteString(MasterFilesSection, MasterParcelsKey, Value);
end;

procedure TModelConfig.SetMasterQuartersectionPath(const Value: String);
begin
  fConfig.WriteString(MasterFilesSection, MasterQuartersectionsKey, Value);
end;

procedure TModelConfig.SetMasterRootPath(const Value: String);
begin
  fConfig.WriteString(MasterFilesSection, MasterRootKey, Value);
end;

procedure TModelConfig.SetMasterSpecLinksDataPath(const Value: String);
begin
  fConfig.WriteString(MasterFilesSection, MasterSpecLinksDataKey, Value);
end;

procedure TModelConfig.SetMasterSpecLinksPath(const Value: String);
begin
  fConfig.WriteString(MasterFilesSection, MasterSpecLinksKey, Value);
end;

procedure TModelConfig.SetMasterSurfacePath(const Value: String);
begin
  fConfig.WriteString(MasterFilesSection, MasterSurfaceKey, Value);
end;

procedure TModelConfig.SetMDBRootToModel;
begin
  fConfig.WriteString('MDBs', 'root', IncludeTrailingPathDelimiter(Path)+'mdbs');
  fMDBFiles.Values['root'] := IncludeTrailingPathDelimiter(Path)+'mdbs';
  fConfig.UpdateFile;
end;

procedure TModelConfig.SetMDBRootToSystem;
begin
  fConfig.WriteString('MDBs', 'root',
    IncludeTrailingPathDelimiter(SystemConfig.MDBsByName['root']));
  fMDBFiles.Values['root'] :=
    IncludeTrailingPathDelimiter(SystemConfig.MDBsByName['root']);
  fConfig.UpdateFile;
end;

procedure TModelConfig.SetModelCreateDate(const Value: TDateTime);
begin
  fConfig.WriteDateTime(AdminSection, CreatedKey, Value);
end;

procedure TModelConfig.SetModelEnteredDate(const Value: TDateTime);
begin
  fConfig.WriteDateTime(AdminSection, EnteredKey, Value);
end;

procedure TModelConfig.SetModelModifiedDate(const Value: TDateTime);
begin
  fConfig.WriteDateTime(AdminSection, ModifiedKey, Value);
end;

procedure TModelConfig.SetReachField(const Value: String);
begin
  fConfig.WriteString(TreeVerseSection, ReachKey, Value);
end;

procedure TModelConfig.SetResultViewsProjectDescription(const Value: String);
begin
  fConfig.WriteString(ResultViewsSection, ProjectDescriptionKey, Value);
end;

procedure TModelConfig.SetResultViewsProjectNumber(const Value: String);
begin
  fConfig.WriteString(ResultViewsSection, ProjectNumberKey, Value);
end;

procedure TModelConfig.SetResultViewsStudyAreaID(const Value: String);
begin
  fConfig.WriteString(ResultViewsSection, StudyAreaIDKey, Value);
end;

procedure TModelConfig.SetRemoveSanitaryUponDeploy(const Value: Boolean);
begin
  fConfig.WriteBool(SimulationSection, RemoveSanitaryUponDeployKey, Value);
end;

procedure TModelConfig.SetRootLink(Index: Integer; const Value: Integer);
begin
  fRootLinks[Index] := Format('%d=%s', [Value, fRootLinks.Values[fRootLinks.Names[Index]]]);
end;

procedure TModelConfig.SetRootLinkComment(Index: Integer; const Value: String);
begin
  fRootLinks[Index] := Format('%s=%s', [fRootLinks.Names[Index], Value]);
end;

procedure TModelConfig.SetRunoffDeployDate(const Value: TDateTime);
begin
  fConfig.WriteDateTime(AdminSection, RunoffDeployedKey, Value);
end;

procedure TModelConfig.SetRunoffFileName(const Value: TFileName);
begin
  fConfig.WriteString(SimulationSection, RunoffFileKey, Value);
end;

procedure TModelConfig.SetStopLink(Index: Integer; const Value: Integer);
begin
  fStopLinks[Index] := Format('%s=%s', [Value, fStopLinks.Values[fStopLinks.Names[Index]]]);
end;

procedure TModelConfig.SetStopLinkComment(Index: Integer; const Value: String);
begin
  fStopLinks[Index] := Format('%s=%s', [fStopLinks.Names[Index], Value]);
end;

procedure TModelConfig.SetStormsToBuildList(const Value: String);
begin
  fStormsToBuild.DelimitedText := Value;
end;

procedure TModelConfig.SetSystemMDBRefreshDate(const Value: TDateTime);
begin
  fConfig.WriteDateTime(AdminSection, SystemMDBUpdateDateKey, Value);
end;

procedure TModelConfig.SetTimeFrame(const Value: TTimeFrame);
begin
  if Value = tfExisting then
    fConfig.WriteString(ModelStateSection, TimeFrameKey, 'EX')
  else
    fConfig.WriteString(ModelStateSection, TimeFrameKey, 'FU')
end;

procedure TModelConfig.SetTitle(const Value: String);
begin
  fConfig.WriteString(AdminSection, DescriptionKey, Value);
end;

procedure TModelConfig.SetTraceFileName(const Value: TFileName);
begin
  fConfig.WriteString(TreeVerseSection, TraceFileKey, Value);
end;

procedure TModelConfig.SetTraceSourceDatabase(const Value: TFileName);
begin
  fConfig.WriteString(TreeVerseSection, SourceDatabaseKey, Value);
end;

procedure TModelConfig.SetTraceSourceTable(const Value: String);
begin
  fConfig.WriteString(TreeVerseSection, SourceTableKey, Value);
end;

procedure TModelConfig.SetTraceStormwater(const Value: Boolean);
begin
  fConfig.WriteBool(TreeVerseSection, TraceStormwaterKey, Value);
end;

procedure TModelConfig.SetTransportFileName(const Value: TFileName);
begin
  fConfig.WriteString(SimulationSection, TransportFileKey, Value);
end;

procedure TModelConfig.SetUseBaseflow(const Value: Boolean);
begin
  fConfig.WriteBool(SimulationSection, UseBaseflowKey, Value);
end;

procedure TModelConfig.SetUSNodeField(const Value: String);
begin
  fConfig.WriteString(TreeVerseSection, USNodeKey, Value);
end;

procedure TModelConfig.Update;
var
  i: Integer;
begin
  // Process Roots and Stops
  fConfig.EraseSection(RootLinksSection);
  for i := 0 to fRootLinks.Count - 1 do
    fConfig.WriteString(RootLinksSection, fRootLinks.Names[i],
      fRootLinks.Values[fRootLinks.Names[i]]);

  fConfig.EraseSection(StopLinksSection);
  for i := 0 to fStopLinks.Count - 1 do
    fConfig.WriteString(StopLinksSection, fStopLinks.Names[i],
      fStopLinks.Values[fStopLinks.Names[i]]);

  // Process Storms
  fConfig.WriteString(SimulationSection, StormsToBuildKey, fStormsToBuild.DelimitedText);

  fConfig.UpdateFile;
end;

end.
