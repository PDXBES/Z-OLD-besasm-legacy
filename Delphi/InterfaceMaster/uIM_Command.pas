unit uIM_Command;

interface

uses SysUtils, Classes, Contnrs;

type

	{=============================================================================
		Name:
		Purpose:
		Requirements
	=============================================================================}
	IIM_Command = interface
		['{F4DA4059-651F-440D-A1B8-9A659A5C4B7C}']
		function GetEnabled: Boolean;
		procedure SetEnabled(Value: Boolean);
		function GetName: String;
		function GetDescription: String;
		procedure Configure;
		procedure Execute;
		procedure Undo;
	//----------------------------------------------------------------------------
		property Enabled: Boolean read GetEnabled write SetEnabled;
		property Name: String read GetName;
		property Description: String read GetDescription;
	end;

	TIM_CommandList = class
	private
		fList: TInterfaceList;
	protected
		function GetCommand(AIndex: Integer): IIM_Command;
		function GetCommandName(AIndex: Integer): String;
		function GetCount: Integer;
	public
	//-Object management----------------------------------------------------------
		constructor Create; virtual;
		destructor Destroy; override;
	//----------------------------------------------------------------------------
		procedure AddCommand(ACommand: IIM_Command);
		procedure DeleteCommand(ACommand: IIM_Command);
		procedure Run(AIndex: Integer); overload;
		procedure Run; overload;
		property Command[AIndex: Integer]: IIM_Command read GetCommand; default;
		property CommandName[AIndex: Integer]: String read GetCommandName;
		property Count: Integer read GetCount;
	end;

implementation

uses CodeSiteLogging;

{ TIM_CommandList }

constructor TIM_CommandList.Create;
begin
	fList := TInterfaceList.Create;
end;

function TIM_CommandList.GetCommand(AIndex: Integer): IIM_Command;
begin
	Result := IIM_Command(fList[AIndex]);
end;

function TIM_CommandList.GetCommandName(AIndex: Integer): String;
begin
	Result := IIM_Command(fList[AIndex]).GetName;
end;

procedure TIM_CommandList.DeleteCommand(ACommand: IIM_Command);
begin
	fList.Delete(fList.IndexOf(ACommand));
end;

procedure TIM_CommandList.AddCommand(ACommand: IIM_Command);
begin
	fList.Add(ACommand);
end;

destructor TIM_CommandList.Destroy;
begin
	fList.Free;
	inherited;
end;

function TIM_CommandList.GetCount: Integer;
begin
	Result := fList.Count;
end;

procedure TIM_CommandList.Run;
var
	i: Integer;
begin
	for i := 0 to Count-1 do
	begin
		if Command[i].Enabled then
			Command[i].Execute;
	end;
end;

procedure TIM_CommandList.Run(AIndex: Integer);
begin
	Command[AIndex].Execute;
end;

end.
