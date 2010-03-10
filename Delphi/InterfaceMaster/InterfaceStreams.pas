unit InterfaceStreams;

{$WARNINGS OFF}

interface

uses StStrms, StStrL, Classes, SysUtils, Dialogs, DateUtils, StrUtils, Types,
	CodeSiteLogging, StdConvs, ConvUtils;

const
	SYFRecSize = 4;

type

	TInterfaceFormat = (if32, if95, ifXP, ifXPSYF, ifText, ifMOUSEPRF);
  TSWMMInterfaceFormat = if32..ifXP;
	TFlowsArray = array of Double;

{
	IInterfaceFileStream
	Services for handling binary files
}
	IInterfaceFileStream = interface
		['{C3DAD331-E297-11D5-8895-00B0D0920301}']
		function ReadString(ASize: Integer): String;
		function ReadInteger: Integer;
		function ReadExtended: Extended;
		function ReadDouble: Double;
		function ReadSingle: Single;
		function ReadByte: Byte;
		procedure WriteString(AString: String);
		procedure WriteInteger(AInteger: Integer);
		procedure WriteExtended(AExtended: Extended);
		procedure WriteDouble(ADouble: Double);
		procedure WriteSingle(ASingle: Single);
		procedure WriteByte(AByte: Byte);
		function IsEOF: Boolean;
	end;

{
	IFortranInterfaceFileStream
	Services for transparently handling unformatted binary 32-bit Fortran files
}
	IFortranInterfaceFileStream = interface
		['{713578B1-E427-11D5-8895-00B0D0920301}']
		function ReadRecordSize: Integer;
		procedure WriteRecordSize(ASize: Integer);
		procedure FlushRecord;
		function IsEndOfRecord: Boolean;
		procedure CheckRecordSize;
		procedure CheckSignature;
		procedure Rewind;
		function GetRecordSize: Integer;
		function GetInterfaceFormat: TInterfaceFormat;
		property CurrentRecordSize: Integer read GetRecordSize;
	end;

{
	ISWMMStandardInterfaceFileStream
	Services for accessing standard SWMM interface files
}
	TCustomFortranInterfaceFileStream = class;
	ISWMMStandardInterfaceFile = interface(IInterfaceFileStream)
		['{713578B2-E427-11D5-8895-00B0D0920301}']
		function GetTitles(Index: Integer): String;
		function GetStart: TDateTime;
		function GetSourceBlock: String;
		function GetNumInlets: Integer;
		function GetNumPollutants: Integer;
		function GetArea: Double;
		function GetUsesAlphaNumericIDs: Boolean;
		function GetAlphaIDSize: Integer;
		function GetIDs(Index: Integer): String;
		function GetToIDs(Index: Integer): String;
		function GetIDsList: TStringList;
		function GetFlowMultiplier: Double;
		function GetCurrentTime: TDateTime;
		function GetCurrentTimeStep: Double;
		function GetFlows(Index: Integer): Double;
		function GetFlowUnits(Index: Integer): TConvType;
		function GetStream: TStream;
		function GetInterfaceFormat: TInterfaceFormat;
		procedure SetTitles(Index: Integer; Value: String);
		procedure SetStart(Value: TDateTime);
		procedure SetSourceBlock(Value: String);
		procedure SetNumInlets(Value: Integer);
		procedure SetNumPollutants(Value: Integer);
		procedure SetArea(Value: Double);
		procedure SetIDs(Index: Integer; Value: String);
		procedure SetFlowMultiplier(Value: Double);
		procedure SetAlphaIDSize(Value: Integer);
		procedure SetUsesAlphaNumericIDs(Value: Boolean);
		procedure Restart;
		procedure ReadHeader;
		procedure ReadFlows;

		procedure WriteHeader; overload;
		procedure WriteHeaderLimitNodes(NodesList: TStringList); overload;
		procedure WriteHeader(AInterfaceFile: ISWMMStandardInterfaceFile); overload;
		procedure WriteHeaderLimitNodes(AInterfaceFile: ISWMMStandardInterfaceFile;
			NodesList: TStringList); overload;
		procedure WriteFlows(SkipInterval: Integer = 0); overload;
		procedure WriteFlowsLimitNodes(NodesList: TStringList; SkipInterval: Integer = 0); overload;
		procedure WriteFlowsLimitNodesIndexList(NodesList: TStringList; SkipInterval: Integer = 0); overload;
		procedure WriteFlows(AInterfaceFile: ISWMMStandardInterfaceFile; SkipInterval: Integer = 0); overload;
		procedure WriteFlowsLimitNodes(AInterfaceFile: ISWMMStandardInterfaceFile;
			NodesList: TStringList; SkipInterval: Integer = 0); overload;
		procedure WriteFlowsLimitNodesIndexList(AInterfaceFile: ISWMMStandardInterfaceFile;
			NodesList: TStringList; SkipInterval: Integer = 0); overload;

		function IsEOF: Boolean;
		procedure FlushRecord;

		property Titles[Index: Integer]: String read GetTitles write SetTitles;
		property Start: TDateTime read GetStart write SetStart;
		property SourceBlock: String read GetSourceBlock write SetSourceBlock;
		property NumInlets: Integer read GetNumInlets write SetNumInlets;
		property NumPollutants: Integer read GetNumPollutants write SetNumPollutants;
		property Area: Double read GetArea write SetArea;
		property UsesAlphaNumericIDs: Boolean read GetUsesAlphaNumericIDs
			write SetUsesAlphaNumericIDs;
		property AlphaIDSize: Integer read GetAlphaIDSize write SetAlphaIDSize;
		property IDs[Index: Integer]: String read GetIDs write SetIDs;
		property ToIds[Index: Integer]: String read GetToIDs;
		property IDsList: TStringList read GetIDsList;
		property FlowMultiplier: Double read GetFlowMultiplier write SetFlowMultiplier;
		property CurrentTime: TDateTime read GetCurrentTime;
		property CurrentTimeStep: Double read GetCurrentTimeStep;
		property Flows[Index: Integer]: Double read GetFlows;
		property FlowUnits[Index: Integer]: TConvType read GetFlowUnits;
		property Stream: TStream read GetStream;
		property InterfaceFormat: TInterfaceFormat read GetInterfaceFormat;
	end;

	TSYFLink = record
		ID: String;
		USNode: String;
		USNodeIndex: Integer;
		DSNode: String;
		DSNodeIndex: Integer;
		InitFlow: Single;
		InitVelocity: Single;
		InitXSecArea: Single;
		InitDepth: Single;
		InitUSDepth: Single;
		InitDSDepth: Single;
		// Intermediate results
		DSDepth: Single;
		Flow: Single;
		USDepth: Single;
		Velocity: Single;
	end;

	TSYFNode = record
		ID: String;
		GroundElev: Single;
		CrownElev: Single;
		InitElev: Single;
		InvertElev: Single;
		// Intermediate results
		Depth: Single;
		Invert: Single;
	end;

{
	IXP_SYF_FileStream
	Services for handling XP SYF files
}
	IXP_SYF_FileStream = interface(IInterfaceFileStream)
		['{998FF8A1-58A5-11D7-890E-00B0D0920301}']
		function GetJCE: Integer;
		function GetIsMetric: Boolean;
		function GetNumCycles: Integer;
		function GetNumJunctions: Integer;
		function GetNumLinks: Integer;
		function GetNumConduits: Integer;
		function GetNumPumps: Integer;
		function GetNumOrifices: Integer;
		function GetNumWeirs: Integer;
		function GetTimeStep: Single;
		function GetNode(Index: Integer): TSYFNode;
		function GetLink(Index: Integer): TSYFLink;
		function GetCurrentTimeCycle: Integer;
		function GetCurrentTime: TDateTime;
		function GetSYFTimeStep: Single;
		function GetFileSize: Int64;
		function GetFilePosition: Int64;

		procedure ReadHeader;
		procedure ReadFlows;
		procedure MoveToStartOfFlows;

		property IsMetric: Boolean read GetIsMetric;
		property NumCycles: Integer read GetNumCycles;
		property NumJunctions: Integer read GetNumJunctions;
		property NumLinks: Integer read GetNumLinks;
		property NumConduits: Integer read GetNumConduits;
		property NumPumps: Integer read GetNumPumps;
		property NumOrifices: Integer read GetNumOrifices;
		property NumWeirs: Integer read GetNumWeirs;
		property TimeStep: Single read GetTimeStep;
		property Nodes[Index: Integer]: TSYFNode read GetNode;
		property Links[Index: Integer]: TSYFLink read GetLink;

		property CurrentTimeCycle: Integer read GetCurrentTimeCycle;
		property CurrentTime: TDateTime read GetCurrentTime;
		property SYFTimeStep: Single read GetSYFTimeStep;
		property FileSize: Int64 read GetFileSize;
		property FilePosition: Int64 read GetFilePosition;
	end;

	TPRFNodeType = (ntLevel, ntFlow, ntVolume, ntVelocity, ntTime, ntOther);
	TPRFNode = record
		ID: String;
		NodeType: TPRFNodeType;
		NodeTypeDescription: String;
		UpNodeID: String;
		DnNodeID: String;
		Position: Double;
		PositionUnits: String;
		Value: Double;
	end;

	IMOUSE_PRF_FileStream = interface(IInterfaceFileStream)
		['{DBFC5250-F036-44C3-A0F3-AC84F30F2DF6}']
		function GetNode(AIndex: Integer): TPRFNode;

		procedure ReadHeader;
		procedure ReadFlows;
		procedure MoveToStartOfFlows;

		property Nodes[Index: Integer]: TPRFNode read GetNode;
	end;

{
	IFlowStream
	Services for multicombine
}
	IFlowStream = interface
		['{362154B1-691A-11D7-8910-00B0D0920301}']
		function GetNumFlows: Integer;
		function GetFlow(AIndex: Integer): Double;
		function GetFileSize: Int64;
		function GetFilePosition: Int64;
		function GetCurrentTime: TDateTime;
		function GetCurrentEndTime: TDateTime;
		function GetNextTime: TDateTime;
		function GetInitialTime: TDateTime;
		function GetID(AIndex: Integer): String;
		function GetEOF: Boolean;

		procedure ReadFlows;
		procedure FlushFlows;
		procedure MoveToStartOfFlows;

		property NumFlows: Integer read GetNumFlows;
		property Flow[Index: Integer]: Double read GetFlow;
		property ID[Index: Integer]: String read GetID;
		property FileSize: Int64 read GetFileSize;
		property FilePosition: Int64 read GetFilePosition;
		property CurrentTime: TDateTime read GetCurrentTime;
		property CurrentEndTime: TDateTime read GetCurrentEndTime;
		property NextTime: TDateTime read GetNextTime;
		property InitialTime: TDateTime read GetInitialTime;
		property EOF: Boolean read GetEOF;
	end;

{
  IRainfallInterfaceFileStream
  Services for Rainfall Interface File Streams
}
  IRainfallInterfaceFileStream = interface(IInterfaceFileStream)
    ['{0850CD1E-C6A7-4A9B-A565-98B6B35982AD}']
    function GetNumGages: Integer;
    function GetCurrentTime: TDateTime;
    function GetInitialTime: TDateTime;
    function GetGage(AIndex: Integer): String;
    function GetRainfall(AIndex: Integer): Double;

    procedure MoveToStartOfRainfall;
    procedure ReadRain;

    property NumGages: Integer read GetNumGages;
    property CurrentTime: TDateTime read GetCurrentTime;
    property InitialTime: TDateTime read GetInitialTime;
    property GageID[Index: Integer]: String read GetGage;
    property Rainfall[Index: Integer]: Double read GetRainfall;
  end;

