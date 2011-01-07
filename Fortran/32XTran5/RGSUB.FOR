       SUBROUTINE RGSUB
C================================================================
C      CALCULATES SUBMERGED DISCHARGE FOR RADIAL GATE STRUCTURES
C================================================================
      INCLUDE 'TAPES.INC'
      INCLUDE 'RDGATE.INC'
      INCLUDE 'CONST.INC'
      INCLUDE 'RINGAT.INC'
      INCLUDE 'RGFLOW.INC'
      INCLUDE 'FLCOMN.INC'
      INCLUDE 'LRG.INC'
C================================================================
C      ESTIMATE INITIAL VAUE OF SUBMERGED DISCHARGE
C================================================================
       IF(RGC(IR).EQ.7) GOTO 50
         MQS1 = 0
         NRGCNV = 0
         MRIMAX = 0
       IF(HU2.LE.0.0) HU2 = 0.1
         QS = 0.3*GWDTH(IR)*GOPEN(IR)*SQRT(2.*GC*HU2)*CSDA(IR)
C================================================================
 10      CONTINUE
         DY2 = 1.0
         DY3 = 1.0
         QG = QS
         QSOLD = QS
C================================================================
C       COMPUTE ENERGY GRADIENTS UPSTREAM AND DOWNSTREAM
C================================================================
       IF(QS.LE.0.0001)THEN
          ERRMES = 2
C AMM 12/23/96 Changed following line; ENDOFF does not require a
C              parameter         
C         CALL ENDOFF(ERRMES)
         CALL ENDOFF
          GOTO 40
       ENDIF
       CALL SUBCAL
C================================================================
C     CONVERGENCE ROUTINE NEWTON METHOD FOR Y2
C================================================================
       DO 20 MM = 1, RGIMAX
         IF(ABS(DY2).GT.YDELTA) THEN
           VH2 = ((QG/(GWDTH(IR)*Y2))**2)/(2.0*GC)
           DY2 = -1*(EG2-Y2-VH2)/(-1.0+2.0*VH2/Y2)
           Y2 = Y2 + DY2
         IF(Y2.LE.0.0) Y2 = 0.1
C================================================================
           IF(MM.EQ.RGIMAX) THEN
             ERRMES = 5
C AMM 12/23/96 Changed following line; ENDOFF does not require a
C              parameter         
C         CALL ENDOFF(ERRMES)
         CALL ENDOFF
             GOTO 40
           ENDIF
C================================================================
         ENDIF
 20    CONTINUE
C================================================================
C      CONVERGENCE ROUTINE NEWTON METHOD FOR Y3
C================================================================
       DO 30 MM = 1, RGIMAX
         IF(ABS(DY3).GT.YDELTA) THEN
           VH3 = ((QG/(GWDTH(IR)*Y3))**2)/(2.0*GC)
           DY3 = -1*(EG3-Y3-VH3)/(-1.0+2.0*VH3/Y3)
           Y3 = Y3 + DY3
C================================================================
           IF(MM.EQ.RGIMAX) THEN
             ERRMES = 5
C AMM 12/23/96 Changed following line; ENDOFF does not require a
C              parameter         
C         CALL ENDOFF(ERRMES)
         CALL ENDOFF
             GOTO 40
           ENDIF
C================================================================
         ENDIF
 30   CONTINUE
C================================================================
       IF((Y2-(Y3+0.05)).LE.0.0) THEN
         QS = QS*0.9
         NRGCNV = NRGCNV + 1
         IF(NRGCNV.LE.RGIMAX) GOTO 10
       ENDIF
       IF(Y3.GE.Y2) THEN
          ERRMES = 3
C AMM 12/23/96 Changed following line; ENDOFF does not require a
C              parameter         
C         CALL ENDOFF(ERRMES)
         CALL ENDOFF
          GOTO 40
       ENDIF
C================================================================
C      COMPUTE DISCHARGE USING SUBMERGED DISCHARGE ALGORITHMS
C==============================================================
      CALL SUBFLW
      IF(RGC(IR).NE.0) GOTO 50
C===============================================================
C      CONVERGENCE ROUTINE FOR SUBMERGED DISCHARGE
C===============================================================
          IF(ABS(QSOLD-QS).GT.QDELTA) THEN
          IF(MQS1.EQ.0) THEN
            FOLD = QSOLD-QS
            QSOLD1 = QSOLD
            QST = QSOLD + (QS-QSOLD)*0.2
            MQS1 = 1
            GOTO 10
          ELSE
            FNEW = QSOLD - QS
            FONS = FOLD - FNEW
          IF(ABS(FONS).LT.0.001) FONS = 0.001
            QS = QSOLD - FNEW*(QSOLD1-QSOLD)/FONS
            FOLD = FNEW
            QSOLD1 = QSOLD
C===============================================================
             MRIMAX = MRIMAX + 1
          IF(MRIMAX.GE.RGIMAX) THEN
            ERRMES = 5
C AMM 12/23/96 Changed following line; ENDOFF does not require a
C              parameter         
C         CALL ENDOFF(ERRMES)
         CALL ENDOFF
            GOTO 40
          ELSE
            GOTO 10
          ENDIF
C===============================================================
          ENDIF
          ENDIF
C===============================================================
 40    CONTINUE
C===============================================================
       QSUB = QS
C================================================================
 50    CONTINUE
       RETURN
       END
