unit fMultiCombineInterfaceFiles;

interface

uses
	Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, fModuleMaster, StdCtrls, RzLabel,
	Mask, RzEdit, RzBtnEdt, RzButton, InterfaceStreams, RzTabs,
	ExtCtrls, RzPanel, RzRadGrp, DateUtils, Types, PDXDateUtils, Math, CodeSiteLogging,
	ActnList, RzCmboBx, DB, ADODB, RzPrgres, IniFiles, ComCtrls, RzListVw,
	RzLstBox, RzRadChk, StStrL, ComObj, StStrms, RzBorder, RzErrHnd,
	cxInplaceContainer, cxTL, cxControls, cxStyles, cxGraphics, cxCustomData,
  cxTextEdit, cxDropDownEdit, cxCalendar, cxTimeEdit, cxCheckBox, cxSpinEdit,
  RzDTP;

const
	ExceptionConversionsSection = 'MultiCombineExceptionConversions';
	MultipleFlowExclusionsSection = 'MultiCombineMultipleFlowExclusions';
  PDXNodeIDSize = 6;
type
  TLeavePage =(lpPrevious, lpNext);

	TInterfaceTransferRec = record
		Include: Boolean;
		OriginalID: String;
		NewID: String;
	end;

	TInterfaceTransferFileRec = record
		TransferRecs: array of TInterfaceTransferRec;
		FileName: String;
		FileFormat: String;
		FileActualStartTime: TDateTime;
		FileActualEndTime: TDateTime;
		FileExtractStartTime: TDateTime; // Relative to actual start time
		FileExtractEndTime: TDateTime; // Relative to actual start time
		FileRelativeStartTime: TDateTime;
		Stream: IFlowStream;
	end;
	TInterfaceTransferFileRecArray = array of TInterfaceTransferFileRec;

	TInterfaceFileRec = record
		FileName: String;
		Location: String;
	end;

  TInterfaceFileRecArray = array of TInterfaceFileRec;

  TExceptionConversion = class
  public
    InterfaceFileIndex: Integer;
		FlowIndex: Integer;
  end;

  TMultipleFlowExclusion = class
  public
    Target: String;
    InterfaceFileIndex: Integer;
		FlowIndex: Integer;
  end;

	TfrmMultiCombineInterfaceFiles = class(TfrmModuleMaster)
    pgcMultiCombineWizard: TRzPageControl;
    shtSpecifyFiles: TRzTabSheet;
    shtSelectNodes: TRzTabSheet;
    shtSaveInterface: TRzTabSheet;
    lblCurrentDate: TRzLabel;
    btnProcess: TRzButton;
    rgrpFormat: TRzRadioGroup;
    edtSaveFile: TRzButtonEdit;
		RzLabel4: TRzLabel;
    btnAddInterfaceFile: TRzButton;
		btnRemoveInterfaceFile: TRzButton;
    btnResolveStartTimes: TRzButton;
    btnLockInterfaceFiles: TRzButton;
		btnAutoGenerateTransfers: TRzButton;
    pgcNodes: TRzPageControl;
    shtSelectedInterfaceFile: TRzTabSheet;
		RzPanel1: TRzPanel;
    RzButton2: TRzButton;
    RzButton3: TRzButton;
    btnCloseTask: TRzButton;
    ActionList1: TActionList;
    actPrevious: TAction;
    actNext: TAction;
    RzLabel8: TRzLabel;
		RzLabel2: TRzLabel;
		RzLabel3: TRzLabel;
		btnLoadConfiguration: TRzButton;
    actProcess: TAction;
    actExcludeAll: TAction;
		actIncludeAll: TAction;
		actToggleRange: TAction;
    RzPanel2: TRzPanel;
    btnExcludeAll: TRzButton;
    btnIncludeAll: TRzButton;
    btnToggleRange: TRzButton;
    RzPanel3: TRzPanel;
    RzLabel5: TRzLabel;
    adoReceivingLinks: TADOConnection;
    adoqUpstreamLinks: TADOQuery;
    adoqUpstreamNodes: TADOQuery;
    adoqUpstreamLinksCompKey: TIntegerField;
    adoqUpstreamNodesUSNode: TWideStringField;
		prgAutoGenerateTransfers: TRzProgressBar;
		adoqUpstreamLinksSimLinkID: TWideStringField;
		adoqUpstreamLinksUSNode: TWideStringField;
		lblAutoGenerateStatus: TRzLabel;
		RzLabel6: TRzLabel;
		edtInterfaceFileStartDate: TRzDateTimeEdit;
		edtInterfaceFileStartTime: TRzDateTimeEdit;
		RzLabel7: TRzLabel;
    btnSaveConfiguration: TRzButton;
    shtReviewExceptionalNodes: TRzTabSheet;
    RzLabel9: TRzLabel;
    lstInterfaceFiles: TRzListBox;
    RzLabel10: TRzLabel;
    adoqUpstreamLinksMLinkID: TIntegerField;
    lblNumExceptionalNodes: TRzLabel;
    adoLinks: TADOTable;
    adoLinksUSNode: TWideStringField;
    adoLinksDSNode: TWideStringField;
    adoLinksMLinkID: TIntegerField;
    adoLinksSimLinkID: TWideStringField;
    shtReviewMultipleInflows: TRzTabSheet;
    RzLabel11: TRzLabel;
    shtReviewMultiCombinedInterfaceFile: TRzTabSheet;
    RzLabel12: TRzLabel;
    RzLabel13: TRzLabel;
    pnlPreviewAssembly: TRzPanel;
    lblPreviewAssembly: TRzLabel;
    prgPreviewAssembly: TRzProgressBar;
    pnlMultipleInflowsAssembly: TRzPanel;
    RzLabel14: TRzLabel;
    prgMultipleInflowsAssembly: TRzProgressBar;
    RzLabel15: TRzLabel;
    RzPanel4: TRzPanel;
    lblCurrentInterfaceFile: TRzLabel;
    btnExportExceptionalNodes: TRzButton;
    lblTotalCompiledNodes: TRzLabel;
    RzButton1: TRzButton;
    lblTotalNodes: TRzLabel;
    lblTotalVolume: TRzLabel;
    RzLabel19: TRzLabel;
    RzBorder1: TRzBorder;
    treeInterfaceFiles: TcxTreeList;
    colInterfaceFile: TcxTreeListColumn;
    colLocation: TcxTreeListColumn;
    colFormat: TcxTreeListColumn;
    colFileStart: TcxTreeListColumn;
    colExtractStartDate: TcxTreeListColumn;
    colNewStartTime: TcxTreeListColumn;
    colNewStartDate: TcxTreeListColumn;
    colExtractEndDate: TcxTreeListColumn;
    colExtractStartTime: TcxTreeListColumn;
    colExtractEndTime: TcxTreeListColumn;
    colExcludeFromAutoGen: TcxTreeListColumn;
    cxStyleRepository2: TcxStyleRepository;
    cxStyle1: TcxStyle;
    colSkipInterval: TcxTreeListColumn;
    lblNumSelectedNodes: TRzLabel;
    treeNodes: TcxTreeList;
    cxColRecord: TcxTreeListColumn;
    cxColIncludeNode: TcxTreeListColumn;
    cxColOriginalID: TcxTreeListColumn;
    cxColNewID: TcxTreeListColumn;
    cxColFlowMultiplier: TcxTreeListColumn;
    prgCombine: TRzProgressBar;
    errHandler: TRzErrorHandler;
    treeExceptionalNodes: TcxTreeList;
    cxColStopPipeNotFound: TcxTreeListColumn;
    cxColStopPipeUSNode: TcxTreeListColumn;
    cxColPipeNotFoundFileAssignment: TcxTreeListColumn;
    cxColPipeNotFoundFlowAssignment: TcxTreeListColumn;
    treeMultipleInflows: TcxTreeList;
    cxColMultipleInflowsTarget: TcxTreeListColumn;
    cxColMultipleInflowsInclude: TcxTreeListColumn;
    cxColMultipleInflowsFile: TcxTreeListColumn;
    cxColMultipleInflowsFlow: TcxTreeListColumn;
    treePreviewNodes: TcxTreeList;
    cxColPreviewNewID: TcxTreeListColumn;
    cxColFromInterfaceFile: TcxTreeListColumn;
    cxColPreviewOriginalID: TcxTreeListColumn;
    cxColInterfaceIndex: TcxTreeListColumn;
    cxColInterfaceFlowIndex: TcxTreeListColumn;
    treeCheck: TcxTreeList;
    cxColFileName: TcxTreeListColumn;
    cxColNumNodes: TcxTreeListColumn;
    cxColTotalVolume: TcxTreeListColumn;
    btnCopyCheckToClipboard: TRzButton;
    btnMakeExtractSameAsStart: TRzButton;
    RzLabel16: TRzLabel;
    edtSyncDate: TRzDateTimeEdit;
    procedure treeMultipleInflowsCustomDrawCell(Sender: TObject;
      ACanvas: TcxCanvas; AViewInfo: TcxTreeListEditCellViewInfo;
      var ADone: Boolean);
    procedure edtSyncDateChange(Sender: TObject);
    procedure btnMakeExtractSameAsStartClick(Sender: TObject);
    procedure btnCopyCheckToClipboardClick(Sender: TObject);
    procedure treeCheckCustomDrawCell(Sender: TObject; ACanvas: TcxCanvas;
      AViewInfo: TcxTreeListEditCellViewInfo; var ADone: Boolean);
    procedure treeMultipleInflowsEditing(Sender: TObject;
      AColumn: TcxTreeListColumn; var Allow: Boolean);
    procedure treeExceptionalNodesCustomDrawCell(Sender: TObject;
      ACanvas: TcxCanvas; AViewInfo: TcxTreeListEditCellViewInfo;
      var ADone: Boolean);
    procedure treeMultipleInflowsCompare(Sender: TObject; ANode1,
      ANode2: TcxTreeListNode; var ACompare: Integer);
    procedure treeExceptionalNodesFocusedColumnChanged(Sender: TObject;
      APrevFocusedColumn, AFocusedColumn: TcxTreeListColumn);
    procedure treeExceptionalNodesCompare(Sender: TObject; ANode1,
      ANode2: TcxTreeListNode; var ACompare: Integer);
    procedure treeNodesEdited(Sender: TObject; AColumn: TcxTreeListColumn);
    procedure treePreviewNodesFocusedColumnChanged(Sender: TObject;
      APrevFocusedColumn, AFocusedColumn: TcxTreeListColumn);
    procedure treeNodesFocusedColumnChanged(Sender: TObject; APrevFocusedColumn,
      AFocusedColumn: TcxTreeListColumn);
    procedure treeInterfaceFilesCustomDrawCell(Sender: TObject;
      ACanvas: TcxCanvas; AViewInfo: TcxTreeListEditCellViewInfo;
      var ADone: Boolean);
		procedure btnCloseTaskClick(Sender: TObject);
		procedure btnAddInterfaceFileClick(Sender: TObject);
		procedure btnResolveStartTimesClick(Sender: TObject);
		procedure treeInterfaceFilesMouseUp(Sender: TObject;
			Button: TMouseButton; Shift: TShiftState; X, Y: Integer);
		procedure btnLockInterfaceFilesClick(Sender: TObject);
		procedure btnAutoGenerateTransfersClick(Sender: TObject);
		procedure edtSaveFileButtonClick(Sender: TObject);
		procedure actPreviousExecute(Sender: TObject);
		procedure actPreviousUpdate(Sender: TObject);
		procedure actNextExecute(Sender: TObject);
		procedure actNextUpdate(Sender: TObject);
		procedure actProcessExecute(Sender: TObject);
		procedure actProcessUpdate(Sender: TObject);
		procedure actExcludeAllExecute(Sender: TObject);
		procedure actIncludeAllExecute(Sender: TObject);
		procedure actToggleRangeExecute(Sender: TObject);
		procedure FormCreate(Sender: TObject);
		procedure btnSaveConfigurationClick(Sender: TObject);
		procedure btnLoadConfigurationClick(Sender: TObject);
		procedure btnRemoveInterfaceFileClick(Sender: TObject);
		procedure lstInterfaceFilesClick(Sender: TObject);
		procedure FormDestroy(Sender: TObject);
    procedure btnExportExceptionalNodesClick(Sender: TObject);
	private
		{ Private declarations }
		ExceptionConversions: TStringList;
    MultipleFlowExclusions: TStringList;
		AutoGenerated: Boolean;
		ModelIni: TMemIniFile;
	public
		{ Public declarations }
		InterfaceTransferFileRecs: TInterfaceTransferFileRecArray;
		InterfaceFiles: TInterfaceFileRecArray;
		function GetInterfaceFileName(AIndex: Integer): String;
		function GetInterfaceFileFormat(AIndex: Integer): String;
		function GetInterfaceFileRelativeStartDate(AIndex: Integer): TDate;
		function GetInterfaceFileRelativeStartTIme(AIndex: Integer): TTime;
		procedure ResolveStartTime(AIndex: Integer);
		procedure ReadTransferRecords;
		procedure EnableInterfaceFileUI(IsEnabled: Boolean);
		function FindPreviewNodeByID(AID: String): TcxTreeListNode;
		function FindMultipleInflowByID(AID: String): TcxTreeListNode;
		function AllFilesEOF: Boolean;
		function GetEarliestNextTime: TDateTime;
		function GetEarliestCurrentTime: TDateTime;
		function GetEarliestInitialTime: TDateTime;
		function GetEarliestAvailableTime(FromTime: TDateTime): TDateTime;
		function GetLatestAvailableTime: TDateTime;
		function GetEarliestRelativeTime: TDateTime;
		procedure ConnectInterfaceFilesToModel(AModelPath: String);
		function CreateFlowStream(AFile: String; AFormat: String): IFlowStream;
		function AddTransferFile(AFile: String; AFormat: String): Integer;
		procedure DeleteTransferFile(AIndex: Integer);
		function AddTransferPoint(AFileIndex: Integer; AOriginalID: String;
			AInclude: Boolean; ANewID: String): Integer;
		procedure PreparePreviewInterfaceFile;
    procedure SaveExceptionConversions;
    procedure ResetExceptionConversions;
    procedure SaveMultipleFlowExclusions;
    procedure ResetMultipleFlowExclusions;
    procedure FillInterfaceFilesStringList(AStringList: TStringList);
    procedure FillInterfaceFileNodesStringList(AStringList: TStringList;
      InterfaceFileIndex: Integer);
		procedure LeavePage(Direction: TLeavePage);
		procedure FindDuplicateFlows;
	end;

var
	frmMultiCombineInterfaceFiles: TfrmMultiCombineInterfaceFiles;

implementation

uses fMain, dToggleRange, StrUtils, RzShellDialogs, Clipbrd;

{$R *.dfm}

procedure TfrmMultiCombineInterfaceFiles.btnCloseTaskClick(
	Sender: TObject);
begin
	inherited;
	Close;
	frmMultiCombineInterfaceFiles :=nil;
end;

procedure TfrmMultiCombineInterfaceFiles.btnAddInterfaceFileClick(
	Sender: TObject);
var
	ANode: TcxTreeListNode;
	InterfaceFilesIndex: Integer;
	i: Integer;
