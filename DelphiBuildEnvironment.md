# Introduction #

This guide instructs how to set up your machine to enable building of EMGAATS 2.2, InterfaceMaster, and other legacy Delphi ASM applications.  Note that this is geared towards EMGAATS 2.2 building, but most of it applies to other legacy Delphi applications.

# Details #

## Installation Procedure ##

  1. Install Delphi 2006
  1. Install components
  1. Test Delphi 2006

## Install Delphi 2006 ##

  1. **Install Borland Developer Studio 2006.**  You can install all languages if you wish, but at the very least make sure Delphi for Win32 is installed.  EMGAATS v2.2 is programmed in Delphi for Win32.  Use the BDS 2006 package (Dave Collins' copy is in the lower drawer of the left vertical file cabinet in my cube).
  1. **Install updates and hotfixes.**  Delphi 2006 patches are found in [[\\OBERON\GRP117\AppInstall\Programming\Delphi\Delphi 2006][\\OBERON\GRP117\AppInstall\Programming\Delphi\Delphi 2006]].  Install the following:
    1. Bds2006\_en\_pro\_upd2.msp: Updates the base install of BDS2006 to Update 2.
    1. BDS2006HotfixRollup2.exe: Hotfixes for BDS2006 Update 2.
  1. **Install optional tools.** There are additional tools I use for Delphi 2006 to make things a tad easier.  These are also in the same directory as step 2.
    1. Castalia 4 Borland Edition for Developer Studio 2006.exe. Provides miscellaneous IDE tools.  Most useful are the Code Navigator it adds to the top of the code pane and the live Find tool (Ctrl+F).
    1. GXBDS2006-130.exe: GExperts 1.30 for BDS 2006.  Provides miscellaneous IDE tools.

## Install Components ##

  1. **Raize Components 4.0.** Install from [[\\OBERON\GRP117\AppInstall\Programming\Delphi\Raize4][\\OBERON\GRP117\AppInstall\Programming\Delphi\Raize4]].  Run RC4.EXE.
    1. Serial numbers: _see Arnel_
    1. Previous serial number for Raize Components 3.0 is likely required: _see Arnel_
  1. **CodeSite 3.** Install from [[\\CASSIO\GRP117\AppInstall\Programming\Delphi\CodeSite3][../../../GRP117/AppInstall/Programming/Delphi/CodeSite3]].  Run CS3.exe first, then run CS3\_BDS2006.exe.  For the first setup program, CS3.exe, you'll need to choose Delphi 2005 as your choice of installation (even though you probably don't have it installed).
    1. Serial numbers: _see Arnel_
  1. **Developer Express.**  Install from [[\\CASSIO\GRP117\AppInstall\Programming\Delphi\DevExpress][../../../GRP117/AppInstall/Programming/Delphi/DevExpress]].  Run DeveloperExpressVCLProducts.exe.
    1. Serial number: _see Arnel_
  1. Custom Components. Copy the directory [[\\CASSIO\GRP117\AppInstall\Programming\Components][../../../GRP117/AppInstall/Programming/Components]] to a local directory (e.g., D:\Development\Components).
    1. Install packages in BDS 2006:
      1. Add the D:\Development\Components\systools4\source (or wherever you copied the above directory to), D:\Development\Components\pngimage, and D:\Development\Components to the library path.  To access the library path, from the main menu click Tools, Options.  From the tree on the left, navigate to Environment Options, Delphi Options, Library, Win32.  Edit the Library path to include the directories I just specified.
      1. Open AMMComponents\_100.bdsgroup.  Right click the AMMComponents.bpl file in the Project Manager pane on the right of the IDE and select Install.  Do the same for the other bpl files.  You'll get errors that the bpl files aren't design-time packages.  Do the same thing again to the S403\_d90.bpl and S403bd90.bpl files and things should be okay.
  1. PNG Components.  Install from [[\\CASSIO\GRP117\AppInstall\Programming\Delphi\PngComponents][../../../GRP117/AppInstall/Programming/Delphi/PngComponents]].  Run PngComponentsSetup.exe.

## Test Delphi 2006 ##

Run BDS 2006 and open the EMGAATS Shell project.  From the directory [[\\CASSIO\Modeling\Model\_Programs\Emgaats\CodeV2.2\EMGAATS Shell][../../../Modeling/Model\_Programs/Emgaats/CodeV2.2/EMGAATS Shell]],  open the file EMGAATS.bdsproj.  All forms should be displayable.

## Required Files for Running the EMGAATS Shell ##

