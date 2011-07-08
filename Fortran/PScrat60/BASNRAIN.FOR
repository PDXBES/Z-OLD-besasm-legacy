       SUBROUTINE BASNRAIN
C    
C    
C      The purpose of this program is to develope lumped or weighted basin
C      rainfall files for the CSO basins.  The weighting is based on
C      the squared distance from the centroid of the basin to each rainfall
C      gage.  
C      The program will determine if the data being read into the program 
C      is from raingages that have been moved.  If so, the new coordinates
C      are used for the rainfall data corresponding to the new location.
C      The program will setup a flag at those points of interest in the basin 
C      file to show the dates raingages were moved in the basin file.
C
C      Also the program will adjust the rainfall data to account for missing
C      data or when rainfall at some gages did not occur.  
C
C	Program Updated May 7, 1995:  Fix lost rain after mis-matched times
C
C**********************************************************************
C
C     The following variables are defined as follows.
C  
C     basin = The basin name for which data files will be opened
C     bcent = The variable name for the centroids that are within a basin  
C         d = The variable name used for the calculation of the distances 
C  filenami = The input name for the data file to be read in initially
C    fnamei = The input name for the data file to be read in (*.dat)
C    fnameo = The output name for the data file (*.DAT)  
C    infile = The name of the file to be used as the input file 
C    nbasin = The basin number for which data files will be opened
C     ndate = The date in which a raingage was moved
C      nday = The day that is read in from the data file
C    nfiles = The file number in which the rainfall gage corresponds with 
C        ng = The number gauges for each basin at a time 
C     ngage = The number of gauges for basins 1-NB 
C     nhour = The hour that is read in from the data file
C    nqrain = The depth of rainfall that occurred in a period (units = in.)  
C      nqtr = The quarters in a particular hour an event can occur
C     nqtrs = The variable name for checking that the exact quarter for
C             each gage is being read 
C     nrank = The ranking subrountine variable which will hold and sort the 
C             rainfall data to read the data in the rainfall files    
C      nrow = The variable name which relates the gage in a row to the 
C             same gage in a particular basin     
C     rcent = The variable name for the rainfall gage coordinates
C    rfiles = The file name in which the rainfall gage corresponds with 
C       wpr = The variable name for the weighted percent rainfall calculated
C**********************************************************************
C
C
      DIMENSION nbasin(40),ngage(40),ng(40,5),nfiles(24),basin(40),
     &          rfiles(24),bcent(40,2),rcent(24,4),ndate(24),d(5),
     &          wpr(5),nrow(40,5),nyr(5),nday(5),nqrain(5),nqtrs(5),
     &          nrank(5,2),nhour(5),nqtr(5), nbatch(5), ntest(5)
C
      INTEGER*1 nhour,nqtr
      INTEGER*2 nbasin,ngage,ng,pctrain,nrow,nday,nqrain,nyr
C
      INTEGER RAINMIN, DIFF
      CHARACTER filenami*240,rfiles*48
      CHARACTER*240 fnameo
      CHARACTER*8 basin
      CHARACTER*80 title
      DOUBLE PRECISION wpr, twpr
      REAL AvgDist
C
      LOGICAL OK
*
C**********************************************************************
*		Write Header Label:
*
      WRITE(*,2000)
*
*
C**********************************************************************
C     Get input file name
C**********************************************************************
C
  5   WRITE(*,10)
 10   FORMAT(\\\,10X,'PLEASE TYPE THE INPUT FILE NAME:   ')
      READ(*,20) filenami
 20   FORMAT(A12)
C
      INQUIRE(FILE=filenami,EXIST=OK)
      IF(.NOT. OK)THEN
        WRITE(*,3000)
        GO TO 5
        ENDIF
*
C**********************************************************************
C     Get the input file started
C
C      This next set of statements will be a do loop that will access 
C      the different basins one at a time  and input their respective
C      gauges into the qrain1 program one basin file at a time.     
C
C**********************************************************************   
C     Open input file and read nb and ngage(I) 
C**********************************************************************
*
      OPEN(1,FILE=filenami)
      REWIND(1)
**      OPEN(40,FILE='DEBUG.OUT')
C
C**********************************************************************
C
C	READ BASIN DATA
C
C**********************************************************************
C
       READ(1,*) nb, ntype
