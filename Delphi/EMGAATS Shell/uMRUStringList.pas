unit uMRUStringList;

interface

uses SysUtils, Classes;

type
  TMRUStringList = class
  private
    fList: TStringList;
    fCountLimit: Integer;
    procedure SetCountLimit(const Value: Integer);
    function GetCount: Integer;
    function GetItem(Index: Integer): String;
    procedure SetItem(Index: Integer; const Value: String);
  public
    constructor Create;
    destructor Destroy; override;
    property CountLimit: Integer read fCountLimit write SetCountLimit;
    property Count: Integer read GetCount;
    property Items[Index: Integer]: String read GetItem write SetItem;
    procedure Insert(const Value: String);
    function MostRecentItem: String;
  end;

implementation


{ TMRUStringList }

constructor TMRUStringList.Create;
begin
  fList := TStringList.Create;
  fCountLimit := 4;
end;

destructor TMRUStringList.Destroy;
begin
  fList.Free;
  inherited;
end;

function TMRUStringList.GetCount: Integer;
begin
  Result := fList.Count;
end;

function TMRUStringList.GetItem(Index: Integer): String;
begin
  Result := fList[Index];
end;

procedure TMRUStringList.Insert(const Value: String);
begin
  fList.Insert(0, Value);
  if fList.Count > fCountLimit then
    while fList.Count > fCountLimit do
      fList.Delete(fCountLimit);
end;

function TMRUStringList.MostRecentItem: String;
begin
  Result := fList[0];
end;

procedure TMRUStringList.SetCountLimit(const Value: Integer);
begin
  if fCountLimit <> Value then
    fCountLimit := Value;
end;

procedure TMRUStringList.SetItem(Index: Integer; const Value: String);
begin
  if fList[Index] <> Value then
    fList[Index] := Value;
end;

end.
