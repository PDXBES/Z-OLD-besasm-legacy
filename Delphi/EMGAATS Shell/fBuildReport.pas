unit fBuildReport;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, fLabeledChild, cxControls, cxSSheet, RzTabs, ExtCtrls, RzPanel,
  StdCtrls, uEMGAATSModel, uMapInfoManager, pngextra, RzSplit, RzButton, RzEdit,
  ComCtrls, Buttons, PngSpeedButton, RzLabel, pngimage, RzBckgnd, cxGraphics,
  cxCustomData, cxStyles, cxTL, cxImage, cxTextEdit, ImgList, PngImageList,
  cxInplaceContainer, RzCmboBx, RzShellDialogs, ActnList, XPStyleActnCtrls,
  ActnMan, uEMGAATSTypes, RzRadChk, FileCtrl, RzFilSys, RzTreeVw, ShlObj,
  cxShellCommon, cxShellTreeView, cxContainer, cxShellListView, RzListVw,
  RzShellCtrls;

type
  TfrmBuildReport = class(TfrmLabeledChild)
    pgcBuildReport: TRzPageControl;
    RzPanel1: TRzPanel;
    tabSummary: TRzTabSheet;
    tabHydroQualityControl: TRzTabSheet;
    ssQualityControl: TcxSpreadSheet;
    tabMap: TRzTabSheet;
    RzPanel2: TRzPanel;
    pnlMap: TRzPanel;
    btnReturnToBuild: TRzButton;
    btnHand: TPngSpeedButton;
    btnZoomIn: TPngSpeedButton;
    btnZoomOut: TPngSpeedButton;
    btnInfo: TPngSpeedButton;
    RzLabel1: TRzLabel;
    RzLabel2: TRzLabel;
    tabXPX: TRzTabSheet;
    tabRunoff: TRzTabSheet;
    treeErrors: TcxTreeList;
    colErrorType: TcxTreeListColumn;
    colError: TcxTreeListColumn;
    imglstErrorTypes: TPngImageList;
    colTime: TcxTreeListColumn;
    RzPanel3: TRzPanel;
    RzLabel3: TRzLabel;
    cmbActiveView: TRzComboBox;
    btnOpenMap: TRzButton;
    dlgOpen: TRzOpenDialog;
    RzPanel5: TRzPanel;
    RzLabel4: TRzLabel;
    RzFlowPanel1: TRzFlowPanel;
    btnShowErrors: TRzBitBtn;
    btnShowWarnings: TRzBitBtn;
    btnShowInfos: TRzBitBtn;
    btnShowHints: TRzBitBtn;
    btnShowStats: TRzBitBtn;
    ActionManager1: TActionManager;
    actShowErrors: TAction;
    actShowWarnings: TAction;
    actShowInfos: TAction;
    actShowHints: TAction;
    actShowStats: TAction;
    RzPanel6: TRzPanel;
    RzLabel5: TRzLabel;
    cmbRunoffStormToView: TRzComboBox;
    RzLabel6: TRzLabel;
    RzRadioButton1: TRzRadioButton;
    RzRadioButton2: TRzRadioButton;
    memRunoff: TRzMemo;
    tabModelDirectory: TRzTabSheet;
    RzSizePanel1: TRzSizePanel;
    dtreeModel: TRzShellTree;
    RzShellList1: TRzShellList;
    actShowRunCommands: TAction;
    btnShowRunCommands: TRzBitBtn;
    btnShowToDos: TRzBitBtn;
    actShowToDos: TAction;
    procedure FormDestroy(Sender: TObject);
    procedure btnHandClick(Sender: TObject);
    procedure btnZoomInClick(Sender: TObject);
    procedure btnZoomOutClick(Sender: TObject);
    procedure btnInfoClick(Sender: TObject);
    procedure pnlMapResize(Sender: TObject);
    procedure RzButton2Click(Sender: TObject);
    procedure RzButton3Click(Sender: TObject);
    procedure RzButton4Click(Sender: TObject);
    procedure btnReturnToBuildClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure btnOpenMapClick(Sender: TObject);
    procedure cmbActiveViewChange(Sender: TObject);
    procedure actShowErrorsUpdate(Sender: TObject);
    procedure actShowWarningsUpdate(Sender: TObject);
    procedure actShowInfosUpdate(Sender: TObject);
    procedure actShowHintsUpdate(Sender: TObject);
    procedure actShowStatsUpdate(Sender: TObject);
    procedure actShowErrorsExecute(Sender: TObject);
    procedure actShowWarningsExecute(Sender: TObject);
    procedure actShowInfosExecute(Sender: TObject);
    procedure actShowHintsExecute(Sender: TObject);
    procedure actShowStatsExecute(Sender: TObject);
    procedure cmbRunoffStormToViewChange(Sender: TObject);
    procedure actShowRunCommandsExecute(Sender: TObject);
    procedure actShowRunCommandsUpdate(Sender: TObject);
    procedure treeErrorsCustomDrawCell(Sender: TObject; ACanvas: TcxCanvas;
      AViewInfo: TcxTreeListEditCellViewInfo; var ADone: Boolean);
    procedure actShowToDosExecute(Sender: TObject);
    procedure actShowToDosUpdate(Sender: TObject);
  private
    { Private declarations }
    fModel: TEMGAATSModel;
    fMapInfoDisplayManager: TMapInfoDisplayManager;
    fCurrentDisplayManager: Integer;
    fNumErrors: Integer;
    fNumWarnings: Integer;
    fNumHints: Integer;
    fNumInfos: Integer;
    fNumStats: Integer;
    fNumRunCommands: Integer;
    fNumToDos: Integer;
    fErrorList: TStringList;
    procedure BuildDefaultMapOverview;
    procedure TallyErrorCount(CurrentErrorType: TEMGAATSErrorType);
    procedure ResetErrorCount;
    procedure ProduceSummary;
  protected
    procedure CreateParams(var Params: TCreateParams); override;
  public
    { Public declarations }
    procedure InitReport(AModel: TEMGAATSModel; Aborted: Boolean = True);
    procedure AnalyzeErrors(AErrorList: TStringList);
    procedure AnalyzeStats(AStatsList: TStringList);
    procedure InitMap;
  end;

