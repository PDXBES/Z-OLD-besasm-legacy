unit dmXPExport;

interface

uses
  SysUtils, Classes, DB, ADODB, ActiveX;

type
  TdmodXPExport = class(TDataModule)
    adoOutConnection: TADOConnection;
    adoOutCommand: TADOCommand;
    adoTableE09: TADOTable;
    adoTableE09NodeName: TWideStringField;
    adoTableE09GrElev: TFloatField;
    adoTableE09MaxCrown: TFloatField;
    adoTableE09MaxJElev: TFloatField;
    adoTableE09TimeOfMax: TDateTimeField;
    adoTableE09Surcharge: TFloatField;
    adoTableE09Freeboard: TFloatField;
    adoTableE09MaxArea: TFloatField;
    adoTableE10: TADOTable;
    adoTableE10CondName: TWideStringField;
    adoTableE10DesignQ: TFloatField;
    adoTableE10DesignV: TFloatField;
    adoTableE10MaxD: TFloatField;
    adoTableE10MaxQ: TFloatField;
    adoTableE10TimeMaxQ: TDateTimeField;
    adoTableE10MaxV: TFloatField;
    adoTableE10TimeMaxV: TDateTimeField;
    adoTableE10QqRatio: TFloatField;
    adoTableE10MaxUsElev: TFloatField;
    adoTableE10MaxDsElev: TFloatField;
    adoTableE20: TADOTable;
    adoTableE20NodeName: TWideStringField;
    adoTableE20SurchargeTime: TFloatField;
    adoTableE20FloodedTime: TFloatField;
    adoTableE20FloodVol: TFloatField;
    adoTableE20MaxStoredVol: TFloatField;
    adoTableE20PondingVol: TFloatField;
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  dmodXPExport: TdmodXPExport;

implementation

{$R *.dfm}

initialization
	CoInitialize(nil);

finalization
	CoUninitialize;

end.
