unit fCalculateFlowStatistics;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, fModuleMaster, StdCtrls, RzLabel, RzPrgres, RzButton,
  RzLstBox, RzChkLst, RzBorder,
  InterfaceStreams, StStrms, DateUtils, Contnrs, StDQue, StList, Math, ConvUtils,
  StdConvs, CodeSiteLogging, Mask, RzEdit, RzBtnEdt, ExtCtrls, RzPanel, RzRadChk,
  RzSpnEdt, cxGraphics, cxCustomData, cxStyles, cxTL, cxControls,
  cxInplaceContainer, cxTextEdit, cxCheckBox, cxSpinEdit;

const
  MinEventDuration: Double = 15.0;

type
  TFlowFile = class;
  TNodeFlowStats = class;
  TfrmCalculateFlowStatistics = class(TfrmModuleMaster)
    lblStep1: TRzLabel;
    lblSelectNodes: TRzLabel;
    RzLabel3: TRzLabel;
    RzLabel16: TRzLabel;
    btnProcessStatistics: TRzButton;
    btnStop: TRzButton;
    lblFileProgress: TRzLabel;
    prgFile: TRzProgressBar;
    RzLabel6: TRzLabel;
    edtOpenFile: TRzButtonEdit;
    pnlOptions: TRzPanel;
		edtSaveFile: TRzButtonEdit;
    RzLabel2: TRzLabel;
    RzLabel4: TRzLabel;
    RzLabel5: TRzLabel;
    chklstWinterSeason: TRzCheckList;
    chkDebugLog: TRzCheckBox;
    chkEventLog: TRzCheckBox;
    edtMinIntereventTime: TRzSpinEdit;
    btnCloseTask: TRzButton;
    RzButton1: TRzButton;
    RzButton2: TRzButton;
    RzButton3: TRzButton;
    treeNodes: TcxTreeList;
    cxColRecord: TcxTreeListColumn;
    cxColInclude: TcxTreeListColumn;
    cxColOriginalID: TcxTreeListColumn;
    cxColNewID: TcxTreeListColumn;
    cxColMinFlow: TcxTreeListColumn;
		procedure btnProcessStatisticsClick(Sender: TObject);
		procedure edtOpenFileButtonClick(Sender: TObject);
    procedure edtSaveFileButtonClick(Sender: TObject);
    procedure edtOpenFileChange(Sender: TObject);
    procedure btnCloseTaskClick(Sender: TObject);
    procedure RzButton1Click(Sender: TObject);
    procedure RzButton2Click(Sender: TObject);
    procedure RzButton3Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
		InterfaceFile: ISWMMStandardInterfaceFile;
		OutputBackEndFile: TFileStream;
		OutputFile: TStAnsiTextStream;
		IsProcessing: Boolean;
    FlowFile: TFlowFile;
  end;

  TEventRec = class
  private
    function GetNumCalendarDays: Integer;
  public
    BeginDate: TDateTime;
    EndDate: TDateTime;
    Volume: Double;
    Peak: Double;
    FourHourPeak: Double;
    HourPeak: Double;
    property NumCalendarDays: Integer read GetNumCalendarDays;
  end;

  TFlowRec = class
  public
    FlowTime: TDateTime;
    Flow: Double;
    constructor Create(AFlowTime: TDateTime; AFlow: Double);
  end;

  TFlowFile = class
  private
    fBeginDate: TDateTime;
    fEndDate: TDateTime;
    fNodeFlowStats: TObjectList;
    function GetNumYears: Double;
    function GetNumCalendarYears: Integer;
  public
    constructor Create;
    destructor Destroy; override;
    property NumYears: Double read GetNumYears;
    property NumCalendarYears: Integer read GetNumCalendarYears;
  end;

  TStatsPeriod = (spAnnual, spJanuary, spFebruary, spMarch, spApril, spMay,
    spJune, spJuly, spAugust, spSeptember, spOctober, spNovember, spDecember,
    spWinter, spSummer);

	TNodeFlowStats = class
  private
    fNodeID: String;
    fEvents: TStList;
    fFourHourFlowRecord: TStDQue;
    fHourFlowRecord: TStDQue;
    fFlowFile: TFlowFile;
    fTopFiveHourPeaks: TStDQue;
    fTopFiveFourHourPeaks: TStDQue;
    fWinterMonths: TStringList;
    function GetTotalVolume: Double;
    function GetTotalDays: Double;
    function GetTotalCalendarDays: Integer;
    function GetAverageDaysPerYear: Double;
    function GetAverageDaysPerEvent: Double;
    function GetAverageCalendarDaysPerYear: Integer;
    function GetAverageCalendarDaysPerEvent: Integer;
    function GetNumEvents: Integer;
  public
    constructor Create(AFlowFile: TFlowFile);
    destructor Destroy; override;
    property NodeID: String read fNodeID write fNodeID;
    property FlowFile: TFlowFile read fFlowFile;
    property TotalVolume: Double read GetTotalVolume;
    property TotalDays: Double read GetTotalDays;
    property TotalCalendarDays: Integer read GetTotalCalendarDays;
    property AverageDaysPerYear: Double read GetAverageDaysPerYear;
    property AverageDaysPerEvent: Double read GetAverageDaysPerEvent;
    property AverageCalendarDaysPerYear: Integer read GetAverageCalendarDaysPerYear;
    property AverageCalendarDaysPerEvent: Integer read GetAverageCalendarDaysPerEvent;
    property NumEvents: Integer read GetNumEvents;
    procedure UpdateFlowRecords(FlowTime: TDateTime; Flow: Double;
      TimeStep: Double);
    procedure UpdateEventRecords(FlowTime: TDateTime; Flow: Double;
      TimeStep: Double);
    function ActiveEventOnDate(ADate: TDateTime; MIT: Double): Boolean;
    function CurrentFourHourFlow: Double;
    function CurrentHourFlow: Double;
    procedure OpenNewEvent(StartDate: TDateTime);
    procedure RemoveShortEvents;
    function SummarizeEvents(AYear: Word; APeriod: TStatsPeriod): String;
    function SummarizeAverageEvents(APeriod: TStatsPeriod): String;
    procedure InitWinterMonths(AChkList: TRzCheckList);
    function IsWinter(AMonth: Word): Boolean;
	end;

