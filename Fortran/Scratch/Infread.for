C     Last change:  AMM   6 Jun 2001    4:06 pm
      SUBROUTINE INFREAD
C
C TO READ THE SWMM INTERFACE FILE USING SUBROUTINE INFACE
*	Allow user to have a standard ASCII table or *.csv file output
*
*	Subroutine Created by Steve Merrill/Brown & Caldwell
*	Revised 8/19/92 by Virgil Adderley/CH2M HILL
C=======================================================================
      INCLUDE 'TAPESIR.INC'
      INCLUDE 'INTER.INC'
      INCLUDE 'STIMER.INC'
C=======================================================================
**      DIMENSION JN(200),QO1(200)
      DIMENSION QO1(NIE)
      CHARACTER*32 FIN,FOUT
      CHARACTER BLANK*1,KPLACE*10,OUTNAM*32,ANS*1
      LOGICAL OK
*      DOUBLE PRECISION DELTA
      DATA BLANK/' '/
C=======================================================================
C
**      WRITE(*,50)
C======================================================================
* 50   FORMAT( ////,15X,' *****>>>>   S C R A T C H   <<<<*****'///,
*     +10X,'   By Brown & Caldwell/CH2M HILL, August, 1992'/,
*     +10X,'   For City of Portland, OR CSO Facilities Project'///)
C======================================================================
C
      N7=7
      NTAPE=15
      NPLACE=0
      KPLACE='0'
      ISDATE = 0
      STIME = 0
      WRITE(*,2000)
999   WRITE(*,100)
      READ(*,'(A)')FIN
      INQUIRE(FILE=FIN,EXIST=OK)
      IF(.NOT. OK)THEN
        WRITE(*,3000)
        GO TO 999
        ENDIF
      OPEN(NTAPE,FILE=FIN,FORM='UNFORMATTED')
      WRITE(*,200)
      READ(*,'(A)')FOUT
*
      WRITE(*,210)
      READ(*,'(I1)') ntype
*
      IF(FOUT .NE. BLANK)THEN
         N6=26
         OPEN(N6,FILE=FOUT)
*
***         IF(ntype.LE.1) WRITE(N6,60) FIN
***  60  FORMAT('"DATA FROM FILE", "',A8,'"')
         IF(ntype.GT.1) WRITE(N6,'(2A)')'DATA FROM FILE ',FIN
      ELSE
         N6=0
      ENDIF
C
C NOW CALL INFACE TO READ AND WRITE HEADER
C
      IDO=1
      CALL INFACEIR(IDO,NTAPE,FIN,ntype)
C
C NOW ASK FOR SPECIFIC LOCATIONS AND DATES
C
      MONS=0
      CALL IDATE(IDATEZ,MON,IDY,IYR)
500   WRITE(*,410)MON,IDY,IYR
      READ(*,'(A)')ANS
      IF(ANS.EQ.'Y' .OR. ANS.EQ.'y')THEN
           WRITE(*,420)
           READ(*,*)MONS,IDYS,IYRS
           WRITE(*,430)
           READ(*,*)MONF,IDYF,IYRF
           ISDATE=MDATE(IDYS,MONS,IYRS)
           IFDATE=MDATE(IDYF,MONF,IYRF)
           IF(ISDATE.LT.IDATEZ)ISDATE=IDATEZ
           IF(IFDATE.LT.ISDATE .OR. IFDATE.LT.IDATEZ)THEN
             WRITE(*,'(A)')' END DATE < STARTING DATE'
             GO TO 500
             ENDIF
*	   Provide time summary and prompt for beginning time:
           READ(NTAPE) JDAY,HOUR,DELTA,(QO1(J),J=1,LOCATS)
           IF(DELTA.LE.0) THEN
              READ(NTAPE)JDAY,HOUR,DELTA,(QO1(J),J=1,LOCATS)
              BACKSPACE(NTAPE)
           ENDIF
           WRITE(*,85) HOUR,DELTA
           BACKSPACE(NTAPE)
           READ(*,*) STIME
  85  FORMAT(5X,'Interface Beginning Time (seconds) = ',F8.0,/,
     &       5X,'Interface Time Step (seconds) = ',F8.0,/,
     &   5X,'Please Enter Time of Day (secs) to Start New File: ',\)
*     &   /,5X,'JDAY = ',I6,5X,'QO1 = ',F8.2,'  :  ')
*
           WRITE(*,*)
           WRITE(*,'(A\)')' NEW INTERFACE FILE NAME:  '
           READ(*,'(A)')OUTNAM
           OPEN(N7,FILE=OUTNAM,FORM='UNFORMATTED')
           ENDIF
