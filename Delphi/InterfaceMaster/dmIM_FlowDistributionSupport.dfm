object dmFlowDistributionSupport: TdmFlowDistributionSupport
  OldCreateOrder = False
  Height = 242
  Width = 323
  object adoConnLinks: TADOConnection
    ConnectionString = 
      'Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source=C:\Mo' +
      'del_Programs\Interface Master\RedistributionFiles\Model\links\md' +
      'l_Links_ac_2003.mdb;Mode=Share Deny None;Extended Properties="";' +
      'Persist Security Info=False;Jet OLEDB:System database="";Jet OLE' +
      'DB:Registry Path="";Jet OLEDB:Database Password="";Jet OLEDB:Eng' +
      'ine Type=4;Jet OLEDB:Database Locking Mode=0;Jet OLEDB:Global Pa' +
      'rtial Bulk Ops=2;Jet OLEDB:Global Bulk Transactions=1;Jet OLEDB:' +
      'New Database Password="";Jet OLEDB:Create System Database=False;' +
      'Jet OLEDB:Encrypt Database=False;Jet OLEDB:Don'#39't Copy Locale on ' +
      'Compact=False;Jet OLEDB:Compact Without Replica Repair=False;Jet' +
      ' OLEDB:SFP=False'
    LoginPrompt = False
    Mode = cmShareDenyNone
    Provider = 'Microsoft.Jet.OLEDB.4.0'
    Left = 44
    Top = 32
  end
  object adoLinks: TADOTable
    Connection = adoConnLinks
    CursorType = ctStatic
    TableName = 'mdl_Links_ac'
    Left = 112
    Top = 32
  end
  object adoCommand: TADOCommand
    Connection = adoConnLinks
    Parameters = <>
    Left = 180
    Top = 32
  end
  object adoProportion: TADOQuery
    Connection = adoConnLinks
    Parameters = <>
    Left = 112
    Top = 88
  end
  object adoConnNodes: TADOConnection
    LoginPrompt = False
    Left = 44
    Top = 184
  end
  object adoNodes: TADOTable
    Connection = adoConnNodes
    TableName = 'mdl_Nodes_ac'
    Left = 112
    Top = 184
  end
  object adoLinkAreas: TADOTable
    Connection = adoConnLinks
    TableName = 'IM_LinkAreas'
    Left = 112
    Top = 136
  end
  object adoDistributionNodes: TADOTable
    Connection = adoConnLinks
    TableName = 'IM_DistributionNodes'
    Left = 184
    Top = 88
  end
end
