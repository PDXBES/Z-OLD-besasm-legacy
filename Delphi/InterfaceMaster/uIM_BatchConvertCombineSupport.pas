unit uIM_BatchConvertCombineSupport;

interface

uses Windows, SysUtils, Classes, IniFiles,
	uIM_Command, uIM_ConvertEngine, uIM_MultiCombineEngine, uIM_InterfaceFiles,
	uIM_ProgressTracker;

const
	IM_BCC_ConvertCommandName = 'CONVERT';
	IM_BCC_CombineCommandName = 'MULTICOMBINE';

	BATCH_SECTION_COMMANDS = 'IM_COMMANDS';
		BATCH_COMMANDS_NUMCOMMANDS = 'NUMCOMMANDS';
		BATCH_COMMANDS_COMMANDTYPE = 'COMMAND%d';
	BATCH_SECTION_COMMAND = 'COMMAND%d';
		BATCH_COMMAND_NUMFILES = 'NUMFILES';
		BATCH_COMMAND_DESTFILE = 'DESTFILE';
		BATCH_COMMAND_DESTFORMAT = 'DESTFORMAT';
	BATCH_SECTION_COMMAND_FILE = 'COMMAND%dFILE%d';
		BATCH_COMMAND_FILE_SOURCEFILE = 'SOURCEFILE';
		BATCH_COMMAND_FILE_SOURCEFORMAT = 'SOURCEFORMAT';
			BATCH_COMMAND_FILE_FORMAT_DHI_MOUSE_PRF = 'DHI_MOUSE_PRF';
			BATCH_COMMAND_FILE_FORMAT_SWMM_XP = 'SWMM_XP';
			BATCH_COMMAND_FILE_FORMAT_SWMM_F95 = 'SWMM_F95';
			BATCH_COMMAND_FILE_FORMAT_SWMM_F32 = 'SWMM_F32';
			BATCH_COMMAND_FILE_FORMAT_SWMM_TEXT = 'SWMM_TEXT';
			BATCH_COMMAND_FILE_FORMAT_SWMM_XP_SYF = 'SWMM_XP_SYF';
		BATCH_COMMAND_FILE_DESTFILE = 'DESTFILE';
		BATCH_COMMAND_FILE_DESTFORMAT = 'DESTFORMAT';
		BATCH_COMMAND_FILE_FILTERED = 'FILTERED';
		BATCH_COMMAND_FILE_STARTDATE = 'STARTDATE';
		BATCH_COMMAND_FILE_ENDDATE = 'ENDDATE';
		BATCH_COMMAND_FILE_NEWSTARTDATE = 'NEWSTARTDATE';
	BATCH_SECTION_COMMAND_FILEFILTERS = 'COMMAND%dFILE%dFILTERS';
		BATCH_COMMAND_FILEFILTERS_NUMFILTERS = 'NUMFILTERS';
		BATCH_COMMAND_FILEFILTERS_DEFAULTFILTER = 'DEFAULTFILTER';
		BATCH_COMMAND_FILEFILTERS_FILTER = 'FILTER%d';
	BATCH_SECTION_COMMAND_DEST = 'COMMAND%dDEST';
		BATCH_COMMAND_DEST_NUMNODES = 'NUMNODES';
		BATCH_COMMAND_DEST_NODE = 'NODE%d';

