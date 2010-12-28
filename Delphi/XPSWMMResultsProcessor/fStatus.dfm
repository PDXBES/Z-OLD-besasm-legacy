object frmStatus: TfrmStatus
  Left = 715
  Top = 331
  Width = 417
  Height = 109
  Caption = 'XPResults Status'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poDesktopCenter
  PixelsPerInch = 96
  TextHeight = 13
  object lblInFile: TRzLabel
    Left = 4
    Top = 4
    Width = 397
    Height = 13
    AutoSize = False
    Caption = 'lblInFile'
    FrameSides = []
  end
  object lblOutFile: TRzLabel
    Left = 4
    Top = 20
    Width = 397
    Height = 13
    AutoSize = False
    Caption = 'lblOutFile'
    FrameSides = []
  end
  object prgFile: TRzProgressBar
    Left = 8
    Top = 44
    Width = 393
    BorderWidth = 0
    InteriorOffset = 0
    PartsComplete = 0
    Percent = 0
    TotalParts = 100
  end
end
