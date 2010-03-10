unit Coords;

////////////////////////////////////////////////////////////////////////////////
//
// MVWUtil Unit
// This unit provides utility classes for manipulating points and rectangles
//
// Author: Arnel M. Mandilag
// Version: 1.2
// Date: August 11, 1998

//==============================================================================
interface

uses WinTypes, SysUtils, Classes, Printers;

{$M+}

type

////////////////////////////////////////////////////////////////////////////////
// PDPoint/TDPoint
//
	PDPoint = ^TDPoint;
	TDPoint = record
		X: Double;
		Y: Double;
	end;

	TRectangle = class;

////////////////////////////////////////////////////////////////////////////////
// TDRectangle
//	 Nonvisible rectangle class using doubles as coordinates
//
	TDRectangle = class
	private
		FLeft: Double;
		FTop: Double;
		FRight: Double;
		FBottom: Double;
		procedure SetLeft(Value: Double);
		procedure SetTop(Value: Double);
		procedure SetRight(Value: Double);
		procedure SetBottom(Value: Double);
		function GetTopLeft: TDPoint;
		procedure SetTopLeft(Value: TDPoint);
		function GetBottomRight: TDPoint;
		procedure SetBottomRight(Value: TDPoint);
		function GetWidth: Double;
		procedure SetWidth(Value: Double);
		function GetHeight: Double;
		procedure SetHeight(Value: Double);
	public
		// Object management
		constructor Create; overload;
		constructor Create(ALeft, ATop, ARight, ABottom: Double); overload;
		constructor Create(ATopLeft, ABottomRight: TDPoint); overload;
		constructor Create(ARect: TDRectangle); overload;
		// Operations
		function IsRectEmpty: Boolean;
		function IsRectNull: Boolean;
		function PtInRect(APoint: TDPoint): Boolean;
		procedure SetRect(ALeft, ATop, ARight, ABottom: Double); overload;
		procedure SetRect(APoint1, APoint2: TDPoint); overload;
		procedure SetRectEmpty;
		procedure CopyRect(ARect: TDRectangle); overload;
		procedure CopyRect(ARect: TRectangle); overload;
		procedure CopyRect(ARect: TRect); overload;
		function EqualRect(ARect: TDRectangle): Boolean;
		function IntersectRect(ARect: TDRectangle): Boolean;
		function UnionRect(ARect: TDRectangle): Boolean;
		function SubtractRect(ARect: TDRectangle): Boolean;
		procedure InflateRect(DeltaX, DeltaY: Double);
		procedure NormalizeRect;
		procedure OffsetRect(DeltaX, DeltaY: Double); overload;
		procedure OffsetRect(APoint: TDPoint); overload;
  published
		// Properties
		property Left: Double read FLeft write SetLeft;
		property Top: Double read FTop write SetTop;
		property Right: Double read FRight write SetRight;
		property Bottom: Double read FBottom write SetBottom;
		property TopLeft: TDPoint read GetTopLeft write SetTopLeft;
		property BottomRight: TDPoint read GetBottomRight write SetBottomRight;
		property Width: Double read GetWidth write SetWidth;
		property Height: Double read GetHeight write SetHeight;
	end;

////////////////////////////////////////////////////////////////////////////////
// TRectangle
//   Nonvisible rectangle class using integers as coordinates
//
	TRectangle = class
	private
		FLeft: Integer;
		FTop: Integer;
		FRight: Integer;
		FBottom: Integer;
		procedure SetLeft(Value: Integer);
		procedure SetTop(Value: Integer);
		procedure SetRight(Value: Integer);
		procedure SetBottom(Value: Integer);
		function GetTopLeft: TPoint;
		procedure SetTopLeft(Value: TPoint);
		function GetBottomRight: TPoint;
		procedure SetBottomRight(Value: TPoint);
		function GetWidth: Integer;
		procedure SetWidth(Value: Integer);
		function GetHeight: Integer;
		procedure SetHeight(Value: Integer);
	public
		// Object management
		constructor Create; overload;
		constructor Create(ALeft, ATop, ARight, ABottom: Integer); overload;
		constructor Create(ATopLeft, ABottomRight: TPoint); overload;
		constructor Create(ARect: TRectangle); overload;
		constructor Create(ARect: TRect); overload;
		// Operations
		function IsRectEmpty: Boolean;
		function IsRectNull: Boolean;
		function PtInRect(APoint: TPoint): Boolean;
		procedure SetRect(ALeft, ATop, ARight, ABottom: Integer); overload;
		procedure SetRect(APoint1, APoint2: TPoint); overload;
		procedure SetRectEmpty;
		procedure CopyRect(ARect: TRectangle); overload;
		procedure CopyRect(ARect: TDRectangle); overload;
		procedure CopyRect(ARect: TRect); overload;
		function EqualRect(ARect: TRectangle): Boolean;
		function IntersectRect(ARect: TRectangle): Boolean;
		function UnionRect(ARect: TRectangle): Boolean;
		function SubtractRect(ARect: TRectangle): Boolean;
		procedure InflateRect(DeltaX, DeltaY: Integer);
		procedure NormalizeRect;
		procedure OffsetRect(DeltaX, DeltaY: Integer); overload;
		procedure OffsetRect(APoint: TPoint); overload;
		function AsRect: TRect;
  published
		// Properties
		property Left: Integer read FLeft write SetLeft;
		property Top: Integer read FTop write SetTop;
		property Right: Integer read FRight write SetRight;
		property Bottom: Integer read FBottom write SetBottom;
		property TopLeft: TPoint read GetTopLeft write SetTopLeft;
		property BottomRight: TPoint read GetBottomRight write SetBottomRight;
		property Width: Integer read GetWidth write SetWidth;
		property Height: Integer read GetHeight write SetHeight;
	end;

