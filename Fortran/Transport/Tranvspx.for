C     Last change:  AMM  17 May 2000    1:25 pm
      SUBROUTINE TRANS
C=======================================================================
C     UNIVERSITY OF FLORIDA TRANSPORT MODEL
C
C     This large subroutine runs the main computational loops of the
C     Transport.   Last updated December, 1990 BY R.E.D.
C=======================================================================
      INCLUDE 'TAPES.INC'
      INCLUDE 'INTER.INC'
      INCLUDE 'STIMER.INC'
      INCLUDE 'DRWF.INC'
      INCLUDE 'TABLES.INC'
      INCLUDE 'NAMES.INC'
      INCLUDE 'TST.INC'
      INCLUDE 'NEW81.INC'
      INCLUDE 'HUGO.INC'
      INCLUDE 'NEWTR.INC'
CPDX--------------------------------------------------------------------
      INCLUDE 'VARY.INC'
C=======================================================================
      DIMENSION QI(NET),QO(NET),SURGE2(NET),IAMFL(NET),
     1          WELL2(NET),QO1(NET),QO2(NET),OUTTAP(NTH)
CPDX--SM, 5/03/91-------------------------------------------------------
C NEW ARRAY FOR SURCHARGE SUMMARY and RIIS
      LOGICAL OK
      DIMENSION SUMSUR(NET),QRIIS(NET),QDUM(NTH),IISL(NTH)
CPDX VCA 1/18/93      DOUBLE PRECISION DUMMY
      CHARACTER TSOURC*20
      EQUIVALENCE (QDUM(1),QDWF(1)),(QRIIS(1),QINFIL(1))
CPDX--------------------------------------------------------------------
      CHARACTER BLANK*1,ASTER*1,QQQ*1,BMJ*10
CPDX VCA 1/18/93      DOUBLE PRECISION DELTA,TFILE
      EQUIVALENCE (QO(1),Q(1,2,2)),(QI(1),Q(1,1,2)),(SURGE2(1),P2(1))
      EQUIVALENCE (QO1(1),QMAX(1)),(QO2(1),QFULL(1)),(WELL2(1),ROUGH(1))
      DATA ASTER/'*'/,BLANK/' '/
C=======================================================================
C     DEFINE STATEMENT FUNCTION FOR LINEAR INTERPOLATION.
C=======================================================================
      QLINTP(Q1,Q2,T1,T2,T) = Q1 + (Q2-Q1)*(T-T1)/(T2-T1)
C=======================================================================
C     CALL SUBROUTINE INTRAN FOR DATA INPUT
C=======================================================================
      DO 500 I  = 1,NET
      NODSGN(I) = 0
 500  CONTINUE
      CALL INTRAN
      NSCRT1   = NSCRAT(1)
      NSCRT2   = NSCRAT(2)
      NSCRT7   = NSCRAT(7)
CPDX--SM, 5/03/91-------------------------------------------------------
C OPEN NSCRAT(6) FOR SURCHARGE SUMMARY - ASSUME NUMERICAL NODE IDs
      WRITE(N6,9888)
