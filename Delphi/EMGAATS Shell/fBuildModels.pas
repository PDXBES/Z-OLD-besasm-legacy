{==fBuildModels Unit============================================================

	Form for deploying models to a specific modeling engine

	Revision History
	2.11   07/13/2006 (AMM) added code to auto-run project area and mask creation\
										in build-all
					12/3/2003 (DJC) added the deploy pdx runoff to build and deploy
         05/06/2003 (DJC)  baseflow patch
         04/03/2003 (DJC)  Get rid of hydworkbench
         03/27/2003 (DJC) Added relink for IC in HYDinitDSC.mdb
	2.1    03/06/2003 (AMM) Added one-stop Build & Deploy model (unattended)
													Added mst_speclinks_ac to relink method (actrelinkmdbsExecute)
	1.5    ?          ?
	1.0    11/01/2002 Initial release

===============================================================================}

unit fBuildModels;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, fLabeledChild, StdCtrls, ActnList, comobj, utilmsaccess,
  RzButton, globalconstants,
  idglobal, RzLaunch, RzLabel, pngimage, RzBckgnd, ExtCtrls, RzPanel;

type
  TfrmBuildModels = class(TfrmLabeledChild)
    chkBuildPipeSystem: TCheckBox;
    chkBuildLateralsAndParcels: TCheckBox;
    chkBuildSubcatchments: TCheckBox;
    btnBuildPipeSystem: TButton;
    btnBuildLateralsAndParcels: TButton;
		btnBuildSubcatchments: TButton;
    ActionList1: TActionList;
    actModifyModelBoundaries_old: TAction;
    actBuildPipeSystem: TAction;
    actBuildLateralsAndParcels: TAction;
    actBuildSubcatchments: TAction;
    actDeployModelToEngine: TAction;
    btnDeployModelToEngine: TButton;
    btnModelResults: TButton;
    actModelResutls: TAction;
    actrelinkmdbs: TAction;
    mdlrelinkmdbs: TButton;
    btnPerformQualityControl: TButton;
    actPerformQualityControl: TAction;
    RzButton1: TRzButton;
    actBuildAndDeploy: TAction;
    Button1: TButton;
    btnConvertModelToArcGIS: TRzButton;
    RzLauncher1: TRzLauncher;
    procedure actBuildPipeSystemUpdate(Sender: TObject);
    procedure actBuildLateralsAndParcelsUpdate(Sender: TObject);
    procedure actBuildSubcatchmentsUpdate(Sender: TObject);
    procedure actModifyModelBoundaries_oldExecute(Sender: TObject);
    procedure actBuildPipeSystemExecute(Sender: TObject);
    procedure actBuildLateralsAndParcelsExecute(Sender: TObject);
    procedure actBuildSubcatchmentsExecute(Sender: TObject);
		procedure actDeployModelToEngineUpdate(Sender: TObject);
    procedure actDeployModelToEngineExecute(Sender: TObject);
    procedure actModelResutlsExecute(Sender: TObject);
    procedure actrelinkmdbsExecute(Sender: TObject);
    procedure relinkmdbs(Sender: TObject);
    procedure actBuildAndDeployUpdate(Sender: TObject);
    procedure actPerformQualityControlExecute(Sender: TObject);
    procedure actBuildAndDeployExecute(Sender: TObject);
		procedure Button1Click(Sender: TObject);
		procedure runpatchcode(Sender: TObject);
		procedure btnConvertModelToArcGISClick(Sender: TObject);

	private
		{ Private declarations }
	public
		{ Public declarations }
	end;

var
	frmBuildModels: TfrmBuildModels;

implementation

uses fMain, dmStateMachine, fBuildPipeSystem, fModifyModelBoundaries,
  fBuildSubcatchments, fBuildLateralsAndParcels, fDeployModelToEngine,
  fModelResults, fChild, fPerformQualityControl, fStatus, StSystem,
  uMSAccessManager, uEMGAATSSystemConfig, StStrL;

{$R *.dfm}



