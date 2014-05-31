unit dSpecifyExtractDates;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, RzButton, StdCtrls,
  RzLabel, ExtCtrls, RzDlgBtn, cxGraphics, cxCustomData,
  cxStyles, cxTL, cxCalendar, cxTextEdit, cxCheckBox, cxInplaceContainer, Mask,
  RzEdit, RzRadChk, cxControls;

type
  TdlgSpecifyExtractDates = class(TForm)
    RzDialogButtons1: TRzDialogButtons;
    RzLabel1: TRzLabel;
    Label1: TLabel;
    btnAdd: TRzButton;
    btnDelete: TRzButton;
    btnClear: TRzButton;
    treeDateRanges: TcxTreeList;
    chkReorderDates: TRzCheckBox;
    chkAddDryDay: TRzCheckBox;
    edtStartDatae: TRzDateTimeEdit;
    cxColStartDate: TcxTreeListColumn;
    cxColEndDate: TcxTreeListColumn;
    procedure treeDateRangesEdited(Sender: TObject; AColumn: TcxTreeListColumn);
    procedure btnAddClick(Sender: TObject);
    procedure btnDeleteClick(Sender: TObject);
    procedure btnClearClick(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
  private
    { Private declarations }
  public
		{ Public declarations }
		function InRange(ADate: TDateTime): Boolean;
	end;

var
	dlgSpecifyExtractDates: TdlgSpecifyExtractDates;

implementation

uses fMain, fConvertInterface;

{$R *.dfm}

procedure TdlgSpecifyExtractDates.btnAddClick(Sender: TObject);
begin
	treeDateRanges.Add;
end;

procedure TdlgSpecifyExtractDates.btnDeleteClick(Sender: TObject);
begin
	if Assigned(treeDateRanges.FocusedNode) then
		treeDateRanges.FocusedNode.Free
	else if treeDateRanges.Count > 0 then
		treeDateRanges.Items[treeDateRanges.Count-1].Free;
end;

procedure TdlgSpecifyExtractDates.btnClearClick(Sender: TObject);
begin
  treeDateRanges.Clear;
end;

procedure TdlgSpecifyExtractDates.FormClose(Sender: TObject;
  var Action: TCloseAction);
var
  i: Integer;
	StartCol: Integer;
	EndCol: Integer;
	CurrentNode: TcxTreeListNode;
begin
	StartCol := cxColStartDate.ItemIndex;
	EndCol := cxColEndDate.ItemIndex;
	if ModalResult = mrCancel then
	begin
		treeDateRanges.Clear;
	end
	else
		for i := treeDateRanges.Count-1 downto 0 do
		begin
			CurrentNode := treeDateRanges.Items[i];
			if (CurrentNode.Texts[StartCol] = '') and (CurrentNode.Texts[EndCol] = '') then
				CurrentNode.Free
			else if (CurrentNode.Texts[EndCol] = '') then
				CurrentNode.Texts[EndCol] := CurrentNode.Texts[StartCol]
			else
				CurrentNode.Texts[StartCol] := CurrentNode.Texts[EndCol];
		end;
end;

function TdlgSpecifyExtractDates.InRange(ADate: TDateTime): Boolean;
var
	i: Integer;
	CurrentNode: TcxTreeListNode;
	StartCol: Integer;
	EndCol: Integer;
	BeginDate, EndDate: TDateTime;
begin
	Result := False;
	StartCol := cxColStartDate.ItemIndex;
	EndCol := cxColEndDate.ItemIndex;
	for i := 0 to treeDateRanges.Count-1 do
	begin
		CurrentNode := treeDateRanges.Items[i];
		BeginDate := StrToDate(CurrentNode.Values[StartCol]);
		EndDate := StrToDate(CurrentNode.Values[EndCol]);
		if (BeginDate <= ADate) and (ADate <= EndDate) then
		begin
			Result := True;
			Break;
    end;
	end;
end;

procedure TdlgSpecifyExtractDates.treeDateRangesEdited(Sender: TObject;
  AColumn: TcxTreeListColumn);
var
	StartCol: Integer;
	EndCol: Integer;
	PrevNode: TcxTreeListNode;
  Node: TcxTreeListNode;
begin
	StartCol := cxColStartDate.ItemIndex;
	EndCol := cxColEndDate.ItemIndex;
  Node := treeDateRanges.FocusedNode;
	if Node.Index > 0 then
		PrevNode := treeDateRanges.Items[Node.Index-1]
	else
		PrevNode := nil;
	if Assigned(PrevNode) and
		(StrToDate(Node.Texts[StartCol]) < StrToDate(PrevNode.Texts[EndCol])) then
		Node.Values[StartCol] := PrevNode.Values[EndCol];
	if (Node.Texts[EndCol] <> '') and (Node.Texts[StartCol] <> '') then
		if StrToDate(Node.Texts[EndCol]) < StrToDate(Node.Texts[StartCol]) then
			Node.Values[EndCol] := Node.Values[StartCol];
end;

end.
