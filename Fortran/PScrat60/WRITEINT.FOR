      SUBROUTINE WRITEINT
      INCLUDE 'TAPESIR.INC'
      INCLUDE 'INTER.INC'
      DOUBLE PRECISION DELTA, Q(NIE), POLL(4,10), TimDay
      CHARACTER*240  filein, filout
	CHARACTER*48station
C
*****************************************************************************
*
      WRITE(*,2000)

C
C     Get input file
C
       WRITE(*,10)
   10  FORMAT( /,2X,'PLEASE TYPE THE NAME OF THE ASCII DATA FILE:  ',\)
       READ(*,15) filein
C
   15  FORMAT(A240)
************************
C
      ntype = 1
      OPEN(1,FILE=filein)
      REWIND(1)
C
C     Get output file name and type:
C
       WRITE(*,25)
   25  FORMAT( //,2X,'PLEASE ENTER THE INTERFACE FILE NAME TO BE',
     +       ' CREATED:  ',\)
       READ(*,15) filout
C
*
           OPEN(2,FILE=filout,FORM='BINARY')
*
*
******************************************************************************
C      
*     Read & Write the header lines of the interface file for each type:
      IF(ntype.LE.1) THEN
C
        READ(1,*) title(1)
        READ(1,*) title(2)
*
        READ(1,*) idatez, tzero
*
        READ(1,*) title(3)
        READ(1,*) title(4)
*
*
        READ(1,*) source, locats, NQUAL, triba, njce
        IF(NJCE.LE.0) READ(1,*) (nloc(K), K=1, locats)
        IF(NJCE.GE.1) READ(1,*) (KAN(K), K=1, locats)
        IF(NQUAL.GT.0) READ(1,*)  (pname(J),J=1,NQUAL)
        IF(NQUAL.GT.0) READ(1,*)  (punit(J),J=1,NQUAL)
        IF(NQUAL.GT.0) READ(1,*)  (ndim(J),J=1,NQUAL)
        READ(1,*) qconv
        
      ENDIF
**** VCA 8/98:  Force WriteInt to write XP-Compatible format
**
       IF(NJCE.LT.1) THEN
          triba = -1*triba
          njce = 1
          DO 65 K=1,locats
             write(KAN(K),64) NLOC(K)
  64  FORMAT(I10)
**             WRITE(*,*) '  NLOC= ',NLOC(K),'  KAN= ',KAN(K),' K=',K
  65      CONTINUE
       ENDIF
*
          WRITE(2) title(1)
          WRITE(2) title(2)
*
          WRITE(2) idatez, tzero
*
          WRITE(2) title(3)
          WRITE(2) title(4)
*
*
**          WRITE(2) source, locats, NQUAL, triba, njce
          WRITE(2) source, locats, NQUAL, triba
**          IF(NJCE.LE.0) WRITE(2) (nloc(K), K=1, locats)
          IF(NJCE.GE.1) WRITE(2) (KAN(I),I=1,LOCATS)
          IF(NQUAL.GT.0) WRITE(2)  (pname(J),J=1,NQUAL)
          IF(NQUAL.GT.0) WRITE(2)  (punit(J),J=1,NQUAL)
          IF(NQUAL.GT.0) WRITE(2)  (ndim(J),J=1,NQUAL)
*
          WRITE(2) qconv
*
***************************************************************************
*
*	Set up DO WHILE to loop through file and read in each line:
*
C
        DO WHILE(nerr.EQ.0)

*** Istep = 1, 540000
C

*	Test if there is any pollutant data to read in, else read in flow data:
*
       IF(NQUAL.GT.0) THEN
*
            READ(1,*,IOSTAT=nerr)  julday, timday, DELTA,
     +            (q(k), (poll(J,K),J=1,NQUAL), K=1,locats)
*
*
            WRITE(2) julday, timday, DELTA, (Q(K),
     &               (poll(J,K),J=1,NQUAL), K=1,locats)
*
       ELSE 
             READ(1,*,IOSTAT=nerr)  julday, timday, DELTA,
     &                                     (Q(K),K=1,locats)
        
             WRITE(2) julday, timday, DELTA,(Q(K),K=1,locats)
*
      ENDIF
*
*
 1000  END DO
*
C
C**********************************************************************
C         The following is the read error statement.
C**********************************************************************
C
*** 3000  IF((nerr.EQ.-1).OR.(nerr.EQ.6505).OR.(nerr.EQ.0))THEN
C
C**********************************************************************
C         An expected end of error, the program has successfully 
C         finished the transformation.
C**********************************************************************
C
          WRITE(*,3110)
***      ELSE
C
C**********************************************************************
C         Unexpected error occurred, write message plus the error number 
C**********************************************************************

***          WRITE(*,3120) nerr
***      ENDIF
C
C**********************************************************************
*
2000  FORMAT( /,' >>>>  CREATE STANDARD SWMM INTERFACE FILES (JIN):',
     + //)
C
 3110  FORMAT(/,5x,'>>>  File Transformation Complete  <<<')
C
 3120  FORMAT(//,5x,'==>> UNKNOWN ERROR OCCURRED:  Error # ',I4,/)
C
*
C
       CLOSE(1)
       CLOSE(2)
C
C
       STOP
       END