////////////////////////////////////////////////////////////////////////////////
// TQuadrant
//   Used to determine the position of one point in relation to another
//
	TQuadrant = (qOrigin, qUpperRight, qUpperLeft, qLowerLeft, qLowerRight);

const
	qUpper: set of TQuadrant = [qUpperRight, qUpperLeft];
	qLower: set of TQuadrant = [qLowerRight, qLowerLeft];
	qRight: set of TQuadrant = [qUpperRight, qLowerRight];
	qLeft: set of TQuadrant = [qUpperLeft, qLowerLeft];

////////////////////////////////////////////////////////////////////////////////
// Utility procedures for Double points and rectangles
//
function DPoint(XCoord, YCoord: Double): TDPoint; overload;
function DPoint(APoint: TPoint): TDPoint; overload;
procedure OffsetPt(var APoint: TDPoint; AOffset: TDPoint); overload;
procedure DecomposePt(APoint: TDPoint; var XCoord, YCoord: Double); overload;
function LengthFromPts(Point1, Point2: TDPoint): Double; overload;
function PtToLineDist(APoint, StartPt, EndPt: TDPoint): Double; overload;
function PtToLineSegmentDist(APoint, StartPt, EndPt: TDPoint): Double; overload;
procedure NormalizePts(var ULPt, LRPt: TDPoint); overload;
function PtsEqual(APoint1, APoint2: TDPoint): Boolean; overload;

function PtFromSmallPt(ASmallPt: TSmallPoint): TPoint;
function RectFromPts(Point1, Point2: TPoint): TRect;

////////////////////////////////////////////////////////////////////////////////
// Utility procedures for Integer points and rectangles
//
procedure OffsetPt(var APoint: TPoint; AOffset: TPoint); overload;
procedure DecomposePt(APoint: TPoint; var XCoord, YCoord: Integer); overload;
function LengthFromPts(Point1, Point2: TPoint): Double; overload;
function PtToLineDist(APoint, StartPt, EndPt: TPoint): Double; overload;
function PtToLineSegmentDist(APoint, StartPt, EndPt: TPoint): Double; overload;
procedure NormalizePts(var ULPt, LRPt: TPoint); overload;
procedure NormalizeRect(var ARect: TRect);
function PtsEqual(APoint1, APoint2: TPoint): Boolean; overload;

////////////////////////////////////////////////////////////////////////////////
// Point relation functions
//
function Quadrant(APoint, ComparePoint: TPoint): TQuadrant; overload;
function Quadrant(APoint, ComparePoint: TDPoint): TQuadrant; overload;

////////////////////////////////////////////////////////////////////////////////
// Printer conversion functions
//
function DCMult(DC: HDC): Integer;

////////////////////////////////////////////////////////////////////////////////
// Miscellaneous procedures
//
procedure Swap(var Num1, Num2: Integer); overload;
procedure Swap(var Num1, Num2: Double); overload;
function Sgn(Num: Integer): Integer; overload;
function Sgn(Num: Double): Double; overload;
procedure Delay(Seconds: Double);

//==============================================================================
implementation

////////////////////////////////////////////////////////////////////////////////
// TDRectangle Implementation
//

// Object management

constructor TDRectangle.Create;
begin
	FLeft := 0.0;
	FTop := 0.0;
	FRight := 0.0;
	FBottom := 0.0;
end;

constructor TDRectangle.Create(ALeft, ATop, ARight, ABottom: Double);
begin
	Create;
	SetRect(ALeft, ATop, ARight, ABottom);
end;

constructor TDRectangle.Create(ATopLeft, ABottomRight: TDPoint);
begin
	Create;
	SetRect(ATopLeft, ABottomRight);
end;

constructor TDRectangle.Create(ARect: TDRectangle);
begin
	Create;
	CopyRect(ARect);
end;

// Property set/get methods

procedure TDRectangle.SetLeft(Value: Double);
begin
	if FLeft <> Value then
		FLeft := Value;
end;

procedure TDRectangle.SetTop(Value: Double);
begin
	if FTop <> Value then
		FTop := Value;
end;

