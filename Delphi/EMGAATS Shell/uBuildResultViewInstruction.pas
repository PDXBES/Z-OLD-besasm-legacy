unit uBuildResultViewInstruction;

interface

uses SysUtils, Classes, uMapInfoManager;

type
  TResultViewMapOrientation = (rvmoLandscape, rvmoPortrait);
  TResultViewLegendOrientation = (rvloLandscape, rvloPortrait);
  TResultViewPaperSize = (rvpsA, rvpsB, rvpsC, rvpsD, rvpsE);

  ///<summary>
  ///  Interface for strategy to set up the decorative elements of a workspace,
  ///  depending on layout
  ///</summary>
  IResultViewElementsSetup = interface
    ['{3D83F7FC-5DD5-40A4-9B8F-AB34693C6023}']
    procedure SetupElements;
  end;

  TBuildResultViewInstruction = class;

  TBaseSetupStrategy = class(TInterfacedObject)
  private
    fMapInfoManager: TMapInfoDisplayManager;
    fInstruction: TBuildResultViewInstruction;
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); virtual;
  end;

  TSetupMapLandscape_LegendPortrait = class(TBaseSetupStrategy, IResultViewElementsSetup)
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); override;
    procedure SetupElements;
  end;

  TSetupMapLandscape_LegendLandscape = class(TBaseSetupStrategy, IResultViewElementsSetup)
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); override;
    procedure SetupElements;
  end;

  TSetupMapPortrait_LegendPortrait = class(TBaseSetupStrategy, IResultViewElementsSetup)
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); override;
    procedure SetupElements;
  end;

  TSetupMapPortrait_LegendLandscape = class(TBaseSetupStrategy, IResultViewElementsSetup)
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); override;
    procedure SetupElements;
  end;

  ///<summary>
  ///  Interface for strategy to set up the main workspace
  ///</summary>
  IResultViewWorkspaceSetup = interface
    ['{F5EC1331-DEAE-43BD-AAF9-D4CF787AC8E4}']
    function Title: String;
    function ShortName: String;
    procedure SetupWorkspace;
    function GetInstruction: TBuildResultViewInstruction;
    procedure SetInstruction(Value: TBuildResultViewInstruction);
    function GetMapInfoManager: TMapInfoDisplayManager;
    procedure SetMapInfoManager(Value: TMapInfoDisplayManager);
    property Instruction: TBuildResultViewInstruction read GetInstruction
      write SetInstruction;
    property MapInfoManager: TMapInfoDisplayManager read GetMapInfoManager
      write SetMapInfoManager;
  end;

  TBaseWorkspaceStrategy = class(TInterfacedObject, IResultViewWorkspaceSetup)
  private
    fMapInfoManager: TMapInfoDisplayManager;
    fInstruction: TBuildResultViewInstruction;
    function GetInstruction: TBuildResultViewInstruction;
    procedure SetInstruction(Value: TBuildResultViewInstruction);
    function GetMapInfoManager: TMapInfoDisplayManager;
    procedure SetMapInfoManager(Value: TMapInfoDisplayManager);
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); virtual;
    procedure SetupWorkspace; virtual;
    property Instruction: TBuildResultViewInstruction read GetInstruction
      write SetInstruction;
    property MapInfoManager: TMapInfoDisplayManager read GetMapInfoManager
      write SetMapInfoManager;
    function Title: String; virtual;
    function ShortName: String; virtual;
  end;

  TSystemWorkspace = class(TBaseWorkspaceStrategy)
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); override;
    function Title: String; override;
    procedure SetupWorkspace; override;
    function ShortName: String; override;
  end;

  TBasementSewerBackupRiskWorkspace = class(TBaseWorkspaceStrategy)
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); override;
    function Title: String; override;
    procedure SetupWorkspace; override;
    function ShortName: String; override;
  end;

  TTopographyWorkspace = class(TBaseWorkspaceStrategy)
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); override;
    function Title: String; override;
    procedure SetupWorkspace; override;
    function ShortName: String; override;
  end;

  TLargeUtilitiesTrafficWorkspace = class(TBaseWorkspaceStrategy)
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); override;
    function Title: String; override;
    procedure SetupWorkspace; override;
    function ShortName: String; override;
  end;

  TAreaExistingLandUseWorkspace = class(TBaseWorkspaceStrategy)
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); override;
    function Title: String; override;
    procedure SetupWorkspace; override;
    function ShortName: String; override;
  end;

  TComprehensivePlanWorkspace = class(TBaseWorkspaceStrategy)
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); override;
    function Title: String; override;
    procedure SetupWorkspace; override;
    function ShortName: String; override;
  end;

  TEnvironmentalConsiderationsWorkspace = class(TBaseWorkspaceStrategy)
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); override;
    function Title: String; override;
    procedure SetupWorkspace; override;
    function ShortName: String; override;
  end;

  TSewerSurchargeRiskWorkspace = class(TBaseWorkspaceStrategy)
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); override;
    function Title: String; override;
    procedure SetupWorkspace; override;
    function ShortName: String; override;
  end;

  TImpPctIncreaseWorkspace = class(TBaseWorkspaceStrategy)
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); override;
    function Title: String; override;
    procedure SetupWorkspace; override;
    function ShortName: String; override;
  end;

  TExistingLandUseDSCWorkspace = class(TBaseWorkspaceStrategy)
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); override;
    function Title: String; override;
    procedure SetupWorkspace; override;
    function ShortName: String; override;
  end;

  TComprehensivePlanDSCWorkspace = class(TBaseWorkspaceStrategy)
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); override;
    function Title: String; override;
    procedure SetupWorkspace; override;
    function ShortName: String; override;
  end;

  TSystemQAQCWorkspace = class(TBaseWorkspaceStrategy)
  public
    constructor Create(AMapInfoManager: TMapInfoDisplayManager;
      AInstruction: TBuildResultViewInstruction); override;
    function Title: String; override;
    procedure SetupWorkspace; override;
    function ShortName: String; override;
  end;

  ///<summary>
  ///  A package specifying the workspace orientation, legend orientation, and
  ///  paper size for creating a result view
  ///</summary>
  TBuildResultViewInstruction = class
  private
    fResultDatabase: TFileName;
    fMapInfoManager: TMapInfoDisplayManager;
    fMapOrientation: TResultViewMapOrientation;
    fResultViewElementsSetupStrategy: IResultViewElementsSetup;
    fResultViewWorkspaceSetupStrategy: IResultViewWorkspaceSetup;
    fLegendOrientation: TResultViewLegendOrientation;
    fPaperSize: TResultViewPaperSize;
    fFontFactor: Double;
    fXFactor: Double;
    fYFactor: Double;
    fFileName: String;
    fProjectNumber: String;
    fProjectDesc: String;
    fStudyArea: String;
    fTitle: String;
    procedure SetupBaseMaps;
    procedure MakeAuxiliaryTables;
    procedure MakeOutfallTable;
    procedure MakeSumpedSurfSCtable;
    procedure MakeTransfersToExternalModelsTable;
    procedure MakeInflowsFromExternalModelsTable;
  public
    constructor Create(AResultDatabase: TFileName;
      AMapOrientation: TResultViewMapOrientation;
      ALegendOrientation: TResultViewlegendOrientation;
      APaperSize: TResultViewPaperSize;
      AResultViewWorkspaceStrategy: IResultViewWorkspaceSetup);
    destructor Destroy; override;

    ///<summary>
    ///  Build the result view workspace
    ///</summary>
    procedure Build;

    ///<summary>
    ///  Build the MapInfo workspace file
    ///</summary>
    procedure MakeWorkspace;

    ///<summary>
    ///  Build the labels and regalia for the MapInfo workspace file
    ///</summary>
    procedure SetPageElements;

    ///<summary>
    ///  Open the workspace and do other work for completing the workspace file
    ///</summary>
    procedure SetResults;

    ///<summary>
    ///  Save the workspace file
    ///</summary>
    procedure SaveWorkspace;

    property XFactor: Double read fXFactor;
    property YFactor: Double read fYFactor;
    property FontFactor: Double read fFontFactor;
    property FileName: String read fFileName write fFileName;
    property ProjectNumber: String read fProjectNumber write fProjectNumber;
    property ProjectDesc: String read fProjectDesc write fProjectDesc;
    property StudyArea: String read fStudyArea write fStudyArea;
    property Title: String read fTitle write fTitle;
    property ResultDatabase: TFileName read fResultDatabase;
  end;

implementation

uses uEMGAATSSystemConfig, uEMGAATSModel, StStrL, ADODB, CodeSiteLogging,
  GlobalConstants;

const
  XFactors: array [0..9] of Double = (0.62,1.00,1.33,2.00,2.60,0.62,1.00,1.60,2.10,3.20);
  YFactors: array [0..9] of Double = (0.62,1.00,1.55,2.00,3.20,0.62,1.00,1.31,2.01,2.60);
  FontFactors: array [0..9] of Double = (0.50,1.00,1.30,2.00,2.60,0.50,1.00,1.30,2.00,2.60);
  MapWidthFactors: array [0..1,0..1] of Double = ((15.6,11.92),(9.622,6.91));
  ScaleWFactors: array [0..1,0..1] of Double = ((3.524,3.524),(2.76,2.76));
  PaperSizeStrings: array [0..4] of String = ('A', 'B', 'C', 'D', 'E');


{ TBuildResultViewInstruction }

procedure TBuildResultViewInstruction.Build;
begin
  fMapInfoManager := TMapInfoDisplayManager.Create;

  try
    fResultViewWorkspaceSetupStrategy.Instruction := self;
    fResultViewWorkspaceSetupStrategy.MapInfoManager := self.fMapInfoManager;

    case fMapOrientation of
      rvmoLandscape:
        case fLegendOrientation of
          rvloLandscape: fResultViewElementsSetupStrategy :=
            TSetupMapLandscape_LegendLandscape.Create(self.fMapInfoManager, self);
          rvloPortrait: fResultViewElementsSetupStrategy :=
            TSetupMapLandscape_LegendPortrait.Create(self.fMapInfoManager, self);
        end;
      rvmoPortrait:
        case fLegendOrientation of
          rvloLandscape: fResultViewElementsSetupStrategy :=
            TSetupMapPortrait_LegendLandscape.Create(self.fMapInfoManager, self);
          rvloPortrait: fResultViewElementsSetupStrategy :=
            TSetupMapPortrait_LegendPortrait.Create(self.fMapInfoManager, self);
        end;
    end;

    MakeWorkspace;
    SetResults;
    SetupBaseMaps;
    SetPageElements;
    SaveWorkspace;
  finally
    FreeAndNil(fMapInfoManager);
  end;
