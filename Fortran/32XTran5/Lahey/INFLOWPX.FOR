      SUBROUTINE INFLOW
C=======================================================================
C     THIS SUBROUTINE SELECTS THE INPUT HYDROGRAPH
C     ORDINATE FROM OFF-LINE FILES AND/OR USER INPUT
C=======================================================================
      INCLUDE 'TAPES.INC'
      INCLUDE 'INTER.INC'
      INCLUDE 'STIMER.INC'
      INCLUDE 'CONTR.INC'
      INCLUDE 'JUNC.INC'
      INCLUDE 'OUT.INC'
      INCLUDE 'ORF.INC'
      INCLUDE 'PIPE.INC'
      INCLUDE 'HYFLOW.INC'
CPDX--------------------------------------------------------------------
      INCLUDE 'RDGATE.INC'
      INCLUDE 'RINGAT.INC'
      INCLUDE 'RGFLOW.INC'
      INCLUDE 'LRG.INC'
      INCLUDE 'PDXTRN.INC'
C=======================================================================
      DIMENSION LSWIT(NIE),NSWIT(NIE),MSWIT(NIE)
      DIMENSION ORTIME(NVORF),NORPLC(NVORF)
CPDX2      DOUBLE PRECISION DELTA,TFILE
      CHARACTER*10 NSWIT,MSWIT
      REAL QDW1,QDW2,QDWF
      INTEGER DHR
C=======================================================================
C     Define statement function for linear interpolation.
C=======================================================================
      QLINTP(Q1,Q2,T1,T2,T) = Q1 + (Q2-Q1)*(T-T1)/(T2-T1)
C=======================================================================
CPDX -- VCA
C     Initialize DW Flow Hour: DHR
C=======================================================================
C
       JHR = JHR - 24*(JHR/24) + 1
       DHR = JHR + 1
       IF(DHR.GT.24) DHR = 1
C
C=======================================================================
C     Initialize new QINN and QIN arrays.
C=======================================================================
CPDX--SM, 11/23/91---HOURLY VARIATION ON QINST ADDED
      DO 40 J   = 1,NJ
CPDX--- VCA ADD Land Use Curves in Variation
         IF(LUDWF(J).GE.0) THEN
C       Land Use Curves are to be applied
            IF(DWFLAG(J).LE.0) THEN
C       Need to assign specific curve to this node
C       First, set default
                DWFLAG(J) = 1
                DO 35 IL = 2, 22
                     IF(LUDWF(J).EQ.LUCODE(IL)) DWFLAG(J) = IL
  35            CONTINUE
                IF(JCE.EQ.0) WRITE(N6,44) JUN(J),LUCODE(DWFLAG(J)),
     +                       LUDESC(DWFLAG(J))
                IF(JCE.EQ.1) WRITE(N6,45) AJUN(J),LUCODE(DWFLAG(J)),
     +                       LUDESC(DWFLAG(J))
  44  FORMAT(5X,' ===> LAND USE FACTOR FOR JUNCTION ',I9,' IS #',I2,
     +       2X,A25)
  45  FORMAT(5X,' ===> LAND USE FACTOR FOR JUNCTION ',A10,' IS #',I2,
     +       2X,A25)
            ENDIF
            QDW2        = HVDWF(DWFLAG(J),DHR)
            QDW1        = HVDWF(DWFLAG(J),JHR)
         ELSE
            QDW2        = HVDWF(1,DHR)
            QDW1        = HVDWF(1,JHR)
         ENDIF
         T         = TIME/3600.0
CPDX	Test if T exceeds 24 hours - Need it to be within T1 & T2
         IF(T.GE.24.0) T = T - 24*INT(T/24)
         T2        = FLOAT(JHR)
         T1        = FLOAT(JHR-1)
         IF((JHR-1).LE.0) T1 = 0.
         IF((T.LT.T1).OR.(T.GT.T2)) T = T1
         QDWF      = QLINTP(QDW1,QDW2,T1,T2,T) 
         QIN(J)    = QINST(J) * QDWF
C
C -- Done with Land Use DWF Variation
      QINN(J,1) = QINN(J,2) 
      QINN(J,2) = 0.0
   40 CONTINUE      
C=======================================================================
C     Calculate new depth and pipe length for an orifice with a 
C               time history.
C=======================================================================

