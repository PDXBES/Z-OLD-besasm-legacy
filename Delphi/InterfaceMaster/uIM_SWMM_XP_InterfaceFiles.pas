unit uIM_SWMM_XP_InterfaceFiles;

interface

uses SysUtils, Classes, Math, DateUtils, Variants,
	PDXDateUtils, StStrms, StStrL,
	uIM_InterfaceFiles, uIM_ExpressionEngine, uIM_ConvertEngine, uIM_IndexLookup;

const
	IM_SWMMXP_TITLE_LENGTH = 80;
	IM_SWMMXP_SOURCEBLOCK_LENGTH = 20;
	IM_SWMMXP_ALPHAID_LENGTH = 16;
	IM_SWMMXP_FORMAT_DESC = 'SWMM-XP';
type

	T_SWMM_XP_InterfaceFile_NodeFilter = class(TInterfacedObject,
		IIM_ConvertFilter)
	private
		fID: String;
		fNewID: String;
		fNewSeries: String;
		fInclude: Boolean;
		fExpression: String;
	protected
	//-IIM_ConvertFilter implementation-------------------------------------------
		function GetID: String;
		procedure SetID(Value: String);
		function GetNewID: String;
		procedure SetNewID(Value: String);
		function GetFinalID: String;
		function GetNewSeries: String;
		procedure SetNewSeries(Value: String);
		function GetInclude: Boolean;
		procedure SetInclude(Value: Boolean);
		function GetExpression: String;
		procedure SetExpression(Value: String);
	public
		constructor Create; overload;
		constructor Create(AID: String; ANewID: String = ''; ANewSeries: String = '';
			AInclude: Boolean = True; AExpression: String = ''); overload;
		destructor Destroy; override;
	//-IIM_ConvertFilter implementation-------------------------------------------
		property ID: String read GetID write SetID;
		property NewID: String read GetNewID write SetNewID;
		property FinalID: String read GetFinalID;
		property NewSeries: String read GetNewSeries write SetNewSeries;
		property Include: Boolean read GetInclude write SetInclude;
		property Expression: String read GetExpression write SetExpression;
	end;

	T_SWMM_XP_StandardInterfaceFileHeader = class;
	T_SWMM_XP_InterfaceFileIO = class;
	{=============================================================================
		 Name: T_SWMM_XP_StandardInterfaceFile
		 Purpose: Provides services for reading, writing, and manipulating standard
			 swmm interface files in XP 8.x+ format
		 Requirements: -
	=============================================================================}
	T_SWMM_XP_StandardInterfaceFile = class(TInterfacedObject,
		IIM_InterfaceFile,
		IIM_Convert)
	private
		fFileName: TFileName;
		fStream: TFileStream;
		fBuffer: TStBufferedStream;
		fHeader: IIM_InterfaceHeader;
		IO: IIM_InterfaceFileIO;
		fDataIterator: IIM_InterfaceTimeSeriesDataIterator;
	protected
	//-IIM_InterfaceHeader implementation-----------------------------------------
		function GetHeaderValue(AIndex: String): Variant;
		procedure SetHeaderValue(AIndex: String; Value: Variant);
		function GetFileName: TFileName;
		function GetFormat: String;
	public
	//-Object management----------------------------------------------------------
		constructor Create(AFile: TFileName; Mode: Word = fmOpenRead); virtual;
		destructor Destroy; override;
	//-IIM_InterfaceFile implementation-------------------------------------------
		function GetTimeSeriesDataIterator: IIM_InterfaceTimeSeriesDataIterator;
		function GetIOServices: IIM_InterfaceFileIO;
		function GetHeader: IIM_InterfaceHeader;
		procedure ReadInterface;
		procedure WriteInterface;
		property FileName: TFileName read fFileName;
		property Format: String read GetFormat;
	//-IIM_Convertible implementation-------------------------------------------
		procedure ConvertFrom(ASourceInterface: IIM_InterfaceFile;
			AFilterPackage: IIM_ConvertFilterPackage = nil);
{		procedure ConvertTo(ADestInterface: IIM_InterfaceFile;
			AFilterPackage: IIM_ConvertFilterPackage = nil);}
	end;

	{=============================================================================
		Name: T_SWMM_XP_ConvertFilterPackage
		Purpose: Provides filter information for a source XP interface file
		Requirements:
	=============================================================================}
	T_SWMM_XP_ConvertFilterPackage = class(TInterfacedObject,
		IIM_ConvertFilterPackage)
	private
		fFilters: TInterfaceList;
	protected
		function GetFilter(AIndex: Integer): IIM_ConvertFilter; overload;
		procedure SetFilter(AIndex: Integer; Value: IIM_ConvertFilter);
		function GetFilterByID(ID: String): IIM_ConvertFilter; overload;
		procedure SetFilterByID(ID: String; Value: IIM_ConvertFilter);
	public
		constructor Create; virtual;
		destructor Destroy; override;
		procedure SetupFilters(SourceInterface: IIM_InterfaceFile);
		function NumIncludes: Integer;
		function IsIncluded(AIndex: Integer): Boolean;
		function IncludeLookup: TIndexLookupList;
		procedure AddFilter(AFilter: IIM_ConvertFilter);
		procedure DeleteFilter(AFilter: IIM_ConvertFilter);
	//----------------------------------------------------------------------------
		property Filter[AIndex: Integer]: IIM_ConvertFilter read GetFilter
			write SetFilter;
		property FilterByID[ID: String]: IIM_ConvertFilter read GetFilterByID
			write SetFilterByID;
	end;

	T_SWMM_XP_StandardInterfaceFile_TimeSeriesData = class;
	{=============================================================================
		 Name: T_SWMM_XP_StandardInterfaceFile_DataIterator
		 Purpose: Provides a way to traverse back and forth an XP standard interface
			 file
		 Requirements: a T_SWMM_XP_StandardInterfaceFile
		 Restrictions: usage restricted to T_SWMM_XP_StandardInterfaceFile
	=============================================================================}
	T_SWMM_XP_StandardInterfaceFile_DataIterator = class(TInterfacedObject,
		IIM_InterfaceTimeSeriesDataIterator)
	private
		fInterfaceFile: Pointer;
		fCurrentTime: TDateTime;
		fCurrentTimeStep: Double;
		fNextTime: TDateTime;
		fPreviousTime: TDateTime;
		fData: Pointer;
	private
	//-Object management----------------------------------------------------------
		constructor Create(AInterfaceFile: IIM_InterfaceFile); virtual;
		destructor Destroy; override;
	public
			//-IIM_InterfaceDataIterator implementation-----------------------------------
		function HasNext: Boolean;
		function Next: IIM_InterfaceTimeSeriesData;
		function HasPrevious: Boolean;
		function Previous: IIM_InterfaceTimeSeriesData;
		function Current: IIM_InterfaceTimeSeriesData;
		function CurrentTime: TDateTime;
		function NextTime: TDateTime;
		function PreviousTime: TDateTime;
		function DataValidUntilTime: TDateTime;
		function CurrentTimeStep: Double;
		function Position: Int64;
		function PositionPercent: Integer;
	end;

	{=============================================================================
		 Name: T_SWMM_XP_StandardInterfaceFile_TimeSeriesData
		 Purpose: Provides access to standard interface file data
		 Requirements: a T_SWMM_XP_StandardInterfaceFile_TimeSeriesDataIterator
	=============================================================================}
	T_SWMM_XP_StandardInterfaceFile_TimeSeriesData = class(TInterfacedObject,
		IIM_InterfaceTimeSeriesData)
	private
		fIterator: Pointer;
		fInterfaceFile: Pointer;
		fTime: TDateTime;
		fIndexedData: array of Double;
		fTimeStep: Double;
	public
	//-Object management----------------------------------------------------------
		constructor Create(AInterfaceFile: IIM_InterfaceFile;
			AIterator: IIM_InterfaceTimeSeriesDataIterator);
		destructor Destroy; override;
	//-IIM_InterfaceTimeSeriesData implementation---------------------------------
		procedure ReadData;
		procedure WriteData;
		function Size: Int64;
		function GetDataValue(AIndex: String): Variant;
		procedure SetDataValue(AIndex: String; Value: Variant);
		property DataValue[AIndex: String]: Variant read GetDataValue write SetDataValue;
		function NumIndexedDataValues: Integer;
		function GetIndexedDataValue(AIndex: Integer): Variant;
		procedure SetIndexedDataValue(AIndex: Integer; Value: Variant);
		property IndexedDataValue[AIndex: Integer]: Variant read GetIndexedDataValue
			write SetIndexedDataValue;
		function Time: TDateTime;
	end;

	{=============================================================================
		 Name: T_SWMM_XP_StandardInterfaceFileHeader
		 Purpose:
		 Requirements:
		 Restrictions: usage restricted to T_SWMM_XP_StandardInterfaceFile
	=============================================================================}
	T_SWMM_XP_StandardInterfaceFileHeader = class(TInterfacedObject,
		IIM_InterfaceHeader)
	private
		fTitles: array[1..4] of String;
		fSourceBlock: String;
		fStartDate: TDateTime;
		fNumFlows: Integer;
		fFlowIDs: array of String;
		fNumPollutants: Integer;
		fArea: Double;
		fUsesAlphaNumericIDs: Boolean;
		fFlowMultiplier: Double;
		fInterfaceFile: Pointer;
	protected
		function GetTitles(AIndex: Integer): String;
		procedure SetTitles(AIndex: Integer; Value: String);
		function GetSourceBlock: String;
		procedure SetSourceBlock(Value: String);
		function GetStartDate: TDateTime;
		procedure SetStartDate(Value: TDateTime);
		function GetFlowID(AIndex: Integer): String;
		procedure SetFlowID(AIndex: Integer; Value: String);
		function GetArea: Double;
		procedure SetArea(Value: Double);
		function GetUsesAlphaNumericIDs: Boolean;
		procedure SetUsesAlphaNumericIDs(Value: Boolean);
		function GetFlowMultiplier: Double;
		procedure SetFlowMultiplier(Value: Double);
	private
	//-Object management----------------------------------------------------------
		constructor Create(AInterfaceFile: IIM_InterfaceFile);
		destructor Destroy; override;
	public
	//-Object properties----------------------------------------------------------
		property Titles[AIndex: Integer]: String read GetTitles write SetTitles;
		property SourceBlock: String read GetSourceBlock write SetSourceBlock;
		property StartDate: TDateTime read GetStartDate write SetStartDate;
		property FlowIDs[AIndex: Integer]: String read GetFlowID write SetFlowID;
		property Area: Double read GetArea write SetArea;
		property UsesAlphaNumericIDs: Boolean read GetUsesAlphaNumericIDs
			write SetUsesAlphaNumericIDs;
		property FlowMultiplier: Double read GetFlowMultiplier write SetFlowMultiplier;
	//-IIM_InterfaceHeader implementation-----------------------------------------
		procedure ReadHeader;
		procedure WriteHeader;
		function Size: Int64;
		function GetHeaderValue(AIndex: String): Variant;
		procedure SetHeaderValue(AIndex: String; Value: Variant);
		property HeaderValue[AIndex: String]: Variant read GetHeaderValue write SetHeaderValue;
		function GetIndexedHeaderValue(AIndex: Integer): Variant;
		procedure SetIndexedHeaderValue(AIndex: Integer; Value: Variant);
		function NumIndexedHeaderValues: Integer;
		function AddIndexedHeaderValue(Value: Variant): Integer;
		procedure ClearIndexedHeaderValues;
 		property IndexedHeaderValue[AIndex: Integer]: Variant read GetIndexedHeaderValue
			write SetIndexedHeaderValue;
	end;

	{=============================================================================
		 Name: T_SWMM_XP_InterfaceFileIO
		 Purpose:	Provides IO services for XP interface file
		 Requirements: usage restricted to T_SWMM_XP_StandardInterfaceFile
	=============================================================================}
	T_SWMM_XP_InterfaceFileIO = class(TInterfacedObject,
		IIM_InterfaceFileIO)
	private
		fStream: TStBufferedStream;
		fInterfaceFile: Pointer;
	public
	//-Object management----------------------------------------------------------
		constructor Create(AInterfaceFile: IIM_InterfaceFile; AStream: TStBufferedStream);
		destructor Destroy; override;
	//-IIM_InterfaceFileIO implementation-----------------------------------------
		function ReadString(ALength: Integer): String; overload;
		function ReadString: String; overload;
		function ReadInteger: Integer;
		function ReadExtended: Extended;
		function ReadDouble: Double;
		function ReadSingle: Single;
		function ReadByte: Byte;
		procedure WriteString(Value: String; ALength: Integer); overload;
		procedure WriteString(Value: String); overload;
		procedure WriteInteger(Value: Integer);
		procedure WriteExtended(Value: Extended);
		procedure WriteDouble(Value: Double);
		procedure WriteSingle(Value: Single);
		procedure WriteByte(Value: Byte);
		function MoveTo(Offset: Int64; Origin: TSeekOrigin): Int64;
		procedure MoveToBeginning;
		procedure MoveToEnd;
		procedure MoveToBeginningOfData;
		function IsBeginningOfData: Boolean;
		function IsEOF: Boolean;
		function Position: Int64;
		function Size: Int64;
		procedure NextLine;
		procedure PreviousLine;
	end;

