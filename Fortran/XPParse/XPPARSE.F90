PROGRAM XPPARSE
! Routine to automatically read SWMM4 EXTRAN Generic.out output file and 
! parse for spreadsheet format

! function is to read number of time steps, timing parameters, and
!  the depth, conduit flow fields from summary output section.
!     6/23/95:  Add in ability to create interface file
!     7/24/96:  AMM Added XTPARSER capabilities
!                 if no AXTPARSE.INP file found, assume XTPARSER functions.
!                 if no GENERIC.OUT file found, ask for an output file.
!                 XTPARSER outputs to EX_COND.CSV and EX_JUNC.CSV.
!                 XTPARSER at this time does not print out the runtime messages. 
!    10/17/96: VCA - Increase Format to write large numbers of hours
!     8/15/97: VCA - Correct writing of XFL & XHG Files for first time step
!     9/29/98: AMM - Changed formats to XPSWMM 5.2 Output Format
!======================================================================
USE DFLIB
IMPLICIT NONE
!=======================================================================
!     NW   = NUMBER OF SUBCATCHMENTS IN THE RUNOFF BLOCK
!     NG   = NUMBER OF GUTTER/PIPES IN THE RUNOFF BLOCK
!     NET  = NUMBER OF ELEMENTS IN THE TRANSPORT BLOCK
!     NTH  = NUMBER OF INPUT HYDROGRAPHS IN TRANSPORT
!     NEE  = NUMBER OF ELEMENTS IN EXTRAN BLOCK
!     NGW  = NUMBER OF SUBCATCHMENTS WITH GROUNDWATER
!                     COMPARTMENTS IN RUNOFF
!     NIE  = NUMBER OF INTERFACE LOCATIONS FOR ALL BLOCKS
!     NEP  = NUMBER OF EXTRAN PUMPS
!     NEO  = NUMBER OF EXTRAN ORIFICES
!     NTG  = NUMBER OF TIDE GATES OR FREE OUTFALLS IN EXTRAN
!     NEW  = NUMBER OF EXTRAN WEIRS
!     NPO  = NUMBER OF EXTRAN PRINTOUT LOCATIONS
!     NTE  = NUMBER OF TIDE ELEMENTS IN EXTRAN
!     NNC  = NUMBER OF NATURAL CHANNELS IN EXTRAN AND TRANSPORT
!     NVSE = NUMBER OF STORAGE JUNCTIONS IN EXTRAN
!     NVST = NUMBER OF DATA POINTS FOR VARIABLE STORAGE ELEMENTS
!            IN THE EXTRAN BLOCK
!     NEH  = NUMBER OF INPUT HYDROGRAPHS IN THE EXTRAN BLOCK
!
!     INSTRUCTIONS - INCREASE DIMENSIONS OF SUBCATCHMENTS ETC.
!                    BY MODIFYING THE PARAMETER STATEMENT
!                    AND RECOMPILING YOUR PROGRAM
!=======================================================================
INTEGER*2 NW,NG,NET,NEE,NGW,NTH,NIE,NTE,NEW,NEO,NEP,NTG,NPO,NVSE, &
	NVST,NNC,NEH,INCNT,IOUTCT,JIN,JOUT,JCE,NSCRAT,N5,N6,JKP, &
	FINJUNC,FINCOND,FAXT,FNEWINT,FIN,FJUNC,FCOND
CHARACTER*2 CC
CHARACTER    TITLE*80,SOURCE*20,PNAME*8,PUNIT*8,KAN*16
INTEGER NBD,NLOC,NDIM,NQUAL,LOCATS,IDATEZ,NEXT,LAST,NJCE
REAL TRIBA,TZERO,QCONV
!
CHARACTER DUMMY1*36,DUMMY3*6,SUR*1
CHARACTER DUMMY4*14,BLANK*10,AJUNC*10,ACOND*10,DUMMY5*13
CHARACTER KAJUNC*10, KACOND*10
CHARACTER ECOND*10
CHARACTER*240 SWMMFILE
LOGICAL FILEFOUND,HIT,XTPARSER,ControlsFound
INTEGER ReqType,NumReqJuncs,NumReqConds,ControlsCount
INTEGER, PARAMETER :: rtNone = 0,	rtJunc = 1, rtCond = 2, rtBoth = 3

INTEGER MAXD,JSUR,ICOND,KCOND,JUNC,KJUNC, &
	I,J,N7,IPCON,NDT,NSTART,IPCYC,NCN,NC, &
	NJN,NJ,LINEC,NCEXTRA,LINEJ,JN,ICN,ICYC,IRD, &
	K,IC,NYEAR,JDAY,JULDAY,YESDAY,L,M,N,X
REAL PRTGD,PRTQ,CROWN,BOTTOM,PRTBOT,PRTCWN,ELEV,DEPTH,FLOW, &
	PRTE,GRND,DELT,DELTA,TIME,TIMDAY,SFLOW,A
