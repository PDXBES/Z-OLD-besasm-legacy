unit Geometry;

interface

uses Math;

function CircleInternalAngle(ADepth: Double; AHeight: Double): Double;
function CirclePartialFullArea(ADepth: Double; AHeight: Double): Double;

implementation

function CircleInternalAngle(ADepth: Double; AHeight: Double): Double;
begin
  Result := 2*arccos((ADepth/2-AHeight)/(ADepth/2))
end;

function CirclePartialFullArea(ADepth: Double; AHeight: Double): Double;
begin
  Result := 0.5*Power((ADepth/2),2)*(CircleInternalAngle(ADepth,AHeight) -
    sin(CircleInternalAngle(ADepth,AHeight)))
end;

end.
 