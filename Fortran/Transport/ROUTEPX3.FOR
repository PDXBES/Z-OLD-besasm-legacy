      SUBROUTINE ROUTE(DELQ,WSLOPE)
C=======================================================================
C     ROUTING SUBPROGRAM FOR FLOW THROUGH SEWER ELEMENTS.
C     IN THE PROGRAM, ALPHA REPRESENTS NORMALIZED AREA, A/AFULL.
C                     PSI AND PS REPRESENT NORMALIZED FLOW, Q/QFULL.
C=======================================================================
C     UPDATED SEPTEMBER 1981 BY W.C.H. AND S.J.N.
C     UPDATED SEPTEMBER 1988 BY R.E.D.
C     UPDATED SEPTEMBER 1989 BY R.E.D.
C=======================================================================
C     MEANING OF A AN Q ARRAYS
C     A OR Q(CHANNEL,1-UPSTREAM 2-DOWNSTREAM,
C                    1-START OF TIME STEP 2-END OF TIME STEP)
C=======================================================================
      INCLUDE 'TAPES.INC'
      INCLUDE 'TABLES.INC'
      INCLUDE 'NAMES.INC'
      INCLUDE 'HUGO.INC'
      INCLUDE 'NEW81.INC'
      INCLUDE 'TST.INC'
      INCLUDE 'FLODAT.INC'
CPDX--SM,7/17/91--------------------------------------------------------
      INCLUDE 'VARY.INC'
CPDX--------------------------------------------------------------------
      DIMENSION QI(NET),QO(NET),WELL1(NET),WELL2(NET),PUMP(NET),
     +          QO1(NET),QO2(NET)
      CHARACTER BMJ*10
      EQUIVALENCE (QO(1),Q(1,2,2)),(QI(1),Q(1,1,2)),(PUMP(1),DIST(1))
      EQUIVALENCE (QO1(1),QMAX(1)),(QO2(1),QFULL(1))
      EQUIVALENCE (WELL1(1),SLOPE(1)),(WELL2(1),ROUGH(1))
      DATA WT/0.60/,WD/0.60/
      DATA NUMERR/0/
