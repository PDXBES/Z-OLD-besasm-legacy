object dlgExtractMOUSEPRFNodes: TdlgExtractMOUSEPRFNodes
  Left = 280
  Top = 105
  Width = 640
  Height = 480
  Caption = 'Extract MOUSE PRF Time Series'
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnShow = FormShow
  DesignSize = (
    632
    453)
  PixelsPerInch = 96
  TextHeight = 13
  object RzLabel1: TRzLabel
    Left = 4
    Top = 12
    Width = 215
    Height = 13
    Caption = 'Select Nodes to Extract from MOUSE PRF file'
  end
  object lblNumSelected: TRzLabel
    Left = 8
    Top = 384
    Width = 72
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = 'lblNumSelected'
  end
  object RzLabel2: TRzLabel
    Left = 260
    Top = 12
    Width = 26
    Height = 13
    Caption = 'View:'
  end
  object prgListUpdate: TRzProgressBar
    Left = 564
    Top = 12
    Width = 60
    Height = 17
    BorderOuter = fsFlat
    BorderWidth = 0
    InteriorOffset = 0
    PartsComplete = 0
    Percent = 0
    TotalParts = 0
    Visible = False
  end
  object RzDialogButtons1: TRzDialogButtons
    Left = 0
    Top = 416
    Width = 632
    Height = 37
    CaptionOk = 'OK'
    CaptionCancel = 'Cancel'
    CaptionHelp = '&Help'
    HotTrack = True
    OnClickOk = RzDialogButtons1ClickOk
    OnClickCancel = RzDialogButtons1ClickCancel
    TabOrder = 0
  end
  object chkWaterLevel: TRzCheckBox
    Left = 288
    Top = 12
    Width = 77
    Height = 17
    Caption = 'Water Level'
    Checked = True
    FrameController = frmMain.RzFrameController1
    State = cbChecked
    TabOrder = 1
    OnClick = chkWaterLevelClick
  end
  object chkDischarge: TRzCheckBox
    Left = 372
    Top = 12
    Width = 65
    Height = 17
    Caption = 'Discharge'
    Checked = True
    FrameController = frmMain.RzFrameController1
    State = cbChecked
    TabOrder = 2
    OnClick = chkWaterLevelClick
  end
  object chkVelocity: TRzCheckBox
    Left = 444
    Top = 12
    Width = 57
    Height = 17
    Caption = 'Velocity'
    Checked = True
    FrameController = frmMain.RzFrameController1
    State = cbChecked
    TabOrder = 3
    OnClick = chkWaterLevelClick
  end
  object chkOther: TRzCheckBox
    Left = 508
    Top = 12
    Width = 53
    Height = 17
    Caption = 'Other'
    Checked = True
    State = cbChecked
    TabOrder = 4
    OnClick = chkWaterLevelClick
  end
  object btnToggleRange: TRzButton
    Left = 516
    Top = 380
    Width = 109
    Anchors = [akRight, akBottom]
    Caption = 'Toggle Range...'
    Enabled = False
    HotTrack = True
    TabOrder = 5
  end
  object btnExcludeAllShown: TRzButton
    Left = 408
    Top = 380
    Width = 105
    Anchors = [akRight, akBottom]
    Caption = 'Exclude All Shown'
    HotTrack = True
    TabOrder = 7
    OnClick = btnExcludeAllShownClick
  end
  object btnIncludeAllShown: TRzBitBtn
    Left = 300
    Top = 380
    Width = 105
    Anchors = [akRight, akBottom]
    Caption = 'Include All Shown'
    HotTrack = True
    TabOrder = 6
    OnClick = btnIncludeAllShownClick
  end
  object treeNodes: TcxTreeList
    Left = 4
    Top = 32
    Width = 621
    Height = 345
    Anchors = [akLeft, akTop, akRight, akBottom]
    Bands = <
      item
      end>
    BufferedPaint = False
    OptionsView.ColumnAutoWidth = True
    OptionsView.ShowRoot = False
    TabOrder = 8
    OnCompare = treeNodesCompare
    OnCustomDrawCell = treeNodesCustomDrawCell
    OnEdited = treeNodesEdited
    OnEditing = treeNodesEditing
    OnFocusedColumnChanged = treeNodesFocusedColumnChanged
    object cxColRowID: TcxTreeListColumn
      PropertiesClassName = 'TcxSpinEditProperties'
      Caption.Text = 'Record'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 0
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColID: TcxTreeListColumn
      Caption.Text = 'ID'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 1
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColSelected: TcxTreeListColumn
      PropertiesClassName = 'TcxCheckBoxProperties'
      Properties.OnEditValueChanged = cxColSelectedPropertiesEditValueChanged
      Caption.Text = 'Selected'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 2
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColNodeType: TcxTreeListColumn
      Visible = False
      Caption.Text = 'Type'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 3
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColNodeTypeDescription: TcxTreeListColumn
      Caption.Text = 'Description'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 3
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColUpNodeID: TcxTreeListColumn
      Caption.Text = 'Up Node ID'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 4
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColDnNodeID: TcxTreeListColumn
      Caption.Text = 'Dn Node ID'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 5
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColPosition: TcxTreeListColumn
      Caption.Text = 'Position'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 6
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColPositionUnits: TcxTreeListColumn
      Caption.Text = 'Units'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 7
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
  end
end
