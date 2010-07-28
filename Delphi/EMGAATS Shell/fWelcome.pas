unit fWelcome;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, fLabeledChild, RzLabel, ExtCtrls, RzPanel, pngextra, StdCtrls,
  ActnList, XPStyleActnCtrls, ActnMan, pngimage, RzBckgnd, Buttons,
  PngSpeedButton;

type
  TfrmWelcome = class(TfrmLabeledChild)
    pnlButtons: TRzFlowPanel;
    btnCheck: TPNGButton;
    btnConfigure: TPNGButton;
    actManager: TActionManager;
    actBuild: TAction;
    actCheck: TAction;
    actDeploy: TAction;
    actReview: TAction;
    actConfigure: TAction;
    RzLabel2: TRzLabel;
    pnlMRUModels: TRzGridPanel;
    RzLabel1: TRzLabel;
    actClose: TAction;
    RzBackground2: TRzBackground;
    btnBuild: TPNGButton;
    btnReview: TPNGButton;
    procedure actBuildExecute(Sender: TObject);
    procedure actReviewExecute(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    procedure AddMRUFile(AFileName: TFileName);
    procedure ClearMRUList;
  end;

var
  frmWelcome: TfrmWelcome;

implementation

uses uUtilities, fMain, fBuild, fViewResults;
{$R *.dfm}

procedure TfrmWelcome.actBuildExecute(Sender: TObject);
begin
  if Assigned(frmBuild) then
    frmBuild.Show
  else
    frmBuild := frmMain.DisplayForm(TfrmBuild) as TfrmBuild;
end;

procedure TfrmWelcome.actReviewExecute(Sender: TObject);
begin
  if Assigned(frmViewResults) then
    frmViewResults.Show
  else
    frmViewResults := frmMain.DisplayForm(TfrmViewResults) as TfrmViewResults;
end;

procedure TfrmWelcome.AddMRUFile(AFileName: TFileName);
var
  MRUFileName: TRzURLLabel;
begin
  MRUFileName := TRzURLLabel.Create(Application);
  MRUFileName.Parent := pnlMRUModels;
  MRUFileName.Caption := AFileName;
  // Have to align by client and then none due to funny way RZGridPanel autosizes
  MRUFileName.Align := alClient;
//  MRUFileName.Align := alNone;
  MRUFileName.AutoSize := True;
  MRUFileName.WordWrap := True;
//  MRUFileName.AlignWithMargins := True;
  MRUFileName.Show;
end;

procedure TfrmWelcome.ClearMRUList;
var
  i: Integer;
begin
  for i := 0 to pnlMRUModels.ControlCount - 1 do
    pnlMRUModels.Controls[i].Free;
end;

procedure TfrmWelcome.FormCreate(Sender: TObject);
begin
  inherited;
  pnlButtons.Transparent := True;
end;

end.
