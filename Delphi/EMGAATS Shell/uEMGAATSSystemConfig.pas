unit uEMGAATSSystemConfig;

interface

uses SysUtils, Classes, uConfigFiles, uMRUStringList, IniFiles,
  uEMGAATSModelConfig;

type
  TVitalProgramsAction = (vpaCloseAll, vpaContinue, vpaExit);
  {
  TSystemConfig
    This class manages the main EMGAATS config file and user preferences for the
    application, including window positions and most recently used files.  The
    main EMGAATS config file is stored in the application directory and the
    user preferences config file is stored in the user's Documents and Settings
    directory.
  }
  TSystemConfig = class
  private
    fStandardDirectories: THashedStringList;
    fSystemConfigFile: IConfigFile;
    fUserConfigFile: IConfigFile;
    fMasterFilesConfigFile: IConfigFile;
    fMRUModels: TMRUStringList;
    fDataAccessSources: THashedStringList;
    fMasterFiles: THashedStringList;
    fMDBs: THashedStringList;
    function GetVersion: String;
    function GetRefreshMDBs: Boolean;
    function GetStandardDirectory(Index: Integer): String;
    function GetStandardDirectoryCount: Integer;
    function GetMRUModel(Index: Integer): TFileName;
    function GetMRUModelsCount: Integer;
    procedure SetMRUModelCountLimit(Value: Integer);
    function GetStandardDirectoryByName(Index: String): String;
    procedure SetStandardDirectory(Index: Integer; AValue: String);
    function GetDataAccessSource(Index: Integer): String;
    function GetDataAccessSourceCount: Integer;
    function GetDataAccessSourcesByName(Index: String): String;
    function GetDataAccessName: String;
    procedure SetDataAccessName(const Value: String);
    function GetDataAccessID(Index: Integer): String;
    function GetEMGWorkbench: String;
    function GetMasterFile(Index: Integer): String;
    function GetMasterFileByName(Index: String): String;
    function GetMasterFileCount: Integer;
    function GetMDB(Index: Integer): String;
    function GetMDBByName(Index: String): String;
    function GetMDBCount: Integer;
    function GetStandardDirectoryID(Index: Integer): String;
    function GetShowSplashScreen: Boolean;
    procedure SetShowSplashScreen(const Value: Boolean);
    function GetShowVitalProgramsWarning: Boolean;
    procedure SetShowVitalProgramsWarning(const Value: Boolean);
    function GetVitalProgramsAction: TVitalProgramsAction;
    procedure SetVitalProgramsAction(const Value: TVitalProgramsAction);
    function GetDataAccessFileByName(Index: String): String;
    function GetQCWorkspace: String;
    function GetStudyAreaMaxExtents: String;
    procedure SetStudyAreaMaxExtents(const Value: String);
    function GetMSAccessWaitMilliseconds: Integer;
    procedure SetMSAccessWaitMilliseconds(const Value: Integer);
    function GetBasinBoundaries: String;
    procedure SetBasinBoundaries(const Value: String);
    function GetMasterFilesVersion: String;
    function GetMasterFilesConfigFileName: String;
    function GetLayoutSetupFileName_MapLandscape_LegendLandscape: String;
    function GetLayoutSetupFileName_MapLandscape_LegendPortrait: String;
    function GetLayoutSetupFileName_MapPortrait_LegendLandscape: String;
    function GetLayoutSetupFileName_MapPortrait_LegendPortrait: String;
  public
    constructor Create;
    destructor Destroy; override;

    //
    // Admin section
    //

    // Version: version string for the system. Used to check models against the
    // current system, and to prevent them from being opened by the wrong version.
    // Can also be used to patch older versions to the current version.
    property Version: String read GetVersion;
    property MasterFilesVersion: String read GetMasterFilesVersion;
    property MasterFilesConfigFileName: String read GetMasterFilesConfigFileName;

    // RefreshMDBs: user sets to false to prevent propagation of system MDBs
    // to model databases.  Systemwide setting.
    property RefreshMDBs: Boolean read GetRefreshMDBs;

    //
    // MasterFiles section
    //
    property MasterFiles[Index: Integer]: String read GetMasterFile;
    property MasterFilesByName[Index: String]: String read GetMasterFileByName;
    property MasterFileCount: Integer read GetMasterFileCount;

    //
    // MDBs section
    //
    property MDBs[Index: Integer]: String read GetMDB;
    property MDBsByName[Index: String]: String read GetMDBByName;
    property MDBCount: Integer read GetMDBCount;

    //
    // Code section
    //
    property EMGWorkbench: String read GetEMGWorkbench;
    property QCWorkspace: String read GetQCWorkspace;

    //
    // StandardDirectories section
    //
    property StandardDirectories[Index: Integer]: String read GetStandardDirectory;
    property StandardDirectoriesByName[Index: String]: String read GetStandardDirectoryByName;
    property StandardDirectoriesID[Index: Integer]: String read GetStandardDirectoryID;
    property StandardDirectoryCount: Integer read GetStandardDirectoryCount;

    //
    // Most-recently used models
    //
    property MRUModels[Index: Integer]: TFileName read GetMRUModel;
    procedure AddMRUModel(AFileName: TFileName);
    property MRUModelsCount: Integer read GetMRUModelsCount;

    //
    // DataAccess section
    //
    property DataAccessName: String read GetDataAccessName write SetDataAccessName;
    property DataAccessSources[Index: Integer]: String read GetDataAccessSource;
    property DataAccessSourceCount: Integer read GetDataAccessSourceCount;
    property DataAccessSourcesByName[Index: String]: String read GetDataAccessSourcesByName;
    property DataAccessID[Index: Integer]: String read GetDataAccessID;
    property DataAccessFileByName[Index: String]: String read GetDataAccessFileByName;

    // Model initialization
    procedure CopySystemToModelConfig(AConfig: IConfigFile);

    // Application settings
    property ShowSplashScreen: Boolean read GetShowSplashScreen write SetShowSplashScreen;
    property ShowVitalProgramsWarning: Boolean read GetShowVitalProgramsWarning
      write SetShowVitalProgramsWarning;
    property VitalProgramsAction: TVitalProgramsAction read GetVitalProgramsAction
      write SetVitalProgramsAction;
    property MSAccessWaitMilliseconds: Integer read GetMSAccessWaitMilliseconds
      write SetMSAccessWaitMilliseconds;

    // ExternMaps settings
    property StudyAreaMaxExtents: String read GetStudyAreaMaxExtents
      write SetStudyAreaMaxExtents;
    property BasinBoundaries: String read GetBasinBoundaries write SetBasinBoundaries;

    // ResultView settings
    property LayoutSetupFileName_MapLandscape_LegendPortrait: String
      read GetLayoutSetupFileName_MapLandscape_LegendPortrait;
    property LayoutSetupFileName_MapLandscape_LegendLandscape: String
      read GetLayoutSetupFileName_MapLandscape_LegendLandscape;
    property LayoutSetupFileName_MapPortrait_LegendPortrait: String
      read GetLayoutSetupFileName_MapPortrait_LegendPortrait;
    property LayoutSetupFileName_MapPortrait_LegendLandscape: String
      read GetLayoutSetupFileName_MapPortrait_LegendLandscape;
    // Operations
    procedure Update;
  end;

