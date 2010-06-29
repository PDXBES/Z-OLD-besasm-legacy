      SUBROUTINE RAINRD
C======================================================================
C ROUTINE TO READ THE RAINFALL FILE FROM THE RAIN BLOCK
C
      CHARACTER*240 FIN,FOUT,OUTNAM
      CHARACTER ANS*1
*      CHARACTER*10 KAN(200)
      LOGICAL OK
      DIMENSION JSTA(200),REIN(100)
C======================================================================

      NTAPE=5
      NPLACE=0
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
      IF(FOUT .NE. ' ')THEN
         N6=6
         OPEN(N6,FILE=FOUT)
      ELSE
         N6=0
      ENDIF
C
C NOW READ THE FLOWS FROM THE INTERFACE FILE
C
      READ(NTAPE,ERR=10)NSTA,MRAIN,(JSTA(I),I=1,NSTA)
      IF(NSTA.GT.1)THEN
3      WRITE(*,250)(JSTA(J),J=1,NSTA)
       WRITE(*,300)
       READ(*,*)NPLACE
       IF(NPLACE.GT.0)THEN
        DO 1 I=1,NSTA
          IF(JSTA(I).EQ.NPLACE)GO TO 2
1       CONTINUE
        WRITE(*,350)NPLACE
        NPLACE=0
        GO TO 3
2       NPRT=I
        WRITE(N6,1000)NPRT
       ELSE
        WRITE(N6,1000)(JSTA(I),I=1,NSTA)
       ENDIF
      ENDIF
C
C NOW FIND OUT IF A SUBSET IS DESIRED
C
      READ(NTAPE,ERR=10,END=9) JDAY,HOUR,THISTO,(REIN(J),J=1,NSTA)
      BACKSPACE NTAPE
         CALL IDATE(JDAY,MON,IDY,IYR)
         HOUR=HOUR/3600.
30       WRITE(*,2100)MON,IDY,IYR
         READ(*,'(A)')ANS
         IF(ANS.EQ.'Y' .OR. ANS.EQ.'y')THEN
           WRITE(*,'(A\)')' Starting Date?  (M D Y) - > '
           READ(*,*)MONS,IDYS,IYRS
           WRITE(*,'(A\)')' Ending Date?  (M D Y) - > '
           READ(*,*)MONF,IDYF,IYRF
           WRITE(*,'(A\)')' New Rainfall Filename? - > '
           READ(*,'(A)')OUTNAM
           OPEN(7,FILE=OUTNAM,FORM='UNFORMATTED')
           IF(NPLACE.EQ.0)THEN
             WRITE(7)NSTA,MRAIN,(JSTA(I),I=1,NSTA)
            ELSE
             ISTA=1
             WRITE(7)ISTA,MRAIN,JSTA(NPRT)
            ENDIF
           IF(IYRS.GT.0)THEN
             ISDATE=MDATE(IDYS,MONS,IYRS)
             ELSE
               ISDATE=0
               ENDIF
           IF(IYRF.GT.0)THEN
              IFDATE=MDATE(IDYF,MONF,IYRF)
              IF(IFDATE.LT.JDAY .OR. IFDATE.LT.ISDATE)THEN
                WRITE(*,'(A/)')' ENDING DATE < STARTING DATE'
                WRITE(*,1001) JDAY, ISDATE, IFDATE
 1001  FORMAT( ' JDAY = ',I8,/,' ISDATE = ',I8,/,' IFDATE = ',I8,/)
                GO TO 30
                ENDIF
              ELSE
                IFDATE=0
                ENDIF
C
C NOW SEARCH RAINFILE FOR STARTING DATE AND MAKE SURE THAT THE FIRST
C     RECORD OF NEW RAINFILE IS THE BEGINNING OF THE DAY.
C
           DO 35 N=1,1000000
            READ(NTAPE,ERR=10,END=40)JDAY,HOUR,THISTO,(REIN(J),J=1,NSTA)
            IF(JDAY.GE.ISDATE)THEN
              BACKSPACE NTAPE