var
  frmCalculateFlowStatistics: TfrmCalculateFlowStatistics;

implementation

uses PDXDateUtils, fMain, StBase;

{$R *.dfm}

procedure DisposeTEventRec(Data: Pointer);
begin
  TEventRec(Data).Free;
end;

procedure DisposeTFlowRec(Data: Pointer);
begin
  TFlowRec(Data).Free;
end;

procedure DisposeDouble(Data: Pointer);
begin
  Dispose(PDouble(Data));
end;

function CompareDoubles(Data1, Data2 : Pointer) : Integer;
begin
  Result := Round(PDouble(Data1)^ - PDouble(Data2)^);
end;

function CompareTopFloatStr(List: TStringList; Index1, Index2: Integer): Integer;
begin
  Result := -Round(StrToFloat(List[Index1]) - StrToFLoat(List[Index2]));
end;


procedure TfrmCalculateFlowStatistics.btnProcessStatisticsClick(
  Sender: TObject);
var
  FileSize: Int64;
  i,j,k: Integer;
  AddedNodeIndex: Integer;
  IncludeCol: Integer;
  OriginalIDCol: Integer;
  NewIDCol: Integer;
  MinFlowCol: Integer;
  CurrentNodeFlowStats: TNodeFlowStats;
  CurrentEventRec: TEventRec;
  NodeID: String;
  WriteString: String;
  FirstRead: Boolean;