begin
	inherited;
	frmMain.dlgOpen.InitialDir := frmMain.InitRegistryDir;
	frmMain.dlgOpen.Title := 'Add Interface File';
	frmMain.dlgOpen.Filter := 'Interface Files (*.int,*.r05,*.r15,*.r20,*.r60,*.t05,*.t15,*.t20,*.t60,*.x05,*.x15,*.x20,*.x60)'+
		'|*.int;*.r05;*.r15;*.r20;*.r60;*.t05;*.t15;*.t20;*.t60;*.x05;*.x15;*.x20;*.x60'+
		'|SYF Files (*.syf)|*.syf|PDX Extran Output Files (*.out; *.xot)|*.out;*.xot|All Files|*.*';
	frmMain.dlgOpen.DefaultExt := 'int';
	frmMain.dlgOpen.Options := frmMain.dlgOpen.Options + [osoAllowMultiselect];
	if frmMain.dlgOpen.Execute then
	begin
		for i := 0 to frmMain.dlgOpen.Files.Count-1 do
		begin
			SetLength(InterfaceFiles, Length(InterfaceFiles)+1);
			InterfaceFilesIndex := Length(InterfaceFiles)-1;
			InterfaceFiles[InterfaceFilesIndex].FileName := ExtractFileName(frmMain.dlgOpen.Files[i]);
			InterfaceFiles[InterfaceFilesIndex].Location :=
				ExcludeTrailingPathDelimiter(ExtractFilePath(frmMain.dlgOpen.Files[i]));
			ANode := treeInterfaceFiles.Add;
			ANode.Values[colInterfaceFile.ItemIndex] :=
				ExtractFileName(frmMain.dlgOpen.Files[i]);
			ANode.Values[colLocation.ItemIndex] :=
				ExcludeTrailingPathDelimiter(ExtractFilePath(frmMain.dlgOpen.Files[i]));
      if Uppercase(ExtractFileExt(frmMain.dlgOpen.Files[i])) = '.SYF' then
				ANode.Values[colFormat.ItemIndex] :=
					(colFormat.Properties as TcxComboBoxProperties).Items[9]
      else if (Uppercase(ExtractFileExt(frmMain.dlgOpen.Files[i])) = '.XOT') or
        (Uppercase(ExtractFileExt(frmMain.dlgOpen.Files[i])) = '.OUT') then
				ANode.Values[colFormat.ItemIndex] :=
					(colFormat.Properties as TcxComboBoxProperties).Items[5]
      else
				ANode.Values[colFormat.ItemIndex] :=
					(colFormat.Properties as TcxComboBoxProperties).Items[3];
			ANode.Values[colFileStart.ItemIndex] := 'Click Resolve Start Times';
			ANode.Values[colExcludeFromAutoGen.ItemIndex] := False;
		end;
		frmMain.SaveDirToRegistry;
	end;
	frmMain.dlgOpen.Options := frmMain.dlgOpen.Options - [osoAllowMultiselect];
end;

procedure TfrmMultiCombineInterfaceFiles.btnResolveStartTimesClick(
	Sender: TObject);
var
	i: Integer;
	AInterfaceFile: IFlowStream;
	FileFormat: String;
	FileName: String;
begin
	for i := 0 to treeInterfaceFiles.Count-1 do
	begin
		FileFormat := GetInterfaceFileFormat(i);
		FileName := GetInterfaceFileName(i);
		if (FileFormat = 'PDX-32 Runoff') or (FileFormat = 'PDX-32 Conveyance') then
			AInterfaceFile := TSWMMStandardInterfaceFile.Create(FileName, fmOpenRead or fmShareDenyWrite, if32)
		else if (FileFormat = 'PDX-95 Runoff') or (FileFormat = 'PDX-95 Conveyance')
			or (FileFormat = 'PDX-95 SYF Conveyance') then
			AInterfaceFile := TSWMMStandardInterfaceFile.Create(FileName, fmOpenRead or fmShareDenyWrite, if95)
		else if (FileFormat = 'PDX-95 Conveyance Text') then
			AInterfaceFile := TPDXTextStream.Create(FileName, fmOpenRead or fmShareDenyWrite)
		else if (FileFormat = 'XP Runoff') or (FileFormat = 'XP Conveyance') then
			AInterfaceFile := TSWMMStandardInterfaceFile.Create(FileName, fmOpenRead or fmShareDenyWrite, ifXP)
		else if FileFormat = 'XP SYF Conveyance' then
			AInterfaceFile := TXP_SYF_FileStream.Create(FileName, fmOpenRead or fmShareDenyWrite)
		else
		begin
			ShowMessage(FileFormat + ' is not implemented');
			treeInterfaceFiles.Items[i].Values[colFileStart.ItemIndex] := 'Format not implemented';
			Continue;
		end;
		treeInterfaceFiles.Items[i].Values[colFileStart.ItemIndex] :=
			FormatDateTime('mm/dd/yyyy hh:mm:ss', AInterfaceFile.InitialTime);
{		if (treeInterfaceFiles.Items[i].Values[colNewStartDate.ItemIndex] = Null) then
			treeInterfaceFiles.Items[i].Values[colNewStartDate.ItemIndex] :=
      FormatDateTime('mm/dd/yyyy', AInterfaceFile.InitialTime);
		if (treeInterfaceFiles.Items[i].Values[colNewStartTime.ItemIndex] = Null) then
			treeInterfaceFiles.Items[i].Values[colNewStartTime.ItemIndex] :=
      FormatDateTime('hh:mm', AInterfaceFile.InitialTime);}
		if (treeInterfaceFiles.Items[i].Values[colNewStartDate.ItemIndex] = Null) then
			treeInterfaceFiles.Items[i].Values[colNewStartDate.ItemIndex] :=
      DateOf(AInterfaceFile.InitialTime);
		if (treeInterfaceFiles.Items[i].Values[colNewStartTime.ItemIndex] = Null) then
			treeInterfaceFiles.Items[i].Values[colNewStartTime.ItemIndex] :=
      TimeOf(AInterfaceFile.InitialTime);
	end;
end;

procedure TfrmMultiCombineInterfaceFiles.treeInterfaceFilesMouseUp(
	Sender: TObject; Button: TMouseButton; Shift: TShiftState; X,
	Y: Integer);
{var
	AInterfaceFile: IFlowStream;
	AFormat: String;
	AFile: String;
	ANode: TcxTreeListNode;
	NodeIndex: Integer;
	i: Integer;}
begin
{	inherited;
	if btnLockInterfaceFiles.Caption <> 'Unlock Interface Files' then
		Exit;
	ANode := treeInterfaceFiles.GetNodeAt(X, Y);
	if not Assigned(ANode) then
		Exit;

	NodeIndex := ANode.Index;
	treeNodes.ClearNodes;
	for i := 0 to Length(InterfaceTransferFileRecs[NodeIndex].TransferRecs)-1 do
	begin
		ANode := treeNodes.Add;
		ANode.Values[treeNodes.ColumnByName('colIncludeNode').Index] :=
			InterfaceTransferFileRecs[NodeIndex].TransferRecs[i].Include;
		ANode.Values[treeNodes.ColumnByName('colOriginalID').Index] :=
			InterfaceTransferFileRecs[NodeIndex].TransferRecs[i].OriginalID;
		ANode.Values[treeNodes.ColumnByName('colNewID').Index] :=
			InterfaceTransferFileRecs[NodeIndex].TransferRecs[i].NewID;
	end;}
end;

procedure TfrmMultiCombineInterfaceFiles.btnLockInterfaceFilesClick(
	Sender: TObject);
begin
	if btnLockInterfaceFiles.Caption = 'Lock Interface Files' then
	begin
		btnLockInterfaceFiles.Caption := 'Unlock Interface Files';
		EnableInterfaceFileUI(False);
		ReadTransferRecords;
	end
	else
	begin
		btnLockInterfaceFiles.Caption := 'Lock Interface Files';
		EnableInterfaceFileUI(True);
	end;
end;

procedure TfrmMultiCombineInterfaceFiles.btnAutoGenerateTransfersClick(Sender: TObject);
var
	i: Integer;
	AFile: String;
	AFormat: String;
	ANode: TcxTreeListNode;
begin
	frmMain.dlgSelectDir.SelectedPathName := frmMain.InitRegistryDir;
	frmMain.dlgSelectDir.Title := 'Open Receiving EMGAATS Model';

	if frmMain.dlgSelectDir.Execute then
	begin
		frmMain.Update;
		Update;
		ConnectInterfaceFilesToModel(frmMain.dlgSelectDir.SelectedPathName);
	end;
	lstInterfaceFilesClick(Sender);
  AutoGenerated := True;
end;

procedure TfrmMultiCombineInterfaceFiles.ReadTransferRecords;
var
ItemExtractEndTimeText: string;

ItemExtractEndDateText: string;

ItemEndTime: System.Variant;

	i: Integer;
	j: Integer;
	AFile: String;
	AFormat: String;
	ANode: TcxTreeListNode;
  ItemStartDate: Variant;
  ItemStartTime: Variant;
  ItemExtractStartDateText: String;
  ItemExtractStartTimeText: String;
  ItemEndDate: Variant;
begin
	SetLength(InterfaceTransferFileRecs, 0);
	Finalize(InterfaceTransferFileRecs);
	for i := 0 to treeInterfaceFiles.Count-1 do
	begin
		AFile := GetInterfaceFileName(i);
		AFormat := GetInterfaceFileFormat(i);
		ANode := treeInterfaceFiles.Items[i];

		if ANode.Values[colFileStart.ItemIndex] = 'Click Resolve Start Times' then
			ResolveStartTime(i);

		SetLength(InterfaceTransferFileRecs, i+1);
		with InterfaceTransferFileRecs[i] do
		begin
			FileName := AFile;
			FileFormat := AFormat;
			if not TryStrToDateTime(treeInterfaceFiles.Items[i].Values[
				colFileStart.ItemIndex], FileActualStartTime) then
			begin
				ShowMessage(Format('%s has an invalid start time of %s',
					[FileName, treeInterfaceFiles.Items[i].Texts[colFileStart.ItemIndex]]));
			end;

			// Set File Extract Times (with respect to actual time)
      ItemStartDate := treeInterfaceFiles.Items[i].Values[colExtractStartDate.ItemIndex];
      ItemStartTime := treeInterfaceFiles.Items[i].Values[colExtractStartTime.ItemIndex];
      ItemExtractStartDateText := treeInterfaceFiles.Items[i].Texts[colExtractStartDate.ItemIndex];
      ItemExtractStartTimeText := treeInterfaceFiles.Items[i].Texts[colExtractStartTime.ItemIndex];
			if ItemStartDate <> Null then
			begin
				if ItemStartTime <> Null then
					FileExtractStartTime := StrToDateTime(ItemExtractStartDateText) +
						StrToTime(ItemExtractStartTimeText)
				else
					FileExtractStartTime := StrToDateTime(ItemExtractStartDateText)
			end
			else
				FileExtractStartTime := 0;

      ItemEndDate := treeInterfaceFiles.Items[i].Values[colExtractEndDate.ItemIndex];
      ItemEndTime := treeInterfaceFiles.Items[i].Values[colExtractEndTime.ItemIndex];
      ItemExtractEndDateText := treeInterfaceFiles.Items[i].Texts[colExtractEndDate.ItemIndex];
      ItemExtractEndTimeText := treeInterfaceFiles.Items[i].Texts[colExtractEndTime.ItemIndex];
			if ItemEndDate <> Null then
			begin

        if ItemEndTime <> Null then
					FileExtractEndTime := StrToDateTime(ItemExtractEndDateText) +
						StrToTime(ItemExtractEndTimeText)
				else
					FileExtractEndTime := StrToDateTime(ItemExtractEndDateText)
			end
			else
				FileExtractEndTime := MaxDateTime;

			FileRelativeStartTime := GetInterfaceFileRelativeStartDate(i)+
				GetInterfaceFileRelativeStartTime(i);
			Stream := CreateFlowStream(AFile, AFormat);

			SetLength(TransferRecs, Stream.NumFlows);
			for j := 0 to Stream.NumFlows-1 do
			begin
				TransferRecs[j].Include := True;
				TransferRecs[j].OriginalID := Trim(Stream.ID[j]);
				TransferRecs[j].NewID := '';
			end;
		end;
	end;

	edtInterfaceFileStartDate.Date := DateOf(GetEarliestRelativeTime);
	edtInterfaceFileStartTime.Time := TimeOf(GetEarliestRelativeTime);
end;

function TfrmMultiCombineInterfaceFiles.GetInterfaceFileFormat(
	AIndex: Integer): String;
var
	ANode: TcxTreeListNode;
begin
	ANode := treeInterfaceFiles.Items[AIndex];
	Result := ANode.Values[colFormat.ItemIndex];
end;

function TfrmMultiCombineInterfaceFiles.GetInterfaceFileName(
	AIndex: Integer): String;
var
	ANode: TcxTreeListNode;
begin
	ANode := treeInterfaceFiles.Items[AIndex];
	Result := IncludeTrailingPathDelimiter(InterfaceFiles[ANode.Index].Location)+
		InterfaceFiles[ANode.Index].FileName;
end;

procedure TfrmMultiCombineInterfaceFiles.EnableInterfaceFileUI(
	IsEnabled: Boolean);
begin
	btnAddInterfaceFile.Enabled := IsEnabled;
	btnRemoveInterfaceFile.Enabled := IsEnabled;
	btnResolveStartTimes.Enabled := IsEnabled;
	if IsEnabled then
	begin
		treeInterfaceFiles.OptionsSelection.CellSelect := True;
		treeInterfaceFiles.Styles.Background.Color := clWindow;
		treeNodes.Enabled := False;
	end
	else
	begin
		treeInterfaceFiles.OptionsSelection.CellSelect := False;
		treeInterfaceFiles.Styles.Background.Color := clInactiveBorder;
		treeNodes.Enabled := True;
	end;
end;

function TfrmMultiCombineInterfaceFiles.FindPreviewNodeByID(
	AID: String): TcxTreeListNode;
var
	i: Integer;
	PreviewNewIDIdx: Integer;
begin
	Result := nil;
	for i := 0 to treePreviewNodes.Count-1 do
	begin
		PreviewNewIDIdx := cxColPreviewNewID.ItemIndex;
		if treePreviewNodes.Items[i].Values[PreviewNewIDIdx] = AID then
		begin
			Result := treePreviewNodes.Items[i];
			Break;
		end;
	end;
end;

procedure TfrmMultiCombineInterfaceFiles.edtSaveFileButtonClick(
	Sender: TObject);
begin
	frmMain.dlgSave.InitialDir := frmMain.InitRegistryDir;
	frmMain.dlgSave.Title := 'Save Interface File';
{	frmMain.dlgSave.Filter := 'Interface Files (*.int,*.r05,*.r15,*.r20,*.r60,*.t05,*.t15,*.t20,*.t60,*.x05,*.x15,*.x20,*.x60)'+
		'|*.int;*.r05;*.r15;*.r20;*.r60;*.t05;*.t15;*.t20;*.t60;*.x05;*.x15;*.x20;*.x60'+
		'|SYF Files (*.syf)|*.syf|All Files|*.*';}
	frmMain.dlgSave.Filter := 'Interface Files (*.int)'+
		'|*.int|SYF Files (*.syf)|*.syf|All Files|*.*';
	frmMain.dlgSave.DefaultExt := 'int';
	if frmMain.dlgSave.Execute then
	begin
		edtSaveFile.Text := frmMain.dlgSave.FileName;
	end;
