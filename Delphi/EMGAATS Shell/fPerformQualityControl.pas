unit fPerformQualityControl;

interface

uses
	Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, fLabeledChild, StdCtrls, fBuildModels, fMain, ActnList, RzButton,
	ADODB, DB, RzPrgres, RzLabel, pngimage, RzBckgnd, ExtCtrls, RzPanel;

type
	TfrmPerformQualityControl = class(TfrmLabeledChild)
		Label2: TLabel;
		Label3: TLabel;
		btnBuildSubcatchments: TButton;
		btnBuildPipeSystem: TButton;
		btnBuildModels: TButton;
		RzButton1: TRzButton;
		ActionList1: TActionList;
		actCreateQCWorkspaces: TAction;
		RzButton2: TRzButton;
		actRetraceSurfaceSubcatchments: TAction;
		Label4: TLabel;
		RzButton3: TRzButton;
		actRetraceLaterals: TAction;
		RzButton4: TRzButton;
		actRetraceLinks: TAction;
    adoLinksConnection: TADOConnection;
		adoLinks: TADOTable;
    adoNodes: TADOTable;
    adoCommand: TADOCommand;
    adoNodesConnection: TADOConnection;
    adoLinksDSTerminals: TADOQuery;
    adoLinksDSTerminalsLinkID: TIntegerField;
    adoLinksLinkID: TIntegerField;
    adoLinksLinkReach: TWideStringField;
    adoLinksReachElement: TIntegerField;
    adoLinksTraceVisit: TWideStringField;
    adoNodesNode: TWideStringField;
    adoNodesTraceVisit: TWideStringField;
    prgRetrace: TRzProgressBar;
    adoLinksMLinkID: TIntegerField;
		procedure btnBuildModelsClick(Sender: TObject);
		procedure btnBuildPipeSystemClick(Sender: TObject);
		procedure btnBuildSubcatchmentsClick(Sender: TObject);
		procedure actRetraceSurfaceSubcatchmentsExecute(Sender: TObject);
		procedure actRetraceLateralsExecute(Sender: TObject);
		procedure actRetraceLinksExecute(Sender: TObject);
	private
		{ Private declarations }
	public
		{ Public declarations }
	end;

var
	frmPerformQualityControl: TfrmPerformQualityControl;

{function Treeverse(strINImdl:Pchar; strINISection:Pchar = nil;
	bRootsOnly:Boolean = False): integer; stdcall ;
	external 'tracer.dll';}

implementation

uses fStatus, uWorkbenchDefs, GlobalConstants, StStrms, StStrL;

{$R *.dfm}

procedure TfrmPerformQualityControl.btnBuildModelsClick(Sender: TObject);
begin
	inherited;
	frmMain.actBuildModelsExecute(Sender);
end;

procedure TfrmPerformQualityControl.btnBuildPipeSystemClick(
	Sender: TObject);
begin
	inherited;
	frmBuildModels.actBuildPipeSystemExecute(Sender);
end;

procedure TfrmPerformQualityControl.btnBuildSubcatchmentsClick(
	Sender: TObject);
begin
	inherited;
	frmBuildModels.actBuildSubcatchmentsExecute(Sender);
end;

procedure TfrmPerformQualityControl.actRetraceSurfaceSubcatchmentsExecute(
	Sender: TObject);
var
	ReturnCode: Integer;
begin
	inherited;
	ReturnCode := frmStatus.MIRunMenuCmd(frmmain.goleMI, 'Retracing surface subcatchments',
		EMG_RetraceSurfaceSC, 'Quality Control',EMG_AppName, strMBmsg , strMBrc);
end;

procedure TfrmPerformQualityControl.actRetraceLateralsExecute(
	Sender: TObject);
var
	ReturnCode: Integer;
begin
	inherited;
	ReturnCode := frmStatus.MIRunMenuCmd(frmmain.goleMI, 'Retracing laterals',
		EMG_ReSnapModelLaterals,'Quality Control', EMG_AppName, strMBmsg , strMBrc);
end;

procedure TfrmPerformQualityControl.actRetraceLinksExecute(
	Sender: TObject);
var
	rc_treeverse: Integer;
	CurrentModelDir: String;
	RetraceTextFileName: String;
	RetraceTextFileStream: TFileStream;
	RetraceTextStream: TStAnsiTextStream;
	CurrentLine: String;
	Tokens: TStringList;
	OriginalRootLinks: TStringList;
	i: Integer;
