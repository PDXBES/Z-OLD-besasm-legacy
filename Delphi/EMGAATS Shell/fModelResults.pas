{==fModelResults Unit===========================================================

	Form for deploying models to a specific modeling engine

	Revision History
	3.0    03/06/2003 (AMM) Put relative path in call to XPResults.DLL
	1.5    ?          ?
	1.0    11/01/2002 Initial release

===============================================================================}

unit fModelResults;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, fLabeledChild, StdCtrls, ActnList,comobj, dmmapinfo, globalconstants,
	utilMSaccess, DB, ADODB, MapInfo_TLB, RzLabel, RzBckgnd, ExtCtrls, RzPanel,
  pngimage;

type
  TfrmModelResults = class(TfrmLabeledChild)
    Button1: TButton;
    Button2: TButton;
    Label3: TLabel;
    Button4: TButton;
    ActionList1: TActionList;
    actXPresults: TAction;
    OpenDialog1: TOpenDialog;

    procedure xpresults(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure actXPresultsExecute(Sender: TObject);
    procedure Button2Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmModelResults: TfrmModelResults;
	function getXPresults(InPath, OutPath: PChar): Integer; stdcall;
		external  'XPResults.dll';

implementation

uses fBuildModels, fMain, fStatus, dmStateMachine, fgetStormOption;

{$R *.dfm}

procedure TfrmModelResults.FormShow(Sender: TObject);
begin
	inherited;
	

//  actbuildpdxrunoff.Enabled :=false;
//  actDeployModel.Enabled :=false;
end;

procedure TfrmModelResults.actXPresultsExecute(Sender: TObject);
begin
  inherited;
  Screen.Cursor := crHourGlass;
  XPresults(sender);
  Screen.Cursor := crDefault;

end;

procedure TfrmModelResults.XPresults(Sender: TObject);
var
  rcxp : integer ;
  projdir : string;
  myoleac : variant;

begin
  inherited;

  try

		opendialog1.Title := 'Select XP results file';
    OpenDialog1.InitialDir := ExtractFileDir(frmMain.ModelIni.FileName);
    if not opendialog1.Execute then
    begin
      exit;
    end;

    projdir := string(extractFiledir(frmmain.modelini.FileName));

		//frmstatus.StatusBox('This may take several minutes','','Retreiving XP results');
		// Call external DLL here to extract results
		rcxp := getxpresults(pchar(opendialog1.FileName),pchar(projdir + '\mdbs\ModelResults.mdb'));
		if rcxp >= 0 then
		begin
			If rcxp > 0 then showmessage(Format('An results error has occurred (%d)',[rcxp]));
			If rcxp = 0 then showmessage('One or more results tables are empty');
			myoleac := unassigned;
			exit;
		end;

		// Update direct subcatchment table with results
		myoleac := createoleobject('Access.Application');
		myoleac.OpenCurrentDatabase(projdir + '\mdbs\ModelResults.mdb');
		myoleac.currentdb.execute('Query01_initialize');
		myoleac.currentdb.execute('Query02_update');
		myoleac.CloseCurrentDatabase;
		myoleac := unassigned;

		// AMM 9/12/2005 Refresh MapInfo TAB files

		// 1. Delete files first to force MapInfo to recreate tables for registration
		DeleteFile(projdir+'\mdbs\tableE09.aid');
		DeleteFile(projdir+'\mdbs\tableE09.ind');
		DeleteFile(projdir+'\mdbs\tableE09.tab');
		DeleteFile(projdir+'\mdbs\tableE10.aid');
		DeleteFile(projdir+'\mdbs\tableE10.ind');
		DeleteFile(projdir+'\mdbs\tableE10.tab');
		DeleteFile(projdir+'\mdbs\tableE20.aid');
		DeleteFile(projdir+'\mdbs\tableE20.ind');
		DeleteFile(projdir+'\mdbs\tableE20.tab');
		DeleteFile(projdir+'\nodes\BFNodesE09.dat');
		DeleteFile(projdir+'\nodes\BFNodesE09.id');
		DeleteFile(projdir+'\nodes\BFNodesE09.ind');
		DeleteFile(projdir+'\nodes\BFNodesE09.map');
		DeleteFile(projdir+'\nodes\BFNodesE09.tab');
		DeleteFile(projdir+'\nodes\BFLinksE10.dat');
		DeleteFile(projdir+'\nodes\BFLinksE10.id');
		DeleteFile(projdir+'\nodes\BFLinksE10.ind');
		DeleteFile(projdir+'\nodes\BFLinksE10.map');
		DeleteFile(projdir+'\nodes\BFLinksE10.tab');

		// 2. Register the tables
		myoleac := createoleobject('MapInfo.Application');
		if VarIsNull(myoleac) then
		begin
			ShowMessage('MapInfo is not installed');
			Exit;
		end;
		myoleac.Do('Register Table "'+ projdir + '\mdbs\ModelResults.mdb" '+
							 'Type ACCESS Table "tableE09" '+
							 'Into "'+ projdir + '\mdbs\tableE09.tab"');
		myoleac.Do('Open Table "'+ projdir + '\mdbs\tableE09.tab" As tableE09 Interactive');
		myoleac.Do('Register Table "'+ projdir + '\mdbs\ModelResults.mdb" '+
							 'Type ACCESS Table "tableE10" '+
							 'Into "'+ projdir + '\mdbs\tableE10.tab"');
		myoleac.Do('Open Table "'+ projdir + '\mdbs\tableE10.tab" As tableE10 Interactive');
		myoleac.Do('Register Table "'+ projdir + '\mdbs\ModelResults.mdb" '+
							 'Type ACCESS Table "tableE20" '+
							 'Into "'+ projdir + '\mdbs\tableE20.tab"');
		myoleac.Do('Open Table "'+ projdir + '\nodes\mdl_nodes_ac.tab" As mdl_Nodes_ac Interactive');
		myoleac.Do('Open Table "'+ projdir + '\links\mdl_links_ac.tab" As mdl_Links_ac Interactive');
		myoleac.Do('Select *  from tableE09, mdl_Nodes_ac '+
							 'where  tableE09.NodeName = mdl_Nodes_ac.Node '+
							 'into ResultsNodes noselect ');
		myoleac.Do('Select *  from  mdl_Links_ac , tableE10 '+
							 'where mdl_Links_ac.SimLinkID = tableE10.CondName '+
							 'into ResultsLinks noselect');
		myoleac.Do('Commit Table ResultsNodes As Str$("' + projdir + '\nodes\BFNodesE09.TAB") '+
							 'TYPE NATIVE Charset "WindowsLatin1"');
		myoleac.Do('Commit Table ResultsLinks As Str$("' + projdir + '\nodes\BFLinksE10.TAB") '+
							 'TYPE NATIVE Charset "WindowsLatin1"');
		myoleac.Do('Close table tableE09');
		myoleac.Do('Close table tableE10');
		myoleac := Unassigned;

	except
		on E: eoleexception do
		begin
			myoleac := unassigned;
			showmessage(E.Message);
			exit;
		end;
	else
		raise Exception.create('error running queries from ModelAssemble table');
  end;
  showmessage('XP results successfully extracted');

end;




procedure TfrmModelResults.Button2Click(Sender: TObject);
begin
  inherited;
  frmmain.actBuildModelsExecute(Sender)
end;

end.
