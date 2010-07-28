unit uUtilities;

interface

uses SysUtils;

function CopyFile(SourceFile, DestFile: TFileName): boolean;
function CopyFileExtensionGroup(Extensions, SourceDir, DestDir: String): Boolean;
function CopyDir(const SourceDir, DestDir: String): Boolean;
function GetApplicationPath: String;
function GetApplicationName: String;
function GetApplicationUserConfigPath: String;
function GetApplicationUserConfigFileName: TFileName;
function Ellipsize(Text: String; MaxLength: Integer): String;
function DelTree(Path: String): Boolean;
function IsValidFileName(AFileName: TFileName): Boolean;

implementation

uses Windows, StStrL, GlobalConstants, SHFolder, StrUtils, ShellAPI, Classes,
  Forms;

const
  IllegalFileNameChars = '<>:"/\|*?';
  ReservedDeviceNames: array[0..22] of String = ('CON', 'PRN', 'AUX', 'NUL',
    'COM1', 'COM2', 'COM3', 'COM4', 'COM5', 'COM6', 'COM7', 'COM8', 'COM9',
    'LPT1', 'LPT2', 'LPT3', 'LPT4', 'LPT5', 'LPT6', 'LPT7', 'LPT8', 'LPT9',
    'CLOCK$');

function CopyFile(SourceFile, DestFile: TFileName): boolean;
begin
  Result := Windows.CopyFile(pChar(SourceFile), pChar(DestFile), false);
end;

function CopyFileExtensionGroup(Extensions, SourceDir, DestDir: String): Boolean;
var
  ShellFileOp: TSHFileOpStruct;
  List: TStringList;
  i: Integer;
begin
  List := TStringList.Create;
  try
    try
      List.Delimiter := ';';
      List.DelimitedText := Extensions;
      for i := 0 to List.Count - 1 do
      begin
        ShellFileOp.Wnd := Application.Handle;
        ShellFileOp.wFunc := FO_COPY;
        ShellFileOp.pFrom := PChar(IncludeTrailingPathDelimiter(SourceDir) +
          List[i] + #0#0);
        ShellFileOp.pTo := PChar(IncludeTrailingPathDelimiter(DestDir));
        ShellFileOp.fFlags := FOF_NOCONFIRMATION or FOF_SIMPLEPROGRESS;
        ShellFileOp.fAnyOperationsAborted := True;
        ShellFileOp.lpszProgressTitle := PChar('Copying files with extension ' +
          List[i]);
        ShFileOperation(ShellFileOp);
      end;
      Result := True;
    except
      Result := False;
    end;
  finally
    List.Free;
  end;
end;

function CopyDir(const SourceDir, DestDir: String): Boolean;
var
  fos: TSHFileOpStruct;
begin
  ZeroMemory(@fos, SizeOf(fos));
  with fos do
  begin
    wFunc := FO_COPY;
    fFlags := FOF_FILESONLY or FOF_NOCONFIRMATION or FOF_SIMPLEPROGRESS;
    pFrom := PChar(SourceDir + #0);
    pTo := PChar(DestDir);
  end;
  Result := (ShFileOperation(fos) = 0);
end;

function GetApplicationPath: String;
begin
  Result := ExtractFilePath(ParamStr(0));
end;

function GetApplicationName: String;
begin
  Result := ExtractFileName(ParamStr(0));
end;

function GetApplicationUserConfigPath: String;
var
  Path: String;
begin
  Path := PadL(Path, MaxPathLength);
  SHGetFolderPath(0, CSIDL_APPDATA, 0, SHGFP_TYPE_CURRENT, PChar(Path));
  Result := IncludeTrailingPathDelimiter(Trim(Path) + UserConfigLocation);
end;

function GetApplicationUserConfigFileName: TFileName;
begin
  Result := IncludeTrailingPathDelimiter(GetApplicationUserConfigPath) + UserConfigFileName;
end;

function Ellipsize(Text: String; MaxLength: Integer): String;
begin
  if Length(Text) > MaxLength then
    Result := '...' + RightStr(Text, MaxLength-3)
  else
    Result := Text;
end;

function DelTree(Path: String): Boolean;
var
  SHFileOpStruct : TSHFileOpStruct;
  DirBuf : array [0..255] of char;
begin
  try
   Fillchar(SHFileOpStruct,Sizeof(SHFileOpStruct),0) ;
   FillChar(DirBuf, Sizeof(DirBuf), 0 ) ;
   StrPCopy(DirBuf, Path) ;
   with SHFileOpStruct do begin
    Wnd := 0;
    pFrom := @DirBuf;
    wFunc := FO_DELETE;
    fFlags := FOF_ALLOWUNDO;
    fFlags := fFlags or FOF_NOCONFIRMATION;
    fFlags := fFlags or FOF_SILENT;
   end;
    Result := (SHFileOperation(SHFileOpStruct) = 0) ;
   except
    Result := False;
  end;
end;

function IsValidFileName(AFileName: TFileName): Boolean;
var
  FileName: String;
  FileNameWithoutExtension: String;
  i: Integer;
begin
  Result := True;
  FileName := ExtractFileName(AFileName);
  FileNameWithoutExtension := JustNameL(AFileName);

  // Check for illegal characters
  for i := 1 to Length(IllegalFileNameChars) do
    if StrScan(PChar(FileName), IllegalFileNameChars[i]) <> nil then
    begin
      Result := False;
      Exit
    end;

  // Check for device name usage
  for i := Low(ReservedDeviceNames) to High(ReservedDeviceNames) do
    if Uppercase(FileNameWithoutExtension) = ReservedDeviceNames[i] then
    begin
      Result := False;
      Exit;
    end;
end;

end.
