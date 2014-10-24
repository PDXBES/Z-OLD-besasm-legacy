      SUBROUTINE STRIP(N55)
C=======================================================================
C     THIS SUBROUTINE STRIPS COMMENT LINES FROM THE SWMM INPUT FILE
C     Note to Vax Programmers: An output format error will occur
C                              unless special provisions are made by the
C                              VAX operator.  An alternative is to change
C                              all occurrences of 230 to 120 in the
C                              STRIP subroutine.
C     Change in message to screen, WCH, 12/9/94.
C=======================================================================
      INCLUDE 'TAPES.INC'
      CHARACTER ICODE*1,JCODE*1,ISTAR*1,KCODE*230,DOLL*1,BLANK*1,LEND*10
      DATA ISTAR/'*'/,DOLL/'$'/,BLANK/' '/,LEND/'''  ''ENDPR'/
C=======================================================================
      WRITE(*,999)
      DO 100 I = 1,1000000
      READ(N55,1,END=101,ERR=101) ICODE,JCODE,KCODE
cim  further process Kcode to strip stuff after any additional asterisks
cim
      locast = index(kcode,istar)
c     write(n6,*) icode,jcode,kcode,locast
      locast =locast-1
      if(locast.le.0) locast = len(kcode)
c     write(n6,*) Icode,jcode,kcode(1:locast-1)
cim end
      IF(ICODE.EQ.ISTAR) GO TO 100
      IF(ICODE.EQ.DOLL) THEN
                        WRITE(N5,2) ICODE,JCODE,KCODE(1:locast)
                        ELSE IF(ICODE.EQ.BLANK) THEN
                             WRITE(N5,5) ICODE,JCODE,KCODE(1:locast)
                             ELSE
                             WRITE(N5,4) ICODE,JCODE,KCODE(1:locast)
                             ENDIF
100   CONTINUE
101   WRITE(N5,6) LEND
      CLOSE (N55)
      REWIND N5
1     FORMAT(2A1,A230)
2     FORMAT('''',A1,'''',1X,A1,A230)
3     FORMAT(A230)
4     FORMAT('''',2A1,''' ',A230)
5     FORMAT(2A1,A230)
6     FORMAT(A10)
C#### WCH, 12/9/94.  REMOVE "...process is much faster..." stuff.
999   FORMAT(/,' Reading the input file and deleting comment lines.')
C####     +       ' This process is much faster when a RAM drive is used.',/)
      RETURN
      END
