C     Last change:  AMM   7 Aug 2000   11:39 am
      SUBROUTINE COMBIN
C=======================================================================
C     Rewritten   JUNE, 1988 by R. Dickinson
C     Updated      May, 1989 by R.E.D.
C     Updated November, 1990 by Laura Terrell, CDM
C     Updated December, 1990 by R.E.D. 
C=======================================================================
      INCLUDE 'TAPES.INC'
      INCLUDE 'INTER.INC'
      INCLUDE 'STIMER.INC'
      INCLUDE 'COMB.INC'
C=======================================================================
CPDX-- VCA --     DOUBLE PRECISION DELT,DELT1,DELT2,DIFF,TDIFF
      DOUBLE PRECISION DIFF,TDIFF
      CHARACTER KODEOT*10
      INTEGER   SECOND,FIRST,ILAG
      LOGICAL*1 OK1,OK2
C=======================================================================
C     This Subroutine has five main objectives:
C
C     ICOMB      => IF = 1, COLLATE PROGRAM IS USED
C                   IF = 2, COMBINE PROGRAM IS USED
C                   IF = 3, READ FILE HEADER
C                   IF = 4, CREATE ASCII FILE
C                   IF = 5, CALCULATE THE SIMPLE STATISTICS OF 
C                           AN INTERFACE FILE
C                   IF = 6, CALCULATE THE SIMPLE STATISTICS OF 
C                           AN RAIN BLOCK INTERFACE FILE
C                   IF = 7, CALCULATE THE SIMPLE STATISTICS OF 
C                           A TEMP BLOCK INTERFACE FILE
C=======================================================================
C     Define Statement function for linear interpolation.
C=======================================================================
      QLINTP(Q1,Q2,T1,T2,T) = Q1 + (Q2-Q1)*(T-T1)/(T2-T1)
C=======================================================================
      WRITE(N6,10)
      WRITE(*,10)
C=======================================================================
C     Initialization.
C=======================================================================
      INCNT      = INCNT  + 1
      IOUTCT     = IOUTCT + 1
      LAST       = JIN(INCNT)
      NEXT       = JOUT(IOUTCT)
      SECOND     = NSCRAT(1)
      IF(LAST.LE.0) CALL ERRORSW(100)
C=======================================================================
C     Open files for the Combine Block.
C=======================================================================
      IF(JIN(INCNT).GT.0.AND.(FFNAME(INCNT).EQ.'JOT.UF'.OR.
     +      FFNAME(INCNT).EQ.'JIN.UF'))
     +      OPEN(JIN(INCNT),FORM='UNFORMATTED',STATUS='SCRATCH')
      IF(JIN(INCNT).GT.0.AND.FFNAME(INCNT).NE.'JOT.UF'.AND.
     +      FFNAME(INCNT).NE.'JIN.UF')
     +      OPEN(JIN(INCNT),FILE=FFNAME(INCNT),FORM='UNFORMATTED',
     +      STATUS='UNKNOWN')
      IF(JOUT(IOUTCT).GT.0.AND.(FFNAME(25+IOUTCT).EQ.'JOT.UF'.OR.
     +      FFNAME(25+IOUTCT).EQ.'JIN.UF'))
     +      OPEN(JOUT(IOUTCT),FORM='UNFORMATTED',STATUS='SCRATCH')
      IF(JOUT(IOUTCT).GT.0.AND.FFNAME(25+IOUTCT).NE.'JOT.UF'.AND.
     +      FFNAME(25+IOUTCT).NE.'JIN.UF')
     +      OPEN(JOUT(IOUTCT),FILE=FFNAME(25+IOUTCT),FORM='UNFORMATTED',
     +      STATUS='UNKNOWN')
      IF(NSCRAT(1).GT.0.AND.FFNAME(51).NE.'SCRT1.UF') OPEN(NSCRAT(1),
     +             FILE=FFNAME(51),FORM='UNFORMATTED',STATUS='UNKNOWN')
      IF(NSCRAT(1).GT.0.AND.FFNAME(51).EQ.'SCRT1.UF') OPEN(NSCRAT(1),
     +             FORM='UNFORMATTED',STATUS='SCRATCH')
C=======================================================================
C     Data initialization.
C=======================================================================
      DO 8 J     = 1,10
      NDIM(J)    = 0
      NDIM2(J)   = 0
      PNAM1(J)   = ' '
      PNAM2(J)   = ' '
      PUNIT(J)   = ' '
      PUNIT2(J)  = ' '
      PUNIT1(J)  = ' '
      NDIM1(J)   = 0
      DO 8 I     = 1,NIE
      CPOLL(J,I) = 0.0
      POLL1(J,I) = 0.0
      POLL2(J,I) = 0.0
      POLL3(J,I) = 0.0
      POLD1(J,I) = 0.0
      POLD2(J,I) = 0.0
      IF(J.GT.1) GO TO 8
      QO1(I)     = 0.0
      QO2(I)     = 0.0
      QO3(I)     = 0.0
      QQO(I)     = 0.0
      QOLD1(I)   = 0.0
      QOLD2(I)   = 0.0
      INPOS1(I)  = 0
      INPOS2(I)  = 0
      JCOMB(I)   = 0
    8 CONTINUE
C=======================================================================
C>>>>>READ DATA GROUP A1<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
C=======================================================================
      READ(N5,*,ERR=888) CC,ICOMB
      WRITE(N6,71) ICOMB
      IF(NEXT.LE.0.AND.ICOMB.LT.2) CALL ERRORSW(101)
      IF(NEXT.LE.0.AND.ICOMB.EQ.4) CALL ERRORSW(101)
C=======================================================================
CPDX--PLJ/CH2M HILL 11/5/91--------------------------------------------
C       to access the multiple combine subroutines

      IF(ICOMB.EQ.10 .OR. ICOMB.EQ.11) THEN
         CALL MULTCOMB(ICOMB)
         WRITE(N6,9000)
         WRITE(*,9000)
         RETURN
      ENDIF
CPDX--------------------------------------------------------------------

      IF(ICOMB.EQ.3.OR.ICOMB.GT.4) THEN
                                   CALL COMB1(ICOMB)
                                   WRITE(N6,9000)
                                   WRITE(*,9000)
                                   RETURN
                                   ENDIF
C=======================================================================
C==================> END OF COMBINE BLOCK IF ICOMB = 3, 5, 6, 7, 10, 11 
C=======================================================================
C     Read remaining COMBIN block input.
C=======================================================================
C>>>>>Read data group B1<<<<<<<<<<<<<<<<<<<<<<<
C=======================================================================
      READ(N5,*,ERR=888) CC,TITLE(3)
      READ(N5,*,ERR=888) CC,TITLE(4)
      WRITE(N6,660)         TITLE(3),TITLE(4)
