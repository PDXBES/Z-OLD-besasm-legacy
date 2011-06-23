unit uEMGWorkbenchManager;

interface

uses SysUtils, Classes, uEMGAATSModel, uEMGAATSTypes, RzStatus;

type
  TEMGWorkbenchManager = class
  private
    fMapInfoApp: OleVariant;
    fModel: TEMGAATSModel;
    fStatusPane: TRzGlyphStatus;
    function RunMenuCommand(CommandID: String): String;
    function GetModelPath: String;
    procedure SetStatusPane(const Value: TRzGlyphStatus);
  public
    constructor Create;
    destructor Destroy; override;
    procedure RunCommand(Command: String);
    function Eval(Expression: String): Variant;
    procedure SetModel(AModel: TEMGAATSModel);
    procedure CreateModelTables;
    procedure OpenTables;
    procedure TraceParcels;
    procedure TraceSurfaceSubcatchments;
    procedure CreateSurfaceSubcatchmentPointers;
    procedure CheckMapInfoProcess;
    procedure Initialize(EMGAppLocation: String);
    procedure CreateMappablePipes;
    procedure GetSpecialLinksData;
    procedure RelateDSCtoSurfSC;
    procedure CheckForDuplicateDSCs;
    procedure CheckForExcessiveICAreas;
    procedure ClearTable(ATable: String);
    procedure ClearNetwork;
    procedure ClearDirectSubcatchments;
    procedure ClearSurfaceSubcatchments;
    procedure SetTimeFrame(ATimeFrame: TTimeFrame);
    property ModelPath: String read GetModelPath;
    property StatusPane: TRzGlyphStatus read fStatusPane write SetStatusPane;
    function CreateStudyAreaBoundary: Boolean;
  end;

  EEMGWorkbenchException = class(Exception)
  end;

var
  EMGWorkbenchManager: TEMGWorkbenchManager;

implementation

uses ComObj, Variants, uWorkbenchDefs, GlobalConstants, uMSAccessManager,
  uEMGAATSSystemConfig;

{ TEMGWorkbenchManager }

procedure TEMGWorkbenchManager.CheckForDuplicateDSCs;
var
  CmdResult: String;
begin
  CmdResult := RunMenuCommand(EMG_QADupDsc);
  if CmdResult = emgwFailure then
    raise EEMGWorkbenchException.Create('Duplicate DSCs found');

end;

procedure TEMGWorkbenchManager.CheckForExcessiveICAreas;
var
  CmdResult: String;
begin
  CmdResult := RunMenuCommand(EMG_QAICmax);
  if CmdResult = emgwFailure then
    raise EEMGWorkbenchException.Create('Excessive IC areas found');
end;

procedure TEMGWorkbenchManager.CheckMapInfoProcess;
begin

end;

procedure TEMGWorkbenchManager.ClearDirectSubcatchments;
begin
  ClearTable('mdl_dsc');
end;

procedure TEMGWorkbenchManager.ClearNetwork;
begin
  ClearTable('mdl_links');
  ClearTable('mdl_speclinks');
  ClearTable('mdl_speclinkdata');
  ClearTable('mdl_nodes');
end;

procedure TEMGWorkbenchManager.ClearSurfaceSubcatchments;
begin
  ClearTable('mdl_surfsc');
  ClearTable('mdl_surfzing');
end;

procedure TEMGWorkbenchManager.ClearTable(ATable: String);
begin
  RunCommand('Delete From ' + ATable);
  RunCommand('Commit Table ' + ATable);
end;

constructor TEMGWorkbenchManager.Create;
begin
	fMapInfoApp := CreateOleObject('MapInfo.Application');
end;

procedure TEMGWorkbenchManager.CreateMappablePipes;
var
  CmdResult: String;
begin
  CmdResult :=  RunMenuCommand(EMG_BuildMDL_Links);
  if CmdResult = emgwFailure then
    raise EEMGWorkbenchException.Create('')
  else if CmdResult = emgwError then
    raise EEMGWorkbenchException.Create('');
end;

procedure TEMGWorkbenchManager.CreateModelTables;
begin
  RunMenuCommand(EMG_CreateModelTables);
end;

function TEMGWorkbenchManager.CreateStudyAreaBoundary: Boolean;
var
  ModelPath: String;
  NumRows: Integer;
