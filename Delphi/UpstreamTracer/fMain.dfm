object frmMain: TfrmMain
  Left = 230
  Top = 137
  Width = 417
  Height = 462
  Caption = 'UpstreamTracer'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  OnDestroy = FormDestroy
  DesignSize = (
    409
    435)
  PixelsPerInch = 96
  TextHeight = 13
  object RzLabel1: TRzLabel
    Left = 4
    Top = 60
    Width = 129
    Height = 13
    Caption = 'Grab all  nodes upstream of'
  end
  object RzLabel2: TRzLabel
    Left = 4
    Top = 180
    Width = 76
    Height = 13
    Caption = 'Upstream Links:'
  end
  object RzLabel3: TRzLabel
    Left = 140
    Top = 40
    Width = 62
    Height = 13
    Caption = 'Use MLinkID'
  end
  object RzLabel4: TRzLabel
    Left = 8
    Top = 8
    Width = 90
    Height = 13
    Caption = 'Database Location'
  end
  object memUpStreamNodes: TRzMemo
    Left = 4
    Top = 196
    Width = 398
    Height = 233
    Anchors = [akLeft, akTop, akRight, akBottom]
    ScrollBars = ssBoth
    TabOrder = 2
    FrameController = RzFrameController1
  end
  object RzButton1: TRzButton
    Left = 4
    Top = 152
    Width = 201
    Default = True
    Caption = 'Go'
    HotTrack = True
    TabOrder = 1
    OnClick = RzButton1Click
  end
  object edtMLinkID: TRzNumericEdit
    Left = 140
    Top = 56
    Width = 65
    Height = 21
    FrameController = RzFrameController1
    TabOrder = 0
    DisplayFormat = '0'
  end
  object rgrpTimeFrame: TRzRadioGroup
    Left = 4
    Top = 80
    Width = 201
    Height = 65
    Caption = 'TimeFrame'
    ItemIndex = 0
    Items.Strings = (
      'Existing'
      'Future')
    TabOrder = 3
  end
  object edtDBLocation: TRzButtonEdit
    Left = 104
    Top = 4
    Width = 301
    Height = 21
    Text = '\\cassio\modeling\AGmaster21\portal\masterportal.mdb'
    Anchors = [akLeft, akTop, akRight]
    FrameController = RzFrameController1
    TabOrder = 4
    ButtonKind = bkFolder
    OnButtonClick = edtDBLocationButtonClick
  end
  object RzFrameController1: TRzFrameController
    FrameHotTrack = True
    FrameVisible = True
    Left = 64
    Top = 168
  end
  object dlgOpen: TRzOpenDialog
    Left = 160
    Top = 168
  end
end