var
  SystemConfig: TSystemConfig;

implementation

uses GlobalConstants, Dialogs, uUtilities, StrUtils, StStrL;

{ TSystemConfig }

procedure TSystemConfig.AddMRUModel(AFileName: TFileName);
begin
  fMRUModels.Insert(AFileName);
end;

procedure TSystemConfig.CopySystemToModelConfig(AConfig: IConfigFile);
begin
  fSystemConfigFile.CopySectionToConfig('Admin', AConfig);
  fSystemConfigFile.CopySectionToConfig('MasterFiles', AConfig);
  fSystemConfigFile.CopySectionToConfig('MDBs', AConfig);
  fSystemConfigFile.CopySectionToConfig('Code', AConfig);
  fSystemConfigFile.CopySectionToConfig('StandardDirectories', AConfig);
  fSystemConfigFile.CopySectionToConfig('DataAccess', AConfig);
  fUserConfigFile.CopySectionToConfig('Simulation', AConfig);
end;

constructor TSystemConfig.Create;
var
  AppUserConfigFileName: TFileName;
  AppConfigPath: String;
  NewUserConfigFile: TFileStream;
  TemplateUserConfigPath: String;
  TemplateUserConfigFile: TFileStream;
  TemplateUserConfig: IConfigFile;
  MRUSectionItems: TStringList;
  MRUSectionKeys: TStringList;
  ItemValue: String;
  i: Integer;
  MasterFilesItems: TStringList;
  MDBsItems: TStringList;
  StandardDirectoriesItems: TStringList;
  DataAccessItems: TStringList;
