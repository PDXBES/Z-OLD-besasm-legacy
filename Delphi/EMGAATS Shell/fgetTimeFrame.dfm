object frmTimeFrame: TfrmTimeFrame
  Left = 387
  Top = 400
  BorderIcons = []
  Caption = 'Choose Model Time Frame'
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
  object rdgTimeFrame: TRadioGroup
    Left = 8
    Top = 12
    Width = 121
    Height = 61
    Caption = 'Time Frame Choices'
    ItemIndex = 0
    Items.Strings = (
      'Existing Condition'
      'Future Base')
    TabOrder = 0
  end
  object RzDialogButtons1: TRzDialogButtons
    Left = 0
    Top = 111
    Width = 357
    CaptionOk = 'OK'
    CaptionCancel = 'Cancel'
    CaptionHelp = '&Help'
    HotTrack = True
    ModalResultOk = 1
    ModalResultCancel = 2
    ModalResultHelp = 0
    ShowDivider = True
    OnClickOk = btnOKClick
    OnClickCancel = btnCancelClick
    TabOrder = 1
  end
end
