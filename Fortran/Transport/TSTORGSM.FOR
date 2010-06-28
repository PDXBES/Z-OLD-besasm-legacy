      SUBROUTINE TSTORG
C=======================================================================
C     ROUTINE TO COMPUTE NEW STORAGE VOLUME AND OUTFLOW FOR
C     STORAGE UNIT IN THE TRANSPORT BLOCK.
C     MODIFIED FOR TRANSPORT BLOCK BY S.J.NIX, SEPTEMBER 1981.
C     REVISED, JANUARY 1983)
C
CPDX  MODIFIED BY STEVE MERRILL/BROWN & CALDWELL TO INCORPORATE 6-PUMP
C     OPTION AND OVERFLOW ROUTING FROM STORAGE STRUCTURE.
C=======================================================================
CPDX  MODIFIED BY VIRGIL ADDERLEY & STONER BELL, CH2M HILL 10/6/93:
C        CHANGED CODE TO OPERATE ON CURRENT FLOWS, STORAGE DEPTH AND
C        NOT DIVIDE FLOWS IN HALF, AND FIXED CONTINUITY PROBLEMS.
C=======================================================================
      INCLUDE 'TAPES.INC'
      INCLUDE 'HUGO.INC'
      INCLUDE 'NEW81.INC'
      INCLUDE 'TST.INC'
C=======================================================================
      DIMENSION SURGE2(NET),SDE(17),SST(17),DEPTHL(NTSE)
CPDX--SM 4/2/91---------------------------------------------------
C DIMENSIONS CHANGED ON A1,A2--note change in tst.inc also.
C
      DIMENSION SAO(17),SAT(17),SSTOP(NTSE),A1(NTSE,2),
     +         A2(NTSE,2),KLIST(NTSE),ION(NTSE)
CPDX--------------------------------------------------------------
      EQUIVALENCE (SURGE2(1),P2(1)),(A1,B1),(A2,B2)
      DATA KLIST/NTSE*0/
C=======================================================================
      REAL*4 QOUST2,QOUST1,QOUST
      QOUST  = 0.0
      QOUST1 = 0.0
      QOUST2 = 0.0
      DEPTH  = 0.0
      FLOOD  = 0.0
      EXCESS = 0.0
      IS     = KSTORE(M)
      STORE(IS) = 0.0
C	MINT is the number of depth-area-storage points on rating curve
      MINT   = MINTS(IS)
      IF(QINST+STORL(IS).LE.0.0.AND.N.GE.1) GO TO 200
C=======================================================================
C     INITIALIZATION IS DONE WHEN CALLED FROM SUB INITAL (VIA ROUTE)
C     WHEN CALLED FROM INITAL, N = 0.
CPDX--N (TIMESTEP) AND M (ELEMENT NO) ARE IN HUGO.INC
C=======================================================================
      SO2DT2    = 0.0
      DO 110 MM = 1,MINT
      SDE(MM)   = TSDEP(IS,MM)
  110 SST(MM)   = TSTORE(IS,MM)
      IF(N.GE.1) GO TO 200
      ION(IS)    = 0
      STORE(IS)  = 0.0
      QINSTL(IS) = 0.0
      QOUSTL(IS) = 0.0
      DEPTHL(IS) = 0.0
C+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
      IF(LOUT(IS).GE.3) CALL TINTRP(SDE,SST,MINT,TDSTOP(IS),SSTOP(IS))
C+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
CPDX-  IF(STORL(IS).GT.0.0)CALL TINTRP(SST,SDE,MINT,STORL(IS),DEPTHL(IS))
CPDX---------------
        NEWSTOR = QINST*DT + STORL(IS)

      IF(STORL(IS).GT.0.0)CALL TINTRP(SST,SDE,MINT,NEWSTOR,DEPTHL(IS))
CPDX-----------------
C+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  200 IF(QINST+STORL(IS).LE.0.0) GO TO 500
      MIN = 1
      IF(LOUT(IS).LE.2) GO TO 210
CPDX      IF(DEPTHL(IS).LT.TDSTOP(IS)+1.E-06)then
      IF(DEPTHL(IS).LT.TDSTAR(IS,1)+1.E-06)then
        ION(IS) = 0
      else
       kkk=lout(is)-1
       do 201 NPS=1,kkk
        IF(DEPTHL(IS).GE.TDSTAR(IS,NPS)) then
          ION(IS) = NPS
          endif