end;

constructor TBuildResultViewInstruction.Create(
  AResultDatabase: TFileName;
  AMapOrientation: TResultViewMapOrientation;
  ALegendOrientation: TResultViewlegendOrientation;
  APaperSize: TResultViewPaperSize;
  AResultViewWorkspaceStrategy: IResultViewWorkspaceSetup);
begin
  fResultDatabase := AResultDatabase;
  fMapOrientation := AMapOrientation;
  fLegendOrientation := ALegendOrientation;
  fPaperSize := APaperSize;

  // Setup factors here dependent on size/orientations; improve with a table lookup later
  case fMapOrientation of
    rvmoLandscape:
      begin
        fXFactor := XFactors[Ord(fPaperSize)];
        fYFactor := YFactors[Ord(fPaperSize)];
        fFontFactor := FontFactors[Ord(fPaperSize)];
      end;
    rvmoPortrait:
      begin
        fXFactor := XFactors[Ord(fPaperSize)+5];
        fYFactor := YFactors[Ord(fPaperSize)+5];
        fFontFactor := FontFactors[Ord(fPaperSize)+5];
      end;
  end;

  fResultViewWorkspaceSetupStrategy := AResultViewWorkspaceStrategy;

//  fMapInfoManager := TMapInfoDisplayManager.Create;
end;

destructor TBuildResultViewInstruction.Destroy;
begin
  if Assigned(fMapInfoManager) then
    FreeAndNil(fMapInfoManager);
  inherited;
end;

procedure TBuildResultViewInstruction.MakeAuxiliaryTables;
begin
  MakeOutfallTable;
  MakeSumpedSurfSCtable;
  MakeTransfersToExternalModelsTable;
  MakeInflowsFromExternalModelsTable;
end;

procedure TBuildResultViewInstruction.MakeInflowsFromExternalModelsTable;
var
  InflowNodesFileName: String;
  CurrentStopLink: Integer;
  NumRecords: Integer;
  HasValidStopLinks: Boolean;
  i: Integer;
begin
  // Iterate over each stop link and get the upstream node for selection
  // Using the upstream node, insert a row into the QyInflowNodes table

  if not fMapInfoManager.TableIsOpen('QyInflowNodes') then
  begin
    InflowNodesFileName := CurrentModel.Path + '\nodes\QyInflowNodes.TAB';
    if not FileExists(InflowNodesFileName) then
    begin
      // Check to see if there all stoplinks are invalid
      HasValidStopLinks := false;
      for i := 0 to CurrentModel.Config.StopLinksCount - 1 do
      begin
        CurrentStopLink := CurrentModel.Config.StopLinks[i];
        fMapInfoManager.RunCommand('Select * from mdl_Nodes_ac ' +
          'where node in ' +
            '(select USNode from mdl_links_ac where MLinkID = '
            + IntToStr(CurrentStopLink) + ')' +
          'into Selection');
        NumRecords := fMapInfoManager.NumRecords('Selection');
        if NumRecords > 0 then
        begin
          HasValidStopLinks := true;
          Break;
        end;
      end;
      if fMapInfoManager.TableIsOpen('Selection') then
        fMapInfoManager.RunCommand('Close Table Selection');

      if (CurrentModel.Config.StopLinksCount = 0) or not HasValidStopLinks then
      begin
        // Create an empty QyInflowNodes table
        if not fMapInfoManager.TableIsOpen('DummyObj') then
          fMapInfoManager.OpenTable(SystemConfig.MasterFilesByName['master_wors']+'\DummyObj.TAB');
        fMapInfoManager.RunCommand('Select * from DummyObj where str$(obj) = "point" into QyInflowNodes');
        Exit;
      end;

      // Get the first stop node to create the file
      CurrentStopLink := CurrentModel.Config.StopLinks[0];
      fMapInfoManager.RunCommand('Select * from mdl_Nodes_ac ' +
        'where node in ' +
          '(select USNode from mdl_links_ac where MLinkID = '
          + IntToStr(CurrentStopLink) + ')' +
        'into QyInflowNodes');
      fMapInfoManager.RunCommand('Commit Table QyInflowNodes as "' + InflowNodesFileName +
        '" TYPE NATIVE Charset "WindowsLatin1"');
      fMapInfoManager.RunCommand('Close Table QyInflowNodes');
      fMapInfoManager.OpenTable(InflowNodesFileName, False);

      // Go through the rest of the stop links and continue added stop nodes
      for i := 0 to CurrentModel.Config.StopLinksCount - 1 do
      begin
        CurrentStopLink := CurrentModel.Config.StopLinks[i];
        fMapInfoManager.RunCommand('Select * from mdl_Nodes_ac ' +
          'where node in ' +
            '(select USNode from mdl_links_ac where MLinkID = '
            + IntToStr(CurrentStopLink) + ')' +
          'into CurrentStopNode');
        fMapInfoManager.RunCommand('Insert Into QyInflowNodes Select * from CurrentStopNode');
      end;
      fMapInfoManager.RunCommand('Commit Table QyInflowNodes');
      fMapInfoManager.RunCommand('Close Table QyInflowNodes');
    end;
    fMapInfoManager.OpenTable(InflowNodesFileName);
  end;
end;

procedure TBuildResultViewInstruction.MakeOutfallTable;
var
  OFListFileName: String;
  NumRecords: Integer;
begin
{
  Select * from mdl_Nodes_ac
  		where node in(select DSNode from mdl_links_ac
  		where ReachElement= 1 and LinkType="D"
  		and LinkReach not like "%.%") into QyOFList
  	If tableinfo(QyOFList,8)>0 then
  			Commit Table QyOFList As Str$(TblPath + "\nodes\OFList.TAB") TYPE NATIVE Charset "WindowsLatin1"
  			Close Table QyOFList
  			Open Table Str$(TblPath + "nodes\OFList") As QyOutfalls Interactive
  	Else
  			DummyTbl=TblXist("nodes","QyOutfalls", "point")
  	End if
}

  if not fMapInfoManager.TableIsOpen('QyOFList') then
  begin
    OFListFileName := CurrentModel.Path + '\nodes\OFList.TAB';
    if not FileExists(OFListFileName) then
    begin
      fMapInfoManager.RunCommand('  Select * from mdl_Nodes_ac ' +
        'where node in(select DSNode from mdl_links_ac '+
        'where ReachElement= 1 and LinkType="D" ' +
        'and LinkReach not like "%.%") into QyOFList');
      NumRecords := fMapInfoManager.NumRecords('QyOFList');
      if NumRecords > 0 then
      begin
        fMapInfoManager.RunCommand('Commit Table QyOFList As "' + OFListFileName +
          '" TYPE NATIVE Charset "WindowsLatin1"');
        fMapInfoManager.RunCommand('Close Table QyOFList');
      end
      else
      begin
        // Create an empty OFList table
        if not fMapInfoManager.TableIsOpen('DummyObj') then
          fMapInfoManager.OpenTable(SystemConfig.MasterFilesByName['master_wors']+'\DummyObj.TAB');
        fMapInfoManager.RunCommand('Select * from DummyObj where str$(obj) = "point" into QyOutfalls');
        Exit;
      end;
    end;
    fMapInfoManager.RunCommand('Open Table "' + OFListFileName + '" As QyOutfalls Interactive');
  end;
end;

procedure TBuildResultViewInstruction.MakeSumpedSurfSCtable;
var
  SumpedSurfSCFileName: String;
  NumRecords: Integer;
begin
{
  GetFld=FldXist("mdl_SurfSC_ac","issumped" )	'Print GetFld + " ChkFld"
  if GetFld = "T" then
  	Select *  from mdl_SurfSC_ac  where issumped = "T"  into QySumpedSC
  	if tableinfo(QySumpedSC,8)>0 then
  			Commit Table QySumpedSC As Str$(TblPath + "\SurfSC\QySumpedSC.TAB") TYPE NATIVE Charset "WindowsLatin1"
  			Close Table QySumpedSC
  			Open Table Str$(TblPath + "SurfSC\QySumpedSC") Interactive
  		Else
  			DummyTbl=TblXist("surfSC","QySumpedSC", "region")
  	end if
  end if
}

  if not fMapInfoManager.TableIsOpen('QySumpedSC') then
  begin
    SumpedSurfSCFileName := CurrentModel.Path + '\SurfSC\QySumpedSC.TAB';
    if not FileExists(SumpedSurfSCFileName) then
    begin
      fMapInfoManager.RunCommand('  Select * from mdl_SurfSC_ac ' +
        'where issumped = "T" into QySumpedSC');
      NumRecords := fMapInfoManager.NumRecords('QySumpedSC');
      if NumRecords > 0 then
      begin
        fMapInfoManager.RunCommand('Commit Table QySumpedSC As "' + SumpedSurfSCFileName +
          '" TYPE NATIVE Charset "WindowsLatin1"');
        fMapInfoManager.RunCommand('Close Table QySumpedSC');
      end
      else
      begin
        // Create an empty OFList table
        if not fMapInfoManager.TableIsOpen('DummyObj') then
          fMapInfoManager.OpenTable(SystemConfig.MasterFilesByName['master_wors']+'\DummyObj.TAB');
        fMapInfoManager.RunCommand('Select * from DummyObj where str$(obj) = "region" into QySumpedSC');
        Exit;
      end;
    end;
    fMapInfoManager.OpenTable(SumpedSurfSCFileName);
  end;
end;

procedure TBuildResultViewInstruction.MakeTransfersToExternalModelsTable;
var
  TransfersToExternalModelsFileName: String;
  NumRecords: Integer;
