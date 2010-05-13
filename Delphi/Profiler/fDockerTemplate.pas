unit fDockerTemplate;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ExtCtrls, Buttons, RzButton, dxBar, StdCtrls, RzLabel, RzPanel;

type
  TfrmDocker = class(TForm)
    RzPanel1: TRzPanel;
    lblDockerTitle: TRzLabel;
    dxBarPopupMenu1: TdxBarPopupMenu;
    dxBarManager1: TdxBarManager;
    RzPanel2: TRzPanel;
    RzToolbarButton1: TRzToolbarButton;
    RzPanel3: TRzPanel;
    Image1: TImage;
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmDocker: TfrmDocker;

implementation

{$R *.dfm}

end.
