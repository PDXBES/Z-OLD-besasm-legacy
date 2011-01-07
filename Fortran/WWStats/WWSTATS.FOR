      PROGRAM WWSTAT
************************************************************************
*
*    The purpose of this program is to determine Wet Weather events
*    and their statistics.  It is derived from WWFACS, which simulates 
*    varying storage and treatment rates for Wet Weather Facility.  
*    A CSO overflow event is determined by the minimum inter-event 
*    time specified in the input file.  
*
*
*    The program uses rainfall event variable names from programs written
*    by V. Adderley of CH2M HILL/PDX for the City of Portland's BES
*    CSO/SS Project.
C
*    Program created 8/15/91 by Virgil Adderley, CH2M HILL, Portland OR.
C
*    Program updated: 11/13/91 to include Tracking File
C
*    Program updated: 1/27/92 to revise input parameters
C
*    Program updated: 7/13/92 to incorporate monthly statistics
*
*    Program updated and revised by extracting statistics code out of
*    WWFACS to create WWstats.
*
*    Program updated 6/5/98 to provide multiple columns and plain CSV output
*    format (AMM); major comment cleanup
*
*****************************************************************************
*
*    Declare Variables:
*
      INTEGER julday, locats, poll(4,100), npoll, cnov, ntype
      INTEGER mseasn(12), mit, ovcon(100), jce, idatez, nloops
      INTEGER NLOC(100), ndim(4), nc2, ovcol(100), month, nerr
      REAL  Q(100), Qo, Qallow, triba, tzero, qconv, Vcso
      REAL dt, delta, tmday
      CHARACTER title(4)*80, source*20, basin*40
      CHARACTER Label1*80,Label2*80,JunkLabel
      CHARACTER*8 pname(4), punit(4)
      LOGICAL firsttime !AMM 6/4/98 First time through printing, for Plain CSV
      INTEGER eofdetect !AMM 6/4/98 EOF detector

      COMMON julday, tmday, delta, npoll, locats, Q, poll
      COMMON Qo, cnov, ntype, nloops, month, mseasn, mit
      COMMON Qallow, ovcon, source, triba, jce
      COMMON idatez,title, tzero, NLOC, pname, punit, ndim
      COMMON qconv, nc2, Vcso, dt, basin, ovcol, nerr
 
      DIMENSION ALOC(15),sumvol(15),nevnts(15),
     +  sumdur(15),sumcdy(15),xqo3(15),
     2  xqo1(15),xqo2(15),period(15),nprd(3)

*     DOUBLE PRECISION delta
      REAL evol1(15),evol2(15),evol3(15),evol4(15),pk4hrq
      REAL pk4hr1(15),pk4hr2(15),pk4hr3(15),Vso4hr(49),Vsomin(13)
      REAL Vsosum, V4hsum
      INTEGER*1 nleaps,nyr,nhour,nqtr,nseasn,nbseasn,nbmonth
      INTEGER*2 nday,lday
      INTEGER*4 tqtrs,deltaq,calday,n,m,i,lq
      CHARACTER*8 period
      CHARACTER*12 filein, filout, filenami, infile, ALOC
************************************************************************
*    The variables have the following definition:
*
*          evolm      =    CSO volume for current event
*          duratn     =    Duration of current CSO event
*          sumvol(n)  =    Cumulative CSO volume for period (n)
*          sumvm2(n)  =    Cumulative values of the squared CSO event 
*                          volumes - used for determining the variance of 
*                          the CSO event volumes
*          sumdur(n)  =    Cumulative event duration
*          sumdr2(n)  =    Cumulative values of the squared event durations
*
*          nloops     =    Number of periods to be looped through
*
*          tqtrs      =    Number of hourly quarters since 1/1/76
*          lq         =    The no. of quarters for the last data line read in
*          nf         =    The number of the first hour quarter at the time the
*                          current storm began
*
*******************************************************************************
*
*	The WWSTATS program is made up of four main sections:  
*
*		1)	READIN 	-  Reads in the data needed
*		3)	TIMER	-  Calculations seasonal time variables
*		4) EVSTATS -  Calculates summary and event statistics
*     5) OUTPUT  -  Creates tables for output file
*
*******************************************************************************
*
*	The statistics portion of the program is taken directly from the 
*	rainfall statistics programs written for the Portland CSO Project.
*
*	The program will calculate the monthly, seasonal and annual values for
*	the following parameters:
*
*		o	No. of Overflow Events
*		o	Overflow Volume (mean and coefficient of variation)
*		o	Duration of Overflow (mean and cv)
*		o	No. of Calendar Days of Discharge (mean and cv)
*
*	The statistics will be calculated for annual and seasonal periods
*	for all cases.  An option is incorporated to calculate the statistics
*	on a monthly basis as well.  As of 7/13/92, this option is activated.
*
******************************************************************************
*
*		>>>>	 R E A D I N	<<<<
*
*******************************************************************************
*    Get Input file name from user
      firsttime = .true.
      WRITE(*,10)
10    FORMAT( /,10X,'PLEASE TYPE THE INPUT FILE NAME:  ',\)
      READ(*,20) filein
20    FORMAT(A12)
*	Open input file
      OPEN(1,FILE=filein)
*    Get name of output file from user
      WRITE(*,30)   
30    FORMAT( /,10X,'PLEASE TYPE THE OUTPUT FILE NAME:  ',\)
      READ(*,20) filout
*    Open output file:
      OPEN(2,FILE=filout)
*    Get the type input file from user
      WRITE(*,'( //,15X,''PLEASE INDICATE THE TYPE '', 
     +      ''OF INPUT FILE DESIRED:'')')
      WRITE(*,'(20X,''Option #1 = standard WWSTATS  input file'')')
      WRITE(*,'(20X,''Option #2 = standard EVFLOTAK input file'')')
      WRITE(*,'(/,15X,''Your Choice is (1 or 2): '',\)')
      READ(*,50) intype
C     If user selects EVFLOTAK file, ask for time step in seconds:
      IF (intype.GT.1) THEN
        WRITE(*,'(/,25X,''Please input the time step in seconds: '',\)')
        READ(*,154) dt
      ENDIF
154   FORMAT(F10.0)
*     Get the type output file from user
      WRITE(*,40)   
40    FORMAT( //,15X,'PLEASE INDICATE THE TYPE OF OUTPUT FILE DESIRED:',
     &        /,20X,'Option #1 = ASCII Tables',/,20X,'Option #2 = ',
     2        'Comma Separated Values',/,32X,'(for spreadsheet import)',
     3        /,20X,'Option #3 = Plain CSV',
     4        /,/,15X,'Your Choice is (1,2,3): ',\)
      READ(*,50) nfopt
