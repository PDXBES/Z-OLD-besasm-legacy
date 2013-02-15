unit uIM_ExpressionEngine;

interface

uses StExpr, Forms;

type
	TIM_ExprEngineVarNames= array of String;
	TIM_ExprEngineVars = array of Extended;
	TIM_ExprEngine = class
	private
		fEngine: TStExpression;
		fExpression: String;
	public
		constructor Create;
		destructor Destroy; override;
		procedure SetupVars(Names: TIM_ExprEngineVarNames;
			Vars: TIM_ExprEngineVars);
		function Evaluate: Double;
		property Expression: String read fExpression write fExpression;
		property Engine: TStExpression read fEngine;
	end;

var
	ExprEngine: TIM_ExprEngine;

function GetExpressionEngine: TIM_ExprEngine;

implementation

{ TIM_ExprEngine }

constructor TIM_ExprEngine.Create;
begin
	fEngine := TStExpression.Create(Application);
end;

function TIM_ExprEngine.Evaluate: Double;
begin
	fEngine.Expression := fExpression;
	Result := fEngine.AsFloat;
end;

procedure TIM_ExprEngine.SetupVars(Names: TIM_ExprEngineVarNames;
	Vars: TIM_ExprEngineVars);
var
	i: Integer;
begin
	fEngine.ClearIdentifiers;
	fEngine.AddInternalFunctions;
	for i := Low(Names) to High(Names) do
		fEngine.AddVariable(Names[i], @Vars[i]);
end;

destructor TIM_ExprEngine.Destroy;
begin
	fEngine.Free;
	inherited;
end;

{ Unit procedures and functions }

function GetExpressionEngine: TIM_ExprEngine;
begin
	if not Assigned(ExprEngine) then
		ExprEngine := TIM_ExprEngine.Create;
	Result := ExprEngine;
end;

end.
