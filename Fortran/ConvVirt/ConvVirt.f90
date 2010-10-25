program ConvVirt

use DFLIB

CHARACTER Dummy*120, Infile*64, Outfile*64
INTEGER Year, Month, Day, Hour
INTEGER PrevYear, PrevMonth, PrevDay, PrevHour
INTEGER OutYear, OutDay, OutHour, OutRain
REAL Rain, PrevRain

!Functions
INTEGER JulDate
LOGICAL IsLeapYear

INTEGER, PARAMETER :: InUnit=50, OutUnit=51

call GETARG(1, Infile)
call GETARG(2, Outfile)
open(UNIT=InUnit, FILE=Infile, ACTION='READ')
open(UNIT=OutUnit, FILE=Outfile, ACTION='WRITE')

! Skip first three lines in InUnit
read(InUnit, '(2/)')

! Initialize accumulators
PrevYear = 0
Year = 0
PrevMonth = 0
Month = 0
PrevDay = 0
Day = 0
PrevHour = 0
Hour = 0
PrevRain = 0.00
Rain = 0.00

do while (.NOT.EOF(InUnit))
	PrevYear = Year
	PrevMonth = Month
	PrevDay = Day
	PrevHour = Hour
	
	read(InUnit, 10) Year, Month, Day, Hour, Rain
	10 format(5X, I4, 1X, I2, 1X, I2, 1X, I2, 1X, 3X, F4.2)

	! Check if we're dealing with the same hour;
	! If we are, add up the rain and move on to next line
	! otherwise, write out the most recent hourly data
	if ((PrevYear.EQ.Year) .AND. (PrevMonth.EQ.Month) .AND. &
		(PrevDay.EQ.Day) .AND. (PrevHour.EQ.Hour)) then
		PrevRain = PrevRain + Rain
	else
		! Check if this is the first record
		if (.NOT.(PrevYear.EQ.0)) then
			OutYear = PrevYear - 1900 !Gasp, Y2K Noncompliant
			OutDay = JulDate(PrevYear, PrevMonth, PrevDay)
			OutHour = PrevHour+1 !RAINEV looks only at hrs1-24
			OutRain = NINT(PrevRain*100)
			PrevRain = Rain
			write(OutUnit, 20) OutYear, OutDay, OutHour, OutRain
			20 format(I2,',',I3,',',I2,',',I3)
		else
			PrevRain = Rain
		end if
	end if

	if (EOF(InUnit)) then
		OutYear = PrevYear - 1900 !Gasp, Y2K Noncompliant
		OutDay = JulDate(PrevYear, PrevMonth, PrevDay)
		OutHour = PrevHour+1 !RAINEV looks only at hrs 1-24
		OutRain = NINT((PrevRain)*100)
		write(OutUnit, 20) OutYear, OutDay, OutHour, OutRain
	end if
end do

end program

!///////////////////////////////////////////////////////////////////////////////
! JulDate
INTEGER function JulDate(Year, Month, Day)
INTEGER Year, Month, Day
INTEGER LY

if (IsLeapYear(Year)) then
	LY = 1
else
	LY = 0
end if

select case (Month)
	case (1)
		JulDate = Day
	case (2)
		JulDate = 31+Day
	case (3)
		JulDate = 59+LY+Day
	case (4)
		JulDate = 90+LY+Day
	case (5)
		JulDate = 120+LY+Day
	case (6)
		JulDate = 151+LY+Day
	case (7)
		JulDate = 181+LY+Day
	case (8)
		JulDate = 212+LY+Day
	case (9)
		JulDate = 243+LY+Day
	case (10)
		JulDate = 273+LY+Day
	case (11)
		JulDate = 304+LY+Day
	case (12)
		JulDate = 334+LY+Day
end select

end function

!///////////////////////////////////////////////////////////////////////////////
! IsLeapYear
LOGICAL function IsLeapYear(Year)
INTEGER Year

if (MOD(Year,4).EQ.0) then
	! Special case: years divisible by 100 but not by 400 are NOT leap years
	! e.g., 1900 is not a leap year, but 2000 is
	if (MOD(Year,100).EQ.0) then
		if (MOD(Year,400).EQ.0) then
			IsLeapYear = .TRUE.
		else
			IsLeapYear = .FALSE.
		end if
	else
		IsLeapYear = .TRUE.
	end if
else
	IsLeapYear = .FALSE.
end if

end function