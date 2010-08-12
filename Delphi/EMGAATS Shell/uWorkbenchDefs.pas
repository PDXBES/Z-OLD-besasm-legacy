{==uWorkbenchDefs Unit==========================================================

	Constants for communicating with MapInfo EMGWorkbench

	Revision History
	3.0    03/07/2003 (AMM) Initial inclusion

===============================================================================}

unit uWorkbenchDefs;

interface

const
	//-EMGWorkbench------------------------------------------------------------
	EMG_AppName = 'EMGWorkbench';

	// Laterals menu
	EMG_ShowLatEndDialog = '4001'; // Common: EMGLaterals.mb
	EMG_ShowLatSnapDialog = '4002'; // Common: EMGLaterals.mb
	EMG_ShowLatComputeDialog = ''; // no ID defined in EMGMenus.mb; Common: EMGLaterals.mb
	EMG_ReSnapModelLaterals = '4050'; // Common: EMGLaterals.mb

	// ModelBuilding menu
	EMG_DoAllTraceLinks = '5020'; // EMG: EMGTraceLinks.mb
	EMG_RebuildTraceLinks = ''; // no ID defined in EMGMenus.mb; EMG: EMGTraceLinks.mb

	// HiddenModels menu
	EMG_SetGlobals = '5000'; // EMG: EMGTraceLinks.mb
	EMG_OpenTables = '5004'; // EMG: EMGInit.mb
	EMG_OpenTablesDenyWrite = '5005'; // EMG: EMGInit.mb
	EMG_BuildMDL_Links = '5001'; // EMG: EMGTraceLinks.mb
	EMG_CreateModelTables = '5002'; // Common: EMGModMake.mb
	EMG_GetNodes = '5003'; // EMG: EMGTraceLinks.mb
	EMG_RelateDSCtoSurfSC = '5006'; // EMG: EMGBuildDeck.mb
  EMG_ADDICTables = '5007'; // EMG: EMGBuildDeck.mb
  EMG_INITHydPre = '5008'; // EMG: EMGinitDSC.mb - NOT USED
  EMG_INITHydPost = '5009'; // EMG: EMGinitDSC.mb - NOT USED
  EMG_QAICmax = '5010'; // EMG: EMGinitDSC.mb
  EMG_QADupDsc = '5011'; // EMG: EMGinitDSC.mb

	// AlternateLateral menu
	EMG_TraceLaterals = '6007'; // Common: EMGLaterals.mb
	EMG_CalcDSC = '6008'; // EMG: EMGCalcDSC.mb
	EMG_DeleteUnpickedLatsFromMaster = '6009'; // Common: EMGLaterals.mb
	EMG_TraceSurfaceSC = '6010'; // EMG: EMGSurfSC.mb
	EMG_MakeSurfZingers = '6011'; // EMG: EMGSurfSC.mb
	EMG_RetraceSurfaceSC = '6012'; // EMG: EMGSurfSC.mb
  EMG_TraceDSCs = '6013'; // EMG: EMGLaterals.mb

	// EMGWorkbench menu
	EMG_OpenModel = ''; // no ID defined in EMGMenus.mb; EMG: EMGInit.mb
	EMG_Laterals = ''; // no ID defined in EMGMenus.mb
	EMG_AlternateLateralMenu = ''; // no ID defined in EMGMenus.mb
	EMG_ModelBuilding = ''; // no ID defined in EMGMenus.mb
	EMG_HiddenModels = ''; // no ID defined in EMGMenus.mb
	EMG_Help = ''; // no ID defined in EMGMenus.mb
	EMG_ShowAbout = ''; // no ID defined in EMGMenus.mb; EMG: EMGAbout.mb
	EMG_EndEMGAATS = '5005'; // EMG: EMGEnd.mb


	//-QCWorkspace------------------------------------------------------------
	QCW_AppName = 'QCWorkspace';

  // QCWorkspace menu
  QCW_QCWorkspace = '6700'; // QC: QAQC_wor4.mb
  
implementation

end.