C=======================================================================
C     NODEOT => Node number for output.
C     KODEOT => Node name   for output.
C=======================================================================
      IF(JCE.EQ.0) THEN
                   READ(N5,*,ERR=888) CC,NODEOT,NPOLL
                   WRITE(N6,410)         NODEOT,JIN(IOUTCT),NPOLL
                   ELSE
                   READ(N5,*,ERR=888) CC,KODEOT,NPOLL
                   WRITE(N6,411)         KODEOT,JIN(IOUTCT),NPOLL
                   ENDIF
C=======================================================================
C>>>>>Read data group B3<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
C=======================================================================
      IF(NPOLL.GT.0) THEN
                     READ(N5,*,ERR=888) CC,(NPOS1(J),NPOS2(J),J=1,NPOLL)
                     WRITE(N6,420)       (J,NPOS1(J),NPOS2(J),J=1,NPOLL)
                     ENDIF
C=======================================================================
C>>>>>Read data group C1<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
C=======================================================================
      READ(N5,*,ERR=888) CC,NUMX,NUMR
      WRITE(N6,430)         NUMX,NUMR
C=======================================================================
C>>>>>Read data group C2<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
C=======================================================================
      IF(NUMX.GT.0) THEN
              IF(JCE.EQ.0) READ(N5,*,ERR=888) CC,(NODEX(I),I=1,NUMX)
              IF(JCE.EQ.1) READ(N5,*,ERR=888) CC,(KODEX(I),I=1,NUMX)
              WRITE(N6,440)
              IF(JCE.EQ.0) WRITE(N6,450) (NODEX(I),I=1,NUMX)
              IF(JCE.EQ.1) WRITE(N6,451) (KODEX(I),I=1,NUMX)
              ENDIF
C=======================================================================
C>>>>>Read data group C3<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
C=======================================================================
      IF(NUMR.GT.0) THEN
              IF(JCE.EQ.0) READ(N5,*,ERR=888) CC,(NODER(I),I=1,NUMR)
              IF(JCE.EQ.1) READ(N5,*,ERR=888) CC,(KODER(I),I=1,NUMR)
              WRITE(N6,445)
              IF(JCE.EQ.0) WRITE(N6,450) (NODER(I),I=1,NUMR)
              IF(JCE.EQ.1) WRITE(N6,451) (KODER(I),I=1,NUMR)
              ENDIF
C#######################################################################
C========> Extract and renumber an interface file (ICOMB=2)
C#######################################################################
      IF(ICOMB.EQ.2.AND.NUMX.EQ.0) CALL ERRORSW(102)
      IF(ICOMB.EQ.2) THEN
               CALL INFACE(1,LAST)
C=======================================================================
C              Transfer information to file next.
C=======================================================================
               REWIND NEXT
               WRITE(NEXT) NUMX,NQUAL
               IF(JCE.EQ.0.AND.NUMR.GT.0) WRITE(NEXT)(NODER(I),I=1,NUMX)
               IF(JCE.EQ.0.AND.NUMR.EQ.0) WRITE(NEXT)(NODEX(I),I=1,NUMX)
               IF(JCE.EQ.1.AND.NUMR.GT.0) WRITE(NEXT)(KODER(I),I=1,NUMX)
               IF(JCE.EQ.1.AND.NUMR.EQ.0) WRITE(NEXT)(KODEX(I),I=1,NUMX)
               SOURCE = 'COMBINE BLOCK'
               CALL INFACE(2,NEXT)
C=======================================================================
C              Read input interface file information.
C=======================================================================
  50           IF(NQUAL.LE.0) READ(LAST,END=90) JDAY,TMDAY,
     +                        DELT,(QO1(I),I=1,LOCATS)
               IF(NQUAL.GT.0) READ(LAST,END=90) JDAY,TMDAY,
     +                  DELT,(QO1(I),(POLL1(J,I),J=1,NQUAL),I=1,LOCATS)
C=======================================================================
C              Extract only selected nodes.
C=======================================================================
               DO 55 J = 1,LOCATS
               DO 60 K = 1,NUMX
               IF(JCE.EQ.0.AND.NODEX(K).EQ.NLOC(J)) THEN
                                           QO2(K) = QO1(J)
                                           IF(NQUAL.GT.0) THEN
                                           DO 70 KK    = 1,NQUAL
   70                                      POLL2(KK,K) = POLL1(KK,J)
                                           ENDIF                                         
                                           ENDIF
               IF(JCE.EQ.1.AND.KODEX(K).EQ.KAN(J)) THEN
                                           QO2(K) = QO1(J)
                                           IF(NQUAL.GT.0) THEN
                                           DO 75 KK    = 1,NQUAL
   75                                      POLL2(KK,K) = POLL1(KK,J)
                                           ENDIF                                         
                                           ENDIF
   60          CONTINUE
   55          CONTINUE
C=======================================================================
C              Write output file information.
C=======================================================================
               IF(NQUAL.EQ.0) WRITE(NEXT) JDAY,TMDAY,
     +                        DELT,(QO2(I),I=1,NUMX)
               IF(NQUAL.GT.0) WRITE(NEXT) JDAY,TMDAY,
     +            DELT,(QO2(I),(POLL2(J,I),J=1,NQUAL),I=1,NUMX)
               GO TO 50
  90           WRITE(N6,9000)
               WRITE(*,9000)
               RETURN
               ENDIF         
C#######################################################################
C============> End of ICOMB = 2 Option.
C#######################################################################
      IF(ICOMB.EQ.4) THEN
                     CALL COMB2(ICOMB)
                     WRITE(N6,9000)
                     WRITE(*,9000)
                     RETURN
                     ENDIF
C#######################################################################
C============> END OF ICOMB = 4 OPTION
C#######################################################################
C#######################################################################
C     COLLATE (ICOMB=0) OR COMBINE (ICOMB=1) OPTIONS are left.
C     The pollutant names and units are saved from the first
C     data-set and written on the output data-set.
C#######################################################################
C#######################################################################
C     LOCAT1 = Total number of inlets on first input data set.
C     LOCAT2 = Total number of inlets on second input data set.
C     NQ1    = Total number of water quality constituents on 1st data set
C     NQ2    = Total number of water quality constituents on 2nd data set
C=======================================================================
C     Read file headers from file # 2.
C=======================================================================
      IF(SECOND.LE.0) CALL ERRORSW(103)
      WRITE(N6,9005)
      CALL INFACE(1,SECOND)
      LOCAT2   = LOCATS
      NQ2      = NQUAL
      DO 210 I = 1,LOCAT2
      JUNC2(I) = NLOC(I)
      IF(NQ2.GT.0) THEN
                   DO 220 J  = 1,NQ2
                   PNAM2(J)  = PNAME(J)
                   PUNIT2(J) = PUNIT(J)
  220              NDIM2(J)  = NDIM(J)                     
                   ENDIF
  210 CONTINUE
