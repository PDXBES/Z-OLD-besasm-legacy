{==fBuildLateralsAndParcels Unit================================================

	Form for building laterals and parcels

	Revision History
	2.1    03/13/2003 (DJC removed relink in InitDSCHydro Function.  All relinks occur in TfrmMain.EnterModel
	2.1    03/06/2003 (AMM) Revised calls to MapInfo to use constants in uWorkbenchDefs
	1.5    ?          ?
	1.0    11/01/2002 Initial release

===============================================================================}

unit fBuildLateralsAndParcels;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, fLabeledChild, StdCtrls, ActnList, comobj,dmmapinfo, RzLabel,
  RzBckgnd, ExtCtrls, RzPanel;

type
  TfrmBuildLateralsAndParcels = class(TfrmLabeledChild)
    btnSpecifyDivides: TButton;
    btnTraceLateralsAndParcels: TButton;
    btnCheckLateralsAndParcels: TButton;
    btnBuildSubcatchments: TButton;
    btnBuildPipeSystem: TButton;
    btnBuildModels: TButton;
    chkSpecifyDivides: TCheckBox;
    chkTraceLateralsAndParcels: TCheckBox;
    chkCheckLateralsAndParcels: TCheckBox;
    ActionList1: TActionList;
    actSpecifyDivides: TAction;
    actTraceLateralsAndParcels: TAction;
    actCheckLateralsAndParcels: TAction;
    Label2: TLabel;
    Label3: TLabel;
    btnInitDSCHydro: TButton;
    chkInitDSCHydro: TCheckBox;
    chkUseDefaultDiscoTable: TCheckBox;
    procedure actSpecifyDividesExecute(Sender: TObject);
    procedure actTraceLateralsAndParcelsExecute(Sender: TObject);
    procedure TraceLateralsAndParcels(Sender: TObject);
    procedure actCheckLateralsAndParcelsExecute(Sender: TObject);
    procedure actCheckLateralsAndParcelsUpdate(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure btnBuildModelsClick(Sender: TObject);
    procedure TraceParcels(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmBuildLateralsAndParcels: TfrmBuildLateralsAndParcels;

implementation

uses fMain, fBuildModels, dmStateMachine, fStatus, globalconstants,
  uWorkbenchDefs;

{$R *.dfm}

{ TODO -oAMM -cModel Functionality :
If working from existing model, update check boxes depending on state
(use model.ini) }

procedure TfrmBuildLateralsAndParcels.actSpecifyDividesExecute(
  Sender: TObject);
begin
  inherited;
	{ TODO -oDJC -cModel Functionality : Specify Divides: launch code }

	chkSpecifyDivides.Checked := True;
end;

procedure TfrmBuildLateralsAndParcels.actTraceLateralsAndParcelsExecute(
	Sender: TObject);
begin
  inherited;
  Screen.Cursor := crHourGlass;
  frmstatus.StatusBox('','','Trace Laterals and Parcels');

//  TraceLateralsAndParcels(sender);
  TraceParcels(sender);
  frmmain.modelini.WriteString(strbuildlogsection,'TraceParcels',DateTimeToStr(Now));
  frmmain.modelini.UpdateFile;

  frmstatus.Hide;
  Screen.Cursor := crDefault;
end;


procedure TfrmBuildLateralsAndParcels.TraceLateralsAndParcels(Sender: TObject);
var
  mbrc : integer;
  xrc: integer;
  projdir, mytime, strcmd, mycaption : string;
  s : string;
  myoleac : variant;


begin
	inherited;
  mycaption := 'Trace Laterals and Parcels';
	StateMachine.HasLateralsAndParcels := False;

	{ TODO -oDJC -cModel Functionality : Trace Laterals and Parcels: launch code
	and dependency code}
  mbrc := frmStatus.MIRunMenuCmd(frmmain.goleMI, 'Trace Laterals',EMG_TraceLaterals, mycaption, 'EMGWorkbench', strMBmsg , strMBrc);

  if mbrc <> ReturnSuccess then
  begin
    StateMachine.HasLateralsAndParcels := False;
    exit;
  end;

	mbrc := frmStatus.MIRunMenuCmd(frmmain.goleMI, 'Calculate DSC',
		EMG_CalcDSC, mycaption, EMG_AppName, strMBmsg , strMBrc);

  if mbrc <> ReturnSuccess then
  begin
    StateMachine.HasLateralsAndParcels := False;
    exit;
  end;

  Try
    projdir :=  extractFiledir(frmmain.modelini.FileName);
    myoleac := createoleobject('Access.Application');
    myoleac.OpenCurrentDatabase(projdir + '\mdbs\HYDInitDSC.mdb');

    mytime := uppercase(frmmain.ModelIni.ReadString('ModelState', 'timeframe', 'NOT FOUND'));

    frmstatus.StatusBox('Reset inflow controls with master source','',mycaption);

    // delete all ics
    // append all existing ics
    // initialize the baseflow as EX. calc DM baseflow
    xrc := myoleac.run('executequerytable','listexecuteInitDSC', 'Block' , 'TDSC_ALL' , 0);
		if xrc >=0 then
		begin
      showmessage('executequerytable error, block TDSC_ALL, return code: ' + inttostr(xrc));
      myoleac := unassigned;
      exit;
    end;


    if mytime = 'FU' then
    begin
      //set the future base base flow
      frmstatus.StatusBox('setting future baseflow','',mycaption);
     	myoleac.run('setFBfactoredbaseflow',0.5);

      //append future ics -- these are INCRMENTAL or IN ADDITION TO exising ics
      frmstatus.StatusBox('getting future inflow controls from master','',mycaption);

      { TODO -odjc : add queries for storage and green roofs }
      xrc := myoleac.run('executequerytable','listexecuteInitDSC', 'Block' , 'TDSC_FB' , 0);
		  if xrc >=0 then
		  begin
        showmessage('executequerytable error, block TDSC_FB, return code: ' + inttostr(xrc));
        myoleac := unassigned;
        exit;
      end;
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


  StateMachine.HasLateralsAndParcels := True;

end;

procedure TfrmBuildLateralsAndParcels.TraceParcels(Sender: TObject);
var
  mbrc: Integer;
  mycaption: string;
begin
  mycaption := 'Trace Parcels';
  mbrc := frmStatus.MIRunMenuCmd(frmmain.goleMI, 'Trace Parcels', EMG_TraceDSCs,
    mycaption, 'EMGWorkbench', strMBmsg , strMBrc);
  if mbrc <> ReturnSuccess then
    ShowMessage('TraceParcels failed');
end;

procedure TfrmBuildLateralsAndParcels.actCheckLateralsAndParcelsExecute(
	Sender: TObject);
begin
	inherited;
	{ TODO -oDJC -cModel Functionality : Check Laterals and Parcels: launch code }

	chkCheckLateralsAndParcels.Checked := True;
end;

procedure TfrmBuildLateralsAndParcels.actCheckLateralsAndParcelsUpdate(
  Sender: TObject);
begin
  inherited;
  actCheckLateralsAndParcels.Enabled := StateMachine.HasLateralsAndParcels;
end;

procedure TfrmBuildLateralsAndParcels.FormShow(Sender: TObject);
begin
  inherited;
  actTraceLateralsandParcels.Enabled := statemachine.HasPipeSystem;
  if strtobool(frmmain.ModelIni.ReadString('Options','DefaCombined','TRUE')) then
    frmBuildLateralsAndParcels.chkUseDefaultDiscoTable.state := cbChecked
  else
    frmBuildLateralsAndParcels.chkUseDefaultDiscoTable.state := cbUnChecked;

end;




procedure TfrmBuildLateralsAndParcels.btnBuildModelsClick(Sender: TObject);
begin
  inherited;
  frmMain.actBuildModelsExecute(Sender);
end;

end.