implementation

uses CodeSiteLogging, fProgress, Forms, Types;

{ T_SWMM_XP_StandardInterfaceFile }

procedure T_SWMM_XP_StandardInterfaceFile.WriteInterface;
begin
	fHeader.WriteHeader;
end;

constructor T_SWMM_XP_StandardInterfaceFile.Create(AFile: TFileName;
	Mode: Word);
var
	i: Integer;
	CurrentFilter: IIM_ConvertFilter;
begin
	fFileName := AFile;
	fStream := TFileStream.Create(fFileName, Mode);
	fBuffer := TStBufferedStream.Create(fStream);
	fHeader := T_SWMM_XP_StandardInterfaceFileHeader.Create(Self);
	IO := T_SWMM_XP_InterfaceFileIO.Create(Self, fBuffer);
	if Mode <> fmCreate then
		fHeader.ReadHeader;
end;

procedure T_SWMM_XP_StandardInterfaceFile.SetHeaderValue(AIndex: String;
	Value: Variant);
begin
	Assert(false, 'T_SWMM_XP_StandardInterfaceFile.SetHeaderValue not implemented');
end;

function T_SWMM_XP_StandardInterfaceFile.GetIOServices: IIM_InterfaceFileIO;
begin
	Result := IO;
end;

function T_SWMM_XP_StandardInterfaceFile.GetTimeSeriesDataIterator: IIM_InterfaceTimeSeriesDataIterator;
begin
	Result := T_SWMM_XP_StandardInterfaceFile_DataIterator.Create(Self);