end;

function TfrmMultiCombineInterfaceFiles.AllFilesEOF: Boolean;
var
	i: Integer;
begin
	Result := True;
	for i := 0 to Length(InterfaceTransferFileRecs)-1 do
		if not InterfaceTransferFileRecs[i].Stream.EOF then
		begin
			Result := False;
			Break;
		end;
end;

function TfrmMultiCombineInterfaceFiles.GetEarliestNextTime: TDateTime;
var
	i: Integer;
begin
	Result := MaxDateTime;
	for i := 0 to Length(InterfaceTransferFileRecs)-1 do
		if CompareDateTime(InterfaceTransferFileRecs[i].Stream.NextTime, Result) = LessThanValue then
			Result := InterfaceTransferFileRecs[i].Stream.NextTime;
end;

function TfrmMultiCombineInterfaceFiles.GetEarliestCurrentTime: TDateTime;
var
	i: Integer;
begin
	Result := MaxDateTime;
	for i := 0 to Length(InterfaceTransferFileRecs)-1 do
		if CompareDateTime(InterfaceTransferFileRecs[i].Stream.CurrentTime, Result) = LessThanValue then
			Result := InterfaceTransferFileRecs[i].Stream.CurrentTime;
end;

function TfrmMultiCombineInterfaceFiles.GetEarliestInitialTime: TDateTime;
var
	i: Integer;
begin
	Result := MaxDateTime;
	for i := 0 to Length(InterfaceTransferFileRecs)-1 do
		if CompareDateTime(InterfaceTransferFileRecs[i].Stream.InitialTime, Result) = LessThanValue then
			Result := InterfaceTransferFileRecs[i].Stream.InitialTime;
end;

function TfrmMultiCombineInterfaceFiles.GetEarliestAvailableTime(
	FromTime: TDateTime): TDateTime;
var
	i: Integer;
	EarliestTime: TDateTime;
	CompareTime: TDateTime;
	FileCurrentTime: TDateTime;
	FileCurrentEndTime: TDateTime;
	FileNextTime: TDateTime;
begin
	EarliestTime := MaxDateTime;
	for i := 0 to Length(InterfaceTransferFileRecs)-1 do
	begin
		while (CompareDateTime(InterfaceTransferFileRecs[i].Stream.NextTime,
			InterfaceTransferFileRecs[i].Stream.CurrentTime) = EqualsValue) do
		begin
			InterfaceTransferFileRecs[i].Stream.ReadFlows;
		end;

		CompareTime := MaxDateTime;

		if CompareDateTime(MaxDateTime, InterfaceTransferFileRecs[i].Stream.CurrentTime) <> EqualsValue then
		begin
			FileCurrentTime := (InterfaceTransferFileRecs[i].Stream.CurrentTime -
				InterfaceTransferFileRecs[i].Stream.InitialTime) +
				InterfaceTransferFileRecs[i].FileRelativeStartTime;
//			CodeSite.SendDateTime(csmOrange, 'Set FileCurrentTime=', FileCurrentTime);
		end
		else
		begin
			FileCurrentTime := MaxDateTime;
		end;

//		CodeSite.SendFmtMsg(csmBlue,'FileCurrentTime-FromTime=%.3e', [Double(FileCurrentTime)-Double(FromTime)]);
		if (CompareDateTime(FromTime, FileCurrentTime) = LessThanValue) then
		begin
//			CodeSite.SendMsg('Set CompareTime to FileCurrentTime');
			CompareTime := FileCurrentTime;
		end;


		// Only figure out the next two comparisons if we're not at the end of file (NextTime = MaxDateTime)
		if CompareDateTime(MaxDateTime, InterfaceTransferFileRecs[i].Stream.NextTime) <> EqualsValue then
		begin
			FileNextTime := (InterfaceTransferFileRecs[i].Stream.NextTime -
				InterfaceTransferFileRecs[i].Stream.InitialTime) +
				InterfaceTransferFileRecs[i].FileRelativeStartTime;
//			CodeSite.SendDateTime(csmOrange, 'Set FileNextTime=', FileNextTime);
		end
		else
		begin
			FileNextTime := MaxDateTime;
		end;
		if (CompareDateTime(MaxDateTime, FileNextTime) <> EqualsValue) and
			(CompareDateTime(MaxDateTime, FileNextTime) <> LessThanValue) then
		begin
			FileCurrentEndTime := (InterfaceTransferFileRecs[i].Stream.CurrentEndTime -
				InterfaceTransferFileRecs[i].Stream.InitialTime) +
				InterfaceTransferFileRecs[i].FileRelativeStartTime;
//			CodeSite.SendDateTime(csmOrange, 'Set FileCurrentEndTime=', FileCurrentEndTime);
			if CompareDateTime(FromTime, FileCurrentEndTime) = LessThanValue then
				if CompareDateTime(CompareTime, FileCurrentEndTime) = GreaterThanValue then
				begin
//					CodeSite.SendMsg('Set CompareTime to FileCurrentEndTime');
					CompareTime := FileCurrentEndTime;
				end;
			if CompareDateTime(FromTime, FileNextTime) = LessThanValue then
				if CompareDateTime(CompareTime, FileNextTime) = GreaterThanValue then
				begin
//					CodeSite.SendMsg('Set CompareTime to FileNextTime');
					CompareTime := FileNextTime;
				end;
		end;

		if (CompareDateTime(EarliestTime, CompareTime) = GreaterThanValue) then
			EarliestTime := CompareTime;
//		CodeSite.SendDateTime(csmYellow, 'From Time=', FromTime);
//		CodeSite.SendDateTime(csmYellow, 'FileCurrent Time=', FileCurrentTime);
//		CodeSite.SendDateTime(csmYellow, 'FileCurrentEnd Time=', FileCurrentEndTime);
//		CodeSite.SendDateTime(csmYellow, 'FileNext Time=', FileNextTime);
//		CodeSite.SendDateTime(csmYellow, 'Compare Time=', CompareTime);
	end;
//	CodeSite.SendDateTime(csmViolet, 'Earliest Time=', EarliestTime);
	Result := EarliestTime;
end;

procedure TfrmMultiCombineInterfaceFiles.actPreviousExecute(
	Sender: TObject);
begin
	pgcMultiCombineWizard.ActivePageIndex := Max(0, pgcMultiCombineWizard.ActivePageIndex-1);
  LeavePage(lpPrevious);
end;

procedure TfrmMultiCombineInterfaceFiles.actPreviousUpdate(
	Sender: TObject);
begin
	actPrevious.Enabled := pgcMultiCombineWizard.ActivePageIndex > 0;
end;

procedure TfrmMultiCombineInterfaceFiles.actNextExecute(Sender: TObject);
begin
	pgcMultiCombineWizard.ActivePageIndex := Min(pgcMultiCombineWizard.PageCount-1,
		pgcMultiCombineWizard.ActivePageIndex+1);
  LeavePage(lpNext);
end;

procedure TfrmMultiCombineInterfaceFiles.actNextUpdate(Sender: TObject);
begin
	if (pgcMultiCombineWizard.ActivePage = shtSpecifyFiles) then
		actNext.Enabled := btnLockInterfaceFiles.Caption = 'Unlock Interface Files'
	else
		actNext.Enabled := pgcMultiCombineWizard.ActivePageIndex <
			pgcMultiCombineWizard.PageCount-1;
end;

procedure TfrmMultiCombineInterfaceFiles.actProcessExecute(
	Sender: TObject);
var
  IsWithinExtractPeriod: boolean;
	NewInterfaceFile: ISWMMStandardInterfaceFile;
	InterfaceIndex: Integer;
	FlowIndex: Integer;
	NodeList: TStringList;
	i: Integer;
	j: Integer;
	CurrentTime: TDateTime;
	CurrentTimeStep: Double;
	NextTime: TDateTime;
	ExtractEndTime: TDateTime;
	CombinedNode: TcxTreeListNode;
	ANode: TcxTreeListNode;
	ANodeCurrentTime: TDateTime;
	ANodeCurrentEndTime: TDateTime;
	SumFlows: Single;
	FileCurrentTime: TDateTime;
	FileCurrentEndTime: TDateTime;
	FileNextTime: TDateTime;
	EndTime: TDateTime;
	WriteStartTime: TDateTime;
	WriteCurrentTime: TDateTime;
	InitialInterfaceTime: TDateTime;
	PreviousTime: TDateTime;
	InterfaceVolumes: array of Double;
	SumInterfaceVolumes: Double;
	TotalNewVolume: Double;
	ACheckGridNode: TcxTreeListNode;
	icolFileName: Integer;
	icolNumNodes: Integer;
	icolTotalVolume: Integer;
	NumNodes: Integer;
begin
	treeCheck.Clear;
	case rgrpFormat.ItemIndex of
		0: NewInterfaceFile := TSWMMStandardInterfaceFile.Create(edtSaveFile.Text, fmCreate, if32);
		1: NewInterfaceFile := TSWMMStandardInterfaceFile.Create(edtSaveFile.Text, fmCreate, if95);
		2: NewInterfaceFile := TSWMMStandardInterfaceFile.Create(edtSaveFile.Text, fmCreate, ifXP);
		3: begin
				ShowMessage('Text file output not yet implemented');
				Exit;
			end;
	end;

//	pgcNodes.ActivePage := shtPreviewInterfaceFile;
//	PreparePreviewInterfaceFile;

	NodeList := TStringList.Create;
	for i := 0 to treePreviewNodes.Count-1 do
		NodeList.Add(treePreviewNodes.Items[i].Values[cxColPreviewNewID.ItemIndex]);

	NewInterfaceFile.Titles[0] := '';
	NewInterfaceFile.Titles[1] := '';
	NewInterfaceFile.Titles[2] := '';
	NewInterfaceFile.Titles[3] := '';
	NewInterfaceFile.SourceBlock := 'PDXMultiCombine';
	NewInterfaceFile.UsesAlphaNumericIDs := True;
	case rgrpFormat.ItemIndex of
		0: NewInterfaceFile.AlphaIDSize := 10;
		1: NewInterfaceFile.AlphaIDSize := 10;
		2: NewInterfaceFile.AlphaIDSize := 16;
		3: ;
	end;
	NewInterfaceFile.FlowMultiplier := 1.000;

	WriteStartTime := edtInterfaceFileStartDate.Date + edtInterfaceFileStartTime.Time;

	// Rifle through interfaces until we're done
	SetLength(InterfaceVolumes, treeInterfaceFiles.Count);
	for i := 0 to treeInterfaceFiles.Count-1 do
	begin
		InterfaceTransferFileRecs[i].Stream.MoveToStartOfFlows;
		InterfaceTransferFileRecs[i].Stream.ReadFlows;
		InterfaceVolumes[i] := 0;
	end;
	CurrentTime := GetEarliestAvailableTime(0);
	InitialInterfaceTime := CurrentTime;
	NextTime := GetEarliestAvailableTime(CurrentTime);
	CurrentTimeStep := RoundTo((NextTime - CurrentTime) * 86400,-3);

	NewInterfaceFile.Start := WriteStartTime + (CurrentTime - InitialInterfaceTime);

	NewInterfaceFile.WriteHeaderLimitNodes(NodeList);

	ExtractEndTime := GetLatestAvailableTime;

	prgCombine.PartsComplete := 0;

	// Main write loop
//  CodeSite.SendDateTime('At beginning of loop, CurrentTime', CurrentTime);
//  CodeSite.SendDateTime('At beginning of loop, ExtractEndTime', ExtractEndTime);
	while (CompareDateTime(CurrentTime, ExtractEndTime) = LessThanValue) do
	begin

		WriteCurrentTime := WriteStartTime + (CurrentTime - InitialInterfaceTime);
//		CodeSite.SendDateTime('WriteCurrentTime', WriteCurrentTime);

		NewInterfaceFile.WriteInteger(Y2KJulDateOfDateTime(WriteCurrentTime));
		NewInterfaceFile.WriteSingle(SecondsOfDayOfDateTime(WriteCurrentTime));
		NewInterfaceFile.WriteSingle(CurrentTimeStep);

		// Write out nodes
		for i := 0 to treePreviewNodes.Count-1 do
		begin
			CombinedNode := treePreviewNodes.Items[i];
			SumFlows := 0;
			for j := 0 to CombinedNode.Count-1 do
			begin
				ANode := CombinedNode.Items[j];
				InterfaceIndex := ANode.Values[cxColInterfaceIndex.ItemIndex];
				FlowIndex := ANode.Values[cxColInterfaceFlowIndex.ItemIndex];
				ANodeCurrentTime := (InterfaceTransferFileRecs[InterfaceIndex].Stream.CurrentTime -
					InterfaceTransferFileRecs[InterfaceIndex].Stream.InitialTime) +
					InterfaceTransferFileRecs[InterfaceIndex].FileRelativeStartTime;
				ANodeCurrentEndTime := (InterfaceTransferFileRecs[InterfaceIndex].Stream.CurrentEndTime -
					InterfaceTransferFileRecs[InterfaceIndex].Stream.InitialTime) +
					InterfaceTransferFileRecs[InterfaceIndex].FileRelativeStartTime;
				with InterfaceTransferFileRecs[InterfaceIndex] do
				IsWithinExtractPeriod :=
					((CompareDateTime(Stream.CurrentTime, FileExtractEndTime) = LessThanValue) or
						(CompareDateTime(Stream.CurrentTime, FileExtractEndTime) = EqualsValue))
					and
					((CompareDateTime(Stream.CurrentTime, FileExtractStartTime) = GreaterThanValue) or
						(CompareDateTime(Stream.CurrentTime, FileExtractStartTime) = EqualsValue));
				if ((CompareDateTime(CurrentTime, ANodeCurrentTime) = GreaterThanValue) or
					(CompareDateTime(CurrentTime, ANodeCurrentTime) = EqualsValue)) and
					((CompareDateTime(CurrentTime, ANodeCurrentEndTime) = LessThanValue) {or
					(CompareDateTime(CurrentTime, ANodeCurrentEndTime) = EqualsValue)}) and
					IsWithinExtractPeriod then
				begin
{					CodeSite.SendInteger('j', j);
					CodeSite.SendDateTime('CurrentTime', WriteCurrentTime);
					CodeSite.SendDateTime('NodeCurrentTime', InterfaceTransferFileRecs[InterfaceIndex].Stream.CurrentTime);
					CodeSite.SendDateTime('NodeCurrentEndTime', InterfaceTransferFileRecs[InterfaceIndex].Stream.CurrentEndTime);
					CodeSite.SendFmtMsg('NodeFlow = %.4f', [InterfaceTransferFileRecs[InterfaceIndex].Stream.Flow[FlowIndex]]);}
					SumFlows := SumFlows + InterfaceTransferFileRecs[InterfaceIndex].Stream.Flow[FlowIndex];
					InterfaceVolumes[InterfaceIndex] := InterfaceVolumes[InterfaceIndex] +
						InterfaceTransferFileRecs[InterfaceIndex].Stream.Flow[FlowIndex]*CurrentTimeStep;
				end;
			end;
			NewInterfaceFile.WriteSingle(SumFlows);
			TotalNewVolume := TotalNewVolume + SumFlows * CurrentTimeStep;
		end;
		NewInterfaceFile.FlushRecord;

