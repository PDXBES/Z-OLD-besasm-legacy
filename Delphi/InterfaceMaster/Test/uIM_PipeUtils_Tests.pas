unit uIM_PipeUtils_Tests;

interface

uses SysUtils, TestFramework, Math,
	uIM_PipeUtils;

type
	TTestCaseA = class(TTestCase)
	private
		Pipe: TCircularPipe;
	published
		// Creation tests
		procedure TestPipeBadInitialization;
		procedure TestPipeZeroDiameter;
		procedure TestPipeZeroLength;
		procedure TestPipeNegativeDiameter;
		procedure TestPipeNegativeLength;
		procedure TestPipeZeroRoughness;
		procedure TestPipeNegativeRoughness;
		procedure TestPipeSetZeroRoughness;
		procedure TestPipeSetZeroDiameter;
		procedure TestPipeSetZeroLength;
		procedure TestPipeSetNegativeRoughness;
		procedure TestPipeSetNegativeDiameter;
		procedure TestPipeSetNegativeLength;

		// Property tests
		procedure TestPipeZeroSlope;
		procedure TestPipeHeightHigherThanDiameter;
		procedure TestNegativeSlopeFlow;
		procedure TestZeroHeightFlow;
		procedure TestZeroHeightVelocity;
	end;

implementation


{ TTestCaseA }

procedure TTestCaseA.TestPipeHeightHigherThanDiameter;
begin
	try
		Pipe := TCircularPipe.Create('Test',1,0,0,1,0.013);
		Pipe.Height := 2;
		Check(Pipe.Height >= Pipe.Diameter, 'Height higher than diameter allowed');
	except
		on E: ECircularPipeException do ;
		else
			Check(false, 'Other exception recorded');
	end;
end;

procedure TTestCaseA.TestNegativeSlopeFlow;
begin
	try
		Pipe := TCircularPipe.Create('Test',1,-1,0,1,0.013);
		Pipe.Height := 0.5;
		Check(Pipe.Flow < 0, 'Negative slope did not produce negative flow');
	except
		on E: EMathError do
			Check(false, 'Math error on negative slope flow test');
		on E : Exception do
			Check(false, 'Negative slope assignment failed');
	end;
end;

procedure TTestCaseA.TestPipeBadInitialization;
begin
	try
		Pipe := TCircularPipe.Create('Test',0,0,0,0,0);
		Check(false, 'Pipe not created successfully');
	except
		on E: ECircularPipeException do ;
		else
			Check(false, 'Other exception recorded');
	end;
end;

procedure TTestCaseA.TestPipeNegativeDiameter;
begin
	try
		Pipe := TCircularPipe.Create('Test', -1, 0, 0, 1, 0.013);
		Check(False, 'Pipe with negative diameter created');
	except
		on E: ECircularPipeException do ;
		else
			Check(false, 'Other exception recorded');
	end;
end;

procedure TTestCaseA.TestPipeNegativeLength;
begin
	try
		Pipe := TCircularPipe.Create('Test', 1, 0, 0, -1, 0.013);
		Check(False, 'Pipe with negative diameter created');
	except
		on E: ECircularPipeException do ;
		else
			Check(false, 'Other exception recorded');
	end;
end;

procedure TTestCaseA.TestPipeNegativeRoughness;
begin
	try
		Pipe := TCircularPipe.Create('Test',1,0,0,1,-0.013);
	except
		on E: ECircularPipeException do ;
		else
			Check(false, 'Other exception recorded');
	end;
end;

procedure TTestCaseA.TestPipeSetNegativeDiameter;
begin
	Pipe := TCircularPipe.Create('Test',1,0,0,1,0.013);
	Pipe.Diameter := -1;
	Check(Pipe.Diameter > 0, 'Set negative diameter allowed');
end;

procedure TTestCaseA.TestPipeSetNegativeLength;
begin
	Pipe := TCircularPipe.Create('Test',1,0,0,1,0.013);
	Pipe.Length := -1;
	Check(Pipe.Length > 0, 'Set negative length allowed');
end;

procedure TTestCaseA.TestPipeSetNegativeRoughness;
begin
	Pipe := TCircularPipe.Create('Test',1,0,0,1,0.013);
	Pipe.Roughness := -1;
	Check(Pipe.Roughness > 0, 'Set negative roughness allowed');
end;

procedure TTestCaseA.TestPipeSetZeroDiameter;
begin
	Pipe := TCircularPipe.Create('Test',1,0,0,1,0.013);
	Pipe.Diameter := 0;
	Check(Pipe.Diameter > 0, 'Set zero diameter allowed');
end;

procedure TTestCaseA.TestPipeSetZeroLength;
begin
	Pipe := TCircularPipe.Create('Test',1,0,0,1,0.013);
	Pipe.Length := 0;
	Check(Pipe.Length > 0, 'Set zero length allowed');
end;

procedure TTestCaseA.TestPipeSetZeroRoughness;
begin
	Pipe := TCircularPipe.Create('Test',1,0,0,1,0.013);
	Pipe.Roughness := 0;
	Check(Pipe.Roughness > 0, 'Set zero roughness allowed');
end;

procedure TTestCaseA.TestPipeZeroDiameter;
begin
	try
		Pipe := TCircularPipe.Create('Test',0,0,0,1,0.013);
	except
		on E: ECircularPipeException do ;
		else
			Check(false, 'Other exception recorded');
	end;
end;

procedure TTestCaseA.TestPipeZeroLength;
begin
	try
		Pipe := TCircularPipe.Create('Test',1,0,0,0,0.013);
	except
		on E: ECircularPipeException do ;
		else
			Check(false, 'Other exception recorded');
	end;
end;

procedure TTestCaseA.TestPipeZeroRoughness;
begin
	try
		Pipe := TCircularPipe.Create('Test',1,0,0,1,0);
	except
		on E: ECircularPipeException do ;
		else
			Check(false, 'Other exception recorded');
	end;
end;

procedure TTestCaseA.TestPipeZeroSlope;
begin
	Pipe := TCircularPipe.Create('Test',1,0,0,1,0.013);
	Check(IsZero(Pipe.Slope), 'Pipe with zero slope was corrected improperly');
end;

procedure TTestCaseA.TestZeroHeightFlow;
begin
	try
		Pipe := TCircularPipe.Create('Test',1,0,0,1,0.013);
		Pipe.Height := 0;
		Check(IsZero(Pipe.Flow), 'Zero height did not produce zero flow');
	except
		on E: EMathError do
			Check(false, 'Math error on zero height flow test');
		on E : Exception do
			Check(false, 'General error');
	end;
end;

procedure TTestCaseA.TestZeroHeightVelocity;
begin
	try
		Pipe := TCircularPipe.Create('Test',1,0,0,1,0.013);
		Pipe.Height := 0;
		Check(IsZero(Pipe.Velocity), 'Zero height did not produce zero velocity');
	except
		on E: EMathError do
			Check(false, 'Math error on zero height velocity test');
		on E : Exception do
			Check(false, 'General error');
	end;
end;

initialization
	TestFramework.RegisterTest(TTestCaseA.Suite);

end.
