unit fChooseAProject;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, fLabeledChild, StdCtrls, ExtCtrls, FileCtrl,
  dmstatemachine;

type
  TfrmChooseAProject = class(TfrmLabeledChild)
    edtWorkingDirectory: TLabeledEdit;
    Button1: TButton;
    Button2: TButton;
    dlgOpen: TOpenDialog;
    lstDirectory: TDirectoryListBox;
    lstFiles: TFileListBox;
    cboDrive: TDriveComboBox;
    procedure lstDirectoryChange(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure cboDriveChange(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmChooseAProject: TfrmChooseAProject;

implementation

{$R *.dfm}

procedure TfrmChooseAProject.lstDirectoryChange(Sender: TObject);
begin
  inherited;
  edtWorkingDirectory.Text := lstDirectory.Directory;
  lstFiles.Directory := lstDirectory.Directory;
end;

procedure TfrmChooseAProject.FormCreate(Sender: TObject);
begin
  inherited;
  lstdirectory.Directory :=  gloinimst.readstring('Directories', 'dirmdbs', 'notfound');
  lstdirectory.update;
  lstfiles.Directory := gloinimst.readstring('Directories', 'dirmdbs', 'notfound');
  edtWorkingDirectory.Text := lstDirectory.Directory;

end;

procedure TfrmChooseAProject.cboDriveChange(Sender: TObject);
begin
  inherited;
  lstdirectory.Drive := cboDrive.Drive;
  edtWorkingDirectory.Text := lstDirectory.Directory;
  lstFiles.Directory := lstDirectory.Directory;
end;

end.
