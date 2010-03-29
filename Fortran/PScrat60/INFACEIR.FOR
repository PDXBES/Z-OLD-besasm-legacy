      SUBROUTINE INFACEIR(IDO,NTAPE,FFNAME,ntype)
C=======================================================================
C     This subroutine reads or writes the header information
C     for a SWMM 4.05 interface file.  Created FEBRUARY, 1987 
C     by Robert E. Dickinson.  
*	Last updated by Virgil Adderley, CH2M HILL 8/19/92
**    Modified 8/18/98 by Virgil Adderley to match XP-SWMM 5.20
C=======================================================================      
      INCLUDE 'TAPESIR.INC'
      INCLUDE 'INTER.INC'
      CHARACTER KJN*16, FFNAME*240
      DIMENSION JN(NIE),KJN(NIE)
C=======================================================================
C     Read interface headers.
C=======================================================================
      IF(IDO.EQ.0) THEN
                   REWIND NTAPE
                   READ(NTAPE) TITLE(1)
                   READ(NTAPE) TITLE(2)
                   READ(NTAPE) IDATEZ,TZERO
                   READ(NTAPE) TITLE(3)
                   READ(NTAPE) TITLE(4)
                   READ(NTAPE) SOURCE,LOCATS,NQUAL,TRIBA
**                   READ(NTAPE) SOURCE,LOCATS,NQUAL,TRIBA,NJCE
                     NJCE=1
                   IF(NJCE.EQ.0) READ(NTAPE)(NLOC(I),I=1,LOCATS)
                   IF(NJCE.EQ.1) READ(NTAPE)(KAN(I),I=1,LOCATS)
                   IF(NQUAL.GT.0) THEN
                            READ(NTAPE) (PNAME(J),J=1,NQUAL)
                            READ(NTAPE) (PUNIT(J),J=1,NQUAL)
                            READ(NTAPE) (NDIM(J),J=1,NQUAL)
                            ENDIF
                   READ(NTAPE) QCONV
                   ENDIF
C=======================================================================
C     Read and write interface headers.
C=======================================================================
      IF(IDO.EQ.1) THEN
                   REWIND NTAPE
                   READ(NTAPE) TITLE(1)
                   READ(NTAPE) TITLE(2)
                   READ(NTAPE) IDATEZ,TZERO
                   READ(NTAPE) TITLE(3)
                   READ(NTAPE) TITLE(4)
**                   READ(NTAPE) SOURCE,LOCATS,NQUAL,TRIBA,NJCE
                     READ(NTAPE) SOURCE,LOCATS,NQUAL, TRIBA
*                     READ(NTAPE) (GUESS(I),I=1,100)
                     NJCE = 1
*
              IF(ntype.LE.1) THEN
*						Write *.csv match of interface file:
          WRITE(N6,100) title(1), title(2)
          WRITE(N6,110) idatez, tzero
          WRITE(N6,100) title(3), title(4)
          WRITE(N6,130) source, locats, nqual, triba, njce
*                     WRITE(N6,*) (GUESS(I),I=1,100)
*
  110     FORMAT( I10,',   ',F12.2)
  100     FORMAT( '"',A80,'"',/, '"',A80,'"')
  130     FORMAT('"',A20,'", ',I9,', ',I2,', ',F12.2,',   ',I1)
  140     FORMAT(10000(I16,', '))
  141     FORMAT(10000('"',A16,'", '))
  150     FORMAT(4(A8,', '))
  160     FORMAT(4(I1,', '))
  170     FORMAT(F5.3)
*
              ELSE
*
                   WRITE(N6,1)  TITLE(1),TITLE(2)
                   WRITE(N6,2)  TITLE(3),TITLE(4)
                   WRITE(N6,3)  SOURCE
                   WRITE(N6,4)  IDATEZ,TZERO
                   WRITE(N6,5)  LOCATS,NQUAL,TRIBA,NJCE
*
              ENDIF
C=======================================================================
C     Read sequence of location numbers.
C=======================================================================
                   IF(NJCE.EQ.0) THEN
                      READ(NTAPE) (NLOC(I),I=1,LOCATS)
                      if(ntype.LE.1) WRITE(N6,140) (NLOC(I),I=1,locats)
                      if(ntype.GT.1) WRITE(N6,6) (NLOC(I),I=1,LOCATS)
                      ELSE
                      READ(NTAPE)  (KAN(I),I=1,LOCATS)                       
*                        ntype = 1 for *.csv, = 2 for formatted table                     
                      if(ntype.LE.1) WRITE(N6,141) (KAN(I),I=1,locats)
                      if(ntype.GT.1) WRITE(N6,66) (KAN(I),I=1,LOCATS)
                      ENDIF
                   IF(NQUAL.GT.0) THEN
                            READ(NTAPE)   (PNAME(J),J=1,NQUAL)
                            READ(NTAPE)   (PUNIT(J),J=1,NQUAL)
                            READ(NTAPE)   (NDIM(J),J=1,NQUAL)
                       if(ntype.LE.1) WRITE(N6,150) (pname(J),J=1,nqual)
                       if(ntype.LE.1) WRITE(N6,150) (punit(J),J=1,nqual)
                       if(ntype.LE.1) WRITE(N6,160) (ndim(J),J=1,nqual)
