       SUBROUTINE RGINIT
C==================================================================
C     INITILIZATION OF RADIAL GATE VARIABLES
C==================================================================
        INCLUDE 'TAPES.INC'
        INCLUDE 'CONTR.INC'
        INCLUDE 'CONST.INC'
        INCLUDE 'RDGATE.INC'
        INCLUDE 'RINGAT.INC'
        INCLUDE 'RGINTK.INC'
        INCLUDE 'RGFLOW.INC'
        INCLUDE 'LRG.INC'
C=======================================================================
      DO 20 IJ = 1, NRADG
         GWDTH(IJ) = 0.0
         GRAD(IJ) = 0.0
         CFDA(IJ) = 1.0
         CSDA(IJ) = 1.0
         CDRG(IJ) = 0.0
         CFLRVS(IJ) = 0.0
         PINHGT(IJ) = 0.0
         YINVRT(IJ) = 0.0
         UPTRNS(IJ) = 1.0
         DNTRNS(IJ) = 1.0
         UPINV(IJ) = 0.0
         DNINV(IJ) = 0.0
         UPDEP(IJ) = 0.0
         CONTRL(IJ) = 0
         GOPEN(IJ) = 0.0
         CRTJUN(IJ) = 0
         DNYCLS(IJ) = 0.0
         DNYOPN(IJ) = 0.0
         RTGCLS(IJ) = 0.0
         RTEGPN(IJ) = 0.0
         GPNMAX(IJ) = 0.0
CPDX--SM, 11/11/91--NTVAL CHANGED TO NTVALR
         NTVALR(IJ) = 0
         NGCNRT(IJ) = 0
      DO  5 K = 1, 5
         NRGERR(IJ,K) = 0
  5   CONTINUE
      DO 10 M = 1, 10
         GCDPTH(IJ,M) = 0.0
         GDOPEN(IJ,M) = 0.0
 10   CONTINUE
 20   CONTINUE
       NGFLAG = 0
       RGERRS = 0
       NRPIPE = 0
       IPR    = 0
C=================================================================
       IF(METRIC.EQ.1) THEN
          GC = 32.2
          CFMANN = 1.486
       ELSE
          GC = 9.81
          CFMANN = 1.0
       ENDIF
C==================================================================
      RETURN
      END
