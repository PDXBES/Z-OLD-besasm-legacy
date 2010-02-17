unit uEMGAATSModelTemplateConfig;

interface

uses SysUtils, Classes, IniFiles, uConfigFiles, uEMGAATSTypes, Windows;

type
  TModelTemplateConfig = class
  private
    fConfig: IConfigFile;
  public
    constructor Create(AFileName: TFileName);
    destructor Destroy; override;
    procedure CopyTemplateTo(AConfig: IConfigFile);
  end;

var
  ModelTemplateConfig: TModelTemplateConfig;

implementation

{ TModelTemplateConfig }

procedure TModelTemplateConfig.CopyTemplateTo(AConfig: IConfigFile);
var
  i: Integer;
  SectionNames: TStringList;
begin
  SectionNames := TStringList.Create;
  fConfig.ReadSections(SectionNames);
  try
    for i := 0 to SectionNames.Count - 1 do
      fConfig.CopySectionToConfig(SectionNames[i], AConfig);
  finally
    SectionNames.Free;
  end;

end;

constructor TModelTemplateConfig.Create(AFileName: TFileName);
begin
  fConfig := TIniConfigFile.Create(AFileName);
end;

destructor TModelTemplateConfig.Destroy;
begin
  inherited;
end;

end.
