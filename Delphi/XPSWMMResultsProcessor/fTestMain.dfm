object Form1: TForm1
  Left = 270
  Top = 205
  Width = 1088
  Height = 750
  Caption = 'Form1'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object RzLabel1: TRzLabel
    Left = 108
    Top = 16
    Width = 9
    Height = 13
    Caption = 'In'
  end
  object RzLabel2: TRzLabel
    Left = 108
    Top = 60
    Width = 17
    Height = 13
    Caption = 'Out'
  end
  object RzLabel3: TRzLabel
    Left = 96
    Top = 164
    Width = 45
    Height = 13
    Caption = 'RzLabel3'
  end
  object RzButton1: TRzButton
    Left = 176
    Top = 116
    Width = 213
    Caption = 'Process'
    TabOrder = 0
    OnClick = RzButton1Click
  end
  object edtIn: TRzButtonEdit
    Left = 136
    Top = 16
    Width = 321
    Height = 21
    TabOrder = 1
    ButtonKind = bkFolder
    OnButtonClick = edtInButtonClick
  end
  object edtOut: TRzButtonEdit
    Left = 136
    Top = 56
    Width = 321
    Height = 21
    TabOrder = 2
    ButtonKind = bkFolder
    OnButtonClick = edtOutButtonClick
  end
  object dlgOpen: TOpenDialog
    Left = 476
    Top = 40
  end
  object CSGlobalObject1: TCSGlobalObject
    CategoryColor = clWindow
    CategoryFontColor = clWindowText
    Left = 640
    Top = 560
  end
end