begin
  // Load system configuration items
  AppConfigPath := GetApplicationPath + SystemConfigFileName;
  if not FileExists(AppConfigPath) then
    raise Exception.Create('EMGAATS configuration file missing.  Please restore.');
  fSystemConfigFile := TIniConfigFile.Create(AppConfigPath);

  //
  // Load user preferences items, or create it if it doesn't exist
  //

  // Get user config file or create it if it doesn't exist; restore missing prefs
  // if they are missing (or add them if they were inserted into the template!)
  AppUserConfigFileName := GetApplicationUserConfigFileName;
  if not DirectoryExists(GetApplicationUserConfigPath) then
    ForceDirectories(GetApplicationUserConfigPath);
  TemplateUserConfigPath := GetApplicationPath + TemplateUserConfigFileName;
  if not FileExists(AppUserConfigFileName) then
  begin
    NewUserConfigFile := TFileStream.Create(AppUserConfigFileName, fmCreate);
    TemplateUserConfigFile := TFileStream.Create(TemplateUserConfigPath, fmShareDenyRead);
    try
      NewUserConfigFile.CopyFrom(TemplateUserConfigFile, TemplateUserConfigFile.Size);
    finally
      NewUserConfigFile.Free;
      TemplateUserConfigFile.Free;
    end;
  end;
  fUserConfigFile := TIniConfigFile.Create(AppUserConfigFileName);
  TemplateUserConfig := TIniConfigFile.Create(TemplateUserConfigPath);
  fUserConfigFile.UpdateFromTemplateConfig(TemplateUserConfig);

  // Get MRU List, sorted so that File00 is the most-recent, higher numbers are
  // older
  fMRUModels := TMRUStringList.Create;
  fMRUModels.CountLimit := fUserConfigFile.ReadInteger('MRUModelsLimit', 'Limit', 4);
  MRUSectionItems := TStringList.Create;
  MRUSectionKeys := TStringList.Create;
  MRUSectionKeys.Sort;
  try
    fUserConfigFile.ReadSectionItems('MRUModels', MRUSectionItems);
    fUserConfigFile.ReadSection('MRUModels', MRUSectionKeys);
    for i := MRUSectionKeys.Count - 1 downto 0 do
    begin
      ItemValue := RightStr(MRUSectionItems[i], Length(MRUSectionItems[i]) -
        Pos('=', MRUSectionItems[i]));
      fMRUModels.Insert(ItemValue);
    end;
  finally
    MRUSectionItems.Free;
    MRUSectionKeys.Free;
  end;

  // Get Master Files
  fMasterFiles := THashedStringList.Create;
  MasterFilesItems := TStringList.Create;
  try
    fSystemConfigFile.ReadSectionItems('MasterFiles', MasterFilesItems);
    for i := 0 to MasterFilesItems.Count - 1 do
      fMasterFiles.Add(MasterFilesItems[i]);
  finally
    MasterFilesItems.Free;
  end;

  // Load master files configuration items
  fMasterFilesConfigFile := TIniConfigFile.Create(MasterFilesConfigFileName);

  // Get MDBs
  fMDBs := THashedStringList.Create;
  MDBsItems := TStringList.Create;
  try
    fSystemConfigFile.ReadSectionItems('MDBs', MDBsItems);
    for i := 0 to MDBsItems.Count - 1 do
      fMDBs.Add(MDBsItems[i]);
  finally
    MDBsItems.Free;
  end;

  // Get Standard Directories
  fStandardDirectories := THashedStringList.Create;
  StandardDirectoriesItems := TStringList.Create;
  try
    fSystemConfigFile.ReadSectionItems('StandardDirectories', MDBsItems);
    for i := 0 to StandardDirectoriesItems.Count - 1 do
      fStandardDirectories.Add(StandardDirectoriesItems[i]);
  finally
    StandardDirectoriesItems.Free;
  end;

  // Get Data Access Sources
  fDataAccessSources := THashedStringList.Create;
  DataAccessItems := TStringList.Create;
  try
    fSystemConfigFile.ReadSectionItems('DataAccess', DataAccessItems);
    for i := 0 to DataAccessItems.Count - 1 do
      fDataAccessSources.Add(DataAccessItems[i]);
  finally
    DataAccessItems.Free;
  end;

end;

