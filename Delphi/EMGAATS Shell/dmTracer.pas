unit dmTracer;

interface

uses
  SysUtils, Classes, IniFiles, DB, ADODB, uEMGAATSModelConfig, RzPrgres, Contnrs,
  RzErrHnd, uEMGAATSModel;

type
	TLinkData = class
  public
    ID: String;
    USNode: String;
    DSNode: String;
    Reach: String;
    ReachElement: String;
    Visited: Boolean;
    constructor Create(ALinkID: String; AUSNode: String; ADSNode: String;
      AReach: String = ''; AReachElement: String = '');
  end;

  TdatmodTracer = class(TDataModule)
    adoSource: TADOQuery;
    adoConnection: TADOConnection;
  private
  private
    fModelConfig: TModelConfig;
    fLinks: TStringList;
    fDSNodes: TStringList;
    fRoots: TObjectList;
    fStops: TObjectList;
    fNumTracedLinks: Integer;
    fprgProgress: TRzProgressBar;
    fModel: TEMGAATSModel;
    procedure Connect;
    procedure LoadLinks;
    procedure LoadRoots;
    procedure LoadStops(IsRequired: Boolean);
    procedure Trace;
    procedure ExportData;
    procedure FreeLists;
  public
    { Public declarations }
    function TraceNetwork(ModelConfig: TModelConfig; AModel: TEMGAATSModel; RootsOnly: Boolean = False): Boolean;
    property NumTracedLinks: Integer read fNumTracedLinks;
    property ProgressBar: TRzProgressBar read fprgProgress write fprgProgress;
  end;

var
  datmodTracer: TdatmodTracer;

implementation

uses StStrms, CodeSiteLogging, Forms, uEMGAATSTypes;

{$R *.dfm}

{ TLinkData }

constructor TLinkData.Create(ALinkID: String; AUSNode: String; ADSNode: String;
  AReach: String = ''; AReachElement: String = '');
begin
  ID := ALinkID;
  USNode := AUSNode;
  DSNode := ADSNode;
  Reach := AReach;
  ReachElement := AReachElement;
end;

{ TdatmodTracer }

procedure TdatmodTracer.Connect;
begin
  adoConnection.ConnectionString :=
    'Provider=Microsoft.Jet.OLEDB.4.0;Data Source=' +
		fModelConfig.TraceSourceDatabase + ';Persist Security Info=False';

	adoSource.CacheSize := 66000;
	adoSource.SQL.Clear;
	adoSource.SQL.Add('Select ['+ fModelConfig.LinkField + '], [' +
    fModelConfig.USNodeField + '], [' +
    fModelConfig.DSNodeField + ']' +
	  ' FROM [' + fModelConfig.TraceSourceTable + ']' +
	  ' WHERE ((([' + fModelConfig.LinkField + ']) IS NOT NULL) AND (([' +
    fModelConfig.USNodeField +	']) IS NOT NULL) AND (([' +
    fModelConfig.DSNodeField + ']) IS NOT NULL))');
end;

procedure TdatmodTracer.ExportData;
var
  CurrentLink: TLinkData;
  DSNodeIndex: Integer;
  NumVisited: Integer;
  OutputFile: TStAnsiTextStream;
  OutputFileStream: TFileStream;
  WriteText: String;
begin
  OutputFileStream := TFileStream.Create(fModelConfig.TraceFileName, fmCreate);
  OutputFile := TStAnsiTextStream.Create(OutputFileStream);

  try
    WriteText := Format('%s,%s,%s,%s,%s', [
      fModelConfig.LinkField, fModelConfig.USNodeField, fModelConfig.DSNodeField,
      fModelConfig.ReachField, fModelConfig.ElementField]);
    OutputFile.WriteLine(WriteText);

    NumVisited := 0;
    for DSNodeIndex := 0 to fDSNodes.Count - 1 do
    begin
      CurrentLink := fDSNodes.Objects[DSNodeIndex] as TLinkData;
      if CurrentLink.Visited then
      begin
        Inc(NumVisited);
        WriteText := Format('%s,%s,%s,%s,%s', [
          CurrentLink.ID, CurrentLink.USNode, CurrentLink.DSNode,
          CurrentLink.Reach, CurrentLink.ReachElement]);
        OutputFile.WriteLine(WriteText)
      end;
    end;

    fNumTracedLinks := NumVisited;
  finally
    OutputFile.Free;
    OutputFileStream.Free;
  end;
end;

procedure TdatmodTracer.FreeLists;
var
  i: Integer;
begin
  for i := 0 to fLinks.Count - 1 do
    fLinks.Objects[i].Free;

  fLinks.Free;
  fDSNodes.Free;
  fRoots.Free;
  fStops.Free;
end;

procedure TdatmodTracer.LoadLinks;
var
  LinksCount: Integer;
  CurrentLink: TLinkData;
  LinkFieldName: String;
  USNodeFieldName: String;
  DSNodeFieldName: String;