//------------------------------------------------------------------------------

	TCustomFortranInterfaceFileStream = class(TInterfacedObject, IInterfaceFileStream,
		IFortranInterfaceFileStream)
	private
		fFile: TFileStream;
		fStream: TStBufferedStream;
		fCurrentRecSize: Integer;
		fPosInRec: Integer;
		fCurrentWriteRecordStream: TMemoryStream;
		fCurrentWriteRecordSize: Integer;
		function GetInterfaceFormat: TInterfaceFormat; virtual; abstract;
	protected
		// IFortranInterfaceFileStream implementation
		function GetRecordSize: Integer; virtual;
	public
		constructor Create(AFile: String; Mode: Word = fmOpenRead);
		destructor Destroy; override;
		procedure GoToStartOfFile;
		// IFortranInterfaceFileStream implementation
		function ReadRecordSize: Integer; virtual; abstract;
		function IsEndOfRecord: Boolean; virtual;
		function IsStartOfRecord: Boolean; virtual;
		procedure CheckRecordSize; virtual;
		procedure FlushRecord; virtual; abstract;
		procedure Rewind; virtual;
		procedure WriteRecordSize(ASize: Integer); virtual; abstract;
		procedure CheckSignature; virtual;
		property CurrentRecordSize: Integer read GetRecordSize;
		// IInterfaceFileStream implementation
		function ReadString(ASize: Integer): String; virtual;
		function ReadDouble: Double; virtual;
		function ReadInteger: Integer; virtual;
		function ReadExtended: Extended; virtual;
		function ReadSingle: Single; virtual;
		function ReadByte: Byte; virtual;
		procedure WriteString(AString: String); virtual;
		procedure WriteInteger(AInteger: Integer); virtual;
		procedure WriteExtended(AExtended: Extended); virtual;
		procedure WriteDouble(ADouble: Double); virtual;
		procedure WriteSingle(ASingle: Single); virtual;
		procedure WriteByte(AByte: Byte); virtual;
		procedure WriteSignature; virtual;
		function IsEOF: Boolean; virtual;
		property Stream: TStBufferedStream read fStream;
		property InterfaceFormat: TInterfaceFormat read GetInterfaceFormat;
	end;

	TF95InterfaceFileStream = class(TCustomFortranInterfaceFileStream)
	private
		function GetInterfaceFormat: TInterfaceFormat; override;
	public
		function ReadRecordSize: Integer; override;
		procedure WriteRecordSize(ASize: Integer); override;
		constructor Create(AFile: String; Mode: Word = fmOpenRead);
		destructor Destroy; override;
		procedure WriteString(AString: String); override;
		procedure WriteInteger(AInteger: Integer); override;
		procedure WriteExtended(AExtended: Extended); override;
		procedure WriteDouble(ADouble: Double); override;
		procedure WriteSingle(ASingle: Single); override;
		procedure WriteByte(AByte: Byte); override;
		procedure FlushRecord; override;
	end;

	TF32InterfaceFileStream = class(TCustomFortranInterfaceFileStream)
	private
		fRecordSizeSize: Integer;
		function GetInterfaceFormat: TInterfaceFormat; override;
	public
		function ReadRecordSize: Integer; override;
		procedure WriteRecordSize(ASize: Integer); override;
		procedure WriteReverseRecordSize(ASize: Integer);
		procedure Rewind; override;
		procedure CheckSignature; override;
		procedure CheckRecordSize; override;
		constructor Create(AFile: String; Mode: Word = fmOpenRead);
		destructor Destroy; override;
		procedure WriteString(AString: String); override;
		procedure WriteInteger(AInteger: Integer); override;
		procedure WriteExtended(AExtended: Extended); override;
		procedure WriteDouble(ADouble: Double); override;
		procedure WriteSingle(ASingle: Single); override;
		procedure WriteByte(AByte: Byte); override;
		procedure WriteSignature; override;
		procedure FlushRecord; override;
	end;

	TXPInterfaceFileStream = class(TCustomFortranInterfaceFileStream)
	private
		function GetInterfaceFormat: TInterfaceFormat; override;
	protected
		function GetRecordSize: Integer; override;
	public
		function ReadRecordSize: Integer; override;
		procedure WriteRecordSize(ASize: Integer); override;
		constructor Create(AFile: String; Mode: Word = fmOpenRead);
		destructor Destroy; override;
		procedure WriteString(AString: String); override;
		procedure WriteInteger(AInteger: Integer); override;
		procedure WriteExtended(AExtended: Extended); override;
		procedure WriteDouble(ADouble: Double); override;
		procedure WriteSingle(ASingle: Single); override;
		procedure WriteByte(AByte: Byte); override;
		procedure FlushRecord; override;
	end;

	TTextInterfaceFileStream = class(TCustomFortranInterfaceFileStream)
	private
		fFile: TFileStream;
		fCurrentLine: String;
		fCurrentRecSize: Integer;
		fPosInRec: Integer;
		fCurrentWriteRecordStream: String;
		fCurrentWriteRecordSize: Integer;
		fTextFile: TStAnsiTextStream;
		fTokens: TStringList;
		fCurrentTokenIndex: Integer;
		function GetInterfaceFormat: TInterfaceFormat; override;
		procedure CheckCurrentLineTokens;
	protected
		// IFortranInterfaceFileStream implementation
		function GetRecordSize: Integer; override;
	public
		constructor Create(AFile: String; Mode: Word = fmOpenRead);
		destructor Destroy; override;
		procedure GoToStartOfFile;
		// IFortranInterfaceFileStream implementation
		function ReadRecordSize: Integer; override;
		function IsEndOfRecord: Boolean; override;
		function IsStartOfRecord: Boolean; override;
		procedure CheckRecordSize; override;
		procedure FlushRecord; override;
		procedure Rewind; override;
		procedure WriteRecordSize(ASize: Integer); override;
		procedure CheckSignature; override;
		property CurrentRecordSize: Integer read GetRecordSize;
		// IInterfaceFileStream implementation
		function ReadString(ASize: Integer): String; override;
		function ReadDouble: Double; override;
		function ReadInteger: Integer; override;
		function ReadExtended: Extended; override;
		function ReadSingle: Single; override;
		function ReadByte: Byte; override;
		procedure WriteString(AString: String); override;
		procedure WriteInteger(AInteger: Integer); override;
		procedure WriteExtended(AExtended: Extended); override;
		procedure WriteDouble(ADouble: Double); override;
		procedure WriteSingle(ASingle: Single); override;
		procedure WriteByte(AByte: Byte); override;
		procedure WriteSignature; override;
		function IsEOF: Boolean; override;
		property Stream: TStBufferedStream read fStream;
		property InterfaceFormat: TInterfaceFormat read GetInterfaceFormat;
	end;

	TSWMMStandardInterfaceFile = class(TInterfacedObject, ISWMMStandardInterfaceFile,
		IFlowStream, IInterfaceFileStream)
	private
		fStream: TCustomFortranInterfaceFileStream;
		fTitles: array[0..3] of String;
		fStart: TDateTime;
		fSourceBlock: String;
		fNumInlets: Integer;
		fNumPollutants: Integer;
		fArea: Double;
		fUsesAlphanumericIDs: Boolean;
		fAlphaIDSize: Integer;
		fIDs: TStringList;
		fFlowMultiplier: Double;
		fCurrentTime: TDateTime;
		fCurrentTimeStep: Double;
		fFlows: TFlowsArray;
		fInitialTime: TDateTime;
		fInterfaceFormat: TInterfaceFormat;
		fNextTime: TDateTime;
	protected
		function GetTitles(Index: Integer): String;
		function GetStart: TDateTime;
		function GetSourceBlock: String;
		function GetNumInlets: Integer;
		function GetNumPollutants: Integer;
		function GetArea: Double;
		function GetUsesAlphaNumericIDs: Boolean;
		function GetIDs(Index: Integer): String;
		function GetIDsList: TStringList;
		function GetToIDs(Index: Integer): String;
		function GetFlowMultiplier: Double;
		function GetCurrentTime: TDateTime;
		function GetFlows(Index: Integer): Double;
		function GetFlowUnits(Index: Integer): TConvType;
		function GetCurrentTimeStep: Double;
		function GetStream: TStream;
		function GetAlphaIDSize: Integer;
		procedure SetTitles(Index: Integer; Value: String);
		procedure SetStart(Value: TDateTime);
		procedure SetSourceBlock(Value: String);
		procedure SetNumInlets(Value: Integer);
		procedure SetNumPollutants(Value: Integer);
		procedure SetArea(Value: Double);
		procedure SetIDs(Index: Integer; Value: String);
		procedure SetFlowMultiplier(Value: Double);
		procedure SetAlphaIDSize(Value: Integer);
		procedure SetUsesAlphaNumericIDs(Value: Boolean);
		// IFlowStream implementation
		function GetNumFlows: Integer;
		function GetFlow(AIndex: Integer): Double;
		function GetFileSize: Int64;
		function GetFilePosition: Int64;
		function GetInitialTime: TDateTime;
		function GetNextTime: TDateTime;
		function GetCurrentEndTime: TDateTime;
		function GetID(AIndex: Integer): String;
		function GetEOF: Boolean;
		function GetInterfaceFormat: TInterfaceFormat;
	public
		constructor Create(AFile: String; Mode: Word = fmOpenRead;
			InterfaceFormat: TInterfaceFormat = if32);
		destructor Destroy; override;

		// ISWMMStandardInterfaceFile implementation
		procedure ReadHeader;
		procedure ReadFlows;
		procedure FlushFlows;
		procedure Restart;
		function IsEOF: Boolean;
		property Titles[Index: Integer]: String read GetTitles;
		property Start: TDateTime read fStart;
		property SourceBlock: String read GetSourceBlock;
		property NumInlets: Integer read GetNumInlets;
		property NumPollutants: Integer read GetNumPollutants;
		property Area: Double read GetArea;
		property UsesAlphaNumericIDs: Boolean read GetUsesAlphaNumericIDs;
		property AlphaIDSize: Integer read GetAlphaIDSize write SetAlphaIDSize;
		property IDs[Index: Integer]: String read GetIDs;
		property IDsList: TStringList read GetIDsList;
		property ToIds[Index: Integer]: String read GetToIDs;
		property FlowMultiplier: Double read GetFlowMultiplier;
		property CurrentTime: TDateTime read GetCurrentTime;
		property CurrentTimeStep: Double read GetCurrentTimeStep;
		property Flows[Index: Integer]: Double read GetFlows;
		property Stream: TStream read GetStream;
		property FlowUnits[Index: Integer]: TConvType read GetFlowUnits;
		procedure WriteHeader; overload;
		procedure WriteHeaderLimitNodes(NodesList: TStringList); overload;
		procedure WriteHeader(AInterfaceFile: ISWMMStandardInterfaceFile); overload;
		procedure WriteHeaderLimitNodes(AInterfaceFile: ISWMMStandardInterfaceFile;
			NodesList: TStringList); overload;
		procedure WriteFlows(SkipInterval: Integer = 0); overload;
		procedure WriteFlowsLimitNodes(NodesList: TStringList; SkipInterval: Integer = 0); overload;
		procedure WriteFlowsLimitNodesIndexList(NodesList: TStringList; SkipInterval: Integer = 0); overload;
		procedure WriteFlows(AInterfaceFile: ISWMMStandardInterfaceFile; SkipInterval: Integer = 0); overload;
		procedure WriteFlowsLimitNodes(AInterfaceFile: ISWMMStandardInterfaceFile;
			NodesList: TStringList; SkipInterval: Integer = 0); overload;
		procedure WriteFlowsLimitNodesIndexList(AInterfaceFile: ISWMMStandardInterfaceFile;
			NodesList: TStringList; SkipInterval: Integer = 0); overload;
		property InterfaceFormat: TInterfaceFormat read GetInterfaceFormat;
		procedure FlushRecord;

		// ISWMMStandardInterfaceFile, IInterfaceFileStream implementation
		function ReadString(ASize: Integer): String;
		function ReadInteger: Integer;
		function ReadExtended: Extended;
		function ReadDouble: Double;
		function ReadSingle: Single;
		function ReadByte: Byte;
		procedure WriteString(AString: String);
		procedure WriteInteger(AInteger: Integer);
		procedure WriteExtended(AExtended: Extended);
		procedure WriteDouble(ADouble: Double);
		procedure WriteSingle(ASingle: Single);
		procedure WriteByte(AByte: Byte);

		// IFlowStream implementation
		procedure MoveToStartOfFlows;
		property NumFlows: Integer read GetNumFlows;
		property Flow[Index: Integer]: Double read GetFlow;
		property FileSize: Int64 read GetFileSize;
		property FilePosition: Int64 read GetFilePosition;
		property InitialTime: TDateTime read GetInitialTime;
		property NextTime: TDateTime read GetNextTime;
		property CurrentEndTime: TDateTime read GetCurrentEndTime;
		property ID[Index: Integer]: String read GetID;
		property EOF: Boolean read GetEOF;
	end;

	TXP_SYF_FileStream = class(TInterfacedObject, IInterfaceFileStream,
		IXP_SYF_FileStream, IFlowStream)
	private
		fFile: TFileStream;
		fStream: TStBufferedStream;
		fJCE: Integer;
		fMetric: Integer;
		fNumCycles: Integer;
		fNumJunctions: Integer;
		fNumLinks: Integer;
		fNumConduits: Integer;
		fNumPumps: Integer;
		fNumOrifices: Integer;
		fNumWeirs: Integer;
		fTimeStep: Single;
		fLinks: array of TSYFLink;
		fNodes: array of TSYFNode;
		fCycle: Integer;
		fHour: Integer;
		fMinute: Integer;
		fSecond: Integer;
		fBeginFlowsPos: Int64;
		fSYFTimeStep: Single;
		fInitialTime: TDateTime;
		fCycleStep: Integer;
	protected
		function GetJCE: Integer;
		function GetIsMetric: Boolean;
		function GetNumCycles: Integer;
		function GetNumJunctions: Integer;
		function GetNumLinks: Integer;
		function GetNumConduits: Integer;
		function GetNumPumps: Integer;
		function GetNumOrifices: Integer;
		function GetNumWeirs: Integer;
		function GetTimeStep: Single;
		function GetNode(Index: Integer): TSYFNode;
		function GetLink(Index: Integer): TSYFLink;
		function GetCurrentTimeCycle: Integer;
		function GetCurrentTime: TDateTime;
		function GetSYFTimeStep: Single;
		function GetFileSize: Int64;
		function GetFilePosition: Int64;
		// IFlowStream implementation
		function GetNumFlows: Integer;
		function GetFlow(AIndex: Integer): Double;
		function GetInitialTime: TDateTime;
		function GetNextTime: TDateTime;
		function GetCurrentEndTime: TDateTime;
		function GetID(AIndex: Integer): String;
		function GetEOF: Boolean;
	public
		constructor Create(AFile: String; Mode: Word = fmOpenRead);
		destructor Destroy; override;
		function GetFlowRecordSize: Integer;
		// IInterfaceFileStream Implementation
		function ReadString(ASize: Integer): String;
		function ReadInteger: Integer;
		function ReadExtended: Extended;
		function ReadDouble: Double;
		function ReadSingle: Single;
		function ReadByte: Byte;
		procedure WriteString(AString: String);
		procedure WriteInteger(AInteger: Integer);
		procedure WriteExtended(AExtended: Extended);
		procedure WriteDouble(ADouble: Double);
		procedure WriteSingle(ASingle: Single);
		procedure WriteByte(AByte: Byte);
		function IsEOF: Boolean;
		// IXP_SYF_FileStream Implementation
		procedure ReadHeader;
		procedure ReadFlows;
		procedure FlushFlows;
		procedure MoveToStartOfFlows;

		property IsMetric: Boolean read GetIsMetric;
		property NumCycles: Integer read GetNumCycles;
		property NumJunctions: Integer read GetNumJunctions;
		property NumLinks: Integer read GetNumLinks;
		property NumConduits: Integer read GetNumConduits;
		property NumPumps: Integer read GetNumPumps;
		property NumOrifices: Integer read GetNumOrifices;
		property NumWeirs: Integer read GetNumWeirs;
		property TimeStep: Single read GetTimeStep;
		property Nodes[Index: Integer]: TSYFNode read GetNode;
		property Links[Index: Integer]: TSYFLink read GetLink;

		property CurrentTimeCycle: Integer read GetCurrentTimeCycle;
		property CurrentTime: TDateTime read GetCurrentTime;
		property SYFTimeStep: Single read GetSYFTimeStep;
		property FileSize: Int64 read GetFileSize;
		property FilePosition: Int64 read GetFilePosition;

		// IFlowStream implementation
		property NumFlows: Integer read GetNumFlows;
		property Flow[Index: Integer]: Double read GetFlow;
		property InitialTime: TDateTime read GetInitialTime;
		property NextTime: TDateTime read GetNextTime;
		property CurrentEndTime: TDateTime read GetCurrentEndTime;
		property ID[Index: Integer]: String read GetID;
		property EOF: Boolean read GetEOF;
	end;

	TPDXTextLink = record
		ID: String;
		Flow: Single;
	end;

	TPDXTextStream = class(TInterfacedObject, IFlowStream) //PDX Output Text Files
	private
		fFile: TFileStream;
		fTextStream: TStAnsiTextStream;
		fLinks: array of TPDXTextLink;
		fTimeStep: Single;
		fInitialTime: TDateTime;
		fBeginFlowsPos: Int64;
		fEndOfFile: Boolean;
		fCurrentTime: TDateTime;
		fAtBeginningOfFlows: Boolean;
	protected
		function GetNumFlows: Integer;
		function GetFlow(AIndex: Integer): Double;
		function GetFileSize: Int64;
		function GetFilePosition: Int64;
		function GetCurrentTime: TDateTime;
		function GetCurrentEndTime: TDateTime;
		function GetNextTime: TDateTime;
		function GetInitialTime: TDateTime;
		function GetID(AIndex: Integer): String;
		function GetEOF: Boolean;
	public
		constructor Create(AFile: String; Mode: Word = fmOpenRead);
		destructor Destroy; override;

		procedure ReadFlows;
		procedure FlushFlows;
		procedure MoveToStartOfFlows;
		procedure ReadHeader;

		property NumFlows: Integer read GetNumFlows;
		property Flow[Index: Integer]: Double read GetFlow;
		property ID[Index: Integer]: String read GetID;
		property FileSize: Int64 read GetFileSize;
		property FilePosition: Int64 read GetFilePosition;
		property CurrentTime: TDateTime read GetCurrentTime;
		property CurrentEndTime: TDateTime read GetCurrentEndTime;
		property NextTime: TDateTime read GetNextTime;
		property InitialTime: TDateTime read GetInitialTime;
		property EOF: Boolean read GetEOF;
	end;

	TMOUSE_PRF_FileStream = class(TInterfacedObject, IInterfaceFileStream, IMOUSE_PRF_FileStream,
		IFlowStream)
	private
		fCurrentLineIndex: Integer;
		fFile: TFileStream;
		fTextFile: TStAnsiTextStream;
		fInitialTime: TDateTime;
		fFirstRead: Boolean;
		fLastTimeStep: Double;
		fNodes: array of TPRFNode;
		fCurrentLine: String;
		fTokens: TStringList;
		fCurrentTokenIndex: Integer;
		fCurrentTime: TDateTime;
		// IMOUSE_PRF_FileStream implementation
		function GetNode(AIndex: Integer): TPRFNode;
		procedure CheckCurrentLineTokens;
	protected
		// IFlowStream implementation
		function GetNumFlows: Integer;
		function GetFlow(AIndex: Integer): Double;
		function GetFileSize: Int64;
		function GetFilePosition: Int64;
		function GetCurrentTime: TDateTime;
		function GetCurrentEndTime: TDateTime;
		function GetNextTime: TDateTime;
		function GetInitialTime: TDateTime;
		function GetID(AIndex: Integer): String;
		function GetEOF: Boolean;
	public
		constructor Create(AFile: String; Mode: Word = fmOpenRead);
		destructor Destroy; override;
		procedure FlushLine;
		function IsEndOfRecord: boolean;
		function IsStartOfRecord: boolean;
		// IInterfaceFileStream Implementation
		function ReadString(ASize: Integer): String;
		function ReadInteger: Integer;
		function ReadExtended: Extended;
		function ReadDouble: Double;
		function ReadSingle: Single;
		function ReadByte: Byte;
		procedure WriteString(AString: String);
		procedure WriteInteger(AInteger: Integer);
		procedure WriteExtended(AExtended: Extended);
		procedure WriteDouble(ADouble: Double);
		procedure WriteSingle(ASingle: Single);
		procedure WriteByte(AByte: Byte);
		procedure FlushFlows;
		function IsEOF: Boolean;
		// IMOUSE_PRF_FileStream implementation
		procedure ReadHeader;
		procedure ReadFlows;
		procedure MoveToStartOfFlows;
		property Nodes[Index: Integer]: TPRFNode read GetNode;
		// IFlowStream implementation
		property NumFlows: Integer read GetNumFlows;
		property Flow[Index: Integer]: Double read GetFlow;
		property InitialTime: TDateTime read GetInitialTime;
		property NextTime: TDateTime read GetNextTime;
		property CurrentEndTime: TDateTime read GetCurrentEndTime;
		property ID[Index: Integer]: String read GetID;
		property EOF: Boolean read GetEOF;
	end;

	TMOUSE_PRF_InterfaceFileStream = class(TInterfacedObject, ISWMMStandardInterfaceFile,
		IFlowStream, IInterfaceFileStream)
	private
		fFlowsStartLine: int64;
		fCurrentLine: string;
		fFirstRead: boolean;
		fLastTimeStep: Double;
		fCurrentTime: TDateTime;
		fInitialTime: TDateTime;
		fFile: TFileStream;
		fTextFile: TStAnsiTextStream;
		fTokens: TStringList;
		fCurrentTokenIndex: Integer;
		fCurrentLineIndex: Integer;
		fNodes: array of TPRFNode;
		fIDs: TStringList;
		fHeaderAlreadyRead: boolean;
		procedure CheckCurrentLineTokens;
	protected
		// ISWMMStandardInterfaceFile implementation
		function GetTitles(Index: Integer): String;
		function GetStart: TDateTime;
		function GetSourceBlock: String;
		function GetNumInlets: Integer;
		function GetNumPollutants: Integer;
		function GetArea: Double;
		function GetUsesAlphaNumericIDs: Boolean;
		function GetAlphaIDSize: Integer;
		function GetIDs(Index: Integer): String;
		function GetIDsList: TStringList;
		function GetToIDs(Index: Integer): String;
		function GetFlowMultiplier: Double;
		function GetCurrentTime: TDateTime;
		function GetCurrentTimeStep: Double;
		function GetFlows(Index: Integer): Double;
		function GetFlowUnits(Index: Integer): TConvType;
		function GetStream: TStream;
		function GetInterfaceFormat: TInterfaceFormat;
		procedure SetTitles(Index: Integer; Value: String);
		procedure SetStart(Value: TDateTime);
		procedure SetSourceBlock(Value: String);
		procedure SetNumInlets(Value: Integer);
		procedure SetNumPollutants(Value: Integer);
		procedure SetArea(Value: Double);
		procedure SetIDs(Index: Integer; Value: String);
		procedure SetFlowMultiplier(Value: Double);
		procedure SetAlphaIDSize(Value: Integer);
		procedure SetUsesAlphaNumericIDs(Value: Boolean);

		// IFlowStream implementation
		function GetNumFlows: Integer;
		function GetFlow(AIndex: Integer): Double;
		function GetFileSize: Int64;
		function GetFilePosition: Int64;
		function GetCurrentEndTime: TDateTime;
		function GetNextTime: TDateTime;
		function GetInitialTime: TDateTime;
		function GetID(AIndex: Integer): String;
		function GetEOF: Boolean;
	public
		constructor Create(AFile: String; Mode: Word = fmOpenRead);
		destructor Destroy; override;
		procedure FlushLine;
		function IsEndOfRecord: boolean;
		function IsStartOfRecord: boolean;
		// IInterfaceFileStream implementation
		function ReadString(ASize: Integer): String;
		function ReadInteger: Integer;
		function ReadExtended: Extended;
		function ReadDouble: Double;
		function ReadSingle: Single;
		function ReadByte: Byte;
		procedure WriteString(AString: String);
		procedure WriteInteger(AInteger: Integer);
		procedure WriteExtended(AExtended: Extended);
		procedure WriteDouble(ADouble: Double);
		procedure WriteSingle(ASingle: Single);
		procedure WriteByte(AByte: Byte);
		function IsEOF: Boolean;
		// ISWMMStandardInterfaceFile implementation
		procedure Restart;
		procedure ReadHeader;
		procedure ReadFlows;
		procedure WriteHeader; overload;
		procedure WriteHeaderLimitNodes(NodesList: TStringList); overload;
		procedure WriteHeader(AInterfaceFile: ISWMMStandardInterfaceFile); overload;
		procedure WriteHeaderLimitNodes(AInterfaceFile: ISWMMStandardInterfaceFile;
			NodesList: TStringList); overload;
		procedure WriteFlows(SkipInterval: Integer = 0); overload;
		procedure WriteFlowsLimitNodes(NodesList: TStringList; SkipInterval: Integer = 0); overload;
		procedure WriteFlowsLimitNodesIndexList(NodesList: TStringList; SkipInterval: Integer = 0); overload;
		procedure WriteFlows(AInterfaceFile: ISWMMStandardInterfaceFile; SkipInterval: Integer = 0); overload;
		procedure WriteFlowsLimitNodes(AInterfaceFile: ISWMMStandardInterfaceFile;
			NodesList: TStringList; SkipInterval: Integer = 0); overload;
		procedure WriteFlowsLimitNodesIndexList(AInterfaceFile: ISWMMStandardInterfaceFile;
			NodesList: TStringList; SkipInterval: Integer = 0); overload;
    procedure FlushRecord;
		property Titles[Index: Integer]: String read GetTitles write SetTitles;
		property Start: TDateTime read GetStart write SetStart;
		property SourceBlock: String read GetSourceBlock write SetSourceBlock;
		property NumInlets: Integer read GetNumInlets write SetNumInlets;
		property NumPollutants: Integer read GetNumPollutants write SetNumPollutants;
		property Area: Double read GetArea write SetArea;
		property UsesAlphaNumericIDs: Boolean read GetUsesAlphaNumericIDs
			write SetUsesAlphaNumericIDs;
		property AlphaIDSize: Integer read GetAlphaIDSize write SetAlphaIDSize;
		property IDs[Index: Integer]: String read GetIDs write SetIDs;
		property IDsList: TStringList read GetIDsList;
		property ToIds[Index: Integer]: String read GetToIDs;
		property FlowMultiplier: Double read GetFlowMultiplier write SetFlowMultiplier;
		property CurrentTime: TDateTime read GetCurrentTime;
		property CurrentTimeStep: Double read GetCurrentTimeStep;
		property Flows[Index: Integer]: Double read GetFlows;
		property InterfaceFormat: TInterfaceFormat read GetInterfaceFormat;
		property FlowUnits[Index: Integer]: TConvType read GetFlowUnits;
		// IFlowStream implementation
		procedure FlushFlows;
		procedure MoveToStartOfFlows;

		property NumFlows: Integer read GetNumFlows;
		property Flow[Index: Integer]: Double read GetFlow;
		property ID[Index: Integer]: String read GetID;
		property FileSize: Int64 read GetFileSize;
		property FilePosition: Int64 read GetFilePosition;
		property CurrentEndTime: TDateTime read GetCurrentEndTime;
		property NextTime: TDateTime read GetNextTime;
		property InitialTime: TDateTime read GetInitialTime;
		property EOF: Boolean read GetEOF;
	end;

	TPRFSelectNode = record
		ID: String;
		Selected: Boolean;
		NodeTypeID: Integer;
		NodeType: TPRFNodeType;
		NodeTypeDescription: String;
		UpNodeID: String;
		DnNodeID: String;
		Position: Double;
		PositionUnits: String;
	end;

	TMOUSE_PRF_M11InFileStream = class
	private
		fNodes: array of TPRFSelectNode;
		fFile: TFileStream;
		fTextFile: TStAnsiTextStream;
		fFileName: String;
		function GetNodeCount: Integer;
		function GetFileName: String;
    procedure SetNode(AIndex: Integer; ANode: TPRFSelectNode);
	protected
		function GetNode(AIndex: Integer): TPRFSelectNode;
	public
		constructor Create(AFile: String; Mode: Word = fmOpenRead);
		property Nodes[Index: Integer]: TPRFSelectNode read GetNode write SetNode;
		property NodeCount: Integer read GetNodeCount;
		property Stream: TFileStream read fFile;
		property FileName: String read GetFileName;
	end;

	TNodeModifier = class
	public
		Multiplier: Double;
	end;