//		CodeSite.SendDateTime('', CurrentTime);
//		CodeSite.SendFloat('CurrentTimeStep', CurrentTimeStep);
		for i := 0 to Length(InterfaceTransferFileRecs)-1 do
		begin
//			CodeSite.SendFmtMsg('%s: %.2f',
//				[InterfaceTransferFileRecs[i].FileName, InterfaceVolumes[i]]);
		end;
		PreviousTime := CurrentTime;
//    CodeSite.SendDateTime('NextTime', NextTime);
		CurrentTime := NextTime;

		// Jump out at ultimate end of files
		if CompareDate(CurrentTime, MaxDateTime) = EqualsValue then
			Break;

		// Advance in time
		for i := 0 to Length(InterfaceTransferFileRecs)-1 do
		begin
			FileCurrentTime := (InterfaceTransferFileRecs[i].Stream.CurrentTime -
				InterfaceTransferFileRecs[i].Stream.InitialTime) +
				InterfaceTransferFileRecs[i].FileRelativeStartTime;
			FileCurrentEndTime := (InterfaceTransferFileRecs[i].Stream.CurrentEndTime -
				InterfaceTransferFileRecs[i].Stream.InitialTime) +
				InterfaceTransferFileRecs[i].FileRelativeStartTime;
			FileNextTime := (InterfaceTransferFileRecs[i].Stream.NextTime -
				InterfaceTransferFileRecs[i].Stream.InitialTime) +
				InterfaceTransferFileRecs[i].FileRelativeStartTime;

			if (CompareDateTime(CurrentTime, FileNextTime) = EqualsValue) then
			begin
				if (CompareDateTime(FileNextTime, MaxDateTime) = EqualsValue) then
					InterfaceTransferFileRecs[i].Stream.FlushFlows
				else
				begin
					InterfaceTransferFileRecs[i].Stream.ReadFlows;
					//CodeSite.SendDateTime('After ReadFlows CurrentTime', InterfaceTransferFileRecs[i].Stream.CurrentTime);
				end;
			end;
		end;

		NextTime := GetEarliestAvailableTime(CurrentTime);
//		CodeSite.SendDateTime('NextTime', NextTime);

		if (CompareDateTime(NextTime, ExtractEndTime) = EqualsValue) or
			(CompareDateTime(NextTime, ExtractEndTime) = GreaterThanValue) then // last time step
		begin
//			CodeSite.SendMsg(csmBlue, 'On last time step');
			EndTime := 0;
			for i := 0 to Length(InterfaceTransferFileRecs)-1 do
			begin
				FileCurrentEndTime := (InterfaceTransferFileRecs[i].Stream.CurrentEndTime -
					InterfaceTransferFileRecs[i].Stream.InitialTime) +
					InterfaceTransferFileRecs[i].FileRelativeStartTime;
				EndTime := Max(EndTime,FileCurrentEndTime);
			end;
			CurrentTimeStep := RoundTo((EndTime - CurrentTime)*86400,-3);
			//CodeSite.SendDateTime('Last End Time', EndTime);
			//CodeSite.SendDateTime('Last Current Time', CurrentTime);
			if CompareDateTime(EndTime, CurrentTime) = EqualsValue then
				CurrentTimeStep := RoundTo((CurrentTime - PreviousTime)*86400,-3);
			//CodeSite.SendFloat('CurrentTimeStep', CurrentTimeStep);
		end
		else
		begin
			CurrentTimeStep := RoundTo((NextTime - CurrentTime) * 86400,-3);
			//CodeSite.SendFloat('CurrentTimeStep', CurrentTimeStep);
		end;
		lblCurrentDate.Caption := FormatDateTime('m/d/yyyy h:mm', WriteCurrentTime);
		lblCurrentDate.Update;
		prgCombine.PartsComplete := Round((CurrentTime-InitialInterfaceTime)/
			(ExtractEndTime-InitialInterfaceTime)*100);
		prgCombine.Update;
		Application.ProcessMessages;
	end;

	// Write to check grid
	icolFileName := cxColFileName.ItemIndex;
	icolNumNodes := cxColNumNodes.ItemIndex;
	icolTotalVolume := cxColTotalVolume.ItemIndex;
	SumInterfaceVolumes := 0;
	for i := 0 to treeInterfaceFiles.Count-1 do
	begin
		ACheckGridNode := treeCheck.Add;
		ACheckGridNode.Values[icolFileName] := ExtractFileName(InterfaceFiles[i].FileName);
		NumNodes := 0;
		for j := 0 to Length(InterfaceTransferFileRecs[i].TransferRecs)-1 do
			if InterfaceTransferFileRecs[i].TransferRecs[j].Include then
				Inc(NumNodes);
		ACheckGridNode.Values[icolNumNodes] := NumNodes;
		ACheckGridNode.Values[icolTotalVolume] := Format('%.2f', [InterfaceVolumes[i]]);
		SumInterfaceVolumes := SumInterfaceVolumes + InterfaceVolumes[i];
	end;
	ACheckGridNode := treeCheck.Add;
	ACheckGridNode.Values[icolFileName] := 'Total';
	ACheckGridNode.Values[icolTotalVolume] := Format('%.2f', [SumInterfaceVolumes]);
	lblTotalNodes.Caption := Format('Total Nodes in Interface File: %d', [treePreviewNodes.Count]);
	lblTotalVolume.Caption := Format('Total Volume in Interface File: %.2f', [TotalNewVolume]);
end;

procedure TfrmMultiCombineInterfaceFiles.actProcessUpdate(Sender: TObject);
begin
	actProcess.Enabled := edtSaveFile.Text <> '';
end;

procedure TfrmMultiCombineInterfaceFiles.actExcludeAllExecute(
	Sender: TObject);
var
	i: Integer;
begin
	inherited;

	for i := 0 to treeNodes.Count-1 do
	begin
		treeNodes.Items[i].Values[
			cxColIncludeNode.ItemIndex] := 'False';
		InterfaceTransferFileRecs[lstInterfaceFiles.ItemIndex].TransferRecs[i].Include := False;
	end;
end;

procedure TfrmMultiCombineInterfaceFiles.actIncludeAllExecute(
	Sender: TObject);
var
	i: Integer;
begin
	inherited;

	for i := 0 to treeNodes.Count-1 do
	begin
		treeNodes.Items[i].Values[
			cxColIncludeNode.ItemIndex] := 'True';
		InterfaceTransferFileRecs[lstInterfaceFiles.ItemIndex].TransferRecs[i].Include := True;
	end;
end;

procedure TfrmMultiCombineInterfaceFiles.actToggleRangeExecute(
	Sender: TObject);
var
	i: Integer;
	IncludeCol: Integer;
begin
	inherited;

  dlgToggleRange.NumItems := treeNodes.Count;
	if dlgToggleRange.ShowModal = mrOK then
	begin
		IncludeCol := cxColIncludeNode.ItemIndex;
		for i :=	dlgToggleRange.edtLower.IntValue to dlgToggleRange.edtUpper.IntValue do
		begin
			if treeNodes.Items[i-1].Values[IncludeCol] = 'True' then
			begin
				treeNodes.Items[i].Values[
					cxColIncludenode.ItemIndex] := 'False';
				InterfaceTransferFileRecs[lstInterfaceFiles.ItemIndex].TransferRecs[i].Include := False;
			end
			else
			begin
				treeNodes.Items[i].Values[
					cxColIncludeNode.ItemIndex] := 'True';
				InterfaceTransferFileRecs[lstInterfaceFiles.ItemIndex].TransferRecs[i].Include := True;
			end;
		end;
  end;

end;

procedure TfrmMultiCombineInterfaceFiles.FormCreate(Sender: TObject);
begin
  inherited;
	pgcMultiCombineWizard.ActivePageIndex := 0;
	with colFormat.Properties as TcxComboBoxProperties do
		DropDownRows := Items.Count;
	ExceptionConversions := TStringList.Create;
  MultipleFlowExclusions := TStringList.Create;
  AutoGenerated := False;
end;

procedure TfrmMultiCombineInterfaceFiles.ConnectInterfaceFilesToModel(
	AModelPath: String);
var
	i: Integer;
	j: Integer;
	ANode: TcxTreeListNode;
	AdxNode: TcxTreeListNode;
	AFormat: String;
	AFile: String;
	NumNodes: Integer;
	StopPipes: TStringList;
  NodesWithConveyance: TStringList;
begin
	Screen.Cursor := crHourglass;
  try
    Application.ProcessMessages;
    adoReceivingLinks.Connected := False;
    adoReceivingLinks.ConnectionString := 'Provider=Microsoft.Jet.OLEDB.4.0;'+
      'Data Source='+AModelPath+'\links\mdl_Links_ac.mdb;'+
      'Persist Security Info=False';
    try
      adoReceivingLinks.Connected := True;
    except
      on EOleException do
      begin
        MessageDlg('Could not open links\mdl_links_ac.mdb database.  Check to '+
          'make sure the directory you selected is a valid EMGAATS  '+
          'model.', mtError, [mbOK], 0);
        Exit;
      end;
    end;

    adoqUpstreamLinks.Close;
    lblAutoGenerateStatus.Caption := 'Gathering all upstream links of receiving model';
    adoqUpstreamLinks.Open;
    adoqUpstreamNodes.Close;
    lblAutoGenerateStatus.Caption := 'Gathering all upstream nodes of receiving model';
    adoqUpstreamNodes.Open;

    NumNodes := 0;
    for i := 0 to treeInterfaceFiles.Count-1 do
    begin
      Inc(NumNodes, Length(InterfaceTransferFileRecs[i].TransferRecs))
    end;
    prgAutoGenerateTransfers.TotalParts := NumNodes;
    prgAutoGenerateTransfers.PartsComplete := 0;

    // Tally up all the stop pipes
    ModelIni := TMemIniFile.Create(AModelPath+'\model.ini');
    StopPipes := TStringList.Create;
    ModelIni.ReadSection('stoplinks', StopPipes);

    NodesWithConveyance := TStringList.Create;
    NodesWithConveyance.Sorted := True;
    NodesWithConveyance.Duplicates := dupIgnore;

    for i := 0 to treeInterfaceFiles.Count-1 do
    begin
      ANode := treeInterfaceFiles.Items[i];
      if ANode.Values[colExcludeFromAutoGen.ItemIndex] = 'True' then // skip out on excluded autogen files
        Continue;
      AFormat := GetInterfaceFileFormat(ANode.Index);
      AFile := GetInterfaceFileName(ANode.Index);
      for j := 0 to Length(InterfaceTransferFileRecs[i].TransferRecs)-1 do
      begin
        // If we're processing a conveyance file, then we need to look up the
        // PIPE ID in the receiving model's link table SimLinkID field and assign
        // that flow set to the upstream node
        if (InterfaceTransferFileRecs[i].FileFormat = 'XP SYF Conveyance') or
          (InterfaceTransferFileRecs[i].FileFormat = 'PDX-95 SYF Conveyance') then
        begin
          if adoqUpstreamLinks.Locate('SimLinkID',
            InterfaceTransferFileRecs[i].TransferRecs[j].OriginalID,
            [loCaseInsensitive]) then
          begin
            InterfaceTransferFileRecs[i].TransferRecs[j].Include := True;
            InterfaceTransferFileRecs[i].TransferRecs[j].NewID := adoqUpstreamLinksUSNode.Value;
            NodesWithConveyance.Add(adoqUpstreamLinksUSNode.Value);
            if StopPipes.IndexOf(adoqUpstreamLinksMLinkID.AsString) > -1 then
              StopPipes.Delete(StopPipes.IndexOf(adoqUpstreamLinksMLinkID.AsString));
          end
          else
          begin
            InterfaceTransferFileRecs[i].TransferRecs[j].Include := False;
            InterfaceTransferFileRecs[i].TransferRecs[j].NewID := '';
          end;
        end
        // If we're processing a runoff file, then we need to look up the
        // NODE ID in the receiving model's nodes set, which is composed
        // of all nodes that are at least one node down from the most upstream nodes
        // and downstream; we assume all upstream nodes are taken care of by
        // the source conveyance models...this will be a problem in receiving models
        // which are unintentionally not completely covered by source models already
        // AMM 5/31/2006 see below for additional code that allows runoff model
        // to cover most upstream nodes if a conveyance model isn't available to
        // provide flow
        else if (InterfaceTransferFileRecs[i].FileFormat = 'PDX-32 Runoff') or
          (InterfaceTransferFileRecs[i].FileFormat = 'PDX-95 Runoff') then
        begin
          if adoqUpstreamNodes.Locate('USNode',
            LeftStr(Trim(InterfaceTransferFileRecs[i].TransferRecs[j].OriginalID),PDXNodeIDSize), // PDX-specific 6 char node IDs
            [loCaseInsensitive]) then
          begin
            CodeSite.SendFmtMsg('Normal Runoff, turned off: %s->%s',
              [InterfaceTransferFileRecs[i].TransferRecs[j].OriginalID,
              InterfaceTransferFileRecs[i].TransferRecs[j].NewID]);
            InterfaceTransferFileRecs[i].TransferRecs[j].Include := False // assume conveyance models cover the upstream inflows
          end
          else
            InterfaceTransferFileRecs[i].TransferRecs[j].Include := True;
        end
        else
        begin
          //CodeSite.SendMsg(InterfaceTransferFileRecs[i].TransferRecs[j].OriginalID);
          if adoqUpstreamNodes.Locate('USNode',
            Trim(LeftStr(InterfaceTransferFileRecs[i].TransferRecs[j].OriginalID,PDXNodeIDSize)),
            [loCaseInsensitive]) then
            InterfaceTransferFileRecs[i].TransferRecs[j].Include := True
          else
            InterfaceTransferFileRecs[i].TransferRecs[j].Include := False;
        end;
        prgAutoGenerateTransfers.PartsComplete := prgAutoGenerateTransfers.PartsComplete + 1;
        Application.ProcessMessages;
      end;
    end;

    // 5/31/2006 Added option to assign runoff interface node to most-upstream
    // nodes that didn't receive any flow from a conveyance file
    for i := 0 to treeInterfaceFiles.Count-1 do
    begin
      if not ((InterfaceTransferFileRecs[i].FileFormat = 'PDX-32 Runoff') or
        (InterfaceTransferFileRecs[i].FileFormat = 'PDX-95 Runoff')) then
        Continue;

      for j := 0 to Length(InterfaceTransferFileRecs[i].TransferRecs)-1 do
      begin
        if (NodesWithConveyance.IndexOf(
          Trim(InterfaceTransferFileRecs[i].TransferRecs[j].OriginalID)) = -1) and
          adoqUpstreamNodes.Locate('USNode',
          LeftStr(Trim(InterfaceTransferFileRecs[i].TransferRecs[j].OriginalID),PDXNodeIDSize), // PDX-specific 6 char node IDs
          [loCaseInsensitive]) then
          begin
            CodeSite.SendFmtMsg('Runoff with no conveyance file, turned on: %s->%s',
              [InterfaceTransferFileRecs[i].TransferRecs[j].OriginalID,
              InterfaceTransferFileRecs[i].TransferRecs[j].NewID]);
            InterfaceTransferFileRecs[i].TransferRecs[j].Include := True;
          end;
      end;
    end;

    lblAutoGenerateStatus.Caption := '';
    prgAutoGenerateTransfers.PartsComplete := 0;

    adoLinks.Open;
    treeExceptionalNodes.Clear;
    for i := 0 to StopPipes.Count-1 do
    begin
      AdxNode := treeExceptionalNodes.Add;
      AdxNode.Values[cxColStopPipeNotFound.ItemIndex] := StopPipes[i];
      if adoLinks.Locate('MLinkID', StopPipes[i], [loCaseInsensitive]) then
        AdxNode.Values[cxColStopPipeUSNode.ItemIndex] := adoLinksUSNode.AsString
      else
        AdxNode.Values[cxColStopPipeUSNode.ItemIndex] := 'Stop Pipe not in receiving model';
    end;
    adoLinks.Close;

    for i := 0 to Length(InterfaceFiles)-1 do
      (cxColPipeNotFoundFileAssignment.Properties as TcxComboBoxProperties).Items.Add(ExtractFileName(InterfaceFiles[i].FileName));
    lblNumExceptionalNodes.Caption := Format('%d Exceptions Found', [StopPipes.Count]);
    cxColStopPipeNotFound.SortOrder := soAscending;
    if treeExceptionalNodes.Count > 0 then
      treeExceptionalNodes.Items[0].Focused := True;
  finally
    NodesWithConveyance.Free;
    StopPipes.Free;
    adoqUpstreamLinks.Close;
    adoqUpstreamNodes.Close;
    adoReceivingLinks.Connected := False;
    Screen.Cursor := crDefault;
  end;
