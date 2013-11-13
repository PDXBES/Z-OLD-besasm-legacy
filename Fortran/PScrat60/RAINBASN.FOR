        SUBROUTINE RAINBASN
C***********************************************************************
C
C    The purpose of this subroutine is to read an ASCII rainfall file 
C    created by BASNRAIN.FOR and write the rainfall data to a SWMM 4.05
C    scratch file for use in SWMM RUNOFF.  This is done for any time step
C    increment greater than 1 minute.
C
C    The user must enter the name of the BASNRAIN data file to be converted,
C    the name of the interface file to be created, the beginning and ending
C    dates to be used.
C
C    The subroutine uses rainfall event variable names from programs written
C    by V. Adderley of CH2M HILL/PDX for the City of Portland's BES
C    CSO/SS Project.
C
C    Subroutine created 5/2/91 by Virgil Adderley, CH2M HILL, Portland OR.
C
C    Subroutine last updated: 8/19/92
C
C**************************************************************************
C    Declare variables 
C
      INTEGER*1     nhour, begnyr, endyr
      INTEGER*2     nday, begnday, enday
      CHARACTER*240  filein,fileout,rainfile
      DOUBLE PRECISION DELTA
C
*****************************************************************************
C
C     Get input file
C
       WRITE(*,10)
   10  FORMAT( /,10X,'PLEASE TYPE THE INPUT FILE NAME:  ',\)
       READ(*,15) filein
C
   15  FORMAT(A12)
C
*       WRITE(*,12)
*   12  FORMAT( /,10X,'PLEASE TYPE THE OUTPUT TABLE FILE NAME:  ',\)
*       READ(*,15) ftable
C
      OPEN(1,FILE=filein)
      REWIND(1)
C
*      OPEN(2,FILE=ftable)
*      REWIND(2)
C
       NTEMP = 18
       OPEN(NTEMP,FILE='DEBUG.OUT')
C
C	Create file table header:
C
*      WRITE(2,20)
C
*  20  FORMAT( /,4X,'FILE NAME',6X,'NHR',4X,'NMN',4X,'NDAY',2X,
*     &       'MONTH',2X,'IYRSTR',/)
C
******************************************************************************
C
C	Read the number of files to be processed:
C
      READ(1,*) nfiles
*
*	Now, set up DO LOOP to process each of the nfiles:
*
             DO 6000  nf = 1, nfiles
*
C
C	Read the name of the rainfall file as well as the 
C       name of the output file to be created.
C
       READ(1,*) rainfile, fileout
C
C
C  	Read the number of the rainfall station = basin number and the type
C       of file (15min ntype = 1, hourly ntype = 2)
C
      READ(1,*) nbasin, ntype
      IF(ntype.GT.30) ntype = 2
      IF(ntype.LT.30) ntype = 1

C
C	Now read the beginning date and ending date of period desired:
C
        READ(1,*) begnyr, begnday, endyr, enday
C
******************************************************************************
C
C	Calculate the initial parameters SWMM RUNOFF expects to see from the
C       rainfall interface file:
C
C		NSTA	=	Number of rainfall stations = 1
C		MRAIN	=	Number of rainfall points = hours in period
C		JSTA	=	Rainfall Station Number = basin number
C
        NSTA = 1
        MRAIN = INT(((endyr-begnyr)*365.25 + (enday-begnday)+1)*24)
C
C	Adjust for 15-minute rainfall files
C
        IF(ntype.GT.1) MRAIN = MRAIN * 4
C
        JSTA = nbasin
C
C
C	Open rainfall interface file and write the leading data:
C
        NOUT = 11
C
        OPEN(NOUT,FILE=fileout,FORM='UNFORMATTED')
*        OPEN(NOUT,FILE=fileout)
C
        REWIND(NOUT)
        WRITE(NOUT) NSTA, MRAIN, JSTA
*        WRITE(NOUT,*) NSTA, MRAIN, JSTA
C
******************************************************************************
C
C	Open the rainfall file:
C
        NRFILE = 13
C
        OPEN(NRFILE,file=rainfile)
