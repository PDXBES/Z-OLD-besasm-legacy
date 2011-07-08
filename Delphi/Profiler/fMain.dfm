object frmMain: TfrmMain
  Left = 180
  Top = 130
  Width = 1069
  Height = 823
  Caption = 'Profiler'
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  KeyPreview = True
  OldCreateOrder = False
  Position = poDesktopCenter
  OnCloseQuery = FormCloseQuery
  OnCreate = FormCreate
  OnDestroy = FormDestroy
  OnKeyUp = DrawBoxKeyUp
  OnResize = FormResize
  PixelsPerInch = 96
  TextHeight = 13
  object lblDatabase: TRzLabel
    Left = 0
    Top = 63
    Width = 1061
    Height = 38
    Align = alTop
    AutoSize = False
    Caption = ' No Active Database'
    Color = clBtnShadow
    Font.Charset = ANSI_CHARSET
    Font.Color = clWhite
    Font.Height = -21
    Font.Name = 'Tahoma'
    Font.Style = []
    ParentColor = False
    ParentFont = False
    Layout = tlCenter
  end
  object dxBarDockControl1: TdxBarDockControl
    Left = 0
    Top = 0
    Width = 1061
    Height = 63
    Align = dalTop
    BarManager = dxBarManager1
  end
  object RzPanel1: TRzPanel
    Left = 0
    Top = 101
    Width = 1061
    Height = 671
    Align = alClient
    BorderOuter = fsPopup
    TabOrder = 4
    object pnlProperties: TRzSizePanel
      Left = 1
      Top = 1
      Width = 207
      Height = 669
      BorderOuter = fsStatus
      Caption = 'Select or hover over an object in the profile to view properties'
      HotSpotSizePercent = 33
      HotSpotVisible = True
      RealTimeDrag = True
      SizeBarStyle = ssGroove
      SizeBarWidth = 7
      TabOrder = 0
      UseGradients = True
      OnHotSpotClick = pnlPropertiesHotSpotClick
      object pnlPropertiesHolder: TRzPanel
        Left = 1
        Top = 1
        Width = 197
        Height = 667
        Align = alClient
        BorderOuter = fsNone
        Caption = 'Select or hover over an object in the profile to view properties'
        TabOrder = 0
        object RzLabel1: TRzLabel
          Left = 0
          Top = 0
          Width = 197
          Height = 17
          Align = alTop
          AutoSize = False
          Caption = ' Profile Properties'
          Color = clBtnShadow
          Font.Charset = ANSI_CHARSET
          Font.Color = clWhite
          Font.Height = -11
          Font.Name = 'Tahoma'
          Font.Style = []
          ParentColor = False
          ParentFont = False
          Layout = tlCenter
        end
        object pnlPropertiesData: TRzSizePanel
          Left = 0
          Top = 17
          Width = 197
          Height = 650
          Align = alClient
          TabOrder = 0
          UseGradients = True
          Visible = False
          object ScrollBox1: TScrollBox
            Left = 0
            Top = 0
            Width = 197
            Height = 650
            VertScrollBar.Tracking = True
            Align = alClient
            BorderStyle = bsNone
            TabOrder = 0
            DesignSize = (
              197
              650)
            object RzLabel2: TRzLabel
              Left = 8
              Top = 80
              Width = 45
              Height = 13
              Caption = 'CompKey'
            end
            object RzLabel3: TRzLabel
              Left = 8
              Top = 200
              Width = 51
              Height = 13
              Caption = 'Up Ground'
            end
            object RzLabel4: TRzLabel
              Left = 8
              Top = 224
              Width = 65
              Height = 13
              Caption = 'Down Ground'
            end
            object RzLabel5: TRzLabel
              Left = 8
              Top = 248
              Width = 46
              Height = 13
              Caption = 'Up Invert'
            end
            object RzLabel6: TRzLabel
              Left = 8
              Top = 272
              Width = 60
              Height = 13
              Caption = 'Down Invert'
            end
            object RzLabel7: TRzLabel
              Left = 8
              Top = 296
              Width = 45
              Height = 13
              Caption = 'Up Depth'
            end
            object RzLabel8: TRzLabel
              Left = 8
              Top = 320
              Width = 59
              Height = 13
              Caption = 'Down Depth'
            end
            object RzLabel9: TRzLabel
              Left = 8
              Top = 104
              Width = 41
              Height = 13
              Caption = 'Up Node'
            end
            object RzLabel10: TRzLabel
              Left = 8
              Top = 152
              Width = 55
              Height = 13
              Caption = 'Down Node'
            end
            object RzLabel11: TRzLabel
              Left = 8
              Top = 344
              Width = 33
              Height = 13
              Caption = 'Length'
            end
            object RzLabel12: TRzLabel
              Left = 8
              Top = 368
              Width = 55
              Height = 13
              Caption = 'Vertical Dim'
            end
            object RzLabel15: TRzLabel
              Left = 8
              Top = 464
              Width = 47
              Height = 13
              Caption = 'As-built #'
            end
            object RzLabel16: TRzLabel
              Left = 8
              Top = 32
              Width = 45
              Height = 13
              Caption = 'Link Type'
            end
            object RzLabel17: TRzLabel
              Left = 8
              Top = 128
              Width = 40
              Height = 13
              Caption = 'Up Type'
            end
            object RzLabel18: TRzLabel
              Left = 8
              Top = 176
              Width = 54
              Height = 13
              Caption = 'Down Type'
            end
            object RzLabel20: TRzLabel
              Left = 8
              Top = 416
              Width = 30
              Height = 13
              Caption = 'Shape'
            end
            object RzLabel21: TRzLabel
              Left = 8
              Top = 392
              Width = 81
              Height = 13
              Caption = 'Horiz Dim  (Diam)'
            end
            object RzLabel22: TRzLabel
              Left = 8
              Top = 440
              Width = 38
              Height = 13
              Caption = 'Material'
            end
            object RzLabel24: TRzLabel
              Left = 182
              Top = 200
              Width = 8
              Height = 13
              Anchors = [akTop, akRight]
              Caption = 'ft'
            end
            object RzLabel25: TRzLabel
              Left = 182
              Top = 224
              Width = 8
              Height = 13
              Anchors = [akTop, akRight]
              Caption = 'ft'
            end
            object RzLabel26: TRzLabel
              Left = 182
              Top = 248
              Width = 8
              Height = 13
              Anchors = [akTop, akRight]
              Caption = 'ft'
            end
            object RzLabel27: TRzLabel
              Left = 182
              Top = 272
              Width = 8
              Height = 13
              Anchors = [akTop, akRight]
              Caption = 'ft'
            end
            object RzLabel28: TRzLabel
              Left = 182
              Top = 296
              Width = 8
              Height = 13
              Anchors = [akTop, akRight]
              Caption = 'ft'
            end
            object RzLabel29: TRzLabel
              Left = 182
              Top = 320
              Width = 8
              Height = 13
              Anchors = [akTop, akRight]
              Caption = 'ft'
            end
            object RzLabel30: TRzLabel
              Left = 182
              Top = 368
              Width = 8
              Height = 13
              Anchors = [akTop, akRight]
              Caption = 'in'
            end
            object RzLabel31: TRzLabel
              Left = 182
              Top = 392
              Width = 8
              Height = 13
              Anchors = [akTop, akRight]
              Caption = 'in'
            end
            object RzLabel32: TRzLabel
              Left = 8
              Top = 8
              Width = 29
              Height = 13
              Caption = 'LinkID'
            end
            object RzLabel23: TRzLabel
              Left = 182
              Top = 176
              Width = 8
              Height = 13
              Anchors = [akTop, akRight]
              Caption = 'ft'
            end
            object RzBorder1: TRzBorder
              Left = 8
              Top = 504
              Width = 185
              Height = 6
              BorderSides = [sdBottom]
              Anchors = [akLeft, akTop, akRight]
            end
            object RzLabel33: TRzLabel
              Left = 8
              Top = 516
              Width = 59
              Height = 13
              Caption = 'Data Source'
            end
            object RzLabel34: TRzLabel
              Left = 8
              Top = 56
              Width = 49
              Height = 13
              Caption = 'Flow Type'
            end
            object RzLabel36: TRzLabel
              Left = 182
              Top = 344
              Width = 8
              Height = 13
              Anchors = [akTop, akRight]
              Caption = 'ft'
            end
            object edtUpGround: TdxSpinEdit
              Left = 76
              Top = 196
              Width = 99
              TabOrder = 4
              Anchors = [akLeft, akTop, akRight]
              StyleController = dxStyle
              OnChange = edtUpGroundChange
              ValueType = vtFloat
              StoredValues = 8
            end
            object edtDnGround: TdxSpinEdit
              Left = 76
              Top = 220
              Width = 99
              TabOrder = 5
              Anchors = [akLeft, akTop, akRight]
              StyleController = dxStyle
              OnChange = edtUpGroundChange
              ValueType = vtFloat
              StoredValues = 8
            end
            object edtUpInvert: TdxSpinEdit
              Left = 76
              Top = 244
              Width = 99
              TabOrder = 6
              Anchors = [akLeft, akTop, akRight]
              StyleController = dxStyle
              OnChange = edtUpGroundChange
              ValueType = vtFloat
              StoredValues = 8
            end
            object edtDnInvert: TdxSpinEdit
              Left = 76
              Top = 268
              Width = 99
              TabOrder = 7
              Anchors = [akLeft, akTop, akRight]
              StyleController = dxStyle
              OnChange = edtUpGroundChange
              ValueType = vtFloat
              StoredValues = 8
            end
            object edtCompKey: TdxSpinEdit
              Left = 76
              Top = 76
              Width = 115
              TabOrder = 1
              Anchors = [akLeft, akTop, akRight]
              ReadOnly = False
              StyleController = dxStyle
              OnChange = edtUpGroundChange
              EditorEnabled = False
              StoredValues = 64
            end
            object edtUpDepth: TdxSpinEdit
              Left = 76
              Top = 292
              Width = 99
              Color = clBtnFace
              TabOrder = 15
              TabStop = False
              Anchors = [akLeft, akTop, akRight]
              ReadOnly = True
              StyleController = dxStyle
              EditorEnabled = False
              ValueType = vtFloat
              StoredValues = 72
            end
            object edtDnDepth: TdxSpinEdit
              Left = 76
              Top = 316
              Width = 99
              Color = clBtnFace
              TabOrder = 16
              TabStop = False
              Anchors = [akLeft, akTop, akRight]
              ReadOnly = True
              StyleController = dxStyle
              EditorEnabled = False
              ValueType = vtFloat
              StoredValues = 72
            end
            object edtUpNode: TdxEdit
              Left = 76
              Top = 100
              Width = 115
              Color = clBtnFace
              TabOrder = 17
              TabStop = False
              Text = 'edtUpNode'
              Anchors = [akLeft, akTop, akRight]
              ReadOnly = True
              StyleController = dxStyle
              StoredValues = 64
            end
            object edtDnNode: TdxEdit
              Left = 76
              Top = 148
              Width = 115
              Color = clBtnFace
              TabOrder = 18
              TabStop = False
              Text = 'edtDnNode'
              Anchors = [akLeft, akTop, akRight]
              ReadOnly = True
              StyleController = dxStyle
              StoredValues = 64
            end
            object edtLength: TdxSpinEdit
              Left = 76
              Top = 340
              Width = 99
              TabOrder = 8
              Anchors = [akLeft, akTop, akRight]
              ReadOnly = False
              StyleController = dxStyle
              OnChange = edtUpGroundChange
              ValueType = vtFloat
              StoredValues = 72
            end
            object edtVertDim: TdxSpinEdit
              Left = 76
              Top = 364
              Width = 99
              TabOrder = 9
              Anchors = [akLeft, akTop, akRight]
              ReadOnly = False
              StyleController = dxStyle
              OnChange = edtUpGroundChange
              ValueType = vtFloat
              StoredValues = 72
            end
            object edtLinkType: TdxEdit
              Left = 76
              Top = 28
              Width = 115
              Color = clBtnFace
              TabOrder = 19
              TabStop = False
              Text = 'edtLinkType'
              Anchors = [akLeft, akTop, akRight]
              ReadOnly = True
              StyleController = dxStyle
              StoredValues = 64
            end
            object edtUpType: TdxEdit
              Left = 76
              Top = 124
              Width = 115
              TabOrder = 2
              Text = 'edtUpType'
              Anchors = [akLeft, akTop, akRight]
              ReadOnly = False
              StyleController = dxStyle
              OnChange = edtUpGroundChange
              StoredValues = 64
            end
            object edtDnType: TdxEdit
              Left = 76
              Top = 172
              Width = 115
              TabOrder = 3
              Text = 'edtDnType'
              Anchors = [akLeft, akTop, akRight]
              ReadOnly = False
              StyleController = dxStyle
              OnChange = edtUpGroundChange
              StoredValues = 64
            end
            object edtHorizDim: TdxSpinEdit
              Left = 96
              Top = 388
              Width = 79
              TabOrder = 10
              Anchors = [akLeft, akTop, akRight]
              StyleController = dxStyle
              OnChange = edtUpGroundChange
            end
            object edtShape: TdxEdit
              Left = 76
              Top = 412
              Width = 93
              TabOrder = 11
              Text = 'edtShape'
              Anchors = [akLeft, akTop, akRight]
              MaxLength = 4
              StyleController = dxStyle
              OnChange = edtUpGroundChange
              StoredValues = 2
            end
            object edtPipetype: TdxEdit
              Left = 76
              Top = 436
              Width = 93
              TabOrder = 12
              Text = 'edtPipeType'
              Anchors = [akLeft, akTop, akRight]
              MaxLength = 6
              StyleController = dxStyle
              OnChange = edtUpGroundChange
              StoredValues = 2
            end
            object edtLinkID: TdxSpinEdit
              Left = 76
              Top = 4
              Width = 115
              Color = clBtnFace
              TabOrder = 20
              TabStop = False
              Anchors = [akLeft, akTop, akRight]
              ReadOnly = True
              StyleController = dxStyle
              EditorEnabled = False
              StoredValues = 64
            end
            object btnDataSource: TRzMenuButton
              Left = 8
              Top = 532
              Width = 185
              Anchors = [akLeft, akTop, akRight]
              Caption = '????.????'
              TabOrder = 21
              OnClick = btnDataSourceClick
            end
            object edtFlowType: TdxEdit
              Left = 76
              Top = 52
              Width = 115
              TabOrder = 0
              Text = 'edtFlowType'
              OnEnter = edtFlowTypeEnter
              Anchors = [akLeft, akTop, akRight]
              ReadOnly = False
              StyleController = dxStyle
              OnChange = edtUpGroundChange
              StoredValues = 64
            end
            object chkSpecLink: TdxCheckEdit
              Left = 8
              Top = 484
              Width = 161
              TabOrder = 14
              Anchors = [akLeft, akTop, akRight]
              Caption = 'Special Link'
              ReadOnly = True
              StoredValues = 64
            end
            object edtAsBuilt: TdxButtonEdit
              Left = 76
              Top = 460
              Width = 117
              TabOrder = 13
              Anchors = [akLeft, akTop, akRight]
              StyleController = dxStyle
              Text = 'edtAsBuilt'
              OnChange = edtUpGroundChange
              Buttons = <
                item
                  Default = True
                  Glyph.Data = {
                    D6000000424DD60000000000000076000000280000000C0000000C0000000100
                    0400000000006000000000000000000000001000000000000000000000000000
                    8000008000000080800080000000800080008080000080808000C0C0C0000000
                    FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00333333333333
                    0000333333333333000033333333333300003333030000330000333F88888F03
                    000030F88CCC880300003300CC0FC03300003333000003330000330033333333
                    0000333300000003000033333333333300003333333333330000}
                  Kind = bkGlyph
                end>
              OnButtonClick = edtAsBuiltButtonClick
              ExistButtons = True
            end
          end
        end
      end
    end
    object RzPanel4: TRzPanel
      Left = 237
      Top = 1
      Width = 823
      Height = 669
      Align = alClient
      BorderOuter = fsNone
      TabOrder = 1
      object pnlMap: TRzSizePanel
        Left = 0
        Top = 0
        Width = 823
        Height = 237
        Align = alTop
        BorderOuter = fsStatus
        HotSpotSizePercent = 33
        HotSpotVisible = True
        RealTimeDrag = True
        SizeBarStyle = ssGroove
        SizeBarWidth = 7
        TabOrder = 0
        UseGradients = True
        OnHotSpotClick = pnlMapHotSpotClick
        OnResize = pnlMapResize
        object lblPlanView: TRzLabel
          Left = 1
          Top = 1
          Width = 821
          Height = 24
          Align = alTop
          AutoSize = False
          Caption = ' Plan View'
          Color = clBtnShadow
          Font.Charset = ANSI_CHARSET
          Font.Color = clWhite
          Font.Height = -16
          Font.Name = 'Tahoma'
          Font.Style = [fsBold]
          ParentColor = False
          ParentFont = False
          Layout = tlCenter
        end
        object RzLabel35: TRzLabel
          Left = 1
          Top = 215
          Width = 821
          Height = 13
          Align = alBottom
          Caption = 
            ' Hold down Ctrl to select links to trace through, from upstream ' +
            'to downstream'
          Font.Charset = ANSI_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'Tahoma'
          Font.Style = []
          ParentColor = False
          ParentFont = False
          Layout = tlCenter
        end
        object mapNetwork: TMap
          Left = 92
          Top = 25
          Width = 730
          Height = 190
          ParentColor = False
          TabStop = False
          Align = alClient
          TabOrder = 0
          OnMouseMove = mapNetworkMouseMove
          OnSelectionChanged = mapNetworkSelectionChanged
          ControlData = {
            8E1A0600734B0000A3130000010000000F0000800D47656F44696374696F6E61
            727905456D70747900E8030000000000000000030002001C001E000000000000
            0000000000000000000000000000000000000001000000000006000100000000
            005000010100000A000000001400000001F4010000050000800C000000000000
            000000000000FFFFFF0001000000000000000000000000000000000000000000
            00000352E30B918FCE119DE300AA004BB851010000009001A42C020005417269
            616C000352E30B918FCE119DE300AA004BB851010000009001DC7C0100054172
            69616C0000000000000000000000000000000000000000000000000000000000
            00000000000000FFFFFF000000000000000001370000000000FFFFFF00000000
            0000000352E30B918FCE119DE300AA004BB851010000009001DC7C0100054172
            69616C000352E30B918FCE119DE300AA004BB851010000009001A42C02000541
            7269616C0000000000000001000100FFFFFF000200FFFFFF0000000000000100
            000001000118010000A8021400030000001C6DD4770000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000030000
            00000000000000000000205EC0A967555555D545405798AAAAAA2A4640000000
            00000047405A643BEFDC495F4100000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000A6A9AE
            0892C7B4C169C64B0971FEB3C1C88428F0E0C1B54103688BEF018BB641010000
            0018010000A8021400010000000000FF001C0000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000020000
            0000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000
            0000000000587ED477B037F90000000000100000000C00000000000000000000
            0000000088B3400000000000408F400001000001}
        end
        object dxBarDockControl3: TdxBarDockControl
          Left = 1
          Top = 25
          Width = 91
          Height = 190
          Align = dalLeft
          BarManager = dxBarManager1
          SunkenBorder = True
          UseOwnSunkenBorder = True
        end
      end
      object RzPanel2: TPanel
        Left = 0
        Top = 237
        Width = 823
        Height = 305
        Align = alClient
        TabOrder = 1
        object rulVert: TRuler
          Left = 1
          Top = 153
          Width = 17
          Height = 151
          Align = alLeft
          BevelInner = bvNone
          BevelOuter = bvRaised
          BevelWidth = 1
          Spacing = 100.000000000000000000
          Zoom = 1.000000000000000000
          Style = rsVertical
        end
        object RzLabel14: TRzLabel
          Left = 1
          Top = 1
          Width = 821
          Height = 24
          Align = alTop
          AutoSize = False
          Caption = ' Profile View'
          Color = clBtnShadow
          Font.Charset = ANSI_CHARSET
          Font.Color = clWhite
          Font.Height = -16
          Font.Name = 'Tahoma'
          Font.Style = [fsBold]
          ParentColor = False
          ParentFont = False
          Layout = tlCenter
        end
        object RzPanel3: TRzPanel
          Left = 1
          Top = 136
          Width = 821
          Height = 17
          Align = alTop
          BorderOuter = fsNone
          TabOrder = 1
          object Bevel1: TBevel
            Left = 0
            Top = 0
            Width = 17
            Height = 17
            Align = alLeft
            Style = bsRaised
          end
          object rulHoriz: TRuler
            Left = 17
            Top = 0
            Width = 804
            Height = 17
            Align = alClient
            BevelInner = bvNone
            BevelOuter = bvRaised
            BevelWidth = 1
            Spacing = 100.000000000000000000
            Zoom = 1.000000000000000000
          end
        end
        object edtInplace: TdxEdit
          Left = 76
          Top = 92
          Width = 121
          TabOrder = 2
          Text = 'edtInplace'
          Visible = False
          StyleController = dxStyle
        end
        object dxBarDockControl2: TdxBarDockControl
          Left = 1
          Top = 25
          Width = 821
          Height = 83
          Align = dalTop
          BarManager = dxBarManager1
          SunkenBorder = True
          UseOwnSunkenBorder = True
        end
        object Panel1: TPanel
          Left = 1
          Top = 108
          Width = 821
          Height = 28
          Align = alTop
          TabOrder = 4
          DesignSize = (
            821
            28)
          object Label1: TLabel
            Left = 4
            Top = 8
            Width = 108
            Height = 13
            Caption = 'Comments on changes'
          end
          object edtComment: TdxEdit
            Left = 120
            Top = 4
            Width = 694
            TabOrder = 0
            Anchors = [akLeft, akTop, akRight]
            StyleController = dxStyle
          end
        end
        object DrawBox: TTransPanel
          Left = 18
          Top = 153
          Width = 804
          Height = 151
          Align = alClient
          Constraints.MinHeight = 30
          Constraints.MinWidth = 100
          TabOrder = 0
          OnDblClick = DrawBoxDblClick
          OnMouseDown = DrawBoxMouseDown
          OnMouseLeave = DrawBoxMouseLeave
          OnMouseMove = DrawBoxMouseMove
          OnMouseUp = DrawBoxMouseUp
          OnPaint = DrawBoxPaint
          OnResize = DrawBoxResize
        end
      end
      object pnlChanges: TRzSizePanel
        Left = 0
        Top = 542
        Width = 823
        Height = 127
        Hint = 'Drag bar to resize or click hot spot to show/hide Changes panel'
        Align = alBottom
        BorderOuter = fsStatus
        HotSpotVisible = True
        ParentShowHint = False
        RealTimeDrag = True
        ShowHint = True
        SizeBarStyle = ssGroove
        SizeBarWidth = 7
        TabOrder = 2
        UseGradients = True
        OnHotSpotClick = pnlChangesHotSpotClick
        DesignSize = (
          823
          127)
        object RzLabel13: TRzLabel
          Left = 1
          Top = 113
          Width = 821
          Height = 13
          Align = alBottom
          Caption = ' Use Ctrl+Delete to Delete Records'
          Font.Charset = ANSI_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'Tahoma'
          Font.Style = []
          ParentColor = False
          ParentFont = False
          Layout = tlCenter
        end
        object RzLabel19: TRzLabel
          Left = 1
          Top = 9
          Width = 821
          Height = 16
          Align = alTop
          AutoSize = False
          Caption = ' Changes Recorded'
          Color = clBtnShadow
          Font.Charset = ANSI_CHARSET
          Font.Color = clWhite
          Font.Height = -11
          Font.Name = 'Tahoma'
          Font.Style = []
          ParentColor = False
          ParentFont = False
          Layout = tlCenter
        end
        object grdNodeChanges: TdxDBGrid
          Left = 1
          Top = 49
          Width = 821
          Height = 64
          Bands = <
            item
            end>
          DefaultLayout = True
          HeaderPanelRowCount = 1
          KeyField = 'Node'
          SummaryGroups = <>
          SummarySeparator = ', '
          Align = alClient
          TabOrder = 2
          OnExit = grdChangesExit
          DataSource = srcDSNodeDetailChanges
          Filter.Criteria = {00000000}
          LookAndFeel = lfFlat
          OptionsBehavior = [edgoAutoSort, edgoDragScroll, edgoEditing, edgoEnterShowEditor, edgoImmediateEditor, edgoMultiSelect, edgoTabThrough, edgoVertThrough]
          OptionsDB = [edgoCanDelete, edgoCanNavigation, edgoConfirmDelete, edgoLoadAllRecords, edgoSmartRefresh, edgoUseBookmarks, edgoUseLocate]
          OptionsView = [edgoAutoWidth, edgoBandHeaderWidth, edgoHotTrack, edgoIndicator, edgoUseBitmap]
          object grdNodeChangesNode: TdxDBGridColumn
            Width = 61
            BandIndex = 0
            RowIndex = 0
            FieldName = 'Node'
          end
          object grdNodeChangesFieldName: TdxDBGridColumn
            Width = 105
            BandIndex = 0
            RowIndex = 0
            FieldName = 'FieldName'
          end
          object grdNodeChangesChanged: TdxDBGridCheckColumn
            Width = 54
            BandIndex = 0
            RowIndex = 0
            FieldName = 'Changed'
            ValueChecked = 'True'
            ValueUnchecked = 'False'
          end
          object grdNodeChangesOldValue: TdxDBGridColumn
            Width = 103
            BandIndex = 0
            RowIndex = 0
            FieldName = 'OldValue'
          end
          object grdNodeChangesNewValue: TdxDBGridColumn
            Width = 103
            BandIndex = 0
            RowIndex = 0
            FieldName = 'NewValue'
          end
          object grdNodeChangesComment: TdxDBGridColumn
            Width = 185
            BandIndex = 0
            RowIndex = 0
            FieldName = 'Comment'
          end
          object grdNodeChangesUserName: TdxDBGridColumn
            Width = 54
            BandIndex = 0
            RowIndex = 0
            FieldName = 'UserName'
          end
          object grdNodeChangesUserTime: TdxDBGridDateColumn
            Width = 96
            BandIndex = 0
            RowIndex = 0
            FieldName = 'UserTime'
          end
        end
        object grdChanges: TdxDBGrid
          Left = 1
          Top = 49
          Width = 821
          Height = 64
          TabStop = False
          Bands = <
            item
            end>
          DefaultLayout = True
          HeaderPanelRowCount = 1
          KeyField = 'LinkID'
          SummaryGroups = <>
          SummarySeparator = ', '
          Align = alClient
          TabOrder = 1
          OnExit = grdChangesExit
          DataSource = srcDetailChanges
          Filter.Criteria = {00000000}
          LookAndFeel = lfFlat
          OptionsDB = [edgoCanDelete, edgoCanNavigation, edgoConfirmDelete, edgoLoadAllRecords, edgoSmartRefresh, edgoUseBookmarks, edgoUseLocate]
          OptionsView = [edgoAutoWidth, edgoBandHeaderWidth, edgoHotTrack, edgoIndicator, edgoUseBitmap]
          object grdChangesLinkID: TdxDBGridMaskColumn
            Width = 63
            BandIndex = 0
            RowIndex = 0
            FieldName = 'LinkID'
          end
          object grdChangesFieldName: TdxDBGridColumn
            Width = 104
            BandIndex = 0
            RowIndex = 0
            FieldName = 'FieldName'
          end
          object grdChangesChanged: TdxDBGridCheckColumn
            ReadOnly = True
            Width = 51
            BandIndex = 0
            RowIndex = 0
            FieldName = 'Changed'
            ValueChecked = 'True'
            ValueUnchecked = 'False'
          end
          object grdChangesOldValue: TdxDBGridColumn
            BandIndex = 0
            RowIndex = 0
            FieldName = 'OldValue'
          end
          object grdChangesNewValue: TdxDBGridColumn
            Width = 70
            BandIndex = 0
            RowIndex = 0
            FieldName = 'NewValue'
          end
          object grdChangesComment: TdxDBGridColumn
            Width = 240
            BandIndex = 0
            RowIndex = 0
            FieldName = 'Comment'
          end
          object grdChangesUserName: TdxDBGridColumn
            Width = 57
            BandIndex = 0
            RowIndex = 0
            FieldName = 'UserName'
          end
          object grdChangesUserTime: TdxDBGridDateColumn
            Width = 112
            BandIndex = 0
            RowIndex = 0
            FieldName = 'UserTime'
          end
        end
        object pgcChanges: TdxPageControl
          Left = 1
          Top = 25
          Width = 821
          Height = 24
          ActivePage = tabLink
          Align = alTop
          HideButtons = False
          HotTrack = False
          MultiLine = False
          OwnerDraw = False
          RaggedRight = False
          ScrollOpposite = False
          TabHeight = 0
          TabOrder = 0
          TabPosition = dxtpTop
          TabStop = False
          TabWidth = 0
          OnChange = pgcChangesChange
          object tabLink: TdxTabSheet
            Caption = 'Link'
          end
          object tabUSNode: TdxTabSheet
            Caption = 'Upstream Node'
          end
          object tabDSNode: TdxTabSheet
            Caption = 'Downstream Node'
          end
        end
        object btnCloseChangeLog: TRzBitBtn
          Left = 753
          Top = 12
          Width = 65
          Anchors = [akTop, akRight]
          Caption = 'Close'
          TabOrder = 3
          OnClick = btnCloseChangeLogClick
          Glyph.Data = {
            F6000000424DF600000000000000760000002800000010000000100000000100
            0400000000008000000000000000000000001000000000000000000000000000
            80000080000000808000800000008000800080800000C0C0C000808080000000
            FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00777777777777
            777777777777777771F77771F7777777777777111F7777771F7777111F777771
            F777777111F77711F7777777111F711F77777777711111F77777777777111F77
            77777777711111F777777777111F71F77777771111F77711F77771111F777771
            1F77711F7777777711F777777777777777777777777777777777}
        end
      end
    end
    object dxBarDockControl4: TdxBarDockControl
      Left = 208
      Top = 1
      Width = 29
      Height = 669
      Align = dalLeft
      BarManager = dxBarManager1
      SunkenBorder = True
      UseOwnSunkenBorder = True
    end
  end
  object dxBarManager1: TdxBarManager
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    Bars = <
      item
        Caption = 'Main Menu'
        DockControl = dxBarDockControl1
        DockedDockControl = dxBarDockControl1
        DockedLeft = 0
        DockedTop = 0
        FloatLeft = 651
        FloatTop = 673
        FloatClientWidth = 23
        FloatClientHeight = 22
        IsMainMenu = True
        ItemLinks = <
          item
            Item = mnuFile
            Visible = True
          end
          item
            Item = mnuView
            Visible = True
          end
          item
            Item = mnuNetwork
            Visible = True
          end
          item
            Item = mnuTools
            Visible = True
          end>
        MultiLine = True
        Name = 'Main Menu'
        OneOnRow = True
        Row = 0
        UseOwnFont = False
        Visible = True
        WholeRow = True
      end
      item
        Caption = 'Toolbox'
        DockControl = dxBarDockControl4
        DockedDockControl = dxBarDockControl4
        DockedLeft = 0
        DockedTop = 0
        FloatLeft = 619
        FloatTop = 737
        FloatClientWidth = 23
        FloatClientHeight = 22
        ItemLinks = <
          item
            Item = mnuToolboxSelect
            Visible = True
          end
          item
            Item = mnuToolboxZoomIn
            Visible = True
          end
          item
            Item = mnuToolboxZoomOut
            Visible = True
          end
          item
            Item = mnuToolboxPan
            Visible = True
          end>
        Name = 'Standard'
        OneOnRow = True
        Row = 0
        UseOwnFont = False
        Visible = True
        WholeRow = False
      end
      item
        Caption = 'Standard'
        DockControl = dxBarDockControl1
        DockedDockControl = dxBarDockControl1
        DockedLeft = 0
        DockedTop = 23
        FloatLeft = 461
        FloatTop = 529
        FloatClientWidth = 23
        FloatClientHeight = 22
        ItemLinks = <
          item
            Item = mnuFileOpenDatabase
            Visible = True
          end
          item
            Item = mnuFileDeactivateDatabase
            Visible = True
          end
          item
            Item = mnuFileUpdateMasterDatabase
            Visible = True
          end>
        Name = 'Toolbox'
        OneOnRow = True
        Row = 1
        UseOwnFont = False
        Visible = True
        WholeRow = False
      end
      item
        BorderStyle = bbsNone
        Caption = 'Status Bar'
        DockedDockingStyle = dsBottom
        DockedLeft = 0
        DockedTop = 0
        DockingStyle = dsBottom
        FloatLeft = 672
        FloatTop = 934
        FloatClientWidth = 23
        FloatClientHeight = 22
        ItemLinks = <
          item
            Item = mnuStatusPosition
            Visible = True
          end
          item
            Item = mnuStatusSize
            Visible = True
          end
          item
            Item = mnuStatusCancel
            Visible = True
          end
          item
            Item = mnuStatusProgress
            Visible = True
          end
          item
            Item = mnuStatusStatus
            Visible = True
          end>
        Name = 'Status Bar'
        OneOnRow = True
        Row = 0
        UseOwnFont = False
        Visible = True
        WholeRow = True
      end
      item
        Caption = 'Profile'
        DockControl = dxBarDockControl2
        DockedDockControl = dxBarDockControl2
        DockedLeft = 0
        DockedTop = 0
        FloatLeft = 564
        FloatTop = 432
        FloatClientWidth = 73
        FloatClientHeight = 108
        ItemLinks = <
          item
            Item = mnuNetworkSaveProfile
            Visible = True
          end
          item
            Item = mnuNetworkRevertPath
            Visible = True
          end
          item
            Item = mnuNetworkHighlightPathOnNetwork
            Visible = True
          end
          item
            BeginGroup = True
            Item = mnuNetworkInterpolate
            Visible = True
          end
          item
            Item = mnuNetworkFixInvertsToDepths
            Visible = True
          end
          item
            Item = mnuFixGroundsToDepths
            Visible = True
          end
          item
            BeginGroup = True
            Item = mnuNetworkShiftLeft
            Visible = True
          end
          item
            Item = mnuNetworkShiftRight
            Visible = True
          end
          item
            BeginGroup = True
            Item = mnuViewExtents
            Visible = True
          end
          item
            Item = mnuViewAutoExtents
            Visible = True
          end
          item
            BeginGroup = True
            Item = dxBarLargeButton1
            Visible = True
          end
          item
            Item = mnuNetworkCopyAdjacentSlope
            Visible = True
          end
          item
            Item = mnuNetworkMinimumPipeSlop
            Visible = True
          end>
        MultiLine = True
        Name = 'Profile'
        OneOnRow = True
        Row = 0
        UseOwnFont = False
        Visible = True
        WholeRow = False
      end
      item
        Caption = 'Map'
        DockControl = dxBarDockControl3
        DockedDockControl = dxBarDockControl3
        DockedLeft = 0
        DockedTop = 0
        FloatLeft = 564
        FloatTop = 432
        FloatClientWidth = 68
        FloatClientHeight = 36
        ItemLinks = <
          item
            Item = mnuNetworkTracePath
            Visible = True
          end
          item
            Item = mnuViewAutoLabels
            Visible = True
          end
          item
            Item = mnuViewLineDirection
            Visible = True
          end
          item
            Item = mnuViewLayers
            Visible = True
          end
          item
            Item = mnuNetworkFind
            Visible = True
          end>
        Name = 'Map'
        OneOnRow = True
        RotateWhenVertical = False
        Row = 0
        UseOwnFont = False
        Visible = True
        WholeRow = False
      end>
    Categories.Strings = (
      'Built-in Menus'
      'File'
      'View'
      'Network'
      'Tools'
      'Status Bar'
      'Toolbox'
      'DataSource'
      'Anchor')
    Categories.ItemsVisibles = (
      2
      2
      2
      2
      2
      2
      2
      2
      2)
    Categories.Visibles = (
      True
      True
      True
      True
      True
      True
      True
      True
      True)
    Images = imglstBar
    LargeImages = imglstBar
    MenuAnimations = maSlide
    PopupMenuLinks = <
      item
        PopupMenu = dxBarDataSource
      end>
    StoreInRegistry = True
    Style = bmsFlat
    UseSystemFont = False
    Left = 708
    Top = 20
    DockControlHeights = (
      0
      0
      0
      24)
    object mnuFile: TdxBarSubItem
      Caption = '&File'
      Category = 0
      Visible = ivAlways
      ItemLinks = <
        item
          Item = mnuFileOpenDatabase
          Visible = True
        end
        item
          Item = mnuFileDeactivateDatabase
          Visible = True
        end
        item
          Item = mnuFileUpdateMasterDatabase
          Visible = True
        end
        item
          BeginGroup = True
          Item = btnFilePrint
          Visible = True
        end
        item
          BeginGroup = True
          Item = mnuFileMRUFiles
          Visible = True
        end
        item
          BeginGroup = True
          Item = mnuFileExit
          Visible = True
        end>
    end
    object mnuStatusStatus: TdxBarStatic
      Align = iaClient
      Caption = 'Status'
      Category = 5
      Description = 'Status bar'
      Hint = 'Status'
      Visible = ivAlways
      Alignment = taLeftJustify
      ImageIndex = 78
    end
    object mnuStatusPosition: TdxBarStatic
      Caption = 'Position'
      Category = 5
      Description = 'Position of the cursor'
      Hint = 'Position'
      Visible = ivAlways
      Alignment = taLeftJustify
      ImageIndex = 74
      Width = 150
    end
    object mnuStatusSize: TdxBarStatic
      Caption = 'Size'
      Category = 5
      Description = 'Size of the element currently being resized'
      Hint = 'Size'
      Visible = ivAlways
      Alignment = taLeftJustify
      ImageIndex = 77
      Width = 150
    end
    object mnuToolboxSelect: TdxBarButton
      Action = actToolboxSelect
      Category = 6
      Description = 'Select an element from the plan view or profile view'
      ButtonStyle = bsChecked
      GroupIndex = 1
    end
    object mnuToolboxZoomIn: TdxBarButton
      Action = actToolboxZoomIn
      Category = 6
      Description = 
        'Zooms in for the plan view or profile view; drag to zoom in to t' +
        'he desired area'
      Hint = 'Zoom In'
      ButtonStyle = bsChecked
      GroupIndex = 1
    end
    object mnuToolboxZoomOut: TdxBarButton
      Action = actToolboxZoomOut
      Category = 6
      Description = 
        'Zooms out for the plan view or profile view; drag to fit the cur' +
        'rent window inside the desired area'
      ButtonStyle = bsChecked
      GroupIndex = 1
    end
    object mnuToolboxPan: TdxBarButton
      Action = actToolboxPan
      Category = 6
      Description = 'Pans the plan view or profile view'
      ButtonStyle = bsChecked
      GroupIndex = 1
    end
    object mnuViewTraceThematic: TdxBarButton
      Action = actViewTraceThematic
      Category = 2
      Description = 'Options for the Trace thematic map in the Plan View'
      Hint = 'Trace Thematic'
    end
    object mnuView: TdxBarSubItem
      Caption = '&View'
      Category = 0
      Visible = ivAlways
      ItemLinks = <
        item
          Item = mnuViewTraceThematic
          Visible = True
        end
        item
          Item = mnuViewNetworkThematic
          Visible = True
        end
        item
          Item = mnuViewExtents
          Visible = True
        end
        item
          Item = mnuViewAutoExtents
          Visible = True
        end
        item
          Item = mnuViewAutoLabels
          Visible = True
        end
        item
          BeginGroup = True
          Item = mnuViewResetToolbars
          Visible = True
        end
        item
          Item = mnuViewShowToolbarCaptions
          Visible = True
        end
        item
          BeginGroup = True
          Item = mnuViewChangeLog
          Visible = True
        end>
    end
    object mnuStatusCancel: TdxBarButton
      Caption = 'Cancel'
      Category = 5
      Description = 'Cancel'
      Hint = 'Cancel'
      Visible = ivNever
      ImageIndex = 81
      OnClick = mnuStatusCancelClick
    end
    object mnuNetworkTracePath: TdxBarLargeButton
      Action = actNetworkTracePath
      Category = 3
      Description = 
        'Traces the path, from upstream to downstream, between at least t' +
        'wo selected links in the plan view'
    end
    object mnuNetworkSaveProfile: TdxBarLargeButton
      Action = actNetworkSaveProfile
      Category = 3
      Description = 
        'Saves any changes to the profile and records them in the change ' +
        'log'
    end
    object mnuFileOpenDatabase: TdxBarLargeButton
      Action = actFileOpenDatabase
      Category = 1
      Description = 'Opens an EMGAATS-compliant database for editing'
    end
    object mnuFileExit: TdxBarButton
      Action = actFileExit
      Category = 1
      Description = 'Exits the Profiler'
    end
    object mnuStatusProgress: TdxBarProgressItem
      Caption = 'Progress'
      Category = 5
      Description = 'Progress bar'
      Hint = 'Progress'
      Visible = ivNever
      Width = 200
      Smooth = True
    end
    object mnuNetwork: TdxBarSubItem
      Caption = '&Network'
      Category = 0
      Visible = ivAlways
      ItemLinks = <
        item
          Item = mnuNetworkTracePath
          Visible = True
        end
        item
          Item = mnuNetworkSaveProfile
          Visible = True
        end
        item
          Item = mnuNetworkRevertPath
          Visible = True
        end
        item
          Item = mnuNetworkInterpolate
          Visible = True
        end
        item
          Item = dxBarLargeButton1
          Visible = True
        end
        item
          Item = mnuNetworkMinimumPipeSlop
          Visible = True
        end
        item
          Item = mnuNetworkCopyAdjacentSlope
          Visible = True
        end
        item
          Item = mnuNetworkFind
          Visible = True
        end>
    end
    object mnuNetworkRevertPath: TdxBarLargeButton
      Action = actNetworkRevertPath
      Category = 3
      Description = 
        'Reverts the profile'#39's data to what is currently held in the data' +
        'base'
      Hint = 'Revert'
    end
    object mnuViewNetworkThematic: TdxBarButton
      Action = actViewNetworkThematic
      Category = 2
      Description = 'Options for the Network thematic map in the Plan View'
    end
    object mnuNetworkInterpolate: TdxBarLargeButton
      Action = actNetworkInterpolate
      Category = 3
      Description = 
        'Interpolates inverts between the two selected links in the profi' +
        'le'
    end
    object mnuViewExtents: TdxBarLargeButton
      Action = actViewExtents
      Category = 2
      Description = 'Stretches the profile to the entire profile window'
      Hint = 'Zoom Extents'
    end
    object mnuNetworkFixInvertsToDepths: TdxBarLargeButton
      Action = actNetworkFixInvertsToDepths
      Category = 3
      Description = 
        'Fixes inverts between the two selected links in the profile by s' +
        'ubtracting the Up Depth and Down Depth from the Up Ground and Do' +
        'wn Ground'
    end
    object mnuViewAutoExtents: TdxBarLargeButton
      Action = actViewAutoExtents
      Category = 2
      Description = 
        'Automatically stretch the profile to the entire profile window w' +
        'hen the profile window is resized'
      AllowAllUp = True
      ButtonStyle = bsChecked
      GroupIndex = 2
      LargeImageIndex = 85
    end
    object mnuViewAutoLabels: TdxBarLargeButton
      Action = actViewAutoLabels
      Category = 2
      Description = 'Automatically labels the links in the plan view'
      ButtonStyle = bsChecked
      Down = True
    end
    object mnuViewLineDirection: TdxBarLargeButton
      Action = actViewLineDirections
      Category = 2
      Description = 'Shows line directions for the links in the plan view'
      ButtonStyle = bsChecked
      Down = True
    end
    object mnuFixGroundsToDepths: TdxBarLargeButton
      Action = actNetworkFixGroundsToDepths
      Category = 3
      Description = 
        'Fixes ground levels between the two selected links in the profil' +
        'e by adding the Up Depth and Down Depth to the Up Invert and Dow' +
        'n Invert'
    end
    object mnuNetworkFuturePipes: TdxBarLargeButton
      Action = actNetworkFuturePipes
      Category = 3
      Description = '(currently not used)'
      Hint = 'Future Pipes'
      ButtonStyle = bsChecked
    end
    object mnuNetworkHighlightPathOnNetwork: TdxBarLargeButton
      Action = actNetworkHighlightPathOnNetwork
      Category = 3
      Description = 'Highlights the profile'#39's path in the plan view'
      Hint = 'Show Path'
    end
    object mnuNetworkShiftLeft: TdxBarLargeButton
      Action = actNetworkShiftLeft
      Category = 3
      Description = 'Shifts the profile left (upstream) by one link'
      Hint = 'Shift Left'
    end
    object mnuNetworkShiftRight: TdxBarLargeButton
      Action = actNetworkShiftRight
      Category = 3
      Description = 'Shifts the profile right (downstream) one link'
      Hint = 'Shift Right'
    end
    object mnuFileDeactivateDatabase: TdxBarLargeButton
      Action = actNetworkDeactivateDatabase
      Category = 1
      Description = 
        'Temporarily deactivates the database currently open to reduce di' +
        'fficulties using other programs'
      AllowAllUp = True
      ButtonStyle = bsChecked
      GroupIndex = 3
    end
    object btnFilePrint: TdxBarButton
      Action = actFilePrint
      Category = 1
      Description = 'Prints the current profile'
    end
    object mnuFileMRUFiles: TdxBarMRUListItem
      Caption = 'Most Recently Used Files'
      Category = 1
      Visible = ivAlways
      OnClick = mnuFileMRUFilesClick
    end
    object mnuTools: TdxBarSubItem
      Caption = '&Tools'
      Category = 0
      Visible = ivAlways
      ItemLinks = <
        item
          Item = mnuToolsOptions
          Visible = True
        end>
    end
    object mnuToolsOptions: TdxBarButton
      Action = actToolsOptions
      Category = 4
      Description = '(currently not used)'
    end
    object mnuDSDimension: TdxBarLookupCombo
      Caption = 'Dimension'
      Category = 7
      Description = 'Dimension data source flag'
      Hint = 'Dimension'
      Visible = ivAlways
      OnChange = mnuDSDimensionChange
      OnEnter = mnuDSDimensionEnter
      Glyph.Data = {
        F6000000424DF600000000000000760000002800000010000000100000000100
        0400000000008000000000000000000000001000000000000000000000000000
        8000008000000080800080000000800080008080000080808000C0C0C0000000
        FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00DDDDDDDDDDDD
        DDDD000000000000000D0FFFF0FFFFFFFF0D0F77F0F777777F0D0CCCC0CCCCCC
        CC0D0C77C0C777777C0D0CCCC0CCCCCCCC0D0F77F0F777777F0D0FFFF0FFFFFF
        FF0D0F77F0F777777F0D0FFFF0FFFFFFFF0D000000000000000D0FFFCCCCFFF0
        DDDD0F777777FFF0DDDD0FFFCCCCFFF0DDDD000000000000DDDD}
      Width = 100
      OnCloseUp = mnuDSDimensionCloseUp
      ImmediateDropDown = True
      KeyField = 'DataSourceID'
      ListField = 'DataSourceID;Description'
      ListSource = srcDataSources
      RowCount = 20
    end
    object mnuDSShape: TdxBarLookupCombo
      Caption = 'Shape'
      Category = 7
      Description = 'Shape data source flag'
      Hint = 'Shape'
      Visible = ivAlways
      OnChange = mnuDSShapeChange
      OnEnter = mnuDSDimensionEnter
      Glyph.Data = {
        F6000000424DF600000000000000760000002800000010000000100000000100
        0400000000008000000000000000000000001000000000000000000000000000
        8000008000000080800080000000800080008080000080808000C0C0C0000000
        FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00DDDDDDDDDDDD
        DDDD000000000000000D0FFFF0FFFFFFFF0D0F77F0F777777F0D0CCCC0CCCCCC
        CC0D0C77C0C777777C0D0CCCC0CCCCCCCC0D0F77F0F777777F0D0FFFF0FFFFFF
        FF0D0F77F0F777777F0D0FFFF0FFFFFFFF0D000000000000000D0FFFCCCCFFF0
        DDDD0F777777FFF0DDDD0FFFCCCCFFF0DDDD000000000000DDDD}
      Width = 100
      OnCloseUp = mnuDSDimensionCloseUp
      ImmediateDropDown = True
      KeyField = 'DataSourceID'
      ListField = 'DataSourceID;Description'
      ListSource = srcDataSources
      RowCount = 20
    end
    object mnuDSMaterial: TdxBarLookupCombo
      Caption = 'Material'
      Category = 7
      Description = 'Material data asource flag'
      Hint = 'Material'
      Visible = ivAlways
      OnChange = mnuDSMaterialChange
      OnEnter = mnuDSDimensionEnter
      Glyph.Data = {
        F6000000424DF600000000000000760000002800000010000000100000000100
        0400000000008000000000000000000000001000000000000000000000000000
        8000008000000080800080000000800080008080000080808000C0C0C0000000
        FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00DDDDDDDDDDDD
        DDDD000000000000000D0FFFF0FFFFFFFF0D0F77F0F777777F0D0CCCC0CCCCCC
        CC0D0C77C0C777777C0D0CCCC0CCCCCCCC0D0F77F0F777777F0D0FFFF0FFFFFF
        FF0D0F77F0F777777F0D0FFFF0FFFFFFFF0D000000000000000D0FFFCCCCFFF0
        DDDD0F777777FFF0DDDD0FFFCCCCFFF0DDDD000000000000DDDD}
      Width = 100
      OnCloseUp = mnuDSDimensionCloseUp
      ImmediateDropDown = True
      KeyField = 'DataSourceID'
      ListField = 'DataSourceID;Description'
      ListSource = srcDataSources
      RowCount = 20
    end
    object mnuDSIEUp: TdxBarLookupCombo
      Caption = 'IE Up'
      Category = 7
      Description = 'IE Up data source flag'
      Hint = 'IE Up'
      Visible = ivAlways
      OnChange = mnuDSIEUpChange
      OnEnter = mnuDSDimensionEnter
      Glyph.Data = {
        F6000000424DF600000000000000760000002800000010000000100000000100
        0400000000008000000000000000000000001000000000000000000000000000
        8000008000000080800080000000800080008080000080808000C0C0C0000000
        FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00DDDDDDDDDDDD
        DDDD000000000000000D0FFFF0FFFFFFFF0D0F77F0F777777F0D0CCCC0CCCCCC
        CC0D0C77C0C777777C0D0CCCC0CCCCCCCC0D0F77F0F777777F0D0FFFF0FFFFFF
        FF0D0F77F0F777777F0D0FFFF0FFFFFFFF0D000000000000000D0FFFCCCCFFF0
        DDDD0F777777FFF0DDDD0FFFCCCCFFF0DDDD000000000000DDDD}
      Width = 100
      OnCloseUp = mnuDSDimensionCloseUp
      ImmediateDropDown = True
      KeyField = 'DataSourceID'
      ListField = 'DataSourceID;Description'
      ListSource = srcDataSources
      RowCount = 20
    end
    object mnuDSIEDown: TdxBarLookupCombo
      Caption = 'IE Down'
      Category = 7
      Description = 'IE down data source flag'
      Hint = 'IE Down'
      Visible = ivAlways
      OnChange = mnuDSIEDownChange
      OnEnter = mnuDSDimensionEnter
      Glyph.Data = {
        F6000000424DF600000000000000760000002800000010000000100000000100
        0400000000008000000000000000000000001000000000000000000000000000
        8000008000000080800080000000800080008080000080808000C0C0C0000000
        FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00DDDDDDDDDDDD
        DDDD000000000000000D0FFFF0FFFFFFFF0D0F77F0F777777F0D0CCCC0CCCCCC
        CC0D0C77C0C777777C0D0CCCC0CCCCCCCC0D0F77F0F777777F0D0FFFF0FFFFFF
        FF0D0F77F0F777777F0D0FFFF0FFFFFFFF0D000000000000000D0FFFCCCCFFF0
        DDDD0F777777FFF0DDDD0FFFCCCCFFF0DDDD000000000000DDDD}
      Width = 100
      OnCloseUp = mnuDSDimensionCloseUp
      ImmediateDropDown = True
      KeyField = 'DataSourceID'
      ListField = 'DataSourceID;Description'
      ListSource = srcDataSources
      RowCount = 20
    end
    object mnuDSRim: TdxBarLookupCombo
      Caption = 'Rim'
      Category = 7
      Description = 'Rim data source flag'
      Hint = 'Rim'
      Visible = ivAlways
      OnChange = mnuDSRimChange
      OnEnter = mnuDSDimensionEnter
      Glyph.Data = {
        F6000000424DF600000000000000760000002800000010000000100000000100
        0400000000008000000000000000000000001000000000000000000000000000
        8000008000000080800080000000800080008080000080808000C0C0C0000000
        FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00DDDDDDDDDDDD
        DDDD000000000000000D0FFFF0FFFFFFFF0D0F77F0F777777F0D0CCCC0CCCCCC
        CC0D0C77C0C777777C0D0CCCC0CCCCCCCC0D0F77F0F777777F0D0FFFF0FFFFFF
        FF0D0F77F0F777777F0D0FFFF0FFFFFFFF0D000000000000000D0FFFCCCCFFF0
        DDDD0F777777FFF0DDDD0FFFCCCCFFF0DDDD000000000000DDDD}
      Width = 100
      OnCloseUp = mnuDSDimensionCloseUp
      ImmediateDropDown = True
      KeyField = 'DataSourceID'
      ListField = 'DataSourceID;Description'
      ListSource = srcDataSources
      RowCount = 20
    end
    object mnuDSLength: TdxBarLookupCombo
      Caption = 'Length'
      Category = 7
      Description = 'Length data source flag'
      Hint = 'Length'
      Visible = ivAlways
      OnChange = mnuDSLengthChange
      OnEnter = mnuDSDimensionEnter
      Glyph.Data = {
        F6000000424DF600000000000000760000002800000010000000100000000100
        0400000000008000000000000000000000001000000000000000000000000000
        8000008000000080800080000000800080008080000080808000C0C0C0000000
        FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00DDDDDDDDDDDD
        DDDD000000000000000D0FFFF0FFFFFFFF0D0F77F0F777777F0D0CCCC0CCCCCC
        CC0D0C77C0C777777C0D0CCCC0CCCCCCCC0D0F77F0F777777F0D0FFFF0FFFFFF
        FF0D0F77F0F777777F0D0FFFF0FFFFFFFF0D000000000000000D0FFFCCCCFFF0
        DDDD0F777777FFF0DDDD0FFFCCCCFFF0DDDD000000000000DDDD}
      Width = 100
      OnCloseUp = mnuDSDimensionCloseUp
      ImmediateDropDown = True
      KeyField = 'DataSourceID'
      ListField = 'DataSourceID;Description'
      ListSource = srcDataSources
      RowCount = 20
    end
    object mnuDSSynth: TdxBarLookupCombo
      Caption = 'Synth'
      Category = 7
      Description = 'Synth data source flag'
      Hint = 'Synth'
      Visible = ivAlways
      OnChange = mnuDSSynthChange
      OnEnter = mnuDSDimensionEnter
      Glyph.Data = {
        F6000000424DF600000000000000760000002800000010000000100000000100
        0400000000008000000000000000000000001000000000000000000000000000
        8000008000000080800080000000800080008080000080808000C0C0C0000000
        FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00DDDDDDDDDDDD
        DDDD000000000000000D0FFFF0FFFFFFFF0D0F77F0F777777F0D0CCCC0CCCCCC
        CC0D0C77C0C777777C0D0CCCC0CCCCCCCC0D0F77F0F777777F0D0FFFF0FFFFFF
        FF0D0F77F0F777777F0D0FFFF0FFFFFFFF0D000000000000000D0FFFCCCCFFF0
        DDDD0F777777FFF0DDDD0FFFCCCCFFF0DDDD000000000000DDDD}
      Width = 100
      OnCloseUp = mnuDSDimensionCloseUp
      ImmediateDropDown = True
      KeyField = 'DataSourceID'
      ListField = 'DataSourceID;Description'
      ListSource = srcDataSources
      RowCount = 20
    end
    object mnuFileUpdateMasterDatabase: TdxBarLargeButton
      Action = actFileUpdateMasterDatabase
      Category = 1
      Description = 
        'Updates the master database using changes recorded for the activ' +
        'e database'
      Hint = 'Update Master Database'
    end
    object dxBarLargeButton1: TdxBarLargeButton
      Action = actNetworkCopyGroundSlope
      Category = 3
      Description = 
        'Changes the inverts of the selected links so that the links'#39' slo' +
        'pes match their ground slopes'
      ButtonStyle = bsDropDown
      DropDownMenu = dxBarAnchor
    end
    object mnuNetworkMinimumPipeSlop: TdxBarLargeButton
      Action = actNetworkMinimumPipeSlope
      Category = 3
      Description = 
        'Changes the inverts of the selected pipes to make sure the pipe ' +
        'can deliver flow at the specified minimum velocity at half/full ' +
        'flow depth'
      Hint = 'Minimum Pipe Slope'
      ButtonStyle = bsDropDown
      DropDownMenu = dxBarAnchor
    end
    object mnuNetworkCopyAdjacentSlope: TdxBarLargeButton
      Action = actNetworkCopyAdjacentSlope
      Category = 3
      Description = 
        'Changes the inverts of the selected link to match an adjacent pi' +
        'pe'#39's slope'
      Hint = 'Copy Adjacent Slope'
      ButtonStyle = bsDropDown
      DropDownMenu = dxBarAnchor
    end
    object mnuNetworkFind: TdxBarLargeButton
      Action = actNetworkFind
      Category = 3
      Description = 
        'Searches for a pipe using its CompKey, one of its node IDs, its ' +
        'LinkID, or its MLinkID'
    end
    object btnAnchorUpstream: TdxBarButton
      Caption = 'Anchor Upstream'
      Category = 8
      Description = 
        'Anchors the invert changes so the downstream invert doesn'#39't chan' +
        'ge and the upstream invert is recalculated'
      Hint = 'Anchor Upstream'
      Visible = ivAlways
      ButtonStyle = bsChecked
      GroupIndex = 2
      Down = True
    end
    object btnAnchorDownstream: TdxBarButton
      Caption = 'Anchor Downstream'
      Category = 8
      Description = 
        'Anchors the invert changes so the upstream invert doesn'#39't change' +
        ' and the downstream invert is recalculated'
      Hint = 'Anchor Downstream'
      Visible = ivAlways
      ButtonStyle = bsChecked
      GroupIndex = 2
    end
    object mnuAnchorRoughness: TdxBarSpinEdit
      Caption = 'Roughness'
      Category = 8
      Description = 'Pipe roughness for calculating minimum pipe slope'
      Hint = 'Roughness'
      Visible = ivAlways
      Width = 100
      ValueType = svtFloat
      Increment = 0.001000000000000000
      Value = 0.013000000000000000
    end
    object mnuAnchorVelocity: TdxBarSpinEdit
      Caption = 'Velocity'
      Category = 8
      Description = 'Minimum pipe velocity for calculating minimum pipe slope'
      Hint = 'Velocity'
      Visible = ivAlways
      Width = 100
      ValueType = svtFloat
      Prefix = ' fps'
      Value = 3.000000000000000000
    end
    object mnuViewLayers: TdxBarLargeButton
      Action = actViewLayers
      Category = 2
      Description = 
        'Shows options for the layers available as well as allows user to' +
        ' add layers'
    end
    object mnuViewChangeLog: TdxBarButton
      Action = actViewChangeLog
      Category = 2
      Description = 'Shows the change log'
      Hint = 'Change Log'
      ButtonStyle = bsChecked
    end
    object mnuViewResetToolbars: TdxBarButton
      Action = actViewResetToolbars
      Category = 2
      Description = 'Resets toolbars'
    end
    object mnuViewShowToolbarCaptions: TdxBarButton
      Action = actViewShowToolbarCaptions
      Category = 2
      Hint = 'Show Toolbar Captions'
      ButtonStyle = bsChecked
    end
  end
  object actActions: TActionList
    Images = imglstBar
    Left = 572
    Top = 20
    object actToolboxSelect: TAction
      Category = 'Toolbox'
      Caption = 'Select'
      Checked = True
      GroupIndex = 1
      ImageIndex = 20
      OnExecute = actToolboxSelectExecute
    end
    object actToolboxZoomIn: TAction
      Category = 'Toolbox'
      Caption = 'Zoom In'
      GroupIndex = 1
      ImageIndex = 79
      OnExecute = actToolboxZoomInExecute
    end
    object actToolboxZoomOut: TAction
      Category = 'Toolbox'
      Caption = 'Zoom Out'
      GroupIndex = 1
      ImageIndex = 80
      OnExecute = actToolboxZoomOutExecute
    end
    object actToolboxPan: TAction
      Category = 'Toolbox'
      Caption = 'Pan'
      GroupIndex = 1
      ImageIndex = 19
      OnExecute = actToolboxPanExecute
    end
    object actViewTraceThematic: TAction
      Category = 'View'
      Caption = 'Trace Thematic'
      OnExecute = actViewTraceThematicExecute
    end
    object actNetworkTracePath: TAction
      Category = 'Network'
      Caption = 'Trace Path'
      Hint = 'Trace between the selected pipes'
      ImageIndex = 56
      OnExecute = actNetworkTracePathExecute
      OnUpdate = actNetworkTracePathUpdate
    end
    object actNetworkSaveProfile: TAction
      Category = 'Network'
      Caption = 'Save'
      Hint = 'Save changes to the profile'
      ImageIndex = 39
      OnExecute = actNetworkSaveProfileExecute
      OnUpdate = actNetworkSaveProfileUpdate
    end
    object actFileOpenDatabase: TAction
      Category = 'File'
      Caption = 'Open Database'
      ImageIndex = 95
      OnExecute = actFileOpenDatabaseExecute
    end
    object actFileExit: TFileExit
      Category = 'File'
      Caption = 'E&xit'
      Hint = 'Exit|Quits the application'
    end
    object actNetworkInterpolate: TAction
      Category = 'Network'
      Caption = 'Interpolate'
      Hint = 'Interpolate inverts between the selected pipes'
      ImageIndex = 84
      OnExecute = actNetworkInterpolateExecute
      OnUpdate = actNetworkInterpolateUpdate
    end
    object actNetworkRevertPath: TAction
      Category = 'Network'
      Caption = 'Revert'
      Hint = 'Revert the profile'
      ImageIndex = 43
      OnExecute = actNetworkRevertPathExecute
      OnUpdate = actNetworkRevertPathUpdate
    end
    object actViewExtents: TAction
      Category = 'View'
      Caption = 'Zoom Extents'
      ImageIndex = 83
      OnExecute = actViewExtentsExecute
      OnUpdate = actViewExtentsUpdate
    end
    object actNetworkFuturePipes: TAction
      Category = 'Network'
      Caption = 'Future Pipes'
      GroupIndex = 2
      OnExecute = actNetworkFuturePipesExecute
    end
    object actViewAutoExtents: TAction
      Category = 'View'
      Caption = 'Auto Extents'
      GroupIndex = 2
      ImageIndex = 86
      OnExecute = actViewAutoExtentsExecute
      OnUpdate = actViewAutoExtentsUpdate
    end
    object actViewAutoLabels: TAction
      Category = 'View'
      Caption = 'Auto Labels'
      ImageIndex = 85
      OnExecute = actViewAutoLabelsExecute
      OnUpdate = actViewAutoLabelsUpdate
    end
    object actNetworkFixGroundToRimDepth: TAction
      Category = 'Network'
      Caption = 'Fix Ground to Rim Depth'
      ImageIndex = 88
      OnExecute = actNetworkFixGroundToRimDepthExecute
      OnUpdate = actNetworkFixGroundToRimDepthUpdate
    end
    object actViewLineDirections: TAction
      Category = 'View'
      Caption = 'Line Directions'
      ImageIndex = 89
      OnExecute = actViewLineDirectionsExecute
      OnUpdate = actViewLineDirectionsUpdate
    end
    object actViewLayers: TAction
      Category = 'View'
      Caption = 'Layers'
      ImageIndex = 88
      OnExecute = actViewLayersExecute
      OnUpdate = actViewLayersUpdate
    end
    object actNetworkFixInvertsToDepths: TAction
      Category = 'Network'
      Caption = 'Fix Inverts to Depths'
      ImageIndex = 90
      OnExecute = actNetworkFixInvertsToDepthsExecute
      OnUpdate = actNetworkFixInvertsToDepthsUpdate
    end
    object actNetworkFixGroundsToDepths: TAction
      Category = 'Network'
      Caption = 'Fix Grounds to Depths'
      ImageIndex = 91
      OnExecute = actNetworkFixGroundsToDepthsExecute
      OnUpdate = actNetworkFixGroundsToDepthsUpdate
    end
    object actNetworkHighlightPathOnNetwork: TAction
      Category = 'Network'
      Caption = 'Show Path'
      Hint = 'Rehighlight Path on Plan View'
      ImageIndex = 92
      OnExecute = actNetworkHighlightPathOnNetworkExecute
      OnUpdate = actNetworkHighlightPathOnNetworkUpdate
    end
    object actNetworkShiftLeft: TAction
      Category = 'Network'
      Caption = 'Shift Left'
      Hint = 'Shift current selection upstream'
      ImageIndex = 94
      OnExecute = actNetworkShiftLeftExecute
      OnUpdate = actNetworkHighlightPathOnNetworkUpdate
    end
    object actNetworkShiftRight: TAction
      Category = 'Network'
      Caption = 'Shift Right'
      Hint = 'Shift current selection downstream'
      ImageIndex = 93
      OnExecute = actNetworkShiftRightExecute
      OnUpdate = actNetworkHighlightPathOnNetworkUpdate
    end
    object actNetworkDeactivateDatabase: TAction
      Category = 'Network'
      Caption = 'Deactivate Database'
      ImageIndex = 96
      OnExecute = actNetworkDeactivateDatabaseExecute
      OnUpdate = actNetworkDeactivateDatabaseUpdate
    end
    object actFilePrint: TAction
      Category = 'File'
      Caption = '&Print'
      ImageIndex = 38
      OnExecute = actFilePrintExecute
      OnUpdate = actFilePrintUpdate
    end
    object actFileUpdateMasterDatabase: TAction
      Category = 'File'
      Caption = 'Update Master Database'
      ImageIndex = 97
      OnExecute = actFileUpdateMasterDatabaseExecute
      OnUpdate = actFileUpdateMasterDatabaseUpdate
    end
    object actToolsOptions: TAction
      Category = 'Tools'
      Caption = 'Options...'
    end
    object actNetworkCopyGroundSlope: TAction
      Category = 'Network'
      Caption = 'Copy Ground Slope'
      ImageIndex = 100
      OnExecute = actNetworkCopyGroundSlopeExecute
      OnUpdate = actNetworkCopyGroundSlopeUpdate
    end
    object actNetworkMinimumPipeSlope: TAction
      Category = 'Network'
      Caption = 'Minimum Pipe Slope'
      ImageIndex = 98
      OnExecute = actNetworkMinimumPipeSlopeExecute
      OnUpdate = actNetworkMinimumPipeSlopeUpdate
    end
    object actNetworkCopyAdjacentSlope: TAction
      Category = 'Network'
      Caption = 'Copy Adjacent Slope'
      ImageIndex = 99
      OnExecute = actNetworkCopyAdjacentSlopeExecute
      OnUpdate = actNetworkCopyAdjacentSlopeUpdate
    end
    object actNetworkFind: TAction
      Category = 'Network'
      Caption = 'Find'
      ImageIndex = 42
      OnExecute = actNetworkFindExecute
      OnUpdate = actNetworkFindUpdate
    end
    object actViewChangeLog: TAction
      Category = 'View'
      Caption = 'Change Log'
      Checked = True
      OnExecute = actViewChangeLogExecute
    end
    object actViewNetworkThematic: TAction
      Category = 'View'
      Caption = 'Network Thematic'
      Hint = 'Network Thematic'
      OnExecute = actViewNetworkThematicExecute
    end
    object actViewResetToolbars: TAction
      Category = 'View'
      Caption = 'Reset Toolbars'
      OnExecute = actViewResetToolbarsExecute
    end
    object actViewShowToolbarCaptions: TAction
      Category = 'View'
      Caption = 'Show Toolbar Captions'
      Checked = True
      OnExecute = actViewShowToolbarCaptionsExecute
    end
  end
  object imglstBar: TImageList
    Left = 632
    Top = 20
    Bitmap = {
      494C010166006800040010001000FFFFFFFFFF10FFFFFFFFFFFFFFFF424D3600
      000000000000360000002800000040000000A0010000010020000000000000A0
      0100000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000000000008484
      840000FFFF008484840000FFFF0000FFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000000000000000FF
      FF008484840000FFFF0000FFFF0000FFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000000000008484
      840000FFFF008484840000FFFF0000FFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000008400000000000000000000000000000000000000FF
      FF008484840000FFFF0000FFFF0000FFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000008400000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000084000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000000000000000000000000000000000000000000000000000008400000084
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000008400000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000008400000084
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000084000000000000008400000084000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000084000000840000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000084000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000084000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000008484840000FF
      FF008484840000FFFF0000FFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      840000FFFF008484840000FFFF0000FFFF000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000FFFF008484
      840000FFFF0000FFFF0000FFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000008400000000000000000000000000000000000000FF
      FF008484840000FFFF0000FFFF0000FFFF000000000000000000000000000000
      00000000000000000000000000000000000000000000000000008484840000FF
      FF008484840000FFFF0000FFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      840000FFFF008484840000FFFF0000FFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000FFFF008484
      840000FFFF0000FFFF0000FFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000000000000000
      0000000084000000840000008400000084000000840000000000000000000000
      0000000000000000000000840000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000008400000000000000000000000000000000000000FF
      FF008484840000FFFF0000FFFF0000FFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000FFFF008484840000FFFF00C6C6C60000FF
      FF0000FFFF0000FFFF0000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000008484840000FFFF008484840000FFFF0000FF
      FF0000FFFF0000FFFF0000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000840000008400000000000000
      0000000000000000840000008400000000000000000000000000000000000000
      000000000000000000000000000000FFFF008484840000FFFF00C6C6C60000FF
      FF0000FFFF0000FFFF0000000000000000000000000000000000008400000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000084000000000000000000000000000000000000008400000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000084000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000008400000084000000
      0000000084000000840000000000000000000000000000000000000000000000
      00000000000000000000000000008484840000FFFF008484840000FFFF0000FF
      FF0000FFFF0000FFFF0000000000000000000000000000000000000000000084
      0000000000000000000000000000000000000000000000000000008400000084
      0000000000000000000000000000000000000000000000000000000000000084
      0000000000000000000000000000000000000000000000000000008400000084
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000084000000
      8400000084000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000084000000
      8400000084000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000FFFF008484840000FFFF00C6C6C60000FF
      FF0000FFFF0000FFFF0000000000000000000000000000000000000000000000
      0000000000000084000000000000008400000084000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000084000000000000008400000084000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000008400000084000000
      0000000084000000840000000000000000000000000000000000000000000000
      00000000000000000000000000008484840000FFFF008484840000FFFF0000FF
      FF0000FFFF0000FFFF0000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000840000008400000000000000
      0000000000000000840000008400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      8400008484000084840000848400008484000084840000848400008484000084
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000FFFFFF000000
      0000008484000084840000848400008484000084840000848400008484000084
      8400008484000000000000000000000000000000000000000000000000000000
      000000000000000000000000FF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000FFFF00FFFF
      FF00000000000084840000848400008484000084840000848400008484000084
      8400008484000084840000000000000000000000000000000000000000000000
      000000000000000000000000FF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000FFFFFF0000FF
      FF00FFFFFF000000000000848400008484000084840000848400008484000084
      8400008484000084840000848400000000000000000000000000000000000000
      000000000000000000000000FF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000084000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000840000008400000000000000000000000000000000000000FFFF00FFFF
      FF0000FFFF00FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000FF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000008400000084
      0000000000000000000000000000000000000000000000000000008400000084
      0000000000000000000000000000000000000000000000000000008400000084
      0000000000000000000000000000000000000000000000000000008400000084
      0000000000000000000000000000000000000000000000000000FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000FF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000084000000000000000000000084000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000084000000000000000000000084000000840000000000000000
      000000000000000000000000000000000000000000000000000000FFFF00FFFF
      FF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000FF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000008400000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000008400000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF0000FF
      FF00FFFFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000FF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000008484840000FF
      FF008484840000FFFF0000FFFF00000000000000000000000000000000000000
      000000000000000000000000FF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000FFFF008484
      840000FFFF0000FFFF0000FFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000008484840000FF
      FF008484840000FFFF0000FFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      8400000000008484840000000000000000008484840000000000848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000008484840000000000000000008484
      8400000000008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000FFFF008484
      840000FFFF0000FFFF0000FFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      FF000000FF000000FF000000FF00000000000000FF000000FF000000FF000000
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      8400848484008484840084848400848484008484840084848400848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000000000000000
      0000000000008484840084848400848484008484840084848400FFFFFF000000
      0000848484008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000008400000084
      0000008400000084000000840000008400000084000000840000008400000084
      0000008400000084000000840000000000000000000000000000000000000000
      000000000000000000000000000000000000000000008484840084848400FFFF
      FF00000000008484840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00008484840084848400848484008484840084848400FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840084848400FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FF00000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FF000000FF00000000000000000000000000
      0000000000000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000FF000000FF000000FF00000000000000000000000000
      0000000000000000000000000000000000000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000084000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FF000000FF000000FF000000FF00000000000000000000000000
      0000000000000000000000000000000000000000000000000000008400000084
      0000000000000000000000000000000000000000000000000000008400000084
      0000000000000000000000000000000000000000000000000000000000000000
      FF000000FF000000FF000000FF00000000000000FF000000FF000000FF000000
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000FF000000FF000000FF00000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000084000000000000000000000084000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FF000000FF00000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000008400000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FF00000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400848484008484
      8400848484008484840000000000FFFFFF000000000000000000000000000000
      00000000000000000000FFFFFF00848484000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FFFFFF000000000000000000000000000000
      00000000000000000000FFFFFF00848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840000000000000000000000000000000000848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FFFFFF000000000000000000000000000000
      00000000000000000000FFFFFF00848484000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000000000000000000000000000000000000000
      0000000000008484840000000000000000000000000000000000848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      FF000000FF000000000000000000FFFFFF000000000000000000000000000000
      00000000000000000000FFFFFF00848484000000000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000084000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      FF00000000000000000000000000FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF0000000000FFFFFF00FFFFFF00848484000000000000000000008400000084
      0000000000000000000000000000000000000000000000000000008400000084
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      00000000FF000000000000000000FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF0000000000FFFFFF00FFFFFF00848484000000000000000000000000000000
      0000008400000084000000000000000000000084000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF0000000000FFFFFF00FFFFFF00FFFFFF00000000000000
      0000FFFFFF00FFFFFF00FFFFFF00848484000000000000000000000000000000
      0000000000000000000000840000008400000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000840000008400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      0000000000000000000000000000000000000000000000000000000084000000
      8400000084000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      0000000000000000000000008400000084000000840000008400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      00000000FF000000000000000000000000000000FF00000000000000FF000000
      0000000000008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400000000000000
      8400000084000000840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      FF0000000000000000000000000000000000000000000000FF000000FF000000
      0000000000008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400000084000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      FF000000FF000000000000000000000000000000FF000000FF000000FF000000
      0000000000008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840000000000000000000000000000000000000000000000
      FF000000FF000000FF000000FF00000000000000FF000000FF000000FF000000
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840084848400848484008484
      8400848484008484840084848400848484008484840084848400FFFFFF00C6C6
      C60084848400C6C6C600FFFFFF00C6C6C6000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840084848400848484008484
      840084848400848484008484840084848400848484008484840084848400FFFF
      FF0084848400FFFFFF0084848400848484000000000000000000848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000840000008400000084000000840000008400000000000000
      0000000000000000000000000000000000008400000084000000840000008400
      00000000000000000000848484008484840084848400FFFFFF00FFFFFF00FFFF
      FF00840000008400000084000000840000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000084000000840000008400000084000000840000008400000084000000
      0000000000000000000000000000000000000000000000000000000000008400
      0000FF00000084000000000000000000000084848400FFFFFF00FFFFFF00FFFF
      FF00840000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      8400000084000000840000008400000084000000840000008400000084000000
      8400000000000000000000000000000000000000000000000000000000008400
      000084000000FF0000008400000000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00840000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000084000000
      8400FFFFFF000000840000008400000084000000840000008400FFFFFF000000
      8400000084000000000000000000000000000000000000000000000000008400
      0000FF00000084000000FF00000000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00840000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000008400000084000000
      840000008400FFFFFF00000084000000840000008400FFFFFF00000084000000
      8400000084000000840000000000000000000000000000000000000000008400
      000084000000FF0000008400000000000000FFFFFF00FFFF0000FFFFFF00FFFF
      0000840000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000008484840000000000000000000000000000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000008400000084000000
      84000000840000008400FFFFFF0000008400FFFFFF0000008400000084000000
      8400000084000000840000000000000000000000000000000000000000008400
      0000FF00000084000000FF00000000000000FFFF0000FFFFFF00FFFF0000FFFF
      FF00840000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000008400000084000000
      8400000084000000840000008400FFFFFF000000840000008400000084000000
      8400000084000000840000000000000000000000000000000000000000008400
      000084000000FF0000008400000000000000FFFFFF00FFFF0000FFFFFF00FFFF
      0000840000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000848484000000
      00000000000000000000000000000000000000000000FFFFFF00000000000000
      0000000000000000000000000000000000000000000000008400000084000000
      84000000840000008400FFFFFF0000008400FFFFFF0000008400000084000000
      8400000084000000840000000000000000000000000000000000000000008400
      0000FF00000084000000FF00000000000000FFFF0000FFFFFF00FFFF0000FFFF
      FF00840000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000FFFFFF000000
      00000000000000000000000000000000000000000000FFFFFF00000000000000
      0000000000000000000000000000000000000000000000008400000084000000
      840000008400FFFFFF00000084000000840000008400FFFFFF00000084000000
      8400000084000000840000000000000000000000000000000000000000008400
      0000840000008400000084000000840000008400000084000000840000008400
      0000840000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000008484840000000000000000000000000084848400FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000084000000
      8400FFFFFF000000840000008400000084000000840000008400FFFFFF000000
      8400000084000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000000000008484
      8400FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      8400000084000000840000008400000084000000840000008400000084000000
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000000000000000
      000084848400FFFFFF0084848400FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000084000000840000008400000084000000840000008400000084000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000840000008400000084000000840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000840000008400000084000000840000008400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400848484008484840084848400848484008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008400000084000000840000008400000084000000840000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000FFFF0000FF
      FF0000FFFF0000FFFF0000FFFF000000000000FFFF0000FFFF0000FFFF0000FF
      FF0000FFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000840000008400000084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000000000000000FF
      FF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF0000FF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000840000008400000084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000000000000000FF
      FF0000FFFF0000FFFF0000FFFF000000000000FFFF0000FFFF0000FFFF0000FF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000840000008400000084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000FFFF0000FFFF0000FFFF000000000000FFFF0000FFFF0000FFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000840000008400000084848400000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00FFFFFF000000000000000000FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000FFFF0000FFFF0000000000000000000000000000FFFF0000FFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000840000008400000084848400000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF000000000000000000FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000FFFF0000000000000000000000000000FFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400840000008400000084848400000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      00000000000000000000000000000000000000000000FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000FFFF0000000000000000000000000000FFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008400000084000000840000008400000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF000000
      00000000000000000000000000000000000000000000FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000FFFF000000000000FFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000084848400FFFF
      FF00FFFFFF000000000000000000FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000FFFF0000FFFF0000FFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      8400FFFFFF000000000000000000FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000FFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000840000008400000084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000084848400FFFFFF0084848400FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000FFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000840000008400000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0000000000FFFFFF00FFFFFF00848484000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0000000000FFFFFF00FFFFFF00848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000FFFF0000000000000000000000
      000000000000FFFFFF000000000084848400FFFFFF0000000000000000000000
      000000000000FFFFFF00FFFFFF00848484000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF0000000000000000000000
      000000000000FFFFFF00FFFFFF00848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000FFFF000000
      000000000000FFFFFF00FFFFFF0084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000000000000000FFFFFF00848484000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000000000000000FFFFFF00848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000FFFF000000000000FF
      FF0000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484008484840084848400000000000000000000000000C6C6C600FFFF
      FF00C6C6C600000000000000000000000000C6C6C600FFFFFF00C6C6C6000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484008484840084848400848484000000000084848400848484008484
      84000000000000000000000000000000000000000000C6C6C600FFFFFF000000
      0000000000000000000000000000000000000000000000000000FFFFFF00C6C6
      C600000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400000000000000
      0000000000000000000000000000848484000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400848484008484
      8400000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00000000008484
      840000000000C6C6C600C6C6C600C6C6C600000000008484840000000000FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000C6C6C600FFFFFF000000
      0000000000000000000000000000000000000000000000000000FFFFFF00C6C6
      C600000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000C6C6C600FFFF
      FF00C6C6C600000000000000000000000000C6C6C600FFFFFF00C6C6C6000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484008484840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0000000000FFFFFF00FFFFFF00848484000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0000000000FFFFFF00FFFFFF00848484000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0000000000FFFFFF00FFFFFF00848484000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0000000000FFFFFF00FFFFFF008484840000FFFF0000000000000000000000
      000000000000FFFFFF000000000084848400FFFFFF0000000000000000000000
      000000000000FFFFFF00FFFFFF008484840000FFFF0000000000000000000000
      000000000000FFFFFF000000000084848400FFFFFF0000000000000000000000
      000000000000FFFFFF00FFFFFF008484840000FFFF0000000000000000000000
      000000000000FFFFFF000000000084848400FFFFFF0000000000000000000000
      000000000000FFFFFF00FFFFFF008484840000FFFF0000000000000000000000
      000000000000FFFFFF000000000084848400FFFFFF0000000000000000000000
      000000000000FFFFFF00FFFFFF0084848400000000000000000000FFFF000000
      000000000000FFFFFF00FFFFFF0084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000000000000000FFFFFF0084848400000000000000000000FFFF000000
      000000000000FFFFFF00FFFFFF0084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000000000000000FFFFFF0084848400000000000000000000FFFF000000
      000000000000FFFFFF00FFFFFF0084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000000000000000FFFFFF0084848400000000000000000000FFFF000000
      000000000000FFFFFF00FFFFFF0084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000000000000000FFFFFF00848484000000000000FFFF000000000000FF
      FF0000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000FFFF000000000000FF
      FF0000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000FFFF000000000000FF
      FF0000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000FFFF000000000000FF
      FF0000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000000000000000FF
      000000FF00000000000000FF000000FF00000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000FF00000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FFFF000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484008484840084848400000000000000000000000000000000000000
      000000000000FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000084848400000000000000000000FF000000FF000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FFFF0000FFFF000000000000FFFF00008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000848484008484840000000000000000000000000000000000848484008484
      8400000000000000000000000000000000000000000000000000000000000000
      000000FF00000000000000FF0000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484008484
      840000FF000000FF000000FF000000FF000000FF000000FF0000000000000000
      00000000000000FFFF0000FFFF0000000000000000000000000000000000FFFF
      0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF000000000000000000000000
      0000000000000000000000000000000000000000000084848400848484008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000FF000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400848484008484
      840000000000000000000000000000FF000000FF00000000000000FFFF0000FF
      FF0000FFFF0000000000000000000000000000000000FFFF0000FFFF0000FFFF
      0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000FF000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400000000000000
      000000000000000000000000000000FF00000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400848484008484
      8400848484008484840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000000000848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000000000000000000000
      000084848400000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0000000000FFFFFF00FFFFFF0084848400000000000000000084848400FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0000000000FFFFFF00FFFFFF00848484000000000000000000848484000000
      000000000000FFFF0000FFFF000084848400FFFFFF0000000000000000000000
      000000000000FFFFFF00FFFFFF0084848400000000000000000084848400FFFF
      FF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000000000000000000000000000000000000000FFFFFF00000000000000
      0000FFFFFF000000000000000000000000000000000000000000FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000840000000000000000000084848400FFFFFF0000000000000000000000
      000000000000FFFFFF00FFFFFF00848484000000000084848400000000008484
      0000FFFF0000FFFF0000FFFF000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000000000000000FFFFFF0084848400000000000000000084848400FFFF
      FF00000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000084
      000000840000008400000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000000000000000FFFFFF00848484008484840000000000FFFFFF00FFFF
      0000FFFF0000FFFF0000FFFF000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0084848400000000000000000084848400FFFF
      FF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000000000000000000000000000000000000000FFFFFF00000000000000
      0000FFFFFF000000000000000000000000000000000000000000FFFFFF000000
      0000000000000000000000000000000000000000000000000000008400000084
      000000840000008400000084000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF008484840000000000FFFFFF0084840000FFFF
      0000FFFF00008484840000000000848484008484840084848400848484008484
      840084848400848484008484840084848400000000000000000084848400FFFF
      FF00FFFFFF00FFFFFF000000000000000000000000000000000000000000FFFF
      FF000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      0000000000000000000000000000000000000000000000840000008400000084
      0000000000000084000000840000848484008484840084848400848484008484
      840084848400848484008484840084848400000000008484000084840000FFFF
      0000FFFF000084848400FFFFFF00FFFFFF00FFFFFF00FFFFFF00848484000000
      000000000000000000000000000000000000000000000000000084848400FFFF
      FF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF000000
      0000000000000000000000000000000000000000000000000000008400000000
      0000000000000000000000840000008400000084000000000000000000000000
      0000000000000000000000000000000000000000000084840000FFFFFF00FFFF
      0000FFFF00008484840084848400848484000000000000000000000000000000
      000000000000000000000000000000000000000000000000000084848400FFFF
      FF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF000000000000FFFF00FFFF
      FF000000000000000000000000000000000000000000FFFFFF00FFFFFF000000
      0000FFFFFF00FFFFFF00FFFFFF00000000000000000000000000FFFFFF000000
      0000000000000000000000000000840000000000000000000000000000000000
      0000000000000000000000000000008400000084000000840000000000000000
      00000000000000000000000000000000000000000000FFFFFF00848400008484
      0000FFFF0000FFFF0000FFFF0000FFFF000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000000084848400FFFF
      FF00000000000000000000000000FFFFFF00FFFFFF00FFFFFF0000FFFF00FFFF
      FF000000000000FFFF00000000000000000000000000FFFFFF00000000000000
      000000000000FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000084000000840000000000000000000000000000000000
      0000000000000000000000000000000000000084000000840000008400000000
      0000000000000000000000000000000000008484840000000000848400008484
      0000FFFFFF00FFFF0000FFFF0000FFFF000000000000FFFFFF00FFFFFF00FF00
      0000FFFFFF00FFFFFF00FFFFFF0000000000000000000000000084848400FFFF
      FF0000000000FFFFFF00FFFFFF00FFFFFF0000FFFF008484840000FFFF00FFFF
      FF0000FFFF0000000000000000000000000000000000FFFFFF00FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084000000840000000000000000000000000000000000
      0000000000000000000000000000000000000000000000840000008400000084
      000000000000000000000000000000000000000000008484840000000000FFFF
      FF008484000084840000000000000000000000000000FFFFFF00FF000000FF00
      0000FF000000FFFFFF00FFFFFF0000000000000000000000000084848400FFFF
      FF0000000000FFFFFF00FFFFFF00FFFFFF00848484008484840000FFFF0000FF
      FF00848484008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084000000840000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000008400000084
      0000008400000000000000000000000000000000000000000000848484000000
      00000000000000000000848484000000000000000000FFFFFF00FF000000FFFF
      FF00FF000000FF000000FFFFFF0000000000000000000000000084848400FFFF
      FF0000000000FFFFFF00848484008484840000FFFF00FFFFFF0000FFFF00FFFF
      FF0000FFFF0000FFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084000000840000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000084000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FF000000FFFFFF0000000000000000000000000084848400FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF008484840000FFFF008484
      8400848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084000000840000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000008400000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00000000000000000000000000848484008484
      84008484840084848400848484008484840000FFFF0084848400FFFFFF000000
      000000FFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084000000840000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000FFFF00000000008484840000FFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000840000008400
      0000840000008400000084000000840000008400000084000000840000008400
      0000840000008400000084000000000000000000000000000000840000008400
      0000840000008400000084000000840000008400000084000000840000008400
      0000840000008400000084000000000000000000000000000000000000000000
      0000000000000000000084000000840000008400000084000000840000008400
      0000840000008400000084000000840000000000000000000000000000000000
      000000000000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0084848400000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF008400000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000000000000000000000000
      0000000000000000000084000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00840000000000000000000000000000000000
      000084848400000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0000000000FFFFFF00FFFFFF0084848400000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF008400000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000000000000000000000000
      0000000000000000000084000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00840000000000000000000000848484000000
      000000000000FFFF0000FFFF000084848400FFFFFF0000000000000000000000
      000000000000FFFFFF00FFFFFF0084848400000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF008400000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000000000000000000000000
      0000000000000000000084000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00840000000000000084848400000000008484
      0000FFFF0000FFFF0000FFFF000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000000000000000FFFFFF0084848400000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000000000000840000008400
      0000840000008400000084000000840000008400000084000000840000008400
      0000840000008400000084000000000000008484000084840000848400008484
      0000848400008484000084000000840000008400000084000000840000008400
      0000840000008400000084000000840000008484840000000000FFFFFF00FFFF
      0000FFFF0000FFFF0000FFFF000084848400FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00848484000000000000000000840000008484
      8400FFFFFF0084848400FFFFFF0084848400FFFFFF0084848400FFFFFF008484
      8400FFFFFF00848484008400000000000000000000000000000084000000FFFF
      FF00840000008400000084000000840000008400000084000000840000008400
      00008400000084000000840000000000000084840000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000FFFFFF008400000084000000840000008400
      00008400000084000000840000008400000000000000FFFFFF0084840000FFFF
      0000FFFF00008484840000000000848484008484840084848400848484008484
      840084848400848484008484840084848400000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000000000000840000008400
      0000840000008400000084000000840000008400000084000000840000008400
      00008400000084000000840000000000000084840000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000840000008400000084000000840000008400
      000084000000840000008400000084000000000000008484000084840000FFFF
      0000FFFF000084848400FFFFFF00FFFFFF00FFFFFF00FFFFFF00848484000000
      000000000000000000000000000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF008400000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00840000000000000084840000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0084840000000000000000
      0000000000000000000000000000000000000000000084840000FFFFFF00FFFF
      0000FFFF00008484840084848400848484008484840084848400848484000000
      000000000000000000000000000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF008400000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000008484000084840000848400008484
      0000848400008484000084840000848400008484000084840000848400008484
      00008484000084840000848400000000000000000000FFFFFF00848400008484
      0000FFFF0000FFFF0000FFFF0000FFFF0000FFFF000000000000848484000000
      000000000000000000000000000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF008400000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00840000000000000084840000FFFFFF00848400008484
      0000848400008484000084840000848400008484000084840000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084840000000000008484840000000000848400008484
      0000FFFFFF00FFFF0000FFFF0000FFFF00000000000084848400000000000000
      0000000000000000000000000000000000000000000000000000840000008400
      0000840000008400000084000000840000008400000084000000840000008400
      0000840000008400000084000000000000000000000000000000840000008400
      0000840000008400000084000000840000008400000084000000840000008400
      0000840000008400000084000000000000008484000084840000848400008484
      0000848400008484000084840000848400008484000084840000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF008484000000000000000000008484840000000000FFFF
      FF00848400008484000000000000000000008484840000000000000000000000
      000000000000000000000000000000000000000000000000000084000000FFFF
      FF00840000008400000084000000840000008400000084000000840000008400
      000084000000840000008400000000000000000000000000000084000000FFFF
      FF00840000008400000084000000840000008400000084000000840000008400
      0000840000008400000084000000000000000000000000000000000000000000
      00000000000084840000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084840000000000000000000000000000848484000000
      0000000000000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000840000008400
      0000840000008400000084000000840000008400000084000000840000008400
      0000840000008400000084000000000000000000000000000000840000008400
      0000840000008400000084000000840000008400000084000000840000008400
      0000840000008400000084000000000000000000000000000000000000000000
      0000000000008484000084840000848400008484000084840000848400008484
      0000848400008484000084840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000084840000FFFFFF00848400008484000084840000848400008484
      0000848400008484000084840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484000084840000848400008484000084840000848400008484
      0000848400008484000084840000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00000000000000000000000000000000000000000000000000840000008400
      0000840000008400000084000000840000008400000084000000840000008400
      0000840000008400000084000000000000000000000000000000000000000000
      0000000000008400000084000000840000008400000084000000840000008400
      0000840000008400000084000000000000000000000000000000848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484000000000000000000000000008484
      8400000000000000000084848400FFFFFF00FF000000FF000000FF000000FFFF
      FF0000000000000000000000000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0084000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000000000000000000000000
      00000000000084000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000084848400000000000000
      0000000000000000840000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000000000008484
      8400FFFFFF00FFFFFF0084848400FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0000000000000000000000000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0084000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000000000000000000000000
      00000000000084000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000000000000848484000000
      00000000FF00000084000000840000000000000000000000FF000000FF000000
      FF000000FF000000FF0000000000000000008484840000000000000000008484
      8400FFFFFF00FF00000084848400FFFFFF00FF000000FF000000FF000000FFFF
      FF0000000000000000000000000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0084000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000000000000000000000000
      00000000000084000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000000000000000000000000
      FF000000FF0000008400000084000000840000000000000000000000FF000000
      FF000000FF000000FF00000000000000000084848400FFFFFF00FFFFFF008484
      8400FFFFFF00FFFFFF0084848400FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0000000000000000000000000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0084000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000000000000000000008400
      0000840000008400000084000000840000008400000084000000840000008400
      00008400000084000000840000000000000000000000000000000000FF000000
      FF000000FF00000084000000840000008400000000000000FF000000FF000000
      FF000000FF000000FF00000000000000000084848400FFFFFF00FF0000008484
      8400FFFFFF00FF00000084848400FFFFFF00FF000000FF000000000000000000
      000000000000000000000000000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0084000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000000000000000000008400
      0000FFFFFF0084000000FFFFFF00840000008400000084000000840000008400
      000084000000840000008400000000000000000000000000FF000000FF000000
      FF000000FF000000840000008400000084000000FF000000FF000000FF000000
      FF000000FF000000FF00000000000000000084848400FFFFFF00FFFFFF008484
      8400FFFFFF00FFFFFF0084848400FFFFFF00FFFFFF00FFFFFF00000000000000
      000000000000000000000000000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0084000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000000000000000000008400
      0000FFFFFF008400000084000000840000008400000084000000840000008400
      00008400000084000000840000000000000000000000000000000000FF000000
      FF000000FF0000008400000084000000FF000000FF000000FF000000FF000000
      FF00000000000000FF00000000000000000084848400FFFFFF00FF0000008484
      8400FFFFFF00FF00000084848400848484008484840084848400848484000000
      000000000000000000000000000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0084000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000000000000000000008400
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00840000000000000000000000000000000000000084848400000000000000
      FF0000000000000084000000FF000000FF000000FF000000FF000000FF000000
      00000000000000000000000000000000000084848400FFFFFF00FFFFFF008484
      8400FFFFFF00FFFFFF00FFFFFF00000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0084000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000084000000840000008400
      0000840000008400000084000000840000008400000084000000840000008400
      0000840000000000000000000000000000008484840000000000000000000000
      0000000000000000FF000000FF000000FF000000FF000000FF00000000008484
      84000000000000000000000000000000000084848400FFFFFF00FF0000008484
      840084848400848484008484840084848400FFFFFF0084848400848484008484
      840084848400848484008484840000000000000000000000000084000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0084000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084000000000000000000000084000000FFFFFF008400
      0000FFFFFF008400000084000000840000008400000084000000840000008400
      0000840000000000000000000000000000000000000000000000000000000000
      000000000000000000000000FF000000FF000000FF0000000000000000000000
      00008484840000000000000000000000000084848400FFFFFF00FFFFFF00FFFF
      FF0000000000000000000000000000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000840000008400
      0000840000008400000084000000840000008400000084000000840000008400
      0000840000008400000084000000000000000000000084000000FFFFFF008400
      0000840000008400000084000000840000008400000084000000840000008400
      0000840000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000FF000000000000000000000000000000
      0000000000008484840000000000000000008484840084848400848484008484
      840084848400000000000000000000000000FFFFFF00C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C6008484840000000000000000000000000084000000FFFF
      FF008400000084000000840000008400000084000000FFFFFF00840000008400
      0000840000008400000084000000000000000000000084000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00840000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484000000000084848400000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084848400000000000000000000000000840000008400
      0000840000008400000084000000840000008400000084000000840000008400
      0000840000008400000084000000000000000000000084000000840000008400
      0000840000008400000084000000840000008400000084000000840000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      8400000000000000000000000000848484000000000000000000000000000000
      0000000000000000000000000000000000008400000084000000840000008400
      0000840000008400000084000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084000000FFFFFF008400
      0000840000008400000084000000840000008400000084000000840000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008400000084000000840000008400
      0000840000008400000084000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084000000840000008400
      0000840000008400000084000000840000008400000084000000840000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000008400FFFFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084000000840000008400000084000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      8400FFFFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000000000000000
      0000848484000000000000000000000000000000000000000000000000000000
      0000840000008400000084000000840000008400000084000000840000008400
      0000000000000000000000000000000000000000000000000000000084000000
      840000008400FFFFFF0000000000000000000000000000000000000000000000
      000000008400FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00000000008484840000000000000000008484
      8400000000008484840000000000000000000000000000000000000000008400
      0000840000008400000000000000000000000000000000000000840000008400
      0000840000000000000000000000000000000000000000000000000084000000
      840000008400FFFFFF0000000000000000000000000000000000000000000000
      8400FFFFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400848484000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000848484000000000000000000FFFF00008484
      8400848484000000000000000000000000000000000000000000000000008400
      0000840000000000000000000000000000000000000000000000000000008400
      0000840000000000000000000000000000000000000000000000000000000000
      84000000840000008400FFFFFF00000000000000000000000000000084000000
      8400FFFFFF000000000000000000000000000000000000000000000000000000
      000000000000848484008484840000000000FFFFFF00FFFFFF00FFFFFF000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000008484
      8400000000000000000000000000000000000000000000000000840000008400
      0000000000000000000000000000000000000000000000000000000000000000
      0000840000008400000000000000000000000000000000000000000000000000
      0000000084000000840000008400FFFFFF00000000000000840000008400FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000008484840000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF000000000000000000FFFF000000000000000000008484
      8400000000000000000000000000000000000000000000000000840000008400
      0000000000000000000000000000000000000000000000000000000000000000
      0000840000008400000000000000000000000000000000000000000000000000
      0000000000000000840000008400000084000000840000008400FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840000000000848484008484840084848400848484008484
      84000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF000000000084848400FFFF0000FFFF0000000000008484
      8400848484000000000000000000000000000000000000000000840000008400
      0000000000000000000000000000000000000000000000000000000000000000
      0000840000008400000000000000000000000000000000000000000000000000
      00000000000000000000000084000000840000008400FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00000000008484840000000000000000008484
      8400000000000000000000000000000000000000000000000000840000008400
      0000000000000000000000000000000000000000000000000000000000000000
      0000840000008400000000000000000000000000000000000000000000000000
      0000000000000000840000008400000084000000840000008400FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00FFFFFF00FFFFFF000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008400
      0000840000000000000000000000000000000000000084000000000000008400
      0000840000000000000000000000000000000000000000000000000000000000
      0000000084000000840000008400FFFFFF000000000000008400FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000008400
      0000840000000000000000000000000000000000000084000000840000008400
      0000840000000000000000000000000000000000000000000000000084000000
      84000000840000008400FFFFFF00000000000000000000000000000084000000
      8400FFFFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084000000840000008400
      0000000000000000000000000000000000000000000000008400000084000000
      840000008400FFFFFF0000000000000000000000000000000000000000000000
      840000008400FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084000000840000008400
      000084000000000000000000000000000000000000000000840000008400FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      00000000840000008400FFFFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840000000000000000000000000000000000008484000084
      8400008484000084840000848400008484000084840000848400008484000084
      8400008484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000C6C6
      C600C6C6C60084008400C6C6C600C6C6C600C6C6C600C6C6C60084008400C6C6
      C600000000008484840000000000000000000000000000000000008484000084
      8400008484000084840000848400008484000084840000848400008484000084
      8400008484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008400
      8400840084008400840084008400840084008400840084008400840084008400
      8400000000008484840000000000000000000000000000000000008484000084
      8400008484000084840000848400008484000084840000848400008484000084
      8400008484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000C6C6
      C600C6C6C60084008400C6C6C600C6C6C600C6C6C600C6C6C60084008400C6C6
      C600000000008484840000000000000000000000000000000000008484000084
      8400008484000084840000848400008484000084840000848400008484000084
      8400008484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000C6C6
      C600C6C6C60084008400C6C6C600C6C6C600C6C6C600C6C6C60084008400C6C6
      C600000000008484840000000000000000000000000000000000008484000084
      8400008484000084840000848400008484000084840000848400008484000084
      8400008484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840000000000000000000000
      000000000000000000000000000000000000000000000000000000000000C6C6
      C600C6C6C60084008400C6C6C600C6C6C600C6C6C600C6C6C60084008400C6C6
      C600000000008484840000000000000000000000000000000000008484000084
      8400008484000084840000848400008484000084840000848400008484000084
      8400008484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000C6C6
      C600C6C6C60084008400C6C6C600C6C6C600C6C6C600C6C6C60084008400C6C6
      C600000000008484840000000000000000000000000000000000008484000084
      8400008484000084840000848400008484000084840000848400008484000084
      8400008484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400000000000000
      000000000000000000000000000000000000000000000000000000000000C6C6
      C600C6C6C60084008400C6C6C600C6C6C600C6C6C600C6C6C60084008400C6C6
      C600000000008484840000000000000000000000000084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000C6C6
      C600C6C6C60084008400C6C6C600C6C6C600C6C6C600C6C6C60084008400C6C6
      C60000000000848484000000000000000000000000000000000000000000FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000840000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      000000000000000000000000000000000000000000000000000000000000C6C6
      C600C6C6C60084008400C6C6C600C6C6C600C6C6C60000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000840000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008400
      8400840084008400840084008400840084008400840000000000C6C6C6000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000840000000000000084000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000C6C6
      C600C6C6C60084008400C6C6C600C6C6C600C6C6C60000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000840000008400000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000840000008400000084000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008400
      0000840000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000840000008400000084000000840000008400
      0000840000008400000084000000840000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000840000008400000084000000840000008400
      0000840000008400000084000000840000000000000000000000840000000000
      0000000000008400000000000000000000008400000084000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000084000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00840000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000084000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00840000000000000000000000840000000000
      0000000000008400000000000000840000000000000000000000840000000000
      0000000000000000000000000000000000000000000000000000008484008484
      840000848400848484000084840084000000FFFFFF0084000000840000008400
      00008400000084000000FFFFFF00840000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000084000000FFFFFF0000000000000000000000
      00000000000000000000FFFFFF00840000000000000000000000840000000000
      0000000000008400000000000000840000000000000000000000840000000000
      0000000000000000000000000000000000000000000000000000848484000084
      840084848400008484008484840084000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00840000000000000000000000000000008400
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000084000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00840000000000000000000000000000008400
      0000840000008400000000000000840000000000000000000000840000000000
      0000000000000000000000000000000000000000000000000000008484008484
      840000848400848484000084840084000000FFFFFF0084000000840000008400
      0000FFFFFF008400000084000000840000000000000000000000848484008400
      0000000000000000000000000000000000000000000084000000840000008400
      0000840000008400000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0084000000FFFFFF0000000000000000000000
      00000000000000000000FFFFFF00840000000000000000000000000000000000
      0000000000008400000000000000840000008400000084000000000000000000
      0000000000000000000000000000000000000000000000000000848484000084
      840084848400008484008484840084000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF0084000000FFFFFF00840000000000000000000000840000000000
      0000000000000000000000000000000000000000000000000000840000008400
      0000840000008400000000000000000000000000000000000000FFFFFF000000
      000000000000000000000000000084000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00840000000000000000000000000000000000
      0000000000008400000000000000840000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000008484008484
      840000848400848484000084840084000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF008400000084000000000000000000000000000000840000000000
      0000000000000000000000000000000000000000000000000000000000008400
      0000840000008400000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0084000000FFFFFF000000000000000000FFFF
      FF00840000008400000084000000840000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000084
      8400848484000084840084848400840000008400000084000000840000008400
      0000840000008400000000000000000000000000000000000000840000000000
      0000000000000000000000000000000000000000000000000000840000000000
      0000840000008400000000000000000000000000000000000000FFFFFF000000
      000000000000000000000000000084000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0084000000FFFFFF0084000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000008484008484
      8400008484008484840000848400848484000084840084848400008484008484
      8400008484008484840000000000000000000000000000000000848484008400
      0000000000000000000000000000000000008400000084000000000000000000
      0000000000008400000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0084000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00840000008400000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000084
      8400000000000000000000000000000000000000000000000000000000000000
      0000848484000084840000000000000000000000000000000000000000008484
      8400840000008400000084000000840000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF000000
      000000000000FFFFFF0000000000840000008400000084000000840000008400
      0000840000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000008484008484
      8400000000000000000000000000000000000000000000000000000000000000
      0000008484008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000084
      8400848484000000000000FFFF00000000000000000000FFFF00000000000084
      8400848484000084840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000FFFF0000FFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000848400008484000000
      0000000000000000000000000000FFFFFF000000000000848400000000000000
      0000000000000000000000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000848400008484000000
      0000000000000000000000000000FFFFFF000000000000848400000000000000
      0000000000000000000000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000848400008484000000
      0000000000000000000000000000000000000000000000848400000000000084
      8400000000000000000000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      8400840000000000000000000000000000000000000000848400008484000084
      8400008484000084840000848400008484000084840000848400000000000084
      8400000000000000000000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00000000000000000000000000000000000000000000000000FFFFFF000000
      0000000000000000000000000000000000000000000000000000840000008400
      0000840000008400000084000000000000000000000000000000000000000000
      0000840000008484840000000000000000000000000000848400008484000000
      0000000000000000000000000000000000000084840000848400000000000084
      8400000000000084840000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00000000000000000000000000000000000000000000000000FFFFFF000000
      0000000000000000000000000000000000000000000000000000840000008400
      0000840000008400000000000000000000000000000000000000000000000000
      000000000000840000000000000000000000000000000084840000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000848400000000000084
      8400000000000084840000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00000000000000000000000000000000000000000000000000FFFFFF000000
      0000000000000000000000000000000000000000000000000000840000008400
      0000840000000000000000000000000000000000000000000000000000000000
      000000000000840000000000000000000000000000000084840000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000848400000000000084
      8400000000000084840000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000840000008400
      0000000000008400000000000000000000000000000000000000000000000000
      000000000000840000000000000000000000000000000084840000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000000000000084
      8400000000000084840000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF000000000000000000000000000000000000000000FFFFFF000000
      0000000000000000000000000000000000000000000000000000840000000000
      0000000000000000000084000000840000000000000000000000000000000000
      000084000000848484000000000000000000000000000084840000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00000000000084
      8400000000000084840000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084848400FFFFFF00000000000000000000000000C6C6
      C6000000000000000000C6C6C600000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008400000084000000840000008400
      0000848484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000084840000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF008484840000000000000000000000000000000000C6C6
      C6000000000000000000C6C6C600000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      840000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000FFFF
      FF00000000000084840000000000000000000000000084848400848484008484
      840084848400848484008484840000000000000000000000000000000000C6C6
      C600C6C6C600C6C6C600C6C6C600000000000000000000000000000000000000
      000000000000FFFFFF000000000000000000000000000000000000000000FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000C6C6
      C600C6C6C600C6C6C600C6C6C600000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000084840000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0000000000FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000000000000084
      8400008484000084840000848400008484000084840000848400008484000084
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      8400008484000000000000000000000000000000000000000000000000000000
      0000000000000000000000848400000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000FFFFFF000000
      0000008484000084840000848400008484000084840000848400008484000084
      8400008484000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      8400008484000000000000000000000000000000000000000000000000000000
      0000000000000000000000848400000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00000000000000000000000000000000000000000000FFFF00FFFF
      FF00000000000084840000848400008484000084840000848400008484000084
      8400008484000084840000000000000000000000000000000000000000000000
      000000000000000000000000000000FFFF0000FFFF0000FFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      8400008484000000000000000000000000000000000000000000000000000000
      0000000000000000000000848400000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000FFFFFF0000FF
      FF00FFFFFF000000000000848400008484000084840000848400008484000084
      8400008484000084840000848400000000000000000000000000000000000000
      0000000000000000000000000000848484008484840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      8400008484000000000000000000000000000000000000000000000000000000
      0000000000000000000000848400000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00000000000000000000000000000000000000000000FFFF00FFFF
      FF0000FFFF00FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      8400008484000084840000848400008484000084840000848400008484000084
      8400008484000084840000848400000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      8400008484000000000000000000000000000000000000000000000000000000
      0000000000000084840000848400000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00000000000000000000000000000000000000000000FFFF00FFFF
      FF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000848400000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000FFFFFF0000FF
      FF00FFFFFF000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000084
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000848400000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF000000000000000000000000000000000000000000FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000084
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000848400000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00000000000000000000000000000000000000000000000000000000000084
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000848400000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FFFFFF000000000000000000000000000000000000000000FFFF
      FF00000000000000000000000000000000000000000000000000000000000084
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000000000000084
      8400000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000008484000084840000848400008484000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000084000000840000008400000084
      0000008400000084000000840000008400000000000000000000000000000000
      0000000000008484000084840000848400000084000000840000008400000084
      0000008400000084000000840000008400000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000084000000840000008400000084
      0000008400000084000000840000008400000000000000000000000000000000
      0000000000008484000084840000848400000084000000840000008400000084
      0000008400000084000000840000008400000000000000000000FF0000000000
      0000FF000000FF00000000000000FF00000000000000FF00000000000000FF00
      000000000000FF00000000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000084848400848484008484
      8400848484008484840000000000000000000000000000000000848484000084
      0000008400008484840000000000000000000000000000000000000000000000
      0000000000008484000084840000848400008484000084840000000000000084
      000000840000000000000000000000000000000000000000000000000000FF00
      00000000000000000000FF00000000000000FF00000000000000FF000000FF00
      0000FF0000000000000000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000084848400848484008484
      8400848484008484840000000000000000000000000000000000848484000084
      0000008400008484840000000000000000000000000000000000000000000000
      0000000000000000000084840000848400008484000084840000000000000084
      000000840000000000000000000000000000000000000000000000000000FF00
      0000000000000000000000000000FF00000000000000FF000000FF0000000000
      0000000000000000000000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      00000000000000000000000000000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000000000000000000000C6C6C6000084
      000000840000C6C6C60000000000000000000000000000000000000000000000
      0000000000000000000000000000848400008484000084840000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FF000000FF00000000000000FF0000000000
      0000000000000000000000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      00000000000000000000000000000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000000000000000000000000000000000
      0000000000000000000000000000848400008484000084840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FF00000000000000FF000000000000000000
      0000000000000000000000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000848400008484000084840000848400000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FF00000000000000000000000000
      0000000000000000000000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084848400000000000000000000000000000000000000
      00000000000000000000000000000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000000000000000000000000000000000
      0000848400008484000084840000848400008484000084840000848400008484
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FF00000000000000000000000000
      0000000000000000000000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0084848400FFFFFF00000000000000000000000000C6C6
      C6000000000000000000C6C6C600000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848400008484000084840000848400008484000084840000848400000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000FF00000000000000000000000000
      0000000000000000000000000000000000000000000084848400FFFFFF00FFFF
      FF00FFFFFF00FFFFFF008484840000000000000000000000000000000000C6C6
      C6000000000000000000C6C6C600000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848400008484000084840000848400008484000084840000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000084848400848484008484
      840084848400848484008484840000000000000000000000000000000000C6C6
      C600C6C6C600C6C6C600C6C6C600000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484000084840000848400008484000084840000848400000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000FF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000C6C6
      C600C6C6C600C6C6C600C6C6C600000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000FF000000FF000000FF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000FF000000FF000000FF000000FF000000FF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000084000000840000008400000084
      0000008400000084000000840000008400000000000000000000000000000000
      0000000000000000000000000000000000000084000000840000008400000084
      0000008400000084000000840000008400000000000000000000000000000000
      0000000000000000000000000000000000000084000000840000008400000084
      0000008400000084000000840000008400000000000000000000000000000000
      0000C6C6C600C6C6C600C6C6C600C6C6C6000084000000840000008400000084
      0000008400000084000000840000008400000000000000000000000000000000
      0000000000000000000000000000000000000084000000840000008400000084
      0000008400000084000000840000008400000000000000000000000000000000
      0000000000000000000000000000000000000084000000840000008400000084
      0000008400000084000000840000008400000000000000000000000000000000
      0000000000000000000000000000000000000084000000840000008400000084
      0000008400000084000000840000008400000000000000000000000000000000
      0000000000000000000000000000000000000084000000840000008400000084
      0000008400000084000000840000008400000000000084848400848484008484
      8400848484008484840084848400848484008484840084848400848484000084
      0000008400008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000084848400848484008484
      8400848484008484840084848400848484008484840084848400848484000084
      00000084000084848400000000000000000000000000C6C6C600C6C6C600C6C6
      C600000000000000000084848400848484008484840000000000000000000084
      0000008400000000000000000000000000000000000084848400848484008484
      8400848484008484840084848400848484008484840084848400848484000084
      0000008400008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000084
      0000008400000000000000000000000000000000000084848400848484008484
      8400848484008484840084848400848484008484840084848400848484000084
      00000084000084848400000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF0000000000C6C6C600C6C6C600C6C6C600C6C6C60084848400000000000084
      00000084000000000000000000000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C6000084
      000000840000C6C6C60000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00FFFFFF00FFFFFF0000000000000000000084
      00000084000000000000000000000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C6000000000000000000C6C6C600C6C6C6000084
      000000840000C6C6C60000000000000000000000000000000000000000000000
      0000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600848484000084
      00000084000000000000000000000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000008484840084848400848484008484
      84008484840000000000FFFFFF00FFFFFF00FFFFFF0000000000848484008484
      84008484840084848400848484000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C6000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000000000000000000000000000000000
      0000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600848484000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000000000
      0000C6C6C600FFFFFF00C6C6C600C6C6C600C6C6C600C6C6C600848484000000
      0000C6C6C600C6C6C600000000000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C6000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000000000000000000000000000000000
      000000000000FFFFFF00FFFFFF00C6C6C600C6C6C600C6C6C60000000000FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000C6C6C600C6C6C600C6C6C60000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FF0000000000000000000000000000000000
      0000000000000000FF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000FF000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000084848400FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000084848400FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000000084848400FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484008484
      8400FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000008484
      840084848400FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF008484840000000000000000000000000000000000848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      00008484840000000000000000000000000000000000FFFFFF00848484000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF0084848400000000000000000000000000C6C6C600848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      00008484840084848400000000000000000000000000FFFFFF00848484000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00848484000000000000000000C6C6C600C6C6C600848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF0084848400848484000000000000000000FFFFFF00848484000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000C6C6C600848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF008484840000000000C6C6C600C6C6C600C6C6C600848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00C6C6C600848484008484840000000000FFFFFF00848484000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF0000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00000000000000000000000000000000000000
      000000000000000000000000000000000000FFFFFF00C6C6C600848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF0000000000FFFFFF00C6C6C600C6C6C600C6C6C600848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00C6C6C600C6C6C600848484008484840000000000848484000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF000000
      000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0000000000FFFFFF00FFFFFF00000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF008484840000000000FFFFFF00C6C6C600C6C6C600848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00C6C6C600C6C6C600C6C6C60000000000FFFFFF00848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF0000000000FFFFFF00FFFFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00848484000000000000000000FFFFFF00C6C6C600848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00C6C6C600C6C6C6000000000000000000FFFFFF00848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF000000000000000000FFFFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF0084848400000000000000000000000000FFFFFF00848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00C6C6C600000000000000000000000000FFFFFF00848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF008484840000000000000000000000000000000000848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF0000000000000000000000000000000000FFFFFF00848484000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00FFFFFF000000000000000000FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000FFFFFF00848484000000
      000000000000000000000000000000000000000000000000000000000000FFFF
      FF00FFFFFF000000000000000000FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000FFFFFF00FFFFFF0000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000000000000000FF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      FF000000FF000000FF000000FF00000000000000FF000000FF000000FF000000
      FF000000FF000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840084848400848484008484
      8400848484000000000000000000000000000000000000000000848484008484
      84008484840084848400848484000000000000000000000000000000FF000000
      000000000000000000000000000000000000000000000000FF000000FF000000
      0000000000000000FF0000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FFFFFF0084848400000000000000000000000000000000000000
      00000000000000000000000000000000000084848400C6C6C600C6C6C600C6C6
      C60084848400000000000000000000000000000000000000000084848400C6C6
      C600C6C6C600C6C6C600848484000000000000000000000000000000FF000000
      00000000000000000000000000000000000000000000000000000000FF000000
      0000000000000000FF0000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00C6C6C6008484840084848400000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FFFFFF0084848400848484000000000000000000000000000000
      00000000000000000000000000000000000084848400C6C6C600C6C6C600C6C6
      C60084848400000000000000000000000000000000000000000084848400C6C6
      C600C6C6C600C6C6C600848484000000000000000000000000000000FF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF0000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00C6C6C600C6C6C60084848400848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FFFFFF00C6C6C600000000000000000000000000000000000000
      00000000000000000000000000000000000084848400C6C6C600C6C6C600C6C6
      C60084848400000000000000000000000000000000000000000084848400C6C6
      C600C6C6C600C6C6C600848484000000000000000000000000000000FF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000FF0000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00C6C6C600C6C6C600C6C6C600000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840084848400848484008484
      8400848484000000000000000000000000000000000000000000848484008484
      8400848484008484840084848400000000000000000000000000000000000000
      FF000000FF000000FF000000FF000000FF000000FF000000FF000000FF000000
      FF000000FF000000000000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00C6C6C600C6C6C60000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00C6C6C6000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484008484840000000000000000000000000084848400848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF0084848400000000000000000000000000FFFFFF00848484000000
      0000000000000000000000000000000000000000000084848400848484008484
      8400848484008484840000000000000000000000000000000000848484008484
      8400848484008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF0084848400000000000000000000000000FFFFFF00848484000000
      0000000000000000000000000000000000000000000084848400848484008484
      8400848484008484840000000000000000000000000000000000848484008484
      8400848484008484840000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840084848400848484008484
      8400848484000000000000000000000000000000000000000000848484008484
      8400848484008484840084848400000000000000000000000000000000000000
      0000FFFFFF0084848400000000000000000000000000FFFFFF00848484000000
      00000000000000000000000000000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000000000000000000000C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      00000000000000000000000000000000000084848400C6C6C600C6C6C600C6C6
      C60084848400000000000000000000000000000000000000000084848400C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000FFFFFF0084848400000000000000000000000000FFFFFF00848484000000
      00000000000000000000000000000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000008484840084848400848484008484
      84008484840000000000FFFFFF00FFFFFF00FFFFFF0000000000848484008484
      84008484840084848400848484000000000084848400C6C6C600C6C6C600C6C6
      C60084848400000000000000000000000000000000000000000084848400C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000FFFFFF0084848400000000000000000000000000FFFFFF00848484000000
      00000000000000000000000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000000000
      00000000000000000000FFFFFF00FFFFFF00FFFFFF0000000000000000000000
      00000000000000000000000000000000000084848400C6C6C600C6C6C600C6C6
      C60084848400000000000000000000000000000000000000000084848400C6C6
      C600C6C6C600C6C6C60084848400000000000000000000000000000000000000
      0000FFFFFF0084848400000000000000000000000000FFFFFF00848484000000
      00000000000000000000000000000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000008484840084848400848484008484
      8400848484000000000000000000000000000000000000000000848484008484
      8400848484008484840084848400000000000000000000000000000000000000
      0000FFFFFF0084848400000000000000000000000000FFFFFF00848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF0084848400000000000000000000000000FFFFFF00848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF0084848400000000000000000000000000FFFFFF00848484000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00000000000000000000000000FFFFFF00FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000FFFFFF0000000000848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C6000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0000000000FFFFFF0000000000848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000000000000000000000000000C6C6C600000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00848400008484000084840000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF0000000000FFFFFF0000000000848484000000000084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840000000000000000000000000084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      84008484840084848400000000000000000000000000C6C6C600C6C6C600C6C6
      C600000000000000000084848400848484008484840000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF008484000084840000848400008484000084840000FFFFFF00FFFFFF00FFFF
      FF0000000000FFFFFF0000000000848484000000000084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840000000000000000000000000084848400848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      84008484840084848400000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF0000000000C6C6C600C6C6C600C6C6C600C6C6C60084848400000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF008484000084840000C6C6C600848400008484000084840000FFFFFF00FFFF
      FF0000000000FFFFFF00000000008484840000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600000000000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C6000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000000000000000000000000000000000
      0000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600848484000000
      0000000000000000000000000000000000000000000000000000848400008484
      00008484000084840000C6C6C600C6C6C6008484000084840000848400008484
      000000000000FFFFFF00000000008484840000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600000000000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C6000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000000000000000000000000000000000
      0000C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600848484000000
      0000000000000000000000000000000000000000000000000000848400008484
      00008484000084840000C6C6C600C6C6C6008484000084840000848400008484
      000000000000FFFFFF00000000008484840000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00000000000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF000000000000000000FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000000000000000
      0000C6C6C600FFFFFF00C6C6C600C6C6C600C6C6C600C6C6C600848484000000
      0000C6C6C600C6C6C60000000000000000000000000000000000FFFFFF00FFFF
      FF008484000084840000C6C6C6008484000084840000FFFF0000FFFF0000FFFF
      000000000000FFFFFF00000000008484840000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600000000000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C600C6C6C6000000000000000000C6C6C600C6C6C600C6C6
      C600C6C6C600C6C6C60000000000000000000000000000000000000000000000
      000000000000FFFFFF00FFFFFF00C6C6C600C6C6C600C6C6C60000000000FFFF
      FF00FFFFFF00FFFFFF0000000000000000000000000000000000FFFFFF00FFFF
      FF008484000084840000848400008484000084840000FFFF0000FFFF0000FFFF
      000000000000FFFFFF0000000000848484000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      00000000000000000000C6C6C600C6C6C600C6C6C60000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF0084840000848400008484000084840000FFFF0000FFFF0000FFFF0000FFFF
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF008484000084840000FFFF0000FFFF0000FFFF0000FFFF
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF00FFFFFF000000000084848400000000000000000000000000848484008484
      8400848484008484840084848400848484008484840084848400848484008484
      8400848484008484840084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF00FFFFFF000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF00FFFFFF000000000084848400000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF00FFFFFF000000000084848400000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF00FFFFFF000000000084848400000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF00FFFFFF000000000084848400000000000000000000000000FFFFFF00FFFF
      FF0000000000FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF0000000000FFFF
      FF00FFFFFF000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000084848400000000000000000000000000FFFFFF00FFFF
      FF0000000000FFFFFF00FFFFFF0000000000FFFFFF00FFFFFF0000000000FFFF
      FF00FFFFFF000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF00FFFFFF000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF00FFFFFF000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000FFFFFF00FFFF
      FF00FFFFFF0000000000FFFFFF00FFFFFF00FFFFFF0000000000FFFFFF00FFFF
      FF00FFFFFF000000000084848400000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      000000000000000000000000000000000000424D3E000000000000003E000000
      2800000040000000A00100000100010000000000000D00000000000000000000
      000000000000000000000000FFFFFF00FFFFFFFF00000000FFFFFFFF00000000
      8001E0FF00000000BEFDC07F000000008001C07F00000000BFFDC07F00000000
      BF75C07F00000000BE21C07F00000000BF75E0FF00000000DF73FFFF00000000
      EF4FFFDF00000000F73FFF8F00000000FA7FFF2700000000FDFFFFF300000000
      FFFFFFF900000000FFFFFFFF00000000FFFFFFFFFFFFFFFFFFFFC1FFFFFFFFFF
      E0FF80FF80018001C07F80FFBEFDBEFDC07F80FF80018001C07F80FFBFFDBFFD
      C07F8003B07DB8EDC07FC001BDFDB9DDE0FFFC01B8FDBA3DFF39BC01D073DFF3
      FF93AC01EDCFEFCFFFC7C401F5BFF7BFFFC7EC01F87FFA7FFF93FC01FDFFFDFF
      FF39FE03FFFFFFFFFFFFFFFFFFFFFFFFFFE3FFFFFFFFFFFFEFDDFFFFFFFF800F
      EFA7800180018007EF43BEFDBEFD8003E180800180018001FDC3BFFDBFFD8000
      FDBEBFF3BFF38000FDC1CFCFCFCF800FFDFFF33FF33F800FFDFFFCFFFCFF8001
      FDFFFFFFFFFFC780F007FFEFFBFFFF80F77FFFE7F3FFED80C77FEA03E02BF080
      FFFFFFE7F3FFFD80FFFFFFEFFBFFFFC1FFFFFFC7FFFFFFFFFFFFFFBBE10F8001
      E01FFF4FFEFFBEFDC00FFE86FC7F8001E007FF01F83FBFFDF003FF87FEFF8001
      8001FF7DFEFFFEFFC003FF838001FEFFE01FFFFFBEFDFEFF000FFF7F8001F83F
      8007FE7FBFFDFC7FC03FFC7FBFF3FEFFE01F502ACFCFE10FF00FFC7FF33FFFFF
      FFFFFE7FFCFFFFFFFFFFFF7FFFFFFFFFFFFFFFC7FC00FFFFFEFFFFBBFC008001
      FC7FFF4F8000BFFDFCBFFE8600008001FADFFF017C00BFFDFADFFF874400BFF3
      F6EFFF7D4C00CFCFF6F77D835400F33FEC7B7DFF7800FCFFEEF981FF7C00FEFF
      DFC7BBFF7800FEFFDC3FBBFF5753F83FA3FFD6034F93FC7F9FFFD7FF4713FEFF
      FFFFD7FF7FF3E10FFFFFEFFF0007FFFFFFFFFFFF0000FFFFFFFBFFFF0000C001
      FFF1F83F00008001FFE3F01FE007BFF9FFC7E00FE007A389F08FC007E007A7C9
      C01F8003E007ABA9C03F8003E007BD79801F8003E007BFF9801F8003E007BD79
      801F8003E007ABA9801FC007FFFFA7C9C03FE00FF81FA389C03FF01FF81FBFF9
      F0FFF83FF81F8003FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFE0038555FFFFFFFB
      C001DFFFFC0FFFF180018DFDF81FFFE38003DFFFFE3FFFC7C007DDFDFE3FF08F
      C007DFFFFE3FC01FE00FDDFDFE3FC03FE00FDFFFFE3F801FF01FDD55FC3F801F
      F01FFFFFF87F801FF83F8FFDFFFF801FF83FF7F5FF3FC03FFC7FF401FE3FC03F
      FC7FF7F5FE7FF0FFFEFFFFFFFFFFFFFFDE00FE00FFFFFFFFCE00FE00F555FFFF
      E600FE00FFFFFFF77200FE00F7FDFFE35800FE00FFFFFFC78600FE00F7FDF18F
      FE00FE00FFFFCE1FFFF0C71FF7FDBFBFF000800FF7FFBCBF00000007F7FD3EDF
      000F0007FFFF3FDF0F7F000714553FDFFB6F800FFFFF9FBFF007C71FF7FF8FBF
      FB6FFFFFF7FFC07FFF7FFFFFF7FFF1FFDE00DE00DE00DE00CE00CE00CE00CE00
      E600E600E600E600720072007200720058005800580058008600860086008600
      FE00FE00FE00FE00C07FFFFFFFF8FFF0C07FF2FFFE00F180E0FFC278E0000180
      000700000007018FF0FF0200003F0FFFF17F0E87003FFBDFFBBF3FFF01BFE187
      FBDFFFFF1FBFFBDFFFFFFFFFFFFFFFFFFE00FFFFFFFFFE00FE00C007000FFE00
      F000C007000FFE00C000C007000FF6008000C007000FE2000000C007000FC000
      0000C007000F8800001FC007000FDC7F0000C007008EFE3F0000C0031144FF1F
      0000C0070AB8FF8F8000C003057CFFC7C100C003FAFCFFE3FF00C007FDF8FFF7
      FF00C017FE04FFFFFF00FE9FFFFFFFFFFFFFFFFFFFFFFE00C001C001FC00FE00
      C001C001FC00F000C001C001FC00C000C001C001FC008000C001C00100000000
      C001C00100000000C001C00100000019C001C001003F0010C001C00100010019
      C001C00100010039C001C00100018079C001C001F801C1F9C001C001F801FFC3
      FFFFFFFFF801FFFFFFFFFFFFF801FFFFDFFFFC07FFFFFFFFEFFFFC07C001F801
      DFFEE007C001F801BBFDE007C001F801D1830007C001F801E0C30007C001E001
      C0830007C001E0018003000FC001E001C00B001FC001E007A81F007FC0018007
      782F0001C0018007FC770301C0018007FEFB0701C001801FFFF5FF01C001801F
      FFEEFF01FFFF801FFFDFFF01FFFF801FFFFFFFFFFFFFFFFF000CFFFFFFF9FFFF
      0008FC3FE7FFDFFD0001F00FC3F3DE3B0063E3C7C3E7DC1700C3E7E7E1C7C00F
      01EBCFF3F08FD807016BCFF3F81FD8070023CFF3FC3F00000067CFF3F81FDE0F
      000FE7A7F09FDF1F000FE787C1C7DFFF000FFF8F83E3C1FF005FFF878FF1DDFF
      003FFFFFFFFFDDFF007FFFFFFFFFFDFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFE003
      8003FFFFFFFFC0038003FFFFFFFFC0038003E01FE07FC0038003F18FF8FFC003
      8003F18FF8FFC0038003F18FFC7FC0038003F01FFC7FC0038003F18FFE3FC003
      8003F18FFE3FC003C1FEF18FFF1FC007E3FEE01FFE0FC00FFFF5FFFFFFFFC01F
      FFF3FFFFFFFFC03FFFF1FFFFFFFFFFFFFFFFFFFFFFFFFFFFE7FFFE00FFFFFE00
      DB3FC000FFFFFE00DADF8000FFFFFE00DADF8000E7FF8000E2DF8000CF838000
      FA3F8000DFC38000F8FF8001DFE38000FDFF8001DFD38001F8FF8001CF3B8003
      FAFF8001E0FF8007F27F87E1FFFF807FF77F8001FFFF80FFF77FC003FFFF81FF
      F77FFC3FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF801F803F83E0FFFF
      001F803F83E0FFFF0007803F83E0FFFF0007800F8080FFE7000180078000C1F3
      000180378100C3FB0001803B8100C7FB0001803BC001CBFB00018020E083DCF3
      00018040E083FF07000180C0F1C7FFFFC00181C0F1C7FFFFC001FFC0F1C7FFFF
      F001FFC0FFFFFFFFF001FFFFFFFFFFFFFFFFFFFFFFFFFFFFE003800FC007E000
      E0038007BFEBC018E00380030005C018E00380017E31C018E00380007E35C000
      E00380000006C000E003800F7FEAC000E003800F8014C7F8E003800FC00AC7F8
      E003C7F8E001C7F8E007FFFCE007C7F8E00FFFBAF007C7F8E01FFFC7F003C7FA
      FFFFFFFFF803C000FFFFFFFFFFFFFFFFFFE7FFE7FFFFFFFFFFE7F067FFFF803F
      FFE7F0270001803FFF00F0007FFF80070000F00052AB80330001F0076D478033
      0001F8076E9F80000001FC077E5F80210001FC1F7EBF80330001E00F7F7F8020
      0001E0077F7F80400001E00F7F7F80C0FFFFE01F7FFF81C0FFFFF00F7F7FFFC0
      FFFFF81FFE3FFFC0FFFFFFFFFC1FFFFFFFE7FFE7FFE7FFE7FFE7FFE7FFE7FFE7
      FFE7FFE7FFE7C007FF00FF00FF00E0000000FF00000000000001FFE700010007
      0001F827000100070001F82700010007000100010001E0010001F83F0001E001
      0001F83F0001F0010001FFFF0001F001FFFFFFFFFFFFFC7FFFFFFFFFFFFFFFFF
      FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF7C718FFFFFFFFFFF
      3C01D0FFFFFFFFFF1C71DF1FF81FC0031FFBEFFFE7E7DFFB0FFBEFFFDFFBDFFB
      8FFBF7FBBFFDDFFB87FBF7F7BFFDDFFBC7FBFBEFBFFDDFFBC3FBFBDFBFFDDFFB
      E3FBFDDFDFFBDFFBE1F1FDBFE7E7DFFBF1F1FE7FF81FC003F3F1FEFFFFFFFFFF
      FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFBFFFF
      FF9FFFFFFFF1C783FF9FFBFFFFE3EFEFFF3FF9FFFFC7F7EFF73FF8FFF08FFBEF
      F27FF80FC01FFC0FF07FF81FC03FFDEFF01FF83F801FFEEFF03FF87F801FFF6F
      F07FF8FF801FFFAFF0FFF9FF801FFFAFF1FFFBFFC03FFFCFF3FFFFFFC03FFFEF
      F7FFFFFFF0FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF807FFFFFFFFFFFFF807
      FFFFE1EFEF0FF003FFFFE1CFE70FE003FFEFE18FE30FC001C7CFE10FE10FC001
      EF8FE00FE00F8001EF0FE00FE00F0000EE0FE00FE00F0000EF0FE00FE00F9000
      CF8FE00FE00FE000EFCFE10FE10FE005FFEFE18FE30FC007FFFFE1CFE70FC007
      FFFFE1EFEF0FE40FFFFFFFFFFFFFFE7FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF
      FFFFFFFFFFFFFBFFFFFFFFFFFFDFF9FFF7FFFFFFFF9FF8FFF3E3FFFFE107F87F
      F1F707C1DF9BF83FF0F707C1DFDBF81FF0770001DFFBF80FF0F707C1DFFBF81F
      F1E707C1E007F83FF3F7FFFFFFFFF87FF7FFFFFFFFFFF8FFFFFFFFFFFFFFF9FF
      FFFFFFFFFFFFFBFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF
      FFFFFFFFFFFFE10FFFFFFFFFFFFFE10F0001FFFFFFFFE10F0001FFFFFFFFE10F
      0001F83F07C1E10F0001F83F07C1E10F000100010001E10F0001F83F07C1E10F
      0001F83F07C1E10F0001FFFFFFFFE10FFFFFFFFFFFFFE10FFFFFFFFFFFFFE10F
      FFFFFFFFFFFFE10FFFFFFFFFFFFFFFFFF000FFFFFFFFFFFFE000FFFFFFFFFFFF
      E000FFFFFFFFC0078000FFFFFFFFE00F800000010001001F800000010001001F
      800000010001001F800000010001000F800000010001E001800000010001E001
      800000010001F001800000010001F0018001FFFFFFFFFC7F8007FFFFFFFFFFFF
      8007FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFC001FFFFFFFFFFFF
      8001FFFFFFFFEF618001C001FFFFEF5D800180018C13E05D80018001DEA7F6E1
      80018001EEB7F6FD80018001E0BBF6E380018001F6DBD9FF80018001FAE3D9FF
      80018001FAFF8FFF80018003FCFF802B8001FFFFFEFF8FFF8001FFFFFFFFDFFF
      8003FFFFFFFFDFFFFFFFFFFFFFFFFFFF00000000000000000000000000000000
      000000000000}
  end
  object adoNetwork: TADOTable
    Connection = adoLinksConnection
    CursorType = ctDynamic
    OnPostError = adoNetworkPostError
    OnMoveComplete = adoNetworkMoveComplete
    TableName = 'mdl_Links_ac'
    Left = 416
    Top = 520
    object adoNetworkMAPINFO_ID: TAutoIncField
      FieldName = 'MAPINFO_ID'
      ReadOnly = True
    end
    object adoNetworkLinkID: TIntegerField
      FieldName = 'LinkID'
    end
    object adoNetworkMLinkID: TIntegerField
      FieldName = 'MLinkID'
    end
    object adoNetworkShape: TWideStringField
      FieldName = 'Shape'
      Size = 4
    end
    object adoNetworkLinkType: TWideStringField
      FieldName = 'LinkType'
      Size = 2
    end
    object adoNetworkUSNode: TWideStringField
      FieldName = 'USNode'
      Size = 6
    end
    object adoNetworkCompKey: TIntegerField
      FieldName = 'CompKey'
    end
    object adoNetworkDSNode: TWideStringField
      FieldName = 'DSNode'
      Size = 6
    end
    object adoNetworkLength: TFloatField
      FieldName = 'Length'
    end
    object adoNetworkDiamWidth: TFloatField
      FieldName = 'DiamWidth'
    end
    object adoNetworkHeight: TFloatField
      FieldName = 'Height'
    end
    object adoNetworkMaterial: TWideStringField
      FieldName = 'Material'
      Size = 6
    end
    object adoNetworkupsdpth: TFloatField
      FieldName = 'upsdpth'
    end
    object adoNetworkUSIE: TFloatField
      FieldName = 'USIE'
    end
    object adoNetworkDSIE: TFloatField
      FieldName = 'DSIE'
    end
    object adoNetworkAsBuilt: TWideStringField
      FieldName = 'AsBuilt'
      Size = 14
    end
    object adoNetworkInstdate: TDateTimeField
      FieldName = 'Instdate'
    end
    object adoNetworkFromX: TFloatField
      FieldName = 'FromX'
    end
    object adoNetworkFromY: TFloatField
      FieldName = 'FromY'
    end
    object adoNetworkToX: TFloatField
      FieldName = 'ToX'
    end
    object adoNetworkToY: TFloatField
      FieldName = 'ToY'
    end
    object adoNetworkRoughness: TFloatField
      FieldName = 'Roughness'
    end
    object adoNetworkLinkReach: TWideStringField
      FieldName = 'LinkReach'
      Size = 254
    end
    object adoNetworkReachElement: TIntegerField
      FieldName = 'ReachElement'
    end
    object adoNetworkDataQual: TWideStringField
      FieldName = 'DataQual'
      Size = 15
    end
    object adoNetworkPipeFlowType: TWideStringField
      FieldName = 'PipeFlowType'
      Size = 1
    end
    object adoNetworkIsSpecLink: TBooleanField
      FieldName = 'IsSpecLink'
    end
  end
  object adoLinksConnection: TADOConnection
    ConnectionString = 
      'Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Development\Prof' +
      'iler Test Data\links\mdl_Links_ac.mdb;Persist Security Info=Fals' +
      'e'
    LoginPrompt = False
    Mode = cmShareDenyNone
    Provider = 'Microsoft.Jet.OLEDB.4.0'
    Left = 260
    Top = 516
  end
  object srcNetwork: TDataSource
    DataSet = adoNetwork
    Left = 504
    Top = 520
  end
  object adoDownstreams: TADOTable
    Connection = adoLinksConnection
    CursorType = ctDynamic
    IndexFieldNames = 'USNode'
    MasterFields = 'DSNode'
    MasterSource = srcNetwork
    TableName = 'mdl_Links_ac'
    Left = 416
    Top = 564
    object adoDownstreamsMAPINFO_ID: TAutoIncField
      FieldName = 'MAPINFO_ID'
      ReadOnly = True
    end
    object adoDownstreamsLinkID: TIntegerField
      FieldName = 'LinkID'
    end
    object adoDownstreamsMLinkID: TIntegerField
      FieldName = 'MLinkID'
    end
    object adoDownstreamsShape: TWideStringField
      FieldName = 'Shape'
      Size = 4
    end
    object adoDownstreamsLinkType: TWideStringField
      FieldName = 'LinkType'
      Size = 2
    end
    object adoDownstreamsUSNode: TWideStringField
      FieldName = 'USNode'
      Size = 6
    end
    object adoDownstreamsCompKey: TIntegerField
      FieldName = 'CompKey'
    end
    object adoDownstreamsDSNode: TWideStringField
      FieldName = 'DSNode'
      Size = 6
    end
    object adoDownstreamsLength: TFloatField
      FieldName = 'Length'
    end
    object adoDownstreamsDiamWidth: TFloatField
      FieldName = 'DiamWidth'
    end
    object adoDownstreamsHeight: TFloatField
      FieldName = 'Height'
    end
    object adoDownstreamsMaterial: TWideStringField
      FieldName = 'Material'
      Size = 6
    end
    object adoDownstreamsupsdpth: TFloatField
      FieldName = 'upsdpth'
    end
    object adoDownstreamsUSIE: TFloatField
      FieldName = 'USIE'
    end
    object adoDownstreamsDSIE: TFloatField
      FieldName = 'DSIE'
    end
    object adoDownstreamsAsBuilt: TWideStringField
      FieldName = 'AsBuilt'
      Size = 14
    end
    object adoDownstreamsInstdate: TDateTimeField
      FieldName = 'Instdate'
    end
    object adoDownstreamsFromX: TFloatField
      FieldName = 'FromX'
    end
    object adoDownstreamsFromY: TFloatField
      FieldName = 'FromY'
    end
    object adoDownstreamsToX: TFloatField
      FieldName = 'ToX'
    end
    object adoDownstreamsToY: TFloatField
      FieldName = 'ToY'
    end
    object adoDownstreamsRoughness: TFloatField
      FieldName = 'Roughness'
    end
    object adoDownstreamsLinkReach: TWideStringField
      FieldName = 'LinkReach'
      Size = 254
    end
    object adoDownstreamsReachElement: TIntegerField
      FieldName = 'ReachElement'
    end
    object adoDownstreamsDataQual: TWideStringField
      FieldName = 'DataQual'
      Size = 15
    end
    object adoDownstreamsPipeFlowType: TWideStringField
      FieldName = 'PipeFlowType'
      Size = 1
    end
    object adoDownstreamsIsSpecLink: TBooleanField
      FieldName = 'IsSpecLink'
    end
  end
  object adoUpstreams: TADOTable
    Connection = adoLinksConnection
    CursorType = ctDynamic
    IndexFieldNames = 'DSNode'
    MasterFields = 'USNode'
    MasterSource = srcNetwork
    TableName = 'mdl_Links_ac'
    Left = 416
    Top = 608
    object adoUpstreamsMAPINFO_ID: TAutoIncField
      FieldName = 'MAPINFO_ID'
      ReadOnly = True
    end
    object adoUpstreamsLinkID: TIntegerField
      FieldName = 'LinkID'
    end
    object adoUpstreamsMLinkID: TIntegerField
      FieldName = 'MLinkID'
    end
    object adoUpstreamsShape: TWideStringField
      FieldName = 'Shape'
      Size = 4
    end
    object adoUpstreamsLinkType: TWideStringField
      FieldName = 'LinkType'
      Size = 2
    end
    object adoUpstreamsUSNode: TWideStringField
      FieldName = 'USNode'
      Size = 6
    end
    object adoUpstreamsCompKey: TIntegerField
      FieldName = 'CompKey'
    end
    object adoUpstreamsDSNode: TWideStringField
      FieldName = 'DSNode'
      Size = 6
    end
    object adoUpstreamsLength: TFloatField
      FieldName = 'Length'
    end
    object adoUpstreamsDiamWidth: TFloatField
      FieldName = 'DiamWidth'
    end
    object adoUpstreamsHeight: TFloatField
      FieldName = 'Height'
    end
    object adoUpstreamsMaterial: TWideStringField
      FieldName = 'Material'
      Size = 6
    end
    object adoUpstreamsupsdpth: TFloatField
      FieldName = 'upsdpth'
    end
    object adoUpstreamsUSIE: TFloatField
      FieldName = 'USIE'
    end
    object adoUpstreamsDSIE: TFloatField
      FieldName = 'DSIE'
    end
    object adoUpstreamsAsBuilt: TWideStringField
      FieldName = 'AsBuilt'
      Size = 14
    end
    object adoUpstreamsInstdate: TDateTimeField
      FieldName = 'Instdate'
    end
    object adoUpstreamsFromX: TFloatField
      FieldName = 'FromX'
    end
    object adoUpstreamsFromY: TFloatField
      FieldName = 'FromY'
    end
    object adoUpstreamsToX: TFloatField
      FieldName = 'ToX'
    end
    object adoUpstreamsToY: TFloatField
      FieldName = 'ToY'
    end
    object adoUpstreamsRoughness: TFloatField
      FieldName = 'Roughness'
    end
    object adoUpstreamsLinkReach: TWideStringField
      FieldName = 'LinkReach'
      Size = 254
    end
    object adoUpstreamsReachElement: TIntegerField
      FieldName = 'ReachElement'
    end
    object adoUpstreamsDataQual: TWideStringField
      FieldName = 'DataQual'
      Size = 15
    end
    object adoUpstreamsPipeFlowType: TWideStringField
      FieldName = 'PipeFlowType'
      Size = 1
    end
    object adoUpstreamsIsSpecLink: TBooleanField
      FieldName = 'IsSpecLink'
    end
  end
  object dsrcDownstreams: TDataSource
    DataSet = adoDownstreams
    Left = 504
    Top = 564
  end
  object dsrcUpstreams: TDataSource
    DataSet = adoUpstreams
    Left = 504
    Top = 608
  end
  object dxStyle: TdxEditStyleController
    BorderColor = clBtnShadow
    BorderStyle = xbsSingle
    ButtonStyle = btsHotFlat
    HotTrack = True
    Left = 764
    Top = 20
  end
  object dlgOpen: TOpenDialog
    DefaultExt = '*.tab'
    Filter = 'MapInfo/Access|*.tab;*.mdb|All files|*.*'
    Title = 'Open MapInfo/Access Trace Database'
    Left = 484
    Top = 20
  end
  object adoLinkChanges: TADOTable
    Connection = adoChangesConnection
    CursorType = ctDynamic
    BeforeInsert = adoLinkChangesBeforeInsert
    TableName = 'LinkChanges'
    Left = 792
    Top = 512
    object adoLinkChangesLinkID: TIntegerField
      FieldName = 'LinkID'
    end
    object adoLinkChangesFieldName: TWideStringField
      FieldName = 'FieldName'
      Size = 50
    end
    object adoLinkChangesChanged: TBooleanField
      FieldName = 'Changed'
    end
    object adoLinkChangesOldValue: TWideStringField
      FieldName = 'OldValue'
      Size = 50
    end
    object adoLinkChangesNewValue: TWideStringField
      FieldName = 'NewValue'
      Size = 50
    end
    object adoLinkChangesComment: TWideStringField
      FieldName = 'Comment'
      Size = 100
    end
    object adoLinkChangesUserName: TWideStringField
      FieldName = 'UserName'
      Size = 50
    end
    object adoLinkChangesUserTime: TDateTimeField
      FieldName = 'UserTime'
    end
  end
  object adoLinksCommand: TADOCommand
    Connection = adoLinksConnection
    Parameters = <>
    Left = 336
    Top = 516
  end
  object adoTraced: TADOTable
    Connection = adoChangesConnection
    CursorType = ctDynamic
    Left = 672
    Top = 524
  end
  object adoLinkDetailChanges: TADOTable
    Connection = adoChangesConnection
    CursorType = ctDynamic
    IndexFieldNames = 'LinkID'
    MasterFields = 'LinkID'
    MasterSource = srcNetwork
    TableName = 'LinkChanges'
    Left = 792
    Top = 556
    object adoLinkDetailChangesLinkID: TIntegerField
      FieldName = 'LinkID'
    end
    object adoLinkDetailChangesFieldName: TWideStringField
      FieldName = 'FieldName'
      Size = 50
    end
    object adoLinkDetailChangesChanged: TBooleanField
      FieldName = 'Changed'
    end
    object adoLinkDetailChangesOldValue: TWideStringField
      FieldName = 'OldValue'
      Size = 50
    end
    object adoLinkDetailChangesNewValue: TWideStringField
      FieldName = 'NewValue'
      Size = 50
    end
    object adoLinkDetailChangesComment: TWideStringField
      FieldName = 'Comment'
      Size = 100
    end
    object adoLinkDetailChangesUserName: TWideStringField
      FieldName = 'UserName'
      Size = 50
    end
    object adoLinkDetailChangesUserTime: TDateTimeField
      FieldName = 'UserTime'
    end
  end
  object srcDetailChanges: TDataSource
    DataSet = adoLinkDetailChanges
    Left = 824
    Top = 556
  end
  object CSGlobalObject1: TCSGlobalObject
    HexNumPrefix = '$'
    QuoteChar = #39
    Left = 840
    Top = 20
  end
  object PrintJob1: TPrintJob
    MultiDoc = False
    PageCount = 1
    Title = 'Print Profile'
    Margins.Left = 0.500000000000000000
    Margins.Top = 0.500000000000000000
    Margins.Right = 0.500000000000000000
    Margins.Bottom = 0.500000000000000000
    MarginsUnits = unInches
    HeaderUnits = unPixels
    FooterUnits = unPixels
    PageMode = pmDefault
    PageWidth = 5100.000000000000000000
    PageHeight = 6600.000000000000000000
    PageUnits = unPixels
    Orientation = orDefault
    Options = [joMargins]
    OnDraw = PrintJob1Draw
    Left = 900
    Top = 20
  end
  object PreviewWindow1: TPreviewWindow
    PrintJob = PrintJob1
    BorderIcons = [biSystemMenu, biMinimize, biMaximize]
    BorderStyle = bsSizeable
    Caption = 'Profiler: Print Preview'
    Ctl3D = True
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    Position = poMainFormCenter
    WindowState = wsNormal
    Left = 416
    Top = 28
    Width = 400
    Height = 500
    Color = clBlack
    ShadowColor = clBlack
    MarginsBorder = clMoneyGreen
    MarginsFill = clMoneyGreen
    MarginsVisible = True
    NonPrintableBorder = clSkyBlue
    NonPrintableFill = clSkyBlue
    NonPrintableVisible = True
    PageIndex = 1
    DragScroll = False
    ScrollTracking = False
    ViewMode = vmWholePage
    ViewScale = 0
    UseMetafile = False
    Buttons = [btnPrint, btnPrinterSetupDialog, btnFirstPage, btnPrevPage, btnNextPage, btnLastPage, btnZoomOut, btnZoomIn, btnClose]
    Flat = True
    ScaleBox = True
    ScaleBoxPos = btnZoomOut
    ShowHint = True
    UserButtonEnabled = False
    PagePanelWidth = 100
    Panels = [pnPage, pnScale]
    ScalePanelWidth = 100
    SizeGrip = True
  end
  object adoChangesCommand: TADOCommand
    Connection = adoChangesConnection
    Parameters = <>
    Left = 336
    Top = 596
  end
  object adoChangesConnection: TADOConnection
    ConnectionString = 
      'Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\Development\Prof' +
      'iler Test Data\qc\Changes.mdb;Persist Security Info=False'
    LoginPrompt = False
    Mode = cmShareDenyNone
    Provider = 'Microsoft.Jet.OLEDB.4.0'
    Left = 256
    Top = 604
  end
  object adoDnNodes: TADOTable
    Connection = adoNodesConnection
    CursorType = ctDynamic
    IndexFieldNames = 'Node'
    MasterFields = 'DSNode'
    MasterSource = srcNetwork
    TableName = 'mdl_nodes_ac'
    Left = 580
    Top = 564
    object adoDnNodesMAPINFO_ID: TAutoIncField
      FieldName = 'MAPINFO_ID'
      ReadOnly = True
    end
    object adoDnNodesNode: TWideStringField
      FieldName = 'Node'
      Size = 6
    end
    object adoDnNodesXCoord: TFloatField
      FieldName = 'XCoord'
    end
    object adoDnNodesYCoord: TFloatField
      FieldName = 'YCoord'
    end
    object adoDnNodesNodeType: TWideStringField
      FieldName = 'NodeType'
      Size = 6
    end
    object adoDnNodesGrndElev: TFloatField
      FieldName = 'GrndElev'
    end
    object adoDnNodesHasSpecNode: TWideStringField
      FieldName = 'HasSpecNode'
      Size = 1
    end
    object adoDnNodesHasSpecLink: TWideStringField
      FieldName = 'HasSpecLink'
      Size = 1
    end
  end
  object adoNodesConnection: TADOConnection
    ConnectionString = 
      'Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\Development\AGMa' +
      'ster\Nodes\mst_Nodes_ac.MDB;Persist Security Info=False'
    LoginPrompt = False
    Mode = cmShareDenyNone
    Provider = 'Microsoft.Jet.OLEDB.4.0'
    Left = 260
    Top = 560
  end
  object adoUpNodes: TADOTable
    Connection = adoNodesConnection
    CursorType = ctDynamic
    IndexFieldNames = 'Node'
    MasterFields = 'USNode'
    MasterSource = srcNetwork
    TableName = 'mdl_nodes_ac'
    Left = 580
    Top = 608
    object adoUpNodesMAPINFO_ID: TAutoIncField
      FieldName = 'MAPINFO_ID'
      ReadOnly = True
    end
    object adoUpNodesNode: TWideStringField
      FieldName = 'Node'
      Size = 6
    end
    object adoUpNodesXCoord: TFloatField
      FieldName = 'XCoord'
    end
    object adoUpNodesYCoord: TFloatField
      FieldName = 'YCoord'
    end
    object adoUpNodesNodeType: TWideStringField
      FieldName = 'NodeType'
      Size = 6
    end
    object adoUpNodesGrndElev: TFloatField
      FieldName = 'GrndElev'
    end
    object adoUpNodesHasSpecNode: TWideStringField
      FieldName = 'HasSpecNode'
      Size = 1
    end
    object adoUpNodesHasSpecLink: TWideStringField
      FieldName = 'HasSpecLink'
      Size = 1
    end
  end
  object adoNodeChanges: TADOTable
    Connection = adoChangesConnection
    CursorType = ctDynamic
    TableName = 'NodeChanges'
    Left = 884
    Top = 512
    object adoNodeChangesNode: TWideStringField
      FieldName = 'Node'
      Size = 6
    end
    object adoNodeChangesFieldName: TWideStringField
      FieldName = 'FieldName'
      Size = 50
    end
    object adoNodeChangesChanged: TBooleanField
      FieldName = 'Changed'
    end
    object adoNodeChangesOldValue: TWideStringField
      FieldName = 'OldValue'
      Size = 50
    end
    object adoNodeChangesNewValue: TWideStringField
      FieldName = 'NewValue'
      Size = 50
    end
    object adoNodeChangesComment: TWideStringField
      FieldName = 'Comment'
      Size = 100
    end
    object adoNodeChangesUserName: TWideStringField
      FieldName = 'UserName'
      Size = 50
    end
    object adoNodeChangesUserTime: TDateTimeField
      FieldName = 'UserTime'
    end
  end
  object adoDSNodeDetailChanges: TADOTable
    Connection = adoChangesConnection
    CursorType = ctDynamic
    IndexFieldNames = 'Node'
    MasterFields = 'DSNode'
    MasterSource = srcNetwork
    TableName = 'NodeChanges'
    Left = 792
    Top = 600
    object adoDSNodeDetailChangesNode: TWideStringField
      FieldName = 'Node'
      Size = 6
    end
    object adoDSNodeDetailChangesFieldName: TWideStringField
      FieldName = 'FieldName'
      Size = 50
    end
    object adoDSNodeDetailChangesChanged: TBooleanField
      FieldName = 'Changed'
    end
    object adoDSNodeDetailChangesOldValue: TWideStringField
      FieldName = 'OldValue'
      Size = 50
    end
    object adoDSNodeDetailChangesNewValue: TWideStringField
      FieldName = 'NewValue'
      Size = 50
    end
    object adoDSNodeDetailChangesComment: TWideStringField
      FieldName = 'Comment'
      Size = 100
    end
    object adoDSNodeDetailChangesUserName: TWideStringField
      FieldName = 'UserName'
      Size = 50
    end
    object adoDSNodeDetailChangesUserTime: TDateTimeField
      FieldName = 'UserTime'
    end
  end
  object srcDSNodeDetailChanges: TDataSource
    DataSet = adoDSNodeDetailChanges
    Left = 824
    Top = 600
  end
  object adoUSNodeDetailChanges: TADOTable
    Connection = adoChangesConnection
    CursorType = ctDynamic
    IndexFieldNames = 'Node'
    MasterFields = 'USNode'
    MasterSource = srcNetwork
    TableName = 'NodeChanges'
    Left = 792
    Top = 644
    object adoUSNodeDetailChangesNode: TWideStringField
      FieldName = 'Node'
      Size = 6
    end
    object adoUSNodeDetailChangesFieldName: TWideStringField
      FieldName = 'FieldName'
      Size = 50
    end
    object adoUSNodeDetailChangesChanged: TBooleanField
      FieldName = 'Changed'
    end
    object adoUSNodeDetailChangesOldValue: TWideStringField
      FieldName = 'OldValue'
      Size = 50
    end
    object adoUSNodeDetailChangesNewValue: TWideStringField
      FieldName = 'NewValue'
      Size = 50
    end
    object adoUSNodeDetailChangesComment: TWideStringField
      FieldName = 'Comment'
      Size = 100
    end
    object adoUSNodeDetailChangesUserName: TWideStringField
      FieldName = 'UserName'
      Size = 50
    end
    object adoUSNodeDetailChangesUserTime: TDateTimeField
      FieldName = 'UserTime'
    end
  end
  object srcUSNodeDetailChanges: TDataSource
    DataSet = adoUSNodeDetailChanges
    Left = 824
    Top = 644
  end
  object adoNodes: TADOTable
    Connection = adoNodesConnection
    CursorType = ctDynamic
    TableName = 'mdl_nodes_ac'
    Left = 580
    Top = 516
    object adoNodesMAPINFO_ID: TAutoIncField
      FieldName = 'MAPINFO_ID'
      ReadOnly = True
    end
    object adoNodesNode: TWideStringField
      FieldName = 'Node'
      Size = 6
    end
    object adoNodesXCoord: TFloatField
      FieldName = 'XCoord'
    end
    object adoNodesYCoord: TFloatField
      FieldName = 'YCoord'
    end
    object adoNodesNodeType: TWideStringField
      FieldName = 'NodeType'
      Size = 6
    end
    object adoNodesGrndElev: TFloatField
      FieldName = 'GrndElev'
    end
    object adoNodesHasSpecNode: TWideStringField
      FieldName = 'HasSpecNode'
      Size = 1
    end
    object adoNodesHasSpecLink: TWideStringField
      FieldName = 'HasSpecLink'
      Size = 1
    end
  end
  object adoLookupTablesConnection: TADOConnection
    LoginPrompt = False
    Mode = cmShareDenyNone
    Provider = 'Microsoft.Jet.OLEDB.4.0'
    Left = 464
    Top = 452
  end
  object adoDataSources: TADOTable
    Connection = adoLookupTablesConnection
    TableName = 'DataSources'
    Left = 676
    Top = 612
  end
  object srcDataSources: TDataSource
    DataSet = adoDataSources
    Left = 708
    Top = 612
  end
  object dxBarDataSource: TdxBarPopupMenu
    BarManager = dxBarManager1
    ItemLinks = <
      item
        Item = mnuDSDimension
        Visible = True
      end
      item
        Item = mnuDSShape
        Visible = True
      end
      item
        Item = mnuDSMaterial
        Visible = True
      end
      item
        Item = mnuDSIEUp
        Visible = True
      end
      item
        Item = mnuDSIEDown
        Visible = True
      end
      item
        Item = mnuDSRim
        Visible = True
      end
      item
        Item = mnuDSLength
        Visible = True
      end
      item
        Item = mnuDSSynth
        Visible = True
      end>
    UseOwnFont = False
    OnPopup = dxBarDataSourcePopup
    Left = 40
    Top = 656
  end
  object dxBarAnchor: TdxBarPopupMenu
    BarManager = dxBarManager1
    ItemLinks = <
      item
        Item = btnAnchorUpstream
        Visible = True
      end
      item
        Item = btnAnchorDownstream
        Visible = True
      end
      item
        BeginGroup = True
        Item = mnuAnchorRoughness
        Visible = True
      end
      item
        Item = mnuAnchorVelocity
        Visible = True
      end>
    UseOwnFont = False
    Left = 40
    Top = 704
  end
  object adoMasterNodesConnection: TADOConnection
    ConnectionString = 
      'Provider=Microsoft.Jet.OLEDB.4.0;Data Source=F:\Development\AGMa' +
      'ster\Nodes\mst_Nodes_ac.MDB;Persist Security Info=False'
    LoginPrompt = False
    Mode = cmShareDenyNone
    Provider = 'Microsoft.Jet.OLEDB.4.0'
    Left = 696
    Top = 452
  end
  object adoMasterNodes: TADOTable
    Connection = adoMasterNodesConnection
    CursorType = ctDynamic
    TableName = 'mst_Nodes_ac'
    Left = 728
    Top = 452
    object adoMasterNodesMAPINFO_ID: TAutoIncField
      FieldName = 'MAPINFO_ID'
      ReadOnly = True
    end
    object adoMasterNodesNode: TWideStringField
      FieldName = 'Node'
      Size = 6
    end
    object adoMasterNodesXCoord: TFloatField
      FieldName = 'XCoord'
    end
    object adoMasterNodesYCoord: TFloatField
      FieldName = 'YCoord'
    end
    object adoMasterNodesNodeType: TWideStringField
      FieldName = 'NodeType'
      Size = 4
    end
    object adoMasterNodesGrndElev: TFloatField
      FieldName = 'GrndElev'
    end
    object adoMasterNodesHasSpecNode: TWideStringField
      FieldName = 'HasSpecNode'
      Size = 1
    end
    object adoMasterNodesHasSpecLink: TWideStringField
      FieldName = 'HasSpecLink'
      Size = 1
    end
  end
  object adoMasterLinks: TADOTable
    Connection = adoMasterLinksConnection
    CursorType = ctDynamic
    TableName = 'mst_links_ac'
    Left = 600
    Top = 452
    object adoMasterLinksMAPINFO_ID: TAutoIncField
      FieldName = 'MAPINFO_ID'
      ReadOnly = True
    end
    object adoMasterLinksMLinkID: TIntegerField
      FieldName = 'MLinkID'
    end
    object adoMasterLinksShape: TWideStringField
      FieldName = 'Shape'
      FixedChar = True
      Size = 4
    end
    object adoMasterLinksLinkType: TWideStringField
      FieldName = 'LinkType'
      FixedChar = True
      Size = 2
    end
    object adoMasterLinksUSNode: TWideStringField
      FieldName = 'USNode'
      FixedChar = True
      Size = 6
    end
    object adoMasterLinksCompKey: TIntegerField
      FieldName = 'CompKey'
    end
    object adoMasterLinksDSNode: TWideStringField
      FieldName = 'DSNode'
      FixedChar = True
      Size = 6
    end
    object adoMasterLinksLength: TFloatField
      FieldName = 'Length'
    end
    object adoMasterLinksDiamWidth: TFloatField
      FieldName = 'DiamWidth'
    end
    object adoMasterLinksHeight: TFloatField
      FieldName = 'Height'
    end
    object adoMasterLinksMaterial: TWideStringField
      FieldName = 'Material'
      FixedChar = True
      Size = 6
    end
    object adoMasterLinksupsdpth: TFloatField
      FieldName = 'upsdpth'
    end
    object adoMasterLinksUSIE: TFloatField
      FieldName = 'USIE'
    end
    object adoMasterLinksDSIE: TFloatField
      FieldName = 'DSIE'
    end
    object adoMasterLinksAsBuilt: TWideStringField
      FieldName = 'AsBuilt'
      FixedChar = True
      Size = 14
    end
    object adoMasterLinksInstdate: TDateTimeField
      FieldName = 'Instdate'
    end
    object adoMasterLinksFromX: TFloatField
      FieldName = 'FromX'
    end
    object adoMasterLinksFromY: TFloatField
      FieldName = 'FromY'
    end
    object adoMasterLinksToX: TFloatField
      FieldName = 'ToX'
    end
    object adoMasterLinksToY: TFloatField
      FieldName = 'ToY'
    end
    object adoMasterLinksRoughness: TFloatField
      FieldName = 'Roughness'
    end
    object adoMasterLinksTimeFrame: TWideStringField
      FieldName = 'TimeFrame'
      FixedChar = True
      Size = 2
    end
    object adoMasterLinksDataFlagSynth: TIntegerField
      FieldName = 'DataFlagSynth'
    end
    object adoMasterLinksDataQual: TWideStringField
      FieldName = 'DataQual'
      Size = 15
    end
    object adoMasterLinksPipeFlowType: TWideStringField
      FieldName = 'PipeFlowType'
      Size = 1
    end
  end
  object adoMasterLinksConnection: TADOConnection
    ConnectionString = 
      'Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Development\AGMa' +
      'ster\Links\mst_links_ac.MDB;Persist Security Info=False'
    LoginPrompt = False
    Mode = cmShareDenyNone
    Provider = 'Microsoft.Jet.OLEDB.4.0'
    Left = 568
    Top = 452
  end
  object adoMasterChangesConnection: TADOConnection
    ConnectionString = 
      'Provider=Microsoft.Jet.OLEDB.4.0;Data Source=W:\AGMaster\maint\C' +
      'hanges.mdb;Persist Security Info=False'
    LoginPrompt = False
    Mode = cmShareDenyNone
    Provider = 'Microsoft.Jet.OLEDB.4.0'
    Left = 828
    Top = 456
  end
  object adoMasterChangesCommand: TADOCommand
    Connection = adoMasterChangesConnection
    Parameters = <>
    Left = 860
    Top = 456
  end
  object adoMasterLinkChanges: TADOTable
    Connection = adoMasterChangesConnection
    CursorType = ctDynamic
    TableName = 'LinkChanges'
    Left = 892
    Top = 456
  end
  object adoMasterNodeChanges: TADOTable
    Connection = adoMasterChangesConnection
    CursorType = ctDynamic
    TableName = 'NodeChanges'
    Left = 924
    Top = 456
  end
  object adoNodesCommand: TADOCommand
    Connection = adoNodesConnection
    Parameters = <>
    Left = 336
    Top = 560
  end
  object dlgBrowser: TRzSelectFolderDialog
    Left = 396
    Top = 452
  end
end
