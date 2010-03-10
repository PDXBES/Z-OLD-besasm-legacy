unit fStatus;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls,
  dmStateMachine, globalconstants;

type
  TfrmStatus = class(TForm)
    btnCancel: TButton;
    lbl1: TLabel;
    Lbl2: TLabel;
    procedure btnCancelClick(Sender: TObject);
  private
    myCancel : boolean;
    { Private declarations }
  public
    procedure StatusBox(Label1, Label2, myCaption: string);
    function MIRunMenuCmd(var oleMI: Variant; Label1, cmdID, myCaption,appname,appmsg,apprc : string) :integer ;
    { Public declarations }
  end;

var
  frmStatus: TfrmStatus;

implementation

uses
  fmain, dmmapinfo;

{$R *.dfm}
procedure TfrmStatus.StatusBox(Label1, Label2, myCaption: string);
begin

  lbl1.caption := Label1;
  lbl2.caption := Label2;
  caption := myCaption;

  btnCancel.Caption := '';
  btnCancel.Enabled := false;
  btnCancel.Visible := false;
  myCancel := False;

  update;
  if not visible then Show;
  application.ProcessMessages;

end;




function TfrmStatus.MIRunMenuCmd(var oleMI: Variant; Label1, cmdID, myCaption,appname,appmsg,apprc : string) :integer ;
var
  myrc : string;
  VisibleWhenInvoked : boolean;
begin

VisibleWhenInvoked := Visible;

try

  StatusBox(Label1, cmdID, myCaption);

  oleMI.mbapplications.item(appname).mbglobals.item(appmsg).value := 'NULL';
  oleMI.mbapplications.item(appname).mbglobals.item(apprc).value := 'NULL';


  //lblmessage.Caption := string(oleMI.mbapplications.item(appname).mbglobals.item(apprc).value);
  //labelmessage2.Caption := '';


  olemi.do ('Run Menu Command ID ' + cmdID);
  sleep(1000);

 // showMIglobals(olemi, appname);


  myrc  :=  uppercase(string(oleMI.mbapplications.item(appname).mbglobals.item(apprc).value));
  if myrc <> 'SUCCESS' Then
  begin
    if not visible then Show;
    {
    btnCancel.Enabled := True;
    btnCancel.Visible := True;
    lbl1.Caption :=   'Return code:' +  myrc + #13 +
                                      'Message:' + string(oleMI.mbapplications.item(appname).mbglobals.item(appmsg).value);
    lbl2.caption :='';
     }


    application.processmessages;

    if not VisibleWhenInvoked then Hide;

    messageDlg('Mapinfo Error' + #13 + 'appmsg=' +
      string(oleMI.mbapplications.item(appname).mbglobals.item(appmsg).value) +
      #13 + #13 + 'call 823-7735' , mtError, [MBOK],0);

    result := ReturnFailure;
  end
  else
  begin
    if not VisibleWhenInvoked then Hide;
    application.processmessages;
    result := ReturnSuccess;
  end;


except
  on E: Exception do
    begin
      if not VisibleWhenInvoked then Hide;

      messageDlg('Mapinfo Error'+ #13 +
      'Attempt will be made to kill mapinfow process' + #13
      + E.message + #13 +
      'cmd:' + cmdID + #13 +
      'app:' + appname + #13,
       mtError, [MBOK],0);
      olemi := unassigned;

      result := ReturnFailure;
    end;
end; //try


end;

procedure TfrmStatus.btnCancelClick(Sender: TObject);
begin
  myCancel := True;
  frmstatus.Hide;
end;

end.