begin
  inherited;
  CodeSite.EnterMethod('btnProcessStatisticsClick');
	btnStop.Enabled := True;
  btnProcessStatistics.Enabled := False;

  try
		OutputBackEndFile := TFileStream.Create(edtSaveFile.Text, fmCreate);
		OutputFile := TStAnsiTextStream.Create(OutputBackEndFile);
		FileSize := InterfaceFile.Stream.Size;

    IsProcessing := True;
    Screen.Cursor := crHourglass;

    IncludeCol := cxColInclude.ItemIndex;
    OriginalIDCol := cxColOriginalID.ItemIndex;
    NewIDCol := cxColNewID.ItemIndex;
    MinFlowCol := cxColMinFlow.ItemIndex;

    // Set up stats objects
    CodeSite.SendMsg('Setting up stats objects');
    for i := 0 to TreeNodes.Count-1 do
      if TreeNodes.Items[i].Values[IncludeCol] = 'True' then
      begin
        AddedNodeIndex := FlowFile.fNodeFlowStats.Add(TNodeFlowStats.Create(FlowFile));
        with FlowFile.fNodeFlowStats[AddedNodeIndex] as TNodeFlowStats do
				begin

          if (TreeNodes.Items[i].Values[NewIDCol] <> '') and
            (TreeNodes.Items[i].Values[NewIDCol] <> Null) then
            NodeID := TreeNodes.Items[i].Values[NewIDCol]
          else
            NodeID := TreeNodes.Items[i].Values[OriginalIDCol];
        end;
      end
      else
        // These are unused but necessary to keep indices in sync
        FlowFile.fNodeFlowStats.Add(TNodeFlowStats.Create(FlowFile));

    FirstRead := True;
    while (not InterfaceFile.IsEOF) and IsProcessing do
    begin
			InterfaceFile.ReadFlows;
      CodeSite.SendFmtMsg('Reading flows %s',[FormatDateTime('dd/mm/yyyy hh:mm:ss', InterfaceFile.CurrentTime)]);
      if FirstRead then
      begin
        FlowFile.fBeginDate := InterfaceFile.CurrentTime;
        FirstRead := False;
      end;

      // Rifle through nodes
      for i := 0 to TreeNodes.Count-1 do
      begin
        if TreeNodes.Items[i].Values[IncludeCol] = 'False' then
          Continue;

        CurrentNodeFlowStats := TNodeFlowStats(FlowFile.fNodeFlowStats[i]);

        // Is there flow above minimum for flow at current step?
        if InterfaceFile.Flows[i] > StrToFloat(TreeNodes.Items[i].Values[MinFlowCol]) then
        begin
          // If so, open a new event if the node is not currently in an event
          if not CurrentNodeFlowStats.ActiveEventOnDate(InterfaceFile.CurrentTime,
            edtMinIntereventTime.Value) then
          begin
            CurrentNodeFlowStats.OpenNewEvent(InterfaceFile.CurrentTime);
          end;
          CurrentNodeFlowStats.UpdateEventRecords(InterfaceFile.CurrentTime,
            InterfaceFile.Flows[i], InterfaceFile.CurrentTimeStep);
          CurrentNodeFlowStats.UpdateFlowRecords(InterfaceFile.CurrentTime,
            InterfaceFile.Flows[i], InterfaceFile.CurrentTimeStep);
        end
        else
          CurrentNodeFlowStats.UpdateFlowRecords(InterfaceFile.CurrentTime,
            0, InterfaceFile.CurrentTimeStep);
      end;
      prgFile.PartsComplete := Round(InterfaceFile.Stream.Position / FileSize*100);
      lblFileProgress.Caption := Format('File Progress (%d)', [Y2KJulDateOfDateTime(InterfaceFile.CurrentTime)]);
      Application.ProcessMessages;
		end;
		FlowFile.fEndDate := InterfaceFile.CurrentTime;

		// Remove short events
		for i := 0 to TreeNodes.Count-1 do
		begin
			if TreeNodes.Items[i].Values[IncludeCol] = 'False' then
				Continue;
			CurrentNodeFlowStats := TNodeFlowStats(FlowFile.fNodeFlowStats[i]);
			CurrentNodeFlowStats.RemoveShortEvents;
		end;

		// Debug log results
		for i := 0 to TreeNodes.Count-1 do
		begin
			if TreeNodes.Items[i].Values[IncludeCol] = 'False' then
				Continue;
			if (TreeNodes.Items[i].Values[NewIDCol] <> '') and
        (TreeNodes.Items[i].Values[NewIDCol] <> Null ) then
				NodeID := TreeNodes.Items[i].Values[NewIDCol]
			else
				NodeID := TreeNodes.Items[i].Values[OriginalIDCol];
			CurrentNodeFlowStats := TNodeFlowStats(FlowFile.fNodeFlowStats[i]);
			for j := 0 to CurrentNodeFlowStats.fEvents.Count-1 do
			begin
				CurrentEventRec := TEventRec(CurrentNodeFlowStats.fEvents[j].Data);
				WriteString := NodeID + ',' + FormatDateTime('mm/dd/yyyy hh:mm:ss', CurrentEventRec.BeginDate);
				WriteString := WriteString + ',' + FormatDateTime('mm/dd/yyyy hh:mm:ss', CurrentEventRec.EndDate);
				WriteString := Writestring + ',' + Format('%.1f cfs,%.1f cf',
					[CurrentEventRec.Peak, CurrentEventRec.Volume]);
				OutputFile.WriteLine(WriteString);
			end;
		end;

		// WWStats results
		OutputFile.WriteLine('Node,Year,Period,NumEvents,OverflowVol,OverflowVolPerEvent,'+
			'MaxVol1,MaxVol2,MaxVol3,MaxVol4,OverflowDur,OverflowDurPerEvt,MaxFlow1,'+
			'MaxFlow2,MaxFlow3,Max4HrFlow1,Max4HrFlow2,Max4HrFlow3,OverflowDays,'+
      'OverflowDaysPerEvent');
    for i := 0 to TreeNodes.Count-1 do
    begin
      if TreeNodes.Items[i].Values[IncludeCol] = 'False' then
        Continue;
      CurrentNodeFlowStats := TNodeFlowStats(FlowFile.fNodeFlowStats[i]);
      CurrentNodeFlowStats.InitWinterMonths(chklstWinterSeason);
      for j := YearOf(FlowFile.fBeginDate) to YearOf(FlowFile.fEndDate) do
      begin
        for k := 1 to 12 do
        begin
          WriteString := CurrentNodeFlowStats.SummarizeEvents(j, TStatsPeriod(k));
          OutputFile.WriteLine(WriteString);
        end;
        WriteString := CurrentNodeFlowStats.SummarizeEvents(j, spAnnual);
        OutputFile.WriteLine(WriteString);
        WriteString := CurrentNodeFlowStats.SummarizeEvents(j, spWinter);
        OutputFile.WriteLine(WriteString);
        WriteString := CurrentNodeFlowStats.SummarizeEvents(j, spSummer);
        OutputFile.WriteLine(WriteString);
      end;
      WriteString := CurrentNodeFlowStats.SummarizeAverageEvents(spAnnual);
      OutputFile.WriteLine(WriteString);
      WriteString := CurrentNodeFlowStats.SummarizeAverageEvents(spWinter);
      OutputFile.WriteLine(WriteString);
      WriteString := CurrentNodeFlowStats.SummarizeAverageEvents(spSummer);
      OutputFile.WriteLine(WriteString);
    end;
  finally
    IsProcessing := False;
    Screen.Cursor := crDefault;
    btnStop.Enabled := False;
    btnProcessStatistics.Enabled := True;
    OutputFile.Free;
    OutputBackEndFile.Free;
    CodeSite.ExitMethod('btnProcessStatisticsClick');
	end;
end;

{ TNodeFlowStats }

function TNodeFlowStats.ActiveEventOnDate(ADate: TDateTime; MIT: Double): Boolean;
var
  i: Integer;
