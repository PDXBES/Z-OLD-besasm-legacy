unit uTMainConfigSpecs;

interface

uses SysUtils;

type

	TMainConfigSpecs = record
		ModelPath: TFileName;
		NumDistribs: Integer;
		LinkIDField: String;
		NodeField: String;
    DistributionSource: String;
    DistributionTable: String;
		DistributionField: String;
		StartTime: TDateTime;
		EndTime: TDateTime;
		TimeStep: Double;
	end;

implementation

end.
