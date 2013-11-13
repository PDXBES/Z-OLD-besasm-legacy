{==dmTracer3 Unit===============================================================

	Tracer Data Module

	Revision History
	1.1    03/25/2003 (AMM) Revisions to status
													Revised name of status form from Form2 to frmTracerStatus
	1.0    2/2002     (DJC) Initial inclusion
===============================================================================}

unit dmTracer3;

interface


uses
	SysUtils, Classes, ActiveX, DB, ADODB, Dialogs, forms, math,
	//QcomCtrls,
	fTracerStatus,
	inifiles;
	//djchash;


const
	DefaultINITreeverseSection = 'Treeverse';

type

	//PlinkData = ^LinkData;

	//basic pipe data
	TLinkData = Class
		public
    strLinkID, strUSID, strDSID, strReach, ReachElt : string;
    visited : boolean;
  end;

  TTraceData1 = class(TDataModule)
		ADOSource: TADOQuery;

		private
			mydbsrc,
			mytblsrc,
			mytracedest,
			myNameLink,
			myNameUSN,
			myNameDSN,
			myNameReach,
			myNameElement : string;

			TerminalLinksPrefix: String;
				{ Section prefix for RootLinks and StopLinks in case
					caller provides a different section other than
					nil ('Treeverse') for initialization }

			myfounderrors : boolean;
			myNtracedLinks : integer;
			{Index to Link ID
			 The records are read and memory is allocated into this list
			 Ignores (but does not trap for) duplicate Link IDs}
			HshLstLink: THashedStringList;

			{Index to Downstream Node ID (strDSID)
			 The records are read and memory is allocated into this list}
			HshLstDSID: THashedStringList;

			HshLstRoot: THashedStringList;

			HshLstStop: THashedStringList;

			debug_Filename : string;
			debug_fs : TFileStream;
			isdebug : boolean;

			enablemessages: boolean;

			{ setConStr: establish connections with external tables}
			procedure setConStr();

			{loadroots: read recordset table into hashed string list
			}
			procedure loadroots();

			{loadstopw: read recordset table into hashed string list
			}
			procedure loadstops(IsRequired: Boolean);

			{TraverseAllTrees: read link database}
			procedure loadlinks();

      {TraverseAllTrees: using the root node table specified}
      procedure TraverseAllTrees();

      {TraverseOneTree: the recursive proceedure that does the work}
      procedure TraverseOneTree(ndx_link:integer; myReach:string; myelt:integer);

      {exportdata: export data to a text file
      }
			procedure exportdata();

      {FreeHash: release the objects and the hashedstringlist indexes
      }
      procedure FreeHash();


			procedure debugprintinit();
      procedure debugprint(s:string);

      property dbsrc: string Read mydbsrc write mydbsrc;
      property tblsrc: string Read mytblsrc write mytblsrc;
      property tracefile: string Read mytracedest write mytracedest;

      property NameLink: string Read myNameLink write myNameLink;
      property NameUSN: string Read myNameUSN write myNameUSN;
      property NameDSN: string Read myNameDSN write myNameDSN;
      property NameReach: string Read myNameReach write myNameReach;
      property NameElement: string Read myNameElement write myNameElement;

      property NTracedLinks: integer Read myNtracedLinks write myNtracedLinks;
      property founderrors: boolean Read myfounderrors write myfounderrors;

	 public

	end;

var


	TraceData1: TTraceData1;
	gINImdl: Tmeminifile;



function Treeverse(strINImdl:Pchar; strINISection:Pchar = nil;
	bRootsOnly:Boolean = False): integer; stdcall ;

{$R *.dfm}

implementation
{Traces a network and grabs the contributing network given a downstream
	set of nodes and an upstream set of links}

function Treeverse(strINImdl:Pchar; strINIsection:Pchar = nil;
	bRootsOnly:Boolean = False): integer; stdcall ;


var
	strdebug, strEnabledMessages : string;


