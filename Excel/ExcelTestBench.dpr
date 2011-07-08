program ExcelTestBench;

uses
  Forms,
  fmain in 'fmain.pas' {Form1},
  dmExcel1 in 'dmExcel1.pas' {DataModule2: TDataModule},
  uXLSconstants in 'uXLSconstants.pas',
  uConstants in 'uConstants.pas',
  dmReconSS in 'dmReconSS.pas' {reconSS: TDataModule};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TForm1, Form1);
  Application.CreateForm(TDataModule2, DataModule2);
  Application.Run;
end.