begin
	inherited;
	// Assertions
	Assert(not adoLinksDSTerminals.Active, 'adoLinksDSTerminals should not be open');
	Assert(not adoLinksConnection.Connected, 'adoLinksConnection should not be open');
	Assert(not adoNodesConnection.Connected, 'adoNodesConnection should not be open');

	// Connect to the databases
	CurrentModelDir := ExtractFileDir(frmMain.ModelIni.FileName);

	adoLinksConnection.Close;
	adoLinksConnection.ConnectionString :=
		'Provider=Microsoft.Jet.OLEDB.4.0;'+
		'Data Source='+CurrentModelDir+'\links\mdl_Links_ac.mdb;'+
		'Persist Security Info=False';
	adoLinksConnection.Open;

	adoNodesConnection.Close;
	adoNodesConnection.ConnectionString :=
		'Provider=Microsoft.Jet.OLEDB.4.0;'+
		'Data Source='+CurrentModelDir+'\nodes\mdl_Nodes_ac.mdb;'+
		'Persist Security Info=False';
	adoNodesConnection.Open;

	// Set TraceVisit to false for all mdl_links_ac and mdl_nodes_ac
	adoCommand.Connection := adoLinksConnection;
	adoCommand.CommandText := 'UPDATE mdl_Links_ac SET mdl_Links_ac.TraceVisit = "F",'+
		'mdl_Links_ac.LinkReach = Null, mdl_Links_ac.ReachElement = 0;';
	adoCommand.Execute;

	adoCommand.Connection := adoNodesConnection;
	adoCommand.CommandText := 'UPDATE mdl_Nodes_ac SET mdl_Nodes_ac.TraceVisit = "F";';
	adoCommand.Execute;

	// Reset the RetraceRootLinks to all the original downstream terminal links available in
	// the model
	frmMain.ModelIni.EraseSection('RetraceRootLinks');
	OriginalRootLinks := TStringList.Create;
	frmMain.ModelIni.ReadSection('RootLinks',OriginalRootLinks);
	adoLinks.Open;
	for i := 0 to OriginalRootLinks.Count-1 do
	begin
		if adoLinks.Locate('MLinkID', StrToInt(OriginalRootLinks[i]), [loCaseInsensitive]) then
			frmMain.ModelIni.WriteString('RetraceRootLinks',adoLinksLinkID.AsString,'');
	end;
	adoLinks.Close;

	// Add to the RetraceRootLinks all downstream terminal links available in
	// the model without MLinkIDs
	adoLinksDSTerminals.Close;
	adoLinksDSTerminals.Open;
	adoLinksDSTerminals.First;
	while not adoLinksDSTerminals.Eof do
	begin
		frmMain.ModelIni.WriteString('RetraceRootLinks',adoLinksDSTerminalsLinkID.AsString,'');
		adoLinksDSTerminals.Next;
	end;
	adoLinksDSTerminals.Close;

	frmMain.ModelIni.UpdateFile;

	// Trace the model
//	rc_treeverse := Treeverse(pchar(frmMain.ModelIni.Filename), strRetraceSection, True);
	if rc_treeverse <= 0 then
	begin
		ShowMessage('Nothing traced');
		Exit;
	end;

	// Read in retrace.txt and update the mdl_links_ac and mdl_nodes_ac tables
	adoLinks.Open;
	adoNodes.Open;

	RetraceTextFileName := frmMain.ModelIni.ReadString('Retrace','tracefile','');
	try
		try
			RetraceTextFileStream := TFileStream.Create(RetraceTextFileName,
				fmOpenRead or fmShareDenyWrite);
			RetraceTextStream := TStAnsiTextStream.Create(RetraceTextFileStream);

			Tokens := TStringList.Create;
			CurrentLine := RetraceTextStream.ReadLine; // First line is the header
			prgRetrace.TotalParts := RetraceTextStream.FastSize;
			prgRetrace.PartsComplete := 0;
			prgRetrace.Show;
			while not RetraceTextStream.AtEndOfStream do
			begin
				CurrentLine := RetraceTextStream.ReadLine;
				prgRetrace.PartsComplete := RetraceTextStream.Position;
				prgRetrace.Update;

				ExtractTokensL(CurrentLine, ',', '''', False, Tokens);

				if adoLinks.Locate('LinkID', Tokens[0], [loCaseInsensitive]) then
				begin
					adoLinks.Edit;
					adoLinksTraceVisit.Value := 'T';
					adoLinksLinkReach.Value := Tokens[3];
					adoLinksReachElement.Value := StrToInt(Tokens[4]);
					adoLinks.Post;
				end;

				if adoNodes.Locate('Node', Tokens[1], []) then
				begin
					adoNodes.Edit;
					adoNodesTraceVisit.Value := 'T';
					adoNodes.Post;
				end;

				if adoNodes.Locate('Node', Tokens[2], []) then
				begin
					adoNodes.Edit;
					adoNodesTraceVisit.Value := 'T';
					adoNodes.Post;
				end;
			end;

		except
			on EFOpenError do
				MessageDlg('Could not open retrace file. Make sure retrace.txt in model root '
					+#13+#10+'directory is not in use, and try again.', mtError, [mbOK], 0);
		end;
	finally
		adoNodes.Close;
		adoLinks.Close;
		adoNodesConnection.Close;
		adoLinksConnection.Close;
		Tokens.Free;
		prgRetrace.Hide;
		if Assigned(RetraceTextStream) then
		begin
			RetraceTextStream.Free;
			RetraceTextFileStream.Free;
		end;
	end;

end;

end.