begin
  ModelPath := IncludeTrailingPathDelimiter(fModel.Path);

  // If there's surface subcatchments, create a buffer around them
	NumRows := StrToInt(Eval('TableInfo(mdl_SurfSC,8)'));
  if NumRows > 0 then
  begin
    RunCommand('select obj from mdl_SurfSC where col1 = -1 into QyTmp ');
    RunCommand('Commit Table QyTmp As Str$("'+ModelPath+'" + "surfsc\ProjAreaTmp.TAB") TYPE NATIVE Charset "WindowsLatin1"');
    RunCommand('Open Table Str$("'+ModelPath+'" + "surfsc\ProjAreaTmp.TAB") Interactive');
    RunCommand('select * from mdl_SurfSC where obj into QyTmp');
    NumRows := StrToInt(Eval('TableInfo(QyTmp,8)'));
    if NumRows > 0 then
    begin
      RunCommand('Create Object As Buffer From Selection Width 10 Units "ft" Type Cartesian Resolution 12 Into Table ProjAreaTmp');
      Result := True;
    end
    else
      Result := False;
  end
  else
    Result := False;

  // If there's direct subcatchments, buffer those too
  if Result then
  begin
    NumRows := StrToInt(Eval('TableInfo(mdl_DSC,8)'));
    if NumRows > 0 then
    begin
      RunCommand('select * from mdl_DSC into QyTmp');
      RunCommand('Create Object As Buffer From Selection Width 10 Units "ft" Type Cartesian Resolution 12 Into Table ProjAreaTmp');
      // Create Project Area
      RunCommand('Select * from ProjAreaTmp into QyTmp');
      RunCommand('Objects Combine');
      RunCommand('Create Object As Buffer From Selection Width -10 Units "ft" Type Cartesian Resolution 12 Into Table ProjAreaTmp');
      RunCommand('Commit Table ProjAreaTmp');
      RunCommand('Select * from ProjAreaTmp where rowid<>INT(tableinfo("ProjAreaTmp",8)) into QyTmp');
      RunCommand('Commit Table QyTmp As Str$("'+ModelPath+'" + "surfsc\ProjArea.TAB") TYPE NATIVE Charset "WindowsLatin1"');
      RunCommand('Open Table Str$("'+ModelPath+'" + "surfsc\ProjArea.TAB") Interactive');
      RunCommand('Drop Table ProjAreaTmp');
    end
    else
    begin
      RunCommand('Select * from ProjAreaTmp where rowid=INT(tableinfo("ProjAreaTmp",8)) into QyTmp');
      RunCommand('Commit Table QyTmp As Str$("'+ModelPath+'" + "surfsc\ProjArea.TAB") TYPE NATIVE Charset "WindowsLatin1"');
      RunCommand('Open Table Str$("'+ModelPath+'" + "surfsc\ProjArea.TAB") Interactive');
      RunCommand('Drop Table ProjAreaTmp');
    end;
  end
  else
  begin
    // If there aren't any surface subcatchments, buffer the dsc's
  	NumRows := StrToInt(Eval('TableInfo(mdl_DSC,8)'));
    if NumRows > 0 then
    begin
      RunCommand('select obj from mdl_DSC where col1= -1 into QyTmp ');
      RunCommand('Commit Table QyTmp As Str$("'+ModelPath+'" + "surfsc\ProjAreaTmp.TAB") TYPE NATIVE Charset "WindowsLatin1"');
      RunCommand('Open Table Str$("'+ModelPath+'" + "surfsc\ProjAreaTmp.TAB") Interactive');
      RunCommand('select * from mdl_DSC where obj into QyTmp');
      NumRows := StrToInt(Eval('TableInfo(QyTmp,8)'));
      if NumRows > 0 then
      begin
        RunCommand('Create Object As Buffer From Selection Width 10 Units "ft" Type Cartesian Resolution 12 Into Table ProjAreaTmp');
        RunCommand('Commit Table ProjAreaTmp');