! AMM 6/22/95
! AXTPARSE.INP Data
CHARACTER AXTFILE*240,NMINT*40,NMROOT*40
INTEGER NCOND,ANODE,ACONDS
! AMM 7/24/96 Added following array types
REAL LENGTH,DIAMTR,DNOFF,UPOFF,PRTLEN,PRTDIA,PRTDNO,PRTUPO
! AMM 9/28/98 Added following to calculate times
INTEGER HOURS
REAL MINUTES
CHARACTER*10 DUM10
CHARACTER*33 DUM33
CHARACTER*10 UPNODE,DNNODE
REAL UPELEV,DNELEV
!
INTEGER CursorStatus
TYPE (rccoord) ScreenPos
CHARACTER*240 CurrentLine
!
PARAMETER(NW=1,NG=1,NET=1,NEE=1,NGW=1,NTH=1, &
	NIE=500,NTE=1,NEW=1,NEO=1,NEP=1,NTG=1, &
	NPO=1,NVSE=1,NVST=1,NNC=1,NEH=1)
PARAMETER(MAXD=500)
! Unit numbers
PARAMETER(FINJUNC=56,FINCOND=57,FAXT=50,FNEWINT=51, &
	FIN=15,FJUNC=16,FCOND=17)
DIMENSION JUNC(MAXD),DEPTH(MAXD),ELEV(MAXD),FLOW(MAXD), &
	ICOND(MAXD),KJUNC(MAXD),KCOND(MAXD),IPCON(MAXD), &
	PRTE(MAXD),PRTQ(MAXD),SUR(MAXD),JSUR(MAXD), &
	GRND(MAXD),BOTTOM(MAXD),PRTGD(MAXD),PRTBOT(MAXD), &
	AJUNC(MAXD),ACOND(MAXD),PRTCWN(MAXD),CROWN(MAXD),SFLOW(MAXD), &
	LENGTH(MAXD),DIAMTR(MAXD),DNOFF(MAXD),UPOFF(MAXD),PRTLEN(MAXD), &
	PRTDIA(MAXD),PRTDNO(MAXD),PRTUPO(MAXD),KAJUNC(MAXD),KACOND(MAXD)
DIMENSION JIN(25),JOUT(25),NSCRAT(7),JKP(57)
DIMENSION NBD(6),NLOC(NIE),NDIM(10)
DIMENSION PNAME(10),PUNIT(10),TITLE(4),KAN(NIE)
DIMENSION ANODE(MAXD),NCOND(MAXD),ACONDS(MAXD,8)
DIMENSION UPNODE(MAXD),DNNODE(MAXD),UPELEV(MAXD),DNELEV(MAXD)
!      
DATA BLANK/'          '/
!
! check to see if requested junction/cond data has been output from acad
! Index ReqType used to indicate which files (junc or cond data) were
! requested.
!   ReqType=rtNone->no request
!		ReqType=rtJunc->junction data only, 
!		ReqType=rtCond->conduit data only
!		ReqType=rtBoth->both
!

CALL CLEARSCREEN($GCLEARSCREEN)
CursorStatus = DISPLAYCURSOR($GCURSORON)

ReqType=rtNone
NumReqJuncs=0
NumReqConds=0
WRITE(*,'(A)') ' ****************'
WRITE(*,'(A)') ' *** XPPARSER ***'
WRITE(*,'(A)') ' ****************'
WRITE(*,'(A/)') ' 2/2/99 Build'
INQUIRE(FILE='AC_JUNC.DAT',EXIST=FILEFOUND)
IF (FILEFOUND) THEN
	OPEN(FINJUNC,ERR=3100,FILE='AC_JUNC.DAT',STATUS='OLD')
	ReqType=rtJunc
	DO I=1,1000
		READ(FINJUNC,*,END=3101,ERR=3101)KAJUNC(I)
		IF(KAJUNC(I).EQ.BLANK) EXIT
		NumReqJuncs=NumReqJuncs+1
	END DO
!
  IF(NumReqJuncs.GT.500)THEN
		WRITE(*,3200)
		NumReqJuncs=500
  END IF
END IF
!
INQUIRE(FILE='AC_COND.DAT',EXIST=FILEFOUND)
IF(FILEFOUND)THEN
	OPEN(FINCOND,ERR=3100,FILE='AC_COND.DAT',STATUS='OLD')
	ReqType=ReqType + rtCond
!
	DO I=1,1000
		READ(FINCOND,*,END=50000,ERR=3102)KACOND(I)
		IF(KACOND(I).EQ.BLANK) EXIT
			NumReqConds = NumReqConds + 1
	END DO
!
	50000 IF(NumReqConds.GT.500)THEN
		WRITE(*,3200)
		NumReqConds=500
	END IF
END IF
! AMM 6/22/95
! Request AXTPARSE.INP file
! AMM 7/26/96 Modified to perform like XTPARSER if AXTPARSE.INP not found.
!             Previously, AXTPARSE would ask where an AXTPARSE input file
!             was located.
INQUIRE(FILE='AXTPARSE.INP',EXIST=FILEFOUND)
IF (FILEFOUND) THEN
	OPEN(FAXT,ERR=3100,FILE='AXTPARSE.INP',STATUS='OLD')
	XTPARSER = .FALSE.
	WRITE(*,'(/A/)')'  AXTPARSE.INP found, Running Auto-XTParser'
