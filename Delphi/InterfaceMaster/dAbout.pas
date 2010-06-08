unit dAbout;

interface

uses Windows, SysUtils, Classes, Graphics, Forms, Controls, StdCtrls,
  Buttons, ExtCtrls, RzStatus, RzForms, jpeg, RzButton;

type
  TdlgAbout = class(TForm)
    versionInfo: TRzVersionInfo;
    RzFormShape1: TRzFormShape;
    Version: TLabel;
    Copyright: TLabel;
    Comments: TLabel;
    RzButton1: TRzButton;
    FileDescription: TLabel;
    procedure RzFormShape1FormShow(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  dlgAbout: TdlgAbout;

implementation

{$R *.dfm}

procedure TdlgAbout.RzFormShape1FormShow(Sender: TObject);
begin
	FileDescription.Caption := versionInfo.FileDescription;
	Version.Caption := 'Build '+ versionInfo.FileVersion;
	Copyright.Caption := versionInfo.Copyright;
	Comments.Caption := versionInfo.Comments;
end;

end.
 
