library XPResults;

{ Important note about DLL memory management: ShareMem must be the
	first unit in your library's USES clause AND your project's (select
	Project-View Source) USES clause if your DLL exports any procedures or
	functions that pass strings as parameters or function results. This
	applies to all strings passed to and from your DLL--even those that
	are nested in records and classes. ShareMem is the interface unit to
	the BORLNDMM.DLL shared memory manager, which must be deployed along
	with your DLL. To avoid using BORLNDMM.DLL, pass string information
	using PChar or ShortString parameters. }

uses
	SysUtils,
	Classes, COMObj,
	StStrms,
  Dialogs,
	ADOX_TLB,
	StStrL,
	DateUtils,
	Forms,
	CodeSiteLogging,
	dmXPExport in 'dmXPExport.pas', {dmodXPExport: TDataModule}
	fStatus in 'fStatus.pas' {frmStatus};


function getXPresults(InPath, OutPath: PChar): Integer; stdcall;
var
  VersionNum: Double;
	InStream: TFileStream;
  InTextStream: TStAnsiTextStream;
  ACatalog: Catalog;
  ResultsConnectionString: String;
  i: Integer;
  CurrentLine: String;
	Tokens: TStringList;
  MultiVersionTokens: TStringList;
  MajorVersionNum: Integer;
  MinorVersionNum: Integer;
  BeginYear, BeginMonth, BeginDay, BeginHour, BeginMinute, BeginSecond: Word;
  BeginTime: TDateTime;
  Year, Month, Day: Word;
  Hour, Minute: Word;
  EncodedTime: TDateTime;
	StrInPath, StrOutPath: String;
	Version: String;
