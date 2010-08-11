unit fConvertInterface;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, fModuleMaster, RzPrgres, RzButton,
	StdCtrls, RzLabel,
	InterfaceStreams, StStrms, ActnList, Types, ExtCtrls, RzPanel, CodeSiteLogging,
  RzEdit, RzCmboBx, RzRadChk, RzSpnEdt, Mask, RzTabs, RzBtnEdt, RzRadGrp,
	RzLaunch, cxGraphics, cxCustomData, cxStyles, cxTL, cxTextEdit, cxCheckBox,
  cxFilter, cxData, cxDataStorage, cxEdit, DB, cxDBData, cxGridLevel, cxClasses,
  cxControls, cxGridCustomView, cxGridCustomTableView, cxGridTableView,
  cxGridDBTableView, cxGrid, cxInplaceContainer, cxCalendar, cxSpinEdit;

type
  TIMConvertOptions = class
  end;

  TIMMOUSEConvertOptions = class(TIMConvertOptions)
  private
    fMOUSEDBName: String;
    fEventType: String;
    fUnits: String;
    fFactor: Double;
    fOffset: Double;
    fGenerateMOUSETimeSeries: Boolean;
    fGenerateMOUSEBSF: Boolean;
    fGenerateMOUSEBSFName: String;
  protected
    function GetOption(const Name: String): Variant;
    procedure SetOption(const Name: String; Value: Variant);
  public
    property MOUSEDBName: String read fMOUSEDBName write fMOUSEDBName;
    property EventType: String read fEventType write fEventType;
    property Units: String read fUnits write fUnits;
    property Factor: Double read fFactor write fFactor;
    property Offset: Double read fOffset write fOffset;
    property GenerateMOUSETimeSeries: Boolean read fGenerateMOUSETimeSeries
      write fGenerateMOUSETimeSeries;
    property GenerateMOUSEBSF: Boolean read fGenerateMOUSEBSF
      write fGenerateMOUSEBSF;
    property GenerateMOUSEBSFName: String read fGenerateMOUSEBSFName
      write fGenerateMOUSEBSFName;
    property Options[const Name: String]: Variant read GetOption
      write SetOption; default;
  end;

  TIMSWMMConvertOptions = class(TIMConvertOptions)
  private
    fTitles: array[1..4] of String;
    fSourceBlock: String;
    fArea: Double;
    fFlowMultiplier: Double;
    fSWMMFormat: TSWMMInterfaceFormat;
    fAlphaNumericIDs: Boolean;
    fSkipInterval: Integer;
  protected
    function GetTitles(Index: Integer): String;
    procedure SetTitles(Index: Integer; AString: String);
    function GetOption(const Name: String): Variant;
    procedure SetOption(const Name: String; Value: Variant);
  public
    property Titles[Index: Integer]: String read GetTitles
      write SetTitles;
    property SourceBlock: String read fSourceBlock write fSourceBlock;
    property Area: Double read fArea write fArea;
    property FlowMultiplier: Double read fFlowMultiplier
      write fFlowMultiplier;
    property SWMMFormat: TSWMMInterfaceFormat read fSWMMFormat
      write fSWMMFormat;
    property AlphaNumericIDs: Boolean read fAlphaNumericIDs
      write fAlphaNumericIDs;
    property SkipInterval: Integer read fSkipInterval write fSkipInterval;
    property Options[const Name: String]: Variant read GetOption
      write SetOption; default;
  end;

  TIMSYFConvertOptions = class(TIMConvertOptions)
  private
    fNodeDepths: Boolean;
    fNodeInverts: Boolean;
    fLinkFlows: Boolean;
    fLinkDepths: Boolean;
    fLinkVelocities: Boolean;
  protected
    function GetOption(const Name: String): Variant;
    procedure SetOption(const Name: String; Value: Variant);
  public
    property NodeDepths: Boolean read fNodeDepths write fNodeDepths;
    property NodeInverts: Boolean read fNodeInverts write fNodeInverts;
    property LinkFlows: Boolean read fLinkFlows write fLinkFlows;
    property LinkDepths: Boolean read fLinkDepths write fLinkDepths;
    property LinkVelocities: Boolean read fLinkVelocities write fLinkVelocities;
    property Options[const Name: String]: Variant read GetOption
      write SetOption; default;
  end;

  TIMConvertNodeOptions = class
  private
    fInclude: Boolean;
    fOriginalID: String;
    fNewTimeSeriesID: String;
    fAssignToNode: String;
    fReplaceWith: Double;
    fMultiplier: Double;
  protected
    function GetOption(const Name: String): Variant;
    procedure SetOption(const Name: String; Value: Variant);
  public
    property Include: Boolean read fInclude write fInclude;
    property OriginalID: String read fOriginalID write fOriginalID;
    property NewTimeSeriesID: String read fNewTimeSeriesID
      write fNewTimeSeriesID;
    property AssignToNode: String read fAssignToNode write fAssignToNode;
    property ReplaceWith: Double read fReplaceWith write fReplaceWith;
    property Multiplier: Double read fMultiplier write fMultiplier;
    property Options[const Name: String]: Variant read GetOption
      write SetOption; default;
  end;

  TIMConvertParams = class
  private
    fSourceFormat: TInterfaceFormat;
    fSourceName: TFileName;
    fSourceExtractStartDateTime: TDateTime;
    fSourceExtractEndDateTime: TDateTime;
    fDestFormat: TInterfaceFormat;
    fDestName: TFileName;
    fConvertOptions: TIMConvertOptions;
    fConvertNodeList: TStringList;
  protected
    function GetConvertNodeOptions(Index: Integer): TIMConvertNodeOptions;
  public
    constructor Create(ASourceFormat: TInterfaceFormat);
    destructor Destroy;

    function AddConvertNode: TIMConvertNodeOptions;

    property SourceFormat: TInterfaceFormat read fSourceFormat;
    property SourceName: TFileName read fSourceName write fSourceName;
    property SourceExtractStartDateTime: TDateTime read fSourceExtractStartDateTime
      write fSourceExtractStartDateTime;
    property SourceExtractEndDateTime: TDateTime read fSourceExtractEndDateTime
      write fSourceExtractEndDateTime;
    property DestFormat: TInterfaceFormat read fDestFormat write fDestFormat;
    property DestName: TFileName read fDestName write fDestName;
    property ConvertOptions: TIMConvertOptions read fConvertOptions;
    property ConvertNode[Index: Integer]: TIMConvertNodeOptions read
      GetConvertNodeOptions;
  end;

  TfrmConvertInterface = class(TfrmModuleMaster)
    ActionList1: TActionList;
    actExcludeAll: TAction;
    actIncludeAll: TAction;
    actToggleRange: TAction;
    actSaveModifiers: TAction;
    actLoadModifiers: TAction;
    actConvertFile: TAction;
    actStopConversion: TAction;
		actSpecifyExtractDates: TAction;
    pgcConvertInterfaceWizard: TRzPageControl;
    shtOpenFile: TRzTabSheet;
    shtSetOptions: TRzTabSheet;
    shtSelectNodes: TRzTabSheet;
    shtSaveFile: TRzTabSheet;
    shtProcessFile: TRzTabSheet;
    RzLabel2: TRzLabel;
    edtOpenFile: TRzButtonEdit;
    rgrpFormat: TRzRadioGroup;
    btnExcludeAll: TRzButton;
    btnIncludeAll: TRzButton;
    btnToggleRange: TRzButton;
    btnSaveModifiers: TRzButton;
    btnLoadModifiers: TRzButton;
    edtSaveFile: TRzButtonEdit;
    prgConvert: TRzProgressBar;
    lblTotalProgress: TRzLabel;
    prgFile: TRzProgressBar;
    lblFileProgress: TRzLabel;
    btnConvertFile: TRzButton;
    btnStopConversion: TRzButton;
    RzLabel5: TRzLabel;
    RzLabel6: TRzLabel;
    RzLabel7: TRzLabel;
    RzLabel8: TRzLabel;
    RzLabel9: TRzLabel;
    RzPanel1: TRzPanel;
    btnCloseTask: TRzButton;
    RzButton1: TRzButton;
    RzButton2: TRzButton;
    lblSaveConvertedFile: TRzLabel;
    actPrevious: TAction;
    actNext: TAction;
    tabConvertType: TRzPageControl;
    shtMOUSEOptions: TRzTabSheet;
    RzLabel11: TRzLabel;
    RzLabel14: TRzLabel;
    RzLabel25: TRzLabel;
    lblFactor: TRzLabel;
    lblOffset: TRzLabel;
    RzLabel28: TRzLabel;
    edtMOUSEDB: TRzEdit;
    edtEventType: TRzMRUComboBox;
    edtFactor: TRzSpinEdit;
    edtOffset: TRzSpinEdit;
    chkGenerateMouseTimeSeries: TRzCheckBox;
    chkGenerateMouseBoundaryConnectors: TRzCheckBox;
    edtBSFName: TRzEdit;
    btnSpecifyExtractDates: TRzBitBtn;
    edtUnits: TRzComboBox;
    shtSWMMOptions: TRzTabSheet;
    RzLabel29: TRzLabel;
    RzLabel30: TRzLabel;
    RzLabel31: TRzLabel;
    RzLabel32: TRzLabel;
    RzLabel33: TRzLabel;
    RzLabel34: TRzLabel;
    RzLabel35: TRzLabel;
    chkSWMMAlphanumeric: TRzCheckBox;
    edtSWMMTitle1: TRzEdit;
    edtSWMMTitle2: TRzEdit;
    edtSWMMTitle3: TRzEdit;
    edtSWMMTitle4: TRzEdit;
    edtSWMMSourceBlock: TRzEdit;
    edtSWMMArea: TRzSpinEdit;
    edtSWMMFlowMultiplier: TRzSpinEdit;
    shtSYFTextOptions: TRzTabSheet;
    RzCheckBox1: TRzCheckBox;
    RzCheckBox2: TRzCheckBox;
    RzCheckBox3: TRzCheckBox;
    RzCheckBox4: TRzCheckBox;
    RzCheckBox5: TRzCheckBox;
    edtSYFStartDate: TRzDateTimeEdit;
    edtSYFStartTime: TRzDateTimeEdit;
    chkLimitExtractPeriod: TRzCheckBox;
    edtFromDate: TRzDateTimeEdit;
    edtFromTime: TRzDateTimeEdit;
    lblFromDate: TRzLabel;
    lblToDate: TRzLabel;
    edtToDate: TRzDateTimeEdit;
    edtToTime: TRzDateTimeEdit;
    lblFromTime: TRzLabel;
    lblToTime: TRzLabel;
    btnSetGlobalMultiplier: TRzButton;
    appLauncher: TRzLauncher;
    rgrpToSWMMFormat: TRzRadioGroup;
    rgrpConvertFormat: TRzRadioGroup;
    RzLabel12: TRzLabel;
    RzLabel3: TRzLabel;
    edtSkipInterval: TRzSpinEdit;
    RzLabel4: TRzLabel;
    RzLabel13: TRzLabel;
    treeNodes: TcxTreeList;
    cxColAssignToID: TcxTreeListColumn;
    cxColInclude: TcxTreeListColumn;
    cxColModifier: TcxTreeListColumn;
    cxColMultiplier: TcxTreeListColumn;
    cxColNewID: TcxTreeListColumn;
    cxColOriginalID: TcxTreeListColumn;
    cxColRecord: TcxTreeListColumn;
    procedure treeNodesCustomDrawFooterCell(Sender: TObject; ACanvas: TcxCanvas;
      AViewInfo: TcxTreeListFooterItemViewInfo; var ADone: Boolean);
    procedure treeNodesFocusedColumnChanged(Sender: TObject; APrevFocusedColumn,
      AFocusedColumn: TcxTreeListColumn);
    procedure cxColIncludePropertiesEditValueChanged(Sender: TObject);
		procedure btnConvertFileClick(Sender: TObject);
		procedure actExcludeAllUpdate(Sender: TObject);
		procedure actStopConversionUpdate(Sender: TObject);
		procedure FormCreate(Sender: TObject);
		procedure btnStopConversionClick(Sender: TObject);
		procedure edtEventTypeChange(Sender: TObject);
		procedure edtUnitsChange(Sender: TObject);
		procedure actToggleRangeExecute(Sender: TObject);
		procedure actExcludeAllExecute(Sender: TObject);
		procedure actIncludeAllExecute(Sender: TObject);
		procedure actSaveModifiersExecute(Sender: TObject);
		procedure actLoadModifiersExecute(Sender: TObject);
		procedure tabConvertTypeChange(Sender: TObject);
		procedure actSpecifyExtractDatesExecute(Sender: TObject);
    procedure edtOpenFileButtonClick(Sender: TObject);
		procedure edtSaveFileButtonClick(Sender: TObject);
    procedure edtOpenFileChange(Sender: TObject);
    procedure btnCloseTaskClick(Sender: TObject);
		procedure actPreviousExecute(Sender: TObject);
    procedure actNextExecute(Sender: TObject);
    procedure actPreviousUpdate(Sender: TObject);
    procedure actNextUpdate(Sender: TObject);
    procedure chkLimitExtractPeriodClick(Sender: TObject);
    procedure btnSetGlobalMultiplierClick(Sender: TObject);
    procedure rgrpConvertFormatClick(Sender: TObject);
	private
		{ Private declarations }
		NumIncluded: Integer;
	public
		{ Public declarations }
		InterfaceFile: ISWMMStandardInterfaceFile;
		SYFFile: IXP_SYF_FileStream;
		OutputInterfaceFile: ISWMMStandardInterfaceFile;
    M11InStream: TMOUSE_PRF_M11InFileStream;
		OutputBackEndFile: TFileStream;
		OutputFile: TStAnsiTextStream;
		IsProcessing: Boolean;
	end;

