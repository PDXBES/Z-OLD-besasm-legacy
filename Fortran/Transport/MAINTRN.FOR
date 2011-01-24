C#######################################################################
C            EPA Storm Water Management Model Main Program
C
C     NOTE THIS VERSION IS SPECFIC TO PDX VERSION OF THE TRANSPORT BLOCK
C
C     This is the executive block and includes graph routines.
C#######################################################################
C     This program reads the off-line file assignments, and then reads
C     the alphanumeric codes for the block to be called.  Each major
C     SUBROUTINE returns here for new block assignments.  Last updated
C     December, 1990 by the University of Florida(RED).
C#######################################################################
      PROGRAM SWMM4
      INCLUDE 'TAPES.INC'
      CHARACTER CNAME*4,FNAMI*60,FNAMO*60,FINTER*60,BMJ*1,MNAME(12)*10
      DIMENSION I_VALUES(8)
      LOGICAL HAMLET
      CHARACTER*11 SYSDAT, SYSTIM
      CHARACTER*5 ZONEDAT
************************
*CNBC - VCA 7/17/92
*		Remove statements for BMJ since it is never used in MAIN.FOR
*
*     Remove statements for MNAME due to using Lahey Fortran and
*     not going into determining the month from a character variable
*
*      CHARACTER CNAME*4,FNAMI*60,FNAMO*60,FINTER*60,BMJ*1,MNAME(12)*10
*      DATA BMJ/' '/
*      DATA MNAME/'January   ','February  ','March     ','April     ',
*     +           'May       ','June      ','July      ','August    ',
*     +           'September ','October   ','November  ','December  '/
*
       CHARACTER*11 bdate, edate
************************
*
C=======================================================================
C                 N5 = Data input,  (Usually = 5).
C                 N6 = Data output, (Usually = 6).
C     Parameters N5 and N6 are used in read\write statemtnts in
C     all the blocks of SWMM.  This is the location to change unit
C     number assignments if necessary.
C=======================================================================
*
*CNBC - VA 7/17/92  ***********************
*	Change the Time and Date code for Lahey Fortran:
*
**C     Read initial time and date using GETTIM and GETDAT
**C     Subroutines that are RM-FORTRAN specific.
**C=======================================================================
**      CALL GETTIM(M11,M12,M13,M14)
**      CALL GETDAT(N11,N12,N13)
**C=========
*	Timer(iticks) determines the number of ticks or hundreths of seconds
*	since midnight.
       write(*,*)' calling TIMER '
      CALL DATE_AND_TIME(bdate,SYSTIM,ZONEDAT,I_VALUES)
CC       CALL TIMER(nbticks)
       write(*,*)' after timer '
*                       Calculate beginning time:
      N11 = I_VALUES(1)
      N12 = I_VALUES(2)
      N13 = I_VALUES(3)
      nbhr = I_VALUES(5)
      nbmin = I_VALUES(6)
      nbsec = I_VALUES(7)
      M14 = I_VALUES(8)
C
cc         nbhr  = nbticks/100/3600
cc         nbmin = (float(nbticks)/100./3600. - nbhr)*60
cc         nbsec = (float(nbticks)/100./60. - nbhr*60 - nbmin)*60
*
*	Date(result) determines the date as a character*8 variable: MM/DD/YY
*
cc      write(*,*)' calling date '
cc      CALL DATE(bdate)
cc      write(*,*)' after date '
*
*CNBC  ****************************************
*
C=======================================================================
C     Read input and output file names from screen.
C=======================================================================
      WRITE(*,15)
      N55   = 55
      JCE   = 0
      NOQ   = 0
C=======================================================================
C     IF FILE 'GENERIC.INP' WAS NOT DEFINED ON THE COMMAND LINE 
C        PROMPT THE USER FOR INPUT AND OUTPUT FILE NAMES
C=======================================================================
      INQUIRE(FILE='GENERIC.INP',EXIST=HAMLET)
      IF(.NOT.HAMLET) GO TO 3100
      write(*,*)' generic.inp exists '
      OPEN(N55,ERR=3100,FILE='GENERIC.INP',STATUS='OLD')
      OPEN(N5,STATUS='SCRATCH')
      FNAMI = 'GENERIC.INP'
      FNAMO = 'GENERIC.OUT'
      OPEN(N6,FILE='GENERIC.OUT',STATUS='UNKNOWN')
      GO TO 3200