var
  frmBuildReport: TfrmBuildReport;

implementation

uses uEMGWorkbenchManager, uMIConstants, fMain, fBuild,
  uEMGAATSSystemConfig, CodeSiteLogging;

{$R *.dfm}

{ TfrmBuildReport }

procedure TfrmBuildReport.actShowErrorsExecute(Sender: TObject);
begin
  ProduceSummary;
end;

procedure TfrmBuildReport.actShowErrorsUpdate(Sender: TObject);
begin
  actShowErrors.Enabled := fNumErrors > 0;
  if actShowErrors.Enabled then
  begin
    if fNumErrors = 1 then
      actShowErrors.Caption := '1 Error'
    else
      actShowErrors.Caption := Format('%d Errors', [fNumErrors]);
  end;
end;

procedure TfrmBuildReport.actShowHintsExecute(Sender: TObject);
begin
  ProduceSummary;
end;

procedure TfrmBuildReport.actShowHintsUpdate(Sender: TObject);
begin
  actShowHints.Enabled := fNumHints > 0;
  if actShowHints.Enabled then
  begin
    if fNumHints = 1 then
      actShowHints.Caption := '1 Hint'
    else
      actShowHints.Caption := Format('%d Hints', [fNumHints]);
  end;
end;

procedure TfrmBuildReport.actShowInfosExecute(Sender: TObject);
begin
  ProduceSummary;
end;

procedure TfrmBuildReport.actShowInfosUpdate(Sender: TObject);
begin
  actShowInfos.Enabled := fNumInfos > 0;
  if actShowInfos.Enabled then
  begin
    if fNumInfos = 1 then
      actShowInfos.Caption := '1 Info'
    else
      actShowInfos.Caption := Format('%d Infos', [fNumInfos]);
  end;