C              IF(HOUR.GT.0)THEN
C                HOUR=0
C                IF(NPLACE.EQ.0)THEN
C                  DO 31 J=1,NSTA
C31                  REIN(J)=0
C                  WRITE(7)JDAY,HOUR,THISTO,(REIN(J),J=1,NSTA)
C                  ELSE
C                   WRITE(7)JDAY,HOUR,THISTO,HOUR
C                  ENDIF
C                ENDIF
                GO TO 45
            ENDIF
35         CONTINUE
C     ISDATE NOT FOUND ON RAINFILE
40         WRITE(*,'(/A)')' STARTING DATE > END DATE ON FILE'
           WRITE(*,1001) JDAY, ISDATE, IFDATE
           REWIND NTAPE
           GO TO 30
         ELSE
            ISDATE=0
            IFDATE=0
         ENDIF
45    CONTINUE
      
      DO 8 N=1,1000000
        READ(NTAPE,ERR=10,END=9)JDAY,HOUR,THISTO,(REIN(J),J=1,NSTA)
        IF(IFDATE.GT.0 .AND. JDAY.GT.IFDATE)GO TO 9
          CALL IDATE(JDAY,MON,IDY,IYR)
          IF(NPLACE.EQ.0)THEN
           IF(JDAY.GE.ISDATE)THEN
            IF(IFDATE.EQ.0 .OR. JDAY.LE.IFDATE)THEN
              IF(ANS.EQ.'Y' .OR. ANS.EQ.'y')THEN
                WRITE(7)JDAY,HOUR,THISTO,(REIN(J),J=1,NSTA)
                ENDIF
CC              HOUR=HOUR/3600.
              WRITE(N6,120)MON,IDY,IYR,HOUR,THISTO,(REIN(J),J=1,NSTA)
              ENDIF
              ENDIF
          ELSE
           IF(JDAY.GE.ISDATE)THEN
            IF(IFDATE.EQ.0 .OR. JDAY.LE.IFDATE)THEN
              IF(ANS.EQ.'Y' .OR. ANS.EQ.'y')THEN
                WRITE(7)JDAY,HOUR,THISTO,REIN(NPRT)
                ENDIF
CC              HOUR=HOUR/3600.
              WRITE(N6,120)MON,IDY,IYR,HOUR,THISTO,REIN(NPRT)
              ENDIF
              ENDIF
          ENDIF
8     CONTINUE
9     CONTINUE
C      IF(ANS.EQ.'Y' .OR. ANS.EQ.'y')THEN
C        WRITE(*,2200)
C        ENDIF
      RETURN
10    WRITE(*,'(A)')' ===>ERROR IN READING SCRATCH FILE'
      WRITE(N6,'(A)')' ===>ERROR IN READING SCRATCH FILE'
      STOP
C======================================================================      
100   FORMAT(' Please Enter an Rainfall Interface Filename -> '\)
200   FORMAT(' Please Enter a Text Output Filename -> '\)
400   FORMAT(/' Now Enter a Skip Interval (0 for all) -> '\)
300   FORMAT(/' DATA SAVED FOR ABOVE STATIONS'/
     +       '  GIVE ME ONE TO PRINT (0 FOR ALL)-> '\)
350   FORMAT(/I10,' NOT FOUND')
250   FORMAT(10I10)
1000  FORMAT('   RAINFALL BY STATION'//
     + ' STATION->',14X,40I6,/)
120   FORMAT(I4,'/',I2,'/',I2,F8.1,F7.1,40F6.2,(25X,40F6.2))
2000  FORMAT(/' >>>>  READING SWMM4 RAINFALL INTERFACE FILE'//)
2100  FORMAT(/' Starting Date on Rainfall File Is: ',I2,'/',I2,'/',I2/
     + '    Do you wish a subset of the File? (Y/N)-> '\)
2200  FORMAT(/' Finished Translation.  New Rainfall File Begins ',
     + 'at time 0.00 on Specified Start Date')
3000  FORMAT(/' NAMED SCRATCH FILE NOT FOUND-TRY AGAIN')
C======================================================================      
      END
