program InterfaceMaster;

{%TogetherDiagram 'ModelSupport\default.txaPackage'}

uses
  ShareMem,
  Forms,
  SysUtils,
  Controls,
  fMain in 'fMain.pas' {frmMain},
  fModuleMaster in 'fModuleMaster.pas' {frmModuleMaster},
  InterfaceStreams in 'InterfaceStreams.pas',
  fConvertInterface in 'fConvertInterface.pas' {frmConvertInterface},
  PDXDateUtils in 'PDXDateUtils.pas',
  fCalculateFlowStatistics in 'fCalculateFlowStatistics.pas' {frmCalculateFlowStatistics},
  fPrepareVirtualGages in 'fPrepareVirtualGages.pas' {frmPrepareVirtualGages},
  dSpecifyExtractDates in 'dSpecifyExtractDates.pas' {dlgSpecifyExtractDates},
  dToggleRange in 'dToggleRange.pas' {dlgToggleRange},
  dSelectQuartersections in 'dSelectQuartersections.pas' {dlgSelectQuartersections},
  uMIConstants in 'uMIConstants.pas',
  dOptions in 'dOptions.pas' {dlgOptions},
  fMultiCombineInterfaceFiles in 'fMultiCombineInterfaceFiles.pas' {frmMultiCombineInterfaceFiles},
  dAbout in 'dAbout.pas' {dlgAbout},
  fStatus in 'fStatus.pas' {frmStatus},
  dExtractMOUSEPRFNodes in 'dExtractMOUSEPRFNodes.pas' {dlgExtractMOUSEPRFNodes},
  fViewInterface in 'fViewInterface.pas' {frmViewInterface},
  fSplash in 'fSplash.pas' {frmSplash},
  fProgress in 'fProgress.pas' {frmProgress},
  fViewRainfallIInterface in 'fViewRainfallIInterface.pas' {frmViewIRainfallInterfaceFile},
  uIM_InterfaceFiles in 'uIM_InterfaceFiles.pas',
  uIM_SWMM_XP_InterfaceFiles in 'uIM_SWMM_XP_InterfaceFiles.pas',
  uIM_ConvertEngine in 'uIM_ConvertEngine.pas',
  uIM_Command in 'uIM_Command.pas',
  uInterfaceStack in 'uInterfaceStack.pas',
  uIM_MultiCombineEngine in 'uIM_MultiCombineEngine.pas',
  uIM_MOUSE_PRF_InterfaceFiles in 'uIM_MOUSE_PRF_InterfaceFiles.pas',
  uIM_ExpressionEngine in 'uIM_ExpressionEngine.pas',
  uIM_IndexLookup in 'uIM_IndexLookup.pas',
  fBatchConvertCombine in 'fBatchConvertCombine.pas' {frmBatchConvertCombine},
  uIM_BatchConvertCombineSupport in 'uIM_BatchConvertCombineSupport.pas',
  uIM_ProgressTracker in 'uIM_ProgressTracker.pas',
  uIM_PipeUtils in 'uIM_PipeUtils.pas',
  pngimage,
  Windows,
  Graphics,
  Classes,
  uTAggregatedNode in 'FlowDistributorModel\uTAggregatedNode.pas',
  uTConfiguration in 'FlowDistributorModel\uTConfiguration.pas',
  uTDistribution in 'FlowDistributorModel\uTDistribution.pas',
  uTDistributionCombiner in 'FlowDistributorModel\uTDistributionCombiner.pas',
  uTFlowSeriesDatum in 'FlowDistributorModel\uTFlowSeriesDatum.pas',
  uTFlowSeriesList in 'FlowDistributorModel\uTFlowSeriesList.pas',
  uTLink in 'FlowDistributorModel\uTLink.pas',
  uTLinksList in 'FlowDistributorModel\uTLinksList.pas',
  uTNetwork in 'FlowDistributorModel\uTNetwork.pas',
  uTNode in 'FlowDistributorModel\uTNode.pas',
  uTNodeMapping in 'FlowDistributorModel\uTNodeMapping.pas',
  uTNodeMappingsList in 'FlowDistributorModel\uTNodeMappingsList.pas',
  dmIM_FlowDistributionSupport in 'dmIM_FlowDistributionSupport.pas' {dmFlowDistributionSupport: TDataModule},
  uTNodesList in 'FlowDistributorModel\uTNodesList.pas',
  uTFlowFileSpecs in 'FlowDistributorModel\uTFlowFileSpecs.pas',
  fDistributeFlowsToModel in 'fDistributeFlowsToModel.pas' {frmDistributeFlowsToModel},
  uTMainConfigSpecs in 'FlowDistributorModel\uTMainConfigSpecs.pas',
  uIM_SWMM_TEXT_InterfaceFiles in 'uIM_SWMM_TEXT_InterfaceFiles.pas';

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
	VersionTextWidth, VersionTextHeight: Integer;

begin
	Application.Initialize;

	Application.CreateForm(TfrmMain, frmMain);
  Application.CreateForm(TdlgAbout, dlgAbout);
  // Load the splash screen
	PNGImage := TPNGObject.Create;
	PNGImage.LoadFromFile('InterfaceMasterSplash.png');

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

{
		frmSplash := TfrmSplash.Create(Application);
		try
			Screen.Cursor := crHourglass;
			frmSplash.Show;
			frmSplash.Update;
}	Application.CreateForm(TdlgSpecifyExtractDates, dlgSpecifyExtractDates);
	Application.CreateForm(TdlgOptions, dlgOptions);
	Application.CreateForm(TdlgToggleRange, dlgToggleRange);
	Application.CreateForm(TdlgExtractMOUSEPRFNodes, dlgExtractMOUSEPRFNodes);
	Application.CreateForm(TfrmProgress, frmProgress);
	Application.CreateForm(TdmFlowDistributionSupport, dmFlowDistributionSupport);

	// Draw the original desktop image
	DesktopCanvas.Draw(SplashLeft, SplashTop, DesktopImage);
	ReleaseDC(0, DesktopDC);
	DesktopImage.Free;
	DesktopCanvas.Free;

	{		Sleep(5000);
		finally
			frmSplash.Close;
			Screen.Cursor := crDefault;
		end;

}
	Application.Run;
end.