end;

procedure TfrmMultiCombineInterfaceFiles.btnSaveConfigurationClick(
	Sender: TObject);
var
	i: Integer;
	j: Integer;
	SaveIni: TMemIniFile;
begin
	frmMain.dlgSave.InitialDir := frmMain.InitRegistryDir;
	frmMain.dlgSave.Title := 'Save Multicombine Configuration File';
	frmMain.dlgSave.Filter := 'MultiCombine Configuration (*.ini)|*.ini|'+
		'All Files|*.*';
	frmMain.dlgSave.DefaultExt := 'ini';
	if frmMain.dlgSave.Execute then
	begin
		SaveIni := TMemIniFile.Create(frmMain.dlgSave.FileName);
		SaveIni.WriteInteger('InterfaceFiles', 'NumFiles', Length(InterfaceTransferFileRecs));
		SaveIni.WriteInteger('MultiCombine', 'StartMonth', MonthOf(edtInterfaceFileStartDate.Date));
		SaveIni.WriteInteger('MultiCombine', 'StartDay', DayOf(edtInterfaceFileStartDate.Date));
		SaveIni.WriteInteger('MultiCombine', 'StartYear', YearOf(edtInterfaceFileStartDate.Date));
		SaveIni.WriteInteger('MultiCombine', 'StartHour', HourOf(edtInterfaceFileStartTime.Time));
		SaveIni.WriteInteger('MultiCombine', 'StartMinute', MinuteOf(edtInterfaceFileStartTime.Time));
		SaveIni.WriteInteger('MultiCombine', 'StartSecond', SecondOf(edtInterfaceFileStartTime.Time));
		for i := 0 to Length(InterfaceTransferFileRecs)-1 do
		begin
			SaveIni.WriteString('InterfaceFile'+IntToStr(i), 'FileName',
				InterfaceTransferFileRecs[i].FileName);
			SaveIni.WriteString('InterfaceFile'+IntToStr(i), 'FileFormat',
				InterfaceTransferFileRecs[i].FileFormat);
			SaveIni.WriteInteger('InterfaceFile'+IntToStr(i), 'FileActualStartMonth',
				MonthOf(InterfaceTransferFileRecs[i].FileActualStartTime));
			SaveIni.WriteInteger('InterfaceFile'+IntToStr(i), 'FileActualStartDay',
				DayOf(InterfaceTransferFileRecs[i].FileActualStartTime));
			SaveIni.WriteInteger('InterfaceFile'+IntToStr(i), 'FileActualStartYear',
				YearOf(InterfaceTransferFileRecs[i].FileActualStartTime));
			SaveIni.WriteInteger('InterfaceFile'+IntToStr(i), 'FileActualStartHour',
				HourOf(InterfaceTransferFileRecs[i].FileActualStartTime));
			SaveIni.WriteInteger('InterfaceFile'+IntToStr(i), 'FileActualStartMinute',
				MinuteOf(InterfaceTransferFileRecs[i].FileActualStartTime));
			SaveIni.WriteInteger('InterfaceFile'+IntToStr(i), 'FileActualStartSecond',
				SecondOf(InterfaceTransferFileRecs[i].FileActualStartTime));
			SaveIni.WriteInteger('InterfaceFile'+IntToStr(i), 'FileRelativeStartMonth',
				MonthOf(InterfaceTransferFileRecs[i].FileRelativeStartTime));
			SaveIni.WriteInteger('InterfaceFile'+IntToStr(i), 'FileRelativeStartDay',
				DayOf(InterfaceTransferFileRecs[i].FileRelativeStartTime));
			SaveIni.WriteInteger('InterfaceFile'+IntToStr(i), 'FileRelativeStartYear',
				YearOf(InterfaceTransferFileRecs[i].FileRelativeStartTime));
			SaveIni.WriteInteger('InterfaceFile'+IntToStr(i), 'FileRelativeStartHour',
				HourOf(InterfaceTransferFileRecs[i].FileRelativeStartTime));
			SaveIni.WriteInteger('InterfaceFile'+IntToStr(i), 'FileRelativeStartMinute',
				MinuteOf(InterfaceTransferFileRecs[i].FileRelativeStartTime));
			SaveIni.WriteInteger('InterfaceFile'+IntToStr(i), 'FileRelativeStartSecond',
				SecondOf(InterfaceTransferFileRecs[i].FileRelativeStartTime));
			SaveIni.WriteInteger('InterfaceFile'+IntToStr(i), 'NumTransferPoints',
				Length(InterfaceTransferFileRecs[i].TransferRecs));
			for j := 0 to Length(InterfaceTransferFileRecs[i].TransferRecs)-1 do
			begin
				SaveIni.WriteString('InterfaceFile'+IntToStr(i), 'OriginalID'+IntToStr(j),
					InterfaceTransferFileRecs[i].TransferRecs[j].OriginalID);
				SaveIni.WriteBool('InterfaceFile'+IntToStr(i), 'Include'+IntToStr(j),
					InterfaceTransferFileRecs[i].TransferRecs[j].Include);
				SaveIni.WriteString('InterfaceFile'+IntToStr(i), 'NewID'+IntToStr(j),
					InterfaceTransferFileRecs[i].TransferRecs[j].NewID);
			end;
		end;
		SaveIni.UpdateFile;
		FreeAndNil(SaveIni);
	end;
end;

function TfrmMultiCombineInterfaceFiles.GetInterfaceFileRelativeStartDate(
	AIndex: Integer): TDate;
var
	ANode: TcxTreeListNode;
begin
	ANode := treeInterfaceFiles.Items[AIndex];
	Result := StrToDate(ANode.Values[colNewStartDate.ItemIndex]);
end;

function TfrmMultiCombineInterfaceFiles.GetInterfaceFileRelativeStartTIme(
	AIndex: Integer): TTime;
var
	ANode: TcxTreeListNode;
begin
	ANode := treeInterfaceFiles.Items[AIndex];
	Result := StrToTime(ANode.Values[colNewStartTime.ItemIndex]);
end;

procedure TfrmMultiCombineInterfaceFiles.ResolveStartTime(AIndex: Integer);
var
	AInterfaceFile: IFlowStream;
	FileFormat: String;
	FileName: String;
begin
	FileFormat := GetInterfaceFileFormat(AIndex);
	FileName := GetInterfaceFileName(AIndex);
	if (FileFormat = 'PDX-32 Runoff') or (FileFormat = 'PDX-32 Conveyance') then
		AInterfaceFile := TSWMMStandardInterfaceFile.Create(FileName, fmOpenRead or fmShareDenyWrite, if32)
	else if (FileFormat = 'PDX-95 Runoff') or (FileFormat = 'PDX-95 Conveyance') then
		AInterfaceFile := TSWMMStandardInterfaceFile.Create(FileName, fmOpenRead or fmShareDenyWrite, if95)
	else if (FileFormat = 'XP Runoff') or (FileFormat = 'XP Conveyance') then
		AInterfaceFile := TSWMMStandardInterfaceFile.Create(FileName, fmOpenRead or fmShareDenyWrite, ifXP)
	else if FileFormat = 'XP SYF Conveyance' then
		AInterfaceFile := TXP_SYF_FileStream.Create(FileName, fmOpenRead or fmShareDenyWrite)
	else if FileFormat = 'PDX-95 Conveyance Text' then
		AInterfaceFile := TPDXTextStream.Create(FileName, fmOpenRead or fmShareDenyWrite)
	else
	begin
		ShowMessage(FileFormat + ' is not implemented');
		treeInterfaceFiles.Items[AIndex].Values[colNewStartTime.ItemIndex] := 'Format not implemented';
		Exit;
	end;

	treeInterfaceFiles.Items[AIndex].Values[colNewStartTime.ItemIndex] :=
		FormatDateTime('m/d/yyyy hh:mm:ss', AInterfaceFile.InitialTime);
	if (treeInterfaceFiles.Items[AIndex].Values[colNewStartDate.ItemIndex] = Null) or
		(Length(treeInterfaceFiles.Items[AIndex].Texts[colNewStartDate.ItemIndex]) = 0) then
		treeInterfaceFiles.Items[AIndex].Values[colNewStartDate.ItemIndex] :=
			FormatDateTime('m/d/yyyy', AInterfaceFile.InitialTime);
	if (treeInterfaceFiles.Items[AIndex].Values[colNewStartTime.ItemIndex] = Null) or
		(Length(treeInterfaceFiles.Items[AIndex].Texts[colNewStartTime.ItemIndex]) = 0) then
		treeInterfaceFiles.Items[AIndex].Values[colNewStartTime.ItemIndex] :=
			FormatDateTime('hh:mm:ss', AInterfaceFile.InitialTime);
end;

procedure TfrmMultiCombineInterfaceFiles.btnLoadConfigurationClick(
	Sender: TObject);
var
	LoadIni: TMemIniFile;
	i: Integer;
	NumFiles: Integer;
	j: Integer;
	NumTransfers: Integer;
	ANode: TcxTreeListNode;
	AFileName: String;
	AFileFormat: String;
	AMonth, ADay, AYear, AHour, AMinute, ASecond: Word;
	AIndex: Integer;
	AOriginalID: String;
	AInclude: Boolean;
	ANewID: String;
begin
	frmMain.dlgOpen.InitialDir := frmMain.InitRegistryDir;
	frmMain.dlgOpen.Title := 'Save Multicombine Configuration File';
	frmMain.dlgOpen.Filter := 'MultiCombine Configuration (*.ini)|*.ini|'+
		'All Files|*.*';
	frmMain.dlgOpen.DefaultExt := 'ini';
	if frmMain.dlgOpen.Execute then
	begin
		EnableInterfaceFileUI(False);
		SetLength(InterfaceTransferFileRecs, 0);
		Finalize(InterfaceTransferFileRecs);
		LoadIni := TMemIniFile.Create(frmMain.dlgOpen.FileName);

		NumFiles := LoadIni.ReadInteger('InterfaceFiles', 'NumFiles', 0);

		AMonth := LoadIni.ReadInteger('MultiCombine', 'StartMonth', 0);
		ADay := LoadIni.ReadInteger('MultiCombine', 'StartDay', 0);
		AYear := LoadIni.ReadInteger('MultiCombine', 'StartYear', 0);
		edtInterfaceFileStartDate.Date := EncodeDate(AYear, AMonth, ADay);

		AHour := LoadIni.ReadInteger('MultiCombine', 'StartHour', 0);
		AMinute := LoadIni.ReadInteger('MultiCombine', 'StartMinute', 0);
		ASecond := LoadIni.ReadInteger('MultiCombine', 'StartSecond', 0);
		edtInterfaceFileStartTime.Time := EncodeTime(AHour, AMinute, ASecond, 0);

		for i := 0 to NumFiles-1 do
		begin
			AFileName := LoadIni.ReadString('InterfaceFile'+IntToStr(i), 'FileName',
				'unknown');
			AFileFormat := LoadIni.ReadString('InterfaceFile'+IntToStr(i), 'FileFormat',
				'unknown');

			AIndex := AddTransferFile(AFileName, AFileFormat);
			ANode := treeInterfaceFiles.Items[AIndex];

			AMonth := LoadIni.ReadInteger('InterfaceFile'+IntToStr(i), 'FileActualStartMonth', 0);
			ADay := LoadIni.ReadInteger('InterfaceFile'+IntToStr(i), 'FileActualStartDay', 0);
			AYear := LoadIni.ReadInteger('InterfaceFile'+IntToStr(i), 'FileActualStartYear', 0);
			AHour := LoadIni.ReadInteger('InterfaceFile'+IntToStr(i), 'FileActualStartHour', 0);
			AMinute := LoadIni.ReadInteger('InterfaceFile'+IntToStr(i), 'FileActualStartMinute', 0);
			ASecond := LoadIni.ReadInteger('InterfaceFile'+IntToStr(i), 'FileActualStartSecond', 0);
			InterfaceTransferFileRecs[AIndex].FileActualStartTime :=
				EncodeDateTime(AYear, AMonth, ADay, AHour, AMinute, ASecond, 0);
			ANode.Values[colNewStartTime.ItemIndex] :=
				FormatDateTime('MM/DD/YYYY HH:MM:SS',
				InterfaceTransferFileRecs[AIndex].FileActualStartTime);

			AMonth := LoadIni.ReadInteger('InterfaceFile'+IntToStr(i), 'FileRelativeStartMonth', 0);
			ADay := LoadIni.ReadInteger('InterfaceFile'+IntToStr(i), 'FileRelativeStartDay', 0);
			AYear := LoadIni.ReadInteger('InterfaceFile'+IntToStr(i), 'FileRelativeStartYear', 0);
			AHour := LoadIni.ReadInteger('InterfaceFile'+IntToStr(i), 'FileRelativeStartHour', 0);
			AMinute := LoadIni.ReadInteger('InterfaceFile'+IntToStr(i), 'FileRelativeStartMinute', 0);
			ASecond := LoadIni.ReadInteger('InterfaceFile'+IntToStr(i), 'FileRelativeStartSecond', 0);
			InterfaceTransferFileRecs[AIndex].FileRelativeStartTime :=
				EncodeDateTime(AYear, AMonth, ADay, AHour, AMinute, ASecond, 0);
			ANode.Values[colNewStartDate.ItemIndex] :=
				FormatDateTime('MM/DD/YYYY', InterfaceTransferFileRecs[AIndex].FileRelativeStartTime);
			ANode.Values[colNewStartTime.ItemIndex] :=
				FormatDateTime('HH:MM:SS', InterfaceTransferFileRecs[AIndex].FileRelativeStartTime);

			NumTransfers := LoadIni.ReadInteger('InterfaceFile'+IntToStr(i), 'NumTransferPoints',0);
			for j := 0 to NumTransfers-1 do
			begin
				AOriginalID := LoadIni.ReadString('InterfaceFile'+IntToStr(i), 'OriginalID'+IntToStr(j),
					'unknown');
				AInclude := LoadIni.ReadBool('InterfaceFile'+IntToStr(i), 'Include'+IntToStr(j),
					False);
				ANewID := LoadIni.ReadString('InterfaceFile'+IntToStr(i), 'NewID'+IntToStr(j),
					'');

				AddTransferPoint(i, AOriginalID, AInclude, ANewID);
			end;
		end;

		FreeAndNil(LoadIni);
	end;
