unit fViewInterface;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, fModuleMaster, StdCtrls, RzLabel, ActnList, RzButton, ExtCtrls,
  RzPanel, Mask, RzEdit, RzBtnEdt, RzRadGrp,
	RzTabs, RzSpnEdt, TeeProcs, TeEngine, Chart, cxControls, cxSSheet, StStrms,
	InterfaceStreams, cxInplaceContainer, cxTL, RzLaunch, CodeSiteLogging, RzPrgres,
  ImgList, RzStatus, Menus, cxGraphics, cxCustomData, cxStyles, cxCheckBox,
  cxTextEdit, cxSpinEdit;

type
  TfrmViewInterface = class(TfrmModuleMaster)
    RzPanel1: TRzPanel;
    btnCloseTask: TRzButton;
    RzButton1: TRzButton;
    RzButton2: TRzButton;
    ActionList1: TActionList;
    actConvertFile: TAction;
    actPrevious: TAction;
    actNext: TAction;
    pgcViewInterface: TRzPageControl;
    shtOpenFile: TRzTabSheet;
		RzLabel5: TRzLabel;
		rgrpFormat: TRzRadioGroup;
    RzLabel2: TRzLabel;
    edtOpenFile: TRzButtonEdit;
    shtViewFile: TRzTabSheet;
    RzPanel2: TRzPanel;
    RzLabel3: TRzLabel;
		pgcViewTimeSeries: TRzPageControl;
    tabGraph: TRzTabSheet;
    tabSpreadsheet: TRzTabSheet;
    tabTools: TRzTabSheet;
    RzGroupBox1: TRzGroupBox;
    RzButton3: TRzButton;
    RzButton4: TRzButton;
    RzButton5: TRzButton;
    RzGroupBox2: TRzGroupBox;
    RzButton6: TRzButton;
    RzButton7: TRzButton;
    RzLabel4: TRzLabel;
    RzEdit1: TRzEdit;
    RzSpinEdit1: TRzSpinEdit;
    RzLabel6: TRzLabel;
    RzPanel3: TRzPanel;
    ssTimeSeries: TcxSpreadSheetBook;
    edtCellContents: TRzEdit;
    grdTimeSeries: TcxTreeList;
    colInclude: TcxTreeListColumn;
    colTimeSeries: TcxTreeListColumn;
    appLauncher: TRzLauncher;
    btnChart: TRzButton;
    btnAll: TRzButton;
    btnNone: TRzButton;
    prgFile: TRzProgressBar;
    RzPanel4: TRzPanel;
    RzToolButton1: TRzToolButton;
    RzToolButton2: TRzToolButton;
    ImageList1: TImageList;
    actZoom: TAction;
    actPan: TAction;
    RzPanel5: TRzPanel;
    chtTimeSeries: TChart;
    RzStatusBar1: TRzStatusBar;
    statusInfo: TRzStatusPane;
    mnuChart: TPopupMenu;
    CopytoClipboard1: TMenuItem;
    actCopyToClipboardWMF: TAction;
    actCopyToClipboardEMF: TAction;
    actCopyToClipboardBMP: TAction;
    CopytoClipboardEMF1: TMenuItem;
    CopytoClipboardBMP1: TMenuItem;
    RzToolButton3: TRzToolButton;
    RzToolButton4: TRzToolButton;
    procedure ssTimeSeriesActiveCellChanging(Sender: TcxSSBookSheet;
      const ActiveCell: TPoint; var CanSelect: Boolean);
    procedure edtOpenFileButtonClick(Sender: TObject);
    procedure actPreviousUpdate(Sender: TObject);
    procedure actNextUpdate(Sender: TObject);
    procedure btnCloseTaskClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure btnChartClick(Sender: TObject);
    procedure btnAllClick(Sender: TObject);
    procedure btnNoneClick(Sender: TObject);
    procedure actNextExecute(Sender: TObject);
    procedure actZoomExecute(Sender: TObject);
    procedure actPanExecute(Sender: TObject);
    procedure chtTimeSeriesMouseMove(Sender: TObject; Shift: TShiftState;
      X, Y: Integer);
    procedure actCopyToClipboardWMFExecute(Sender: TObject);
    procedure actCopyToClipboardEMFExecute(Sender: TObject);
    procedure actCopyToClipboardBMPExecute(Sender: TObject);
  private
		{ Private declarations }
		InterfaceFile: ISWMMStandardInterfaceFile;
		SYFFile: IXP_SYF_FileStream;
		OutputInterfaceFile: ISWMMStandardInterfaceFile;
		M11InStream: TMOUSE_PRF_M11InFileStream;
		OutputBackEndFile: TFileStream;
		OutputFile: TStAnsiTextStream;
		IsProcessing: Boolean;
	public
		{ Public declarations }
	end;