implementation

uses PDXDateUtils, fStatus;

{ TCustomFortranInterfaceFileStream }

constructor TCustomFortranInterfaceFileStream.Create(AFile: String; Mode: Word = fmOpenRead);
begin
	fFile := TFileStream.Create(AFile, Mode);
	fStream := TStBufferedStream.Create(fFile);
	fCurrentWriteRecordStream := TMemoryStream.Create;
	fCurrentWriteRecordStream.Clear;
	if Mode <> fmCreate then
		GoToStartOfFile;
end;

destructor TCustomFortranInterfaceFileStream.Destroy;
begin
	fStream.Free;
	fFile.Free;
	fCurrentWriteRecordStream.Free;
	inherited;
end;

procedure TCustomFortranInterfaceFileStream.CheckRecordSize;
begin
	if IsEndOfRecord then
	begin
		fCurrentRecSize := ReadRecordSize; // Read end-of-record delimiter (previous record's size)
		if not IsEOF then
			fCurrentRecSize := ReadRecordSize // Read current record's size
		else
		begin
			fCurrentRecSize := 0;
		end;
		fPosInRec := 0;
	end;
end;

function TCustomFortranInterfaceFileStream.ReadDouble: Double;
var
	Buf: Double;
begin
	fStream.Read(Buf, SizeOf(Double));
	Inc(fPosInRec, SizeOf(Double));
	CheckRecordSize;
	Result := Buf;
end;

function TCustomFortranInterfaceFileStream.ReadExtended: Extended;
var
	Buf: Extended;
begin
	fStream.Read(Buf, SizeOf(Extended));
	Inc(fPosInRec, SizeOf(Extended));
	CheckRecordSize;
	Result := Buf;
end;

function TCustomFortranInterfaceFileStream.ReadInteger: Integer;
var
	Buf: Integer;
begin
	fStream.Read(Buf, SizeOf(Integer));
	Inc(fPosInRec, SizeOf(Integer));
	CheckRecordSize;
	Result := Buf;
end;

function TCustomFortranInterfaceFileStream.ReadString(ASize: Integer): String;
var
	Buf: String;
begin
	SetLength(Buf, ASize);
	fStream.Read(Buf[1], ASize);
	Inc(fPosInRec, ASize);
	CheckRecordSize;
	Result := Buf;
end;

procedure TCustomFortranInterfaceFileStream.WriteDouble(ADouble: Double);
begin
	fStream.Write(ADouble, SizeOf(ADouble));
end;

procedure TCustomFortranInterfaceFileStream.WriteExtended(AExtended: Extended);
begin
	fStream.Write(AExtended, SizeOf(AExtended));
end;

procedure TCustomFortranInterfaceFileStream.WriteInteger(AInteger: Integer);
begin
	fStream.Write(AInteger, SizeOf(AInteger));
end;

procedure TCustomFortranInterfaceFileStream.WriteString(AString: String);
begin
	fStream.Write(AString[1], Length(AString));
end;

function TCustomFortranInterfaceFileStream.ReadSingle: Single;
var
	Buf: Single;
begin
	fStream.Read(Buf, SizeOf(Single));
	Inc(fPosInRec, SizeOf(Single));
	CheckRecordSize;
	Result := Buf;
end;

procedure TCustomFortranInterfaceFileStream.WriteSingle(ASingle: Single);
begin
	fStream.Write(ASingle, SizeOf(ASingle));
end;

function TCustomFortranInterfaceFileStream.GetRecordSize: Integer;
begin
	Result := fCurrentRecSize;
end;

function TCustomFortranInterfaceFileStream.IsEOF: Boolean;
begin
	Result := fStream.Position >= fStream.FastSize-1;
end;

function TCustomFortranInterfaceFileStream.IsEndOfRecord: Boolean;
begin
	Result := fPosInRec = fCurrentRecSize;
end;

function TCustomFortranInterfaceFileStream.IsStartOfRecord: Boolean;
begin
	Result := fPosInRec = 0;
end;

procedure TCustomFortranInterfaceFileStream.Rewind;
var
	PrevRecSize: Integer;
begin
	if fPosInRec > 0 then
		fStream.Seek(-fPosInRec, soFromCurrent)
	else
	begin // we're at the beginning of a record
		if IsEOF then
			fStream.Seek(-SizeOf(Integer), soFromCurrent)
		else
			fStream.Seek(-2*SizeOf(Integer), soFromCurrent);
		PrevRecSize := ReadRecordSize;
		fStream.Seek(-(SizeOf(Integer)+PrevRecSize), soFromCurrent);
		fCurrentRecSize := PrevRecSize;
	end;
	fPosInRec := 0;
end;

function TCustomFortranInterfaceFileStream.ReadByte: Byte;
var
	Buf: Byte;
begin
	fStream.Read(Buf, SizeOf(Byte));
	Inc(fPosInRec, SizeOf(Byte));
	CheckRecordSize;
	Result := Buf;
end;

procedure TCustomFortranInterfaceFileStream.WriteByte(AByte: Byte);
begin
	fStream.Write(AByte, SizeOf(Byte));
end;

procedure TCustomFortranInterfaceFileStream.GoToStartOfFile;
begin
	fStream.Seek(0, soFromBeginning);
	try
		CheckSignature;
	except
		raise;
	end;
	fCurrentRecSize := ReadRecordSize;
	fPosInRec := 0;
end;

procedure TCustomFortranInterfaceFileStream.CheckSignature;
begin
	// Empty
end;

procedure TCustomFortranInterfaceFileStream.WriteSignature;
begin
	// Empty;
end;

{ TF95InterfaceFileStream }

constructor TF95InterfaceFileStream.Create(AFile: String; Mode: Word = fmOpenRead);
begin
	inherited;
end;

destructor TF95InterfaceFileStream.Destroy;
begin
	inherited;
end;

procedure TF95InterfaceFileStream.FlushRecord;
begin
	inherited;
	// Write BOR size
	fStream.Write(fCurrentWriteRecordSize, SizeOf(fCurrentWriteRecordSize));
	// Write record
	fCurrentWriteRecordStream.Position := 0;
	fStream.Write(fCurrentWriteRecordStream.Memory^, fCurrentWriteRecordSize);
	// Write EOR size
	fStream.Write(fCurrentWriteRecordSize, SizeOf(fCurrentWriteRecordSize));

	fCurrentWriteRecordStream.Clear;
	fCurrentWriteRecordSize := 0;
end;

function TF95InterfaceFileStream.GetInterfaceFormat: TInterfaceFormat;
begin
  Result := if95;
end;

function TF95InterfaceFileStream.ReadRecordSize: Integer;
var
	Buf: Integer;
begin
	fStream.Read(Buf, SizeOf(Integer));
	Result := Buf;
end;

procedure TF95InterfaceFileStream.WriteByte(AByte: Byte);
begin
//  inherited;
	Inc(fCurrentWriteRecordSize, SizeOf(AByte));
	fCurrentWriteRecordStream.Write(AByte, SizeOf(AByte));
end;

procedure TF95InterfaceFileStream.WriteDouble(ADouble: Double);
begin
//	inherited;
	Inc(fCurrentWriteRecordSize, SizeOf(ADouble));
	fCurrentWriteRecordStream.Write(ADouble, SizeOf(ADouble));
end;

procedure TF95InterfaceFileStream.WriteExtended(AExtended: Extended);
begin
//	inherited;
	Inc(fCurrentWriteRecordSize, SizeOf(AExtended));
	fCurrentWriteRecordStream.Write(AExtended, SizeOf(AExtended));
end;

procedure TF95InterfaceFileStream.WriteInteger(AInteger: Integer);
begin
//	inherited;
	Inc(fCurrentWriteRecordSize, SizeOf(AInteger));
	fCurrentWriteRecordStream.Write(AInteger, SizeOf(AInteger));
end;

procedure TF95InterfaceFileStream.WriteRecordSize(ASize: Integer);
begin
	fCurrentWriteRecordStream.Write(ASize, SizeOf(ASize));
end;

procedure TF95InterfaceFileStream.WriteSingle(ASingle: Single);
begin
//	inherited;
	Inc(fCurrentWriteRecordSize, SizeOf(ASingle));
	fCurrentWriteRecordStream.Write(ASingle, SizeOf(ASingle));
end;

procedure TF95InterfaceFileStream.WriteString(AString: String);
begin
//	inherited;
	Inc(fCurrentWriteRecordSize, Length(AString));
	fCurrentWriteRecordStream.Write(AString[1], Length(AString));
end;

{ TSWMMStandardInterfaceFile }

constructor TSWMMStandardInterfaceFile.Create(AFile: String; Mode: Word = fmOpenRead;
	InterfaceFormat: TInterfaceFormat = if32);
var
	AStream: TCustomFortranInterfaceFileStream;
	TestByte: Byte;
	CurrentPos: Integer;
begin
	case InterfaceFormat of
		if95: AStream := TF95InterfaceFileStream.Create(AFile, Mode);
		if32:
			try
				AStream := TF32InterfaceFileStream.Create(AFile, Mode);
			except
				AStream := TF95InterfaceFileStream.Create(AFile, Mode);
			end;
		ifXP: AStream := TXPInterfaceFileStream.Create(AFile, Mode);
		ifXPSYF: ;
		ifText: AStream := TTextInterfaceFileStream.Create(AFile, Mode);
	end;
  fInterfaceFormat := InterfaceFormat;

	fStream := AStream;
	fIDs := TStringList.Create;
	if not (Mode = fmCreate) then
	begin
		ReadHeader;
	end
	else
	begin
		if InterfaceFormat = if32 then
			AStream.WriteSignature;
	end;
end;

destructor TSWMMStandardInterfaceFile.Destroy;
begin
	fIDs.Free;
	fStream.Free;
end;

function TSWMMStandardInterfaceFile.GetArea: Double;
begin
	Result := fArea;
end;

function TSWMMStandardInterfaceFile.GetCurrentTime: TDateTime;
begin
	Result := fCurrentTime;
end;

function TSWMMStandardInterfaceFile.GetCurrentTimeStep: Double;
begin
  Result := fCurrentTimeStep;
end;

function TSWMMStandardInterfaceFile.GetFlowMultiplier: Double;
begin
  Result := fFlowMultiplier;
end;

function TSWMMStandardInterfaceFile.GetFlows(Index: Integer): Double;
begin
  Result := fFlows[Index];
end;

function TSWMMStandardInterfaceFile.GetIDs(Index: Integer): String;
begin
  if (Index < 0) or (Index > fNumInlets-1) then
		raise ERangeError.CreateFmt('Index %d is out of range [0..%d]',
      [Index, fNumInlets-1]);
  Result := fIDs[Index];
end;

function TSWMMStandardInterfaceFile.GetNumInlets: Integer;
begin
  Result := fNumInlets;
end;

function TSWMMStandardInterfaceFile.GetNumPollutants: Integer;
begin
	Result := fNumPollutants;
end;

function TSWMMStandardInterfaceFile.GetNumFlows: Integer;
begin
	Result := GetNumInlets;
end;

function TSWMMStandardInterfaceFile.GetFlow(AIndex: Integer): Double;
begin
	Result := GetFlows(AIndex);
end;

function TSWMMStandardInterfaceFile.GetFileSize: Int64;
begin
	Result := fStream.Stream.FastSize;
end;

function TSWMMStandardInterfaceFile.GetFilePosition: Int64;
begin
	Result := fStream.Stream.Position;
end;

function TSWMMStandardInterfaceFile.GetSourceBlock: String;
begin
  Result := fSourceBlock;
end;

function TSWMMStandardInterfaceFile.GetStart: TDateTime;
begin
	Result := fStart;
end;

function TSWMMStandardInterfaceFile.GetStream: TStream;
begin
	Result := fStream.Stream;
end;

function TSWMMStandardInterfaceFile.GetTitles(Index: Integer): String;
begin
	if (Index < 0) or (Index > 3) then
		raise ERangeError.CreateFmt('Index %d is out of range [0..3]', [Index]);
	Result := fTitles[Index];
end;

function TSWMMStandardInterfaceFile.GetUsesAlphaNumericIDs: Boolean;
begin
	Result := fUsesAlphaNumericIDs;
end;

function TSWMMStandardInterfaceFile.IsEOF: Boolean;
begin
  Result := fStream.IsEOF;
end;

procedure TSWMMStandardInterfaceFile.ReadFlows;
var
  XPRecSize: Integer;
	AJulDate: Integer;
	AJulTime: Double;
	i: Integer;
	ReadStream: TCustomFortranInterfaceFileStream;
begin
	ReadStream := fStream;
	AJulDate := ReadStream.ReadInteger;
	if ReadStream.InterfaceFormat = ifXP then
		AJulTime := ReadStream.ReadDouble
	else
		AJulTime := ReadStream.ReadSingle;
	fCurrentTime := DateTimeOfJulDate(AJulDate, AJulTime);
	if ReadStream.InterfaceFormat = ifXP then
		fCurrentTimeStep := ReadStream.ReadDouble
	else
		fCurrentTimeStep := ReadStream.ReadSingle;
	if ReadStream.InterfaceFormat =  ifXP then
		for i := 1 to fNumInlets do
			fFlows[i-1] := ReadStream.ReadDouble
	else
		for i := 1 to fNumInlets do
    begin
			fFlows[i-1] := ReadStream.ReadSingle;
    end;
	if IsEOF then
		fNextTime := MaxDateTime
	else
	begin
		AJulDate := ReadStream.ReadInteger;
		if ReadStream.InterfaceFormat = ifXP then
			AJulTime := ReadStream.ReadDouble
		else
			AJulTime := ReadStream.ReadSingle;
		fNextTime := DateTimeOfJulDate(AJulDate, AJulTime);
		if fStream.InterfaceFormat = ifXP then
		begin
			XPRecSize := SizeOf(Integer)+SizeOf(Double)+SizeOf(Double)+
				NumInlets*SizeOf(Double);
			fStream.Stream.Seek(-1*(SizeOf(Integer)+SizeOf(Double)), soFromCurrent);
		end
		else
		begin
			fStream.Rewind;
		end;
	end;
end;

procedure TSWMMStandardInterfaceFile.ReadHeader;
var
	StartJulDate: Integer;
	StartJulTime: Double;
	i: Integer;
	AlphaIDSize: Integer;
	FlowsPos: Int64;
	AJulDate: Integer;
	AJulTime: Double;
begin
	fTitles[0] := fStream.ReadString(80);
	fTitles[1] := fStream.ReadString(80);
	StartJulDate := fStream.ReadInteger;
	case InterfaceFormat of
		ifXP:	StartJulTime := fStream.ReadDouble;
	else
		StartJulTime := fStream.ReadSingle;
	end;
	fStart := DateTimeOfJulDate(StartJulDate, StartJulTime);
	fTitles[2] := fStream.ReadString(80);
	fTitles[3] := fStream.ReadString(80);
	fSourceBlock := fStream.ReadString(20);
	fNumInlets := fStream.ReadInteger;
//	SetLength(fIDs, NumInlets);
	SetLength(fFlows, NumInlets);
	fNumPollutants := fStream.ReadInteger;
	if fStream.InterfaceFormat = ifXP then
		fArea := fStream.ReadDouble
	else
		fArea := fStream.ReadSingle;
	if fStream.InterfaceFormat = ifXP then
		fUsesAlphanumericIDs := True
	else
		fUsesAlphanumericIDs := fStream.ReadSingle <> 0;
	if fUsesAlphanumericIDs then
	begin
		if fStream.InterfaceFormat = ifXP then
			AlphaIDSize := 16
		else
			AlphaIDSize := fStream.CurrentRecordSize div NumInlets;
		for i := 1 to fNumInlets do
//			fIDs[i-1] := fStream.ReadString(AlphaIDSize);
			fIDs.Add(fStream.ReadString(AlphaIDSize))
	end
	else
	begin
		for i := 1 to fNumInlets do
//			fIDs[i-1] := IntToStr(fStream.ReadInteger);
			fIDs.Add(IntToStr(fStream.ReadInteger));
	end;
	if fStream.InterfaceFormat = ifXP then
		fFlowMultiplier := fStream.ReadDouble
	else
		fFlowMultiplier := fStream.ReadSingle;

	fInitialTime := fStart;
end;

procedure TSWMMStandardInterfaceFile.Restart;
begin
	fStream.GoToStartOfFile;
	ReadHeader;
end;

procedure TSWMMStandardInterfaceFile.WriteFlows(SkipInterval: Integer = 0);
var
	i: Integer;
