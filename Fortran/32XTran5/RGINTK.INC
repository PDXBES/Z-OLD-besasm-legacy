C      FILE: RGINTK.INC
C==================================================================
C      DEFINES THE VARIABLES DESCRIBING THE CONDUIT SECTION
C      HOUSING THE RADIAL GATE
C==================================================================
       INTEGER RGJUP,RGJDN
       DIMENSION UPINV(NRADG),DNINV(NRADG),UPDEP(NRADG),RGJUP(NRADG),
     +        RGJDN(NRADG),RLRUFF(NRADG),NRGPUP(NRADG),NRGPDN(NRADG)
       COMMON /RGINTK/ UPINV,DNINV,UPDEP,RGJUP,RGJDN,CFMANN,RLRUFF,
     +                 NRGPUP,NRGPDN
C==================================================================
