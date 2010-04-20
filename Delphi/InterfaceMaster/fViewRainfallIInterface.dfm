inherited frmViewRainfallInterfaceFile: TfrmViewRainfallInterfaceFile
  Caption = 'frmViewRainfallInterfaceFile'
  PixelsPerInch = 96
  TextHeight = 13
  object RzLabel2: TRzLabel [0]
    Left = 8
    Top = 44
    Width = 147
    Height = 13
    Caption = '1. Select Rainfall Interface File'
  end
  inherited pnlTitleHolder: TRzPanel
    TabOrder = 1
    inherited RzLabel1: TRzLabel
      Caption = ' View Rainfall Interface File'
    end
  end
  object edtFileName: TRzButtonEdit
    Left = 8
    Top = 60
    Width = 757
    Height = 21
    Anchors = [akLeft, akTop, akRight]
    FrameController = frmMain.RzFrameController1
    TabOrder = 0
    ButtonKind = bkFolder
    OnButtonClick = edtFileNameButtonClick
  end
  object cxSpreadSheet1: TcxSpreadSheet
    Left = 8
    Top = 108
    Width = 757
    Height = 309
    DefaultStyle.Font.Name = 'Tahoma'
    HeaderFont.Charset = DEFAULT_CHARSET
    HeaderFont.Color = clWindowText
    HeaderFont.Height = -11
    HeaderFont.Name = 'Tahoma'
    HeaderFont.Style = []
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
    end
    object RzButton1: TRzButton
      Left = 612
      Top = 12
      Anchors = [akTop, akRight]
      HotTrack = True
      TabOrder = 1
      Visible = False
    end
    object RzButton2: TRzButton
      Left = 692
      Top = 12
      Anchors = [akTop, akRight]
      HotTrack = True
      TabOrder = 2
      Visible = False
    end
  end
end
