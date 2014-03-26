unit fMain;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, ShlObj, ActiveX, ComObj, ActnList, RzCommon, XPStyleActnCtrls,
  ActnMan, RzBckgnd, RzButton, ComCtrls, RzListVw, StdCtrls, RzLabel, Mask,
	RzEdit, RzBtnEdt, RzShellDialogs, DateUtils, Types,
	RzBorder, INIFiles, Registry, RzForms, StdActns, RzStatus, RzBHints,
	cxInplaceContainer, cxTL, cxControls, cxGraphics, cxCustomData, cxStyles,
  cxCheckBox, cxTextEdit, cxCalendar, cxButtonEdit;

const
	IndividualDirectoryText = 'Individual';
	AccountingDirectoryText = 'Accounting';
	TARIniFileName = 'TARS.ini';
	UserSectionName = 'Users';
	TARKey = 'Software\BES\TARCollector';
	TARDirectoryKey = 'TARDirectory';

type
	TfrmMain = class(TForm)
		edtTARDirectory: TRzButtonEdit;
		RzLabel1: TRzLabel;
		RzLabel2: TRzLabel;
		btnManageDirectBillingPersons: TRzButton;
		btnCollate: TRzButton;
		ActionManager1: TActionManager;
		RzFrameController1: TRzFrameController;
		btnFindTARs: TRzButton;
		actFindTARs: TAction;
    actCollectTARs: TAction;
		actManageDirectoryBillingPersons: TAction;
		dlgSelectFolder: TRzSelectFolderDialog;
		RzBorder1: TRzBorder;
    RzLabel3: TRzLabel;
    RzBorder2: TRzBorder;
    RzFormState1: TRzFormState;
    RegSettings: TRzRegIniFile;
    FileExit1: TFileExit;
    RzButton1: TRzButton;
    RzVersionInfo1: TRzVersionInfo;
    RzBalloonHints1: TRzBalloonHints;
    grdTars: TcxTreeList;
    colInclude: TcxTreeListColumn;
    colUser: TcxTreeListColumn;
    colFileName: TcxTreeListColumn;
    colFileDate: TcxTreeListColumn;
    colEditFile: TcxTreeListColumn;
    dlgOpen: TRzOpenDialog;
    procedure actFindTARsExecute(Sender: TObject);
    procedure edtTARDirectoryButtonClick(Sender: TObject);
    procedure actCollectTARsExecute(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure actManageDirectoryBillingPersonsExecute(Sender: TObject);
		procedure grdTarsCustomDrawCell(Sender: TObject; ACanvas: TcxCanvas;
      AViewInfo: TcxTreeListEditCellViewInfo; var ADone: Boolean);
    procedure colEditFilePropertiesButtonClick(Sender: TObject;
      AButtonIndex: Integer);
	private
		{ Private declarations }
		procedure CreateShortcut(BaseFilePath, AliasPath: String);
	public
		{ Public declarations }
		UserDirectories: TStringList;
		TARIniFile: TIniFile;
		UserList: TStringList;
		procedure ReadUserDirectories;
	end;

var
  frmMain: TfrmMain;

implementation

uses fManageUsers;

{$R *.dfm}

{ TForm1 }

procedure TfrmMain.CreateShortcut(BaseFilePath, AliasPath: String);
var
  IPFile: IPersistFile;
  ISLink: IShellLink;
	IObject: IUnknown;
	SavePathW: WideString;
	SavePath: PWideChar;
	TargetPath: PChar;
	TargetDirPath: PChar;
begin
	IObject := CreateComObject(CLSID_ShellLink);
	ISLink := IObject as IShellLink;
	IPFile := IObject as IPersistFile;
	with ISLink do
	begin
		TargetPath := PChar(BaseFilePath);
		TargetDirPath := PChar(ExtractFilePath(BaseFilePath));
		SetPath(TargetPath);
		SetWorkingDirectory(TargetDirPath);
	end;
	SavePathW := AliasPath;
	SavePath := PWideChar(SavePathW);
	IPFile.Save(SavePath, false)
end;

procedure TfrmMain.actFindTARsExecute(Sender: TObject);
var
	i: Integer;
	SearchPath: String;
	SearchFilesPath: String;
	IndividualPath: String;
	SearchRec: TSearchRec;
	FileAttrs: Integer;
	LatestDate: TDateTime;
	LatestTAR: String;
	CurrentNode: TcxTreeListNode;
	idxInclude: Integer;
	idxUser: Integer;
	idxFileName: Integer;
	idxFileDate: Integer;
begin
	idxInclude := colInclude.ItemIndex;
	idxUser := colUser.ItemIndex;
	idxFileName := colFileName.ItemIndex;
	idxFileDate := colFileDate.ItemIndex;

	// Find all the directories in the Individual directory; these should contain
	// all the users' subdirectories
	ReadUserDirectories;

	// Compare the directory list with the active user list and remove inactives
	for i := UserDirectories.Count - 1 downto 0 do    // Iterate
	begin
		if UserList.IndexOf(UserDirectories[i]) = -1 then
			UserDirectories.Delete(i);
	end;    // for

	SearchPath := edtTARDirectory.Text + '\' + IndividualDirectoryText;
	SearchFilesPath := SearchPath + '\*.*';

	grdTARs.Clear;

	// Find the latest PDF file in each Individual subdirectory
	for i := 0 to UserDirectories.Count - 1 do    // Iterate
	begin
		IndividualPath := SearchPath + '\' + UserDirectories[i] + '\*.pdf';
		FileAttrs := faAnyFile + $80;
		LatestDate := EncodeDate(1900,1,1);
		LatestTAR := '';
		if FindFirst(IndividualPath, FileAttrs, SearchRec) = 0 then
		begin
			repeat
				if (SearchRec.Attr and FileAttrs) = SearchRec.Attr then
				begin
					if Uppercase(ExtractFileExt(SearchRec.Name)) <> '.PDF' then
						Continue;
					if (CompareDateTime(FileDateToDateTime(SearchRec.Time),LatestDate) = GreaterThanValue) then
					begin
						LatestDate := FileDateToDateTime(SearchRec.Time);
						LatestTAR := SearchRec.Name;
					end;
				end;
			until FindNext(SearchRec) <> 0;
		end;
		CurrentNode := grdTars.Add;
		if LatestTAR = '' then
		begin
			CurrentNode.Values[idxInclude] := False;
			CurrentNode.Values[idxUser] := UserDirectories[i];
			CurrentNode.Values[idxFileDate] := '';
			CurrentNode.Values[idxFileName] := 'none';
		end
		else
		begin
			if (CompareDateTime(LatestDate, IncDay(Date, -5)) = LessThanValue) then
				CurrentNode.Values[idxInclude] := False
			else
				CurrentNode.Values[idxInclude] := True;
			CurrentNode.Values[idxUser] := UserDirectories[i];
			CurrentNode.Values[idxFileName] := LatestTAR;
			CurrentNode.Values[idxFileDate] := FormatDateTime('mm/dd/yyyy hh:mm:ss', LatestDate);
		end;
		FindClose(SearchRec);
	end;    // for
end;

procedure TfrmMain.edtTARDirectoryButtonClick(Sender: TObject);
begin
	if dlgSelectFolder.Execute then
	begin
		edtTARDirectory.Text := ExpandUNCFileName(dlgSelectFolder.SelectedPathName);
		actFindTARsExecute(Sender);
	end;
end;

procedure TfrmMain.actCollectTARsExecute(Sender: TObject);
var
	idxFileName: Integer;
	idxUser: Integer;
	idxInclude: Integer;
	CurrentNode: TcxTreeListNode;
	i: Integer;
	AccountingPath: String;
	BasePath: String;
	CurrentDate: TDateTime;
begin
	if grdTARs.Count = 0 then
	begin
		MessageDlg('There are no TARs to collect.  Make sure you have specified a '+#13+#10+'TAR directory above and clicked Find TARs to scan for TARs '+#13+#10+'to collect.', mtInformation, [mbOK], 0);
		Exit;
	end;

	AccountingPath := edtTARDirectory.Text + '\' + AccountingDirectoryText;
	idxInclude := colInclude.ItemIndex;
	idxUser := colUser.ItemIndex;
	idxFileName := colFileName.ItemIndex;
	CurrentDate := Date;

	CreateDir(AccountingPath + '\' + FormatDateTime('mmddyyyy', CurrentDate));
	for i := 0 to grdTARs.Count - 1 do    // Iterate
	begin
		CurrentNode := grdTARs.Items[i];
		if CurrentNode.Values[idxInclude] = False then
			Continue;
		if ExtractFileName(CurrentNode.Texts[idxFileName]) <> CurrentNode.Texts[idxFileName] then
			BasePath := CurrentNode.Texts[idxFileName]
		else
			BasePath := edtTARDirectory.Text + '\' + IndividualDirectoryText + '\' +
				CurrentNode.Values[idxUser] + '\' + CurrentNode.Values[idxFileName];
		CreateShortcut(BasePath, AccountingPath + '\' + FormatDateTime('mmddyyyy', CurrentDate) +
			'\' + CurrentNode.Values[idxUser] + '_' + FormatDateTime('mmddyyyy', CurrentDate) +
			'.pdf.lnk');
	end;    // for

	ShowMessage(Format('TARS collected to %s', [AccountingPath + '\' + FormatDateTime('mmddyyyy', CurrentDate)]));
end;

procedure TfrmMain.FormCreate(Sender: TObject);
var
	IniPath: String;
begin
	Caption := 'TAR Collector ' + RzVersionInfo1.FileVersion;
	RegSettings.Path := TARKey;
	RzFormState1.RestoreState;
	edtTARDirectory.Text := RegSettings.ReadString('', TARDirectoryKey, '');
	IniPath := ExtractFileDir(ParamStr(0));
	TARIniFile := TIniFile.Create(IniPath+'\'+TARIniFileName);
	UserList := TStringList.Create;
	TARIniFile.ReadSection(UserSectionName, UserList);
	UserDirectories := TStringList.Create;
end;

procedure TfrmMain.FormDestroy(Sender: TObject);
begin
	RegSettings.WriteString('', TARDirectoryKey, edtTARDirectory.Text);
	RzFormState1.SaveState;
	UserList.Free;
	UserDirectories.Free;
	TARIniFile.Free;
end;

procedure TfrmMain.ReadUserDirectories;
var
	SearchFilesPath: String;
	SearchPath: String;
	FindFirstResult: Integer;
	SearchRec: TSearchRec;
	FileAttrs: Integer;
begin
	SearchPath := edtTARDirectory.Text + '\' + IndividualDirectoryText;
	SearchFilesPath := SearchPath + '\*.*';
	FileAttrs := faDirectory;
	FindFirstResult := FindFirst(SearchFilesPath, FileAttrs, SearchRec);
	UserDirectories.Clear;
	if  FindFirstResult = 0 then
	begin
		repeat
			if (SearchRec.Attr and FileAttrs) = FileAttrs then
			begin
				if not ((SearchRec.Name = '.') or (SearchRec.Name = '..')) then
					UserDirectories.Add(SearchRec.Name);
			end;
		until FindNext(SearchRec) <> 0;
	end;
	FindClose(SearchRec);
end;

procedure TfrmMain.actManageDirectoryBillingPersonsExecute(
	Sender: TObject);
begin
	if not Assigned(frmManageUsers) then
		frmManageUsers := TfrmManageUsers.Create(Application);
	if frmManageUsers.ShowModal = mrOK then
		grdTARs.Clear;
	frmManageUsers.Free;
end;

procedure TfrmMain.grdTarsCustomDrawCell(Sender: TObject;
	ACanvas: TcxCanvas; AViewInfo: TcxTreeListEditCellViewInfo;
  var ADone: Boolean);
begin
	if AViewInfo.Node.Values[colInclude.ItemIndex] = False then
	begin
		ACanvas.Font.Color := clInactiveCaptionText;
	end;
end;

procedure TfrmMain.colEditFilePropertiesButtonClick(Sender: TObject;
	AButtonIndex: Integer);
var
	CurrentNode: TcxTreeListNode;
begin
	CurrentNode := grdTars.FocusedNode;
	dlgOpen.InitialDir := edtTARDirectory.Text + '\' + IndividualDirectoryText +
		'\' + CurrentNode.Texts[colUser.ItemIndex];
	dlgOpen.Filter := 'Timesheets (*.pdf)|*.pdf|All files (*.*)|*.*';
	if dlgOpen.Execute then
	begin
		CurrentNode.Values[colInclude.ItemIndex] := True;
		CurrentNode.Values[colFileName.ItemIndex] := dlgOpen.FileName;
		CurrentNode.Values[colFileDate.ItemIndex] := FormatDateTime('mm/dd/yyyy hh:mm:ss',
			FileDateToDateTime(FileAge(dlgOpen.FileName)));
	end;
end;

end.
