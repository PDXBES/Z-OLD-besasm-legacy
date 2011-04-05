       SUBROUTINE FRFLOW
C================================================================
C      CALCULATES DISCHARGE USING THE FREE DISCHARGE ALGORITHMS
C================================================================
        INCLUDE 'TAPES.INC'
        INCLUDE 'RDGATE.INC'
        INCLUDE 'CONST.INC'
        INCLUDE 'RGFLOW.INC'
        INCLUDE 'FLCOMN.INC'
        INCLUDE 'DISCOF.INC'
        INCLUDE 'LRG.INC'
C================================================================
          QF = 0.0
          GOF = GOPEN(IR)
          GOPH = GOF/PINHGT(IR)
          RADPH = GRAD(IR)/PINHGT(IR)
          Y2PH = Y2/PINHGT(IR)
C=================================================================
C      COMPUTE DISCHARGE COEFICIENT
C=================================================================
        CALL CDFREE(FCDA)
C================================================================
C      COMPUTE FREE DISCHARGE
C================================================================
          IF(RGC(IR).EQ.7) THEN
             QF = FCDA*GWDTH(IR)*GOF*SQRT(2.0*GC*HU2)
          ELSE
             QF = FCDA*GWDTH(IR)*GOF*SQRT(2.0*GC*Y2)
          ENDIF
C================================================================
       RETURN
       END
