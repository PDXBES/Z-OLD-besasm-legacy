      SUBROUTINE INDAT12P
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
C AMM 1/13/97 Removed following since they aren't used in this module
C      INTEGER pdx_3,pdx_4,pdx_5,pdx_13,pdx_14,pdx_15 
C      INTEGER pdx_16,pdx_17,pdx_18,pdx_19,pdx_20
C      INTEGER p_i1,pdx_cl
C      INTEGER pdxn
      INTEGER iCCTEST

C AMM 1/13/97 Removed following since they aren't used in this module
C      REAL    p_f1,p_f2,p_f3,p_f4,p_f5
C      REAL    p_f6,p_f7,p_f8,p_f9,p_f10
C      REAL    pdx_ca,pdx_cr,pdx_cw

C AMM 12/23/96 Changes per VCA
C              area, hydraulic radius, and width array sizes use constant
C              NUM_CP instead of hard-coded 3
C      DIMENSION pdx_ca(NUM_CP),pdx_cr(NUM_CP),pdx_cw(NUM_CP)
      CHARACTER ANS*1
      CHARACTER CCTEST*1
CPDX--------------------------------------------------------------------
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
C=======================================================================
C                  E X E C U T I O N
C=======================================================================
C=======================================================================
C     PRINT CONDUIT DATA
C=======================================================================
      DO 300 N = 1,NC
      KLASS    = NKLASS(N)
c On first conduit and every 50th conduit, flip to a new page
      IF(N.EQ.1.OR.MOD(N,50).EQ.0) THEN
			     WRITE(N6,2999)
			     WRITE(N6,5060) ALPHA1,ALPHA2
			     IF(METRIC.EQ.1) WRITE(N6,5300)
			     IF(METRIC.EQ.2) WRITE(N6,5301)
			     ENDIF
      IF(JCE.EQ.0) THEN
C Change output depending on whether we have offsets or not, and if we
C have a trapezoidal channel (NKLASS = 6), what the side slopes are
      IF(ZU(N).EQ.0.0.AND.ZD(N).EQ.0.0) THEN
	IF(NKLASS(N).EQ.6) WRITE(N6,5320) N,NCOND(N),LEN(N),
     +                     CTYPE(KLASS),AFULL(N),ROUGH(N),WIDE(N),
     +                     DEEP(N),(NJUNC(N,K),K=1,2),STHETA(N),SPHI(N)
	IF(NKLASS(N).NE.6) WRITE(N6,5321) N,NCOND(N),LEN(N),
     +                     CTYPE(KLASS),AFULL(N),ROUGH(N),WIDE(N),
     +                     DEEP(N),(NJUNC(N,K),K=1,2)
		 ELSE
	IF(NKLASS(N).EQ.6) WRITE(N6,5322) N,NCOND(N),LEN(N),
     +                     CTYPE(KLASS),AFULL(N),ROUGH(N),WIDE(N),
     +                     DEEP(N),(NJUNC(N,K),K=1,2),ZU(N),ZD(N),
     +                     STHETA(N),SPHI(N)
	IF(NKLASS(N).NE.6) WRITE(N6,5323) N,NCOND(N),LEN(N),
     +                     CTYPE(KLASS),AFULL(N),ROUGH(N),WIDE(N),
     +                     DEEP(N),(NJUNC(N,K),K=1,2),ZU(N),ZD(N)
		ENDIF
      ENDIF
      IF(JCE.EQ.1) THEN
      IF(ZU(N).EQ.0.0.AND.ZD(N).EQ.0.0) THEN
	IF(NKLASS(N).EQ.6) WRITE(N6,5420) N,ACOND(N),LEN(N),
     +                     CTYPE(KLASS),AFULL(N),ROUGH(N),WIDE(N),
     +                     DEEP(N),(KJUNC(N,K),K=1,2),STHETA(N),SPHI(N)
	IF(NKLASS(N).NE.6) WRITE(N6,5421) N,ACOND(N),LEN(N),
     +                     CTYPE(KLASS),AFULL(N),ROUGH(N),WIDE(N),
     +                     DEEP(N),(KJUNC(N,K),K=1,2)
		 ELSE
	IF(NKLASS(N).EQ.6) WRITE(N6,5422) N,ACOND(N),LEN(N),
     +                     CTYPE(KLASS),AFULL(N),ROUGH(N),WIDE(N),
     +                     DEEP(N),(KJUNC(N,K),K=1,2),ZU(N),ZD(N),
     +                     STHETA(N),SPHI(N)
	IF(NKLASS(N).NE.6) WRITE(N6,5423) N,ACOND(N),LEN(N),
     +                     CTYPE(KLASS),AFULL(N),ROUGH(N),WIDE(N),
     +                     DEEP(N),(KJUNC(N,K),K=1,2),ZU(N),ZD(N)
		ENDIF
      ENDIF
  300 CONTINUE