var
	frmConvertInterface: TfrmConvertInterface;

implementation

uses fMain, DateUtils, PDXDateUtils, Math, dToggleRange,
	dSpecifyExtractDates, fStatus, dExtractMOUSEPRFNodes, StdConvs, ConvUtils;

{$R *.dfm}

procedure TfrmConvertInterface.btnConvertFileClick(Sender: TObject);
var
  UserSpecStartTime: TDateTime;
  SYFFileStartTime: TDateTime;
	HeaderNodesList: TStringList;
	FileComplete: Double;
	i: Integer;
	IncludeCol: Integer;
	OriginalIDCol: Integer;
	NewIDCol: Integer;
	AssignToIDCol: Integer;
	ModifierCol: Integer;
	MultiplierCol: Integer;
	CurrentNode: TcxTreeListNode;
	AdjustedFlow: Double;
	FirstTimeStep: Boolean;
//	SkippedTimeStep: Boolean;
	PrevDateTime: array of TDateTime;
	PrevDelta: array of Double;
	PrevDateTimeWithDelta: TDateTime;
	RoundedCurrentTime: TDateTime;
	ZeroDateTime: TDateTime;
	NodesToProcess: Integer;
	AllNodesStr: String;
	FlowStr: String;
	ExtractNodesList: TStringList;
	FileSize: Int64;
	WriteNodeStr: String;
	ABuf: Single;
	AIntPtr: ^Integer;
	AInt: Integer;
	BeginLimitTime: TDateTime;
	EndLimitTime: TDateTime;
	TempOutputFiles: array of TFileStream;
	TempOutputFileNames: array of String;
	TempOutputTextFiles: array of TStAnsiTextStream;
	PrevFlows: array of Double;
	SkippedTimeStep: array of Boolean;
	TempOutputCounter: Integer;
	ANodeModifier: TNodeModifier;
	CurrentStep, SkipInterval: Int64;
begin
	inherited;
	btnStopConversion.Enabled := True;
	btnConvertFile.Enabled := False;
	BeginLimitTime := edtFromDate.Date + edtFromTime.Time;
	EndLimitTime := edtToDate.Date + edtToTime.Time;

	if tabConvertType.ActivePage = shtMOUSEOptions then // To-MOUSE Conversion
	begin

		OutputBackEndFile := TFileStream.Create(edtSaveFile.Text, fmCreate);
		OutputFile := TStAnsiTextStream.Create(OutputBackEndFile);
		FileSize := InterfaceFile.Stream.Size;

		try
			IsProcessing := True;
			Screen.Cursor := crHourglass;

			IncludeCol := cxColInclude.ItemIndex;
			OriginalIDCol := cxColOriginalID.ItemIndex;
			NewIDCol := cxColNewID.ItemIndex;
			AssignToIDCol := cxColAssignToID.ItemIndex;
			ModifierCol := cxColModifier.ItemIndex;
			MultiplierCol := cxColMultiplier.ItemIndex;

			// Count actual number of nodes to process
			NodesToProcess := 0;
			for i := 0 to treeNodes.Count-1 do
				if treeNodes.Items[i].Values[IncludeCol] = 'True' then
					Inc(NodesToProcess);
			prgConvert.TotalParts := NodesToProcess + InterfaceFile.Stream.Size div (1024*64) + NodesToProcess*2;
			prgConvert.PartsComplete := 0;
			SetLength(TempOutputFiles, NodesToProcess);
			SetLength(TempOutputTextFiles, NodesToProcess);
			SetLength(TempOutputFileNames, NodesToProcess);
			SetLength(PrevFlows, NodesToProcess);
			SetLength(PrevDateTime, NodesToProcess);
			SetLength(PrevDelta, NodesToProcess);
			SetLength(SkippedTimeStep, NodesToProcess);

			// Generate temporary output files to combine at the end of the process
			lblTotalProgress.Caption := 'Creating temporary files';
			for i := 0 to NodesToProcess-1 do
			begin
				TempOutputFileNames[i] := Format('%s\IMTemp%.5d.tmp',
					[ExtractFileDir(edtSaveFile.Text),i]);
				TempOutputFiles[i] := TFileStream.Create(TempOutputFileNames[i], fmCreate);
				TempOutputTextFiles[i] := TStAnsiTextStream.Create(TempOutputFiles[i]);
				prgConvert.PartsComplete := prgConvert.PartsComplete + 1;
			end;

			lblTotalProgress.Caption := 'Writing to temporary M11 files';
			if chkGenerateMouseTimeSeries.Checked then
			begin
				InterfaceFile.Restart;
				TempOutputCounter := 0;

				// write BDB_EVENT headers
				for i := 0 to treeNodes.Count-1 do
				begin
					if treeNodes.Items[i].Values[IncludeCol] = 'False' then
						Continue;

					TempOutputTextFiles[TempOutputCounter].WriteLine('*BDB_EVENT');
					if TempOutputCounter > 999 then // M11FTOOL can only write out 1000 events per database
						TempOutputTextFiles[TempOutputCounter].WriteLine(Format('%s%d', [edtMouseDB.Text, TempOutputCounter div 1000]))
					else
						TempOutputTextFiles[TempOutputCounter].WriteLine(edtMouseDB.Text);
					CurrentNode := treeNodes.Items[i];
					if (CurrentNode.Values[NewIDCol] <> '') and
            (CurrentNode.Values[NewIDCol] <> Null) then
						WriteNodeStr := (Format('%s', [Trim(CurrentNode.Values[NewIDCol])]))
					else
						WriteNodeStr := (Format('%s', [Trim(CurrentNode.Values[OriginalIDCol])]));
					TempOutputTextFiles[TempOutputCounter].WriteLine(WriteNodeStr);
{					TempOutputTextFiles[TempOutputCounter].WriteLine(edtEventType.Text);}
					if (InterfaceFile.FlowUnits[i]=duMeters) or (InterfaceFile.FlowUnits[i]=duFeet) then
						TempOutputTextFiles[TempOutputCounter].WriteLine('WATER_LEVEL')
					else if (InterfaceFile.FlowUnits[i]=vuCubicMeters) or
						(InterfaceFile.FlowUnits[i]=vuCubicFeet) then
						TempOutputTextFiles[TempOutputCounter].WriteLine('DISCHARGE');
					frmMain.RegSettings.WriteString(UnitsSection, 'EventType', edtEventType.Text);

					if (InterfaceFile.FlowUnits[i] = duMeters) or (InterfaceFile.FlowUnits[i]=duFeet) then
					begin
						TempOutputTextFiles[TempOutputCounter].WriteLine('*UNIT_FT')
					end
					else if (InterfaceFile.FlowUnits[i] = vuCubicMeters) or
						(InterfaceFile.FlowUnits[i]=vuCubicFeet) then
					begin
						TempOutputTextFiles[TempOutputCounter].WriteLine('*UNIT_CFS')
					end;