end;

procedure TfrmBuildReport.actShowRunCommandsExecute(Sender: TObject);
begin
  ProduceSummary;
end;

procedure TfrmBuildReport.actShowRunCommandsUpdate(Sender: TObject);
begin
  inherited;
  actShowRunCommands.Enabled := fNumRunCommands > 0;
  if actShowRunCommands.Enabled then
  begin
    if fNumRunCommands = 1 then
      actShowRunCommands.Caption := '1 Command'
    else
      actShowRunCommands.Caption := Format('%d Commands', [fNumRunCommands]);
  end;
end;

procedure TfrmBuildReport.actShowStatsExecute(Sender: TObject);
begin
  ProduceSummary;
end;

procedure TfrmBuildReport.actShowStatsUpdate(Sender: TObject);
begin
  actShowStats.Enabled := fNumStats > 0;
  if actShowStats.Enabled then
  begin
    if fNumStats = 1 then
      actShowStats.Caption := '1 Stat'
    else
      actShowStats.Caption := Format('%d Stats', [fNumStats]);
  end;
end;

procedure TfrmBuildReport.actShowToDosExecute(Sender: TObject);
begin
  ProduceSummary;
end;

procedure TfrmBuildReport.actShowToDosUpdate(Sender: TObject);
begin
  actShowToDos.Enabled := fNumToDos > 0;
  if actShowToDos.Enabled then
  begin
    if fNumToDos = 1 then
      actShowToDos.Caption := '1 To-Do'
    else
      actShowToDos.Caption := Format('%d To-Do''s', [fNumToDos]);
  end;
end;

procedure TfrmBuildReport.actShowWarningsExecute(Sender: TObject);
begin
  ProduceSummary;
end;

procedure TfrmBuildReport.actShowWarningsUpdate(Sender: TObject);
begin
  actShowWarnings.Enabled := fNumWarnings > 0;
  if actShowWarnings.Enabled then
  begin
    if fNumWarnings = 1 then
      actShowWarnings.Caption := '1 Warning'
    else
      actShowWarnings.Caption := Format('%d Warnings', [fNumWarnings]);
  end;
end;

procedure TfrmBuildReport.AnalyzeErrors(AErrorList: TStringList);
var
  i: Integer;
  CurrentNode: TcxTreeListNode;
  CurrentError: TEMGAATSError;
begin
  for i := 0 to AErrorList.Count - 1 do
  begin
    CurrentNode := treeErrors.Add;
    CurrentError := AErrorList.Objects[i] as TEMGAATSError;
    if Assigned(CurrentError) then
    begin
      CurrentNode.AssignValues([CurrentError.Msg, Integer(CurrentError.ErrorType),
        FormatDateTime('mm/dd hh:nn:ss.zzz', CurrentError.LogTime)]);
      CurrentNode.ImageIndex := Integer(CurrentError.ErrorType);
      CurrentNode.SelectedIndex := CurrentNode.ImageIndex;
      TallyErrorCount(CurrentError.ErrorType);
      fErrorList.AddObject('', CurrentError);
    end;
  end;
end;

procedure TfrmBuildReport.TallyErrorCount(CurrentErrorType: TEMGAATSErrorType);
begin
  case CurrentErrorType of
    eetStats:
      Inc(fNumStats);
    eetInfo:
      Inc(fNumInfos);
    eetHint:
      Inc(fNumHints);
    eetWarning:
      Inc(fNumWarnings);
    eetError:
      Inc(fNumErrors);
    eetRunCommand:
      Inc(fNumRunCommands);
    eetToDo:
      Inc(fNumToDos);
  end;
end;

procedure TfrmBuildReport.treeErrorsCustomDrawCell(Sender: TObject;
  ACanvas: TcxCanvas; AViewInfo: TcxTreeListEditCellViewInfo;
  var ADone: Boolean);
