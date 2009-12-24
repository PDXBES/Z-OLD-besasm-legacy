      PROGRAM AXTPARSE
c routine to automatically read SWMM4 EXTRAN Generic.out output file and 
c parse for spreadsheet format
c
c function is to read number of time steps, timing parameters, and
c  the depth, conduit flow fields from summary output section.
C     6/23/95:  Add in ability to create interface file
C     7/24/96:  AMM Added XTPARSER capabilities
C                 if no AXTPARSE.INP file found, assume XTPARSER functions.
C                 if no GENERIC.OUT file found, ask for an output file.
C                 XTPARSER outputs to EX_COND.CSV and EX_JUNC.CSV.
C                 XTPARSER at this time does not print out the runtime messages. 
C    10/17/96: VCA - Increase Format to write large numbers of hours
C     8/15/97: VCA - Correct writing of XFL & XHG Files for first time step
C     5/27/99: AMM - Increased element limit (MAXD) to 4000
C     1/11/2000: AMM - Fixed problem with reading incorrect Delta
C======================================================================
      IMPLICIT NONE

      INCLUDE 'TAPESIR.INC'
      INCLUDE 'INTER.INC'
      
      CHARACTER DUMMY1*36,DUMMY3*6,SUR*1
      CHARACTER DUMMY4*14,BLANK*10,AJUNC*10,ACOND*10,DUMMY5*13
      CHARACTER ECOND*10,KCOND*10,KJUNC*10
      CHARACTER*40 SWMMFILE
      LOGICAL HAMLET,HIT,XTPARSER
      INTEGER MAXD,JSUR,ICOND,JUNC,KACAD,KJ,
     +  KC,I,J,N7,IPCON,NDT,NSTART,IPCYC,NCN,NC,
     +  NJN,NJ,LINEC,NCEXTRA,LINEJ,JN,ICN,ICYC,IRD,
     +  K,IC,NYEAR,JDAY,JULDAY,YESDAY,L,M,N,X
      REAL PRTGD,PRTQ,CROWN,BOTTOM,PRTBOT,PRTCWN,ELEV,DEPTH,FLOW,
     +  PRTE,GRND,DELT,DELTA,TIME,TIMDAY,SFLOW,A
C AMM 7/24/96 Added following array types
      REAL LENGTH,DIAMTR,DNOFF,UPOFF,PRTLEN,PRTDIA,PRTDNO,PRTUPO
C
C AMM 12/20/2000 Changed maximum array size to 50000, from 5000
      PARAMETER(MAXD=50000)
      DIMENSION JUNC(MAXD),DEPTH(MAXD),ELEV(MAXD),FLOW(MAXD),
     +  ICOND(MAXD),KJUNC(MAXD),KCOND(MAXD),IPCON(MAXD),
     +  PRTE(MAXD),PRTQ(MAXD),SUR(MAXD),JSUR(MAXD),
     +  GRND(MAXD),BOTTOM(MAXD),PRTGD(MAXD),PRTBOT(MAXD),
     +  AJUNC(MAXD),ACOND(MAXD),PRTCWN(MAXD),CROWN(MAXD),SFLOW(MAXD),
     +  LENGTH(MAXD),DIAMTR(MAXD),DNOFF(MAXD),UPOFF(MAXD),PRTLEN(MAXD),
     +  PRTDIA(MAXD),PRTDNO(MAXD),PRTUPO(MAXD)
C AMM 6/22/95
C AXTPARSE.INP Data
      CHARACTER AXTFILE*40,NMINT*40,NMROOT*40
      INTEGER NCOND,ANODE,ACONDS
      DIMENSION ANODE(MAXD),NCOND(MAXD),ACONDS(MAXD,8)
C      
      DATA BLANK/'          '/
c
C check to see if requested junction/cond data has been output from acad
C Index KACAD used to indicate which files (junc or cond data) were
c requested.  KACAD=0->no request, KACAD=1->junc only, 
c KACAD=2->cond data only, KACAD=3->both
c
      KACAD=0
      KJ=0
      KC=0
      WRITE(*,'(A)') ' ******************************'
      WRITE(*,'(A)') ' *** XTPARSER/Auto-XTPARSER ***'
      WRITE(*,'(A)') ' ******************************'
      WRITE(*,'(A/)') ' 1/11/2000 Build'
      INQUIRE(FILE='AC_JUNC.DAT',EXIST=HAMLET)
      IF(HAMLET)THEN
        OPEN(55,ERR=3100,FILE='AC_JUNC.DAT',STATUS='OLD')
        KACAD=1
        DO 1 I=1,1000
          READ(55,*,END=2,ERR=2)KJUNC(I)
          IF(KJUNC(I).EQ.'')GO TO 2
          KJ=KJ+1
