unit dmMapinfo;

{$WARN SYMBOL_PLATFORM OFF}

interface

uses
  ComObj, ActiveX, StdVcl, SysUtils, Forms, globalconstants;
  {  EMGAATS_TLB,}

{type
  TCobjMICB = class(TAutoObject, ICobjMICB)
  protected
  private
    mystatustext : widestring;

  public
    procedure setstatustext(const txt: WideString); safecall;
    function  getStatusText : string;
  end;
}
  procedure showMIglobals(var oleMI : variant; MBappname : string);
  procedure delay(msecs : Longint);
  function mapinfodo(var oleMI : Variant ; dostr: string) :integer ;
implementation

uses ComServ, dialogs;

procedure delay(msecs : Longint);
var
  DelayTime: TDateTime;
begin
  DelayTime := Now + msecs/86400000;
  while (Now < DelayTime) do Application.ProcessMessages;
end;

{
function TCobjMICB.getStatusText: string;
begin
  result := mystatustext;
end;

procedure TCobjMICB.setstatustext(const txt: WideString);
begin
  showmessage(txt);
  mystatustext := txt;

end;
}

function mapinfodo(var oleMI : Variant; dostr: string) :integer ;
begin
  result := ReturnSuccess;
  try

  oleMI.do(dostr);
  except
  on E: Exception do
    begin
      messageDlg('Mapinfo Error' + #13 + E.message + #13 + 'cmd:' + dostr, mtError, [MBOK],0);
      result := ReturnFailure;
    end;
  end; //try

end;

procedure showMIglobals(var oleMI : variant; MBappname : string);
var
  junk : string;
  i : integer;
begin

  junk := '';
  for i := 1 to oleMI.mbapplications.item(MBappname).mbglobals.count do
    junk := junk + string(oleMI.mbapplications.item(MBappname).mbglobals.item(i).name) + '->'
    + string(oleMI.mbapplications.item(MBappname).mbglobals.item(i).value) + #13;
  showmessage(junk);
end;


initialization
  {TAutoObjectFactory.Create(ComServer, TCobjMICB, Class_CobjMICB,
    ciSingleInstance, tmApartment);}

end.
