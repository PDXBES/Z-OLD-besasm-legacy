unit fChild;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs;

const
  cDesignBordersHeight = 9;
  cDesignBordersWidth = 8;
  cDesignCaptionHeight = 19;
  cDesignScreenDPI = 96;

type
  TfrmChild = class(TForm)
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
  private
    { Private declarations }
    FParentWnd: TWinControl;
  protected
    { Protected declarations }
    procedure CreateParams(var Params: TCreateParams); override;
    procedure Loaded; override;
  public
    { Public declarations }
    constructor Create(AOwner: TComponent; AParent: TWinControl); reintroduce;
      overload;
  end;
  TfrmChildClass = class of TfrmChild;

var
  frmChild: TfrmChild;

implementation

{$R *.dfm}

constructor TfrmChild.Create(AOwner: TComponent; AParent: TWinControl);
begin
  FParentWnd := AParent;
  inherited Create(AOwner);
end;

procedure TfrmChild.CreateParams(var Params: TCreateParams);
begin
  inherited;
  Params.Style := Params.Style or WS_CHILD;
  // for WinXP and above, have Windows manage double-buffered painting
  if CheckWin32Version(5, 1) then
    Params.ExStyle := Params.ExStyle or WS_EX_COMPOSITED;
end;

procedure TfrmChild.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  Action := caFree;
end;

procedure TfrmChild.Loaded;
begin
  inherited;
  Align := alClient;
  BorderStyle := bsNone;
  BorderIcons := [];
  Parent := FParentWnd;
  Position := poDefault;
end;

end.