{					if edtUnits.Text = '' then
						frmMain.RegSettings.WriteString(UnitsSection, edtEventType.Text+'Units', 'none')
					else
						frmMain.RegSettings.WriteString(UnitsSection, edtEventType.Text+'Units', edtUnits.Text);
					if edtUnits.Text = 'custom' then
					begin
						TempOutputTextFiles[TempOutputCounter].WriteLine('*FACTOR');
						TempOutputTextFiles[TempOutputCounter].WriteLine(edtFactor.Text);
						TempOutputTextFiles[TempOutputCounter].WriteLine('*OFFSET');
						TempOutputTextFiles[TempOutputCounter].WriteLine(edtOffset.Text);
						frmMain.RegSettings.WriteFloat(UnitsSection, edtEventType.Text+'Factor', StrToFloat(edtFactor.Text));
						frmMain.RegSettings.WriteFloat(UnitsSection, edtEventType.Text+'Offset', StrToFloat(edtOffset.Text));
					end
					else if edtUnits.Text = 'in/hr' then
					begin
						TempOutputTextFiles[TempOutputCounter].WriteLine('*FACTOR');
						TempOutputTextFiles[TempOutputCounter].WriteLine('7.055555555');
						TempOutputTextFiles[TempOutputCounter].WriteLine('*OFFSET');
						TempOutputTextFiles[TempOutputCounter].WriteLine('0.00');
					end
					else if edtUnits.Text = 'ft' then
						TempOutputTextFiles[TempOutputCounter].WriteLine('*UNIT_FT')
					else if edtUnits.Text = 'cfs' then
						TempOutputTextFiles[TempOutputCounter].WriteLine('*UNIT_CFS')
					else if edtUnits.Text = 'ft/s' then
					begin
						TempOutputTextFiles[TempOutputCounter].WriteLine('*FACTOR');
						TempOutputTextFiles[TempOutputCounter].WriteLine('.3048');
						TempOutputTextFiles[TempOutputCounter].WriteLine('*OFFSET');
						TempOutputTextFiles[TempOutputCounter].WriteLine('0.00');
					end
					else if edtUnits.Text = 'Fahrenheit' then
					begin
						TempOutputTextFiles[TempOutputCounter].WriteLine('*FACTOR');
						TempOutputTextFiles[TempOutputCounter].WriteLine('0.555555555');
						TempOutputTextFiles[TempOutputCounter].WriteLine('*OFFSET');
						TempOutputTextFiles[TempOutputCounter].WriteLine('-17.77777777');
					end;}
					TempOutputTextFiles[TempOutputCounter].WriteLine('*DATA_FORMAT_3');
					Inc(TempOutputCounter);
				end; // writing out headers

				// Write out time series
				FirstTimeStep := True;
				while (not InterfaceFile.IsEOF) and IsProcessing do
				begin

					if not IsProcessing then
					begin
						btnStopConversion.Enabled := False;
						btnConvertFile.Enabled := True;
						prgConvert.PartsComplete := 0;
						prgFile.PartsComplete := 0;
						Break;
					end;

					InterfaceFile.ReadFlows;

					// First time step processing

					if FirstTimeStep then
					begin

						// Sometimes the first time step is a zero delta, so we need to check
						TempOutputCounter := 0;
						for i := 0 to treeNodes.Count-1 do
						begin
							if treeNodes.Items[i].Values[IncludeCol] = 'False' then
								Continue;
							if PrevDelta[TempOutputCounter] <> 0 then
							begin
								FirstTimeStep := False;
								Break;
							end;
							Inc(TempOutputCounter);
						end;

						TempOutputCounter := 0;
						for i := 0 to treeNodes.Count-1 do
						begin
							if treeNodes.Items[i].Values[IncludeCol] = 'False' then
								Continue;
							PrevDateTime[TempOutputCounter] := InterfaceFile.CurrentTime;
							PrevDelta[TempOutputCounter] := InterfaceFile.CurrentTimeStep;
							PrevFlows[TempOutputCounter] := InterfaceFile.Flows[i];
							if Length(treeNodes.Items[i].Texts[MultiplierCol]) > 0 then
								AdjustedFlow := InterfaceFile.Flows[i]*treeNodes.Items[i].Values[MultiplierCol]
							else
								AdjustedFlow := InterfaceFile.Flows[i];
							if Length(treeNodes.Items[i].Texts[ModifierCol]) > 0 then
								if ((InterfaceFile.Flows[i] > StrToFloat(treeNodes.Items[i].Values[ModifierCol])) and
									(StrToFloat(treeNodes.Items[i].Values[ModifierCol]) <> 0)) then
									AdjustedFlow := StrToFloat(treeNodes.Items[i].Values[ModifierCol]);

							if (not chkLimitExtractPeriod.Checked) or
								(chkLimitExtractPeriod.Checked and
								DateIsWithin(PrevDateTime[TempOutputCounter], BeginLimitTime, EndLimitTime)) then
								TempOutputTextFiles[TempOutputCounter].WriteLine(Format('%.4d,%.2d,%.2d,%.2d,%.2d,%.2d,%.3f',
									[YearOfDateTime(PrevDateTime[TempOutputCounter]),
									MonthOfDateTime(PrevDateTime[TempOutputCounter]),
									DayOfDateTime(PrevDateTime[TempOutputCounter]),
									HourOfDateTime(PrevDateTime[TempOutputCounter]),
									MinuteOfDateTime(PrevDateTime[TempOutputCounter]),
									SecondOfDateTime(PrevDateTime[TempOutputCounter]),
									AdjustedFlow]));
							Inc(TempOutputCounter);
						end;
						Continue; // while (not InterfaceFile.IsEOF) and IsProcessing
					end;

					// Beyond first time step processing

					// If the time step is farther than the previous (time step + delta),
					// we need to stick a zero flow at (time step + delta)
					TempOutputCounter := 0;
					for i := 0 to treeNodes.Count-1 do
					begin
						if treeNodes.Items[i].Values[IncludeCol] = 'False' then
							Continue;

						PrevDateTimeWithDelta := RoundToNearestSecond(PrevDateTime[TempOutputCounter] +
							PrevDelta[TempOutputCounter]/86400);
						RoundedCurrentTime := RoundToNearestSecond(InterfaceFile.CurrentTime);
						if (CompareDateTime(RoundedCurrentTime,PrevDateTimeWithDelta) = GreaterThanValue) then
						begin
							ZeroDateTime := PrevDateTime[TempOutputCounter] + PrevDelta[TempOutputCounter]/86400;
							if (not chkLimitExtractPeriod.Checked) or
								(chkLimitExtractPeriod.Checked and
								DateIsWithin(ZeroDateTime, BeginLimitTime, EndLimitTime)) then
								begin
									TempOutputTextFiles[TempOutputCounter].WriteLine(Format('%.4d,%.2d,%.2d,%.2d,%.2d,%.2d,%.3f',
										[YearOfDateTime(ZeroDateTime),
										MonthOfDateTime(ZeroDateTime),
										DayOfDateTime(ZeroDateTime),
										HourOfDateTime(ZeroDateTime),
										MinuteOfDateTime(ZeroDateTime),
										SecondOfDateTime(ZeroDateTime),
										0.000]));
								end;
						end;
						Inc(TempOutputCounter);
					end;

					TempOutputCounter := 0;
					for i := 0 to treeNodes.Count-1 do
					begin
						if treeNodes.Items[i].Values[IncludeCol] = 'False' then
							Continue;

						if IsZero(InterfaceFile.Flows[i],0.0005) and IsZero(PrevFlows[TempOutputCounter],0.0005) then
						begin
							SkippedTimeStep[TempOutputCounter] := True;
							PrevDateTime[TempOutputCounter] := InterfaceFile.CurrentTime;
							PrevDelta[TempOutputCounter] := InterfaceFile.CurrentTimeStep;
							PrevFlows[TempOutputCounter] := InterfaceFile.Flows[i];
							Inc(TempOutputCounter);
							Continue;
						end
						else
						begin
							SkippedTimeStep[TempOutputCounter] := False;
							PrevDateTime[TempOutputCounter] := InterfaceFile.CurrentTime;
							PrevDelta[TempOutputCounter] := InterfaceFile.CurrentTimeStep;
							PrevFlows[TempOutputCounter] := InterfaceFile.Flows[i];
						end;

						// Flow Capping ... insert flow modifications here
						if Length(treeNodes.Items[i].Texts[MultiplierCol]) > 0 then
							AdjustedFlow := InterfaceFile.Flows[i]*treeNodes.Items[i].Values[MultiplierCol]
						else
							AdjustedFlow := InterfaceFile.Flows[i];
						if Length(treeNodes.Items[i].Texts[ModifierCol]) > 0 then
							if ((AdjustedFlow > StrToFloat(treeNodes.Items[i].Values[ModifierCol])) and
								(StrToFloat(treeNodes.Items[i].Values[ModifierCol]) <> 0)) then
								AdjustedFlow := StrToFloat(treeNodes.Items[i].Values[ModifierCol]);

						if (not chkLimitExtractPeriod.Checked) or
							(chkLimitExtractPeriod.Checked and
							DateIsWithin(InterfaceFile.CurrentTime, BeginLimitTime, EndLimitTime)) then
							TempOutputTextFiles[TempOutputCounter].WriteLine(Format('%.4d,%.2d,%.2d,%.2d,%.2d,%.2d,%.3f',
								[YearOfDateTime(InterfaceFile.CurrentTime),
								MonthOfDateTime(InterfaceFile.CurrentTime),
								DayOfDateTime(InterfaceFile.CurrentTime),
								HourOfDateTime(InterfaceFile.CurrentTime),
								MinuteOfDateTime(InterfaceFile.CurrentTime),
								SecondOfDateTime(InterfaceFile.CurrentTime),
								AdjustedFlow]));
						Inc(TempOutputCounter);
					end;

					lblTotalProgress.Caption := Format('Total Progress (%s)',
						[FormatDateTime('mm/dd/yyyy hh:mm:ss', InterfaceFile.CurrentTime)]);
					prgFile.PartsComplete := Round(InterfaceFile.Stream.Position / FileSize*100);
					prgConvert.PartsComplete := NodesToProcess + InterfaceFile.Stream.Position div (1024*64);
					Application.ProcessMessages;

				end;

				// Write out the last time step if we're still in skip mode
				TempOutputCounter := 0;
				for i := 0 to treeNodes.Count-1 do
				begin
					if treeNodes.Items[i].Values[IncludeCol] = 'False' then
						Continue;
					if SkippedTimeStep[TempOutputCounter] then
					begin
						if Length(treeNodes.Items[i].Texts[MultiplierCol]) > 0 then
							AdjustedFlow := InterfaceFile.Flows[i]*treeNodes.Items[i].Values[MultiplierCol]
						else
							AdjustedFlow := InterfaceFile.Flows[i];
						if Length(treeNodes.Items[i].Texts[ModifierCol]) > 0 then
							if ((AdjustedFlow > StrToFloat(treeNodes.Items[i].Values[ModifierCol])) and
								(StrToFloat(treeNodes.Items[i].Values[ModifierCol]) <> 0)) then
								AdjustedFlow := StrToFloat(treeNodes.Items[i].Values[ModifierCol]);
						if (not chkLimitExtractPeriod.Checked) or
							(chkLimitExtractPeriod.Checked and
							DateIsWithin(InterfaceFile.CurrentTime, BeginLimitTime, EndLimitTime)) then
							TempOutputTextFiles[TempOutputCounter].WriteLine(Format('%.4d,%.2d,%.2d,%.2d,%.2d,%.2d,%.3f',
								[YearOfDateTime(InterfaceFile.CurrentTime),
								MonthOfDateTime(InterfaceFile.CurrentTime),
								DayOfDateTime(InterfaceFile.CurrentTime),
								HourOfDateTime(InterfaceFile.CurrentTime),
								MinuteOfDateTime(InterfaceFile.CurrentTime),
								SecondOfDateTime(InterfaceFile.CurrentTime),
								AdjustedFlow]));
					end;
					Inc(TempOutputCounter);
				end;

				prgFile.PartsComplete := 0;

				prgConvert.PartsComplete := prgConvert.PartsComplete + 1;
				prgConvert.Update;

				// Assemble the final output here
				prgFile.PartsComplete := 0;
				prgFile.TotalParts := NodesToProcess;
				lblTotalProgress.Caption := 'Assembling final M11 file';
				for i := 0 to NodesToProcess-1 do
				begin
					TempOutputTextFiles[i].SeekLine(0);
					while not TempOutputTextFiles[i].AtEndOfStream do
					begin
						OutputFile.WriteLine(TempOutputTextFiles[i].ReadLine);
						Application.ProcessMessages;
					end;
					lblFileProgress.Caption := Format('File Progress %d of %d', [i+1, NodesToProcess]);
					prgFile.PartsComplete := i+1;
					prgConvert.PartsComplete := prgConvert.TotalParts - NodesToProcess*2 + i;
				end;
				prgFile.PartsComplete := 0;
				lblFileProgress.Caption := 'File Progress';

				// Close out the temporary files
				lblTotalProgress.Caption := 'Deleting temporary M11 files';
				for i := 0 to NodesToProcess-1 do
				begin
					TempOutputTextFiles[i].Free;
					TempOutputFiles[i].Free;
					DeleteFile(TempOutputFileNames[i]);
					prgConvert.PartsComplete := prgConvert.TotalParts - NodesToProcess + i;
				end;
			end; // if chkGenerateTimeSeries.Checked

