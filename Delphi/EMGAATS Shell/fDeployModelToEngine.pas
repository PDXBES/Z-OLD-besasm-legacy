{==fDeployModelToEngine Unit====================================================

	Form for deploying models to a specific modeling engine

	Revision History
          12/3/2003 (DJC) added the deploy pdx runoff to build and deploy
	2.1    03/06/2003 (AMM) Revised calls to MapInfo to use constants in uWorkbenchDefs
         03/12/2003 (AMM) Changed paths to SWMM executables in batch file
	1.5    ?          ?
	1.0    11/01/2002 Initial release

===============================================================================}

unit fDeployModelToEngine;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, fLabeledChild, StdCtrls, ActnList,comobj, dmmapinfo, globalconstants,
  utilMSaccess, DB, ADODB, strutils, RzLabel, RzBckgnd, ExtCtrls, RzPanel;

type
  TfrmDeployModel = class(TfrmLabeledChild)
    Label2: TLabel;
    cmbEngine: TComboBox;
    btnDeployModel: TButton;
    ActionList1: TActionList;
    actDeployHydraulicModels: TAction;
    Button1: TButton;
    BuildModels: TButton;
    Label3: TLabel;
    Button4: TButton;
    btnBuildPDXRunoff: TButton;
    btnBuildPDXTransport: TButton;
    actPrepHydrologicData: TAction;
    actDeployPDXRunoff: TAction;
    chkMiVisible: TCheckBox;
    actMIVisible: TAction;
    Button2: TButton;
    Label4: TLabel;
    actDeployPDXTransport: TAction;
    procedure actDeployHydraulicModelsExecute(Sender: TObject);
    procedure DeployHydraulicModels(Sender: TObject);

    procedure actPrepHydrologicDataExecute(Sender: TObject);
    procedure PrepHydrologicData(Sender: TObject);
    
    procedure actDeployPDXRunoffExecute(Sender: TObject);
    procedure DeployPDXRunoff(Sender: TObject; Verbose: Boolean);
    procedure FormShow(Sender: TObject);
    procedure actMIVisibleExecute(Sender: TObject);

    procedure actDeployPDXTransportExecute(Sender: TObject);
    procedure DeployPDXTransport(Sender: TObject);

    procedure createmodelbatchfile(deck : string);
    procedure BuildModelsClick(Sender: TObject);

  private
    { Private declarations }
  public
    { Public declarations }
  end;

  const
  runofflines : array[0..13] of string = (
  '@del runoff.out',
  '@del runoff.int',
  '@del transport.out',
  '@del transport.int',
  '',
  '@echo runoff.rdt > in.ro',
  '@echo runoff.out >> in.ro',
  '',
  '',
  '',
  '',
  '',
	'\\cassio\modeling\model_programs\swmm\xswmm95y2k < in.ro',
  '');

  transportlines : array[0..13] of string = (
  '@del runoff.out',
  '@del runoff.int',
  '@del transport.out',
  '@del transport.int',
  '',
  '@echo runoff.rdt > in.ro',
  '@echo runoff.out >> in.ro',
  '',
  '@echo transport.tdt > in.tr',
  '@echo transport.out >> in.tr',
  '',
	'\\cassio\modeling\model_programs\swmm\xswmm95y2k < in.ro',
	'\\cassio\modeling\model_programs\swmm\trans95a < in.tr',
  '');


var
  frmDeployModel: TfrmDeployModel;

implementation

uses fBuildModels, fMain, fStatus, dmStateMachine, fgetStormOption,
  uWorkbenchDefs;

{$R *.dfm}


procedure TfrmDeployModel.actDeployHydraulicModelsExecute(Sender: TObject);

begin
  inherited;
  Screen.Cursor := crHourGlass;
  DeployHydraulicModels(sender);
  Screen.Cursor := crdefault;
end;



procedure TfrmDeployModel.DeployHydraulicModels(Sender: TObject);
Var
  myoleac : variant;
  projdir,  XPXname, DHIName, StrA2, StrQry, strDateTime : string;
  xrc : integer;


