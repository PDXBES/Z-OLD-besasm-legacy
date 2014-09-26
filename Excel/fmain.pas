unit fmain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ActnList, StdCtrls, dmExcel1, inifiles,
  dmReconSS
  ;
const
  AppiniName = 'ExcelTestBench.ini';
type
  TForm1 = class(TForm)
    Button1: TButton;
    ActionList1: TActionList;
    Action1: TAction;
    Button2: TButton;
    procedure FormCreate(Sender: TObject);
    procedure Action1Execute(Sender: TObject);
    procedure Button2Click(Sender: TObject);

  private
    { Private declarations }
  public
    { Public declarations }
    AppIni: TMemIniFile;

  end;

var
  Form1: TForm1;
  EXEdir : string;
  
implementation

{$R *.dfm}

procedure TForm1.FormCreate(Sender: TObject);
begin
  form1.caption := 'EXCEL TEST BENCH';

	EXEDir := ExtractFileDir(ParamStr(0));


	if FileExists(EXEdir + '\' + appininame) then
		AppIni := TMemIniFile.Create(EXEdir + '\' + appininame)
	else
	begin
		MessageDlg(EXEdir + '\' + appininame + ' not found', mtInformation, [mbOK], 0);
	end;

end;



procedure TForm1.Action1Execute(Sender: TObject);
begin
  datamodule2.ExcelTest1;
end;

procedure TForm1.Button2Click(Sender: TObject);


begin

  datamodule2.testReconSS;


end;

end.