procedure TDRectangle.SetRight(Value: Double);
begin
	if FRight <> Value then
		FRight := Value;
end;

procedure TDRectangle.SetBottom(Value: Double);
begin
	if FBottom <> Value then
		FBottom := Value;
end;

function TDRectangle.GetTopLeft: TDPoint;
begin
	Result := DPoint(FLeft, FTop);
end;

procedure TDRectangle.SetTopLeft(Value: TDPoint);
begin
	if (FLeft <> Value.X) or (FTop <> Value.Y) then
		DecomposePt(Value, FLeft, FTop);
end;

function TDRectangle.GetBottomRight: TDPoint;
begin
	Result := DPoint(FRight, FBottom);
end;

procedure TDRectangle.SetBottomRight(Value: TDPoint);
begin
	if (FRight <> Value.X) or (FBottom <> Value.Y) then
		DecomposePt(Value, FRight, FBottom);
end;

function TDRectangle.GetWidth: Double;
begin
	Result := Abs(FRight - FLeft);
end;

procedure TDRectangle.SetWidth(Value: Double);
begin
	if Value < 0.0 then
		Exit;
	if (GetWidth <> Value) then
		FRight := FLeft + Value;
end;

function TDRectangle.GetHeight: Double;
begin
	Result := Abs(FBottom - FTop);
end;

procedure TDRectangle.SetHeight(Value: Double);
begin
	if Value < 0.0 then
		Exit;
	if (GetHeight <> Value) then
		FBottom := FLeft + Value;
end;

// TDRectangle Operations

// IsRectEmpty checks whether a rectangle is empty--that is, one with no height
//	or width
function TDRectangle.IsRectEmpty: Boolean;
begin
	Result := (Width = 0.0) or (Height = 0.0);
end;

// IsRectNull checks whether all the rectangle fields are equal to zero
function TDRectangle.IsRectNull: Boolean;
begin
	Result := (FLeft = 0.0) and (FRight = 0.0) and (FTop = 0.0)
		and (FBottom = 0.0);
end;

// PtInRect checks whether a point is inside the rectangle
function TDRectangle.PtInRect(APoint: TDPoint): Boolean;
begin
	Result := (APoint.X >= FLeft) and (APoint.X <= FRight) and
		(APoint.Y >= FBottom) and (APoint.Y <= FTop);
end;

// SetRect sets the rectangle fields with provided values
procedure TDRectangle.SetRect(ALeft, ATop, ARight, ABottom: Double);
begin
	SetLeft(ALeft);
	SetTop(ATop);
	SetRight(ARight);
	SetBottom(ABottom);
	NormalizeRect;
end;

// SetRect sets the rectangle fields using two diagonally opposite points; the
// rectangle is normalized automatically
procedure TDRectangle.SetRect(APoint1, APoint2: TDPoint);
begin
	SetTopLeft(APoint1);
	SetBottomRight(APoint2);
	NormalizeRect;
end;

// SetRectEmpty nulls the rectangle fields
procedure TDRectangle.SetRectEmpty;
begin
	SetLeft(0.0);
	SetTop(0.0);
	SetRight(0.0);
	SetBottom(0.0);
end;

// CopyRect copies the fields from another rectangle
procedure TDRectangle.CopyRect(ARect: TDRectangle);
begin
	SetLeft(ARect.Left);
	SetRight(ARect.Right);
	SetTop(ARect.Top);
	SetBottom(ARect.Bottom);
end;

procedure TDRectangle.CopyRect(ARect: TRectangle);
begin
	SetLeft(ARect.Left);
	SetRight(ARect.Right);
	SetTop(ARect.Top);
	SetBottom(ARect.Bottom);
	NormalizeRect;
end;

procedure TDRectangle.CopyRect(ARect: TRect);
begin
	SetLeft(ARect.Left);
	SetRight(ARect.Right);
	SetTop(ARect.Top);
	SetBottom(ARect.Bottom);
	NormalizeRect;
end;

// EqualRect checks whether the provided rectangle is identical.  This function
// assumes the rectangles are normalized.
function TDRectangle.EqualRect(ARect: TDRectangle): Boolean;
begin
	Result := (FLeft = ARect.Left) and (FRight = ARect.Right) and
		(FTop = ARect.Top) and (FBottom = ARect.Bottom);
end;

// IntersectRect checks whether a provided rectangle intersects the current
// rectangle.  The current rectangle is changed to the intersection of the two
// rectangles; if there is no intersection, the current rectangle is left alone.
// This function assumes the rectangles are normalized. }
function TDRectangle.IntersectRect(ARect: TDRectangle): Boolean;
var
	TestRect: TDRectangle;
