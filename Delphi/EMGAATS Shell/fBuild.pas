unit fBuild;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, fLabeledChild, cxStyles, cxGraphics, cxEdit, StdCtrls, RzLstBox,
  RzChkLst, RzAnimtr, cxControls, cxInplaceContainer, cxVGrid, ExtCtrls,
  RzPanel, RzButton, RzCmboBx, RzLabel, RzPrgres, ImgList, RzShellDialogs,
  RzEdit, RzCommon, cxDropDownEdit, ActnList, XPStyleActnCtrls, ActnMan, Menus,
  uEMGAATSModelCommands, cxTextEdit, pngimage, RzBckgnd, RzSplit, Buttons,
  PngSpeedButton, cxCheckComboBox, DB, ADODB, cxMemo, cxContainer, cxMaskEdit,
  RzErrHnd, uEMGAATSTypes, cxCheckBox;

type
  ///<summary>
  ///  Form for building models
  ///</summary>
  TfrmBuild = class(TfrmLabeledChild)
    pnlProgress: TRzPanel;
    prgProgress: TRzProgressBar;
    pnlBuild: TRzPanel;
    lblProgress: TRzLabel;
    btnCancelBuild: TRzButton;
    dlgSelectFolder: TRzSelectFolderDialog;
    pnlBuildActionsHolder: TRzPanel;
    ActionManager1: TActionManager;
    actLoadModel: TAction;
    actGotoBuildReport: TAction;
    actPresetBuildAll: TAction;
    mnuPopup: TPopupMenu;
    mnuPresetBuildAll: TMenuItem;
    actPresetRecalculateHydrology: TAction;
    mnuPresetRecalculateHydrology: TMenuItem;
    actPresetIndividualOptions: TAction;
    Individualoptions1: TMenuItem;
    Gotobuildreport1: TMenuItem;
    RzSizePanel1: TRzSizePanel;
    grpbBuildOptions: TRzGroupBox;
    RzPanel1: TRzPanel;
    RzPanel2: TRzPanel;
    RzSizePanel2: TRzSizePanel;
    vgrdBuildOptions: TcxVerticalGrid;
    vgrdTimeFrame: TcxEditorRow;
    vgrdTitle: TcxEditorRow;
    vgrdStorms: TcxEditorRow;
    vgrdRunoffBaseFileName: TcxEditorRow;
    vgrdEngineBaseFileName: TcxEditorRow;
    vgrdEngines: TcxEditorRow;
    RzPanel3: TRzPanel;
    grpbModelBoundaries: TRzGroupBox;
    RzGridPanel1: TRzGridPanel;
    RzLabel2: TRzLabel;
    RzLabel3: TRzLabel;
    memRootPipes: TRzMemo;
    memStopPipes: TRzMemo;
    RzPanel6: TRzPanel;
    btnCopyModelBoundaries: TRzButton;
    btnHome: TPngSpeedButton;
    adoConnection: TADOConnection;
    adoStorms: TADOTable;
    vgrdDescription: TcxEditorRow;
    RzPanel5: TRzPanel;
    RzLabel1: TRzLabel;
    cmbModelDirectory: TRzMRUComboBox;
    btnBrowseModelDirectory: TRzButton;
    btnLoad: TRzButton;
    RzLabel4: TRzLabel;
    actPresetDeployModelFiles: TAction;
    Deploymodelfiles1: TMenuItem;
    actPresetCreatePreset: TAction;
    Createapreset1: TMenuItem;
    lblStats: TRzLabel;
    actPresetTraceNetworkOnly: TAction;
    racenetworkonly1: TMenuItem;
    RzGroupBox1: TRzGroupBox;
    mnuBuildPresetsMenu: TRzMenuButton;
    lblPreset: TRzLabel;
    RzPanel7: TRzPanel;
    chklstBuildOptions: TRzCheckList;
    actPresetDeployAlternative: TAction;
    Deployalternative1: TMenuItem;
    actPresetBuildAllStormwaterControls: TAction;
    mnuPresetBuildStormwaterControls: TMenuItem;
    vgrdUseBaseflow: TcxEditorRow;
    RzPanel8: TRzPanel;
    btnHomeAlt: TRzButton;
    pnlBuildActions: TRzPanel;
    btnGotoBuildReport: TRzButton;
    btnBuildModel: TRzButton;
    actPresetBuildStreetStormwaterControls: TAction;
    Buildstreetstormwatercontrols1: TMenuItem;
    vgrdTraceStormwater: TcxEditorRow;
    vgrdRemoveSanitaryUponDeploy: TcxEditorRow;
    procedure btnBrowseModelDirectoryClick(Sender: TObject);
    procedure btnBuildModelClick(Sender: TObject);
    procedure btnCancelBuildClick(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure btnCopyModelBoundariesClick(Sender: TObject);
    procedure cmbModelDirectoryChange(Sender: TObject);
    procedure btnLoadClick(Sender: TObject);
    procedure actLoadModelUpdate(Sender: TObject);
    procedure cmbModelDirectorySelect(Sender: TObject);
    procedure actGotoBuildReportUpdate(Sender: TObject);
    procedure actGotoBuildReportExecute(Sender: TObject);
    procedure actPresetBuildAllExecute(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure actLoadModelExecute(Sender: TObject);
    procedure cmbModelDirectoryEnter(Sender: TObject);
    procedure cmbModelDirectoryExit(Sender: TObject);
    procedure memRootPipesEnter(Sender: TObject);
    procedure memRootPipesExit(Sender: TObject);
    procedure memStopPipesEnter(Sender: TObject);
    procedure memStopPipesExit(Sender: TObject);
    procedure actPresetRecalculateHydrologyExecute(Sender: TObject);
    procedure actPresetIndividualOptionsExecute(Sender: TObject);
    procedure chklstBuildOptionsChange(Sender: TObject; Index: Integer;
      NewState: TCheckBoxState);
    procedure vgrdBuildOptionsDrawBackground(Sender: TObject;
      ACanvas: TcxCanvas; const R: TRect; const AViewParams: TcxViewParams;
      var Done: Boolean);
    procedure btnHomeClick(Sender: TObject);
    procedure actPresetDeployModelFilesExecute(Sender: TObject);
    procedure actPresetTraceNetworkOnlyExecute(Sender: TObject);
    procedure actPresetDeployAlternativeExecute(Sender: TObject);
    procedure actPresetBuildAllStormwaterControlsExecute(Sender: TObject);
    procedure actPresetBuildStreetStormwaterControlsExecute(Sender: TObject);
  private
    { Private declarations }

    ///<summary>
    ///   Flag for whether model is loaded, for UI activity
    ///</summary>
    ModelLoaded: Boolean;

    ///<summary>
    ///  List of TEMGAATSModelCommands to run when user executes build
    ///</summary>
    fCommandList: TEMGAATSModelCommandList;

    ///<summary>
    ///  Temporary list of roots saved when copying from another model
    ///</summary>
    SavedRoots: TStringList;

    ///<summary>
    ///  Temporary list of stops saved when copying from another model
    ///</summary>
    SavedStops: TStringList;

    ///<summary>
    ///  Abort signal (simple boolean field as class) used for interrupting
    ///  model build.
    ///</summary>
    fAbortSignal: TEMGAATSCommandAbortSignal;

    ///<summary>
    ///  Check if a model exists and if so, show a dialog to determine what
    ///  user wants to do
    ///</summary>
    procedure CheckForExistingModel;

    ///<summary>
    ///  Enables appropriate parts of the UI
    ///</summary>
    procedure EnableBuildUI(Enable: Boolean);

    ///<summary>
    ///  Initializes the UI when the form is opened
    ///</summary>
    procedure InitializeUI;

    ///<summary>
    ///  Checks certain UI elements and changes color and text if there is no
    ///  user entry available
    ///</summary>
    procedure CheckUIPrompts;

    ///<summary>
    ///  Checks the root and stop pipe lists to see if there is an entry
    ///  available; if not, provide a user prompt
    ///</summary>
    procedure CheckPipeListPrompt(AMemoControl: TRzMemo);

    ///<summary>
    ///  Checks the model directory box to see if there is an entry available;
    ///  if not, provide a user prompt
    ///</summary>
    procedure CheckModelDirectoryPrompt;

    ///<summary>
    ///  Fills the storm list in Build Options according to the model's
    ///  LookupTables.Storms table
    ///</summary>
    procedure FillStormsCheckComboList;

    ///<summary>
    ///  Creates the string representation of the storm list for storing in the
    ///  model config file
    ///</summary>
    function StormListString(AValue: String): String;

    ///<summary>
    ///  Checks on the storms in the storm list in Build Options from a given
    ///  storm list constructed by StormListString
    ///</summary>
    procedure CheckBoxesFromStormListString(AStormList: String);

    ///<summary>
    ///  Message for updating the statistics in the status box during build
    ///</summary>
    procedure OnUpdateStats(var Msg: TMessage); message EMG_UPDATE_STATS;
  public
    { Public declarations }

    ///<summary>
    ///  Loads a model into the CurrentModel global object from a directory and
    /// remembers the model boundaries if available and desired
    ///</summary>
    procedure LoadModel(RememberBoundaries: Boolean = True; IsNewModel: Boolean = False);

    ///<summary>
    ///  Unloads a model from memory from the CurrentModel global object
    ///</summary>
    procedure UnloadModel;

    ///<summary>
    ///  Cleans up a model directory and saves the root and stop pipes if
    ///  desired
    ///</summary>
    ///<param name="SaveBoundaries">
    ///  Boolean. True (default) to save the boundaries before cleaning up
    ///  model directory
    ///</param>
    procedure CleanModelDirectory(SaveBoundaries: Boolean = True);

  end;

var
  frmBuild: TfrmBuild;

implementation

uses fMain, uEMGAATSModel, uEMGWorkbenchManager, uEMGAATSSystemConfig,
  uUtilities, GlobalConstants, dModelExists, fBuildReport, fChild, DateUtils,
  CodeSiteLogging, fWelcome, StStrL, StrUtils, uEMGAATSModelConfig;

resourcestring
  PromptModelDirectory = 'Type a full path and filename, or click Browse...';
  PromptLinks = 'Type in links, one per line';

{$R *.dfm}

procedure TfrmBuild.actPresetDeployAlternativeExecute(Sender: TObject);
var
  i: Integer;
begin
  fCommandList.Clear;
  fCommandList.Add(TOpenMasterModelDataCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TRelinkModelDataCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TBuildStudyAreaBoundaryCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TRunAllStormwaterControlsCommand.Create(CurrentModel, lblProgress,
    prgProgress, fAbortSignal));
  fCommandList.Add(TRunStreetStormwaterControlsCommand.Create(CurrentModel, lblProgress,
    prgProgress, fAbortSignal));
  fCommandList.Add(TCalculateHydrologyCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TDeployRunoffCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TCalculateHydraulicsCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TDeployEngineFileCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TCreateQCWorkspacesCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));

  chklstBuildOptions.Clear;
  lblPreset.Caption := 'Deploy alternative';

  for i := 0 to fCommandList.Count - 1 do
  begin
    if (fCommandList.Items[i].Name = 'Run all stormwater controls alternatives calculations') or
      (fCommandList.Items[i].Name = 'Run street stormwater controls alternatives calculations') then
    begin
      fCommandList[i].Enabled := False;
      chklstBuildOptions.AddEx(fCommandList.Items[i].Name, False);
    end
    else
    begin
      fCommandList[i].Enabled := True;
      chklstBuildOptions.AddEx(fCommandList.Items[i].Name, True);
    end;
  end;

end;

procedure TfrmBuild.actGotoBuildReportExecute(Sender: TObject);
begin
  if not Assigned(frmBuildReport) then
  begin
    frmBuildReport := frmMain.DisplayForm(TfrmBuildReport) as TfrmBuildReport;
    frmBuildReport.InitReport(CurrentModel);
  end
  else
    frmBuildReport.Show;
end;

procedure TfrmBuild.actGotoBuildReportUpdate(Sender: TObject);
begin
  actGotoBuildReport.Enabled := ModelLoaded;
end;

procedure TfrmBuild.actLoadModelExecute(Sender: TObject);
begin
  LoadModel(True);
  CheckUIPrompts;
end;

procedure TfrmBuild.actLoadModelUpdate(Sender: TObject);
begin
  actLoadModel.Enabled := not ModelLoaded and (cmbModelDirectory.Text <> '') and
    (cmbModelDirectory.Text <> PromptModelDirectory);
end;

procedure TfrmBuild.actPresetBuildAllExecute(Sender: TObject);
var
  i: Integer;
  AddedIndex: Integer;
begin
  fCommandList.Clear;
  AddedIndex := fCommandList.Add(TBuildNewModelCommand.Create(CurrentModel,
    lblProgress, prgProgress, fAbortSignal));
  fCommandList[AddedIndex].Enabled := True;

  chklstBuildOptions.Clear;
  lblPreset.Caption := 'Build all';
  for i := 0 to fCommandList.Count - 1 do
  begin
    chklstBuildOptions.AddEx(fCommandList.Items[i].Name, True);
    fCommandList[i].Enabled := True;
  end;
end;

procedure TfrmBuild.actPresetBuildAllStormwaterControlsExecute(Sender: TObject);
var
  i: Integer;
begin
  inherited;

  fCommandList.Clear;
  fCommandList.Add(TOpenMasterModelDataCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TRelinkModelDataCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TBuildAllStormwaterControlsCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TDeployRunoffCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));

  chklstBuildOptions.Clear;
  lblPreset.Caption := 'Build all stormwater controls';
  for i := 0 to fCommandList.Count - 1 do
  begin
    chklstBuildOptions.AddEx(fCommandList.Items[i].Name, True);
    fCommandList[i].Enabled := True;
  end;
