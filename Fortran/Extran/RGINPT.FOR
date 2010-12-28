       SUBROUTINE RGINPT
C===============================================================
C       READS INPUT DATA FROM INPUT FILE AND WRITES AN ECHO
C       OUTPUT OF THE DATA TO THE OUTPUT FILE
C Converted to SWMM4.05 by Steve Merrill, Oct 1991
c   for use with PDXTRAN
C===============================================================
        INCLUDE 'TAPES.INC'
        INCLUDE 'CONTR.INC'
        INCLUDE 'RDGATE.INC'
        INCLUDE 'RINGAT.INC'
        INCLUDE 'RGINTK.INC'
        INCLUDE 'RGFLOW.INC'
        INCLUDE 'PIPE.INC'
        INCLUDE 'JUNC.INC'
        INCLUDE 'LRG.INC'
C===============================================================
       CHARACTER*8 FCNTRL
       DIMENSION FCNTRL(NRADG)
C===============================================================
C      INITIALIZE RADIAL GATE VARIABLES
C===============================================================
       CALL RGINIT
C===============================================================
C       READ RADIAL GATE DATA FROM INPUT FILE
C===============================================================
       DO 10 I = 1, NRADG
             READ(N5,*,ERR=888) CC
             BACKSPACE N5
           IF(CC.NE.'L1') GOTO 20
             N = NTL + I
              READ(N5,*,ERR=888) CC,(NJUNC(N,K),K=1,2),GWDTH(I),
     +            PINHGT(I), GRAD(I), CFDA(I), CSDA(I), YINVRT(I),
     +            YSYFON(I), UPTRNS(I), DNTRNS(I)
 10    CONTINUE
 20    CONTINUE
             NRGATE = I - 1
       IF(NRGATE.LE.0) GOTO 300
       DO 30 I = 1, NRGATE
              READ(N5,*,ERR=888) CC,UPINV(I),DNINV(I),UPDEP(I),
     +                           RLRUFF(I),CDRG(I),CFLRVS(I)
 30    CONTINUE
       DO 40 I = 1,NRGATE
         READ(N5,*,ERR=888) CC,CONTRL(I),DISCND(I),YDELTA,QDELTA,RGIMAX
 40    CONTINUE
       DO 60 I = 1, NRGATE
           IF(CONTRL(I).EQ.0) THEN
              FCNTRL(I) = 'CONSTANT'
              READ(N5,*,ERR=888) CC, GOPEN(I)
           ELSEIF(CONTRL(I).EQ.1) THEN
              FCNTRL(I) = '  TIME  '
              READ(N5,*,ERR=888) CC,GOPEN(I),CRTJUN(I),DNYCLS(I),
     +                           DNYOPN(I)
              READ(N5,*,ERR=888) CC,RTGCLS(I),RTEGPN(I),GPNMAX(I),
     +                           GPNMIN(I)
           ELSEIF(CONTRL(I).EQ.2) THEN
              FCNTRL(I) = ' DEPTH  '
CPDX--SM, 11/11/91--NTVAL CHANGED TO NTVALR
              READ(N5,*,ERR=888) CC,GOPEN(I),NTVALR(I),CRTJUN(I)
             DO 50 M = 1, NTVALR(I)
                   READ(N5,*,ERR=888) CC,GCDPTH(I,M),GDOPEN(I,M)
 50          CONTINUE
           ELSEIF(CONTRL(I).EQ.3) THEN
              FCNTRL(I) = '  TIME  '
              READ(N5,*,ERR=888) CC,GOPEN(I),CRTJUN(I),DNYCLS(I),
     +                           DNYOPN(I)
              READ(N5,*,ERR=888) CC,RTGCLS(I),RTEGPN(I),GPNMAX(I),
     +                           GPNMIN(I)
           ELSE
             NSTOP = NSTOP + 1
             WRITE(N6,235) I
           ENDIF
           IF(GOPEN(I).LE.0.0) GOPEN(I) = 0.01
 60    CONTINUE
C================================================================
C      CONVERT TO INTERNAL NUMBERING SYSTEM
C       CONSISTENT WITH EXTRAN
C================================================================
        WRITE(N6,200)
        WRITE(N6,210)
        DO 100 I = 1, NRGATE
            NR1 = NTL + I
            N = NTL + I
            LRGATE(I) = NR1
            NCOND(NR1) = 90000 + NR1
         IF(DISCND(I).EQ.0) NRPIPE = NRPIPE + 1
         WRITE(N6,220) I,NCOND(NR1),NJUNC(N,1), NJUNC(N,2),
     +                  UPINV(I), DNINV(I)
            DO 90 K = 1, 2
               IF(NJUNC(NR1,K).EQ.0) GOTO 90
               DO 70 J = 1, NJ
                  IF(NJUNC(NR1,K).EQ.JUN(J)) GOTO 75
 70   CONTINUE
                  WRITE(N6,230) NJUNC(NR1,K)
                  NSTOP = NSTOP + 1
 75   CONTINUE
                NJUNC(NR1,K) = J
                RGJUP(I) = NJUNC(NR1,1)
                RGJDN(I) = NJUNC(NR1,2)
                DO 80 KK = 1, 8
                   IF(NCHAN(J,KK)) 85,85,80
 80   CONTINUE