begin
	TestRect := TDRectangle.Create;
	try
		if ARect.Left > Left then
			TestRect.Left := ARect.Left
		else
			TestRect.Left := Left;
		if ARect.Right < Right then
			TestRect.Right := ARect.Right
		else
			TestRect.Right := Right;
		if ARect.Bottom > Bottom then
			TestRect.Bottom := ARect.Bottom
		else
			TestRect.Bottom := Bottom;
		if ARect.Top < Top then
			TestRect.Top := ARect.Top
		else
			TestRect.Top := Top;
		if (TestRect.Bottom > TestRect.Top) or (TestRect.Left > TestRect.Right) then
			Result := False
		else
		begin
			CopyRect(TestRect);
			Result := True;
		end;
	finally
		TestRect.Free;
	end;
end;

// UnionRect changes the current rectangle to the smallest rectangle that
// contains both the provided and current rectangles. This function assumes the
// rectangles are normalized.  If not, and the resulting rectangle is not
// normalized, the current rectangle is left alone. }
function TDRectangle.UnionRect(ARect: TDRectangle): Boolean;
var
	TestRect: TDRectangle;
begin
	TestRect := TDRectangle.Create;
	try
		if ARect.Left < Left then
			TestRect.Left := ARect.Left
		else
			TestRect.Left := Left;
		if ARect.Right > Right then
			TestRect.Right := ARect.Right
		else
			TestRect.Right := Right;
		if ARect.Bottom < Bottom then
			TestRect.Bottom := ARect.Bottom
		else
			TestRect.Bottom := Bottom;
		if ARect.Top > Top then
			TestRect.Top := ARect.Top
		else
			TestRect.Top := Top;
		if (TestRect.Bottom > TestRect.Top) or (TestRect.Left > TestRect.Right) then
			Result := False
		else
		begin
			CopyRect(TestRect);
			Result := True;
		end;
	finally
		TestRect.Free;
	end;
end;

// SubtractRect subtracts the provided rectangle from the current one.  This
// function succeeds only if the provided rectangle completely intersects along
// the width or the height of the current rectangle.  Otherwise, the current
// rectangle is left alone. }
function TDRectangle.SubtractRect(ARect: TDRectangle): Boolean;
var
	TestRect: TDRectangle;
	SaveRect: TDRectangle;
	SubtractFailed: Boolean;
begin
	TestRect := TDRectangle.Create;
	SaveRect := TDRectangle.Create;
	try
		SubtractFailed := False;
		TestRect.CopyRect(Self);

		if not TestRect.IntersectRect(ARect) then
			Result := False
		else
		begin
			SaveRect.CopyRect(Self);
			if TestRect.Width = Width then
			begin
				if TestRect.Height = Height then
					SetRectEmpty
				else if (TestRect.Top < Top) and (TestRect.Bottom > Bottom) then
					SubtractFailed := True
				else
				begin
					if TestRect.Top < Top then
						FBottom := TestRect.Top
					else if TestRect.Bottom > Bottom then
						FTop := TestRect.Bottom;
					Result := True;
				end;
			end
			else if TestRect.Height = Height then
			begin
				if (TestRect.Right < Right) and (TestRect.Left > Left) then
					SubtractFailed := True
				else
				begin
					if TestRect.Right < Right then
						FLeft := TestRect.Right
					else if TestRect.Left > Left then
						FRight := TestRect.Left;
					Result := True;
				end;
			end
			else
				SubtractFailed := True;

			if (TestRect.Bottom > TestRect.Top) or (TestRect.Left > TestRect.Right)
				or SubtractFailed then
			begin
				CopyRect(SaveRect);
				Result := False;
			end
			else
				Result := True;
		end;
	finally
		TestRect.Free;
		SaveRect.Free;
	end;
end;

// InflateRect resizes the rectangle so it expands horizontally and vertically in
// both directions by the provided arguments
procedure TDRectangle.InflateRect(DeltaX, DeltaY: Double);
begin
	SetLeft(FLeft - DeltaX);
	SetRight(FRight + DeltaX);
	SetTop(FTop + DeltaY);
	SetBottom(FBottom - DeltaY);
end;

// NormalizeRect corrects the rectangle so that the fields are correctly
// represented (sometimes, the top and the bottom are interchanged; this function
// switches the two so that the fields match the correct value)
procedure TDRectangle.NormalizeRect;
var
	ULPt: TDPoint;
	LRPt: TDPoint;
begin
	ULPt := GetTopLeft;
	LRPt := GetBottomRight;
	NormalizePts(ULPt, LRPt);
	SetTopLeft(ULPt);
	SetBottomRight(LRPt);
end;

// OffsetRect moves a rectangle by the provided offsets
procedure TDRectangle.OffsetRect(DeltaX, DeltaY: Double);
begin
	SetLeft(FLeft + DeltaX);
	SetRight(FRight + DeltaX);
	SetTop(FTop + DeltaY);
	SetBottom(FBottom + DeltaY);
end;

// OffsetRectByPt moves a rectangle by the offsets represented as the provided
// point
procedure TDRectangle.OffsetRect(APoint: TDPoint);
begin
	OffsetRect(APoint.X, APoint.Y);
end;

////////////////////////////////////////////////////////////////////////////////
// TRectangle Implementation
//

// Object management