C=======================================================================
C     Add the tributary area's together in variable TRIBBA.
C=======================================================================
      TRIBBA = 0.0
      TRIBBA = TRIBBA + TRIBA
C=======================================================================
C     Read file headers from file # 1.
C=======================================================================
      IF(LAST.LE.0) CALL ERRORSW(103)
      WRITE(N6,9010)
      CALL INFACE(1,LAST)
      LOCAT1   = LOCATS
      NQ1      = NQUAL
      DO 230 I = 1,LOCAT1
      JUNC1(I) = NLOC(I)
      IF(NQ1.GT.0) THEN
                   DO 240 J  = 1,NQ1
                   PNAM1(J)  = PNAME(J)
                   PUNIT1(J) = PUNIT(J)
  240              NDIM1(J)  = NDIM(J)                     
                   ENDIF
  230 CONTINUE
      TRIBA  = TRIBBA + TRIBA
C=======================================================================
C     DETERMINE THE NAMES OF THE QUALITY CONSTITUENTS ON THE NEW
C               INTERFACE FILE.  THE NAMES WILL BE DUPLICATES OF
C               THE NAMES ON THE FIRST INPUT FILE - except for the
C               case in which NPOS1(KPOLL) equals zero, in which the
C               name will be copied from the second input file.
C=======================================================================
      IF(NPOLL.GT.0) THEN
                     DO 470 KPOLL = 1,NPOLL
                     IF(NPOS1(KPOLL).LE.0) GO TO 460
                     K1           = NPOS1(KPOLL)
                     PNAME(KPOLL) = PNAM1(K1)
                     PUNIT(KPOLL) = PUNIT1(K1)
                     NDIM(KPOLL)  = NDIM1(K1)
                     GO TO 470
  460                K2 = NPOS2(KPOLL)
                     IF(K2.LE.0) GO TO 2200
                     PNAME(KPOLL)   = PNAM2(K2)
                     PUNIT(KPOLL)   = PUNIT2(K2)
                     NDIM(KPOLL)    = NDIM2(K2)
  470                CONTINUE
                     ENDIF
C=======================================================================
C     Read the first line of the two interface files.
C=======================================================================
      IF(NQ1.EQ.0) THEN
                   READ(LAST) JULDAY,TIMDAY,DELT1,(QO1(I),I=1,LOCAT1)
                   ELSE
                   READ(LAST) JULDAY,TIMDAY,DELT1,
     +                        (QO1(I),(POLL1(J,I),J=1,NQ1),I=1,LOCAT1)
                   ENDIF
      IF(NQ2.EQ.0) THEN
                   READ(SECOND) JDAY2,TMDAY2,DELT2,(QO2(I),I=1,LOCAT2)
                   ELSE
                   READ(SECOND) JDAY2,TMDAY2,DELT2,
     +                          (QO2(I),(POLL2(J,I),J=1,NQ2),I=1,LOCAT2)
                   ENDIF
C=======================================================================
C     Determine TZERO and IDATEZ for the new interface file.
C=======================================================================
      CALL NTIME(JDAY2,TMDAY2,DIFF)
      IF(DIFF.GE.0.0) THEN
                      TZERO  = TIMDAY
                      FIRST  = 1
                      IDATEZ = JULDAY
                      ENDIF
      IF(DIFF.LT.0.0) THEN
                      TZERO  = TMDAY2
                      FIRST  = 2
                      IDATEZ = JDAY2
                      ENDIF
C=======================================================================
C     IF ICOMB EQUALS ONE AND THE COLLATE PROGRAM IS REQUESTED
C     IT IS NECESSARY TO INTERLEAVE THE FILE NUMBERS FROM INPUT
C     FILES ONE AND TWO.  IF THE TWO FILES HAVE THE SAME INLET NUMBER
C     IN THEIR INLET ARRAY THE FLOW AND QUALITY CONSTITUENT VALUES WILL
C     BE ADDED TOGETHER.  IF THE INLET NUMBER APPEARS ON ONLY ONE FILE
C     THE FLOW QUALITY CONSTITUENT VALUES WILL BE TRANSFERED TO THE
C     NEW INTERFACE FILE FROM THAT INPUT FILE.
C=======================================================================
C    THE NEW INTERFACE FILE'S INLET NUMBERS WILL BE ARRANGED IN THE
C        FOLLOWING ORDER: FIRST --> INLETS IN BOTH INPUT FILES
C                         SECOND--> THOSE INLETS IN FILE ONE
C                                   NOT ALREADY TRANSFERED
C                         THIRD --> THOSE INLETS IN FILE TWO
C                                   NOT ALREADY TRANSFERED
C========================================================================
C     NPOSIT    = INLET position on the new interface file.
C     INPOS1    = INLET position on input file 1 corresponding
C                 to NPOSIT position on the new interface file.
C     INPOS2    = INLET position on input file 2 corresponding
C                 to NPOSIT position on the new interface file.
C     JUNC1     = INLET array from input file 1.
C     JUNC2     = INLET array from input file 2.
C     NLOC      = INLET array for the new interface file.
C     LOCATS    = Number of inlets on both file'S 1 and 2.
C=======================================================================
C     Find the INLET locations that are on both interface files.
C=======================================================================
      NPOSIT         = 0
      DO 575 I       = 1,LOCAT1
      DO 550 N       = 1,LOCAT2
      IF(JUNC1(I).NE.JUNC2(N)) GO TO 550
      NPOSIT         = NPOSIT + 1
      INPOS1(NPOSIT) = I
      INPOS2(NPOSIT) = N
      NLOC(NPOSIT)   = JUNC1(I)
      GO TO 575
  550 CONTINUE
  575 CONTINUE
C=======================================================================
C     Find the INLET locations that are only on interface file # 1.
C=======================================================================
      LOCATS         = NPOSIT
      DO 625  I      = 1,LOCAT1
      DO 600  N      = 1,LOCATS
      IF(NLOC(N).EQ.JUNC1(I)) GO TO 625
  600 CONTINUE
      NPOSIT         = NPOSIT + 1
      NLOC(NPOSIT)   = JUNC1(I)
      INPOS1(NPOSIT) = I
      INPOS2(NPOSIT) = 0
  625 CONTINUE