* 25   FORMAT(I2)  
C
       DO 35 I = 1,NB
C
          NF = 1
C           
          READ(NF,*,ERR=130,IOSTAT=ierr) nbasin(I),basin(I),ngage(I),
     &                        (ng(I,nbg),nbg=1,4),bcent(I,1),
     &                        bcent(I,2)
C           
*          WRITE(40,*)  nbasin(I),basin(I),ngage(I),
*     &                        (ng(I,nbg),nbg=1,4),bcent(I,1),
*     &                        bcent(I,2)

 35    CONTINUE
C
C**********************************************************************
C
C		READ RAINFALL GAGE DATA
C
C**********************************************************************
C
       READ(1,*) NGF 
** 40   FORMAT(1X,I2)
C
       DO 50 N = 1,NGF
C       
           NF = 1

           READ(NF,*,ERR=130,IOSTAT=ierr) nfiles(n),rfiles(n),
     &       rcent(n,1),rcent(n,2),NDATE(n),rcent(n,3),rcent(n,4)           
C
**           WRITE(40,*) nfiles(n),rfiles(n),
**     &       rcent(n,1),rcent(n,2),NDATE(n),rcent(n,3),rcent(n,4)           
C
 50     CONTINUE 
C
      CLOSE(1)
C
C**********************************************************************
C
C	We are now done with the input file
C
C**********************************************************************
C
C**********************************************************************
C 
C     Begin loop through each basin to open the basin specific rainfall
C     gages and calculated the weighted rainfall.
C
C**********************************************************************
C     
      DO 250 I = 1,nb
C   
C**********************************************************************
C     Open output file
C**********************************************************************
C
         fnameo = basin(I) // '.DAT' 
C
         OPEN(2,FILE=fnameo) 
C
        WRITE(*,52) Basin(I)
C
 52   FORMAT(/,' Processing Basin: ',A8)       
C**********************************************************************
C
          DO 70 N = 1,NGAGE(I)
             NF = N + 6
C 
             DO 65 J = 1,NGF
               IF(ng(I,n).EQ.NFILES(J)) THEN
C
               nrow(I,n)=J 
C
*
**************** TEST FOR EXISTENCE OF RAINFALL FILE:
*
C
               INQUIRE(FILE=RFILES(J),EXIST=OK)
                 IF(.NOT. OK)THEN
                   WRITE(*,3005) RFILES(J)
                   STOP
                 ENDIF
*
               OPEN(UNIT=NF,FILE=RFILES(J))
C
                    REWIND(NF)
*
               WRITE(*,55) rfiles(J)
C
 55         FORMAT('   Opened File: ',A12)       
C
C**********************************************************************
C
C     Verifies if the data being read in is from a raingage that has  
C     been moved; then setup flags at those particular points of interest 
C     to show that date which the raingage was moved. 
C
C**********************************************************************
C
      SELECT CASE (nfiles(J))
C
      CASE (4,10,13,19,24,25)
C 
*        WRITE(2,60) nfiles(J),ndate(J),(rcent(J,NC),NC=1,4) 
C
* 60   FORMAT(/,5X,'==> GAUGE ',I2,'  THE DATA IS FROM NEW LOCATION:',
*     &     '  DATE REINSTALLED  ', I5 ,'  THE OLD GAUGE COORDINATES ',
*     &     'ARE  ',2(2X,F5.2),'  THE NEW GAUGE COORDINATES  ',
*     &     2(1X,F5.2),/)  
C
      END SELECT
C
C**********************************************************************
C
C
               ENDIF
C
 65         CONTINUE
C
 70      CONTINUE
C
C	Initialize nerr for next Basin Rainfall Gages:
C
            nerr = 0
            nbatch(1) = 0
            nbatch(2) = 0
            nbatch(3) = 0
            nbatch(4) = 0
C
C  Primary Loop through all times steps:
C
  80    DO WHILE (nerr.LE.0)
C
*		Initialize minimum distance variables:
                DISTMIN = 99999999.
                NODEMIN = 0
                RAINMIN = 0
*
*
         DO 90 n = 1,NGAGE(I)
C
               nf = n + 6
