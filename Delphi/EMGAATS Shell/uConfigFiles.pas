unit uConfigFiles;

interface

uses SysUtils, Classes, IniFiles;

type
  {
  IConfigFile
    Interface for communicating with configuration files (XML or INI)
  }
  IConfigFile = interface
    procedure DeleteItem(const SectionName: String; const Item: String);
    procedure EraseSection(const SectionName: String);
    procedure ReadSection(const SectionName: String; List: TStringList);
    procedure ReadSections(List: TStringList);
    procedure ReadSectionItems(const SectionName: String; List: TStringList);
    procedure CopySectionToConfig(const SectionName: String; AConfigFile: IConfigFile);
    procedure UpdateFromTemplateConfig(AConfig: IConfigFile);
    procedure UpdateFile;
    procedure RevertFile;
    function ReadString(const SectionName: String; const ItemName: String; Default: String = ''): String;
    procedure WriteString(const SectionName: String; const ItemName: String; Value: String);
    function ReadBool(const SectionName: String; const ItemName: String; Default: Boolean = False): Boolean;
    procedure WriteBool(const SectionName: String; const ItemName: String; Value: Boolean);
    function ReadDate(const SectionName: String; const ItemName: String; Default: TDateTime = 0): TDateTime;
    procedure WriteDate(const SectionName: String; const ItemName: String; Value: TDateTime);
    function ReadDateTime(const SectionName: String; const ItemName: String; Default: TDateTime = 0): TDateTime;
    procedure WriteDateTime(const SectionName: String; const ItemName: String; Value: TDateTime);
    function ReadTime(const SectionName: String; const ItemName: String; Default: TDateTime = 0): TDateTime;
    procedure WriteTime(const SectionName: String; const ItemName: String; Value: TDateTime);
    function ReadFloat(const SectionName: String; const ItemName: String; Default: Double = 0): Double;
    procedure WriteFloat(const SectionName: String; const ItemName: String; Value: Double);
    function ReadInteger(const SectionName: String; const ItemName: String; Default: Integer = 0): Integer;
    procedure WriteInteger(const SectionName: String; const ItemName: String; Value: Integer);
    function GetFileName: TFileName;
    property FileName: TFileName read GetFileName;
  end;

  TIniConfigFile = class(TInterfacedObject, IConfigFile)
  private
    fIniFile: TMemIniFile;
  public
    constructor Create(AFileName: TFileName);
    destructor Destroy; override;
    procedure DeleteItem(const SectionName: string; const Item: string);
    procedure EraseSection(const SectionName: string);
    function ReadBool(const SectionName: string; const ItemName: string;
      Default: Boolean = False): Boolean;
    function ReadDate(const SectionName: string; const ItemName: string;
      Default: TDateTime = 0): TDateTime;
    function ReadDateTime(const SectionName: string; const ItemName: string;
      Default: TDateTime = 0): TDateTime;
    function ReadFloat(const SectionName: string; const ItemName: string;
      Default: Double = 0): Double;
    function ReadInteger(const SectionName: string; const ItemName: string;
      Default: Integer = 0): Integer;
    procedure ReadSection(const SectionName: string; List: TStringList);
    procedure ReadSectionItems(const SectionName: string; List: TStringList);
    procedure ReadSections(List: TStringList);
    procedure CopySectionToConfig(const SectionName: String; AConfigFile: IConfigFile);
    function ReadString(const SectionName: string; const ItemName: string;
      Default: string = ''): string;
    function ReadTime(const SectionName: string; const ItemName: string;
      Default: TDateTime = 0): TDateTime;
    procedure UpdateFile;
    procedure RevertFile;
    procedure WriteBool(const SectionName: string; const ItemName: string;
      Value: Boolean);
    procedure WriteDate(const SectionName: string; const ItemName: string;
      Value: TDateTime);
    procedure WriteDateTime(const SectionName: string; const ItemName: string;
      Value: TDateTime);
    procedure WriteFloat(const SectionName: string; const ItemName: string;
      Value: Double);
    procedure WriteInteger(const SectionName: string; const ItemName: string;
      Value: Integer);
    procedure WriteString(const SectionName: string; const ItemName: string;
      Value: string);
    procedure WriteTime(const SectionName: string; const ItemName: string;
      Value: TDateTime);
    function GetFileName: TFileName;
    procedure UpdateFromTemplateConfig(AConfig: IConfigFile);
  end;

implementation

{ TIniConfigFile }

procedure TIniConfigFile.CopySectionToConfig(const SectionName: String;
  AConfigFile: IConfigFile);
var
  SectionItems: THashedStringList;
  i: Integer;
begin
  SectionItems := THashedStringList.Create;
  try
    ReadSectionItems(SectionName, SectionItems);
    for i := 0 to SectionItems.Count - 1 do
      AConfigFile.WriteString(SectionName, SectionItems.Names[i],
        SectionItems.Values[SectionItems.Names[i]]);
  finally
    SectionItems.Free;
  end;
end;

constructor TIniConfigFile.Create(AFileName: TFileName);
begin
  fIniFile := TMemIniFile.Create(AFileName);
end;