C=======================================================================
C     CHECK FOR VIOLATION OF WAVE TRAVEL/CONDUIT LENGTH RATIO
C=======================================================================
      WRITE(N6,9030)
      VOLOLD   = 0.0
      VOLNEW   = 0.0
      DO 320 N = 1,NC
      VOLOLD   = VOLOLD + AFULL(N)*LEN(N)
C AMM 12/23/96 Changes per VCA
C              Change range of pipe class from GT 5 to GT 5 AND LT 13
      IF((NKLASS(N).GT.5).AND.(NKLASS(N).LT.13)) THEN
			 CALL HYDRAD(N,NKLASS(N),DEEP(N),RMID,AMID,BMID)
			 AB = AMID/BMID
			 IF(AB.LE.0.0) AB = 0.01
			 ELSE
			 AB = DEEP(N)
			 ENDIF
      RATIO    = SQRT(AB*GRVT)*DELT/LEN(N)
      IF(RATIO.GT.1.0.AND.JCE.EQ.0) WRITE(N6,5335) NCOND(N),RATIO
      IF(RATIO.GT.1.0.AND.JCE.EQ.1) WRITE(N6,5336) ACOND(N),RATIO
      RATIO    = SQRT(AB*GRVT)*FLOAT(NEQUAL)/LEN(N)
      IF(RATIO.GT.1.0.AND.NEQUAL.GE.1) THEN
		      CLEN     = FLOAT(NEQUAL)*SQRT(AB*GRVT)
		      ROUGH(N) = ROUGH(N)*SQRT(LEN(N)/CLEN)
		      LEN(N)   = CLEN
		      RATIO    = SQRT(AB*GRVT)*DELT/LEN(N)
		      IF(JCE.EQ.0) WRITE(N6,5337) NCOND(N),CLEN,
     +                                            ROUGH(N),RATIO
		      IF(JCE.EQ.1) WRITE(N6,5338) ACOND(N),CLEN,
     +                                            ROUGH(N),RATIO
		      VOLNEW   = VOLNEW + AFULL(N)*LEN(N)
		      ELSE
		      VOLNEW   = VOLNEW + AFULL(N)*LEN(N)
		      ENDIF
  320 CONTINUE
      IF(NEQUAL.GE.1.AND.METRIC.EQ.1) WRITE(N6,5339)
     +                                VOLOLD,VOLNEW,VOLNEW/VOLOLD
      IF(NEQUAL.GE.1.AND.METRIC.EQ.2) WRITE(N6,5340) 
     +                                VOLOLD,VOLNEW,VOLNEW/VOLOLD
      IF(NEQUAL.EQ.0.AND.METRIC.EQ.1) WRITE(N6,5341)  VOLOLD
      IF(NEQUAL.EQ.0.AND.METRIC.EQ.2) WRITE(N6,5342)  VOLOLD
C=======================================================================
C     READ JUNCTION DATA ON DATA GROUP D1
C=======================================================================
      WRITE(*,6050)
      DO 380 J = 1,NEE
      READ(N5,*,ERR=888) CC
      BACKSPACE N5
CPDX--SM, 6/2/92--------------------------------------------------------
      IF(CC.EQ.'C1')THEN
	  WRITE(N6,'(/A)')' ===> ERROR !!! NUMBER OF CONDUITS EXCEED ALLOW
     +ABLE'
	  STOP
	ENDIF
CPDX--------------------------------------------------------------------
      IF(CC.NE.'D1') GO TO 400
CPDX  VCA 6/23/95:  Add in Land Use DWF Factor -----------------
CPDX  --- Check if LUDWF(J) is on the D1 card