begin
  Result := False;
  for i := 0 to fEvents.Count-2 do
  begin
    with TEventRec(fEvents[i].Data) do
      if DateIsWithin(ADate, BeginDate, EndDate) then
      begin
        Result := True;
        Break;
      end;
  end;
  if fEvents.Count > 0 then
    with TEventRec(fEvents[fEvents.Count-1].Data) do
      if (not Result) and DateIsWithin(ADate, BeginDate, EndDate+MIT/24) then
        Result := True;
end;

constructor TNodeFlowStats.Create(AFlowFile: TFlowFile);
begin
  fFlowFile := AFlowFile;
  fHourFlowRecord := TStDQue.Create(TStListNode);
  fHourFlowRecord.DisposeData := DisposeTFlowRec;
  fFourHourFlowRecord := TStDQue.Create(TStListNode);
  fFourHourFlowRecord.DisposeData := DisposeTFlowRec;
  fEvents := TStList.Create(TStListNode);
  fEvents.DisposeData := DisposeTEventRec;
  fTopFiveFourHourPeaks := TStDQue.Create(TStListNode);
  fTopFiveFourHourPeaks.DisposeData := DisposeDouble;
	fTopFiveFourHourPeaks.Compare := CompareDoubles;
  fTopFiveHourPeaks := TStDQue.Create(TStListNode);
  fTopFiveHourPeaks.DisposeData := DisposeDouble;
  fTopFiveHourPeaks.Compare := CompareDoubles;
  fWinterMonths := TStringList.Create;
end;

destructor TNodeFlowStats.Destroy;
begin
  fHourFlowRecord.Free;
  fFourHourFlowRecord.Free;
  fEvents.Free;
  fTopFiveFourHourPeaks.Free;
  fTopFiveHourPeaks.Free;
  fWinterMonths.Free;
  inherited;
end;

function TNodeFlowStats.GetAverageCalendarDaysPerEvent: Integer;
begin
  Result := TotalCalendarDays div NumEvents;
end;

function TNodeFlowStats.GetAverageCalendarDaysPerYear: Integer;
begin
  Result := TotalCalendarDays div FlowFile.NumCalendarYears;
end;

function TNodeFlowStats.GetAverageDaysPerEvent: Double;
var
  i: Integer;
begin
  Result := 0;
  for i := 0 to fEvents.Count-1 do
    with TEventRec(fEvents[i].Data) do
      Result := Result + (EndDate - BeginDate);
  Result := Result / NumEvents;
end;

function TNodeFlowStats.GetAverageDaysPerYear: Double;
begin
  Result := TotalDays / FlowFile.NumYears;
end;

function TNodeFlowStats.CurrentFourHourFlow: Double;
var
  i: Integer;
begin
  Result := 0.0;
  for i := 0 to fFourHourFlowRecord.Count-1 do
  begin
    Result := Result + TFlowRec(fFourHourFlowRecord[i].Data).Flow;
{    with TFlowRec(fFourHourFlowRecord[i].Data) do
      CodeSite.SendFmtMsg('4Hr %s %s %.2f',
        [NodeID,
        FormatDateTime('mm/dd/yyyy hh:mm', FlowTime),
        Flow]);}
  end;
  Result := Result / fFourHourFlowRecord.Count;
end;

function TNodeFlowStats.CurrentHourFlow: Double;
var
  i: Integer;
begin
  Result := 0.0;
  for i := 0 to fHourFlowRecord.Count-1 do
  begin
    Result := Result + TFlowRec(fHourFlowRecord[i].Data).Flow;
{		with TFlowRec(fHourFlowRecord[i].Data) do
			CodeSite.SendFmtMsg('1Hr %s %s %.2f',
				[NodeID,
				FormatDateTime('mm/dd/yyyy hh:mm', FlowTime),
        Flow]);}
  end;
  Result := Result / fHourFlowRecord.Count;
end;

function TNodeFlowStats.GetNumEvents: Integer;
begin
  Result := fEvents.Count;
end;

function TNodeFlowStats.GetTotalCalendarDays: Integer;
var
  i: Integer;
begin
  Result := 0;
  for i := 0 to fEvents.Count-1 do
    with TEventRec(fEvents[i]) do
      Result := Result + NumCalendarDays;
end;

function TNodeFlowStats.GetTotalDays: Double;
var
  i: Integer;
begin
  Result := 0;
  for i := 0 to fEvents.Count-1 do
    with TEventRec(fEvents[i]) do
      Result := Result + (EndDate - BeginDate);
end;

function TNodeFlowStats.GetTotalVolume: Double;
var
  I: Integer;
begin
  Result := 0;
  for i := 0 to fEvents.Count-1 do
    Result := Result + TEventRec(fEvents[i]).Volume;
end;

procedure TNodeFlowStats.UpdateEventRecords(FlowTime: TDateTime;
  Flow: Double; TimeStep: Double);
