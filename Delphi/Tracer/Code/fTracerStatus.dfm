object frmTracerStatus: TfrmTracerStatus
  Left = 308
  Top = 236
  BorderIcons = [biSystemMenu]
  BorderStyle = bsDialog
  Caption = 'Tracer Status'
  ClientHeight = 141
  ClientWidth = 357
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Verdana'
  Font.Style = []
  OldCreateOrder = False
  OnClose = FormClose
  PixelsPerInch = 96
  TextHeight = 13
  object prgStatus: TRzProgressBar
    Left = 4
    Top = 104
    Width = 349
    BackColor = clBtnFace
    BorderInner = fsFlat
    BorderOuter = fsNone
    BorderWidth = 1
    InteriorOffset = 0
    PartsComplete = 0
    Percent = 0
    TotalParts = 0
  end
  object statustext1: TRzLabel
    Left = 8
    Top = 16
    Width = 345
    Height = 33
    AutoSize = False
    WordWrap = True
  end
  object statustext2: TRzLabel
    Left = 8
    Top = 56
    Width = 345
    Height = 13
    AutoSize = False
  end
  object statustext3: TRzLabel
    Left = 8
    Top = 76
    Width = 345
    Height = 13
    AutoSize = False
  end
end
