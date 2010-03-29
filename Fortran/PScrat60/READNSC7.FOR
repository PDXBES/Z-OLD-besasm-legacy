       SUBROUTINE READNSC7
*
*	Purpose of this program is to read the NSCRAT7
*	files out of SWMM Transport and write them
*	to an ASCII file:
*
      Real QO(500) 
      CHARACTER*240  filein,fileout
*      CHARACTER*8   pname(4), punit(4)
*      CHARACTER*20  source
*       CHARACTER title(4)*80
C
*****************************************************************************
C
C     Get input file
C
       WRITE(*,10)
   10  FORMAT( /,10X,'PLEASE TYPE THE SCRATCH #7 FILE NAME:  ',\)
       READ(*,15) filein
C
   15  FORMAT(A12)
C
      OPEN(1,FILE=filein,FORM='UNFORMATTED')
      REWIND(1)
C
C     Get output file
C
       WRITE(*,25)
   25  FORMAT( /,10X,'PLEASE TYPE THE OUTPUT FILE NAME:  ',\)
       READ(*,15) fileout
C
      OPEN(2,FILE=fileout,FORM='FORMATTED')
C
*
*	 Get the value of NNPE:
*
         WRITE(*,35)
   35    FORMAT( /,15x,'PLEASE ENTER THE NUMBER OF CONDUITS ON FILE: ',
     &       /,15X,'(Which is the first number on the I2 Card) ', \)
*
         READ(*,*) NNPE
*
         npoll = 0
*
***************************************************************************
*
*	Set up DO WHILE to loop through file and read in each line:
*
C
        DO 1000 Istep = 1, 530000
*
*
            DO 200 K = 1, NNPE
*
             READ(1,ERR=2000, IOSTAT=nerr)  QO(K)
*
  200       CONTINUE
*       
*
 2000       WRITE(2,210) (QO(K),K=1,NNPE)
*
  210       FORMAT(80(F8.3,2X))
*
            IF((nerr.NE.0).AND.(nerr.NE.-1).AND.(nerr.NE.6405)) THEN
*
               WRITE(*,220) nerr
  220 FORMAT( 10X,' Error #',I6,' Occurred:  Hit key to go -')
*
               READ(*,*) nothing
*
             ELSEIF(nerr.EQ.0) THEN
*
                  continue
*
              ELSE
*
                   STOP
*
              ENDIF

*            
*
*
 1000  CONTINUE
*
C
C
       CLOSE(1)
       CLOSE(2)
C
C
       STOP
       END
