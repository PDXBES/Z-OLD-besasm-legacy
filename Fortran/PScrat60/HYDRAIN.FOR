      SUBROUTINE HYDRAIN
C======================================================================
C     ROUTINE READS AND CONVERTS PORTLAND HYDRA RAIN FILES
C     HYDRA FILE STRUCTURE IS:
C     GAUGE,YEAR,JULIAN DAY,HOUR,INCREMENT OF HOUR, TENTHS OF RAIN IN INCREMENT
C     FORMAT IS 2I2,I3,2I2,I3
C     INCREMENT OF HOUR IS THE NUMBER OF THE 60/DT INCREMENTS,
C       E.G. THERE ARE 12 INCREMENTS FOR 5 MIN DATA, 4 INCREMENTS FOR 15M.
C
C======================================================================
      CHARACTER*48 FIN,FOUT,OUTNAM
      CHARACTER ANS*1
      CHARACTER*80 TITLE
      LOGICAL OK
C======================================================================
      NTAPE=50
      WRITE(*,2000)
999   WRITE(*,100)
      READ(*,'(A)')FIN
      INQUIRE(FILE=FIN,EXIST=OK)
      IF(.NOT. OK)THEN
        WRITE(*,3000)
        GO TO 999
        ENDIF
*
*			Request type of file:  RAIN_BATCH or COLUMN FORMAT
      WRITE(*,210)
      READ(*,'(I1)') ntype
*
*
*			Request type of RAIN:  DEPTHS OR INTENSITIES
      WRITE(*,250)
      READ(*,'(I1)') ndepth
*
*
      OPEN(NTAPE,FILE=FIN)
*
*
      WRITE(*,200)
      READ(*,'(A)')FOUT
      IF(FOUT .NE. ' ')THEN
         N6=60
         OPEN(N6,FILE=FOUT)
      ELSE
         N6=0
      ENDIF
      N7=7
      WRITE(*,500)
      READ(*,'(A)')OUTNAM
      IF(OUTNAM.EQ.' ')THEN
       N7=0
       ELSE
        OPEN(N7,FILE=OUTNAM,FORM='UNFORMATTED')
       ENDIF
        WRITE(*,'(A\)')'   Pleae Enter the Time Step Base for the ',
     +  ' input file (minutes) -> '
        READ(*,*)ITIM
        NTEMP = 18
        OPEN(NTEMP,FILE='DEBUG.OUT',FORM='FORMATTED')
*
        IF(ntype.GT.2) THEN
*
*			Old Hydra Format:
*           READ(NTAPE,600,END=1000,ERR=666)JSTA,IYR,JEWEL,IHR,INC,IHYE
           READ(NTAPE,600,END=1000)JSTA,IYR,JEWEL,IHR,INC,IHYE
*
        ELSEIF(ntype.EQ.2) THEN
*			Year,JDay,Hour,Minute,Depth
           READ(NTAPE,*,END=1000)IYR,JEWEL,IHR,MIN,IHYE
                JSTA = 1
                HOUR = IHR*3600 + MIN*60
                INC = (MIN + 1)/15
           WRITE(NTEMP,*) IYR,JEWEL,IHR,MIN,IHYE,HOUR,INC
*
        ELSE
*
*              RAIN_BATCH Format:
*
           DO 60 NT = 1, 24
              READ(NTAPE,65,ERR=666) title
   60      CONTINUE
*
   65       FORMAT(A80)
*
*            READ(NTAPE,*,END=1000,ERR=666) NYR,JEWEL,FDAY,IHR,MIN,IHYE
            READ(NTAPE,*,END=1000) NYR,JEWEL,FDAY,IHR,MIN,IHYE
*
                 JSTA = 1
                 IYR = NYR - 1900
*	FDAY is not sufficiently accurate: Use Hour & Min
**                 HOUR = FDAY * 86400
                 HOUR = IHR*3600 + MIN*60
                 IF(ITIM.GT.1) INC = (MIN+1)/ITIM
                 IF(ITIM.LE.1) INC = 1
