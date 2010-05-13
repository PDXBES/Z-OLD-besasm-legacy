unit uIM_Tracing;

interface

uses SysUtils, Classes, Contnrs;

type

	ITraceable = interface
		['{BFEB48EA-7E97-430E-96B3-CC1E63C79C0A}']
		function GetID: String;
		procedure SetID(AValue: String);
		function GetUS: String;
		procedure SetUS(AValue: String);
		function GetDS: String;
		procedure SetDS(AValue: String);
		function GetVisited: Boolean;
		procedure SetVisited(AValue: Boolean);
		function GetLengthFromRoot: Double;
		function GetLength: Double;
		function GetTravelTime: Double;
		function GetTravelTimeToRoot: Double;
		property ID: String read GetID write SetID;
		property US: String read GetUS write SetUS;
		property DS: String read GetDS write SetDS;
		property Visited: Boolean read GetVisited write SetVisited;
		property Length: Double read GetLength;
		property LengthFromRoot: Double read GetLengthFromRoot;
		property TravelTime: Double read GetTravelTime;
		property TravelTimeToRoot: Double read GetTravelTimeToRoot;
	end;

	TTraceableStack = class
	private
		fStack: TStack;
	public
		constructor Create;
		destructor Destroy; override;
		procedure Push(ATraceable: ITraceable);
		function Peek: ITraceable;
		function Pop: ITraceable;
		function Count: Integer;
	end;

	TTraceableList = class
	private
		fList: TInterfaceList;
	protected
		function GetItem(AIndex: Integer): ITraceable;
	public
		constructor Create;
		destructor Destroy; override;
		function Add(ATraceable: ITraceable): Integer;
		procedure Clear;
		function Count: Integer;
		procedure Delete(ATraceable: ITraceable); overload;
		procedure Delete(AIndex: Integer); overload;
		function IndexOf(ATraceable: ITraceable): Integer;
		property Items[AIndex: Integer]: ITraceable read GetItem; default;
		function USNodesList: TStringList;
		function DSNodesList: TStringList;
		function NodesList: TStringList;
	end;

	TTraceDirection = (tdUp, tdDown);

	TTraceableNetwork = class
	private
		fItemsList: TTraceableList;
		fTracedList: TTraceableList;
	protected
		function GetItem(AIndex: Integer): ITraceable; overload;
		function GetItem(AIndex: String): ITraceable; overload;
		function GetTraced(AIndex: Integer): ITraceable;
	public
		constructor Create;
		destructor Destroy; override;
		function Trace(Starts: TTraceableList; Ends: TTraceableList;
			ADirection: TTraceDirection): Integer;
		function CountTraced: Integer;
		property Items[AIndex: Integer]: ITraceable read GetItem; default;
		property ItemsByName[AIndex: String]: ITraceable read GetItem;
		function Add(ATraceable: ITraceable): Integer;
		property Traced[AIndex: Integer]: ITraceable read GetTraced;
		function GetUSList(ATraceable: ITraceable): TTraceableList;
		function GetDSList(ATraceable: ITraceable): TTraceableList;
	end;

implementation

uses StrUtils;

{ TTraceableNetwork }

function TTraceableNetwork.Add(ATraceable: ITraceable): Integer;
begin
	Result :=  fItemsList.Add(ATraceable)
end;

function TTraceableNetwork.CountTraced: Integer;
begin
	Result := fTracedList.Count;
end;

constructor TTraceableNetwork.Create;
begin
	fItemsList := TTraceableList.Create;
	fTracedList := TTraceableList.Create;
end;

destructor TTraceableNetwork.Destroy;
begin
	fItemsList.Free;
	fTracedList.Free;
	inherited;
end;

function TTraceableNetwork.GetDSList(ATraceable: ITraceable): TTraceableList;
var
	CommonNode: String;
	i: Integer;
begin
	CommonNode := ATraceable.DS;
	Result := TTraceableList.Create;
	for i := 0 to fItemsList.Count - 1 do
		if fItemsList[i].US = CommonNode then
			Result.Add(fItemsList[i]);
	if Result.Count = 0 then
		FreeAndNil(Result);
end;

function TTraceableNetwork.GetItem(AIndex: String): ITraceable;
var
	i: Integer;
begin
	Result := nil;
	for i := 0 to fItemsList.Count - 1 do
		if CompareText(fItemsList[i].ID, AIndex) = 0 then
		begin
			Result := fItemsList[i];
			Exit;
    end;			
end;

function TTraceableNetwork.GetItem(AIndex: Integer): ITraceable;
begin
	Result := fItemsList[AIndex];
end;

function TTraceableNetwork.GetTraced(AIndex: Integer): ITraceable;
begin
	Result := fTracedList[AIndex];
end;

function TTraceableNetwork.GetUSList(ATraceable: ITraceable): TTraceableList;
var
	CommonNode: String;
	i: Integer;
begin
	CommonNode := ATraceable.US;
	Result := TTraceableList.Create;
	for i := 0 to fItemsList.Count - 1 do
		if fItemsList[i].DS = CommonNode then
			Result.Add(fItemsList[i]);
	if Result.Count = 0 then
		FreeAndNil(Result);
end;

function TTraceableNetwork.Trace(Starts, Ends: TTraceableList;
	ADirection: TTraceDirection): Integer;


var
	TraceStack: TTraceableStack;
	i: Integer;
	InternalStarts: TTraceableList;
	InternalEnds: TTraceableList;
	AddedIndex: Integer;
	UpstreamsList: TTraceableList;
	CurrentItem: ITraceable;
  CumulativeLength: Double;
  CumulativeTravelTime: Double;

	function IsEnd(ATraceable: ITraceable): Boolean;
	var
		j: Integer;
	begin
		Result := False;
		for j := 0 to InternalEnds.Count - 1 do
			if InternalEnds[j] = ATraceable then
			begin
				Result := True;
				Exit;
			end;
	end;