begin
{
  Select * from mdl_Nodes_ac where node in(select DSNode from mdl_links_ac
  		where ReachElement= 1 and LinkType in("S", "C", "IS", "IC")
  		and LinkReach not like "%.%") into Qy2ExternalMdl1
  if tableinfo(Qy2ExternalMdl1,8)>0 then
  			Commit Table Qy2ExternalMdl1 As Str$(TblPath + "\nodes\Qy2ExternalMdl.TAB") TYPE NATIVE Charset "WindowsLatin1"
  			Close Table Qy2ExternalMdl1
  			Open Table Str$(TblPath + "nodes\Qy2ExternalMdl") As Qy2ExternalMdl Interactive
  	Else
  			DummyTbl=TblXist("Nodes","Qy2ExternalMdl", "point")
  end if
}

  if not fMapInfoManager.TableIsOpen('Qy2ExternalMdl') then
  begin
    TransfersToExternalModelsFileName := CurrentModel.Path + '\nodes\Qy2ExternalMdl.TAB';
    if not FileExists(TransfersToExternalModelsFileName) then
    begin
      fMapInfoManager.RunCommand('  Select * from mdl_nodes_ac ' +
        'where node in(select DSNode from mdl_links_ac ' +
  		  'where ReachElement= 1 and LinkType in("S", "C", "IS", "IC") ' +
  		  'and LinkReach not like "%.%") into Qy2ExternalMdl');
      NumRecords := fMapInfoManager.NumRecords('Qy2ExternalMdl');
      if NumRecords > 0 then
      begin
        fMapInfoManager.RunCommand('Commit Table Qy2ExternalMdl As "' + TransfersToExternalModelsFileName +
          '" TYPE NATIVE Charset "WindowsLatin1"');
        fMapInfoManager.RunCommand('Close Table Qy2ExternalMdl');
      end
      else
      begin
        // Create an empty OFList table
        if not fMapInfoManager.TableIsOpen('DummyObj') then
          fMapInfoManager.OpenTable(SystemConfig.MasterFilesByName['master_wors']+'\DummyObj.TAB');
        fMapInfoManager.RunCommand('Select * from DummyObj where str$(obj) = "point" into Qy2ExternalMdl');
        Exit;
      end;
    end;
    fMapInfoManager.OpenTable(TransfersToExternalModelsFileName);
  end;
end;

procedure TBuildResultViewInstruction.MakeWorkspace;
begin
  fFileName := ExtractFilePath(fResultDatabase) + 
    Format('%s_%s_%s.WOR', [fStudyArea,
      fResultViewWorkspaceSetupStrategy.ShortName,
      PaperSizeStrings[Ord(fPaperSize)]]);

  fMapInfoManager.OpenTable(IncludeTrailingPathDelimiter(CurrentModel.Path) + 'links\mdl_links_ac');
  fMapInfoManager.OpenTable(IncludeTrailingPathDelimiter(CurrentModel.Path) + 'nodes\mdl_nodes_ac');
  fMapInfoManager.OpenTable(IncludeTrailingPathDelimiter(CurrentModel.Path) + 'dsc\mdl_dirsc_ac');
  fMapInfoManager.OpenTable(IncludeTrailingPathDelimiter(CurrentModel.Path) + 'surfsc\mdl_surfsc_ac');

  fMapInfoManager.RunCommand('Set distance units "ft"');
  MakeAuxiliaryTables;

{$IFDEF HERMESLAPTOP}
  fMapInfoManager.OpenTable('C:\CASSIO\GIS1\MI_DATA\BOUNDARY\Citylimi');
  fMapInfoManager.OpenTable('C:\CASSIO\GIS1\MI_DATA\STREET\ROW\Tric_row');
  fMapInfoManager.OpenTable('C:\CASSIO\GIS1\MI_MISC\LABELS\WORKING\PDX_Cad_Str_Lbl');
  fMapInfoManager.OpenTable('C:\CASSIO\gis1\METRODAT\RLISLT99\streets\arterial');
  fMapInfoManager.OpenTable('C:\CASSIO\gis1\Mi_data\WATER\Waternew_rev0922');
  fMapInfoManager.OpenTable('C:\CASSIO\gis1\MI_CGIS\transportation\streets\freeways_metro');
  fMapInfoManager.OpenTable('C:\CASSIO\GIS1\MI_DATA\IMAGES\Hillshade\hillshadehi.TAB');
  fMapInfoManager.OpenTable('C:\CASSIO\gis1\MI_MISC\LOGOS\WORKING\North2');
  fMapInfoManager.OpenTable('C:\CASSIO\gis1\MI_MISC\LOGOS\WORKING\Besbird.TAB');
  fMapInfoManager.OpenTable('C:\CASSIO\gis1\MI_MISC\LOGOS\WORKING\Mapinfo2.TAB');
  fMapInfoManager.OpenTable('C:\CASSIO\GIS1\MI_MISC\SCALEBAR\_MiFt_scale');
  fMapInfoManager.RunCommand('Open Table "C:\CASSIO\GIS1\MI_MISC\LOGOS\SystemsAnalysis5.TAB" As SysAnaLog Interactive');
  fMapInfoManager.OpenTable('C:\CASSIO\gis1\MI_DATA\STREET\Bridges\PDX_Bridges.TAB');
  fMapInfoManager.OpenTable('C:\CASSIO\gis1\MI_DATA\STREET\CENTERLN\centerli');
{$ELSE}
  fMapInfoManager.OpenTable('\\CASSIO\GIS1\MI_DATA\BOUNDARY\Citylimi');
  fMapInfoManager.OpenTable('\\CASSIO\GIS1\MI_DATA\STREET\ROW\Tric_row');
  fMapInfoManager.OpenTable('\\CASSIO\GIS1\MI_MISC\LABELS\WORKING\PDX_Cad_Str_Lbl');
  fMapInfoManager.OpenTable('\\CASSIO\gis1\METRODAT\RLISLT99\streets\arterial');
  fMapInfoManager.OpenTable('\\CASSIO\gis1\Mi_data\WATER\Waternew_rev0922');
  fMapInfoManager.OpenTable('\\CASSIO\gis1\MI_CGIS\transportation\streets\freeways_metro');
  fMapInfoManager.OpenTable('\\CASSIO\GIS1\MI_DATA\IMAGES\Hillshade\hillshadehi.TAB');
  fMapInfoManager.OpenTable('\\CASSIO\gis1\MI_MISC\LOGOS\WORKING\North2');
  fMapInfoManager.OpenTable('\\CASSIO\gis1\MI_MISC\LOGOS\WORKING\Besbird.TAB');
  fMapInfoManager.OpenTable('\\CASSIO\gis1\MI_MISC\LOGOS\WORKING\Mapinfo2.TAB');
  fMapInfoManager.OpenTable('\\CASSIO\GIS1\MI_MISC\SCALEBAR\_MiFt_scale');
  fMapInfoManager.RunCommand('Open Table "\\CASSIO\GIS1\MI_MISC\LOGOS\SystemsAnalysis5.TAB" As SysAnaLog Interactive');
  fMapInfoManager.OpenTable('\\CASSIO\gis1\MI_DATA\STREET\Bridges\PDX_Bridges.TAB');
  fMapInfoManager.OpenTable('\\CASSIO\gis1\MI_DATA\STREET\CENTERLN\centerli');
{$ENDIF}

  Assert(FileExists(CurrentModel.Path + '\surfsc\ProjArea.TAB'),
    'ProjArea.TAB does not exist');
  Assert(FileExists(CurrentModel.Path + '\surfsc\ProjMask.TAB'),
    'ProjMask.TAB does not exist');

  fMapInfoManager.OpenTable(CurrentModel.Path + '\surfsc\ProjArea.TAB');
  fMapInfoManager.OpenTable(CurrentModel.Path + '\surfsc\ProjMask.TAB');
end;

procedure TBuildResultViewInstruction.SaveWorkspace;
begin

{
  SavStr= Str$("Save Workspace As" +Chr$(34)+ Str$(PathToDirectory$(AppPath)+BsnName +"_"+CurMapCode+"_" + CurWorName + ".WOR") +Chr$(34))
  Run Command SavStr
}
  fMapInfoManager.RunCommand('Save Workspace As "' + FileName + '"');
end;

procedure TBuildResultViewInstruction.SetPageElements;
begin
  Assert(Assigned(fResultViewElementsSetupStrategy), 'Result view elements setup '+
    'strategy not set');
  fResultViewElementsSetupStrategy.SetupElements;
end;

procedure TBuildResultViewInstruction.SetResults;
begin
  fResultViewWorkspaceSetupStrategy.SetupWorkspace;
end;

procedure TBuildResultViewInstruction.SetupBaseMaps;
var
  MainMapID: Integer;
  MapZoom: Double;
  ScaleBarZoom: Double;
  ZoomWidthRatio: Double;
  ScaleBarMapID: Integer;
  DateMapID: Integer;
begin
{
  if MapFig = 0 and LgdFig=0 then
  	MapFig =2
  	LgdFig=2
  	LO_Type="MpL_LgP"
  	MpWidth = 11.92
  	ScaleW = 3.524
  	MpSize=2
  	x=1
  	y=1
  end if
  Print MapFig + " " + LgdFig + " FigChk!"
  	Set CoordSys Earth Projection 3, 33, "ft", -120.5, 43.6666666667, 44.3333333333, 46, 8202099.7379999999, 0
  		CurWinID=GetWinID$("Main Map")
  		If WinID<>0 then
  			MpZoom = 1.01 * Mapperinfo(WinID, 1)
  		Else
  			MpZoom = 2000
  		End if
  		ScaleZ= ScaleW*MpZoom/MpWidth  'print ScaleZ
  	Select *  from _MiFt_scale  where ZoomID =(Select max(ZoomID)
  		from _MiFt_scale where ZoomID <MpZoom/MpWidth)  into QyScale noselect
  	Run Application "\\oberon\grp117\_ATtemp\_MITools\MdlMap\wors\MpBody\Mp00_Base.wor"
  		CurWinID= GetWinID$("Scalebar Map")
  		Set Map Window WinID Zoom (ScaleZ/1.01) Units "ft"  'synchronizing scalebar with map in layout
  		CurWinID= GetWinID$("Date Map")
      FontStr = Makefont("Arial Narrow",1,int(6*MpSize),0,-1)
  		Set Map Window WinID Layer 1 Label Font FontStr
}
  fMapInfoManager.RunCommand('Set CoordSys Earth Projection 3, 33, "ft", -120.5, ' +
    '43.6666666667, 44.3333333333, 46, 8202099.7379999999, 0');
  MainMapID := fMapInfoManager.WindowID('Main Map');
  if MainMapID <> 0 then
  begin
    MapZoom := fMapInfoManager.Zoom('Main Map');
    MapZoom := 1.01 * MapZoom;
  end
  else
    MapZoom := 2000;

  ZoomWidthRatio := MapZoom / MapWidthFactors[Ord(fMapOrientation),Ord(fLegendOrientation)];
  ScaleBarZoom := ScaleWFactors[Ord(fMapOrientation),Ord(fLegendOrientation)] *
    ZoomWidthRatio;

  fMapInfoManager.RunCommand('Select *  from _MiFt_scale  where ZoomID = (Select max(ZoomID) ' +
    'from _MiFt_scale where ZoomID < ' +Format('%.4f', [ZoomWidthRatio])+ ') into QyScale noselect');

  fMapInfoManager.RunCommand('Run Application "' +
    IncludeTrailingPathDelimiter(SystemConfig.MasterFilesByName['master_wors']) +'Mp00_Base.WOR"');

  ScaleBarMapID := fMapInfoManager.WindowID('Scalebar Map');
  fMapInfoManager.RunCommand(Format('Set Map Window %d Zoom (%.4f/1.01) Units "ft"',
    [ScaleBarMapID, ScaleBarZoom]));

  DateMapID := fMapInfoManager.WindowID('Date Map');
  fMapInfoManager.RunCommand('Dim FontRes As Font');
  fMapInfoManager.RunCommand(Format('FontRes = Makefont("Arial Narrow",1,int(6*%d),0,-1)',
    [Ord(fPaperSize)+1]));
  fMapInfoManager.RunCommand(Format('Set Map Window %d Layer 1 Label Font FontRes',
    [DateMapID]));
  fMapInfoManager.RunCommand('Undim FontRes');