C
99    IF(NJCE.EQ.0)THEN
        WRITE(*,250)(NLOC(I),I=1,LOCATS)
        WRITE(*,300)LOCATS
        READ(*,*)NPLACE
        ENDIF
      IF(NJCE.EQ.1)THEN

        WRITE(*,251)(KAN(I),I=1,LOCATS)
        WRITE(*,300)LOCATS
        READ(*,'(A)')KPLACE
        ENDIF
      IPLACE=LOCATS
 
      IF(NPLACE.GT.0 .OR. KPLACE.NE.'0')THEN
       IPLACE=1
       DO 1 I=1,LOCATS
         IF(NJCE.EQ.0 .AND. NLOC(I).EQ.NPLACE)GO TO 2
         IF(NJCE.EQ.1 .AND. KAN(I).EQ.KPLACE)GO TO 2
1      CONTINUE
       IF(NJCE.EQ.0)WRITE(*,350)NPLACE
       IF(NJCE.EQ.1)WRITE(*,351)KPLACE
       GO TO 99
2      NPRT=I
      ENDIF
C
C NOW GET A SKIP INTERVAL
C
 
      WRITE(*,400)
      READ(*,*)NSKIP
      IF(NSKIP.LT.1)NSKIP=1
C
      IF(FOUT .NE. BLANK) THEN
       IF(MONS.GT.0)THEN
        if(ntype.GT.1) WRITE(N6,5000)MONS,IDYS,IYRS
        ELSE
        CALL IDATE(IDATEZ,MON,IDY,IYR)
        if(ntype.GT.1) WRITE(N6,5000)MON,IDY,IYR
        ENDIF
C
       if(ntype.GT.1) then
        IF(NPLACE.GT.0 .OR. KPLACE.NE.'0')THEN
          IF(NJCE.EQ.0)WRITE(N6,115)NPLACE
          IF(NJCE.EQ.1)WRITE(N6,116)KPLACE
        ELSE
          IF(NJCE.EQ.0) WRITE(N6,110)(NLOC(I),I=1,LOCATS)
          IF(NJCE.EQ.1) WRITE(N6,111)(KAN(I),I=1,LOCATS)
        ENDIF
       endif
      ENDIF
C
C
C NOW READ THE FLOWS FROM THE INTERFACE FILE
C
C      IF(NSKIP.GT.1)NSKIP=NSKIP+1
c
      JULDAY=IDATEZ
      TIMDAY=TZERO
      QFLAG = 0
*      NNN=0
       NNN = NSKIP - 1
       IF(NNN.LT.0) NNN = 0
*
      DO 5 N=1,1000000000
        READ(NTAPE,ERR=10,END=10)JDAY,HOUR,DELTA,(QO1(J),J=1,LOCATS)
*         READ(NTAPE) JDAY, HOUR, DELTA, (QO1(J),J=1,LOCATS)
         IF(IFDATE.GT.0 .AND. JDAY.GT.IFDATE) GO TO 10
         IF(JDAY.GE.ISDATE)THEN
*
*	  Test for time of day to begin simulation:
          IF((JDAY.EQ.ISDATE).AND.(HOUR.LT.STIME)) GO TO 5

         NNN=NNN+1
         IF(MOD(NNN,NSKIP).EQ.0)THEN