begin
	fStream.WriteInteger(Y2KJulDateOfDateTime(fCurrentTime));
	fStream.WriteSingle(SecondsOfDayOfDateTime(fCurrentTime));
	fStream.WriteSingle(fCurrentTimeStep * (SkipInterval+1));
	for i := 0 to fNumInlets-1 do
		fStream.WriteSingle(fFlows[i]);
	fStream.FlushRecord;
end;

procedure TSWMMStandardInterfaceFile.WriteFlowsLimitNodes(
	NodesList: TStringList; SkipInterval: Integer = 0);
var
	i: Integer;
begin
	fStream.WriteInteger(Y2KJulDateOfDateTime(fCurrentTime));
	fStream.WriteSingle(SecondsOfDayOfDateTime(fCurrentTime));
	fStream.WriteSingle(fCurrentTimeStep * (SkipInterval+1));
	for i := 0 to NodesList.Count-1 do
		if Assigned(NodesList.Objects[i]) then
			fStream.WriteSingle(fFlows[fIDs.IndexOf(NodesList[i])]*
				TNodeModifier(NodesList.Objects[i]).Multiplier)
		else
			fStream.WriteSingle(fFlows[fIDs.IndexOf(NodesList[i])]);
	fStream.FlushRecord;
end;

procedure TSWMMStandardInterfaceFile.WriteHeader;
var
	i: Integer;
begin
	fStream.WriteString(PadL(fTitles[0],80));
	fStream.FlushRecord;
	fStream.WriteString(PadL(fTitles[1],80));
	fStream.FlushRecord;
	fStream.WriteInteger(Y2KJulDateOfDateTime(Start));
	fStream.WriteSingle(SecondsOfDayOfDateTime(Start));
	fStream.FlushRecord;
	fStream.WriteString(PadL(fTitles[2],80));
	fStream.FlushRecord;
	fStream.WriteString(PadL(fTitles[3],80));
	fStream.FlushRecord;
	fStream.WriteString(PadL(fSourceBlock, 20));
	fStream.WriteInteger(fNumInlets);
	fStream.WriteInteger(fNumPollutants);
	if fStream.InterfaceFormat = ifXP then
	begin
		if fArea = 0 then
			fStream.WriteSingle(-1.0)
		else
			fStream.WriteSingle(-Abs(fArea));
	end
	else
		fStream.WriteSingle(Abs(fArea));
	if fUsesAlphaNumericIDs then
	begin
		fStream.WriteInteger(1);
		fStream.FlushRecord;
		for i := 1 to fNumInlets do
			fStream.WriteString(PadL(fIDs[i-1], fAlphaIDSize));
	end
	else
	begin
		fStream.WriteSingle(0);
		for i := 1 to fNumInlets do
			fStream.WriteInteger(StrToInt(fIDs[i-1]));
	end;
	fStream.FlushRecord;
	fStream.WriteSingle(fFlowMultiplier);
	fStream.FlushRecord;
end;

procedure TSWMMStandardInterfaceFile.WriteHeaderLimitNodes(NodesList: TStringList);
var
	i: Integer;
	AString: String;
begin
	AString := PadL(fTitles[0],80);
	fStream.WriteString(AString);
	fStream.FlushRecord;
	fStream.WriteString(PadL(fTitles[1],80));
	fStream.FlushRecord;
	fStream.WriteInteger(Y2KJulDateOfDateTime(Start));
	fStream.WriteSingle(SecondsOfDayOfDateTime(Start));
	fStream.FlushRecord;
	fStream.WriteString(PadL(fTitles[2],80));
	fStream.FlushRecord;
	fStream.WriteString(PadL(fTitles[3],80));
	fStream.FlushRecord;
	fStream.WriteString(PadL(fSourceBlock, 20));
	fStream.WriteInteger(NodesList.Count);
	fStream.WriteInteger(fNumPollutants);
	if fStream.InterfaceFormat = ifXP then
	begin
		if fArea = 0 then
			fStream.WriteSingle(-1.0)
		else
			fStream.WriteSingle(-Abs(fArea));
	end
	else
		fStream.WriteSingle(Abs(fArea));
	if fUsesAlphaNumericIDs then
	begin
		if fStream.InterfaceFormat <> ifXP then  //XP doesn't use NJCE flag
			fStream.WriteInteger(1);
		fStream.FlushRecord;
		for i := 1 to NodesList.Count do
		begin
			AString := PadL(NodesList[i-1], fAlphaIDSize);
			AString := Copy(AString, 0, fAlphaIDSize);
			fStream.WriteString(AString);
		end;
	end
	else
	begin
		if fStream.InterfaceFormat <> ifXP then  //XP doesn't use NJCE flag
			fStream.WriteInteger(0);
		fStream.FlushRecord;
		for i := 1 to NodesList.Count do
			fStream.WriteInteger(StrToInt(NodesList[i-1]));
	end;
	fStream.FlushRecord;
	fStream.WriteSingle(fFlowMultiplier);
	fStream.FlushRecord;
end;

procedure TSWMMStandardInterfaceFile.WriteFlows(
	AInterfaceFile: ISWMMStandardInterfaceFile; SkipInterval: Integer = 0);
var
	i: Integer;
begin
	fStream.WriteInteger(Y2KJulDateOfDateTime(AInterfaceFile.CurrentTime));
	fStream.WriteSingle(SecondsOfDayOfDateTime(AInterfaceFile.CurrentTime));
	fStream.WriteSingle(AInterfaceFile.CurrentTimeStep * (SkipInterval+1));
	for i := 0 to AInterfaceFile.NumInlets-1 do
		fStream.WriteSingle(AInterfaceFile.Flows[i]);
	fStream.FlushRecord;
end;

procedure TSWMMStandardInterfaceFile.WriteFlowsLimitNodes(
	AInterfaceFile: ISWMMStandardInterfaceFile; NodesList: TStringList;
	SkipInterval: Integer = 0);
var
	i: Integer;
begin
	fStream.WriteInteger(Y2KJulDateOfDateTime(AInterfaceFile.CurrentTime));
	fStream.WriteSingle(SecondsOfDayOfDateTime(AInterfaceFile.CurrentTime));
	fStream.WriteSingle(AInterfaceFile.CurrentTimeStep * (SkipInterval+1));

	for i := 0 to NodesList.Count-1 do
	begin
		if Assigned(NodesList.Objects[i]) then
			fStream.WriteSingle(AInterfaceFile.Flows[AInterfaceFile.IDsList.IndexOf(NodesList[i])]*
				TNodeModifier(NodesList.Objects[i]).Multiplier)
		else
			fStream.WriteSingle(AInterfaceFile.Flows[AInterfaceFile.IDsList.IndexOf(NodesList[i])]);
	end;
	fStream.FlushRecord;
end;

procedure TSWMMStandardInterfaceFile.WriteHeader(
	AInterfaceFile: ISWMMStandardInterfaceFile);
var
	i: Integer;
begin
	fStream.WriteString(PadL(AInterfaceFile.Titles[0],80));
	fStream.FlushRecord;
	fStream.WriteString(PadL(AInterfaceFile.Titles[1],80));
	fStream.FlushRecord;
	fStream.WriteInteger(Y2KJulDateOfDateTime(AInterfaceFile.Start));
	fStream.WriteSingle(SecondsOfDayOfDateTime(AInterfaceFile.Start));
	fStream.FlushRecord;
	fStream.WriteString(PadL(AInterfaceFile.Titles[2],80));
	fStream.FlushRecord;
	fStream.WriteString(PadL(AInterfaceFile.Titles[3],80));
	fStream.FlushRecord;
	fStream.WriteString(PadL(AInterfaceFile.SourceBlock, 20));
	fStream.WriteInteger(AInterfaceFile.NumInlets);
	fStream.WriteInteger(AInterfaceFile.NumPollutants);
	if fStream.InterfaceFormat = ifXP then
	begin
		if AInterfaceFile.Area = 0 then
			fStream.WriteSingle(-1.0)
		else
			fStream.WriteSingle(-Abs(AInterfaceFile.Area));
	end
	else
		fStream.WriteSingle(Abs(AInterfaceFile.Area));
	if AInterfaceFile.UsesAlphaNumericIDs then
	begin
		if fStream.InterfaceFormat <> ifXP then //XP doesn't use NJCE flag
			fStream.WriteInteger(1);
		fStream.FlushRecord;
		for i := 1 to AInterfaceFile.NumInlets do
			fStream.WriteString(PadL(AInterfaceFile.IDs[i-1], AInterfaceFile.AlphaIDSize));
	end
	else
	begin
		if fStream.InterfaceFormat <> ifXP then //XP doesn't use NJCE flag
			fStream.WriteInteger(0);
		fStream.FlushRecord;
		for i := 1 to AInterfaceFile.NumInlets do
			fStream.WriteInteger(StrToInt(AInterfaceFile.IDs[i-1]));
	end;
	fStream.FlushRecord;
	fStream.WriteSingle(AInterfaceFile.FlowMultiplier);
	fStream.FlushRecord;
end;

procedure TSWMMStandardInterfaceFile.WriteHeaderLimitNodes(
	AInterfaceFile: ISWMMStandardInterfaceFile; NodesList: TStringList);
var
	i: Integer;
	AString: String;
begin
	fStream.WriteString(PadL(AInterfaceFile.Titles[0],80));
	fStream.FlushRecord;
	fStream.WriteString(PadL(AInterfaceFile.Titles[1],80));
	fStream.FlushRecord;
	fStream.WriteInteger(Y2KJulDateOfDateTime(AInterfaceFile.Start));
	fStream.WriteSingle(SecondsOfDayOfDateTime(AInterfaceFile.Start));
	fStream.FlushRecord;
	fStream.WriteString(PadL(AInterfaceFile.Titles[2],80));
	fStream.FlushRecord;
	fStream.WriteString(PadL(AInterfaceFile.Titles[3],80));
	fStream.FlushRecord;
	fStream.WriteString(PadL(AInterfaceFile.SourceBlock, 20));
	fStream.WriteInteger(NodesList.Count);
	fStream.WriteInteger(AInterfaceFile.NumPollutants);
	if fStream.InterfaceFormat = ifXP then
	begin
		if AInterfaceFile.Area = 0 then
			fStream.WriteSingle(-1.0)
		else
			fStream.WriteSingle(-Abs(AInterfaceFile.Area));
	end
	else
		fStream.WriteSingle(Abs(AInterfaceFile.Area));
	if AInterfaceFile.UsesAlphaNumericIDs then
	begin
		if fStream.InterfaceFormat <> ifXP then  //XP doesn't use NJCE flag
			fStream.WriteInteger(1);
		fStream.FlushRecord;
		for i := 1 to NodesList.Count do
		begin
			AString := PadL(NodesList[i-1], AInterfaceFile.AlphaIDSize);
			AString := Copy(AString, 0, AInterfaceFile.AlphaIDSize);
			fStream.WriteString(AString);
		end;
	end
	else
	begin
		if fStream.InterfaceFormat <> ifXP then  //XP doesn't use NJCE flag
			fStream.WriteInteger(0);
		fStream.FlushRecord;
		for i := 1 to NodesList.Count do
			fStream.WriteInteger(StrToInt(NodesList[i-1]));
	end;
	fStream.FlushRecord;
	fStream.WriteSingle(AInterfaceFile.FlowMultiplier);
	fStream.FlushRecord;
end;

function TSWMMStandardInterfaceFile.GetAlphaIDSize: Integer;
begin
	Result := fAlphaIDSize;
end;

function TSWMMStandardInterfaceFile.GetIDsList: TStringList;
begin
	Result := fIDs;
end;

procedure TSWMMStandardInterfaceFile.SetAlphaIDSize(Value: Integer);
begin
	if fAlphaIDSize <> Value then
		fAlphaIDSize := Value;
end;

procedure TSWMMStandardInterfaceFile.SetUsesAlphaNumericIDs(
  Value: Boolean);
begin
	if fUsesAlphaNumericIDs <> Value then
		fUsesAlphaNumericIDs := Value;
end;

procedure TSWMMStandardInterfaceFile.MoveToStartOfFlows;
begin
	Restart;
end;

function TSWMMStandardInterfaceFile.GetInitialTime: TDateTime;
begin
  Result := fInitialTime;
end;

function TSWMMStandardInterfaceFile.GetID(AIndex: Integer): String;
begin
  Result := GetIDs(AIndex); 
end;

function TSWMMStandardInterfaceFile.GetNextTime: TDateTime;
{var
	CurrentPos: Int64;
	OldCurrentTime: TDateTime;
	OldCurrentTimeStep: TDateTime;
	OldFlows: TFlowsArray;
	XPRecSize: Integer;}
begin
	Result := fNextTime;
{	OldCurrentTime := fCurrentTime;
	OldCurrentTimeStep := fCurrentTimeStep;
	OldFlows := Copy(fFlows);
	if IsEOF then
		Result := MaxDateTime
	else
	begin
		ReadFlows;
		Result := CurrentTime;
		if fStream.InterfaceFormat = ifXP then
		begin
			XPRecSize := SizeOf(Integer)+SizeOf(Double)+SizeOf(Double)+
				NumInlets*SizeOf(Double);
			fStream.Stream.Seek(-2*XPRecSize, soFromCurrent);
		end
		else
		begin
			fStream.Rewind;
			fStream.Rewind;
		end;
		ReadFlows;
	end;}
end;

function TSWMMStandardInterfaceFile.GetCurrentEndTime: TDateTime;
var
	CurrentPos: Int64;
begin
	Result := CurrentTime+CurrentTimeStep/86400;
	if SecondSpan(fNextTime, Result) <1 then
		Result := fNextTime;
end;

function TSWMMStandardInterfaceFile.GetEOF: Boolean;
begin
	Result := IsEOF;
end;

procedure TSWMMStandardInterfaceFile.SetArea(Value: Double);
begin
	if fArea <> Value then
		fArea := Value;
end;

procedure TSWMMStandardInterfaceFile.SetFlowMultiplier(Value: Double);
begin
	if fFlowMultiplier <> Value then
		fFlowMultiplier := Value;
end;

procedure TSWMMStandardInterfaceFile.SetIDs(Index: Integer; Value: String);
begin
	if fIDs[Index] <> Value then
		fIDs[Index] := Value;
end;

procedure TSWMMStandardInterfaceFile.SetNumInlets(Value: Integer);
begin
	if fNumInlets <> Value then
		fNumInlets := Value;
end;

procedure TSWMMStandardInterfaceFile.SetNumPollutants(Value: Integer);
begin
	if fNumPollutants <> Value then
		fNumPollutants := Value;
end;

procedure TSWMMStandardInterfaceFile.SetSourceBlock(Value: String);
begin
	if fSourceBlock <> Value then
		fSourceBlock := Value;
end;

procedure TSWMMStandardInterfaceFile.SetStart(Value: TDateTime);
begin
	if fStart <> Value then
		fStart := Value;
end;

procedure TSWMMStandardInterfaceFile.SetTitles(Index: Integer;
	Value: String);
begin
	if fTitles[Index] <> Value then
		fTitles[Index] := Value;
end;

procedure TSWMMStandardInterfaceFile.FlushFlows;
var
	i: Integer;
begin
	for i := 0 to fNumInlets-1 do
		fFlows[i] := 0.0;
end;

procedure TSWMMStandardInterfaceFile.WriteFlowsLimitNodesIndexList(
	NodesList: TStringList; SkipInterval: Integer = 0);
var
	i: Integer;
begin
	fStream.WriteInteger(Y2KJulDateOfDateTime(fCurrentTime));
	fStream.WriteSingle(SecondsOfDayOfDateTime(fCurrentTime));
	fStream.WriteSingle(fCurrentTimeStep * (SkipInterval+1));
	for i := 0 to NodesList.Count-1 do
		if Assigned(NodesList.Objects[i]) then
			fStream.WriteSingle(fFlows[StrToInt(NodesList[i])]*
				TNodeModifier(NodesList.Objects[i]).Multiplier)
		else
			fStream.WriteSingle(fFlows[StrToInt(NodesList[i])]);
	fStream.FlushRecord;
end;

procedure TSWMMStandardInterfaceFile.WriteFlowsLimitNodesIndexList(
	AInterfaceFile: ISWMMStandardInterfaceFile; NodesList: TStringList;
	SkipInterval: Integer = 0);
var
	i: Integer;
begin
	fStream.WriteInteger(Y2KJulDateOfDateTime(AInterfaceFile.CurrentTime));
	fStream.WriteSingle(SecondsOfDayOfDateTime(AInterfaceFile.CurrentTime));
	fStream.WriteSingle(AInterfaceFile.CurrentTimeStep * (SkipInterval+1));

	for i := 0 to NodesList.Count-1 do
	begin
		if Assigned(NodesList.Objects[i]) then
			fStream.WriteSingle(AInterfaceFile.Flows[StrToInt(NodesList[i])]*
				TNodeModifier(NodesList.Objects[i]).Multiplier)
		else
			fStream.WriteSingle(AInterfaceFile.Flows[StrToInt(NodesList[i])]);
	end;

	fStream.FlushRecord;
end;

function TSWMMStandardInterfaceFile.GetInterfaceFormat: TInterfaceFormat;
begin
	Result := fStream.InterfaceFormat;
end;

function TSWMMStandardInterfaceFile.ReadByte: Byte;
begin
	Result := fStream.ReadByte;
end;

function TSWMMStandardInterfaceFile.ReadDouble: Double;
begin
	Result := fStream.ReadDouble;
end;

function TSWMMStandardInterfaceFile.ReadExtended: Extended;
begin
	Result := fStream.ReadExtended;
end;

function TSWMMStandardInterfaceFile.ReadInteger: Integer;
begin
	Result := fStream.ReadInteger;
end;

function TSWMMStandardInterfaceFile.ReadSingle: Single;
begin
	Result := fStream.ReadSingle;
end;

function TSWMMStandardInterfaceFile.ReadString(ASize: Integer): String;
begin
	Result := fStream.ReadString(ASize);
end;

procedure TSWMMStandardInterfaceFile.WriteByte(AByte: Byte);
begin
	fStream.WriteByte(AByte);
end;

procedure TSWMMStandardInterfaceFile.WriteDouble(ADouble: Double);
begin
	fStream.WriteDouble(ADouble);
end;

procedure TSWMMStandardInterfaceFile.WriteExtended(AExtended: Extended);
begin
	fStream.WriteExtended(AExtended);
end;

procedure TSWMMStandardInterfaceFile.WriteInteger(AInteger: Integer);
begin
	fStream.WriteInteger(AInteger);
end;

procedure TSWMMStandardInterfaceFile.WriteSingle(ASingle: Single);
begin
	fStream.WriteSingle(ASingle);
end;

procedure TSWMMStandardInterfaceFile.WriteString(AString: String);
begin
  fStream.WriteString(AString);
end;

procedure TSWMMStandardInterfaceFile.FlushRecord;
begin
	fStream.FlushRecord;
end;

function TSWMMStandardInterfaceFile.GetFlowUnits(
  Index: Integer): TConvType;
