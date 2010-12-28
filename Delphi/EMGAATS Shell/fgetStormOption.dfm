object frmStormOption: TfrmStormOption
  Left = 387
  Top = 400
  AutoScroll = False
  BorderIcons = []
  Caption = 'Enter Runoff Storm Option'
  ClientHeight = 147
  ClientWidth = 357
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object btnCancel: TButton
    Left = 264
    Top = 104
    Width = 65
    Height = 33
    Caption = 'Cancel'
    ModalResult = 2
    TabOrder = 1
    OnClick = btnCancelClick
  end
  object btnOK: TButton
    Left = 184
    Top = 104
    Width = 65
    Height = 33
    Caption = 'OK'
    TabOrder = 0
    OnClick = btnOKClick
  end
  object rdgStormOption: TRadioGroup
    Left = 24
    Top = 16
    Width = 113
    Height = 73
    Caption = 'Choose Calibration or Design'
    ItemIndex = 0
    Items.Strings = (
      'Calibration'
      'Design')
    TabOrder = 2
  end
end