*
*
        ENDIF
*
        JULDAY=IYR*1000+JEWEL
        BACKSPACE NTAPE
        CALL IDATE(JULDAY,MONTH,IDAY,IYR)
30      WRITE(*,2100)MONTH,IDAY,IYR
        IYRS=0
        IYRF=0
        READ(*,'(A)')ANS
         IF(ANS.EQ.'Y' .OR. ANS.EQ.'y')THEN
           WRITE(*,'(A\)')' STARTING DATE (M D Y): '
           READ(*,*)MONS,IDYS,IYRS
           WRITE(*,'(A\)')'   ENDING DATE (M D Y): '
           READ(*,*)MONF,IDYF,IYRF
         ENDIF
           IF(IYRS.GT.0)THEN
             ISDATE=MDATE(IDYS,MONS,IYRS)
             IF(ISDATE.LT.JULDAY)THEN
              WRITE(*,'(A)')' START DATE NOT ON FILE'
              GO TO 30
              ENDIF
             ELSE
               ISDATE=JULDAY
               IYRS=IYR
               ENDIF
           IF(IYRF.GT.0)THEN
              IFDATE=MDATE(IDYF,MONF,IYRF)
              IF(IFDATE.LT.JULDAY .OR. IFDATE.LT.ISDATE)THEN
                WRITE(*,'(A/)')' ENDING DATE < STARTING DATE'
                GO TO 30
                ENDIF
              ELSE
                IFDATE=10000000
                ENDIF
C
C COMPUTE SERIAL NUMBER OF STARTING DATE (FOR LOTUS)
C
         LOTUS=0
         DO 5 I=1,IYRS
           IF(MOD(I-1,4).EQ.0)THEN
             NDAYS=366
             ELSE
              NDAYS=365
              ENDIF
           LOTUS=LOTUS+NDAYS
5        CONTINUE
         LOTUS=LOTUS+(ISDATE-IYRS*1000)
C
         NSTA=1
         MRAIN=1000000
         THISTO=ITIM*60.
         TIME=60./FLOAT(ITIM)
         IF(N7.GT.0)WRITE(N7,ERR=20)NSTA,MRAIN,JSTA
         JDAY=ISDATE
           DO 10 I=1,1000000
*
        IF(ntype.GT.2) THEN
*
**           READ(NTAPE,600,END=1000,ERR=666)JSTA,IYR,JEWEL,IHR,INC,IHYE
           READ(NTAPE,600,END=1000)JSTA,IYR,JEWEL,IHR,INC,IHYE
*
        ELSEIF(ntype.EQ.2) THEN
*			Year,JDay,Hour,Minute,Depth
           READ(NTAPE,*,END=1000)IYR,JEWEL,IHR,MIN,IHYE
                JSTA = 1
                HOUR = IHR*3600 + MIN*60
                INC = (MIN + 1)/15
           WRITE(NTEMP,*) IYR,JEWEL,IHR,MIN,IHYE,HOUR,INC

*
        ELSE
*
**             READ(NTAPE,*,END=1000,ERR=666) NYR,JEWEL,FDAY,IHR,MIN,IHYE
             READ(NTAPE,*,END=1000) NYR,JEWEL,FDAY,IHR,MIN,IHYE
*
                 IYR = NYR - 1900
*	FDAY is not sufficiently accurate: Use Hour & Min
**                 HOUR = FDAY * 86400
                 HOUR = IHR*3600 + MIN*60
                 IF(ITIM.GT.1) INC = (MIN+1)/ITIM
                 IF(ITIM.LE.1) INC = 1
*
        ENDIF
*
*
            JULDAY=IYR*1000+JEWEL
            IF(JULDAY.GE.ISDATE .AND. JULDAY.LE.IFDATE)THEN
*
*		TEST FOR DEPTH OR INTENSITY UNITS:
*
           IF(ndepth.LE.1) THEN
