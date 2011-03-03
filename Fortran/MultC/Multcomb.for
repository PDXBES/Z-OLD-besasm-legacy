C     Last change:  AMM   7 Aug 2000   11:39 am
      SUBROUTINE MULTCOMB(ICOMB)
C=======================================================================
C     Written November 1991 by Peter Jacobsen/CH2M HILL
c       for the Portland, OR CSO project.
c       This subroutine allows collating and combining.
C=======================================================================
      INCLUDE 'TAPES.INC'
      INCLUDE 'INTER.INC'
      INCLUDE 'STIMER.INC'
c      INCLUDE 'COMB.INC'
C=======================================================================
c       set max mumber of files to combine
      PARAMETER (MAXFLS=50)

CPDX--VCA  DOUBLE PRECISION DIFF,DELTM(MAXFLS),DELTAMIN,DIFFM(MAXFLS)
      REAL DELTM(MAXFLS),DELTAMIN
      DOUBLE PRECISION DIFF,DIFFM(MAXFLS)
      CHARACTER*10 KODEOT,
     &          KODEXM(MAXFLS,NIE),KODERM(MAXFLS,NIE),
     &          AJUNCM(MAXFLS,NIE)
      CHARACTER*60 FINTERM(MAXFLS)
      CHARACTER*80 NEWTITLE(2)
      INTEGER   FIRST,CURRENT
      INTEGER*2 INPOSM(MAXFLS,NIE)
      LOGICAL*1 DUPLICATE,OKM(MAXFLS),BEGIN(MAXFLS),ALLBEGIN,
     &          EXTRACT(MAXFLS,NIE),VIRGIN(MAXFLS,NIE)
      DIMENSION NUMXM(MAXFLS),NUMRM(MAXFLS),NODEXM(MAXFLS,NIE),
     &     IFFM(MAXFLS),NODERM(MAXFLS,NIE),
     &     LOCATM(MAXFLS),JUNCM(MAXFLS,NIE),
     &     TIMDAYM(MAXFLS),JULDAYM(MAXFLS),
     &     QOM(MAXFLS,NIE),QOLDM(MAXFLS,NIE),IDATEZM(MAXFLS),
     &     TZEROM(MAXFLS),QQO(NIE),SMALLDELT(MAXFLS),ADJUST(MAXFLS)
C=======================================================================
C        This Subroutine has two objectives:
C
C        ICOMB      => 10, collate data from multiple files
C                      11, combine data from multiple files
C=======================================================================

C=======================================================================
C     Data initialization.
C=======================================================================
      DUPLICATE = .FALSE.
      NUMENDED = 0
      DO 8 II = 1,MAXFLS
         DO 8 I = 1,NIE
            INPOSM(II,I) = 0
            VIRGIN(II,I) = .TRUE.
            IF(II.EQ.1) THEN
               QQO(I)     = 0.0
               QOLDM(II,I)= 0.0
            ENDIF
    8 CONTINUE
C=======================================================================
C>>>>>Read data group B1<<<<<<<<<<<<<<<<<<<<<<<
C=======================================================================
      READ(N5,*,ERR=888) CC,NEWTITLE(1)
      READ(N5,*,ERR=888) CC,NEWTITLE(2)
      WRITE(N6,660)         NEWTITLE(1),NEWTITLE(2)
C=======================================================================
C     NODEOT => Node number for output.
C     KODEOT => Node name   for output.
C=======================================================================
      IF(JCE.EQ.0) THEN
         READ(N5,*,ERR=888) CC,NODEOT,NPOLL
         WRITE(N6,410)         NODEOT,JIN(IOUTCT)
      ELSE
         READ(N5,*,ERR=888) CC,KODEOT,NPOLL
         WRITE(N6,411)         KODEOT,JIN(IOUTCT)
      ENDIF
      IF(NPOLL.NE.0) THEN
         WRITE(N6,400)
         WRITE(*,400)
         STOP
      ENDIF
