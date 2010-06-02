C     Last change:  AMM  17 May 2000    1:17 pm
      SUBROUTINE TSTRDT
C=======================================================================
C     INPUT AND INITIALIZATION ROUTINE FOR INTERNAL STORAGE IN THE
C     TRANSPORT BLOCK.
C     MODIFIED FROM ROUTINE IN THE STORAGE/TREATMENT BLOCK BY
C     S.J.NIX, SEPTEMBER 1981.  (REVISED, JANUARY 1983)
C     LAST UPDATED NOVEMBER, 1989
C=======================================================================
      INCLUDE 'TAPES.INC'
      INCLUDE 'INTER.INC'
      INCLUDE 'HUGO.INC'
      INCLUDE 'NEW81.INC'
      INCLUDE 'TST.INC'
CPDX--SM, 4/2/91-------------------------------------------------------
C     SEE BELOW FOLLOWING STATEMENT LABEL 410.  Dimensions changed on
c       A1 and A2.  Also requires a change in TST.INC.
C
C      DIMENSION A1(NTSE),A2(NTSE)
C
      DIMENSION A1(NTSE,2),A2(NTSE,2)
CPDX--------------------------------------------------------------------
      EQUIVALENCE (A1,B1),(A2,B2)
C=======================================================================
C     LOOP THRU KSTOR ELEMENTS
C=======================================================================
      DO 1270 IS = 1,KSTOR
C=======================================================================
C     FIND INTERNAL NUMBER OF THIS STORAGE ELEMENT.
C=======================================================================
      DO 50 I = 1,NE
      M       = I
      IF(KSTORE(I).EQ.IS) GO TO 100
   50 CONTINUE
C=======================================================================
C>>>>>>>>  READ DATA GROUP G1  <<<<<<<<
C=======================================================================
cpdx----
100   if(ntype(m).eq.23)go to 1270
      READ(N5,*,ERR=888) CC,LOUT(IS)
C=======================================================================
C>>>>>>>>  READ DATA GROUP G2  <<<<<<<<
C=======================================================================
CPDX--SM, 4/2/91-------------------------------------------------------
C  ALL STOPS CHANGED TO JSTOP=1, STOP AT END OF ROUTINE
      JSTOP=0
CPDX--------------------------------------------------------------------
      MINT      = 0
      STOR      = 0.0
      DO 230 MM = 1,17
      READ(N5,*,ERR=888) CC
      BACKSPACE N5
      IF(CC.NE.'G2') GO TO 240                          
      IF(LOUT(IS).EQ.0) THEN
                        READ(N5,*,ERR=888) CC,TSDEP(IS,MM),
     +                      TSAREA(IS,MM),TSTORE(IS,MM),TSQOU(IS,MM)
                        ELSE
                        READ(N5,*,ERR=888) CC,TSDEP(IS,MM),
     +                           TSAREA(IS,MM),TSTORE(IS,MM)
                        ENDIF
      IF(MM.GT.1.AND.TSDEP(IS,MM)+TSAREA(IS,MM).LE.0.0) GO TO 240
      MINT = MINT+1
      STOR = STOR+TSTORE(IS,MM)
  230 CONTINUE
  240 MINTS(IS) = MINT
      IF(TSDEP(IS,1)+TSTORE(IS,1).GT.0.0) THEN
                                  IF(JCE.EQ.0) WRITE(N6,220) NOE(M)
                                  IF(JCE.EQ.1) WRITE(N6,221) KOE(M)
                                  JSTOP=1
                                  ENDIF
CPDX--SM, 4/6/91-------------------------------------------------------
C ADD AN ERROR TRAP
      IF(MINT.EQ.0)THEN
        WRITE(N6,'(A,I5)')' NO DATA FOR STORAGE NODE',IS
        WRITE(*,999)
        STOP
        ENDIF
CPDX--------------------------------------------------------------------
      IF(STOR.GT.0.0) GO TO 260
      DO 250 MM     = 2,MINT          ! Calculate Storage from surface area
  250 TSTORE(IS,MM) = TSTORE(IS,MM-1) + (TSDEP(IS,MM) - 
     +            TSDEP(IS,MM-1))*(TSAREA(IS,MM)+TSAREA(IS,MM-1))/2.0
  260  CONTINUE