begin
	dmodXPExport := TdmodXPExport.Create(Application);
  Result := -1;

	with dmodXPExport do
  begin
    StrInPath := String(InPath);
    StrOutPath := String(OutPath);
    frmStatus := TfrmStatus.Create(Application);
    frmStatus.lblInFile.Caption := 'Parsing '+StrInPath;
    frmStatus.lblOutFile.Caption := 'Storing at '+StrOutPath;
    frmStatus.prgFile.PartsComplete := 0;
		frmStatus.Show;

		InStream := TFileStream.Create(StrInPath, fmOpenRead or fmShareDenyWrite);
		InTextStream := TStAnsiTextStream.Create(InStream);
		Tokens := TStringList.Create;
		CodeSite.SendMsg('Connecting to Database');
		try
			try
				ACatalog := CoCatalog.Create;

				ResultsConnectionString :=
					'Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source='+StrOutPath+';'+
					'Mode=Share Deny None;Extended Properties="";Jet OLEDB:System database="";'+
					'Jet OLEDB:Registry Path="";Jet OLEDB:Database Password="";'+
					'Jet OLEDB:Engine Type=4;Jet OLEDB:Database Locking Mode=0;'+
					'Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Global Bulk Transactions=1;'+
					'Jet OLEDB:New Database Password="";Jet OLEDB:Create System Database=False;'+
					'Jet OLEDB:Encrypt Database=False;Jet OLEDB:Don''t Copy Locale on Compact=False;'+
					'Jet OLEDB:Compact Without Replica Repair=False;Jet OLEDB:SFP=False';
				if not FileExists(StrOutPath) then
				begin
					ACatalog.Create(ResultsConnectionString);
				end;

				adoOutConnection.ConnectionString := ResultsConnectionString;

				adoOutConnection.Open;

				ACatalog := CoCatalog.Create;
				ACatalog.Set_ActiveConnection(adoOutConnection.ConnectionObject);

				// Delete tables if they exist
				for i := 0 to ACatalog.Tables.Count-1 do
				begin
					if ACatalog.Tables[i].Name = 'tableE09' then
					begin
						adoOutCommand.CommandText := 'DROP TABLE tableE09';
						adoOutCommand.Execute;
            Continue;
					end;
					if ACatalog.Tables[i].Name = 'tableE10' then
					begin
						adoOutCommand.CommandText := 'DROP TABLE tableE10';
						adoOutCommand.Execute;
						Continue;
					end;
					if ACatalog.Tables[i].Name = 'tableE20' then
					begin
						adoOutCommand.CommandText := 'DROP TABLE tableE20';
						adoOutCommand.Execute;
						Continue;
					end;
				end;

        // Create tables
          adoOutCommand.CommandText :=
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
          adoOutCommand.Execute;
          adoOutCommand.CommandText :=
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
          adoOutCommand.Execute;
					adoOutCommand.CommandText :=
						'CREATE TABLE tableE20 ('+
						'NodeName TEXT(10),'+
						'SurchargeTime DOUBLE,'+
						'FloodedTime DOUBLE,'+
						'FloodVol DOUBLE,'+
						'MaxStoredVol DOUBLE,'+
						'PondingVol DOUBLE,'+
						'CONSTRAINT idxPrimary PRIMARY KEY (NodeName));';
					adoOutCommand.Execute;
					// Read Version
          CodeSite.SendMsg('Reading version');
					CurrentLine := InTextStream.ReadLine;
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
          CodeSite.SendMsg('First version attempt: ' + CurrentLine);
					ExtractTokensL(CurrentLine, ' ', '''', False, Tokens);
					Version := Tokens[2];  // These pre 10.x versions always in x.x format
          if not TryStrToFloat(Version, VersionNum) then
          begin
            CurrentLine := InTextStream.ReadLine;
            CodeSite.SendMsg('Second version attempt: ' + CurrentLine);
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
                      Version := Format('%d.%d', [MajorVersionNum, MinorVersionNum])
              end
              else
              begin
                // 2010 version really switches crap around
                CurrentLine := InTextStream.ReadLine;
                CodeSite.SendMsg('Third version attempt: ' + CurrentLine);
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


					// Read Summaries
					CurrentLine := InTextStream.ReadLine;
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
						BeginTime := EncodeDateTime(BeginYear, BeginMonth, BeginDay, BeginHour,
							BeginMinute, BeginSecond, 0);
						Break;
						frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
						frmStatus.Update;
					end;

					CodeSite.SendMsg('Reading Nodes');
					adoTableE09.Open;
					while not InTextStream.AtEndOfStream do
					begin
						if CurrentLine <> ' |        Table E9 - JUNCTION SUMMARY STATISTICS        |' then
						begin
							CurrentLine := InTextStream.ReadLine;
							frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
							frmStatus.Update;
							Continue;
						end;
						for i := 1 to 10 do
							CurrentLine := InTextStream.ReadLine;
						while CurrentLine <> '' do
						begin
							//CodeSite.SendMsg(CurrentLine);
							ExtractTokensL(CurrentLine, ' ', '''', False, Tokens);
							adoTableE09.Append;
							adoTableE09NodeName.Value := Tokens[0];
							adoTableE09GrElev.Value := StrToFloat(Tokens[1]);
							adoTableE09MaxCrown.Value := StrToFloat(Tokens[2]);
							adoTableE09MaxJElev.Value := StrToFloat(Tokens[3]);
							Hour := StrToInt(Tokens[4]);
							Minute := StrToInt(Tokens[5]);
							EncodedTime := BeginTime + (Hour+(Minute/60))/24;
							adoTableE09TimeOfMax.Value := EncodedTime;
							adoTableE09Surcharge.Value := StrToFloat(Tokens[6]);
							adoTableE09Freeboard.Value := StrToFloat(Tokens[7]);
							try // some results (with 10^X format) incorrectly written as float+exp (9+10) instead of
									//   floatE+xp (9E+10)
								adoTableE09MaxArea.Value := StrToFloat(Tokens[8]);
							except
								on EConvertError do
									adoTableE09MaxArea.Value := 0;
							end;
							adoTableE09.Post;
							CurrentLine := InTextStream.ReadLine;
							frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
							frmStatus.Update;
						end;
						Break;
					end;
					if adoTableE09.RecordCount = 0 then
						Result := 0;
					adoTableE09.Close;

					CodeSite.SendMsg('Reading Conduits');
					adoTableE10.Open;
					while not InTextStream.AtEndOfStream do
					begin
						if CurrentLine <> ' |        Table E10 - CONDUIT SUMMARY STATISTICS        |' then
						begin
							CurrentLine := InTextStream.ReadLine;
							frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
							frmStatus.Update;
							Continue;
						end;

						VersionNum := StrToFloat(Version);
						if (VersionNum >= 8.0) and (VersionNum < 8.29) then
							for i := 1 to 11 do
								CurrentLine := InTextStream.ReadLine
						//if Version = '8.29' then //Collins 20031113
						else if (VersionNum >=8.29) then
							for i := 1 to 14 do
								CurrentLine := InTextStream.ReadLine;

{ collins 20031113
					else
							raise Exception.Create('This version of XP is not handled: version ' + Version);
 }
						while CurrentLine <> '' do
						begin
							//CodeSite.SendMsg(CurrentLine);
							ExtractTokensL(CurrentLine, ' ', '''', False, Tokens);
							adoTableE10.Append;
{							if Tokens.Count = 7 then // Orifice/weir, etc.
							begin
								adoTableE10CondName.Value := Tokens[0];
								adoTableE10DesignQ.Value := -1;
								adoTableE10DesignV.Value := -1;
								adoTableE10MaxD.Value := -1;
								adoTableE10MaxQ.Value := StrToFloat(Tokens[4]);
								Hour := StrToInt(Tokens[5]);
								Minute := StrToInt(Tokens[6]);
								EncodedTime := BeginTime + (Hour+(Minute/60))/24;
								adoTableE10TimeMaxQ.Value := EncodedTime;
								adoTableE10MaxV.Value := -1;
								adoTableE10TimeMaxV.Value := 0;
								adoTableE10QqRatio.Value := -1;
								adoTableE10MaxUsElev.Value := -1;
								adoTableE10MaxDsElev.Value := -1;
							end
							else if Tokens.Count = 9 then // FREE # X
							begin
								adoTableE10CondName.Value := Tokens[0]+' '+Tokens[1]+' '+Tokens[2];
								adoTableE10DesignQ.Value := -1;
								adoTableE10DesignV.Value := -1;
								adoTableE10MaxD.Value := -1;
								adoTableE10MaxQ.Value := StrToFloat(Tokens[6]);
								Hour := StrToInt(Tokens[7]);
								Minute := StrToInt(Tokens[8]);
								EncodedTime := BeginTime + (Hour+(Minute/60))/24;
								adoTableE10TimeMaxQ.Value := EncodedTime;
								adoTableE10MaxV.Value := -1;
								adoTableE10TimeMaxV.Value := 0;
								adoTableE10QqRatio.Value := -1;
								adoTableE10MaxUsElev.Value := -1;
								adoTableE10MaxDsElev.Value := -1;
							end
							else if Tokens.Count = 9 then // FREE # X
							begin
								adoTableE10CondName.Value := Tokens[0]+' '+Tokens[1]+' '+Tokens[2];
								adoTableE10DesignQ.Value := -1;
								adoTableE10DesignV.Value := -1;
								adoTableE10MaxD.Value := -1;
								adoTableE10MaxQ.Value := StrToFloat(Tokens[6]);
								Hour := StrToInt(Tokens[7]);
								Minute := StrToInt(Tokens[8]);
								EncodedTime := BeginTime + (Hour+(Minute/60))/24;
								adoTableE10TimeMaxQ.Value := EncodedTime;
								adoTableE10MaxV.Value := -1;
								adoTableE10TimeMaxV.Value := 0;
								adoTableE10QqRatio.Value := -1;
								adoTableE10MaxUsElev.Value := -1;
								adoTableE10MaxDsElev.Value := -1;
							end
							else}
              if Tokens.Count >= 15 then
              begin
                adoTableE10CondName.Value := Tokens[0];
                adoTableE10DesignQ.Value := StrToFloat(Tokens[1]);
								adoTableE10DesignV.Value := StrToFloat(Tokens[2]);
                adoTableE10MaxD.Value := StrToFloat(Tokens[3]);
                adoTableE10MaxQ.Value := StrToFloat(Tokens[4]);
                Hour := StrToInt(Tokens[5]);
								Minute := StrToInt(Tokens[6]);
                EncodedTime := BeginTime + (Hour+(Minute/60))/24;
                adoTableE10TimeMaxQ.Value := EncodedTime;
                adoTableE10MaxV.Value := StrToFloat(Tokens[7]);
                Hour := StrToInt(Tokens[8]);
                Minute := StrToInt(Tokens[9]);
								EncodedTime := BeginTime + (Hour+(Minute/60))/24;
                adoTableE10TimeMaxV.Value := EncodedTime;
                adoTableE10QqRatio.Value := StrToFloat(Tokens[10]);
                adoTableE10MaxUsElev.Value := StrToFloat(Tokens[11]);
                adoTableE10MaxDsElev.Value := StrToFloat(Tokens[12]);
              end
              else
              begin
                CurrentLine := InTextStream.ReadLine;
								frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
								frmStatus.Update;
								Continue;
							end;
							adoTableE10.Post;
							CurrentLine := InTextStream.ReadLine;
							frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
							frmStatus.Update;
						end;
						Break;
					end;
					if adoTableE10.RecordCount = 0 then
						Result := 0;
					adoTableE10.Close;

					CodeSite.SendMsg('Reading Junction Flooding and Volume');
					adoTableE20.Open;
					while not InTextStream.AtEndOfStream do
					begin
						if CurrentLine <> '   | Table E20 - Junction Flooding and Volume Listing.   |' then
						begin
							CurrentLine := InTextStream.ReadLine;
							frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
							frmStatus.Update;
							Continue;
						end;

						VersionNum := StrToFloat(Version);
						if (VersionNum >= 8.0) and (VersionNum < 8.29) then
							for i := 1 to 18 do
								CurrentLine := InTextStream.ReadLine
						else if (VersionNum >=8.29) then
							for i := 1 to 18 do
								CurrentLine := InTextStream.ReadLine;

						while CurrentLine <> '' do
						begin
							ExtractTokensL(CurrentLine, ' ', '''', False, Tokens);
							adoTableE20.Append;
							if Tokens.Count >= 6 then
							begin
								adoTableE20NodeName.Value := Tokens[0];
								adoTableE20SurchargeTime.Value := StrToFloat(Tokens[1]);
								adoTableE20FloodedTime.Value := StrToFloat(Tokens[2]);
								adoTableE20FloodVol.Value := StrToFloat(Tokens[3]);
								adoTableE20MaxStoredVol.Value := StrToFloat(Tokens[4]);
								adoTableE20pondingVol.Value := StrToFloat(Tokens[5]);
							end
							else
							begin
								CurrentLine := InTextStream.ReadLine;
								frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
								frmStatus.Update;
								Continue;
							end;
							adoTableE20.Post;
							CurrentLine := InTextStream.ReadLine;
							frmStatus.prgFile.PartsComplete := Round(InTextStream.Position/InTextStream.FastSize*100);
							frmStatus.Update;
						end;
						Break;
					end;
					if adoTableE20.RecordCount = 0 then
						Result := 0;
					adoTableE20.Close;
			except
				on E:EOLEException do Result := E.ErrorCode;
				on E:Exception do
				begin
					ShowMessage(E.Message);
					Result := 1;
				end;
      end;
    finally
      Tokens.Free;
      InTextStream.Free;
      InStream.Free;
      frmStatus.Free;
    end;
  end;

  dmodXPExport.Free;
end;

exports getXPresults;

{$R *.res}

begin
end.