begin
	try

		gINImdl := TMemInifile.create(strINImdl);
		tracedata1 := TTraceData1.create(application);
		tracedata1.founderrors := false;

		if not Assigned(strINIsection) then
		begin
			strINISection := PChar(DefaultINITreeverseSection);
			tracedata1.TerminalLinksPrefix := '';
		end
		else if (String(strIniSection) = DefaultINITreeverseSection) then
			tracedata1.TerminalLinksPrefix := ''
		else
			tracedata1.TerminalLinksPrefix := String(strINIsection);

		if uppercase(gINImdl.ReadString(strINISection,'isdebugon','yes')) = 'NO' then tracedata1.isdebug := false
    else  tracedata1.isdebug := true;

		strEnabledMessages := uppercase(gINImdl.ReadString(strINISection,'enablemessages','NO'));
    if  (strEnabledMessages = 'YES') or (strEnabledMessages = 'TRUE') or (strEnabledMessages = 'T') then
    tracedata1.enablemessages := true
		else  tracedata1.enablemessages := false;


		strdebug := ExtractFileName(gINImdl.ReadString(strINISection,'debugfile','unknown'));
    if strdebug = 'unknown' then
      tracedata1.debug_filename := extractfiledir(strINImdl) + '\tracedebug.txt'
    else
      tracedata1.debug_filename := extractfiledir(strINImdl) + '\' + strdebug;

		{INITIALIZE DEBUG}
		tracedata1.debugprintinit();

		Tracedata1.dbsrc := gINImdl.ReadString(strINISection,'linkdbsrc','unknown');
		if tracedata1.dbsrc = 'unknown' then
    begin
			tracedata1.debugprint('Fatal: linkdbsrc variable in '+strINISection+' section not specified');
			tracedata1.founderrors := true;
		end;

		tracedata1.tblsrc := gINImdl.ReadString(strINISection,'linktblsrc','unknown');
		if tracedata1.tblsrc = 'unknown' then
		begin
			tracedata1.debugprint('Fatal: linktblsrc variable in '+strINISection+' section not specified');
			tracedata1.founderrors := true;
		end;

		tracedata1.tracefile := gINImdl.ReadString(strINISection,'tracefile','unknown');
		if tracedata1.tracefile = 'unknown' then
		begin
			tracedata1.debugprint('Fatal: tracefile variable in '+strINISection+' section not specified');
			tracedata1.founderrors := true;
		end;

		tracedata1.NameLink := gINImdl.ReadString(strINISection,'Link','unknown');
		if tracedata1.NameLink = 'unknown' then
		begin
			tracedata1.debugprint('Fatal: Link variable in '+strINISection+' section not specified');
			tracedata1.founderrors := true;
		end;

		tracedata1.NameUSN := gINImdl.ReadString(strINISection, 'USNode','unknown');
		if tracedata1.NameUSN = 'unknown' then
		begin
			tracedata1.debugprint('Fatal: USNode variable in '+strINISection+' section not specified');
			tracedata1.founderrors := true;
		end;

		tracedata1.NameDSN := gINImdl.ReadString(strINISection,'DSNode','unknown');
		if tracedata1.NameDSN = 'unknown' then
		begin
			tracedata1.debugprint('Fatal: DSNode variable in '+strINISection+' section not specified');
			tracedata1.founderrors := true;
		end;

		tracedata1.NameReach := gINImdl.ReadString(strINISection,'Reach','unknown');
		if tracedata1.NameReach = 'unknown' then
		begin
			tracedata1.debugprint('Fatal: Reach variable in '+strINISection+' section not specified');
			tracedata1.founderrors := true;
		end;

		tracedata1.NameElement := gINImdl.ReadString(strINISection,'Element','unknown');
		if tracedata1.NameElement = 'unknown' then
		begin
			tracedata1.debugprint('Fatal: Element variable in '+strINISection+' section not specified');
			tracedata1.founderrors := true;
		end;

		if tracedata1.founderrors then
		begin
			tracedata1.Free;
			result := -1;
			exit;
		end;

		if tracedata1.enablemessages then
		begin
      frmTracerStatus := TfrmTracerStatus.create(Application);
			{note: frmTracerStatus frees itself}
      frmTracerStatus.StatusText1.Caption := 'Connecting to ' + Tracedata1.dbsrc;
      frmTracerStatus.show;
      frmTracerStatus.Setfocus;
      frmTracerStatus.Update;
    end;

    application.processmessages;

    tracedata1.myNtracedLinks := 0;
    tracedata1.setconstr;
		tracedata1.loadroots;
		tracedata1.loadstops(not bRootsOnly);
		tracedata1.loadlinks;
		tracedata1.TraverseAllTrees;
		tracedata1.exportdata;
    if tracedata1.enablemessages then
  		frmTracerStatus.Close;
		tracedata1.debug_fs.Free;
		tracedata1.freehash;
    gINImdl.Free;

		if tracedata1.founderrors then  Result := -1
		else result := tracedata1.NtracedLinks;
		FreeAndNil(tracedata1);
	except
		on E: Exception do
		begin
			if Assigned(tracedata1) and tracedata1.enablemessages then
				messageDlg('Error in Tracer.dll: ' + E.message, mtError, [MBOK],0);
      result := -1;
    end;
  end; //try
	//ShowMessage('Ended Treeverse');
