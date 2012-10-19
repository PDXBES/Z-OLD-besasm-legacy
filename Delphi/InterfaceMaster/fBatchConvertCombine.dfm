inherited frmBatchConvertCombine: TfrmBatchConvertCombine
  Height = 511
  Caption = 'frmBatchConvertCombine'
  OnCreate = FormCreate
  OnDestroy = FormDestroy
  PixelsPerInch = 96
  TextHeight = 13
  inherited RzLabel1: TRzLabel
    Caption = ' Batch Convert/Combine'
  end
  object RzLabel2: TRzLabel
    Left = 8
    Top = 44
    Width = 46
    Height = 13
    Caption = 'Batch File'
  end
  object RzLabel3: TRzLabel
    Left = 8
    Top = 92
    Width = 52
    Height = 13
    Caption = 'Commands'
  end
  object RzLabel4: TRzLabel
    Left = 8
    Top = 343
    Width = 82
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = 'Command Details'
  end
  object edtBatchFile: TRzButtonEdit
    Left = 8
    Top = 60
    Width = 757
    Height = 21
    Anchors = [akLeft, akTop, akRight]
    FrameController = frmMain.RzFrameController1
    TabOrder = 0
    ButtonKind = bkFolder
    OnButtonClick = edtBatchFileButtonClick
  end
  object RzPanel1: TRzPanel
    Left = 0
    Top = 443
    Width = 774
    Height = 41
    Align = alBottom
    BorderOuter = fsNone
    TabOrder = 1
    DesignSize = (
      774
      41)
    object btnRun: TRzButton
      Left = 692
      Top = 8
      Anchors = [akTop, akRight]
      Caption = 'Run'
      HotTrack = True
      TabOrder = 0
      OnClick = btnRunClick
    end
    object btnCloseTask: TRzButton
      Left = 8
      Top = 8
      Caption = 'Close Task'
      HotTrack = True
      TabOrder = 1
      OnClick = btnCloseTaskClick
    end
    object RzButton3: TRzButton
      Left = 612
      Top = 8
      Anchors = [akTop, akRight]
      Caption = 'Save'
      HotTrack = True
      TabOrder = 2
    end
  end
  object treeCommands: TcxTreeList
    Left = 8
    Top = 108
    Width = 757
    Height = 200
    Anchors = [akLeft, akTop, akRight, akBottom]
    Bands = <
      item
      end>
    BufferedPaint = False
    OptionsView.ColumnAutoWidth = True
    OptionsView.GridLines = tlglBoth
    OptionsView.Indicator = True
    OptionsView.ShowRoot = False
    TabOrder = 2
    object cxColCommandType: TcxTreeListColumn
      Caption.Text = 'Command Type'
      DataBinding.ValueType = 'String'
      Width = 143
      Position.ColIndex = 2
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColDescription: TcxTreeListColumn
      Caption.Text = 'Description'
      DataBinding.ValueType = 'String'
      Width = 295
      Position.ColIndex = 3
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColRunOrder: TcxTreeListColumn
      Caption.Text = 'Run Order'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 0
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColInclude: TcxTreeListColumn
      PropertiesClassName = 'TcxCheckBoxProperties'
      Properties.ImmediatePost = True
      Properties.OnChange = cxColIncludePropertiesChange
      Caption.Text = 'Include'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 1
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColProgress: TcxTreeListColumn
      PropertiesClassName = 'TcxProgressBarProperties'
      Caption.Text = 'Progress'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 4
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
  end
  object RzMemo1: TRzMemo
    Left = 8
    Top = 359
    Width = 757
    Height = 73
    Anchors = [akLeft, akRight, akBottom]
    TabOrder = 3
    FrameController = frmMain.RzFrameController1
  end
  object btnAddConversion: TRzButton
    Left = 8
    Top = 312
    Width = 101
    Anchors = [akLeft, akBottom]
    Caption = 'Add Conversion'
    HotTrack = True
    TabOrder = 4
  end
  object btnAddMulticombine: TRzButton
    Left = 112
    Top = 312
    Width = 101
    Anchors = [akLeft, akBottom]
    Caption = 'Add Multicombine'
    HotTrack = True
    TabOrder = 5
  end
  object RzMenuButton1: TRzMenuButton
    Left = 216
    Top = 312
    Anchors = [akLeft, akBottom]
    Caption = 'Add Command'
    HotTrack = True
    TabOrder = 6
    DropDownMenu = PopupMenu1
  end
  object PopupMenu1: TPopupMenu
    Left = 360
    Top = 312
    object AddConversion1: TMenuItem
      Caption = 'Add Conversion'
    end
    object AddMultiCombine1: TMenuItem
      Caption = 'Add MultiCombine'
    end
    object AddWWStats1: TMenuItem
      Caption = 'Add WWStats'
    end
  end
end