begin
  inherited;
  case TEMGAATSErrorType(AViewInfo.Node.ImageIndex) of
    eetWarning: ACanvas.Font.Color := clWebOrange;
    eetError: ACanvas.Font.Color := clRed;
    eetToDo: ACanvas.Font.Color := clBlue;
  else
    ACanvas.Font.Color := clWindowText;
  end;
end;

procedure TfrmBuildReport.AnalyzeStats(AStatsList: TStringList);
var
  i: Integer;
  CurrentNode: TcxTreeListNode;
  CurrentTime: TDateTime;
  StatError: TEMGAATSError;
begin
  CurrentTime := Now;
  for i := 0 to AStatsList.Count - 1 do
  begin
    CurrentNode := treeErrors.Add;
    StatError := TEMGAATSError.Create(AStatsList[i], eetStats);
    CurrentNode.AssignValues([StatError.Msg, StatError.ErrorType,
      FormatDateTime('mm/dd hh:nn:ss.zzz', StatError.LogTime)]);
    CurrentNode.ImageIndex := Integer(StatError.ErrorType);
    CurrentNode.SelectedIndex := CurrentNode.ImageIndex;
    TallyErrorCount(eetStats);
    fErrorList.AddObject('', StatError);
  end;
end;

procedure TfrmBuildReport.btnHandClick(Sender: TObject);
begin
  fMapInfoDisplayManager.SelectTool(mi_M_TOOLS_PAN);
end;

procedure TfrmBuildReport.btnInfoClick(Sender: TObject);
begin
  fMapInfoDisplayManager.SelectTool(mi_M_TOOLS_INFO);
  fMapInfoDisplayManager.AssignInfoWindow(pnlMap.Handle);
end;

procedure TfrmBuildReport.btnOpenMapClick(Sender: TObject);
begin
  dlgOpen.InitialDir := fModel.Path;
  dlgOpen.Title := 'Select map file';
  dlgOpen.Filter := 'MapInfo tables (*.tab)|*.tab|All files (*.*}|*.*';
  dlgOpen.FilterIndex := 1;
  if dlgOpen.Execute then
  begin
    fMapInfoDisplayManager.OpenTable(dlgOpen.FileName);
    fMapInfoDisplayManager.AddLayer(
      fMapInfoDisplayManager.Tables[
        fMapInfoDisplayManager.TableCount-1]);
  end;
end;

procedure TfrmBuildReport.btnZoomInClick(Sender: TObject);
begin
  fMapInfoDisplayManager.SelectTool(mi_M_TOOLS_ZOOM_IN);
end;

procedure TfrmBuildReport.btnZoomOutClick(Sender: TObject);
begin
  fMapInfoDisplayManager.SelectTool(mi_M_TOOLS_ZOOM_OUT);
end;

procedure TfrmBuildReport.cmbActiveViewChange(Sender: TObject);
begin
  fCurrentDisplayManager := cmbActiveView.ItemIndex;
end;

procedure TfrmBuildReport.cmbRunoffStormToViewChange(Sender: TObject);
var
  RunoffFileName: String;
begin
  Assert(Assigned(fModel), 'fModel not assigned');
  Assert(fModel.Config.StormsToBuildCount > 0, 'No storms built');

  memRunoff.Clear;
  RunoffFileName := IncludeTrailingPathDelimiter(fModel.Path) +
    '\sim\' + fModel.Config.StormsToBuild[cmbRunoffStormToView.ItemIndex] +
    '\' + fModel.Config.RunoffFileName;
  memRunoff.Lines.LoadFromFile(RunoffFileName);
end;

procedure TfrmBuildReport.CreateParams(var Params: TCreateParams);
begin
  inherited;
  if CheckWin32Version(5, 1) then
    Params.ExStyle := Params.ExStyle and (not WS_EX_COMPOSITED);
end;

procedure TfrmBuildReport.FormCreate(Sender: TObject);
begin
  pgcBuildReport.ActivePage := tabSummary;
  fErrorList := TStringList.Create;
end;

procedure TfrmBuildReport.FormDestroy(Sender: TObject);
begin
  inherited;
  FreeAndNil(fMapInfoDisplayManager);
  FreeAndNil(fErrorList);