3100  WRITE(*,1)
      READ(*,2) FNAMI
      OPEN(N55,ERR=3000,FILE=FNAMI,STATUS='OLD')
      OPEN(N5,STATUS='SCRATCH')
      WRITE(*,3)
      READ(*,2) FNAMO
      OPEN(N6,FILE=FNAMO,STATUS='UNKNOWN')
C=======================================================================
C     Strip comment lines and make temporary file containing the data.
C=======================================================================
 3200 continue
      write(*,*)' calling strigp '
      CALL STRIP(N55)
cc 3200 CALL STRIP(N55)
      write(*,*)' after strip '
      READ(N5,*,ERR=888) CC,NBLOCK,(JIN(J),JOUT(J),J=1,NBLOCK)
      READ(N5,*,ERR=888) CC,NITCH,(NSCRAT(I),I=1,NITCH)
      write(*,*)' after first read '
      WRITE(N6,10)
      write(n6,11)
      WRITE(N6,15)
      DO 80 I = 1,NBLOCK
      IF(JIN(I).EQ.5.OR.JIN(I).EQ.6.OR.JIN(I).EQ.55) CALL ERROR('117')
      IF(JOUT(I).EQ.5.OR.JOUT(I).EQ.6.OR.JOUT(I).EQ.55)
     +  CALL ERROR('117')
   80 CONTINUE
      DO 90 I = 1,NITCH
      IF(NSCRAT(I).EQ.5.OR.NSCRAT(I).EQ.6.OR.NSCRAT(I).EQ.55) 
     +                                         CALL ERROR('117')
   90 CONTINUE
C=======================================================================
C     @ - READ THE NAMES OF PERMANENTLY SAVED 
C         INTERFACE OR SCRATCH FILES
C     $ - READ THE NAME OF THE NEXT PROGRAM BLOCK
C=======================================================================
 100  READ(N5,*,ERR=888) CC
      IF(CC.EQ.'$') THEN
                    BACKSPACE N5
                    READ(N5,110,ERR=888) CNAME
                    ELSE IF(CC.EQ.'@') THEN
                              BACKSPACE N5
                              READ(N5,*,ERR=888) CC,IFF,FINTER
                              IF(IFF.EQ.5.OR.IFF.EQ.6.OR.
     +                                   IFF.EQ.55)CALL ERROR('117')
                              DO 50 J = 1,NBLOCK
                              IF(JIN(J).EQ.IFF)  THEN
                                                 FFNAME(J) = FINTER
                                                 ENDIF                       
                              IF(JOUT(J).EQ.IFF) THEN
                                                 FFNAME(J+25) = FINTER
                                                 ENDIF
  50                          CONTINUE  
                              DO 75 J    = 1,NITCH
                              IF(NSCRAT(J).EQ.IFF) THEN
                                                   JKP(J+50)    = 1
                                                   FFNAME(J+50) = FINTER
                                                   GO TO 76
                                                   ENDIF
  75                          CONTINUE                                         
  76                          CONTINUE                                         
                    GO TO 100
                    ENDIF
C=======================================================================
C     Find similar named interface and scratch files.
C=======================================================================
      DO 34 J = 1,NBLOCK
      DO 21 K = J,NBLOCK
      IF(JIN(K).EQ.JOUT(J)) THEN
                            FFNAME(K) = FFNAME(25+J)
                            JKP(K)    = 1
                            JKP(25+J) = 1
                            ENDIF
   21 CONTINUE
      DO 25 K = 1,NITCH
      IF(NSCRAT(K).EQ.JIN(J))  THEN
                               FFNAME(50+K) = FFNAME(J)
                               JKP(50+K)    = 2
                               JKP(J)       = 1
                               ENDIF
   25 CONTINUE
      DO 27 K = 1,NITCH
      IF(NSCRAT(K).EQ.JOUT(J)) THEN
                               FFNAME(50+K) = FFNAME(25+J)
                               JKP(50+K)    = 2
                               JKP(25+J)    = 1
                               ENDIF
   27 CONTINUE
      DO 32 K = J,NBLOCK
      IF(JIN(K).EQ.JIN(J)) THEN
                           FFNAME(K) = FFNAME(J)
                           JKP(J)    = 1
                           JKP(K)    = 1
                           ENDIF
   32 CONTINUE
   34 CONTINUE
