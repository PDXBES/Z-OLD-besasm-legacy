unit dExtractMOUSEPRFNodes;

interface

uses
	Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, StdCtrls, RzLabel, ExtCtrls, RzDlgBtn,
	InterfaceStreams, StStrms, CodeSiteLogging, RzButton, RzRadChk, cxGraphics,
  cxCustomData, cxStyles, cxTL, cxSpinEdit, cxTextEdit, cxCheckBox,
  cxInplaceContainer, cxControls, RzPanel, RzPrgres;

const
	sMOUSE_M11IN_FILENAME: string = 'M11.IN';

type
  TdlgExtractMOUSEPRFNodes = class(TForm)
    RzDialogButtons1: TRzDialogButtons;
    RzLabel1: TRzLabel;
    lblNumSelected: TRzLabel;
    chkWaterLevel: TRzCheckBox;
		chkDischarge: TRzCheckBox;
		chkVelocity: TRzCheckBox;
    RzLabel2: TRzLabel;
    chkOther: TRzCheckBox;
    btnToggleRange: TRzButton;
    btnExcludeAllShown: TRzButton;
    btnIncludeAllShown: TRzBitBtn;
    treeNodes: TcxTreeList;
    cxColRowID: TcxTreeListColumn;
    cxColID: TcxTreeListColumn;
    cxColSelected: TcxTreeListColumn;
    cxColNodeType: TcxTreeListColumn;
    cxColNodeTypeDescription: TcxTreeListColumn;
    cxColUpNodeID: TcxTreeListColumn;
    cxColDnNodeID: TcxTreeListColumn;
    cxColPosition: TcxTreeListColumn;
    cxColPositionUnits: TcxTreeListColumn;
    prgListUpdate: TRzProgressBar;
    procedure treeNodesEditing(Sender: TObject; AColumn: TcxTreeListColumn;
      var Allow: Boolean);
    procedure treeNodesFocusedColumnChanged(Sender: TObject; APrevFocusedColumn,
      AFocusedColumn: TcxTreeListColumn);
    procedure treeNodesEdited(Sender: TObject; AColumn: TcxTreeListColumn);
    procedure treeNodesCompare(Sender: TObject; ANode1, ANode2: TcxTreeListNode;
      var ACompare: Integer);
    procedure cxColSelectedPropertiesEditValueChanged(Sender: TObject);
    procedure treeNodesCustomDrawCell(Sender: TObject; ACanvas: TcxCanvas;
      AViewInfo: TcxTreeListEditCellViewInfo; var ADone: Boolean);
		procedure RzDialogButtons1ClickOk(Sender: TObject);
    procedure RzDialogButtons1ClickCancel(Sender: TObject);
    procedure FormShow(Sender: TObject);
		procedure chkWaterLevelClick(Sender: TObject);
    procedure btnIncludeAllShownClick(Sender: TObject);
    procedure btnExcludeAllShownClick(Sender: TObject);
	private
		NumSelected: Integer;
		{ Private declarations }
		SourceFile: TMOUSE_PRF_M11InFileStream;
	public
		{ Public declarations }
		procedure SetNodesSelect(FirstNode, LastNode: Integer; SelectState: Boolean);
		procedure FillGrid(AM11InFile: TMOUSE_PRF_M11InFileStream); overload;
		procedure FillGrid; overload;
	end;

var
	dlgExtractMOUSEPRFNodes: TdlgExtractMOUSEPRFNodes;

implementation

uses fMain;

{$R *.dfm}

{ TfrmExtractPRFTimeSeries }

procedure TdlgExtractMOUSEPRFNodes.FillGrid(
	AM11InFile: TMOUSE_PRF_M11InFileStream);
var
	nDnNodeID: integer;
	nNodeTypeDescription: Integer;
	nPositionUnits: Integer;
	nPosition: Integer;
	nUpNodeID: Integer;
	nNodeType: Integer;
	nSelected: Integer;
	nRowID: Integer;
	nID: Integer;
	i: Integer;
	ANode: TcxTreeListNode;