end;

function T_SWMM_XP_StandardInterfaceFile.GetHeaderValue(
	AIndex: String): Variant;
begin
	Assert(false, 'T_SWMM_XP_StandardInterfaceFile.GetHeaderValue not implemented');
end;

procedure T_SWMM_XP_StandardInterfaceFile.ReadInterface;
begin
	fHeader.ReadHeader;
end;

destructor T_SWMM_XP_StandardInterfaceFile.Destroy;
begin
	fBuffer.Free;
	fStream.Free;
	inherited;
end;

function T_SWMM_XP_StandardInterfaceFile.GetHeader: IIM_InterfaceHeader;
begin
	Result := fHeader;
end;

{procedure T_SWMM_XP_StandardInterfaceFile.ConvertTo(
	ADestInterface: IIM_InterfaceFile;
	AFilterPackage: IIM_ConvertFilterPackage = nil);
var
	DestHeader: IIM_InterfaceHeader;
	SourceData: IIM_InterfaceTimeSeriesData;
	SourceDataIterator: IIM_InterfaceTimeSeriesDataIterator;
	FilteredData: TGenericTimeSeriesData;
	i: Integer;
	FilteredIndices: TIndexLookupList;
	SourceIDs: TIM_ExprEngineVarNames;
	SourceDataValues: TIM_ExprEngineVars;
	ExprEngine: TIM_ExprEngine;
begin
	DestHeader := ADestInterface.GetHeader;

	// Assign Header Values
	DestHeader.HeaderValue[IM_IFHDR_TITLE1] := fHeader.HeaderValue[IM_IFHDR_TITLE1];
	DestHeader.HeaderValue[IM_IFHDR_TITLE2] := fHeader.HeaderValue[IM_IFHDR_TITLE2];
	DestHeader.HeaderValue[IM_IFHDR_TITLE3] := fHeader.HeaderValue[IM_IFHDR_TITLE3];
	DestHeader.HeaderValue[IM_IFHDR_TITLE4] := fHeader.HeaderValue[IM_IFHDR_TITLE4];
	DestHeader.HeaderValue[IM_IFHDR_SOURCE] := fHeader.HeaderValue[IM_IFHDR_SOURCE];
	DestHeader.HeaderValue[IM_IFHDR_STARTDATE] := fHeader.HeaderValue[IM_IFHDR_STARTDATE];
	DestHeader.HeaderValue[IM_IFHDR_AREA] := fHeader.HeaderValue[IM_IFHDR_AREA];
	DestHeader.HeaderValue[IM_IFHDR_MULTIPLIER] := fHeader.HeaderValue[IM_IFHDR_MULTIPLIER];

	// Assign Header IDs
	if Assigned(AFilterPackage) then
	begin
		FilteredIndices := AFilterPackage.IncludeLookup;
		for i := 0 to AFilterPackage.NumIncludes-1 do
			DestHeader.AddIndexedHeaderValue(AFilterPackage.Filter[FilteredIndices[i]].FinalID);
	end
	else
	begin
		for i := 0 to fHeader.NumIndexedHeaderValues-1 do
			DestHeader.AddIndexedHeaderValue(fHeader.IndexedHeaderValue[i]);
	end;

	DestHeader.WriteHeader;

	// Write out Data Series
	if Assigned(AFilterPackage) then
	begin
		SetLength(SourceIDs, fHeader.NumIndexedHeaderValues);
		for i := 0 to fHeader.NumIndexedHeaderValues-1 do
			SourceIDs[i] := fHeader.IndexedHeaderValue[i];

		ExprEngine := GetExpressionEngine;
		ExprEngine.SetupVars(SourceIDs, SourceDataValues);

		SourceDataIterator := GetTimeSeriesDataIterator;
		ADestInterface.SetTimeSeriesDataIterator(SourceDataIterator);
		while SourceDataIterator.HasNext do
		begin
			SourceData := SourceDataIterator.Next;

			FilteredData := TGenericTimeSeriesData.Create;
			for i := 0 to AFilterPackage.NumIncludes-1 do
			begin
				ExprEngine.Expression := AFilterPackage.Filter[FilteredIndices[i]].Expression;
				FilteredData.AddIndexedDataValue(ExprEngine.Evaluate);
			end;

			ADestInterface.WriteData(FilteredData);

			FilteredData.Free;
			SourceData := nil;
		end;
																										 S
		FilteredIndices.Free;
	end
	else
	begin
		SourceDataIterator := GetTimeSeriesDataIterator;
		while SourceDataIterator.HasNext do
		begin
			SourceData := SourceDataIterator.Next;
			ADestInterface.WriteData(SourceData);
			SourceData := nil;
		end;
	end;

end;}

procedure T_SWMM_XP_StandardInterfaceFile.ConvertFrom(
	ASourceInterface: IIM_InterfaceFile;
	AFilterPackage: IIM_ConvertFilterPackage);
var
	SourceHeader: IIM_InterfaceHeader;
	FilteredIndices: TIndexLookupList;
	i: Integer;
	SourceIDs: TIM_ExprEngineVarNames;
	SourceDataValues: TIM_ExprEngineVars;
	ExprEngine: TIM_ExprEngine;
	SourceData: IIM_InterfaceTimeSeriesData;
	SourceDataIterator: IIM_InterfaceTimeSeriesDataIterator;
	VarList: TStringList;
	SourceTime: TDateTime;