9888  FORMAT(//'  RUN TIME MESSAGES  ',
     +'************************************************************'//)
      NSCRT6   = NSCRAT(6)
      KFLAG=0
      IF(NSCRT6.GT.0)THEN
        INQUIRE(NSCRT6,NAMED=OK)
        IF(OK)THEN
         DO 501 I=1,NE
           M=JR(I)
           KITER(M)=NTYPE(M)
501        IAMFL(M)=NOE(M)
         WRITE(NSCRT6)NE
         WRITE(NSCRT6)(KITER(K),K=1,NE)
         WRITE(NSCRT6)JULDAY,(IAMFL(K),K=1,NE)
         ELSE
          NSCRT6=0
          ENDIF
        ENDIF
C
C  OPEN NSCRAT5 FOR RIIS INPUT -- ASSUME NUMERICAL NODE IDs
C
      NSCRT5=NSCRAT(5)
      IF(NSCRT5.GT.0)THEN
       INQUIRE(NSCRT5,NAMED=OK)
       IF(OK)THEN
         IF(NFILTH.GT.0 .OR. NINFIL.GT.0)THEN
          WRITE(*,'(A)')' FILTH OR INFIL NOT ALLOWED WITH RIIS CALL'
          STOP
          ENDIF
C        PERFORM INFACE READS TO POSITION RIIS FILE
      ICALL=-1
         DO 502 I=1,5
502           READ(NSCRT5,ERR=503,END=503)
C
         READ(NSCRT5,ERR=503)TSOURC,IISLOC,IDUM,DUM
         READ(NSCRT5,ERR=503)(IISL(I),I=1,IISLOC)
         READ(NSCRT5,ERR=503,END=503)RIISCONV
         IF(RIISCONV.LE.0)RIISCONV=1.0
580      READ(NSCRT5,ERR=503,END=503)IISDAY,TMRIIS,DUMMY,
     +     (QDUM(I),I=1,IISLOC)
c
c check and position iis file to match input interface file
c
         IF(JULDAY.GT.IISDAY .OR. TIMDAY.GT.TMRIIS)THEN
             GO TO 580
             ENDIF
          BMJ=' '
          DO 590 I=1,IISLOC
            NNEED=IISL(I)
            NS2=NIN(NNEED,BMJ)
            IF(NS2.GT.0)QRIIS(NS2)=QDUM(I)*RIISCONV
590       CONTINUE
        ELSE
         NSCRT5=0
        ENDIF
      ENDIF
      GO TO 504
C ASSUME ERROR MEANS RIIS FILE NOT AVAILABLE
*
503   WRITE(*,'(A,I5)')' ERROR READING RIIS FILE ON NSCRT5',ICALL
      STOP
504   CONTINUE
C
*C CK FOR POLLUTANT INPUT
*      IF(NPOLL.GT.0)THEN
*        WRITE(*,'(A)')' NO POLLUTANT TRANSPORT IN THIS VERSION'
*        STOP
*        ENDIF
CPDX--------------------------------------------------------------------       

      OLDDAY   = NDAY
      DO 100 I = 1,50
 100  XNT(I)   = 0.0      
C=======================================================================
C     CALCULATE INITIAL CONDUIT VOLUME
C=======================================================================
      DO 1000 I  = 1,NE
      M          = JR(I) 
      IAMFL(M)   = 0
      SURLEN(M)  = 0.0
      RANQ(M)    = 0.0
      QR(M)      = 0.0
      QBIG(M)    = 0.0
      SBIG(M)    = 0.0
      SMAL(M)    = 100000.0
      SMEAN(M)   = 0.0
      SPEAK(M)   = 0.0
      EMAX(M)    = 0.0
      STOTAL(M)  = 0.0
      KITMAX(M)  = 0
      KITER(M)   = 0
      DO 110  J  = 1,2
      ISTIME(M,J)= 0
  110 JSTIME(M,J)= 0      
      IF(NPOLL.GT.0) THEN
                     DO 1100 K  = 1,8
 1100                RANK(M,K)  = 0.0
                     DO 1150 K  = 1,NPOLL
                     K1         = K + 4
 1150                RANK(M,K1) = SCOUR(M,K)
                     ENDIF
      IF(NTYPE(M).EQ.22) THEN
                         IS     = KSTORE(M)
                         XNT(5) = XNT(5) + STORE(IS)
                         ENDIF
      IF(NTYPE(M).GT.18) GO TO 1000
      AAA        = 0.5*(A(M,1,1)+A(M,2,1))*DIST(M)
      XNT(5)     = XNT(5) + AAA 
      IF(NPOLL.GT.0) THEN
               DO 1010 K  = 1,NPOLL
               XNT(12+K)  = XNT(12+K) + CPOL2(M,1,K)*AAA*28.3E-06
 1010          CONTINUE
               ENDIF
 1000 CONTINUE
C=======================================================================
C     GENERATE FIRST LINE OF OUTPUT TAPE TO BE USED BY INTERFACING MODEL
C=======================================================================
      IF(NEXT.GT.0.AND.NOUTS.GT.0) THEN
              DELTA     = 0.0
              DO 1666 J = 1,NOUTS
              IF(JCE.EQ.0.AND.NOE(M).NE.JN(J))  GO TO 1666
              IF(JCE.EQ.1.AND.KOE(M).NE.KJN(J)) GO TO 1666
              OUTTAP(J) = QO(M)
              IF(NPOLL.GT.0) THEN
                             DO 1620 K  = 1,NPOLL
 1620                        PFILE(K,J) = CPOL2(M,2,K)*QO(M)
                             ENDIF
              GO TO 1667
 1666         CONTINUE
 1667         IF(NPOLL.LE.0) WRITE(NEXT) JULDAY,TIMDAY,
     +                       DELTA,(OUTTAP(K),K=1,NOUTS)
              IF(NPOLL.GT.0) WRITE(NEXT) JULDAY,TIMDAY,DELTA,
     +           (OUTTAP(K),(PFILE(J,K),J=1,NPOLL),K=1,NOUTS)
              ENDIF
C=======================================================================
C     BEGIN MAIN LOOPS OF PROGRAM
C     OUTER LOOP ON TIME, INNER LOOP ON ELEMENT NUMBER
C=======================================================================
      TREF   = 0.0
      TIME   = TIMDAY
      TFLOW  = TIMDAY/3600.0
      JDAY   = JULDAY
      TMDAY  = TIMDAY
CPDX--SM, 5/04/91-------------------------------------------------------
C ADD NEW STATEMENT FOR SURCHARGE SUMMARY
      JLAST=JULDAY
CPDX--------------------------------------------------------------------
      WRITE(*,23)  NDT
      DO 200 N = 1,NDT
CPDX      WRITE(*,22) N
      IF(MOD(N,NWRITE).EQ.0)WRITE(*,22)N
CPDX--SM, 7/12/91-------------------------------------------------------
      ISWET=0
CPDX--------------------------------------------------------------------
C=======================================================================
C     UPDATE TIME OF DAY.
C     TIME   = RUNNING TIME OF SIMULATION, SECONDS.
C     TIMEHR = RUNNING TIME IN HRS, BEGINNING AT TZERO.
C     TIMDAY = TIME OF DAY IN HRS.
C=======================================================================
      CALL STIME(DT)
      CALL DATED
      TIME   = TIME + DT
      TIMEHR = TIME/3600.0
      IF(NINPUT.GT.0) THEN
                      DO 4510 I  = 1,NE
                      RNOFF(I)   = 0.0
                      IF(NPOLL.GT.0) THEN
                                     DO 4511 K  = 1,NPOLL
 4511                                PLUTO(K,I) = 0.0
                                     ENDIF
 4510                 CONTINUE
                      ENDIF
C=======================================================================
C     READ INPUT FROM INTERFACE FILE AT REQUIRED TIMES.
C     PERFORM LINEAR INTERPOLATION FOR INTERMEDIATE VALUES.
C=======================================================================
      IF(NCNTRL.EQ.0) THEN
 4515 IF(JULDAY.GT.JDAY.OR.(JULDAY.EQ.JDAY.AND.TIMDAY.GE.TMDAY)) THEN
              DO 4523 I = 1,LOCATS
              QF1(I)    = QF2(I)
              IF(NPOLL.GT.0) THEN
                             DO 4520 J = 1,NPOLL
 4520                        PF1(J,I)  = PF2(J,I)
                             ENDIF
 4523         CONTINUE
              IF(NQUAL.LE.0) READ(LAST,ERR=205,END=205) JDAY,TMDAY,
     +                       DELTA,(QF2(I),I=1,LOCATS)
              IF(NQUAL.GT.0) READ(LAST,ERR=205,END=205) JDAY,TMDAY,
     +           DELTA,(QF2(I),(PFILE(J,I),J=1,NQUAL),I=1,LOCATS)
              TREF = TIMEHR
              CALL NTIME(JDAY,TMDAY,TFILE)
              IF(TFILE.LT.0.0) GO TO 4515
              IF(TFILE.LT.DT/10.0) TFILE = 0.0
              TFILE = TFILE/3600.0
              IF(NPOLL.GT.0) THEN
                             DO 4530 I = 1,LOCATS
                             DO 4540 J = 1,NPOLL
                             KPOL      = IPOLX(J)
                             IF(KPOL.NE.0) PF2(J,I) = PFILE(KPOL,I)
 4540                        CONTINUE
 4530                        CONTINUE
                             ENDIF
C=======================================================================
C             CONTINUE READING BEYOND THE END OF THE INTERFACE FILE
C=======================================================================
              GO TO 9320
  205         JDAY      = 99999
              TMDAY     = 0.0
              TREF      = TZERO/3600.0 + DT*FLOAT(NDT)/3600.0
              DO 9300 I = 1,LOCATS
              QF1(I)    = 0.0
              QF2(I)    = 0.0

C*************** MODIFIED FOLLOWING STATEMENTS - TRANSPOSITION ERROR ****
C*************** CHANGED PF1(I,J) TO PF1(J,I), PF2 ALSO              ****
C*************** JANUARY 1992    -   LISA BENSON, CSC                ****

              IF(NQUAL.GT.0) THEN
                             DO 9310 J  = 1,NQUAL
                             PF1(J,I)   = 0.0
 9310                        PF2(J,I)   = 0.0
                             ENDIF

 9300         CONTINUE                             
              ENDIF
 9320 THR       = TIMEHR - TREF
      DO 4570 I = 1,LOCATS
      NNEED     = NLOC(I)
      BMJ       = KAN(I)
cpdx      IF(NNEED.LT.0) GO TO 4570
      NS2 = NIN(NNEED,BMJ)
CPDX--SM, 11/6/91-------------------------------------------------------
      IF(NS2.LE.0)GO TO 4570
CPDX--------------------------------------------------------------------
      QQ1 = QF1(I)
      QQ2 = QF2(I)
      IF(TFILE.EQ.0.0) RNOFF(NS2) = QQ2*QCONV
      IF(TFILE.GT.0.0) RNOFF(NS2) = QLINTP(QQ1,QQ2,0.0,TFILE,THR)*QCONV
      XNT(1) = XNT(1) + RNOFF(NS2)*DT
      IF(NPOLL.GT.0) THEN
                     DO 4560 J = 1,NPOLL
                     QQ1       = PF1(J,I)
                     QQ2       = PF2(J,I)
                     IF(TFILE.EQ.0.0) PLUTO(J,NS2) = QQ2*QCONV
                     IF(TFILE.GT.0.0) PLUTO(J,NS2) = QLINTP(QQ1,QQ2,
     +                                 0.0,TFILE,THR)*QCONV
 4560                CONTINUE
                     ENDIF
 4570 CONTINUE
      ENDIF
CPDX--SM, 7/2/91--------------------------------------------------------
C READ RIIS FILE IF PRESENT
C
      IF(NSCRT5.GT.0)THEN
        IF(JULDAY.GT.IISDAY .OR. TIMDAY.GT.TMRIIS)THEN
          READ(NSCRT5,ERR=206,END=207)IISDAY,TMRIIS,DUMMY,
     +     (QDUM(I),I=1,IISLOC)
          BMJ=' '
          DO 4590 I=1,IISLOC
            NNEED=IISL(I)
            NS2=NIN(NNEED,BMJ)
            IF(NS2.GT.0)QRIIS(NS2)=QDUM(I)*RIISCONV
4590      CONTINUE
          ENDIF
      ENDIF
      GO TO 207
206   NSCRT5=0
207   CONTINUE
CPDX--------------------------------------------------------------------
C=======================================================================
C     Read input from R1 lines at required times.
C=======================================================================
      IF(NINPUT.GT.0) THEN
      IF(TIMEHR.LE.TFLOW) GO TO 4650
 4610 TFLW      = TFLOW
C=======================================================================
C>>>>>>>>>>>> READ DATA GROUP R1 <<<<<<<<<<<<
C=======================================================================
C     Input flow and water quality on multiple R1 lines.
C=======================================================================
      IF(IFLIP.EQ.0) THEN
      DO 4640 I = 1,NINPUT
      QE1(I)    = QE2(I)
      IF(NPOLL.GT.0) THEN
                     DO 4620 J = 1,NPOLL
 4620                PE1(J,I)  = PE2(J,I)
                     ENDIF
      IF(NPOLL.EQ.0) READ(N5,*,ERR=888) CC,TFLOW,QE2(I)
      IF(NPOLL.GT.0) READ(N5,*,ERR=888)
     +                    CC,TFLOW,QE2(I),(PE2(J,I),J=1,NPOLL)
 4640 CONTINUE
      ENDIF
C=======================================================================
C     INPUT FLOW AND WATER QUALITY ON ONE R1 LINE
C=======================================================================
      IF(IFLIP.EQ.1) THEN
      DO 4645 I = 1,NINPUT
      IF(NPOLL.GT.0) THEN
                     DO 4625 J = 1,NPOLL
 4625                PE1(J,I)  = PE2(J,I)
                     ENDIF
 4645 QE1(I)    = QE2(I)
      IF(NPOLL.EQ.0) READ(N5,*,ERR=888) CC,TFLOW,(QE2(I),I=1,NINPUT)
      IF(NPOLL.GT.0) READ(N5,*,ERR=888) CC,TFLOW,
     +                      (QE2(I),(PE2(J,I),J=1,NPOLL),I=1,NINPUT)
      ENDIF
C=======================================================================
      IF(TFLOW.LT.TIMEHR) GO TO 4610
 4650 DO 4670 I  = 1,NINPUT
      NNEED      = KORDER(I)
      BMJ        = BORDER(I)
      NS2        = NIN(NNEED,BMJ)
CPDX--SM, 11/6/91-------------------------------------------------------
      IF(NS2.LE.0)GO TO 4670
CPDX--------------------------------------------------------------------
      QQ1        = QE1(I)
      QQ2        = QE2(I)
      FLOW       = QLINTP(QQ1,QQ2,TFLW,TFLOW,TIMEHR)*CMET(8,METRIC)
      XNT(1)     = XNT(1)     + FLOW*DT
      RNOFF(NS2) = RNOFF(NS2) + FLOW
      IF(NPOLL.GT.0) THEN
                     DO 4660 J = 1,NPOLL
                     QQ1       = PE1(J,I)
                     QQ2       = PE2(J,I)
 4660                PLUTO(J,NS2) = PLUTO(J,NS2) + 
     +                     QLINTP(QQ1,QQ2,TFLW,TFLOW,TIMEHR)*FLOW
                     ENDIF
 4670 CONTINUE
      ENDIF
C=======================================================================
C     NNOTE, PRECEDING QUALITY INPUTS HAVE UNITS OF CFS*CONCENTRATION
C            FLOW HAS UNITS OF CFS.
C=======================================================================
C     BEGIN INNER LOOP ON ELEMENT NUMBER
C     M = CURRENT ELEMENT NUMBER,(INTERNAL NUMBER).
C=======================================================================
      DO 150 I = 1,NE
      M        = JR(I)
C=======================================================================
C     STORE INPUT HYDROGRAPHS AND POLLUTOGRAPHS
C           FOR DESIRED ELEMENTS IF INFLEW = 0 (DEFAULT).
C=======================================================================
      IF(NNYN.GT.0.AND.INFLEW.EQ.0) THEN
                    DO 70 J = 1,NNYN
                    IF(JCE.EQ.0.AND.NOE(M).NE.NYN(J)) GO TO 70
                    IF(JCE.EQ.1.AND.KOE(M).NE.KYN(J)) GO TO 70
                    IF(NPOLL.GT.0) THEN
                                   DO 68 K = 1,NPOLL
                                   CPPP(K) = 0.0
                                   IF(RNOFF(M).GT.0.0) CPPP(K) = 
     +                                        PLUTO(K,M)/RNOFF(M)
   68                              CONTINUE
                                   WRITE (NSCRT1) RNOFF(M),
     +                                            (CPPP(K),K=1,NPOLL)
                                   ELSE
                                   WRITE (NSCRT1) RNOFF(M)
                                   ENDIF
                    GO TO 71
   70               CONTINUE
                    ENDIF
   71 INPUT     = M
      DO 4680 K = 1,NPOLL
 4680 POLDWF(K) = 0.0
      DUMY1     = 0.0
C=======================================================================
C     CORRECT DWF FOR TEMPORAL VARIATIONS
C     CORRECT SEWAGE FOR DAILY AND HOURLY VARIATION
C
C     WDWF(INPUT,1) IS B O D IN MG/L * CFS
C     WDWF(INPUT,2) IS SS IN MG/L * CFS
C     WDWF(INPUT,3) IS COLIFORM IN MPN/L * CFS
C=======================================================================
      IF(NFILTH.GT.0) THEN
                    KHR = JHR + 1
      IF(KHR.LT.0)  KHR = 1
      IF(KHR.GT.24) KHR = 24
C=======================================================================
C     Update the day of the week(KDAY) for dry weather flow.
C=======================================================================
      IF(NDAY.NE.OLDDAY) THEN
                         KDAY = KDAY + 1
                         IF(KDAY.GT.7) KDAY = 1
                         OLDDAY = NDAY
                         ENDIF
      IF(NPOLL.GT.0)  THEN
                      WDWF1     = WDWF(INPUT,1)*DVBOD(KDAY)*DVDWF(KDAY)
                      WDWF2     = WDWF(INPUT,2)*DVSS(KDAY)*DVDWF(KDAY)
                      WDWF3     = WDWF(INPUT,3)*DVDWF(KDAY)
                      POLDWF(1) = WDWF1*HVBOD(KHR)*HVDWF(KHR)
                      POLDWF(2) = WDWF2*HVSS(KHR)*HVDWF(KHR)
                      POLDWF(3) = WDWF3*HVCOLI(KHR)*HVDWF(KHR)
                      XNT(21)   = XNT(21) + POLDWF(1)
                      XNT(22)   = XNT(22) + POLDWF(2)
                      XNT(23)   = XNT(23) + POLDWF(3)
                      ENDIF
      DDWF      = QDWF(INPUT)*DVDWF(KDAY)
      DUMY1     = DDWF*HVDWF(KHR)
      XNT(2)    = XNT(2) + DUMY1*DT
      ELSE
      DUMY1     = 0.0
      ENDIF
C=======================================================================
C     Sum upstream flows.
C=======================================================================
      TOTAL   = 0.0
      DO 80 J = 1,3
      NNEED   = INUE(M,J)
      IF(NNEED.GT.NE) GO TO 80
      NTU     = NTYPE(NNEED)
C=======================================================================
C     HERE IF UPSTREAM ELEMENT IS OF FLOW DIVIDER TYPE.
C=======================================================================
      IF(NTU.LE.20) THEN
                    TOTAL = TOTAL + QO(NNEED)
                    ELSE
                    L     = GEOM3(NNEED)
                    BMJ   = KGEOM(NNEED)
                    QQ    = QO2(NNEED)
                    IF(JCE.EQ.0.AND.NOE(M).EQ.L)   QQ = QO1(NNEED)
                    IF(JCE.EQ.1.AND.KOE(M).EQ.BMJ) QQ = QO1(NNEED)
                    TOTAL = TOTAL + QQ
                    ENDIF
   80 CONTINUE
      KFULL = 2
      NT    = NTYPE(M)
      K     = KLASS(NT)
C=======================================================================
C     Assume If QI = exactly QFULL, conduit has been determined
C                    to be surcharging.
C=======================================================================
      IF(K.LE.2) THEN
                 IF(BARREL(M).GE.1.0) QI(M) = TOTAL/BARREL(M)
                 IF(BARREL(M).LT.1.0) QI(M) = TOTAL*BARREL(M)
CPDX--SM, 4/6/91------------------------------------------------------
C QI MAY NOT EXACTLY EQUAL QFULL, TEST SMALL DIFF
CPDX                 IF(IAMFL(M).EQ.1.OR.QI(M).EQ.QFULL(M)) THEN
          IF(IAMFL(M).EQ.1.OR.ABS(QI(M)-QFULL(M)).LT.1.E-06) THEN
CPDX------------------------------------------------------------------
                                           QI(M) = QFULL(M)
                                           KFULL = 1
                                           ITER  = 0
                                           ELSE
                                           ITER  = NITER
                                           ENDIF
                 ELSE
                                          QMANHO = 0.0
CPDX--SM, 6/21/91-------------------------------------------------------
C CHANGES FOR DIRUNAL VARIATION ON QMANHO.  NOTE NEW INCLUDE FILE VARY.INC
C
                 IF((NT.EQ.19.OR.NT.EQ.22) .AND. DIST(M).GT.0)THEN
                   IF(GEOM3(M).GT.0)THEN
                    IF(NDAY.NE.OLDDAY) THEN
                         KDAY = KDAY + 1
                         IF(KDAY.GT.7) KDAY = 1
                         OLDDAY = NDAY
                         ENDIF
                    LTHR=TIMDAY/3600.+1.
                    QMANHO=DIST(M)*DVDWF(KDAY)*HVDWF(LTHR)*SVDWF(MONTH)
                   ELSE                     
                      QMANHO = DIST(M)
                   ENDIF
                 ENDIF
C
C NOW ADD CALL TO NEW INTERFACE FROM IISD<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
C   FOLLOWING STATEMENT TAKEN CARE OF BY EQUIVALENCE OF QRIIS TO QINFIL
CCCC                 IF(NSCRT5.GT.0)QMANHO=QMANHO+QRIIS(M)
CPDX------------------------------------------------------------------
                 QI(M)     = TOTAL + RNOFF(M) + SURGE2(M)/DT + DUMY1 + 
     +                       QINFIL(M) + QMANHO
                 XNT(3)    = XNT(3)    + QINFIL(M)*DT
                 XNT(4)    = XNT(4)    + QMANHO*DT
                 RANQ(M)   = RANQ(M)   + RNOFF(M) + DUMY1 + 
     +                       QINFIL(M) + QMANHO 
                 IF(NPOLL.GT.0) THEN
                                DO 1200 KK = 1,NPOLL
 1200                           RANK(M,KK) = RANK(M,KK) + PLUTO(KK,M)
                                ENDIF
                 IF(NT.EQ.19.OR.NT.EQ.22) THEN
                 IF(NPOLL.GT.0) THEN
                                DO 1250 KK = 1,NPOLL
                                IF(KK.EQ.1) PMAN = GEOM1(M)
                                IF(KK.EQ.2) PMAN = SLOPE(M)
                                IF(KK.EQ.3) PMAN = ROUGH(M)
                                IF(KK.EQ.4) PMAN = GEOM2(M)
 1250                           XNT(30+KK) = XNT(30+KK) + PMAN
                                ENDIF
                 ENDIF
                 IF(QI(M).LT.0.0) QI(M) = 0.0
                 ITER = 0
                 ENDIF
C=======================================================================
C     Route flow through element.
C=======================================================================
  115 KKK    = 0
  116 WSLOPE = 0.0
      CALL ROUTE(DELQ,WSLOPE)
      IF(NT.EQ.22) GO TO 130
      KKK  = KKK  + 1
      ITER = ITER + 1
      IF(ITER.GT.NITER) GO TO 116
C=======================================================================
C     SURCHARGE ROUTINE.
C     CHECK THE ELEMENTS DOWNSTREAM FROM A NON-CONDUIT 
C     FOR POSSIBLE SURCHARGING.
C=======================================================================
  130                     DENOM = QFULL(M)
CPDX--SM, 4/3/91------------------------------------------------
C  QFULL(M)MAY NOT BE EXACTLY ZERO
C
CC      IF(QFULL(M).EQ.0.0) DENOM = 1.0
C
      IF(ABS(QFULL(M)).LT.1.0E-06) DENOM = 1.0
CPDX-----------------------------------------------------------
      IF(DELQ/DENOM.GT.EMAX(M)) EMAX(M) = DELQ/DENOM
                           KITER(M)  = KITER(M) + KKK
      IF(KKK.GT.KITMAX(M)) KITMAX(M) = KKK
      SMEAN(M)  = SMEAN(M)  + WSLOPE
      STOTAL(M) = STOTAL(M) + QO(M)*DT
      IF(WSLOPE.EQ.0.0)           WSLOPE   = SLOPE(M)
      IF(WSLOPE.GT.SBIG(M))       SBIG(M)  = WSLOPE
      IF(WSLOPE.LT.SMAL(M))       SMAL(M)  = WSLOPE
      IF(K.EQ.3.AND.I.LT.NE) THEN
      NODO      = 0
      II        = 1
 1162 MI        = JR(I+II)
C=======================================================================
C     CHECK TO SEE IF NEXT ELEMENT ROUTED IS DOWNSTREAM ELEMENT.
C=======================================================================
      IF(INUE(MI,1).EQ.M) GO TO 1163
 1164 II = II+1
      IF(I+II-NE) 1162,1162,1170
 1163 IFD    = 1
      IF(NODO.EQ.0) SURTMP = 0.0
      IAMFL(MI) = 0 
      NTI       = NTYPE(MI)
      KI        = KLASS(NTI)
C=======================================================================
C     IS CURRENT ELEMENT A FLOW DIVIDER ?
C     SET PARAMETERS FOR FLOW DIVIDER.
C=======================================================================
CPDX--SM, 4/3/91----------------------------------------------
C QO2=QOOUST2=MAIN OUTFLOW FROM STORAGE ELEMENT (LOWER ORF OR PUMPS)
C * QO2=OVERFLOW FROM FLOW DIVIDER (TYPE 21 AND 24)????
CPDX-------------------------------------------------------------------
C
      IF(NT.LE.20.OR.NT.EQ.25)                      GO TO 1165
      IF(JCE.EQ.0.AND.NT.EQ.22.AND.GEOM3(M).LE.0.0) GO TO 1165
      IF(JCE.EQ.1.AND.NT.EQ.22.AND.KGEOM(M).EQ.' ') GO TO 1165
      IFD = 2
      L   = GEOM3(M)
      BMJ = KGEOM(M)
      IF(MI.EQ.NIN(L,BMJ)) GO TO 1164
      IF(KI.EQ.3) GO TO 1167
      IF(BARREL(MI).GE.1.0) QINN = QO2(M)/BARREL(MI)
      IF(BARREL(MI).LT.1.0) QINN = QO2(M)*BARREL(MI)
      GO TO 1166
 1165 IF(KI.EQ.3) GO TO 1169
      IF(BARREL(MI).GE.1.0) QINN = QO(M)/BARREL(MI)
      IF(BARREL(MI).LT.1.0) QINN = QO(M)*BARREL(MI)
 1166 IF(QINN.LE.QMAX(MI).AND.IFD.EQ.1) GO TO 1169
      IF(QINN.LE.QMAX(MI))              GO TO 1167
C=======================================================================
C     IF CONDUIT CAPACITY EXCEEDED, STORE EXCESS AT UPSTREAM NON-CONDUIT
C     CONDUIT ASSUMED TO FLOW FULL AT UPSTREAM END.
C     QFULL BASED ON CONDUIT SLOPE.
C=======================================================================
      IF(NDESN.EQ.0.OR.NODSGN(MI).EQ.1) THEN
         IF(ISTIME(MI,1).EQ.0) THEN
                               ISTIME(MI,1) = JULDAY
                               ISTIME(MI,2) = TIMDAY
                               ENDIF
         JSTIME(MI,1) = JULDAY
         JSTIME(MI,2) = TIMDAY
         QFULL(MI)    = P1(MI)*SQRT(SLOPE(MI))
         IF(BARREL(MI).GE.1.0) SURGE2(M) = (QINN -QFULL(MI)) * 
     +                                      DT*BARREL(MI) + SURTMP
         IF(BARREL(MI).LT.1.0) SURGE2(M) = (QINN-QFULL(MI))*DT
     &                                     + SURTMP
         QRATIO       = QINN/QFULL(MI)
         SURTMP       = SURGE2(M)
         SURLEN(MI)   = SURLEN(MI) + DT
         IF(SURTMP.GT.SPEAK(MI)) SPEAK(MI) = SURGE2(M) 
         IF(QRATIO.GT.QR(MI))       QR(MI) = QRATIO
CPDX--SM, 5/04/91------------------------------------------------------
C NOW STORE THE MAX QRATIO FOR ANY JULDAY IN SUMSURC ARRAY
         KFLAG=1
         IF(QRATIO.GT.SUMSUR(MI))THEN
              SUMSUR(MI) = QRATIO
         ENDIF
CPDX-------------------------------------------------------------------
         ENDIF
C======================================================================
C     DESIGN ROUTINE.
C     INCREASE CONDUIT DIMENSION UNTIL SURCHARGE ELIMINATED.
C     INCREASE DIAMETER OF CIRCULAR CONDUITS BY STANDARD AMOUNTS (NTI=1).
C     INCREASE WIDTH OF RECTANGULAR CONDUIT BY 0.5 FT (NTI=2).
C     IF NOT CIRCULAR OR RECT., REPLACE BY CIRCULAR OF EQUAL AREA, USING
C     STANDARD PIPE SIZES.
C=======================================================================
CPDX calls to first changed 9/6/91--SM
c
      IF(NDESN.GT.0.AND.NODSGN(MI).EQ.0) THEN
                     SURTMP     = 0.0
                     KSTORE(MI) =  1
                     IF(NTI.EQ.1) THEN
                            DEL = 0.25
                            IF(GEOM1(MI).GE.3.0) DEL = 0.50
                            IF(GEOM1(MI).LT.1.0) DEL = 1.0-GEOM1(MI)
                            GEOM1(MI) = GEOM1(MI) + DEL
                            CALL FIRST(MI,1)
                            IF(JCE.EQ.0) WRITE(N6,480) 
     +                                   NOE(MI),DEL,GEOM1(MI)
                            IF(JCE.EQ.1) WRITE(N6,481) 
     +                                   KOE(MI),DEL,GEOM1(MI)
                            GO TO 1166
                            ENDIF
                     IF(NTI.EQ.2) THEN
                            GEOM2(MI) = GEOM2(MI) + 0.50
                            CALL FIRST(MI,1)
                            IF(JCE.EQ.0) WRITE(N6,400) NOE(MI),GEOM2(MI)
                            IF(JCE.EQ.1) WRITE(N6,401) KOE(MI),GEOM2(MI)
                            GO TO 1166
                            ENDIF
                     NTYPE(MI) = 1
                     NTI       = 1
                     GE        = 1.12838*SQRT(AFULL(MI))
                     IF(GE.LE.3.0) THEN
                           GEOM1(MI) = FLOAT(IFIX(GE*100.)/25+1)*0.25
                           ELSE
                           GEOM1(MI) = FLOAT(IFIX(GE*100.)/50+1)*0.50
                           ENDIF
                     CALL FIRST(MI,1)
                     IF(JCE.EQ.0) WRITE(N6,415) NOE(MI),GEOM1(MI)
                     IF(JCE.EQ.1) WRITE(N6,416) KOE(MI),GEOM1(MI)
                     GO TO 1166
                     ENDIF  
      IF(IFD.EQ.1) GO TO 1168
      IF(BARREL(MI).GE.1.0) QO2(M) = QFULL(MI)*BARREL(MI)
      IF(BARREL(MI).LT.1.0) QO2(M) = QFULL(MI)
      QO(M)  = QO1(M) + QO2(M)
C=======================================================================
C     CHECK CAPACITY OF SECOND ELEMENT DOWNSTREAM FROM FLOW-DIVIDER.
C=======================================================================
 1167 L   = GEOM3(M)
      BMJ = KGEOM(M)
      MI  = NIN(L,BMJ)
C AMM 5/17/2000 Check for connectivity match; compiler imprecision for plain
C               reals; had to up GEOM3 to Double Precision
C      WRITE (N6,*) L, NOE(M)
*
CPDX----VCA 1/23/92:  CHECK ON EXISTENCE OF DOWNSTREAM ELEMENT:
*
      IF(MI.LE.0) THEN
*
          IF(JCE.EQ.0) WRITE(N6,710) NOE(M)
          IF(JCE.EQ.0) WRITE(*,710) NOE(M)
          IF(JCE.EQ.1) WRITE(N6,712) KOE(M)
          IF(JCE.EQ.1) WRITE(*,712) KOE(M)
*
*CPDX VCA 11/6/97:  Storage Nodes may have problem if regulator not defined correctly
          IF (NTYPE(M).EQ.22) THEN
              WRITE(*,715)
              WRITE(N6,715)
          ENDIF
*
          STOP
  710 FORMAT(/,' ERROR:  DOWNSTREAM ELEMENT FOR #',I9,' IS UNDEFINED.')
  712 FORMAT(/,' ERROR:  DOWNSTREAM ELEMENT FOR #',A10,' IS UNDEFINED.')
  715 FORMAT(/,'         --> (Check for bad GEOM3 number on G4 card)')
      ENDIF
*
CPDX ---------------------------
      NTI = NTYPE(MI)
      KI  = KLASS(NTI)
      IF(KI.EQ.3) GO TO 1169
      IF(BARREL(MI).GE.1.0) QINN = QO1(M)/BARREL(MI)
      IF(BARREL(MI).LT.1.0) QINN = QO1(M)
      IFD  = 1
      GO TO 1166
 1168 IF(BARREL(MI).GE.1.0) THEN
                            QO1(M) = QFULL(MI) * BARREL(MI)
                            QO(M)  = QO1(M)    + QO2(M)
                            ELSE
                            IF(NODO.EQ.0) THEN
                                          QO1(M)    = QFULL(MI)
                                          QO2(M)    = QI(M)-QFULL(MI)
                                          IAMFL(MI) = 1
                                          ENDIF
                            IF(NODO.EQ.1) THEN
                                          QO2(M)    = QFULL(MI)
                                          IAMFL(MI) = 1
                                          ENDIF
                            QO(M) = QO1(M) + QO2(M)
                            ENDIF
      GO TO 1170
 1169 IF(NT.NE.22) SURGE2(M) = SURTMP
      IF(NT.EQ.22) SURGE2(M) = SURGE2(M) + SURTMP
 1170 CONTINUE
      IF(BARREL(MI).LT.1.0) THEN
                            NODO = NODO + 1
                            GO TO 1164
                            ENDIF
      ENDIF
C======================================================================
C     Multiply flow by number of barrels if barrels > 1.0
C=======================================================================
      IF(BARREL(M).GE.1.0) THEN
                           QI(M) = QI(M)*BARREL(M)
                           QO(M) = QO(M)*BARREL(M)
                           ENDIF
      IF(QO(M).GT.QBIG(M)) QBIG(M) = QO(M)
C=======================================================================
C     Route quality parameters through element.
C=======================================================================
      IF(NPOLL.GT.0) CALL QUAL(1)
C=======================================================================
C     STORE DESIRED OUTFLOWS TO BE GENERATED ONTO OUTPUT TAPE
C     POLLUTANTS ON INTERFACE FILE HAVE UNITS OF CFS*CONCENTRATION.
C=======================================================================
      IF(NORDER(I).EQ.1) THEN
                         XNT(7)    = XNT(7) + QO(M)*DT
                         IF(NPOLL.GT.0) THEN
                         DO 5555 K = 1,NPOLL
 5555                    XNT(8+K)  = XNT(8+K) +
     +                               DT*CPOL2(M,2,K)*QO(M)
                         ENDIF
                         ENDIF
cpdx--sm, 4/24/91-------------------------------------------------------
c  following 'iswet' statements added to permit generation of CSO tape
c   without intervent dry time steps.  Works if only outfall
c   flow traces are saved.  This makes for smaller interface files.
C    ISWET=0 STATEMENT MOVED TO OUTER TIME STEP DO LOOP 7/12/91
c
      IF(NOUTS.GT.0.AND.NEXT.GT.0) THEN
               DO 6666 J = 1,NOUTS
               IF(JCE.EQ.0.AND.NOE(M).NE.JN(J))  GO TO 6666
               IF(JCE.EQ.1.AND.KOE(M).NE.KJN(J)) GO TO 6666
               OUTTAP(J) = QO(M)
              if(qo(m).gt.0)iswet=1
               IF(NPOLL.GT.0) THEN
                              DO 6620 K  = 1,NPOLL
 6620                         PFILE(K,J) = CPOL2(M,2,K)*QO(M)
                              ENDIF
 6666          CONTINUE
               ENDIF
CPDX--------------------------------------------------------------------
C=======================================================================
C     STORE DESIRED OUTFLOWS TO BE GENERATED ON SCRATCH TAPE
C                                      FOR PRINTING PURPOSES
C=======================================================================
      IF(NNPE.GT.0) THEN
                    DO 122 J = 1,NNPE
                    IF(JCE.EQ.0.AND.NOE(M).NE.NPE(J)) GO TO 122
                    IF(JCE.EQ.1.AND.KOE(M).NE.KPE(J)) GO TO 122
                    IF(NPOLL.EQ.0) WRITE (NSCRT2) QO(M)
                    IF(NPOLL.GT.0) WRITE (NSCRT2) QO(M),
     +                            (CPOL2(M,2,K),K=1,NPOLL)
                    GO TO 124
  122               CONTINUE
                    ENDIF
  124 CONTINUE
C=======================================================================
C     STORE DESIRED DEPTHS TO BE GENERATED ON SCRATCH TAPE
C                                    FOR PRINTING PURPOSES
C=======================================================================
      IF(NSURF.GT.0) THEN
                     DO 132 J = 1,NSURF
                     IF(JCE.EQ.0.AND.NOE(M).NE.JSURF(J)) GO TO 132
                     IF(JCE.EQ.1.AND.M.NE.JSURF(J))      GO TO 132
                     A1   = A(M,1,2)/AFULL(M)
                     A2   = A(M,2,2)/AFULL(M)
                     A1   = 0.5 * (DEPTH(A1) + DEPTH(A2))
                     A1   = A1*GEOM1(M)
                     WRITE(NSCRT7) A1
                     GO TO 134
  132                CONTINUE
                     ENDIF
  134 CONTINUE
C=======================================================================
C     STORE INPUT HYDROGRAPHS AND POLLUTOGRAPHS
C           FOR DESIRED ELEMENTS IF INFLEW = 1 (NEW OPTION).
C=======================================================================
      IF(NNYN.GT.0.AND.INFLEW.EQ.1) THEN
                    DO 170 J = 1,NNYN
                    IF(JCE.EQ.0.AND.NOE(M).NE.NYN(J)) GO TO 170
                    IF(JCE.EQ.1.AND.KOE(M).NE.KYN(J)) GO TO 170
                    IF(NPOLL.GT.0) THEN
                                   DO 168 K = 1,NPOLL
                                   CPPP(K) = 0.0
                                   IF(QI(M).GT.0.0) CPPP(K) = 
     +                                        CPOL1(M,2,K)*QI(M)
  168                              CONTINUE
                                   WRITE(NSCRT1) QI(M),
     +                                           (CPPP(K),K=1,NPOLL)
                                   ELSE
                                   WRITE(NSCRT1) QI(M)
                                   ENDIF
                    GO TO 171
  170               CONTINUE
                    ENDIF
  171 CONTINUE
C=======================================================================
C     REPLACE VALUES AT OLD TIME STEP BY VALUES AT NEW ONE
C=======================================================================
      A(M,1,1) = A(M,1,2)
      A(M,2,1) = A(M,2,2)
      IF(NPOLL.GT.0) THEN
                     DO 126 IP     = 1,NPOLL
                     CPOL1(M,1,IP) = CPOL1(M,2,IP)
  126                CPOL2(M,1,IP) = CPOL2(M,2,IP)
                     ENDIF
      IF(BARREL(M).GE.1.0) Q(M,1,1)  = Q(M,1,2)/BARREL(M)
      IF(BARREL(M).LT.1.0) Q(M,1,1)  = Q(M,1,2)
      IF(BARREL(M).GE.1.0) Q(M,2,1)  = Q(M,2,2)/BARREL(M)
  150 IF(BARREL(M).LT.1.0) Q(M,2,1)  = Q(M,2,2)
C=======================================================================
C     GENERATE OUTPUT TAPE TO BE USED BY INTERFACING MODEL
C=======================================================================
      IF(NEXT.GT.0.AND.NOUTS.GT.0) THEN
cpdx--sm, 4/24/91-----------------------------------------------------
c  For continuous simulation, test to see if any OUTTAP>0.  This permits
c    CSO tape to be generated without interevent dry time steps.  Output
c    is generated on the Interface file if flow at any 'saved' node is >0.
c    
c
c
              if(iswet.gt.0 .or. n.eq.1)then
                     IF(NPOLL.LE.0) WRITE (NEXT) JULDAY,TIMDAY,
     +                              DT,(OUTTAP(K),K=1,NOUTS)
                     IF(NPOLL.GT.0) WRITE (NEXT) JULDAY,TIMDAY,
     +                              DT,(OUTTAP(K),(PFILE(J,K),J=1,
     +                              NPOLL),K=1,NOUTS)
                     ENDIF
              endif
CPDX--SM, 5/04/91------------------------------------------------------
CPDX NOW WRITE SURCHARGE SUMMARY FILE IF KFLAG>0
          IF(NSCRT6.GT.0 .AND. KFLAG.GT.0 .AND. JULDAY.GT.JLAST)THEN
              WRITE(NSCRT6)JLAST,(SUMSUR(K),K=1,NE)
              JLAST=JULDAY
              KFLAG=0
              DO 151 K=1,NE
151               SUMSUR(K)=0
              ENDIF
CPDX--------------------------------------------------------------------
  200 CONTINUE
C=======================================================================
C     CALL NEW OUTPUT SUBROUTINE
C=======================================================================
      CALL OTRAIN
C=======================================================================
C     WRITE NEW ELEMENT TABLE.
C=======================================================================
      IF(NDESN.GT.0) THEN
                     WRITE (N6,930)
                     CALL FIRST(MI,2)
                     KARIGE = 0
                     IF(METRIC.EQ.1) WRITE (N6,916) KARIGE
                     IF(METRIC.EQ.2) WRITE (N6,919) KARIGE
                     DO 300 I = 1,NE
                     NT       = NTYPE(I)
                     IF(NT.LE.18) THEN
                           QQQ = BLANK
                           IF(KSTORE(I).EQ.1) QQQ = ASTER
                           DIST(I)  = DIST(I)/CMET(1,METRIC)
                           GEOM1(I) = GEOM1(I)/CMET(1,METRIC)
                           GEOM2(I) = GEOM2(I)/CMET(1,METRIC)
                           GEOM3(I) = GEOM3(I)/CMET(1,METRIC)
                           AFULL(I) = AFULL(I)/CMET(1,METRIC)**2.0
                           QFULL(I) = QFULL(I)/CMET(8,METRIC)
                           QMAX(I)  = QMAX(I)/CMET(8,METRIC)
                           IF(JCE.EQ.0) WRITE(N6,931) NOE(I),NT,QQQ,
     +                           NAME(NT),SLOPE(I),DIST(I),ROUGH(I),
     +                           GEOM1(I),GEOM2(I),GEOM3(I),BARREL(I),
     +                           AFULL(I),QFULL(I),QMAX(I),SCF(I)
                           IF(JCE.EQ.1) WRITE(N6,932) KOE(I),NT,QQQ,
     +                           NAME(NT),SLOPE(I),DIST(I),ROUGH(I),
     +                           GEOM1(I),GEOM2(I),GEOM3(I),BARREL(I),
     +                           AFULL(I),QFULL(I),QMAX(I),SCF(I)
                           ENDIF
  300                CONTINUE
                     ENDIF
C=======================================================================
C     PRINT SELECTED INPUT AND OUTPUT HYDROGRAPHS AND POLLUTOGRAPHS.
C=======================================================================
      IF(NPOLL.LE.0.AND.JPRINT.GT.0) CALL PRINTF(0)
      IF(NPOLL.GT.0.AND.JPRINT.GT.0) THEN
                                     CALL PRINTQ
                                     CALL PRINTF(1)
                                     ENDIF
      WRITE(*,4850)
      WRITE(N6,4850)
C=======================================================================
   22 FORMAT('+',6X,I8)      
   23 FORMAT(/,' Beginning loop thru ',I8,' Time steps',/,
     +         ' Time step # ',/)      
  400 FORMAT(' INCREASE WIDTH OF EXT ELEMENT ',I8,' BY 0.50 TO ',F7.3,
     1' FT.')
  401 FORMAT(' INCREASE WIDTH OF EXT ELEMENT ',A10,' BY 0.50 TO ',F7.3,
     1' FT.')
  415 FORMAT(' REPLACE EXT ELEMENT ',I8,' BY CIRCULAR CONDUIT OF DIAMETE
     1R ',F7.3,'FT.')
  416 FORMAT(' REPLACE EXT ELEMENT ',A10,' BY CIRCULAR CONDUIT OF DIAMET
     1ER ',F7.3,'FT.')
  480 FORMAT(' INCREASE DIAMETER OF EXT ELEMENT ',I8,' BY ',F6.2,
     1' TO ',F8.3,'FT.')
  481 FORMAT(' INCREASE DIAMETER OF EXT ELEMENT ',A10,' BY ',F6.2,
     1' TO ',F8.3,'FT.')
  910 FORMAT (//, ' THE TOTAL SIMULATION TIME =',F12.1,' SECONDS.'/,
     1            '                            ',F12.2,' MINUTES.',/,
     2            '                            ',F12.3,'   HOURS.',/,
     3            '         THE TIME STEP(DT) =',F12.1,' SECONDS.')
  916 FORMAT(I1,/,
     1' *****************************************************',/,
     1' *           TRANSPORT ELEMENT PARAMETERS            *',/,
     1' *                                                   *',/,
     *' * CAUTION: COLUMN HEADINGS ARE FOR CONDUITS.  REFER *',/,
     2' * TO USERS MANUAL FOR MEANING FOR NON-CONDUITS.     *',/,
     1' *****************************************************',//,
     3'  EXTERNAL                    SLOPE  DISTANCE  MANNING    GEOM1',
     3'  GEOM2  GEOM3   NUMBER   AFULL    QFULL    QMAX  SUPER-CRITICA',
     4'L',/,'  ELEMENT  TY                (FT/FT)   (FT)   ROUGHNESS  ',
     4' (FT)   (FT)   (FT)      OF    (SQ.FT)   (CFS)    (CFS) FLOW WH',
     5'EN LESS',/,'  NUMBER   PE  TYPE NAME                           ',
     6'                            BARRELS                           ',
     6'THAN 95% FULL?',/,
     7' --------- -- -----------    --------  ------  --------   -----',
     8'  -----  -----  -------   ------   ------   ----- -------------',
     9'--')
  919 FORMAT(I1,/,
     1' *****************************************************',/,
     1' *           TRANSPORT ELEMENT PARAMETERS            *',/,
     1' *                                                   *',/,
     *' * CAUTION: COLUMN HEADINGS ARE FOR CONDUITS.  REFER *',/,
     2' * TO USERS MANUAL FOR MEANING FOR NON-CONDUITS.     *',/,
     1' *****************************************************',//,
     3'  EXTERNAL                    SLOPE  DISTANCE  MANNING    GEOM1',
     3'  GEOM2  GEOM3   NUMBER   AFULL    QFULL    QMAX  SUPER-CRITICA',
     4'L',/,'  ELEMENT  TY                (M/M)     (M)    ROUGHNESS  ',
     4' (M)    (M)    (M)       OF    (SQ.M)    (CMS)    (CMS) FLOW WH',
     5'EN LESS',/,'  NUMBER   PE  TYPE NAME                           ',
     6'                            BARRELS                           ',
     6'THAN 95% FULL?',/,
     7' --------- -- -----------    --------  ------  --------   -----',
     8'  -----  -----  -------   ------   ------   ----- -------------',
     9'--')
  925 FORMAT (//,' THE ENDING DATE (YR/MO/DAY)....',I3,'/',I2,'/',I2,/,
     1           ' THE ENDING TIME OF DAY.........',F9.3,' SECONDS.',//)
  930 FORMAT(/,1H1,/,
     1' **********************************************************',/,
     1' *         HYDRAULIC DESIGN ROUTINE FINAL RESULTS.        *',/,
     1' *  FINAL CONDUIT DIMENSIONS. ASTERISK INDICATES ALTERED  *',/,
     2' *  CONDUITS.  ==> NOTE, PIPE SIZES ARE INCREASED BY U.S. *',/,
     2' *                 STD. SIZES (E.G., 6 INCHES) AND MAY    *',/,
     2' *                 NOT AGREE WITH METRIC STANDARDS.       *',/,
     2' **********************************************************',/)
CPDX  931 FORMAT(1X,2I5,1X,A1,A16,F8.5,F9.2,F9.4,F9.3,2F7.3,F6.1,3G9.2,
  931 FORMAT(1X,I9,I3,1X,A1,A15,F7.5,F8.0,F10.4,F8.3,2F7.3,F8.1,3F9.2,
     +                                                       5X,A4)
CPDX  932 FORMAT(1X,A5,I5,1X,A1,A16,F8.5,F9.2,F9.4,F9.3,2F7.3,F6.1,
  932 FORMAT(1X,A9,I3,1X,A1,A15,F7.5,F8.0,F10.4,F8.3,2F7.3,F8.1,
     +                                              3F9.2,5X,A4)
 4750 FORMAT(//,11X,A80,/,11X,A80)
 4850 FORMAT(/,1X,'Transport simulation ended normally.')
C=======================================================================
      RETURN
  888 CALL IERROR
      END