C
C**********************************************************************
C           Begin to read one line at a time and check that
C           each gauge is at the same 15-minute period   
C**********************************************************************
*
******    Test for type of File:  1 = RAIN_BATCH, 2 = Column Format
*
        IF(ntype.GT.1) THEN
*
*				Old Hydra Format:

          READ(NF,85,ERR=110,IOSTAT=nerr,END=220) nstatn, nyr(n),
     &                       nday(n),nhour(n),nqtr(n),nqrain(n)
C
  85     FORMAT(I2,I2,I3,I2,I1,I3)
*
        ELSE
*
*              RAIN_BATCH Format:
***            Test if this is the first time step:
*
             IF(nbatch(n).LT.1) THEN
                DO 32 J = 1,24
                    READ(NF,86,ERR=110,IOSTAT=nerr,END=220) title
   86               FORMAT(A80)
   32             CONTINUE
                nbatch(n) = 2
             ENDIF
*
             READ(NF,*,ERR=110,IOSTAT=nerr,END=220) NYRB,nday(n),
     +       FDAY,nhour(n),MIN,nqrain(n)

                 nstatn = 1
                 nyr(n) = NYRB - 1900
                 nqtr(n) = (MIN+1)/15
*
         ENDIF
*
C**********************************************************************
C     Calculate the number of leap years since 1/1/76.
C**********************************************************************
C
                 nleaps = INT((nyr(n)-73)/4)
C
C**********************************************************************
C    Now calculate the hour number:
C**********************************************************************
C
        nhours = ((nyr(n)-76)*365 + nleaps + nday(n) - 1)*24 + nhour(n)
C
C**********************************************************************
C    Now calculate the quarter hours:
C**********************************************************************
C
                nqtrs(n) = nhours * 4 + nqtr(n)
C  
 90      CONTINUE
C
C**********************************************************************
C             INITIALIZE THE FOLLOWING VARIABLES BEFORE STARTING 
C             CALCULATIONS FOR NEW TIME STEP
C
  95            NGFLAG = 1
                twpr = 0
             CumDist = 0 
             AvgDist = 0
C
C**********************************************************************
C
C
        IF(ngage(I).GT.1) THEN
*
*
           DO 100 N = 1,(NGAGE(I)-1)     
C
              IF(nqtrs(n).NE.nqtrs(n+1)) NGFLAG = -1
C
 100       CONTINUE  
C
          ENDIF
*              
      IF(NGFLAG.GT.0) THEN  
C
C**********************************************************************
C              ALL TIMES MATCH         
C**********************************************************************
C
C
        DO 105 N = 1,NGAGE(I)
C
           NF = N + 6
C      
C
C********************************************************************** 
C           THERE ARE TWO SETS OF COORDINATES; ADJUSTMENTS MUST BE MADE   
C           TO ACCOUNT FOR THE DATA TAKEN AFTER GAUGE WAS MOVED. THE
C           DATES ARE CHECKED BY MATCHING NDATE(nrow(I,n)) WITH NYR AND
C           NDAY IN RAINFALL FILES. THEREFORE COORDINATES MUST BE CHANGED
C           IN DISTANCE FORMULA TO PERFORM BASIN WEIGHTED RAINFALL 
C           CALCULATIONS ACCURATELY. THE NEW COORDINATES WILL BE INSERTED
C           INTO  THE DISTANCE FORMULA    
C**********************************************************************
C
      SELECT CASE (NFILES(nrow(I,n)))
C
      CASE (4,10,13,19,24,25)
C
C**********************************************************************
C           THE DATES WILL BE CHECKED FOR THE ABOVE FILES FOR THE DAY
C           IN WHICH THE RAINGAGE WAS MOVED MATCHES THE NDATE(J)            
C**********************************************************************     
C
           IF((ndate(nrow(I,n))/1000.LE.nyr(n)).AND.
     &         (ndate(nrow(I,n)) - nyr(n)*1000.LE.nday(n))) THEN      
C
C**********************************************************************
C              CHANGE THE COORDINATES
C**********************************************************************
C
              X3 = (BCENT(I,1) - RCENT(nrow(I,n),3))**2
              X4 = (BCENT(I,2) - RCENT(nrow(I,n),4))**2
               X = X3 + X4
            D(N) = SQRT(X)      
