unit dmHydroStats;

interface

uses Windows, SysUtils, DB, ADODB, Classes, ComObj, Variants, StrUtils,
  uEMGAATSModel, RzErrHnd;

type
  TdatmodHydroStats = class(TDataModule)
    adoQuery: TADOQuery;
    adoConnection: TADOConnection;
  private
    { Private declarations }
    fModel: TEMGAATSModel;
    fExcelApp : OleVariant;
    fErrHandler: TRzErrorHandler;
    procedure PrepareWorkbook(AFile: TFileName);
    procedure ConnectToHydrology;
    procedure Query1;
    procedure Query2;
    procedure Query3;
    procedure Query4;
    procedure Query5;
    procedure Query6;
    procedure Cleanup;
  public
    { Public declarations }
    csHCardTA : Variant;
    csHCardIA : Variant;
    csdscTA : Variant;
    csdscIA : Variant;
    constructor Initialize(AModel: TEMGAATSModel);
    procedure Calculate(AFile: TFileName);
    property ErrHandler: TRzErrorHandler read fErrHandler write fErrHandler;
  end;

var
  datmodHydroStats: TdatmodHydroStats;

implementation

uses uEMGAATSTypes;

const
  // Return codes
  ReturnSuccess = -1;
  ReturnCaution = 0;
  ReturnFailure = 1;
  // SheetType
  xlChart = -4109;
  xlWorksheet = -4167;
  // WBATemplate
  xlWBATWorksheet = -4167;
  xlWBATChart = -4109;
  // Page Setup
  xlPortrait = 1;
  xlLandscape = 2;
  xlPaperA4 = 9;
  // Format Cells
  xlBottom = -4107;
  xlLeft = -4131;
  xlRight = -4152;
  xlTop = -4160;
  // Text Alignment
  xlHAlignCenter = -4108;
  xlVAlignCenter = -4108;
  // Cell Borders
  xlThick = 4;
  xlThin = 2;
  //unofficial constants
  xlcRed = 3;
  {
  These constants indicate the row that spreadsheet data are refenced to. The six
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

{ TdatmodHydroStats }

procedure TdatmodHydroStats.Calculate(AFile: TFileName);
begin
  PrepareWorkbook(AFile);
  ConnectToHydrology;
  Query1;
  Query2;
  Query3;
  Query4;
  Query5;
  Query6;
  Cleanup;
end;

procedure TdatmodHydroStats.Cleanup;
begin
  adoConnection.Close;
  fExcelApp.ActiveWorkbook.Save;
  fExcelApp.ActiveWorkbook.Close;
  fExcelApp := Unassigned;
end;

procedure TdatmodHydroStats.ConnectToHydrology;
begin
  adoConnection.ConnectionString :=
  	'Provider=Microsoft.Jet.OLEDB.4.0;'+
		'Data Source='+ fModel.MDBs['ModelDeployHydrology'] + ';'+
		'Persist Security Info=False';
  adoConnection.Open;
  adoQuery.Connection := adoConnection;
end;

constructor TdatmodHydroStats.Initialize(AModel: TEMGAATSModel);
begin
  fModel := AModel;
  fExcelApp := CreateOleObject('Excel.Application');
  fExcelApp.Visible := False;
end;

procedure TdatmodHydroStats.PrepareWorkbook(AFile: TFileName);
var
  CurrentRow: Integer;
begin
  // Add a new Workbook, and give it a name
  fExcelApp.Workbooks.Add(xlWBatWorkSheet);
  fExcelApp.Workbooks[1].WorkSheets[1].Name := 'Recon';
  fExcelApp.DisplayAlerts := False;
  fExcelApp.ActiveWorkbook.SaveAs(AFile);

  // General Formatting
  fExcelApp.Sheets['Recon'].Columns['B:C'].NumberFormat := '0.0';
  fExcelApp.Sheets['Recon'].Columns['A:A'].ColumnWidth := 22;
  fExcelApp.Sheets['Recon'].Cells.Font.Size := 9;
  fExcelApp.Sheets['Recon'].Cells.RowHeight := 12.75;
  fExcelApp.ActiveWindow.Zoom := 75;
  //fExcelApp.ActiveWindow.SplitRow := 7;

  // Print formatting
  fExcelApp.Sheets['Recon'].PageSetup.PrintGridlines :=true;
  fExcelApp.Sheets['Recon'].PageSetup.FitToPagesWide := 1;
  fExcelApp.Sheets['Recon'].PageSetup.FitToPagesTall := 1;

  CurrentRow := 1;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1] := fModel.Path;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1 ].Font.Bold := true;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1 ].Font.Size := 14;
  fExcelApp.Sheets['Recon'].Rows['1:1'].RowHeight := 18;