C=======================================================================
cpdx      IF(LOUT(IS).LE.0) GO TO 600
      IF(LOUT(IS).eq.0) GO TO 600
      IF(LOUT(IS).GE.3) GO TO 500
C=======================================================================
C>>>>>>>>  READ DATA GROUP G3  <<<<<<<<
C=======================================================================
CPDX--SM, 4/6/91-------------------------------------------------------
c read coefs for regulator station.  Assumes Lout(is)=-1 or -2, so 
c change the following statement to absolute value.
ccc      LT           = LOUT(IS)
      LT           = abs(LOUT(IS))
CPDX--------------------------------------------------------------------
      DO 410    MM = 1,MINT
  410 TSQOU(IS,MM) = 0.0
CPDX--SM, 4/2/91-------------------------------------------------------
C     PRESUMABLEY, THE FOLLOWING COEFFICIENTS WERE MENT TO BE
C     READ FOR STORAGE COUNTER IS.  AS WRITTEN, ONLY ONE STORAGE NODE
C     WITH POWER CURVE OUTLET IS ALLOWED.
C     NOTE THAT TST.INC IS ALSO CHANGED TO INCREASE DIMEN OF B1,B2
C
C      IF(JCE.EQ.0.AND.LT.EQ.2) READ(N5,*,ERR=888) CC,A1(1),D0(1),
C     +                         A2(1),A1(2),D0(2),A2(2),GEOM3(M)
C      IF(JCE.EQ.1.AND.LT.EQ.2) READ(N5,*,ERR=888) CC,A1(1),D0(1),
C     +                         A2(1),A1(2),D0(2),A2(2),KGEOM(M)
C      IF(LT.EQ.1) READ(N5,*,ERR=888) CC,A1(1),D0(1),A2(1)
C
      IF(JCE.EQ.0.AND.LT.EQ.2) THEN
       IF(NTYPE(M).LT.26)THEN
        READ(N5,*,ERR=888) CC,A1(IS,1),D0(IS,1),
     +                     A2(IS,1),A1(IS,2),D0(IS,2),A2(IS,2),GEOM3(M)
       ELSE
        READ(N5,*,ERR=888) CC,A1(IS,1),D0(IS,1),
     +                     A2(IS,1),A1(IS,2),D0(IS,2),A2(IS,2)
       ENDIF
        IF(GEOM3(M).LE.0)THEN
          WRITE(N6,9520)NOE(M)
          JSTOP=1
          ENDIF
      ENDIF
      IF(JCE.EQ.1.AND.LT.EQ.2) THEN
       IF(NTYPE(M).LT.26)THEN
        READ(N5,*,ERR=888) CC,A1(IS,1),D0(IS,1),
     +                     A2(IS,1),A1(IS,2),D0(IS,2),A2(IS,2),KGEOM(M)
       ELSE
        READ(N5,*,ERR=888) CC,A1(IS,1),D0(IS,1),
     +                     A2(IS,1),A1(IS,2),D0(IS,2),A2(IS,2)
       ENDIF
        IF(KGEOM(M).EQ.' ')THEN
          WRITE(N6,9521)KOE(M)
          JSTOP=1
          ENDIF
      ENDIF
      IF(LT.EQ.1) READ(N5,*,ERR=888) CC,A1(IS,1),D0(IS,1),A2(IS,1)
CPDX--------------------------------------------------------------------
      DO 420 I  = 1,LT
      DO 420 MM = 1,MINT
      IF(TSDEP(IS,MM)-D0(IS,I).GT.0.0001)
     +  TSQOU(IS,MM)=TSQOU(IS,MM)+A1(IS,I)*
     +   (TSDEP(IS,MM)-D0(IS,I))**A2(IS,I)
  420 CONTINUE
      GO TO 600
