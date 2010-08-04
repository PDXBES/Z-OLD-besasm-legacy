program ArchiveAssistant;

{%File 'ModelSupport\fMain\fMain.txvpck'}
{%File 'ModelSupport\fProgress\fProgress.txvpck'}
{%File 'ModelSupport\default.txvpck'}

uses
  Forms,
  fMain in 'fMain.pas' {frmMain},
  fProgress in 'fProgress.pas' {frmProgress};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmMain, frmMain);
  Application.CreateForm(TfrmProgress, frmProgress);
  Application.Run;
end.
