      SUBROUTINE RGSECT
C================================================================
C     COMPUTES DEPTHS AND FLOW AREAS OF RADIAL GATE CALCULATIONS
C================================================================
       INCLUDE 'TAPES.INC'
       INCLUDE 'RDGATE.INC'
       INCLUDE 'RGINTK.INC'
       INCLUDE 'RINGAT.INC'
       INCLUDE 'CONST.INC'
       INCLUDE 'LRG.INC'
       INCLUDE 'PIPE.INC'
C================================================================
         UPYRG = UPYRG - UPINV(IR)
         HU1 = UPYRG
         DNYRG = DNYRG - DNINV(IR)
         HD1 = DNYRG
         HU2 = UPYRG + YINVRT(IR)
C===============================================================
C      DETERMINE UP & DOWNSTREAM PIPES
C===============================================================
         NRUP = NRGPUP(IR)
         NRDN = NRGPDN(IR)
C===============================================================
C      COMPUTE UPSTREAM FLOW AREA
C===============================================================
         AREAUP = UPYRG * (WIDE(NRUP)+ UPYRG)
         IF(UPYRG.GT.UPDEP(IR)) THEN
            AREAUP = UPDEP(IR) * (WIDE(NRUP)+ UPDEP(IR))
         ENDIF
C===============================================================
C      COMPUTE DOWNSTREAM FLOW AREA
C===============================================================
       IF(DISCND(IR).EQ.1) THEN
           AREADN = DNYRG * (WIDE(NRDN) + DNYRG)
           IF(DNYRG.GT.UPDEP(IR)) THEN
              AREADN = UPDEP(IR) * (WIDE(NRDN)+ UPDEP(IR))
           ENDIF
         ENDIF
C================================================================
       RETURN
       END
