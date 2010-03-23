      SUBROUTINE INFACE(IDO,NTAPE)
C=======================================================================
C     This subroutine reads or writes the header information
C     for a SWMM 4.05 interface file.  Created FEBRUARY, 1987 
C     by Robert E. Dickinson.  Last updated December, 1990
C=======================================================================      
      INCLUDE 'TAPES.INC'
      INCLUDE 'INTER.INC'
      CHARACTER KJN*10
      DIMENSION JN(NIE),KJN(NIE)
C=======================================================================
C     Read interface headers.
C=======================================================================
      IF(IDO.EQ.0) THEN
                   REWIND NTAPE
                   READ(NTAPE,ERR=999) TITLE(1)
                   READ(NTAPE,ERR=999) TITLE(2)
                   READ(NTAPE,ERR=999) IDATEZ,TZERO
                   READ(NTAPE,ERR=999) TITLE(3)
                   READ(NTAPE,ERR=999) TITLE(4)
                   READ(NTAPE,ERR=999) SOURCE,LOCATS,NQUAL,TRIBA,NJCE
                   IF(NJCE.EQ.0) READ(NTAPE,ERR=999)(NLOC(I),I=1,LOCATS)
                   IF(NJCE.EQ.1) READ(NTAPE,ERR=999)(KAN(I),I=1,LOCATS)
                   IF(NQUAL.GT.0) THEN
                            READ(NTAPE,ERR=999) (PNAME(J),J=1,NQUAL)
                            READ(NTAPE,ERR=999) (PUNIT(J),J=1,NQUAL)
                            READ(NTAPE,ERR=999) (NDIM(J),J=1,NQUAL)
                            ENDIF
                   READ(NTAPE,ERR=999) QCONV
                   ENDIF
C=======================================================================
C     Read and write interface headers.
C=======================================================================
      IF(IDO.EQ.1) THEN
                   REWIND NTAPE
                   READ(NTAPE,ERR=999) TITLE(1)
                   READ(NTAPE,ERR=999) TITLE(2)
                   READ(NTAPE,ERR=999) IDATEZ,TZERO
                   READ(NTAPE,ERR=999) TITLE(3)
                   READ(NTAPE,ERR=999) TITLE(4)
                   READ(NTAPE,ERR=999) SOURCE,LOCATS,NQUAL,TRIBA,NJCE
                   WRITE(N6,1)  TITLE(1),TITLE(2)
                   WRITE(N6,2)  TITLE(3),TITLE(4)
                   WRITE(N6,3)  SOURCE
                   WRITE(N6,4)  IDATEZ,TZERO
                   WRITE(N6,5)  LOCATS,NQUAL,TRIBA,NJCE
C=======================================================================
C     Read sequence of location numbers.
C=======================================================================
                   IF(NJCE.EQ.0) THEN
                      READ(NTAPE,ERR=999) (NLOC(I),I=1,LOCATS)
                      WRITE(N6,6) (NLOC(I),I=1,LOCATS)
                      ELSE
                      READ(NTAPE,ERR=999)  (KAN(I),I=1,LOCATS)
                      WRITE(N6,66) (KAN(I),I=1,LOCATS)
                      ENDIF
                   IF(NQUAL.GT.0) THEN
                            READ(NTAPE,ERR=999)   (PNAME(J),J=1,NQUAL)
                            READ(NTAPE,ERR=999)   (PUNIT(J),J=1,NQUAL)
                            READ(NTAPE,ERR=999)   (NDIM(J),J=1,NQUAL)
                            WRITE(N6,7) (J,PNAME(J),PUNIT(J),
     +                                    NDIM(J),J=1,NQUAL)
                            ENDIF
                   READ(NTAPE,ERR=999)  QCONV
                   WRITE(N6,8)          QCONV
                   ENDIF
C=======================================================================
C     Write interface headers.
C=======================================================================
      IF(IDO.EQ.2) THEN
                   REWIND NTAPE
                   READ(NTAPE,ERR=999) NEWOUT,NPOLL
                   IF(JCE.EQ.0) READ(NTAPE,ERR=999)  (JN(I),I=1,NEWOUT)
                   IF(JCE.EQ.1) READ(NTAPE,ERR=999) (KJN(I),I=1,NEWOUT)
                   REWIND NTAPE
                   WRITE(NTAPE,ERR=998) TITLE(1)
                   WRITE(NTAPE,ERR=998) TITLE(2)
                   WRITE(NTAPE,ERR=998) IDATEZ,TZERO
                   WRITE(NTAPE,ERR=998) TITLE(3)
                   WRITE(NTAPE,ERR=998) TITLE(4)
                   WRITE(NTAPE,ERR=998) SOURCE,NEWOUT,NPOLL,TRIBA,JCE
                   IF(JCE.EQ.0) WRITE(NTAPE,ERR=998) (JN(I),I=1,NEWOUT)
                   IF(JCE.EQ.1) WRITE(NTAPE,ERR=998) (KJN(I),I=1,NEWOUT)
                   IF(NPOLL.GT.0) THEN
                            WRITE(NTAPE,ERR=998) (PNAME(J),J=1,NPOLL)
                            WRITE(NTAPE,ERR=998) (PUNIT(J),J=1,NPOLL)
                            WRITE(NTAPE,ERR=998) (NDIM(J),J=1,NPOLL)
                            ENDIF
                   WRITE(NTAPE,ERR=998)   QCONV
                   ENDIF
      RETURN
  998 WRITE(N6,1998) NTAPE,FFNAME(IOUTCT+25)
      STOP
  999 WRITE(N6,1999) NTAPE,FFNAME(INCNT)
      STOP
C=======================================================================
1     FORMAT(//,
     1' ###########################################',/,
     2' # Header information from interface file: #',/,
     3' ###########################################',//,
     4 ' Title from first computational block:',/,1X,A80,/,1X,A80)
2     FORMAT(/,' Title from immediately preceding computational block',
     1 /,1X,A80,/,1X,A80)
3     FORMAT(/,
     1 ' Name of preceding block:................',A20)
4     FORMAT(
     1 ' Initial Julian date (IDATEZ)......................',I8,/,
     2 ' Initial time of day in seconds (TZERO)............',F8.1)
5     FORMAT(' No. Transfered input locations....................',I8,/,
     1       ' No. Transfered pollutants.........................',I8,/,
     2       ' Size of total catchment area (acres)..............',F8.2,
     3     /,' Numbers (JCE=0) or Alphanumeric (JCE=1)...........',I8)
6     FORMAT(/,' #################################################',/,
     1' # Element numbers of interface inlet locations: # ',/,
     2' #################################################',/,(9I8))
7     FORMAT(/,' #########################################',/,
     1' # Quality parameters on interface file: #',/,
     2' #########################################',//,
     3 ' No. Name      Units     Type of units',/,
     4 ' --- ----      -----     -------------',/,
     5 (1X,I2,2X,A8,2X,A8,I7))
8     FORMAT (/,' Conversion factor to cfs for flow units',/,
     1           ' on interface file.  Multiply by: ',F11.5)
66    FORMAT(/,' #################################################',/,
     1' # Element numbers of interface inlet locations: # ',/,
     2' #################################################',/,
     3(9(1X,A10)))
1998  FORMAT(/,' ===> Error !!  Writing the interface file on unit # ',
     1       I10,/,'  File name... ',A60)
1999  FORMAT(/,' ===> Error !!  Reading the interface file on unit # ',
     1       I10,/,'  File name... ',A60)
C=======================================================================
      END