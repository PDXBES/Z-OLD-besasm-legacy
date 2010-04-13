{==fModifyModelBoundaries Unit==================================================

	Form for changing model boundaries (start/stop pipes)

	Revision History
	2.11   03/23/2006 (AMM) Changed role of mdbkeys to RequiredMDBS, the list of
													model MDBs that must always be present
	2.1    03/25/2003 (AMM) Added 'Retrace' to SystemIni array
													Added 'ic' to modeldirs array
	1.5    ?          ?
	1.0    11/01/2002 Initial release

===============================================================================}

unit GlobalConstants;

interface

type
  TACrelink = record
    linkmdb, linktable, srctable, srcdatabase, emsg : string ;
  end;

const
  ReturnSuccess = -1;
  ReturnCaution = 0;
  ReturnFailure = 1;

  // max threshold of the reconciliation for the user to get a warning
  RSSwarn = 0.02;

 	EMGAATSVersion = '2.2';
	EMGAATSVersionComment = '22 May 2007';

  //Config File Names
  SystemConfigFileName = 'EMGAATSSystem.ini';
  TemplateUserConfigFileName = 'EMGAATSUserTemplate.ini';
  UserConfigLocation = '\BES_SystemsAnalysis\EMGAATS_' + EMGAATSVersion;
  UserConfigFileName = 'EMGAATSUser.ini';
  ModelConfigFileName = 'Model.ini';
  ModelResultsStoreFileName = 'ModelResults.mdb';

	SystemIniFileName = 'EMGAATS.ini';
	{ upon opening an existing model, the model values in these sections are
    replaced with the values in emgaats.ini}
	SystemIniSections : array[0..3] of string = ('MasterFiles', 'Code', 'mdbs', 'TraceLinks');

  ModelResultsDirName = 'ModelResults';

	PatchIniFileName = 'patch.ini';  //obsolete
	UserRegistryKey = 'Software\EMGAATS';

  PATCH_allmdbs = 100;
  Patch_mxd = 101;
  PATCH_IC01 = 102;

	ModelIniFileName = 'Model.ini';

	strMasterSection = 'MasterFiles';
	strMdlState = 'ModelState';
	strMdlRootSection = 'rootlinks';
	strmdlstopsection = 'stoplinks';
	strRetraceSection = 'Retrace';
  strBuildLogSection = 'BuildLog';

	strMBmsg = 'gstrCurrentMessage';
	strMBrc = 'gstrReturnStatus';
  emgwError = 'ERROR';
  emgwFailure = 'FAILURE';
  emgwSuccess = 'SUCCESS';
  
	modeldirs : array[0..10] of string = ('dsc','links','nodes','laterals','qc',
		'mdbs', 'wors', 'sim', 'surfsc', 'ic', 'mxd');

	MDBSSectionString = 'mdbs';
	MDBSDirPath = '\mdbs\';
	// AMM 3/23/2006: renamed mdbkeys to RequiredMDBS
{	mdbkeys : array[0..7] of string = ('HYDInitDsc','LinkQAQC',
		'ModelDeployHydraulics', 'ModelDeployHydrology','ModelAssemble',
		'ModelResults', 'LookupTables', 'EmgaatsCode');}
	RequiredMDBS : array[0..7] of string = ('HYDInitDsc','LinkQAQC',
		'ModelDeployHydraulics', 'ModelDeployHydrology','ModelAssemble',
		'ModelResults', 'LookupTables', 'EmgaatsCode');
	strLookupTables = 'LookupTables';
	timeframes : array[0..1] of string = ('EX', 'FU');

	strPathToGISTranslator: PChar = '\\cassio\modeling\model_programs\GISTranslator\log.txt';

  MaxPathLength = 250;
  
  // Missing Windows constant for SHGetFolderPath
  SHGFP_TYPE_CURRENT = 0;

function IsRequiredMDB(AKey: String): Boolean;

implementation

uses SysUtils;

function IsRequiredMDB(AKey: String): Boolean;
var
	i: Integer;
begin
	Result := False;
	for i := Low(RequiredMDBS) to High(RequiredMDBS) do    // Iterate
	begin
		if CompareText(AKey, RequiredMDBS[i]) = 0 then
		begin
			Result := True;
			Exit;
		end;
	end;    // for

end;

end.