begin
  Result := vuCubicFeet;
end;

function TSWMMStandardInterfaceFile.GetToIDs(Index: Integer): String;
begin
  Result := IDs[Index];
end;

{ TF32InterfaceFileStream }

procedure TF32InterfaceFileStream.CheckRecordSize;
var
	SkipBytes: Integer;
begin
	if IsEndOfRecord then
	begin
		SkipBytes := 1;
		if (fCurrentRecSize > 63) then
			Inc(SkipBytes);
		if (fCurrentRecSize > 16383) then
			Inc(SkipBytes);
		if (fCurrentRecSize > 4194303) then
			Inc(SkipBytes);
		fStream.Seek(SkipBytes, soFromCurrent);
		if not IsEOF then
			fCurrentRecSize := ReadRecordSize // Read current record's size
		else
		begin
			fCurrentRecSize := 0;
			fRecordSizeSize := 0;
		end;
		fPosInRec := 0;
	end;
end;

procedure TF32InterfaceFileStream.CheckSignature;
var
	CurrentPos: Integer;
	TestByte: Byte;
begin
	Assert(fStream.Position = 0, 'Stream not positioned at the beginning for signature check');
	// Check for F32 signature
	TestByte := ReadByte;
	if TestByte <> $FD then
		raise Exception.Create('not F32 Format');
end;

constructor TF32InterfaceFileStream.Create(AFile: String; Mode: Word = fmOpenRead);
begin
	inherited;
end;

destructor TF32InterfaceFileStream.Destroy;
begin
	inherited;
end;

procedure TF32InterfaceFileStream.FlushRecord;
var
	ABuf: String;
begin
	inherited;
	// Write BOR size
	WriteRecordSize(fCurrentWriteRecordSize);
	// Write record
	fCurrentWriteRecordStream.Position := 0;
	fStream.Write(fCurrentWriteRecordStream.Memory^, fCurrentWriteRecordSize);
	// Write EOR size
	WriteReverseRecordSize(fCurrentWriteRecordSize);

	fCurrentWriteRecordStream.Clear;
	fCurrentWriteRecordSize := 0;
end;

function TF32InterfaceFileStream.GetInterfaceFormat: TInterfaceFormat;
begin
	Result := if32;
end;

function TF32InterfaceFileStream.ReadRecordSize: Integer;
var
	Buf: Byte;
	BufAdd: Byte;
	AddInteger: Integer;
	BytesToRead: Byte;
	i: Byte;
begin
	// Forward read

	// BOR record, 1st byte: left 6 bits are least significant bits of record length;
	// right 2 bits indicate how many more bytes to read, each succeeding byte is
	// more significant

	if IsEOF then
		Result := 0
	else
	begin
		fStream.Read(Buf, SizeOf(Byte));
		BytesToRead := Buf and 3;
		Result := 0;
		for i := 1 to BytesToRead do
		begin
			fStream.Read(BufAdd, SizeOf(Byte));
			AddInteger := BufAdd;
			Result := Result + AddInteger shl (6 + (i-1)*8);
		end;
		fRecordSizeSize := BytesToRead+1;
		Result := Result + (Buf and 252) shr 2
	end;
end;

procedure TF32InterfaceFileStream.Rewind;
var
	PrevRecSize: Integer;
	Buf: Byte;
	BufAdd: Byte;
	BytesToRead: Byte;
	i: Byte;
begin
	if fPosInRec > 0 then
		fStream.Seek(-fPosInRec, soFromCurrent)
	else
	begin // we're at the beginning of a record
		fStream.Seek(-SizeOf(Byte)*fRecordSizeSize, soFromCurrent);
		fStream.Seek(-SizeOf(Byte), soFromCurrent);
		fStream.Read(Buf, SizeOf(Byte));
		BytesToRead := Buf and 3;
		PrevRecSize := (Buf and 252) shr 2;
		for i := 1 to BytesToRead do
		begin
			fStream.Seek(-2*SizeOf(Byte), soFromCurrent);
			fStream.Read(BufAdd, SizeOf(Byte));
			PrevRecSize := PrevRecSize + BufAdd shl (6 + (i-1)*8);
		end;
		fStream.Seek(-SizeOf(Byte), soFromCurrent);
		fStream.Seek(-PrevRecSize, soFromCurrent);
		fCurrentRecSize := ReadRecordSize;
	end;
	fPosInRec := 0;
end;

procedure TF32InterfaceFileStream.WriteByte(AByte: Byte);
begin
//	inherited;
	Inc(fCurrentWriteRecordSize, SizeOf(AByte));
	fCurrentWriteRecordStream.Write(AByte, SizeOf(AByte));
end;

procedure TF32InterfaceFileStream.WriteDouble(ADouble: Double);
begin
//	inherited;
	Inc(fCurrentWriteRecordSize, SizeOf(ADouble));
	fCurrentWriteRecordStream.Write(ADouble, SizeOf(ADouble));
end;

procedure TF32InterfaceFileStream.WriteExtended(AExtended: Extended);
begin
//	inherited;
	Inc(fCurrentWriteRecordSize, SizeOf(AExtended));
	fCurrentWriteRecordStream.Write(AExtended, SizeOf(AExtended));
end;

procedure TF32InterfaceFileStream.WriteInteger(AInteger: Integer);
//var
//	AWord: Word;
begin
//	inherited;
	//AWord := AInteger;
	Inc(fCurrentWriteRecordSize, SizeOf(AInteger));
	fCurrentWriteRecordStream.Write(AInteger, SizeOf(AInteger));
end;

procedure TF32InterfaceFileStream.WriteRecordSize(ASize: Integer);
var
	BORRecordSize: array[0..3] of Byte;
begin
	if fCurrentWriteRecordSize <= 63 then
	begin
		BORRecordSize[0] := 0 + fCurrentWriteRecordSize shl 2;
		fStream.Write(BORRecordSize[0], SizeOf(Byte))
	end
	else if fCurrentWriteRecordSize <= 16383 then
	begin
		BORRecordSize[0] := 1 + (fCurrentWriteRecordSize and 63) shl 2;
		fStream.Write(BORRecordSize[0], SizeOf(Byte));
		BORRecordSize[1] := (fCurrentWriteRecordSize and 16320) shr 6;
		fStream.Write(BORRecordSize[1], SizeOf(Byte));
	end
	else if fCurrentWriteRecordSize <= 4194305 then
	begin
		BORRecordSize[0] := 2 +  (fCurrentWriteRecordSize and 63) shl 2;
		fStream.Write(BORRecordSize[0], SizeOf(Byte));
		BORRecordSize[1] := (fCurrentWriteRecordSize and 16320) shr 6;
		fStream.Write(BORRecordSize[1], SizeOf(Byte));
		BORRecordSize[2] := (fCurrentWriteRecordSize and 4177920) shr 8;
		BORRecordSize[2] := BORRecordSize[2] shr 6;
		fStream.Write(BORRecordSize[2], SizeOf(Byte));
	end
	else if fCurrentWriteRecordSize <= 10737418233 then
	begin
		BORRecordSize[0] := 3 + (fCurrentWriteRecordSize and 252) shl 2;
		fStream.Write(BORRecordSize[0], SizeOf(Byte));
		BORRecordSize[1] := (fCurrentWriteRecordSize and 16320) shr 6;
		fStream.Write(BORRecordSize[1], SizeOf(Byte));
		BORRecordSize[2] := (fCurrentWriteRecordSize and 4177920) shr 8;
		BORRecordSize[2] := BORRecordSize[2] shr 6;
		fStream.Write(BORRecordSize[2], SizeOf(Byte));
		BORRecordSize[3] := (fCurrentWriteRecordSize and 1069547520) shr 8;
		BORRecordSize[3] := BORRecordSize[3] shr 8;
		BORRecordSize[3] := BORRecordSize[3] shr 6;
		fStream.Write(BORRecordSize[3], SizeOf(Byte));
	end;
end;

procedure TF32InterfaceFileStream.WriteReverseRecordSize(ASize: Integer);
var
	BORRecordSize: array[0..3] of Byte;
begin
	if fCurrentWriteRecordSize <= 63 then
	begin
		BORRecordSize[0] := 0 + (1+fCurrentWriteRecordSize) shl 2;
		fStream.Write(BORRecordSize[0], SizeOf(Byte))
	end
	else if fCurrentWriteRecordSize <= 16383 then
	begin
		BORRecordSize[0] := 1 + ((2+fCurrentWriteRecordSize) and 63) shl 2;
		BORRecordSize[1] := ((2+fCurrentWriteRecordSize) and 16320) shr 6;
		fStream.Write(BORRecordSize[1], SizeOf(Byte));
		fStream.Write(BORRecordSize[0], SizeOf(Byte));
	end
	else if fCurrentWriteRecordSize <= 4194305 then
	begin
		BORRecordSize[0] := 2 +  ((3+fCurrentWriteRecordSize) and 63) shl 2;
		BORRecordSize[1] := ((3+fCurrentWriteRecordSize) and 16320) shr 6;
		BORRecordSize[2] := ((3+fCurrentWriteRecordSize) and 4177920) shr 8;
		BORRecordSize[2] := BORRecordSize[2] shr 6;
		fStream.Write(BORRecordSize[2], SizeOf(Byte));
		fStream.Write(BORRecordSize[1], SizeOf(Byte));
		fStream.Write(BORRecordSize[0], SizeOf(Byte));
	end
	else if fCurrentWriteRecordSize <= 10737418233 then
	begin
		BORRecordSize[0] := 3 + ((4+fCurrentWriteRecordSize) and 252) shl 2;
		BORRecordSize[1] := ((4+fCurrentWriteRecordSize) and 16320) shr 6;
		BORRecordSize[2] := ((4+fCurrentWriteRecordSize) and 4177920) shr 8;
		BORRecordSize[2] := BORRecordSize[2] shr 6;
		BORRecordSize[3] := ((4+fCurrentWriteRecordSize) and 1069547520) shr 8;
		BORRecordSize[3] := BORRecordSize[3] shr 8;
		BORRecordSize[3] := BORRecordSize[3] shr 6;
		fStream.Write(BORRecordSize[3], SizeOf(Byte));
		fStream.Write(BORRecordSize[2], SizeOf(Byte));
		fStream.Write(BORRecordSize[1], SizeOf(Byte));
		fStream.Write(BORRecordSize[0], SizeOf(Byte));
	end;
end;

procedure TF32InterfaceFileStream.WriteSignature;
var
	ASignature: Byte;
begin
	inherited;
	ASignature := $FD;
	fStream.Write(ASignature, SizeOf(ASignature));
end;

procedure TF32InterfaceFileStream.WriteSingle(ASingle: Single);
begin
//	inherited;
	Inc(fCurrentWriteRecordSize, SizeOf(ASingle));
	fCurrentWriteRecordStream.Write(ASingle, SizeOf(ASingle));
end;

procedure TF32InterfaceFileStream.WriteString(AString: String);
begin
//	inherited;
	Inc(fCurrentWriteRecordSize, Length(AString));
	fCurrentWriteRecordStream.Write(AString[1], Length(AString));
end;


{ TXPInterfaceFileStream }

constructor TXPInterfaceFileStream.Create(AFile: String; Mode: Word);
begin
	inherited;
end;

destructor TXPInterfaceFileStream.Destroy;
begin
	inherited;
end;

procedure TXPInterfaceFileStream.FlushRecord;
begin
	inherited;
end;

function TXPInterfaceFileStream.GetInterfaceFormat: TInterfaceFormat;
begin
	Result := ifXP;
end;

function TXPInterfaceFileStream.GetRecordSize: Integer;
begin
	Result := -1;
end;

function TXPInterfaceFileStream.ReadRecordSize: Integer;
begin
	inherited
end;

procedure TXPInterfaceFileStream.WriteByte(AByte: Byte);
begin
	inherited;
end;

procedure TXPInterfaceFileStream.WriteDouble(ADouble: Double);
begin
	inherited;
end;

procedure TXPInterfaceFileStream.WriteExtended(AExtended: Extended);
begin
	inherited;
end;

procedure TXPInterfaceFileStream.WriteInteger(AInteger: Integer);
begin
	inherited;
end;

procedure TXPInterfaceFileStream.WriteRecordSize(ASize: Integer);
begin
	inherited;
end;

procedure TXPInterfaceFileStream.WriteSingle(ASingle: Single);
var
	ADouble: Double;
begin
//	inherited;
	ADouble := ASingle;
	fStream.Write(ADouble, SizeOf(ADouble));
end;

procedure TXPInterfaceFileStream.WriteString(AString: String);
begin
	inherited;
end;

{ TXP_SYF_FileStream }

constructor TXP_SYF_FileStream.Create(AFile: String; Mode: Word);
begin
	fFile := TFileStream.Create(AFile, Mode);
	fStream := TStBufferedStream.Create(fFile);
	ReadHeader;
end;

destructor TXP_SYF_FileStream.Destroy;
begin
	fStream.Free;
	fFile.Free;
	inherited;
end;

procedure TXP_SYF_FileStream.FlushFlows;
var
	i: Integer;
begin
	for i := 0 to NumLinks-1 do
		fLinks[i].Flow := 0;
end;

function TXP_SYF_FileStream.GetCurrentEndTime: TDateTime;
begin
	if IsEOF then
		Result := CurrentTime + SYFTimeStep/86400
	else
		Result := NextTime;
end;

function TXP_SYF_FileStream.GetCurrentTime: TDateTime;
var
	Days: Single;
	Hours: Single;
	RoundedDays: Word;
	RoundedHours: Word;
begin
	Days := Trunc((fCycle*fTimeStep)/86400);
	Hours := Trunc(((fCycle*fTimeStep)-(Days*86400))/3600);
	RoundedDays := Round(Days);
	RoundedHours := Round(Hours);
	Result := EncodeDateTime(1900, 1, 1, RoundedHours, fMinute, fSecond, 0);
	Result := IncDay(Result, RoundedDays);
end;

function TXP_SYF_FileStream.GetCurrentTimeCycle: Integer;
begin
	Result := fCycle;
end;

function TXP_SYF_FileStream.GetEOF: Boolean;
begin
	Result := IsEOF;
end;

function TXP_SYF_FileStream.GetFilePosition: Int64;
begin
	Result := fStream.Position;
end;

function TXP_SYF_FileStream.GetFileSize: Int64;
begin
	Result := fStream.FastSize;
end;

function TXP_SYF_FileStream.GetFlow(AIndex: Integer): Double;
begin
	Result := Links[AIndex].Flow;
end;

function TXP_SYF_FileStream.GetFlowRecordSize: Integer;
begin
	Result :=
		SizeOf(Integer)+              // Cycle
		SizeOf(Integer)+              // Hour
		SizeOf(Integer)+              // Minute
		SizeOf(Integer)+              // Second
		SizeOf(Single)*fNumJunctions+ // Depths
		SizeOf(Single)*fNumJunctions+ // Inverts
		SizeOf(Single)*fNumLinks+     // Flows
		SizeOf(Single)*fNumLinks*2+   // US/DS depths
    SizeOf(Single)*fNumConduits;  // Velocities
end;

function TXP_SYF_FileStream.GetID(AIndex: Integer): String;
begin
  Result := Links[AIndex].ID;
end;

function TXP_SYF_FileStream.GetInitialTime: TDateTime;
begin
	Result := fInitialTime;
end;

function TXP_SYF_FileStream.GetIsMetric: Boolean;
begin
	Result := fMetric <> 0;
end;

function TXP_SYF_FileStream.GetJCE: Integer;
begin
	Result := fJCE;
end;

function TXP_SYF_FileStream.GetLink(Index: Integer): TSYFLink;
begin
	if Index > Length(fLinks) then
		raise EListError.CreateFmt('Link Index %d out of range (0..%d)',
			[Index, Length(fLinks)])
	else
		Result := fLinks[Index];
end;

function TXP_SYF_FileStream.GetNextTime: TDateTime;
begin
	if IsEOF then
		Result := MaxDateTime
	else
		Result := CurrentTime + SYFTimeStep/86400
end;

function TXP_SYF_FileStream.GetNode(Index: Integer): TSYFNode;
begin
	if Index > Length(fNodes) then
		raise EListError.CreateFmt('Node Index %d out of range (0..%d)',
			[Index, Length(fNodes)])
	else
		Result := fNodes[Index];
end;

function TXP_SYF_FileStream.GetNumConduits: Integer;
begin
	Result := fNumConduits;
end;

function TXP_SYF_FileStream.GetNumCycles: Integer;
begin
	Result := fNumCycles;
end;

function TXP_SYF_FileStream.GetNumFlows: Integer;
begin
  Result := fNumLinks;
end;

function TXP_SYF_FileStream.GetNumJunctions: Integer;
begin
	Result := fNumJunctions;
end;

function TXP_SYF_FileStream.GetNumLinks: Integer;
begin
	Result := fNumLinks;
end;

function TXP_SYF_FileStream.GetNumOrifices: Integer;
begin
	Result := fNumOrifices;
end;

function TXP_SYF_FileStream.GetNumPumps: Integer;
begin
	Result := fNumPumps;
end;

function TXP_SYF_FileStream.GetNumWeirs: Integer;
begin
	Result := fNumWeirs;
end;

function TXP_SYF_FileStream.GetSYFTimeStep: Single;
begin
	Result := fSYFTimeStep;
end;

function TXP_SYF_FileStream.GetTimeStep: Single;
begin
	Result := fTimeStep;
end;

function TXP_SYF_FileStream.IsEOF: Boolean;
var
	RecSize: Int64;
	CurrentPos: Int64;
begin
	RecSize := SizeOf(Integer)+SizeOf(Integer)+SizeOf(Integer)+SizeOf(Integer)+
		fNumJunctions*SizeOf(Single)+fNumJunctions*SizeOf(Single)+
		fNumLinks*SizeOf(Single)+2*fNumLinks*SizeOf(Single)+
		fNumConduits*SizeOf(Single);
		CurrentPos := fBeginFlowsPos+((fNumCycles div fCycleStep) - 1)*RecSize;
	Result := fStream.Position >= CurrentPos;
end;

procedure TXP_SYF_FileStream.MoveToStartOfFlows;
begin
	fStream.Position := fBeginFlowsPos;
end;

function TXP_SYF_FileStream.ReadByte: Byte;
var
	Buf: Integer;
begin
	fStream.Read(Buf, SizeOf(Integer));
	Result := Buf;
end;

function TXP_SYF_FileStream.ReadDouble: Double;
var
	Buf: Double;
begin
	fStream.Read(Buf, SizeOf(Double));
	Result := Buf;
end;

function TXP_SYF_FileStream.ReadExtended: Extended;
var
	Buf: Extended;
	ExtraBuf: Byte;
	NumBytesLeft: Integer;
	i: Integer;
begin
	fStream.Read(Buf, SizeOf(Extended));
	Result := Buf;
	NumBytesLeft := SizeOf(Extended) mod SYFRecSize;
	for i := 1 to NumBytesLeft do
		fStream.Read(Buf, SizeOf(ExtraBuf));