*
        REWIND(NRFILE)
C
******************************************************************************
C
C	Initialize SWMM Variables for next file:
C
           JULDAY = 0
           TIMDAY = 0.
           DELTA  = 0.
c
C
C     Set up DO WHILE loop to read in each separate event from the 
C     event file:
C
 100   DO 5000 WHILE(nerr.EQ.0)
C
C	Read the next line of the event file:
C
C
        IF(ntype.LE.1) THEN
           READ(NRFILE,*,ERR=3800,IOSTAT=nerr) nyr,nday,nhour,
     &          min, nrain
           WRITE(NTEMP,*) nyr,nday,nhour,min,nrain
*
        ELSE
*
           READ(NRFILE,*,ERR=3800,IOSTAT=nerr) nyr,nday,nhour,nrain
*
        ENDIf
*
*			Error Trap for incorrect file format:
* 
        IF(nhour.GT.24 .OR. min.GT.4) THEN
            WRITE(*,*)
            WRITE(*,*) ' ERROR!! Time exceeds 24 hours per day or 60 ',
     +                 'minutes per hour!'
            WRITE(*,*) ' ---->   Check file type and format.'
            STOP
        ENDIF
*
*------------------------------------------------------------------
*
*	Test if data read in is before or after the desired period:
*
       IF((nyr.LT.begnyr).OR.(nyr.GT.endyr)) GOTO 5000
       IF((nyr.EQ.begnyr).AND.(nday.LT.begnday)) GOTO 5000
       IF((nyr.EQ.endyr).AND.(nday.GT.enday)) GOTO 5000
*
*		Otherwise, we are in the correct period.
*
******************************************************************************

                minute = min * 15
                IF(ntype.GT.1) minute = 0
C
C	   Calculate the intensity (in/hr) from the depth and time step
C
            ntensty = nrain * 4/(ntype**2)
C       
C
C		Assure that minute is >= 0
           IF(minute.LT.0) minute = 0
*      Check for hour = 24 and minute = 60
           IF(minute.GE.60) THEN
              minute = 0
              nhour = nhour + 1
           ENDIF
*
          IF(nhour.GE.24) THEN
              nhour = 0
              nday = nday + 1
          ENDIF
*
C
C----------------------------------------------------------------------------
C		nyr 	=  year of current time step
C               nday	=  julian day of current time step
C		nhour	=  hour of current time step
C               minute  =  minute of hour of current time step
C               nrain   =  rain over time step
C
C----------------------------------------------------------------------------
C
*	Skip over calculations if rainfall is zero:
*
          IF(nrain.LE.0.00001) GOTO 5000
*
C
C=============================================================================
C
C	Now check to see if this is the first time step in the storm/period.
C	If so, then write the name of the file and the beginning times
C	to file #2: ftable.
C
        IF(JULDAY.LE.0) THEN
C
C		This is the first time step of the storm.  Determine
C		what month of the year and day of the month that it
C		currently is:
C
*******************************************************************************
*
*	Determine if it is currently a leap year:
C
                declyr = (float(nyr))/4.
                intlyr = nyr/4
                ytest = declyr-intlyr
C
                IF(ytest.LT.0.1) THEN
C
*		      It is a leap year
                      lyday = 1
                ELSE
C
*                     It is not a leap year
                      lyday = 0
                ENDIF
C
C------------------------------------------------------------------------------
*	Setup IF/THEN/ELSE IF Block to determine what month it is.
C
*	The IF/THEN block tests for the range of julian days (nday) 
*	while accounting for the possibility of a leap year.  We must
*	subtract the extra day (February 29) from the julian date
*	if it is a leap year and the day is past February 28 which
*	has a julian day of 59.
C
C
            IF(nday.LE.31) THEN
*		January
                month = 1
                mday = nday
C
            ELSE IF((nday-lyday).LE.59) THEN
*               February
                month = 2
                mday = (nday-lyday) - 31
C
            ELSE IF((nday-lyday).LE.90) THEN
*               March
                month = 3
                mday = (nday-lyday) - 59