All the files in [[\\CASSIO\Modeling\Model\_Programs\Emgaats\binV2.2][../../../Modeling/Model\_Programs/Emgaats/binV2.2]] are required for EMGAATS to run.  The files are

| **File** | **Description** |
|:---------|:----------------|
| EMGAATS.exe | Main executable for the shell |
| EMGAATSModelTemplate.ini | The "clean" INI file that is copied to a new model directory before they are modified by the shell. |
| EMGAATSSplash.png | Splash-screen graphic displayed at startup. |
| EMGAATSSystem.ini | System preferences that  are common to all users of EMGAATS. |
| EMGAATSUserTemplate.ini |Initial user preference file that is copied to the user's C:\Documents and Settings\_user_\Application Data\BES\_SystemsAnalysis directory if it doesn't exist.  New entries here that are not found in the user's copy are propagated to the copy.  However, existing entries in the user's copy do not get copied over. |
| EMGWorkbench.MBX | MapInfo application required to perform various EMGAATS modeling tasks, such as tracing and workspace creation. |
| QCWorkspace.MBX | MapInfo application required to create quality control workspaces.|
| XPResults.dll | DLL for extracting results.  This is obsolete, but is still required for running until the dependency in EMGAATS is removed in the near future. |

## Development Environment ##

### Working Codebase ###

There is a working codebase in W:\Model\_Programs\EMGAATS\CodeV2.2.  Executables and files indicated above in section 0 are pulled from this codebase.

### Subversion Repository ###

There is a Subversion repository in W:\Model\_Programs\SubversionRepositories for EMGAATS.  To use this repository, install TortoiseSVN (in [[\\OBERON\GRP117\AppInstall\Programming\TortoiseSVN][\\OBERON\GRP117\AppInstall\Programming\TortoiseSVN]]; run the TortoiseSVN-1.4.3.8645-win32-svn-1.4.3.msi installer).  You probably will need to consult some documentation on how TortoiseSVN/Subversion work together for version control.  It is best to check out files from the repository to your local computer instead of writing directly to the network codebase (section 0), which is provided for reference only.

There are three branches underneath EMGAATS:

  * Branches: where alternative development is occurring.  I have one instance here that has to do with increasing application responsiveness with multithreading.  Kind of tanked, but that's why it's in a separate branch of development that doesn't muck up the main branch (the _trunk_, described below)
  * Tags: where frozen copies of versions of EMGAATS are held.  Currently no tags are present.  Ideally, when EMGAATS v2.2 in its current form stabilizes, I will create the tag 2.2.0._build_ (where _build_ is the stable build number) and that will be a frozen version that will not have any development occurring on it.
  * Trunk: where the main development is occurring and where the main check-out/check-in activity is occurring.  Usually you will use Tortoise to check out copies of the code from here.

## EMGAATS Shell Structure ##

### Forms ###

EMGAATS 2.2 is now consolidated to a lot fewer forms than in EMGAATS 2.11.  Most of the work is done in the Build form, frmBuild, and a report is created in the Build Report form, frmBuildReport.  There are helper dialogs also.

#### Build Form ####

The Build form, frmBuild, acts as the main switchboard for model building activity.  After a user browses for a directory to build a model in, the form changes to enable all the settings possible.

The user specifies Root and Stop pipes and sets Build Options.  Then he specifies what to build with Build Instructions.

There are four build presets that are hard-coded in EMGAATS:

  * Build All (the action actPresetBuildAll)
  * Trace Network Only (the action actPresetTraceNetworkOnly)
  * Recalculate Hydrology (the action actPresetRecalculateHydrology)
  * Deploy Model Files (the action actPresetDeployModelFiles)

There is a special preset called actPresetIndividualOptions that shows most of the commonly used commands for
building models.  This is the default preset shown when the Build form opens, and they are disabled by default.

Looking at the execute methods of these actions (for example, the execute method for actPresetBuildAll is actPresetBuildAllExecute), you will see that the list fCommandList is cleared and commands are added.  These commands are defined in the unit uEMGAATSModelCommands.  Also, after these commands are added, the commands are enabled/disabled.

#### uEMGAATSModelCommands Unit ####

This unit contains all the commands that can be performed on an EMGAATS model (a TEMGAATSModel object).  There are two kinds of commands, a TSimpleCommand and a TMacroCommand.  The difference is that a TMacroCommand is made up of one or more TSimpleCommands.  TSimpleCommands are "atomic" operations on a model.

