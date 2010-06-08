      SUBROUTINE INDAT11P
C=======================================================================
C     This Subroutine reads and prints control, 
C     Conduit and Junction data.
C=======================================================================
      INCLUDE 'TAPES.INC'
      INCLUDE 'BD.INC'
      INCLUDE 'CONTR.INC'
      INCLUDE 'INTER.INC'
      INCLUDE 'JUNC.INC'
      INCLUDE 'PIPE.INC'
      INCLUDE 'STORE.INC'
      INCLUDE 'ORF.INC'
      INCLUDE 'WEIR.INC'
      INCLUDE 'PUMP.INC'
      INCLUDE 'BND.INC'
      INCLUDE 'OUT.INC'
      INCLUDE 'TIDE.INC'
      INCLUDE 'HYFLOW.INC'
      INCLUDE 'SURCHG.INC'
      INCLUDE 'FLODAT.INC'
CPDX--SM, 7/22/91-------------------------------------------------------
      INCLUDE 'PDXTRN.INC'
CPDX--------------------------------------------------------------------
CPDX--BC, 10/15/91------------------------------------------------------
C
C     ADDITIONAL VARIABLES TO PERMIT INPUT OF NON-STANDARD SECTIONS.
C       NOTE USE OF PIPE TYPES.
C       BRUCE CRAWFORD (Anne Symonds) & STEVE MERRILL (BCC) - OCT 91
C
C AMM 12/23/96 Changes per VCA
C              Number of USER defined shapes are set by the constant
C              NUM_CP defined in PDXTRN.INC; added pdx_16-20
C              pdxn used as loop counter to read through each set of 
C                10 CP cards
C      INTEGER pdx_3,pdx_4,pdx_5,pdx_13,pdx_14,pdx_15
C      INTEGER pdx_16,pdx_17,pdx_18,pdx_19,pdx_20
C AMM 1/13/97 Consolidated pdx_3... variables into pdxcp array; elements
C             6-20 are ignored since they are not custom shapes.
C             Added LASTCP constant to PDXTRN.INC; indicates last valid
C             shape number for CP cards
C             Added variable pdxar; used for accessing arrays for the
C             custom shapes instead of pdx_cl
      INTEGER pdxcp
      DIMENSION pdxcp(13:LASTCP)
      INTEGER i
      
      INTEGER p_i1
      INTEGER pdxn,pdx_cl,pdxar

      REAL    p_f1,p_f2,p_f3,p_f4,p_f5
      REAL    p_f6,p_f7,p_f8,p_f9,p_f10
      REAL    pdx_ca,pdx_cr,pdx_cw
C AMM 12/23/96 Changes per VCA
C              area, hydraulic radius, and width array sizes use constant
C              NUM_CP instead of hard-coded 3
      DIMENSION pdx_ca(NUM_CP),pdx_cr(NUM_CP),pdx_cw(NUM_CP)
C AMM 12/23/96 Added arrays for Portland pipe shape 16-20 area, hydraulic
C              radius, and width coefficients
      real pdxsha,pdxshr,pdxshw
      DIMENSION pdxsha(16:20),pdxshr(16:20),pdxshw(16:20)
CPDX--------------------------------------------------------------------
C AMM 1/13/97 Increased array size of CTYPE to accommodate custom shapes
C             21-30 and added data initializations for CTYPE; CTYPE data
C             must be changed if LASTCP changes
      CHARACTER GTYPE(2)*20,CTYPE(LASTCP)*10,KDUM(8)*10,IREAD*4
      DATA GTYPE/'  POSITIVE FLAP GATE','  NEGATIVE FLAP GATE'/
      DATA CTYPE/' CIRCULAR ',' RECTANGLE',' HORSESHOE',
     +           ' EGG-SHAPE',' BASKET   ',' TRAPEZOID',
     +           ' POWER FNC',' NATURAL  ',' SPECIAL  ',
     +           ' SPECIAL  ',' SPECIAL  ',' SPECIAL  ',
     +           'USR DEF 13','USR DEF 14','USR DEF 15',
     +           'SEMIELLIPT','KEYHOLE   ','BSKT-ROUND',
     +           'BSKT-TRAPZ','RECT-ROUND','USR DEF 21',
     +           'USR DEF 22','USR DEF 23','USR DEF 24',
     +           'USR DEF 25','USR DEF 26','USR DEF 27',
     +           'USR DEF 28','USR DEF 29','USR DEF 30'/
      DATA IREAD/'CARD'/
C AMM 12/23/96 Added data section to initialize arrays for Portland
C              pipe shapes
      DATA pdxsha/0.783100,0.448200,0.895393,0.867402,1.238800/
      DATA pdxshr/0.240400,0.166500,0.257993,0.252646,0.411800/
      DATA pdxshw/1.000000,1.000000,1.000000,1.000000,1.352900/
C=======================================================================
C                  E X E C U T I O N
C=======================================================================
      NSTOP  = 0
      ISOL   = 0
      JSLOT  = 0
      KSUPER = 0
C      N5 = 5
C      N6 = 6
      NCSAVE = NSCRAT(4)
      IF(NCSAVE.GT.0) REWIND NCSAVE
CPDX--BC, 10/15/91------------------------------------------------------
C
C     SET PDX FLAG VARIABLES TO ZERO (FALSE) (-1 IS TRUE)
C
C AMM 12/23/96 Added initializations for pdx_16-20
C AMM 1/13/97 Changed initializations for pdx_## to initialization of 
C             pdxcp array
      DO 267 i = 13,LASTCP
        pdxcp(i) = 0
267   CONTINUE       
C      pdx_3  = 0
C      pdx_4  = 0
C      pdx_5  = 0
C      pdx_13 = 0
C      pdx_14 = 0
C      pdx_15 = 0
C      pdx_16 = 0
C      pdx_17 = 0
C      pdx_18 = 0
C      pdx_19 = 0
C      pdx_20 = 0

C
C     SET PDX ARRAYS TO 0.0
C
C AMM 12/23/96 Changed pdxn loop from 1 to 3 to 1 to NUM_CP
      DO 266 pdxn = 1,NUM_CP
        pdx_ca(pdxn) = 0.0
        pdx_cr(pdxn) = 0.0
        pdx_cw(pdxn) = 0.0
  266 CONTINUE
CPDX--------------------------------------------------------------------
C=======================================================================
C     READ THE TWO A1 TITLE LINES 
C=======================================================================
      READ(N5,*,ERR=888) CC,ALPHA1
      READ(N5,*,ERR=888) CC,ALPHA2
 
      WRITE(N6,2999)
      WRITE(N6,5060) ALPHA1,ALPHA2
      WRITE(*,5060) ALPHA1,ALPHA2