C=======================================================================
C>>>>>>>>  READ DATA GROUP G4  <<<<<<<<
C=======================================================================
CPDX--SM, 4/1/91-----------------------
C     FOLLOWING CHANGES TO READ MULTIPLE PUMPING RATES AND AN OVERFLOW
C         IF LOUT>3.  OVERFLOW DEPTH=TSDEP(MINT)
c         Up to 6 pumps are allowed, no. of pumps=lout(is)-1
C         GOEM3 OR KGOEM3=ELEMENT FOR OVERFLOW
c     tsqou=tqpump added for tstorg calculation of FLOOD
C
C  500 READ(N5,*,ERR=888) CC,TDSTAR(IS,1),TDSTAR(IS,2),TQPUMP(IS,1),
C     1                      TQPUMP(IS,2),TDSTOP(IS)
500   CONTINUE
      IF(LOUT(IS).EQ.3)THEN
          READ(N5,*,ERR=888) CC,TDSTAR(IS,1),TDSTAR(IS,2),TQPUMP(IS,1),
     1                          TQPUMP(IS,2),TDSTOP(IS)
          TSQOU(IS,MINT)=TQPUMP(IS,2)
       ELSE
        kkk=lout(is)-1
           if(kkk.gt.6)then
             write(n6,8007)
             jstop=1
           endif
        IF(JCE.EQ.0) THEN
          READ(N5,*,ERR=888)CC,(TDSTAR(IS,JSM),JSM=1,kkk),
     +            (TQPUMP(IS,JSM),JSM=1,kkk),TDSTOP(IS),GEOM3(M)
          IF(GEOM3(M).LE.0)THEN
            WRITE(N6,9520)NOE(M)
            JSTOP=1
          ENDIF
        ENDIF
        IF(JCE.EQ.1)THEN
          READ(N5,*,ERR=888)CC,(TDSTAR(IS,JSM),JSM=1,KKK),
     +            (TQPUMP(IS,JSM),JSM=1,KKK),TDSTOP(IS),KGEOM(M)
          IF(KGEOM(M).EQ.' ')THEN
            WRITE(N6,9521)KOE(M)
            JSTOP=1
          ENDIF
        ENDIF
         TSQOU(IS,MINT)=GEOM2(M)
C
       IF(TSDEP(IS,MINT).LT.tdstar(is,kkk)) THEN
                         IF(JCE.EQ.0) WRITE(N6,510) NOE(M)
                         IF(JCE.EQ.1) WRITE(N6,511) KOE(M)
                         JSTOP=1
                         ENDIF
       ENDIF
CDPX-----------------------------------------------------------------
      do 501 i=kkk,2,-1
       if(tdstar(is,i).lt.tdstar(is,i-1))then
                         IF(JCE.EQ.0) WRITE(N6,510) NOE(M)
                         IF(JCE.EQ.1) WRITE(N6,511) KOE(M)
                         JSTOP=1
                         ENDIF
501   continue
      IF(TDSTOP(IS).GT.TDSTAR(IS,1))   THEN
                         IF(JCE.EQ.0) WRITE(N6,520) NOE(M)
                         IF(JCE.EQ.1) WRITE(N6,521) KOE(M)
                         JSTOP=1
                         ENDIF
C=======================================================================
C>>>>>>>>  READ DATA GROUP G5  <<<<<<<<
C=======================================================================
  600 IF(NPOLL.GT.0) READ(N5,*,ERR=888) CC,STORL(IS),
     +                                  (PTC0(IS,IP),IP=1,NPOLL)
      IF(NPOLL.EQ.0) READ(N5,*,ERR=888) CC,STORL(IS)
      WRITE(N6,1010)
      IF(JCE.EQ.0) WRITE(N6,1030) NOE(M)
      IF(JCE.EQ.1) WRITE(N6,1031) KOE(M)
      IF(METRIC.EQ.1) WRITE(N6,1110) LOUT(IS)
      IF(METRIC.GE.2) WRITE(N6,1115) LOUT(IS)
      DO 1120 MM = 1,MINT
 1120 WRITE(N6,1130) TSDEP(IS,MM),TSAREA(IS,MM),
     +               TSTORE(IS,MM),TSQOU(IS,MM)