1       CONTINUE

2       CONTINUE

        IF(KJ.GT.500)THEN
          WRITE(*,3200)
          KJ=500
        ENDIF
      ENDIF

      INQUIRE(FILE='AC_COND.DAT',EXIST=HAMLET)
      IF(HAMLET)THEN
        OPEN(56,ERR=3100,FILE='AC_COND.DAT',STATUS='OLD')
        KACAD=KACAD+2
        DO 3 I=1,1000
          READ(56,*,END=4,ERR=4)KCOND(I)
          If(KCOND(I).EQ.'') GO To 4
          KC = KC + 1
3       CONTINUE
4       IF(KC.GT.500)THEN
          WRITE(*,3200)
          KC=500
        ENDIF
      ENDIF
C AMM 6/22/95
C Request AXTPARSE.INP file
C AMM 7/26/96 Modified to perform like XTPARSER if AXTPARSE.INP not found.
C             Previously, AXTPARSE would ask where an AXTPARSE input file
C             was located.
      INQUIRE(FILE='AXTPARSE.INP',EXIST=HAMLET)
      IF (HAMLET) THEN
        OPEN(50,ERR=3100,FILE='AXTPARSE.INP',STATUS='OLD')
        XTPARSER = .FALSE.
        WRITE(*,'(/A/)')'  AXTPARSE.INP found, Running Auto-XTParser'
      ELSE
        WRITE(*,'(A)')'  No AXTPARSE.INP file found, Running XTParser. '
        XTPARSER = .TRUE.
        GOTO 10000
      ENDIF

C Read in AXTPARSE.INP file
      READ(50,*) NMINT,NMROOT
      READ(50,*) TITLE(1)
      READ(50,*) TITLE(2)
      READ(50,*) IDATEZ
      READ(50,*) LOCATS
      DO 8000 I = 1,LOCATS
        READ(50,*) ANODE(I),NCOND(I),(ACONDS(I,J),J=1,NCOND(I))
8000  CONTINUE
      NQUAL = 0

c here request file names and open for ASCII output
C AMM 7/24/96 Added jump to this portion if AXTPARSE.INP file not found.
10000 INQUIRE(FILE='generic.out',EXIST=HAMLET)
      IF(HAMLET)THEN
        OPEN(5,ERR=3100,FILE='generic.out',STATUS='OLD')
        SWMMFile='GENERIC.OUT'
        WRITE(*,'(/A/)')'  GENERIC.OUT found.'
      ELSE
        WRITE(*,'(/A)')'  No GENERIC.OUT file found. '
        WRITE(*,'(A\)')'  Please Enter Path & Name of Output File: '
        READ(*,'(A\)') SWMMFile
        INQUIRE(FILE=SWMMFile,EXIST=HAMLET)
        IF(HAMLET)THEN
          OPEN(5,ERR=3100,FILE=SWMMFile,STATUS='OLD')
        ELSE        
          WRITE(*,'(A\)')' ERROR! File Not Found! '
          STOP
        ENDIF
      ENDIF
C General Unit numbers: N6 = Junctions, N7 = Conduits
      N5=5
C AMM 1/10/2000 ? Someone change these? Maybe me?
C      N6=6000
C      N7=7000
      N6=60
      N7=70

