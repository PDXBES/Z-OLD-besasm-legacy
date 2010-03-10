inherited frmConvertInterface: TfrmConvertInterface
  Left = 223
  Top = 356
  Caption = 'frmConvertInterface'
  OldCreateOrder = True
  Position = poScreenCenter
  OnCreate = FormCreate
  ExplicitWidth = 774
  ExplicitHeight = 512
  PixelsPerInch = 96
  TextHeight = 13
  inherited pnlTitleHolder: TRzPanel
    TabOrder = 2
    inherited RzLabel1: TRzLabel
      Caption = ' Convert Interface File'
    end
  end
  object pgcConvertInterfaceWizard: TRzPageControl
    Left = 0
    Top = 41
    Width = 766
    Height = 396
    ActivePage = shtSelectNodes
    Align = alClient
    ShowFocusRect = False
    ShowShadow = False
    TabOrder = 0
    FixedDimension = 0
    object shtOpenFile: TRzTabSheet
      TabVisible = False
      Caption = 'shtOpenFile'
      DesignSize = (
        764
        394)
      object RzLabel2: TRzLabel
        Left = 8
        Top = 84
        Width = 46
        Height = 13
        Caption = 'File Name'
      end
      object RzLabel5: TRzLabel
        Left = 8
        Top = 8
        Width = 184
        Height = 16
        Caption = 'Open File to Convert (1 of 5)'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object lblFromDate: TRzLabel
        Left = 24
        Top = 152
        Width = 50
        Height = 13
        Caption = 'From Date'
        Enabled = False
      end
      object lblToDate: TRzLabel
        Left = 24
        Top = 176
        Width = 38
        Height = 13
        Caption = 'To Date'
        Enabled = False
      end
      object lblFromTime: TRzLabel
        Left = 208
        Top = 152
        Width = 49
        Height = 13
        Caption = 'From Time'
        Enabled = False
      end
      object lblToTime: TRzLabel
        Left = 208
        Top = 176
        Width = 37
        Height = 13
        Caption = 'To Time'
        Enabled = False
      end
      object edtOpenFile: TRzButtonEdit
        Left = 8
        Top = 100
        Width = 755
        Height = 21
        Anchors = [akLeft, akTop, akRight]
        FrameController = frmMain.RzFrameController1
        TabOrder = 1
        OnChange = edtOpenFileChange
        ButtonKind = bkFolder
        OnButtonClick = edtOpenFileButtonClick
      end
      object rgrpFormat: TRzRadioGroup
        Left = 8
        Top = 36
        Width = 753
        Height = 37
        Hint = 
          #8226'PDX-F32: Format used by pre-2000 PDX-SWMM Engines.'#13#8226'PDX-F95: Cu' +
          'rrent format used by BES'#13#8226'XP binary: Format for XP-SWMM 8.x'#13#8226'Tex' +
          't (SWMM): Comma-separated values text file in SWMM format'#13#8226'XP SY' +
          'F: SYF Hydraulics Layer Results file from XP-SWMM 8.x'#13#8226'MOUSE PRF' +
          ': Format for MOUSE 2003 Results File'#13#8226'Text (MOUSE PRF): Text For' +
          'mat for MOUSE 2003 Results File, after using M11 Extra'
        Anchors = [akLeft, akTop, akRight]
        Caption = 'Format'
        Columns = 7
        ItemIndex = 1
        Items.Strings = (
          'PDX-F32'
          'PDX-F95'
          'XP Binary'
          'Text (SWMM)'
          'XP SYF'
          'MOUSE PRF'
          'Text (MOUSE PRF)')
        ParentShowHint = False
        ShowHint = True
        TabOrder = 0
      end
      object chkLimitExtractPeriod: TRzCheckBox
        Left = 8
        Top = 132
        Width = 753
        Height = 17
        Hint = 
          'Turn this checkbox on to limit the date/time range to extract fo' +
          'r conversion.  After you specify a file name, the From Date and ' +
          'From Time boxes will fill depending on what InterfaceMaster find' +
          's as the start date and time in the file you specified.'
        Anchors = [akLeft, akTop, akRight]
        Caption = 'Extract data only from the following time period:'
        HotTrack = True
        State = cbUnchecked
        TabOrder = 2
        OnClick = chkLimitExtractPeriodClick
      end
      object edtFromDate: TRzDateTimeEdit
        Left = 80
        Top = 148
        Width = 121
        Height = 21
        EditType = etDate
        Enabled = False
        FrameController = frmMain.RzFrameController1
        TabOrder = 3
      end
      object edtFromTime: TRzDateTimeEdit
        Left = 260
        Top = 148
        Width = 121
        Height = 21
        EditType = etTime
        Enabled = False
        FrameController = frmMain.RzFrameController1
        TabOrder = 5
      end
      object edtToDate: TRzDateTimeEdit
        Left = 80
        Top = 172
        Width = 121
        Height = 21
        EditType = etDate
        Enabled = False
        FrameController = frmMain.RzFrameController1
        TabOrder = 4
      end
      object edtToTime: TRzDateTimeEdit
        Left = 260
        Top = 172
        Width = 121
        Height = 21
        EditType = etTime
        Enabled = False
        FrameController = frmMain.RzFrameController1
        TabOrder = 6
      end
    end
    object shtSetOptions: TRzTabSheet
      TabVisible = False
      Caption = 'shtSetOptions'
      DesignSize = (
        764
        394)
      object RzLabel6: TRzLabel
        Left = 8
        Top = 8
        Width = 247
        Height = 16
        Caption = 'Set Options for Converted File (2 of 5)'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object RzLabel12: TRzLabel
        Left = 8
        Top = 84
        Width = 74
        Height = 13
        Caption = 'Format Options'
      end
      object tabConvertType: TRzPageControl
        Left = 8
        Top = 100
        Width = 753
        Height = 281
        ActivePage = shtSWMMOptions
        ActivePageDefault = shtMOUSEOptions
        Anchors = [akLeft, akTop, akRight, akBottom]
        BoldCurrentTab = True
        ShowFocusRect = False
        ShowShadow = False
        TabOrder = 1
        Visible = False
        OnChange = tabConvertTypeChange
        FixedDimension = 0
        object shtMOUSEOptions: TRzTabSheet
          TabVisible = False
          Caption = 'To MOUSE'
          DesignSize = (
            751
            279)
          object RzLabel11: TRzLabel
            Left = 8
            Top = 12
            Width = 81
            Height = 13
            Caption = 'MOUSE DB Name'
          end
          object RzLabel14: TRzLabel
            Left = 8
            Top = 36
            Width = 55
            Height = 13
            Caption = 'Event Type'
            Visible = False
          end
          object RzLabel25: TRzLabel
            Left = 8
            Top = 60
            Width = 24
            Height = 13
            Caption = 'Units'
            Visible = False
          end
          object lblFactor: TRzLabel
            Left = 248
            Top = 60
            Width = 31
            Height = 13
            Caption = 'Factor'
            Visible = False
          end
          object lblOffset: TRzLabel
            Left = 360
            Top = 60
            Width = 31
            Height = 13
            Caption = 'Offset'
            Visible = False
          end
          object RzLabel28: TRzLabel
            Left = 304
            Top = 96
            Width = 48
            Height = 13
            Caption = 'BSF Name'
          end
          object edtMOUSEDB: TRzEdit
            Left = 96
            Top = 8
            Width = 647
            Height = 21
            Anchors = [akLeft, akTop, akRight]
            FrameController = frmMain.RzFrameController1
            TabOrder = 3
          end
          object edtEventType: TRzMRUComboBox
            Left = 96
            Top = 32
            Width = 145
            Height = 21
            RemoveItemCaption = '&Remove item from history list'
            Ctl3D = False
            FrameController = frmMain.RzFrameController1
            ItemHeight = 13
            Items.Strings = (
              'BOTTOM_LEVEL'
              'CONCENTRATION'
              'DISCHARGE'
              'EVAPORATION'
              'FRACTION_VALUE'
              'GATE_POSITION'
              'RAIN_INTENSITY'
              'SEDIMENT_TRANSPORT'
              'TEMPERATURE'
              'TIME_STEP'
              'VELOCITY'
              'WATER_LEVEL'
              'WEIR_POSITION')
            ParentCtl3D = False
            TabOrder = 4
            Visible = False
            OnChange = edtEventTypeChange
          end
          object edtFactor: TRzSpinEdit
            Left = 284
            Top = 56
            Width = 65
            Height = 21
            AllowKeyEdit = True
            CheckRange = True
            IntegersOnly = False
            Max = 9999999.000000000000000000
            FrameController = frmMain.RzFrameController1
            TabOrder = 6
            Visible = False
          end
          object edtOffset: TRzSpinEdit
            Left = 396
            Top = 56
            Width = 65
            Height = 21
            AllowKeyEdit = True
            IntegersOnly = False
            FrameController = frmMain.RzFrameController1
            TabOrder = 8
            Visible = False
          end
          object chkGenerateMouseTimeSeries: TRzCheckBox
            Left = 8
            Top = 80
            Width = 281
            Height = 17
            Caption = 'Generate MOUSE time series (BDB_EVENT)'
            Checked = True
            HotTrack = True
            State = cbChecked
            TabOrder = 0
          end
          object chkGenerateMouseBoundaryConnectors: TRzCheckBox
            Left = 8
            Top = 96
            Width = 289
            Height = 17
            Caption = 'Generate MOUSE boundary connectors (MOUSE_BSF)'
            Checked = True
            HotTrack = True
            State = cbChecked
            TabOrder = 1
          end
          object edtBSFName: TRzEdit
            Left = 356
            Top = 92
            Width = 387
            Height = 21
            Anchors = [akLeft, akTop, akRight]
            FrameController = frmMain.RzFrameController1
            TabOrder = 7
          end
          object btnSpecifyExtractDates: TRzBitBtn
            Left = 8
            Top = 116
            Width = 185
            Action = actSpecifyExtractDates
            Caption = 'Specify Extract Dates'
            HotTrack = True
            TabOrder = 2
          end
          object edtUnits: TRzComboBox
            Left = 96
            Top = 56
            Width = 145
            Height = 21
            Ctl3D = False
            FrameController = frmMain.RzFrameController1
            ItemHeight = 13
            ParentCtl3D = False
            TabOrder = 5
            Visible = False
            OnChange = edtUnitsChange
          end
        end
        object shtSWMMOptions: TRzTabSheet
          TabVisible = False
          Caption = 'To SWMM'
          DesignSize = (
            751
            279)
          object RzLabel29: TRzLabel
            Left = 8
            Top = 12
            Width = 29
            Height = 13
            Caption = 'Title 1'
          end
          object RzLabel30: TRzLabel
            Left = 8
            Top = 36
            Width = 29
            Height = 13
            Caption = 'Title 2'
          end
          object RzLabel31: TRzLabel
            Left = 8
            Top = 60
            Width = 29
            Height = 13
            Caption = 'Title 3'
          end
          object RzLabel32: TRzLabel
            Left = 8
            Top = 84
            Width = 29
            Height = 13
            Caption = 'Title 4'
          end
          object RzLabel33: TRzLabel
            Left = 8
            Top = 108
            Width = 60
            Height = 13
            Caption = 'Source Block'
          end
          object RzLabel34: TRzLabel
            Left = 204
            Top = 108
            Width = 23
            Height = 13
            Caption = 'Area'
          end
          object RzLabel35: TRzLabel
            Left = 352
            Top = 108
            Width = 67
            Height = 13
            Caption = 'Flow Multiplier'
          end
          object RzLabel3: TRzLabel
            Left = 8
            Top = 180
            Width = 60
            Height = 13
            Caption = 'Skip Interval'
          end
          object chkSWMMAlphanumeric: TRzCheckBox
            Left = 200
            Top = 132
            Width = 105
            Height = 17
            Caption = 'Alphanumeric IDs'
            HotTrack = True
            State = cbUnchecked
            TabOrder = 6
          end
          object edtSWMMTitle1: TRzEdit
            Left = 44
            Top = 8
            Width = 699
            Height = 21
            Anchors = [akLeft, akTop, akRight]
            FrameController = frmMain.RzFrameController1
            MaxLength = 80
            TabOrder = 1
          end
          object edtSWMMTitle2: TRzEdit
            Left = 44
            Top = 32
            Width = 699
            Height = 21
            Anchors = [akLeft, akTop, akRight]
            FrameController = frmMain.RzFrameController1
            MaxLength = 80
            TabOrder = 2
          end
          object edtSWMMTitle3: TRzEdit
            Left = 44
            Top = 56
            Width = 699
            Height = 21
            Anchors = [akLeft, akTop, akRight]
            FrameController = frmMain.RzFrameController1
            MaxLength = 80
            TabOrder = 3
          end
          object edtSWMMTitle4: TRzEdit
            Left = 44
            Top = 80
            Width = 699
            Height = 21
            Anchors = [akLeft, akTop, akRight]
            FrameController = frmMain.RzFrameController1
            MaxLength = 80
            TabOrder = 4
          end
          object edtSWMMSourceBlock: TRzEdit
            Left = 72
            Top = 104
            Width = 121
            Height = 21
            FrameController = frmMain.RzFrameController1
            MaxLength = 20
            TabOrder = 5
          end
          object edtSWMMArea: TRzSpinEdit
            Left = 233
            Top = 105
            Width = 101
            Height = 21
            AllowKeyEdit = True
            IntegersOnly = False
            Max = 3.4E38
            Min = -3.4E38
            Enabled = False
            FrameController = frmMain.RzFrameController1
            TabOrder = 7
          end
          object edtSWMMFlowMultiplier: TRzSpinEdit
            Left = 424
            Top = 104
            Width = 101
            Height = 21
            AllowKeyEdit = True
            Decimals = 2
            IntegersOnly = False
            Max = 3.4E38
            Min = -3.4E38
            Enabled = False
            FrameController = frmMain.RzFrameController1
            TabOrder = 8
          end
          object rgrpToSWMMFormat: TRzRadioGroup
            Left = 8
            Top = 128
            Width = 185
            Height = 45
            Hint = 
              #8226'F32: Old Lahey-32 format'#13#8226'F95: PDX-SWMM format'#13#8226'XP: XP-SWMM 8.x' +
              ' format'#13#8226'Text: Text format'
            Caption = 'SWMM Format'
            Columns = 4
            ItemIndex = 1
            Items.Strings = (
              'F32'
              'F95'
              'XP'
              'Text')
            TabOrder = 0
          end
          object edtSkipInterval: TRzSpinEdit
            Left = 76
            Top = 176
            Width = 61
            Height = 21
            Hint = 
              'USE ONLY FOR REGULARLY SPACED TIME STEPS (Runoff has variable ti' +
              'me steps, so don'#39't use this feature with Runoff interfaces)'#13#13'0 =' +
              ' don'#39't skip any time steps'#13'1 = skip every other time step'#13'2 = ta' +
              'ke one time step, skip two, then take another'#13'etc.'
            AllowKeyEdit = True
            Max = 999999999.000000000000000000
            FrameController = frmMain.RzFrameController1
            TabOrder = 9
          end
        end
        object shtSYFTextOptions: TRzTabSheet
          TabVisible = False
          Caption = 'SYF To Text'
          object RzLabel4: TRzLabel
            Left = 8
            Top = 112
            Width = 50
            Height = 13
            Caption = 'Start Date'
            Visible = False
          end
          object RzLabel13: TRzLabel
            Left = 204
            Top = 112
            Width = 49
            Height = 13
            Caption = 'Start Time'
            Visible = False
          end
          object RzCheckBox1: TRzCheckBox
            Left = 8
            Top = 8
            Width = 115
            Height = 17
            Caption = 'Node Depths'
            Checked = True
            HotTrack = True
            State = cbChecked
            TabOrder = 0
          end
          object RzCheckBox2: TRzCheckBox
            Left = 8
            Top = 24
            Width = 115
            Height = 17
            Caption = 'Node Inverts'
            Checked = True
            HotTrack = True
            State = cbChecked
            TabOrder = 1
          end
          object RzCheckBox3: TRzCheckBox
            Left = 8
            Top = 40
            Width = 115
            Height = 17
            Caption = 'Link Flows'
            Checked = True
            HotTrack = True
            State = cbChecked
            TabOrder = 2
          end
          object RzCheckBox4: TRzCheckBox
            Left = 8
            Top = 56
            Width = 193
            Height = 17
            Caption = 'Link Upstream/Downstream Depths'
            Checked = True
            HotTrack = True
            State = cbChecked
            TabOrder = 3
          end
          object RzCheckBox5: TRzCheckBox
            Left = 8
            Top = 72
            Width = 115
            Height = 17
            Caption = 'Velocities'
            Checked = True
            HotTrack = True
            State = cbChecked
            TabOrder = 4
          end
          object edtSYFStartDate: TRzDateTimeEdit
            Left = 64
            Top = 108
            Width = 121
            Height = 21
            EditType = etDate
            FrameController = frmMain.RzFrameController1
            TabOrder = 5
            Visible = False
          end
          object edtSYFStartTime: TRzDateTimeEdit
            Left = 260
            Top = 108
            Width = 121
            Height = 21
            EditType = etTime
            FrameController = frmMain.RzFrameController1
            TabOrder = 6
            Visible = False
          end
        end
      end
      object rgrpConvertFormat: TRzRadioGroup
        Left = 8
        Top = 32
        Width = 753
        Height = 45
        Hint = 
          'To convert text, select SWMM, then in Format Options, for SWMM f' +
          'ormat, choose Text.'
        Anchors = [akLeft, akTop, akRight]
        Caption = 'Converted File Format'
        Columns = 4
        ItemIndex = 0
        Items.Strings = (
          'MOUSE'
          'SWMM'
          'SYF to Text')
        TabOrder = 0
        OnClick = rgrpConvertFormatClick
      end
    end
    object shtSelectNodes: TRzTabSheet
      TabVisible = False
      Caption = 'shtSelectNodes'
      DesignSize = (
        764
        394)
      object RzLabel7: TRzLabel
        Left = 8
        Top = 8
        Width = 210
        Height = 16
        Caption = 'Select Nodes to Convert (3 of 5)'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object btnExcludeAll: TRzButton
        Left = 8
        Top = 361
        Width = 77
        Action = actExcludeAll
        Anchors = [akLeft, akBottom]
        BiDiMode = bdLeftToRight
        HotTrack = True
        ParentBiDiMode = False
        TabOrder = 1
      end
      object btnIncludeAll: TRzButton
        Left = 88
        Top = 361
        Width = 77
        Action = actIncludeAll
        Anchors = [akLeft, akBottom]
        BiDiMode = bdLeftToRight
        HotTrack = True
        ParentBiDiMode = False
        TabOrder = 2
      end
      object btnToggleRange: TRzButton
        Left = 168
        Top = 361
        Width = 85
        Action = actToggleRange
        Anchors = [akLeft, akBottom]
        BiDiMode = bdLeftToRight
        HotTrack = True
        ParentBiDiMode = False
        TabOrder = 5
      end
      object btnSaveModifiers: TRzButton
        Left = 116
        Top = 32
        Width = 105
        Action = actSaveModifiers
        Caption = 'Save Nodes List'
        HotTrack = True
        TabOrder = 3
      end
      object btnLoadModifiers: TRzButton
        Left = 8
        Top = 32
        Width = 105
        Action = actLoadModifiers
        Caption = 'Load Nodes List'
        HotTrack = True
        TabOrder = 0
      end
      object btnSetGlobalMultiplier: TRzButton
        Left = 584
        Top = 360
        Width = 179
        Hint = 
          'Sets all nodes with a blank Multiplier field with a specified mu' +
          'ltiplier.'
        Anchors = [akRight, akBottom]
        Caption = 'Set Global Multiplier'
        HotTrack = True
        TabOrder = 4
        OnClick = btnSetGlobalMultiplierClick
      end
      object treeNodes: TcxTreeList
        Left = 8
        Top = 60
        Width = 757
        Height = 293
        Anchors = [akLeft, akTop, akRight, akBottom]
        Bands = <
          item
          end>
        BufferedPaint = False
        OptionsView.ColumnAutoWidth = True
        OptionsView.GridLines = tlglBoth
        OptionsView.Indicator = True
        OptionsView.ShowRoot = False
        TabOrder = 6
        OnCustomDrawFooterCell = treeNodesCustomDrawFooterCell
        OnFocusedColumnChanged = treeNodesFocusedColumnChanged
        object cxColAssignToID: TcxTreeListColumn
          Caption.Text = 'Assign to Node ID'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 4
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object cxColInclude: TcxTreeListColumn
          PropertiesClassName = 'TcxCheckBoxProperties'
          Properties.OnEditValueChanged = cxColIncludePropertiesEditValueChanged
          Caption.Text = 'Include?'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 1
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object cxColModifier: TcxTreeListColumn
          Caption.Text = 'Replace With'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 5
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object cxColMultiplier: TcxTreeListColumn
          Caption.Text = 'Multiplier'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 6
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
        object cxColOriginalID: TcxTreeListColumn
          Caption.Text = 'Original ID'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 2
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object cxColRecord: TcxTreeListColumn
          Caption.Text = 'Record'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 0
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
      end
    end
    object shtSaveFile: TRzTabSheet
      TabVisible = False
      Caption = 'shtSaveFile'
      DesignSize = (
        764
        394)
      object RzLabel8: TRzLabel
        Left = 8
        Top = 8
        Width = 181
        Height = 16
        Caption = 'Save Converted File (4 of 5)'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object lblSaveConvertedFile: TRzLabel
        Left = 8
        Top = 36
        Width = 46
        Height = 13
        Caption = 'File Name'
      end
      object edtSaveFile: TRzButtonEdit
        Left = 8
        Top = 52
        Width = 756
        Height = 21
        Anchors = [akLeft, akTop, akRight]
        FrameController = frmMain.RzFrameController1
        TabOrder = 0
        ButtonKind = bkFolder
        OnButtonClick = edtSaveFileButtonClick
      end
    end
    object shtProcessFile: TRzTabSheet
      TabVisible = False
      Caption = 'shtProcessFile'
      DesignSize = (
        764
        394)
      object prgConvert: TRzProgressBar
        Left = 8
        Top = 148
        Width = 753
        Height = 20
        Anchors = [akLeft, akTop, akRight]
        BackColor = clBtnFace
        BorderInner = fsFlat
        BorderOuter = fsNone
        BorderWidth = 1
        InteriorOffset = 0
        PartsComplete = 0
        Percent = 0
        TotalParts = 100
      end
      object lblTotalProgress: TRzLabel
        Left = 8
        Top = 132
        Width = 69
        Height = 13
        Caption = 'Total Progress'
      end
      object prgFile: TRzProgressBar
        Left = 8
        Top = 100
        Width = 753
        Height = 20
        Anchors = [akLeft, akTop, akRight]
        BackColor = clBtnFace
        BorderInner = fsFlat
        BorderOuter = fsNone
        BorderWidth = 1
        InteriorOffset = 0
        PartsComplete = 0
        Percent = 0
        TotalParts = 100
      end
      object lblFileProgress: TRzLabel
        Left = 8
        Top = 84
        Width = 61
        Height = 13
        Caption = 'File Progress'
      end
      object RzLabel9: TRzLabel
        Left = 8
        Top = 8
        Width = 129
        Height = 16
        Caption = 'Convert File (5 of 5)'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object btnConvertFile: TRzButton
        Left = 12
        Top = 44
        Width = 185
        Action = actConvertFile
        HotTrack = True
        TabOrder = 0
      end
      object btnStopConversion: TRzButton
        Left = 200
        Top = 44
        Width = 185
        Action = actStopConversion
        HotTrack = True
        TabOrder = 1
      end
    end
  end
  object RzPanel1: TRzPanel
    Left = 0
    Top = 437
    Width = 766
    Height = 48
    Align = alBottom
    BorderOuter = fsNone
    TabOrder = 1
    DesignSize = (
      766
      48)
    object btnCloseTask: TRzButton
      Left = 4
      Top = 11
      Width = 105
      Anchors = [akLeft, akBottom]
      Caption = 'Close Task'
      HotTrack = True
      TabOrder = 0
      OnClick = btnCloseTaskClick
    end
    object RzButton1: TRzButton
      Left = 612
      Top = 12
      Action = actPrevious
      Anchors = [akTop, akRight]
      HotTrack = True
      TabOrder = 1
    end
    object RzButton2: TRzButton
      Left = 692
      Top = 12
      Action = actNext
      Anchors = [akTop, akRight]
      HotTrack = True
      TabOrder = 2
    end
  end
  object ActionList1: TActionList
    Left = 568
    Top = 440
    object actExcludeAll: TAction
      Caption = 'Exclude All'
      OnExecute = actExcludeAllExecute
      OnUpdate = actExcludeAllUpdate
    end
    object actIncludeAll: TAction
      Caption = 'Include All'
      OnExecute = actIncludeAllExecute
      OnUpdate = actExcludeAllUpdate
    end
    object actToggleRange: TAction
      Caption = 'Toggle Range'
      OnExecute = actToggleRangeExecute
      OnUpdate = actExcludeAllUpdate
    end
    object actSaveModifiers: TAction
      Caption = 'Save Modifiers'
      OnExecute = actSaveModifiersExecute
      OnUpdate = actExcludeAllUpdate
    end
    object actLoadModifiers: TAction
      Caption = 'Load Modifiers'
      OnExecute = actLoadModifiersExecute
      OnUpdate = actExcludeAllUpdate
    end
    object actConvertFile: TAction
      Caption = 'Convert File'
      OnExecute = btnConvertFileClick
      OnUpdate = actExcludeAllUpdate
    end
    object actStopConversion: TAction
      Caption = 'Stop Conversion'
      OnUpdate = actStopConversionUpdate
    end
    object actSpecifyExtractDates: TAction
      Caption = 'Specify Extract Dates'
      OnExecute = actSpecifyExtractDatesExecute
    end
    object actPrevious: TAction
      Caption = '<< Previous'
      OnExecute = actPreviousExecute
      OnUpdate = actPreviousUpdate
    end
    object actNext: TAction
      Caption = 'Next >>'
      OnExecute = actNextExecute
      OnUpdate = actNextUpdate
    end
  end
  object appLauncher: TRzLauncher
    Action = 'Open'
    ShowMode = smMinimized
    Timeout = -1
    WaitUntilFinished = True
    Left = 416
    Top = 441
  end
end