constructor TRectangle.Create;
begin
	Left := 0;
	Top := 0;
	Right := 0;
	Bottom := 0;
end;

constructor TRectangle.Create(ALeft, ATop, ARight, ABottom: Integer);
begin
	Create;
	SetRect(ALeft, ATop, ARight, ABottom);
end;

constructor TRectangle.Create(ATopLeft, ABottomRight: TPoint);
begin
	Create;
	SetRect(ATopLeft, ABottomRight);
end;

constructor TRectangle.Create(ARect: TRectangle);
begin
	Create;
	CopyRect(ARect);
end;

// Property set/get methods

procedure TRectangle.SetLeft(Value: Integer);
begin
	if FLeft <> Value then
		FLeft := Value;
end;

procedure TRectangle.SetTop(Value: Integer);
begin
	if FTop <> Value then
		FTop := Value;
end;

procedure TRectangle.SetRight(Value: Integer);
begin
	if FRight <> Value then
		FRight := Value;
end;

procedure TRectangle.SetBottom(Value: Integer);
begin
	if FBottom <> Value then
		FBottom := Value;
end;

function TRectangle.GetTopLeft: TPoint;
begin
	Result := Point(FLeft, FTop);
end;

procedure TRectangle.SetTopLeft(Value: TPoint);
begin
	if (FLeft <> Value.X) or (FTop <> Value.Y) then
		DecomposePt(Value, FLeft, FTop);
end;

function TRectangle.GetBottomRight: TPoint;
begin
	Result := Point(FRight, FBottom);
end;

procedure TRectangle.SetBottomRight(Value: TPoint);
begin
	if (FRight <> Value.X) or (FBottom <> Value.Y) then
		DecomposePt(Value, FRight, FBottom);
end;

function TRectangle.GetWidth: Integer;
begin
	Result := Abs(FRight - FLeft);
end;

procedure TRectangle.SetWidth(Value: Integer);
begin
	if Value < 0 then
		Exit;
	if (GetWidth <> Value) then
		FRight := FLeft + Value;
end;

function TRectangle.GetHeight: Integer;
begin
	Result := Abs(FBottom - FTop);
end;

procedure TRectangle.SetHeight(Value: Integer);
begin
	if Value < 0 then
		Exit;
	if (GetHeight <> Value) then
		FBottom := FLeft + Value;
end;

// TRectangle Operations

// IsRectEmpty checks whether a rectangle is empty--that is, one with no height
// or width
function TRectangle.IsRectEmpty: Boolean;
begin
	Result := (Width = 0.0) or (Height = 0.0);
end;

// IsRectNull checks whether all the rectangle fields are equal to zero
function TRectangle.IsRectNull: Boolean;
begin
	Result := (FLeft = 0) and (FRight = 0) and (FTop = 0)
		and (FBottom = 0);
end;

// PtInRect checks whether a point is inside the rectangle
function TRectangle.PtInRect(APoint: TPoint): Boolean;
begin
	Result := (APoint.X >= FLeft) and (APoint.X <= FRight) and
		(APoint.Y <= FBottom) and (APoint.Y >= FTop);
end;

// SetRect sets the rectangle fields with provided values
procedure TRectangle.SetRect(ALeft, ATop, ARight, ABottom: Integer);
begin
	SetLeft(ALeft);
	SetTop(ATop);
	SetRight(ARight);
	SetBottom(ABottom);
	NormalizeRect;
end;

// SetRect sets the rectangle fields using two diagonally opposite points; the
//	rectangle is normalized automatically
procedure TRectangle.SetRect(APoint1, APoint2: TPoint);
begin
	SetTopLeft(APoint1);
	SetBottomRight(APoint2);
	NormalizeRect;
end;

// SetRectEmpty nulls the rectangle fields
procedure TRectangle.SetRectEmpty;
begin
	SetLeft(0);
	SetTop(0);
	SetRight(0);
	SetBottom(0);
end;

// CopyRect copies the fields from another rectangle
procedure TRectangle.CopyRect(ARect: TRectangle);
begin
	SetLeft(ARect.Left);
	SetRight(ARect.Right);
	SetTop(ARect.Top);
	SetBottom(ARect.Bottom);
	NormalizeRect;
end;

procedure TRectangle.CopyRect(ARect: TDRectangle);
begin
	SetLeft(Round(ARect.Left));
	SetRight(Round(ARect.Right));
	SetTop(Round(ARect.Top));
	SetBottom(Round(ARect.Bottom));
	NormalizeRect;
end;

procedure TRectangle.CopyRect(ARect: TRect);
begin
	SetLeft(ARect.Left);
	SetRight(ARect.Right);
	SetTop(ARect.Top);
	SetBottom(ARect.Bottom);
	NormalizeRect;
end;

// EqualRect checks whether the provided rectangle is identical.  This function
// assumes the rectangles are normalized.
function TRectangle.EqualRect(ARect: TRectangle): Boolean;
begin
	Result := (FLeft = ARect.Left) and (FRight = ARect.Right) and
		(FTop = ARect.Top) and (FBottom = ARect.Bottom);