C
           ELSE
C
                 X1 = (BCENT(I,1) - RCENT(nrow(I,n),1))**2
                 X2 = (BCENT(I,2) - RCENT(nrow(I,n),2))**2         
                  X = X1 + X2  
               D(N) = SQRT(X)
C
            ENDIF           
C
      CASE DEFAULT
C
                 X1 = (BCENT(I,1) - RCENT(nrow(I,n),1))**2
                 X2 = (BCENT(I,2) - RCENT(nrow(I,n),2))**2         
                  X = X1 + X2  
               D(N) = SQRT(X)
C
      END SELECT
C
*	Test for minimum distance:
*
          IF(D(N).LE.DISTMIN) THEN
             DISTMIN = D(N)
             NODEMIN = N
             RAINMIN = nqrain(N)
*           WRITE(*,404) DISTMIN, RAINMIN, NODEMIN
*  404 FORMAT( '       DistMin =',F8.2,' RainMin= ',F8.2,'  NodeMin=',
*     +       I8)
**             IF(PCTRAIN.LT.RAINMIN) WRITE(40,434) nday(1),nhour(1),
**     +          nqtr(1),nqrain(1),DISTMIN, RAINMIN, 
**     +          NODEMIN, PCTRAIN
**           WRITE(40,434) DISTMIN, RAINMIN,NODEMIN
*           WRITE(40,406) BCENT(I,1), RCENT(nrow(I,n),1), BCENT(I,2), 
*     +                   RCENT(nrow(I,n),2)
* 406  FORMAT(2X,'BCENT(I,1)=',F5.2,' RCENT(N,1)=',F5.2,' BCENT(I,2)=',
*     + F5.2,'RCENT(N,2)=',F5.2)

          ENDIF
C
C**********************************************************************
C     NOW SUM THE CALCULATED INVERSE DISTANCES TO OBTAIN THE TOTAL 
C**********************************************************************
C
            CumDist = CumDist + 1/(D(N)**2.0)
            IF(n.GT.1) AvgDist = (CumDist/(n-1))**0.5
C
C**********************************************************************
C     THE WEIGHTED PERCENT RAINFALL CAN NOW BE CALCULATED USING THE 
C     TOTAL DISTANCE        
C**********************************************************************
C
           wpr(N) = (nqrain(n))/(D(N)**2.0)
**           wpr(N) = (nqrain(n))/(D(N))
C
           ntest(n) = nqrain(n)
C
C**********************************************************************    
C     TOTAL THE WEIGHTED BASIN RAINFALL TO CALCULATE THE ACTUAL WEIGHTED
C     PERCENT RAINFALL FOR EACH BASIN
C**********************************************************************   
C
            twpr   = twpr + wpr(n)
C
C
  105  CONTINUE
C
C
C**********************************************************************
C     WEIGHTED BASIN RAINFALL FOR EACH BASIN ON 100% BASIS 
C**********************************************************************
C 
            PCTRAIN = NINT(twpr/CumDist)
*
*	Now test the weighted rainfall against the nearest maximum rainfall
*	The purpose of this is to keep peak rainfalls from being dampened.
        IF(RAINMIN.GE.3) THEN

**           IF(PCTRAIN.LT.RAINMIN) PCTRAIN = RAINMIN
           IF(PCTRAIN.LT.RAINMIN) THEN 	! Adjust PCTRAIN to better match
					! RAINMIN 
              DIFF = RAINMIN - PCTRAIN
**
**
              NEWRAIN = PCTRAIN + DIFF*(1-(DISTMIN/AvgDist))
              IF(NEWRAIN.GT.RAINMIN) NEWRAIN = RAINMIN
              IF(NEWRAIN.GT.PCTRAIN) PCTRAIN = NEWRAIN
           ENDIF

*                WRITE(40,434) nday(1),nhour(1),
*     +          nqtr(1),nqrain(1),DISTMIN, RAINMIN, 
*     +          NODEMIN, PCTRAIN
* 434  FORMAT(3X,'Date/Time:',I6,I4,I2,I3,'MIN DIST =',F8.2,2X,
*     +          ' RAIN = ',I4,2X,' GAGE = ',I4,
*     + '  PCTRAIN=',I4)
        ENDIF
