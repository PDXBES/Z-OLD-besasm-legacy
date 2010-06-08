unit dFindLink;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, dxCntner, dxEditor, dxExEdtr, dxDBEdtr, dxDBELib, StdCtrls,
  ExtCtrls, RzDlgBtn, DBCtrls, RzDBCmbo, dxTL, dxDBCtrl, dxDBGrid, dxEdLib,
  RzLabel, RzPanel;

type
  TdlgFindLink = class(TForm)
    RzDialogButtons1: TRzDialogButtons;
    Label1: TLabel;
    edtFindLink: TdxPopupEdit;
    pnlLookup: TRzPanel;
    RzLabel1: TRzLabel;
    dxDBGrid1: TdxDBGrid;
    dxDBGrid1LinkID: TdxDBGridMaskColumn;
    dxDBGrid1MLinkID: TdxDBGridMaskColumn;
    dxDBGrid1USNode: TdxDBGridColumn;
    dxDBGrid1CompKey: TdxDBGridMaskColumn;
    dxDBGrid1DSNode: TdxDBGridColumn;
    dlgbSearch: TRzDialogButtons;
    procedure dxPickEdit1DropDown(Sender: TObject);
    procedure edtFindLinkCloseUp(Sender: TObject; var Text: String;
      var Accept: Boolean);
    procedure dlgbSearchClickOk(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  dlgFindLink: TdlgFindLink;

implementation

uses fMain;

{$R *.dfm}

procedure TdlgFindLink.dxPickEdit1DropDown(Sender: TObject);
var
  i: Integer;
begin

end;

procedure TdlgFindLink.edtFindLinkCloseUp(Sender: TObject;
  var Text: String; var Accept: Boolean);
var
	CompKeyCol: Integer;
begin
	CompKeyCol := dxDBGrid1.ColumnByName('dxDBGrid1CompKey').Index;
  Text :=  dxDBGrid1.FocusedNode.Values[CompKeyCol];
  Accept := True;
end;

procedure TdlgFindLink.dlgbSearchClickOk(Sender: TObject);
begin
	edtFindLink.DroppedDown := False;
end;

end.