*
*          Test if any significant flows have occurred yet:
*
           IF(QFLAG.LE.0) THEN
           DO 25 NL = 1, LOCATS
              IF(QO1(NL).GT.0.001) QFLAG=1
  25       CONTINUE
*		
*	Determine time and date of beginning of nonzero flows:
             BTIME = IFIX(HOUR+.5)
             JBDAY = JDAY
             CALL IDATE(JDAY,MON,IBDYS,IBYRS)
           ENDIF
*
*
           THOUR=IFIX(HOUR+.5)
c
c write new interface file header at time match
c
              DELTA=NSKIP*DELTA
              IF(NNN.LT.(NSKIP+1))THEN
                IF(ANS.EQ.'Y' .OR. ANS.EQ.'y')THEN
                  WRITE(N7)IPLACE,NQUAL
                  IF(NPLACE.GT.0 .OR. KPLACE.NE.'0')THEN
                    IF(NJCE.EQ.0)WRITE(N7)NPLACE
                    IF(NJCE.EQ.1)WRITE(N7)KPLACE
                  ELSE
                    IF(NJCE.EQ.0)WRITE(N7)(NLOC(I),I=1,LOCATS)
                    IF(NJCE.EQ.1)WRITE(N7)(KAN(I),I=1,LOCATS)
                  ENDIF
                  IDO=2
                  IDATEZ=JDAY
                  TZERO=THOUR
C
                  CALL INFACEIR(IDO,N7,OUTNAM,ntype)
                ENDIF  
              ENDIF
C
           JULDAY=JDAY
           TIMDAY=THOUR
C
           CALL IDATE(JDAY,MON,IDY,IYR)
*           HDAY=LDATE(JDAY)+THOUR/86400.
           IF(NPLACE.EQ.0 .AND. KPLACE.EQ.'0')THEN
              IF(ANS.EQ.'Y' .OR. ANS.EQ.'y')THEN
C							Write file only if a non-zero flow has occurred:
                IF(QFLAG.GT.0) WRITE(N7)JDAY,THOUR,DELTA,
     +                         (QO1(J),J=1,LOCATS)
              ENDIF

CCC              HOUR=HOUR/3600.
              IF(FOUT .NE. BLANK)THEN
                 IF(QFLAG.GT.0) WRITE(N6,150)JDAY,THOUR,delta,
     +                          (QO1(J),J=1,LOCATS)
              ELSE
                 IF(QFLAG.GT.0) WRITE(*,120)MON,IDY,IYR,
     +                          (QO1(J),J=1,LOCATS)
              ENDIF
          ELSE

              IF(ANS.EQ.'Y' .OR. ANS.EQ.'y')THEN
C							Write file only if a non-zero flow has occurred:
                  IF(QFLAG.GT.0) WRITE(N7)JDAY,THOUR,DELTA,QO1(NPRT)
*
*							Save final date & time written to file:
                       JFDAY = JDAY
                       FTIME = THOUR
*
             ENDIF
C
                IF(FOUT .NE. BLANK)THEN
                  IF(QFLAG.GT.0) WRITE(N6,150)JDAY,THOUR,delta,
     +                           QO1(NPRT)
                ELSE
                  IF(QFLAG.GT.0) WRITE(*,120)MON,IDY,IYR,HOUR,QO1(NPRT)
                ENDIF
           ENDIF
         ENDIF
       ENDIF
5     CONTINUE
C
10    CONTINUE
 
        WRITE(*,130)N-1
      IF(ANS.EQ.'Y' .OR. ANS.EQ.'y')WRITE(*,140)MONS,IBDYS,IYRS,BTIME
      IF(FOUT .NE. BLANK)THEN
      if(ntype.GT.1) WRITE(N6,130)N-1
           IF(ANS.EQ.'Y' .OR. ANS.EQ.'y') THEN
              WRITE(N6,140)MONS,IBDYS,IYRS,BTIME
