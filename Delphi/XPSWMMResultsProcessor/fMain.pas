unit fMain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, RzLabel, dxCntner, dxEditor, dxExEdtr, dxEdLib,
  RzButton, ADODB, DB;

type
  TForm1 = class(TForm)
    dlgOpen: TOpenDialog;
    edtInPath: TdxButtonEdit;
    RzLabel1: TRzLabel;
    RzLabel2: TRzLabel;
    edtOutPath: TdxButtonEdit;
    btnProcess: TRzButton;
    adoOutConnection: TADOConnection;
    adoOutCommand: TADOCommand;
    adoTableE09: TADOTable;
    adoTableE10: TADOTable;
    adoTableE09NodeName: TWideStringField;
    adoTableE09GrElev: TFloatField;
    adoTableE09MaxCrown: TFloatField;
    adoTableE09MaxJElev: TFloatField;
    adoTableE09TimeOfMax: TDateTimeField;
    adoTableE09Surcharge: TFloatField;
    adoTableE09Freeboard: TFloatField;
    adoTableE09MaxArea: TFloatField;
    adoTableE10CondName: TWideStringField;
    adoTableE10DesignQ: TFloatField;
    adoTableE10DesignV: TFloatField;
    adoTableE10MaxD: TFloatField;
    adoTableE10MaxQ: TFloatField;
    adoTableE10TimeMaxQ: TDateTimeField;
    adoTableE10MaxV: TFloatField;
    adoTableE10TimeMaxV: TDateTimeField;
    adoTableE10QqRatio: TFloatField;
    adoTableE10MaxUsElev: TFloatField;
    adoTableE10MaxDsElev: TFloatField;
    procedure edtInPathButtonClick(Sender: TObject;
      AbsoluteIndex: Integer);
    procedure edtOutPathButtonClick(Sender: TObject;
      AbsoluteIndex: Integer);
    procedure btnProcessClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

uses StStrms, ADOX_TLB, StStrL, DateUtils;

procedure TForm1.edtInPathButtonClick(Sender: TObject;
  AbsoluteIndex: Integer);
begin
	if dlgOpen.Execute then
  begin
  	edtInPath.Text := dlgOpen.FileName;
  end;
end;

procedure TForm1.edtOutPathButtonClick(Sender: TObject;
  AbsoluteIndex: Integer);
begin
	if dlgOpen.Execute then
  begin
  	edtOutPath.Text := dlgOpen.FileName;
  end;
end;

procedure TForm1.btnProcessClick(Sender: TObject);
var
	InStream: TFileStream;
  InTextStream: TStAnsiTextStream;
  ACatalog: Catalog;
  i: Integer;
  CurrentLine: String;
  Tokens: TStringList;
  BeginYear, BeginMonth, BeginDay, BeginHour, BeginMinute, BeginSecond: Word;
  BeginTime: TDateTime;
  Year, Month, Day: Word;
  Hour, Minute: Word;
  EncodedTime: TDateTime;
begin
	InStream := TFileStream.Create(edtInPath.Text, fmOpenRead or fmShareDenyWrite);
  InTextStream := TStAnsiTextStream.Create(InStream);
  Tokens := TStringList.Create;
  try
  	adoOutConnection.ConnectionString :=
    	'Provider=Microsoft.Jet.OLEDB.4.0;'+
      'Data Source='+edtOutPath.Text+';'+
      'Persist Security Info=False';
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
      end;

      adoTableE09.Open;
      while not InTextStream.AtEndOfStream do
      begin
      	if CurrentLine <> ' |        Table E9 - JUNCTION SUMMARY STATISTICS        |' then
        begin
        	CurrentLine := InTextStream.ReadLine;
          Continue;
        end;
        for i := 1 to 10 do
        	CurrentLine := InTextStream.ReadLine;
        while CurrentLine <> '' do
        begin
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
          adoTableE09MaxArea.Value := StrToFloat(Tokens[8]);
          adoTableE09.Post;
        	CurrentLine := InTextStream.ReadLine;
        end;
        Break;
      end;
			adoTableE09.Close;

      adoTableE10.Open;
      while not InTextStream.AtEndOfStream do
      begin
      	if CurrentLine <> ' |        Table E10 - CONDUIT SUMMARY STATISTICS        |' then
        begin
        	CurrentLine := InTextStream.ReadLine;
          Continue;
        end;
        for i := 1 to 11 do
        	CurrentLine := InTextStream.ReadLine;
        while CurrentLine <> '' do
        begin
        	ExtractTokensL(CurrentLine, ' ', '''', False, Tokens);
					adoTableE10.Append;
          if Tokens.Count = 7 then // Orifice/weir, etc.
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
          else
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
          end;
          adoTableE10.Post;
        	CurrentLine := InTextStream.ReadLine;
        end;
        Break;
      end;
      adoTableE10.Close;

  finally
  	Tokens.Free;
  	InTextStream.Free;
    InStream.Free;
  end;
end;

end.