C=======================================================================
C     READ SOLUTION PARAMETERS ON OPTIONAL DATA GROUP B0
C=======================================================================
      READ(N5,*,ERR=888) CC
      IF(CC.EQ.'B0') THEN
                     BACKSPACE N5
                     READ(N5,*,ERR=888) CC,ISOL,KSUPER
                     ELSE
                     BACKSPACE N5
                     ENDIF
C=======================================================================
C     READ SOLUTION PARAMETERS ON OPTIONAL DATA GROUP BB
C=======================================================================
      READ(N5,*,ERR=888) CC
      IF(CC.EQ.'BB') THEN
                     BACKSPACE N5
                     READ(N5,*,ERR=888) CC,JELEV,JDOWN
                     IF(JELEV.LT.0.OR.JELEV.GT.1) JELEV = 0
                     IF(JDOWN.LT.0.OR.JDOWN.GT.2) JDOWN = 0
                     ELSE
                     JELEV = 0
                     JDOWN = 0
                     BACKSPACE N5
                     ENDIF
CPDX--SM, 7/22/91--10/19/91---------------------------------------------
C READ NEW OPTIONAL BP CARD FOR PDX SPECIFIC SOLUTION PARAMETERS
C  IPSTEPS IS SKIP INTERVAL FOR SCREEN OUTPUT
C  ISTPCYC IS NUMBER OF CYCLES PERFORMED BEFORE PAUSE (0->NO PAUSE)
C  NRGPRNT CONTROLS OUTPUT FOR REGULATOR GATES
C
      READ(N5,*,ERR=888) CC
                     BACKSPACE N5
      IF(CC.EQ.'BP') THEN
                     READ(N5,*,ERR=888) CC,IPSTEPS,ISTPCYC
                     IF(IPSTEPS.LT.1)IPSTEPS=1
                     IF(ISTPCYC.LT.1)ISTPCYC=0
                     IF(NRPGPNT.LT.1)NRPGPNT=0
                     ELSE
                     IPSTEPS = 1
                     ISTPCYC = 0
                     NRGPNT  = 0
                     ENDIF
C READ NEW OPTIONAL BS CARD TO SAVE SPECIFIED NAMED CONDUITS
C
      READ(N5,*,ERR=888) CC
                     BACKSPACE N5
      IF(CC.EQ.'BS') THEN
                    IF(JCE.EQ.0)THEN
                     READ(N5,*,ERR=888) CC,NSAVEC,(SCOND(I),I=1,NSAVEC)
                    ELSE
                     READ(N5,*,ERR=888) CC,NSAVEC,(ASCOND(I),I=1,NSAVEC)
                    ENDIF
                   ENDIF
CPDX--------------------------------------------------------------------
C=======================================================================
C     GENERAL CONTROL PARAMETERS ON DATA GROUPS B1, B2 AND B3
C=======================================================================
      READ(N5,*,ERR=888) CC,NTCYC,DELT,TZERO,NSTART,INTER,JNTER,JREDO
      READ(N5,*,ERR=888) CC,METRIC,NEQUAL,AMEN,ITMAX,SURTOL
CPDX--SM, 11/11/91--NRGPRNT ADDED FOR REGULATOR GATES- WITH ERROR TRAP
      READ(N5,*,ERR=270) CC,NHPRT,NQPRT,NPLT,LPLT,NJSW,NRGPNT
      GO TO 271
270   BACKSPACE N5
      BACKSPACE N5
      READ(N5,*,ERR=888) CC,NHPRT,NQPRT,NPLT,LPLT,NJSW
      NRGPNT=0
271   CONTINUE
C=======================================================================
C     METRIC = 1 --> AMERICAN/ENGLISH UNITS
C     METRIC = 2 --> METRIC UNITS
C     JSLOT  = 0 --> SUM OF JUNCTION FLOW IS ZERO
C     JSLOT  = 1 --> PREISSMAN SLOT
C=======================================================================
      IF(SURTOL.LT.0.0) THEN
                        ISOL   = 1
                        SURTOL = ABS(SURTOL)
                        ENDIF
      IF(ISOL.GE.2)  JSLOT = 1
      METRIC               = METRIC + 1
      RDELT                = DELT
      DELT2                = DELT/2.0
      GRVT                 = 32.2
      IF(METRIC.EQ.2) GRVT = 9.806
      IF(AMEN.EQ.0.0.AND.METRIC.EQ.1) AMEN = 12.566
      IF(AMEN.EQ.0.0.AND.METRIC.EQ.2) AMEN =  1.22
      WRITE(N6,5100)  NTCYC
      WRITE(N6,5120)  DELT,DELT*FLOAT(NTCYC)/3600.0
      IF(NEQUAL.EQ.0) WRITE(N6,5121) NEQUAL
      IF(NEQUAL.GE.1) WRITE(N6,5122) NEQUAL
      IF(METRIC.EQ.1) WRITE(N6,5123) METRIC-1
      IF(METRIC.EQ.2) WRITE(N6,5124) METRIC-1
      IF(NSTART.LE.0) NSTART   = 1
      WRITE(N6,5140)  NSTART,INTER,FLOAT(INTER)*DELT/60.0,JNTER,
     +                FLOAT(JNTER)*DELT/60.0,JREDO
      WRITE(N6,5160)  TZERO
      WRITE(N6,5170)  ITMAX,SURTOL
      IF(METRIC.EQ.1) WRITE(N6,5175) AMEN
      IF(METRIC.EQ.2) WRITE(N6,5176) AMEN
      IF(ISOL.EQ.0)   WRITE(N6,5177) 
      IF(ISOL.EQ.1)   WRITE(N6,5178) 
      IF(ISOL.EQ.2)   WRITE(N6,5179) 
      IF(KSUPER.EQ.0) WRITE(N6,5183)
      IF(KSUPER.EQ.1) WRITE(N6,5184)
      TZERO  =  3600.0*TZERO
      ITMAX  = IABS(ITMAX)
      IF(NEQUAL.EQ.1) NEQUAL = INT(DELT)
      WRITE(N6,5180) NJSW
      IF(JELEV.GT.0) WRITE(N6,5185)
C=======================================================================
C     Read Junction numbers for print and plot data.
C=======================================================================
      IF(NHPRT.GT.0.AND.JCE.EQ.0) THEN
                     READ(N5,*,ERR=888) CC,(JPRT(I),I=1,NHPRT)
                     WRITE(N6,5200)  NHPRT,(JPRT(I),I=1,NHPRT)
                     ENDIF
      IF(NHPRT.GT.0.AND.JCE.EQ.1) THEN
                     READ(N5,*,ERR=888) CC,(AOUT(I,1),I=1,NHPRT)
                     WRITE(N6,5201)  NHPRT,(AOUT(I,1),I=1,NHPRT)
                     ENDIF
