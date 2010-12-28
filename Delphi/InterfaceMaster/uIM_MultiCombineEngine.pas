unit uIM_MultiCombineEngine;

interface

uses SysUtils, Classes, Contnrs,
	uInterfaceStack,
	uIM_Command, uIM_InterfaceFiles;

type

	TIM_SeriesState = (imssBeforeExtraction, imssValid, imssPending,
		imssAfterExtraction);

	//////////////////////////////////////////////////////////////////////////////
	// INTERFACES
	//////////////////////////////////////////////////////////////////////////////

	{=============================================================================
		Name:
		Purpose:
		Requirements:
	=============================================================================}
	IIM_CombineFilter = interface
	end;

	{=============================================================================
		Name:
		Purpose:
		Requirements:
	=============================================================================}
	IIM_Combinable = interface
		procedure Synchronize(AtTime: TDateTime);
	end;

	//////////////////////////////////////////////////////////////////////////////
	// RECORDS
	//////////////////////////////////////////////////////////////////////////////

	TIM_BCC_SourceSpec = record
		SourceFile: TFileName;
		SourceFormat: String;
		StartDate: TDateTime;
		EndDate: TDateTime;
		NewStartDate: TDateTime;
	end;
	TIM_Array_BCC_SourceSpecs = array of TIM_BCC_SourceSpec;

	TIM_BCC_DestSpec = record
		ID: String;
		Expression: String;
	end;
	TIM_Array_BCC_DestSpecs = array of TIM_BCC_DestSpec;


	//////////////////////////////////////////////////////////////////////////////
	// CLASSES
	//////////////////////////////////////////////////////////////////////////////

	{=============================================================================
		Name: TCombineEngine
		Purpose: Manages the combine operation for multiple interface sources
		Requirements: -
	=============================================================================}
	TCombineEngine = class
	private
		fDestFile: IIM_InterfaceFile;
		fSourceFiles: TInterfaceList; // List of IIM_InterfaceFiles
		fSourceSpecs: TIM_Array_BCC_SourceSpecs;
		fDestSpecs: TIM_Array_BCC_DestSpecs;
	public
	//-Object management----------------------------------------------------------
		constructor Create;
		destructor Destroy; override;
	//----------------------------------------------------------------------------
		property DestFile: IIM_InterfaceFile read fDestFile write fDestFile;
		property SourceFiles: TInterfaceList read fSourceFiles;
		property SourceSpecs: TIM_Array_BCC_SourceSpecs read fSourceSpecs write
			fSourceSpecs;
		property DestSpecs: TIM_Array_BCC_DestSpecs read fDestSpecs write
			fDestSpecs;
		procedure Execute;
		procedure CleanUp;
	end;

implementation

uses DateUtils, Types, uIM_ExpressionEngine, PDXDateUtils, Math, Forms,
	fProgress, CodeSiteLogging, TypInfo;

{ TCombineEngine }

procedure TCombineEngine.CleanUp;
begin
	fDestFile := nil;
	fSourceFiles.Clear;
	SetLength(fDestSpecs, 0);
	Finalize(fDestSpecs);
	Setlength(fSourceSpecs, 0);
	Finalize(fSourceSpecs);
end;

constructor TCombineEngine.Create;
begin
	fSourceFiles := TInterfaceList.Create;
end;

destructor TCombineEngine.Destroy;
begin
	fSourceFiles.Free;
	inherited;
end;

procedure TCombineEngine.Execute;
var
	m, n: Integer;
	IO: IIM_InterfaceFileIO;
	DestHeader: IIM_InterfaceHeader;
	DestStartDate: TDateTime;
	ExprEngine: TIM_ExprEngine;
	SourceIDs: TIM_ExprEngineVarNames;
	SourceDataValues: TIM_ExprEngineVars;
	NumSourceFileIDs: Integer;
	LastIndex: Integer;
	EarliestSourceTime: TDateTime;
	LatestSourceTime: TDateTime;
	SourceDataIterators: TInterfaceList;
	SourceDataSeries: TInterfaceList;
	SourceDataSeriesEndTimes: array of TDateTime;
	SourceDataLookup: array of array of Integer;
	PreviousDestTime: TDateTime;
	CurrentDestTime: TDateTime;
	NextDestTime: TDateTime;
	CurrentDestTimeStep: Double;
	CurrentDataSeries: IIM_InterfaceTimeSeriesData;
	ExprVars: TStringList;

