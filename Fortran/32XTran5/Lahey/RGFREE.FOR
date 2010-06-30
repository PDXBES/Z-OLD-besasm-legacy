       SUBROUTINE RGFREE
C===============================================================
C      COMPUTES THE FREE DISCHARGE FOR RADIAL GATE STRUCTURES
C===============================================================
       INCLUDE 'TAPES.INC'
       INCLUDE 'RDGATE.INC'
       INCLUDE 'RINGAT.INC'
       INCLUDE 'RGINTK.INC'
       INCLUDE 'CONST.INC'
       INCLUDE 'RGFLOW.INC'
       INCLUDE 'FLCOMN.INC'
       INCLUDE 'LRG.INC'
C================================================================
C      ESTIMATE INITIAL VALUE OF FREE DISCHARGE
C================================================================
       IF(RGC(IR).EQ.7) THEN
          Y2 = UPDEP(IR)
          GOTO 25
       ENDIF
         MQF1 = 0
         MRIMAX = 0
         NRGCNV = 0
       IF(HU2.LE.0.0) HU2 = 0.0
         QF = 0.6*GWDTH(IR)*GOPEN(IR)*SQRT(2.0*GC*HU2)*CFDA(IR)
C===============================================================
 10      CONTINUE
         DY2 = 1.0
         QFOLD = QF
C=============================================================== 
C      COMPUTE VALUES OF UPSTREAM HGL AND CRITICAL DEPTH
C===============================================================
       IF(QF.LE.0.0001)THEN
          QF = 0.0
          ERRMES = 1
C AMM 12/23/96 Changed following line; ENDOFF does not require a
C              parameter         
C         CALL ENDOFF(ERRMES)
         CALL ENDOFF
          GOTO 30
       ENDIF
       CALL FRECAL
C================================================================
C      COMPUTE Y2 USING NEWTON'S METHOD CONVERGENCE ROUTINE
C================================================================
          MM = 0 
       DO 20 MM = 1, RGIMAX
        IF(ABS(DY2).GE.YDELTA) THEN
          VH2 = ((QF/(GWDTH(IR)*Y2))**2)/(2.*GC)
          DY2 = -(EG2-Y2-VH2)/(-1.+2.*VH2/Y2) 
          Y2 = Y2 + DY2
C================================================================
         IF(MM.EQ.RGIMAX) THEN
            ERRMES = 5
C AMM 12/23/96 Changed following line; ENDOFF does not require a
C              parameter         
C         CALL ENDOFF(ERRMES)
         CALL ENDOFF
            GOTO 30
         ENDIF
C================================================================
        ENDIF
 20    CONTINUE
        IF(Y2.LE.0.0) THEN
           QF = QF*0.9
           NRGCNV = NRGCNV + 1
        IF(NRGCNV.LE.RGIMAX) GOTO 10
        ENDIF
C================================================================
C      COMPUTE DISCHARGE USING FREE DISCHARGE ALGORITHMS
C================================================================
 25    CONTINUE
       CALL FRFLOW
       IF(RGC(IR).EQ.7) GOTO 30
C================================================================
C       CONVERGENCE ROUTINE FOR FREE DISCHARGE
C================================================================
           IF(ABS(QFOLD-QF).GT.QDELTA) THEN
           IF(MQF1.EQ.0) THEN
             FOLDF = QFOLD - QF
             QFOLD1 = QFOLD
             QF = QFOLD + (QF-QFOLD)*0.2
             MQF1 = 1
             GOTO 10
           ELSE
             FNEWF = QFOLD - QF
             FONF = FOLDF - FNEWF
           IF(ABS(FONF).LT.0.001) FONF = 0.001
             QF = QFOLD - FNEWF*(QFOLD1-QFOLD)/FONF
             FOLDF = FNEWF
             QFOLD1 = QFOLD
C================================================================
              MRIMAX = MRIMAX + 1
           IF(MRIMAX.GE.RGIMAX) THEN
             ERRMES = 5
C AMM 12/23/96 Changed following line; ENDOFF does not require a
C              parameter         
C         CALL ENDOFF(ERRMES)
         CALL ENDOFF
             GOTO 30
           ELSE
             GOTO 10
           ENDIF
C================================================================
           ENDIF
           ENDIF
C================================================================
 30   CONTINUE
C================================================================
          QFREE = QF
C================================================================
       RETURN
       END