C=======================================================================
C     Write the names of the JIN and JOUT files.
C=======================================================================
      WRITE(N6,45) 
      DO 450       J = 1,NBLOCK
      WRITE(N6,55) J,JIN(J),FFNAME(J)
      WRITE(N6,56) J,JOUT(J),FFNAME(25+J)
  450 CONTINUE
C=======================================================================
C     Write the names of the SCRATCH files.
C=======================================================================
      WRITE(N6,46) 
      DO 460       J = 1,NITCH
      WRITE(N6,57) J,NSCRAT(J),FFNAME(50+J)
  460 CONTINUE
C=======================================================================
CPDX      WRITE(N6,300) NW,NG,NGW,NRQ,NLU,NET,NTSE,NTH,NIE,NEE,NEP,NEO,
      WRITE(N6,300) NET-1,NTSE,NTH,ntpl,NIE
C=======================================================================
C     Call the individual SWMM 4.05 Blocks.
C=======================================================================
 2000 CONTINUE
C=======================================================================
C     Close the last JIN, JOUT and NSCRAT files.
C=======================================================================
      IFIND = 0
      IF(INCNT.GT.0.AND.JIN(INCNT).GT.0.AND.JKP(INCNT).NE.1) 
     +                                     CLOSE(JIN(INCNT))
      IF(INCNT.GT.0.AND.JOUT(IOUTCT).GT.0.AND.JKP(25+IOUTCT).NE.1) 
     +                                         CLOSE(JOUT(IOUTCT))
      IF(INCNT.GT.0.AND.NSCRAT(1).GT.0.AND.JKP(51).NE.2)CLOSE(NSCRAT(1))
      IF(INCNT.GT.0.AND.NSCRAT(2).GT.0.AND.JKP(52).NE.2)CLOSE(NSCRAT(2))
      IF(INCNT.GT.0.AND.NSCRAT(3).GT.0.AND.JKP(53).NE.2)CLOSE(NSCRAT(3))
      IF(INCNT.GT.0.AND.NSCRAT(4).GT.0.AND.JKP(54).NE.2)CLOSE(NSCRAT(4))
      IF(INCNT.GT.0.AND.NSCRAT(5).GT.0.AND.JKP(55).NE.2)CLOSE(NSCRAT(5))
      IF(INCNT.GT.0.AND.NSCRAT(6).GT.0.AND.JKP(56).NE.2)CLOSE(NSCRAT(6))
      IF(INCNT.GT.0.AND.NSCRAT(7).GT.0.AND.JKP(57).NE.2)CLOSE(NSCRAT(7))

      IF(CNAME.EQ.'ANUM') THEN
                          IFIND = 1
                          JCE   = 1
                          ENDIF
      IF(CNAME.EQ.'NOQU') THEN
                          IFIND = 1
                          NOQ   = 1
                          ENDIF
      IF(CNAME.EQ.'ENDP') THEN
                          IFIND = 1
                          ENDIF
      IF(CNAME.EQ.'GRAP') THEN
CC                          CALL GRAPH
                          IFIND = 2
                          ENDIF
      IF(CNAME.EQ.'COMB') THEN
CC                          CALL COMBIN
                          IFIND = 2
                          ENDIF
      IF(CNAME.EQ.'RAIN') THEN
CC                          CALL RAIN
                          IFIND = 2
                          ENDIF
      IF(CNAME.EQ.'TEMP') THEN
CC                          CALL TEMP 
                          IFIND = 2
                          ENDIF
      IF(CNAME.EQ.'RUNO') THEN
CC                          CALL RUNOFF
                          IFIND = 2
                          ENDIF
      IF(CNAME.EQ.'TRAN') THEN
                          CALL TRANS
                          IFIND = 1
                          ENDIF
      IF(CNAME.EQ.'STOR') THEN