| **Command** | **Function** |
|:------------|:-------------|
| TBuildNewModelCommand  | Builds a new model.  Calls |
| TBuildOverExistingModel (to be renamed later to have _Command_ at the end) | Currently unused, but is used like TBuildNewModelCommand to reconstruct a model. |
| TBuildEmptyModelCommand  |Builds the skeleton directory structure of a model |
| TClearModelDirectoryCommand | Erases the contents of a model directory |
| TConfigureModelForBuildingCommand | Sets defaults for the model.ini configuration file |
| TBuildNetworkCommand | Fills in the mdl\_links\_ac, mdl\_speclinks\_ac, and mdl\_speclinkdata\_ac tables for the model. |
| TTraceNetworkCommand | Used by TBuildNetworkCommand to perform a trace on the master data |
| TCreateMappablePipesCommand | Used by TBuildNetworkCommand to fill in MapInfo data for the trace into mdl\_links\_ac |
| TAssembleSpecialLinksCommand | Used by TBuildNetworkCommand to fill in MapInfo data for the trace into the mdl\_speclinks\_ac and mdl\_speclinkdata\_ac tables. |
| TBuildDirectSubcatchmentsCommand | Fills in MapInfo data for the trace into the mdl\_dirsc\_ac table. |
| TBuildStudyAreaBoundaryCommand | Creates the study area boundary MapInfo files (ProjArea and ProjMask) |
| TCalculateHydrologyCommand | Initializes the mdl\_dirsc\_ac hydrology fields |
| TInitializeICsCommand | Runs the TDSC\_ALL queries from the HydInitDSC database. |
| TApplyVirtualGagesCommand | Updates the virtual gage IDs for mst\_nodes\_ac according to the master quartersections table. |
| TPerformGISHydroQueriesCommand | Runs the RelateDSCtoSurfSC menu command from EMGWorkbench. |
| TCheckForDuplicateDSCsCommand | Runs the CheckForDuplicateDSCs menu command from EMGWorkbench. |
| TCheckForExcessiveICAreasCommand | Runs the CheckForExcessiveICAreas menu command from EMGWorkbench. |
| TCreateModelConfigCommand | Copies the appropriate sections from EMGAATSSystem.ini, EMGAATSModelTemplate.ini into the model.ini file. |
| TCreateStandardDirectoriesCommand | Creates models' standard directories. |
| TCopyMasterTemplateCommand | Copies over files from the master files directory to update the model. |
| TCreateEmptyModelDataCommand | Creates empty model files (IC tables in Delphi, standard model tables using the CreateModelTables menu command from EMGWorkbench) |
| TCreateDataAccessFileCommand | Creates John's data access MDB file. |
| TOpenMasterModelDataCommand | Opens the master and model data and MapInfo tables.  Always required before manipulating any data. |
| TRelinkModelDataCommand | Relinks model databases.  Usually required if a model directory moved. |
| TDeployRunoffCommand | Creates the runoff files. |
| TPrepareRunoffFileCommand | Calls ModelDeployHydrology to create Runoff files. |
| TCreateRunoffBatchFileCommand | Creates the pdxrun.bat file. |
| TCreateReconciliationSpreadsheetCommand | Creates the HydroQC.xls file for reconciliation checking between the GIS and model representations. |
| TDeployEngineFileCommand | Creates the XPX and DHI database files for import. |
| TCreateQCWorkspacesCommand | Creates the QC Workspaces |
| TCopyStandardMapInfoWorkspacesCommand | Copies files from the SAMaster\_22\wors directory to the model&#8217;s wors directory |
| TCopyStandardMXDsCommand | Copies files from the SAMaster\_22\mxds directory to the model&#8217;s mxds directory |

## Configuration Files ##

### System Configuration File, EMGAATSSystem.ini ###

This file manages the system settings and can be found in the application directory (W:\Model\_Programs\EMGAATS\binv2.2).  The important settings here are the correct paths for the MasterFiles, MDBs, and Code sections.

### User Configuration File, EMGAATSUser.ini and EMGAATSUserTemplate.ini ###

This file manages user preferences and can be found in the user&#8217;s documents and settings directory (C:\Documents and Settings\_user_\Application Data\BES\_SystemsAnalysis).  This file is replicated from the EMGAATSUserTemplate.ini file in the application directory if this directory or file is not found.

### Model Configuration File, Model.ini and EMGAATSModelTemplate.ini ###

All model directories have this file and is initially replicated from the EMGAATSModelTemplate.ini file in the application directory, but is then modified as the user changes build parameters.

# Workbench Structure #

# QCWorkbench Structure #