*        RAINMIN = 0
*
C     
C**********************************************************************
C
C       This portion of the program then writes out the data in the 
C       desired format for non-zero rainfall:
C**********************************************************************
C      
            IF(PCTRAIN.GT.0) WRITE(2,200) nyr(1),nday(1),nhour(1),
     +                          nqtr(1),pctrain
*               IF(pctrain.GT.0) THEN
**               WRITE(40,201) nday(1),nhour(1),nqtr(1),NGFLAG,
**     +                   (D(k),k=1,ngage(I)),
**     +                   (FLOAT(ntest(k)),k=1,ngage(I)),PCTRAIN
*               ENDIF
C
  200 FORMAT(I2,1X,I3,1X,I2,1X,I1,1X,I3)
**  201 format(4I3,9f10.2,'PCTRAIN = ',I4)
C             
C
C
C**********************************************************************   
C
      ELSE
C
C	This portion of the program works on the rainfall files when there
C	is a difference in time steps between the files.
C
C	The first DO LOOP places the values of nqtrs(n) into the ranking 
C	array:  nrank(n,2).  It also places the the file number (n) into
C	the nrank(n,1) to keep track of where the ranked value came from.
C
C**********************************************************************
C
* VCA 5/5/95:  Re-initialize twpr & CumDist
  700   twpr = 0
        CumDist = 0
        AvgDist = 0
C

        DO 720 n = 1, ngage(I)
C
*           WRITE(*,701) nfiles(n), nqrain(n)
*
*  701  FORMAT( 5X,' Rainfall for gage # ',I2,'  = ',I4)
*
           nrank(n,1) = n
           nrank(n,2) = nqtrs(n)
C
  720   CONTINUE
C**********************************************************************
C     Now rank the values of nqtrs(n) in ascending order
*C**********************************************************************
C
        DO 750 n1 = 1, (ngage(I)-1)
C
           DO 740 n2 = (n1+1),ngage(I)
C
              IF (nrank(n1,2).GT.nrank(n2,2)) THEN
C
                 nhold1      = nrank(n2,1)
                 nrank(n2,1) = nrank(n1,1)
                 nrank(n1,1) = nhold1
C
                 nhold2      = nrank(n2,2)
                 nrank(n2,2) = nrank(n1,2)
                 nrank(n1,2) = nhold2
C
              ENDIF
C
  740       CONTINUE
C
  750    CONTINUE
C
C
C**********************************************************************
C	Place the time step values into a set of "hold" variables to be
C	certain we do not overwrite them in the procedures below:
C**********************************************************************
C
        	nhldyr = nyr(nrank(1,1))
         	nhlday = nday(nrank(1,1))
                nhldhr = nhour(nrank(1,1))
                nhldqt = nqtr(nrank(1,1))
C
C**********************************************************************
C	Because the values are ranked in ascending order, we can calculate
C       the time difference (delta) from the lowest to the highest nqtrs by:
C**********************************************************************
C
          ndelta = nrank(ngage(I),2) - nrank(1,2)
C
C**********************************************************************
C	We now process each file.  For the files at the earliest time step
C	(smallest nqtrs), the weighted rainfall and distances are calculated.
C	For the files at later time steps, the rainfall and distances are set
C	equal to zero.
C**********************************************************************
C
       DO 800 n = 1,ngage(I)
C
          NF = N + 6
C**********************************************************************
C           THERE ARE TWO SETS OF COORDINATES; ADJUSTMENTS MUST BE MADE   
C           TO ACCOUNT FOR THE DATA TAKEN AFTER GAUGE WAS MOVED. THE DATES  
C           BEING USED MUST MATCH NDATE(nrow(I,n)) WITH NYR AND NDAY IN
C           RAINFALL FILES . THEREFORE THE COORDINATES MUST BE CHANGED 
C           IN DISTANCE FORMULA TO PERFORM BASIN WEIGHTED RAINFALL 
C           CALCULATIONS ACCURATELY. THE NEW COORDINATES WILL BE INSERTED
C           INTO  THE DISTANCE FORMULA    
C**********************************************************************
C
            SELECT CASE (NFILES(nrow(I,n)))
C
               CASE (4,10,13,19,24,25)
