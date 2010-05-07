unit unitmodelcontrols;

interface

uses
  Sysutils;

  function getmodelname(modelpath: PChar): string;


implementation


function getmodelname(modelpath: PChar): string;

var
  P: PChar;
begin
  P := StrRScan(modelpath, '\');
  if P = nil then
    result := ''
  else
  begin
    result := copy(string(P),2,100);
  end;

end;

end.
