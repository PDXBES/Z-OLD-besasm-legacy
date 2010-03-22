unit fgetStormOption;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls,
  dmStateMachine, ExtCtrls;

type
  TfrmStormOption = class(TForm)
    btnCancel: TButton;
    btnOK: TButton;
    rdgStormOption: TRadioGroup;
    procedure btnOKClick(Sender: TObject);
    procedure btnCancelClick(Sender: TObject);
  private
    mymbrc : tmsgdlgbtn;
    { Private declarations }
  public
    property mbrc : tmsgdlgbtn read mymbrc;

    { Public declarations }
  end;

var
  frmStormOption: TfrmStormOption;
implementation

uses
  fmain;

{$R *.dfm}



procedure TfrmStormOption.btnOKClick(Sender: TObject);
begin
      mymbrc := MBOK;

      modalresult := MrOK;

end;

procedure TfrmStormOption.btnCancelClick(Sender: TObject);
begin
      mymbrc := MBCancel;

      modalresult := MrCancel;

end;



end.