begin
	SourceHeader := ASourceInterface.GetHeader;

	// Assign Header Values
	fHeader.HeaderValue[IM_IFHDR_TITLE1] := SourceHeader.HeaderValue[IM_IFHDR_TITLE1];
	fHeader.HeaderValue[IM_IFHDR_TITLE2] := SourceHeader.HeaderValue[IM_IFHDR_TITLE2];
	fHeader.HeaderValue[IM_IFHDR_TITLE3] := SourceHeader.HeaderValue[IM_IFHDR_TITLE3];
	fHeader.HeaderValue[IM_IFHDR_TITLE4] := SourceHeader.HeaderValue[IM_IFHDR_TITLE4];
	fHeader.HeaderValue[IM_IFHDR_SOURCE] := SourceHeader.HeaderValue[IM_IFHDR_SOURCE];
	fHeader.HeaderValue[IM_IFHDR_STARTDATE] := SourceHeader.HeaderValue[IM_IFHDR_STARTDATE];
	fHeader.HeaderValue[IM_IFHDR_AREA] := SourceHeader.HeaderValue[IM_IFHDR_AREA];
	fHeader.HeaderValue[IM_IFHDR_MULTIPLIER] := SourceHeader.HeaderValue[IM_IFHDR_MULTIPLIER];

	// Assign Header IDs
	if Assigned(AFilterPackage) then
	begin
		FilteredIndices := AFilterPackage.IncludeLookup;
		for i := 0 to AFilterPackage.NumIncludes-1 do
			fHeader.AddIndexedHeaderValue(AFilterPackage.Filter[FilteredIndices[i]].FinalID);
	end
	else
	begin
		for i := 0 to fHeader.NumIndexedHeaderValues-1 do
			fHeader.AddIndexedHeaderValue(fHeader.IndexedHeaderValue[i]);
	end;

	fHeader.WriteHeader;

	frmProgress.prgProgress.Percent := 0;
	frmProgress.Show;
	frmProgress.lblProgress.Caption := SysUtils.Format('Converting %s to %s',
		[ExtractFileName(ASourceInterface.FileName), ExtractFileName(FileName)]);
	// Write out Data Series
	if Assigned(AFilterPackage) then
	begin
		SetLength(SourceIDs, SourceHeader.NumIndexedHeaderValues);
		SetLength(SourceDataValues, SourceHeader.NumIndexedHeaderValues);
		for i := 0 to SourceHeader.NumIndexedHeaderValues-1 do
			SourceIDs[i] := SourceHeader.IndexedHeaderValue[i];

		ExprEngine := GetExpressionEngine;
		ExprEngine.SetupVars(SourceIDs, SourceDataValues);

		SourceDataIterator := ASourceInterface.GetTimeSeriesDataIterator;
		while SourceDataIterator.HasNext do
		begin
//			CodeSite.EnterMethod('Data write loop');
			SourceData := SourceDataIterator.Next;
//			CodeSite.SendNote('SourceDataIterator.Next');
			SourceTime := SourceDataIterator.CurrentTime;
//			CodeSite.SendNote('SourceDataIterator.CurrentTime');
			for i := 0 to SourceData.NumIndexedDataValues-1 do
				SourceDataValues[i] := SourceData.IndexedDataValue[i];
//			CodeSite.SendNote('Filled SourceDataValues');

			IO.WriteInteger(Y2KJulDateOfDateTime(SourceData.Time));
			IO.WriteDouble(SecondsOfDayOfDateTime(SourceData.Time));
			IO.WriteDouble(SourceData.DataValue[IM_IF_TIMESTEP]);
//			CodeSite.SendNote('Wrote time data');

			for i := 0 to AFilterPackage.NumIncludes-1 do
			begin
				ExprEngine.Expression := AFilterPackage.Filter[FilteredIndices[i]].Expression;
				IO.WriteDouble(ExprEngine.Evaluate);
			end;
//			CodeSite.SendNote('Wrote flow data');

			frmProgress.prgProgress.Percent := SourceDataIterator.PositionPercent;
			Application.ProcessMessages;
//			CodeSite.SendNote('Updated progress bar');

//			CodeSite.ExitMethod('Data write loop');
//			CodeSite.AddCheckPoint;
		end;
//		CodeSite.AddSeparator;

		FilteredIndices.Free;
	end
	else
	begin
		SourceDataIterator := ASourceInterface.GetTimeSeriesDataIterator;
		while SourceDataIterator.HasNext do
		begin
			SourceData := SourceDataIterator.Next;

			IO.WriteInteger(Y2KJulDateOfDateTime(SourceData.Time));
			IO.WriteDouble(SecondsOfDayOfDateTime(SourceData.Time));
			IO.WriteDouble(SourceData.DataValue[IM_IF_TIMESTEP]);
			for i := 0 to SourceData.NumIndexedDataValues-1 do
			begin
				IO.WriteDouble(SourceData.IndexedDataValue[i]);
			end;
			frmProgress.prgProgress.Percent := SourceDataIterator.PositionPercent;
			Application.ProcessMessages;
		end;
	end;
	frmProgress.Hide;

end;

function T_SWMM_XP_StandardInterfaceFile.GetFileName: TFileName;
begin
	Result := fFileName;
end;

function T_SWMM_XP_StandardInterfaceFile.GetFormat: String;
begin
	Result := IM_SWMMXP_FORMAT_DESC;
end;

{ T_SWMM_XP_StandardInterfaceFileHeader }

procedure T_SWMM_XP_StandardInterfaceFileHeader.WriteHeader;
var
	IO: IIM_InterfaceFileIO;
	i: Integer;
begin
	IO := IIM_InterfaceFile(fInterfaceFile).GetIOServices;
	IO.WriteString(fTitles[1], IM_SWMMXP_TITLE_LENGTH);
	IO.WriteString(fTitles[2], IM_SWMMXP_TITLE_LENGTH);
	IO.WriteInteger(Y2KJulDateOfDateTime(fStartDate));
	IO.WriteDouble(SecondsOfDayOfDateTime(fStartDate));
	IO.WriteString(fTitles[3], IM_SWMMXP_TITLE_LENGTH);
	IO.WriteString(fTitles[4], IM_SWMMXP_TITLE_LENGTH);
	IO.WriteString(fSourceBlock, IM_SWMMXP_SOURCEBLOCK_LENGTH);
	IO.WriteInteger(Length(fFlowIDs));
	IO.WriteInteger(fNumPollutants);
	if IsZero(fArea) then
		IO.WriteDouble(-1.0)
	else
		IO.WriteDouble(-Abs(fArea));
	for i := 0 to Length(fFlowIDs)-1 do
		IO.WriteString(fFlowIDs[i], IM_SWMMXP_ALPHAID_LENGTH);
	IO.WriteDouble(fFlowMultiplier);
end;

procedure T_SWMM_XP_StandardInterfaceFileHeader.SetFlowMultiplier(
	Value: Double);
begin
	if not SameValue(fFlowMultiplier, Value, IM_EPSILON) then
		fFlowMultiplier := Value;
end;

constructor T_SWMM_XP_StandardInterfaceFileHeader.Create(
	AInterfaceFile: IIM_InterfaceFile);
begin
	fInterfaceFile := Pointer(AInterfaceFile);
end;

procedure T_SWMM_XP_StandardInterfaceFileHeader.SetFlowID(AIndex: Integer;
	Value: String);
begin
	if CompareStr(fFlowIDs[AIndex], Value) <> 0 then
		fFlowIDs[AIndex] := Value;
end;

procedure T_SWMM_XP_StandardInterfaceFileHeader.SetSourceBlock(Value: String);
begin
	if CompareStr(fSourceBlock, Value) <> 0 then
		fSourceBlock := Value;
end;

function T_SWMM_XP_StandardInterfaceFileHeader.GetUsesAlphaNumericIDs: Boolean;
begin
	Result := True; // XP always uses alphanumeric IDs
