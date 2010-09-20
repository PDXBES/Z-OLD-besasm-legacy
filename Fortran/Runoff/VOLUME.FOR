      Subroutine VOLUME(VOLNW)
CIM  new subroutine that performs system volume calculations.
CIM  called from TRANSX.FOR   1996

      INCLUDE 'TAPES.INC'
      INCLUDE 'CONTR.INC'
      INCLUDE 'JUNC.INC'
      INCLUDE 'PIPE.INC'
      INCLUDE 'VOLDAT.INC'
      INCLUDE 'TRANAID.INC'
      INCLUDE 'OUT.INC'

c      CHARACTER*3 STINK
       INTEGER STINK

c.....  Initialize volume array
	do j=1,nj
	vol(j) = 0.0
	end do
        VOLNW = 0.0

c.....  Loop though links  CODE FROM TRANSX, BUT DON'T CHANGE ANY ARRAYS
      DO 25 N   = 1,NTC
      NL        = NJUNC(N,1)
      NH        = NJUNC(N,2)
      H1    = Y(NL) + Z(NL)
      H2    = Y(NH) + Z(NH)
      IF(JREDO.EQ.0.OR.JREDO.EQ.2) THEN
                       CALL HEAD(N,NL,NH,H1,H2,QO(N),
     +                             AREA,VELL,HRAD,ANH,ANL,RNL,RNH,
     +                             IDOIT,stINK)
                                   ELSE
                       CALL HEAD(N,NL,NH,H1,H2,QT(N),
     +                             AREA,VELL,HRAD,ANH,ANL,RNL,RNH,
     +                             IDOIT,STINK)
                                   ENDIF
CIM  NOTE yo WAS HYDRAULIC RADIUS, A REQUIRED OUTPUT FOR TRANAID INTERFACE
cim  NOW IN r ARRAY
      r(N)     = HRAD
C
C=======================================================================
C     Note, initial volume will never be zero because FUDGE = 0.0001 is
C     used in Sub. HEAD instead of zeroes for cross sectional areas
C     (and other cross section parameters).  Thus, initial volume will
C     approximately equal sum of pipe lengths x FUDGE, plus corrections
C     for Courant violations plus new equivalent pipes, e.g., for
C     orifices.  I.e., in equation below, ANL = ANH = 0.0001 even if
C     zero flows and depths are specified as initial conditions.
C=======================================================================
      VOLNW     = VOLNW + (ANL+ANH)*LEN(N)/2.0
      vol(nl) = vol(nl) + 0.5*anl*len(n)
      vol(nh) = vol(nh) + 0.5*anh*len(n)
 25   CONTINUE
C=======================================================================
C     New Computation of Volume for Irregular Storage Elements.
C=======================================================================
      DO 70 I = 1,NSTORE
      J       = JSTORE(I)
      IF(ASTORE(I).GE.0.0) THEN
                         VOLNW  = VOLNW + Y(J)*ASTORE(I)
                         vol(j) = vol(j)+ Y(J)*ASTORE(I)
                         ELSE IF(NUMV(I).LT.0) THEN
                            IF(Y(J).EQ.0.0) Y(J) = 0.0001
C=======================================================================
C#### WCH, 12/8/94.  ERROR HERE.  MUST >>INTEGRATE<< AREA POWER
C     FUNCTION TO GET VOLUME!  ALSO, LET MINIMUM AREA = AMEN.
C=======================================================================
C####                         VOLNW = VOLNW +
C####     +                           VCURVE(I,1,1)*Y(J)**VCURVE(I,2,1)
C=======================================================================
C#### WCH (C.MOORE), 6/5/95.  CORRECTION TO CORRECTION.  MUST INCLUDE
C     VOLNW IN SUMMATION SINCE VOLNW IS GRAND TOTAL FOR ALL
C     PIPES/JUNCTIONS.
C=======================================================================
C####                            VOLNW = Y(J)*AMEN
                            VOLNW = VOLNW + Y(J)*AMEN
                            VOL(J) = VOL(J) + Y(J)*AMEN
                            VOLNW = VOLNW + VCURVE(I,1,1) *
     +         Y(J)**(VCURVE(I,2,1)+1)/(VCURVE(I,2,1)+1)
                            VOL(J) = VOL(J) + VCURVE(I,1,1) *
     +         Y(J)**(VCURVE(I,2,1)+1)/(VCURVE(I,2,1)+1)
                         ELSE
                         NTOX       = NUMV(I)
                         RESELV     = Y(J)
C#### WCH, 12/8/94.  MIN. AREA = AMEN NOT USED IN SUB. BOUND, SO
C     DON'T INCLUDE IT HERE.  BUT ADD CHECK IN SUB. INDAT2 TO ENSURE
C     THAT MIN. AREA NOT = ZERO.   ALSO, START WITH SUBSCRIPT IX = 2
C     TO ENSURE THAT IX-1 NOT = 0.
C####                         VOLNW      = VOLNW + RESELV*AMEN
                         DO 1891 IX = 2,NTOX
 1891                    IF(RESELV.LT.VCURVE(I,2,IX)) GO TO 1892
 1892                    DELTA = (RESELV-VCURVE(I,2,IX-1))/
     +                           (VCURVE(I,2,IX)-VCURVE(I,2,IX-1))
                         VOLNW = VOLNW+DELTA*(VCURVE(I,3,IX) -
     +                           VCURVE(I,3,IX-1))+VCURVE(I,3,IX-1)
                         VOL(J) = VOL(J)+DELTA*(VCURVE(I,3,IX) -
     +                           VCURVE(I,3,IX-1))+VCURVE(I,3,IX-1)
                         ENDIF
   70 CONTINUE

c.....  CHECK total volume
	Vtot = 0.0
	do j=1,nj
	Vtot = Vtot + Vol(j)
	end do
cim        WRITE(N6,*) 'VOLNOW = ',VOLNW
cim        WRITE(N6,*) 'VTOT   = ',VTOT
      RETURN
      END
