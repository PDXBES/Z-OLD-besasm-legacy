object frmStatus: TfrmStatus
  Left = 498
  Top = 309
  BorderIcons = []
  Caption = 'Status'
  ClientHeight = 140
  ClientWidth = 357
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  FormStyle = fsStayOnTop
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object lbl1: TLabel
    Left = 46
    Top = 16
    Width = 267
    Height = 25
    AutoSize = False
  end
  object Lbl2: TLabel
    Left = 46
    Top = 56
    Width = 267
    Height = 25
    AutoSize = False
  end
  object btnCancel: TButton
    Left = 144
    Top = 96
    Width = 65
    Height = 33
    Caption = 'Cancel'
    ModalResult = 2
    TabOrder = 0
    OnClick = btnCancelClick
  end
end
