unit dOptions;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, RzButton, RzRadChk, RzTabs, ExtCtrls, RzPanel, RzDlgBtn, StdCtrls,
  RzLabel, Mask, RzEdit, RzSpnEdt, RzBtnEdt, RzShellDialogs;

type
  TdlgOptions = class(TForm)
    pgcOptions: TRzPageControl;
    dlgButtons: TRzDialogButtons;
    TabSheet1: TRzTabSheet;
    chkShowSplashScreen: TRzCheckBox;
    chkShowVitalProgramsWarning: TRzCheckBox;
    edtMSAccessWaitMilliseconds: TRzSpinEdit;
    RzLabel1: TRzLabel;
    TabSheet2: TRzTabSheet;
    RzLabel2: TRzLabel;
    edtExtentsMap: TRzButtonEdit;
    dlgOpen: TRzOpenDialog;
    RzLabel3: TRzLabel;
    edtRunoffEngine: TRzButtonEdit;
    edtBasinBoundaries: TRzButtonEdit;
    RzLabel4: TRzLabel;
    procedure dlgButtonsClickOk(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure edtExtentsMapButtonClick(Sender: TObject);
    procedure edtRunoffEngineButtonClick(Sender: TObject);
    procedure edtBasinBoundariesButtonClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  dlgOptions: TdlgOptions;

implementation

uses uEMGAATSSystemConfig, fMain;

{$R *.dfm}

procedure TdlgOptions.dlgButtonsClickOk(Sender: TObject);
begin
  SystemConfig.ShowSplashScreen := chkShowSplashScreen.Checked;
  SystemConfig.ShowVitalProgramsWarning := chkShowVitalProgramsWarning.Checked;
  SystemConfig.MSAccessWaitMilliseconds := edtMSAccessWaitMilliseconds.IntValue;
  SystemConfig.StudyAreaMaxExtents := edtExtentsMap.Text;
  SystemConfig.BasinBoundaries := edtBasinBoundaries.Text;

  SystemConfig.Update;
end;

procedure TdlgOptions.edtBasinBoundariesButtonClick(Sender: TObject);
begin
  dlgOpen.InitialDir := ExtractFileDir(edtBasinBoundaries.Text);
  dlgOpen.Title := 'Select basin boundaries map file';
  dlgOpen.Filter := 'MapInfo tables (*.tab)|*.tab|All files (*.*}|*.*';
  dlgOpen.FilterIndex := 1;
  if dlgOpen.Execute then
    edtBasinBoundaries.Text := dlgOpen.FileName;
end;

procedure TdlgOptions.edtExtentsMapButtonClick(Sender: TObject);
begin
  dlgOpen.InitialDir := ExtractFileDir(edtExtentsMap.Text);
  dlgOpen.Title := 'Select extents map file';
  dlgOpen.Filter := 'MapInfo tables (*.tab)|*.tab|All files (*.*}|*.*';
  dlgOpen.FilterIndex := 1;
  if dlgOpen.Execute then
    edtExtentsMap.Text := dlgOpen.FileName;
end;

procedure TdlgOptions.FormShow(Sender: TObject);
begin
  chkShowSplashScreen.Checked := SystemConfig.ShowSplashScreen;
  chkShowVitalProgramsWarning.Checked := SystemConfig.ShowVitalProgramsWarning;
  edtMSAccessWaitMilliseconds.Value := SystemConfig.MSAccessWaitMilliseconds;
  edtExtentsMap.Text := SystemConfig.StudyAreaMaxExtents;
  edtBasinBoundaries.Text := SystemConfig.BasinBoundaries;

  pgcOptions.ActivePageIndex := 0;
end;

procedure TdlgOptions.edtRunoffEngineButtonClick(Sender: TObject);
begin
  dlgOpen.InitialDir := ExtractFileDir(edtRunoffEngine.Text);
  dlgOpen.Title := 'Select basin boundaries map file';
  dlgOpen.Filter := 'Runoff engines (*.exe)|*.exe|All files (*.*}|*.*';
  dlgOpen.FilterIndex := 1;
  if dlgOpen.Execute then
    edtRunoffEngine.Text := dlgOpen.FileName;
end;

end.
