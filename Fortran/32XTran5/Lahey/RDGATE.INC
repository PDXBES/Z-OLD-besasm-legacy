C       FILE: RDGATE.INC
C================================================================
C       THIS FILE DEFINES AND DIMENSIONS THE VARIABLES 
C           DESCRIBING THE RADIAL GATE GEOMETRY
C================================================================
      INTEGER MM,ERRMES
      REAL UPYRG, DNYRG
      DIMENSION GWDTH(NRADG),GRAD(NRADG),PINHGT(NRADG),GOPEN(NRADG),
     +         YINVRT(NRADG),YSYFON(NRADG),UPTRNS(NRADG),DNTRNS(NRADG),
     +         CFDA(NRADG),CSDA(NRADG),NRGERR(NRADG,5)
      COMMON /RDGATE/ MM,GWDTH,GRAD,PINHGT,GOPEN,YINVRT,NRGERR,
     +      YSYFON, UPTRNS, DNTRNS,CFDA,CSDA,ERRMES,UPYRG,DNYRG
C================================================================
