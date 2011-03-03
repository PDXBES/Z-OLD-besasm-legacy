unit uXPResultsProcessor;

interface

uses SysUtils, Classes, dmXPExport, StStrms;

type

  TXPResultsProcessor = class
  private
    dmodXPExport: TdmodXPExport;
    fResultsStorePath: TFileName;
    fResultsPath: TFileName;
    InStream: TFileStream;
    InTextStream: TStAnsiTextStream;
    CurrentLine: String;
    fBeginTime: TDateTime;
    fVersionNum: Double;
    function GetResultsPath: TFileName;
    function GetResultsStorePath: TFileName;
    function GetResultsStoreConnectionString: String;

    procedure ConnectResultsStore;

    ///<summary>
    ///  Build tables for the results store
    ///</summary>
    procedure CreateResultsStore;

    ///<summary>
    ///  Load an existing results store
    ///</summary>
    procedure LoadResultsStore;

    ///<summary>
    ///  Drop tables from the results store if they already exist
    ///</summary>
    procedure ClearResultsStore;

    procedure BuildResultsTables;

    procedure PrepareReading;
    procedure ReadVersion;
    procedure ReadBeginTime;
    procedure ReadJunctionSummary;
    procedure ReadConduitSummary;
    procedure ReadJunctionFloodingAndVolume;
    procedure CreateHGLTable;
    procedure FinalizeReading;
  public
    constructor Create;
    destructor Destroy; override;

    procedure ProcessResults(ResultsFile: TFileName; ResultsStore: TFileName);

    property VersionNum: Double read fVersionNum;
    property BeginTime: TDateTime read fBeginTime;
    property ResultsStorePath: TFileName read GetResultsStorePath
      write fResultsStorePath;
    property ResultsStoreConnectionString: String read GetResultsStoreConnectionString;
    property ResultsPath: TFileName read GetResultsPath write fResultsPath;

  end;

implementation

uses ADOX_TLB, StStrL, DateUtils, Forms, Variants, uEMGAATSModel, StrUtils;

{ TXPResultProcessor }

procedure TXPResultsProcessor.BuildResultsTables;
var
  ACatalog: Catalog;
  ATable: Table;