end;

procedure TfrmBuildReport.InitMap;
var
  DefaultMapOverviewFileName: String;
begin
  // Build the Build View
  if fModel.Config.HasNetwork and fModel.Config.HasDirectSubcatchments and
    fModel.Config.HasSurfaceSubcatchments then
  begin
    DefaultMapOverviewFileName := IncludeTrailingPathDelimiter(fModel.Path) +
      'wors\ModelOverview.WOR';
    if FileExists(DefaultMapOverviewFileName) then
    begin
      if not Assigned(fMapInfoDisplayManager) then
        fMapInfoDisplayManager := TMapInfoDisplayManager.Create;
      fMapInfoDisplayManager.AssignMapWindow(pnlMap.Handle);
      fMapInfoDisplayManager.OpenWorkspace(DefaultMapOverviewFileName);
      fMapInfoDisplayManager.ZoomLayer('ProjArea');
      fMapInfoDisplayManager.SelectTool(mi_M_TOOLS_PAN);
    end;
  end
  else
    BuildDefaultMapOverview;
end;

procedure TfrmBuildReport.BuildDefaultMapOverview;
var
  ModelPath: string;
begin
  CodeSite.EnterMethod('BuildDefaultMapOverview');
  if fModel.Config.HasNetwork then
  begin
    if not Assigned(fMapInfoDisplayManager) then
      fMapInfoDisplayManager := TMapInfoDisplayManager.Create;
    fMapInfoDisplayManager.AssignMapWindow(pnlMap.Handle);
    fMapInfoDisplayManager.OpenTable(SystemConfig.BasinBoundaries);
    fMapInfoDisplayManager.OpenTable(fModel.MapFileNames['mdl_links']);
    fMapInfoDisplayManager.OpenTable(fModel.MapFileNames['mdl_nodes']);
  end;

  if fModel.Config.HasDirectSubcatchments then
    fMapInfoDisplayManager.OpenTable(fModel.MapFileNames['mdl_dirsc']);

  if fModel.Config.HasSurfaceSubcatchments then
    fMapInfoDisplayManager.OpenTable(fModel.MapFileNames['mdl_surfsc']);

  if fModel.Config.HasDirectSubcatchments or fModel.Config.HasSurfaceSubcatchments then
  begin
    ModelPath := IncludeTrailingPathDelimiter(fModel.Path);
    fMapInfoDisplayManager.OpenTable(ModelPath + 'surfsc\ProjMask.tab');
    fMapInfoDisplayManager.OpenTable(ModelPath + 'surfsc\ProjArea.tab');
  end;

  if Assigned(fMapInfoDisplayManager) then
  begin
    fMapInfoDisplayManager.CreateMap;
    if fModel.Config.HasDirectSubcatchments or fModel.Config.HasSurfaceSubcatchments then
      fMapInfoDisplayManager.ZoomLayer('ProjArea')
    else
      fMapInfoDisplayManager.ZoomLayer('mdl_links_ac');
    fMapInfoDisplayManager.SelectTool(mi_M_TOOLS_PAN);
  end;
  CodeSite.ExitMethod('BuildDefaultMapOverview');
end;

procedure TfrmBuildReport.InitReport(AModel: TEMGAATSModel; Aborted: Boolean);
var
  ErrorList: TStringList;
  StatsList: TStringList;
  i: Integer;
  RunoffFileName: string;
