inherited frmBuildPipeSystem: TfrmBuildPipeSystem
  Left = 218
  Top = 220
  Caption = 'Trace Laterals and Parcels'
  ClientHeight = 470
  ClientWidth = 564
  OnShow = FormShow
  ExplicitWidth = 572
  ExplicitHeight = 498
  PixelsPerInch = 96
  TextHeight = 13
  object Label3: TLabel [0]
    Left = 12
    Top = 455
    Width = 23
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = 'Next'
  end
  object Label2: TLabel [1]
    Left = 12
    Top = 425
    Width = 41
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = 'Previous'
  end
  inherited RzPanel4: TRzPanel
    Width = 564
    TabOrder = 12
    ExplicitWidth = 564
    inherited RzBackground1: TRzBackground
      Width = 564
      ExplicitWidth = 564
    end
    inherited lblLabel: TRzLabel
      Width = 195
      Caption = ' Build Pipe System'
      ExplicitWidth = 195
    end
  end
  object btnViewProfiles: TButton
    Left = 28
    Top = 104
    Width = 237
    Height = 25
    Action = actViewProfiles
    Enabled = False
    TabOrder = 0
  end
  object btnPipeSystemQueries: TButton
    Left = 28
    Top = 132
    Width = 237
    Height = 25
    Action = actPipeSystemQueries
    TabOrder = 1
  end
  object btnSpecifyDiversions: TButton
    Left = 28
    Top = 160
    Width = 237
    Height = 25
    Action = actSpecifyDiversions
    Enabled = False
    TabOrder = 2
  end
  object btnBuildLateralsAndParcels: TButton
    Left = 76
    Top = 450
    Width = 253
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = 'Build Laterals and Parcels'
    TabOrder = 3
    OnClick = btnBuildLateralsAndParcelsClick
  end
  object btnBuildModels: TButton
    Left = 76
    Top = 750
    Width = 253
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = 'Build Models'
    TabOrder = 4
    OnClick = btnBuildModelsClick
  end
  object chkViewProfiles: TCheckBox
    Left = 8
    Top = 108
    Width = 13
    Height = 17
    Enabled = False
    TabOrder = 5
  end
  object chkPipeSystemQueries: TCheckBox
    Left = 8
    Top = 136
    Width = 13
    Height = 17
    Enabled = False
    TabOrder = 6
  end
  object chkSpecifyDiversions: TCheckBox
    Left = 8
    Top = 164
    Width = 13
    Height = 17
    Enabled = False
    TabOrder = 7
  end
  object btnTracePipeSystem: TButton
    Left = 28
    Top = 76
    Width = 237
    Height = 25
    Action = actTracePipeSystem
    TabOrder = 8
  end
  object chkTracePipeSystem: TCheckBox
    Left = 8
    Top = 80
    Width = 13
    Height = 17
    Enabled = False
    TabOrder = 9
  end
  object btnModifyModelBoundaries2: TButton
    Left = 28
    Top = 44
    Width = 237
    Height = 25
    Action = actModifyModelBoundaries
    Caption = 'Modify Model Boundaries'
    TabOrder = 10
  end
  object Button1: TButton
    Left = 76
    Top = 420
    Width = 253
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = 'Build Models Main Screen'
    TabOrder = 11
    OnClick = Button1Click
  end
  object ActionList1: TActionList
    Left = 24
    Top = 344
    object actTracePipeSystem: TAction
      Caption = 'Trace Pipe System'
      OnExecute = actTracePipeSystemExecute
    end
    object actViewProfiles: TAction
      Caption = 'View Profiles'
      OnExecute = actViewProfilesExecute
      OnUpdate = actViewProfilesUpdate
    end
    object actPipeSystemQueries: TAction
      Caption = 'Check Pipe System with Queries'
      OnExecute = actPipeSystemQueriesExecute
      OnUpdate = actViewProfilesUpdate
    end
    object actSpecifyDiversions: TAction
      Caption = 'Specify Diversions'
      OnExecute = actSpecifyDiversionsExecute
      OnUpdate = actViewProfilesUpdate
    end
    object actUpdateChangesToMaster: TAction
      Caption = 'Update Changes to Master Pipe System'
      OnUpdate = actViewProfilesUpdate
    end
    object actModifyModelBoundaries: TAction
      Caption = 'actModifyModelBoundaries'
      OnExecute = ActModifyModelBoundariesExecute
    end
  end
end
