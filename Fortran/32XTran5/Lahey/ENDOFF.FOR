       SUBROUTINE ENDOFF
C================================================================
C     PRINTS APPROPRIATE ERROR OR WARNING MESSAGES
C     FOR RADIAL GATE COMPUTATIONS
C================================================================
       INCLUDE 'TAPES.INC'
       INCLUDE 'CONTR.INC'
       INCLUDE 'RDGATE.INC'
       INCLUDE 'LRG.INC'
C================================================================
            TMERRS = TIME/3600.
       GOTO(1,2,3,4,5)ERRMES
C================================================================
 1     NRGERR(IR,1) = NRGERR(IR,1) + 1
       IF(NRGERR(IR,1).EQ.50) THEN
          WRITE(N6,100) IR,TMERRS
          WRITE(N6,101)
          NRGERR(IR,1) = 0
       ENDIF
       GOTO 10
 2     NRGERR(IR,2) = NRGERR(IR,2) + 1
       IF(NRGERR(IR,2).EQ.50) THEN
          WRITE(N6,100) IR,TMERRS
          WRITE(N6,102)
          NRGERR(IR,2) = 0
       ENDIF
       GOTO 10
 3     NRGERR(IR,3) = NRGERR(IR,3) + 1
       IF(NRGERR(IR,3).EQ.50) THEN
          WRITE(N6,100) IR,TMERRS
          WRITE(N6,103)
          NRGERR(IR,3) = 0
       ENDIF
       GOTO 10
 4     NRGERR(IR,4) = NRGERR(IR,4) + 1
       IF(NRGERR(IR,4).EQ.50) THEN
          WRITE(N6,100) IR,TMERRS
          WRITE(N6,104)
          NRGERR(IR,4) = 0
       ENDIF
       GOTO 10
 5     NRGERR(IR,5) = NRGERR(IR,5) + 1
       IF(NRGERR(IR,5).EQ.50) THEN
          WRITE(N6,100) IR,TMERRS
          WRITE(N6,105)
          NRGERR(IR,5) = 0
       ENDIF
C================================================================
 10    CONTINUE
C==================================================================
 100   FORMAT(/,'GATE No: ',I3,6X,'TIME - ',F6.3,' HRS')
 101   FORMAT(/,
     +' ===> WARNING !!! FREE DISCHARGE COMPUTATIONS             ',/,
     +'                  The continuity failures at the radial   ',/,
     +'                  gate have exceeded 50 !                 ',/,
     +' *** Results may be unreliable if discharge is "free" ***',/)
 102   FORMAT(/,
     +' ===> WARNING !!! SUBMERGED DISCHARGE COMPUTATIONS        ',/,
     +'                  The continuity failures at the radial   ',/,
     +'                  gate have exceeded 50 !                 ',/,
     +' *** Results may be unreliable if discharge is "submerged" **',/)
 103   FORMAT(/,
     +' ===> WARNING !!! SUBMERGED DISCHARGE  COMPUTATIONS     ',/,
     +'                  The head differential across the gate ',/,
     +'                  is computed as less than zero (exceeds 50)',/,
     +' *** Results may be unreliable if discharge is "submerged" **',/)
 104   FORMAT(/,
     +' ===> WARNING !!! SUBMERGED DISCHARGE  COMPUTATIONS       ',/,
     +'                  The conditions are out of range of the  ',/,
     +'             validity of the submerged discharge algorithms.',/,
     +'                  Occurances exceed 50!                 ',/,
     +' *** Results may be unreliable if discharge is "submerged" **',/)
 105   FORMAT(/,
     +' ===> WARNING !!! The iteration limit for radial gate',/,
     +'                  computations has been exceeded.    ',/,
     +'                  Occurances exceed 50!         ',/,
     +'               ***Results may be unreliable***    ',/)
C================================================================
       RETURN
       END
