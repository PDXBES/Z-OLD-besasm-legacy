unit fStatus;

interface

uses
	Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, RzPrgres, StdCtrls, ComCtrls, RzEdit, RzLabel;

type
  TfrmStatus = class(TForm)
    RzLabel1: TRzLabel;
    edtStatus: TRzRichEdit;
    prgStatus: TRzProgressBar;
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmStatus: TfrmStatus;

implementation

uses fMain;

{$R *.dfm}

end.
