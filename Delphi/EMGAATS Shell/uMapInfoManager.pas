unit uMapInfoManager;

interface

uses SysUtils, Classes, uEMGAATSModel, uEMGAATSTypes, Windows;

type
  TMapInfoDisplayManager = class
  private
    fMapInfoApp: OleVariant;
    fMapWindowID: Integer;
    fTables: TStringList;
    function GetTables(Index: Integer): String;
    function GetTableCount: Integer;
  public
    constructor Create;
    destructor Destroy; override;
    procedure AssignMapWindow(AHandle: HWND);
    procedure AssignInfoWindow(AHandle: HWND);
    procedure OpenTable(TableFileName: TFileName; ReadOnly: Boolean = True;
      Interactive: Boolean = True);
    procedure OpenWorkspace(WorkspaceFileName: TFileName);
    procedure AddLayer(ATable: String);
    procedure CreateMap;
    procedure SelectTool(ATool: Integer);
    procedure ResizeMapWindow(Width: Integer; Height: Integer);
    procedure MinimizeMapWindow;
    procedure MaximizeMapWindow;
    procedure RestoreMapWindow;
    procedure RunCommand(Command: String);
    procedure ZoomLayer(ATable: String);
    procedure CloseAll;
    function WindowID(AWindowName: String): Integer;
    function NumRecords(ATableName: String): Integer;
    function NumCols(ATableName: String): Integer;
    function ColName(ATableName: String; Index: Integer): String;
    procedure AddColumn(ATableName: String; AColumnName: String; AColumnType: String);
    procedure SaveTable(ATableName: String);
    function TableIsOpen(ATableName: String): Boolean;
    function Zoom(AWindowName: String): Double;
    property Tables[Index: Integer]: String read GetTables;
    property TableCount: Integer read GetTableCount;
  end;

implementation

uses ComObj, Variants, uMIConstants, uMBMapBasic;

{ TMapInfoManager }

procedure TMapInfoDisplayManager.AddColumn(ATableName, AColumnName,
  AColumnType: String);
begin
  fMapInfoApp.Do('Alter Table "' + ATableName + '" (Add "' + AColumnName + '" ' +
    AColumnType + ')');  
end;

procedure TMapInfoDisplayManager.AddLayer(ATable: String);
begin
  fMapInfoApp.Do('Add Map Layer ' + ATable);
end;

procedure TMapInfoDisplayManager.AssignInfoWindow(AHandle: HWND);
begin
  fMapInfoApp.Do('Set Window Info Parent ' + IntToStr(AHandle) + ' Show ReadOnly');
end;

procedure TMapInfoDisplayManager.AssignMapWindow(AHandle: HWND);
begin
  fMapInfoApp.Do('Set Application Window ' + IntToStr(AHandle));
  fMapInfoApp.Do('Set Next Document Parent ' + IntToStr(AHandle) + ' Style 1');
  fMapInfoApp.RunMenuCommand(mi_M_TOOLS_SELECTOR);
end;

procedure TMapInfoDisplayManager.CloseAll;
begin
  fmapInfoApp.Do('Close All');
end;

function TMapInfoDisplayManager.ColName(ATableName: String; Index: Integer): String;
begin
  Result := fMapInfoApp.Eval('ColumnInfo("' + ATableName + '", "COL' +
    IntToStr(Index) + '", ' + IntToStr(mb_COL_INFO_NAME) + ')');
end;

constructor TMapInfoDisplayManager.Create;
begin
  fMapInfoApp := CreateOleObject('MapInfo.Application');
  fTables := TStringList.Create;
end;

procedure TMapInfoDisplayManager.CreateMap;
var
  LayerList: String;
begin
  if fTables.Count > 0 then
  begin
    LayerList := fTables.CommaText;
    fMapInfoApp.Do('Map From ' + LayerList);
    fMapInfoApp.Do('Set Map Window Frontwindow() Zoom Entire');
    fMapWindowID := fMapInfoApp.Eval('WindowID(0)');
  end;
end;

destructor TMapInfoDisplayManager.Destroy;
begin
  fMapInfoApp := Unassigned;
  fTables.Free;
  inherited;
end;

function TMapInfoDisplayManager.GetTableCount: Integer;
begin
  Result := fTables.Count;
end;

function TMapInfoDisplayManager.GetTables(Index: Integer): String;
begin
  Result := fTables[Index];
end;

procedure TMapInfoDisplayManager.MaximizeMapWindow;
begin
  fMapInfoApp.Do('Set Window ' + IntToStr(fMapWindowID) + ' Max');
end;

procedure TMapInfoDisplayManager.MinimizeMapWindow;
begin
  fMapInfoApp.Do('Set Window ' + IntToStr(fMapWindowID) + ' Min');
