object dlgOptions: TdlgOptions
  Left = 0
  Top = 0
  ActiveControl = edtExtentsMap
  Caption = 'EMGAATS Options'
  ClientHeight = 300
  ClientWidth = 467
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poMainFormCenter
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object pgcOptions: TRzPageControl
    Left = 0
    Top = 0
    Width = 467
    Height = 264
    ActivePage = TabSheet2
    Align = alClient
    TabIndex = 1
    TabOrder = 0
    FixedDimension = 19
    object TabSheet1: TRzTabSheet
      Caption = 'General'
      object RzLabel1: TRzLabel
        Left = 8
        Top = 51
        Width = 168
        Height = 13
        Caption = 'MS Access command wait time (ms)'
        FocusControl = edtMSAccessWaitMilliseconds
      end
      object chkShowSplashScreen: TRzCheckBox
        Left = 8
        Top = 11
        Width = 449
        Height = 17
        Caption = 'Show splash screen on startup'
        HotTrack = True
        State = cbUnchecked
        TabOrder = 0
      end
      object chkShowVitalProgramsWarning: TRzCheckBox
        Left = 8
        Top = 28
        Width = 449
        Height = 17
        Caption = 'Show vital programs warning on startup'
        HotTrack = True
        State = cbUnchecked
        TabOrder = 1
      end
      object edtMSAccessWaitMilliseconds: TRzSpinEdit
        Left = 182
        Top = 48
        Width = 69
        Height = 21
        AllowKeyEdit = True
        CheckRange = True
        Increment = 100.000000000000000000
        Max = 10000.000000000000000000
        Min = 10.000000000000000000
        Value = 10.000000000000000000
        FrameController = frmMain.RzFrameController1
        TabOrder = 2
      end
    end
    object TabSheet2: TRzTabSheet
      Caption = 'File Locations'
      ExplicitLeft = 2
      ExplicitTop = 17
      object RzLabel2: TRzLabel
        Left = 8
        Top = 12
        Width = 60
        Height = 13
        Caption = 'Extents map'
        FocusControl = edtExtentsMap
      end
      object RzLabel3: TRzLabel
        Left = 8
        Top = 66
        Width = 68
        Height = 13
        Caption = 'Runoff engine'
      end
      object RzLabel4: TRzLabel
        Left = 8
        Top = 39
        Width = 104
        Height = 13
        Caption = 'Basin boundaries map'
        FocusControl = edtBasinBoundaries
      end
      object edtExtentsMap: TRzButtonEdit
        Left = 74
        Top = 9
        Width = 383
        Height = 21
        FrameController = frmMain.RzFrameController1
        TabOrder = 0
        ButtonKind = bkFolder
        OnButtonClick = edtExtentsMapButtonClick
      end
      object edtRunoffEngine: TRzButtonEdit
        Left = 82
        Top = 63
        Width = 375
        Height = 21
        FrameController = frmMain.RzFrameController1
        TabOrder = 1
        ButtonKind = bkFolder
        OnButtonClick = edtRunoffEngineButtonClick
      end
      object edtBasinBoundaries: TRzButtonEdit
        Left = 118
        Top = 36
        Width = 339
        Height = 21
        FrameController = frmMain.RzFrameController1
        TabOrder = 2
        ButtonKind = bkFolder
        OnButtonClick = edtBasinBoundariesButtonClick
      end
    end
  end
  object dlgButtons: TRzDialogButtons
    Left = 0
    Top = 264
    Width = 467
    CaptionOk = 'OK'
    CaptionCancel = 'Cancel'
    CaptionHelp = '&Help'
    HotTrack = True
    ModalResultOk = 1
    ModalResultCancel = 2
    ModalResultHelp = 0
    OnClickOk = dlgButtonsClickOk
    TabOrder = 1
  end
  object dlgOpen: TRzOpenDialog
    Left = 80
    Top = 240
  end
end
