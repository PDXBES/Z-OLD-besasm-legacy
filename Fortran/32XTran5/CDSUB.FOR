       SUBROUTINE CDSUB(SCDA)
C================================================================
C      COMPUTES SUBMERGED DISCHARGE COEFFICIENT
C================================================================
        INCLUDE 'TAPES.INC'
        INCLUDE 'RDGATE.INC'
        INCLUDE 'RGFLOW.INC'
        INCLUDE 'DISCOF.INC'
        INCLUDE 'LRG.INC'
C================================================================
         ADA = 1.0/(11.98*RADPH-26.7)
         ADB = -0.276/RADPH+0.62
         AD1 = ADA*GOPH + ADB
         AD = 1.0/AD1
         BDA = 0.025*RADPH-2.711
         BDB = -0.033*RADPH+0.071
         BD = BDA*GOPH+BDB
         DR = AD*Y3PH + BD
C================================================================
       IF(DR.LE.0.0001.OR.AD1.LT.0.0001) THEN
         ERRMES = 4
C AMM 12/23/96 Changed following line; ENDOFF does not require a
C              parameter         
C         CALL ENDOFF(ERRMES)
         CALL ENDOFF
         GOTO 10
       ENDIF
C================================================================
         D = (1.0/DR)**1.429
         AEA = -0.019*RADPH+0.06
         AEB = 0.0052*RADPH+0.996
         AE = 1.0/(AEA*GOPH+AEB)
         BEK = -0.293*RADPH+0.32
         BE = SQRT((1.0+(GOPH-0.44)**2/0.7)*0.255)+BEK
         E1 = (AE*D+BE)/D
         P = 1.0
       IF(E1.LT.1.0) P = -1.0
         E = SQRT(P*ALOG(E1))
         V1 = E*D/(1.0+E)
C===============================================================
         AFA = -0.158/RADPH+0.038
         AFB = -0.115*RADPH+0.29
         AF = AFA*GOPH + AFB
         BFA = 0.0445/RADPH-0.0321
         BFB = -0.0092/RADPH + 0.155
         GOPH1 = GOPH
       IF(GOPH.LE.0.0) GOPH1 = 0.001
         BF = BFA/GOPH1+BFB
         FY = -AF*Y3PH+BF
       IF(FY.LT.0.0) FY = 0.0
         FX = SQRT(V1**2+FY**2)-V1
C===============================================================
         VX = Y2PH-(V1+Y3PH+FX)
         CDA1 = E**2*(D+VX)**2 - VX**2
       IF(CDA1.LT.0.0) CDA1 = 0.0
C===============================================================
C      CORRECT THE DISCHARGE COEFFICIENT USING THE GATE SEAL
C      CORRECTION COEFFICIENT
C===============================================================
         CSCDA = CSDA(IR)
       IF(CSDA(IR).EQ.0.0) THEN
           CSCDA = 0.39*GOPH+0.85
       ELSEIF(CSDA(IR).EQ.1.0) THEN
           CSCDA = 0.11*GOPH + 0.935
       ENDIF
         SCDA = (SQRT(CDA1)+FY)*CSCDA
C================================================================
 10   CONTINUE
      RETURN
      END