{			if chkGenerateMouseTimeSeries.Checked then
			begin
				for i := 0 to treeNodes.Count-1 do
				begin
					if not IsProcessing then
					begin
						btnStopConversion.Enabled := False;
						btnConvertFile.Enabled := True;
						prgConvert.PartsComplete := 0;
						prgFile.PartsComplete := 0;
						Break;
					end;

					if treeNodes.Items[i].Values[IncludeCol] = 'False' then
						Continue;

					InterfaceFile.Restart;

					// Write BDB_EVENT header
					OutputFile.WriteLine('*BDB_EVENT');
					OutputFile.WriteLine(edtMouseDB.Text);
					CurrentNode := treeNodes.Items[i];
					if CurrentNode.Values[NewIDCol] <> '' then
						WriteNodeStr := (Format('%s', [CurrentNode.Values[NewIDCol]]))
					else
						WriteNodeStr := (Format('%s', [CurrentNode.Values[OriginalIDCol]]));
					OutputFile.WriteLine(WriteNodeStr);
					lblTotalProgress.Caption := Format('Total Progress (%s)', [WriteNodeStr]);
					OutputFile.WriteLine(edtEventType.Text);
					frmMain.RegSettings.WriteString(UnitsSection, 'EventType', edtEventType.Text);
					if edtUnits.Text = '' then
						frmMain.RegSettings.WriteString(UnitsSection, edtEventType.Text+'Units', 'none')
					else
						frmMain.RegSettings.WriteString(UnitsSection, edtEventType.Text+'Units', edtUnits.Text);
					if edtUnits.Text = 'custom' then
					begin
						OutputFile.WriteLine('*FACTOR');
						OutputFile.WriteLine(edtFactor.Text);
						OutputFile.WriteLine('*OFFSET');
						OutputFile.WriteLine(edtOffset.Text);
						frmMain.RegSettings.WriteFloat(UnitsSection, edtEventType.Text+'Factor', StrToFloat(edtFactor.Text));
						frmMain.RegSettings.WriteFloat(UnitsSection, edtEventType.Text+'Offset', StrToFloat(edtOffset.Text));
					end
					else if edtUnits.Text = 'in/hr' then
					begin
						OutputFile.WriteLine('*FACTOR');
						OutputFile.WriteLine('7.055555555');
						OutputFile.WriteLine('*OFFSET');
						OutputFile.WriteLine('0.00');
					end
					else if edtUnits.Text = 'ft' then
						OutputFile.WriteLine('*UNIT_FT')
					else if edtUnits.Text = 'cfs' then
						OutputFile.WriteLine('*UNIT_CFS')
					else if edtUnits.Text = 'ft/s' then
					begin
						OutputFile.WriteLine('*FACTOR');
						OutputFile.WriteLine('.3048');
						OutputFile.WriteLine('*OFFSET');
						OutputFile.WriteLine('0.00');
					end
					else if edtUnits.Text = 'Fahrenheit' then
					begin
						OutputFile.WriteLine('*FACTOR');
						OutputFile.WriteLine('0.555555555');
						OutputFile.WriteLine('*OFFSET');
						OutputFile.WriteLine('-17.77777777');
					end;
					OutputFile.WriteLine('*DATA_FORMAT_3');

					// Write out time series
					FirstTimeStep := True;
					while (not InterfaceFile.IsEOF) and IsProcessing do
					begin
						InterfaceFile.ReadFlows;
						// Some interface files have an initial time step that has a delta of
						// zero and should be skipped
						// This was removed to preserve the actual simulation time
//            if IsZero(InterfaceFile.CurrentTimeStep) then
//              Continue;

						// Skip if we have date ranges specified and we're not in a date range
						if not dlgSpecifyExtractDates.InRange(InterfaceFile.CurrentTime) then
							Continue;

						// We need to know if we are processing the first time step since
						// we test for each time step how far we are from the previous time
						// step.  Naturally, the first time step doesn't go through this.
						if FirstTimeStep then
						begin
							FirstTimeStep := False;
							PrevDateTime := InterfaceFile.CurrentTime;
							PrevDelta := InterfaceFile.CurrentTimeStep;
							if PrevDelta = 0 then
								FirstTimeStep := True; // Sometimes the first time step is a zero delta
							PrevFlow := InterfaceFile.Flows[i];
							AdjustedFlow := InterfaceFile.Flows[i];
							if Length(treeNodes.Items[i].Texts[ModifierCol]) > 0 then
								if ((InterfaceFile.Flows[i] > StrToFloat(treeNodes.Items[i].Values[ModifierCol])) and
									(StrToFloat(treeNodes.Items[i].Values[ModifierCol]) <> 0)) then
									AdjustedFlow := StrToFloat(treeNodes.Items[i].Values[ModifierCol]);

							if (not chkLimitExtractPeriod.Checked) or
								(chkLimitExtractPeriod.Checked and
								DateIsWithin(PrevDateTime, BeginLimitTime, EndLimitTime)) then
								OutputFile.WriteLine(Format('%.4d,%.2d,%.2d,%.2d,%.2d,%.2d,%.3f',
									[YearOfDateTime(PrevDateTime),
									MonthOfDateTime(PrevDateTime),
									DayOfDateTime(PrevDateTime),
									HourOfDateTime(PrevDateTime),
									MinuteOfDateTime(PrevDateTime),
									SecondOfDateTime(PrevDateTime),
									AdjustedFlow]));
							Continue;
						end;

						// If the time step is farther than the previous (time step + delta),
						// we need to stick a zero flow at (time step + delta)
						PrevDateTimeWithDelta := RoundToNearestSecond(PrevDateTime + PrevDelta/86400);
						RoundedCurrentTime := RoundToNearestSecond(InterfaceFile.CurrentTime);
						if (CompareDateTime(RoundedCurrentTime,PrevDateTimeWithDelta) = GreaterThanValue) then
						begin
							ZeroDateTime := PrevDateTime + PrevDelta/86400;
							if (not chkLimitExtractPeriod.Checked) or
								(chkLimitExtractPeriod.Checked and
								DateIsWithin(ZeroDateTime, BeginLimitTime, EndLimitTime)) then
								OutputFile.WriteLine(Format('%.4d,%.2d,%.2d,%.2d,%.2d,%.2d,%.3f',
									[YearOfDateTime(ZeroDateTime),
									MonthOfDateTime(ZeroDateTime),
									DayOfDateTime(ZeroDateTime),
									HourOfDateTime(ZeroDateTime),
									MinuteOfDateTime(ZeroDateTime),
									SecondOfDateTime(ZeroDateTime),
									0.000]));
						end;

						if IsZero(InterfaceFile.Flows[i],0.0005) and IsZero(PrevFlow,0.0005) then
						begin
							PrevDateTime := InterfaceFile.CurrentTime;
							PrevDelta := InterfaceFile.CurrentTimeStep;
							PrevFlow := InterfaceFile.Flows[i];
							SkippedTimeStep := True;
							Application.ProcessMessages;
							Continue; // skip the time step if it and the previous step are zero
						end
						else
						begin
							PrevDateTime := InterfaceFile.CurrentTime;
							PrevDelta := InterfaceFile.CurrentTimeStep;
							PrevFlow := InterfaceFile.Flows[i];
						end;

						SkippedTimeStep := False;

						// Flow Capping ... insert flow modifications here
						AdjustedFlow := InterfaceFile.Flows[i];
						if Length(treeNodes.Items[i].Texts[ModifierCol]) > 0 then
							if ((InterfaceFile.Flows[i] > StrToFloat(treeNodes.Items[i].Values[ModifierCol])) and
								(StrToFloat(treeNodes.Items[i].Values[ModifierCol]) <> 0)) then
								AdjustedFlow := StrToFloat(treeNodes.Items[i].Values[ModifierCol]);

						if (not chkLimitExtractPeriod.Checked) or
							(chkLimitExtractPeriod.Checked and
							DateIsWithin(InterfaceFile.CurrentTime, BeginLimitTime, EndLimitTime)) then
							OutputFile.WriteLine(Format('%.4d,%.2d,%.2d,%.2d,%.2d,%.2d,%.3f',
								[YearOfDateTime(InterfaceFile.CurrentTime),
								MonthOfDateTime(InterfaceFile.CurrentTime),
								DayOfDateTime(InterfaceFile.CurrentTime),
								HourOfDateTime(InterfaceFile.CurrentTime),
								MinuteOfDateTime(InterfaceFile.CurrentTime),
								SecondOfDateTime(InterfaceFile.CurrentTime),
								AdjustedFlow]));

						prgFile.PartsComplete := Round(InterfaceFile.Stream.Stream.Position / FileSize*100);
						Application.ProcessMessages;

					end;

					prgFile.PartsComplete := 0;

					// If we skipped and we're at the end, write out the last flow
					if SkippedTimeStep then
					begin
						AdjustedFlow := InterfaceFile.Flows[i];
						if Length(treeNodes.Items[i].Texts[ModifierCol]) > 0 then
							if ((InterfaceFile.Flows[i] > StrToFloat(treeNodes.Items[i].Values[ModifierCol])) and
								(StrToFloat(treeNodes.Items[i].Values[ModifierCol]) <> 0)) then
								AdjustedFlow := StrToFloat(treeNodes.Items[i].Values[ModifierCol]);
						if (not chkLimitExtractPeriod.Checked) or
							(chkLimitExtractPeriod.Checked and
							DateIsWithin(InterfaceFile.CurrentTime, BeginLimitTime, EndLimitTime)) then
							OutputFile.WriteLine(Format('%.4d,%.2d,%.2d,%.2d,%.2d,%.2d,%.3f',
								[YearOfDateTime(InterfaceFile.CurrentTime),
								MonthOfDateTime(InterfaceFile.CurrentTime),
								DayOfDateTime(InterfaceFile.CurrentTime),
								HourOfDateTime(InterfaceFile.CurrentTime),
								MinuteOfDateTime(InterfaceFile.CurrentTime),
								SecondOfDateTime(InterfaceFile.CurrentTime),
								AdjustedFlow]));
					end;

					prgConvert.PartsComplete := prgConvert.PartsComplete + 1;
					prgConvert.Update;
				end;
			end; // if chkGenerateTimeSeries.Checked
}

			// Write out BSF connectors
			if chkGenerateMouseBoundaryConnectors.Checked and IsProcessing then
			begin
				// Write out BSF
				TempOutputCounter := 0;
				for i := 0 to treeNodes.Count-1  do
				begin
					if treeNodes.Items[i].Values[IncludeCol] = 'False' then
						Continue;

					OutputFile.WriteLine('*MOUSE_BSF');
					OutputFile.WriteLine(edtBSFName.Text);
					if TempOutputCounter > 999 then // M11FTOOL can only write out 1000 events per database
						OutputFile.WriteLine(Format('%s%d', [edtMouseDB.Text, TempOutputCounter div 1000]))
					else
						OutputFile.WriteLine(edtMOUSEDB.Text);
					if (treeNodes.Items[i].Values[NewIDCol] <> '') and
            (treeNodes.Items[i].Values[NewIDCol] <> Null) then
						OutputFile.WriteLine(Format('%s', [treeNodes.Items[i].Values[NewIDCol]]))
					else
						OutputFile.WriteLine(Format('%s', [treeNodes.Items[i].Values[OriginalIDCol]]));
					if (InterfaceFile.FlowUnits[i]=duMeters) or (InterfaceFile.FlowUnits[i]=duFeet) then
						OutputFile.WriteLine('WATER_LEVEL')
					else if (InterfaceFile.FlowUnits[i]=vuCubicMeters) or
						(InterfaceFile.FlowUnits[i]=vuCubicFeet) then
						OutputFile.WriteLine('DISCHARGE');