procedure TfrmBuildModels.actBuildPipeSystemUpdate(Sender: TObject);
begin
  inherited;
  //actBuildPipeSystem.Enabled := StateMachine.HasBoundaries;
  actBuildPipeSystem.Enabled := (frmmain.modelversion = emgaatsversion);

end;

procedure TfrmBuildModels.actBuildLateralsAndParcelsUpdate(
  Sender: TObject);
begin
  inherited;
  actBuildLateralsAndParcels.Enabled := StateMachine.HasPipeSystem and (frmmain.modelversion = emgaatsversion);
end;

procedure TfrmBuildModels.actBuildSubcatchmentsUpdate(Sender: TObject);
begin
  inherited;
  actBuildSubcatchments.Enabled := StateMachine.HasPipeSystem and (frmmain.modelversion = emgaatsversion);
end;

procedure TfrmBuildModels.actModifyModelBoundaries_oldExecute(Sender: TObject);
begin
  inherited;
  frmMain.HideWindows;
//  if not Assigned(frmModifyModelBoundaries) then
//    frmModifyModelBoundaries := TfrmModifyModelBoundaries.Create(Application, frmMain.pnlMain);
  frmModifyModelBoundaries.Show;
end;

procedure TfrmBuildModels.actBuildPipeSystemExecute(Sender: TObject);

begin
  inherited;
	frmMain.HideWindows;
//  if not Assigned(frmBuildPipeSystem) then
//    frmBuildPipeSystem := TfrmBuildPipeSystem.Create(Application, frmMain.pnlMain);

	frmBuildPipeSystem.Show;
end;

procedure TfrmBuildModels.actBuildLateralsAndParcelsExecute(
  Sender: TObject);
begin
  inherited;
  frmMain.HideWindows;
//  if not Assigned(frmBuildLateralsAndParcels) then
//    frmBuildLateralsAndParcels := TfrmBuildLateralsAndParcels.Create(Application, frmMain.pnlMain);
  frmBuildLateralsAndParcels.Show;
end;

procedure TfrmBuildModels.actBuildSubcatchmentsExecute(Sender: TObject);
begin
  inherited;
  frmMain.HideWindows;
//  if not Assigned(frmBuildSubcatchments) then
//    frmBuildSubcatchments := TfrmBuildSubcatchments.Create(Application, frmMain.pnlMain);
  frmBuildSubcatchments.Show;
end;

procedure TfrmBuildModels.actDeployModelToEngineUpdate(Sender: TObject);
begin
	inherited;
	actDeployModelToEngine.Enabled := StateMachine.HasPipeSystem and (frmmain.modelversion = emgaatsversion);
end;

procedure TfrmBuildModels.actBuildAndDeployUpdate(Sender: TObject);
begin
	inherited;
  frmModifyModelBoundaries.FormInitialize();
	actBuildAndDeploy.Enabled := (frmmain.modelversion = emgaatsversion) and Statemachine.HasBoundaries;
end;


procedure TfrmBuildModels.actDeployModelToEngineExecute(Sender: TObject);
begin
  inherited;
	{ DONE -oAMM -cModel Functionality : Add model file construction code (for specific modeling engines) }
	if not Assigned(frmDeployModel) then
		frmDeployModel := TfrmDeployModel.Create(Application, frmMain.pnlMain);
	frmDeployModel.Show;
end;

procedure TfrmBuildModels.actModelResutlsExecute(Sender: TObject);
begin
  inherited;
  if not Assigned(frmModelResults) then
		frmModelResults := TfrmModelResults.Create(Application, frmMain.pnlMain);
	frmModelResults.Show;

end;

procedure TfrmBuildModels.actrelinkmdbsExecute(Sender: TObject);
begin
  inherited;

  Screen.Cursor := crHourGlass;
  relinkmdbs(sender);
  Screen.Cursor := crdefault;

end;

procedure TfrmBuildModels.relinkmdbs(Sender: TObject);
var
  myoleac : variant;
  projdir : string;