CPDX---ERROR TRAP-------------------------------------------------------
                    IF(JCE.EQ.0)WRITE(N6,5594)I,JUN(J)
                    IF(JCE.EQ.1)WRITE(N6,5593)A,AJUN(J)
                    NSTOP=NSTOP+1
5594  FORMAT(/' ===> ERROR !!! REGULATOR GATE',I3,' CAUSES MORE THAN 8 C
     +ONNECTIONS TO JUNC ' ,I10)
5593  FORMAT(/' ===> ERROR !!! REGULATOR GATE',I3,' CAUSES MORE THAN 8 C
     +ONNECTIONS TO JUNC ' ,A10)
CPDX--------------------------------------------------------------------
 85   CONTINUE
                   NCHAN(J,KK) = NR1
 90   CONTINUE
 100  CONTINUE
            NTL = NTL + NRGATE
            NTC = NTC + NRPIPE
C================================================================
C     DETERMINE NODE NUMBER CONTROLLING GATE OPERATION
C================================================================
      DO 120 I = 1, NRGATE
      DO 110 J = 1, NJ
         IF(CRTJUN(I).EQ.JUN(J)) GOTO 115
 110   CONTINUE
CPDX--SM. 11/23/91------------------------------------------------------
      WRITE(N6,2000)CRTJUN(I)
      NSTOP=NSTOP+1
2000  FORMAT(/' ===> ERROR !!! REGULATOR GATE CONTROL NODE ',I10,
     +' NOT FOUND IN JUNCTION (D1) CARDS')
CPDX--------------------------------------------------------------------
 115   CONTINUE
         CRTJUN(I) = J
C================================================================
C     DETERMINE CONDUIT NUMBER UPSTREAM & DOWNSTREAM OF GATE
C================================================================
            DO 117 NP = 1, NTC
               NUR = NJUNC(NP,1)
               NDR = NJUNC(NP,2)
               IF(NDR.EQ.RGJUP(I)) THEN
                 NRGPUP(I) = NP
               ENDIF
               IF(NUR.EQ.RGJDN(I)) THEN
                 NRGPDN(I) = NP
               ENDIF
 117   CONTINUE
 120   CONTINUE
C================================================================
C        WRITE ECHO INPUT OF RADIAL GATE DATA
C================================================================
        WRITE(N6,250)
      DO 130 I = 1, NRGATE
         WRITE(N6,260) I,GWDTH(I),GRAD(I),PINHGT(I),FCNTRL(I)
 130  CONTINUE
      DO 140 I = 1, NRGATE
       IF(DISCND(I).EQ.0) THEN
          WRITE(N6,240) I
       ELSEIF(DISCND(I).EQ.1) THEN
          WRITE(N6,242) I
       ELSEIF(DISCND(I).EQ.2) THEN
          WRITE(N6,245) I
       ELSEIF(DISCND(I).EQ.3) THEN
         WRITE(N6,248) I, CDRG(I)
       ELSE
          WRITE(N6,255) I
          NSTOP = NSTOP + 1
       ENDIF
 140  CONTINUE
C================================================================
C       IF EQUIVALENT PIPE OPTION IS CHOSEN
C          - INITIALIZE PIPE PARAMETERS
C
C       NOTE: THIS IS THE SAME AS EXTRAN ORIFICES
C             EXCEPT THE GATE OPENING CAN VARY DYNAMICALLY
C================================================================
             IPR = 0
      DO 180 I = 1, NRGATE
         IF(DISCND(I).NE.0) GOTO 180
            IPR = IPR + 1
            N = LRGATE(I)
            NKLASS(N) = 11
            WIDE(N) = GWDTH(I)
            DEEP(N) = GOPEN(I)
            AFULL(N) = GWDTH(I)*GOPEN(I)
            RFULL(N) = AFULL(N)/(2.0*GWDTH(I) + 2.0*GOPEN(I))
                      DDD = DELT
            IF(NEQUAL.GT.1) DDD = FLOAT(NEQUAL)
            CLEN     = 2.0*DDD*SQRT(GRVT*(AFULL(N)/GWDTH(I)))
            LEN(N)   = AMAX1(400.,CLEN)
            CMANN    = CMET(9,METRIC)
      ROUGH(N) = CMANN*RFULL(N)**.66667/(CDRG(I)*SQRT(LEN(N)*2.0*GRVT))
C=======================================================================
C     SET ZU(N) AND ZD(N) ELEVATIONS
C=======================================================================
      DO 150 J = 1,NJ
        IF(RGJUP(I).EQ.J) THEN
           KRP = 1
        ELSEIF(RGJDN(I).EQ.J) THEN
           KRP = 2
        ELSE
           KRP = 0
        ENDIF
C=======================================================================
           IF(KRP.EQ.1) THEN
             IF(Z(J).LE.UPINV(I)) ZU(N) = Z(J) + (UPINV(I)-Z(J))
             IF(Z(J).GT.UPINV(I)) ZU(N) = Z(J)
           ENDIF
           IF(KRP.EQ.2) THEN
             IF(Z(J).LE.DNINV(I)) ZD(N) = Z(J) + (DNINV(I)-Z(J))
             IF(Z(J).GT.DNINV(I)) ZD(N) = Z(J)
           ENDIF
 150  CONTINUE
C=======================================================================
C     CHECK GRAVITY FLOW DIRECTION
C=======================================================================
      IF(ZU(N).LT.ZD(N)) THEN
                         J2    = NJUNC(N,2)
                         WRITE(N6,270) JUN(J2)
                         NSTOP = NSTOP+1
                         ENDIF
      WRITE(N6,280) I,NCOND(N),LEN(N),ROUGH(N),ZU(N),ZD(N)
C=======================================================================
 180  CONTINUE
C=======================================================================
 200   FORMAT(/,
     +'      ***************************************',/,
     +'      *          RADIAL GATE  DATA          *',/,
     +'      ***************************************',/)
 210   FORMAT(/,
     +'  *******************************************************   ',/,
     +'  *            RADIAL GATE SECTION DATA                 *   ',/,
     +'  *******************************************************   ',/,
     +'                                     INTAKE        INTAKE   ',/,
     +'GATE   LINK      FROM       TO       UPSTREAM    DOWNSTREAM ',/,
     +' No.  NUMBER   JUNCTION  JUNCTION   INVRT.ELEV   INVRT.ELEV ',/,
     +'----  ------   --------  --------   ----------   ---------- ',/)
 220   FORMAT(I2,2X,I6,2X,I9,2X,I9,6X,F8.2,5X,F8.2)
 230   FORMAT(/,' ===> ERROR !!! INPUT RADIAL GATE JUNCTION ',I9,
     1'  IS NOT CONTAINED IN THE JUNCTION DATA')
 235   FORMAT(/,' ===> ERROR !!! INPUT RADIAL GATE ',I3,
     1'  CONTRL = 0, 1 or 2  ONLY !!!')
 240   FORMAT(/,'=======> RADIAL GATE ',I3,
     1'       IS MODELLED AS AN EQUIVALENT PIPE !')
 242   FORMAT(/,'=======> RADIAL GATE ',I3,
     1'       FREE & SUBMERGED DISCHARGE ALGORITHMS USED.')
 245   FORMAT(/,'=======> RADIAL GATE ',I3,
     1'       FREE DISCHARGE ASSUMED AT ALL TIMES.')
 248   FORMAT(/,'=======> RADIAL GATE ',I3,
     1' USER INPUT DISCHARGE COEFFICIENT ',F4.2,' USED AT ALL TIMES!!')
 255   FORMAT(/,' ===> ERROR !!! INPUT RADIAL GATE ',I3,
     1'  DISCND = 0,1,2 OR 3  ONLY !!!')
 250   FORMAT(/,
     +' *********************************************************',/,
     +' *            GEOMETRY OF RADIAL GATES                   *',/,
     +' *********************************************************',/,
     +'                                                          ',/,
     +'   GATE      GATE        ARM        PINION       CONTROL  ',/,
     +'    No.     WIDTH       RADIUS      HEIGHT        TYPE    ',/,
     +'   ----     -----       ------      -------      -------  ',/)
 260   FORMAT(2X,I3,6X,F6.3,7X,F6.3,6X,F6.3,8X,A8)
 270  FORMAT(/,' ===> ERROR !!! "RADIAL GATE PIPE" AT JUNCTION ',I9,
     1' HAS AN ADVERSE SLOPE. ')
 280   FORMAT(/,' ====> EQUIVALENT PIPE FOR RADIAL GATE # ',I7,/,
     1       '       CONDUIT NUMBER..........................',I9,/,
     3       '       PIPE LENGTH..........................',F12.2,/,
     4       '       MANNINGS ROUGHNESS...................',F12.4,/,
     5       '       INVERT ELEVATION AT UPSTREAM END.....',F12.4,/,
     6       '       INVERT ELEVATION AT DOWNSTREAM END...',F12.4,//,
     7 'These dimensions will vary with the dynamic gate opening !')
C================================================================
C     CALL SUBROUTINE IERROR IF AN ERROR OCCURS IN THE INPUT
C================================================================
 300    CONTINUE
        RETURN
 888    CALL IERROR
        END