C=======================================================================
C     READ CONDUIT NUMBERS FOR DETAILED PRINTOUT
C=======================================================================
      IF(NQPRT.GT.0.AND.JCE.EQ.0) THEN
                     READ(N5,*,ERR=888) CC,(CPRT(I),I=1,NQPRT)
                     WRITE(N6,5220)  NQPRT,(CPRT(I),I=1,NQPRT)
                     ENDIF
      IF(NQPRT.GT.0.AND.JCE.EQ.1) THEN
                     READ(N5,*,ERR=888) CC,(AOUT(I,2),I=1,NQPRT)
                     WRITE(N6,5221)  NQPRT,(AOUT(I,2),I=1,NQPRT)
                     ENDIF
C=======================================================================
C     READ JUNCTION NUMBERS FOR PLOTTING
C=======================================================================
      IF(NPLT.GT.0.AND.JCE.EQ.0) THEN
                    READ(N5,*,ERR=888) CC,(JPLT(N),N=1,NPLT)
                    WRITE(N6,5240)     NPLT,(JPLT(N),N=1,NPLT)
                    ENDIF
      IF(NPLT.GT.0.AND.JCE.EQ.1) THEN
                    READ(N5,*,ERR=888) CC,(AOUT(N,3),N=1,NPLT)
                    WRITE(N6,5241)     NPLT,(AOUT(N,3),N=1,NPLT)
                    ENDIF
C=======================================================================
C     READ CONDUIT NUMBERS FOR PLOTTING
C=======================================================================
      IF(LPLT.GT.0.AND.JCE.EQ.0) THEN
                    READ(N5,*,ERR=888) CC,(KPLT(N),N=1,LPLT)
                    WRITE(N6,5260)     LPLT,(KPLT(N),N=1,LPLT)
                    ENDIF
      IF(LPLT.GT.0.AND.JCE.EQ.1) THEN
                    READ(N5,*,ERR=888) CC,(AOUT(N,4),N=1,LPLT)
                    WRITE(N6,5261)     LPLT,(AOUT(N,4),N=1,LPLT)
                    ENDIF
C=======================================================================
C     READ CONDUIT NUMBERS FOR PLOTTING WATER SURFACE SLOPE
C     DATA WILL BE ON DATA GROUP B8 IF PRESENT.
C=======================================================================
      READ(N5,*,ERR=888) CC
      IF(CC.EQ.'B8') THEN
                     BACKSPACE N5
                     IF(JCE.EQ.0) THEN
                     READ(N5,*,ERR=888) CC,NSURF,(JSURF(N),N=1,NSURF)
                     WRITE(N6,5265)        NSURF,(JSURF(N),N=1,NSURF)
                     ENDIF
                     IF(JCE.EQ.1) THEN
                     READ(N5,*,ERR=888) CC,NSURF,(AOUT(N,5),N=1,NSURF)
                     WRITE(N6,5266)        NSURF,(AOUT(N,5),N=1,NSURF)
                     ENDIF
                     ELSE
                     NSURF = 0
                     BACKSPACE N5
                     ENDIF
C=======================================================================
C     Read conduit data on data group C1.
C=======================================================================
CPDX--BC, 10/15/91------------------------------------------------------
C     THE CONDUIT DATA SECTION HAS BEEN MODIFIED TO ALLOW
C     NON STANDARD SECTIONS TO BE ENTERED USING ADDITIONAL
C     CARDS WITH THE DESIGNATOR CP
C
CPDX--------------------------------------------------------------------
      WRITE(*,6000)
      NATUR    = 0
      DO 260 N = 1,NEE
      READ(N5,*,ERR=888) CC
      BACKSPACE N5
      IF(CC.NE.'C1') GO TO 280
      IF(JCE.EQ.0) READ(N5,*,ERR=888) CC,NCOND(N),(NJUNC(N,K),K=1,2),
     +                  QO(N),NKLASS(N),AFULL(N),DEEP(N),WIDE(N),LEN(N),
     +                  ZU(N),ZD(N),ROUGH(N),STHETA(N),SPHI(N)
      IF(JCE.EQ.1) READ(N5,*,ERR=888) CC,ACOND(N),(KJUNC(N,K),K=1,2),
     +                  QO(N),NKLASS(N),AFULL(N),DEEP(N),WIDE(N),LEN(N),
     +                  ZU(N),ZD(N),ROUGH(N),STHETA(N),SPHI(N)
      IF(ROUGH(N).LE.0.0) ROUGH(N) = 0.014
C=======================================================================
C     IF LEN(N) IS NEGATIVE A FLAP GATE IS BEING MODELED
C        THIS FLAP GATE ONLY ALLOWS FLOW IN THE POSITIVE DIRECTION
C=======================================================================
      IF(LEN(N).LT.0) THEN
                      INGATE(N) = 1
                      LEN(N)    = ABS(LEN(N))
                      ENDIF
C=======================================================================
C     IF NKLASS(N) IS NEGATIVE A FLAP GATE IS BEING MODELED
C        THIS FLAP GATE ONLY ALLOWS FLOW IN THE NEGATIVE DIRECTION
C=======================================================================
      IF(NKLASS(N).LT.0) THEN
                         INGATE(N) = 2
                         NKLASS(N) = IABS(NKLASS(N))
                         ENDIF
C=======================================================================
C     NKLASS=1 CIRCULAR PIPE
C     NKLASS=2 RECTANGULAR PIPE
C     NKLASS=3 HORSESHOE PIPE
C     NKLASS=4 EGGSHAPED PIPE
C     NKLASS=5 BASKETHANDLE PIPE
C     NKLASS=6 TRAPEZOIDAL CHANNEL
C     NKLASS=7 PARABOLIC CROSS-SECTION OR POWER FUNCTION CROSS SECTION
C     NKLASS=8 IRREGULAR CROSS-SECTION 
C     NKLASS=9  ORIFICES (SEE BELOW)
C     NKLASS=10 ORIFICES (SEE BELOW)
C     NKLASS=13-15 USER DEFINED SHAPES (CP cards)
C     NKLASS=16 SEMIELLIPTICAL
C     NKLASS=17 KEYHOLE TUNNEL
C     NKLASS=18 MODIFIED BASKETHANDLE (ROUND BOTTOM)
C     NKLASS=19 MODIFIED BASKETHANDLE (TRAPEZOIDAL BOTTOM)
C     NKLASS=20 RECTANGULAR (ROUND BOTTOM)
C     NKLASS=21-LASTCP USER DEFINED SHAPES (CP cards)
C=======================================================================
      KLASS = NKLASS(N)
CPDX--BC, 10/15/91------------------------------------------------------
C
C     ALTER TO ALLOW CLASSES 13, 14 AND 15 TO PASS
C
C      IF(KLASS.EQ.1.OR.KLASS.GE.9) THEN
C
C AMM 1/13/97 Altered to allow classes 21-LASTCP to pass
      IF ((KLASS .EQ. 1) .OR. ((KLASS .GE. 9) .AND. (KLASS .LE. 12))
     +  .OR. (KLASS .GE. (LASTCP+1))) THEN
