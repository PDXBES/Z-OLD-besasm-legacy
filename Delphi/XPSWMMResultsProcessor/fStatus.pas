unit fStatus;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, RzPrgres, StdCtrls, RzLabel;

type
  TfrmStatus = class(TForm)
    lblInFile: TRzLabel;
    lblOutFile: TRzLabel;
    prgFile: TRzProgressBar;
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmStatus: TfrmStatus;

implementation

{$R *.dfm}

end.