C=======================================================================
CPDX--SM, 4/6/91-----------------------
C=======================================================================
C>>>>>>>>  READ new DATA GROUP G6 for regulators  <<<<<<<<
c rulec(is,mm)=flow at control node, rulev(is,mm)=allowable regulator Q.
C=======================================================================
      minr=0
      DO 2301 mm = 1,17
      READ(N5,*,ERR=888) CC
      BACKSPACE N5
      IF(CC.NE.'G6') GO TO 9991
        read(n5,*)CC,rulec(is,mm),rulev(is,mm)
        if(mm.gt.1 .and. rulec(is,mm).le.0)go to 2301
        minr=minr+1
2301  continue
9991  if(minr.eq.0 .and. Lout(is).lt.0)then
        write(n6,8001)
        JSTOP=1
      endif
      RULEC(is,18)=MINR
C=======================================================================
CPDX      IF(LOUT(IS).LE.0) GO TO 1200
CPDX VCA       IF(LOUT(IS).EQ.0) GO TO 1200
      IF(LOUT(IS).EQ.0) GO TO 1155
CPDX--------------------------------------------------------------------
      IF(LOUT(IS).GE.3) GO TO 1170
      WRITE(N6,1140)
      DO 1150 I = 1,LT
 1150 WRITE(N6,1160) A1(IS,I),D0(IS,I),A2(IS,I)
      IF(JCE.EQ.0.AND.LOUT(IS).EQ.2) WRITE(N6,1165) GEOM3(M)
      IF(JCE.EQ.1.AND.LOUT(IS).EQ.2) WRITE(N6,1166) KGEOM(M)
CPDX--SM, 4/13/91--REGULATOR--------------------------------------------
CPDX VCA  10/4/93      IF(NTYPE(M).EQ.26)THEN
 1155 IF(MINR.GT.0)THEN
       WRITE(N6,8002)
       DO 1158 I=1,MINR
 1158  WRITE(N6,8003)RULEC(IS,I),RULEV(IS,I)
       IF(JCE.EQ.0.AND.LOUT(IS).LT.0) WRITE(N6,8004) GEOM3(M)
       IF(JCE.EQ.1.AND.LOUT(IS).LT.0) WRITE(N6,8005) KGEOM(M)
       IF(LOUT(IS).EQ.-2)WRITE(N6,8006)
      ENDIF
      GO TO 1200
 1170 WRITE(N6,1180)
      IF(METRIC.EQ.1)WRITE(N6,8008)TDSTOP(IS)
      IF(METRIC.EQ.2)WRITE(N6,8009)TDSTOP(IS)
      DO 1171 I=1,KKK
1171        WRITE(N6,8010)TDSTAR(IS,I),TQPUMP(IS,I)

CPDX 1200 IF(LOUT(IS).LE.0) WRITE(N6,1205)
 1200 IF(LOUT(IS).EQ.0) WRITE(N6,1205)
CPDX--------------------------------------------------------------------
      IF(METRIC.EQ.1)   WRITE(N6,1210) IS,STORL(IS)
      IF(METRIC.EQ.2)   WRITE(N6,1220) IS,STORL(IS)
      IF(NPOLL.LE.0)    GO TO 1260
      WRITE(N6,1230)
      DO 1240 IP = 1,NPOLL
 1240 WRITE(N6,1250) PNAME(IP),PUNIT(IP),PTC0(IS,IP)
 1260 IF(METRIC.EQ.1) GO TO 1270
C=======================================================================
C     CONVERT FROM METRIC TO U.S. CUSTOMARY UNITS.
C=======================================================================
      DO 1262 MM    = 1,MINT
      TSDEP(IS,MM)  = TSDEP(IS,MM)*3.2808
      TSAREA(IS,MM) = TSAREA(IS,MM)*10.764
      TSTORE(IS,MM) = TSTORE(IS,MM)*35.315
 1262 TSQOU(IS,MM)  = TSQOU(IS,MM)*35.315
      STORL(IS)     = STORL(IS)*35.315
      IF(LOUT(IS).LT.3) GO TO 1264
      TDSTOP(IS)    = TDSTOP(IS)*3.2808
      DO 1263 I     = 1,KKK
      TDSTAR(IS,I)  = TDSTAR(IS,I)*3.2808
 1263 TQPUMP(IS,I)  = TQPUMP(IS,I)*35.315
      GO TO 1270
