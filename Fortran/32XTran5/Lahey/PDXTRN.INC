        INTEGER*4 DWFLAG
        CHARACTER ASCOND*10, LUDESC*25
        COMMON /PDXTN4/LUCODE(22)
        COMMON /PDXTN5/HVDWF(22,24)
        COMMON /PDXTRN/IPSTEPS,ISTPCYC,NRGPNT,NSAVEC,SCOND(10)
        COMMON /PDXTN1/ASCOND(10),JELEV
        COMMON /PDXTN2/DWFlag(NEE),LUDWF(NEE)
        COMMON /PDXTN3/LUDESC(22)
C AMM 12/23/96 Added following constant per VCA
C AMM 1/13/97 Change the NUM_CP parameter to total number of custom
C             shapes desired (generally, 3+desired number; the 3 corresponds
C             to shapes 13-15; thereafter, custom shape numbers increase
C             from 21.
        PARAMETER(NUM_CP=13,LASTCP=20+NUM_CP-3)