//					OutputFile.WriteLine(edtEventType.Text);
					OutputFile.WriteLine('1');
					if not (edtEventType.Text = 'RAIN_INTENSITY') then
						if (treeNodes.Items[i].Values[AssignToIDCol] <> '') and
              (treeNodes.Items[i].Values[AssignToIDCol] <> Null) then
							OutputFile.WriteLine(Format('%s', [treeNodes.Items[i].Values[AssignToIDCol]]))
						else
						begin
							if (treeNodes.Items[i].Values[NewIDCol] <> '') and
                (treeNodes.Items[i].Values[NewIDCol] <> Null) then
								OutputFile.WriteLine(Format('%s', [treeNodes.Items[i].Values[NewIDCol]]))
							else
								OutputFile.WriteLine(Format('%s', [treeNodes.Items[i].Values[OriginalIDCol]]));
						end;
					OutputFile.WriteLine('');
					Inc(TempOutputCounter);
				end;
			end;

			IsProcessing := False;
			prgConvert.PartsComplete := 0;
		finally
			OutputFile.Free;
			OutputBackEndFile.Free;
			Screen.Cursor := crDefault;
		end;

	end	// end MOUSE conversion
	else if tabConvertType.ActivePage = shtSWMMOptions then // To-SWMM Conversion
	begin
		try
			IsProcessing := True;
			Screen.Cursor := crHourglass;

			CurrentStep := 0;
			SkipInterval := edtSkipInterval.IntValue;

			IncludeCol := cxColInclude.ItemIndex;
			OriginalIDCol := cxColOriginalID.ItemIndex;
			NewIDCol := cxColNewID.ItemIndex;
			AssignToIDCol := cxColAssignToID.ItemIndex;
			ModifierCol := cxColModifier.ItemIndex;
			MultiplierCol := cxColMultiplier.ItemIndex;

			// Count actual number of nodes to process
			NodesToProcess := 0;
			ExtractNodesList := TStringList.Create;
			HeaderNodesList := TStringList.Create;
			for i := 0 to treeNodes.Count-1 do
				if treeNodes.Items[i].Values[IncludeCol] = 'True' then
				begin
					Inc(NodesToProcess);
					ANodeModifier := TNodeModifier.Create;
					if Length(treeNodes.Items[i].Texts[MultiplierCol]) > 0 then
						ANodeModifier.Multiplier := treeNodes.Items[i].Values[MultiplierCol]
					else
						ANodeModifier.Multiplier := 1;
					ExtractNodesList.AddObject(treeNodes.Items[i].Values[OriginalIDCol], ANodeModifier);
					if (treeNodes.Items[i].Values[NewIDCol] <> '') and
            (treeNodes.Items[i].Values[NewIDCol] <> Null) then
						HeaderNodesList.Add(treeNodes.Items[i].Values[NewIDCol])
					else
						HeaderNodesList.Add(treeNodes.Items[i].Values[OriginalIDCol]);
				end;
			prgConvert.TotalParts := InterfaceFile.Stream.Size;

			// Write out header
			if Assigned(OutputInterfaceFile) then
				OutputInterfaceFile := nil;

			case rgrpToSWMMFormat.ItemIndex of
				0: begin
					OutputInterfaceFile := TSWMMStandardInterfaceFile.Create(frmMain.dlgSave.FileName, fmCreate, if32);
					InterfaceFile.AlphaIDSize := 10;
				end;
				1: begin
					OutputInterfaceFile := TSWMMStandardInterfaceFile.Create(frmMain.dlgSave.FileName, fmCreate, if95);
					InterfaceFile.AlphaIDSize := 10;
				end;
				2: begin
					OutputInterfaceFile := TSWMMStandardInterfaceFile.Create(frmMain.dlgSave.FileName, fmCreate, ifXP);
					InterfaceFile.AlphaIDSize := 16;
				end;
				3: begin
					OutputInterfaceFile := TSWMMStandardInterfaceFile.Create(frmMain.dlgSave.FileName, fmCreate, ifText);
					InterfaceFile.AlphaIDSize := 10;
				end;
			end;

			InterfaceFile.UsesAlphaNumericIDs :=  chkSWMMAlphanumeric.Checked;

			// To-Do: Must write out correct beginning time if we're limiting the
			//   extraction time period
			if chkLimitExtractPeriod.Checked then
			begin
				InterfaceFile.Restart;
				if InterfaceFile.InterfaceFormat <> ifXP then
					InterfaceFile.ReadFlows; // first line of nonXP is crap
				while (not InterfaceFile.IsEOF) do
				begin
					InterfaceFile.ReadFlows;
					if DateIsWithin(InterfaceFile.CurrentTime, BeginLimitTime, EndLimitTime) then
					begin
						InterfaceFile.Start := InterfaceFile.CurrentTime;
						Break;
					end;
				end;
			end;
			OutputInterfaceFile.WriteHeaderLimitNodes(InterfaceFile, HeaderNodesList);

			// Convert nodes list to indices for faster response
			for i := 0 to ExtractNodesList.Count-1 do
				ExtractNodesList[i] := IntToStr(InterfaceFile.IDsList.IndexOf(ExtractNodesList[i]));

			// Write out time series
			InterfaceFile.Restart;
			if (InterfaceFile.InterfaceFormat <> ifXP) and
				(InterfaceFile.InterfaceFormat <> ifText) then
				InterfaceFile.ReadFlows; // first line of nonXP is crap
			while (not InterfaceFile.IsEOF) and IsProcessing do
			begin
				InterfaceFile.ReadFlows;

				if (not chkLimitExtractPeriod.Checked) or
					(chkLimitExtractPeriod.Checked and
					DateIsWithin(InterfaceFile.CurrentTime, BeginLimitTime, EndLimitTime)) then
				begin
					OutputInterfaceFile.WriteFlowsLimitNodesIndexList(InterfaceFile, ExtractNodesList,
						SkipInterval);
				end;
				Inc(CurrentStep);

				Application.ProcessMessages;
				prgConvert.PartsComplete := InterfaceFile.Stream.Position;
				prgConvert.Update;
			end;

			IsProcessing := False;
			prgConvert.PartsComplete := 0;
		finally
			for i := 0 to ExtractNodesList.Count-1 do
				ExtractNodesList.Objects[i].Free;
			ExtractNodesList.Free;
      HeaderNodesList.Free;
			Screen.Cursor := crDefault;
		end;

	end // end SWMM conversion
	else if tabConvertType.ActivePage = shtSYFTextOptions then // XP SYF-to-Text Conversion
	begin
		try
			IsProcessing := True;
			Screen.Cursor := crHourglass;
			OutputBackEndFile := TFileStream.Create(edtSaveFile.Text, fmCreate);
			OutputFile := TStAnsiTextStream.Create(OutputBackEndFile);
			MultiplierCol := cxColMultiplier.ItemIndex;

			// Write out header
			FlowStr := '';
			for i := 0 to SYFFile.NumLinks-1 do
			begin
				CurrentNode := treeNodes.Items[i];
				if CurrentNode.Values[cxColInclude.ItemIndex] = True then
					FlowStr := FlowStr + SYFFile.Links[i].ID+',';
			end;
			OutputFile.WriteLine(FlowStr);

			// Write out flows
			SYFFile.MoveToStartOfFlows;
			prgConvert.TotalParts := 500;
			SYFFileStartTime := EncodeDateTime(1900,1,1,0,0,0,0);
			UserSpecStartTime := edtSYFStartDate.Date + edtSYFStartTime.Time;
			while not SYFFile.IsEOF do
			begin
				SYFFile.ReadFlows;
				FlowStr := Format('%d,%.3f,%.3f,', [Y2KJulDateOfDateTime(SYFFile.CurrentTime - SYFFileStartTime +
					UserSpecStartTime),
					SecondsOfDayOfDateTime(SYFFile.CurrentTime - SYFFileStartTime + UserSpecStartTime),
					SYFFile.SYFTimeStep]);
				for i := 0 to SYFFile.NumLinks-1 do
				begin
					CurrentNode := treeNodes.Items[i];
					if CurrentNode.Values[cxColInclude.ItemIndex] = True then
					begin
						if Length(CurrentNode.Texts[MultiplierCol]) > 0 then
							FlowStr := FlowStr + Format('%.3f,', [SYFFile.Links[i].Flow*Double(CurrentNode.Values[MultiplierCol])])
						else
							FlowStr := FlowStr + Format('%.3f,', [SYFFile.Links[i].Flow]);
					end;
				end;
				if (not chkLimitExtractPeriod.Checked) or
					(chkLimitExtractPeriod.Checked and
					DateIsWithin(SYFFile.CurrentTime, BeginLimitTime, EndLimitTime)) then
					OutputFile.WriteLine(FlowStr);
				Application.ProcessMessages;
				FileComplete := SYFFile.FilePosition/SYFFile.FileSize*500;
				prgConvert.PartsComplete := Round(FileComplete);
				prgConvert.Update;
			end;

			{while not SYFFile.IsEOF do
			begin
				ABuf := SYFFile.ReadSingle;
				AIntPtr := @ABuf;
				AInt := AIntPtr^;
				OutputFile.WriteLine(Format('%d, %.3f', [AInt, ABuf]));
				Application.ProcessMessages;
				prgConvert.PartsComplete := SYFFile.FilePosition;
				prgConvert.Update;
			end;}

		finally
			IsProcessing := False;
			prgConvert.PartsComplete := 0;
			OutputFile.Free;
			OutputBackEndFile.Free;
			Screen.Cursor := crDefault;
		end;
	end; // end SYF-to-Text Conversion

	btnStopConversion.Enabled := False;
	btnConvertFile.Enabled := True;
	lblTotalProgress.Caption := 'Total Progress';
	if FileExists(ExtractFileDir(frmMain.dlgOpen.FileName)+'\M11.OUT') then
		DeleteFile(ExtractFileDir(frmMain.dlgOpen.FileName)+'\M11.OUT');
	if FileExists(ExtractFileDir(frmMain.dlgOpen.FileName)+'\M11.IN') then
		DeleteFile(ExtractFileDir(frmMain.dlgOpen.FileName)+'\M11.IN');
	if FileExists(ExtractFileDir(frmMain.dlgOpen.FileName)+'\M11.TXT') then
		DeleteFile(ExtractFileDir(frmMain.dlgOpen.FileName)+'\M11.TXT');