type

	TIM_BCC_ConvertCommand = class(TInterfacedObject,
		IIM_Command)
	private
		fEngine: TConvertEngine;
		fSourceFile: TFileName;
		fSourceFormat: String;
		fDestFile: TFileName;
		fDestFormat: String;
		fFilterPackage: IIM_ConvertFilterPackage;
		fEnabled: Boolean;
	protected
		function GetEnabled: Boolean;
		procedure SetEnabled(Value: Boolean);
		function GetSourceFileName: String;
		function GetDestFileName: String;
		function GetDescription: String;
	public
		constructor Create(Engine: TConvertEngine; SourceFile: TFileName; SourceFormat: String;
			DestFile: TFileName; DestFormat: String; FilterPackage: IIM_ConvertFilterPackage = nil);
		destructor Destroy; override;
		function GetName: String;
		procedure Execute;
		procedure Undo;
		procedure Configure;
	//----------------------------------------------------------------------------
		property Enabled: Boolean read GetEnabled write SetEnabled;
		property SourceFileName: String read GetSourceFileName;
		property DestFileName: String read GetDestFileName;
		property Description: String read GetDescription;
	end;

	TIM_BCC_CombineCommand = class(TInterfacedObject,
		IIM_COMMAND)
	private
		fEngine: TCombineEngine;
		fDestFile: TFileName;
		fDestFormat: String;
		fEnabled: Boolean;
		fSourceSpecs: TIM_Array_BCC_SourceSpecs;
		fDestSpecs: TIM_Array_BCC_DestSpecs;
	protected
		function GetEnabled: Boolean;
		procedure SetEnabled(Value: Boolean);
		function GetSourceFileName: String;
		function GetDestFileName: String;
		function GetDescription: String;
	public
		constructor Create(Engine: TCombineEngine; DestFile: TFileName;
			DestFormat: String; SourceSpecs: TIM_Array_BCC_SourceSpecs;
			DestSpecs: TIM_Array_BCC_DestSpecs);
		destructor Destroy; override;
		function GetName: String;
		procedure Execute;
		procedure Undo;
		procedure Configure;
	//----------------------------------------------------------------------------
		property Enabled: Boolean read GetEnabled write SetEnabled;
		property SourceFileName: String read GetSourceFileName;
		property DestFileName: String read GetDestFileName;
		property Description: String read GetDescription;
	end;

	TIM_BCC_BatchEngine = class
	private
		fIniFile: TMemIniFile;
		fCommandList: TIM_CommandList;
		fConvertEngine: TConvertEngine;
		fCombineEngine: TCombineEngine;
	protected
		function GetCount: Integer;
		function GetEnabled(AIndex: Integer): Boolean;
		procedure SetEnabled(AIndex: Integer; Value: Boolean);
		function GetDescription(AIndex: Integer): String;
		function GetCommandType(AIndex: Integer): String;
	public
		constructor Create(BatchFile: String);
		destructor Destroy; override;
		procedure ReadConvertCommand(CommandID: Integer);
		procedure ReadCombineCommand(CommandID: Integer);
		procedure Execute;
		property Count: Integer read GetCount;
		property Enabled[AIndex: Integer]: Boolean read GetEnabled write SetEnabled;
		property CommandType[AIndex: Integer]: String read GetCommandType;
		property Description[AIndex: Integer]: String read GetDescription;
	end;

implementation

uses uIM_MOUSE_PRF_InterfaceFiles,
	uIM_SWMM_XP_InterfaceFiles, uIM_SWMM_TEXT_InterfaceFiles,
  StStrL, CodeSiteLogging;

{ TIM_BCC_BatchEngine }

constructor TIM_BCC_BatchEngine.Create(BatchFile: String);
var
	NumCommands: Integer;
	CommandType: String;
	i: Integer;
begin
	fIniFile := TMemIniFile.Create(BatchFile);
	fCommandList := TIM_CommandList.Create;
	fConvertEngine := TConvertEngine.Create;
	fCombineEngine := TCombineEngine.Create;

	NumCommands := fIniFile.ReadInteger(BATCH_SECTION_COMMANDS,
		BATCH_COMMANDS_NUMCOMMANDS, 0);
	for i := 1 to NumCommands do
	begin
		CommandType := fIniFile.ReadString(BATCH_SECTION_COMMANDS,
			Format(BATCH_COMMANDS_COMMANDTYPE, [i]), '');
		if CompareText(CommandType, IM_BCC_ConvertCommandName) = 0 then
			ReadConvertCommand(i)
		else if CompareText(CommandType, IM_BCC_CombineCommandName) = 0 then
			ReadCombineCommand(i);
	end;
