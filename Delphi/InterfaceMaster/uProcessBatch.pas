unit uProcessBatch;

interface

uses InterfaceStreams, Classes, SysUtils, StrUtils, IniFiles;

const
  imbfCommandSection: String = 'commands';

type
  TIMCommandType = (imcNone, imcConvert, imcMulticombine);

  IIMFileSpec = interface
    ['{4014CCBB-9950-48B5-B25C-E6AC78261304}']
    procedure Read(ABatchFile: TIniFile);
  end;

  {
    TIMConvertMOUSEOptions
    Provides options for specifying the converted MOUSE M11 file that will
    result from a conversion command
  }
  TIMConvertMOUSEOptions = record
    MOUSEDB: String;
    MOUSEBSF: String;
    GenerateMOUSETimeSeries: Boolean;
    GenerateMOUSEConnectors: Boolean;
  end;

  {
    TIMConvertSWMMOptions
    Provides options for specifying the converted SWMM interface file that
    will result from a conversion command
  }
  TIMConvertSWMMOptions = record
    Titles: array[1..4] of String;
    SourceBlock: String;
    Area: Double;
    FlowMultiplier: Double;
    AlphaNumericIDs: Boolean;
    SkipInterval: Integer;
  end;

  {
    TIMConvertNodeOptions
    Provides options for specifying the converted node that results from a
    conversion command
  }
  TIMConvertNodeOptions = record
    OriginalID: String;
    NewSeriesID: String;
    NewNodeID: String;
    ReplaceWithValue: Double;
    Multiplier: Double;
  end;

  TIMCommand = class;

  {
    TIMConvertFileSpec
    Provides options for specifying how to convert a source file to a
    destination file
  }
  TIMConvertFileSpec = class(TInterfacedObject, IIMFileSpec)
  private
    fSourcePath: String;
    fSourceFormat: TInterfaceFormat;
    fDestPath: String;
    fDestFormat: TInterfaceFormat;
    fMOUSEOptions: TIMConvertMOUSEOptions;
    fSWMMOptions: TIMConvertSWMMOptions;
    fConvertAll: Boolean;
    fConvertNodeList: TStringList;
  public
    constructor Create(ACommand: TIMCommand);
    destructor Destroy;

    // IIMFileSpec Implementation
    procedure Read(ABatchFile: TIniFile);
  end;

  {
    TIMMultiCombineNodeOptions
    Provides options for specifying transforming a node to another ID to fit
    the destination model, and/or to multiply the flow by a factor
  }
  TIMMultiCombineNodeOptions = record
    OriginalID: String;
    Include: Boolean;
    NewNodeID: String;
    Multiplier: Double;
  end;

  {
    TIMMultiCombineFileSpec
    Provides options for specifying how a source file will fit into the
    timeline of the multicombined file
  }
  TIMMultiCombineFileSpec = class(TInterfacedObject, IIMFileSpec)
  private
    fSourcePath: String;
    fSourceFormat: TInterfaceFormat;
    fExtractStartDateTime: TDateTime;
    fExtractEndDateTime: TDateTime;
    fNewStartDateTime: TDateTime;
    fExcludeFromAutoGen: Boolean;
    fSkipInterval: Integer;
    fMultiCombineNodeList: TStringList;
  public
    constructor Create(ACommand: TIMCommand);
    destructor Destroy;

    // IIMFileSpec Implementation
    procedure Read(ABatchFile: TIniFile);
  end;

  {
    IIMCommandProcessor
    Interface for classes that can process a command
  }
  IIMCommandProcessor = interface
    ['{8F853137-2687-464A-B198-1C18C1B2C8EC}']
    procedure Run;
    procedure WriteLog(ALog: TStringList);
  end;

  {
    TIMNoneProcessor
    Empty file processor; will log error
  }
  TIMNoneProcessor = class(TInterfacedObject, IIMCommandProcessor)
  public
    // IIMCommandProcessor implementation
    procedure Run;
    procedure WriteLog(ALog: TStringList);
  end;

  {
    TIMConvertProcessor
    Processes a file spec for conversion and produces a destination file
  }
  TIMConvertProcessor = class(TInterfacedObject, IIMCommandProcessor)
  public
    constructor Create(AFileSpec: TIMConvertFileSpec);
    // IIMCommandProcessor implementation
    procedure Run;
    procedure WriteLog(ALog: TStringList);
  end;

  {
    TIMMultiCombineProcessor
    Processes a file spec for multicombining and produces a destination file
  }
  TIMMultiCombineProcessor = class(TInterfacedObject, IIMCommandProcessor)
  private
    fFileSpecs: TStringList;
    fDestPath: String;
    fDestFormat: TInterfaceFormat;
    fStartDateTime: TDateTime;
  public
    constructor Create(FileSpecList: TStringList);
    destructor Destroy;

    property DestPath: String read fDestPath write fDestPath;
    property DestFormat: TInterfaceFormat read fDestFormat write fDestFormat;
    property StartDateTime: TDateTime read fStartDateTime write fStartDateTime;

    // IIMCommandProcessor implementation
    procedure Run;
    procedure WriteLog(ALog: TStringList);
  end;

  TIMBatchFile = class;

  {
    TIMCommand
    Provides the framework for reading a batch command and running it
  }
  TIMCommand = class
  private
    fID: Integer;
    fFileSpecList: TStringList; // List of TIMConvertFileSpecs or TIMMultiCombineFileSpecs
    fOwner: TIMBatchFile;
    fCommandType: TIMCommandType;
    fCommandProcessor: IIMCommandProcessor;
    function GetNumFileSpecs: Integer;
    function GetFileSpec(Index: Integer): IIMFileSpec;
		procedure SetFileSpec(Index: Integer; AFileSpec: IIMFileSpec);
  public
    constructor Create(AOwner: TIMBatchFile; AID: Integer;
      ACommandType: TIMCommandType);
    destructor Destroy;

    procedure ReadCommand;
    procedure Run;

    property CommandType: TIMCommandType read fCommandType;
    property NumFileSpecs: Integer read GetNumFileSpecs;
    property Owner: TIMBatchFile read fOwner;
    property ID: Integer read fID;
    property FileSpec[Index: Integer]: IIMFileSpec read GetFileSpec
			write SetFileSpec;
  end;

  {
    TIMBatchFile
    Provides the framework for reading a batch file and processing it
  }
  TIMBatchFile = class
  private
    fIniFile: TMemIniFile;
    fCommandList: TStringList;
    fErrorLog: TStringList;
    function GetNumCommands: Integer;
    function GetCommand(Index: Integer): TIMCommand;
  public
    constructor Create(AFileName: String);
    destructor Destroy;

    procedure ReadBatchFile;
    procedure Run;

    property IniFile: TMemIniFile read fIniFile;
    property NumCommands: Integer read GetNumCommands;
    property Command[Index: Integer]: TIMCommand read GetCommand;
    property ErrorLog: TStringList read fErrorLog;
  end;