function GetSourceIterator(AIndex: Integer): IIM_InterfaceTimeSeriesDataIterator;
begin
	Result := IIM_InterfaceTimeSeriesDataIterator(SourceDataIterators[AIndex]);
end;

function GetEarliestSourceTime: TDateTime;
var
	i: Integer;
begin
	Result := MaxDateTime;
	for i := 0 to Length(fSourceSpecs)-1 do
	begin
		if CompareDateTime(fSourceSpecs[i].NewStartDate, Result) = LessThanValue then
			Result := fSourceSpecs[i].NewStartDate;
	end;
end;

function GetAbsSourceTime(AIndex: Integer; ADateTime: TDateTime): TDateTime;
begin
	Result := fSourceSpecs[AIndex].NewStartDate +
		(ADateTime - fSourceSpecs[AIndex].StartDate);
end;

function GetAbsSourceCurrentTime(AIndex: Integer): TDateTime;
begin
	Result := GetAbsSourceTime(AIndex, GetSourceIterator(AIndex).CurrentTime);
end;

function GetAbsSourceNextTime(AIndex: Integer): TDateTime;
begin
	Result := GetAbsSourceTime(AIndex, GetSourceIterator(AIndex).NextTime);
end;

function GetAbsSourceDataValidUntilTime(AIndex: Integer): TDateTime;
begin
	Result := GetAbsSourceTime(AIndex, GetSourceIterator(AIndex).DataValidUntilTime);
end;

function GetRelSourceTime(AIndex: Integer; ADateTime: TDateTime): TDateTime;
begin
	Result := (ADateTime - fSourceSpecs[AIndex].NewStartDate) +
		fSourceSpecs[AIndex].StartDate;
end;

function GetLatestSourceTime: TDateTime;
var
	i: Integer;
	AbsEndDate: TDateTime;
begin
	Result := MaxDateTime;
	for i := 0 to Length(fSourceSpecs)-1 do
	begin
		AbsEndDate := GetAbsSourceTime(i, fSourceSpecs[i].EndDate);
		if CompareDateTime(AbsEndDate, Result) = LessThanValue then
			Result := AbsEndDate;
	end;
end;

function WithinSourceExtractionRange(AIndex: Integer; ADateTime: TDateTime): Boolean;
var
	AbsStartDate: TDateTime;
	AbsEndDate: TDateTime;
begin
	AbsStartDate := fSourceSpecs[AIndex].NewStartDate;
	AbsEndDate := GetAbsSourceTime(AIndex, fSourceSpecs[AIndex].EndDate);

	// See if AbsStartDate <= ADateTime <= AbsEndDate
	Result := (CompareDateTime(AbsStartDate, ADateTime) <= EqualsValue) and
		(CompareDateTime(ADateTime, AbsEndDate) <= EqualsValue);
end;

function BeforeSourceExtractionRange(AIndex: Integer; ADateTime: TDateTime): Boolean;
begin
	Result := CompareDateTime(ADateTime, fSourceSpecs[AIndex].NewStartDate) = LessThanValue;
end;

function SeriesStateAtTime(AIndex: Integer; AtTime: TDateTime): TIM_SeriesState;
var
	SourceIterator: IIM_InterfaceTimeSeriesDataIterator;
//	SourceTime: TDateTime;
	SourceDataValidUntilTime: TDateTime;
	SourceNextTime: TDateTime;