CPDX--SM, 11/16/91--NVOTIM REPLACED WITH NTVAL AT LINE 51---------------

      IF(NVOR.GT.0) THEN
                    DO 9000 M = 1,NVOR
                    N         = LORIF(M)
                    JJ        = NORPLC(M)
                    IF(JJ.EQ.0) JJ = 1
                    DO 8990 J      = JJ,NTVAL
                    IF(TIME/3600.0.GE.VORIF(M,J,1)) GO TO 8991
 8990               CONTINUE
                    GO TO 9000
 8991               CONTINUE
                    IF(ORTIME(M).GT.TIME/3600.0) GO TO 9000
                    ORTIME(M) = VORIF(M,J,1)
                    NORPLC(M) = J + 1
                    ZZ        = 0.96*DEEP(N)
                    DEEP(N)   = SQRT(4.0*VORIF(M,J,3)/3.14159)
                    WIDE(N)   = DEEP(N)
                    AFULL(N)  = VORIF(M,J,3)
                    RFULL(N)  = DEEP(N)/4.0
                                    DDD = DELT
                    IF(NEQUAL.GT.1) DDD = FLOAT(NEQUAL)                                
                    CLEN      = 2.0*DDD*SQRT(GRVT*DEEP(N))
                    LEN(N)    = AMAX1(400.0,CLEN)
                    CMANN     = CMET(9,METRIC)
                    ROUGH(N)  = CMANN*RFULL(N)**.66667/
     +                         (VORIF(M,J,2)*SQRT(LEN(N)*2.0*GRVT))
                    DO 700 J = 1,NJ
                    IF(JCE.EQ.0.AND.NJUNC(N,1).EQ.JUN(J))  GO TO 720
                    IF(JCE.EQ.1.AND.KJUNC(N,1).EQ.AJUN(J)) GO TO 720
  700               CONTINUE
  720               IF(NKLASS(N).EQ.10) THEN
                       ZU(N)      = ZU(N) - 0.96*DEEP(N) + ZZ
                       Z(J)       = ZU(N)
                       ZD(N)      = ZU(N) - 0.01/CMET(1,METRIC)
                       IF(JCE.EQ.0) WRITE(N6,6010) M,NCOND(N),
     +                              DEEP(N),LEN(N),ROUGH(N),ZU(N),
     +                              ZD(N),TIME/3600.0
                       IF(JCE.EQ.1) WRITE(N6,6011) M,ACOND(N),
     +                              DEEP(N),LEN(N),ROUGH(N),ZU(N),
     +                              ZD(N),TIME/3600.0
                       ELSE
                       IF(JCE.EQ.0) WRITE(N6,6015) M,NCOND(N),DEEP(N),
     +                                LEN(N),ROUGH(N),TIME/3600.0
                       IF(JCE.EQ.1) WRITE(N6,6016) M,ACOND(N),DEEP(N),
     +                                LEN(N),ROUGH(N),TIME/3600.0
                       ENDIF
 9000               CONTINUE
                    ENDIF
C***********************************************************************
C CALCULATE NEW DEPTH AND PIPE LENGTH DYNAMICLY CONTROLLED RADIAL GATES
C***********************************************************************
      IF(NRGATE.GT.0) THEN
         DO 9099 M = 1, NRGATE
            IF(DISCND(M).NE.0) GOTO 9099
            IF(CONTRL(M).EQ.0) GOTO 9099
               N = LRGATE(M)
               DEEP(N) = GOPEN(M)
               AFULL(N) = GWDTH(M)*GOPEN(M)
               RFULL(N) = AFULL(N)/(2.0*GWDTH(M) + 2.0*GOPEN(M))
                    DDD = DELT
               IF(NEQUAL.GT.1) DDD = FLOAT(NEQUAL)
               CLEN     = 2.0*DDD*SQRT(GRVT*(AFULL(N)/GWDTH(M)))
               LEN(N)   = AMAX1(400.,CLEN)
               CMANN    = CMET(9,METRIC)
      ROUGH(N) = CMANN*RFULL(N)**.66667/(CDRG(M)*SQRT(LEN(N)*2.0*GRVT))
 9099 CONTINUE
      ENDIF
