program TARCollector;

uses
  Forms,
  fMain in 'fMain.pas' {frmMain},
  fManageUsers in 'fManageUsers.pas' {frmManageUsers};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmMain, frmMain);
  Application.Run;
end.