end;

{ TBaseSetupStrategy }

constructor TBaseSetupStrategy.Create(AMapInfoManager: TMapInfoDisplayManager;
  AInstruction: TBuildResultViewInstruction);
begin
  fMapInfoManager := AMapInfoManager;
  fInstruction := AInstruction;
end;

{ TSetupMapLandscape_LegendPortrait }

constructor TSetupMapLandscape_LegendPortrait.Create(
  AMapInfoManager: TMapInfoDisplayManager; AInstruction: TBuildResultViewInstruction);
begin
  inherited Create(AMapInfoManager, AInstruction);
end;

procedure TSetupMapLandscape_LegendPortrait.SetupElements;
var
  CurrentWindowID: Integer;
  RunCommandText: String;
begin
  fMapInfoManager.RunCommand('dim x as float');
  fMapInfoManager.RunCommand('dim y as float');

  fMapInfoManager.RunCommand('x = ' + Format('%.4f', [fInstruction.XFactor]));
  fMapInfoManager.RunCommand('y = ' + Format('%.4f', [fInstruction.YFactor]));

  CodeSite.Send('fInstruction.XFactor', fInstruction.XFactor);
  CodeSite.Send('fInstruction.YFactor', fInstruction.YFactor);
  CodeSite.Send('fInstruction.FontFactor', fInstruction.FontFactor);

  fMapInfoManager.RunCommand('Run Application "' +
    SystemConfig.LayoutSetupFileName_MapLandscape_LegendPortrait + '"');

{
  CurWinID = GetWinID$("Main Layout")
      FontStr = Makefont("Arial",3,int(7* fontsize),16777215,-1)
    Create Text into Window WinID
  		"File Location : (\" + Str$(PathToDirectory$(AppPath)+BsnName +"_"+CurMapCode+"_" + CurWorName + ".WOR")
      (0.5215*x,10.2014*y) (6.8104*x,10.3153*y)
  		Font FontStr
      Angle 90
      FontStr = Makefont("Arial Narrow",1,int(8* fontsize),0,-1)
    Create Text into Window WinID
      ProjNo +chr$(10) + ProjDesc
      (13.63*x-len(ProjDesc)*0.025*x,9.9285*y) (14.3444*x,10.1896*y)
  		Font FontStr
      Justify Center
    Create Text into Window WinID
      "1    O F    1"
      (13.1958*x,9.5903*y) (13.8944*x,9.7542*y)
  		Font FontStr
      Justify Center
      FontStr = Makefont("Times",1,int(18* fontsize),0,-1)
    Create Text
      BsnName + " " + Chr$(10)+ MapTitle
      (14.51*x-len(MapTitle)*0.0625*x,8.8285*y) (16.25*x,9.4147*y)
  		Font FontStr
      Justify Center
  Set CoordSys Earth Projection 3, 33, "ft", -120.5, 43.6666666667, 44.3333333333, 46, 8202099.7379999999, 0
}


{    fMapInfoManager.RunCommand('Set CoordSys Layout Units "in"');
    fMapInfoManager.RunCommand('dim FontRes as Font');
    CurrentWindowID := fMapInfoManager.WindowID('Main Layout');
    RunCommandText := 'FontRes = MakeFont("Arial",3,int(7*'+
      Format('%.4f', [fInstruction.FontFactor])+'),16777215,-1)';
    CodeSite.Send('RunCommandText', RunCommandText);
    fMapInfoManager.RunCommand(RunCommandText);
    RunCommandText := 'Create Text into Window ' + IntToStr(CurrentWindowID) +
      ' "Test Text" (0.5,8.0) (0.75,4.0) Font FontRes Angle 90';
    RunCommandText := 'Create Text '+
      ' "Test Text" (0.5,0.25) (4.0,0.5) Font FontRes';
    CodeSite.Send('RunCommandText', RunCommandText);
    fMapInfoManager.RunCommand(RunCommandText);
    fMapInfoManager.RunCommand('undim FontRes');}

  fMapInfoManager.RunCommand('dim FontRes as Font');
  CurrentWindowID := fMapInfoManager.WindowID('Main Layout');
  RunCommandText := 'FontRes = MakeFont("Arial",3,int(7*'+
    Format('%.4f', [fInstruction.FontFactor])+'),16777215,-1)';
  CodeSite.Send('RunCommandText', RunCommandText);
  fMapInfoManager.RunCommand(RunCommandText);
  RunCommandText := 'Create Text into Window ' + IntToStr(CurrentWindowID) +
    ' "File Location: (' + fInstruction.FileName + ')"' +
    Format(' (0.5215*%.4f,10.2014*%.4f) (6.8104*%.4f,10.3153*%.4f)', [fInstruction.XFactor,
      fInstruction.YFactor, fInstruction.XFactor, fInstruction.YFactor]) +
    ' Font FontRes Angle 90';
  CodeSite.Send('RunCommandText', RunCommandText);
  fMapInfoManager.RunCommand(RunCommandText);

  RunCommandText := 'FontRes = MakeFont("Arial Narrow",1,int(8*'+
    Format('%.4f', [fInstruction.FontFactor])+'),0,-1)';
  CodeSite.Send('RunCommandText', RunCommandText);
  fMapInfoManager.RunCommand(RunCommandText);
  RunCommandText := 'Create Text into Window ' + IntToStr(CurrentWindowID) +
    Format(' "%s" + Chr$(10) + "%s"', [fInstruction.ProjectNumber, fInstruction.ProjectDesc]) +
    Format(' (13.63*%.4f-len("' + fInstruction.ProjectDesc + '")*0.025*%.4f, 9.9285*%.4f)', [fInstruction.XFactor,
      fInstruction.XFactor, fInstruction.YFactor]) +
    Format(' (14.3444*%.4f,10.1896*%.4f)', [fInstruction.XFactor, fInstruction.YFactor]) +
    ' Font FontRes Justify Center';
  CodeSite.Send('RunCommandText', RunCommandText);
  fMapInfoManager.RunCommand(RunCommandText);

  RunCommandText := 'Create Text into Window ' + IntToStr(CurrentWindowID) +
    ' "1    O F    1"' +
    Format(' (13.1958*%.4f,9.5903*%.4f) (13.8944*%.4f,9.7542*%.4f)', [fInstruction.XFactor,
      fInstruction.YFactor, fInstruction.XFactor, fInstruction.YFactor]) +
    ' Font FontRes Justify Center';
  CodeSite.Send('RunCommandText', RunCommandText);
  fMapInfoManager.RunCommand(RunCommandText);

  RunCommandText := 'FontRes = MakeFont("Times",1,int(18*'+
    Format('%.4f', [fInstruction.FontFactor])+'),0,-1)';
  CodeSite.Send('RunCommandText', RunCommandText);
  fMapInfoManager.RunCommand(RunCommandText);
  RunCommandText := 'Create Text ' +
    '"' + fInstruction.StudyArea + '" + Chr$(10) + "' + fInstruction.fResultViewWorkspaceSetupStrategy.Title + '"' +
    Format(' (14.51*%.4f-len("' + fInstruction.fResultViewWorkspaceSetupStrategy.Title + '")*0.0625*%.4f, 8.8285*%.4f)', [fInstruction.XFactor,
      fInstruction.XFactor, fInstruction.YFactor]) +
    Format(' (16.25*%.4f,9.4147*%.4f)', [fInstruction.XFactor, fInstruction.YFactor]) +
    ' Font FontRes Justify Center';
  CodeSite.Send('RunCommandText', RunCommandText);
  fMapInfoManager.RunCommand(RunCommandText);

  fMapInfoManager.RunCommand('Set CoordSys Earth Projection 3, 33, "ft", '+
    '-120.5, 43.6666666667, 44.3333333333, 46, 8202099.7379999999, 0');

  fMapInfoManager.RunCommand('undim FontRes');
  fMapInfoManager.RunCommand('undim x');
  fMapInfoManager.RunCommand('undim y');


end;

{ TSetupMapLandscape_LegendLandscape }

constructor TSetupMapLandscape_LegendLandscape.Create(
  AMapInfoManager: TMapInfoDisplayManager; AInstruction: TBuildResultViewInstruction);
begin
  inherited Create(AMapInfoManager, AInstruction);
end;

procedure TSetupMapLandscape_LegendLandscape.SetupElements;
var
  CurrentWindowID: Integer;
begin
  fMapInfoManager.RunCommand('dim x as float');
  fMapInfoManager.RunCommand('dim y as float');

  fMapInfoManager.RunCommand('x = ' + Format('%.4f', [fInstruction.XFactor]));
  fMapInfoManager.RunCommand('y = ' + Format('%.4f', [fInstruction.YFactor]));

  CodeSite.Send('fInstruction.XFactor', fInstruction.XFactor);
  CodeSite.Send('fInstruction.YFactor', fInstruction.YFactor);

  fMapInfoManager.RunCommand('Run Application "' +
    SystemConfig.LayoutSetupFileName_MapLandscape_LegendLandscape + '"');

