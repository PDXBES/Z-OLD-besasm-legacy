object frmMain: TfrmMain
  Left = 206
  Top = 209
  Caption = 'EMGAATS Framework'
  ClientHeight = 506
  ClientWidth = 712
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poDesigned
  ShowHint = True
  OnCreate = FormCreate
  OnDestroy = FormDestroy
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object RzSizePanel1: TRzSizePanel
    Left = 0
    Top = 23
    Width = 8
    Height = 483
    Hint = 
      'Click hot-spot to shrink or expand tasks panel, or drag the grip' +
      ' to resize manually.'
    HotSpotVisible = True
    RealTimeDrag = True
    SizeBarStyle = ssBump
    SizeBarWidth = 7
    TabOrder = 0
    Visible = False
    VisualStyle = vsGradient
    HotSpotClosed = True
    HotSpotPosition = 185
    object RzGroupBar1: TRzGroupBar
      Left = 0
      Top = 0
      Width = 0
      Height = 483
      GradientColorStart = clBtnFace
      GradientColorStop = clBtnShadow
      GroupBorderSize = 4
      Style = gbsTaskList
      Align = alClient
      Color = clWindow
      ParentColor = False
      TabOrder = 0
      object RzGroup1: TRzGroup
        CanClose = False
        Items = <
          item
            Caption = '&New Model...'
            Hint = 'New Model...'
            ImageIndex = 0
          end
          item
            Caption = '&Open Model...'
            Hint = 'Open Model...'
            ImageIndex = 1
          end>
        Opened = True
        OpenedHeight = 92
        DividerVisible = True
        Caption = 'Tasks'
      end
      object RzGroup3: TRzGroup
        CanClose = False
        Items = <
          item
            Caption = 'Build pipe system'
          end
          item
            Caption = 'Build laterals and parcels'
          end
          item
            Caption = 'Build subcatchments'
          end
          item
            Caption = 'Deploy model'
          end
          item
            Caption = 'Perform quality control'
          end>
        Opened = True
        OpenedHeight = 240
        DividerVisible = True
        Caption = 'Model Building Tasks'
      end
      object RzGroup2: TRzGroup
        CanClose = False
        Items = <
          item
            Caption = 'InterfaceMaster'
          end
          item
            Caption = 'Profiler'
          end
          item
            Caption = 'UpstreamTracer'
          end
          item
            Caption = 'Hoptimizer'
          end
          item
            Caption = 'Restart Workbenches'
          end>
        Opened = True
        OpenedHeight = 140
        DividerVisible = True
        Caption = 'Tools'
      end
    end
  end
  object RzPanel1: TRzPanel
    Left = 8
    Top = 23
    Width = 704
    Height = 483
    Align = alClient
    BorderOuter = fsNone
    TabOrder = 5
    DesignSize = (
      704
      483)
    object RzBackground1: TRzBackground
      Left = 0
      Top = 0
      Width = 704
      Height = 483
      Active = True
      Align = alClient
      ImageStyle = isStretch
      ParentShowHint = False
      ShowGradient = True
      ShowHint = False
      ShowImage = False
      ShowTexture = False
      ExplicitLeft = 664
      ExplicitTop = 472
      ExplicitWidth = 200
      ExplicitHeight = 100
    end
    object RzPanel2: TRzPanel
      Left = 12
      Top = 12
      Width = 689
      Height = 459
      Anchors = [akLeft, akTop, akRight, akBottom]
      BorderOuter = fsNone
      TabOrder = 0
      object pnlMain: TRzPanel
        Left = 0
        Top = 0
        Width = 689
        Height = 440
        Align = alClient
        BorderOuter = fsNone
        TabOrder = 0
      end
      object RzStatusBar1: TRzStatusBar
        Left = 0
        Top = 440
        Width = 689
        Height = 19
        BorderInner = fsNone
        BorderOuter = fsNone
        BorderSides = [sdLeft, sdTop, sdRight, sdBottom]
        BorderWidth = 0
        TabOrder = 1
        object paneGlyphStatus: TRzGlyphStatus
          Left = 0
          Top = 0
          Width = 500
          Height = 19
          Align = alLeft
        end
        object paneTextStatus: TRzStatusPane
          Left = 500
          Top = 0
          Width = 189
          Height = 19
          Align = alClient
          ExplicitLeft = 685
          ExplicitWidth = 100
          ExplicitHeight = 20
        end
      end
    end
  end
  object actManager: TActionManager
    Images = ImageList1
    Left = 216
    Top = 288
    StyleName = 'XP Style'
    object actFileExit: TFileExit
      Category = 'File'
      Caption = 'E&xit'
      Hint = 'Exit|Quits the application'
      ImageIndex = 43
      ShortCut = 32883
    end
    object actHelpSendTicket: TAction
      Category = 'Help'
      Caption = '&Send ticket...'
    end
    object actToolsOptions: TAction
      Category = 'Tools'
      Caption = '&Options...'
      OnExecute = actToolsOptionsExecute
    end
  end
  object dlgOpen: TOpenDialog
    Left = 216
    Top = 192
  end
  object ImageList1: TImageList
    Left = 216
    Top = 240
    Bitmap = {
      494C010102000400040010001000FFFFFFFFFF10FFFFFFFFFFFFFFFF424D3600
      0000000000003600000028000000400000001000000001002000000000000010
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
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
      FF00FFFFFF000000000000000000000000000000000000000000000000000080
      8000008080000080800000808000008080000080800000808000008080000080
      8000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000FFFFFF000000
      0000008080000080800000808000008080000080800000808000008080000080
      8000008080000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00000000000000000000000000000000000000000000FFFF00FFFF
      FF00000000000080800000808000008080000080800000808000008080000080
      8000008080000080800000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000FFFFFF0000FF
      FF00FFFFFF000000000000808000008080000080800000808000008080000080
      8000008080000080800000808000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00000000000000000000000000000000000000000000FFFF00FFFF
      FF0000FFFF00FFFFFF0000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000FFFFFF0000FF
      FF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF00000000000000000000000000000000000000000000FFFF00FFFF
      FF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF00FFFFFF0000FFFF000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFF
      FF00FFFFFF000000000000000000000000000000000000000000FFFFFF0000FF
      FF00FFFFFF000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF0000000000FFFF
      FF00000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00FFFFFF00000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
      0000000000000000000000000000000000000000000000000000000000000000
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
      2800000040000000100000000100010000000000800000000000000000000000
      000000000000000000000000FFFFFF00FFFFFFFF00000000E003800F00000000
      E003800700000000E003800300000000E003800100000000E003800000000000
      E003800000000000E003800F00000000E003800F00000000E003800F00000000
      E003C7F800000000E007FFFC00000000E00FFFBA00000000E01FFFC700000000
      FFFFFFFF00000000FFFFFFFF0000000000000000000000000000000000000000
      000000000000}
  end
  object RzFrameController1: TRzFrameController
    FrameHotTrack = True
    FrameVisible = True
    Left = 44
    Top = 44
  end
  object dlgSelectDirectory: TRzSelectFolderDialog
    Options = [sfdoCreateDeleteButtons, sfdoContextMenus, sfdoCreateFolderIcon, sfdoDeleteFolderIcon, sfdoVirtualFolders, sfdoShowHidden]
    Left = 368
    Top = 148
  end
  object appLauncher: TRzLauncher
    Action = 'Open'
    Timeout = -1
    Left = 368
    Top = 196
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
    Left = 289
    Top = 253
  end
  object barManager: TdxBarManager
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    Bars = <
      item
        Caption = 'MainMenu'
        DockedDockingStyle = dsTop
        DockedLeft = 0
        DockedTop = 0
        DockingStyle = dsTop
        FloatLeft = 386
        FloatTop = 261
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
            Item = mnuTools
            Visible = True
          end
          item
            Item = mnuHelp
            Visible = True
          end>
        MultiLine = True
        Name = 'MainMenu'
        OneOnRow = True
        Row = 0
        UseOwnFont = False
        Visible = True
        WholeRow = True
      end>
    Categories.Strings = (
      'Default'
      'File'
      'Model'
      'View'
      'Tools'
      'Help')
    Categories.ItemsVisibles = (
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
      True)
    LookAndFeel.Kind = lfOffice11
    LookAndFeel.NativeStyle = False
    PopupMenuLinks = <>
    Style = bmsUseLookAndFeel
    UseSystemFont = True
    Left = 484
    Top = 44
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
          Item = mnuFileExit
          Visible = True
        end>
    end
    object mnuFileNewModel: TdxBarButton
      Caption = '&New Model...'
      Category = 1
      Hint = 'New Model...'
      Visible = ivAlways
      ImageIndex = 0
      ShortCut = 16462
    end
    object mnuFileOpenModel: TdxBarButton
      Caption = '&Open Model...'
      Category = 1
      Hint = 'Open Model...'
      Visible = ivAlways
      ImageIndex = 1
      ShortCut = 16463
    end
    object mnuFileExit: TdxBarButton
      Action = actFileExit
      Category = 1
    end
    object mnuView: TdxBarSubItem
      Caption = '&View'
      Category = 3
      Visible = ivAlways
      ItemLinks = <>
    end
    object mnuTools: TdxBarSubItem
      Caption = '&Tools'
      Category = 4
      Visible = ivAlways
      ItemLinks = <
        item
          Item = mnuToolsOptions
          Visible = True
        end>
    end
    object mnuHelp: TdxBarSubItem
      Caption = '&Help'
      Category = 5
      Visible = ivAlways
      ItemLinks = <
        item
          Item = mnuHelpSendTicket
          Visible = True
        end>
    end
    object mnuHelpSendTicket: TdxBarButton
      Action = actHelpSendTicket
      Category = 5
    end
    object mnuToolsOptions: TdxBarButton
      Action = actToolsOptions
      Category = 4
    end
    object mnuModel: TdxBarSubItem
      Caption = 'Model'
      Category = 2
      Visible = ivAlways
      ItemLinks = <
        item
          Item = mnuModelBuild
          Visible = True
        end>
    end
    object mnuModelBuild: TdxBarButton
      Caption = 'Build'
      Category = 2
      Hint = 'Build'
      Visible = ivAlways
    end
  end
  object RzMenuController1: TRzMenuController
    Left = 44
    Top = 92
  end
  object RzFormState1: TRzFormState
    Section = 'Application'
    RegIniFile = regIniFile
    Left = 44
    Top = 140
  end
  object regIniFile: TRzRegIniFile
    Left = 44
    Top = 188
  end
  object styleRepository: TcxStyleRepository
    Left = 484
    Top = 92
  end
  object CSGlobalObject1: TCSGlobalObject
    CategoryColor = clWindow
    CategoryFontColor = clWindowText
    Left = 44
    Top = 240
  end
  object versionInfo: TRzVersionInfo
    Left = 216
    Top = 352
  end
end
