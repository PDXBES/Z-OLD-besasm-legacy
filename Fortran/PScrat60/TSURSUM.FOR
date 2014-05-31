      SUBROUTINE TSURSUM
C======================================================================
C ROUTINE TO READ THE SURCHARGE SUMMARY FROM PORTLAND TRANSPORT BLOCK
C
      CHARACTER*240 FIN,FOUT
*      CHARACTER KAN(200)
      LOGICAL OK
      DIMENSION NOE(500),QR(500),NTYPE(500),NEW(500),QO(500),NIN(500)

      NTAPE=5
      NPLACE=0
      WRITE(*,2000)
999   WRITE(*,100)
      READ(*,'(A)')FIN
      INQUIRE(FILE=FIN,EXIST=OK)
      IF(.NOT. OK)THEN
        WRITE(*,3000)
        GO TO 999
        ENDIF
      OPEN(NTAPE,FILE=FIN,FORM='UNFORMATTED')
      WRITE(*,200)
      READ(*,'(A)')FOUT
      IF(FOUT .NE. ' ')THEN
         N6=6
         OPEN(N6,FILE=FOUT)
      ELSE
         N6=0
      ENDIF
C
C NOW READ THE FLOWS FROM THE INTERFACE FILE
C
      READ(NTAPE,ERR=10)NE
      READ(NTAPE,ERR=10)(NTYPE(J),J=1,NE)
      READ(NTAPE,ERR=10)JDAY,(NOE(J),J=1,NE)
3     WRITE(*,250)(NOE(J),J=1,NE)
      WRITE(*,300)
      READ(*,*)NPLACE
      IF(NPLACE.GT.0)THEN
       DO 1 I=1,NE
         IF(NOE(I).EQ.NPLACE)GO TO 2
1      CONTINUE
       WRITE(*,350)NPLACE
       NPLACE=0
       GO TO 3
2      NPRT=I
       IF(NTYPE(I).GT.18)THEN
         WRITE(*,500)NPLACE
         GO TO 3
         ENDIF
       WRITE(N6,1000)NPRT
      ELSE
       IM=0
       DO 4 J=1,NE
         IF(NTYPE(J).LT.19)THEN
           IM=IM+1
           NEW(IM)=NOE(J)
           NIN(IM)=J
           ENDIF
4      CONTINUE
       WRITE(N6,1000)(NEW(J),J=1,IM)
      ENDIF
C
      DO 8 N=1,1000000
      READ(NTAPE,ERR=10,END=9) JDAY,(QR(J),J=1,NE)
         CALL IDATE(JDAY,MON,IDY,IYR)
         IF(NPLACE.EQ.0)THEN
          DO 5 J=1,IM
            K=NIN(J)
5           QO(J)=QR(K)
          WRITE(N6,120)MON,IDY,IYR,(QO(J),J=1,IM)
         ELSE
          WRITE(N6,120)MON,IDY,IYR,QR(NPRT)
         ENDIF
8     CONTINUE
9     CONTINUE
      WRITE(*,'(A)')' ==>> Finished Conversion of Transport ',
     + 'Surcharge File'          
      STOP
10    WRITE(*,'(A)')' ===>ERROR IN READING SCRATCH FILE'
      WRITE(N6,'(A)')' ===>ERROR IN READING SCRATCH FILE'
C======================================================================      
100   FORMAT(' Please Enter an NSCRAT(6) Filename -> '\)
200   FORMAT(' Please Enter a Text Output Filename (Space for ',
     + 'Screen)-> '\)
300   FORMAT(/' Data Saved for above conduits.',/,
     +        ' Enter one conduit # to be printed or 0 for all. -> '\)
350   FORMAT(/I10,' NOT FOUND')
250   FORMAT(5I10)
1000  FORMAT('   RATIO OF FLOW TO QFULL BY CONDUIT AND DATE',//,
     + ' CONDUIT->',5X,40I6,(15X,40I6),/)
120   FORMAT(I4,'/',I2,'/',I2,5X,40F6.2,(15X,40F6.2))
500   FORMAT(' ELEMENT ',I5,' IS NOT A CONDUIT'//)
2000  FORMAT(' >>>>  READING SURCHARGE SUMMARY FILE FROM TRANSPORT'//)
3000  FORMAT(/' NAMED SCRATCH FILE NOT FOUND-TRY AGAIN')
C======================================================================      
      END