C
C**********************************************************************
C           THE DATES WILL BE CHECKED FOR THE ABOVE FILES FOR THE DAY
C           IN WHICH THE RAINGAGE WAS MOVED MATCHES THE NDATE(J)            
C**********************************************************************     
C
                  IF((ndate(nrow(I,n))/1000.LE.nyr(n)).AND.
     &              (ndate(nrow(I,n)) - nyr(n)*1000.LE.nday(n))) THEN
C
C**********************************************************************
C              CHANGE THE COORDINATES
C**********************************************************************
C
                     X3 = (BCENT(I,1) - RCENT(nrow(I,n),3))**2
                     X4 = (BCENT(I,2) - RCENT(nrow(I,n),4))**2
                     X = X3 + X4
                     D(N) = SQRT(X)      
C
                  ELSE
C
                     X1 = (BCENT(I,1) - RCENT(nrow(I,n),1))**2
                     X2 = (BCENT(I,2) - RCENT(nrow(I,n),2))**2         
                     X = X1 + X2  
                     D(N) = SQRT(X)
C
                  ENDIF           
C
               CASE DEFAULT
C
C**********************************************************************
C                   CALCULATE CENTROIDAL DISTANCE
C**********************************************************************
C
C
                     X1 = (BCENT(I,1) - RCENT(nrow(I,n),1))**2
                     X2 = (BCENT(I,2) - RCENT(nrow(I,n),2))**2         
                      X = X1 + X2  
                   D(N) = SQRT(X)
C
            END SELECT
C
C VCA 5/7/95:  Calculate AvgDist just in case it changes in time:
C              This is not really valid until all D(N) has been calculated.
C
           AvgDist = 0
        Do 630 NGN = 1, NGage(I)
           AvgDist = AvgDist + D(N)
  630   CONTINUE
           AvgDist = AvgDist / NGAGE(I)
*
*	Test for minimum distance:
*
          IF(D(N).LE.DISTMIN) THEN
             DISTMIN = D(N)
             NODEMIN = N
             RAINMIN = nqrain(N)
*           WRITE(*,434) DISTMIN, RAINMIN, NODEMIN
*           WRITE(40,434) DISTMIN, RAINMIN,NODEMIN
*                WRITE(40,434) nday(1),nhour(1),
*     +          nqtr(1),nqrain(1),DISTMIN, RAINMIN, 
*     +          NODEMIN, PCTRAIN
*
*           WRITE(40,406) BCENT(I,1), RCENT(nrow(I,n),1), 
*     +           BCENT(I,2), RCENT(nrow(I,n),2)

          ENDIF
C
C**********************************************************************
C     THE WEIGHTED PERCENT RAINFALL CAN NOW BE CALCULATED
C**********************************************************************
C
          IF (nqtrs(n).LE.nrank(1,2)) THEN
C
C**********************************************************************
C       This file matches the earliest time step and thus should be
C	correctly processed.
C**********************************************************************
C
            wpr(N) = (nqrain(n))/(D(N)**2.0)
**            wpr(N) = (nqrain(n))/(D(N))
C
            ntest(n) = nqrain(n)
*
C**********************************************************************
C	We are done calculating the distance and weighted rainfall
C	for the files which match the lowest time step.  For these
C	files, we need to read in the next time step data and calculate 
C	the new nqtrs(n).
C**********************************************************************
C
*
******    Test for type of File:  1 = RAIN_BATCH, 2 = Column Format
*
        IF(ntype.GT.1) THEN
*
*				Old Hydra Format:

*          READ(NF,85,IOSTAT=nerr,END=220) nstatn, nyr(n),
*     &                       nday(n),nhour(n),nqtr(n),nqrain(n)
C
          READ(NF,85,ERR=110,IOSTAT=nerr,END=220) nstatn, nyr(n),
     &                       nday(n),nhour(n),nqtr(n),nqrain(n)
C
*
        ELSE
*
*              RAIN_BATCH Format:
*
*             READ(NF,*,IOSTAT=nerr,END=220) NYRB,nday(n),
*     +            FDAY,nhour(n),MIN,nqrain(n)
*
             READ(NF,*,ERR=110,IOSTAT=nerr,END=220) NYRB,nday(n),
     +            FDAY,nhour(n),MIN,nqrain(n)
