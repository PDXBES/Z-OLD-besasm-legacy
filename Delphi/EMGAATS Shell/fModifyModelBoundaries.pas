{==fModifyModelBoundaries Unit==================================================

	Form for changing model boundaries (start/stop pipes)

	Revision History
	2.1    03/25/2003 (AMM) Revised copy start/stops from another model
	1.5    ?          ?
	1.0    11/01/2002 Initial release

===============================================================================}
unit fModifyModelBoundaries;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, fLabeledChild, StdCtrls, ActnList,
  GlobalConstants, Grids, inifiles, RzShellDialogs, Menus, RzLabel, RzBckgnd,
  ExtCtrls, RzPanel;

type
  TfrmModifyModelBoundaries = class(TfrmLabeledChild)
    lstRootPipes: TListBox;
    Label2: TLabel;
    btnAddRootPipe: TButton;
    btnCopyRootPipes: TButton;
    btnDeleteRootPipe: TButton;
    btnBuildPipeSystem: TButton;
    lstStopPipes: TListBox;
    Label3: TLabel;
    btnAddStopPipe: TButton;
    btnDeleteStopPipe: TButton;
		btnSpecifyBoundaryConditions: TButton;
    btnSpecifyStopPipeInflows: TButton;
    btnBuildModels: TButton;
    Label4: TLabel;
    ActionList1: TActionList;
    actAddRootPipe: TAction;
    actDelRootPipes: TAction;
    actCopyRootsFromAnotherModel: TAction;
    actSpecifyBoundaryConditions: TAction;
    actAddStopPipe: TAction;
    actDelStopPipes: TAction;
    actSpecifyInflowsAtStopPipes: TAction;
    StringGrid1: TStringGrid;
    dlgCopyDirectory: TRzSelectFolderDialog;
    Label5: TLabel;
    procedure actAddRootPipeExecute(Sender: TObject);
    procedure actDelRootPipesExecute(Sender: TObject);
    procedure actCopyRootsFromAnotherModelExecute(Sender: TObject);
    procedure actSpecifyBoundaryConditionsExecute(Sender: TObject);
    procedure actAddStopPipeExecute(Sender: TObject);
    procedure actDelStopPipesExecute(Sender: TObject);
		procedure actSpecifyInflowsAtStopPipesExecute(Sender: TObject);
		procedure actDelRootPipesUpdate(Sender: TObject);
		procedure actDelStopPipesUpdate(Sender: TObject);
		procedure actSpecifyBoundaryConditionsUpdate(Sender: TObject);
		procedure actSpecifyInflowsAtStopPipesUpdate(Sender: TObject);
		procedure FormShow(Sender: TObject);
		procedure FormHide(Sender: TObject);
		procedure FormInitialize();
    procedure btnBuildModelsClick(Sender: TObject);
	private

//    myHasChangedBoundry : boolean;
		{ Private declarations }
	public
		{ Public declarations }
		procedure ReloadModelTerminators(AModelDirectory: String);
	end;

var
	frmModifyModelBoundaries: TfrmModifyModelBoundaries;

implementation

uses fMain, fBuildModels, dmstatemachine, dCopyBoundaries;

{ TODO -oAMM -cModel Functionality :
If working from existing model, update check boxes depending on state
(use model.ini) }

{$R *.dfm}

procedure TfrmModifyModelBoundaries.actAddRootPipeExecute(Sender: TObject);
var
	ARootPipe: String;
begin
	inherited;

	ARootPipe := InputBox('Specify Root Pipe', 'Type in a root pipe', '');
	lstRootPipes.AddItem(ARootPipe, nil);
end;

procedure TfrmModifyModelBoundaries.actDelRootPipesExecute(
	Sender: TObject);
begin
	inherited;

	// if nothing is selected, delete the last one
	if lstRootPipes.SelCount < 1 then
	begin
		if lstRootPipes.Count > 0 then
			lstRootPipes.Items.Delete(lstRootPipes.Count-1);
	end
	else
		lstRootPipes.DeleteSelected;
end;

procedure TfrmModifyModelBoundaries.actCopyRootsFromAnotherModelExecute(
	Sender: TObject);
begin
	inherited;

{	dlgCopyDirectory.SelectedPathName := frmMain.dlgSelectDirectory.SelectedPathName;

	dlgCopyDirectory.Title := 'Select Model Directory to Copy Roots and Stops';

	if not dlgCopyDirectory.Execute then
	begin
		exit;
	end;}

	if dlgCopyBoundaries.ShowModal = mrOK then
		ReloadModelTerminators(dlgCopyBoundaries.cmbModelDirectory.Text);
end;

procedure TfrmModifyModelBoundaries.actSpecifyBoundaryConditionsExecute(
	Sender: TObject);
