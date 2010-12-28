inherited frmPerformQualityControl: TfrmPerformQualityControl
  Left = 224
  Top = 295
  Caption = 'frmPerformQualityControl'
  ClientHeight = 407
  ClientWidth = 580
  ExplicitWidth = 588
  ExplicitHeight = 435
  PixelsPerInch = 96
  TextHeight = 13
  object Label2: TLabel [0]
    Left = 12
    Top = 366
    Width = 23
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = 'Next'
  end
  object Label3: TLabel [1]
    Left = 12
    Top = 336
    Width = 41
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = 'Previous'
  end
  object Label4: TLabel [2]
    Left = 80
    Top = 108
    Width = 94
    Height = 13
    Caption = 'Check Alternatives:'
  end
  object prgRetrace: TRzProgressBar [3]
    Left = 324
    Top = 124
    Width = 249
    Anchors = [akLeft, akTop, akRight]
    BackColor = clBtnFace
    BorderInner = fsFlat
    BorderOuter = fsNone
    BorderWidth = 1
    InteriorOffset = 0
    PartsComplete = 0
    Percent = 0
    TotalParts = 0
    Visible = False
  end
  inherited RzPanel4: TRzPanel
    Width = 580
    TabOrder = 7
    inherited lblLabel: TRzLabel
      Width = 257
      Caption = ' Perform Quality Control'
      ExplicitWidth = 257
    end
  end
  object btnBuildSubcatchments: TButton
    Left = 76
    Top = 360
    Width = 253
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = 'Build Subcatchments'
    TabOrder = 0
    OnClick = btnBuildSubcatchmentsClick
  end
  object btnBuildPipeSystem: TButton
    Left = 76
    Top = 332
    Width = 253
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = 'Build Pipe System'
    TabOrder = 1
    OnClick = btnBuildPipeSystemClick
  end
  object btnBuildModels: TButton
    Left = 76
    Top = 300
    Width = 253
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = 'Build Models Main Screen'
    TabOrder = 2
    OnClick = btnBuildModelsClick
  end
  object RzButton1: TRzButton
    Left = 80
    Top = 72
    Width = 241
    Action = actCreateQCWorkspaces
    HotTrack = True
    TabOrder = 3
  end
  object RzButton2: TRzButton
    Left = 80
    Top = 180
    Width = 241
    Action = actRetraceSurfaceSubcatchments
    HotTrack = True
    TabOrder = 4
  end
  object RzButton3: TRzButton
    Left = 80
    Top = 152
    Width = 241
    Action = actRetraceLaterals
    HotTrack = True
    TabOrder = 5
  end
  object RzButton4: TRzButton
    Left = 80
    Top = 124
    Width = 241
    Action = actRetraceLinks
    HotTrack = True
    TabOrder = 6
  end
  object ActionList1: TActionList
    Left = 424
    Top = 224
    object actCreateQCWorkspaces: TAction
      Caption = 'Create QC Workspaces'
    end
    object actRetraceSurfaceSubcatchments: TAction
      Caption = 'Retrace Surface Subcatchments'
      OnExecute = actRetraceSurfaceSubcatchmentsExecute
    end
    object actRetraceLaterals: TAction
      Caption = 'Retrace Laterals'
      OnExecute = actRetraceLateralsExecute
    end
    object actRetraceLinks: TAction
      Caption = 'Retrace Links'
      OnExecute = actRetraceLinksExecute
    end
  end
  object adoLinksConnection: TADOConnection
    LoginPrompt = False
    Mode = cmShareDenyNone
    Provider = 'Microsoft.Jet.OLEDB.4.0'
    Left = 404
    Top = 132
  end
  object adoLinks: TADOTable
    Connection = adoLinksConnection
    TableName = 'mdl_Links_ac'
    Left = 476
    Top = 132
    object adoLinksLinkID: TIntegerField
      FieldName = 'LinkID'
    end
    object adoLinksLinkReach: TWideStringField
      FieldName = 'LinkReach'
      Size = 254
    end
    object adoLinksReachElement: TIntegerField
      FieldName = 'ReachElement'
    end
    object adoLinksTraceVisit: TWideStringField
      FieldName = 'TraceVisit'
      Size = 1
    end
    object adoLinksMLinkID: TIntegerField
      FieldName = 'MLinkID'
    end
  end
  object adoNodes: TADOTable
    Connection = adoNodesConnection
    TableName = 'mdl_nodes_ac'
    Left = 476
    Top = 176
    object adoNodesNode: TWideStringField
      FieldName = 'Node'
      Size = 6
    end
    object adoNodesTraceVisit: TWideStringField
      FieldName = 'TraceVisit'
      Size = 1
    end
  end
  object adoCommand: TADOCommand
    Parameters = <>
    Left = 404
    Top = 92
  end
  object adoNodesConnection: TADOConnection
    LoginPrompt = False
    Mode = cmShareDenyNone
    Provider = 'Microsoft.Jet.OLEDB.4.0'
    Left = 404
    Top = 176
  end
  object adoLinksDSTerminals: TADOQuery
    Connection = adoLinksConnection
    Parameters = <>
    SQL.Strings = (
      'SELECT mdl_Links_ac.LinkID'
      'FROM mdl_Links_ac'
      
        'WHERE ((mdl_Links_ac.DSNode) NOT IN (SELECT mdl_Links_ac.USNode ' +
        'FROM mdl_Links_ac) AND (mdl_Links_ac.MLinkID = 0))')
    Left = 532
    Top = 132
    object adoLinksDSTerminalsLinkID: TIntegerField
      FieldName = 'LinkID'
    end
  end
end
