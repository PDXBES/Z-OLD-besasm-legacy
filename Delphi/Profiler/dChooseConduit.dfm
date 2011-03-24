object dlgChooseConduit: TdlgChooseConduit
  Left = 306
  Top = 746
  BorderStyle = bsDialog
  Caption = 'Choose Conduit'
  ClientHeight = 123
  ClientWidth = 416
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnActivate = FormActivate
  PixelsPerInch = 96
  TextHeight = 13
  object RzLabel1: TRzLabel
    Left = 8
    Top = 16
    Width = 84
    Height = 13
    Caption = 'Choose a conduit'
    FrameSides = []
  end
  object edtChooseConduit: TdxPickEdit
    Left = 8
    Top = 32
    Width = 401
    TabOrder = 0
    OnClick = edtChooseConduitChange
    StyleController = frmMain.dxStyle
    Text = 'edtChooseConduit'
    OnChange = edtChooseConduitChange
    ImmediateDropDown = False
    DropDownListStyle = True
  end
  object RzDialogButtons1: TRzDialogButtons
    Left = 0
    Top = 87
    Width = 416
    CaptionOk = 'OK'
    CaptionCancel = 'Cancel'
    CaptionHelp = '&Help'
    ShowCancelButton = False
    TabOrder = 1
  end
  object btnHighlightSelected: TRzButton
    Left = 172
    Top = 56
    Width = 159
    FrameFlat = True
    Caption = 'Highlight Selected on Map'
    TabOrder = 2
    OnClick = btnHighlightSelectedClick
  end
  object btnHighlightAll: TRzButton
    Left = 8
    Top = 56
    Width = 159
    FrameFlat = True
    Caption = 'Highlight All on Map'
    TabOrder = 3
    OnClick = btnHighlightAllClick
  end
  object btnZoomIn: TRzBitBtn
    Left = 348
    Top = 56
    Width = 29
    TabOrder = 4
    OnClick = btnZoomInClick
    Glyph.Data = {
      F6000000424DF600000000000000760000002800000010000000100000000100
      0400000000008000000000000000000000001000000000000000000000000000
      80000080000000808000800000008000800080800000C0C0C000808080000000
      FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00777777777777
      7777777777777777707777777777777700077777777777700077777777777700
      077777770000700077777700FFFF00077777770FF00FF077777770FFF00FFF07
      7777708000000F07777770F000000F077777708FF00FFF0777777708F00FF077
      777777008F8F0077777777770000777777777777777777777777}
  end
  object btnZoomOut: TRzBitBtn
    Left = 380
    Top = 56
    Width = 29
    TabOrder = 5
    OnClick = btnZoomOutClick
    Glyph.Data = {
      F6000000424DF600000000000000760000002800000010000000100000000100
      0400000000008000000000000000000000001000000000000000000000000000
      80000080000000808000800000008000800080800000C0C0C000808080000000
      FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00777777777777
      7777777777777777707777777777777700077777777777700077777777777700
      077777770000700077777700FFFF00077777770FFFFFF077777770FFFFFFFF07
      7777708000000F07777770F000000F077777708FFFFFFF0777777708FFFFF077
      777777008F8F0077777777770000777777777777777777777777}
  end
end
