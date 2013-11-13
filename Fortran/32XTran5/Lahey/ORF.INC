C=======================================================================
C     NVORF  ==> Number of variable orifices.
C     NVOTIM ==> Number of data points for variable orifices.
C=======================================================================
CDPX--SM, 11/16/91--NVORF MOVED TO TAPES.INC, NVOTIM REPLACED WITH NTVAL
CPDX      PARAMETER(NVORF=20,NVOTIM=25)
      COMMON/ORF/LORIF(NEO),AORIF(NEO),CORIF(NEO),
     +           VORIF(NVORF,NTVAL,3),NVOR,NORIF
CPDX     +           VORIF(NVORF,NVOTIM,3),NVOR,NORIF
