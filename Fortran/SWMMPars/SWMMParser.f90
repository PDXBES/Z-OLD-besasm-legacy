SUBROUTINE ParseExtran(InFileName, InFileNameLen, OutFileName, OutFileNameLen)
!DEC$ ATTRIBUTES DLLEXPORT :: ParseExtran
!DEC$ ATTRIBUTES STDCALL :: ParseExtran

	IMPLICIT NONE

	INTEGER InFileNameLen, OutFileNameLen
	CHARACTER InFilename*(InFileNameLen), OutFileName*(OutFileNameLen)

	INTEGER, PARAMETER :: IDWidth = 10, InUnit = 50, OutUnit=60
	INTEGER Counter, LastCounter, NumConds, NumJuncs
	LOGICAL FileFound, BreakCondLoop, BreakJuncLoop
	CHARACTER*20 ScanString
	CHARACTER*255 Dummy

	CHARACTER*80 Titles(2)
	INTEGER NumSteps, StartCycle, CycleInterval
	REAL TimeStep, StartTime
	CHARACTER*IDWidth ConduitID, JunctionID, UpJunc, DnJunc
	REAL Length, Area, Rough, Width, Depth
	REAL GrElev, Invert, QInst, InitDepth

	! Scan the file and count nodes and conduits
	INQUIRE(FILE=InFileName, EXIST=FileFound)
	IF (.NOT.FileFound) THEN
		!WRITE (*, '("File not found: ", A)') FileName
		STOP 1
	ELSE
		OPEN(UNIT=InUnit, FILE=InFileName)
		OPEN(UNIT=OutUnit, FILE=OutFileName, FORM='BINARY')
		BreakCondLoop = .FALSE.
		DO WHILE (.NOT.EOF(InUnit))
			READ(InUnit, '(A20)') ScanString
			IF (ScanString.EQ."  INP  CONDUIT    LE")  THEN
				READ(InUnit, *)
				READ(InUnit, *)
				DO WHILE (.NOT.EOF(InUnit).AND..NOT.(BreakCondLoop)) 
					READ(InUnit, '(I4)', ERR=1000) Counter
					IF (Counter.NE.0) THEN
						LastCounter = Counter
					END IF
					CYCLE
					! Jump to this point if we reached a page break or end of conduit list
					1000 BACKSPACE InUnit
					DO WHILE (.NOT.EOF(InUnit))
						READ(InUnit, '(A20)') ScanString
						IF (ScanString.EQ."  INP  CONDUIT    LE") THEN
							READ(InUnit, *)
							READ(InUnit, *)
							EXIT
						ELSE IF (ScanString.EQ." *  Equivalent Condu") THEN
							BACKSPACE InUnit
							BACKSPACE InUnit
							BACKSPACE InUnit
							BreakCondLoop = .TRUE.
							EXIT
						ELSE IF (ScanString.EQ." INP  JUNCTION    GR") THEN
							BACKSPACE InUnit
							BreakCondLoop = .TRUE.
							EXIT
						END IF
					END DO
				END DO
				NumConds = LastCounter
			END IF

			BreakJuncLoop = .FALSE.
			IF (ScanString.EQ." INP  JUNCTION    GR") THEN
				READ(InUnit, *)
				READ(InUnit, *)
				DO WHILE (.NOT.EOF(InUnit).AND..NOT.(BreakJuncLoop))
					READ(InUnit, '(I4)', ERR=1100) Counter
					IF (Counter.NE.0) THEN
						LastCounter = Counter
					END IF
					CYCLE
					! Jump to this point if we reached a page break or end of junction list
					1100 BACKSPACE InUnit
					DO WHILE (.NOT.EOF(InUnit))
						READ(InUnit, '(A20)') ScanString
						IF (ScanString.EQ." INP  JUNCTION    GR") THEN
							READ(InUnit, *)
							READ(InUnit, *)
							EXIT
						ELSE IF (ScanString.EQ."      lie above the ") THEN
							EXIT
