unit uIM_InterfaceFiles;

interface

uses SysUtils, Classes, Math;

const
	IM_EPSILON: Double = 0.00001;
	IM_IF_TIME: String = 'Time';
	IM_IF_TIMESTEP: String = 'TimeStep';
	IM_IFHDR_TITLE1: String = 'Title1';
	IM_IFHDR_TITLE2: String = 'Title2';
	IM_IFHDR_TITLE3: String = 'Title3';
	IM_IFHDR_TITLE4: String = 'Title4';
	IM_IFHDR_SOURCE: String = 'SourceBlock';
	IM_IFHDR_STARTDATE: String = 'StartDate';
	IM_IFHDR_NUMDATA: String = 'NumData';
	IM_IFHDR_ISALPHA: String = 'IsAlpha';
	IM_IFHDR_AREA: String = 'Area';
	IM_IFHDR_MULTIPLIER: String = 'Multiplier';

type
	EIMSetHeaderValue = class(Exception)
	end;

	{=============================================================================
		 Name:  IIM_InterfaceHeader
		 Purpose: Services for accessing an interface file's header
		 Requirements: an IIM_InterfaceFile connection
	=============================================================================}
	IIM_InterfaceHeader = interface
		['{D6B67A0D-84BB-4FCA-97D7-CCFC8BD684ED}']
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
	//----------------------------------------------------------------------------
		property IndexedHeaderValue[AIndex: Integer]: Variant read GetIndexedHeaderValue
			write SetIndexedHeaderValue;
	end;

	{=============================================================================
		 Name: IIM_InterfaceTimeSeriesData
		 Purpose: Services for accessing a single element of data
		 Requirements: an IIM_InterfaceFile connection
	=============================================================================}
	IIM_InterfaceTimeSeriesData = interface
		['{BA865961-0E96-4446-9D4C-7EC1BD6372C0}']
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
		 Name: IIM_InterfaceTimeSeriesDataIterator
		 Purpose: Services for traversing an interface file's interface data
		 Requirements: an IIM_InterfaceFile connection
	=============================================================================}
	IIM_InterfaceTimeSeriesDataIterator = interface
		['{8C91BAC7-6205-495A-B08E-27F80B722CBF}']
		function HasNext: Boolean;
		function Next: IIM_InterfaceTimeSeriesData;
		function HasPrevious: Boolean;
		function Previous: IIM_InterfaceTimeSeriesData;
		function Current: IIM_InterfaceTimeSeriesData;
		function CurrentTime: TDateTime;
		function NextTime: TDateTime;
		function PreviousTime: TDateTime;
		function CurrentTimeStep: Double;
		function DataValidUntilTime: TDateTime;
		function Position: Int64;
		function PositionPercent: Integer;
	end;

	{=============================================================================
		 Name:  IIM_InterfaceFileIO
		 Purpose: Services for reading/writing data to an interface file
		 Requirements: an IIM-InterfaceFile connection
	=============================================================================}
	IIM_InterfaceFileIO = interface
		['{5BA1484B-85C3-47C9-B047-71D6AC7522F0}']
	//-Read/write services--------------------------------------------------------
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
	//-File transport services----------------------------------------------------
		function MoveTo(Offset: Int64; Origin: TSeekOrigin): Int64;
		procedure MoveToBeginning;
		procedure MoveToEnd;
		procedure MoveToBeginningOfData;
		function IsBeginningOfData: Boolean;
		function IsEOF: Boolean;
		function Position: Int64;
		function Size: Int64;
	//-Record services------------------------------------------------------------
		procedure NextLine;
		procedure PreviousLine;
	end;

	{=============================================================================
		 Name: IIM_FortranInterfaceFileIO
		 Purpose: Services for reading/writing data to an interface file, Fortran-style
		 Requirements:
	=============================================================================}
	IIM_FortranInterfaceFileIO = interface
		['{F7DBBD77-CDDA-4E05-99B6-E2647B68955D}']
	end;

	{=============================================================================
		 Name: IIM_InterfaceFile
		 Purpose: Services for high-level interface file manipulation
		 Requirements: -
	=============================================================================}
	IIM_InterfaceFile = interface
		['{FEDD780A-D91E-4A83-8492-E9D033845848}']
		function GetTimeSeriesDataIterator: IIM_InterfaceTimeSeriesDataIterator;
		function GetIOServices: IIM_InterfaceFileIO;
		function GetHeader: IIM_InterfaceHeader;
		function GetFileName: TFileName;
		function GetFormat: String;
		procedure ReadInterface;
		procedure WriteInterface;
		property FileName: TFileName read GetFileName;
		property Format: String read GetFormat;
	end;

	{=============================================================================
		Name: TGenericTimeSeriesData
		Purpose:
		Requirements:
	=============================================================================}
	TGenericTimeSeriesData = class(TInterfacedObject,
		IIM_InterfaceTimeSeriesData)
	private
		fTime: TDateTime;
		fTimeStep: Double;
		fData: array of Double;
	protected
		function GetDataValue(AIndex: String): Variant;
		procedure SetDataValue(AIndex: String; Value: Variant);
		function GetIndexedDataValue(AIndex: Integer): Variant;
		procedure SetIndexedDataValue(AIndex: Integer; Value: Variant);
	public
		procedure ReadData;
		procedure WriteData;
		function Size: Int64;
		function NumIndexedDataValues: Integer;
		function Time: TDateTime;
		function AddIndexedDataValue(Value: Variant): Integer;
		procedure ClearIndexedDataValues;
	//----------------------------------------------------------------------------
		property DataValue[AIndex: String]: Variant read GetDataValue write SetDataValue;
		property IndexedDataValue[AIndex: Integer]: Variant read GetIndexedDataValue
			write SetIndexedDataValue;
	end;

	function IMDetectInterfaceFormat(AFileName: TFileName): IIM_InterfaceFile;
	function IMInterfaceRef(AFileName: TFileName; AFormat: String): IIM_InterfaceFile;