end;


{ need some error checking here to make sure we got our tables and
  at least one root node}
procedure TTraceData1.setConStr;
begin
 {source -- a query
  query string tests for nulls
  }
  ADOSource.ConnectionString :=
    'Provider=Microsoft.Jet.OLEDB.4.0;Data Source=' +
		dbsrc + ';Persist Security Info=False';
	ADOSource.CacheSize := 66000;

	ADOSource.SQL.Clear;
	ADOSource.SQL.Add('Select ['+ NameLink + '], [' + NameUSN + '], [' + nameDSN + ']');
	ADOSource.SQL.Add(' FROM [' + tblsrc + ']');
	ADOSource.SQL.Add(' WHERE ((([' + nameLink + ']) Is Not Null) AND (([' + nameUSN +
	']) Is Not Null) AND (([' + nameDSN + ']) Is Not Null))');

end;

procedure ttracedata1.loadroots;
begin
	if tracedata1.enablemessages then
	begin
		frmTracerStatus.StatusText1.Caption := 'Loading Roots';
		frmTracerStatus.Update;
	end;

	HshLstRoot := THashedstringList.create;
	HshLstRoot.Duplicates := dupignore;

	gINImdl.ReadSection(TerminalLinksPrefix+'rootlinks',HshLstRoot);
	if hshlstroot.Count < 1 then debugprint('no roots found')
end;

procedure ttracedata1.loadstops(IsRequired: Boolean);
//var
//  done : boolean;
begin
	if tracedata1.enablemessages then
	begin
		frmTracerStatus.StatusText1.Caption := 'Loading Stops';
		frmTracerStatus.Update;
	end;

	HshLstStop := THashedstringList.create;
	HshLstStop.Duplicates := dupignore;

	if IsRequired then
		gINImdl.ReadSection(TerminalLinksPrefix+'stoplinks',HshLstStop);

end;

{ ttracedata1.loadlinks
	PRE:
	1) SetConStr has been run
	2) the connection strings have been set
	3) the source table has been set
}
procedure ttracedata1.loadlinks;
var
	myreccount: integer;
	item : TLinkData;

begin

	if tracedata1.enablemessages then
	begin
		frmTracerStatus.StatusText1.Caption := 'Loading Links';
		frmTracerStatus.Show;
		frmTracerStatus.setfocus;
		frmTracerStatus.Update;
	end;

	{this is the link ID index}
//  HshLstLink := TdjcHashedstringList.create;
	HshLstLink := THashedstringList.create;
	HshLstLink.Sorted := true;
	HshLstLink.Duplicates := dupignore;

	{this is the DwnNode index}
 // HshLstDSID := TdjcHashedstringList.create;
	HshLstDSID := THashedstringList.create;
	HshLstDSID.Sorted := true;
	HshLstDSID.Duplicates := dupaccept;

	ADOSource.Open ;
	ADOSource.Recordset.MoveFirst;

	if tracedata1.enablemessages then
	begin
		frmTracerStatus.statustext1.caption := string('Importing From: ' + tblsrc);
		frmTracerStatus.statustext2.caption := string(' Total Records: ' + inttostr(ADOSource.Recordset.RecordCount));
		frmTracerStatus.prgStatus.TotalParts := ADOSource.Recordset.RecordCount;
		HshLstLink.Capacity := ADOSource.Recordset.RecordCount;
		HshLstDSID.Capacity := ADOSource.Recordset.RecordCount;
	end;

	myreccount :=0 ;
	while not ADOSource.RecordSet.EOF do
	begin
		item := Tlinkdata.create;
		item.strLinkID := string(ADOSource.Recordset.Fields[NameLink].value);
		item.strUSID := string(ADOSource.Recordset.Fields[NameUSN].value) ;
		item.strDSID := string(ADOSource.Recordset.Fields[NameDSN].value);
		item.Visited := false;