C
            ELSE IF((nday-lyday).LE.120) THEN
*               April
                month = 4
                mday = (nday-lyday) - 90
C
            ELSE IF((nday-lyday).LE.151) THEN
*               May
                month = 5
                mday = (nday-lyday) - 120
C
            ELSE IF((nday-lyday).LE.181) THEN
*               June
                month = 6
                mday = (nday-lyday) - 151
C
            ELSE IF((nday-lyday).LE.212) THEN
*               July
                month = 7
                mday = (nday-lyday) - 181
C
            ELSE IF((nday-lyday).LE.243) THEN
*               August
                month = 8
                mday = (nday-lyday) - 212
C
            ELSE IF((nday-lyday).LE.273) THEN
*               September
                month = 9
                mday = (nday-lyday) - 243
C
            ELSE IF((nday-lyday).LE.304) THEN
*               October
                month = 10
                mday = (nday-lyday) - 273
C
            ELSE IF((nday-lyday).LE.334) THEN
*               November
                month = 11
                mday = (nday-lyday) - 304
C
            ELSE
*               December
                month = 12
                mday = (nday-lyday) - 334
C
            ENDIF
*
*----------------------------------------------------------------------------
*
*	Write to the ftable file the name of the file and the times:
*
        NMN = minute 
        IF(NMN.LT.0) NMN = 0
*
*----------------------------------------------------------------------------
*		Force the rainfall file to begin at the beginning of the first day of
*	   rainfall with a neglible rain of 0.01 inches.
*----------------------------------------------------------------------------
*
        IF((NHOUR.EQ.0).AND.(NMN.EQ.0)) THEN
*
        ELSE
*
C
           JULDAY = nyr*1000 + nday
           TIMDAY = 0.0
           DELTA = 900.*(ntype**2)
           rain = 0.01 
C
           WRITE(NOUT) JULDAY, TIMDAY, DELTA, rain
*
        ENDIF
*
*
 1500   FORMAT( 3X,A12,5(5X,I2))
*
*******************************************************************************

C
        ENDIF
C
C
C=============================================================================
C
C	Write the results for the current rainfall and their corresponding
C       SWMM4 variables:
C
C
           JULDAY = nyr*1000 + nday
C
           TIMDAY = nhour * 3600. + 60.*(minute)
C
            DELTA = 900.*(ntype**2)
C
           rain = float(ntensty)/100.
C
         WRITE(NOUT) JULDAY, TIMDAY, DELTA, rain
*         WRITE(NOUT,*) JULDAY, TIMDAY, DELTA, rain
C
C=============================================================================
C
C	Perform the error handling routine:
C
 3800  IF((nerr.NE.6405).AND.(nerr.GT.0)) THEN
C
          IF(nerr.EQ.6101) THEN
C
               WRITE(*,3850)
C
           ELSE
C
               WRITE(*,3900) nerr,fileout 
           ENDIF
             nerr = 0
C
       ELSEIF((nerr.EQ.6405).OR.(nerr.LT.0)) THEN
C
           WRITE(*,3950) fileout
           nerr = -2
C
       ENDIF
C
 3850 FORMAT( 5X,'Incorrect Data Read ===> Searching for Rain Data ')
 3900 FORMAT( /,5X,'ERROR # ',I5,' Occurred in file:  ',A12)
 3950 FORMAT( /,5X,'End of file ',A12,'  -->  Finishing Translation.')
C
C============================================================================
C
C	Continue on to next event:
C
 5000  CONTINUE
C
C============================================================================
C
C	Close the remaining files:
C
	IF(nerr.NE.-2) WRITE(*,3950) fileout
C
        nerr = 0
      CLOSE(NOUT)
      CLOSE(NRFILE)             
*
****************************************************************************
*
*	Go on to next file to be processed:
*
 6000     CONTINUE
*
*	Finished with nfile DO LOOP, now close input file and quit:
*
      CLOSE(1)             
C
 6100 WRITE(*,*) '   -->>  Completed Conversions of BasnRain Files'
      STOP
C   
      END