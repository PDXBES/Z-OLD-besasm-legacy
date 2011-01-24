      SUBROUTINE RUNDEPTH
C
C TO READ THE SWMM RUNOFF SCRATCH 7 FILE OF RUNOFF DEPTHS
*	Allow user to have a standard ASCII table or *.csv file output
*
*	Subroutine Created by Virgil Adderley/BES 5/97
C=======================================================================
      INCLUDE 'TAPESIR.INC'
      INCLUDE 'INTER.INC'
      INCLUDE 'STIMER.INC'
C=======================================================================
      DIMENSION GDEPTH(NIE)
      CHARACTER*240 FIN,FOUT
      CHARACTER BLANK*1
      INTEGER MDEEP
      REAL GDEPTH
      LOGICAL OK
      DATA BLANK/' '/
C=======================================================================
      NTAPE=5
*
      WRITE(*,2000)
999   WRITE(*,100)
      READ(*,'(A)')FIN
      INQUIRE(FILE=FIN,EXIST=OK)
      IF(.NOT. OK)THEN
        WRITE(*,3000)
        GO TO 999
        ENDIF
      OPEN(NTAPE,FILE=FIN,FORM='UNFORMATTED')
      REWIND(NTAPE)
*
      WRITE(*,200)
      READ(*,'(A)')FOUT
      IF(FOUT .NE. BLANK)THEN
         N6=6
         OPEN(N6,FILE=FOUT)
      ELSE
         N6=0
      ENDIF
*
*
      WRITE(*,250)
      READ(*,'(I4)') MDEEP
*
      WRITE(N6,115)
*
      DO 5 N=1,100000000
        READ(NTAPE,ERR=10,END=10)JDAY,HOUR,DELTA,(GDEPTH(J),J=1,MDEEP)
           TIMDAY=IFIX(HOUR+.5)
           WRITE(N6,150)JDAY,TIMDAY,DELTA,(GDEPTH(J),J=1,MDEEP)
5     CONTINUE
10    CONTINUE
      CLOSE(N6)
      RETURN
C=======================================================================
100   FORMAT(2X,' PLEASE ENTER A RUNOFF SCRATCH FILE #7 NAME -> '\)
200   FORMAT(2X,' PLEASE ENTER A TEXT OUTPUT FILE NAME (Space for ',
     +'Screen)-> '\)
250   FORMAT(/,/,2X,' Please Enter the Number of Gutters Print ',
     +'(MDEEP on M4 Card) -> '\)
115   FORMAT(/' "JDAY", "HOUR", "DELTA", "GUTTERS #1 to #MDEEP"'/)
150   FORMAT(I7,', ',F9.2,', ',F10.2,', ',F14.6,', ',
     + 250(F12.3,', '))
2000  FORMAT( /,' >>>>  ACCESS RUNOFF SCRATCH FILE #7 (NSCRAT(7)):',
     + //)
3000  FORMAT(/' NAMED SCRATCH FILE NOT FOUND-TRY AGAIN')
C=======================================================================
      END
