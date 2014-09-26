object frmDocker: TfrmDocker
  Left = 289
  Top = 200
  Width = 464
  Height = 506
  Caption = 'frmDocker'
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object RzPanel1: TRzPanel
    Left = 0
    Top = 0
    Width = 456
    Height = 16
    Align = alTop
    BorderOuter = fsNone
    Color = clBtnShadow
    TabOrder = 0
    object lblDockerTitle: TRzLabel
      Left = 16
      Top = 0
      Width = 424
      Height = 16
      Align = alClient
      Caption = 'Docker'
      Font.Charset = ANSI_CHARSET
      Font.Color = clWhite
      Font.Height = -11
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
      Layout = tlCenter
      BorderColor = clBtnShadow
      BorderWidth = 1
      FrameSides = []
    end
    object RzPanel2: TRzPanel
      Left = 440
      Top = 0
      Width = 16
      Height = 16
      Align = alRight
      BorderOuter = fsNone
      TabOrder = 0
      object RzToolbarButton1: TRzToolbarButton
        Left = 0
        Top = 0
        Width = 16
        Height = 16
        Glyph.Data = {
          F6000000424DF600000000000000760000002800000010000000100000000100
          0400000000008000000000000000000000001000000000000000000000000000
          8000008000000080800080000000800080008080000080808000C0C0C0000000
          FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00333333333333
          3333333333333333333333333033333333333333300333333333333330003333
          3333333330000333333333333000003333333333300000033333333330000000
          3333333330000003333333333000003333333333300003333333333330003333
          3333333330033333333333333033333333333333333333333333}
        HotNumGlyphs = 0
      end
    end
    object RzPanel3: TRzPanel
      Left = 0
      Top = 0
      Width = 16
      Height = 16
      Align = alLeft
      BorderOuter = fsNone
      Color = clBtnShadow
      TabOrder = 1
      object Image1: TImage
        Left = 0
        Top = 0
        Width = 16
        Height = 16
        Align = alClient
        Picture.Data = {
          07544269746D6170F6000000424DF60000000000000076000000280000001000
          0000100000000100040000000000800000000000000000000000100000000000
          0000000000000000800000800000008080008000000080008000808000008080
          8000C0C0C0000000FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFF
          FF0033333333333333333333330033333333333333F033333333333333333333
          3333333333330033333333333333F03333333333333333333333333333003333
          3333333333F0333333333333333333333333333333330033333333333333F033
          333333333333333333333333330033333333333333F033333333333333333333
          3333}
        Transparent = True
      end
    end
  end
  object dxBarPopupMenu1: TdxBarPopupMenu
    BarManager = dxBarManager1
    ItemLinks = <>
    UseOwnFont = False
    Left = 200
    Top = 264
  end
  object dxBarManager1: TdxBarManager
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    Bars = <>
    Categories.Strings = (
      'Default')
    Categories.ItemsVisibles = (
      2)
    Categories.Visibles = (
      True)
    PopupMenuLinks = <>
    UseSystemFont = True
    Left = 200
    Top = 312
    DockControlHeights = (
      0
      0
      0
      0)
  end
end