begin
	SourceFile := AM11InFile;
	nRowID := cxColRowID.ItemIndex;
	nID := cxColID.ItemIndex;
	nSelected := cxColSelected.ItemIndex;
	nNodeType := cxColNodeType.ItemIndex;
	nNodeTypeDescription := cxColNodeTypeDescription.ItemIndex;
	nUpNodeID := cxColUpNodeID.ItemIndex;
	nDnNodeID := cxColDnNodeID.ItemIndex;
	nPosition := cxColPosition.ItemIndex;
	nPositionUnits := cxColPositionUnits.ItemIndex;
	treeNodes.Clear;
	treeNodes.BeginUpdate;
	for i := 0 to AM11InFile.NodeCount - 1 do    // Iterate
	begin
		ANode := treeNodes.Add;
		ANode.Values[nRowID] := i;
		ANode.Texts[nID] := AM11InFile.Nodes[i].ID;
		ANode.Values[nSelected] := AM11InFile.Nodes[i].Selected;
		ANode.Values[nNodeType] := AM11InFile.Nodes[i].NodeType;
		ANode.Values[nNodeTypeDescription] := AM11InFile.Nodes[i].NodeTypeDescription;
		ANode.Texts[nUpNodeID] := AM11InFile.Nodes[i].UpNodeID;
		ANode.Texts[nDnNodeID] := AM11InFile.Nodes[i].DnNodeID;
		ANode.Values[nPosition] := AM11InFile.Nodes[i].Position;
		ANode.Texts[nPositionUnits] := AM11InFile.Nodes[i].PositionUnits;
	end;    // for
	treeNodes.EndUpdate;
end;

procedure TdlgExtractMOUSEPRFNodes.RzDialogButtons1ClickOk(
	Sender: TObject);
var
	OutString: string;
	nSelected: Integer;
	CurrentNode: TcxTreeListNode;
	i: Integer;
	OutFile: TFileStream;
	OutTextFile: TStAnsiTextStream;
	OutFileName: String;
begin
	OutFileName := ExtractFileDir(SourceFile.FileName) + '\' + sMOUSE_M11IN_FILENAME;
	OutFile := TFileStream.Create(OutFileName, fmCreate);
	OutTextFile := TStAnsiTextStream.Create(OutFile);
  FillGrid;
  cxColRowID.SortOrder := soAscending;
	nSelected := cxColSelected.ItemIndex;
	for i := 1 to 12 do    // Iterate
	begin
		OutTextFile.WriteLine('//');
	end;    // for
	OutTextFile.WriteLine('1 2'); // Required for time field
	for i := 1 to treeNodes.Count - 1 do    // Iterate
	begin
		CurrentNode := treeNodes.Items[i];
		if CurrentNode.Values[nSelected] then
			OutString := Format('1 %d', [SourceFile.Nodes[i].NodeTypeID])
		else
			OutString := Format('0 %d', [SourceFile.Nodes[i].NodeTypeID]);
		OutTextFile.WriteLine(OutString);
	end;    // for
	OutTextFile.WriteLine('0 3'); // Required for 'Time Series'
	OutTextFile.Free;
	OutFile.Free;
	ModalResult := mrOK;
end;

procedure TdlgExtractMOUSEPRFNodes.RzDialogButtons1ClickCancel(
  Sender: TObject);
begin
  ModalResult := mrCancel;
end;

procedure TdlgExtractMOUSEPRFNodes.FormShow(Sender: TObject);
begin
	NumSelected := 1;
	chkWaterLevel.Checked := True;
	chkDischarge.Checked := True;
	chkVelocity.Checked := True;
  chkOther.Checked := True;
	lblNumSelected.Caption := '';
end;

procedure TdlgExtractMOUSEPRFNodes.chkWaterLevelClick(Sender: TObject);
var
  saveCursor: TCursor;
	i: Integer;
	nNodeType: Integer;
	ANode: TcxTreeListNode;
	DeleteNode: boolean;
	DeleteNodeTypeSet: set of TPRFNodeType;