begin
  // Build tables
  dmodXPExport.adoOutCommand.CommandText :=
    'CREATE TABLE tableE09 ('+
    'NodeName TEXT(10),'+
    'GrElev DOUBLE,'+
    'MaxCrown DOUBLE,'+
    'MaxJElev DOUBLE,'+
    'TimeOfMax DATETIME,'+
    'Surcharge DOUBLE,'+
    'Freeboard DOUBLE,'+
    'MaxArea DOUBLE,'+
    'CONSTRAINT idxPrimary PRIMARY KEY (NodeName));';
  dmodXPExport.adoOutCommand.Execute;
  dmodXPExport.adoOutCommand.CommandText :=
    'CREATE TABLE tableE10 ('+
    'CondName TEXT(10),'+
    'DesignQ DOUBLE,'+
    'DesignV DOUBLE,'+
    'MaxD DOUBLE,'+
    'MaxQ DOUBLE,'+
    'TimeMaxQ DATETIME,'+
    'MaxV DOUBLE,'+
    'TimeMaxV DATETIME,'+
    'QqRatio DOUBLE,'+
    'MaxUsElev DOUBLE,'+
    'MaxDsElev DOUBLE,'+
    'CONSTRAINT idxPrimary PRIMARY KEY (CondName));';
  dmodXPExport.adoOutCommand.Execute;
  dmodXPExport.adoOutCommand.CommandText :=
    'CREATE TABLE tableE20 ('+
    'NodeName TEXT(10),'+
    'SurchargeTime DOUBLE,'+
    'FloodedTime DOUBLE,'+
    'FloodVol DOUBLE,'+
    'MaxStoredVol DOUBLE,'+
    'PondingVol DOUBLE,'+
    'CONSTRAINT idxPrimary PRIMARY KEY (NodeName));';
  dmodXPExport.adoOutCommand.Execute;

  // Build linked tables
  ACatalog := CoCatalog.Create;
  ACatalog.Set_ActiveConnection(dmodXPExport.adoOutConnection.ConnectionObject);
  ATable := CoTable.Create;
  ATable.ParentCatalog := ACatalog;
  ATable.Name := 'mdl_dirsc_ac';
  ATable.Properties['Jet OLEDB:Link Datasource'].Value :=
    IncludeTrailingPathDelimiter(CurrentModel.Path) + '\dsc\mdl_dirsc_ac.mdb';
  ATable.Properties['Jet OLEDB:Remote Table Name'].Value := 'mdl_dirsc_ac';
  ATable.Properties['Jet OLEDB:Create Link'].Value := True;
  ACatalog.Tables.Append(ATable);

  ATable := CoTable.Create;
  ATable.ParentCatalog := ACatalog;
  ATable.Name := 'mdl_links_ac';
  ATable.Properties['Jet OLEDB:Link Datasource'].Value :=
    IncludeTrailingPathDelimiter(CurrentModel.Path) + '\links\mdl_links_ac.mdb';
  ATable.Properties['Jet OLEDB:Remote Table Name'].Value := 'mdl_links_ac';
  ATable.Properties['Jet OLEDB:Create Link'].Value := True;
  ACatalog.Tables.Append(ATable);

	//JHB - 11/19/2007 Changed query to default CalculatedDHGL to default to 10 for properties connected to non-surcharged pipes

  dmodXPExport.adoOutCommand.CommandText :=
    'CREATE VIEW ComputeHGL AS '+
    'SELECT mdl_DirSC_ac.DSCID, mdl_DirSC_ac.ParcelID, mdl_DirSC_ac.DivideID, tableE09.MaxJElev AS USJelev, ' +
  		'tableE09_1.MaxJElev AS DSJelev, mdl_DirSC_ac.Frac2SwrBeg, ' +
  		'CDbl(Format$([tableE09].[maxjelev]-([Frac2swrbeg]*([tableE09].[maxjelev]-[tableE09_1].[maxjelev])),"Fixed")) AS CalculatedHGL, ' +
  		'CDbl(Format$([FloodRefElev]-([tableE09].[maxjelev]-([Frac2swrbeg]*([tableE09].[maxjelev]-[tableE09_1].[maxjelev]))),"Fixed")) AS CalculatedDHGL, ' +
  		'IIf([tableE09].[Surcharge]>[tableE09_1].[Surcharge],[tableE09].[Surcharge],[tableE09_1].[Surcharge]) AS Surcharge ' +
		'FROM mdl_DirSC_ac INNER JOIN ((mdl_Links_ac INNER JOIN tableE09 ON mdl_Links_ac.USNode = tableE09.NodeName) ' +
		'INNER JOIN tableE09 AS tableE09_1 ON mdl_Links_ac.DSNode = tableE09_1.NodeName) ON mdl_DirSC_ac.ToLinkSan = mdl_Links_ac.LinkID;';
  //  'CREATE VIEW ComputeHGL AS '+
  //  'SELECT mdl_DirSC_ac.DSCID, mdl_DirSC_ac.ParcelID, mdl_DirSC_ac.DivideID, tableE09.MaxJElev AS USJelev, ' +
  //    'tableE09_1.MaxJElev AS DSJelev, mdl_DirSC_ac.Frac2SwrBeg, '+
  //    'CDbl(Format$([tableE09].[maxjelev]-([Frac2swrbeg]*([tableE09].[maxjelev]-[tableE09_1].[maxjelev])),"Fixed")) AS CalculatedHGL, '+
  //    'CDbl(Format$([FloodRefElev]-([tableE09].[maxjelev]-([Frac2swrbeg]*([tableE09].[maxjelev]-[tableE09_1].[maxjelev]))),"Fixed")) AS CalculatedDHGL '+
  //  'FROM mdl_DirSC_ac INNER JOIN ((mdl_Links_ac INNER JOIN tableE09 ON mdl_Links_ac.USNode = tableE09.NodeName) '+
  //    'INNER JOIN tableE09 AS tableE09_1 ON mdl_Links_ac.DSNode = tableE09_1.NodeName) ON mdl_DirSC_ac.ToLinkSan = mdl_Links_ac.LinkID;';      	
  dmodXPExport.adoOutCommand.Execute;  

end;

procedure TXPResultsProcessor.ClearResultsStore;
var
  ACatalog: Catalog;
  i: Integer;
