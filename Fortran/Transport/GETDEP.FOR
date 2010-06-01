      REAL FUNCTION GETDEP(DELTA,Y,TOP,DFULL,PEXP)
C=======================================================================
C     THIS FUNCTION CALCULATES THE ARC LENGTH OF A POWER FUNCTION
C          CURVE BETWEEN Y-DELTA AND Y.
C=======================================================================
      POW    = 1.0/PEXP
      COEF   = 0.5*TOP/DFULL**POW
      DIFF   = Y - DELTA
      IF(DIFF.LE.0.0) DIFF = 1.0E-05
      X      = COEF*(Y**POW - DIFF**POW)
      GETDEP = SQRT(X**2.0  + DELTA**2.0)
      RETURN
      END
