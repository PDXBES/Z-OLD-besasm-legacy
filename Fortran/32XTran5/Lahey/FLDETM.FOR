      SUBROUTINE FLDETM
C================================================================
C      DETERMINE THE FLOW CONDITION OF THE RADIAL GATE
C================================================================
       INCLUDE 'TAPES.INC'
       INCLUDE 'RDGATE.INC'
       INCLUDE 'RGFLOW.INC'
       INCLUDE 'CONST.INC'
       INCLUDE 'FLCOMN.INC'
       INCLUDE 'RINGAT.INC'
       INCLUDE 'LRG.INC'
C================================================================
       INTEGER FCTEST
       DIMENSION FCTEST(2,NRADG)
       NRGCNV = 0
C================================================================
       IF(RGC(IR).EQ.4) GOTO 40
C================================================================
C      CALCULATE DOWNSTREAM FORCE
C================================================================
          DYVCC = 1.0
       DO 30 JT = 1, 2
          IF(JT.EQ.1) THEN
            Q = QFREE
          ELSE
            Q = QSUB
          ENDIF
            FDN = Q**2/(32.2*GWDTH(IR)*Y3) + GWDTH(IR)*Y3**2/2.0
            A = GWDTH(IR)**2*64.4
C================================================================
C      ESTIMATE DEPTH AT VENA CONTRACTA(YVCC)
C================================================================
            YVCC = GOPEN(IR)*0.6
C================================================================
C      CONVERGENCE ROUTINE NEWTON METHOD FOR YVCC
C================================================================
          DO 20  MM = 1, RGIMAX
             IF(ABS(DYVCC).GT.YDELTA) THEN
 10   CONTINUE
               OLDYVC = YVCC
               DY = EG2 - YVCC
               FYVCC = Q**2 - A*YVCC**2*DY
               DFYVCC = A*(3.*YVCC**2-2.*YVCC*EG2)
             IF(ABS(DFYVCC).LT.0.0001) DFYVCC = 0.0001
               DYVCC  = -FYVCC/DFYVCC
               YVCC = YVCC + DYVCC
             IF(YVCC.LE.0.0) THEN
               YVCC = OLDYVC*0.9
               NRGCNV = NRGCNV + 1
               IF(NRGCNV.LE.RGIMAX) GOTO 10
             ENDIF
               IF(MM.EQ.RGIMAX) THEN
                 ERRMES = 1
C AMM 12/23/96 Changed following line; ENDOFF does not require a
C              parameter         
C         CALL ENDOFF(ERRMES)
         CALL ENDOFF
               ENDIF
             ENDIF
 20   CONTINUE
C================================================================
C      CALCULATE UPSTREAM FORCES ASSUMING JET IS FREE
C================================================================
         FUP = Q**2/(32.2*GWDTH(IR)*YVCC)+(GWDTH(IR)*YVCC**2)/2.0
       IF(YVCC.LE.0.0) FUP = 0.0
C================================================================
         IF(YVCC.GE.DC) THEN
           FCTEST(JT,IR) = 2
         ELSEIF(FUP.GE.FDN) THEN
           FCTEST(JT,IR) = 1
         ELSE
           FCTEST(JT,IR) = 2
         ENDIF
 30   CONTINUE
C================================================================
C   DETERMINE IF FLOW IS FREE, SUBMERGED OR IN TRANSITION RANGE
C================================================================
       IF(FCTEST(1,IR).EQ.1.AND.FCTEST(2,IR).EQ.1) THEN
         RGC(IR) = 4
       ELSEIF(FCTEST(1,IR).EQ.2.AND.FCTEST(2,IR).EQ.2) THEN
         RGC(IR) = 5
       ELSE
         RGC(IR) = 6
       ENDIF
C================================================================
 40    CONTINUE
       IF(RGC(IR).EQ.4) QGATE(IR) = QFREE
       IF(RGC(IR).EQ.5) QGATE(IR) = QSUB
       IF(RGC(IR).EQ.6) QGATE(IR) = 0.5*(QFREE+QSUB)
C================================================================
       RETURN
       END
