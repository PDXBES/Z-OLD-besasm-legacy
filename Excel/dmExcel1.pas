unit dmExcel1;

interface

uses
  SysUtils, Classes, dialogs, comobj, forms, uconstants,
  uxlsconstants, dmreconSS;

type
  TDataModule2 = class(TDataModule)
    Procedure ExcelTest1();
    Procedure InitExcel();
    Procedure testReconSS();

  private
    { Private declarations }

  public

   { Public declarations }
    oleMI : Variant;
    oleXL : oleVariant;

  end;


var
  DataModule2: TDataModule2;

implementation

uses fmain;


Procedure TDataModule2.ExcelTest1();
var
XLpath : string;

begin

  XLpath := form1.Appini.ReadString('Section1','XLSname','unknown');
  if XLpath  = 'unknown' then
  begin
		MessageDlg('XLSname key not found in Section1', mtInformation, [mbOK], 0);
    exit;
  end;

  if FileExists(XLpath) then
    if not DeleteFile(XLpath) then
    begin
      showmessage('unable to delete ' + XLpath);
      exit;
    end;


  InitExcel;


End;

procedure TDataModule2.InitExcel;
var
strRC : string;
begin
 try
  // If no instance of Word is running, try to Create a new Excel Object
  oleXL := CreateOleObject('Excel.Application');
  oleXL.Visible := True;
  // Add a new Workbook, and give it a name
  oleXL.Workbooks.Add(xlWBatWorkSheet);
  oleXL.Workbooks[1].WorkSheets[1].Name := 'SheetOne';

  // Insert Header Text in some Cells[Row,Col]
  oleXL.Cells[1, 1].Value := 'Desc';
  oleXL.Cells[1, 2].Value := 'Result';
  oleXL.Cells[4, 4].Value := 4;
  oleXL.Cells[5, 4].Value := 6;
  oleXL.Cells[6, 4].Value := 8;


   strRC := '=Sum(R4C4:R6C4)';

   oleXL.sheets['SheetOne'].Cells[2, 1].value := ' '  + strRC;
   oleXL.sheets['SheetOne'].Cells[2, 2].FormulaR1C1 := strRC;

   oleXL.Activeworkbook.SaveAs('C:\foobar.xls');
   oleXL.Activeworkbook.save;
   oleXL.Activeworkbook.close;

   oleXL.quit;
 except
   ShowMessage('Cannot start Excel/Excel not installed ?');
   Exit;
 end;
end;

{$R *.dfm}

Procedure TDataModule2.testReconSS();
var

  modeldir: string;
  rc : integer;
  ReconSS: TReconSS;

begin

  modeldir := form1.Appini.ReadString('Section1','modeldir','unknown');
  if modeldir  = 'unknown' then
  begin
		MessageDlg('modelpath key not found in Section1', mtInformation, [mbOK], 0);
    exit;
  end;

  if not directoryexists(modeldir) then
  begin
      showmessage('no directory there ' + modeldir);
      exit;
  end;

  Application.createform(TReconss, reconss);

  rc :=  ReconSS.Initialize(modeldir);
  if  rc = returnfailure then
  begin
    reconSS.free;
    exit;
  end;

  rc := ReconSS.Reconcile;
  if rc = returnfailure then
  begin
    reconSS.free;
    exit;
  end
  else
    showmessage(ReconSS.getReconSSname);

  reconSS.free;


end;













end.
