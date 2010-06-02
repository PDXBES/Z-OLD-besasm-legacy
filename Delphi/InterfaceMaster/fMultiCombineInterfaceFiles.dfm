inherited frmMultiCombineInterfaceFiles: TfrmMultiCombineInterfaceFiles
  Left = 257
  Top = 386
  Caption = 'frmMultiCombineInterfaceFiles'
  OldCreateOrder = True
  OnCreate = FormCreate
  OnDestroy = FormDestroy
  ExplicitWidth = 774
  ExplicitHeight = 512
  PixelsPerInch = 96
  TextHeight = 13
  inherited pnlTitleHolder: TRzPanel
    TabOrder = 2
    inherited RzLabel1: TRzLabel
      Caption = ' Multi-Combine Interface Files'
    end
  end
  object pgcMultiCombineWizard: TRzPageControl
    Left = 0
    Top = 41
    Width = 766
    Height = 396
    ActivePage = shtSaveInterface
    Align = alClient
    BackgroundColor = clBtnFace
    ParentBackgroundColor = False
    ShowFocusRect = False
    ShowShadow = False
    TabOrder = 0
    FixedDimension = 0
    object shtSpecifyFiles: TRzTabSheet
      TabVisible = False
      Caption = 'shtSpecifyFiles'
      DesignSize = (
        764
        394)
      object RzLabel8: TRzLabel
        Left = 8
        Top = 8
        Width = 131
        Height = 16
        Caption = 'Specify Files (1 of 6)'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object RzLabel16: TRzLabel
        Left = 600
        Top = 40
        Width = 46
        Height = 13
        Anchors = [akTop, akRight]
        Caption = 'SyncDate'
      end
      object btnAddInterfaceFile: TRzButton
        Left = 8
        Top = 32
        Width = 121
        Caption = 'Add Interface File(s)'
        HotTrack = True
        TabOrder = 0
        OnClick = btnAddInterfaceFileClick
      end
      object btnRemoveInterfaceFile: TRzButton
        Left = 132
        Top = 32
        Width = 121
        Caption = 'Remove Interface File'
        HotTrack = True
        TabOrder = 1
        OnClick = btnRemoveInterfaceFileClick
      end
      object btnResolveStartTimes: TRzButton
        Left = 8
        Top = 352
        Width = 121
        Anchors = [akLeft, akBottom]
        Caption = 'Resolve Start Times'
        HotTrack = True
        TabOrder = 2
        OnClick = btnResolveStartTimesClick
      end
      object btnLockInterfaceFiles: TRzButton
        Left = 132
        Top = 352
        Width = 145
        Anchors = [akLeft, akBottom]
        Caption = 'Lock Interface Files'
        HotTrack = True
        TabOrder = 3
        OnClick = btnLockInterfaceFilesClick
      end
      object btnLoadConfiguration: TRzButton
        Left = 212
        Top = 4
        Width = 121
        Anchors = [akTop, akRight]
        Caption = 'Load Configuration'
        Enabled = False
        HotTrack = True
        TabOrder = 4
        Visible = False
        OnClick = btnLoadConfigurationClick
      end
      object treeInterfaceFiles: TcxTreeList
        Left = 8
        Top = 64
        Width = 757
        Height = 281
        Anchors = [akLeft, akTop, akRight, akBottom]
        Bands = <
          item
          end>
        BufferedPaint = False
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -9
        Font.Name = 'Tahoma'
        Font.Style = []
        LookAndFeel.Kind = lfUltraFlat
        LookAndFeel.NativeStyle = False
        OptionsView.CellAutoHeight = True
        OptionsView.CellEndEllipsis = True
        OptionsView.ShowEditButtons = ecsbFocused
        OptionsView.ColumnAutoWidth = True
        OptionsView.GridLineColor = clInactiveBorder
        OptionsView.GridLines = tlglBoth
        OptionsView.Indicator = True
        OptionsView.ShowRoot = False
        ParentFont = False
        Styles.Background = cxStyle1
        Styles.Content = cxStyle1
        TabOrder = 5
        OnCustomDrawCell = treeInterfaceFilesCustomDrawCell
        object colInterfaceFile: TcxTreeListColumn
          PropertiesClassName = 'TcxTextEditProperties'
          Caption.MultiLine = True
          Caption.Text = 'Interface File'
          DataBinding.ValueType = 'String'
          Width = 126
          Position.ColIndex = 0
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object colLocation: TcxTreeListColumn
          PropertiesClassName = 'TcxTextEditProperties'
          Caption.MultiLine = True
          Caption.Text = 'Path'
          DataBinding.ValueType = 'String'
          Width = 56
          Position.ColIndex = 1
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object colFormat: TcxTreeListColumn
          PropertiesClassName = 'TcxComboBoxProperties'
          Properties.Items.Strings = (
            'PDX-32 Runoff'
            'PDX-32 Conveyance'
            'PDX-95 Runoff'
            'PDX-95 Conveyance'
            'PDX-95 SYF Conveyance'
            'PDX-95 Conveyance Text'
            'MOUSE M11'
            'XP Runoff'
            'XP Conveyance'
            'XP SYF Conveyance'
            'XP SYR Conveyance')
          Caption.MultiLine = True
          Caption.Text = 'Format'
          DataBinding.ValueType = 'String'
          Width = 56
          Position.ColIndex = 2
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object colFileStart: TcxTreeListColumn
          PropertiesClassName = 'TcxTextEditProperties'
          Caption.MultiLine = True
          Caption.Text = 'File Start'
          DataBinding.ValueType = 'String'
          Width = 56
          Position.ColIndex = 3
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object colExtractStartDate: TcxTreeListColumn
          PropertiesClassName = 'TcxDateEditProperties'
          Caption.MultiLine = True
          Caption.Text = 'Extract Start Date'
          DataBinding.ValueType = 'String'
          Width = 56
          Position.ColIndex = 4
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object colNewStartTime: TcxTreeListColumn
          PropertiesClassName = 'TcxTimeEditProperties'
          Caption.MultiLine = True
          Caption.Text = 'New Start Time'
          DataBinding.ValueType = 'String'
          Width = 55
          Position.ColIndex = 9
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object colNewStartDate: TcxTreeListColumn
          PropertiesClassName = 'TcxDateEditProperties'
          Caption.MultiLine = True
          Caption.Text = 'New Start Date'
          DataBinding.ValueType = 'String'
          Width = 56
          Position.ColIndex = 8
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object colExtractEndDate: TcxTreeListColumn
          PropertiesClassName = 'TcxDateEditProperties'
          Caption.MultiLine = True
          Caption.Text = 'Extract End Date'
          DataBinding.ValueType = 'String'
          Width = 56
          Position.ColIndex = 6
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object colExtractStartTime: TcxTreeListColumn
          PropertiesClassName = 'TcxTimeEditProperties'
          Caption.MultiLine = True
          Caption.Text = 'Extract Start Time'
          DataBinding.ValueType = 'String'
          Width = 56
          Position.ColIndex = 5
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object colExtractEndTime: TcxTreeListColumn
          PropertiesClassName = 'TcxTimeEditProperties'
          Caption.MultiLine = True
          Caption.Text = 'Extract End Time'
          DataBinding.ValueType = 'String'
          Width = 56
          Position.ColIndex = 7
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object colExcludeFromAutoGen: TcxTreeListColumn
          PropertiesClassName = 'TcxCheckBoxProperties'
          Caption.MultiLine = True
          Caption.Text = 'Exclude from AutoGen'
          DataBinding.ValueType = 'String'
          Width = 56
          Position.ColIndex = 10
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object colSkipInterval: TcxTreeListColumn
          PropertiesClassName = 'TcxSpinEditProperties'
          Caption.MultiLine = True
          Caption.Text = 'Skip Interval'
          DataBinding.ValueType = 'String'
          Width = 55
          Position.ColIndex = 11
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
      end
      object btnMakeExtractSameAsStart: TRzButton
        Left = 384
        Top = 32
        Width = 209
        Anchors = [akTop, akRight]
        Caption = 'Make Extract Date Same as File Start'
        HotTrack = True
        TabOrder = 6
        OnClick = btnMakeExtractSameAsStartClick
      end
      object edtSyncDate: TRzDateTimeEdit
        Left = 652
        Top = 36
        Width = 113
        Height = 21
        EditType = etDate
        Anchors = [akTop, akRight]
        FrameController = frmMain.RzFrameController1
        TabOrder = 7
        OnChange = edtSyncDateChange
      end
    end
    object shtSelectNodes: TRzTabSheet
      TabVisible = False
      Caption = 'shtSelectNodes'
      ExplicitLeft = 0
      ExplicitTop = 0
      ExplicitWidth = 0
      ExplicitHeight = 0
      DesignSize = (
        764
        394)
      object RzLabel3: TRzLabel
        Left = 8
        Top = 8
        Width = 136
        Height = 16
        Caption = 'Select Nodes (2 of 6)'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object prgAutoGenerateTransfers: TRzProgressBar
        Left = 196
        Top = 32
        Width = 293
        Height = 21
        Anchors = [akLeft, akTop, akRight]
        BackColor = clBtnFace
        BorderInner = fsFlat
        BorderOuter = fsNone
        BorderWidth = 1
        InteriorOffset = 0
        PartsComplete = 0
        Percent = 0
        TotalParts = 0
      end
      object lblAutoGenerateStatus: TRzLabel
        Left = 496
        Top = 36
        Width = 3
        Height = 13
        Anchors = [akTop, akRight]
      end
      object btnAutoGenerateTransfers: TRzButton
        Left = 7
        Top = 30
        Width = 185
        Caption = 'Auto Generate Transfers...'
        HotTrack = True
        TabOrder = 0
        OnClick = btnAutoGenerateTransfersClick
      end
      object pgcNodes: TRzPageControl
        Left = 8
        Top = 60
        Width = 755
        Height = 327
        ActivePage = shtSelectedInterfaceFile
        Anchors = [akLeft, akTop, akRight, akBottom]
        ShowFocusRect = False
        TabIndex = 0
        TabOrder = 1
        FixedDimension = 19
        object shtSelectedInterfaceFile: TRzTabSheet
          Caption = 'Interface Files'
          object RzPanel2: TRzPanel
            Left = 0
            Top = 263
            Width = 751
            Height = 41
            Align = alBottom
            BorderOuter = fsNone
            TabOrder = 0
            DesignSize = (
              751
              41)
            object lblNumSelectedNodes: TRzLabel
              Left = 260
              Top = 12
              Width = 401
              Height = 25
              Anchors = [akLeft, akTop, akRight]
              AutoSize = False
              Caption = 'lblNumSelectedNodes'
              WordWrap = True
            end
            object btnExcludeAll: TRzButton
              Left = 8
              Top = 8
              Width = 77
              Action = actExcludeAll
              BiDiMode = bdLeftToRight
              HotTrack = True
              ParentBiDiMode = False
              TabOrder = 0
            end
            object btnIncludeAll: TRzButton
              Left = 88
              Top = 8
              Width = 77
              Action = actIncludeAll
              BiDiMode = bdLeftToRight
              HotTrack = True
              ParentBiDiMode = False
              TabOrder = 1
            end
            object btnToggleRange: TRzButton
              Left = 168
              Top = 8
              Width = 85
              Action = actToggleRange
              BiDiMode = bdLeftToRight
              HotTrack = True
              ParentBiDiMode = False
              TabOrder = 2
            end
            object RzButton1: TRzButton
              Left = 668
              Top = 8
              Anchors = [akTop, akRight]
              Caption = 'Set Multiplier'
              HotTrack = True
              TabOrder = 3
            end
          end
          object RzPanel3: TRzPanel
            Left = 0
            Top = 0
            Width = 165
            Height = 263
            Align = alLeft
            BorderOuter = fsNone
            TabOrder = 1
            DesignSize = (
              165
              263)
            object RzLabel5: TRzLabel
              Left = 8
              Top = 8
              Width = 149
              Height = 29
              AutoSize = False
              Caption = 'Click an Interface File to display nodes on the right'
              WordWrap = True
            end
            object lstInterfaceFiles: TRzListBox
              Left = 8
              Top = 40
              Width = 149
              Height = 223
              Anchors = [akLeft, akTop, akBottom]
              FrameController = frmMain.RzFrameController1
              ItemHeight = 13
              TabOrder = 0
              OnClick = lstInterfaceFilesClick
            end
          end
          object RzPanel4: TRzPanel
            Left = 165
            Top = 0
            Width = 586
            Height = 263
            Align = alClient
            BorderOuter = fsNone
            TabOrder = 2
            object lblCurrentInterfaceFile: TRzLabel
              Left = 0
              Top = 0
              Width = 145
              Height = 16
              Align = alTop
              Caption = 'lblCurrentInterfaceFile'
              Font.Charset = ANSI_CHARSET
              Font.Color = clWindowText
              Font.Height = -13
              Font.Name = 'Tahoma'
              Font.Style = [fsBold]
              ParentFont = False
              Layout = tlCenter
            end
            object treeNodes: TcxTreeList
              Left = 0
              Top = 16
              Width = 586
              Height = 247
              Align = alClient
              Bands = <
                item
                end>
              BufferedPaint = False
              OptionsView.ColumnAutoWidth = True
              OptionsView.GridLines = tlglBoth
              OptionsView.Indicator = True
              OptionsView.ShowRoot = False
              TabOrder = 0
              OnEdited = treeNodesEdited
              OnFocusedColumnChanged = treeNodesFocusedColumnChanged
              object cxColRecord: TcxTreeListColumn
                Caption.Text = 'Record'
                DataBinding.ValueType = 'String'
                Position.ColIndex = 0
                Position.RowIndex = 0
                Position.BandIndex = 0
              end
              object cxColIncludeNode: TcxTreeListColumn
                PropertiesClassName = 'TcxCheckBoxProperties'
                Caption.Text = 'Include?'
                DataBinding.ValueType = 'String'
                Position.ColIndex = 1
                Position.RowIndex = 0
                Position.BandIndex = 0
              end
              object cxColOriginalID: TcxTreeListColumn
                Caption.Text = 'Original Node ID'
                DataBinding.ValueType = 'String'
                Position.ColIndex = 2
                Position.RowIndex = 0
                Position.BandIndex = 0
              end
              object cxColNewID: TcxTreeListColumn
                Caption.Text = 'New Node ID'
                DataBinding.ValueType = 'String'
                Position.ColIndex = 3
                Position.RowIndex = 0
                Position.BandIndex = 0
              end
              object cxColFlowMultiplier: TcxTreeListColumn
                Caption.Text = 'Multiplier'
                DataBinding.ValueType = 'String'
                Position.ColIndex = 4
                Position.RowIndex = 0
                Position.BandIndex = 0
              end
            end
          end
        end
      end
    end
    object shtReviewExceptionalNodes: TRzTabSheet
      TabVisible = False
      Caption = 'shtReviewExceptionalNodes'
      ExplicitLeft = 0
      ExplicitTop = 0
      ExplicitWidth = 0
      ExplicitHeight = 0
      DesignSize = (
        764
        394)
      object RzLabel9: TRzLabel
        Left = 8
        Top = 8
        Width = 220
        Height = 16
        Caption = 'Review Exceptional Nodes (3 of 6)'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object RzLabel10: TRzLabel
        Left = 8
        Top = 32
        Width = 530
        Height = 13
        Caption = 
          'The following stop pipes were specified in the MODEL.INI file bu' +
          't no matches were found in the source models.'
      end
      object lblNumExceptionalNodes: TRzLabel
        Left = 8
        Top = 64
        Width = 137
        Height = 13
        Caption = 'lblNumExceptionalNodes'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object RzLabel15: TRzLabel
        Left = 8
        Top = 44
        Width = 311
        Height = 13
        Caption = 
          'Do not assign the same file/flow ID combination to multiple pipe' +
          's.'
      end
      object btnExportExceptionalNodes: TRzButton
        Left = 8
        Top = 370
        Width = 193
        Anchors = [akLeft, akBottom]
        Caption = 'Export List to Text File'
        HotTrack = True
        TabOrder = 0
        OnClick = btnExportExceptionalNodesClick
      end
      object treeExceptionalNodes: TcxTreeList
        Left = 8
        Top = 80
        Width = 757
        Height = 285
        Anchors = [akLeft, akTop, akRight, akBottom]
        Bands = <
          item
          end>
        BufferedPaint = False
        OptionsView.ColumnAutoWidth = True
        OptionsView.GridLines = tlglBoth
        OptionsView.Indicator = True
        OptionsView.ShowRoot = False
        TabOrder = 1
        OnCompare = treeExceptionalNodesCompare
        OnCustomDrawCell = treeExceptionalNodesCustomDrawCell
        OnFocusedColumnChanged = treeExceptionalNodesFocusedColumnChanged
        object cxColStopPipeNotFound: TcxTreeListColumn
          Caption.Text = 'Stop Pipe'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 0
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object cxColStopPipeUSNode: TcxTreeListColumn
          Caption.Text = 'US Node'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 1
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object cxColPipeNotFoundFileAssignment: TcxTreeListColumn
          PropertiesClassName = 'TcxComboBoxProperties'
          Properties.Sorted = True
          Caption.Text = 'Receives Flow from File'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 2
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object cxColPipeNotFoundFlowAssignment: TcxTreeListColumn
          Caption.Text = 'Receives Flow from Flow ID'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 3
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
      end
    end
    object shtReviewMultipleInflows: TRzTabSheet
      TabVisible = False
      Caption = 'shtReviewMultipleInflows'
      ExplicitLeft = 0
      ExplicitTop = 0
      ExplicitWidth = 0
      ExplicitHeight = 0
      DesignSize = (
        764
        394)
      object RzLabel11: TRzLabel
        Left = 8
        Top = 8
        Width = 205
        Height = 16
        Caption = 'Review Multiple Inflows (4 of 6)'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object RzLabel13: TRzLabel
        Left = 8
        Top = 28
        Width = 581
        Height = 13
        Caption = 
          'The following nodes are receiving multiple inflows from multiple' +
          ' source models.  Review and exclude any duplicated flows.'
      end
      object pnlMultipleInflowsAssembly: TRzPanel
        Left = 0
        Top = 353
        Width = 764
        Height = 41
        Align = alBottom
        BorderOuter = fsNone
        TabOrder = 0
        DesignSize = (
          764
          41)
        object RzLabel14: TRzLabel
          Left = 8
          Top = 16
          Width = 94
          Height = 13
          Anchors = [akLeft, akBottom]
          Caption = 'Assembling Preview'
        end
        object prgMultipleInflowsAssembly: TRzProgressBar
          Left = 108
          Top = 9
          Width = 649
          Anchors = [akLeft, akRight, akBottom]
          BorderOuter = fsFlat
          BorderWidth = 0
          InteriorOffset = 0
          PartsComplete = 0
          Percent = 0
          TotalParts = 0
          ExplicitWidth = 657
        end
      end
      object treeMultipleInflows: TcxTreeList
        Left = 8
        Top = 48
        Width = 757
        Height = 313
        Anchors = [akLeft, akTop, akRight, akBottom]
        Bands = <
          item
          end>
        BufferedPaint = False
        OptionsBehavior.Sorting = False
        OptionsBehavior.MultiSort = False
        OptionsView.ColumnAutoWidth = True
        OptionsView.GridLines = tlglBoth
        OptionsView.Indicator = True
        OptionsView.ShowRoot = False
        TabOrder = 1
        OnCustomDrawCell = treeMultipleInflowsCustomDrawCell
        OnEditing = treeMultipleInflowsEditing
        object cxColMultipleInflowsTarget: TcxTreeListColumn
          PropertiesClassName = 'TcxTextEditProperties'
          Caption.Text = 'Target ID'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 0
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object cxColMultipleInflowsInclude: TcxTreeListColumn
          PropertiesClassName = 'TcxCheckBoxProperties'
          Properties.NullStyle = nssInactive
          Caption.Text = 'Include?'
          DataBinding.ValueType = 'String'
          Options.Sorting = False
          Position.ColIndex = 1
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object cxColMultipleInflowsFile: TcxTreeListColumn
          PropertiesClassName = 'TcxTextEditProperties'
          Caption.Text = 'From Interface File'
          DataBinding.ValueType = 'String'
          Options.Sorting = False
          Position.ColIndex = 2
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object cxColMultipleInflowsFlow: TcxTreeListColumn
          PropertiesClassName = 'TcxTextEditProperties'
          Caption.Text = 'Flow ID'
          DataBinding.ValueType = 'String'
          Options.Sorting = False
          Position.ColIndex = 3
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
      end
    end
    object shtReviewMultiCombinedInterfaceFile: TRzTabSheet
      TabVisible = False
      Caption = 'shtReviewMultiCombinedInterfaceFile'
      ExplicitLeft = 0
      ExplicitTop = 0
      ExplicitWidth = 0
      ExplicitHeight = 0
      DesignSize = (
        764
        394)
      object RzLabel12: TRzLabel
        Left = 8
        Top = 8
        Width = 292
        Height = 16
        Caption = 'Review Multi-Combined Interface File (5 of 6)'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object lblTotalCompiledNodes: TRzLabel
        Left = 8
        Top = 344
        Width = 69
        Height = 13
        Anchors = [akLeft, akBottom]
        Caption = 'Total Nodes:'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object pnlPreviewAssembly: TRzPanel
        Left = 0
        Top = 353
        Width = 764
        Height = 41
        Align = alBottom
        BorderOuter = fsNone
        TabOrder = 0
        DesignSize = (
          764
          41)
        object lblPreviewAssembly: TRzLabel
          Left = 8
          Top = 16
          Width = 94
          Height = 13
          Anchors = [akLeft, akBottom]
          Caption = 'Assembling Preview'
        end
        object prgPreviewAssembly: TRzProgressBar
          Left = 108
          Top = 9
          Width = 649
          Anchors = [akLeft, akRight, akBottom]
          BorderOuter = fsFlat
          BorderWidth = 0
          InteriorOffset = 0
          PartsComplete = 0
          Percent = 0
          TotalParts = 0
          ExplicitWidth = 657
        end
      end
      object treePreviewNodes: TcxTreeList
        Left = 8
        Top = 28
        Width = 757
        Height = 313
        Anchors = [akLeft, akTop, akRight, akBottom]
        Bands = <
          item
          end>
        BufferedPaint = False
        OptionsView.ColumnAutoWidth = True
        OptionsView.GridLines = tlglBoth
        OptionsView.Indicator = True
        OptionsView.ShowRoot = False
        TabOrder = 1
        OnFocusedColumnChanged = treePreviewNodesFocusedColumnChanged
        object cxColPreviewNewID: TcxTreeListColumn
          Caption.Text = 'New Node ID'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 0
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object cxColFromInterfaceFile: TcxTreeListColumn
          Caption.Text = 'From Interface File'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 1
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object cxColPreviewOriginalID: TcxTreeListColumn
          Caption.Text = 'Original ID'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 2
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object cxColInterfaceIndex: TcxTreeListColumn
          Caption.Text = 'Idx'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 3
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object cxColInterfaceFlowIndex: TcxTreeListColumn
          Caption.Text = 'FIdx'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 4
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
      end
    end
    object shtSaveInterface: TRzTabSheet
      TabVisible = False
      Caption = 'shtSaveInterface'
      ExplicitLeft = 0
      ExplicitTop = 0
      ExplicitWidth = 0
      ExplicitHeight = 0
      DesignSize = (
        764
        394)
      object lblCurrentDate: TRzLabel
        Left = 158
        Top = 158
        Width = 70
        Height = 13
        Caption = 'lblCurrentDate'
      end
      object RzLabel4: TRzLabel
        Left = 10
        Top = 83
        Width = 46
        Height = 13
        Caption = 'File Name'
      end
      object RzLabel2: TRzLabel
        Left = 8
        Top = 8
        Width = 174
        Height = 16
        Caption = 'Save Interface File (6 of 6)'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object RzLabel6: TRzLabel
        Left = 268
        Top = 36
        Width = 117
        Height = 13
        Caption = 'Interface File Start Date'
      end
      object RzLabel7: TRzLabel
        Left = 268
        Top = 60
        Width = 116
        Height = 13
        Caption = 'Interface File Start Time'
      end
      object lblTotalNodes: TRzLabel
        Left = 8
        Top = 364
        Width = 135
        Height = 13
        Anchors = [akLeft, akBottom]
        Caption = 'Total Nodes in Interface File'
      end
      object lblTotalVolume: TRzLabel
        Left = 8
        Top = 380
        Width = 139
        Height = 13
        Anchors = [akLeft, akBottom]
        Caption = 'Total Volume in Interface File'
      end
      object RzLabel19: TRzLabel
        Left = 8
        Top = 192
        Width = 38
        Height = 16
        Caption = 'Check'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
      end
      object RzBorder1: TRzBorder
        Left = 8
        Top = 180
        Width = 753
        Height = 5
        BorderSides = [sdTop]
        Anchors = [akLeft, akTop, akRight]
      end
      object prgCombine: TRzProgressBar
        Left = 276
        Top = 152
        Width = 485
        Anchors = [akLeft, akTop, akRight]
        BorderOuter = fsFlatRounded
        BorderWidth = 0
        InteriorOffset = 0
        PartsComplete = 0
        Percent = 0
        TotalParts = 100
        Visible = False
      end
      object btnProcess: TRzButton
        Left = 8
        Top = 151
        Width = 145
        Action = actProcess
        HotTrack = True
        TabOrder = 0
      end
      object rgrpFormat: TRzRadioGroup
        Left = 8
        Top = 31
        Width = 249
        Height = 37
        Hint = 
          'PDX-F32: Format used by pre-2000 PDX-SWMM Engines.'#13#13'PDX-F95: Cur' +
          'rent format used by BES'#13#13'XP: Format for XP-SWMM 8.5'#13#13'Text: Comma' +
          '-separated values text file'
        Caption = 'Format'
        Columns = 4
        ItemIndex = 1
        Items.Strings = (
          'PDX-F32'
          'PDX-F95'
          'XP'
          'Text')
        ParentShowHint = False
        ShowHint = True
        TabOrder = 1
      end
      object edtSaveFile: TRzButtonEdit
        Left = 8
        Top = 99
        Width = 755
        Height = 21
        Anchors = [akLeft, akTop, akRight]
        FrameController = frmMain.RzFrameController1
        TabOrder = 2
        ButtonKind = bkFolder
        OnButtonClick = edtSaveFileButtonClick
      end
      object edtInterfaceFileStartDate: TRzDateTimeEdit
        Left = 392
        Top = 32
        Width = 121
        Height = 21
        EditType = etDate
        FrameController = frmMain.RzFrameController1
        TabOrder = 3
      end
      object edtInterfaceFileStartTime: TRzDateTimeEdit
        Left = 392
        Top = 56
        Width = 121
        Height = 21
        EditType = etTime
        FrameController = frmMain.RzFrameController1
        TabOrder = 4
      end
      object btnSaveConfiguration: TRzButton
        Left = 8
        Top = 124
        Width = 145
        Caption = 'Save Configuration'
        Enabled = False
        HotTrack = True
        TabOrder = 5
        Visible = False
        OnClick = btnSaveConfigurationClick
      end
      object treeCheck: TcxTreeList
        Left = 8
        Top = 208
        Width = 753
        Height = 150
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
        OnCustomDrawCell = treeCheckCustomDrawCell
        object cxColFileName: TcxTreeListColumn
          PropertiesClassName = 'TcxTextEditProperties'
          Caption.Text = 'File Name'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 0
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object cxColNumNodes: TcxTreeListColumn
          PropertiesClassName = 'TcxTextEditProperties'
          Properties.Alignment.Horz = taRightJustify
          Caption.AlignHorz = taRightJustify
          Caption.Text = '# Nodes'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 1
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
        object cxColTotalVolume: TcxTreeListColumn
          PropertiesClassName = 'TcxTextEditProperties'
          Properties.Alignment.Horz = taRightJustify
          Caption.AlignHorz = taRightJustify
          Caption.Text = 'Total Volume'
          DataBinding.ValueType = 'String'
          Position.ColIndex = 2
          Position.RowIndex = 0
          Position.BandIndex = 0
        end
      end
      object btnCopyCheckToClipboard: TRzButton
        Left = 652
        Top = 364
        Width = 107
        Anchors = [akRight, akBottom]
        Caption = 'Copy to Clipboard'
        HotTrack = True
        TabOrder = 7
        OnClick = btnCopyCheckToClipboardClick
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
    object RzButton2: TRzButton
      Left = 612
      Top = 12
      Action = actPrevious
      Anchors = [akTop, akRight]
      HotTrack = True
      TabOrder = 0
    end
    object RzButton3: TRzButton
      Left = 692
      Top = 12
      Action = actNext
      Anchors = [akTop, akRight]
      HotTrack = True
      TabOrder = 1
    end
    object btnCloseTask: TRzButton
      Left = 8
      Top = 11
      Anchors = [akLeft, akBottom]
      Caption = 'Close Task'
      HotTrack = True
      TabOrder = 2
      OnClick = btnCloseTaskClick
    end
  end
  object ActionList1: TActionList
    Left = 348
    Top = 412
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
    object actProcess: TAction
      Caption = 'Process'
      OnExecute = actProcessExecute
      OnUpdate = actProcessUpdate
    end
    object actExcludeAll: TAction
      Caption = 'Exclude All'
      OnExecute = actExcludeAllExecute
    end
    object actIncludeAll: TAction
      Caption = 'Include All'
      OnExecute = actIncludeAllExecute
    end
    object actToggleRange: TAction
      Caption = 'Toggle Range'
      OnExecute = actToggleRangeExecute
    end
  end
  object adoReceivingLinks: TADOConnection
    ConnectionString = 
      'Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\\cassio\modeling\H' +
      'istorical\NorthsideInterceptor\NS_EX01_CB1\links\mdl_Links_ac.md' +
      'b;Persist Security Info=False'
    LoginPrompt = False
    Mode = cmShareDenyNone
    Provider = 'Microsoft.Jet.OLEDB.4.0'
    Left = 156
    Top = 428
  end
  object adoqUpstreamLinks: TADOQuery
    Connection = adoReceivingLinks
    Parameters = <>
    SQL.Strings = (
      
        'SELECT mdl_Links_ac.SimLinkID, mdl_Links_ac.MLinkID, mdl_Links_a' +
        'c.CompKey, mdl_Links_ac.USNode'
      'FROM mdl_Links_ac'
      
        'WHERE (((mdl_Links_ac.USNode) Not In (select dsnode from mdl_lin' +
        'ks_ac)));')
    Left = 488
    Top = 416
    object adoqUpstreamLinksCompKey: TIntegerField
      FieldName = 'CompKey'
    end
    object adoqUpstreamLinksSimLinkID: TWideStringField
      FieldName = 'SimLinkID'
    end
    object adoqUpstreamLinksUSNode: TWideStringField
      FieldName = 'USNode'
      Size = 6
    end
    object adoqUpstreamLinksMLinkID: TIntegerField
      FieldName = 'MLinkID'
    end
  end
  object adoqUpstreamNodes: TADOQuery
    Connection = adoReceivingLinks
    Parameters = <>
    SQL.Strings = (
      'SELECT mdl_Links_ac.USNode'
      'FROM mdl_Links_ac'
      
        'WHERE (((mdl_Links_ac.USNode) Not In (select dsnode from mdl_lin' +
        'ks_ac)));')
    Left = 428
    Top = 408
    object adoqUpstreamNodesUSNode: TWideStringField
      FieldName = 'USNode'
      Size = 6
    end
  end
  object adoLinks: TADOTable
    Connection = adoReceivingLinks
    TableName = 'mdl_Links_ac'
    Left = 220
    Top = 424
    object adoLinksUSNode: TWideStringField
      FieldName = 'USNode'
      Size = 6
    end
    object adoLinksDSNode: TWideStringField
      FieldName = 'DSNode'
      Size = 6
    end
    object adoLinksMLinkID: TIntegerField
      FieldName = 'MLinkID'
    end
    object adoLinksSimLinkID: TWideStringField
      FieldName = 'SimLinkID'
    end
  end
  object cxStyleRepository2: TcxStyleRepository
    Left = 344
    Top = 140
    object cxStyle1: TcxStyle
    end
  end
  object errHandler: TRzErrorHandler
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    OKCaption = 'OK'
    CancelCaption = 'Cancel'
    ProceedCaption = 'Proceed'
    Left = 296
    Top = 396
  end
end