var
  frmViewInterface: TfrmViewInterface;

implementation

uses fMain, fStatus, dExtractMOUSEPRFNodes, Series, Math;

{$R *.dfm}

procedure TfrmViewInterface.edtOpenFileButtonClick(Sender: TObject);
var
	i: Integer;
	ANode: TcxTreeListNode;
	Ext: String;
	InStream, OutStream: TFileStream;
	InTextStream: TStAnsiTextStream;
	CurrentLine: String;
begin
	frmMain.dlgOpen.InitialDir := frmMain.InitRegistryDir;
	frmMain.dlgOpen.Title := 'Open Interface File';
	frmMain.dlgOpen.Filter := 'Interface Files (*.int,*.r05,*.r15,*.r20,*.r60,*.t05,*.t15,*.t20,*.t60,*.x05,*.x15,*.x20,*.x60)'+
		'|*.int;*.r05;*.r15;*.r20;*.r60;*.t05;*.t15;*.t20;*.t60;*.x05;*.x15;*.x20;*.x60'+
		'|XP-SWMM Extran Result Files (*.syf)|*.syf|MOUSE Result Files (*.prf)|*.prf'+
		'|Text Files (*.txt)|*.txt|All Files|*.*';
	case rgrpFormat.ItemIndex of
		0,1,2: begin
			frmMain.dlgOpen.DefaultExt := 'int';
			frmMain.dlgOpen.FilterIndex := 1;
		end;
		3, 6: begin
			frmMain.dlgOpen.DefaultExt := 'txt';
			frmMain.dlgOpen.FilterIndex := 4;
		end;
		4: begin
			frmMain.dlgOpen.DefaultExt := 'syf';
			frmMain.dlgOpen.FilterIndex := 2;
		end;
		5: begin
			frmMain.dlgOpen.DefaultExt := 'prf';
			frmMain.dlgOpen.FilterIndex := 3;
		end;
	end;

	if frmMain.dlgOpen.Execute then
	begin
		if Assigned(InterfaceFile) then
		begin
			InterfaceFile := nil;
			grdTimeSeries.Clear;
		end;
		Ext := ExtractFileExt(frmMain.dlgOpen.FileName);
		chtTimeSeries.Title.Text.Clear;
		chtTimeSeries.Title.Text.Add(ExtractFileName(frmMain.dlgOpen.FileName));
		if Uppercase(Ext) = '.SYF' then
		begin
			SYFFile := TXP_SYF_FileStream.Create(frmMain.dlgOpen.FileName, fmOpenRead or fmShareDenyWrite);
			//lblStep1.Caption := Format('1. Open File to Convert (Nodes: %d, Links: %d, '+
			//	'Conduits: %d, Pumps: %d, Orifices: %d, Weirs: %d)', [SYFFile.NumJunctions,
			//	SYFFile.NumLinks, SYFFile.NumConduits, SYFFile.NumPumps, SYFFile.NumOrifices,
			//	SYFFile.NumWeirs]);
			frmMain.SaveDirToRegistry;
			edtOpenFile.Text := frmMain.dlgOpen.FileName;

			// Node list
			grdTimeSeries.BeginUpdate;
			for i := 0 to SYFFile.NumLinks-1 do
			begin
				ANode := grdTimeSeries.Add;
				ANode.Values[colInclude.ItemIndex] := False;
				ANode.Values[colTimeSeries.ItemIndex] := SYFFile.Links[i].ID;
			end;
			grdTimeSeries.EndUpdate;
			grdTimeSeries.Invalidate;
		end
		else
		begin
			case rgrpFormat.ItemIndex of
				0: InterfaceFile := TSWMMStandardInterfaceFile.Create(frmMain.dlgOpen.FileName, fmOpenRead or fmShareDenyWrite, if32);
				1: InterfaceFile := TSWMMStandardInterfaceFile.Create(frmMain.dlgOpen.FileName, fmOpenRead or fmShareDenyWrite, if95);
				2: InterfaceFile := TSWMMStandardInterfaceFile.Create(frmMain.dlgOpen.FileName, fmOpenRead or fmShareDenyWrite, ifXP);
				3: InterfaceFile := TSWMMStandardInterfaceFile.Create(frmMain.dlgOpen.FileName, fmOpenRead or fmShareDenyWrite, ifText);
				4: SYFFile := TXP_SYF_FileStream.Create(frmMain.dlgOpen.FileName, fmOpenRead or fmShareDenyWrite); // XP SYF
				5: begin // Read PRF info
						frmStatus := TfrmStatus.Create(Application);
						frmStatus.edtStatus.Lines.Add('Running M11Extra, writing M11.OUT file');
						frmStatus.Show;
						Application.ProcessMessages;
						appLauncher.FileName := 'M11EXTRA';
						appLauncher.Parameters := ExtractFileName(frmMain.dlgOpen.FileName);
						appLauncher.StartDir := ExtractFileDir(frmMain.dlgOpen.FileName);
						try
							try
								appLauncher.Launch;
								frmStatus.edtStatus.Lines.Add('Running M11Extra, reading time series');
								Application.ProcessMessages;
								InStream := TFileStream.Create(ExtractFileDir(frmMain.dlgOpen.FileName)+'\M11.OUT', fmShareDenyWrite);
								OutStream := TFileStream.Create(ExtractFileDir(frmMain.dlgOpen.FileName) + '\M11.IN', fmCreate);
								try
									OutStream.CopyFrom(InStream, InStream.Size);
								finally
									OutStream.Free;
									InStream.Free;
								end;
								appLauncher.Parameters := ExtractFileName(frmMain.dlgOpen.FileName) +
									' M11.TXT';
								appLauncher.Launch;

								frmStatus.edtStatus.Lines.Add('Reading PRF time series');
								Application.ProcessMessages;

								// Read M11.OUT file into M11In stream
								M11InStream := TMOUSE_PRF_M11InFileStream.Create(ExtractFileDir(frmMain.dlgOpen.FileName)+'\M11.OUT',
									fmShareDenyWrite);

								InStream := TFileStream.Create(ExtractFileDir(frmMain.dlgOpen.FileName) + '\M11.TXT', fmShareDenyWrite);
								InTextStream := TStAnsiTextStream.Create(InStream);
								try
									InTextStream.ReadLine;
									InTextStream.ReadLine;
									InTextStream.ReadLine;
									CurrentLine := InTextStream.ReadLine;
								finally
								end;
								InTextStream.Free;
								InStream.Free;

								frmStatus.edtStatus.Lines.Add('Finished');

								dlgExtractMOUSEPRFNodes.FillGrid(M11InStream);
								if dlgExtractMOUSEPRFNodes.ShowModal = mrOK then
								begin
									appLauncher.Parameters := ExtractFileName(frmMain.dlgOpen.FileName) +
										' M11.TXT';
									appLauncher.Launch;
									frmStatus.edtStatus.Lines.Add('Extracting selected time series');
									Application.ProcessMessages;
									InterfaceFile := TMOUSE_PRF_InterfaceFileStream.Create(
										ExtractFileDir(frmMain.dlgOpen.FileName)+'\M11.TXT',
										fmOpenRead or fmShareDenyWrite);
								end
								else
								begin
									if FileExists(ExtractFileDir(frmMain.dlgOpen.FileName)+'\M11.OUT') then
										DeleteFile(ExtractFileDir(frmMain.dlgOpen.FileName)+'\M11.OUT');
									if FileExists(ExtractFileDir(frmMain.dlgOpen.FileName)+'\M11.IN') then
										DeleteFile(ExtractFileDir(frmMain.dlgOpen.FileName)+'\M11.IN');
									if FileExists(ExtractFileDir(frmMain.dlgOpen.FileName)+'\M11.TXT') then
										DeleteFile(ExtractFileDir(frmMain.dlgOpen.FileName)+'\M11.TXT');
									Exit;
								end;
							except
								on E: Exception do
									ShowMessage(E.Message);
							end;
						finally
							M11InStream.Free;
							frmStatus.Free;
						end;
					end;
				6: InterfaceFile := TMOUSE_PRF_InterfaceFileStream.Create(frmMain.dlgOpen.FileName, fmOpenRead or fmShareDenyWrite);
			end;

			frmMain.SaveDirToRegistry;
			edtOpenFile.Text := frmMain.dlgOpen.FileName;

			// Node list
			grdTimeSeries.Clear;
			grdTimeSeries.BeginUpdate;
			for i := 0 to InterfaceFile.NumInlets-1 do
			begin
				ANode := grdTimeSeries.Add;
				ANode.Values[colInclude.ItemIndex] := False;
				ANode.Values[colTimeSeries.ItemIndex] := InterfaceFile.IDs[i];
			end;
			grdTimeSeries.EndUpdate;
			grdTimeSeries.Invalidate;

		end;  // non-SYF files
	end;