begin
  CodeSite.EnterMethod('InitReport');
  Screen.Cursor := crHourglass;

  try
    fModel := AModel;

    // Prepare summary tab
    CodeSite.Send('Prepare summary tab');
    treeErrors.Clear;
    treeErrors.Sorted := True;

    ErrorList := TStringList.Create;
    try
      fModel.TransferErrors(ErrorList);
      ResetErrorCount;
      AnalyzeErrors(ErrorList);
    finally
      ErrorList.Free;
    end;

    // Prepare QC worksheet and statistics if build wasn't aborted
    CodeSite.Send('Prepare QC worksheet and stats');
    if not Aborted then
    begin
      try
        if FileExists(AModel.QCWorkbookFileName) then
        begin
          ssQualityControl.LoadFromFile(AModel.QCWorkbookFileName);
          ssQualityControl.Show;
        end
        else
          ssQualityControl.Hide;

        StatsList := TStringList.Create;
        try
          fModel.TransferStats(StatsList);
          AnalyzeStats(StatsList);
        finally
          StatsList.Free;
        end;
      except
        on E: exception do
        begin
          with ssQualityControl.Sheet.GetCellObject(0,0) do
            Text := 'Display not supported with Excel 2013';
          with ssQualityControl.Sheet.GetCellObject(0,1) do
            Text := 'Go to Model Directory tab and Open qc\HydroC.xls to see reconciliation sheet';
        end
      end;

      InitMap;
    end;

    // Prepare summary tab's error type buttons
    CodeSite.Send('Prepare summary tab''s error type buttons');
    actShowErrors.Enabled := fNumErrors > 0;
    btnShowErrors.Down := fNumErrors > 0;
    if fNumErrors = 1 then
      actShowErrors.Caption := '1 Error'
    else
      actShowErrors.Caption := Format('%d Errors', [fNumErrors]);

    actShowWarnings.Enabled := fNumWarnings > 0;
    btnShowWarnings.Down := fNumWarnings > 0;
    if fNumWarnings = 1 then
      actShowWarnings.Caption := '1 Warning'
    else
      actShowWarnings.Caption := Format('%d Warnings', [fNumWarnings]);

    actShowInfos.Enabled := fNumInfos > 0;
    btnShowInfos.Down := fNumInfos > 0;
    if fNumInfos = 1 then
      actShowInfos.Caption := '1 Info'
    else
      actShowInfos.Caption := Format('%d Infos', [fNumInfos]);

    actShowHints.Enabled := fNumHints > 0;
    btnShowHints.Down := fNumHints > 0;
    if fNumHints = 1 then
      actShowHints.Caption := '1 Hint'
    else
      actShowHints.Caption := Format('%d Hints', [fNumHints]);

    actShowStats.Enabled := fNumStats > 0;
    btnShowStats.Down := fNumStats > 0;
    if fNumStats = 1 then
      actShowStats.Caption := '1 Stat'
    else
      actShowStats.Caption := Format('%d Stats', [fNumStats]);

    actShowRunCommands.Enabled := fNumRunCommands > 0;

    // Turn off the Commands by default
    CodeSite.Send('Turn off commands by default');
    btnShowRunCommands.Down := False;
    if fNumRunCommands = 1 then
      actShowRunCommands.Caption := '1 Command'
    else
      actShowRunCommands.Caption := Format('%d Commands', [fNumRunCommands]);

    actShowToDos.Enabled := fNumToDos > 0;
    btnShowToDos.Down := fNumToDos > 0;
    if fNumToDos = 1 then
      actShowToDos.Caption := '1 To-Do'
    else
      actShowToDos.Caption := Format('%d To-Do''s', [fNumToDos]);

    // Prepare Model Directory tab
    CodeSite.Send('Prepare model directory tab');
    dtreeModel.SelectedPathName := fModel.Path;

    // Prepare Runoff tab
    CodeSite.Send('Prepare runoff tab');
    if fModel.Config.StormsToBuildCount > 0 then
    begin
      cmbRunoffStormToView.Items.Clear;
      for i := 0 to fModel.Config.StormsToBuildCount - 1 do
        cmbRunoffStormToView.Items.Add(fModel.Config.StormsToBuild[i]);
      cmbRunoffStormToView.ItemIndex := 0;
      RunoffFileName := IncludeTrailingPathDelimiter(fModel.Path) +
        '\sim\' + fModel.Config.StormsToBuild[0] + '\' + fModel.Config.RunoffFileName;
      memRunoff.Lines.LoadFromFile(RunoffFileName);
      tabRunoff.TabEnabled := True;
    end
    else
      tabRunoff.TabEnabled := False;

    ProduceSummary;
    pgcBuildReport.ActivePage := tabSummary;
  finally
    Screen.Cursor := crDefault;
  end;
  CodeSite.ExitMethod('InitReport');
