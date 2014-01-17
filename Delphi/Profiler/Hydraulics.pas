unit Hydraulics;

interface

uses
  Math, Geometry;

function CircularMinSlopeRequired(AVelocity: Double; ADiameter: Double;
  ARoughness: Double): Double;

implementation

function CircularMinSlopeRequired(AVelocity: Double; ADiameter: Double;
  ARoughness: Double): Double;
var
  InvHydRad: Double;
  Angle: Double;
begin
  Angle := CircleInternalAngle(ADiameter, ADiameter/2);
  InvHydRad := (ADiameter/2*Angle/(0.5*Power((ADiameter/2),2)*(Angle-sin(Angle))));
  Result := Power((AVelocity*ARoughness/1.49)*Power((InvHydRad),2/3),2);
end;

end.