*      Open (1029, file='test.dat')
CPDX  SWB   Create Test File for Debug/Verification
      IF(JCE.EQ.0) READ(N5,*,ERR=888) CC,JUN(J),GRELEV(J),
     +                                Z(J),QINST(J),Y(J),CCTEST
      IF(JCE.EQ.1) READ(N5,*,ERR=888) CC,AJUN(J),GRELEV(J),
     +                                Z(J),QINST(J),Y(J),CCTEST
CPDX  SWB  Change cctest To its ASCII character code
      iCCTEST = ICHAR(CCTEST)
      SELECT CASE (iCCTEST)
	CASE (45:46,48:57)
CPDX      SWB cctest was a number (or decimal point or minus sign).  Re-read
CPDX          variable as a REAL number   
	  BACKSPACE N5

	  IF(JCE.EQ.0) READ(N5,*,ERR=888) CC,JUN(J),GRELEV(J),
     +                                Z(J),QINST(J),Y(J),DWFTEST
	  IF(JCE.EQ.1) READ(N5,*,ERR=888) CC,AJUN(J),GRELEV(J),
     +                                Z(J),QINST(J),Y(J),DWFTEST
CPDX      SWB Write Test File to see if data is being read correctly
*          NLINE = 174
*          WRITE(1029,*) CC,JUN(J),GRELEV(J),
*     +                  Z(J),QINST(J),Y(J),DWFTEST,NLINE

	  NDWFINT = INT(DWFTEST)

CPDX      SWB Check to see if dwftest was an INTEGER or a REAL number
	  IF((NDWFINT.LE.21).AND.(DWFTEST.EQ.NDWFINT))THEN
CPDX        KEEP DIURNAL LAND USE VALUE
	    LUDWF(J) = DWFTEST
	    IF(J.EQ.1) WRITE(N6,5355)
	    IF(JCE.EQ.0) WRITE(N6,5358) J,JUN(J),LUDWF(J),QINST(J)
	    IF(JCE.EQ.1) WRITE(N6,5359) J,AJUN(J),LUDWF(J),QINST(J)
CPDX        Zero Out DWFTEST
	    DWFTEST = 0

	  ELSE
	    LUDWF(J) = -1
	    DWFTEST = 0
	  ENDIF
CPDX      Skip over the standard D1 card reading

	CASE DEFAULT
CPDX      Line is Standard D1 Card
CPCX      Now re-read standard D1 card ignoring the columns past Y(J)
	  BACKSPACE N5
	  BACKSPACE N5        
	  IF(JCE.EQ.0) READ(N5,*,ERR=888) CC,JUN(J),GRELEV(J),
     +                                Z(J),QINST(J),Y(J)
	  IF(JCE.EQ.1) READ(N5,*,ERR=888) CC,AJUN(J),GRELEV(J),
     +                                Z(J),QINST(J),Y(J)             
C AMM 1/6/97 Added following code to prevent warning message that user
C            is specifying a land use diurnal curve without corresponding
C            D3 cards even though no land use diurnal curve was specified.
        LUDWF(J) = -1     
CPDX      SWB  Write Test File to see if data is being read correctly
*          NLINE = 207
*          WRITE(1029,*) CC,JUN(J),GRELEV(J),
*     +                  Z(J),QINST(J),Y(J),NLINE

      END SELECT
      ZCROWN(J) = Z(J)
CPDX  -----------------------------------------------------------
C=======================================================================
C     Set up junction connectivity array from pipe data.
C=======================================================================
      LOC      = 0
      IF(NC.GT.0) THEN
      DO 360 N = 1,NC
      DO 360 K = 1,2
      IF(JCE.EQ.0.AND.NJUNC(N,K).EQ.JUN(J)) THEN
					    LOC          = LOC+1
					    NCHAN(J,LOC) = N
CPDX--ERROR TRAP--------------------------------------------------------
					    IF(LOC.GT.8) THEN
					      WRITE(N6,5000)JUN(J)
					      NSTOP=NSTOP+1
					      ENDIF