C***********************************************************************
C=======================================================================
C     Conditional time for first time step.
C=======================================================================
      IF(MTIME.EQ.1) THEN
                     JDAY  = IDATEZ
                     TMDAY = TZERO
                     TEND  = TZERO
                     IF(METRIC.EQ.1) THEN
                                     QQCONV = QCONV
                                     ELSE
                                     QQCONV = QCONV/CMET(8,METRIC)
                                     ENDIF
CPDX--SM, 11/23/91------------------------------------------------------
C FOLLOWING STATEMENTS MOVED FROM INTERPOLATION SECTION BELOW
      IF(LOCATS.GT.0)THEN
C--
      DO 1500 L  = 1,LOCATS
      KSWIT      = NLOC(L)
      IF(JCE.EQ.0) LSWIT(L)   =  JUN(KSWIT)
 1500 IF(JCE.EQ.1) MSWIT(L)   = AJUN(KSWIT)
C--
      ENDIF
CPDX--------------------------------------------------------------------
                     ENDIF
C=======================================================================
C     Disk values from watershed model are interpolated.
C=======================================================================
      IF(LOCATS.GT.0) THEN
 1000 IF(JULDAY.GT.JDAY.OR.(JULDAY.EQ.JDAY.AND.TIMDAY.GT.TMDAY)) THEN
              DO 4523 I  = 1,LOCATS
              QTAPE(I,1) = QTAPE(I,2)
 4523         CONTINUE
              IF(NQUAL.LE.0) READ(LAST,END=205) JDAY,TMDAY,
     +                       DELTA,(QTAPE(I,2),I=1,LOCATS)
              IF(NQUAL.GT.0) READ(LAST,END=205) JDAY,TMDAY,
     +                  DELTA,(QTAPE(I,2),(PDUM,J=1,NQUAL),I=1,LOCATS)
              TREF = TIME/3600.0
              CALL NTIME(JDAY,TMDAY,TFILE)
              IF(TFILE.LT.0.0) GO TO 1000
              TFILE      = TFILE/3600.0
              DO 1400 I  = 1,LOCATS
 1400         QTAPE(I,2) = QTAPE(I,2)*QQCONV
              ENDIF
C=======================================================================
C     Interpolate flow values.
C=======================================================================
      THR        = TIME/3600.0 - TREF

CPDX--DO 1500 STATEMENTS MOVED FROM HERE

      IF(MOD(MTIME,INTER).EQ.0) THEN
             IF(METRIC.EQ.1) WRITE(N6,5000) TIME/3600.0
             IF(METRIC.EQ.2) WRITE(N6,5001) TIME/3600.0
             IF(JCE.EQ.0) WRITE(N6,5830)(LSWIT(L),QTAPE(L,2),L=1,LOCATS)
             IF(JCE.EQ.1) WRITE(N6,5833)(MSWIT(L),QTAPE(L,2),L=1,LOCATS)
             ENDIF
C=======================================================================
      DO 4570 L = 1,LOCATS
      J         = NLOC(L)
      QQ1       = QTAPE(L,1)
      QQ2       = QTAPE(L,2)
      IF(TFILE.EQ.0.0) THEN
                       QINN(J,2) = QQ2
                       QIN(J)    = QIN(J) + QQ1/2.0 + QQ2/2.0
                       ELSE
                       QEND      = QLINTP(QQ1,QQ2,0.0,TFILE,THR) 
                       QINN(J,2) = QEND
                       QIN(J)    = QIN(J) + QINN(J,1)/2.0 + QEND/2.0
                       ENDIF
 4570 CONTINUE
      ENDIF
C=======================================================================
C     Line hydrograph input values are interpolated.
C=======================================================================
      IF(NJSW.GT.0) THEN
C=======================================================================
C     New input data required.
C=======================================================================
      IF(MTIME.EQ.1) THEN
               READ(N5,*,ERR=888) CC
               IF(CC.NE.'K3') THEN
                              WRITE(N6,9100) TIME/3600.0
                              STOP
                              ENDIF
               BACKSPACE N5
               READ(N5,*,ERR=888) CC,TSTART,(QCARD(L,2),L=1,NJSW)
               WRITE(N6,4999)
               DO 2500 L     = 1,NJSW
               KSWIT         = JSW(L)
               IF(KSWIT.EQ.0) THEN
                              WRITE(*,*) L,KSWIT,NJSW
                              PAUSE
                              ENDIF