*
*				     Calculate Duration of simulation:
*
                 NDAYS = (IYRS-IBYRS)*365 + (JFDAY-JBDAY-1)
                 IF(NDAYS.LT.0) DURAT = FTIME - BTIME
                 IF(NDAYS.GE.0) DURAT = NDAYS*86400. + FTIME + BTIME
              WRITE(N6,145) DURAT
           ENDIF
        ENDIF
      RETURN
C=======================================================================
100   FORMAT(2X,' PLEASE ENTER AN INTERFACE FILE NAME -> '\)
200   FORMAT(2X,' PLEASE ENTER A TEXT OUTPUT FILE NAME (Space for ',
     +'Screen)-> '\)
210   FORMAT(7X,'Please Select Output File Format:',/,12X,
     + 'Comma Separated Values (*.csv) ...... 1',/,12X,
     + 'ASCII Tables ........................ 2',/,12X,
     + 'Your Choice -> ',\)
250   FORMAT(5I10)
251   FORMAT(5A10)
300   FORMAT(' DATA SAVED FOR ABOVE ',I4,' INLETS'/
     +       '  ENTER ONE INLET TO PRINT (0 FOR ALL)-> '\)
350   FORMAT(/I10,' NOT FOUND')
351   FORMAT(/A10,' NOT FOUND')
400   FORMAT(/' NOW ENTER A SKIP INTERVAL (0 FOR ALL)-> '\)
410   FORMAT(//' BEGINNING DATE ON INTERFACE FILE IS ',I2,'/',I2,'/',
     + I4/' DO YOU WANT A SUBSET OF THE DATA? (Y/N)-> '\)
420   FORMAT(/' STARTING DATE? (M D Y)-> ')
430   FORMAT('   ENDING DATE? (M D Y)-> ')
C
**110   FORMAT(/' JDAY,  HOUR,  DELTA,  EDATE,',
**     +  250(I10,', ')/)
**111   FORMAT(/' JDAY,  HOUR,  DELTA,  EDATE,',
**     +  250(A10,', ')/)
110   FORMAT(/' JDAY,  HOUR,  DELTA',
     +  250(I10,', ')/)
111   FORMAT(/' JDAY,  HOUR,  DELTA',
     +  250(A10,', ')/)

CC110   FORMAT(/'      JDAY    HOUR   DELTA    EDATE     QOUT'/)
**115   FORMAT(/'    JDAY,   HOUR,  DELTA,   EDATE,    QOUT(',I10,')'/)
**116   FORMAT(/'    JDAY,   HOUR,  DELTA,   EDATE,    QOUT(',A10,')'/)
115   FORMAT(/'    JDAY,   HOUR,  DELTA,    QOUT(',I10,')'/)
116   FORMAT(/'    JDAY,   HOUR,  DELTA,    QOUT(',A10,')'/)
120   FORMAT(I4,'/',I2,'/',I4,F8.1,30F8.3,(32X,30F8.3))
130   FORMAT(/' TOTAL NUMBER OF TIME STEPS PROCESSED = ',I10)
140   FORMAT(/,' NEW INTERFACE FILE BEGINS ON ',I2,'/',I2,'/',I4,
     +        ' AT TIME = ',F7.0,' SECONDS')
145   FORMAT(/,' DURATION OF NEW INTERFACE FILE  = ',F12.0,' SECONDS')
CC150   FORMAT(' "',I2,'/',I2,'/',I2,', ',F9.2,', ',G10.4,', ',
CC     + 250(F8.3,', '))
* AMM 12/11/2000 Increased to 30000 values per line
150   FORMAT(I7,', ',F9.2,', ',F10.2,', ',F14.6,', ',
*     + 250(F8.3,', '))
     + 30000(F8.3,', '))
2000  FORMAT( /,' >>>>  ACCESS STANDARD SWMM INTERFACE FILES (JOUT):',
     + //)
3000  FORMAT(/' NAMED SCRATCH FILE NOT FOUND-TRY AGAIN')
5000  FORMAT(/' STARTING DATE IS: ',I2,'/',I2,'/',I4,//)
C=======================================================================
      END
