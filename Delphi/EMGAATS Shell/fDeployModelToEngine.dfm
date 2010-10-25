inherited frmDeployModel: TfrmDeployModel
  Left = 291
  Top = 202
  Caption = 'Deploy Model'
  ClientHeight = 500
  ClientWidth = 697
  OnShow = FormShow
  ExplicitWidth = 705
  ExplicitHeight = 528
  PixelsPerInch = 96
  TextHeight = 13
  object Label2: TLabel [0]
    Left = 80
    Top = 236
    Width = 32
    Height = 13
    Caption = 'Engine'
  end
  object Label3: TLabel [1]
    Left = 12
    Top = 454
    Width = 41
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = 'Previous'
  end
  object Label4: TLabel [2]
    Left = 12
    Top = 484
    Width = 23
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = 'Next'
  end
  inherited RzPanel4: TRzPanel
    Width = 697
    TabOrder = 9
    inherited lblLabel: TRzLabel
      Width = 289
      Caption = ' Deploy Model to Engine'
      ExplicitWidth = 289
    end
  end
  object cmbEngine: TComboBox
    Left = 124
    Top = 232
    Width = 145
    Height = 21
    Style = csDropDownList
    ItemHeight = 13
    ItemIndex = 0
    TabOrder = 0
    Text = 'XP-SWMM'
    Items.Strings = (
      'XP-SWMM'
      'PDX'
      'MOUSE')
  end
  object btnDeployModel: TButton
    Left = 84
    Top = 152
    Width = 185
    Height = 25
    Action = actDeployHydraulicModels
    Caption = 'Deploy Hydraulic Model'
    TabOrder = 6
  end
  object Button1: TButton
    Left = 76
    Top = 449
    Width = 253
    Height = 25
    Action = frmBuildModels.actBuildSubcatchments
    Anchors = [akLeft, akBottom]
    TabOrder = 1
  end
  object BuildModels: TButton
    Left = 76
    Top = 419
    Width = 253
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = 'Build Models Main Screen'
    TabOrder = 2
    OnClick = BuildModelsClick
  end
  object Button4: TButton
    Left = 84
    Top = 56
    Width = 185
    Height = 25
    Action = actPrepHydrologicData
    Caption = 'Prepare Hydrologic Data'
    TabOrder = 3
  end
  object btnBuildPDXRunoff: TButton
    Left = 84
    Top = 88
    Width = 185
    Height = 25
    Action = actDeployPDXRunoff
    Caption = 'Deploy PDX Runoff Deck'
    TabOrder = 4
  end
  object btnBuildPDXTransport: TButton
    Left = 84
    Top = 120
    Width = 185
    Height = 25
    Caption = 'Deploy PDX Transport Deck'
    TabOrder = 5
    OnClick = actDeployPDXTransportExecute
  end
  object chkMiVisible: TCheckBox
    Left = 344
    Top = 88
    Width = 129
    Height = 25
    Action = actMIVisible
    Caption = 'MI Visible'
    Color = clBtnFace
    Enabled = False
    ParentColor = False
    TabOrder = 7
  end
  object Button2: TButton
    Left = 76
    Top = 479
    Width = 253
    Height = 25
    Action = frmBuildModels.actModelResutls
    Anchors = [akLeft, akBottom]
    TabOrder = 8
  end
  object ActionList1: TActionList
    Left = 484
    Top = 224
    object actDeployHydraulicModels: TAction
      Caption = 'Deploy XP Hydraulic Model'
      OnExecute = actDeployHydraulicModelsExecute
    end
    object actPrepHydrologicData: TAction
      Caption = 'actPrepHydrologicData'
      OnExecute = actPrepHydrologicDataExecute
    end
    object actDeployPDXRunoff: TAction
      Caption = 'actDeployPDXRunoff'
      OnExecute = actDeployPDXRunoffExecute
    end
    object actMIVisible: TAction
      Caption = 'actMIVisible'
      Visible = False
      OnExecute = actMIVisibleExecute
    end
    object actDeployPDXTransport: TAction
      Caption = 'Act'
      OnExecute = actDeployPDXTransportExecute
    end
  end
end