end;

procedure TfrmViewInterface.actPreviousUpdate(Sender: TObject);
begin
	actPrevious.Enabled := pgcViewInterface.ActivePageIndex > 0;
end;

procedure TfrmViewInterface.actNextUpdate(Sender: TObject);
begin
	if (pgcViewInterface.ActivePage = shtOpenFile) then
		actNext.Enabled := edtOpenFile.Text <> ''
	else
		actNext.Enabled := pgcViewInterface.ActivePageIndex <
			pgcViewInterface.PageCount-1;
end;

procedure TfrmViewInterface.btnCloseTaskClick(Sender: TObject);
begin
	inherited;
	Close;
	frmViewInterface := nil;
end;

procedure TfrmViewInterface.FormCreate(Sender: TObject);
begin
	inherited;
	pgcViewInterface.ActivePage := shtOpenFile;
end;

procedure TfrmViewInterface.btnChartClick(Sender: TObject);
var
  curRow: int64;
  FileComplete: Double;
  FileSize: int64;
	i: Integer;
	ActiveNodeList: array of Integer;
	NewSeries: TFastLineSeries;
	CurrentSeries: TFastLineSeries;
begin
	inherited;

	Screen.Cursor := crHourglass;

	// Clear out the chart
	for i := chtTimeSeries.SeriesList.Count - 1 downto 0 do    // Iterate
	begin
		chtTimeSeries[i].Free;
	end;    // for

	// Create chart series
	for i := 0 to grdTimeSeries.Count - 1 do    // Iterate
	begin
		if grdTimeSeries.Nodes[i].Values[colInclude.ItemIndex] = 'True' then
		begin
			SetLength(ActiveNodeList, Length(ActiveNodeList)+1);
			ActiveNodeList[Length(ActiveNodeList)-1] := i;
			NewSeries := TFastLineSeries.Create(Self);
			NewSeries.ParentChart := chtTimeSeries;
			NewSeries.XValues.DateTime := True;
			NewSeries.Title := grdTimeSeries.Nodes[i].Values[colTimeSeries.ItemIndex];
		end;
	end;    // for

	// Fill in chart series' data
	case rgrpFormat.ItemIndex of
		4: begin
			SYFFile.MoveToStartOfFlows;
			prgFile.TotalParts := 100;
			prgFile.Show;
			FileSize := SYFFile.FileSize;
			curRow := 0;