! Read in AXTPARSE.INP file
	READ(FAXT,*) NMINT,NMROOT
	READ(FAXT,*) TITLE(1)
	READ(FAXT,*) TITLE(2)
	READ(FAXT,*) IDATEZ
	READ(FAXT,*) LOCATS
	DO I = 1,LOCATS
		READ(FAXT,*) ANODE(I),NCOND(I),(ACONDS(I,J),J=1,NCOND(I))
	END DO
	NQUAL = 0
ELSE
	WRITE(*,'(A)')'  No AXTPARSE.INP file found, Running XTParser. '
	XTPARSER = .TRUE.
END IF
! here request file names and open for ASCII output
INQUIRE(FILE='generic.out',EXIST=FILEFOUND)
IF(FILEFOUND)THEN
	OPEN(FIN,ERR=3100,FILE='generic.out',STATUS='OLD')
	SWMMFile='GENERIC.OUT'
	WRITE(*,'(/A/)')'  GENERIC.OUT found.'
ELSE
	WRITE(*,'(/A)')'  No GENERIC.OUT file found. '
	WRITE(*,'(A\)')'  Please Enter Path & Name of Output File: '
	READ(*,'(A\)') SWMMFile
	INQUIRE(FILE=SWMMFile,EXIST=FILEFOUND)
	IF(FILEFOUND)THEN
		OPEN(FIN,ERR=3100,FILE=SWMMFile,STATUS='OLD')
	ELSE        
		WRITE(*,'(A\)')' ERROR! File Not Found! '
    STOP
	END IF
END IF
! AMM 7/24/96 Added branch if XTPARSER is on; if XTPARSER is running,
! use the original EX_COND.CSV and EX_JUNC.CSV filenames.  Otherwise, use
! AXTPARSER's .XHG and .XFL extensions.
IF (XTPARSER) THEN
	IF(ReqType.NE.rtCond) OPEN(FJUNC,FILE='EX_JUNC.CSV')
	IF(ReqType.NE.rtJunc) OPEN(FCOND,FILE='EX_COND.CSV')
