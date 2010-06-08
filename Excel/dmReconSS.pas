unit dmReconSS;

interface

uses
  SysUtils, Classes, inifiles, dialogs, messages, comobj, variants,
  strutils,
  uxlsconstants, uConstants, DB, ADODB
  ;

type
  TreconSS = class(TDataModule)
    ADOQuery1: TADOQuery;
    ADOConnection1: TADOConnection;
    function Initialize(mdir: string) : integer;
    function Reconcile() : integer;
    function getReconSSname() : string;

  private
    { Private declarations }
    modeldir : string;
    modelini : Tmeminifile;
    XLpath : string;


  public
    { Public declarations }
    oleXL : olevariant;
    csHCardTA : variant;
    csHCardIA : variant;
    csdscTA : variant;
    csdscIA : variant;

  end;

//var
//  reconss: TReconSS;

implementation
 uses fmain;

 const
 {
  The constants inicate the row that spreadsheet data are refenced to. The six
  iQ constants correspond to the six queries in the ModelDeployHydrology access
  data base that are called to do the reconciliation.
 }
 iQ1 = 4;
 iQ2 = 35;
 iQ3 = 49;
 iQ4 = 71;
 iQ5 = 86;
 iQ6 = 103;

{$R *.dfm}
function TReconSS.getreconSSname(): string;
begin
  result := XLpath;
end;

function TReconSS.initialize(mdir: string): integer;
var
  mdlversion, modelname : string;

begin

  modeldir := mdir;
  modelini := tmeminifile.Create(mdir + '\model.ini');
  mdlversion := modelini.ReadString('control','version','unknown');
  if mdlversion <> '2.2' then
  begin
    showmessage('version ' + mdlversion);
    result := returnFailure;
    exit;
  end;

  modelname := modelini.ReadString('admin','modelname','unknown');
  if modelname = 'unknown' then
  begin
    showmessage('no model name in INI file');
    result := returnFailure;
    exit;
  end;

  XLpath := modeldir + '\qc\R_' + modelname + '.xls';
  if FileExists(XLpath) then
    if not DeleteFile(XLpath) then
      begin
        showmessage('unable to delete ' + XLpath);
        result := Returnfailure;
        exit;
      end;

  result := Returnsuccess;

end;

function TReconSS.reconcile() : integer;
var

  i, irow: integer;
  v : variant;
  strRC : string;
  mystr : string;
begin


