unit uIM_MOUSE_PRF_InterfaceFiles;

interface

uses Windows, SysUtils, Classes, Math, DateUtils, Variants,
	PDXDateUtils, StStrms, StStrL,
	uIM_InterfaceFiles;

const
	IM_MOUSEPRF_TSTYPE_TIME_SERIES = 2;
	IM_MOUSEPRF_TSTYPE_NODE_WATER_LEVEL = 103;
	IM_MOUSEPRF_TSTYPE_PUMP_DISCHARGE = 203;
	IM_MOUSEPRF_TSTYPE_HOT_START_PUMP = 81;
	IM_MOUSEPRF_TSTYPE_WEIRGATE_POSITION = 110;
	IM_MOUSEPRF_TSTYPE_WEIR_DISCHARGE = 204;
	IM_MOUSEPRF_TSTYPE_HOT_START_REGULATOR = 83;
	IM_MOUSEPRF_TSTYPE_LINK_WATER_LEVEL = 100;
	IM_MOUSEPRF_TSTYPE_LINK_DISCHARGE = 200;
	IM_MOUSEPRF_TSTYPE_LINK_VELOCITY = 300;
	IM_MOUSEPRF_TSTYPE_SYSTEM_VOLUME = 250;
	IM_MOUSEPRF_SOURCEBLOCK = 'MOUSE PRF           ';
	IM_MOUSEPRF_FORMAT_DESC = 'DHI MOUSE PRF';
	TempM11OutFileName = 'M11.OUT';
	TempM11InFileName = 'M11.IN';
	TempM11TextFileName = 'M11.TXT';

type

	T_MOUSE_PRF_StandardInterfaceFileHeader = class;
	T_MOUSE_PRF_InterfaceFileIO = class;
	{=============================================================================
		 Name: T_MOUSE_PRF_StandardInterfaceFile
		 Purpose: Provides services for reading MOUSE PRF files converted to text
			 by M11EXTRA
		 Requirements: -
	 ============================================================================}
	T_MOUSE_PRF_StandardInterfaceFile = class(TInterfacedObject,
		IIM_InterfaceFile)
	private
		fFileName: TFileName;
		fStream: TFileStream;
		fBuffer: TStAnsiTextStream;
		fHeader: IIM_InterfaceHeader;
		IO: IIM_InterfaceFileIO;
	protected
	//-IIM_InterfaceHeader implementation-----------------------------------------
		function GetHeaderValue(AIndex: String): Variant;
		procedure SetHeaderValue(AIndex: String; Value: Variant);
	//-IIM_InterfaceFile implementation-------------------------------------------
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
	end;

	T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData = class;
	{=============================================================================
		 Name: T_MOUSE_PRF_StandardInterfaceFile_DataIterator
		 Purpose: Provides a way to traverse back and forth an XP standard interface
			 file
		 Requirements: a T_MOUSE_PRF_StandardInterfaceFile
		 Restrictions: usage restricted to T_MOUSE_PRF_StandardInterfaceFile
	 ============================================================================}
	T_MOUSE_PRF_StandardInterfaceFile_DataIterator = class(TInterfacedObject,
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
		 Name: T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData
		 Purpose: Provides access to standard interface file data
		 Requirements: a T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesDataIterator
	 ============================================================================}
	T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData = class(TInterfacedObject,
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
		Name:
		Purpose:
		Requirements:
	=============================================================================}
	T_MOUSE_PRF_Series = record
		ID: String;
		NodeType: Integer;
		NodeTypeDescription: String;
		UpNodeID: String;
		DnNodeID: String;
		Position: Double;
		PositionUnits: String;
		Value: Double;
	end;

	{=============================================================================
		 Name: T_MOUSE_PRF_StandardInterfaceFileHeader
		 Purpose:
		 Requirements:
		 Restrictions: usage restricted to T_MOUSE_PRF_StandardInterfaceFile
	 ============================================================================}
	T_MOUSE_PRF_StandardInterfaceFileHeader = class(TInterfacedObject,
		IIM_InterfaceHeader)
	private
		fInterfaceFile: Pointer;
		fStartDate: TDateTime;
		fSeries: array of T_MOUSE_PRF_Series;
		fSize: Int64;
	protected
		function GetStartDate: TDateTime;
		procedure SetStartDate(Value: TDateTime);
		function GetSeries(AIndex: Integer): T_MOUSE_PRF_Series;
		procedure SetSeries(AIndex: Integer; Value: T_MOUSE_PRF_Series);
		function GetNumSeries: Integer;
	private
	//-Object management----------------------------------------------------------
		constructor Create(AInterfaceFile: IIM_InterfaceFile);
		destructor Destroy; override;
	public
	//----------------------------------------------------------------------------
	//-Object properties----------------------------------------------------------
		property StartDate: TDateTime read GetStartDate write SetStartDate;
		property Series[AIndex: Integer]: T_MOUSE_PRF_Series read GetSeries
			write SetSeries;
		property NumSeries: Integer read GetNumSeries;
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
		property IndexedHeaderValue[AIndex: Integer]: Variant read GetIndexedHeaderValue
			write SetIndexedHeaderValue;
		function AddIndexedHeaderValue(Value: Variant): Integer;
		procedure ClearIndexedHeaderValues;
	end;

	{=============================================================================
		 Name: T_MOUSE_PRF_InterfaceFileIO
		 Purpose:	Provides IO services for XP interface file
		 Requirements: usage restricted to T_MOUSE_PRF_StandardInterfaceFile
	 ============================================================================}
	T_MOUSE_PRF_InterfaceFileIO = class(TInterfacedObject,
		IIM_InterfaceFileIO)
	private
		fStream: TStAnsiTextStream;
		fInterfaceFile: Pointer;
		fTokens: TStringList;
		fPreviousLine: String;
		fCurrentLine: String;
		fNextLine: String;
		fCurrentTokenIndex: Integer;
	protected
		procedure CheckCurrentLineTokens;
	public
	//-Object management----------------------------------------------------------
		constructor Create(AInterfaceFile: IIM_InterfaceFile; AStream: TStAnsiTextStream);
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

