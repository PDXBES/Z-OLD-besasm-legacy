unit dmIM_FlowDistributionSupport;

interface

uses
  SysUtils, Classes, DB, ADODB, dxmdaset;

type
  TdmFlowDistributionSupport = class(TDataModule)
    adoConnLinks: TADOConnection;
    adoLinks: TADOTable;
    adoCommand: TADOCommand;
    adoProportion: TADOQuery;
    adoConnNodes: TADOConnection;
    adoNodes: TADOTable;
    adoLinkAreas: TADOTable;
    adoDistributionNodes: TADOTable;
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  dmFlowDistributionSupport: TdmFlowDistributionSupport;

implementation

{$R *.dfm}

end.
