      SUBROUTINE LEAPIN
C=======================================================================
C     THIS SUBROUTINE READS AND INITIALIZES THE LEAPING WEIR ROUTINES
C     AS CONCEIVED BY THE CITY OF SEATTLE, WA. ENGR. DESIGN DIV.
C=======================================================================
      INCLUDE 'TAPES.INC'
      INCLUDE 'CONTR.INC'
      INCLUDE 'JUNC.INC'
      INCLUDE 'PIPE.INC'
      INCLUDE 'LEAPW.INC'
C=======================================================================
      CHARACTER*8 TYPE(2)
      CHARACTER*10 ALEAP(NGA),AODECO(NGA)
      DATA TYPE/'HEAD    ','VELOCITY'/
C=======================================================================
      DO 10 I=1,NGA
10    TDEP(I)=0
C=======================================================================
C     READ LEAPING WEIR RECORDS
C         CC     = F3
C         ILTP   = CONTROL TYPE;  1=HEAD, 2=VELOCITY
C         NLEAP  = JUNCTION NUMBER CONTAINING THE LEAPING WEIR
C         NODECO = NODE TO WHICH FLOW 'LEAPS' TO AT THE SET POINT
C         TDEP   = SET POINT FOR LEAP; DEPTH(FT) OR VELOCITY(FPS)
C=======================================================================
      DO 20 I=1,NGA
        READ(N5,*,ERR=888)CC
        BACKSPACE N5
        IF(CC.NE.'F3')GO TO 50
        IF(I.EQ.1)WRITE(N6,4000)
        IF(JCE.EQ.0)THEN
                  READ(N5,*,ERR=888)CC,ILTP,NLEAP(I),NODECO(I),TDEP(I)
                  WRITE(N6,4010)i,TYPE(ILTP),NLEAP(I),NODECO(I),TDEP(I)
                  ELSE
                  READ(N5,*,ERR=888)CC,ILTP,ALEAP(I),AODECO(I),TDEP(I)
                  WRITE(N6,4020)i,TYPE(ILTP),ALEAP(I),AODECO(I),TDEP(I)
                  ENDIF
        LWTYP(I)=ILTP
20    CONTINUE

50    NLPW=I-1
C=======================================================================
C     CONVERT TO INTERNAL NUMBER SYSTEM
C=======================================================================
      IF(NLPW.GT.0)THEN

      DO 100 I=1,NLPW
        DO 105 J=1,NJ
         IF(JCE.EQ.0 .AND. NLEAP(I).EQ.JUN(J))GO TO 110
         IF(JCE.EQ.1 .AND. ALEAP(I).EQ.AJUN(J))GO TO 110
105     CONTINUE

      IF(JCE.EQ.0)WRITE(N6,1000)NLEAP(I)
      IF(JCE.EQ.1)WRITE(N6,1005)ALEAP(I)
      NSTOP=NSTOP+1

110   CONTINUE
C
C     CHECK FOR MULTIPLE UPSTREAM CHANNELS
C
      MATCH=0
      DO 115 N=1,NC
        IF(NJUNC(N,2).EQ.J)THEN
          MATCH=MATCH+1
          ENDIF
115   CONTINUE

      IF(MATCH.GT.1)THEN
         IF(JCE.EQ.0)WRITE(N6,6000)NLEAP(I),MATCH
         IF(JCE.EQ.1)WRITE(N6,6001)ALEAP(I),MATCH
         NSTOP=NSTOP+1
         ENDIF

      NLEAP(I)=J

100   CONTINUE

      DO 200 I=1,NLPW
        DO 205 J=1,NJ
          IF(JCE.EQ.0 .AND. NODECO(I).EQ.JUN(J))GO TO 210
          IF(JCE.EQ.1 .AND. AODECO(I).EQ.AJUN(J))GO TO 210
205     CONTINUE

      IF(JCE.EQ.0)WRITE(N6,1010)NODECO(I)
      IF(JCE.EQ.1)WRITE(N6,1015)AODECO(I)
      NSTOP=NSTOP+1

210   NODECO(I)=J
200   CONTINUE

      ENDIF

      RETURN
C=======================================================================
 888  CALL IERROR
C=======================================================================
1000  FORMAT(/' ===> ERROR !!!  LEAPING WEIR ',I10,' IS NOT CONTAINED ',
     +'IN THE JUNCTION DATA')
1005  FORMAT(/' ===> ERROR !!!  LEAPING WEIR ',A10,' IS NOT CONTAINED ',
     +'IN THE JUNCTION DATA')
1010  FORMAT(/' ===> ERROR !!!  LEAPING WEIR CONNECTING NODE ',I10,
     +' IS NOT CONTAINED IN JUNCTION DATA')
1015  FORMAT(/' ===> ERROR !!!  LEAPING WEIR CONNECTING NODE ',A10,
     +' IS NOT CONTAINED IN JUNCTION DATA')
4000  FORMAT(/,
     +' ********************************************',/,
     +' *                Leaping Weir Data         *',/,
     +' ********************************************',//,
     1'  INP  CONTROL  JUNCTION   LEAP TO       SET'/,
     3'  NUM     TYPE    NUMBER      NODE     POINT'/,
     3'  ---  -------  --------   -------     -----')
4010  FORMAT(I5,A9,I10,I10,F10.3)
4020  FORMAT(I5,A9,A10,A10,F10.3)
6000  FORMAT(/' ===> ERROR !!!  LEAPING WEIR AT ',I10,' HAS ',I5,
     +' UPSTREAM CONDUITS--ONLY ONE IS ALLOWED')
6001  FORMAT(/' ===> ERROR !!!  LEAPING WEIR AT ',A10,' HAS ',I5,
     +' UPSTREAM CONDUITS--ONLY ONE IS ALLOWED')
C=======================================================================
      END