function StripAngleBrackets(AString: String): String;

implementation

uses Forms, RzLaunch, StrUtils, ConvUtils, StdConvs, CodeSiteLogging, Types;

var
	DateTimeStr: String;
	DateTimeFormatSettings: TFormatSettings;

////////////////////////////////////////////////////////////////////////////////

function StripAngleBrackets(AString: String): String;
begin
	Result := StringReplace(AString, '<', '', [rfReplaceAll]);
	Result := StringReplace(Result, '>', '', [rfReplaceAll]);
end;

{ T_MOUSE_PRF_StandardInterfaceFile }

function T_MOUSE_PRF_StandardInterfaceFile.GetHeader: IIM_InterfaceHeader;
begin
	Result := fHeader;
end;

constructor T_MOUSE_PRF_StandardInterfaceFile.Create(AFile: TFileName;
	Mode: Word);
var
	InStream: TFileStream;
	InTextFile: TStAnsiTextStream;
	OutStream: TFileStream;
	OutTextFile: TStAnsiTextStream;
	appLauncher: TRzLauncher;
	CurrentID: String;
	IDs: TStringList;
	Tokens: TStringList;
	i: Integer;
	CurrentLine: String;
	GeneratedTextFileName: TFileName;
begin
	fFileName := AFile;

	CodeSite.Send('Creating standard file ref', fFileName);
	// PRF files cannot be read directly, so we use M11EXTRA to extract the file
	// into a text form (M11.TXT).  First, run M11EXTRA AFile to get M11.OUT, which
	// is automatically written when using this form of M11EXTRA.  Then, to get the
	// text form, set the initial 0 toggles at each line of M11.OUT to 1, save the
	// file as M11.IN and run M11EXTRA AFile M11.TXT. The M11.TXT file is the final
	// text form

	// Run M11EXTRA AFile to get M11.OUT
	appLauncher := TRzLauncher.Create(Application);
	appLauncher.FileName := ExtractFileDir(ParamStr(0))+'\M11EXTRA.EXE';
	appLauncher.Parameters := ExtractFileName(AFile);
	appLauncher.StartDir := ExtractFileDir(AFile);
	appLauncher.ShowMode := smMinimized;
	appLauncher.WaitType := wtFullStop;
	appLauncher.WaitUntilFinished := True;
	appLauncher.Timeout := -1;
	appLauncher.Action := 'Open';
	appLauncher.Launch;
	Application.ProcessMessages;

	// Write out M11.IN
	OutStream := TFileStream.Create(ExtractFileDir(AFile)+'\' + TempM11OutFileName,
		fmShareDenyWrite);
	OutTextFile := TStAnsiTextStream.Create(OutStream);
	InStream := TFileStream.Create(ExtractFileDir(AFile) + '\' + TempM11InFileName,
		fmCreate);
	InTextFile := TStAnsiTextStream.Create(InStream);

	Tokens := TStringList.Create;
	IDs := TStringList.Create;
	for i := 1 to 12 do
	begin
		CurrentLine := OutTextFile.ReadLine;
		InTextFile.WriteLine(CurrentLine);
	end;
	CurrentLine := OutTextFile.ReadLine;
	CurrentLine := '1' + RightStr(CurrentLine, Length(CurrentLine)-1);
	InTextFile.WriteLine(CurrentLine);
	while not OutTextFile.AtEndOfStream do
	begin
		// Extract only Link Discharges
		CurrentLine := OutTextFile.ReadLine;
		ExtractTokensL(CurrentLine, ' ', '''', False, Tokens);
		CurrentID := StripAngleBrackets(Tokens[3]);
		if IDs.IndexOf(CurrentID) = -1 then
		begin
			IDs.Add(CurrentID);
			if StrToInt(Tokens[1]) = IM_MOUSEPRF_TSTYPE_LINK_DISCHARGE then
				CurrentLine := '1' + RightStr(CurrentLine, Length(CurrentLine)-1);
		end;
		InTextFile.WriteLine(CurrentLine);
	end;
	IDs.Free;
	Tokens.Free;
	OutTextFile.Free;
	OutStream.Free;
	InTextFile.Free;
	InStream.Free;

	// Write out M11.TXT
	// Account for more than 1 M11.txt file being generated in same dir
	if FileExists(AFile+'.TXT') then
	begin
		fStream := TFileStream.Create(ExtractFileDir(AFile)+'\'+
			ExtractFileName(AFile)+'.TXT', fmShareDenyWrite);
	end
	else
	begin
		appLauncher.Parameters := ExtractFileName(AFile) + ' ' +
			ExtractFileName(AFile)+'.TXT';
		appLauncher.Launch;

		Application.ProcessMessages;

		appLauncher.Free;
		fStream := TFileStream.Create(ExtractFileDir(AFile)+'\'+
			ExtractFileName(AFile)+'.TXT', Mode);
	end;
	fBuffer := TStAnsiTextStream.Create(fStream);
	fHeader := T_MOUSE_PRF_StandardInterfaceFileHeader.Create(Self);
	IO := T_MOUSE_PRF_InterfaceFileIO.Create(Self, fBuffer);
	if Mode <> fmCreate then
		fHeader.ReadHeader;
end;

procedure T_MOUSE_PRF_StandardInterfaceFile.SetHeaderValue(AIndex: String;
	Value: Variant);
begin
	Assert(false, 'T_MOUSE_PRF_StandardInterfaceFile.SetHeaderValue not implemented');
end;

function T_MOUSE_PRF_StandardInterfaceFile.GetIOServices: IIM_InterfaceFileIO;
begin
	Result := IO;
end;

procedure T_MOUSE_PRF_StandardInterfaceFile.ReadInterface;
begin
	fHeader.ReadHeader;
end;

procedure T_MOUSE_PRF_StandardInterfaceFile.WriteInterface;
begin
	fHeader.WriteHeader;
end;

function T_MOUSE_PRF_StandardInterfaceFile.GetTimeSeriesDataIterator: IIM_InterfaceTimeSeriesDataIterator;
begin
	Result := T_MOUSE_PRF_StandardInterfaceFile_DataIterator.Create(Self);
end;

function T_MOUSE_PRF_StandardInterfaceFile.GetHeaderValue(
	AIndex: String): Variant;
begin
	Assert(false, 'T_MOUSE_PRF_StandardInterfaceFile.GetHeaderValue not implemented');
end;

destructor T_MOUSE_PRF_StandardInterfaceFile.Destroy;
var
	FileToDelete: TFileName;
begin
	fBuffer.Free;
	fStream.Free;

	FileToDelete := ExtractFileDir(fFileName) + '\' + TempM11TextFileName;
	if FileExists(FileToDelete) then
		DeleteFile(FileToDelete);
	FileToDelete := ExtractFileDir(fFileName) + '\' + TempM11InFileName;
	if FileExists(FileToDelete) then
		DeleteFile(FileToDelete);
	FileToDelete := ExtractFileDir(fFileName) + '\' + TempM11OutFileName;
	if FileExists(FileToDelete) then
		DeleteFile(FileToDelete);

	inherited;
end;

function T_MOUSE_PRF_StandardInterfaceFile.GetFileName: TFileName;
begin
	Result := fFileName;
end;

function T_MOUSE_PRF_StandardInterfaceFile.GetFormat: String;
begin
	Result := IM_MOUSEPRF_FORMAT_DESC;
end;

{ T_MOUSE_PRF_StandardInterfaceFileHeader }

constructor T_MOUSE_PRF_StandardInterfaceFileHeader.Create(
	AInterfaceFile: IIM_InterfaceFile);
begin
	fInterfaceFile := Pointer(AInterfaceFile);
end;

procedure T_MOUSE_PRF_StandardInterfaceFileHeader.SetHeaderValue(AIndex: String;
	Value: Variant);
begin
	if AIndex = IM_IFHDR_TITLE1 then
	begin
		// Do nothing
	end
	else if AIndex = IM_IFHDR_TITLE2 then
	begin
		// Do nothing
	end
	else if AIndex = IM_IFHDR_TITLE3 then
	begin
		// Do nothing
	end
	else if AIndex = IM_IFHDR_TITLE4 then
	begin
		// Do nothing
	end
	else if AIndex = IM_IFHDR_SOURCE then
	begin
		// Do nothing
	end
	else if AIndex = IM_IFHDR_STARTDATE then
		fStartDate := Value
	else if AIndex = IM_IFHDR_NUMDATA then
	begin
		SetLength(fSeries, Integer(Value));
	end
	else if AIndex = IM_IFHDR_ISALPHA then
	begin
		// XP files always use alphanum IDs, so don't do anything
	end
	else if AIndex = IM_IFHDR_AREA then
	begin
		// Do nothing
	end
	else if AIndex = IM_IFHDR_MULTIPLIER then
	begin
		// Do nothing
	end
	else
		raise EIMSetHeaderValue.CreateFmt('Called T_SWMM_XP_StandardInterfaceFileHeader.'+
			'SetHeaderValue with %s = %s', [AIndex, Value]);
end;

procedure T_MOUSE_PRF_StandardInterfaceFileHeader.ReadHeader;
var
	IO: IIM_InterfaceFileIO;
	ReadingHeader: Boolean;
	Token: String;
	ANodeType: Integer;
	NewNodeID: Integer;
	DateTimeStr: String;
	DateTimeFormatSettings: TFormatSettings;
begin
	IO := IIM_InterfaceFile(fInterfaceFile).GetIOServices;
	IO.MoveToBeginning;
	IO.NextLine; // *M11 CHAN
	// Read time series available

	ReadingHeader := True;
	while ReadingHeader do
	begin
		// Read the import flag (a 1 in the first column or the *M11 DATA header end marker
		Token := IO.ReadString;
		if Token = '*M11' then
		begin
			ReadingHeader := False;
			IO.NextLine;

			// Read initial date then backup
			DateTimeStr := IO.ReadString + ' ' + IO.ReadString + ':'+ IO.ReadString + ':' +
				IO.ReadString;
			GetLocaleFormatSettings(LOCALE_SYSTEM_DEFAULT, DateTimeFormatSettings);
			DateTimeFormatSettings.DateSeparator := '-';
			DateTimeFormatSettings.TimeSeparator := ':';
			DateTimeFormatSettings.ShortDateFormat := 'YYYY-MM-DD';
			DateTimeFormatSettings.ShortTimeFormat := 'HH:MM:SS';
			fStartDate := StrToDateTime(DateTimeStr, DateTimeFormatSettings);
			IO.PreviousLine;
			
			Continue;
		end;

		// Read in a series descriptor
		ANodeType := IO.ReadInteger;
		NewNodeID := Length(fSeries);
		case ANodeType of
			IM_MOUSEPRF_TSTYPE_TIME_SERIES: begin
				IO.NextLine;
			end;
			IM_MOUSEPRF_TSTYPE_NODE_WATER_LEVEL: begin
				SetLength(fSeries, NewNodeID+1);
				fSeries[NewNodeID].NodeTypeDescription := IO.ReadString;
				fSeries[NewNodeID].ID := StripAngleBrackets(IO.ReadString);
				fSeries[NewNodeID].NodeType := IM_MOUSEPRF_TSTYPE_NODE_WATER_LEVEL;
			end;
			IM_MOUSEPRF_TSTYPE_PUMP_DISCHARGE: begin
				SetLength(fSeries, NewNodeID+1);
				fSeries[NewNodeID].NodeTypeDescription := IO.ReadString;
				fSeries[NewNodeID].ID := StripAngleBrackets(IO.ReadString);
				fSeries[NewNodeID].UpNodeID := IO.ReadString;
				fSeries[NewNodeID].DnNodeID := IO.ReadString;
				fSeries[NewNodeID].NodeType := IM_MOUSEPRF_TSTYPE_PUMP_DISCHARGE;
			end;
			IM_MOUSEPRF_TSTYPE_HOT_START_PUMP: begin
				IO.NextLine;
			end;
			IM_MOUSEPRF_TSTYPE_WEIRGATE_POSITION: begin
				SetLength(fSeries, NewNodeID+1);
				fSeries[NewNodeID].NodeTypeDescription := IO.ReadString;
				fSeries[NewNodeID].ID := StripAngleBrackets(IO.ReadString);
				fSeries[NewNodeID].UpNodeID := IO.ReadString;
				fSeries[NewNodeID].DnNodeID := IO.ReadString;
				fSeries[NewNodeID].NodeType := IM_MOUSEPRF_TSTYPE_WEIRGATE_POSITION;
			end;
			IM_MOUSEPRF_TSTYPE_WEIR_DISCHARGE: begin
				SetLength(fSeries, NewNodeID+1);
				fSeries[NewNodeID].NodeTypeDescription := IO.ReadString;
				fSeries[NewNodeID].ID := StripAngleBrackets(IO.ReadString);
				fSeries[NewNodeID].UpNodeID := IO.ReadString;
				fSeries[NewNodeID].DnNodeID := IO.ReadString;
				fSeries[NewNodeID].NodeType := IM_MOUSEPRF_TSTYPE_WEIR_DISCHARGE;
			end;
			IM_MOUSEPRF_TSTYPE_HOT_START_REGULATOR: begin
				IO.NextLine;
			end;
			IM_MOUSEPRF_TSTYPE_LINK_WATER_LEVEL: begin
				SetLength(fSeries, NewNodeID+1);
				fSeries[NewNodeID].NodeTypeDescription := IO.ReadString;
				fSeries[NewNodeID].ID := StripAngleBrackets(IO.ReadString);
				fSeries[NewNodeID].UpNodeID := IO.ReadString;
				fSeries[NewNodeID].DnNodeID := IO.ReadString;
				fSeries[NewNodeID].Position := IO.ReadDouble;
				fSeries[NewNodeID].PositionUnits := IO.ReadString;
				fSeries[NewNodeID].NodeType := IM_MOUSEPRF_TSTYPE_LINK_WATER_LEVEL;
			end;
			IM_MOUSEPRF_TSTYPE_LINK_DISCHARGE: begin
				SetLength(fSeries, NewNodeID+1);
				fSeries[NewNodeID].NodeTypeDescription := IO.ReadString;
				fSeries[NewNodeID].ID := StripAngleBrackets(IO.ReadString);
				fSeries[NewNodeID].UpNodeID := IO.ReadString;
				fSeries[NewNodeID].DnNodeID := IO.ReadString;
				fSeries[NewNodeID].Position := IO.ReadDouble;
				fSeries[NewNodeID].PositionUnits := IO.ReadString;
				fSeries[NewNodeID].NodeType := IM_MOUSEPRF_TSTYPE_LINK_DISCHARGE;
			end;
			IM_MOUSEPRF_TSTYPE_LINK_VELOCITY: begin
				SetLength(fSeries, NewNodeID+1);
				fSeries[NewNodeID].NodeTypeDescription := IO.ReadString;
				fSeries[NewNodeID].ID := StripAngleBrackets(IO.ReadString);
				fSeries[NewNodeID].UpNodeID := IO.ReadString;
				fSeries[NewNodeID].DnNodeID := IO.ReadString;
				fSeries[NewNodeID].Position := IO.ReadDouble;
				fSeries[NewNodeID].PositionUnits := IO.ReadString;
				fSeries[NewNodeID].NodeType := IM_MOUSEPRF_TSTYPE_LINK_VELOCITY;
			end;
			IM_MOUSEPRF_TSTYPE_SYSTEM_VOLUME: begin
				SetLength(fSeries, NewNodeID+1);
				fSeries[NewNodeID].NodeTypeDescription := IO.ReadString;
				fSeries[NewNodeID].ID := StripAngleBrackets(IO.ReadString);
				fSeries[NewNodeID].NodeType := IM_MOUSEPRF_TSTYPE_SYSTEM_VOLUME;
			end;
		end;
	end;
	fSize := IO.Position;
end;

procedure T_MOUSE_PRF_StandardInterfaceFileHeader.WriteHeader;
begin
	Assert(False, 'T_MOUSE_PRF_StandardInterfaceFile.WriteHeader not implemented');
end;

function T_MOUSE_PRF_StandardInterfaceFileHeader.Size: Int64;
begin
	Result := fSize;
end;

function T_MOUSE_PRF_StandardInterfaceFileHeader.GetHeaderValue(
	AIndex: String): Variant;
begin
	if AIndex = IM_IFHDR_TITLE1 then
		Result := ''
	else if AIndex = IM_IFHDR_TITLE2 then
		Result := ''
	else if AIndex = IM_IFHDR_TITLE3 then
		Result := ''
	else if AIndex = IM_IFHDR_TITLE4 then
		Result := ''
	else if AIndex = IM_IFHDR_SOURCE then
		Result := IM_MOUSEPRF_SOURCEBLOCK
	else if AIndex = IM_IFHDR_STARTDATE then
		Result := fStartDate
	else if AIndex = IM_IFHDR_NUMDATA then
		Result := GetNumSeries
	else if AIndex = IM_IFHDR_ISALPHA then
		Result := True
	else if AIndex = IM_IFHDR_AREA then
		Result := 0.1
	else if AIndex = IM_IFHDR_MULTIPLIER then
		Result := 1
	else
		Result := Unassigned;
end;

destructor T_MOUSE_PRF_StandardInterfaceFileHeader.Destroy;
begin
	fInterfaceFile := nil;
	inherited;
end;

function T_MOUSE_PRF_StandardInterfaceFileHeader.GetNumSeries: Integer;
begin
	Result := Length(fSeries);
end;

function T_MOUSE_PRF_StandardInterfaceFileHeader.GetSeries(
	AIndex: Integer): T_MOUSE_PRF_Series;
begin
	Result := fSeries[AIndex];
end;

function T_MOUSE_PRF_StandardInterfaceFileHeader.GetStartDate: TDateTime;
begin
	Result := fStartDate;
end;

procedure T_MOUSE_PRF_StandardInterfaceFileHeader.SetSeries(AIndex: Integer;
	Value: T_MOUSE_PRF_Series);
begin
	fSeries[AIndex] := Value;
end;

procedure T_MOUSE_PRF_StandardInterfaceFileHeader.SetStartDate(
	Value: TDateTime);
begin
	if not SameDate(fStartDate, Value) then
		fStartDate := Value;
end;

function T_MOUSE_PRF_StandardInterfaceFileHeader.GetIndexedHeaderValue(
	AIndex: Integer): Variant;
begin
	Result := fSeries[AIndex].ID;
end;

function T_MOUSE_PRF_StandardInterfaceFileHeader.NumIndexedHeaderValues: Integer;
begin
	Result := Length(fSeries);
end;

procedure T_MOUSE_PRF_StandardInterfaceFileHeader.SetIndexedHeaderValue(
  AIndex: Integer; Value: Variant);
begin
  fSeries[AIndex].ID := Value;
end;

procedure T_MOUSE_PRF_StandardInterfaceFileHeader.ClearIndexedHeaderValues;
begin

end;

function T_MOUSE_PRF_StandardInterfaceFileHeader.AddIndexedHeaderValue(
  Value: Variant): Integer;
begin

end;

{ T_MOUSE_PRF_InterfaceFileIO }

function T_MOUSE_PRF_InterfaceFileIO.ReadByte: Byte;
begin
	CheckCurrentLineTokens;
	Result := Byte(StrToInt(fTokens[fCurrentTokenIndex]));
	Inc(fCurrentTokenIndex);
end;

function T_MOUSE_PRF_InterfaceFileIO.IsBeginningOfData: Boolean;
begin
	Result := Position < IIM_InterfaceFile(fInterfaceFile).GetHeader.Size;
end;

procedure T_MOUSE_PRF_InterfaceFileIO.WriteInteger(Value: Integer);
begin
	Assert(False, 'T_MOUSE_PRF_InterfaceFileIO.WriteInteger not implemented');
end;

constructor T_MOUSE_PRF_InterfaceFileIO.Create(
	AInterfaceFile: IIM_InterfaceFile; AStream: TStAnsiTextStream);
begin
	fInterfaceFile := Pointer(AInterfaceFile);
	fStream := AStream;
	fTokens := TStringList.Create;
end;

procedure T_MOUSE_PRF_InterfaceFileIO.WriteByte(Value: Byte);
begin
	Assert(False, 'T_MOUSE_PRF_InterfaceFileIO.WriteByte not implemented');
end;

function T_MOUSE_PRF_InterfaceFileIO.ReadDouble: Double;
begin
	CheckCurrentLineTokens;
	Result := StrToFloat(fTokens[fCurrentTokenIndex]);
	Inc(fCurrentTokenIndex);
end;

procedure T_MOUSE_PRF_InterfaceFileIO.WriteDouble(Value: Double);
begin
	Assert(False, 'T_MOUSE_PRF_InterfaceFileIO.WriteDouble not implemented');
end;

procedure T_MOUSE_PRF_InterfaceFileIO.PreviousLine;
var
	CurrentLine: Integer;
	CurrentPos: Int64;
begin
	CurrentLine := fStream.SeekNearestLine(fStream.Position);
	if not IsBeginningOfData then
	begin
		fStream.SeekLine(CurrentLine-1);
		fCurrentTokenIndex := fTokens.Count;
		CheckCurrentLineTokens;
	end;
end;

procedure T_MOUSE_PRF_InterfaceFileIO.MoveToBeginning;
begin
	fStream.Seek(0, soFromBeginning);
	NextLine;
end;

function T_MOUSE_PRF_InterfaceFileIO.ReadExtended: Extended;
begin
	CheckCurrentLineTokens;
	Result := StrToFloat(fTokens[fCurrentTokenIndex]);
	Inc(fCurrentTokenIndex);
end;

procedure T_MOUSE_PRF_InterfaceFileIO.MoveToEnd;
begin
	MoveTo(0, soEnd);
end;

function T_MOUSE_PRF_InterfaceFileIO.MoveTo(Offset: Int64;
	Origin: TSeekOrigin): Int64;
begin
	fStream.Seek(Offset, Origin);
end;

procedure T_MOUSE_PRF_InterfaceFileIO.WriteExtended(Value: Extended);
begin
	Assert(False, 'T_MOUSE_PRF_InterfaceFileIO.WriteExtended not implemented');
end;

procedure T_MOUSE_PRF_InterfaceFileIO.NextLine;
begin
	if not IsEOF then
	begin
		fCurrentTokenIndex := fTokens.Count;
		CheckCurrentLineTokens;
	end;
end;

function T_MOUSE_PRF_InterfaceFileIO.IsEOF: Boolean;
begin
	Result := (fStream.Position >= fStream.FastSize-1) and
		(fCurrentTokenIndex = fTokens.Count);
end;

function T_MOUSE_PRF_InterfaceFileIO.ReadSingle: Single;
begin
	CheckCurrentLineTokens;
	Result := StrToFloat(fTokens[fCurrentTokenIndex]);
	Inc(fCurrentTokenIndex);
end;

function T_MOUSE_PRF_InterfaceFileIO.ReadString: String;
begin
	CheckCurrentLineTokens;
	Result := fTokens[fCurrentTokenIndex];
	Inc(fCurrentTokenIndex);
end;

function T_MOUSE_PRF_InterfaceFileIO.ReadString(ALength: Integer): String;
begin
	CheckCurrentLineTokens;
	Result := fTokens[fCurrentTokenIndex];
	Inc(fCurrentTokenIndex);
end;

procedure T_MOUSE_PRF_InterfaceFileIO.WriteSingle(Value: Single);
begin
	Assert(False, 'T_MOUSE_PRF_InterfaceFileIO.WriteSingle not implemented');
end;

function T_MOUSE_PRF_InterfaceFileIO.Position: Int64;
begin
	Result := fStream.Position;
end;

procedure T_MOUSE_PRF_InterfaceFileIO.WriteString(Value: String);
begin
	Assert(False, 'T_MOUSE_PRF_InterfaceFileIO.WriteString not implemented');
end;

procedure T_MOUSE_PRF_InterfaceFileIO.WriteString(Value: String;
	ALength: Integer);
begin
	Assert(False, 'T_MOUSE_PRF_InterfaceFileIO.WriteString() not implemented');
end;

function T_MOUSE_PRF_InterfaceFileIO.ReadInteger: Integer;
begin
	CheckCurrentLineTokens;
	Result := StrToInt(fTokens[fCurrentTokenIndex]);
	Inc(fCurrentTokenIndex);
end;

destructor T_MOUSE_PRF_InterfaceFileIO.Destroy;
begin
	fInterfaceFile := nil;
	fStream := nil;
	fTokens.Free;
	inherited;
end;

procedure T_MOUSE_PRF_InterfaceFileIO.CheckCurrentLineTokens;
begin
	if fCurrentTokenIndex = fTokens.Count then
	begin
		fCurrentLine := fStream.ReadLine;
		ExtractTokensL(fCurrentLine, ',: ', '''', False, fTokens);
		fCurrentTokenIndex := 0;
	end;
end;

procedure T_MOUSE_PRF_InterfaceFileIO.MoveToBeginningOfData;
begin
	fStream.Seek(IIM_InterfaceFile(fInterfaceFile).GetHeader.Size, soBeginning);
end;

function T_MOUSE_PRF_InterfaceFileIO.Size: Int64;
begin
	Result := fStream.FastSize;
end;

{ T_MOUSE_PRF_StandardInterfaceFile_DataIterator }

function T_MOUSE_PRF_StandardInterfaceFile_DataIterator.HasNext: Boolean;
var
	IO: IIM_InterfaceFileIO;
begin
	IO := IIM_InterfaceFile(fInterfaceFile).GetIOServices;
	Result := not IO.IsEOF;
end;

constructor T_MOUSE_PRF_StandardInterfaceFile_DataIterator.Create(
	AInterfaceFile: IIM_InterfaceFile);
begin
	fInterfaceFile := Pointer(AInterfaceFile);
end;

function T_MOUSE_PRF_StandardInterfaceFile_DataIterator.PreviousTime: TDateTime;
begin
	Result := fPreviousTime;
end;

function T_MOUSE_PRF_StandardInterfaceFile_DataIterator.CurrentTimeStep: Double;
begin
	if SameDateTime(fNextTime, MaxDateTime) then
		Result := fCurrentTimeStep
	else
	begin
		Result := fNextTime - fCurrentTime;
		fCurrentTimeStep := Result;
	end;
end;

function T_MOUSE_PRF_StandardInterfaceFile_DataIterator.NextTime: TDateTime;
begin
	Result := fNextTime;
end;

function T_MOUSE_PRF_StandardInterfaceFile_DataIterator.Current: IIM_InterfaceTimeSeriesData;
begin
	Result := IIM_InterfaceTimeSeriesData(fData);
end;

function T_MOUSE_PRF_StandardInterfaceFile_DataIterator.CurrentTime: TDateTime;
begin
	Result := fCurrentTime;
end;

function T_MOUSE_PRF_StandardInterfaceFile_DataIterator.Previous: IIM_InterfaceTimeSeriesData;
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
		IO.PreviousLine;
		IO.PreviousLine;
		if IO.IsBeginningOfData then
		begin
			fPreviousTime := MinDateTime;
			fCurrentTimeStep := 0;
		end
		else
		begin
			SecondPreviousData := T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData.Create(
				IIM_InterfaceFile(fInterfaceFile), Self);
			fPreviousTime := SecondPreviousData.Time;
		end;
		Result := T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData.Create(
			IIM_InterfaceFile(fInterfaceFile), Self);
		fData := Pointer(Result);
		fCurrentTime := Result.Time;
		fCurrentTimeStep := SecondSpan(fNextTime, fCurrentTime);
		Result.DataValue[IM_IF_TIMESTEP] := fCurrentTimeStep;
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

function T_MOUSE_PRF_StandardInterfaceFile_DataIterator.HasPrevious: Boolean;
begin
	Result := not IIM_InterfaceFile(fInterfaceFile).GetIOServices.IsBeginningOfData;
end;

destructor T_MOUSE_PRF_StandardInterfaceFile_DataIterator.Destroy;
begin
	fData := nil;
	fInterfaceFile := nil;
	inherited;
end;

function T_MOUSE_PRF_StandardInterfaceFile_DataIterator.Next: IIM_InterfaceTimeSeriesData;
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
		Result := T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData.Create(
			IIM_InterfaceFile(fInterfaceFile), Self);
		fData := Pointer(Result);
		fCurrentTime := Result.Time;
		if IO.IsEOF then
		begin
			fNextTime := MaxDateTime;
			Result.DataValue[IM_IF_TIMESTEP] := fCurrentTimeStep;
		end
		else
		begin
			SecondNextData := T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData.Create(
				IIM_InterfaceFile(fInterfaceFile), Self);
			fNextTime := SecondNextData.Time;
			fCurrentTimeStep := SecondSpan(fNextTime, fCurrentTime);
//			fCurrentTimeStep := 900;
//			fNextTime := IncSecond(fCurrentTime, Round(fCurrentTimeStep));
			Result.DataValue[IM_IF_TIMESTEP] := fCurrentTimeStep;
			IIM_InterfaceFile(fInterfaceFile).GetIOServices.PreviousLine;
		end;
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

function T_MOUSE_PRF_StandardInterfaceFile_DataIterator.PositionPercent: Integer;
var
	IFPos: Int64;
	IFSize: Int64;
	IFPercentPos: Double;
	IO: IIM_InterfaceFileIO;
begin
	IO := IIM_InterfaceFile(fInterfaceFile).GetIOServices;
	IFPos := IO.Position;
	IFSize := IO.Size;
	IFPercentPos := IFPos/IFSize*100;
	Result := Round(IFPercentPos);
end;

function T_MOUSE_PRF_StandardInterfaceFile_DataIterator.Position: Int64;
begin
	Result := IIM_InterfaceFile(fInterfaceFile).GetIOServices.Position;
end;

function T_MOUSE_PRF_StandardInterfaceFile_DataIterator.DataValidUntilTime: TDateTime;
begin
	Result := IncSecond(fCurrentTime, Round(fCurrentTimeStep));
	if CompareDateTime(Result, MaxDateTime) = GreaterThanValue then
		Result := MaxDateTime;
end;

{ T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData }

constructor T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData.Create(
	AInterfaceFile: IIM_InterfaceFile;
	AIterator: IIM_InterfaceTimeSeriesDataIterator);
var
	IO: IIM_InterfaceFileIO;
begin
	fInterfaceFile := Pointer(AInterfaceFile);
	fIterator := Pointer(AIterator);
	IO := IIM_InterfaceFile(fInterfaceFile).GetIOServices;
	DateTimeStr := IO.ReadString + ' ' + IO.ReadString + ':'+ IO.ReadString + ':' +
		IO.ReadString;
	fTime := StrToDateTime(DateTimeStr, DateTimeFormatSettings);
	fTimeStep := 0;
	ReadData;
end;

procedure T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData.SetIndexedDataValue(
	AIndex: Integer; Value: Variant);
begin
	if fIndexedData[AIndex] <> Value then
		fIndexedData[AIndex] := Value;
end;

function T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData.GetDataValue(
	AIndex: String): Variant;
begin
	if AIndex = IM_IF_TIMESTEP then
		Result := fTimeStep;
end;

function T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData.Time: TDateTime;
begin
	Result := fTime;
end;

function T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData.GetIndexedDataValue(
	AIndex: Integer): Variant;
begin
	Result := fIndexedData[AIndex];
end;

procedure T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData.SetDataValue(
	AIndex: String; Value: Variant);
begin
	if AIndex = IM_IF_TIMESTEP then
		if not SameValue(fTimeStep, Value) then
			fTimeStep := Value;
end;

function T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData.Size: Int64;
begin
	{ TODO -oAMM : Determine how to calculate size of data }
	Result := SizeOf(Integer) + SizeOf(Double) + SizeOf(Double) +
		NumIndexedDataValues * SizeOf(Double);
end;

procedure T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData.ReadData;
var
	i: Integer;
	Header: IIM_InterfaceHeader;
	IO: IIM_InterfaceFileIO;
	NumDataPoints: Integer;
begin
	Header := IIM_InterfaceFile(fInterfaceFile).GetHeader;
	IO := IIM_InterfaceFile(fInterfaceFile).GetIOServices;
	NumDataPoints := Header.NumIndexedHeaderValues;
	SetLength(fIndexedData, NumDataPoints);
	for i := 0 to NumDataPoints-1 do
	begin
		fIndexedData[i] := Convert(IO.ReadDouble, vuCubicMeters, vuCubicFeet);
	end;
end;

destructor T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData.Destroy;
begin
	fInterfaceFile := nil;
	fIterator := nil;
	inherited;
end;

procedure T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData.WriteData;
begin
	Assert(False, 'T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData.WriteData not implemented');
end;

function T_MOUSE_PRF_StandardInterfaceFile_TimeSeriesData.NumIndexedDataValues: Integer;
begin
	Result := Length(fIndexedData);
end;

initialization
	GetLocaleFormatSettings(LOCALE_SYSTEM_DEFAULT, DateTimeFormatSettings);
	DateTimeFormatSettings.DateSeparator := '-';
	DateTimeFormatSettings.TimeSeparator := ':';
	DateTimeFormatSettings.ShortDateFormat := 'YYYY-MM-DD';
	DateTimeFormatSettings.ShortTimeFormat := 'HH:MM:SS';

end.
