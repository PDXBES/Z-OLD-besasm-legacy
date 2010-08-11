unit fViewRainfallIInterface;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, fModuleMaster, RzButton, ExtCtrls, RzPanel, cxControls, cxSSheet,
  StdCtrls, Mask, RzEdit, RzBtnEdt, RzLabel;

type
  TfrmViewRainfallInterfaceFile = class(TfrmModuleMaster)
    edtFileName: TRzButtonEdit;
    RzLabel2: TRzLabel;
    cxSpreadSheet1: TcxSpreadSheet;
    RzPanel1: TRzPanel;
    btnCloseTask: TRzButton;
    RzButton1: TRzButton;
    RzButton2: TRzButton;
    procedure edtFileNameButtonClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmViewRainfallInterfaceFile: TfrmViewRainfallInterfaceFile;

implementation

uses fMain;

{$R *.dfm}

procedure TfrmViewRainfallInterfaceFile.edtFileNameButtonClick(Sender: TObject);
begin
	inherited;
	frmMain.dlgOpen.InitialDir := frmMain.InitRegistryDir;
	frmMain.dlgOpen.Title := 'Open Rainfall Interface File';
	frmMain.dlgOpen.Filter := 'Interface Files (*.int,*.rin,*.r05,*.r15,*.r20,*.r60'+
		'|*.int,*.rin,*.r05,*.r15,*.r20,*.r60|All Files|*.*';
	if frmMain.dlgOpen.Execute then
	begin
	end;

end;

end.