CC                          CALL STRT
                          IFIND = 2
                          ENDIF
      IF(CNAME.EQ.'STAT') THEN
CC                          CALL STATS
                          IFIND = 2
                          ENDIF
      IF(CNAME.EQ.'EXTR') THEN
CC                          CALL EXTRAN
                          IFIND = 2
                          ENDIF
CPDX--------------------------------------------------------------------
      IF(IFIND.EQ.2)THEN
                    WRITE(*,1851)
                    WRITE(N6,1851)
                    ENDIF
CPDX--------------------------------------------------------------------
      IF(CNAME.EQ.'ENDP'.OR.IFIND.EQ.0) THEN
                          IF(IFIND.EQ.0) THEN
                                         WRITE(*,230)  CNAME
                                         WRITE(N6,230) CNAME
                                         ELSE
                                         WRITE(*,185)
                                         WRITE(N6,185)
                                         ENDIF
                          IF(FNAMI.NE.'GENERIC.INP') THEN
                             WRITE(*,180)  FNAMI,FNAMO
                             WRITE(N6,180) FNAMI,FNAMO
                             ENDIF
**C=======================================================================
*
*
*CNBC - VA 7/17/92  ***********************
*	Change the Time and Date code for Lahey Fortran:
*
**C     Read last time and date using GETTIM and GETDAT
**C     Subroutines that are RM-FORTRAN specific.
**C=======================================================================
**                          CALL GETTIM(M21,M22,M23,M24)
**                          CALL GETDAT(N21,N22,N23)

**                          IF(N23.NE.N13) M22 = M22 + 24
**                          ELAPSE = FLOAT(M21)*60.0 + FLOAT(M22) +
**     +                             FLOAT(M23)/60.0
**                          ELAPSE = ELAPSE     - FLOAT(M11)*60.0 -
**     +                             FLOAT(M12) - FLOAT(M13)/60.0
**                          WRITE(N6,310) MNAME(N12),N13,N11,M11,M12,
**     +                                  M13,M14,MNAME(N22),N23,N21,
**     +                                  M21,M22,M23,M24,ELAPSE

**C=========
*	Timer(iticks) determines the number of ticks or hundreths of seconds
*	since midnight.
cc       CALL TIMER(neticks)
*			Calculate the ending time:
      CALL DATE_AND_TIME(edate,SYSTIM,ZONEDAT,I_VALUES)

      N11 = I_VALUES(1)
      N12 = I_VALUES(2)
      N13 = I_VALUES(3)
      nehr = I_VALUES(5)
      nemin = I_VALUES(6)
      nesec = I_VALUES(7)
      M14 = I_VALUES(8)
C
cc         nehr  = neticks/100/3600
cc         nemin = (float(neticks)/100./3600. - nehr)*60
cc         nesec = (float(neticks)/100./60. - nehr*60 - nemin)*60
*
*	Date(result) determines the date as a character*8 variable: MM/DD/YY
*
cc       CALL DATE(edate)
*
*
*	Calculate the elasped in minutes time for the simulation length:
*
       ELAPSE = (nehr-nbhr)*60. + (nemin-nbmin) + (nesec-nbsec)/60.
