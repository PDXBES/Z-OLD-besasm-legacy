       SUBROUTINE SYPHON(NUMSYF,YS1,YS2,ZS1,QSIPHN)
C========================================================================
C         COMPUTES DISCHARGE FOR SIPHON WEIR LINKS
C========================================================================
       INCLUDE 'TAPES.INC'
       INCLUDE 'SIPHON.INC'
       INCLUDE 'CONST.INC'
       INCLUDE 'CONTR.INC'
C========================================================================
             TRVFLW = TIME/3600.0
             QTOTAL = 0.0
C========================================================================
C             COMPUTE FLOW FOR EACH SIPHON IN BATTERY
C========================================================================
             DO 20 M = 1, NUMSYF
                YSWEIR = SYCRST(IS,M) + ZS1
                YPRIME = SLPRIM(IS,M) + ZS1
                YSSHUT = SYFOFF(IS,M) + ZS1
C========================================================================
C    IF DEPTH IS LOWER THAN THE SIPHON CREST ---> NO FLOW
C       ONCE PRIMED, FLOW STOPS WHEN YS1 < YSHUT
C========================================================================
        IF(NSFLOW(IS,M).EQ.1.AND.YS1.LT.YSSHUT) NSFLOW(IS,M) = 0
        IF(NSFLOW(IS,M).EQ.0) THEN
           IF(YS1.LE.YSWEIR) THEN
              SYF(IS,M) = 1
              QSYF = 0.0
              GOTO 10
           ENDIF
C========================================================================
C    IF DEPTH IS GREATER THAN THE SIPHON CREST ---> WEIR FLOW
C========================================================================
           IF(YS1.LT.YPRIME) THEN
              SYF(IS,M) = 2
              HEADWR = YS1-YSWEIR
              CDWR = SYCOEF(IS,M)
              WRLENG = SYLENG(IS,M)
              QSWEIR = CDWR*WRLENG*(HEADWR**1.5)
              QSYF   = QSWEIR
              GOTO 15
           ENDIF
 10     CONTINUE
       ENDIF
C========================================================================
C    IF DEPTH IS GREATER THAN PRIMING LEVEL  ---> SIPHON FLOW
C========================================================================
           IF(YS1.GE.YPRIME) THEN
              SYF(IS,M) = 3
              HEADSY = YS1-YS2
C========================================================================
C            FLAG REVERSE FLOW IN OUTPUT FILE
C    DO NOT ALLLOW REVERSE FLOW FOR SIPHON WEIRS
C========================================================================
           IF(HEADSY.LT.0.0) THEN
                 WRITE(N6,30) IS, TRVFLW
                 WRITE(N6,50)
                 QSYF = 0.0
                 GOTO 15
           ENDIF
C========================================================================
           QSYF = SYCOEF(IS,M)*SYAREA(IS,M)*SQRT(2.0*GC*HEADSY)
              NSFLOW(IS,M) = 1
           ENDIF
C========================================================================
 15    CONTINUE
       QTOTAL = QTOTAL + QSYF
 20    CONTINUE
C========================================================================
       QSIPHN = QTOTAL
C========================================================================
 30   FORMAT(/,
     +' ===> WARNING !!! SIPHON LINK No. ',I3,' ;AT TIME = ',F8.2,
     +' Hrs',/,'      SIPHON HEAD HAS BEEN COMPUTED AS NEGATIVE !!')
 50   FORMAT(/,
     +'      REVERSE FLOW IS NOT PERMITTED ACROSS THE SIPHON-WEIR!',/,
     +'      SET SIPHON FLOW TO ZERO AT THIS TIME !!',/)
C========================================================================
       RETURN
       END