const
	IMIF_SWMM_F95: String = 'SWMM.F95';
	IMIF_SWMM_XP: String = 'SWMM.XP';
	IMIF_SWMM_F32: String = 'SWMM.F32';
	IMIF_SWMM_TEXT: String = 'SWMM.TEXT';
	IMIF_MOUSE_PRF: String = 'DHI.PRF';
	IMIF_MOUSE_M11: String = 'DHI.M11';

implementation

uses uIM_SWMM_XP_InterfaceFiles;

function IMDetectInterfaceFormat(AFileName: TFileName): IIM_InterfaceFile;
begin

end;

function IMInterfaceRef(AFileName: TFileName; AFormat: String): IIM_InterfaceFile;
begin
end;

{ TGenericTimeSeriesData }

procedure TGenericTimeSeriesData.SetIndexedDataValue(AIndex: Integer;
	Value: Variant);
begin
	if not SameValue(fData[AIndex], Value, IM_EPSILON) then
		fData[AIndex] := Value;
end;

function TGenericTimeSeriesData.GetDataValue(AIndex: String): Variant;
begin
	if AIndex = IM_IF_TIMESTEP then
		Result := fTimeStep;
end;

function TGenericTimeSeriesData.Time: TDateTime;
begin
	Result := fTime;
end;

function TGenericTimeSeriesData.Size: Int64;
begin
	// Time + TimeStep + DataVals
	Result := SizeOf(TDateTime) + SizeOf(Double) + NumIndexedDataValues * SizeOf(Double);
end;

function TGenericTimeSeriesData.GetIndexedDataValue(AIndex: Integer): Variant;
begin
	Result := fData[AIndex];
end;

procedure TGenericTimeSeriesData.SetDataValue(AIndex: String; Value: Variant);
begin
	if AIndex = IM_IF_TIMESTEP then
		if not SameValue(fTimeStep, Value, IM_EPSILON) then
			fTimeStep := Value;
end;

procedure TGenericTimeSeriesData.ReadData;
begin
	// Do nothing; only valid if attached to a data holder
end;

procedure TGenericTimeSeriesData.WriteData;
begin
	// Do nothing; only valid if attached to a data holder
end;

function TGenericTimeSeriesData.NumIndexedDataValues: Integer;
begin
	Result := Length(fData);
end;

function TGenericTimeSeriesData.AddIndexedDataValue(Value: Variant): Integer;
begin
	SetLength(fData, Length(fData)+1);
	fData[Length(fData)-1] := Value;
end;

procedure TGenericTimeSeriesData.ClearIndexedDataValues;
begin
	SetLength(fData, 0);
	Finalize(fData);
end;

end.