destructor TSystemConfig.Destroy;
begin
  fMasterFiles.Free;
  fMDBs.Free;
  fStandardDirectories.Free;
  fMRUModels.Free;
  inherited;
end;

// Retrieves relative path to data access files
function TSystemConfig.GetBasinBoundaries: String;
begin
  Result := fUserConfigFile.ReadString('ExternMaps', 'BasinBoundaries', '');
end;

function TSystemConfig.GetDataAccessFileByName(Index: String): String;
var
  Tokens: TStringList;
  DataAccessSource: String;
begin
  DataAccessSource := DataAccessSourcesByName[Index];

  Tokens := TStringList.Create;
  try
    ExtractTokensL(DataAccessSource, ';', '"', False, Tokens);
    Result := Tokens[1];
  finally
    Tokens.Free;
  end;
end;

function TSystemConfig.GetDataAccessID(Index: Integer): String;
begin
  Result := fDataAccessSources.Names[Index];
end;

function TSystemConfig.GetDataAccessName: String;
begin
  Result := fSystemConfigFile.ReadString('DataAccess', 'FileName', '');
end;

function TSystemConfig.GetDataAccessSource(Index: Integer): String;
begin
  Result := fDataAccessSources.Values[fDataAccessSources.Names[Index]];
end;

function TSystemConfig.GetDataAccessSourceCount: Integer;
begin
  Result := fDataAccessSources.Count;
end;

function TSystemConfig.GetDataAccessSourcesByName(Index: String): String;
begin
  Result := fDataAccessSources.Values[Index];
end;

function TSystemConfig.GetEMGWorkbench: String;
begin
  Result := fSystemConfigFile.ReadString('Code', 'MIWorkbench', '');
end;

function TSystemConfig.GetLayoutSetupFileName_MapLandscape_LegendLandscape: String;
begin
  Result := IncludeTrailingPathDelimiter(MasterFilesByName['master_wors']) +
    fSystemConfigFile.ReadString('ResultViews', 'LayoutSetup_MapLandscape_LegendLandscape',
    '');
end;

function TSystemConfig.GetLayoutSetupFileName_MapLandscape_LegendPortrait: String;
begin
  Result := IncludeTrailingPathDelimiter(MasterFilesByName['master_wors']) +
    fSystemConfigFile.ReadString('ResultViews', 'LayoutSetup_MapLandscape_LegendPortrait',
    '');
end;

function TSystemConfig.GetLayoutSetupFileName_MapPortrait_LegendLandscape: String;
begin
  Result := IncludeTrailingPathDelimiter(MasterFilesByName['master_wors']) +
    fSystemConfigFile.ReadString('ResultViews', 'LayoutSetup_MapPortrait_LegendLandscape',
    '');
end;

function TSystemConfig.GetLayoutSetupFileName_MapPortrait_LegendPortrait: String;
begin
  Result := IncludeTrailingPathDelimiter(MasterFilesByName['master_wors']) +
    fSystemConfigFile.ReadString('ResultViews', 'LayoutSetup_MapPortrait_LegendPortrait',
    '');
end;

function TSystemConfig.GetMasterFile(Index: Integer): String;
begin
  Result := IncludeTrailingPathDelimiter(IncludeTrailingPathDelimiter(fMasterFiles.Values['root']) +
    fMasterFiles.Values[fMasterFiles.Names[Index]]);
end;

function TSystemConfig.GetMasterFileByName(Index: String): String;
begin
  if Uppercase(Index) <> 'ROOT' then
  begin
    if fMasterFiles.Values[Index] = '' then
      raise EIniFileException.Create('Could not find index ' + Index + ' in EMGAATSSystem.ini')
    else
      Result := IncludeTrailingPathDelimiter(IncludeTrailingPathDelimiter(fMasterFiles.Values['root'])) +
        fMasterFiles.Values[Index]
  end
  else
    Result := IncludeTrailingPathDelimiter(fMasterFiles.Values['root']);
end;

function TSystemConfig.GetMasterFileCount: Integer;
begin
  Result := fMasterFiles.Count;
end;

function TSystemConfig.GetMasterFilesConfigFileName: String;
begin
  Result := fSystemConfigFile.ReadString('Admin', 'MasterFilesConfigFileName', '');
end;

function TSystemConfig.GetMasterFilesVersion: String;
begin
  Result := fMasterFilesConfigFile.ReadString('Admin', 'Version', '');
end;

