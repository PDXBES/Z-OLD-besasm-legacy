object frmMain: TfrmMain
  Left = 250
  Top = 200
  Width = 640
  Height = 480
  Caption = 'TAR Collector'
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnCreate = FormCreate
  OnDestroy = FormDestroy
  DesignSize = (
    632
    453)
  PixelsPerInch = 96
  TextHeight = 13
  object RzLabel1: TRzLabel
    Left = 8
    Top = 44
    Width = 67
    Height = 13
    Caption = 'TAR Directory'
  end
  object RzLabel2: TRzLabel
    Left = 8
    Top = 112
    Width = 507
    Height = 13
    Caption = 
      'TAR Listing to Collect to Accounting Directory.  Check Include? ' +
      'column to determine what will be collected.'
  end
  object RzBorder1: TRzBorder
    Left = 8
    Top = 40
    Width = 613
    Height = 9
    BorderSides = [sdTop]
    Anchors = [akLeft, akTop, akRight]
  end
  object RzLabel3: TRzLabel
    Left = 8
    Top = 8
    Width = 612
    Height = 26
    Anchors = [akLeft, akTop, akRight]
    Caption = 
      'This program collects all active users'#39' latest Direct Billing Ti' +
      'mesheets into shortcuts that can be found in the Accounting dire' +
      'ctory.'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    ParentFont = False
    WordWrap = True
  end
  object RzBorder2: TRzBorder
    Left = 12
    Top = 420
    Width = 613
    Height = 9
    BorderSides = [sdTop]
    Anchors = [akLeft, akRight, akBottom]
  end
  object edtTARDirectory: TRzButtonEdit
    Left = 8
    Top = 60
    Width = 613
    Height = 21
    Hint = 'Click the folder button on the right to browse for a directory.'
    Anchors = [akLeft, akTop, akRight]
    FrameController = RzFrameController1
    ParentShowHint = False
    ShowHint = True
    TabOrder = 0
    ButtonKind = bkFolder
    OnButtonClick = edtTARDirectoryButtonClick
  end
  object btnManageDirectBillingPersons: TRzButton
    Left = 276
    Top = 84
    Width = 161
    Hint = 
      'Click to edit active users (when they move into or out of charge' +
      'back status) or to create new ones.'
    Action = actManageDirectoryBillingPersons
    Anchors = [akTop, akRight]
    HotTrack = True
    ParentShowHint = False
    ShowHint = True
    TabOrder = 1
  end
  object btnCollate: TRzButton
    Left = 440
    Top = 392
    Width = 183
    Hint = 
      'Collect all the TARs above (with the Include column checked) int' +
      'o a convenient location, Accounting, for further processing by t' +
      'hat department.'
    Action = actCollectTARs
    Anchors = [akRight, akBottom]
    HotTrack = True
    ParentShowHint = False
    ShowHint = True
    TabOrder = 2
  end
  object btnFindTARs: TRzButton
    Left = 440
    Top = 84
    Width = 183
    Hint = 
      'Search for TARs in the Individual subdirectory underneath the sp' +
      'ecified TAR directory.'
    Action = actFindTARs
    Anchors = [akTop, akRight]
    HotTrack = True
    ParentShowHint = False
    ShowHint = True
    TabOrder = 3
  end
  object RzButton1: TRzButton
    Left = 12
    Top = 424
    Width = 161
    Action = FileExit1
    Anchors = [akLeft, akBottom]
    HotTrack = True
    ParentShowHint = False
    ShowHint = True
    TabOrder = 4
  end
  object grdTars: TcxTreeList
    Left = 8
    Top = 132
    Width = 613
    Height = 253
    Anchors = [akLeft, akTop, akRight, akBottom]
    Bands = <
      item
      end>
    BufferedPaint = False
    LookAndFeel.Kind = lfUltraFlat
    OptionsView.ColumnAutoWidth = True
    OptionsView.GridLines = tlglHorz
    OptionsView.Indicator = True
    OptionsView.ShowRoot = False
    TabOrder = 5
    OnCustomDrawCell = grdTarsCustomDrawCell
    object colInclude: TcxTreeListColumn
      PropertiesClassName = 'TcxCheckBoxProperties'
      Caption.Text = 'Include?'
      DataBinding.ValueType = 'String'
      Width = 30
      Position.ColIndex = 0
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object colUser: TcxTreeListColumn
      PropertiesClassName = 'TcxTextEditProperties'
      Caption.Text = 'User'
      DataBinding.ValueType = 'String'
      Width = 50
      Position.ColIndex = 1
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object colFileName: TcxTreeListColumn
      PropertiesClassName = 'TcxTextEditProperties'
      Caption.Text = 'File Name'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 2
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object colFileDate: TcxTreeListColumn
      PropertiesClassName = 'TcxDateEditProperties'
      Caption.Text = 'File Date'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 3
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object colEditFile: TcxTreeListColumn
      PropertiesClassName = 'TcxButtonEditProperties'
      Properties.Buttons = <
        item
          Default = True
          Kind = bkEllipsis
        end>
      Properties.ViewStyle = vsButtonsOnly
      Properties.OnButtonClick = colEditFilePropertiesButtonClick
      Caption.Text = 'Edit'
      DataBinding.ValueType = 'String'
      Options.ShowEditButtons = eisbAlways
      Width = 20
      Position.ColIndex = 4
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
  end
  object ActionManager1: TActionManager
    Left = 200
    Top = 172
    StyleName = 'XP Style'
    object actFindTARs: TAction
      Caption = 'Find TARs'
      OnExecute = actFindTARsExecute
    end
    object actCollectTARs: TAction
      Caption = 'Collect TARs'
      OnExecute = actCollectTARsExecute
    end
    object actManageDirectoryBillingPersons: TAction
      Caption = 'Manage Direct Billing Persons'
      OnExecute = actManageDirectoryBillingPersonsExecute
    end
    object FileExit1: TFileExit
      Category = 'File'
      Caption = 'E&xit'
      Hint = 'Quits the application'
      ImageIndex = 43
    end
  end
  object RzFrameController1: TRzFrameController
    FrameHotTrack = True
    FrameVisible = True
    Left = 280
    Top = 172
  end
  object dlgSelectFolder: TRzSelectFolderDialog
    Options = [sfdoContextMenus, sfdoShowHidden]
    Left = 384
    Top = 172
  end
  object RzFormState1: TRzFormState
    Section = 'MainFormState'
    RegIniFile = RegSettings
    Left = 460
    Top = 172
  end
  object RegSettings: TRzRegIniFile
    PathType = ptRegistry
    Left = 536
    Top = 172
  end
  object RzVersionInfo1: TRzVersionInfo
    Left = 104
    Top = 176
  end
  object RzBalloonHints1: TRzBalloonHints
    Alignment = taLeftJustify
    Bitmaps.TransparentColor = clOlive
    CaptionWidth = 200
    Color = clInfoBk
    FrameColor = clBlack
    Font.Charset = ANSI_CHARSET
    Font.Color = clInfoText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    HintPause = 500
    Shadow = True
    Left = 104
    Top = 228
  end
  object dlgOpen: TRzOpenDialog
    Left = 200
    Top = 228
  end
end