C=======================================================================
C     Find the INLET locations that are only on interface file # 2.
C=======================================================================
      LOCATS         = NPOSIT
      DO 675 I       = 1,LOCAT2
      DO 650 N       = 1,LOCATS
      IF(NLOC(N).EQ.JUNC2(I)) GO TO 675
  650 CONTINUE
      NPOSIT         = NPOSIT + 1
      NLOC(NPOSIT)   = JUNC2(I)
      INPOS1(NPOSIT) = 0
      INPOS2(NPOSIT) = I
  675 CONTINUE
      LOCATS         = NPOSIT
C=======================================================================
C     Now create another tester array - JCOMB(200)
C         If JCOMB = 1 then both files one and two have an inlet
C                      corresponding to NPOSIT on the new interface file
C         If JCOMB = 2 then only the first file has an inlet
C                      corresponding to NPOSIT on the new interface file
C         If JCOMB = 3 then only the second file has an inlet
C                      corresponding to NPOSIT on the new interface file
C=======================================================================
      DO 700 NPOSIT = 1,LOCATS
      IF(INPOS1(NPOSIT).GT.0.AND.INPOS2(NPOSIT).GT.0) JCOMB(NPOSIT) = 1
      IF(INPOS1(NPOSIT).GT.0.AND.INPOS2(NPOSIT).LE.0) JCOMB(NPOSIT) = 2
      IF(INPOS1(NPOSIT).LE.0.AND.INPOS2(NPOSIT).GT.0) JCOMB(NPOSIT) = 3
  700 CONTINUE
C=======================================================================
C     THE NEXT STATEMENTS IS ESSENTIALLY THE ONLY DIFFERENCE
C         BETWEEN THE COMBINE PROGRAM AND THE COLLATE PROGRAM.
C         THE COMBINE PROGRAM HAS THE SAME PROGRAM INPUT AS THE
C         COLLATE PROGRAM EXCEPT THAT SUMMATION SEQUENCE DEPOSITS
C         THE ENTIRE FLOW AND QUALITY VALUES INTO INLET - NODEOT.
C=======================================================================
      IF(ICOMB.EQ.1) THEN
                     KPOSIT  = 1
                     IF(NODEOT.LE.0)   NODEOT =  12345
                     IF(KODEOT.EQ.' ') KODEOT = '12345'
                     IF(JCE.EQ.0) NLOC(1) = NODEOT
                     IF(JCE.EQ.1)  KAN(1) = KODEOT
                     LOCATS  = 1
                     ENDIF
C=======================================================================
C     Write the header information on the new interface file.
C=======================================================================
      IF(NEXT.GT.0) THEN
                    REWIND NEXT
                    WRITE(NEXT) LOCATS,NPOLL
                    IF(JCE.EQ.0) WRITE(NEXT) (NLOC(I),I=1,LOCATS)
                    IF(JCE.EQ.1) WRITE(NEXT)  (KAN(I),I=1,LOCATS)
                    SOURCE = 'COMBINE BLOCK'
                    WRITE(N6,9015)
                    CALL INFACE(2,NEXT)
                    CALL INFACE(1,NEXT)
                    ENDIF
C=======================================================================
C     Do loop for reading all input data.
C=======================================================================
      XTIM1      = 0.0
      XTIM2      = 0.0
      DELT1      = 0.0
      DELT2      = 0.0
      OK1        = .FALSE.
      OK2        = .FALSE.
      DIFF       = DABS(DIFF)
      ILAG       = 1
      J1LAG      = 0
      J2LAG      = 0
      IF(FIRST.EQ.1)  OK1 = .TRUE.
      IF(FIRST.EQ.2)  OK2 = .TRUE.
      IF(DIFF.EQ.0.0) OK2 = .TRUE.
C=======================================================================
C     Initialize arrays CPOLL and QQO for each time step.
C=======================================================================
      DO 999 KDT = 1,1000000
      DO 750   K = 1,LOCATS
      DO 740   L = 1,NPOLL
  740 CPOLL(L,K) = 0.0
  750 QQO(K)     = 0.0
C=======================================================================
C     Read the two input files.
C=======================================================================
      IF(FIRST.EQ.2.AND.XTIM2.GE.DIFF) OK1 = .TRUE.
      IF(FIRST.EQ.1.AND.XTIM1.GE.DIFF) OK2 = .TRUE.
C=======================================================================
      IF(JULDAY.NE.99999.AND.OK1) THEN
      IF(ILAG.NE.2) THEN
                   IF(NQ1.EQ.0) READ(LAST,END=1000) JULDAY,TIMDAY,DELT1,
     +                                 (QO1(I),I=1,LOCAT1)
                   IF(NQ1.GT.0) READ(LAST,END=1000) JULDAY,TIMDAY,DELT1,
     +                         (QO1(I),(POLL1(J,I),J=1,NQ1),I=1,LOCAT1)
                   ELSE
                   DO 744 I   = 1,LOCAT1
                   QO1(I)     = QO3(I)
                   DO 743 J   = 1,NQ1
                   POLL1(J,I) = POLL3(J,I)
  743              CONTINUE
  744              CONTINUE
                   ENDIF
      DELT = DELT1
      ENDIF
C=======================================================================
      IF(JDAY2.NE.99999.AND.OK2) THEN
      IF(ILAG.NE.3) THEN
                   IF(NQ2.EQ.0) READ(SECOND,END=1010) JDAY2,TMDAY2,
     +                                    DELT2,(QO2(I),I=1,LOCAT2)
                   IF(NQ2.GT.0) READ(SECOND,END=1010) JDAY2,TMDAY2,
     +               DELT2,(QO2(I),(POLL2(J,I),J=1,NQ2),I=1,LOCAT2)
                   ELSE
                   DO 746 I   = 1,LOCAT2
                   QO2(I)     = QO3(I)
                   DO 745 J   = 1,NQ2
                   POLL2(J,I) = POLL3(J,I)
  745              CONTINUE
  746              CONTINUE
                   ENDIF
      DELT = DELT2
      ENDIF
C=======================================================================
C     The end of both input files has been found.  Branch to 1020.
C=======================================================================
      IF(JULDAY.EQ.99999.AND.JDAY2.EQ.99999) GO TO 1020
C=======================================================================
C     Calculate the time difference in seconds between two data sets.
C=======================================================================
      CALL NTIME(JDAY2,TMDAY2,DIFF)
C#######################################################################
C                      Calculate the variable ILAG.  
C     
C     ILAG = 1 ==> The difference in time between FILE 2 & 1 = 0.
C                  No changes from the original COMBINE code.
C     ILAG = 2 ==> The difference in time between FILE 2 & 1 is < 0.
C                  This means that FILE 2'S time is before FILE 1'S 
C                  time.  FILE 1'S values for flow and pollutants will
C                  be set to zero.  FILE 1'S values will be temporarily
C                  stored in QO3 and POLL3.
C     ILAG = 3 ==> The difference in time between FILE 2 & 1 is > 0.
C                  This means that FILE 1'S time is before FILE 2'S 
C                  time.  FILE 2'S values for flow and pollutants will
C                  be set to zero.  FILE 2'S values will be temporarily
C                  stored in QO3 and POLL3.
C
C#######################################################################
      IF(DIFF.EQ.0.0) THEN
                      ILAG        = 1
                      J1LAG       = 0
                      J2LAG       = 0
                      DO 7400 I   = 1,LOCAT1
                      QOLD1(I)    = QO1(I)
                      DO 7410 J   = 1,NQ1