201    continue
202    do 203 NPS=kkk,1,-1
        if(depthl(is).lt.tdstar(is,NPS).and.ion(is).gt.NPS)then
           ion(is)=NPS
           endif
203    continue
ccc        IF(DEPTHL(IS).LT.TDSTAR(IS,2).AND.ION(IS).EQ.3) ION(IS) = 2
      ENDIF
204    KKK = ION(IS)
       IF(ION(IS).GT.0)then
         IF (KKK.LT.(LOUT(IS)-1)) THEN
            D1 = TDSTAR(IS,KKK)
            D2 = TDSTAR(IS,KKK+1)
            Q1 = TQPUMP(IS,KKK)
            Q2 = TQPUMP(IS,KKK+1)
            Q3 = Q1 + (Q2-Q1)*(DEPTHL(IS)-D1)/(D2-D1)
            SO2DT2 = Q3*DT
         ELSE
            SO2DT2 = TQPUMP(IS,KKK)*DT
         ENDIF
       else
         so2dt2=0
       endif
CPDX--------------------------------------------------------------
      SAT(MIN)  = SSTOP(IS) + SO2DT2
      SAT(MINT) = SST(MINT) + SO2DT2
      GO TO 300
  210 DO 220 MM = 1,MINT
      SAO(MM)   = TSQOU(IS,MM)*DT
      SAT(MM)   = SST(MM) + SAO(MM)
      IF(SAO(MM).LE.0.0) MIN = MM
  220 CONTINUE
C Calculate current storage based on new inflow & last and storage
CPDX  300 STERMS  = (QINST+QINSTL(IS)-QOUSTL(IS))*DT + STORL(IS)
CPDX  Let STERMS be the total volume handled this current time step:

  300   STERMS = (QINST)*DT + STORL(IS)
CPDX      IF(STERMS.LE.SAT(MIN).AND.QOUSTL(IS).GT.0.0) STERMS = SAT(MIN)

      IF(STERMS.LE.SAT(MINT)) GO TO 350	!	Current volume within tank limits

CPDX--SM 4/2/91---------------------------------------------------
c store excess as overflow if lout(is)=4
c check to see if pumping rate should have increased this time step
      if(lout(is).ge.3)then
          if(ion(is).lt.lout(is)-1)then
            ion(is)=lout(is)-1
            goto 204
            endif
      endif
c
      STERMS = SAT(MINT)
      IF(LOUT(IS).LE.3)THEN
CPDX VCA 10/7/93:  This is not always valid:  FLOOD = QINST*DT-TSQOU(IS,MINT)*DT
          FLOOD = (QINST*DT+STORL(IS)) - SAT(MINT)
          IF(FLOOD.LE.0.0001) FLOOD = 0.0
C=======================================================================
C     STORE FLOOD VOLUME AS SURCHARGE (AT END OF SUBROUTINE).
C=======================================================================
       KLIST(IS) = KLIST(IS) + 1
       IF(KLIST(IS).EQ.1.OR.MOD(KLIST(IS),10).EQ.0) THEN
             IF(JCE.EQ.0) WRITE(N6,340) NOE(M),N,FLOOD,KLIST(IS)
             IF(JCE.EQ.1) WRITE(N6,341) KOE(M),N,FLOOD,KLIST(IS)
             ENDIF
CPDX VCA 10/7/93:  NOT NEEDED
**       ELSE
**       QOUST2=SO2DT2/DT
**       QOUST1=MAX(QINST-QOUST2,0)        
       ENDIF
CPDX--------------------------------------------------------------
  350 IF(STERMS.LE.0.0001) STERMS = 0.0
      IF(LOUT(IS).GE.3) GO TO 410
C+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
      CALL TINTRP(SAT,SAO,MINT,STERMS,SO2DT2)
C+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  410                     STORE(IS) = STERMS - SO2DT2
        IF(STORE(IS).LE.0.001) STORE(IS) = 0.0

CPDX--SM 4/2/91---third pump--------------------------------------
      IF(LOUT(IS).LE.3)THEN
CPDX VCA 10/6/93 :  CHECK FOR CONTINUITY PROBLEMS----------

         IF(STERMS.LT.SO2DT2)THEN                ! Pumping out too much
            SO2DT2=STERMS-SSTOP(IS)
            IF((SO2DT2/DT).LT.TSQOU(IS,1)) THEN 
