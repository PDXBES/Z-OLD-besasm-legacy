unit uInterfaceStack;

interface

uses SysUtils, Classes, Contnrs;

type

	{=============================================================================
		 Name: TInterfaceStack
		 Purpose: Stack of interfaces
		 Requirements: -
	=============================================================================}
	TInterfaceStack = class
	private
		fList: TInterfaceList;
	public
		constructor Create; virtual;
		destructor Destroy; override;
		procedure Push(AInterface: IInterface);
		function Pop: IInterface;
		function Peek: IInterface;
		function Count: Integer;
	end;

implementation

{ TInterfaceStack }

constructor TInterfaceStack.Create;
begin
	fList := TInterfaceList.Create;
	fList.Capacity := 32;
end;

function TInterfaceStack.Count: Integer;
begin
	Result := fList.Count;
end;

procedure TInterfaceStack.Push(AInterface: IInterface);
begin
	fList.Add(AInterface);
end;

function TInterfaceStack.Peek: IInterface;
begin
	if fList.Count > 0 then
		Result := fList.Last
	else
		Result := nil;
end;

function TInterfaceStack.Pop: IInterface;
begin
	if fList.Count > 0 then
	begin
		Result := fList.Last;
		fList.Delete(fList.Count - 1);
	end
	else
		Result := nil;
end;

destructor TInterfaceStack.Destroy;
begin
	fList.Free;
	inherited;
end;

end.
