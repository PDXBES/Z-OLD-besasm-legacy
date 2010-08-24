object dlgToggleRange: TdlgToggleRange
  Left = 280
  Top = 334
  BorderStyle = bsDialog
  Caption = 'Toggle Range'
  ClientHeight = 106
  ClientWidth = 181
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnCloseQuery = FormCloseQuery
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object RzLabel1: TRzLabel
    Left = 12
    Top = 24
    Width = 57
    Height = 13
    Caption = 'Lower Node'
  end
  object RzLabel2: TRzLabel
    Left = 12
    Top = 48
    Width = 57
    Height = 13
    Caption = 'Upper Node'
  end
  object lblNumNodes: TRzLabel
    Left = 12
    Top = 4
    Width = 72
    Height = 13
    Caption = 'lblNumNodes'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object btnOK: TRzButton
    Left = 12
    Top = 72
    Default = True
    ModalResult = 1
    Caption = 'OK'
    HotTrack = True
    TabOrder = 0
  end
  object btnCancel: TRzButton
    Left = 92
    Top = 72
    Cancel = True
    ModalResult = 2
    Caption = 'Cancel'
    HotTrack = True
    TabOrder = 1
  end
  object edtLower: TRzSpinEdit
    Left = 120
    Top = 20
    Width = 47
    Height = 21
    TabOrder = 2
  end
  object edtUpper: TRzSpinEdit
    Left = 120
    Top = 44
    Width = 47
    Height = 21
    TabOrder = 3
  end
end
