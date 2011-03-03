unit fProgress;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, RzLabel, RzPrgres, ExtCtrls, RzPanel;

type
  TfrmProgress = class(TForm)
    RzPanel1: TRzPanel;
    prgScan: TRzProgressBar;
    lblProgress: TRzLabel;
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmProgress: TfrmProgress;

implementation

{$R *.dfm}

end.