begin
  if VarIsEmpty(dmodXPExport.adoOutConnection.ConnectionObject) then
    Exit;

  ACatalog := CoCatalog.Create;
  ACatalog.Set_ActiveConnection(dmodXPExport.adoOutConnection.ConnectionObject);

  for i := 0 to ACatalog.Tables.Count-1 do
  begin
    if ACatalog.Tables[i].Name = 'tableE09' then
    begin
      dmodXPExport.adoOutCommand.CommandText := 'DROP TABLE tableE09';
      dmodXPExport.adoOutCommand.Execute;
      Continue;
    end;
    if ACatalog.Tables[i].Name = 'tableE10' then
    begin
      dmodXPExport.adoOutCommand.CommandText := 'DROP TABLE tableE10';
      dmodXPExport.adoOutCommand.Execute;
      Continue;
    end;
    if ACatalog.Tables[i].Name = 'tableE20' then
    begin
      dmodXPExport.adoOutCommand.CommandText := 'DROP TABLE tableE20';
      dmodXPExport.adoOutCommand.Execute;
      Continue;
    end;
    if ACatalog.Tables[i].Name = 'DSCHGLs' then
    begin
      dmodXPExport.adoOutCommand.CommandText := 'DROP TABLE DSCHGLs';
      dmodXPExport.adoOutCommand.Execute;
      Continue;
    end;
  end;
end;

procedure TXPResultsProcessor.ConnectResultsStore;
begin
  if dmodXPExport.adoOutConnection.Connected then
    dmodXPExport.adoOutConnection.Close;

  dmodXPExport.adoOutConnection.ConnectionString := ResultsStoreConnectionString;
  dmodXPExport.adoOutConnection.Open;
end;

constructor TXPResultsProcessor.Create;
begin
  dmodXPExport := TdmodXPExport.Create(Application);
end;

procedure TXPResultsProcessor.CreateHGLTable;
begin
  dmodXPExport.adoOutCommand.CommandText :=
    'SELECT ComputeHGL.* INTO DSCHGLs FROM ComputeHGL;';
  dmodXPExport.adoOutCommand.Execute;
end;

procedure TXPResultsProcessor.CreateResultsStore;
var
  ACatalog: Catalog;
begin
  if not FileExists(fResultsStorePath) then
  begin
    ACatalog := CoCatalog.Create;

    ACatalog.Create(ResultsStoreConnectionString);

    ConnectResultsStore;
    BuildResultsTables;
  end
  else
  begin
    ConnectResultsStore;
    LoadResultsStore;
  end;
end;

destructor TXPResultsProcessor.Destroy;
begin
  dmodXPExport.Free;
  inherited;
end;

procedure TXPResultsProcessor.FinalizeReading;
begin
  InTextStream.Free;
  InStream.Free;
end;

function TXPResultsProcessor.GetResultsPath: TFileName;
begin
  Result := fResultsPath;
end;

function TXPResultsProcessor.GetResultsStoreConnectionString: String;
begin
  Result := 'Provider=Microsoft.Jet.OLEDB.4.0;' +
    'Data Source='+fResultsStorePath+';';

end;

function TXPResultsProcessor.GetResultsStorePath: TFileName;
begin
  Result := fResultsStorePath;
end;

procedure TXPResultsProcessor.LoadResultsStore;
begin
  if VarIsEmpty(dmodXPExport.adoOutConnection.ConnectionObject) then
    Exit;

  ClearResultsStore;
  BuildResultsTables;
end;

procedure TXPResultsProcessor.PrepareReading;
begin
  InStream := TFileStream.Create(ResultsPath, fmOpenRead or fmShareDenyWrite);
  InTextStream := TStAnsiTextStream.Create(InStream);
end;

procedure TXPResultsProcessor.ProcessResults(ResultsFile,
  ResultsStore: TFileName);
begin
  ResultsPath := ResultsFile;
  ResultsStorePath := ResultsStore;

  CreateResultsStore;
  PrepareReading;
  ReadVersion;
  ReadBeginTime;
  ReadJunctionSummary;
  ReadConduitSummary;
  ReadJunctionFloodingAndVolume;
  CreateHGLTable;
  FinalizeReading;
end;