{
  	CurWinID = GetWinID$("Main Layout")
      FontStr = Makefont("Arial",3,int(7* fontsize),16777215,-1)
    Create Text into Window WinID
  		"File Location : (\" + Str$(PathToDirectory$(AppPath)+BsnName +"_"+CurMapCode+"_" + CurWorName + ".WOR")
      (0.5215*x,10.2014*y) (6.8104*x,10.3153*y)
  		Font FontStr
      Angle 90
        FontStr = Makefont("Arial Narrow",1,int(8* fontsize),0,-1)
    Create Text into Window WinID
      ProjNo +chr$(10) + ProjDesc
      (13.63*x-len(ProjDesc)*0.025*x,9.9285*y) (14.3444*x,10.1896*y)
  		Font FontStr
      Justify Center
  	Create Text into Window WinID
      "1    O F    1"
      (13.1958*x,9.5903*y) (13.8958*x,9.7597*y)
  		Font FontStr
      Justify Center
      FontStr = Makefont("Times",1,int(18* fontsize),0,-1)
    Create Text
      BsnName + " " + Chr$(10)+ MapTitle
      (8.5*x-len(MapTitle)*0.0625*x,9.2646*y) (9.6014*x,9.8535*y)
  		Font FontStr
      Justify Center
  	Set CoordSys Earth Projection 3, 33, "ft", -120.5, 43.6666666667, 44.3333333333, 46, 8202099.7379999999, 0
}

  fMapInfoManager.RunCommand('dim FontRes as Font');
  CurrentWindowID := fMapInfoManager.WindowID('Main Layout');
  fMapInfoManager.RunCommand('FontRes = MakeFont("Arial",3,int(7*'+
    Format('%.4f', [fInstruction.FontFactor])+'),16777215,-1)');
  fMapInfoManager.RunCommand('Create Text into Window ' + IntToStr(CurrentWindowID) +
    ' "File Location: (' + fInstruction.FileName + ')"' +
    Format(' (0.5215*%.4f,10.2014*%.4f) (6.8104*%.4f,10.3153*%.4f)', [fInstruction.XFactor,
      fInstruction.YFactor, fInstruction.XFactor, fInstruction.YFactor]) +
    ' Font FontRes Angle 90');

  fMapInfoManager.RunCommand('FontRes = MakeFont("Arial Narrow",1,int(8*'+
    Format('%.4f', [fInstruction.FontFactor])+'),0,-1)');
  fMapInfoManager.RunCommand('Create Text into Window ' + IntToStr(CurrentWindowID) +
    Format(' "%s" + Chr$(10) + "%s"', [fInstruction.ProjectNumber, fInstruction.ProjectDesc]) +
    Format(' (13.63*%.4f-len("' + fInstruction.ProjectDesc + '")*0.025*%.4f, 9.9285*%.4f)', [fInstruction.XFactor,
      fInstruction.XFactor, fInstruction.YFactor]) +
    Format(' (14.3444*%.4f,10.1896*%.4f)', [fInstruction.XFactor, fInstruction.YFactor]) +
    ' Font FontRes Justify Center');

  fMapInfoManager.RunCommand('Create Text into Window ' + IntToStr(CurrentWindowID) +
    ' "1    O F    1"' +
    Format(' (13.1958*%.4f,9.5903*%.4f) (13.8958*%.4f,9.7597*%.4f)', [fInstruction.XFactor,
      fInstruction.YFactor, fInstruction.XFactor, fInstruction.YFactor]) +
    ' Font FontRes Justify Center');

  fMapInfoManager.RunCommand('FontRes = MakeFont("Times",1,int(18*'+
    Format('%.4f', [fInstruction.FontFactor])+'),0,-1)');
  fMapInfoManager.RunCommand('Create Text ' +
    '"' + fInstruction.StudyArea + '" + Chr$(10) + "' + fInstruction.fResultViewWorkspaceSetupStrategy.Title + '"' +
    Format(' (8.5*%.4f-len("' + fInstruction.fResultViewWorkspaceSetupStrategy.Title + '")*0.0625*%.4f, 9.2646*%.4f)', [fInstruction.XFactor,
      fInstruction.XFactor, fInstruction.YFactor]) +
    Format(' (9.6014*%.4f,9.8535*%.4f)', [fInstruction.XFactor, fInstruction.YFactor]) +
    ' Font FontRes Justify Center');
  fMapInfoManager.RunCommand('Set CoordSys Earth Projection 3, 33, "ft", '+
    '-120.5, 43.6666666667, 44.3333333333, 46, 8202099.7379999999, 0');

  fMapInfoManager.RunCommand('undim FontRes');
  fMapInfoManager.RunCommand('undim x');
  fMapInfoManager.RunCommand('undim y');
end;

{ TSetupMapPortrait_LegendPortrait }

constructor TSetupMapPortrait_LegendPortrait.Create(
  AMapInfoManager: TMapInfoDisplayManager; AInstruction: TBuildResultViewInstruction);
begin
  inherited Create(AMapInfoManager, AInstruction);
end;

procedure TSetupMapPortrait_LegendPortrait.SetupElements;
var
  CurrentWindowID: Integer;
begin
  fMapInfoManager.RunCommand('dim x as float');
  fMapInfoManager.RunCommand('dim y as float');

  fMapInfoManager.RunCommand('x = ' + Format('%.4f', [fInstruction.XFactor]));
  fMapInfoManager.RunCommand('y = ' + Format('%.4f', [fInstruction.YFactor]));

  fMapInfoManager.RunCommand('Run Application "' +
    SystemConfig.LayoutSetupFileName_MapPortrait_LegendPortrait + '"');

{
  	CurWinID = GetWinID$("Main Layout")
      FontStr = Makefont("Arial",3,int(7* fontsize),16777215,-1)
    Create Text into Window WinID
  		"File Location : (\" + Str$(PathToDirectory$(AppPath)+BsnName +"_"+CurMapCode+"_" + CurWorName + ".WOR")
      (0.5375*x,16.1729*y) (5.1611*x,16.2861*y)
  		Font FontStr
      Angle 90
        FontStr = Makefont("Arial Narrow",1,int(8* fontsize),0,-1)
    Create Text into Window WinID
      ProjNo +chr$(10) + ProjDesc
      (7.625*x,15.8639*y) (10.00*x,16.1194*y)
  		Font FontStr
      Justify Center
  	Create Text into Window WinID
      "1    O F    1"
      (7.75*x,15.2778*y) (9.0*x,15.4056*y)
  		Font FontStr
      Justify Center
      FontStr = Makefont("Times",1,int(18* fontsize),0,-1)
    Create Text
      BsnName + " " + Chr$(10)+ MapTitle
      (7.75*x,14.2215*y) (10.44*x,14.8104*y)
  		Font FontStr
      Justify Center
  	Set CoordSys Earth Projection 3, 33, "ft", -120.5, 43.6666666667, 44.3333333333, 46, 8202099.7379999999, 0
}

  fMapInfoManager.RunCommand('dim FontRes as Font');
  CurrentWindowID := fMapInfoManager.WindowID('Main Layout');
  fMapInfoManager.RunCommand('FontRes = MakeFont("Arial",3,int(7*'+
    Format('%.4f', [fInstruction.FontFactor])+'),16777215,-1)');
  fMapInfoManager.RunCommand('Create Text into Window ' + IntToStr(CurrentWindowID) +
    ' "File Location: (' + fInstruction.FileName + ')"' +
    Format(' (0.5375*%.4f,16.1729*%.4f) (5.1611*%.4f,16.2861*%.4f)', [fInstruction.XFactor,
      fInstruction.YFactor, fInstruction.XFactor, fInstruction.YFactor]) +
    ' Font FontRes Angle 90');
  fMapInfoManager.RunCommand('FontRes = MakeFont("Arial Narrow",1,int(8*'+
    Format('%.4f', [fInstruction.FontFactor])+'),0,-1)');
  fMapInfoManager.RunCommand('Create Text into Window ' + IntToStr(CurrentWindowID) +
    Format(' "%s" + Chr$(10) + "%s"', [fInstruction.ProjectNumber, fInstruction.ProjectDesc]) +
    Format(' (7.625*%.4f, 15.8639*%.4f)', [fInstruction.XFactor, fInstruction.YFactor]) +
    Format(' (10.00*%.4f, 16.1194*%.4f)', [fInstruction.XFactor, fInstruction.YFactor]) +
    ' Font FontRes Justify Center');
  fMapInfoManager.RunCommand('Create Text into Window ' + IntToStr(CurrentWindowID) +
    ' "1    O F    1"' +
    Format(' (7.75*%.4f,15.2778*%.4f) (9.0*%.4f,15.4056*%.4f)', [fInstruction.XFactor,
      fInstruction.YFactor, fInstruction.XFactor, fInstruction.YFactor]) +
    ' Font FontRes Justify Center');
  fMapInfoManager.RunCommand('FontRes = MakeFont("Times",1,int(18*'+
    Format('%.4f', [fInstruction.FontFactor])+'),0,-1)');
  fMapInfoManager.RunCommand('Create Text ' +
    '"' + fInstruction.StudyArea + '" + Chr$(10) + "' + fInstruction.fResultViewWorkspaceSetupStrategy.Title + '"' +
    Format(' (7.75*%.4f, 14.2215*%.4f)', [fInstruction.XFactor, fInstruction.YFactor]) +
    Format(' (10.44*%.4f, 14.8104*%.4f)', [fInstruction.XFactor, fInstruction.YFactor]) +
    ' Font FontRes Justify Center');
  fMapInfoManager.RunCommand('Set CoordSys Earth Projection 3, 33, "ft", '+
    '-120.5, 43.6666666667, 44.3333333333, 46, 8202099.7379999999, 0');

  fMapInfoManager.RunCommand('undim FontRes');
  fMapInfoManager.RunCommand('undim x');
  fMapInfoManager.RunCommand('undim y');
end;

{ TSetupMapPortrait_LegendLandscape }

constructor TSetupMapPortrait_LegendLandscape.Create(
  AMapInfoManager: TMapInfoDisplayManager; AInstruction: TBuildResultViewInstruction);
begin
  inherited Create(AMapInfoManager, AInstruction);
end;

