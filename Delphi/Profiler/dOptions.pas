unit dOptions;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, RzButton, StdCtrls, RzLabel, dxCntner, dxEditor, dxExEdtr,
  dxEdLib, RzDlgBtn, ExtCtrls, RzPanel;

type
  TdlgOptions = class(TForm)
    RzGroupBox1: TRzGroupBox;
    RzDialogButtons1: TRzDialogButtons;
    dxButtonEdit1: TdxButtonEdit;
    RzLabel1: TRzLabel;
    RzButton1: TRzButton;
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  dlgOptions: TdlgOptions;

implementation

uses fMain;

{$R *.dfm}

end.
