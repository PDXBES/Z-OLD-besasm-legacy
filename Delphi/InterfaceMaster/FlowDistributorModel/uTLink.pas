{*******************************************************}
{* 
{* uTLink.pas
{* Delphi Implementation of the Class TLink
{* Generated by Enterprise Architect
{* Created on:      03-May-2006 11:45:48 AM
{* Original author: Arnel Mandilag
{*  
{*******************************************************}

unit uTLink;

interface

uses SysUtils, Classes,
	uIM_PipeUtils, uTNode;

const
	ZeroSlopeVelocity = 1; // 1 ft/s

type

	TLink = class
	private
		//Diameter of link (feet)
		Diameter: Double;
		//Downstream invert elevation of link
		DSIE: Double;
		//Downstream node ID
		fDSNode: TNode;
		//ID of link
		ID: Integer;
		//Length of link (feet)
		fLength: Double;
		//Upstream invert elevation of link
		USIE: Double;
		//Upstream node ID
		fUSNode: TNode;
		//For tracing purposes, determines whether link has already been visited in the
			//trace
		Visited: Boolean;
		PipeProperties: TCircularPipe;
	protected
		//Calculates slope of link
		function GetSlope: Double;
		//Calculates length of time (minutes) for flow to travel down pipe
		function GetTravelTime: Double;
		//Calculates velocity of normal flow in the link
		function GetVelocity: Double;
	public
		CumulativeLength: Double;
		CumulativeTime: Double;
		//Returns whether the link has been visited
		function HasBeenVisited: Boolean;
		//Renders the link "unvisited"
		procedure Unvisit;
		//Renders the link visited
		procedure Visit;
		constructor Create(AID: Integer; AUSNode: TNode; ADSNode: TNode;
			ADiameter: Double; ALength: Double; AUSIE: Double; ADSIE: Double); overload;
		property Slope:Double read GetSlope;
		property TravelTime:Double read GetTravelTime;
		property Velocity:Double read GetVelocity;
		property LinkID:Integer read ID;
		property Length:Double read fLength;
		property USNode:TNode read fUSNode;
		property DSNode:TNode read fDSNode;
		constructor Create; overload;
		destructor Destroy; override;
	end;

implementation

uses CodeSiteLogging;

{implementation of TLink}

constructor TLink.Create;
begin
	inherited Create;
end;

destructor TLink.Destroy;
begin
	inherited Destroy;
end;

function TLink.GetSlope: Double;
begin
	Result := PipeProperties.Slope;
end;

function TLink.GetTravelTime: Double;
begin
	if PipeProperties.FullVelocity > 0 then
		Result := PipeProperties.Length / PipeProperties.FullVelocity / 60
	else
		Result := PipeProperties.Length / ZeroSlopeVelocity / 60;
end;

function TLink.GetVelocity: Double;
begin
	Result := PipeProperties.FullVelocity;
end;

function TLink.HasBeenVisited: Boolean;
begin
	Result := Visited;
end;

procedure TLink.Unvisit;
begin
	Visited := False;
end;

procedure TLink.Visit;
begin
	Visited := True;
end;

constructor TLink.Create(AID: Integer; AUSNode: TNode; ADSNode: TNode;
	ADiameter: Double; ALength: Double; AUSIE: Double; ADSIE: Double);
begin
	PipeProperties := TCircularPipe.Create(IntToStr(AID), ADiameter/12, AUSIE, ADSIE,
		ALength, 0.013);

	ID := AID;
	fUSNode := AUSNode;
	fDSNode := ADSNode;
	Diameter := ADiameter/12;
	fLength := ALength;
	USIE := AUSIE;
	DSIE := ADSIE;
end;

end.