*
                 nstatn = 1
                 nyr(n) = NYRB - 1900
                 nqtr(n) = (MIN+1)/15
*
         ENDIF
*
C
C**********************************************************************
C     Calculate the number of leap years since 1/1/76.
C**********************************************************************
C
           nleaps = INT((nyr(n)-73)/4)
C
C**********************************************************************
C     Now calculate the hour number:
C**********************************************************************
C
       nhours = ((nyr(n)-76)*365 + nleaps + nday(n) - 1)*24 + nhour(n)
C
C**********************************************************************
C    Now calculate the quarter hours:
C**********************************************************************
C
                nqtrs(n) = nhours * 4 + nqtr(n)
C
C**********************************************************************
C	The files which match the lowest time step are now done.
C**********************************************************************
C
       ELSE
C
C**********************************************************************
C	    Process the files which are not in line with the current
C	    time step
C**********************************************************************
C
C**********************************************************************
C           Set the weighted rainfall = 0; and reduce the weighting
C	    of the D(N) for gages with missing data by increasing the distance
C**********************************************************************
C
            wpr(n) = 0
            ntest(n) = 0
C VCA 5/7/95            D(N)   = 2.0 * SQRT(X)
C  Try to re-set the missing gage distance to the maximum of the average distance
C  all of the gages or the gage's own distance
            D(N) = MAX(AvgDist,SQRT(X))
C
       ENDIF
C
*	Test for minimum distance:
*
          IF(D(N).LE.DISTMIN) THEN
             DISTMIN = D(N)
             NODEMIN = N
             RAINMIN = ntest(N)
*           WRITE(*,434) DISTMIN, RAINMIN, NODEMIN
*                WRITE(40,434) nday(1),nhour(1),
*     +          nqtr(1),nqrain(1),DISTMIN, RAINMIN, 
*     +          NODEMIN, PCTRAIN
*           WRITE(40,434) DISTMIN, RAINMIN,NODEMIN
*           WRITE(40,406) BCENT(I,1), RCENT(nrow(I,n),1), 
*     +            BCENT(I,2), RCENT(nrow(I,n),2)

          ENDIF

C
C**********************************************************************
C     Now for every file, calculate the summation of the distances
C**********************************************************************
C
            CumDist = CumDist + 1/(D(n)**2.0)
            IF(n.GT.1) AvgDist = (CumDist/(n-1))**0.5
C
C**********************************************************************    
C     TOTAL THE WEIGHTED BASIN RAINFALL TO CALCULATE THE ACTUAL WEIGHTED
C     PERCENT RAINFALL FOR EACH BASIN
C**********************************************************************   
C
            twpr   = twpr + wpr(n)
C
C
  800  CONTINUE
C
C**********************************************************************
C       We have examined all the files for this time step now, so
C	we can calculate the weighted basin rainfall and write it
C	to a file.
C
C**********************************************************************
C     WEIGHTED BASIN RAINFALL FOR EACH BASIN ON 100% BASIS 
C**********************************************************************
C 
            pctrain = NINT(twpr/CumDist)
C     
*	Now test the weighted rainfall against the nearest maximum rainfall
*	The purpose of this is to keep peak rainfalls from being dampened.
        IF(RAINMIN.GE.3) THEN
**           IF(PCTRAIN.LT.RAINMIN) PCTRAIN = RAINMIN
           IF(PCTRAIN.LT.RAINMIN) THEN 	! Adjust PCTRAIN to better match*
					! RAINMIN 

              DIFF = RAINMIN - PCTRAIN
**              WRITE(*,802) RAINMIN, PCTRAIN, CumDist, AvgDist
** 802  FORMAT(/,' RainMin=',F6.2,' PctRain=',F6.2,' CumDist=',E9.2,
**     +         ' AvgDist=',E9.2)
** VCA 8/1/97 AvgDist & CumDist are showing up as 0.00 - but why???
              NEWRAIN = PCTRAIN + DIFF*(1-(DISTMIN/AvgDist))
              IF(NEWRAIN.GT.RAINMIN) NEWRAIN = RAINMIN
              IF(NEWRAIN.GT.PCTRAIN) PCTRAIN = NEWRAIN
           ENDIF