begin
	SourceIterator := GetSourceIterator(AIndex);
	SourceDataValidUntilTime := GetAbsSourceTime(AIndex, SourceIterator.DataValidUntilTime);
	SourceNextTime := GetAbsSourceTime(AIndex, SourceIterator.NextTime);

	if BeforeSourceExtractionRange(AIndex, AtTime) then
		Result := imssBeforeExtraction
	else if CompareDateTime(AtTime, SourceDataValidUntilTime) <= EqualsValue then
		Result := imssValid
	else if (CompareDateTime(AtTime, SourceDataValidUntilTime) = GreaterThanValue) and
		(CompareDateTime(AtTime, SourceNextTime) = LessThanValue) then
			Result := imssPending
	else
	begin
		Result := imssAfterExtraction;
	end;
end;

function GetNextAvailableTime(FromTime: TDateTime): TDateTime;
var
	i: Integer;
	EarliestSourceTime: TDateTime;
	CompareTime: TDateTime;
	SourceIterator: IIM_InterfaceTimeSeriesDataIterator;
	SeriesState: TIM_SeriesState;
	EnumTypeInfo: PTypeInfo;
begin
	EarliestSourceTime := MaxDateTime;
	for i := 0 to SourceDataIterators.Count-1 do
	begin
		SourceIterator := IIM_InterfaceTimeSeriesDataIterator(SourceDataIterators[i]);
		SeriesState := SeriesStateAtTime(i, FromTime);
		EnumTypeInfo := TypeInfo(TIM_SeriesState);
		case SeriesState of
			imssBeforeExtraction: begin
				EarliestSourceTime := Min(EarliestSourceTime, fSourceSpecs[i].NewStartDate);
			end;
			imssValid: begin
				EarliestSourceTime := Min(EarliestSourceTime, GetAbsSourceDataValidUntilTime(i));
			end;
			imssPending: begin
				EarliestSourceTime := Min(EarliestSourceTime, GetAbsSourceNextTime(i));
			end;
			imssAfterExtraction: begin
				// Do nothing
			end;
		end;
	end;
	Result := EarliestSourceTime;

	// Prevent endless loop when reaching end of all series
	if CompareDateTime(Result, FromTime) = EqualsValue then
	begin
		Result := MaxDateTime;
	end;
end;

procedure InitDataLookup;
var
	i, j: Integer;
	SeriesIndex: Integer;
	SourceHeader: IIM_InterfaceHeader;
begin
	SetLength(SourceDataLookup, fSourceFiles.Count);
	SeriesIndex := 0;
	for i := 0 to Length(SourceDataLookup)-1 do
	begin
		SourceHeader := IIM_InterfaceFile(fSourceFiles[i]).GetHeader;
		SetLength(SourceDataLookup[i], SourceHeader.NumIndexedHeaderValues);
		for j := 0 to Length(SourceDataLookup[i])-1 do
		begin
			SourceDataLookup[i, j] := SeriesIndex;
			Inc(SeriesIndex);
		end;
	end;
end;

procedure InitSeries;
var
	i, j: Integer;
	SeriesIndex: Integer;
	CurrentIterator: IIM_InterfaceTimeSeriesDataIterator;
	EarliestSourceTime: TDateTime;