end;

procedure T_SWMM_XP_StandardInterfaceFileHeader.SetTitles(AIndex: Integer;
	Value: String);
begin
	if CompareStr(fTitles[AIndex], Value) <> 0 then
		fTitles[AIndex] := Value;
end;

function T_SWMM_XP_StandardInterfaceFileHeader.GetStartDate: TDateTime;
begin
	Result := fStartDate;
end;

procedure T_SWMM_XP_StandardInterfaceFileHeader.SetUsesAlphaNumericIDs(
	Value: Boolean);
begin
	// Do nothing; XP always uses alphanumeric IDs
end;

function T_SWMM_XP_StandardInterfaceFileHeader.GetArea: Double;
begin
	Result := fArea;
end;

function T_SWMM_XP_StandardInterfaceFileHeader.GetFlowMultiplier: Double;
begin
	Result := fFlowMultiplier;
end;

function T_SWMM_XP_StandardInterfaceFileHeader.GetFlowID(
	AIndex: Integer): String;
begin
	Result := fFlowIDs[AIndex];
end;

function T_SWMM_XP_StandardInterfaceFileHeader.GetSourceBlock: String;
begin
	Result := fSourceBlock;
end;

procedure T_SWMM_XP_StandardInterfaceFileHeader.SetStartDate(Value: TDateTime);
begin
	if not SameDateTime(fStartDate, Value) then
		fStartDate := Value;
end;

procedure T_SWMM_XP_StandardInterfaceFileHeader.ReadHeader;
var
	StartJulDate: Integer;
	StartTime: Double;
	i: Integer;
	IO: IIM_InterfaceFileIO;
begin
	IO := IIM_InterfaceFile(fInterfaceFile).GetIOServices;
	IO.MoveToBeginning;
	fTitles[1] := IO.ReadString(IM_SWMMXP_TITLE_LENGTH);
	fTitles[2] := IO.ReadString(IM_SWMMXP_TITLE_LENGTH);
	StartJulDate := IO.ReadInteger;
	StartTime := IO.ReadDouble;
	fStartDate := DateTimeOfJulDate(StartJulDate, StartTime);
	fTitles[3] := IO.ReadString(IM_SWMMXP_TITLE_LENGTH);
	fTitles[4] := IO.ReadString(IM_SWMMXP_TITLE_LENGTH);
	fSourceBlock := IO.ReadString(IM_SWMMXP_SOURCEBLOCK_LENGTH);
	fNumFlows := IO.ReadInteger;
	SetLength(fFlowIDs, fNumFlows);
	fNumPollutants := IO.ReadInteger;
	fArea := IO.ReadDouble; //WARNING! XP designates this as negative
	fUsesAlphaNumericIDs := True; // XP *always* uses alphanum IDs
	for i := 0 to fNumFlows-1 do
		fFlowIDs[i] := IO.ReadString(IM_SWMMXP_ALPHAID_LENGTH);
	fFlowMultiplier := IO.ReadDouble;
end;

procedure T_SWMM_XP_StandardInterfaceFileHeader.SetArea(Value: Double);
begin
	if not SameValue(Value, fArea, IM_EPSILON) then
		fArea := Value;
end;

destructor T_SWMM_XP_StandardInterfaceFileHeader.Destroy;
begin
	fInterfaceFile := nil;
	inherited;
end;

function T_SWMM_XP_StandardInterfaceFileHeader.GetTitles(
	AIndex: Integer): String;
begin
	Result := fTitles[AIndex];
end;

function T_SWMM_XP_StandardInterfaceFileHeader.Size: Int64;
begin
	Result :=
		IM_SWMMXP_TITLE_LENGTH +
		IM_SWMMXP_TITLE_LENGTH +
		SizeOf(Integer) + SizeOf (Double) +
		IM_SWMMXP_TITLE_LENGTH +
		IM_SWMMXP_TITLE_LENGTH +
		IM_SWMMXP_SOURCEBLOCK_LENGTH +
		SizeOf(Integer) + SizeOf(Integer) + SizeOf(Double) +
		fNumFlows * IM_SWMMXP_ALPHAID_LENGTH +
		SizeOf(Double);
end;

procedure T_SWMM_XP_StandardInterfaceFileHeader.SetHeaderValue(AIndex: String;
	Value: Variant);
begin
	if AIndex = IM_IFHDR_TITLE1 then
		fTitles[1] := Value
	else if AIndex = IM_IFHDR_TITLE2 then
		fTitles[2] := Value
	else if AIndex = IM_IFHDR_TITLE3 then
		fTitles[3] := Value
	else if AIndex = IM_IFHDR_TITLE4 then
		fTitles[4] := Value
	else if AIndex = IM_IFHDR_SOURCE then
		fSourceBlock := Value
	else if AIndex = IM_IFHDR_STARTDATE then
		fStartDate := Value
	else if AIndex = IM_IFHDR_NUMDATA then
		fNumFlows := Value
	else if AIndex = IM_IFHDR_ISALPHA then
	begin
		// XP files always use alphanum IDs, so don't do anything
	end
	else if AIndex = IM_IFHDR_AREA then
		fArea := Value
	else if AIndex = IM_IFHDR_MULTIPLIER then
		fFlowMultiplier := Value
	else
		raise EIMSetHeaderValue.CreateFmt('Called T_SWMM_XP_StandardInterfaceFileHeader.'+
			'SetHeaderValue with %s = %s', [AIndex, Value]);
end;

function T_SWMM_XP_StandardInterfaceFileHeader.GetHeaderValue(
  AIndex: String): Variant;
begin
	if AIndex = IM_IFHDR_TITLE1 then
		Result := fTitles[1]
	else if AIndex = IM_IFHDR_TITLE2 then
		Result := fTitles[2]
	else if AIndex = IM_IFHDR_TITLE3 then
		Result := fTitles[3]
	else if AIndex = IM_IFHDR_TITLE4 then
		Result := fTitles[4]
	else if AIndex = IM_IFHDR_SOURCE then
		Result := fSourceBlock
	else if AIndex = IM_IFHDR_STARTDATE then
		Result := fStartDate
	else if AIndex = IM_IFHDR_NUMDATA then
		Result := fNumFlows
	else if AIndex = IM_IFHDR_ISALPHA then
		Result := True
	else if AIndex = IM_IFHDR_AREA then
		Result := fArea
	else if AIndex = IM_IFHDR_MULTIPLIER then
		Result := fFlowMultiplier
	else
		Result := Unassigned;
end;

function T_SWMM_XP_StandardInterfaceFileHeader.GetIndexedHeaderValue(
	AIndex: Integer): Variant;
begin
	Result := fFlowIDs[AIndex];
end;

function T_SWMM_XP_StandardInterfaceFileHeader.NumIndexedHeaderValues: Integer;
begin
	Result := Length(fFlowIDs);
end;

procedure T_SWMM_XP_StandardInterfaceFileHeader.SetIndexedHeaderValue(
	AIndex: Integer; Value: Variant);
begin
	fFlowIDs[AIndex] := Value;
end;

