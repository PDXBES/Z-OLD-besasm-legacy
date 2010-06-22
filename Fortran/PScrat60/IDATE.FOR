      SUBROUTINE IDATE(JULIAN,MONTH,IDAY,IYR)
C=======================================================================
C ROUTINE TO CONVERT JULIAN DATE BACK TO MO/DAY/YR
C
      DIMENSION MDAYS(12)
      DATA MDAYS/31,28,31,30,31,30,31,31,30,31,30,31/
C=======================================================================
      MDAYS(2)=28
      IYR=JULIAN/1000
      JDAY=JULIAN-IYR*1000
      IF(MOD(IYR,4).EQ.0)MDAYS(2)=29
      JSUM=0
      DO 10 I=1,12
          JSUM=JSUM+MDAYS(I)
          IF(JDAY.LE.JSUM)THEN
             MONTH=I
             IDAY=JDAY-(JSUM-MDAYS(I))
             GO TO 20
             ENDIF
10    CONTINUE
20    CONTINUE
      RETURN
      END
