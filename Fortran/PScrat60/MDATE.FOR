      FUNCTION MDATE(IDAY,MONTH,IYEAR)
C=======================================================================
C     FUNCTION TO CONVERT STD DATE (MM/DD/YY) TO YEAR+JULIAN
C
C=======================================================================
C
C
      MDATE=IYEAR*1000
      IFEB  = 28
      IF((IYEAR/4)*4-IYEAR.EQ.0) IFEB = 29
C
      GO TO (1,2,3,4,5,6,7,8,9,10,11,12),MONTH
 12   MDATE = MDATE+30
 11   MDATE = MDATE+31
 10   MDATE = MDATE+30
  9   MDATE = MDATE+31
  8   MDATE = MDATE+31
  7   MDATE = MDATE+30
  6   MDATE = MDATE+31
  5   MDATE = MDATE+30
  4   MDATE = MDATE+31
  3   MDATE = MDATE+IFEB
  2   MDATE = MDATE+31
  1   MDATE = MDATE+IDAY
      RETURN
      END

              
