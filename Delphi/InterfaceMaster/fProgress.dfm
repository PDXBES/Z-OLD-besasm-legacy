object frmProgress: TfrmProgress
  Left = 463
  Top = 253
  BorderStyle = bsNone
  Caption = 'frmProgress'
  ClientHeight = 109
  ClientWidth = 356
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poMainFormCenter
  PixelsPerInch = 96
  TextHeight = 13
  object RzPanel1: TRzPanel
    Left = 0
    Top = 0
    Width = 356
    Height = 109
    Align = alClient
    TabOrder = 0
    object prgProgress: TRzProgressBar
      Left = 8
      Top = 44
      Width = 337
      BorderOuter = fsFlat
      BorderWidth = 0
      InteriorOffset = 0
      PartsComplete = 0
      Percent = 0
      TotalParts = 0
    end
    object lblProgress: TRzLabel
      Left = 8
      Top = 8
      Width = 337
      Height = 33
      Alignment = taCenter
      AutoSize = False
      Caption = 'Status'
      Layout = tlBottom
      WordWrap = True
    end
  end
end