begin
	inherited;
	{ TODO -oDJC -cModel Functionality : Specify Boundary Conditions for root nodes: launch code }
end;

procedure TfrmModifyModelBoundaries.actAddStopPipeExecute(Sender: TObject);
var
	AStopPipe: String;
begin
	inherited;

	AStopPipe := InputBox('Specify Stop Pipe', 'Type in a stop pipe', '');
	lstStopPipes.AddItem(AStopPipe, nil);
end;

procedure TfrmModifyModelBoundaries.actDelStopPipesExecute(
	Sender: TObject);
begin
	inherited;

	// if nothing is selected, delete the last one
	if lstStopPipes.SelCount < 1 then
	begin
		if lstStopPipes.Count > 0 then
			lstStopPipes.Items.Delete(lstStopPipes.Count-1);
	end
	else
		lstStopPipes.DeleteSelected;
end;

procedure TfrmModifyModelBoundaries.actSpecifyInflowsAtStopPipesExecute(
	Sender: TObject);
begin
	inherited;
	{ TODO -oDJC -cModel Functionality : Specify Inflows at Stop Pipes: launch code }
end;

procedure TfrmModifyModelBoundaries.actDelRootPipesUpdate(Sender: TObject);
begin
	inherited;
	actDelRootPipes.Enabled := lstRootPipes.Count > 0;
end;

procedure TfrmModifyModelBoundaries.actDelStopPipesUpdate(Sender: TObject);
begin
	inherited;
	actDelStopPipes.Enabled := lstStopPipes.Count > 0;
end;

procedure TfrmModifyModelBoundaries.actSpecifyBoundaryConditionsUpdate(
	Sender: TObject);
begin
	inherited;
	actSpecifyBoundaryConditions.Enabled := lstRootPipes.Count > 0;
end;

procedure TfrmModifyModelBoundaries.actSpecifyInflowsAtStopPipesUpdate(
	Sender: TObject);
begin
	inherited;
	actSpecifyInflowsAtStopPipes.Enabled := lstStopPipes.Count > 0;
end;

procedure TfrmModifyModelBoundaries.FormInitialize();
begin
  frmmain.ModelIni.ReadSection(strmdlrootsection,lstrootpipes.Items);
  frmmain.ModelIni.ReadSection(strmdlstopsection,lststoppipes.Items);
end;

procedure TfrmModifyModelBoundaries.FormShow(Sender: TObject);
begin
  inherited;
//  myHasChangedBoundry := false;

  FormInitialize();

end;


procedure TfrmModifyModelBoundaries.FormHide(Sender: TObject);
var
  i : integer;
begin
  inherited;

  if statemachine.HasChangedBoundaries then
  begin
    case MessageDlg('New boundary conditions require retrace' + #13 +
      'Continue with boundary changes?', mtWarning, [mbYes, mbCancel], 0) of
			mrYes: // User wants to overwrite
			begin
        showmessage('STUB: need to delete existing stuff here');
        frmmain.modelini.EraseSection(strMdlRootSection);
        for i := frmModifyModelBoundaries.lstRootPipes.items.Count - 1 downto 0 do
        begin
          frmmain.modelini.WriteString(strMdlRootSection,frmModifyModelBoundaries.lstRootPipes.Items[i],'');
        end;

        frmmain.modelini.EraseSection(strMdlStopSection);
        for i := frmModifyModelBoundaries.lstStopPipes.items.Count - 1 downto 0 do
        begin
          frmmain.modelini.WriteString(strMdlStopSection,frmModifyModelBoundaries.lstStopPipes.Items[i],'');
        end;

        statemachine.HasPipeSystem := false;

        frmmain.modelini.UpdateFile;
      end;
			mrCancel: // User doesn't want to overwrite
			begin
				 showmessage('cancel changes');
         FormInitialize;
			end;
    end; //case
  end; //if

end;
procedure TfrmModifyModelBoundaries.ReloadModelTerminators(
  AModelDirectory: String);
var
	CopyIni : tmeminifile;
begin
	if FileExists(AModelDirectory + '\' + ModelIniFileName) then
	begin


		lstStoppipes.Clear;
		lstrootpipes.Clear;

		CopyIni := tmeminifile.Create(AModelDirectory + '\' + ModelIniFileName);
		Copyini.ReadSection(strMdlStopSection,lstStoppipes.Items);
		Copyini.ReadSection(strMdlRootSection,lstRootpipes.items);
		refresh;

		copyini.Free;
	end;
end;

procedure TfrmModifyModelBoundaries.btnBuildModelsClick(Sender: TObject);
begin
  inherited;
  frmmain.actBuildModelsExecute(Sender)
end;

end.