end;

function TfrmMultiCombineInterfaceFiles.CreateFlowStream(AFile,
	AFormat: String): IFlowStream;
begin
	if (AFormat = 'PDX-32 Conveyance') or (AFormat = 'PDX-32 Runoff') then
		Result := TSWMMStandardInterfaceFile.Create(AFile, fmOpenRead or fmShareDenyWrite, if32)
	else if (AFormat = 'PDX-95 Conveyance') or (AFormat = 'PDX-95 Runoff') or (AFormat = 'PDX-95 SYF Conveyance') then
		Result := TSWMMStandardInterfaceFile.Create(AFile, fmOpenRead or fmShareDenyWrite, if95)
	else if (AFormat = 'PDX-95 Conveyance Text') then
		Result := TPDXTextStream.Create(AFile, fmOpenRead or fmShareDenyWrite)
	else if (AFormat = 'XP Conveyance') or (AFormat = 'XP Runoff') then
		Result := TSWMMStandardInterfaceFile.Create(AFile, fmOpenRead or fmShareDenyWrite, ifXP)
	else if AFormat = 'XP SYF Conveyance' then
		Result := TXP_SYF_FileStream.Create(AFile, fmOpenRead or fmShareDenyWrite);
end;

function TfrmMultiCombineInterfaceFiles.AddTransferFile(AFile,
	AFormat: String): Integer;
var
	AIndex: Integer;
	ANode: TcxTreeListNode;
begin
	AIndex := Length(InterfaceTransferFileRecs);
	SetLength(InterfaceTransferFileRecs, AIndex+1);
	InterfaceTransferFileRecs[AIndex].FileName := AFile;
	InterfaceTransferFileRecs[AIndex].FileFormat := AFormat;

	ANode := treeInterfaceFiles.Add;
	ANode.Values[colInterfaceFile.ItemIndex] := ExtractFileName(AFile);
	ANode.Values[colLocation.ItemIndex] := ExtractFilePath(AFile);
	ANode.Values[colFormat.ItemIndex] := AFormat;

	Result := AIndex;
end;

procedure TfrmMultiCombineInterfaceFiles.DeleteTransferFile(
	AIndex: Integer);
begin
	//
end;

function TfrmMultiCombineInterfaceFiles.AddTransferPoint(
	AFileIndex: Integer; AOriginalID: String; AInclude: Boolean;
	ANewID: String): Integer;
var
	AIndex: Integer;
begin
	AIndex := Length(InterfaceTransferFileRecs[AFileIndex].TransferRecs);
	SetLength(InterfaceTransferFileRecs[AFileIndex].TransferRecs, AIndex+1);
	InterfaceTransferFileRecs[AFileIndex].TransferRecs[AIndex].OriginalID := AOriginalID;
	InterfaceTransferFileRecs[AFileIndex].TransferRecs[AIndex].Include := AInclude;
	InterfaceTransferFileRecs[AFileIndex].TransferRecs[AIndex].NewID := ANewID;
	Result := AIndex;
end;

function TfrmMultiCombineInterfaceFiles.GetEarliestRelativeTime: TDateTime;
var
	i: Integer;
	CurrentDateTime: TDateTime;
	EarliestDateTime: TDateTime;
begin
	EarliestDateTime := MaxDateTime;
	for i := 0 to Length(InterfaceTransferFileRecs)-1 do
	begin
		CurrentDateTime := GetInterfaceFileRelativeStartDate(i) +
			GetInterfaceFileRelativeStartTime(i);
		if CurrentDateTime < EarliestDateTime then
			EarliestDateTime := CurrentDateTime;
	end;
	Result := EarliestDateTime;
end;

procedure TfrmMultiCombineInterfaceFiles.PreparePreviewInterfaceFile;
var
	i: Integer;
	j: Integer;
	ANode: TcxTreeListNode;
	NewTransferID: String;
	colInterfaceFileWidth: Integer;
	NumTotalNodes: Integer;
begin
	inherited;
	Screen.Cursor := crHourglass;

	treePreviewNodes.Clear;
	treePreviewNodes.BeginUpdate;
	pnlPreviewAssembly.Show;
	Application.ProcessMessages;

	NumTotalNodes := 0;
	for i := 0 to Length(InterfaceTransferFileRecs)-1 do
		with InterfaceTransferFileRecs[i] do
			Inc(NumTotalNodes, Length(TransferRecs));
	prgPreviewAssembly.TotalParts := NumTotalNodes;

	for i := 0 to Length(InterfaceTransferFileRecs)-1 do
		with InterfaceTransferFileRecs[i] do
			for j := 0 to Length(TransferRecs)-1 do
			begin
				if TransferRecs[j].Include then
				begin
					if TransferRecs[j].NewID <> '' then
						NewTransferID := TransferRecs[j].NewID
					else
						NewTransferID := TransferRecs[j].OriginalID;
					ANode := FindPreviewNodeByID(NewTransferID);
					if not Assigned(ANode) then
					begin
						ANode := treePreviewNodes.Add;
						ANode.Values[cxColPreviewNewID.ItemIndex] :=
							NewTransferID
					end;
					ANode := ANode.AddChild;
					ANode.Values[cxColFromInterfaceFile.ItemIndex] := ExtractFileName(FileName);
					ANode.Values[cxColPreviewOriginalID.ItemIndex] := TransferRecs[j].OriginalID;
					ANode.Values[cxColPreviewNewID.ItemIndex] := NewTransferID;
					ANode.Values[cxColInterfaceIndex.ItemIndex] := i;
					ANode.Values[cxColInterfaceFlowIndex.ItemIndex] := j;
				end;
				prgPreviewAssembly.IncPartsByOne;
			end;
	pnlPreviewAssembly.Hide;
	cxColPreviewNewID.SortOrder := soAscending;
//	treePreviewNodes.RefreshSorting;
	treePreviewNodes.FullExpand;
	treePreviewNodes.EndUpdate;
  treePreviewNodes.Items[0].Focused := True;
	lblTotalCompiledNodes.Caption := Format('%d Nodes to Combine', [treePreviewNodes.Count]);
	Screen.Cursor := crDefault;
end;

procedure TfrmMultiCombineInterfaceFiles.btnRemoveInterfaceFileClick(
  Sender: TObject);
var
  ANode: TcxTreeListNode;
  NewInterfaceFileRecs: TInterfaceFileRecArray;
  i: Integer;
  RemoveNodeIndex: Integer;
begin
  if treeInterfaceFiles.Enabled then
  begin
		ANode := treeInterfaceFiles.FocusedNode;
    if ANode = nil then
      Exit;
    RemoveNodeIndex := ANode.Index;
    SetLength(NewInterfaceFileRecs, Length(InterfaceFiles)-1);
    for i := 0 to Length(InterfaceFiles)-1-1 do
    begin
      if i < RemoveNodeIndex then
        NewInterfaceFileRecs[i] := InterfaceFiles[i]
      else
        NewInterfaceFileRecs[i] := InterfaceFiles[i+1];
    end;
    Finalize(InterfaceFiles);
    InterfaceFiles := NewInterfaceFileRecs;
    treeInterfaceFiles.Items[RemoveNodeIndex].Free;
  end;
end;

procedure TfrmMultiCombineInterfaceFiles.lstInterfaceFilesClick(
	Sender: TObject);
var
	NumNodesSelected: Integer;
	NumCurrentInterfaceNodesSelected: Integer;
	i, j: Integer;
	NodeIndex: Integer;
	ANode: TcxTreeListNode;
begin
	Screen.Cursor := crHourglass;
	NodeIndex := lstInterfaceFiles.ItemIndex;
	treeNodes.Clear;
	if Length(InterfaceTransferFileRecs) = 0 then
		Exit;
	lblCurrentInterfaceFile.Caption := lstInterfaceFiles.Items[lstInterfaceFiles.ItemIndex];
	for i := 0 to Length(InterfaceTransferFileRecs[NodeIndex].TransferRecs)-1 do
	begin
		ANode := treeNodes.Add;
		ANode.Values[cxColRecord.ItemIndex] := i+1;
		ANode.Values[cxColIncludeNode.ItemIndex] :=
			InterfaceTransferFileRecs[NodeIndex].TransferRecs[i].Include;
		ANode.Values[cxColOriginalID.ItemIndex] :=
			InterfaceTransferFileRecs[NodeIndex].TransferRecs[i].OriginalID;
		ANode.Values[cxColNewID.ItemIndex] :=
			InterfaceTransferFileRecs[NodeIndex].TransferRecs[i].NewID;
	end;
	Screen.Cursor := crDefault;

	NumNodesSelected := 0;
	NumCurrentInterfaceNodesSelected := 0;
	for i := 0 to Length(InterfaceTransferFileRecs)-1 do
		for j := 0 to Length(InterfaceTransferFileRecs[i].TransferRecs)-1 do
		begin
			if InterfaceTransferFileRecs[i].TransferRecs[j].Include then
			begin
				Inc(NumNodesSelected);
				if i = NodeIndex then
					Inc(NumCurrentInterfaceNodesSelected);
			end;
		end;
	lblNumSelectedNodes.Caption := Format('%d Total Nodes Selected; %d Nodes Selected for Current Interface',
		[NumNodesSelected, NumCurrentInterfaceNodesSelected]);
end;

procedure TfrmMultiCombineInterfaceFiles.SaveExceptionConversions;
var
  i: Integer;
  AExceptionConversion: TExceptionConversion;
  ANode: TcxTreeListNode;
  InterfaceFileList: TStringList;
  NodeList: TStringList;
begin
	ModelIni.EraseSection(ExceptionConversionsSection);

  ExceptionConversions.Clear;
  InterfaceFileList := TStringList.Create;
  NodeList := TStringList.Create;
  FillInterfaceFilesStringList(InterfaceFileList);
  for i := 0 to treeExceptionalNodes.Count-1 do
  begin
    ANode := treeExceptionalNodes.Items[i];
    AExceptionConversion := TExceptionConversion.Create;
		AExceptionConversion.InterfaceFileIndex := cxColPipeNotFoundFileAssignment.ItemIndex;
		if AExceptionConversion.InterfaceFileIndex <> -1 then
		begin
			FillInterfaceFileNodesStringList(NodeList, AExceptionConversion.InterfaceFileIndex);
      if ANode.Values[cxColPipeNotFoundFileAssignment.ItemIndex] <> Null then
      begin
  			AExceptionConversion.FlowIndex := NodeList.IndexOf(ANode.Values[cxColPipeNotFoundFileAssignment.ItemIndex]);
  			ModelIni.WriteString(ExceptionConversionsSection, ANode.Values[cxColStopPipeNotFound.ItemIndex],
		  		Format('%s;%s', [ANode.Values[cxColPipeNotFoundFileAssignment.ItemIndex],
          ANode.Values[cxColMultipleInflowsFlow.ItemIndex]]));
      end
      else
        AExceptionConversion.FlowIndex := -1;
		end
		else if (ANode.Values[cxColPipeNotFoundFileAssignment.ItemIndex] = 'EXCLUDE') and
			(ANode.Values[cxColStopPipeNotFound.ItemIndex] = 'EXCLUDE') then
		begin
			AExceptionConversion.FlowIndex := -1;
			ModelIni.WriteString(ExceptionConversionsSection, ANode.Values[cxColStopPipeNotFound.ItemIndex],
				'EXCLUDE;EXCLUDE');
		end
		else
			AExceptionConversion.FlowIndex := -1;
		ExceptionConversions.AddObject(ANode.Values[cxColStopPipeNotFound.ItemIndex], AExceptionConversion);

	end;

	NodeList.Free;
	InterfaceFileList.Free;
end;

procedure TfrmMultiCombineInterfaceFiles.FillInterfaceFilesStringList(
	AStringList: TStringList);
var
	i: Integer;
begin
	AStringList.Clear;
	for i := 0 to Length(InterfaceFiles)-1 do
		AStringList.Add(ExtractFileName(InterfaceFiles[i].FileName));
end;

procedure TfrmMultiCombineInterfaceFiles.FormDestroy(Sender: TObject);
var
	i: Integer;
begin
  inherited;
	for i := 0 to ExceptionConversions.Count-1 do
		ExceptionConversions.Objects[i].Free;
	for i := 0 to MultipleFlowExclusions.Count-1 do
		MultipleFlowExclusions.Objects[i].Free;
	ExceptionConversions.Free;
  MultipleFlowExclusions.Free;
	ModelIni.Free;
end;

procedure TfrmMultiCombineInterfaceFiles.FillInterfaceFileNodesStringList(
  AStringList: TStringList; InterfaceFileIndex: Integer);
var
  i: Integer;
begin
  AStringList.Clear;
  for i := 0 to Length(InterfaceTransferFileRecs[InterfaceFileIndex].TransferRecs)-1 do
    AStringList.Add(InterfaceTransferFileRecs[InterfaceFileIndex].TransferRecs[i].OriginalID);
end;

procedure TfrmMultiCombineInterfaceFiles.ResetExceptionConversions;
var
	i: Integer;
  ExceptionsList: TStringList;
  Tokens: TStringList;
  Conversion: String;
  StopPipeCol: TcxTreeListColumn;
  FileCol: TcxTreeListColumn;
  FlowCol: TcxTreeListColumn;
  ANode: TcxTreeListNode;
	InterfaceFileList: TStringList;
	InterfaceFileNodesList: TStringList;
