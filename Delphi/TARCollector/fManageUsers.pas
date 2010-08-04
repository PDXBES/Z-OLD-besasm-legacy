unit fManageUsers;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, RzButton, StdCtrls, RzLabel, RzForms, cxInplaceContainer, cxTL,
  cxControls, cxGraphics, cxCustomData, cxStyles, cxCheckBox, cxTextEdit,
  cxCalendar, cxButtonEdit;

type
  TfrmManageUsers = class(TForm)
		RzLabel1: TRzLabel;
    btnOK: TRzButton;
		btnCreateUser: TRzButton;
    grdUsers: TcxTreeList;
    colActive: TcxTreeListColumn;
    colUserName: TcxTreeListColumn;
    procedure FormShow(Sender: TObject);
    procedure btnOKClick(Sender: TObject);
    procedure btnCreateUserClick(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
	private
		{ Private declarations }
  public
    { Public declarations }
  end;

var
  frmManageUsers: TfrmManageUsers;

implementation

uses fMain;

{$R *.dfm}

procedure TfrmManageUsers.FormShow(Sender: TObject);
var
	i: Integer;
	CurrentNode: TcxTreeListNode;
	idxActive: Integer;
	idxUserName: Integer;
begin
	grdUsers.Clear;
	idxActive := colActive.ItemIndex;
	idxUserName := colUserName.ItemIndex;
	frmMain.ReadUserDirectories;
	for i := 0 to frmMain.UserDirectories.Count - 1 do    // Iterate
	begin
		CurrentNode := grdUsers.Add;
		CurrentNode.Values[idxUserName] := frmMain.UserDirectories[i];
		if frmMain.UserList.IndexOf(frmMain.UserDirectories[i]) > -1 then
			CurrentNode.Values[idxActive] := True;
	end;    // for

	// Delete keys from userlist if no directory found

	for i := 0 to frmMain.UserList.Count - 1 do    // Iterate
	begin
		if frmMain.UserDirectories.IndexOf(frmMain.UserList[i]) = -1 then
			frmMain.TARIniFile.DeleteKey(UserSectionName, frmMain.UserList[i]);
	end;    // for
end;

procedure TfrmManageUsers.btnOKClick(Sender: TObject);
var
	idxUserName: Integer;
	idxActive: Integer;
	i: Integer;
begin
	with frmMain do
	begin
		TARIniFile.EraseSection(UserSectionName);
		idxActive := colActive.ItemIndex;
		idxUserName := colUserName.ItemIndex;
		for i := 0 to grdUsers.Count - 1 do    // Iterate
		begin
			if grdUsers.Nodes[i].Values[idxActive] = True then
				TARIniFile.WriteString(UserSectionName,
					grdUsers.Items[i].Values[idxUserName], '');
		end;    // for
    TARIniFile.UpdateFile;
		TARIniFile.ReadSection(UserSectionName, UserList);
	end;
end;

procedure TfrmManageUsers.btnCreateUserClick(Sender: TObject);
var
	UserName: String;
begin
	UserName := InputBox('TAR Collector : New User', 'Enter a username', '');
	if Length(UserName) > 0 then
	begin
		CreateDir(frmMain.edtTARDirectory.Text + '\' + IndividualDirectoryText +
			'\' + UserName);
		frmMain.TARIniFile.WriteString(UserSectionName, UserName, '');
		FormShow(Sender);
	end;
end;

procedure TfrmManageUsers.FormClose(Sender: TObject;
	var Action: TCloseAction);
begin
	Action := caFree;
	frmManageUsers := nil;
end;

end.
