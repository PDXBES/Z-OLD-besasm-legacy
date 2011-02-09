object dlgFindLink: TdlgFindLink
  Left = 425
  Top = 686
  BorderStyle = bsDialog
  Caption = 'Find Link'
  ClientHeight = 181
  ClientWidth = 373
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 8
    Top = 48
    Width = 247
    Height = 13
    Caption = 'Enter a CompKey (click drop-down arrow to search)'
  end
  object RzDialogButtons1: TRzDialogButtons
    Left = 0
    Top = 145
    Width = 373
    CaptionOk = 'OK'
    CaptionCancel = 'Cancel'
    CaptionHelp = '&Help'
    FrameFlat = True
    TabOrder = 0
  end
  object edtFindLink: TdxPopupEdit
    Left = 8
    Top = 64
    Width = 357
    TabOrder = 1
    StyleController = frmMain.dxStyle
    PopupControl = pnlLookup
    OnCloseUp = edtFindLinkCloseUp
  end
  object pnlLookup: TRzPanel
    Left = 16
    Top = 8
    Width = 357
    Height = 157
    BorderOuter = fsNone
    TabOrder = 2
    Visible = False
    object RzLabel1: TRzLabel
      Left = 0
      Top = 0
      Width = 357
      Height = 13
      Align = alTop
      Caption = 'Type in a column to search'
      FrameSides = []
    end
    object dxDBGrid1: TdxDBGrid
      Left = 0
      Top = 13
      Width = 357
      Height = 108
      Bands = <
        item
        end>
      DefaultLayout = True
      HeaderPanelRowCount = 1
      KeyField = 'LinkID'
      SummaryGroups = <>
      SummarySeparator = ', '
      Align = alClient
      TabOrder = 0
      DataSource = frmMain.srcNetwork
      Filter.Criteria = {00000000}
      LookAndFeel = lfFlat
      OptionsBehavior = [edgoAutoSearch, edgoAutoSort, edgoDragScroll, edgoEditing, edgoEnterShowEditor, edgoImmediateEditor, edgoTabThrough, edgoVertThrough]
      OptionsView = [edgoAutoWidth, edgoBandHeaderWidth, edgoUseBitmap]
      object dxDBGrid1LinkID: TdxDBGridMaskColumn
        DisableEditor = True
        BandIndex = 0
        RowIndex = 0
        FieldName = 'LinkID'
      end
      object dxDBGrid1MLinkID: TdxDBGridMaskColumn
        DisableEditor = True
        BandIndex = 0
        RowIndex = 0
        FieldName = 'MLinkID'
      end
      object dxDBGrid1USNode: TdxDBGridColumn
        DisableEditor = True
        BandIndex = 0
        RowIndex = 0
        FieldName = 'USNode'
      end
      object dxDBGrid1CompKey: TdxDBGridMaskColumn
        DisableEditor = True
        BandIndex = 0
        RowIndex = 0
        FieldName = 'CompKey'
      end
      object dxDBGrid1DSNode: TdxDBGridColumn
        DisableEditor = True
        BandIndex = 0
        RowIndex = 0
        FieldName = 'DSNode'
      end
    end
    object dlgbSearch: TRzDialogButtons
      Left = 0
      Top = 121
      Width = 357
      CaptionOk = 'OK'
      CaptionCancel = 'Cancel'
      CaptionHelp = '&Help'
      ShowCancelButton = False
      OnClickOk = dlgbSearchClickOk
      TabOrder = 1
    end
  end
end
