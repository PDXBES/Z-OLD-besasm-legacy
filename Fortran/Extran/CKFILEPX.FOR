      SUBROUTINE CKFILE(IFILE)
      INCLUDE 'TAPES.INC'
      CHARACTER FNAME*60,BLANK*60,FMT*60,FORM*60
      DATA BLANK/' '/
      DATA FMT/'('' ENTER OUTPUT FILENAME FOR STORM EVENT DATA: '')'/
C=======================================================================
C     OPEN A FORMATTED OUTPUT FILE
C=======================================================================
      IF(JKP(51).EQ.0) THEN
                       FORM = FMT
100                    WRITE(*,FORM) 
                       READ(*,99) FNAME
                       IF(FNAME.EQ.BLANK) GO TO 100
                       JKP(51) = 1
                       OPEN(IFILE,FILE=FNAME,STATUS='UNKNOWN')                         
                    ELSE
                       INQUIRE(IFILE,NAME=FNAME)
                       CLOSE(IFILE)
              OPEN(IFILE,FILE=FNAME,FORM='FORMATTED',STATUS='UNKNOWN')
                       ENDIF 
99    FORMAT(A)
      RETURN
      END      
      
