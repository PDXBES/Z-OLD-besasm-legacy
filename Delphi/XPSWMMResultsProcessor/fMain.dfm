object Form1: TForm1
  Left = 260
  Top = 155
  Width = 1088
  Height = 750
  Caption = 'Form1'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object RzLabel1: TRzLabel
    Left = 20
    Top = 16
    Width = 32
    Height = 13
    Caption = 'InPath'
    FrameSides = []
  end
  object RzLabel2: TRzLabel
    Left = 20
    Top = 64
    Width = 40
    Height = 13
    Caption = 'OutPath'
    FrameSides = []
  end
  object edtInPath: TdxButtonEdit
    Left = 20
    Top = 32
    Width = 605
    TabOrder = 0
    Text = 'edtInPath'
    Buttons = <
      item
        Default = True
      end>
    OnButtonClick = edtInPathButtonClick
    ExistButtons = True
  end
  object edtOutPath: TdxButtonEdit
    Left = 20
    Top = 80
    Width = 605
    TabOrder = 1
    Text = 'edtOutPath'
    Buttons = <
      item
        Default = True
      end>
    OnButtonClick = edtOutPathButtonClick
    ExistButtons = True
  end
  object btnProcess: TRzButton
    Left = 60
    Top = 136
    Caption = 'Process'
    TabOrder = 2
    OnClick = btnProcessClick
  end
  object dlgOpen: TOpenDialog
    Left = 216
    Top = 116
  end
  object adoOutConnection: TADOConnection
    ConnectionString = 
      'Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Development\Hopt' +
      'imizer\BE Test\Hoptimize\hoptimize.mdb;Persist Security Info=Fal' +
      'se'
    LoginPrompt = False
    Mode = cmShareDenyNone
    Provider = 'Microsoft.Jet.OLEDB.4.0'
    Left = 132
    Top = 208
  end
  object adoOutCommand: TADOCommand
    Connection = adoOutConnection
    Parameters = <>
    Left = 132
    Top = 256
  end
  object adoTableE09: TADOTable
    Connection = adoOutConnection
    TableName = 'tableE09'
    Left = 132
    Top = 304
    object adoTableE09NodeName: TWideStringField
      FieldName = 'NodeName'
      Size = 10
    end
    object adoTableE09GrElev: TFloatField
      FieldName = 'GrElev'
    end
    object adoTableE09MaxCrown: TFloatField
      FieldName = 'MaxCrown'
    end
    object adoTableE09MaxJElev: TFloatField
      FieldName = 'MaxJElev'
    end
    object adoTableE09TimeOfMax: TDateTimeField
      FieldName = 'TimeOfMax'
    end
    object adoTableE09Surcharge: TFloatField
      FieldName = 'Surcharge'
    end
    object adoTableE09Freeboard: TFloatField
      FieldName = 'Freeboard'
    end
    object adoTableE09MaxArea: TFloatField
      FieldName = 'MaxArea'
    end
  end
  object adoTableE10: TADOTable
    Connection = adoOutConnection
    TableName = 'tableE10'
    Left = 132
    Top = 348
    object adoTableE10CondName: TWideStringField
      FieldName = 'CondName'
      Size = 10
    end
    object adoTableE10DesignQ: TFloatField
      FieldName = 'DesignQ'
    end
    object adoTableE10DesignV: TFloatField
      FieldName = 'DesignV'
    end
    object adoTableE10MaxD: TFloatField
      FieldName = 'MaxD'
    end
    object adoTableE10MaxQ: TFloatField
      FieldName = 'MaxQ'
    end
    object adoTableE10TimeMaxQ: TDateTimeField
      FieldName = 'TimeMaxQ'
    end
    object adoTableE10MaxV: TFloatField
      FieldName = 'MaxV'
    end
    object adoTableE10TimeMaxV: TDateTimeField
      FieldName = 'TimeMaxV'
    end
    object adoTableE10QqRatio: TFloatField
      FieldName = 'QqRatio'
    end
    object adoTableE10MaxUsElev: TFloatField
      FieldName = 'MaxUsElev'
    end
    object adoTableE10MaxDsElev: TFloatField
      FieldName = 'MaxDsElev'
    end
  end
end