CPDX 1264 IF(LOUT(IS).LE.0) GO TO 1270
 1264 IF(LOUT(IS).EQ.0) GO TO 1270
CPDX      LT        = LOUT(IS)
CPDX--SM, 4/1/91-DO CHANGED TO D0, DIMENSIONS ON A1,A2 CHANGED BELOW
      DO 1265 I = 1,LT
      D0(IS,I)     = D0(IS,I)*3.2808
 1265 A1(IS,I)     = A1(IS,I)*3.2808**(3.0-A2(IS,I))
CPDX--SM, 4/13/91----REGULATOR-------------------
      DO 1266 I=1,MINR
       RULEC(IS,I)=RULEC(IS,I)*35.315
1266   RULEV(IS,I)=RULEV(IS,I)*35.315
CPDX-----------------------------------
 1270 CONTINUE
CPDX--SM, 4/1/91-----------------------
      IF(JSTOP.GT.0)THEN
        WRITE(*,999)
        STOP
        ENDIF
CPDX-----------------------------------
      RETURN
  888 CALL IERROR
      STOP
C=======================================================================
CPDX--SM, 4/1/91-----------------------
999   FORMAT(' ERRORS DETECTED IN STORAGE NODE DATA')
1191  FORMAT(/15X,'THIRD PUMPING DEPTH: ',F8.2/
     +        15X,'THIRD PUMING RATE  : ',F8.2)
