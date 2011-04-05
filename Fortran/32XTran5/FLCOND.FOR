      SUBROUTINE FLCOND(BYPASS)
C================================================================
C     DETECTS SPECIAL FLOW CONDITIONS AND DIRECTS THE
C                   PROGRAM AS APPROPRIATE
C================================================================
        INCLUDE 'TAPES.INC'
        INCLUDE 'RDGATE.INC'
        INCLUDE 'RGINTK.INC'
        INCLUDE 'RINGAT.INC'
        INCLUDE 'RGFLOW.INC'
        INCLUDE 'CONST.INC'
        INCLUDE 'PIPE.INC'
        INCLUDE 'LRG.INC'
C======================================================================
      INTEGER BYPASS
C======================================================================
C                          GATE CLOSED
C               Bypass all Radial Gate Computations
C=======================================================================
         IF(GOPEN(IR).LE.0.01) THEN
            RGC(IR) = 1
            BYPASS = 1
            QGATE(IR) = 0.0
            GOTO 30
         ENDIF            
C================================================================
C                ZERO DEPTH AT UPSTREAM NODE
C            Bypass all Radial Gate Computations
C================================================================
         IF(HU2.LE.0.01) THEN
            RGC(IR) = 2
            BYPASS = 2
            QGATE(IR) = 0.0
            GOTO 30
         ENDIF
C================================================================
C                    FLOW REVERSAL
C        Use "user-specified" Discharge Coefficient
C
C       DURATION OF FLOW REVERSAL IS GIVEN BY NEGATIVE
C       FLOWS IN TIME HISTORY OUTPUT OF RADIAL GATES
C================================================================
         IF(DISCND(IR).NE.2) THEN
           YDDNN = DNYRG - YSYFON(IR)
           IF(HU2.LE.YDDNN) THEN
             RGC(IR) = 8
             BYPASS = 8
             HEFFRG = YDDNN - HU2
             AREARG = GWDTH(IR)*GOPEN(IR)
             IF(YDDNN.GE.GOPEN(IR)) THEN
                QGATE(IR) = -1.0*CFLRVS(IR)*AREARG*(SQRT(2.*GC*HEFFRG))
             ELSE
                QGATE(IR) = 0.0
             ENDIF
             GOTO 30
           ENDIF
         ENDIF
C================================================================
C                   UNRESTRICTED DISCHARGE
C     Use Manning Equation based on Up or Downstream Section
C================================================================
            HU2FUD = HU2 + 0.1
         IF(HU2FUD.LE.GOPEN(IR)) THEN
            RGC(IR) = 3
            BYPASS = 3
            IRG = NRGPUP(IR)
            RLNKSL = SQRT((ZU(IRG)-ZD(IRG))/LEN(IRG))
            AREARL = WIDE(IRG)*UPYRG
            WETPRL = WIDE(IRG)+(2.0*UPYRG)
            HRADRL = AREARL/WETPRL
            QGATE(IR) = CFMANN*AREARL*(HRADRL**0.666667)*
     +                  RLNKSL/RLRUFF(IR)
            GOTO 30
         ENDIF
C================================================================
C               USER SPECIFIED DISCHARGE COEFFICIENT
C                Use H = Y1 - Y2 and orifice euation.
C================================================================
         IF(DISCND(IR).EQ.3) THEN
            RGC(IR) = 9
            BYPASS = 9
            HEFFRG = HU2 - YDDNN
            AREARG = GWDTH(IR)*GOPEN(IR)
            QGATE(IR) = CDRG(IR)*AREARG*(SQRT(2.*GC*HEFFRG))
            GOTO 30
         ENDIF
C================================================================
C    IF JUNCTION DEPTH EXCEEDS DEPTH AT INTAKE:
C      SET DEPTH AT GATE EQUAL TO INTAKE DEPTH
C
C    NB:"SURCHARGE ONLY OCCURS WHEN JUNCTION IS SURCHARGED"
C================================================================
         IF(HU2.GE.UPDEP(IR)) THEN
            RGC(IR) = 7
            IF(DISCND(IR).NE.2) THEN
              BYPASS = 7
              HEFFRG = HU2 - YDDNN
              AREARG = GWDTH(IR)*GOPEN(IR)
              QGATE(IR) = CDRG(IR)*AREARG*(SQRT(2.*GC*HEFFRG))
            ENDIF
            GOTO 30
         ENDIF
C================================================================
 30    CONTINUE
       RETURN
       END
