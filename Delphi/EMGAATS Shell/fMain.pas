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
	dmmapinfo, comobj, dmstatemachine, fStatus, Mapinfo_TLB, DB, ADODB, idglobal,
	unitmodelcontrols, RzCommon, StrUtils, RzShellDialogs, RzGroupBar,
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
		actBuildModels: TAction;
		actNewModel: TAction;
		actOpenModel: TAction;
		actMasterUpdateWithChangeLogs: TAction;
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
    actToolsInterfaceMaster: TAction;
    actToolsProfiler: TAction;
    actToolsUpstreamTracer: TAction;
    actToolsHoptimizer: TAction;
    actRestartWorkbenches: TAction;
    actBuildModelPipeSystem: TAction;
    actBuildModelLateralsAndParcels: TAction;
    actBuildModelSubcatchments: TAction;
    actBuildModelDeployModel: TAction;
    actBuildModelPerformQualityControl: TAction;
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
		procedure actBuildModelsExecute(Sender: TObject);
		procedure FormCreate(Sender: TObject);
		procedure FormDestroy(Sender: TObject);
		procedure actMasterUpdateWithChangeLogsExecute(Sender: TObject);
		procedure actNewModelExecute(Sender: TObject);
		procedure actOpenModelExecute(Sender: TObject);
		procedure FormClose(Sender: TObject; var Action: TCloseAction);
		procedure actToolsInterfaceMasterExecute(Sender: TObject);
		procedure actRestartWorkbenchesUpdate(Sender: TObject);
		procedure actRestartWorkbenchesExecute(Sender: TObject);
		procedure actBuildModelPipeSystemExecute(Sender: TObject);
		procedure actBuildModelLateralsAndParcelsExecute(Sender: TObject);
		procedure actBuildModelSubcatchmentsExecute(Sender: TObject);
		procedure actBuildModelDeployModelExecute(Sender: TObject);
		procedure actBuildModelPerformQualityControlExecute(Sender: TObject);
		procedure actBuildModelPipeSystemUpdate(Sender: TObject);
		procedure actBuildModelLateralsAndParcelsUpdate(Sender: TObject);
		procedure actBuildModelSubcatchmentsUpdate(Sender: TObject);
		procedure actBuildModelDeployModelUpdate(Sender: TObject);
		procedure actBuildModelPerformQualityControlUpdate(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure actToolsOptionsExecute(Sender: TObject);
	private
		{ Private declarations }
    FirstRun: Boolean;
	public
		{ Public declarations }
		UserRegistry: TRegistryIniFile;
		SystemIni: TMemIniFile;
		ModelIni: TMemIniFile;
		PatchIni: TMemIniFile;
		goleMI : Variant;
		goleCB : Variant;

		goleMItest : Variant;
		goleCBtest : Variant;

		EMGInitialized: Boolean;
    modelversion : string;
		function EnterModel(Sender: TObject ;  IsNewModel : boolean) : TmdlChangeStatus;
		function ExitModel : TmdlChangeStatus;
    function installmodelpatch(ptype : integer) : boolean;
    function copymastermdbs() : boolean;
		procedure InitMapInfo;
		procedure HideWindows;

    function CreateNewModel: Boolean;
    function OpenExistingModel: Boolean;
    function CopyStandardDirectories: Boolean;

    // EMGAATS 2.2 methods
    procedure ConfigureSystem;
    function DisplayForm(ClassType: TfrmChildClass): TfrmChild;
    procedure InitializeGlobalObjects;
    procedure FinalizeGlobalObjects;
  protected
    procedure CreateParams(var Params: TCreateParams); override;
	end;

var
	frmMain: TfrmMain;

implementation

uses fBuildModels, fModifyModelBoundaries,
	fBuildPipeSystem, fBuildLateralsAndParcels, fBuildSubcatchments,
	GlobalConstants, fDeployModelToEngine, fgettimeframe, uWorkbenchDefs,
	fPerformQualityControl, fModelResults, StSystem, fWelcome, uEMGWorkbenchManager,
  uEMGAATSModelTemplateConfig, uMSAccessManager, dOptions, dmHydroStats,
  uUtilities;

{$R *.dfm}


procedure TfrmMain.FormCreate(Sender: TObject);
var
	EXEDir: String;

begin

{	// Load EMGAATS (System) INI
	frmmain.caption := 'EMGAATS Framework ' + emgaatsversion + ', '+  emgaatsversioncomment;
	EXEDir := ExtractFileDir(ParamStr(0));
	if FileExists(EXEDir + '\emgaats.ini') then
		SystemIni := TMemIniFile.Create(EXEdir + '\'+SystemIniFileName)
	else
	begin
		MessageDlg(EXEDir + '\emgaats.ini not found', mtInformation, [mbOK], 0);
	end;

	PatchIni := TMemIniFile.Create(EXEdir + '\'+ PatchIniFileName);
	if not FileExists(EXEDir + '\' + PatchIniFileName) then
	begin
		PatchIni.WriteString('control','VersionPatch','OFF');
		PatchIni.UpdateFile;
	end;

	UserRegistry := TRegistryIniFile.Create(UserRegistryKey);}

//	frmmodifyModelBoundaries := TfrmModifyModelBoundaries.create(application, pnlmain);
//	frmBuildModels := TfrmBuildModels.create(application, pnlmain);
//	frmBuildPipeSystem := TfrmBuildPipeSystem.create(application, pnlmain);
//	frmBuildLateralsAndParcels := TfrmBuildLateralsAndParcels.create(application, pnlmain);
//	frmBuildSubcatchments := TfrmBuildSubcatchments.create(application, pnlmain);
//	frmDeployModel := TfrmDeployModel.create(application, pnlmain);
//	frmPerformQualityControl := TfrmPerformQualityControl.Create(Application, pnlMain);

//	frmmain.caption := 'EMGAATS Framework ' + emgaatsversion + ', '+  emgaatsversioncomment;
  frmMain.Caption := 'EMGAATS - ' + versionInfo.FileVersion + ' (' +
    EMGAATSVersionComment +')';

  if not Assigned(frmWelcome) then
    frmWelcome := TfrmWelcome.Create(Application, pnlMain);

  FirstRun := True;
  regIniFile.Path := GetApplicationUserConfigFileName;

end;


procedure TfrmMain.HideWindows;
var
	i: Integer;
begin
  for i := 0 to pnlMain.ControlCount-1 do
    pnlMain.Controls[i].Hide;
end;

procedure TfrmMain.actBuildModelsExecute(Sender: TObject);
begin
  HideWindows;
  if not Assigned(frmBuildModels) then
    frmBuildModels := TfrmBuildModels.Create(Application, pnlMain);
  frmBuildModels.Show;
end;


procedure TfrmMain.FormDestroy(Sender: TObject);
begin

  FinalizeGlobalObjects;
  FreeAndNil(SystemConfig);

	{ TODO -oDJC -cFinalization : Add code to save system and user settings }
{	UserRegistry.WriteString('LastSession', 'Directory', dlgSelectDirectory.SelectedPathName);


	SystemIni.Free;
	UserRegistry.Free;
	ModelIni.Free;}

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

procedure TfrmMain.actMasterUpdateWithChangeLogsExecute(Sender: TObject);
begin
	{ TODO -oDJC -cMaster Functionality : Update with Change Logs: Add code }
	ShowMessage('Not implemented');
end;

{TfrmMain.fEnterNewModel
 This function is called prior to entering a new or exising model
 There are three (3) states you can be in prior to calling this function
 mdlNull) No model is currently associated with the program
 mdlUnchanged) You are in a model that has not been changed
 mdlChanged) You are in a model and have changed it

 PRE: All UI forms have been created
 }
function TfrmMain.ExitModel : TmdlChangeStatus;
begin
  result := mdlNull;
	if statemachine.mdlChangeStatus <> mdlNull then
	begin
    case MessageDlg('Exit Current Model?',
      mtWarning, [mbYes, mbNo], 0) of
			mrYes: // User wants to leave
      begin
        // write ini file

        modelini.updatefile;
        statemachine.mdlChangeStatus := mdlNull;
				ShowMessage('STUB: call cleanup');
        hidewindows;

      end;
      mrNo:
			begin
        // User doesn't want to exit
        result := statemachine.mdlChangeStatus;
			end;
    end // case
  end; //if

end;

function Tfrmmain.copymastermdbs() : boolean;
var
	NumMDBSToCopy: Integer;
	i : integer;
	iniroot : string;
	MDBSToCopy: TStringList;
begin
	 iniroot := extractFiledir(frmmain.ModelIni.FileName);


	 frmstatus.StatusBox('Copying mdbs from master','','EnterModel');
// AMM 3/10/2006 Changed reading of mdbs-to-copy from static array in mdbkeys
//   to reading the entire mdbs section in the ini file
	MDBSToCopy := TStringList.Create;
	frmMain.ModelIni.ReadSection(MDBSSectionString, MDBSToCopy);
	NumMDBSToCopy := MDBSToCopy.Count;
	for i := 0 to NumMDBSToCopy - 1 do    // Iterate
	begin
		if Length(MDBSToCopy[i]) = 0 then
			Continue;

		// The following should never happen
		if ModelIni.ReadString(MDBSSectionString, MDBSToCopy[i], 'unknown') = 'unknown' then
		begin
			ShowMessage('mdb KEY not found' + #13 + #13 + MDBSToCopy[i] );
			Result := False ;
			Exit;
		end;

		// Check if mdb file actually exists
		// AMM 3/21/2006 Legacy 2.11 files may have keys that point at tables that shouldn't necessarily
		// be copied over since they may not exist.  If this is the case, instead of killing
		// the process, inform the user that the tables don't exist, and #-out the line
		if LeftStr(MDBSToCopy[i],1) = '#' then
			Continue
		else
		begin
			if not FileExists(ModelIni.ReadString(MDBSSectionString, MDBSToCopy[i], 'unknown')) then
			begin
				errHandler.Add('Master MDB not found: ' +
					ModelIni.ReadString(MDBSSectionString, MDBSToCopy[i],'unknown') +
					'', etWarning);
				ModelIni.WriteString(MDBSSectionString, '#'+MDBSToCopy[i],
					ModelIni.ReadString(MDBSSectionString, MDBSToCopy[i],'unknown'));
				ModelIni.DeleteKey(MDBSSectionString, MDBSToCopy[i]);
				StateMachine.MdlChangeStatus := mdlNull;
				Result := False;
				Continue;
			end;
		end;

		// Copy over the mdb
		DeleteFile(iniroot + MDBSDirPath + MDBSToCopy[i] + '.mdb');
		if CopyFile(ModelIni.ReadString(MDBSSectionString, MDBSToCopy[i], 'unknown'),
			iniroot + MDBSDirPath + MDBSToCopy[i] + '.mdb') then
		begin
			ShowMessage('Could not copy file: ' +
				modelini.ReadString(MDBSSectionString, MDBSToCopy[i],'unknown'));
			Result := False;
			Exit;
		end;
	end;    // for
	MDBSToCopy.Free;
{   for i := 0 to high(mdbkeys) do
	 begin

		 if modelini.ReadString('mdbs',mdbkeys[i],'unknown') = 'unknown' then
		 begin
			 showmessage('mdb KEY not found' + #13 + #13 + mdbkeys[i] );
			 result := false ;
			 exit;
		 end;
		 if not Fileexists(modelini.ReadString('mdbs',mdbkeys[i],'unknown')) then
		 begin
			 showmessage('mdb FILE not found: ' + #13 + #13 +
			 modelini.ReadString('mdbs',mdbkeys[i],'unknown'));
			 statemachine.mdlChangeStatus := mdlNull;
			 result := false;
			 exit;
		 end;

		 //copyfileto(
		 deletefile(iniroot + '\mdbs\' + mdbkeys[i] + '.mdb');
		 if not CopyFileTo(modelini.ReadString('mdbs',mdbkeys[i],'unknown'),
				 iniroot + '\mdbs\' + mdbkeys[i] + '.mdb' ) then
		 begin
			 showmessage('could not copy file: ' + modelini.ReadString('mdbs',mdbkeys[i],'unknown'));
			 result := false;
			 exit;
		 end;

	 end; // for}

	 errHandler.HandleErrors;
	 modelini.WriteString('Admin','mdbdate',DateTimeToStr(Date + Time));
	 result := true;
end;


function TfrmMain.CopyStandardDirectories: Boolean;
var
  i: Integer;
  iniroot: string;
begin
  { TODO -oAMM -cInifiles : Change standard directory declaration to be read from EMGAATS Ini file }
  iniroot := extractFiledir(frmmain.modelini.FileName);
  for i := 0 to high(modeldirs) do
  begin
    if not DirectoryExists(iniroot + '\' + modeldirs[i]) then
      if not CreateDir(iniroot + '\' + modeldirs[i]) then
      begin
        statemachine.mdlChangeStatus := mdlNull;
        raise Exception.Create('Cannot create ' + iniroot + '\' + modeldirs[i]);
        exit;
      end;
   end
end;

function TfrmMain.CreateNewModel: Boolean;
var
  dirResult: Integer;
  modelOverwriteWarning: string;
  inibuffer: TStringList;
  strDateTime: string;
  timeFrame: string;
begin
	dlgSelectDirectory.Title := 'Select Directory for New Model';
  if dlgSelectDirectory.Execute then
  begin
    // Check if directory already exists, and if so, send user a warning about the
    // contents
		if FileExists(dlgSelectDirectory.SelectedPathName + '\' + ModelIniFileName) then
    begin
      modelOverwriteWarning := 'An EMGAATS model already exists in this directory.' +
        #13#10 + 'If you continue, only the standard .MDB and MapInfo files that' +
        #13#10 + 'EMGAATS generates will be replaced. Continue?';
      dirResult := MessageDlg(modelOverwriteWarning, mtWarning, [mbYes, mbNo], 0);
			if dirResult = mrNo then
      begin
        StateMachine.mdlChangeStatus := mdlNull;
        Result := False;
        Exit; // in a mdlNull State
      end;
		end; // if fileexists

		//create the new model inifile in memory from the system ini file
		modelIni := TMemIniFile.Create(dlgSelectDirectory.SelectedPathName + '\'+ modelinifilename);
		iniBuffer := TStringList.Create;
		systemIni.GetStrings(iniBuffer);
		modelIni.SetStrings(iniBuffer);
		modelIni.WriteString('retrace','linkdbsrc',
			dlgSelectDirectory.SelectedPathName + '\links\mdl_links_ac.mdb');
		modelIni.WriteString('retrace','tracefile',
			dlgSelectDirectory.SelectedPathName + '\retrace.txt');

		strDateTime:=DateToStr(Date) + ' ' + Timetostr(Time);
		modelIni.Writestring('Admin', 'Created', strDateTime);

		//Get the time frame for a new model
		frmTimeFrame.ShowModal;
		if frmTimeFrame.mbrc = mbCancel then
		begin
			StateMachine.mdlChangeStatus := mdlNull;
			Result := False;
			Exit;
		end
		else
		begin
			case frmTimeFrame.rdgTimeFrame.ItemIndex of
				-1: // no time frame selected -- this should be unreached
				begin
					ShowMessage('Error: no time frame selected');
					StateMachine.mdlChangeStatus := mdlNull;
					Result := False;
					Exit;
				end;
				0: //existing
					if modelIni.ReadString('treeverse','linkexisting','unk') = 'unk' then
					begin
						ShowMessage('Sys Ini file error: linkexisting not specified in treeverse section');
						StateMachine.mdlChangeStatus := mdlNull;
						Result := False;
						Exit;
					end
					else
					begin
						modelIni.WriteString('treeverse','linktblsrc',
              modelIni.ReadString('treeverse','linkexisting','unk'));
						modelIni.WriteString('ModelState','timeframe','EX');
					end;
				1: //future
          if modelIni.ReadString('treeverse','linkfuture','unk') = 'unk' then
          begin
            ShowMessage('Ini file error: linkfuture not specified in treeverse section');
            StateMachine.mdlChangeStatus := mdlnull;
            Result := False;
            Exit;
          end
          else
          begin
            modelIni.WriteString('treeverse','linktblsrc',
              modelIni.ReadString('treeverse','linkfuture','unk'));
            modelIni.WriteString('ModelState','timeframe','FU');
          end;
			end; //case
		end; // if frmTimeFrame

    timeFrame := modelIni.ReadString('ModelState', 'timeframe', '');
    Assert((timeFrame <> ''), 'Timeframe not determined.');

		modelIni.WriteString('treeverse',
			'tracefile',dlgSelectDirectory.SelectedPathName + '\mdltrace.txt');

		modelIni.UpdateFile;
    modelVersion := modelini.ReadString('control','version','0');

		iniBuffer.free;
  end;
end;

procedure TfrmMain.CreateParams(var Params: TCreateParams);
begin
  inherited;
  // for WinXP and above, have Windows manage double-buffered painting
//  if CheckWin32Version(5, 1) then
//    Params.ExStyle := Params.ExStyle or WS_EX_COMPOSITED;
end;

function TfrmMain.DisplayForm(ClassType: TfrmChildClass): TfrmChild;
begin
  HideWindows;
  Result := ClassType.Create(Application, pnlMain);
  Result.Realign;
  Result.Show;
end;

function Tfrmmain.installmodelpatch(ptype : integer) : boolean;
var
	i, mbrc : integer;
	iniroot : string;
	MODmdbdate, SYSmdbdate : Tdatetime;
	strMODmdbdate, strSYSmdbdate : string;
	tempSQLtimestamp : Tsqltimestamp;
	timecomp : Tvaluerelationship;

begin

	iniroot := extractFiledir(frmmain.ModelIni.FileName);

  Case ptype of
    PATCH_mxd:
    begin
    for i := 0 to high(modeldirs) do
      if modeldirs[i] = 'mxd' then
        if not DirectoryExists(iniroot + '\' + modeldirs[i]) then
          if not CreateDir(iniroot + '\' + modeldirs[i]) then
          begin
            showmessage('unable to create mxd directory');
            statemachine.mdlChangeStatus := mdlNull;
            result := false ;
            exit;
          end;

    result :=true;
    exit;
    end;



    //ASSERT: The standard directories are set and the status box is on top
    PATCH_allmdbs:
    begin
      if not strtobool(systemini.readstring('Admin','refreshmdbs','True')) then
      begin
        result := true;
        exit;
      end;

      strSYSmdbdate := systemini.readstring('Admin','sysmdbdate','not found');

      if not TryStrToSQLTimeStamp(strSYSmdbdate, tempSQLtimeStamp) then
      begin
        showmessage('system mdb date not found');
        result := false ;
        exit;
      end
      else
        Sysmdbdate := Strtodatetime(strSYSmdbdate);

      strMODmdbdate := modelini.readstring('Admin','mdbdate','not found');
      if not TryStrToSQLTimeStamp(strMODmdbdate, tempSQLTimeStamp) then
        MODmdbdate := 0
      else
        Modmdbdate := Strtodatetime(strMODmdbdate);

      {
        model > system ; it has been refreshed
        model = systen ; it has been refreshed
        model < system ; need to refresh
      }
      timecomp := CompareDateTime(Modmdbdate, Sysmdbdate);
      if timecomp <> lessthanvalue then
      begin
        result := true;
        exit;
      end;

      if not copymastermdbs() then
      begin
        result := false;
        exit;
      end;


      modelini.WriteString('Admin','mdbdate',DateTimeToStr(Date + Time));
      showmessage('you should redeploy runoff prior to exiting');
      result := true;
      exit;
    end; //PATCH_allmdbs


    PATCH_IC01:
    begin
      if not fileexists(iniroot + '\ic\mdl_ic_Infilt_ac.TAB') or
        not fileexists(iniroot + '\ic\mdl_ic_SWPlnt_ac.TAB') or
        not fileexists(iniroot + '\ic\mdl_ic_NgtoRedir_ac.TAB') then

      begin
		    mbrc := frmStatus.MIRunMenuCmd(frmmain.goleMI, 'Adding inflow control tables',
    			EMG_AddICtables, 'Enter Model', EMG_AppName, strMBmsg , strMBrc);

		    if mbrc <> ReturnSuccess then
		    begin
			    result := false;
			    exit;
		    end;

        modelini.Writestring('Admin','ICPatch',DateTimeToStr(Date + Time));
      end;
      result := true;
      exit;
    end;


    else // unknown ptype
    begin
      showmessage('unreached condition in Tfrmmain.installmodelpatch' + #13 + #13 +
            'ptype = ' + inttostr(ptype));
      result := false;
      exit;
    end

  end; //case ptype

end;

function TfrmMain.OpenExistingModel: Boolean;
var
  systemIniKeys: TStringList;
  i: Integer;
  j: Integer;
  strValue: string;
  timeFrame: string;
  strDateTime: string;
  strMsg: string;
begin
  // If model ini file doesn't exist, warn user
  if not FileExists(dlgSelectDirectory.SelectedPathName + '\' + modelIniFileName) then
  begin
    ShowMessage('No model in this directory');
    statemachine.mdlChangeStatus := mdlNull;
    Result := False;
    Exit;
  end
  else // Model ini file exist
  begin
    modelIni := tmeminifile.Create(dlgSelectDirectory.SelectedPathName + '\'+ modelInifilename);

    // Update certain System IniSections into the Model Ini
    // Sections are declared in unit GlobalConstants
    systemIniKeys := TStringList.Create;
    for i := 0 to high(SystemIniSections) do
    begin
      systemIniKeys.clear;
      systemIni.ReadSection(systemIniSections[i], systemIniKeys);
      for j := 0 to SystemIniKeys.count -1 do
      begin
        if (length(SystemIniKeys[j]) <> 0) and (SystemIniKeys[j][1] <> '#') then
        begin
          strValue := systemini.ReadString(SystemIniSections[i], SystemIniKeys[j], 'not found');
          if strValue = 'not found' then
            ShowMessage( 'SystemIniSections[' + inttostr(i) +']=' + SystemIniSections[i] + #13 +   '  key[' + inttostr(j) +']=' + SystemIniKeys[j])
          else
            modelIni.writestring(SystemIniSections[i],SystemIniKeys[j],strvalue);
        end;
      end;
    end;
    modelIni.WriteString('retrace','linkdbsrc',
      dlgSelectDirectory.SelectedPathName + '\links\mdl_links_ac.mdb');
    modelIni.WriteString('retrace','tracefile',
      dlgSelectDirectory.SelectedPathName + '\retrace.txt');

    strValue :=  systemini.ReadString('treeverse','linkdbsrc','not found');
    if strvalue = 'not found' then
      ShowMessage('WARNING: no linkdbsrc found')
    else
      modelIni.WriteString('treeverse','linkdbsrc',strvalue);

    modelIni.WriteString('treeverse','tracefile',
      dlgSelectDirectory.SelectedPathName + '\mdltrace.txt');


    { the following checks for consistency between timeframe and table source
      because of potential error in existing models}
    timeFrame := modelIni.ReadString('ModelState','timeframe','unk');
    if timeFrame = 'EX' then
    begin
      if modelIni.ReadString('treeverse','linktblsrc','unk') <>
        modelIni.ReadString('treeverse','linkexisting','unk') then
        begin
          { TODO -oAMM -cSupport : Change phone number to MAPI ticket }
          ShowMessage('Warning: linktblsrc inconsistent with timeframe, call 823-7735');
          modelIni.writestring('treeverse','linktblsrc',
            modelIni.ReadString('treeverse','linkexisting','unk'));
        end
    end
    else if timeFrame = 'FU' then
    begin
      if modelIni.ReadString('treeverse','linktblsrc','unk') <>
        modelIni.ReadString('treeverse','linkfuture','unk') then
        begin
          { TODO -oAMM -cSupport : Change phone number to MAPI ticket }
          ShowMessage('Warning: linktblsrc inconsistant with timeframe, call 823-7735');
          modelIni.writestring('treeverse','linktblsrc',
            modelIni.ReadString('treeverse','linkfuture','unk'));
        end
    end
    else if strvalue = 'unk' then
    begin
      ShowMessage('Error: no time frame selected');
      statemachine.mdlChangeStatus := mdlnull;
      Result := False;
      Exit;
    end;
    // end consistency check


    strDateTime := DateToStr(Date) + ' ' + Timetostr(Time);
    modelIni.writestring('Admin','Entered',strDateTime);
    modelIni.UpdateFile;

    systemIniKeys.Free;

    // Check version number
    modelversion := modelIni.ReadString('control','version','0');
    if  modelversion <> emgaatsversion then
      if MidStr(modelversion,1,3) = MidStr(emgaatsversion,1,3) then
      // the model is close but we will disable
      begin
        strMsg := 'Old Model Version';
        ShowMessage(strMsg);
      end
      else
      begin
        strMsg := 'model version # ' +
        modelIni.ReadString('control','version','0') + ' is out of date' + #13 +
          'current version is #' + emgaatsversion + #13 + #13 +
          '1) create new model in new directory' + #13 +
          '2) copy starts and stops from this model';
        ShowMessage(strMsg);
        StateMachine.mdlChangeStatus := mdlNull;
        Result := False;
        Exit;
      end;

  end;
end;

function  TfrmMain.EnterModel(Sender: TObject ; IsNewModel : boolean) : TmdlChangeStatus;
var
  NumMDBSToCopy: Integer;
  MDBSToCopy: TStringList;
  mbrc, i , j: integer;
  inibuffer, SystemIniKeys : Tstringlist;
  EXEDir, iniroot, strDateTime, lastmodel, thismodel, strmsg, strvalue : string;
begin
	if ExitModel() <> mdlNull then
  begin
    result := statemachine.mdlChangeStatus;
    exit;
  end;


  //ASSERT; any previously opened model is now closed
	errHandler.Clear;

  // select the model diectory
	EXEDir := extractfiledir(paramstr(0));
	dlgSelectDirectory.SelectedPathName := UserRegistry.ReadString('LastSession', 'Directory', EXEDir);

	if isNewModel then
		dlgSelectDirectory.Title := 'Select Directory for New Model'
  else
    dlgSelectDirectory.Title := 'Select Model Directory';

  if not dlgSelectDirectory.Execute then
  begin
    result := statemachine.mdlChangeStatus;
    exit;
  end;

  //ASSERT: we have a valid directory

	  // check if we should overwrite an existing model
  if isnewmodel then
  begin
		if FileExists(dlgSelectDirectory.SelectedPathName + '\' + ModelIniFileName) then
    begin
			case MessageDlg('Model exists in this directory.  Overwrite?',
				mtWarning, [mbYes, mbNo], 0) of
				mrYes: // User wants to overwrite
					begin
						MessageDlg('Since you are overwriting an existing model directory, there may '
							+#13+#10+'be some residual files that may be out of date.  Only the .MDB '
							+#13+#10+'and MapInfo files that EMGAATS generates were replaced.',
							mtWarning, [mbOK], 0);
//						showmessage('STUB: need to delete existing stuff here');
					end;
				mrNo: // User doesn't want to overwrite
					begin
            statemachine.mdlChangeStatus := mdlNull;
            result := statemachine.mdlChangeStatus;
		        EXIT; // in a mdlNull State
					end;
			end; //case
		end; // if fileexists

		//create the new model inifile in memory from the system ini file
		modelINI := tmeminifile.Create(dlgSelectDirectory.SelectedPathName + '\'+ modelinifilename);
		inibuffer := Tstringlist.Create;
		systemini.GetStrings(inibuffer);
		modelini.SetStrings(inibuffer);  // this is the booboo
		modelini.WriteString('retrace','linkdbsrc',
			dlgSelectDirectory.SelectedPathName + '\links\mdl_links_ac.mdb');
		modelini.WriteString('retrace','tracefile',
			dlgSelectDirectory.SelectedPathName + '\retrace.txt');

		strDateTime:=DateToStr(Date) + ' ' + Timetostr(Time);
		modelini.Writestring('Admin','Created',strDateTime);

		//Get the time frame for a new model
		frmtimeframe.Showmodal;
		if frmtimeframe.mbrc = mbCancel then
		begin
			statemachine.mdlChangeStatus := mdlnull;
			result := statemachine.mdlChangeStatus;
			exit;
		end
		else
		begin
			case frmtimeframe.rdgTimeFrame.ItemIndex of
				-1: // no time frame selected -- this should be unreached
				begin
					showmessage('Error: no time frame selected');
					statemachine.mdlChangeStatus := mdlnull;
					result := statemachine.mdlChangeStatus;
					exit;
				end;
				0: //existing
					if modelini.ReadString('treeverse','linkexisting','unk') = 'unk' then
					begin
						showmessage('Sys Ini file error: linkexisting not specified in treeverse section');
						statemachine.mdlChangeStatus := mdlnull;
						result := statemachine.mdlChangeStatus;
						exit;
					end
					else
					begin
						modelini.Writestring('treeverse','linktblsrc',modelini.ReadString('treeverse','linkexisting','unk'));
						modelini.Writestring('ModelState','timeframe','EX');
					end;
				1: //future
				if modelini.ReadString('treeverse','linkfuture','unk') = 'unk' then
					begin
						showmessage('Ini file error: linkfuture not specified in treeverse section');
						statemachine.mdlChangeStatus := mdlnull;
						result := statemachine.mdlChangeStatus;
						exit;
					end
					else
					begin
//						showmessage('Note: future base not fully implemented');
						modelini.Writestring('treeverse','linktblsrc',modelini.ReadString('treeverse','linkfuture','unk'));
						modelini.Writestring('ModelState','timeframe','FU');
					end;

// AMM 3.0 3/26/2003 Business about linkexisting and linkfuture no longer apply
//				modelini.WriteString('treeverse', 'linktimeframe',
//					FormatDateTime('YYYYMMDD', frmTimeFrame.edtHydraulicDate.Date));
//				modelini.WriteString('treeverse', 'hydrologytimeframe',
//					LeftStr(frmTimeFrame.cmbHydrologyTimeFrame.Text,2));
			end; //case
		end; // if frmtimeframe

		//ASSERT: we've got a timeframe
		modelini.WriteString('treeverse',
			'tracefile',dlgSelectDirectory.SelectedPathName + '\mdltrace.txt');

		modelini.UpdateFile;
    modelversion := modelini.ReadString('control','version','0');

		inibuffer.free;
  end //if new model
	else // not a new model
	begin
		{ TODO -oDJC -cInitialization : Open Model: Check initialization code }
		// Check for existence of model ini file
		if not FileExists(dlgSelectDirectory.SelectedPathName + '\' + ModelIniFileName) then
		begin
			ShowMessage('No model in this directory');
			statemachine.mdlChangeStatus := mdlNull;
			result := mdlNull;
			exit;
		end
		else
		begin
			modelINI := tmeminifile.Create(dlgSelectDirectory.SelectedPathName + '\'+ modelinifilename);


			// copy certain System IniSections into the Model Ini
			SystemIniKeys := Tstringlist.Create;
      for i := 0 to high(SystemIniSections) do
      begin
        SystemIniKeys.clear;
        systemini.ReadSection(SystemIniSections[i],SystemIniKeys);
        for j := 0 to SystemIniKeys.count -1 do
        begin
          if (length(SystemIniKeys[j]) <> 0) and (SystemIniKeys[j][1] <> '#') then
          begin
            strvalue := systemini.ReadString(SystemIniSections[i],SystemIniKeys[j],'not found');
            if strvalue = 'not found' then
              showmessage( 'SystemIniSections[' + inttostr(i) +']=' + SystemIniSections[i] + #13 +   '  key[' + inttostr(j) +']=' + SystemIniKeys[j])
            else
              modelini.writestring(SystemIniSections[i],SystemIniKeys[j],strvalue);
          end;
        end;
      end;
			modelini.WriteString('retrace','linkdbsrc',
				dlgSelectDirectory.SelectedPathName + '\links\mdl_links_ac.mdb');
			modelini.WriteString('retrace','tracefile',
				dlgSelectDirectory.SelectedPathName + '\retrace.txt');

      strValue :=  systemini.ReadString('treeverse','linkdbsrc','not found');
      if strvalue = 'not found' then
        showmessage('WARNING: no linkdbsrc found')
      else
			  modelini.WriteString('treeverse','linkdbsrc',strvalue);

			modelini.WriteString('treeverse','tracefile',
        dlgSelectDirectory.SelectedPathName + '\mdltrace.txt');


      { the following checks for consistancy between timeframe and table source
        because of potential error in existing models}

      strvalue := modelini.ReadString('ModelState','timeframe','unk');
      if strvalue = 'EX' then
      begin
        if modelini.ReadString('treeverse','linktblsrc','unk') <>
          modelini.ReadString('treeverse','linkexisting','unk') then
          begin
            showmessage('Warning: linktblsrc inconsistant with timeframe, call 823-7735');
            modelini.writestring('treeverse','linktblsrc',
              modelini.ReadString('treeverse','linkexisting','unk'));
          end
      end;

      if strvalue = 'FU' then
      begin
        if modelini.ReadString('treeverse','linktblsrc','unk') <>
          modelini.ReadString('treeverse','linkfuture','unk') then
          begin
            showmessage('Warning: linktblsrc inconsistant with timeframe, call 823-7735');
            modelini.writestring('treeverse','linktblsrc',
              modelini.ReadString('treeverse','linkfuture','unk'));
          end
      end;

      if strvalue = 'unk' then
      begin
				showmessage('Error: no time frame selected');
				statemachine.mdlChangeStatus := mdlnull;
				result := statemachine.mdlChangeStatus;
				exit;
			end;
      // end consistancy check


			strDateTime:=DateToStr(Date) + ' ' + Timetostr(Time);
			modelini.writestring('Admin','Entered',strDateTime);
			modelini.UpdateFile;

      systeminikeys.Free;

      modelversion := modelini.ReadString('control','version','0');
      if  modelversion <> emgaatsversion then

			  if MidStr(modelversion,1,3) = MidStr(emgaatsversion,1,3) then

        // the model is close but we will disable
        begin
          strmsg := 'Old Model Version';
					showmessage(strmsg);
				end
				else
				begin
					strmsg := 'model version # ' +
					modelini.ReadString('control','version','0') + ' is out of date' + #13 +
						'current version is #' + emgaatsversion + #13 + #13 +
						'1) create new model in new directory' + #13 +
						'2) copy starts and stops from this model';
					showmessage(strmsg);
          statemachine.mdlChangeStatus := mdlNull;
          result := mdlNull;
          exit;
        end;

    end;
  end;

  //ASSERT: modelinifilename is located in dlgSelectDirectory.Directory

  // this should be the only place states are initialized
  StateMachine.InitializeStates;

  if modelini.ReadString('masterfiles','links','unk') = 'unk' then
  begin
    showmessage('INI error no masterfile-links' + #13 +'you should abort');
  	statemachine.mdlChangeStatus := mdlNull;
    result := mdlNull;
    exit;
  end;

  if modelini.ReadString('masterfiles','nodes','unk') = 'unk' then
  begin
    showmessage('INI error no masterfile-nodes' + #13 +'you should abort');
		statemachine.mdlChangeStatus := mdlNull;
    result := mdlNull;
    exit;
  end;

  if modelini.ReadString('masterfiles','diversions','unk') = 'unk' then
  begin
    showmessage('INI error no masterfile-diversions' + #13 +'you should abort');
  	statemachine.mdlChangeStatus := mdlNull;
    result := mdlNull;
		exit;
  end;

  if modelini.ReadString('treeverse','tracefile','unk') = 'unk' then
  begin
    showmessage('INI error no treeverse-tracefile' + #13 +'you should abort');
  	statemachine.mdlChangeStatus := mdlNull;
    result := mdlNull;
    exit;
  end;

  iniroot := extractFiledir(frmmain.modelini.FileName);

  // PATCH add mxd to existing model if it is not there
  if not isnewmodel then
    if not installmodelpatch(Patch_mxd) then
    begin
			raise Exception.Create('mxd Patch Failed ' + #13 + #13 + iniroot);
      statemachine.mdlChangeStatus := mdlNull;
      result := mdlnull;
      exit;
    end;

  // setup the standard directories
  if isnewmodel then
    for i := 0 to high(modeldirs) do
    begin
			if not DirectoryExists(iniroot + '\' + modeldirs[i]) then
        if not CreateDir(iniroot + '\' + modeldirs[i]) then
        begin
          statemachine.mdlChangeStatus := mdlNull;
          raise Exception.Create('Cannot create ' + iniroot + '\' + modeldirs[i]);
        	exit;
        end;
     end
  else
    for i := 0 to high(modeldirs) do
      if not DirectoryExists(iniroot + '\' + modeldirs[i]) then
      begin
        if not CreateDir(iniroot + '\' + modeldirs[i]) then
        begin
          showmessage('directory not found in existing model ' + modeldirs[i] );
          statemachine.mdlChangeStatus := mdlNull;
          result := statemachine.mdlChangeStatus ;
					exit;
        end;
      end;



  //ASSERT: The standard directories are set

	//for new model copy standard code
  //for old model check to make sure standard code exists

  if not isnewmodel then
  begin
    if not installmodelpatch(PATCH_allmdbs) then
    begin
      raise Exception.Create('allmdbs Patch Failed ' + #13 + #13 + iniroot);
      statemachine.mdlChangeStatus := mdlNull;
			result := mdlnull;
			exit;
		end;
	end;


	if isnewmodel then
	begin
		if not copymastermdbs() then
		begin
			raise Exception.Create('Unable to copy master mdbs ' + #13 + #13 + iniroot);
			statemachine.mdlChangeStatus := mdlNull;
			result := mdlnull;
			exit;
		end;
	end
	else
	begin
		// AMM 3/21/2006 Changed logic for checking local mdbs to scan unsharped
		// [mdbs] keys
		MDBSToCopy := TStringList.Create;
		ModelIni.ReadSection(MDBSSectionString, MDBSToCopy);
		NumMDBSToCopy := MDBSToCopy.Count;
		for i := 0 to NumMDBSToCopy - 1 do    // Iterate
		begin
			if (LeftStr(MDBSToCopy[i],1) = '#') or (Length(MDBSToCopy[i]) = 0) then
				Continue;

			if (not FileExists(iniroot + MDBSDirPath + MDBSToCopy[i] + '.mdb')) then
				if (IsRequiredMDB(MDBSToCopy[i])) then
				begin
					errHandler.Add('Model MDB doesn''t exist: ' + MDBSToCopy[i] + '.mdb',
						etCritical);
					statemachine.mdlChangeStatus := mdlNull;
					result := statemachine.mdlChangeStatus ;
				end
				else
				begin // comment it out if it wasn't found
					ModelIni.WriteString(MDBSSectionString, '#' + MDBSToCopy[i],
						ModelIni.ReadString(MDBSSectionString, MDBSToCopy[i], ''));
					ModelIni.DeleteKey(MDBSSectionString, MDBSToCopy[i]);
				end;
		end;    // for
		MDBSToCopy.Free;
		errHandler.HandleErrors;
		if stateMachine.mdlChangeStatus = mdlNull then
		begin
			ShowMessage('A required Model MDB is missing.  Please check model.ini, '+
				'in the [mdbs] section, for the required Model MDBs that should be stored '+
				'in \mdbs.');
			Exit;
		end;

{
		for i := 0 to high(mdbkeys) do
			if not fileexists(iniroot + '\mdbs\' + mdbkeys[i] + '.mdb' ) then
			begin
				showmessage('mdb key file not found: ' + mdbkeys[i]);
				statemachine.mdlChangeStatus := mdlNull;
				result := statemachine.mdlChangeStatus ;
				exit;
			end; //for}

	end; // if is new model


	// Let the mapbasic apps know where they are located
	goleMI.mbapplications.item('EMGWorkbench').mbglobals.item('gmdlRootFolder').value := iniroot;
	goleMI.mbapplications.item('EMGWorkbench').mbglobals.item('gstrTimeFrame').value := modelini.ReadString('ModelState','TimeFrame','');


  //showMIglobals(goleMI,'EMGWorkbench');
  //deletes and recreate all model tables
  if isnewmodel then
  begin

		mbrc := frmStatus.MIRunMenuCmd(frmmain.goleMI, 'Creating New Model Tables',
    			EMG_CreateModelTables, 'Enter Model', EMG_AppName, strMBmsg , strMBrc);
		if mbrc <> ReturnSuccess then
		begin
			statemachine.mdlChangeStatus := mdlNull;
			result := statemachine.mdlChangeStatus ;
			exit;
		end;
	end;


  // check for the inflow control tables and create them if they do not exist
  if not isnewmodel then
    if not installmodelpatch(Patch_IC01) then
    begin
      statemachine.mdlChangeStatus := mdlNull;
			result := statemachine.mdlChangeStatus ;
			exit;
    end;


	//opens master and model

  mbrc := frmStatus.MIRunMenuCmd(frmmain.goleMI, 'Opening Model and Master Tables',
	EMG_OpenTables, 'Enter Model', EMG_AppName, strMBmsg , strMBrc);
	if mbrc <> ReturnSuccess then
	begin
		statemachine.mdlChangeStatus := mdlNull;
		result := statemachine.mdlChangeStatus ;
		exit;
	end;

	//the the user know if the model location has changed
	Screen.Cursor := crHourGlass;
	thismodel := getmodelname(pchar(iniroot));

	// TODO: Provide solution with error message, recover
	if thismodel = '' then
		showmessage ('you have a problem with your model name');

	lastmodel := modelini.readstring('Admin','ModelName','unknown');
	if lastmodel = 'unknown' then
		modelini.Writestring('Admin','ModelName',thismodel)
	else
		if lastmodel <> thismodel then
		begin
			modelini.Writestring('Admin','ModelName',thismodel);
			showmessage(thismodel + ' last opened as ' + lastmodel);
		end;

	// ATTEMPT THE PATCH
	if modelini.ReadString('Control','VersionPatch','notfound') = 'INPROCESS' then
	begin
    showmessage('patch disabled');
  {

		mbrc := frmStatus.MIRunMenuCmd(frmmain.goleMI, True , 'Attempting Patch',
			HYD_Patch, HYD_AppName, strMBmsg , strMBrc);
		if mbrc <> ReturnSuccess then
		begin
			statemachine.mdlChangeStatus := mdlNull;
			result := statemachine.mdlChangeStatus ;
			exit;
		end
		else
		begin
			modelini.WriteString('Control','VersionPatch','Patched');
			modelini.WriteString('Control','version',emgaatsversion);
			showmessage('patch complete');
		end;
  }
	end;

  //relink mdbs in all cases because it is easy
  frmstatus.StatusBox('Relinking Access Databases','','EnterModel');
  frmBuildModels.actrelinkmdbsExecute(Sender);
  frmstatus.Hide;
  Screen.Cursor := crDefault;

//	actBuildModelsExecute(Sender);

  //codesite.DestinationDetails := iniroot + '\EMGAATSLOG.txt';

  statemachine.mdlChangeStatus := mdlunchanged;
  result := statemachine.mdlChangeStatus ;

end; // enter model



procedure TfrmMain.actNewModelExecute(Sender: TObject);
begin
	{ TODO -oDJC -cModel Functionality : New Model: Add initialization code }
  {ASSERT: the directory is clean even if a existing file was there}

	Screen.Cursor := crHourGlass;
	try
		if EnterModel(sender, True) = mdlunchanged then
		begin
			Screen.Cursor := crDefault;
			actBuildModelsExecute(Sender);
		end
		else
			statemachine.mdlChangeStatus := mdlnull;
	finally
		Screen.Cursor := crDefault;
	end;

end;

procedure TfrmMain.actOpenModelExecute(Sender: TObject);
var
  myrc : integer;
begin
  Screen.Cursor := crHourGlass;
  if entermodel(sender, False) = mdlunchanged then
  begin
    //showMIglobals(frmmain.golemi, 'EMGworkbench');

    myrc := mapinfodo(frmmain.golemi,'commit table mdl_SpecLinkData');

    if myrc = ReturnSuccess then
      myrc := mapinfodo(frmmain.golemi,'pack table mdl_SpecLinkData data')
    else
    begin
      showmessage('failure to pack mdl_SpecLinkData, call 823-7735');
      statemachine.mdlChangeStatus := mdlnull;
      Screen.Cursor := crDefault;
      exit;
    end;

    myrc := mapinfodo(frmmain.golemi,'commit table mdl_SurfSC ');

    if myrc = ReturnSuccess then
      myrc := mapinfodo(frmmain.golemi,'pack table mdl_SurfSC Graphic Data')
    else
    begin
      showmessage('failure to pack mdl_SurfSC, call 823-7735');
      statemachine.mdlChangeStatus := mdlnull;
      Screen.Cursor := crDefault;
      exit;
    end;


    actBuildModelsExecute(Sender);

  end
  else
  begin
    statemachine.mdlChangeStatus := mdlnull;
  end;

  Screen.Cursor := crDefault;
end;



procedure TfrmMain.FinalizeGlobalObjects;
begin
  FreeAndNil(EMGWorkbenchManager);
  FreeAndNil(MSAccessManager);
  FreeAndNil(ModelTemplateConfig);
end;

procedure TfrmMain.FormClose(Sender: TObject; var Action: TCloseAction);
var
  strdatetime : string;
begin

  if modelini <> nil then
  begin
    if statemachine.mdlChangeStatus <> mdlNull then
    begin
      strDateTime:=DateToStr(Date) + ' ' + Timetostr(Time);
      modelini.Writestring('Admin','Modified',strDateTime);
      modelini.UpdateFile;
    end;

    //this should close all tables so no need to run one for HYDworkbench
 //   if mapinfodo(frmmain.goleMI,'Run Menu Command ID 5005') <> -1 then
 //     showmessage('Application Terminate Failed');
  end;


  goleCB := unassigned;
  goleMI := Unassigned;
  golemitest := unassigned;
  application.ProcessMessages;
  sleep(1000);


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

procedure TfrmMain.actToolsInterfaceMasterExecute(Sender: TObject);
begin
	appLauncher.FileName := '\\CASSIO\Modeling\Model_Programs\InterfaceMaster.EXE';
	appLauncher.Launch;
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

procedure TfrmMain.InitMapInfo;
var
	DoStr: String;
  MIWorkbenchAppName: string;
begin
  try
//    paneStatus.Caption := 'Starting MapInfo in the background...';
    try
      goleMI := createoleobject('Mapinfo.Application');
    except
      ShowMessage('EMGAATS could not create a MapInfo instance. Please repair or '+
        'install MapInfo 7.0');
    end;

    EMGInitialized := False;
    MIWorkbenchAppName := systemIni.ReadString('code','MIworkbench','notfound');
    if DoStr = 'notfound' then showmessage('Mapbasic application MIWorkbench not found')
    else
    begin
      DoStr := 'Run Application '+ #34 + MIWorkbenchAppName + #34;
      if mapinfodo(frmmain.goleMI , DoStr) = ReturnSuccess then
        EMGInitialized := True
      else
      begin
        ShowMessage('EMGWorkbench failed.'#13'Close any MAPINFOW.EXE processes running EMGWorkbench, '+
          'then click Tools, Restart Workbenches');
        { TODO -oAMM -cMapInfo : Search through all MapInfo processes (MAPINFOW.EXE) and close them }
      end;
    end;
  finally
//    paneStatus.Caption := 'Ready';
  end;
end;

procedure TfrmMain.actRestartWorkbenchesUpdate(Sender: TObject);
begin
	actRestartWorkbenches.Enabled :=  (not EMGInitialized);
end;

procedure TfrmMain.actRestartWorkbenchesExecute(Sender: TObject);
begin
  InitMapInfo;
end;

procedure TfrmMain.actBuildModelPipeSystemExecute(Sender: TObject);
begin
	HideWindows;
	frmBuildPipeSystem.Show;
end;

procedure TfrmMain.actBuildModelLateralsAndParcelsExecute(Sender: TObject);
begin
	HideWindows;
	frmBuildLateralsAndParcels.Show;
end;

procedure TfrmMain.actBuildModelSubcatchmentsExecute(Sender: TObject);
begin
	HideWindows;
	frmBuildSubcatchments.Show;
end;

procedure TfrmMain.actBuildModelDeployModelExecute(Sender: TObject);
begin
	HideWindows;
	frmDeployModel.Show;
end;

procedure TfrmMain.actBuildModelPerformQualityControlExecute(
  Sender: TObject);
begin
	HideWindows;
	frmPerformQualityControl.Show;
end;

procedure TfrmMain.actBuildModelPipeSystemUpdate(Sender: TObject);
begin
	actBuildModelPipeSystem.Enabled := statemachine.mdlChangeStatus <> mdlNull;
end;

procedure TfrmMain.actBuildModelLateralsAndParcelsUpdate(Sender: TObject);
begin
	actBuildModelLateralsAndParcels.Enabled := statemachine.mdlChangeStatus <> mdlNull;
end;

procedure TfrmMain.actBuildModelSubcatchmentsUpdate(Sender: TObject);
begin
	actBuildModelSubcatchments.Enabled := statemachine.mdlChangeStatus <> mdlNull;
end;

procedure TfrmMain.actBuildModelDeployModelUpdate(Sender: TObject);
begin
	actBuildModelDeployModel.Enabled := statemachine.mdlChangeStatus <> mdlNull;
end;

procedure TfrmMain.actBuildModelPerformQualityControlUpdate(
	Sender: TObject);
begin
	actBuildModelPerformQualityControl.Enabled := statemachine.mdlChangeStatus <> mdlNull;
end;




end.
