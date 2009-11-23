{*******************************************************}
{*
{* uTNodesList.pas
{* Delphi Implementation of the Class TNodesList
{* Generated by Enterprise Architect
{* Created on:      09-May-2006 1:49:42 AM
{* Original author: Arnel Mandilag
{*
{*******************************************************}

unit uTNodesList;

interface

uses SysUtils, Classes, Contnrs, StrUtils, uTNode;

type

	TNodesList = class
	private
		List: TObjectList;
	protected
		function GetItem(AIndex: Integer): TNode;
	public
		function Add(ANode: TNode): Integer;
		procedure Clear;
		function Count: Integer;
		procedure Delete(AIndex: Integer);
		constructor Create(OwnsObjects: Boolean); overload;
		destructor Destroy; override;
		property Items[Index : Integer]:TNode read GetItem; default;
    function FindByID(AID: String): TNode;
  end;

implementation

{implementation of TNodesList}

function TNodesList.Add(ANode: TNode): Integer;
begin
	Result := List.Add(ANode);
end;

procedure TNodesList.Clear;
begin
	List.Clear;
end;

function TNodesList.Count: Integer;
begin
	Result := List.Count;
end;

procedure TNodesList.Delete(AIndex: Integer);
begin
	List.Delete(AIndex);
end;

function TNodesList.GetItem(AIndex: Integer): TNode;
begin
	Result := TNode(List[AIndex]);
end;

constructor TNodesList.Create(OwnsObjects: Boolean);
begin
	List := TObjectList.Create(OwnsObjects);
end;

destructor TNodesList.Destroy;
begin
	List.Free;
	inherited;
end;

function TNodesList.FindByID(AID: String): TNode;
var
	i: Integer;
begin
	Result := nil;
	for i := 0 to List.Count - 1 do
		if CompareText(TNode(List[i]).NodeID, AID) = 0 then
		begin
			Result := TNode(List[i]);
			Break;
		end;
end;

end.