end;

procedure TXP_SYF_FileStream.ReadFlows;
var
	i: Integer;
begin
	fCycle := ReadInteger;
	fHour := ReadInteger;
	fMinute := ReadInteger;
	fSecond := ReadInteger;
	for i := 1 to fNumJunctions do
		fNodes[i-1].Depth := ReadSingle;
	for i := 1 to fNumJunctions do
		fNodes[i-1].Invert := ReadSingle;
	for i := 1 to fNumLinks do
		fLinks[i-1].Flow := ReadSingle;
	for i := 1 to fNumLinks do
	begin
		fLinks[i-1].USDepth := ReadSingle;
		fLinks[i-1].DSDepth := ReadSingle;
	end;
	for i := 1 to fNumConduits do
	begin
		fLinks[i-1].Velocity := ReadSingle;
	end;
end;

procedure TXP_SYF_FileStream.ReadHeader;
var
	i: Integer;
	FirstCycle: Integer;
	SecondCycle: Integer;
	Days: Word;
	Hours: Word;
begin
	fJCE := ReadInteger;
	fMetric := ReadInteger;
	fNumCycles := ReadInteger;
	fNumJunctions := ReadInteger;
	fNumLinks := ReadInteger;
	fNumConduits := ReadInteger;
	fNumPumps := ReadInteger;
	fNumOrifices := ReadInteger;
	fNumWeirs := ReadInteger;
	fTimeStep := ReadSingle;

	SetLength(fNodes, fNumJunctions);
	for i := 1 to fNumJunctions do
		fNodes[i-1].ID := Trim(ReadString(12));

	SetLength(FLinks, fNumLinks);
	for i := 1 to fNumLinks do
		fLinks[i-1].ID := Trim(ReadString(12));

	for i := 1 to fNumLinks do
	begin
		fLinks[i-1].USNode := Trim(ReadString(12));
		fLinks[i-1].DSNode := Trim(ReadString(12));
		fLinks[i-1].USNodeIndex := ReadInteger;
		fLinks[i-1].DSNodeIndex := ReadInteger;
	end;

	for i := 1 to fNumJunctions do
		fNodes[i-1].GroundElev := ReadSingle;

	for i := 1 to fNumJunctions do
		fNodes[i-1].CrownElev := ReadSingle;

	for i := 1 to fNumJunctions do
		fNodes[i-1].InitElev := ReadSingle;

	for i := 1 to fNumJunctions do
		fNodes[i-1].InvertElev := ReadSingle;

	for i := 1 to fNumLinks do
		fLinks[i-1].InitFlow := ReadSingle;

	for i := 1 to fNumConduits do
		fLinks[i-1].InitVelocity := ReadSingle;

	for i := 1 to fNumConduits do
		fLinks[i-1].InitXSecArea := ReadSingle;

	for i := 1 to fNumConduits do
		fLinks[i-1].InitDepth := ReadSingle;

	for i := 1 to fNumConduits do
	begin
		fLinks[i-1].InitUSDepth := ReadSingle;
		fLinks[i-1].InitDSDepth := ReadSingle;
	end;
	fBeginFlowsPos := fStream.Position;

	ReadFlows;
	FirstCycle := CurrentTimeCycle;

	Days := fHour div 24;
	Hours := fHour mod 24;
	fInitialTime := EncodeDateTime(1900, 1, Days+1, Hours, fMinute, fSecond, 0);

	ReadFlows;
	SecondCycle := CurrentTimeCycle;
	fCycleStep := SecondCycle - FirstCycle;
	fSYFTimeStep := fCycleStep*fTimeStep;

	MoveToStartOfFlows;
end;

function TXP_SYF_FileStream.ReadInteger: Integer;
var
	Buf: Integer;
begin
	fStream.Read(Buf, SizeOf(Integer));
	Result := Buf;
end;

function TXP_SYF_FileStream.ReadSingle: Single;
var
	Buf: Single;
begin
	fStream.Read(Buf, SizeOf(Single));
	Result := Buf;
end;

function TXP_SYF_FileStream.ReadString(ASize: Integer): String;
var
	Buf: String;
	ExtraBuf: Byte;
	NumBytesLeft: Integer;
	i: Integer;
begin
	SetLength(Buf, ASize);
	fStream.Read(Buf[1], ASize);
	Result := Buf;
	NumBytesLeft := ASize mod SYFRecSize;
	for i := 1 to NumBytesLeft do
		fStream.Read(ExtraBuf, SizeOf(Byte));
end;

procedure TXP_SYF_FileStream.WriteByte(AByte: Byte);
var
	AInteger: Integer;
begin
	AInteger := AByte;
	fStream.Write(AInteger, SizeOf(Integer));
end;

procedure TXP_SYF_FileStream.WriteDouble(ADouble: Double);
begin
	fStream.Write(ADouble, SizeOf(Double));
end;

procedure TXP_SYF_FileStream.WriteExtended(AExtended: Extended);
var
	Buf: String;
	i: Integer;
	ExtraBuf: Byte;
	NumBytesLeft: Integer;
begin
	fStream.Write(AExtended, SizeOf(Extended));
	NumBytesLeft := SizeOf(Extended) mod SYFRecSize;
	ExtraBuf := 0;
	for i := 1 to NumBytesLeft do
		fStream.Write(ExtraBuf, SizeOf(Byte));
end;

procedure TXP_SYF_FileStream.WriteInteger(AInteger: Integer);
begin
	fStream.Write(AInteger, SizeOf(Integer));
end;

procedure TXP_SYF_FileStream.WriteSingle(ASingle: Single);
begin
	fStream.Write(ASingle, SizeOf(Single));
end;

procedure TXP_SYF_FileStream.WriteString(AString: String);
var
	i: Integer;
	ExtraBuf: Byte;
	NumBytesLeft: Integer;
begin
	fStream.Write(AString[1], Length(AString));
	NumBytesLeft := SizeOf(Extended) mod SYFRecSize;
	ExtraBuf := 0;
	for i := 1 to NumBytesLeft do
		fStream.Write(ExtraBuf, SizeOf(Byte));
end;

{ TPDXTextStream }

constructor TPDXTextStream.Create(AFile: String; Mode: Word);
begin
	fFile := TFileStream.Create(AFile, Mode);
	fTextStream := TStAnsiTextStream.Create(fFile);
	ReadHeader;
end;

destructor TPDXTextStream.Destroy;
begin
	fTextStream.Free;
	fFile.Free;
	inherited;
end;

procedure TPDXTextStream.FlushFlows;
var
	i: Integer;
begin
	for i := 0 to Length(fLinks)-1 do
		fLinks[i].Flow := 0;
end;

function TPDXTextStream.GetCurrentEndTime: TDateTime;
begin
	Result := IncSecond(fCurrentTime, Round(fTimeStep));
	Result := IncMillisecond(Result, Round((fTimeStep-Trunc(fTimeStep))*1000));
end;

function TPDXTextStream.GetCurrentTime: TDateTime;
begin
	Result := fCurrentTime;
end;

function TPDXTextStream.GetEOF: Boolean;
begin
	Result := fEndOfFile;
end;

function TPDXTextStream.GetFilePosition: Int64;
begin
	Result := fTextStream.Position;
end;

function TPDXTextStream.GetFileSize: Int64;
begin
	Result := fTextStream.FastSize;
end;

function TPDXTextStream.GetFlow(AIndex: Integer): Double;
begin
	Result := fLinks[AIndex].Flow;
end;

function TPDXTextStream.GetID(AIndex: Integer): String;
begin
	Result := fLinks[AIndex].ID;
end;

function TPDXTextStream.GetInitialTime: TDateTime;
begin
	Result := fInitialTime;
end;

function TPDXTextStream.GetNextTime: TDateTime;
begin
	if not EOF then
	begin
		Result := IncSecond(fCurrentTime, Round(fTimeStep));
		Result := IncMillisecond(Result, Round((fTimeStep-Trunc(fTimeStep))*1000));
	end
	else
		Result := MaxDateTime;
end;

function TPDXTextStream.GetNumFlows: Integer;
begin
	Result := Length(fLinks);
end;

procedure TPDXTextStream.MoveToStartOfFlows;
begin
	fTextStream.Position := fBeginFlowsPos;
	fAtBeginningOfFlows := True;
	fEndOfFile := False;
end;

procedure TPDXTextStream.ReadFlows;
var
	i: Integer;
	CurrentLine: String;
begin
	while not fTextStream.AtEndOfStream do
	begin
		if CurrentLine = '   CONDUIT/       FLOW   ===> "*" CONDUIT USES THE NORMAL FLOW OPTION.' then
		begin
			CurrentLine := fTextStream.ReadLine;
			if fAtBeginningOfFlows then
			begin
				fCurrentTime := fInitialTime;
				fAtBeginningOfFlows := False;
			end
			else
			begin
				fCurrentTime := IncSecond(fCurrentTime, Round(fTimeStep));
				fCurrentTime := IncMillisecond(fCurrentTime, Round((fTimeStep-Trunc(fTimeStep))*1000));
			end;
			for i := 1 to Length(fLinks) do
			begin
				fLinks[i-1].Flow := StrToFloat(Copy(CurrentLine, ((i-1) mod 4)*23+11+1, 11));
				if ((i mod 4) = 0) then
					CurrentLine := fTextStream.ReadLine;
			end;
			Break;
		end;

		if CurrentLine = ' *     FINAL MODEL CONDITION     *' then
		begin
{			while not fTextStream.AtEndOfStream do
			begin
				if CurrentLine = '   CONDUIT/       FLOW   ===> "*" CONDUIT USES THE NORMAL FLOW OPTION.' then
				begin
					CurrentLine := fTextStream.ReadLine;
					fCurrentTime := IncSecond(fCurrentTime, Round(fTimeStep));
					fCurrentTime := IncMillisecond(fCurrentTime, Round((fTimeStep-Trunc(fTimeStep))*1000));
					CodeSite.SendDateTime('CurrentTime', fCurrentTime);
					CodeSite.SendMsg(csmRed, 'Reading final model condition');
					for i := 1 to Length(fLinks) do
					begin
						fLinks[i-1].Flow := StrToFloat(Copy(CurrentLine, ((i-1) mod 4)*23+11+1, 11));
						if ((i mod 4) = 0) then
							CurrentLine := fTextStream.ReadLine;
					end;
					Break;
					fEndOfFile := True;
				end;
				CurrentLine := fTextStream.ReadLine;
			end;}
			//CodeSite.SendMsg(csmRed, 'Reading final model condition');
			fEndOfFile := True;
			Break;
		end;
		CurrentLine := fTextStream.ReadLine;
	end;
end;

procedure TPDXTextStream.ReadHeader;
var
	CurrentLine: String;
	i: Integer;
	ModelTimeStep: Single;
	InterfaceStartDate: Integer;
	InterfaceStartTime: Single;
	AtEndOfHeader: Boolean;

	procedure SkipLines(AStream: TStAnsiTextStream; NumLines: Integer);
	var
		LineCount: Integer;
	begin
		for LineCount := 1 to NumLines do
			AStream.ReadLine;
	end;

begin
	CurrentLine := fTextStream.ReadLine;
	AtEndOfHeader := False;
	while (not fTextStream.AtEndOfStream) and (not AtEndOfHeader) do
	begin
		// Read time step for intermediate output
		if CurrentLine = ' Control information for simulation' then
		begin
			SkipLines(fTextStream, 4);
			CurrentLine := fTextStream.ReadLine;
			ModelTimeStep := StrToFloat(Copy(CurrentLine, 37, 8));
			SkipLines(fTextStream, 9);
			CurrentLine := fTextStream.ReadLine;
			fTimeStep := ModelTimeStep * StrToFloat(Copy(CurrentLine, 37, 8));
		end;

		// Read in conduits
		if CurrentLine = ' *                     Conduit Data                  *' then
		begin
			SkipLines(fTextStream, 5);
			CurrentLine := fTextStream.ReadLine;

			while not fTextStream.AtEndOfStream do
			begin

				// Read in block of conduits
				while CurrentLine <> '' do
				begin
					SetLength(fLinks, Length(fLinks)+1);
					fLinks[Length(fLinks)-1].ID := Trim(Copy(CurrentLine, 5, 10));
					CurrentLine := fTextStream.ReadLine;
				end;

				SkipLines(fTextStream, 11);
				CurrentLine := fTextStream.ReadLine;
				if CurrentLine <> ' *                     Conduit Data                  *' then
					Break
				else
				begin
					SkipLines(fTextStream, 5);
					CurrentLine := fTextStream.ReadLine;
				end;

			end;
		end;

		// Read in auto-created conduits
		if CurrentLine =  ' *        INTERNAL CONNECTIVITY INFORMATION       *' then
		begin
			SkipLines(fTextStream, 4);
			CurrentLine := fTextStream.ReadLine;
			while CurrentLine <> '1' do
			begin
				SetLength(fLinks, Length(fLinks)+1);
				fLinks[Length(fLinks)-1].ID := Trim(Copy(CurrentLine, 11, 8));
				CurrentLine := fTextStream.ReadLine;
			end;
		end;

		// Read in incoming interface info
		if CurrentLine = ' # Header information from interface file: #' then
		begin
			SkipLines(fTextStream, 11);
			CurrentLine := fTextStream.ReadLine;
			InterfaceStartDate := StrToInt(Copy(CurrentLine, 52, 8));
			CurrentLine := fTextStream.ReadLine;
			InterfaceStartTime := StrToFloat(Copy(CurrentLine, 52, 8));
			fInitialTime := DateTimeOfJulDate(InterfaceStartDate, InterfaceStartTime);
		end;

		// Read in initial condition
		if CurrentLine = ' *    INITIAL MODEL CONDITION      *' then
		begin
			fBeginFlowsPos := fTextStream.Position;
			fCurrentTime := fInitialTime;
			CurrentLine := fTextStream.ReadLine;
			while not fTextStream.AtEndOfStream do
			begin
				if CurrentLine = '   CONDUIT/       FLOW   ===> "*" CONDUIT USES THE NORMAL FLOW OPTION.' then
				begin
					CurrentLine := fTextStream.ReadLine;
					for i := 1 to Length(fLinks) do
					begin
						fLinks[i-1].Flow := StrToFloat(Copy(CurrentLine, ((i-1) mod 4)*23+11+1, 11));
						if ((i mod 4) = 0) then
							CurrentLine := fTextStream.ReadLine;
					end;
					AtEndOfHeader := True;
					fAtBeginningOfFlows := False;
					Break;
				end;
				CurrentLine := fTextStream.ReadLine;
			end;
		end;

		CurrentLine := fTextStream.ReadLine;
	end;
end;

{ TTextInterfaceFileStream }