7410                  POLD1(J,I)  = POLL1(J,I)
7400                  CONTINUE
                      DO 7420 I   = 1,LOCAT2
                      QOLD2(I)    = QO2(I)
                      DO 7430 J   = 1,NQ2
7430                  POLD2(J,I)  = POLL2(J,I)
7420                  CONTINUE
                      ENDIF
      IF(DIFF.LT.0.0) THEN
           ILAG       = 2
           J2LAG      = 0
           J1LAG      = J1LAG + 1
           TDIFF      = DABS(DIFF)
           IF(J1LAG.EQ.1) TDIF1(I) = TDIFF + DELT2 
           SLOPE      = (TDIF1(I)-TDIFF)/TDIF1(I)
           DO 748 I   = 1,LOCAT1
           QO3(I)     = QO1(I)
           QO1(I)     = (QO1(I)-QOLD1(I))*SLOPE + QOLD1(I)
           IF(QO1(I).LT.0.0) QO1(I) = 0.0
           DO 747 J   = 1,NQ1
           POLL3(J,I) = POLL1(J,I)
           POLL1(J,I) = (POLL1(J,I)-POLD1(J,I))*SLOPE + POLD1(J,I)
           IF(POLL1(J,I).LT.0.0) POLL1(J,I) = 0.0
  747      CONTINUE
  748      CONTINUE
           DELT1      = DELT1 - DELT2
           DELT       = DELT2
           ENDIF
      IF(DIFF.GT.0.0) THEN
           ILAG       = 3
           J1LAG      = 0
           J2LAG      = J2LAG + 1
           IF(J2LAG.EQ.1) TDIF2(I) = DIFF + DELT1 
           SLOPE      = (TDIF2(I)-DIFF)/TDIF2(I)
           DO 751 I   = 1,LOCAT2
           QO3(I)     = QO2(I)
           QO2(I)     = (QO2(I)-QOLD2(I))*SLOPE + QOLD2(I)
           IF(QO2(I).LT.0.0) QO2(I) = 0.0
           DO 749 J   = 1,NQ2
           POLL3(J,I) = POLL2(J,I)
           POLL2(J,I) = (POLL2(J,I)-POLD2(J,I))*SLOPE + POLD2(J,I)
           IF(POLL2(J,I).LT.0.0) POLL2(J,I) = 0.0
  749      CONTINUE
  751      CONTINUE
           DELT2      = DELT2 - DELT1
           DELT       = DELT1
           ENDIF
C=======================================================================
      XTIM1 = XTIM1 + DELT1
      XTIM2 = XTIM2 + DELT2
C=======================================================================
C     Combine all the input locations into one output location.
C=======================================================================
      IF(ICOMB.EQ.1) THEN
                     DO 940 KK1  = 1,LOCAT1
                     QQO(KPOSIT) = QQO(KPOSIT) + QO1(KK1)
                     IF(NPOLL.GT.0) THEN
                              DO 945  MM       = 1,NPOLL
                              K1               = NPOS1( MM )
  945                         CPOLL(MM,KPOSIT) = CPOLL(MM,KPOSIT)
     +                                            + POLL1(K1,KK1)
                              ENDIF
  940                CONTINUE
                     DO 950 KK2  = 1,LOCAT2
                     QQO(KPOSIT) = QQO(KPOSIT) + QO2(KK2)
                     IF(NPOLL.GT.0) THEN
                              DO 955  MM       = 1,NPOLL
                              K2               = NPOS2( MM )
  955                         CPOLL(MM,KPOSIT) = CPOLL(MM,KPOSIT)
     +                                            + POLL2(K2,KK2)
                              ENDIF
  950                CONTINUE
                     ENDIF
C=======================================================================
C     Collate (Interleave) all the input locations together.
C=======================================================================
      IF(ICOMB.EQ.0) THEN
                     DO 960 NPOSIT = 1,LOCATS
                     KK1         = INPOS1(NPOSIT)
                     KK2         = INPOS2(NPOSIT)
                     IF(JCOMB(NPOSIT).EQ.1) QQO(NPOSIT) = 
     +                               QQO(NPOSIT) + QO1(KK1) + QO2(KK2)
                     IF(JCOMB(NPOSIT).EQ.2) QQO(NPOSIT) = 
     +                                          QQO(NPOSIT) + QO1(KK1) 
                     IF(JCOMB(NPOSIT).EQ.3) QQO(NPOSIT) = 
     +                                          QQO(NPOSIT) + QO2(KK2) 
                     IF(NPOLL.GT.0) THEN
                        DO 920 MM        = 1,NPOLL
                        K1               = NPOS1( MM )
                        K2               = NPOS2( MM )
                        IF(JCOMB(NPOSIT).EQ.1) CPOLL(MM,NPOSIT) = 
     +                  CPOLL(MM,NPOSIT) + POLL1(K1,KK1) + POLL2(K2,KK2)
                        IF(JCOMB(NPOSIT).EQ.2) CPOLL(MM,NPOSIT) = 
     +                                  CPOLL(MM,NPOSIT) + POLL1(K1,KK1) 
                        IF(JCOMB(NPOSIT).EQ.3) CPOLL(MM,NPOSIT) = 
     +                                  CPOLL(MM,NPOSIT) + POLL2(K2,KK2) 
  920                   CONTINUE
                        ENDIF
  960                   CONTINUE
                     ENDIF
C=======================================================================
C     Write the actual interface data line.
C=======================================================================
  990 IF(NPOLL.EQ.0) WRITE(NEXT) JULDAY,TIMDAY,DELT,(QQO(K),K=1,LOCATS)
      IF(NPOLL.GT.0) THEN
      IF(ILAG.EQ.2)  THEN
                     WRITE(NEXT) JDAY2,TMDAY2,DELT2,(QQO(K),
     .                      (CPOLL(M,K),M=1,NPOLL),K=1,LOCATS)
                     ELSE
                     WRITE(NEXT) JULDAY,TIMDAY,DELT1,(QQO(K),
     .                      (CPOLL(M,K),M=1,NPOLL),K=1,LOCATS)
                     ENDIF
                     ENDIF
      GO TO 999
