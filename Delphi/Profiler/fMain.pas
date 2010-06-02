unit fMain;

interface

uses
	Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, Ruler, ExtCtrls, OleCtrls, MapXLib_TLB, Types,
	RzSplit, RzPanel, dxBar, ImgList, dxBarExtItems, StdCtrls, ActnList,
	dxCntner, DB, ADODB, Contnrs, dxDBTLCl, dxGrClms,
	dxDBGrid, dxTL, dxDBCtrl, dxEditor, dxEdLib, Coords, Math, Printers,
	RzLabel, StdActns, StrUtils, dxExEdtr, TransparentPanel, CSIntf, PSWindow,
	PSJob, StBase, Registry, IniFiles, dxPageControl, RzBorder, dxDBEdtr,
	dxDBELib, RzButton, dxBarExtDBItems, StStrL,
  RzShellDialogs;


// {$R RES\MerlVu.RES}

const
	CurrentEmgaatsVersion = '2.11';
	SelectionPenWidth: Integer = 5;
	HandleSize: Integer = 3;
	HitTol: Integer = 3;
	NonConduitLinkLength: Double = 350.0;
	AnimateSteps: Integer = 7;

	ChangesDirName: String = '\qc\';
	ChangesDBName: String = 'Changes.mdb';
	ChangesName: String = 'Changes';
	MasterChangesDirName: String = '\maint\';


	LinksDirName: String = '\links\';
	LinksDBName: String = 'mdl_links_ac.mdb';
	MasterLinksDBName: String = 'mst_links_ac.mdb';
	MasterLinksName: String = 'mst_links_ac';
	LinksName: String = 'mdl_links_ac';
	NodesDirName: String = '\nodes\';
	NodesDBName: String = 'mdl_nodes_ac.mdb';
	MasterNodesDBName: String = 'mst_nodes_ac.mdb';
	MasterNodesName: String = 'mst_nodes_ac';
	NodesName: String = 'mdl_nodes_ac';
	ModelRegistryName: String = 'model.ini';

	AppRegistryKey: String = 'Software\BES\Profiler';
	AppRegistryDirectoryKey: String = 'Directory';
	AppRegistryToolBarKey: String = 'Toolbars';
	crPlanSelect = 1;
	crShapeEdit = 2;
	crPlanModel = 3;
	crZoom = 4;
	crPan = 5;
	crLine = 6;
	crPolyline = 7;
	crRect = 8;
	crEllipse = 9;
	crText = 10;
	crCrossHair = 11;
	crSelectNode = 12;
	crSelectLink = 13;
	crSelectLine = 14;
	crSelectEllipse = 15;
	crSelectRect = 16;
	crSelectPolyline = 17;
	crSelectText = 18;
	crZoomIn = 19;
	crZoomOut = 20;
	crSelectOrifice = 21;
	crSelectPump = 22;
	crSelectPath = 23;
	crGrElevEd = 24;
	crInElevEd = 25;
	crOfElevEd = 26;
	crCondShift = 27;
	crCrElevEd = 28;
	crNodeShift = 29;
	crInDn = 30;
	crInUp = 31;

type
	// TTrackerState identifies the type of tracking displayed by an object; it is
	// either selected or being flown over by the mouse
	TTrackerState = (tsSelected, tsFlyOver);
	// TDrawStyle identifies the visual state of the object:
	//   dsSelected: the object is selected
	//   dsEditing: the object is being edited
	//   dsFlyOver: the mouse is over the object
	//   dsTemp: the object is being "rubberbanded"
	//   dsPath: the object is part of a path
	//   dsSimple: the object is to be drawn with no adornments (lines only or
	//     small rectangles for points
	//   dsFlow: the object shows flow information
	TDrawStyle = (dsSelected, dsEditing, dsFlyOver, dsTemp, dsPath, dsSimple,
		dsFlow);
	// TDrawStyleSet is used since more than one visual state can be represented
	// for an object
	TDrawStyleSet = set of TDrawStyle;

	TDocObj = class(TPersistent)
	private
		FPosition: TDRectangle;
		procedure SetPosition(Value: TDRectangle);
	public
		constructor Create;
		destructor Destroy; override;
		// Manipulator methods
		function NumHandles: Integer; virtual;
		function GetHandle(WhichHandle: Integer): TDPoint; virtual;
		function GetHandleRect(WhichHandle: Integer): TRect;
		function GetHandleCursor(WhichHandle: Integer): TCursor; virtual;
		procedure MoveTo(APosition: TDRectangle); virtual;
		procedure MoveHandleTo(var AHandle: Integer; APoint: TDPoint); virtual;
		procedure EditProperties; virtual;
		// Draw functions
		procedure Draw(ACanvas: TCanvas;
			DrawStyle: TDrawStyleSet; IsPrinting: Boolean = False); virtual; abstract;
		procedure DrawTracker(ACanvas: TCanvas;
			TrackerState: TTrackerState); virtual;
		// Queries
		function HitTest(APoint: TDPoint): Integer; virtual;
		function Intersects(ARect: TDRectangle): Boolean; virtual;
		function GetExtents: TDRectangle; virtual;
		function ObjectType: String; virtual; abstract;
		procedure Assign(Source: TPersistent); override;
	published
		property Position: TDRectangle read FPosition write SetPosition;
	end;

	//////////////////////////////////////////////////////////////////////////////
	// TModelObj
	//  Base class for all SWMM objects
	//
	TModelObj = class(TDocObj)
	private
		FAlias: String;
		FLocked: Boolean;
		FComments: String;
		procedure SetAlias(Value: String);
		procedure SetLocked(Value: Boolean);
		procedure SetComments(Value: String);
	protected
		function GetAsString: String; virtual; abstract;
		function GetComments: String; virtual;
	public
		constructor Create;
		destructor Destroy; override;
		procedure Assign(Source: TPersistent); override;
	published
		property Alias: String read FAlias write SetAlias;
		property Locked: Boolean read FLocked write SetLocked default False;
		property Comments: String read FComments write SetComments;
		property AsString: String read GetAsString;
	end;

	//////////////////////////////////////////////////////////////////////////////
	// TLink
	//  Base class for link objects
	//
	TLink = class(TModelObj)
	private
		FUpJunc: String;
		FDnJunc: String;
		FVertices: array of TDPoint;
		procedure SetUpJunc(Value: String);
		procedure SetDnJunc(Value: String);
		function GetVertex(Idx: Integer): TDPoint;
		procedure SetVertex(Idx: Integer; Value: TDPoint);
		function GetMidPoint: TDPoint;
	public
		constructor Create;
		destructor Destroy; override;
		procedure Draw(ACanvas: TCanvas;
			DrawStyle: TDrawStyleSet; IsPrinting: Boolean = False); override;
		procedure DrawTracker(ACanvas: TCanvas;
			TrackerState: TTrackerState); override;
		procedure MoveTo(APosition: TDRectangle); override;
		function HitTest(APoint: TDPoint): Integer; override;
		function Intersects(ARect: TDRectangle): Boolean; override;
		function GetHandle(WhichHandle: Integer): TDPoint; override;
		function GetHandleCursor(WhichHandle: Integer): TCursor; override;
		function GetExtents: TDRectangle; override;
		procedure MoveHandleTo(var AHandle: Integer;
			APoint: TDPoint); override;
		procedure EditProperties; override;
		procedure Reposition;
		property Vertices[Idx: Integer]: TDPoint read GetVertex write SetVertex;
		procedure AddVertex(Idx: Integer; AVertex: TDPoint);
		procedure DeleteVertex(Idx: Integer);
		function NumHandles: Integer; override;
		procedure Assign(Source: TPersistent); override;
	published
		property UpJunc: String read FUpJunc write SetUpJunc;
		property DnJunc: String read FDnJunc write SetDnJunc;
		property MidPoint: TDPoint read GetMidPoint;
	end;

	TConduit = class(TLink)
	private
    FLinkID: Integer;
		FCompKey: Integer;
		FVertDim: Double;
		FHorizDim: Double;
		FLength: Double;
		FUpInvert: Double;
		FDnInvert: Double;
		FUpDepth: Double;
		FDnDepth: Double;
		FUpGround: Double;
		FDnGround: Double;
		FAsBuilt: String;
		FMapinfoID: Integer;
		FLinkType: String;
		FUpType: String;
		FDnType: String;
    FShape: String;
    FPipeType: String;
    FDataSource: String;
    FFlowType: String;
    FIsSpecLink: Boolean;
    procedure SetLinkID(Value: Integer);
		procedure SetCompKey(Value: Integer);
		procedure SetVertDim(Value: Double);
		procedure SetHorizDim(Value: Double);
		procedure SetLength(Value: Double);
		procedure SetUpInvert(Value: Double);
		procedure SetDnInvert(Value: Double);
		procedure SetUpDepth(Value: Double);
		procedure SetDnDepth(Value: Double);
		function GetUpCrown: Double;
		function GetDnCrown: Double;
		function GetSlope: Double;
		function GetDnGround: Double;
		function GetUpGround: Double;
		procedure SetDnGround(const Value: Double);
		procedure SetUpGround(const Value: Double);
		procedure SetAsBuilt(const Value: String);
		procedure SetMapinfoID(const Value: Integer);
		procedure SetLinkType(const Value: String);
		procedure SetUpType(const Value: String);
		procedure SetDnType(const Value: String);
    procedure SetShape(const Value: String);
    procedure SetPipeType(Const Value: String);
    procedure SetDataSource(const Value: String);
    procedure SetFlowType(const Value: String);
    procedure SetIsSpecLink(const Value: Boolean);
	protected
		function GetAsString: String; override;
	public
		constructor Create;
		constructor CreateFromDB(ALinkID: Integer);
		procedure RecallFromDB;
		procedure SaveToDB;
		destructor Destroy; override;
		procedure Draw(ACanvas: TCanvas;
			DrawStyle: TDrawStyleSet; IsPrinting: Boolean = False); override;
		procedure DrawTracker(ACanvas: TCanvas;
			TrackerState: TTrackerState); override;
		function ObjectType: String; override;
		function HitTest(APoint: TDPoint): Integer; override;
		function NumHandles: Integer; override;
		function GetHandle(WhichHandle: Integer): TDPoint; override;
		function GetExtents: TDRectangle; override;
		function GetBestVertDim(TrialDim: Double): Double;
		procedure MoveHandleTo(var AHandle: Integer; APoint: TDPoint); override;
		procedure Assign(Source: TPersistent); override;
    procedure SetDataSourceItem(APos: Integer; ACode: Char);
	published
    property LinkID: Integer read FLinkID write SetLinkID;
		property CompKey: Integer read FCompKey write SetCompKey;
		property MapinfoID: Integer read FMapinfoID write SetMapinfoID;
		property VertDim: Double read FVertDim write SetVertDim;
		property HorizDim: Double read FHorizDim write SetHorizDim;
		property Length: Double read FLength write SetLength;
		property UpInvert: Double read FUpInvert write SetUpInvert;
		property DnInvert: Double read FDnInvert write SetDnInvert;
		property UpDepth: Double read FUpDepth write SetUpDepth;
		property DnDepth: Double read FDnDepth write SetDnDepth;
		property UpGround: Double read FUpGround write SetUpGround;
		property DnGround: Double read FDnGround write SetDnGround;
		property UpCrown: Double read GetUpCrown;
		property DnCrown: Double read GetDnCrown;
		property Slope: Double read GetSlope;
		property AsBuilt: String read FAsBuilt write SetAsBuilt;
		property LinkType: String read FLinkType write SetLinkType;
		property UpType: String read FUpType write SetUpType;
		property DnType: String read FDnType write SetDnType;
    property Shape: String read FShape write SetShape;
    property PipeType: String read FPipeType write SetPipeType;
    property DataSource: String read FDataSource write SetDataSource;
    property FlowType: String read FFlowType write SetFlowType;
    property IsSpecLink: Boolean read fIsSpecLink write SetIsSpecLink;
	end;

	TAdjacentDir = (adIn, adOut);
	TAdjacent = class
	public
		Position: Integer;
		FConduit: TConduit;
		constructor Create;
		destructor Destroy; override;
	end;

	TPath = class(TModelObj)
	private
		FObjects: TObjectList;
		FInAdjacents: TObjectList;
		FOutAdjacents: TObjectList;
		FExpandedLinks: TObjectList;
		FColor: TColor;
		FWidth: Integer;
		procedure SetObjects(Value: TObjectList);
		procedure SetExpandedLinks(Value: TObjectList);
		procedure SetColor(Value: TColor);
		procedure SetWidth(Value: Integer);
		function GetPathDescription: String;
	public
		constructor Create;
		destructor Destroy; override;
		function ObjectType: String; override;
		function Sort: Boolean;
		function GetExtents: TDRectangle; override;
		function GetOtherAdjacents(AConduit: TConduit): TObjectList;
		function GetUpstreamAdjacents(AConduit: TConduit): TObjectList;
		function GetDownstreamAdjacents(AConduit: TConduit): TObjectList;
		{ Path manipulations }
		function AddUpstreamObj(AObj: Integer): Boolean;
		function AddDownstreamObj(AObj: Integer): Boolean;
		function AddUpstream: Boolean;
		function AddDownstream: Boolean;
		function DeleteUpstreamObj: Boolean;
		function DeleteDownstreamObj: Boolean;
		function DeleteUpstreamOfObj(AObj: TModelObj): Boolean;
		function DeleteDownstreamOfObj(AObj: TModelObj): Boolean;
		function NextHighestUpInvert(APos: Integer; AY: Double): Double;
		function NextLowestUpInvert(APos: Integer; AY: Double): Double;
		function NextHighestDnInvert(APos: Integer; AY: Double): Double;
		function NextLowestDnInvert(APos: Integer; AY: Double): Double;
		procedure ProcessAdjacents;
		{ Manipulations on SWMM Objects in Path }
		procedure Straighten;
		procedure Reslope(NewSlope: Double; Anchor: TModelObj; KeepOffsets: Boolean);
		procedure ShiftVert(Offset: Double);
		{ Painting }
		procedure Draw(ACanvas: TCanvas;
			DrawStyle: TDrawStyleSet; IsPrinting: Boolean = False); override;
		procedure DrawTracker(ACanvas: TCanvas;
			TrackerState: TTrackerState); override;
		procedure DrawMiniature(ACanvas: TCanvas; ARect: TRect);
		function LengthFromStart(AObj: TModelObj): Double;
		function EnumAttachedProfiles: TObjectList;
		function PosOfObj(AObj: TModelObj): Integer;
		function LengthBetweenObjects(UpObj, DnObj: TModelObj): Double; overload;
		function LengthBetweenObjects(UpPos, DnPos: Integer): Double; overload;
	published
		{ Properties }
		property Objects: TObjectList read FObjects write SetObjects;
		property InAdjacents: TObjectList read FInAdjacents;
		property OutAdjacents: TObjectList read FOutAdjacents;
		property ExpandedLinks: TObjectList read FExpandedLinks
			write SetExpandedLinks;
		property Color: TColor read FColor write SetColor;
		property Width: Integer read FWidth write SetWidth;
		property PathDescription: String read GetPathDescription;
	end;

	TGridStyle = (gsLines, gsDotted, gsAlternate);
	TGridSnap = (gsNone, gsGrid, gsGuides, gsObjects);
	TSnapSet = set of TGridSnap;
	TGrid = class(TPersistent)
	private
		FGridStyle: TGridStyle;
		FMin: TDPoint;
		FSpacing: TDPoint;
		FVisible: Boolean;
		FZoom: TDPoint;
		FColor: TColor;
		FSnap: TSnapSet;
		FMinGridSize: Integer;
		FPrintGrid: Boolean;
		FIsotropic: Boolean;
		procedure SetIsotropic(Value: Boolean);
		procedure SetGridStyle(Value: TGridStyle);
		procedure SetMin(Value: TDPoint);
		procedure SetSpacing(Value: TDPoint);
		procedure SetZoom(Value: TDPoint);
		procedure SetColor(Value: TColor);
		procedure SetSnap(Value: TSnapSet);
		procedure SetMinGridSize(Value: Integer);
		function GetMax: TDPoint; virtual;
		procedure SetVisible(Value: Boolean);
		function GetWidth: Double;
		function GetHeight: Double;
		function GetBoundsRect: TDRectangle;
	public
		constructor Create;
		destructor Destroy; override;
		procedure Draw(ACanvas: TCanvas; Clipper: TDRectangle); virtual;
		// Conversion Functions
		function GridToClient(APoint: TDPoint): TPoint; overload; virtual;
		function GridToClient(ARect: TDRectangle): TRect; overload; virtual;
		function GridToClientX(XCoord: Double): Integer; virtual;
		function GridToClientY(YCoord: Double): Integer; virtual;
		function ClientUnits(Len: Double): Integer; overload; virtual;
		function ClientUnits(LenX, LenY: Double): Integer; overload; virtual;
		function ClientToGrid(APoint: TPoint): TDPoint; overload; virtual;
		function ClientToGrid(ARect: TRect): TDRectangle; overload; virtual;
		function ClientToGridX(XCoord: Integer): Double; virtual;
		function ClientToGridY(YCoord: Integer): Double; virtual;
		function GridUnits(Len: Integer): Double; overload; virtual;
		function GridUnits(LenX, LenY: Integer): Double; overload; virtual;
		function GridAspect: Double; virtual;
		procedure UpdateGrid;
		function CalcRulerInterval(Interval: Double): Double;
		function HorizClientUnits(Len: Double): Integer;
		function HorizGridUnits(Len: Integer): Double;
		function VertClientUnits(Len: Double): Integer;
		function VertGridUnits(Len: Integer): Double;
	published
		property GridStyle: TGridStyle read FGridStyle write SetGridStyle
			default gsLines;
		property Min: TDPoint read FMin write SetMin;
		property Spacing: TDPoint read FSpacing write SetSpacing;
		property Zoom: TDPoint read FZoom write SetZoom;
		property Color: TColor read FColor write SetColor default clInactiveCaption;
		property Snap: TSnapset read FSnap write SetSnap;
		property MinGridSize: Integer read FMinGridSize write SetMinGridSize;
		property Max: TDPoint read GetMax;
		property PrintGrid: Boolean read FPrintGrid write FPrintGrid default False;
		property Visible: Boolean read FVisible write SetVisible default True;
		property Isotropic: Boolean read FIsotropic write SetIsotropic default True;
		property Width: Double read GetWidth;
		property Height: Double read GetHeight;
		property BoundsRect: TDRectangle read GetBoundsRect;
	end;

	TGraphicalViewTool = class
	private
		FOriginPt: TPoint;
		FDragPt: TPoint;
		FDragging: Boolean;
		FButton: TMouseButton;
		FShift: TShiftState;
		FDrawDragRect: Boolean;
		FRight: Boolean;
		FHitObj: TDocObj;
	protected
		function GetToolName: String; virtual; abstract;
	public
		constructor Create;
		procedure MouseDown(Sender: TObject; Button: TMouseButton;
			Shift: TShiftState; X, Y: Integer); virtual;
		procedure MouseMove(Sender: TObject; Shift: TShiftState; X, Y: Integer);
			virtual;
		procedure MouseUp(Sender: TObject; Button: TMouseButton;
			Shift: TShiftState; X, Y: Integer); virtual;
		procedure DblClick; virtual;
		procedure ConstructContextMenu(Sender: TObject; HitObj: TDocObj); virtual;
		procedure ContextMenuClick(Sender: TObject); virtual;
		property OriginPt: TPoint read FOriginPt write FOriginPt;
		property DragPt: TPoint read FDragPt write FDragPt;
		property Dragging: Boolean read FDragging write FDragging;
		property Button: TMouseButton read FButton;
		property Shift: TShiftState read FShift;
		property DrawDragRect: Boolean read FDrawDragRect write FDrawDragRect;
		property Right: Boolean read FRight write FRight;
		property HitObj: TDocObj read FHitObj write FHitObj;
		property ToolName: String read GetToolName;
	end;

	//////////////////////////////////////////////////////////////////////////////
	// TPanTool
	//   Tool for real-time panning of a graphical view
	TPanTool = class(TGraphicalViewTool)
	protected
		function GetToolName: String; override;
	public
		DoubleClicked: Boolean;
		OldMinPt: TDPoint;
		GridWasVisible: Boolean;
		WasSimple: Boolean;
		procedure MouseDown(Sender: TObject; Button: TMouseButton;
			Shift: TShiftState; X, Y: Integer); override;
		procedure MouseMove(Sender: TObject; Shift: TShiftState; X, Y: Integer);
			override;
		procedure MouseUp(Sender: TObject; Button: TMouseButton;
			Shift: TShiftState; X, Y: Integer); override;
		procedure DblClick; override;
	end;

	//////////////////////////////////////////////////////////////////////////////
	// TZoomTool
	//   Tool for zooming back and forth in a graphical view
	TZoomTool = class(TGraphicalViewTool)
	protected
		function GetToolName: String; override;
	public
		DoubleClicked: Boolean;
		ZoomOut: Boolean;
		procedure MouseDown(Sender: TObject; Button: TMouseButton;
			Shift: TShiftState; X, Y: Integer); override;
		procedure MouseMove(Sender: TObject; Shift: TShiftState; X, Y: Integer);
			override;
		procedure MouseUp(Sender: TObject; Button: TMouseButton;
			Shift: TShiftState; X, Y: Integer); override;
		procedure DblClick; override;
	end;

	TProfileSelectMode = (smNone, smResize, smMove, smMarqueeSelect);
	TProfileSelectTool = class(TGraphicalViewTool)
	protected
		function GetToolName: String; override;
	public
		DragHandle: Integer;
		SelectMode: TProfileSelectMode;
		AnchorPos: TObjectList;      // OwnerList
		Connected: TObjectList;      // RefList
		UpdateLinks: TObjectList;    // RefList
		MovedObjectsRect: TDRectangle;
		NotMoved: Boolean;
		NotMovedOnDelete: Boolean;
		NotPreviouslySelected: Boolean;
		DoubleClicked: Boolean;
		ObjToDelete: TObject;
		FlyOverObj: TObject;
		procedure MouseDown(Sender: TObject; Button: TMouseButton;
			Shift: TShiftState; X, Y: Integer); override;
		procedure MouseMove(Sender: TObject; Shift: TShiftState; X, Y: Integer);
			override;
		procedure MouseUp(Sender: TObject; Button: TMouseButton;
			Shift: TShiftState; X, Y: Integer); override;
		procedure DblClick; override;
		procedure ClearAnchors;
		procedure ConstructContextMenu(Sender: TObject; HitObj: TDocObj); override;
		procedure ContextMenuClick(Sender: TObject); override;
		procedure PaintProfileNodeInfo(ACanvas: TCanvas;
			ANode: Integer; WhichHandle: Integer);
		procedure PaintProfileConduitInfo(ACanvas: TCanvas;
			AConduit: TConduit; WhichHandle: Integer);
		constructor Create;
		destructor Destroy; override;
	end;

	TfrmMain = class(TForm)
		dxBarManager1: TdxBarManager;
		mnuFile: TdxBarSubItem;
		RzPanel1: TRzPanel;
		actActions: TActionList;
		mnuStatusStatus: TdxBarStatic;
		mnuStatusPosition: TdxBarStatic;
		mnuStatusSize: TdxBarStatic;
		imglstBar: TImageList;
		mnuToolboxSelect: TdxBarButton;
		mnuToolboxZoomIn: TdxBarButton;
		mnuToolboxPan: TdxBarButton;
		actToolboxSelect: TAction;
		actToolboxZoomIn: TAction;
		actToolboxPan: TAction;
		mnuToolboxZoomOut: TdxBarButton;
		actToolboxZoomOut: TAction;
		adoNetwork: TADOTable;
    adoLinksConnection: TADOConnection;
		srcNetwork: TDataSource;
    actViewTraceThematic: TAction;
    mnuViewTraceThematic: TdxBarButton;
		mnuView: TdxBarSubItem;
		actNetworkTracePath: TAction;
    adoDownstreams: TADOTable;
    adoUpstreams: TADOTable;
		dsrcDownstreams: TDataSource;
    dsrcUpstreams: TDataSource;
    dxStyle: TdxEditStyleController;
    mnuStatusCancel: TdxBarButton;
    pnlProperties: TRzSizePanel;
    RzPanel4: TRzPanel;
    pnlMap: TRzSizePanel;
		mapNetwork: TMap;
    RzPanel2: TPanel;
    rulVert: TRuler;
		RzPanel3: TRzPanel;
    Bevel1: TBevel;
    rulHoriz: TRuler;
    edtInplace: TdxEdit;
    lblDatabase: TRzLabel;
    dlgOpen: TOpenDialog;
    actNetworkSaveProfile: TAction;
    mnuNetworkSaveProfile: TdxBarLargeButton;
    mnuNetworkTracePath: TdxBarLargeButton;
    mnuFileOpenDatabase: TdxBarLargeButton;
    actFileOpenDatabase: TAction;
    actFileExit: TFileExit;
    mnuFileExit: TdxBarButton;
    dxBarDockControl1: TdxBarDockControl;
    mnuStatusProgress: TdxBarProgressItem;
    mnuNetwork: TdxBarSubItem;
    actNetworkInterpolate: TAction;
    mnuNetworkInterpolate: TdxBarLargeButton;
    actNetworkRevertPath: TAction;
		actViewExtents: TAction;
    mnuViewExtents: TdxBarLargeButton;
    pnlPropertiesHolder: TRzPanel;
    RzLabel1: TRzLabel;
    dxBarDockControl2: TdxBarDockControl;
    mnuNetworkFuturePipes: TdxBarLargeButton;
    actNetworkFuturePipes: TAction;
    dxBarDockControl3: TdxBarDockControl;
    mnuViewAutoExtents: TdxBarLargeButton;
    actViewAutoExtents: TAction;
    mnuNetworkRevertPath: TdxBarLargeButton;
		actViewAutoLabels: TAction;
    mnuViewAutoLabels: TdxBarLargeButton;
		actNetworkFixGroundToRimDepth: TAction;
		mnuViewLineDirection: TdxBarLargeButton;
		actViewLineDirections: TAction;
		mnuViewLayers: TdxBarLargeButton;
    actViewLayers: TAction;
    Panel1: TPanel;
    Label1: TLabel;
		edtComment: TdxEdit;
    adoLinkChanges: TADOTable;
    adoLinksCommand: TADOCommand;
    pnlPropertiesData: TRzSizePanel;
    lblPlanView: TRzLabel;
    RzLabel14: TRzLabel;
    adoTraced: TADOTable;
    adoLinkDetailChanges: TADOTable;
    srcDetailChanges: TDataSource;
    DrawBox: TTransPanel;
    mnuNetworkFixInvertsToDepths: TdxBarLargeButton;
    actNetworkFixInvertsToDepths: TAction;
		actNetworkFixGroundsToDepths: TAction;
    mnuFixGroundsToDepths: TdxBarLargeButton;
    dxBarDockControl4: TdxBarDockControl;
    CSGlobalObject1: TCSGlobalObject;
    mnuNetworkHighlightPathOnNetwork: TdxBarLargeButton;
    actNetworkHighlightPathOnNetwork: TAction;
    mnuNetworkShiftLeft: TdxBarLargeButton;
    mnuNetworkShiftRight: TdxBarLargeButton;
    actNetworkShiftLeft: TAction;
    actNetworkShiftRight: TAction;
    mnuFileDeactivateDatabase: TdxBarLargeButton;
    actNetworkDeactivateDatabase: TAction;
    PrintJob1: TPrintJob;
    PreviewWindow1: TPreviewWindow;
    actFilePrint: TAction;
    btnFilePrint: TdxBarButton;
    mnuFileMRUFiles: TdxBarMRUListItem;
    actFileUpdateMasterDatabase: TAction;
    adoChangesCommand: TADOCommand;
    adoChangesConnection: TADOConnection;
    adoDnNodes: TADOTable;
    adoNodesConnection: TADOConnection;
    adoUpNodes: TADOTable;
    adoNodeChanges: TADOTable;
    adoDSNodeDetailChanges: TADOTable;
    srcDSNodeDetailChanges: TDataSource;
    adoUSNodeDetailChanges: TADOTable;
    srcUSNodeDetailChanges: TDataSource;
    actToolsOptions: TAction;
    mnuTools: TdxBarSubItem;
    mnuToolsOptions: TdxBarButton;
    adoNodes: TADOTable;
    adoNetworkMAPINFO_ID: TAutoIncField;
    adoNetworkLinkID: TIntegerField;
    adoNetworkMLinkID: TIntegerField;
    adoNetworkShape: TWideStringField;
    adoNetworkLinkType: TWideStringField;
    adoNetworkUSNode: TWideStringField;
    adoNetworkCompKey: TIntegerField;
    adoNetworkDSNode: TWideStringField;
    adoNetworkLength: TFloatField;
    adoNetworkDiamWidth: TFloatField;
    adoNetworkHeight: TFloatField;
    adoNetworkMaterial: TWideStringField;
    adoNetworkupsdpth: TFloatField;
    adoNetworkUSIE: TFloatField;
    adoNetworkDSIE: TFloatField;
    adoNetworkAsBuilt: TWideStringField;
    adoNetworkInstdate: TDateTimeField;
    adoNetworkFromX: TFloatField;
    adoNetworkFromY: TFloatField;
    adoNetworkToX: TFloatField;
    adoNetworkToY: TFloatField;
    adoNetworkRoughness: TFloatField;
    adoNetworkLinkReach: TWideStringField;
    adoNetworkReachElement: TIntegerField;
    adoDownstreamsMAPINFO_ID: TAutoIncField;
    adoDownstreamsLinkID: TIntegerField;
    adoDownstreamsMLinkID: TIntegerField;
    adoDownstreamsShape: TWideStringField;
    adoDownstreamsLinkType: TWideStringField;
    adoDownstreamsUSNode: TWideStringField;
    adoDownstreamsCompKey: TIntegerField;
    adoDownstreamsDSNode: TWideStringField;
    adoDownstreamsLength: TFloatField;
    adoDownstreamsDiamWidth: TFloatField;
    adoDownstreamsHeight: TFloatField;
    adoDownstreamsMaterial: TWideStringField;
    adoDownstreamsupsdpth: TFloatField;
    adoDownstreamsUSIE: TFloatField;
    adoDownstreamsDSIE: TFloatField;
    adoDownstreamsAsBuilt: TWideStringField;
    adoDownstreamsInstdate: TDateTimeField;
    adoDownstreamsFromX: TFloatField;
    adoDownstreamsFromY: TFloatField;
    adoDownstreamsToX: TFloatField;
    adoDownstreamsToY: TFloatField;
    adoDownstreamsRoughness: TFloatField;
    adoDownstreamsLinkReach: TWideStringField;
    adoDownstreamsReachElement: TIntegerField;
    adoUpstreamsMAPINFO_ID: TAutoIncField;
    adoUpstreamsLinkID: TIntegerField;
    adoUpstreamsMLinkID: TIntegerField;
    adoUpstreamsShape: TWideStringField;
    adoUpstreamsLinkType: TWideStringField;
    adoUpstreamsUSNode: TWideStringField;
    adoUpstreamsCompKey: TIntegerField;
    adoUpstreamsDSNode: TWideStringField;
    adoUpstreamsLength: TFloatField;
    adoUpstreamsDiamWidth: TFloatField;
    adoUpstreamsHeight: TFloatField;
    adoUpstreamsMaterial: TWideStringField;
    adoUpstreamsupsdpth: TFloatField;
    adoUpstreamsUSIE: TFloatField;
    adoUpstreamsDSIE: TFloatField;
    adoUpstreamsAsBuilt: TWideStringField;
    adoUpstreamsInstdate: TDateTimeField;
    adoUpstreamsFromX: TFloatField;
    adoUpstreamsFromY: TFloatField;
    adoUpstreamsToX: TFloatField;
    adoUpstreamsToY: TFloatField;
    adoUpstreamsRoughness: TFloatField;
    adoUpstreamsLinkReach: TWideStringField;
    adoUpstreamsReachElement: TIntegerField;
    adoNodesMAPINFO_ID: TAutoIncField;
    adoNodesNode: TWideStringField;
    adoNodesXCoord: TFloatField;
    adoNodesYCoord: TFloatField;
    adoNodesNodeType: TWideStringField;
    adoNodesGrndElev: TFloatField;
    adoNodesHasSpecNode: TWideStringField;
    adoNodesHasSpecLink: TWideStringField;
    adoDnNodesMAPINFO_ID: TAutoIncField;
    adoDnNodesNode: TWideStringField;
    adoDnNodesXCoord: TFloatField;
    adoDnNodesYCoord: TFloatField;
    adoDnNodesNodeType: TWideStringField;
    adoDnNodesGrndElev: TFloatField;
    adoDnNodesHasSpecNode: TWideStringField;
    adoDnNodesHasSpecLink: TWideStringField;
    adoUpNodesMAPINFO_ID: TAutoIncField;
    adoUpNodesNode: TWideStringField;
    adoUpNodesXCoord: TFloatField;
    adoUpNodesYCoord: TFloatField;
    adoUpNodesNodeType: TWideStringField;
    adoUpNodesGrndElev: TFloatField;
    adoUpNodesHasSpecNode: TWideStringField;
    adoUpNodesHasSpecLink: TWideStringField;
    pnlChanges: TRzSizePanel;
    RzLabel13: TRzLabel;
    RzLabel19: TRzLabel;
    pgcChanges: TdxPageControl;
    tabLink: TdxTabSheet;
    grdChanges: TdxDBGrid;
    tabUSNode: TdxTabSheet;
    adoLookupTablesConnection: TADOConnection;
    adoDataSources: TADOTable;
    srcDataSources: TDataSource;
    dxBarDataSource: TdxBarPopupMenu;
    mnuDSDimension: TdxBarLookupCombo;
    mnuDSShape: TdxBarLookupCombo;
    mnuDSMaterial: TdxBarLookupCombo;
    mnuDSIEUp: TdxBarLookupCombo;
    mnuDSIEDown: TdxBarLookupCombo;
    mnuDSRim: TdxBarLookupCombo;
    mnuDSLength: TdxBarLookupCombo;
    mnuDSSynth: TdxBarLookupCombo;
    mnuFileUpdateMasterDatabase: TdxBarLargeButton;
    adoLinkChangesLinkID: TIntegerField;
    adoLinkChangesFieldName: TWideStringField;
    adoLinkChangesChanged: TBooleanField;
    adoLinkChangesOldValue: TWideStringField;
    adoLinkChangesNewValue: TWideStringField;
    adoLinkChangesComment: TWideStringField;
    adoLinkChangesUserName: TWideStringField;
    adoLinkChangesUserTime: TDateTimeField;
    actNetworkCopyGroundSlope: TAction;
    actNetworkMinimumPipeSlope: TAction;
    actNetworkCopyAdjacentSlope: TAction;
    actNetworkFind: TAction;
    dxBarLargeButton1: TdxBarLargeButton;
    mnuNetworkMinimumPipeSlop: TdxBarLargeButton;
    mnuNetworkCopyAdjacentSlope: TdxBarLargeButton;
    mnuNetworkFind: TdxBarLargeButton;
    dxBarAnchor: TdxBarPopupMenu;
    btnAnchorUpstream: TdxBarButton;
    btnAnchorDownstream: TdxBarButton;
    mnuAnchorRoughness: TdxBarSpinEdit;
    mnuAnchorVelocity: TdxBarSpinEdit;
    adoNetworkDataQual: TWideStringField;
    adoDownstreamsDataQual: TWideStringField;
    adoUpstreamsDataQual: TWideStringField;
    adoMasterNodesConnection: TADOConnection;
    adoMasterNodes: TADOTable;
    adoNodeChangesNode: TWideStringField;
    adoNodeChangesFieldName: TWideStringField;
    adoNodeChangesChanged: TBooleanField;
    adoNodeChangesOldValue: TWideStringField;
    adoNodeChangesNewValue: TWideStringField;
    adoNodeChangesComment: TWideStringField;
    adoNodeChangesUserName: TWideStringField;
    adoNodeChangesUserTime: TDateTimeField;
    tabDSNode: TdxTabSheet;
    adoMasterLinks: TADOTable;
    adoMasterLinksConnection: TADOConnection;
    adoMasterLinksMAPINFO_ID: TAutoIncField;
    adoMasterLinksMLinkID: TIntegerField;
    adoMasterLinksShape: TWideStringField;
    adoMasterLinksLinkType: TWideStringField;
    adoMasterLinksUSNode: TWideStringField;
    adoMasterLinksCompKey: TIntegerField;
    adoMasterLinksDSNode: TWideStringField;
    adoMasterLinksLength: TFloatField;
    adoMasterLinksDiamWidth: TFloatField;
    adoMasterLinksHeight: TFloatField;
    adoMasterLinksMaterial: TWideStringField;
    adoMasterLinksupsdpth: TFloatField;
    adoMasterLinksUSIE: TFloatField;
    adoMasterLinksDSIE: TFloatField;
    adoMasterLinksAsBuilt: TWideStringField;
    adoMasterLinksInstdate: TDateTimeField;
    adoMasterLinksFromX: TFloatField;
    adoMasterLinksFromY: TFloatField;
    adoMasterLinksToX: TFloatField;
    adoMasterLinksToY: TFloatField;
    adoMasterLinksRoughness: TFloatField;
    adoMasterLinksTimeFrame: TWideStringField;
    adoMasterLinksDataFlagSynth: TIntegerField;
    adoMasterLinksDataQual: TWideStringField;
    adoMasterNodesMAPINFO_ID: TAutoIncField;
    adoMasterNodesNode: TWideStringField;
    adoMasterNodesXCoord: TFloatField;
    adoMasterNodesYCoord: TFloatField;
    adoMasterNodesNodeType: TWideStringField;
    adoMasterNodesGrndElev: TFloatField;
    adoMasterNodesHasSpecNode: TWideStringField;
    adoMasterNodesHasSpecLink: TWideStringField;
    adoMasterChangesConnection: TADOConnection;
    adoMasterChangesCommand: TADOCommand;
    adoMasterLinkChanges: TADOTable;
    adoMasterNodeChanges: TADOTable;
    adoLinkDetailChangesLinkID: TIntegerField;
    adoLinkDetailChangesFieldName: TWideStringField;
    adoLinkDetailChangesChanged: TBooleanField;
    adoLinkDetailChangesOldValue: TWideStringField;
    adoLinkDetailChangesNewValue: TWideStringField;
    adoLinkDetailChangesComment: TWideStringField;
    adoLinkDetailChangesUserName: TWideStringField;
    adoLinkDetailChangesUserTime: TDateTimeField;
    adoDSNodeDetailChangesNode: TWideStringField;
    adoDSNodeDetailChangesFieldName: TWideStringField;
    adoDSNodeDetailChangesChanged: TBooleanField;
    adoDSNodeDetailChangesOldValue: TWideStringField;
    adoDSNodeDetailChangesNewValue: TWideStringField;
    adoDSNodeDetailChangesComment: TWideStringField;
    adoDSNodeDetailChangesUserName: TWideStringField;
    adoDSNodeDetailChangesUserTime: TDateTimeField;
    adoUSNodeDetailChangesNode: TWideStringField;
    adoUSNodeDetailChangesFieldName: TWideStringField;
    adoUSNodeDetailChangesChanged: TBooleanField;
    adoUSNodeDetailChangesOldValue: TWideStringField;
    adoUSNodeDetailChangesNewValue: TWideStringField;
    adoUSNodeDetailChangesComment: TWideStringField;
    adoUSNodeDetailChangesUserName: TWideStringField;
    adoUSNodeDetailChangesUserTime: TDateTimeField;
    grdChangesLinkID: TdxDBGridMaskColumn;
    grdChangesFieldName: TdxDBGridColumn;
    grdChangesOldValue: TdxDBGridColumn;
    grdChangesNewValue: TdxDBGridColumn;
    grdChangesComment: TdxDBGridColumn;
    grdChangesUserName: TdxDBGridColumn;
    grdChangesUserTime: TdxDBGridDateColumn;
    grdNodeChanges: TdxDBGrid;
    grdNodeChangesNode: TdxDBGridColumn;
    grdNodeChangesFieldName: TdxDBGridColumn;
    grdNodeChangesOldValue: TdxDBGridColumn;
    grdNodeChangesNewValue: TdxDBGridColumn;
    grdNodeChangesComment: TdxDBGridColumn;
    grdNodeChangesUserName: TdxDBGridColumn;
    grdNodeChangesUserTime: TdxDBGridDateColumn;
    grdChangesChanged: TdxDBGridCheckColumn;
    grdNodeChangesChanged: TdxDBGridCheckColumn;
    mnuViewChangeLog: TdxBarButton;
    actViewChangeLog: TAction;
    btnCloseChangeLog: TRzBitBtn;
    adoMasterLinksPipeFlowType: TWideStringField;
    adoNetworkPipeFlowType: TWideStringField;
    adoNetworkIsSpecLink: TBooleanField;
    adoDownstreamsPipeFlowType: TWideStringField;
    adoDownstreamsIsSpecLink: TBooleanField;
    adoUpstreamsPipeFlowType: TWideStringField;
    adoUpstreamsIsSpecLink: TBooleanField;
    actViewNetworkThematic: TAction;
    mnuViewNetworkThematic: TdxBarButton;
    actViewResetToolbars: TAction;
    mnuViewResetToolbars: TdxBarButton;
    adoNodesCommand: TADOCommand;
    ScrollBox1: TScrollBox;
    RzLabel2: TRzLabel;
    RzLabel3: TRzLabel;
    RzLabel4: TRzLabel;
    RzLabel5: TRzLabel;
    RzLabel6: TRzLabel;
    RzLabel7: TRzLabel;
    RzLabel8: TRzLabel;
    RzLabel9: TRzLabel;
    RzLabel10: TRzLabel;
    RzLabel11: TRzLabel;
    RzLabel12: TRzLabel;
    RzLabel15: TRzLabel;
    RzLabel16: TRzLabel;
    RzLabel17: TRzLabel;
    RzLabel18: TRzLabel;
    RzLabel20: TRzLabel;
    RzLabel21: TRzLabel;
    RzLabel22: TRzLabel;
    RzLabel24: TRzLabel;
    RzLabel25: TRzLabel;
    RzLabel26: TRzLabel;
    RzLabel27: TRzLabel;
    RzLabel28: TRzLabel;
    RzLabel29: TRzLabel;
    RzLabel30: TRzLabel;
    RzLabel31: TRzLabel;
    RzLabel32: TRzLabel;
    RzLabel23: TRzLabel;
    RzBorder1: TRzBorder;
    RzLabel33: TRzLabel;
    RzLabel34: TRzLabel;
    edtUpGround: TdxSpinEdit;
    edtDnGround: TdxSpinEdit;
    edtUpInvert: TdxSpinEdit;
    edtDnInvert: TdxSpinEdit;
    edtCompKey: TdxSpinEdit;
    edtUpDepth: TdxSpinEdit;
    edtDnDepth: TdxSpinEdit;
    edtUpNode: TdxEdit;
    edtDnNode: TdxEdit;
    edtLength: TdxSpinEdit;
    edtVertDim: TdxSpinEdit;
    edtLinkType: TdxEdit;
    edtUpType: TdxEdit;
    edtDnType: TdxEdit;
    edtHorizDim: TdxSpinEdit;
    edtShape: TdxEdit;
    edtPipetype: TdxEdit;
    edtLinkID: TdxSpinEdit;
    btnDataSource: TRzMenuButton;
    edtFlowType: TdxEdit;
    chkSpecLink: TdxCheckEdit;
    mnuViewShowToolbarCaptions: TdxBarButton;
    actViewShowToolbarCaptions: TAction;
    edtAsBuilt: TdxButtonEdit;
    dlgBrowser: TRzSelectFolderDialog;
    RzLabel35: TRzLabel;
    RzLabel36: TRzLabel;
		procedure FormCreate(Sender: TObject);
		procedure actToolboxSelectExecute(Sender: TObject);
		procedure actToolboxZoomInExecute(Sender: TObject);
		procedure actToolboxPanExecute(Sender: TObject);
		procedure mapNetworkMouseMove(Sender: TObject; Shift: TShiftState; X,
			Y: Integer);
		procedure mapNetworkSelectionChanged(Sender: TObject);
		procedure actToolboxZoomOutExecute(Sender: TObject);
		procedure actViewTraceThematicExecute(Sender: TObject);
		procedure pnlMapHotSpotClick(Sender: TObject);
		procedure actNetworkTracePathExecute(Sender: TObject);
		procedure RzSizePanel2HotSpotClick(Sender: TObject);
		procedure DrawBoxPaint(Sender: TObject);
		procedure FormDestroy(Sender: TObject);
		procedure DrawBoxMouseDown(Sender: TObject; Button: TMouseButton;
			Shift: TShiftState; X, Y: Integer);
		procedure DrawBoxMouseMove(Sender: TObject; Shift: TShiftState; X,
			Y: Integer);
		procedure DrawBoxMouseUp(Sender: TObject; Button: TMouseButton;
			Shift: TShiftState; X, Y: Integer);
		procedure DrawBoxDblClick(Sender: TObject);
		procedure mnuStatusCancelClick(Sender: TObject);
		procedure actNetworkSaveProfileExecute(Sender: TObject);
		procedure pnlPropertiesHotSpotClick(Sender: TObject);
		procedure actNetworkSaveProfileUpdate(Sender: TObject);
		procedure actFileOpenDatabaseExecute(Sender: TObject);
		procedure actViewExtentsExecute(Sender: TObject);
		procedure actNetworkFuturePipesExecute(Sender: TObject);
		procedure actNetworkTracePathUpdate(Sender: TObject);
		procedure actViewAutoExtentsExecute(Sender: TObject);
		procedure actViewAutoExtentsUpdate(Sender: TObject);
		procedure actViewExtentsUpdate(Sender: TObject);
		procedure pnlMapResize(Sender: TObject);
		procedure actNetworkInterpolateExecute(Sender: TObject);
		procedure actNetworkInterpolateUpdate(Sender: TObject);
		procedure actNetworkRevertPathExecute(Sender: TObject);
		procedure actNetworkRevertPathUpdate(Sender: TObject);
		procedure FormResize(Sender: TObject);
		procedure actViewAutoLabelsExecute(Sender: TObject);
		procedure actViewAutoLabelsUpdate(Sender: TObject);
		procedure actNetworkFixGroundToRimDepthExecute(Sender: TObject);
		procedure actNetworkFixGroundToRimDepthUpdate(Sender: TObject);
		procedure actViewLayersExecute(Sender: TObject);
		procedure actViewLineDirectionsExecute(Sender: TObject);
		procedure actViewLineDirectionsUpdate(Sender: TObject);
		procedure actViewLayersUpdate(Sender: TObject);
		procedure adoNetworkMoveComplete(DataSet: TCustomADODataSet;
			const Reason: TEventReason; const Error: Error;
			var EventStatus: TEventStatus);
		procedure edtUpGroundChange(Sender: TObject);
    procedure actNetworkFixInvertsToDepthsExecute(Sender: TObject);
    procedure actNetworkFixInvertsToDepthsUpdate(Sender: TObject);
    procedure DrawBoxKeyUp(Sender: TObject; var Key: Word;
      Shift: TShiftState);
    procedure adoNetworkPostError(DataSet: TDataSet; E: EDatabaseError;
      var Action: TDataAction);
    procedure actNetworkFixGroundsToDepthsExecute(Sender: TObject);
    procedure actNetworkFixGroundsToDepthsUpdate(Sender: TObject);
    procedure DrawBoxMouseLeave(Sender: TObject);
    procedure actNetworkHighlightPathOnNetworkExecute(Sender: TObject);
    procedure actNetworkHighlightPathOnNetworkUpdate(Sender: TObject);
    procedure actNetworkShiftRightExecute(Sender: TObject);
    procedure actNetworkShiftLeftExecute(Sender: TObject);
    procedure actNetworkDeactivateDatabaseUpdate(Sender: TObject);
		procedure actNetworkDeactivateDatabaseExecute(Sender: TObject);
    procedure actFilePrintExecute(Sender: TObject);
    procedure PrintJob1Draw(Sender: TObject; TheCanvas: TCanvas;
      PageIndex: Integer; Rect: TRect; Area: TDrawArea;
      Target: TDrawTarget);
    procedure actFilePrintUpdate(Sender: TObject);
    procedure mnuFileMRUFilesClick(Sender: TObject);
    procedure pnlChangesHotSpotClick(Sender: TObject);
    procedure pgcChangesChange(Sender: TObject);
    procedure btnDataSourceClick(Sender: TObject);
    procedure mnuDSDimensionChange(Sender: TObject);
    procedure mnuDSShapeChange(Sender: TObject);
    procedure mnuDSMaterialChange(Sender: TObject);
    procedure mnuDSIEUpChange(Sender: TObject);
    procedure mnuDSIEDownChange(Sender: TObject);
    procedure mnuDSRimChange(Sender: TObject);
    procedure mnuDSLengthChange(Sender: TObject);
    procedure mnuDSSynthChange(Sender: TObject);
    procedure dxBarDataSourcePopup(Sender: TObject);
    procedure actFileUpdateMasterDatabaseExecute(Sender: TObject);
    procedure FormCloseQuery(Sender: TObject; var CanClose: Boolean);
    procedure actNetworkFindExecute(Sender: TObject);
    procedure actNetworkFindUpdate(Sender: TObject);
    procedure actNetworkCopyGroundSlopeExecute(Sender: TObject);
    procedure actNetworkMinimumPipeSlopeExecute(Sender: TObject);
    procedure actNetworkCopyAdjacentSlopeExecute(Sender: TObject);
    procedure actNetworkCopyGroundSlopeUpdate(Sender: TObject);
    procedure actNetworkMinimumPipeSlopeUpdate(Sender: TObject);
    procedure actNetworkCopyAdjacentSlopeUpdate(Sender: TObject);
    procedure actFileUpdateMasterDatabaseUpdate(Sender: TObject);
    procedure grdChangesExit(Sender: TObject);
    procedure actViewChangeLogExecute(Sender: TObject);
    procedure btnCloseChangeLogClick(Sender: TObject);
    procedure mnuDSDimensionCloseUp(Sender: TObject);
    procedure mnuDSDimensionEnter(Sender: TObject);
    procedure adoLinkChangesBeforeInsert(DataSet: TDataSet);
    procedure DrawBoxResize(Sender: TObject);
    procedure actViewNetworkThematicExecute(Sender: TObject);
    procedure actViewResetToolbarsExecute(Sender: TObject);
    procedure actViewShowToolbarCaptionsExecute(Sender: TObject);
    procedure edtAsBuiltButtonClick(Sender: TObject;
      AbsoluteIndex: Integer);
    procedure edtFlowTypeEnter(Sender: TObject);
	private
		{ Private declarations }
		FSelected: TObjectList;
		FSnap: TSnapSet;
		FLocked: Boolean;
		FDrawSimple: Boolean;
		FGrid: TGrid;
		FInplaceObj: TModelObj;
		AnchorID: Integer;
		TraceToID: Integer;
		Clipper: TDRectangle;
		Offscreen: TBitmap;
		FPath: TPath;
		CurrentHorizPos: Double;
		PathSelectionList: TList;
		Tracing: Boolean;
		ShiftState: TShiftState;
		FillingPropertiesData: Boolean;
		TraceItemsAvailable: Boolean;
		MapZoom: Double;
		MapCenterX: Double;
		MapCenterY: Double;
		OpenedDB: Boolean;
		AppRegistry: TRegistryIniFile;
    ModelRegistry: TIniFile;
    UserName: String;
		procedure SetPath(Value: TPath);
		procedure SetSnap(Value: TSnapSet);
		procedure SetLocked(Value: Boolean);
		procedure SetDrawSimple(Value: Boolean);
		function GetSelected(Idx: Integer): TDocObj;
		procedure WmSetcursor(var Msg: TWMSetCursor); message wm_SetCursor;
		procedure CatchKeyDown(Key: Word; Shift: TShiftState); virtual;
	public
		{ Public declarations }
		// Hint windows
		HintWin: THintWindow;
		CurrentTool: TGraphicalViewTool;
		SearchCanceled: Boolean;
		Modified: Boolean;
		NetworkTableName: String;
		PrintRect: TRect;
    ModelDirectory: String;
		procedure DisplayHint(HintText: String; XPos, YPos: Integer);
		procedure RemoveHint;
		procedure OpenTraceFile(ADirectory: String);
    procedure CloseDBs;
    procedure OpenDBs;
    procedure SetupMap(ADirectory: String);
    procedure ZoomExtents;
    function FormatDataSourceString(AString: String): String;
		function DeformatDataSourceString(AString: String): String;
		// Selection functions
		procedure Select(AObj: TDocObj; AddToSelection: Boolean = False); virtual;
		procedure SelectInRect(ARect: TRect; AddToSelection: Boolean = False);
		function IsSelected(AObj: TDocObj): Boolean;
		procedure Deselect(AObj: TDocObj);
		procedure ResetSelection;
		// General object manipulations
		procedure DeleteObj(AObj: TDocObj); virtual; abstract;
		function ObjectAt(APoint: TDPoint): TDocObj; virtual;
		function ObjectAtClient(APoint: TPoint): TDocObj; virtual;
		procedure InvalidateObj(AObj: TDocObj); overload;
		procedure InvalidateObj(AObj: TDocObj; OldRect: TRect); overload;
		procedure InvalidateViewRect(ARect: TDRectangle);
		procedure UpdateObj(AObj: TDocObj);
		// View update methods
		procedure UpdateView(Clipper: TDRectangle);
		procedure UpdateViewClient(Clipper: TRect);
		procedure FitWindow(ARect: TDRectangle); virtual;
		procedure FitWindowClient(ARect: TRect); virtual;
		procedure CenterWindow(APoint: TDPoint; Animate: Boolean = False); virtual;
		procedure CenterWindowClient(APoint: TPoint; Animate: Boolean = False); virtual;
		function GetSelectedExtents: TDRectangle; virtual;
		function GetSelectedExtentsClient: TRect; virtual;
		property Selected[Idx: Integer]: TDocObj read GetSelected;
		property SelectedList: TObjectList read FSelected;
		function CalcClientOffset: TPoint; virtual;
		procedure ViewChanged;
		function GetHorizExtents: Double;
		function GetVertLow: Double;
		function GetVertHigh: Double;
		procedure RecalcView;
	published
		property Grid: TGrid read FGrid;
		property Snap: TSnapSet read FSnap write SetSnap;
		property Locked: boolean read FLocked write SetLocked;
		property DrawSimple: Boolean read FDrawSimple write SetDrawSimple;
		property InplaceObj: TModelObj read FInplaceObj write FInplaceObj;
		property Path: TPath read FPath write SetPath;
	end;

var
	frmMain: TfrmMain;
	PanTool: TPanTool;
	ZoomTool: TZoomTool;
	ProfileSelectTool: TProfileSelectTool;

implementation

uses ComObj, ADOX_TLB, dChooseConduit, dFindLink, Hydraulics;

var
	DotSet: Integer;

{$R *.dfm}

procedure InitCursors;
begin
	Screen.Cursors[crPlanSelect] := LoadCursor(HInstance, 'SELECT');
	Screen.Cursors[crShapeEdit] := LoadCursor(HInstance, 'SHAPEEDIT');
	Screen.Cursors[crPlanModel] := LoadCursor(HInstance, 'MODEL');
	Screen.Cursors[crZoom] := LoadCursor(HInstance, 'ZOOM');
	Screen.Cursors[crPan] := LoadCursor(HInstance, 'HAND');
	Screen.Cursors[crLine] := LoadCursor(HInstance, 'LINE');
	Screen.Cursors[crPolyline] := LoadCursor(HInstance, 'POLYLINE');
	Screen.Cursors[crRect] := LoadCursor(HInstance, 'RECT');
	Screen.Cursors[crEllipse] := LoadCursor(HInstance, 'ELLIPSE');
	Screen.Cursors[crText] := LoadCursor(HInstance, 'TEXT');
	Screen.Cursors[crCrossHair] := LoadCursor(HInstance, 'CROSSHAIR');
	Screen.Cursors[crSelectNode] := LoadCursor(HInstance, 'SELECTNODE');
	Screen.Cursors[crSelectLink] := LoadCursor(HInstance, 'SELECTLINK');
	Screen.Cursors[crSelectLine] := LoadCursor(HInstance, 'SELECTLINE');
	Screen.Cursors[crSelectEllipse] := LoadCursor(HInstance, 'SELECTELLIPSE');
	Screen.Cursors[crSelectRect] := LoadCursor(HInstance, 'SELECTRECT');
	Screen.Cursors[crSelectPolyline] := LoadCursor(HInstance, 'SELECTPOLYLINE');
	Screen.Cursors[crSelectText] := LoadCursor(HInstance, 'SELECTTEXT');
	Screen.Cursors[crZoomIn] := LoadCursor(HInstance, 'ZOOMIN');
	Screen.Cursors[crZoomOut] := LoadCursor(HInstance, 'ZOOMOUT');
	Screen.Cursors[crSelectOrifice] := LoadCursor(HInstance, 'SELECTORIFICE');
	Screen.Cursors[crSelectPump] := LoadCursor(HInstance, 'SELECTPUMP');
	Screen.Cursors[crSelectPath] := LoadCursor(HInstance, 'SELECTPATH');
	Screen.Cursors[crGrElevEd] := LoadCursor(HInstance, 'GRELEVED');
	Screen.Cursors[crInElevEd] := LoadCursor(HInstance, 'INELEVED');
	Screen.Cursors[crOfElevEd] := LoadCursor(HInstance, 'OFELEVED');
	Screen.Cursors[crCondShift] := LoadCursor(HInstance, 'CONDSHIFT');
	Screen.Cursors[crCrElevEd] := LoadCursor(HInstance, 'CRELEVED');
	Screen.Cursors[crNodeShift] := LoadCursor(HInstance, 'NODESHIFT');
	Screen.Cursors[crInDn] := LoadCursor(HInstance, 'INDN');
	Screen.Cursors[crInUp] := LoadCursor(HInstance, 'INUP');
end;

procedure DottedLineDDAProc(X, Y: Integer; lpData: lParam); stdcall;
begin
	Inc(DotSet);
	if DotSet > 3 then
		DotSet := 0;
	if DotSet mod 4 = 0 then
		with TCanvas(Pointer(lpData)) do
		begin
			MoveTo(X, Y);
			LineTo(X+1, Y);
		end;
end;

procedure DottedLine(ACanvas: TCanvas; X1, Y1, X2, Y2: Integer);
begin
	DotSet := 0;
	LineDDA(X1, Y1, X2, Y2, @DottedLineDDAProc, Integer(ACanvas));
end;

procedure TfrmMain.FormCreate(Sender: TObject);
var
	LabelProperties: CMapXLabelProperties;
	Dataset: CMapXDataset;
  AUserName: String;
  AUserNameSize: Cardinal;
begin
	{$IFDEF DEBUG}
	CodeSite.Enabled := True;
	{$ELSE}
	CodeSite.Enabled := False;
	{$ENDIF}

  CodeSite.EnterMethod('frmMain.FormCreate');
	Clipper := TDRectangle.Create;
	Offscreen := TBitmap.Create;
	FGrid := TGrid.Create;
	FGrid.Isotropic := False;
	FSelected := TObjectList.Create(False);
	HintWin := THintWindow.Create(Self);
	HintWin.Color := clInfoBk;
	HintWin.Canvas.Font.Color := clInfoText;
	PanTool := TPanTool.Create;
	ZoomTool := TZoomTool.Create;
	ProfileSelectTool := TProfileSelectTool.Create;
	CurrentTool := ProfileSelectTool;
	Modified := False;
	PathSelectionList := TList.Create;
	Tracing := False;
	adoLinksConnection.Connected := False;
	adoNetwork.Active := False;
	adoUpstreams.Active := False;
	adoDownstreams.Active := False;
	OpenedDB := False;

  AUserNameSize := 64;
  SetLength(AUserName, AUserNameSize-1);
  CodeSite.SendMsg('Calling API:GetUserName');
  GetUserName(PChar(AUserName), AUserNameSize);
  CodeSite.SendMsg('Called API:GetUserName');
  SetLength(AUserName, AUserNameSize);
  UserName := String(AUserName);
  CodeSite.SendMsg('User: '+UserName);

	InitCursors;

  AppRegistry := TRegistryIniFile.Create(AppRegistryKey);
  dxBarManager1.RegistryPath := AppRegistryKey + '\' + AppRegistryToolbarKey;
	dxBarManager1.LoadFromRegistry(dxBarManager1.RegistryPath);
	// The action-execute will toggle the Checked field, so we need to toggle it back
  // temporarily
	actViewShowToolbarCaptions.Checked := not AppRegistry.ReadBool('', 'ShowToolbarCaptions', True);
	actViewShowToolbarCaptionsExecute(Self);
  CodeSite.ExitMethod('frmMain.FormCreate');
end;

procedure TfrmMain.actToolboxSelectExecute(Sender: TObject);
begin
	mapNetwork.CurrentTool := miSelectTool;
	mapNetwork.MousePointer := miArrowCursor;
	CurrentTool := ProfileSelectTool;
end;

procedure TfrmMain.actToolboxZoomInExecute(Sender: TObject);
begin
	mapNetwork.CurrentTool := miZoomInTool;
	mapNetwork.MousePointer := miZoomInCursor;
	CurrentTool := ZoomTool;
	ZoomTool.ZoomOut := False;
end;

procedure TfrmMain.actToolboxPanExecute(Sender: TObject);
begin
	mapNetwork.CurrentTool := miPanTool;
	mapNetwork.MousePointer := miPanCursor;
	CurrentTool := PanTool;
end;

procedure TfrmMain.mapNetworkMouseMove(Sender: TObject; Shift: TShiftState;
	X, Y: Integer);
var
	ScreenX, ScreenY: Single;
	MapX, MapY: Double;
begin
	ScreenX := X;
	ScreenY := Y;
	mapNetwork.ConvertCoord(ScreenX, ScreenY, MapX, MapY, miScreenToMap);
	mnuStatusPosition.Caption := Format('%.0f,%.0f', [MapX, MapY]);
//	Refresh;
end;

procedure TfrmMain.mapNetworkSelectionChanged(Sender: TObject);
var
	SelectionSet: CMapXSelection;
	Dataset: CMapXDataset;
	FeatureID: Integer;
	i: Integer;
begin
	if Tracing then Exit;
	SelectionSet := mapNetwork.Layers.Item(NetworkTableName).Selection;
	if SelectionSet.Count = 0 then
	begin
		mnuStatusStatus.Caption := 'Nothing selected';
		PathSelectionList.Clear;
		Exit;
	end;

	if SelectionSet.Count = 1 then
		PathSelectionList.Clear;

	Dataset := mapNetwork.Datasets.Item(1);
	FeatureID := SelectionSet.Item(1).FeatureID;
	mnuStatusStatus.Caption := Format('%d selected: (%s->%s)', [SelectionSet.Count, Dataset.Value[FeatureID, 'USNode'],
		Dataset.Value[FeatureID, 'DSNode']]);
	adoNetwork.Locate('LinkID', Dataset.Value[FeatureID, 'LinkID'], [loCaseInsensitive]);
	mnuStatusSize.Caption := Format('%.2f ft', [adoNetworkLength.Value]);

	for i := 1 to SelectionSet.Count do
	begin
		FeatureID := SelectionSet.Item(i).FeatureID;
		adoNetwork.Locate('LinkID', Dataset.Value[FeatureID, 'LinkID'], [loCaseInsensitive]);
		if not (PathSelectionList.IndexOf(Pointer(adoNetworkLinkID.Value)) > -1) then
			PathSelectionList.Add(Pointer(adoNetworkLinkID.Value));
	end;

//	if SelectionSet.Count = 1 then
//		AnchorID := Dataset.Value[SelectionSet.Item(1).FeatureID, 'Compkey'];

end;

procedure TfrmMain.actToolboxZoomOutExecute(Sender: TObject);
begin
	mapNetwork.CurrentTool := miZoomOutTool;
	mapNetwork.MousePointer := miZoomOutCursor;
	CurrentTool := ZoomTool;
	ZoomTool.ZoomOut := True;
end;

procedure TfrmMain.actViewTraceThematicExecute(Sender: TObject);
begin
	mapNetwork.Datasets.Item('Traced').Themes.Item(1).ThemeDlg(0,0);
end;

procedure TfrmMain.pnlMapHotSpotClick(Sender: TObject);
begin
	mapNetwork.Visible := not pnlMap.HotSpotClosed;
	lblPlanView.Visible := not pnlMap.HotSpotClosed;
end;

procedure TfrmMain.actNetworkTracePathExecute(Sender: TObject);
var
	SelectionSet: CMapXSelection;
	Dataset: CMapXDataset;

	PathSelectionQueue: TQueue;
	PathStack, TempPathStack: TStack;
	BranchSetStack: TStack;
	VisitedList: TList;

	IsSearching: Boolean;
	IsRetreating: Boolean;
	BadPath: Boolean;
	CurrentID: Integer;
	UpUnitid, DnUnitid: Integer;
	AFeature: CMapXFeature;
	Features: CMapXFeatures;
	Layer: CMapXLayer;
	i: Integer;
	NextPathObj: TConduit;
	AnAdjacent: TAdjacent;
	DlgResult: Word;
	AddingBranches: Boolean;
	DownstreamsRecNo, UpstreamsRecNo: Integer;

	procedure AddToPathStack(APointer: Pointer);
	begin
    CodeSite.SendFmtMsg('Adding to PathStack: %d', [Integer(APointer)]);
		PathStack.Push(APointer);
		VisitedList.Add(PathStack.Peek);
		adoNetwork.Locate('LinkID', Integer(PathStack.Peek), [loCaseInsensitive]);
		if BranchSetStack.Count > 0 then
			mnuStatusStatus.Caption := Format('Added to path stack: %d in path stack, %d branch sets, %d branches in current set',
				[PathStack.Count, BranchSetStack.Count, TStack(BranchSetStack.Peek).Count])
		else
			mnuStatusStatus.Caption := Format('Added to path stack: %d in path stack, no branch sets',
				[PathStack.Count]);
	end;

	procedure Retreat;
	var
		IsRetreating: Boolean;
		IsPoppingBranches: Boolean;
	begin
		if BranchSetStack.Count = 0 then
		begin
			BadPath := True;
			Exit;
		end;
		IsRetreating := True;
		CodeSite.SendFmtMsg('Initial Retreat: Looking for branch: %d', [Integer(TStack(BranchSetStack.Peek).Peek)]);
		while IsRetreating do
		begin
			if Integer(PathStack.Peek) = Integer(TStack(BranchSetStack.Peek).Peek) then
			begin
				while True do
				begin
					CodeSite.SendFmtMsg('Popping a Branch from BranchSetStack: %d',[Integer(TStack(BranchSetStack.Peek).Peek)]);
					TStack(BranchSetStack.Peek).Pop;
					CodeSite.SendFmtMsg('Popping from PathStack: %d', [Integer(PathStack.Peek)]);
					PathStack.Pop;
					if TStack(BranchSetStack.Peek).Count > 0 then // There are branches left
					begin
						CodeSite.SendFmtMsg('Looking for branch: %d', [Integer(TStack(BranchSetStack.Peek).Peek)]);
						if (VisitedList.IndexOf(TStack(BranchSetStack.Peek).Peek) > -1) then
						begin
							CodeSite.SendFmtMsg('Branch already visited, popping it from BranchSetStack: %d', [Integer(TStack(BranchSetStack.Peek).Peek)]);
							AddToPathStack(TStack(BranchSetStack.Peek).Peek);
							Continue
						end
						else
						begin // Found a branch to move forward on
							AddToPathStack(TStack(BranchSetStack.Peek).Peek);
							IsRetreating := False;
							Break;
						end;
					end
					else
					begin // All branches exhausted, continue retreating
						CodeSite.SendMsg('Popping from BranchSetStack');
						BranchSetStack.Pop;
						if BranchSetStack.Count = 0 then // no branch sets left, path is bad
						begin
							BadPath := True;
							IsRetreating := False;
							Break;
						end;
						Break;
					end;
				end;
			end
			else
			begin
				CodeSite.SendFmtMsg('Searching for %d; Popping from PathStack: %d',
					[Integer(TStack(BranchSetStack.Peek).Peek), Integer(PathStack.Peek)]);
				PathStack.Pop;
				if BranchSetStack.Count > 0 then
					mnuStatusStatus.Caption := Format('Retreating: %d in path stack, %d branch sets, %d branches in current set',
						[PathStack.Count, BranchSetStack.Count, TStack(BranchSetStack.Peek).Count])
				else
					mnuStatusStatus.Caption := Format('Retreating: %d in path stack, no branch sets',
						[PathStack.Count]);
				Application.ProcessMessages;
				if PathStack.Count = 0 then
				begin
					BadPath := True;
					Break;
				end;
			end;
		end;
	end;

begin

	// If profile was modified, prompt to save first
	if Modified then
	begin
		DlgResult := MessageDlg('Save modifications to profile?', mtConfirmation, mbYesNoCancel, 0);
		case DlgResult of
			mrYes: actNetworkSaveProfileExecute(Sender);
			mrCancel: Exit;
		end;
	end;

	Tracing := True;

//	mnuStatusCancel.Visible := ivAlways;
	mnuStatusStatus.Caption := 'Tracing path...';
	mnuStatusStatus.ImageIndex := 76;

	Layer := mapNetwork.Layers.Item(NetworkTableName);
	SelectionSet := Layer.Selection;
	Dataset := mapNetwork.Datasets.Item(1);

	PathSelectionQueue := TQueue.Create;
	PathStack := TStack.Create;
	BranchSetStack := TStack.Create;
	VisitedList := TList.Create;
  SelectedList.Clear;
  FreeAndNil(FPath);
  DrawBox.Refresh;
  pnlPropertiesData.Hide;

	try
		IsSearching := True;
		SearchCanceled := False;
		BadPath := False;
		Tracing := True;

		// Prepare Path Selection Queue
		for i := 0 to PathSelectionList.Count-1 do
			PathSelectionQueue.Push(PathSelectionList[i]);

		// Trace Routine
		AddToPathStack(PathSelectionQueue.Pop);
		while IsSearching and not SearchCanceled and not BadPath do
		begin
			Application.ProcessMessages;

			if PathSelectionQueue.Count = 0 then
			begin
				IsSearching := False;
				Break;
			end;

			if adoDownstreams.RecordCount > 1 then // There are branches (more than one conduit downstream)
			begin
				CodeSite.SendMsg('Added a BranchSet');
				BranchSetStack.Push(TStack.Create);
				adoDownstreams.First;
				while not adoDownstreams.Eof do
				begin
					CodeSite.SendMsg('Has branches, testing for visited list');
					if VisitedList.IndexOf(Pointer(adoDownstreamsLinkID.Value)) = -1 then
					begin
						TStack(BranchSetStack.Peek).Push(Pointer(adoDownstreamsLinkID.Value));
						CodeSite.SendFmtMsg('Added to BranchSetStack Branch: %d', [adoDownstreamsLinkID.Value]);
					end;

					CodeSite.SendFmtMsg('Has branches, testing for existence in path selection; Branch set count: %d',
						[TStack(BranchSetStack.Peek).Count]);
					if (TStack(BranchSetStack.Peek).Count > 0) and
						(TStack(BranchSetStack.Peek).Peek = (PathSelectionQueue.Peek)) then
					begin
						CodeSite.SendFmtMsg('Hit a Path Selection target: %d', [Integer(PathSelectionQueue.Peek)]);
						AddingBranches := False;
						PathSelectionQueue.Pop;
						// Reset the visited list to only the path stack
						TempPathStack := TStack.Create;
						VisitedList.Clear;
						while PathStack.Count > 0 do
						begin
							VisitedList.Add(PathStack.Peek);
							TempPathStack.Push(PathStack.Pop);
						end;
						while TempPathStack.Count > 0 do
						begin
							PathStack.Push(TempPathStack.Pop);
						end;
						TempPathStack.Free;
						Break;
					end;
					adoDownstreams.Next;
				end;
				if TStack(BranchSetStack.Peek).Count > 0 then
				begin
					CodeSite.SendMsg('Adding last in branch set to path stack');
					AddToPathStack(TStack(BranchSetStack.Peek).Peek);
				end
				else
				begin
					// no branches added, must retreat
					CodeSite.SendMsg('No branches added, must retreat; popping a BranchSet');
					BranchSetStack.Pop;
					Retreat;
				end;
			end
			// One conduit downstream;
			else if adoDownstreams.RecordCount = 1 then
			begin
				if VisitedList.IndexOf(Pointer(adoDownstreamsLinkID.Value)) > -1 then
				begin
					CodeSite.SendFmtMsg('Hit a visited pipe, %d', [adoDownstreamsLinkID.Value]);
					Retreat;
				end
				else if Integer(PathSelectionQueue.Peek) = adoDownstreamsLinkID.Value then
				begin
					CodeSite.SendFmtMsg('Hit a Path Selection target: %d', [Integer(PathSelectionQueue.Peek)]);
					if PathSelectionQueue.Count = 1 then
					begin
						AddToPathStack(PathSelectionQueue.Pop);
						IsSearching := False;
						Continue;
					end
					else
					begin
						AddToPathStack(Pointer(adoDownstreamsLinkID.Value));
						PathSelectionQueue.Pop;
						// Reset the visited list to only the path stack
						TempPathStack := TStack.Create;
						VisitedList.Clear;
						while PathStack.Count > 0 do
						begin
							VisitedList.Add(PathStack.Peek);
							TempPathStack.Push(PathStack.Pop);
						end;
						while TempPathStack.Count > 0 do
						begin
							PathStack.Push(TempPathStack.Pop);
						end;
						TempPathStack.Free;
						Continue;
					end;
				end
				else
				begin
					AddToPathStack(Pointer(adoDownstreamsLinkID.Value));
					Continue;
				end;
			end
			// No conduits downstream, we've reached a terminal
			else
			begin
				CodeSite.SendMsg('Hit a terminal');
				Retreat;
			end
		end;

		// Clean up branch set stack
		while BranchSetStack.Count > 0 do
		begin
			TStack(BranchSetStack.Pop).Free;
		end;

		// The path was bad
		if SearchCanceled or BadPath then
		begin
			if BadPath then
				mnuStatusStatus.Caption := 'Cannot trace between selected objects'
			else
				mnuStatusStatus.Caption := 'Trace canceled';
			mnuStatusStatus.ImageIndex := 78;
			mnuStatusCancel.Visible := ivNever;

			FreeAndNil(PathStack);
			FreeAndNil(BranchSetStack);
			FreeAndNil(VisitedList);
			FreeAndNil(PathSelectionQueue);

			SelectionSet.ClearSelection;
			for i := 0 to PathSelectionList.Count-1 do
			begin
				adoNetwork.Locate('LinkID', Integer(PathSelectionList[i]), [loCaseInsensitive]);
				SelectionSet.Add(Layer.GetFeatureByID(adoNetworkMAPINFO_ID.Value));
			end;

			Tracing := False;
			Exit;
		end;

		// Select the path
		SelectionSet.ClearSelection;

		if Assigned(FPath) then
			FreeAndNil(FPath);
		if Assigned(FSelected) then
		begin
			FreeAndNil(FSelected);
			FSelected := TObjectList.Create(False);
		end;

		// Create path object for profile
		mnuStatusStatus.Caption := 'Preparing path for profile';
		mnuStatusProgress.Visible := ivAlways;
		mnuStatusProgress.Max := PathStack.Count;
		Refresh;
		Path := TPath.Create;
		while PathStack.Count > 0 do
		begin
			mnuStatusProgress.Position := mnuStatusProgress.Max-PathStack.Count+1;
			Application.ProcessMessages;
			Path.AddUpstreamObj(Integer(PathStack.Peek));
			adoNetwork.Locate('LinkID', Integer(PathStack.Pop), [loCaseInsensitive]);
			SelectionSet.Add(Layer.GetFeatureByID(adoNetworkMAPINFO_ID.Value));
		end;

		Tracing := False;

		mnuStatusStatus.Caption := 'Preparing path for profile, finding connections to path';
		mnuStatusProgress.Max := Path.Objects.Count;
		// Create adjacents for profile
		Path.ProcessAdjacents;
		mnuStatusProgress.Visible := ivNever;

		mnuStatusStatus.Caption := 'Trace complete';
		mnuStatusStatus.ImageIndex := 78;
	//	mnuStatusCancel.Visible := ivNever;

		RecalcView;
    DrawBox.Refresh;
		Modified := False;
	finally
		PathSelectionQueue.Free;
		PathStack.Free;
		if Assigned(BranchSetStack) and (BranchSetStack.Count > 0) then
			while BranchSetStack.Count > 0 do
				TStack(BranchSetStack.Pop).Free;
		BranchSetStack.Free;
		VisitedList.Free;
	end;
end;

procedure TfrmMain.RzSizePanel2HotSpotClick(Sender: TObject);
begin
end;

{ TDocObj }

procedure TDocObj.Assign(Source: TPersistent);
begin
	if Source is TDocObj then
	begin
		FPosition.CopyRect(TDocObj(Source).FPosition)
	end;
end;

constructor TDocObj.Create;
begin
	FPosition := TDRectangle.Create;
end;

destructor TDocObj.Destroy;
begin
	FPosition.Free;
	inherited;
end;

procedure TDocObj.DrawTracker(ACanvas: TCanvas;	TrackerState: TTrackerState);
var
	HandleCounter: Integer;
	HandlePt: TPoint;
	OldPt: TPoint;                  // Used for painting the selection border from
																	// handle to handle
	Grid: TGrid;
//	BorderPen: TPen;
//	BorderBrush: LOGBRUSH;
//	BrushBMP: TBitmap;
begin
	Grid := frmMain.Grid;
	case TrackerState of
		// Selected painting: default is to paint a hatched border around the object
		// using the bounding rectangle and the eight perimeter handles
		tsSelected:
			begin
				OldPt := Grid.GridToClient(GetHandle(1));

				with ACanvas do
				begin
					Pen.Style := psSolid;
					Pen.Mode := pmCopy;
					Pen.Color := clRed;
					Pen.Width := 3;
				end;    // with

				// Paint the selection border
				BeginPath(ACanvas.Handle);
				for HandleCounter := 1 to NumHandles do
				begin
					HandlePt := Grid.GridToClient(GetHandle(HandleCounter));
					with ACanvas do
					begin
						MoveTo(OldPt.X, OldPt.Y);
						LineTo(HandlePt.X, HandlePt.Y);
						OldPt := Types.Point(HandlePt.X, HandlePt.Y)
					end;
				end;
				HandlePt := Grid.GridToClient(GetHandle(1));
				with ACanvas do
				begin
					MoveTo(OldPt.X, OldPt.Y);
					LineTo(HandlePt.X, HandlePt.Y);
				end;
				EndPath(ACanvas.Handle);
				StrokePath(ACanvas.Handle);

				// Paint the selection handles
				with ACanvas do
				begin
					Brush.Style := bsClear;
					Pen.Style := psSolid;
					Pen.Color := clBlack;
					Pen.Mode := pmCopy;
					Pen.Width := 1;
				end;  // with
				for HandleCounter := 1 to NumHandles do
				begin
					HandlePt := Grid.GridToClient(GetHandle(HandleCounter));
					with ACanvas do
					begin
						PatBlt(Handle, HandlePt.X-HandleSize, HandlePt.Y-HandleSize,
							HandleSize*2+1, HandleSize*2+1, WHITENESS);
						Polyline([Types.Point(HandlePt.X-HandleSize, HandlePt.Y-HandleSize),
							Types.Point(HandlePt.X+HandleSize, HandlePt.Y-HandleSize),
							Types.Point(HandlePt.X+HandleSize, HandlePt.Y+HandleSize),
							Types.Point(HandlePt.X-HandleSize, HandlePt.Y+HandleSize),
							Types.Point(HandlePt.X-HandleSize, HandlePt.Y-HandleSize)]);
					end;
				end;
			end;
		// Fly over painting: default is to paint a thick rectangle using the
		// bounding rectangle of the object
		tsFlyOver:
			begin
				with Grid, ACanvas do
				begin
					Pen.Color := clBlack;
					Pen.Mode := pmNotXOR;
					Pen.Width := 1;
					Pen.Style := psSolid;
					Brush.Style := bsClear;
					if Position.Width = 0 then
						Rectangle(GridToClientX(Position.Left)-5, GridToClientY(Position.Top),
							GridToClientX(Position.Right)+6, GridToClientY(Position.Bottom))
					else if Position.Height = 0 then
						Rectangle(GridToClientX(Position.Left), GridToClientY(Position.Top)-5,
							GridToClientX(Position.Right), GridToClientY(Position.Bottom)+6)
					else
						Rectangle(GridToClientX(Position.Left), GridToClientY(Position.Top),
							GridToClientX(Position.Right), GridToClientY(Position.Bottom));
				end;
			end;
	end;
end;

procedure TDocObj.EditProperties;
begin

end;

function TDocObj.GetExtents: TDRectangle;
begin
	Result := TDRectangle.Create(FPosition);
end;

function TDocObj.GetHandle(WhichHandle: Integer): TDPoint;
begin

end;

function TDocObj.GetHandleCursor(WhichHandle: Integer): TCursor;
begin
	case WhichHandle of
		1,5: Result := crSizeNWSE;
		2,6: Result := crSizeNS;
		3,7: Result := crSizeNESW;
		4,8: Result := crSizeWE;
	else
		raise Exception.Create('TDocObj.GetHandleCursor Argument must be 1..8');
	end;
end;

function TDocObj.GetHandleRect(WhichHandle: Integer): TRect;
var
	APoint: TPoint;
	Grid: TGrid;
begin
	Grid := frmMain.Grid;
	APoint := Grid.GridToClient(GetHandle(WhichHandle));
	Result := Rect(APoint.X-HandleSize, APoint.Y-HandleSize,
		APoint.X+HandleSize, APoint.Y+HandleSize);
end;

function TDocObj.HitTest(APoint: TDPoint): Integer;
var
	HandleCounter: Integer;
	TestRect: TRectangle;
	TestPt: TPoint;
	Grid: TGrid;
begin
	Grid := frmMain.Grid;
	Result := -1;
	TestRect := TRectangle.Create;
	try
		if frmMain.IsSelected(Self) then
		begin
			TestPt := Grid.GridToClient(APoint);
			for HandleCounter := 1 to NumHandles do
			begin
				TestRect.CopyRect(GetHandleRect(HandleCounter));
				if (TestRect.PtInRect(TestPt)) then
				begin
					Result := HandleCounter;
					Break;
				end;
			end;
			if Result < 0 then
			begin
				with Grid do
					TestRect.CopyRect(Rect(GridToClientX(FPosition.Left),
						GridToClientY(FPosition.Top), GridToClientX(FPosition.Right),
						GridToClientY(FPosition.Bottom)));
				if TestRect.PtInRect(TestPt) then
					Result := 0;
			end;
		end
		else
		begin
			with Grid do
			begin
				TestRect.CopyRect(Rect(GridToClientX(FPosition.Left),
					GridToClientY(FPosition.Top), GridToClientX(FPosition.Right),
					GridToClientY(FPosition.Bottom)));
				TestPt := GridToClient(APoint);
			end;
			if TestRect.PtInRect(TestPt) then
			begin
				Result := 0;
				Exit;
			end;
		end;
	finally
		TestRect.Free;
	end;
end;

function TDocObj.Intersects(ARect: TDRectangle): Boolean;
begin
	Result := FPosition.IntersectRect(ARect);
end;

procedure TDocObj.MoveHandleTo(var AHandle: Integer; APoint: TDPoint);
begin

end;

procedure TDocObj.MoveTo(APosition: TDRectangle);
begin
	if FPosition.EqualRect(APosition) then
		Exit;

	FPosition.CopyRect(APosition);
	FPosition.NormalizeRect;
end;

function TDocObj.NumHandles: Integer;
begin
  Result := 8;
end;

procedure TDocObj.SetPosition(Value: TDRectangle);
begin
	if FPosition <> Value then
	begin
		FPosition := Value;
		FPosition.NormalizeRect;
	end;
end;

{ TModelObj }

procedure TModelObj.Assign(Source: TPersistent);
begin
  if Source is TModelObj then
  begin
    inherited Assign(Source);
    FAlias := TModelObj(Source).FAlias;
    FLocked := TModelObj(Source).FLocked;
  end;
end;

constructor TModelObj.Create;
begin
	inherited;
	FAlias := '';
	FLocked := False;
end;

destructor TModelObj.Destroy;
begin

	inherited;
end;

function TModelObj.GetComments: String;
begin
	Result := FComments
end;

procedure TModelObj.SetAlias(Value: String);
begin
	if FAlias <> Value then
	begin
		FAlias := Value;
	end;
end;

procedure TModelObj.SetComments(Value: String);
begin
	if FComments <> Value then
		FComments := Value;
end;

procedure TModelObj.SetLocked(Value: Boolean);
begin
	if FLocked <> Value then
		FLocked := Value;
end;

{ TLink }

procedure TLink.AddVertex(Idx: Integer; AVertex: TDPoint);
var
  i: Integer;
begin
  SetLength(FVertices, Length(FVertices)+1);
  for i := Length(FVertices)-1 downto Idx+1 do // Move all elements above idx down
  begin
    FVertices[i] := FVertices[i-1];
  end;    // for
  FVertices[Idx] := AVertex;
  Reposition;
end;

procedure TLink.Assign(Source: TPersistent);
begin
	if Source is TLink then
	begin
		inherited Assign(Source);
		FDnJunc := TLink(Source).FDnJunc;
		FUpJunc := TLink(Source).FUpJunc;
		FVertices := Copy(FVertices, 0, High(FVertices));
	end;
end;

constructor TLink.Create;
begin
	inherited;
	FUpJunc := '';
	FDnJunc := '';
	SetLength(FVertices, 2);
end;

procedure TLink.DeleteVertex(Idx: Integer);
var
	i: Integer;
begin
		if Length(FVertices) = 2 then
		Exit; // Cannot delete any more vertices
	for i := Idx to Length(FVertices)-2 do    // Iterate
	begin
		FVertices[i] := FVertices[i+1];
	end;    // for
	SetLength(FVertices, Length(FVertices)-1);
	Reposition;
end;

destructor TLink.Destroy;
begin
	inherited;
end;

procedure TLink.Draw(ACanvas: TCanvas; DrawStyle: TDrawStyleSet;
	IsPrinting: Boolean);
begin
	inherited;

end;

procedure TLink.DrawTracker(ACanvas: TCanvas; TrackerState: TTrackerState);
begin
end;

procedure TLink.EditProperties;
begin
	inherited;

end;

function TLink.GetExtents: TDRectangle;
begin

end;

function TLink.GetHandle(WhichHandle: Integer): TDPoint;
begin

end;

function TLink.GetHandleCursor(WhichHandle: Integer): TCursor;
begin

end;

function TLink.GetMidPoint: TDPoint;
begin

end;

function TLink.GetVertex(Idx: Integer): TDPoint;
begin

end;

function TLink.HitTest(APoint: TDPoint): Integer;
begin

end;

function TLink.Intersects(ARect: TDRectangle): Boolean;
begin

end;

procedure TLink.MoveHandleTo(var AHandle: Integer; APoint: TDPoint);
begin
	inherited;

end;

procedure TLink.MoveTo(APosition: TDRectangle);
begin
	inherited;

end;

function TLink.NumHandles: Integer;
begin

end;

procedure TLink.Reposition;
begin

end;

procedure TLink.SetDnJunc(Value: String);
begin
	if FDnJunc <> Value then
		FDnJunc := Value;
end;

procedure TLink.SetUpJunc(Value: String);
begin
	if FUpJunc <> Value then
		FUpJunc := Value;
end;

procedure TLink.SetVertex(Idx: Integer; Value: TDPoint);
begin

end;

{ TConduit }

procedure TConduit.Assign(Source: TPersistent);
begin
	if Source is TConduit then
	begin
		inherited Assign(Source);
		FDnInvert := TConduit(Source).FDnInvert;
		FHorizDim := TConduit(Source).FHorizDim;
		FLength := TConduit(Source).FLength;
		FUpInvert := TConduit(Source).FUpInvert;
		FVertDim := TConduit(Source).FVertDim;
		FDnDepth := TConduit(Source).FDnDepth;
		FUpDepth := TConduit(Source).FUpDepth;
	end;
end;

constructor TConduit.Create;
begin
	inherited
end;

constructor TConduit.CreateFromDB(ALinkID: Integer);
var
	ABookMark: Pointer;
begin
	Create;
  FLinkID := ALinkID;
	ABookMark := frmMain.adoNetwork.GetBookMark;
	frmMain.adoNetwork.Locate('LinkID', ALinkID, [loCaseInsensitive]);
	FMapinfoID := frmMain.adoNetworkMAPINFO_ID.Value;
  FCompKey := frmMain.adoNetworkCompKey.Value;
{	FDnDepth := frmMain.adoNetworkDwndpth.Value;
	FDnInvert := frmMain.adoNetworkDwnelev.Value;
	FHorizDim := frmMain.adoNetworkPipediam.Value/12;
	FLength := frmMain.adoNetworkPipelen.Value;
	FUpDepth := frmMain.adoNetworkUpsdpth.Value;
	FUpInvert := frmMain.adoNetworkUpselev.Value;
	FVertDim := frmMain.adoNetworkPipeht.Value/12;
	FUpJunc := frmMain.adoNetworkUnitid.Value;
	FUpType := frmMain.adoNetworkMhfrom_type.Value;
	FDnJunc := frmMain.adoNetworkUnitid2.Value;
	FDnType := frmMain.adoNetworkMhto_type.Value;
	FUnitType := frmMain.adoNetworkUnitType.Value;
	FUpGround := frmMain.adoNetworkUps_Grnd_Elev.Value;
	FDnGround := frmMain.adoNetworkDwn_Grnd_Elev.Value;
	FAsBuilt := frmMain.adoNetworkAsblt.Value;
  FShape := frmMain.adoNetworkPipeshp.Value;
  FPipeType := frmMain.adoNetworkPipetype.Value;}

	//fDnDepth
	fDnInvert := frmMain.adoNetworkDSIE.Value;
	fHorizDim := frmMain.adoNetworkDiamWidth.Value/12;
	fLength := frmMain.adoNetworkLength.Value;
	fUpDepth := frmMain.adoNetworkupsdpth.Value;
	fUpInvert := frmMain.adoNetworkUSIE.Value;
  if frmMain.adoNetworkHeight.Value = 0 then
  	fVertDim := fHorizDim
  else
  	fVertDim := frmMain.adoNetworkHeight.Value/12;

	fUpJunc := frmMain.adoNetworkUSNode.Value;
	fUpType := frmMain.adoUpNodesNodeType.Value;
	fDnJunc := frmMain.adoNetworkDSNode.Value;
	fDnType := frmMain.adoDnNodesNodeType.Value;
	fLinkType := frmMain.adoNetworkLinkType.Value;
	fUpGround := frmMain.adoUpNodesGrndElev.Value;
	fDnGround := frmMain.adoDnNodesGrndElev.Value;
  fIsSpecLink := frmMain.adoNetworkIsSpecLink.Value;
  if fIsSpecLink then // Temporary for special links
  begin
    FLength := 100;
    fUpInvert := fUpGround;
    fDnInvert := fDnGround;
  end;
	fAsBuilt := frmMain.adoNetworkAsBuilt.Value;
	fShape := frmMain.adoNetworkShape.Value;
	fPipeType := frmMain.adoNetworkMaterial.Value;
  fDataSource := frmMain.adoNetworkDataQual.Value;
  fFlowType := frmMain.adoNetworkPipeFlowType.Value;

	frmMain.adoNetwork.GotoBookmark(ABookMark);
	frmMain.adoNetwork.FreeBookmark(ABookMark);
end;

destructor TConduit.Destroy;
begin
	inherited;
end;

procedure TConduit.Draw(ACanvas: TCanvas; DrawStyle: TDrawStyleSet;
	IsPrinting: Boolean);
var
	DrawPt: TPoint;
	OrderDrawn: Integer;
	PreviousObj, NextObj: TConduit;
	DrawArray: array[0..5] of TPoint;
	HGLSlope: Double;
	XPos: Double;
	XInt, YInt: Double;
	DCX: Integer;
	Grid: TGrid;
	LengthFromStart: Double;
	Path: TPath;
	APoint: TPoint;
begin
	Grid := frmMain.Grid;
	Path := frmMain.Path;
	DCX := DCMult(ACanvas.Handle);
	with ACanvas do
	begin

		OrderDrawn := Path.Objects.IndexOf(Self);

    // Draw the invert/ground
    LengthFromStart := Path.LengthFromStart(Self);
    if dsTemp in DrawStyle then
      Pen.Mode := pmNotXOR
    else
      Pen.Mode := pmCopy;
    if fIsSpecLink then
      Pen.Color := clPurple
    else
      Pen.Color := clGreen;
    Pen.Width := 2;
    APoint := Grid.GridToClient(DPoint(LengthFromStart, UpGround));
    MoveTo(APoint.X, APoint.Y);
    APoint := Grid.GridToClient(DPoint(LengthFromStart+Length, DnGround));
    LineTo(APoint.X, APoint.Y);
    Pen.Color := clNavy;
    APoint := Grid.GridToClient(DPoint(LengthFromStart, UpInvert));
    MoveTo(APoint.X, APoint.Y);
    APoint := Grid.GridToClient(DPoint(LengthFromStart, UpGround));
    LineTo(APoint.X, APoint.Y);
    APoint := Grid.GridToClient(DPoint(LengthFromStart+Length, DnInvert));
    MoveTo(APoint.X, APoint.Y);
    APoint := Grid.GridToClient(DPoint(LengthFromStart+Length, DnGround));
    LineTo(APoint.X, APoint.Y);

    // Draw the Conduit
    DrawArray[0] := Grid.GridToClient(DPoint(LengthFromStart, UpInvert));
    DrawArray[1] := Grid.GridToClient(DPoint(LengthFromStart, UpCrown));
    DrawArray[2] := Grid.GridToClient(DPoint(LengthFromStart+Length, DnCrown));
    DrawArray[3] := Grid.GridToClient(DPoint(LengthFromStart+Length, DnInvert));
    DrawArray[4] := DrawArray[0];
    DrawArray[5] := DrawArray[4];
    if fIsSpecLink then
      Pen.Color := clPurple
    else
      Pen.Color := clBlack;
    Pen.Width := 1;

		if dsSelected in DrawStyle then
		begin
			Brush.Style := bsSolid;
			Brush.Color := clRed;
			Polygon(DrawArray);
		end
		else
		begin
			Brush.Style := bsSolid;
			Brush.Color := clLtGray;
			Polygon(DrawArray);
		end;

		// TickMarks for dwn/up depths
		Pen.Color := clRed;
		Pen.Width := 1;
		PenPos := Grid.GridToClient(DPoint(LengthFromStart, UpGround-UpDepth));
		MoveTo(PenPos.X+1*DCX, PenPos.Y);
		LineTo(PenPos.X+8*DCX, PenPos.Y);
		PenPos := Grid.GridToClient(DPoint(LengthFromStart+Length, DnGround-DnDepth));
		MoveTo(PenPos.X-1*DCX, PenPos.Y);
		LineTo(PenPos.X-8*DCX, PenPos.Y);

		if not (dsTemp in DrawStyle) then
		begin
			Brush.Style := bsClear;
			ACanvas.Font.Name := 'Tahoma';
			ACanvas.Font.Size := 7;
			ACanvas.Font.Color := clBlack;
			ACanvas.Font.Style := [];
			TextOut(Grid.GridToClientX(LengthFromStart), Grid.GridToClientY(UpGround), Format('%d', [CompKey]));
			TextOut(Grid.GridToClientX(LengthFromStart), Grid.GridToClientY(UpGround)+TextWidth('Wg'), Format('%s', [Trim(FUpJunc)]));
			if dsSelected in DrawStyle then
			begin
				Font.Color := clRed;
				TextOut(Grid.GridToClientX(LengthFromStart), Grid.GridToClientY(UpGround), Format('%d', [CompKey]));
				TextOut(Grid.GridToClientX(LengthFromStart), Grid.GridToClientY(UpGround)+TextWidth('Wg'), Format('%s', [Trim(FUpJunc)]));
				DrawTracker(ACanvas, tsSelected);
			end;
		end;
	end;
end;

procedure TConduit.DrawTracker(ACanvas: TCanvas;
	TrackerState: TTrackerState);
var
	Grid: TGrid;
	ProfilePath: TPath;
	XPos: Double;
	HighCrown, LowInvert: Double;
	HandlePt: TPoint;
	HandleCounter: Integer;
begin
	Grid := frmMain.Grid;
	ProfilePath := frmMain.Path;
	XPos := ProfilePath.LengthFromStart(Self);
	HighCrown := Math.Max(Math.Max(UpGround,UpCrown), Math.Max(DnGround,DnCrown));
	LowInvert := Math.Min(UpInvert, DnInvert);
	case TrackerState of
		tsSelected:
		begin
			// Paint the selection handles
			with ACanvas do
			begin
				Brush.Style := bsSolid;
				Pen.Style := psSolid;
				Pen.Color := clBlack;
				Pen.Mode := pmCopy;
				Pen.Width := 1;
			end;  // with
			for HandleCounter := 1 to NumHandles do
			begin
				HandlePt := Grid.GridToClient(GetHandle(HandleCounter));
				with ACanvas do
				begin
					case HandleCounter of
						1..8: Brush.Color := clWhite;
						else
							Brush.Color := clBlue;
					end;
//					PatBlt(Handle, HandlePt.X-HandleSize, HandlePt.Y-HandleSize,
//						HandleSize*2+1, HandleSize*2+1, WHITENESS);
					Polygon([Types.Point(HandlePt.X-HandleSize, HandlePt.Y-HandleSize),
						Types.Point(HandlePt.X+HandleSize, HandlePt.Y-HandleSize),
						Types.Point(HandlePt.X+HandleSize, HandlePt.Y+HandleSize),
						Types.Point(HandlePt.X-HandleSize, HandlePt.Y+HandleSize),
						Types.Point(HandlePt.X-HandleSize, HandlePt.Y-HandleSize)]);
					Polyline([Types.Point(HandlePt.X-HandleSize, HandlePt.Y-HandleSize),
						Types.Point(HandlePt.X+HandleSize, HandlePt.Y-HandleSize),
						Types.Point(HandlePt.X+HandleSize, HandlePt.Y+HandleSize),
						Types.Point(HandlePt.X-HandleSize, HandlePt.Y+HandleSize),
						Types.Point(HandlePt.X-HandleSize, HandlePt.Y-HandleSize)]);
				end;
			end;
		end;
		tsFlyOver:
		begin
			with Grid, ACanvas do
			begin
				Pen.Color := clGray;
				Pen.Mode := pmNotXOR;
				Pen.Width := SelectionPenWidth;
				Pen.Style := psSolid;
				Brush.Style := bsClear;
				Rectangle(GridToClientX(XPos)-5, GridToClientY(HighCrown)-5,
					GridToClientX(XPos+Length)+5, GridToClientY(LowInvert)+5);
			end;
		end;
	end;
end;

function TConduit.GetAsString: String;
begin

end;

function TConduit.GetBestVertDim(TrialDim: Double): Double;
begin
	if TrialDim < 0.50 then
		Result := 0.50
	else if TrialDim < 0.67 then
		Result := 0.67
	else if TrialDim < 0.83 then
		Result := 0.83
	else if TrialDim < 1.00 then
		Result := 1.00
	else if TrialDim < 2.50 then
		Result := Round((TrialDim - 1.00)/0.25)*0.25+1.00
	else if TrialDim < 5.00 then
		Result := Round((TrialDim - 2.50)/0.50)*0.50+2.50
	else
    Result := Round(TrialDim - 5.00)+5.00;
end;

function TConduit.GetDnCrown: Double;
begin
  if (FShape = 'CIRC') or (FShape = '') then
  	Result := FDnInvert + FHorizDim
  else
  	Result := FDnInvert + FVertDim;
end;

function TConduit.GetDnGround: Double;
begin
	Result := FDnInvert + FDnDepth;
end;

function TConduit.GetExtents: TDRectangle;
var
	ProfilePath: TPath;
	XPos: Double;
	HighGround: Double;
	LowGround: Double;
	LowInvert: Double;
	HighInvert: Double;
	Grid: TGrid;
	Canvas: TCanvas;
begin
	Grid := frmMain.Grid;
	Canvas := frmMain.DrawBox.Canvas;
	ProfilePath := frmMain.Path;
	XPos := ProfilePath.LengthFromStart(Self);
	HighGround := Max(UpGround, DnGround);
	LowGround := Min(UpGround, DnGround);
	HighInvert := Max(UpInvert, DnInvert);
	LowInvert := Min(UpInvert, DnInvert);
	HighGround := Max(HighGround, HighInvert+VertDim);
	LowInvert := Min(LowInvert, LowGround);
	LowInvert := Min(LowInvert, UpGround-UpDepth);
	LowInvert := Min(LowInvert, DnGround-DnDepth);

	Result := TDRectangle.Create(DPoint(XPos, HighGround),
		DPoint(XPos+FLength, LowInvert));
	Result.InflateRect(Grid.HorizGridUnits(20), Grid.VertGridUnits(20));
	with Canvas do
	begin
		Font.Name := 'Tahoma';
		Font.Size := 8;
		with Grid do
			Result.Right := Result.Right + Math.Max(0,GridUnits(TextWidth(Format('%s',[FUpJunc])),0)-Result.Width);
	end;
end;

function TConduit.GetHandle(WhichHandle: Integer): TDPoint;
var
	ProfilePath: TPath;
	XPos: Double;
	Grid: TGrid;
begin
	Grid := frmMain.Grid;
	ProfilePath := frmMain.Path;
	XPos := ProfilePath.LengthFromStart(Self);
	case WhichHandle of
		1: Result := DPoint(XPos, UpCrown);
		2: Result := DPoint(XPos+Length, DnCrown);
		3: Result := DPoint(XPos, UpInvert);
		4: Result := DPoint(XPos+Length, DnInvert);
		5: Result := DPoint(XPos+Length/2, (UpCrown+DnCrown)/2-VertDim/2);
		6: Result := DPoint(XPos+Length, FDnInvert+VertDim/2);
		7: Result := DPoint(XPos, UpGround);
		8: Result := DPoint(XPos+Length, DnGround);
		9: Result := DPoint(XPos-Grid.HorizGridUnits(7), UpInvert);
		10: Result := DPoint(XPos+Length+Grid.HorizGridUnits(7), DnInvert);
	end;
end;

function TConduit.GetSlope: Double;
begin
	Result := (FUpInvert - FDnInvert)/Length;
end;

function TConduit.GetUpCrown: Double;
begin
  if (FShape = 'CIRC') or (FShape = '') then
  	Result := FUpInvert + FHorizDim
  else
  	Result := FUpInvert + FVertDim;
end;

function TConduit.GetUpGround: Double;
begin
	Result := FUpInvert + FUpDepth;
end;

function TConduit.HitTest(APoint: TDPoint): Integer;
var
	HandleCounter: Integer;
	TestRect: TRectangle;
	TestPt: TPoint;
	TestRegion: HRgn;
	TestArray: array [0..3] of TPoint;
	ProfilePath: TPath;
	XPos: Double;
	Grid: TGrid;
begin
	Grid := frmMain.Grid;
	Result := -1;
	TestRect := TRectangle.Create;
	try
		if frmMain.IsSelected(Self) then
			// Look through all the handles to see if we hit one; handles appear only
			// if the link is selected
			for HandleCounter := 1 to NumHandles do
			begin
				TestRect.CopyRect(GetHandleRect(HandleCounter));
				TestPt := Grid.GridToClient(APoint);
				if TestRect.PtInRect(TestPt) then
				begin
					Result := HandleCounter;
					Break;
				end;
			end;

		// If we did not hit a handle, did we hit the link itself?
//				if (Result < 0) and HitLine(View.Grid, APoint, FUpJunc.Position.TopLeft,
//					FDnJunc.Position.BottomRight) then
//					Result := 0;
		if (Result < 0) then
		begin
			ProfilePath := frmMain.Path;
			with Grid do
			begin
				XPos := ProfilePath.LengthFromStart(Self);
{				TestArray[0] := Types.Point(GridToClientX(XPos), GridToClientY(UpInvert));
				TestArray[1] := Types.Point(GridToClientX(XPos), GridToClientY(Math.Max(UpCrown,UpGround)));
				TestArray[2] := Types.Point(GridToClientX(XPos+FLength), GridToClientY(Math.Max(DnGround,DnCrown)));
				TestArray[3] := Types.Point(GridToClientX(XPos+FLength), GridToClientY(DnInvert));}
				TestArray[0] := Types.Point(GridToClientX(XPos), GridToClientY(Max.Y));
				TestArray[1] := Types.Point(GridToClientX(XPos), GridToClientY(Min.Y));
				TestArray[2] := Types.Point(GridToClientX(XPos+FLength), GridToClientY(Min.Y));
				TestArray[3] := Types.Point(GridToClientX(XPos+FLength), GridToClientY(Max.Y));
				TestRegion := CreatePolygonRgn(TestArray, 4, ALTERNATE);
				if PtInRegion(TestRegion, GridToClientX(APoint.X), GridToClientY(APoint.Y)) then
					Result := 0;
			end;
			DeleteObject(TestRegion);
		end;
	finally
		TestRect.Free;
	end;
end;

procedure TConduit.MoveHandleTo(var AHandle: Integer; APoint: TDPoint);
var
	NewVertDim: Double;
	MaxOppDim: Double;
	MaxDim: Double;
	ProfilePath: TPath;
	TempUpCrown: Double;
	OldSlope, OldDnGround, OldUpGround: Double;
	SnapPt: TDPoint;
	Grid: TGrid;
begin
	Grid := frmMain.Grid;
	case AHandle of
		1: // Upper Left Corner: Vertical Dimension
		begin
			NewVertDim := GetBestVertDim(APoint.Y-UpInvert);
			MaxDim := UpGround-(UpInvert);
			MaxOppDim := DnGround-(DnInvert);
			if (MaxDim < 0) or (MaxOppDim < 0) then
				FVertDim := NewVertDim
			else
				FVertDim := Max(0, Min(NewVertDim, Min(MaxOppDim,MaxDim)));
      if (FShape = 'CIRC') or (FShape = '') then
        FHorizDim := FVertDim;
		end;
		2: // Upper Right Corner: Vertical Dimension
		begin
			NewVertDim := GetBestVertDim(APoint.Y-(DnInvert));
			MaxDim := DnGround-(DnInvert);
			MaxOppDim := UpGround-(UpInvert);
			if (MaxDim < 0) or (MaxOppDim < 0) then
				FVertDim := NewVertDim
			else
				FVertDim := Max(0, Min(NewVertDim, Min(MaxOppDim,MaxDim)));
      if (FShape = 'CIRC') or (FShape = '') then
        FHorizDim := FVertDim;
		end;
		3: // Lower Left Corner: Upstream Invert
		begin
			OldUpGround := UpGround;
{			if Grid.VertClientUnits(Abs(APoint.Y-(OldUpGround-UpDepth))) <= HitTol then
				UpInvert := OldUpGround-UpDepth
			else}
				UpInvert := StrToFloat(Format('%.2f', [APoint.Y]));
//			UpDepth := OldUpGround-UpInvert;
			if (ssAlt in frmMain.ShiftState) and (frmMain.Path.PosOfObj(Self) > 0) then
				TConduit(frmMain.Path.Objects[frmMain.Path.PosOfObj(Self)-1]).DnInvert := UpInvert;
		end;
		4: // Lower Right Corner: Downstream Invert
		begin
			OldDnGround := DnGround;
{			if Grid.VertClientUnits(Abs(APoint.Y-(OldDnGround-DnDepth))) <= HitTol then
				DnInvert := OldDnGround-DnDepth
			else}
				DnInvert := StrToFloat(Format('%.2f', [APoint.Y]));
//			DnDepth := OldDnGround-DnInvert;
			if (ssAlt in frmMain.ShiftState) and
				(frmMain.Path.PosOfObj(Self) < (frmMain.Path.Objects.Count-1)) then
				TConduit(frmMain.Path.Objects[frmMain.Path.PosOfObj(Self)+1]).UpInvert := DnInvert;
		end;
		5: // Center: Vertical Position
		begin
			OldSlope := Slope;
			OldDnGround := DnGround;
			OldUpGround := UpGround;
			DnInvert := StrToFloat(Format('%.2f',
				[Min((APoint.Y-Slope*(Length/2)-FVertDim/2), DnGround-VertDim)]));

			TempUpCrown := StrToFloat(Format('%.2f',
				[DnCrown+OldSlope*FLength]));
			// Cannot have upstream pipe crown break the ground
			if TempUpCrown > UpGround then
			begin
				FUpInvert :=	UpGround-VertDim;
				FDnInvert := StrToFloat(Format('%.2f',
					[UpCrown-OldSlope*Length-VertDim]));
				end
			else
				FUpInvert := StrToFloat(Format('%.2f',
					[DnCrown+OldSlope*Length-VertDim]));
			if (ssAlt in frmMain.ShiftState) and
				((frmMain.Path.PosOfObj(Self) < (frmMain.Path.Objects.Count-1)) and (frmMain.Path.PosOfObj(Self) > 0)) then
			begin
				TConduit(frmMain.Path.Objects[frmMain.Path.PosOfObj(Self)+1]).UpInvert := DnInvert;
				TConduit(frmMain.Path.Objects[frmMain.Path.PosOfObj(Self)-1]).DnInvert := UpInvert;
			end;

//			DnDepth := OldDnGround - DnInvert;
//			UpDepth := OldUpGround - UpInvert;
			// Cannot move upstream invert below node invert
{			if FUpOffset < 0 then
			begin
				FUpOffset := 0;
				FDnOffset := StrToFloat(Format('%.2f',
					[FUpJunc.Invert-OldSlope*Abs(FLength)-FDnJunc.Invert]));
			end;}
		end;
		6: // Right Center: Length
		begin
			ProfilePath := frmMain.Path;
			FLength := Max(0.1,Round(Max(0, APoint.X - ProfilePath.LengthFromStart(Self))));
			FLength := StrToFloat(Format('%.2f', [FLength]))
		end;
		7: // Left Ground
		begin
			if APoint.Y < UpCrown then
				FUpGround := UpCrown
			else
				FUpGround := StrToFloat(Format('%.2f', [APoint.Y]));
			if Grid.VertClientUnits(Abs(APoint.Y-(UpInvert+UpDepth))) <= HitTol then
				UpGround := UpInvert+UpDepth;
//			if (ssAlt in frmMain.ShiftState) and (frmMain.Path.PosOfObj(Self) > 0) then
        if frmMain.Path.PosOfObj(Self) > 0 then
  				TConduit(frmMain.Path.Objects[frmMain.Path.PosOfObj(Self)-1]).DnGround := UpGround;
		end;
		8: // Right Ground
		begin
			if APoint.Y < DnCrown then
				FDnGround := DnCrown
			else
				FDnGround := StrToFloat(Format('%.2f', [APoint.Y]));
			if Grid.VertClientUnits(Abs(APoint.Y-(DnInvert+DnDepth))) <= HitTol then
				DnGround := DnInvert+DnDepth;
//			if (ssAlt in frmMain.ShiftState) and
//				(frmMain.Path.PosOfObj(Self) < (frmMain.Path.Objects.Count-1)) then
      if frmMain.Path.PosOfObj(Self) < (frmMain.Path.Objects.Count-1) then
				TConduit(frmMain.Path.Objects[frmMain.Path.PosOfObj(Self)+1]).UpGround := DnGround;
		end;
		9: // Snap upstream invert to next highest adjacent pipe invert
		begin
			if Grid.VertClientUnits(Abs(APoint.Y-(UpGround-UpDepth))) <= HitTol then
			begin
				UpInvert := UpGround-UpDepth;
				frmMain.mnuStatusStatus.Caption := 'Snapped to upstream rim-to-depth';
			end
			else
				with frmMain.Path do
        begin
					FUpInvert := NextHighestUpInvert(PosOfObj(Self), APoint.Y);
          SetDataSourceItem(4,'T');
        end;
			if (ssAlt in frmMain.ShiftState) and (frmMain.Path.PosOfObj(Self) > 0) then
      begin
				TConduit(frmMain.Path.Objects[frmMain.Path.PosOfObj(Self)-1]).DnInvert := UpInvert;
				TConduit(frmMain.Path.Objects[frmMain.Path.PosOfObj(Self)-1]).SetDataSourceItem(5,'T');
		    frmMain.btnDataSource.Caption := frmMain.FormatDataSourceString(
        	TConduit(frmMain.Path.Objects[frmMain.Path.PosOfObj(Self)-1]).FDataSource);
      end;
		end;
{		10: // Snap upstream invert to next lowest adjacent pipe invert
		begin
			if Grid.VertClientUnits(Abs(APoint.Y-(UpGround-UpDepth))) <= HitTol then
				UpInvert := UpGround-UpDepth
			else
				with frmMain.Path do
					FUpInvert := NextLowestUpInvert(PosOfObj(Self), APoint.Y);
			if (ssAlt in frmMain.ShiftState) and (frmMain.Path.PosOfObj(Self) > 0) then
				TConduit(frmMain.Path.Objects[frmMain.Path.PosOfObj(Self)-1]).DnInvert := UpInvert;
		end;}
		10: // Snap downstream invert to next highest adjacent pipe invert
		begin
			if Grid.VertClientUnits(Abs(APoint.Y-(DnGround-DnDepth))) <= HitTol then
			begin
				DnInvert := DnGround-DnDepth;
				frmMain.mnuStatusStatus.Caption := 'Snapped to downstream rim-to-depth';
			end
			else
				with frmMain.Path do
        begin
					FDnInvert := NextHighestDnInvert(PosOfObj(Self), APoint.Y);
          SetDataSourceItem(5,'T');
        end;
			if (ssAlt in frmMain.ShiftState) and
				(frmMain.Path.PosOfObj(Self) < (frmMain.Path.Objects.Count-1)) then
      begin
				TConduit(frmMain.Path.Objects[frmMain.Path.PosOfObj(Self)+1]).UpInvert := DnInvert;
				TConduit(frmMain.Path.Objects[frmMain.Path.PosOfObj(Self)+1]).SetDataSourceItem(4,'T');
		    frmMain.btnDataSource.Caption := frmMain.FormatDataSourceString(
        	TConduit(frmMain.Path.Objects[frmMain.Path.PosOfObj(Self)+1]).FDataSource);
      end;
		end;
{		12: // Snap downstream invert to next lowest adjacent pipe invert
		begin
			if Grid.VertClientUnits(Abs(APoint.Y-(DnGround-DnDepth))) <= HitTol then
				DnInvert := DnGround-DnDepth
			else
				with frmMain.Path do
					FDnInvert := NextLowestDnInvert(PosOfObj(Self), APoint.Y);
			if (ssAlt in frmMain.ShiftState) and
				(frmMain.Path.PosOfObj(Self) < (frmMain.Path.Objects.Count-1)) then
				TConduit(frmMain.Path.Objects[frmMain.Path.PosOfObj(Self)+1]).UpInvert := DnInvert;
		end;}
	end; // case AHandle of
	frmMain.Modified := True;
end;

function TConduit.NumHandles: Integer;
begin
	Result := 10;
end;

function TConduit.ObjectType: String;
begin
	Result := 'Conduit';
end;

procedure TConduit.RecallFromDB;
var
	ABookMark: Pointer;
begin
	ABookMark := frmMain.adoNetwork.GetBookmark;
	frmMain.adoNetwork.Locate('LinkID', FLinkID, [loCaseInsensitive]);
	FMapinfoID := frmMain.adoNetworkMAPINFO_ID.Value;
  FCompKey := frmMain.adoNetworkCompKey.Value;
{	FDnDepth := frmMain.adoNetworkDwndpth.Value;
	FDnInvert := frmMain.adoNetworkDwnelev.Value;
	FHorizDim := frmMain.adoNetworkPipediam.Value/12;
	FLength := frmMain.adoNetworkPipelen.Value;
	FUpDepth := frmMain.adoNetworkUpsdpth.Value;
	FUpInvert := frmMain.adoNetworkUpselev.Value;
	FVertDim := frmMain.adoNetworkPipeht.Value/12;
	FUpJunc := frmMain.adoNetworkUnitid.Value;
	FUpType := frmMain.adoNetworkMhfrom_type.Value;
	FDnJunc := frmMain.adoNetworkUnitid2.Value;
	FDnType := frmMain.adoNetworkMhto_type.Value;
	FUnitType := frmMain.adoNetworkUnitType.Value;
	FUpGround := frmMain.adoNetworkUps_Grnd_Elev.Value;
	FDnGround := frmMain.adoNetworkDwn_Grnd_Elev.Value;
	FAsBuilt := frmMain.adoNetworkAsblt.Value;
  FShape := frmMain.adoNetworkPipeshp.Value;
  FPipeType := frmMain.adoNetworkPipetype.Value;}

	//fDnDepth
	fDnInvert := frmMain.adoNetworkDSIE.Value;
	fHorizDim := frmMain.adoNetworkDiamWidth.Value/12;
	fLength := frmMain.adoNetworkLength.Value;
	fUpDepth := frmMain.adoNetworkupsdpth.Value;
	fUpInvert := frmMain.adoNetworkUSIE.Value;
  if frmMain.adoNetworkHeight.Value = 0 then
  	fVertDim := fHorizDim
  else
  	fVertDim := frmMain.adoNetworkHeight.Value/12;

	fUpJunc := frmMain.adoNetworkUSNode.Value;
	fUpType := frmMain.adoUpNodesNodeType.Value;
	fDnJunc := frmMain.adoNetworkDSNode.Value;
	fDnType := frmMain.adoDnNodesNodeType.Value;
	fLinkType := frmMain.adoNetworkLinkType.Value;
	fUpGround := frmMain.adoUpNodesGrndElev.Value;
	fDnGround := frmMain.adoDnNodesGrndElev.Value;
  fIsSpecLink := frmMain.adoNetworkIsSpecLink.Value;
  if fIsSpecLink then // Temporary for special links
  begin
    FLength := 100;
    fUpInvert := fUpGround;
    fDnInvert := fDnGround;
  end;
	fAsBuilt := frmMain.adoNetworkAsBuilt.Value;
	fShape := frmMain.adoNetworkShape.Value;
	fPipeType := frmMain.adoNetworkMaterial.Value;
  fDataSource := frmMain.adoNetworkDataQual.Value;
  fFlowType := frmMain.adoNetworkPipeFlowType.Value;

	frmMain.adoNetwork.GotoBookmark(ABookMark);
	frmMain.adoNetwork.FreeBookmark(AbookMark);
end;

procedure TConduit.SaveToDB;
var
	ABookMark: Pointer;
  SaveDateTime: TDateTime;
begin
	with frmMain do
	begin
    SaveDateTime := Now;
		ABookMark := adoNetwork.GetBookmark;
		adoNetwork.Locate('LinkID', FLinkID, [loCaseInsensitive]);
		adoNetwork.Edit;

		if (not fIsSpecLink) and (adoNetworkDSIE.Value <> FDnInvert) then
		begin
			if not adoLinkChanges.Locate('LinkID;FieldName',VarArrayOf([adoNetworkLinkID.Value,adoNetworkDSIE.FieldName]),
				[loCaseInsensitive]) then
			begin
				adoLinkChanges.Append;
				adoLinkChanges.FieldByName('LinkID').Value := adoNetworkLinkID.Value;
				adoLinkChanges.FieldByName('FieldName').Value := adoNetworkDSIE.FieldName;
				adoLinkChanges.FieldByName('OldValue').Value := adoNetworkDSIE.Value;
			end
			else
				adoLinkChanges.Edit;
      adoLinkChanges.FieldByName('Changed').Value := True;
			adoLinkChanges.FieldByName('NewValue').Value := StrToFloat(Format('%.2f', [FDnInvert]));
			adoLinkChanges.FieldByName('Comment').Value := edtComment.Text;
      adoLinkChanges.FieldByName('UserName').Value := UserName;
      adoLinkChanges.FieldByName('UserTime').Value := SaveDateTime;
			adoLinkChanges.Post;
			adoNetworkDSIE.Value := StrToFloat(Format('%.2f', [FDnInvert]));
		end;

		if Format('%.2f', [adoNetworkDiamWidth.Value]) <> Format('%.2f', [FHorizDim*12]) then
		begin
			if not adoLinkChanges.Locate('LinkID;FieldName',VarArrayOf([adoNetworkLinkID.Value,adoNetworkDiamWidth.FieldName]),
				[loCaseInsensitive]) then
			begin
				adoLinkChanges.Append;
				adoLinkChanges.FieldByName('LinkID').Value := adoNetworkLinkID.Value;
				adoLinkChanges.FieldByName('FieldName').Value := adoNetworkDiamWidth.FieldName;
				adoLinkChanges.FieldByName('OldValue').Value := adoNetworkDiamWidth.Value;
			end
			else
				adoLinkChanges.Edit;
      adoLinkChanges.FieldByName('Changed').Value := True;
			adoLinkChanges.FieldByName('NewValue').Value := StrToFloat(Format('%.2f', [FHorizDim*12]));
			adoLinkChanges.FieldByName('Comment').Value := edtComment.Text;
      adoLinkChanges.FieldByName('UserName').Value := UserName;
      adoLinkChanges.FieldByName('UserTime').Value := SaveDateTime;
			adoLinkChanges.Post;
			adoNetworkDiamWidth.Value := StrToFloat(Format('%.2f', [FHorizDim*12]));
		end;

    if ((FShape <> 'CIRC') and (FShape <> '')) and (Format('%.2f', [adoNetworkHeight.Value]) <> Format('%.2f', [FVertDim*12])) then
		begin
			if not adoLinkChanges.Locate('LinkID;FieldName',VarArrayOf([adoNetworkLinkID.Value,adoNetworkHeight.FieldName]),
				[loCaseInsensitive]) then
			begin
				adoLinkChanges.Append;
				adoLinkChanges.FieldByName('LinkID').Value := adoNetworkLinkID.Value;
				adoLinkChanges.FieldByName('FieldName').Value := adoNetworkHeight.FieldName;
				adoLinkChanges.FieldByName('OldValue').Value := adoNetworkHeight.Value;
			end
			else
				adoLinkChanges.Edit;
      adoLinkChanges.FieldByName('Changed').Value := True;
			adoLinkChanges.FieldByName('NewValue').Value := StrToFloat(Format('%.2f', [FVertDim*12]));
			adoLinkChanges.FieldByName('Comment').Value := edtComment.Text;
      adoLinkChanges.FieldByName('UserName').Value := UserName;
      adoLinkChanges.FieldByName('UserTime').Value := SaveDateTime;
			adoLinkChanges.Post;
			adoNetworkHeight.Value := StrToFloat(Format('%.2f', [FVertDim*12]));
		end;

		if (not fIsSpecLink) and (adoNetworkLength.Value <> FLength) then
		begin
			if not adoLinkChanges.Locate('LinkID;FieldName',VarArrayOf([adoNetworkLinkID.Value,adoNetworkLength.FieldName]),
				[loCaseInsensitive]) then
			begin
				adoLinkChanges.Append;
				adoLinkChanges.FieldByName('LinkID').Value := adoNetworkLinkID.Value;
				adoLinkChanges.FieldByName('FieldName').Value := adoNetworkLength.FieldName;
				adoLinkChanges.FieldByName('OldValue').Value := adoNetworkLength.Value;
			end
			else
				adoLinkChanges.Edit;
      adoLinkChanges.FieldByName('Changed').Value := True;
			adoLinkChanges.FieldByName('NewValue').Value := StrToFloat(Format('%.2f', [FLength]));
			adoLinkChanges.FieldByName('Comment').Value := edtComment.Text;
      adoLinkChanges.FieldByName('UserName').Value := UserName;
      adoLinkChanges.FieldByName('UserTime').Value := SaveDateTime;
			adoLinkChanges.Post;
			adoNetworkLength.Value := StrToFloat(Format('%.2f', [FLength]));
		end;

		if (not fIsSpecLink) and (adoNetworkUSIE.Value <> FUpInvert) then
		begin
			if not adoLinkChanges.Locate('LinkID;FieldName',VarArrayOf([adoNetworkLinkID.Value,adoNetworkUSIE.FieldName]),
				[loCaseInsensitive]) then
			begin
				adoLinkChanges.Append;
				adoLinkChanges.FieldByName('LinkID').Value := adoNetworkLinkID.Value;
				adoLinkChanges.FieldByName('FieldName').Value := adoNetworkUSIE.FieldName;
				adoLinkChanges.FieldByName('OldValue').Value := adoNetworkUSIE.Value;
			end
			else
				adoLinkChanges.Edit;
      adoLinkChanges.FieldByName('Changed').Value := True;
			adoLinkChanges.FieldByName('NewValue').Value := StrToFloat(Format('%.2f', [FUpInvert]));
			adoLinkChanges.FieldByName('Comment').Value := edtComment.Text;
      adoLinkChanges.FieldByName('UserName').Value := UserName;
      adoLinkChanges.FieldByName('UserTime').Value := SaveDateTime;
			adoLinkChanges.Post;
			adoNetworkUSIE.Value := StrToFloat(Format('%.2f', [FUpInvert]));
		end;

		if adoUpNodesGrndElev.Value <> FUpGround then
		begin
			if not adoNodeChanges.Locate('Node;FieldName',VarArrayOf([adoNetworkUSNode.Value,adoUpNodesGrndElev.FieldName]),
				[loCaseInsensitive]) then
			begin
				adoNodeChanges.Append;
				adoNodeChanges.FieldByName('Node').Value := adoNetworkUSNode.Value;
				adoNodeChanges.FieldByName('FieldName').Value := adoUpNodesGrndElev.FieldName;
				adoNodeChanges.FieldByName('OldValue').Value := adoUpNodesGrndElev.Value;
			end
			else
				adoNodeChanges.Edit;
      adoNodeChanges.FieldByName('Changed').Value := True;
			adoNodeChanges.FieldByName('NewValue').Value := StrToFloat(Format('%.2f', [FUpGround]));
			adoNodeChanges.FieldByName('Comment').Value := edtComment.Text;
      adoNodeChanges.FieldByName('UserName').Value := UserName;
      adoNodeChanges.FieldByName('UserTime').Value := SaveDateTime;
			adoNodeChanges.Post;
      adoNodes.Locate('Node', adoUpNodesNode.Value, [loCaseInsensitive]);
      adoNodes.Edit;
			adoNodesGrndElev.Value := StrToFloat(Format('%.2f', [FUpGround]));
      adoNodes.Post;
		end;

		if adoUpNodesNodeType.Value <> FUpType then
		begin
			if not adoNodeChanges.Locate('Node;FieldName',VarArrayOf([adoNetworkUSNode.Value,adoUpNodesNodeType.FieldName]),
				[loCaseInsensitive]) then
			begin
				adoNodeChanges.Append;
				adoNodeChanges.FieldByName('Node').Value := adoNetworkUSNode.Value;
				adoNodeChanges.FieldByName('FieldName').Value := adoUpNodesNodeType.FieldName;
				adoNodeChanges.FieldByName('OldValue').Value := adoUpNodesNodeType.Value;
			end
			else
				adoNodeChanges.Edit;
      adoNodeChanges.FieldByName('Changed').Value := True;
			adoNodeChanges.FieldByName('NewValue').Value := FUpType;
			adoNodeChanges.FieldByName('Comment').Value := edtComment.Text;
      adoNodeChanges.FieldByName('UserName').Value := UserName;
      adoNodeChanges.FieldByName('UserTime').Value := SaveDateTime;
			adoNodeChanges.Post;
      adoNodes.Locate('Node', adoUpNodesNode.Value, [loCaseInsensitive]);
      adoNodes.Edit;
			adoNodesNodeType.Value := FUpType;
      adoNodes.Post;
		end;

		if adoDnNodesGrndElev.Value <> FDnGround then
		begin
			if not adoNodeChanges.Locate('Node;FieldName',VarArrayOf([adoNetworkDSNode.Value,adoDnNodesGrndElev.FieldName]),
				[loCaseInsensitive]) then
			begin
				adoNodeChanges.Append;
				adoNodeChanges.FieldByName('Node').Value := adoNetworkDSNode.Value;
				adoNodeChanges.FieldByName('FieldName').Value := adoDnNodesGrndElev.FieldName;
				adoNodeChanges.FieldByName('OldValue').Value := adoDnNodesGrndElev.Value;
			end
			else
				adoNodeChanges.Edit;
      adoNodeChanges.FieldByName('Changed').Value := True;
			adoNodeChanges.FieldByName('NewValue').Value := StrToFloat(Format('%.2f', [FDnGround]));
			adoNodeChanges.FieldByName('Comment').Value := edtComment.Text;
      adoNodeChanges.FieldByName('UserName').Value := UserName;
      adoNodeChanges.FieldByName('UserTime').Value := SaveDateTime;
			adoNodeChanges.Post;
      adoNodes.Locate('Node', adoDnNodesNode.Value, [loCaseInsensitive]);
      adoNodes.Edit;
			adoNodesGrndElev.Value := StrToFloat(Format('%.2f', [FDnGround]));
      adoNodes.Post;
		end;

		if adoDnNodesNodeType.Value <> FDnType then
		begin
			if not adoNodeChanges.Locate('Node;FieldName',VarArrayOf([adoNetworkDSNode.Value,adoDnNodesNodeType.FieldName]),
				[loCaseInsensitive]) then
			begin
				adoNodeChanges.Append;
				adoNodeChanges.FieldByName('Node').Value := adoNetworkDSNode.Value;
				adoNodeChanges.FieldByName('FieldName').Value := adoDnNodesNodeType.FieldName;
				adoNodeChanges.FieldByName('OldValue').Value := adoDnNodesNodeType.Value;
			end
			else
				adoNodeChanges.Edit;
      adoNodeChanges.FieldByName('Changed').Value := True;
			adoNodeChanges.FieldByName('NewValue').Value := FDnType;
			adoNodeChanges.FieldByName('Comment').Value := edtComment.Text;
      adoNodeChanges.FieldByName('UserName').Value := UserName;
      adoNodeChanges.FieldByName('UserTime').Value := SaveDateTime;
			adoNodeChanges.Post;
      adoNodes.Locate('Node', adoDnNodesNode.Value, [loCaseInsensitive]);
      adoNodes.Edit;
			adoNodesNodeType.Value := FDnType;
      adoNodes.Post;
		end;

		if adoNetworkShape.Value <> FShape then
		begin
			if not adoLinkChanges.Locate('LinkID;FieldName',VarArrayOf([adoNetworkLinkID.Value,adoNetworkShape.FieldName]),
				[loCaseInsensitive]) then
			begin
				adoLinkChanges.Append;
				adoLinkChanges.FieldByName('LinkID').Value := adoNetworkLinkID.Value;
				adoLinkChanges.FieldByName('FieldName').Value := adoNetworkShape.FieldName;
				adoLinkChanges.FieldByName('OldValue').Value := adoNetworkShape.Value;
			end
			else
				adoLinkChanges.Edit;
      adoLinkChanges.FieldByName('Changed').Value := True;
			adoLinkChanges.FieldByName('NewValue').Value := FShape;
			adoLinkChanges.FieldByName('Comment').Value := edtComment.Text;
      adoLinkChanges.FieldByName('UserName').Value := UserName;
      adoLinkChanges.FieldByName('UserTime').Value := SaveDateTime;
			adoLinkChanges.Post;
			adoNetworkShape.Value := FShape;
		end;

		if adoNetworkMaterial.Value <> FPipeType then
		begin
			if not adoLinkChanges.Locate('LinkID;FieldName',VarArrayOf([adoNetworkLinkID.Value,adoNetworkMaterial.FieldName]),
				[loCaseInsensitive]) then
			begin
				adoLinkChanges.Append;
				adoLinkChanges.FieldByName('LinkID').Value := adoNetworkLinkID.Value;
				adoLinkChanges.FieldByName('FieldName').Value := adoNetworkMaterial.FieldName;
				adoLinkChanges.FieldByName('OldValue').Value := adoNetworkMaterial.Value;
			end
			else
				adoLinkChanges.Edit;
      adoLinkChanges.FieldByName('Changed').Value := True;
			adoLinkChanges.FieldByName('NewValue').Value := FPipetype;
			adoLinkChanges.FieldByName('Comment').Value := edtComment.Text;
      adoLinkChanges.FieldByName('UserName').Value := UserName;
      adoLinkChanges.FieldByName('UserTime').Value := SaveDateTime;
			adoLinkChanges.Post;
			adoNetworkMaterial.Value := FPipeType;
		end;

		if adoNetworkAsBuilt.Value <> FAsBuilt then
		begin
			if not adoLinkChanges.Locate('LinkID;FieldName',VarArrayOf([adoNetworkLinkID.Value,adoNetworkAsBuilt.FieldName]),
				[loCaseInsensitive]) then
			begin
				adoLinkChanges.Append;
				adoLinkChanges.FieldByName('LinkID').Value := adoNetworkLinkID.Value;
				adoLinkChanges.FieldByName('FieldName').Value := adoNetworkAsBuilt.FieldName;
				adoLinkChanges.FieldByName('OldValue').Value := adoNetworkAsBuilt.Value;
			end
			else
				adoLinkChanges.Edit;
      adoLinkChanges.FieldByName('Changed').Value := True;
			adoLinkChanges.FieldByName('NewValue').Value := FAsBuilt;
			adoLinkChanges.FieldByName('Comment').Value := edtComment.Text;
      adoLinkChanges.FieldByName('UserName').Value := UserName;
      adoLinkChanges.FieldByName('UserTime').Value := SaveDateTime;
			adoLinkChanges.Post;
			adoNetworkAsBuilt.Value := FAsBuilt;
		end;

		if adoNetworkCompKey.Value <> FCompKey then
		begin
			if not adoLinkChanges.Locate('LinkID;FieldName',VarArrayOf([adoNetworkLinkID.Value,adoNetworkCompKey.FieldName]),
				[loCaseInsensitive]) then
			begin
				adoLinkChanges.Append;
				adoLinkChanges.FieldByName('LinkID').Value := adoNetworkLinkID.Value;
				adoLinkChanges.FieldByName('FieldName').Value := adoNetworkCompKey.FieldName;
				adoLinkChanges.FieldByName('OldValue').Value := adoNetworkCompKey.Value;
			end
			else
				adoLinkChanges.Edit;
      adoLinkChanges.FieldByName('Changed').Value := True;
			adoLinkChanges.FieldByName('NewValue').Value := FCompKey;
			adoLinkChanges.FieldByName('Comment').Value := edtComment.Text;
      adoLinkChanges.FieldByName('UserName').Value := UserName;
      adoLinkChanges.FieldByName('UserTime').Value := SaveDateTime;
			adoLinkChanges.Post;
			adoNetworkCompKey.Value := FCompKey;
		end;

    if adoNetworkDataQual.Value <> FDataSource then
    begin
    	if not adoLinkChanges.Locate('LinkID;FieldName', VarArrayOf([adoNetworkLinkID.Value,adoNetworkDataQual.FieldName]),
        [loCaseInsensitive]) then
      begin
        adoLinkChanges.Append;
				adoLinkChanges.FieldByName('LinkID').Value := adoNetworkLinkID.Value;
				adoLinkChanges.FieldByName('FieldName').Value := adoNetworkDataQual.FieldName;
				adoLinkChanges.FieldByName('OldValue').Value := adoNetworkDataQual.Value;
      end
      else
      	adoLinkChanges.Edit;
      adoLinkChanges.FieldByName('Changed').Value := True;
			adoLinkChanges.FieldByName('NewValue').Value := FDataSource;
			adoLinkChanges.FieldByName('Comment').Value := edtComment.Text;
      adoLinkChanges.FieldByName('UserName').Value := UserName;
      adoLinkChanges.FieldByName('UserTime').Value := SaveDateTime;
			adoLinkChanges.Post;
			adoNetworkDataQual.Value := FDataSource;
    end;

    if adoNetworkPipeFlowType.Value <> fFlowType then
    begin
    	if not adoLinkChanges.Locate('LinkID;FieldName', VarArrayOf([adoNetworkLinkID.Value,adoNetworkPipeFlowType.FieldName]),
        [loCaseInsensitive]) then
      begin
        adoLinkChanges.Append;
				adoLinkChanges.FieldByName('LinkID').Value := adoNetworkLinkID.Value;
				adoLinkChanges.FieldByName('FieldName').Value := adoNetworkPipeFlowType.FieldName;
				adoLinkChanges.FieldByName('OldValue').Value := adoNetworkPipeFlowType.Value;
      end
      else
      	adoLinkChanges.Edit;
      adoLinkChanges.FieldByName('Changed').Value := True;
			adoLinkChanges.FieldByName('NewValue').Value := FDataSource;
			adoLinkChanges.FieldByName('Comment').Value := edtComment.Text;
      adoLinkChanges.FieldByName('UserName').Value := UserName;
      adoLinkChanges.FieldByName('UserTime').Value := SaveDateTime;
			adoLinkChanges.Post;
			adoNetworkPipeFlowType.Value := fFlowType;
    end;

		adoNetwork.Post;
		adoNetwork.GotoBookmark(ABookMark);
		adoNetwork.FreeBookmark(ABookMark);
	end;
end;

procedure TConduit.SetAsBuilt(const Value: String);
begin
	if FAsBuilt <> Value then
		FAsBuilt := Value;
end;

procedure TConduit.SetCompKey(Value: Integer);
begin
	if FCompKey <> Value then
		FCompKey := Value;
end;

procedure TConduit.SetDnDepth(Value: Double);
begin
	if FDnDepth <> Value then
		FDnDepth := Value;
end;

procedure TConduit.SetDnGround(const Value: Double);
begin
	if FDnGround <> Value then
		FDnGround := Value;
end;

procedure TConduit.SetDnInvert(Value: Double);
begin
	if FDnInvert <> Value then
		FDnInvert := Value;
end;

procedure TConduit.SetDnType(const Value: String);
begin
	if FDnType <> Value then
		FDnType := Value;
end;

procedure TConduit.SetHorizDim(Value: Double);
begin
	if FHorizDim <> Value then
		FHorizDim := Value;
end;

procedure TConduit.SetLength(Value: Double);
begin
	if FLength <> Value then
		FLength := Value;
end;

procedure TConduit.SetLinkID(Value: Integer);
begin
  if FLinkID <> Value then
    FlinkID := Value;
end;

procedure TConduit.SetMapinfoID(const Value: Integer);
begin
	if FMapinfoID <> Value then
		FMapinfoID := Value;
end;

procedure TConduit.SetPipeType(const Value: String);
begin
  if FPipeType <> Value then
    FPipeType := Value;
end;

procedure TConduit.SetShape(const Value: String);
begin
  if FShape <> Value then
    FShape := Value;
end;

procedure TConduit.SetLinkType(const Value: String);
begin
	if FLinkType <> Value then
		FLinkType := Value;
end;

procedure TConduit.SetUpDepth(Value: Double);
begin
	if FUpDepth <> Value then
		FUpDepth := Value;
end;

procedure TConduit.SetUpGround(const Value: Double);
begin
	if FUpGround <> Value then
		FUpGround := Value;
end;

procedure TConduit.SetUpInvert(Value: Double);
begin
	if FUpInvert <> Value then
		FUpInvert := Value;
end;

procedure TConduit.SetUpType(const Value: String);
begin
	if FUpType <> Value then
		FUpType := Value;
end;

procedure TConduit.SetVertDim(Value: Double);
begin
	if FVertDim <> Value then
		FVertDim := Value;
end;



procedure TConduit.SetDataSource(const Value: String);
begin
	if FDataSource <> Value then
  	FDataSource := Value;
end;

procedure TConduit.SetDataSourceItem(APos: Integer; ACode: Char);
begin
	if FDataSource = '' then
  	FDataSource := '????????';
  FDataSource[APos] := ACode;
end;

procedure TConduit.SetFlowType(const Value: String);
begin
	if fFlowType <> Value then
  fFlowType := Value;
end;

procedure TConduit.SetIsSpecLink(const Value: Boolean);
begin
  if fIsSpecLink <> Value then
    fIsSpecLink := Value;
end;

{ TGrid }

function TGrid.CalcRulerInterval(Interval: Double): Double;
begin
	if IsZero(Interval) then
	begin
		Result := 1;
		Exit;
	end;

	Result := Power(10, Ceil(log10(Abs(Interval)))-1);
	if IsInfinite(Result) then
		Result := Interval;
	while Result > (Interval / 4) do
  begin
    Result := Result / 2;
	end;
end;

function TGrid.ClientToGrid(ARect: TRect): TDRectangle;
begin
	Result := TDRectangle.Create(ClientToGridX(ARect.Left), ClientToGridY(ARect.Top),
		ClientToGridX(ARect.Right), ClientToGridY(ARect. Bottom));
	Result.NormalizeRect;
end;

function TGrid.ClientToGrid(APoint: TPoint): TDPoint;
begin
	Result := DPoint(ClientToGridX(APoint.X), ClientToGridY(APoint.Y));
end;

function TGrid.ClientToGridX(XCoord: Integer): Double;
begin
	Result := XCoord * (1 / FZoom.X) + FMin.X;
end;

function TGrid.ClientToGridY(YCoord: Integer): Double;
begin
	Result := (frmMain.DrawBox.Height - YCoord - 1) * (1 / FZoom.Y) + FMin.Y;
end;

function TGrid.ClientUnits(LenX, LenY: Double): Integer;
begin
	LenX := Abs(LenX);
	LenY := Abs(LenY);
	if (LenX > MaxInt) or (LenY > MaxInt) then
		Result := MaxInt
	else
		Result := Round(Sqrt(Sqr(LenX * FZoom.X)+Sqr(LenY * FZoom.Y)));
end;

function TGrid.ClientUnits(Len: Double): Integer;
begin
	Len := Abs(Len);
	if Len > MaxInt then
		Result := MaxInt
	else
		Result := Round(Len * FZoom.X);
end;

constructor TGrid.Create;
begin
	inherited Create;
	FGridStyle := gsLines;
	FMin := DPoint(0.0, 0.0);
	FSpacing := DPoint(50.0, 50.0);
	FZoom := DPoint(1.0, 1.0);
	FColor := clInactiveCaption;
	FMinGridSize := 10;
	FVisible := True;
	FPrintGrid := False;
	FIsotropic := True;
end;

destructor TGrid.Destroy;
begin
	inherited;
end;

procedure TGrid.Draw(ACanvas: TCanvas; Clipper: TDRectangle);
var
	BackgroundRect: TRect;
	Frame: TRect;
	GridCountX: Double;
	GridCountY: Double;
	DrawX: Integer;
	DrawY: Integer;
	OldGridSpacing: TDPoint;
	OldFontSize: Integer;
	LogRec: TLogFont;
	OldFontHandle,
	NewFontHandle: HFont;
	DrawBox: TTransPanel;
begin
	BackgroundRect := Rect(GridToClientX(Clipper.Left), GridToClientY(Clipper.Top),
		GridToClientX(Clipper.Right), GridToClientY(Clipper.Bottom));
	{ Draw grid frame }
	DrawBox := frmMain.DrawBox;
	with ACanvas do
	begin
		Brush.Color := clWhite;
		Brush.Style := bsSolid;
		if PrintGrid then
		begin
			Brush.Color := clWhite;
			OldFontSize := Font.Size;
			Font.Size := 5;
		end;
		Pen.Mode := pmCopy;
		if PrintGrid then
		begin
			Font.Name := 'Arial';
			Font.Style := [fsBold, fsItalic];
			TextOut(GridToClientX(Clipper.Right)-TextWidth('Ft. from beginning of profile'),
				GridToClientY(Clipper.Top)+TextHeight('A'), 'Ft. from beginning of profile');

			GetObject(ACanvas.Font.Handle, SizeOf(LogRec), Addr(LogRec));
			LogRec.lfEscapement := 2700; // 2700 = 270 degrees rotation
			NewFontHandle := CreateFontIndirect(LogRec);
			OldFontHandle := SelectObject(ACanvas.Handle,NewFontHandle);

			TextOut(GridToClientX(Clipper.Left)+TextHeight('A'),
				GridToClientY(Clipper.Top)+TextHeight('A'), 'Ft. Elevation');

			NewFontHandle := SelectObject(ACanvas.Handle,OldFontHandle);
			DeleteObject(NewFontHandle);

			Frame := GridToClient(Clipper);
			Pen.Color := clBlack;
			Pen.Width := 5;
			//Rectangle(Frame);
			Polyline([Point(Frame.Left, Frame.Top), Point(Frame.Right, Frame.Top),
				Point(Frame.Right, Frame.Bottom), Point(Frame.Left, Frame.Bottom),
				Point(Frame.Left, Frame.Top)]);
		end;
		if not PrintGrid then
		begin
			FillRect(BackgroundRect);
			Frame := Rect(0, 0, DrawBox.Width, DrawBox.Height);
			Frame3D(ACanvas, Frame, clBtnHighlight, clBtnShadow, 1);
		end;
	end;

	{ Draw grid, if necessary }
	if FVisible then
	begin
		{ Adjust grid spacing temporarily, if necessary, to prevent crowded grid }
		OldGridSpacing := FSpacing;
		if HorizClientUnits(FSpacing.X) < 10 then
			FSpacing.X := (Trunc(GridUnits(10,0) / FSpacing.X) + 1) * FSpacing.X;
		if VertClientUnits(FSpacing.Y) < 10 then
			FSpacing.Y := (Trunc(GridUnits(10,0) / FSpacing.Y) + 1) * FSpacing.Y;

		{ Draw the grid }
		with ACanvas do
		begin
			Pen.Color := FColor;
			Pen.Mode := pmCopy;
      Pen.Width := 1;
			case FGridStyle of
				{ Lines grid style }
				gsLines :
					begin
						Pen.Style := psSolid;
						{ Draw the verticals }
						GridCountX := (Trunc(Clipper.Left / FSpacing.X)-1)* FSpacing.X;
            while GridCountX < Clipper.Left do
              GridCountX := GridCountX + FSpacing.X;
						while GridCountX <= Clipper.Right do
						begin
							DrawX := GridToClientX(GridCountX);
							if DrawX = 0 then
							begin
								GridCountX := GridCountX + FSpacing.X;
								Continue;
							end;
							if PrintGrid then
								TextOut(DrawX+10, GridToClientY(Clipper.Top), Format('%.0f', [GridCountX]));
							MoveTo(DrawX, GridToClientY(Clipper.Top));
							LineTo(DrawX, GridToClientY(Clipper.Bottom));
							GridCountX := GridCountX + FSpacing.X;
						end;
						{ Draw the horizontals }
						GridCountY := (Trunc(Clipper.Bottom / FSpacing.Y)-1)* FSpacing.Y;
            while GridCountY < Clipper.Bottom do
              GridCountY := GridCountY + FSpacing.Y;
						while GridCountY <= Clipper.Top do
						begin
							DrawY := GridToClientY(GridCountY);
							if DrawY = frmMain.DrawBox.Height-1 then
							begin
								GridCountY := GridCountY + FSpacing.Y;
								Continue;
							end;
							if PrintGrid then
								TextOut(GridToClientX(Clipper.Left)+TextHeight('A')+10, DrawY+10, Format('%.0f', [GridCountY]));
							MoveTo(GridToClientX(Clipper.Left), DrawY);
							LineTo(GridToClientX(Clipper.Right), DrawY);
							GridCountY := GridCountY + FSpacing.Y;
						end;
					end;
				{ Dots grid style }
				gsDotted :
					begin
						GridCountX := (Trunc(Clipper.Left / FSpacing.X)-1) * FSpacing.X;
            while GridCountX < Clipper.Left do
              GridCountX := GridCountX + FSpacing.X;
						while GridCountX <= Clipper.Right do
						begin
							DrawX := GridToClientX(GridCountX);
							if PrintGrid then
								TextOut(DrawX+10, GridToClientY(Clipper.Top), Format('%.0f', [GridCountX]));
							if DrawX = 0 then
							begin
								GridCountX := GridCountX + FSpacing.X;
								Continue;
							end;
							GridCountY := (Trunc(Clipper.Bottom / FSpacing.Y)-1) * FSpacing.Y;
              while GridCountY < Clipper.Bottom do
                GridCountY := GridCountY + FSpacing.Y;
							while GridCountY <= Clipper.Top do
							begin
								DrawY := GridToClientY(GridCountY);
								if PrintGrid then
									TextOut(GridToClientX(Clipper.Left)+TextHeight('A')+10, DrawY+10, Format('%.0f', [GridCountY]));
								if DrawY = frmMain.DrawBox.Height-1 then
								begin
									GridCountY := GridCountY + FSpacing.Y;
									Continue;
								end;
								MoveTo(DrawX, DrawY);
								LineTo(DrawX, DrawY - 1);
								GridCountY := GridCountY + FSpacing.Y;
							end;
							GridCountX := GridCountX + FSpacing.X;
						end;
					end;
				{ Alternating Dots grid style }
				gsAlternate :
					begin
{						GridCountX := (Trunc(Clipper.Left / FSpacing.X)-1) * FSpacing.X;
						while GridCountX <= Clipper.Right do
						begin
							DrawX := GridToClientX(GridCountX);
							if DrawX = 0 then
							begin
								GridCountX := GridCountX + FSpacing.X;
								Continue;
							end;
							DrawY := GridToClientY(Clipper.Top) div 2;
							if Odd(DrawY) then Dec(DrawY);
							while DrawY <= GridToClientY(Clipper.Bottom) do
							begin
								MoveTo (DrawX, DrawY);
								LineTo (DrawX, DrawY - 1);
								DrawY := DrawY + 2;
							end;
							GridCountX := GridCountX + FSpacing.X;
						end;
						GridCountY := (Trunc(Clipper.Bottom / FSpacing.Y)-1) * FSpacing.Y;
						while GridCountY <= (Clipper.Top + FSpacing.Y) do
						begin
							DrawY := GridToClientY(GridCountY);
							if DrawY = FView.DrawBox.Height-1 then
							begin
								GridCountY := GridCountY + FSpacing.Y;
								Continue;
							end;
							DrawX := GridToClientX(Clipper.Left) div 2;
							if Odd(DrawX) then Dec(DrawX);
							while DrawX <= GridToClientX(Clipper.Right) do
							begin
								MoveTo (DrawX, DrawY);
								LineTo (DrawX-1, DrawY);
								DrawX := DrawX + 2;
							end;
							GridCountY := GridCountY + FSpacing.Y;}
						{ Draw the verticals }
						GridCountX := (Trunc(Clipper.Left / FSpacing.X)-1)* FSpacing.X;
            while GridCountX < Clipper.Left do
              GridCountX := GridCountX + FSpacing.X;
						while GridCountX <= Clipper.Right do
						begin
							DrawX := GridToClientX(GridCountX);
							if PrintGrid then
								TextOut(DrawX+10, GridToClientY(Clipper.Top), Format('%.0f', [GridCountX]));
							if DrawX = 0 then
							begin
								GridCountX := GridCountX + FSpacing.X;
								Continue;
							end;
							DottedLine(ACanvas, DrawX, GridToClientY(Clipper.Top), DrawX,
								GridToClientY(Clipper.Bottom));
							GridCountX := GridCountX + FSpacing.X;
						end;
						{ Draw the horizontals }
						GridCountY := (Trunc(Clipper.Bottom / FSpacing.Y)-1)* FSpacing.Y;
            while GridCountY < Clipper.Bottom do
              GridCountY := GridCountY + FSpacing.Y;
						while GridCountY <= Clipper.Top do
						begin
							DrawY := GridToClientY(GridCountY);
							if PrintGrid then
								TextOut(GridToClientX(Clipper.Left)+TextHeight('A')+10, DrawY+10, Format('%.0f', [GridCountY]));
							if DrawY = frmMain.DrawBox.Height-1 then
							begin
								GridCountY := GridCountY + FSpacing.Y;
								Continue;
							end;
							DottedLine(ACanvas, GridToClientX(Clipper.Left), DrawY,
								GridToClientX(Clipper.Right), DrawY);
							GridCountY := GridCountY + FSpacing.Y;
						end;
					end;
			end;
		end;
		{ Reset the grid spacing }
		FSpacing := OldGridSpacing;
    if PrintGrid then
    ACanvas.Font.Size := OldFontSize;
	end;

	with ACanvas do
	begin
		Brush.Style := bsClear;
		Font.Name := 'Tahoma';
		Font.Size := 12;
		Font.Color := clBlack;
    if PrintGrid then
  		TextOut(GridToClientX(Clipper.Left)+TextWidth('M'),
        GridToClientY(Clipper.Bottom) - TextHeight('Ay') - 2, 'Downstream ->')
    else
  		TextOut(10, DrawBox.Height - TextHeight('Ay') - 2, 'Downstream ->');
	end;

end;

function TGrid.GetBoundsRect: TDRectangle;
begin
	Result := TDRectangle.Create(Min, Max);
end;

function TGrid.GetHeight: Double;
begin
	Result := GetMax.Y - FMin.Y;
end;

function TGrid.GetMax: TDPoint;
begin
	if FIsotropic then
		Result := DPoint(FMin.X+frmMain.DrawBox.Width/Zoom.X,
			FMin.Y+frmMain.DrawBox.Height/Zoom.X)
	else
		Result := DPoint(FMin.X+frmMain.DrawBox.Width/Zoom.X,
			FMin.Y+frmMain.DrawBox.Height/Zoom.Y);
end;

function TGrid.GetWidth: Double;
begin
	Result := GetMax.X - FMin.X;
end;

function TGrid.GridAspect: Double;
begin
	if FPrintGrid then
		Result := GridUnits(0,Abs(frmMain.PrintRect.Top-frmMain.PrintRect.Bottom)) /
			GridUnits(frmMain.DrawBox.Width,0)
	else
		Result := GridUnits(0,frmMain.DrawBox.Height) /
			GridUnits(frmMain.DrawBox.Width,0);
end;

procedure TfrmMain.ZoomExtents;
var
  LayerBounds: CMapXRectangle;
  MapAspect: Double;
begin
	LayerBounds := mapNetwork.Layers.Item(1).Bounds;
	MapAspect := mapNetwork.Height/mapNetwork.Width;
	if MapAspect < 1 then
		mapNetwork.ZoomTo((LayerBounds.YMax-LayerBounds.YMin)/MapAspect, (LayerBounds.XMax+LayerBounds.XMin)/2,
			(LayerBounds.YMax+LayerBounds.YMin)/2)
	else
		mapNetwork.ZoomTo(LayerBounds.XMax-LayerBounds.XMin, (LayerBounds.XMax+LayerBounds.XMin)/2,
			(LayerBounds.YMax+LayerBounds.YMin)/2);
	if Assigned(FPath) then
		FreeAndNil(FPath);
	DrawBox.Refresh;
end;

procedure TfrmMain.SetupMap(ADirectory: String);
var
  QualifiedNetworkTableName: String;
  TracedTheme: CMapXTheme;
  IndCatStyle: CMapXStyle;
  PipeFlowTypeTheme: CMapXTheme;
begin
	mnuStatusStatus.Caption := 'Opening MapInfo Layer...';
	mnuStatusProgress.Position := 0;
	Refresh;
	QualifiedNetworkTableName := ADirectory+'\links\'+NetworkTableName;
  try
	mapNetwork.Layers.Add(QualifiedNetworkTableName+'.tab',0);
  except
  end;
	mnuStatusStatus.Caption := 'Adding MapInfo Dataset...';
	mnuStatusProgress.Position := 25;
	Refresh;
	mapNetwork.Datasets.Add(miDatasetLayer, mapNetwork.Layers.Item(NetworkTableName), 'Network',
		EmptyParam, EmptyParam, EmptyParam, EmptyParam, EmptyParam);
	mapNetwork.Layers.Item(NetworkTableName).LabelProperties.Dataset :=
		mapNetwork.Datasets.Item('Network');
	mapNetwork.Layers.Item(NetworkTableName).LabelProperties.DataField :=
		mapNetwork.Datasets.Item('Network').Fields.Item('Compkey');

  try
		mapNetwork.Datasets.Item('Network').Themes.Add(miThemeIndividualValue, 'PipeFlowType',
			'By PipeFlowType', True);
		PipeFlowTypeTheme := mapNetwork.Datasets.Item('Network').Themes.Item('By PipeFlowType');
		IndCatStyle := PipeFlowTypeTheme.ThemeProperties.IndividualValueCategories.Item(1).Style;
    IndCatStyle.LineColor := clBlue;
    IndCatStyle := PipeFlowTypeTheme.ThemeProperties.IndividualValueCategories.Item(2).Style;
    IndCatStyle.LineColor := clGreen;
    IndCatStyle := PipeFlowTypeTheme.ThemeProperties.IndividualValueCategories.Item(3).Style;
    IndCatStyle.LineColor := clBlack;
    IndCatStyle := PipeFlowTypeTheme.ThemeProperties.IndividualValueCategories.Item(4).Style;
    IndCatStyle.LineColor := clRed;
  except
    on EOleException do ;
  end;

  try
  	try
	  	mapNetwork.Datasets.Add(miDatasetADO, adoTraced.Recordset, 'Traced', 'LinkID',
		  	EmptyParam, NetworkTableName, EmptyParam, EmptyParam);
    except
    	on EOleException do
      begin
				MessageDlg('Traced thematic not available; '+LinksName+' must have LinkID indexed. '+
					'Try reindexing mdl_links_ac fields or packing the table in MapInfo.',
          mtError, [mbOK], 0);
      end;
    end;
    mapNetwork.Datasets.Item('Traced').Themes.Add(miThemeIndividualValue, 'Traced',
      'By Traced', True);
    TracedTheme := mapNetwork.Datasets.Item('Traced').Themes.Item('By Traced');
		TraceItemsAvailable := True;
		IndCatStyle := TracedTheme.ThemeProperties.IndividualValueCategories.Item(1).Style;
		IndCatStyle.LineWidth := 2;
		IndCatStyle.LineStyle := 89;
	except
		on EOleException do
			TraceItemsAvailable := False;
	end;

	//"Oregon 3601, Northern Zone (1983, feet)", 3, 74, 3, -120.5, 43.6666666667, 44.3333333333, 46, 8202099.738, 0
	mapNetwork.NumericCoordSys.Set_(miLambertConformalConic,74,3,-120.5,43.6666666667,
		44.3333333333, 46, EmptyParam, EmptyParam, 8202099.738, 0,
		EmptyParam, EmptyParam, EmptyParam);
{		mnuStatusStatus.Caption := 'Preparing Thematic Map...';
	mnuStatusProgress.Position := 50;
	Refresh;
	mapNetwork.Datasets.Item(1).Themes.Add(miThemeIndividualValue, 'Pipetype', 'By Pipe Type',
		True);}
	mapNetwork.Layers.Item(NetworkTableName).ShowLineDirection := True;
	mapNetwork.Layers.Item(NetworkTableName).AutoLabel := True;
	mapNetwork.SelectionStyle.LineStyle := 90;
	mapNetwork.SelectionStyle.LineWidth := 1;
	mapNetwork.CurrentTool := miSelectTool;
	mapNetwork.MousePointer := miArrowCursor;
	mnuStatusStatus.Caption := mapNetwork.Datasets.Item(1).Name;
	mapNetwork.Refresh;
end;

procedure TfrmMain.CloseDBs;
begin
  adoLinksConnection.Close;
  adoNodesConnection.Close;
  adoChangesConnection.Close;
  adoLookupTablesConnection.Close;

  adoNetwork.Close;
  adoDownstreams.Close;
  adoUpstreams.Close;
  adoNodes.Close;
  adoDnNodes.Close;
  adoUpNodes.Close;
  adoTraced.Close;
  adoLinkChanges.Close;
  adoLinkDetailChanges.Close;
  adoDSNodeDetailChanges.Close;
  adoUSNodeDetailChanges.Close;
  adoNodeChanges.Close;
  adoDataSources.Close;
end;

procedure TfrmMain.OpenDBs;
begin
  adoLinksConnection.Open;
  adoNodesConnection.Open;
  adoChangesConnection.Open;
  adoLookupTablesConnection.Open;

  adoNetwork.Open;
  adoDownstreams.Open;
  adoUpstreams.Open;
  adoNodes.Open;
  adoDnNodes.Open;
  adoUpNodes.Open;
  adoTraced.Open;
  adoLinkChanges.Open;
  adoLinkDetailChanges.Open;
  adoDSNodeDetailChanges.Open;
  adoUSNodeDetailChanges.Open;
  adoNodeChanges.Open;
  adoDataSources.Open;
end;

function TGrid.GridToClient(ARect: TDRectangle): TRect;
begin
	Result := Rect(GridToClientX(ARect.Left), GridToClientY(ARect.Top),
		GridToClientX(ARect.Right), GridToClientY(ARect.Bottom));
	NormalizeRect(Result);
end;

function TGrid.GridToClient(APoint: TDPoint): TPoint;
begin
	Result := Types.Point(GridToClientX(APoint.X), GridToClientY(APoint.Y));
end;

function TGrid.GridToClientX(XCoord: Double): Integer;
begin
	Result := Round((XCoord - FMin.X) * FZoom.X);
end;

function TGrid.GridToClientY(YCoord: Double): Integer;
begin
	if FPrintGrid then
//		Result := Round(frmMain.PrintJob1.PageHeight - ((YCoord - FMin.Y) * FZoom.Y)) - 1
		Result := Round(Abs(frmMain.PrintRect.Top-frmMain.PrintRect.Bottom) - ((YCoord - FMin.Y) * FZoom.Y)) - 1
	else
		Result := Round(frmMain.DrawBox.Height - ((YCoord - FMin.Y) * FZoom.Y)) - 1;
end;

function TGrid.GridUnits(LenX, LenY: Integer): Double;
begin
	Result := Abs(Sqrt(Sqr(LenX / FZoom.X) + Sqr(LenY / FZoom.Y)));
end;

function TGrid.GridUnits(Len: Integer): Double;
begin
//	Result := Abs(Len / FZoom.X);
	// Can only call this function if grid is isotropic (1H:1V)
	raise Exception.Create('Illegal call to function TProfileGrid.GridUnits(x)');
end;

function TGrid.HorizClientUnits(Len: Double): Integer;
begin
	Len := Abs(Len);
	if Len > MaxInt then
		Result := MaxInt
	else
		Result := Round(Len * FZoom.X);
end;

function TGrid.HorizGridUnits(Len: Integer): Double;
begin
	Result := Abs(Len / FZoom.X);
end;

procedure TGrid.SetColor(Value: TColor);
begin
	if FColor <> Value then
	begin
		FColor := Value;
		UpdateGrid;
	end;
end;

procedure TGrid.SetGridStyle(Value: TGridStyle);
begin
	if FGridStyle <> Value then
	begin
		FGridStyle := Value;
		UpdateGrid;
	end;
end;

procedure TGrid.SetIsotropic(Value: Boolean);
begin
  if FIsotropic <> Value then
  begin
    FIsotropic := Value;
    FZoom.Y := FZoom.X;
  end;
end;

procedure TGrid.SetMin(Value: TDPoint);
begin
	if (FMin.X <> Value.X) or (FMin.Y <> Value.Y) then
	begin
		FMin := Value;
		UpdateGrid;
	end;
end;

procedure TGrid.SetMinGridSize(Value: Integer);
begin
	if FMinGridSize <> Value then
	begin
		FMinGridSize := Value;
		UpdateGrid;
	end;
end;

procedure TGrid.SetSnap(Value: TSnapSet);
begin
	if FSnap <> Value then
		FSnap := Value;
end;

procedure TGrid.SetSpacing(Value: TDPoint);
begin
	if (FSpacing.X <> Value.X) or (FSpacing.Y <> Value.Y) then
	begin
		FSpacing := Value;
		UpdateGrid;
	end;
end;

procedure TGrid.SetVisible(Value: Boolean);
begin
	if FVisible <> Value then
	begin
		FVisible := Value;
		UpdateGrid;
	end;
end;

procedure TGrid.SetZoom(Value: TDPoint);
begin
	if (FZoom.X <> Value.X) or (FZoom.Y <> Value.Y) then
	begin
    if FIsotropic then
      if FZoom.X <> Value.X then
      begin
        FZoom.X := Value.X;
        FZoom.Y := Value.X;
      end
      else
      begin
        FZoom.X := FZoom.Y;
				FZoom.Y := FZoom.Y;
      end
    else
  		FZoom := Value;
		UpdateGrid;
	end;
end;

procedure TGrid.UpdateGrid;
var
	UpdateRect: TRect;
begin
	UpdateRect := frmMain.DrawBox.ClientRect;
//	OffsetRect(UpdateRect, frmMain.DrawBox.Left, frmMain.DrawBox.Top);
	InvalidateRect(frmMain.DrawBox.Handle, @UpdateRect, False);
end;

function TGrid.VertClientUnits(Len: Double): Integer;
begin
	Len := Abs(Len);
	if Len > MaxInt then
		Result := MaxInt
	else
		Result := Round(Len * FZoom.Y);
end;

function TGrid.VertGridUnits(Len: Integer): Double;
begin
	Result := Abs(Len / FZoom.Y);
end;

{ TPath }

function TPath.AddDownstream: Boolean;
begin

end;

function TPath.AddDownstreamObj(AObj: Integer): Boolean;
begin
	FObjects.Add(TConduit.CreateFromDB(AObj));
end;

function TPath.AddUpstream: Boolean;
begin

end;

function TPath.AddUpstreamObj(AObj: Integer): Boolean;
begin
	FObjects.Insert(0, TConduit.CreateFromDB(AObj));
end;

constructor TPath.Create;
begin
	inherited;
	FObjects := TObjectList.Create(True);
	FInAdjacents := TObjectList.Create(True);
	FOutAdjacents := TObjectList.Create(True);
	FExpandedLinks := TObjectList.Create(False);
	FColor := clGray;
	FWidth := 5;
	FAlias := 'Path'
end;

function TPath.DeleteDownstreamObj: Boolean;
begin
	FObjects.Delete(FObjects.Count-1);
end;

function TPath.DeleteDownstreamOfObj(AObj: TModelObj): Boolean;
begin

end;

function TPath.DeleteUpstreamObj: Boolean;
begin
	FObjects.Delete(0);
end;

function TPath.DeleteUpstreamOfObj(AObj: TModelObj): Boolean;
begin

end;

destructor TPath.Destroy;
begin
	FObjects.Free;
	FInAdjacents.Free;
	FOutAdjacents.Free;
	FExpandedLinks.Free;
	inherited;
end;

procedure TPath.Draw(ACanvas: TCanvas; DrawStyle: TDrawStyleSet; IsPrinting: Boolean);
var
	ItemCounter: Integer;
	RotateFont: TLogFont;
	NewFont, OldFont: HFont;
	TextAngle: Double;
	DrawAngle: Double;
	DrawPt: TPoint;
	HeightAdjust, WidthAdjust: Integer;
	BkMode: Integer;
	DCX: Integer;
	Grid: TGrid;
  GridClipper: TDRectangle;
	DrawBox: TTransPanel;

	OldGridMin: TDPoint;
	OldGridZoom: TDPoint;
	DisplayGridHeight: Double;
	DisplayGridWidth: Double;

	PrinterAspect: Double;
  PrinterWidth: Integer;
  PrinterHeight: Integer;
  PrinterLeftMargin: Double;
  PrinterBottomMargin: Double;

	HorizPos: Double;

	procedure RestoreGrid;
	begin
		Grid.Min := OldGridMin;
		Grid.Zoom := OldGridZoom;
		Grid.PrintGrid := False;
	end;

begin
	Grid := frmMain.Grid;
	DrawBox := frmMain.DrawBox;

	DCX := DCMult(ACanvas.Handle);

	// Adjust page if we're printing
	if IsPrinting then
	begin
		OldGridMin := Grid.Min;
		OldGridZoom := Grid.Zoom;
    PrinterHeight := Abs(frmMain.PrintRect.Top-frmMain.PrintRect.Bottom);
    PrinterWidth := Abs(frmMain.PrintRect.Right-frmMain.PrintRect.Left);
		PrinterAspect := PrinterHeight/PrinterWidth;
		if PrinterAspect > Grid.GridAspect then // Printer is taller, fit to width
		begin
			DisplayGridHeight := Grid.GridUnits(0,DrawBox.Height);
			Grid.Zoom := DPoint(PrinterWidth/Grid.GridUnits(DrawBox.Width,0),
				Grid.Zoom.Y*(PrinterWidth/Grid.GridUnits(DrawBox.Width,0)/OldGridZoom.X));
      with frmMain.PrintJob1 do
      begin
        PrinterLeftMargin := Grid.HorizGridUnits(Trunc(InchToPix(Margins.Left, dirHorizontal)));
        PrinterBottomMargin := Grid.VertGridUnits(Trunc(InchToPix(Margins.Bottom, dirVertical)));
      end;
			Grid.Min := DPoint(Grid.Min.X - PrinterLeftMargin, Grid.Min.Y-
				(Grid.GridUnits(0,Trunc(PrinterHeight))-DisplayGridHeight)/2 + PrinterBottomMargin);
		end
		else // Printer is wider, fit to height
		begin
			DisplayGridWidth := Grid.GridUnits(DrawBox.Width,0);
			Grid.Zoom := DPoint(Grid.Zoom.X*(PrinterHeight/Grid.GridUnits(0,DrawBox.Height)/OldGridZoom.Y),
				PrinterHeight/Grid.GridUnits(0,DrawBox.Height));
      with frmMain.PrintJob1 do
      begin
        PrinterLeftMargin := Grid.HorizGridUnits(Trunc(InchToPix(Margins.Left, dirHorizontal)));
        PrinterBottomMargin := Grid.VertGridUnits(Trunc(InchToPix(Margins.Bottom, dirVertical)));
      end;
			Grid.Min := DPoint(Grid.Min.X-
				(Grid.GridUnits(Trunc(PrinterWidth),0)-DisplayGridWidth)/2 - PrinterLeftMargin,
				Grid.Min.Y + PrinterBottomMargin);
		end;
		Grid.PrintGrid := True;
    GridClipper := TDRectangle.Create(Grid.Min,
      DPoint(Grid.Min.X+PrinterWidth/Grid.Zoom.X,
        Grid.Min.Y+PrinterHeight/Grid.Zoom.Y));
    GridClipper.OffsetRect(PrinterLeftMargin, -PrinterBottomMargin);
    Grid.Draw(ACanvas, GridClipper);
    FreeAndNil(GridClipper);
	end;

	// Draw links
	for ItemCounter := 0 to FObjects.Count-1 do
	begin
		TDocObj(Objects[ItemCounter]).Draw(ACanvas, DrawStyle, IsPrinting);
	end;

	// Draw adjacents
	with ACanvas do
	begin
		for ItemCounter := 0 to FInAdjacents.Count-1 do
		begin
			if TAdjacent(FInAdjacents[ItemCounter]).Position = Objects.Count then
				HorizPos := LengthFromStart(TConduit(Objects[Objects.Count-1]))+TConduit(Objects[Objects.Count-1]).Length
			else
				HorizPos := LengthFromStart(TConduit(Objects[TAdjacent(FInAdjacents[ItemCounter]).Position]));
			if dsTemp in DrawStyle then
				Pen.Mode := pmNotXOR
			else
				Pen.Mode := pmCopy;
			Pen.Style := psSolid;
			Pen.Width := 1;
			Pen.Color := clMaroon;
			Ellipse(Grid.GridToClientX(HorizPos)-5*DCX, Grid.GridToClientY(TAdjacent(FInAdjacents[ItemCounter]).FConduit.DnCrown),
				Grid.GridToClientX(HorizPos)+5*DCX, Grid.GridToClientY(TAdjacent(FInAdjacents[ItemCounter]).FConduit.DnInvert));
			if not (dsTemp in DrawStyle) then
			begin
				Brush.Style := bsClear;
				Font.Name := 'Tahoma';
				Font.Size := 7;
				Font.Color := clMaroon;
				Font.Style := [];
				TextOut(Grid.GridToClientX(HorizPos),
					Grid.GridToClientY(TAdjacent(FInAdjacents[ItemCounter]).FConduit.DnCrown),
					Format('In %s-%s', [Trim(TAdjacent(FInAdjacents[ItemCounter]).FConduit.UpJunc),
						Trim(TAdjacent(FInAdjacents[ItemCounter]).FConduit.DnJunc)]));
			end;
		end;
		for ItemCounter := 0 to FOutAdjacents.Count-1 do
		begin
			if TAdjacent(FOutAdjacents[ItemCounter]).Position = Objects.Count then
				HorizPos := LengthFromStart(TConduit(Objects[Objects.Count-1]))+TConduit(Objects[Objects.Count-1]).Length
			else
				HorizPos := LengthFromStart(TConduit(Objects[TAdjacent(FOutAdjacents[ItemCounter]).Position]));
			if dsTemp in DrawStyle then
				Pen.Mode := pmNotXOR
			else
				Pen.Mode := pmCopy;
			Pen.Style := psSolid;
			Pen.Width := 1;
			Pen.Color := clNavy;
			Ellipse(Grid.GridToClientX(HorizPos)-5*DCX, Grid.GridToClientY(TAdjacent(FOutAdjacents[ItemCounter]).FConduit.UpCrown),
				Grid.GridToClientX(HorizPos)+5*DCX, Grid.GridToClientY(TAdjacent(FOutAdjacents[ItemCounter]).FConduit.UpInvert));
			if not (dsTemp in DrawStyle) then
			begin
				Brush.Style := bsClear;
				Font.Name := 'Tahoma';
				Font.Size := 7;
				Font.Color := clNavy;
				Font.Style := [];
				TextOut(Grid.GridToClientX(HorizPos),
					Grid.GridToClientY(TAdjacent(FOutAdjacents[ItemCounter]).FConduit.UpCrown),
					Format('Out %s-%s', [Trim(TAdjacent(FOutAdjacents[ItemCounter]).FConduit.UpJunc),
						Trim(TAdjacent(FOutAdjacents[ItemCounter]).FConduit.DnJunc)]));
			end;
		end;
	end;

	// Draw Selections
	for ItemCounter := 0 to frmMain.SelectedList.Count-1 do
		TDocObj(frmMain.Selected[ItemCounter]).Draw(ACanvas, [dsSelected], IsPrinting);

	if IsPrinting then
		RestoreGrid;
end;

procedure TPath.DrawMiniature(ACanvas: TCanvas; ARect: TRect);
begin

end;

procedure TPath.DrawTracker(ACanvas: TCanvas; TrackerState: TTrackerState);
begin
	inherited;

end;

function TPath.EnumAttachedProfiles: TObjectList;
begin

end;

function TPath.GetDownstreamAdjacents(AConduit: TConduit): TObjectList;
begin

end;

function TPath.GetExtents: TDRectangle;
begin

end;

function TPath.GetOtherAdjacents(AConduit: TConduit): TObjectList;
begin

end;

function TPath.GetPathDescription: String;
begin

end;

function TPath.GetUpstreamAdjacents(AConduit: TConduit): TObjectList;
begin

end;

function TPath.LengthBetweenObjects(UpObj, DnObj: TModelObj): Double;
var
	UpPos, DnPos: Integer;
begin
	UpPos := PosOfObj(UpObj);
	DnPos := PosOfObj(DnObj);
	if UpPos > DnPos then
		Swap(UpPos, DnPos);
	Result := LengthBetweenObjects(UpPos, DnPos);
end;

function TPath.LengthBetweenObjects(UpPos, DnPos: Integer): Double;
var
	i: Integer;
begin
	Result := -1;
	if (UpPos < 0) or (DnPos > FObjects.Count-1) then Exit;
	Result := 0;
	for i := UpPos to DnPos-1 do
		Result := Result + TConduit(FObjects[i]).Length;
end;

function TPath.LengthFromStart(AObj: TModelObj): Double;
var
	i: Integer;
begin
	Result := 0.0;
	for i := 0 to FObjects.Count-1 do
		if FObjects[i] = AObj then
			Exit
		else if FObjects[i] is TConduit then
			Result := Result + Abs(TConduit(FObjects[i]).Length)
		else if FObjects[i] is TLink then
			if FExpandedLinks.IndexOf(FObjects[i]) > -1 then
				Result := Result + NonConduitLinkLength;
end;

function TPath.NextHighestDnInvert(APos: Integer; AY: Double): Double;
var
	i: Integer;
begin
	if APos=Objects.Count-1 then
	begin
		Result := MaxInt;
		for i := 0 to FInAdjacents.Count-1 do
		begin
			if (TAdjacent(FInAdjacents[i]).Position = Objects.Count) then
				if (TAdjacent(FInAdjacents[i]).FConduit.DnInvert > AY) then
				begin
					Result := Min(Result, TAdjacent(FInAdjacents[i]).FConduit.DnInvert);
					frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s invert', [TAdjacent(FInAdjacents[i]).FConduit.CompKey]);
				end
				else if ((TAdjacent(FInAdjacents[i]).FConduit.DnInvert+TAdjacent(FInAdjacents[i]).FConduit.VertDim) > AY) then
				begin
					Result := Min(Result, TAdjacent(FInAdjacents[i]).FConduit.DnInvert+TAdjacent(FInAdjacents[i]).FConduit.VertDim-TConduit(Objects[APos]).VertDim);
					frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s crown', [TAdjacent(FInAdjacents[i]).FConduit.CompKey]);
				end;
		end;
		for i := 0 to FOutAdjacents.Count-1 do
		begin
			if (TAdjacent(FOutAdjacents[i]).Position = Objects.Count) then
				if (TAdjacent(FOutAdjacents[i]).FConduit.UpInvert > AY) then
				begin
					Result := Min(Result, TAdjacent(FOutAdjacents[i]).FConduit.UpInvert);
					frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s invert', [TAdjacent(FOutAdjacents[i]).FConduit.CompKey]);
				end
				else if ((TAdjacent(FOutAdjacents[i]).FConduit.UpInvert+TAdjacent(FOutAdjacents[i]).FConduit.VertDim) > AY) then
				begin
					Result := Min(Result, TAdjacent(FOutAdjacents[i]).FConduit.UpInvert+TAdjacent(FOutAdjacents[i]).FConduit.VertDim-TConduit(Objects[APos]).VertDim);
					frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s crown', [TAdjacent(FOutAdjacents[i]).FConduit.CompKey]);
				end;
		end;
		if Result > (MaxInt-1) then
		begin
			Result := AY;
			frmMain.mnuStatusStatus.Caption := 'No snap';
		end;
	end
	else
	begin
		Result := MaxInt;
		for i := 0 to FInAdjacents.Count-1 do
		begin
			if (TAdjacent(FInAdjacents[i]).Position = APos+1) then
				if (TAdjacent(FInAdjacents[i]).FConduit.DnInvert > AY) then
				begin
					Result := Min(Result, TAdjacent(FInAdjacents[i]).FConduit.DnInvert);
					frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s invert', [TAdjacent(FInAdjacents[i]).FConduit.CompKey]);
				end
				else if ((TAdjacent(FInAdjacents[i]).FConduit.DnInvert+TAdjacent(FInAdjacents[i]).FConduit.VertDim) > AY) then
				begin
					Result := Min(Result, TAdjacent(FInAdjacents[i]).FConduit.DnInvert+TAdjacent(FInAdjacents[i]).FConduit.VertDim-TConduit(Objects[APos]).VertDim);
					frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s crown', [TAdjacent(FInAdjacents[i]).FConduit.CompKey]);
				end;
		end;
		for i := 0 to FOutAdjacents.Count-1 do
		begin
			if (TAdjacent(FOutAdjacents[i]).Position = APos+1) then
				if (TAdjacent(FOutAdjacents[i]).FConduit.UpInvert > AY) then
				begin
					Result := Min(Result, TAdjacent(FOutAdjacents[i]).FConduit.UpInvert);
					frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s invert', [TAdjacent(FOutAdjacents[i]).FConduit.CompKey]);
				end
				else if ((TAdjacent(FOutAdjacents[i]).FConduit.UpInvert+TAdjacent(FOutAdjacents[i]).FConduit.VertDim) > AY) then
				begin
					Result := Min(Result, TAdjacent(FOutAdjacents[i]).FConduit.UpInvert+TAdjacent(FOutAdjacents[i]).FConduit.VertDim-TConduit(Objects[APos]).VertDim);
					frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s crown', [TAdjacent(FOutAdjacents[i]).FConduit.CompKey]);
				end;
		end;
		if (TConduit(Objects[APos+1]).UpInvert > AY) then
		begin
			Result := Min(Result, TConduit(Objects[APos+1]).UpInvert);
			frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s invert', [TConduit(Objects[APos+1]).CompKey]);
		end
		else if ((TConduit(Objects[APos+1]).UpInvert+TConduit(Objects[APos+1]).VertDim) > AY) then
		begin
			Result := Min(Result, TConduit(Objects[APos+1]).UpInvert+TConduit(Objects[APos+1]).VertDim-TConduit(Objects[APos]).VertDim);
			frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s crown', [TConduit(Objects[APos+1]).CompKey]);
		end;
		if Result >= (MaxInt-1) then
		begin
			Result := AY;
			frmMain.mnuStatusStatus.Caption := 'No snap';
		end;
	end
end;

function TPath.NextHighestUpInvert(APos: Integer; AY: Double): Double;
var
	i: Integer;
begin
	if APos=0 then
	begin
		Result := MaxInt;
		for i := 0 to FInAdjacents.Count-1 do
		begin
			if (TAdjacent(FInAdjacents[i]).Position = 0) then
				if (TAdjacent(FInAdjacents[i]).FConduit.DnInvert > AY) then
				begin
					Result := Min(Result, TAdjacent(FInAdjacents[i]).FConduit.DnInvert);
					frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s invert', [TAdjacent(FInAdjacents[i]).FConduit.CompKey]);
				end
				else if ((TAdjacent(FInAdjacents[i]).FConduit.DnInvert+TAdjacent(FInAdjacents[i]).FConduit.VertDim) > AY) then
				begin
					Result := Min(Result, TAdjacent(FInAdjacents[i]).FConduit.DnInvert+TAdjacent(FInAdjacents[i]).FConduit.VertDim-TConduit(Objects[APos]).VertDim);
					frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s crown', [TAdjacent(FInAdjacents[i]).FConduit.CompKey]);
				end;
		end;
		for i := 0 to FOutAdjacents.Count-1 do
		begin
			if (TAdjacent(FOutAdjacents[i]).Position = 0) then
				if (TAdjacent(FOutAdjacents[i]).FConduit.UpInvert > AY) then
				begin
					Result := Min(Result, TAdjacent(FOutAdjacents[i]).FConduit.UpInvert);
					frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s invert', [TAdjacent(FOutAdjacents[i]).FConduit.CompKey]);
				end
				else if ((TAdjacent(FOutAdjacents[i]).FConduit.UpInvert+TAdjacent(FOutAdjacents[i]).FConduit.VertDim) > AY) then
				begin
					Result := Min(Result, TAdjacent(FOutAdjacents[i]).FConduit.UpInvert+TAdjacent(FOutAdjacents[i]).FConduit.VertDim-TConduit(Objects[APos]).VertDim);
					frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s crown', [TAdjacent(FOutAdjacents[i]).FConduit.CompKey]);
				end;
		end;
		if Result > (MaxInt-1) then
		begin
			Result := AY;
			frmMain.mnuStatusStatus.Caption := 'No snap';
		end;
	end
	else
	begin
		Result := MaxInt;
		for i := 0 to FInAdjacents.Count-1 do
		begin
			if (TAdjacent(FInAdjacents[i]).Position = APos) then
				if (TAdjacent(FInAdjacents[i]).FConduit.DnInvert > AY) then
				begin
					Result := Min(Result, TAdjacent(FInAdjacents[i]).FConduit.DnInvert);
					frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s invert', [TAdjacent(FInAdjacents[i]).FConduit.CompKey]);
				end
				else if ((TAdjacent(FInAdjacents[i]).FConduit.DnInvert+TAdjacent(FInAdjacents[i]).FConduit.VertDim) > AY) then
				begin
					Result := Min(Result, TAdjacent(FInAdjacents[i]).FConduit.DnInvert+TAdjacent(FInAdjacents[i]).FConduit.VertDim-TConduit(Objects[APos]).VertDim);
					frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s crown', [TAdjacent(FInAdjacents[i]).FConduit.CompKey]);
				end;
		end;
		for i := 0 to FOutAdjacents.Count-1 do
		begin
			if (TAdjacent(FOutAdjacents[i]).Position = APos) then
				if (TAdjacent(FOutAdjacents[i]).FConduit.UpInvert > AY) then
				begin
					Result := Min(Result, TAdjacent(FOutAdjacents[i]).FConduit.UpInvert);
					frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s invert', [TAdjacent(FOutAdjacents[i]).FConduit.CompKey]);
				end
				else if ((TAdjacent(FOutAdjacents[i]).FConduit.UpInvert+TAdjacent(FOutAdjacents[i]).FConduit.VertDim) > AY) then
				begin
					Result := Min(Result, TAdjacent(FOutAdjacents[i]).FConduit.UpInvert+TAdjacent(FOutAdjacents[i]).FConduit.VertDim-TConduit(Objects[APos]).VertDim);
			frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s crown', [TConduit(Objects[APos+1]).CompKey]);
				end;
		end;
		if (TConduit(Objects[APos-1]).DnInvert > AY) then
		begin
			Result := Min(Result, TConduit(Objects[APos-1]).DnInvert);
			frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s invert', [TConduit(Objects[APos-1]).CompKey]);
		end
		else if ((TConduit(Objects[APos-1]).DnInvert+TConduit(Objects[APos-1]).VertDim) > AY) then
		begin
			Result := Min(Result, TConduit(Objects[APos-1]).DnInvert+TConduit(Objects[APos-1]).VertDim-TConduit(Objects[APos]).VertDim);
			frmMain.mnuStatusStatus.Caption := Format('Snapped to %d''s crown', [TConduit(Objects[APos-1]).CompKey]);
		end;
		if Result >= (MaxInt-1) then
		begin
			Result := AY;
			frmMain.mnuStatusStatus.Caption := 'No snap';
		end;
	end
end;

function TPath.NextLowestDnInvert(APos: Integer; AY: Double): Double;
var
	i: Integer;
begin
	if APos=Objects.Count-1 then
	begin
		Result := -MaxInt;
		for i := 0 to FInAdjacents.Count-1 do
		begin
			if (TAdjacent(FInAdjacents[i]).Position = Objects.Count) and
				(TAdjacent(FInAdjacents[i]).FConduit.DnInvert < AY) then
				Result := Max(Result, TAdjacent(FInAdjacents[i]).FConduit.DnInvert)
		end;
		for i := 0 to FOutAdjacents.Count-1 do
		begin
			if (TAdjacent(FOutAdjacents[i]).Position = Objects.Count) and
				(TAdjacent(FOutAdjacents[i]).FConduit.UpInvert < AY) then
				Result := Max(Result, TAdjacent(FOutAdjacents[i]).FConduit.UpInvert)
		end;
		if Result < (-MaxInt+1) then
			Result := AY;
	end
	else
	begin
		Result := -MaxInt;
		for i := 0 to FInAdjacents.Count-1 do
		begin
			if (TAdjacent(FInAdjacents[i]).Position = APos+1) and
				(TAdjacent(FInAdjacents[i]).FConduit.DnInvert < AY) then
				Result := Max(Result, TAdjacent(FInAdjacents[i]).FConduit.DnInvert)
		end;
		for i := 0 to FOutAdjacents.Count-1 do
		begin
			if (TAdjacent(FOutAdjacents[i]).Position = APos+1) and
				(TAdjacent(FOutAdjacents[i]).FConduit.UpInvert < AY) then
				Result := Max(Result, TAdjacent(FOutAdjacents[i]).FConduit.UpInvert)
		end;
		if TConduit(Objects[APos+1]).UpInvert < AY then
			Result := Max(Result, TConduit(Objects[APos+1]).UpInvert);
		if Result < -MaxInt+1 then
			Result := AY;
	end
end;

function TPath.NextLowestUpInvert(APos: Integer; AY: Double): Double;
var
	i: Integer;
begin
	if APos=0 then
	begin
		Result := -MaxInt;
		for i := 0 to FInAdjacents.Count-1 do
		begin
			if (TAdjacent(FInAdjacents[i]).Position = 0) and
				(TAdjacent(FInAdjacents[i]).FConduit.DnInvert < AY)then
				Result := Max(Result, TAdjacent(FInAdjacents[i]).FConduit.DnInvert)
		end;
		for i := 0 to FOutAdjacents.Count-1 do
		begin
			if (TAdjacent(FOutAdjacents[i]).Position = 0) and
				(TAdjacent(FOutAdjacents[i]).FConduit.UpInvert < AY) then
				Result := Max(Result, TAdjacent(FOutAdjacents[i]).FConduit.UpInvert)
		end;
		if Result < (-MaxInt+1) then
			Result := AY;
	end
	else
	begin
		Result := -MaxInt;
		for i := 0 to FInAdjacents.Count-1 do
		begin
			if (TAdjacent(FInAdjacents[i]).Position = APos) and
				(TAdjacent(FInAdjacents[i]).FConduit.DnInvert < AY) then
				Result := Max(Result, TAdjacent(FInAdjacents[i]).FConduit.DnInvert)
		end;
		for i := 0 to FOutAdjacents.Count-1 do
		begin
			if (TAdjacent(FOutAdjacents[i]).Position = APos) and
				(TAdjacent(FOutAdjacents[i]).FConduit.UpInvert < AY) then
				Result := Max(Result, TAdjacent(FOutAdjacents[i]).FConduit.UpInvert)
		end;
		if TConduit(Objects[APos-1]).DnInvert < AY then
			Result := Max(Result, TConduit(Objects[APos-1]).DnInvert);
		if Result < -MaxInt+1 then
			Result := AY;
	end
end;

function TPath.ObjectType: String;
begin
	Result := 'Path';
end;

function TPath.PosOfObj(AObj: TModelObj): Integer;
var
	i: Integer;
begin
	Result := -1;
	for i := 0 to FObjects.Count-1 do
		if FObjects[i] = AObj then
		begin
			Result := i;
			Break;
		end;
end;

procedure TPath.ProcessAdjacents;
var
	i: Integer;
	NextPathObj: TConduit;
	AnAdjacent: TAdjacent;
	DownstreamsRecNo: Integer;
	UpstreamsRecNo: Integer;
begin
	OutAdjacents.Clear;
	InAdjacents.Clear;
	with frmMain do
	begin
		for i := 0 to Objects.Count-1 do
		begin
			mnuStatusProgress.Position := i+1;
			Application.ProcessMessages;

			adoNetwork.Locate('LinkID', TConduit(Objects[i]).LinkID, [loCaseInsensitive]);

			// Grab outgoing adjacents
			if i < Objects.Count-1 then
				NextPathObj := TConduit(Objects[i+1])
			else
				NextPathObj := nil;
			if (adoDownstreams.RecordCount > 1) or (i = Objects.Count-1) then
			begin
				adoDownstreams.First;
				while not adoDownstreams.Eof do
				begin
					if (Assigned(NextPathObj) and (adoDownstreamsLinkID.Value <> NextPathObj.LinkID)) or
						(not Assigned(NextPathObj)) then
					begin
						AnAdjacent := TAdjacent.Create;
						AnAdjacent.Position := i+1;
						OutAdjacents.Add(AnAdjacent);
						DownstreamsRecNo := adoDownstreams.RecNo;
						AnAdjacent.FConduit := TConduit.CreateFromDB(adoDownstreamsLinkID.Value);
						adoDownstreams.RecNo := DownstreamsRecNo;
					end;
					adoDownstreams.Next;
				end;
			end;

			// Grab incoming adjacents
			if i = 0 then
				NextPathObj := nil
			else
				NextPathObj := TConduit(Objects[i-1]);
			if (adoUpstreams.RecordCount > 1) or (i = 0) then
			begin
				adoUpstreams.First;
				while not adoUpstreams.Eof do
				begin
					if (Assigned(NextPathObj) and (adoUpstreamsLinkID.Value <> NextPathObj.LinkID)) or
						(not Assigned(NextPathObj)) then
					begin
						AnAdjacent := TAdjacent.Create;
						AnAdjacent.Position := i;
						InAdjacents.Add(AnAdjacent);
						UpstreamsRecNo := adoUpstreams.RecNo;
						AnAdjacent.FConduit := TConduit.CreateFromDB(adoUpstreamsLinkID.Value);
						adoUpstreams.RecNo := UpstreamsRecNo;
					end;
					adoUpstreams.Next;
				end;
			end;
		end;
	end; // with frmMain
end;

procedure TPath.Reslope(NewSlope: Double; Anchor: TModelObj;
	KeepOffsets: Boolean);
begin

end;

procedure TPath.SetColor(Value: TColor);
begin

end;

procedure TPath.SetExpandedLinks(Value: TObjectList);
begin

end;

procedure TPath.SetObjects(Value: TObjectList);
begin

end;

procedure TPath.SetWidth(Value: Integer);
begin

end;

procedure TPath.ShiftVert(Offset: Double);
begin

end;

function TPath.Sort: Boolean;
begin

end;

procedure TPath.Straighten;
begin

end;

{ TGraphicalViewTool }

procedure TGraphicalViewTool.ConstructContextMenu(Sender: TObject;
  HitObj: TDocObj);
begin

end;

procedure TGraphicalViewTool.ContextMenuClick(Sender: TObject);
begin

end;

constructor TGraphicalViewTool.Create;
begin
	inherited;
end;

procedure TGraphicalViewTool.DblClick;
begin

end;

procedure TGraphicalViewTool.MouseDown(Sender: TObject;
	Button: TMouseButton; Shift: TShiftState; X, Y: Integer);
var
	HitPt: TDPoint;
	DragRect: TRect;
	Grid: TGrid;
begin
	FDragging := True;
	Grid := frmMain.Grid;

	// Was an object hit?
	with Sender as TTransPanel do
	begin
		HitPt := Grid.ClientToGrid(Types.Point(X, Y));
		FHitObj := frmMain.ObjectAt(HitPt);
		if not Assigned(FHitObj) then
			FHitObj := frmMain.ObjectAt(HitPt);
	end;

	// Construct context menu if necessary
	if Button = mbRight then
	begin
		FRight := True;
		ConstructContextMenu(Sender, FHitObj);
	end
	else
		FRight := False;

	// Snap modifications
	with Sender as TTransPanel do
		if gsGrid in Grid.Snap then
			with Grid do
				FOriginPt := GridToClient(DPoint(Round(ClientToGridX(X)/Spacing.X)*Spacing.X,
					Round(ClientToGridY(Y)/Spacing.Y) * Spacing.Y))
		else
			FOriginPt := Types.Point(X, Y);

	FDragPt := FOriginPt;

	// Draw rubberband rectangle if necessary
	if DrawDragRect then
	begin
		DragRect := Rect(FOriginPt.x, FOriginPt.y, FDragPt.x, FDragPt.y);
		NormalizeRect(DragRect);
		with Sender as TTransPanel, Canvas do
		begin
			Brush.Style := bsSolid;
			DrawFocusRect(DragRect);
		end;    // with
	end;

	FButton := Button;
	FShift := Shift;
end;

procedure TGraphicalViewTool.MouseMove(Sender: TObject; Shift: TShiftState;
	X, Y: Integer);
var
	DragRect: TRect;
	ModifX, ModifY: Integer;
	StatusRect: TRect;
	Grid: TGrid;
begin
	Grid := frmMain.Grid;

	with Sender as TTransPanel, Grid do
	begin
		frmMain.mnuStatusPosition.Caption := Format('%.0f,%.0f', [ClientToGridX(X),
			ClientToGridY(Y)]);
		if FDragging then
		begin
			frmMain.mnuStatusSize.Caption := Format('%.0fx%.0f', [HorizGridUnits(DragPt.X - OriginPt.X),
				VertGridUnits(DragPt.Y - OriginPt.Y)]);
		end
		else
			frmMain.mnuStatusSize.Caption := '';
{		if FDragging then
			StatusRect := sbrStatus.ClientRect
		else
		begin
			StatusRect.TopLeft := spanePosition.ClientRect.TopLeft;
			StatusRect.BottomRight := spaneSize.ClientRect.BottomRight;
		end;
		InvalidateRect(sbrStatus.Handle, @StatusRect, False);
		sbrStatus.Update;}
	end;
	if FDragging then
	begin
		// Draw old rubberband rectangle if necessary
		if DrawDragRect then
		begin
			DragRect := Rect(FOriginPt.x, FOriginPt.y, FDragPt.x, FDragPt.y);
			NormalizeRect(DragRect);
			(Sender as TTransPanel).Canvas.DrawFocusRect(DragRect);
		end;

		ModifX := X;
		ModifY := Y;
		if ssCtrl in Shift then // Constrain to square if Ctrl is down
		begin
			if Abs(X-FOriginPt.X) < Abs(Y-FOriginPt.Y) then
				ModifY := FOriginPt.Y + (X-FOriginPt.X)
			else
				ModifX := FOriginPt.X + (Y-FOriginPt.Y)
		end;
		FDragPt := Types.Point(ModifX, ModifY);

		// Snap modifications
		with Sender as TTransPanel do
			if gsGrid in Grid.Snap then
				with Grid do
					DragPt := GridToClient(DPoint(Round(ClientToGridX(X)/Spacing.X)*Spacing.X,
						Round(ClientToGridY(Y)/Spacing.Y) * Spacing.Y))
			else
				DragPt := Types.Point(X, Y);

		// Draw new rubberband rectangle if necessary
		if DrawDragRect then
		begin
			DragRect := Rect(FOriginPt.x, FOriginPt.y, FDragPt.x, FDragPt.y);
			NormalizeRect(DragRect);
			(Sender as TTransPanel).Canvas.DrawFocusRect(DragRect);
		end;
	end;

	FButton := Button;
	FShift := Shift;
end;

procedure TGraphicalViewTool.MouseUp(Sender: TObject; Button: TMouseButton;
	Shift: TShiftState; X, Y: Integer);
var
	DragRect: TRect;
	ModifX, ModifY: Integer;
	StatusRect: TRect;
	Grid: TGrid;
begin
	Grid := frmMain.Grid;

	if FDragging then
	begin

		// Draw old rubberband rectangle if necessary
		if DrawDragRect then
		begin
			DragRect := Rect(FOriginPt.x, FOriginPt.y, FDragPt.x, FDragPt.y);
			NormalizeRect(DragRect);
			(Sender as TTransPanel).Canvas.DrawFocusRect(DragRect);
		end;

		ModifX := X;
		ModifY := Y;
		if ssCtrl in Shift then // Constrain to square if Ctrl is down
		begin
			if Abs(X-FOriginPt.X) < Abs(Y-FOriginPt.Y) then
				ModifY := FOriginPt.Y + (X-FOriginPt.X)
			else
				ModifX := FOriginPt.X + (Y-FOriginPt.Y)
		end;
		DragPt := Types.Point(ModifX, ModifY);

		// Snap modifications
		with Sender as TTransPanel do
			if gsGrid in Grid.Snap then
				with Grid do
					DragPt := GridToClient(DPoint(Round(ClientToGridX(X)/Spacing.X)*Spacing.X,
						Round(ClientToGridY(Y)/Spacing.Y) * Spacing.Y))
			else
				DragPt := Types.Point(X, Y);

		FDragging := False;
	end;

	FButton := Button;
	FShift := Shift;
end;

{ TPanTool }

procedure TPanTool.DblClick;
begin
	inherited;

end;

function TPanTool.GetToolName: String;
begin
  Result := 'Pan'
end;

procedure TPanTool.MouseDown(Sender: TObject; Button: TMouseButton;
	Shift: TShiftState; X, Y: Integer);
var
	Grid: TGrid;
begin
	Grid := frmMain.Grid;

	DrawDragRect := False;
	inherited MouseDown(Sender, Button, Shift, X, Y);

	if Right then
		Exit;

	with Sender as TTransPanel, Grid do
	begin
		OldMinPt := Min;
		GridWasVisible := Visible;
		Visible := False;
	end;
end;

procedure TPanTool.MouseMove(Sender: TObject; Shift: TShiftState; X,
	Y: Integer);
var
	Grid: TGrid;
begin
	Grid := frmMain.Grid;

	inherited MouseMove(Sender, Shift, X, Y);

	if Right then
		Exit;

	if FDragging then
	begin
		with Sender as TTransPanel, Grid do
			Min := DPoint(OldMinPt.X - (ClientToGridX(FDragPt.X) - ClientToGridX(FOriginPt.X)),
				OldMinPt.Y - (ClientToGridY(FDragPt.Y) - ClientToGridY(FOriginPt.Y)));
		with Sender as TTransPanel do
		begin
			frmMain.rulHoriz.Min := Grid.Min.X;
			frmMain.rulVert.Min := Grid.Min.Y;
		end;
	end;
end;

procedure TPanTool.MouseUp(Sender: TObject; Button: TMouseButton;
	Shift: TShiftState; X, Y: Integer);
var
	OldRect: TDRectangle;
	Grid: TGrid;
begin
	Grid := frmMain.Grid;

	inherited MouseUp(Sender, Button, Shift, X, Y);

	if Right then
		Exit;

	if FDragging then
	begin
		with Sender as TTransPanel, Grid do
			Min := DPoint(OldMinPt.X - (ClientToGridX(FDragPt.X) - ClientToGridX(FOriginPt.X)),
				OldMinPt.Y - (ClientToGridY(FDragPt.Y) - ClientToGridY(FOriginPt.Y)));
		with Sender as TTransPanel do
		begin
			frmMain.rulHoriz.Min := Grid.Min.X;
			frmMain.rulVert.Min := Grid.Min.Y;
		end;
	end;

	with Sender as TTransPanel do
	begin
		Grid.Visible := GridWasVisible;
//		DrawSimple := WasSimple;
	end;

	frmMain.ViewChanged;
end;

{ TZoomTool }

procedure TZoomTool.DblClick;
begin
	inherited;
end;

function TZoomTool.GetToolName: String;
begin
	Result := 'Zoom';
end;

procedure TZoomTool.MouseDown(Sender: TObject; Button: TMouseButton;
	Shift: TShiftState; X, Y: Integer);
begin
	DrawDragRect := True;
	inherited;
end;

procedure TZoomTool.MouseMove(Sender: TObject; Shift: TShiftState; X,
	Y: Integer);
begin
	if not Dragging then
	begin
		inherited MouseMove(Sender, Shift, X, Y);
		Exit;
	end
	else
		if (ssShift in Shift) or Right then
			inherited MouseMove(Sender, Shift - [ssShift], X, Y)
		else
			inherited MouseMove(Sender, Shift, X, Y);
end;

procedure TZoomTool.MouseUp(Sender: TObject; Button: TMouseButton;
	Shift: TShiftState; X, Y: Integer);
var
	MarqueeRect, StatusRect: TRect;
	NewAspectRatio, CurrentAspectRatio: Double;
	MarqueeHeight, MarqueeWidth: Integer;
	CHeight, CWidth: Integer; // Paintbox height and width
	FromRect, ToRect, DrawRect: TRect;
	FromDRect, ToDRect, DrawDRect, UpdateRect: TDRectangle;
	OldZoom, NewZoom: TDPoint;
	OldMin, NewMin: TDPoint;
	WidthStep, HeightStep: Double;
	RefreshRect: TRect;
	NewRect: TRect;
	OldRect: TDRectangle;
	GridWasVisible: Boolean;
	WasSimple: Boolean;
	Grid: TGrid;
	DrawBox: TTransPanel;

	procedure AnimateZoom(FromRect, ToRect: TRect);
	var
		AnimateCount: Integer;
	begin
		with Sender as TTransPanel, Grid do
		begin
			GridWasVisible := Visible;
			WasSimple := frmMain.DrawSimple;

			NewMin := Min;
			NewZoom := Zoom;
			// Revert for correct
			Min := OldMin;
			Zoom := OldZoom;

			WidthStep := ((ToRect.Right-ToRect.Left)-(FromRect.Right-FromRect.Left))/AnimateSteps;
			HeightStep := ((ToRect.Bottom-ToRect.Top)-(FromRect.Bottom-FromRect.Top))/AnimateSteps;
			FromDRect := TDRectangle.Create(ClientToGrid(FromRect.TopLeft), ClientToGrid(FromRect.BottomRight));
			ToDRect := TDRectangle.Create(ClientToGrid(ToRect.TopLeft), ClientToGrid(ToRect.BottomRight));
			DrawDRect := TDRectangle.Create;
			Grid.Visible := False;
			frmMain.DrawSimple := True;
		end;

		for AnimateCount := 1 to AnimateSteps do
		begin
			with Sender as TTransPanel, frmMain, Grid do
			begin
				DrawDRect.SetRect(FromDRect.Left+(ToDRect.Left-FromDRect.Left)/AnimateSteps*AnimateCount,
					FromDRect.Top+(ToDRect.Top-FromDRect.Top)/AnimateSteps*AnimateCount,
					FromDRect.Right+(ToDRect.Right-FromDRect.Right)/AnimateSteps*AnimateCount,
					FromDRect.Bottom+(ToDRect.Bottom-FromDRect.Bottom)/AnimateSteps*AnimateCount);
				with Sender as TTransPanel, frmMain do
				begin
					FitWindow(DrawDRect);
					with Grid do
					begin
						RulVert.Min := Min.Y;
						RulVert.Zoom := Zoom.Y;
						try
							rulVert.Spacing := CalcRulerInterval(Max.Y-Min.Y);
						except
							on EZeroDivide do // Zero for Min.Y
								rulVert.Spacing := 10;
						end;
						RulHoriz.Min := Min.X;
						RulHoriz.Zoom := Zoom.X;
						try
							rulHoriz.Spacing := CalcRulerInterval(Max.X-Min.X);
						except
							on EZeroDivide do // Zero for Min.X
								rulHoriz.Spacing := 10;
						end;
					end;
				end;
				frmMain.Update;
				frmMain.DrawBox.Update;
			end;
			Delay(0.001);
		end;
		FromDRect.Free;
		ToDRect.Free;
		DrawDRect.Free;
		with Sender as TTransPanel, Grid do
		begin
			Visible := GridWasVisible;
			frmMain.DrawSimple := WasSimple;
		end;
	end;

begin
	Grid := frmMain.Grid;
	DrawBox := frmMain.DrawBox;

	if not Dragging then
	begin
		inherited MouseUp(Sender, Button, Shift, X, Y);
		Exit;
	end;

	with Sender as TTransPanel do
	begin
		OldZoom := Grid.Zoom;
		OldMin := Grid.Min;
		CHeight := DrawBox.Height;
		CWidth := DrawBox.Width;
		inherited MouseUp(Sender, Button, Shift, X, Y);
		MarqueeRect := RectFromPts(OriginPt, DragPt);
		NormalizeRect(MarqueeRect);

		MarqueeHeight := Abs(MarqueeRect.Top - MarqueeRect.Bottom);
		MarqueeWidth := Abs(MarqueeRect.Right - MarqueeRect.Left);
		if MarqueeWidth > 0 then
			NewAspectRatio := MarqueeHeight / MarqueeWidth
		else
			NewAspectRatio := 0;
		CurrentAspectRatio := CHeight / CWidth;

		// Click!
		UpdateRect := TDRectangle.Create;
		if (MarqueeHeight = 0) and (MarqueeWidth = 0) then
		begin
			if ((not ZoomOut) and ((ssShift in Shift) or Right)) or
				(ZoomOut and not ((ssShift in Shift) or Right)) then { Zoom out }
			begin
				with Grid do
				begin
					UpdateRect.SetRect(ClientToGridX(OriginPt.X-CWidth),
						ClientToGridY(OriginPt.Y-CHeight), ClientToGridX(OriginPt.X+CWidth),
						ClientToGridY(OriginPt.Y+CHeight));
					frmMain.FitWindow(UpdateRect);
				end;
			end
			else { Zoom in }
			begin
				with Grid do
				begin
					UpdateRect.SetRect(ClientToGridX(MarqueeRect.Left-CWidth div 4),
						ClientToGridY(MarqueeRect.Top-CHeight div 4),
						ClientToGridX(MarqueeRect.Left+CWidth div 4),
						ClientToGridY(MarqueeRect.Top+CHeight div 4));
					frmMain.FitWindow(UpdateRect);
				end;
			end;
//			frmMain.ToolHint := 'Click: zoom 2x; Drag: fit window to marquee; '+
//				'Use right button or hold down shift to zoom out';
//			frmMain.ShowHint(Sender);
			with frmMain, Grid do
			begin
				rulHoriz.Min := Min.X;
				rulHoriz.Zoom := Zoom.X;
				try
					rulHoriz.Spacing := CalcRulerInterval(Max.X-Min.X);
				except
					on EZeroDivide do // Zero for Min.X
						rulHoriz.Spacing := 10;
				end;
				rulVert.Min := Min.Y;
				rulVert.Zoom := Zoom.Y;
				try
					rulVert.Spacing := CalcRulerInterval(Max.Y-Min.Y);
				except
					on EZeroDivide do // Zero for Min.Y
						rulVert.Spacing := 10;
				end;
			end;

			if ((not ZoomOut) and ((ssShift in Shift) or Right)) or
				(ZoomOut and not ((ssShift in Shift) or Right)) then { Zoom out }
				AnimateZoom(Rect(0,0,CWidth, CHeight), Rect(OriginPt.X-CWidth,
					OriginPt.Y-CHeight, OriginPt.X+CWidth, OriginPt.Y+CHeight))
			else
			begin
				AnimateZoom(Rect(0,0,CWidth, CHeight), Rect(MarqueeRect.Left-CWidth div 4,
					MarqueeRect.Top-CHeight div 4, MarqueeRect.Left+CWidth div 4,
					MarqueeRect.Top+CHeight div 4));
			end;
			UpdateRect.Free;

			with Grid do
			begin
				OldRect := TDRectangle.Create(OldMin, DPoint(OldMin.X+DrawBox.Width/OldZoom.X,
					OldMin.Y+DrawBox.Height/OldZoom.Y));
				OldRect.Free;
			end;

			frmMain.ViewChanged;
			Exit;
		end;

		// Marquee!
		try
			if Grid.Isotropic then
			begin
				if ((not ZoomOut) and ((ssShift in Shift) or Right)) or
					(ZoomOut and not ((ssShift in Shift) or Right)) then { Zoom out }
				begin
					if NewAspectRatio > CurrentAspectRatio then { Fit to height }
						with Grid do
						begin
							Min := DPoint((Min.X - (MarqueeRect.Left)/(Zoom.X*(MarqueeHeight/CHeight))),
								(Min.Y - (CHeight - MarqueeRect.Bottom)/(Zoom.Y*(MarqueeHeight/CHeight))));
							Zoom := DPoint(Zoom.X*(MarqueeHeight/CHeight), Zoom.Y*MarqueeHeight/CHeight);
						end
					else { Fit to width }
						with Grid do
						begin
							Min := DPoint((Min.X -
								(MarqueeRect.Left)/(Zoom.X*(MarqueeWidth/CWidth))),
								(Min.Y - (CHeight - MarqueeRect.Bottom)/(Zoom.Y*(MarqueeHeight/CHeight))));
							Zoom := DPoint(Zoom.X*MarqueeWidth/CWidth, Zoom.Y*MarqueeHeight/CHeight);
						end;
				end
				else { Zoom in }
				begin
					if NewAspectRatio > CurrentAspectRatio then { Fit to height }
						with Grid do
						begin
							Min := DPoint(ClientToGridX(MarqueeRect.Left), ClientToGridY(MarqueeRect.Bottom));
							Zoom := DPoint(CWidth/GridUnits(MarqueeWidth,0),
								CHeight/GridUnits(0,MarqueeHeight));
						end
					else { Fit to width }
					begin
						if NewAspectRatio = 0 then
							MarqueeWidth := 1;
						with Grid do
						begin
							Min := DPoint(ClientToGridX(MarqueeRect.Left), ClientToGridY(MarqueeRect.Bottom));
							Zoom := DPoint(CWidth/GridUnits(MarqueeWidth,0),
								CHeight/GridUnits(0,MarqueeHeight));
						end;
					end;
				end;
			end
			else
			begin
				if ((not ZoomOut) and ((ssShift in Shift) or Right)) or
					(ZoomOut and not ((ssShift in Shift) or Right)) then { Zoom out }
				begin
					with Grid do
					begin
						Min := DPoint(Min.X-(MarqueeRect.Left)/(Zoom.X*(MarqueeWidth/CWidth)),
							(Min.Y-(CHeight - MarqueeRect.Bottom)/(Zoom.Y*(MarqueeHeight/CHeight))));
						Zoom := DPoint(Zoom.X*(MarqueeWidth/CWidth), Zoom.Y*MarqueeHeight/CHeight);
					end
				end
				else { Zoom in }
				begin
					with Grid do
					begin
						Min := DPoint(ClientToGridX(MarqueeRect.Left), ClientToGridY(MarqueeRect.Bottom));
						Zoom := DPoint(CWidth/GridUnits(MarqueeWidth,0),
							CHeight/GridUnits(0,MarqueeHeight));
					end
				end;
			end;
		except
			on EZeroDivide do
			begin
				Grid.Min := OldMin;
				Grid.Zoom := OldZoom;
			end;
		end;
	end;

//	frmMain.ToolHint := 'Click: zoom 2x; Drag: fit window to marquee; '+
//		'Use right button or hold down shift to zoom out';
//	frmMain.ShowHint(Sender);

	with frmMain, Grid do
	begin
		rulHoriz.Min := Min.X;
		rulHoriz.Zoom := Zoom.X;
		try
			rulHoriz.Spacing := CalcRulerInterval(Max.X-Min.X);
		except
			on EZeroDivide do // Zero for Min.X
				rulHoriz.Spacing := 10;
		end;
		rulVert.Min := Min.Y;
		rulVert.Zoom := Zoom.Y;
		try
			rulVert.Spacing := CalcRulerInterval(Max.Y-Min.Y);
		except
			on EZeroDivide do // Zero for Min.Y
				rulVert.Spacing := 10;
		end;
	end;

	if ((not ZoomOut) and ((ssShift in Shift) or Right)) or
		(ZoomOut and not ((ssShift in Shift) or Right)) then { Zoom out }
		with Grid do
		begin
			NewMin := Min;
			NewZoom := Zoom;
			Min := OldMin;
			Zoom := OldZoom;
			NewRect := Rect(GridToClientX(NewMin.X), GridToClientY(NewMin.Y+CHeight/NewZoom.Y),
				GridToClientX(NewMin.X+CWidth/NewZoom.X), GridToClientY(NewMin.Y));
			Min := NewMin;
			Zoom := NewZoom;
			AnimateZoom(Rect(0,0,CWidth,CHeight), NewRect)
		end
	else
		AnimateZoom(Rect(0,0,CWidth,CHeight), MarqueeRect);

	frmMain.ViewChanged;
end;

{ TProfileSelectTool }

procedure TProfileSelectTool.ClearAnchors;
var
	ListCounter: Integer;
begin
	for ListCounter := AnchorPos.Count-1 downto 0 do
		TObject(AnchorPos[ListCounter]).Free;
	AnchorPos.Clear;
end;

procedure TProfileSelectTool.ConstructContextMenu(Sender: TObject;
	HitObj: TDocObj);
var
	Grid: TGrid;
	Popup: TdxBarPopupMenu;
	Button: TdxBarButton;
	ProfilePath: TPath;
	OrderInPath: Integer;
begin
	Grid := frmMain.Grid;
	Popup := TdxBarPopupMenu.Create(frmMain);
	// Construct background context menu
	if HitObj = nil then
	begin
		Button := TdxBarButton.Create(frmMain);
		Button.Caption := 'Options...';
		Button.OnClick := ContextMenuClick;
		with Popup.ItemLinks.Add do
			Item := Button;
	end
	// Construct conduit context menu
	else if HitObj is TConduit then
	begin
		Button := TdxBarButton.Create(frmMain);
		Button.Caption := 'Properties';
		Button.OnClick := ContextMenuClick;
		with Popup.ItemLinks.Add do
			Item := Button;
	end;
end;

procedure TProfileSelectTool.ContextMenuClick(Sender: TObject);
begin
	inherited;
	if TdxBarButton(Sender).Caption = 'Properties' then
	begin
	end;
end;

constructor TProfileSelectTool.Create;
begin
	inherited Create;
	AnchorPos := TObjectList.Create;
	Connected := TObjectList.Create(False);
	UpdateLinks := TObjectList.Create(False);
  MovedObjectsRect := TDRectangle.Create;
end;

procedure TProfileSelectTool.DblClick;
begin
	DoubleClicked := True;
end;

destructor TProfileSelectTool.Destroy;
begin
	AnchorPos.Free;
	Connected.Free;
	UpdateLinks.Free;
  MovedObjectsRect.Free;
	inherited;
end;

function TProfileSelectTool.GetToolName: String;
begin
	Result := 'ProfileSelect';
end;

procedure TProfileSelectTool.MouseDown(Sender: TObject;
	Button: TMouseButton; Shift: TShiftState; X, Y: Integer);
var
	HitObj: TDocObj;
	HitPt: TDPoint;
	ObjRect: TDRectangle;
	SelectedCounter, ListCounter: Integer;
	Obj: TDocObj;
	ProfilePath: TPath;
	OrderInPath: Integer;
	Grid: TGrid;
	ACanvas: TCanvas;
begin
	CodeSite.EnterMethod('ProfileSelectTool.MouseDown');
	DrawDragRect := False;

	Grid := frmMain.Grid;
	ACanvas := frmMain.DrawBox.Canvas;

	// Double-click occurred
	if DoubleClicked then
	begin
		DoubleClicked := False;
		with Sender as TTransPanel do
		begin
			HitPt := Grid.ClientToGrid(Types.Point(X, Y));
			HitObj := frmMain.ObjectAt(HitPt);
			if Assigned(HitObj) then
			begin
				HitObj.EditProperties;
			end;
			DoubleClicked := True;
		end;
		inherited MouseDown(Sender, Button, Shift, X, Y);
		Dragging := False;
		CodeSite.ExitMethod('ProfileSelectTool.MouseDown');
		Exit;
	end;

	with frmMain do
	begin
		DrawBox.SetFocus;
		HitPt := Grid.ClientToGrid(Types.Point(X, Y));
		SelectMode := smNone;

		// Was an object hit?
		HitObj := frmMain.ObjectAt(HitPt);
		if Assigned(HitObj) then
		begin
			inherited MouseDown(Sender, Button, Shift, X, Y);
			// Turn off the flyover
			if Assigned(FlyOverObj) then
			begin
				(FlyOverObj as TDocObj).DrawTracker(ACanvas, tsFlyOver);
				FlyOverObj := nil;
			end;

			// Object was hit; is the object already selected?
			if SelectedList.IndexOf(HitObj) > -1 then
			begin
				NotPreviouslySelected := False;
				// Object was already selected; is it the only one selected?
				if SelectedList.Count = 1 then
				begin
					frmMain.adoNetwork.Locate('LinkID', TConduit(SelectedList[0]).LinkID, [loCaseInsensitive]);
					DragHandle := HitObj.HitTest(HitPt);
					if DragHandle > 0 then
					begin
						SelectMode := smResize;
						HitObj.Draw(ACanvas, [dsTemp]);
						// Paint info
						ProfilePath := frmMain.Path;
						OrderInPath := ProfilePath.Objects.IndexOf(HitObj);
						if HitObj is TConduit then
						begin
							PaintProfileConduitInfo(ACanvas, TConduit(HitObj), DragHandle);
						end;
					end
					else
						SelectMode := smMove;
				end
				else
				begin
					{ Object is not the only one selected; hold the object for
						deselection if move does not occur }
					if ssShift in Shift then
					begin
						ObjToDelete := HitObj;
						NotMovedOnDelete := True;
					end
					else
						NotMovedOnDelete := False;
					SelectedList.Exchange(SelectedList.IndexOf(HitObj), SelectedList.Count-1);
					SelectMode := smMove;
				end;
			end
			else
			begin
				{ Object was not already selected }
				NotPreviouslySelected := True;
				if ssShift in Shift then
					if SelectedList.Count > 0 then
					begin
						Select(HitObj, True);
					end
					else
						Select(HitObj, False)
				else
				begin
					Select(HitObj, False);
				end;
				NotMovedOnDelete := False;
				SelectMode := smMove;
				frmMain.adoNetwork.Locate('LinkID', TConduit(SelectedList[0]).LinkID, [loCaseInsensitive]);
			end;

			Connected.Clear;
			for SelectedCounter := 0 to SelectedList.Count-1 do
			begin
				Obj := TObject(Selected[SelectedCounter]) as TDocObj;
				Obj.Draw(ACanvas, [dsTemp]);
			end;
			NotMoved := True;
		end
		else
		begin
			{ No object hit; clear the selection list, prepare for marquee selection }
			if not (ssShift in Shift) then
				ResetSelection;
			SelectMode := smMarqueeSelect;
			DrawDragRect := True;
//			inspNetwork.Hide;
			pnlPropertiesData.Hide;
			inherited MouseDown(Sender, Button, Shift, X, Y);
		end;
	end;
	CodeSite.ExitMethod('ProfileSelectTool.MouseDown');
end;

procedure TProfileSelectTool.MouseMove(Sender: TObject; Shift: TShiftState;
	X, Y: Integer);
var
	Obj: TDocObj;
	ObjPos: TDRectangle;
	SelectionCounter, ListCounter: Integer;
	NextFlyOverObj: TObject;
	ProfilePath: TPath;
	OrderInPath: Integer;
	Grid: TGrid;
	ACanvas: TCanvas;
begin
	DoubleClicked := False;

	Grid := frmMain.Grid;
	ACanvas := frmMain.DrawBox.Canvas;

	if not Dragging then
	begin
		inherited MouseMove(Sender, Shift, X, Y);
		with frmMain do
		begin
			NextFlyOverObj := frmMain.ObjectAt(Grid.ClientToGrid(Types.Point(X, Y)));
			if not Assigned(NextFlyOverObj) then
			begin
				CodeSite.SendMsg('No object under mouse detected');
				NextFlyOverObj := ObjectAt(Grid.ClientToGrid(Types.Point(X, Y)));
			end;
			if Assigned(FlyOverObj) then
				if Assigned(NextFlyOverObj) then
				begin
					// New flyover object with previous flyover
					CodeSite.SendMsg('New flyover object with previous flyover object still present');
					if NextFlyOverObj <> FlyOverObj then
					begin
						(NextFlyOverObj as TDocObj).DrawTracker(ACanvas, tsFlyOver);
						try
							(FlyOverObj as TDocObj).DrawTracker(ACanvas, tsFlyOver);
						except
							on EAccessViolation do FlyOverObj := nil;
							on EInvalidCast do FlyOverObj := nil;
						end;
						FlyOverObj := NextFlyOverObj;
						if SelectedList.Count < 1 then
						begin
							frmMain.adoNetwork.Locate('LinkID', TConduit(FlyOverObj).LinkID, [loCaseInsensitive]);
//							inspNetwork.Show;
							pnlPropertiesData.Show;
						end;
					end
				end
				else
				// No flyover object; blank space
				begin
					CodeSite.SendMsg('No flyover object detected, removing previous flyover object');
					(FlyOverObj as TDocObj).DrawTracker(ACanvas, tsFlyOver);
					FlyOverObj := nil;
					if SelectedList.Count < 1 then
					begin
//						inspNetwork.Hide;
						pnlPropertiesData.Hide;
					end
					else
					begin
						frmMain.adoNetwork.Locate('LinkID', TConduit(SelectedList[0]).LinkID, [loCaseInsensitive]);
//						inspNetwork.Show;
						pnlPropertiesData.Show;
					end;
				end
			else
			begin
				// New flyover object, no previous flyover
				if Assigned(NextFlyOverObj) then
				begin
					CodeSite.SendMsg('A new flyover object detected without previous flyover');
					(NextFlyOverObj as TDocObj).DrawTracker(ACanvas, tsFlyOver);
					FlyOverObj := NextFlyOverObj;
					if SelectedList.Count < 1 then
					begin
						frmMain.adoNetwork.Locate('LinkID', TConduit(FlyOverObj).LinkID, [loCaseInsensitive]);
//						inspNetwork.Show;
						pnlPropertiesData.Show;
					end
				end
				else
				begin
					CodeSite.SendMsg('No flyover object detected, no previous flyover');
					FlyOverObj := nil;
				end;
			end;
		end;
		Exit;
	end;

	with frmMain do
	begin
		{ Are we marquee selecting? }
		if SelectMode = smMarqueeSelect then
		begin
			if ssShift in Shift then { do not constrain marquee if shift is down }
				inherited MouseMove(Sender, Shift - [ssShift], X, Y)
			else
				inherited MouseMove(Sender, Shift, X, Y);
			Exit;
		end;

		inherited MouseMove(Sender, Shift, X, Y);

		{ Are we resizing? }
		if SelectMode = smResize then
		begin
			Obj := TObject(Selected[0]) as TDocObj;
			Obj.Draw(ACanvas, [dsTemp]);
			ProfilePath := Path;
			OrderInPath := ProfilePath.Objects.IndexOf(HitObj);
			if HitObj is TConduit then
			begin
				// Draw the info to erase it
//        PaintProfileConduitInfo(View, ACanvas, TConduit(HitObj), DragHandle);
			end;

			// Move it buddy
			Obj.MoveHandleTo(DragHandle, Grid.ClientToGrid(DragPt));
			Obj.Draw(ACanvas, [dsTemp]);

			if HitObj is TConduit then
			begin
				// Redraw the info in new position
				PaintProfileConduitInfo(ACanvas, TConduit(HitObj), DragHandle);
			end;
		end;

		{ Are we moving? }
		if SelectMode = smMove then
		begin
			if NotMoved then
			begin
				if LengthFromPts(DragPt, OriginPt) > 5 then
				begin
					NotMoved := False;
					NotMovedOnDelete := False;
					ObjToDelete := nil;
				end
				else
				begin
					NotMovedOnDelete := True;
					Exit; { Don't give the impression we're moving if we haven't yet moved
									past the minimum distance }
				end;
			end;

			if Assigned(FlyOverObj) then
			begin
				(FlyOverObj as TDocObj).DrawTracker(ACanvas, tsFlyOver);
				FlyOverObj := nil;
			end;

			ObjPos := TDRectangle.Create;
			try
				for ListCounter := 0 to Connected.Count-1 do
				begin
					Obj := TObject(Connected[ListCounter]) as TDocObj;
					Obj.Draw(ACanvas, [dsTemp]);
				end;
				{ Now move the nonlinks }
				for SelectionCounter := 0 to SelectedList.Count-1 do
				begin
					Obj := TObject(Selected[SelectionCounter]) as TDocObj;
					with Grid do
						ObjPos.OffsetRect(ClientToGridX(DragPt.X) - ClientToGridX(OriginPt.X),
							ClientToGridY(DragPt.Y) - ClientToGridY(OriginPt.Y));
					Obj.Draw(ACanvas, [dsTemp]);
					Obj.Draw(ACanvas, [dsTemp]);
				end;
				for ListCounter := 0 to Connected.Count-1 do
				begin
					Obj := TObject(Connected[ListCounter]) as TDocObj;
					Obj.Draw(ACanvas, [dsTemp]);
				end;
			finally
				ObjPos.Free;
			end;
		end;
	end;
end;

procedure TProfileSelectTool.MouseUp(Sender: TObject; Button: TMouseButton;
	Shift: TShiftState; X, Y: Integer);
var
	SelectionCounter: Longint;
	ListCounter: Longint;
	ObjPos: TDRectangle;
	Obj: TDocObj;
	DontDelete: Boolean;
	PreviousSelectionCount: Integer;
	RepaintExtents: TDRectangle;
	NewSelectionExtents: TDRectangle;
	UpdateRect: TRect;
	Grid: TGrid;
	ACanvas: TCanvas;
	i: Integer;
	Layer: CMapXLayer;
begin
	if not Dragging then
	begin
		inherited MouseUp(Sender, Button, Shift, X, Y);
		Exit;
	end;

	Grid := frmMain.Grid;
	ACanvas := frmMain.DrawBox.Canvas;

	with frmMain do
	begin
		CodeSite.SendEnum('SelectMode', TypeInfo(TProfileSelectMode), Ord(SelectMode));

		{ Are we marquee selecting? }
		if SelectMode = smMarqueeSelect then
		begin
			if ssShift in Shift then { do not constrain marquee if shift is down }
				inherited MouseUp(Sender, Button, Shift - [ssShift], X, Y)
			else
				inherited MouseUp(Sender, Button, Shift, X, Y);
			PreviousSelectionCount := SelectedList.Count;

			SelectInRect(RectFromPts(OriginPt, DragPt), ssShift in Shift);

			// Calculate new selection extents for repainting
			if SelectedList.Count > 0 then
			begin
				NewSelectionExtents := GetSelectedExtents;
				UpdateRect := Grid.GridToClient(NewSelectionExtents);
//				OffsetRect(UpdateRect, DrawBox.Left, DrawBox.Top);
//				InvalidateRect(RzPanel2.Handle, @UpdateRect, False);
				InvalidateRect(frmMain.DrawBox.Handle, @UpdateRect, False);
				frmMain.DrawBox.Update;
				NewSelectionExtents.Free;
			end
			else
			begin
				UpdateRect := frmMain.DrawBox.ClientRect;
				InvalidateRect(frmMain.DrawBox.Handle, @UpdateRect, False);
				frmMain.DrawBox.Update;
			end;

{			if (SelectedList.Count <> 1) and (Assigned(frmPropertiesFrame)) then
			begin
				frmPropertiesFrame.Obj := nil;
				frmPropertiesFrame.UpdateSheet;
			end;}

			ClearAnchors;
			Layer := mapNetwork.Layers.Item(NetworkTableName);
			Layer.Selection.ClearSelection;
			for i := 0 to SelectedList.Count-1 do
				Layer.Selection.Add(Layer.GetFeatureByID(TConduit(SelectedList[i]).MapinfoID));
			Exit;
		end;

		{ Are we resizing? }
		if SelectMode = smResize then
		begin
			Obj := TObject(frmMain.Selected[0]) as TDocObj;
			Obj.Draw(ACanvas, [dsTemp]);
			inherited MouseUp(Sender, Button, Shift, X, Y);
			if Obj is TConduit then
			begin
				// Update Database
				frmMain.adoNetwork.Locate('LinkID', TConduit(Obj).LinkID, [loCaseInsensitive]);
//				inspNetwork.Show;
				pnlPropertiesData.Show;
{        if (dmProject.tblLinks.FindKey([TConduit(HitObj).DBLinkID])) and
					(dmProject.tblDetailConduits.RecordCount > 0) then
				begin
					dmProject.tblDetailConduits.Edit;
					dmProject.tblDetailConduits.FieldByName('Length').AsFloat := TConduit(HitObj).Length;
					dmProject.tblDetailConduits.FieldByName('UpOffset').AsFloat := TConduit(HitObj).UpOffset;
					dmProject.tblDetailConduits.FieldByName('DnOffset').AsFloat := TConduit(HitObj).DnOffset;
					dmProject.tblDetailConduits.FieldByName('VertDim').AsFloat := TConduit(HitObj).VertDim;
					dmProject.tblDetailConduits.Post;
				end;}
			end;
			//frmMain.Refresh;
{			UpdateRect := RzPanel2.ClientRect;
			InvalidateRect(RzPanel2.Handle, @UpdateRect, False);
			RzPanel2.Update;}
			UpdateRect := frmMain.DrawBox.ClientRect;
			InvalidateRect(frmMain.DrawBox.Handle, @UpdateRect, False);
			frmMain.DrawBox.Update;
{			if (Assigned(frmPropertiesFrame)) then
			begin
				frmPropertiesFrame.Obj := HitObj;
				frmPropertiesFrame.UpdateSheet;
			end;}
			ClearAnchors;
			Exit;
		end;

		{ Are we moving? }
		if SelectMode = smMove then
		begin
			if NotMoved then
			begin
				if NotMovedOnDelete then
				begin
					Obj := ObjToDelete as TDocObj;
					DontDelete := False;
					{ Delete something from the selection }
					if not DontDelete and not NotPreviouslySelected then
					begin
//						AnchorPos.Delete(SelectedList.IndexOf(Obj));
						Deselect(Obj);
						UpdateObj(Obj);
						ObjToDelete := nil;
						inherited MouseUp(Sender, Button, Shift, X, Y);
						NotMoved := False;
						if SelectedList.Count > 0 then
						begin
							NewSelectionExtents := GetSelectedExtents;
							// Update display
//							UpdateRect := Grid.GridToClient(NewSelectionExtents);
							UpdateRect := frmMain.DrawBox.ClientRect;
	//							OffsetRect(UpdateRect, DrawBox.Left, DrawBox.Top);
	//							InvalidateRect(RzPanel2.Handle, @UpdateRect, False);
							InvalidateRect(frmMain.DrawBox.Handle, @UpdateRect, False);
							frmMain.DrawBox.Update;
							NewSelectionExtents.Free;
						end;
						//frmMain.Refresh;
						frmMain.Update;
						ClearAnchors;
						Layer := mapNetwork.Layers.Item(NetworkTableName);
						Layer.Selection.ClearSelection;
						for i := 0 to SelectedList.Count-1 do
							Layer.Selection.Add(Layer.GetFeatureByID(TConduit(SelectedList[i]).MapinfoID));
						Exit;
					end
					else
					{ If we're not deleting, just clean up the screen }
					begin
						inherited MouseUp(Sender, Button, Shift, X, Y);
						NotMoved := False;
						if SelectedList.Count > 0 then
						begin
							NewSelectionExtents := GetSelectedExtents;
							// Update display
//							UpdateRect := Grid.GridToClient(NewSelectionExtents);
							UpdateRect := frmMain.DrawBox.ClientRect;
//							OffsetRect(UpdateRect, DrawBox.Left, DrawBox.Top);
//							InvalidateRect(RzPanel2.Handle, @UpdateRect, False);
							InvalidateRect(frmMain.DrawBox.Handle, @UpdateRect, False);
							frmMain.DrawBox.Update;
							NewSelectionExtents.Free;
						end;
						//frmMain.Refresh;
						frmMain.Update;
						ClearAnchors;
						Layer := mapNetwork.Layers.Item(NetworkTableName);
						Layer.Selection.ClearSelection;
						for i := 0 to SelectedList.Count-1 do
							Layer.Selection.Add(Layer.GetFeatureByID(TConduit(SelectedList[i]).MapinfoID));
						Exit;
					end;
				end
				else
				{ Mouse was moved during selection delete, so just clean up the screen}
				begin
					inherited MouseUp(Sender, Button, Shift, X, Y);
					NotMoved := False;
					if SelectedList.Count > 0 then
					begin
						NewSelectionExtents := GetSelectedExtents;
						// Rifle through the connecting links
						for SelectionCounter := 0 to SelectedList.Count-1 do
						begin
							Obj := TObject(Selected[SelectionCounter]) as TDocObj;
						end;
						// Update display
//						UpdateRect := Grid.GridToClient(NewSelectionExtents);
						UpdateRect := frmMain.DrawBox.ClientRect;
						InflateRect(UpdateRect, 5, 5);
//						OffsetRect(UpdateRect, DrawBox.Left, DrawBox.Top);
//						InvalidateRect(RzPanel2.Handle, @UpdateRect, False);
						InvalidateRect(frmMain.DrawBox.Handle, @UpdateRect, False);
						frmMain.DrawBox.Update;
//						NewSelectionExtents.Free;
					end;
					if Assigned(FlyOverObj) then
					begin
						(FlyOverObj as TDocObj).DrawTracker(ACanvas, tsFlyOver);
						FlyOverObj := nil;
						if SelectedList.Count < 1 then
						begin
//							inspNetwork.Hide;
							pnlPropertiesData.Hide;
							UpdateRect := frmMain.DrawBox.ClientRect;
							InvalidateRect(frmMain.DrawBox.Handle, @UpdateRect, False);
							frmMain.DrawBox.Update;
//							NewSelectionExtents.Free;
						end
						else
						begin
							frmMain.adoNetwork.Locate('LinkID', TConduit(SelectedList[0]).LinkID, [loCaseInsensitive]);
//							inspNetwork.Show;
							pnlPropertiesData.Show;
						end;
					end;
{					if (Assigned(frmPropertiesFrame)) then
					begin
						frmPropertiesFrame.Obj := HitObj;
						frmPropertiesFrame.UpdateSheet;
					end;
					ClearAnchors;
					Exit;}
				end
			end
			else
			begin
				inherited MouseUp(Sender, Button, Shift, X, Y);
				for SelectionCounter := 0 to SelectedList.Count-1 do
				begin
					Obj := TObject(Selected[SelectionCounter]) as TDocObj;
					Obj.Draw(ACanvas, [dsTemp]);
				end;
			end;

			ClearAnchors;
			//frmMain.Refresh;
			frmMain.Update;
			if Assigned(FlyOverObj) then
			begin
				(FlyOverObj as TDocObj).DrawTracker(ACanvas, tsFlyOver);
				FlyOverObj := nil;
			end;
			Layer := mapNetwork.Layers.Item(NetworkTableName);
			Layer.Selection.ClearSelection;
			for i := 0 to SelectedList.Count-1 do
				Layer.Selection.Add(Layer.GetFeatureByID(TConduit(SelectedList[i]).MapinfoID));
			Exit;
		end;
	end;

	inherited MouseUp(Sender, Button, Shift, X, Y);

end;

procedure TProfileSelectTool.PaintProfileConduitInfo(ACanvas: TCanvas;
	AConduit: TConduit; WhichHandle: Integer);
var
	ProfilePath: TPath;
	OrderInPath: Integer;
	HintText: String;
	HintRect: TRect;
	DrawBoxOffsetX, DrawBoxOffsetY: Integer;
begin
	ProfilePath := frmMain.Path;
	OrderInPath := ProfilePath.Objects.IndexOf(AConduit);
	with frmMain do
	begin
		DrawBoxOffsetX := RzPanel1.Left + RzPanel2.Left + RzPanel4.Left;
		DrawBoxOffsetY := RzPanel1.Top + RzPanel2.Top + RzPanel4.Top;
	end;
	with frmMain do
	begin
		case WhichHandle of
			1: // Upstream Crown
			begin
				HintText := Format('Conduit %s-%s'#13'Depth %.2f ft',
					[AConduit.UpJunc, AConduit.DnJunc, AConduit.VertDim]);
				HintRect := HintWin.CalcHintRect(Screen.Width, HintText, nil);
				DisplayHint(HintText, Grid.GridToClientX(ProfilePath.LengthFromStart(AConduit))-5-(HintRect.Right-HintRect.Left)+DrawBoxOffsetX,
					Grid.GridToClientY(AConduit.UpCrown)+DrawBoxOffsetY);
			end;
			2: // Downstream Crown
			begin
				HintText := Format('Conduit %s-%s'#13'Depth %.2f ft',
					[AConduit.UpJunc, AConduit.DnJunc, AConduit.VertDim]);
				DisplayHint(HintText, Grid.GridToClientX(ProfilePath.LengthFromStart(AConduit)+Abs(AConduit.Length))+25+DrawBoxOffsetX,
					Grid.GridToClientY(AConduit.DnCrown)+DrawBoxOffsetY);
			end;
			3,9,10: // Upstream Invert
			begin
				HintText := Format('Conduit %s-%s'#13'Up el %.2f ft'#13'Slope %.2f%%',
					[AConduit.UpJunc, AConduit.DnJunc, AConduit.UpInvert,
						AConduit.Slope*100]);
				HintRect := HintWin.CalcHintRect(Screen.Width, HintText, nil);
				DisplayHint(HintText, Grid.GridToClientX(ProfilePath.LengthFromStart(AConduit))-5-(HintRect.Right-HintRect.Left)+DrawBoxOffsetX,
					Grid.GridToClientY(AConduit.UpInvert)+DrawBoxOffsetY);
			end;
			4,11,12: // Downstream Invert
			begin
				HintText := Format('Conduit %s-%s'#13'Dn el %.2f ft'#13'Slope %.2f%%',
					[AConduit.UpJunc, AConduit.DnJunc, AConduit.DnInvert,
						AConduit.Slope*100]);
				DisplayHint(HintText, Grid.GridToClientX(ProfilePath.LengthFromStart(AConduit)+Abs(AConduit.Length))+25+DrawBoxOffsetX,
					Grid.GridToClientY(AConduit.DnInvert)+DrawBoxOffsetY);
			end;
			5: // Vertical Position
			begin
				HintText := Format('Conduit %s-%s'#13'Up el %.2f ft'#13+
					'Dn el %.2f ft',
					[AConduit.UpJunc, AConduit.DnJunc, AConduit.UpInvert, AConduit.DnInvert]);
				DisplayHint(HintText, Grid.GridToClientX(ProfilePath.LengthFromStart(AConduit)+Abs(AConduit.Length))+25+DrawBoxOffsetX,
					Grid.GridToClientY((AConduit.UpCrown+AConduit.DnCrown)/2-AConduit.VertDim/2)+DrawBoxOffsetY);
			end;
			6: // Length
			begin
				HintText := Format('Conduit %s-%s'#13'Length %.2f ft'#13'Slope %.2f%%',
					[AConduit.UpJunc, AConduit.DnJunc, AConduit.Length, AConduit.Slope*100]);
				DisplayHint(HintText, Grid.GridToClientX(ProfilePath.LengthFromStart(AConduit)+AConduit.Length)+25+DrawBoxOffsetX,
					Grid.GridToClientY(AConduit.DnInvert+AConduit.VertDim/2)+DrawBoxOffsetY)
			end;
			7: // Upstream Ground
			begin
				HintText := Format('Conduit %s-%s'#13'UpGround %.2f ft'#13'UpDepth %.2f%%',
					[AConduit.UpJunc, AConduit.DnJunc, AConduit.UpGround, AConduit.UpDepth]);
				HintRect := HintWin.CalcHintRect(Screen.Width, HintText, nil);
				DisplayHint(HintText, Grid.GridToClientX(ProfilePath.LengthFromStart(AConduit))-5-(HintRect.Right-HintRect.Left)+DrawBoxOffsetX,
					Grid.GridToClientY(AConduit.UpGround)+DrawBoxOffsetY)
			end;
			8: // Downstream Ground
			begin
				HintText := Format('Conduit %s-%s'#13'DnGround %.2f ft'#13'DnDepth %.2f%%',
					[AConduit.UpJunc, AConduit.DnJunc, AConduit.DnGround, AConduit.DnDepth]);
				DisplayHint(HintText, Grid.GridToClientX(ProfilePath.LengthFromStart(AConduit)+Abs(AConduit.Length))+25+DrawBoxOffsetX,
					Grid.GridToClientY(AConduit.DnGround)+DrawBoxOffsetY)
			end;
		end; // case WhichHandle of
	end;
end;

procedure TProfileSelectTool.PaintProfileNodeInfo(ACanvas: TCanvas; ANode,
	WhichHandle: Integer);
begin

end;

function TfrmMain.CalcClientOffset: TPoint;
begin
	Result := Types.Point(0, 0);
end;

procedure TfrmMain.CatchKeyDown(Key: Word; Shift: TShiftState);
begin

end;

procedure TfrmMain.CenterWindow(APoint: TDPoint; Animate: Boolean);
var
	ARect: TDRectangle;
	CurrentRect: TDRectangle;
	HorizStep, VertStep: Double;
	i: Integer;
begin
	CurrentRect := Grid.GetBoundsRect;
	ARect := TDRectangle.Create(APoint.X-FGrid.Width/2, APoint.Y-FGrid.Height/2,
		APoint.X+FGrid.Width/2, APoint.Y+FGrid.Height/2);
	try
		if Animate then
		begin
			HorizStep := (ARect.Left - CurrentRect.Left)/AnimateSteps;
			VertStep := (ARect.Top - CurrentRect.Top)/AnimateSteps;
			for i := 1 to AnimateSteps do
			begin
				CurrentRect.OffsetRect(HorizStep, VertStep);
				FitWindow(CurrentRect);
				with Grid do
				begin
					RulVert.Min := Min.Y;
					RulVert.Zoom := Zoom.Y;
					try
						rulVert.Spacing := CalcRulerInterval(Max.Y-Min.Y);
					except
						on EZeroDivide do // Zero for Min.Y
							rulVert.Spacing := 10;
					end;
					RulHoriz.Min := Min.X;
					RulHoriz.Zoom := Zoom.X;
					try
						rulHoriz.Spacing := CalcRulerInterval(Max.X-Min.X);
					except
						on EZeroDivide do // Zero for Min.X
							rulHoriz.Spacing := 10;
					end;
				end;
				Update;
				Delay(0.001);
			end;
			FitWindow(ARect);
		end
		else
		FitWindow(ARect);
	finally
		ARect.Free;
		CurrentRect.Free;
	end;
end;

procedure TfrmMain.CenterWindowClient(APoint: TPoint; Animate: Boolean);
begin
  CenterWindow(Grid.ClientToGrid(APoint));
end;

procedure TfrmMain.Deselect(AObj: TDocObj);
begin
	if FSelected.IndexOf(AObj) > -1 then
		FSelected.Delete(FSelected.IndexOf(AObj));
end;

procedure TfrmMain.DisplayHint(HintText: String; XPos, YPos: Integer);
var
	HintRect: TRect;
begin
	HintRect := HintWin.CalcHintRect(Screen.Width, HintText, nil);
	OffsetRect(HintRect, XPos, YPos);
	HintRect.Right := HintRect.Right + 3;
	HintRect.TopLeft := ClientToScreen(HintRect.TopLeft);
	HintRect.BottomRight := ClientToScreen(HintRect.BottomRight);
	HintWin.ActivateHint(HintRect, HintText);
end;

procedure TfrmMain.FitWindow(ARect: TDRectangle);
var
	CHeight, CWidth: Integer;
	CurrentAspectRatio, RectAspectRatio: Double;
begin
	CHeight := DrawBox.Height;
	CWidth := DrawBox.Width;
	CurrentAspectRatio := CHeight / CWidth;
	if ARect.Width = 0 then
		RectAspectRatio := CurrentAspectRatio + 1 // Force a fit to height
	else
		RectAspectRatio := ARect.Height / ARect.Width;

	with Grid do
	begin
		// Rect is taller than client, fit to height
		if RectAspectRatio > CurrentAspectRatio then
		begin
			if Grid.Isotropic then
			begin
				Grid.Min := DPoint(ARect.Left - ((ARect.Height/CurrentAspectRatio) - ARect.Width) / 2,
					ARect.Bottom);
				Zoom := DPoint((CHeight/ARect.Height), (CHeight/ARect.Height));
			end
			else
			begin
				Grid.Min := DPoint(ARect.Left, ARect.Bottom);
				Zoom := DPoint((CWidth/ARect.Width), (CHeight/ARect.Height));
			end;
		end
		// Rect is wider than client, fit to width
		else
		begin
			if Grid.Isotropic then
			begin
				Grid.Min := DPoint(ARect.Left,
					ARect.Bottom - ((ARect.Width*CurrentAspectRatio) - ARect.Height) / 2);
				Zoom := DPoint((CWidth/ARect.Width), (CWidth/ARect.Width));
			end
			else
			begin
				Grid.Min := DPoint(ARect.Left, ARect.Bottom);
				Zoom := DPoint((CWidth/ARect.Width), (CHeight/ARect.Height))
			end;
		end;
	end;
end;

procedure TfrmMain.FitWindowClient(ARect: TRect);
var
  GridRect: TDRectangle;
begin
  // Convert rect to grid coordinates and pass to FitWindow
	GridRect := Grid.ClientToGrid(ARect);
	try
		FitWindow(GridRect);
	finally
		GridRect.Free;
	end;
end;

function TfrmMain.GetHorizExtents: Double;
var
	i: Integer;
begin
	i := 1;
	Result := 0;
	for i := 0 to FPath.Objects.Count-1 do
	begin
		if TObject(FPath.Objects[i]) is TConduit then
			Result := Result + Abs(TConduit(FPath.Objects[i]).Length)
	end;
end;

function TfrmMain.GetSelected(Idx: Integer): TDocObj;
begin
  Result := TDocObj(FSelected[Idx]);
end;

function TfrmMain.GetSelectedExtents: TDRectangle;
var
	i: Integer;
	SelectedObj: TDocObj;
	SelectedObjRect: TDRectangle;
begin
	Result := TDRectangle.Create;
	if SelectedList.Count = 0 then
	begin
		Result.Free;
		Result := nil;
		Exit;
	end;

	SelectedObjRect := TConduit(FSelected[0]).GetExtents;
	Result.CopyRect(SelectedObjRect);
	SelectedObjRect.Free;
	for i := 1 to SelectedList.Count-1 do
	begin
		SelectedObj := TConduit(FSelected[i]);
		SelectedObjRect := SelectedObj.GetExtents;
		Result.UnionRect(SelectedObjRect);
		SelectedObjRect.Free;
	end;
end;

function TfrmMain.GetSelectedExtentsClient: TRect;
begin

end;

function TfrmMain.GetVertHigh: Double;
var
	i: Integer;
begin
	Result := TConduit(FPath.Objects[0]).UpGround;
	for i := 0 to FPath.Objects.Count-1 do
	begin
		Result := Max(Result, TConduit(FPath.Objects[i]).UpGround);
	end;
	for i := 0 to FPath.Objects.Count-1 do
	begin
		Result := Max(Result, TConduit(FPath.Objects[i]).DnGround);
	end;
	for i := 0 to FPath.Objects.Count-1 do
	begin
		Result := Max(Result, TConduit(FPath.Objects[i]).UpInvert);
	end;
	for i := 0 to FPath.Objects.Count-1 do
	begin
		Result := Max(Result, TConduit(FPath.Objects[i]).DnInvert);
	end;
end;

function TfrmMain.GetVertLow: Double;
var
	i: Integer;
begin
	Result := TConduit(FPath.Objects[0]).UpInvert;
	for i := 0 to FPath.Objects.Count-1 do
	begin
		Result := Min(Result, TConduit(FPath.Objects[i]).UpInvert);
	end;
	for i := 0 to FPath.Objects.Count-1 do
	begin
		Result := Min(Result, TConduit(FPath.Objects[i]).DnInvert);
	end;
	for i := 0 to FPath.Objects.Count-1 do
	begin
		Result := Min(Result, TConduit(FPath.Objects[i]).UpGround);
	end;
	for i := 0 to FPath.Objects.Count-1 do
	begin
		Result := Min(Result, TConduit(FPath.Objects[i]).DnGround);
	end;
	for i := 0 to FPath.Objects.Count-1 do
	begin
		Result := Min(Result, TConduit(FPath.Objects[i]).UpGround-TConduit(FPath.Objects[i]).UpDepth);
	end;
	for i := 0 to FPath.Objects.Count-1 do
	begin
		Result := Min(Result, TConduit(FPath.Objects[i]).DnGround-TConduit(FPath.Objects[i]).DnDepth);
	end;
end;

procedure TfrmMain.InvalidateObj(AObj: TDocObj);
var
	ExtentsRect: TDRectangle;
	UpdateRect: TRect;
begin
	ExtentsRect := AObj.GetExtents;
	try
		UpdateRect := Grid.GridToClient(ExtentsRect);
	finally
		ExtentsRect.Free;
	end;
	UpdateRect := Rect(UpdateRect.Left - 10, UpdateRect.Top - 10, UpdateRect.Right + 10,
		UpdateRect.Bottom + 10);
	OffsetRect(UpdateRect, DrawBox.Left, DrawBox.Top);
//	InvalidateRect(RzPanel2.Handle, @UpdateRect, False);
	InvalidateRect(frmMain.DrawBox.Handle, @UpdateRect, False);
end;

procedure TfrmMain.InvalidateObj(AObj: TDocObj; OldRect: TRect);
var
	ExtentsRect: TDRectangle;
	NewExtentsRect,	OldExtentsRect: TRectangle;
	UpdateRect: TRect;
begin
	ExtentsRect := AObj.GetExtents;
	NewExtentsRect := TRectangle.Create(Grid.GridToClient(ExtentsRect));
	OldExtentsRect := TRectangle.Create(OldRect);
	OldExtentsRect.UnionRect(NewExtentsRect);
	try
		UpdateRect := OldExtentsRect.AsRect;
	finally
		ExtentsRect.Free;
		NewExtentsRect.Free;
		OldExtentsRect.Free;
	end;
	InflateRect(UpdateRect, 10, 10);
	OffsetRect(UpdateRect, DrawBox.Left, DrawBox.Top);
//	InvalidateRect(RzPanel2.Handle, @UpdateRect, False);
	InvalidateRect(frmMain.DrawBox.Handle, @UpdateRect, False);
end;

procedure TfrmMain.InvalidateViewRect(ARect: TDRectangle);
var
	UpdateRect: TRect;
begin
	UpdateRect := FGrid.GridToClient(ARect);
	UpdateRect := Rect(UpdateRect.Left - 10, UpdateRect.Top - 10, UpdateRect.Right + 10,
		UpdateRect.Bottom + 10);
	OffsetRect(UpdateRect, DrawBox.Left, DrawBox.Top);
//	InvalidateRect(RzPanel2.Handle, @UpdateRect, False);
	InvalidateRect(frmMain.DrawBox.Handle, @UpdateRect, False);
end;

function TfrmMain.IsSelected(AObj: TDocObj): Boolean;
begin
	Result := False;
	if FSelected.IndexOf(AObj) > -1 then
		Result := True;
end;

function TfrmMain.ObjectAt(APoint: TDPoint): TDocObj;
var
	ListCounter: Integer;
	HitResult: Integer;
	ProfilePath: TPath;
	i: Integer;
	Grid: TGrid;
begin
	Grid := frmMain.Grid;

	Result := nil;
	ProfilePath := Path;
	if SelectedList.Count > 0 then
		for i := 0 to SelectedList.Count-1 do
		begin
			HitResult := TModelObj(Selected[i]).HitTest(APoint);
			if HitResult > -1 then
			begin
				Result := TModelObj(Selected[i]);
				Exit;
			end;
		end;

	if not Assigned(Path) then
		Exit;
		
	for i := 0 to ProfilePath.Objects.Count-1 do
	begin
		HitResult := TLink(ProfilePath.Objects[i]).HitTest(APoint);
		if HitResult > -1 then
		begin
			Result := TLink(ProfilePath.Objects[i]);
			Exit;
		end;
	end;
end;

function TfrmMain.ObjectAtClient(APoint: TPoint): TDocObj;
begin
  // Call ObjectAt and convert to screen coordinates
end;

procedure TfrmMain.RecalcView;
begin
	if Assigned(FPath) then
		with Grid do
		begin
			if IsZero(GetVertHigh-GetVertLow) then
				Zoom := DPoint((DrawBox.Width-50)/GetHorizExtents,
					(DrawBox.Height-20)/(DrawBox.Height-20))
			else
				Zoom := DPoint((DrawBox.Width-50)/GetHorizExtents,
					(DrawBox.Height-20)/(GetVertHigh-GetVertLow));
			Min := DPoint(0-GridUnits(10,0), GetVertLow-GridUnits(0,10));
			rulVert.Min := Min.Y;
			rulVert.Zoom := Zoom.Y;
			rulVert.Spacing := CalcRulerInterval(GetVertHigh-GetVertLow);
			rulHoriz.Min := Min.X;
			rulHoriz.Zoom := Zoom.X;
			rulHoriz.Spacing := CalcRulerInterval(GetHorizExtents);
			Spacing := DPoint(rulHoriz.Spacing/2, rulVert.Spacing/2);
		end;
end;

procedure TfrmMain.RemoveHint;
begin
	if HintWin.HandleAllocated then
		HintWin.ReleaseHandle;
end;

procedure TfrmMain.OpenTraceFile(ADirectory: String);
var
	LinksFileName: String;
	NodesFileName: String;
  LookupTablesFileName: String;
  ModelRegistryFileName: String;
	ChangesFileName: String;
	LinkChangesFileName: String;
	NodeChangesFileName: String;
	QualifiedNetworkTableName: String;
	ChangesConnectionString: String;
	TableList: TStringList;
	IndCatStyle: CMapXStyle;
	TracedTheme: CMapXTheme;
	LayerBounds: CMapXRectangle;
	MapAspect: Double;
	ACatalog: Catalog;
	ChangeTable: Table;
	ChangeCreate: TDateTime;
	NetworkTable: Table;
	NetworkCreate: TDateTime;
	DlgResult: Word;
	ModelVersion: String;

	TracedFields: CMapXFields;
begin
	mapNetwork.Layers.RemoveAll;
  CloseDBs;

	LinksFileName := ADirectory + '\links\mdl_links_ac.mdb';
	NodesFileName := ADirectory + '\nodes\mdl_nodes_ac.mdb';
  ModelRegistryFileName := ADirectory + '\' + ModelRegistryName;

  if not FileExists(ModelRegistryFileName) then
  begin
    MessageDlg(Format('No %s file found in %s', [ModelRegistryFileName, ADirectory]),
      mtError, [mbOK], 0);
    Exit;
  end;

  FreeAndNil(ModelRegistry);
  ModelRegistry := TIniFile.Create(ModelRegistryFileName);
  LookupTablesFileName := ModelRegistry.ReadString('mdbs', 'LookupTables',
		'\mdbs\LookupTables.mdb');
	ModelVersion := ModelRegistry.ReadString('control', 'version', '0.0');
	if StrToFloat(ModelVersion) <> StrToFloat(CurrentEmgaatsVersion) then
	begin
		MessageDlg('You have opened up a model created with EMGAATS Version '+ModelVersion+'.'#13+
			'Please open the model in EMGAATS Version '+CurrentEmgaatsVersion+' first before using Profiler on the model.',
			mtWarning, [mbOK], 0);
		Exit;
	end;

	try
		adoLinksConnection.ConnectionString := 'Provider=Microsoft.Jet.OLEDB.4.0;Password="";User ID=Admin;'+
			'Data Source='+LinksFileName+';'+
			'Mode=Share Deny None;Extended Properties="";Jet OLEDB:System database="";Jet OLEDB:Registry Path="";'+
			'Jet OLEDB:Database Password="";Jet OLEDB:Engine Type=4;Jet OLEDB:Database Locking Mode=0;Jet OLEDB:'+
			'Global Partial Bulk Ops=2;Jet OLEDB:Global Bulk Transactions=1;Jet OLEDB:New Database Password="";'
			+'Jet OLEDB:Create System Database=False;Jet OLEDB:Encrypt Database=False;'+
			'Jet OLEDB:Don''t Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;'+
			'Jet OLEDB:SFP=False';
		adoLinksConnection.Open;
	except
		on E: EOleException do
			MessageDlg(Format('Error %d'#13'%s', [E.ErrorCode, E.Message]), mtError, [mbOK], 0);
	end;

	try
		adoNodesConnection.ConnectionString := 'Provider=Microsoft.Jet.OLEDB.4.0;Password="";User ID=Admin;'+
			'Data Source='+NodesFileName+';'+
      'Mode=Share Deny None;Extended Properties="";Jet OLEDB:System database="";Jet OLEDB:Registry Path="";'+
      'Jet OLEDB:Database Password="";Jet OLEDB:Engine Type=4;Jet OLEDB:Database Locking Mode=0;Jet OLEDB:'+
      'Global Partial Bulk Ops=2;Jet OLEDB:Global Bulk Transactions=1;Jet OLEDB:New Database Password="";'
      +'Jet OLEDB:Create System Database=False;Jet OLEDB:Encrypt Database=False;'+
      'Jet OLEDB:Don''t Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;'+
      'Jet OLEDB:SFP=False';
    adoNodesConnection.Open;
  except
  	on E: EOleException do
    	MessageDlg(Format('Error %d'#13'%s', [E.ErrorCode, E.Message]), mtError, [mbOK], 0);
  end;

  try
    adoLookupTablesConnection.ConnectionString := 'Provider=Microsoft.Jet.OLEDB.4.0;'+
      'Data Source='+LookupTablesFileName+';Persist Security Info=False';
    adoLookupTablesConnection.Open;
  except
  	on E: EOleException do
    	MessageDlg(Format('Error %d'#13'%s', [E.ErrorCode, E.Message]), mtError, [mbOK], 0);
  end;

//	NetworkTableName := LeftStr(ExtractFileName(LinksFileName),Length(ExtractFileName(LinksFileName))-4);
  NetworkTableName := LinksName;

	// Create change tables if they don't exist
  ChangesFileName := ADirectory + ChangesDirName + ChangesDBName;
	ChangesConnectionString := 'Provider=Microsoft.Jet.OLEDB.4.0;Password="";User ID=Admin;'+
		'Data Source='+ChangesFileName+';'+
		'Mode=Share Deny None;Extended Properties="";Jet OLEDB:System database="";Jet OLEDB:Registry Path="";'+
		'Jet OLEDB:Database Password="";Jet OLEDB:Engine Type=4;Jet OLEDB:Database Locking Mode=0;Jet OLEDB:'+
		'Global Partial Bulk Ops=2;Jet OLEDB:Global Bulk Transactions=1;Jet OLEDB:New Database Password="";'
		+'Jet OLEDB:Create System Database=False;Jet OLEDB:Encrypt Database=False;'+
		'Jet OLEDB:Don''t Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;'+
		'Jet OLEDB:SFP=False';

	if not FileExists(ChangesFileName) then
	begin
		ACatalog := CoCatalog.Create;
		ACatalog.Create(ChangesConnectionString);
		adoChangesConnection.ConnectionString := ChangesConnectionString;
		adoChangesConnection.Open;
		adoChangesCommand.CommandText := 'CREATE TABLE LinkChanges (' +
			'LinkID INTEGER,' +
			'FieldName TEXT(50),' +
			'Changed LOGICAL,' +
			'OldValue TEXT(50),' +
			'NewValue TEXT(50),' +
			'Comment TEXT(100),' +
			'UserName TEXT(50),' +
			'UserTime DATETIME,' +
			'CONSTRAINT idxPrimary PRIMARY KEY (LinkID, FieldName));';
		adoChangesCommand.Execute;

		adoChangesCommand.CommandText := 'CREATE TABLE NodeChanges (' +
			'Node TEXT(6),' +
			'FieldName TEXT(50),' +
			'Changed LOGICAL,' +
			'OldValue TEXT(50),' +
			'NewValue TEXT(50),' +
			'Comment TEXT(100),' +
			'UserName TEXT(50),' +
			'UserTime DATETIME,' +
			'CONSTRAINT idxPrimary PRIMARY KEY (Node, FieldName));';
		adoChangesCommand.Execute;

    // Set AllowZeroLength property to true
    ACatalog.Set_ActiveConnection(adoChangesConnection.ConnectionString);
    ACatalog.Tables.Refresh;
    ACatalog.Tables['LinkChanges'].Columns['OldValue'].Properties['Jet OLEDB:Allow Zero Length'].Value := True;
    ACatalog.Tables['LinkChanges'].Columns['NewValue'].Properties['Jet OLEDB:Allow Zero Length'].Value := True;
    ACatalog.Tables['LinkChanges'].Columns['Comment'].Properties['Jet OLEDB:Allow Zero Length'].Value := True;

    ACatalog.Tables['NodeChanges'].Columns['OldValue'].Properties['Jet OLEDB:Allow Zero Length'].Value := True;
    ACatalog.Tables['NodeChanges'].Columns['NewValue'].Properties['Jet OLEDB:Allow Zero Length'].Value := True;
    ACatalog.Tables['NodeChanges'].Columns['Comment'].Properties['Jet OLEDB:Allow Zero Length'].Value := True;

    ACatalog.Set_ActiveConnection(adoLinksConnection.ConnectionString);
    ACatalog.Tables.Refresh;
    ACatalog.Tables[LinksName].Columns['Material'].Properties['Jet OLEDB:Allow Zero Length'].Value := True;
    ACatalog.Tables[LinksName].Columns['Shape'].Properties['Jet OLEDB:Allow Zero Length'].Value := True;
    ACatalog.Tables[LinksName].Columns['AsBuilt'].Properties['Jet OLEDB:Allow Zero Length'].Value := True;
	end
	// Otherwise, just connect to it
	else
	begin
		adoChangesConnection.ConnectionString := ChangesConnectionString;
		adoChangesConnection.Open;
	end;

	//Test to see if Trace field is in Access database; if not, add it
	TableList := TStringList.Create;
	adoChangesConnection.GetTableNames(TableList);
	if TableList.IndexOf('Traced') = -1 then
	begin
		mnuStatusStatus.Caption := 'Creating Traced Table';
		Refresh;
		adoChangesCommand.CommandText := 'CREATE TABLE Traced (' +
			'LinkID INTEGER,' +
			'Traced TEXT(1),' +
			'CONSTRAINT idxPrimary PRIMARY KEY (LinkID));';
		adoChangesCommand.Execute;
	end;
	adoTraced.TableName := 'Traced';
	adoTraced.Open;
	TableList.Free;

	adoLinkChanges.TableName := 'LinkChanges';
	adoLinkChanges.Open;
	adoLinkDetailChanges.TableName := 'LinkChanges';
	adoLinkDetailChanges.Open;
  adoNodeChanges.TableName := 'NodeChanges';
  adoNodeChanges.Open;
	adoUSNodeDetailChanges.TableName := 'NodeChanges';
	adoUSNodeDetailChanges.Open;
	adoDSNodeDetailChanges.TableName := 'NodeChanges';
	adoDSNodeDetailChanges.Open;
  adoDataSources.Open;

  SetupMap(ADirectory);
  OpenDBs;
  ZoomExtents;

{	mnuStatusStatus.Caption := 'Opening Network Table...';
	mnuStatusProgress.Position := 75;
	Refresh;
	adoNetwork.TableName := NetworkTableName;
	adoNetwork.Open;
	mnuStatusStatus.Caption := 'Opening Upstreams Table...';
	mnuStatusProgress.Position := 83;
	Refresh;
	adoUpstreams.TableName := adoNetwork.TableName;
	adoUpstreams.Open;
	mnuStatusStatus.Caption := 'Opening Downstreams Table...';
	mnuStatusProgress.Position := 91;
	Refresh;
	adoDownstreams.TableName := adoNetwork.TableName;
	adoDownstreams.Open;
	mnuStatusStatus.Caption := 'Opening Nodes Table...';
	mnuStatusProgress.Position := 91;
	Refresh;
	adoNodes.TableName := NodesName;
	adoNodes.Open;
	mnuStatusStatus.Caption := 'Opening UpNodes Table...';
	mnuStatusProgress.Position := 91;
	Refresh;
	adoUpNodes.TableName := NodesName;
	adoUpNodes.Open;
	mnuStatusStatus.Caption := 'Opening DnNodes Table...';
	mnuStatusProgress.Position := 91;
	Refresh;
	adoDnNodes.TableName := NodesName;
	adoDnNodes.Open;}

	mnuStatusStatus.Caption := '';
	lblDatabase.Caption := ' '+ADirectory;
  ModelDirectory := ADirectory;
  OpenedDB := True;
end;

procedure TfrmMain.ResetSelection;
var
	i: Integer;
	OldSelectionList: TObjectList;
begin
	OldSelectionList := TObjectList.Create(False);
	try
		for i := 0 to FSelected.Count-1  do
			OldSelectionList.Add(FSelected[i]);
		FSelected.Clear;
    for i := 0 to OldSelectionList.Count-1 do    // Iterate
      InvalidateObj(TDocObj(OldSelectionList[i]));
  finally
		OldSelectionList.Free;
		OldSelectionList := nil;
  end;  // try/finally
	Update;
end;

procedure TfrmMain.Select(AObj: TDocObj; AddToSelection: Boolean);
begin
	if AddToSelection then
		SelectedList.Add(AObj)
	else
	begin
		ResetSelection;
		SelectedList.Add(AObj);
	end;
end;

procedure TfrmMain.SelectInRect(ARect: TRect; AddToSelection: Boolean);
var
	i: Integer;
	SelectRect: TDRectangle;
	TestRect: TDRectangle;
begin
{	if not AddToSelection then
		ResetSelection;

	SelectRect := TDRectangle.Create;
	try
		SelectRect.SetRect(FGrid.ClientToGrid(ARect.TopLeft),
			FGrid.ClientToGrid(ARect.BottomRight));
		SelectRect.NormalizeRect;
		TestRect := TDRectangle.Create;
		try
			for i := 0 to Model.LinkList.Count-1 do
			begin
				TestRect.CopyRect(SelectRect);
				TestRect.UnionRect(Model.Links[i].Position);
				TestRect.NormalizeRect;
				if SelectRect.EqualRect(TestRect) then
					if FSelected.IndexOf(Model.Links[i]) < 0 then
						Select(Model.Links[i], True)
					else
						FSelected.Remove(Model.Links[i]);
			end;
			if FSelected.Count > 0 then
			begin
				OffsetRect(ARect, DrawBox.Left, DrawBox.Top);
				InvalidateRect(Handle, @ARect, False);
				Update;
			end;
		finally
			TestRect.Free;
		end;
	finally
		SelectRect.Free;
	end;}
end;

procedure TfrmMain.SetDrawSimple(Value: Boolean);
begin
	if FDrawSimple <> Value then
	begin
		FDrawSimple := Value;
		Grid.UpdateGrid;
	end;
end;

procedure TfrmMain.SetLocked(Value: Boolean);
begin
	if FLocked <> Value then
	begin
		FLocked := Value;
//		pnlLocked.Visible := FLocked;
	end
end;

procedure TfrmMain.SetPath(Value: TPath);
begin
  if FPath <> Value then
    FPath := Value;
end;

procedure TfrmMain.SetSnap(Value: TSnapSet);
begin

end;

procedure TfrmMain.UpdateObj(AObj: TDocObj);
begin

end;

procedure TfrmMain.UpdateView(Clipper: TDRectangle);
begin

end;

procedure TfrmMain.UpdateViewClient(Clipper: TRect);
begin

end;

procedure TfrmMain.ViewChanged;
begin
	//Refresh;
	Update;
end;

procedure TfrmMain.WmSetcursor(var Msg: TWMSetCursor);
var
	CurPos: TPoint;
	GridCurPos: TPoint;
	Rect: TRectangle;
	HitObj: TDocObj;
begin
	GetCursorPos(CurPos);
	CurPos := ScreenToClient(CurPos);
	GridCurPos := CurPos;
	OffsetPt(GridCurPos, Types.Point(-DrawBox.Left-RzPanel1.Left-RzPanel2.Left-RzPanel4.Left,
		-DrawBox.Top-RzPanel1.Top-RzPanel2.Top-RzPanel4.Top));
	if Msg.HitTest = htClient then
	begin
		Rect := TRectangle.Create(DrawBox.Left, DrawBox.Top,
			DrawBox.Left+DrawBox.Width-1, DrawBox.Top+DrawBox.Height-1);
		Rect.OffsetRect(RzPanel1.Left+RzPanel2.Left+RzPanel4.Left,
			RzPanel1.Top+RzPanel2.Top+RzPanel4.Top);
		try
			if Rect.PtInRect(CurPos) then
			begin
				if CurrentTool.ToolName = 'ProfileSelect' then
				begin
					HitObj := ObjectAt(Grid.ClientToGrid(GridCurPos));
					if HitObj is TConduit then
					begin
						if (SelectedList.Count =1) and (SelectedList.IndexOf(HitObj) = 0) then
							case HitObj.HitTest(Grid.ClientToGrid(GridCurPos)) of
								0: SetCursor(Screen.Cursors[crSelectLink]);
								1,2,7,8: SetCursor(Screen.Cursors[crCrElevEd]);
								3,4: SetCursor(Screen.Cursors[crOfElevEd]);
								5: SetCursor(Screen.Cursors[crCondShift]);
								6: SetCursor(Screen.Cursors[crNodeShift]);
								9,11: SetCursor(Screen.Cursors[crInUp]);
								10,12: SetCursor(Screen.Cursors[crInDn]);
							end
						else
							SetCursor(Screen.Cursors[crSelectLink])
					end
					else
						SetCursor(Screen.Cursors[crPlanSelect]);
				end
				else if CurrentTool.ToolName = 'Zoom' then
					SetCursor(Screen.Cursors[crZoom])
				else if CurrentTool.ToolName = 'Pan' then
					SetCursor(Screen.Cursors[crPan])
				else if CurrentTool.ToolName = 'Line' then
					SetCursor(Screen.Cursors[crLine]);
			end
			else
				inherited;
		finally
			Rect.Free;
		end;
	end
	else
		inherited;
	Msg.Result := 1;
end;

procedure TfrmMain.DrawBoxPaint(Sender: TObject);
begin
	if Path = nil then
		with DrawBox do
		begin
			Clipper.SetRect(Grid.ClientToGrid(Canvas.ClipRect.TopLeft),
				Grid.ClientToGrid(Canvas.ClipRect.BottomRight));
			OffScreen.Width := Width;
			OffScreen.Height := Height;
			Grid.Draw(Offscreen.Canvas, Clipper);
			Canvas.CopyRect(Canvas.ClipRect, OffScreen.Canvas, Canvas.ClipRect);
			Exit;
		end;
	with DrawBox do
	begin
		Clipper.SetRect(Grid.ClientToGrid(Canvas.ClipRect.TopLeft),
			Grid.ClientToGrid(Canvas.ClipRect.BottomRight));
//		OffScreen := TBitmap.Create;
		OffScreen.Width := Width;
		OffScreen.Height := Height;
		// Draw annotations
		// Draw the grid
		Grid.Draw(Offscreen.Canvas, Clipper);
		// Draw model
		CurrentHorizPos := 0;
		Path.Draw(Offscreen.Canvas, []);
		// Paint it in!
		Canvas.CopyRect(Canvas.ClipRect, OffScreen.Canvas, Canvas.ClipRect);
//		OffScreen.Free;
	end;
end;

procedure TfrmMain.FormDestroy(Sender: TObject);
begin
	PathSelectionList.Free;
	Clipper.Free;
	FGrid.Free;
	Offscreen.Free;
	FSelected.Free;
	HintWin.Free;
	adoLinksConnection.Close;
	dxBarManager1.SaveToRegistry(dxBarManager1.RegistryPath);
	AppRegistry.WriteBool('', 'ShowToolbarCaptions', actViewShowToolbarCaptions.Checked);
	AppRegistry.Free;
	ModelRegistry.Free;
	mapNetwork.Datasets.RemoveAll;
end;

procedure TfrmMain.DrawBoxMouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
begin
  CurrentTool.MouseDown(Sender, Button, Shift, X, Y);
end;

procedure TfrmMain.DrawBoxMouseMove(Sender: TObject; Shift: TShiftState; X,
	Y: Integer);
begin
	ShiftState := Shift;
	CurrentTool.MouseMove(Sender, Shift, X, Y);
end;

procedure TfrmMain.DrawBoxMouseUp(Sender: TObject; Button: TMouseButton;
	Shift: TShiftState; X, Y: Integer);
begin
	ShiftState := Shift;
	CurrentTool.MouseUp(Sender, Button, Shift, X, Y);
	if HintWin.HandleAllocated then
    HintWin.ReleaseHandle;
end;

procedure TfrmMain.DrawBoxDblClick(Sender: TObject);
begin
	CurrentTool.DblClick;
end;

procedure TfrmMain.mnuStatusCancelClick(Sender: TObject);
begin
	SearchCanceled := True;
end;

procedure TfrmMain.actNetworkSaveProfileExecute(Sender: TObject);
var
	i: Integer;
	AConduit: TConduit;
	DlgResult: Word;
	IndCatStyle: CMapXStyle;
	TracedTheme: CMapXTheme;
begin
{	if Length(edtComment.Text) = 0 then
	begin
		DlgResult := MessageDlg('Comment for change is empty; enter a comment?', mtWarning,
			mbYesNoCancel, 0);
		case DlgResult of
			mrYes:
				begin
					edtComment.Text := InputBox('Change comment', 'Enter reason for change', '');
				end;
			mrCancel: Exit;
		end;
	end;}

	// Rifle through the path and commit the conduits
	Screen.Cursor := crHourglass;
	for i := 0 to Path.Objects.Count-1 do
	begin
		AConduit := TConduit(Path.Objects[i]);
		AConduit.SaveToDB;
		mnuStatusStatus.Caption := Format('Saving Conduit %d:%s-%s', [AConduit.CompKey,
			AConduit.UpJunc, AConduit.DnJunc]);
		try
			if not adoTraced.Locate('LinkID', AConduit.LinkID, [loCaseInsensitive]) then
			begin
				adoTraced.Append;
				adoTraced.FieldByName('LinkID').Value := AConduit.LinkID;
			end
			else
			begin
				adoTraced.Edit;
			end;
			adoTraced.FieldByName('Traced').Value := 'Y';
			adoTraced.Post;
		except
			on EOleException do ;
		end;
	end;
	edtComment.Text := '';
	mnuStatusStatus.Caption := 'Updating traced lines';
	Refresh;
	mapNetwork.Datasets.Item('Traced').Refresh;
	if not TraceItemsAvailable then
	begin
		mapNetwork.Datasets.Item('Traced').Themes.RemoveAll;
		mapNetwork.Datasets.Item('Traced').Themes.Add(miThemeIndividualValue, 'Traced',
			'By Traced', True);
		TracedTheme := mapNetwork.Datasets.Item('Traced').Themes.Item('By Traced');
		IndCatStyle := TracedTheme.ThemeProperties.IndividualValueCategories.Item(1).Style;
		IndCatStyle.LineWidth := 2;
		IndCatStyle.LineStyle := 89;
		TraceItemsAvailable := True;
	end;
	adoLinkDetailChanges.Close;
	adoLinkDetailChanges.Open;
  adoUSNodeDetailChanges.Close;
  adoUSNodeDetailChanges.Open;
  adoDSNodeDetailChanges.Close;
  adoDSNodeDetailChanges.Open;
  adoUpNodes.Close;
  adoUpNodes.Open;
  adoDnNodes.Close;
  adoDnNodes.Open;
	RecalcView;

	DrawBox.Refresh;
	Modified := False;
	mnuStatusStatus.Caption := 'Saved profile';
	Screen.Cursor := crDefault;
end;

procedure TfrmMain.pnlPropertiesHotSpotClick(Sender: TObject);
begin
	pnlPropertiesHolder.Visible := not pnlProperties.HotSpotClosed;
//	inspNetwork.Visible := not pnlProperties.HotSpotClosed;
	pnlPropertiesData.Visible := not pnlProperties.HotSpotClosed;
	RzLabel1.Visible := not pnlProperties.HotSpotClosed;
end;

procedure TfrmMain.actNetworkSaveProfileUpdate(Sender: TObject);
begin
	actNetworkSaveProfile.Enabled := Modified;
end;

procedure TfrmMain.actFileOpenDatabaseExecute(Sender: TObject);
begin
	mnuStatusProgress.Visible := ivAlways;

  dlgBrowser.Title := 'Browse for Model Folder; Directory needs a MODEL.INI file to be valid.';

  { TODO : Grab directory from registry }
  dlgBrowser.SelectedPathName := AppRegistry.ReadString('', AppRegistryDirectoryKey, 'C:\');
  if dlgBrowser.Execute then
  begin
		OpenTraceFile(dlgBrowser.SelectedPathName);
    AppRegistry.WriteString('', AppRegistryDirectoryKey, dlgBrowser.SelectedPathName);
  end;

	mnuStatusProgress.Visible := ivNever;

end;


{ TAdjacent }

constructor TAdjacent.Create;
begin
	inherited Create;
	FConduit := nil;
end;

destructor TAdjacent.Destroy;
begin
	if Assigned(FConduit) then
		FreeAndNil(FConduit);
	inherited;
end;


procedure TfrmMain.actViewExtentsExecute(Sender: TObject);
begin
	RecalcView;	
end;

procedure TfrmMain.actNetworkFuturePipesExecute(Sender: TObject);
begin
	//
end;

procedure TfrmMain.actNetworkTracePathUpdate(Sender: TObject);
var
	SelectionSet: CMapXSelection;
begin
	if mapNetwork.Layers.Count > 0 then
	begin
		SelectionSet := mapNetwork.Layers.Item(NetworkTableName).Selection;
		actNetworkTracePath.Enabled := SelectionSet.Count > 0;
	end
	else
		actNetworkTracePath.Enabled := False;
end;

procedure TfrmMain.actViewAutoExtentsExecute(Sender: TObject);
begin
	RecalcView;
end;

procedure TfrmMain.actViewAutoExtentsUpdate(Sender: TObject);
begin
	actViewAutoExtents.Enabled := Assigned(FPath);
	actViewAutoExtents.Checked := mnuViewAutoExtents.Down;
end;

procedure TfrmMain.actViewExtentsUpdate(Sender: TObject);
begin
	actViewExtents.Enabled := Assigned(FPath);
end;

procedure TfrmMain.pnlMapResize(Sender: TObject);
begin
	if actViewAutoExtents.Checked then
		RecalcView;
end;

procedure TfrmMain.actNetworkInterpolateExecute(Sender: TObject);
var
	UpObj, DnObj: TConduit;
	UpPos, DnPos: Integer;
	TotalLength: Double;
	LengthOfInterp: Double;
	SlopeOfInterp: Double;
	CurrentObj: TConduit;
	i: Integer;
begin
	UpPos := Path.PosOfObj(TModelObj(Selected[0]));
	DnPos := Path.PosOfObj(TModelObj(Selected[1]));
	if UpPos > DnPos then
		Swap(UpPos, DnPos);
	UpObj := TConduit(Path.Objects[UpPos]);
	DnObj := TConduit(Path.Objects[DnPos]);
	TotalLength := Path.LengthBetweenObjects(UpObj, DnObj)+DnObj.Length;
	SlopeOfInterp := (UpObj.UpInvert - DnObj.DnInvert)/TotalLength;
	for i := UpPos to DnPos do
	begin
		CurrentObj := TConduit(Path.Objects[i]);
		if i = UpPos then //Update downstream invert only
    begin
			CurrentObj.DnInvert := StrToFloat(Format('%.2f', [UpObj.UpInvert-UpObj.Length*SlopeOfInterp]));
      CurrentObj.SetDataSourceItem(5,'I');
    end
		else if i = DnPos then //Update upstream invert only
    begin
			CurrentObj.UpInvert := StrToFloat(Format('%.2f', [DnObj.DnInvert+DnObj.Length*SlopeOfInterp]));
      CurrentObj.SetDataSourceItem(4,'I');
    end
		else
		begin
			CurrentObj.UpInvert := StrToFloat(Format('%.2f', [UpObj.UpInvert-
				Path.LengthBetweenObjects(UpObj, CurrentObj)*SlopeOfInterp]));
			CurrentObj.DnInvert := StrToFloat(Format('%.2f', [UpObj.UpInvert-
				(Path.LengthBetweenObjects(UpObj, CurrentObj)+CurrentObj.Length)*SlopeOfInterp]));
      CurrentObj.SetDataSourceItem(4,'I');
      CurrentObj.SetDataSourceItem(5,'I');
		end;
	end;
  btnDataSource.Caption := FormatDataSourceString(UpObj.FDataSource);
	RecalcView;
	DrawBox.Refresh;
	Modified := True;
end;

procedure TfrmMain.actNetworkInterpolateUpdate(Sender: TObject);
begin
	actNetworkInterpolate.Enabled := SelectedList.Count = 2;
end;

procedure TfrmMain.actNetworkRevertPathExecute(Sender: TObject);
var
	i: Integer;
begin
	Screen.Cursor := crHourglass;
	mnuStatusStatus.Caption := 'Reverting Profile';
	Refresh;
	for i := 0 to Path.Objects.Count-1 do
		TConduit(Path.Objects[i]).RecallFromDB;
	RecalcView;
	DrawBox.Refresh;
	Modified := False;
	mnuStatusStatus.Caption := 'Profile successfully reverted';
	Screen.Cursor := crDefault;
end;

procedure TfrmMain.actNetworkRevertPathUpdate(Sender: TObject);
begin
	actNetworkRevertPath.Enabled := Modified;
end;

procedure TfrmMain.FormResize(Sender: TObject);
begin
	if actViewAutoExtents.Checked then
		RecalcView;
end;

procedure TfrmMain.actViewAutoLabelsExecute(Sender: TObject);
begin
	mapNetwork.Layers.Item(NetworkTableName).AutoLabel := actViewAutoLabels.Checked;
end;

procedure TfrmMain.actViewAutoLabelsUpdate(Sender: TObject);
begin
	actViewAutoLabels.Enabled := mapNetwork.Layers.Count > 0;
	actViewAutoLabels.Checked := mnuViewAutoLabels.Down;
end;

procedure TfrmMain.actNetworkFixGroundToRimDepthExecute(Sender: TObject);
var
	UpObj, DnObj: TConduit;
	UpPos, DnPos: Integer;
	NextObj, PrevObj: TConduit;
	TotalLength: Double;
	LengthOfInterp: Double;
	SlopeOfInterp: Double;
	CurrentObj: TConduit;
	i: Integer;
begin
	UpPos := Path.PosOfObj(TModelObj(Selected[0]));
	DnPos := Path.PosOfObj(TModelObj(Selected[1]));
	if UpPos > DnPos then
		Swap(UpPos, DnPos);
	UpObj := TConduit(Path.Objects[UpPos]);
	DnObj := TConduit(Path.Objects[DnPos]);
	for i := UpPos to DnPos do
	begin
		if i > 0 then
			PrevObj := TConduit(Path.Objects[i-1])
		else
			PrevObj := nil;
		if i < Path.Objects.Count-1 then
			NextObj := TConduit(Path.Objects[i+1])
		else
			NextObj := nil;

		CurrentObj := TConduit(Path.Objects[i]);
		if Assigned(PrevObj) then
			CurrentObj.UpGround := StrToFloat(Format('%.2f', [Max(CurrentObj.UpInvert+CurrentObj.UpDepth,PrevObj.DnInvert+PrevObj.DnDepth)]))
		else
			CurrentObj.UpGround := StrToFloat(Format('%.2f', [CurrentObj.UpInvert+CurrentObj.UpDepth]));

		if Assigned(NextObj) then
			CurrentObj.DnGround := StrToFloat(Format('%.2f', [Max(CurrentObj.DnInvert+CurrentObj.DnDepth,NextObj.UpInvert+NextObj.UpDepth)]))
		else
			CurrentObj.DnGround := StrToFloat(Format('%.2f', [CurrentObj.DnInvert+CurrentObj.DnDepth]));;
	end;
	RecalcView;
	DrawBox.Repaint;
	Modified := True;
end;

procedure TfrmMain.actNetworkFixGroundToRimDepthUpdate(Sender: TObject);
begin
	actNetworkFixGroundToRimDepth.Enabled := SelectedList.Count = 2;
end;

procedure TfrmMain.actViewLayersExecute(Sender: TObject);
begin
	mapNetwork.Layers.LayersDlg(EmptyParam, EmptyParam);
end;

procedure TfrmMain.actViewLineDirectionsExecute(Sender: TObject);
begin
	mapNetwork.Layers.Item(NetworkTableName).ShowLineDirection := actViewLineDirections.Checked;
end;

procedure TfrmMain.actViewLineDirectionsUpdate(Sender: TObject);
begin
	actViewLineDirections.Enabled := mapNetwork.Layers.Count > 0;
	actViewLineDirections.Checked := mnuViewLineDirection.Down;
end;

procedure TfrmMain.actViewLayersUpdate(Sender: TObject);
begin
	actViewLayers.Enabled := mapNetwork.Layers.Count > 0;
end;

procedure TfrmMain.adoNetworkMoveComplete(DataSet: TCustomADODataSet;
	const Reason: TEventReason; const Error: Error;
	var EventStatus: TEventStatus);
var
	AConduit: TConduit;
begin
	CodeSite.EnterMethod('adoNetworkMoveComplete');
	AConduit := nil;
  if not Assigned(Path) then
  begin
  	CodeSite.ExitMethod('adoNetworkMoveComplete');
    Exit;
  end;
	if (Path.Objects.Count > 0) then
	begin
		if (SelectedList.Count >= 1) then
			AConduit := TConduit(SelectedList[0])
		else if Assigned(ProfileSelectTool.FlyOverObj) then
			AConduit := TConduit(ProfileSelectTool.FlyOverObj);
		if Assigned(AConduit) then
			with AConduit do
			begin
				FillingPropertiesData := True;
				edtCompKey.IntValue := CompKey;
				edtUpNode.Text := UpJunc;
				edtUpType.Text := UpType;
				edtDnNode.Text := DnJunc;
				edtDnType.Text := DnType;
        edtLinkID.Value := LinkID;
				edtLinkType.Text := LinkType;
				edtDnGround.Value := DnGround;
				edtUpGround.Value := UpGround;
				edtDnInvert.Value := DnInvert;
				edtUpInvert.Value := UpInvert;
				edtUpDepth.Value := UpDepth;
				edtDnDepth.Value := DnDepth;
				edtLength.Value := Length;
				edtVertDim.Value := VertDim*12;
				edtAsBuilt.Text := AsBuilt;
        edtHorizDim.Value := HorizDim*12;
        edtShape.Text := Shape;
        edtPipeType.Text := PipeType;
        if (FDataSource = '') then
        begin
          btnDataSource.Caption := 'No data source info';
          CodeSite.SendMsg('No data source info');
        end
        else
        begin
        	btnDataSource.Caption := FormatDataSourceString(FDataSource);
          CodeSite.SendMsg('Has data source info internally')
        end;
        edtFlowType.Text := FlowType;
        edtVertDim.Enabled := Shape <> 'CIRC';
        chkSpecLink.Checked := IsSpecLink;
				FillingPropertiesData := False;
			end
		else
			begin
				FillingPropertiesData := True;
				edtCompKey.IntValue := 0;
				edtUpNode.Text := '';
				edtUpType.Text := '';
				edtDnNode.Text := '';
				edtDnType.Text := '';
        edtLinkID.Value := 0;
				edtLinkType.Text := '';
				edtDnGround.Value := 0;
				edtUpGround.Value := 0;
				edtDnInvert.Value := 0;
				edtUpInvert.Value := 0;
				edtUpDepth.Value := 0;
				edtDnDepth.Value := 0;
				edtLength.Value := 0;
				edtVertDim.Value := 0;
				edtAsBuilt.Text := '';
        edtHorizDim.Value := 0;
        edtShape.Text := '';
        edtPipeType.Text := '';
        edtFlowType.Text := '';
        edtVertDim.Enabled := True;
        chkSpecLink.Checked := False;
				FillingPropertiesData := False;
			end;
	end;
  CodeSite.ExitMethod('adoNetworkMoveComplete');
end;

procedure TfrmMain.edtUpGroundChange(Sender: TObject);
var
	AConduit: TConduit;
begin
	if (SelectedList.Count = 1) and not FillingPropertiesData then
	begin
		AConduit := TConduit(SelectedList[0]);
		AConduit.UpGround := edtUpGround.Value;
		AConduit.DnGround := edtDnGround.Value;
		AConduit.UpInvert := edtUpInvert.Value;
		AConduit.DnInvert := edtDnInvert.Value;
    AConduit.Length := edtLength.Value;
    AConduit.FlowType := edtFlowType.Text;
    AConduit.CompKey := edtCompKey.IntValue;
    AConduit.VertDim := edtVertDim.Value/12;
    AConduit.HorizDim := edtHorizDim.Value/12;
    AConduit.Shape := edtShape.Text;
    edtVertDim.Enabled := AConduit.Shape <> 'CIRC';
    AConduit.PipeType := edtPipetype.Text;
    AConduit.AsBuilt := edtAsBuilt.Text;
    AConduit.UpType := edtUpType.Text;
    AConduit.DnType := edtDnType.Text;
		RecalcView;
		DrawBox.Refresh;
		Modified := True;
	end;
end;

procedure TfrmMain.actNetworkFixInvertsToDepthsExecute(Sender: TObject);
var
	UpObj, DnObj: TConduit;
	UpPos, DnPos: Integer;
	NextObj, PrevObj: TConduit;
	TotalLength: Double;
	LengthOfInterp: Double;
	SlopeOfInterp: Double;
	CurrentObj: TConduit;
	i: Integer;
begin
	UpPos := Path.PosOfObj(TModelObj(Selected[0]));
	DnPos := Path.PosOfObj(TModelObj(Selected[1]));
	if UpPos > DnPos then
		Swap(UpPos, DnPos);
	UpObj := TConduit(Path.Objects[UpPos]);
	DnObj := TConduit(Path.Objects[DnPos]);
	for i := UpPos to DnPos do
	begin
		if i > 0 then
			PrevObj := TConduit(Path.Objects[i-1])
		else
			PrevObj := nil;
		if i < Path.Objects.Count-1 then
			NextObj := TConduit(Path.Objects[i+1])
		else
			NextObj := nil;

		CurrentObj := TConduit(Path.Objects[i]);
		if Assigned(PrevObj) then
			CurrentObj.UpInvert := StrToFloat(Format('%.2f', [Min(CurrentObj.UpGround-CurrentObj.UpDepth,PrevObj.DnGround-PrevObj.DnDepth)]))
		else
			CurrentObj.UpInvert := StrToFloat(Format('%.2f', [CurrentObj.UpGround-CurrentObj.UpDepth]));
    CurrentObj.SetDataSourceItem(4,'D');
    btnDataSource.Caption := FormatDataSourceString(CurrentObj.FDataSource);

		if Assigned(NextObj) then
			CurrentObj.DnInvert := StrToFloat(Format('%.2f', [Min(CurrentObj.DnGround-CurrentObj.DnDepth,NextObj.UpGround-NextObj.UpDepth)]))
		else
			CurrentObj.DnInvert := StrToFloat(Format('%.2f', [CurrentObj.DnGround-CurrentObj.DnDepth]));
    CurrentObj.SetDataSourceItem(5,'D');
    btnDataSource.Caption := FormatDataSourceString(CurrentObj.FDataSource);
	end;
	RecalcView;
	DrawBox.Repaint;
	Modified := True;
end;

procedure TfrmMain.actNetworkFixInvertsToDepthsUpdate(Sender: TObject);
begin
	actNetworkFixInvertsToDepths.Enabled := SelectedList.Count = 2;
end;

procedure TfrmMain.DrawBoxKeyUp(Sender: TObject; var Key: Word;
	Shift: TShiftState);
var
	SelIdxToShift: Integer;
	UpdateRect: TRect;
	Layer: CMapXLayer;
	Selection: CMapXSelection;
	i: Integer;
	OutList: TList;
	DlgResult: Word;
begin
	if not Assigned(FPath) or (not (ssCtrl in Shift)) {or (ActiveControl <> DrawBox)} then
		Exit;
	case Key of
		VK_HOME: edtFlowType.SetFocus;
		VK_END: edtAsBuilt.SetFocus;
		VK_LEFT: actNetworkShiftLeftExecute(Sender);
		VK_RIGHT: actNetworkShiftRightExecute(Sender);
	end;
end;

procedure TfrmMain.adoNetworkPostError(DataSet: TDataSet;
	E: EDatabaseError; var Action: TDataAction);
var
	dlgResult: Word;
begin
	dlgResult := MessageDlg(E.Message, mtError, [mbAbort, mbRetry, mbCancel], 0);
	case dlgResult of
		mrAbort: Action := daAbort;
		mrRetry: Action := daRetry;
		mrCancel:
	end;
end;

procedure TfrmMain.actNetworkFixGroundsToDepthsExecute(Sender: TObject);
var
	UpObj, DnObj: TConduit;
	UpPos, DnPos: Integer;
	NextObj, PrevObj: TConduit;
	TotalLength: Double;
	LengthOfInterp: Double;
	SlopeOfInterp: Double;
	CurrentObj: TConduit;
	i: Integer;
begin
	UpPos := Path.PosOfObj(TModelObj(Selected[0]));
	DnPos := Path.PosOfObj(TModelObj(Selected[1]));
	if UpPos > DnPos then
		Swap(UpPos, DnPos);
	UpObj := TConduit(Path.Objects[UpPos]);
	DnObj := TConduit(Path.Objects[DnPos]);
	for i := UpPos to DnPos do
	begin
		if i > 0 then
			PrevObj := TConduit(Path.Objects[i-1])
		else
			PrevObj := nil;
		if i < Path.Objects.Count-1 then
			NextObj := TConduit(Path.Objects[i+1])
		else
			NextObj := nil;

		CurrentObj := TConduit(Path.Objects[i]);
		if Assigned(PrevObj) then
			CurrentObj.UpGround := Max(CurrentObj.UpInvert+CurrentObj.UpDepth,PrevObj.DnInvert+PrevObj.DnDepth)
		else
			CurrentObj.UpGround := CurrentObj.UpInvert+CurrentObj.UpDepth;

		if Assigned(NextObj) then
			CurrentObj.DnGround := Max(CurrentObj.DnInvert+CurrentObj.DnDepth,NextObj.UpInvert+NextObj.UpDepth)
		else
			CurrentObj.DnGround := CurrentObj.DnInvert+CurrentObj.DnDepth;
    CurrentObj.SetDataSourceItem(6,'D');
    btnDataSource.Caption := FormatDataSourceString(CurrentObj.FDataSource);
	end;
	RecalcView;
	DrawBox.Repaint;
	Modified := True;
end;

procedure TfrmMain.actNetworkFixGroundsToDepthsUpdate(Sender: TObject);
begin
	actNetworkFixGroundsToDepths.Enabled := SelectedList.Count = 2;
end;

procedure TfrmMain.DrawBoxMouseLeave(Sender: TObject);
begin
	CodeSite.SendMsg('Leaving Drawbox');
	if Assigned(ProfileSelectTool.FlyOverObj) then
	begin
		CodeSite.SendMsg('Erasing previous flyover object');
		(ProfileSelectTool.FlyOverObj as TDocObj).DrawTracker(frmMain.DrawBox.Canvas, tsFlyOver);
		ProfileSelectTool.FlyOverObj := nil;
		if SelectedList.Count < 1 then
		begin
			pnlPropertiesData.Hide;
		end
	end;
end;

procedure TfrmMain.actNetworkHighlightPathOnNetworkExecute(
	Sender: TObject);
var
	Layer: CMapXLayer;
	SelectionSet: CMapXSelection;
	i: Integer;
begin
	Layer := mapNetwork.Layers.Item(NetworkTableName);
	SelectionSet := Layer.Selection;
	SelectionSet.ClearSelection;
	Tracing := True;
	for i := 0 to Path.Objects.Count-1 do
		SelectionSet.Add(Layer.GetFeatureByID(TConduit(Path.Objects[i]).MapinfoID));
	Tracing := False;
end;

procedure TfrmMain.actNetworkHighlightPathOnNetworkUpdate(Sender: TObject);
begin
	actNetworkHighlightPathOnNetwork.Enabled := Assigned(FPath);
	actNetworkShiftLeft.Enabled := Assigned(FPath);
	actNetworkShiftRight.Enabled := Assigned(FPath);
end;

procedure TfrmMain.actNetworkShiftRightExecute(Sender: TObject);
var
	SelIdxToShift: Integer;
	UpdateRect: TRect;
	Layer: CMapXLayer;
	Selection: CMapXSelection;
	i: Integer;
	OutList: TList;
	DlgResult: Word;
begin
	if SelectedList.Count = 0 then
		SelectedList.Add(Path.Objects[Path.Objects.Count-1]);
	SelIdxToShift := Path.Objects.IndexOf(SelectedList[0]);
	if SelIdxToShift < Path.Objects.Count-1 then
	begin
		SelectedList.Clear;
		SelectedList.Add(Path.Objects[SelIdxToShift+1]);
		UpdateRect := frmMain.DrawBox.ClientRect;
		InvalidateRect(frmMain.DrawBox.Handle, @UpdateRect, False);
		frmMain.adoNetwork.Locate('LinkID', TConduit(SelectedList[0]).LinkID, [loCaseInsensitive]);
		frmMain.DrawBox.Update;
	end
	else // Add another object
	begin
		OutList := TList.Create;
		for i := 0 to Path.OutAdjacents.Count-1 do
		begin
			if TAdjacent(Path.OutAdjacents[i]).Position = Path.Objects.Count then
				OutList.Add(Path.OutAdjacents[i]);
		end;
		if OutList.Count > 0 then
		begin
			// If profile was modified, prompt to save first
			if Modified then
			begin
				DlgResult := MessageDlg('Save modifications to profile?', mtConfirmation, mbYesNoCancel, 0);
				case DlgResult of
					mrYes: actNetworkSaveProfileExecute(Sender);
					mrNo: actNetworkRevertPathExecute(Sender);
					mrCancel: Exit;
				end;
			end;
			dlgChooseConduit.edtChooseConduit.Items.Clear;
			if OutList.Count > 1 then // Initiate pick
			begin
				for i := 0 to OutList.Count-1 do
					dlgChooseConduit.edtChooseConduit.Items.Add(Format('%d/%d (%s-%s)',
						[TAdjacent(OutList[i]).FConduit.LinkID,
            TAdjacent(OutList[i]).FConduit.CompKey,
						TAdjacent(OutList[i]).FConduit.UpJunc,
						TAdjacent(OutList[i]).FConduit.DnJunc]));
				dlgChooseConduit.edtChooseConduit.ItemIndex := 0;
				dlgChooseConduit.ShowModal;
			end
			else
			begin
				dlgChooseConduit.edtChooseConduit.Items.Add(Format('%d/%d (%s-%s)',
						[TAdjacent(OutList[0]).FConduit.LinkID,
            TAdjacent(OutList[0]).FConduit.CompKey,
						TAdjacent(OutList[0]).FConduit.UpJunc,
						TAdjacent(OutList[0]).FConduit.DnJunc]));
				dlgChooseConduit.edtChooseConduit.ItemIndex := 0;
			end;
			Path.AddDownstreamObj(TAdjacent(OutList[dlgChooseConduit.edtChooseConduit.ItemIndex]).FConduit.LinkID);
			Path.DeleteUpstreamObj;
			Path.ProcessAdjacents;
			SelectedList.Clear;
			SelectedList.Add(Path.Objects[Path.Objects.Count-1]);
			frmMain.adoNetwork.Locate('LinkID', TConduit(SelectedList[0]).LinkID, [loCaseInsensitive]);
			RecalcView;
		end
		else
			mnuStatusStatus.Caption := 'No further conduits downstream';
		OutList.Free;
	end;
	Layer := mapNetwork.Layers.Item(NetworkTableName);
	Selection := Layer.Selection;
	Selection.ClearSelection;
	Selection.Add(Layer.GetFeatureByID(TConduit(SelectedList[0]).MapinfoID));
end;

procedure TfrmMain.actNetworkShiftLeftExecute(Sender: TObject);
var
	SelIdxToShift: Integer;
	UpdateRect: TRect;
	Layer: CMapXLayer;
	Selection: CMapXSelection;
	i: Integer;
	OutList: TList;
	DlgResult: Word;
begin
	if SelectedList.Count = 0 then
		SelectedList.Add(Path.Objects[0]);
	SelIdxToShift := Path.Objects.IndexOf(SelectedList[0]);
	if SelIdxToShift > 0 then
	begin
		SelectedList.Clear;
		SelectedList.Add(Path.Objects[SelIdxToShift-1]);
		UpdateRect := frmMain.DrawBox.ClientRect;
		InvalidateRect(frmMain.DrawBox.Handle, @UpdateRect, False);
		frmMain.adoNetwork.Locate('LinkID', TConduit(SelectedList[0]).LinkID, [loCaseInsensitive]);
		frmMain.DrawBox.Update;
	end
	else // Add another object
	begin
		OutList := TList.Create;
		for i := 0 to Path.InAdjacents.Count-1 do
		begin
			if TAdjacent(Path.InAdjacents[i]).Position = 0 then
				OutList.Add(Path.InAdjacents[i]);
		end;
		if OutList.Count > 0 then
		begin
			// If profile was modified, prompt to save first
			if Modified then
			begin
				DlgResult := MessageDlg('Save modifications to profile?', mtConfirmation, mbYesNoCancel, 0);
				case DlgResult of
					mrYes: actNetworkSaveProfileExecute(Sender);
					mrNo: actNetworkRevertPathExecute(Sender);
					mrCancel: Exit;
				end;
			end;
			dlgChooseConduit.edtChooseConduit.Items.Clear;
			if OutList.Count > 1 then // Initiate pick
			begin
				for i := 0 to OutList.Count-1 do
					dlgChooseConduit.edtChooseConduit.Items.Add(Format('%d/%d (%s-%s)',
						[TAdjacent(OutList[i]).FConduit.LinkID,
            TAdjacent(OutList[i]).FConduit.CompKey,
						TAdjacent(OutList[i]).FConduit.UpJunc,
						TAdjacent(OutList[i]).FConduit.DnJunc]));
				dlgChooseConduit.edtChooseConduit.ItemIndex := 0;
				dlgChooseConduit.ShowModal;
			end
			else
			begin
				dlgChooseConduit.edtChooseConduit.Items.Add(Format('%d/%d (%s-%s)',
						[TAdjacent(OutList[0]).FConduit.LinkID,
            TAdjacent(OutList[0]).FConduit.CompKey,
						TAdjacent(OutList[0]).FConduit.UpJunc,
						TAdjacent(OutList[0]).FConduit.DnJunc]));
				dlgChooseConduit.edtChooseConduit.ItemIndex := 0;
			end;
			Path.AddUpstreamObj(TAdjacent(OutList[dlgChooseConduit.edtChooseConduit.ItemIndex]).FConduit.LinkID);
			Path.DeleteDownstreamObj;
			Path.ProcessAdjacents;
			SelectedList.Clear;
			SelectedList.Add(Path.Objects[0]);
			frmMain.adoNetwork.Locate('LinkID', TConduit(SelectedList[0]).LinkID, [loCaseInsensitive]);
			RecalcView;
		end
		else
			mnuStatusStatus.Caption := 'No further conduits upstream';
		OutList.Free;
	end;
	Layer := mapNetwork.Layers.Item(NetworkTableName);
	Selection := Layer.Selection;
	Selection.ClearSelection;
	Selection.Add(Layer.GetFeatureByID(TConduit(SelectedList[0]).MapinfoID));
end;

procedure TfrmMain.actNetworkDeactivateDatabaseUpdate(Sender: TObject);
begin
	actNetworkDeactivateDatabase.Enabled := (mapNetwork.Layers.Count > 0) or OpenedDB;
	actNetworkDeactivateDatabase.Checked := mnuFileDeactivateDatabase.Down;
end;

procedure TfrmMain.actNetworkDeactivateDatabaseExecute(Sender: TObject);
var
	QualifiedNetworkTableName: String;
	TableList: TStringList;
	IndCatStyle: CMapXStyle;
	TracedTheme: CMapXTheme;
	LayerBounds: CMapXRectangle;
	MapAspect: Double;
	ACatalog: Catalog;
	ChangeTable: Table;
	ChangeCreate: TDateTime;
	NetworkTable: Table;
	NetworkCreate: TDateTime;
	DlgResult: Word;
begin
	if mnuFileDeactivateDatabase.Down then
	begin
		if Modified then
		begin
			DlgResult := MessageDlg('Save modifications to profile?', mtConfirmation, mbYesNoCancel, 0);
			case DlgResult of
				mrYes: actNetworkSaveProfileExecute(Sender);
				mrCancel: Exit;
			end;
		end;

{		adoLinksConnection.Close;
    adoNodesConnection.Close;

		adoNetwork.Close;
		adoUpstreams.Close;
		adoDownstreams.Close;
    adoDnNodes.Close;
    adoUpNodes.Close;
		adoLinkChanges.Close;
		adoLinkDetailChanges.Close;}

    CloseDBs;

		MapZoom := mapNetwork.Zoom;
		MapCenterX := mapNetwork.CenterX;
		MapCenterY := mapNetwork.CenterY;
		mapNetwork.Datasets.RemoveAll;
		mapNetwork.Layers.RemoveAll;

		if Assigned(FPath) then
			FreeAndNil(FPath);
		DrawBox.Refresh;
    actNetworkDeactivateDatabase.Caption := 'Activate Database';
    actNetworkDeactivateDatabase.ImageIndex := 101;
    mnuFileDeactivateDatabase.LargeImageIndex := 101;
	end
	else
	begin
		mnuStatusProgress.Visible := ivAlways;

{		adoTraced.Open;
		adoLinkChanges.Open;
		adoLinkDetailChanges.Open;
    adoNodeChanges.Open;
    adoDSNodeDetailChanges.Open;
    adoUSNodeDetailChanges.Open;}

    OpenDBs;
    SetupMap(ModelDirectory);
    mapNetwork.ZoomTo(MapZoom, MapCenterX, MapCenterY);
		actToolboxSelectExecute(Sender);
    mnuToolboxSelect.Down := True;
    mnuFileDeactivateDatabase.Down := False;
    actNetworkDeactivateDatabase.Caption := 'Deactivate Database';
    actNetworkDeactivateDatabase.ImageIndex := 96;
    mnuFileDeactivateDatabase.LargeImageIndex := 96;

{		mnuStatusStatus.Caption := 'Opening Network Table...';
		mnuStatusProgress.Position := 75;
		Refresh;
		adoNetwork.TableName := NetworkTableName;
		adoNetwork.Open;
		mnuStatusStatus.Caption := 'Opening Upstreams Table...';
		mnuStatusProgress.Position := 83;
		Refresh;
		adoUpstreams.TableName := adoNetwork.TableName;
		adoUpstreams.Open;
		mnuStatusStatus.Caption := 'Opening Downstreams Table...';
		mnuStatusProgress.Position := 91;
		Refresh;
		adoDownstreams.TableName := adoNetwork.TableName;
		adoDownstreams.Open;

		mnuStatusStatus.Caption := '';
		lblDatabase.Caption := ' '+dlgOpen.Filename;}
	end;
end;

procedure TfrmMain.actFilePrintExecute(Sender: TObject);
begin
  PreviewWindow1.Show;
end;

procedure TfrmMain.PrintJob1Draw(Sender: TObject; TheCanvas: TCanvas;
  PageIndex: Integer; Rect: TRect; Area: TDrawArea; Target: TDrawTarget);
var
  ACanvas: TMetafileCanvas;
  AMetafile: TMetafile;
begin
  if Area=daPage then
  begin
    if Path <> nil then
      with DrawBox do
      begin
        PrintRect := Rect;
        AMetafile := TMetafile.Create;
        ACanvas := TMetafileCanvas.Create(AMetafile, Printer.Handle);
				ACanvas.Font.PixelsPerInch:=PrintJob1.DPIX;
				if ACanvas.Font.PixelsPerInch > PrintJob1.DPIY then
					ACanvas.Font.PixelsPerInch := PrintJob1.DPIY;
				try
					Path.Draw(ACanvas, [], True);
					ACanvas.Free;
					TheCanvas.Draw(0, 0, AMetafile);
				finally
          AMetafile.Free;
        end;
      end;
  end;
end;

procedure TfrmMain.actFilePrintUpdate(Sender: TObject);
begin
  actFilePrint.Enabled := Assigned(FPath);
end;

procedure TfrmMain.mnuFileMRUFilesClick(Sender: TObject);
var
	AFileName: String;
begin
	AFileName := mnuFileMRUFiles.Items[mnuFileMRUFiles.ItemIndex];
	if FileExists(AFileName) then
	else
    MessageDlg('File "'+AFileName+'" not found.', mtError, [mbOK], 0); 
end;

procedure TfrmMain.pnlChangesHotSpotClick(Sender: TObject);
begin
  grdChanges.Visible := not pnlChanges.HotSpotClosed;
  RzLabel13.Visible := not pnlChanges.HotSpotClosed;
  RzLabel19.Visible := not pnlChanges.HotSpotClosed;
end;

procedure TfrmMain.pgcChangesChange(Sender: TObject);
begin
	if pgcChanges.ActivePage = tabLink then
  begin
    grdChanges.Show;
    grdNodeChanges.Hide;
  end
  else if pgcChanges.ActivePage = tabUSNode then
  begin
  	if grdNodeChanges.Visible then
    	grdNodeChanges.Hide;
    grdNodeChanges.Show;
    grdChanges.Hide;
  	grdNodeChanges.DataSource := srcUSNodeDetailChanges;
  end
  else if pgcChanges.ActivePage = tabDSNode then
  begin
  	if grdNodeChanges.Visible then
    	grdNodeChanges.Hide;
    grdNodeChanges.Show;
    grdChanges.Hide;
  	grdNodeChanges.DataSource := srcDSNodeDetailChanges;
  end;
end;

procedure TfrmMain.btnDataSourceClick(Sender: TObject);
begin
	dxBarDataSource.PopupFromCursorPos;
end;

procedure TfrmMain.mnuDSDimensionChange(Sender: TObject);
var
	DeformatDS: String;
  AConduit: TConduit;
begin
	DeformatDS := DeformatDataSourceString(btnDataSource.Caption);
  DeformatDS := mnuDSDimension.KeyValue +	RightStr(DeformatDS, 7);
  btnDataSource.Caption := FormatDataSourceString(DeformatDS);
  AConduit := TConduit(SelectedList[0]);
  if Assigned(AConduit) then
  begin
  	AConduit.DataSource := DeformatDS;
    Modified := True;
  end;
end;

procedure TfrmMain.mnuDSShapeChange(Sender: TObject);
var
	DeformatDS: String;
  AConduit: TConduit;
begin
	DeformatDS := DeformatDataSourceString(btnDataSource.Caption);
	DeformatDS := LeftStr(DeformatDS,1) +	mnuDSShape.KeyValue + RightStr(DeformatDS, 6);
  btnDataSource.Caption := FormatDataSourceString(DeformatDS);
  AConduit := TConduit(SelectedList[0]);
  if Assigned(AConduit) then
  begin
  	AConduit.DataSource := DeformatDS;
    Modified := True;
  end;
end;

procedure TfrmMain.mnuDSMaterialChange(Sender: TObject);
var
	DeformatDS: String;
  AConduit: TConduit;
begin
	DeformatDS := DeformatDataSourceString(btnDataSource.Caption);
	DeformatDS := LeftStr(DeformatDS,2) + mnuDSMaterial.KeyValue + RightStr(DeformatDS, 5);
  btnDataSource.Caption := FormatDataSourceString(DeformatDS);
  AConduit := TConduit(SelectedList[0]);
  if Assigned(AConduit) then
  begin
  	AConduit.DataSource := DeformatDS;
    Modified := True;
  end;
end;

procedure TfrmMain.mnuDSIEUpChange(Sender: TObject);
var
	DeformatDS: String;
  AConduit: TConduit;
begin
	DeformatDS := DeformatDataSourceString(btnDataSource.Caption);
	DeformatDS := LeftStr(DeformatDS,3) + mnuDSIEUp.KeyValue + RightStr(DeformatDS, 4);
  btnDataSource.Caption := FormatDataSourceString(DeformatDS);
  AConduit := TConduit(SelectedList[0]);
  if Assigned(AConduit) then
  begin
  	AConduit.DataSource := DeformatDS;
    Modified := True;
  end;
end;

procedure TfrmMain.mnuDSIEDownChange(Sender: TObject);
var
	DeformatDS: String;
  AConduit: TConduit;
begin
	DeformatDS := DeformatDataSourceString(btnDataSource.Caption);
	DeformatDS := LeftStr(DeformatDS,4) + mnuDSIEDown.KeyValue + RightStr(DeformatDS, 3);
  btnDataSource.Caption := FormatDataSourceString(DeformatDS);
  AConduit := TConduit(SelectedList[0]);
  if Assigned(AConduit) then
  begin
  	AConduit.DataSource := DeformatDS;
    Modified := True;
  end;
end;

procedure TfrmMain.mnuDSRimChange(Sender: TObject);
var
	DeformatDS: String;
  AConduit: TConduit;
begin
	DeformatDS := DeformatDataSourceString(btnDataSource.Caption);
	DeformatDS := LeftStr(DeformatDS,5) + mnuDSRim.KeyValue + RightStr(DeformatDS, 2);
  btnDataSource.Caption := FormatDataSourceString(DeformatDS);
  AConduit := TConduit(SelectedList[0]);
  if Assigned(AConduit) then
  begin
  	AConduit.DataSource := DeformatDS;
    Modified := True;
  end;
end;

procedure TfrmMain.mnuDSLengthChange(Sender: TObject);
var
	DeformatDS: String;
  AConduit: TConduit;
begin
	DeformatDS := DeformatDataSourceString(btnDataSource.Caption);
	DeformatDS := LeftStr(DeformatDS,6) + mnuDSLength.KeyValue + RightStr(DeformatDS, 1);
  btnDataSource.Caption := FormatDataSourceString(DeformatDS);
  AConduit := TConduit(SelectedList[0]);
  if Assigned(AConduit) then
  begin
  	AConduit.DataSource := DeformatDS;
    Modified := True;
  end;
end;

procedure TfrmMain.mnuDSSynthChange(Sender: TObject);
var
	DeformatDS: String;
  AConduit: TConduit;
begin
	DeformatDS := DeformatDataSourceString(btnDataSource.Caption);
	DeformatDS := LeftStr(DeformatDS,7) + mnuDSSynth.KeyValue;
  btnDataSource.Caption := FormatDataSourceString(DeformatDS);
  AConduit := TConduit(SelectedList[0]);
  if Assigned(AConduit) then
  begin
  	AConduit.DataSource := DeformatDS;
    Modified := True;
  end;
end;

function TfrmMain.FormatDataSourceString(AString: String): String;
begin
  Result := LeftStr(AString,4)+'.'+RightStr(AString,4);
end;

function TfrmMain.DeformatDataSourceString(AString: String): String;
begin
	Result := LeftStr(AString,4)+RightStr(AString,4);
end;

procedure TfrmMain.dxBarDataSourcePopup(Sender: TObject);
var
	DeformatDS: String;
  AConduit: TConduit;
begin
	AConduit := TConduit(SelectedList[0]);
  if Assigned(AConduit) then
  begin
    if AConduit.DataSource = '' then
      AConduit.DataSource := '????????';
    btnDataSource.Caption := FormatDataSourceString(AConduit.DataSource);
    DeformatDS := AConduit.DataSource;
    mnuDSDimension.KeyValue := DeformatDS[1];
    mnuDSShape.KeyValue := DeformatDS[2];
    mnuDSMaterial.KeyValue := DeformatDS[3];
    mnuDSIEUp.KeyValue := DeformatDS[4];
    mnuDSIEDown.KeyValue := DeformatDS[5];
    mnuDSRim.KeyValue := DeformatDS[6];
    mnuDSLength.KeyValue := DeformatDS[7];
    mnuDSSynth.KeyValue := DeformatDS[8];
  end;
end;

procedure TfrmMain.actFileUpdateMasterDatabaseExecute(Sender: TObject);
var
  MasterDir: String;
  MasterChangesFileName: String;
  MasterChangesConnectionString: String;
	ACatalog: Catalog;
  TracedTheme: CMapXTheme;
  IndCatStyle: CMapXStyle;
  i: Integer;
begin
  Screen.Cursor := crHourglass;

  if grdChanges.State = tsEditing then
  	grdChanges.CloseEditor;
  if grdNodeChanges.State = tsEditing then
  	grdNodeChanges.CloseEditor;

  // Ensure to deactivate Master Database Connections
  adoMasterLinks.Close;
  adoMasterNodes.Close;
  adoMasterLinksConnection.Close;
  adoMasterNodesConnection.Close;

  // Create Master Database Changes Table if Necessary
  mnuStatusStatus.Caption := 'Opening Master Changes Logs';
	Refresh;
	MasterDir := ModelRegistry.ReadString('masterfiles','root','C:\');

	// Trap for master links not found
	if not FileExists(MasterDir+LinksDirName+MasterLinksDBName) then
	begin
		ShowMessage('Model does not currently point to a valid Master Table '+
			MasterDir+LinksDirName+MasterLinksDBName+'. No changes updated.');
		Exit;
	end;

	MasterChangesFileName := MasterDir + MasterChangesDirName + ChangesDBName;

	MasterChangesConnectionString := 'Provider=Microsoft.Jet.OLEDB.4.0;Password="";User ID=Admin;'+
		'Data Source='+MasterChangesFileName+';'+
		'Mode=Share Deny None;Extended Properties="";Jet OLEDB:System database="";Jet OLEDB:Registry Path="";'+
		'Jet OLEDB:Database Password="";Jet OLEDB:Engine Type=4;Jet OLEDB:Database Locking Mode=0;Jet OLEDB:'+
		'Global Partial Bulk Ops=2;Jet OLEDB:Global Bulk Transactions=1;Jet OLEDB:New Database Password="";'
		+'Jet OLEDB:Create System Database=False;Jet OLEDB:Encrypt Database=False;'+
		'Jet OLEDB:Don''t Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;'+
		'Jet OLEDB:SFP=False';
	if not FileExists(MasterChangesFileName) then
  begin
		ACatalog := CoCatalog.Create;
		ACatalog.Create(MasterChangesConnectionString);
		adoMasterChangesConnection.ConnectionString := MasterChangesConnectionString;
		adoMasterChangesConnection.Open;
		adoMasterChangesCommand.CommandText := 'CREATE TABLE LinkChanges (' +
			'MLinkID INTEGER,' +
			'FieldName TEXT(50),' +
			'Changed LOGICAL,' +
			'OldValue TEXT(50),' +
			'NewValue TEXT(50),' +
			'Comment TEXT(100),' +
			'UserName TEXT(50),' +
			'UserTime DATETIME);';
		adoMasterChangesCommand.Execute;

		adoMasterChangesCommand.CommandText := 'CREATE TABLE NodeChanges (' +
			'Node TEXT(6),' +
			'FieldName TEXT(50),' +
			'Changed LOGICAL,' +
			'OldValue TEXT(50),' +
			'NewValue TEXT(50),' +
			'Comment TEXT(100),' +
			'UserName TEXT(50),' +
			'UserTime DATETIME);';
		adoMasterChangesCommand.Execute;

    // Set AllowZeroLength property to true
    ACatalog.Set_ActiveConnection(adoMasterChangesConnection.ConnectionString);
    ACatalog.Tables.Refresh;
    ACatalog.Tables['LinkChanges'].Columns['OldValue'].Properties['Jet OLEDB:Allow Zero Length'].Value := True;
    ACatalog.Tables['LinkChanges'].Columns['NewValue'].Properties['Jet OLEDB:Allow Zero Length'].Value := True;
    ACatalog.Tables['LinkChanges'].Columns['Comment'].Properties['Jet OLEDB:Allow Zero Length'].Value := True;

    ACatalog.Tables['NodeChanges'].Columns['OldValue'].Properties['Jet OLEDB:Allow Zero Length'].Value := True;
    ACatalog.Tables['NodeChanges'].Columns['NewValue'].Properties['Jet OLEDB:Allow Zero Length'].Value := True;
    ACatalog.Tables['NodeChanges'].Columns['Comment'].Properties['Jet OLEDB:Allow Zero Length'].Value := True;
	end
	// Otherwise, just connect to it
	else
	begin
		adoMasterChangesConnection.ConnectionString := MasterChangesConnectionString;
		adoMasterChangesConnection.Open;
	end;
  adoMasterLinkChanges.TableName := 'LinkChanges';
  adoMasterLinkChanges.Open;
  adoMasterNodeChanges.TableName := 'NodeChanges';
  adoMasterNodeChanges.Open;

  // Activate Master Database Connections
  try
    adoMasterLinksConnection.ConnectionString := 'Provider=Microsoft.Jet.OLEDB.4.0;'+
			'Data Source='+MasterDir+LinksDirName+MasterLinksDBName+';Persist Security Info=False';
		adoMasterLinksConnection.Open;
    adoMasterLinks.TableName := MasterLinksName;
    adoMasterLinks.Open;
  except
  	on E: EOleException do
    	MessageDlg(Format('Error %d'#13'%s', [E.ErrorCode, E.Message]), mtError, [mbOK], 0);
  end;
  try
    adoMasterNodesConnection.ConnectionString := 'Provider=Microsoft.Jet.OLEDB.4.0;'+
      'Data Source='+MasterDir+NodesDirName+MasterNodesDBName+';Persist Security Info=False';
    adoMasterNodesConnection.Open;
    adoMasterNodes.TableName := MasterNodesName;
    adoMasterNodes.Open;
  except
  	on E: EOleException do
    	MessageDlg(Format('Error %d'#13'%s', [E.ErrorCode, E.Message]), mtError, [mbOK], 0);
  end;

  try
  	mnuStatusStatus.Caption := 'Updating links change log';
    Refresh;
    if adoLinkChanges.RecordCount > 0 then
    begin
      adoLinkChanges.FindFirst;
      while not adoLinkChanges.Eof do
      begin
        if adoLinkChangesChanged.Value then
        begin
          //Apply change
          adoNetwork.Locate('LinkID', adoLinkChangesLinkID.Value, [loCaseInsensitive]);
          adoMasterLinks.Locate('MLinkID', adoNetworkMLinkID.Value, [loCaseInsensitive]);

          adoMasterLinks.Edit;
          adoMasterLinks.FieldByName(adoLinkChangesFieldName.Value).Value := adoLinkChangesNewValue.Value;
          adoMasterLinks.Post;

          adoLinkChanges.Edit;
          adoLinkChangesChanged.Value := False;
          adoLinkChanges.Post;

          adoMasterLinkChanges.AppendRecord(
            [adoNetworkMLinkID.Value,
            adoLinkChangesFieldName.Value,
            adoLinkChangesChanged.Value,
            adoLinkChangesOldValue.Value,
            adoLinkChangesNewValue.Value,
            adoLinkChangesComment.Value,
            adoLinkChangesUserName.Value,
            adoLinkChangesUserTime.Value]);
        end;
        adoLinkChanges.Next;
      end;
    end;

  	mnuStatusStatus.Caption := 'Updating nodes change log';
    Refresh;
    if adoNodeChanges.RecordCount > 0 then
    begin
      adoNodeChanges.FindFirst;
      while not adoNodeChanges.Eof do
      begin
        if adoNodeChangesChanged.Value then
        begin
          //Apply change
          adoNodes.Locate('Node', adoNodeChangesNode.Value, [loCaseInsensitive]);
          adoMasterNodes.Locate('Node', adoNodesNode.Value, [loCaseInsensitive]);

          adoMasterNodes.Edit;
          adoMasterNodes.FieldByName(adoNodeChangesFieldName.Value).Value := adoNodeChangesNewValue.Value;
          adoMasterNodes.Post;

          adoNodeChanges.Edit;
          adoNodeChangesChanged.Value := False;
          adoNodeChanges.Post;

          adoMasterNodeChanges.AppendRecord(
            [adoNodeChangesNode.Value,
            adoNodeChangesFieldName.Value,
            adoNodeChangesChanged.Value,
            adoNodeChangesOldValue.Value,
            adoNodeChangesNewValue.Value,
            adoNodeChangesComment.Value,
            adoNodeChangesUserName.Value,
            adoNodeChangesUserTime.Value]);
        end;
        adoNodeChanges.Next;
      end;
    end;
  finally
    Screen.Cursor := crDefault;
	  mnuStatusStatus.Caption := ''
  end;

  adoLinkDetailChanges.Close;
  adoLinkDetailChanges.Open;
  adoUSNodeDetailChanges.Close;
  adoUSNodeDetailChanges.Open;
  adoDSNodeDetailChanges.Close;
  adoDSNodeDetailChanges.Open;
  grdChanges.Refresh;

  //Deactivate Master Database Connections
  adoMasterLinks.Close;
  adoMasterNodes.Close;
  adoMasterLinkChanges.Close;
  adoMasterNodeChanges.Close;
  adoMasterLinksConnection.Close;
  adoMasterNodesConnection.Close;
  adoMasterChangesConnection.Close;

  // Delete logs
  if adoTraced.RecordCount > 0 then
    for i := 1 to adoTraced.RecordCount do
      adoTraced.Delete;
  if adoLinkChanges.RecordCount > 0 then
    for i := 1 to adoLinkChanges.RecordCount do
      adoLinkChanges.Delete;
  if adoNodeChanges.RecordCount > 0 then
    for i := 1 to adoNodeChanges.RecordCount do
      adoNodeChanges.Delete;

  adoDSNodeDetailChanges.Close;
  adoDSNodeDetailChanges.Open;
  adoUSNodeDetailChanges.Close;
  adoUSNodeDetailChanges.Open;
  adoLinkDetailChanges.Close;
  adoLinkDetailChanges.Open;

  mapNetwork.Datasets.Item('Traced').Themes.RemoveAll;
  TraceItemsAvailable := False;
{	mapNetwork.Datasets.Item('Traced').Themes.RemoveAll;
	mapNetwork.Datasets.Item('Traced').Themes.Add(miThemeIndividualValue, 'Traced',
		'By Traced', True);
	TracedTheme := mapNetwork.Datasets.Item('Traced').Themes.Item('By Traced');
	IndCatStyle := TracedTheme.ThemeProperties.IndividualValueCategories.Item(1).Style;
	IndCatStyle.LineWidth := 2;
	IndCatStyle.LineStyle := 89;}
end;

procedure TfrmMain.FormCloseQuery(Sender: TObject; var CanClose: Boolean);
var
	DlgResult: Word;
begin
	// If profile was modified, prompt to save first
	if Modified then
	begin
		DlgResult := MessageDlg('Save modifications to profile?', mtConfirmation, mbYesNoCancel, 0);
		case DlgResult of
			mrYes:
      	begin
        	actNetworkSaveProfileExecute(Sender);
          CanClose := True;
        end;
      mrNo:
      	CanClose := True;
			mrCancel:
      	CanClose := False;
		end;
	end;
end;

procedure TfrmMain.actNetworkFindExecute(Sender: TObject);
var
  FindCompKey: Integer;
  Features: CMapXFeatures;
  FindBounds: CMapXRectangle;
  MapAspect: Double;
begin
  if dlgFindLink.ShowModal = mrOK then
  begin
    FindCompKey := StrToInt(dlgFindLink.edtFindLink.Text);
    Features := mapNetwork.Layers.Item(NetworkTableName).Search(
    	Format('Network.CompKey = %d', [FindCompKey]), EmptyParam);
    if Features.Count > 0 then
    begin
    	mapNetwork.AutoRedraw := False;
      FindBounds := Features.Bounds;
      MapAspect := mapNetwork.Height/mapNetwork.Width;
      if MapAspect < 1 then
        mapNetwork.ZoomTo((FindBounds.YMax-FindBounds.YMin)/MapAspect, (FindBounds.XMax+FindBounds.XMin)/2,
          (FindBounds.YMax+FindBounds.YMin)/2)
      else
        mapNetwork.ZoomTo(FindBounds.XMax-FindBounds.XMin, (FindBounds.XMax+FindBounds.XMin)/2,
          (FindBounds.YMax+FindBounds.YMin)/2);
    	mapNetwork.AutoRedraw := True;
      mapNetwork.Layers.Item(NetworkTableName).Selection.ClearSelection;
      mapNetwork.Layers.Item(NetworkTableName).Selection.Add(Features.Item(1));
    end;
  end;
end;

procedure TfrmMain.actNetworkFindUpdate(Sender: TObject);
begin
	actNetworkFind.Enabled := mapNetwork.Layers.Count > 0;
end;

procedure TfrmMain.actNetworkCopyGroundSlopeExecute(Sender: TObject);
var
	i: Integer;
  GroundSlope: Double;
begin
	// Rifle through the selection and copy the ground slope to the IEs
	for i := 0 to SelectedList.Count-1 do
  begin
  	with TConduit(Selected[i]) do
    begin
    	GroundSlope := (UpGround-DnGround)/Length;
      if btnAnchorUpstream.Down then
      begin
      	DnInvert := StrToFloat(Format('%.2f', [UpInvert - GroundSlope*Length]));
        SetDataSourceItem(5, 'G');
      end
      else
      begin
      	UpInvert := StrToFloat(Format('%.2f', [DnInvert + GroundSlope*Length]));
        SetDataSourceItem(4, 'G');
      end;
    end;
	end;
  DrawBox.Refresh;
  Modified := True;
  btnDataSource.Caption := FormatDataSourceString(TConduit(Selected[0]).DataSource);
  frmMain.adoNetwork.Locate('LinkID', TConduit(Selected[0]).LinkID, [loCaseInsensitive]);
end;

procedure TfrmMain.actNetworkMinimumPipeSlopeExecute(Sender: TObject);
var
	i: Integer;
	MinSlope: Double;
	AnchorConduit: TConduit;
begin
	// Rifle through the selection and apply minimum slope to the IEs
	for i := 0 to SelectedList.Count-1 do
	begin
		with TConduit(Selected[i]) do
		begin
			MinSlope := CircularMinSlopeRequired(3, VertDim, mnuAnchorRoughness.Value);
			if btnAnchorUpstream.Down then
			begin
				if Path.PosOfObj(TModelObj(Selected[i])) = 0 then
				begin
					MessageDlg('Upstream pipe must be visible in profile to adjust to minimum slope;'#13+
						'Use Shift Left button or Ctrl+Left Arrow to shift profile upstream',
						mtWarning, [mbOK], 0);
					Exit;
				end;
				AnchorConduit := TConduit(Path.Objects[Path.PosOfObj(TModelObj(Selected[i]))-1]);
				UpInvert := AnchorConduit.DnInvert;
				DnInvert := StrToFloat(Format('%.2f', [AnchorConduit.DnInvert - MinSlope*Length]));
				SetDataSourceItem(5, 'S');
			end
			else
			begin
				if Path.PosOfObj(TModelObj(Selected[i])) = Path.Objects.Count-1 then
				begin
					MessageDlg('Downstream pipe must be visible in profile to adjust to minimum slope;'#13+
						'Use Shift Right button or Ctrl+Right Arrow to shift profile downstream',
						mtWarning, [mbOK], 0);
					Exit;
				end;
				AnchorConduit := TConduit(Path.Objects[Path.PosOfObj(TModelObj(Selected[i]))+1]);
        DnInvert := AnchorConduit.UpInvert;
				UpInvert := StrToFloat(Format('%.2f', [AnchorConduit.UpInvert + MinSlope*Length]));
				SetDataSourceItem(4, 'S');
			end;
		end;
	end;
	DrawBox.Refresh;
	Modified := True;
	btnDataSource.Caption := FormatDataSourceString(TConduit(Selected[0]).DataSource);
	frmMain.adoNetwork.Locate('LinkID', TConduit(Selected[0]).LinkID, [loCaseInsensitive]);
end;

procedure TfrmMain.actNetworkCopyAdjacentSlopeExecute(Sender: TObject);
var
	i: Integer;
	AnchorConduit: TConduit;
begin
	for i := 0 to SelectedList.Count-1 do
	begin
		if btnAnchorUpstream.Down then
		begin
			if Path.PosOfObj(TModelObj(Selected[i])) = 0 then
			begin
				MessageDlg('Upstream pipe must be visible in profile to copy its slope;'#13+
					'Use Shift Left button or Ctrl+Left Arrow to shift profile upstream',
					mtWarning, [mbOK], 0);
				Exit;
			end;
			AnchorConduit := TConduit(Path.Objects[Path.PosOfObj(TModelObj(Selected[i]))-1]);
			with TConduit(Selected[i]) do
			begin
				DnInvert := StrToFloat(Format('%.2f', [UpInvert - AnchorConduit.Slope*Length]));
				SetDataSourceItem(5, 'X');
			end;
		end
		else
		begin
			if Path.PosOfObj(TModelObj(Selected[i])) = Path.Objects.Count-1 then
			begin
				MessageDlg('Downstream pipe must be visible in profile to copy its slope;'#13+
					'Use Shift Right button or Ctrl+Right Arrow to shift profile downstream',
					mtWarning, [mbOK], 0);
				Exit;
			end;
			AnchorConduit := TConduit(Path.Objects[Path.PosOfObj(TModelObj(Selected[i]))+1]);
			with TConduit(Selected[i]) do
			begin
				UpInvert := StrToFloat(Format('%.2f', [DnInvert + AnchorConduit.Slope*Length]));
				SetDataSourceItem(4, 'X');
			end;
		end;
	end;

	DrawBox.Refresh;
	Modified := True;
	btnDataSource.Caption := FormatDataSourceString(TConduit(Selected[0]).DataSource);
	frmMain.adoNetwork.Locate('LinkID', TConduit(Selected[0]).LinkID, [loCaseInsensitive]);
end;

procedure TfrmMain.actNetworkCopyGroundSlopeUpdate(Sender: TObject);
begin
	actNetworkCopyGroundSlope.Enabled := SelectedList.Count > 0;
end;

procedure TfrmMain.actNetworkMinimumPipeSlopeUpdate(Sender: TObject);
begin
	actNetworkMinimumPipeSlope.Enabled := SelectedList.Count > 0;
end;

procedure TfrmMain.actNetworkCopyAdjacentSlopeUpdate(Sender: TObject);
begin
	actNetworkCopyAdjacentSlope.Enabled := SelectedList.Count > 0;
end;

procedure TfrmMain.actFileUpdateMasterDatabaseUpdate(Sender: TObject);
begin
	actFileUpdateMasterDatabase.Enabled := (mapNetwork.Layers.Count > 0) or OpenedDB;
end;

procedure TfrmMain.grdChangesExit(Sender: TObject);
begin
	CodeSite.SendMsg('grdChangesExit');
  if grdChanges.State = tsEditing then
  	grdChanges.CloseEditor;
  if grdNodeChanges.State = tsEditing then
		grdNodeChanges.CloseEditor;
end;

procedure TfrmMain.actViewChangeLogExecute(Sender: TObject);
begin
	actViewChangeLog.Checked := not actViewChangeLog.Checked;
	if actViewChangeLog.Checked then
  	pnlChanges.Show
  else
  	pnlChanges.Hide;
end;

procedure TfrmMain.btnCloseChangeLogClick(Sender: TObject);
begin
	actViewChangeLogExecute(Sender);
end;

procedure TfrmMain.mnuDSDimensionCloseUp(Sender: TObject);
begin
	with Sender as TdxBarLookupCombo do
  begin
  	if CurText = '' then
    	KeyValue := '?'
    else
	  	KeyValue := CurText;
  end;
end;

procedure TfrmMain.mnuDSDimensionEnter(Sender: TObject);
begin
	with Sender as TdxBarLookupCombo do
  begin
		DroppedDown := True;
    CurText := KeyValue;
  end;
end;

procedure TfrmMain.adoLinkChangesBeforeInsert(DataSet: TDataSet);
begin
	CodeSite.SendMsg('Inserting record to adoLinkChanges');
end;

procedure TfrmMain.DrawBoxResize(Sender: TObject);
begin
	if actViewAutoExtents.Checked then
		RecalcView;
end;

procedure TfrmMain.actViewNetworkThematicExecute(Sender: TObject);
begin
	mapNetwork.Datasets.Item('Network').Themes.Item(1).ThemeDlg(0,0);
end;

procedure TfrmMain.actViewResetToolbarsExecute(Sender: TObject);
var
  i: Integer;
begin
  for i := 0 to dxBarManager1.Bars.Count-1 do
    dxBarManager1.Bars[i].Reset;
end;

procedure TfrmMain.actViewShowToolbarCaptionsExecute(Sender: TObject);
var
	i: Integer;
begin
  actViewShowToolbarCaptions.Checked := not ActViewShowToolbarCaptions.Checked;
	for i := 0 to dxBarManager1.ItemCount-1 do
	begin
		if dxBarManager1.Items[i] is TdxBarLargeButton then
      TdxBarLargeButton(dxBarManager1.Items[i]).ShowCaption := actViewShowToolbarCaptions.Checked;
	end;
end;

procedure TfrmMain.edtAsBuiltButtonClick(Sender: TObject;
	AbsoluteIndex: Integer);
var
	FileToOpen: String;
	FileExt: String;

	function StripNonNum(AString: String): String;
	var
		NewStr: String;
		i: Integer;
	begin
		for i := 1 to Length(AString) do
			if IsChNumericL(AString[i], '0123456789') then
				NewStr := NewStr + AString[i];
		Result := NewStr;
	end;

begin
{	frmAsBuilt.Show;
	if Length(edtAsBuilt.Text) = 0 then
	begin
		ShowMessage('No as-built information recorded');
		Exit;
	end;

	frmAsBuilt.adoJobDetail.Filtered := False;
	frmAsBuilt.adoJobDetail.Filter := '[JOB NUMBER] = '+QuotedStr(Format('%5.5d',
		[StrToInt(StripNonNum(edtAsBuilt.Text))]));
	CodeSite.SendMsg(frmAsBuilt.adoJobDetail.Filter);
	frmAsBuilt.adoJobDetail.Filtered := True;
	if frmAsBuilt.adoJobDetail.RecordCount > 0 then
	begin
		FileToOpen := TrimCharsL(frmAsBuilt.dxDBGrid1.FocusedNode.Strings[
			frmAsBuilt.dxDBGrid1.ColumnByFieldName('VIEW').Index], '#');
		FileExt := UpperCase(ExtractFileExt(FileToOpen));
		if (FileExt = '.DWG') or (FileExt = '.DWF') then
		begin
			frmAsBuilt.tabACAD.Show;
			//frmAsBuilt.avvView.src := FileToOpen;
      //frmAsBuilt.avvView.NamedView := 'As Last Saved';
		end
		else
		begin
			frmAsBuilt.tabRaster.Show;
			frmAsBuilt.ieInputOutput.LoadFromFile(FileToOpen);
    end;
	end
	else
		ShowMessage('No as-built information recorded');
}
end;

procedure TfrmMain.edtFlowTypeEnter(Sender: TObject);
begin
	(Sender as TWincontrol).SetFocus;
end;

end.
