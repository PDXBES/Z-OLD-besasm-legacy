unit utilMSaccess;



interface

uses
   // sysutils, comobj, variants, dialogs, ADODB;
    Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, fLabeledChild, StdCtrls, ActnList,comobj, dmmapinfo, globalconstants,
   DB, ADODB;

    function ACtblrelink(myoleac : variant;
        linktable, srctable, srcdatabase : string ; verboseOleError : boolean) : integer;


    function ACrunExportQueryTable(myoleac : variant ; outfilename,stblname,
      sfieldname,sfieldvalue : string; iomode : integer ; emsg : string ) : integer;

    function ACrunExecuteQueryTable(myoleac : variant ; stblname,
    sfieldname,sfieldvalue: string;iomode: integer; emsg : string) : integer;





implementation



//-1 = success
// > -1 = error

function ACtblrelink(myoleac : variant;
    linktable, srctable, srcdatabase: string ; verboseOleError : boolean) : integer;
var
  mytbl : variant;
begin
 //de-link
  try
	  myoleac.currentdb.tabledefs.delete(linktable);
  except
    on E: eoleexception do
    begin
	    if uppercase(E.message) <> 'ITEM NOT FOUND IN THIS COLLECTION' then
      begin
			  if verboseoleerror then
          showmessage(E.Message);

        result:=0;
        exit;
      end;
    end;
  else
    begin
 		  raise Exception.create('ACtblrelink Error');
    end;
  end;

  //re-link
  try
    mytbl := myoleac.currentdb.createtabledef(linktable);
    mytbl.connect := ';DATABASE='+ srcdatabase;
    mytbl.sourceTablename := srctable;
    myoleac.currentdb.tabledefs.append(mytbl);
    mytbl:= unassigned;

  except
    on E: eoleexception do
    begin
		  if verboseoleerror then
        showmessage(E.Message);
      result :=0;
      exit;
    end;
  else
    begin
 		  raise Exception.create('ACtblrelink error');
      exit;
    end;
  end;

  result :=-1;
end;

//Function ExportQueryTable(outfilename As String, stblname As String, sfieldname As String, sfieldvalue As String, iomode As Integer) As Integer

function ACrunExportQueryTable(myoleac : variant ; outfilename,stblname,sfieldname,sfieldvalue : string; iomode : integer ; emsg : string ) : integer;
var
  x : integer;
begin
    x := -1;
  try
    x := myoleac.run('ExportQueryTable',outfilename,stblname,sfieldname,sfieldvalue,iomode);

  except
    on E: eoleexception do
    begin
      showmessage(emsg + #13 + E.Message + #13 + 'return code: ' + inttostr(x));
      result := x;
      exit
    end;
  else
      raise Exception.create('ACrunExportQueryTable error');
  end;

   result := x;
end;

function ACrunExecuteQueryTable(myoleac : variant ; stblname,sfieldname,sfieldvalue: string;iomode: integer; emsg : string ) : integer;
var
  x : integer;
begin
    x := -1;
  try
    x := myoleac.run('ExecuteQueryTable',stblname,sfieldname,sfieldvalue, iomode);

  except
    on E: eoleexception do
    begin
      showmessage(emsg + #13 + E.Message + #13 + 'return code: ' + inttostr(x));
      result := x;
      exit
    end;
  else
      raise Exception.create('ACrunExecuteQueryTable error');
  end;

   result := x;
end;



end.