end;

procedure TfrmBuild.actPresetBuildStreetStormwaterControlsExecute(
  Sender: TObject);
var
  i: Integer;
begin
  inherited;

  fCommandList.Clear;
  fCommandList.Add(TOpenMasterModelDataCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TRelinkModelDataCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TBuildStreetStormwaterControlsCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TDeployRunoffCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));

  chklstBuildOptions.Clear;
  lblPreset.Caption := 'Build street stormwater controls';
  for i := 0 to fCommandList.Count - 1 do
  begin
    chklstBuildOptions.AddEx(fCommandList.Items[i].Name, True);
    fCommandList[i].Enabled := True;
  end;
end;

procedure TfrmBuild.actPresetDeployModelFilesExecute(Sender: TObject);
var
  i: Integer;
begin
  fCommandList.Clear;
  fCommandList.Add(TOpenMasterModelDataCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TRelinkModelDataCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TCalculateHydrologyCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TDeployRunoffCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TDeployEngineFileCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));

  chklstBuildOptions.Clear;
  lblPreset.Caption := 'Deploy model files';
  for i := 0 to fCommandList.Count - 1 do
  begin
    chklstBuildOptions.AddEx(fCommandList.Items[i].Name, True);
    fCommandList[i].Enabled := True;
  end;
end;