begin
  // Update event record
  with TEventRec(fEvents[fEvents.Count-1].Data) do
  begin
    EndDate := FlowTime;
    if Flow > Peak then
      Peak := Flow;
    if CurrentFourHourFlow > FourHourPeak then
    begin
{			CodeSite.SendFmtMsg('%s %s 4Hr %.2f old %.2f',
				[FormatDateTime('mm/dd/yyyy hh:mm:ss', FlowTime),
				NodeID, CurrentFourHourFlow,
				FourHourPeak]);}
			FourHourPeak := CurrentFourHourFlow;
		end;
		if CurrentHourFlow > HourPeak then
		begin
{			CodeSite.SendFmtMsg('%s %s 1Hr %.2f old %.2f',
				[FormatDateTime('mm/dd/yyyy hh:mm:ss', FlowTime),
				NodeID, CurrentHourFlow,
				HourPeak]);}
			HourPeak := CurrentHourFlow;
		end;
		Volume := Volume + Flow*TimeStep;
		CodeSite.SendFmtMsg('Vol:%.2f Flow:%.2f Step:%.2f', [Volume, Flow, TimeStep]);
  end;
end;

procedure TNodeFlowStats.OpenNewEvent(StartDate: TDateTime);
var
  NewEvent: TEventRec;
begin
  NewEvent := TEventRec.Create;
  with NewEvent do
  begin
    BeginDate := StartDate;
    EndDate := StartDate;
    Peak := 0.0;
    Volume := 0.0;
  end;
  fEvents.Append(NewEvent);
end;

procedure TNodeFlowStats.RemoveShortEvents;
var
  i: Integer;
begin
  for i := fEvents.Count-1 downto 0 do
    with TEventRec(fEvents[i].Data) do
      if MinuteSpan(EndDate, BeginDate) <= MinEventDuration then
        fEvents.Delete(fEvents[i]);
end;

function TNodeFlowStats.SummarizeEvents(AYear: Word;
  APeriod: TStatsPeriod): String;
var
  i: Integer;
  NumEvents: Integer;
  OverflowVol: Double;
  MaxEventVols: TStringList;
  OverflowDuration: Double;
  MaxHourPeaks: TStringList;
  MaxFourHourPeaks: TStringList;
  NumCalDays: Integer;
  PeriodString: String;
  OverflowPerEvent: Double;
  OverflowDurationPerEvent: Double;
  CalDaysPerEvent: Double;
begin
  MaxEventVols := TStringList.Create;
  MaxHourPeaks := TStringList.Create;
  MaxFourHourPeaks := TStringList.Create;

  try
    for i := 0 to fEvents.Count-1 do
      with TEventRec(fEvents[i].Data) do
      begin
        case APeriod of
          spAnnual:
            begin
              if YearOf(BeginDate) = AYear then
              begin
                Inc(NumEvents);
                OverflowVol := OverflowVol + Volume;
                OverflowDuration := OverflowDuration + (EndDate-BeginDate)*24;
                MaxEventVols.Append(FloatToStr(Volume));
                MaxHourPeaks.Append(FloatToStr(HourPeak));
                MaxFourHourPeaks.Append(FloatToStr(FourHourPeak));
                NumCalDays := NumCalDays + NumCalendarDays;
              end;
            end;
          spWinter:
            begin
              if (YearOf(BeginDate) = AYear) and
                (IsWinter(MonthOf(BeginDate))) then
              begin
                Inc(NumEvents);
                OverflowVol := OverflowVol + Volume;
                OverflowDuration := OverflowDuration + (EndDate-BeginDate)*24;
                MaxEventVols.Append(FloatToStr(Volume));
                MaxHourPeaks.Append(FloatToStr(HourPeak));
                MaxFourHourPeaks.Append(FloatToStr(FourHourPeak));
                NumCalDays := NumCalDays + NumCalendarDays;
              end;
            end;
          spSummer:
            begin
              if (YearOf(BeginDate) = AYear) and
                (not IsWinter(MonthOf(BeginDate))) then
              begin
                Inc(NumEvents);
                OverflowVol := OverflowVol + Volume;
                OverflowDuration := OverflowDuration + (EndDate-BeginDate)*24;
                MaxEventVols.Append(FloatToStr(Volume));
                MaxHourPeaks.Append(FloatToStr(HourPeak));
                MaxFourHourPeaks.Append(FloatToStr(FourHourPeak));
                NumCalDays := NumCalDays + NumCalendarDays;
              end;
            end
          else
            begin
              if (YearOf(BeginDate) = AYear) and
								(MonthOf(BeginDate) = Ord(APeriod)) then
              begin
                Inc(NumEvents);
                OverflowVol := OverflowVol + Volume;
                OverflowDuration := OverflowDuration + (EndDate-BeginDate)*24;
                MaxEventVols.Append(FloatToStr(Volume));
                MaxHourPeaks.Append(FloatToStr(HourPeak));
                MaxFourHourPeaks.Append(FloatToStr(FourHourPeak));
                NumCalDays := NumCalDays + NumCalendarDays;
              end;
            end;
        end; // case
      end; // with TEventRec

      if MaxEventVols.Count < 4 then
        for i := MaxEventVols.Count-1 to 3 do
          MaxEventVols.Append('0');
      if MaxHourPeaks.Count < 4 then
        for i := MaxHourPeaks.Count-1 to 3 do
          MaxHourPeaks.Append('0');
      if MaxFourHourPeaks.Count < 4 then
        for i := MaxFourHourPeaks.Count-1 to 3 do
          MaxFourHourPeaks.Append('0');

      MaxEventVols.CustomSort(CompareTopFloatStr);
      MaxHourPeaks.CustomSort(CompareTopFloatStr);
      MaxFourHourPeaks.CustomSort(CompareTopFloatStr);

      case APeriod of
        spAnnual: PeriodString := 'Annual';
        spWinter: PeriodString := 'Winter';
        spSummer: PeriodString := 'Summer';
        else
          PeriodString := IntToStr(Ord(APeriod));
      end;
      if NumEvents > 0 then
      begin
        OverflowPerEvent := OverflowVol/NumEvents;
        OverflowDurationPerEvent := OverflowDuration/NumEvents;
        CalDaysPerEvent := NumCalDays/NumEvents;
      end;
      Result := Format('%s,%d,%s,%d,%.1f,%.1f,%.1f,%.1f,%.1f,%.1f,'+
        '%.1f,%.1f,%.1f,%.1f,%.1f,%.1f,%.1f,%.1f,%d,%.1f',
        [fNodeID,
        AYear,
        PeriodString,
        NumEvents,
        Convert(OverflowVol, vuCubicFeet, vuFluidGallons)*1e-6,
        Convert(OverflowPerEvent, vuCubicFeet, vuFluidGallons)*1e-6,
        Convert(StrToFloat(MaxEventVols[0]), vuCubicFeet, vuFluidGallons)*1e-6,
        Convert(StrToFloat(MaxEventVols[1]), vuCubicFeet, vuFluidGallons)*1e-6,
        Convert(StrToFloat(MaxEventVols[2]), vuCubicFeet, vuFluidGallons)*1e-6,
        Convert(StrToFloat(MaxEventVols[3]), vuCubicFeet, vuFluidGallons)*1e-6,
        OverflowDuration,
        OverflowDurationPerEvent,
        StrToFloat(MaxHourPeaks[0]),
        StrToFloat(MaxHourPeaks[1]),
        StrToFloat(MaxHourPeaks[2]),
        StrToFloat(MaxFourHourPeaks[0]),
        StrToFloat(MaxFourHourPeaks[1]),
        StrToFloat(MaxFourHourPeaks[2]),
        NumCalDays,
        CalDaysPerEvent]);
  finally
    MaxEventVols.Free;
    MaxHourPeaks.Free;
    MaxFourHourPeaks.Free;
  end;