end;

procedure TfrmBuildReport.pnlMapResize(Sender: TObject);
begin
  if Assigned(fMapInfoDisplayManager) then
    fMapInfoDisplayManager.ResizeMapWindow(pnlMap.Width, pnlMap.Height);
end;

procedure TfrmBuildReport.ProduceSummary;
var
  i: Integer;
  CurrentError: TEMGAATSError;
  CurrentNode: TcxTreeListNode;

  function ShowError: Boolean;
  begin
    Result := (CurrentError.ErrorType = eetError) and actShowErrors.Enabled and
      btnShowErrors.Down;
  end;

  function ShowWarning: Boolean;
  begin
    Result := (CurrentError.ErrorType = eetWarning) and actShowWarnings.Enabled and
      btnShowWarnings.Down;
  end;

  function ShowInfo: Boolean;
  begin
    Result := (CurrentError.ErrorType = eetInfo) and actShowInfos.Enabled and
      btnShowInfos.Down;
  end;

  function ShowHint: Boolean;
  begin
    Result := (CurrentError.ErrorType = eetHint) and actShowHints.Enabled and
      btnShowHints.Down;
  end;

  function ShowStat: Boolean;
  begin
    Result := (CurrentError.ErrorType = eetStats) and actShowStats.Enabled and
      btnShowStats.Down;
  end;

  function ShowRunCommand: Boolean;
  begin
    Result := (CurrentError.ErrorType= eetRunCommand) and actShowRunCommands.Enabled and
      btnShowRunCommands.Down;
  end;

  function ShowToDo: Boolean;
  begin
    Result := (CurrentError.ErrorType= eetToDo) and actShowToDos.Enabled and
      btnShowToDos.Down;
  end;

begin
  treeErrors.BeginUpdate;
  treeErrors.Clear;
  try
    for i := 0 to fErrorList.Count - 1 do
    begin
      CurrentError := fErrorList.Objects[i] as TEMGAATSError;
      if ShowError or ShowWarning or ShowInfo or ShowHint or ShowStat or
        ShowRunCommand or ShowToDo then
      begin
        CurrentNode := treeErrors.Add;
        CurrentNode.AssignValues([CurrentError.Msg, Integer(CurrentError.ErrorType),
          FormatDateTime('mm/dd hh:nn:ss.zzz', CurrentError.LogTime)]);
        CurrentNode.ImageIndex := Integer(CurrentError.ErrorType);
        CurrentNode.SelectedIndex := CurrentNode.ImageIndex;
      end;
    end;
  finally
    treeErrors.EndUpdate;
  end;
end;

procedure TfrmBuildReport.btnReturnToBuildClick(Sender: TObject);
begin
  frmBuild.Show;
end;

procedure TfrmBuildReport.ResetErrorCount;
begin
  fErrorList.Clear;
  fNumErrors := 0;
  fNumWarnings := 0;
  fNumInfos := 0;
  fNumHints := 0;
  fNumStats := 0;
  fNumRunCommands := 0;
  fNumToDos := 0;
end;

procedure TfrmBuildReport.RzButton2Click(Sender: TObject);
begin
  if Assigned(fMapInfoDisplayManager) then
  begin
    fMapInfoDisplayManager.MinimizeMapWindow;
  end;
end;

procedure TfrmBuildReport.RzButton3Click(Sender: TObject);
begin
  if Assigned(fMapInfoDisplayManager) then
  begin
    fMapInfoDisplayManager.MaximizeMapWindow;
  end;
end;

procedure TfrmBuildReport.RzButton4Click(Sender: TObject);
begin
  if Assigned(fMapInfoDisplayManager) then
  begin
    fMapInfoDisplayManager.RestoreMapWindow;
  end;
end;

end.