end;

// IntersectRect checks whether a provided rectangle intersects the current
// rectangle.  The current rectangle is changed to the intersection of the two
// rectangles; if there is no intersection, the current rectangle is left alone.
//	This function assumes the rectangles are normalized.
function TRectangle.IntersectRect(ARect: TRectangle): Boolean;
var
	TestRect: TRectangle;
begin
	TestRect := TRectangle.Create;
	try
		if ARect.Left > Left then
			TestRect.Left := ARect.Left
		else
			TestRect.Left := Left;
		if ARect.Right < Right then
			TestRect.Right := ARect.Right
		else
			TestRect.Right := Right;
		if ARect.Bottom > Bottom then
			TestRect.Bottom := ARect.Bottom
		else
			TestRect.Bottom := Bottom;
		if ARect.Top < Top then
			TestRect.Top := ARect.Top
		else
			TestRect.Top := Top;
		if (TestRect.Bottom > TestRect.Top) or (TestRect.Left > TestRect.Right) then
			Result := False
		else
		begin
			CopyRect(TestRect);
			Result := True;
		end;
	finally
		TestRect.Free;
	end;
end;

// UnionRect changes the current rectangle to the smallest rectangle that
// contains both the provided and current rectangles. This function assumes the
// rectangles are normalized.  If not, and the resulting rectangle is not
// normalized, the current rectangle is left alone.
function TRectangle.UnionRect(ARect: TRectangle): Boolean;
var
	TestRect: TRectangle;
begin
	TestRect := TRectangle.Create;
	try
		if ARect.Left < Left then
			TestRect.Left := ARect.Left
		else
			TestRect.Left := Left;
		if ARect.Right > Right then
			TestRect.Right := ARect.Right
		else
			TestRect.Right := Right;
		if ARect.Bottom > Bottom then
			TestRect.Bottom := ARect.Bottom
		else
			TestRect.Bottom := Bottom;
		if ARect.Top < Top then
			TestRect.Top := ARect.Top
		else
			TestRect.Top := Top;
		if (TestRect.Bottom < TestRect.Top) or (TestRect.Left > TestRect.Right) then
			Result := False
		else
		begin
			CopyRect(TestRect);
			Result := True;
		end;
	finally
		TestRect.Free;
	end;
end;

// SubtractRect subtracts the provided rectangle from the current one.  This
// function succeeds only if the provided rectangle completely intersects along
// the width or the height of the current rectangle.  Otherwise, the current
// rectangle is left alone.
function TRectangle.SubtractRect(ARect: TRectangle): Boolean;
var
	TestRect: TRectangle;
	SaveRect: TRectangle;
	SubtractFailed: Boolean;
begin
	TestRect := TRectangle.Create;
	SaveRect := TRectangle.Create;
	try
		SubtractFailed := False;
		TestRect.CopyRect(Self);

		if not TestRect.IntersectRect(ARect) then
			Result := False
		else
		begin
			SaveRect.CopyRect(Self);
			if TestRect.Width = Width then
			begin
				if TestRect.Height = Height then
					SetRectEmpty
				else if (TestRect.Top < Top) and (TestRect.Bottom > Bottom) then
					SubtractFailed := True
				else
				begin
					if TestRect.Top < Top then
						FBottom := TestRect.Top
					else if TestRect.Bottom > Bottom then
						FTop := TestRect.Bottom;
					Result := True;
				end;
			end
			else if TestRect.Height = Height then
			begin
				if (TestRect.Right < Right) and (TestRect.Left > Left) then
					SubtractFailed := True
				else
				begin
					if TestRect.Right < Right then
						FLeft := TestRect.Right
					else if TestRect.Left > Left then
						FRight := TestRect.Left;
					Result := True;
				end;
			end
			else
				SubtractFailed := True;

			if (TestRect.Bottom > TestRect.Top) or (TestRect.Left > TestRect.Right)
				or SubtractFailed then
			begin
				CopyRect(SaveRect);
				Result := False;
			end
			else
				Result := True;
		end;
	finally
		TestRect.Free;
		SaveRect.Free;
	end;
end;

// InflateRect resizes the rectangle so it expands horizontally and vertically in
// both directions by the provided arguments
procedure TRectangle.InflateRect(DeltaX, DeltaY: Integer);
begin
	SetLeft(FLeft - DeltaX);
	SetRight(FRight + DeltaX);
	SetTop(FTop - DeltaY);
	SetBottom(FBottom + DeltaY);
end;

// NormalizeRect corrects the rectangle so that the fields are correctly
// represented (sometimes, the top and the bottom are interchanged; this function
// switches the two so that the fields match the correct value)
procedure TRectangle.NormalizeRect;
var
	ULPt: TPoint;
	LRPt: TPoint;
