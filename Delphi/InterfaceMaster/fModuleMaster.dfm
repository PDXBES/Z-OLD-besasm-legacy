object frmModuleMaster: TfrmModuleMaster
  Left = 33
  Top = 51
  Caption = 'frmModuleMaster'
  ClientHeight = 485
  ClientWidth = 766
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnClose = FormClose
  PixelsPerInch = 96
  TextHeight = 13
  object pnlTitleHolder: TRzPanel
    Left = 0
    Top = 0
    Width = 766
    Height = 41
    Align = alTop
    BorderOuter = fsNone
    TabOrder = 0
    DesignSize = (
      766
      41)
    object RzLabel1: TRzLabel
      Left = 8
      Top = 4
      Width = 755
      Height = 37
      Anchors = [akLeft, akTop, akRight]
      AutoSize = False
      Caption = ' Title'
      Color = clBtnShadow
      Font.Charset = ANSI_CHARSET
      Font.Color = clWindowText
      Font.Height = -24
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentColor = False
      ParentFont = False
      Layout = tlCenter
      TextStyle = tsRaised
    end
  end
end
