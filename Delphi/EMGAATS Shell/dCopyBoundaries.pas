{==dCopyBoundaries Unit=========================================================

	Dialog for specifying directory from which to grab stop/start pipes. Includes
	an MRU list for easily selecting previous models selected for copying.

	Revision History
	2.1    03/25/2003 (AMM) Initial inclusion

===============================================================================}
unit dCopyBoundaries;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, RzLabel, RzButton, RzShellDialogs, RzCmboBx, ExtCtrls,
  RzDlgBtn, RzPanel;

type
  TdlgCopyBoundaries = class(TForm)
    RzDialogButtons1: TRzDialogButtons;
    cmbModelDirectory: TRzMRUComboBox;
    dlgSelectDirectory: TRzSelectFolderDialog;
    btnSelectDirectory: TRzButton;
    RzLabel1: TRzLabel;
    procedure btnSelectDirectoryClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure FormShow(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  dlgCopyBoundaries: TdlgCopyBoundaries;

implementation

uses fMain, GlobalConstants;

{$R *.dfm}

procedure TdlgCopyBoundaries.btnSelectDirectoryClick(Sender: TObject);
var
	CurrentPath: String;
begin
	if Length(cmbModelDirectory.Text) > 0 then
		dlgSelectDirectory.SelectedPathName := cmbModelDirectory.Text;

	if dlgSelectDirectory.Execute then
		cmbModelDirectory.Text := dlgSelectDirectory.SelectedPathName;
end;

procedure TdlgCopyBoundaries.FormCreate(Sender: TObject);
begin
	cmbModelDirectory.MruPath := UserRegistryKey;
	cmbModelDirectory.MruSection := 'MRULists';
	cmbModelDirectory.MruID := 'CopyModelDir';
end;

procedure TdlgCopyBoundaries.FormShow(Sender: TObject);
begin
  cmbModelDirectory.UpdateMRUList;
	cmbModelDirectory.LoadMRUData(False);
end;

end.