5000  FORMAT(/' ===> ERROR !!! INFLOWS EXCEED 8 AT JUNCTION ',I10)
CPDX--------------------------------------------------------------------
					    ENDIF
      IF(JCE.EQ.1.AND.KJUNC(N,K).EQ.AJUN(J))THEN
					    LOC          = LOC+1
					    NCHAN(J,LOC) = N
CPDX--ERROR TRAP--------------------------------------------------------
					    IF(LOC.GT.8) THEN
					      WRITE(N6,5001)AJUN(J)
					      NSTOP=NSTOP+1
					      ENDIF
5001  FORMAT(/' ===> ERROR !!! INFLOWS EXCEED 8 AT JUNCTION ',A10)
CPDX--------------------------------------------------------------------
					    ENDIF
  360 CONTINUE
      IF(LOC.EQ.0) THEN
		   IF(JCE.EQ.0) WRITE(N6,5350)  JUN(J)
		   IF(JCE.EQ.1) WRITE(N6,5351) AJUN(J)
		   JSKIP(J) = 1
		   ENDIF
      ELSE
C=======================================================================
C     ZCROWN IS EQUAL TO THE GROUND ELEVATION FOR JUNCTIONS WITH
C            NO CONNECTING CONDUITS.
C=======================================================================
      ZCROWN(J) = GRELEV(J) - Z(J)
      IF(ZCROWN(J).LE.0.0) THEN
			   IF(JCE.EQ.0) WRITE(N6,8200)  JUN(J)
			   IF(JCE.EQ.1) WRITE(N6,8201) AJUN(J)
			   NSTOP = NSTOP + 1
			   ENDIF
      ENDIF
  380 CONTINUE
  400 NJ = J-1
C=======================================================================
C     CONVERT CONDUIT CONNECTIVITY NUMBERS TO INTERNAL SYSTEM
C     ASSIGN POSITIVE DOWNSTREAM FLOW CONVENTION
C=======================================================================
      IMOVE    = 0
      IF(NC.GT.0) THEN
      DO 600 N = 1,NC
      DO 540 K = 1,2
      DO 500 J = 1,NJ
      IF(JCE.EQ.0.AND.NJUNC(N,K).EQ.JUN(J))  GO TO 520
      IF(JCE.EQ.1.AND.KJUNC(N,K).EQ.AJUN(J)) GO TO 520
  500 CONTINUE
      IF(JCE.EQ.0) WRITE(N6,5390) NJUNC(N,K),NCOND(N)
      IF(JCE.EQ.1) WRITE(N6,5391) KJUNC(N,K),ACOND(N)
      NSTOP      = NSTOP + 1
  520 NJUNC(N,K) = J
  540 CONTINUE
      NL    = NJUNC(N,1)
      NH    = NJUNC(N,2)
      IF(JELEV.EQ.0) THEN
		     ZU(N) = Z(NL) + ZU(N)
		     ZD(N) = Z(NH) + ZD(N)
		     ELSE
		     IF(ZU(N).EQ.0.0) ZU(N) = Z(NL) + ZU(N)
		     IF(ZD(N).EQ.0.0) ZD(N) = Z(NH) + ZD(N)
		     ENDIF
      IF(ZU(N)-ZD(N).LT.0.0) THEN 
			     IMOVE      = IMOVE + 1
			     IF(JCE.EQ.1) THEN
					  KDUM(1)    = KJUNC(N,1)
					  KJUNC(N,1) = KJUNC(N,2) 
					  KJUNC(N,2) = KDUM(1)
					  ENDIF 
			     TEMP       = ZU(N)
			     ZU(N)      = ZD(N)
			     ZD(N)      = TEMP
			     QO(N)      = -QO(N)
			     NJUNC(N,1) = NH
			     NJUNC(N,2) = NL
			     NL         = NJUNC(N,1)
			     NH         = NJUNC(N,2)
			     IF(IMOVE.EQ.1) WRITE(N6,6200) 
			     IF(JCE.EQ.0) WRITE(N6,6205) IMOVE,NCOND(N)
			     IF(JCE.EQ.1) WRITE(N6,6206) IMOVE,ACOND(N)
			     ENDIF
      IF((ZU(N) + DEEP(N)).GT.ZCROWN(NL))  ZCROWN(NL) = ZU(N)+DEEP(N)
      IF((ZD(N) + DEEP(N)).GT.ZCROWN(NH))  ZCROWN(NH) = ZD(N)+DEEP(N)
      IF(ZCROWN(NL).GT.GRELEV(NL)+0.001) THEN
		    IF(JCE.EQ.0) WRITE(N6,5395) NCOND(N),JUN(NL)
		    IF(JCE.EQ.1) WRITE(N6,5396) ACOND(N),AJUN(NL)
		    ZCROWN(NL) = GRELEV(NL) - 0.01
		    NSTOP      = NSTOP + 1
		    ENDIF
      IF(ZCROWN(NH).GT.GRELEV(NH)+0.001) THEN
		    IF(JCE.EQ.0) WRITE(N6,5395) NCOND(N),JUN(NH)
		    IF(JCE.EQ.1) WRITE(N6,5396) ACOND(N),AJUN(NH)
		    ZCROWN(NH) = GRELEV(NH) - 0.01
		    NSTOP      = NSTOP + 1
		    ENDIF
  600 CONTINUE
      ENDIF
