unit uMSAccessManager;

interface

uses SysUtils, Classes, ComObj, Variants, ADODB;

type
  TMSAccessManager = class
  private
    fAccessApp: OleVariant;
    fCommand: TADOCommand;
    fConnection: TADOConnection;
  public
    constructor Create; overload;
    constructor Create(ADatabase: TFileName); overload;
    destructor Destroy; override;
    procedure Relink(LinkTable: String; SourceTable: String; SourceDatabase: String);
    procedure Run(FunctionName: String; Arguments: array of Variant);
    procedure RunQuery(QueryName: String);
    procedure Link(LinkTable: String; SourceTable: String; SourceDatabase: String);
    procedure CreateDatabase(ADatabase: TFileName);
    procedure OpenDatabase(ADatabase: TFileName);
    procedure CloseDatabase;
    procedure SendCommand(ACommand: String);
    function RecordSetCount(ARecordSet: String): Integer;
    function RecordCount(TableName: String): Integer;
    function TableExists(TableName: String): Boolean;
    function SetQueryDef(QueryName: String; QueryDef: String): Boolean;
  end;

var
  MSAccessManager: TMSAccessManager;

implementation

uses ADOX_TLB, uEMGAATSSystemConfig;

{ TMSAccessManager }

procedure TMSAccessManager.CloseDatabase;
begin
  fAccessApp.CloseCurrentDatabase;
end;

constructor TMSAccessManager.Create(ADatabase: TFileName);
var
  DataSource: String;
begin
  fAccessApp := CreateOleObject('Access.Application');
  if not FileExists(ADatabase) then
    CreateDatabase(ADatabase);
  OpenDatabase(ADatabase);
  fCommand := TADOCommand.Create(nil);
  fConnection := TADOConnection.Create(nil);
  DataSource := 'Provider=Microsoft.Jet.OLEDB.4.0;' +
    'Data Source=' + ADatabase + ';' +
    'Persist Security Info=False';
  fConnection.Close;
  fConnection.ConnectionString := DataSource;
  fConnection.LoginPrompt := False;
  fConnection.Open;
  fCommand.Connection := fConnection;
end;

constructor TMSAccessManager.Create;
begin
  fAccessApp := CreateOleObject('Access.Application');
  fCommand := TADOCommand.Create(nil);
  fConnection := TADOConnection.Create(nil);
  fCommand.Connection := fConnection;
end;

procedure TMSAccessManager.CreateDatabase(ADatabase: TFileName);
var
  ACatalog: Catalog;
  DataSource: String;
begin
  DataSource := 'Provider=Microsoft.Jet.OLEDB.4.0;' +
    'Data Source = ' + ADatabase + ';';
  ACatalog := CoCatalog.Create;
  ACatalog.Create(DataSource);

  if not VarIsClear(fAccessApp.CurrentDB) then
    fAccessApp.CloseCurrentDatabase;
  fAccessApp.OpenCurrentDatabase(ADatabase);

  fConnection.Close;
  fConnection.ConnectionString := DataSource;
  fConnection.LoginPrompt := False;
  fConnection.Open;
end;

destructor TMSAccessManager.Destroy;
begin
  fCommand.Free;
  fConnection.Free;
  fAccessApp := Unassigned;
  inherited;
end;

procedure TMSAccessManager.Link(LinkTable, SourceTable, SourceDatabase: String);
var
  TableToLink : OleVariant;
begin
  // Perform the link
  try
    TableToLink := fAccessApp.CurrentDB.CreateTableDef(LinkTable);
    TableToLink.Connect := ';DATABASE='+ SourceDatabase;
    TableToLink.SourceTableName := SourceTable;
    fAccessApp.CurrentDB.TableDefs.Append(TableToLink);
    TableToLink:= Unassigned;

  except
    on E: eoleexception do
    begin
{		  if verboseoleerror then
        showmessage(E.Message);
      result :=0;
}
      Exit;
    end;
  else
    begin
 		  raise Exception.Create('Undefined error in access table link');
      exit;
    end;
  end;
end;

procedure TMSAccessManager.OpenDatabase(ADatabase: TFileName);
var
  DataSource: String;
begin
  if not VarIsClear(fAccessApp.CurrentDB) then
    fAccessApp.CloseCurrentDatabase;
  fAccessApp.OpenCurrentDatabase(ADatabase);
  DataSource := 'Provider=Microsoft.Jet.OLEDB.4.0;' +
    'Data Source=' + ADatabase + ';' +
    'Persist Security Info=False';
  fConnection.Close;
  fConnection.ConnectionString := DataSource;
  fConnection.LoginPrompt := False;
  fConnection.Open;
end;

function TMSAccessManager.RecordCount(TableName: String): Integer;
var
  ATable: OleVariant;
begin
  ATable := fAccessApp.CurrentDB.OpenRecordset(TableName);
  if not ATable.Eof then
  begin
    ATable.MoveLast;
    Result := ATable.RecordCount;
  end
  else
    Result := 0;
end;

function TMSAccessManager.RecordSetCount(ARecordSet: String): Integer;
var
  ReturnedRecordSet: _RecordSet;
begin
  fCommand.CommandText := ARecordSet;
  ReturnedRecordSet := fCommand.Execute;
  Result := ReturnedRecordSet.RecordCount;
end;

procedure TMSAccessManager.Relink(LinkTable, SourceTable,
  SourceDatabase: String);
begin
 // First unlink the table
  fAccessApp.CurrentDB.TableDefs.Delete(LinkTable);

  // Perform the link
  Link(LinkTable, SourceTable, SourceDatabase);
end;

procedure TMSAccessManager.Run(FunctionName: String;
  Arguments: array of Variant);
begin
  case Length(Arguments) of
    0: fAccessApp.Run(FunctionName);
    1: fAccessApp.Run(FunctionName, Arguments[0]);
    2: fAccessApp.Run(FunctionName, Arguments[0], Arguments[1]);
    3: fAccessApp.Run(FunctionName, Arguments[0], Arguments[1], Arguments[2]);
    4: fAccessApp.Run(FunctionName, Arguments[0], Arguments[1], Arguments[2],
        Arguments[3]);
    5: fAccessApp.Run(FunctionName, Arguments[0], Arguments[1], Arguments[2],
        Arguments[3], Arguments[4]);
  end;
  Sleep(SystemConfig.MSAccessWaitMilliseconds);
end;

procedure TMSAccessManager.RunQuery(QueryName: String);
begin
  fAccessApp.CurrentDB.Execute(QueryName);
  Sleep(SystemConfig.MSAccessWaitMilliseconds);
end;

procedure TMSAccessManager.SendCommand(ACommand: String);
begin
  fCommand.CommandText := ACommand;
  fCommand.Execute;
  Sleep(SystemConfig.MSAccessWaitMilliseconds);
end;

function TMSAccessManager.SetQueryDef(QueryName, QueryDef: String): Boolean;
var
  AQueryDef: Variant;
begin
  AQueryDef := fAccessApp.CurrentDB.QueryDefs[QueryName];
  AQueryDef.SQL := QueryDef;
  Result := True;
end;

function TMSAccessManager.TableExists(TableName: String): Boolean;
var
  ACatalog: Catalog;
  i: Integer;
begin
  ACatalog := CoCatalog.Create;
  ACatalog.Set_ActiveConnection(fConnection.ConnectionObject);
  Result := False;
  for i := 0 to ACatalog.Tables.Count - 1 do
    if UpperCase(ACatalog.Tables[i].Name) = UpperCase(TableName)  then
    begin
      Result := True;
      Exit;
    end;
end;

end.
