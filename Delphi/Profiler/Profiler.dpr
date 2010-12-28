program Profiler;

uses
  Forms,
  fMain in 'fMain.pas' {frmMain},
  Coords in 'Coords.pas',
  dChooseConduit in 'dChooseConduit.pas' {dlgChooseConduit},
  Geometry in 'Geometry.pas',
  Hydraulics in 'Hydraulics.pas',
  dFindLink in 'dFindLink.pas' {dlgFindLink},
  fSplash in 'fSplash.pas' {frmSplash},
  dOptions in 'dOptions.pas' {dlgOptions};

{$R *.res}

begin
  Application.Initialize;
  Application.Title := 'Profiler';

  frmSplash := TfrmSplash.Create(Application);
  try
  	frmSplash.Show;
    frmSplash.Update;
    Application.CreateForm(TfrmMain, frmMain);
  Application.CreateForm(TdlgChooseConduit, dlgChooseConduit);
  Application.CreateForm(TdlgFindLink, dlgFindLink);
  finally
  	frmSplash.Close;
  end;
  Application.Run;
end.
