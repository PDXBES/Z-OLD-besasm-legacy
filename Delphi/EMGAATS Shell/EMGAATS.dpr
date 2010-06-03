program EMGAATS;

{%TogetherDiagram 'ModelSupport_EMGAATS\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fgetTimeFrame\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fPerformQualityControl\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\GlobalConstants\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fDeployModelToEngine\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fSplash\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\EMGAATS_TLB\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fBuildSubcatchments\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fStatus\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fBuildLateralsAndParcels\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fWelcome\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fModifyModelBoundaries\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uConstants\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fMain\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\dmStateMachine\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uConfigFiles\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\utilMSaccess\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fModelResults\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fgetStormOption\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\dmMapinfo\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uUtilities\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uEMGAATSModelConfig\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fChild\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\unitmodelcontrols\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\EMGAATS\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fBuildPipeSystem\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\dCopyBoundaries\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uXLSconstants\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uEMGAATSTypes\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\dAbout\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uWorkbenchDefs\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fBuildModels\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fLabeledChild\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uEMGAATSSystemConfig\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\default.txvpck'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uQCWorkspaceManager\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\dCheckProcesses\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uMRUStringList\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uMIConstants\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\dmTracer\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uEMGWorkbenchManager\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\dOptions\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uMSAccessManager\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fBuild\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uMapInfoManager\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uEMGAATSModelTemplateConfig\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fBuildReport\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\dModelExists\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uEMGAATSModelCommands\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\dmHydroStats\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uEMGAATSModel\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uXLSconstants\default.txvpck'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uConstants\default.txvpck'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fBuild\default.txvpck'}
{%TogetherDiagram 'ModelSupport_EMGAATS\EMGAATS\default.txvpck'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uBuildResultViewInstruction\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\fViewResults\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\uXPResultsProcessor\default.txaPackage'}
{%TogetherDiagram 'ModelSupport_EMGAATS\dmXPExport\default.txaPackage'}

uses
  Forms,
  Controls,
  StdStyleActnCtrls,
  SysUtils,
  fMain in 'fMain.pas' {frmMain},
  fChild in 'fChild.pas' {frmChild},
  fLabeledChild in 'fLabeledChild.pas' {frmLabeledChild},
  fBuildModels in 'fBuildModels.pas' {frmBuildModels},
  fModifyModelBoundaries in 'fModifyModelBoundaries.pas' {frmModifyModelBoundaries},
  fBuildPipeSystem in 'fBuildPipeSystem.pas' {frmBuildPipeSystem},
  fBuildLateralsAndParcels in 'fBuildLateralsAndParcels.pas' {frmBuildLateralsAndParcels},
  fBuildSubcatchments in 'fBuildSubcatchments.pas' {frmBuildSubcatchments},
  dmStateMachine in 'dmStateMachine.pas' {StateMachine: TDataModule},
  GlobalConstants in 'GlobalConstants.pas',
  fDeployModelToEngine in 'fDeployModelToEngine.pas' {frmDeployModel},
  dmMapinfo in 'dmMapinfo.pas' {CobjMICB: CoClass},
  fgetStormOption in 'fgetStormOption.pas' {frmStormOption},
  utilMSaccess in 'utilMSaccess.pas',
  unitmodelcontrols in 'unitmodelcontrols.pas',
  fSplash in 'fSplash.pas' {frmSplash},
  uWorkbenchDefs in 'uWorkbenchDefs.pas',
  fgetTimeFrame in 'fgetTimeFrame.pas' {frmTimeFrame},
  fModelResults in 'fModelResults.pas' {frmModelResults},
  fStatus in 'fStatus.pas' {frmStatus},
  fPerformQualityControl in 'fPerformQualityControl.pas' {frmPerformQualityControl},
  dCopyBoundaries in 'dCopyBoundaries.pas' {dlgCopyBoundaries},
  EMGAATS_TLB in 'EMGAATS_TLB.pas',
  uUtilities in 'uUtilities.pas',
  pngimage,
  Windows,
  Graphics,
  Classes,
  Dialogs,
  dAbout in 'dAbout.pas' {dlgAbout},
  fWelcome in 'fWelcome.pas' {frmWelcome},
  uEMGAATSTypes in 'uEMGAATSTypes.pas',
  uEMGAATSSystemConfig in 'uEMGAATSSystemConfig.pas',
  uEMGAATSModelConfig in 'uEMGAATSModelConfig.pas',
  uConfigFiles in 'uConfigFiles.pas',
  uMRUStringList in 'uMRUStringList.pas',
  uEMGAATSModel in 'uEMGAATSModel.pas',
  uEMGWorkbenchManager in 'uEMGWorkbenchManager.pas',
  uMSAccessManager in 'uMSAccessManager.pas',
  uEMGAATSModelTemplateConfig in 'uEMGAATSModelTemplateConfig.pas',
  fBuild in 'fBuild.pas' {frmBuild},
  dCheckProcesses in 'dCheckProcesses.pas' {dlgCheckProcesses},
  dmTracer in 'dmTracer.pas' {datmodTracer: TDataModule},
  dOptions in 'dOptions.pas' {dlgOptions},
  dModelExists in 'dModelExists.pas' {dlgModelExists},
  dmHydroStats in 'dmHydroStats.pas' {datmodHydroStats: TDataModule},
  fBuildReport in 'fBuildReport.pas' {frmBuildReport},
  uMapInfoManager in 'uMapInfoManager.pas',
  uMIConstants in 'uMIConstants.pas',
  uEMGAATSModelCommands in 'uEMGAATSModelCommands.pas',
  uQCWorkspaceManager in 'uQCWorkspaceManager.pas',
  fViewResults in 'fViewResults.pas' {frmViewResults},
  uXPResultsProcessor in 'uXPResultsProcessor.pas',
  dmXPExport in 'dmXPExport.pas' {dmodXPExport: TDataModule},
  uBuildResultViewInstruction in 'uBuildResultViewInstruction.pas';

{$R *.TLB}

{$R *.res}
var
	PNGImage: TPNGObject;
	DesktopDC: HDC;
	DesktopCanvas: TCanvas;
	DesktopImage: TBitmap;
	CopyRect: TRect;
	DestRect: TRect;
	SplashLeft: Integer;
	SplashTop: Integer;
	VersionText: String;
begin
  Application.Initialize;

  SystemConfig := TSystemConfig.Create;

	Application.CreateForm(TfrmMain, frmMain);
  Application.CreateForm(TdlgAbout, dlgAbout);
  Application.CreateForm(TdatmodTracer, datmodTracer);
  Application.CreateForm(TdlgOptions, dlgOptions);
  Application.CreateForm(TdlgModelExists, dlgModelExists);
  Application.CreateForm(TdatmodHydroStats, datmodHydroStats);
  // Load the splash screen if desired
  if SystemConfig.ShowSplashScreen then
  begin
    PNGImage := TPNGObject.Create;
    PNGImage.LoadFromFile('EMGAATSSplash.png');

    SplashLeft := (Screen.Width - PNGImage.Width) div 2;
    SplashTop := (Screen.Height - PNGImage.Height) div 2;

    // Copy desktop image so we can copy it back
    DesktopImage := TBitmap.Create;
    DesktopDC := GetDC(0);
    DesktopCanvas := TCanvas.Create;
    DesktopCanvas.Handle := DesktopDC;
    CopyRect := Rect(SplashLeft, SplashTop, SplashLeft + PNGImage.Width,
      SplashTop + PNGImage.Height);
    DesktopImage.Width := CopyRect.Right - CopyRect.Left + 1;
    DesktopImage.Height := CopyRect.Bottom - CopyRect.Top + 1;
    DestRect := Rect(0, 0, DesktopImage.Width-1, DesktopImage.Height-1);
    DesktopImage.Canvas.CopyRect(DestRect, DesktopCanvas, CopyRect);

    // Draw the splash screen
    PNGImage.Draw(DesktopCanvas, Rect(SplashLeft, SplashTop, SplashLeft + PNGImage.Width,
      SplashTop + PNGImage.Height));
    VersionText := 'Version ' + dlgAbout.versionInfo.FileVersion;
    DesktopCanvas.TextOut(Screen.Width div 2 - PNGImage.Width div 2 + 35,
      Screen.Height div 2 + PNGImage.Height div 2 - 40, VersionText);
    PNGImage.Free;
    Sleep(2000);
  end;

  Application.CreateForm(TStateMachine, StateMachine);
  Application.CreateForm(TfrmStormOption, frmStormOption);
  Application.CreateForm(TfrmTimeFrame, frmTimeFrame);
  Application.CreateForm(TfrmStatus, frmStatus);
  Application.CreateForm(TdlgCopyBoundaries, dlgCopyBoundaries);

  // Draw the original desktop image if splash was shown
  if SystemConfig.ShowSplashScreen then
  begin
    DesktopCanvas.Draw(SplashLeft, SplashTop, DesktopImage);
    ReleaseDC(0, DesktopDC);
    DesktopImage.Free;
    DesktopCanvas.Free;
  end;

  // Check processes
  if SystemConfig.ShowVitalProgramsWarning then
  begin
    dlgCheckProcesses := TdlgCheckProcesses.Create(Application);
    if dlgCheckProcesses.EMGAATSProcessesActive then
      dlgCheckProcesses.ShowModal;
    dlgCheckProcesses.Close;
    FreeAndNil(dlgCheckProcesses);
  end
  else
  begin
    dlgCheckProcesses := TdlgCheckProcesses.Create(Application);
    if dlgCheckProcesses.EMGAATSProcessesActive then
    begin
      if (SystemConfig.VitalProgramsAction = vpaExit)  then
        MessageDlg('MapInfo and Access programs are running. Please close them and ' +
                   'restart EMGAATS.'#13#13'To change how EMGAATS starts when MapInfo and/or ' +
                   'Access is running, check "Show vital programs warning on startup" ' +
                   'in Tools, Options after completing the above task.', mtInformation,
                   [mbOK], 0);
      dlgCheckProcesses.Execute;
    end;
    dlgCheckProcesses.Close;
    FreeAndNil(dlgCheckProcesses);
  end;

	Application.Run;
end.