50    FORMAT(I1)
*    Open input file and read nloops (number of periods to loop through - 
*    annual = 1, annual & seasonal = 2, annual & seasonal & monthly = 3 and 
*    mit - the minimum inter-event time to be used.
      OPEN(1,FILE=filein)
      REWIND(1)
      INQUIRE(UNIT=1,IOSTAT=eofdetect)
      multiout: DO WHILE(eofdetect.ge.0)

C EVFLOTAK
        IF(intype.EQ.2)THEN
          CALL rdinpt
          IF(nerr.NE.0) GOTO 2000
C SWMM Interface
        ELSE
C     Read only the first 13 lines if this is our first time through...
*     Read months and their specified season:
*          Winter = 1
*          Summer = 2
          if (firsttime) then
            READ(1,*) ntype, nloops, mit
            DO 100 m = 1,12
              READ(1,*) month, mseasn(m)
100         CONTINUE
          end if

          READ(UNIT=1,FMT=*,IOSTAT=eofdetect,END=9999) infile,nc1,nc2,
     +      Qallow
*     Determine which of nc1 or nc2 is biggest, to provide a
*     default locats in case the COMBINE ASCII option is used:
          nmax = MAX(nc1,nc2)
          locats = nmax
*     Now open the input file:
          filenami = infile
*     Open the input file:
*       ntype = 1 for UNFORMATTED
*       ntype = 2 for COMBINE ASCII
*       ntype = 3 for Parsed Flow / Output File
          IF(ntype.EQ.1) THEN
*			Open SWMM Interface UNFORMATTED file
            OPEN(3,FILE=filenami,FORM='UNFORMATTED',IOSTAT=nerr,ERR=475)
          ELSE
*			Open SWMM COMBINE Interface or Parse Output ASCII file
            OPEN(3,FILE=filenami,IOSTAT=nerr,ERR=475)
          ENDIF
          REWIND(3)
*	Read the controlling storage/treatment values:
*
*	    Read the annual volume of CSO, Vcso, in cubic feet
*	    Read the time step in seconds
*	    Read the total service area of basin in acres, triba
*	    Read the Basin Name, 40 characters or less
          READ(1,*) Vcso, dt, triba, Basin
*	Read the SWMM interface file depending on the type of file:
          IF(ntype.EQ.1) THEN
*	UNFORMATTED FILE
*	Read the header lines of the interface file:
	      READ(3) title(1)
	      READ(3) title(2)
	      READ(3) idatez, tzero
	      READ(3) title(3)
	      READ(3) title(4)
            READ(3) source, locats, npoll, triba, jce
            IF(triba.LE.0.0) triba = 0.0
	      IF(JCE.LE.0) READ(3) (NLOC(K), K=1, locats)
	      IF(JCE.GE.1) READ(3) (ALOC(K), K=1, locats)
			  IF(npoll.GT.0) READ(3) (pname(J),J=1,npoll)
			  IF(npoll.GT.0) READ(3) (punit(J),J=1,npoll)
			  IF(npoll.GT.0) READ(3) (ndim(J),J=1,npoll)
			  READ(3) qconv
          elseif (ntype.EQ.2) then
*	ASCII FILE FROM COMBINE
*	Read the 4 header lines of the interface file:
            READ(3,330) title(1)
            READ(3,330) title(2)
            READ(3,330) title(3)
*			Test if the user has placed the locats and JCE on line #4:
            READ(3,*,ERR=325) locats, JCE
            GOTO 335
*	An error occurred, so re-read title(4) as a character string
325         BACKSPACE(3)
            READ(3,330) title(4)
            locats = nmax
            WRITE(*,328)
328       FORMAT( /,5X,'ERROR OCCURRED ON INPUT, READING TITLE(4) AS ',
     +      'CHARACTER.')
330         FORMAT(A80)
*  Now read the number of locations on the header file and set
*  npoll = 0
335         IF(JCE.LE.0) READ(3,*) (NLOC(K), K=1, locats)
            IF(JCE.GE.1) READ(3,*) (ALOC(K), K=1, locats)
*
            npoll = 0.
*
          ELSE
*  Read for ntype = 3 for Parsed Flow / Output File
*  VCA 10/17/96 Add in ability to read Parsed Extran Output files
*
            READ(3,*) Label1
            READ(3,*) Label2
            READ(3,*) title(1)
            READ(3,*) title(2)
            READ(3,*) JunkLabel,IDateZ, JunkLabel,TZero
            READ(3,'(A)') title(3)
            READ(3,'(A)') title(4)
            READ(3,*) source, JunkLabel, triba
            IF(triba.LE.0.0) triba = 0.0
            READ(3,*) JunkLabel, Delta
            READ(3,*) JunkLabel, Locats
            NJCE = 0
            IF(JCE.LE.0) READ(3,*) JunkLabel, (NLOC(K), K=1, locats)
            READ(3,*) JunkLabel
            READ(3,*) JunkLabel
            READ(3,*) JunkLabel
            READ(3,*) JunkLabel
            READ(3,*) JunkLabel
            READ(3,*) JunkLabel
          ENDIF
*	Check to be sure that nc1 and nc2 is = or < locats
          IF (nc1.GT.locats) THEN
            WRITE(*,287) nc1, locats
            STOP
          ELSEIF (nc2.GT.locats) THEN
            WRITE(*,287) nc2, locats
            STOP
          ENDIF
*
        ENDIF !then block for input type reading
287     FORMAT( /,5X,'Error in input file:  nc = ',I2,' while ',
     &    'interface file has locats = ',I2,//)


*    Initialize Variables:
*    Set up DO LOOP to initialize all the test dependent variables:
C
*	Determine the beginning of the DO LOOP for the number of periods
*	to be examined:  (months = 1 to 12, seasons = 13 to 14, annual = 15)
*
        IF(nloops.EQ.1) npb = 15
        IF(nloops.EQ.2) npb = 13
        IF(nloops.EQ.3) npb = 1
*
C----------------------------------------------------------------------------
C
*	Set names of the months, seasons or year into the character 
*       array "period":
C
        period(1)="January"
        period(2)="February"
        period(3)="March"
        period(4)="April"
        period(5)="May"
        period(6)="June"
        period(7)="July"
        period(8)="August"
        period(9)="Septembr"
        period(10)="October"
        period(11)="November"
        period(12)="December"
        period(15)="Annual"
        period(13)="Winter"
        period(14)="Summer"
*	Test if Vcso is negative:  If yes, then user is requesting
*	that the program use more standard units.  Set unit flag and
*	conversion:
        IF(Vcso.LT.0) THEN
          Vcso = Vcso * 1000000/7.4805 * (-1)
        ENDIF
*	Begin loop through the periods to be examined:
        DO 440 np = npb, 15
*	Initialize the summary variables:
*       sumev2(np) = 0.
          nevnts(np) = 0
*       sumvm2(np) = 0.
          sumvol(np) = 0.
*       sumdr2(np) = 0.
          sumdur(np) = 0.
*       sumcd2(np) = 0.
          sumcdy(np) = 0.
          xqo1(np) = 0. 
          xqo2(np) = 0.
          xqo3(np) = 0.
	    pk4hr1(np) = 0.
	    pk4hr2(np) = 0.
	    pk4hr3(np) = 0.
	    evol1(np) = 0.
	    evol2(np) = 0.
	    evol3(np) = 0.
	    evol4(np) = 0.
440     CONTINUE
*     Initialize event-based variables:
* AMM 6/8/98 Added following two initializations for multiple loops        
        nf = 0
	  nbseasn = 0
        evolm = 0.
        peakqo = 0.
        pk4hrq = 0.
        calday = 0.
        Vso = 0.
*	Create a conversion factor for moving between cubic feet and MG
        convMG = 7.4805 / 1000000.
*	Write message to screen to tell user the current date examined:
        WRITE(*,4180)
4180    FORMAT( /,20X,'==>>  Current Julian Date')

*=======================================================================
*     Begin loop to read in one line of data at a time:
* 475       IF(.NOT.EOF(3)) THEN
475     IF(abs(nerr).LE.0.001) THEN
*	Test for Type of file:
          IF(intype.GT.1)THEN
            CALL rdfile
            IF(nerr.NE.0) GOTO 2000
          ELSE
            IF(ntype.EQ.1) THEN
*     UNFORMATTED FILE
*	Test if there is any pollutant data to read in, else read in flow data:
              IF(npoll.GT.0) THEN
                READ(3,ERR=2000,IOSTAT=nerr) julday, tmday, delta,
     &            (Q(K),(poll(J,K),J=1,npoll), K=1,nmax)
              ELSE
                READ(3,ERR=2000,IOSTAT=nerr) julday,tmday,delta,
     &            (Q(K),K=1,nmax)
              ENDIF
            ELSEIF(ntype.EQ.2) THEN
*     COMBINE ASCII FILE
*     Test if there is any pollutant data to read in, else read in flow data:
              IF(npoll.GT.0) THEN
                READ(3,*,ERR=2000,IOSTAT=nerr) julday, tmday, delta,
     &            (Q(K),(poll(J,K),J=1,npoll), K=1,nmax)
              ELSE
                READ(3,*,ERR=2000,IOSTAT=nerr) julday,tmday,delta,
     &            (Q(K),K=1,nmax)
              ENDIF
            ELSE
*			Parsed Extran Output File
              READ(3,*,ERR=2000,IOSTAT=nerr) Hour,(Q(K),K=1,nmax)
              Year = Int(IDatez/1000)
              days = Int(TZero/3600.0/24.0 + Hour / 24.0)
              YrDays = IDatez - Year*1000 + days
              If(YrDays.GT.365) Then
                Year = Year + Int(YrDays/365)
                YrDays = YrDays - 365*(Int(YrDays/365))
              EndIf
              julday = Year*1000 + YrDays
              tmday  = (Hour + TZero/3600.0 - 24*days)*3600.0
            ENDIF
*	Assign the appropriate column to the diversion structure overflow:
            Qo = Q(nc1)
          ENDIF    !end test for evflotak of wwstats input file format
*	Write message to screen to tell user the current date examined:
          WRITE(*,4230) julday
4230      FORMAT('+',12X,I6)
          IF (lq.GE.1) THEN
*	This is not the first time step:
*	Check if user dt is equal to input file delta. Stop program if different
	  	  IF(ABS(delta-dt).GT.0.1)THEN
	  	    WRITE(*,'('' Input file time step is different from'',
     +          '' interface file time step'')')
		      WRITE(*,'('' Input file time step= '',F8.3)') dt
		      WRITE(*,'('' Interface file time step='',F8.3)')delta
		      WRITE(*,'('' Continuing with statistics... '')')
              WRITE(*,4180)
      		    GOTO 475
	      ENDIF
          ENDIF

*		>>>>	 T I M E R	<<<<
*
*	Calculate nyr, nday, nhour and nqtr:
          nyr = julday/1000
          nday = julday - nyr*1000
          nhour =  INT(tmday/3600.)
          nqtr = (tmday - nhour*3600 + 60)/60/15
*     Calculate the number of leap years since 1/1/1900.
          nleaps = INT((nyr)/4)
*	Determine if it is currently a leap year:
          declyr = (float(nyr))/4.
          intlyr = nyr/4
          ytest = declyr-intlyr
          IF(ytest.LT.0.1) THEN
            lyday = 1 ! It's a leap year
          ELSE
            lyday = 0 ! It's not a leap year
          ENDIF
*	Setup IF/THEN/ELSE IF Block to determine what month it is.
C
*	The IF/THEN block tests for the range of julian days (nday) 
*	while accounting for the possibility of a leap year.  We must
*	subtract the extra day (February 29) from the julian date
*	if it is a leap year and the day is past February 28 which
*	has a julian day of 59.
C
          IF(nday.LE.31) THEN
            month = 1 ! Jan
            mday = nday
          ELSE IF((nday-lyday).LE.59) THEN
            month = 2 ! Feb
            mday = (nday-lyday)-31
          ELSE IF((nday-lyday).LE.90) THEN
            month = 3 ! Mar
            mday = (nday-lyday)-59
          ELSE IF((nday-lyday).LE.120) THEN
            month = 4 ! Apr
            mday = (nday-lyday)-90
          ELSE IF((nday-lyday).LE.151) THEN
            month = 5 ! May
            mday = (nday-lyday)-120
          ELSE IF((nday-lyday).LE.181) THEN
            month = 6 ! Jun
            mday = (nday-lyday)-151
          ELSE IF((nday-lyday).LE.212) THEN
            month = 7 ! Jul
            mday = (nday-lyday)-181
          ELSE IF((nday-lyday).LE.243) THEN
            month = 8 ! Aug
            mday = (nday-lyday)-212
          ELSE IF((nday-lyday).LE.273) THEN
            month = 9 ! Sep
            mday = (nday-lyday)-243
          ELSE IF((nday-lyday).LE.304) THEN
            month = 10 ! Oct
            mday = (nday-lyday)-273
          ELSE IF((nday-lyday).LE.334) THEN
            month = 11 ! Nov
            mday = (nday-lyday)-304
          ELSE
            month = 12 ! Dec
            mday = (nday-lyday)-334
          ENDIF
*	Now set the season variable = season for the current month
          nseasn = mseasn(month)
*	Set the period variables (nprd(np)) to calculate the event
*	statistics on the proper monthly, seasonal and annual basis:
*	The nprd array is based on the nloops variable which states
*	the number of loops to be made when calculating the statistics.
*
*		nprd(3) = monthly parameters
*		nprd(2) = seasonal parameters
*		nprd(1) = annual parameters
          nprd(3) = month
          nprd(2) = nseasn+12
          nprd(1) = 15
*     Now calculate the hour number:
          nhours = ((nyr-0)*365 + nleaps + nday - 1)*24 + nhour
*     Now calculate the quarter hours:
          tqtrs = nhours * 4 + nqtr
*	Test if this is the first quarter read in:
          IF (lq.LE.0) THEN
            nbqtr   = tqtrs
            lq = tqtrs
            lqtrs   = tqtrs
          ENDIF
*	Calculate the time (number of quarters) since the last
*	non-zero positive flow occurred:
          ndelta = tqtrs - lqtrs - 1
*	Convert ndelta to number of time steps:
          ndelta = ndelta/(dt/900.)
*	Test if flow is <= 0. IF yes, move
*	on to next data line - Short cut to speed up program execution.
*	This option was tested on 11/13/91 and was correct.
          IF((Qo-Qallow).GT.0.0001) GOTO 4300
*		We can use the short cut:  Get next line of data
          Qo = 0.
          GOTO 475
*     We can't use the short cut:  Go on to do calculations
*
*     Calculate new overflow volume:
*
4300      Vso = dt * (Qo-Qallow)
*******************************************************************************
*
*		>>>>	 E V S T A T S    <<<<
*
*******************************************************************************
*
*     Test for end of a CSO event for the minimim inter-event period input by 
*     the user as mit. (k=ntest loop variable)
*
*	Test if any overflow from the storage tank occurred.  If yes,
*	then an event is either continuing or has begun.  If no, then
*	an event has either ended or has a dry period.
*
          IF (Vso.GT.0.00001) THEN
*     We have overflow!
*     Initialize "first event time record keeper" nf
            IF(nf.LE.0) THEN
              nf = tqtrs
*			Initialize the "last quarter record keeper" also
              lq = nf
              nprd(2) = nseasn+12
              nprd(3) = month
            ENDIF
*		Test to see if this is the beginning of an event by
*		calculating the time difference since the last overflow
*		occurred (deltaq).
            deltaq = float(tqtrs) - float(lq) - 1
*AMM
*            write(2,90001) deltaq, nf, nprd(3)
*90001       format(' deltaq=',F8.2, ' nf=',I8, ' nprd(3)=',I2)
            IF (deltaq.GT.(mit*4)) THEN
*		This is the beginning of a new event, so we must
*		store the parameters for the last event.
*       Set up DO LOOP for placing the summary data into the 
*         appropriate period: 
              DO 800 npc = 1, nloops
*			The variable "npc" is a dummy counter variable that
*			will allow the CSO parameter summations to be
*			done for the three types of periods:  The monthly
*			period, the seasonal period and the annual period.
C
*			npc = 3 for monthly parameters
*			npc = 2 for seasonal parameters
*			npc = 1 for annual parameters
*			
*			We therefore perform annual calculations first,
*			seasonal second and then monthly third if requested
*			by user in specifying the nloops value.
*
*         Re-Set the nprd array element for the month and season in which the current
*        event began:
                IF(nbseasn.GT.0) THEN
                  nprd(2) = nbseasn+12
                  nprd(3) = nbmonth
                ELSE
*     Current time step:
                  nprd(2) = nseasn+12
                  nprd(3) = month
                ENDIF
*     Now set the value of "np" or number of period to
*     the value of the complete nprd(npc) array:
                np = nprd(npc)
*     Increase the number of events counter:
                nevnts(np) = nevnts(np) + 1
*     Store the CSO volume parameters
                sumvol(np) = sumvol(np) + evolm
                IF(evolm.GT.evol1(np)) THEN
	 				    evol4(np) = evol3(np)
                  evol3(np) = evol2(np)
                  evol2(np) = evol1(np)
                  evol1(np) = evolm
                ELSEIF(evolm.GT.evol2(np)) THEN
					    evol4(np) = evol3(np)
                  evol3(np) = evol2(np)
                  evol2(np) = evolm
                ELSEIF(evolm.GT.evol3(np)) THEN
					    evol4(np) = evol3(np)
                  evol3(np) = evolm
			      ELSEIF(evolm.GT.evol4(np)) THEN
					    evol4(np) = evolm
                ENDIF
*     Calculate the overflow duration in quarters of hours:
                duratn = (float(lq)-float(nf)+1)/4.0
*     Store the CSO duration parameters
                sumdur(np) = sumdur(np) + duratn
*AMM	Print out sumdur for debug
*                WRITE(2,90000) np, sumdur(np), duratn, month, mday,nday,
*     +            nf     
*90000     FORMAT(' SUMDUR(',I5,')=',F8.2, ' DURATN=',F8.2,
*     +           ' MONTH=',I2,' MDAY=',I2,' NDAY=',I3, ' NF=',I7)
*     Store the number of calendar days of CSO
                sumcdy(np) = sumcdy(np) + calday
*     Store the peak CSO parameters
                IF(peakqo.GT.xqo1(np)) THEN
                  xqo3(np) = xqo2(np)
                  xqo2(np) = xqo1(np)
                  xqo1(np) = peakqo
                ELSEIF(peakqo.GT.xqo2(np)) THEN
                  xqo3(np) = xqo2(np)
                  xqo2(np) = peakqo
                ELSEIF(peakqo.GT.xqo3(np)) THEN
                  xqo3(np) = peakqo
                ENDIF
            
                IF(pk4hrq.GT.pk4hr1(np)) THEN
                  pk4hr3(np) = pk4hr2(np)
                  pk4hr2(np) = pk4hr1(np)
                  pk4hr1(np) = pk4hrq
                ELSEIF(pk4hrq.GT.pk4hr2(np)) THEN
                  pk4hr3(np) = pk4hr2(np)
                  pk4hr2(np) = pk4hrq
                ELSEIF(pk4hrq.GT.pk4hr3(np)) THEN
                  pk4hr3(np) = pk4hrq
                ENDIF
800           CONTINUE
*     Now re-set the event-based variables such as overflow and treated
*     volume, peak overflow and the first quarter hour of the new 
*     storm (nfqtr):
              evolm  = Vso
              peakqo = Vso
              pk4hrq = 0
              nf     = tqtrs
*	Set the month and season number (1 = winter, 2 = summer) in which the
*	current event began:
              nbmonth = month
              nbseasn = nseasn
              calday = 1

			    DO i= 1, 3600/delta
				    Vsomin(i)= 0
			    END DO

			    DO i= 1, 4*3600/delta
				    Vso4hr(i)= 0
			    END DO

            ELSE
*     The CSO event has not ended and is on-going.  Increase the
*     event based parameters accordingly:
			    n= 3600/delta
              IF(n.LT.1) n=1
			    m= 4*n
			    Vsomin(n+1)= Vso
			    Vso4hr(m+1)= Vso
			    Vsosum= 0
			    V4hsum= 0			
			    DO i= 1, n
				    Vsomin(i)=  Vsomin(i+1)
                Vsosum= Vsosum + Vsomin(i)
			    END DO
			    DO i= 1, m
				    Vso4hr(i)= Vso4hr(i+1)
				    V4hsum= V4hsum + Vso4hr(i)
			    END DO
*     Now reset Vsosum and V4hsum to the average value over the period
*     which is independent of the time step delta.
              Vsosum = Vsosum / n
              V4hsum = V4hsum / n
              evolm = evolm + Vso
*     Check for peak CSO volume
              IF (Vsosum.GT.peakqo) THEN
                peakqo = Vsosum
              ENDIF
              IF (V4hsum.GT.pk4hrq)pk4hrq= V4hsum
*	Determine if this is a new calendar day:
              IF(lday.NE.nday) THEN
                calday = calday + 1
              ENDIF
*	Complete the IF/THEN/ELSE block testing for the end of the event:
            ENDIF
*	Set lag variables for occurence of an overflow:
            lday = nday
            lq = tqtrs
*	Continue the IF/THEN/ELSE block for deteriming if an overflow
*	occurred:
          ELSE
*     No overflow for this time step.  
          ENDIF
*-----------------------------------------------------------------------------
*
*		We have looked at all the situations:  overflow at the
*		beginning of an event, overflow during an event, and
*		no overflow during an event - for each period (year,
*		season and/or month) selected by the user.  
*
*-----------------------
*
*	Continue on to read next line of data:
*
*		 We must first re-set the lag variables:
*
          lqtrs = tqtrs
          GOTO 475
*          Finish IF/THEN block testing for end of file:
        ENDIF
*******************************************************************************
*
*		>>>>	 R E A D E R R O R    <<<<
*
*******************************************************************************
*
*    When a READ error occurs, the program will come to this section.
*    Here, we want to check for the end of file error numbers (-1 & 6405)
*    and write a summary report if the file is completed.  If an unkown 
*    error occurs, then write the summary report anyway.
C
2000    IF((nerr.EQ.-1).OR.(nerr.EQ.6405).OR.(nerr.EQ.0)) THEN
*         These are the end of file errors, so its okay to continue.
*         Write status message to screen
          WRITE(*,2100) infile
        ELSE
*         An unexpected error has occurred:  Write message to output file
*         and to the screen.  Continue on as though no error occured
*         so that the statistics thus far calculated are saved.
          WRITE(2,2200) nerr,infile
          WRITE(*,2200) nerr,infile
        ENDIF
2100  FORMAT( /,5X,'===>>  Writing analysis results for file: ',A12,//)
2200  FORMAT( /,5X,'UNEXPECTED ERROR # ',I6,' OCCURRED IN FILE: ',A12,
     2  /,5X,'>> CURRENT STATISTICS SAVED TO OUTPUT FILE',//)
*******************************************************************************
*
*		>>>>	 O U T P U T    <<<<
*
*******************************************************************************
*******************************************************************************
*
*
*    Now finish the statistics calculations and write the results
*    to the output file table.
*
C
*    Begin Creation of the output file summary table
C
C
*	  Determine how many years of data were examined by dividing
*	  the total numbers of quarters by 35,064, which is the
*	  average number of 15 minute periods in a year.
C
        years = float(tqtrs-nbqtr)/35064
*	Test if User wants an ASCII table file or a CSV file:
        IF(nfopt.LE.1) THEN
*     Create the output file for ASCII Tables
*     Creating a tab character
*     Print Summary Table Title
          WRITE(2,200)
200       FORMAT( /,40X,'C S O   STATISTICAL',
     2      '   EVALUATION    SUMMARY   TABLE',/)
*     Print the basin name, years examined and initial values:
          IF(nc2.LE.0) THEN
            nc2 = 9
            NLOC(nc2) = 0000
            ALOC(nc2) = '0000'
          ENDIF
          IF(JCE.LE.0) WRITE(2,205) basin,NLOC(nc1),years, 
     +      dt,Vcso*convMG,triba
          IF(JCE.GE.1) WRITE(2,206) basin, ALOC(nc1),
     +      years, dt, triba
205       FORMAT('Basin Name=',A20,/,'Overflow Element',
     +      '   Number=',I12,/,'Number of years=',
     +      F5.2,/,'Time Step (seconds)=',F6.0,
     +      /,'Annual CSO Volume (Vcso)=',F8.2,
     +      ' MG',/,15X,'Basin Area (acres)=',F12.2,/) 
206      FORMAT(15X,'Basin Name',16X,'=  ',A20,/,15X,'Overflow Element',
     +    ' Number   =',A12,/,15X,'Number of years:',
     +    10X,'=   ',F5.2,/,15X,'Time Step (seconds)',8X,'= ',F6.0,
     +    /,15X,'Basin Area',16X,'= ',F12.2,' acres',/) 
*     Print "====" line
          WRITE(2,210)
*     Print table column lables
          WRITE(2,220)
C     Print table column spacers
*     WRITE(2,230)
C
*     Print "====" line
          WRITE(2,210)
*     Print table column spacers
          WRITE(2,230)
C
210       FORMAT(2X,
     +      '====================================================',
     +      '=======================================================',
     +      '=======================================================',
     +      '====================')
220       FORMAT(2X,'||',8X,'| No. of  |',
     +    '   OVERFLOW     |    OVERFLOW    |     MAXIMUM  EVENT   |',
     +    '   No. of Calendar  ||',/,2X,'|| PERIOD | EVENTS  |    ',
     +    'VOLUME      |    DURATION    |    OVERFLOW  RATES    |  ',
     +    'Days of Overflow  ||',/,2X,'||',8X,'|---------|------------',
     +    '----|----------------|---------|-----------|--------------',
     +    '---------|--------------------|-----------|-----------|---',
     +    '--------||',/,2X,'||',8X,'| Avg/Yr  | Avg/Yr|Avg/Evnt| Avg/',
     +    'Yr|Avg/Evnt|  1st  |  2nd  |  3rd  |  Avg/Yr | Avg/Evnt ||',
     +    /,2X,'||',8X,'|',9X,'|  (MG) |  (MG)  | (hrs) |  (hrs) |',
     +    ' (cfs) | (cfs) | (cfs) |',9X,'|',10X,'||')
230       FORMAT(2X,'||',8X,'|',9X,'|',7X,'|',8X,'|',2(7X,'|',8X,'|'),
     +      8X,'|',11X,'|',3(7X,'|'),9X,'|',10X,'|',11X,'|',11X,'|',
     +      11X,'||')
240       FORMAT(2X,'||----------------------------------------------',
     +      '-------------------------------------------------------',
     +      '-------------------------------------------------------',
     +      '---------------------||')
        ELSE
*     Create a Comma Separated Value (*.csv) for
*     importing into a spreadsheet
*     Print Summary Table Title
	    IF (nfopt.EQ.2) then 
            WRITE(2,202)
* 
202         FORMAT( '"STATISTICAL   EVALUATION    SUMMARY   TABLE"',/)
*		Print the basin name, years examined and initial values:
            IF((JCE.LE.0).AND.(intype.EQ.1)) WRITE(2,207) basin,
     +        NLOC(nc1), years, dt,triba
            IF((JCE.LE.0).AND.(intype.EQ.2)) WRITE(2,307) basin,
     +        years, dt,triba,(NLOC(ovcol(i)), i= 1,cnov)
*
207         FORMAT(',"Basin Name = ",,,"',A20,'"',/,',"Overflow',
     +        ' Element Number = ",,,',I12,/,',"Number of years = ",,,',
     +        F5.2,/,',"Time Step (seconds) = ",,,',F6.0,
     +        /,',"Basin Area (acres) =",,,',F12.2,/) 
307         FORMAT(',"Basin Name = ",,,"',A20,'"',/,
     +        ',"Number of years =",,,',F5.2,/,
     +        ',"Time Step (seconds) =" ,,,',F6.0,/,
     +        ',"Basin Area (acres) =",,,',F12.2,/, 
     +        ',"Overflow Element Number =",,,':100(I12,','))

            IF(JCE.GE.1) WRITE(2,208) basin, ALOC(nc1),
     +        years, dt, Vcso*convMG, triba
208         FORMAT(' " ", ',
     +      '"Basin Name = ", "',A20,'"',/,' " ", "Overflow',
     +     ' Element Number = ", ',A12,/,' " ", "Number of years = ", ',
     +      F5.2,/,' " ", "Time Step (seconds) = ", ',F6.0,
     +      /,' " ", "Annual CSO Volume (Vcso, MG) =", ',F8.2,
     +      /,' " ", "Basin Area (acres) =", ',F12.2,/) 
*     Print table column labels
	    ENDIF

	    if (nfopt.eq.2) then
            WRITE(2,222)
	    else
		    if (firsttime) then
			    WRITE(2,223)
			    firsttime = .false.
	      endif
	    endif
222       FORMAT( /,',"No. of","OVERFLOW",,"MAXIMUM EVENT VOLUMES",',
     +      ',,,"OVERFLOW",,"MAXIMUM PEAK HOUR",,,',
     +      '"MAXIMUM PEAK 4-HOUR",,," No. of Calendar"',/,'"PER',
     +      'IOD","EVENTS","VOLUME",,"VOLUME",,,,"DURATION"',
     +      ',,"FLOWRATE",,,"FLOWRATE",,,"Days of Overflow"',
     +      /,'" ","--------","----------","--------","----------",',
     +      '"--------","--------","--------","----------","--------",',
     +      '"----------","--------","---------","---------",',
     +      '"----------","---------","---------","---------"',/,
     +      '" ","Avg/Yr","Avg/Yr","Avg/Evnt","1st Evnt","2nd Evnt",',
     +      '"3rd Evnt","4th Evnt","Avg/Yr","Avg/Evnt","1st Evnt",',
     +      '"2nd Evnt","3rd Evnt","1st Evnt","2nd Evnt","3rd Evnt",',
     +      '"Avg/Yr","Avg/Evnt"',/,',,',
     +     '"(MG)","(MG)","(MG)","(MG)","(MG)","(MG)","(hrs)","(hrs)",',
     +      '"(cfs)","(cfs)","(cfs)","(cfs)","(cfs)","(cfs)",',
     +      '"(days)","(days)"',/)
223	    FORMAT('"Basin",',
     +      '"Period","NumEvt","OverflowVolPerYr","OverflowVolPerEvt",',
     +    '"MaxVol1","MaxVol2","MaxVol3","MaxVol4","OverflowDurPerYr",',
     +      '"OverflowDurPerEvt","MaxFlow1","MaxFlow2","MaxFlow3",',
     +      '"Max4HrFlow1","Max4HrFlow2","Max4HrFlow3",',
     +      '"OverflowDaysPerYr","OverflowDaysPerEvt"')
*		Finish the IF/THEN/ELSE block for type of output file
        ENDIF
*	We have reached the end of the file.  We first set up the necessary 
*	DO LOOPS to finish the statistics for the last CSO event, then 
*	calculate the total period statistics, and then write the results
*	to the output file:
*
*     Set up DO LOOP for placing the summary data into the 
*     appropriate period: 
        DO 2500 npc = 1, nloops
*     npc = 3 for monthly parameters
*     npc = 2 for seasonal parameters
*     npc = 1 for annual parameters
*
*     Re-Set the nprd array element for the month and season in which the current
*     event began:
          IF(nbseasn.GT.0) THEN
            nprd(2) = nbseasn+12
            nprd(3) = nbmonth
          ELSE
*     Current time step:
            nprd(2) = nseasn+12
            nprd(3) = month
          ENDIF
*     Now set the value of "np" or number of period to
*     the value of the complete nprd(npc) array:
          np = nprd(npc)
*	Test if there is currently an event which needs to be closed out:
          IF(evolm.GT.0.001) THEN
*     Yes, an event has been on-going and needs to be closed out:
*     Increase the number of events counter:
            nevnts(np) = nevnts(np) + 1
*     sumev2(np) = sumev2(np) + nevnts(np)**2

*     Store the CSO volume parameters
            sumvol(np) = sumvol(np) + evolm
*     sumvm2(np) = sumvm2(np) + evolm**2
            IF(evolm.GT.evol1(np)) THEN
      	      evol4(np) = evol3(np)
              evol3(np) = evol2(np)
              evol2(np) = evol1(np)
              evol1(np) = evolm
            ELSEIF(evolm.GT.evol2(np)) THEN
              evol4(np) = evol3(np)
              evol3(np) = evol2(np)
              evol2(np) = evolm
            ELSEIF(evolm.GT.evol3(np)) THEN
              evol4(np) = evol3(np)
              evol3(np) = evolm
            ELSEIF(evolm.GT.evol4(np)) THEN
              evol4(np) = evolm
            ENDIF
*     Calculate the overflow duration in quarters of hours:
            duratn = (float(lq)-float(nf)+1)/4.0
*     Store the CSO duration parameters
            sumdur(np) = sumdur(np) + duratn
*       sumdr2(np) = sumdr2(np) + duratn**2
*
*     Store the number of calendar days of CSO
            sumcdy(np) = sumcdy(np) + calday
*       sumcd2(np) = sumcd2(np) + calday**2
*
*     Store the peak hourly CSO parameters
            IF(peakqo.GT.xqo1(np)) THEN
              xqo3(np) = xqo2(np)
              xqo2(np) = xqo1(np)
              xqo1(np) = peakqo
            ELSEIF(peakqo.GT.xqo2(np)) THEN
              xqo3(np) = xqo2(np)
              xqo2(np) = peakqo
            ELSEIF(peakqo.GT.xqo3(np)) THEN
              xqo3(np) = peakqo
            ENDIF

            IF(pk4hrq.GT.pk4hr1(np)) THEN
              pk4hr3(np) = pk4hr2(np)
              pk4hr2(np) = pk4hr1(np)
              pk4hr1(np) = pk4hrq
            ELSEIF(pk4hrq.GT.pk4hr2(np)) THEN
              pk4hr3(np) = pk4hr2(np)
              pk4hr2(np) = pk4hrq
            ELSEIF(pk4hrq.GT.pk4hr3(np)) THEN
              pk4hr3(np) = pk4hrq
            ENDIF
          ELSE
            IF(nevnts(np).LE.0) THEN
*       No overflow events have occured for this configuration
              IF(sumvol(np).GT.0.) THEN
*			An error has occured
                WRITE(*,1119) np
1119     FORMAT( /,'An  error has occurred:  no events, no volume but ',
     +      'sumvol > 0.0 ?!?',/,'  nsv = ',I1,' ntq = ',I1,' np = ',I1)
              ENDIF
            ENDIF
*	Finish the IF/THEN block for an on-going event which needed to be
*	finalized or closed out:
          ENDIF
*	Finish DO LOOP for finializing events:
2500    CONTINUE
*	Set up nperiod DO LOOP to calculate final statistics:
*
*	Determine the beginning of the DO LOOP for the number of periods
*	to be examined:  (months = 1 to 12, seasons = 13 to 14, annual = 15)
        IF(nloops.EQ.1) npb = 15
        IF(nloops.EQ.2) npb = 13
        IF(nloops.EQ.3) npb = 1

*		Begin loop through the periods to be examined:
        DO 2800 np = npb, 15
*	  Determine the average number of events/year or period:
*
*	  Test for occurence of less than one year which might hang program:
          IF(years.GE.1.0) THEN
            events = float(nevnts(np))/years
*     Determine average values for overflow volume, duration,
*     treatment volume and calendays of events per period
*
            avgvol = sumvol(np)/years
            avgdur = sumdur(np)/years
            avgcdy = sumcdy(np)/years
          ELSE
            events = float(nevnts(np))
*     Determine average values for overflow volume, duration,
*     treatment volume and calendays of events per period
            avgvol = sumvol(np)
            avgdur = sumdur(np)
            avgcdy = sumcdy(np)
          ENDIF
          frstqof = xqo1(np)/dt
          scndqof = xqo2(np)/dt
          thrdqof = xqo3(np)/dt
		  pk4hr1(np)= pk4hr1(np)/dt/4.0
		  pk4hr2(np)= pk4hr2(np)/dt/4.0
		  pk4hr3(np)= pk4hr3(np)/dt/4.0			
*     Determine average values for overflow volume, duration,
*     treatment volume and calendays of events per event:
          IF(nevnts(np).GE.1) THEN
            eavgvol = sumvol(np)/(nevnts(np))
            eavgdur = sumdur(np)/(nevnts(np))
            eavgcdy = sumcdy(np)/(nevnts(np))
          ELSE
            eavgvol = 0.0
            eavgdur = 0.0
            eavgcdy = 0.0
          ENDIF
*     Determine the variances - Event/Period Variance
*     varevn = (sumev2(np)-(years*events**2))/(years-1)
*     Event-based variances:
*-----------------------------------------------------------------------------
*	Now is the time to write the results for this period to the output 
*       file.  Each line in the file will contain the following information:
*
*		*	Basin Name
*		*	No. of years examined
*		*	Time Step
*		*	Average annual Vcso
*		*	Basin Area
*		*	No. of Events per year or period
*		*	Average for:
*		*	Average and coefficient	of variation for:
*
*		*	CSO Volume (feet^3)
*       *  CSO Duration (hours)
*       *  Peak Hourly CSO (feet^3)
*     	*	Calendar Days of Overflow
*
*-------------------------
*	Test for type of file:
          IF(nfopt.LE.1) THEN
*		Write output for ASCII Table
            WRITE(2,2300) period(np),events,
     +        avgvol*convMG,eavgvol*convMG,avgdur,eavgdur,
     +        frstqof,scndqof,thrdqof,avgcdy,eavgcdy
2300        FORMAT(2X,'||',A8,'|  ',F6.2,
     +        ' |',F7.2,'| ',F7.3,'|',F7.2,'| ',F7.3,'|',
     +        F6.1,' |',F6.1,' |',F6.1,' |  ',F6.2,' |  ',F6.3,' ||')
          ELSE 
*		Write output for CSV file:
	      if (nfopt.EQ.2) then
              WRITE(2,2302) period(np),events,
     +          avgvol*convMG,eavgvol*convMG,evol1(np)*convMG,
     +          evol2(np)*convMG,evol3(np)*convMG,evol4(np)*convMG,
     +          avgdur,eavgdur,frstqof,scndqof,thrdqof,
     +          pk4hr1(np),pk4hr2(np),pk4hr3(np),avgcdy,eavgcdy
	      else
              WRITE(2,2303) Basin,period(np),events,
     +          avgvol*convMG,eavgvol*convMG,evol1(np)*convMG,
     +          evol2(np)*convMG,evol3(np)*convMG,evol4(np)*convMG,
     +          avgdur,eavgdur,frstqof,scndqof,thrdqof,
     +          pk4hr1(np),pk4hr2(np),pk4hr3(np),avgcdy,eavgcdy
	      endif
2302        FORMAT('"',A8,'", ',F6.2,',',F7.2,', ',F7.3,', ',
     +	      4(F7.3,','),F7.2,',',F7.3,',', 6(F6.1,','),F6.2,', ',F6.3)
2303        FORMAT('"',A,'","',A8,'", ',F10.2,',',F10.2,', ',F10.3,', ',
     +	      4(F10.3,','),F10.2,',',F10.3,',', 6(F10.1,','),F10.2,', ',
     +        F10.3)
          ENDIF


*	Test to see if months are done & print separator line
          IF((nloops.EQ.3.).AND.(np.EQ.12)) THEN
            IF(nfopt.LE.1) WRITE(2,240)
            IF(nfopt.EQ.2) WRITE(2,*)
          ENDIF
*	Test to see if Seasons are done & print separator line
          IF((nloops.GE.2.).AND.(np.EQ.14)) THEN
            IF(nfopt.LE.1) WRITE(2,240)
            IF(nfopt.EQ.2) WRITE(2,*)
          ENDIF
*	Continue on to next period (season or month) as we are done with
*	the period-dependent summary variables
2800    CONTINUE
*	We are done with this file now.  Print the bottom line of
*	the table and continue on to the next input file.
*        BACKSPACE(2)
*        BACKSPACE(2)


*     Print table column spacers
        IF(nfopt.LE.1) THEN
          WRITE(2,230)
          WRITE(2,210)
	  ENDIF
*	We have read all of the files now.
*	Close the output and input files and finish the program.
*
* AMM 6/4/98 Loop statement for multiple outputs
      END DO multiout

      CLOSE(1)
      CLOSE(2)
      CLOSE(3)
      CLOSE(4)
      STOP
9999  STOP
      END

************************************************************************
************************************************************************
************************************************************************

      SUBROUTINE rdinpt
        IMPLICIT none

        INTEGER julday, locats, poll(4,100), npoll, cnov, ntype
        INTEGER mseasn(12), mit, ovcon(100), jce, idatez, nloops
        INTEGER NLOC(100), ndim(4), nc2, ovcol(100), month, nerr
        REAL  Q(100), Qo, Qallow, triba, tzero, qconv, Vcso
        REAL dt, delta, tmday
        CHARACTER title(4)*80, source*20, basin*40
        CHARACTER*8 pname(4), punit(4)

        COMMON julday, tmday, delta, npoll, locats, Q, poll
        COMMON Qo, cnov, ntype, nloops, month, mseasn, mit
        COMMON Qallow, ovcon, source, triba, jce
        COMMON idatez, title, tzero, NLOC, pname, punit, ndim
        COMMON qconv, nc2, Vcso, dt, basin, ovcol, nerr
 
        INTEGER i, mmit,  maxit, cnin, incon(100), saexist
        INTEGER cnsa, sacon(100), k, m
        LOGICAL sanyes, found
        CHARACTER title1*60, title2*10, title3*20
        CHARACTER*12 fileinf, fileov, filesa
   
        READ(1,*)title1
        READ(1,*)title2, title3
        ntype= 1
        nloops= 3

        DO m= 1, 12
          READ(1,*) month, mseasn(m), mit, mmit, maxit
        END DO
       
        READ(1,*) fileinf, cnin, Qallow
        DO i = 1, cnin
          READ(1,*)  incon(i)
        END DO

        READ(1,*) fileov, cnov
        DO i = 1, cnov
          READ(1,*)  ovcon(i)
        END DO

        READ(1,*)saexist
        IF(saexist.GE.1)THEN
          sanyes= .true.
        ELSE
          sanyes= .false.
        ENDIF
        IF(sanyes) THEN
          READ(1,*) filesa, cnsa
          DO i = 1, cnsa
            READ(1,*)  sacon(i)
          END DO
        ENDIF
C OPEN FLOW INPUT FILES  open only overflow file
        OPEN(3,FILE=fileov,FORM='UNFORMATTED') !open overflow file if the name is different
        REWIND(3)
        READ(3,ERR=2000,IOSTAT=nerr)title(1) !read title lines of overflow file
        READ(3,ERR=2000,IOSTAT=nerr)title(2)				
        READ(3,ERR=2000,IOSTAT=nerr)idatez, tzero				
        READ(3,ERR=2000,IOSTAT=nerr)title(3)
        READ(3,ERR=2000,IOSTAT=nerr)title(4)
        READ(3,ERR=2000,IOSTAT=nerr)source, locats, npoll, triba, jce
C      FINDING WHICH COLUMN OF THE INPUT FILES THE NODES OF INTEREST ARE FOUND
        READ(3,ERR=2000,IOSTAT=nerr) (NLOC(i), i=1, locats)
        DO i= 1, cnov
          found= .false.
          k=1
          DO WHILE((.NOT.found).AND.(k.LE.locats))	
            IF(ovcon(i).EQ.NLOC(k))THEN
              found= .true.
              ovcol(i)= k
            ENDIF
            k=k+1
          END DO
          IF(.NOT.found)THEN
            WRITE(*,*)'Conduit ',ovcon(i),' missing from overflow file'
            STOP
          END IF
        END DO
C     Done with Conduits(locats) Loops'
C
C     Read in pollutant garbage if it exists!'
        IF(npoll.GT.0)THEN
          READ(3) (pname(i),i=1,npoll)
          READ(3) (punit(i),i=1,npoll)
          READ(3) (ndim(i),i=1,npoll)
        ENDIF
        READ(3) qconv

        nc2= 0
        Vcso= -1
        Basin= 'N/A'

2000  RETURN
      END

************************************************************************
************************************************************************
************************************************************************
       
      SUBROUTINE rdfile
        IMPLICIT none

        INTEGER julday, locats, poll(4,100), npoll, cnov, ntype
        INTEGER mseasn(12), mit, ovcon(100), jce, idatez, nloops
        INTEGER NLOC(100), ndim(4), nc2, ovcol(100), month, nerr
        REAL  Q(100), Qo, Qallow, triba, tzero, qconv, Vcso
        REAL dt, delta, tmday
        CHARACTER title(4)*80, source*20, basin*40
        CHARACTER*8 pname(4), punit(4)

        COMMON julday, tmday, delta, npoll, locats, Q, poll
        COMMON Qo, cnov, ntype, nloops, month, mseasn, mit
        COMMON Qallow, ovcon, source, triba, jce
        COMMON idatez,title, tzero, NLOC, pname, punit, ndim
        COMMON qconv, nc2, Vcso, dt, basin, ovcol, nerr
   
        INTEGER j, k, i

        IF(npoll.GT.0)THEN
          READ(3,ERR=2000,IOSTAT=nerr) julday, tmday, delta,
     +      (Q(k),(poll(j,k),j=1,npoll), k=1,locats)
        ELSE
          READ(3,ERR=2000,IOSTAT=nerr) julday, tmday, delta,
     +      (Q(i), i=1,locats)
        ENDIF
        Qo= 0
        DO i= 1, cnov
          Qo= Qo + Q(ovcol(i))
        END DO
*          dt= delta
2000  RETURN
      END