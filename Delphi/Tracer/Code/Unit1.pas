unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls,inifiles;


type
  TForm1 = class(TForm)
    Button1: TButton;
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;



var
  Form1: TForm1;

  function  Treeverse(strINImdl :Pchar): Integer ; stdcall ;
        external 'tracer.dll';


implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);

var

  strINI : string;
  treeverserc : integer;


  label myend;




begin

  {strini := '\\oberon\modeling\projects\6675tfnb\models\fb03_1a\trace\Ebaltrace.ini';}
  strini := 'tracertester.ini';
  treeverserc := Treeverse(pchar(strINI));

  showmessage(inttostr(treeverserc));

myend:


end;

end.