end;

destructor TIM_BCC_BatchEngine.Destroy;
begin
	fCommandList.Free;
	fIniFile.Free;
	fConvertEngine.Free;
	fCombineEngine.Free;
	inherited;
end;

procedure TIM_BCC_BatchEngine.Execute;
begin
	fCommandList.Run;
end;

function TIM_BCC_BatchEngine.GetCommandType(AIndex: Integer): String;
begin
	Result := fCommandList[AIndex].Name;
end;

function TIM_BCC_BatchEngine.GetCount: Integer;
begin
	Result := fCommandList.Count;
end;

function TIM_BCC_BatchEngine.GetDescription(AIndex: Integer): String;
begin
	Result := fCommandList[AIndex].Description;
end;

function TIM_BCC_BatchEngine.GetEnabled(AIndex: Integer): Boolean;
begin
	Result := fCommandList[AIndex].Enabled;
end;

procedure TIM_BCC_BatchEngine.ReadCombineCommand(CommandID: Integer);
var
	NumFiles: Integer;
	DestFileName: TFileName;
	DestFormat: String;
	SourceFileName: TFileName;
	SourceFormat: String;
	StartDateStr: String;
	EndDateStr: String;
	NewStartDateStr: String;
	i: Integer;
	SourceSpecs: TIM_Array_BCC_SourceSpecs;
	NumDestSpecs: Integer;
	DestSpecs: TIM_Array_BCC_DestSpecs;
	DateTimeFormatSettings: TFormatSettings;
	Tokens: TStringList;
	DestNode: String;
