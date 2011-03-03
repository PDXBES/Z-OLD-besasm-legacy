       SUBROUTINE FRECAL
C===============================================================
C      COMPUTES THE UPSTREAM HGL AND THE CRITICAL FLOW DEPTH
C       FOR FREE FLOW CONDITIONS
C===============================================================
        INCLUDE 'TAPES.INC'
        INCLUDE 'RDGATE.INC'
        INCLUDE 'CONST.INC'
        INCLUDE 'RGFLOW.INC'
        INCLUDE 'FLCOMN.INC'
        INCLUDE 'LRG.INC'
C===============================================================
C      DETERMINE UPSTREAM HGL
C===============================================================
         VH1 = (QF/AREAUP)**2/(2.*GC)
         VH2 = (QF/(GWDTH(IR)*(HU1+YINVRT(IR))))**2/(2.*GC)
         HLTUP = (ABS(VH1-VH2)) * UPTRNS(IR)
         EG2 = HU1 + (QF/AREAUP)**2/(2.*GC) + YINVRT(IR) - HLTUP
         Y2 = EG2 
C================================================================
C      COMPUTE CRITICAL DEPTH AND CRITICAL ENERGY GRADIENT
C================================================================
         DC = (QF/(5.67*GWDTH(IR)))**0.667
         EGC = DC + (QF/(DC*GWDTH(IR)))**2/(2.*GC)
        IF(EGC.GT.EG2) THEN
         ERRMES = 1
C AMM 12/23/96 Changed following line; ENDOFF does not require a
C              parameter         
C         CALL ENDOFF(ERRMES)
         CALL ENDOFF
        ENDIF
C================================================================
       RETURN
       END
