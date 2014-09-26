      SUBROUTINE WRTFLOW
c  stores flows to sumflow array
c  writes average flow for previous time step to file when required
cim   3/97
      INCLUDE 'TAPES.INC'
      INCLUDE 'STIMER.INC'
      INCLUDE 'PIPE.INC'
      INCLUDE 'CWRITE.INC'
      LOGICAL NONZERO
      IOFLOW = IOFLOW + 1
      DO 100 N=1,NOFLOW
      L = FLOWOUT(N)
 100  SUMFLW(N) = SUMFLW(N) + Q(L)
cim      write(nfout,*) (q(flowout(n)),n=1,noflow)
CIM
cim  WRITE AND RESET IF DESIRED
cim
      IF (IOFLOW.GE.IFINTER) THEN
      NONZERO = .FALSE.
      DO 150 N=1,NOFLOW
      SUMFLW(N) = SUMFLW(N)/IOFLOW
 150  IF (SUMFLW(N).GT.FLOWMIN) NONZERO = .TRUE.
      IF (NONZERO) THEN
      IF (.NOT.WRITING) THEN
c write a zero flow record -FLWSTEP before this time.
       IF(IFLBIN.EQ.0) THEN
       WRITE(NFOUT,7000) IMONTH,INDAY,INYEAR,PTIMDAY/60.0/60.0,
     +                   DRYFLWSTEP,(0.0,N=1,NOFLOW)
      ELSE
       WRITE(NFOUT) IJULDAY,PTIMDAY,DRYFLWSTEP,(0.0,N=1,NOFLOW)
      ENDIF
      DRYFLWSTEP = 0.0
      WRITING = .TRUE.
      ENDIF
      IF(IFLBIN.EQ.0) THEN
       WRITE(NFOUT,7000) MONTH,NDAY,NYEAR,TIMDAY/60.0/60.0,FLWSTEP,
     +                               (SUMFLW(N),N=1,NOFLOW)
      ELSE
       WRITE(NFOUT) JULDAY,TIMDAY,FLWSTEP,(SUMFLW(N),N=1,NOFLOW)
      ENDIF
      ELSE IF (WRITING) THEN
C  Write zero flow record at end of positive flow period
      IF(IFLBIN.EQ.0) THEN
       WRITE(NFOUT,7000) MONTH,NDAY,NYEAR,TIMDAY/60.0/60.0,FLWSTEP,
     +                               (0.0,N=1,NOFLOW)
      ELSE
       WRITE(NFOUT) JULDAY,TIMDAY,FLWSTEP,(0.0,N=1,NOFLOW)
      ENDIF
      WRITING = .FALSE.
      DRYFLWSTEP = 0.0
      IMONTH=MONTH
      INDAY=NDAY
      INYEAR=NYEAR
      PTIMDAY=TIMDAY
      IJULDAY=JULDAY
      ELSE
C  Save previous time step values
      DRYFLWSTEP = DRYFLWSTEP + FLWSTEP
      IMONTH=MONTH
      INDAY=NDAY
      INYEAR=NYEAR
      PTIMDAY=TIMDAY
      IJULDAY=JULDAY
      ENDIF
      DO 500 N=1,NOFLOW
 500  SUMFLW(N) = 0.0
      IOFLOW = 0
      ENDIF
cim
      RETURN
 7000 FORMAT(I3,I3,I5,F7.3,F7.0,1PE14.4,200E14.4)
      END
