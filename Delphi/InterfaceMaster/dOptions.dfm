object dlgOptions: TdlgOptions
  Left = 294
  Top = 459
  Width = 748
  Height = 471
  Caption = 'InterfaceMaster Options'
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object dlgbtnOptions: TRzDialogButtons
    Left = 0
    Top = 408
    Width = 740
    CaptionOk = 'OK'
    CaptionCancel = 'Cancel'
    CaptionHelp = '&Help'
    ShowDivider = True
    OnClickOk = dlgbtnOptionsClickOk
    TabOrder = 0
  end
  object RzGroupBar1: TRzGroupBar
    Left = 0
    Top = 0
    Height = 408
    ColorAdjustment = 30
    GroupBorderSize = 4
    Style = gbsTaskList
    UseGradients = False
    Color = clWindow
    ParentColor = False
    TabOrder = 1
    object RzGroup1: TRzGroup
      CanClose = False
      Items = <
        item
          Caption = 'Convert Interface File'
        end
        item
          Caption = 'Calculate Flow Statistics'
        end
        item
          Caption = 'Prepare Virtual Gages'
          OnClick = RzGroup1Items2Click
        end>
      Opened = True
      OpenedHeight = 87
      DividerVisible = True
      UseGradients = True
      Caption = 'Options'
    end
  end
  object pageOptions: TRzPageControl
    Left = 160
    Top = 0
    Width = 580
    Height = 408
    ActivePage = shtPrepareVirtualGages
    Align = alClient
    ShowShadow = False
    TabOrder = 2
    object shtConvertInterfaceFile: TRzTabSheet
      TabVisible = False
      Caption = 'shtConvertInterfaceFile'
      object RzLabel7: TRzLabel
        Left = 0
        Top = 0
        Width = 578
        Height = 33
        Align = alTop
        AutoSize = False
        Caption = ' Convert Interface File Options'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -19
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
        Layout = tlCenter
        TextStyle = tsRaised
      end
    end
    object shtCalculateFlowStatistics: TRzTabSheet
      TabVisible = False
      Caption = 'shtCalculateFlowStatistics'
      object RzLabel6: TRzLabel
        Left = 0
        Top = 0
        Width = 578
        Height = 33
        Align = alTop
        AutoSize = False
        Caption = ' Calculate Flow Statistics Options'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -19
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
        Layout = tlCenter
        TextStyle = tsRaised
      end
    end
    object shtPrepareVirtualGages: TRzTabSheet
      TabVisible = False
      Caption = 'shtPrepareVirtualGages'
      DesignSize = (
        578
        406)
      object RzLabel1: TRzLabel
        Left = 8
        Top = 40
        Width = 143
        Height = 13
        Caption = 'Quartersections Map Location'
      end
      object RzLabel2: TRzLabel
        Left = 8
        Top = 64
        Width = 137
        Height = 13
        Caption = 'Raingage Database Location'
      end
      object RzLabel3: TRzLabel
        Left = 8
        Top = 88
        Width = 172
        Height = 13
        Caption = 'Raingage Builder Database Location'
      end
      object RzLabel4: TRzLabel
        Left = 8
        Top = 112
        Width = 123
        Height = 13
        Caption = 'Combined Basins Location'
      end
      object RzLabel5: TRzLabel
        Left = 0
        Top = 0
        Width = 578
        Height = 33
        Align = alTop
        AutoSize = False
        Caption = ' Prepare Virtual Gages Options'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -19
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
        Layout = tlCenter
        TextStyle = tsRaised
      end
      object edtQuartersectionsMapLocation: TRzButtonEdit
        Left = 188
        Top = 36
        Width = 383
        Height = 21
        Anchors = [akLeft, akTop, akRight]
        FrameController = frmMain.RzFrameController1
        TabOrder = 0
        ButtonKind = bkFolder
        OnButtonClick = edtQuartersectionsMapLocationButtonClick
      end
      object edtRaingageDatabaseLocation: TRzButtonEdit
        Left = 188
        Top = 60
        Width = 383
        Height = 21
        Anchors = [akLeft, akTop, akRight]
        FrameController = frmMain.RzFrameController1
        TabOrder = 1
        ButtonKind = bkFolder
        OnButtonClick = edtRaingageDatabaseLocationButtonClick
      end
      object edtRaingageBuilderDatabaseLocation: TRzButtonEdit
        Left = 188
        Top = 84
        Width = 383
        Height = 21
        Anchors = [akLeft, akTop, akRight]
        FrameController = frmMain.RzFrameController1
        TabOrder = 2
        ButtonKind = bkFolder
        OnButtonClick = edtRaingageBuilderDatabaseLocationButtonClick
      end
      object edtCombinedBasinsLocation: TRzButtonEdit
        Left = 188
        Top = 108
        Width = 383
        Height = 21
        Anchors = [akLeft, akTop, akRight]
        FrameController = frmMain.RzFrameController1
        TabOrder = 3
        ButtonKind = bkFolder
        OnButtonClick = edtCombinedBasinsLocationButtonClick
      end
    end
  end
end
