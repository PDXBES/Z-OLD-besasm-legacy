unit dOptions;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, Mask, RzEdit, RzBtnEdt, RzLabel, RzGroupBar, RzTabs,
  ExtCtrls, RzDlgBtn;

type
  TdlgOptions = class(TForm)
    dlgbtnOptions: TRzDialogButtons;
    RzGroupBar1: TRzGroupBar;
    pageOptions: TRzPageControl;
    shtConvertInterfaceFile: TRzTabSheet;
    shtCalculateFlowStatistics: TRzTabSheet;
    RzGroup1: TRzGroup;
    shtPrepareVirtualGages: TRzTabSheet;
    RzLabel1: TRzLabel;
    RzLabel2: TRzLabel;
    RzLabel3: TRzLabel;
    edtQuartersectionsMapLocation: TRzButtonEdit;
    edtRaingageDatabaseLocation: TRzButtonEdit;
    edtRaingageBuilderDatabaseLocation: TRzButtonEdit;
    RzLabel4: TRzLabel;
    edtCombinedBasinsLocation: TRzButtonEdit;
    RzLabel5: TRzLabel;
    RzLabel6: TRzLabel;
    RzLabel7: TRzLabel;
    procedure RzGroup1Items2Click(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure dlgbtnOptionsClickOk(Sender: TObject);
    procedure edtQuartersectionsMapLocationButtonClick(Sender: TObject);
    procedure edtRaingageDatabaseLocationButtonClick(Sender: TObject);
    procedure edtRaingageBuilderDatabaseLocationButtonClick(
      Sender: TObject);
    procedure edtCombinedBasinsLocationButtonClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
	end;

var
  dlgOptions: TdlgOptions;

implementation

uses fMain;

{$R *.dfm}

procedure TdlgOptions.RzGroup1Items2Click(Sender: TObject);
begin
  pageOptions.ActivePage := shtPrepareVirtualGages;
end;

procedure TdlgOptions.FormShow(Sender: TObject);
begin
	edtQuartersectionsMapLocation.Text := frmMain.RegSettings.ReadString(
		PrepareVirtualGagesSection, 'QuartersectionsMapLocation',
		DefaultQuartersectionsMap);
	edtRaingageDatabaseLocation.Text := frmMain.RegSettings.ReadString(
		PrepareVirtualGagesSection, 'RaingageDatabaseLocation',
		DefaultRaingageDatabase);
	edtRaingageBuilderDatabaseLocation.Text := frmMain.RegSettings.ReadString(
		PrepareVirtualGagesSection, 'RaingageBuilderDatabaseLocation',
		DefaultRaingageBuilderDatabase);
	edtCombinedBasinsLocation.Text := frmMain.RegSettings.ReadString(
		PrepareVirtualGagesSection, 'CombinedBasinsLocation',
		DefaultCombinedBasinsMap);
end;

procedure TdlgOptions.dlgbtnOptionsClickOk(Sender: TObject);
begin
	frmMain.RegSettings.WriteString(PrepareVirtualGagesSection,
		'QuartersectionsMapLocation', edtQuartersectionsMapLocation.Text);
	frmMain.RegSettings.WriteString(PrepareVirtualGagesSection,
		'RaingageDatabaseLocation', edtRaingageDatabaseLocation.Text);
	frmMain.RegSettings.WriteString(PrepareVirtualGagesSection,
		'RaingageBuilderDatabaseLocation', edtRaingageBuilderDatabaseLocation.Text);
	frmMain.RegSettings.WriteString(PrepareVirtualGagesSection,
		'CombinedBasinsLocation', edtCombinedBasinsLocation.Text);
end;

procedure TdlgOptions.edtQuartersectionsMapLocationButtonClick(
  Sender: TObject);
begin
	frmMain.dlgOpen.Title := 'Select Quartersections Map File';
	frmMain.dlgOpen.Filter := 'MapInfo Table (*.tab)|*.tab|All Files (*.*)|*.*';
	if frmMain.dlgOpen.Execute then
		edtQuartersectionsMapLocation.Text := frmMain.dlgOpen.FileName;
end;

procedure TdlgOptions.edtRaingageDatabaseLocationButtonClick(
	Sender: TObject);
begin
	frmMain.dlgOpen.Title := 'Select Raingage Database';
	frmMain.dlgOpen.Filter := 'Access Database (*.mdb)|*.mdb|All Files (*.*)|*.*';
	if frmMain.dlgOpen.Execute then
		edtRaingageDatabaseLocation.Text := frmMain.dlgOpen.FileName;
end;

procedure TdlgOptions.edtRaingageBuilderDatabaseLocationButtonClick(
	Sender: TObject);
begin
	frmMain.dlgOpen.Title := 'Select Raingage Builder Database';
	frmMain.dlgOpen.Filter := 'Access Database (*.mdb)|*.mdb|All Files (*.*)|*.*';
	if frmMain.dlgOpen.Execute then
		edtRaingageBuilderDatabaseLocation.Text := frmMain.dlgOpen.FileName;
end;

procedure TdlgOptions.edtCombinedBasinsLocationButtonClick(
	Sender: TObject);
begin
	frmMain.dlgOpen.Title := 'Select Combined Basins Map Location';
	frmMain.dlgOpen.Filter := 'MapInfo Table (*.tab)|*.tab|All Files (*.*)|*.*';
	if frmMain.dlgOpen.Execute then
		edtCombinedBasinsLocation.Text := frmMain.dlgOpen.FileName;
end;

end.