begin
	ULPt := GetTopLeft;
	LRPt := GetBottomRight;
	NormalizePts(ULPt, LRPt);
	SetTopLeft(ULPt);
	SetBottomRight(LRPt);
end;

// OffsetRect moves a rectangle by the provided offsets
procedure TRectangle.OffsetRect(DeltaX, DeltaY: Integer);
begin
	SetLeft(FLeft + DeltaX);
	SetRight(FRight + DeltaX);
	SetTop(FTop + DeltaY);
	SetBottom(FBottom + DeltaY);
end;

// OffsetRectByPt moves a rectangle by the offsets represented as the provided
// point
procedure TRectangle.OffsetRect(APoint: TPoint);
begin
	OffsetRect(APoint.X, APoint.Y);
end;

////////////////////////////////////////////////////////////////////////////////
// Utility procedures for points and rectangles
//

// Create a Double point out of given coordinates
function DPoint(XCoord, YCoord: Double): TDPoint;
var
	ADPoint: TDPoint;
begin
	ADPoint.X := XCoord;
	ADPoint.Y := YCoord;
	Result := ADPoint;
end;

function DPoint(APoint: TPoint): TDPoint;
begin
	Result := DPoint(APoint.X, APoint.Y);
end;

{ Offsets a Double point using the X an Y coordinates of another Double point
	as offsets }
procedure OffsetPt(var APoint: TDPoint; AOffset: TDPoint);
begin
	APoint.X := APoint.X + AOffset.X;
	APoint.Y := APoint.Y + AOffset.Y;
end;

{ Separates the coordinates of a Double point into the given variables }
procedure DecomposePt(APoint: TDPoint; var XCoord, YCoord: Double);
begin
	XCoord := APoint.X;
	YCoord := APoint.Y;
end;

{ Calculates the length between two Double points }
function LengthFromPts(Point1, Point2: TDPoint): Double;
begin
	Result := Sqrt(Sqr(Point2.X - Point1.X) + Sqr(Point2.Y - Point1.Y));
end;

{ Calculates the length between a point and a line defined by two other points }
function PtToLineDist(APoint, StartPt, EndPt: TDPoint): Double;
begin
	Result := Abs((StartPt.X-APoint.X)*(EndPt.Y-APoint.Y)-
		(EndPt.X-APoint.X)*(StartPt.Y-APoint.Y))/LengthFromPts(StartPt, EndPt);
end;

{ Calculates the length between a point and a line segment defined by two other
	points; if the point in question is not within the rectangular bounds of the
	line segment, then the length is given as infinity--i.e., the largest double }
function PtToLineSegmentDist(APoint, StartPt, EndPt: TDPoint): Double;
var
	LineRect: TDRectangle;
begin
	LineRect := TDRectangle.Create;
	try
		LineRect.SetRect(StartPt, EndPt);
		if LineRect.PtInRect(APoint) then
			Result := Abs((StartPt.X-APoint.X)*(EndPt.Y-APoint.Y)-
				(EndPt.X-APoint.X)*(StartPt.Y-APoint.Y))/LengthFromPts(StartPt, EndPt)
		else
			Result := 1.7e38;
	finally
		LineRect.Free;
	end;
end;

{ Exchanges the coordinates between two points so that the first point gives the
	upper left coordinate and the second gives the lower right coordinate }
procedure NormalizePts(var ULPt, LRPt: TDPoint);
begin
	if ULPt.X > LRPt.X then
		Swap(ULPt.X, LRPt.X);
	if ULPt.Y < LRPt.Y then
		Swap(ULPt.Y, LRPt.Y);
end;

function PtsEqual(APoint1, APoint2: TDPoint): Boolean;
begin
	Result := (APoint1.X = APoint2.X) and (APoint1.Y = APoint2.Y);
end;

{ Creates an Integer point from a Small point }
function PtFromSmallPt(ASmallPt: TSmallPoint): TPoint;
begin
	Result := Point(ASmallPt.X, ASmallPt.Y);
end;

{ Creates an Integer rectangle from two Integer points }
function RectFromPts(Point1, Point2: TPoint): TRect;
var
	ULPt: TPoint;
	LRPt: TPoint;
begin
	ULPt := Point1;
	LRPt := Point2;
	NormalizePts(ULPt, LRPt);
	Result := Rect(ULPt.X, ULPt.Y, LRPt.X, LRPt.Y);
end;

{ Offsets an Integer point using the X an Y coordinates of another Integer point
	as offsets }
procedure OffsetPt(var APoint: TPoint; AOffset: TPoint);
begin
	APoint.X := APoint.X + AOffset.X;
	APoint.Y := APoint.Y + AOffset.Y;
end;

{ Separates the coordinates of an Integer point into the given variables }
procedure DecomposePt(APoint: TPoint; var XCoord, YCoord: Integer);
begin
	XCoord := APoint.X;
	YCoord := APoint.Y;
end;

{ Calculates the length between two Integer points }
function LengthFromPts(Point1, Point2: TPoint): Double;
begin
	Result := Sqrt(Sqr(Point2.X - Point1.X) + Sqr(Point2.Y - Point1.Y));
