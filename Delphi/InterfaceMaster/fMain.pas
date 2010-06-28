unit fMain;

interface

uses
	Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, dxBar, ExtCtrls, RzPanel, ActnList, StdCtrls,
	RzLabel, Registry, RzGroupBar, RzShellDialogs, RzCommon, RzSplit, RzStatus,
  RzBckgnd, jpeg, RzForms, pngimage;

const
  FileSection: String = 'InterfaceFixer Files';
	UnitsSection: String = 'InterfaceFixer Units';
	PrepareVirtualGagesSection: String = 'PrepareVirtualGages';
	DefaultRaingageBuilderDatabase: String =
		'\\CASSIO\Modeling\Virtual Raingage\BldVirt20010614.mdb';
	DefaultRaingageDatabase: String =
		'\\CASSIO\Modeling\Virtual Raingage\VirtRAINDATA.mdb';
	DefaultQuartersectionsMap: String =
		'\\CASSIO\GIS1\MI_DATA\Boundary\Grid\QS_NAD27.TAB';
	DefaultCombinedBasinsMap: String =
		'\\CASSIO\GIS1\MI_DATA\Boundary\Combined\Working\CSBAALL.TAB';
type
	TfrmMain = class(TForm)
    pnlMain: TRzPanel;
    dxBarManager1: TdxBarManager;
    mnuFile: TdxBarSubItem;
    mnuFileConvertInterface: TdxBarButton;
    mnuFileCalculateFlowStatistics: TdxBarButton;
    mnuFileExit: TdxBarButton;
    mnuFilePrepareVirtualGages: TdxBarButton;
		ActionList1: TActionList;
    actFileConvertInterface: TAction;
    actFileExit: TAction;
    actFileCalculateFlowStatistics: TAction;
    actFilePrepareVirtualGages: TAction;
    dlgOpen: TRzOpenDialog;
    dlgSave: TRzSaveDialog;
    RzFrameController1: TRzFrameController;
    dlgSelectDir: TRzSelectFolderDialog;
    actFileMultiCombineInterfaceFiles: TAction;
    RzSizePanel1: TRzSizePanel;
    RzGroupBar1: TRzGroupBar;
    RzGroup1: TRzGroup;
    RzGroup2: TRzGroup;
    versionInfo: TRzVersionInfo;
    actHelpAbout: TAction;
    mnuHelpAbout: TdxBarButton;
    mnuHelp: TdxBarSubItem;
    actFileViewInterface: TAction;
    RzBackground1: TRzBackground;
    mnuFileViewInterface: TdxBarButton;
    mnuFileMultiCombineInterfaceFiles: TdxBarButton;
    mnuHelpSendApplicationDiagnosis: TdxBarButton;
    actFileViewRainfallInterface: TAction;
    actFileBatchConvertCombine: TAction;
    actDistributeFlowsToModel: TAction;
    RzFormState1: TRzFormState;
    RzRegIniFile1: TRzRegIniFile;
    procedure actFileBatchConvertCombineExecute(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure actFileConvertInterfaceExecute(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure actFileExitExecute(Sender: TObject);
    procedure actFileCalculateFlowStatisticsExecute(Sender: TObject);
    procedure actFilePrepareVirtualGagesExecute(Sender: TObject);
    procedure RzGroup2Items0Click(Sender: TObject);
    procedure actFileMultiCombineInterfaceFilesExecute(Sender: TObject);
    procedure actHelpAboutExecute(Sender: TObject);
    procedure actFileViewInterfaceExecute(Sender: TObject);
    procedure actDistributeFlowsToModelExecute(Sender: TObject);
	private
		{ Private declarations }
	public
		{ Public declarations }
		RegSettings: TRegistryIniFile;
		procedure SaveDirToRegistry;
		procedure SaveSelectDirToRegistry;
    function InitRegistryDir: String;
    function ProcessBatchFile(AFileName: String): boolean;
	end;

var
  frmMain: TfrmMain;

implementation

uses fConvertInterface, fModuleMaster, fCalculateFlowStatistics,
  fPrepareVirtualGages, dOptions, fMultiCombineInterfaceFiles, dAbout,
  fViewInterface, fBatchConvertCombine, fDistributeFlowsToModel;

{$R *.dfm}

procedure TfrmMain.actFileConvertInterfaceExecute(Sender: TObject);
begin
	if not Assigned(frmConvertInterface) then
		frmConvertInterface := TfrmConvertInterface.Create(Application, pnlMain);
	frmConvertInterface.Show;
end;

procedure TfrmMain.FormCreate(Sender: TObject);
begin
	RegSettings := TRegistryIniFile.Create('InterfaceMaster');
	dlgOpen.InitialDir := InitRegistryDir;
	dlgSave.InitialDir := InitRegistryDir;
	Caption := 'InterfaceMaster '+versionInfo.FileVersion;
end;

procedure TfrmMain.FormDestroy(Sender: TObject);
begin
	RegSettings.Free;
end;

procedure TfrmMain.actFileExitExecute(Sender: TObject);
begin
	Close;
end;

procedure TfrmMain.actFileCalculateFlowStatisticsExecute(Sender: TObject);
begin
	if not Assigned(frmCalculateFlowStatistics) then
		frmCalculateFlowStatistics := TfrmCalculateFlowStatistics.Create(Application, pnlMain);
	frmCalculateFlowStatistics.Show;
end;

procedure TfrmMain.actFilePrepareVirtualGagesExecute(Sender: TObject);
begin
	if not Assigned(frmPrepareVirtualGages) then
		frmPrepareVirtualGages := TfrmPrepareVirtualGages.Create(Application, pnlMain);
	frmPrepareVirtualGages.Show;
end;

procedure TfrmMain.SaveDirToRegistry;
begin
	RegSettings.WriteString(FileSection, 'InitialDir', ExtractFileDir(dlgOpen.FileName));
end;

procedure TfrmMain.SaveSelectDirToRegistry;
begin
	RegSettings.WriteString(FileSection, 'InitialDir', dlgSelectDir.SelectedPathName);
end;

function TfrmMain.InitRegistryDir: String;
begin
	Result :=	RegSettings.ReadString(FileSection, 'InitialDir', GetCurrentDir);
end;

procedure TfrmMain.RzGroup2Items0Click(Sender: TObject);
begin
	dlgOptions.ShowModal;
end;

procedure TfrmMain.actFileMultiCombineInterfaceFilesExecute(
	Sender: TObject);
begin
	if not Assigned(frmMultiCombineInterfaceFiles) then
		frmMultiCombineInterfaceFiles := TfrmMultiCombineInterfaceFiles.Create(Application, pnlMain);
	frmMultiCombineInterfaceFiles.Show;
end;

procedure TfrmMain.actHelpAboutExecute(Sender: TObject);
begin
	dlgAbout.ShowModal;
end;

procedure TfrmMain.actFileViewInterfaceExecute(Sender: TObject);
begin
	if not Assigned(frmViewInterface) then
		frmViewInterface := TfrmViewInterface.Create(Application, pnlMain);
	frmViewInterface.Show;
end;

procedure TfrmMain.FormShow(Sender: TObject);
var
	i: Integer;
begin
	if ParamCount > 0 then
	begin
		if FindCmdLineSwitch('hide') then
			frmMain.WindowState := wsMinimized;
		if Lowercase(ParamStr(1)) <> '-hide' then // a batch file with commands supplied
			ProcessBatchFile(ParamStr(1));
	end;
end;

function TfrmMain.ProcessBatchFile(AFileName: String): boolean;
begin
	ShowMessage(Format('Processing %s', [AFileName]));
end;

procedure TfrmMain.actDistributeFlowsToModelExecute(Sender: TObject);
begin
	if not Assigned(frmDistributeFlowsToModel) then
		frmDistributeFlowsToModel := TfrmDistributeFlowsToModel.Create(Application, pnlMain);
	frmDistributeFlowsToModel.Show;
end;

procedure TfrmMain.actFileBatchConvertCombineExecute(Sender: TObject);
begin
	if not Assigned(frmBatchConvertCombine) then
		frmBatchConvertCombine := TfrmBatchConvertCombine.Create(Application, pnlMain);
	frmBatchConvertCombine.Show;
end;

end.
