unit dSelectQuartersections;

interface

uses
	Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, StdCtrls, RzLabel, Mask, RzEdit, RzBtnEdt, RzPanel, ExtCtrls,
	RzDlgBtn, ComObj, MapInfo_TLB, RzGroupBar, RzTabs, RzButton, ImgList,
  ActnList, RzCmboBx, OleCtrls;

type
	TdlgSelectQuartersections = class(TForm)
		RzDialogButtons1: TRzDialogButtons;
    RzGroupBar1: TRzGroupBar;
    pageOptions: TRzPageControl;
    shtEmgaats: TRzTabSheet;
    shtManual: TRzTabSheet;
    RzGroup1: TRzGroup;
    RzLabel1: TRzLabel;
    pnlMap: TRzPanel;
    edtModelDirectory: TRzButtonEdit;
    RzLabel2: TRzLabel;
    RzLabel3: TRzLabel;
    pnlManualQuartersections: TRzPanel;
    btnOpenQuartersectionsMap: TRzButton;
    ImageList1: TImageList;
    RzButton2: TRzButton;
    RzLabel4: TRzLabel;
    btnZoomOut: TRzToolButton;
    btnZoomIn: TRzToolButton;
    btnSelect: TRzToolButton;
    btnPan: TRzToolButton;
    btnEMSelect: TRzToolButton;
    btnEMZoomIn: TRzToolButton;
    btnEMZoomOut: TRzToolButton;
    btnEMPan: TRzToolButton;
    RzLabel7: TRzLabel;
    btnRectSelect: TRzToolButton;
    btnEMRectSelect: TRzToolButton;
		btnReselectQuartersections: TRzButton;
    ActionList1: TActionList;
    shtRetrieveFromFile: TRzTabSheet;
    RzLabel5: TRzLabel;
    edtRainfallFileName: TRzButtonEdit;
    RzLabel6: TRzLabel;
    RzLabel8: TRzLabel;
    cmbEngineType: TRzComboBox;
    shtManualEntry: TRzTabSheet;
    RzLabel9: TRzLabel;
    memManualEntry: TRzMemo;
    RzLabel10: TRzLabel;
    procedure RzGroup1Items2Click(Sender: TObject);
    procedure edtModelDirectoryButtonClick(Sender: TObject);
    procedure RzDialogButtons1ClickOk(Sender: TObject);
    procedure RzGroup1Items0Click(Sender: TObject);
    procedure RzGroup1Items1Click(Sender: TObject);
    procedure btnOpenQuartersectionsMapClick(Sender: TObject);
    procedure btnSelectClick(Sender: TObject);
    procedure btnZoomInClick(Sender: TObject);
    procedure btnZoomOutClick(Sender: TObject);
    procedure btnPanClick(Sender: TObject);
    procedure btnRectSelectClick(Sender: TObject);
    procedure btnReselectQuartersectionsClick(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure RzGroup1Items3Click(Sender: TObject);
    procedure edtRainfallFileNameButtonClick(Sender: TObject);
	private
		{ Private declarations }
		appMapInfo: OleVariant;
	public
		{ Public declarations }
    procedure ActivateMapButtons(IsActive: Boolean);
	end;

var
	dlgSelectQuartersections: TdlgSelectQuartersections;

implementation

uses fMain, fPrepareVirtualGages, uMIConstants, InterfaceStreams;

{$R *.dfm}

procedure TdlgSelectQuartersections.edtModelDirectoryButtonClick(Sender: TObject);
var
	Path: String;
	MapperID: Integer;
	QuartersectionsMap: String;
begin
	frmMain.dlgOpen.Title := 'Open EMGAATS Model.INI file';
	frmMain.dlgOpen.Filter := 'EMGAATS Model INI File (*.ini)|*.ini|All files (*.*)|*.*';
	if frmMain.dlgOpen.Execute then
	begin
		appMapInfo := Unassigned;
		edtModelDirectory.Text := ExtractFileDir(frmMain.dlgOpen.FileName);;
		Path := edtModelDirectory.Text;
		if not FileExists(Path+'\model.ini') then
		begin
			MessageDlg('The selected directory is not an EMGAATS directory.', mtError, [mbOK], 0);
			Exit;
		end;

		Refresh;
		Application.ProcessMessages;
		Screen.Cursor := crHourglass;
		try
			appMapInfo := CreateOleObject('MapInfo.Application');
			if VarIsNull(appMapInfo) then
			begin
				ShowMessage('MapInfo not installed.');
				Exit;
			end;
			QuartersectionsMap := frmMain.RegSettings.ReadString(
				PrepareVirtualGagesSection, 'QuartersectionsMapLocation',
				DefaultQuartersectionsMap);
//      MapControl1.AddShapeFile('\\cassio\GIS3\mi_data_shp\BOUNDARY\COMBINED\WORKING', 'CSBAALL.shp');
			appMapInfo.Do('Set Application Window '+IntToStr(pnlMap.Handle));
			appMapInfo.Do('Set Next Document Parent '+IntToStr(pnlMap.Handle)+' Style 1');
			appMapInfo.Do('Open Table "'+Path+'\links\mdl_Links_ac.TAB'+'" Interactive');
			appMapInfo.Do('Map from mdl_Links_ac');
			MapperID := appMapInfo.Eval('WindowID(0)');
			appMapInfo.Do('Open Table "'+Path+'\surfsc\mdl_surfsc_ac.TAB'+'" Interactive');
			appMapInfo.Do('Add Map Auto Layer mdl_surfsc_ac');
			appMapInfo.Do('Open Table "'+Path+'\dsc\mdl_dirsc_ac.TAB'+'" Interactive');
			appMapInfo.Do('Add Map Auto Layer mdl_dirsc_ac');
			appMapInfo.Do('Set Map Window Frontwindow() Zoom Entire Layer "mdl_links_ac"');
			appMapInfo.Do('Open Table "'+QuartersectionsMap+'" Interactive As Qs_nad27');
			appMapInfo.Do('Add Map Auto Layer Qs_nad27');
			appMapInfo.Do('Select Qs_nad27.QS from Qs_nad27, mdl_Links_ac where Qs_nad27.obj '+
				'Intersects mdl_Links_ac.obj');
			appMapInfo.Do('Set Map Redraw Off');
			appMapInfo.Do('Set Map Layer "Qs_nad27" Label Font ("Arial",257,10,255,16776960) With Format$(QS, "0") Auto On Offset 0');
			appMapInfo.Do('Set Map Redraw On');
			appMapInfo.Do('Set Window '+IntToStr(MapperID)+' Front');
			appMapInfo.RunMenuCommand(mi_M_TOOLS_SELECTOR);
			ActivateMapButtons(True);
		finally
			Screen.Cursor := crDefault;
		end;
	end;
end;

procedure TdlgSelectQuartersections.RzDialogButtons1ClickOk(
	Sender: TObject);
var
	QuartersectionList: TStringList;
	RainInterfaceFile: TCustomFortranInterfaceFileStream;
	QSecRaw: Variant;
	NumRaingages: Integer;
	Dummy: Integer;
	RaingageID: String;
	i: Integer;
begin
	if (pageOptions.ActivePage = shtEmgaats) or (pageOptions.ActivePage = shtManual) then
	begin
		if not VarIsNull(appMapInfo) then
		begin
			if appMapInfo.Eval('SelectionInfo(3)') = 0 then // SEL_INFO_NROWS=3
			begin
				ShowMessage('No quartersections selected.');
				ModalResult := mrNone;
				Exit;
			end;

			QuartersectionList := TStringList.Create;
			appMapInfo.Do('Select QS from Selection Group By QS Into SelectedQS');
			appMapInfo.Do('Fetch First From SelectedQS');
			while not (appMapInfo.Eval('EOT(SelectedQS)') = 'T') do
			begin
				QSecRaw := appMapInfo.Eval('SelectedQS.QS');
				QuarterSectionList.Add(IntToStr(QSecRaw));
				appMapInfo.Do('Fetch Next From SelectedQS');
			end;
			frmPrepareVirtualGages.Quartersections := QuartersectionList;
		end;
	end
	else if pageOptions.ActivePage = shtRetrieveFromFile then
	begin
		case cmbEngineType.ItemIndex of
			1:
				begin
					QuartersectionList := TStringList.Create;
					RainInterfaceFile := TF95InterfaceFileStream.Create(edtRainfallFileName.Text,
						fmOpenRead or fmShareDenyWrite);
					NumRaingages := RainInterfaceFile.ReadInteger;
          Dummy := RainInterfaceFile.ReadInteger;
					for i := 1 to NumRaingages do
						QuartersectionList.Add(Trim(RainInterfaceFile.ReadString(8)));
          RainInterfaceFile.Free;
					frmPrepareVirtualGages.Quartersections := QuartersectionList;
				end;
		end;
	end
  else if pageOptions.ActivePage = shtManualEntry then
  begin
    QuarterSectionList := TStringList.Create;
    QuarterSectionList.AddStrings(memManualEntry.Lines);
    frmPrepareVirtualGages.Quartersections := QuarterSectionList;
  end;
end;

procedure TdlgSelectQuartersections.RzGroup1Items0Click(Sender: TObject);
begin
	if pageOptions.ActivePage <> shtEmgaats then
	begin
		ActivateMapButtons(False);
		appMapInfo := Unassigned;
		pageOptions.ActivePage := shtEmgaats;
	end;
end;

procedure TdlgSelectQuartersections.RzGroup1Items1Click(Sender: TObject);
begin
	if pageOptions.ActivePage <> shtManual then
	begin
		ActivateMapButtons(False);
		btnOpenQuartersectionsMap.Enabled := True;
		appMapInfo := Unassigned;
		pageOptions.ActivePage := shtManual;
	end;
end;

procedure TdlgSelectQuartersections.btnOpenQuartersectionsMapClick(Sender: TObject);
var
	QuartersectionsMap: String;
	CombinedBasinsMap: String;
  arcResult: Integer;
begin
	appMapInfo := Unassigned;
	Screen.Cursor := crHourglass;
	try
//    arcControl.Show;
//    arcControl.CheckMxFile('\\CASSIO\Modeling\Model_Programs\Interface Master\Code\VGageSelect.mxd');
//    arcControl.LoadMxFile('\\CASSIO\Modeling\Model_Programs\Interface Master\Code\VGageSelect.mxd');
//    arcControl.AddShapeFile('\\cassio\GIS3\mi_data_shp\BOUNDARY\COMBINED\WORKING', 'CSBAALL.shp');
		appMapInfo := CreateOleObject('MapInfo.Application');
		if VarIsNull(appMapInfo) then
		begin
			ShowMessage('MapInfo not installed.');
			Exit;
		end;
		QuartersectionsMap := frmMain.RegSettings.ReadString(
			PrepareVirtualGagesSection, 'QuartersectionsMapLocation',
			DefaultQuartersectionsMap);
		CombinedBasinsMap := frmMain.RegSettings.ReadString(
			PrepareVirtualGagesSection, 'CombinedBasinsLocation',
			DefaultCombinedBasinsMap);
		appMapInfo.Do('Set Application Window '+IntToStr(pnlManualQuartersections.Handle));
		appMapInfo.Do('Set Next Document Parent '+IntToStr(pnlManualQuartersections.Handle)+' Style 1');
		appMapInfo.Do('Open Table "'+QuartersectionsMap+'" Interactive As Qs_nad27');
		appMapInfo.Do('Open Table "'+CombinedBasinsMap+'" Interactive As CSBAALL');
		appMapInfo.Do('Map From CSBAALL');
		appMapInfo.Do('Set Map Window Frontwindow() Zoom Entire Layer 1');
		appMapInfo.Do('Add Map Auto Layer Qs_nad27');
		appMapInfo.Do('Set Map Layer "CSBAALL" Selectable Off');
		appMapInfo.Do('Set Map Redraw Off');
		appMapInfo.Do('Set Map Layer "Qs_nad27" Label Font ("Arial",257,10,255,16776960) With Format$(QS,"0") Auto On Offset 0 ');
		appMapInfo.Do('Set Map Redraw On');
		appMapInfo.RunMenuCommand(mi_M_TOOLS_SELECTOR);
		btnOpenQuartersectionsMap.Enabled := False;
		ActivateMapButtons(True);
	finally
    Screen.Cursor := crDefault;
	end;
end;

procedure TdlgSelectQuartersections.btnSelectClick(Sender: TObject);
begin
	appMapInfo.RunMenuCommand(mi_M_TOOLS_SELECTOR);
end;

procedure TdlgSelectQuartersections.btnZoomInClick(Sender: TObject);
begin
	appMapInfo.RunMenuCommand(mi_M_TOOLS_ZOOM_IN);
end;

procedure TdlgSelectQuartersections.btnZoomOutClick(Sender: TObject);
begin
  appMapInfo.RunMenuCommand(mi_M_TOOLS_ZOOM_OUT);
end;

procedure TdlgSelectQuartersections.btnPanClick(Sender: TObject);
begin
  appMapInfo.RunMenuCommand(mi_M_TOOLS_PAN);
end;

procedure TdlgSelectQuartersections.btnRectSelectClick(Sender: TObject);
begin
	appMapInfo.RunMenuCommand(mi_M_TOOLS_SEARCH_RECT);
end;

procedure TdlgSelectQuartersections.ActivateMapButtons(IsActive: Boolean);
begin
	if pageOptions.ActivePage = shtEmgaats then
	begin
		btnEMSelect.Enabled := IsActive;
		btnEMZoomIn.Enabled := IsActive;
		btnEMZoomOut.Enabled := IsActive;
		btnEMPan.Enabled := IsActive;
		btnEMRectSelect.Enabled := IsActive;
    btnReselectQuartersections.Enabled := IsActive;
	end
	else if pageOptions.ActivePage = shtManual then
	begin
		btnSelect.Enabled := IsActive;
		btnZoomIn.Enabled := IsActive;
		btnZoomOut.Enabled := IsActive;
		btnPan.Enabled := IsActive;
		btnRectSelect.Enabled := IsActive;
	end;
end;

procedure TdlgSelectQuartersections.btnReselectQuartersectionsClick(
  Sender: TObject);
begin
	if not VarIsNull(appMapInfo) then
		appMapInfo.Do('Select Qs_nad27.QS from Qs_nad27, mdl_Links_ac where Qs_nad27.obj '+
			'Intersects mdl_Links_ac.obj');
end;

procedure TdlgSelectQuartersections.FormShow(Sender: TObject);
begin
	ActivateMapButtons(False);
  memManualEntry.Lines.Clear;
  if Assigned(frmPrepareVirtualGages.Quartersections) then
    memManualEntry.Lines.AddStrings(frmPrepareVirtualGages.Quartersections);
  pageOptions.ActivePage := shtManualEntry;
end;

procedure TdlgSelectQuartersections.RzGroup1Items3Click(Sender: TObject);
begin
	pageOptions.ActivePage := shtRetrieveFromFile;
end;

procedure TdlgSelectQuartersections.edtRainfallFileNameButtonClick(
	Sender: TObject);
begin
	frmMain.dlgOpen.Title := 'Open rainfall interface file';
	frmMain.dlgOpen.Filter := 'Rainfall interface file (*.r??)|*.r??|Interface Files (*.int)|*.int|All files (*.*)|*.*';
	if frmMain.dlgOpen.Execute then
		edtRainfallFileName.Text := frmMain.dlgOpen.FileName;
end;

procedure TdlgSelectQuartersections.RzGroup1Items2Click(Sender: TObject);
begin
  pageOptions.ActivePage := shtManualEntry;
end;

end.
