object dlgOptions: TdlgOptions
  Left = 474
  Top = 557
  Width = 553
  Height = 363
  Caption = 'Options'
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  DesignSize = (
    545
    336)
  PixelsPerInch = 96
  TextHeight = 13
  object RzGroupBox1: TRzGroupBox
    Left = 12
    Top = 12
    Width = 521
    Height = 93
    Caption = 'Job Records'
    GroupStyle = gsTopLine
    TabOrder = 0
    Anchors = [akLeft, akTop, akRight]
    DesignSize = (
      521
      93)
    object RzLabel1: TRzLabel
      Left = 16
      Top = 20
      Width = 41
      Height = 13
      Caption = 'Location'
      FrameSides = []
    end
    object dxButtonEdit1: TdxButtonEdit
      Left = 12
      Top = 36
      Width = 497
      TabOrder = 0
      Anchors = [akLeft, akTop, akRight]
      StyleController = frmMain.dxStyle
      Text = 'dxButtonEdit1'
      Buttons = <
        item
          Default = True
        end>
      ExistButtons = True
    end
    object RzButton1: TRzButton
      Left = 380
      Top = 60
      Width = 127
      FrameFlat = True
      Caption = 'Define Structure'
      TabOrder = 1
    end
  end
  object RzDialogButtons1: TRzDialogButtons
    Left = 0
    Top = 300
    Width = 545
    CaptionOk = 'OK'
    CaptionCancel = 'Cancel'
    CaptionHelp = '&Help'
    FrameFlat = True
    TabOrder = 1
  end
end