C=======================================================================
C     Print junction data.
C=======================================================================
      ANS='Y'
      DO 460 J = 1,NJ
      MPT      = 0
      NZP      = 0
      DO 420 I = 1,8
      K1       = NCHAN(J,I)
      IF(K1.EQ.0)            GO TO 440
      IF(JCE.EQ.0) IDUM(I) = NCOND(K1)
      IF(JCE.EQ.1) KDUM(I) = ACOND(K1)
      MPT                  = MPT + 1
C=======================================================================
C     Check for all conduits above the Junction invert.
C=======================================================================
			   JJ = 1
      IF(NJUNC(K1,1).EQ.J) JJ = 1
      IF(NJUNC(K1,2).EQ.J) JJ = 2
      IF(JJ.EQ.1.AND.ZU(K1).GT.Z(J)) NZP = NZP + 1
      IF(JJ.EQ.2.AND.ZD(K1).GT.Z(J)) NZP = NZP + 1
      IF(JJ.EQ.1.AND.ZU(K1).LT.Z(J)) NZP = NZP + 1
      IF(JJ.EQ.2.AND.ZD(K1).LT.Z(J)) NZP = NZP + 1
  420 CONTINUE
  440 CONTINUE
      IF(J.EQ.1.OR.MOD(J,50).EQ.0) THEN
				   WRITE(N6,2999)
				   WRITE(N6,5060) ALPHA1,ALPHA2
				   IF(METRIC.EQ.1) WRITE(N6,5360)
				   IF(METRIC.EQ.2) WRITE(N6,5361)
				   ENDIF
      IF(MPT.GT.0) THEN
      IF(JCE.EQ.0) WRITE(N6,5380) J,JUN(J),GRELEV(J),ZCROWN(J),
     +                            Z(J),QINST(J),Y(J),(IDUM(K),K=1,MPT)
      IF(JCE.EQ.1) WRITE(N6,5381) J,AJUN(J),GRELEV(J),ZCROWN(J),
     +                            Z(J),QINST(J),Y(J),(KDUM(K),K=1,MPT)
CPDX      IF(NZP.EQ.MPT.AND.JCE.EQ.0) WRITE(N6,5382)  JUN(J)
CPDX      IF(NZP.EQ.MPT.AND.JCE.EQ.1) WRITE(N6,5383) AJUN(J)
CPDX--------------------------------------------------------------------
	IF(NZP.EQ.MPT)THEN
	  IF(JCE.EQ.0)THEN
	     WRITE(N6,5382)  JUN(J)
	     WRITE(*,5382)   JUN(J)
	     ELSE
	      WRITE(N6,5383) AJUN(J)
	      WRITE(*,5383) AJUN(J)
	      ENDIF
CPDX VCA          IF(ANS.EQ.'Y' .OR. ANS.EQ.'y')THEN
CPDX VCA            WRITE(*,'(/A)')' CONTINUE??? (Y/N)'
CPDX VCA            READ(*,'(A)') ANS
CPDX VCA            IF(ANS.EQ.'N' .OR. ANS.EQ.'n')NSTOP=NSTOP+1
CPDX VCA            ENDIF
	ENDIF
