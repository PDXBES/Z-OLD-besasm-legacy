inherited frmBuildLateralsAndParcels: TfrmBuildLateralsAndParcels
  Top = 165
  Caption = 'frmBuildLateralsAndParcels'
  ClientHeight = 468
  OnShow = FormShow
  ExplicitWidth = 320
  ExplicitHeight = 496
  PixelsPerInch = 96
  TextHeight = 13
  object Label2: TLabel [0]
    Left = 12
    Top = 455
    Width = 23
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = 'Next'
  end
  object Label3: TLabel [1]
    Left = 12
    Top = 425
    Width = 41
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = 'Previous'
  end
  inherited RzPanel4: TRzPanel
    TabOrder = 12
    inherited lblLabel: TRzLabel
      Width = 313
      Caption = ' Build Laterals and Parcels'
      ExplicitWidth = 313
    end
  end
  object btnSpecifyDivides: TButton
    Left = 28
    Top = 44
    Width = 185
    Height = 25
    Action = actSpecifyDivides
    Enabled = False
    TabOrder = 0
  end
  object btnTraceLateralsAndParcels: TButton
    Left = 28
    Top = 73
    Width = 185
    Height = 25
    Action = actTraceLateralsAndParcels
    TabOrder = 1
  end
  object btnCheckLateralsAndParcels: TButton
    Left = 28
    Top = 132
    Width = 185
    Height = 25
    Action = actCheckLateralsAndParcels
    Enabled = False
    TabOrder = 2
  end
  object btnBuildSubcatchments: TButton
    Left = 76
    Top = 450
    Width = 253
    Height = 25
    Action = frmBuildModels.actBuildSubcatchments
    Anchors = [akLeft, akBottom]
    TabOrder = 3
  end
  object btnBuildPipeSystem: TButton
    Left = 76
    Top = 420
    Width = 253
    Height = 25
    Action = frmBuildModels.actBuildPipeSystem
    Anchors = [akLeft, akBottom]
    TabOrder = 4
  end
  object btnBuildModels: TButton
    Left = 76
    Top = 390
    Width = 253
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = 'Build Models Main Screen'
    TabOrder = 5
    OnClick = btnBuildModelsClick
  end
  object chkSpecifyDivides: TCheckBox
    Left = 8
    Top = 48
    Width = 13
    Height = 17
    AllowGrayed = True
    Enabled = False
    TabOrder = 6
  end
  object chkTraceLateralsAndParcels: TCheckBox
    Left = 8
    Top = 77
    Width = 13
    Height = 17
    AllowGrayed = True
    TabOrder = 7
  end
  object chkCheckLateralsAndParcels: TCheckBox
    Left = 8
    Top = 136
    Width = 13
    Height = 17
    AllowGrayed = True
    Enabled = False
    TabOrder = 8
  end
  object chkInitDSCHydro: TCheckBox
    Left = 8
    Top = 106
    Width = 17
    Height = 17
    Caption = 'chkInitDSCHydro'
    Enabled = False
    TabOrder = 10
  end
  object chkUseDefaultDiscoTable: TCheckBox
    Left = 232
    Top = 112
    Width = 17
    Height = 17
    Caption = 'actClickDefaultTable'
    Enabled = False
    TabOrder = 11
    Visible = False
  end
  object btnInitDSCHydro: TButton
    Left = 28
    Top = 102
    Width = 185
    Height = 25
    Caption = 'Init DSC Hydrology'
    Enabled = False
    TabOrder = 9
  end
  object ActionList1: TActionList
    Left = 376
    Top = 264
    object actSpecifyDivides: TAction
      Caption = 'Specify Divides'
      OnExecute = actSpecifyDividesExecute
    end
    object actTraceLateralsAndParcels: TAction
      Caption = 'Trace Laterals and Parcels'
      OnExecute = actTraceLateralsAndParcelsExecute
    end
    object actCheckLateralsAndParcels: TAction
      Caption = 'Check Laterals and Parcels'
      OnExecute = actCheckLateralsAndParcelsExecute
      OnUpdate = actCheckLateralsAndParcelsUpdate
    end
  end
end