end;

procedure TNodeFlowStats.InitWinterMonths(AChkList: TRzCheckList);
var
  i: Integer;
begin
  for i := 0 to AChkList.Count-1 do
    if AChkList.ItemChecked[i] then
      fWinterMonths.Add(IntToStr(i+1));
end;

function TNodeFlowStats.IsWinter(AMonth: Word): Boolean;
begin
  Result := fWinterMonths.IndexOf(IntToStr(AMonth)) > -1;
end;

function TNodeFlowStats.SummarizeAverageEvents(
  APeriod: TStatsPeriod): String;
var
  i: Integer;
  NumEvents: Integer;
  OverflowVol: Double;
  MaxEventVols: TStringList;
  OverflowDuration: Double;
  MaxHourPeaks: TStringList;
  MaxFourHourPeaks: TStringList;
  NumCalDays: Integer;
  PeriodString: String;
  OverflowPerEvent: Double;
  OverflowDurationPerEvent: Double;
  CalDaysPerEvent: Double;
begin
  MaxEventVols := TStringList.Create;
  MaxHourPeaks := TStringList.Create;
  MaxFourHourPeaks := TStringList.Create;

  try
    for i := 0 to fEvents.Count-1 do
      with TEventRec(fEvents[i].Data) do
      begin
        case APeriod of
          spAnnual:
            begin
              Inc(NumEvents);
              OverflowVol := OverflowVol + Volume;
              OverflowDuration := OverflowDuration + (EndDate-BeginDate)*24;
              MaxEventVols.Append(FloatToStr(Volume));
              MaxHourPeaks.Append(FloatToStr(HourPeak));
              MaxFourHourPeaks.Append(FloatToStr(FourHourPeak));
              NumCalDays := NumCalDays + NumCalendarDays;
            end;
          spWinter:
            begin
              if (IsWinter(MonthOf(BeginDate))) then
              begin
                Inc(NumEvents);
                OverflowVol := OverflowVol + Volume;
                OverflowDuration := OverflowDuration + (EndDate-BeginDate)*24;
                MaxEventVols.Append(FloatToStr(Volume));
                MaxHourPeaks.Append(FloatToStr(HourPeak));
                MaxFourHourPeaks.Append(FloatToStr(FourHourPeak));
                NumCalDays := NumCalDays + NumCalendarDays;
              end;
            end;
          spSummer:
            begin
              if (not IsWinter(MonthOf(BeginDate))) then
              begin
                Inc(NumEvents);
                OverflowVol := OverflowVol + Volume;
                OverflowDuration := OverflowDuration + (EndDate-BeginDate)*24;
                MaxEventVols.Append(FloatToStr(Volume));
                MaxHourPeaks.Append(FloatToStr(HourPeak));
                MaxFourHourPeaks.Append(FloatToStr(FourHourPeak));
                NumCalDays := NumCalDays + NumCalendarDays;
              end;
            end
          else
            begin
              if (MonthOf(BeginDate) = Ord(APeriod)) then
              begin
                Inc(NumEvents);
                OverflowVol := OverflowVol + Volume;
                OverflowDuration := OverflowDuration + (EndDate-BeginDate)*24;
                MaxEventVols.Append(FloatToStr(Volume));
                MaxHourPeaks.Append(FloatToStr(HourPeak));
                MaxFourHourPeaks.Append(FloatToStr(FourHourPeak));
                NumCalDays := NumCalDays + NumCalendarDays;
              end;
            end;
        end; // case
      end; // with TEventRec

      if MaxEventVols.Count < 4 then
        for i := MaxEventVols.Count-1 to 3 do
          MaxEventVols.Append('0');
      if MaxHourPeaks.Count < 4 then
        for i := MaxHourPeaks.Count-1 to 3 do
          MaxHourPeaks.Append('0');
      if MaxFourHourPeaks.Count < 4 then
        for i := MaxFourHourPeaks.Count-1 to 3 do
          MaxFourHourPeaks.Append('0');

      MaxEventVols.CustomSort(CompareTopFloatStr);
      MaxHourPeaks.CustomSort(CompareTopFloatStr);
      MaxFourHourPeaks.CustomSort(CompareTopFloatStr);

      case APeriod of
        spAnnual: PeriodString := 'Annual';
        spWinter: PeriodString := 'Winter';
        spSummer: PeriodString := 'Summer';
        else
          PeriodString := IntToStr(Ord(APeriod));
      end;
      if NumEvents > 0 then
      begin
        OverflowPerEvent := OverflowVol/NumEvents;
        OverflowDurationPerEvent := OverflowDuration/NumEvents;
        CalDaysPerEvent := NumCalDays/NumEvents;
      end;
      Result := Format('%s,%s,%s,%.2f,%.1f,%.1f,%.1f,%.1f,%.1f,%.1f,'+
        '%.1f,%.1f,%.1f,%.1f,%.1f,%.1f,%.1f,%.1f,%.1f,%.1f',
				[fNodeID,
				'Average',
				PeriodString,
				NumEvents/fFlowFile.NumCalendarYears,
				Convert(OverflowVol/fFlowFile.NumCalendarYears, vuCubicFeet, vuFluidGallons)*1e-6,
				Convert(OverflowPerEvent, vuCubicFeet, vuFluidGallons)*1e-6,
				Convert(StrToFloat(MaxEventVols[0]), vuCubicFeet, vuFluidGallons)*1e-6,
				Convert(StrToFloat(MaxEventVols[1]), vuCubicFeet, vuFluidGallons)*1e-6,
				Convert(StrToFloat(MaxEventVols[2]), vuCubicFeet, vuFluidGallons)*1e-6,
				Convert(StrToFloat(MaxEventVols[3]), vuCubicFeet, vuFluidGallons)*1e-6,
				OverflowDuration/fFlowFile.NumCalendarYears,
				OverflowDurationPerEvent,
				StrToFloat(MaxHourPeaks[0]),
				StrToFloat(MaxHourPeaks[1]),
				StrToFloat(MaxHourPeaks[2]),
				StrToFloat(MaxFourHourPeaks[0]),
				StrToFloat(MaxFourHourPeaks[1]),
				StrToFloat(MaxFourHourPeaks[2]),
				NumCalDays/fFlowFile.NumCalendarYears,
        CalDaysPerEvent]);
	finally
    MaxEventVols.Free;
		MaxHourPeaks.Free;
		MaxFourHourPeaks.Free;
  end;