//ActiveWorkbook.Names.Add Name:="HcardSum", RefersToR1C1:="=Recon!R20C2"


  result := returnfailure;
  try

  // If no instance of Excel is running, try to Create a new Excel Object
  oleXL := CreateOleObject('Excel.Application');
  oleXL.Visible := false;

  // Add a new Workbook, and give it a name
  oleXL.Workbooks.Add(xlWBatWorkSheet);
  oleXL.Workbooks[1].WorkSheets[1].Name := 'Recon';
  oleXL.Activeworkbook.SaveAs(XLpath);

  // General Formatting
  oleXL.sheets['Recon'].Columns['B:C'].NumberFormat := '0.0';
  oleXL.sheets['Recon'].Columns['A:A'].ColumnWidth := 22;
  oleXL.sheets['Recon'].Cells.Font.size := 9;
  oleXL.sheets['Recon'].Cells.RowHeight := 12.75;
  oleXL.ActiveWindow.Zoom := 75;
  //oleXL.ActiveWindow.SplitRow := 7;

  // Print formatting
  oleXL.sheets['Recon'].PageSetup.PrintGridlines :=true;
  oleXL.sheets['Recon'].PageSetup.FitToPagesWide := 1;
  oleXL.sheets['Recon'].PageSetup.FitToPagesTall := 1;


  adoconnection1.ConnectionString :=
  	'Provider=Microsoft.Jet.OLEDB.4.0;'+
		'Data Source='+ modeldir + '\mdbs\modeldeployhydrology.mdb' + ';'+
		'Persist Security Info=False';
  adoconnection1.Open;
  adoquery1.Connection := adoconnection1;

  irow := 1;
  oleXL.sheets['Recon'].Cells[irow, 1] := modeldir;
  oleXL.sheets['Recon'].Cells[irow, 1 ].font.bold := true;
  oleXL.sheets['Recon'].Cells[irow, 1 ].font.size := 14;
  oleXL.sheets['Recon'].Rows['1:1'].RowHeight := 18;


  // QUERY 1
  irow := iQ1;
  oleXL.sheets['Recon'].Cells[irow, 1 ] := '1) Summary of Model H-Cards';
  oleXL.sheets['Recon'].Cells[irow, 1 ].font.bold := true;
  irow := irow + 1;

  adoquery1.SQL.clear;
  adoquery1.SQL.Add('select * from __01_SumHCardsBySource');
  adoquery1.Open;
  adoquery1.FindFirst;

  for i := 0 to adoquery1.FieldList.count - 1 do
  begin
    oleXL.sheets['Recon'].Cells[irow, i+ 1 ] := adoquery1.Fields[i].displayname;
  end;
  irow := irow + 1;

  while not adoquery1.Eof do
  begin
    v := adoquery1.fields[0].Value;
    oleXL.sheets['Recon'].Cells[irow,1 ] := v;
    v := adoquery1.fields[1].Value;
    oleXL.sheets['Recon'].Cells[irow,2 ] := v;
    v := adoquery1.fields[2].Value;
    oleXL.sheets['Recon'].Cells[irow,3 ] := v;
    adoquery1.Next;

    irow := irow + 1;
  end;

  oleXL.sheets['Recon'].Cells[irow,1 ] := 'Disco to SSC not in model';
  // the formula gets inserted later
  strRC := '=R[' + inttostr(iQ4 - iQ1 + 13) + ']C';
  oleXL.sheets['Recon'].Cells[irow,2 ].FormulaR1C1 := '=Total_DiscoVeg_not_in_model';
  oleXL.sheets['Recon'].Cells[irow,3 ].FormulaR1C1 := '=Imperv_DiscoVeg_not_in_model';
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow,1 ] := 'Adjust for SSC Imp Area > Area';
  oleXL.sheets['Recon'].Cells[irow,3 ] := '=Adj_SSC_Imp_Area_GT_SSC_Area';
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow,1 ] := 'Adjusted Hcard Areas';
  oleXL.sheets['Recon'].Cells[irow, 1 ].font.bold := true;
  strRC :=  '=sum(R' + inttostr(iQ1 + 2) + 'C:R[-1]C)';
  oleXL.sheets['Recon'].Cells[irow,2 ].FormulaR1C1 := strRC;
  oleXL.sheets['Recon'].Cells[irow,3 ].FormulaR1C1 := strRC;

  strRC := '=Recon!R' + inttostr(irow) + 'C2';
  oleXL.sheets['Recon'].Names.Add('Adj_HCard_Total_All',strRC);

  strRC := '=Recon!R' + inttostr(irow) + 'C3';
  oleXL.sheets['Recon'].Names.Add('Adj_HCard_Total_IA',strRC);
  irow := irow + 1;



  strRC:= inttostr(irow) + ':' + inttostr(irow);
  oleXL.sheets['Recon'].Cells[irow,1 ] := 'CheckSum';
  oleXL.sheets['Recon'].Cells[irow, 1].HorizontalAlignment := xlRight;
  oleXL.sheets['Recon'].Cells[irow, 1].Font.Size := 14;
  oleXL.sheets['Recon'].Cells[irow, 1].Font.Bold := true;
  oleXL.sheets['Recon'].Rows[strRC].RowHeight := 18;

  oleXL.sheets['Recon'].Cells[irow,2 ].FormulaR1C1 := '=Adj_HCard_Total_All-Adj_GIS_Total_ALL';
  oleXL.sheets['Recon'].Cells[irow,3 ].FormulaR1C1 := '=R[-1]C-R[9]C';
  oleXL.sheets['Recon'].Names.Add('Checksum_Hcard_TA','=Recon!R' + inttostr(irow) + 'C2');
  oleXL.sheets['Recon'].Names.Add('Checksum_Hcard_IA','=Recon!R' + inttostr(irow) + 'C3');
  irow := irow + 2;

  oleXL.sheets['Recon'].Cells[irow,1 ] := 'Total SSC Area in GIS';
  oleXL.sheets['Recon'].Cells[irow,2 ].FormulaR1C1 := '=SSC_SumOftotalgrossacres';
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow,1 ] := 'Roof and Parking Outside of SSC';
  oleXL.sheets['Recon'].Cells[irow,2 ].FormulaR1C1 := '=dsc_GIS_total-dsc_in_ssc_total';
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow,1 ] := 'Total ssc + dsc';
  oleXL.sheets['Recon'].Cells[irow,2 ].FormulaR1C1 := '=SUM(R[-2]C:R[-1]C)';
  strRC := '=Recon!R' + inttostr(irow) + 'C2';
  oleXL.sheets['Recon'].Names.Add('Adj_GIS_Total_ALL',strRC);
  irow := irow + 2;

  oleXL.sheets['Recon'].Cells[irow,1 ] := 'All Impervious SSC';
  oleXL.sheets['Recon'].Cells[irow,3 ].FormulaR1C1 := '=R' + inttostr(iQ5 + 9) + 'C2';
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow,1 ] := 'All DSC';
  oleXL.sheets['Recon'].Cells[irow,3 ].FormulaR1C1 := '=R' + inttostr(iQ2 + 3) + 'C2';
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow,1 ] := 'Total Effective Disco To Veg';
  oleXL.sheets['Recon'].Cells[irow,3 ].FormulaR1C1 := '=-R' + inttostr(iQ3 + 6) + 'C3';
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow,1 ] := 'Total Impervious Area in GIS';
  oleXL.sheets['Recon'].Cells[irow,3 ].FormulaR1C1 := '=sum(R[-1]C:R[-3]C)';
  irow := irow + 1;


  //Query 2
  irow := iQ2;
  oleXL.sheets['Recon'].Cells[irow, 1 ] := '2) Summary of Roof and Parking in mdl_DSC GIS layer';
  oleXL.sheets['Recon'].Cells[irow, 1 ].font.bold := true;
  irow := irow + 1;

  adoquery1.SQL.clear;
  adoquery1.SQL.Add('select * from __02_Total_RoofandParkingInGIS');
  adoquery1.Open;
  adoquery1.FindFirst;

  if adoquery1.eof then
  begin
    showmessage('recon query [__02_Total_RoofandParkingInGIS] has no records' + #13 + 'Call 37735');
    //gotta do some cleanup here
  end;

  oleXL.sheets['Recon'].Cells[irow,1 ] := adoquery1.Fields[0].displayname;
  oleXL.sheets['Recon'].Cells[irow,2 ] := adoquery1.Fields[0].Value;
  //RC[-1]-(R[16]C[-1]-R[16]C)
  strRC := '=RC[-1]- (R' + inttostr(iQ3+2) + 'C2-R' + inttostr(iQ3+2) +'C3)';
  oleXL.sheets['Recon'].Cells[irow,3].FormulaR1C1 := strRC;
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow,1 ] := adoquery1.Fields[1].displayname;
  oleXL.sheets['Recon'].Cells[irow,2 ] := adoquery1.Fields[1].Value;
  strRC := '=RC[-1]- (R' + inttostr(iQ3+3) + 'C2-R' + inttostr(iQ3+3) +'C3)';
  oleXL.sheets['Recon'].Cells[irow,3].FormulaR1C1 := strRC;
  irow := irow + 1;

  strRC := '=sum(R[-1]C:R[-2]C)';
  oleXL.sheets['Recon'].Cells[irow, 1] := 'Total Roof and Parking';
  oleXL.sheets['Recon'].Cells[irow, 2] := strRC;
  oleXL.sheets['Recon'].Cells[irow, 3] := strRC;

  strRC := '=R' + inttostr(iQ2 + 3) + 'C2';
  oleXL.sheets['Recon'].Names.Add('dsc_GIS_total',strRC);
  strRC := '=R' + inttostr(iQ2 + 3) + 'C3';
  oleXL.sheets['Recon'].Names.Add('dsc_GIS_EffImp',strRC);
  irow := irow + 1;


  strRC:= inttostr(irow) + ':' + inttostr(irow);
  oleXL.sheets['Recon'].Cells[irow,1 ] := 'CheckSum';
  oleXL.sheets['Recon'].Cells[irow, 1].HorizontalAlignment := xlRight;
  oleXL.sheets['Recon'].Cells[irow, 1].Font.Size := 14;
  oleXL.sheets['Recon'].Cells[irow, 1].Font.Bold := true;
  oleXL.sheets['Recon'].Rows[strRC].RowHeight := 18;


  oleXL.sheets['Recon'].Cells[irow, 2] := '=dsc_GIS_total - dsc_HCards_plus_Disco_Total';
  oleXL.sheets['Recon'].Cells[irow, 3] := '=dsc_GIS_EffImp - dsc_HCards_plus_Disco_EffImp';

  oleXL.sheets['Recon'].Names.Add('Checksum_dsc_TA','=Recon!R' + inttostr(irow) + 'C2');
  oleXL.sheets['Recon'].Names.Add('Checksum_dsc_IA','=Recon!R' + inttostr(irow) + 'C3');
  irow := irow + 2;


  oleXL.sheets['Recon'].Cells[irow, 1] := 'Roof And Parking Within Surface SC';
  oleXL.sheets['Recon'].Cells[irow, 1].Font.Bold := true;
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow, 1] := 'Parking';
  oleXL.sheets['Recon'].Cells[irow, 2] :='=SSC_SumOfc_PKgrossacres';
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow, 1] := 'Roof';
  oleXL.sheets['Recon'].Cells[irow, 2] :='=SSC_SumOfc_RFgrossacres';
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow, 1] := 'Total';
  oleXL.sheets['Recon'].Cells[irow, 1].HorizontalAlignment := xlRight;
  oleXL.sheets['Recon'].Cells[irow, 1].Font.Bold := true;
  strRC := '=sum(R[-1]C:R[-2]C)';
  oleXL.sheets['Recon'].Cells[irow, 2] := strRC;
  oleXL.sheets['Recon'].Cells[irow, 3] := strRC;

  strRC := '=R' + inttostr(iQ2 + 9) + 'C2';
  oleXL.sheets['Recon'].Names.Add('dsc_in_ssc_total',strRC);
  strRC := '=R' + inttostr(iQ2 + 9) + 'C3';
  oleXL.sheets['Recon'].Names.Add('dsc_in_ssc_EffImp',strRC);



  // QUERY 3
  irow := iQ3;
  oleXL.sheets['Recon'].Cells[irow, 1 ] := '3) Summary of DSC IC + H Cards';
  oleXL.sheets['Recon'].Cells[irow, 1 ].font.bold := true;
  irow := irow + 1;

  adoquery1.SQL.clear;
  adoquery1.SQL.Add('select * from __03_SummaryICVeg');
  adoquery1.Open;
  adoquery1.FindFirst;
  for i := 0 to adoquery1.FieldList.count - 1 do
  begin
    oleXL.sheets['Recon'].Cells[irow, i+ 1 ] := adoquery1.Fields[i].displayname;
  end;
  irow := irow + 1;

  while not adoquery1.Eof do
  begin
    v := adoquery1.fields[0].Value;
    oleXL.sheets['Recon'].Cells[irow,1 ] := v;
    v := adoquery1.fields[1].Value;
    oleXL.sheets['Recon'].Cells[irow,2 ] := v;
    v := adoquery1.fields[2].Value;
    oleXL.sheets['Recon'].Cells[irow,3 ] := v;
    adoquery1.Next;

    irow := irow + 1;
  end;
  oleXL.sheets['Recon'].Cells[irow,1] := 'Total';
  oleXL.sheets['Recon'].Cells[irow,2].FormulaR1C1 := '=sum(R[-2]C:R[-1]C)';
  oleXL.sheets['Recon'].Cells[irow,3].FormulaR1C1 := '=sum(R[-2]C:R[-1]C)';
  irow := irow + 2;

  oleXL.sheets['Recon'].Cells[irow,1] := 'Effective Disco to Veg';
  oleXL.sheets['Recon'].Cells[irow,3].FormulaR1C1 := '=R[-2]C[-1]-R[-2]C';
  irow := irow + 2;

  for i := iQ1 + 2 to iQ2 - 1 do
  begin
    mystr := midstr(uppercase(widestring(oleXL.sheets['Recon'].Cells[i,1].value)),1,1);
    if mystr = 'D' then
    begin
      break;
    end;

    if AnsiSameText(mystr,'R') or AnsiSameText(mystr,'P') then
    begin
      strRC := '=R[' + inttostr(i - irow) + ']C';
      oleXL.sheets['Recon'].Cells[irow,1].FormulaR1C1 := strRC;
      oleXL.sheets['Recon'].Cells[irow,2].FormulaR1C1 := strRC;
      oleXL.sheets['Recon'].Cells[irow,3].FormulaR1C1 := strRC;
      oleXL.sheets['Recon'].range[oleXL.sheets['Recon'].Cells[irow,1],oleXL.sheets['Recon'].Cells[irow,3]].Font.ColorIndex := xlcRed;
      irow := irow + 1;
    end;

  end;
  oleXL.sheets['Recon'].Cells[irow,1] := 'Total DSC';
  oleXL.sheets['Recon'].Cells[irow,1].font.bold := true;
  strRC := '=SUM(' +
              'R[-1]C:R[' + inttostr(iQ3 - irow + 8) + ']C, ' +
              'R[' + inttostr(iq3 - irow + 2) +']C, ' +
              'R[' + inttostr(iq3 - irow + 3) +']C' +
              ')';
  oleXL.sheets['Recon'].Cells[irow,2].FormulaR1C1 := strRC;
  oleXL.sheets['Recon'].Cells[irow,3].FormulaR1C1 := strRC;
  oleXL.sheets['Recon'].range[oleXL.sheets['Recon'].Cells[irow,2],oleXL.sheets['Recon'].Cells[irow,3]].Font.ColorIndex := xlcRed;

  oleXL.sheets['Recon'].Names.Add('dsc_HCards_plus_Disco_Total','=Recon!R' + inttostr(irow) + 'C2');
  oleXL.sheets['Recon'].Names.Add('dsc_HCards_plus_Disco_EffImp','=Recon!R' + inttostr(irow) + 'C3');


  // QUERY 4
  irow := iq4;
  oleXL.sheets['Recon'].Cells[irow,1] := '4) Total Surface Area calculated in GIS';
  oleXL.sheets['Recon'].Cells[irow,1].font.bold := true;
  irow := irow + 1;

  adoquery1.SQL.clear;
  adoquery1.SQL.Add('select * from __04_Total_SurfaceAreaInGIS');
  adoquery1.Open;
  adoquery1.FindFirst;

  if adoquery1.eof then
  begin
    showmessage('recon query [__04_Total_SurfaceAreaInGIS] has no records');
    //gotta do some cleanup here
  end;

  for i := 0 to adoquery1.FieldList.count - 1 do
  begin
    oleXL.sheets['Recon'].Cells[irow,1] := adoquery1.Fields[i].displayname;
    oleXL.sheets['Recon'].Cells[irow,2] := adoquery1.Fields[i].Value;
    irow := irow + 1;
  end;
  strRC :='=R[-6]C-R[-5]C-R[-4]C-R[-3]C+R[-2]C+R[-1]C';
  oleXL.sheets['Recon'].Cells[irow,2].FormulaR1C1 := strRC;
  oleXL.sheets['Recon'].Cells[irow,2].Font.ColorIndex := xlcRed;

  strRC := '=Recon!R' + inttostr(iQ4 + 1) + 'C2';
  oleXL.sheets['Recon'].Names.Add('SSC_SumOftotalgrossacres',strRC);

  strRC := '=Recon!R' + inttostr(iQ4 + 3) + 'C2';
  oleXL.sheets['Recon'].Names.Add('SSC_SumOfc_RFgrossacres',strRC);

  strRC := '=Recon!R' + inttostr(iQ4 + 4) + 'C2';
  oleXL.sheets['Recon'].Names.Add('SSC_SumOfc_PKgrossacres',strRC);
  irow := irow + 2;


  oleXL.sheets['Recon'].Cells[irow,1] := 'Calculate DSC disco to areas outside of model';
  oleXL.sheets['Recon'].Cells[irow,1].font.bold := true;
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow,1] := 'Sum of disco to ssc in model';
  oleXL.sheets['Recon'].Cells[irow,2].FormulaR1C1 := '=sum(R[-5]C:R[-4]C)';
  oleXL.sheets['Recon'].Cells[irow,2].Font.ColorIndex := xlcRed;
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow,1] := 'Sum of disco to ssc total';
  oleXL.sheets['Recon'].Cells[irow,2].FormulaR1C1 := '=sum(R[' + inttostr(iq3 - irow + 2) +']C:R[' + inttostr(iq3 - irow  + 3) +']C)';
  oleXL.sheets['Recon'].Cells[irow,2].Font.ColorIndex := xlcRed;
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow,1] := 'Total DiscoVeg not in model';
  oleXL.sheets['Recon'].Cells[irow,2].FormulaR1C1 := '=R[-1]C-R[-2]C';
  oleXL.sheets['Recon'].Cells[irow,2].Font.ColorIndex := xlcRed;
  strRC := '=Recon!R' + inttostr(irow) + 'C2';
  oleXL.sheets['Recon'].Names.Add('Total_DiscoVeg_not_in_model',strRC);

  //Query 5
  irow := iQ5;
  oleXL.sheets['Recon'].Cells[irow, 1 ] := '5) Impervious Surface Area in GIS';
  oleXL.sheets['Recon'].Cells[irow, 1 ].font.bold := true;
  irow := irow + 1;

  adoquery1.SQL.clear;
  adoquery1.SQL.Add('select * from __05_Impervious_SurfaceAreaInGIS');
  adoquery1.Open;
  adoquery1.FindFirst;

  if adoquery1.eof then
  begin
    showmessage('recon query [__05_Impervious_SurfaceAreaInGIS] has no records');
    //gotta do some cleanup here
  end;

  for i := 0 to adoquery1.FieldList.count - 1 do
  begin
    oleXL.sheets['Recon'].Cells[irow,1 ] := adoquery1.Fields[i].displayname;
    oleXL.sheets['Recon'].Cells[irow,2 ] := adoquery1.Fields[i].Value;
    irow := irow + 1;
  end;

  oleXL.sheets['Recon'].Cells[irow,1] :='Check Model SSC IA';
  oleXL.sheets['Recon'].Cells[irow,2].FormulaR1C1 := '=SUM(R[-7]C:R[-3]C)-R[-2]C';
  oleXL.sheets['Recon'].Cells[irow,2].Font.ColorIndex := xlcRed;
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow,1] :='Imp Area from Surface Sources';
  oleXL.sheets['Recon'].Cells[irow,2].FormulaR1C1 := '=SUM(R[-8]C:R[-7]C,R[-4]C)-R[-3]C';
  oleXL.sheets['Recon'].Cells[irow,2].Font.ColorIndex := xlcRed;
  irow := irow + 2;

  oleXL.sheets['Recon'].Cells[irow,1] := 'Calculate INEFFECTIVE DSC disco to areas outside of model';
  oleXL.sheets['Recon'].Cells[irow,1].font.bold := true;
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow,1] := 'Ineffective disco to SSC';
  oleXL.sheets['Recon'].Cells[irow,3].FormulaR1C1 := '=SUM(R[-9]C[-1]:R[-8]C[-1])';
  oleXL.sheets['Recon'].Cells[irow,3].Font.ColorIndex := xlcRed;
  irow := irow + 1;

  oleXL.sheets['Recon'].Cells[irow,1] := 'Ineffective disco in mdl_ic_disco_veg';
  oleXL.sheets['Recon'].Cells[irow,3].FormulaR1C1 := '=sum(R[' + inttostr(iq3 - irow + 2) +']C:R[' + inttostr(iq3 - irow  + 3) +']C)';
  oleXL.sheets['Recon'].Cells[irow,3].Font.ColorIndex := xlcRed;
  irow := irow  + 1;

  oleXL.sheets['Recon'].Cells[irow,1] := 'Imperv DiscoVeg not in model';
  oleXL.sheets['Recon'].Cells[irow, 1].HorizontalAlignment := xlRight;

  strRC := '=Recon!R' + inttostr(irow) + 'C3';
  oleXL.sheets['Recon'].Names.Add('Imperv_DiscoVeg_not_in_model',strRC);
  oleXL.sheets['Recon'].Cells[irow,3].FormulaR1C1 := '=R[-1]C-R[-2]C';
  oleXL.sheets['Recon'].Cells[irow,3].Font.ColorIndex := xlcRed;
  oleXL.sheets['Recon'].Cells[irow,3].Font.Bold := True;

  // QUERY 6
  irow := iQ6;
  oleXL.sheets['Recon'].Cells[irow, 1 ] := '6) Adjustment due to SSC Imp Area > SSC Area';
  oleXL.sheets['Recon'].Cells[irow, 1 ].font.bold := true;
  irow := irow + 1;

  adoquery1.SQL.clear;
  adoquery1.SQL.Add('select * from __06_AdjustmentIA_Error');
  adoquery1.Open;
  adoquery1.FindFirst;
  for i := 0 to adoquery1.FieldList.count - 1 do
  begin
    oleXL.sheets['Recon'].Cells[irow, i+ 1 ] := adoquery1.Fields[i].displayname;
  end;
  irow := irow + 1;

  if adoquery1.eof then
    for i := 0 to adoquery1.FieldList.count - 1 do
      oleXL.sheets['Recon'].Cells[irow, i+ 1 ] := 0
  else
    for i := 0 to adoquery1.FieldList.count - 1 do
    begin
      v:= adoquery1.Fields[i].Value;
      if v = null then v :=0;
      oleXL.sheets['Recon'].Cells[irow, i+ 1 ] := v;
    end;

  irow := irow + 1;
  oleXL.sheets['Recon'].Cells[irow,1] := 'Adjustment ---->';
  oleXL.sheets['Recon'].Cells[irow,1].HorizontalAlignment := xlRight;

  oleXL.sheets['Recon'].Cells[irow,3].FormulaR1C1 := '=R[-1]C[-1]-R[-1]C[-2]';
  oleXL.sheets['Recon'].Cells[irow,3].Font.ColorIndex := xlcRed;
  oleXL.sheets['Recon'].Cells[irow,3].Font.Bold := True;
  strRC := '=Recon!R' + inttostr(irow) + 'C3';
  oleXL.sheets['Recon'].Names.Add('Adj_SSC_Imp_Area_GT_SSC_Area',strRC);

  oleXL.calculate;
  
  csHCardTA := oleXL.sheets['Recon'].Range['Checksum_Hcard_TA'].value;
  csHCardTA := oleXL.sheets['Recon'].Range['Checksum_Hcard_IA'].Value;
  csHCardTA := oleXL.sheets['Recon'].Range['Checksum_dsc_TA'].Value;
  csHCardTA := oleXL.sheets['Recon'].Range['Checksum_dsc_IA'].Value;

  //Clean Up
  adoquery1.Close;
  adoconnection1.Close;
  oleXL.Activeworkbook.Save;
  oleXL.Activeworkbook.close;
  oleXL := unassigned;

  result := returnsuccess;


  except
    ShowMessage('Fatal error in reconciliation Process' + #13 + '  call 823-7735');
    Exit;
  end;

end;
end.