procedure TSetupMapPortrait_LegendLandscape.SetupElements;
var
  CurrentWindowID: Integer;
begin
  fMapInfoManager.RunCommand('dim x as float');
  fMapInfoManager.RunCommand('dim y as float');

  fMapInfoManager.RunCommand('x = ' + Format('%.4f', [fInstruction.XFactor]));
  fMapInfoManager.RunCommand('y = ' + Format('%.4f', [fInstruction.YFactor]));

  fMapInfoManager.RunCommand('Run Application "' +
    SystemConfig.LayoutSetupFileName_MapPortrait_LegendLandscape + '"');

{
  	CurWinID = GetWinID$("Main Layout")
      FontStr = Makefont("Arial",3,int(7* fontsize),16777215,-1)
    Create Text into Window WinID
  		"File Location : (\" + Str$(PathToDirectory$(AppPath)+BsnName +"_"+CurMapCode+"_" + CurWorName + ".WOR")
      (0.5792*x,16.3035*y) (5.1875*x,16.4167*y)
  		Font FontStr
      Angle 90
        FontStr = Makefont("Arial Narrow",1,int(8* fontsize),0,-1)
    Create Text into Window WinID
      ProjNo +chr$(10) + ProjDesc
      (7.6632*x,16.0306*y) (9.0*x,16.2875*y)
  		Font FontStr
      Justify Center
  	Create Text into Window WinID
      "1    O F    1"
      (7.2694*x,15.6924*y) (7.9611*x,15.8576*y)
  		Font FontStr
      Justify Center
      FontStr = Makefont("Times",1,int(18* fontsize),0,-1)
    Create Text
      BsnName + " " + Chr$(10)+ MapTitle
      (4.69*x-len(MapTitle)*0.0625*x,15.516*y) (5.5118*x,15.809*y)
  		Font FontStr
      Justify Center
  	Set CoordSys Earth Projection 3, 33, "ft", -120.5, 43.6666666667, 44.3333333333, 46, 8202099.7379999999, 0
}

  fMapInfoManager.RunCommand('dim FontRes as Font');
  CurrentWindowID := fMapInfoManager.WindowID('Main Layout');
  fMapInfoManager.RunCommand('FontRes = MakeFont("Arial",3,int(7*'+
    Format('%.4f', [fInstruction.FontFactor])+'),16777215,-1)');
  fMapInfoManager.RunCommand('Create Text into Window ' + IntToStr(CurrentWindowID) +
    ' "File Location: (' + fInstruction.FileName + ')"' +
    Format(' (0.5792*%.4f,16.3035*%.4f) (5.1875*%.4f,16.4167*%.4f)', [fInstruction.XFactor,
      fInstruction.YFactor, fInstruction.XFactor, fInstruction.YFactor]) +
    ' Font FontRes Angle 90');
  fMapInfoManager.RunCommand('FontRes = MakeFont("Arial Narrow",1,int(8*'+
    Format('%.4f', [fInstruction.FontFactor])+'),0,-1)');
  fMapInfoManager.RunCommand('Create Text into Window ' + IntToStr(CurrentWindowID) +
    Format(' "%s" + Chr$(10) + "%s"', [fInstruction.ProjectNumber, fInstruction.ProjectDesc]) +
    Format(' (7.6632*%.4f, 16.0306*%.4f)', [fInstruction.XFactor, fInstruction.YFactor]) +
    Format(' (9.00*%.4f, 16.2875*%.4f)', [fInstruction.XFactor, fInstruction.YFactor]) +
    ' Font FontRes Justify Center');
  fMapInfoManager.RunCommand('Create Text into Window ' + IntToStr(CurrentWindowID) +
    ' "1    O F    1"' +
    Format(' (7.2694*%.4f,15.6924*%.4f) (7.9611*%.4f,15.8576*%.4f)', [fInstruction.XFactor,
      fInstruction.YFactor, fInstruction.XFactor, fInstruction.YFactor]) +
    ' Font FontRes Justify Center');
  fMapInfoManager.RunCommand('FontRes = MakeFont("Times",1,int(18*'+
    Format('%.4f', [fInstruction.FontFactor])+'),0,-1)');
  fMapInfoManager.RunCommand('Create Text ' +
    '"' + fInstruction.StudyArea + '" + Chr$(10) + "' + fInstruction.fResultViewWorkspaceSetupStrategy.Title + '"' +
    Format(' (4.69*%.4f-len("'+ fInstruction.fResultViewWorkspaceSetupStrategy.Title +'")*0.0625*%.4f, 15.516*%.4f)',
      [fInstruction.XFactor, fInstruction.XFactor, fInstruction.YFactor]) +
    Format(' (5.5118*%.4f, 15.809*%.4f)', [fInstruction.XFactor, fInstruction.XFactor,
      fInstruction.YFactor]) +
    ' Font FontRes Justify Center');
  fMapInfoManager.RunCommand('Set CoordSys Earth Projection 3, 33, "ft", '+
    '-120.5, 43.6666666667, 44.3333333333, 46, 8202099.7379999999, 0');

  fMapInfoManager.RunCommand('undim FontRes');
  fMapInfoManager.RunCommand('undim x');
  fMapInfoManager.RunCommand('undim y');
end;

{ TBaseWorkspaceStrategy }

constructor TBaseWorkspaceStrategy.Create(
  AMapInfoManager: TMapInfoDisplayManager;
  AInstruction: TBuildResultViewInstruction);
begin
  fMapInfoManager := AMapInfoManager;
  fInstruction := AInstruction;
end;

function TBaseWorkspaceStrategy.GetInstruction: TBuildResultViewInstruction;
begin
  Result := fInstruction;
end;

function TBaseWorkspaceStrategy.GetMapInfoManager: TMapInfoDisplayManager;
begin
  Result := fMapInfoManager;
end;

procedure TBaseWorkspaceStrategy.SetInstruction(
  Value: TBuildResultViewInstruction);
begin
  if fInstruction <> Value then
    fInstruction := Value;
end;

procedure TBaseWorkspaceStrategy.SetMapInfoManager(
  Value: TMapInfoDisplayManager);
begin
  if fMapInfoManager <> Value then
    fMapInfoManager := Value;
end;

procedure TBaseWorkspaceStrategy.SetupWorkspace;
begin
  // Do nothing
end;

function TBaseWorkspaceStrategy.ShortName: String;
begin
  Result := '(abstract)';
end;

function TBaseWorkspaceStrategy.Title: String;
begin
  Result := '(abstract workspace strategy - this should never appear)';
end;

{ TSystemWorkspace }

constructor TSystemWorkspace.Create(AMapInfoManager: TMapInfoDisplayManager;
  AInstruction: TBuildResultViewInstruction);
begin
  inherited;
end;

procedure TSystemWorkspace.SetupWorkspace;
begin
{
  	CurMapCode = "SYS"
  	MapTitle = " Explicit Model System"
  		Print "Creating" + MapTitle + " Map..."
  	Run Application "\\oberon\grp117\_ATtemp\_MITools\MdlMap\wors\MpBody\Mp01_System.WOR"
}

  fMapInfoManager.RunCommand('Run Application "'+
    IncludeTrailingPathDelimiter(SystemConfig.MasterFilesByName['master_wors']) + 'Mp01_System.WOR"');
end;

function TSystemWorkspace.ShortName: String;
begin
  Result := 'sys';
end;

function TSystemWorkspace.Title: String;
begin
  Result := 'System Map'
end;

{ TBasementSewerBackupRiskWorkspace }

constructor TBasementSewerBackupRiskWorkspace.Create(
  AMapInfoManager: TMapInfoDisplayManager;
  AInstruction: TBuildResultViewInstruction);
begin
  inherited;
end;

procedure TBasementSewerBackupRiskWorkspace.SetupWorkspace;
var
  TableE09FileName: TFileName;
  TableE10FileName: TFileName;
  TableE20FileName: TFileName;
  ResultsNodesFileName: TFileName;
  ResultsLinksFileName: TFileName;
  NumRecords: integer;
  DSCHGLsFileName: String;

  function CreateResultTableName(ResultDatabase: TFileName; TableName: String): String;
  begin
    Result := ExtractFilePath(ResultDatabase) + JustNameL(ResultDatabase) +
      TableName + '.TAB';
  end;

begin
{
  	MapTitle = " Basement Flooding "
  	CurMapCode = "ChikBF"
  		Print "Creating" + MapTitle + " Map..."
  	'goto doEMGATTS  	'===================> DELETE FROM THIS LINE AFTER EMGATTS PICKUP
  	'doEMGATTS:			  '===================> DELETE TO THIS LINE AFTER EMGATTS PICKUP

  '==========================================================================
  'PATCH FOR FUTURE BASE 25 YR CONDITIONS: Case; Surcharge pipes missing'
  Select * from mdl_links_ac where SimLinkId = "" into QyUpdt
  Update QyUpdate set SimLinkId = "M" + MLinkId
  Commit table mdl_links_ac
  Close table QyUpdt
  '==========================================================================
  	Register Table Str$(TblPath + "\mdbs\ModelResults.mdb")
  		Type ACCESS Table "tableE09"
  		Into Str$(TblPath + "\mdbs\tableE09.TAB")
  	Open Table Str$(TblPath + "\mdbs\tableE09") As tableE09 Interactive
  	Register Table Str$(TblPath + "\mdbs\ModelResults.mdb")
  		Type ACCESS Table "tableE10"
  		Into Str$(TblPath + "\mdbs\tableE10.TAB")
  	Open Table Str$(TblPath + "\mdbs\tableE10") As tableE10 Interactive
  	Register Table Str$(TblPath + "\mdbs\ModelResults.mdb")
  		Type ACCESS Table "tableE20"
  		Into Str$(TblPath + "\mdbs\tableE20.TAB")
  	Open Table Str$(TblPath + "\mdbs\tableE20") As tableE20 Interactive
  	Select *  from tableE09, mdl_Nodes_ac   where  tableE09 . NodeName = mdl_Nodes_ac . Node into ResultsNodes noselect
  	'====>Select *  from  tableE10, mdl_Links_ac   where tableE10 . CondName = mdl_Links_ac . SimLinkID  into ResultsLinks noselect
  	Select *  from  mdl_Links_ac , tableE10  where mdl_Links_ac . SimLinkID =tableE10 . CondName  into ResultsLinks noselect
  	Commit Table ResultsNodes As Str$(TblPath + "\nodes\BFNodesE09.TAB") TYPE NATIVE Charset "WindowsLatin1"
  	Commit Table ResultsLinks As Str$(TblPath + "\nodes\BFLinksE10.TAB") TYPE NATIVE Charset "WindowsLatin1"
  	Open Table "C:\CASSIO\modeling\BASEMENT\Flood_parcel_hits_ac" As Flood_parcel_hits_102001 Interactive
  	Open Table Str$(TblPath + "nodes\BFNodesE09") As BFNodesE09 Interactive
  	Open Table Str$(TblPath + "nodes\BFLinksE10") As BFLinksE10 Interactive
  	Open Table Str$(TblPath + "laterals\mdl_laterals_ac.TAB") Interactive
  	'xOUT 091205 ===> Add Column "mdl_DirSC_ac" (SwrCrwnEL Float)From mdl_laterals_ac Set To DSSewerIE + SewerDiameter/12 Where COL1 = COL2  Dynamic
  	Close table tableE10
  	Close table tableE09
  	'=====>CREATING BASEMENT FLOODING MAP
  	Run Application "\\oberon\grp117\_ATtemp\_MITools\MdlMap\wors\MpBody\Mp02_BF.wor"
}

