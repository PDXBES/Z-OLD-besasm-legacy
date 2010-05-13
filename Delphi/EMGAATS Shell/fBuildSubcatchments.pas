{==fBuildSubcatchments Unit=====================================================

	Form for building subcatchments

	Revision History
	2.11   07/13/2006 (AMM) Added code to create project area and canopy mask
  2.1    04/3/2003 (DJC) Eliminate hydworkbench
  2.1    03/25/2003 (DJC) Removed HYDworkbench init calls - these happen in the Hydworkbench
	2.1    03/06/2003 (AMM) Revised calls to MapInfo to use constants in uWorkbenchDefs
	1.5    ?          ?
	1.0    11/01/2002 Initial release

===============================================================================}

unit fBuildSubcatchments;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, fLabeledChild, StdCtrls, ActnList, GlobalConstants, RzLabel,
  pngimage, RzBckgnd, ExtCtrls, RzPanel;

type
  TfrmBuildSubcatchments = class(TfrmLabeledChild)
    btnTraceSubcatchments: TButton;
    btnCheckSubcatchments: TButton;
    btnCreateMapLinkingSubcatchmentsToNodes: TButton;
    btnFinish: TButton;
    btnBuildModels: TButton;
    chkTraceSubcatchments: TCheckBox;
    chkCheckSubcatchments: TCheckBox;
    chkCreateMapLinkingSubcatchmentsToNodes: TCheckBox;
    ActionList1: TActionList;
    actTraceSubcatchments: TAction;
    actCheckSubcatchments: TAction;
    actCreateMapLinkingSubcatchmentsToNodes: TAction;
    Button1: TButton;
    Label2: TLabel;
    Label3: TLabel;
    Button2: TButton;
    actCreateProjectArea: TAction;
    procedure actTraceSubcatchmentsExecute(Sender: TObject);
    procedure TraceSubcatchments(Sender: TObject);
    procedure actCheckSubcatchmentsExecute(Sender: TObject);
    procedure actCreateMapLinkingSubcatchmentsToNodesExecute(
      Sender: TObject);
    procedure actCheckSubcatchmentsUpdate(Sender: TObject);
    procedure actCreateMapLinkingSubcatchmentsToNodesUpdate(
      Sender: TObject);
    procedure btnBuildModelsClick(Sender: TObject);
    procedure actCreateProjectAreaExecute(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmBuildSubcatchments: TfrmBuildSubcatchments;

implementation

uses fMain, fBuildModels, dmStateMachine, fStatus, uWorkbenchDefs;

{$R *.dfm}

{ TODO -oAMM -cModel Functionality :
If working from existing model, update check boxes depending on state
(use model.ini) }

procedure TfrmBuildSubcatchments.actTraceSubcatchmentsExecute(
	Sender: TObject);
begin

  Screen.Cursor := crHourGlass;
  TraceSubcatchments(sender);
  frmmain.modelini.WriteString(strBuildLogSection,'TraceSurface',DateTimeToStr(Now));
  frmmain.modelini.UpdateFile;
  Screen.Cursor := crDefault;

end;


procedure TfrmBuildSubcatchments.TraceSubcatchments(Sender: TObject);

var
  mbrc, i : integer;
  mytime : string;


begin
	inherited;

  if uppercase(frmmain.ModelIni.ReadString('admin', 'disable', 'FALSE')) = 'TRUE' then
  begin
    showmessage('admin has locked this model');
    exit;
  end;

  mytime := uppercase(frmmain.ModelIni.ReadString('ModelState', 'timeframe', 'NOT FOUND'));
  if mytime = 'NOT FOUND' then
  begin
    showmessage('fatal error time frame not set');
    exit;
  end;

  for i:=0 to high(timeframes) do begin
    if mytime = timeframes[i] then  break;
  end;

  if i > high(timeframes) then
  begin
    showmessage('illegal timeframe[' + mytime +']');
    exit;
  end;

	StateMachine.HasSubcatchments := False;

	mbrc := frmStatus.MIRunMenuCmd(frmmain.goleMI, 'Selecting Surface Subcatchments',
		EMG_TraceSurfaceSC, 'Surface Subcatchments', EMG_AppName, strMBmsg , strMBrc);
  if mbrc <> ReturnSuccess then
  begin
    StateMachine.HasSubcatchments := False;
    exit;
  end;

 	{ TODO -oDJC -cModel Functionality : Trace Subcatchments: launch code and
	dependency code}

	StateMachine.HasSubcatchments := True;
end;

procedure TfrmBuildSubcatchments.actCheckSubcatchmentsExecute(
	Sender: TObject);
begin
	inherited;
	{ TODO -oDJC -cModel Functionality : Check Subcatchments: launch code }

	chkCheckSubcatchments.Checked := True;
end;

procedure TfrmBuildSubcatchments.actCreateMapLinkingSubcatchmentsToNodesExecute(
	Sender: TObject);
var
  mbrc : integer;

begin
	inherited;
	{ TODO -oDJC -cModel Functionality : Create Map Linking Subcatchments to Nodes: launch code }


  if uppercase(frmmain.ModelIni.ReadString('admin', 'disable', 'FALSE')) = 'TRUE' then exit;

	mbrc := frmStatus.MIRunMenuCmd(frmmain.goleMI, 'Creating Surface Subcatchment Zingers',
		EMG_MakeSurfZingers, 'Surface Subcatchments', EMG_AppName, strMBmsg , strMBrc);
  if mbrc <> ReturnSuccess then
  begin
    showmessage('create zingers failed');
    StateMachine.HasSubcatchments := False;
    exit;
  end;

	chkCreateMapLinkingSubcatchmentsToNodes.Checked := True;
end;

procedure TfrmBuildSubcatchments.actCheckSubcatchmentsUpdate(
  Sender: TObject);
begin
  inherited;
	actCheckSubcatchments.Enabled := StateMachine.HasSubcatchments;
end;

procedure TfrmBuildSubcatchments.actCreateMapLinkingSubcatchmentsToNodesUpdate(
	Sender: TObject);
begin
	inherited;
  exit;
  actCreateMapLinkingSubcatchmentsToNodes.Enabled := StateMachine.HasSubcatchments;
end;





procedure TfrmBuildSubcatchments.btnBuildModelsClick(Sender: TObject);
begin
  inherited;
  frmmain.actBuildModelsExecute(Sender)
end;

// AMM 7/13/2006 Added creation of project area layer
procedure TfrmBuildSubcatchments.actCreateProjectAreaExecute(
	Sender: TObject);
var
	NumRows: Integer;
	appMapInfo: Variant;
	appMapInfoResult: Variant;
	iniroot: String;
begin
	inherited;

	Screen.Cursor := crHourglass;


	iniroot := extractFiledir(frmmain.ModelIni.FileName) + '\';

	// TableInfo(QyTmp,8) means to return number of rows from QyTmp table

	// Create project boundary
	appMapInfo := frmMain.goleMI;
	appMapInfo.Do('select obj from mdl_SurfSC where col1= -1 into QyTmp ');
	appMapInfo.Do('Commit Table QyTmp As Str$("'+iniroot+'" + "surfsc\ProjAreaTmp.TAB") TYPE NATIVE Charset "WindowsLatin1"');
	appMapInfo.Do('Open Table Str$("'+iniroot+'" + "surfsc\ProjAreaTmp.TAB") Interactive');
	appMapInfo.Do('select * from mdl_SurfSC where obj into QyTmp');
	NumRows := StrToInt(appMapInfo.Eval('TableInfo(QyTmp,8)'));
	if NumRows > 0 then
		appMapInfo.Do('Create Object As Buffer From Selection Width 10 Units "ft" Type Cartesian Resolution 12 Into Table ProjAreaTmp')
	else
		ShowMessage('No objects found in mdl_surfsc_ac');
	appMapInfo.Do('select * from mdl_DSC into QyTmp');
	appMapInfo.Do('Create Object As Buffer From Selection Width 10 Units "ft" Type Cartesian Resolution 12 Into Table ProjAreaTmp');
	appMapInfo.Do('Select * from ProjAreaTmp into QyTmp');
	appMapInfo.Do('Objects Combine');
	appMapInfo.Do('Create Object As Buffer From Selection Width -10 Units "ft" Type Cartesian Resolution 12 Into Table ProjAreaTmp');
	appMapInfo.Do('Commit Table ProjAreaTmp');
	appMapInfo.Do('Select * from ProjAreaTmp where rowid<>INT(tableinfo("ProjAreaTmp",8)) into QyTmp');
	appMapInfo.Do('Commit Table QyTmp As Str$("'+iniroot+'" + "surfsc\ProjArea.TAB") TYPE NATIVE Charset "WindowsLatin1"');
	appMapInfo.Do('Open Table Str$("'+iniroot+'" + "surfsc\ProjArea.TAB") Interactive');
	appMapInfo.Do('Drop Table ProjAreaTmp');

	// Create project mask
	appMapInfo.Do('Open Table "\\cassio\gis1\MI_DATA\BOUNDARY\Extents.TAB" Interactive');
	appMapInfo.Do('Commit Table Extents As Str$("'+iniroot+'" + "surfsc\ProjMask.TAB") TYPE NATIVE Charset "WindowsLatin1"');
	appMapInfo.Do('Open Table Str$("'+iniroot+'" + "surfsc\ProjMask.TAB") Interactive');
	appMapInfo.Do('Close table extents');
	appMapInfo.Do('select * from ProjArea into Eraser');
	appMapInfo.Do('select * from ProjMask');
	appMapInfo.Do('set target on');
	appMapInfo.Do('select * from Eraser');
	appMapInfo.Do('Objects Erase Into Target');
	appMapInfo.Do('Close Table eraser');
	appMapInfo.Do('Commit Table ProjMask');
	appMapInfo.Do('Close Table ProjMask');
	appMapInfo.Do('Open Table Str$("'+iniroot+'" + "surfsc\ProjMask.TAB") Interactive');
	Screen.Cursor := crDefault;
end;

end.