function TSystemConfig.GetMDB(Index: Integer): String;
begin
  Result := fMDBs.Values[fMDBs.Names[Index]];
end;

function TSystemConfig.GetMDBByName(Index: String): String;
begin
  Result := fMDBs.Values[Index];
end;

function TSystemConfig.GetMDBCount: Integer;
begin
  Result := fMDBs.Count;
end;

function TSystemConfig.GetMRUModel(Index: Integer): TFileName;
begin
  Result := fMRUModels.Items[Index];
end;

function TSystemConfig.GetMRUModelsCount: Integer;
begin
  Result := fMRUModels.Count;
end;

function TSystemConfig.GetMSAccessWaitMilliseconds: Integer;
begin
  Result := fUserConfigFile.ReadInteger('Application', 'MSAccessWaitMilliseconds',
    1000);
end;

function TSystemConfig.GetQCWorkspace: String;
begin
  Result := fSystemConfigFile.ReadString('Code', 'QCWorkspace', '');
end;

function TSystemConfig.GetRefreshMDBs: Boolean;
begin
  Result := fSystemConfigFile.ReadBool('Admin', 'RefreshMDBs', True);
end;

function TSystemConfig.GetShowSplashScreen: Boolean;
begin
  Result := fUserConfigFile.ReadBool('Application', 'ShowSplashScreen', True);
end;

function TSystemConfig.GetShowVitalProgramsWarning: Boolean;
begin
  Result := fUserConfigFile.ReadBool('Application', 'ShowVitalProgramsWarning', True);
end;

function TSystemConfig.GetStandardDirectory(Index: Integer): String;
begin
  Result := fStandardDirectories.Values[fStandardDirectories.Names[Index]];
end;

function TSystemConfig.GetStandardDirectoryByName(Index: String): String;
begin
  Result := fStandardDirectories.Values[Index];
end;

function TSystemConfig.GetStandardDirectoryCount: Integer;
begin
  Result := fStandardDirectories.Count;
end;

function TSystemConfig.GetStandardDirectoryID(Index: Integer): String;
begin
  Result := fStandardDirectories.Names[Index];
end;

function TSystemConfig.GetStudyAreaMaxExtents: String;
begin
  Result := fUserConfigFile.ReadString('ExternMaps', 'StudyAreaMaxExtents', '');
end;

function TSystemConfig.GetVersion: String;
begin
  Result := fSystemConfigFile.ReadString('Admin', 'Version', '');
end;

function TSystemConfig.GetVitalProgramsAction: TVitalProgramsAction;
begin
  Result := TVitalProgramsAction(
    fUserConfigFile.ReadInteger('Application', 'VitalProgramsAction', 0));
end;

procedure TSystemConfig.SetBasinBoundaries(const Value: String);
begin
  fUserConfigFile.WriteString('ExternMaps', 'BasinBoundaries', Value);
end;

procedure TSystemConfig.SetDataAccessName(const Value: String);
begin
  fSystemConfigFile.WriteString('DataAccess', 'FileName', Value);
end;

procedure TSystemConfig.SetMRUModelCountLimit(Value: Integer);
begin
  fMRUModels.CountLimit := Value;
end;

procedure TSystemConfig.SetMSAccessWaitMilliseconds(const Value: Integer);
begin
  fUserConfigFile.WriteInteger('Application', 'MSAccessWaitMilliseconds', Value);
end;

procedure TSystemConfig.SetShowSplashScreen(const Value: Boolean);
begin
  fUserConfigFile.WriteBool('Application', 'ShowSplashScreen', Value);
end;

procedure TSystemConfig.SetShowVitalProgramsWarning(const Value: Boolean);
begin
  fUserConfigFile.WriteBool('Application', 'ShowVitalProgramsWarning', Value);
end;

procedure TSystemConfig.SetStandardDirectory(Index: Integer; AValue: String);
begin
  fStandardDirectories.Values[fStandardDirectories.Names[Index]] := AValue;
end;

procedure TSystemConfig.SetStudyAreaMaxExtents(const Value: String);
begin
  fUserConfigFile.WriteString('ExternMaps', 'StudyAreaMaxExtents', Value);
end;

procedure TSystemConfig.SetVitalProgramsAction(
  const Value: TVitalProgramsAction);
begin
  fUserConfigFile.WriteInteger('Application', 'VitalProgramsAction', Integer(Value));
end;

procedure TSystemConfig.Update;
begin
  fUserConfigFile.UpdateFile;
end;

end.