C=======================================================================
C     End of the first file is reached.
C=======================================================================
 1000 CONTINUE
      JULDAY    = 99999
      TIMDAY    =   0.0      
      DO 1050 I = 1,LOCAT1
      QO1(I)    = 0.0
      QOLD1(I)  = 0.0
      IF(NQ1.GT.0) THEN
                   DO 1060 J  = 1,NQ1
                   POLL1(J,I) = 0.0
 1060              POLD1(J,I) = 0.0
                   ENDIF
 1050 CONTINUE                          
      GO TO 999
C=======================================================================
C     End of the second file is reached.
C=======================================================================
 1010 CONTINUE
      JDAY2     = 99999
      TMDAY2    =   0.0      
      DO 1100 I = 1,LOCAT2
      QO2(I)    = 0.0
      QOLD2(I)  = 0.0
      IF(NQ2.GT.0) THEN
                   DO 1110 J  = 1,NQ2
                   POLL2(J,I) = 0.0
 1110              POLD2(J,I) = 0.0
                   ENDIF
 1100 CONTINUE                          
  999 CONTINUE
 1020 CONTINUE
      WRITE(N6,9000)
      WRITE(*,9000)
      RETURN
 2200 WRITE(N6,2210)
  888 CALL IERROR
C=======================================================================
  10  FORMAT(/,
     +' ################################################',/,
     +' # Entry made to Combine model. Last updated by #',/,
     .' # the University of Florida, December, 1990.   #',/,
     +' ################################################',/)
  71  FORMAT(/,' ICOMB...................................',I5,/,
     +         ' ICOMB = 0 ===> Collate program',/,
     +         '         1 ===> Combine program',/,
     +         '         2 ===> Extract program',/,
     +         '         3 ===> File reader program',/,
     +         '         4 ===> ASCII file creation',/,
     +         '         5 ===> File statistics program',/,
     +         '         6 ===> Read RAIN block output',/,
     +         '         7 ===> Read TEMP block output',/
     +         '        10 ===> Collate multiple files',/,
     +         '        11 ===> Combine multiple files',/)
  400 FORMAT(/,' Error - Multiple combine/collate does not allow '
     &             'quality constituent data.')
  410 FORMAT(/,' Output node number is............',I9,//,
     1         ' Output data-set unit number is...',I9,//,
     2         ' Number of quality constituents...',I9,/)
  411 FORMAT(/,' Output node name is..............',A9,//,
     1         ' Output data-set unit number is...',I9,//,
     2         ' Number of quality constituents...',I9,/)
  420 FORMAT(/,' Water quality constituent........',I9,/,
     +         ' Position on input file one.......',I9,/,
     +         ' Position on input file two.......',I9)
  430 FORMAT(' Locations to be extracted (NUMX).',I9,//,
     .       ' Locations to be renumbered (NUMR)',I9)
  440 FORMAT(/,
     +' #########################################',/,
     +' # The following nodes will be extracted #',/,
     +' #     from the input file(s).           #',/,
     +' #########################################',/)
  445 FORMAT(/,
     +' ##################################################',/,
     +' # The following numbers are the new node numbers #',/,
     +' # assigned to the extracted/renumbered nodes.    #',/,
     +' ##################################################',/)
  450 FORMAT(1X,10I10)
  451 FORMAT(1X,10A10)
  455 FORMAT(///,' Ending time in seconds for file 1 --->',F15.2,
     .        /, ' Ending time in seconds for file 2 --->',F15.2)
  660 FORMAT(1H1,4X,A80,/,5X,A80)
 2210 FORMAT(//,' NPOS1 and NPOS2 are both zero - where are the ',
     .          ' quality interface values for this constituent.')
 9000 FORMAT(/,' ===> Combine Block ended normally.')
 9005 FORMAT(/,
     +' **************************************************',/,
     +' *  Reading information from the NSCRAT(1) file.  *',/,
     +' **************************************************')
 9010 FORMAT(/,
     +' ********************************************',/,
     +' *  Reading information from the JIN file.  *',/,
     +' ********************************************')
 9015 FORMAT(/,
     +' *******************************************',/,
     +' *  Writing information on the JOUT file.  *',/,
     +' *******************************************')
      END
C$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
C     SUBROUTINE COMB1 does the analysis for ICOMB options 3,5,6 and 7.
C$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
      SUBROUTINE COMB1(ICOMB)
C$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
      INCLUDE 'TAPES.INC'
      INCLUDE 'INTER.INC'
      INCLUDE 'STIMER.INC'
      INCLUDE 'COMB.INC'
      DOUBLE PRECISION DELT1,DELT2,POOP
      DIMENSION JSTA(10),PCONV(10),SUMM(11)
      CHARACTER JOHNNY*10,NOTED(3)*10,MOTED(3)*10
      DATA NOTED/'  Pounds  ',' Quantity ',' Unit*cfs '/
      DATA MOTED/' Kilograms',' Quantity ',' Unit*l/s '/
      DATA JOHNNY/' ---------'/
C=======================================================================
C===> Read interface file headers (ICOMB=3)
C=======================================================================
      IF(ICOMB.EQ.3) THEN
                     IF(LAST.GT.0) CALL INFACE(1,LAST)
                     RETURN
                     ENDIF
C=======================================================================
C===> Read file headers and calculate simple statistics (ICOMB=5).
C=======================================================================
      IF(ICOMB.EQ.5) THEN
                     METRIC = 1
                     XTIME  = 0.0
                     IF(LAST.GT.0) CALL INFACE(1,LAST)
                     IF(QCONV.NE.1.0) METRIC = 2
                     DO 7990 I = 1,LOCATS
C=======================================================================
C                    Create quality conversion factors.
C=======================================================================
                     IF(NQUAL.GT.0) THEN
                              DO 7995 J  = 1,NQUAL
                                 IF(METRIC.EQ.1) THEN
                                 PCONV(J) = 16016.35
                                 IF(NDIM(J).EQ.1) PCONV(J) = 3.5315E-2
                                 ELSE
                                 PCONV(J) = 1000.0
                                 IF(NDIM(J).EQ.1) PCONV(J) = 0.001
                                 ENDIF
                              IF(NDIM(J).GE.2) PCONV(J) = 1.0
 7995                         POLL2(J,I) = 0.0                                     
                              ENDIF
 7990                QO2(I)    = 0.0                     
C=======================================================================
C                    Read the interface file.
C=======================================================================
                     ITEST     = 0
                     DELT1     = 0.0
                     DO 8888 K = 1,1000000
                     IF(NQUAL.LE.0) READ(LAST,END=8100) JDAY,TMDAY,
     +                              DELT2,(QO1(I),I=1,LOCATS)
                     IF(NQUAL.GT.0) READ(LAST,END=8100) JDAY,TMDAY,
     +                              DELT2,(QO1(I),(POLL1(J,I),
     +                              J=1,NQUAL),I=1,LOCATS)