//			ssTimeSeries.BeginUpdate;
			while not SYFFile.IsEOF do
			begin
				SYFFile.ReadFlows;
{				with ssTimeSeries.Pages[0].GetCellObject(0,curRow) do
					try
						Text := FormatDateTime('DD/MM/YYYY HH:MM:SS', SYFFile.CurrentTime);
					finally
						Free;
					end;}
				for i := 0 to Length(ActiveNodeList) - 1 do
				begin
					CurrentSeries := chtTimeSeries.SeriesList[i] as TFastLineSeries;
					CurrentSeries.AddXY(SYFFile.CurrentTime, SYFFile.Links[ActiveNodeList[i]].Flow);
					with ssTimeSeries.Pages[0].GetCellObject(1,curRow) do
						try
							Text := Format('%f', [SYFFile.Links[ActiveNodeList[i]].Flow]);
						finally
							Free;
						end;
				end;
				FileComplete := (SYFFile.FilePosition/FileSize)*100;
				prgFile.PartsComplete := Round(FileComplete);
				prgFile.Update;
				Inc(curRow);
			end;
 //			ssTimeSeries.EndUpdate;
			prgFile.PartsComplete := 0;
			prgFile.Hide;
		end
		else
		begin
			InterfaceFile.Restart;
			prgFile.TotalParts := 100;
			FileSize := InterfaceFile.Stream.Size;
			prgFile.Show;
      ssTimeSeries.BeginUpdate;
			while not InterfaceFile.IsEOF do
			begin
				InterfaceFile.ReadFlows;
				for i := 0 to Length(ActiveNodeList) - 1 do    // Iterate
				begin
					CurrentSeries := chtTimeSeries.SeriesList[i] as TFastLineSeries;
					CurrentSeries.AddXY(InterfaceFile.CurrentTime, InterfaceFile.Flows[ActiveNodeList[i]]);
				end;    // for
				FileComplete := (InterfaceFile.Stream.Position/FileSize)*100;
				prgFile.PartsComplete := Round(FileComplete);
				prgFile.Update;
			end;
      ssTimeSeries.EndUpdate;
			prgFile.PartsComplete := 0;
			prgFile.Hide;
		end;
	end;

	Screen.Cursor := crDefault;
