unit dChooseConduit;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, RzLabel, dxCntner, dxEditor, dxExEdtr, dxEdLib,
  ExtCtrls, RzDlgBtn, RzButton;

type
  TdlgChooseConduit = class(TForm)
    edtChooseConduit: TdxPickEdit;
    RzLabel1: TRzLabel;
    RzDialogButtons1: TRzDialogButtons;
    btnHighlightSelected: TRzButton;
    btnHighlightAll: TRzButton;
    btnZoomIn: TRzBitBtn;
    btnZoomOut: TRzBitBtn;
    procedure FormActivate(Sender: TObject);
    procedure edtChooseConduitChange(Sender: TObject);
    procedure btnHighlightSelectedClick(Sender: TObject);
    procedure btnHighlightAllClick(Sender: TObject);
    procedure btnZoomInClick(Sender: TObject);
    procedure btnZoomOutClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  dlgChooseConduit: TdlgChooseConduit;

implementation

uses fMain, MapXLib_TLB, StStrL;

{$R *.dfm}

procedure TdlgChooseConduit.FormActivate(Sender: TObject);
begin
//	edtChooseConduit.SetFocus;
//	edtChooseConduit.DroppedDown := True;
end;

procedure TdlgChooseConduit.edtChooseConduitChange(Sender: TObject);
begin
//  Close;
end;

procedure TdlgChooseConduit.btnHighlightSelectedClick(Sender: TObject);
var
  FindLinkID: Integer;
  Features: CMapXFeatures;
  FindBounds: CMapXRectangle;
  MapAspect: Double;
  Tokens: TStringList;
begin
  Tokens := TStringList.Create;
  ExtractTokensL(edtChooseConduit.Text, '/', '"', True, Tokens);
  FindLinkID := StrToInt(Tokens[0]);
  Features := frmMain.mapNetwork.Layers.Item(frmMain.NetworkTableName).Search(
    Format('Network.LinkID = %d', [FindLinkID]), EmptyParam);
  if Features.Count > 0 then
  begin
    frmMain.mapNetwork.AutoRedraw := False;
    FindBounds := Features.Bounds;
    MapAspect := frmMain.mapNetwork.Height/frmMain.mapNetwork.Width;
    if MapAspect < 1 then
      frmMain.mapNetwork.ZoomTo((FindBounds.YMax-FindBounds.YMin)/MapAspect, (FindBounds.XMax+FindBounds.XMin)/2,
        (FindBounds.YMax+FindBounds.YMin)/2)
    else
      frmMain.mapNetwork.ZoomTo(FindBounds.XMax-FindBounds.XMin, (FindBounds.XMax+FindBounds.XMin)/2,
        (FindBounds.YMax+FindBounds.YMin)/2);
    frmMain.mapNetwork.Zoom := frmMain.mapNetwork.Zoom * 4;
    frmMain.mapNetwork.AutoRedraw := True;
    frmMain.mapNetwork.Layers.Item(frmMain.NetworkTableName).Selection.ClearSelection;
    frmMain.mapNetwork.Layers.Item(frmMain.NetworkTableName).Selection.Add(Features.Item(1));
  end;
  Tokens.Free;
end;

procedure TdlgChooseConduit.btnHighlightAllClick(Sender: TObject);
var
  i: Integer;
  FindLinkID: Integer;
  Features: CMapXFeatures;
  FindBounds: CMapXRectangle;
  MapAspect: Double;
  Tokens: TStringList;
begin
  Tokens := TStringList.Create;
  for i := 0 to edtChooseConduit.Items.Count-1 do
  begin
    ExtractTokensL(edtChooseConduit.Items[i], '/', '"', True, Tokens);
    FindLinkID := StrToInt(Tokens[0]);
    Features := frmMain.mapNetwork.Layers.Item(frmMain.NetworkTableName).Search(
      Format('Network.LinkID = %d', [FindLinkID]), EmptyParam);
    frmMain.mapNetwork.Layers.Item(frmMain.NetworkTableName).Selection.Add(Features.Item(1));
  end;
  if Features.Count > 0 then
  begin
    frmMain.mapNetwork.AutoRedraw := False;
    FindBounds := frmMain.mapNetwork.Layers.Item(frmMain.NetworkTableName).Selection.Bounds;
    MapAspect := frmMain.mapNetwork.Height/frmMain.mapNetwork.Width;
    if MapAspect < 1 then
      frmMain.mapNetwork.ZoomTo((FindBounds.YMax-FindBounds.YMin)/MapAspect, (FindBounds.XMax+FindBounds.XMin)/2,
        (FindBounds.YMax+FindBounds.YMin)/2)
    else
      frmMain.mapNetwork.ZoomTo(FindBounds.XMax-FindBounds.XMin, (FindBounds.XMax+FindBounds.XMin)/2,
        (FindBounds.YMax+FindBounds.YMin)/2);
    frmMain.mapNetwork.Zoom := frmMain.mapNetwork.Zoom * 4;
    frmMain.mapNetwork.AutoRedraw := True;
  end;
  Tokens.Free;
end;

procedure TdlgChooseConduit.btnZoomInClick(Sender: TObject);
begin
  frmMain.mapNetwork.Zoom := frmMain.mapNetwork.Zoom / 2;
end;

procedure TdlgChooseConduit.btnZoomOutClick(Sender: TObject);
begin
  frmMain.mapNetwork.Zoom := frmMain.mapNetwork.Zoom * 2;
end;

end.