procedure TTextInterfaceFileStream.CheckCurrentLineTokens;
begin
	if fCurrentTokenIndex = fTokens.Count then
	begin
		fCurrentLine := fTextFile.ReadLine;
		ExtractTokensL(fCurrentLine, ',', '''', True, fTokens);
		fCurrentTokenIndex := 0;
	end;
end;

procedure TTextInterfaceFileStream.CheckRecordSize;
begin
//	inherited;
end;

procedure TTextInterfaceFileStream.CheckSignature;
begin
//	inherited;
end;

constructor TTextInterfaceFileStream.Create(AFile: String; Mode: Word);
begin
	fFile := TFileStream.Create(AFile, Mode);
	fTextFile := TStAnsiTextStream.Create(fFile);
 	fStream := fTextFile;
	fTokens := TStringList.Create;
end;

destructor TTextInterfaceFileStream.Destroy;
begin
	fTextFile.Free;
	fStream := nil;
	fFile.Free;
	fTokens.Free;
	inherited;
end;

procedure TTextInterfaceFileStream.FlushRecord;
var
	LastChar: String;
begin
	inherited;
	LastChar := RightStr(fCurrentWriteRecordStream, 1);
	if (Length(fCurrentWriteRecordStream) > 1) and (LastChar = ',') then
		fCurrentWriteRecordStream := LeftStr(fCurrentWriteRecordStream,
			Length(fCurrentWriteRecordStream)-1);
	fTextFile.WriteLine(fCurrentWriteRecordStream);
	fCurrentWriteRecordStream := '';
	fCurrentWriteRecordSize := 0;
end;

function TTextInterfaceFileStream.GetInterfaceFormat: TInterfaceFormat;
begin
	Result := ifText;
end;

function TTextInterfaceFileStream.GetRecordSize: Integer;
begin
	Result := Length(fCurrentLine);
end;

procedure TTextInterfaceFileStream.GoToStartOfFile;
begin
	fTextFile.SeekLine(0);
	fCurrentLine := fTextFile.ReadLine;
	ExtractTokensL(fCurrentLine, ',', '''', True, fTokens);
	fCurrentTokenIndex := 0;
end;

function TTextInterfaceFileStream.IsEndOfRecord: Boolean;
begin
	Result := (fCurrentTokenIndex = fTokens.Count);
end;

function TTextInterfaceFileStream.IsEOF: Boolean;
begin
	Result := fTextFile.AtEndOfStream and IsEndOfRecord;
end;

function TTextInterfaceFileStream.IsStartOfRecord: Boolean;
begin
	Result := (fCurrentTokenIndex = 0);
end;

function TTextInterfaceFileStream.ReadByte: Byte;
begin
	CheckCurrentLineTokens;
	Result := Byte(StrToInt(fTokens[fCurrentTokenIndex]));
	Inc(fCurrentTokenIndex);
end;

function TTextInterfaceFileStream.ReadDouble: Double;
begin
	CheckCurrentLineTokens;
	Result := StrToFloat(fTokens[fCurrentTokenIndex]);
	Inc(fCurrentTokenIndex);
end;

function TTextInterfaceFileStream.ReadExtended: Extended;
begin
	CheckCurrentLineTokens;
	Result := StrToFloat(fTokens[fCurrentTokenIndex]);
	Inc(fCurrentTokenIndex);
end;

function TTextInterfaceFileStream.ReadInteger: Integer;
begin
	CheckCurrentLineTokens;
	Result := StrToInt(fTokens[fCurrentTokenIndex]);
	Inc(fCurrentTokenIndex);
end;

function TTextInterfaceFileStream.ReadRecordSize: Integer;
begin
//
end;

function TTextInterfaceFileStream.ReadSingle: Single;
begin
	CheckCurrentLineTokens;
	Result := StrToFloat(fTokens[fCurrentTokenIndex]);
	Inc(fCurrentTokenIndex);
end;

function TTextInterfaceFileStream.ReadString(ASize: Integer): String;
begin
	CheckCurrentLineTokens;
	Result := fTokens[fCurrentTokenIndex];
	Inc(fCurrentTokenIndex);
end;

procedure TTextInterfaceFileStream.Rewind;
var
  CurPos: Int64;
  CurLine: Int64;
begin
	fTextFile.SeekNearestLine(fTextFile.Position);
  fCurrentTokenIndex := 0;
end;

procedure TTextInterfaceFileStream.WriteByte(AByte: Byte);
begin
//  inherited;
	Inc(fCurrentWriteRecordSize, Length(IntToStr(AByte))+1);
	fCurrentWriteRecordStream := fCurrentWriteRecordStream + IntToStr(AByte) +',';
end;

procedure TTextInterfaceFileStream.WriteDouble(ADouble: Double);
begin
//  inherited;
	Inc(fCurrentWriteRecordSize, Length(FloatToStrF(ADouble,ffFixed,15,4))+1);
	fCurrentWriteRecordStream := fCurrentWriteRecordStream + FloatToStrF(ADouble,ffFixed,15,4) + ',';
end;

procedure TTextInterfaceFileStream.WriteExtended(AExtended: Extended);
begin
//	inherited;
	Inc(fCurrentWriteRecordSize, Length(FloatToStrF(AExtended,ffFixed,18,4))+1);
	fCurrentWriteRecordStream := fCurrentWriteRecordStream + FloatToStrF(AExtended,ffFixed,18,4) + ',';
end;

procedure TTextInterfaceFileStream.WriteInteger(AInteger: Integer);
begin
//	inherited;
	Inc(fCurrentWriteRecordSize, Length(IntToStr(AInteger))+1);
	fCurrentWriteRecordStream := fCurrentWriteRecordStream + IntToStr(AInteger) + ',';
end;

procedure TTextInterfaceFileStream.WriteRecordSize(ASize: Integer);
begin
//  inherited;
end;

procedure TTextInterfaceFileStream.WriteSignature;
begin
//  inherited;
end;

procedure TTextInterfaceFileStream.WriteSingle(ASingle: Single);
begin
//	inherited;
	Inc(fCurrentWriteRecordSize, Length(FloatToStrF(ASingle,ffFixed,7,4))+1);
	fCurrentWriteRecordStream := fCurrentWriteRecordStream + FloatToStrF(ASingle,ffFixed,7,4) + ',';
end;

procedure TTextInterfaceFileStream.WriteString(AString: String);
var
	QuotedString: String;
begin
//	inherited;
	QuotedString := AnsiQuotedStr(AnsiDequotedStr(AString,''''),'''');
	Inc(fCurrentWriteRecordSize, Length(QuotedString)+3);
	fCurrentWriteRecordStream := fCurrentWriteRecordStream + QuotedString + ',';
end;

{ TMOUSE_PRF_FileStream }

procedure TMOUSE_PRF_FileStream.CheckCurrentLineTokens;
begin
	if fCurrentTokenIndex = fTokens.Count then
	begin
		fCurrentLine := fTextFile.ReadLine;
		ExtractTokensL(fCurrentLine, ',: ', '''', True, fTokens);
		fCurrentTokenIndex := 0;
		Inc(fCurrentLineIndex);
	end;
end;

constructor TMOUSE_PRF_FileStream.Create(AFile: String; Mode: Word);
begin
	fFile := TFileStream.Create(AFile, Mode);
	fTextFile := TStAnsiTextStream.Create(fFile);
	fTokens := TStringList.Create;
	fCurrentLineIndex := 0;
	ReadHeader;
end;

destructor TMOUSE_PRF_FileStream.Destroy;
begin
	fTokens.Free;
	inherited;
end;

procedure TMOUSE_PRF_FileStream.FlushFlows;
var
	I: Integer;
begin
	for I := 0 to Length(fNodes) - 1 do    // Iterate
	begin
		fNodes[i].Value := 0;
	end;    // for
end;

procedure TMOUSE_PRF_FileStream.FlushLine;
begin
	fCurrentLine := fTextFile.ReadLine;
	ExtractTokensL(fCurrentLine, ',: ', '''', False, fTokens);
	fCurrentTokenIndex := 0;
end;

function TMOUSE_PRF_FileStream.GetCurrentEndTime: TDateTime;
begin
	Result := GetNextTime;
end;

function TMOUSE_PRF_FileStream.GetCurrentTime: TDateTime;
begin
	Result := fCurrentTime;
end;

function TMOUSE_PRF_FileStream.GetEOF: Boolean;
begin
	Result := IsEOF;
end;

function TMOUSE_PRF_FileStream.GetFilePosition: Int64;
begin
	Result := fTextFile.Position;
end;

function TMOUSE_PRF_FileStream.GetFileSize: Int64;
begin
	Result := fTextFile.FastSize;
end;

function TMOUSE_PRF_FileStream.GetFlow(AIndex: Integer): Double;
begin
	Result := fNodes[AIndex].Value;
end;

function TMOUSE_PRF_FileStream.GetID(AIndex: Integer): String;
begin
	Result := fNodes[AIndex].ID;
end;

function TMOUSE_PRF_FileStream.GetInitialTime: TDateTime;
begin
	Result := fInitialTime;
end;

function TMOUSE_PRF_FileStream.GetNextTime: TDateTime;
begin
	Result := fCurrentTime + fLastTimeStep/SecsPerDay;
end;

function TMOUSE_PRF_FileStream.GetNode(AIndex: Integer): TPRFNode;
begin
	Result := fNodes[AIndex];
end;

function TMOUSE_PRF_FileStream.GetNumFlows: Integer;
begin
	Result := Length(fNodes);
end;

function TMOUSE_PRF_FileStream.IsEndOfRecord: boolean;
begin
	Result := (fCurrentTokenIndex = (fTokens.Count-1));
end;

function TMOUSE_PRF_FileStream.IsEOF: Boolean;
begin
	Result := (fTextFile.AtEndOfStream and IsEndOfRecord);
end;

function TMOUSE_PRF_FileStream.IsStartOfRecord: boolean;
begin
	Result := (fCurrentTokenIndex = 0);
end;

procedure TMOUSE_PRF_FileStream.MoveToStartOfFlows;
begin
	fTextFile.Seek(0, soFromBeginning);
	ReadHeader;
end;

function TMOUSE_PRF_FileStream.ReadByte: Byte;
begin
	CheckCurrentLineTokens;
	Result := Byte(StrToInt(fTokens[fCurrentTokenIndex]));
	Inc(fCurrentTokenIndex);
end;

function TMOUSE_PRF_FileStream.ReadDouble: Double;
begin
	CheckCurrentLineTokens;
	Result := StrToFloat(fTokens[fCurrentTokenIndex]);
	Inc(fCurrentTokenIndex);
end;

function TMOUSE_PRF_FileStream.ReadExtended: Extended;
begin
	CheckCurrentLineTokens;
	Result := StrToFloat(fTokens[fCurrentTokenIndex]);
	Inc(fCurrentTokenIndex);
end;

procedure TMOUSE_PRF_FileStream.ReadFlows;
var
	I: Integer;
	CurrentDate: TDateTime;
	CurrentSecond: Integer;
	CurrentMinute: Integer;
	CurrentHour: Integer;
	CurrentDateTime: TDateTime;
	NextDate: TDateTime;
	NextSecond: Integer;
	NextMinute: Integer;
	NextHour: Integer;
	NextDateTime: TDateTime;
	Dummy: String;
	PRFDateFormatSettings: TFormatSettings;
begin
	PRFDateFormatSettings.DateSeparator := '-';
	PRFDateFormatSettings.ShortDateFormat := 'YYYY-MM-DD';
	CurrentDate := StrToDate(ReadString(1), PRFDateFormatSettings);
	CurrentHour := ReadInteger;
	CurrentMinute := ReadInteger;
	CurrentSecond := ReadInteger;
	CurrentDateTime := CurrentDate + EncodeTime(CurrentHour, CurrentMinute,
		CurrentSecond, 0);

	if fFirstRead then
	begin
		fFirstRead := True;
		fInitialTime := CurrentDateTime;
	end;

	// Do a look-ahead to get timestep
	if not fTextFile.AtEndOfStream then
	begin
		FlushLine;
		NextDate := StrToDate(ReadString(1), PRFDateFormatSettings);
		NextHour := ReadInteger;
		NextMinute := ReadInteger;
		NextSecond := ReadInteger;
		NextDateTime := NextDate + EncodeTime(NextHour, NextMinute, NextSecond, 0);
		fTextFile.SeekLine(fCurrentLineIndex-2);
		FlushLine;
		fLastTimeStep := SecondSpan(NextDateTime, CurrentDateTime);
	end;

	for I := 0 to Length(fNodes) - 1 do    // Iterate
	begin
		fNodes[i].Value := ReadDouble;
	end;    // for
end;

procedure TMOUSE_PRF_FileStream.ReadHeader;
var
	ANodeType: Integer;
	Dummy: string;
	NewNodeID: Integer;
	APRFNode: TPRFNode;

	function StripAngleBrackets(AString: String): String;
	var
		I: Integer;
	begin
		Result := '';
		for I := 0 to Length(AString) - 1 do    // Iterate
		begin
			if (AString[i] <> '<') or (AString[i] <> '>') then
				Result := Result + AString[i];
		end;    // for
	end;    // function StripAngleBrackets

begin
	fFirstRead := True;
	Dummy := ReadString(1); // *M11
	Dummy := ReadString(1); //      CHAN
	Dummy := ReadString(1); // Selection toggle (always 1)
	ANodeType := ReadInteger;
	NewNodeID := Length(fNodes);
	case ANodeType of    //
		2: FlushLine; // Time Series
		103: begin // Node Water Level
				SetLength(fNodes, NewNodeID+1);
				with fNodes[NewNodeID] do
				begin
					NodeTypeDescription := ReadString(1);
					ID := ReadString(1);
					ID := StripAngleBrackets(ID);
					NodeType := ntLevel;
				end;    // with fNodes[NewNodeID]
			end;
		203: begin // Pump Discharge
			SetLength(fNodes, NewNodeID+1);
				with fNodes[NewNodeID] do
				begin
					NodeTypeDescription := ReadString(1);
					ID := ReadString(1);
					ID := StripAngleBrackets(ID);
					UpNodeID := ReadString(1);
					DnNodeID := ReadString(1);
					NodeType := ntFlow;
				end;    // with fNodes[NewNodeID]
			end;
		81: FlushLine; // Hot-start pump
		110: begin // Weir/gate position
			SetLength(fNodes, NewNodeID+1);
				with fNodes[NewNodeID] do
				begin
					NodeTypeDescription := ReadString(1);
					ID := ReadString(1);
					ID := StripAngleBrackets(ID);
					UpNodeID := ReadString(1);
					DnNodeID := ReadString(1);
					NodeType := ntFlow;
				end;    // with fNodes[NewNodeID]
			end;
		204: begin  // Weir Discharge
			SetLength(fNodes, NewNodeID+1);
				with fNodes[NewNodeID] do
				begin
					NodeTypeDescription := ReadString(1);
					ID := ReadString(1);
					ID := StripAngleBrackets(ID);
					UpNodeID := ReadString(1);
					DnNodeID := ReadString(1);
					NodeType := ntFlow;
				end;    // with fNodes[NewNodeID]
			end;
		83: FlushLine; // Hot-start regulator
		100: begin  // Link Water Level
			SetLength(fNodes, NewNodeID+1);
				with fNodes[NewNodeID] do
				begin
					NodeTypeDescription := ReadString(1);
					ID := ReadString(1);
					ID := StripAngleBrackets(ID);
					UpNodeID := ReadString(1);
					DnNodeID := ReadString(1);
					Position := ReadDouble;
					PositionUnits := ReadString(1);
					NodeType := ntLevel;
				end;    // with fNodes[NewNodeID]
			end; // Link Water Level
		200: begin // Link Discharge
			SetLength(fNodes, NewNodeID+1);
				with fNodes[NewNodeID] do
				begin
					NodeTypeDescription := ReadString(1);
					ID := ReadString(1);
					ID := StripAngleBrackets(ID);
					UpNodeID := ReadString(1);
					DnNodeID := ReadString(1);
					Position := ReadDouble;
					PositionUnits := ReadString(1);
					NodeType := ntFlow;
				end;    // with fNodes[NewNodeID]
			end;
		300: begin // Link Velocity
			SetLength(fNodes, NewNodeID+1);
				with fNodes[NewNodeID] do
				begin
					NodeTypeDescription := ReadString(1);
					ID := ReadString(1);
					ID := StripAngleBrackets(ID);
					UpNodeID := ReadString(1);
					DnNodeID := ReadString(1);
					Position := ReadDouble;
					PositionUnits := ReadString(1);
					NodeType := ntVelocity;
				end;    // with fNodes[NewNodeID]
			end;
		250: begin // System Volume
			SetLength(fNodes, NewNodeID+1);
				with fNodes[NewNodeID] do
				begin
					NodeTypeDescription := ReadString(1);
					ID := ReadString(1);
					ID := StripAngleBrackets(ID);
					NodeType := ntVolume;
				end;    // with fNodes[NewNodeID]
			end;
	end;    // case NodeType
end;

function TMOUSE_PRF_FileStream.ReadInteger: Integer;
begin
	CheckCurrentLineTokens;
	Result := StrToInt(fTokens[fCurrentTokenIndex]);
	Inc(fCurrentTokenIndex);
end;

function TMOUSE_PRF_FileStream.ReadSingle: Single;
begin
	CheckCurrentLineTokens;
	Result := StrToFloat(fTokens[fCurrentTokenIndex]);
	Inc(fCurrentTokenIndex);
end;

function TMOUSE_PRF_FileStream.ReadString(ASize: Integer): String;
begin
	CheckCurrentLineTokens;
	Result := fTokens[fCurrentTokenIndex];
	Inc(fCurrentTokenIndex);
end;

procedure TMOUSE_PRF_FileStream.WriteByte(AByte: Byte);
begin

end;

procedure TMOUSE_PRF_FileStream.WriteDouble(ADouble: Double);
begin

end;

procedure TMOUSE_PRF_FileStream.WriteExtended(AExtended: Extended);
begin

end;

procedure TMOUSE_PRF_FileStream.WriteInteger(AInteger: Integer);
begin

end;

procedure TMOUSE_PRF_FileStream.WriteSingle(ASingle: Single);
begin

end;

procedure TMOUSE_PRF_FileStream.WriteString(AString: String);
begin

end;

{ TMOUSE_PRF_InterfaceFileStream }

procedure TMOUSE_PRF_InterfaceFileStream.CheckCurrentLineTokens;
begin
	if fCurrentTokenIndex = fTokens.Count then
	begin
		fCurrentLine := fTextFile.ReadLine;
		ExtractTokensL(fCurrentLine, ',: ', '''', False, fTokens);
    Inc(fCurrentLineIndex);
		fCurrentTokenIndex := 0;
	end;
end;

constructor TMOUSE_PRF_InterfaceFileStream.Create(AFile: String;
  Mode: Word);
begin
	fFile := TFileStream.Create(AFile, Mode);
	fTextFile := TStAnsiTextStream.Create(fFile);
	fTokens := TStringList.Create;
	fIDs := TStringList.Create;
	fCurrentLineIndex := 0;
  fHeaderAlreadyRead := False;
	ReadHeader;
end;

destructor TMOUSE_PRF_InterfaceFileStream.Destroy;
begin
	fTokens.Free;
	fIDs.Free;
	inherited;
end;

procedure TMOUSE_PRF_InterfaceFileStream.FlushFlows;
var
	i: Integer;
begin
	for i := 0 to Length(fNodes) - 1 do    // Iterate
	begin
		fNodes[i].Value := 0;
	end;    // for
end;

procedure TMOUSE_PRF_InterfaceFileStream.FlushLine;
begin
	fCurrentLine := fTextFile.ReadLine;
	ExtractTokensL(fCurrentLine, ',: ', '''', False, fTokens);
	fCurrentTokenIndex := 0;
	Inc(fCurrentLineIndex);
end;

procedure TMOUSE_PRF_InterfaceFileStream.FlushRecord;
begin

end;

function TMOUSE_PRF_InterfaceFileStream.GetAlphaIDSize: Integer;
begin
	Result := 16;
end;

function TMOUSE_PRF_InterfaceFileStream.GetArea: Double;
begin
	Result := 1;
end;

function TMOUSE_PRF_InterfaceFileStream.GetCurrentEndTime: TDateTime;
begin
	Result := GetNextTime;
end;

function TMOUSE_PRF_InterfaceFileStream.GetCurrentTime: TDateTime;
begin
	Result := fCurrentTime;
end;

function TMOUSE_PRF_InterfaceFileStream.GetCurrentTimeStep: Double;
begin
	Result := fLastTimeStep;
end;

function TMOUSE_PRF_InterfaceFileStream.GetEOF: Boolean;
begin
	Result := (fTextFile.AtEndOfStream and IsEndOfRecord);
end;

function TMOUSE_PRF_InterfaceFileStream.GetFilePosition: Int64;
begin
	Result := fTextFile.Position;
end;

function TMOUSE_PRF_InterfaceFileStream.GetFileSize: Int64;
begin
	Result := fTextFile.FastSize;
end;

function TMOUSE_PRF_InterfaceFileStream.GetFlow(AIndex: Integer): Double;
begin
	Result := GetFlows(AIndex);
end;

function TMOUSE_PRF_InterfaceFileStream.GetFlowMultiplier: Double;
begin
	Result := 1;
end;

function TMOUSE_PRF_InterfaceFileStream.GetFlows(Index: Integer): Double;
begin
	Result := fNodes[Index].Value;
end;

function TMOUSE_PRF_InterfaceFileStream.GetFlowUnits(
  Index: Integer): TConvType;
begin
	case fNodes[Index].NodeType of
		ntLevel, ntVelocity: Result := duMeters;
		ntFlow, ntVolume: Result := vuCubicMeters;
	else
    Result := duMeters; // default to meters
	end;
end;

function TMOUSE_PRF_InterfaceFileStream.GetID(AIndex: Integer): String;
begin
	Result := GetIDs(AIndex);
end;

function TMOUSE_PRF_InterfaceFileStream.GetIDs(Index: Integer): String;
begin
	Result := fNodes[Index].ID;
end;

function TMOUSE_PRF_InterfaceFileStream.GetIDsList: TStringList;
begin
	Result := fIDs;
end;

function TMOUSE_PRF_InterfaceFileStream.GetInitialTime: TDateTime;
begin
	Result := fInitialTime;
end;

function TMOUSE_PRF_InterfaceFileStream.GetInterfaceFormat: TInterfaceFormat;
begin
	Result := ifMOUSEPRF;
end;

function TMOUSE_PRF_InterfaceFileStream.GetNextTime: TDateTime;
begin
	Result := fCurrentTime + fLastTimeStep/SecsPerDay;
end;

function TMOUSE_PRF_InterfaceFileStream.GetNumFlows: Integer;
begin
	Result := Length(fNodes);
end;

function TMOUSE_PRF_InterfaceFileStream.GetNumInlets: Integer;
begin
	Result := Length(fNodes);
end;

function TMOUSE_PRF_InterfaceFileStream.GetNumPollutants: Integer;
begin
	Result := 0;
end;

function TMOUSE_PRF_InterfaceFileStream.GetSourceBlock: String;
begin
	Result := PadL('MOUSE_PRF', 20);
end;

function TMOUSE_PRF_InterfaceFileStream.GetStart: TDateTime;
begin
	Result := fInitialTime;
end;

function TMOUSE_PRF_InterfaceFileStream.GetStream: Tstream;
begin
	Result := fFile;
end;

function TMOUSE_PRF_InterfaceFileStream.GetTitles(Index: Integer): String;
begin
	Result := '';
end;

function TMOUSE_PRF_InterfaceFileStream.GetToIDs(Index: Integer): String;
begin
  Result := fNodes[Index].UpNodeID;
end;

function TMOUSE_PRF_InterfaceFileStream.GetUsesAlphaNumericIDs: Boolean;
begin
	Result := True;
end;

function TMOUSE_PRF_InterfaceFileStream.IsEndOfRecord: boolean;
begin
	Result := (fCurrentTokenIndex >= (fTokens.Count-1));
end;

function TMOUSE_PRF_InterfaceFileStream.IsEOF: Boolean;
begin
	Result := (fTextFile.AtEndOfStream and IsEndOfRecord);
end;

function TMOUSE_PRF_InterfaceFileStream.IsStartOfRecord: boolean;
begin
	Result := (fCurrentTokenIndex = 0);
end;

procedure TMOUSE_PRF_InterfaceFileStream.MoveToStartOfFlows;
begin
	fTextFile.SeekLine(fFlowsStartLine);
	fCurrentLineIndex := fFlowsStartLine;
	fCurrentLine := fTextFile.ReadLine;
	ExtractTokensL(fCurrentLine, ',: ', '''', False, fTokens);
  fCurrentTokenIndex := 0;
end;

function TMOUSE_PRF_InterfaceFileStream.ReadByte: Byte;
begin
	CheckCurrentLineTokens;
	Result := Byte(StrToInt(fTokens[fCurrentTokenIndex]));
	Inc(fCurrentTokenIndex);
end;

function TMOUSE_PRF_InterfaceFileStream.ReadDouble: Double;
begin
	CheckCurrentLineTokens;
	Result := StrToFloat(fTokens[fCurrentTokenIndex]);
	Inc(fCurrentTokenIndex);
end;

function TMOUSE_PRF_InterfaceFileStream.ReadExtended: Extended;
begin
	CheckCurrentLineTokens;
	Result := StrToFloat(fTokens[fCurrentTokenIndex]);
	Inc(fCurrentTokenIndex);
end;

procedure TMOUSE_PRF_InterfaceFileStream.ReadFlows;
var
	i: Integer;
	CurrentDate: TDateTime;
	CurrentSecond: Integer;
	CurrentMinute: Integer;
	CurrentHour: Integer;
	CurrentDateTime: TDateTime;
	NextDate: TDateTime;
	NextSecond: Integer;
	NextMinute: Integer;
	NextHour: Integer;
	NextDateTime: TDateTime;
	Dummy: String;
	PRFDateFormatSettings: TFormatSettings;
begin
	PRFDateFormatSettings.DateSeparator := '-';
	PRFDateFormatSettings.ShortDateFormat := 'YYYY-MM-DD';
	CurrentDate := StrToDate(ReadString(1), PRFDateFormatSettings);
	CurrentHour := ReadInteger;
	CurrentMinute := ReadInteger;
	CurrentSecond := ReadInteger;
	CurrentDateTime := CurrentDate + EncodeTime(CurrentHour, CurrentMinute,
		CurrentSecond, 0);
  fCurrentTime := CurrentDateTime;

	if fFirstRead then
	begin
		fFirstRead := False;
		fInitialTime := CurrentDateTime;
	end;

	// Do a look-ahead to get timestep
	if not fTextFile.AtEndOfStream then
	begin
		FlushLine;
		NextDate := StrToDate(ReadString(1), PRFDateFormatSettings);
		NextHour := ReadInteger;
		NextMinute := ReadInteger;
		NextSecond := ReadInteger;
		NextDateTime := NextDate + EncodeTime(NextHour, NextMinute, NextSecond, 0);
		fTextFile.SeekLine(fCurrentLineIndex-1);
		Dec(fCurrentLineIndex, 2);
		FlushLine;
		fLastTimeStep := SecondSpan(NextDateTime, CurrentDateTime);
		ReadString(1); // Re-read Date
		ReadInteger; // Hour
		ReadInteger; // Minute
		ReadInteger; // Second
	end;

	for i := 0 to Length(fNodes) - 1 do    // Iterate and convert to English units
	begin
		case fNodes[i].NodeType of
			ntLevel, ntVelocity: fNodes[i].Value := Convert(ReadDouble, FlowUnits[i], duFeet);
			ntFlow, ntVolume: fNodes[i].Value := Convert(ReadDouble, FlowUnits[i], vuCubicFeet);
		else
			fNodes[i].Value := ReadDouble;
		end;
	end;    // for
end;

procedure TMOUSE_PRF_InterfaceFileStream.ReadHeader;
var
  PRFDateFormatSettings: TFormatSettings;
  NextDateTime: TDateTime;
  NextSecond: Integer;
  NextMinute: Integer;
  NextHour: Integer;
  NextDate: TDateTime;
	ANodeType: Integer;
	Dummy: string;
	NewNodeID: Integer;
	APRFNode: TPRFNode;

	function StripAngleBrackets(AString: String): String;
	begin
		Result := MidStr(AString, 2, Length(AString)-2);
	end;    // function StripAngleBrackets

begin
	fFirstRead := True;
	Dummy := ReadString(1); // *M11
	Dummy := ReadString(1); //      CHAN

	// Read time series identifiers available
	while true do
	begin
		Dummy := ReadString(1); // Selection toggle (always 1)
		if Dummy = '*M11' then
		begin
			Dummy := ReadString(1);
			fFlowsStartLine := fCurrentLineIndex;
			fHeaderAlreadyRead := True;
			Break;
		end;
		if fHeaderAlreadyRead then
		begin
			FlushLine;
			Continue;
		end;
		ANodeType := ReadInteger;
		NewNodeID := Length(fNodes);
		case ANodeType of    //
			2: FlushLine; // Time Series
			103: begin // Node Water Level
				SetLength(fNodes, NewNodeID+1);
				with fNodes[NewNodeID] do
				begin
					NodeTypeDescription := ReadString(1);
					ID := ReadString(1);
					ID := StripAngleBrackets(ID);
					fIDs.Add(ID);
					NodeType := ntLevel;
				end;
			end;    // with fNodes[NewNodeID]
			203: begin // Pump Discharge
				SetLength(fNodes, NewNodeID+1);
				with fNodes[NewNodeID] do
				begin
					NodeTypeDescription := ReadString(1);
					ID := ReadString(1);
					ID := StripAngleBrackets(ID);
					fIDs.Add(ID);
					UpNodeID := ReadString(1);
					DnNodeID := ReadString(1);
					NodeType := ntFlow;
				end;    // with fNodes[NewNodeID]
			end;
			81: FlushLine; // Hot-start pump
			110: begin // Weir/gate position
				SetLength(fNodes, NewNodeID+1);
				with fNodes[NewNodeID] do
				begin
					NodeTypeDescription := ReadString(1);
					ID := ReadString(1);
					ID := StripAngleBrackets(ID);
					fIDs.Add(ID);
					UpNodeID := ReadString(1);
					DnNodeID := ReadString(1);
					NodeType := ntLevel;
				end;    // with fNodes[NewNodeID]
			end;
			204: begin  // Weir Discharge
				SetLength(fNodes, NewNodeID+1);
				with fNodes[NewNodeID] do
				begin
					NodeTypeDescription := ReadString(1);
					ID := ReadString(1);
					ID := StripAngleBrackets(ID);
					fIDs.Add(ID);
					UpNodeID := ReadString(1);
					DnNodeID := ReadString(1);
					NodeType := ntFlow;
				end;    // with fNodes[NewNodeID]
			end;
			83: FlushLine; // Hot-start regulator
			100: begin  // Link Water Level
			SetLength(fNodes, NewNodeID+1);
				with fNodes[NewNodeID] do
				begin
					NodeTypeDescription := ReadString(1);
					ID := ReadString(1);
					ID := StripAngleBrackets(ID);
					fIDs.Add(ID);
					UpNodeID := ReadString(1);
					DnNodeID := ReadString(1);
					Position := ReadDouble;
					PositionUnits := ReadString(1);
					NodeType := ntLevel;
				end;    // with fNodes[NewNodeID]
			end; // Link Water Level
			200: begin // Link Discharge
			SetLength(fNodes, NewNodeID+1);
				with fNodes[NewNodeID] do
				begin
					NodeTypeDescription := ReadString(1);
					ID := ReadString(1);
					ID := StripAngleBrackets(ID);
					fIDs.Add(ID);
					UpNodeID := ReadString(1);
					DnNodeID := ReadString(1);
					Position := ReadDouble;
					PositionUnits := ReadString(1);
					NodeType := ntFlow;
				end;    // with fNodes[NewNodeID]
			end;
			300: begin // Link Velocity
				SetLength(fNodes, NewNodeID+1);
				with fNodes[NewNodeID] do
				begin
					NodeTypeDescription := ReadString(1);
					ID := ReadString(1);
					ID := StripAngleBrackets(ID);
					fIDs.Add(ID);
					UpNodeID := ReadString(1);
					DnNodeID := ReadString(1);
					Position := ReadDouble;
					PositionUnits := ReadString(1);
					NodeType := ntVelocity;
				end;    // with fNodes[NewNodeID]
			end;
			250: begin // System Volume
				SetLength(fNodes, NewNodeID+1);
				with fNodes[NewNodeID] do
				begin
					NodeTypeDescription := ReadString(1);
					ID := ReadString(1);
					ID := StripAngleBrackets(ID);
					fIDs.Add(ID);
					NodeType := ntVolume;
				end;    // with fNodes[NewNodeID]
			end;
		end;    // case NodeType
	end;

	// Do a look-ahead to get timestep
	if not fTextFile.AtEndOfStream then
	begin
		PRFDateFormatSettings.DateSeparator := '-';
		PRFDateFormatSettings.ShortDateFormat := 'YYYY-MM-DD';
		NextDate := StrToDate(ReadString(1), PRFDateFormatSettings);
		NextHour := ReadInteger;
		NextMinute := ReadInteger;
		NextSecond := ReadInteger;
		NextDateTime := NextDate + EncodeTime(NextHour, NextMinute, NextSecond, 0);
    fInitialTime := NextDateTime;
		fTextFile.SeekLine(fCurrentLineIndex-2);
		FlushLine;
	end;

end;

function TMOUSE_PRF_InterfaceFileStream.ReadInteger: Integer;
begin
	CheckCurrentLineTokens;
	Result := StrToInt(fTokens[fCurrentTokenIndex]);
	Inc(fCurrentTokenIndex);
end;

function TMOUSE_PRF_InterfaceFileStream.ReadSingle: Single;
begin
	CheckCurrentLineTokens;
	Result := StrToFloat(fTokens[fCurrentTokenIndex]);
	Inc(fCurrentTokenIndex);
end;

function TMOUSE_PRF_InterfaceFileStream.ReadString(ASize: Integer): String;
begin
	CheckCurrentLineTokens;
	Result := fTokens[fCurrentTokenIndex];
	Inc(fCurrentTokenIndex);
end;

procedure TMOUSE_PRF_InterfaceFileStream.Restart;
begin
	MoveToStartOfFlows;
end;

procedure TMOUSE_PRF_InterfaceFileStream.SetAlphaIDSize(Value: Integer);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.SetArea(Value: Double);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.SetFlowMultiplier(Value: Double);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.SetIDs(Index: Integer;
	Value: String);
begin
	if fNodes[Index].ID <> Value then
		fNodes[Index].ID := Value;
end;

procedure TMOUSE_PRF_InterfaceFileStream.SetNumInlets(Value: Integer);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.SetNumPollutants(Value: Integer);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.SetSourceBlock(Value: String);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.SetStart(Value: TDateTime);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.SetTitles(Index: Integer;
	Value: String);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.SetUsesAlphaNumericIDs(
	Value: Boolean);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.WriteByte(AByte: Byte);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.WriteDouble(ADouble: Double);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.WriteExtended(
	AExtended: Extended);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.WriteFlows(
	AInterfaceFile: ISWMMStandardInterfaceFile; SkipInterval: Integer = 0);
begin
 // ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.WriteFlows(SkipInterval: Integer = 0);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.WriteFlowsLimitNodes(
	AInterfaceFile: ISWMMStandardInterfaceFile; NodesList: TStringList;
	SkipInterval: Integer = 0);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.WriteFlowsLimitNodes(
	NodesList: TStringList; SkipInterval: Integer = 0);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.WriteFlowsLimitNodesIndexList(
	NodesList: TStringList; SkipInterval: Integer = 0);
begin
	 // ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.WriteFlowsLimitNodesIndexList(
	AInterfaceFile: ISWMMStandardInterfaceFile; NodesList: TStringList;
	SkipInterval: Integer = 0);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.WriteHeader;
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.WriteHeader(
	AInterfaceFile: ISWMMStandardInterfaceFile);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.WriteHeaderLimitNodes(
	NodesList: TStringList);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.WriteHeaderLimitNodes(
	AInterfaceFile: ISWMMStandardInterfaceFile; NodesList: TStringList);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.WriteInteger(AInteger: Integer);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.WriteSingle(ASingle: Single);
begin
	// ignore
end;

procedure TMOUSE_PRF_InterfaceFileStream.WriteString(AString: String);
begin
	// ignore
end;

{ TMOUSE_PRF_M11InFileStream }

constructor TMOUSE_PRF_M11InFileStream.Create(AFile: String; Mode: Word);
var
  NodeType: Integer;
  CurrentNodeIndex: Integer;
	CurrentLine: String;
	Tokens: TStringList;
	NumLinesToRead: Integer;

	function StripAngleBrackets(AString: String): String;
	begin
		Result := MidStr(AString, 2, Length(AString)-2);
	end;    // function StripAngleBrackets

begin
	fFileName := AFile;
	fFile := TFileStream.Create(AFile, Mode);
	fTextFile := TStAnsiTextStream.Create(fFile);
	Tokens := TStringList.Create;
	Finalize(fNodes);
	if fTextFile.LineCount >= MaxLongInt then
	begin
		fTextFile.SeekLine(MaxLongInt);
		fTextFile.Seek(0, soFromBeginning);
	end;
	NumLinesToRead := fTextFile.LineCount;
	SetLength(fNodes, NumLinesToRead);
	CurrentNodeIndex := -1;

	if Assigned(frmStatus) and frmStatus.Visible then
	begin
		frmStatus.prgStatus.TotalParts := NumLinesToRead;
	end;
	while True do
	begin
		CurrentLine := fTextFile.ReadLine;
		if LeftStr(CurrentLine, 2) = '//' then
			Continue
		else
		begin
			ExtractTokensL(CurrentLine, ',: ', '''', False, Tokens);
			if (Tokens.Count = 0) or (Tokens[2] = '<') then
				Break;
			Inc(CurrentNodeIndex);
			if CurrentNodeIndex > NumLinesToRead then
				SetLength(fNodes, CurrentNodeIndex+1);
			fNodes[CurrentNodeIndex].Selected := (Tokens[0] = '1');
			fNodes[CurrentNodeIndex].ID := StripAngleBrackets(Tokens[3]);
			if Length(fNodes[CurrentNodeIndex].ID) = 0 then
				fNodes[CurrentNodeIndex].ID := 'X'+IntToStr(CurrentNodeIndex);
			NodeType := StrToInt(Tokens[1]);
      fNodes[CurrentNodeIndex].NodeTypeID := NodeType;
			case NodeType of
				2: fNodes[CurrentNodeIndex].NodeType := ntTime;
				100, 103, 110: begin
					fNodes[CurrentNodeIndex].NodeType := ntLevel;
					if NodeType = 100 then
					begin
            try
              fNodes[CurrentNodeIndex].Position := StrToInt(Tokens[6]);
              fNodes[CurrentNodeIndex].PositionUnits := Tokens[7];
            except // In Mouse2004 results, there is a 100 Link_WL line that corresponds to diddly
              on EConvertError do
              begin
                fNodes[CurrentNodeIndex].Position := StrToInt(Tokens[5]);
                fNodes[CurrentNodeIndex].PositionUnits := Tokens[6];
              end;
            end;
					end;
				end;
				200, 203, 204: begin
					fNodes[CurrentNodeIndex].NodeType := ntFlow;
					fNodes[CurrentNodeIndex].UpNodeID := Tokens[4];
					fNodes[CurrentNodeIndex].DnNodeID := Tokens[5];
					if NodeType = 200 then
					begin
						fNodes[CurrentNodeIndex].Position := StrToInt(Tokens[6]);
						fNodes[CurrentNodeIndex].PositionUnits := Tokens[7];
					end;
				end;
				250: begin
					fNodes[CurrentNodeIndex].NodeType := ntVolume;
					if Tokens[4] = 'VOLUME' then
						fNodes[CurrentNodeIndex].ID := 'VOLUME IN SEWER SYSTEM'
					else if Tokens[4] = 'CONTINUITY' then
						fNodes[CurrentNodeIndex].ID := 'CONTINUITY BALANCE'
					else if Tokens[4] = 'WATER' then
						fNodes[CurrentNodeIndex].ID := 'WATER GENERATED IN EMPTY PARTS'
					else if Tokens[4] = 'ACCUMULATED' then
					begin
						if tokens[5] = 'IN-FLOW' then
							fNodes[CurrentNodeIndex].ID := 'ACCUMULATED IN-FLOW'
						else
							fNodes[CurrentNodeIndex].ID := 'ACCUMULATED OUT-FLOW';
					end;
				end;
				300: begin
					fNodes[CurrentNodeIndex].NodeType := ntVelocity;
					fNodes[CurrentNodeIndex].Position := StrToInt(Tokens[6]);
					fNodes[CurrentNodeIndex].PositionUnits := Tokens[7];
				end;
				81, 83: fNodes[CurrentNodeIndex].NodeType := ntOther;
			end;
			fNodes[CurrentNodeIndex].NodeTypeDescription := Tokens[2];
		end;
		if Assigned(frmStatus) and frmStatus.Visible then
		begin
			frmStatus.prgStatus.IncPartsByOne;
		end;
	end;
	SetLength(fNodes, CurrentNodeIndex+1);
	fTextFile.Free;
	fFile.Free;
	Tokens.Free;
end;

function TMOUSE_PRF_M11InFileStream.GetFileName: String;
begin
  Result := fFileName;
end;

function TMOUSE_PRF_M11InFileStream.GetNode(
  AIndex: Integer): TPRFSelectNode;
begin
  Result := fNodes[AIndex];
end;

function TMOUSE_PRF_M11InFileStream.GetNodeCount: Integer;
begin
  Result := Length(fNodes);
end;

procedure TMOUSE_PRF_M11InFileStream.SetNode(AIndex: Integer;
  ANode: TPRFSelectNode);
begin
  fNodes[AIndex] := ANode;
end;

end.