end;

procedure TdatmodHydroStats.Query1;
var
  CurrentRow: Integer;
  QueryVal: Variant;
  i: Integer;
  Formula: string;
begin
  // QUERY 1
  CurrentRow := iQ1;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1 ] := '1) Summary of Model H-Cards';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1 ].Font.Bold := true;
  Inc(CurrentRow);

  adoQuery.SQL.Clear;
  adoQuery.SQL.Add('select * from __01_SumHCardsBySource');
  adoQuery.Open;
  adoQuery.FindFirst;

  for i := 0 to adoQuery.FieldList.count - 1 do
  begin
    fExcelApp.Sheets['Recon'].Cells[CurrentRow, i+ 1 ] := adoQuery.Fields[i].DisplayName;
  end;
  Inc(CurrentRow);

  while not adoQuery.Eof do
  begin
    QueryVal := adoQuery.fields[0].Value;
    fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := QueryVal;
    QueryVal := adoQuery.fields[1].Value;
    fExcelApp.Sheets['Recon'].Cells[CurrentRow,2 ] := QueryVal;
    QueryVal := adoQuery.fields[2].Value;
    fExcelApp.Sheets['Recon'].Cells[CurrentRow,3 ] := QueryVal;
    adoQuery.Next;

  Inc(CurrentRow);
  end;

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := 'Disco to SSC not in model';
  // the formula gets inserted later
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2 ].FormulaR1C1 := '=Total_DiscoVeg_not_in_model';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3 ].FormulaR1C1 := '=Imperv_DiscoVeg_not_in_model';
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := 'Adjust for SSC Imp Area > Area';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3 ] := '=Adj_SSC_Imp_Area_GT_SSC_Area';
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := 'Adjusted Hcard Areas';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1 ].font.bold := true;
  Formula :=  '=sum(R' + IntToStr(iQ1 + 2) + 'C:R[-1]C)';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2 ].FormulaR1C1 := Formula;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3 ].FormulaR1C1 := Formula;

  Formula := '=Recon!R' + IntToStr(CurrentRow) + 'C2';
  fExcelApp.Sheets['Recon'].Names.Add('Adj_HCard_Total_All',Formula);

  Formula := '=Recon!R' + IntToStr(CurrentRow) + 'C3';
  fExcelApp.Sheets['Recon'].Names.Add('Adj_HCard_Total_IA',Formula);
  Inc(CurrentRow);


  Formula:= IntToStr(CurrentRow) + ':' + IntToStr(CurrentRow);
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := 'CheckSum';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1].HorizontalAlignment := xlRight;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1].Font.Size := 14;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1].Font.Bold := true;
  fExcelApp.Sheets['Recon'].Rows[Formula].RowHeight := 18;

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2 ].FormulaR1C1 := '=Adj_HCard_Total_All-Adj_GIS_Total_ALL';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3 ].FormulaR1C1 := '=R[-1]C-R[9]C';
  fExcelApp.Sheets['Recon'].Names.Add('Checksum_Hcard_TA','=Recon!R' + IntToStr(CurrentRow) + 'C2');
  fExcelApp.Sheets['Recon'].Names.Add('Checksum_Hcard_IA','=Recon!R' + IntToStr(CurrentRow) + 'C3');
  Inc(CurrentRow, 2);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := 'Total SSC Area in GIS';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2 ].FormulaR1C1 := '=SSC_SumOftotalgrossacres';
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := 'Roof and Parking Outside of SSC';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2 ].FormulaR1C1 := '=dsc_GIS_total-dsc_in_ssc_total';
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := 'Total ssc + dsc';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2 ].FormulaR1C1 := '=SUM(R[-2]C:R[-1]C)';
  Formula := '=Recon!R' + IntToStr(CurrentRow) + 'C2';
  fExcelApp.Sheets['Recon'].Names.Add('Adj_GIS_Total_ALL',Formula);
  Inc(CurrentRow, 2);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := 'All Impervious SSC';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3 ].FormulaR1C1 := '=R' + IntToStr(iQ5 + 9) + 'C2';
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := 'All DSC';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3 ].FormulaR1C1 := '=R' + IntToStr(iQ2 + 3) + 'C2';
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := 'Total Effective Disco To Veg';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3 ].FormulaR1C1 := '=-R' + IntToStr(iQ3 + 6) + 'C3';
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := 'Total Impervious Area in GIS';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3 ].FormulaR1C1 := '=sum(R[-1]C:R[-3]C)';
  Inc(CurrentRow);

  adoQuery.Close;