!						ELSE IF (ScanString.EQ." # Element numbers o") THEN
!							BreakJuncLoop = .TRUE.
!							EXIT
						ELSE IF (ScanString.EQ." JUNCTION / DEPTH  /") THEN
							BACKSPACE InUnit
							BreakJuncLoop = .TRUE.
							EXIT
						END IF
					END DO
				END DO
				NumJuncs = LastCounter
			END IF
		END DO
	END IF

	! Allocate array according to number of junctions and conduits found
	WRITE(OutUnit) NumJuncs
	WRITE(OutUnit) NumConds

	! Read in the IDs and Data
	BreakCondLoop = .FALSE.
	REWIND InUnit
	DO WHILE (.NOT.EOF(InUnit))
		READ(InUnit, '(A20)') ScanString
		IF (ScanString.EQ."  INP  CONDUIT    LE")  THEN
			READ(InUnit, *)
			READ(InUnit, *)
			DO WHILE (.NOT.EOF(InUnit).AND..NOT.(BreakCondLoop)) 
				READ(InUnit, FMT=2000, ERR=1001) Counter, ConduitID, &
					Length, Dummy, Area, Rough, Width, Depth, UpJunc, DnJunc
				WRITE(OutUnit) ConduitID, Length, Area, Rough, Width, Depth, UpJunc, DnJunc
				2000 FORMAT(I4,A10,F10.0,A10,F10.2,F10.5,F10.2,F10.2,A10,A10)
				CYCLE
				! Jump to this point if we reached a page break or end of conduit list
				1001 BACKSPACE InUnit
				DO WHILE (.NOT.EOF(InUnit))
					READ(InUnit, '(A20)') ScanString
					IF (ScanString.EQ."  INP  CONDUIT    LE") THEN
						READ(InUnit, *)
						READ(InUnit, *)
						EXIT
					ELSE IF (ScanString.EQ." *  Equivalent Condu") THEN
						BACKSPACE InUnit
						BACKSPACE InUnit
						BACKSPACE InUnit
						BreakCondLoop = .TRUE.
						EXIT
					ELSE IF (ScanString.EQ." INP  JUNCTION    GR") THEN
						BACKSPACE InUnit
						BreakCondLoop = .TRUE.
						EXIT
					END IF
				END DO
			END DO
		END IF

		BreakJuncLoop = .FALSE.
		IF (ScanString.EQ." INP  JUNCTION    GR") THEN
			READ(InUnit, *)
			READ(InUnit, *)
			DO WHILE (.NOT.EOF(InUnit).AND..NOT.(BreakJuncLoop))
				READ(InUnit, FMT=2100, ERR=1101) Counter, JunctionID, GrElev, Dummy, Invert, &
					QInst, InitDepth
				WRITE(OutUnit) JunctionID, GrElev, Invert, QInst, InitDepth
				2100 FORMAT(I4,A10,F10.2,A10,F10.2,F10.2,F10.2)
				CYCLE
				! Jump to this point if we reached a page break or end of junction list
				1101 BACKSPACE InUnit
				DO WHILE (.NOT.EOF(InUnit))
					READ(InUnit, '(A20)') ScanString
					IF (ScanString.EQ." INP  JUNCTION    GR") THEN
						READ(InUnit, *)
						READ(InUnit, *)
						EXIT
					ELSE IF (ScanString.EQ."      lie above the ") THEN
						EXIT
!					ELSE IF (ScanString.EQ." # Element numbers o") THEN
!						BreakJuncLoop = .TRUE.
!						EXIT
					ELSE IF (ScanString.EQ." JUNCTION / DEPTH  /") THEN
						BACKSPACE InUnit
						BreakJuncLoop = .TRUE.
						EXIT
					END IF
				END DO
			END DO
		END IF
	END DO

	CLOSE(InUnit)
	CLOSE(OutUnit)
	
END SUBROUTINE ParseExtran
