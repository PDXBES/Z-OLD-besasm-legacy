unit PDXDateUtils_Tests;

interface

uses SysUtils, TestFramework, DateUtils,
	PDXDateUtils;

type
	TTestCaseA = class(TTestCase)
	published
		procedure TestMonth_BadDay;
		procedure TestMonthName_BadMonth;
		procedure TestDayOfMonth_BadDay;

		procedure TestYearOfJulDate_BadJulDate;
		procedure TestDayOfJulDate_BadJulDate;
		procedure TestDayOfMonthOfJulDate_BadJulDate;
		procedure TestDayOfYearOfJulDate_BadJulDate;
		procedure TestMonthOfJulDate_BadJulDate;
		procedure TestDateTimeOfJulDate_BadJulDate;
		procedure TestIsJulDateLeapYear_BadJulDate;
		procedure TestDiffJulDate_BadJulDate1;
		procedure TestDiffJulDate_BadJulDate2;
		procedure TestDiffJulDate_JulDate1EarlierThanJulDate2;

		procedure TestHourOfSecondsOfDay_BadSeconds;
		procedure TestMinuteOfSecondsOfDay_BadSeconds;
		procedure TestSecondOfSecondsOfDay_BadSeconds;

		procedure TestSecondsOfDayOfDateTime_BadDateTime;

		procedure TestAssembleDateTime_BadYear;
		procedure TestAssembleDateTime_BadMonth;
		procedure TestAssembleDateTime_BadDay;
		procedure TestAssembleDateTime_BadHour;
		procedure TestAssembleDateTime_BadMinute;
		procedure TestAssembleDateTime_BadSecond;
		
	end;

implementation

initialization
	TestFramework.RegisterTest(TTestCaseA.Suite);

end.