C=======================================================================
C     USE GENERALIZED WEIGHTS OF TIME AND SPACE DERIVATIVES OF
C     CONTINUITY EQUATION.
C     WT = WEIGHT ON TIME DERIVATIVE.
C     WD = WEIGHT ON SPATIAL DERIVATIVE.
C          THESE CONSTANTS APPEAR IN DEFINITIONS OF C1(M) AND C2.
C     NOTE, WT  = WD = 0.50 CORRESPOND TO ORIGINAL VERSION OF TRANSPORT MODEL.
C           WT  = WD = 0.55 APPEAR TO GIVE BEST ATTENUATION OF HYDROGRAPHS.
C     M         = ELEMENT NUMBER
C     KLASS(NT) = 1 FOR CONDUIT WITH FUNCTIONAL Q-A RELATIONSHIP.
C     KLASS(NT) = 2 FOR CONDUIT WITH TABULAR Q-A RELATIONSHIP.
C     KLASS(NT) = 3 FOR ELEMENT OTHER THAN CONDUIT.
C                   COMPUTE TOTAL HEAD SLOPE AND ASSOCIATED PARAMETERS.
C     KFULL = 1 IF CONDUIT IS FULL AT UPSTREAM END.
C     KFULL = 2 IF CONDUIT IS NOT FULL AT UPSTREAM END.
C=======================================================================
CPDX--SM 4/2/91------------------------------
C     NOTE:  M (ELEMENT #) IS IN HUGO.INC
CSM-------
      NT     = NTYPE(M)
      K      = KLASS(NT)
C=======================================================================
C     PRINT OF ERRORS STOPS WHEN NUMBER OF ERRORS EXCEED 50
C=======================================================================
      IF(NUMERR.EQ.50.AND.NPRINT.GT.0) THEN
                                        NUMERR = NUMERR + 1
                                        WRITE(N6,930) 
                                        ENDIF
C=======================================================================
C     NO ITERATIONS REQUIRED FOR CONDUITS WITH SURCHARGED FLOW.
C     NO ITERATIONS REQUIRED FOR CONDUITS WITH SUPER-CRITICAL FLOW.
C=======================================================================
      IF(K.LE.2)     THEN
      IF(KFULL.EQ.1) THEN
                  C1(M)   = AFULL(M)/QFULL(M)*DXDT(M)*WT/WD
                  QMAX(M) = P4(M)*QFULL(M)
                  WSLOPE  = SLOPE(M)
                  DELQ    = 0.0
                  ELSE
                  IF(SCF(M).NE.GNO) THEN
                            ITER    = 0
                            WSLOPE  = SLOPE(M)
                            DELQ    = 0.0
                            ELSE
C=======================================================================
C                           NON-SUPERCRITICAL FLOW
C=======================================================================
                            IF(ITER.LE.NITER) THEN
                               QOLD = QFULL(M)
                               A1   = A(M,1,1)/AFULL(M)
                               A2   = A(M,2,1)/AFULL(M)
                               DV   = (VEL(Q(M,1,1),A(M,1,1))**2 - 
     +                                 VEL(Q(M,2,1),A(M,2,1))**2)
     +                                /DIST(M)/64.4
                               WSLOPE = SLOPE(M) + P2(M)*(DEPTH(A1) -
     +                                  DEPTH(A2)) + DV
                               ELSE
                               A1 = A(M,1,2)/AFULL(M)
                               A2 = A(M,2,2)/AFULL(M)
                               DV = (VEL(Q(M,1,2),A(M,1,2))**2 - 
     +                               VEL(Q(M,2,2),A(M,2,2))**2)
     +                              /DIST(M)/64.4
                               WSLOPE = SLOPE(M) + P2(M)*(DEPTH(A1) -
     +                                  DEPTH(A2)) + DV
                               ENDIF
C=======================================================================
                            IF(WSLOPE.LE.SLOPE(M)) WSLOPE = SLOPE(M)
                                                OMEGA = 0.50
                            IF(ITER.GT.NITER+3) OMEGA = 0.33
                            QFULL(M) = P1(M)*SQRT(WSLOPE)
                            QFULL(M) = (1.0-OMEGA)*QOLD+OMEGA*QFULL(M)
                            DELQ     = ABS(QFULL(M)-QOLD)
                            QOLD     = QFULL(M)
                            IF(DELQ.LT.EPSIL*QFULL(M)) ITER = 0
                            IF(ITER-NITER.GE.NITER-1)  ITER = 0
                            C1(M)   = AFULL(M)/QFULL(M)*DXDT(M)*WT/WD
                            QMAX(M) = P4(M)*QFULL(M)
                            ENDIF
C=======================================================================
         ENDIF
         ENDIF
C=======================================================================
C     ROUTINE FOR CONDUIT WITH FUNCTIONAL Q-A RELATIONSHIP.
C     FIND NEW UPSTREAM AREA.
C=======================================================================
      IF(K.EQ.1) THEN
      IF(QI(M).EQ.0.0.AND.Q(M,2,1).LE.1.0E-20) THEN
                                               A(M,2,2) = 0.0
                                               Q(M,2,2) = 0.0
                                               RETURN
                                               ENDIF
      IF(KFULL.EQ.1) THEN
                     A(M,1,2) = AFULL(M)
                     ELSE
                     C2    = -Q(M,1,2)/QFULL(M)
                     ALPHA = A(M,1,1)/AFULL(M)
                     CALL NEWTON(ALPHA,PS,0.0,C2,KFLAG)
                     IF(KFLAG.EQ.2) THEN
                              NUMERR = NUMERR + 1
                              IF(NPRINT.GE.1.AND.NUMERR.LT.50) THEN
                                 IF(JCE.EQ.0) WRITE(N6,910) 
     +                                        TIME,N,NOE(M),A(M,1,1)
                                 IF(JCE.EQ.1) WRITE(N6,911) 
     +                                        TIME,N,KOE(M),A(M,1,1)
                                 ENDIF
                              ALPHA = A(M,1,1)/AFULL(M)
                              ENDIF
                     A(M,1,2) = ALPHA*AFULL(M)
                     ENDIF
C=======================================================================
C     ASSIGN VALUES TO CONSTANTS AND SOLVE FOR DOWNSTREAM Q AND A.
C=======================================================================
      C2    = (1.0-WD)*Q(M,2,1)-(1.0-WD)*Q(M,1,1)-WD*Q(M,1,2)
      C2    = C2 + DXDT(M)*((1.0-WT)*A(M,1,2) - 
     +                      (1.0-WT)*A(M,1,1)-WT*A(M,2,1))
      C2    = C2/QFULL(M)/WD
      ALPHA = A(M,2,1)/AFULL(M)
C=======================================================================
C     DOWNSTREAM Q AND A MAY NOW BE CALCULATED.
C=======================================================================
      CALL NEWTON(ALPHA,PS,C1(M),C2,KFLAG)
      IF(KFLAG.EQ.1) THEN
                     A(M,2,2) = ALPHA*AFULL(M)
                     Q(M,2,2) = PS*QFULL(M)
                     RETURN
                     ENDIF
C=======================================================================
C     DETERMINE REASON FOR NON-CONVERGENCE.
C     THEN USE DEFAULT OPTIONS TO DETERMINE Q AND A.
C
C     SEE IF LINE-C1*ALPHA-C2 INTERSECTS Q-A CURVE.

C     CONTINUITY EQUATION TRIES TO FORCE Q.GT.QMAX.
C     LET DOWNSTREAM FLOW BE QFULL UNLESS UPSTREAM Q IS GT QFULL.
C=======================================================================
      NUMERR = NUMERR + 1
      IF(NPRINT.GE.1.AND.NUMERR.LT.50) THEN 
         WRITE(N6,900)
         IF(JCE.EQ.0) WRITE(N6,901) TIME,NOE(M),QFULL(M),AFULL(M),
     +                DXDT(M),Q(M,1,1),Q(M,1,2),Q(M,2,1),A(M,1,1),
     +                A(M,1,2),A(M,2,1),ALPHA,PS,C2,N
         IF(JCE.EQ.1) WRITE(N6,1901) TIME,KOE(M),QFULL(M),AFULL(M),
     +                DXDT(M),Q(M,1,1),Q(M,1,2),Q(M,2,1),A(M,1,1),
     +                A(M,1,2),A(M,2,1),ALPHA,PS,C2,N
         ENDIF
      IF((-C1(M)*ALFMAX(NT)-C2).GT.P4(M)) THEN
         IF(Q(M,1,2).GT.QFULL(M)) THEN
                                  Q(M,2,2) = Q(M,1,2)
                                  A(M,2,2) = A(M,1,2)
                                  ELSE
                                  IF(NPRINT.GE.1.AND.
     +                                      NUMERR.LT.50) WRITE(N6,903)
                                  NUMERR = NUMERR + 1
                                  Q(M,2,2) = QFULL(M)
                                  A(M,2,2) = AFULL(M)
                                  ENDIF
         RETURN
         ELSE
C=======================================================================
C     CONTINUITY EQUATION TRIES TO FORCE Q. LT. ZERO.
C     LET DOWNSTREAM FLOW BE ZERO.
C=======================================================================
         IF(C2.GT.0.0) THEN
                       NUMERR = NUMERR + 1
                       IF(NPRINT.GE.1.AND.NUMERR.LT.50) WRITE(N6,904)
                       Q(M,2,2) = 0.0
                       A(M,2,2) = 0.0
                       RETURN
                       ENDIF
C=======================================================================
C     REASON FOR NON-CONVERGENCE UNDETERMINED.
C     LET NEW Q AND A EQUAL VALUES AT PREVIOUS TIME STEP.
C=======================================================================
         IF(NPRINT.GE.1.AND.NUMERR.LT.50) WRITE(N6,902)
                   NUMERR   = NUMERR + 1
                   A(M,2,2) = A(M,2,1)
                   Q(M,2,2) = Q(M,2,1)
                   RETURN
                   ENDIF
      ENDIF
C=======================================================================
C     ROUTINE FOR CONDUITS WITH TABULAR Q-A RELATIONSHIP.
C=======================================================================
      IF(K.EQ.2)   THEN
      IF(QI(M).EQ.0.0.AND.Q(M,2,1).LE.1.0E-20) THEN
                                               A(M,2,2) = 0.0
                                               Q(M,2,2) = 0.0
                                               RETURN
                                               ENDIF
      IF(NT.EQ.16) THEN
                   KK     = NQC(M)
                   DALPHA = 0.04
                   MMM    = 26
                   ELSE
                   DALPHA = ANORM(NT,2) - ANORM(NT,1)
                   MMM    = MM(NT)
                   ENDIF 
C=======================================================================
C     CALCULATE UPSTREAM AREA.
C=======================================================================
      IF(KFULL.EQ.1) THEN
                     A(M,1,2) = AFULL(M)
                     ELSE
                     PS = Q(M,1,2)/QFULL(M)
                     CALL FINDA(PS,A(M,1,2))
                     ENDIF
C=======================================================================
C     CALCULATE DOWNSTREAM Q AND A.
C=======================================================================
                               ISIGN =  1
      IF(A(M,1,2).LE.A(M,1,1)) ISIGN = -1
      ICHK = 1
      I    = IOLD(M)
      C2   = (1.0-WD)*Q(M,2,1)-(1.0-WD)*Q(M,1,1)-WD*Q(M,1,2)
      C2   = C2 + DXDT(M)*((1.0-WT)*A(M,1,2) - 
     +                     (1.0-WT)*A(M,1,1) - WT*A(M,2,1))
      C2   = C2/QFULL(M)/WD
C=======================================================================
C     CALCULATE SLOPE OF LINE SEGMENT I OF Q-A CURVE.
C=======================================================================
  120 IF(NT.EQ.16) THEN
                   KK    = NQC(M)
                   SLUPE = (QCURVE(KK,3,I+1) - QCURVE(KK,3,I))/DALPHA
                   ELSE
                   SLUPE = (QNORM(NT,I+1) - QNORM(NT,I))/DALPHA
                   ENDIF
      IF(SLUPE+C1(M).EQ.0.0) GO TO 130
C=======================================================================
C     COMPUTE ALPHA CORRESPONDING TO INTERSECTION OF LINE SEGMENT
C     I OF Q-A CURVE WITH LINE -C1*ALPHA-C2.
C     CHECK TO SEE IF ALPHA IS IN PROPER RANGE.
C=======================================================================
      IF(NT.EQ.16) THEN
            KK    = NQC(M)
            ALPHA = (-QCURVE(KK,3,I)-C2+SLUPE*FLOAT(I-1)*0.04) / 
     +                                     (SLUPE + C1(M))
            ELSE
            ALPHA = (-QNORM(NT,I)-C2+SLUPE*ANORM(NT,I))/(SLUPE+C1(M))
            ENDIF
      IF(ALPHA.GT.1.0.OR.ALPHA.LT.0.0) GO TO 125
      L  =  IFIX(ALPHA/DALPHA) + 1
      IF(NT.EQ.16) THEN
            IF(L.GT.25) L = 25
            KK    = NQC(M)
            PS    = QCURVE(KK,3,L) + (ALPHA - FLOAT(L-1)*0.04 )/DALPHA
     1                          * (QCURVE(KK,3,L+1) - QCURVE(KK,3,L))
            ELSE
            PS    = QNORM(NT,L) + (ALPHA-ANORM(NT,L))/DALPHA
     1                          * (QNORM(NT,L+1) - QNORM(NT,L))
            ENDIF
      IF(ABS(PS+C1(M)*ALPHA+C2).LE.EPSIL) GO TO 150
C=======================================================================
C     TRY NEXT LINE SEGMENT.
C=======================================================================
  125 I = I+ISIGN
  126 IF(I.GT.0.AND.I.LT.MMM) GO TO 120
C=======================================================================
C     CONDITION FOR ICHK = 2
C=======================================================================
      IF(ICHK.EQ.2) THEN
              NUMERR = NUMERR + 1
              IF(NPRINT.GE.1.AND.NUMERR.LT.50) THEN 
               WRITE(N6,900)
               IF(JCE.EQ.0) WRITE(N6,901)TIME,NOE(M),QFULL(M),AFULL(M),
     +                      DXDT(M),Q(M,1,1),Q(M,1,2),Q(M,2,1),A(M,1,1),
     +                      A(M,1,2),A(M,2,1),ALPHA,PS,C2,N
               IF(JCE.EQ.1) WRITE(N6,1901)TIME,KOE(M),QFULL(M),AFULL(M),
     +                      DXDT(M),Q(M,1,1),Q(M,1,2),Q(M,2,1),A(M,1,1),
     +                      A(M,1,2),A(M,2,1),ALPHA,PS,C2,N
               ENDIF
               IF((-C1(M)*ALFMAX(NT)-C2).GT.P4(M)) THEN
               IF(Q(M,1,2).GT.QFULL(M)) THEN
                                        Q(M,2,2) = Q(M,1,2)
                                        A(M,2,2) = A(M,1,2)
                                        ELSE
                                        IF(NPRINT.GE.1.AND.NUMERR.
     +                                            LT.50) WRITE(N6,903)
                                        Q(M,2,2) = QFULL(M)
                                        A(M,2,2) = AFULL(M)
                                        NUMERR = NUMERR + 1
                                        ENDIF
               RETURN
               ELSE
               IF(C2.GT.0.0) THEN
                             IF(NPRINT.GE.1.AND.NUMERR.LT.50)
     +                                          WRITE(N6,904)
                             Q(M,2,2) = 0.0
                             A(M,2,2) = 0.0
                             NUMERR = NUMERR + 1
                             RETURN
                             ENDIF
               IF(NPRINT.GE.1.AND.NUMERR.LT.50) WRITE(N6,902)
               A(M,2,2) = A(M,2,1)
               Q(M,2,2) = Q(M,2,1)
               NUMERR = NUMERR + 1
               RETURN
               ENDIF
      ENDIF
C=======================================================================
C     END OF ICHK = 2 CONDITIONAL STATEMENTS
C=======================================================================
C     IF I HAS REACHED ZERO OR MMM START AT IOLD AND GO OTHER WAY
C=======================================================================
      ISIGN = -ISIGN
      I     = IOLD(M)+ISIGN
      ICHK  = 2
      GO TO 126
C=======================================================================
C     LINE-C1*ALPHA-C2 AND LINE SEGMENT ARE PARALLEL.
C     CHECK TO SEE IF THEY ARE CO-LINEAR.
C=======================================================================
  130 IF(NT.EQ.16) THEN
      KK    = NQC(M)
      IF(ABS(C2+QCURVE(KK,3,I)-SLUPE*FLOAT(I-1)*.04).GT.EPSIL) GO TO 125
      ALPHA = QCURVE(KK,2,I) + DALPHA/2.0
      L     = IFIX(ALPHA/DALPHA) + 1
      PS    = QCURVE(KK,3,L) + (ALPHA - FLOAT(I-1)*0.04)/DALPHA
     +                       * (QCURVE(KK,3,L+1) - QCURVE(KK,3,L) )
      ELSE
      IF(ABS(C2 + QNORM(NT,I) - SLUPE*ANORM(NT,I)).GT.EPSIL) GO TO 125
      ALPHA = ANORM(NT,I)    + DALPHA/2.0
      L     = IFIX(ALPHA/DALPHA) + 1
      PS    = QNORM(NT,L)    + (ALPHA-ANORM(NT,L))/DALPHA
     +                       * (QNORM(NT,L+1) - QNORM(NT,L))
      ENDIF
 150  IOLD(M) = I
C=======================================================================
C     IF VERY LOW FLOW, OBTAIN REFINED CALCULATION FOR CIRCULAR PIPE
C=======================================================================
      IF(ALPHA.LT.0.04.AND.NT.EQ.1) THEN
               CALL NEWTON(ALPHA,PS,C1(M),C2,KFLAG)
               IF(KFLAG.EQ.1) THEN
                              A(M,2,2) = ALPHA*AFULL(M)
                              Q(M,2,2) = PS*QFULL(M)
                              RETURN
                              ENDIF
              IF(NPRINT.GE.1.AND.NUMERR.LT.50) THEN 
               WRITE(N6,900)
               IF(JCE.EQ.0) WRITE(N6,901)TIME,NOE(M),QFULL(M),AFULL(M),
     +                      DXDT(M),Q(M,1,1),Q(M,1,2),Q(M,2,1),A(M,1,1),
     +                      A(M,1,2),A(M,2,1),ALPHA,PS,C2,N
               IF(JCE.EQ.1) WRITE(N6,1901)TIME,KOE(M),QFULL(M),AFULL(M),
     +                      DXDT(M),Q(M,1,1),Q(M,1,2),Q(M,2,1),A(M,1,1),
     +                      A(M,1,2),A(M,2,1),ALPHA,PS,C2,N
               NUMERR = NUMERR + 1
               ENDIF
               IF((-C1(M)*ALFMAX(NT)-C2).GT.P4(M)) THEN
               IF(Q(M,1,2).GT.QFULL(M)) THEN
                                        Q(M,2,2) = Q(M,1,2)
                                        A(M,2,2) = A(M,1,2)
                                        ELSE
                                        IF(NPRINT.GE.1.AND.NUMERR.
     +                                            LT.50) WRITE(N6,903)
                                        Q(M,2,2) = QFULL(M)
                                        A(M,2,2) = AFULL(M)
                                        NUMERR = NUMERR + 1
                                        ENDIF
               RETURN
               ELSE
               IF(C2.GT.0.0) THEN
                             IF(NPRINT.GE.1.AND.
     +                                 NUMERR.LT.50) WRITE(N6,904)
                             Q(M,2,2) = 0.0
                             A(M,2,2) = 0.0
                             NUMERR   = NUMERR + 1
                             RETURN
                             ENDIF
               IF(NPRINT.GE.1.AND.NUMERR.LT.50) WRITE(N6,902)
               A(M,2,2) = A(M,2,1)
               Q(M,2,2) = Q(M,2,1)
               NUMERR = NUMERR + 1
               RETURN
               ENDIF
      ENDIF
      A(M,2,2) = ALPHA * AFULL(M)
      Q(M,2,2) =    PS * QFULL(M)
      RETURN
      ENDIF
C===================================================================      
C 
C=========> ELEMENT IS NOT A CONDUIT.
C
C====================================================================
      NGOTO = NT - 18
C====================================================================
C     ERROR ===> CONDUITS SHOULD BE KLASS 1 OR 2
C====================================================================
      IF(NGOTO.LT.1) THEN
                     QO(M)  = QI(M)
                     NUMERR = NUMERR + 1
                     IF(NPRINT.GT.0.AND.NUMERR.LT.50) WRITE(N6,905) M
                     RETURN
                     ENDIF
C====================================================================
C     MANHOLE ===> SIMPLY TRANSLATE FLOW WITH NO TIME DELAY
C====================================================================
      IF(NGOTO.EQ.1) THEN
                     QO(M) = QI(M)
                     RETURN
                     ENDIF
C====================================================================
C     LIFT STATION ===> PUMPS ASSUMED TO BE PUMPING AT CONSTANT RATE
C     FORCE MAIN ASSUMED TO REMAIN FULL AT ALL TIMES RESULTING IN NO
C     VOLUME IN WET WELL INITIALLY IS HALF THE CAPACITY(GEOM1).
C     TIME DELAY.
C====================================================================
      IF(NGOTO.EQ.2) THEN
                     WELL2(M) = WELL2(M) + QI(M)*DT
                     IF(WELL2(M).GE.GEOM1(M)) THEN
                             IF(WELL2(M).GE.PUMP(M)*DT) THEN
                                         QO(M)    = PUMP(M)
                                         WELL2(M) = WELL2(M)-PUMP(M)*DT
                                         GEOM2(M) = 1.0
                                         RETURN
                                         ELSE
                                         QO(M)    = WELL2(M)/DT
                                         WELL2(M) = 0.0
                                         GEOM2(M) = 0.0
                                         RETURN
                                         ENDIF
                     ENDIF         
                     IF(GEOM2(M).LE.0.0) THEN
                             QO(M)    = 0.0
                             GEOM2(M) = 0.0
                             RETURN
                             ELSE
                             IF(WELL2(M).GE.PUMP(M)*DT) THEN
                                         QO(M)    = PUMP(M)
                                         WELL2(M) = WELL2(M)-PUMP(M)*DT
                                         GEOM2(M) = 1.0
                                         RETURN
                                         ELSE
                                         QO(M)    = WELL2(M)/DT
                                         WELL2(M) = 0.0
                                         GEOM2(M) = 0.0
                                         RETURN
                                         ENDIF
                     ENDIF
                     ENDIF
C====================================================================
C     ROUTINE FOR TYPE 21 AND TYPE 24 FLOW DIVIDERS.
C     TYPE 21 MAY BE USED FOR A SIMPLE OVERFLOW STRUCTURE.
C     TYPE 24 MAY BE USED WITH A CUNNETTE SECTION DOWNSTREAM.
C====================================================================
      IF(NGOTO.EQ.3.OR.NGOTO.EQ.6) THEN
                     QO(M) = QI(M)
                     IF(GEOM1(M).GT.0.0)   THEN
                     IF(QI(M).LE.GEOM1(M)) THEN
                                            QO1(M) = QI(M)
                                            QO2(M) = 0.0
                                            ELSE
                                            QO1(M) = GEOM1(M)
                                            QO2(M) = QI(M)-GEOM1(M)
                                            ENDIF
                     ELSE
C====================================================================
C     MODIFICATION MADE 9/26/89 TO ALLOW GEOM1(M) TO INDICATE
C     A PIPE THAT WILL ONLY ALLOW QFULL(M) TO PASS TO GEOM3(M).
C====================================================================
                     L      = GEOM3(M)
                     BMJ    = KGEOM(M)
                     NEXRAM = NIN(L,BMJ)
                     IF(QI(M).LE.QFULL(NEXRAM)) THEN
                                            QO1(M) = QI(M)
                                            QO2(M) = 0.0
                                            ELSE
                                            QO1(M) = QFULL(NEXRAM)
                                            QO2(M) = QI(M)-QFULL(NEXRAM)
                                            ENDIF
                     ENDIF
                     RETURN
                     ENDIF
C====================================================================
C     STORAGE ELEMENT. ADAPTED FROM STORAGE/TREATMENT BLOCK.
C====================================================================
CPDX--SM, 4/3/91------------------------------------------------
C     QOUST1 IS SECOND LEVEL OUTFLOW (LOUT=2), OR OVERFLOW (LOUT=4)
C
      IF(NGOTO.EQ.4) THEN
                     KSTOR = KSTORE(M)
                     QINST = QI(M) 
                     CALL TSTORG
                     QO1(M) = QOUST1
                     QO2(M) = QOUST2
                     QO(M)  = QOUST1 + QOUST2
                     RETURN
                     ENDIF
C====================================================================
C     ROUTINE FOR TYPE 23 FLOW DIVIDER.
C     USE WITH SIDE WEIR OR OTHER WEIR TYPE DIVERSION.
C     DIST  = MAX FLOW WITHOUT FLOW OVER WEIR.
C     SLOPE = MAXIMUM FLOW THROUGH WHOLE STRUCTURE.
C     ROUGH = WEIR CONSTANT TIMES WEIR LENGTH.
C     GEOM1 = WEIR HEIGHT.
C     GEOM2 = DEPTH IN STRUCTURE AT TIME OF MAXIMUM FLOW.
C     GEOM3 = DOWNSTREAM EXT. ELE. NUM. INTO WHICH GOES UNDIVERTED FLOW.
C             FLOW OVER WEIR IS THE DIVERTED FLOW (qo2).
C====================================================================
      IF(NGOTO.EQ.5) THEN

CPDX--SM, 9/7/91--------------------------------------------------------
C FOR USE AS A REGULATOR WITH RULE CURVE DICTATED MAX UNDERFLOW
C   FIRST FIND DOWNSTREAM ELEMENT AND ALLOWABLE FLOW FROM RULE CURVE

                      IS    = KSTORE(M)
C   Given control node in GEOM3, find last outflow of control node.
CPDX--SM, 4/5/92--MODIFIED TO PERMIT ANY ARBITRARY CONTROL NODE---------
C                 STORE INTERNAL ELEMENT IN RULEV(IS,18)
C
           IF(RULEV(IS,18).LE.0)THEN

               L        = GEOM3(M)
               BMJ      = KGEOM(M)
               L        = NIN(L,BMJ)
               QCNTL    = QO(L)

           ELSE

               L=RULEV(IS,18)
               QCNTL=QO(L)

           ENDIF

           MINT     = RULEC(IS,18)

C     NOW FIND THE CORRESPONDING MAX ALLOWABLE REGULATOR DIVERSION
C
      IF(MINT.GT.0)THEN
         DO 400 J = 2,MINT
          L        = J
          IF(QCNTL-RULEC(IS,J)) 402,401,400
  400     CONTINUE
  401     QCNTL = RULEV(IS,L)
          GO TO 403
  402     QCNTL = RULEV(IS,L-1)+(RULEV(IS,L)-RULEV(IS,L-1))*
     +     (QCNTL-RULEC(IS,L-1))/(RULEC(IS,L)-RULEC(IS,L-1))
  403    CONTINUE

      ELSE
         QCNTL=DIST(M)
      ENDIF

                     QO(M) = QI(M)
*-------------------------------------------------------
CPDX 1/18/93 - VCA:  Setup IF/THEN Block to bypass flows higher than SLOPE(M)
*                    which is the assumed maximum inflow.
*
***                     IF(QI(M).GT.SLOPE(M).AND.JCE.EQ.0) WRITE(N6,920) 
***     +                                     TIME,NOE(M),QI(M)
***                     IF(QI(M).GT.SLOPE(M).AND.JCE.EQ.1) WRITE(N6,921) 
***     +                                     TIME,KOE(M),QI(M)
                BYPASS = 0.
                IF(QI(M).GT.SLOPE(M).AND.JCE.EQ.0) THEN
***                        WRITE(N6,920)  TIME,NOE(M),QI(M)
                        BYPASS = QI(M) - SLOPE(M)
                ENDIF
                IF(QI(M).GT.SLOPE(M).AND.JCE.EQ.1) THEN
***                        WRITE(N6,921)  TIME,KOE(M),QI(M)
                        BYPASS = QI(M) - SLOPE(M)
                ENDIF
*-------------
                     IF (QI(M).LE.DIST(M)) THEN
                                           QO2(M) = 0.0
                                           QO1(M) = QI(M)
                                           RETURN
                                           ENDIF
*
* CPDX VCA 10/1/93:  Check for SLOPE < or = DIST
*
         IF(SLOPE(M).LE.DIST(M)) THEN
            IF(JCE.LE.0) WRITE(N6,601) NOE(M)
            IF(JCE.LE.0) WRITE(*,601) NOE(M)
            IF(JCE.GT.0) WRITE(*,602) KOE(M)
            IF(JCE.GT.0) WRITE(N6,602) KOE(M)
            STOP
         ENDIF
* 
*
CPDX VCA 1/18/93:  Account for BYPASS & Check for GEOM2 < GEOM1
**                     DH     = (QI(M)-DIST(M))/(SLOPE(M)-DIST(M)) * 
**     +                                        (GEOM2(M)-GEOM1(M))
         IF(GEOM1(M).GT.GEOM2(M)) THEN
            IF(JCE.LE.0) WRITE(N6,604) NOE(M)
            IF(JCE.LE.0) WRITE(*,604) NOE(M)
            IF(JCE.GT.0) WRITE(*,605) KOE(M)
            IF(JCE.GT.0) WRITE(N6,605) KOE(M)
            STOP
         ENDIF
* 
                DH     = (QI(M)-DIST(M)-BYPASS)/(SLOPE(M)-DIST(M)) * 
     +                                          (GEOM2(M)-GEOM1(M))
*
CPDX--SM, 7.17.91-------------------------------------------------------
C EXPONENT CHANGED TO ONE IN FOLLOWING STATEMENT PER VIRGIL'S OBSERVATIONS
C  OF PORTLAND DIVERSIONS--ANY ARBITRARY EXPONENT MAY BE USED.
CCC                     QO2(M) = ROUGH(M)*DH*SQRT(DH)
CPDX VCA 1/18/93     QO2(M) = ROUGH(M)*DH**T23EXP
*
                     QO2(M) = BYPASS + ROUGH(M)*DH**T23EXP

CPDX--------------------------------------------------------------------
                     QO1(M) = QI(M) - QO2(M)
                     QO1(M) = AMAX1(QO1(M),DIST(M))
CPDX------------------
                  IF(MINT .GT. 0)THEN
                     QO1(M) = AMIN1(QO1(M),QCNTL)
                     ENDIF
CPDX------------------
                     QO2(M) = QI(M) - QO1(M)
*  VCA DEBUG 11/14/97
*      WRITE(N6,644) NOE(M),QI(M),QO1(M),QO2(M),DIST(M),SLOPE(M),DH
* 644  FORMAT(' Diversion ',I9,'  Qin=',F5.2,'  Qun=',F5.2,'  Qov=',F5.2,
*     + '  DIST= ',F5.2,'  SLOPE=',F7.2,'  DH=',F6.3)
                     RETURN
                     ENDIF
C====================================================================
C     ROUTINE FOR BACKWATER ELEMENT.
C     ELEMENT ACTS AS FLOW DIVIDER.
C     GEOM3 = ELEMENT NUMBER OF DOWNSTREAM STORAGE UNIT.
C
C     IF BACKWATER EXTENDS ALL THE WAY UP TO ELEMENT,
C     THEN WHOLE FLOW IS DIVERTED TO STORAGE ELEMENT.
C     ASSUME LENGTH OF BACKWATER IS PROPORTIONAL TO SQRT OF STORAGE
C     VOLUME.
C====================================================================
      IF(NGOTO.EQ.7) THEN
               QO(M)  = QI(M)
               L      = GEOM3(M)
               BMJ    = KGEOM(M)
               L      = NIN(L,BMJ)
               KSTOR  = KSTORE(L)
               MINT   = MINTS(KSTOR)
               QO1(M) = QI(M)*SQRT(STORL(KSTOR)/TSTORE(KSTOR,MINT))
               IF(QO1(M).GT.QI(M)) QO1(M) = QI(M)
               QO2(M) = QI(M) - QO1(M)
               RETURN
               ENDIF
C====================================================================
c
CPDX--SM, 4/3/91------------------------------------------------------
c new routine to simulate regulator stations with storage (NTYPE=26).
c  Provides a variable orfice for use in a storage node based
c    on a rule curve and head or flow at the control node.
c  Identify in TSTORG by LOUT=-1.
c  Assume control node identified by GEOM3 or KGEOM (undiverted flow).
c  Assume rule curve given by array RULE(controled var,max diversion).
C     RULEC,V(nste,17) added to tst.inc (file named tstpdx.inc).
c  Assume diverted flow is given by power curve in TSTORG, with the
c    maximum allowable outflow (from the rule curve) given by GEOM1(M).
C
      IF(NGOTO.EQ.8) THEN
                      IS    = KSTORE(M)
                      QINST = QI(M) 
c   Given control node in GEOM3, find last outflow (inflow) of control node.

               L        = GEOM3(M)
               BMJ      = KGEOM(M)
               L        = NIN(L,BMJ)
ccc               QCNTL    = QI(L)
               QCNTL    = QO(L)
               MINT     = RULEC(IS,18)
C
C     NOW FIND THE CORRESPONDING MAX ALLOWABLE REGULATOR DIVERSION
C
      DO 199 J = 2,MINT
      L        = J
      IF(QCNTL-RULEC(IS,J)) 300,200,199
  199 CONTINUE
CPDX:  VCA: 10/1/93:  NEED GO TO 350 IF NO RULE CURVE
      GO TO 350
  200 GEOM1(M) = RULEV(IS,L)
      GO TO 350
  300 GEOM1(M) = RULEV(IS,L-1)+(RULEV(IS,L)-RULEV(IS,L-1))*
     +  (QCNTL-RULEC(IS,L-1))/(RULEC(IS,L)-RULEC(IS,L-1))
C
C     AND CALL TSTORG TO COMPLETE CALCS
C
350                   CALL TSTORG
                      QO1(M) = QOUST1
                      QO2(M) = QOUST2
                      QO(M)  = QOUST1 + QOUST2
                      RETURN
                      ENDIF
CPDX------------------------------------------------------------------
C====================================================================
  601 FORMAT(/,' ===> ERROR!  SLOPE <,= DIST FOR TYPE 23 FLOW DIVIDER!',
     +       /,'              ELEMENT #',I9)
  602 FORMAT(/,' ===> ERROR!  SLOPE <,= DIST FOR TYPE 23 FLOW DIVIDER!',
     +       /,'              ELEMENT #',A10)
  604 FORMAT(/,' ===> ERROR!  GEOM1 > GEOM2 FOR TYPE 23 FLOW DIVIDER!',
     +       /,'              ELEMENT #',I9)
  605 FORMAT(/,' ===> ERROR!  GEOM1 > GEOM2 FOR TYPE 23 FLOW DIVIDER!',
     +       /,'              ELEMENT #',A10)
  900 FORMAT(/,' ===> ITERATION FOR DOWNSTREAM FLOW HAS NOT CONVERGED.',
     +/,'      Q(CHANNEL,1-UPSTREAM 2-DOWNSTREAM,1-START 2-END)',/,3X
     1,'TIME  ELEMENT  QFULL  AFULL  DXDT  Q(M,1,1) Q(M,1,2) Q(M,2,1) A(
     2M,1,1) A(M,1,2) A(M,2,1)  ALPHA     PS     C2 TIME STEP')
  901 FORMAT(1PE8.2,I6,0PF9.1,F7.2,F7.1,3F9.2,3F9.3,3F7.3,I10)
 1901 FORMAT(F8.1,1X,A5,1X,F8.1,F7.2,F7.1,3F9.2,3F9.3,3F7.3,I10)
  902 FORMAT(/,' ===> REASON FOR NON-CONVERGENCE UNDETERMINED. USE Q,A V
     +ALUES AT PREVIOUS TIME STEP.')
  903 FORMAT(/,' ===> CONTINUITY EQN TRIES TO FORCE Q.GT.QMAX. USE QFULL
     + UNLESS UPSTREAM Q.GT.QFULL - THEN USE UPSTREAM VALUE.')
  904 FORMAT(/,' ===> CONTINUITY EQN TRIES TO FORCE Q < 0.0   USE',
     +         ' ZERO FLOW.')
  905 FORMAT(/,' ===> ERROR !! CONDUITS SHOULD BE CLASS 1 OR 2. M= ',I5)
  910 FORMAT(/,' ===> WARNING !!  NEWTON UNABLE TO FIND AREA GIVEN FLOW.
     + TIME = ',F7.1,', TIME STEP=',I3,/,
     +'       EXT. ELE. NUM.=',I8,', USE OLD UPSTREAM AREA = ',F6.2)
  911 FORMAT(/,' ===> WARNING !!  NEWTON UNABLE TO FIND AREA GIVEN FLOW.
     + TIME = ',F7.1,', TIME STEP=',I3,/,
     +'       EXT. ELE. NUM.= ',A10,', USE OLD UPSTREAM AREA = ',F6.2)
*  920 FORMAT(/,' ===> WARNING !!. TIME = ',E14.7,' ELEMENT',I9,
*     +  '      MAX FLOW TO TYPE 23 DIVIDER EXCEEDED. INFLOW = ',E14.7)
*  921 FORMAT(/,' ===> WARNING !!. TIME = ',E14.7,' ELEMENT ',A10,
*     +  '      MAX FLOW TO TYPE 23 DIVIDER EXCEEDED. INFLOW = ',E14.7)
  930 FORMAT(/,' ====> ALLOWABLE ERRORS IN ROUTE EXCEEDED.',/,
     +         ' ====> PRINTOUT OF ERRORS ENDED.')
C====================================================================
      END 