CPDX - DIURNAL FLOW MODIFICATIONS-----------------
CPDX ------- Add in Code for Land Use Diurnal Curves
               IF(LUDWF(KSWIT).GE.0)THEN
                  QDW2        = HVDWF(DWFLAG(KSWIT),JHR+1)
                  QDW1        = HVDWF(DWFLAG(KSWIT),DHR)
               ELSE
                  QDW2        = HVDWF(1,(JHR+1))
                  QDW1        = HVDWF(1,DHR)
               ENDIF
               T         = TIME/3600.0
               T2        = FLOAT(JHR+1)
               IF(DHR.GE.24) T1 = 0.
               IF(DHR.LT.24) T1 = FLOAT(DHR)
               QDWF      = QLINTP(QDW1,QDW2,T1,T2,T) 
               QINN(KSWIT,1) = QCARD(L,2) + 
     +                         QINST(KSWIT) * QDWF
CPDX - END DIURNAL FLOW MODIFICATIONS-----------------
               IF(JCE.EQ.0) LSWIT(L)  = JUN(KSWIT)
 2500          IF(JCE.EQ.1) NSWIT(L)  = AJUN(KSWIT)
               IF(METRIC.EQ.1) WRITE(N6,5831) TSTART
               IF(METRIC.EQ.2) WRITE(N6,5832) TSTART
               IF(JCE.EQ.0) WRITE(N6,5830)(LSWIT(L),QCARD(L,2),L=1,NJSW)
               IF(JCE.EQ.1) WRITE(N6,5833)(NSWIT(L),QCARD(L,2),L=1,NJSW)
               TSTART = 3600.0*TSTART
               TEND   = TSTART
               ENDIF
C=======================================================================
 6666 CONTINUE
      IF(TIME-TEND.GT.0.0) THEN
               TSTART     = TEND
               DO 300 L   = 1,NJSW
  300          QCARD(L,1) = QCARD(L,2)
               READ(N5,*,ERR=888) CC
               IF(CC.NE.'K3') THEN
                              WRITE(N6,9100) TIME/3600.0
                              STOP
                              ENDIF
               BACKSPACE N5
               READ(N5,*,ERR=888) CC,TEND,(QCARD(L,2),L=1,NJSW)
               WRITE(N6,5999)

CPDX--SM, 11/23/91--FOLLOWING LOOP TAKEN CARE OF AT STATEMENT LABEL 2500
C               DO 3000 L              = 1,NJSW
C               KSWIT                  = JSW(L)
C               IF(JCE.EQ.0) LSWIT(L)  = JUN(KSWIT)
C 3000          IF(JCE.EQ.1) NSWIT(L)  = AJUN(KSWIT)
CPDX--------------------------------------------------------------------
               IF(METRIC.EQ.1) WRITE(N6,5831) TEND
               IF(METRIC.EQ.2) WRITE(N6,5832) TEND
               IF(JCE.EQ.0) WRITE(N6,5830)(LSWIT(L),QCARD(L,2),L=1,NJSW)
               IF(JCE.EQ.1) WRITE(N6,5833)(NSWIT(L),QCARD(L,2),L=1,NJSW)
               WRITE(N6,5999)
               TEND = 3600.0*TEND
               GO TO 6666
               ENDIF
C=======================================================================
C     No new input data required.
C=======================================================================
         DO 4000 L = 1,NJSW
         J         = JSW(L)
         QQ1       = QCARD(L,1)
         QQ2       = QCARD(L,2)
         T         = TIME/3600.0
         T1        = TSTART/3600.0
         T2        = TEND/3600.0
         QQ        = QLINTP(QQ1,QQ2,T1,T2,T) 
         QINN(J,2) = QQ     + QINN(J,2)   
CPDX 4000    QIN(J)    = QIN(J) + QINN(J,1)/2.0 + QQ/2.0 
C 
CPDX --- VCA 6/26/95:  ADD IN CODE FOR LINEAR INTERPOLATION AND
CPDX --- VCA  ADD IN CODE FOR LAND USE BASED DIURNAL CURVE
             IF(LUDWF(J).GE.0)THEN
                QDW2        = HVDWF(DWFLAG(J),JHR+1)
                QDW1        = HVDWF(DWFLAG(J),DHR)
             ELSE
                QDW2        = HVDWF(1,JHR+1)
                QDW1        = HVDWF(1,DHR)
             ENDIF
             T2        = FLOAT(JHR+1)
             IF(DHR.GE.24) T1 = 0.
             IF(DHR.LT.24) T1 = FLOAT(DHR)
             QDWF      = QLINTP(QDW1,QDW2,T1,T2,T) 
 4000        QIN(J)    = QINST(J)*QDWF + (QINN(J,1) + QINN(J,2))/2.0 
         ENDIF