begin
	// Assume normal direction of trace is tdUp; switch out starts/ends otherwise
	if ADirection = tdUp then
	begin
		InternalStarts := Ends;
		InternalEnds := Starts;
	end
	else
	begin
		InternalStarts := Starts;
		InternalEnds := Ends;
	end;

	// Initialize network and clear the trace list so that nothing has been visited
	fTracedList.Clear;
	for i := 0 to fItemsList.Count - 1 do
		fItemsList[i].Visited := False;

	// Initialize stack and traced list
	for i := 0 to Starts.Count - 1 do
	begin
		TraceStack.Push(InternalStarts[i]);
		AddedIndex := fTracedList.Add(InternalStarts[i]);
		fTracedList[AddedIndex].Visited := True;
		fTracedList[AddedIndex].LengthFromRoot := fTracedList[AddedIndex].Length;
		fTracedList[AddedIndex].TravelTimeToRoot := fTracedList[AddedIndex].TravelTime;
	end;

	{ Perform trace: place starts on the stack, then travel up each item in the
		stack until the stack has been exhausted.  As each upstream item is found,
		push it on the stack.  Each item on the stack is then popped and any
		upstream items found are pushed onto the stack.  If the most upstream
		item is found, there are no further items to push onto the stack, and so
		the loop moves to the next branch. Any items reached are considered visited.
		To prevent an item from being retraced, any visited items are not counted
		in the upstream items when pushing them onto the stack.  If an end (stop)
		item is reached, it is not placed onto the stack so that the trace up the
		branch stops. }

	while TraceStack.Count > 0 do
	begin
		CurrentItem := TraceStack.Pop;
		CumulativeLength := CurrentItem.LengthFromRoot;
		CumulativeTravelTime := CurrentItem.TravelTimeToRoot;
		if ADirection = tdUp then
			UpstreamsList := GetUSList(CurrentItem)
		else
			UpstreamsList := GetDSList(CurrentItem);
		if UpstreamsList.Count > 0 then
			for i := 0 to UpstreamsList.Count - 1 do
			begin
				if not (UpstreamsList[i].Visited) then
				begin
					if not IsEnd(UpstreamsList[i]) then
						TraceStack.Push(UpstreamsList[i]);
					AddedIndex := fTracedList.Add(UpstreamsList[i]);
					fTracedList[AddedIndex].Visited := True;
					fTracedList[AddedIndex].LengthFromRoot := CumulativeLength +
						fTracedList[AddedIndex].Length;
					fTracedList[AddedIndex].TravelTimeToRoot := CumulativeTravelTime +
          	fTracedList[AddedIndex].TravelTime;
				end;
			end;
	end;

end;

{ TTraceableStack }

function TTraceableStack.Count: Integer;
begin
	Result := fStack.Count;
end;

constructor TTraceableStack.Create;
begin
	fStack := TStack.Create;
end;

destructor TTraceableStack.Destroy;
begin
	fStack.Free;
  inherited;
end;

function TTraceableStack.Peek: ITraceable;
begin
	Result := ITraceable(fStack.Peek);
end;

function TTraceableStack.Pop: ITraceable;
begin
	Result := ITraceable(fStack.Pop);
end;

procedure TTraceableStack.Push(ATraceable: ITraceable);
begin
	fStack.Push(Pointer(ATraceable));
end;

{ TTraceableList }

function TTraceableList.Add(ATraceable: ITraceable): Integer;
begin
	Result := fList.Add(ATraceable);
end;

procedure TTraceableList.Clear;
begin
	fList.Clear;
end;

function TTraceableList.Count: Integer;
begin
	Result := fList.Count;
end;

constructor TTraceableList.Create;
begin
	fList := TInterfaceList.Create;
end;

procedure TTraceableList.Delete(AIndex: Integer);
begin
	fList.Delete(AIndex);
end;

procedure TTraceableList.Delete(ATraceable: ITraceable);
var
	DeleteIndex: Integer;
begin
	DeleteIndex := fList.IndexOf(ATraceable);
	fList.Delete(DeleteIndex);
end;

destructor TTraceableList.Destroy;
begin
	fList.Free;
	inherited;
end;

function TTraceableList.DSNodesList: TStringList;
var
	i: Integer;
begin
	if fList.Count > 0 then
	begin
		Result := TStringList.Create;
		Result.Sorted := True;
		Result.Duplicates := dupIgnore;
		for i := 0 to fList.Count - 1 do
			Result.Add(ITraceable(fList[i]).DS);
	end
	else
		Result := nil;
end;

function TTraceableList.GetItem(AIndex: Integer): ITraceable;
begin
	Result := ITraceable(fList[AIndex]);
end;

function TTraceableList.IndexOf(ATraceable: ITraceable): Integer;
begin
	Result := fList.IndexOf(ATraceable);
end;

function TTraceableList.NodesList: TStringList;
var
	i: Integer;
begin
	if fList.Count > 0 then
	begin
		Result := TStringList.Create;
		Result.Sorted := True;
		Result.Duplicates := dupIgnore;
		for i := 0 to fList.Count - 1 do
		begin
			Result.Add(ITraceable(fList[i]).DS);
			Result.Add(ITraceable(fList[i]).US);
		end;
	end
	else
		Result := nil;
end;

function TTraceableList.USNodesList: TStringList;
var
	i: Integer;
begin
	if fList.Count > 0 then
	begin
		Result := TStringList.Create;
		Result.Sorted := True;
		Result.Duplicates := dupIgnore;
		for i := 0 to fList.Count - 1 do
			Result.Add(ITraceable(fList[i]).DS);
	end
	else
		Result := nil;
end;

end.