begin
  inherited;

  Try
    projdir :=  extractFiledir(frmmain.modelini.FileName);
    myoleac := createoleobject('Access.Application');

    //frmmain.CSGlobalObject1.Dispatch(projdir);

    myoleac.OpenCurrentDatabase(projdir + '\mdbs\ModelAssemble.mdb');
    if actblrelink(myoleac,'mdl_nodes_ac','mdl_nodes_ac', projdir + '\nodes\mdl_nodes_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_links_ac','mdl_links_ac', projdir + '\links\mdl_links_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_speclinkdata_ac','mdl_speclinkdata_ac', projdir + '\links\mdl_speclinkdata_ac.mdb',TRUE) <> -1 then exit;
		if actblrelink(myoleac,'mdl_speclinks_ac','mdl_speclinks_ac', projdir + '\links\mdl_speclinks_ac.mdb',TRUE) <> -1 then exit;
		if actblrelink(myoleac,'mst_speclinkdata_ac','mst_speclinkdata_ac',frmmain.ModelIni.ReadString('masterfiles','root','\no_root') + frmmain.ModelIni.ReadString('masterfiles','speclinkdata','\no_speclinkdata') + '.mdb', TRUE) <> -1 then exit;
		if actblrelink(myoleac,'mst_speclinks_ac','mst_speclinks_ac',frmmain.ModelIni.ReadString('masterfiles','root','\no_root') + frmmain.ModelIni.ReadString('masterfiles','speclinks','\no_speclinks') + '.mdb', TRUE) <> -1 then exit;
		myoleac.closecurrentdatabase;

		myoleac.OpenCurrentDatabase(projdir + '\mdbs\linkQAQC.mdb');
    if actblrelink(myoleac,'mdl_nodes_ac','mdl_nodes_ac', projdir + '\nodes\mdl_nodes_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_links_ac','mdl_links_ac', projdir + '\links\mdl_links_ac.mdb',TRUE) <> -1 then exit;
    myoleac.closecurrentdatabase;

    //relink library then relink tables
    myoleac.OpenCurrentDatabase(projdir + '\mdbs\HYDInitDsc.mdb');
    myoleac.run('ReferenceFromFile',projdir +'\mdbs\emgaatscode.mdb','EMGAATScode');
    if actblrelink(myoleac,'mdl_DirSC_ac','mdl_DirSC_ac', projdir + '\dsc\mdl_DirSC_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_ic_discoveg_ac','mdl_ic_discoveg_ac', projdir + '\ic\mdl_ic_discoveg_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_ic_drywell_ac','mdl_ic_drywell_ac', projdir + '\ic\mdl_ic_drywell_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_ic_GrnRoof_ac','mdl_ic_GrnRoof_ac', projdir + '\ic\mdl_ic_GrnRoof_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_ic_store_ac','mdl_ic_store_ac', projdir + '\ic\mdl_ic_store_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_ic_Infilt_ac','mdl_ic_Infilt_ac', projdir + '\ic\mdl_ic_Infilt_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_ic_SWplnt_ac','mdl_ic_SWPlnt_ac', projdir + '\ic\mdl_ic_SWPlnt_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_ic_NgtoRedir_ac','mdl_ic_NgtoRedir_ac', projdir + '\ic\mdl_ic_NgtoRedir_ac.mdb',TRUE) <> -1 then exit;

    // relinks assume standard location of files with respect to root -- ini file is consulted ONLY for root location
		if actblrelink(myoleac,'mst_ic_discoveg_ac','mst_ic_discoveg_ac',frmmain.ModelIni.ReadString('masterfiles','root','\no_root') + '\ic\mst_ic_discoveg_ac.mdb', TRUE) <> -1 then exit;
		if actblrelink(myoleac,'mst_ic_drywell_ac','mst_ic_drywell_ac',frmmain.ModelIni.ReadString('masterfiles','root','\no_root') + '\ic\mst_ic_drywell_ac.mdb', TRUE) <> -1 then exit;
		if actblrelink(myoleac,'mst_ic_GrnRoof_ac','mst_ic_GrnRoof_ac',frmmain.ModelIni.ReadString('masterfiles','root','\no_root') + '\ic\mst_ic_GrnRoof_ac.mdb', TRUE) <> -1 then exit;
		if actblrelink(myoleac,'mst_ic_Store_ac','mst_ic_Store_ac',frmmain.ModelIni.ReadString('masterfiles','root','\no_root') + '\ic\mst_ic_store_ac.mdb', TRUE) <> -1 then exit;
    myoleac.Closecurrentdatabase;

    //relink library then relink tables
    myoleac.OpenCurrentDatabase(projdir + '\mdbs\ModelDeployHydraulics.mdb');
    myoleac.run('ReferenceFromFile',projdir +'\mdbs\emgaatscode.mdb','EMGAATScode');
    if actblrelink(myoleac,'_PipeShapes','PipeShapes', projdir + '\mdbs\lookuptables.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'_SanPattern','SanPattern', projdir + '\mdbs\lookuptables.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'_UserDefinedGeometry','UserDefinedGeometry', projdir + '\mdbs\lookuptables.mdb',TRUE) <> -1 then exit;

    //optional
		actblrelink(myoleac,'_PumpCurves','PumpCurves', projdir + '\mdbs\lookuptables.mdb',False);
    actblrelink(myoleac,'_TagDefinition','TagDefinition', projdir + '\mdbs\lookuptables.mdb',False);

    if actblrelink(myoleac,'mdl_DirSC_ac','mdl_DirSC_ac', projdir + '\dsc\mdl_DirSC_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_nodes_ac','mdl_nodes_ac', projdir + '\nodes\mdl_nodes_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_links_ac','mdl_links_ac', projdir + '\links\mdl_links_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_speclinkdata_ac','mdl_speclinkdata_ac', projdir + '\links\mdl_speclinkdata_ac.mdb',TRUE) <> -1 then exit;
		if actblrelink(myoleac,'mdl_speclinks_ac','mdl_speclinks_ac', projdir + '\links\mdl_speclinks_ac.mdb',TRUE) <> -1 then exit;

    myoleac.closecurrentdatabase;

    //relink library then relink tables
    myoleac.OpenCurrentDatabase(projdir + '\mdbs\ModelDeployHydrology.mdb');
    myoleac.run('ReferenceFromFile',projdir +'\mdbs\emgaatscode.mdb','EMGAATScode');
    if actblrelink(myoleac,'mdl_DirSC_ac','mdl_DirSC_ac', projdir + '\dsc\mdl_DirSC_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_links_ac','mdl_links_ac', projdir + '\links\mdl_links_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_nodes_ac','mdl_nodes_ac', projdir + '\nodes\mdl_nodes_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_SurfSC_ac','mdl_SurfSC_ac', projdir + '\surfsc\mdl_SurfSC_ac.mdb',TRUE) <> -1 then exit;

    // relink the inflow control tables
    if actblrelink(myoleac,'mdl_ic_discoveg_ac','mdl_ic_discoveg_ac', projdir + '\ic\mdl_ic_discoveg_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_ic_drywell_ac','mdl_ic_drywell_ac', projdir + '\ic\mdl_ic_drywell_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_ic_GrnRoof_ac','mdl_ic_GrnRoof_ac', projdir + '\ic\mdl_ic_GrnRoof_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_ic_store_ac','mdl_ic_store_ac', projdir + '\ic\mdl_ic_store_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_ic_Infilt_ac','mdl_ic_Infilt_ac', projdir + '\ic\mdl_ic_Infilt_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_ic_SWPlnt_ac','mdl_ic_SWPlnt_ac', projdir + '\ic\mdl_ic_SWPlnt_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_ic_NgtoRedir_ac','mdl_ic_NgtoRedir_ac', projdir + '\ic\mdl_ic_NgtoRedir_ac.mdb',TRUE) <> -1 then exit;

    myoleac.Closecurrentdatabase;

    myoleac.OpenCurrentDatabase(projdir + '\mdbs\ModelResults.mdb');
    if actblrelink(myoleac,'mdl_DirSC_ac','mdl_DirSC_ac', projdir + '\dsc\mdl_DirSC_ac.mdb',TRUE)<> -1 then exit;
    if actblrelink(myoleac,'mdl_links_ac','mdl_links_ac',projdir + '\links\mdl_links_ac.mdb',TRUE)<> -1 then exit;
    if actblrelink(myoleac,'mdl_Laterals_ac','mdl_Laterals_ac',projdir +'\laterals\mdl_Laterals_ac.mdb',TRUE)<> -1 then exit;
    if actblrelink(myoleac,'mdl_LinkRes_ac','mdl_LinkRes_ac',projdir +'\links\mdl_LinkRes_ac.mdb',TRUE)<> -1 then exit;
    if actblrelink(myoleac,'mdl_NodeRes_ac','mdl_NodeRes_ac',projdir +'\nodes\mdl_NodeRes_ac.mdb',TRUE)<> -1 then exit;
    myoleac.Closecurrentdatabase;



  except

    on E: eoleexception do
    begin
      showmessage('An oleexception occured' + #13 + E.Message );
      myoleac := unassigned;
      showmessage('relink failed');
      exit;
    end;
    else
    begin
      myoleac := unassigned;
      raise Exception.create('relink failed');
      exit;
    end;

  end;


end;

procedure TfrmBuildModels.actPerformQualityControlExecute(Sender: TObject);
begin
	inherited;
	frmMain.HideWindows;
  frmPerformQualityControl.Show;
end;

procedure TfrmBuildModels.actBuildAndDeployExecute(Sender: TObject);
begin
  inherited;
  frmmain.modelini.EraseSection(strBuildLogSection);
  frmmain.modelini.Writestring(strBuildLogSection,'Begin',DateTimeToStr(Now));
  frmmain.modelini.UpdateFile;

  //trace the pipe system
  frmBuildPipeSystem.actTracePipeSystemExecute(Sender);

  {the trace pipe system code has a dialog that allows the user to back out
        if the user decides to back out the haspipesytem will be false
  }
  if not stateMachine.HasPipeSystem then exit;

  //run QC pipe checks
  Screen.Cursor := crHourGlass;
  frmBuildPipeSystem.PipeSystemQueries(sender, False);
  Screen.Cursor := crDefault;


  //get the parcels
  frmBuildLateralsAndParcels.actTraceLateralsAndParcelsExecute(Sender);

  //trace the subcatchcments
  frmBuildSubcatchments.actTraceSubcatchmentsExecute(Sender);

	// create the project area and project canopy mask layers
	frmBuildSubcatchments.actCreateProjectAreaExecute(Sender);

  //create the zingers
	frmBuildSubcatchments.actCreateMapLinkingSubcatchmentsToNodesExecute(Sender);

	//prepare the hydrologic data
	frmDeployModel.actPrepHydrologicDataExecute(Sender);

	//prepare the pdx runoff deck in default design mode
	Screen.Cursor := crHourGlass;
	frmDeployModel.DeployPDXRunoff(Sender, False);
	Screen.Cursor := crDefault;


end;

procedure TfrmBuildModels.Button1Click(Sender: TObject);
begin
  inherited;
  if frmmain.modelini.readstring('Patches','BFPatch20030506','') <> '' then
  begin
    showmessage('Patch already run');
    exit;
  end;


  Screen.Cursor := crHourGlass;
  frmstatus.StatusBox('','','Running Patch');

  RunPatchCode(sender);

  frmstatus.Hide;
  Screen.Cursor := crDefault;

  actrelinkmdbsExecute(Sender);


end;

procedure TfrmBuildModels.runpatchcode(Sender: TObject);
var
  myoleac : variant;
  projdir, targetpath, sourcepath, xpxname, mytime : string;
  xrc : integer;
begin
  inherited;

  projdir :=  extractFiledir(frmmain.modelini.FileName);
  targetpath:= projdir + '\mdbs\bfpatch20030506.mdb';
  sourcepath := '\\oberon\modeling\model_programs\emgaats\binv2.1\mdbs\bfpatch20030506.mdb';
  xpxname := projdir +  '\sim\baseflowpatch.xpx';

  mytime := uppercase(frmmain.ModelIni.ReadString('ModelState', 'timeframe', 'NOT FOUND'));

	deletefile(targetpath);
  if CopyFile(sourcepath,targetpath) > 0 then
  begin
    Showmessage('could not copy file: ' + sourcepath);
    exit;
  end;

  targetpath:= projdir + '\mdbs\hydinitdsc.mdb';
  sourcepath := '\\oberon\modeling\model_programs\emgaats\binv2.1\mdbs\hydinitdsc.mdb';

  deletefile(targetpath);
  if CopyFile(sourcepath,targetpath) > 0 then
  begin
    Showmessage('could not copy file: ' + sourcepath);
    exit;
  end;

  Try
    myoleac := createoleobject('Access.Application');
    myoleac.OpenCurrentDatabase(projdir + '\mdbs\bfpatch20030506.mdb');
    myoleac.run('ReferenceFromFile',projdir +'\mdbs\emgaatscode.mdb','EMGAATScode');
    if actblrelink(myoleac,'mdl_DirSC_ac','mdl_DirSC_ac', projdir + '\dsc\mdl_DirSC_ac.mdb',TRUE) <> -1 then exit;
    if actblrelink(myoleac,'mdl_nodes_ac','mdl_nodes_ac', projdir + '\nodes\mdl_nodes_ac.mdb',TRUE) <> -1 then exit;

    //Execute initialization queries
		xrc := myoleac.run('executequerytable','ListExecuteInitDSCPatch', 'Block' , 'EX' , 0);
		if xrc >=0 then
		begin
      showmessage('executequerytable error, block EX, return code: ' + inttostr(xrc));
      myoleac := unassigned;
      exit;
    end;

    if mytime = 'FU' then
    begin
      //set the future base base flow
      frmstatus.StatusBox('setting future baseflow','','baseflow patch');
     	myoleac.run('setFBfactoredbaseflow',0.5);
		end;

    // - 1 is a successful return
    xrc := myoleac.run('ExportQueryTable',xpxname,'XPExportQueryTable','Class','ALL',0);
		if xrc >=0 then
		begin
      showmessage('ExportQueryTable error, return code: ' + inttostr(xrc));
      myoleac := unassigned;
      exit;
    end;

  except

		on E: eoleexception do
		begin
			showmessage('An oleexception occured' + #13 + E.Message );
			myoleac := unassigned;
			showmessage('patch failed');
			exit;
		end;
		else
		begin
			myoleac := unassigned;
			raise Exception.create('patch failed');
			exit;
		end;

	end;

	myoleac :=unassigned;
	frmmain.modelini.WriteString('Patches','BFPatch20030506',DateTimeToStr(Now));
	frmmain.modelini.UpdateFile;
	Showmessage(xpxname + ' created' + #13 + 'import to xp model');

end;


procedure TfrmBuildModels.btnConvertModelToArcGISClick(Sender: TObject);
var
	TranslateResult: Integer;
	FilePath: String;
	Translator: Variant;
	TranslatorIsUnregistered: Boolean;
begin
	inherited;

	try
		Translator := CreateOLEObject('translator.GISTranslator');
	except
		on e: EOleSysError do
		begin
			ShowMessage('Either the .NET Framework 1.1 has not been installed or the '+
				'translator.GISTranslator object has not been registered.  EMGAATS '+
				'cannot create the translator.GISTranslator object.');
		end;
	end;

	if not VarIsEmpty(Translator) then
	begin
		FilePath := ExtractFilePath(frmMain.ModelIni.FileName);
		TranslateResult := Translator.TranslateModel(FilePath);
		Translator := Unassigned;
	end;

{	if TranslatorIsUnregistered then
	begin
		RZLauncher1.FileName := 'RegAsm.exe';
		RZLauncher1.StartDir := 'W:\Model_Programs\GISTranslator\Translator\bin';
		RZLauncher1.Parameters := '/u translator.dll';
		RZLauncher1.WaitUntilFinished := True;
		RZLauncher1.Launch;
	end;}

	ShowMessage(Format('TranslateModel Result = %d', [TranslateResult]));
end;

end.
