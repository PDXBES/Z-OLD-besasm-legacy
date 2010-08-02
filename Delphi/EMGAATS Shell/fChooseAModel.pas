unit fChooseAModel;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, fLabeledChild, StdCtrls, ExtCtrls, FileCtrl,
  dmstatemachine, ActnList, ActnMan;

type
  TfrmChooseAModel = class(TfrmLabeledChild)
    edtWorkingDirectory: TLabeledEdit;
    btnNewModel: TButton;
    btnExtgModel: TButton;
    dlgOpen: TOpenDialog;
    lstDirectory: TDirectoryListBox;
    lstFiles: TFileListBox;
    cboDrive: TDriveComboBox;
    ActionManager1: TActionManager;
    actNewFolder: TAction;
    procedure lstDirectoryChange(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure cboDriveChange(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmChooseAModel: TfrmChooseAModel;

implementation

uses fMain;

{$R *.dfm}

procedure TfrmChooseAModel.lstDirectoryChange(Sender: TObject);
begin
  inherited;
  edtWorkingDirectory.Text := lstDirectory.Directory;
  lstFiles.Directory := lstDirectory.Directory;
end;

procedure TfrmChooseAModel.FormCreate(Sender: TObject);
begin
  inherited;
  lstdirectory.Directory :=  frmmain.modelini.readstring('Directories', 'dirmdbs', 'notfound');
  lstdirectory.update;
  lstfiles.Directory := frmmain.modelini.readstring('Directories', 'dirmdbs', 'notfound');
  edtWorkingDirectory.Text := lstDirectory.Directory;

end;

procedure TfrmChooseAModel.cboDriveChange(Sender: TObject);
begin
  inherited;
  lstdirectory.Drive := cboDrive.Drive;
  edtWorkingDirectory.Text := lstDirectory.Directory;
  lstFiles.Directory := lstDirectory.Directory;
end;

end.
