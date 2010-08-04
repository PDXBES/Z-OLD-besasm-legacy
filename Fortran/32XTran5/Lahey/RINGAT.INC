C      FILE: RINGAT.INC
C================================================================
C      THIS FILE DEFINES THE OTHER VARIABLES FROM THE INPUT FILE
C      THAT ARE NOT ASSOCIATED WITH THE GATE GEOMETRY
C================================================================
CPDX--SM, 11/11/91--NTVAL CHANGED TO NTVALR

      INTEGER NTVALR,CONTRL, RGIMAX,CRTJUN,DISCND
      DIMENSION CONTRL(NRADG),CRTJUN(NRADG),NTVALR(NRADG),
     +          DNYCLS(NRADG),DNYOPN(NRADG),GCDPTH(NRADG,10),
     +          GDOPEN(NRADG,10), RTGCLS(NRADG), RTEGPN(NRADG),
     +          GPNMAX(NRADG),GPNMIN(NRADG),DISCND(NRADG)
      COMMON /RINGAT/ YDELTA,QDELTA,RGIMAX,CONTRL,CRTJUN,DNYCLS,
     +   DNYOPN, RTGCLS, RTEGPN, GCDPTH, GDOPEN, NTVALR,DISCND,
     +   GPNMAX, GPNMIN
C================================================================
