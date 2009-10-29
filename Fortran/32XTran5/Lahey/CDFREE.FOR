       SUBROUTINE CDFREE(FCDA)
C================================================================
C      COMPUTES FREE DISCHARGE COEFFICIENT
C================================================================
       INCLUDE 'TAPES.INC'
       INCLUDE 'RDGATE.INC'
       INCLUDE 'RGFLOW.INC'
       INCLUDE 'DISCOF.INC'
       INCLUDE 'LRG.INC'
C================================================================
          AFE = SQRT((1.+(RADPH-1.6)**2*31.2)*0.00212) + 0.901
          BFE = SQRT((1.+(RADPH-1.635)**2*187.7)*0.00212)-0.079  
          AFD = 0.788-SQRT((1.0+(RADPH-1.619)**2*89.2)*0.04) 
          BFD = 0.0534*RADPH+0.0457
          FE = AFE - BFE*GOPH 
          FD1 = (1.0 - (GOPH-AFD)**2)*BFD
        IF(FD1.LT.0.0) FD1 = 0.0
          FD = 0.472 - SQRT(FD1) 
        IF(GOPH.LE.0.277) FX1 = 1.94*GOPH-0.377
        IF(GOPH.GT.0.277) FX1 = 0.18*GOPH+0.111
          FY1 = 0.309-0.192*GOPH
          FXV = Y2PH-FX1
          FCD = FE**2*(FD+FXV)**2 -FXV**2
        IF(FCD.LT.0.0) FCD = 0.0    
C================================================================
C      CORRECT THE DISCHARGE COEFFICIENT USING THE  GATE SEAL
C      CORRECTION COEFFICIENT
C================================================================
          CFCDA = CFDA(IR)
        IF(CFDA(IR).EQ.0.0) THEN
          CFCDA = 0.125*GOPH+0.91
        ELSEIF(CFDA(IR).EQ.1.0) THEN
          CFCDA = 0.11*GOPH+0.90
        ENDIF
          FCDA = (SQRT(FCD)+FY1)*CFCDA
C================================================================
 10   CONTINUE
      RETURN
      END

