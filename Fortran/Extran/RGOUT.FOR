      SUBROUTINE RGOUT(NOUT)
CPDX      SUBROUTINE RGOUT(NOUT,JPRINT)
C================================================================
C     PRINTS TIME HISTORY OF RADIAL GATE OPERATIONS
C     OUTPUT FORMAT KEPT THE SAME AS EXTRAN
C================================================================
        INCLUDE 'TAPES.INC'
        INCLUDE 'LRG.INC'
        INCLUDE 'RGPRNT.INC'
        INCLUDE 'CONTR.INC'
        INCLUDE 'PIPE.INC'
        INCLUDE 'OUT.INC'
CPDX         INCLUDE 'XL.INC'
        INCLUDE 'PDXTRN.INC'
C================================================================
      CHARACTER*2 TP
      DIMENSION RGMEAN(NRADG), RGMAX(NRADG), RGMIN(NRADG),
     +   GVMEAN(NRADG), GVMAX(NRADG), GVMIN(NRADG),PSF(NPO,2)
C================================================================
      DO 10 I = 1, NRGATE
            LR = LRGATE(I)
            RGMEAN(I) = 0.0
            RGMAX(I) = 0.0
            RGMIN(I) = 1.0E30
            GVMEAN(I) = 0.0
            GVMAX(I) = 0.0
            GVMIN(I) = 1.0E30
            LRGATE(I) = NCOND(LR)
 10   CONTINUE
C================================================================
C     READ THE INFORMATION ON FILE NOUT
C================================================================
            TP = '  '
      DO 40 I = 1, NRGATE, 5
            REWIND NOUT
CPDX            IF(NPPOUT.LE.1) THEN
            READ(NOUT,110) JCE,NHPRT,NQPRT,NSURF
            IF(JCE.EQ.0) THEN
            IF(NHPRT.GT.0) READ(NOUT,110) (N1,J=1,NHPRT)
            IF(NQPRT.GT.0) READ(NOUT,110) (N1,J=1,NQPRT)
            IF(NSURF.GT.0) READ(NOUT,110) (N1,J=1,NSURF)
            ELSE
            IF(NHPRT.GT.0) READ(NOUT,110) (BMJ,J=1,NHPRT)
            IF(NQPRT.GT.0) READ(NOUT,110) (BMJ,J=1,NQPRT)
            IF(NSURF.GT.0) READ(NOUT,110) (BMJ,J=1,NSURF)
            ENDIF
CPDX            ENDIF
            WRITE(N6,100)
            ITRG = I + 4
            IF(ITRG.GT.NRGATE) ITRG = NRGATE
               WRITE(N6,101) (TP,LRGATE(L),L=I,ITRG)
               WRITE(N6,102) (TP,L=I,ITRG)
               WRITE(N6,103) (TP,L=I,ITRG)
            LTR = MIN0(I+4,NRGATE)
      DO 30 L = 1, LTIME
      IF(JPRINT.EQ.1) READ(NOUT,120) TIME,DELT,(PRTY(K),PRTH(K),
     +                    K=1,NHPRT),(PRTQ(J),PRTV(J),J=1,NQPRT),
     +                              (PSF(J,1),PSF(J,2),J=1,NSURF)
      IF(JPRINT.EQ.2) READ(NOUT,120) TIME,DELT,(PRTY(K),PRTH(K),
     +                     K=1,NHPRT),(PRTQ(J),PRTV(J),J=1,NQPRT)
      IF(JPRINT.EQ.3) READ(NOUT,120) TIME,DELT,(PRTY(K),PRTH(K),
     +                                                 K=1,NHPRT)
      IF(JPRINT.EQ.4) READ(NOUT,120) TIME,DELT,(PRTY(K),PRTH(K),
     +                   K=1,NHPRT),(PSF(J,1),PSF(J,2),J=1,NSURF)
      IF(JPRINT.EQ.5) READ(NOUT,120) TIME,DELT,(PRTQ(J),PRTV(J),
     +                   J=1,NQPRT),(PSF(J,1),PSF(J,2),J=1,NSURF)
      IF(JPRINT.EQ.6) READ(NOUT,120) TIME,DELT,(PRTQ(J),PRTV(J),
     +                                                 J=1,NQPRT)
      IF(JPRINT.EQ.7) READ(NOUT,120) TIME,DELT,(PSF(J,1),PSF(J,2),
     +                                                   J=1,NSURF)
      IF(NRGPNT.GT.0) READ(NOUT,120) TIME,DELT,
     +                             (PRTRGQ(K),PRTGPN(K),K=1,NRGATE)
            DO 20 K = 1, LTR
                  RGMEAN(K) = RGMEAN(K) + PRTRGQ(K)
                  GVMEAN(K) = GVMEAN(K) + PRTGPN(K)
               IF(PRTRGQ(K).GT.RGMAX(K)) RGMAX(K) = PRTRGQ(K)
               IF(PRTRGQ(K).LT.RGMIN(K)) RGMIN(K) = PRTRGQ(K)
               IF(PRTGPN(K).GT.GVMAX(K)) GVMAX(K) = PRTGPN(K)
               IF(PRTGPN(K).LT.GVMIN(K)) GVMIN(K) = PRTGPN(K)
 20   CONTINUE
C================================================================
C     WRITE RADIAL GATE OPERATION SUMMARY TO THE OUTPUT FILE
C================================================================
               LTIMEH = INT(TIME/3600.0)
               LTIMEM = INT((TIME/3600.0-FLOAT(LTIMEH))*60.0)
               WRITE(N6,104) LTIMEH,LTIMEM,(PRTRGQ(K),PRTGPN(K),K=I,LTR)
 30   CONTINUE
        WRITE(N6,105) (RGMEAN(K)/FLOAT(LTIME),GVMEAN(K)/FLOAT(LTIME),
     +                 K=I,LTR)
        WRITE(N6,106) (RGMEAN(K)*DELT,K=I,LTR)
        WRITE(N6,107) (RGMAX(K),GVMAX(K),K=I,LTR)
        WRITE(N6,108) (RGMIN(K),GVMIN(K),K=I,LTR)
 40   CONTINUE
C=================================================================
 100    FORMAT(/,
     +'  *********************************************************',/,
     +'  *       TIME HISTORY OF RADIAL GATE OPERATIONS          *',/,
     +'  *              Q(CFS)     GOPEN(FT)                     *',/,
     +'  *********************************************************',/)
 101   FORMAT(/,'    TIME ',5(A2,'RAD.GATE ',I9))
 102   FORMAT('  HR:MIN ',5(A2,'    FLOW     GOPEN'))
 103   FORMAT('  ====== ',5(A2,'    ====     ====='))
 104   FORMAT(1H ,I3,':',I2,2X,5(F10.2,F10.2))
 105   FORMAT('    MEAN ',5(F10.2,F10.2))
 106   FORMAT('   TOTAL ',5(1PE10.2,10X))
 107   FORMAT(' MAXIMUM ',5(F10.2,F10.2))
 108   FORMAT(' MINIMUM ',5(F10.2,F10.2))
 110   FORMAT(200(I10,1X))
 120   FORMAT(200(E12.5,1X))
C================================================================
      RETURN
      END