ELSE
	IF(ReqType.NE.rtCond) OPEN(FJUNC,FILE=NMROOT//'.XHG')
	IF(ReqType.NE.rtJunc) OPEN(FCOND,FILE=NMROOT//'.XFL') 
	OPEN(FNEWINT,FILE=NMINT,FORM='UNFORMATTED')
ENDIF
!
DO J=1,MAXD
	JSUR(J)=0
END DO
NJCE=0
!      
DO J = 1, 4
	TITLE(J) = ' '
END DO
!     Write message to user on status of processing:
! AMM 7/24/96 Added branch to process XTPARSER stuff
IF (XTPARSER) THEN
	WRITE(*,4130) SWMMFile
	WRITE(*,4140)
  4130 FORMAT(/,10X,' **********  EXTRAN-PARSER  **********',//, &
		10X,' Processing Extran Output File: ',A40)
	4140 FORMAT(/,18X,'==>> Is the Current Julian Date')
ELSE
	SWMMFile = NMROOT//'.OUT'
	WRITE(*,4110) SWMMFile
  4110 FORMAT(/,10X,' **********  AUTO-EXTRAN-PARSER  **********',//, &
		10X,' Processing Extran Output File: ',A40)
	WRITE(*,4120)
	4120 FORMAT( /,18X,'==>> Is the Current Julian Date')
END IF
!
! HERE READ DATA FILE TO FIND CONTROLS
!
! AMM 9/28/98 Changed formats of read from 8 width to 10
ControlsFound = .FALSE.
ControlsCount = 0
DO WHILE (.NOT.(EOF(FIN).OR.ControlsFound))
	READ(FIN,'(A)')DUMMY1
	IF(DUMMY1.EQ.' Integration cycles.................')THEN
		BACKSPACE FIN
		READ(FIN,'(36X,I10)')NDT
		ControlsCount = ControlsCount + 1
	ELSEIF(DUMMY1.EQ.' Length of integration step is......')THEN
		BACKSPACE FIN
		READ(FIN,'(36X,F10.2)')DELT
		ControlsCount = ControlsCount + 1
	ELSEIF(DUMMY1.EQ.' Printing starts in cycle...........')THEN
		BACKSPACE FIN
		READ(FIN,'(36X,I10)')NSTART
		ControlsCount = ControlsCount + 1
	ELSEIF(DUMMY1.EQ.' Intermediate printout intervals of.')THEN
		IF(IPCYC.LE.0) THEN
			BACKSPACE FIN
			READ(FIN,'(36X,I10)')IPCYC
		ENDIF
		ControlsCount = ControlsCount + 1
	ELSEIF(DUMMY1.EQ.' Initial time.......................')THEN
		BACKSPACE FIN
		READ(FIN,'(36X,F10.2)') TZero
		ControlsCount = ControlsCount + 1
	ENDIF
	IF (ControlsCount.GE.5) THEN
		ControlsFound = .TRUE.
	END IF
END DO

IF (EOF(FIN)) THEN
	WRITE(*,'(A)')' READING ERROR!  File may not produced have been ', &
		'produced by XPSWMM Version 5.x'
	STOP
END IF
! finished reading controls--look for warning messages and initialize

! AMM 9/29/98 Added rewind since titles in XPS are found only at beginning      
100 REWIND FIN
DO I=1,1000000
	READ(FIN,'(A)',ERR=111) DUMMY4
! Check for Title(1) and Title(2)
	IF(Title(1).EQ.' ') THEN
		IF(DUMMY4.EQ.' #############') THEN
			READ(FIN,*)
      READ(FIN,*)
      READ(FIN,*)
      READ(FIN,*)
      READ(FIN,*)
      READ(FIN,*)
      READ(FIN,'(5X,A)') Title(1)
      READ(FIN,'(5X,A)') Title(2)
		ENDIF
	ENDIF
!        
	IF(DUMMY4.EQ.' Title from fi')THEN
    READ(FIN,'(1X,A80)')TITLE(3)
    READ(FIN,'(1X,A80)')TITLE(4)
    READ(FIN,*)
    READ(FIN,*)
    READ(FIN,*)
    READ(FIN,*)
    READ(FIN,*)
    READ(FIN,*)
    READ(FIN,'(52X,I8)') IDATEZ
    READ(FIN,'(52X,F8.1)') TZERO
    READ(FIN,*)
    READ(FIN,*)
    READ(FIN,'(52X,F8.2)') TRIBA
	ENDIF
! read conduit numbers
	IF(DUMMY4.EQ.'  Inp Conduit ')THEN
    READ(FIN,'(A)')
    READ(FIN,'(A)')
		NJCE=1
		DO NCN=1,10000
			105 IF(NJCE.EQ.0)THEN
				IF (XTPARSER) THEN
					READ(FIN,10001,ERR=114)J
          BACKSPACE FIN
          IF (J.EQ.0) THEN
					! AMM 7/31/96 Changed following statement from NC=NCN-1 because NCN was
					!             found to reset itself when the GOTO 111 statement is
					!             executed.
						NC=NC+NCN-1
						GOTO 111
					ELSE
						READ(FIN,10001,ERR=114)J,ICOND(J),LENGTH(J),DUMMY5,A, &
							A,DIAMTR(J),A,X,X,DNOFF(J),UPOFF(J)
						10001 FORMAT(I4,I10,F10.0,A10,F10.2,F10.5,2(F10.2),2(I10),2(F8.2))
					END IF
				ELSE
					READ(FIN,*,ERR=114)J,ICOND(J)
				END IF
			ELSE
				READ(FIN,11001,ERR=114)J,ACOND(J),LENGTH(J),DUM33,DIAMTR(J)
				11001 FORMAT(I4,1X,A10,1X,F10.2,1X,A33,F10.5)
			END IF
      CYCLE
!     READ ERR-CHK FOR MORE CONDUITS ON NEXT PAGE AND GET WARNINGS
			114 BACKSPACE FIN
			DO J=1,1000
				READ(FIN,'(A)')DUMMY4
				IF(DUMMY4.EQ.'  Inp Conduit ')THEN
					READ(FIN,'(A)')
					READ(FIN,'(A)')
					GO TO 105
        END IF
				IF(DUMMY4.EQ.' *  Equivalent')THEN
					BACKSPACE FIN
					BACKSPACE FIN
					BACKSPACE FIN
					NC=NCN-1
					GO TO 111
				END IF
				IF(DUMMY4.EQ.' Inp  Junction')THEN
					BACKSPACE FIN
					NC=NCN-1
					GO TO 111
				END IF
			END DO
		END DO
	END IF
!
! AMM 9/30/98 Added following section since offsets are no longer recorded--
!             Only absolutes are provided
	IF(DUMMY4.EQ.' Input  Condui')THEN
		IF(NJCE.EQ.1) THEN
			READ(FIN,'(A)')
			READ(FIN,'(A)')
			DO NCN=1,NC
				READ(FIN,11002,ERR=11100)J,DUM10,UPNODE(J),DNNODE(J),UPELEV(J),DNELEV(J)
				11002 FORMAT(2X,I4,2X,3(A10,1X),2(F12.3))
				CYCLE
				11100 STOP
			END DO
		ENDIF
	ENDIF
! read junction data
	IF(DUMMY4.EQ.' Inp  Junction')THEN
		READ(FIN,'(A)')
		READ(FIN,'(A)')
		DO NJN=1,1000
			IF(NJCE.EQ.0)THEN
				READ(FIN,*,ERR=116)J,JUNC(J),GRND(J),CROWN(J),BOTTOM(J)
			ELSE
				READ(FIN,*,ERR=116)J,AJUNC(J),GRND(J),CROWN(J),BOTTOM(J)
			END IF
			CYCLE
			! READ ERR-CHECK FOR MORE JUNCTIONS ON NEXT PAGE OR CONDUIT WARNING MSG
			116	BACKSPACE FIN
			DO J=1,1000
				READ(FIN,'(A)')DUMMY4
				IF(DUMMY4.EQ.' INP  JUNCTION')THEN
					READ(FIN,'(A)')
					READ(FIN,'(A)')
					EXIT
				ELSEIF(DUMMY4.EQ.'      lie abov')THEN
					EXIT
				END IF
				IF(DUMMY4.EQ.' STORAGE JUNCT' .OR. DUMMY4.EQ.' Input  Condui' .OR. &
					DUMMY4.EQ.' *        DIUR' .OR. DUMMY4.EQ.' # Header info' .OR. &
					DUMMY4.EQ.' *        INTE')THEN
					NJ=NJN-1
					LINEJ=FLOAT(NJ)/3.+.7
					BACKSPACE FIN
					GO TO 111
				END IF
			END DO
		END DO
	END IF
! Find extra conduits created internally
	IF(DUMMY4.EQ.' |        INTE')THEN
		DO J=1,4
			READ(FIN,*)
		END DO
    DO J=1,1000
      IF(NJCE.EQ.0)THEN
        READ(FIN,'(1X,I14)')NCEXTRA
        IF(NCEXTRA.EQ.0)THEN
          EXIT
        ELSE
          NC=NC+1
          ICOND(NC)=NCEXTRA
        END IF
      ELSE
        READ(FIN,'(5X,A)')ECOND
        IF(ECOND.EQ.BLANK)THEN
          EXIT
        ELSE
          NC=NC+1
          ACOND(NC)=ECOND
        END IF
      END IF
		END DO
		LINEC=FLOAT(NC)/4.+0.8
	ENDIF
! Read initial junction conditions
	IF(DUMMY4.EQ.' Junction / De')THEN
		IF(NJCE.EQ.0)THEN
			DO JN=1,LINEJ,3
				READ(FIN,6000,ERR=151)JUNC(JN),DEPTH(JN),ELEV(JN), &
					JUNC(JN+1),DEPTH(JN+1),ELEV(JN+1), &
					JUNC(JN+2),DEPTH(JN+2),ELEV(JN+2)
			END DO
		ELSE
			DO JN=1,LINEJ*3,3
				READ(FIN,6014,ERR=151)DUM10,DEPTH(JN),ELEV(JN), &
					DUM10,DEPTH(JN+1),ELEV(JN+1), &
					DUM10,DEPTH(JN+2),ELEV(JN+2)
				IF(DUM10.EQ.BLANK) EXIT
			END DO
		ENDIF
    151 BACKSPACE FIN
	END IF
! Read initial conduit conditions
	IF(DUMMY4.EQ.'   Conduit/   ')THEN
		IF(NJCE.EQ.0)THEN
			DO ICN=1,LINEC,4
				READ(FIN,6001,ERR=111)ICOND(ICN),FLOW(ICN),ICOND(ICN+1), &
					FLOW(ICN+1),ICOND(ICN+2),FLOW(ICN+2), &
					ICOND(ICN+3),FLOW(ICN+3)
			END DO
		ELSE
			DO ICN=1,LINEC*4,4
				READ(FIN,6016,ERR=111)ACOND(ICN),FLOW(ICN),ACOND(ICN+1), &
					FLOW(ICN+1),ACOND(ICN+2),FLOW(ICN+2), &
					FLOW(ICN+3),ACOND(ICN+3),FLOW(ICN+3)
			END DO
		ENDIF
!        Finished Reading all that we can now:
    EXIT
	END IF
END DO

!       Now assign Delta Time Step and default Source
111 SOURCE = 'PDXTran-AXTParser'
DELTA = 60.0 * FLOAT(INT(DELT * IPCYC + 5)/60)
NYEAR = IDATEZ / 1000
JDAY = IDATEZ - NYEAR*1000
! AMM 6/22/95
! Reprocess the node/conduit list from AXTPARSE.INP so that the conduit
! list uses the array indexes in ICOND().
DO I=1,LOCATS
	DO J=1,NCOND(I)
		HIT = .FALSE.
		K = 1
		IF (NJCE.EQ.1) THEN
			DO WHILE ((K.LE.MAXD).AND.(.NOT.HIT))
				IF(ICOND(K).EQ.ACONDS(I,J)) THEN
					ACONDS(I,J) = K
					HIT = .TRUE.
				ELSE
					K = K + 1
				END IF
			END DO
			IF (K .GT. MAXD) THEN
				WRITE(*,8600) ACONDS(I,J),ANODE(I)
				8600 FORMAT(/,' ===> ERROR! CONDUIT',I10,' CONNECTING TO ',I10, &
						 ' NOT FOUND IN EXTRAN FILE')
				STOP
			END IF
		ELSE
			DO WHILE ((K.LE.MAXD).AND.(.NOT.HIT))
				IF(ICOND(K).EQ.ACONDS(I,J)) THEN
					ACONDS(I,J) = K
					HIT = .TRUE.
				ELSE
					K = K+1
				END IF
			END DO
			IF (K .GT. MAXD) THEN
				WRITE(*,8600) ACONDS(I,J),ANODE(I)
				STOP
			END IF
		END IF
	END DO
END DO
! AMM 6/22/95
! Write out interface header
IF (.NOT. XTPARSER) THEN
	WRITE(FNEWINT) TITLE(1)
	WRITE(FNEWINT) TITLE(2)
	WRITE(FNEWINT) IDATEZ, TZERO
	WRITE(FNEWINT) TITLE(3)
	WRITE(FNEWINT) TITLE(4)
	WRITE(FNEWINT) SOURCE, LOCATS, NQUAL, TRIBA, NJCE
	IF(NJCE.LE.0) WRITE(FNEWINT) (ANODE(K), K=1, LOCATS)
	IF(NJCE.GE.1) WRITE(FNEWINT) (KAN(I),I=1,LOCATS)
	IF(NQUAL.GT.0) WRITE(FNEWINT)  (PNAME(J),J=1,NQUAL)
	IF(NQUAL.GT.0) WRITE(FNEWINT)  (PUNIT(J),J=1,NQUAL)
	IF(NQUAL.GT.0) WRITE(FNEWINT)  (NDIM(J),J=1,NQUAL)
	QCONV = 1.0
	WRITE(FNEWINT) QCONV
ENDIF
! NOW WRITE THE TOP OF OUTPUT FILE AND INITIAL CONDITION
!       Test if user wants all or a select few.  Write full inteface
!       data/header only for ReqType = 0
! AMM 7/30/96 Changed this so that full interface data/header will
!             always print when XTPARSER function is used.
IF(ReqType.EQ.rtNone)THEN
	IF (XTPARSER) THEN
		WRITE(FJUNC,'(A)')'"Junction WSElev Data From XTParser"'
		WRITE(FJUNC,10005)SWMMFile
		WRITE(FCOND,'(A)')'"Conduit Flow Data From XTParser"'
		WRITE(FCOND,10005)SWMMFile
		10005 FORMAT('"',A,'"')
	ELSE
		WRITE(FJUNC,'(A)')'"Junction WSElev Data From Auto-XTParser"'
		WRITE(FCOND,'(A)')'"Conduit Flow Data From Auto-XTParser"'
	END IF
	WRITE(FJUNC,4000) Title(1),Title(2),IDateZ,TZero, &
		Title(3),Title(4),Source,TRIBA,Delta,NJ
	WRITE(FCOND,4000) Title(1),Title(2),IDateZ,TZero, &
		Title(3),Title(4),Source,TRIBA,Delta,NC
  IF(NJCE.EQ.0)THEN
    WRITE(FJUNC,6008)(JUNC(J),J=1,NJ)
    WRITE(FCOND,6018)(ICOND(J),J=1,NC)
  ELSE
    WRITE(FJUNC,6013)(AJUNC(J),J=1,NJ)
    WRITE(FCOND,6023)(ACOND(J),J=1,NC)
  END IF
	WRITE(FJUNC,6009)(GRND(J),J=1,NJ)
	WRITE(FJUNC,6012)(CROWN(J),J=1,NJ)
	WRITE(FJUNC,6011)(BOTTOM(J),J=1,NJ)
	WRITE(FJUNC,'(/)')
	IF (XTPARSER) THEN
		WRITE(FCOND, 6024)(LENGTH(J),J=1,NC)
		WRITE(FCOND, 6025)(DIAMTR(J),J=1,NC)
		WRITE(FCOND, 6026)(DNELEV(J),J=1,NC)
		WRITE(FCOND, 6027)(UPELEV(J),J=1,NC)
	END IF
	WRITE(FCOND,'(/)')
	IF(NSTART.LE.1)THEN
		WRITE(FJUNC,6003)TZERO/3600.,(BOTTOM(J),J=1,NJ)
		WRITE(FCOND,6003)TZERO/3600.,(FLOW(J),J=1,NC)
	END IF
ELSE
	IF(ReqType.EQ.rtJunc .OR. ReqType.EQ.rtBoth)THEN
		IF (XTPARSER) THEN
			WRITE(FJUNC,'(A)')'"Junction WSElev Data From XTParser"'
			WRITE(FJUNC,10005)SWMMFile
			WRITE(FCOND,'(A)')'"Conduit Flow Data From XTParser"'
			WRITE(FCOND,10005)SWMMFile
			WRITE(FJUNC,4000) Title(1),Title(2),IDateZ,TZero, &
				Title(3),Title(4),Source,TRIBA,Delta,NumReqJuncs
			WRITE(FJUNC,4000) Title(1),Title(2),IDateZ,TZero, &
				Title(3),Title(4),Source,TRIBA,Delta,NumReqConds
		END IF
		IF (.NOT.XTPARSER) THEN
			WRITE(FJUNC,'(A,I10/)')' "STARTING DATE =", ',IDATEZ
		END IF
		WRITE(FJUNC,6008)(KAJUNC(J),J=1,NumReqJuncs)
		DO K=1,NumReqJuncs
			DO J=1,NJ
				IF(AJUNC(J).EQ.KAJUNC(K))GO TO 449
			END DO
			WRITE(*,7000)KAJUNC(K)
			STOP
			449 PRTGD(K)=GRND(J)
			PRTCWN(K)=CROWN(J)
			PRTBOT(K)=BOTTOM(J)
		END DO
		WRITE(FJUNC,6009)(PRTGD(J),J=1,NumReqJuncs)
		WRITE(FJUNC,6012)(PRTCWN(J),J=1,NumReqJuncs)
		WRITE(FJUNC,6011)(PRTBOT(J),J=1,NumReqJuncs)
		WRITE(FJUNC,'(/)')
	END IF
	IF(ReqType.GT.rtJunc)THEN
		IF (.NOT.XTPARSER) THEN
			WRITE(FCOND,'(A,I10/)')' "STARTING DATE =", ',IDATEZ
		ENDIF
		IF (NJCE.EQ.0) THEN
			WRITE(FCOND,6018)(KCOND(J),J=1,NumReqConds)
		ELSE
			WRITE(FCOND,6023)(KACOND(J),J=1,NumReqConds)
		ENDIF
		DO K=1,NumReqConds
			DO J=1,MAXD
				IF(ACOND(J).EQ.KACOND(K))GO TO 499
			END DO
			WRITE(*,7100)KCOND(K)
			STOP
			499 IPCON(K)=J
			PRTLEN(K)=LENGTH(J)
			PRTDIA(K)=DIAMTR(J)
			PRTDNO(K)=DNELEV(J)
			PRTUPO(K)=UPELEV(J)
		END DO
		WRITE(FCOND,6024)(PRTLEN(J),J=1,NumReqConds)
		WRITE(FCOND,6025)(PRTDIA(J),J=1,NumReqConds)
		WRITE(FCOND,6026)(PRTDNO(J),J=1,NumReqConds)
		WRITE(FCOND,6027)(PRTUPO(J),J=1,NumReqConds)
		WRITE(FCOND,'(/)')
	ENDIF
ENDIF
! ' CUMULATIVE OVERFLOW VOLUME FROM NODE'
! NOW FIND THE FIRST CYCLE OF OUTPUT
DO I=1,1000000 
! BEGIN DO LOOP FOR EACH TIME STEP/PRINT INTERVAL
	READ(FIN,'(A)')DUMMY3
	IF(DUMMY3.EQ.' Cycle') EXIT
END DO
! NOW WE HAVE FOUND THE FIRST CYCLE TIME OUTPUT
!
BACKSPACE FIN
! AMM 9/28/98 For some reason, XP has a smaller width for cycle number
READ(FIN,'(23X,I4,6X,F6.2)')HOURS,MINUTES
! ICYC is the number of time steps skipped
IRD=(NDT-NSTART)/IPCYC
DO IC=1,IRD+1
! AMM 9/28/98 Recalc since XP's ICYC is unreliable
!        TIME=(ICYC*DELT+TZERO)/3600.
	TIME=TZERO+HOURS+MINUTES/60
! Calculate Julian Day for Interface File
	JDAY = (IDATEZ - NYEAR*1000) + INT((TIME+.01)/24.)
	IF(JDAY.GT.365) THEN
		JDAY = JDAY - 365
		NYEAR = (IDATEZ/1000) + INT(JDAY/365.)
	ENDIF
	JULDAY = NYEAR*1000 + JDAY
! Calculate TIMDAY for Interface file - round to nearest Delta
  TIMDAY = TIME - 24.*Int((TIME+.01)/24.)
  IF(TIMDAY.LT.0.00) TIMDAY = 0.00
  TIMDAY = Delta * Float(Int((TIMDAY * 3600.+5.)/Delta))
	DO K=1,1000
		READ(FIN,'(A)',ERR=52,END=52)DUMMY3
    IF(DUMMY3.EQ.' Junct') EXIT
	END DO

	GOTO 53
	52 BACKSPACE FIN
	READ(FIN,*) CurrentLine
	WRITE (*,'(A,/,A,/)') 'Error reading file.', CurrentLine
	STOP

	53 DO JN=1,LINEJ*3,3
		IF(NJCE.EQ.0)THEN
			READ(FIN,6010,ERR=40)JUNC(JN),DEPTH(JN),SUR(JN),ELEV(JN), &
				JUNC(JN+1),DEPTH(JN+1),SUR(JN+1),ELEV(JN+1), &
				JUNC(JN+2),DEPTH(JN+2),SUR(JN+2),ELEV(JN+2)
		ELSE
			READ(FIN,6014,ERR=40)DUM10,DEPTH(JN),SUR(JN),ELEV(JN), &
				DUM10,DEPTH(JN+1),SUR(JN+1),ELEV(JN+1), &
				DUM10,DEPTH(JN+2),SUR(JN+2),ELEV(JN+2)
		ENDIF
    DO J=JN,JN+2
      IF(SUR(J).NE.' ' .OR. ELEV(J).GT.CROWN(J))THEN
        IF(ELEV(J)-GRND(J) .LE. 1.E-06)JSUR(J)=1
      ENDIF
		END DO
		CYCLE
		40 EXIT
	END DO
!
	BACKSPACE FIN
	DO K=1,1000
		READ(FIN,'(A)')DUMMY3
		IF(DUMMY3.EQ.'   Con') EXIT
	END DO

	DO ICN=1,LINEC*4,4
		IF(NJCE.EQ.0)THEN
			READ(FIN,6001,ERR=42)ICOND(ICN),FLOW(ICN),ICOND(ICN+1), &
				FLOW(ICN+1),ICOND(ICN+2),FLOW(ICN+2), &
				ICOND(ICN+3),FLOW(ICN+3)
		ELSE
			READ(FIN,6016,ERR=42)DUM10,FLOW(ICN),DUM10, &
				FLOW(ICN+1),DUM10,FLOW(ICN+2),DUM10,FLOW(ICN+3)
		ENDIF
		CYCLE
		42 EXIT
	END DO
! Write message to screen to tell user the current date examined:
! AMM 7/24/96 line 4130 renumbered to 4150 to avoid conflict with new
! line 4130 above.
	IF(JULDAY.NE.YESDAY) THEN
		CALL SETTEXTPOSITION(15, 5, ScreenPos)
!		WRITE(*,4150) JulDay
		WRITE(*,*) JulDay
!		4150 FORMAT(' ',10X,I6,$,T1)
 		YESDAY = JulDay
	ENDIF
!         WRITE DATA TO FILES:
  IF(ReqType.EQ.rtNone)THEN
    WRITE(FJUNC,6003)TIME,(ELEV(J),J=1,NJ)
    WRITE(FCOND,6003)TIME,(FLOW(J),J=1,NC)
		! AMM 6/22/95
		! Write out time step to interface file
    DO L = 1, LOCATS
      SFLOW(L) = 0
      DO M=1, NCOND(L)
        SFLOW(L) = SFLOW(L)+FLOW((ACONDS(L,M)))*QCONV
			END DO
		END DO
    IF (.NOT. XTPARSER) THEN
      WRITE(FNEWINT) julday, TIMDAY, delta, (SFLOW(N),N=1,LOCATS)
    END IF
  ELSE
    IF(ReqType.EQ.rtJunc .OR. ReqType.EQ.rtBoth)THEN
      DO K=1,NumReqJuncs
        DO J=1,NJ
          IF(AJUNC(J).EQ.KAJUNC(K)) GOTO 10502
				END DO
        WRITE(*,7000) KAJUNC(K)
        STOP
				10502 PRTE(K)=ELEV(J)
			END DO
      WRITE(FJUNC,6003)TIME,(PRTE(J),J=1,NumReqJuncs)
    END IF
    IF(ReqType.GT.rtJunc)then
      DO K=1,NumReqConds
        J=IPCON(K)
        PRTQ(K)=FLOW(J)
			END DO
      WRITE(FCOND,6003)TIME,(PRTQ(J),J=1,NumReqConds)
    ENDIF
	ENDIF
	! Search for next intermediate printout
  IF(IC.LT.IRD)THEN
    DO I=1,10000
      READ(FIN,'(A)',ERR=51,END=51)DUMMY3
      IF(DUMMY3.EQ.' Cycle')THEN
        BACKSPACE FIN
				READ(FIN,'(23X,I4,6X,F6.2)')HOURS,MINUTES
        EXIT
      ENDIF
			CYCLE
			51 EXIT
		END DO
  ELSE
    CYCLE
  ENDIF
END DO

IF (XTPARSER) THEN
	WRITE(*,'(A)')
	STOP 'XTPARSER Ended normally.'
ELSE
	WRITE(*,'(A)')
	STOP 'Auto-XTPARSER Ended normally.'
ENDIF

3100  WRITE(*,'(a)')' Error in opening request files'
STOP
3101  WRITE(*,'(a)')' Error in opening junction request files'
STOP
3102  WRITE(*,'(a)')' Error in opening conduit request files'
STOP
!=======================================================================
3200 FORMAT(' ERROR, ONLY THE FIRST 500 ELEMENTS WILL BE PRINTED')
4000 FORMAT(/,2('"',A80,'"',/),'"Starting Date =", ',I6,', ', &
	'"Starting Time = ",',F8.2,/,2('"',A80,'"',/), &
	'"',A20,'", "Area = ",',F8.2,/, &
	'"Output Time Step (secs) = ", ',F10.2,/, &
	'"Number of Locations = ", ',I6)
5000 FORMAT(' RUN TIME MESSAGES FROM FILE ',A/ &
	' USING INTERFACE FILE ',A//)
5001 FORMAT(I4,A10,F10.0)
6000 FORMAT(3(I10,1X,F7.2,2X,F9.2))
6001 FORMAT(4(I10,1X,F11.2,1X))
6002 FORMAT('('/(1X,20I10))
6003 FORMAT(F16.3,',',500(F8.2,','))
6004 FORMAT('''(')
6005 FORMAT(')')
6006 FORMAT('(')
6007 FORMAT(')''')
6008 FORMAT('"JUNC", ',500(I10,','))
6009 FORMAT('"GROUND", ',500(F8.2,','))
6010 FORMAT(3(I10,1X,F7.2,A,1X,F9.2))
6011 FORMAT('"INVERT", ',500(F8.2,', '))
6012 FORMAT('"CROWN", ',500(F8.2,', '))
6013 FORMAT('"JUNC", ',500(A10,', '))
6014 FORMAT(2(A10,1X,F7.2,A1,1X,F9.2,1X),A10,1X,F7.2,A1,1X,F9.2)
6015 FORMAT(3(A10,1X,F7.2,2X,F9.2))
6016 FORMAT(4(A10,1X,F11.2,1X))
6018 FORMAT('"COND",',500(I10,','))
6023 FORMAT('"COND",',500(:,'"',A10,'",'))
! AMM 7/24/96 Added following formats for printing Conduit Info
6024 FORMAT('"LENGTH", ',500(F8.2,', '))
6025 FORMAT('"DIAMETER", ',500(F8.2,', '))
6026 FORMAT('"DN ELEV", ',500(F8.2,', '))
6027 FORMAT('"UP ELEV", ',500(F8.2,', '))
7000 FORMAT(/,' ===> ERROR! JUNCTION',A10,' NOT FOUND IN EXTRAN FILE')
7100 FORMAT(/,' ===> ERROR! CONDUIT ',A10,' NOT FOUND IN EXTRAN FILE')

END PROGRAM XPPARSE