end;

procedure TfrmViewInterface.btnAllClick(Sender: TObject);
var
	i: Integer;
begin
	inherited;
	for i := 0 to grdTimeSeries.Count - 1 do    // Iterate
	begin
		grdTimeSeries.Nodes[i].Values[colInclude.ItemIndex] := True;
	end;    // for
end;

procedure TfrmViewInterface.btnNoneClick(Sender: TObject);
var
	i: Integer;
begin
	inherited;
	for i := 0 to grdTimeSeries.Count - 1 do    // Iterate
	begin
		grdTimeSeries.Nodes[i].Values[colInclude.ItemIndex] := False;
	end;    // for
end;

procedure TfrmViewInterface.actNextExecute(Sender: TObject);
begin
	pgcViewInterface.ActivePageIndex := Min(pgcViewInterface.PageCount-1,
		pgcViewInterface.ActivePageIndex+1);
end;

procedure TfrmViewInterface.actZoomExecute(Sender: TObject);
begin
	chtTimeSeries.AllowZoom := True;
	chtTimeSeries.AllowPanning := pmNone;
end;

procedure TfrmViewInterface.actPanExecute(Sender: TObject);
begin
	chtTimeSeries.AllowZoom := False;
	chtTimeSeries.AllowPanning := pmBoth;
end;

procedure TfrmViewInterface.chtTimeSeriesMouseMove(Sender: TObject;
	Shift: TShiftState; X, Y: Integer);
var
  DateText: string;
  YValue: Double;
	XValue: Double;
	SeriesIndex: Integer;
begin
	if chtTimeSeries.SeriesList.Count = 0 then
		Exit;
	SeriesIndex := chtTimeSeries.Series[0].GetCursorValueIndex;
	XValue := chtTimeSeries.Series[0].XScreenToValue(X);
	YValue := chtTimeSeries.Series[0].YScreenToValue(Y);
	DateText := FormatDateTime('MM/DD/YYYY hh:mm:ss', XValue);
	statusInfo.Caption := Format('%s,%.5f', [DateText, YValue]);
end;

procedure TfrmViewInterface.actCopyToClipboardWMFExecute(Sender: TObject);
begin
	chtTimeSeries.CopyToClipboardMetafile(False);
end;

procedure TfrmViewInterface.actCopyToClipboardEMFExecute(Sender: TObject);
begin
	chtTimeSeries.CopyToClipboardMetafile(True);
end;

procedure TfrmViewInterface.actCopyToClipboardBMPExecute(Sender: TObject);
begin
	chtTimeSeries.CopyToClipboardBitmap;
end;

procedure TfrmViewInterface.ssTimeSeriesActiveCellChanging(
  Sender: TcxSSBookSheet; const ActiveCell: TPoint; var CanSelect: Boolean);
begin
  inherited;
  edtCellContents.Text := ssTimeSeries.ActiveSheet.GetCellObject(
    ActiveCell.X, ActiveCell.Y).Text;
end;

end.