{
    // Account for missing surcharge pipes
    fMapInfoManager.RunCommand('Select * from mdl_links_ac where SimLinkId = "" into QyUpdt');
    fMapInfoManager.RunCommand('Update QyUpdate set SimLinkId = "M" + MLinkId');
    fMapInfoManager.RunCommand('Commit table mdl_links_ac');
    fMapInfoManager.RunCommand('Close table QyUpdt');  
}

  // Register model result tables
  TableE09FileName := CreateResultTableName(fInstruction.ResultDatabase, '_TableE09');
  if not FileExists(TableE09FileName) then
    fMapInfoManager.RunCommand('Register Table "' + fInstruction.ResultDatabase + '"' +
      ' Type ACCESS Table "tableE09" Into "' + TableE09FileName + '"');
  fMapInfoManager.RunCommand('Open Table "' + TableE09FileName + '" As tableE09 Interactive');

  TableE10FileName := CreateResultTableName(fInstruction.ResultDatabase, '_TableE10');
  if not FileExists(TableE10FileName) then
    fMapInfoManager.RunCommand('Register Table "' + fInstruction.ResultDatabase + '"' +
      ' Type ACCESS Table "tableE10" Into "' + TableE10FileName + '"');
  fMapInfoManager.RunCommand('Open Table "' + TableE10FileName + '" As tableE10 Interactive');

  TableE20FileName := CreateResultTableName(fInstruction.ResultDatabase, '_TableE20');
  if not FileExists(TableE20FileName) then
    fMapInfoManager.RunCommand('Register Table "' + fInstruction.ResultDatabase + '"' +
      ' Type ACCESS Table "tableE20" Into "' + TableE20FileName + '"');
  fMapInfoManager.RunCommand('Open Table "' + TableE20FileName + '" As tableE20 Interactive');

  DSCHGLsFileName := CreateResultTableName(fInstruction.ResultDatabase, '_DSCHGLs');
  if not FileExists(DSCHGLsFileName) then
    fMapInfoManager.RunCommand('Register Table "' + fInstruction.ResultDatabase + '"' +
      ' Type ACCESS Table "DSCHGLs" Into "' + DSCHGLsFileName + '"');
  fMapInfoManager.RunCommand('Open Table "' + DSCHGLsFileName + '" As DSCHGLs Interactive');

  // Do the basement flooding selections
  fMapInfoManager.RunCommand('Select *  from tableE09, mdl_Nodes_ac ' +
    'where tableE09.NodeName = mdl_Nodes_ac.Node into ResultsNodes noselect');
  fMapInfoManager.RunCommand('Select *  from  mdl_Links_ac, tableE10 ' +
    'where mdl_Links_ac.SimLinkID = tableE10.CondName into ResultsLinks noselect');
  ResultsNodesFileName := CreateResultTableName(fInstruction.ResultDatabase, '_BFNodesE09');
  fMapInfoManager.RunCommand('Commit Table ResultsNodes As "' + ResultsNodesFileName + '" ' +
    'TYPE NATIVE Charset "WindowsLatin1"');
  ResultsLinksFileName := CreateResultTableName(fInstruction.ResultDatabase, '_BFLinksE10');
  fMapInfoManager.RunCommand('Commit Table ResultsLinks As "' + ResultsLinksFileName + '" ' +
    'TYPE NATIVE Charset "WindowsLatin1"');

  // Open tables for processing
{$IFDEF HERMESLAPTOP}
  fMapInfoManager.RunCommand('Open Table "C:\CASSIO\modeling\BASEMENT\Flood_parcel_hits_102001" As Flood_parcel_hits_102001 Interactive');
{$ELSE}
  fMapInfoManager.RunCommand('Open Table "\\CASSIO\modeling\BASEMENT\Flood_parcel_hits_102001" As Flood_parcel_hits_102001 Interactive');
{$ENDIF}
  fMapInfoManager.RunCommand('Open Table "' + ResultsNodesFileName + '" As BFNodesE09 Interactive');
  fMapInfoManager.RunCommand('Open Table "' + ResultsLinksFileName + '" As BFLinksE10 Interactive');

{
  Print "Checking Special Pipes..."
  If
   tableinfo (SpecialPipesZero_Qry,8) = 0 then
   		Open Table "\\Oberon\Grp117\_Attemp\_MiTools\MdlMap\Const\DummyObj.tab" Interactive
   		Select *  from DummyObj  where Str$(obj)= "Polyline"  into SpecialPipesZero_Qry noselect
  		Print "SpecialPipesZero => Dummy Object"

  End if
}
  fMapInfoManager.RunCommand('Select *  from BFLinksE10  where PipeShape <> "CIRC" and DesignQ = 0  into SpecialPipesZero_Qry noselect');
  NumRecords := fMapInfoManager.NumRecords('SpecialPipesZero_Qry');
  if NumRecords = 0 then
  begin
    // Create an empty SpecialPipesZero_Qry table
    if not fMapInfoManager.TableIsOpen('DummyObj') then
      fMapInfoManager.OpenTable(SystemConfig.MasterFilesByName['master_wors']+'\DummyObj.TAB');
    fMapInfoManager.RunCommand('Select * from DummyObj where str$(obj) = "polyline" into SpecialPipesZero_qry noselect');
  end;

  fMapInfoManager.RunCommand('Run Application "'+
    IncludeTrailingPathDelimiter(SystemConfig.MasterFilesByName['master_wors']) +'Mp02_BF.WOR"');

end;

function TBasementSewerBackupRiskWorkspace.ShortName: String;
begin
  Result := 'chikbf';
end;

function TBasementSewerBackupRiskWorkspace.Title: String;
begin
  Result := 'Basement Sewer Backup Risk';
end;

{ TTopographyWorkspace }

constructor TTopographyWorkspace.Create(AMapInfoManager: TMapInfoDisplayManager;
  AInstruction: TBuildResultViewInstruction);
begin
  inherited;
end;

procedure TTopographyWorkspace.SetupWorkspace;
begin
  fMapInfoManager.RunCommand('Run Application "'+
    IncludeTrailingPathDelimiter(SystemConfig.MasterFilesByName['master_wors']) +'Mp03_Topo.WOR"');
end;

function TTopographyWorkspace.ShortName: String;
begin
  Result := 'topo';
end;

function TTopographyWorkspace.Title: String;
begin
  Result := 'Boundary and Topography';
end;

{ TLargeUtilitiesTrafficWorkspace }

constructor TLargeUtilitiesTrafficWorkspace.Create(
  AMapInfoManager: TMapInfoDisplayManager;
  AInstruction: TBuildResultViewInstruction);
begin
  inherited;
end;

procedure TLargeUtilitiesTrafficWorkspace.SetupWorkspace;
begin
  fMapInfoManager.RunCommand('Run Application "'+
    IncludeTrailingPathDelimiter(SystemConfig.MasterFilesByName['master_wors']) +'Mp04_Util.WOR"');
end;

function TLargeUtilitiesTrafficWorkspace.ShortName: String;
begin
  Result := 'largeutils';
end;

function TLargeUtilitiesTrafficWorkspace.Title: String;
begin
  Result := 'Large Utilities/Traffic';
end;

{ TAreaExistingLandUseWorkspace }

constructor TAreaExistingLandUseWorkspace.Create(
  AMapInfoManager: TMapInfoDisplayManager;
  AInstruction: TBuildResultViewInstruction);
begin
  inherited;
end;

procedure TAreaExistingLandUseWorkspace.SetupWorkspace;
begin
  fMapInfoManager.RunCommand('Run Application "'+
    IncludeTrailingPathDelimiter(SystemConfig.MasterFilesByName['master_wors']) +'Mp05_GenZone.WOR"');
end;

function TAreaExistingLandUseWorkspace.ShortName: String;
begin
  Result := 'exlu';
end;

function TAreaExistingLandUseWorkspace.Title: String;
begin
  Result := 'Area Existing Land Use';
end;

{ TComprehensivePlanWorkspace }

constructor TComprehensivePlanWorkspace.Create(
  AMapInfoManager: TMapInfoDisplayManager;
  AInstruction: TBuildResultViewInstruction);
begin
  inherited;
end;

procedure TComprehensivePlanWorkspace.SetupWorkspace;
begin
  fMapInfoManager.RunCommand('Run Application "'+
    IncludeTrailingPathDelimiter(SystemConfig.MasterFilesByName['master_wors']) +'Mp06_CMPZone.WOR"');
end;

function TComprehensivePlanWorkspace.ShortName: String;
begin
  Result := 'cmplu';
end;

function TComprehensivePlanWorkspace.Title: String;
begin
  Result := 'Comprehensive Plan';
end;

{ TEnvironmentalConsiderationsWorkspace }

constructor TEnvironmentalConsiderationsWorkspace.Create(
  AMapInfoManager: TMapInfoDisplayManager;
  AInstruction: TBuildResultViewInstruction);
begin
  inherited;
end;

procedure TEnvironmentalConsiderationsWorkspace.SetupWorkspace;
begin
  fMapInfoManager.RunCommand('Run Application "'+
    IncludeTrailingPathDelimiter(SystemConfig.MasterFilesByName['master_wors']) +'Mp07_EnvZone.WOR"');
