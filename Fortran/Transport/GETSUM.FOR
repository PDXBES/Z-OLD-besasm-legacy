      SUBROUTINE GETSUM(N,START,DELTA,INTEG,TOP,DFULL,PEXP)
      REAL INTEG
C=======================================================================
C     THIS SUBROUTINE SUMS THE ARC LENGTH OF A POWER FUNCTION 
C     BETWEEN THE VALUES START AND START + N*DELTA.
C     N     = NUMBER OF PANELS
C     INTEG = INTEGRAL APPROXIMATION
C     START = STARING Y VALUE
C=======================================================================
      Y        = START 
      INTEG    = 0.0
      DO 100 I = 1,N
      Y        = Y     + DELTA
100   INTEG    = INTEG + GETDEP(DELTA,Y,TOP,DFULL,PEXP)          
      RETURN
      END
