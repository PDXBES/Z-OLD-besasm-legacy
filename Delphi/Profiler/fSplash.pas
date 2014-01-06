unit fSplash;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, jpeg, ExtCtrls, StBase, StVInfo, StdCtrls;

type
  TfrmSplash = class(TForm)
    Image1: TImage;
    StVersionInfo1: TStVersionInfo;
    Label1: TLabel;
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure FormShow(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmSplash: TfrmSplash;

implementation

{$R *.dfm}

procedure TfrmSplash.FormClose(Sender: TObject; var Action: TCloseAction);
begin
	Action := caFree;
end;

procedure TfrmSplash.FormShow(Sender: TObject);
begin
	Label1.Caption := Format('Version %s, ', [stVersionInfo1.FileVersion]);
end;

end.
