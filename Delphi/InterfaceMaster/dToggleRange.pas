unit dToggleRange;

interface

uses
	Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
	RzButton, StdCtrls, RzLabel, Mask, RzEdit, RzSpnEdt;

type
  TdlgToggleRange = class(TForm)
    RzLabel1: TRzLabel;
    RzLabel2: TRzLabel;
    btnOK: TRzButton;
    btnCancel: TRzButton;
    lblNumNodes: TRzLabel;
    edtLower: TRzSpinEdit;
    edtUpper: TRzSpinEdit;
    procedure FormShow(Sender: TObject);
    procedure FormCloseQuery(Sender: TObject; var CanClose: Boolean);
	private
		{ Private declarations }
	public
		{ Public declarations }
		NumItems: Integer;
	end;

var
	dlgToggleRange: TdlgToggleRange;

implementation

uses fConvertInterface, fMain;

{$R *.DFM}

procedure TdlgToggleRange.FormShow(Sender: TObject);
begin
	lblNumNodes.Caption := 'Total # of nodes: ' + IntToStr(NumItems);
  edtLower.Max := NumItems;
  edtUpper.Max := NumItems;
end;

procedure TdlgToggleRange.FormCloseQuery(Sender: TObject;
  var CanClose: Boolean);
begin
	if edtLower.IntValue > edtUpper.IntValue then
  begin
		MessageDlg('Lower value cannot be higher than upper value.', mtError, [mbOK], 0);
    CanClose := False;
  end;
end;

end.