begin

	NumFiles := fIniFile.ReadInteger(Format(BATCH_SECTION_COMMAND, [CommandID]),
		BATCH_COMMAND_NUMFILES, 0);
	DestFileName := fIniFile.ReadString(Format(BATCH_SECTION_COMMAND, [CommandID]),
		BATCH_COMMAND_DESTFILE, '');
	DestFormat := fIniFile.ReadString(Format(BATCH_SECTION_COMMAND, [CommandID]),
		BATCH_COMMAND_DESTFORMAT, '');

	GetLocaleFormatSettings(LOCALE_SYSTEM_DEFAULT, DateTimeFormatSettings);
	DateTimeFormatSettings.DateSeparator := '/';
	DateTimeFormatSettings.TimeSeparator := ':';
	DateTimeFormatSettings.ShortDateFormat := 'M/D/YYYY';
	DateTimeFormatSettings.ShortTimeFormat := 'HH:MM:SS';

	SetLength(SourceSpecs, NumFiles);
	for i := 1 to NumFiles do
	begin
		SourceFileName := fIniFile.ReadString(
			Format(BATCH_SECTION_COMMAND_FILE, [CommandID, i]),
			BATCH_COMMAND_FILE_SOURCEFILE, '');
		SourceFormat := fIniFile.ReadString(
			Format(BATCH_SECTION_COMMAND_FILE, [CommandID, i]),
			BATCH_COMMAND_FILE_SOURCEFORMAT, '');
		StartDateStr := fIniFile.ReadString(
			Format(BATCH_SECTION_COMMAND_FILE, [CommandID, i]),
			BATCH_COMMAND_FILE_STARTDATE, '');
		EndDateStr := fIniFile.ReadString(
			Format(BATCH_SECTION_COMMAND_FILE, [CommandID, i]),
			BATCH_COMMAND_FILE_ENDDATE, '');
		NewStartDateStr := fIniFile.ReadString(
			Format(BATCH_SECTION_COMMAND_FILE, [CommandID, i]),
			BATCH_COMMAND_FILE_NEWSTARTDATE, '');

		SourceSpecs[i-1].SourceFile := SourceFileName;
		SourceSpecs[i-1].SourceFormat := SourceFormat;
		SourceSpecs[i-1].StartDate := StrToDateTime(StartDateStr, DateTimeFormatSettings);
		SourceSpecs[i-1].EndDate := StrToDateTime(EndDateStr, DateTimeFormatSettings);
		SourceSpecs[i-1].NewStartDate := StrToDateTime(NewStartDateStr, DateTimeFormatSettings);
	end;

	NumDestSpecs := fIniFile.ReadInteger(Format(BATCH_SECTION_COMMAND_DEST,
		[CommandID]), BATCH_COMMAND_DEST_NUMNODES, 0);
	SetLength(DestSpecs, NumDestSpecs);
	Tokens := TStringList.Create;
	for i := 1 to NumDestSpecs do
	begin
		DestNode := fIniFile.ReadString(Format(BATCH_SECTION_COMMAND_DEST,
			[CommandID]), Format(BATCH_COMMAND_DEST_NODE, [i]), '');
		ExtractTokensL(DestNode, ';', '''', True, Tokens);
		DestSpecs[i-1].ID := Tokens[0];
		DestSpecs[i-1].Expression := Tokens[1];
	end;
	Tokens.Free;

	fCommandList.AddCommand(TIM_BCC_CombineCommand.Create(fCombineEngine,
		DestFileName, DestFormat, SourceSpecs, DestSpecs));
end;

procedure TIM_BCC_BatchEngine.ReadConvertCommand(CommandID: Integer);
var
	SourceFileName: String;
	SourceFormat: String;
	DestFileName: String;
	DestFormat: String;
	Filtered: Boolean;
	FilterPackage: IIM_ConvertFilterPackage;
	NumFilters: Integer;
	Filter: String;
	Tokens: TStringList;
	NumFiles: Integer;
	i, j: Integer;
begin

	NumFiles := fIniFile.ReadInteger(Format(BATCH_SECTION_COMMAND, [CommandID]),
		BATCH_COMMAND_NUMFILES, 0);
	for i := 1 to NumFiles do
	begin
		SourceFileName := fIniFile.ReadString(
			Format(BATCH_SECTION_COMMAND_FILE, [CommandID, i]),
			BATCH_COMMAND_FILE_SOURCEFILE, '');
		SourceFormat := fIniFile.ReadString(
			Format(BATCH_SECTION_COMMAND_FILE, [CommandID, i]),
			BATCH_COMMAND_FILE_SOURCEFORMAT, '');

		DestFileName :=  fIniFile.ReadString(
			Format(BATCH_SECTION_COMMAND_FILE, [CommandID, i]),
			BATCH_COMMAND_FILE_DESTFILE, '');
		DestFormat := fIniFile.ReadString(
			Format(BATCH_SECTION_COMMAND_FILE, [CommandID, i]),
			BATCH_COMMAND_FILE_DESTFORMAT, '');

		Filtered := fIniFile.ReadBool(
			Format(BATCH_SECTION_COMMAND_FILE, [CommandID, i]),
			BATCH_COMMAND_FILE_FILTERED, False);

		if Filtered then
		begin
			Tokens := TStringList.Create;
			if Destformat = BATCH_COMMAND_FILE_FORMAT_SWMM_XP then
				FilterPackage := T_SWMM_XP_ConvertFilterPackage.Create;
			NumFilters := fIniFile.ReadInteger(
				Format(BATCH_SECTION_COMMAND_FILEFILTERS, [CommandID, i]),
				BATCH_COMMAND_FILEFILTERS_NUMFILTERS, 0);
			for j := 1 to NumFilters do
			begin
				Filter := fIniFile.ReadString(
					Format(BATCH_SECTION_COMMAND_FILEFILTERS, [CommandID, i]),
					Format(BATCH_COMMAND_FILEFILTERS_FILTER, [j]), '');
				ExtractTokensL(Filter, ';', '''', True, Tokens);
				if Destformat = BATCH_COMMAND_FILE_FORMAT_SWMM_XP then
				begin
					FilterPackage.AddFilter(T_SWMM_XP_InterfaceFile_NodeFilter.Create(
						Tokens[0], Tokens[1], '', StrToBool(Tokens[2]), Tokens[3]));
				end;
			end;
			Tokens.Free;

			fCommandList.AddCommand(TIM_BCC_ConvertCommand.Create(fConvertEngine,
				SourceFileName, SourceFormat, DestFileName, DestFormat, FilterPackage));
		end
		else
			fCommandList.AddCommand(TIM_BCC_ConvertCommand.Create(fConvertEngine,
				SourceFileName, SourceFormat, DestFileName, DestFormat));
	end;
end;

procedure TIM_BCC_BatchEngine.SetEnabled(AIndex: Integer; Value: Boolean);
begin
	if fCommandList[AIndex].Enabled <> Value then
		fCommandList[AIndex].Enabled := Value;
end;

{ TIM_BCC_ConvertCommand }

constructor TIM_BCC_ConvertCommand.Create(Engine: TConvertEngine;
	SourceFile: TFileName; SourceFormat: String; DestFile: TFileName;
	DestFormat: String;	FilterPackage: IIM_ConvertFilterPackage);
begin
	fEngine := Engine;
	fSourceFile := SourceFile;
	fSourceFormat := SourceFormat;
	fDestFile := DestFile;
	fDestFormat := DestFormat;
	fFilterPackage := FilterPackage;
	fEnabled := True;
end;

procedure TIM_BCC_ConvertCommand.Undo;
begin
	// Do nothing
end;

function TIM_BCC_ConvertCommand.GetName: String;
begin
	Result := 'Convert';
end;

procedure TIM_BCC_ConvertCommand.Execute;
var
	SourceFile: IIM_InterfaceFile;
	DestFile: IIM_Convert;
begin
	// Source File Assignment
	if fSourceFormat = BATCH_COMMAND_FILE_FORMAT_DHI_MOUSE_PRF then
	begin
		// Destroy any accompanying text files first
		DeleteFile(fSourceFile + '.TXT');
		SourceFile := T_MOUSE_PRF_StandardInterfaceFile.Create(fSourceFile, fmShareDenyWrite);
	end
	else if fSourceFormat = BATCH_COMMAND_FILE_FORMAT_SWMM_XP then
		SourceFile := T_SWMM_XP_StandardInterfaceFile.Create(fSourceFile, fmShareDenyWrite)
  else if fSourceFormat = BATCH_COMMAND_FILE_FORMAT_SWMM_TEXT then
    SourceFile := T_SWMM_TEXT_StandardInterfaceFile.Create(fSourceFile, fmShareDenyWrite);

	// Destination File Assignment
	//if DestFormat = BATCH_COMMAND_FILE_FORMAT_DHI_MOUSE_PRF then
	//	DestFile := T_MOUSE_PRF_StandardInterfaceFile.Create(fDestFile, fmShareDenyWrite)
	//else}
  if fDestFormat = BATCH_COMMAND_FILE_FORMAT_SWMM_XP then
		DestFile := T_SWMM_XP_StandardInterfaceFile.Create(fDestFile, fmCreate)
  else if fDestFormat = BATCH_COMMAND_FILE_FORMAT_SWMM_TEXT then
    DestFile := T_SWMM_TEXT_StandardInterfaceFile.Create(fDestFile, fmCreate);

	fEngine.SourceFile := SourceFile;
	fEngine.DestFile := DestFile;
	fEngine.FilterPackage := fFilterPackage;
	fEngine.Execute;
	fEngine.Cleanup;
end;

procedure TIM_BCC_ConvertCommand.SetEnabled(Value: Boolean);
begin
	if fEnabled <> Value then
		fEnabled := Value;
end;

function TIM_BCC_ConvertCommand.GetEnabled: Boolean;
begin
	Result := fEnabled;
end;

function TIM_BCC_ConvertCommand.GetDestFileName: String;
begin
	Result := fDestFile;
end;

function TIM_BCC_ConvertCommand.GetSourceFileName: String;
begin
	Result := fSourceFile;
end;

function TIM_BCC_ConvertCommand.GetDescription: String;
begin
	Result :=	Format('Convert %s (%s) to %s (%s)',
		[ExtractFileName(fSourceFile), fSourceFormat,
		ExtractFileName(fDestFile), fDestFormat]);
end;

procedure TIM_BCC_ConvertCommand.Configure;
begin
	// Pop up the configurator for the command
end;

destructor TIM_BCC_ConvertCommand.Destroy;
begin
	inherited;
end;

{ TIM_BCC_CombineCommand }

constructor TIM_BCC_CombineCommand.Create(Engine: TCombineEngine;
	DestFile: TFileName; DestFormat: String;
	SourceSpecs: TIM_Array_BCC_SourceSpecs; DestSpecs: TIM_Array_BCC_DestSpecs);
begin
	fEngine := Engine;
	fDestFile := DestFile;
	fDestFormat := DestFormat;
	fDestSpecs := DestSpecs;
	fSourceSpecs := SourceSpecs;
	fEnabled := True;
end;

procedure TIM_BCC_CombineCommand.SetEnabled(Value: Boolean);
begin
	if fEnabled <> Value then
		fEnabled := Value;
end;

function TIM_BCC_CombineCommand.GetDestFileName: String;
begin
	Result := fDestFile;
end;

function TIM_BCC_CombineCommand.GetDescription: String;
begin
	Result := Format('Create %s from %d files', [ExtractFileName(fDestFile),
		Length(fSourceSpecs)]);
end;

procedure TIM_BCC_CombineCommand.Configure;
begin
	// Pop up the configurator for the command
end;

procedure TIM_BCC_CombineCommand.Undo;
begin
	// Do nothing
end;

function TIM_BCC_CombineCommand.GetName: String;
begin
	Result := 'MultiCombine';
end;

function TIM_BCC_CombineCommand.GetEnabled: Boolean;
begin
	Result := fEnabled;
end;

destructor TIM_BCC_CombineCommand.Destroy;
begin
	inherited;
end;

procedure TIM_BCC_CombineCommand.Execute;
var
	i: Integer;
begin
	if fDestFormat = BATCH_COMMAND_FILE_FORMAT_SWMM_XP then
		fEngine.DestFile := T_SWMM_XP_StandardInterfaceFile.Create(fDestFile, fmCreate);
	fEngine.DestSpecs := fDestSpecs;
	fEngine.SourceSpecs := fSourceSpecs;

	for i := 0 to Length(fSourceSpecs)-1 do
	begin
		if fSourceSpecs[i].SourceFormat = BATCH_COMMAND_FILE_FORMAT_SWMM_XP then
			fEngine.SourceFiles.Add(
				T_SWMM_XP_StandardInterfaceFile.Create(fSourceSpecs[i].SourceFile, fmShareDenyWrite)
				as IIM_InterfaceFile)
		else if fSourceSpecs[i].SourceFormat = BATCH_COMMAND_FILE_FORMAT_DHI_MOUSE_PRF then
			fEngine.SourceFiles.Add(
				T_MOUSE_PRF_StandardInterfaceFile.Create(fSourceSpecs[i].SourceFile, fmShareDenyWrite)
				as IIM_InterfaceFile)
	end;

	fEngine.Execute;
	fEngine.CleanUp;
end;

function TIM_BCC_CombineCommand.GetSourceFileName: String;
begin
	Result := Format('%d files', [Length(fSourceSpecs)]);
end;

end.