CPDX--------------------------------------------------------------------
C Circular parameters
               RFULL(N)  = DEEP(N)/4.0
               AFULL(N)  = (3.1415926/4.0)*DEEP(N)**2
               WIDE(N)   = DEEP(N)
      ENDIF
      
      IF(KLASS.EQ.2) THEN
C Rectangular parameters
               RFULL(N) = (WIDE(N)*DEEP(N))/(2.*WIDE(N)+2.0*DEEP(N))
               AFULL(N) =  WIDE(N)*DEEP(N)
               ENDIF
      IF(KLASS.EQ.3) THEN
CPDX--BC, 10/15/91------------------------------------------------------
C Horseshoe parameters
               IF(SPHI(N).LE.0)THEN
                 RFULL(N) = 0.25381  * DEEP(N)
                 ELSE
                 RFULL(N) = SPHI(N)*DEEP(N)
                 SPHI(N)=0
                 ENDIF
               IF(AFULL(N).LE.0)THEN
                 IF(STHETA(N).LE.0)THEN
                   AFULL(N)=0.8404 * DEEP(N)**2
                   ELSE
                   AFULL(N)=STHETA(N) * DEEP(N)**2
                   STHETA(N)=0
                   ENDIF
                   ENDIF
C AMM 1/13/97 Next statement no longer necessary since types 3-5 can
C             now coexist with types 13-15
C               pdx_3 = -1
CPDX--------------------------------------------------------------------
               ENDIF
      IF(KLASS.EQ.4) THEN
CPDX--BC, 10/15/91------------------------------------------------------
C Egg parameters
               IF(SPHI(N).LE.0)THEN
                 RFULL(N) = 0.19311 * DEEP(N)
                 ELSE
                 RFULL(N) = SPHI(N)*DEEP(N)
                 SPHI(N)=0
                 ENDIF
               IF(AFULL(N).LE.0)THEN
                 IF(STHETA(N).LE.0)THEN
                   AFULL(N)=0.5105*DEEP(N)**2
                   ELSE
                   AFULL(N)=STHETA(N) * DEEP(N)**2
                   STHETA(N)=0
                   ENDIF
                   ENDIF
C AMM 1/13/97 Next statement no longer necessary since types 3-5 can
C             now coexist with types 13-15
C               pdx_4 = -1
CPDX--------------------------------------------------------------------
               ENDIF
      IF(KLASS.EQ.5) THEN
CPDX--BC, 10/15/91------------------------------------------------------
C Baskethandle parameters
               IF(SPHI(N).LE.0)THEN
cpdx               RFULL(N) = 0.28800*DEEP(N)
                              RFULL(N) = 0.2553*DEEP(N)
                 ELSE
                 RFULL(N) = SPHI(N)*DEEP(N)
                 SPHI(N)=0
                 ENDIF
               IF(AFULL(N).LE.0)THEN
                 IF(STHETA(N).LE.0)THEN
                   AFULL(N)=0.83127*DEEP(N)**2
                   ELSE
                   AFULL(N)=STHETA(N) * DEEP(N)**2
                   STHETA(N)=0
                   ENDIF
                   ENDIF
C AMM 1/13/97 Next statement no longer necessary since types 3-5 can
C             now coexist with types 13-15
C               pdx_5 = -1
CPDX--------------------------------------------------------------------
               ENDIF
      IF(KLASS.EQ.6) THEN
C Trapezoidal channel parameters      
               AFULL(N)=DEEP(N)*(WIDE(N)+DEEP(N)/2.*(STHETA(N)+SPHI(N)))
               IF(WIDE(N).LE.0.0) WIDE(N) = 0.01
               RFULL(N)=AFULL(N)/(WIDE(N)+DEEP(N)*
     +                  (SQRT(1.0+STHETA(N)**2)+SQRT(1.0+SPHI(N)**2)))
               ENDIF
      IF(KLASS.EQ.7.AND.STHETA(N).EQ.0.0) THEN
C Parabolic channel parameters
               AFULL(N) = 0.666666667*WIDE(N)*DEEP(N)
               X      = WIDE(N)/2.0
               X1     = WIDE(N)**4/(64.0*DEEP(N)**2)
               WETPER = 8.0*DEEP(N)/WIDE(N)**2*(X*SQRT(X1+X**2) 
     +                             +  X1*LOG(X+SQRT(X1+X**2)) - 
     +                                X1*LOG(SQRT(X1)))       
               RFULL(N) = AFULL(N)/WETPER 
               ENDIF
      IF(KLASS.EQ.7.AND.STHETA(N).GT.0.0) THEN
               AFULL(N) = WIDE(N)*DEEP(N)*(1.0 - 1.0/(STHETA(N)+1.0))
               X      = WIDE(N)/2.0
               X1     = WIDE(N)**4/(64.0*DEEP(N)**2)
               WETPER = 8.0*DEEP(N)/WIDE(N)**2*(X*SQRT(X1+X**2) 
     +                             +  X1*LOG(X+SQRT(X1+X**2)) - 
     +                                X1*LOG(SQRT(X1)))       
               RFULL(N) = AFULL(N)/WETPER 
               ENDIF
C AMM 12/23/96 Added code to calculate AFULL and RFULL of the new
C              Portland pipe shapes 16-20
      IF (KLASS.GE.16.AND.KLASS.LE.20) THEN
        IF(SPHI(N).LE.0)THEN
          RFULL(N) = pdxshr(KLASS)*DEEP(N)
        ELSE
          RFULL(N) = SPHI(N)*DEEP(N)
          SPHI(N)=0
        ENDIF
        IF(AFULL(N).LE.0)THEN
          IF(STHETA(N).LE.0)THEN
            AFULL(N)=pdxsha(KLASS)*DEEP(N)**2
          ELSE
            AFULL(N)=STHETA(N)*DEEP(N)**2
            STHETA(N)=0
          ENDIF
        ENDIF
        IF(WIDE(N).LE.0)THEN
          WIDE(N) = pdxshw(KLASS)*DEEP(N)
        ENDIF
      ENDIF
CPDX--SM/BC 9/27/91-----------------------------------------------------
C
C     READ PDX SPECIAL SECTIONS - PIPE TYPES 13 14 AND 15
C     IF THIS IS THE FIRST CARD OF ONE OF THESE PIPE TYPES
C     THEN THE SECTION DATA MUST FOLLOW THE C1 CARD
C     IN A SET OF 10 CP CARDS
C     CP1          CONTAINS THE FULL AREA, HR AND TW COEFFS
C                    IT ALSO CONTAINS THE OLD SECTION NUMBER
C                    WHICH IS READ AND IGNORED
C     CP2 TO CP4   ARE THE NORMALIZED AREA
C     CP5 TO CP7   ARE THE NORMALIZED HYDRAULIC RADIUS
C     CP8 TO CP10  ARE THE NORMALIZED TOP (SURFACE) WIDTH
C
      IF (((KLASS .GE. 13) .AND. (KLASS .LE. 15)) .OR. 
     +    ((KLASS .GE. 21) .AND. (KLASS .LE. LASTCP)))THEN