CPDX            Don't pump at a rate lower than the lowest pump rate
                SO2DT2 = 0      
                STORE(IS)= ((QINST)*DT+STORL(IS))-SO2DT2
            ELSE
                STORE(IS)=SSTOP(IS)
            ENDIF
            EXCESS = 0.0
         ELSE

            STORE(IS) =  ((QINST)*DT + STORL(IS))-SO2DT2
            IF (STORE(IS).GT.SST(MINT)) THEN     ! We have overflow, store upstream
                FLOOD = (QINST*DT+STORL(IS)) - (SO2DT2+SST(MINT))
                IF(FLOOD.LE.0.0001) FLOOD = 0.0
                STORE(IS) = SST(MINT)
            ENDIF
         ENDIF
           QOUST = SO2DT2/DT
*

      ELSE
C               MULTIPLE PUMPS WITH OVERFLOW FROM TANK
C         	QOUST2 IS UNDERFLOW, QOUST1 IS OVERFLOW
C	Check overall volume continuity:

         IF(STERMS.LT.SO2DT2)THEN                ! Pumping out too much
            SO2DT2=STERMS-SSTOP(IS)
            IF((SO2DT2/DT).LT.TQPUMP(IS,1)) THEN
CPDX            Don't pump at a rate lower than the lowest pump rate
                SO2DT2 = 0      
                STORE(IS)= ((QINST)*DT+STORL(IS))-SO2DT2
            ELSE
                STORE(IS)=SSTOP(IS)
            ENDIF
            EXCESS = 0.0
         ELSE
            STORE(IS) =  ((QINST)*DT + STORL(IS))-SO2DT2
            IF (STORE(IS).GT.SST(MINT)) THEN     ! We have overflow
                EXCESS = STORE(IS)-SST(MINT)
                STORE(IS) = SST(MINT)
            ENDIF
         ENDIF
*
            QOUST2=SO2DT2/DT

CPDX VCA 10/8/93:  This can't be right:    QOUST1=MAX(QINST-QOUST2,0)
          QOUST1 = EXCESS/DT
          QOUST = QOUST2+QOUST1
CPDX 
       ENDIF
CPDX--------------------------------------------------------------
C+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
      CALL TINTRP(SST,SDE,MINT,STORE(IS),DEPTH)
C+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
CPDX--SM 4/2/91---------------------------------------------------
C DIMENSIONS CHANGED ON A1,A2 AND D0, NEXT STATEMENT COMMENTED OUT,
C    qoust1 set to zero on entry, and is reset for regulators.
C      QOUST1 = 0.0
C
      IF(LOUT(IS).EQ.2.AND.DEPTH.GT.D0(IS,2))
     &   QOUST1  = A1(IS,2)*(DEPTH-D0(IS,2))**A2(IS,2)
c
cpdx for regulator routine
      if(lout(is).lt.0)then
            qoust1=AMIN1(so2dt2/dt,(geom1(m)))
            qoust2=AMAX1(qoust-qoust1,FLOAT(0))
         else
            QOUST2 = QOUST - QOUST1
            IF(QOUST2.LE.0.0) QOUST2 = 0.0
         endif
CPDX--------------------------------------------------------------
  500 STORL(IS)  = STORE(IS)
      QOUSTL(IS) = QOUST
      QINSTL(IS) = QINST
      DEPTHL(IS) = DEPTH
      SURGE2(M)  = FLOOD 
      RETURN
C=======================================================================
  340 FORMAT (' ===> WARNING !! FROM SUB TSTORG.  STORAGE UNIT ',I7,' IS
     1 FLOODING.  EXCESS VOLUME BEING STORED AS SURCHARGE.',/,
     2 7X,'TIME STEP = ',I6,'.  FLOOD VOLUME = ',1PE14.7,' CU FT.',
     3' OCCURANCE # ',I6)
  341 FORMAT (' ===> WARNING !! FROM SUB TSTORG.  STORAGE UNIT ',A10,
     1' IS FLOODING.  EXCESS VOLUME BEING STORED AS SURCHARGE.',/,
     2 7X,'TIME STEP = ',I6,'.  FLOOD VOLUME = ',1PE14.7,' CU FT.',
     3' OCCURANCE # ',I6)
C=======================================================================
      END
