object Form1: TForm1
  Left = 158
  Top = 248
  Width = 696
  Height = 480
  Caption = 'Form1'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object Button1: TButton
    Left = 104
    Top = 96
    Width = 137
    Height = 57
    Caption = 'Basic Excel Create'
    TabOrder = 0
    OnClick = Action1Execute
  end
  object Button2: TButton
    Left = 104
    Top = 200
    Width = 137
    Height = 49
    Caption = 'Recon SS Work'
    TabOrder = 1
    OnClick = Button2Click
  end
  object ActionList1: TActionList
    Left = 376
    Top = 160
    object Action1: TAction
      Caption = 'Action1'
      OnExecute = Action1Execute
    end
  end
end