C AMM 12/23/96 Changes per VCA
C              Allow for both types 3-5 and 13-15 simultaneously
C              Changed CTYPEs from 3,4,5 to 13,14,15
C AMM 1/13/97 Allow for types 21-LASTCP
        pdx_read = 0
        IF (pdxcp(KLASS) .EQ. 0) THEN
          pdxcp(KLASS) = -1
          pdx_read = -1
        ENDIF
        NKLASS(N) = KLASS
        pdx_cl = KLASS
                
C        IF (KLASS .EQ. 13) THEN
C          IF (pdx_13 .EQ. 0) THEN
C      IS FIRST NKLASS = 13 CARD
C            pdx_13 = -1
C            pdx_read = -1
C            CTYPE(13) = ' USRDEF#13'
C          ENDIF
C          NKLASS(N) = 13
C          pdx_cl = 13
C        ENDIF
C        IF (KLASS .EQ. 14) THEN
C          IF (pdx_14 .EQ. 0) THEN
C      IS FIRST NKLASS = 14 CARD
C            pdx_14 = -1
C            pdx_read = -1
C            CTYPE(14) = ' USRDEF#14'
C          ENDIF
C          NKLASS(N) = 14
C          pdx_cl = 14
C        ENDIF
C        IF (KLASS .EQ. 15) THEN
C          IF (pdx_15 .EQ. 0) THEN
C      IS FIRST NKLASS = 15 CARD
C            pdx_15 = -1
C            pdx_read = -1
C            CTYPE(15) = ' USRDEF#15'
C           ENDIF
C          NKLASS(N) = 15
C          pdx_cl = 15
C       ENDIF

        IF (pdx_read .NE. 0) THEN
C IS FIRST CARD OF TYPE SO READ SECTION CARDS
          DO 265 pdxn = 1,10
            READ(N5,*,ERR=888)CC
C TRAP ERRORS IN CARD TYPE
C AMM 1/13/97 Change the following error message if you change the number
C             of custom shapes (NUM_CP)
           IF (CC .NE. 'CP') THEN
           WRITE(*,6010)'===> ERROR !!! Expected Section Data Not Found'
              WRITE(*,6010)'              If pipe types 13-15 or 21-30'
              WRITE(*,6010)'              are used the first C1 card'
              WRITE(*,6010)'              of that type must be'
              WRITE(*,6010)'              followed by ten CP cards'
              WRITE(*,6010)'              describing properties'
              IF(JCE.EQ.0)WRITE(*,'(A,I10)')' CONDUIT ',NCOND(N)
              IF(JCE.EQ.1)WRITE(*,'(A,A10)')' CONDUIT ',ACOND(N)
              STOP
            ENDIF
            BACKSPACE N5
C AMM 1/13/97 Changed the following assignment to the pdx_ca,cr,cw arrays
C             to account for shapes 21-LASTCP; use pdxar as the array
C             element instead of using pdx_cl directly
            IF (pdx_cl .GE. 21) THEN
              pdxar = pdx_cl - 17
            ELSE
              pdxar = pdx_cl - 12
            ENDIF
            
            IF (pdxn .EQ. 1) THEN
              READ (N5,*,ERR=888)CC,p_i1,pdx_ca(pdxar),
     +          pdx_cr(pdxar),pdx_cw(pdxar)
            ENDIF
C
C     READ IN THE AREA CARDS
C
            IF (pdxn .EQ. 2) THEN
              READ (N5,*,ERR=888)CC,p_f1,p_f2,p_f3,p_f4,p_f5,p_f6,p_f7,
     +                              p_f8,p_f9,p_f10
              ANORM(1,pdx_cl) = p_f1
              ANORM(2,pdx_cl) = p_f2
              ANORM(3,pdx_cl) = p_f3
              ANORM(4,pdx_cl) = p_f4
              ANORM(5,pdx_cl) = p_f5
              ANORM(6,pdx_cl) = p_f6
              ANORM(7,pdx_cl) = p_f7
              ANORM(8,pdx_cl) = p_f8
              ANORM(9,pdx_cl) = p_f9
              ANORM(10,pdx_cl) = p_f10
            ENDIF
            IF (pdxn .EQ. 3) THEN
              READ (N5,*,ERR=888)CC,p_f1,p_f2,p_f3,p_f4,p_f5,p_f6,p_f7,
     +                              p_f8,p_f9,p_f10
              ANORM(11,pdx_cl) = p_f1
              ANORM(12,pdx_cl) = p_f2
              ANORM(13,pdx_cl) = p_f3
              ANORM(14,pdx_cl) = p_f4
              ANORM(15,pdx_cl) = p_f5
              ANORM(16,pdx_cl) = p_f6
              ANORM(17,pdx_cl) = p_f7
              ANORM(18,pdx_cl) = p_f8
              ANORM(19,pdx_cl) = p_f9
              ANORM(20,pdx_cl) = p_f10
            ENDIF
            IF (pdxn .EQ. 4) THEN
              READ (N5,*,ERR=888)CC,p_f1,p_f2,p_f3,p_f4,p_f5,p_f6
              ANORM(21,pdx_cl) = p_f1
              ANORM(22,pdx_cl) = p_f2
              ANORM(23,pdx_cl) = p_f3
              ANORM(24,pdx_cl) = p_f4
              ANORM(25,pdx_cl) = p_f5
              ANORM(26,pdx_cl) = p_f6
            ENDIF