function T_SWMM_XP_StandardInterfaceFileHeader.AddIndexedHeaderValue(
  Value: Variant): Integer;
begin
	SetLength(fFlowIDs, Length(fFlowIDs)+1);
	fFlowIDs[Length(fFlowIDs)-1] := Value;
  fNumFlows := Length(fFlowIDs);
end;

procedure T_SWMM_XP_StandardInterfaceFileHeader.ClearIndexedHeaderValues;
begin
	SetLength(fFlowIDs, 0);
	Finalize(fFlowIDs);
end;

{ T_SWMM_XP_InterfaceFileIO }

function T_SWMM_XP_InterfaceFileIO.ReadByte: Byte;
var
	Buf: Byte;
begin
	fStream.Read(Buf, SizeOf(Byte));
	Result := Buf;
end;

procedure T_SWMM_XP_InterfaceFileIO.WriteInteger(Value: Integer);
begin
	fStream.Write(Value, SizeOf(Value));
end;

constructor T_SWMM_XP_InterfaceFileIO.Create(AInterfaceFile: IIM_InterfaceFile;
	AStream: TStBufferedStream);
begin
	fInterfaceFile := Pointer(AInterfaceFile);
	fStream := AStream;
end;

procedure T_SWMM_XP_InterfaceFileIO.WriteByte(Value: Byte);
begin
	fStream.Write(Value, SizeOf(Value));
end;

function T_SWMM_XP_InterfaceFileIO.ReadDouble: Double;
var
	Buf: Double;
begin
	fStream.Read(Buf, SizeOf(Double));
	Result := Buf;
end;

procedure T_SWMM_XP_InterfaceFileIO.WriteDouble(Value: Double);
begin
	fStream.Write(Value, SizeOf(Value));
end;

procedure T_SWMM_XP_InterfaceFileIO.MoveToBeginning;
begin
	fStream.Seek(0, soFromBeginning);
end;

function T_SWMM_XP_InterfaceFileIO.ReadExtended: Extended;
var
	Buf: Extended;
begin
	fStream.Read(Buf, SizeOf(Extended));
	Result := Buf;
end;

procedure T_SWMM_XP_InterfaceFileIO.MoveToEnd;
begin
	MoveTo(0, soEnd);
end;

function T_SWMM_XP_InterfaceFileIO.MoveTo(Offset: Int64;
	Origin: TSeekOrigin): Int64;
begin
	fStream.Seek(Offset, Origin);
end;

procedure T_SWMM_XP_InterfaceFileIO.WriteExtended(Value: Extended);
begin
	fStream.Write(Value, SizeOf(Value));
end;

function T_SWMM_XP_InterfaceFileIO.IsEOF: Boolean;
begin
	Result := fStream.Position >= fStream.FastSize-1;
end;

function T_SWMM_XP_InterfaceFileIO.ReadSingle: Single;
var
	Buf: Single;
begin
	fStream.Read(Buf, SizeOf(Single));
	Result := Buf;
end;

function T_SWMM_XP_InterfaceFileIO.ReadString: String;
begin
	Result := '';
end;

function T_SWMM_XP_InterfaceFileIO.ReadString(ALength: Integer): String;
var
	Buf: String;
begin
	SetLength(Buf, ALength);
	fStream.Read(Buf[1], ALength);
	Result := Buf;
end;

procedure T_SWMM_XP_InterfaceFileIO.WriteSingle(Value: Single);
begin
	fStream.Write(Value, SizeOf(Value));
end;

procedure T_SWMM_XP_InterfaceFileIO.WriteString(Value: String);
begin
	fStream.Write(Value[1], Length(Value));
end;

procedure T_SWMM_XP_InterfaceFileIO.WriteString(Value: String;
	ALength: Integer);
begin
	WriteString(PadL(Value, ALength));
end;

function T_SWMM_XP_InterfaceFileIO.ReadInteger: Integer;
var
	Buf: Integer;
begin
	fStream.Read(Buf, SizeOf(Integer));
	Result := Buf;
end;

destructor T_SWMM_XP_InterfaceFileIO.Destroy;
begin
	fInterfaceFile := nil;
	fStream := nil;
	inherited;
end;

function T_SWMM_XP_InterfaceFileIO.IsBeginningOfData: Boolean;
begin
	Result := fStream.Position < IIM_InterfaceFile(fInterfaceFile).GetHeader.Size;
end;

procedure T_SWMM_XP_InterfaceFileIO.MoveToBeginningOfData;
begin
	fStream.Seek(IIM_InterfaceFile(fInterfaceFile).GetHeader.Size, soBeginning);
end;

procedure T_SWMM_XP_InterfaceFileIO.PreviousLine;
begin
	// No record markers in XP interface files; don't do anything
end;

procedure T_SWMM_XP_InterfaceFileIO.NextLine;
begin
	// No record markers in XP interface files; don't do anything
end;

function T_SWMM_XP_InterfaceFileIO.Position: Int64;
begin
	Result := fStream.Position;
end;

function T_SWMM_XP_InterfaceFileIO.Size: Int64;
begin
	Result := fStream.FastSize;
end;

{ T_SWMM_XP_StandardInterfaceFile_DataIterator }

function T_SWMM_XP_StandardInterfaceFile_DataIterator.HasNext: Boolean;
begin
	Result := not IIM_InterfaceFile(fInterfaceFile).GetIOServices.IsEOF;
end;

constructor T_SWMM_XP_StandardInterfaceFile_DataIterator.Create(
	AInterfaceFile: IIM_InterfaceFile);
begin
	fInterfaceFile := Pointer(AInterfaceFile);
end;

function T_SWMM_XP_StandardInterfaceFile_DataIterator.PreviousTime: TDateTime;
begin
	Result := fPreviousTime;
end;

function T_SWMM_XP_StandardInterfaceFile_DataIterator.CurrentTimeStep: Double;
begin
{	if SameDateTime(fNextTime, MaxDateTime) then
		Result := fCurrentTimeStep
	else
	begin
		Result := fNextTime - fCurrentTime;
		fCurrentTimeStep := Result;
	end;}
	Result := fCurrentTimeStep;
end;

function T_SWMM_XP_StandardInterfaceFile_DataIterator.NextTime: TDateTime;
begin
	Result := fNextTime;
end;

function T_SWMM_XP_StandardInterfaceFile_DataIterator.Current: IIM_InterfaceTimeSeriesData;
begin
	Result := IIM_InterfaceTimeSeriesData(fData);
end;

function T_SWMM_XP_StandardInterfaceFile_DataIterator.CurrentTime: TDateTime;
begin
	Result := IIM_InterfaceTimeSeriesData(fData).Time;
end;

function T_SWMM_XP_StandardInterfaceFile_DataIterator.Previous: IIM_InterfaceTimeSeriesData;
var
	IO: IIM_InterfaceFileIO;
	SecondPreviousData: IIM_InterfaceTimeSeriesData;
