inherited frmModifyModelBoundaries: TfrmModifyModelBoundaries
  Left = 176
  Top = 186
  Caption = 'frmModifyModelBoundaries'
  ClientHeight = 474
  ClientWidth = 747
  OnHide = FormHide
  OnShow = FormShow
  ExplicitWidth = 755
  ExplicitHeight = 502
  PixelsPerInch = 96
  TextHeight = 13
  object Label2: TLabel [0]
    Left = 12
    Top = 52
    Width = 51
    Height = 13
    Caption = 'Root Pipes'
  end
  object Label3: TLabel [1]
    Left = 236
    Top = 52
    Width = 50
    Height = 13
    Caption = 'Stop Pipes'
  end
  object Label4: TLabel [2]
    Left = 16
    Top = 448
    Width = 23
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = 'Next'
  end
  object Label5: TLabel [3]
    Left = 8
    Top = 408
    Width = 59
    Height = 13
    Caption = 'THIS ONE->'
  end
  inherited RzPanel4: TRzPanel
    Width = 747
    TabOrder = 12
    inherited lblLabel: TRzLabel
      Width = 307
      Caption = ' Modify Model Boundaries'
      ExplicitWidth = 307
    end
  end
  object lstRootPipes: TListBox
    Left = 12
    Top = 68
    Width = 157
    Height = 97
    BevelKind = bkFlat
    BorderStyle = bsNone
    ItemHeight = 13
    Sorted = True
    TabOrder = 0
  end
  object btnAddRootPipe: TButton
    Left = 12
    Top = 176
    Width = 157
    Height = 25
    Action = actAddRootPipe
    TabOrder = 1
  end
  object btnCopyRootPipes: TButton
    Left = 12
    Top = 232
    Width = 157
    Height = 25
    Action = actCopyRootsFromAnotherModel
    TabOrder = 2
  end
  object btnDeleteRootPipe: TButton
    Left = 12
    Top = 204
    Width = 157
    Height = 25
    Action = actDelRootPipes
    TabOrder = 3
  end
  object btnBuildPipeSystem: TButton
    Left = 80
    Top = 442
    Width = 313
    Height = 25
    Action = frmBuildModels.actBuildPipeSystem
    Anchors = [akLeft, akBottom]
    TabOrder = 4
  end
  object lstStopPipes: TListBox
    Left = 236
    Top = 68
    Width = 157
    Height = 97
    BevelKind = bkFlat
    BorderStyle = bsNone
    ItemHeight = 13
    TabOrder = 5
  end
  object btnAddStopPipe: TButton
    Left = 236
    Top = 176
    Width = 157
    Height = 25
    Action = actAddStopPipe
    TabOrder = 6
  end
  object btnDeleteStopPipe: TButton
    Left = 236
    Top = 204
    Width = 157
    Height = 25
    Action = actDelStopPipes
    TabOrder = 7
  end
  object btnSpecifyBoundaryConditions: TButton
    Left = 12
    Top = 268
    Width = 157
    Height = 25
    Action = actSpecifyBoundaryConditions
    TabOrder = 8
  end
  object btnSpecifyStopPipeInflows: TButton
    Left = 236
    Top = 240
    Width = 157
    Height = 25
    Action = actSpecifyInflowsAtStopPipes
    TabOrder = 9
  end
  object btnBuildModels: TButton
    Left = 80
    Top = 398
    Width = 313
    Height = 25
    Anchors = [akLeft, akBottom]
    Caption = 'Build Models Main Screen'
    TabOrder = 10
    OnClick = btnBuildModelsClick
  end
  object StringGrid1: TStringGrid
    Left = 416
    Top = 264
    Width = 105
    Height = 130
    ColCount = 1
    FixedCols = 0
    RowCount = 30
    FixedRows = 0
    Options = [goFixedVertLine, goFixedHorzLine, goVertLine, goRangeSelect, goEditing, goAlwaysShowEditor]
    ScrollBars = ssNone
    TabOrder = 11
    ColWidths = (
      99)
  end
  object ActionList1: TActionList
    Left = 436
    Top = 180
    object actAddRootPipe: TAction
      Caption = 'Add Root Pipe'
      OnExecute = actAddRootPipeExecute
    end
    object actDelRootPipes: TAction
      Caption = 'Delete Root Pipes'
      OnExecute = actDelRootPipesExecute
      OnUpdate = actDelRootPipesUpdate
    end
    object actCopyRootsFromAnotherModel: TAction
      Caption = 'Copy from Another Model...'
      OnExecute = actCopyRootsFromAnotherModelExecute
    end
    object actSpecifyBoundaryConditions: TAction
      Caption = 'Specify Boundary Conditions'
      OnExecute = actSpecifyBoundaryConditionsExecute
      OnUpdate = actSpecifyBoundaryConditionsUpdate
    end
    object actAddStopPipe: TAction
      Caption = 'Add Stop Pipe'
      OnExecute = actAddStopPipeExecute
    end
    object actDelStopPipes: TAction
      Caption = 'Delete Stop Pipes'
      OnExecute = actDelStopPipesExecute
      OnUpdate = actDelStopPipesUpdate
    end
    object actSpecifyInflowsAtStopPipes: TAction
      Caption = 'Specify Inflows at Stop Pipes'
      OnExecute = actSpecifyInflowsAtStopPipesExecute
      OnUpdate = actSpecifyInflowsAtStopPipesUpdate
    end
  end
  object dlgCopyDirectory: TRzSelectFolderDialog
    Options = [sfdoContextMenus, sfdoReadOnly, sfdoShowHidden]
    Left = 528
    Top = 88
  end
end
