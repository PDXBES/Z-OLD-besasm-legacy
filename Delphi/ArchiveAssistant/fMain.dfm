object frmMain: TfrmMain
  Left = 522
  Top = 316
  Caption = 'Archive Assistant'
  ClientHeight = 453
  ClientWidth = 632
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object RzSizePanel1: TRzSizePanel
    Left = 0
    Top = 23
    Width = 185
    Height = 430
    HotSpotVisible = True
    RealTimeDrag = True
    SizeBarStyle = ssBump
    SizeBarWidth = 7
    TabOrder = 0
    VisualStyle = vsGradient
    OnResize = RzSizePanel1Resize
    DesignSize = (
      177
      430)
    object treeShell: TRzShellTree
      Left = 4
      Top = 4
      Width = 169
      Height = 390
      Anchors = [akLeft, akTop, akRight, akBottom]
      AutoExpand = True
      BaseFolder.Pidl = {0100}
      FrameController = RzFrameController1
      HideSelection = False
      HotTrack = True
      Indent = 19
      Options = [stoAutoFill, stoVirtualFolders, stoDesignInteractive, stoDefaultKeyHandling, stoContextMenus, stoDynamicRefresh, stoShowHidden]
      RowSelect = True
      TabOrder = 0
    end
    object btnScan: TRzButton
      Left = 4
      Top = 401
      Width = 169
      Anchors = [akLeft, akRight, akBottom]
      Caption = 'Scan'
      HotTrack = True
      TabOrder = 1
      OnClick = btnScanClick
    end
  end
  object RzPanel1: TRzPanel
    Left = 185
    Top = 23
    Width = 447
    Height = 430
    Align = alClient
    BorderOuter = fsNone
    TabOrder = 1
    DesignSize = (
      447
      430)
    object lblDir: TRzLabel
      Left = 8
      Top = 16
      Width = 44
      Height = 13
      Caption = 'Directory'
    end
    object lblSelection: TRzLabel
      Left = 8
      Top = 385
      Width = 3
      Height = 13
      Anchors = [akLeft, akBottom]
      Caption = ' '
    end
    object treeResultsList: TcxTreeList
      Left = 8
      Top = 36
      Width = 433
      Height = 346
      Anchors = [akLeft, akTop, akRight, akBottom]
      Bands = <
        item
        end>
      BufferedPaint = False
      LookAndFeel.Kind = lfFlat
      LookAndFeel.NativeStyle = False
      OptionsSelection.MultiSelect = True
      OptionsView.ColumnAutoWidth = True
      OptionsView.Indicator = True
      OptionsView.ShowRoot = False
      Styles.ContentEven = styleOddRow
      Styles.ContentOdd = styleEvenRow
      TabOrder = 0
      OnColumnHeaderClick = treeResultsListColumnHeaderClick
      OnCompare = treeResultsListCompare
      OnCustomDrawCell = treeResultsListCustomDrawCell
      OnDblClick = treeResultsListDblClick
      OnSelectionChanged = treeResultsListSelectionChanged
      object colDirectory: TcxTreeListColumn
        Caption.Text = 'Directory'
        DataBinding.ValueType = 'String'
        Position.ColIndex = 0
        Position.RowIndex = 0
        Position.BandIndex = 0
      end
      object colLatestUpdate: TcxTreeListColumn
        Caption.Text = 'Latest Update'
        DataBinding.ValueType = 'DateTime'
        Position.ColIndex = 1
        Position.RowIndex = 0
        Position.BandIndex = 0
        SortOrder = soAscending
      end
      object colSize: TcxTreeListColumn
        PropertiesClassName = 'TcxTextEditProperties'
        Properties.Alignment.Horz = taRightJustify
        Caption.Text = 'Size'
        DataBinding.ValueType = 'String'
        Position.ColIndex = 2
        Position.RowIndex = 0
        Position.BandIndex = 0
      end
      object colRawSize: TcxTreeListColumn
        Visible = False
        DataBinding.ValueType = 'String'
        Position.ColIndex = 3
        Position.RowIndex = 0
        Position.BandIndex = 0
      end
    end
    object btnDrillDown: TRzButton
      Left = 8
      Top = 401
      Width = 433
      Anchors = [akLeft, akRight, akBottom]
      Caption = 'Drill Down'
      HotTrack = True
      TabOrder = 1
      OnClick = btnDrillDownClick
    end
    object btnUp: TRzButton
      Left = 412
      Top = 8
      Width = 27
      Anchors = [akTop, akRight]
      Caption = 'Up'
      HotTrack = True
      TabOrder = 2
      OnClick = btnUpClick
    end
  end
  object cxStyleRepository1: TcxStyleRepository
    Left = 100
    Top = 272
    object styleOddRow: TcxStyle
    end
    object styleEvenRow: TcxStyle
      AssignedValues = [svColor]
      Color = clMoneyGreen
    end
  end
  object RzFrameController1: TRzFrameController
    FrameHotTrack = True
    FrameVisible = True
    Left = 100
    Top = 344
  end
  object dxBarManager1: TdxBarManager
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    Bars = <
      item
        Caption = 'Custom 1'
        DockedDockingStyle = dsTop
        DockedLeft = 0
        DockedTop = 0
        DockingStyle = dsTop
        FloatLeft = 472
        FloatTop = 316
        FloatClientWidth = 0
        FloatClientHeight = 0
        IsMainMenu = True
        ItemLinks = <
          item
            Item = dxBarSubItem1
            Visible = True
          end
          item
            Item = mnuEdit
            Visible = True
          end>
        MultiLine = True
        Name = 'Custom 1'
        OneOnRow = True
        Row = 0
        UseOwnFont = False
        Visible = True
        WholeRow = True
      end>
    Categories.Strings = (
      'File'
      'Edit'
      'Default')
    Categories.ItemsVisibles = (
      2
      2
      2)
    Categories.Visibles = (
      True
      True
      True)
    PopupMenuLinks = <>
    UseSystemFont = False
    Left = 100
    Top = 200
    DockControlHeights = (
      0
      0
      23
      0)
    object dxBarSubItem1: TdxBarSubItem
      Caption = '&File'
      Category = 2
      Visible = ivAlways
      ItemLinks = <
        item
          Item = mnuFileExit
          Visible = True
        end>
    end
    object mnuEdit: TdxBarSubItem
      Caption = '&Edit'
      Category = 2
      Visible = ivAlways
      ItemLinks = <
        item
          Item = mnuEditCopyListToClipboard
          Visible = True
        end
        item
          Item = mnuEditCopySelectedToClipboard
          Visible = True
        end>
    end
    object mnuFileExit: TdxBarButton
      Action = actFileExit
      Category = 0
    end
    object mnuEditCopyListToClipboard: TdxBarButton
      Action = actCopyListToClipboard
      Category = 1
    end
    object mnuEditCopySelectedToClipboard: TdxBarButton
      Action = actCopySelectedToClipboard
      Category = 1
    end
  end
  object ActionList1: TActionList
    Left = 100
    Top = 136
    object actCopyListToClipboard: TAction
      Category = 'Edit'
      Caption = 'Copy List to Clipboard'
      OnExecute = actCopyListToClipboardExecute
      OnUpdate = actCopyListToClipboardUpdate
    end
    object actCopySelectedToClipboard: TAction
      Category = 'Edit'
      Caption = 'Copy Selected to Clipboard'
      ShortCut = 16451
      OnExecute = actCopySelectedToClipboardExecute
      OnUpdate = actCopySelectedToClipboardUpdate
    end
    object actFileExit: TFileExit
      Category = 'File'
      Caption = 'E&xit'
      Hint = 'Exit|Quits the application'
      ImageIndex = 43
    end
  end
end