C=======================================================================
      RETURN
  888 CALL IERROR
C=======================================================================
  205 DO 2000 L  = 1,LOCATS
      QTAPE(L,1) = 0.0
2000  QTAPE(L,2) = 0.0
      WRITE(N6,9200) TIME/3600.0
      JDAY       = 999999
      TMDAY      = 0.0
      RETURN
C=======================================================================
C 4980 FORMAT(/,' TZERO is later in time than last record on tape from
C     1watershed')
 4999 FORMAT(/,1X,40(2H# ))
 5000 FORMAT(/,' ===> System inflows (file) at ',F8.2,' hours',
     *' (  Junction / Inflow, cfs)',/)
 5001 FORMAT(/,' ===> System inflows (file) at ',F8.2,' hours',
     *' (  Junction / Inflow, cu m/s)',/)
 5830 FORMAT(6(1X,I10,'/',1PE9.2))
 5833 FORMAT(6(2X,A9,'/',1PE9.2))
 5831 FORMAT(' ===> System inflows (data group K3) at ',F8.2,
     +          ' hours ( Junction / Inflow,cfs )',/)
 5832 FORMAT(' ===> System inflows (data group K3) at ',F8.2,
     +          ' hours ( Junction / Inflow,cu m/s )',/)
 5999 FORMAT(1X,40(2H# ))
 6010 FORMAT(/,' ====> EQUIVALENT PIPE INFORMATION FOR ORIFICE # ',I9,/,
     .       '       CONDUIT NUMBER..........................',I9,/,
     .       '       PIPE DIAMETER........................',F12.2,/,
     .       '       PIPE LENGTH..........................',F12.2,/,
     .       '       MANNINGS ROUGHNESS...................',F12.4,/,
     .       '       INVERT ELEVATION AT UPSTREAM END.....',F12.4,/,
     .       '       INVERT ELEVATION AT DOWNSTREAM END...',F12.4,/,
     .       '       SIMULATION TIME IN HOURS.............',F12.4)
 6011 FORMAT(/,' ====> EQUIVALENT PIPE INFORMATION FOR ORIFICE # ',I9,/,
     .       '       CONDUIT NAME............................',A10,/,
     .       '       PIPE DIAMETER........................',F12.2,/,
     .       '       PIPE LENGTH..........................',F12.2,/,
     .       '       MANNINGS ROUGHNESS...................',F12.4,/,
     .       '       INVERT ELEVATION AT UPSTREAM END.....',F12.4,/,
     .       '       INVERT ELEVATION AT DOWNSTREAM END...',F12.4,/,
     .       '       SIMULATION TIME IN HOURS.............',F12.4)
 6015 FORMAT(/,' ====> EQUIVALENT PIPE INFORMATION FOR ORIFICE # ',I7,/,
     .       '       INTERNAL CONDUIT NUMBER.................',I9,/,
     .       '       PIPE DIAMETER........................',F12.2,/,
     .       '       PIPE LENGTH..........................',F12.2,/,
     .       '       MANNINGS ROUGHNESS...................',F12.4,/,
     .       '       SIMULATION TIME IN HOURS.............',F12.4)
 6016 FORMAT(/,' ====> EQUIVALENT PIPE INFORMATION FOR ORIFICE # ',I7,/,
     .       '       CONDUIT NAME............................',A10,/,
     .       '       PIPE DIAMETER........................',F12.2,/,
     .       '       PIPE LENGTH..........................',F12.2,/,
     .       '       MANNINGS ROUGHNESS...................',F12.4,/,
     .       '       SIMULATION TIME IN HOURS.............',F12.4)
 9100 FORMAT(/,' ===> ERROR !!! K3 data input ended at time: ',F12.5,
     +        ' hours.')
 9200 FORMAT(/,' ===> WARNING !!! End of input file reached at time = ',
     +         F6.1,' hours.',/,
     +'                 Simulation continues with zero inflow.',/)
C=======================================================================
      END
