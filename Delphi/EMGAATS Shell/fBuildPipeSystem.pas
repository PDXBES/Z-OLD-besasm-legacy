{==fBuildPipeSystem Unit========================================================

	Form for tracing and building pipe system for model

	Revision History
	2.1    03/06/2003 (AMM) Revised calls to MapInfo to use constants in uWorkbenchDefs
	1.5    ?          ?
	1.0    11/01/2002 Initial release

===============================================================================}

unit fBuildPipeSystem;

interface

uses
	Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, fLabeledChild, StdCtrls, ActnList,
	GlobalConstants, comobj, mapinfo_TLB, utilmsaccess, IdGlobal, CodeSiteLogging,
  RzLabel, pngimage, RzBckgnd, ExtCtrls, RzPanel;

type
  TfrmBuildPipeSystem = class(TfrmLabeledChild)
    btnViewProfiles: TButton;
    btnPipeSystemQueries: TButton;
    btnSpecifyDiversions: TButton;
    btnBuildLateralsAndParcels: TButton;
    btnBuildModels: TButton;
    chkViewProfiles: TCheckBox;
    chkPipeSystemQueries: TCheckBox;
    chkSpecifyDiversions: TCheckBox;
    btnTracePipeSystem: TButton;
    chkTracePipeSystem: TCheckBox;
    ActionList1: TActionList;
    actTracePipeSystem: TAction;
    actViewProfiles: TAction;
    actPipeSystemQueries: TAction;
    actSpecifyDiversions: TAction;
    actUpdateChangesToMaster: TAction;
    Label3: TLabel;
    btnModifyModelBoundaries2: TButton;
    actModifyModelBoundaries: TAction;
    Button1: TButton;
    Label2: TLabel;
    procedure actViewProfilesUpdate(Sender: TObject);
    procedure actTracePipeSystemExecute(Sender: TObject);
    procedure TracePipeSystem(Sender: TObject);
    procedure actViewProfilesExecute(Sender: TObject);
    procedure actPipeSystemQueriesExecute(Sender: TObject);
    procedure PipeSystemQueries(Sender: TObject;  verbose : boolean);
    procedure actSpecifyDiversionsExecute(Sender: TObject);
    procedure ActModifyModelBoundariesExecute(Sender: TObject);
    procedure btnBuildModelsClick(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure btnBuildLateralsAndParcelsClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
	frmBuildPipeSystem: TfrmBuildPipeSystem;

{function Treeverse(strINImdl:Pchar; strINISection:Pchar = nil;
	bRootsOnly:Boolean = False): integer; stdcall ;
	external 'tracer.dll';}

implementation

uses dmStateMachine,dmmapinfo, fMain, fBuildModels, fStatus,
  fModifyModelBoundaries, fBuildLateralsAndParcels, uWorkbenchDefs, uUtilities;

{$R *.dfm}

{ TODO -oAMM -cModel Functionality :
If working from existing model, update check boxes depending on state
(use model.ini) }

{ TfrmBuildPipeSystem }

procedure TfrmBuildPipeSystem.actViewProfilesUpdate(Sender: TObject);
begin
  inherited;
  actViewProfiles.Enabled := StateMachine.HasPipeSystem;
  actPipeSystemQueries.Enabled := StateMachine.HasPipeSystem;
  actSpecifyDiversions.Enabled := StateMachine.HasPipeSystem;
  actUpdateChangesToMaster.Enabled := StateMachine.HasPipeSystem;
end;


procedure TfrmBuildPipeSystem.actTracePipeSystemExecute(Sender: TObject);
begin
  inherited;
  Screen.Cursor := crHourGlass;
  TracePipeSystem(sender);
  frmmain.modelini.WriteString(strbuildlogsection,'TracePipes',DateTimeToStr(Now));
  frmmain.modelini.UpdateFile;
  Screen.Cursor := crDefault;
end;

procedure TfrmBuildPipeSystem.TracePipeSystem(Sender: TObject);
var
  ModelLookupTablesPath: string;
  rc_treeverse, mbrc: integer;
  myoleac : variant;
//  mytbl :variant;
	projdir : string;
	IniRoot: String;
begin
	inherited;
	// We're rewriting the trace, so perform any necessary resets for dependents
	// on trace

{
  case MessageDlg('All pipes, laterals, parcels, etc. will be deleted' + #13 + 'Continue Trace?',
      mtWarning, [mbYes, mbNo], 0) of
    //mrYes: // User wants to overwrite
		mrNo: // User doesn't want to overwrite
			begin
		    Exit; // in a mdlNull State
		  end;
 	end; //case


	StateMachine.HasPipeSystem := False;
 }
	{ TODO -oDJC -cModel Functionality : Build Pipe System: trace launch code and
	dependency code }


	// AMM 3/21/2006
	// Copy over the LookupTables.mdb
	// In the future, we should have an entry in [mdbs] that indicate which master
	// mdbs should be copied over everytime a trace occurs.  This is it for now,
	// I think.
	IniRoot := extractFiledir(frmmain.ModelIni.FileName);
	ModelLookupTablesPath := IniRoot + MDBSDirPath +
		ExtractFileName(frmMain.ModelIni.ReadString(MDBSSectionString, strLookupTables, 'unknown'));

	// Rename the local copy first before deleting it, just in case the master
	// can't be copied over.  If that doesn't work, continue with the local copy.
	if FileExists(frmMain.ModelIni.ReadString(MDBSSectionString, strLookupTables, 'unknown')) then
	begin
		if not RenameFile(ModelLookupTablesPath, ModelLookupTablesPath + '.bak') then
			ShowMessage('Could not rename local LookupTables.mdb for overwriting; '+
				'continuing with local copy');
		// If the rename does work, try copying the master over
		// If the master copy-over doesn't work, restore the original local file to
		// its original state and move on
		if CopyFile(frmMain.ModelIni.ReadString(MDBSSectionString, strLookupTables, 'unknown'),
			IniRoot + MDBSDirPath + strLookupTables + '.mdb') then
		begin
			// If the master copy-over did work, then we get rid of the original local cppy
			DeleteFile(ModelLookupTablesPath + '.bak');
		end
		else
		begin
			// skip it and continue with Local if we couldn't copy from the master
			ShowMessage('Could not copy master LookupTables.mdb; using local copy: ' +
				frmMain.ModelIni.ReadString(MDBSSectionString, strLookupTables,'unknown'));
			RenameFile(ModelLookupTablesPath + '.bak', ModelLookupTablesPath);
		end;
	end;

	//  frmstatus.StatusBox('This may take several minutes','','Traversing Pipe Network');
//	rc_treeverse := Treeverse(pchar(frmmain.ModelIni.filename));
	frmstatus.hide;

	if rc_treeverse <= 0 then
	begin
		stateMachine.HasPipeSystem := False;
    If rc_treeverse < 0 then showmessage('An treeverse error has occurred');
    If rc_treeverse = 0 then showmessage('No links were found, check roots');
    exit;
  end;

  // The following code should only execute if pipe system trace is successful
	// Tracing a pipe system will also reset subsequent pipe system activities as
	// well as invalidate laterals/parcels and subcatchments

  case MessageDlg(inttostr(rc_treeverse) + ' Pipes found.' +  #13 +
    'All pipes, laterals, parcels, etc. will be deleted' + #13 +
    'Continue Trace?',
			mtWarning, [mbYes, mbNo], 0) of
    //mrYes: // User wants to overwrite
		mrNo: // User doesn't want to overwrite
			begin
        stateMachine.HasPipeSystem := False;
		    Exit; // in a mdlNull State
		  end;
 	end; //case

  frmbuildpipesystem.Repaint;

	mbrc := frmStatus.MIRunMenuCmd(frmmain.goleMI,
  'Creating ' + inttostr(rc_treeverse)+ ' Mappable Pipes',	EMG_BuildMDL_Links, 'Trace Pipes', EMG_AppName, strMBmsg , strMBrc);
  if mbrc <> ReturnSuccess then
  begin
    StateMachine.HasPipeSystem := False;
    exit;
  end;

  projdir := extractFiledir(frmmain.modelini.FileName);
  myoleac := createoleobject('Access.Application');
  myoleac.OpenCurrentDatabase(projdir + '\mdbs\ModelAssemble.mdb');

  try
    if mapinfodo(frmmain.golemi,'commit table mdl_SpecLinkData') <> ReturnSuccess then exit;
    if mapinfodo(frmmain.golemi,'pack table mdl_SpecLinkData data') <> ReturnSuccess then exit;
    myoleac.currentdb.execute('qryAppendSpecLinkData');
    myoleac.currentdb.execute('qrySpecLinkGageID');
    if mapinfodo(frmmain.golemi,'commit table mdl_SpecLinkData') <> ReturnSuccess then exit;
	except
		on E: eoleexception do
		begin
				showmessage('Exception'#13'TracePipeSystem: Committing special links '#13+E.Message);
    end;
  else
 		raise Exception.create('error running queries from ModelAssemble table');
  end;
  myoleac := unassigned;
  StateMachine.HasPipeSystem := True;
end;

procedure TfrmBuildPipeSystem.actViewProfilesExecute(Sender: TObject);
begin
	inherited;
	{ TODO -oDJC -cModel Functionality : View Profiles: Profiler launch code }

	chkViewProfiles.Checked := True;
end;

procedure TfrmBuildPipeSystem.actPipeSystemQueriesExecute(Sender: TObject);
begin
  Screen.Cursor := crHourGlass;
  PipeSystemQueries(sender, True);
  Screen.Cursor := crDefault;
end;

procedure TfrmBuildPipeSystem.PipeSystemQueries(Sender: TObject; verbose: Boolean);
var
  myoleac : variant;
  x : variant;
  projdir : string;

begin
	inherited;

  chkPipeSystemQueries.Checked := false;

  projdir := extractFiledir(frmmain.modelini.FileName);
  try
    myoleac := createoleobject('Access.Application');
    myoleac.OpenCurrentDatabase(projdir + '\mdbs\LinkQAQC.mdb');

    x := myoleac.run('ExportQCReport', 'Qchec', projdir + '\qc\LinkQAQC.txt',0);
    x := myoleac.run('ExportQCTables',  projdir + '\qc', 'QAQCQueryList', 0);

    myoleac.closecurrentdatabase;
    myoleac := unassigned;

    if x > -1 then
    begin
      showmessage('an error occured writing ' + extractFiledir(frmmain.modelini.FileName) + '\qc\LinkQAQC.txt');
      exit;
    end;

  except
	  on E: eoleexception do
    begin
			  showmessage(E.Message);
        exit
    end;
  else
 		raise Exception.create('error writing report');
  end;

	{ TODO -oDJC -cModel Functionality : Pipe System QC Queries: launch code }

  if verbose then
    showmessage(extractFiledir(frmmain.modelini.FileName) + '\qc\LinkQAQC.txt written');
  
  chkPipeSystemQueries.Checked := True;
end;

procedure TfrmBuildPipeSystem.actSpecifyDiversionsExecute(Sender: TObject);
begin
	inherited;
	{ TODO -oDJC -cModel Functionality : Specify Diversions: launch code }

	chkSpecifyDiversions.Checked := True;
end;

procedure TfrmBuildPipeSystem.ActModifyModelBoundariesExecute(Sender: TObject);
begin
  inherited;
  frmmain.HideWindows;
  frmModifyModelBoundaries.show;
end;

procedure TfrmBuildPipeSystem.btnBuildModelsClick(Sender: TObject);
begin
  inherited;
  frmmain.HideWindows;
  frmBuildModels.show;
end;

procedure TfrmBuildPipeSystem.FormShow(Sender: TObject);
begin
  inherited;
  frmModifyModelBoundaries.Forminitialize();
  actTracePipeSystem.Enabled := statemachine.HasBoundaries;
  actPipeSystemQueries.Enabled := statemachine.Haspipesystem;
  //frmBuildLateralsandParcels.actTraceLateralsAndParcels.Enabled := true;

end;

procedure TfrmBuildPipeSystem.btnBuildLateralsAndParcelsClick(
  Sender: TObject);
begin
  inherited;
   frmmain.HideWindows;
   frmBuildLateralsAndParcels.show;
end;

procedure TfrmBuildPipeSystem.Button1Click(Sender: TObject);
begin
  inherited;
  frmmain.actBuildModelsExecute(Sender)
end;

end.
