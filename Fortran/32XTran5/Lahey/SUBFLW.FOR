       SUBROUTINE SUBFLW
C================================================================
C      CALCULATES DISCHARGE USING SUBMERGED DISCHARGE ALGORITHMS
C================================================================
       INCLUDE 'TAPES.INC'
       INCLUDE 'RDGATE.INC'
       INCLUDE 'CONST.INC'
       INCLUDE 'RGFLOW.INC'
       INCLUDE 'FLCOMN.INC'
       INCLUDE 'DISCOF.INC'
       INCLUDE 'LRG.INC'
C================================================================
         QS = 0.0
         GOS = GOPEN(IR)
         GOPH = GOS/PINHGT(IR)
         RADPH = GRAD(IR)/PINHGT(IR)
         Y2PH=Y2/PINHGT(IR)
         Y3PH=Y3/PINHGT(IR)
C===============================================================
C     CHECK THAT FLOW IS SUBMERGED BY COMPARING DOWNSTREAM DEPTH
C      WITH CRITICAL DETPH. IF NOT RETURN TO MAIN PROGRAM
C===============================================================
       IF(Y3.LE.DC.OR.Y3PH.LT.GOPH) THEN
          QGATE(IR) = QFREE
          RGC(IR) = 4
          GOTO 10
       ENDIF
C================================================================
C      COMPUTE DISCHARGE COEFFICIENT
C================================================================
       CALL CDSUB(SCDA)
       IF(RGC(IR).NE.0) GOTO 10
C================================================================
C      COMPUTE SUBMERGED DISCHARGE
C================================================================
         QS = SCDA*GWDTH(IR)*GOS*SQRT(2.0*GC*Y2)
C================================================================
 10    CONTINUE
       RETURN
       END