end;

{ Calculates the length between an Integer point and a line defined by two other
	Integer points }
function PtToLineDist(APoint, StartPt, EndPt: TPoint): Double;
begin
	Result := Abs((StartPt.X-APoint.X)*(EndPt.Y-APoint.Y)-
		(EndPt.X-APoint.X)*(StartPt.Y-APoint.Y))/
		LengthFromPts(DPoint(StartPt), DPoint(EndPt));
end;

{ Calculates the length between a point and a line segment defined by two other
	points; if the point in question is not within the rectangular bounds of the
	line segment, then the length is given as infinity--i.e., the largest double }
function PtToLineSegmentDist(APoint, StartPt, EndPt: TPoint): Double;
var
	LineRect: TDRectangle;
begin
	LineRect := TDRectangle.Create;
	try
		LineRect.SetRect(DPoint(StartPt), DPoint(EndPt));
		if LineRect.PtInRect(DPoint(APoint)) then
			Result := Abs((StartPt.X-APoint.X)*(EndPt.Y-APoint.Y)-
				(EndPt.X-APoint.X)*(StartPt.Y-APoint.Y))/
				LengthFromPts(DPoint(StartPt), DPoint(EndPt))
		else
			Result := 1.7e38;
	finally
		LineRect.Free;
	end;
end;

procedure NormalizePts(var ULPt, LRPt: TPoint);
begin
	if ULPt.X > LRPt.X then
		Swap(ULPt.X, LRPt.X);
	if ULPt.Y > LRPt.Y then
		Swap(ULPt.Y, LRPt.Y);
end;

procedure NormalizeRect(var ARect: TRect);
var
	APoint1: TPoint;
	APoint2: TPoint;
begin
	APoint1 := Point(ARect.Left, ARect.Top);
	APoint2 := Point(ARect.Right, ARect.Bottom);
	NormalizePts(APoint1, APoint2);
	DecomposePt(APoint1, ARect.Left, ARect.Top);
	DecomposePt(APoint2, ARect.Right, ARect.Bottom);
end;

function PtsEqual(APoint1, APoint2: TPoint): Boolean;
begin
	Result := (APoint1.X = APoint2.X) and (APoint1.Y = APoint2.Y);
end;

procedure OffsetRect(var ARect: TRect; AOffset: TPoint);
begin
	with ARect do
	begin
		Inc(Left, AOffset.X);
		Inc(Right, AOffset.X);
		Inc(Top, AOffset.Y);
		Inc(Bottom, AOffset.Y);
	end;
end;

function Quadrant(APoint, ComparePoint: TPoint): TQuadrant;
begin
	if PtsEqual(APoint, ComparePoint) then
	begin
		Result := qOrigin;
		Exit;
	end;

	if APoint.X < ComparePoint.X then
		if APoint.Y > ComparePoint.Y then
			Result := qLowerLeft
		else
			Result := qUpperLeft
	else
		if APoint.Y > ComparePoint.Y then
			Result := qLowerRight
		else
			Result := qUpperRight;
end;

function Quadrant(APoint, ComparePoint: TDPoint): TQuadrant;
begin
	if PtsEqual(APoint, ComparePoint) then
	begin
		Result := qOrigin;
		Exit;
	end;

	if APoint.X < ComparePoint.X then
		if APoint.Y < ComparePoint.Y then
			Result := qLowerLeft
		else
			Result := qUpperLeft
	else
		if APoint.Y < ComparePoint.Y then
			Result := qLowerRight
		else
			Result := qUpperRight;
end;

function DCMult(DC: HDC): Integer;
var
	Res: Integer;
begin
	Res := GetDeviceCaps(DC, LOGPIXELSX);
	case Res of
		96, 120: Result := 1; // Screen
	else
		Result := Res div 100;
	end;
end;

procedure Swap(var Num1, Num2: Integer);
var
	Temp: Integer;
begin
	Temp := Num1;
	Num1 := Num2;
	Num2 := Temp;
end;

procedure Swap(var Num1, Num2: Double);
var
	Temp: Double;
begin
	Temp := Num1;
	Num1 := Num2;
	Num2 := Temp;
end;

function Sgn(Num: Integer): Integer;
begin
	if Num = 0 then
		Result := 0
	else
		Result := Num div Abs(Num);
end;

function Sgn(Num: Double): Double;
begin
	if Num = 0 then
		Result := 0
	else
		Result := Num/Abs(Num);
end;

constructor TRectangle.Create(ARect: TRect);
begin
	Create;
	CopyRect(ARect);
end;

function TRectangle.AsRect: TRect;
begin
	Result := Rect(Left, Top, Right, Bottom);
end;

procedure Delay(Seconds: Double);
var
	Start, Now: TDateTime;
begin
	Start := Time;
	repeat
		Now := Time;
	until (Now-Start) > (Seconds/86400);
end;

end.

