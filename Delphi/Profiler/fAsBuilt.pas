unit fAsBuilt;

interface

uses
	Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, dxBar, StdCtrls, FileCtrl, RzFilSys, OleCtrls,
	RzLabel, dxPageControl, ExtCtrls, RzPanel, RzSplit, dxDBTLCl, dxGrClms,
	dxTL, dxDBCtrl, dxDBGrid, DB, ADODB, dxCntner, StStrL, ieview,
  ImageEnView, ImageEnIO, RzButton;

type
  TfrmAsBuilt = class(TForm)
    dxBarManager1: TdxBarManager;
    mnuFile: TdxBarSubItem;
    mnuFileClose: TdxBarButton;
    RzSizePanel1: TRzSizePanel;
    dxPageControl1: TdxPageControl;
    tabACAD: TdxTabSheet;
    tabRaster: TdxTabSheet;
    RzFileListBox1: TRzFileListBox;
    RzDirectoryListBox1: TRzDirectoryListBox;
    RzDriveComboBox1: TRzDriveComboBox;
    adoJobDetail: TADOTable;
    adoJobDetailJOBNUMBER: TWideStringField;
    adoJobDetailSGLSHEET: TWideStringField;
    adoJobDetailVIEW: TMemoField;
    dsJobDetail: TDataSource;
    adoSewerJobs: TADOConnection;
    ieInputOutput: TImageEnIO;
    ieView: TImageEnView;
    dxDBGrid1: TdxDBGrid;
    dxDBGrid1JOBNUMBER: TdxDBGridColumn;
    dxDBGrid1SGLSHEET: TdxDBGridColumn;
    dxDBGrid1VIEW: TdxDBGridMemoColumn;
		procedure RzFileListBox1Click(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure dxDBGrid1MouseUp(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
	frmAsBuilt: TfrmAsBuilt;

implementation

{$R *.dfm}

procedure TfrmAsBuilt.RzFileListBox1Click(Sender: TObject);
var
	FileToOpen: String;
	FileExt: String;
begin
	FileToOpen := RzFileListBox1.LongFileName;
	FileExt := UpperCase(ExtractFileExt(FileToOpen));
	if (FileExt = '.DWG') or (FileExt = '.DWF') then
	begin
		tabACAD.Show;
		//avvView.src := FileToOpen;
	end
	else
	begin
		tabRaster.Show;
		ieInputOutput.LoadFromFile(FileToOpen);
	end;
end;

procedure TfrmAsBuilt.FormShow(Sender: TObject);
begin
	adoSewerJobs.Open;
	adoJobDetail.Open;
end;

procedure TfrmAsBuilt.dxDBGrid1MouseUp(Sender: TObject;
	Button: TMouseButton; Shift: TShiftState; X, Y: Integer);
var
	FileToOpen: String;
	FileExt: String;
begin
	FileToOpen := TrimCharsL(dxDBGrid1.FocusedNode.Strings[dxDBGrid1.ColumnByFieldName('VIEW').Index],
		'#');
	FileExt := UpperCase(ExtractFileExt(FileToOpen));
	if (FileExt = '.DWG') or (FileExt = '.DWF') then
	begin
		tabACAD.Show;
		//avvView.src := FileToOpen;
	end
	else
	begin
		tabRaster.Show;
		ieInputOutput.LoadFromFile(FileToOpen);
	end;
end;

end.
