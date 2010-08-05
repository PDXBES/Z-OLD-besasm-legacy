unit fViewResults;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, fLabeledChild, StdCtrls, RzLabel, pngimage, RzBckgnd, ExtCtrls,
  RzPanel, RzCmboBx, Mask, RzEdit, RzButton, RzLstBox, RzShellDialogs, Contnrs,
  RzRadGrp, RzPrgres, Buttons, PngSpeedButton;

type
  TfrmViewResults = class(TfrmLabeledChild)
    pnlBuildViewsHolder: TRzPanel;
    pnlViewResults: TRzPanel;
    pnlProgress: TRzPanel;
    lblStatus: TRzLabel;
    dlgSelectFolder: TRzSelectFolderDialog;
    prgStatus: TRzProgressBar;
    pnlBuildViewResults: TRzPanel;
    RzLabel6: TRzLabel;
    lstBuildInstructions: TRzListBox;
    btnAdd: TRzButton;
    RzPanel2: TRzPanel;
    RzLabel9: TRzLabel;
    cmbModelDirectory: TRzMRUComboBox;
    btnBrowseModelDirectory: TRzButton;
    grpSpecifyTitleBlock: TRzGroupBox;
    RzLabel2: TRzLabel;
    RzLabel3: TRzLabel;
    RzLabel7: TRzLabel;
    edtProjectNumber: TRzEdit;
    edtProjectDescription: TRzEdit;
    edtStudyAreaID: TRzEdit;
    grpSpecifyView: TRzGroupBox;
    lstAvailableResults: TRzListBox;
    lstAvailableWorkspaces: TRzListBox;
    lstAvailableSizes: TRzListBox;
    rgrpMapOrientation: TRzRadioGroup;
    rgrpLegendOrientation: TRzRadioGroup;
    RzLabel1: TRzLabel;
    RzLabel4: TRzLabel;
    RzLabel10: TRzLabel;
    lblAvailableResults: TRzLabel;
    RzLabel5: TRzLabel;
    RzLabel8: TRzLabel;
    btnHome: TPngSpeedButton;
    btnAddResultFile: TRzButton;
    RzPanel1: TRzPanel;
    btnHomeAlt: TRzButton;
    pnlBuildViewsActions: TRzPanel;
    btnBuildResultViews: TRzButton;
    procedure btnBrowseModelDirectoryClick(Sender: TObject);
    procedure cmbModelDirectorySelect(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure btnBuildResultViewsClick(Sender: TObject);
    procedure btnAddClick(Sender: TObject);
    procedure edtProjectNumberEnter(Sender: TObject);
    procedure edtProjectNumberExit(Sender: TObject);
    procedure edtProjectDescriptionEnter(Sender: TObject);
    procedure edtProjectDescriptionExit(Sender: TObject);
    procedure edtStudyAreaIDEnter(Sender: TObject);
    procedure edtStudyAreaIDExit(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure cmbModelDirectoryExit(Sender: TObject);
    procedure cmbModelDirectoryEnter(Sender: TObject);
    procedure btnHomeClick(Sender: TObject);
    procedure btnAddResultFileClick(Sender: TObject);
  private
    { Private declarations }
    fRegisteredResultViews: TInterfaceList;
    fBuildInstructions: TObjectList;
    procedure SearchForFile(FileMask: String; Path: String; FileList: TStringList);
    procedure LoadModel;
    procedure RegisterResultViews;
    procedure EnumOutFiles;
    procedure CheckModelDirectoryPrompt;
    procedure CheckProjectNumberPrompt;
    procedure CheckProjectDescriptionPrompt;
    procedure CheckStudyAreaIDPrompt;
    procedure CheckUIPrompts;
    procedure ResizeStatusPanel;
    procedure LoadSettings;
    procedure SaveSettings;
    procedure EnableUI(Enable: Boolean);
    procedure GetResultsFiles;
  public
    { Public declarations }
  end;

var
  frmViewResults: TfrmViewResults;

implementation

uses fMain, uEMGAATSModel, uXPResultsProcessor, GlobalConstants, CodeSiteLogging,
  StStrL, uBuildResultViewInstruction, StrUtils, fWelcome;

resourcestring
  PromptModelDirectory = 'Type a full path and filename, or click Browse...';
  PromptProjectNumber = 'Type in a project number to be used in the view''s title block';
  PromptProjectDescription = 'Type in a project description to be used in the view''s title block';
  PromptStudyAreaID = 'Type in a study area ID/abbreviation describing the area displayed';
  PromptViewsToBuild = 'Click Add -> to add views selected on the left';

{$R *.dfm}

procedure TfrmViewResults.btnAddClick(Sender: TObject);
var
  ABuildInstruction: TBuildResultViewInstruction;
  CurrentResult: Integer;
  CurrentWorkspace: Integer;
  CurrentSize: Integer;
  ResultsFileName: String;
  ResultsDBFileName: String;
  ResultsDBPath: String;
  XPResultsProcessor: TXPResultsProcessor;
  ArchiveDatabaseResult: Integer;
  CurrentResultsDir: String;
  NewArchiveDir: String;
  NewArchiveNum: Integer;
  DeleteSearch: TSearchRec;
  DeleteSearchResult: Integer;

  procedure CreateResultsDatabase;
  begin
    // Create results database
    pnlProgress.Show;
    lblStatus.Caption := 'Creating results database ' + ResultsDBFileName;
    Application.ProcessMessages;

    XPResultsProcessor := TXPResultsProcessor.Create;
    XPResultsProcessor.ProcessResults(ResultsFileName, ResultsDBFileName);
    XPResultsProcessor.Free;

    lblStatus.Caption := '';
    pnlProgress.Hide;
    Application.ProcessMessages;
  end;

begin
  if (lstAvailableResults.SelCount = 0) or (lstAvailableWorkspaces.SelCount = 0) or
    (lstAvailableSizes.SelCount = 0) then
  begin
    ShowMessage('You must select at least one result file, one workspace/map, and one size');
    Exit;
  end;

  ResizeStatusPanel;

  for CurrentResult := 0 to lstAvailableResults.Count - 1 do
  begin
    if lstAvailableResults.Selected[CurrentResult] then
    begin
      for CurrentWorkspace := 0 to lstAvailableWorkspaces.Count - 1 do
      begin
        if lstAvailableWorkspaces.Selected[CurrentWorkspace] then
        begin
          for CurrentSize := 0 to lstAvailableSizes.Count - 1 do
          begin
            if lstAvailableSizes.Selected[CurrentSize] then
            begin
              // If the user specified an exact path to the out file...
              if ExtractFileDrive(lstAvailableResults.Items[CurrentResult]) <> '' then
              begin
                ResultsDBPath := ExtractFilePath(lstAvailableResults.Items[CurrentResult]) + ModelResultsDirName + '\';
                ForceDirectories(ResultsDBPath);
                ResultsDBFileName := ResultsDBPath + JustNameL(lstAvailableResults.Items[CurrentResult]) +
                  '_' + ModelResultsStoreFileName;
                ResultsFileName := lstAvailableResults.Items[CurrentResult];
              end
              // Otherwise, the user specified a standard storm...
              else
              begin
                ResultsDBPath := IncludeTrailingPathDelimiter(CurrentModel.Path) + 'sim\'
                  + ExtractFilePath(lstAvailableResults.Items[CurrentResult]) +
                  ModelResultsDirName + '\' ;
                ForceDirectories(ResultsDBPath);
                ResultsDBFileName := ResultsDBPath + JustNameL(lstAvailableResults.Items[CurrentResult]) + '_' + ModelResultsStoreFileName;
                ResultsFileName := IncludeTrailingPathDelimiter(CurrentModel.Path) + 'sim\' + lstAvailableResults.Items[CurrentResult];
              end;

              if FileExists(ResultsDBFileName) then
              begin
                ArchiveDatabaseResult := MessageDlg('File ' + resultsDBFileName +
                ' exists.  Archive?', mtWarning, mbYesNoCancel, 0,
                mbYes);

                CurrentResultsDir := ExcludeTrailingPathDelimiter(ExtractFileDir(ResultsDBFileName));
                if ArchiveDatabaseResult = mrYes then
                begin
                  // Find the best name for the archive dir (ModelResults0x)
                  NewArchiveNum := 1;
                  while DirectoryExists(Format('%s%.2d', [CurrentResultsDir, NewArchiveNum])) do
                    Inc(NewArchiveNum);
                  NewArchiveDir := Format('%s%.2d', [CurrentResultsDir, NewArchiveNum]);
                  // Move files from the current directory to the archive dir
                  MoveFile(PAnsiChar(CurrentResultsDir), PAnsiChar(NewArchiveDir));
                  ForceDirectories(CurrentResultsDir);

                  CreateResultsDatabase;
                end
                else if ArchiveDatabaseResult = mrNo then
                begin
                  // Pass through to the next steps
                end
                else
                begin
                  Exit;
                end;
              end
              else
              begin
                CreateResultsDatabase;
              end;

              ABuildInstruction := TBuildResultViewInstruction.Create(
                ResultsDBFileName,
                TResultViewMapOrientation(rgrpMapOrientation.ItemIndex),
                TResultViewLegendOrientation(rgrpLegendOrientation.ItemIndex),
                TResultViewPaperSize(CurrentSize),
                fRegisteredResultViews[CurrentWorkspace] as IResultViewWorkspaceSetup);

              if edtProjectNumber.Text = PromptProjectNumber then
                ABuildInstruction.ProjectNumber := ''
              else
                ABuildInstruction.ProjectNumber := edtProjectNumber.Text;

              if edtProjectDescription.Text = PromptProjectDescription then
                ABuildInstruction.ProjectDesc := ''
              else
                ABuildInstruction.ProjectDesc := edtProjectDescription.Text;

              if edtStudyAreaID.Text = PromptStudyAreaID then
                ABuildInstruction.StudyArea := ''
              else
                ABuildInstruction.StudyArea := edtStudyAreaID.Text;

              ABuildInstruction.Title := Format('%s,%s,Map:%s,Leg:%s',
                [(fRegisteredResultViews[CurrentWorkspace] as IResultViewWorkspaceSetup).ShortName,
                lstAvailableSizes.Items[CurrentSize],
                rgrpMapOrientation.Items[rgrpMapOrientation.ItemIndex],
                rgrpLegendOrientation.Items[rgrpLegendOrientation.ItemIndex]]);
              lstBuildInstructions.Add(ABuildInstruction.Title);
              fBuildInstructions.Add(ABuildInstruction);
            end;
          end;
        end;
      end;
    end;
  end;

end;

procedure TfrmViewResults.btnAddResultFileClick(Sender: TObject);
begin
  frmMain.dlgOpen.Title := 'Open result file for extraction';
  frmMain.dlgOpen.Filter := 'XP-SWMM Output Files (*.out)|*.out|All Files (*.*)|*.*';
  frmMain.dlgOpen.FilterIndex := 0;
  frmMain.dlgOpen.InitialDir := IncludeTrailingPathDelimiter(CurrentModel.Path) + 'sim';
  if frmMain.dlgOpen.Execute then
  begin
    lstAvailableResults.Add(frmMain.dlgOpen.FileName);
  end;
end;

procedure TfrmViewResults.btnBrowseModelDirectoryClick(Sender: TObject);
begin
  if dlgSelectFolder.Execute then
  begin
    cmbModelDirectory.Text := dlgSelectFolder.SelectedPathName;
    cmbModelDirectory.UpdateMRUList;
    cmbModelDirectory.SaveMRUData;
    LoadModel;
    LoadSettings;
    GetResultsFiles;
    CheckUIPrompts;
    EnableUI(True);
  end;
end;

procedure TfrmViewResults.cmbModelDirectoryEnter(Sender: TObject);
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

procedure TfrmViewResults.cmbModelDirectoryExit(Sender: TObject);
begin
  if (cmbModelDirectory.Text = '') or
    (cmbModelDirectory.Text = PromptModelDirectory) then
  begin
    FreeAndNil(CurrentModel);
    EnableUI(False);
  end
  else
  begin
    LoadModel;
    EnableUI(True);
  end;

  if Assigned(CurrentModel) then
    GetResultsFiles;
  CheckUIPrompts;
end;

procedure TfrmViewResults.cmbModelDirectorySelect(Sender: TObject);
begin
  LoadModel;
  EnableUI(True);
  GetResultsFiles;

  CheckUIPrompts;
end;

procedure TfrmViewResults.edtProjectDescriptionEnter(Sender: TObject);
begin
  if edtProjectDescription.Text = PromptProjectDescription then
  begin
    edtProjectDescription.Text := '';
    edtProjectDescription.Font.Color := clWindowText;
  end;
end;

procedure TfrmViewResults.edtProjectDescriptionExit(Sender: TObject);
begin
  if edtProjectDescription.Text = '' then
  begin
    edtProjectDescription.Text := PromptProjectDescription;
    edtProjectDescription.Font.Color := clGrayText;
  end
  else
    edtProjectNumber.Font.Color := clWindowText;
end;

procedure TfrmViewResults.edtProjectNumberEnter(Sender: TObject);
begin
  if edtProjectNumber.Text = PromptProjectNumber then
  begin
    edtProjectNumber.Text := '';
    edtProjectNumber.Font.Color := clWindowText;
  end;
end;

procedure TfrmViewResults.edtProjectNumberExit(Sender: TObject);
begin
  if edtProjectNumber.Text = '' then
  begin
    edtProjectNumber.Text := PromptProjectNumber;
    edtProjectNumber.Font.Color := clGrayText;
  end
  else
    edtProjectNumber.Font.Color := clWindowText;
end;

procedure TfrmViewResults.edtStudyAreaIDEnter(Sender: TObject);
begin
  if edtStudyAreaID.Text = PromptStudyAreaID then
  begin
    edtStudyAreaID.Text := '';
    edtStudyAreaID.Font.Color := clWindowText;
  end;
end;

procedure TfrmViewResults.edtStudyAreaIDExit(Sender: TObject);
begin
  if edtStudyAreaID.Text = '' then
  begin
    edtStudyAreaID.Text := PromptStudyAreaID;
    edtStudyAreaID.Font.Color := clGrayText;
  end
  else
    edtStudyAreaID.Font.Color := clWindowText;
end;

procedure TfrmViewResults.EnableUI(Enable: Boolean);
begin
  pnlBuildViewResults.Enabled := Enable;
  pnlBuildViewsActions.Enabled := Enable;
  grpSpecifyTitleBlock.Enabled := Enable;
  grpSpecifyView.Enabled := Enable;
end;

procedure TfrmViewResults.GetResultsFiles;
var
  FileList: TStringList;
  i: Integer;
  CurrentStorm: string;
  ResultsDBPath: string;
  SimPathLength: Integer;
  FileListCounter: Integer;
  j: Integer;
  ResultsDBFileName: string;
begin
  lblAvailableResults.Caption := 'Available results (' + CurrentModel.Path + 'sim\)';
  lstAvailableResults.Clear;
  FileList := TStringList.Create;
  for i := 0 to CurrentModel.Config.StormsToBuildCount - 1 do
  begin
    FileList.Clear;
    CurrentStorm := CurrentModel.Config.StormsToBuild[i];
    ResultsDBPath := IncludeTrailingPathDelimiter(CurrentModel.Config.Path) + 'sim\' + CurrentStorm;
    SearchForFile('*.out', ResultsDBPath, FileList);
    SimPathLength := Length(IncludeTrailingPathDelimiter(CurrentModel.Config.Path) + 'sim\');
    for FileListCounter := 0 to FileList.Count - 1 do
      FileList[FileListCounter] := RightStr(FileList[FileListCounter], Length(FileList[FileListCounter]) - SimPathLength);
    CodeSite.Send('Searching out files in ' + ResultsDBPath);
    for j := 0 to FileList.Count - 1 do
    begin
      CodeSite.SendFmtMsg('FileList[%d]=%s', [j, FileList[j]]);
      ResultsDBFileName := IncludeTrailingPathDelimiter(ResultsDBPath) + JustNameL(FileList[j]) + '_' + ModelResultsStoreFileName;
    end;
    lstAvailableResults.Items.AddStrings(FileList);
  end;
  FileList.Free;
end;

procedure TfrmViewResults.EnumOutFiles;
var
  i: Integer;
  CurrentStorm: String;
  XPResultsProcessor: TXPResultsProcessor;
  ResultsDBPath: String;
  ResultsDBFileName: String;
  FileList: TStringList;
  j: Integer;
begin
  lstAvailableResults.Clear;
  FileList := TStringList.Create;
  for i := 0 to CurrentModel.Config.StormsToBuildCount - 1 do
  begin
    FileList.Clear;
    CurrentStorm := CurrentModel.Config.StormsToBuild[i];
    ResultsDBPath := IncludeTrailingPathDelimiter(CurrentModel.Config.Path) +
      'sim\' + CurrentStorm;
    SearchForFile('*.out', ResultsDBPath, FileList);
    lstAvailableResults.Items.AddStrings(FileList);
  end;
  FileList.Free;
end;

procedure TfrmViewResults.FormCreate(Sender: TObject);
begin
  inherited;
  fRegisteredResultViews := TInterfaceList.Create;
  RegisterResultViews;
  fBuildInstructions := TObjectList.Create(True);
end;

procedure TfrmViewResults.FormDestroy(Sender: TObject);
begin
  fRegisteredResultViews.Free;
  fBuildInstructions.Free;
  FreeAndNil(CurrentModel);
  inherited;
end;

procedure TfrmViewResults.FormShow(Sender: TObject);
begin
  inherited;
  CheckUIPrompts;
end;

procedure TfrmViewResults.LoadModel;
begin
  if cmbModelDirectory.Text <> PromptModelDirectory then
  begin
    if Assigned(CurrentModel) then
      CurrentModel.Free;
    CurrentModel := TEMGAATSModel.Create(cmbModelDirectory.Text);
    LoadSettings;
  end;
end;

procedure TfrmViewResults.LoadSettings;
begin
  edtProjectNumber.Text := CurrentModel.Config.ResultViewsProjectNumber;
  edtProjectDescription.Text := CurrentModel.Config.ResultViewsProjectDescription;
  edtStudyAreaID.Text := CurrentModel.Config.ResultViewsStudyAreaID;
end;

procedure TfrmViewResults.RegisterResultViews;
var
  i: Integer;
begin
  fRegisteredResultViews.Add(TBasementSewerBackupRiskWorkspace.Create(nil, nil));
  fRegisteredResultViews.Add(TSewerSurchargeRiskWorkspace.Create(nil, nil));
  fRegisteredResultViews.Add(TSystemWorkspace.Create(nil, nil));
  fRegisteredResultViews.Add(TTopographyWorkspace.Create(nil, nil));
  fRegisteredResultViews.Add(TLargeUtilitiesTrafficWorkspace.Create(nil, nil));
  fRegisteredResultViews.Add(TAreaExistingLandUseWorkspace.Create(nil, nil));
  fRegisteredResultViews.Add(TComprehensivePlanWorkspace.Create(nil, nil));
  fRegisteredResultViews.Add(TEnvironmentalConsiderationsWorkspace.Create(nil, nil));
  fRegisteredResultViews.Add(TImpPctIncreaseWorkspace.Create(nil, nil));
  fRegisteredResultViews.Add(TExistingLandUseDSCWorkspace.Create(nil, nil));
  fRegisteredResultViews.Add(TComprehensivePlanDSCWorkspace.Create(nil, nil));
//  fRegisteredResultViews.Add(TSystemQAQCWorkspace.Create(nil, nil));

  // Fill in the available workspaces list
  lstAvailableWorkspaces.Clear;
  for i := 0 to fRegisteredResultViews.Count - 1 do
    lstAvailableWorkspaces.Add((fRegisteredResultViews[i] as IResultViewWorkspaceSetup).Title);
end;

procedure TfrmViewResults.ResizeStatusPanel;
const
  ProgressPanelMargin = 500;
begin
  pnlProgress.Top := (pnlViewResults.Height - pnlProgress.Height) div 2 + pnlViewResults.Top;
  pnlProgress.Width := pnlViewResults.Width - ProgressPanelMargin;
  pnlProgress.Left := ProgressPanelMargin div 2;
end;

procedure TfrmViewResults.btnBuildResultViewsClick(Sender: TObject);
var
  i: Integer;
begin
  SaveSettings;
  ResizeStatusPanel;
  prgStatus.TotalParts := fBuildInstructions.Count;
  prgStatus.Show;

  pnlProgress.Show;
  Screen.Cursor := crHourglass;
  try
    for i := 0 to fBuildInstructions.Count - 1 do
    begin
      lblStatus.Caption := Format('Building View %d of %d'#10'%s',
        [i+1, fBuildInstructions.Count,
        (fBuildInstructions[i] as TBuildResultViewInstruction).Title]);
      prgStatus.IncPartsByOne;
      Application.ProcessMessages;
      (fBuildInstructions[i] as TBuildResultViewInstruction).Build;
    end;
  finally
    pnlProgress.Hide;
    prgStatus.Hide;
    Screen.Cursor := crDefault;
  end;
end;

procedure TfrmViewResults.btnHomeClick(Sender: TObject);
begin
  frmWelcome.Show;
  Close;
  FreeAndNil(frmViewResults);
end;

procedure TfrmViewResults.CheckModelDirectoryPrompt;
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

procedure TfrmViewResults.CheckProjectDescriptionPrompt;
begin
  if edtProjectDescription.Text = PromptProjectDescription then
    edtProjectDescription.Font.Color := clGrayText
  else if edtProjectDescription.Text = '' then
  begin
    edtProjectDescription.Text := PromptProjectDescription;
    edtProjectDescription.Font.Color := clGrayText;
  end
  else
    edtProjectDescription.Font.Color := clWindowText;
end;

procedure TfrmViewResults.CheckProjectNumberPrompt;
begin
  if edtProjectNumber.Text = PromptProjectNumber then
    edtProjectNumber.Font.Color := clGrayText
  else if edtProjectNumber.Text = '' then
  begin
    edtProjectNumber.Text := PromptProjectNumber;
    edtProjectNumber.Font.Color := clGrayText;
  end
  else
    edtProjectNumber.Font.Color := clWindowText;
end;

procedure TfrmViewResults.CheckStudyAreaIDPrompt;
begin
  if edtStudyAreaID.Text = PromptStudyAreaID then
    edtStudyAreaID.Font.Color := clGrayText
  else if edtStudyAreaID.Text = '' then
  begin
    edtStudyAreaID.Text := PromptStudyAreaID;
    edtStudyAreaID.Font.Color := clGrayText;
  end
  else
    edtStudyAreaID.Font.Color := clWindowText;
end;

procedure TfrmViewResults.CheckUIPrompts;
begin
  CheckModelDirectoryPrompt;
  CheckProjectNumberPrompt;
  CheckProjectDescriptionPrompt;
  CheckStudyAreaIDPrompt;
end;

procedure TfrmViewResults.SaveSettings;
begin
  if edtProjectNumber.Text <> PromptProjectNumber then
    CurrentModel.Config.ResultViewsProjectNumber := edtProjectNumber.Text;
  if edtProjectDescription.Text <> PromptProjectDescription then
    CurrentModel.Config.ResultViewsProjectDescription := edtProjectDescription.Text;
  if edtStudyAreaID.Text <> PromptStudyAreaID then
    CurrentModel.Config.ResultViewsStudyAreaID := edtStudyAreaID.Text;
  CurrentModel.Config.Update;
end;

procedure TfrmViewResults.SearchForFile(FileMask: String; Path: String;
  FileList: TStringList);
var
  SearchRec: TSearchRec;
begin
  CodeSite.EnterMethod('SearchForFile');
  Path := IncludeTrailingPathDelimiter(Path);
  CodeSite.Send('Path', Path);
  if FindFirst(Path + FileMask, faAnyFile - faDirectory, SearchRec) = 0 then
  try
    repeat
      FileList.Add(Path + SearchRec.Name);
    until FindNext(SearchRec) <> 0;
  finally
    FindClose(SearchRec)
  end;
  CodeSite.ExitMethod('SearchForFile');
end;

end.