end;

function TEnvironmentalConsiderationsWorkspace.ShortName: String;
begin
  Result := 'envzone';
end;

function TEnvironmentalConsiderationsWorkspace.Title: String;
begin
  Result := 'Environmental Considerations';
end;

{ TSewerSurchargeRiskWorkspace }

constructor TSewerSurchargeRiskWorkspace.Create(
  AMapInfoManager: TMapInfoDisplayManager;
  AInstruction: TBuildResultViewInstruction);
begin
  inherited;
end;

procedure TSewerSurchargeRiskWorkspace.SetupWorkspace;
var
  TableE09FileName: String;
  TableE10FileName: String;
  TableE20FileName: String;
  DSCHGLsFileName: String;
  ResultsNodesFileName: string;
  ResultsLinksFileName: string;

  function CreateResultTableName(ResultDatabase: TFileName; TableName: String): String;
  begin
    Result := ExtractFilePath(ResultDatabase) + JustNameL(ResultDatabase) +
      TableName + '.TAB';
  end;

begin
{
  	Register Table Str$(TblPath + "\mdbs\ModelResults.mdb")
  		Type ACCESS Table "tableE09"
  		Into Str$(TblPath + "\mdbs\tableE09.TAB")
  	Open Table Str$(TblPath + "\mdbs\tableE09") As tableE09 Interactive
  	Register Table Str$(TblPath + "\mdbs\ModelResults.mdb")
  		Type ACCESS Table "tableE10"
  		Into Str$(TblPath + "\mdbs\tableE10.TAB")
  	Open Table Str$(TblPath + "\mdbs\tableE10") As tableE10 Interactive
  	Register Table Str$(TblPath + "\mdbs\ModelResults.mdb")
  		Type ACCESS Table "tableE20"
  		Into Str$(TblPath + "\mdbs\tableE20.TAB")
  	Open Table Str$(TblPath + "\mdbs\tableE20") As tableE20 Interactive
  	Select *  from tableE09, mdl_Nodes_ac   where  tableE09 . NodeName = mdl_Nodes_ac . Node into ResultsNodes noselect
  	Select *  from  mdl_Links_ac , tableE10  where mdl_Links_ac . SimLinkID =tableE10 . CondName  into ResultsLinks noselect
  	Commit Table ResultsNodes As Str$(TblPath + "\nodes\BFNodesE09.TAB") TYPE NATIVE Charset "WindowsLatin1"
  	Commit Table ResultsLinks As Str$(TblPath + "\nodes\BFLinksE10.TAB") TYPE NATIVE Charset "WindowsLatin1"
  	Open Table "C:\CASSIO\modeling\BASEMENT\Flood_parcel_hits_ac" As Flood_parcel_hits_102001 Interactive
  	Open Table Str$(TblPath + "nodes\BFNodesE09") As BFNodesE09 Interactive
  	Open Table Str$(TblPath + "nodes\BFLinksE10") As BFLinksE10 Interactive
  	Open Table Str$(TblPath + "laterals\mdl_laterals_ac.TAB") Interactive
  	Add Column "mdl_DirSC_ac" (SwrCrwnEL Float)From mdl_laterals_ac Set To DSSewerIE + SewerDiameter/12 Where COL1 = COL2  Dynamic
  	Close table tableE10
  	Close table tableE09
}

  TableE09FileName := CreateResultTableName(fInstruction.ResultDatabase, '_TableE09');
  if not FileExists(TableE09FileName) then
    fMapInfoManager.RunCommand('Register Table "' + fInstruction.ResultDatabase + '"' +
      ' Type ACCESS Table "tableE09" Into "' + TableE09FileName + '"');
  fMapInfoManager.RunCommand('Open Table "' + TableE09FileName + '" As tableE09 Interactive');

  TableE10FileName := CreateResultTableName(fInstruction.ResultDatabase, '_TableE10');
  if not FileExists(TableE10FileName) then
    fMapInfoManager.RunCommand('Register Table "' + fInstruction.ResultDatabase + '"' +
      ' Type ACCESS Table "tableE10" Into "' + TableE10FileName + '"');
  fMapInfoManager.RunCommand('Open Table "' + TableE10FileName + '" As tableE10 Interactive');

  TableE20FileName := CreateResultTableName(fInstruction.ResultDatabase, '_TableE20');
  if not FileExists(TableE20FileName) then
    fMapInfoManager.RunCommand('Register Table "' + fInstruction.ResultDatabase + '"' +
      ' Type ACCESS Table "tableE20" Into "' + TableE20FileName + '"');
  fMapInfoManager.RunCommand('Open Table "' + TableE20FileName + '" As tableE20 Interactive');

  DSCHGLsFileName := CreateResultTableName(fInstruction.ResultDatabase, '_DSCHGLs');
  if not FileExists(DSCHGLsFileName) then
    fMapInfoManager.RunCommand('Register Table "' + fInstruction.ResultDatabase + '"' +
      ' Type ACCESS Table "DSCHGLs" Into "' + DSCHGLsFileName + '"');
  fMapInfoManager.RunCommand('Open Table "' + DSCHGLsFileName + '" As DSCHGLs Interactive');

  // Do the basement flooding selections
  fMapInfoManager.RunCommand('Select *  from tableE09, mdl_Nodes_ac ' +
    'where tableE09.NodeName = mdl_Nodes_ac.Node into ResultsNodes noselect');
  fMapInfoManager.RunCommand('Select *  from  mdl_Links_ac, tableE10 ' +
    'where mdl_Links_ac.SimLinkID = tableE10.CondName into ResultsLinks noselect');
  ResultsNodesFileName := CreateResultTableName(fInstruction.ResultDatabase, '_BFNodesE09');
  fMapInfoManager.RunCommand('Commit Table ResultsNodes As "' + ResultsNodesFileName + '" ' +
    'TYPE NATIVE Charset "WindowsLatin1"');
  ResultsLinksFileName := CreateResultTableName(fInstruction.ResultDatabase, '_BFLinksE09');
  fMapInfoManager.RunCommand('Commit Table ResultsLinks As "' + ResultsLinksFileName + '" ' +
    'TYPE NATIVE Charset "WindowsLatin1"');

  // Open tables for processing
{$IFDEF HERMESLAPTOP}
  fMapInfoManager.RunCommand('Open Table "C:\CASSIO\modeling\BASEMENT\Flood_parcel_hits_102001" As Flood_parcel_hits_102001 Interactive');
{$ELSE}
  fMapInfoManager.RunCommand('Open Table "\\CASSIO\modeling\BASEMENT\Flood_parcel_hits_102001" As Flood_parcel_hits_102001 Interactive');
{$ENDIF}
  fMapInfoManager.RunCommand('Open Table "' + ResultsNodesFileName + '" As BFNodesE09 Interactive');
  fMapInfoManager.RunCommand('Open Table "' + ResultsLinksFileName + '" As BFLinksE10 Interactive');

  fMapInfoManager.RunCommand('Run Application "'+
    IncludeTrailingPathDelimiter(SystemConfig.MasterFilesByName['master_wors']) +'Mp08_SurchRisk.WOR"');
end;

function TSewerSurchargeRiskWorkspace.ShortName: String;
begin
  Result := 'swrsurch';
end;

function TSewerSurchargeRiskWorkspace.Title: String;
begin
  Result := 'Sewer Surcharge Risk';
end;

{ TImpPctIncreaseWorkspace }

constructor TImpPctIncreaseWorkspace.Create(
  AMapInfoManager: TMapInfoDisplayManager;
  AInstruction: TBuildResultViewInstruction);
begin
  inherited;
end;

procedure TImpPctIncreaseWorkspace.SetupWorkspace;
begin
  fMapInfoManager.RunCommand('Run Application "'+
    IncludeTrailingPathDelimiter(SystemConfig.MasterFilesByName['master_wors']) +'Mp10_DeltaImp.WOR"');
end;

function TImpPctIncreaseWorkspace.ShortName: String;
begin
  Result := 'deltaimp';
end;

function TImpPctIncreaseWorkspace.Title: String;
begin
  Result := 'Impervious Pct Increase';
end;

{ TExistingLandUseDSCWorkspace }

constructor TExistingLandUseDSCWorkspace.Create(
  AMapInfoManager: TMapInfoDisplayManager;
  AInstruction: TBuildResultViewInstruction);
begin
  inherited;
end;

procedure TExistingLandUseDSCWorkspace.SetupWorkspace;
begin
  fMapInfoManager.RunCommand('Run Application "'+
    IncludeTrailingPathDelimiter(SystemConfig.MasterFilesByName['master_wors']) +'Mp12_DscExLU.wor"');
end;

function TExistingLandUseDSCWorkspace.ShortName: String;
begin
  Result := 'dscexlu';
end;

function TExistingLandUseDSCWorkspace.Title: String;
begin
  Result := 'Existing Land Use (DSC)';
end;

{ TComprehensivePlanDSCWorkspace }

constructor TComprehensivePlanDSCWorkspace.Create(
  AMapInfoManager: TMapInfoDisplayManager;
  AInstruction: TBuildResultViewInstruction);
begin
  inherited;
end;

procedure TComprehensivePlanDSCWorkspace.SetupWorkspace;
begin
  fMapInfoManager.RunCommand('Run Application "'+
    IncludeTrailingPathDelimiter(SystemConfig.MasterFilesByName['master_wors']) +'Mp13_DscCmpPln.wor"');
end;

function TComprehensivePlanDSCWorkspace.ShortName: String;
begin
  Result := 'dsccmppln';
end;

function TComprehensivePlanDSCWorkspace.Title: String;
begin
  Result := 'Comprehensive Plan (DSC)';
end;

{ TSystemQAQCWorkspace }

constructor TSystemQAQCWorkspace.Create(AMapInfoManager: TMapInfoDisplayManager;
  AInstruction: TBuildResultViewInstruction);
begin
  inherited;
end;

procedure TSystemQAQCWorkspace.SetupWorkspace;
begin

  fMapInfoManager.RunCommand('Run Application "'+
    IncludeTrailingPathDelimiter(SystemConfig.MasterFilesByName['master_wors']) +'Mp14_QaQc.wor"');
end;

function TSystemQAQCWorkspace.ShortName: String;
begin
  Result := 'qaqc';
end;

function TSystemQAQCWorkspace.Title: String;
begin
  Result := 'System QA/QC';
end;

end.
