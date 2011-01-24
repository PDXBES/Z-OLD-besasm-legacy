unit dAbout;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, RzStatus;

type
  TdlgAbout = class(TForm)
    versionInfo: TRzVersionInfo;
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  dlgAbout: TdlgAbout;

implementation

{$R *.dfm}

end.