end;

procedure TNodeFlowStats.UpdateFlowRecords(FlowTime: TDateTime; Flow,
  TimeStep: Double);
var
  APointer: Pointer;
	AFlowRec: TFlowRec;
	NewPeakRecord: PDouble;
  i: Integer;
begin
  // Update 4Hour Running List
  fFourHourFlowRecord.PushHead(TFlowRec.Create(FlowTime, Flow));
  fFourHourFlowRecord.PeekTail(APointer);
  AFlowRec := APointer;
  while HourSpan(FlowTime, AFlowRec.FlowTime) > 4 do
  begin
    fFourHourFlowRecord.PopTail;
    fFourHourFlowRecord.PeekTail(APointer);
    AFlowRec := APointer;
  end;

  // Update Top 5 4-Hour Peaks
  New(NewPeakRecord);
  NewPeakRecord^ := CurrentFourHourFlow;
  fTopFiveFourHourPeaks.InsertSorted(NewPeakRecord);
  for i := 5 to fTopFiveFourHourPeaks.Count-1 do
    fTopFiveFourHourPeaks.Delete(fTopFiveFourHourPeaks[5]);

  // Update 1Hour Running List
  fHourFlowRecord.PushHead(TFlowRec.Create(FlowTime, Flow));
  fHourFlowRecord.PeekTail(APointer);
  AFlowRec := APointer;
  while HourSpan(FlowTime, AFlowRec.FlowTime) > 1 do
  begin
    fHourFlowRecord.PopTail;
    fHourFlowRecord.PeekTail(APointer);
    AFlowRec := APointer;
  end;

  // TODO: Expensive loop, revise
  // Update Top 5 1-Hour Peaks
  New(NewPeakRecord);
  NewPeakRecord^ := CurrentHourFlow;
  fTopFiveHourPeaks.InsertSorted(NewPeakRecord);
  for i := 5 to fTopFiveHourPeaks.Count-1 do
    fTopFiveHourPeaks.Delete(fTopFiveHourPeaks[5]);

end;

{ TFlowFile }