C=======================================================================
C     Read COMBIN 'D' block input.
C=======================================================================
C>>>>>Read data group D0<<<<<<<<<<<<<<<<<<<<<<<
c       determine the number of files to use
C=======================================================================
      READ(N5,*,ERR=888) CC,NUMFILES
      WRITE(N6,100)NUMFILES
      IF(NUMFILES.GT.MAXFLS)THEN
         WRITE(N6,99)MAXFLS
         WRITE( *,99)MAXFLS
 99      FORMAT(' Error - Too many files: ',I4,' allowed.')
         STOP
      ENDIF
 100  FORMAT(/,' Number of interchange files is...',I9,//)
C=======================================================================
c               determine filenames, read @ (unit #) (filename) 
c                       (smallest time step)

      DO 110, II=1,NUMFILES
         READ(N5,*,ERR=888) CC,IFFM(II),FINTERM(II),SMALLDELT(II)
         WRITE(N6,425) FINTERM(II)
         IF(IFFM(II).EQ.5.OR.IFFM(II).EQ.6.OR.IFFM(II).EQ.55
     &          .OR.IFFM(II).EQ.N5 .OR. IFFM(II).EQ.N6)
     &          CALL ERRORSW(117)
         OPEN(UNIT=IFFM(II), FILE=FINTERM(II), FORM='UNFORMATTED',
     &          STATUS='OLD')

C=======================================================================
C>>>>>Read data group D1<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
c               determine number of nodes to be extracted,
c               and number of node to be renumbered -- for each file
c               same format as C1
C=======================================================================
         READ(N5,*,ERR=888) CC,NUMXM(II),NUMRM(II)
         WRITE(N6,430)         NUMXM(II),NUMRM(II)
         IF(NUMRM(II).GT.0 .AND. NUMXM(II).NE.NUMRM(II))THEN
            WRITE(N6,405)
            WRITE(*,405)
            STOP
         ENDIF
C=======================================================================
C>>>>>Read data group D2<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
c               list of nodes to be extracted
c               same format as C2
C=======================================================================
      IF(NUMXM(II).GT.0) THEN
         WRITE(N6,440)
         IF(JCE.EQ.0) THEN
            READ(N5,*,ERR=888) CC,(NODEXM(II,I),I=1,NUMXM(II))
            WRITE(N6,450)         (NODEXM(II,I),I=1,NUMXM(II))
         ELSE
            READ(N5,*,ERR=888) CC,(KODEXM(II,I),I=1,NUMXM(II))
            WRITE(N6,451)         (KODEXM(II,I),I=1,NUMXM(II))
         ENDIF
      ENDIF
C=======================================================================
C>>>>>Read data group D3<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
c               new id's of nodes to be renumbered
C=======================================================================
         IF(NUMRM(II).GT.0) THEN
            WRITE(N6,445)
            IF(JCE.EQ.0) THEN
               READ(N5,*,ERR=888) CC,(NODERM(II,I),I=1,NUMRM(II))
               WRITE(N6,450)         (NODERM(II,I),I=1,NUMRM(II))
            ELSE
               READ(N5,*,ERR=888) CC,(KODERM(II,I),I=1,NUMRM(II))
               WRITE(N6,451)         (KODERM(II,I),I=1,NUMRM(II))
            ENDIF
         ENDIF
 110  CONTINUE
C#######################################################################
c               end of command data read
C#######################################################################




C=======================================================================
C     Read file headers from each file
C           LOCATM(II) = Total number of inlets on ii input data set.
C=======================================================================

      SUMTRIBA = 0.0
      DO 210, II=1,NUMFILES
         WRITE(N6,9005) FINTERM(II)
         CALL INFACE(1,IFFM(II))
         LOCATM(II)= LOCATS
         IF(NPOLL.NE.0)THEN
            WRITE(N6,400)
            WRITE(*,400)
            STOP
         ENDIF

         DO 205 I = 1,LOCATM(II)
            IF(JCE.EQ.0) JUNCM(II,I) = NLOC(I)
            IF(JCE.EQ.1) AJUNCM(II,I) = KAN(I)
  205    CONTINUE

         IDATEZM(II) = IDATEZ
         TZEROM(II)  = TZERO
         SUMTRIBA = SUMTRIBA + TRIBA
  210 CONTINUE
      TRIBA = SUMTRIBA

C=======================================================================
C     Read the first line of the interface files and
C       determine if the time of the file needs to be adjusted to get
c       back in synch with the standard times.
C       ANINT is FORTRAN's intrinsic rounding function
C=======================================================================

      DO 230, II=1,NUMFILES
         READ(IFFM(II)) JULDAYM(II),TIMDAYM(II),DELTM(II),
     &          (QOM(II,I),I=1,LOCATM(II))
         ADJUST(II)=ANINT(TIMDAYM(II)/SMALLDELT(II))*SMALLDELT(II)
     &          -TIMDAYM(II)
 230  CONTINUE
C=======================================================================
C     Determine TZERO and IDATEZ for the new interface file.
C     Find earliest time...
C=======================================================================

         FIRST  = 1
         IDATEZ = JULDAYM(1)
         TZERO  = TIMDAYM(1)+ADJUST(1)
         JULDAY = JULDAYM(1)
         TIMDAY = TIMDAYM(1)+ADJUST(1)

      DO 240, II=2,NUMFILES
         CALL NTIME(JULDAYM(II),TIMDAYM(II)+ADJUST(II),DIFF)
         DIFFM(II)=DIFF
         IF(DIFF.LT.0.0) THEN
C               means ii is earlier than julday/timday
            FIRST  = II
            IDATEZ = JULDAYM(II)
            TZERO  = TIMDAYM(II)+ADJUST(II)
            JULDAY = JULDAYM(II)
            TIMDAY = TIMDAYM(II)+ADJUST(II)
         ENDIF
 240  CONTINUE


C=======================================================================
c       first, initialize the extract flag
c               if not going to extract from this file (that is,
c               use all the nodes) set all the flags to true.

c               if going to extract from this file (that is, use
c               a subset of the nodes) set the flag to false.
C=======================================================================

      DO 330, II=1,NUMFILES
         IF(NUMXM(II).GT. 0) THEN
            DO 310, I=1,LOCATM(II)
 310           EXTRACT(II,I)=.FALSE.
         ELSE
            DO 320, I=1,LOCATM(II)
 320           EXTRACT(II,I)=.TRUE.
         ENDIF
 330  CONTINUE

C=======================================================================
c      for each file ii, go thru list of existing id's
c      if matches extracted node id, then set extract flag
c      then, if renumbering is called for that file, renumber the node
C=======================================================================

      DO 370, II=1,NUMFILES
         IF (NUMXM(II).GT.0) THEN
            DO 360, I=1,LOCATM(II)
               DO 350, IX=1,NUMXM(II)
                  IF(JCE.EQ.0)THEN
                     IF(JUNCM(II,I).EQ.NODEXM(II,IX))THEN
                        EXTRACT(II,I)=.TRUE.
                        IF(NUMRM(II).GT.0) JUNCM(II,I)=NODERM(II,IX)
                     ENDIF
                  ELSE
                     IF(AJUNCM(II,I).EQ.KODEXM(II,IX))THEN
                        EXTRACT(II,I)=.TRUE.
                        IF(NUMRM(II).GT.0) AJUNCM(II,I)=KODERM(II,IX)
                     ENDIF
                  ENDIF
 350           CONTINUE
 360        CONTINUE
         ENDIF
 370  CONTINUE


C=======================================================================
C     If ICOMB equals ten (collate multiple files),
C     it is necessary to interleave the inlet numbers from the input
C     files.  If two files have the same inlet number
C     in their inlet array their flow values will
C     be added together.  If the inlet number appears on only one file
C     the flow values will be transfered to the
C     new interface file from that input file.
C=======================================================================
C    The new interface file's inlet numbers will be arranged in the
C        following order: first --> inlets in multiple input files
C                         second--> those inlets 
C                                   not already transfered
C=======================================================================
C     First, find the INLET locations that are on multiple 
c     interface files.
C=======================================================================

         NPOSIT = 1
         DO 585 IIA=1,NUMFILES-1
            DO 580 IA = 1,LOCATM(IIA)
               IF(EXTRACT(IIA,IA) .AND. VIRGIN(IIA,IA)) THEN
c                  DO 575 IIB = IIA+1,NUMFILES
                  DO 575 IIB = IIA,NUMFILES
                     DO 550 IB = 1,LOCATM(IIB)
                        IF(IIA.NE.IIB .OR. IA.NE.IB) THEN
                           IF(EXTRACT(IIB,IB) .AND. VIRGIN(IIB,IB)) THEN
                              IF(JCE.EQ.0) THEN
                                 IF(JUNCM(IIA,IA).EQ.JUNCM(IIB,IB)) THEN
                                    DUPLICATE=.TRUE.
                                    VIRGIN(IIA,IA)=.FALSE.
                                    VIRGIN(IIB,IB)=.FALSE.
                                    INPOSM(IIA,IA) = NPOSIT
                                    INPOSM(IIB,IB) = NPOSIT
                                    NLOC(NPOSIT) = JUNCM(IIA,IA)
                                 ENDIF
                              ELSE
                                IF(AJUNCM(IIA,IA).EQ.AJUNCM(IIB,IB))THEN
                                    DUPLICATE=.TRUE.
                                    VIRGIN(IIA,IA)=.FALSE.
                                    VIRGIN(IIB,IB)=.FALSE.
                                    INPOSM(IIA,IA) = NPOSIT
                                    INPOSM(IIB,IB) = NPOSIT
                                    KAN(NPOSIT) = AJUNCM(IIA,IA)
                                  ENDIF
                              ENDIF
                           ENDIF
                        ENDIF
  550                CONTINUE
  575             CONTINUE
                  IF(DUPLICATE) THEN
                     NPOSIT = NPOSIT + 1
                     DUPLICATE=.FALSE.
                  ENDIF
               ENDIF
               IF(DUPLICATE) THEN
                  NPOSIT = NPOSIT + 1
                  DUPLICATE=.FALSE.
               ENDIF
  580       CONTINUE
  585    CONTINUE
         LOCATS = NPOSIT-1

C=======================================================================
C     Then, find the INLET locations that are only on one interface file
C=======================================================================

         DO 630, II=1,NUMFILES
            DO 625 I = 1,LOCATM(II)
               IF(EXTRACT(II,I) .AND. VIRGIN(II,I)) THEN
                  DO 600 N = 1,LOCATS
                     IF(JCE.EQ.0)THEN
                        IF(NLOC(N).EQ.JUNCM(II,I)) GO TO 625
                     ELSE
                        IF(KAN(N).EQ.AJUNCM(II,I)) GO TO 625
                     ENDIF
  600             CONTINUE
                  VIRGIN(II,I) = .FALSE.
                  IF(JCE.EQ.0)THEN
                     NLOC(NPOSIT) = JUNCM(II,I)
                  ELSE
                     KAN(NPOSIT) = AJUNCM(II,I)
                  ENDIF
                  INPOSM(II,I) = NPOSIT
                  NPOSIT = NPOSIT + 1
               ENDIF
  625       CONTINUE
  630    CONTINUE
         LOCATS = NPOSIT-1

C=======================================================================
C     The next if/then statement is one of two differences
c         between the combine program and the collate program.
c         The combine program has the same program input as the
c         collate program except that summation sequence deposits
c         the entire flow values into inlet - NODEOT.
C=======================================================================

      IF(ICOMB.EQ.11) THEN
         IF(JCE.EQ.0) THEN
            IF(NODEOT.LE.0)   NODEOT =  12345
            NLOC(1) = NODEOT
         ELSE
            IF(KODEOT.EQ.' ') KODEOT = '12345'
            KAN(1) = KODEOT
         ENDIF
         LOCATS  = 1
      ENDIF

C=======================================================================
C     Write the header information on the new interface file.
C=======================================================================
      IF(NEXT.GT.0) THEN
         REWIND NEXT
         WRITE(NEXT) LOCATS,NPOLL
         IF(JCE.EQ.0) THEN
            WRITE(NEXT) (NLOC(I),I=1,LOCATS)
         ELSE
            WRITE(NEXT)  (KAN(I),I=1,LOCATS)
         ENDIF
         SOURCE = 'MULTI-COMBINE BLOCK'
         TITLE(1)=NEWTITLE(1)
         TITLE(2)=NEWTITLE(2)
         WRITE(N6,9015)   
         CALL INFACE(2,NEXT)
         CALL INFACE(1,NEXT)
      ENDIF

c               end of header info
c^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
c^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
c^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

c               now work with time and flow data

C=======================================================================
C     Initialize
C=======================================================================
      DO 700, II=1,NUMFILES
 700     OKM(II) = .FALSE.
      BEGIN(II) = .FALSE.
      CURRENT    = FIRST

C=======================================================================
C     Do loop for reading all input data for each time step.
C=======================================================================

      DO 999 KDT = 1,1000000

C-----------------------------------------------------------------------
C     First initialize calculated results array QQO for this time step.
C-----------------------------------------------------------------------
      DO 750 N=1,NPOSIT-1
  750 QQO(N) = 0.0

C-----------------------------------------------------------------------
c               check if beginning time of file is greater than
c               the current time
C-----------------------------------------------------------------------
      IF(.NOT. ALLBEGIN) THEN
         JULDAY = JULDAYM(CURRENT)
         TIMDAY = TIMDAYM(CURRENT)+ADJUST(CURRENT)
         DO 752, II=1,NUMFILES
            IF(.NOT.BEGIN(II))THEN
               CALL NTIME(JULDAYM(II),TIMDAYM(II)+ADJUST(II),DIFF)
               DIFFM(II)=DIFF
               IF(DIFF .EQ. 0) THEN
                  BEGIN(II) = .TRUE.
                  NUMOPEN = NUMOPEN + 1
                  IF(NUMOPEN.GE.NUMFILES) ALLBEGIN = .TRUE.
               ENDIF
            ENDIF
 752     CONTINUE
      ENDIF

C-----------------------------------------------------------------------
c                  find smallest delta of all files that are at this
c                  time step and see if the read files are at the 
c                  current time step
C-----------------------------------------------------------------------

      JULDAY = JULDAYM(CURRENT)
      IF (JULDAY .EQ. 99999) GOTO 1020
      TIMDAY = TIMDAYM(CURRENT)+ADJUST(CURRENT)
      DELTAMIN = DELTM(1)
      DO 760, II=1,NUMFILES
         CALL NTIME(JULDAYM(II),TIMDAYM(II)+ADJUST(II),DIFF)
         IF(DIFF .EQ. 0) THEN
            OKM(II) = .TRUE.
            IF(DELTM(II) .LT. DELTM(CURRENT)) THEN
               DELTAMIN = DELTM(II)
               CURRENT = II
            ENDIF
         ELSE
            OKM(II)=.FALSE.
         ENDIF
 760  CONTINUE
      JULDAY = JULDAYM(CURRENT)
      TIMDAY = TIMDAYM(CURRENT)+ADJUST(CURRENT)
      IF (JULDAY .EQ. 99999) GOTO 1020

c         calculate flows for this time step, i.e., add flows together

      
           DO 780, N=1, NPOSIT-1
              DO 770, II=1, NUMFILES
                 DO 765, I=1,LOCATM(II)
                    IF (INPOSM(II,I).EQ. N)THEN
                       IF (OKM(II)) THEN
                          QQO(N) = QQO(N) + QOM(II,I)
                       ELSE
                          IF(BEGIN(II)) THEN
c                            interpolate!
C                            find the time difference
                             CALL NTIME(JULDAYM(II),
     &                             TIMDAYM(II)+ADJUST(II),DIFF)
             
                             QQO(N) = QQO(N) + QOLDM(II,I)
     &                        + (QOM(II,I)
     &                        -  QOLDM(II,I))
     &                        * (1-(DIFF/DELTM(II)))
                          ENDIF
                       ENDIF
                    ENDIF
 765             CONTINUE
 770          CONTINUE
 780       CONTINUE

C=======================================================================
C     Combine all the input locations into one output location.
C=======================================================================
      IF(ICOMB.EQ.11) THEN
         QTEMP = 0
         DO 950 N=1,NPOSIT-1
  950       QTEMP = QTEMP + QQO(N)
         QQO(1)=QTEMP
      ENDIF

C-----------------------------------------------------------------------
C     Write the actual interface data line.
C-----------------------------------------------------------------------

      WRITE(NEXT) JULDAY,TIMDAY,DELTAMIN,(QQO(N),N=1,LOCATS)

C-----------------------------------------------------------------------
C     Store the old values and read the new values
C-----------------------------------------------------------------------

 790  DO 800, II=1,NUMFILES
         IF(JULDAYM(II).NE.99999 .AND. OKM(II))THEN
            IF(BEGIN(II)) THEN
c                  put flow from previous time step into QOLD array
               DO 805, I=1,LOCATM(II)
 805              QOLDM(II,I)=QOM(II,I)
c                  read new values for flows
                  READ(IFFM(II),END=1000,IOSTAT=IOCHECK) 
**                  READ(IFFM(II),END=1000) 
     &               JULDAYM(II),TIMDAYM(II),
     &               DELTM(II),(QOM(II,I),I=1,LOCATM(II))
** 1000               CONTINUE
 1000             IF(IOCHECK.EQ.-1)THEN
C                                       End of file ii is reached.
                     WRITE(N6,525) IFFM(II), FINTERM(II),JULDAYM(II),
     +                             TIMDAYM(II)
                     WRITE(*,525) IFFM(II), FINTERM(II),JULDAYM(II),
     +                             TIMDAYM(II)
*
 525   FORMAT(/,2X,'>> NOTE:  End of file reached for file #: ',I2,
     +  '  filename: ',A12,/,8X,'Last read JULDAY of ',I8,2X,
     +  'AT TIME: ',F10.0)
*
                     JULDAYM(II) = 99999
                     TIMDAY = 0.0      
                     OKM(II) = .FALSE.
                     BEGIN(II) = .FALSE.
                     NUMENDED = NUMENDED + 1
                     IF(NUMENDED.EQ.NUMFILES) GO TO 1020
                     DO 1050 I = 1,LOCATM(II)
                        QOM(II,I)    = 0.0
                        QOLDM(II,I)  = 0.0
 1050                CONTINUE                          
                  ENDIF
               IF(IOCHECK.GT.0)THEN
                  WRITE(N6,545) IOCHECK,IFFM(II), FINTERM(II),
     +                          JULDAYM(II),TIMDAYM(II)
                  WRITE(*,545) IOCHECK,IFFM(II), FINTERM(II),
     +                          JULDAYM(II),TIMDAYM(II)
 545   FORMAT(3X,'>> WARNING!!  ERROR #',I6,' OCCURRED IN FILE #: ',
     +  I2,'  FILENAME: ',A12,/,8X,'LAST READ JULDAY OF ',I8,2X,
     +  'AT TIME: ',F10.0)
*                  WRITE(*,*) ' IOSTAT = ', IOCHECK
                  STOP
               ENDIF
            ENDIF
         ENDIF   
  800 CONTINUE

  999 CONTINUE

C=======================================================================
C               End-of-files reached with all files
C=======================================================================

 1020 DO 2000, II=1,NUMFILES
 2000    CLOSE(UNIT=IFFM(II))
      WRITE(N6,9000)
      WRITE(*,9000)
      RETURN
  888 CALL IERROR

C=======================================================================
C=======================================================================
C=======================================================================

  400 FORMAT(/,' Error - Multiple combine/collate does not allow '
     &             'quality constituent data.')
  405 FORMAT(/,' Error - NUMRM(ii) must equal NUMXM(ii) or zero.')
  410 FORMAT(/,' Output node number is............',I9,//,
     1         ' Output data-set unit number is...',I9,//)
  411 FORMAT(/,' Output node name is..............',A9,//,
     1         ' Output data-set unit number is...',I9,//)
  425 FORMAT(/,' For interchange file: ',A30)
  430 FORMAT(/,' Locations to be extracted (NUMXM)',I9,/,
     .         ' Locations to be renumbered (NUMRM)',I8)
  440 FORMAT(/,
     +' #########################################',/,
     +' # The following nodes will be extracted #',/,
     +' #     from the interchange file:        #',/,
     +' #########################################',/)
  445 FORMAT(/,
     +' ##################################################',/,
     +' # The following numbers are the new node numbers #',/,
     +' # assigned to the extracted/renumbered nodes.    #',/,
     +' ##################################################',/)
  450 FORMAT(1X,10I10)
  451 FORMAT(1X,10A10)
  455 FORMAT(///,' Ending time in seconds for file 1 --->',F15.2,
     .        /, ' Ending time in seconds for file 2 --->',F15.2)
  660 FORMAT(1H1,4X,A80,/,5X,A80)
 9000 FORMAT(/,' ===> Combine Block ended normally.')
 9005 FORMAT(/,
     +' ****************************************************',/,
     +' *  Reading information from the interchange file:  *',/,
     &' *',2X,A48,                                         '*',/,
     +' ****************************************************')
 9015 FORMAT(/,
     +' *******************************************',/,
     +' *  Writing information on the JOUT file.  *',/,
     +' *******************************************')
      END