end;

procedure TfrmConvertInterface.actExcludeAllUpdate(Sender: TObject);
begin
	inherited;
	actExcludeAll.Enabled := (treeNodes.Count > 0) and (not IsProcessing);
	actIncludeAll.Enabled := actExcludeAll.Enabled;
	actToggleRange.Enabled := actExcludeAll.Enabled;
	actSaveModifiers.Enabled := actExcludeAll.Enabled;
	actLoadModifiers.Enabled := actExcludeAll.Enabled;
	actConvertFile.Enabled := actExcludeAll.Enabled;
end;

procedure TfrmConvertInterface.actStopConversionUpdate(Sender: TObject);
begin
	inherited;
	actStopConversion.Enabled := IsProcessing;
end;

procedure TfrmConvertInterface.FormCreate(Sender: TObject);
begin
  inherited;
  IsProcessing := False;
	edtEventType.Text := frmMain.RegSettings.ReadString(UnitsSection, 'EventType', 'DISCHARGE');
	edtUnits.Text := frmMain.RegSettings.ReadString(UnitsSection, 'Units', 'cfs');
	if edtUnits.Text = 'none' then
		edtUnits.Text := '';
  pgcConvertInterfaceWizard.ActivePageIndex := 0;
end;

procedure TfrmConvertInterface.btnStopConversionClick(Sender: TObject);
begin
  inherited;
	IsProcessing := False;
end;

procedure TfrmConvertInterface.edtEventTypeChange(Sender: TObject);
begin
	inherited;

	edtUnits.Text := frmMain.RegSettings.ReadString(UnitsSection, edtEventType.Text+'Units', '');
	if edtEventType.Text = 'RAIN_INTENSITY' then
	begin
		edtUnits.Items.Clear;
		edtUnits.Items.Add('microm/s');
		edtUnits.Items.Add('in/hr');
		edtUnits.Items.Add('custom');
	end
	else if ((edtEventType.Text = 'WATER_LEVEL') or (edtEventType.Text = 'GATE_POSITION') or
		(edtEventType.Text = 'WEIR_POSITION') or (edtEventType.Text = 'BOTTOM_LEVEL')) then
	begin
		edtUnits.Items.Clear;
		edtUnits.Items.Add('m');
		edtUnits.Items.Add('ft');
		edtUnits.Items.Add('custom');
	end
	else if edtEventType.Text = 'DISCHARGE' then
	begin
		edtUnits.Items.Clear;
		edtUnits.Items.Add('cu.m/s');
		edtUnits.Items.Add('cfs');
		edtUnits.Items.Add('custom');
	end
	else if edtEventType.Text = 'VELOCITY' then
	begin
		edtUnits.Items.Clear;
		edtUnits.Items.Add('m/s');
		edtUnits.Items.Add('ft/s');
		edtUnits.Items.Add('custom');
	end
	else if edtEventType.Text = 'TEMPERATURE' then
	begin
		edtUnits.Items.Clear;
		edtUnits.Items.Add('Celsius');
		edtUnits.Items.Add('Fahrenheit');
		edtUnits.Items.Add('custom');
	end
	else if ((edtEventType.Text = 'CONCENTRATION') or (edtEventType.Text = 'EVAPORATION') or
		(edtEventType.Text = 'SEDIMENT_TRANSPORT') or (edtEventType.Text = 'FRACTION_VALUE')) then
		edtUnits.Items.Clear
	else if edtEventType.Text = 'TIME_STEP' then
	begin
		edtUnits.Items.Clear;
		edtUnits.Items.Add('s');
		edtUnits.Items.Add('custom');
	end;
end;

procedure TfrmConvertInterface.edtUnitsChange(Sender: TObject);
begin
	inherited;

  if edtUnits.Text = 'custom' then
  begin
    edtFactor.Enabled := True;
    edtOffset.Enabled := True;
		lblFactor.Enabled := True;
    lblOffset.Enabled := True;
		edtFactor.Value := frmMain.RegSettings.ReadFloat(UnitsSection, edtEventType.Text+'Factor',
			0.00);
    edtOffset.Value := frmMain.RegSettings.ReadFloat(UnitsSection, edtEventType.Text+'Offset',
			0.00);
  end
	else
  begin
    edtFactor.Enabled := False;
    edtOffset.Enabled := False;
    lblFactor.Enabled := False;
    lblOffset.Enabled := False;
  end
end;

procedure TfrmConvertInterface.actToggleRangeExecute(Sender: TObject);
var
	i: Integer;
	IncludeCol: Integer;

begin
	inherited;

  dlgToggleRange.NumItems := treeNodes.Count;
	if dlgToggleRange.ShowModal = mrOK then
	begin
		IncludeCol := cxColInclude.ItemIndex;
		for i :=	dlgToggleRange.edtLower.IntValue to dlgToggleRange.edtUpper.IntValue do
			if treeNodes.Items[i-1].Values[IncludeCol] = 'True' then
				treeNodes.Items[i-1].Values[IncludeCol] := 'False'
			else
				treeNodes.Items[i-1].Values[IncludeCol] := 'True';

		NumIncluded := 0;
		for i := 0 to treeNodes.Count-1 do
			if treeNodes.Items[i].Values[IncludeCol] = 'True' then
				Inc(NumIncluded);

		//lblSelectNodes.Caption := Format('3. Select Nodes to Convert (%d Selected)',
    //  [NumIncluded]);
	end;
end;

procedure TfrmConvertInterface.actExcludeAllExecute(Sender: TObject);
var
	i: Integer;
begin
	inherited;

	for i := 0 to treeNodes.Count-1 do
		treeNodes.Items[i].Values[cxColInclude.ItemIndex] := 'False';
	//lblSelectNodes.Caption := Format('3. Select Nodes to Convert (%d Selected)',
  //  [0]);

end;

procedure TfrmConvertInterface.actIncludeAllExecute(Sender: TObject);
var
	i: Integer;
begin
	inherited;

	for i := 0 to treeNodes.Count-1 do
		treeNodes.Items[i].Values[cxColInclude.ItemIndex] := 'True';
	//lblSelectNodes.Caption := Format('3. Select Nodes to Convert (%d Selected)',
	//  [treeNodes.Count]);
end;

procedure TfrmConvertInterface.actSaveModifiersExecute(Sender: TObject);
begin
  inherited;

  frmMain.dlgSave.Title := 'Save modifiers list';
  frmMain.dlgSave.DefaultExt := 'mod';
  frmMain.dlgSave.Filter := 'Modifiers list (*.mod)|*.mod|All files (*.*)|*.*';
	if frmMain.dlgSave.Execute then
    treeNodes.SaveToFile(frmMain.dlgSave.FileName);
end;

procedure TfrmConvertInterface.actLoadModifiersExecute(Sender: TObject);
var
  i: Integer;
  IncludeCol: Integer;
begin
  inherited;
  IncludeCol := cxColInclude.ItemIndex;

  frmMain.dlgOpen.Title := 'Open modifiers list';
  frmMain.dlgOpen.DefaultExt := 'mod';
  frmMain.dlgOpen.Filter := 'Modifiers list (*.mod)|*.mod|All files (*.*)|*.*';
  if frmMain.dlgOpen.Execute then
    treeNodes.LoadFromFile(frmMain.dlgOpen.FileName);

  NumIncluded := 0;
  for i := 0 to treeNodes.Count-1 do
    if treeNodes.Items[i].Values[IncludeCol] = 'True' then
      Inc(NumIncluded);

	//lblSelectNodes.Caption := Format('3. Select Nodes to Convert (%d Selected)',
	//	[NumIncluded]);
end;

procedure TfrmConvertInterface.tabConvertTypeChange(Sender: TObject);
begin
	inherited;
	if tabConvertType.ActivePage = shtMouseOptions then
	begin
		cxColNewID.Visible := True;
	end
	else
	begin
    cxColNewID.Visible := False;
	end;
end;

procedure TfrmConvertInterface.actSpecifyExtractDatesExecute(
	Sender: TObject);