end;

procedure TdatmodHydroStats.Query2;
var
  CurrentRow: Integer;
  Formula: string;
begin
  //Query 2
  CurrentRow := iQ2;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1 ] := '2) Summary of Roof and Parking in mdl_DSC GIS layer';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1 ].font.bold := true;
  CurrentRow := CurrentRow + 1;

  adoQuery.SQL.clear;
  adoQuery.SQL.Add('select * from __02_Total_RoofandParkingInGIS');
  adoQuery.Open;
  adoQuery.FindFirst;

  if adoQuery.Eof then
  begin
    ErrHandler.Add('HydroQC Query [__02_Total_RoofandParkingInGIS] has no records', etWarning);
    fModel.AddError(TEMGAATSError.Create('HydroQC Query [__02_Total_RoofandParkingInGIS] has no records',
      eetWarning));
  end;

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := adoQuery.Fields[0].DisplayName;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2 ] := adoQuery.Fields[0].Value;
  //RC[-1]-(R[16]C[-1]-R[16]C)
  Formula := '=RC[-1]- (R' + IntToStr(iQ3+2) + 'C2-R' + IntToStr(iQ3+2) +'C3)';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3].FormulaR1C1 := Formula;
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := adoQuery.Fields[1].DisplayName;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2 ] := adoQuery.Fields[1].Value;
  Formula := '=RC[-1]- (R' + IntToStr(iQ3+3) + 'C2-R' + IntToStr(iQ3+3) +'C3)';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3].FormulaR1C1 := Formula;
  Inc(CurrentRow);

  Formula := '=sum(R[-1]C:R[-2]C)';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1] := 'Total Roof and Parking';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 2] := Formula;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 3] := Formula;

  Formula := '=R' + inttostr(iQ2 + 3) + 'C2';
  fExcelApp.Sheets['Recon'].Names.Add('dsc_GIS_total',Formula);
  Formula := '=R' + inttostr(iQ2 + 3) + 'C3';
  fExcelApp.Sheets['Recon'].Names.Add('dsc_GIS_EffImp',Formula);
  Inc(CurrentRow);


  Formula:= IntToStr(CurrentRow) + ':' + IntToStr(CurrentRow);
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := 'CheckSum';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1].HorizontalAlignment := xlRight;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1].Font.Size := 14;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1].Font.Bold := true;
  fExcelApp.Sheets['Recon'].Rows[Formula].RowHeight := 18;


  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 2] := '=dsc_GIS_total - dsc_HCards_plus_Disco_Total';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 3] := '=dsc_GIS_EffImp - dsc_HCards_plus_Disco_EffImp';

  fExcelApp.Sheets['Recon'].Names.Add('Checksum_dsc_TA','=Recon!R' + IntToStr(CurrentRow) + 'C2');
  fExcelApp.Sheets['Recon'].Names.Add('Checksum_dsc_IA','=Recon!R' + IntToStr(CurrentRow) + 'C3');
  Inc(CurrentRow, 2);


  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1] := 'Roof And Parking Within Surface SC';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1].Font.Bold := true;
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1] := 'Parking';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 2] :='=SSC_SumOfc_PKgrossacres';
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1] := 'Roof';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 2] :='=SSC_SumOfc_RFgrossacres';
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1] := 'Total';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1].HorizontalAlignment := xlRight;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1].Font.Bold := true;
  Formula := '=sum(R[-1]C:R[-2]C)';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 2] := Formula;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 3] := Formula;

  Formula := '=R' + IntToStr(iQ2 + 9) + 'C2';
  fExcelApp.Sheets['Recon'].Names.Add('dsc_in_ssc_total', Formula);
  Formula := '=R' + IntToStr(iQ2 + 9) + 'C3';
  fExcelApp.Sheets['Recon'].Names.Add('dsc_in_ssc_EffImp', Formula);

  adoQuery.Close;
