unit dModelExists;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, RzButton, RzRadChk, RzDlgBtn, pngimage, ExtCtrls, RzPanel, StdCtrls,
  RzLabel, RzRadGrp;

type
  TModelExistsAction = (meaEraseContents, meaKeepContents, meaCancel);

  TdlgModelExists = class(TForm)
    RzGridPanel1: TRzGridPanel;
    rgrpAction: TRzRadioGroup;
    lblModelPath: TRzLabel;
    RzPanel1: TRzPanel;
    Image1: TImage;
    RzLabel1: TRzLabel;
    dlgButtons: TRzDialogButtons;
    chkRememberModelBoundaries: TRzCheckBox;
    procedure rgrpActionChanging(Sender: TObject; NewIndex: Integer;
      var AllowChange: Boolean);
    procedure FormShow(Sender: TObject);
    procedure CheckRememberBoundaries;
  private
    { Private declarations }
    fPath: String;
    function GetRememberModelBoundaries: Boolean;
    function GetModelExistsAction: TModelExistsAction;
  public
    { Public declarations }
    property ModelExistsAction: TModelExistsAction read GetModelExistsAction;
    property Path: String read fPath write fPath;
    property RememberModelBoundaries: Boolean read GetRememberModelBoundaries;
  end;

var
  dlgModelExists: TdlgModelExists;

implementation

{$R *.dfm}

procedure TdlgModelExists.CheckRememberBoundaries;
begin
  chkRememberModelBoundaries.Enabled :=
    rgrpAction.ItemIndex = Integer(meaEraseContents);
end;

procedure TdlgModelExists.FormShow(Sender: TObject);
begin
  lblModelPath.Caption := fPath;
  CheckRememberBoundaries;
end;

function TdlgModelExists.GetModelExistsAction: TModelExistsAction;
begin
  Result := TModelExistsAction(rgrpAction.ItemIndex);
end;

function TdlgModelExists.GetRememberModelBoundaries: Boolean;
begin
  Result := chkRememberModelBoundaries.Checked;
end;

procedure TdlgModelExists.rgrpActionChanging(Sender: TObject; NewIndex: Integer;
  var AllowChange: Boolean);
var
  Action: TModelExistsAction;
begin
  Action := TModelExistsAction(NewIndex);
  case Action of
    meaEraseContents: dlgButtons.CaptionOk := 'Continue';
    meaKeepContents: dlgButtons.CaptionOk := 'Continue';
    meaCancel: dlgButtons.CaptionOK := 'Cancel';
  end;
  chkRememberModelBoundaries.Enabled := NewIndex = Integer(meaEraseContents);
end;

end.