begin
  inherited;

  Try

    myoleac := createoleobject('Access.Application');
    myoleac.OpenCurrentDatabase(extractFiledir(frmmain.modelini.FileName) + '\mdbs\ModelDeployHydraulics.mdb');

    projdir :=  extractFiledir(frmmain.modelini.FileName);
    xpxname := projdir +  '\sim\XPextran.xpx';

    myoleac.run('ReferenceFromFile',projdir +'\mdbs\emgaatscode.mdb','EMGAATScode');
    myoleac.run('XP_Parallel_Links', '_getParallelLinks');
    myoleac.run('XP_Special_Links');

    strA2:='"TimeFrame: ' + frmmain.ModelIni.ReadString('ModelState','timeframe','ERROR') +
           ', Model Created:' + DateToStr(Date) + ' ' + Timetostr(Time)+ '"';

    strqry := 'UPDATE JobControlExtran SET JobControlExtran.TValue = ' + strA2 +
              ' WHERE (((JobControlExtran.Tag)="ALPHB")) ';
    xrc := myoleac.currentdb.execute(strQry);

    // - 1 is a successful return
    xrc := myoleac.run('ExportQueryTable',xpxname,'XPExportQueryTable','Class','ALL',0);
    myoleac.currentdb.execute('_UpdateSimLinkID');

    dhiName := projdir + '\sim\DHImodel.mdb';
    // - 1 is a successful return
    xrc := myoleac.run('CreateDHIdatabase',dhiname,'DHI_Export');
    myoleac := unassigned;


 except

    on E: eoleexception do
    begin
      showmessage('An oleexception occured' + #13 + E.Message + #13 + 'return code: ' + inttostr(xrc));
      myoleac := unassigned;
      showmessage(xpxname + ' NOT deployed');
      exit;
    end;
    else
    begin
      myoleac := unassigned;
      raise Exception.create('ExecuteQueryTable Error');
      exit;
    end;

  end;

  if xrc < 0 then
  begin
    showmessage('DHIModel.mdb and XPextran.xpx files are created in the sim directory' + #13 +
    '  After Import to XP check the following:' + #13 +
    '  1) Job Control ' + #13 +
    '  2) Interface Files') ;
    strDateTime:=DateToStr(Date) + ' ' + Timetostr(Time);
    frmmain.modelini.Writestring('Admin','DeployEXTRAN',strDateTime);
  end
  else
  begin
    showmessage('deployment FAILED') ;
    strDateTime:=DateToStr(Date) + ' ' + Timetostr(Time);
    frmmain.modelini.Writestring('Admin','DeployEXTRAN','FAILED on ' + strdatetime);
  end;


end;

procedure TfrmDeployModel.actPrepHydrologicDataExecute(Sender: TObject);

begin
  inherited;
  Screen.Cursor := crHourGlass;
  frmstatus.StatusBox('','','Initializing Hydrology');

  PrepHydrologicData(sender);

  frmmain.modelini.WriteString(strBuildLogSection,'PrepHydrologicData',DateTimeToStr(Now));
  frmmain.modelini.UpdateFile;

  frmstatus.Hide;
  Screen.Cursor := crDefault;
end;



procedure TfrmDeployModel.PrepHydrologicData(Sender: TObject);
var
  xrc: integer;
  projdir, mytime, strcmd, mycaption : string;
  myoleac : variant;

begin
  inherited;
  mycaption := 'Initializing Hydrology';

  Try
    projdir :=  extractFiledir(frmmain.modelini.FileName);

    mytime := uppercase(frmmain.ModelIni.ReadString('ModelState', 'timeframe', 'NOT FOUND'));


    {
    frmstatus.StatusBox('commit mapinfo tables prior to running access queries','',mycaption);

    if mapinfodo(frmmain.golemi,'commit table mdl_DSC') <> ReturnSuccess then
    	ShowMessage('Commit mdl_DSC failed, call 823-7735');

    if mapinfodo(frmmain.golemi,'commit table mdl_DiscoVeg') <> ReturnSuccess then
    	ShowMessage('Commit mdl_DiscoVeg failed, call 823-7735');

    if mapinfodo(frmmain.golemi,'commit table mdl_Drywell') <> ReturnSuccess then
    	ShowMessage('Commit mdl_Drywell failed, call 823-7735');

    if mapinfodo(frmmain.golemi,'commit table mdl_SWPlnt') <> ReturnSuccess then
    	ShowMessage('Commit mdl_SWPlnt failed, call 823-7735');

    if mapinfodo(frmmain.golemi,'commit table mdl_Infilt') <> ReturnSuccess then
    	ShowMessage('Commit mdl_Infilt failed, call 823-7735');

    frmstatus.StatusBox('pack mapinfo tables','',mycaption);


    // it seems as though the commit and sleep are required or the pack borks
    sleep(1000);

    if mapinfodo(frmmain.golemi,'Select * from mdl_DiscoVeg where assumeKey="_san" into san_target') <> returnsuccess then
    begin
      showmessage ('select failed');
      exit;
    end;

    if mapinfodo(frmmain.golemi,'delete From san_target') <> returnsuccess then
    begin
      showmessage ('delete failed');
      exit;
    end;

    if mapinfodo(frmmain.golemi,'commit table mdl_DiscoVeg') <> returnsuccess then
    begin
      showmessage ('commit failed');
      exit;
    end;


    if mapinfodo(frmmain.golemi,'Close Table san_target') <> returnsuccess then
    begin
      showmessage ('close failed');
      exit;
    end;

    if mapinfodo(frmmain.golemi,'commit table mdl_DiscoVeg') <> ReturnSuccess then
    	ShowMessage('Commit mdl_DiscoVeg failed, call 823-7735');

    sleep(1000);

    if mapinfodo(frmmain.golemi,'pack table mdl_DiscoVeg data') <> ReturnSuccess then
    begin
    	ShowMessage('mdl_DiscoVeg pack failed, call 823-7735');
      exit;
    end;
    if mapinfodo(frmmain.golemi,'pack table mdl_Drywell data') <> ReturnSuccess then
    begin
    	ShowMessage('mdl_Drywell pack failed, call 823-7735');
      exit;
    end;
    if mapinfodo(frmmain.golemi,'pack table mdl_SWPlnt data') <> ReturnSuccess then
    begin
    	ShowMessage('mdl_SWPlnt pack failed, call 823-7735');
      exit;
    end;
    if mapinfodo(frmmain.golemi,'pack table mdl_Infilt data') <> ReturnSuccess then
    begin
    	ShowMessage('mdl_Infilt pack failed, call 823-7735');
      exit;
    end;
    }
    {
    xrc := frmStatus.MIRunMenuCmd(frmmain.goleMI, 'Prepare IC tables prior to calculate total IC area',
			EMG_InitHydPre,mycaption, EMG_AppName, strMBmsg , strMBrc);
    if xrc <> ReturnSuccess then
    begin
      //StateMachine.HasLateralsAndParcels := False;
      exit;
    end;

    exit;
    sleep(5000);
    }
    myoleac := createoleobject('Access.Application');
    myoleac.OpenCurrentDatabase(projdir + '\mdbs\HYDInitDSC.mdb');

    frmstatus.StatusBox('calculate total IC area for parcels','',mycaption);

    xrc := myoleac.run('executequerytable','listexecuteInitDSC', 'Block' , 'INIT_HYD' , 0);

    myoleac.CloseCurrentDatabase;
    myoleac := unassigned;

    application.ProcessMessages;

    sleep(5000);

		if xrc >=0 then
		begin
      showmessage('executequerytable error, block FB, return code: ' + inttostr(xrc));
      myoleac := unassigned;
      exit;
    end;
    {
    xrc := frmStatus.MIRunMenuCmd(frmmain.goleMI, 'Prepare IC tables after calculate total IC area',
  	EMG_InitHydPost,mycaption, EMG_AppName, strMBmsg , strMBrc);
    if xrc <> ReturnSuccess then
    begin
      showmessage('Prepare IC tables after calculate total IC area failed' + #13 + strMBmsg);

      //StateMachine.HasLateralsAndParcels := False;
      exit;
    end;
    }

    {
    frmstatus.StatusBox('commit mapinfo tables','',mycaption);

    if mapinfodo(frmmain.golemi,'commit table mdl_DSC') <> ReturnSuccess then
    	ShowMessage('Commit mdl_DSC failed, call 823-7735');

    if mapinfodo(frmmain.golemi,'commit table mdl_DiscoVeg') <> ReturnSuccess then
    	ShowMessage('Commit mdl_DiscoVeg failed, call 823-7735');

    if mapinfodo(frmmain.golemi,'commit table mdl_Drywell') <> ReturnSuccess then
    	ShowMessage('Commit mdl_Drywell failed, call 823-7735');

    if mapinfodo(frmmain.golemi,'commit table mdl_SWPlnt') <> ReturnSuccess then
    	ShowMessage('Commit mdl_SWPlnt failed, call 823-7735');

    if mapinfodo(frmmain.golemi,'commit table mdl_Infilt') <> ReturnSuccess then
    	ShowMessage('Commit mdl_Infilt failed, call 823-7735');

    frmstatus.StatusBox('pack mapinfo tables','',mycaption);


    // it seems as though the commit and sleep are required or the pack borks
    sleep(1000);

    if mapinfodo(frmmain.golemi,'pack table mdl_DiscoVeg data') <> ReturnSuccess then
    begin
    	ShowMessage('mdl_DiscoVeg pack failed, call 823-7735');
      exit;
    end;
    if mapinfodo(frmmain.golemi,'pack table mdl_Drywell data') <> ReturnSuccess then
    begin
    	ShowMessage('mdl_Drywell pack failed, call 823-7735');
      exit;
    end;
    if mapinfodo(frmmain.golemi,'pack table mdl_SWPlnt data') <> ReturnSuccess then
    begin
    	ShowMessage('mdl_SWPlnt pack failed, call 823-7735');
      exit;
    end;
    if mapinfodo(frmmain.golemi,'pack table mdl_Infilt data') <> ReturnSuccess then
    begin
    	ShowMessage('mdl_Infilt pack failed, call 823-7735');
      exit;
    end;
    }
    strcmd := 'Add Column ' + #34 + 'mdl_nodes' + #34 + ' (GageID ) From mst_virtgage Set To QS Where contains';
    if mapinfodo(frmmain.golemi,strcmd) <> ReturnSuccess then
    begin
      showMessage('failure to apply vitual gage to nodes');
      exit;
    end;

    if mapinfodo(frmmain.golemi,'commit table mdl_nodes') <> ReturnSuccess then
    begin
      showMessage('failure to commit model nodes');
      exit;
    end;

    application.ProcessMessages;

     //do the surface subcatchment spatial queries
		xrc := frmStatus.MIRunMenuCmd(frmmain.goleMI, 'Perform GIS Queries to Extract Hydrology',
			EMG_RelateDSCtoSurfSC,mycaption, EMG_AppName, strMBmsg , strMBrc);
    if xrc <> ReturnSuccess then
    begin
      //StateMachine.HasLateralsAndParcels := False;
      exit;
    end;

    sleep(1000);

     //do SOME qc
		xrc := frmStatus.MIRunMenuCmd(frmmain.goleMI, 'check for duplicate DSCs',
			EMG_QADupDsc,mycaption, EMG_AppName, strMBmsg , strMBrc);
    if xrc <> ReturnSuccess then
    begin
      //StateMachine.HasLateralsAndParcels := False;
      exit;
    end;

     //do SOME qc
		xrc := frmStatus.MIRunMenuCmd(frmmain.goleMI, 'check for ICs greater than DSC area',
			EMG_QAICmax,mycaption, EMG_AppName, strMBmsg , strMBrc);
    if xrc <> ReturnSuccess then
    begin
      //StateMachine.HasLateralsAndParcels := False;
      exit;
    end;



  except

    on E: eoleexception do
    begin
      showmessage('An oleexception occured' + #13 + E.Message + #13 + 'return code: ' + inttostr(xrc));
      exit;
    end;
    else
    begin
      myoleac := unassigned;
      raise Exception.create('ExecuteQueryTable Error');
    end;

  end;

  if xrc < 0 then frmStatus.StatusBox('', 'Hydrologic Data Successfully Processed', 'Initializing Hydrology');
  if xrc = 0 then showmessage('You may have a problem, xrc = 0');
  if xrc > 0 then showmessage('You may have a problem, xrc > 0');


end;

procedure TfrmDeployModel.actDeployPDXRunoffExecute(Sender: TObject);
begin
  inherited;

  //if it this proceedure gets called then it via the GUI
  DeployPDXRunoff(sender, TRUE);
end;

// the build and deploy calls this proceedure
procedure TfrmDeployModel.DeployPDXRunoff(Sender: TObject; Verbose: Boolean);
var
  myoleac : variant;
  projdir, strA1, strA2, strQry, strdatetime : string;
  runoffname : variant;
  xrc: integer;
//  ReconSS: TReconSS; // recon ss object - created and destroyed here
  rcSS: integer; // reconciliation spreadsheet
  sCheckSumMsg : string;

begin
  inherited;

  try
    Screen.Cursor := crHourGlass;

    projdir := extractFiledir(frmmain.modelini.FileName);
    myoleac := createoleobject('Access.Application');
    myoleac.OpenCurrentDatabase(projdir + '\mdbs\ModelDeployHydrology.mdb');

    //Execute initialization queries
		xrc := myoleac.run('executequerytable','listexecutemodelbuild', 'Block' , 'P' , 0);
		if xrc >=0 then
		begin
      showmessage('executequerytable error, block P, return code: ' + inttostr(xrc));
      myoleac := unassigned;
      exit;
    end;

    //Execute the runoff creation queries
		xrc := myoleac.run('executequerytable','listexecutemodelbuild', 'Block' , 'R' , 0);
		if xrc >=0 then
		begin
      showmessage('executequerytable error, block R, return code: ' + inttostr(xrc));
      myoleac := unassigned;
      exit;
    end;

 {     djc 3/28/03

    xrc := myoleac.run('executequerytable','listexecutemodelbuild', 'Block' , 'T' , 0);
    if xrc >=0 then
    begin
      showmessage('executequerytable error, block T, return code: ' + inttostr(xrc));
      myoleac := unassigned;
      exit;
    end;
 }


    runoffname := projdir + '\sim\' + frmmain.ModelIni.ReadString('simulation','RODeck','runoff.rdt');
    frmmain.ModelIni.WriteString('simulation','RODeck','runoff.rdt');

  //Get the storm option for a new model
    if verbose then
    begin
      frmStormOption.Showmodal;
      if frmStormOption.mbrc = mbCancel then
      begin
        myoleac :=unassigned;
        Screen.Cursor := crDefault;
        exit;
      end;
    end
    else
       frmStormOption.rdgStormOption.ItemIndex := 1;

    case frmStormOption.rdgStormOption.ItemIndex of
      -1: // no time frame selected -- this should be unreached
      begin
        showmessage('Error: no storm selected');
        Screen.Cursor := crDefault;
        myoleac :=unassigned;
        exit;
      end;
      0: //calibration
      begin
        frmmain.modelini.Writestring('ModelOpts','Storm','Calibration');
        strQry := 'Update R_Control_B1 SET R_Control_B1.[DesignStorm?] = FALSE';
        xrc := myoleac.currentdb.execute(strQry);
        strA2:='"TimeFrame: ' + frmmain.ModelIni.ReadString('ModelState','timeframe','ERROR') +
           ', Calibration Model Created:' + DateToStr(Date) + ' ' + Timetostr(Time)+ '"';
      end;
      1: //design
      begin
        frmmain.modelini.Writestring('ModelOpts','Storm','Design');
        strQry := 'Update R_Control_B1 SET R_Control_B1.[DesignStorm?] = TRUE';
        xrc := myoleac.currentdb.execute(strQry);
        strA2:='"TimeFrame: ' + frmmain.ModelIni.ReadString('ModelState','timeframe','ERROR') +
           ', Design Storm Model Created:' + DateToStr(Date) + ' ' + Timetostr(Time)+ '"';

      end;
    end;


    strA1 := '"Model: ' + rightstr(projdir,70) + '"';

    strQry :='UPDATE R_control_A1 SET R_control_A1.TitleText = ' + strA1 +
        'WHERE (((R_control_A1.TwoRecordsOnly)=1))';

    xrc := myoleac.currentdb.execute(strQry);

    strQry :='UPDATE R_control_A1 SET R_control_A1.TitleText = ' + strA2 +
        'WHERE (((R_control_A1.TwoRecordsOnly)=2))';

    xrc := myoleac.currentdb.execute(strQry);

//  xrc := acrunExportQueryTable(myoleac, runoffname ,'listexportmodelpdx','Block','R',0,'Export');
    xrc := myoleac.run('ExportQueryTable',runoffname,'listexportmodelpdx','Block','R',0);
    myoleac := unassigned;

    createmodelbatchfile('runoff');
    { TODO -oDJC -cModel Functionality : Deploy Model: add model file (specific to engine) construction code }


  except
    on E: eoleexception do
    begin
      Screen.Cursor := crDefault;

      showmessage('An Access problem occured' + #13 + E.Message + #13 + 'return code: ' + inttostr(xrc));
      showmessage('deployment failed');
      exit;
    end;
  else
      Screen.Cursor := crDefault;

      raise Exception.create('ExportQueryTable error');
  end;


  strDateTime:=DateToStr(Date) + ' ' + Timetostr(Time);

  if xrc < 0 then
  begin
    showmessage('runoff file created successfully' + #13 + #13 +
    'file: ' + runoffname + #13 + #13 +
    'You will need to edit:' + #13 +
    '  1) rainfall interface (file 11)' + #13 +
    '  2) simulation start times (B1 card)' +  #13 +
    '  3) simulation length (B3 card)' +  #13 +  #13 +
    'RECONCILIATION SPREAD SHEET WILL BE CREATED')
    ;

    update;

    strDateTime:=DateToStr(Date) + ' ' + Timetostr(Time);
    frmmain.modelini.Writestring('Admin','DeployRunoff',strDateTime);

//    Application.createform(TReconss, reconss);
//
//    rcSS :=  ReconSS.Initialize(projdir);
//    if  rcSS = returnfailure then
//    begin
//      showmessage('Recon Spreadsheet Initialization Failed');
//      reconSS.free;
//      //exit;
//    end
//    else
//    begin
//      rcSS := ReconSS.Reconcile;
//      if rcSS = returnfailure then
//      begin
//        showmessage('Recon Spreadsheet Build Failed');
//        reconSS.free;
//        //exit;
//      end
//      else
//      begin
//        //        showmessage('Recon Spreadsheet' + #13 + ReconSS.getReconSSname + #13 + 'Created' );
//
//        try
//          sCheckSumMsg :=
//          'R E C O N C I L I A T I O N    C H E C K S U M S' + #13 + #13 +
//          '     HCard Total Area: ' + format('%.2f',[double(reconss.csHCardTA)]) + #13 +
//          '      HCard Imp. Area: ' + format('%.2f',[double(reconss.csHCardIA)]) + #13 +
//          '        Dsc Total Area: ' + format('%.2f',[double(reconss.csdscTA)]) + #13 +
//          '        Dsc  Imp. Area: ' + format('%.2f',[double(reconss.csdscIA)]) + #13 + #13 +
//          ReconSS.getReconSSname + ' Created.';
//
//         if (abs(double(reconss.csHCardTA)) > RSSwarn) or
//               (abs(double(reconss.csHCardIA)) > RSSwarn) or
//               (abs(double(reconss.csdscTA)) > RSSwarn) or
//               (abs(double(reconss.csdscIA)) > RSSwarn) then
//          begin
//            sCheckSumMsg :=
//              'One or more of your checksums exceeds ' + floattostr(RSSwarn) + #13 + #13 +
//              sCheckSumMsg;
//
//            messagedlg(sCheckSumMsg, mtWarning	,[mbOk], 0);
//          end
//          else
//          begin
//            sCheckSumMsg :=
//              'All checksums are within ' + floattostr(RSSwarn)+ #13 + #13 +
//              sCheckSumMsg;
//
//            messagedlg(sCheckSumMsg, mtinformation	,[mbOk], 0);
//          end;
//
//        except
//           sCheckSumMsg :=
//          'One or more of the check sums has an error' + #13 + #13 +
//
//          ReconSS.getReconSSname + ' Created';
//
//          messagedlg(sCheckSumMsg, mtError	,[mbOk], 0);
//
//        end;
//
//        reconSS.free;
//      end;
//    end;
//
//    Screen.Cursor := crDefault;

  end;

  if xrc >= 0 then
  begin
    showmessage('you may have a problem');
    Screen.Cursor := crDefault;

  end;


end;

procedure TfrmDeployModel.actDeployPDXTransportExecute(Sender: TObject);
begin
  inherited;
  Screen.Cursor := crHourGlass;
  DeployPDXTransport(sender);
  Screen.Cursor := crDefault;
end;

procedure TfrmDeployModel.DeployPDXTransport(Sender: TObject);
var
  myoleac : variant;
  xrc : variant;
  projdir, transportfile, strdatetime, strA1, strA2, strqry : string;

begin
  inherited;
  try
    projdir := extractFiledir(frmmain.modelini.FileName);
    myoleac := createoleobject('Access.Application');

    myoleac.OpenCurrentDatabase(projdir + '\mdbs\ModelDeployHydrology.mdb');
    transportfile := projdir + '\sim\' + frmmain.ModelIni.ReadString('simulation','TRDeck','transport.tdt');
    frmmain.ModelIni.WriteString('simulation','TRDeck','transport.tdt');

    strA1 := '"Model: ' + rightstr(projdir,70) + '"';

    strQry :='UPDATE T_control_A1 SET T_control_A1.TitleText = ' + strA1 +
        'WHERE (((T_control_A1.TwoRecordsOnly)=1))';

    strA2:='"TimeFrame: ' + frmmain.ModelIni.ReadString('ModelState','timeframe','ERROR') +
           ', Design Storm Model Created:' + DateToStr(Date) + ' ' + Timetostr(Time)+ '"';

    strQry :='UPDATE T_control_A1 SET T_control_A1.TitleText = ' + strA2 +
        'WHERE (((T_control_A1.TwoRecordsOnly)=2))';

    xrc := myoleac.currentdb.execute(strQry);



    xrc := myoleac.run('ExportQueryTable',transportfile,'listexportmodelpdx','Block','T',0);
    myoleac := unassigned;
    createmodelbatchfile('transport');

    { TODO -oDJC -cModel Functionality : Deploy Model: add model file (specific to engine) construction code }


  except
    on E: eoleexception do
    begin
      Screen.Cursor := crDefault;

      showmessage('An Access problem occured' + #13 + E.Message + #13 + 'return code: ' + inttostr(xrc));
      showmessage('transport file deployment failed');
      exit;
    end;
  else
      Screen.Cursor := crDefault;
      raise Exception.create('ExportQueryTable error');
  end;


  strDateTime:=DateToStr(Date) + ' ' + Timetostr(Time);

  if xrc < 0 then
  begin

    showmessage('transport file created successfully' +  #13 + #13 +
    'file: ' + transportfile );
    strDateTime:=DateToStr(Date) + ' ' + Timetostr(Time);

    frmmain.modelini.Writestring('Admin','DeployTransport',strDateTime);

  end;

  if xrc >= 0 then
  begin
    showmessage('you may have a problem');
  end;


end;

procedure TfrmDeployModel.FormShow(Sender: TObject);
begin
  inherited;
  Screen.Cursor := crDefault;

//  actbuildpdxrunoff.Enabled :=false;
//  actDeployModel.Enabled :=false;
end;



procedure TfrmDeployModel.actMIVisibleExecute(Sender: TObject);
begin
  inherited;
  if chkmivisible.Checked = True then
  begin
    frmmain.goleMI.visible := True;
  end
  else
  begin
    frmmain.goleMI.visible := False;
  end;
end;

procedure TfrmDeployModel.createmodelbatchfile(deck : string);
var
  fs: TFileStream;
  s, filename : string;
  i : integer;
  rescratlines :array[0..5] of string;

begin
  Filename := extractFiledir(frmmain.modelini.FileName) + '\sim\pdxrun.bat';
  fs := TFileStream.Create(Filename, fmCreate or fmOpenWrite);

  if deck = 'transport' then
    for i := 0 to high(transportlines) do
    begin
      s := string(transportlines[i])+ #13 + #10;
      fs.Write(pchar(s)^, Length(s));
    end;

  if deck = 'runoff' then
    for i := 0 to high(runofflines) do
    begin
      s := string(runofflines[i])+ #13 + #10;
      fs.Write(pchar(s)^, Length(s));
    end;

  fs.free;
end;

procedure TfrmDeployModel.BuildModelsClick(Sender: TObject);
begin
  inherited;
  frmmain.actBuildModelsExecute(Sender)
end;

end.