//        RunCommand('Select * from ProjAreaTmp where rowid<>INT(tableinfo("ProjAreaTmp",8)) into QyTmp');
        RunCommand('Select * from ProjAreaTmp into QyTmp');
        RunCommand('Commit Table QyTmp As Str$("'+ModelPath+'" + "surfsc\ProjArea.TAB") TYPE NATIVE Charset "WindowsLatin1"');
        RunCommand('Open Table Str$("'+ModelPath+'" + "surfsc\ProjArea.TAB") Interactive');
        RunCommand('Drop Table ProjAreaTmp');
        Result := True;
      end
    end
    else
    begin
      // Create ProjArea and ProjMask tables from links if we have nothing, to
      // prevent overview workspace from breaking
      RunCommand('select obj from mdl_Links into QyTmp');
      RunCommand('Commit Table QyTmp As Str$("'+ModelPath+'" + "surfsc\ProjAreaTmp.TAB") TYPE NATIVE Charset "WindowsLatin1"');
      RunCommand('Open Table Str$("'+ModelPath+'" + "surfsc\ProjAreaTmp.TAB") Interactive');
      RunCommand('select * from mdl_Links into QyTmp');
      RunCommand('Create Object As Buffer From Selection Width 10 Units "ft" Type Cartesian Resolution 12 Into Table ProjAreaTmp');
      RunCommand('Select * from ProjAreaTmp where rowid=INT(tableinfo("ProjAreaTmp",8)) into QyTmp');
      RunCommand('Commit Table QyTmp As Str$("'+ModelPath+'" + "surfsc\ProjArea.TAB") TYPE NATIVE Charset "WindowsLatin1"');
      RunCommand('Drop Table ProjAreaTmp');
      RunCommand('Open Table Str$("'+ModelPath+'" + "surfsc\ProjArea.TAB") Interactive');
    end;
  end;

  // Create project mask
  RunCommand('Open Table "' + SystemConfig.StudyAreaMaxExtents + '" Interactive');
  RunCommand('Commit Table Extents As Str$("'+ModelPath+'" + "surfsc\ProjMask.TAB") TYPE NATIVE Charset "WindowsLatin1"');
  RunCommand('Open Table Str$("'+ModelPath+'" + "surfsc\ProjMask.TAB") Interactive');
  RunCommand('Close table extents');
  RunCommand('select * from ProjArea into Eraser');
  RunCommand('select * from ProjMask');
  RunCommand('set target on');
  RunCommand('select * from Eraser');
  NumRows := StrToInt(Eval('TableInfo(Eraser,8)'));
  if NumRows > 0 then
  begin
    RunCommand('Objects Erase Into Target');
    RunCommand('Close Table eraser');
  end;
  RunCommand('Commit Table ProjMask');
  RunCommand('Close Table ProjMask');
end;

procedure TEMGWorkbenchManager.CreateSurfaceSubcatchmentPointers;
begin
  RunMenuCommand(EMG_MakeSurfZingers);
end;

destructor TEMGWorkbenchManager.Destroy;
begin
  fMapInfoApp := Unassigned;
  inherited;
end;

function TEMGWorkbenchManager.Eval(Expression: String): Variant;
begin
  Result := fMapInfoApp.Eval(Expression);
end;

function TEMGWorkbenchManager.GetModelPath: String;
begin
  Result := fModel.Path;
end;

procedure TEMGWorkbenchManager.GetSpecialLinksData;
begin
  // Assert that a current db is open (ModelAssemble)
  RunCommand('commit table mdl_SpecLinkData');
  RunCommand('pack table mdl_SpecLinkData data');
  MSAccessManager.RunQuery('qryAppendSpecLinkData');
  MSAccessManager.RunQuery('qrySpecLinkGageID');
  RunCommand('commit table mdl_SpecLinkData');
end;

procedure TEMGWorkbenchManager.Initialize(EMGAppLocation: String);
begin
  fMapInfoApp.Do('Run Application '#34 + EMGAppLocation + #34);
end;

procedure TEMGWorkbenchManager.OpenTables;
begin
  RunMenuCommand(EMG_OpenTables);
end;

procedure TEMGWorkbenchManager.RelateDSCtoSurfSC;
begin
  RunMenuCommand(EMG_RelateDSCtoSurfSC);
end;

procedure TEMGWorkbenchManager.RunCommand(Command: String);
begin
  fMapInfoApp.Do(Command);
end;

function TEMGWorkbenchManager.RunMenuCommand(CommandID: String): String;
begin
  fMapInfoApp.Do('Run Menu Command ID ' + CommandID);
  Sleep(1000);
  Result := UpperCase(
    string(fMapInfoApp.MBApplications.Item(EMG_AppName).mbglobals.item(strMBrc).value));
end;

procedure TEMGWorkbenchManager.SetModel(AModel: TEMGAATSModel);
begin
  fModel := AModel;
  SetTimeFrame(fModel.TimeFrame);
 	// Let the mapbasic apps know where they are located
	fMapInfoApp.MBApplications.Item(EMG_AppName).MBGlobals.item('gmdlRootFolder').Value := fModel.Path;

  if Assigned(fStatusPane) then
    fStatusPane.Caption := 'EMGWorkbench active: ' + fModel.Path;
end;

procedure TEMGWorkbenchManager.SetStatusPane(const Value: TRzGlyphStatus);
begin
  fStatusPane := Value;
end;

procedure TEMGWorkbenchManager.SetTimeFrame(ATimeFrame: TTimeFrame);
var
  TimeFrameString: String;
begin
  case ATimeFrame of
    tfExisting: TimeFrameString := 'EX';
    tfFuture: TimeFrameString := 'FU';
  end;
	fMapInfoApp.MBApplications.Item(EMG_AppName).MBGlobals.item('gstrTimeFrame').Value := TimeFrameString;
end;

procedure TEMGWorkbenchManager.TraceParcels;
begin
  RunMenuCommand(EMG_TraceDSCs);
end;

procedure TEMGWorkbenchManager.TraceSurfaceSubcatchments;
begin
  RunMenuCommand(EMG_TraceSurfaceSC);
end;

end.
