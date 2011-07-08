inherited frmChooseAProject: TfrmChooseAProject
  Left = 243
  Top = 179
  Width = 703
  Height = 482
  Caption = 'frmChooseAProject'
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  inherited Label1: TLabel
    Width = 695
    Caption = ' Choose a Project'
  end
  object edtWorkingDirectory: TLabeledEdit
    Left = 16
    Top = 80
    Width = 349
    Height = 21
    EditLabel.Width = 86
    EditLabel.Height = 13
    EditLabel.Caption = 'Working Directory'
    LabelPosition = lpAbove
    LabelSpacing = 3
    TabOrder = 0
  end
  object Button1: TButton
    Left = 368
    Top = 80
    Width = 93
    Height = 25
    Caption = 'Open a Project'
    TabOrder = 1
  end
  object Button2: TButton
    Left = 464
    Top = 80
    Width = 93
    Height = 25
    Caption = 'New Project'
    TabOrder = 2
  end
  object lstDirectory: TDirectoryListBox
    Left = 16
    Top = 136
    Width = 145
    Height = 319
    Anchors = [akLeft, akTop, akBottom]
    ItemHeight = 16
    TabOrder = 3
    OnChange = lstDirectoryChange
  end
  object lstFiles: TFileListBox
    Left = 164
    Top = 112
    Width = 393
    Height = 343
    Anchors = [akLeft, akTop, akBottom]
    ItemHeight = 13
    TabOrder = 4
  end
  object cboDrive: TDriveComboBox
    Left = 16
    Top = 112
    Width = 145
    Height = 19
    TabOrder = 5
    OnChange = cboDriveChange
  end
  object dlgOpen: TOpenDialog
    Left = 588
    Top = 52
  end
end