end;

function TMapInfoDisplayManager.NumCols(ATableName: String): Integer;
begin
  Result := fMapInfoApp.Eval('NumCols("' + ATableName + '")');
end;

function TMapInfoDisplayManager.NumRecords(ATableName: String): Integer;
begin
  Result := fMapInfoApp.Eval('TableInfo(' + ATableName + ',' + IntToStr(mb_TAB_INFO_NROWS) + ')');
end;

procedure TMapInfoDisplayManager.OpenTable(TableFileName: TFileName;
  ReadOnly: Boolean = True; Interactive: Boolean = True);
var
  TableName: String;
  OptionsString: String;
begin
  OptionsString := '';

  if Interactive then
    OptionsString := OptionsString + ' Interactive';

  if ReadOnly then
    OptionsString := OptionsString + ' ReadOnly ';

  fMapInfoApp.Do('Open Table "' + TableFileName + '"' + OptionsString);

  TableName := fMapInfoApp.Eval('TableInfo(0, '+  IntToStr(mb_TAB_INFO_NAME) + ')');
  fTables.Add(TableName);
end;

procedure TMapInfoDisplayManager.OpenWorkspace(WorkspaceFileName: TFileName);
begin
  fMapInfoApp.Do('Run Application "' + WorkspaceFileName + '"');
  fMapInfoApp.Do('Set Map Window Frontwindow() Zoom Entire');
  fMapWindowID := fMapInfoApp.Eval('WindowID(0)');
end;

procedure TMapInfoDisplayManager.ResizeMapWindow(Width: Integer; Height: Integer);
var
  MapWindowHandle: HWND;
begin
  if fMapWindowID > 0 then
  begin
    MapWindowHandle := fMapInfoApp.Eval('WindowInfo(' + IntToStr(fMapWindowID) +
    ', ' + IntToStr(mb_WIN_INFO_WND) + ')');
    MoveWindow(MapWindowHandle, 0, 0, Width, Height, False);
  end;
end;

procedure TMapInfoDisplayManager.RestoreMapWindow;
begin
  fMapInfoApp.Do('Set Window ' + IntToStr(fMapWindowID) + ' Restore');
end;

procedure TMapInfoDisplayManager.RunCommand(Command: String);
begin
  fMapInfoApp.Do(Command);
end;

procedure TMapInfoDisplayManager.SaveTable(ATableName: String);
begin
  fMapInfoApp.Do('Commit Table "' + ATableName + '"');
end;

procedure TMapInfoDisplayManager.SelectTool(ATool: Integer);
begin
  fMapInfoApp.RunMenuCommand(ATool);
end;

function TMapInfoDisplayManager.TableIsOpen(ATableName: String): Boolean;
var
  NumTables: Integer;
  CurrentTableName: String;
  i: Integer;
begin
  NumTables := fMapInfoApp.Eval('NumTables()');
  Result := False;
  for i := 1 to NumTables do
  begin
    CurrentTableName := fMapInfoApp.Eval(Format('TableInfo(%d,%d)',
      [i, mb_TAB_INFO_NAME]));
    if Uppercase(CurrentTableName) = Uppercase(ATableName) then
    begin
      Result := True;
      Break;
    end;
  end;
end;

function TMapInfoDisplayManager.WindowID(AWindowName: String): Integer;
var
  NumWindows: Integer;
  CurrentWindowName: String;
  i: Integer;
begin
  NumWindows := fMapInfoApp.Eval('NumWindows()');
  Result := 0;
  for i := 1 to NumWindows do
  begin
    CurrentWindowName := fMapInfoApp.Eval(Format('WindowInfo(%d,%d)', [i, mb_WIN_INFO_NAME]));
    if Uppercase(CurrentWindowName) = Uppercase(AWindowName) then
    begin
      Result := fMapInfoApp.Eval(Format('WindowID(%d)', [i]));
      Break;
    end;
  end;
end;

function TMapInfoDisplayManager.Zoom(AWindowName: String): Double;
var
  AWindowID: Integer;
begin
  AWindowID := WindowID(AWindowName);
  Result := fMapInfoApp.Eval('MapperInfo("' + IntToStr(AWIndowID) + '", ' +
    IntToStr(mb_MAPPER_INFO_ZOOM) + ')');
end;

procedure TMapInfoDisplayManager.ZoomLayer(ATable: String);
var
  TableIndex: Integer;
begin
  if ATable = '' then
  begin
    fMapInfoApp.Do('Set Map Window Frontwindow() Zoom Entire');
  end
  else
  begin
    fMapInfoApp.Do('Set Map Window Frontwindow() Zoom Entire Layer "' +
      ATable +'"');
  end;
end;

end.