end;

procedure TdatmodHydroStats.Query3;
var
  CurrentRow: Integer;
  QueryVal: Variant;
  i: Integer;
  Formula: string;
  CheckType: String;
begin
  // QUERY 3
  CurrentRow := iQ3;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1 ] := '3) Summary of DSC IC + H Cards';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1 ].font.bold := true;
  Inc(CurrentRow);

  adoQuery.SQL.clear;
  adoQuery.SQL.Add('select * from __03_SummaryICVeg');
  adoQuery.Open;
  adoQuery.FindFirst;

  for i := 0 to adoQuery.FieldList.count - 1 do
  begin
    fExcelApp.Sheets['Recon'].Cells[CurrentRow, i+ 1 ] := adoQuery.Fields[i].displayname;
  end;
  Inc(CurrentRow);

  while not adoQuery.Eof do
  begin
    QueryVal := adoQuery.fields[0].Value;
    fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := QueryVal;
    QueryVal := adoQuery.fields[1].Value;
    fExcelApp.Sheets['Recon'].Cells[CurrentRow,2 ] := QueryVal;
    QueryVal := adoQuery.fields[2].Value;
    fExcelApp.Sheets['Recon'].Cells[CurrentRow,3 ] := QueryVal;
    adoQuery.Next;
    Inc(CurrentRow);
  end;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1] := 'Total';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2].FormulaR1C1 := '=sum(R[-2]C:R[-1]C)';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3].FormulaR1C1 := '=sum(R[-2]C:R[-1]C)';
  Inc(CurrentRow, 2);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1] := 'Effective Disco to Veg';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3].FormulaR1C1 := '=R[-2]C[-1]-R[-2]C';
  Inc(CurrentRow, 2);

  for i := iQ1 + 2 to iQ2 - 1 do
  begin
    CheckType := midstr(uppercase(widestring(fExcelApp.Sheets['Recon'].Cells[i,1].Value)),1,1);
    if CheckType = 'D' then
    begin
      Break;
    end;

    if AnsiSameText(CheckType,'R') or AnsiSameText(CheckType,'P') then
    begin
      Formula := '=R[' + inttostr(i - CurrentRow) + ']C';
      fExcelApp.Sheets['Recon'].Cells[CurrentRow,1].FormulaR1C1 := Formula;
      fExcelApp.Sheets['Recon'].Cells[CurrentRow,2].FormulaR1C1 := Formula;
      fExcelApp.Sheets['Recon'].Cells[CurrentRow,3].FormulaR1C1 := Formula;
      fExcelApp.Sheets['Recon'].range[fExcelApp.Sheets['Recon'].Cells[CurrentRow,1],fExcelApp.Sheets['Recon'].Cells[CurrentRow,3]].Font.ColorIndex := xlcRed;
      Inc(CurrentRow);
    end;
  end;

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1] := 'Total DSC';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1].Font.Bold := true;
  Formula := '=SUM(' +
              'R[-1]C:R[' + inttostr(iQ3 - CurrentRow + 8) + ']C, ' +
              'R[' + IntToStr(iq3 - CurrentRow + 2) +']C, ' +
              'R[' + IntToStr(iq3 - CurrentRow + 3) +']C' +
              ')';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2].FormulaR1C1 := Formula;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3].FormulaR1C1 := Formula;
  fExcelApp.Sheets['Recon'].Range[fExcelApp.Sheets['Recon'].Cells[CurrentRow,2],fExcelApp.Sheets['Recon'].Cells[CurrentRow,3]].Font.ColorIndex := xlcRed;

  fExcelApp.Sheets['Recon'].Names.Add('dsc_HCards_plus_Disco_Total','=Recon!R' + IntToStr(CurrentRow) + 'C2');
  fExcelApp.Sheets['Recon'].Names.Add('dsc_HCards_plus_Disco_EffImp','=Recon!R' + IntToStr(CurrentRow) + 'C3');

  adoQuery.Close;
end;

procedure TdatmodHydroStats.Query4;
var
  CurrentRow: Integer;
  i: Integer;
  Formula: string;
