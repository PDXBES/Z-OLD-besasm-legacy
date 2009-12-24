unit uIM_ProgressTracker;

interface

uses SysUtils, Classes;

type

	IIM_ProgressTrackable = interface
		['{F1B91F09-B485-485F-8837-446841601860}']
    procedure UpdateProgress(Percentage: Integer);
	end;

implementation

end.
