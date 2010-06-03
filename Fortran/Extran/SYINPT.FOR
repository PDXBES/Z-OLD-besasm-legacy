       SUBROUTINE SYINPT
C===============================================================
C       READS INPUT DATA FROM INPUT FILE AND WRITES AN ECHO
C       OUTPUT OF THE DATA TO THE OUTPUT FILE
C Converted to SWMM4.05 by Steve Merrill, Oct 1991
c   for use with PDXTRAN
C===============================================================
       INCLUDE 'TAPES.INC'
       INCLUDE 'CONTR.INC'
       INCLUDE 'SIPHON.INC'
       INCLUDE 'PIPE.INC'
       INCLUDE 'JUNC.INC'
C===============================================================
C      INITIALIZE SIPHON WEIR VARIABLES
C===============================================================
       DO 10 I = 1, NSYF
          NSIPHN(I) = 0
          DO 5 M = 1, 4
          SYAREA(I,M) = 0.0
          SYLENG(I,M) = 0.0
          SYCRST(I,M) = 0.0
          SLPRIM(I,M) = 0.0
          SYFOFF(I,M) = 0.0
          SYCOEF(I,M) = 1.0
          NSFLOW(I,M) = 0
  5    CONTINUE
 10    CONTINUE
C===============================================================
C       READ SIPHON DATA FROM INPUT FILE
C===============================================================
       DO 20 I = 1, NSYF
             READ(N5,*,ERR=888) CC
             BACKSPACE N5
           IF(CC.NE.'M1') GOTO 30
             N = NTL + I
             READ(N5,*,ERR=888) CC,(NJUNC(N,K),K=1,2),NSIPHN(I)
             NUMSYF = NSIPHN(I)
         DO 15 M = 1, NUMSYF
         READ(N5,*,ERR=888) CC,SYAREA(I,M),SYLENG(I,M),SYCRST(I,M),
     +                      SLPRIM(I,M),SYFOFF(I,M),SYCOEF(I,M)
 15    CONTINUE
 20    CONTINUE
 30    CONTINUE
             NSYFON = I - 1
       IF(NSYFON.LE.0) GOTO 300
C================================================================
C      CONVERT TO INTERNAL NUMBERING SYSTEM
C       CONSISTENT WITH EXTRAN
C================================================================
        WRITE(N6,200)
        DO 90 I = 1, NSYFON
            NS1 = NTL + I
            LSYFON(I) = NS1
            NCOND(NS1) = 90000 + NS1
         WRITE(N6,210) I,NCOND(NS1),NJUNC(NS1,1),NJUNC(NS1,2),
     +                 NSIPHN(I)
            DO 80 K = 1, 2
               IF(NJUNC(NS1,K).EQ.0) GOTO 80
               DO 40 J = 1, NJ
                  IF(NJUNC(NS1,K).EQ.JUN(J)) GOTO 50
 40   CONTINUE
                  WRITE(N6,240) NJUNC(NS1,K)
                  NSTOP = NSTOP + 1
 50   CONTINUE
                NJUNC(NS1,K) = J
                DO 60 KK = 1, 8
                   IF(NCHAN(J,KK)) 70,70,60
 60   CONTINUE
CPDX---ERROR TRAP-------------------------------------------------------
                    IF(JCE.EQ.0)WRITE(N6,5594)I,JUN(J)
                    IF(JCE.EQ.1)WRITE(N6,5593)I,AJUN(J)
                    NSTOP=NSTOP+1
5594  FORMAT(/' ===> ERROR !!! SYPHON WEIR',I3,' CAUSES MORE THAN 8 CONN
     +ECTIONS TO JUNCTION ' ,I10)
5593  FORMAT(/' ===> ERROR !!! SYPHON WEIR',I3,' CAUSES MORE THAN 8 CONN
     +ECTIONS TO JUNCTION ' ,A10)
CPDX--------------------------------------------------------------------
 70   CONTINUE
                   NCHAN(J,KK) = NS1
 80   CONTINUE
 90   CONTINUE
            NTL = NTL + NSYFON
C================================================================
          WRITE(N6,220)
       DO 110 I = 1, NSYFON
          NUMSYF = NSIPHN(I)
          DO 100 M = 1, NUMSYF
          WRITE(N6,230) I,M,SYAREA(I,M),SYLENG(I,M),SYCRST(I,M),
     +                  SLPRIM(I,M),SYFOFF(I,M),SYCOEF(I,M)
 100   CONTINUE
 110   CONTINUE
C================================================================
 200  FORMAT(//,
     +' ***********************************************************',/,
     +' *                  SIPHON LINK DATA                       *',/,
     +' ***********************************************************',/,
     +'                                               NUMBER       ',/,
     +'      SIPHON    EXTRAN     FROM     TO       OF SIPHONS     ',/,
     +'     LINK No.   LINK No.   JUNCT.   JUNCT.   IN BATTERY     ',/,
     +'     --------   --------   ------   ------   ----------     ',/)
 210  FORMAT(6X,I4,3X,I9,3X,I7,2X,I7,3X,I4)
 220  FORMAT(//,
     +'*************************************************************',/,
     +'*                     SIPHON DATA                           *',/,
     +'*************************************************************',/,
     +'                                       DEPTH    DE-    DIS-  ',/,
     +'LINK  SIPHON  SIPHON  CREST    CREST    TO     PRIME  CHARGE ',/,
     +' No.    No.    AREA   LENGTH   HEIGHT  PRIME   DEPTH  COEFF. ',/,
     +'----  ------  ------  ------   ------  -----   -----  ------ ',/)
 230  FORMAT(I3,3X,I3,3X,F6.2,3X,F6.2,3X,F6.2,2X,F6.2,2X,F6.2,3X,F4.2)
 240  FORMAT(/,' ===> ERROR !!! INPUT SIPHON WEIR JUNCTION ',I9,
     1'  IS NOT CONTAINED IN THE JUNCTION DATA')
C================================================================
C     CALL SUBROUTINE IERROR IF AN ERROR OCCURS IN THE INPUT
C================================================================
 300    CONTINUE
        RETURN
 888    CALL IERROR
        END