CPDX-----------------------------------
  220 FORMAT(/,' ===> ERROR !!! IN DATA GROUP G2 : THE FIRST ENTRIES FOR
     1 DEPTH AND VOLUME FOR STORAGE ELEMENT ',I8,/,32X,'ARE NON-ZERO. SI
     2MULATION TERMINATED.')
  221 FORMAT(/,' ===> ERROR !!! IN DATA GROUP G2 : THE FIRST ENTRIES FOR
     1 DEPTH AND VOLUME FOR STORAGE ELEMENT ',A10,/,32X,'ARE NON-ZERO. S
     2IMULATION TERMINATED.')
  510 FORMAT(/,' ===> ERROR !!! IN DATA GROUP G4 : IN STORAGE ELEMENT ',
     1I8,' THE DEPTH AT WHICH THE SECOND PUMPING RATE BEGINS IS LESS',/,
     232X,'THAN THE DEPTH AT WHICH THE FIRST PUMPING RATE BEGINS. SIMULA
     3TION TERMINATED.')
  511 FORMAT(/,' ===> ERROR !!! IN DATA GROUP G4 : IN STORAGE ELEMENT ',
     1A10,' THE DEPTH AT WHICH THE SECOND PUMPING RATE BEGINS IS LESS',
     2/,32X,'THAN THE DEPTH AT WHICH THE FIRST PUMPING RATE BEGINS. SIMU
     3LATION TERMINATED.')
  520 FORMAT(/,' ===> ERROR !!! IN DATA GROUP G4 : IN STORAGE ELEMENT ',
     1I8,' THE DEPTH AT WHICH ALL PUMPING STOPS IS GREATER THAN',/,32X,
     2'THE DEPTH AT WHICH PUMPING BEGINS. SIMULATION TERMINATED.')
  521 FORMAT(/,' ===> ERROR !!! IN DATA GROUP G4 : IN STORAGE ELEMENT ',
     1A10,' THE DEPTH AT WHICH ALL PUMPING STOPS IS GREATER THAN',/,32X,
     2'THE DEPTH AT WHICH PUMPING BEGINS. SIMULATION TERMINATED.')
 1010 FORMAT(1H1,/,10X,'STORAGE ELEMENT INPUT DATA',//)
 1030 FORMAT(10X,23('*'),/,10X,'STORAGE ELEMENT # ',I9,/,10X,27('*'),/)
 1031 FORMAT(10X,23('*'),/,10X,'STORAGE ELEMENT # ',A10,/,10X,23('*'),/)
 1110 FORMAT(/,10X,'DEPTH-AREA-STORAGE-OUTFLOW RELATIONSHIPS( LOUT = ',
     1I3,') :',//,5X,'DEPTH,FT.   SURFACE AREA,SQ.FT.   STORAGE,CU.FT. 
     2     OUTFLOW,CFS',/,5X,
     3'---------   -------------------   --------------      -----------
     4')
1115  FORMAT(/,10X,'DEPTH-AREA-STORAGE-OUTFLOW RELATIONSHIPS( LOUT = ',
     1I3,') :',//,5X,'DEPTH, M.   SURFACE AREA,SQ. M.   STORAGE,CU. M. 
     2     OUTFLOW,CMS',/,5X,
     3'---------   -------------------   --------------      -----------
     4')
 1130 FORMAT(6X,F6.2,8X,F13.1,7X,F13.1,10X,F8.2)
 1140 FORMAT(/,10X,'+ GOVERNED BY POWER FUNCTIONS :',/)
 1160 FORMAT(15X,'OUTFLOW = ',F8.2,'*(DEPTH - ',F6.2,')** ',F5.3)
 1165 FORMAT(/,5X,'FLOW FROM SECOND OUTLET IS TO ELEMENT NO.',F8.0)
 1166 FORMAT(/,5X,'FLOW FROM SECOND OUTLET IS TO ELEMENT NO.',A10)
 1180 FORMAT(/,10X,'+ GOVERNED BY PUMPING',/)
 1205 FORMAT (/,10X,'+ GOVERNED BY OUTFLOWS FROM DATA GROUP G2')
 1210 FORMAT(/,10X,'INITIAL CONDITIONS FOR JUNCTION INPUT #:',I5,/,
     1         15X,'VOLUME, CU.FT.    : ',1PE13.4)
 1220 FORMAT(/,10X,'INITIAL CONDITIONS FOR JUNCTION INPUT #:',I5,/,
     1         15X,'VOLUME, CU.M.     : ',1PE13.4)
 1230 FORMAT(15X,'POLLUTANT CONCENTRATIONS     :')
 1250 FORMAT(20X,A8,',',A8,7X,': ',5X,E8.3)
 9520 FORMAT(/,' ===> ERROR !! NO GEOM3 ELEMENT IS SPECIFIED FOR ELEMENT
     + # ',I10)
 9521 FORMAT(/,' ===> ERROR !! NO GEOM3 ELEMENT IS SPECIFIED FOR ELEMENT
     + # ',A10)
CPDX-----------------------------------
8001  FORMAT(/' ===>ERROR IN DATA GROUP G6 (NEW REGULATOR SIM):  NO VAL
     +UES FOR RULE CURVE') 
8002  FORMAT(/5X,' REGULATOR RULE CURVE'/5X,
     +        ' FLOW AT CONTROLLED ELEMENT   ALLOWED REGULATOR FLOW'/)
8003  FORMAT(20X,F8.2,15X,F8.2)
8004  FORMAT(/' CONTROLLED ELEMENT FOR REGULATOR = ',F10.0)
8005  FORMAT(/' CONTROLLED ELEMENT FOR REGULATOR = ',A10)
8006  FORMAT(/' SECOND LEVEL OUTLET (power curve) IS UNDIRECTED')
8007  FORMAT(/' ===>ERROR IN DATA GROUP G4: No. OF PUMPS > 6 (LOUT>7)')
8008  FORMAT(10X,'PUMPED OUTFLOW:'/
     +15X,'  DEPTH AT WHICH      PUMPING'/
     +15X,'PUMPING STARTS, ft   RATE, cfs'//
     +20X,F8.2,11X,'NONE')
8009  FORMAT(10X,'PUMPED OUTFLOW:'/
     +15X,'  DEPTH AT WHICH      PUMPING'/
     +15X,'PUMPING STARTS, m    RATE, m/s'//
     +20X,F8.2,11X,'NONE')
8010  FORMAT(20X,F8.2,7X,F8.2)
C=======================================================================
      END