begin
	for i := 0 to SourceDataIterators.Count-1 do
	begin
		CurrentIterator := GetSourceIterator(i);
		if CurrentIterator.HasNext then
		begin
			SeriesIndex := SourceDataSeries.Add(CurrentIterator.Next);

			// Advance iterator to the first available time slot just before the extraction period
			while CurrentIterator.HasNext do
				if CompareDateTime(CurrentIterator.CurrentTime, fSourceSpecs[i].StartDate) >= EqualsValue then
				begin
					SourceDataSeries[SeriesIndex] := CurrentIterator.Current;
					Break;
				end
				else if CompareDateTime(CurrentIterator.CurrentTime, fSourceSpecs[i].StartDate) = LessThanValue then
				begin
					CurrentIterator.Next;
				end
				else
				begin
					SourceDataSeries[SeriesIndex] := nil;
					Break;
				end;

			CurrentDataSeries := IIM_InterfaceTimeSeriesData(SourceDataSeries[SeriesIndex]);
			if Assigned(CurrentDataSeries) then
			begin
				if WithinSourceExtractionRange(i, GetAbsSourceTime(i, CurrentDataSeries.Time)) then
					for j := 0 to CurrentDataSeries.NumIndexedDataValues-1 do
						SourceDataValues[SourceDataLookup[i,j]] := CurrentDataSeries.IndexedDataValue[j]
				else
					for j := 0 to CurrentDataSeries.NumIndexedDataValues-1 do
						SourceDataValues[SourceDataLookup[i,j]] := 0;
			end
			else
			begin
				for j := 0 to IIM_InterfaceFile(fSourceFiles[i]).GetHeader.NumIndexedHeaderValues-1 do
				begin
					SourceDatavalues[SourceDataLookup[i,j]] := 0;
				end;
			end;
		end
		else // Reached the end of a source files
		begin
			SourceDataSeries.Add(nil);
		end;
	end;

	// Calculate initial timestep
	CurrentDestTimeStep := MaxInt;
	EarliestSourceTime := GetEarliestSourceTime;
	for i := 0 to SourceDataIterators.Count-1 do
	begin
		CurrentIterator := GetSourceIterator(i);
		if WithinSourceExtractionRange(i, EarliestSourceTime) then
			CurrentDestTimeStep := Min(CurrentDestTimeStep, CurrentIterator.CurrentTimeStep);
	end;
	Assert(CurrentDestTimeStep > 0, 'CombineEngine - InitSeries: not CurrentDestTimeStep > 0');
	NextDestTime := GetNextAvailableTime(EarliestSourceTime);
end;

procedure SetupSources;
var
	i, j: Integer;
	SourceHeader: IIM_InterfaceHeader;
begin
	SourceDataIterators := TInterfaceList.Create;
	SourceDataSeries := TInterfaceList.Create;
	SetLength(SourceDataSeriesEndTimes, fSourceFiles.Count);
	for i := 0 to fSourceFiles.Count-1 do
	begin
		IIM_InterfaceFile(fSourceFiles[i]).GetIOServices.MoveToBeginningOfData;
		SourceHeader := IIM_InterfaceFile(fSourceFiles[i]).GetHeader;
		NumSourceFileIDs := SourceHeader.NumIndexedHeaderValues;
		LastIndex := Length(SourceIDs);
		SetLength(SourceIDs, Length(SourceIDs) + NumSourceFileIDs);
		SetLength(SourceDataValues, Length(SourceDataValues) + NumSourceFileIDs);
		for j := 0 to NumSourceFileIDs-1 do
		begin
			SourceIDs[j+LastIndex] := Format('%s@%d',
				[Trim(SourceHeader.IndexedHeaderValue[j]), i+1]);
		end;
		SourceDataIterators.Add(IIM_InterfaceFile(fSourceFiles[i]).GetTimeSeriesDataIterator);
	end;
end;

procedure UpdateTimeSeries(ATime: TDateTime);
var
	i, j: Integer;
	SourceTime: TDateTime;
	SourceDataValidUntilTime: TDateTime;
	SourceNextTime: TDateTime;
	SourceFile: IIM_InterfaceFile;
	SourceIterator: IIM_InterfaceTimeSeriesDataIterator;
	SourceSeries: IIM_InterfaceTimeSeriesData;
begin
	for i := 0 to fSourceFiles.Count-1 do
	begin
		SourceFile := IIM_InterfaceFile(fSourceFiles[i]);
		SourceIterator := GetSourceIterator(i);
		SourceTime := GetAbsSourceCurrentTime(i);
		SourceDataValidUntilTime := GetAbsSourceDataValidUntilTime(i);
		SourceNextTime := GetAbsSourceNextTime(i);

		if CompareDateTime(ATime, SourceNextTime) >= EqualsValue then
		begin
			SourceDataSeries[i] := SourceIterator.Next;
			if Assigned(SourceDataSeries[i]) then
			begin
				SourceSeries := IIM_InterfaceTimeSeriesData(SourceDataSeries[i]);
				for j := 0 to SourceSeries.NumIndexedDataValues-1 do
					SourceDataValues[SourceDataLookup[i,j]] := SourceSeries.IndexedDataValue[j];
			end
		end
		else if CompareDateTime(SourceNextTime, MaxDateTime) = EqualsValue then
		begin
			SourceDataSeries[i] := nil;
		end;
	end;
