       SUBROUTINE SUBCAL
C================================================================
C      DETERMINES UPSTREAM AND DOWNSTREAM ENERGY GRADIENTS
C      UNDER SUBMERGED FLOW CONDITIONS
C================================================================
      INCLUDE 'TAPES.INC'
      INCLUDE 'RDGATE.INC'
      INCLUDE 'CONST.INC'
      INCLUDE 'RGFLOW.INC'
      INCLUDE 'FLCOMN.INC'
      INCLUDE 'LRG.INC'
C================================================================
C       UPSTREAM & DOWNSTREAM HEAD LOSS EQUATIONS
C================================================================
         Q1 = QG
       IF(Q1.EQ.0.0) Q1 = 0.0001
         VH1 = (QG/AREAUP)**2/(2.0*GC)
         VH2 = (QG/(GWDTH(IR)*(HU1+YINVRT(IR))))**2/(2.0*GC)
C================================================================
C       TRANSITION HEAD LOSS COEFFICIENT EQUATIONS
C================================================================
         HLTUP =(ABS(VH1-VH2))*UPTRNS(IR)
         VH4 = (QG/AREADN)**2/(2.0*GC)
         VH3=(QG/(GWDTH(IR)*(DNYRG-YSYFON(IR))))**2/(2.*GC)
         HLTDN = (ABS(VH3-VH4))*DNTRNS(IR)
C===============================================================
C       DETERMINE ENERGY GRADE LINE UPSTREAM AND DOWNSTREAM
C===============================================================
         EG2 = HU1+(QG/AREAUP)**2/(2.0*GC)+YSYFON(IR)-HLTUP
         EG4 = DNYRG+(QG/AREADN)**2/(2.0*GC) - YSYFON(IR)
         EG3 = EG4 + HLTDN
         Y2 = EG2
         Y3 = EG3
C===============================================================
C       COMPUTE CRITICAL DEPTH & CRITICAL ENERGY GRADIENT
C===============================================================
         DC=(QS/(5.67*GWDTH(IR)))**0.667
         EGC = DC + (QS/(DC*GWDTH(IR)))**2/(2.0*GC)
         Y3E = DNYRG + HLTDN - YSYFON(IR)
       IF(Y3E.LE.DC) Y3 = Y3E
C===============================================================
       IF(EGC.GT.EG2) THEN
         ERRMES = 2
C AMM 12/23/96 Changed following line; ENDOFF does not require a
C              parameter         
C         CALL ENDOFF(ERRMES)
         CALL ENDOFF
       ENDIF
C===============================================================
       IF(EGC.GT.EG3) THEN
         ERRMES = 2
C AMM 12/23/96 Changed following line; ENDOFF does not require a
C              parameter         
C         CALL ENDOFF(ERRMES)
         CALL ENDOFF
       ENDIF
C================================================================
      RETURN
      END
