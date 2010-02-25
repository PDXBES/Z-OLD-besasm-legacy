      SUBROUTINE LEAPING
C=======================================================================
C     THIS ROUTINE CHECKS THE DEPTH OR VELOCITY AT EACH LEAPING WEIR
C    NODE AND CHANGES THE DOWNSTREAM NODE OF THE INFLOWING CONDUIT
C    AS APPROPRIATE.  CONCEPT ORIGINATED BY CITY OF SEATTLE, WA. EDD.
C    FUNCTION IS ESSENTIALLY AN ON/OFF SWITCH.  PROBABLY DOESN'T WORK
C    WELL UNDER SURCHARGE.
C=======================================================================
      INCLUDE 'TAPES.INC'
      INCLUDE 'STIMER.INC'
      INCLUDE 'CONTR.INC'
      INCLUDE 'JUNC.INC'
      INCLUDE 'PIPE.INC'
      INCLUDE 'LEAPW.INC'
      DATA KIM/0/
C=======================================================================
      DO 500 J=1,NLPW
        LE  = NLEAP(J)
        LCO = NODECO(J)

        IF(LWTYP(J).EQ.1)THEN
C=======================================================================
C CASE 1--DEPTH SET POINT IN LEAPING WEIR JUNCTION
C=======================================================================
          DO 10 I = 1,NC
           N=NJUNC(I,2)
           IF(KIM.EQ.0)THEN

              IF(LE.EQ.N)THEN
                IF(Y(LE).GE.TDEP(J))THEN
                                    NJUNC(I,2) = LCO
                                    KIM = 1
                                    ENDIF
                         GO TO 500
                         ENDIF
           ELSE
              IF(LCO.EQ.N)THEN
                IF(Y(LCO).LT.TDEP(J))THEN
                                     NJUNC(I,2) = LE
                                     KIM = 0
                                     ENDIF
                          GO TO 500
                          ENDIF
           ENDIF

10        CONTINUE

        ELSE
C=======================================================================
C CASE 2--VELOCITY SET POINT IN UPSTREAM CONDUIT, LWTYP=2
C=======================================================================
          DO 20 I=1,NC
            N=NJUNC(I,2)
            IF(KIM.EQ.0)THEN

              IF(LE.EQ.N)THEN
                IF(V(I).GE.TDEP(J))THEN
                                   NJUNC(I,2)=LCO
                                   KIM = 1
                                   ENDIF
                         GO TO 500
                         ENDIF
            ELSE
              IF(LCO.EQ.N)THEN
                IF(V(I).LT.TDEP(J))THEN
                                   NJUNC(I,2) = LE
                                   KIM = 1
                                   ENDIF
                          GO TO 500
                          ENDIF
            ENDIF

20        CONTINUE

        ENDIF
C=======================================================================
500   CONTINUE
      RETURN
      END

