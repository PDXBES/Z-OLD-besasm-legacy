unit fTestMain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, RzButton, StdCtrls, RzLabel, Mask, RzEdit, RzBtnEdt, CodeSiteLogging;

type
  TForm1 = class(TForm)
    RzLabel1: TRzLabel;
    RzLabel2: TRzLabel;
    dlgOpen: TOpenDialog;
    RzButton1: TRzButton;
    RzLabel3: TRzLabel;
    edtIn: TRzButtonEdit;
    edtOut: TRzButtonEdit;
    CSGlobalObject1: TCSGlobalObject;
    procedure edtInButtonClick(Sender: TObject);
    procedure edtOutButtonClick(Sender: TObject);
    procedure RzButton1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    InFile, OutFile: PChar;
  end;

	function getXPresults(InPath, OutPath: PChar): Integer; stdcall; external 'XPResults.dll';

var
  Form1: TForm1;

implementation

{$R *.dfm}

procedure TForm1.edtInButtonClick(Sender: TObject);
begin
	if dlgOpen.Execute then
  	edtIn.Text := dlgOpen.FileName;
end;

procedure TForm1.edtOutButtonClick(Sender: TObject);
begin
	if dlgOpen.Execute then
  	edtOut.Text := dlgOpen.FileName;
end;

procedure TForm1.RzButton1Click(Sender: TObject);
var
	AResult: Integer;
begin
	InFile := PChar(edtIn.Text);
  OutFile := PChar(edtOut.Text);
	AResult := getXPResults(InFile, OutFile);
  RzLabel3.Caption := 'Result '+IntToStr(AResult);
end;

end.
