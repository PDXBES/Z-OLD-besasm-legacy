unit uIM_IndexLookup;

interface

uses Classes;

type
	TIndexLookupList = class
	private
		fList: TList;
	protected
		function GetItem(AIndex: Integer): Integer;
		procedure SetItem(AIndex: Integer; Value: Integer);
	public
		constructor Create;
		destructor Destroy; override;
		function Add(Value: Integer): Integer;
		procedure Delete(AIndex: Integer);
		function Count: Integer;
		property Items[AIndex: Integer]: Integer read GetItem write SetItem; default;
	end;

implementation

{ TIndexLookupList }

constructor TIndexLookupList.Create;
begin
	fList := TList.Create;
end;

function TIndexLookupList.Add(Value: Integer): Integer;
begin
	fList.Add(Pointer(Value));
end;

function TIndexLookupList.Count: Integer;
begin
	Result := fList.Count;
end;

function TIndexLookupList.GetItem(AIndex: Integer): Integer;
begin
	Result := Integer(fList[AIndex]);
end;

procedure TIndexLookupList.SetItem(AIndex, Value: Integer);
begin
	fList[AIndex] := Pointer(Value);
end;

procedure TIndexLookupList.Delete(AIndex: Integer);
begin
	fList.Delete(AIndex);
end;

destructor TIndexLookupList.Destroy;
begin
	fList.Free;
  inherited;
end;

end.
