unit fMain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, RzButton, cxContainer, cxShellTreeView, cxControls,
	cxInplaceContainer, cxTL, cxShellListView, ComCtrls, RzListVw,
	RzShellCtrls, RzTreeVw, RzPrgres, StdCtrls, RzLabel, cxStyles, cxGraphics,
  RzCommon, RzPanel, ExtCtrls, RzSplit, cxCustomData, cxTextEdit,
  cxCalendar, StdActns, ActnList, dxBar, ClipBrd, cxSpinEdit;

type
	TfrmMain = class(TForm)
    cxStyleRepository1: TcxStyleRepository;
    styleOddRow: TcxStyle;
    styleEvenRow: TcxStyle;
    RzFrameController1: TRzFrameController;
    RzSizePanel1: TRzSizePanel;
    treeShell: TRzShellTree;
    btnScan: TRzButton;
    RzPanel1: TRzPanel;
    lblDir: TRzLabel;
    treeResultsList: TcxTreeList;
    colDirectory: TcxTreeListColumn;
    colLatestUpdate: TcxTreeListColumn;
    colSize: TcxTreeListColumn;
    colRawSize: TcxTreeListColumn;
    btnDrillDown: TRzButton;
    btnUp: TRzButton;
    lblSelection: TRzLabel;
    dxBarManager1: TdxBarManager;
    dxBarSubItem1: TdxBarSubItem;
    mnuEdit: TdxBarSubItem;
    mnuFileExit: TdxBarButton;
    mnuEditCopyListToClipboard: TdxBarButton;
    mnuEditCopySelectedToClipboard: TdxBarButton;
    ActionList1: TActionList;
    actCopyListToClipboard: TAction;
    actCopySelectedToClipboard: TAction;
    actFileExit: TFileExit;
    procedure actCopySelectedToClipboardUpdate(Sender: TObject);
    procedure actCopyListToClipboardUpdate(Sender: TObject);
    procedure actCopySelectedToClipboardExecute(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure actCopyListToClipboardExecute(Sender: TObject);
		procedure btnScanClick(Sender: TObject);
    procedure treeResultsListCustomDrawCell(Sender: TObject;
			ACanvas: TcxCanvas; AViewInfo: TcxTreeListEditCellViewInfo;
      var ADone: Boolean);
    procedure RzSizePanel1Resize(Sender: TObject);
    procedure btnDrillDownClick(Sender: TObject);
    procedure treeResultsListDblClick(Sender: TObject);
    procedure btnUpClick(Sender: TObject);
    procedure treeResultsListSelectionChanged(Sender: TObject);
    procedure treeResultsListColumnHeaderClick(Sender: TObject;
      AColumn: TcxTreeListColumn);
    procedure treeResultsListCompare(Sender: TObject; ANode1,
      ANode2: TcxTreeListNode; var ACompare: Integer);
	private
		{ Private declarations }
    AClipboard: TClipboard;
		procedure ScanDirForSizeAndDate(APath: String; var DirSize: Int64;
			var LastDate: TDateTime);
		function NumDirs(APath: String): Integer;
		function ReadableFileSize(AFileSize: Int64): String;
	public
		{ Public declarations }
	end;

var
	frmMain: TfrmMain;

implementation

{$R *.dfm}

uses DateUtils, Types, fProgress, CodeSiteLogging;

procedure TfrmMain.btnScanClick(Sender: TObject);
var
	RootDir: String;
	DirSearchRec: TSearchRec;
	DirSearchResult: Integer;
	DirSize: Int64;
	DirLastDate: TDateTime;
	ANode: TcxTreeListNode;
begin
	RootDir := treeShell.SelectedPathName;
	lblDir.Caption := IncludeTrailingPathDelimiter(RootDir);
	treeResultsList.Clear;
	frmProgress.prgScan.TotalParts := NumDirs(RootDir);

	Screen.Cursor := crHourglass;
	frmProgress.Show;
	try
		DirSearchResult := FindFirst(RootDir+'\*.*', faArchive+faHidden+faAnyFile+faVolumeID+
			faSysFile+faReadOnly+faDirectory, DirSearchRec);
		while DirSearchResult = 0 do
		begin
			if ((DirSearchRec.Attr and faDirectory) = faDirectory) and (DirSearchRec.Name <> '.') and
				(DirSearchRec.Name <> '..') and (DirSearchRec.Name <> 'System Volume Information') then
			begin
				frmProgress.lblProgress.Caption := 'Scanning ' + RootDir + ' > ' + DirSearchRec.Name;
				DirSize := 0;
				DirLastDate := 0;
				ScanDirForSizeAndDate(RootDir+'\'+DirSearchRec.Name, DirSize, DirLastDate);
				ANode := treeResultsList.Add;
				ANode.Values[colDirectory.ItemIndex] := DirSearchRec.Name;
				ANode.Values[colLatestUpdate.ItemIndex] := DirLastDate;
				ANode.Values[colSize.ItemIndex] := ReadableFileSize(DirSize);
				ANode.Values[colRawSize.ItemIndex] := DirSize;
				frmProgress.prgScan.IncPartsByOne;
				frmProgress.prgScan.Update;
			end;
			DirSearchResult := FindNext(DirSearchRec);
			Application.ProcessMessages;
		end;    // while
	finally
		frmProgress.Hide;
		FindClose(DirSearchRec);
		frmProgress.prgScan.PartsComplete := 0;
		Screen.Cursor := crDefault;
		treeResultsList.Sorted := False;
		colLatestUpdate.SortOrder := soAscending;
	end;
end;

function TfrmMain.NumDirs(APath: String): Integer;
var
	SearchRec: TSearchRec;
	SearchResult: Integer;
begin
	Result := 0;
	SearchResult := FindFirst(APath+'\*.*', faArchive+faHidden+faAnyFile+faVolumeID+
		faSysFile+faReadOnly+faDirectory, SearchRec);
	while SearchResult = 0 do
	begin
		if ((SearchRec.Attr and faDirectory) = faDirectory) and (SearchRec.Name <> '.') and
			(SearchRec.Name <> '..') and (SearchRec.Name <> 'System Volume Information') then
			Inc(Result);
		SearchResult := FindNext(SearchRec);
	end;    // while
	FindClose(SearchRec);
end;

function TfrmMain.ReadableFileSize(AFileSize: Int64): String;
const
	KBSize: Int64 = 1024;
	MBSize: Int64 = 1024*1024;
	GBSize: Int64 = 1024*1024*1024;
begin
//  CodeSite.EnterMethod('ReadableFileSize');
	Result := '';
	if AFileSize < KBSize then
		Result := IntToStr(AFileSize) + ' B'
	else if AFileSize < MBSize then
		Result := Format('%d.%.2d KB', [AFileSize div KBSize, (AFileSize * 100 div KBSize) mod 100])
	else if AFileSize < GBSize then
		Result := Format('%d.%.2d MB', [AFileSize div MBSize, (AFileSize * 100 div MBSize) mod 100])
	else if AFileSize >= GBSize then
		Result := Format('%d.%.2d GB', [AFileSize div GBSize, (AFileSize * 100 div GBSize) mod 100]);
//  CodeSite.ExitMethod('ReadableFileSize');
end;

procedure TfrmMain.ScanDirForSizeAndDate(APath: String; var DirSize: Int64;
	var LastDate: TDateTime);
var
	SearchRec: TSearchRec;
	SearchResult: Integer;
	SearchDate: TDateTime;
	HighFileSize: Int64;
	HighFileSizeMult: Int64;
begin
	SearchResult := FindFirst(APath+'\*.*', faArchive+faHidden+faAnyFile+faVolumeID+
		faSysFile+faReadOnly+faDirectory, SearchRec);
	while SearchResult = 0 do
	begin
		if ((SearchRec.Attr and faDirectory) = faDirectory) and (SearchRec.Name <> '.') and
			(SearchRec.Name <> '..') and (SearchRec.Name <> 'System Volume Information') then
			ScanDirForSizeAndDate(APath+'\'+SearchRec.Name, DirSize, LastDate)
		else
		begin
			if (SearchRec.Name <> '.') and ((SearchRec.Name <> '..'))then
			begin
				HighFileSize := SearchRec.FindData.nFileSizeHigh;
				HighFileSizeMult := High(Integer);
				Inc(HighFileSizeMult);
				HighFileSize := HighFileSize*HighFileSizeMult;
				DirSize := DirSize + HighFileSize + SearchRec.FindData.nFileSizeLow;
				SearchDate := FileDateToDateTime(SearchRec.Time);
				if CompareDateTime(SearchDate, LastDate) = GreaterThanValue then
					LastDate := SearchDate;
			end;
		end;
		SearchResult := FindNext(SearchRec);
	end;    // while
	FindClose(SearchRec);
end;

procedure TfrmMain.treeResultsListColumnHeaderClick(Sender: TObject;
  AColumn: TcxTreeListColumn);
var
  SizeColumn: TcxTreeListColumn;
  RawSizeColumn: TcxTreeListColumn;
  SortOrder: TcxDataSortOrder;
begin
  SizeColumn := treeResultsList.ColumnByName('colSize');
  RawSizeColumn := treeResultsList.ColumnByName('colRawSize');
  if AColumn = SizeColumn then
  begin
    SortOrder := AColumn.SortOrder;

    treeResultsList.BeginUpdate;
    try
      treeResultsList.ClearSorting;
      SizeColumn.SortOrder := SortOrder;
      RawSizeColumn.SortOrder := SortOrder;
    finally
      treeResultsList.EndUpdate;
    end;
  end;
end;

procedure TfrmMain.treeResultsListCompare(Sender: TObject; ANode1,
  ANode2: TcxTreeListNode; var ACompare: Integer);
var
  i: Integer;
  NumSortedColumns: Integer;
  SortedCol: TcxTreeListColumn;
  V1, V2: variant;
begin
  NumSortedColumns := TcxTreeList(Sender).SortedColumnCount;

  for i := 0 to NumSortedColumns - 1 do
  begin
    SortedCol := TcxTreeList(Sender).SortedColumns[i];
    V1 := ANode1.Values[SortedCol.ItemIndex];
    V2 := ANode2.Values[SortedCol.ItemIndex];
    if SortedCol = TcxTreeList(Sender).ColumnByName('colRawSize') then
    begin
      V1 := StrToInt64(V1);
      V2 := StrToInt64(V2);
    end;

    if V1 = V2 then
      ACompare := 0
    else begin
      if V1 > V2 then
        ACompare := 1
      else
        ACompare := -1;
      if SortedCol.SortOrder = soDescending then
        ACompare := -ACompare;
    end;
  end;
end;

procedure TfrmMain.treeResultsListCustomDrawCell(Sender: TObject;
	ACanvas: TcxCanvas; AViewInfo: TcxTreeListEditCellViewInfo;
	var ADone: Boolean);
var
	CurrentNode: TcxTreeListNode;
	RawSize: Int64;
begin
	CurrentNode := AViewInfo.Node;
	if AViewInfo.Column = colSize then
	begin
		RawSize := StrToInt64(CurrentNode.Values[colRawSize.ItemIndex]);
		if RawSize > (1024*1024*1024) then
		begin
			ACanvas.Font.Color := clRed;
			ACanvas.Font.Style := ACanvas.Font.Style + [fsBold];
		end;
		ADone := False;
	end;
end;

procedure TfrmMain.RzSizePanel1Resize(Sender: TObject);
begin
	treeShell.Update;
end;

procedure TfrmMain.btnDrillDownClick(Sender: TObject);
var
	DrillDownDir: String;
begin
	if (treeResultsList.Count > 0) and (treeResultsList.SelectionCount > 0) then
	begin
		DrillDownDir := lblDir.Caption + treeResultsList.Selections[0].Texts[colDirectory.ItemIndex];
		treeShell.SelectedPathName := DrillDownDir;
		btnScanClick(Sender);
	end;
end;

procedure TfrmMain.treeResultsListDblClick(Sender: TObject);
begin
	btnDrillDownClick(Sender);
end;

procedure TfrmMain.btnUpClick(Sender: TObject);
var
	UpDir: String;
begin
	UpDir := ExtractFileDir(ExcludeTrailingPathDelimiter(lblDir.Caption));
	treeShell.SelectedPathName := UpDir;
	btnScanClick(Sender);
end;

procedure TfrmMain.treeResultsListSelectionChanged(Sender: TObject);
var
	i: Integer;
	SelectedSize: Int64;
begin
	SelectedSize := 0;
	for i := 0 to treeResultsList.SelectionCount - 1 do    // Iterate
	begin
		SelectedSize := SelectedSize + treeResultsList.Selections[i].Values[colRawSize.ItemIndex];
	end;    // for
	lblSelection.Caption := Format('%d Selected; Total Size = %s',
		[treeResultsList.SelectionCount, ReadableFileSize(SelectedSize)]);
end;

procedure TfrmMain.actCopyListToClipboardExecute(Sender: TObject);
var
  ListTextAsStr: String;
  ListText: PAnsiChar;
  CurrentNode: TcxTreeListNode;
  i: Integer;
begin
  ListTextAsStr := '';
  for i := 0 to treeResultsList.Count-1 do
  begin
    CurrentNode := treeResultsList.Items[i];
    ListTextAsStr := ListTextAsStr + Format('%s (%s, %s)', [CurrentNode.Values[colDirectory.ItemIndex],
      CurrentNode.Values[colLatestUpdate.ItemIndex], CurrentNode.Values[colSize.ItemIndex]]) + #13#10;
  end;

  ListText := PChar(ListTextAsStr);
  AClipboard.Open;
  AClipboard.SetTextBuf(ListText);
  AClipboard.Close;
end;

procedure TfrmMain.FormCreate(Sender: TObject);
begin
  AClipboard := Clipboard;

  // Modify PixelsPerInch depending on Windows' report
  PixelsPerInch := Screen.PixelsPerInch;
end;

procedure TfrmMain.actCopySelectedToClipboardExecute(Sender: TObject);
var
  ListTextAsStr: String;
  ListText: PAnsiChar;
  CurrentNode: TcxTreeListNode;
  i: Integer;
begin
  ListTextAsStr := '';
  for i := 0 to treeResultsList.Count-1 do
  begin
    CurrentNode := treeResultsList.Items[i];
    if CurrentNode.Selected then
      ListTextAsStr := ListTextAsStr + Format('%s (%s, %s)', [CurrentNode.Values[colDirectory.ItemIndex],
        CurrentNode.Values[colLatestUpdate.ItemIndex], CurrentNode.Values[colSize.ItemIndex]]) + #13#10;
  end;

  ListText := PChar(ListTextAsStr);
  AClipboard.Open;
  AClipboard.SetTextBuf(ListText);
  AClipboard.Close;
end;

procedure TfrmMain.actCopyListToClipboardUpdate(Sender: TObject);
begin
  actCopyListToClipboard.Enabled := treeResultsList.Count > 0;
end;

procedure TfrmMain.actCopySelectedToClipboardUpdate(Sender: TObject);
begin
  actCopySelectedToClipboard.Enabled := (treeResultsList.SelectionCount > 0);
end;

end.