begin
{	nNodeType := cxColNodeType.ItemIndex;
	DeleteNodeTypeSet := [];
	if not chkWaterLevel.Checked then
		DeleteNodeTypeSet := DeleteNodeTypeSet + [ntLevel];
	if not chkDischarge.Checked then
		DeleteNodeTypeSet := DeleteNodeTypeSet + [ntFlow];
	if not chkVelocity.Checked then
		DeleteNodeTypeSet := DeleteNodeTypeSet + [ntVelocity];
	if not chkOther.Checked then
		DeleteNodeTypeSet := DeleteNodeTypeSet + [ntOther, ntTime, ntVolume];

	saveCursor := Screen.Cursor;
	Screen.Cursor := crHourGlass;
	try
		FillGrid;
		treeNodes.BeginUpdate;
    prgListUpdate.TotalParts := treeNodes.Count;
    prgListUpdate.Show;
		for i := treeNodes.Count-1 downto 0 do
		begin
			ANode := treeNodes.Items[i];
			DeleteNode := TPRFNodeType(StrToInt(ANode.Values[nNodeType])) in DeleteNodeTypeSet;
			if DeleteNode then
				ANode.Free;
      prgListUpdate.IncPartsByOne;
      prgListUpdate.Update;
		end;
		treeNodes.EndUpdate;
    prgListUpdate.Hide;
	finally
		Screen.Cursor := saveCursor;
	end;  // try/finally }
  FillGrid;
end;

procedure TdlgExtractMOUSEPRFNodes.FillGrid;
var
	nDnNodeID: integer;
	nNodeTypeDescription: Integer;
	nPositionUnits: Integer;
	nPosition: Integer;
	nUpNodeID: Integer;
	nNodeType: Integer;
	nSelected: Integer;
	nRowID: Integer;
	nID: Integer;
	i: Integer;
	ANode: TcxTreeListNode;
begin
	nRowID := cxColRowID.ItemIndex;
	nID := cxColID.ItemIndex;
	nSelected := cxColSelected.ItemIndex;
	nNodeType := cxColNodeType.ItemIndex;
	nNodeTypeDescription := cxColNodeTypeDescription.ItemIndex;
	nUpNodeID := cxColUpNodeID.ItemIndex;
	nDnNodeID := cxColDnNodeID.ItemIndex;
	nPosition := cxColPosition.ItemIndex;
	nPositionUnits := cxColPositionUnits.ItemIndex;
	treeNodes.Clear;
	treeNodes.BeginUpdate;
  prgListUpdate.PartsComplete := 0;
  prgListUpdate.TotalParts := SourceFile.NodeCount;
  prgListUpdate.Show;
	for i := 0 to SourceFile.NodeCount - 1 do    // Iterate
	begin
    if (not ((chkWaterLevel.Checked) and (SourceFile.Nodes[i].NodeType = ntLevel))) and
      (not ((chkDischarge.Checked) and (SourceFile.Nodes[i].NodeType = ntFlow))) and
      (not ((chkVelocity.Checked) and (SourceFile.Nodes[i].NodeType = ntVelocity))) and
      (not ((chkOther.Checked) and ((SourceFile.Nodes[i].NodeType = ntOther) or
        (SourceFile.Nodes[i].NodeType = ntTime) or (SourceFile.Nodes[i].NodeType = ntVolume)))) then
      Continue;
		ANode := treeNodes.Add;
		ANode.Values[nRowID] := i;
		ANode.Texts[nID] := SourceFile.Nodes[i].ID;
		ANode.Values[nSelected] := SourceFile.Nodes[i].Selected;
		ANode.Values[nNodeType] := SourceFile.Nodes[i].NodeType;
		ANode.Values[nNodeTypeDescription] := SourceFile.Nodes[i].NodeTypeDescription;
		ANode.Texts[nUpNodeID] := SourceFile.Nodes[i].UpNodeID;
		ANode.Texts[nDnNodeID] := SourceFile.Nodes[i].DnNodeID;
		ANode.Values[nPosition] := SourceFile.Nodes[i].Position;
		ANode.Texts[nPositionUnits] := SourceFile.Nodes[i].PositionUnits;
    prgListUpdate.IncPartsByOne;
    prgListUpdate.Update;
	end;    // for
	treeNodes.EndUpdate;
  treeNodes.Repaint;
  prgListUpdate.Hide;
end;

procedure TdlgExtractMOUSEPRFNodes.btnIncludeAllShownClick(
	Sender: TObject);
begin
	SetNodesSelect(0, treeNodes.Count-1, True);
end;

procedure TdlgExtractMOUSEPRFNodes.btnExcludeAllShownClick(
	Sender: TObject);
begin
	SetNodesSelect(0, treeNodes.Count-1, False);
end;

procedure TdlgExtractMOUSEPRFNodes.SetNodesSelect(FirstNode,
	LastNode: Integer; SelectState: Boolean);
var
	nRowID: Integer;
	nSelected: Integer;
	i: Integer;
	ANode: TcxTreeListNode;
	ASelectNode: TPRFSelectNode;
begin
	nRowID := cxColRowID.ItemIndex;
	nSelected := cxColSelected.ItemIndex;
	for i := 0 to treeNodes.Count - 1 do    // Iterate
	begin
		ANode := treeNodes.Items[i];
		if ANode.Values[nRowID] = 0 then
			Continue; // skip the Time node, since it must always be checked
		if SelectState = True then
		begin
			ANode.Values[nSelected] := 'True';
			Inc(NumSelected);
		end
		else
		begin
			ANode.Values[nSelected] := 'False';
      Dec(NumSelected);
		end;
		ASelectNode := SourceFile.Nodes[StrToInt(ANode.Values[nRowID])];
		ASelectNode.Selected := SelectState;
		SourceFile.Nodes[StrToInt(ANode.Values[nRowID])] := ASelectNode;
	end;    // for
	lblNumSelected.Caption := Format('%d selected', [NumSelected]);
end;

procedure TdlgExtractMOUSEPRFNodes.treeNodesCustomDrawCell(Sender: TObject;
  ACanvas: TcxCanvas; AViewInfo: TcxTreeListEditCellViewInfo;
  var ADone: Boolean);
var
	nRowID: Integer;
  ANode: TcxTreeListNode;
begin
	nRowID := cxColRowID.ItemIndex;
  case AViewInfo.Column.ItemIndex of
    0,1,3,4,5,6,7,8:
    begin
      ACanvas.Brush.Color := clBtnFace;
      ACanvas.FillRect(AViewInfo.BoundsRect);
    end;
  end;
end;

procedure TdlgExtractMOUSEPRFNodes.cxColSelectedPropertiesEditValueChanged(
  Sender: TObject);
begin
  if treeNodes.FocusedNode.Values[cxColSelected.ItemIndex] = 'True' then
		Inc(NumSelected)
	else
		Dec(NumSelected);
	lblNumSelected.Caption := Format('%d selected', [NumSelected]);
end;

procedure TdlgExtractMOUSEPRFNodes.treeNodesCompare(Sender: TObject; ANode1,
  ANode2: TcxTreeListNode; var ACompare: Integer);
var
	nPositionID: Integer;
	nRowID: Integer;
	ActiveCol: Integer;
begin
	nRowID := cxColRowID.ItemIndex;
	nPositionID := cxColPosition.ItemIndex;
	if cxColRowID.SortOrder <> soNone then
	begin
		ActiveCol := nRowID;
		ACompare := 0;
		if StrToInt(ANode1.Texts[nRowID]) > StrToInt(ANode2.Texts[nRowID]) then
			ACompare := 1
		else if StrToInt(ANode1.Texts[nRowID]) < StrToInt(ANode2.Texts[nRowID]) then
			ACompare := -1;
	end
	else if cxColPosition.SortOrder <> soNone then
	begin
		ActiveCol := nPositionID;
		ACompare := 0;
		if StrToInt(ANode1.Texts[nPositionID]) > StrToInt(ANode2.Texts[nPositionID]) then
			ACompare := 1
		else if StrToInt(ANode1.Texts[nPositionID]) < StrToInt(ANode2.Texts[nPositionID]) then
			ACompare := -1;
	end
	else
	begin
		if cxColID.SortOrder <> soNone then
			ActiveCol := cxColID.ItemIndex
		else if cxColSelected.SortOrder <> soNone then
			ActiveCol := cxColSelected.ItemIndex
		else if cxColNodeTypeDescription.SortOrder <> soNone then
			ActiveCol := cxColNodeTypeDescription.ItemIndex
		else if cxColUpNodeID.SortOrder <> soNone then
			ActiveCol := cxColUpNodeID.ItemIndex
		else if cxColDnNodeID.SortOrder <> soNone then
			ActiveCol := cxColDnNodeID.ItemIndex
		else if cxColPositionUnits.SortOrder <> soNone then
			ActiveCol := cxColPositionUnits.ItemIndex;

		ACompare := AnsiCompareStr(ANode1.Texts[ActiveCol], ANode2.Texts[ActiveCol]);
	end;
	if treeNodes.Columns[ActiveCol].SortOrder = soDescending then
		ACompare := ACompare * -1
end;

procedure TdlgExtractMOUSEPRFNodes.treeNodesEdited(Sender: TObject;
  AColumn: TcxTreeListColumn);
var
	ModifyNode: TPRFSelectNode;
  Node: TcxTreeListNode;
	nRowID: Integer;
begin
	nRowID := cxColRowID.ItemIndex;
  Node := treeNodes.FocusedNode;
	ModifyNode := SourceFile.Nodes[Node.Values[nRowID]];
	ModifyNode.Selected := Node.Values[cxColSelected.ItemIndex] = 'True';
	SourceFile.Nodes[Node.Values[nRowID]] := ModifyNode;
end;

procedure TdlgExtractMOUSEPRFNodes.treeNodesFocusedColumnChanged(
  Sender: TObject; APrevFocusedColumn, AFocusedColumn: TcxTreeListColumn);
begin
  treeNodes.OptionsBehavior.IncSearch := (AFocusedColumn = cxColSelected);
end;

procedure TdlgExtractMOUSEPRFNodes.treeNodesEditing(Sender: TObject;
  AColumn: TcxTreeListColumn; var Allow: Boolean);
var
	nRowID: Integer;
begin
	nRowID := cxColRowID.ItemIndex;
	if treeNodes.FocusedNode.Values[nRowID] = 0 then
		Allow := False;
end;

end.

