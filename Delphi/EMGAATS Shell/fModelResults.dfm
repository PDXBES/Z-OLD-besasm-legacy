inherited frmModelResults: TfrmModelResults
  Left = 191
  Top = 184
  Caption = 'Model Results'
  ClientHeight = 477
  ClientWidth = 706
  OnShow = FormShow
  ExplicitWidth = 714
  ExplicitHeight = 505
  PixelsPerInch = 96
  TextHeight = 13
  object Label3: TLabel [0]
    Left = 12
    Top = 425
    Width = 41
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = 'Previous'
  end
  inherited RzPanel4: TRzPanel
    Width = 706
    TabOrder = 3
    ExplicitWidth = 706
    inherited RzBackground1: TRzBackground
      Width = 706
      ExplicitWidth = 706
    end
    inherited lblLabel: TRzLabel
      Width = 272
      Caption = ' Process Model Results'
      ExplicitWidth = 272
    end
  end
  object Button1: TButton
    Left = 76
    Top = 420
    Width = 253
    Height = 25
    Action = frmBuildModels.actDeployModelToEngine
    Anchors = [akLeft, akBottom]
    TabOrder = 0
  end
  object Button2: TButton
    Left = 76
    Top = 390
    Width = 253
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = 'Build Models Main Screen'
    TabOrder = 1
    OnClick = Button2Click
  end
  object Button4: TButton
    Left = 84
    Top = 56
    Width = 185
    Height = 25
    Action = actXPresults
    TabOrder = 2
  end
  object ActionList1: TActionList
    Left = 484
    Top = 224
    object actXPresults: TAction
      Caption = 'Extract XP Results'
      OnExecute = actXPresultsExecute
    end
  end
  object OpenDialog1: TOpenDialog
    Left = 232
    Top = 200
  end
end
