inherited frmBuildModels: TfrmBuildModels
  Left = 431
  Top = 365
  Caption = 'EMGAATS'
  ClientHeight = 469
  ClientWidth = 730
  ExplicitWidth = 738
  ExplicitHeight = 497
  PixelsPerInch = 96
  TextHeight = 13
  inherited RzPanel4: TRzPanel
    Width = 730
    TabOrder = 13
    inherited lblLabel: TRzLabel
      Width = 140
      Caption = ' Build Models'
      ExplicitWidth = 140
    end
  end
  object chkBuildPipeSystem: TCheckBox
    Left = 8
    Top = 76
    Width = 13
    Height = 17
    AllowGrayed = True
    TabOrder = 0
    Visible = False
  end
  object chkBuildLateralsAndParcels: TCheckBox
    Left = 8
    Top = 104
    Width = 13
    Height = 17
    AllowGrayed = True
    TabOrder = 1
    Visible = False
  end
  object chkBuildSubcatchments: TCheckBox
    Left = 8
    Top = 132
    Width = 13
    Height = 17
    AllowGrayed = True
    TabOrder = 2
    Visible = False
  end
  object btnBuildPipeSystem: TButton
    Left = 28
    Top = 72
    Width = 209
    Height = 25
    Action = actBuildPipeSystem
    TabOrder = 3
  end
  object btnBuildLateralsAndParcels: TButton
    Left = 28
    Top = 100
    Width = 209
    Height = 25
    Action = actBuildLateralsAndParcels
    TabOrder = 4
  end
  object btnBuildSubcatchments: TButton
    Left = 28
    Top = 128
    Width = 209
    Height = 25
    Action = actBuildSubcatchments
    TabOrder = 5
  end
  object btnDeployModelToEngine: TButton
    Left = 28
    Top = 156
    Width = 209
    Height = 25
    Action = actDeployModelToEngine
    TabOrder = 6
  end
  object btnModelResults: TButton
    Left = 28
    Top = 184
    Width = 209
    Height = 25
    Action = actModelResutls
    TabOrder = 7
  end
  object mdlrelinkmdbs: TButton
    Left = 284
    Top = 72
    Width = 209
    Height = 25
    Action = actrelinkmdbs
    Caption = 'Relink model mdbs'
    Enabled = False
    TabOrder = 8
  end
  object btnPerformQualityControl: TButton
    Left = 28
    Top = 212
    Width = 209
    Height = 25
    Action = actPerformQualityControl
    TabOrder = 9
  end
  object RzButton1: TRzButton
    Left = 28
    Top = 244
    Width = 209
    Action = actBuildAndDeploy
    Caption = 'Build Everything, Deploy RUNOFF'
    HotTrack = True
    TabOrder = 10
  end
  object Button1: TButton
    Left = 284
    Top = 112
    Width = 209
    Height = 25
    Caption = 'Patch Base Flow'
    Enabled = False
    TabOrder = 11
    Visible = False
    OnClick = Button1Click
  end
  object btnConvertModelToArcGIS: TRzButton
    Left = 28
    Top = 308
    Width = 209
    Height = 37
    Caption = 'Convert Model to ArcGIS PGDB (beta)'
    HotTrack = True
    TabOrder = 12
    OnClick = btnConvertModelToArcGISClick
  end
  object ActionList1: TActionList
    Left = 284
    Top = 380
    object actModifyModelBoundaries_old: TAction
      Caption = 'Modify Model Boundaries'
      OnExecute = actModifyModelBoundaries_oldExecute
    end
    object actBuildPipeSystem: TAction
      Caption = 'Build Pipe System'
      OnExecute = actBuildPipeSystemExecute
      OnUpdate = actBuildPipeSystemUpdate
    end
    object actBuildLateralsAndParcels: TAction
      Caption = 'Build Laterals and Parcels'
      OnExecute = actBuildLateralsAndParcelsExecute
      OnUpdate = actBuildLateralsAndParcelsUpdate
    end
    object actBuildSubcatchments: TAction
      Caption = 'Build Subcatchments'
      OnExecute = actBuildSubcatchmentsExecute
      OnUpdate = actBuildSubcatchmentsUpdate
    end
    object actDeployModelToEngine: TAction
      Caption = 'Deploy Model to Engine'
      OnExecute = actDeployModelToEngineExecute
      OnUpdate = actDeployModelToEngineUpdate
    end
    object actModelResutls: TAction
      Caption = 'Model Results'
      OnExecute = actModelResutlsExecute
    end
    object actrelinkmdbs: TAction
      Caption = 'Relinkmdbs'
      Visible = False
      OnExecute = actrelinkmdbsExecute
    end
    object actPerformQualityControl: TAction
      Caption = 'Perform Quality Control'
      OnExecute = actPerformQualityControlExecute
    end
    object actBuildAndDeploy: TAction
      Caption = 'Build Everything and Deploy'
      OnExecute = actBuildAndDeployExecute
      OnUpdate = actBuildAndDeployUpdate
    end
  end
  object RzLauncher1: TRzLauncher
    Action = 'Open'
    Timeout = -1
    Left = 356
    Top = 380
  end
end
