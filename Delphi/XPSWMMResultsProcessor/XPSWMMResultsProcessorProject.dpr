program XPSWMMResultsProcessorProject;

uses
  Forms,
  fMain in 'fMain.pas' {Form1},
  dmXPExport in 'dmXPExport.pas' {DataModule1: TDataModule};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TForm1, Form1);
  Application.CreateForm(TDataModule1, DataModule1);
  Application.Run;
end.
