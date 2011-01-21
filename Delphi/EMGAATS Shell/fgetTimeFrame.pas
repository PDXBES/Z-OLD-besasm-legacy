{==fGetTimeFrame Unit===========================================================

	Allows user to select the time frame for the hydraulic (a date) and hydrologic
	(existing or future) components of a model.

	Revision History
	3.0    03/06/2003 Changed time frame spec from EX/FU for both hydraulic and
										hydrologic components to separate specs
  1.5    ?          ?
	1.0    11/01/2002 Initial release

===============================================================================}

unit fgetTimeFrame;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls,
  dmStateMachine, ExtCtrls, RzCmboBx, Mask, RzEdit, RzLabel, RzDlgBtn, RzPanel;

type
  TfrmTimeFrame = class(TForm)
    rdgTimeFrame: TRadioGroup;
    RzDialogButtons1: TRzDialogButtons;
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
  frmTimeFrame: TfrmTimeFrame;

implementation

uses
  fmain;

{$R *.dfm}



procedure TfrmTimeFrame.btnOKClick(Sender: TObject);
begin
      mymbrc := MBOK;
//      if rdgTimeFrame
      modalresult := MrOK;
    //  frmtimeframe.Hide;
end;

procedure TfrmTimeFrame.btnCancelClick(Sender: TObject);
begin
      mymbrc := MBCancel;

      modalresult := MrCancel;



    //  frmtimeframe.Hide;
end;

end.