end;

begin
	// Prepare header
	DestHeader := fDestFile.GetHeader;
	DestHeader.HeaderValue[IM_IFHDR_TITLE1] := '';
	DestHeader.HeaderValue[IM_IFHDR_TITLE2] := '';
	DestHeader.HeaderValue[IM_IFHDR_TITLE3] := '';
	DestHeader.HeaderValue[IM_IFHDR_TITLE4] := '';
	DestHeader.HeaderValue[IM_IFHDR_SOURCE] := 'IM_MultiCombine';
	DestHeader.HeaderValue[IM_IFHDR_AREA] := 0.001;
	DestHeader.HeaderValue[IM_IFHDR_MULTIPLIER] := 1.00;
	EarliestSourceTime := GetEarliestSourceTime;
	DestHeader.HeaderValue[IM_IFHDR_STARTDATE] := EarliestSourceTime;
	for m := 0 to Length(fDestSpecs)-1 do
	begin
		DestHeader.AddIndexedHeaderValue(fDestSpecs[m].ID);
	end;
	DestHeader.WriteHeader;

	// Set up source files
	SetupSources;
	ExprEngine := GetExpressionEngine;
	ExprEngine.SetupVars(SourceIDs, SourceDataValues);
	ExprVars := TStringList.Create;
	ExprEngine.Engine.GetIdentList(ExprVars);
	ExprVars.Free;

	InitDataLookup;

	InitSeries;

	// Write interface file
	IO := fDestFile.GetIOServices;
	LatestSourceTime := GetLatestSourceTime;
	CurrentDestTime := EarliestSourceTime;
	frmProgress.prgProgress.Percent := 0;
	frmProgress.Show;
	frmProgress.lblProgress.Caption := SysUtils.Format('Combining %d files to create %s',
		[Length(fSourceSpecs), ExtractFileName(fDestFile.FileName)]);
	CodeSite.SendDateTime('LatestSourceTime', LatestSourceTime);
	while CompareDateTime(CurrentDestTime, LatestSourceTime) <= EqualsValue do
	begin
		// Write time info
		IO.WriteInteger(Y2KJulDateOfDateTime(CurrentDestTime));
		IO.WriteDouble(SecondsOfDayOfDateTime(CurrentDestTime));
		IO.WriteDouble(CurrentDestTimeStep);

		// Analyze and write destination nodes
		for m := 0 to Length(fDestSpecs)-1 do
		begin
			ExprEngine.Expression := fDestSpecs[m].Expression;
			IO.WriteDouble(ExprEngine.Evaluate);
		end;

		PreviousDestTime := CurrentDestTime;
		CurrentDestTime := NextDestTime;
		UpdateTimeSeries(CurrentDestTime);
		NextDestTime := GetNextAvailableTime(CurrentDestTime);
		CurrentDestTimeStep := SecondSpan(NextDestTime, CurrentDestTime);

		frmProgress.prgProgress.Percent := Round((CurrentDestTime - EarliestSourceTime)/
			(LatestSourceTime - EarliestSourceTime)*100);
		if CompareDateTime(CurrentDestTime, EncodeDateTime(2005,10,1,0,0,0,0)) >= EqualsValue then
			CodeSite.SendDateTime('CurrentDestTime', CurrentDestTime);

		Application.ProcessMessages;
	end;
	frmProgress.Hide;

	// Clean up
	SourceDataIterators.Free;
	SourceDataSeries.Free;
end;

end.
