       SUBROUTINE RGATE(QRG)
C================================================================
C          DISCHARGE OF RADIAL GATE STRUCTURES
C----------------------------------------------------------------
C      THIS PROGRAM COMPUTES THE DISCHARGE OF RADIAL GATES
C        THE COMPUTATIONS ARE BASED ON THE DISCHARGE ALGORITHMS
C        DEVELOPED BY THE U.S.BUREAU OF RECLAMATION
C          (Technical Report No.REC-ERC-83-9: 1983)
C
C              Written by: B.C.Young and W.James
C                          School of Engineering
C                          University of Guelph
C                          Guelph, Ontario
C                          Canada    N1G 2W1
C                          Tel: (519) 824-4120 x.3055
C                          Fax: (519) 824-2270
C================================================================
       INCLUDE 'TAPES.INC'
       INCLUDE 'RDGATE.INC'
       INCLUDE 'RINGAT.INC'
       INCLUDE 'RGFLOW.INC'
       INCLUDE 'LRG.INC'
C================================================================
       INTEGER BYPASS
       BYPASS = 0
       RGC(IR) = 0
C================================================================
C       COMPUTE RADIAL GATE SECTION VARIABLES
C================================================================
       CALL RGSECT
C================================================================
C       DETERMINE SPECIAL FLOW CONDITIONS
C================================================================
       CALL FLCOND(BYPASS)
       IF(BYPASS.NE.0) GOTO 20
C================================================================
C       COMPUTE DISCHARGE ASSUMING FREE FLOW
C================================================================
       CALL RGFREE
C================================================================
C       COMPUTE DISCHARGE ASSUMING SUBMERGED FLOW
C       IF DISCND = 1   ==>  USE SUBMERGED FLOW ALGORITHMS
C================================================================
       IF(DISCND(IR).NE.1) THEN
          RGC(IR) = 4
          GOTO 10
       ENDIF
       CALL RGSUB
C================================================================
C       DETERMINE IF FLOW CONDITION IS FREE OR SUBMERGED
C================================================================
 10    CONTINUE
       CALL FLDETM
 20    CONTINUE
       QRG = QGATE(IR)
C================================================================
       RETURN
       END
