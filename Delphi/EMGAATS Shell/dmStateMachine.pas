unit dmStateMachine;

interface

uses
  SysUtils, Classes, inifiles;

type
  TmdlChangeStatus = (mdlNull, mdlUnchanged, mdlChanged);
  { mdlNull indicates you are not in a model
    mdlUnchanged indicates you have enterd a model but have not changed anything
    mdlChanges indicates a you have modified
   }
  TStateMachine = class(TDataModule)
	private

		fHasPipeSystem: Boolean;
		fHasLateralsAndParcels: Boolean;
		fHasSubcatchments: Boolean;
    fmdlChangeStatus : TmdlChangeStatus;

    function GetHasBoundaries: Boolean;
    function GetHasChangedRoots : Boolean;
    function GetHasChangedStops: Boolean;
    function GetHasChangedBoundaries: Boolean;

//		function GetHasWorkingDirectory: Boolean;


    procedure SetHasLateralsAndParcels(const Value: Boolean);
    procedure SetHasPipeSystem(const Value: Boolean);
    procedure SetHasSubcatchments(const Value: Boolean);
    procedure SetmdlChangeStatus(newstatus :TmdlChangeStatus);
		{ Private declarations }
	public

		{ Public declarations }

    property mdlChangeStatus: TmdlChangeStatus read fmdlChangeStatus write SetmdlChangeStatus;

    { HasChangedRoots:
      When leaving the Modify boundary form -- check if model roots
      are different than ini roots. Change in roots or stops requires a
      model rebuild -- so pipes, laterals, and subcatchments will be deleted
    }
    property HasChangedRoots: Boolean read GetHasChangedRoots;
    property HasChangedStops: Boolean read GetHasChangedStops;


		procedure InitializeStates;
//		property HasWorkingDirectory: Boolean read GetHasWorkingDirectory;
		property HasPipeSystem: Boolean read fHasPipeSystem write SetHasPipeSystem;
		property HasLateralsAndParcels: Boolean read fHasLateralsAndParcels write SetHasLateralsAndParcels;
		property HasSubcatchments: Boolean read fHasSubcatchments write SetHasSubcatchments;
		property HasBoundaries: Boolean read GetHasBoundaries;
    property HasChangedBoundaries: Boolean read GetHasChangedBoundaries;
  end;

var
  StateMachine: TStateMachine;

  gloINIsections : Thashedstringlist;
  gloINIstring : Thashedstringlist;


implementation

uses fModifyModelBoundaries, StdCtrls,
  fBuildPipeSystem, fBuildLateralsAndParcels, fBuildSubcatchments,
  fBuildModels, fMain, GlobalConstants, dmmapinfo,
  dialogs;

{$R *.dfm}

{ TDataModule1 }


function Tstatemachine.Gethaschangedboundaries: boolean;
begin
  result := gethaschangedroots or gethaschangedstops;
end;


function TStateMachine.GetHasChangedRoots: Boolean;
var
  INIRoots : Thashedstringlist;
  FormRoots : Thashedstringlist;
  inistring, frmstring : string;
  i : integer;
