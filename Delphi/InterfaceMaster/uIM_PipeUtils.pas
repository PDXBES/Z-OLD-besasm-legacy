unit uIM_PipeUtils;

interface

uses SysUtils, Math;

const
	MinRoughness = 0.00001;
	MinLength = 1;
	MinDiameter = 0.25;

type
	TCircularPipe = record
	private
		fID: String;
		fDiameter: Double;
		fHeight: Double;
		fRoughness: Double;
		fUSIE: Double;
		fDSIE: Double;
		fLength: Double;
		function GetSlope: Double;
		function GetFullFlow: Double;
		function GetFullHydraulicRadius: Double;
		function GetFullArea: Double;
		function GetFullVelocity: Double;
		procedure SetHeight(Value: Double);
		procedure SetRoughness(Value: Double);
		procedure SetLength(Value: Double);
		procedure SetDiameter(Value: Double);
	public
		constructor Create(AID: String; ADiameter, AUSIE, ADSIE, ALength, ARoughness: Double);
		property ID: String read fID;
		property Diameter: Double read fDiameter write SetDiameter;
		property Height: Double read fHeight write SetHeight;
		property Roughness: Double read fRoughness write SetRoughness;
		property USIE: Double read fUSIE write fUSIE;
		property DSIE: Double read fDSIE write fDSIE;
		property Length: Double read fLength write SetLength;
		property Slope: Double read GetSlope;
		function Flow: Double;
		function Velocity: Double;
		function Area: Double;
		function HydraulicRadius: Double;
		function TopWidth: Double;
		function Alpha: Double;
		property FullFlow: Double read GetFullFlow;
		property FullHydraulicRadius: Double read GetFullHydraulicRadius;
		property FullArea: Double read GetFullArea;
		property FullVelocity: Double read GetFullVelocity;
	end;

	ECircularPipeException = class(Exception)	end;
	
implementation

{ TCircularPipe }

function TCircularPipe.Alpha: Double;
begin
	if (Height > (Diameter/2)) then
		Result := 2 * Pi - 2 * ArcSin(TopWidth/Diameter)
  else
		Result := 2 * ArcSin(TopWidth/Diameter);
end;

function TCircularPipe.Area: Double;
begin
  Result := Power((Diameter/2), 2) * ArcCos((Diameter/2 - Height) / (Diameter/2)) -
  	(Diameter/2 - Height) * SqRt(Diameter*Height - Power(Height,2));
end;

constructor TCircularPipe.Create(AID: String; ADiameter, AUSIE, ADSIE, ALength,
  ARoughness: Double);
begin
	fID := AID;

	if IsZero(ADiameter) or (ADiameter < 0) then
		raise ECircularPipeException.Create('Diameter of '+FloatToStr(ADiameter)+
		' specified')
	else
		fDiameter := ADiameter;

	fUSIE := AUSIE;
	fDSIE := ADSIE;

	if IsZero(ALength) or (ALength < 0) then
		raise ECircularPipeException.Create('Length of '+FloatToStr(ALength)+
		' specified')
	else
		fLength := ALength;

	if IsZero(ARoughness) or (ARoughness < 0) then
		raise ECircularPipeException.Create('Roughness of '+FloatToStr(ARoughness)+
		' specified')
	else
		fRoughness := ARoughness;
end;

function TCircularPipe.Flow: Double;
begin
	Result := 1.49 / Roughness * Area * Power(HydraulicRadius, 2/3) *
		SqRt(Abs(Slope))*Sign(Slope);
end;

function TCircularPipe.GetFullArea: Double;
var
	TempHeight: Double;
begin
	TempHeight := Height;
	Height := Diameter;
	Result := Area;
	Height := TempHeight;
end;

function TCircularPipe.GetFullFlow: Double;
var
	TempHeight: Double;
begin
	TempHeight := Height;
	Height := Diameter;
	Result := Flow;
	Height := TempHeight;
end;

function TCircularPipe.GetFullHydraulicRadius: Double;
var
	TempHeight: Double;
begin
	TempHeight := Height;
  Height := Diameter;
	Result := HydraulicRadius;
	Height := TempHeight;
end;

function TCircularPipe.GetFullVelocity: Double;
var
	TempHeight: Double;
begin
	TempHeight := Height;
	Height := Diameter;
	Result := Velocity;
	Height := TempHeight;
end;

function TCircularPipe.GetSlope: Double;
begin
	Result := (USIE - DSIE) / Length;
end;

function TCircularPipe.HydraulicRadius: Double;
begin
	if IsZero(Alpha) then
		Result := 0
	else
		Result := Area / (Alpha * Diameter/2)
end;

procedure TCircularPipe.SetDiameter(Value: Double);
begin
	if fDiameter <> Value then
		fDiameter := Value;
	if fDiameter <= 0 then
		fDiameter := MinDiameter;
end;

procedure TCircularPipe.SetHeight(Value: Double);
begin
	if fHeight <> Value then
		fHeight := Value;
	if fHeight > Diameter then
		fHeight := Diameter;
end;

procedure TCircularPipe.SetLength(Value: Double);
begin
	if fLength <> Value then
		fLength := Value;
	if fLength <= 0 then
		fLength := MinLength;
end;

procedure TCircularPipe.SetRoughness(Value: Double);
begin
	if fRoughness <> Value then
		fRoughness := Value;
	if fRoughness <= 0 then
		fRoughness := MinRoughness;
end;

function TCircularPipe.TopWidth: Double;
begin
	Result := 2 * SqRt(Height*(Diameter - Height));
end;

function TCircularPipe.Velocity: Double;
begin
	if IsZero(fHeight) then
		Result := 0
	else
		Result := Flow / Area;
end;

end.