*
                       if(ntype.GT.1) WRITE(N6,7) (J,PNAME(J),
     +                                PUNIT(J),NDIM(J),J=1,NQUAL)
                            ENDIF
*
                   READ(NTAPE)  QCONV
                   if(ntype.GT.1) WRITE(N6,8)          QCONV
                   if(ntype.LE.1) WRITE(N6,170)        QCONV
                   ENDIF
C=======================================================================
C     Write interface headers.
C=======================================================================
      IF(IDO.EQ.2) THEN
                   REWIND NTAPE
                   READ(NTAPE) NEWOUT,NPOLL
                   IF(NJCE.EQ.0) READ(NTAPE)  (JN(I),I=1,NEWOUT)
                   IF(NJCE.EQ.1) READ(NTAPE) (KJN(I),I=1,NEWOUT)
                   REWIND NTAPE
                   WRITE(NTAPE,ERR=998) TITLE(1)
                   WRITE(NTAPE,ERR=998) TITLE(2)
                   WRITE(NTAPE,ERR=998) IDATEZ,TZERO
                   WRITE(NTAPE,ERR=998) TITLE(3)
                   WRITE(NTAPE,ERR=998) TITLE(4)
                   WRITE(NTAPE,ERR=998) SOURCE,NEWOUT,NPOLL,TRIBA,NJCE
**                   WRITE(NTAPE,ERR=998) SOURCE,NEWOUT,NPOLL,TRIBA
                  IF(NJCE.EQ.0) WRITE(NTAPE,ERR=998) (JN(I),I=1,NEWOUT)
                  IF(NJCE.EQ.1) WRITE(NTAPE,ERR=998) (KJN(I),I=1,NEWOUT)
                   IF(NPOLL.GT.0) THEN
                            WRITE(NTAPE,ERR=998) (PNAME(J),J=1,NPOLL)
                            WRITE(NTAPE,ERR=998) (PUNIT(J),J=1,NPOLL)
                            WRITE(NTAPE,ERR=998) (NDIM(J),J=1,NPOLL)
                            ENDIF
                   WRITE(NTAPE,ERR=998)   QCONV
                   ENDIF
      RETURN
  998 WRITE(N6,1998) NTAPE,FFNAME
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
     3     /,' Numbers (NJCE=0) or Alphanumeric (NJCE=1)...........',I8)
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
*
**11     FORMAT(' "HEADER INFORMATION FROM INTERFACE FILE:"',/,
**     2' "Title from first computational block: "',/,' "',A80,'"',/,
**     3' "',A80,'"',/)
**12    FORMAT('" Title from immediately preceding computational block:"',
**     1 /,' "',A80,'"',/,' "',A80,'"',/)
**13     FORMAT(' " Name of preceding block:...............", "',A20,'"')
**14     FORMAT(
**     1 ' "Initial Julian date (IDATEZ)......................", ',I8,/,
**     2 ' "Initial time of day in seconds (TZERO)............", ',F8.1)
**15     FORMAT(' "No. Transfered input locations....................",',
**    2 I8,/,
**     3 ' "No. Transfered pollutants.........................", ',I8,/,
**     4 ' "Size of total catchment area (acres)..............", ',F8.2,
**     5 /,' "Numbers (NJCE=0) or Alphanumeric (NJCE=1)...........", ',I8)
**16    FORMAT(/,'" #################################################"',/,
**     1' "# Element numbers of interface inlet locations: # "',/,
**     2' "#################################################"',/,(9I8))
**17     FORMAT(/,'" #########################################"',/,
**     1'" # Quality parameters on interface file: #"',/,
**     2'" #########################################"',//,
**     3 '" No. Name      Units     Type of units"',/,
**     4 '" --- ----      -----     -------------"',/,
**     5 (1X,I2,2X,A8,2X,A8,I7))
**18     FORMAT (/,'" Conversion factor to cfs for flow units"',/,
**    1           '" on interface file.  Multiply by: "',F11.5)
*
*
66    FORMAT(/,' #################################################',/,
     1' # Element numbers of interface inlet locations: # ',/,
     2' #################################################',/,
     3(9(1X,A16)))
**76    FORMAT(/,'" #################################################"',/,
**     1'" # Element numbers of interface inlet locations: # "',/,
**     2'" #################################################"',/,
**     3(9(1X,'"',A10,'",')))
1998  FORMAT(/,' ===> Error !!  Writing the interface file on unit # ',
     1       I10,/,'  File name... ',A60)
1999  FORMAT(/,' ===> Error !!  Reading the interface file on unit # ',
     1       I10,/,'  File name... ',A60)
C=======================================================================
      END
