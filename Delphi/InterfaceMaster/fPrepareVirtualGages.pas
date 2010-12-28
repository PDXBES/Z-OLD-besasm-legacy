unit fPrepareVirtualGages;

interface

uses
	Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, fModuleMaster, StdCtrls, RzLabel, RzButton,
	ExtCtrls, TeeProcs, TeEngine, Chart, RzSpnEdt,
	RzEdit, RzDBEdit, RzDBBnEd, Mask, RzBtnEdt, RzTabs, Access2000, OleServer,
	DB, ADODB, StStrms, StStrL, RzPrgres, RzStatus, RzPanel, Series, RzRadGrp,
	CodeSiteLogging, ComObj, RzBHints, ImgList, DateUtils, Types, TransparentPanel,
	RzTrkBar, RzBckgnd, RzLstBox, Menus, AccessXP;

type
	TQuartersectionRain = class;

	TfrmPrepareVirtualGages = class(TfrmModuleMaster)
		RzLabel2: TRzLabel;
		RzLabel5: TRzLabel;
		RzLabel6: TRzLabel;
		RzLabel7: TRzLabel;
		RzLabel8: TRzLabel;
		RzLabel9: TRzLabel;
		pageGages: TRzPageControl;
		shtCharts: TRzTabSheet;
		shtSpatialView: TRzTabSheet;
		btnIdentifyStorms: TRzButton;
		RzLabel10: TRzLabel;
		btnSelectQuartersections: TRzButton;
		lblQuartersectionsSelected: TRzLabel;
		RzLabel3: TRzLabel;
		RzLabel4: TRzLabel;
		btnCreateGages: TRzButton;
		edtStartDate: TRzDateTimeEdit;
		edtEndDate: TRzDateTimeEdit;
		edtTimeStep: TRzSpinEdit;
		tabGages: TRzTabControl;
		adoBuildVirtGageConnection: TADOConnection;
		adoQSSelect: TADOTable;
		adoBuildVirtGageCommand: TADOCommand;
		adoQSSelectQS: TSmallintField;
		adoQSSelectInclude: TBooleanField;
		edtSaveFile: TRzButtonEdit;
		RzStatusBar1: TRzStatusBar;
		paneStatus: TRzStatusPane;
		paneProgress: TRzProgressBar;
		chtGages: TChart;
		RzPanel1: TRzPanel;
		rgrpFormat: TRzRadioGroup;
		RzBalloonHints1: TRzBalloonHints;
		btnCloseTask: TRzButton;
		RzPanel2: TRzPanel;
		btnFirst: TRzToolButton;
		btnReverse: TRzToolButton;
		btnPlay: TRzToolButton;
		btnForward: TRzToolButton;
		btnLast: TRzToolButton;
		ImageList1: TImageList;
    btnStop: TRzToolButton;
    timerSV: TTimer;
    pnlSpatialView: TTransPanel;
    lblSVTime: TRzLabel;
    tbarTime: TRzTrackBar;
    RzLabel12: TRzLabel;
    RzPanel3: TRzPanel;
    RzBackground1: TRzBackground;
    lblHigh: TRzLabel;
    lblLow: TRzLabel;
    rgrpSVViewStyle: TRzRadioGroup;
    RzStatusBar2: TRzStatusBar;
    paneSpatialViewStatus: TRzStatusPane;
    RzButton1: TRzButton;
    RzLabel13: TRzLabel;
    btnViewQuartersections: TRzButton;
    lstQuartersections: TRzEditListBox;
		mnuQuartersections: TPopupMenu;
    mnuQuartersectionsAdd: TMenuItem;
    mnuQuartersectionsDelete: TMenuItem;
    mnuQuartersectionsRename: TMenuItem;
    btnReloadRainfallInterface: TRzButton;
    btnSelect: TRzToolButton;
    btnZoom: TRzToolButton;
    btnHand: TRzToolButton;
    lblChartInstructions: TRzLabel;
    appAccess: TAccessApplication;
    lblNumGages: TRzLabel;
    lblGageWarning: TRzLabel;
    procedure edtEndDateExit(Sender: TObject);
		procedure btnCreateGagesClick(Sender: TObject);
		procedure btnSelectQuartersectionsClick(Sender: TObject);
		procedure FormClose(Sender: TObject; var Action: TCloseAction);
		procedure edtSaveFileButtonClick(Sender: TObject);
		procedure tabGagesChange(Sender: TObject);
		procedure FormCreate(Sender: TObject);
		procedure btnCloseTaskClick(Sender: TObject);
		procedure btnPlayClick(Sender: TObject);
		procedure btnFirstClick(Sender: TObject);
		procedure pnlSpatialViewPaint(Sender: TObject);
		procedure btnStopClick(Sender: TObject);
		procedure timerSVTimer(Sender: TObject);
		procedure btnReverseClick(Sender: TObject);
		procedure btnForwardClick(Sender: TObject);
		procedure tbarTimeChange(Sender: TObject);
		procedure rgrpSVViewStyleClick(Sender: TObject);
		procedure btnLastClick(Sender: TObject);
		procedure tbarTimeMouseDown(Sender: TObject; Button: TMouseButton;
			Shift: TShiftState; X, Y: Integer);
		procedure tbarTimeMouseUp(Sender: TObject; Button: TMouseButton;
			Shift: TShiftState; X, Y: Integer);
		procedure pnlSpatialViewMouseMove(Sender: TObject; Shift: TShiftState;
			X, Y: Integer);
    procedure pnlSpatialViewMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure tabGagesDblClick(Sender: TObject);
    procedure chtGagesMouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure chtGagesAfterDraw(Sender: TObject);
    procedure btnViewQuartersectionsClick(Sender: TObject);
    procedure mnuQuartersectionsAddClick(Sender: TObject);
    procedure mnuQuartersectionsDeleteClick(Sender: TObject);
    procedure mnuQuartersectionsRenameClick(Sender: TObject);
		procedure lstQuartersectionsItemChanged(Sender: TObject;
			Index: Integer);
		procedure btnReloadRainfallInterfaceClick(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure btnSelectClick(Sender: TObject);
    procedure btnZoomClick(Sender: TObject);
    procedure btnHandClick(Sender: TObject);
	private
		{ Private declarations }
		IsDraggingTBar: Boolean;
	public
		{ Public declarations }
		Quartersections: TStringList;
		QuartersectionGages: array of TQuartersectionRain;
		CurrentSVTime: TDateTime;
		SVActive: Boolean;
		procedure ProcessGages(Filename: String);
    procedure SetupCharts;
		procedure SetUIActive(IsActive: Boolean);
		function GetMaxRain: Double;
		function GetTotalDepth(AGage: Integer): Double;
		function GetMaxTotalDepth: Double;
		function GetFirstRainTime: TDateTime;
		function GetLastRainTime: TDateTime;
		function SVGetLeft: Integer;
		function SVGetRight: Integer;
		function SVGetTop: Integer;
		function SVGetBottom: Integer;
		function SVGetXCoord(AQSec: Integer): Integer;
		function SVGetYCoord(AQSec: Integer): Integer;
		function SVGetQSec(XCoord, YCoord: Word): Integer;
		function SVGetGageFromQSec(AQSec: Integer): Integer;
		function SVGetCurrentValue(AQSec: Integer): Double;
		procedure UpdateSpatialView;
	end;

	TRainfallRec = record
		Time: TDateTime;
		Intensity: Double;
		AccumulatedDepth: Double;
	end;

	TQuartersectionRain = class
	public
		ID: Integer;
		IsEmpty: Boolean;
		Rainfall: array of TRainfallRec;
	end;

var
	frmPrepareVirtualGages: TfrmPrepareVirtualGages;

implementation

uses fMain, RzShellDialogs, dSelectQuartersections, InterfaceStreams,
	PDXDateUtils, uMIConstants, fProgress;

{$R *.dfm}

procedure TfrmPrepareVirtualGages.btnCreateGagesClick(
	Sender: TObject);
var
	SDate: OleVariant;
	EDate: OleVariant;
	OutDir: OleVariant;
	AllInOne: OleVariant;
	Mode: OleVariant;
	Header: OleVariant;
	Delimiter: OleVariant;
	Decimals: OleVariant;
	Silent: OleVariant;
	i: Integer;
	RaingageBuilderDatabase: String;
begin
	if Length(edtSaveFile.Text) = 0 then
	begin
		ShowMessage('Enter an interface file name to save raingage data to.');
		edtSaveFile.SetFocus;
		Exit;
	end;

	{ DONE 1 -oAMM : Clear _QSelect table and reset to chosen quartersections }
	RaingageBuilderDatabase := frmMain.RegSettings.ReadString(
		PrepareVirtualGagesSection, 'RaingageBuilderDatabaseLocation',
		DefaultRaingageBuilderDatabase);
	adoBuildVirtGageConnection.ConnectionString :=
		'Provider=Microsoft.Jet.OLEDB.4.0;'+
		'Data Source='+RaingageBuilderDatabase+';'+
		'Persist Security Info=False';
	adoBuildVirtGageConnection.Open;
	adoBuildVirtGageCommand.CommandText := 'DELETE * FROM _QSSelect';
	adoBuildVirtGageCommand.Execute;
	adoQSSelect.Open;
	for i := 0 to Quartersections.Count-1 do
	begin
		adoQSSelect.Append;
		adoQSSelectQS.Value := StrToInt(Quartersections[i]);
		adoQSSelectInclude.Value := True;
		adoQSSelect.Post;
	end;
	adoQSSelect.Close;
	adoBuildVirtGageConnection.Close;

	// Run Collins' Virtgage Access application
  frmProgress.lblProgress.Caption := 'Creating quartersection files, please wait';
  frmProgress.prgProgress.PartsComplete := 0;
  frmProgress.Show;
	paneStatus.Caption := 'Creating quartersection files, please wait';
	paneStatus.Refresh;
	appAccess.OpenCurrentDatabase(RaingageBuilderDatabase, False, '');
	SDate := edtStartDate.Date;
	EDate := edtEndDate.Date;
	OutDir := ExtractFilePath(edtSaveFile.Text);
	AllInOne := False;
	Mode := 'STD';
	Header := False;
	Delimiter := ' ';
	Decimals := 3;
	Silent := True;
	Forms.Screen.Cursor := crHourglass;
	Application.ProcessMessages;
	Refresh;
	try
		try
			appAccess.Run('virtmain', SDate, EDate, OutDir, AllInOne, Mode, Header, Delimiter,
				Decimals, Silent);
		except
			on e: EOleException do
				begin
					ShowMessage('Database error; close database or check dates selected.'#13+
            e.Message);
					Forms.Screen.Cursor := crDefault;
					FreeAndNil(Quartersections);
					Exit;
				end;
		end;
	finally
		appAccess.CloseCurrentDatabase;
	end;

	{ TODO 1 -oAMM : Add interface file creation }
	ProcessGages(edtSaveFile.Text);
  lblNumGages.Show;
  lblGageWarning.Show;
  lblNumGages.Caption := Format('Number of gages for RUNOFF B1 line: %d',
    [Quartersections.Count]);

	Forms.Screen.Cursor := crDefault;
	FreeAndNil(Quartersections);
end;

procedure TfrmPrepareVirtualGages.btnSelectQuartersectionsClick(Sender: TObject);
var
	i: Integer;
begin
	inherited;
	if not Assigned(dlgSelectQuartersections) then
		dlgSelectQuartersections := TdlgSelectQuartersections.Create(Application);
	frmMain.dlgSelectDir.Title := 'Select EMGAATS directory';
	frmMain.dlgSelectDir.SelectedFolder.PathName :=	frmMain.InitRegistryDir;
	if dlgSelectQuartersections.ShowModal = mrOK then
	begin
		lblQuartersectionsSelected.Caption := Format('%d Quartersections selected',
			[Quartersections.Count]);
		btnViewQuartersections.Visible := True;
		btnViewQuartersections.Left := lblQuartersectionsSelected.BoundsRect.Right + 4;
		lstQuartersections.Left := btnViewQuartersections.BoundsRect.Right + 4;
    btnReloadRainfallInterface.Left := btnViewQuartersections.BoundsRect.Right + 4;
		frmMain.SaveSelectDirToRegistry;
		if edtStartDate.Date = 0 then
			edtStartDate.Date := Now;
		SetUIActive(True);
		btnViewQuartersections.Show;
		btnReloadRainfallInterface.Left := btnViewQuartersections.BoundsRect.Right + 4;
	end
	else
	begin
		lblQuartersectionsSelected.Caption := 'No quartersections selected';
		SetUIActive(False);
    btnViewQuartersections.Hide;
		btnReloadRainfallInterface.Left := lblQuartersectionsSelected.BoundsRect.Right + 4;
	end;
	FreeAndNil(dlgSelectQuartersections);
	lstQuartersections.Clear;
	if Assigned(Quartersections) then
		for i := 0 to Quartersections.Count-1 do
		begin
			lstQuartersections.Add(Quartersections[i]);
		end;
end;

procedure TfrmPrepareVirtualGages.FormClose(Sender: TObject;
	var Action: TCloseAction);
var
  i: Integer;
begin
	inherited;
	FreeAndNil(Quartersections);
	for i := 0 to Length(QuartersectionGages)-1 do
	begin
		Finalize(QuartersectionGages[i].Rainfall);
		QuartersectionGages[i].Free;
	end;
end;

procedure TfrmPrepareVirtualGages.ProcessGages(Filename: String);
var
	RainPath: String;
	RainFile: String;
	RaingageLine: array of String;
	RaingageFileStreams: array of TFileStream;
	RaingageTextStreams: array of TStAnsiTextStream;
	LastRaingageTime: array of TDateTime;
	NextRain: array of TRainfallRec;
	Tokens: TStringList;
	InterfaceFileStream: TFileStream;
	InterfaceTextStream: TStAnsiTextStream;
	InterfaceLine: String;
	MaxDate: TDateTime;
	InitTime, EndTime, CurrentTime: TDateTime;
	LastRecordNum: Integer;
	i, j: Integer;
	RainInterfaceFile: TCustomFortranInterfaceFileStream;
	AccumulatedDepth: array of Double;
  RainAvailable: Boolean;

	function EarliestGage: Integer;
	var
		i: Integer;
		EarliestRaingage: Integer;
		EarliestTime: TDateTime;
	begin
		EarliestTime := MaxDate;
		EarliestRaingage := 0;
		for i := 0 to Quartersections.Count do
		begin
			if (NextRain[i].Time < EarliestTime) and
				(not QuartersectionGages[i].IsEmpty) then
			begin
				EarliestRaingage := i;
				EarliestTime := NextRain[i].Time;
			end;
		end;
		Result := EarliestRaingage;
	end;

	function EarliestRainTime: TDateTime;
	var
		i: Integer;
		EarliestTime: TDateTime;
	begin
		EarliestTime := MaxDate;
		for i := 0 to Quartersections.Count-1 do
		begin
			if (NextRain[i].Time < EarliestTime) and
				(not QuartersectionGages[i].IsEmpty) then
				EarliestTime := NextRain[i].Time;
		end;
		Result := EarliestTime;
	end;

	function LatestRainTime: TDateTime;
	var
		i: Integer;
		LatestTime: TDateTime;
	begin
		LatestTime := 0;
		for i := 0 to Quartersections.Count-1 do
		begin
			if (LastRaingageTime[i] > LatestTime) and
				(not QuartersectionGages[i].IsEmpty) then
				LatestTime := LastRaingageTime[i];
		end;
		Result := LatestTime;
	end;

begin
	// Clear out Gage Data
	Finalize(QuartersectionGages);
	chtGages.SeriesList.Clear;

	// Initialize
	paneStatus.Caption := 'Creating interface file, please wait';
	paneStatus.Refresh;
  frmProgress.lblProgress.Caption := 'Creating interface file';
  frmProgress.Show;
  frmProgress.Update;
	MaxDate := EncodeDate(2500,1,1);
	RainPath := ExtractFilePath(Filename);
	SetLength(RaingageLine, Quartersections.Count);
	SetLength(RaingageFileStreams, Quartersections.Count);
	SetLength(RaingageTextStreams, Quartersections.Count);
	SetLength(QuartersectionGages, Quartersections.Count);
	SetLength(NextRain, Quartersections.Count);
	SetLength(LastRaingageTime, Quartersections.Count);
	Tokens := TStringList.Create;
	for i := 0 to QuarterSections.Count-1 do
	begin
		RainFile := RainPath+Quartersections[i]+'.dat';
		RaingageFileStreams[i] := TFileStream.Create(RainFile, fmOpenRead or fmShareDenyWrite);
		RaingageTextStreams[i] := TStAnsiTextStream.Create(RaingageFileStreams[i]);

		// Read in Last Rain Record
		RaingageTextStreams[i].SeekLine(RaingageTextStreams[i].SeekNearestLine(MaxLongInt)-1);
		RaingageLine[i] := RaingageTextStreams[i].ReadLine;
		ExtractTokensL(RaingageLine[i], ' ' , '''', False, Tokens);
		if Tokens.Count < 6 then // Trap empty files
		begin
			CodeSite.SendMsg('Reading Last Rain. Raingage '+Quartersections[i]+'. Empty Tokens!');
			LastRaingageTime[i] := 0;
		end
		else
		begin
			LastRaingageTime[i] := EncodeDate(StrToInt(Tokens[1]), StrToInt(Tokens[2]),
				StrToInt(Tokens[3]))+EncodeTime(StrToInt(Tokens[4]),StrToInt(Tokens[5]),0,0);
			RaingageTextStreams[i].SeekLine(0);
		end;

		// Initialize First Rain Record
		RaingageLine[i] := RaingageTextStreams[i].ReadLine;
		ExtractTokensL(RaingageLine[i], ' ', '''', False, Tokens);
		QuartersectionGages[i] :=  TQuartersectionRain.Create;
		QuartersectionGages[i].ID := StrToInt(Quartersections[i]);
		if Tokens.Count < 6 then // Trap empty files
		begin
			CodeSite.SendMsg('Reading First Record. Raingage '+Quartersections[i]+'. Empty Tokens!');
			NextRain[i].Time := 0;
			QuartersectionGages[i].IsEmpty := True;
		end
		else
		begin
			NextRain[i].Time := EncodeDate(StrToInt(Tokens[1]), StrToInt(Tokens[2]),
				StrToInt(Tokens[3]))+EncodeTime(StrToInt(Tokens[4]),StrToInt(Tokens[5]),0,0);
			NextRain[i].Intensity := StrToFloat(Tokens[6])*12;
		end;
	end;

	// Rifle through the files
	Forms.Screen.Cursor := crHourglass;
	if rgrpFormat.ItemIndex = 3 then
	begin
		InterfaceFileStream := TFileStream.Create(edtSaveFile.Text, fmCreate);
		InterfaceTextStream := TStAnsiTextStream.Create(InterfaceFileStream);
	end;
	InitTime := EarliestRainTime;
	EndTime := LatestRainTime;
	Finalize(AccumulatedDepth);
	SetLength(AccumulatedDepth, Quartersections.Count);

	case rgrpFormat.ItemIndex of
		0: RainInterfaceFile := TF32InterfaceFileStream.Create(edtSaveFile.Text, fmCreate);
		1: RainInterfaceFile := TF95InterfaceFileStream.Create(edtSaveFile.Text, fmCreate);
		2: RainInterfaceFile := TXPInterfaceFileStream.Create(edtSaveFile.Text, fmCreate);
	end;

	// Interface Header
	if rgrpFormat.ItemIndex <> 3 then
	begin
		RainInterfaceFile.WriteInteger(Length(QuartersectionGages));
		RainInterfaceFile.WriteInteger(MaxInt);
		for i := 0 to Quartersections.Count-1 do
			RainInterfaceFile.WriteString(LeftPadL(IntToStr(QuartersectionGages[i].ID),8));
		RainInterfaceFile.FlushRecord;
	end
	else
	begin
		InterfaceLine := 'Time';
		for i := 0 to Quartersections.Count-1 do
			InterfaceLine := InterfaceLine+','+IntToStr(QuartersectionGages[i].ID);
		InterfaceTextStream.WriteLine(InterfaceLine)
	end;

	// Timesteps
	while EarliestRainTime < MaxDate do
	begin
		CurrentTime := EarliestRainTime;
		if rgrpFormat.ItemIndex = 3 then
			InterfaceLine := FormatDateTime('MM/DD/YYYY HH:MM,', CurrentTime)
		else
		begin
			RainInterfaceFile.WriteInteger(Y2KJulDateOfDateTime(CurrentTime));
			RainInterfaceFile.WriteSingle(SecondsOfDayOfDateTime(CurrentTime));
			RainInterfaceFile.WriteSingle(300); // 300s (5min) time steps
		end;
		for i := 0 to Quartersections.Count-1 do
		begin
			if NextRain[i].Time <> CurrentTime then
			begin
				if rgrpFormat.ItemIndex = 3 then
					InterfaceLine := InterfaceLine+'0.000,'
				else
					RainInterfaceFile.WriteSingle(0.0);
				LastRecordNum := Length(QuartersectionGages[i].RainFall)-1;
				SetLength(QuartersectionGages[i].Rainfall, LastRecordNum+2);
				QuartersectionGages[i].Rainfall[LastRecordNum+1].Time := CurrentTime;
				QuartersectionGages[i].Rainfall[LastRecordNum+1].Intensity := 0.0;
				QuartersectionGages[i].Rainfall[LastRecordNum+1].AccumulatedDepth :=
					AccumulatedDepth[i];
			end
			else
			begin
				if rgrpFormat.ItemIndex = 3 then
					InterfaceLine := InterfaceLine+Format('%.3f,', [NextRain[i].Intensity])
				else
					RainInterfaceFile.WriteSingle(NextRain[i].Intensity);
				LastRecordNum := Length(QuartersectionGages[i].RainFall)-1;
				SetLength(QuartersectionGages[i].Rainfall, LastRecordNum+2);
				QuartersectionGages[i].Rainfall[LastRecordNum+1].Time := CurrentTime;
				QuartersectionGages[i].Rainfall[LastRecordNum+1].Intensity := NextRain[i].Intensity;
				AccumulatedDepth[i] := AccumulatedDepth[i] + NextRain[i].Intensity*(5/60);
				QuartersectionGages[i].Rainfall[LastRecordNum+1].AccumulatedDepth :=
					AccumulatedDepth[i];

				if not RaingageTextStreams[i].AtEndOfStream then
				begin
					RaingageLine[i] := RaingageTextStreams[i].ReadLine;
					ExtractTokensL(RaingageLine[i], ' ', '''', False, Tokens);

					NextRain[i].Time := EncodeDate(StrToInt(Tokens[1]), StrToInt(Tokens[2]),
						StrToInt(Tokens[3]))+EncodeTime(StrToInt(Tokens[4]),StrToInt(Tokens[5]),0,0);
					NextRain[i].Intensity := StrToFloat(Tokens[6])*12;
				end
				else
				begin
					NextRain[i].Time := MaxDate;
					NextRain[i].Intensity := 0.0;
				end;
			end;
		end;
		if rgrpFormat.ItemIndex = 3 then
			InterfaceTextStream.WriteLine(InterfaceLine)
		else
			RainInterfaceFile.FlushRecord;
		paneProgress.Percent := Round((CurrentTime-InitTime)/(EndTime-InitTime)*100);
		paneProgress.Refresh;
    frmProgress.prgProgress.Percent := paneProgress.Percent;
    frmProgress.lblProgress.Caption := 'current: ' +
      FormatDateTime('mmm-dd-yyyy', CurrentTime) +
      ' end: ' + FormatDateTime('mmm-dd-yyyy', EndTime);;
    frmProgress.prgProgress.Update;
	end;
	paneProgress.Percent := 0;
	paneStatus.Caption := '';
  frmProgress.Hide;

	if rgrpFormat.ItemIndex = 3 then
	begin
		InterfaceTextStream.Free;
		InterfaceFileStream.Free;
	end
	else
		RainInterfaceFile.Free;

	for i := 0 to Quartersections.Count-1 do
	begin
		RaingageTextStreams[i].Free;
		RaingageFileStreams[i].Free;
	end;
	Tokens.Free;

	RainAvailable := False;
	for i := 0 to Quartersections.Count-1 do
		if not QuartersectionGages[i].IsEmpty then
		begin
			RainAvailable := True;
			Break;
		end;

	if RainAvailable then
		SetupCharts
	else
		MessageDlg('No rain data available.  Try an earlier range.', mtInformation,
      [mbOK], 0);

	// Clean up quartersection files
	for i := 0 to Quartersections.Count-1 do
	begin
		RainFile := RainPath+Quartersections[i]+'.dat';
		DeleteFile(RainFile);
	end;
	DeleteFile(RainPath+'VGResult.txt');
	DeleteFile(RainPath+'QGinfo.txt');

	Forms.Screen.Cursor := crDefault;
end;

procedure TfrmPrepareVirtualGages.edtSaveFileButtonClick(Sender: TObject);
begin
	inherited;
	frmMain.dlgSave.Title := 'Save rainfall interface file';
	frmMain.dlgSave.InitialDir := frmMain.InitRegistryDir;
	case rgrpFormat.ItemIndex of
		0, 1, 2:
			begin
				frmMain.dlgSave.DefaultExt := 'rin';
				frmMain.dlgSave.Filter := 'Rainfall Interface files (*.rin)|*.rin|'+
					'Interface files (*.int)|*.int|Any file (*.*)|*.*';
			end;
		3:
			begin
				frmMain.dlgSave.DefaultExt := 'txt';
				frmMain.dlgSave.Filter := 'Text files (*.txt)|*.txt|Any file (*.*)|*.*';
			end;
	end;
	if frmMain.dlgSave.Execute then
	begin
		edtSaveFile.Text := frmMain.dlgSave.FileName;
		frmMain.dlgOpen.FileName := frmMain.dlgSave.FileName;
		frmMain.SaveDirToRegistry;
	end;
end;

procedure TfrmPrepareVirtualGages.tabGagesChange(Sender: TObject);
var
	chtSeries: TLineSeries;
	CurrentTab: Integer;
	SumRainfall: Double;
	AverageRainfall: Double;
	i,j: Integer;
begin
	CurrentTab := tabGages.TabIndex;
	chtGages.SeriesList.Clear;

	if CurrentTab = Length(QuartersectionGages) then
	begin
		chtSeries := TLineSeries.Create(Application);
		chtSeries.Title := 'Average';
		for i := 0 to Length(QuartersectionGages[0].Rainfall)-1 do
		begin
			SumRainfall := 0;
			for j := 0 to Length(QuartersectionGages)-1 do
				SumRainfall := SumRainfall + QuartersectionGages[j].Rainfall[i].Intensity;
			if (i > 0) and
				((QuartersectionGages[0].Rainfall[i].Time -
				QuartersectionGages[0].Rainfall[i-1].Time) > (305/86400)) then
				chtSeries.AddXY(QuartersectionGages[0].Rainfall[i-1].Time+300/86400,
					0, '', clTeeColor);
			AverageRainfall := SumRainfall / Length(QuartersectionGages);
			chtSeries.AddXY(QuartersectionGages[0].Rainfall[i].Time,
				AverageRainfall, '', clTeeColor);
		end;
	end
	else
	begin
		chtSeries := TLineSeries.Create(Application);
		chtSeries.Title := IntToStr(QuartersectionGages[CurrentTab].ID);
		for i := 0 to Length(QuartersectionGages[CurrentTab].Rainfall)-1 do
		begin
			if (i > 0) and
				((QuartersectionGages[CurrentTab].Rainfall[i].Time -
				QuartersectionGages[CurrentTab].Rainfall[i-1].Time) > (305/86400)) then
				chtSeries.AddXY(QuartersectionGages[CurrentTab].Rainfall[i-1].Time+300/86400,
					0, '', clTeeColor);
			chtSeries.AddXY(QuartersectionGages[CurrentTab].Rainfall[i].Time,
				QuartersectionGages[CurrentTab].Rainfall[i].Intensity, '', clTeeColor);
		end;
	end;
	chtSeries.Stairs := True;
	chtSeries.XValues.DateTime := True;
	chtGages.BottomAxis.DateTimeFormat := 'M/D/YYYY H:MM';
	chtGages.LeftAxis.AutomaticMaximum := False;
	chtGages.LeftAxis.Maximum := GetMaxRain;
	chtGages.AddSeries(chtSeries);
end;

procedure TfrmPrepareVirtualGages.SetUIActive(IsActive: Boolean);
begin
	edtStartDate.Enabled := IsActive;
	edtEndDate.Enabled := IsActive;
//	edtTimeStep.Enabled := IsActive;  // leave at 300s
	edtSaveFile.Enabled := IsActive;
	btnCreateGages.Enabled := IsActive;
	pageGages.Enabled := IsActive;
end;

procedure TfrmPrepareVirtualGages.FormCreate(Sender: TObject);
begin
	inherited;
	SetUIActive(False);
	IsDraggingTBar := False;
	btnReloadRainfallInterface.Left := btnSelectQuartersections.BoundsRect.Right + 4;
end;

procedure TfrmPrepareVirtualGages.btnCloseTaskClick(Sender: TObject);
begin
	inherited;
	Close;
	frmPrepareVirtualGages := nil;
end;

function TfrmPrepareVirtualGages.SVGetRight: Integer;
var
	i: Integer;
	MaxXCoord: Integer;
	QSecID: Integer;
	TestXCoord: Integer;
begin
	MaxXCoord := 0;
	for i := 0 to Length(QuartersectionGages)-1 do
	begin
		QSecID := QuartersectionGages[i].ID;
		TestXCoord := SVGetXCoord(QSecID);
		if (TestXCoord > MaxXCoord) then
			MaxXCoord := TestXCoord;
	end;
	Result := MaxXCoord;
end;

function TfrmPrepareVirtualGages.SVGetBottom: Integer;
var
	i: Integer;
	MaxYCoord: Integer;
	QSecID: Integer;
	TestYCoord: Integer;
begin
	MaxYCoord := 0;
	for i := 0 to Length(QuartersectionGages)-1 do
	begin
		QSecID := QuartersectionGages[i].ID;
		TestYCoord := SVGetYCoord(QSecID);
		if (TestYCoord > MaxYCoord) then
			MaxYCoord := TestYCoord;
	end;
	Result := MaxYCoord
end;

function TfrmPrepareVirtualGages.SVGetLeft: Integer;
var
	i: Integer;
	MinXCoord: Integer;
	QSecID: Integer;
	TestXCoord: Integer;
begin
	MinXCoord := MaxInt;
	for i := 0 to Length(QuartersectionGages)-1 do
	begin
		QSecID := QuartersectionGages[i].ID;
		TestXCoord := SVGetXCoord(QSecID);
		if (TestXCoord < MinXCoord) then
			MinXCoord := TestXCoord;
	end;
	Result := MinXCoord;
end;

function TfrmPrepareVirtualGages.SVGetTop: Integer;
var
	i: Integer;
	MinYCoord: Integer;
	QSecID: Integer;
	TestYCoord: Integer;
begin
	MinYCoord := MaxInt;
	for i := 0 to Length(QuartersectionGages)-1 do
	begin
		QSecID := QuartersectionGages[i].ID;
		TestYCoord := SVGetYCoord(QSecID);
		if (TestYCoord < MinYCoord) then
			MinYCoord := TestYCoord;
	end;
	Result := MinYCoord
end;

function TfrmPrepareVirtualGages.SVGetXCoord(AQSec: Integer): Integer;
begin
	Result := AQSec - ((AQSec div 100) * 100);
end;

function TfrmPrepareVirtualGages.SVGetYCoord(AQSec: Integer): Integer;
begin
	Result := AQSec div 100;
end;

procedure TfrmPrepareVirtualGages.btnPlayClick(Sender: TObject);
begin
	SVActive := True;
end;

procedure TfrmPrepareVirtualGages.btnFirstClick(Sender: TObject);
begin
	inherited;
	CurrentSVTime := GetFirstRainTime;
	UpdateSpatialView;
end;

function TfrmPrepareVirtualGages.GetMaxRain: Double;
var
	i, j: Integer;
	MaxRain: Double;
begin
	MaxRain := 0;
	for i := 0 to Length(QuartersectionGages)-1 do
		for j := 0 to Length(QuartersectionGages[i].Rainfall)-1 do
			if QuartersectionGages[i].Rainfall[j].Intensity > MaxRain then
				MaxRain := QuartersectionGages[i].Rainfall[j].Intensity;
	Result := MaxRain;
end;

procedure TfrmPrepareVirtualGages.pnlSpatialViewPaint(Sender: TObject);
var
	i: Integer;
	QSecWidth: Integer;
	QSecHeight: Integer;
	QSecColor: TColor;
	CurrentRainfallPos: Integer;
	ARect: TRect;
	SVTop, SVLeft: Integer;
	MaxRain: Double;
	MaxTotalDepth: Double;
	LastPos: Integer;
begin
	if Length(QuartersectionGages) = 0 then
	begin
		Exit;
	end;
	QSecWidth := pnlSpatialView.Width div (SVGetRight - SVGetLeft + 1);
	QSecHeight := pnlSpatialView.Height div (SVGetBottom - SVGetTop + 1);
	CurrentRainfallPos := -1;
	for i := 0 to Length(QuartersectionGages[0].Rainfall)-1 do
	begin
		if CompareDateTime(QuartersectionGages[0].Rainfall[i].Time, CurrentSVTime) =
			EqualsValue then
		begin
			CurrentRainfallPos := i;
			Break;
		end
		else if CompareDateTime(QuartersectionGages[0].Rainfall[i].Time, CurrentSVTime) =
			GreaterThanValue then
		begin
			LastPos := i;
			Break;
		end;
	end;

	SVTop := SVGetTop;
	SVLeft := SVGetLeft;
	MaxRain := GetMaxRain;
	MaxTotalDepth := GetMaxTotalDepth;
	for i := 0 to Length(QuartersectionGages)-1 do
	begin
		if rgrpSVViewStyle.ItemIndex = 0 then
		begin
			if CurrentRainfallPos = -1 then
				QSecColor := RGB(0, 0, 0)
			else
				QSecColor := RGB(0, 0,
					Round(QuartersectionGages[i].Rainfall[CurrentRainfallPos].Intensity/
					MaxRain*255));
		end
		else
		begin
			if CurrentRainfallPos = -1 then
				QSecColor := RGB(0, 0,
					Round(QuartersectionGages[i].Rainfall[LastPos].AccumulatedDepth/
					MaxTotalDepth*255))
			else
				QSecColor := RGB(0, 0,
					Round(QuartersectionGages[i].Rainfall[CurrentRainfallPos].AccumulatedDepth/
					MaxTotalDepth*255))
		end;
		pnlSpatialView.Canvas.Brush.Color := QSecColor;
		ARect := Rect(QSecWidth*(SVGetXCoord(QuartersectionGages[i].ID)-SVLeft),
			QSecHeight*(SVGetYCoord(QuartersectionGages[i].ID)-SVTop),
			QSecWidth*(SVGetXCoord(QuartersectionGages[i].ID)-SVLeft+1),
			QSecHeight*(SVGetYCoord(QuartersectionGages[i].ID)-SVTop+1));
		pnlSpatialView.Canvas.FillRect(ARect);
	end;
end;

function TfrmPrepareVirtualGages.GetFirstRainTime: TDateTime;
var
	i: Integer;
begin
	if Length(QuartersectionGages) = 0 then
		Result := 0
	else
		i := 0;
		while (QuartersectionGages[i].IsEmpty) and (i < (Length(QuartersectionGages)-1)) do
			Inc(i);
		Result := QuartersectionGages[i].Rainfall[0].Time;
end;

function TfrmPrepareVirtualGages.GetLastRainTime: TDateTime;
var
	LastRainfallRecNo: Integer;
	i: Integer;
begin
	if Length(QuartersectionGages) = 0 then
		Result := 0
	else
	begin
		i := 0;
		while (QuartersectionGages[i].IsEmpty) and (i < (Length(QuartersectionGages)-1)) do
			Inc(i);
		LastRainfallRecNo := Length(QuartersectionGages[i].Rainfall)-1;
		Result := QuartersectionGages[i].Rainfall[LastRainfallRecNo].Time;
	end;
end;

procedure TfrmPrepareVirtualGages.SetupCharts;
var
  i: Integer;
	Tab: TRzTabCollectionItem;
	chtSeries: TFastLineSeries;
begin
	// Set up charting
	tabGages.Tabs.Clear;
	for i := 0 to Quartersections.Count-1 do
	begin
		Tab := tabGages.Tabs.Add;
		Tab.Caption := IntToStr(QuartersectionGages[i].ID);
	end;
	// Also add in the 'Average' tab and set it active.
	Tab := tabGages.Tabs.Add;
	Tab.Caption := 'Average';
	tabGages.TabIndex := Quartersections.Count;
	tabGagesChange(Self);

	// Set up Spatial view
	CurrentSVTime := QuartersectionGages[0].Rainfall[0].Time;
	SVActive := False;
	lblHigh.Caption := Format('%.2f in/hr', [GetMaxRain]);
end;

procedure TfrmPrepareVirtualGages.UpdateSpatialView;
var
	ARect: TRect;
	FirstRainTime: TDateTime;
	LastRainTime: TDateTime;
	i: Integer;
	CurrentRainfallPos: Integer;
begin
	lblSVTime.Caption := FormatDateTime('M/D/YYYY HH:MM', CurrentSVTime);
	lblSVTime.Update;
	FirstRainTime := GetFirstRainTime;
	LastRainTime := GetLastRainTime;
	tbarTime.Position := Round((CurrentSVTime-FirstRainTime)/
		(LastRainTime-FirstRainTime)*100);

	ARect := Rect(0, 0, pnlSpatialView.Width-1, pnlSpatialView.Height-1);
	InvalidateRect(pnlSpatialView.Handle, @ARect, False);
	pnlSpatialView.Update;
end;

procedure TfrmPrepareVirtualGages.btnStopClick(Sender: TObject);
begin
	inherited;
	SVActive := False;
end;

procedure TfrmPrepareVirtualGages.timerSVTimer(Sender: TObject);
begin
	if SVActive then
	begin
		CurrentSVTime := IncMinute(CurrentSVTime, 5);
		if CompareDateTime(CurrentSVTime,GetLastRainTime) = GreaterThanValue then
			CurrentSVTime := GetFirstRainTime;
		UpdateSpatialView;
	end;
end;

procedure TfrmPrepareVirtualGages.btnReverseClick(Sender: TObject);
begin
	inherited;
	CurrentSVTime := IncMinute(CurrentSVTime, -5);
	UpdateSpatialView;
end;

procedure TfrmPrepareVirtualGages.btnForwardClick(Sender: TObject);
begin
	inherited;
	CurrentSVTime := IncMinute(CurrentSVTime, 5);
	UpdateSpatialView;
end;

procedure TfrmPrepareVirtualGages.tbarTimeChange(Sender: TObject);
var
	TrackPosition: Double;
	CurrentMinute: Word;
	NewMinute: Word;
begin
	inherited;
	if not IsDraggingTBar then
		Exit;

	TrackPosition := tbarTime.Position;
	CurrentSVTime := (TrackPosition/100)*(GetLastRainTime-GetFirstRainTime)+GetFirstRainTime;
	CurrentMinute := MinuteOf(CurrentSVTime);
	NewMinute := (CurrentMinute div 5) * 5;
	CurrentSVTime := RecodeMinute(CurrentSVTime, NewMinute);
	CurrentSVTime := RecodeSecond(CurrentSVTime, 0);
	RecodeMillisecond(CurrentSVTime, 0);
	UpdateSpatialView;
end;

function TfrmPrepareVirtualGages.GetTotalDepth(AGage: Integer): Double;
var
	LastPos: Integer;
begin
	LastPos := Length(QuartersectionGages[AGage].Rainfall)-1;
	Result := QuartersectionGages[AGage].Rainfall[LastPos].AccumulatedDepth;
end;

function TfrmPrepareVirtualGages.GetMaxTotalDepth: Double;
var
	i: Integer;
	MaxDepth: Double;
	CurrentDepth: Double;
begin
	MaxDepth := 0;
	for i := 0 to Length(QuartersectionGages)-1 do
	begin
		CurrentDepth := GetTotalDepth(i);
		if CurrentDepth > MaxDepth then
			MaxDepth := CurrentDepth;
	end;
	Result := MaxDepth;
end;

procedure TfrmPrepareVirtualGages.rgrpSVViewStyleClick(Sender: TObject);
begin
	inherited;
	case rgrpSVViewStyle.ItemIndex of
		0:
			begin
				lblHigh.Caption := Format('%.2f in/hr', [GetMaxRain]);
				lblLow.Caption := '0.00 in/hr';
			end;
		1:
			begin
				lblHigh.Caption := Format('%.2f in', [GetMaxTotalDepth]);
				lblLow.Caption := '0.00 in';
			end;
	end;
	UpdateSpatialView;
end;

procedure TfrmPrepareVirtualGages.btnLastClick(Sender: TObject);
begin
	inherited;
	CurrentSVTime := GetLastRainTime;
	UpdateSpatialView;
end;

procedure TfrmPrepareVirtualGages.tbarTimeMouseDown(Sender: TObject;
  Button: TMouseButton; Shift: TShiftState; X, Y: Integer);
begin
  inherited;
  IsDraggingTBar := True;
end;

procedure TfrmPrepareVirtualGages.tbarTimeMouseUp(Sender: TObject;
  Button: TMouseButton; Shift: TShiftState; X, Y: Integer);
begin
	inherited;
	IsDraggingTBar := False;
end;

procedure TfrmPrepareVirtualGages.pnlSpatialViewMouseMove(Sender: TObject;
	Shift: TShiftState; X, Y: Integer);
var
	QSec: Integer;
begin
	inherited;
	QSec := SVGetQSec(X,Y);
	case rgrpSVViewStyle.ItemIndex of
		0: paneSpatialViewStatus.Caption := Format('%d: %.2f in/hr; click on map to see intensity chart',
			[QSec, SVGetCurrentValue(QSec)]);
		1: paneSpatialViewStatus.Caption := Format('%d: %.2f in; click on map to see intensity chart',
			[QSec, SVGetCurrentValue(QSec)]);
  end;
end;

function TfrmPrepareVirtualGages.SVGetQSec(XCoord, YCoord: Word): Integer;
var
	QSecWidth, QSecHeight: Word;
	X, Y: Word;
begin
	QSecWidth := pnlSpatialView.Width div (SVGetRight - SVGetLeft + 1);
	QSecHeight := pnlSpatialView.Height div (SVGetBottom - SVGetTop + 1);
	if (QSecWidth = 0) or (QSecHeight = 0) then
		Result := 0
	else
	begin
		X := XCoord div QSecWidth;
		X := X + SVGetLeft;
		Y := YCoord div QSecHeight;
		Y := Y + SVGetTop;
		Result := Y*100 + X;
  end;
end;

function TfrmPrepareVirtualGages.SVGetGageFromQSec(AQSec: Integer): Integer;
var
	i: Integer;
begin
  Result := 0;
	for i := 0 to Length(QuartersectionGages)-1 do
		if QuartersectionGages[i].ID = AQSec then
		begin
			Result := i;
			Break;
		end;
end;

function TfrmPrepareVirtualGages.SVGetCurrentValue(AQSec: Integer): Double;
var
	i: Integer;
	Gage: Integer;
	CurrentRainfallPos: Integer;
	LastPos: Integer;
begin
	if Length(QuartersectionGages) = 0 then
  begin
		Result := 0;
		Exit;
	end;

	Gage := SVGetGageFromQSec(AQSec);
	CurrentRainfallPos := -1;
	for i := 0 to Length(QuartersectionGages[Gage].Rainfall)-1 do
	begin
		if CompareDateTime(QuartersectionGages[Gage].Rainfall[i].Time, CurrentSVTime) =
			EqualsValue then
		begin
			CurrentRainfallPos := i;
			Break;
		end
		else if CompareDateTime(QuartersectionGages[Gage].Rainfall[i].Time, CurrentSVTime) =
			GreaterThanValue then
		begin
			LastPos := i;
			Break;
		end;
	end;

	if CurrentRainfallPos > -1 then
	begin
		if rgrpSVViewStyle.ItemIndex = 0 then
			Result := QuartersectionGages[Gage].Rainfall[CurrentRainfallPos].Intensity
		else
			Result := QuartersectionGages[Gage].Rainfall[CurrentRainfallPos].AccumulatedDepth;
	end
	else
	begin
		if rgrpSVViewStyle.ItemIndex = 0 then
			Result := 0
		else
			Result := QuartersectionGages[Gage].Rainfall[LastPos].AccumulatedDepth;
	end;
end;

procedure TfrmPrepareVirtualGages.pnlSpatialViewMouseUp(Sender: TObject;
	Button: TMouseButton; Shift: TShiftState; X, Y: Integer);
var
	QSec: Integer;
	i: Integer;
	QSecTab: Integer;
begin
	inherited;
	if Length(QuartersectionGages) = 0 then
		Exit;

	QSec := SVGetQSec(X, Y);
	for i := 0 to Length(QuartersectionGages)-1 do
		if QuartersectionGages[i].ID = QSec then
		begin
			QSecTab := i;
			Break;
		end;
	tabGages.TabIndex := QSecTab;
	pageGages.ActivePage := shtCharts;
end;

procedure TfrmPrepareVirtualGages.tabGagesDblClick(Sender: TObject);
begin
	inherited;
	tabGages.TabIndex := tabGages.Tabs.Count-1;
end;

procedure TfrmPrepareVirtualGages.chtGagesMouseUp(Sender: TObject;
	Button: TMouseButton; Shift: TShiftState; X, Y: Integer);
var
	CurrentMinute: Integer;
	NewMinute: Integer;
begin
	inherited;
	if not btnSelect.Down then
    Exit;
	CurrentSVTime := chtGages.Series[0].XScreenToValue(X);
	CurrentMinute := MinuteOf(CurrentSVTime);
	NewMinute := (CurrentMinute div 5) * 5;
	CurrentSVTime := EncodeDate(YearOf(CurrentSVTime), MonthOf(CurrentSVTime),
		DayOf(CurrentSVTime))+EncodeTime(HourOf(CurrentSVTime), NewMinute, 0, 0);
	UpdateSpatialView;
	pageGages.ActivePage := shtSpatialView;
end;

procedure TfrmPrepareVirtualGages.chtGagesAfterDraw(Sender: TObject);
var
	ChartRect: TRect;
	CurrentXCoord: Integer;
begin
	if chtGages.SeriesList.Count = 0 then
		Exit;
	ChartRect := chtGages.ChartRect;
	CurrentXCoord := chtGages.Series[0].CalcXPosValue(CurrentSVTime);
	chtGages.Canvas.MoveTo(CurrentXCoord, ChartRect.Bottom);
	chtGages.Canvas.LineTo(CurrentXCoord, ChartRect.Top);
end;

procedure TfrmPrepareVirtualGages.btnViewQuartersectionsClick(
  Sender: TObject);
begin
	lstQuartersections.Visible := not lstQuartersections.Visible;

	if lstQuartersections.Visible then
	begin
		btnViewQuartersections.Caption := 'Hide Quartersection List';
		btnReloadRainfallInterface.Left := lstQuartersections.BoundsRect.Right + 4;
	end
	else
	begin
		btnViewQuartersections.Caption := 'View Quartersection List';
		btnReloadRainfallInterface.Left := btnViewQuartersections.BoundsRect.Right + 4;
	end;
end;

procedure TfrmPrepareVirtualGages.mnuQuartersectionsAddClick(
	Sender: TObject);
var
	AddedIndex: Integer;
begin
	AddedIndex := lstQuartersections.Add('0000');
  Quartersections.Add('0000');
	lstQuartersections.ClearSelection;
	lstQuartersections.Selected[AddedIndex] := True;
	lstQuartersections.ShowEditor;
end;

procedure TfrmPrepareVirtualGages.mnuQuartersectionsDeleteClick(
	Sender: TObject);
var
	i: Integer;
begin
	lstQuartersections.DeleteSelected;
	Quartersections.Clear;
	for i := 0 to lstQuartersections.Count-1 do
		Quartersections.Add(lstQuartersections.Items[i]);
	lblQuartersectionsSelected.Caption := Format('%d Quartersections selected',
		[Quartersections.Count]);
end;

procedure TfrmPrepareVirtualGages.mnuQuartersectionsRenameClick(
	Sender: TObject);
begin
	lstQuartersections.ShowEditor;
end;

procedure TfrmPrepareVirtualGages.lstQuartersectionsItemChanged(
	Sender: TObject; Index: Integer);
begin
	Quartersections[Index] := lstQuartersections.Items[Index];
	lblQuartersectionsSelected.Caption := Format('%d Quartersections selected',
		[Quartersections.Count]);
end;

procedure TfrmPrepareVirtualGages.btnReloadRainfallInterfaceClick(
	Sender: TObject);
var
	RainInterfaceFile: TCustomFortranInterfaceFileStream;
	FileSize: Double;
	NumRaingages: Integer;
	Dummy: Integer;
	RaingageID: String;
	i: Integer;
	JulDay: Integer;
	TimeOfDay: Single;
	TimeStep: Single;
	LastRainfallRec: Integer;
begin
	frmMain.dlgOpen.Title := 'Reload rainfall interface file';
	frmMain.dlgOpen.InitialDir := frmMain.InitRegistryDir;
	if frmMain.dlgOpen.Execute then
	begin
		// Assume F95 interfaces for now
		RainInterfaceFile := TF95InterfaceFileStream.Create(frmMain.dlgOpen.FileName,
			fmOpenRead or fmShareDenyWrite);
		NumRaingages := RainInterfaceFile.ReadInteger;
		Dummy := RainInterfaceFile.ReadInteger;

		for i := 0 to Length(QuartersectionGages)-1 do
			QuartersectionGages[i].Free;
		Finalize(QuartersectionGages);
		SetLength(QuartersectionGages, NumRaingages);
		if not Assigned(Quartersections) then
			Quartersections := TStringList.Create
		else
			Quartersections.Clear;
		for i := 1 to NumRaingages do
		begin
			Quartersections.Add(Trim(RainInterfaceFile.ReadString(8)));
			QuartersectionGages[i-1] := TQuartersectionRain.Create;
			QuartersectionGages[i-1].ID := StrToInt(Quartersections[i-1]);
			QuartersectionGages[i-1].IsEmpty := False;
		end;

		while not RainInterfaceFile.IsEOF do
		begin
			JulDay := RainInterfaceFile.ReadInteger;
			TimeOfDay := RainInterfaceFile.ReadSingle;
			TimeStep := RainInterfaceFile.ReadSingle;
			for i := 1 to NumRaingages do
			begin
				SetLength(QuartersectionGages[i-1].Rainfall,
					Length(QuartersectionGages[i-1].Rainfall)+1);
				LastRainfallRec := Length(QuartersectionGages[i-1].Rainfall)-1;
				QuartersectionGages[i-1].Rainfall[LastRainfallRec].Time :=
					DateTimeOfJulDate(JulDay, TimeOfDay);
				QuartersectionGages[i-1].Rainfall[LastRainfallRec].Intensity :=
					RainInterfaceFile.ReadSingle;
				if LastRainfallRec > 0 then
					QuartersectionGages[i-1].Rainfall[LastRainfallRec].AccumulatedDepth :=
						QuartersectionGages[i-1].Rainfall[LastRainfallRec-1].AccumulatedDepth +
						QuartersectionGages[i-1].Rainfall[LastRainfallRec].Intensity*TimeStep/3600
				else
					QuartersectionGages[i-1].Rainfall[LastRainfallRec].AccumulatedDepth := 0;
			end;
			FileSize := RainInterfaceFile.Stream.Size;
			paneStatus.Caption := 'Reading ' + FormatDateTime('mm/dd/yyyy hh:mm', DateTimeOfJulDate(JulDay, TimeOfDay));
			paneProgress.Percent := Round((RainInterfaceFile.Stream.Position / FileSize)*100);
			Application.ProcessMessages;
		end;
		SetupCharts;
		SetUIActive(True);
		edtStartDate.Date := GetFirstRainTime;
		edtEndDate.Date := GetLastRainTime;
    paneStatus.Caption := '';
		paneProgress.Percent := 0;
	end;
end;

procedure TfrmPrepareVirtualGages.FormShow(Sender: TObject);
begin
	inherited;
//	TeeZoomMouseButton := mbLeft;
//	TeeScrollMouseButton := mbLeft;
  edtStartDate.Date := IncDay(Now, -1);
  edtEndDate.Date := Now;
end;

procedure TfrmPrepareVirtualGages.btnSelectClick(Sender: TObject);
begin
	inherited;
	chtGages.AllowZoom := False;
	chtGages.AllowPanning := pmNone;
	lblChartInstructions.Caption := 'Click on the chart to switch to spatial view at corresponding time';
end;

procedure TfrmPrepareVirtualGages.btnZoomClick(Sender: TObject);
begin
	inherited;
	chtGages.AllowZoom := True;
	chtGages.AllowPanning := pmNone;
	lblChartInstructions.Caption := 'Drag in chart from top-left to bottom-right to zoom in; drag in other direction to reset';
end;

procedure TfrmPrepareVirtualGages.btnHandClick(Sender: TObject);
begin
	inherited;
	chtGages.AllowZoom := False;
	chtGages.AllowPanning := pmBoth;
	lblChartInstructions.Caption := 'Drag in chart to pan';
end;

procedure TfrmPrepareVirtualGages.edtEndDateExit(Sender: TObject);
begin
  inherited;
	if edtEndDate.Date < IncDay(edtStartDate.Date, 1) then
		edtEndDate.Date := IncDay(edtStartDate.Date, 1);
end;

end.
