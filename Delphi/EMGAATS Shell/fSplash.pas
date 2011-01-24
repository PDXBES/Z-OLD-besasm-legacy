{==fSplash Unit=================================================================

	Splash screen form

	Revision History
	3.0    03/07/2003 (AMM) Initial inclusion

===============================================================================}

unit fSplash;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, RzBckgnd, StdCtrls, RzLabel, ExtCtrls, RzPanel, RzStatus, jpeg,
  RzForms;

type
  TfrmSplash = class(TForm)
    RzPanel1: TRzPanel;
    lblStatus: TRzLabel;
    lblVersion: TRzLabel;
    verEmgaats: TRzVersionInfo;
    lblEMGAATSVersion: TRzLabel;
    RzFormShape1: TRzFormShape;
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure FormCreate(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmSplash: TfrmSplash;

implementation

uses GlobalConstants;

{$R *.dfm}

procedure TfrmSplash.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  Action := caFree;
end;

procedure TfrmSplash.FormCreate(Sender: TObject);
begin
	lblVersion.Caption := 'File Version '+verEmgaats.FileVersion;
	lblEMGAATSVersion.Caption :=  'Version ' + EMGAATSVersion + ', ' + EMGAATSVersionComment;
end;

end.
