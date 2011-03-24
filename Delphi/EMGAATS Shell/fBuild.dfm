inherited frmBuild: TfrmBuild
  Left = 0
  Top = 0
  Caption = 'frmBuild'
  ClientHeight = 575
  ClientWidth = 769
  Position = poDesigned
  OnCreate = FormCreate
  OnDestroy = FormDestroy
  OnShow = FormShow
  ExplicitWidth = 777
  ExplicitHeight = 609
  PixelsPerInch = 96
  TextHeight = 13
  object pnlBuild: TRzPanel [0]
    Left = 0
    Top = 41
    Width = 769
    Height = 534
    Align = alClient
    BorderOuter = fsNone
    TabOrder = 1
    object RzSizePanel1: TRzSizePanel
      Left = 415
      Top = 41
      Width = 354
      Height = 445
      Align = alRight
      RealTimeDrag = True
      SizeBarWidth = 7
      TabOrder = 1
      DesignSize = (
        354
        445)
      object grpbBuildOptions: TRzGroupBox
        Left = 8
        Top = 4
        Width = 338
        Height = 446
        Anchors = [akLeft, akTop, akRight, akBottom]
        Caption = 'Build Options'
        Enabled = False
        GroupStyle = gsTopLine
        Padding.Left = 8
        Padding.Top = 5
        Padding.Right = 8
        Padding.Bottom = 12
        TabOrder = 0
        object RzPanel2: TRzPanel
          Left = 8
          Top = 202
          Width = 322
          Height = 232
          Align = alClient
          BorderOuter = fsNone
          Enabled = False
          TabOrder = 0
          DesignSize = (
            322
            232)
          object RzGroupBox1: TRzGroupBox
            Left = 0
            Top = 6
            Width = 321
            Height = 223
            Anchors = [akLeft, akTop, akRight, akBottom]
            Caption = 'Build Instructions'
            Enabled = False
            GroupStyle = gsTopLine
            TabOrder = 0
            DesignSize = (
              321
              223)
            object lblPreset: TRzLabel
              Left = 116
              Top = 24
              Width = 201
              Height = 22
              Anchors = [akLeft, akTop, akRight]
              AutoSize = False
              Enabled = False
              WordWrap = True
            end
            object mnuBuildPresetsMenu: TRzMenuButton
              Left = 0
              Top = 20
              Caption = 'Build presets'
              Enabled = False
              HotTrack = True
              TabOrder = 0
              DropDownMenu = mnuPopup
            end
            object RzPanel7: TRzPanel
              Left = 0
              Top = 52
              Width = 321
              Height = 171
              Align = alBottom
              Anchors = [akLeft, akTop, akRight, akBottom]
              BorderOuter = fsNone
              Enabled = False
              TabOrder = 1
              object chklstBuildOptions: TRzCheckList
                Left = 0
                Top = 0
                Width = 321
                Height = 171
                TabStopsMode = tsmAutomatic
                Items.Strings = (
                  '//Test'
                  'Build network'
                  'Check network trace before continuing'
                  'Build direct subcatchments'
                  'Build surface subcatchments'
                  'Build inflow controls'
                  'Calculate hydrology'
                  'Deploy model files'
                  'Create QC workspaces'
                  'Create QC worksheets')
                Items.ItemEnabled = (
                  False
                  False
                  False
                  False
                  False
                  False
                  False
                  False
                  False
                  False)
                Items.ItemState = (
                  0
                  1
                  1
                  1
                  1
                  1
                  1
                  1
                  1
                  1)
                OnChange = chklstBuildOptionsChange
                Align = alClient
                Enabled = False
                FrameController = frmMain.RzFrameController1
                GroupColor = clHotLight
                GroupFont.Charset = DEFAULT_CHARSET
                GroupFont.Color = clWindowText
                GroupFont.Height = -11
                GroupFont.Name = 'Tahoma'
                GroupFont.Style = [fsBold]
                HorzExtent = 213
                HorzScrollBar = True
                ItemHeight = 17
                TabOrder = 0
              end
            end
          end
        end
        object RzSizePanel2: TRzSizePanel
          Left = 8
          Top = 18
          Width = 322
          Height = 184
          Align = alTop
          Enabled = False
          RealTimeDrag = True
          SizeBarWidth = 6
          TabOrder = 1
          object vgrdBuildOptions: TcxVerticalGrid
            Left = 0
            Top = 0
            Width = 322
            Height = 177
            Align = alClient
            Enabled = False
            LookAndFeel.Kind = lfUltraFlat
            LookAndFeel.NativeStyle = True
            OptionsView.CellEndEllipsis = True
            OptionsView.ShowEditButtons = ecsbAlways
            OptionsView.RowHeaderWidth = 171
            OptionsBehavior.AlwaysShowEditor = True
            TabOrder = 0
            OnDrawBackground = vgrdBuildOptionsDrawBackground
            object vgrdTitle: TcxEditorRow
              Properties.Caption = 'Title'
              Properties.EditPropertiesClassName = 'TcxTextEditProperties'
              Properties.DataBinding.ValueType = 'String'
              Properties.Value = 'Default model name'
            end
            object vgrdDescription: TcxEditorRow
              Properties.Caption = 'Description'
              Properties.EditPropertiesClassName = 'TcxTextEditProperties'
              Properties.EditProperties.MaxLength = 250
              Properties.DataBinding.ValueType = 'String'
              Properties.Value = Null
            end
            object vgrdTimeFrame: TcxEditorRow
              Properties.Caption = 'Hydrologic Timeframe'
              Properties.EditPropertiesClassName = 'TcxComboBoxProperties'
              Properties.EditProperties.DropDownListStyle = lsFixedList
              Properties.EditProperties.Items.Strings = (
                'Existing'
                'Future')
              Properties.DataBinding.ValueType = 'String'
              Properties.Value = 'Existing'
            end
            object vgrdStorms: TcxEditorRow
              Properties.Caption = 'Storms'
              Properties.EditPropertiesClassName = 'TcxCheckComboBoxProperties'
              Properties.EditProperties.EditValueFormat = cvfIndices
              Properties.EditProperties.Items = <
                item
                end>
              Properties.DataBinding.ValueType = 'String'
              Properties.Value = '0'
            end
            object vgrdRunoffBaseFileName: TcxEditorRow
              Properties.Caption = 'Runoff base name'
              Properties.EditPropertiesClassName = 'TcxTextEditProperties'
              Properties.DataBinding.ValueType = 'String'
              Properties.Value = 'runoff'
            end
            object vgrdEngineBaseFileName: TcxEditorRow
              Properties.Caption = 'Engine base name'
              Properties.EditPropertiesClassName = 'TcxTextEditProperties'
              Properties.DataBinding.ValueType = 'String'
              Properties.Value = 'xpextran'
            end
            object vgrdEngines: TcxEditorRow
              Properties.Caption = 'Engines'
              Properties.EditPropertiesClassName = 'TcxCheckComboBoxProperties'
              Properties.EditProperties.Items = <
                item
                  Description = 'XP-SWMM'
                  ShortDescription = 'XP'
                end
                item
                  Description = 'DHI MOUSE'
                  ShortDescription = 'MOUSE'
                end
                item
                  Description = 'EPA SWMM 5'
                  ShortDescription = 'EPA'
                end>
              Properties.DataBinding.ValueType = 'String'
              Properties.Value = '3'
            end
            object vgrdUseBaseflow: TcxEditorRow
              Properties.Caption = 'Use baseflow/infiltration'
              Properties.EditPropertiesClassName = 'TcxCheckBoxProperties'
              Properties.DataBinding.ValueType = 'String'
              Properties.Value = 'True'
            end
            object vgrdTraceStormwater: TcxEditorRow
              Properties.Caption = 'Trace stormwater'
              Properties.EditPropertiesClassName = 'TcxCheckBoxProperties'
              Properties.DataBinding.ValueType = 'String'
              Properties.Value = 'False'
            end
            object vgrdRemoveSanitaryUponDeploy: TcxEditorRow
              Properties.Caption = 'Remove sanitary upon deploy'
              Properties.EditPropertiesClassName = 'TcxCheckBoxProperties'
              Properties.DataBinding.ValueType = 'String'
              Properties.Value = 'False'
            end
          end
        end
      end
    end
    object RzPanel1: TRzPanel
      Left = 0
      Top = 41
      Width = 415
      Height = 445
      Align = alClient
      BorderOuter = fsNone
      TabOrder = 2
      object RzPanel3: TRzPanel
        Left = 0
        Top = 0
        Width = 415
        Height = 445
        Align = alClient
        BorderOuter = fsNone
        Padding.Left = 8
        Padding.Top = 5
        Padding.Right = 8
        TabOrder = 0
        object grpbModelBoundaries: TRzGroupBox
          Left = 8
          Top = 5
          Width = 399
          Height = 440
          Align = alClient
          Caption = 'Model Boundaries'
          Enabled = False
          GroupStyle = gsTopLine
          TabOrder = 0
          object RzGridPanel1: TRzGridPanel
            Left = 0
            Top = 46
            Width = 399
            Height = 394
            BorderOuter = fsNone
            Align = alClient
            ColumnCollection = <
              item
                Value = 50.000000000000000000
              end
              item
                Value = 50.000000000000000000
              end>
            ControlCollection = <
              item
                Column = 0
                Control = RzLabel2
                Row = 0
              end
              item
                Column = 1
                Control = RzLabel3
                Row = 0
              end
              item
                Column = 0
                Control = memRootPipes
                Row = 1
              end
              item
                Column = 1
                Control = memStopPipes
                Row = 1
              end>
            Enabled = False
            Padding.Left = 5
            Padding.Top = 5
            Padding.Right = 5
            Padding.Bottom = 5
            RowCollection = <
              item
                SizeStyle = ssAuto
                Value = 25.000000000000000000
              end
              item
                Value = 100.000000000000000000
              end>
            TabOrder = 0
            object RzLabel2: TRzLabel
              Left = 10
              Top = 10
              Width = 184
              Height = 13
              Align = alClient
              Caption = 'Root pipes (enter one per line)'
              Enabled = False
              ExplicitWidth = 147
            end
            object RzLabel3: TRzLabel
              Left = 204
              Top = 10
              Width = 185
              Height = 13
              Align = alClient
              Caption = 'Stop pipes (enter one per line)'
              Enabled = False
              ExplicitWidth = 146
            end
            object memRootPipes: TRzMemo
              Left = 10
              Top = 33
              Width = 184
              Height = 351
              Hint = 
                'Enter a root pipe, one per line, with the following examples for' +
                ' MLinkID = 100:'#13#13'100'#13'100='#13'100=Comment describing this root pipe'#39 +
                's significance'
              Align = alClient
              Constraints.MinHeight = 25
              Enabled = False
              ScrollBars = ssVertical
              TabOrder = 0
              OnEnter = memRootPipesEnter
              OnExit = memRootPipesExit
              FrameController = frmMain.RzFrameController1
            end
            object memStopPipes: TRzMemo
              Left = 204
              Top = 33
              Width = 185
              Height = 351
              Hint = 
                'Enter a stop pipe, one per line, with the following examples for' +
                ' MLinkID = 100:'#13#13'100'#13'100='#13'100=Comment describing this stop pipe'#39 +
                's significance'
              Align = alClient
              Constraints.MinHeight = 25
              Enabled = False
              ScrollBars = ssVertical
              TabOrder = 1
              OnEnter = memStopPipesEnter
              OnExit = memStopPipesExit
              FrameController = frmMain.RzFrameController1
            end
          end
          object RzPanel6: TRzPanel
            Left = 0
            Top = 13
            Width = 399
            Height = 33
            Align = alTop
            BorderOuter = fsNone
            Enabled = False
            TabOrder = 1
            object btnCopyModelBoundaries: TRzButton
              Left = 10
              Top = 8
              Width = 186
              Caption = 'Copy from another model...'
              Enabled = False
              HotTrack = True
              TabOrder = 0
              OnClick = btnCopyModelBoundariesClick
            end
          end
        end
      end
    end
    object RzPanel5: TRzPanel
      Left = 0
      Top = 0
      Width = 769
      Height = 41
      Align = alTop
      BorderOuter = fsNone
      TabOrder = 3
      DesignSize = (
        769
        41)
      object RzLabel1: TRzLabel
        Left = 8
        Top = 15
        Width = 75
        Height = 13
        Caption = 'Model Directory'
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = []
        ParentFont = False
      end
      object cmbModelDirectory: TRzMRUComboBox
        Left = 89
        Top = 13
        Width = 496
        Height = 21
        MruRegIniFile = frmMain.regIniFile
        MruSection = 'MRUModels'
        MruID = 'MRUPaths'
        RemoveItemCaption = '&Remove item from history list'
        Anchors = [akLeft, akTop, akRight]
        Ctl3D = False
        Font.Charset = ANSI_CHARSET
        Font.Color = clGrayText
        Font.Height = -11
        Font.Name = 'Tahoma'
        Font.Style = []
        FrameController = frmMain.RzFrameController1
        ItemHeight = 13
        ParentCtl3D = False
        ParentFont = False
        TabOrder = 0
        Text = 'Type a full path and filename, or click Browse...'
        OnChange = cmbModelDirectoryChange
        OnEnter = cmbModelDirectoryEnter
        OnExit = cmbModelDirectoryExit
        OnSelect = cmbModelDirectorySelect
      end
      object btnBrowseModelDirectory: TRzButton
        Left = 591
        Top = 9
        Anchors = [akTop, akRight]
        Caption = 'Browse...'
        HotTrack = True
        TabOrder = 1
        OnClick = btnBrowseModelDirectoryClick
      end
      object btnLoad: TRzButton
        Left = 672
        Top = 9
        Action = actLoadModel
        Anchors = [akTop, akRight]
        HotTrack = True
        TabOrder = 2
      end
    end
    object pnlBuildActionsHolder: TRzPanel
      Left = 0
      Top = 486
      Width = 769
      Height = 48
      Align = alBottom
      TabOrder = 0
      object RzPanel8: TRzPanel
        Left = 2
        Top = 2
        Width = 115
        Height = 44
        Align = alLeft
        BorderOuter = fsNone
        TabOrder = 0
        DesignSize = (
          115
          44)
        object btnHomeAlt: TRzButton
          Left = 8
          Top = 10
          Width = 101
          Anchors = [akLeft, akBottom]
          Caption = '< Welcome'
          HotTrack = True
          TabOrder = 0
          OnClick = btnHomeClick
        end
      end
      object pnlBuildActions: TRzPanel
        Left = 117
        Top = 2
        Width = 650
        Height = 44
        Align = alClient
        BorderOuter = fsNone
        Enabled = False
        TabOrder = 1
        DesignSize = (
          650
          44)
        object btnGotoBuildReport: TRzButton
          Left = 242
          Top = 10
          Width = 197
          Action = actGotoBuildReport
          Anchors = [akRight, akBottom]
          Enabled = False
          HotTrack = True
          TabOrder = 0
        end
        object btnBuildModel: TRzButton
          Left = 445
          Top = 10
          Width = 197
          Anchors = [akRight, akBottom]
          Caption = 'Build model'
          Enabled = False
          HotTrack = True
          TabOrder = 1
          OnClick = btnBuildModelClick
        end
      end
    end
  end
  object pnlProgress: TRzPanel [1]
    Left = 0
    Top = 294
    Width = 761
    Height = 159
    Anchors = [akLeft, akTop, akRight]
    BorderOuter = fsFlatBold
    Constraints.MinWidth = 500
    TabOrder = 0
    DesignSize = (
      761
      159)
    object prgProgress: TRzProgressBar
      Left = 10
      Top = 78
      Width = 540
      Anchors = [akLeft, akRight]
      BackColor = clWindow
      BorderOuter = fsFlat
      BorderWidth = 0
      FlatColorAdjustment = 0
      FrameController = frmMain.RzFrameController1
      InteriorOffset = 0
      PartsComplete = 0
      Percent = 0
      TotalParts = 0
    end
    object lblProgress: TRzLabel
      Left = 10
      Top = 108
      Width = 540
      Height = 41
      Alignment = taCenter
      Anchors = [akLeft, akTop, akRight, akBottom]
      AutoSize = False
      Caption = 'lblProgress'
      WordWrap = True
    end
    object RzLabel4: TRzLabel
      Left = 10
      Top = 6
      Width = 743
      Height = 30
      Alignment = taCenter
      Anchors = [akLeft, akTop, akRight]
      AutoSize = False
      Caption = 'Build in progress'
      Font.Charset = ANSI_CHARSET
      Font.Color = clWindowText
      Font.Height = -19
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
      Layout = tlCenter
      WordWrap = True
      Blinking = True
      BlinkColor = clHotLight
      TextStyle = tsRaised
    end
    object lblStats: TRzLabel
      Left = 10
      Top = 44
      Width = 743
      Height = 17
      Alignment = taCenter
      Anchors = [akLeft, akTop, akRight]
      AutoSize = False
      Caption = 'lblStats'
      Font.Charset = ANSI_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
      Layout = tlCenter
      WordWrap = True
    end
    object btnCancelBuild: TRzButton
      Left = 556
      Top = 76
      Width = 197
      Anchors = [akTop, akRight]
      Caption = 'Cancel build'
      HotTrack = True
      TabOrder = 0
      OnClick = btnCancelBuildClick
    end
  end
  inherited RzPanel4: TRzPanel
    Width = 769
    TabOrder = 2
    ExplicitWidth = 769
    inherited RzBackground1: TRzBackground
      Width = 769
      ExplicitWidth = 769
    end
    inherited lblLabel: TRzLabel
      Width = 769
      Height = 41
      Caption = 'Build'
      ExplicitWidth = 60
    end
    object btnHome: TPngSpeedButton
      Left = 729
      Top = 4
      Width = 40
      Height = 40
      Anchors = [akTop, akRight]
      Flat = True
      OnClick = btnHomeClick
      PngImage.Data = {
        89504E470D0A1A0A0000000D4948445200000020000000200806000000737A7A
        F40000001974455874536F6674776172650041646F626520496D616765526561
        647971C9653C000005534944415478DAB5976D4C535718C7FF2028613A8D4E5D
        B66C9A6882C9CCE29439159DD9A77DF18B6E51A35B060845AC429196524AAB19
        20D6D222E22CA2055AD4CE894133F5CBB604C5F99239716ECB1C9A653A67365F
        B6918150E9CB9E73EE69BD2DE5E58EED26FF9C7B4F9BF3FF9DE73CE7B9E7C661
        74575CD47370B40328B9E24919A404D2119297D4AF14E2DF022408F37AF15C4A
        3A4C7A40EA2505FE4F8044D206922375F654DE71E516F3858D7480748FD43352
        08A50011E6C218B27BBB88CA2FA4BE914028011860DED0D0C07FC8CCCC8C86D8
        03693986851829404CF38C8C0CFE63636363344485588E612146023086943D98
        793048491FF073880DD92AC510C3010C3973661E0CF83840D0EF4793CB85AC8D
        6A398495C4D62994987E2500C39807C85C320641F07B6A9BDC87A0526B460C11
        A7D89CCD5ACC5C3215007E7FB8CF75C8839C3C6D2C885F11B545E3949B07C2B3
        954C23231002711FF9041B0B0CD110F52212E19C880660152E2B96394F366E1E
        39DB903122FAA4B6D9D3825CAD590E5149DA2F4F4C3940BC98797D4C739959D8
        D8FFD410511108F51F3ADA8A4DFAB258BBE33E830801B036937430F4C7BCBC3C
        D4D4D4C8CC7D034CE52011FF8902292EB3A3CE7D2C56247E9703A84875E2F991
        D96C9EB27DFB36B1C6831B0F1D817EDE56D638B16B9FEB6F1A778218BF99B48F
        74430E309EB485348734C36C32BDB9CD6C1A62F0C8BE81517A0A5759DB08ABA3
        F947B1F60F496748D74837E5002CFBD9EB6D2669B3A9D4B8769BC9C807686B6B
        C3D9B3ED7C078476024BC8658B52B16CF1EB3877FE22CE5DB82C256928594969
        A9AF6269EA5CECFCC8056BDDE16F69DC93A4EF4877497718903C09D9FD38D2B3
        2493C968D86C361A38C087E595DC60F9F2E5F0F97C5CEDEDED786BF16B28D9BA
        0915557BF165C70F5890B60C5D4FFCE8EAF7E1DA85F378277536F4B9EB08A019
        55F51E36E32652BB00604BE28DDE862188F25283BED06CD44B001516C48F4D86
        C160405F5F1F7A7B7B61B158901CEF4589662376D8F7E171F234ACDBAAC7ED9E
        27B8D3E345EBEE5D481BF30045396B61711C46D581A31DA20E7C2E00D8092A18
        AB108DE500C53A9DC9A0E300659556F8E3C741ABD57200269BCD8689893E18F2
        55D8B1BB0EF7129FC3DB6A2D37676A73D8B122E9118A54AB61A9F3C076F05887
        C8FCCF04C093C12A210730EA0B75A6E2429E50653B6DF00613A0D168C211A8AD
        ADC5E4A4000C795904508FEF31196F646B70A7DB4B51F0E27AE31EBC3FFE4FE8
        B2DFC5AEFAA3B0398F2B04282AD0951615F008945BAAD1DD1F07B55A1D8E80C3
        E1C0F4F1F128DE9C8ECA3D4E7C7CE906A6CE5B18CE81BFAE7F85BC25B3A0CB5A
        4500C7606F6C550650A2CD27807C1E81726B0DBAFA8250A9547CF64C4EA7132F
        4E4A40B1FA03B45FFA1AE72F7744EC00D62E9937879402ABF338EC4D27150214
        6ED119B55B78042AACB578D8E3437A7A7A7809DC6E37664E49827ED37B117B1E
        FE8145CADAD08A6AD7A7CA000C5BD53A63A19A179A0ADB5EFCD6E5C5FAF5EBC3
        4BE0F178306B5A32DF66030A923FB24855359D44B5FBB44280825C5D4941AE14
        01BB03771F3DC69A356BC211686969C19C1726F06D16F10EF0F907BC176CAE53
        A86E3EA30CA0383F4757A2C9E103ECD8BD1F3FDFEFC6CA952BC3113871E204E6
        BE348900563F0D774414A43EF6A164739F660057451D18168095E48AB485F375
        4B17CDA7C102946057F1076DAF9494947025ECECECC4F4894958B2E015714E90
        CAB43C0943D7C56F3A71E9FAAD2B22025F0880FEA100569056919E273D03E9AC
        309A8BD1DC249D225D807416181480994D24BD4C9A01E9DD30E63F006067C1DB
        A49F485D18E44816EA4B14334F16F7A38D00FB62F609886EC8BEA2FF010E5E51
        90E9BE57B40000000049454E44AE426082}
    end
  end
  object dlgSelectFolder: TRzSelectFolderDialog
    Options = [sfdoCreateDeleteButtons, sfdoContextMenus, sfdoCreateFolderIcon, sfdoDeleteFolderIcon, sfdoShowHidden]
    Left = 252
    Top = 484
  end
  object ActionManager1: TActionManager
    Left = 112
    Top = 484
    StyleName = 'XP Style'
    object actLoadModel: TAction
      Caption = 'Load'
      OnExecute = actLoadModelExecute
      OnUpdate = actLoadModelUpdate
    end
    object actGotoBuildReport: TAction
      Caption = 'Go to build report'
      OnExecute = actGotoBuildReportExecute
      OnUpdate = actGotoBuildReportUpdate
    end
    object actPresetBuildAll: TAction
      Category = 'Preset'
      Caption = 'Build all'
      OnExecute = actPresetBuildAllExecute
    end
    object actPresetRecalculateHydrology: TAction
      Category = 'Preset'
      Caption = 'Recalculate hydrology'
      OnExecute = actPresetRecalculateHydrologyExecute
    end
    object actPresetIndividualOptions: TAction
      Category = 'Preset'
      Caption = 'Individual options'
      OnExecute = actPresetIndividualOptionsExecute
    end
    object actPresetDeployAlternative: TAction
      Category = 'Preset'
      Caption = 'Deploy alternative'
      OnExecute = actPresetDeployAlternativeExecute
    end
    object actPresetDeployModelFiles: TAction
      Category = 'Preset'
      Caption = 'Deploy model files'
      OnExecute = actPresetDeployModelFilesExecute
    end
    object actPresetCreatePreset: TAction
      Category = 'Preset'
      Caption = 'Create a preset'
    end
    object actPresetTraceNetworkOnly: TAction
      Category = 'Preset'
      Caption = 'Trace network only'
      OnExecute = actPresetTraceNetworkOnlyExecute
    end
    object actPresetBuildAllStormwaterControls: TAction
      Category = 'Preset'
      Caption = 'Build all stormwater controls'
      OnExecute = actPresetBuildAllStormwaterControlsExecute
    end
    object actPresetBuildStreetStormwaterControls: TAction
      Category = 'Preset'
      Caption = 'Build street stormwater controls'
      OnExecute = actPresetBuildStreetStormwaterControlsExecute
    end
  end
  object mnuPopup: TPopupMenu
    Left = 36
    Top = 484
    object mnuPresetBuildAll: TMenuItem
      Action = actPresetBuildAll
    end
    object racenetworkonly1: TMenuItem
      Action = actPresetTraceNetworkOnly
    end
    object Deployalternative1: TMenuItem
      Action = actPresetDeployAlternative
    end
    object mnuPresetRecalculateHydrology: TMenuItem
      Action = actPresetRecalculateHydrology
    end
    object mnuPresetBuildStormwaterControls: TMenuItem
      Action = actPresetBuildAllStormwaterControls
    end
    object Buildstreetstormwatercontrols1: TMenuItem
      Action = actPresetBuildStreetStormwaterControls
    end
    object Deploymodelfiles1: TMenuItem
      Action = actPresetDeployModelFiles
    end
    object Createapreset1: TMenuItem
      Action = actPresetCreatePreset
    end
    object Gotobuildreport1: TMenuItem
      Caption = '-'
      OnClick = actGotoBuildReportExecute
    end
    object Individualoptions1: TMenuItem
      Action = actPresetIndividualOptions
    end
  end
  object adoConnection: TADOConnection
    ConnectionString = 
      'Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\AGMaster21\mdbs\' +
      'LookupTables.mdb;Persist Security Info=False'
    LoginPrompt = False
    Mode = cmShareDenyNone
    Provider = 'Microsoft.Jet.OLEDB.4.0'
    Left = 44
    Top = 428
  end
  object adoStorms: TADOTable
    Connection = adoConnection
    TableName = 'Storms'
    Left = 112
    Top = 428
  end
end