*	Depth Units:
              REIN=FLOAT(IHYE)/100.*TIME
*
           ELSE
*	Intensity Units:
              REIN=FLOAT(IHYE)/100.
*
           ENDIF
*
*
             IF(NTYPE.GT.2) HOUR = IHR*3600 + INC*ITIM*60
*
             IF(HOUR.GT.86400.) THEN
              WRITE(*,*) 'ERROR IN INPUT FILE:  TIME IS NOT CORRECT!'
              WRITE(*,*) 'HOUR =', HOUR, 'IHR =', IHR, 'INC =',INC,
     +         ' ITIM =', ITIM
                STOP
              ENDIF
              IF(N7.GT.0)THEN
                WRITE(N7,ERR=20)JULDAY,HOUR,THISTO,REIN
                ENDIF
              CALL IDATE(JULDAY,MONTH,IDAY,IYR)
              IF(JULDAY.GT.JDAY)THEN
               IF(JULDAY/1000.GT.JDAY/1000)THEN
                 IYRL=JDAY/1000
                 IF(MOD(IYR,4).EQ.0)THEN
                   LOTUS=LOTUS+(IYRL*1000+366-JDAY+(JULDAY-IYR*1000))
                   ELSE
                   LOTUS=LOTUS+(IYRL*1000+365-JDAY+(JULDAY-IYR*1000))
                   ENDIF
                ELSE
                LOTUS=LOTUS+(JULDAY-JDAY)
                ENDIF
                JDAY=JULDAY
                ENDIF
              IF(N6.GT.0)THEN
*               WRITE(N6,700)LOTUS,HOUR,REIN
                WRITE(N6,700)JULDAY,HOUR,REIN

               ELSE
                WRITE(*,800)MONTH,IDAY,IYR,HOUR,REIN
                ENDIF
            ENDIF
10         CONTINUE
1000  WRITE(*,*)
      WRITE(*,'(A)') ' SUCCESSFUL CONVERSION OF RAINFALL FILE.'
      RETURN
20    WRITE(*,'(A)')' ERROR WRITING INTERFACE FILE - STOP PROGRAM!'
      STOP
666   WRITE(*,'(A)')' ERROR READING RAINFALL FILE - STOP PROGRAM!'
      STOP
C======================================================================
100   FORMAT(2X,' Please Enter a HYDRA Rainfall File Name -> '\)
200   FORMAT(2X,' Please Enter a Text Output File Name (Space for ',
     +'Screen)-> '\)
*
210   FORMAT(7X,'Please Select Input File Format:',/,12X,
     + 'RAIN_BATCH File (from BES VAX) ....... 1',/,12X,
     + 'Delimited Year,JDay,Hour,Min,Depth.... 2',/,12X,
     + 'Undeleminted Fixed Column Format...... 3',/,12X,
     + 'Your Choice -> ',\)
*
*
250   FORMAT(/,7X,'Is Rain Data in Units of Depth or Intensity?',/,12X,
     + 'Depth Units:  (Total inches over time step) ...... 1',/,12X,
     + 'Intensity Units:  (Inches per hour) .............. 2',/,12X,
     + 'Your Choice -> ',\)
*
350   FORMAT(/I10,' NOT FOUND')
500   FORMAT(3X,'Please Enter the name of the Rainfall Interface File',
     + ' to be created -> '\)
600   FORMAT(I2,I2,I3,2I2,I3)
700   FORMAT(I10,F8.0,F8.3)
800   FORMAT(I3,'/',I2,'/',I2,F8.0,F8.3)
2000  FORMAT( /,' >>>>  READING AND CONVERTING HYDRA RAIN FILE '//)
2100  FORMAT(/' STARTING DATE ON RAIN FILE IS: ',I2,'/',I2,'/',I2/
     + '    DO YOU WISH A SUBSET OF THE FILE? (Y/N)-> '\)
3000  FORMAT(/' NAMED FILE NOT FOUND-TRY AGAIN')
C======================================================================      
      END