begin
  // if not assigned than not possible to have changed roots
  INIRoots := thashedstringlist.Create;
  INIroots.Sorted := False;
  INIroots.Duplicates := dupignore;

  FormRoots := thashedstringlist.Create;
  INIroots.Sorted := False;
  FormRoots.Duplicates := dupignore;

  frmmain.modelini.ReadSection(strMdlRootSection,INIRoots);

  for i := frmModifyModelBoundaries.lstRootPipes.items.Count - 1 downto 0 do
  begin
    FormRoots.Add(frmModifyModelBoundaries.lstRootPipes.Items[i]);
  end;

  iniroots.Sort;
  formroots.Sort;

  inistring := '';
  frmstring := '';
{  for i := 0 to iniroots.count -1 do  inistring := inistring + ' ' + iniroots[i];
  for i := 0 to formroots.count -1 do  frmstring := frmstring + ' ' + formroots[i];

  showmessage( inistring + #13 + frmstring);
}
  if iniroots.Equals(FormRoots) then result := false
  else result := true;

  INIroots.free;
  FormRoots.free;

end;

function TStateMachine.GetHasChangedStops: Boolean;
var
  INIStops : Thashedstringlist;
  FormStops : Thashedstringlist;
  inistring, frmstring : string;
  i : integer;
begin
  // if not assigned than not possible to have changed roots
  INIStops := thashedstringlist.Create;
  INIStops.Sorted := False;
  INIStops.Duplicates := dupignore;

  FormStops := thashedstringlist.Create;
  INIStops.Sorted := False;
  FormStops.Duplicates := dupignore;

  frmmain.modelini.ReadSection(strMdlStopSection,INIStops);

  for i := frmModifyModelBoundaries.lstStopPipes.items.Count - 1 downto 0 do
  begin
    FormStops.Add(frmModifyModelBoundaries.lstStopPipes.Items[i]);
  end;

  iniStops.Sort;
  formStops.Sort;

  inistring := '';
  frmstring := '';
{  for i := 0 to iniroots.count -1 do  inistring := inistring + ' ' + iniroots[i];
  for i := 0 to formroots.count -1 do  frmstring := frmstring + ' ' + formroots[i];

  showmessage( inistring + #13 + frmstring);
}
  if inistops.Equals(FormStops) then result := false
  else result := true;

  INIStops.free;
  FormStops.free;

end;


function TStateMachine.GetHasBoundaries: Boolean;
begin
	if Assigned(frmModifyModelBoundaries) then
	begin
		Result := frmModifyModelBoundaries.lstRootPipes.Items.Count > 0;
	end
	else
	begin
		Result := False;
	end;
end;


{
function TStateMachine.GetHasWorkingDirectory: Boolean;
begin
	if Assigned(frmChooseAModel) then
		Result := Length(frmChooseAModel.edtWorkingDirectory.Text) > 0
	else
		Result := False;
end;
}
procedure TStateMachine.InitializeStates;
begin
	// Initialize Stuff here

  if uppercase(frmmain.ModelIni.ReadString(strmdlstate,'HasPipeSystem','FALSE')) = 'FALSE' then
  	HasPipeSystem := False
  else
  	HasPipeSystem := TRUE;

  frmmain.ModelIni.WriteString(strmdlstate,'HasPipeSystem',booltostr(HasPipeSystem, true));

  if uppercase(frmmain.ModelIni.ReadString(strmdlstate,'HasLateralsAndParcels','FALSE')) = 'FALSE' then
  	fHasLateralsAndParcels := False
  else
  	fHasLateralsAndParcels := TRUE;

  frmmain.ModelIni.WriteString(strmdlstate,'HasLateralsAndParcels',booltostr(HasLateralsAndParcels, True));


  if uppercase(frmmain.ModelIni.ReadString(strmdlstate,'HasSubcatchments','FALSE')) = 'FALSE' then
  	fHasSubcatchments := False
  else
  	fHasSubcatchments := TRUE;

  frmmain.ModelIni.WriteString(strmdlstate,'HasSubcatchments',booltostr(HasSubcatchments, true));

  mdlchangestatus := mdlUnchanged;


	// If opening model, change depending on files in directory
	{ TODO -oDJC -cInitialization :
Initialize States: when opening a model, change
state machine depending on files that exist }
end;

procedure TStateMachine.SetmdlChangeStatus(newstatus : TmdlChangeStatus);
begin
  case newstatus of
  mdlNull:
    begin
      frmmain.caption := 'EMGAATS Framework ' + emgaatsversion + ', '+  emgaatsversioncomment;
      if assigned(frmmain.modelini) then frmmain.modelini.clear;
    end;
  else
    begin
      if assigned(frmmain.modelini) then
        frmmain.caption := ''+ frmmain.dlgSelectDirectory.SelectedPathName
      else
        showmessage('unreached condition in TStateMachine.SetmdlChangeStatus ' + #13 + 
                    'frmmain.modelini is not assigned');
    end;
  end;

  fmdlChangeStatus := newstatus;
end;

procedure TStateMachine.SetHasLateralsAndParcels(const Value: Boolean);
begin
	if fHasLateralsAndParcels <> Value then
	begin
    mdlchangeStatus := mdlChanged;
		if fHasLateralsAndParcels = True then
    begin
    // Invalidate subsequent build activities
    //showmessage('TStateMachine.SetHasLateralsAndParcels delete all from laterals and parcels');
			HasSubcatchments := False
    end
		else
		begin
			// Reset frmBuildLateralsAndParcels check boxes
			with frmBuildLateralsAndParcels do
			begin
				chkTraceLateralsAndParcels.Checked := True;
				chkCheckLateralsAndParcels.Checked := False;
			end;
		end;
		fHasLateralsAndParcels := Value;

    frmmain.ModelIni.WriteString(strmdlstate,'HasLateralsAndParcels',booltostr(value, true));

	end;
end;

procedure TStateMachine.SetHasPipeSystem(const Value: Boolean);
begin
	if fHasPipeSystem <> Value then
	begin
    mdlchangeStatus := mdlChanged;
		if fHasPipeSystem = True then // Invalidate subsequent build activities
    // New Value = False, Old Value = True
    // Reset frmBuildPipeSystem check boxes
    begin
  		HasLateralsAndParcels := False;
      with frmBuildPipeSystem do
      begin
        actpipesystemqueries.Enabled := false;
      end;
    end

		else
    // New Value = True, Old Value = False
    // Reset frmBuildPipeSystem check boxes
		begin
    	with frmBuildPipeSystem do
			begin
				chkTracePipeSystem.Checked := True;
				chkViewProfiles.Checked := False;
				chkPipeSystemQueries.Checked := False;
				chkSpecifyDiversions.Checked := False;

        actpipesystemqueries.Enabled := true;

			end;
		end;
    frmmain.ModelIni.WriteString(strmdlstate,'HasPipeSystem',booltostr(value, true));

		fHasPipeSystem := Value;
	end;
end;

procedure TStateMachine.SetHasSubcatchments(const Value: Boolean);
begin
	if fHasSubcatchments <> Value then
	begin
    mdlchangeStatus := mdlChanged;
		if fHasSubcatchments = True then
		else
		begin
			// Reset frmBuildSubcatchments check boxes
			with frmBuildSubcatchments do
			begin
				chkTraceSubcatchments.Checked := True;
				chkCheckSubcatchments.Checked := False;
				chkCreateMapLinkingSubcatchmentsToNodes.Checked := False;
      end;
		end;
		fHasSubcatchments := Value;

    frmmain.ModelIni.WriteString(strmdlstate,'HasSubcatchments',booltostr(value, true));

	end;
end;

end.
