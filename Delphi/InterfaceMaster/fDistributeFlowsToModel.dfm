inherited frmDistributeFlowsToModel: TfrmDistributeFlowsToModel
  Caption = 'frmDistributeFlowsToModel'
  OnDestroy = FormDestroy
  ExplicitWidth = 774
  ExplicitHeight = 512
  PixelsPerInch = 96
  TextHeight = 13
  object RzLabel2: TRzLabel [0]
    Left = 8
    Top = 47
    Width = 141
    Height = 13
    Caption = 'Distribution Configuration File'
  end
  object RzLabel4: TRzLabel [1]
    Left = 8
    Top = 360
    Width = 199
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = 'Save Interface File with Distributed Flows'
  end
  object lblLagTime: TRzLabel [2]
    Left = 256
    Top = 412
    Width = 49
    Height = 13
    Anchors = [akLeft, akBottom]
    Caption = 'lblLagTime'
    Visible = False
  end
  inherited pnlTitleHolder: TRzPanel
    inherited RzLabel1: TRzLabel
      Caption = ' Distribute Flows to Model'
    end
  end
  object edtDistributionConfigFile: TRzButtonEdit
    Left = 8
    Top = 64
    Width = 750
    Height = 21
    Anchors = [akLeft, akTop, akRight]
    FrameController = frmMain.RzFrameController1
    TabOrder = 1
    ButtonKind = bkFolder
    OnButtonClick = edtDistributionConfigFileButtonClick
  end
  object RzPanel1: TRzPanel
    Left = 0
    Top = 437
    Width = 766
    Height = 48
    Align = alBottom
    BorderOuter = fsNone
    TabOrder = 2
    DesignSize = (
      766
      48)
    object btnCloseTask: TRzButton
      Left = 4
      Top = 11
      Width = 105
      Anchors = [akLeft, akBottom]
      Caption = 'Close Task'
      HotTrack = True
      TabOrder = 0
      OnClick = btnCloseTaskClick
    end
  end
  object btnSaveConfigFile: TRzButton
    Left = 8
    Top = 406
    Width = 157
    Anchors = [akLeft, akBottom]
    Caption = 'Save Configuration'
    Enabled = False
    HotTrack = True
    TabOrder = 3
  end
  object btnRunDistribution: TRzButton
    Left = 171
    Top = 406
    Anchors = [akLeft, akBottom]
    Caption = 'Run'
    Enabled = False
    HotTrack = True
    TabOrder = 4
    OnClick = btnRunDistributionClick
  end
  object edtSaveInterfaceFile: TRzButtonEdit
    Left = 8
    Top = 379
    Width = 750
    Height = 21
    Anchors = [akLeft, akRight, akBottom]
    FrameController = frmMain.RzFrameController1
    TabOrder = 5
    ButtonKind = bkFolder
    OnButtonClick = edtSaveInterfaceFileButtonClick
  end
  object RzPanel2: TRzPanel
    Left = 8
    Top = 93
    Width = 750
    Height = 261
    Anchors = [akLeft, akTop, akRight, akBottom]
    BorderOuter = fsNone
    TabOrder = 6
    object RzSizePanel1: TRzSizePanel
      Left = 0
      Top = 0
      Width = 357
      Height = 261
      BorderOuter = fsFlat
      HotSpotVisible = True
      RealTimeDrag = True
      SizeBarWidth = 7
      TabOrder = 0
      DesignSize = (
        349
        261)
      object RzLabel3: TRzLabel
        Left = 8
        Top = 7
        Width = 107
        Height = 13
        Caption = 'Configuration Settings'
      end
      object RzLabel5: TRzLabel
        Left = 8
        Top = 193
        Width = 59
        Height = 13
        Caption = 'Distributions'
      end
      object cxVerticalGrid1: TcxVerticalGrid
        Left = 8
        Top = 26
        Width = 285
        Height = 155
        Anchors = [akLeft, akTop, akRight]
        OptionsView.RowHeaderWidth = 116
        TabOrder = 0
        object vgrdcMain: TcxCategoryRow
          Properties.Caption = 'Main'
        end
        object vgrdrModelPath: TcxEditorRow
          Properties.Caption = 'Model path'
          Properties.EditPropertiesClassName = 'TcxButtonEditProperties'
          Properties.EditProperties.Buttons = <
            item
              Default = True
              Kind = bkEllipsis
            end>
          Properties.DataBinding.ValueType = 'String'
          Properties.Value = Null
        end
        object vgrdrNumDistributions: TcxEditorRow
          Properties.Caption = '# Distributions'
          Properties.EditPropertiesClassName = 'TcxSpinEditProperties'
          Properties.DataBinding.ValueType = 'String'
          Properties.Value = Null
        end
        object vgrdrNodeField: TcxEditorRow
          Properties.Caption = 'Node field'
          Properties.EditPropertiesClassName = 'TcxTextEditProperties'
          Properties.DataBinding.ValueType = 'String'
          Properties.Value = Null
        end
        object vgrdrDistributionSource: TcxEditorRow
          Properties.Caption = 'Distribution source'
          Properties.DataBinding.ValueType = 'String'
          Properties.Value = Null
        end
        object vgrdrDistributionTable: TcxEditorRow
          Properties.Caption = 'Distribution table'
          Properties.DataBinding.ValueType = 'String'
          Properties.Value = Null
        end
        object vgrdrDistributionField: TcxEditorRow
          Properties.Caption = 'Distribution field'
          Properties.EditPropertiesClassName = 'TcxTextEditProperties'
          Properties.DataBinding.ValueType = 'String'
          Properties.Value = Null
        end
        object vgrdrStartTime: TcxEditorRow
          Properties.Caption = 'Start time'
          Properties.EditPropertiesClassName = 'TcxTextEditProperties'
          Properties.DataBinding.ValueType = 'String'
          Properties.Value = Null
        end
        object vgrdrEndTime: TcxEditorRow
          Properties.Caption = 'End time'
          Properties.EditPropertiesClassName = 'TcxTextEditProperties'
          Properties.DataBinding.ValueType = 'String'
          Properties.Value = Null
        end
        object vgrdrTimeStep: TcxEditorRow
          Properties.Caption = 'Time step (s)'
          Properties.EditPropertiesClassName = 'TcxSpinEditProperties'
          Properties.DataBinding.ValueType = 'String'
          Properties.Value = Null
        end
      end
      object lstDistributions: TRzListBox
        Left = 8
        Top = 212
        Width = 285
        Height = 41
        Anchors = [akLeft, akTop, akRight, akBottom]
        FrameController = frmMain.RzFrameController1
        ItemHeight = 13
        TabOrder = 1
        OnClick = lstDistributionsClick
      end
    end
    object cxVerticalGrid2: TcxVerticalGrid
      Left = 357
      Top = 0
      Width = 393
      Height = 261
      Align = alClient
      TabOrder = 1
      object vgrdcDistributionID: TcxCategoryRow
        Properties.Caption = 'Distribution ###'
      end
      object vgrdrHydrographFile: TcxEditorRow
        Properties.Caption = 'Hydrograph File'
        Properties.EditPropertiesClassName = 'TcxButtonEditProperties'
        Properties.EditProperties.Buttons = <
          item
            Default = True
            Kind = bkEllipsis
          end>
        Properties.DataBinding.ValueType = 'String'
        Properties.Value = Null
      end
      object vgrdrMultiplier: TcxEditorRow
        Properties.Caption = 'Multiplier'
        Properties.EditPropertiesClassName = 'TcxSpinEditProperties'
        Properties.EditProperties.Increment = 0.100000000000000000
        Properties.EditProperties.ValueType = vtFloat
        Properties.DataBinding.ValueType = 'String'
        Properties.Value = '1'
      end
      object vgrdrVelocityFactor: TcxEditorRow
        Properties.Caption = 'Velocity Factor'
        Properties.EditPropertiesClassName = 'TcxSpinEditProperties'
        Properties.DataBinding.ValueType = 'String'
        Properties.Value = Null
      end
      object vgrdrNumHeaderLines: TcxEditorRow
        Properties.Caption = '# Header Lines'
        Properties.EditPropertiesClassName = 'TcxSpinEditProperties'
        Properties.DataBinding.ValueType = 'String'
        Properties.Value = Null
      end
      object vgrdrDateFormat: TcxEditorRow
        Properties.Caption = 'Date Format'
        Properties.EditPropertiesClassName = 'TcxTextEditProperties'
        Properties.DataBinding.ValueType = 'String'
        Properties.Value = 'YYYY-MM-DD'
      end
      object vgrdrTimeFormat: TcxEditorRow
        Properties.Caption = 'Time Format'
        Properties.EditPropertiesClassName = 'TcxTextEditProperties'
        Properties.DataBinding.ValueType = 'String'
        Properties.Value = 'HH:MM:SS'
      end
      object vgrdrDelimiter: TcxEditorRow
        Properties.Caption = 'Delimiter'
        Properties.EditPropertiesClassName = 'TcxTextEditProperties'
        Properties.DataBinding.ValueType = 'String'
        Properties.Value = ' '
      end
      object vgrdrRoot: TcxEditorRow
        Properties.Caption = 'Root Link'
        Properties.EditPropertiesClassName = 'TcxTextEditProperties'
        Properties.DataBinding.ValueType = 'String'
        Properties.Value = Null
      end
      object vgrdrStops: TcxEditorRow
        Properties.Caption = 'Stop Links'
        Properties.EditPropertiesClassName = 'TcxTextEditProperties'
        Properties.DataBinding.ValueType = 'String'
        Properties.Value = Null
      end
    end
  end
  object errHandler: TRzErrorHandler
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = []
    OKCaption = 'OK'
    CancelCaption = 'Cancel'
    ProceedCaption = 'Proceed'
    Left = 412
    Top = 424
  end
end