CPDX--------------------------------------------------------------------
      ELSE
      IF(JCE.EQ.0) WRITE(N6,5380) J,JUN(J),GRELEV(J),
     +                            ZCROWN(J),Z(J),QINST(J),Y(J)
      IF(JCE.EQ.1) WRITE(N6,5381) J,AJUN(J),GRELEV(J),
     +                            ZCROWN(J),Z(J),QINST(J),Y(J)
      ENDIF
  460 CONTINUE
  480 CONTINUE
C=======================================================================
C     Check for high pipe and print a warning at the upstream end.
C=======================================================================
      WRITE(N6,9030)
      DO 495 N = 1,NC
      J        = NJUNC(N,1)
      IF(ZU(N).EQ.Z(J)) GO TO 495
      ITEST     = 0
      JTEST     = 0
      DO 490 KK = 1,8
      NKK       = NCHAN(J,KK)
      IF(NKK.EQ.N) GO TO 490
      IF(NKK.EQ.0.OR.NKK.GT.NC) GO TO 494
			    JJ = 1
      IF(NJUNC(NKK,1).EQ.J) JJ = 1
      IF(NJUNC(NKK,2).EQ.J) JJ = 2
      JTEST = JTEST + 1
      IF(JJ.EQ.1.AND.ZU(N).LE.ZU(NKK) + DEEP(NKK)) GO TO 490
      IF(JJ.EQ.2.AND.ZU(N).LE.ZD(NKK) + DEEP(NKK)) GO TO 490
      ITEST = ITEST + 1
  490 CONTINUE
  494 CONTINUE
      IF(ITEST.EQ.JTEST.AND.JCE.EQ.0) WRITE(N6,5392) NCOND(N),JUN(J)
      IF(ITEST.EQ.JTEST.AND.JCE.EQ.1) WRITE(N6,5393) ACOND(N),AJUN(J)
  495 CONTINUE
C=======================================================================
C     Check for high pipe and print a warning at the downstream end.
C=======================================================================
      DO 595 N = 1,NC
      J        = NJUNC(N,2)
      IF(ZD(N).EQ.Z(J)) GO TO 595
      ITEST     = 0
      JTEST     = 0
      DO 590 KK = 1,8
      NKK       = NCHAN(J,KK)
      IF(NKK.EQ.N) GO TO 590
      IF(NKK.EQ.0.OR.NKK.GT.NC) GO TO 594
			    JJ = 1
      IF(NJUNC(NKK,1).EQ.J) JJ = 1
      IF(NJUNC(NKK,2).EQ.J) JJ = 2
      JTEST = JTEST + 1
      IF(JJ.EQ.1.AND.ZD(N).LE.ZU(NKK) + DEEP(NKK)) GO TO 590
      IF(JJ.EQ.2.AND.ZD(N).LE.ZD(NKK) + DEEP(NKK)) GO TO 590
      ITEST = ITEST + 1
  590 CONTINUE
  594 CONTINUE
      IF(ITEST.EQ.JTEST.AND.JCE.EQ.0) WRITE(N6,5392) NCOND(N),JUN(J)
      IF(ITEST.EQ.JTEST.AND.JCE.EQ.1) WRITE(N6,5393) ACOND(N),AJUN(J)
  595 CONTINUE
C=======================================================================
      WRITE(*,6100)
      RETURN
 888  CALL IERROR

