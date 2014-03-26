unit fBatchConvertCombine;

interface

uses
	Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, fMain, fModuleMaster, StdCtrls, Mask, RzEdit, RzBtnEdt, RzLabel,
	RzButton, ExtCtrls, RzPanel, cxGraphics, cxCustomData, cxStyles, cxTL,
	cxTextEdit, cxInplaceContainer, cxControls, uIM_ConvertEngine,
	uIM_MultiCombineEngine, uIM_BatchConvertCombineSupport, cxCheckBox,
	cxProgressBar, uIM_ProgressTracker, Menus;

type

	TfrmBatchConvertCombine = class(TfrmModuleMaster)
		RzLabel2: TRzLabel;
		edtBatchFile: TRzButtonEdit;
		RzPanel1: TRzPanel;
		btnRun: TRzButton;
		btnCloseTask: TRzButton;
		RzLabel3: TRzLabel;
		treeCommands: TcxTreeList;
		cxColCommandType: TcxTreeListColumn;
		RzMemo1: TRzMemo;
		RzLabel4: TRzLabel;
		cxColDescription: TcxTreeListColumn;
		RzButton3: TRzButton;
		cxColRunOrder: TcxTreeListColumn;
		cxColInclude: TcxTreeListColumn;
		btnAddConversion: TRzButton;
		btnAddMulticombine: TRzButton;
		cxColProgress: TcxTreeListColumn;
    RzMenuButton1: TRzMenuButton;
    PopupMenu1: TPopupMenu;
    AddConversion1: TMenuItem;
    AddMultiCombine1: TMenuItem;
    AddWWStats1: TMenuItem;
    procedure cxColIncludePropertiesChange(Sender: TObject);
		procedure btnRunClick(Sender: TObject);
		procedure edtBatchFileButtonClick(Sender: TObject);
		procedure FormDestroy(Sender: TObject);
		procedure FormCreate(Sender: TObject);
		procedure btnCloseTaskClick(Sender: TObject);
	private
		{ Private declarations }
		BatchEngine: TIM_BCC_BatchEngine;
		CombineEngine: TCombineEngine;
		ConvertEngine: TConvertEngine;
	public
		{ Public declarations }
	end;

var
	frmBatchConvertCombine: TfrmBatchConvertCombine;

implementation

uses CodeSiteLogging;

{$R *.dfm}

procedure TfrmBatchConvertCombine.btnCloseTaskClick(Sender: TObject);
begin
	inherited;
	Close;
	frmBatchConvertCombine := nil;
end;

procedure TfrmBatchConvertCombine.FormCreate(Sender: TObject);
begin
	inherited;
	ConvertEngine := TConvertEngine.Create;
	CombineEngine := TCombineEngine.Create;
end;

procedure TfrmBatchConvertCombine.FormDestroy(Sender: TObject);
begin
	BatchEngine.Free;
	ConvertEngine.Free;
	CombineEngine.Free;
	inherited;
end;

procedure TfrmBatchConvertCombine.edtBatchFileButtonClick(Sender: TObject);
var
	BatchFile: String;
	i: Integer;
	CurrentNode: TcxTreeListNode;
begin
	inherited;
	frmMain.dlgOpen.Title := 'Open Batch File';
	frmMain.dlgOpen.Filter := 'IM Batch File (*.ini)|*.ini|All files (*.*)|*.*';
	frmMain.dlgOpen.FilterIndex := 1;
	if frmMain.dlgOpen.Execute then
	begin
		BatchFile := frmMain.dlgOpen.FileName;
		BatchEngine := TIM_BCC_BatchEngine.Create(BatchFile);
		edtBatchFile.Text := frmMain.dlgOpen.FileName;

		treeCommands.Clear;
		for i := 0 to BatchEngine.Count-1 do
		begin
			CurrentNode := treeCommands.Add;
			CurrentNode.Values[cxColRunOrder.ItemIndex] := i+1;
			CurrentNode.Values[cxColInclude.ItemIndex] := BatchEngine.Enabled[i];
			CurrentNode.Values[cxColCommandType.ItemIndex] := BatchEngine.CommandType[i];
			CurrentNode.Values[cxColDescription.ItemIndex] := BatchEngine.Description[i];
		end;
	end;

  frmMain.SaveDirToRegistry;
end;

procedure TfrmBatchConvertCombine.btnRunClick(Sender: TObject);
begin
	inherited;
	BatchEngine.Execute;
	treeCommands.Clear;
end;

procedure TfrmBatchConvertCombine.cxColIncludePropertiesChange(Sender: TObject);
var
	CurrentNode: TcxTreeListNode;
begin
	inherited;
	CurrentNode := treeCommands.FocusedNode;
	BatchEngine.Enabled[CurrentNode.Index] := CurrentNode.Values[cxColInclude.ItemIndex];
end;

end.
