unit fModuleMaster;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, RzLabel, ExtCtrls, RzPanel;

type
  TfrmModuleMaster = class(TForm)
    pnlTitleHolder: TRzPanel;
    RzLabel1: TRzLabel;
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

var
  frmModuleMaster: TfrmModuleMaster;

implementation

{$R *.dfm}

constructor TfrmModuleMaster.Create(AOwner: TComponent;
  AParent: TWinControl);
begin
  FParentWnd := AParent;
  inherited Create(AOwner);
end;

procedure TfrmModuleMaster.CreateParams(var Params: TCreateParams);
begin
	inherited;
	Params.Style := Params.Style or WS_CHILD;
end;

procedure TfrmModuleMaster.FormClose(Sender: TObject;
	var Action: TCloseAction);
begin
	Action := caFree;
end;

procedure TfrmModuleMaster.Loaded;
begin
  inherited;
  Align := alClient;
  BorderStyle := bsNone;
  BorderIcons := [];
  Parent := FParentWnd;
  Position := poDefault;
end;

end.