procedure TfrmBuild.actPresetIndividualOptionsExecute(Sender: TObject);
var
  i: Integer;
begin
  fCommandList.Clear;
  fCommandList.Add(TOpenMasterModelDataCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TRelinkModelDataCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TBuildEmptyModelCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TConfigureModelForBuildingCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TBuildNetworkCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TBuildDirectSubcatchmentsCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TBuildSurfaceSubcatchmentsCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TBuildStudyAreaBoundaryCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TRunAllStormwaterControlsCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TRunStreetStormwaterControlsCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TMergeStormwaterControlsCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TCalculateHydrologyCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TDeployRunoffCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TCreateReconciliationSpreadsheetCommand.Create(CurrentModel, lblProgress,
    prgProgress, fAbortSignal));
  fCommandList.Add(TCalculateHydraulicsCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TDeployEngineFileCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TCreateQCWorkspacesCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TCopyStandardMapInfoWorkspacesCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));

  chklstBuildOptions.Clear;
  lblPreset.Caption := 'Individual build options';

  for i := 0 to fCommandList.Count - 1 do
  begin
    if (fCommandList.Items[i].Name = 'Open master and model tables') or
      (fCommandList.Items[i].Name = 'Relink model data') then
    begin
      fCommandList[i].Enabled := True;
      chklstBuildOptions.AddEx(fCommandList.Items[i].Name, True);
    end
    else
    begin
      fCommandList[i].Enabled := False;
      chklstBuildOptions.AddEx(fCommandList.Items[i].Name, False);
    end;
  end;