constructor TFlowFile.Create;
begin
  fNodeFlowStats := TObjectList.Create;
end;

destructor TFlowFile.Destroy;
begin
  fNodeFlowStats.Free;
  inherited;
end;

function TFlowFile.GetNumCalendarYears: Integer;
begin
  Result := CalendarYears(fBeginDate, fEndDate);
end;

function TFlowFile.GetNumYears: Double;
begin
  Result := YearsBetween(fBeginDate, fEndDate);
end;

{ TFlowRec }

constructor TFlowRec.Create(AFlowTime: TDateTime; AFlow: Double);
begin
  FlowTime := AFlowTime;
  Flow := AFlow;
end;

{ TEventRec }

function TEventRec.GetNumCalendarDays: Integer;
var
  i: Integer;
begin
  Result := CalendarDays(BeginDate, EndDate);
end;

procedure TfrmCalculateFlowStatistics.edtOpenFileButtonClick(
  Sender: TObject);
var
  i: Integer;
  ANode: TcxTreeListNode;
  NumIncluded: Integer;
begin
  inherited;

	frmMain.dlgOpen.InitialDir := frmMain.InitRegistryDir;
	frmMain.dlgOpen.Title := 'Open Interface File';
	frmMain.dlgOpen.Filter := 'Interface Files (*.int,*.r05,*.r15,*.r20,*.r60,*.t05,*.t15,*.t20,*.t60,*.x05,*.x15,*.x20,*.x60)'+
		'|*.int;*.r05;*.r15;*.r20;*.r60;*.t05;*.t15;*.t20;*.t60;*.x05;*.x15;*.x20;*.x60|All Files|*.*';
	frmMain.dlgOpen.DefaultExt := 'int';

	if frmMain.dlgOpen.Execute then
	begin
		if Assigned(InterfaceFile) then
		begin
			InterfaceFile := nil;
			TreeNodes.Clear;
		end;
		InterfaceFile := TSWMMStandardInterfaceFile.Create(frmMain.dlgOpen.FileName);
		lblStep1.Caption := Format('1. Open File to Convert (Nodes: %d)', [InterfaceFile.NumInlets]);
		edtOpenFile.Text := frmMain.dlgOpen.FileName;
		frmMain.SaveDirToRegistry;

    FreeAndNil(FlowFile);
    FlowFile := TFlowFile.Create;

		// Node list
		TreeNodes.BeginUpdate;
		for i := 0 to InterfaceFile.NumInlets-1 do
		begin
			ANode := TreeNodes.Add;
			ANode.Values[cxColRecord.ItemIndex] := i+1;
			ANode.Values[cxColInclude.ItemIndex] := True;
			ANode.Values[cxColOriginalID.ItemIndex] := InterfaceFile.IDs[i];
			ANode.Values[cxColMinFlow.ItemIndex] := 1.0;
		end;
		NumIncluded := InterfaceFile.NumInlets;
		lblSelectNodes.Caption := Format('2. Select Nodes to Convert (%d Selected)',
			[NumIncluded]);
		TreeNodes.EndUpdate;
		TreeNodes.Invalidate;
	end;
end;

procedure TfrmCalculateFlowStatistics.edtSaveFileButtonClick(
  Sender: TObject);
begin
	inherited;

	frmMain.dlgSave.InitialDir := frmMain.InitRegistryDir;
	frmMain.dlgSave.Title := 'Save Flow Statistics File';
	frmMain.dlgSave.Filter := 'Comma-Separated Value (*.csv)|*.csv|All Files|*.*';
	frmMain.dlgSave.DefaultExt := 'csv';

	if frmMain.dlgSave.Execute then
	begin
		edtSaveFile.Text := frmMain.dlgSave.FileName;
		frmMain.SaveDirToRegistry;
	end;
end;

procedure TfrmCalculateFlowStatistics.edtOpenFileChange(Sender: TObject);
begin
  inherited;
	TreeNodes.Visible := Length(edtOpenFile.Text) > 0;
	pnlOptions.Visible := TreeNodes.Visible;
end;

procedure TfrmCalculateFlowStatistics.btnCloseTaskClick(Sender: TObject);
begin
  inherited;
	Close;
  frmCalculateFlowStatistics := nil;
end;

procedure TfrmCalculateFlowStatistics.RzButton1Click(Sender: TObject);
var
	i: Integer;
begin
  inherited;
	for i := 0 to TreeNodes.Count - 1 do    // Iterate
	begin
    TreeNodes.Items[i].Values[cxColInclude.ItemIndex] := False;
	end;    // for
end;

procedure TfrmCalculateFlowStatistics.RzButton2Click(Sender: TObject);
var
	i: Integer;
begin
  inherited;
	for i := 0 to TreeNodes.Count - 1 do    // Iterate
	begin
		TreeNodes.Items[i].Values[cxColInclude.ItemIndex] := True;
	end;    // for
end;

procedure TfrmCalculateFlowStatistics.RzButton3Click(Sender: TObject);
var
	i: Integer;
begin
  inherited;
	for i := 0 to TreeNodes.Count - 1 do    // Iterate
	begin
		TreeNodes.Items[i].Values[cxColInclude.ItemIndex] :=
      not TreeNodes.Items[i].Values[cxColInclude.ItemIndex];
	end;    // for
end;

end.