C
C     READ IN THE HYDRAULIC RADIUS CARDS
C
            IF (pdxn .EQ. 5) THEN
              READ (N5,*,ERR=888)CC,p_f1,p_f2,p_f3,p_f4,p_f5,p_f6,p_f7,
     +                              p_f8,p_f9,p_f10
              HRNORM(1,pdx_cl) = p_f1
              HRNORM(2,pdx_cl) = p_f2
              HRNORM(3,pdx_cl) = p_f3
              HRNORM(4,pdx_cl) = p_f4
              HRNORM(5,pdx_cl) = p_f5
              HRNORM(6,pdx_cl) = p_f6
              HRNORM(7,pdx_cl) = p_f7
              HRNORM(8,pdx_cl) = p_f8
              HRNORM(9,pdx_cl) = p_f9
              HRNORM(10,pdx_cl) = p_f10
            ENDIF
            IF (pdxn .EQ. 6) THEN
              READ (N5,*,ERR=888)CC,p_f1,p_f2,p_f3,p_f4,p_f5,p_f6,p_f7,
     +                              p_f8,p_f9,p_f10
              HRNORM(11,pdx_cl) = p_f1
              HRNORM(12,pdx_cl) = p_f2
              HRNORM(13,pdx_cl) = p_f3
              HRNORM(14,pdx_cl) = p_f4
              HRNORM(15,pdx_cl) = p_f5
              HRNORM(16,pdx_cl) = p_f6
              HRNORM(17,pdx_cl) = p_f7
              HRNORM(18,pdx_cl) = p_f8
              HRNORM(19,pdx_cl) = p_f9
              HRNORM(20,pdx_cl) = p_f10
            ENDIF
            IF (pdxn .EQ. 7) THEN
              READ (N5,*,ERR=888)CC,p_f1,p_f2,p_f3,p_f4,p_f5,p_f6
              HRNORM(21,pdx_cl) = p_f1
              HRNORM(22,pdx_cl) = p_f2
              HRNORM(23,pdx_cl) = p_f3
              HRNORM(24,pdx_cl) = p_f4
              HRNORM(25,pdx_cl) = p_f5
              HRNORM(26,pdx_cl) = p_f6
            ENDIF
C
C     READ IN THE WIDTH CARDS
C
            IF (pdxn .EQ. 8) THEN
              READ (N5,*,ERR=888)CC,p_f1,p_f2,p_f3,p_f4,p_f5,p_f6,p_f7,
     +                              p_f8,p_f9,p_f10
              TWNORM(1,pdx_cl) = p_f1
              TWNORM(2,pdx_cl) = p_f2
              TWNORM(3,pdx_cl) = p_f3
              TWNORM(4,pdx_cl) = p_f4
              TWNORM(5,pdx_cl) = p_f5
              TWNORM(6,pdx_cl) = p_f6
              TWNORM(7,pdx_cl) = p_f7
              TWNORM(8,pdx_cl) = p_f8
              TWNORM(9,pdx_cl) = p_f9
              TWNORM(10,pdx_cl) = p_f10
            ENDIF
            IF (pdxn .EQ. 9) THEN
              READ (N5,*,ERR=888)CC,p_f1,p_f2,p_f3,p_f4,p_f5,p_f6,p_f7,
     +                              p_f8,p_f9,p_f10
              TWNORM(11,pdx_cl) = p_f1
              TWNORM(12,pdx_cl) = p_f2
              TWNORM(13,pdx_cl) = p_f3
              TWNORM(14,pdx_cl) = p_f4
              TWNORM(15,pdx_cl) = p_f5
              TWNORM(16,pdx_cl) = p_f6
              TWNORM(17,pdx_cl) = p_f7
              TWNORM(18,pdx_cl) = p_f8
              TWNORM(19,pdx_cl) = p_f9
              TWNORM(20,pdx_cl) = p_f10
            ENDIF
            IF (pdxn .EQ. 10) THEN
              READ (N5,*,ERR=888)CC,p_f1,p_f2,p_f3,p_f4,p_f5,p_f6
              TWNORM(21,pdx_cl) = p_f1
              TWNORM(22,pdx_cl) = p_f2
              TWNORM(23,pdx_cl) = p_f3
              TWNORM(24,pdx_cl) = p_f4
              TWNORM(25,pdx_cl) = p_f5
              TWNORM(26,pdx_cl) = p_f6
            ENDIF
C
C     FINISHED READING IN SECTION DESCRIPTION CARDS
C
  265     CONTINUE
C     END OF SECTION DATA READING ROUTINE
        ENDIF

C
C     CALCULATE RFULL AFULL AND WIDE AS REQUIRED
C     EITHER FROM SPHI AND STHETA IF NON ZERO
C     (THE CUSTOMIZED PIPE OPTION)
C     OR FROM VALUES ON THE SECTION CARD
C
        IF (STHETA(N) .GT. 0.0) THEN
          RFULL(N) = STHETA(N) * DEEP(N)
        ELSE
          RFULL(N) = pdx_cr(pdxar) * DEEP(N)
        ENDIF
        IF (AFULL(N) .LE. 0.0) THEN
          IF (SPHI(N) .GT. 0.0) THEN
            AFULL(N) = SPHI(N) * DEEP(N) * DEEP(N)
          ELSE
            AFULL(N) = pdx_ca(pdxar) * DEEP(N) * DEEP(N)
          ENDIF
        ENDIF
        IF (WIDE(N) .LE. 0.0) THEN
          WIDE(N) = pdx_cw(pdxar) * DEEP(N)
        ENDIF
C     END OF NON-STANDARD SECTION ROUTINE
      ENDIF
C
CPDX--------------------------------------------------------------------

  260 CONTINUE
  280 NC  = N-1
      NTC = NC

CPDX--BC, 10/15/91------------------------------------------------------
C
C     TRAP OUT OF SEQUENCE CP CARD AND DECLARE ERROR
C
C AMM 1/13/97 Change the following error message if you change the number
C             of custom shapes (NUM_CP)
      IF (CC .EQ. 'CP') THEN
        WRITE(*,6010)'===> ERROR !!! CP Card Not Properly located'
        WRITE(*,6010)'              If pipe types 13-15 or 21-30'
        WRITE(*,6010)'              are used the first C1 card'
        WRITE(*,6010)'              of that type must be'
        WRITE(*,6010)'              followed by ten CP cards'
        WRITE(*,6010)'              describing properties.'
        WRITE(*,6010)'              Use of the CP card'
        WRITE(*,6010)'              anywhere else is illegal.'
              IF(JCE.EQ.0)WRITE(*,'(A,I10)')' CONDUIT ',NCOND(N)
              IF(JCE.EQ.1)WRITE(*,'(A,A10)')' CONDUIT ',ACOND(N)
        STOP
      ENDIF
C AMM 12/23/96 Changes per VCA
C              Section checking 3-5 for 13-15 removed since
C              simultaneous usage of those types are now allowed
C
C     TRAP USE OF: (3 AND 13) OR (4 AND 14) OR (5 AND 15)
C     WHICH WILL RESULT IN OVERWRITTEN STANDARD SECTION
C     BY CORRESPONDING SPECIAL SECTION
C
C      IF (((pdx_3 .EQ. -1) .AND. (pdx_13 .EQ. -1)) .OR.
C     +  ((pdx_4 .EQ. -1) .AND. (pdx_14 .EQ. -1)) .OR.
C     +  ((pdx_5 .EQ. -1) .AND. (pdx_15 .EQ. -1))) THEN
C        WRITE(*,6010)'===> ERROR !!! Multiple use of same section'
C        WRITE(*,6010)'              Special sections 13 14 and 15'
C        WRITE(*,6010)'              are loaded into 3 4 and 5'
C        WRITE(*,6010)'              respectively.  If both standard'
C        WRITE(*,6010)'              and the special sections are'
C        WRITE(*,6010)'              used the standard section data'
C        WRITE(*,6010)'              is lost.  Hence data is invalid'
C        WRITE(*,6010)'              for standard pipes 3 4 or 5.'
C              IF(JCE.EQ.0)WRITE(*,'(A,I10)')' CONDUIT ',NCOND(N)
C              IF(JCE.EQ.1)WRITE(*,'(A,A10)')' CONDUIT ',ACOND(N)
C        STOP
C      ENDIF
CPDX--------------------------------------------------------------------

