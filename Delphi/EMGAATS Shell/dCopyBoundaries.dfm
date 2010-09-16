object dlgCopyBoundaries: TdlgCopyBoundaries
  Left = 327
  Top = 256
  Caption = 'Copy Boundaries from Another Model'
  ClientHeight = 204
  ClientWidth = 364
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Verdana'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  OnShow = FormShow
  DesignSize = (
    364
    204)
  PixelsPerInch = 96
  TextHeight = 13
  object RzLabel1: TRzLabel
    Left = 8
    Top = 48
    Width = 90
    Height = 13
    Caption = 'Model Directory'
  end
  object RzDialogButtons1: TRzDialogButtons
    Left = 0
    Top = 168
    Width = 364
    CaptionOk = 'OK'
    CaptionCancel = 'Cancel'
    CaptionHelp = '&Help'
    HotTrack = True
    ModalResultOk = 1
    ModalResultCancel = 2
    ModalResultHelp = 0
    TabOrder = 0
    ExplicitTop = 169
  end
  object cmbModelDirectory: TRzMRUComboBox
    Left = 8
    Top = 64
    Width = 346
    Height = 21
    RemoveItemCaption = '&Remove item from history list'
    SelectFirstItemOnLoad = True
    Anchors = [akLeft, akTop, akRight]
    Ctl3D = False
    FrameController = frmMain.RzFrameController1
    ItemHeight = 13
    ParentCtl3D = False
    TabOrder = 1
  end
  object btnSelectDirectory: TRzButton
    Left = 225
    Top = 88
    Width = 127
    Anchors = [akTop, akRight]
    Caption = 'Select Directory...'
    HotTrack = True
    TabOrder = 2
    OnClick = btnSelectDirectoryClick
  end
  object dlgSelectDirectory: TRzSelectFolderDialog
    Options = [sfdoContextMenus, sfdoReadOnly, sfdoShowHidden]
    Left = 180
    Top = 12
  end
end
