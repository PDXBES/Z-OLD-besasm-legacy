unit fDistributeFlowsToModel;

interface

uses
	Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
	Dialogs, fModuleMaster, StdCtrls, RzLabel, ExtCtrls, RzPanel, cxStyles,
	cxGraphics, cxEdit, cxButtonEdit, cxSpinEdit, cxDBLookupComboBox, cxVGrid,
	RzButton, cxControls, cxInplaceContainer, Mask, RzEdit, RzBtnEdt, cxTextEdit,
	RzShellDialogs, IniFiles, RzLstBox, cxDropDownEdit, uTConfiguration, RzSplit,
	Types, DateUtils, RzErrHnd;

type
	TfrmDistributeFlowsToModel = class(TfrmModuleMaster)
		edtDistributionConfigFile: TRzButtonEdit;
		RzLabel2: TRzLabel;
		RzPanel1: TRzPanel;
		btnCloseTask: TRzButton;
		btnSaveConfigFile: TRzButton;
		btnRunDistribution: TRzButton;
		RzLabel4: TRzLabel;
		edtSaveInterfaceFile: TRzButtonEdit;
    RzPanel2: TRzPanel;
    RzSizePanel1: TRzSizePanel;
    RzLabel3: TRzLabel;
    RzLabel5: TRzLabel;
    cxVerticalGrid1: TcxVerticalGrid;
    vgrdcMain: TcxCategoryRow;
    vgrdrModelPath: TcxEditorRow;
    vgrdrNumDistributions: TcxEditorRow;
    vgrdrNodeField: TcxEditorRow;
    vgrdrDistributionField: TcxEditorRow;
    vgrdrStartTime: TcxEditorRow;
    vgrdrTimeStep: TcxEditorRow;
    lstDistributions: TRzListBox;
    cxVerticalGrid2: TcxVerticalGrid;
    vgrdcDistributionID: TcxCategoryRow;
    vgrdrHydrographFile: TcxEditorRow;
    vgrdrMultiplier: TcxEditorRow;
    vgrdrNumHeaderLines: TcxEditorRow;
    vgrdrDateFormat: TcxEditorRow;
    vgrdrTimeFormat: TcxEditorRow;
    vgrdrDelimiter: TcxEditorRow;
    vgrdrRoot: TcxEditorRow;
    vgrdrStops: TcxEditorRow;
    vgrdrVelocityFactor: TcxEditorRow;
		vgrdrEndTime: TcxEditorRow;
    errHandler: TRzErrorHandler;
    lblLagTime: TRzLabel;
    vgrdrDistributionSource: TcxEditorRow;
    vgrdrDistributionTable: TcxEditorRow;
		procedure edtSaveInterfaceFileButtonClick(Sender: TObject);
		procedure edtDistributionConfigFileButtonClick(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure lstDistributionsClick(Sender: TObject);
    procedure btnCloseTaskClick(Sender: TObject);
    procedure btnRunDistributionClick(Sender: TObject);
	private
		{ Private declarations }
		DistribConfig: TDistributionConfiguration;
	public
		{ Public declarations }
	end;

var
	frmDistributeFlowsToModel: TfrmDistributeFlowsToModel;

implementation

uses fMain, dmIM_FlowDistributionSupport, PDXDateUtils;

{$R *.dfm}

procedure TfrmDistributeFlowsToModel.btnCloseTaskClick(Sender: TObject);
begin
  inherited;
	Close;
	frmDistributeFlowsToModel := nil;
end;

procedure TfrmDistributeFlowsToModel.btnRunDistributionClick(Sender: TObject);
var
  i: Integer;
begin
  inherited;

  // Scan the available distributions and see if there are any bad ones
  for i := 0 to Length(DistribConfig.DistributionSpecs)-1 do
    if DistribConfig.DistributionSpecs[i].HydrographFile = '' then
    begin
      MessageDlg('At least one distribution was specified incorrectly.  All '+
        'distributions in the list without a filename had either a bad root or '+
        'stop link.', mtError, [mbOK], 0);
      Exit;
    end;

	DistribConfig.WriteDistributedFile(edtSaveInterfaceFile.Text);
	lblLagTime.Caption := Format('Pre-Lag: %.0f minutes; Earliest Pre-Lagged Distribution '+
		'Time: %s',
		[DistribConfig.LagTime,
		FormatDateTime('m/d/y h:mm', DistribConfig.EarliestLaggedDistributionTime)]);
	lblLagTime.Show;
end;

procedure TfrmDistributeFlowsToModel.edtDistributionConfigFileButtonClick(
	Sender: TObject);
var
	IniFile: TMemIniFile;
	ModelPath: String;
	i: Integer;
begin
	inherited;
	frmMain.dlgOpen.InitialDir := frmMain.InitRegistryDir;
	frmMain.dlgOpen.Filter := 'INI Files (*.ini)|*.ini|All Files (*.*)|*.*';
	frmMain.dlgOpen.Title := 'Open Flow Distribution Config File';
	if frmMain.dlgOpen.Execute then
	begin
		frmMain.SaveDirToRegistry;
		IniFile := TMemIniFile.Create(frmMain.dlgOpen.FileName);
		edtDistributionConfigFile.Text := frmMain.dlgOpen.FileName;
		ModelPath := IniFile.ReadString('main', 'modelpath', '');
		IniFile.Free;
		DistribConfig := TDistributionConfiguration.Create(frmMain.dlgOpen.FileName);

    if DistribConfig.ValidConfiguration then
    begin
      // Show main config properties
      vgrdrModelPath.Properties.Value := DistribConfig.MainConfigSpecs.ModelPath;
      vgrdrNumDistributions.Properties.Value := DistribConfig.MainConfigSpecs.NumDistribs;
      vgrdrNodeField.Properties.Value := DistribConfig.MainConfigSpecs.NodeField;
      vgrdrDistributionSource.Properties.Value := DistribConfig.MainConfigSpecs.DistributionSource;
      vgrdrDistributionTable.Properties.Value := DistribConfig.MainConfigSpecs.DistributionTable;
      vgrdrDistributionField.Properties.Value := DistribConfig.MainConfigSpecs.DistributionField;
      if CompareDateTime(DistribConfig.MainConfigSpecs.StartTime, MinDateTime) =
        EqualsValue then
        vgrdrStartTime.Properties.Value := 'Based on earliest flow'
      else
        vgrdrStartTime.Properties.Value := DistribConfig.MainConfigSpecs.StartTime;
      if CompareDateTime(DistribConfig.MainConfigSpecs.EndTime, MaxDateTime) =
        EqualsValue then
        vgrdrEndTime.Properties.Value := 'Based in latest flow'
      else
        vgrdrEndTime.Properties.Value := DistribConfig.MainConfigSpecs.EndTime;
      vgrdrTimeStep.Properties.Value := DistribConfig.MainConfigSpecs.TimeStep;

      // Fill in list box for distributions
      for i := 0 to Length(DistribConfig.DistributionSpecs) - 1 do
        lstDistributions.Add(IntToStr(i+1)+':'+
          DistribConfig.DistributionSpecs[i].HydrographFile);
      lstDistributions.Selected[0] := True;
      lstDistributionsClick(Sender);
    end;
	end;
end;

procedure TfrmDistributeFlowsToModel.edtSaveInterfaceFileButtonClick(
	Sender: TObject);
begin
	inherited;
	frmMain.dlgSave.InitialDir := frmMain.InitRegistryDir;
	frmMain.dlgSave.Filter := 'Interface files (*.int)|*.int|All Files (*.*)|*.*';
	frmMain.dlgSave.DefaultExt := 'int';
	frmMain.dlgSave.Title := 'Save distributed flow interface file';
	if frmMain.dlgSave.Execute then
	begin
		frmMain.SaveDirToRegistry;
		btnRunDistribution.Enabled := True;
		edtSaveInterfaceFile.Text := frmMain.dlgSave.FileName;
  end;
end;

procedure TfrmDistributeFlowsToModel.FormDestroy(Sender: TObject);
begin
	dmFlowDistributionSupport.adoConnLinks.Close;
  dmFlowDistributionSupport.adoConnNodes.Close;
	DistribConfig.Free;
	inherited;
end;

procedure TfrmDistributeFlowsToModel.lstDistributionsClick(Sender: TObject);
var
	SelectedIndex: Integer;
  i: Integer;
begin
	SelectedIndex := -1;
	for i := 0 to lstDistributions.Count - 1 do
		if lstDistributions.Selected[i] then
			SelectedIndex := i;
	if SelectedIndex >= 0 then
	begin
  	vgrdcDistributionID.Properties.Caption := 'Distribution ' + IntToStr(SelectedIndex+1);
		vgrdrHydrographFile.Properties.Value :=
			DistribConfig.DistributionSpecs[SelectedIndex].HydrographFile;
		vgrdrMultiplier.Properties.Value :=
			DistribConfig.DistributionSpecs[SelectedIndex].Multiplier;
		vgrdrNumHeaderLines.Properties.Value :=
			DistribConfig.DistributionSpecs[SelectedIndex].NumHeaderLines;
		vgrdrDateFormat.Properties.Value :=
			DistribConfig.DistributionSpecs[SelectedIndex].DateFormat;
		vgrdrTimeFormat.Properties.Value :=
			DistribConfig.DistributionSpecs[SelectedIndex].TimeFormat;
		vgrdrDelimiter.Properties.Value :=
			DistribConfig.DistributionSpecs[SelectedIndex].Delimiter;
		vgrdrRoot.Properties.Value :=
			DistribConfig.DistributionSpecs[SelectedIndex].RootLink;
		vgrdrStops.Properties.Value :=
			DistribConfig.DistributionSpecs[SelectedIndex].StopLinks;
		vgrdrVelocityFactor.Properties.Value :=
    	DistribConfig.DistributionSpecs[SelectedIndex].VelocityFactor;
	end;
end;

end.
