unit uTFlowFileSpecs;

interface

type
	TFlowFileSpecs = record
		HeaderLines: Integer;
		DateFormat: String;
		TimeFormat: String;
		Delimiter: String;
		Multiplier: Double;
		constructor Create(NumHeaderLines: Integer; ADateFormat: String;
			ATimeFormat: String; ADelimiter: String; AMultiplier: Double);
	end;

implementation

{ TFlowFileSpecs }

constructor TFlowFileSpecs.Create(NumHeaderLines: Integer; ADateFormat,
	ATimeFormat, ADelimiter: String; AMultiplier: Double);
begin
	HeaderLines := NumHeaderLines;
	DateFormat := ADateFormat;
	TimeFormat := ATimeFormat;
	Delimiter := ADelimiter;
	Multiplier := AMultiplier;
end;

end.
