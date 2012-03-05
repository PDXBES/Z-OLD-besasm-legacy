{==fMain Unit===================================================================

	Main application frame

	Revision History
	2.11   03/21/2006 (AMM) Changed check for MDB presence when existing model is
										opened from using static string array to all unsharped keys.
	2.11   03/10/2006 (AMM) Master MDB copy method changed from using static string
										array of keys to all unsharped (uncommented) keys in [mdbs]
										section
	2.1    03/06/2003 (AMM) Changed time frame spec from EX/FU for both hydraulic and
										hydrologic components to separate specs
				 03/12/2003 (AMM) Moved initialization of MapInfo applications to
				 					  InitMapInfo procedure (originally in TfrmMain.FormCreate)
	1.5    ?          ?
	1.0    11/01/2002 Initial release

===============================================================================}

unit fMain;

interface

uses
	Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, ActnList, ActnMan, ToolWin, ActnCtrls, ActnMenus, ExtCtrls,
	IniFiles, Registry, StdActns, ImgList, StdCtrls,
	dmmapinfo, comobj, Mapinfo_TLB, DB, ADODB, idglobal,
	RzCommon, StrUtils, RzShellDialogs, RzGroupBar,
	RzPanel, RzSplit, RzLaunch, SqlTimSt, types, dateutils,
	XPStyleActnCtrls,  CodeSiteLogging, RzStatus, RzErrHnd, dxBar, xmldom, XMLIntf,
  msxmldom, XMLDoc, uEMGAATSSystemConfig, fChild, RzForms, cxStyles, pngimage,
  RzBckgnd;

type
	TMIglobalsrosetta = record
    miname :string;
    ininame :string;
    inivalue :string;
  end;


	TfrmMain = class(TForm)
    actManager: TActionManager;
    actFileExit: TFileExit;
		dlgOpen: TOpenDialog;
    ImageList1: TImageList;
    RzFrameController1: TRzFrameController;
    dlgSelectDirectory: TRzSelectFolderDialog;
    RzSizePanel1: TRzSizePanel;
    RzGroupBar1: TRzGroupBar;
    RzGroup1: TRzGroup;
    RzGroup2: TRzGroup;
    appLauncher: TRzLauncher;
		RzGroup3: TRzGroup;
    errHandler: TRzErrorHandler;
    barManager: TdxBarManager;
    mnuFile: TdxBarSubItem;
    mnuFileNewModel: TdxBarButton;
    mnuFileOpenModel: TdxBarButton;
    mnuFileExit: TdxBarButton;
    mnuView: TdxBarSubItem;
    mnuTools: TdxBarSubItem;
    mnuHelp: TdxBarSubItem;
    actHelpSendTicket: TAction;
    mnuHelpSendTicket: TdxBarButton;
    actToolsOptions: TAction;
    mnuToolsOptions: TdxBarButton;
    RzMenuController1: TRzMenuController;
    RzFormState1: TRzFormState;
    regIniFile: TRzRegIniFile;
    styleRepository: TcxStyleRepository;
    RzPanel1: TRzPanel;
    RzBackground1: TRzBackground;
    RzPanel2: TRzPanel;
    pnlMain: TRzPanel;
    RzStatusBar1: TRzStatusBar;
    paneGlyphStatus: TRzGlyphStatus;
    paneTextStatus: TRzStatusPane;
    mnuModel: TdxBarSubItem;
    mnuModelBuild: TdxBarButton;
    CSGlobalObject1: TCSGlobalObject;
    versionInfo: TRzVersionInfo;
		procedure FormCreate(Sender: TObject);
		procedure FormDestroy(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure actToolsOptionsExecute(Sender: TObject);
	private
		{ Private declarations }
    FirstRun: Boolean;
	public
		{ Public declarations }
		procedure HideWindows;
    procedure ConfigureSystem;
    function DisplayForm(ClassType: TfrmChildClass): TfrmChild;
    procedure InitializeGlobalObjects;
    procedure FinalizeGlobalObjects;
	end;

var
	frmMain: TfrmMain;

implementation

uses GlobalConstants, uWorkbenchDefs,	StSystem, fWelcome,
  uEMGWorkbenchManager, uEMGAATSModelTemplateConfig, uMSAccessManager,
  dOptions, dmHydroStats, uUtilities;

{$R *.dfm}


procedure TfrmMain.FormCreate(Sender: TObject);
var
	EXEDir: String;

begin
  frmMain.Caption := 'EMGAATS - ' + versionInfo.FileVersion + ' (' +
    EMGAATSVersionComment +')';

  if not Assigned(frmWelcome) then
    frmWelcome := TfrmWelcome.Create(Application, pnlMain);

  FirstRun := True;
  regIniFile.Path := GetApplicationUserConfigFileName;
end;


procedure TfrmMain.FormDestroy(Sender: TObject);
begin
  FinalizeGlobalObjects;
  FreeAndNil(SystemConfig);
end;

procedure TfrmMain.FormShow(Sender: TObject);
begin
  if FirstRun then
  begin
    InitializeGlobalObjects;
    ConfigureSystem;
    FirstRun := False;
  end;
end;

procedure TfrmMain.HideWindows;
var
	i: Integer;
begin
  for i := 0 to pnlMain.ControlCount-1 do
    pnlMain.Controls[i].Hide;
end;

function TfrmMain.DisplayForm(ClassType: TfrmChildClass): TfrmChild;
begin
  HideWindows;
  Result := ClassType.Create(Application, pnlMain);
  Result.Realign;
  Result.Show;
end;

procedure TfrmMain.FinalizeGlobalObjects;
begin
  FreeAndNil(EMGWorkbenchManager);
  FreeAndNil(MSAccessManager);
  FreeAndNil(ModelTemplateConfig);
end;

procedure TfrmMain.ConfigureSystem;
var
  i: Integer;
begin
  //Prepare User MRU files on Welcome form
  for i := 0 to SystemConfig.MRUModelsCount - 1 do
    frmWelcome.AddMRUFile(SystemConfig.MRUModels[i]);

  EMGWorkbenchManager.StatusPane := paneGlyphStatus;
  EMGWorkbenchManager.Initialize(SystemConfig.EMGWorkbench);

  frmWelcome.Show;
end;

procedure TfrmMain.actToolsOptionsExecute(Sender: TObject);
begin
  dlgOptions.ShowModal;
end;

procedure TfrmMain.InitializeGlobalObjects;
begin
  EMGWorkbenchManager := TEMGWorkbenchManager.Create;
  MSAccessManager := TMSAccessManager.Create;
  ModelTemplateConfig := TModelTemplateConfig.Create(
    IncludeTrailingPathDelimiter(ExtractFilePath(ParamStr(0)))+'EMGAATSModelTemplate.ini');
end;

end.
