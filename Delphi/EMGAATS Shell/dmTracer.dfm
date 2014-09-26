object datmodTracer: TdatmodTracer
  OldCreateOrder = False
  Height = 150
  Width = 215
  object adoSource: TADOQuery
    Connection = adoConnection
    Parameters = <>
    Left = 24
    Top = 12
  end
  object adoConnection: TADOConnection
    Left = 24
    Top = 64
  end
end