begin
  fLinks := TStringList.Create;
  fLinks.Sorted := True;
  fLinks.Duplicates := dupIgnore;

  fDSNodes := TStringList.Create;
  fDSNodes.Sorted := True;
  fDSNodes.Duplicates := dupAccept;

  adoConnection.Open;
  adoSource.Open;
  adoSource.First;
  fLinks.Capacity := adoSource.RecordCount;
  fDSNodes.Capacity := adoSource.RecordCount;

  if Assigned(fprgProgress) then
    fprgProgress.TotalParts := adoSource.RecordCount;

  LinkFieldName := fModelConfig.LinkField;
  USNodeFieldName := fModelConfig.USNodeField;
  DSNodeFieldName := fModelConfig.DSNodeField;

  LinksCount := 0;
  while not adoSource.Recordset.EOF do
  begin
    CurrentLink := TLinkData.Create(
      String(adoSource.Recordset.Fields[LinkFieldName].Value),
      String(adoSource.Recordset.Fields[USNodeFieldName].Value),
      String(adoSource.Recordset.Fields[DSNodeFieldName].Value));
    CurrentLink.Visited := False;

    fLinks.AddObject(CurrentLink.ID, CurrentLink);
    fDSNodes.AddObject(CurrentLink.DSNode, CurrentLink);
    Inc(LinksCount);

    if Assigned(fprgProgress) and ((LinksCount mod 100) = 0) then
    begin
      fprgProgress.IncParts(100);
      fprgProgress.Update;
      Application.ProcessMessages;
    end;

    adoSource.RecordSet.MoveNext;
  end;

	adoSource.Close;
  adoConnection.Close;

end;

procedure TdatmodTracer.LoadRoots;
var
  i: Integer;
  RootID: Integer;
  RootLink: TLinkData;
  LinkIndex: Integer;
begin
  fRoots := TObjectList.Create(False);
  for i := 0 to fModelConfig.RootLinksCount - 1 do
  begin
    RootID := fModelConfig.RootLinks[i];
    LinkIndex := fLinks.IndexOf(IntToStr(RootID));
    if LinkIndex > -1 then
    begin
      RootLink := fLinks.Objects[LinkIndex] as TLinkData;
      fRoots.Add(RootLink);
    end
    else
    begin
      fModel.AddError(TEMGAATSError.Create(Format('Root link %d not found in master links. Ignored in trace.',
          [RootID]), eetWarning));
    end;
  end;
  if fRoots.Count < 1 then
    CodeSite.Send('No roots found'); // No roots found
end;

procedure TdatmodTracer.LoadStops(IsRequired: Boolean);
var
  i: Integer;
  StopID: Integer;
  StopLink: TLinkData;
  LinkIndex: Integer;
begin
  if IsRequired then
  begin
    fStops := TObjectList.Create(False);
    for i := 0 to fModelConfig.StopLinksCount - 1 do
    begin
      StopID := fModelConfig.StopLinks[i];
      LinkIndex := fLinks.IndexOf(IntToStr(StopID));
      if LinkIndex > -1 then
      begin
        StopLink := fLinks.Objects[LinkIndex] as TLinkData;
        fStops.Add(StopLink)
      end
      else
      begin
        fModel.AddError(TEMGAATSError.Create(Format('Stop link %d not found in master links. Ignored in trace.',
          [StopID]), eetWarning));
      end;
    end;
    if fStops.Count < 1 then
      CodeSite.Send('No stops found'); // No stops found
  end;
end;

function TdatmodTracer.TraceNetwork(ModelConfig: TModelConfig;
  AModel: TEMGAATSModel; RootsOnly: Boolean): Boolean;
begin
  try
    fModelConfig := ModelConfig;
    fModel := AModel;
    fNumTracedLinks := 0;
    Connect;
    LoadLinks;
    LoadRoots;
    LoadStops(not RootsOnly);
    Trace;
    ExportData;
    FreeLists;
    Result := True;
  except
    Result := False;
  end;
end;

procedure TdatmodTracer.Trace;
var
  fSearchStack: TObjectStack;
  i: Integer;
  CurrentLink: TLinkData;
  ParentLink: TLinkData;
  CurrentDSNode: String;
  DSNodeIndex: Integer;
  BranchCount: Integer;
begin
  fSearchStack := TObjectStack.Create;

  // Initialize the search with the roots
  for i := 0 to fRoots.Count - 1 do
  begin
    CurrentLink := fRoots[i] as TLinkData;
    CurrentLink.Reach := IntToStr(i+1);
    CurrentLink.ReachElement := IntToStr(1);
    fSearchStack.Push(CurrentLink);
  end;

  // Crawl up the connecting links; don't connect if we hit a stop pipe
  while fSearchStack.Count > 0 do
  begin
    CurrentLink := fSearchStack.Pop as TLinkData;
    if CurrentLink.Visited then
      Continue;
    CurrentLink.Visited := True;
    CurrentDSNode := CurrentLink.USNode;

    if fStops.IndexOf(CurrentLink) > -1 then
      Continue;

    // Add connecting pipes
    DSNodeIndex := fDSNodes.IndexOf(CurrentDSNode);
    if DSNodeIndex > -1 then
    begin
      ParentLink := CurrentLink;
      BranchCount := 1;
      while (DSNodeIndex < fDSNodes.Count) and (fDSNodes[DSNodeIndex] = CurrentDSNode) do
      begin
        CurrentLink := fDSNodes.Objects[DSNodeIndex] as TLinkData;
        CurrentLink.Reach := ParentLink.Reach + '.' + IntToStr(BranchCount);
        CurrentLink.ReachElement := IntToStr(1);
        fSearchStack.Push(CurrentLink);
        Inc(DSNodeIndex);
        Inc(BranchCount);
      end;
      if BranchCount = 2 then // continuing a branch
      begin
        CurrentLink.Reach := ParentLink.Reach;
        CurrentLink.ReachElement := IntToStr((StrToInt(ParentLink.ReachElement))+1)
      end;
    end
    else
  end;

  fSearchStack.Free;
end;

end.
