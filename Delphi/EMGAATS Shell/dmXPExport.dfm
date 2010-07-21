object dmodXPExport: TdmodXPExport
  OldCreateOrder = False
  Height = 405
  Width = 408
  object adoOutConnection: TADOConnection
    ConnectionString = 
      'Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Development\Hopt' +
      'imizer\BE Test\Hoptimize\hoptimize.mdb;Persist Security Info=Fal' +
      'se'
    LoginPrompt = False
    Mode = cmShareDenyNone
    Provider = 'Microsoft.Jet.OLEDB.4.0'
    Left = 32
    Top = 24
  end
  object adoOutCommand: TADOCommand
    Connection = adoOutConnection
    Parameters = <>
    Left = 32
    Top = 72
  end
  object adoTableE09: TADOTable
    Connection = adoOutConnection
    TableName = 'tableE09'
    Left = 32
    Top = 120
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
    Left = 32
    Top = 164
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
  object adoTableE20: TADOTable
    Connection = adoOutConnection
    TableName = 'tableE20'
    Left = 32
    Top = 208
    object adoTableE20NodeName: TWideStringField
      FieldName = 'NodeName'
      Size = 10
    end
    object adoTableE20SurchargeTime: TFloatField
      FieldName = 'SurchargeTime'
    end
    object adoTableE20FloodedTime: TFloatField
      FieldName = 'FloodedTime'
    end
    object adoTableE20FloodVol: TFloatField
      FieldName = 'FloodVol'
    end
    object adoTableE20MaxStoredVol: TFloatField
      FieldName = 'MaxStoredVol'
    end
    object adoTableE20PondingVol: TFloatField
      FieldName = 'PondingVol'
    end
  end
end
