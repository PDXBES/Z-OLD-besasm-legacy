unit PDXDateUtils;

interface

uses SysUtils;

function Month(Day, Year: Integer): Integer;
function MonthName(Month: Integer): string;
function DayOfMonth(Day, Year:Integer): Integer;

function YearOfJulDate(JulDate: Integer): Integer;
function DayOfJulDate(JulDate: Integer): Integer;
function DayOfMonthOfJulDate(JulDate: Integer): Integer;
function DayOfYearOfJulDate(ADate: Integer): Integer;
function MonthOfJulDate(JulDate: Integer): Integer;
function DateTimeOfJulDate(JulDate: Integer): TDateTime; overload;
function DateTimeOfJulDate(JulDate: Integer; Seconds: Double): TDateTime; overload;
function IsJulDateLeapYear(ADate: Integer): Boolean;
function DiffJulDate(ADate1, ADate2: Integer): Integer;

function HourOfSecondsOfDay(ASecond: Double): Integer;
function MinuteOfSecondsOfDay(ASecond: Double): Integer;
function SecondOfSecondsOfDay(ASecond: Double): Integer;

function SecondsOfDayOfDateTime(Date: TDateTime): Double;

function AssembleDateTime(Year, Month, Day: Integer): TDateTime; overload;
function AssembleDateTime(Year, Month, Day, Hour, Minute,
	Seconds: Integer): TDateTime; overload;

function YearOfDateTime(Date: TDateTime): Integer;
function MonthOfDateTime(Date: TDateTime): Integer;
function DayOfDateTime(Date: TDateTime): Integer;
function HourofDateTime(Date: TDateTime): Integer;
function MinuteofDateTime(Date: TDateTime): Integer;
function SecondOfDateTime(Date: TDateTime): Integer;
function MSecondOfDateTime(Date: TDateTime): Integer;
function DayOfYearOfDateTime(Date: TDateTime): Integer;
function JulDateOfDateTime(Date: TDateTime): Integer;
function Y2KJulDateOfDateTime(Date: TDateTime): Integer;

function NumLeaps(StartYear, EndYear: Integer): Integer;

function RoundToNearestSecond(Date: TDateTime): TDateTime;
function CalendarDays(BeginDate, EndDate: TDateTime): Integer;
function CalendarYears(BeginDate, EndDate: TDateTime): Integer;
function DateIsWithin(ADate, BeginDate, EndDate: TDateTime): Boolean;

procedure Delay(Msec: Word);

const
	SecondsPerHour: Integer = 3600;
	SecondsPerMinute: Integer = 60;
	SecondsPerDay: Integer = 86400;
	DaysPerRegularYear: Integer = 365;
	DaysPerLeapYear: Integer = 366;
	MaxDateTime: TDateTime = 3650000;
	MinDateTime: TDateTime = -3650000;

type
	EJulDateConvertError = class(EConvertError);

implementation

uses Math, DateUtils, Types;

function Month(Day, Year: Integer): Integer;
var
	LYDay: Integer;
begin
	if IsLeapYear(Year) then
		LYDay := 1
	else
		LYDay := 0;

	if(Day <= 31) then
		Result := 1  // Jan
	else if((Day-LYDay) <= 59) then
		Result := 2  // Feb
	else if((Day-LYDay) <= 90) then
		Result := 3  // Mar
	else if((Day-LYDay) <= 120) then
		Result := 4  // Apr
	else if((Day-LYDay) <= 151) then
		Result := 5  // May
	else if((Day-LYDay) <= 181) then
		Result := 6  // Jun
	else if((Day-LYDay) <= 212) then
		Result := 7  // Jul
	else if((Day-LYDay) <= 243) then
		Result := 8  // Aug
	else if((Day-LYDay) <= 273) then
		Result := 9  // Sep
	else if((Day-LYDay) <= 304) then
		Result := 10  // Oct
	else if((Day-LYDay) <= 334) then
		Result := 11  // Nov
	else
		Result := 12  // Dec
end;

function MonthName(Month: Integer): string;
begin
	case Month of
		1: Result := 'January';
		2: Result := 'February';
		3: Result := 'March';
		4: Result := 'April';
		5: Result := 'May';
		6: Result := 'June';
		7: Result := 'July';
		8: Result := 'August';
		9: Result := 'September';
		10: Result := 'October';
		11: Result := 'November';
		12: Result := 'December';
  end;
end;

function DayOfMonth(Day, Year: Integer): Integer;
var
	LYDay: Integer;