procedure TIniConfigFile.DeleteItem(const SectionName, Item: string);
begin
  fIniFile.DeleteKey(SectionName, Item);
end;

destructor TIniConfigFile.Destroy;
begin
  fIniFile.Free;
  inherited;
end;

procedure TIniConfigFile.EraseSection(const SectionName: string);
begin
  fIniFile.EraseSection(SectionName);
end;

function TIniConfigFile.GetFileName: TFileName;
begin
  Result := fIniFile.FileName;
end;

function TIniConfigFile.ReadBool(const SectionName, ItemName: string;
  Default: Boolean): Boolean;
begin
  Result := fIniFile.ReadBool(SectionName, ItemName, Default);
end;

function TIniConfigFile.ReadDate(const SectionName, ItemName: string;
  Default: TDateTime): TDateTime;
begin
  Result := fIniFile.ReadDate(SectionName, ItemName, Default);
end;

function TIniConfigFile.ReadDateTime(const SectionName, ItemName: string;
  Default: TDateTime): TDateTime;
begin
  Result := fIniFile.ReadDateTime(SectionName, ItemName, Default);
end;

function TIniConfigFile.ReadFloat(const SectionName, ItemName: string;
  Default: Double): Double;
begin
  Result := fIniFile.ReadFloat(SectionName, ItemName, Default);
end;

function TIniConfigFile.ReadInteger(const SectionName, ItemName: string;
  Default: Integer): Integer;
begin
  Result := fIniFile.ReadInteger(SectionName, ItemName, Default);
end;

procedure TIniConfigFile.ReadSection(const SectionName: string;
  List: TStringList);
begin
  fIniFile.ReadSection(SectionName, List);
end;

procedure TIniConfigFile.ReadSectionItems(const SectionName: string;
  List: TStringList);
begin
  fIniFile.ReadSectionValues(SectionName, List);
end;

procedure TIniConfigFile.ReadSections(List: TStringList);
begin
  fIniFile.ReadSections(List);
end;

function TIniConfigFile.ReadString(const SectionName, ItemName: string;
  Default: string): string;
begin
  Result := fIniFile.ReadString(SectionName, ItemName, Default);
end;

function TIniConfigFile.ReadTime(const SectionName, ItemName: string;
  Default: TDateTime): TDateTime;
begin
  Result := fIniFile.ReadTime(SectionName, ItemName, Default);
end;

procedure TIniConfigFile.RevertFile;
var
  SavedFileName: TFileName;
begin
  SavedFileName := fIniFile.FileName;
  fIniFile.Free;
  fIniFile := TMemIniFile.Create(SavedFileName);
end;

procedure TIniConfigFile.UpdateFile;
begin
  fIniFile.UpdateFile;
end;

procedure TIniConfigFile.UpdateFromTemplateConfig(AConfig: IConfigFile);
var
  SectionNames: TStringList;
  SectionItems: TStringList;
  i: Integer;
  j: Integer;
  ItemValue: String;
const
  KeyNotPresentStr = 'TEST!KEYNOTPRESENT';
begin
  SectionNames := TStringList.Create;
  SectionItems := TStringList.Create;
  try
    AConfig.ReadSections(SectionNames);
    for i := 0 to SectionNames.Count - 1 do
    begin
      AConfig.ReadSectionItems(SectionNames[i], SectionItems);
      for j := 0 to SectionItems.Count - 1 do
      begin
        ItemValue := ReadString(SectionNames[i], SectionItems.Names[j], KeyNotPresentStr);
        if ItemValue = KeyNotPresentStr then
          WriteString(SectionNames[i], SectionItems.Names[j],
            AConfig.ReadString(SectionNames[i], SectionItems.Names[j], ''));
      end;
    end;
    UpdateFile;
  finally
    SectionNames.Free;
    SectionItems.Free;
  end;
end;

procedure TIniConfigFile.WriteBool(const SectionName, ItemName: string;
  Value: Boolean);
begin
  fIniFile.WriteBool(SectionName, ItemName, Value);
end;

procedure TIniConfigFile.WriteDate(const SectionName, ItemName: string;
  Value: TDateTime);
begin
  fIniFile.WriteDate(SectionName, ItemName, Value);
end;

procedure TIniConfigFile.WriteDateTime(const SectionName, ItemName: string;
  Value: TDateTime);
begin
  fIniFile.WriteDateTime(SectionName, ItemName, Value);
end;

procedure TIniConfigFile.WriteFloat(const SectionName, ItemName: string;
  Value: Double);
begin
  fIniFile.WriteFloat(SectionName, ItemName, Value);
end;

procedure TIniConfigFile.WriteInteger(const SectionName, ItemName: string;
  Value: Integer);
begin
  fIniFile.WriteInteger(SectionName, ItemName, Value);
end;

procedure TIniConfigFile.WriteString(const SectionName, ItemName: string;
  Value: string);
begin
  fIniFile.WriteString(SectionName, ItemName, Value);
end;

procedure TIniConfigFile.WriteTime(const SectionName, ItemName: string;
  Value: TDateTime);
begin
  fIniFile.WriteTime(SectionName, ItemName, Value);
end;

end.