C=======================================================================
C                    Calculate cumulative volumes and loads
C                    using trapezoidal integration.
C=======================================================================
                     XTIME         = XTIME + DELT2
                     POOP          = 0.5*(DELT1 + DELT2)
                     IF(ITEST.EQ.0) POOP = 2.0*DELT2
                     ITEST         = 0
                     DO 8000 I     = 1,LOCATS
                     IF(NQUAL.GT.0) THEN
                     DO 8005 J     = 1,NQUAL
 8005                POLL2(J,I)    = POLL2(J,I)+POLL1(J,I)*POOP*PCONV(J)
                     ENDIF
                     IF(QO1(I).NE.0.0) ITEST = 1
                     QO2(I)        = QO2(I) + QO1(I)*POOP
 8000                CONTINUE                     
                     DELT1         = DELT2
                     IF(ITEST.EQ.0) DELT1 = 0.0
 8888                CONTINUE                     
 8100                CONTINUE                     
C=======================================================================
C                    Write junction/inlet/manhole summaries.
C=======================================================================
                     NPOINT = K - 1
                                    IEND = 1
                     IF(NQUAL.GT.0) IEND = NQUAL + 1
                     IF(NQUAL.EQ.0) WRITE(N6,8290) 
                     IF(NQUAL.GT.0) WRITE(N6,8290) (PNAME(J),J=1,NQUAL)
                     IF(METRIC.EQ.1.AND.NQUAL.EQ.0) WRITE(N6,8295)
                     IF(METRIC.EQ.1.AND.NQUAL.GT.0) WRITE(N6,8295)
     +                            (NOTED(NDIM(J)+1),J=1,NQUAL)
                     IF(METRIC.EQ.2.AND.NQUAL.EQ.0) WRITE(N6,8296)
                     IF(METRIC.EQ.2.AND.NQUAL.GT.0) WRITE(N6,8296)
     +                            (MOTED(NDIM(J)+1),J=1,NQUAL)
                     WRITE(N6,8297) (JOHNNY,J=1,IEND)
                     XT        = XTIME/3600.0
                     DO 8190 I = 1,11
 8190                SUMM(I)   = 0.0
                     DO 8200 I = 1,LOCATS
                     IF(NQUAL.GT.0) THEN
                                    DO 8205 J  = 1,NQUAL
 8205                               SUMM(J+1)  = SUMM(J+1) + POLL2(J,I)
                                    ENDIF                        
                     SUMM(1)   = SUMM(1) + QO2(I)
                     IF(XTIME.EQ.0.0) THEN
                                      XTIME = 1.0
                                      WRITE(N6,8301) 
                                      ENDIF
                     IF(JCE.EQ.0.AND.NQUAL.EQ.0) WRITE(N6,8300) NLOC(I),
     +                                             QO2(I)/XTIME,QO2(I)
                     IF(JCE.EQ.0.AND.NQUAL.GT.0) WRITE(N6,8300) NLOC(I),
     +                        QO2(I)/XTIME,QO2(I),(POLL2(J,I),J=1,NQUAL)
                     IF(JCE.EQ.1.AND.NQUAL.EQ.0) WRITE(N6,8305) KAN(I),
     +                                             QO2(I)/XTIME,QO2(I)
                     IF(JCE.EQ.1.AND.NQUAL.GT.0) WRITE(N6,8305) KAN(I),
     +                       QO2(I)/XTIME,QO2(I),(POLL2(J,I),J=1,NQUAL)
 8200                CONTINUE                      
C=======================================================================
C                    Write the overall summary.
C=======================================================================
                     WRITE(N6,8310) (JOHNNY,J=1,IEND)
                     TERRA = SUMM(1)/(3630.0*TRIBA)*QCONV
                     IF(METRIC.EQ.2) TERRA = 25.4*TERRA
                     IF(NQUAL.EQ.0) WRITE(N6,8315) TERRA,SUMM(1)
                     IF(NQUAL.GT.0) WRITE(N6,8315) TERRA,SUMM(1),
     +                              (SUMM(J+1),J=1,NQUAL)
                     WRITE(N6,8311) (JOHNNY,J=1,IEND)
                     IF(METRIC.EQ.1.AND.NQUAL.EQ.0) WRITE(N6,8316)
                     IF(METRIC.EQ.1.AND.NQUAL.GT.0) WRITE(N6,8316)
     +                            (NOTED(NDIM(J)+1),J=1,NQUAL)
                     IF(METRIC.EQ.2.AND.NQUAL.EQ.0) WRITE(N6,8317)
                     IF(METRIC.EQ.2.AND.NQUAL.GT.0) WRITE(N6,8317)
     +                            (MOTED(NDIM(J)+1),J=1,NQUAL)
                     WRITE(N6,8320) XT,NPOINT
                     RETURN
                     ENDIF
C=======================================================================
C===> Read Rain block interface file (ICOMB = 6)
C=======================================================================
      IF(ICOMB.EQ.6) THEN
C=======================================================================
C                    Read the precipitation station number.
C=======================================================================
                     REWIND LAST
                     READ(LAST,ERR=335) NSTA,MRAIN,(JSTA(I),I=1,NSTA)
                     WRITE(N6,2115) NSTA
                     WRITE(N6,2120) (I,JSTA(I),I=1,NSTA)
C=======================================================================
C                    Calculate the total precipitation.
C=======================================================================
                     DO 324 I = 1,NSTA
  324                QO1(I)   = 0.0                     
                     DO 325 I = 1,MRAIN
                     READ(LAST,END=330,ERR=330) JDAY,TMDAY,
     +                             THISTO,(QO2(J),J=1,NSTA)
                     IF(I.EQ.1) THEN
                                JFIRST = JDAY
                                TFIRST = TMDAY
                                ENDIF
                     DO 326 J = 1,NSTA
  326                QO1(J)   = QO1(J)   + QO2(J)*THISTO/3600.0
  325                CONTINUE
  330                CONTINUE
                     WRITE(N6,9550) I,JFIRST,TFIRST,JDAY,TMDAY
                     DO 327 J = 1,NSTA
  327                WRITE(N6,9555) JSTA(J),QO1(J)
                     RETURN
  335                WRITE(N6,9500) 
                     RETURN
                     ENDIF