{
    // Enable the OpenMasterModelData and RelinkModelData commands
    fCommandList[0].Enabled := True;
    chklstBuildOptions.ItemState[0] := cbChecked;
    fCommandList[1].Enabled := True;
    chklstBuildOptions.ItemState[1] := cbChecked;
}
end;

procedure TfrmBuild.actPresetRecalculateHydrologyExecute(Sender: TObject);
var
  i: Integer;
begin
  fCommandList.Clear;
  fCommandList.Add(TOpenMasterModelDataCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TRelinkModelDataCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TCalculateHydrologyCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TDeployRunoffCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));

  chklstBuildOptions.Clear;
  lblPreset.Caption := 'Recalculate hydrology';
  for i := 0 to fCommandList.Count - 1 do
  begin
    chklstBuildOptions.AddEx(fCommandList.Items[i].Name, True);
    fCommandList[i].Enabled := True;
  end;
end;

procedure TfrmBuild.actPresetTraceNetworkOnlyExecute(Sender: TObject);
var
  i: Integer;
begin
  fCommandList.Clear;
  fCommandList.Add(TOpenMasterModelDataCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TBuildEmptyModelCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  fCommandList.Add(TBuildNetworkCommand.Create(CurrentModel, lblProgress, prgProgress,
    fAbortSignal));
  chklstBuildOptions.Clear;

  lblPreset.Caption := 'Trace network only';
  for i := 0 to fCommandList.Count - 1 do
  begin
    chklstBuildOptions.AddEx(fCommandList.Items[i].Name, True);
    fCommandList[i].Enabled := True;
  end;
end;

procedure TfrmBuild.btnBrowseModelDirectoryClick(Sender: TObject);
begin
  if dlgSelectFolder.Execute then
  begin
    cmbModelDirectory.Text := dlgSelectFolder.SelectedPathName;
    cmbModelDirectory.UpdateMRUList;
    cmbModelDirectory.SaveMRUData;
    CheckForExistingModel;
    CheckUIPrompts;
  end;
end;

procedure TfrmBuild.btnBuildModelClick(Sender: TObject);
var
  StartDate: TDateTime;
  EndDate: TDateTime;
  i: Integer;
  NumCommands: Integer;
const
  ProgressPanelMargin = 500;
begin
  pnlBuild.Enabled := False;
  vgrdBuildOptions.Visible := False;
  Screen.Cursor := crHourglass;
  try
    pnlProgress.Top := (pnlBuild.Height - pnlProgress.Height) div 2 + pnlBuild.Top;
    pnlProgress.Width := pnlBuild.Width - ProgressPanelMargin;
    pnlProgress.Left := ProgressPanelMargin div 2;
    pnlProgress.Show;

    Application.ProcessMessages;

    CodeSite.AddSeparator;
    for i := 0 to fCommandList.Count - 1 do
      CodeSite.SendFmtMsg('Command list [%d] = %s, Enabled = %d',
        [i, fCommandList[i].Name, Integer(fCommandList[i].Enabled)]);

    Assert(ModelLoaded, 'btnBuildModelClick: Model not loaded');

    CurrentModel.ClearErrorLog;

    // Get rid of the build report form if it's present
    if Assigned(frmBuildReport) then
    begin
      frmBuildReport.Close;
      FreeAndNil(frmBuildReport);
    end;

    StartDate := Now;
    CurrentModel.AddError(TEMGAATSError.Create('Build started', eetRunCommand));
    CurrentModel.AddError(TEMGAATSError.Create('Building model in ' +
      CurrentModel.Path, eetInfo));

    // Clear out the root and stop pipes list if necessary
    if (memRootPipes.Lines.Count <=1) and (memRootPipes.Lines[0] = PromptLinks) then
      memRootPipes.Clear;
    if (memStopPipes.Lines.Count <=1) and (memStopPipes.Lines[0] = PromptLinks) then
      memStopPipes.Clear;

    // Exit if no root pipes
    if (memRootPipes.Lines.Count < 1) then
      CurrentModel.AddError(TEMGAATSError.Create(
        'No root pipes specified, aborting build', eetError))
    else
    begin

      // Assign boundaries and build options
      CurrentModel.AssignStops(memStopPipes.Lines);
      CurrentModel.AssignRoots(memRootPipes.Lines);
      if vgrdTimeFrame.Properties.Value = 'Existing' then
        CurrentModel.Config.TimeFrame := tfExisting
      else
        CurrentModel.Config.TimeFrame := tfFuture;
      CurrentModel.Config.Update;
      EMGWorkbenchManager.SetTimeFrame(CurrentModel.Config.TimeFrame);

      CurrentModel.Config.Title := vgrdTitle.Properties.Value ;
      CurrentModel.Config.Description := vgrdDescription.Properties.Value;
      CodeSite.Send('vgrdStorms.Properties.Value', String(vgrdStorms.Properties.Value));
      CurrentModel.Config.StormsToBuildList := StormListString(vgrdStorms.Properties.Value);
      CurrentModel.Config.RunoffFileName := vgrdRunoffBaseFileName.Properties.Value;
      CurrentModel.Config.EngineExportFileName := vgrdEngineBaseFileName.Properties.Value;
      CurrentModel.Config.UseBaseflow := vgrdUseBaseflow.Properties.Value;
      CurrentModel.Config.TraceStormwater := vgrdTraceStormwater.Properties.Value;
      CurrentModel.Config.RemoveSanitaryUponDeploy := vgrdRemoveSanitaryUponDeploy.Properties.Value;
      CurrentModel.Config.Update;

      // Set status
      NumCommands := fCommandList.EnabledCommandCount;
      CodeSite.Send('# enabled commands', NumCommands);
      prgProgress.TotalParts := NumCommands;
      prgProgress.PartsComplete := 0;
      lblStats.Caption := '';

      // Execute build commands
      fAbortSignal.Abort := False;
      if NumCommands = 0 then
        CurrentModel.AddError(TEMGAATSError.Create('No build instructions were ' +
          'checked.  Please check what parts you wish to build under Build Options.', eetHint))
      else
      begin
        fCommandList.Execute(fAbortSignal);
        EndDate := Now;
        CurrentModel.AddError(TEMGAATSError.Create(Format('Build time: %.2f minutes',
          [MinuteSpan(EndDate, StartDate)]), eetInfo));

        if fAbortSignal.Abort then
          CurrentModel.AddError(TEMGAATSError.Create('Build stopped by user', eetInfo))
        else
        begin
          ModelLoaded := True;
          CurrentModel.AddError(TEMGAATSError.Create('Build ended', eetRunCommand));
        end;
      end;
    end;

    frmBuildReport := frmMain.DisplayForm(TfrmBuildReport) as TfrmBuildReport;
    frmBuildReport.InitReport(CurrentModel, fAbortSignal.Abort);
  finally
    pnlProgress.Hide;
    pnlBuild.Enabled := True;
    vgrdBuildOptions.Visible := True;
    Screen.Cursor := crDefault;
    CheckPipeListPrompt(memRootPipes);
    CheckPipeListPrompt(memStopPipes);
  end;
end;

procedure TfrmBuild.btnCancelBuildClick(Sender: TObject);
begin
  fAbortSignal.Abort := True;
  pnlBuild.Enabled := True;
  pnlProgress.Hide;
end;

procedure TfrmBuild.btnCopyModelBoundariesClick(Sender: TObject);
var
  TempModel: TEMGAATSModel;
  OpenDir: String;
  Roots: TStringList;
  Stops: TStringList;
  ForcedRoots: TStringList;
  ForcedStops: TStringList;
begin
  if Assigned(CurrentModel)  then
    OpenDir := CurrentModel.Path
  else if cmbModelDirectory.Text <> '' then
    OpenDir := cmbModelDirectory.Text
  else
    OpenDir := 'C:\';


  frmMain.dlgOpen.InitialDir := OpenDir;
  frmMain.dlgOpen.Filter := 'Model INI files (*.ini)|*.ini|All Files (*.*)|*.*';
  frmMain.dlgOpen.FilterIndex := 0;
  if frmMain.dlgOpen.Execute then
  begin
    memRootPipes.Clear;
    memStopPipes.Clear;
    TempModel := TEMGAATSModel.Create(ExtractFileDir(frmMain.dlgOpen.FileName));
    Roots := TStringList.Create;
    Stops := TStringList.Create;
    ForcedRoots := TStringList.Create;
    ForcedStops := TStringList.Create;
    frmMain.errHandler.Clear;
    frmMain.errHandler.Caption := 'Model boundaries check';
    frmMain.errHandler.HandleErrors;
    try
      TempModel.GetRootLinks(Roots, frmMain.errHandler);
      TempModel.GetStopLinks(Stops, frmMain.errHandler);
      memRootPipes.Lines.AddStrings(Roots);
      memStopPipes.Lines.AddStrings(Stops);
      TempModel.GetForcedRootLinks(ForcedRoots, frmMain.errHandler);
      TempModel.GetForcedStopLinks(ForcedStops, frmMain.errHandler);
      memRootPipes.Lines.AddStrings(ForcedRoots);
      memStopPipes.Lines.AddStrings(ForcedStops);
      frmMain.errHandler.HandleErrors;
    finally
      Roots.Free;
      Stops.Free;
      ForcedRoots.Free;
      ForcedStops.Free;
      TempModel.Free;
      EMGWorkbenchManager.SetModel(CurrentModel);
    end;
  end;
  CheckUIPrompts;
end;

procedure TfrmBuild.btnHomeClick(Sender: TObject);
begin
  if Assigned(frmBuildReport) then
  begin
    frmBuildReport.Close;
    FreeAndNil(frmBuildReport);
  end;
  frmWelcome.Show;
  Close;
  FreeAndNil(frmBuild);
end;

procedure TfrmBuild.btnLoadClick(Sender: TObject);
begin
  inherited;
  LoadModel;
end;

function TfrmBuild.StormListString(AValue: String): String;
var
  EditValueTokens: TStringList;
  CheckedValueTokens: TStringList;
  i: Integer;
  ComboBoxProperties: TcxCheckComboBoxProperties;
begin
  EditValueTokens := TStringList.Create;
  CheckedValueTokens := TStringList.Create;
  Result := '';
  try
    ExtractTokensL(AValue, ';', '''', True, EditValueTokens);
    if EditValueTokens.Count > 0 then
    begin
      CodeSite.Send(EditValueTokens[1]);
      ExtractTokensL(EditValueTokens[1], ',', '''', True, CheckedValueTokens);
      ComboBoxProperties := vgrdStorms.Properties.EditProperties as TcxCheckComboBoxProperties;
      Result := '';
      for i := 0 to CheckedValueTokens.Count - 1 do
        Result := Result + ComboBoxProperties.Items[StrToInt(CheckedValueTokens[i])].ShortDescription + ';';
      Result := LeftStr(Result, Length(Result)-1);
    end;
  finally
    EditValueTokens.Free;
    CheckedValueTokens.Free;
  end;
end;

procedure TfrmBuild.CleanModelDirectory(SaveBoundaries: Boolean);
var
  TempModel: TEMGAATSModel;
  Roots: TStringList;
  Stops: TStringList;
  ForcedRoots: TStringList;
  ForcedStops: TStringList;
begin
  // Really dangerous!  Check for structure and delete the repeatable stuff only
  CodeSite.Send('Erasing directory contents');
  if (cmbModelDirectory.Text <> '') then
  begin
    if SaveBoundaries then
    begin
      TempModel := TEMGAATSModel.Create(cmbModelDirectory.Text);
      Roots := TStringList.Create;
      Stops := TStringList.Create;
      ForcedRoots := TStringList.Create;
      ForcedStops := TStringList.Create;
      try
        TempModel.GetRootLinks(Roots);
        TempModel.GetStopLinks(Stops);
        SavedRoots.AddStrings(Roots);
        SavedStops.AddStrings(Stops);
        TempModel.GetForcedRootLinks(ForcedRoots);
        TempModel.GetForcedStopLinks(ForcedStops);
        SavedRoots.AddStrings(ForcedRoots);
        SavedStops.AddStrings(ForcedStops);
      finally
        Roots.Free;
        Stops.Free;
        ForcedRoots.Free;
        ForcedStops.Free;
        TempModel.Free;
      end;
    end;
    DelTree(cmbModelDirectory.Text);
    ForceDirectories(cmbModelDirectory.Text);
  end;
end;

procedure TfrmBuild.CheckBoxesFromStormListString(AStormList: String);
var
  i: Integer;
  StormList: TStringList;
  CurrentStormID: String;
  OriginalFocusedRow: TcxCustomRow;
  ComboBox: TcxCheckComboBox;
  ComboBoxItemCounter: Integer;
begin
  StormList := TStringList.Create;
  try
    ExtractTokensL(AStormList, ';', '''', True, StormList);

    OriginalFocusedRow := vgrdBuildOptions.FocusedRow;
    LockWindowUpdate(vgrdBuildOptions.Handle);
    try
      vgrdStorms.Focused := True;
      vgrdBuildOptions.ShowEdit;
      ComboBox := TcxCheckComboBox(vgrdBuildOptions.InplaceEditor);

      for ComboBoxItemCounter := 0 to ComboBox.Properties.Items.Count - 1 do
        ComboBox.States[ComboBoxItemCounter] := cbsUnchecked;
      for i := 0 to StormList.Count - 1 do
      begin
        CurrentStormID := StormList[i];
        for ComboBoxItemCounter := 0 to ComboBox.Properties.Items.Count - 1 do
          if CurrentStormID = ComboBox.Properties.Items[ComboBoxItemCounter].ShortDescription then
          begin
            ComboBox.States[ComboBoxItemCounter] := cbsChecked;
            CodeSite.Send('ComboBox.States[' + IntToStr(ComboBoxItemCounter) + ']',
              Integer(ComboBox.States[ComboBoxItemCounter]));
            Break;
          end;
      end;

      vgrdBuildOptions.HideEdit;
      for ComboBoxItemCounter := 0 to ComboBox.Properties.Items.Count - 1 do
        CodeSite.Send('ComboBox.States[' + IntToStr(ComboBoxItemCounter) + ']',
          Integer(ComboBox.States[ComboBoxItemCounter]));
      CodeSite.Send('ComboBox.EditValue', String(ComboBox.EditValue));
      vgrdStorms.Properties.Value := ComboBox.EditValue;
      CodeSite.Send('ComboBox.Value', String(vgrdStorms.Properties.Value));
      vgrdBuildOptions.FocusedRow := OriginalFocusedRow;
    finally
      LockWindowUpdate(0);
    end;

  finally
    StormList.Free;
  end;

end;

procedure TfrmBuild.CheckForExistingModel;
begin
  Screen.Cursor := crHourglass;
  try
    if FileExists(IncludeTrailingPathDelimiter(cmbModelDirectory.Text) + ModelConfigFileName) then
    begin
      dlgModelExists.Path := cmbModelDirectory.Text;
      dlgModelExists.ShowModal;
      case dlgModelExists.ModelExistsAction of
        meaEraseContents:
        begin
          CleanModelDirectory;
          LoadModel(dlgModelExists.chkRememberModelBoundaries.Checked, True);
        end;
        meaKeepContents:
        begin
          LoadModel;
        end;
      end;
    end
    else
      LoadModel(False, True);
  finally
    Screen.Cursor := crDefault;
  end;
end;

procedure TfrmBuild.CheckUIPrompts;
begin
  CheckPipeListPrompt(memStopPipes);
  CheckPipeListPrompt(memRootPipes);
  CheckModelDirectoryPrompt;
end;

procedure TfrmBuild.chklstBuildOptionsChange(Sender: TObject; Index: Integer;
  NewState: TCheckBoxState);
begin
  fCommandList[Index].Enabled := NewState = cbChecked;
end;

procedure TfrmBuild.cmbModelDirectoryChange(Sender: TObject);
begin
  ModelLoaded := False;
end;

procedure TfrmBuild.cmbModelDirectoryEnter(Sender: TObject);
var
  i: Integer;
begin
  if cmbModelDirectory.Text = PromptModelDirectory then
  begin
    cmbModelDirectory.Text := '';
    cmbModelDirectory.Font.Color := clWindowText;
  end;
  for i := cmbModelDirectory.Items.Count -1 downto 0 do
    if cmbModelDirectory.Items[i] = PromptModelDirectory then
      cmbModelDirectory.Items.Delete(i);
  cmbModelDirectory.SaveMRUData;
end;

procedure TfrmBuild.cmbModelDirectoryExit(Sender: TObject);
begin
  if cmbModelDirectory.Text = '' then
  begin
    cmbModelDirectory.Text := PromptModelDirectory;
    cmbModelDirectory.Font.Color := clGrayText;
  end
  else
    cmbModelDirectory.Font.Color := clWindowText;
end;

procedure TfrmBuild.cmbModelDirectorySelect(Sender: TObject);
begin
  CheckForExistingModel;
  CheckUIPrompts;
end;

procedure TfrmBuild.FillStormsCheckComboList;
var
  ComboBoxProperties: TcxCheckComboBoxProperties;
begin
  adoConnection.ConnectionString :=
    'Provider=Microsoft.Jet.OLEDB.4.0;' +
    'Data Source=' + IncludeTrailingPathDelimiter(SystemConfig.MDBsByName['root']) +
      SystemConfig.MDBsByName['LookupTables'] + ';' +
    'Persist Security Info=False';
  adoConnection.Open;
  adoStorms.Open;
  adoStorms.First;
  ComboBoxProperties := vgrdStorms.Properties.EditProperties as TcxCheckComboBoxProperties;
  ComboBoxProperties.Items.Clear;
  while not adoStorms.Eof do
  begin
    ComboBoxProperties.Items.AddCheckItem(adoStorms.FieldValues['StormName'],
      adoStorms.FieldValues['Abbreviation']);
    adoStorms.Next;
  end;
  vgrdStorms.Properties.Value := 0;
  adoStorms.Close;
  adoConnection.Close;
end;

procedure TfrmBuild.FormCreate(Sender: TObject);
begin
  inherited;
  fCommandList := TEMGAATSModelCommandList.Create;
  fAbortSignal := TEMGAATSCommandAbortSignal.Create;
  SavedRoots := TStringList.Create;
  SavedStops := TStringList.Create;
  chklstBuildOptions.Clear;
  FillStormsCheckComboList;
end;

procedure TfrmBuild.FormDestroy(Sender: TObject);
begin
  fCommandList.Free;
  fAbortSignal.Free;
  SavedRoots.Free;
  SavedStops.Free;
  FreeAndNil(CurrentModel);
  inherited;
end;

procedure TfrmBuild.FormShow(Sender: TObject);
begin
  inherited;
  if not Assigned(CurrentModel) then
    InitializeUI;
end;

procedure TfrmBuild.InitializeUI;
begin
  EnableBuildUI(False);

  memRootPipes.Text := PromptLinks;
  memRootPipes.Font.Color := clGrayText;
  memStopPipes.Text := PromptLinks;
  memStopPipes.Font.Color := clGrayText;
  cmbModelDirectory.Text := PromptModelDirectory;
  cmbModelDirectory.Font.Color := clGrayText;
  cmbModelDirectory.LoadMRUData(False);

  pnlProgress.Hide;
  Realign;
  RzPanel1.Realign;
end;

procedure TfrmBuild.LoadModel(RememberBoundaries: Boolean; IsNewModel: Boolean);
begin
  if Assigned(CurrentModel) then
    FreeAndNil(CurrentModel);

  EnableBuildUI(True);
  CurrentModel := TEMGAATSModel.Create(cmbModelDirectory.Text, IsNewModel);

  if RememberBoundaries then
  begin
    if CurrentModel.Config.StopLinksCount > 0 then
    begin
      memStopPipes.Clear;
      CurrentModel.GetStopLinks(memStopPipes.Lines);
      memStopPipes.Update;
    end;

    if CurrentModel.Config.RootLinksCount > 0 then
    begin
      memRootPipes.Clear;
      CurrentModel.GetRootLinks(memRootPipes.Lines);
      memRootPipes.Update;
    end;

    if SavedRoots.Count > 0 then
    begin
      memRootPipes.Clear;
      memRootPipes.Lines.AddStrings(SavedRoots);
      SavedRoots.Clear;
    end;

    if SavedStops.Count > 0 then
    begin
      memStopPipes.Clear;
      memStopPipes.Lines.AddStrings(SavedStops);
      SavedStops.Clear;
    end;
  end;

  // Timeframe
  case CurrentModel.TimeFrame of
    tfExisting: vgrdTimeFrame.Properties.Value := 'Existing';
    tfFuture: vgrdTimeFrame.Properties.Value := 'Future';
  end;

  // Title, description
  vgrdTitle.Properties.Value := CurrentModel.Config.Title;
  vgrdDescription.Properties.Value := CurrentModel.Config.Description;

  CodeSite.Send('vgrdStorms before', String(vgrdStorms.Properties.Value));
  CodeSite.Send('StormsToBuildList', CurrentModel.Config.StormsToBuildList);
  CheckBoxesFromStormListString(CurrentModel.Config.StormsToBuildList);
  CodeSite.Send('vgrdStorms after', String(vgrdStorms.Properties.Value));

  vgrdRunoffBaseFileName.Properties.Value := CurrentModel.Config.RunoffFileName;
  vgrdEngineBaseFileName.Properties.Value := CurrentModel.Config.EngineExportFileName;
  vgrdUseBaseflow.Properties.Value := CurrentModel.Config.UseBaseflow;
  vgrdTraceStormwater.Properties.Value := CurrentModel.Config.TraceStormwater;
  vgrdRemoveSanitaryUponDeploy.Properties.Value := CurrentModel.Config.RemoveSanitaryUponDeploy;

  pnlBuild.Enabled := True;
  pnlProgress.Hide;
  CurrentModel.ProgressLabel := lblProgress;
  CurrentModel.ProgressBar := prgProgress;
  CurrentModel.Config.ModelEnteredDate := Now;
  ModelLoaded := True;

  // Check for presence of an alternative, and if so, change the preset to deploy
  if CurrentModel.Config.CreatedVia = 'Alternative' then
    actPresetDeployAlternativeExecute(nil)
  else
    actPresetIndividualOptionsExecute(nil);
end;

procedure TfrmBuild.memRootPipesEnter(Sender: TObject);
begin
  if memRootPipes.Text = PromptLinks then
  begin
    memRootPipes.Text := '';
    memRootPipes.Font.Color := clWindowText;
  end;
end;

procedure TfrmBuild.memRootPipesExit(Sender: TObject);
begin
  if memRootPipes.Text = '' then
  begin
    memRootPipes.Text := PromptLinks;
    memRootPipes.Font.Color := clGrayText;
  end;
end;

procedure TfrmBuild.memStopPipesEnter(Sender: TObject);
begin
  if memStopPipes.Text = PromptLinks then
  begin
    memStopPipes.Text := '';
    memStopPipes.Font.Color := clWindowText;
  end;
end;

procedure TfrmBuild.memStopPipesExit(Sender: TObject);
begin
  if memStopPipes.Text = '' then
  begin
    memStopPipes.Text := PromptLinks;
    memStopPipes.Font.Color := clGrayText;
  end;
end;

procedure TfrmBuild.OnUpdateStats(var Msg: TMessage);
begin
  lblStats.Caption := Format('%d links traced', [Msg.WParam]);
end;

procedure TfrmBuild.UnloadModel;
begin
  ModelLoaded := False;
  FreeAndNil(CurrentModel);
  InitializeUI;
end;

procedure TfrmBuild.vgrdBuildOptionsDrawBackground(Sender: TObject;
  ACanvas: TcxCanvas; const R: TRect; const AViewParams: TcxViewParams;
  var Done: Boolean);
begin
  if not grpbBuildOptions.Enabled then
  begin
    ACanvas.Brush.Color := clBtnFace;
    ACanvas.FillRect(R, AViewParams);
  end;
end;

procedure TfrmBuild.CheckModelDirectoryPrompt;
begin
  if cmbModelDirectory.Text = PromptModelDirectory then
    cmbModelDirectory.Font.Color := clGrayText
  else if cmbModelDirectory.Text = '' then
  begin
    cmbModelDirectory.Text := PromptModelDirectory;
    cmbModelDirectory.Font.Color := clGrayText;
  end
  else
    cmbModelDirectory.Font.Color := clWindowText;
end;

procedure TfrmBuild.CheckPipeListPrompt(AMemoControl: TRzMemo);
begin
  if AMemoControl.Text = PromptLinks then
    AMemoControl.Font.Color := clGrayText
  else if AMemoControl.Text = '' then
  begin
    AMemoControl.Text := PromptLinks;
    AMemoControl.Font.Color := clGrayText;
  end
  else
    AMemoControl.Font.Color := clWindowText;
end;

procedure TfrmBuild.EnableBuildUI(Enable: Boolean);
begin
  grpbModelBoundaries.Enabled := Enable;
  grpbBuildOptions.Enabled := Enable;
  pnlBuildActions.Enabled := Enable;
  vgrdBuildOptions.Visible := Enable;
end;

end.
