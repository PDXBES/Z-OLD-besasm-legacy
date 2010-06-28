object dlgSpecifyExtractDates: TdlgSpecifyExtractDates
  Left = 118
  Top = 527
  BorderStyle = bsDialog
  Caption = 'Specify Extract Dates'
  ClientHeight = 275
  ClientWidth = 381
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnClose = FormClose
  PixelsPerInch = 96
  TextHeight = 13
  object RzLabel1: TRzLabel
    Left = 8
    Top = 8
    Width = 226
    Height = 13
    Caption = 'Enter date ranges to extract from interface file'
  end
  object Label1: TLabel
    Left = 116
    Top = 196
    Width = 50
    Height = 13
    Caption = 'Start Date'
  end
  object RzDialogButtons1: TRzDialogButtons
    Left = 0
    Top = 239
    Width = 381
    CaptionOk = 'OK'
    CaptionCancel = 'Cancel'
    CaptionHelp = '&Help'
    HotTrack = True
    TabOrder = 1
  end
  object btnAdd: TRzButton
    Left = 296
    Top = 28
    Caption = 'Add'
    HotTrack = True
    TabOrder = 0
    OnClick = btnAddClick
  end
  object btnDelete: TRzButton
    Left = 296
    Top = 56
    Caption = 'Delete'
    HotTrack = True
    TabOrder = 2
    OnClick = btnDeleteClick
  end
  object btnClear: TRzButton
    Left = 296
    Top = 84
    Caption = 'Clear'
    HotTrack = True
    TabOrder = 3
    OnClick = btnClearClick
  end
  object treeDateRanges: TcxTreeList
    Left = 8
    Top = 24
    Width = 281
    Height = 165
    Bands = <
      item
      end>
    BufferedPaint = False
    OptionsView.ColumnAutoWidth = True
    TabOrder = 4
    OnEdited = treeDateRangesEdited
    object cxColStartDate: TcxTreeListColumn
      PropertiesClassName = 'TcxDateEditProperties'
      Caption.Text = 'Start'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 0
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object cxColEndDate: TcxTreeListColumn
      PropertiesClassName = 'TcxDateEditProperties'
      Caption.Text = 'End'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 1
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
  end
  object chkReorderDates: TRzCheckBox
    Left = 8
    Top = 196
    Width = 97
    Height = 17
    Caption = 'Reorder dates'
    State = cbUnchecked
    TabOrder = 5
  end
  object chkAddDryDay: TRzCheckBox
    Left = 8
    Top = 212
    Width = 97
    Height = 17
    Caption = 'Add dry day between each range'
    State = cbUnchecked
    TabOrder = 6
  end
  object edtStartDatae: TRzDateTimeEdit
    Left = 168
    Top = 192
    Width = 121
    Height = 21
    EditType = etDate
    TabOrder = 7
  end
end