implementation

{ TIMBatchFile }

constructor TIMBatchFile.Create(AFileName: String);
begin
  fIniFile := TMemIniFile.Create(AFileName);
  fCommandList := TStringList.Create;
  fErrorLog := TStringList.Create;
  ReadBatchFile;
end;

function TIMBatchFile.GetNumCommands: Integer;
begin
  Result := fCommandList.Count;
end;

function TIMBatchFile.GetCommand(Index: Integer): TIMCommand;
begin
  Result := fCommandList.Objects[Index] as TIMCommand;
end;

procedure TIMBatchFile.ReadBatchFile;
var
  nBatchCommands: Integer;
  i: Integer;
  ACommand: TIMCommand;
  ACommandTypeAsStr: String;
  ACommandType: TIMCommandType;
begin
  nBatchCommands := IniFile.ReadInteger(imbfCommandSection, 'NumCommands',
    0);
  for i := 1 to nBatchCommands do
  begin
    ACommandTypeAsStr := IniFile.ReadString(imbfCommandSection, Format('Command%d',
      [i]));
    if Uppercase(ACommandTypeAsStr) = 'CONVERT' then
      ACommandType := imcConvert
    else if Uppercase(ACommandTypeAsStr) = 'MULTICOMBINE' then
      ACommandType := imcMultiCombine;
    ACommand := TIMCommand.Create(Self, i, ACommandType)
  end;
end;

procedure TIMBatchFile.Run;
var
  i: Integer;
begin
  for i := 0 to NumCommands - 1 do
    Command[i].Run;
end;

destructor TIMBatchFile.Destroy;
var
  i: Integer;
begin
  for i := 0 to fCommandList.Count-1 do
    fCommandList.Objects[i].Free;
  fCommandList.Free;
  fIniFile.Free;
  fErrorLog.Free;
end;

{ TIMCommand }

constructor TIMCommand.Create(AOwner: TIMBatchFile; AID: Integer;
  ACommandType: TIMCommandType);
begin
  fOwner := AOwner;
  fID := AID;
  fCommandType := ACommandType;
  fFileSpecList := TStringList.Create;
end;

procedure TIMCommand.ReadCommand;
var
  nFileSpecs: Integer;
  i: Integer;
begin
  nFileSpecs := Owner.IniFile.ReadInteger(imbfCommandSection,
    Format('Command%dNumFiles',[ID]), 0);
  for i := 1 to nFileSpecs do
  begin
    fFileSpecList.Add(IntToStr(i));
    case CommandType of
      imcConvert: FileSpec[i-1] := TIMConvertFileSpec.Create(Self);
      imcMultiCombine: FileSpec[i-1] := TIMMultiCombineFileSpec.Create(Self);
    end;
    FileSpec[i].Read(Owner.IniFile);
  end;
end;

procedure TIMCommand.Run;
var
  ACommandProcessor: IIMCommandProcessor;
begin
  case CommandType of
    imcConvert: ACommandProcessor := TIMConvertProcessor.Create();
    imcMultiCombine: ACommandProcessor := TIMMultiCombineProcessor.Create();
    imcNone: Owner.ErrorLog.Add('Unknown command: ');
  end;
  ACommandProcessor.Run;
end;

destructor TIMCommand.Destroy;
begin
  fFileSpecList.Free;
end;

function TIMCommand.GetNumFileSpecs: Integer;
begin
  Result := fFileSpecList.Count;
end;

end.