procedure TXPResultsProcessor.ReadBeginTime;
var
  BeginYear: Word;
  BeginMonth: Word;
  BeginDay: Word;
  BeginHour: Word;
  BeginMinute: Word;
  BeginSecond: Word;
  Tokens: TStringList;
begin
  CurrentLine := InTextStream.ReadLine;
  Tokens := TStringList.Create;
  while not InTextStream.AtEndOfStream do
  begin
    if CurrentLine <> ' Time Control from Hydraulics Job Control ' then
    begin
      CurrentLine := InTextStream.ReadLine;
      Continue;
    end;
    CurrentLine := InTextStream.ReadLine;
    ExtractTokensL(CurrentLine, ' ', '''', False, Tokens);
    BeginYear := StrToInt(Tokens[1]);
    if BeginYear < 100 then
      BeginYear := BeginYear + 1900;
    BeginMonth := StrToInt(Tokens[3]);
    CurrentLine := InTextStream.ReadLine;
    ExtractTokensL(CurrentLine, ' ', '''', False, Tokens);
    BeginDay := StrToInt(Tokens[1]);
    BeginHour := StrToInt(Tokens[3]);
    CurrentLine := InTextStream.ReadLine;
    ExtractTokensL(CurrentLine, ' ', '''', False, Tokens);
    BeginMinute := StrToInt(Tokens[1]);
    BeginSecond := StrToInt(Tokens[3]);
    fBeginTime := EncodeDateTime(BeginYear, BeginMonth, BeginDay, BeginHour,
      BeginMinute, BeginSecond, 0);
    Break;
  end;
  Tokens.Free;
end;

procedure TXPResultsProcessor.ReadConduitSummary;
var
  Tokens: TStringList;
  Hour: Word;
  Minute: Word;
  EncodedTime: TDateTime;
  i: Integer;
const
  Pre8_29_ConduitHeaderLinesToSkip = 11;
  Post8_29_conduitHeaderLinesToSkip = 14;
  NumTokensForValidSummary = 15;
begin
  dmodXPExport.adoTableE10.Open;
  Tokens := TStringList.Create;
  while not InTextStream.AtEndOfStream do
  begin
    if CurrentLine <> ' |        Table E10 - CONDUIT SUMMARY STATISTICS        |' then
    begin
      CurrentLine := InTextStream.ReadLine;
{
        frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
        frmStatus.Update;
}
      Continue;
    end;

    if (VersionNum >= 8.0) and (VersionNum < 8.29) then
      for i := 1 to Pre8_29_ConduitHeaderLinesToSkip do
        CurrentLine := InTextStream.ReadLine
    else if (VersionNum >=8.29) then
      for i := 1 to Post8_29_conduitHeaderLinesToSkip do
        CurrentLine := InTextStream.ReadLine;

    while CurrentLine <> '' do
    begin
      //CodeSite.SendMsg(CurrentLine);
      ExtractTokensL(CurrentLine, ' ', '''', False, Tokens);
      dmodXPExport.adoTableE10.Append;
      if Tokens.Count >= NumTokensForValidSummary then
      begin
        dmodXPExport.adoTableE10CondName.Value := Tokens[0];
        dmodXPExport.adoTableE10DesignQ.Value := StrToFloat(Tokens[1]);
        dmodXPExport.adoTableE10DesignV.Value := StrToFloat(Tokens[2]);
        dmodXPExport.adoTableE10MaxD.Value := StrToFloat(Tokens[3]);
        dmodXPExport.adoTableE10MaxQ.Value := StrToFloat(Tokens[4]);

        // Get MaxQTime
        Hour := StrToInt(Tokens[5]);
        Minute := StrToInt(Tokens[6]);
        EncodedTime := BeginTime + (Hour+(Minute/60))/24;
        dmodXPExport.adoTableE10TimeMaxQ.Value := EncodedTime;
        dmodXPExport.adoTableE10MaxV.Value := StrToFloat(Tokens[7]);

        // Get MaxVTime
        Hour := StrToInt(Tokens[8]);
        Minute := StrToInt(Tokens[9]);
        EncodedTime := BeginTime + (Hour+(Minute/60))/24;
        dmodXPExport.adoTableE10TimeMaxV.Value := EncodedTime;

        dmodXPExport.adoTableE10QqRatio.Value := StrToFloat(Tokens[10]);
        dmodXPExport.adoTableE10MaxUsElev.Value := StrToFloat(Tokens[11]);
        dmodXPExport.adoTableE10MaxDsElev.Value := StrToFloat(Tokens[12]);
      end
      else
      begin
        CurrentLine := InTextStream.ReadLine;
{
          frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
          frmStatus.Update;
}
        Continue;
      end;
      dmodXPExport.adoTableE10.Post;
      CurrentLine := InTextStream.ReadLine;
{
        frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
        frmStatus.Update;
}
    end;
    Break;
  end;
  Tokens.Free;
{
    if dmodXPExport.adoTableE10.RecordCount = 0 then
      Result := 0;
}
  dmodXPExport.adoTableE10.Close;
end;

procedure TXPResultsProcessor.ReadJunctionFloodingAndVolume;
var
  i: Integer;
  Tokens: TStringList;
const
  Pre8_29_JunctionHeaderLinesToSkip = 18;
  Post8_29_JunctionHeaderLinesToSkip = 18;
  NumTokensForValidSummary = 6;
begin
  dmodXPExport.adoTableE20.Open;
  Tokens := TStringList.Create;
  while not InTextStream.AtEndOfStream do
  begin
    if CurrentLine <> '   | Table E20 - Junction Flooding and Volume Listing.   |' then
    begin
      CurrentLine := InTextStream.ReadLine;
{
        frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
        frmStatus.Update;
}
      Continue;
    end;

    if (VersionNum >= 8.0) and (VersionNum < 8.29) then
      for i := 1 to Pre8_29_JunctionHeaderLinesToSkip do
        CurrentLine := InTextStream.ReadLine
    else if (VersionNum >=8.29) then
      for i := 1 to Post8_29_JunctionHeaderLinesToSkip do
        CurrentLine := InTextStream.ReadLine;

    while CurrentLine <> '' do
    begin
      ExtractTokensL(CurrentLine, ' ', '''', False, Tokens);
      dmodXPExport.adoTableE20.Append;
      if Tokens.Count >= NumTokensForValidSummary then
      begin
        dmodXPExport.adoTableE20NodeName.Value := Tokens[0];
        dmodXPExport.adoTableE20SurchargeTime.Value := StrToFloat(Tokens[1]);
        dmodXPExport.adoTableE20FloodedTime.Value := StrToFloat(Tokens[2]);
        dmodXPExport.adoTableE20FloodVol.Value := StrToFloat(Tokens[3]);
        dmodXPExport.adoTableE20MaxStoredVol.Value := StrToFloat(Tokens[4]);
        dmodXPExport.adoTableE20pondingVol.Value := StrToFloat(Tokens[5]);
      end
      else
      begin
        CurrentLine := InTextStream.ReadLine;
{
          frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
          frmStatus.Update;
}
        Continue;
      end;
      dmodXPExport.adoTableE20.Post;
      CurrentLine := InTextStream.ReadLine;
{
        frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
        frmStatus.Update;
}
    end;
    Break;
  end;
{
    if dmodXPExport.adoTableE20.RecordCount = 0 then
      Result := 0;
}
  dmodXPExport.adoTableE20.Close;
  Tokens.Free;
end;

procedure TXPResultsProcessor.ReadJunctionSummary;
var
  Tokens: TStringList;
  Hour: Word;
  Minute: Word;
  EncodedTime: TDateTime;
  i: Integer;
begin
  dmodXPExport.adoTableE09.Open;
  Tokens := TStringList.Create;
  while not InTextStream.AtEndOfStream do
  begin
    if CurrentLine <> ' |        Table E9 - JUNCTION SUMMARY STATISTICS        |' then
    begin
      CurrentLine := InTextStream.ReadLine;
{
        frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
        frmStatus.Update;
}
      Continue;
    end;
    for i := 1 to 10 do
      CurrentLine := InTextStream.ReadLine;
    while CurrentLine <> '' do
    begin
      //CodeSite.SendMsg(CurrentLine);
      ExtractTokensL(CurrentLine, ' ', '''', False, Tokens);
      dmodXPExport.adoTableE09.Append;
      dmodXPExport.adoTableE09NodeName.Value := Tokens[0];
      dmodXPExport.adoTableE09GrElev.Value := StrToFloat(Tokens[1]);
      dmodXPExport.adoTableE09MaxCrown.Value := StrToFloat(Tokens[2]);
      dmodXPExport.adoTableE09MaxJElev.Value := StrToFloat(Tokens[3]);

      // Get MaxTime
      Hour := StrToInt(Tokens[4]);
      Minute := StrToInt(Tokens[5]);
      EncodedTime := BeginTime + (Hour+(Minute/60))/24;
      dmodXPExport.adoTableE09TimeOfMax.Value := EncodedTime;

      dmodXPExport.adoTableE09Surcharge.Value := StrToFloat(Tokens[6]);
      dmodXPExport.adoTableE09Freeboard.Value := StrToFloat(Tokens[7]);
      try // some results (with 10^X format) incorrectly written as float+exp (9+10) instead of
          //   floatE+xp (9E+10)
        dmodXPExport.adoTableE09MaxArea.Value := StrToFloat(Tokens[8]);
      except
        on EConvertError do
          dmodXPExport.adoTableE09MaxArea.Value := 0;
      end;
      dmodXPExport.adoTableE09.Post;
      CurrentLine := InTextStream.ReadLine;
{
        frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
        frmStatus.Update;
}
    end;
    Break;
  end;
{
    if adoTableE09.RecordCount = 0 then
      Result := 0;
}
  dmodXPExport.adoTableE09.Close;
  Tokens.Free;
end;

procedure TXPResultsProcessor.ReadVersion;
var
  Tokens: TStringList;
  MultiVersionTokens: TStringList;
  MajorVersionNum: Integer;
  MinorVersionNum: Integer;
  Version: string;
  VersionNum: Double;
begin
  CurrentLine := InTextStream.ReadLine;
  Tokens := TStringList.Create;
	while not InTextStream.AtEndOfStream do
	begin
		if (CurrentLine <> '          |           Storm Water Management Model        |') and
			(CurrentLine <> '          |     Storm and Wastewater Management Model     |') then
		begin
			CurrentLine := InTextStream.ReadLine;
			Continue;
		end
		else
      Break;
	end;
	CurrentLine := InTextStream.ReadLine;
	ExtractTokensL(CurrentLine, ' ', '''', False, Tokens);
	Version := Tokens[2];  // These pre 10.x versions always in x.x format
  if not TryStrToFloat(Version, VersionNum) then
  begin
    CurrentLine := InTextStream.ReadLine;
		ExtractTokensL(CurrentLine, ' ', '''', False, Tokens);
    if Tokens.Count > 3 then
			Version := Tokens[3]
    else
      Version := Tokens[0]; // Read gibberish to make the next test true and move to the next version read
    if not TryStrToFloat(Version, VersionNum) then // 10.x versions user "Engine Version: x.x" instead of "Version: x.x"
    begin
      if Tokens.Count > 3 then
      begin
        MultiVersionTokens := TStringList.Create;
        ExtractTokensL(Tokens[3], '.', '''', False, MultiVersionTokens);
        if MultiVersionTokens.Count > 3 then
          if TryStrToInt(MultiVersionTokens[0], MajorVersionNum) then
            if TryStrToInt(MultiVersionTokens[1], MinorVersionNum) then
            begin
              Version := Format('%d.%d', [MajorVersionNum, MinorVersionNum]);
              if not TryStrToFloat(Version, VersionNum)
                then VersionNum := 0;
            end;
        MultiVersionTokens.Free;
      end
      else
      begin
        // 2010 version really switches crap around
        CurrentLine := InTextStream.ReadLine;
        while not InTextStream.AtEndOfStream do
        begin
          if (CurrentLine <> '          |                                               |')then
          begin
            CurrentLine := InTextStream.ReadLine;
            Continue;
          end
          else
            Break;
        end;
        CurrentLine := InTextStream.ReadLine;
        CurrentLine := InTextStream.ReadLine;
        CurrentLine := InTextStream.ReadLine;
        CurrentLine := InTextStream.ReadLine;
        ExtractTokensL(CurrentLine, ' ', '''', False, Tokens);
        Version := Tokens[4];
        if not TryStrToFloat(Version, VersionNum) then
          raise Exception.Create('Can''t detect version.');
      end;
    end;
  end;
  fVersionNum := VersionNum;
  Tokens.Free;
end;

end.
