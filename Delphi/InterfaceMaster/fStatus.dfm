object frmStatus: TfrmStatus
  Left = 362
  Top = 319
  Width = 390
  Height = 216
  Caption = 'Status'
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  FormStyle = fsStayOnTop
  OldCreateOrder = False
  Position = poScreenCenter
  PixelsPerInch = 96
  TextHeight = 13
  object RzLabel1: TRzLabel
    Left = 8
    Top = 8
    Width = 81
    Height = 13
    Caption = 'Status Messages'
  end
  object prgStatus: TRzProgressBar
    Left = 4
    Top = 160
    Width = 373
    BackColor = clWindow
    BorderOuter = fsFlat
    BorderWidth = 0
    FlatColorAdjustment = 0
    FrameController = frmMain.RzFrameController1
    InteriorOffset = 0
    PartsComplete = 0
    Percent = 0
    TotalParts = 0
  end
  object edtStatus: TRzRichEdit
    Left = 4
    Top = 24
    Width = 373
    Height = 133
    ScrollBars = ssBoth
    TabOrder = 0
    FrameController = frmMain.RzFrameController1
  end
end