begin
  // QUERY 4
  CurrentRow := iq4;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1] := '4) Total Surface Area calculated in GIS';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1].Font.Bold := True;
  CurrentRow := CurrentRow + 1;

  adoQuery.SQL.Clear;
  adoQuery.SQL.Add('select * from __04_Total_SurfaceAreaInGIS');
  adoQuery.Open;
  adoQuery.FindFirst;

  if adoQuery.Eof then
  begin
    ErrHandler.Add('HydroQC Query [__04_Total_SurfaceAreaInGIS] has no records', etWarning);
    fModel.AddError(TEMGAATSError.Create('HydroQC Query [__04_Total_SurfaceAreaInGIS] has no records',
      eetWarning));
  end;

  for i := 0 to adoQuery.FieldList.count - 1 do
  begin
    fExcelApp.Sheets['Recon'].Cells[CurrentRow,1] := adoQuery.Fields[i].DisplayName;
    fExcelApp.Sheets['Recon'].Cells[CurrentRow,2] := adoQuery.Fields[i].Value;
    Inc(CurrentRow);
  end;
  Formula :='=R[-6]C-R[-5]C-R[-4]C-R[-3]C+R[-2]C+R[-1]C';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2].FormulaR1C1 := Formula;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2].Font.ColorIndex := xlcRed;

  Formula := '=Recon!R' + inttostr(iQ4 + 1) + 'C2';
  fExcelApp.Sheets['Recon'].Names.Add('SSC_SumOftotalgrossacres',Formula);

  Formula := '=Recon!R' + inttostr(iQ4 + 3) + 'C2';
  fExcelApp.Sheets['Recon'].Names.Add('SSC_SumOfc_RFgrossacres',Formula);

  Formula := '=Recon!R' + inttostr(iQ4 + 4) + 'C2';
  fExcelApp.Sheets['Recon'].Names.Add('SSC_SumOfc_PKgrossacres',Formula);
  Inc(CurrentRow, 2);


  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1] := 'Calculate DSC disco to areas outside of model';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1].font.bold := true;
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1] := 'Sum of disco to ssc in model';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2].FormulaR1C1 := '=sum(R[-5]C:R[-4]C)';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2].Font.ColorIndex := xlcRed;
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1] := 'Sum of disco to ssc total';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2].FormulaR1C1 := '=sum(R[' + IntToStr(iq3 - CurrentRow + 2) +']C:R[' + IntToStr(iq3 - CurrentRow  + 3) +']C)';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2].Font.ColorIndex := xlcRed;
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1] := 'Total DiscoVeg not in model';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2].FormulaR1C1 := '=R[-1]C-R[-2]C';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2].Font.ColorIndex := xlcRed;
  Formula := '=Recon!R' + IntToStr(CurrentRow) + 'C2';
  fExcelApp.Sheets['Recon'].Names.Add('Total_DiscoVeg_not_in_model',Formula);

  adoQuery.Close;
end;

procedure TdatmodHydroStats.Query5;
var
  CurrentRow: Integer;
  i: Integer;
  Formula: string;
