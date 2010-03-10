      FUNCTION PRINTP1(IPUMP,VVWELL)
C
C   Finds pumping rate for VWELL for IPTYP 1 pumps.
C   Note that VRATES increase and are checked in INDAT2.FOR.
C
C   Created by C. Moore, CDM  5/97
C
      INCLUDE 'TAPES.INC'
      INCLUDE 'WEIR.INC'
      DO 100 K = 1, NPRATE(IPUMP)-1
      KK = K
  100 IF (VVWELL.LT.VRATE(IPUMP,KK)) GO TO 200
      KK = NPRATE(IPUMP)
  200 TEMP = PRATE(IPUMP,KK)
      CONTINUE
cim  WRITE(n6,*) 'PRINTP1',VVWELL,TEMP
      PRINTP1 = TEMP
      RETURN
      END
C
C
      FUNCTION PRINTP2(IPUMP,YDEP)
C
C   Finds pumping rate for YDEP for IPTYP 2 pumps.
C   Note that VRATES increase and are checked in INDAT2.FOR.
C
C   Created by C. Moore, CDM  5/97
C
      INCLUDE 'TAPES.INC'
      INCLUDE 'WEIR.INC'
      DO 100 K = 1, NPRATE(IPUMP)-1
      KK = K
  100 IF (YDEP.LT.VRATE(IPUMP,KK)) GO TO 200
      KK = NPRATE(IPUMP)
  200 TEMP = PRATE(IPUMP,KK)
  300 CONTINUE
cim   WRITE(n6,*) 'PRINTP2',YDEP,TEMP
      PRINTP2 = TEMP
      RETURN
      END
C
C
C
      FUNCTION PRINTP3(IPUMP,DH)
C
C   Linearly interpolate to find pumping rate for DH for IPTYP 3 pumps.
C   Note that VRATES decrease and are checked in INDAT2.FOR.
C
C   Created by C. Moore, CDM  5/97
C
      INCLUDE 'TAPES.INC'
      INCLUDE 'WEIR.INC'
      IF(DH.GE.VRATE(IPUMP,1)) THEN
      TEMP = PRATE(IPUMP,1)
      GO TO 300
      ENDIF
      DO 100 K = 2, NPRATE(IPUMP)
      KK = K
  100 IF (DH.GT.VRATE(IPUMP,KK)) GO TO 200
cim gets here if DH is less than last VRATE
      TEMP = PRATE(IPUMP,NPRATE(IPUMP))
      GO TO 300
  200 RATIO = (DH-VRATE(IPUMP,KK-1))/
     a        (VRATE(IPUMP,KK)-VRATE(IPUMP,KK-1))
      TEMP = PRATE(IPUMP,KK-1) +
     a         RATIO * (PRATE(IPUMP,KK)-PRATE(IPUMP,KK-1))
  300 CONTINUE
cim   WRITE(n6,*) 'PRINTP3',DH,TEMP
      PRINTP3=TEMP
      RETURN
      END


      FUNCTION PRINTP4(IPUMP,YDEP)
C
C   Interpolates to find pumping rate for YDEP for IPTYP 4 pumps.
C   Note that VRATES increase and are checked in INDAT2.FOR.
C
C   Created by C. Moore, CDM  5/97
C
      INCLUDE 'TAPES.INC'
      INCLUDE 'WEIR.INC'
      DO 100 K = 1, NPRATE(IPUMP)
      KK = K
  100 IF (YDEP.LT.VRATE(IPUMP,KK)) GO TO 200
      TEMP = PRATE(IPUMP,NPRATE(IPUMP))
      GO TO 300
  200 RATIO = (YDEP-VRATE(IPUMP,KK-1))/
     1        (VRATE(IPUMP,KK)-VRATE(IPUMP,KK-1))
      IF (RATIO.LT.0.0) RATIO = 0.0
      TEMP = PRATE(IPUMP,KK-1) +
     1          RATIO * (PRATE(IPUMP,KK)-PRATE(IPUMP,KK-1))
  300 CONTINUE
cim   WRITE(n6,*) 'PRINTP4',YDEP,TEMP
      PRINTP4 = TEMP
      RETURN
      END