begin
	for i := 0 to ExceptionConversions.Count-1 do
		with ExceptionConversions.Objects[i] as TExceptionConversion do
		begin
			if (InterfaceFileIndex <> -1) and (FlowIndex <> -1) then begin
				InterfaceTransferFileRecs[InterfaceFileIndex].TransferRecs[FlowIndex].Include := False;
				InterfaceTransferFileRecs[InterfaceFileIndex].TransferRecs[FlowIndex].NewID := '';
			end;
		end;

	// Read in the ini exception conversions if necessary
	Assert(Assigned(ModelIni), 'Model was not opened, but trying to reset exception conversions');
	ExceptionsList := TStringList.Create;
	Tokens := TStringList.Create;
	ModelIni.ReadSection(ExceptionConversionsSection, ExceptionsList);
	StopPipeCol := cxColStopPipeNotFound;
	FileCol := cxColPipeNotFoundFileAssignment;
	FlowCol := cxColPipeNotFoundFlowAssignment;
	InterfaceFileLIst := TStringList.Create;
	FillInterfaceFilesStringList(InterfaceFileList);
	InterfaceFileNodesList := TStringList.Create;
	for i := 0 to treeExceptionalNodes.Count-1 do
	begin
		ANode := treeExceptionalNodes.Items[i];
		Conversion := ModelIni.ReadString(ExceptionConversionsSection,
			ANode.Values[StopPipeCol.ItemIndex], '');
		if Conversion <> '' then
		begin
			ExtractTokensL(Conversion, ';', '''', False, Tokens);
			if InterfaceFileList.IndexOf(Tokens[0]) > -1 then
			begin
				InterfaceFileNodesList.Clear;
				FillInterfaceFileNodesStringList(InterfaceFileNodesList,
					InterfaceFileList.IndexOf(Tokens[0]));
				ANode.Values[FileCol.ItemIndex] := Tokens[0];
				// To Do: we must test to see that this FlowID has not already been assigned
				if InterfaceFileNodesList.IndexOf(Tokens[1]) > -1 then
					ANode.Values[FlowCol.ItemIndex] := Tokens[1]
				else
					ANode.Values[FlowCol.ItemIndex] := Tokens[1] + ' (not found)';
			end
			else if (Uppercase(Tokens[0]) = 'EXCLUDE') and
				(Uppercase(Tokens[1]) = 'EXCLUDE') then
			begin
				ANode.Values[FileCol.ItemIndex] := 'EXCLUDE';
				ANode.Values[FlowCol.ItemIndex] := 'EXCLUDE';
			end;
		end;
	end;
	Tokens.Free;
	ExceptionsList.Free;
	InterfaceFileList.Free;
	InterfaceFileNodesList.Free;
end;

procedure TfrmMultiCombineInterfaceFiles.LeavePage(Direction: TLeavePage);
var
	i, j: Integer;
	InterfaceFileIndex: Integer;
	InterfaceFileList: TStringList;
	NodeList: TStringList;
	NodeIndex: Integer;
	IDColumn: TcxTreeListColumn;
	IncludeColumn: TcxTreeListColumn;
	FileColumn: TcxTreeListColumn;
	FlowColumn: TcxTreeListColumn;
	CurrentNode: TcxTreeListNode;
	PreviousPage: TRzTabSheet;
begin
	// Determine the page we just left
	if Direction = lpPrevious then
		PreviousPage := pgcMultiCombineWizard.Pages[pgcMultiCombineWizard.ActivePageIndex+1]
	else
		PreviousPage := pgcMultiCombineWizard.Pages[pgcMultiCombineWizard.ActivePageIndex-1];

	// These procedures fire after we leave a page
	if PreviousPage = shtSpecifyFiles then
	begin
		lstInterfaceFiles.Clear;
		for i := 0 to Length(InterfaceTransferFileRecs)-1 do
		begin
			lstInterfaceFiles.Add(ExtractFileName(InterfaceTransferFileRecs[i].FileName))
		end;
		lstInterfaceFiles.ItemIndex := 0;
		lstInterfaceFilesClick(Self);

	end
	else if PreviousPage = shtReviewExceptionalNodes then
	begin
		Screen.Cursor := crHourglass;

		// Commit the exceptional node conversions
		IDColumn := cxColStopPipeUSNode;
		FileColumn := cxColPipeNotFoundFileAssignment;
		FlowColumn := cxColPipeNotFoundFlowAssignment;
		InterfaceFileList := TStringList.Create;
		FillInterfaceFilesStringList(InterfaceFileList);
		errHandler.Clear;
		for i := 0 to treeExceptionalNodes.Count-1 do
		begin
			CurrentNode := treeExceptionalNodes.Items[i];
			if ((Length(CurrentNode.Texts[FileColumn.ItemIndex]) > 0) and
          (Uppercase(CurrentNode.Texts[FileColumn.ItemIndex]) = 'EXCLUDE')) and
				((Length(CurrentNode.Texts[FlowColumn.ItemIndex]) > 0) and
          (Uppercase(CurrentNode.Texts[FlowColumn.ItemIndex]) = 'EXCLUDE')) then
			begin
				// skip it
			end
			else if (CurrentNode.Values[FileColumn.ItemIndex] <> '') and
        (CurrentNode.Values[FileColumn.ItemIndex] <> Null) then
			begin
				InterfaceFileIndex := InterfaceFileList.IndexOf(CurrentNode.Values[FileColumn.ItemIndex]);
				NodeList := TStringList.Create;
				for j := 0 to Length(InterfaceTransferFileRecs[InterfaceFileIndex].TransferRecs)-1 do
				begin
					NodeList.Add(InterfaceTransferFileRecs[InterfaceFileIndex].TransferRecs[j].OriginalID);
				end;
				NodeIndex := NodeList.IndexOf(CurrentNode.Values[FlowColumn.ItemIndex]);
				if NodeIndex = -1 then
				begin
					// TODO: List out the nodes rather than pop up a message for each one
					// MessageDlg('Cannot find node.  Respecify FlowID for StopPipe '+
					//	CurrentNode.Values[treeExceptionalNodes.ColumnByName('colStopPipeNotFound').Index],
					//	mtWarning, [mbOK], 0)
					errHandler.Add(Format('Cannot find node %s. Respecify Flow ID for StopPipe %s',
						[CurrentNode.Values[FlowColumn.ItemIndex],
						CurrentNode.Values[cxColStopPipeNotFound.ItemIndex]]), etWarning);
				end
				else
				begin
					InterfaceTransferFileRecs[InterfaceFileIndex].TransferRecs[NodeIndex].Include := True;
					InterfaceTransferFileRecs[InterfaceFileIndex].TransferRecs[NodeIndex].NewID :=
						CurrentNode.Values[IDColumn.ItemIndex];
				end;
				NodeList.Free;
			end;
		end;
		errHandler.Caption := 'Exceptional Nodes messages';
		errHandler.HandleErrors;
		InterfaceFileList.Free;
		SaveExceptionConversions;

		Screen.Cursor := crDefault;
	end
	else if PreviousPage = shtReviewMultipleInflows then
	begin
		SaveMultipleFlowExclusions;
	end;

	// These procedures fire when we arrive on a page
	if ((pgcMultiCombineWizard.ActivePage = shtReviewExceptionalNodes) or
		(pgcMultiCombineWizard.ActivePage = shtReviewMultipleInflows)) and
		not AutoGenerated then
	begin
		if Direction = lpPrevious then
			pgcMultiCombineWizard.ActivePage := shtSelectNodes
		else
		begin
			pgcMultiCombineWizard.ActivePage := shtReviewMultiCombinedInterfaceFile;
			Application.ProcessMessages;
			PreparePreviewInterfaceFile;
		end
	end
	else if pgcMultiCombineWizard.ActivePage = shtReviewExceptionalNodes then
		ResetExceptionConversions
	else if pgcMultiCombineWizard.ActivePage = shtReviewMultipleInflows then
	begin
		FindDuplicateFlows;
		ResetMultipleFlowExclusions;
	end
	else if pgcMultiCombineWizard.ActivePage = shtReviewMultiCombinedInterfaceFile then
		PreparePreviewInterfaceFile
	else if pgcMultiCombineWizard.ActivePage = shtSaveInterface then
  begin
    lblCurrentDate.Caption := '';
		if Assigned(ModelIni) then
			ModelIni.UpdateFile;
  end;
end;

procedure TfrmMultiCombineInterfaceFiles.FindDuplicateFlows;
var
	i: Integer;
	j: Integer;
	ANode: TcxTreeListNode;
	NewTransferID: String;
	colInterfaceFileWidth: Integer;
	NumTotalNodes: Integer;
  InterfaceFileList: TStringList;
  NodeList: TStringList;
  ExclusionsList: TStringList;
  Exclusion: String;
	Tokens: TStringList;
  InterfaceFileIndex: Integer;
  FlowIndex: Integer;
begin
	inherited;
	Screen.Cursor := crHourglass;

	treeMultipleInflows.Clear;
	treeMultipleInflows.BeginUpdate;
	pnlMultipleInflowsAssembly.Show;
  Application.ProcessMessages;

	NumTotalNodes := 0;
	for i := 0 to Length(InterfaceTransferFileRecs)-1 do
		with InterfaceTransferFileRecs[i] do
			Inc(NumTotalNodes, Length(TransferRecs));
	prgMultipleInflowsAssembly.TotalParts := NumTotalNodes;

	// Check-in the INI exclusions so that they'll show up on the list
	ExclusionsList := TStringList.Create;
	InterfaceFileList := TStringList.Create;
	Tokens := TStringList.Create;
	FillInterfaceFilesStringList(InterfaceFileList);
	NodeList := TStringList.Create;
	ModelIni.ReadSection(MultipleFlowExclusionsSection, ExclusionsList);
	if ExclusionsList.Count > 0 then
	begin
		errHandler.Clear;
		for i := 0 to ExclusionsList.Count-1 do
		begin
			ExtractTokensL(ExclusionsList[i], ';', '''', False, Tokens);
			InterfaceFileIndex := InterfaceFileList.IndexOf(Tokens[1]);
			if InterfaceFileIndex < 0 then
			begin
				errHandler.Add(ExclusionsList[i]+':Invalid, File not found', etWarning);
			end
			else
			begin
				FillInterfaceFileNodesStringList(NodeList, InterfaceFileIndex);
				Exclusion := ModelIni.ReadString(MultipleFlowExclusionsSection, ExclusionsList[i], '');
				FlowIndex := NodeList.IndexOf(Exclusion);
        // Make sure include is true for the node so that it shows up on the
        // treeMultipleInflows grid first; it will be checked off later
				InterfaceTransferFileRecs[InterfaceFileIndex].TransferRecs[FlowIndex].Include := True;
			end;
		end;
	end;
	errHandler.Caption := 'Duplicate Flows messages';
	errHandler.HandleErrors;
	ExclusionsList.Free;
	InterfaceFileList.Free;
	NodeList.Free;
	Tokens.Free;

	treeMultipleInflows.Clear;
	for i := 0 to Length(InterfaceTransferFileRecs)-1 do
		with InterfaceTransferFileRecs[i] do
			for j := 0 to Length(TransferRecs)-1 do
			begin
				if TransferRecs[j].Include then
				begin
					if TransferRecs[j].NewID <> '' then
						NewTransferID := TransferRecs[j].NewID
					else
						NewTransferID := TransferRecs[j].OriginalID;
					ANode := FindMultipleInflowByID(NewTransferID);
					if not Assigned(ANode) then
					begin
						ANode := treeMultipleInflows.Add;
						ANode.Values[cxColMultipleInflowsTarget.ItemIndex] :=
							NewTransferID
					end;
					ANode := ANode.AddChild;
					ANode.Values[cxColMultipleInflowsFile.ItemIndex] := ExtractFileName(FileName);
					ANode.Values[cxColMultipleInflowsFlow.ItemIndex] := TransferRecs[j].OriginalID;
					ANode.Values[cxColMultipleInflowsTarget.ItemIndex] := NewTransferID;
					ANode.Values[cxColMultipleInflowsInclude.ItemIndex] := True;
				end;
				prgMultipleInflowsAssembly.IncPartsByOne;
			end;

	// Remove nonmultiples
	for i := treeMultipleInflows.Count-1 downto 0 do
		if treeMultipleInflows.Items[i].Count < 2 then
			treeMultipleInflows.Items[i].Free;

	pnlMultipleInflowsAssembly.Hide;
	cxColMultipleInflowsTarget.SortOrder := soAscending;
//	treeMultipleInflows.RefreshSorting;
	treeMultipleInflows.FullExpand;
	treeMultipleInflows.EndUpdate;
  treeMultipleInflows.Items[0].Focused := True;

	Screen.Cursor := crDefault;
end;

function TfrmMultiCombineInterfaceFiles.FindMultipleInflowByID(
  AID: String): TcxTreeListNode;
var
	i: Integer;
	PreviewNewIDIdx: Integer;
begin
	Result := nil;
	for i := 0 to treeMultipleInflows.Count-1 do
	begin
		PreviewNewIDIdx := cxColMultipleInflowsTarget.ItemIndex;
		if treeMultipleInflows.Items[i].Values[PreviewNewIDIdx] = AID then
		begin
			Result := treeMultipleInflows.Items[i];
			Break;
		end;
	end;
end;

procedure TfrmMultiCombineInterfaceFiles.SaveMultipleFlowExclusions;
var
	i, j: Integer;
	InterfaceFileIndex: Integer;
	InterfaceFileList: TStringList;
	NodeList: TStringList;
	NodeIndex: Integer;
	IDColumn: TcxTreeListColumn;
	IncludeColumn: TcxTreeListColumn;
	FileColumn: TcxTreeListColumn;
	FlowColumn: TcxTreeListColumn;
	CurrentNode: TcxTreeListNode;
  MultipleFlowExclusion: TMultipleFlowExclusion;
begin
	ModelIni.EraseSection(MultipleFlowExclusionsSection);

  Screen.Cursor := crHourglass;

  // Commit the multiple inflow exclusions
  IDColumn := treeMultipleInflows.ColumnByName('cxColMultipleInflowsTarget');
  IncludeColumn := treeMultipleInflows.ColumnByName('cxColMultipleInflowsInclude');
  FileColumn := treeMultipleInflows.ColumnByName('cxColMultipleInflowsFile');
  FlowColumn := treeMultipleInflows.ColumnByName('cxColMultipleInflowsFlow');
  InterfaceFileList := TStringList.Create;
  FillInterfaceFilesStringList(InterfaceFileList);
  for i := 0 to treeMultipleInflows.Count-1 do
  begin
    CurrentNode := treeMultipleInflows.Items[i];
    for j := 0 to CurrentNode.Count-1 do
    begin
      if CurrentNode.Items[j].Values[FileColumn.ItemIndex] <> '' then
      begin
        InterfaceFileIndex := InterfaceFileList.IndexOf(CurrentNode.Items[j].Values[FileColumn.ItemIndex]);
        NodeList := TStringList.Create;
        FillInterfaceFileNodesStringList(NodeList, InterfaceFileIndex);
        NodeIndex := NodeList.IndexOf(CurrentNode.Items[j].Values[FlowColumn.ItemIndex]);
        InterfaceTransferFileRecs[InterfaceFileIndex].TransferRecs[NodeIndex].Include :=
          CurrentNode.Items[j].Values[IncludeColumn.ItemIndex];
        if not (CurrentNode.Items[j].Values[IncludeColumn.ItemIndex] = 'True') then
        begin
          ModelIni.WriteString(MultipleFlowExclusionsSection, Format('%s;%s',
            [CurrentNode.Values[IDColumn.ItemIndex], CurrentNode.Items[j].Values[FileColumn.ItemIndex]]),
            CurrentNode.Items[j].Values[FlowColumn.ItemIndex]);
        end;
        InterfaceTransferFileRecs[InterfaceFileIndex].TransferRecs[NodeIndex].NewID :=
          CurrentNode.Items[j].Values[IDColumn.ItemIndex];
        NodeList.Free;
      end;
    end;
  end;
  // Write out any INI exclusions that got applied
{  for i := 0 to MultipleFlowExclusions.Count-1 do
  begin
    with MultipleFlowExclusions.Objects[i] as TMultipleFlowExclusion do
    begin
      ModelIni.WriteString(MultipleFlowExclusionsSection, Format('%s;%s',
        [Target, ExtractFileName(InterfaceTransferFileRecs[InterfaceFileIndex].FileName)]),
        InterfaceTransferFileRecs[InterfaceFileIndex].TransferRecs[FlowIndex].OriginalID);
    end;
  end;}

  InterfaceFileList.Free;

  Screen.Cursor := crDefault;
end;

procedure TfrmMultiCombineInterfaceFiles.ResetMultipleFlowExclusions;
var
  i, j: Integer;
	IDColumn: TcxTreeListColumn;
	IncludeColumn: TcxTreeListColumn;
	FileColumn: TcxTreeListColumn;
	FlowColumn: TcxTreeListColumn;
	CurrentNode: TcxTreeListNode;
  Exclusion: String;
  ExclusionsList: TStringList;
  InterfaceFileList: TStringList;
  NodeList: TStringList;
  MultipleFlowExclusion: TMultipleFlowExclusion;
  InterfaceFileIndex: Integer;
  FlowIndex: Integer;
begin
	IDColumn := cxColMultipleInflowsTarget;
	IncludeColumn := cxColMultipleInflowsInclude;
	FileColumn := cxColMultipleInflowsFile;
	FlowColumn := cxColMultipleInflowsFlow;

	MultipleFlowExclusions.Clear;

	ExclusionsList := TStringList.Create;
	InterfaceFileList := TStringList.Create;
	FillInterfaceFilesStringList(InterfaceFileList);
	NodeList := TStringList.Create;
	ModelIni.ReadSection(MultipleFlowExclusionsSection, ExclusionsList);
	for i := 0 to treeMultipleInflows.Count-1 do
	begin
		CurrentNode := treeMultipleInflows.Items[i];
		for j := 0 to CurrentNode.Count-1 do
		begin
			Exclusion := ModelIni.ReadString(MultipleFlowExclusionsSection,
				Format('%s;%s', [CurrentNode.Items[j].Values[IDColumn.ItemIndex],
				CurrentNode.Items[j].Values[FileColumn.ItemIndex]]), '');
			if (Exclusion <> '') and (CurrentNode.Items[j].Values[FlowColumn.ItemIndex] = Exclusion) then
			begin
				CurrentNode.Items[j].Values[IncludeColumn.ItemIndex] := 'False';
				InterfaceFileIndex := InterfaceFileList.IndexOf(CurrentNode.Items[j].Values[FileColumn.ItemIndex]);
				FillInterfaceFileNodesStringList(NodeList, InterfaceFileIndex);
				FlowIndex := NodeList.IndexOf(CurrentNode.Items[j].Values[FlowColumn.ItemIndex]);
				MultipleFlowExclusion := TMultipleFlowExclusion.Create;
				MultipleFlowExclusion.Target := CurrentNode.Items[j].Values[IDColumn.ItemIndex];
				MultipleFlowExclusion.InterfaceFileIndex := InterfaceFileIndex;
				MultipleFlowExclusion.FlowIndex := FlowIndex;
				MultipleFlowExclusions.AddObject(CurrentNode.Items[j].Values[IDColumn.ItemIndex],
					MultipleFlowExclusion);
			end;
		end;
	end;
	ExclusionsList.Free;
	InterfaceFileList.Free;
	NodeList.Free;
end;

procedure TfrmMultiCombineInterfaceFiles.btnExportExceptionalNodesClick(
  Sender: TObject);
var
  i: Integer;
  AFile: TFileStream;
  ATextFile: TStAnsiTextStream;
  TextLine: String;
  CurrentNode: TcxTreeListNode;
begin
	frmMain.dlgSave.InitialDir := frmMain.InitRegistryDir;
	frmMain.dlgSave.Title := 'Save Exceptional Nodes List';
	frmMain.dlgSave.Filter := 'CSV file (*.csv)|*.csv|All Files|*.*';
	frmMain.dlgSave.DefaultExt := 'csv';
	if frmMain.dlgSave.Execute then
	begin
    AFile := TFileStream.Create(frmMain.dlgSave.FileName, fmCreate);
    ATextFile := TStAnsiTextStream.Create(AFile);
		TextLine := '';
    for i := 0 to treeExceptionalNodes.ColumnCount-1 do
      TextLine := TextLine + treeExceptionalNodes.Columns[i].Caption.Text + ',';
    ATextFile.WriteLine(TextLine);
    for i := 0 to treeExceptionalNodes.Count-1 do
    begin
      CurrentNode := treeExceptionalNodes.Items[i];
      TextLine := Format('%s,%s,%s,%s', [CurrentNode.Texts[0],
        CurrentNode.Texts[1], CurrentNode.Texts[2], CurrentNode.Texts[3]]);
      ATextFile.WriteLine(TextLine);
    end;
	end;
  ATextFile.Free;
  AFile.Free;
	ShowMessage('Wrote file '+frmMain.dlgSave.FileName);
end;

function TfrmMultiCombineInterfaceFiles.GetLatestAvailableTime: TDateTime;
var
  i: Integer;
	CompareTime: TDateTime;
	OffsetEndTimes: array of TDateTime;
begin
	CompareTime := 0;
	SetLength(OffsetEndTimes, Length(InterfaceTransferFileRecs));
	for i := 0 to Length(InterfaceTransferFileRecs) - 1 do
	begin
		OffsetEndTimes[i] := (InterfaceTransferFileRecs[i].FileExtractEndTime -
				InterfaceTransferFileRecs[i].FileActualStartTime) +
				InterfaceTransferFileRecs[i].FileRelativeStartTime;
	end;

	for i := 0 to Length(InterfaceTransferFileRecs) - 1 do    // Iterate
	begin
		if CompareDateTime(CompareTime, OffsetEndTimes[i]) = LessThanValue then
			CompareTime := OffsetEndTimes[i];
	end;    // for
	if CompareTime = 0 then
		Result := MaxDateTime
	else
		Result := CompareTime;
end;

procedure TfrmMultiCombineInterfaceFiles.treeInterfaceFilesCustomDrawCell(
  Sender: TObject; ACanvas: TcxCanvas; AViewInfo: TcxTreeListEditCellViewInfo;
  var ADone: Boolean);
var
  TempRect: TRect;
  AText: String;
begin
	if Length(InterfaceFiles) > 0 then
		if AViewInfo.Column = colLocation then
		begin
      AText := InterfaceFiles[AViewInfo.Node.Index].Location;
      UniqueString(AText);
			TempRect := AViewInfo.BoundsRect;
			DrawTextEx(ACanvas.Handle, PChar(AText), Length(PChar(AText)), TempRect,
				DT_CALCRECT or DT_MODIFYSTRING or DT_NOPREFIX or DT_PATH_ELLIPSIS or DT_LEFT, nil);
		end
		else
		if AViewInfo.Column.Name = 'colInterfaceFile' then
		begin
			AText := InterfaceFiles[AViewInfo.Node.Index].FileName;
      UniqueString(AText);
			TempRect := AViewInfo.BoundsRect;
			DrawTextEx(ACanvas.Handle, PChar(AText), Length(PChar(AText)), TempRect,
				DT_CALCRECT or DT_MODIFYSTRING or DT_NOPREFIX or DT_END_ELLIPSIS or DT_LEFT, nil);
		end;
end;

procedure TfrmMultiCombineInterfaceFiles.treeNodesFocusedColumnChanged(
  Sender: TObject; APrevFocusedColumn, AFocusedColumn: TcxTreeListColumn);
begin
  treeNodes.OptionsBehavior.IncSearch := AFocusedColumn = cxColOriginalID;
end;

procedure TfrmMultiCombineInterfaceFiles.treePreviewNodesFocusedColumnChanged(
  Sender: TObject; APrevFocusedColumn, AFocusedColumn: TcxTreeListColumn);
begin
  treePreviewNodes.OptionsBehavior.IncSearch := (AFocusedColumn = cxColPreviewNewID) or
		(AFocusedColumn = cxColPreviewOriginalID);
end;

procedure TfrmMultiCombineInterfaceFiles.treeNodesEdited(Sender: TObject;
  AColumn: TcxTreeListColumn);
var
	InterfaceIdx: Integer;
begin
	InterfaceIdx := treeNodes.FocusedNode.Index;
	InterfaceTransferFileRecs[lstInterfaceFiles.ItemIndex].TransferRecs[InterfaceIdx].Include :=
		treeNodes.FocusedNode.Values[cxColIncludeNode.ItemIndex];
	InterfaceTransferFileRecs[lstInterfaceFiles.ItemIndex].TransferRecs[InterfaceIdx].NewID :=
		treeNodes.FocusedNode.Values[cxColNewID.ItemIndex];
end;

procedure TfrmMultiCombineInterfaceFiles.treeExceptionalNodesCompare(
  Sender: TObject; ANode1, ANode2: TcxTreeListNode; var ACompare: Integer);
begin
	if ANode1.Values[cxColStopPipeNotFound.ItemIndex] >
		ANode2.Values[cxColStopPipeNotFound.ItemIndex] then
		ACompare := 1
	else if ANode1.Values[cxColStopPipeNotFound.ItemIndex] <
		ANode2.Values[cxColStopPipeNotFound.ItemIndex] then
		ACompare := -1
	else
		ACompare := 0;
end;

procedure TfrmMultiCombineInterfaceFiles.treeExceptionalNodesFocusedColumnChanged(
  Sender: TObject; APrevFocusedColumn, AFocusedColumn: TcxTreeListColumn);
var
  i: Integer;
  InterfaceFileIndex: Integer;
  FileList: TStringList;
begin
  if AFocusedColumn = cxColPipeNotFoundFlowAssignment then
  begin
    FileList := TStringList.Create;
    for i := 0 to Length(InterfaceFiles)-1 do
      FileList.Add(ExtractFileName(InterfaceFiles[i].FileName));
    InterfaceFileIndex := FileList.IndexOf(
      treeExceptionalNodes.FocusedNode.Values[cxcolPipeNotFoundFileAssignment.ItemIndex]);
    if InterfaceFileIndex > -1 then
    begin
      for i := 0 to Length(InterfaceTransferFileRecs[InterfaceFileIndex].TransferRecs)-1 do
        if InterfaceTransferFileRecs[InterfaceFileIndex].TransferRecs[i].Include = False then
          (AFocusedColumn.Properties as TcxComboBoxProperties).Items.Add(InterfaceTransferFileRecs[InterfaceFileIndex].TransferRecs[i].OriginalID);
    end;
    (AFocusedColumn.Properties as TcxComboBoxProperties).Sorted := True;
  end;
end;

procedure TfrmMultiCombineInterfaceFiles.treeMultipleInflowsCompare(
  Sender: TObject; ANode1, ANode2: TcxTreeListNode; var ACompare: Integer);
begin
	if ANode1.Values[cxColMultipleInflowsTarget.ItemIndex] >
		ANode2.Values[cxColMultipleInflowsTarget.ItemIndex] then
		ACompare := 1
	else if ANode1.Values[cxColMultipleInflowsTarget.ItemIndex] <
		ANode2.Values[cxColMultipleInflowsTarget.ItemIndex] then
		ACompare := -1
	else
		ACompare := 0;
end;

procedure TfrmMultiCombineInterfaceFiles.treeExceptionalNodesCustomDrawCell(
  Sender: TObject; ACanvas: TcxCanvas; AViewInfo: TcxTreeListEditCellViewInfo;
  var ADone: Boolean);
var
	idxColFileAssignment: Integer;
	idxColFlowAssignment: Integer;
	idxColUSNode: Integer;
  AColor: TColor;
begin
	idxColFileAssignment := cxColPipeNotFoundFileAssignment.ItemIndex;
	idxColFlowAssignment := cxColPipeNotFoundFlowAssignment.ItemIndex;
	idxColUSNode := cxColStopPipeUSNode.ItemIndex;

	if (AViewInfo.Node.Values[idxColUSNode] = 'Stop Pipe not in receiving model') then
	begin
		ACanvas.Brush.Color := clMoneyGreen;
	end
	else if (AViewInfo.Node.Values[idxColFileAssignment] = 'EXCLUDE') and
		(AViewInfo.Node.Values[idxColFlowAssignment] = 'EXCLUDE') then
	begin
		ACanvas.Brush.Color := clLtGray;
	end
	else if (AViewInfo.Node.Values[idxColFlowAssignment] <> Null) and
    ((Pos('(not found)',AViewInfo.Node.Values[idxColFlowAssignment])) > 0) then
	begin
		ACanvas.Brush.Color := clRed;
	end;
  ACanvas.FillRect(AViewInfo.BoundsRect);
  ADone := False;
end;

procedure TfrmMultiCombineInterfaceFiles.treeMultipleInflowsEditing(
  Sender: TObject; AColumn: TcxTreeListColumn; var Allow: Boolean);
begin
  inherited;
  if (AColumn = cxColMultipleInflowsFile) or (AColumn = cxColMultipleInflowsFlow) or
    (AColumn = cxColMultipleInflowsTarget) then
    Allow := False
end;

procedure TfrmMultiCombineInterfaceFiles.treeCheckCustomDrawCell(
  Sender: TObject; ACanvas: TcxCanvas; AViewInfo: TcxTreeListEditCellViewInfo;
  var ADone: Boolean);
begin
  inherited;
  // Highlight volumes on check table where volumes are <= 0
  if AViewInfo.Node.Values[cxColTotalVolume.ItemIndex] <= 0 then
  begin
    ACanvas.Brush.Color := clLtGray;
    ACanvas.FillRect(AViewInfo.BoundsRect);
    ADone := False;
  end;
end;

procedure TfrmMultiCombineInterfaceFiles.btnCopyCheckToClipboardClick(
  Sender: TObject);
var
  i: Integer;
  j: Integer;
  AString: String;
  AStringList: TStringList;
begin
  AStringList := TStringList.Create;
  try
    for i := 0 to treeCheck.Count-1 do
    begin
      AString := treeCheck.Nodes[i].Texts[0];
      for j := 1 to treeCheck.ColumnCount-1 do
      begin
        AString := AString + #9 + treeCheck.Nodes[i].Texts[j];
      end;
      AStringList.Add(AString);
    end;
    Clipboard.AsText := AStringList.Text;
  finally
    AStringList.Free;
  end;
end;

procedure TfrmMultiCombineInterfaceFiles.btnMakeExtractSameAsStartClick(Sender: TObject);
var
  i: Integer;
  CurrentNode: TcxTreeListNode;
begin
  for i := 0 to treeInterfaceFiles.Count-1 do
  begin
    CurrentNode := treeInterfaceFiles.Nodes[i];
    CurrentNode.Values[colExtractStartDate.ItemIndex] :=
      DateOf(StrToDateTime(CurrentNode.Texts[colFileStart.ItemIndex]));
    CurrentNode.Values[colExtractEndDate.ItemIndex] :=
      DateOf(StrToDateTime(CurrentNode.Texts[colFileStart.ItemIndex]));
  end;
end;

procedure TfrmMultiCombineInterfaceFiles.edtSyncDateChange(Sender: TObject);
var
  i: Integer;
  CurrentNode: TcxTreeListNode;
begin
  for i := 0 to treeInterfaceFiles.Count-1 do
  begin
    CurrentNode := treeInterfaceFiles.Nodes[i];
    CurrentNode.Values[colNewStartDate.ItemIndex] :=
      DateOf(edtSyncDate.Date);
  end;
end;

procedure TfrmMultiCombineInterfaceFiles.treeMultipleInflowsCustomDrawCell(
  Sender: TObject; ACanvas: TcxCanvas; AViewInfo: TcxTreeListEditCellViewInfo;
  var ADone: Boolean);
begin
  inherited;
  if (AViewInfo.Node.Values[cxColMultipleInflowsFile.ItemIndex] = Null) and
    (AViewInfo.Column = cxColMultipleInflowsInclude) then
  begin
    ACanvas.Brush.Color := clWindow;
    ACanvas.FillRect(AViewInfo.BoundsRect);
    ADone := True;
  end;
end;

end.
