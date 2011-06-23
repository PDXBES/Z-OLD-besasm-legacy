unit dbMapinfo;

interface

uses
  SysUtils, Classes;

type
  objMICB = class
  private
    mystatustext : string;
  public
    property setStatusText : string write mystatustext;
    property getStatusText : string read mystatustext;
  end;


  TDataModule1 = class(TDataModule)
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  DataModule1: TDataModule1;

implementation

{$R *.dfm}

end.
