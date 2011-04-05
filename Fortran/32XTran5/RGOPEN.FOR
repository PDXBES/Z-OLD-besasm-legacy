       SUBROUTINE RGOPEN(DTRG,YROPEN,RGVOPN)
C=================================================================
C         DETERMINES VERTICAL RADIAL GATE OPENING
C        BY TABLE LOOK-UP AND LINEAR INTERPOLATION
C=================================================================
       INCLUDE 'TAPES.INC'
       INCLUDE 'RINGAT.INC'
       INCLUDE 'RDGATE.INC'
       INCLUDE 'LRG.INC'
C=================================================================
       REAL YROPEN,RGVOPN, DTRG
CPDX2       DOUBLE PRECISION DTRG
C=================================================================
C      CONTRL = 1    ==>  TIME DEPENDENT FUNCTION
C      NGCNRT = 1,2  ==>  GATE CLOSING
C      NGCNRT = 3    ==>  GATE OPENS AGAIN BEFORE FULL CLOSURE
C=================================================================
               DTRHRS = DTRG/3600.
          IF(CONTRL(IR).EQ.1) THEN
            IF(NGCNRT(IR).LE.2) THEN
               RGVOPN = GOPEN(IR) - (RTGCLS(IR)*DTRHRS)
            ELSEIF(NGCNRT(IR).EQ.3) THEN
               RGVOPN = GOPEN(IR) + (RTEGPN(IR)*DTRHRS)
            ENDIF
            IF(RGVOPN.GE.GPNMAX(IR)) RGVOPN = GPNMAX(IR)
            IF(RGVOPN.LE.GPNMIN(IR)) THEN
               RGVOPN = GPNMIN(IR)
               NGCNRT(IR) = 0
            ENDIF
            IF(RGVOPN.LE.0.0001)  NGCNRT(IR) = 0
          ENDIF
C=================================================================
C      CONTRL = 2    ==>  DEPTH DEPENDENT FUNCTION
C=================================================================
          IF(CONTRL(IR).EQ.2) THEN
            RGDPTH = YROPEN
            RGVOPN = TBLUP(RGDPTH)
          ENDIF
C=================================================================
 10   CONTINUE
      RETURN
      END