begin
	if IsLeapYear(Year) then
		LYDay := 1
	else
		LYDay := 0;

	if(Day <= 31) then  // Jan
		Result := Day
	else if((Day-LYDay) <= 59) then  // Feb
		Result := Day-31
	else if((Day-LYDay) <= 90) then  // Mar
		Result := (Day-LYDay)-59
	else if((Day-LYDay) <= 120) then  // Apr
		Result := (Day-LYDay)-90
	else if((Day-LYDay) <= 151) then  // May
		Result := (Day-LYDay)-120
	else if((Day-LYDay) <= 181) then  // Jun
		Result := (Day-LYDay)-151
	else if((Day-LYDay) <= 212) then  // Jul
		Result := (Day-LYDay)-181
	else if((Day-LYDay) <= 243) then  // Aug
		Result := (Day-LYDay)-212
	else if((Day-LYDay) <= 273) then  // Sep
		Result := (Day-LYDay)-243
	else if((Day-LYDay) <= 304) then  // Oct
		Result := (Day-LYDay)-273
	else if((Day-LYDay) <= 334) then  // Nov
		Result := (Day-LYDay)-304
	else  // Dec
		Result := (Day-LYDay)-334;
end;

function YearOfJulDate(JulDate: Integer): Integer;
var
  TestY2K: Integer;
begin
  TestY2K := JulDate div 1000;
  if TestY2K < 100 then
  	Result := JulDate div 1000 + 1900
  else
    Result := JulDate div 1000;

end;

function DayOfJulDate(JulDate: Integer): Integer;
begin
	Result := JulDate - (JulDate div 1000)*1000;
end;

function DayOfMonthOfJulDate(JulDate: Integer): Integer;
begin
	Result := DayOfMonth(DayOfJulDate(JulDate), YearOfJulDate(JulDate));
end;

function MonthOfJulDate(JulDate: Integer): Integer;
var
	Day, Year: Integer;
begin
	Day := DayOfJulDate(JulDate);
	Year := YearOfJulDate(JulDate);
	Result := Month(Day, Year);
end;

function DateTimeOfJulDate(JulDate: Integer): TDateTime;
begin
	Result := StrToDate(Format('%d/%d/%d',[MonthOfJulDate(JulDate),
		DayOfMonthOfJulDate(JulDate),YearOfJulDate(JulDate)]));
end;

function DateTimeOfJulDate(JulDate: Integer; Seconds: Double): TDateTime;
begin
	Result := StrToDate(Format('%d/%d/%d',[MonthOfJulDate(JulDate),
		DayOfMonthOfJulDate(JulDate),YearOfJulDate(JulDate)])) + Seconds/86400;
end;

function AssembleDateTime(Year, Month, Day: Integer): TDateTime; overload;
begin
	Result := StrToDate(Format('%d/%d/%d',[Month,Day,Year]));
end;

function AssembleDateTime(Year, Month, Day, Hour, Minute,
	Seconds: Integer): TDateTime; overload;
begin
	Result := StrToDate(Format('%d/%d/%d',[Month,Day,Year]));
	Result := Result + StrToTime(Format('%.2d:%.2d:%.2d',[Hour,Minute,Seconds]))
end;

function YearOfDateTime(Date: TDateTime): Integer;
var
	Year, Month, Day: Word;
begin
	DecodeDate(Date, Year, Month, Day);
	Result := Year;
end;

function MonthOfDateTime(Date: TDateTime): Integer;
var
	Year, Month, Day: Word;
begin
	DecodeDate(Date, Year, Month, Day);
	Result := Month;
end;

function DayOfDateTime(Date: TDateTime): Integer;
var
	Year, Month, Day: Word;
begin
	DecodeDate(Date, Year, Month, Day);
	Result := Day;
end;

function HourofDateTime(Date: TDateTime): Integer;
var
	Hour, Minute, Sec, MSec: Word;
begin
	DecodeTime(Date, Hour, Minute, Sec, MSec);
	Result := Hour;
end;

function MinuteofDateTime(Date: TDateTime): Integer;
var
	Hour, Minute, Sec, MSec: Word;
begin
	DecodeTime(Date, Hour, Minute, Sec, MSec);
	Result := Minute;
end;

function SecondOfDateTime(Date: TDateTime): Integer;
var
	Hour, Minute, Sec, MSec: Word;
begin
	DecodeTime(Date, Hour, Minute, Sec, MSec);
	Result := Sec;
end;

function MSecondOfDateTime(Date: TDateTime): Integer;
var
	Hour, Minute, Sec, MSec: Word;
begin
	DecodeTime(Date, Hour, Minute, Sec, MSec);
	Result := MSec;
end;

function DayOfYearOfDateTime(Date: TDateTime): Integer;
var
	Year, Month, Day: Word;
	LY: Integer;
begin
	DecodeDate(Date, Year, Month, Day);
	if IsLeapYear(Year) then
		LY := 1
	else
		LY := 0;

	case Month of
		1: Result := Day;
		2: Result := 31+Day;
		3: Result := 59+LY+Day;
		4: Result := 90+LY+Day;
		5: Result := 120+LY+Day;
		6: Result := 151+LY+Day;
		7: Result := 181+LY+Day;
		8: Result := 212+LY+Day;
		9: Result := 243+LY+Day;
		10: Result := 273+LY+Day;
		11: Result := 304+LY+Day;
		12: Result := 334+LY+Day;
		else Result := 0;
	end;
end;

