unit uQCWorkspaceManager;

interface

uses SysUtils, Classes, uEMGAATSModel, uEMGAATSTypes;

type
  TQCWorkspaceManager = class
  private
    fMapInfoApp: OleVariant;
    fModel: TEMGAATSModel;
  public
    constructor Create;
    destructor Destroy; override;
    procedure SetModel(AModel: TEMGAATSModel);
    procedure Initialize(QCWorkspaceAppLocation: String);
    function Run: String;
  end;

var
  QCWorkspaceManager: TQCWorkspaceManager;

implementation

uses ComObj, Variants, uWorkbenchDefs, GlobalConstants;

{ TEMGWorkbenchManager }

constructor TQCWorkspaceManager.Create;
begin
	fMapInfoApp := CreateOleObject('MapInfo.Application');
end;

destructor TQCWorkspaceManager.Destroy;
begin
  fMapInfoApp := Unassigned;
  inherited;
end;

procedure TQCWorkspaceManager.Initialize(QCWorkspaceAppLocation: String);
begin
  fMapInfoApp.Do('Run Application '#34 + QCWorkspaceAppLocation + #34);
end;

function TQCWorkspaceManager.Run;
begin
  fMapInfoApp.Do('Run Menu Command ID ' + QCW_QCWorkspace);
  Sleep(1000);
  Result := UpperCase(
    string(fMapInfoApp.MBApplications.Item(QCW_AppName).mbglobals.item(strMBrc).value));
end;

procedure TQCWorkspaceManager.SetModel(AModel: TEMGAATSModel);
begin
  fModel := AModel;
	fMapInfoApp.MBApplications.Item(QCW_AppName).MBGlobals.item('gmdlRootFolder').Value := fModel.Path;
end;

end.