C=======================================================================
 2115 FORMAT(//,
     +' ########################################################',/,
     +' #  Precipitation output created using the Rain block   #',/,
     +' #  number of precipitation stations...',I9,'        #',/,
     +' ########################################################',/)
 2120 FORMAT(' Location Station number',/,
     +       ' -------- --------------',/,
     +       10(I9,'. ',I13,/))
 8290 FORMAT(/,
     +' ################################################',/
     +' #   Simple flow statistics from interface file #',/,
     +' ################################################',//,
     +'  Location #   Mean Flow  Total Flow ',10(2X,A8,2X))
 8295 FORMAT('                  cfs      cubic ft. ',
     +    10(2X,A10))
 8296 FORMAT('                  cms     cubic met. ',
     +    10(2X,A10))
 8297 FORMAT('  ----------   ---------',11(2X,A10))
 8301 FORMAT(/,' ===> Error !! Total time was 0.0 hours.')
 8300 FORMAT(1X,I10,1X,F12.5,11(1PE12.4))
 8305 FORMAT(2X,A10,F12.5,11(1PE12.4))
 8310 FORMAT(
     +'  ----------  ----------',11(2X,A10))
 8311 FORMAT(
     +'                --------',11(2X,A10))
 8315 FORMAT(' Total       ',F11.4,11(1PE12.4))
 8316 FORMAT('                  inches   cubic ft. ',
     +    10(2X,A10))
 8317 FORMAT('             millimeters  cubic met. ',
     +    10(2X,A10))
 8320 FORMAT(//,' Total simulation time ===> ',F20.4,' hours',//,
     +          ' Total number of steps ===> ',10X,I10)
 9500 FORMAT(/,' ===> Error !!  This file is probably not a Rain',
     +' or Temp Block interface file.')
 9550 FORMAT(//,' Total number of precipitation data points....',I12,/,
     +      ' First Julian day.............................',I12,/,
     +      ' First Julian day start time (seconds)........',F12.1,/,
     +      ' Last Julian day..............................',I12,/,
     +      ' Last Julian day start time (seconds).........',F12.1,//)
 9555 FORMAT(
     +' ###############################################',/,
     +' # Station......................................',I12,/,
     +' # Total Precipitation (inches or millimeters)..',F12.3,/,
     +' ###############################################')
C=======================================================================
      END
C$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
C     SUBROUTINE COMB2 does the analysis for ICOMB option 4.
C$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
      SUBROUTINE COMB2(ICOMB)
C$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
      INCLUDE 'TAPES.INC'
      INCLUDE 'INTER.INC'
      INCLUDE 'STIMER.INC'
      INCLUDE 'COMB.INC'
      DOUBLE PRECISION DELT
      CHARACTER NEWFIL*60
C#######################################################################
C========> Write a formatted interface file (ICOMB=4).
C#######################################################################
      IF(ICOMB.EQ.4.AND.LAST.GT.0) THEN
               XTIM1 = 0.0
               CALL INFACE(1,LAST)
               INQUIRE(NEXT,NAME=NEWFIL)
               CLOSE(NEXT)
               OPEN(NEXT,FILE=NEWFIL,FORM='FORMATTED',STATUS='UNKNOWN')
C=======================================================================
C              Read input interface file information.
C=======================================================================
               IF(NUMX.GT.0.AND.JCE.EQ.0) WRITE(NEXT,155)  
     +                              NQUAL,(NODEX(J),J=1,NUMX)
               IF(NUMX.EQ.0.AND.JCE.EQ.0) WRITE(NEXT,155)  
     +                              NQUAL,(NLOC(J),J=1,LOCATS)
               IF(NUMX.GT.0.AND.JCE.EQ.1) WRITE(NEXT,156)  
     +                              NQUAL,(KODEX(J),J=1,NUMX)
               IF(NUMX.EQ.0.AND.JCE.EQ.1) WRITE(NEXT,156)  
     +                              NQUAL,(KAN(J),J=1,LOCATS)
 100           IF(NQUAL.LE.0) READ(LAST,END=200) JDAY,TMDAY,
     +                        DELT,(QO1(I),I=1,LOCATS)
               IF(NQUAL.GT.0) READ(LAST,END=200) JDAY,TMDAY,
     +                  DELT,(QO1(I),(POLL1(J,I),J=1,NQUAL),I=1,LOCATS)
C=======================================================================
C              Extract only selected nodes.
C=======================================================================
               DO 150 J = 1,LOCATS
               IF(NUMX.GT.0) THEN
                             DO 160 K = 1,NUMX
                             IF(JCE.EQ.0.AND.NODEX(K).EQ.NLOC(J)) THEN
                                             QO2(K) = QO1(J)
                                             IF(NQUAL.GT.0) THEN
                                             DO 170 KK   = 1,NQUAL
  170                                        POLL2(KK,K) = POLL1(KK,J)
                                             ENDIF                                         
                             ENDIF
                             IF(JCE.EQ.1.AND.KODEX(K).EQ.KAN(J)) THEN
                                             QO2(K) = QO1(J)
                                             IF(NQUAL.GT.0) THEN
                                             DO 175 KK   = 1,NQUAL
  175                                        POLL2(KK,K) = POLL1(KK,J)
                                             ENDIF                                         
                             ENDIF
  160                        CONTINUE
                             ELSE
                             QO2(J) = QO1(J)
                             IF(NQUAL.GT.0) THEN
                                            DO 180 KK   = 1,NQUAL
  180                                       POLL2(KK,J) = POLL1(KK,J)
                                            ENDIF                                         
                             ENDIF
  150          CONTINUE
C=======================================================================
C              Write output file information.
C=======================================================================
                             KEND = LOCATS
               IF(NUMX.GT.0) KEND = NUMX
                            XTIM1 = XTIM1 + DELT/3600.0
               IF(NQUAL.EQ.0) WRITE(NEXT,205) JDAY,TMDAY,
     +                        XTIM1,(QO2(I),I=1,KEND)
               IF(NQUAL.GT.0) WRITE(NEXT,205) JDAY,TMDAY,
     +            XTIM1,(QO2(I),(POLL2(J,I),J=1,NQUAL),I=1,KEND)
               GO TO 100
 200           CONTINUE
               RETURN
               ENDIF         
C=======================================================================
  155 FORMAT(' ASCII interface file from the COMBINE block ',/,
     + ' Data columns are 1. Julian day, 2. Time of day in seconds,',/,
     + ' 3. Time in hours since start,   4. Number of pollutants ',I5,/,
     + ' 5. The following nodes ',/,
     +       (10I9))
  156 FORMAT(' ASCII INTERFACE FILE from the COMBINE BLOCK ',/,
     + ' Data columns are 1. Julian day, 2. Time of day in seconds,',/,
     + ' 3. Time in hours since start,   4. Number of pollutants ',I5,/,
     + ' 5. The following nodes ',/,
     +       (10A10))
  205 FORMAT(I8,1PG11.4,G15.6,220(G11.4))
C=======================================================================
      END
