unit fMain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, RzButton, StdCtrls, RzEdit, RzLabel, RzCommon, Mask, IniFiles,
  ExtCtrls, RzPanel, RzRadGrp, RzShellDialogs, RzBtnEdt;

function Treeverse(strINImdl:Pchar; strINISection:Pchar = nil;
	bRootsOnly:Boolean = False): integer; stdcall ;
	external 'tracer.dll';

const
	sIniFileName: String = 'TempIniFile.Ini';
type
	TfrmMain = class(TForm)
		RzFrameController1: TRzFrameController;
		RzLabel1: TRzLabel;
		memUpStreamNodes: TRzMemo;
		RzLabel2: TRzLabel;
		RzButton1: TRzButton;
		RzLabel3: TRzLabel;
		edtMLinkID: TRzNumericEdit;
    rgrpTimeFrame: TRzRadioGroup;
    RzLabel4: TRzLabel;
    edtDBLocation: TRzButtonEdit;
    dlgOpen: TRzOpenDialog;
		procedure FormCreate(Sender: TObject);
		procedure RzButton1Click(Sender: TObject);
		procedure FormDestroy(Sender: TObject);
    procedure edtDBLocationButtonClick(Sender: TObject);
	private
		{ Private declarations }
		IniFile: TIniFile;
		IniPath: String;
	public
		{ Public declarations }
	end;

var
	frmMain: TfrmMain;

implementation

{$R *.dfm}

procedure TfrmMain.FormCreate(Sender: TObject);
begin
	IniPath := ExtractFilePath(ParamStr(0));
	IniFile := TIniFile.Create(IniPath+sIniFileName);
	IniFile.EraseSection('treeverse');
	IniFile.EraseSection('FieldNames');
	IniFile.EraseSection('rootlinks');
	IniFile.WriteString('treeverse', 'tracefile', IniPath+'mdltrace.txt');
	IniFile.WriteString('treeverse', 'linkdbsrc',
		edtDBLocation.Text);
	IniFile.WriteString('treeverse', 'isdebugon', 'YES');
	IniFile.WriteString('treeverse', 'debugfile', 'tracerdebug.txt');
	IniFile.WriteString('treeverse', 'enablemessages', 'YES');
	IniFile.WriteString('treeverse', 'Link', 'MLinkID');
	IniFile.WriteString('treeverse', 'USNode', 'USNode');
	IniFile.WriteString('treeverse', 'DSNode', 'DSNode');
	IniFile.WriteString('treeverse', 'Reach', 'LinkReach');
	IniFile.WriteString('treeverse', 'Element', 'ReachElement');
end;

procedure TfrmMain.RzButton1Click(Sender: TObject);
var
	Err: Integer;
	IniFileName: PChar;
	WriteFile: String;
begin
	case rgrpTimeFrame.ItemIndex of
		0: IniFile.WriteString('treeverse', 'linktblsrc', 'mst_links_existing');
		1: IniFile.WriteString('treeverse', 'linktblsrc', 'mst_links_future');
	end;
	IniFile.EraseSection('rootlinks');
	IniFile.WriteString('rootlinks', edtMLinkID.Text, '');
	IniFile.WriteString('treeverse', 'linkdbsrc',
		edtDBLocation.Text);
	WriteFile := IniPath+sIniFileName;
	Err := Treeverse(PChar(WriteFile));
	memUpstreamNodes.Lines.LoadFromFile(IniPath+'mdltrace.txt');
end;

procedure TfrmMain.FormDestroy(Sender: TObject);
begin
	IniFile.Free;
end;

procedure TfrmMain.edtDBLocationButtonClick(Sender: TObject);
begin
	dlgOpen.Title := 'Open masterportal.mdb';
	dlgOpen.InitialDir := ExtractFileDir(edtDBLocation.Text);
	dlgOpen.Filter := 'Microsoft Access Database (*.mdb)|*.mdb';
	if dlgOpen.Execute then
		edtDBLocation.Text := dlgOpen.FileName;
end;

end.