C=======================================================================
C     PRINT OUT INFORMATION ON FLAP GATES (IF ANY EXIST)
C=======================================================================
      NFLAP     = 0
      DO 9000 N = 1,NC
      IF(INGATE(N).GT.0) THEN
                         NFLAP = NFLAP + 1
                         IF(NFLAP.EQ.1) WRITE(N6,9010)
                         ND = INGATE(N)
                         IF(JCE.EQ.0) WRITE(N6,9020) NCOND(N),GTYPE(ND)
                         IF(JCE.EQ.1) WRITE(N6,9021) ACOND(N),GTYPE(ND)
                         ENDIF
 9000 CONTINUE      
C=======================================================================
C     READ THE CS DATA GROUP TO DETERMINE IF THE NATURAL CROSS
C          SECTION INFORMATION WILL BE SAVED ON NSCRAT(4) OR
C          READ FROM NSCRAT(4) BY THE PROGRAM
C=======================================================================
      READ(N5,*,ERR=888) CC
      IF(CC.EQ.'CS') THEN
                     BACKSPACE N5
                     READ(N5,*,ERR=888) CC,IREAD
                     ELSE
                     BACKSPACE N5
                     ENDIF
C=======================================================================
C     NOW GET DATA FOR IRREGULAR CHANNELS FROM THE SCRATCH FILE. 
C=======================================================================
      IF(IREAD.EQ.'READ') THEN
               IF(NCSAVE.EQ.0) THEN
                               WRITE(N6,8999)
                               STOP
                               ENDIF
               READ(NCSAVE,ERR=8888) NATUR,NC
               READ(NCSAVE,ERR=8888) (NQC(N),N=1,NC)
               READ(NCSAVE,ERR=8888) (NUMQ(N),N=1,NC)
               READ(NCSAVE,ERR=8888) ((QCURVE(N,1,J),J=1,26),N=1,NATUR)
               READ(NCSAVE,ERR=8888) ((QCURVE(N,2,J),J=1,26),N=1,NATUR)
               READ(NCSAVE,ERR=8888) ((QCURVE(N,3,J),J=1,26),N=1,NATUR)
               DO 275 N = 1,NC
               IF(NKLASS(N).LT.7) GO TO 275
               READ(NCSAVE,ERR=8888) SLOPE,AFULL(N),DEEP(N),WIDE(N),
     +                               LEN(N),ROUGH(N),RFULL(N),NKLASS(N)
  275          CONTINUE               
               ENDIF
C=======================================================================
C     NOW GET DATA FOR IRREGULAR CHANNELS, IF ANY. 
C=======================================================================
      IF(IREAD.EQ.'SAVE'.OR.IREAD.EQ.'CARD') THEN
      DO 285 N = 1,NC
      IF(NKLASS(N).EQ.8) THEN
            SLOPE = SPHI(N)
            NATUR = NATUR + 1
            KCOND = 0
            KSTOP = 0
            IF(STHETA(N).LT.0.0) THEN
                                 KSTOP     = 1
                                 STHETA(N) = ABS(STHETA(N))
                                 ENDIF
            CALL GETCUR(N,STHETA(N),SLOPE,METRIC,0,KCOND,AFULL(N),
     +                  DEEP(N),WIDE(N),LEN(N),ROUGH(N),RFULL(N),
     +                  NSTOP,NCOND(N),ACOND(N),KSTOP)
            ENDIF
C=======================================================================
C     CALCULATE DATA FOR POWER FUNCTION CROSS SECTIONS
C=======================================================================
      IF(NKLASS(N).EQ.7) THEN
            KCOND    = 1
            NATUR    = NATUR + 1
            SLOPE    = 0.0
            KSTOP    = 0
            IF(STHETA(N).EQ.0.0) STHETA(N) = 2.0
            IF(STHETA(N).LT.0.0) THEN
                                 KSTOP     = 1
                                 STHETA(N) = ABS(STHETA(N))
                                 ENDIF
            CALL GETCUR(N,STHETA(N),SLOPE,METRIC,0,KCOND,AFULL(N),
     +                  DEEP(N),WIDE(N),LEN(N),ROUGH(N),RFULL(N),
     +                  NSTOP,NCOND(N),ACOND(N),KSTOP)
            NKLASS(N) = 8
            ENDIF
  285 CONTINUE
      ENDIF