begin
	inherited;
	if dlgSpecifyExtractDates.ShowModal = mrOK then
	begin
		if dlgSpecifyExtractDates.treeDateRanges.Count > 0 then
		begin
		end
		else
			;
	end
end;

//procedure TfrmConvertInterface.treeNodesIsExistFooterCell(
//  Sender: TObject; AColumn: Integer; var AExist: Boolean);
//var
//  IncludeCol: Integer;
//begin
//  inherited;
//  IncludeCol := treeNodes.ColumnByName('colInclude').Index;
//  if AColumn = IncludeCol then
//    AExist := True;
//end;

procedure TfrmConvertInterface.edtOpenFileButtonClick(Sender: TObject);
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
			treeNodes.Clear;
		end;
		Ext := ExtractFileExt(frmMain.dlgOpen.FileName);
		if Uppercase(Ext) = '.SYF' then
		begin
			SYFFile := TXP_SYF_FileStream.Create(frmMain.dlgOpen.FileName, fmOpenRead or fmShareDenyWrite);
			//lblStep1.Caption := Format('1. Open File to Convert (Nodes: %d, Links: %d, '+
			//	'Conduits: %d, Pumps: %d, Orifices: %d, Weirs: %d)', [SYFFile.NumJunctions,
			//	SYFFile.NumLinks, SYFFile.NumConduits, SYFFile.NumPumps, SYFFile.NumOrifices,
			//	SYFFile.NumWeirs]);
			frmMain.SaveDirToRegistry;
			edtOpenFile.Text := frmMain.dlgOpen.FileName;
			tabConvertType.ActivePage := shtSYFTextOptions;

			// Node list
			treeNodes.BeginUpdate;
			for i := 0 to SYFFile.NumLinks-1 do
			begin
				ANode := treeNodes.Add;
				ANode.Values[cxColRecord.ItemIndex] := i+1;
				ANode.Values[cxColInclude.ItemIndex] := True;
				ANode.Values[cxColOriginalID.ItemIndex] := SYFFile.Links[i].ID;
			end;
			NumIncluded := SYFFile.NumLinks;
			//lblSelectNodes.Caption := Format('3. Select Links to Convert (%d Selected)',
			//	[NumIncluded]);
			treeNodes.EndUpdate;
			treeNodes.Invalidate;
		end
		else
		begin
			case rgrpFormat.ItemIndex of
				0: InterfaceFile := TSWMMStandardInterfaceFile.Create(frmMain.dlgOpen.FileName, fmOpenRead or fmShareDenyWrite, if32);
				1: InterfaceFile := TSWMMStandardInterfaceFile.Create(frmMain.dlgOpen.FileName, fmOpenRead or fmShareDenyWrite, if95);
				2: InterfaceFile := TSWMMStandardInterfaceFile.Create(frmMain.dlgOpen.FileName, fmOpenRead or fmShareDenyWrite, ifXP);
				3: InterfaceFile := TSWMMStandardInterfaceFile.Create(frmMain.dlgOpen.FileName, fmOpenRead or fmShareDenyWrite, ifText);
				4: ; // XP SYF
				5: begin // Read PRF info
						frmStatus := TfrmStatus.Create(Application);
						frmStatus.edtStatus.Lines.Add('Running M11Extra, writing M11.OUT file');
						frmStatus.Show;
						Application.ProcessMessages;
						appLauncher.FileName := ExtractFileDir(ParamStr(0))+'\M11EXTRA.EXE';
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
									InTextStream.Free;
									InStream.Free;
								end;

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

			chkLimitExtractPeriod.Caption := 'Extract data only from the following time period: (Interface starts at '+
				FormatDateTime('m/d/yyyy hh:mm:ss', InterfaceFile.Start)+')';
			edtFromDate.Date := DateOf(InterfaceFile.Start);
			edtFromTime.Time := TimeOf(InterfaceFile.Start);
			edtToDate.Date := DateOf(MaxDateTime);
			edtToTime.Time := TimeOf(MaxDateTime);
			//lblStep1.Caption := Format('1. Open File to Convert (Nodes: %d)', [InterfaceFile.NumInlets]);
			frmMain.SaveDirToRegistry;
			edtOpenFile.Text := frmMain.dlgOpen.FileName;

			// Node list
      treeNodes.Clear;
			treeNodes.BeginUpdate;
			for i := 0 to InterfaceFile.NumInlets-1 do
			begin
				ANode := treeNodes.Add;
				ANode.Values[cxColRecord.ItemIndex] := i+1;
				ANode.Values[cxColInclude.ItemIndex] := True;
				ANode.Values[cxColOriginalID.ItemIndex] := InterfaceFile.IDs[i];
				if InterfaceFile.IDs[i] <> InterfaceFile.ToIds[i] then
          ANode.Values[cxColAssignToID.ItemIndex] := InterfaceFile.ToIds[i];
			end;
			NumIncluded := InterfaceFile.NumInlets;
			//lblSelectNodes.Caption := Format('3. Select Nodes to Convert (%d Selected)',
			//	[NumIncluded]);
			treeNodes.EndUpdate;
			treeNodes.Invalidate;

			// SWMM options
			edtSWMMTitle1.Text := InterfaceFile.Titles[0];
			edtSWMMTitle2.Text := InterfaceFile.Titles[1];
			edtSWMMTitle3.Text := InterfaceFile.Titles[2];
			edtSWMMTitle4.Text := InterfaceFile.Titles[3];
			edtSWMMSourceBlock.Text := InterfaceFile.SourceBlock;
			edtSWMMArea.Value := InterfaceFile.Area;
			chkSWMMAlphanumeric.Checked := InterfaceFile.UsesAlphaNumericIDs;
			edtSWMMFlowMultiplier.Value := InterfaceFile.FlowMultiplier;
		end;  // non-SYF files
	end;
end;

procedure TfrmConvertInterface.edtSaveFileButtonClick(Sender: TObject);
begin
	inherited;

	frmMain.dlgSave.InitialDir := frmMain.InitRegistryDir;
	frmMain.dlgSave.Title := 'Save Converted Interface File';
	case rgrpConvertFormat.ItemIndex of
		0: begin
			frmMain.dlgSave.Filter := 'Mouse (*.m11)|*.m11|All Files|*.*';
			frmMain.dlgSave.DefaultExt := 'm11';
		end;
		1: begin
			case rgrpToSWMMFormat.ItemIndex of
				0,1,2: begin
					frmMain.dlgSave.Filter := 'SWMM (*.int)|*.int|All Files|*.*';
					frmMain.dlgSave.DefaultExt := 'int';
				end;
				3: begin
					frmMain.dlgSave.Filter := 'SWMM Text (*.txt)|*.txt|All Files|*.*';
					frmMain.dlgSave.DefaultExt := 'int';
				end;
			end;
		end;
		2: begin
			frmMain.dlgSave.Filter := 'SWMM Text (*.txt)|*.int|All Files|*.*';
			frmMain.dlgSave.DefaultExt := 'txt';
		end;
	end;

	if frmMain.dlgSave.Execute then
	begin
		edtSaveFile.Text := frmMain.dlgSave.FileName;
		frmMain.dlgOpen.FileName := frmMain.dlgSave.FileName;
		frmMain.SaveDirToRegistry;
	end;
end;

procedure TfrmConvertInterface.edtOpenFileChange(Sender: TObject);
begin
	inherited;
	tabConvertType.Visible := Length(edtOpenFile.Text) > 0;
	treeNodes.Visible := tabConvertType.Visible;
end;

procedure TfrmConvertInterface.btnCloseTaskClick(Sender: TObject);
begin
  inherited;
	Close;
	frmConvertInterface := nil;
end;

procedure TfrmConvertInterface.actPreviousExecute(Sender: TObject);
begin
	pgcConvertInterfaceWizard.ActivePageIndex := Max(0, pgcConvertInterfaceWizard.ActivePageIndex-1);
end;

procedure TfrmConvertInterface.actNextExecute(Sender: TObject);
var
	LabelCaption: string;
begin
	pgcConvertInterfaceWizard.ActivePageIndex := Min(pgcConvertInterfaceWizard.PageCount-1,
		pgcConvertInterfaceWizard.ActivePageIndex+1);
	if pgcConvertInterfaceWizard.ActivePage = shtSetOptions then
	begin
		case rgrpFormat.ItemIndex of
			4: begin
					rgrpConvertFormat.ItemIndex := 2;
					rgrpConvertFormat.ItemEnabled[0] := False;
					rgrpConvertFormat.ItemEnabled[1] := False;
					rgrpConvertFormat.ItemEnabled[2] := True;
				end;
		end;
	end
	else if pgcConvertInterfaceWizard.ActivePage = shtSaveFile then
	begin
		LabelCaption := 'File Name ';
		case rgrpFormat.ItemIndex of
			0: LabelCaption := LabelCaption + 'SWMM F32 Format to ';
			1: LabelCaption := LabelCaption + 'SWMM F95 Format to ';
			2: LabelCaption := LabelCaption + 'XP Binary Format to ';
			3: LabelCaption := LabelCaption + 'SWMM Text Format to ';
			4: LabelCaption := LabelCaption + 'XP SYF Format to ';
			5: LabelCaption := LabelCaption + 'MOUSE PRF Format to ';
			6: LabelCaption := LabelCaption + 'MOUSE PRF Text Format to ';
		end;
		case rgrpConvertFormat.ItemIndex of
			0: LabelCaption:= 'MOUSE M11 Text Format';
			1: begin
				case rgrpToSWMMFormat.ItemIndex of
					0: LabelCaption := LabelCaption + 'SWMM F32 Format';
					1: LabelCaption := LabelCaption + 'SWMM F95 Format';
					2: LabelCaption := LabelCaption + 'XP Binary Format';
					3: LabelCaption := LabelCaption + 'SWMM Text Format';
				end;
			end;
			2: LabelCaption := 'XP SYF Text Format';
		end;
		lblSaveConvertedFile.Caption := LabelCaption;
	end
	else if pgcConvertInterfaceWizard.ActivePage = shtProcessFile then
		lblFileProgress.Caption := 'File Progress: ' + edtSaveFile.Text;
end;

procedure TfrmConvertInterface.actPreviousUpdate(Sender: TObject);
begin
	actPrevious.Enabled := pgcConvertInterfaceWizard.ActivePageIndex > 0;
end;

procedure TfrmConvertInterface.actNextUpdate(Sender: TObject);
begin
	if (pgcConvertInterfaceWizard.ActivePage = shtOpenFile) then
		actNext.Enabled := edtOpenFile.Text <> ''
	else if (pgcConvertInterfaceWizard.ActivePage = shtSaveFile) then
		actNext.Enabled := edtSaveFile.Text <> ''
	else
		actNext.Enabled := pgcConvertInterfaceWizard.ActivePageIndex <
			pgcConvertInterfaceWizard.PageCount-1;
