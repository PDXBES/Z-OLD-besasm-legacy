object dlgCheckProcesses: TdlgCheckProcesses
  Left = 0
  Top = 0
  BorderStyle = bsDialog
  Caption = 'Programs for EMGAATS Are Running'
  ClientHeight = 419
  ClientWidth = 469
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnCloseQuery = FormCloseQuery
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object RzGridPanel1: TRzGridPanel
    Left = 0
    Top = 0
    Width = 469
    Height = 383
    BorderOuter = fsNone
    Align = alClient
    AutoSize = True
    ColumnCollection = <
      item
        Value = 11.032028469750890000
      end
      item
        Value = 88.967971530249110000
      end>
    ControlCollection = <
      item
        Column = 1
        Control = rgrpAction
        Row = 2
      end
      item
        Column = 1
        Control = chklstProcesses
        Row = 3
      end
      item
        Column = 1
        Control = RzLabel2
        Row = 1
      end
      item
        Column = 0
        ColumnSpan = 2
        Control = RzPanel1
        Row = 0
      end
      item
        Column = 1
        Control = chkRememberShowVitalProgramsWarning
        Row = 4
      end>
    Padding.Left = 5
    Padding.Top = 5
    Padding.Right = 5
    Padding.Bottom = 5
    RowCollection = <
      item
        SizeStyle = ssAuto
        Value = 50.000000000000000000
      end
      item
        SizeStyle = ssAuto
        Value = 100.000000000000000000
      end
      item
        SizeStyle = ssAuto
      end
      item
        SizeStyle = ssAuto
      end
      item
        Value = 100.000000000000000000
      end>
    TabOrder = 0
    object rgrpAction: TRzRadioGroup
      AlignWithMargins = True
      Left = 63
      Top = 130
      Width = 393
      Height = 97
      Margins.Top = 10
      Align = alClient
      Caption = 'Select an action and click Continue'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -11
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      FrameController = frmMain.RzFrameController1
      GroupStyle = gsBanner
      ItemHotTrack = True
      ItemFont.Charset = DEFAULT_CHARSET
      ItemFont.Color = clWindowText
      ItemFont.Height = -11
      ItemFont.Name = 'Tahoma'
      ItemFont.Style = []
      ItemHeight = 20
      ItemIndex = 0
      Items.Strings = (
        'Close the programs checked below after I click continue'
        'Continue starting EMGAATS without closing any programs'
        'Exit EMGAATS')
      ParentFont = False
      StartYPos = 10
      TabOrder = 0
      VerticalSpacing = 0
      OnChanging = rgrpActionChanging
    end
    object chklstProcesses: TRzCheckList
      Left = 60
      Top = 240
      Width = 399
      Height = 102
      Items.Strings = (
        'Process1'
        'Process2')
      Items.ItemEnabled = (
        True
        True)
      Items.ItemState = (
        0
        0)
      Align = alClient
      Columns = 2
      FrameController = frmMain.RzFrameController1
      ItemHeight = 17
      TabOrder = 1
    end
    object RzLabel2: TRzLabel
      Left = 60
      Top = 70
      Width = 399
      Height = 40
      AutoSize = False
      Caption = 
        'EMGAATS uses MapInfo and Microsoft Access. Open MapInfo and Acce' +
        'ss programs may interfere with EMGAATS startup and it is best to' +
        ' close any of them first before continuing.'
      WordWrap = True
    end
    object RzPanel1: TRzPanel
      AlignWithMargins = True
      Left = 13
      Top = 13
      Width = 443
      Height = 44
      Align = alClient
      AutoSize = True
      BorderOuter = fsNone
      TabOrder = 2
      object Image1: TImage
        Left = 0
        Top = 0
        Width = 41
        Height = 44
        Align = alLeft
        Picture.Data = {
          0A54504E474F626A65637489504E470D0A1A0A0000000D494844520000002800
          00002808060000008CFEB86D0000000467414D410000AFC837058AE900000019
          74455874536F6674776172650041646F626520496D616765526561647971C965
          3C000007314944415478DACD98696C146518C7FFB3C7EC6E97B265B72D6D6939
          CB55500E8911842A06446310944F989A12D1183F88F20115314B17FD668C473C
          BE186D1013480C2001CB21448CD18814100D47011DCAA1D8D24BDB6E6977D7FF
          3333DB3D5BB6E5D04DFEECB0D999F9F5F7BECFF3BEB30A6EFE4B612237F36237
          EB3A16C66ABE87CC84FF0F80728D71CC8BCC9DCC29E663E634D3C1F4E0068CDE
          28A09C3F8BD9BB6A1572962E05AAAB818D1BD1C6CF9E606A9926A67BB090370A
          68670EF8FD981B08C42E55591911C8833C7CD534D90263C86F2BA09CFBA0C783
          DD9A06E4E4C42EA569118C19A31FAE12BB4C3D131C8CC51B01B49AF6CAE3ED25
          59FC8187EB985F610CF5802D0E1650CE5B407B7B93EDA5B1F88269F1FC602C0E
          1630C55E24E9B68A72732C0E0650CE59487B7BC49EC7A3E870D144E114FE73FE
          BC1D63C776C9472F317B98B3305ACF2D0594CAFD5AEC55552908B3155FBB469D
          0722FAF1E4C90ABC5E0B9C4E15AAEAC0CA95415A0C1EE2395530DA4E2306D0C0
          070A98606FE85005FBF6455051612364165C2E176C361BD6AC69C1238FD8307C
          B8030D0D368C1B77396A7127F31BD375AB0013EC85389BFC7E051F7E98ADC365
          6565E911A08A8A08EEBEDB8DA2A22C5A6CA0C566B1B89639CAB4666A71208029
          73AF878BD8A64DC06BAF15F6C249860C398765CB2278E0816C5672362E5E04E7
          E271B986F4C55DCCA54C2D0E04B0D79E54AE1484001E39022C5F3E9AC09E5E40
          A7F3072C5E9C4DC06118352A9BC3EEC08A156768F1CFEF798D35CCC94C2D660A
          6831ED25AC1AA19082AB5755CC9B37127979793A5C84E4858587B1689107F7DD
          E7E57136AC5607FB6288360FC869CFC3A8E80BA6C57EFB62A680AA696F5E6CD5
          B010D081B6363B9E792617FFFC53AA03B6B5B561EAD4E3B8FF7E2F66CFF62137
          77880E0838D8176B69F1F71F61CCC55F98665CA72F660228DF59447B35317B56
          FD86E1B0031D1D36AC5EED4573F3141DB0B1F13466CCB84CC05CCC9AE5E3D0BB
          61B118809AD64D8B5BE49AAB991A64B04667022834DFC4762C36FD668093C369
          473068C73BEF3871FAF46C1DF0D2A51F71CF3DAD983F3F1F53A6F858306E366D
          87390862F1002D9ECCD8E2F50065EE2D302A57A13DBB0967DC301271B0FFA9D8
          BCF91A0E1D7A58073C7BB606E5E5611A2CC4C4893EB69F2C13D03847D382B4F8
          41B2C5CEC1029AFB3D85F6A216D45E4801ECE951B17D7B3B0E1F7E5CEF85C78E
          7DC202F111B288FDD007872311505259F9252D1E89B7286B7478A08062EF21DA
          DBA5692EDA4B0F180AA9B8CC85A2AAAA0C65656534F92E5BCC48CC9953849212
          2FEC76570AA0A6B5D3E206B9C72BCC57CC19732E0E0850AEB8DFEF57EF0D04DC
          0960F1C752C90D0DC0EBAF97E9BDB0B3731BDBCE08AE22235050308C3DD09972
          8E6171232D7EFF13FFE3676495694D3717FB0234ED29B49763DA73A405944A6E
          6A52F0D14779ACE822B8DDFB70D75D85983973047CBE1C13504D395FD3DA6871
          55D4E21730569760A680A63D37ED0D4DBA7822A400B6B75BB17B7723B75741AE
          1CC3307E7C1E6F9EC70A961EE8ECF3FCCACAF76971BF58948289AE2EA1EB01C6
          D92BA0BDBE6E60DC440083411B8739CC5525C479A9B2397BF4B85CD203FB3E5F
          D39AF887C8C31F9E63F6A5B3980ED0B4E7A13D5F3F70D122B163FFFE084E9D2A
          6341C8B276827D30C2AD9697FBC21C7E165FC5C971D2E21BB4B8F33B73A8532C
          26035A0D7B969D9A369AF65C69E64F2CE1B08ABABA4E1C3DFA28772B637B370B
          5BB6ACC58205259830A1987B464F1F169DA6C5065A5C28F77E9AF996B988B8D5
          2519D069D8CB9D130814A42D8AF8488BA9A9B94C5BEB12B65BEFBDF732A64D53
          B9164FE666219F565D491613812B2BD7D0E217D2B4DF44D2B34B3CA039F72C9C
          7B93682FABCFCA8D07BC70A10BB5B57359B9C652D7DDDDCDFDE193DCC9943277
          A0B8B888951C0F986A53D3AED0E2F44EB358E481BFF709301ED0DCB11470C752
          D267D5260F714787428B7FA0B575B2BEFDAAABABE5B05E639B19CFCDC224E4E7
          E7738805504D0B17B3F82C2D7E9662310A68FE4A60E57E6F7A1A7BE921A562BB
          BB6DB872A58B45D2CC8D423B212D84CA456969896ECFEDF6E880FD4D15A32FD6
          D3E218B1F8A26951DFE94401658BF2A9DF5F5C11088CEDB772633731360B0219
          0C5AD0D212C2DF7FCB726AE75067735519C6F7A19C7FEEA4F9A7A6B99E81B164
          C912ECD8B1439AF6DBCC09A9E828A07CF3EBAD5BA7CE7DECB1A20C0013D76381
          ECE9B1E92D473E93E62C85610CADB39F3FD29E5006EBD7AFC7860D1B64DBFD16
          7398B9AA98DF906FBF545EEE0D1C3C38AFDFA1483FD4E9CC38FBE97FAA396831
          B896961656FE34D4D7D7F3310C9B4DC0C6E837E44F2965B68D1C993571C58A71
          664BB425BDF7756CCBE0FBF1EF1624BFAAABAB05AE8E879F33D2B8E531B029F6
          800164339398A7983129FE6FED4B9AB23468D9769D637E6634A63D1E4080BC4C
          31135DE36E17A0BCA4C2E4779BBF983F612C79DDF100D1078E684FB0DE46B8A8
          45F93DBBCB8CFEDB763A43D15FECFF8B57242EFAEB5F1C928652648542370000
          000049454E44AE426082}
        ExplicitTop = 2
      end
      object RzLabel1: TRzLabel
        AlignWithMargins = True
        Left = 47
        Top = 1
        Width = 366
        Height = 36
        Caption = 
          'Do you want to close programs vital to EMGAATS that are currentl' +
          'y running?'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clHotLight
        Font.Height = -15
        Font.Name = 'Tahoma'
        Font.Style = [fsBold]
        ParentFont = False
        Layout = tlCenter
        WordWrap = True
        LightTextStyle = True
        TextStyle = tsRaised
      end
    end
    object chkRememberShowVitalProgramsWarning: TRzCheckBox
      Left = 60
      Top = 352
      Width = 399
      Height = 21
      Align = alClient
      AlignmentVertical = avCenter
      Caption = 'Remember my choice and do not display this again'
      HotTrack = True
      State = cbUnchecked
      TabOrder = 3
    end
  end
  object dlgButtons: TRzDialogButtons
    Left = 0
    Top = 383
    Width = 469
    CaptionOk = 'Continue'
    CaptionCancel = 'Cancel'
    CaptionHelp = '&Help'
    HotTrack = True
    ModalResultOk = 1
    ModalResultCancel = 2
    ModalResultHelp = 0
    ShowDivider = True
    ShowCancelButton = False
    WidthOk = 150
    OnClickOk = dlgButtonsClickOk
    ParentShowHint = False
    ShowHint = False
    TabOrder = 1
  end
end
