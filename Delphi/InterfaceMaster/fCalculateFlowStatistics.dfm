inherited frmCalculateFlowStatistics: TfrmCalculateFlowStatistics
  Left = 315
  Top = 286
  ActiveControl = edtSaveFile
  Caption = 'frmCalculateFlowStatistics'
  ClientHeight = 564
  OldCreateOrder = True
  ExplicitHeight = 591
  PixelsPerInch = 96
  TextHeight = 13
  object lblStep1: TRzLabel [0]
    Left = 4
    Top = 44
    Width = 239
    Height = 13
    Caption = '1. Open File to Calculate Flow Statistics For'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object lblSelectNodes: TRzLabel [1]
    Left = 4
    Top = 88
    Width = 161
    Height = 13
    Caption = '2. Review Nodes to Calculate'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object RzLabel3: TRzLabel [2]
    Left = 4
    Top = 284
    Width = 100
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = '3. Specify Options'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object RzLabel16: TRzLabel [3]
    Left = 4
    Top = 463
    Width = 79
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = '5. Process File'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object lblFileProgress: TRzLabel [4]
    Left = 4
    Top = 511
    Width = 61
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = 'File Progress'
  end
  object prgFile: TRzProgressBar [5]
    Left = 152
    Top = 508
    Width = 621
    Height = 20
    Anchors = [akLeft, akRight, akBottom]
    BackColor = clBtnFace
    BorderInner = fsFlat
    BorderOuter = fsNone
    BorderWidth = 1
    InteriorOffset = 0
    PartsComplete = 0
    Percent = 0
    TotalParts = 100
  end
  object RzLabel6: TRzLabel [6]
    Left = 4
    Top = 420
    Width = 140
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = '4. Save Converted File to'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
  end
  inherited pnlTitleHolder: TRzPanel
    TabOrder = 10
    inherited RzLabel1: TRzLabel
      Caption = ' Calculate Flow Statistics'
    end
  end
  object btnProcessStatistics: TRzButton
    Left = 4
    Top = 479
    Width = 185
    Anchors = [akLeft, akBottom]
    Caption = 'Process Statistics'
    HotTrack = True
    TabOrder = 0
    OnClick = btnProcessStatisticsClick
  end
  object btnStop: TRzButton
    Left = 192
    Top = 479
    Width = 149
    Anchors = [akLeft, akBottom]
    Caption = 'Stop'
    HotTrack = True
    TabOrder = 1
  end
  object edtOpenFile: TRzButtonEdit
    Left = 4
    Top = 60
    Width = 765
    Height = 21
    Anchors = [akLeft, akTop, akRight]
    FrameController = frmMain.RzFrameController1
    TabOrder = 2
    OnChange = edtOpenFileChange
    ButtonKind = bkFolder
    OnButtonClick = edtOpenFileButtonClick
  end
  object pnlOptions: TRzPanel
    Left = 4
    Top = 300
    Width = 765
    Height = 113
    Anchors = [akLeft, akRight, akBottom]
    BorderOuter = fsFlat
    TabOrder = 3
    Visible = False
    object RzLabel2: TRzLabel
      Left = 264
      Top = 12
      Width = 120
      Height = 13
      Caption = 'Minimum Interevent Time'
    end
    object RzLabel4: TRzLabel
      Left = 516
      Top = 12
      Width = 16
      Height = 13
      Caption = 'Hrs'
    end
    object RzLabel5: TRzLabel
      Left = 12
      Top = 8
      Width = 70
      Height = 13
      Caption = 'Winter Season'
    end
    object chklstWinterSeason: TRzCheckList
      Left = 88
      Top = 8
      Width = 165
      Height = 97
      Items.Strings = (
        'January'
        'February'
        'March'
        'April'
        'May'
        'June'
        'July'
        'August'
        'September'
        'October'
        'November'
        'December')
      Items.ItemEnabled = (
        True
        True
        True
        True
        True
        True
        True
        True
        True
        True
        True
        True)
      Items.ItemState = (
        1
        1
        1
        1
        0
        0
        0
        0
        0
        0
        1
        1)
      Columns = 2
      FrameHotTrack = True
      FrameHotStyle = fsLowered
      FrameStyle = fsStatus
      FrameVisible = True
      ItemHeight = 15
      TabOrder = 0
    end
    object chkDebugLog: TRzCheckBox
      Left = 264
      Top = 32
      Width = 115
      Height = 17
      Caption = 'Debug Log'
      FrameController = frmMain.RzFrameController1
      HotTrack = True
      HotTrackColor = clBtnShadow
      HotTrackStyle = htsFrame
      State = cbUnchecked
      TabOrder = 1
    end
    object chkEventLog: TRzCheckBox
      Left = 264
      Top = 48
      Width = 133
      Height = 17
      Caption = 'Event Log (per node)'
      FrameController = frmMain.RzFrameController1
      HotTrack = True
      HotTrackColor = clBtnShadow
      HotTrackStyle = htsFrame
      State = cbUnchecked
      TabOrder = 2
    end
    object edtMinIntereventTime: TRzSpinEdit
      Left = 392
      Top = 8
      Width = 117
      Height = 21
      AllowKeyEdit = True
      IntegersOnly = False
      Value = 24.000000000000000000
      FrameController = frmMain.RzFrameController1
      TabOrder = 3
    end
  end
  object edtSaveFile: TRzButtonEdit
    Left = 4
    Top = 436
    Width = 765
    Height = 21
    Anchors = [akLeft, akRight, akBottom]
    FrameController = frmMain.RzFrameController1
    TabOrder = 4
    ButtonKind = bkFolder
    OnButtonClick = edtSaveFileButtonClick
  end
  object btnCloseTask: TRzButton
    Left = 4
    Top = 532
    Width = 169
    Anchors = [akLeft, akBottom]
    Caption = 'Close Task'
    HotTrack = True
    TabOrder = 5
    OnClick = btnCloseTaskClick
  end
  object RzButton1: TRzButton
    Left = 4
    Top = 248
    Width = 101
    Anchors = [akLeft, akBottom]
    Caption = 'Exclude All'
    HotTrack = True
    TabOrder = 6
    OnClick = RzButton1Click
  end
  object RzButton2: TRzButton
    Left = 108
    Top = 248
    Width = 101
    Anchors = [akLeft, akBottom]
    Caption = 'Include All'
    HotTrack = True
    TabOrder = 8
    OnClick = RzButton2Click
  end
  object RzButton3: TRzButton
    Left = 212
    Top = 248
    Width = 101
    Anchors = [akLeft, akBottom]
    Caption = 'Toggle Selection'
    HotTrack = True
    TabOrder = 7
    OnClick = RzButton3Click
  end
  object treeNodes: TcxTreeList
    Left = 4
    Top = 104
    Width = 761
    Height = 141
    Anchors = [akLeft, akTop, akRight, akBottom]
    Bands = <
      item
      end>
    BufferedPaint = False
    Constraints.MinHeight = 80
    LookAndFeel.Kind = lfUltraFlat
    OptionsView.ColumnAutoWidth = True
    OptionsView.GridLines = tlglBoth
    OptionsView.Indicator = True
    OptionsView.ShowRoot = False
    TabOrder = 9
    object cxColRecord: TcxTreeListColumn
      Caption.Text = 'Record'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 0
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColInclude: TcxTreeListColumn
      PropertiesClassName = 'TcxCheckBoxProperties'
      Caption.Text = 'Include?'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 1
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColOriginalID: TcxTreeListColumn
      Caption.Text = 'Original ID'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 2
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColNewID: TcxTreeListColumn
      Caption.Text = 'New Time Series ID'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 3
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColMinFlow: TcxTreeListColumn
      PropertiesClassName = 'TcxSpinEditProperties'
      Caption.Text = 'Minimum Flow'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 4
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
  end
end