C AMM 7/24/96 Added branch if XTPARSER is on; if XTPARSER is running,
C use the original EX_COND.CSV and EX_JUNC.CSV filenames.  Otherwise, use
C AXTPARSER's .XHG and .XFL extensions.
      IF (XTPARSER) THEN
        IF(KACAD.NE.2) OPEN(N6,FILE='EX_JUNC.CSV')
        IF(KACAD.NE.1) OPEN(N7,FILE='EX_COND.CSV')
      ELSE
        IF(KACAD.NE.2) OPEN(N6,FILE=NMROOT//'.XHG')
        IF(KACAD.NE.1) OPEN(N7,FILE=NMROOT//'.XFL') 
        OPEN(51,FILE=NMINT,FORM='UNFORMATTED')
      ENDIF

      DO 5 J=1,MAXD
        JSUR(J)=0
5     CONTINUE
      NJCE=1
      
      DO 8 J = 1, 4
        TITLE(J) = ' '
8     CONTINUE
C     
C     Write message to user on status of processing:
C AMM 7/24/96 Added branch to process XTPARSER stuff
      IF (XTPARSER) THEN
        WRITE(*,4130) SWMMFile
        WRITE(*,4140)
 4130   FORMAT(/,10X,' **********  EXTRAN-PARSER  **********',//,
     +   10X,' Processing Extran Output File: ',A40)
 4140   FORMAT(/,18X,'==>> Is the Current Julian Date')
      ELSE
        SWMMFile = NMROOT//'.OUT'
        WRITE(*,4110) SWMMFile
 4110   FORMAT(/,10X,' **********  AUTO-EXTRAN-PARSER  **********',//,
     +    10X,' Processing Extran Output File: ',A40)
        WRITE(*,4120)
 4120   FORMAT( /,18X,'==>> Is the Current Julian Date')
      ENDIF


c HERE READ DATA FILE TO FIND CONTROLS
C
      DO 10 I=1,2000
        READ(N5,'(A)',END=9901)DUMMY1
        IF(DUMMY1.EQ.' Integration cycles.................')THEN
          BACKSPACE 5
          READ(N5,'(36X,I8)')NDT
        ELSEIF(DUMMY1.EQ.' Length of integration step is......')THEN
          BACKSPACE 5
          READ(N5,'(36X,F8.2)')DELT
        ELSEIF(DUMMY1.EQ.' Printing starts in cycle...........')THEN
          BACKSPACE 5
          READ(N5,'(36X,I8)')NSTART
        ELSEIF(DUMMY1.EQ.' Intermediate printout intervals of.')THEN
          IF(IPCYC.LE.0) Then
            BACKSPACE 5
            READ(N5,'(36X,I8)')IPCYC
          ENDIF
        ELSEIF(DUMMY1.EQ.' Initial time.......................')THEN
          BACKSPACE 5
          READ(N5,'(36X,F8.2)') TZero
          GO TO 11
        ENDIF
10    CONTINUE
9901  WRITE(*,'(A)')' READING ERROR!  File may not produced have been ',
     +              'produced by PDXTRAN Version 4.x'
      STOP
11    CONTINUE
c-----------------------------------------------------------------------
C finished reading controls--look for warning messages and initialize

      DO 111 I=1,1000000
        READ(N5,'(A)',ERR=111) DUMMY4
  185   FORMAT(5X,A14)
C     Check for Title(1) and Title(2)
        IF(Title(1).EQ.' ') Then
          IF(DUMMY4.EQ.' WASHINGTON, D') THEN
            READ(N5,*)
            READ(N5,*)
            READ(N5,'(5X,A)') Title(1)
            READ(N5,'(5X,A)') Title(2)
          ENDIF
        ENDIF
        
        IF(DUMMY4.EQ.' Title from fi')THEN
          READ(N5,'(1X,A80)')Title(3)
          READ(N5,'(1X,A80)')Title(4)
          READ(N5,*)
          READ(N5,*)
          READ(N5,*)
          READ(N5,*)
          READ(N5,*)
          READ(N5,*)
          READ(N5,'(52X,I8)') IDateZ
          READ(N5,'(52X,F8.1)') TZero
          READ(N5,*)
          READ(N5,*)
          READ(N5,'(52X,F8.2)') TRIBA
          READ(N5,'(52X,I8)') NJCE
*          GO TO 160
        ENDIF
 
c read conduit numbers
        IF(DUMMY4.EQ.'  INP  CONDUIT')THEN
          READ(N5,'(A)')
          READ(N5,'(A)')
          READ(N5,'(I4,1X,A10)',ERR=1114)J,ACOND(J)
          GO TO 1113
1114      NJCE=1
          WRITE(*,*) ' Error in Reading Output File - NJCE set to 1'
1113      BACKSPACE N5

          DO 113 NCN=1,MAXD
105         IF(NJCE.EQ.0)THEN
              IF (XTPARSER) THEN
                READ(N5,10001,ERR=114)J,ICOND(J),LENGTH(J),DUMMY5,A,A,
     +            DIAMTR(J),A,X,X,DNOFF(J),UPOFF(J)
C                READ(N5,10001,ERR=114)J
                BACKSPACE N5
                IF (J.EQ.0) THEN
C AMM 7/31/96 Changed following statement from NC=NCN-1 because NCN was
C             found to reset itself when the GOTO 111 statement is
C             executed.
                  NC=NC+NCN-1
                  GOTO 111
               ELSE
                READ(N5,10001,ERR=114)J,ICOND(J),LENGTH(J),DUMMY5,A,A,
     +            DIAMTR(J),A,X,X,DNOFF(J),UPOFF(J)
                ENDIF
C 7/24/96 AMM added FORMAT statement for reading conduit information
10001           FORMAT(I4,I10,F10.0,A10,F10.2,F10.5,2(F10.2),2(I9),
     +            2(F8.2))
              ELSE
                READ(N5,*,ERR=114)J,ICOND(J)
              ENDIF
            ELSE
              READ(N5,15001,ERR=114)J,ACOND(J),LENGTH(J),DUMMY5,A,A,
     +          DIAMTR(J),A,X,X,DNOFF(J),UPOFF(J)
15001           FORMAT(I4,1X,A9,F10.0,A10,F10.2,F10.5,2(F10.2),2X,2(I9),
     +            2(F8.2))
            ENDIF
            GO TO 113

C     READ ERR-CHK FOR MORE CONDUITS ON NEXT PAGE AND GET WARNINGS
114         BACKSPACE 5
            DO 104 J=1,1000
              READ(N5,'(A)')DUMMY4
              IF(DUMMY4.EQ.'  INP  CONDUIT')THEN
                READ(N5,'(A)')
                READ(N5,'(A)')
                GO TO 105
              ENDIF

              IF(DUMMY4.EQ.' *  Equivalent')THEN
                BACKSPACE 5
                BACKSPACE 5
                BACKSPACE 5
                NC=NCN-1
                GO TO 111
              ENDIF

              IF(DUMMY4.EQ.' INP  JUNCTION')THEN
                BACKSPACE 5
                NC=NCN-1
                GO TO 111
              ENDIF

104         CONTINUE
113       CONTINUE
        ENDIF

c read junction data
        IF(DUMMY4.EQ.' INP  JUNCTION')THEN
          READ(N5,'(A)')
          READ(N5,'(A)')

1061      DO 115 NJN=1,MAXD
106         IF(NJCE.EQ.0)THEN
              READ(N5,*,ERR=116)J,JUNC(J),GRND(J),CROWN(J),BOTTOM(J)
            ELSE
              READ(N5,'(I4,1X,A10,F9.2,F10.2,F10.2)',ERR=116)
     +					J,AJUNC(J),GRND(J),CROWN(J),BOTTOM(J)
            ENDIF 
            GO TO 115
C READ ERR-CHECK FOR MORE JUNCTIONS ON NEXT PAGE OR CONDUIT WARNING MSG
116         BACKSPACE 5
            DO 1116 J=1,1000
              READ(N5,'(A)')DUMMY4
              IF(DUMMY4.EQ.' INP  JUNCTION')THEN
                READ(N5,'(A)')
                READ(N5,'(A)')
                GO TO 106
              ELSEIF(DUMMY4.EQ.'      lie abov')THEN
                GO TO 106
              ENDIF
          
              IF(DUMMY4.EQ.' STORAGE JUNCT' .OR.
     +          DUMMY4.EQ.' *        DIUR' .OR.
     +          DUMMY4.EQ.' # Header info' .OR.
     +          DUMMY4.EQ.' *        INTE'     )THEN
                NJ=NJN-1
                LINEJ=FLOAT(NJ)/3.+.7
                BACKSPACE 5
                GO TO 111
              ENDIF
1116        CONTINUE
115       CONTINUE
        ENDIF

C Find extra conduits created internally
        IF(DUMMY4.EQ.' *        INTE')THEN
          DO 117 J=1,4
117         READ(N5,*)
          DO 118 J=1,1000
            IF(NJCE.EQ.0)THEN
              READ(N5,'(1X,I14)')NCEXTRA
              IF(NCEXTRA.EQ.0)THEN
                GO TO 119
              ELSE
                NC=NC+1
                ICOND(NC)=NCEXTRA
              ENDIF
            ELSE
              READ(N5,'(5X,A)')ECOND
              IF(ECOND.EQ.BLANK)THEN
                GO TO 119
              ELSE
                NC=NC+1
                ACOND(NC)=ECOND
              ENDIF
            ENDIF
118       CONTINUE 
119       LINEC=FLOAT(NC)/4.+0.8
        ENDIF
                                                                            
C Read initial date and time for solution
        IF(DUMMY4.EQ.' Initial Julia')THEN
          BACKSPACE 5
          READ(N5,'(51X,I8)') IDATEZ
          READ(N5,'(51X,F8.0)')TZERO
          TZERO=TZERO/3600.
        ENDIF

c Read initial junction conditions
        IF(DUMMY4.EQ.' JUNCTION / DE')THEN
          IF(NJCE.EQ.0)THEN
            DO 150 JN=1,LINEJ,3
              READ(5,6000,ERR=151)JUNC(JN),DEPTH(JN),ELEV(JN),
     +          JUNC(JN+1),DEPTH(JN+1),ELEV(JN+1),
     +          JUNC(JN+2),DEPTH(JN+2),ELEV(JN+2)
150         CONTINUE
          ELSE
            DO 152 JN=1,LINEJ,3
              READ(5,6015,ERR=151)AJUNC(JN),DEPTH(JN),ELEV(JN),
     +          AJUNC(JN+1),DEPTH(JN+1),ELEV(JN+1),
     +          AJUNC(JN+2),DEPTH(JN+2),ELEV(JN+2)
              IF(AJUNC(JN).EQ.BLANK)GO TO 151  
152         CONTINUE
          ENDIF
151         CONTINUE
          BACKSPACE 5
        ENDIF

c Read initial conduit conditions

        IF(DUMMY4.EQ.'   CONDUIT/   ')THEN
          IF(NJCE.EQ.0)THEN
            DO 155 ICN=1,LINEC,4
              READ(5,6001,ERR=156)ICOND(ICN),FLOW(ICN),ICOND(ICN+1),
     +          FLOW(ICN+1),ICOND(ICN+2),FLOW(ICN+2),
     +          ICOND(ICN+3),FLOW(ICN+3)
155         CONTINUE
156         CONTINUE
          ELSE
            DO 157 ICN=1,LINEC,4
              READ(5,6101,ERR=158)ACOND(ICN),FLOW(ICN),ACOND(ICN+1),
     +          FLOW(ICN+1),ACOND(ICN+2),FLOW(ICN+2),
     +          ACOND(ICN+3),FLOW(ICN+3)
157         CONTINUE
158         CONTINUE
          ENDIF
*        Finished Reading all that we can now:
          GOTO 160
        ENDIF
111   CONTINUE
160   CONTINUE

C       Now assign Delta Time Step and default Source
      SOURCE = 'PDXTran-AXTParser'
C AMM 1/10/2000 Corrected calculation of Delta
C      DELTA = 60.0 * FLOAT(INT(DELT * IPCYC + 5)/60)
	DELTA = DELT * IPCYC

      NYEAR = IDATEZ / 1000
      JDAY = IDATEZ - NYEAR*1000
       
C AMM 6/22/95
C Reprocess the node/conduit list from AXTPARSE.INP so that the conduit
C  list uses the array indexes in ICOND().
      DO 8100 I=1,LOCATS
        DO 8105 J=1,NCOND(I)
          HIT = .FALSE.
          K = 1
          IF (NJCE.EQ.1) THEN
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
8105    CONTINUE
8100  CONTINUE
8600  FORMAT(/,' ===> ERROR! CONDUIT',I10,' CONNECTING TO ',I10,
     +' NOT FOUND IN EXTRAN FILE')
C AMM 6/22/95
C Write out interface header
      IF (.NOT. XTPARSER) THEN
        WRITE(51) TITLE(1)
        WRITE(51) TITLE(2)
        WRITE(51) IDATEZ, TZERO
        WRITE(51) TITLE(3)
        WRITE(51) TITLE(4)
        WRITE(51) SOURCE, LOCATS, NQUAL, TRIBA, NJCE
        IF(NJCE.LE.0) WRITE(51) (ANODE(K), K=1, LOCATS)
        IF(NJCE.GE.1) WRITE(51) (KAN(I),I=1,LOCATS)
        IF(NQUAL.GT.0) WRITE(51)  (PNAME(J),J=1,NQUAL)
        IF(NQUAL.GT.0) WRITE(51)  (PUNIT(J),J=1,NQUAL)
        IF(NQUAL.GT.0) WRITE(51)  (NDIM(J),J=1,NQUAL)
        QCONV = 1.0
        WRITE(51) QCONV
      ENDIF

C
C NOW WRITE THE TOP OF OUTPUT FILE AND INITIAL CONDITION

C       Test if user wants all or a select few.  Write full inteface
C       data/header only for KACAD = 0
C AMM 7/30/96 Changed this so that full interface data/header will
C             always print when XTPARSER function is used.
      IF(KACAD.EQ.0)THEN
        IF (XTPARSER) THEN
          WRITE(N6,'(A)')'"Junction WSElev Data From XTParser"'
          WRITE(N6,10005)SWMMFile
          WRITE(N7,'(A)')'"Conduit Flow Data From XTParser"'
          WRITE(N7,10005)SWMMFile
10005     FORMAT('"',A,'"')
        ELSE
          WRITE(N6,'(A)')'"Junction WSElev Data From Auto-XTParser"'
          WRITE(N7,'(A)')'"Conduit Flow Data From Auto-XTParser"'
        ENDIF
            
        WRITE(N6,4000) Title(1),Title(2),IDateZ,TZero,
     +    Title(3),Title(4),Source,TRIBA,Delta,NJ
        WRITE(N7,4000) Title(1),Title(2),IDateZ,TZero,
     +    Title(3),Title(4),Source,TRIBA,Delta,NC
     
        IF(NJCE.EQ.0)THEN
          WRITE(N6,6008)(JUNC(J),J=1,NJ)
          WRITE(N7,6018)(ICOND(J),J=1,NC)
        ELSE
          WRITE(N6,6013)(AJUNC(J),J=1,NJ)
          WRITE(N7,6023)(ACOND(J),J=1,NC)
        ENDIF

        WRITE(N6,6009)(GRND(J),J=1,NJ)
        WRITE(N6,6012)(CROWN(J),J=1,NJ)
        WRITE(N6,6011)(BOTTOM(J),J=1,NJ)
        WRITE(N6,'(/)')
        IF (XTPARSER) THEN
          WRITE(N7, 6024)(LENGTH(J),J=1,NC)
          WRITE(N7, 6025)(DIAMTR(J),J=1,NC)
          WRITE(N7, 6026)(DNOFF(J),J=1,NC)
          WRITE(N7, 6027)(UPOFF(J),J=1,NC)
        ENDIF
        WRITE(N7,'(/)')
      
        IF(NSTART.LE.1)THEN
          WRITE(N6,6003)TZERO/3600.,(BOTTOM(J),J=1,NJ)
          WRITE(N7,6003)TZERO/3600.,(FLOW(J),J=1,NC)
        ENDIF

      ELSE
        IF(KACAD.EQ.1 .OR. KACAD.EQ.3)THEN
          IF (XTPARSER) THEN
            WRITE(N6,'(A)')'"Junction WSElev Data From XTParser"'
            WRITE(N6,10005)SWMMFile
            WRITE(N7,'(A)')'"Conduit Flow Data From XTParser"'
            WRITE(N7,10005)SWMMFile
            WRITE(N6,4000) Title(1),Title(2),IDateZ,TZero,
     +        Title(3),Title(4),Source,TRIBA,Delta,KJ
            WRITE(N7,4000) Title(1),Title(2),IDateZ,TZero,
     +        Title(3),Title(4),Source,TRIBA,Delta,KC
          ENDIF
            
          IF (.NOT.XTPARSER) THEN
            WRITE(N6,'(A,I10/)')' "STARTING DATE =", ',IDATEZ
          ENDIF
          WRITE(N6,6008)(KJUNC(J),J=1,KJ)
C
          DO 450 K=1,KJ
            DO 448 J=1,NJ
              IF(AJUNC(J).EQ.KJUNC(K))GO TO 449
448         CONTINUE
            WRITE(*,7000)KJUNC(K)
            STOP
449         PRTGD(K)=GRND(J)
            PRTCWN(K)=CROWN(J)
            PRTBOT(K)=BOTTOM(J)
450       CONTINUE
          WRITE(N6,6009)(PRTGD(J),J=1,KJ)
          WRITE(N6,6012)(PRTCWN(J),J=1,KJ)
          WRITE(N6,6011)(PRTBOT(J),J=1,KJ)
          WRITE(N6,'(/)')
        ENDIF

        IF(KACAD.GT.1)THEN
          IF (.NOT.XTPARSER) THEN
            WRITE(N7,'(A,I10/)')' "STARTING DATE =", ',IDATEZ
          ENDIF
          WRITE(N7,6018)(KCOND(J),J=1,KC)
          DO 500 K=1,KC
            DO 498 J=1,MAXD
              IF(ACOND(J).EQ.KCOND(K))GO TO 499
498         CONTINUE
            WRITE(*,7100)KCOND(K)
            STOP
499         IPCON(K)=J
            PRTLEN(K)=LENGTH(J)
            PRTDIA(K)=DIAMTR(J)
            PRTDNO(K)=DNOFF(J)
            PRTUPO(K)=UPOFF(J)
500       CONTINUE
          WRITE(N7,6024)(PRTLEN(J),J=1,KC)
          WRITE(N7,6025)(PRTDIA(J),J=1,KC)
          WRITE(N7,6026)(PRTDNO(J),J=1,KC)
          WRITE(N7,6027)(PRTUPO(J),J=1,KC)
          WRITE(N7,'(/)')
        ENDIF
      ENDIF
          
C ' CUMULATIVE OVERFLOW VOLUME FROM NODE'
C NOW FIND THE FIRST CYCLE OF OUTPUT

      DO 30 I=1,1000000 
C BEGIN DO LOOP FOR EACH TIME STEP/PRINT INTERVAL
        READ(N5,'(A)')DUMMY3
        IF(DUMMY3.EQ.' CYCLE')GO TO 35
30    CONTINUE
C NOW WE HAVE FOUND THE FIRST CYCLE TIME OUTPUT
C
35    BACKSPACE 5
      READ(N5,'(6X,I11)')ICYC
C ICYC is the number of time steps skipped
      IRD=(NDT-NSTART)/IPCYC
      DO 50 IC=1,IRD+1
        TIME=(ICYC*DELT+TZERO)/3600.
C         Calculate Julian Day for Interface File
        JDAY = (IDATEZ - NYEAR*1000) + INT((TIME+.01)/24.)
        IF(JDAY.GT.365) THEN
          JDAY = JDAY - 365
          NYEAR = (IDATEZ/1000) + INT(JDAY/365.)
        ENDIF
        JULDAY = NYEAR*1000 + JDAY
          
C         Calculate TIMDAY for Interface file - round to nearest Delta
        TIMDAY = TIME - 24.*Int((TIME+.01)/24.)
        If(TIMDAY.LT.0.00) TIMDAY = 0.00
        TIMDAY = Delta * Float(Int((TIMDAY * 3600.+5.)/Delta))
        
        DO 36 K=1,1000
          READ(N5,'(A)',ERR=51,END=51)DUMMY3
          IF(DUMMY3.EQ.' JUNCT')GOTO 37
36      CONTINUE

37      DO 38 JN=1,LINEJ*3,3
          IF(NJCE.EQ.0)THEN
            READ(5,6010,ERR=40)JUNC(JN),DEPTH(JN),SUR(JN),ELEV(JN),
     +        JUNC(JN+1),DEPTH(JN+1),SUR(JN+1),ELEV(JN+1),
     +        JUNC(JN+2),DEPTH(JN+2),SUR(JN+2),ELEV(JN+2)
          ELSE
            READ(5,6015,ERR=40)AJUNC(JN),DEPTH(JN),SUR(JN),ELEV(JN),
     +        AJUNC(JN+1),DEPTH(JN+1),SUR(JN+1),ELEV(JN+1),
     +        AJUNC(JN+2),DEPTH(JN+2),SUR(JN+2),ELEV(JN+2)
          ENDIF
          DO 371 J=JN,JN+2
            IF(SUR(J).NE.' ' .OR. ELEV(J).GT.CROWN(J))THEN
              IF(ELEV(J)-GRND(J) .LE. 1.E-06)JSUR(J)=1
            ENDIF
371       CONTINUE
38      CONTINUE
40      CONTINUE

        BACKSPACE 5
        DO 400 K=1,1000
          READ(N5,'(A)')DUMMY3
          IF(DUMMY3.EQ.'   CON')GOTO 401
400     CONTINUE
401     CONTINUE

        DO 41 ICN=1,LINEC*4,4
          IF(NJCE.EQ.0)THEN
            READ(5,6001,ERR=42)ICOND(ICN),FLOW(ICN),ICOND(ICN+1),
     +        FLOW(ICN+1),ICOND(ICN+2),FLOW(ICN+2),
     +        ICOND(ICN+3),FLOW(ICN+3)
          ELSE
            READ(5,6016,ERR=42)ACOND(ICN),FLOW(ICN),ACOND(ICN+1),
     +        FLOW(ICN+1),ACOND(ICN+2),FLOW(ICN+2),
     +        ACOND(ICN+3),FLOW(ICN+3)
          ENDIF
41      CONTINUE
42      CONTINUE
C
C
*        Write message to screen to tell user the current date examined:
*                   
C AMM 7/24/96 line 4130 renumbered to 4150 to avoid conflict with new
*             line 4130 above.
        IF(JULDAY.NE.YESDAY) THEN
          WRITE(*,4150) JulDay
          YESDAY = JulDay
        ENDIF
 4150   FORMAT('+',10X,I6)
           
C         WRITE DATA TO FILES:
C
        IF(KACAD.EQ.0)THEN
          WRITE(N6,6003)TIME,(ELEV(J),J=1,NJ)
          WRITE(N7,6003)TIME,(FLOW(J),J=1,NC)
C AMM 6/22/95
C Write out time step to interface file
          DO 8300 L = 1, LOCATS
            SFLOW(L) = 0
            DO 8305 M=1, NCOND(L)
              SFLOW(L) = SFLOW(L)+FLOW((ACONDS(L,M)))*QCONV
8305        CONTINUE
8300      CONTINUE          

          IF (.NOT. XTPARSER) THEN
            WRITE(51) julday, TIMDAY, delta, (SFLOW(N),N=1,LOCATS)
          ENDIF
      
        ELSE
          IF(KACAD.EQ.1 .OR. KACAD.EQ.3)THEN
            DO 600 K=1,KJ
              DO 10500 J=1,NJ
                IF(AJUNC(J).EQ.KJUNC(K)) GOTO 10502
10500         CONTINUE
              WRITE(*,7000) KJUNC(K)
              STOP
10502         PRTE(K)=ELEV(J)
600         CONTINUE
            WRITE(N6,6003)TIME,(PRTE(J),J=1,KJ)
          ENDIF
C
          IF(KACAD.GT.1)then
            DO 601 K=1,KC
              J=IPCON(K)
              PRTQ(K)=FLOW(J)
601         CONTINUE
            WRITE(N7,6003)TIME,(PRTQ(J),J=1,KC)
          ENDIF
        ENDIF
C
        IF(IC.LT.IRD)THEN
          DO 46 I=1,10000
            READ(N5,'(A)',ERR=51,END=51)DUMMY3
            IF(DUMMY3.EQ.' CYCLE')THEN
              BACKSPACE 5
              READ(N5,'(6X,I11)')ICYC
              GO TO 50
            ENDIF
46        CONTINUE
        ELSE
          GO TO 51
        ENDIF
C
50    CONTINUE
51    CONTINUE
C
88    CONTINUE

100   IF (XTPARSER) THEN
        WRITE(*,'(A)')
        STOP 'XTPARSER Ended normally.'
      ELSE
        WRITE(*,'(A)')
        STOP 'Auto-XTPARSER Ended normally.'
      ENDIF
c
3100  WRITE(*,'(a)')' Error in opening request files'
      STOP
C=======================================================================
3200  FORMAT(' ERROR, ONLY THE FIRST 500 ELEMENTS WILL BE PRINTED')
4000  FORMAT(/,2('"',A80,'"',/),'"Starting Date =", ',I6,', ',
     +      '"Starting Time = ",',F8.2,/,2('"',A80,'"',/),
     +      '"',A20,'", "Area = ",',F8.2,/,
     +      '"Output Time Step (secs) = ", ',F10.2,/,
     +      '"Number of Locations = ", ',I6)
5000  FORMAT(' RUN TIME MESSAGES FROM FILE ',A/
     +' USING INTERFACE FILE ',A//)
5001  FORMAT(I4,1X,A10,F10.0)
6000  FORMAT(3(I10,1X,F7.2,2X,F9.2))
6001  FORMAT(4(I10,1X,F11.2,1X))
6101  FORMAT(4(1X,A9,1X,F11.2,1X))
6002  FORMAT('('/(1X,20I10))
6003  FORMAT(F16.3,',',30000(F8.2,','))
6004  FORMAT('''(')
6005  FORMAT(')')
6006  FORMAT('(')
6007  FORMAT(')''')
6008  FORMAT('"JUNC", ',30000(A10,','))
6009  FORMAT('"GROUND", ',30000(F8.2,','))
6010  FORMAT(3(I10,1X,F7.2,A,1X,F9.2))
6011  FORMAT('"INVERT", ',30000(F8.2,', '))
6012  FORMAT('"CROWN", ',30000(F8.2,', '))
6013  FORMAT('"JUNC", ',30000(A8,', '))
6014  FORMAT(3(A10,1X,F7.2,A,1X,F9.2))
6015  FORMAT(3(1X,A9,1X,F7.2,A1,1X,F9.2))
6016  FORMAT(4(1X,A9,1X,F11.2,1X))
6018  FORMAT('"COND", ',500(A10,','))
6023  FORMAT('"COND", ',500(A8,','))
C AMM 7/24/96 Added following formats for printing Conduit Info
6024  FORMAT('"LENGTH", ',500(F8.2,', '))
6025  FORMAT('"DIAMETER", ',500(F8.2,', '))
6026  FORMAT('"DN OFFSET", ',500(F8.2,', '))
6027  FORMAT('"UP OFFSET", ',500(F8.2,', '))
7000  FORMAT(/,' ===> ERROR! JUNCTION',I10,' NOT FOUND IN EXTRAN FILE')
7100  FORMAT(/,' ===> ERROR! CONDUIT ',I10,' NOT FOUND IN EXTRAN FILE')
C=======================================================================
      END