C=======================================================================
C     NOW SAVE THE DATA FOR IRREGULAR CHANNELS ON THE SCRATCH FILE. 
C=======================================================================
      IF(IREAD.EQ.'SAVE') THEN
               IF(NCSAVE.EQ.0) THEN
                               WRITE(N6,8999)
                               STOP
                               ENDIF
               WRITE(NCSAVE,ERR=8888) NATUR,NC
               WRITE(NCSAVE,ERR=8888) (NQC(N),N=1,NC)
               WRITE(NCSAVE,ERR=8888) (NUMQ(N),N=1,NC)
               WRITE(NCSAVE,ERR=8888) ((QCURVE(N,1,J),J=1,26),N=1,NATUR)
               WRITE(NCSAVE,ERR=8888) ((QCURVE(N,2,J),J=1,26),N=1,NATUR)
               WRITE(NCSAVE,ERR=8888) ((QCURVE(N,3,J),J=1,26),N=1,NATUR)
               DO 295 N = 1,NC
               IF(NKLASS(N).LT.7) GO TO 295
               WRITE(NCSAVE,ERR=8888) SLOPE,AFULL(N),DEEP(N),WIDE(N),
     +                                LEN(N),ROUGH(N),RFULL(N),NKLASS(N)
  295          CONTINUE               
               ENDIF
      call INDAT12P
      RETURN
 888  continue
      CALL IERROR
 8888 WRITE(N6,8887)
      STOP
 5100 FORMAT(/,' Control information for simulation',/,
     +       ' ----------------------------------',//,
     +       ' Integration cycles.................',I8,/)
 5120 FORMAT(' Length of integration step is......',F8.2,
     +       ' seconds',/,
     +       ' Simulation length..................',F8.2,' hours',/)
 5121 FORMAT(' Do not create equiv. pipes(NEQUAL).',I8,/)
 5122 FORMAT(' Create equivalent conduits based ',/,
     +       ' on the COURANT condition...........',I8,/)
 5123 FORMAT(' Use U.S. customary units for I/O...',I8,/)
 5124 FORMAT(' Use metric units for I/O...........',I8,/)
 5140 FORMAT(' Printing starts in cycle...........',I8,//,
     +       ' Intermediate printout intervals of.',I8,' cycles',/,
     +       ' Intermediate printout intervals of.',F8.2,' minutes',//,
     +       ' Summary printout intervals of......',I8,' cycles',/,
     +       ' Summary printout time interval of..',F8.2,' minutes',//,
     +       ' Hot start file parameter (REDO)....',I8,/)
 5160 FORMAT(' Initial time.......................',F8.2,' hours')
 5170 FORMAT(/,' Iteration variables: ITMAX.........',I8,/,
     1         '                      SURTOL........',F8.4,/)
 5175 FORMAT(' Default surface area of junctions....',F8.2,
     +       ' square feet.',/)
 5176 FORMAT(' Default surface area of junctions..',F6.2,
     +       ' square meters.',/)
 5177 FORMAT(' EXTRAN VERSION 3.3 SOLUTION. (ISOL = 0).',/,
     +       ' Sum of Junction flow is zero during surcharge.',/)
 5178 FORMAT(' SEMI-IMPLICIT EXTRAN SOLUTION. (ISOL = 1).',/,
     +       ' NEWTON-RAPHSON SURCHARGE ITERATION.',/)
 5179 FORMAT(' ITERATIVE EXTRAN SOLUTION. (ISOL = 2).',/,
     +       ' NEWTON-RAPHSON SURCHARGE ITERATION.',/)
 5180 FORMAT(' NJSW INPUT HYDROGRAPH JUNCTIONS....',I6)
 5183 FORMAT(' NORMAL FLOW OPTION WHEN THE WATER  ',/,
     +       ' SURFACE SLOPE IS LESS THAN THE     ',/,
     +       ' GROUND SURFACE SLOPE (KSUPER=0)....',/)
 5184 FORMAT(' NORMAL FLOW OPTION WHEN THE FROUDE ',/,
     +       ' # IS GREATER THAN 1.0 (KSUPER=1)...',/)
 5185 FORMAT(/,' Note: ZU and ZD are elevations not offsets on C1 data l
     +ines.  You are using the BB optional data group.',/)
 5200 FORMAT(/,' Printed output for the following',I3,
     +         ' Junctions',//,(5X,10I10))
 5201 FORMAT(/,' Printed output for the following',I3,
     +         ' Junctions',//,(5X,10A10))
 5220 FORMAT(/,' Printed output for the following',I3,
     +         ' Conduits',//,(5X,10I10))
 5221 FORMAT(/,' Printed output for the following',I3,
     +         ' Conduits',//,(5X,10A10))
 5240 FORMAT (/,' Water surface elevations will be plotted',
     +    ' for the following ',I3,' Junctions',//,(5X,10I10))
 5241 FORMAT (/,' Water surface elevations will be plotted',
     +    ' for the following ',I3,' Junctions',//,(5X,10A10))
 5260 FORMAT(/,' Flow rate will be plotted for the following ',
     +             I3,' Conduits',//,(5X,10I10))
 5261 FORMAT(/,' Flow rate will be plotted for the following ',
     +             I3,' Conduits',//,(5X,10A10))
 5265 FORMAT(/,' THE WATER SURFACE SLOPE WILL BE PLOTTED FOR THE ', 
     +         'FOLLOWING ',I3,' CONDUITS',//,(5X,10I10))
 5266 FORMAT(/,' THE WATER SURFACE SLOPE WILL BE PLOTTED FOR THE ',
     +         'FOLLOWING ',I3,' CONDUITS',//,(5X,10A10))
 6000 FORMAT(/,' Reading Conduit data.')
CPDX--BC, 10/15/91------------------------------------------------------
 6010 FORMAT(' ',A)
CPDX--------------------------------------------------------------------
 6050 FORMAT(/,' Reading Junction data.')
 6100 FORMAT(/,' Reading remaining simulation data.')
 6200 FORMAT(/,' ===> WARNING !!! The upstream and downstream junctions 
     +for the following conduits',/,
     + '                 have been reversed to correspond to the positiv
     +e flow and decreasing',/
     +,'                 slope EXTRAN convention.  A negative flow in th 
     +e output thus means ',/, 
     + '                 the flow was from your original upstream juncti
     +on to your original',/,
     + '                 downstream junction.  Any initial flow has been
     + multiplied by -1.',/)
 6205 FORMAT(13X,I5,'.  Conduit #...',I10,'  has been changed.')
 6206 FORMAT(13X,I5,'.  Conduit #...',A10,'  has been changed.')
 8200 FORMAT(' ===> ERROR !!! THE GROUND ELEVATION OF JUNCTION ',I9,
     *           ' LIES ABOVE THE JUNCTION INVERT ELEVATION.')
 8201 FORMAT(' ===> ERROR !!! THE GROUND ELEVATION OF JUNCTION ',A10,
     *           ' LIES ABOVE THE JUNCTION INVERT ELEVATION.')
 8887 FORMAT(/,' ===> ERROR !!!  READING OR WRITING NATURAL CHANNEL',
     +         ' INFORMATION ON NSCRAT(4).')
 8999 FORMAT(/,' ===> ERROR !!!  THE NATURAL CHANNEL INFORMATION WAS',
     +         ' NOT PRESENT ON THE NSCRAT(4) FILE.')
 9010 FORMAT(/,
     +' ************************************************************',/,
     +' *               Flap Gate Conduit Information              *',/,
     +' *               -----------------------------              *',/,
     +' * POSITIVE FLAP GATE - FLOW ONLY ALLOWED FROM THE UPSTREAM *',/,
     +' *                      TO THE DOWNSTREAM JUNCTION          *',/,                   
     +' * NEGATIVE FLAP GATE - FLOW ONLY ALLOWED FROM THE          *',/,
     +' *                      DOWNSTREAM TO THE UPSTREAM JUNCTION *',/,                   
     +' ************************************************************',//
     +,'    Conduit   Type of Flap Gate',/,
     + '    -------   -----------------')
 9020 FORMAT(1X,I10,A20)
 9021 FORMAT(1X,A10,A20)
 9030 FORMAT(/)
C
 
 2999 FORMAT(/,
     1       '1',40(2H--)/' ','ENVIRONMENTAL PROTECTION AGENCY',13X,40H*
     2***   EXTENDED TRANSPORT PROGRAM   ****,8X,'WATER RESOURCES DIVISI
     3ON',/,' ','WASHINGTON, D.C.            ',16X,4H****,32X,4H****,8X,
     4'CAMP DRESSER & MCKEE INC.',/,' ','                ',28X,4H****,
     56X,'   ANALYSIS MODULE  ',6X,4H****,8X,'ANNANDALE, VIRGINIA')
 5060 FORMAT(/,5X,A80,/,5X,A80,/)
      END