//		CodeSite.SendFmtMsg(csmRed, 'Adding Record %d', [ADOSource.RecNo]);
//		CodeSite.SendMsg('Adding Link: '+item.strLinkID);
		HshLstLink.AddObject(item.strLinkID,item);
//		CodeSite.SendMsg('Adding DSID: '+item.strDSID);
		HshLstDSID.AddObject(item.strDSID,item);
//		CodeSite.SendMsg(csmBlue, 'Finished adding');

		myreccount := myreccount + 1;


		if tracedata1.enablemessages and (myreccount mod 100 = 0) then
		begin
			frmTracerStatus.prgStatus.PartsComplete := MyRecCount;
			frmTracerStatus.StatusText3.caption :=  'Records Imported: ' + inttostr(myreccount);
			frmTracerStatus.Setfocus;
			frmTracerStatus.Update;
		end;

	 ADOSource.recordset.movenext;
	 application.processmessages;
	end;

	ADOSource.Close;

end;

procedure TTracedata1.TraverseOneTree(ndx_link:integer; myReach:string; myelt:integer);
var
  uslinkcount, ndx_node, ndx_link_next: integer;
  done, confluence : boolean;
  dsnode_next, link_next : string;

  mystrlinkID : string ;


begin
  mystrlinkID  := Tlinkdata(HshLstLink.Objects[ndx_link]).strLinkID;
  if Tlinkdata(HshLstLink.Objects[ndx_link]).visited then exit;

  {20020408 DJC see note below regarding stop pipes}

  Tlinkdata(HshLstLink.Objects[ndx_link]).visited := true;
  Tlinkdata(HshLstLink.Objects[ndx_link]).strReach := myReach ;
  Tlinkdata(HshLstLink.Objects[ndx_link]).ReachElt := inttostr(myelt) ;

  debugprint( Tlinkdata(HshLstLink.Objects[ndx_link]).strLinkID + ',' + myreach + ',' + inttostr(myelt) );

  {20020408 DJC moved this code from above so stop pipe is visited before exit}
  {check if it is a stop pipe}
  if HshLstStop.indexof( mystrlinkID)>=0 then exit;

  { find any links if there are there any other links upstream by looking
    for links whose dsnode = the current link's upstream node}
  dsnode_next := Tlinkdata(HshLstLink.Objects[ndx_link]).strUSID;
  if HshLstDSID.Find(dsnode_next,ndx_node) = false then exit;
  //Assert: There is at least one other link

  //if > 1 upstream link then we are at a confluence
  confluence := false;
  if ndx_node + 1 < HshLstDSID.count then //not at the end of our list
    if HshLstDSID[ndx_node + 1] = dsnode_next then confluence := true;

  //get the first link associated with the dsnode_next
	link_next := Tlinkdata(HshLstDSID.Objects[ndx_node]).strLinkID;
  if HshLstLink.Find(link_next, ndx_link_next) = false then
  begin
    {Assert since there is a index for the dsnode, there has to be an
    index for the associated link so this should never be reached}
       //there should be an REAL error hander call here
    showmessage('unreached condition no index for ' + link_next);
    exit;
  end;

  if not confluence then TraverseOneTree(ndx_link_next, myReach, myelt + 1)
  else
  begin
    done := false;
    uslinkcount :=1;
    while not done  do
    begin
      TraverseOneTree(ndx_link_next, myReach + '.' + inttostr(uslinkcount),1);
      if ndx_node + 1 >= HshLstDSID.count then done := true
      else
      begin
        if HshLstDSID[ndx_node + 1] <> dsnode_next then done := true
        else
        begin
          inc(ndx_node);
          inc(uslinkcount);
          link_next := Tlinkdata(HshLstDSID.Objects[ndx_node]).strLinkID;
          if not HshLstLink.Find(link_next, ndx_link_next) then
          begin
            {Assert since there is a index for the dsnode, there has to be an
            index for the associated link so this should never be reached}
            //there should be an REAL error hander call here
            showmessage('unreached confluence condition no index for ' + link_next);
            exit;
          end;
        end; //if
      end;
    end; //while
	end; //else confluence
end;


procedure TTraceData1.TraverseAllTrees;
var
  mylinkID : string;
	nxtndx, ndx_root, rootnum: integer;
begin

  if tracedata1.enablemessages then
  begin
    frmTracerStatus.statustext1.caption := string('Traversing ');
    frmTracerStatus.statustext2.caption :='';
    frmTracerStatus.statustext3.caption :='';
    frmTracerStatus.setfocus;
    frmTracerStatus.Update;
    application.processmessages;
  end;

  {number of roots traversed -- 1st digit of reach}
  rootnum :=1;
  {index of the root link table}
  ndx_root :=0;

  while ndx_root < HshLstRoot.count do
  begin
    myLinkID := HshLstRoot[ndx_root];


    if HshLstLink.Find(myLinkID,nxtndx) then
    begin
      if tracedata1.enablemessages then
      begin
        frmTracerStatus.statustext1.caption := string('Traversing from Link:' + mylinkID);
        frmTracerStatus.Update;
        application.processmessages;
      end;

      TraverseOneTree(nxtndx, inttostr(rootnum), 1);
      inc(rootnum);
    end; //if
    inc(ndx_root);

  end; // while
end;

procedure TTracedata1.exportdata();
var
  mydata : Tlinkdata;

  ndx,nvisited : integer;
	fs: TFileStream;
  s, filename : string;


begin
  Filename := tracefile;
  fs := TFileStream.Create(Filename, fmCreate or fmOpenWrite);

  ndx := 0;

  if tracedata1.enablemessages then
  begin
    frmTracerStatus.StatusText1.caption := string('Exporting Pipes:');
    frmTracerStatus.Update;
    application.processmessages;
  end;

  s := string(NAMELink + ',' + NameUSN + ',' + NameDSN + ',' + NameReach + ',' + NameElement + #13 + #10);
  fs.Write(pchar(s)^, Length(s));
  nvisited := 0;
  while ndx < HshLstDSID.count do
  begin

    mydata := Tlinkdata(HshLstDSID.Objects[ndx]);

    if Tlinkdata(HshLstDSID.Objects[ndx]).visited = true then
    begin
      inc(nvisited);
      s := string(mydata.strLinkID + ',' + mydata.strUSID + ',' + mydata.strDSID + ',' + mydata.strReach  + ',' + mydata.ReachElt + #13 + #10);
      fs.Write(pchar(s)^, Length(s));
    end;
    inc(ndx);

  end;

  if tracedata1.enablemessages then
  begin
    frmTracerStatus.StatusText1.caption := string('Export Complete:' + inttostr(nvisited) + ' links found');
		frmTracerStatus.Update;
  end;
  fs.free;
  application.processmessages;

  myntracedlinks := nvisited;

end;

procedure TTracedata1.freehash();
var
  ndx : integer;

begin
  ndx := 0;
  while ndx < HshLstDSID.count do
  begin
    HshLstDSID.Objects[ndx].free;
    inc(ndx);
  end;

  HshLstLink.clear;
  HshLstDSID.clear;
	HshLstRoot.clear;
  HshLstStop.clear;

  HshLstLink.Free;
  HshLstDSID.Free;
  HshLstRoot.Free;
  HshLstStop.Free;
end;

procedure TTracedata1.debugprintinit();
begin
  if isdebug then
  begin
    debug_fs := TFileStream.Create(debug_Filename, fmCreate or fmOpenWrite);
  end;
end;

procedure TTracedata1.debugprint(s:string);
var
  sline : string;
begin
  sline := s + #13 + #10;
  if isdebug then debug_fs.Write(pchar(sline)^, Length(sline));
end;

initialization
  //required in dll's that use ADO
  CoInitialize(nil);

finalization
  //required in dll's that use ADO
  CoUninitialize;
  //ShowMessage('Finalizing dmTracer3');
end.