*
*	Check if date has changed.  If so, add 24 hours to ELAPSE (which
*	assumes the user is only executing the model for 24 hours or less.
*
       IF(bdate.NE.edate) ELAPSE = ELAPSE + 24*60.
*
*	Write the beginning and ending date and time and the
*	elasped time of the simulation.
*
                                        WRITE(N6,310) bdate, nbhr,
     &                                  nbmin, nbsec, edate, nehr,
     &                                  nemin, nesec, ELAPSE
*
*CNBC  ****************************************
*
C=======================================================================
                          STOP
                          ENDIF
C=======================================================================
C     Read the new block name.
C=======================================================================
      READ(N5,110,ERR=888,END=888) CNAME
      GO TO 2000       
3000  WRITE(*,3001)
3001  FORMAT(' ===> File error !!! - Missing input file.',/)
      GO TO 3100
 888  CALL IERROR
      STOP
C=======================================================================
    1 FORMAT(' Enter your input file name  -  ')
    2 FORMAT(A)
    3 FORMAT(' Enter your output file name -  ')
   10 FORMAT(1H1,T11,
     1' *************************************************',/,T11,
     1' *          Environmental Protection Agency      *',/,T11,
     1' *           Storm Water Management Model        *',/,T11,
     1' *                Version 4.05PDX                *',/,T11,
     2' *************************************************',//,T11,
     3'                  Developed by                    ',//,T11,
     3' *************************************************',/,T11,
     2' *             Metcalf & Eddy, Inc.              *',/,T11,
     3' *            University of Florida              *',/,T11,
     4' *       Water Resources Engineers, Inc.         *',/,T11,
     4' *              September 1970                   *',/,T11,     
     5' *************************************************',//,T11,
     4'                   Updated by                     ',//,T11,
     4' *************************************************',/T11,
     4' *            University of Florida              *',/,T11,
     4' *          Camp Dresser & McKee, Inc.           *'/,T11,  
     5' *                                               *',/,T11,  
     5' *   March     1975           November  1977     *',/,T11,
     4' *   November  1981           December  1990     *',/,T11,
     4' *************************************************',//,T11,
     4' *************************************************',/,T11,
     5' *    This is a new release of SWMM.  If any     *',/,T11,
     6' *     problems occur in running this model      *',/,T11,
     7' *     please contact Dr. Wayne Huber at the     *',/,T11,
     8' *           University of Florida               *',/,T11,
     8' *            Phone 1-904-392-0846               *'/,T11,
     9' *************************************************',/)
11    FORMAT(//T11,
     4' -------------------------------------------------',/,T11,
     +' |                                               |',/,T11,
     +' |   UPDATED FOR THE CITY OF PORTLAND, OREGON    |',/,T11,
     +' |        BUREAU OF ENVIRONMENTAL SERVICES       |',/,T11,
     +' |                                               |',/,T11,
     +' |        by Brown & Caldwell and CH2M  1993     |',/,T11,
     +' |    contact  Steve Merrill   1-206-281-4000    |',/,T11,
     +' |             Virgil Adderley 1-503-235-5000    |',/,T11,
     +' |                                               |',/,T11,
     4' -------------------------------------------------',//)
   15 FORMAT(T11,
     &' *************************************************',/,T11,
     1' * This is an implementation of EPA SWMM 4.25PDX *',/,T11,
     1' *  "   If you can''t be good - be lucky....   "  *',/,T11,
     1' *                                 Joe Hoffman   *',/,T11,
     3' *************************************************',/)
*     @' *************************************************',/,T11,
*     1' * This is an implementation of EPA SWMM 4.05PDX *',/,T11,
*     1' *  "Nature is full of infinite causes which     *',/,T11,
*     1' *   have never occurred in experience" da Vinci *',/,T11,
*     3' *************************************************',/)
   45 FORMAT(/,T14,
     1' ###########################################',/,T14,
     2' #        File names by SWMM Block         #',/,T14,
     3' #         JIN  -> Input to a Block        #',/,T14,
     4' #        JOUT  -> Output from a Block     #',/,T14,
     5' ###########################################',/)
   46 FORMAT(/,T14,
     1' ###########################################',/,T14,
     2' # Scratch file names for this simulation. #',/,T14,
     5' ###########################################',/)
   55 FORMAT(T14,'  JIN for Block # ',I5,' File #',I5,2X,A60)
   56 FORMAT(T14,' JOUT for Block # ',I5,' File #',I5,2X,A60)
   57 FORMAT(T14,' NSCRAT # ',I5,' File #',I5,2X,A60)
  110 FORMAT(4X,A4)
  180 FORMAT(/,' ===> Your input file was named : ',A24,/,
     +         ' ===> Your output file was named: ',A24)
  185 FORMAT(/,' ===> SWMM 4.05 Simulation ended normally.')
  230 FORMAT(/,' ===> Correct Block name not found. ',/,
     1         ' ===> ',A4,' was found instead.  Program stops.',/,
     2    ' ===> Check your data input for the following problems:',/,
     3    '      1.  Using the wrong executable file.',/,
     4    '      2.  Too many hydrograph input data lines.',/,
     5    '      3.  SWMM Block is commented out of MAIN.FOR',/,
     6    '      4.  Wrong input sequence of data.',/)
  300 FORMAT(/,T11,
     +' ***************************************************',/,T11,
     +' *    Parameter Values on the Tapes Common Block   *',/,T11,
     +' ***************************************************',//,T9,
C     +'  Number of Subcatchments in the Runoff Block (NW)....',I5,/,T9,
C     +'  Number of Channel/Pipes in the Runoff Block (NG)....',I5,/,T9,
C     +'  Number of Groundwater Subcatchments in unoff (NGW).',I5,/,T9,
C     +'  Runoff Water quality constituents (NRQ).............',I5,/,T9,
C     +'  Runoff Land Uses per Subcatchment (NLU).............',I5,/,T9,
     +'  Number of Elements in the Transport Block (NET).....',I5,/,T9,
     +'  Number of Storage Junctions in Transport (NTSE).....',I5,/,T9,
     +'  Number of Input Hydrographs in Transport (NTH)......',I5,/,T9,
     +'  Number of Pollutants in Transport (NTPL)............',I5,/,T9,
     +'  Number of Interface locations (NIE).................',I5,/,//)
C     +'  Number of Elements in the Extran Block (NEE)........',I5,/,T9,
C     +'  Number of Pumps in Extran (NEP).....................',I5,/,T9,
C     +'  Number of Orifices in Extran (NEO)..................',I5,/,T9,
C     +'  Number of Extran Orfices with Time History (NVORF)..',I5,/,T9,
C     +'  Number of Tide Gates/Free Outfalls in Extran (NTG)..',I5,/,T9,
C     +'  Number of Extran Weirs (NEW)........................',I5,/,T9,
C     +'  Number of Extran printout locations (NPO)...........',I5,/,T9,
C     +'  Number of Tide elements in Extran (NTE).............',I5,/,T9,
C     +'  Number of Natural channels (NNC)....................',I5,/,T9,
C     +'  Number of Storage junctions in Extran (NVSE)........',I5,/,T9,
C     +'  Number of Time history data points in Extran(NTVAL).',I5,/,T9,
C     +'  Number of Data points for variable storage elements',    /,T9,
C     +'            in the Extran Block (NVST)................',I5,/,T9,
C     +'  Number of Input Hydrographs in Extran (NEH).........',I5,/,T9,
C     +'  Number of Radial Control Gates in Extran (NRADG)....',I5,/,T9,
C     +'  Number of Siphon Weirs in Extran (NSYF).............',I5,/,T9,
C     +'  Number of Leaping Weirs in Extran (NGA).............',I5,//)
  310 FORMAT(/,
     +' ******************************************************',/,
     +' *        SWMM 4.05 Simulation Date and Time Summary  *',/
     +' ******************************************************',/,
*CNBC	-	VCA 7/17/92 ****************
*	Change output to give only date and time as determined via Lahey Fortran.
**     +' * Starting Date... ',A10,I3,', ',I4,'                *',/,
**     +' *          Time...         ',3(I2,':'),I2,'                *',/,
**     +' *   Ending Date... ',A10,I3,', ',I4,'                *',/,
**     +' *          Time...         ',3(I2,':'),I2,'                *',/,
**     +' *  Elapsed Time...          ',F10.3,' minutes.       *',/,
*
     +' *  Starting Date...   (MM/DD/YY):   ',A8,'         *',/,
     +' *           Time...   (HR:MN:SC):   ',I2,2(':',I2),
     +'         *',/,
     +' *    Ending Date...   (MM/DD/YY):   ',A8,'         *',/,
     +' *           Time...   (HR:MN:SC):   ',I2,2(':',I2),
     +'         *',/,
     +' *   Elapsed Time...             ',F10.3,' minutes.  *',/,
     +' ******************************************************',/)
1851  FORMAT(/' ===> ERROR!  THIS EXECUTABLE CONTAINS ONLY THE',
     +' TRANSPORT BLOCK AND NOT THE BLOCK REQUESTED')
C=======================================================================
      END
