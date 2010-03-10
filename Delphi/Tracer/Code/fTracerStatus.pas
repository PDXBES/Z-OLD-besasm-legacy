{==frmTracerStatus Unit=========================================================

	Tracer Status Form

	Revision History
	1.1    03/25/2003 (AMM) Revised name
	1.0    2/2002     (DJC) Initial inclusion (as Form2)
===============================================================================}

unit fTracerStatus;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, RzPrgres, RzLabel;

type
	TfrmTracerStatus = class(TForm)
    prgStatus: TRzProgressBar;
    statustext1: TRzLabel;
    statustext2: TRzLabel;
    statustext3: TRzLabel;
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
  private
		{ Private declarations }
  public
    { Public declarations }
  end;

var
  frmTracerStatus: TfrmTracerStatus;

implementation

{$R *.dfm}

procedure TfrmTracerStatus.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  action := cafree;
end;


end.
