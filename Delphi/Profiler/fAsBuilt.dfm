object frmAsBuilt: TfrmAsBuilt
  Left = 344
  Top = 270
  Width = 870
  Height = 640
  Caption = 'As-Built'
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object RzSizePanel1: TRzSizePanel
    Left = 0
    Top = 23
    Width = 185
    Height = 440
    SizeBarWidth = 7
    SizeBarStyle = ssBump
    HotSpotVisible = True
    TabOrder = 0
    object RzFileListBox1: TRzFileListBox
      Left = 8
      Top = 216
      Width = 165
      Height = 215
      Anchors = [akLeft, akTop, akRight, akBottom]
      ItemHeight = 18
      TabOrder = 0
      OnClick = RzFileListBox1Click
      FrameFlat = True
      FrameStyle = fsStatus
      FrameVisible = True
    end
    object RzDirectoryListBox1: TRzDirectoryListBox
      Left = 8
      Top = 32
      Width = 165
      Height = 181
      Anchors = [akLeft, akTop, akRight]
      FileList = RzFileListBox1
      ItemHeight = 17
      TabOrder = 1
      FrameFlat = True
      FrameStyle = fsStatus
      FrameVisible = True
    end
    object RzDriveComboBox1: TRzDriveComboBox
      Left = 8
      Top = 8
      Width = 165
      Height = 21
      Anchors = [akLeft, akTop, akRight]
      Ctl3D = False
      DirList = RzDirectoryListBox1
      ParentCtl3D = False
      TabOrder = 2
      FrameFlat = True
      FrameStyle = fsStatus
      FrameVisible = True
    end
  end
  object dxPageControl1: TdxPageControl
    Left = 185
    Top = 23
    Width = 677
    Height = 440
    ActivePage = tabACAD
    Align = alClient
    HideButtons = False
    HotTrack = False
    MultiLine = False
    OwnerDraw = False
    RaggedRight = False
    ScrollOpposite = False
    TabHeight = 0
    TabOrder = 1
    TabPosition = dxtpTop
    TabStop = True
    TabWidth = 0
    object tabACAD: TdxTabSheet
      Caption = 'VoloView'
    end
    object tabRaster: TdxTabSheet
      Caption = 'Raster'
      object ieView: TImageEnView
        Left = 0
        Top = 0
        Width = 677
        Height = 417
        ParentCtl3D = False
        ZoomFilter = rfFastLinear
        MouseInteract = [miZoom, miScroll]
        Align = alClient
        TabOrder = 0
      end
    end
  end
  object dxDBGrid1: TdxDBGrid
    Left = 0
    Top = 463
    Width = 862
    Height = 150
    Bands = <
      item
      end>
    DefaultLayout = True
    HeaderPanelRowCount = 1
    KeyField = 'JOB NUMBER'
    SummaryGroups = <>
    SummarySeparator = ', '
    Align = alBottom
    TabOrder = 6
    OnMouseUp = dxDBGrid1MouseUp
    DataSource = dsJobDetail
    Filter.Criteria = {00000000}
    LookAndFeel = lfUltraFlat
    OptionsView = [edgoAutoWidth, edgoBandHeaderWidth, edgoRowSelect, edgoUseBitmap]
    object dxDBGrid1JOBNUMBER: TdxDBGridColumn
      Width = 141
      BandIndex = 0
      RowIndex = 0
      FieldName = 'JOB NUMBER'
    end
    object dxDBGrid1SGLSHEET: TdxDBGridColumn
      Width = 90
      BandIndex = 0
      RowIndex = 0
      FieldName = 'SGL SHEET #'
    end
    object dxDBGrid1VIEW: TdxDBGridMemoColumn
      Width = 429
      BandIndex = 0
      RowIndex = 0
      FieldName = 'VIEW'
    end
  end
  object dxBarManager1: TdxBarManager
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    Bars = <
      item
        Caption = 'Main Menu'
        DockedDockingStyle = dsTop
        DockedLeft = 0
        DockedTop = 0
        DockingStyle = dsTop
        FloatLeft = 404
        FloatTop = 344
        FloatClientWidth = 23
        FloatClientHeight = 22
        IsMainMenu = True
        ItemLinks = <
          item
            Item = mnuFile
            Visible = True
          end>
        MultiLine = True
        Name = 'Main Menu'
        OneOnRow = True
        Row = 0
        UseOwnFont = False
        Visible = True
        WholeRow = True
      end>
    Categories.Strings = (
      'Default'
      'Built-in Menus'
      'File')
    Categories.ItemsVisibles = (
      2
      2
      2)
    Categories.Visibles = (
      True
      True
      True)
    PopupMenuLinks = <>
    Style = bmsFlat
    UseSystemFont = True
    Left = 780
    Top = 4
    DockControlHeights = (
      0
      0
      23
      0)
    object mnuFile: TdxBarSubItem
      Caption = '&File'
      Category = 1
      Visible = ivAlways
      ItemLinks = <
        item
          Item = mnuFileClose
          Visible = True
        end>
    end
    object mnuFileClose: TdxBarButton
      Caption = '&Close'
      Category = 2
      Hint = 'Close'
      Visible = ivAlways
    end
  end
  object adoSewerJobs: TADOConnection
    ConnectionString = 
      'Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\\Cressida\Job_Sear' +
      'ch\DATA\Access\SEWER JOBS\Sew Job Aug2001.mdb;Persist Security I' +
      'nfo=False'
    LoginPrompt = False
    Provider = 'Microsoft.Jet.OLEDB.4.0'
    Left = 216
    Top = 536
  end
  object adoJobDetail: TADOTable
    Connection = adoSewerJobs
    CursorType = ctStatic
    TableName = 'Job Detail'
    Left = 292
    Top = 536
    object adoJobDetailJOBNUMBER: TWideStringField
      FieldName = 'JOB NUMBER'
      Size = 50
    end
    object adoJobDetailSGLSHEET: TWideStringField
      FieldName = 'SGL SHEET #'
      Size = 50
    end
    object adoJobDetailVIEW: TMemoField
      FieldName = 'VIEW'
      BlobType = ftMemo
    end
  end
  object dsJobDetail: TDataSource
    DataSet = adoJobDetail
    Left = 364
    Top = 536
  end
  object ieInputOutput: TImageEnIO
    AttachedImageEn = ieView
    Background = clBtnFace
    PreviewFont.Charset = DEFAULT_CHARSET
    PreviewFont.Color = clWindowText
    PreviewFont.Height = -11
    PreviewFont.Name = 'MS Sans Serif'
    PreviewFont.Style = []
    Left = 448
    Top = 536
  end
end
