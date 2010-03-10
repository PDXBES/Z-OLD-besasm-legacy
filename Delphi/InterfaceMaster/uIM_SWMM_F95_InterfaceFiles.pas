unit uIM_SWMM_F95_InterfaceFiles;

interface

uses SysUtils, Classes, uIM_InterfaceFiles, StStrms, StStrL;

type

	T_SWMM_F95_StandardInterfaceFileHeader = class;
	{=============================================================================
		 Name: T_SWMM_F95_StandardInterfaceFile
		 Purpose: Provides services for reading, writing, and manipulating standard
			 swmm interface files in Fortran 95/SWMM 4.4+ format
		 Requirements: -
	=============================================================================}
	T_SWMM_F95_StandardInterfaceFile = class(TInterfacedObject,
		IIM_InterfaceHeader,
		IIM_InterfaceFile)
	private
		fFileName: TFileName;
		fStream: TFileStream;
		fBuffer: TStBufferedStream;
		fHeader: T_SWMM_F95_StandardInterfaceFileHeader;
	protected
	//-IIM_InterfaceHeader implementation-----------------------------------------
		function GetHeaderValue(AIndex: String): Variant;
		procedure SetHeaderValue(AIndex: String; AValue: Variant);
	public
	//-Object management----------------------------------------------------------
		constructor Create(AFile: TFileName; Mode: Word = fmOpenRead); virtual;
		destructor Destroy; override;
	//-IIM_InterfaceHeader implementation-----------------------------------------
		procedure ReadHeader;
		procedure WriteHeader;
		property HeaderValue[AIndex: String]: Variant read GetHeaderValue
			write SetHeaderValue;
	//-IIM_InterfaceFile implementation-------------------------------------------
		function GetTimeSeriesDataIterator: IIM_InterfaceTimeSeriesDataIterator;
		function GetIOServices: IIM_InterfaceFileIO;
		procedure Convert(ASourceInterface: IIM_InterfaceFile);
	end;

	{=============================================================================
		 Name: T_SWMM_F95_StandardInterfaceFile_TimeSeriesDataIterator
		 Purpose: Provides a way to traverse back and forth an F95 standard interface
			 file
		 Requirements: a T_SWMM_F95_StandardInterfaceFile
		 Restrictions: usage restricted to TSWMM_F95_StandardInterfaceFile
	=============================================================================}
	T_SWMM_F95_StandardInterfaceFile_TimeSeriesDataIterator = class(TInterfacedObject,
		IIM_InterfaceTimeSeriesDataIterator)
	private
	//-Object management----------------------------------------------------------
		constructor Create(AStream: TStBufferedStream); virtual;
		destructor Destroy; override;
	public
	//-IIM_InterfaceTimeSeriesDataIterator implementation-----------------------------------
		function HasNext: Boolean;
		function Next: IIM_InterfaceTimeSeriesData;
		function Previous: IIM_InterfaceTimeSeriesData;
		function CurrentTime: TDateTime;
		function NextTime: TDateTime;
		function PreviousTime: TDateTime;
		function CurrentTimeStep: Double;
	end;

	{=============================================================================
		 Name: T_SWMM_F95_StandardInterfaceFile_TimeSeriesData
		 Purpose: Provides access to standard interface file data
		 Requirements: a T_SWMM_F95_StandardInterfaceFile_DataIterator
	=============================================================================}
	T_SWMM_F95_StandardInterfaceFile_TimeSeriesData = class(TInterfacedObject,
		IIM_InterfaceTimeSeriesData)
	public
	//-Object management----------------------------------------------------------
		constructor Create; virtual;
		destructor Destroy; override;
	//-IIM_InterfaceTimeSeriesData implementation---------------------------------
		procedure ReadData;
		procedure WriteData;
		function GetDataValue(AIndex: String): Variant;
		procedure SetDataValue(AIndex: String; AValue: Variant);
		property DataValue[AIndex: String]: Variant read GetDataValue write SetDataValue;
		function GetIndexedDataValue(AIndex: Integer): Variant;
		procedure SetIndexedDataValue(AIndex: Integer; AValue: Variant);
		property IndexedDataValue[AIndex: Integer]: Variant read GetIndexedDataValue
			write SetIndexedDataValue;
	end;

	{=============================================================================
		 Name: T_SWMM_F95_StandardInterfaceFileHeader
		 Purpose:
		 Requirements:
		 Restrictions: usage restricted to TSWMM_F95_StandardInterfaceFile
	=============================================================================}
	T_SWMM_F95_StandardInterfaceFileHeader = class(TInterfacedObject)
	private
		fTitles: array[1..4] of String;
		fStartDate: TDateTime;
		fNumFlows: Integer;
		fFlowIDs: array of String;
		fNumPollutants: Integer;
		fArea: Double;
		fFlowMultiplier: Double;
	protected
		function GetTitles(AIndex: Integer): String;
		procedure SetTitles(AIndex: Integer; Value: String);
		function GetStartDate: TDateTime;
		procedure SetStartDate(Value: TDateTime);
		function GetFlowID(AIndex: Integer): String;
		procedure SetFlowID(AIndex: Integer; Value: String);
		function GetArea: Double;
		procedure SetArea(Value: Double);
		function GetFlowMultiplier: Double;
		procedure SetFlowMultiplier(Value: Double);
	private
	//-Object management----------------------------------------------------------
		constructor Create(AStream: TStBufferedStream); virtual;
		destructor Destroy; override;
	public
	//-Object properties----------------------------------------------------------
		property Titles: array of String read GetTitles write SetTitles;
		property StartDate: TDateTime read GetStartDate write SetStartDate;
		property FlowIDs: array of String read GetFlowID write SetFlowID;
		property Area: Double read GetArea write SetArea;
		property FlowMultiplier: Double read GetFlowMultiplier write SetFlowMultiplier;
	end;

	{=============================================================================
		 Name: T_SWMM_F95_InterfaceFileIO
		 Purpose:	Provides IO services for XP interface file
		 Requirements: usage restricted to T_SWMM_XP_StandardInterfaceFile
	=============================================================================}
	T_SWMM_F95_InterfaceFileIO = class(TInterfacedObject,
		IIM_InterfaceFileIO)
	private
		fStream: TStBufferedStream;
	public
	//-Object management----------------------------------------------------------
		constructor Create(AStream: TStBufferedStream); virtual;
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
		function IsEOF: Boolean;
	end;

implementation

end.