c 888  continue
 8888 WRITE(N6,8887)
      STOP
 2999 FORMAT(/,
     1       '1',40(2H--)/' ','ENVIRONMENTAL PROTECTION AGENCY',13X,40H*
     2***   EXTENDED TRANSPORT PROGRAM   ****,8X,'WATER RESOURCES DIVISI
     3ON',/,' ','WASHINGTON, D.C.            ',16X,4H****,32X,4H****,8X,
     4'CAMP DRESSER & MCKEE INC.',/,' ','                ',28X,4H****,
     56X,'   ANALYSIS MODULE  ',6X,4H****,8X,'ANNANDALE, VIRGINIA')
 5060 FORMAT(/,5X,A80,/,5X,A80,/)
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
 5300 FORMAT(/,1H1,/,
     +' *****************************************************',/,
     +' *                     Conduit Data                  *',/,
     +' *****************************************************',//,
     1'  INP  CONDUIT    LENGTH   CONDUIT     AREA    MANNING MAX WIDTH 
     1    DEPTH       JUNCTIONS      INVERT HEIGHT     TRAPEZOID',/,
     2'  NUM   NUMBER     (FT)      CLASS   (SQ FT)     COEF.   (FT)    
     2     (FT)      AT THE ENDS    ABOVE JUNCTIONS   SIDE SLOPES',/,
     3' ----   ------   -------  --------   -------   ------- --------- 
     3    -----   -------   ------- ---------------   ----- -----')     
 5301 FORMAT(/,1H1,/,
     +' *****************************************************',/,
     +' *                     Conduit Data                  *',/,
     +' *****************************************************',//,
     1'  INP  CONDUIT    LENGTH   CONDUIT     AREA    MANNING MAX WIDTH 
     1    DEPTH       JUNCTIONS      INVERT HEIGHT     TRAPEZOID',/,
     2'  NUM   NUMBER     (M)       CLASS    (SQ M)     COEF.   (M)     
     2     (M)       AT THE ENDS    ABOVE JUNCTIONS   SIDE SLOPES',/,
     3' ----   ------   -------  --------   -------   ------- --------- 
     3    -----   -------   ------- ---------------   ----- -----')     
 5320 FORMAT(I4,I10,F10.0,A10,F10.2,F10.5,F10.2,F10.2,2I10,16X,2F7.2)
 5321 FORMAT(I4,I10,F10.0,A10,F10.2,F10.5,F10.2,F10.2,2I10)
 5322 FORMAT(I4,I10,F10.0,A10,F10.2,F10.5,F10.2,F10.2,2I10,2F8.2,2F7.2)
 5323 FORMAT(I4,I10,F10.0,A10,F10.2,F10.5,F10.2,F10.2,2I10,2F8.2)
 5335 FORMAT(' ===> WARNING !!! (C*DELT/LEN) IN CONDUIT',
     .       I10,' IS',F5.1,' AT FULL DEPTH.')
 5336 FORMAT(' ===> WARNING !!! (C*DELT/LEN) IN CONDUIT ',
     .       A10,' IS',F5.1,' AT FULL DEPTH.')
 5337 FORMAT(' ===> THE NEW EQUIVALENT LENGTH/ROUGHNESS/RATIO FOR COND',
     +'UIT ',I10,' IS ',F10.1,' / ',F10.7,' / ',F5.1)
 5338 FORMAT(' ===> THE NEW EQUIVALENT LENGTH/ROUGHNESS/RATIO FOR COND',
     +'UIT ',A10,' IS ',F10.1,' / ',F10.7,' / ',F5.1)
 5339 FORMAT(/,
     +' ****************************************',/,
     +' *  Equivalent Conduit Volume Analysis  *',/,
     +' ****************************************',//,
     +' Input full depth volume............',1PE14.4,' cubic feet',/,
     +' New full depth volume..............',1PE14.4,' cubic feet',/,
     +' New volume / Old volume ratio......',0PF14.4)
 5340 FORMAT(/,
     +' ****************************************',/,
     +' *  Equivalent Conduit Volume Analysis  *',/,
     +' ****************************************',//,
     +' Input full depth volume............',1PE14.4,' cubic meters',/,
     +' New full depth volume..............',1PE14.4,' cubic meters',/,
     +' New volume / Old volume ratio......',0PF14.4)
 5341 FORMAT(/,
     +' ********************',/,
     +' *  Conduit Volume  *',/,
     +' ********************',//,
     +' Input full depth volume............',1PE14.4,' cubic feet')
 5342 FORMAT(/,
     +' ********************',/,
     +' *  Conduit Volume  *',/,
     +' ********************',//,
     +' Input full depth volume............',1PE14.4,' cubic meters')
 5350 FORMAT(/,'===> WARNING !!! JUNCTION',I10,
     1         ' IS NOT ASSOCIATED WITH ANY CONDUIT.')
 5351 FORMAT(/,'===> WARNING !!! JUNCTION',A10,
     1         ' IS NOT ASSOCIATED WITH ANY CONDUIT.')
 5355 FORMAT(/,1H1,/,
     +' *****************************************************',/,
     +' *             Junction Land Use Data                *',/,
     +' *****************************************************',//,
     1'  INP  JUNCTION   LAND USE   APPLIED TO ',/,
     3'  NUM   NUMBER     FACTOR    QINST (CFS)',/,
     4'  ---  --------   --------  ------------')
 5358 FORMAT(1X,I3,1X,I9,7X,I2,7X,F7.2)
 5359 FORMAT(1X,I3,1X,A10,7X,I2,7X,F7.2)
 5360 FORMAT(/,1H1,/,
     +' *****************************************************',/,
     +' *                  Junction Data                    *',/,
     +' *****************************************************',//,
     1' INP  JUNCTION    GROUND    CROWN     INVERT     QINST   INITIAL 
     +    CONNECTING CONDUITS',/,
     3' NUM    NUMBER     ELEV.     ELEV.     ELEV.       CFS DEPTH(FT)'
     3,/,' ---    ------   -------   -------    ------   ------- -------
     3--     -------------------')
 5361 FORMAT(/,1H1,/,
     +' *****************************************************',/,
     +' *                  Junction Data                    *',/,
     +' *****************************************************',//,
     1' INP  JUNCTION    GROUND    CROWN     INVERT     QINST   INITIAL 
     +  CONNECTING CONDUITS',/,
     3' NUM    NUMBER     ELEV.     ELEV.     ELEV.       CMS  DEPTH(M)'
     3,/,' ---    ------   -------   -------    ------   ------- -------
     3--   -------------------')
 5380 FORMAT(I4,I10,F10.2,4F10.2,3X,8(I7,1X))
 5381 FORMAT(I4,1X,A10,F9.2,4F10.2,3X,5(A10,1X),/,67X,3(A10,1X))
 5382 FORMAT(5X,' ===> WARNING all conduits connecting to Junction ',
     +  I9,/,5X,' lie above the Junction invert.')
 5383 FORMAT(5X,' ===> WARNING all conduits connecting to Junction ',
     + A10,/,5X,' lie above the Junction invert.')
 5390 FORMAT(/,' ===> ERROR !!! JUNCTION',I9,' ON CONDUIT',I9,
     1       ' IS NOT CONTAINED IN JUNCTION DATA')
 5391 FORMAT(/,' ===> ERROR !!! JUNCTION',A10,' ON CONDUIT ',A10,
     1       ' IS NOT CONTAINED IN JUNCTION DATA')
 5392 FORMAT(' ===> WARNING !!!  THE INVERT OF ',
     *'CONDUIT ',I9,' LIES ABOVE THE CROWN OF ALL CONDUITS ',
     *' AT JUNCTION ',I9)
 5393 FORMAT(' ===> WARNING !!!  THE INVERT OF ',
     *'CONDUIT ',A10,' LIES ABOVE THE CROWN OF ALL CONDUITS ',
     *' AT JUNCTION ',A10)
 5395 FORMAT(/,' ===> ERROR !!!  CONDUIT',I9,' HAS CAUSED ZCROWN OF',
     1       ' JUNCTION',I9,' TO LIE ABOVE THE SPECIFIED GROUND ELEV.')
 5396 FORMAT(/,' ===> ERROR !!!  CONDUIT ',A10,' HAS CAUSED ZCROWN ',
     1   'OF JUNCTION ',A10,' TO LIE ABOVE THE SPECIFIED GROUND ELEV.')
 5420 FORMAT(I4,1X,A10,F9.0,A10,F10.2,F10.5,F10.2,F10.2,
     +                                 2X,2A9,16X,2F7.2)
 5421 FORMAT(I4,1X,A10,F9.0,A10,F10.2,F10.5,F10.2,F10.2,2X,2A9)
 5422 FORMAT(I4,1X,A10,F9.0,A10,F10.2,F10.5,F10.2,F10.2,2X,2A9,
     +                                           2F8.2,2F7.2)
 5423 FORMAT(I4,1X,A10,F9.0,A10,F10.2,F10.5,F10.2,F10.2,2X,2A9,2F8.2)
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
 9020 FORMAT(1X,I10,A20)
 9021 FORMAT(1X,A10,A20)
 9030 FORMAT(/)
C
      END
