unit fLabeledChild;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, fChild, StdCtrls, pngimage, RzBckgnd, ExtCtrls, RzPanel, RzLabel;

type
  TfrmLabeledChild = class(TfrmChild)
    RzPanel4: TRzPanel;
    RzBackground1: TRzBackground;
    lblLabel: TRzLabel;
  private
    { Private declarations }
  public
    { Public declarations }
  end;
  TfrmLabeledChildClass = class of TfrmLabeledChild;

var
  frmLabeledChild: TfrmLabeledChild;

implementation

{$R *.dfm}

end.
