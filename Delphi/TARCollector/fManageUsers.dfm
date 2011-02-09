object frmManageUsers: TfrmManageUsers
  Left = 706
  Top = 450
  BorderStyle = bsDialog
  Caption = 'Manage Users'
  ClientHeight = 213
  ClientWidth = 312
  Color = clBtnFace
  Font.Charset = ANSI_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnClose = FormClose
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object RzLabel1: TRzLabel
    Left = 8
    Top = 4
    Width = 27
    Height = 13
    Caption = 'Users'
  end
  object btnOK: TRzButton
    Left = 228
    Top = 176
    ModalResult = 1
    Caption = 'OK'
    TabOrder = 1
    OnClick = btnOKClick
  end
  object btnCreateUser: TRzButton
    Left = 8
    Top = 176
    Caption = 'Create User'
    TabOrder = 0
    OnClick = btnCreateUserClick
  end
  object grdUsers: TcxTreeList
    Left = 8
    Top = 20
    Width = 297
    Height = 149
    Bands = <
      item
      end>
    BufferedPaint = False
    LookAndFeel.Kind = lfUltraFlat
    OptionsView.ColumnAutoWidth = True
    OptionsView.GridLines = tlglHorz
    OptionsView.ShowRoot = False
    TabOrder = 2
    object colActive: TcxTreeListColumn
      PropertiesClassName = 'TcxCheckBoxProperties'
      Caption.Text = 'Active?'
      DataBinding.ValueType = 'String'
      Width = 30
      Position.ColIndex = 0
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
    object colUserName: TcxTreeListColumn
      PropertiesClassName = 'TcxTextEditProperties'
      Caption.Text = 'User Name'
      DataBinding.ValueType = 'String'
      Position.ColIndex = 1
      Position.RowIndex = 0
      Position.BandIndex = 0
    end
  end
end
