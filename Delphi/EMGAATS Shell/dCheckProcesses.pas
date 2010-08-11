unit dCheckProcesses;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, RzLabel, ExtCtrls, RzPanel, RzLstBox, RzChkLst, RzDlgBtn,
  RzRadGrp, pngimage, pngextra, RzButton, RzRadChk;

type
  TdlgCheckProcesses = class(TForm)
    RzGridPanel1: TRzGridPanel;
    rgrpAction: TRzRadioGroup;
    dlgButtons: TRzDialogButtons;
    chklstProcesses: TRzCheckList;
    RzLabel2: TRzLabel;
    RzPanel1: TRzPanel;
    Image1: TImage;
    RzLabel1: TRzLabel;
    chkRememberShowVitalProgramsWarning: TRzCheckBox;
    procedure FormCreate(Sender: TObject);
    procedure dlgButtonsClickOk(Sender: TObject);
    procedure FormCloseQuery(Sender: TObject; var CanClose: Boolean);
    procedure rgrpActionChanging(Sender: TObject; NewIndex: Integer;
      var AllowChange: Boolean);
  private
    { Private declarations }
    procedure FillProcesses;
  public
    { Public declarations }
    function EMGAATSProcessesActive: Boolean;
    procedure Execute;
  end;

var
  dlgCheckProcesses: TdlgCheckProcesses;

implementation

uses fMain, TLHelp32, uEMGAATSSystemConfig;

type
  TProcessAction = (paCloseProcesses, paContinue, paExit);

resourcestring
  MapInfoExeName = 'MAPINFOW.EXE';
  AccessExeName = 'MSACCESS.EXE';

type
  TProcessEntryAsObject = class
  public
    ProcessEntry: TProcessEntry32;
    procedure Kill;
  end;
{$R *.dfm}

{ TdlgCheckProcesses }

function TdlgCheckProcesses.EMGAATSProcessesActive: Boolean;
begin
  Result := chkLstProcesses.Count > 0;
end;

procedure TdlgCheckProcesses.Execute;
var
  i: Integer;
  Action: TProcessAction;
begin
  Action := TProcessAction(rgrpAction.ItemIndex);
  SystemConfig.ShowVitalProgramsWarning := not chkRememberShowVitalProgramsWarning.Checked;
  case Action of
    paCloseProcesses:
      begin
        for i := 0 to chklstProcesses.Count - 1 do
        begin
          if chklstProcesses.ItemChecked[i] then
            (chklstProcesses.Items.Objects[i] as TProcessEntryAsObject).Kill;
        end;
        SystemConfig.VitalProgramsAction := vpaCloseAll;
        SystemConfig.Update;
      end;
    paContinue:
      begin
        SystemConfig.VitalProgramsAction := vpaContinue;
        SystemConfig.Update;
      end;
    paExit:
      begin
        SystemConfig.VitalProgramsAction := vpaExit;
        SystemConfig.Update;
        Application.Terminate;
      end;
  end;
end;

procedure TdlgCheckProcesses.FillProcesses;
var
  ContinueLoop: Boolean;
  SnapshotHandle: THandle;
  ProcessEntry: TProcessEntry32;
  AProcessEntryObject: TProcessEntryAsObject;
  ItemIndex: Integer;
begin
  SnapshotHandle := CreateToolHelp32Snapshot(TH32CS_SNAPPROCESS, 0);
  ProcessEntry.dwSize := SizeOf(ProcessEntry);
  ContinueLoop := Process32First(SnapshotHandle, ProcessEntry);
  while ContinueLoop do
  begin
    if (Uppercase(ExtractFileName(ProcessEntry.szExeFile)) = Uppercase(MapInfoExeName)) or
      (Uppercase(ExtractFileName(ProcessEntry.szExeFile)) = Uppercase(AccessExeName)) then
    begin
      AProcessEntryObject := TProcessEntryAsObject.Create;
      AProcessEntryObject.ProcessEntry := ProcessEntry;
      ItemIndex := chklstProcesses.Items.AddObject(ExtractFileName(ProcessEntry.szExeFile),
        AProcessEntryObject);
      chklstProcesses.ItemChecked[ItemIndex] := True;
    end;
    ContinueLoop := Process32Next(SnapshotHandle, ProcessEntry);
  end;
end;

procedure TdlgCheckProcesses.FormCloseQuery(Sender: TObject;
  var CanClose: Boolean);
begin
  CanClose := ModalResult = mrOK;
end;

procedure TdlgCheckProcesses.FormCreate(Sender: TObject);
begin
  chklstProcesses.Clear;
  FillProcesses;
  rgrpAction.ItemIndex := Integer(SystemConfig.VitalProgramsAction);
  chkRememberShowVitalProgramsWarning.Checked :=
    not SystemConfig.ShowVitalProgramsWarning;
end;

procedure TdlgCheckProcesses.rgrpActionChanging(Sender: TObject;
  NewIndex: Integer; var AllowChange: Boolean);
var
  Action: TProcessAction;
begin
  Action := TProcessAction(NewIndex);
  case Action of
    paCloseProcesses: dlgButtons.CaptionOk := 'Continue';
    paContinue: dlgButtons.CaptionOk := 'Continue';
    paExit: dlgButtons.CaptionOk := 'Exit';
  end;
end;

procedure TdlgCheckProcesses.dlgButtonsClickOk(Sender: TObject);
begin
  Execute;
end;

{ TProcessEntryAsObject }

procedure TProcessEntryAsObject.Kill;
var
  ExitCode: Integer;
begin
  ExitCode := 0;
  TerminateProcess(OpenProcess(PROCESS_TERMINATE, false, ProcessEntry.th32ProcessID),
    ExitCode);
  if ExitCode = 0 then
  begin
    Screen.Cursor := crHourglass;
    Sleep(200);
    Screen.Cursor := crDefault;
  end;
end;

end.