end;

procedure TfrmConvertInterface.chkLimitExtractPeriodClick(Sender: TObject);
begin
	edtFromDate.Enabled := chkLimitExtractPeriod.Checked;
	edtFromTime.Enabled := chkLimitExtractPeriod.Checked;
	edtToDate.Enabled := chkLimitExtractPeriod.Checked;
	edtToTime.Enabled := chkLimitExtractPeriod.Checked;
	lblFromDate.Enabled := chkLimitExtractPeriod.Checked;
	lblFromTime.Enabled := chkLimitExtractPeriod.Checked;
	lblToDate.Enabled := chkLimitExtractPeriod.Checked;
	lblToTime.Enabled := chkLimitExtractPeriod.Checked;
end;

procedure TfrmConvertInterface.btnSetGlobalMultiplierClick(Sender: TObject);
var
	i: Integer;
	ANode: TcxTreeListNode;
	icolMultiplier: Integer;
	nMultiplier: Double;
	sMultiplier: String;
	ACaption, APrompt: WideString;
begin
	inherited;

	ACaption := 'Global Multiplier';
	APrompt := 'Enter a multiplier to factor all nodes (without an existing multiplier) by';
	sMultiplier := FloatToStr(nMultiplier);
	if InputQuery(ACaption, APrompt, sMultiplier) then
	begin
		icolMultiplier := cxColMultiplier.ItemIndex;
		for i := 0 to treeNodes.Count - 1 do
		begin
			ANode := treeNodes.Items[i];
			if Length(ANode.Texts[iColMultiplier]) = 0 then
				ANode.Values[icolMultiplier] := sMultiplier;
		end;
	end;
end;

procedure TfrmConvertInterface.rgrpConvertFormatClick(Sender: TObject);
begin
	inherited;
	tabConvertType.ActivePageIndex := rgrpConvertFormat.ItemIndex;
end;

procedure TfrmConvertInterface.cxColIncludePropertiesEditValueChanged(
  Sender: TObject);
begin
  if treeNodes.FocusedNode.Values[cxColInclude.ItemIndex] = 'True' then
    Inc(NumIncluded)
  else
    Dec(NumIncluded);
end;

procedure TfrmConvertInterface.treeNodesFocusedColumnChanged(Sender: TObject;
  APrevFocusedColumn, AFocusedColumn: TcxTreeListColumn);
begin
    treeNodes.OptionsBehavior.IncSearch := (AFocusedColumn = cxColOriginalID);
end;

procedure TfrmConvertInterface.treeNodesCustomDrawFooterCell(Sender: TObject;
  ACanvas: TcxCanvas; AViewInfo: TcxTreeListFooterItemViewInfo;
  var ADone: Boolean);
var
  IncludeCol: Integer;
  i: Integer;
  NumIncluded: Integer;
  AText: String;
begin
  if AViewInfo.Column.Column = cxColInclude then
  begin
    NumIncluded := 0;
    for i := 0 to treeNodes.Count-1 do
    begin
      if treeNodes.Items[i].Values[cxColInclude.ItemIndex] = 'True' then
        Inc(NumIncluded);
    end;
    AText := Format('%d Included', [NumIncluded]);
    ACanvas.DrawText(AViewInfo.Text, AViewInfo.VisibleRect, 0, True)
  end;
end;

{ TIMMOUSEConvertOptions }

procedure TIMMOUSEConvertOptions.SetOption(const Name: String; Value: Variant);
var
  OptionStr: String;
begin
  OptionStr := Uppercase(Name);
  try
    if OptionStr = 'EVENTTYPE' then
      fEventType := Value
    else if OptionStr = 'FACTOR' then
      fFactor := Value
    else if OptionStr = 'GENERATEMOUSEBSF' then
      fGenerateMOUSEBSF := Value
    else if OptionStr = 'GENERATEMOUSEBSFNAME' then
      fGenerateMOUSEBSFName := Value
    else if OptionStr = 'GENERATEMOUSETIMESERIES' then
      fGenerateMOUSETimeSeries := Value
    else if OptionStr = 'MOUSEDBNAME' then
      fMOUSEDBName := Value
    else if OptionStr = 'OFFSET' then
      fOffset := Value
    else if OptionStr = 'UNITS' then
      fUnits := Value;
  except
    on EVariantTypeCastError do Assert(True, 'Invalid variant assignment' + #13 +
      'TIMMouseConvertOptions.SetOption' + #13 + Name + '=' + Value);
  end;
end;

function TIMMOUSEConvertOptions.GetOption(const Name: String): Variant;
var
  OptionStr: String;
begin
  OptionStr := Uppercase(Name);
  if OptionStr = 'EVENTTYPE' then
    Result := fEventType
  else if OptionStr = 'FACTOR' then
    Result := fFactor
  else if OptionStr = 'GENERATEMOUSEBSF' then
    Result := fGenerateMOUSEBSF
  else if OptionStr = 'GENERATEMOUSEBSFNAME' then
    Result := fGenerateMOUSEBSFName
  else if OptionStr = 'GENERATEMOUSETIMESERIES' then
    Result := fGenerateMOUSETimeSeries
  else if OptionStr = 'MOUSEDBNAME' then
    Result := fMOUSEDBName
  else if OptionStr = 'OFFSET' then
    Result := fOffset
  else if OptionStr = 'UNITS' then
    Result := fUnits;
end;

{ TIMSWMMConvertOptions }

procedure TIMSWMMConvertOptions.SetOption(const Name: String; Value: Variant);
var
  OptionStr: String;
begin
  OptionStr := Uppercase(Name);
  if OptionStr = 'ALPHANUMERICIDS' then
    fAlphaNumericIDs := Value
  else if OptionStr = 'AREA' then
    fArea := Value
  else if OptionStr = 'FLOWMULTIPLIER' then
    fFlowMultiplier := Value
  else if OptionStr = 'SKIPINTERVAL' then
    fSkipInterval := Value
  else if OptionStr = 'SOURCEBLOCK' then
    fSourceBlock := Value
  else if OptionStr = 'SWMMFORMAT' then
    fSWMMFormat := Value
  else if OptionStr = 'TITLE1' then
    fTitles[1] := Value
  else if OptionStr = 'TITLE2' then
    fTitles[2] := Value
  else if OptionStr = 'TITLE3' then
    fTitles[3] := Value
  else if OptionStr = 'TITLE4' then
    fTitles[4] := Value;
end;

procedure TIMSWMMConvertOptions.SetTitles(Index: Integer; AString: String);
begin
  fTitles[Index] := AString;
end;

function TIMSWMMConvertOptions.GetOption(const Name: String): Variant;
var
  OptionStr: String;
begin
  OptionStr := Uppercase(Name);
  if OptionStr = 'ALPHANUMERICIDS' then
    Result := fAlphaNumericIDs
  else if OptionStr = 'AREA' then
    Result := fArea
  else if OptionStr = 'FLOWMULTIPLIER' then
    Result := fFlowMultiplier
  else if OptionStr = 'SKIPINTERVAL' then
    Result := fSkipInterval
  else if OptionStr = 'SOURCEBLOCK' then
    Result := fSourceBlock
  else if OptionStr = 'SWMMFORMAT' then
    Result := fSWMMFormat
  else if OptionStr = 'TITLE1' then
    Result := fTitles[1]
  else if OptionStr = 'TITLE2' then
    Result := fTitles[2]
  else if OptionStr = 'TITLE3' then
    Result := fTitles[3]
  else if OptionStr = 'TITLE4' then
    Result := fTitles[4];
end;

function TIMSWMMConvertOptions.GetTitles(Index: Integer): String;
begin
  Result := fTitles[Index];
end;

{ TIMSYFConvertOptions }

procedure TIMSYFConvertOptions.SetOption(const Name: String; Value: Variant);
var
  OptionStr: String;
begin
  OptionStr := Uppercase(Name);
  if OptionStr = 'LINKDEPTHS' then
    fLinkDepths := Value
  else if OptionStr = 'LINKFLOWS' then
    fLinkFlows := Value
  else if OptionStr = 'LINKVELOCITIES' then
    fLinkVelocities := Value
  else if OptionStr = 'NODEDEPTHS' then
    fNodeDepths := Value
  else if OptionStr = 'NODEINVERTS' then
    fNodeInverts := Value;
end;

function TIMSYFConvertOptions.GetOption(const Name: String): Variant;
var
  OptionStr: String;
begin
  OptionStr := Uppercase(Name);
  if OptionStr = 'LINKDEPTHS' then
    Result := fLinkDepths
  else if OptionStr = 'LINKFLOWS' then
    Result := fLinkFlows
  else if OptionStr = 'LINKVELOCITIES' then
    Result := fLinkVelocities
  else if OptionStr = 'NODEDEPTHS' then
    Result := fNodeDepths
  else if OptionStr = 'NODEINVERTS' then
    Result := fNodeInverts;
end;

{ TIMConvertNodeOptions }

procedure TIMConvertNodeOptions.SetOption(const Name: String; Value: Variant);
var
  OptionStr: String;
begin
  OptionStr := Uppercase(Name);
  if OptionStr = 'ASSIGNTONODE' then
    fAssignToNode := Value
  else if OptionStr = 'INCLUDE' then
    fInclude := Value
  else if OptionStr = 'MULTIPLIER' then
    fMultiplier := Value
  else if OptionStr = 'NEWTIMESERIESID' then
    fNewTimeSeriesID := Value
  else if OptionStr = 'ORIGINALID' then
    fOriginalID := Value
  else if OptionStr = 'REPLACEWITH' then
    fReplaceWith := Value;
end;

function TIMConvertNodeOptions.GetOption(const Name: String): Variant;
var
  OptionStr: String;
begin
  OptionStr := Uppercase(Name);
  if OptionStr = 'ASSIGNTONODE' then
    Result := fAssignToNode
  else if OptionStr = 'INCLUDE' then
    Result := fInclude
  else if OptionStr = 'MULTIPLIER' then
    Result := fMultiplier
  else if OptionStr = 'NEWTIMESERIESID' then
    Result := fNewTimeSeriesID
  else if OptionStr = 'ORIGINALID' then
    Result := fOriginalID
  else if OptionStr = 'REPLACEWITH' then
    Result := fReplaceWith;
end;

{ TIMConvertParams }

function TIMConvertParams.GetConvertNodeOptions(
  Index: Integer): TIMConvertNodeOptions;
begin

end;

constructor TIMConvertParams.Create(ASourceFormat: TInterfaceFormat);
begin

end;

function TIMConvertParams.AddConvertNode: TIMConvertNodeOptions;
begin

end;

destructor TIMConvertParams.Destroy;
begin

end;

end.