begin
	if HasPrevious then
	begin
		IO := IIM_InterfaceFile(fInterfaceFile).GetIOServices;
		if Assigned(fData) then
			fNextTime := IIM_InterfaceTimeSeriesData(fData).Time
		else
			fNextTime := MaxDateTime;
		fData := nil;
		IO.MoveTo(-2*IIM_InterfaceTimeSeriesData(fData).Size, soCurrent);
		if IO.IsBeginningOfData then
			fPreviousTime := MinDateTime
		else
		begin
			SecondPreviousData := T_SWMM_XP_StandardInterfaceFile_TimeSeriesData.Create(
				IIM_InterfaceFile(fInterfaceFile), Self);
			fPreviousTime := SecondPreviousData.Time;
		end;
		Result := T_SWMM_XP_StandardInterfaceFile_TimeSeriesData.Create(
			IIM_InterfaceFile(fInterfaceFile), Self);
		fData := Pointer(Result);
		fCurrentTime := Result.Time;
		fCurrentTimeStep := Result.DataValue[IM_IF_TIMESTEP];
	end
	else
	begin
		Result := nil;
		fPreviousTime := MinDateTime;
		fNextTime := fCurrentTime;
		fCurrentTime := MinDateTime;
		fCurrentTimeStep := 0;
	end;
end;

destructor T_SWMM_XP_StandardInterfaceFile_DataIterator.Destroy;
begin
	fData := nil;
	fInterfaceFile := nil;
	inherited;
end;

function T_SWMM_XP_StandardInterfaceFile_DataIterator.Next: IIM_InterfaceTimeSeriesData;
var
	IO: IIM_InterfaceFileIO;
	SecondNextData: IIM_InterfaceTimeSeriesData;
begin
	if HasNext then
	begin
		IO := IIM_InterfaceFile(fInterfaceFile).GetIOServices;
		if Assigned(fData) then
			fPreviousTime := IIM_InterfaceTimeSeriesData(fData).Time
		else
			fPreviousTime := MinDateTime;
		fData := nil;
		Result := T_SWMM_XP_StandardInterfaceFile_TimeSeriesData.Create(IIM_InterfaceFile(fInterfaceFile), Self);
		fCurrentTimeStep := Result.DataValue[IM_IF_TIMESTEP];
		fData := Pointer(Result);
		fCurrentTime:= Result.Time;
		if IO.IsEOF then
			fNextTime := MaxDateTime
		else
		begin
//			SecondNextData := T_SWMM_XP_StandardInterfaceFile_TimeSeriesData.Create(IIM_InterfaceFile(fInterfaceFile),
//				Self);
//			fNextTime := SecondNextData.Time;
			fNextTime := IncSecond(fCurrentTime, Round(fCurrentTimeStep));
		end;
//		IIM_InterfaceFile(fInterfaceFile).GetIOServices.MoveTo(-SecondNextData.Size, soCurrent);
	end
	else
	begin
		Result := nil;
		fPreviousTime := fCurrentTime;
		fCurrentTime := MaxDateTime;
		fNextTime := MaxDateTime;
		fCurrentTimeStep := 0;
	end;
end;

function T_SWMM_XP_StandardInterfaceFile_DataIterator.HasPrevious: Boolean;
begin
	Result := not IIM_InterfaceFile(fInterfaceFile).GetIOServices.IsBeginningOfData;
end;

function T_SWMM_XP_StandardInterfaceFile_DataIterator.PositionPercent: Integer;
var
	IFPos: Int64;
	IFSize: Int64;
	IO: IIM_InterfaceFileIO;
	IFPercentPos: Double;
begin
	IO := IIM_InterfaceFile(fInterfaceFile).GetIOServices;
	IFPos := IO.Position;
	IFSize := IO.Size;
	IFPercentPos := IFPos/IFSize*100;
	Result := Round(IFPercentPos);
end;

function T_SWMM_XP_StandardInterfaceFile_DataIterator.Position: Int64;
begin
	Result := IIM_InterfaceFile(fInterfaceFile).GetIOServices.Position;
end;

function T_SWMM_XP_StandardInterfaceFile_DataIterator.DataValidUntilTime: TDateTime;
begin
	Result := IncSecond(fCurrentTime, Round(fCurrentTimeStep));
	if CompareDateTime(Result, MaxDateTime) = GreaterThanValue then
		Result := MaxDateTime;
end;

{ T_SWMM_XP_StandardInterfaceFile_TimeSeriesData }

constructor T_SWMM_XP_StandardInterfaceFile_TimeSeriesData.Create
	(AInterfaceFile: IIM_InterfaceFile;
	AIterator: IIM_InterfaceTimeSeriesDataIterator);
var
	IO: IIM_InterfaceFileIO;
	JulDate: Integer;
	Seconds: Double;
begin
	fInterfaceFile := Pointer(AInterfaceFile);
	fIterator := Pointer(AIterator);
	IO := IIM_InterfaceFile(fInterfaceFile).GetIOServices;
	JulDate := IO.ReadInteger;
	Seconds := IO.ReadDouble;
	fTime := DateTimeOfJulDate(JulDate, Seconds);
	fTimeStep := IO.ReadDouble;
	ReadData;
end;

procedure T_SWMM_XP_StandardInterfaceFile_TimeSeriesData.SetIndexedDataValue(
	AIndex: Integer; Value: Variant);
begin
	if fIndexedData[AIndex] <> Value then
		fIndexedData[AIndex] := Value;
end;

function T_SWMM_XP_StandardInterfaceFile_TimeSeriesData.GetDataValue(
	AIndex: String): Variant;
begin
	if AIndex = IM_IF_TIMESTEP then
		Result := fTimeStep;
end;

function T_SWMM_XP_StandardInterfaceFile_TimeSeriesData.GetIndexedDataValue(
	AIndex: Integer): Variant;
begin
	Result := fIndexedData[AIndex];
end;

procedure T_SWMM_XP_StandardInterfaceFile_TimeSeriesData.SetDataValue(
	AIndex: String; Value: Variant);
begin
	if AIndex = IM_IF_TIMESTEP then
		if not SameValue(fTimeStep, Value) then
			fTimeStep := Value;
end;

procedure T_SWMM_XP_StandardInterfaceFile_TimeSeriesData.ReadData;
var
	i: Integer;
	Header: IIM_InterfaceHeader;
	IO: IIM_InterfaceFileIO;
	NumDataPoints: Integer;
begin
	Header := IIM_InterfaceFile(fInterfaceFile).GetHeader;
	IO := IIM_InterfaceFile(fInterfaceFile).GetIOServices;
	NumDataPoints := Header.HeaderValue[IM_IFHDR_NUMDATA];
	SetLength(fIndexedData, NumDataPoints);
	for i := 0 to NumDataPoints-1 do
		fIndexedData[i] := IO.ReadDouble;
end;

destructor T_SWMM_XP_StandardInterfaceFile_TimeSeriesData.Destroy;
begin
	fInterfaceFile := nil;
	inherited;
end;

procedure T_SWMM_XP_StandardInterfaceFile_TimeSeriesData.WriteData;
var
	i: Integer;
	Header: IIM_InterfaceHeader;
	IO: IIM_InterfaceFileIO;
	NumDataPoints: Integer;