***           IF(PCTRAIN.LT.RAINMIN) PCTRAIN = RAINMIN
*              WRITE(40,434) nday(1),nhour(1),
*     +          nqtr(1),nqrain(1),DISTMIN, RAINMIN, 
*     +          NODEMIN, PCTRAIN
*             IF(PCTRAIN.LT.RAINMIN) WRITE(40,434) DISTMIN, RAINMIN, 
*     +          NODEMIN, PCTRAIN
        ENDIF
*        RAINMIN = 0

*
C**********************************************************************
C       This portion of the program then writes out the data in the 
C       desired format.
C**********************************************************************
C
               IF(PCTRAIN.GT.0) WRITE(2,200) nhldyr,
     +                          nhlday,nhldhr,nhldqt,pctrain
*               IF(pctrain.GT.0) THEN
*                
**               WRITE(40,201) nhlday,nhldhr,nhldqt,NGFLAG,
**     +                   (D(k),k=1,ngage(I)),
**     +                   (FLOAT(ntest(k)),k=1,ngage(I)),PCTRAIN
*               ENDIF
*
C
C**********************************************************************
C	Now check if the time step difference between the lowest and
C	highest time step (ndelta) is significant.  If yes, continue
C	the process until all the time steps match up.
C**********************************************************************
C

        IF (ndelta.LT.1) Then
            GOTO 95   ! Test again if series are not matched
        Else
            GOTO 700  ! Re-order the times to be sure we don't miss any
        ENDIF

C**********************************************************************
C	We are now done with the mismatched files and can continue on
C	to the next time step for all the files.
C**********************************************************************
C
      ENDIF
C
C**********************************************************************
C       The following is the rainfall input read error statement.
C**********************************************************************
C        
 110    IF((nerr.LT.0).OR.(nerr.EQ.6405))THEN
C
C**********************************************************************
C         An expected end of file error, the program has successfully 
C         finished the transformation.
C**********************************************************************
C            
           WRITE(*,115) fnameo
C
C**********************************************************************
C       Unexpected error occurred, write message plus the error number 
C**********************************************************************
C
         ELSEIF(nerr.GT.0) THEN
           nunit = NF - 6
           WRITE(*,120) nerr, ng(i,n)
C
        ENDIF
C
 115  FORMAT(/,5x,'>>> Transformation Complete for File: ',A12,' <<<')
C
 120  FORMAT(//,5x,'==>> UNKNOWN ERROR!!!',/,10X,' Error # ',I4,
     + ' Occurred While Reading Raingage File #',I2,/)
C
       END DO
C   
C
*
  220    DO 230 JF = 7,(NGAGE(I) + 6)
C 
            CLOSE(JF)
C
 230     CONTINUE
*
*				The last line of data is written twice:  Delete repeated line:
*
            BACKSPACE(2)
            WRITE(2,*)
*
*				Done!
            CLOSE(2)
            CLOSE(40)
C
 250  CONTINUE
C   
C
C**********************************************************************
C        The following is the basin input read error statement 
C**********************************************************************
C        
C**********************************************************************
C   Unexpected error occurred, write message and filename 
C**********************************************************************
C
 130     IF(ierr.LT.0) THEN
           WRITE(*,135) ierr, filenami
         ELSEIF (ierr.GT.0)THEN
           WRITE(*,140) ierr, filenami
         ENDIF
C
C
 135  FORMAT(//,5x,'==>> END OF FILE OCCURRED!!!',/,10X,' Error # ',I4,
     + ' Occurred While Reading Input File: ',A12,/)
C
 140  FORMAT(//,5x,'==>> UNKNOWN ERROR!!!',/,10X,' Error # ',I4,
     + ' Occurred While Reading Input File: ',A12,/)
C
C
ccc      RETURN
      STOP
C
*
 2000  FORMAT( /,'   >>>>  CREATE VIRTUAL BASIN RAINFALL FILE '//)
 3000  FORMAT(/'   INPUT FILE NOT FOUND - TRY AGAIN!')
 3005  FORMAT(/' ERROR!!!  RAIN GAGE FILE ',A12,
     +         ' NOT FOUND - STOPPING PROGRAM!')
*
      END