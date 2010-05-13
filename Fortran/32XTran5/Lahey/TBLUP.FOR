       FUNCTION TBLUP(RGDPTH)
C================================================================
C     DETERMINES VERTICAL GATE OPENING FOR DEPTH DEPENDENT GATES
C         USE TABLE LOOK UP AND LINEAR INTERPOLATION
C================================================================
      INCLUDE 'TAPES.INC'
      INCLUDE 'RINGAT.INC'
      INCLUDE 'LRG.INC'
C================================================================
CPDX--SM, 11/11/91--NTVAL CHANGED TO NTVALR
       NTV = NTVALR(IR) - 1
      DO 10 L = 1, NTV
      IF(RGDPTH.LT.GCDPTH(IR,L+1).AND.RGDPTH.GE.GCDPTH(IR,L)) GOTO 20
 10   CONTINUE
C=================================================================
 20   CONTINUE
        TBLUP = GDOPEN(IR,L) + (RGDPTH-GCDPTH(IR,L)) *
     +  (GDOPEN(IR,L+1)-GDOPEN(IR,L))/(GCDPTH(IR,L+1)-GCDPTH(IR,L))
C=================================================================
      RETURN
      END