begin
	Header := IIM_InterfaceFile(fInterfaceFile).GetHeader;
	IO := IIM_InterfaceFile(fInterfaceFile).GetIOServices;
	NumDataPoints := Header.HeaderValue[IM_IFHDR_NUMDATA];
	IO.WriteInteger(Y2KJulDateOfDateTime(fTime));
	IO.WriteDouble(SecondsOfDayOfDateTime(fTime));
	IO.WriteDouble(fTimeStep);
	for i := 0 to NumDataPoints-1 do
		IO.WriteDouble(fIndexedData[i]);
end;

function T_SWMM_XP_StandardInterfaceFile_TimeSeriesData.Time: TDateTime;
begin
	Result := fTime;
end;

function T_SWMM_XP_StandardInterfaceFile_TimeSeriesData.Size: Int64;
begin
	Result := SizeOf(Integer) + SizeOf(Double) + SizeOf(Double) +
		NumIndexedDataValues * SizeOf(Double);
end;

function T_SWMM_XP_StandardInterfaceFile_TimeSeriesData.NumIndexedDataValues: Integer;
begin
	Result := Length(fIndexedData);
end;

{ T_SWMM_XP_InterfaceFile_NodeFilter }

constructor T_SWMM_XP_InterfaceFile_NodeFilter.Create(AID, ANewID, ANewSeries: String;
	AInclude: Boolean; AExpression: String);
begin
	fID := AID;
	fNewID := ANewID;
	fNewSeries := ANewSeries;
	fInclude := AInclude;
	if AExpression = '' then
		fExpression := fID
	else
		fExpression := AExpression;
end;

constructor T_SWMM_XP_InterfaceFile_NodeFilter.Create;
begin
	// Do nothing
end;

function T_SWMM_XP_InterfaceFile_NodeFilter.GetInclude: Boolean;
begin
	Result := fInclude;
end;

function T_SWMM_XP_InterfaceFile_NodeFilter.GetID: String;
begin
	Result := fID;
end;

function T_SWMM_XP_InterfaceFile_NodeFilter.GetExpression: String;
begin
	Result := fExpression;
end;

procedure T_SWMM_XP_InterfaceFile_NodeFilter.SetInclude(Value: Boolean);
begin
	if Value <> fInclude then
		fInclude := Value;
end;

procedure T_SWMM_XP_InterfaceFile_NodeFilter.SetID(Value: String);
begin
	if CompareStr(Value, fID) <> 0 then
		fID := Value;
end;

function T_SWMM_XP_InterfaceFile_NodeFilter.GetNewID: String;
begin
	Result := fNewID;
end;

procedure T_SWMM_XP_InterfaceFile_NodeFilter.SetExpression(Value: String);
begin
	if CompareStr(Value, fExpression) <> 0 then
		fExpression := Value;
end;

destructor T_SWMM_XP_InterfaceFile_NodeFilter.Destroy;
begin
	// Do nothing
	inherited;
end;

procedure T_SWMM_XP_InterfaceFile_NodeFilter.SetNewID(Value: String);
begin
	if CompareStr(Value, fNewID) <> 0 then
		fNewID := Value;
end;

function T_SWMM_XP_InterfaceFile_NodeFilter.GetNewSeries: String;
begin
	Result := fNewSeries;
end;

procedure T_SWMM_XP_InterfaceFile_NodeFilter.SetNewSeries(Value: String);
begin
	if CompareStr(Value, fNewSeries) <> 0 then
		fNewSeries := Value;
end;

function T_SWMM_XP_InterfaceFile_NodeFilter.GetFinalID: String;
begin
	if Length(fNewID) = 0 then
		Result := fID
	else
		Result := fNewID;
end;

{ T_SWMM_XP_ConvertFilterPackage }

procedure T_SWMM_XP_ConvertFilterPackage.SetupFilters(
  SourceInterface: IIM_InterfaceFile);
var
	i: Integer;
	SourceHeader: IIM_InterfaceHeader;
begin
	SourceHeader := SourceInterface.GetHeader;

	fFilters.Clear;
	for i := 0 to SourceHeader.NumIndexedHeaderValues-1 do
		AddFilter(T_SWMM_XP_InterfaceFile_NodeFilter.Create(
			SourceHeader.IndexedHeaderValue[i]));
end;

function T_SWMM_XP_ConvertFilterPackage.NumIncludes: Integer;
var
	NumIncluded: Integer;
	Intf: IInterface;
begin
	NumIncluded := 0;
	for	Intf in fFilters do
		if IIM_ConvertFilter(Intf).Include then
			Inc(NumIncluded);
	Result := NumIncluded;
end;

function T_SWMM_XP_ConvertFilterPackage.GetFilter(
  AIndex: Integer): IIM_ConvertFilter;
begin
	Result := IIM_ConvertFilter(fFilters[AIndex]);
end;

procedure T_SWMM_XP_ConvertFilterPackage.DeleteFilter(
  AFilter: IIM_ConvertFilter);
begin
	fFilters.Delete(fFilters.IndexOf(AFilter));
end;

function T_SWMM_XP_ConvertFilterPackage.IncludeLookup: TIndexLookupList;
var
	i: Integer;
begin
	Result := TIndexLookupList.Create;
	for i := 0 to fFilters.Count-1 do
		if IIM_ConvertFilter(fFilters[i]).Include then
			Result.Add(i);
end;

function T_SWMM_XP_ConvertFilterPackage.IsIncluded(AIndex: Integer): Boolean;
begin
	Result := IIM_ConvertFilter(fFilters[AIndex]).Include;
end;

function T_SWMM_XP_ConvertFilterPackage.GetFilterByID(
  ID: String): IIM_ConvertFilter;
var
	FindIndex: Integer;
	i: Integer;
begin
	Result :=  nil;
	for i := 0 to fFilters.Count-1 do
		if CompareText(IIM_ConvertFilter(fFilters[i]).ID, ID) = 0 then
			Result := IIM_ConvertFilter(fFilters[i]);
end;

procedure T_SWMM_XP_ConvertFilterPackage.SetFilter(AIndex: Integer;
  Value: IIM_ConvertFilter);
begin
	fFilters[AIndex] := nil;
	fFilters[AIndex] := Value;
end;

procedure T_SWMM_XP_ConvertFilterPackage.AddFilter(AFilter: IIM_ConvertFilter);
begin
	fFilters.Add(AFilter);
end;

procedure T_SWMM_XP_ConvertFilterPackage.SetFilterByID(ID: String;
	Value: IIM_ConvertFilter);
var
	i: Integer;
begin
	for i := 0 to fFilters.Count-1 do
		if IIM_ConvertFilter(fFilters[i]).ID = ID then
		begin
			fFilters[i] := nil;
			fFilters[i] := Value;
			Break;
		end;
end;

constructor T_SWMM_XP_ConvertFilterPackage.Create;
begin
	fFilters := TInterfaceList.Create;
end;

destructor T_SWMM_XP_ConvertFilterPackage.Destroy;
begin
	fFilters.Free;
  inherited;
end;

end.