begin
  //Query 5
  CurrentRow := iQ5;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1 ] := '5) Impervious Surface Area in GIS';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1 ].font.bold := true;
  Inc(CurrentRow);

  adoQuery.SQL.clear;
  adoQuery.SQL.Add('select * from __05_Impervious_SurfaceAreaInGIS');
  adoQuery.Open;
  adoQuery.FindFirst;

  if adoQuery.eof then
  begin
    ErrHandler.Add('HydroQC Query [__05_Impervious_SurfaceAreaInGIS] has no records', etWarning);
    fModel.AddError(TEMGAATSError.Create('HydroQC Query [__05_Impervious_SurfaceAreaInGIS] has no records',
      eetWarning));
  end;

  for i := 0 to adoQuery.FieldList.count - 1 do
  begin
    fExcelApp.Sheets['Recon'].Cells[CurrentRow,1 ] := adoQuery.Fields[i].DisplayName;
    fExcelApp.Sheets['Recon'].Cells[CurrentRow,2 ] := adoQuery.Fields[i].Value;
    Inc(CurrentRow);
  end;

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1] :='Check Model SSC IA';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2].FormulaR1C1 := '=SUM(R[-7]C:R[-3]C)-R[-2]C';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2].Font.ColorIndex := xlcRed;
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1] :='Imp Area from Surface Sources';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2].FormulaR1C1 := '=SUM(R[-8]C:R[-7]C,R[-4]C)-R[-3]C';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,2].Font.ColorIndex := xlcRed;
  Inc(CurrentRow, 2);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1] := 'Calculate INEFFECTIVE DSC disco to areas outside of model';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1].font.bold := true;
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1] := 'Ineffective disco to SSC';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3].FormulaR1C1 := '=SUM(R[-9]C[-1]:R[-8]C[-1])';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3].Font.ColorIndex := xlcRed;
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1] := 'Ineffective disco in mdl_ic_disco_veg';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3].FormulaR1C1 := '=sum(R[' + IntToStr(iq3 - CurrentRow + 2) +']C:R[' + IntToStr(iq3 - CurrentRow  + 3) +']C)';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3].Font.ColorIndex := xlcRed;
  Inc(CurrentRow);

  fExcelApp.Sheets['Recon'].Cells[CurrentRow,1] := 'Imperv DiscoVeg not in model';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow, 1].HorizontalAlignment := xlRight;

  Formula := '=Recon!R' + inttostr(CurrentRow) + 'C3';
  fExcelApp.Sheets['Recon'].Names.Add('Imperv_DiscoVeg_not_in_model',Formula);
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3].FormulaR1C1 := '=R[-1]C-R[-2]C';
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3].Font.ColorIndex := xlcRed;
  fExcelApp.Sheets['Recon'].Cells[CurrentRow,3].Font.Bold := True;

  adoQuery.Close;
end;

procedure TdatmodHydroStats.Query6;
var
  CurrentRow: Integer;
  QueryVal: Variant;
  i: Integer;
  Formula: string;
begin
  // QUERY 6
  CurrentRow := iQ6;
  fExcelApp.sheets['Recon'].Cells[CurrentRow, 1 ] := '6) Adjustment due to SSC Imp Area > SSC Area';
  fExcelApp.sheets['Recon'].Cells[CurrentRow, 1 ].font.bold := true;
  CurrentRow := CurrentRow + 1;

  adoQuery.SQL.clear;
  adoQuery.SQL.Add('select * from __06_AdjustmentIA_Error');
  adoQuery.Open;
  adoQuery.FindFirst;
  for i := 0 to adoQuery.FieldList.count - 1 do
  begin
    fExcelApp.sheets['Recon'].Cells[CurrentRow, i+ 1 ] := adoQuery.Fields[i].DisplayName;
  end;
  Inc(CurrentRow);

  if adoQuery.eof then
    for i := 0 to adoQuery.FieldList.count - 1 do
      fExcelApp.sheets['Recon'].Cells[CurrentRow, i+ 1 ] := 0
  else
    for i := 0 to adoQuery.FieldList.count - 1 do
    begin
      QueryVal:= adoQuery.Fields[i].Value;
      if QueryVal = Null then QueryVal :=0;
      fExcelApp.sheets['Recon'].Cells[CurrentRow, i+ 1 ] := QueryVal;
    end;

  CurrentRow := CurrentRow + 1;
  fExcelApp.sheets['Recon'].Cells[CurrentRow,1] := 'Adjustment ---->';
  fExcelApp.sheets['Recon'].Cells[CurrentRow,1].HorizontalAlignment := xlRight;

  fExcelApp.sheets['Recon'].Cells[CurrentRow,3].FormulaR1C1 := '=R[-1]C[-1]-R[-1]C[-2]';
  fExcelApp.sheets['Recon'].Cells[CurrentRow,3].Font.ColorIndex := xlcRed;
  fExcelApp.sheets['Recon'].Cells[CurrentRow,3].Font.Bold := True;
  Formula := '=Recon!R' + inttostr(CurrentRow) + 'C3';
  fExcelApp.sheets['Recon'].Names.Add('Adj_SSC_Imp_Area_GT_SSC_Area',Formula);

  fExcelApp.Calculate;

  csHCardTA := fExcelApp.sheets['Recon'].Range['Checksum_Hcard_TA'].Value;
  csHCardTA := fExcelApp.sheets['Recon'].Range['Checksum_Hcard_IA'].Value;
  csHCardTA := fExcelApp.sheets['Recon'].Range['Checksum_dsc_TA'].Value;
  csHCardTA := fExcelApp.sheets['Recon'].Range['Checksum_dsc_IA'].Value;

  adoQuery.Close;
end;

end.
