object dlgModelExists: TdlgModelExists
  Left = 0
  Top = 0
  Caption = 'A model in this directory exists'
  ClientHeight = 306
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
  object RzGridPanel1: TRzGridPanel
    Left = 0
    Top = 0
    Width = 467
    Height = 270
    BorderOuter = fsNone
    Align = alClient
    AutoSize = True
    ColumnCollection = <
      item
        Value = 11.032028469750890000
      end
      item
        Value = 88.967971530249120000
      end>
    ControlCollection = <
      item
        Column = 1
        Control = rgrpAction
        Row = 2
      end
      item
        Column = 1
        Control = lblModelPath
        Row = 1
      end
      item
        Column = 0
        ColumnSpan = 2
        Control = RzPanel1
        Row = 0
      end
      item
        Column = 1
        Control = chkRememberModelBoundaries
        Row = 3
      end>
    Padding.Left = 5
    Padding.Top = 5
    Padding.Right = 5
    Padding.Bottom = 5
    RowCollection = <
      item
        SizeStyle = ssAuto
        Value = 50.000000000000000000
      end
      item
        SizeStyle = ssAuto
        Value = 100.000000000000000000
      end
      item
        SizeStyle = ssAuto
      end
      item
        SizeStyle = ssAuto
        Value = 100.000000000000000000
      end>
    TabOrder = 0
    object rgrpAction: TRzRadioGroup
      AlignWithMargins = True
      Left = 63
      Top = 130
      Width = 391
      Height = 97
      Margins.Top = 10
      Align = alClient
      Caption = 'Select an action and click Continue'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -11
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      FrameController = frmMain.RzFrameController1
      GroupStyle = gsBanner
      ItemHotTrack = True
      ItemFont.Charset = DEFAULT_CHARSET
      ItemFont.Color = clWindowText
      ItemFont.Height = -11
      ItemFont.Name = 'Tahoma'
      ItemFont.Style = []
      ItemHeight = 20
      ItemIndex = 1
      Items.Strings = (
        'Erase the directory contents before building the model'
        'Keep the directory contents and build over existing content'
        'Cancel build')
      ParentFont = False
      StartYPos = 10
      TabOrder = 0
      VerticalSpacing = 0
      OnChanging = rgrpActionChanging
    end
    object lblModelPath: TRzLabel
      Left = 60
      Top = 70
      Width = 397
      Height = 40
      AutoSize = False
      Caption = 'Path'
      WordWrap = True
    end
    object RzPanel1: TRzPanel
      AlignWithMargins = True
      Left = 13
      Top = 13
      Width = 441
      Height = 44
      Align = alClient
      AutoSize = True
      BorderOuter = fsNone
      TabOrder = 1
      object Image1: TImage
        Left = 0
        Top = 0
        Width = 41
        Height = 44
        Align = alLeft
        ExplicitTop = 2
      end
      object RzLabel1: TRzLabel
        AlignWithMargins = True
        Left = 44
        Top = 3
        Width = 345
        Height = 36
        Align = alClient
        Caption = 
          'A model in this directory already exists. Clear directory conten' +
          'ts when build starts?'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clHotLight
        Font.Height = -15
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
        Layout = tlCenter
        WordWrap = True
        LightTextStyle = True
        TextStyle = tsRaised
      end
    end
    object chkRememberModelBoundaries: TRzCheckBox
      Left = 60
      Top = 240
      Width = 397
      Height = 17
      Align = alClient
      Caption = 'Remember model boundaries'
      Checked = True
      HotTrack = True
      State = cbChecked
      TabOrder = 2
    end
  end
  object dlgButtons: TRzDialogButtons
    Left = 0
    Top = 270
    Width = 467
    CaptionOk = 'Continue'
    CaptionCancel = 'Cancel'
    CaptionHelp = '&Help'
    HotTrack = True
    ModalResultOk = 1
    ModalResultCancel = 2
    ModalResultHelp = 0
    ShowDivider = True
    ShowCancelButton = False
    WidthOk = 150
    ParentShowHint = False
    ShowHint = False
    TabOrder = 1
  end
end
