unit uRunningObjects;

interface

uses ComObj, ActiveX, Registry, Windows, Dialogs, Classes, SysUtils;

type
	TRunningObjData = class
		Running: Boolean;
		RegID: String;
		ProgID: String;
	end;

var
	RunningObjectsList: TStringList;
	RunningObjCount: Integer;

	function GetApp(AID: String): IUnknown;

implementation

function extractGUIDstr(str : string):string;
var
	guid : TGUID;
	p,pe : integer;
begin
	p:=pos('{',str);
	pe:=pos('}',str);
	if (p=0) or (pe=0) then raise exception.create('No guid');
	str:=copy(str,p,pe-p+1);
	if CLSIDFromString(PWideChar(WideString(Str)), guid)<>S_OK then
		raise exception.create('No guid');
	result:=str;
end;

function hasGuid(str : string):boolean;
var
	guid : TGuid;
	p,pe : integer;
begin
	result:=false;
	p:=pos('{',str);
	pe:=pos('}',str);
	if (p=0) or (pe=0) then EXIT;
	str:=copy(str,p,pe-p+1);
	if CLSIDFromString(PWideChar(WideString(Str)), guid)=S_OK then
		result:=true;
end;

procedure ClearRunningObjectsList;
var
	i: Integer;
begin
	for i := 0 to RunningObjectsList.Count-1 do
	begin
		RunningObjectsList.Objects[i].Free;
	end;
	RunningObjectsList.Clear;
end;

function GetApp(AID: String): IUnknown;
var
	 moniker: IMoniker;
	 rot: IRunningObjectTable;
	 enum: IEnumMoniker;
	 ctx: IBindCtx;
	 malloc: IMalloc;
	 pwName: PWideChar;
	 rval: hresult;
	 reg : TRegistry;
	 name : string;
	 progId : string;
	 guid : string;
	 regId : string;
	 runningStatus : string;
	 ARunningObjData: TRunningObjData;
begin
	 reg:=nil;
	 try
		reg:=TRegistry.create;
		reg.rootkey:=HKEY_CLASSES_ROOT;
		olecheck(coGetMalloc(1, malloc));
		ClearRunningObjectsList;

		olecheck(CreateBindCtx(0, ctx));
		olecheck(ctx.GetRunningObjectTable(rot));
		olecheck(rot.enumRunning(enum));
		while true do begin
			progId:='';
			regId:='';
			runningStatus:='';
			rval := enum.Next(1, moniker, nil);
			if rval = S_FALSE then break;
			if rval <> NOERROR then oleError(rval);
			rval := moniker.GetDisplayName(ctx, nil, pwName);
			if (rval and E_OUTOFMEMORY)>0 then oleerror(rval);
			if not rval = S_OK then continue;
			name := widechartostring(pwName);
			if malloc.DidAlloc(pwName) = 1 then
				malloc.free(pwName);
			if hasGuid(name) then begin
				guid:=extractGUIDstr(name);
				if reg.openkey('\CLSID\'+guid,false) then begin
					if reg.ValueExists('') then
						regId:=reg.readstring('');
					if reg.openkey('progId',false) then
						if reg.valueExists('') then
							progId:=reg.ReadString('');
				end else
					 regId:='Not registered';
			end;
			if moniker.IsRunning(ctx,nil,nil)=S_OK then
				runningStatus:='Running';

			ARunningObjData := TRunningObjData.Create;
			ARunningObjData.Running := RunningStatus = 'Running';
			ARunningObjData.RegID := RegID;
			ARunningObjData.ProgID := ProgID;
			RunningObjectsList.AddObject(Name, ARunningObjData);

		end;
	finally
		reg.free;
	end;
end;

initialization
	RunningObjectsList := TStringList.Create;

finalization
	ClearRunningObjectsList;
	FreeAndNil(RunningObjectsList);

end.