function JulDateOfDateTime(Date: TDateTime): Integer;
begin
	Result := (YearOfDateTime(Date)-(YearOfDateTime(Date) div 100)*100)  * 1000 +
		DayOfYearOfDateTime(Date);
end;

function Y2KJulDateOfDateTime(Date: TDateTime): Integer;
begin
	Result := YearOfDateTime(Date)  * 1000 +
		DayOfYearOfDateTime(Date);
end;


function NumLeaps(StartYear, EndYear: Integer): Integer;
var
	i: Integer;
begin
	Result := 0;
	for i := StartYear to EndYear do
		if IsLeapYear(i) then
			Inc(Result);
end;

function DayOfYearOfJulDate(ADate: Integer): Integer;
begin
	Result := ADate - YearOfJulDate(ADate) * 1000;
end;

function IsJulDateLeapYear(ADate: Integer): Boolean;
begin
	// A year is a leap year only if it is divisible by 4.
	// However, if a year is divisible by 100, it isn't a leap year unless it is
	// also divisible by 400.
	if (YearOfJulDate(ADate) mod 4) = 0 then
	begin
		if	(YearOfJulDate(ADate) mod 100) = 0 then
		begin
			if (YearOfJulDate(ADate) mod 400) = 0 then
				Result := True
			else
				Result := False;
		end
		else
			Result := True;
	end
	else
		Result := False;
end;

function HourOfSecondsOfDay(ASecond: Double): Integer;
begin
	Result := Trunc(ASecond / 3600);
end;

function MinuteOfSecondsOfDay(ASecond: Double): Integer;
begin
	Result := Trunc((ASecond - HourOfSecondsOfDay(ASecond) * 3600) / 60);
end;

function SecondOfSecondsOfDay(ASecond: Double): Integer;
begin
	Result := Trunc(ASecond - (HourOfSecondsOfDay(ASecond) * 3600 +
		MinuteOfSecondsOfDay(ASecond)*60));
end;

function SecondsOfDayOfDateTime(Date: TDateTime): Double;
var
	AHour: Word;
	AMin: Word;
	ASec: Word;
	AMSec: Word;
begin
	DecodeTime(Date, AHour, AMin, ASec, AMSec);
  Result := AHour*3600 + AMin*60 + ASec + AMSec/1000;
end;

function DiffJulDate(ADate1, ADate2: Integer): Integer;
var
	DiffYears: Integer;
	i: Integer;
	Leaps: Integer;
	BeginYear, EndYear: Integer;
	TestJulDate: Integer;
begin
	DiffYears := Round(Abs(YearOfJulDate(ADate1) - YearOfJulDate(ADate2)));
	if DiffYears > 0 then
	begin
		BeginYear := Min(YearOfJulDate(ADate1), YearOfJulDate(ADate2));
		EndYear := Max(YearOfJulDate(ADate1), YearOfJulDate(ADate2));
	end;
	Leaps := 0;
	for i := BeginYear to EndYear do
	begin
		TestJulDate := i * 1000 + 1;
		if IsJulDateLeapYear(TestJulDate) then
			Inc(Leaps);
	end;
	if (DiffYears > 0) and (DayOfYearOfJulDate(Max(ADate1, ADate2)) < 59) and
		IsJulDateLeapYear(Max(ADate1, ADate2)) then
		Dec(Leaps);
	if (DiffYears > 0) and (DayOfYearOfJulDate(Min(ADate1, ADate2)) >= 59) and
		IsJulDateLeapYear(Min(ADate1, ADate2)) then
		Dec(Leaps);
	Result := DiffYears * 1000 + Leaps + (DaysPerRegularYear -
		DayOfYearOfJulDate(Min(ADate1, ADate2))) +
		(DayOfYearOfJulDate(Max(ADate1, ADate2)));
end;

function RoundToNearestSecond(Date: TDateTime): TDateTime;
var
  Month, Day, Year: Word;
  Hour, Minute, Second, MSecond: Word;
begin
  DecodeDateTime(Date, Year, Month, Day, Hour, Minute, Second, MSecond);
  Result := EncodeDateTime(Year, Month, Day, Hour, Minute, Second, 0);
end;

function CalendarDays(BeginDate, EndDate: TDateTime): Integer;
begin
  Result := Trunc(EndDate)-Trunc(BeginDate)+1
end;

function CalendarYears(BeginDate, EndDate: TDateTime): Integer;
begin
  Result := YearOfDateTime(EndDate) - YearOfDateTime(BeginDate) + 1;
end;

function DateIsWithin(ADate, BeginDate, EndDate: TDateTime): Boolean;
begin
  Result := ((CompareDateTime(ADate, BeginDate) = GreaterThanValue) or
    (CompareDateTime(ADate, BeginDate) = EqualsValue)) and
    ((CompareDateTime(ADate, EndDate) = LessThanValue) or
    (CompareDateTIme(ADate, EndDate) = EqualsValue));
end;

procedure Delay(Msec: Word);
var
	EndTime: TDateTime;
begin
	EndTime := IncMillisecond(Now, Msec);
	while Now < EndTime do
    ;
end;

end.
