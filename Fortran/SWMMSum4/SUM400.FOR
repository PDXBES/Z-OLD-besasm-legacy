      !Following includes required to process command line args
      INCLUDE 'FLIB.FI'
      INCLUDE 'FLIB.FD'      

C
C     PROGRAM: SWMMSUM
C
C     DESCRIPTION:
C         Dissects a SWMM output file, removing selected fields from the
C         Junction and Conduit summary tables, writing JUNCTION.SUM &
C         PIPE.SUM respectively.  Output format is Comma Separated Values.
C
C     FUNCTIONS:
C         FINDJUNC - Read lines from an input file until the header text
C                    for the Junction table is found
C
C     RELATED SOURCE:
C         OUTPUT.FOR from SWMM - I/O and Format statements copied liberally
C         INDAT12.FOR from SWMM - Ditto
C
C     LINKAGE:  Standard FORTRAN libraries
C               This source code is specifically coded for use with
C               Microsoft Fortran Powerstation 1.0.
C
C     VERSION    DATE    PROGRAMMER      COMMENT
C     V1.00    09/04/92  Mike White      Initial Creation.
C     V1.01    11/04/92  Virgil Adderley Review & Test Changes/Fixes
C     V2.00    Unknown   Holly Juza      Added column headers
C     V3.00    03/20/96  Arnel Mandilag  Added flags for surcharged conduits,
C                                          as well as peak velocity times
C     V3.01    03/27/96  Arnel Mandilag  Changed format to prevent crossed
C                                          eyes                               
C     V4.00    05/19/97  Arnel Mandilag  Added junction inverts, real
C                                          conduit crown elevations,
C                                          clearance from pipe crown to
C                                          ground, pipe depths, filename
C                                          of output file, and run titles.
C                                          Cleaned up code.
C     V4.01    06/12/97  Arnel Mandilag  Added invert,crown, and ground
C                                          elevations of pipe ends to pipe
C                                          summary
C     V4.02    07/09/97  Arnel Mandilag  Changed command line arguments so
C                                          that arguments without "-" are
C                                          assumed to be filenames
C     V4.03    08/04/97  Arnel Mandilag  Fixed slope values by calculating
C                                          them rather than getting them
C                                          from Extran output
C     V4.04    09/09/97  Arnel Mandilag  Expanded junction/conduit ID limits
C                                          to 9 characters
C

      Character*133 INLINE1, INLINE2   !Temp character strings from file
      Integer INFILE, JUNCTION, PIPE   !Logical unit numbers for files
      Parameter (INFILE=1, JUNCTION=2, PIPE=3)
      Integer MAXRECORDS
      Parameter (MAXRECORDS=500)
      Character TITLE1*96, TITLE2*96
      Character CHDR*50 , EHDR*50,     !Headers that denote table ends
     +   EJDAT*50,ECDAT*50 
     
      !Junction Table Vars
      Integer AJUN
      Character XID*1
      Real GRELEV,ZCROWN,YTOT,YDEV,DEPMAX,SURMAX,FTBLG
      Real SURLEN,FLDLEN,ASMAXX
      Integer IDHR,IDMIN

      !Conduit Table Vars
      Integer ACOND
      Real QFULL,VDSGN,VHGHT,QMAXX, VMAXX, QRATIO,DMAXNL,DMAXNH
      Real SUPLEN,SLOPE
      Integer IVHR,IVMIN, IQHR,IQMIN,NCOND
      Real PIPELENGTH
      Real UPCONDGRD, UPCONDCRN, UPCONDINV,
     +     DNCONDGRD, DNCONDCRN, DNCONDINV

      !Junction Data Vars
      Integer AJUNDAT(MAXRECORDS), ACONDDAT(MAXRECORDS)
      Real GROUNDDAT(MAXRECORDS), CROWNDAT(MAXRECORDS),
     +  INVERTDAT(MAXRECORDS), QINSTDAT(MAXRECORDS), YDAT(MAXRECORDS)
      Real LENGTH(MAXRECORDS), AREADUMMY, NDUMMY, WIDTHDUMMY,
     +  DEPTHDAT(MAXRECORDS), ZP1DAT(MAXRECORDS), ZP2DAT(MAXRECORDS)
      Character CLASSDUMMY*10
      Integer NJUNC1(MAXRECORDS), NJUNC2(MAXRECORDS)
      Integer DUMMY,NJUN,I,J,K
      Real REALCROWN

      !Command Line Argument Vars
      Character*255 buf
      Integer*4 argcount
      Integer*2 argstat,narg
      Logical WantPipeEnds,WantPlain,WantMap,PassFilename
C///////////////////////////////////////////////////////////////////////
C Open Files
C///////////////////////////////////////////////////////////////////////
      WRITE(*,'(A)') ' SWMMSum Version 4.04 9/7/97'
C Process command line args
      WantPipeEnds = .FALSE.
      WantPlain = .FALSE.
      WantMap = .FALSE.
      PassFilename = .FALSE.
      argcount = NARGS()
      IF (argcount.GT.1) THEN
        DO 1 narg = 1,argcount-1
          CALL GETARG(narg,buf,argstat)
          IF (buf.EQ.'-p'.OR.buf.EQ.'-P') THEN
            WantPipeEnds = .TRUE.
          ELSE IF (buf.EQ.'-d'.OR.buf.EQ.'-D') THEN
            IF (WantMap) THEN
              WRITE(*,*) ' -d and -m cannot be used together.'
              STOP
            ENDIF
            WantPlain = .TRUE.
          ELSE IF (buf.EQ.'-m'.OR.buf.EQ.'-M') THEN
            IF (WantPlain) THEN
              WRITE(*,*) ' -d and -m cannot be used together.'
              STOP
            ENDIF
            WantMap = .TRUE.
          ELSE IF (buf(1:1).EQ.'-') THEN
            WRITE(*,*) ' Argument not recognized.'
            WRITE(*,*) ' CORRECT SYNTAX: SWMMSUM4 [filename] [-p] [-d|',
     !      '-m]'
            WRITE(*,*) '   -p = print pipe end info'
            WRITE(*,*) '   -d = plain format (table headers and data)'
            WRITE(*,*) '   -m = MapInfo format'
            WRITE(*,*) '   -d and -m cannot be used together.'
            STOP
          ELSE
            INLINE1 = buf
            PassFilename = .TRUE.
          ENDIF
1       CONTINUE        
      ENDIF

C Ask user for file name
      IF (.NOT.PassFilename) THEN
        WRITE(*,'(A\)') ' Enter SWMM Output File Name>'
        READ(*,'(A)') INLINE1
      ENDIF

      OPEN (INFILE, FILE=INLINE1, ACCESS='SEQUENTIAL',STATUS='OLD')

      IF (WantMap) THEN
        OPEN (JUNCTION, FILE=INLINE1(1:6)//'XH.TXT',ACCESS='SEQUENTIAL',
     +        STATUS='UNKNOWN')
        OPEN (PIPE, FILE=INLINE1(1:6)//'XF.TXT', ACCESS='SEQUENTIAL',
     +        STATUS='UNKNOWN')
      ELSE
        OPEN (JUNCTION, FILE='JUNCTION.SUM',ACCESS='SEQUENTIAL',
     +        STATUS='UNKNOWN')
        OPEN (PIPE, FILE='PIPE.SUM', ACCESS='SEQUENTIAL',
     +        STATUS='UNKNOWN')
      ENDIF


C Headers that denote table ends
      EHDR='           * SUBCRITICAL AND CRITICAL FLOW ASSUMPTIONS FRO'
      CHDR=' *   C O N D U I T   S U M M A R Y   S T A T I S T I C S'
      ECDAT=' *                  Junction Data                       '
      EJDAT=' *        INTERNAL CONNECTIVITY INFORMATION       *     '

C///////////////////////////////////////////////////////////////////////
C
C AMM 5/19/97 Added data table processing
C
C///////////////////////////////////////////////////////////////////////

C Find Titles and print out headers for the output files
      CALL FINDTITLE(INFILE)
      READ(INFILE,'(/A)') TITLE1
      READ(INFILE,'(A)') TITLE2
      WRITE(*,'(A80,/,A80,/)') TITLE1,TITLE2
      IF (.NOT.(WantPlain.OR.WantMap)) THEN
        WRITE(JUNCTION,'(A/)') 'JUNCTION SUMMARY'
        WRITE(JUNCTION,689) INLINE1
        WRITE(JUNCTION,'(A80,/,A80,/)') TITLE1(5:),TITLE2(5:)
        WRITE(PIPE,'(A/)') 'PIPE SUMMARY'
        WRITE(PIPE,689) INLINE1
        WRITE(PIPE,'(A80,/,A80,/)') TITLE1(5:),TITLE2(5:)
      ENDIF
      
C///////////////////////////////////////////////////////////////////////
C Process CONDUIT DATA Table
C///////////////////////////////////////////////////////////////////////

C Find header of Conduit Data Table
      WRITE(*,*) '     Searching for Conduit Data Table...'
      CALL FINDCONDDATA(INFILE)
C This section reads and processes Conduit data table, looking for header
C to Junction information to denote end of conduit data
      WRITE(*,*) '     Processing Conduit Data Table...'
      NCOND = 1
C Assume at first that line has ZPs printed
10200 READ(INFILE,'(A)',END=10297) INLINE2
      IF(INLINE2(1:50).EQ.ECDAT) GOTO 10299  ! Bail out at end of conduit data
      READ(INLINE2,10289,ERR=10201) DUMMY,ACONDDAT(NCOND),LENGTH(NCOND),
     +  CLASSDUMMY,AREADUMMY,NDUMMY,WIDTHDUMMY,DEPTHDAT(NCOND),
     +  NJUNC1(NCOND),NJUNC2(NCOND),ZP1DAT(NCOND),ZP2DAT(NCOND)
      IF(DUMMY.NE.NCOND) THEN
        GOTO 10200
      ENDIF
      NCOND = NCOND + 1
      GOTO 10200
C Test if line just has no ZPs printed
10201 READ(INLINE2,10288,ERR=10200) DUMMY,ACONDDAT(NCOND),LENGTH(NCOND),
     +  CLASSDUMMY,AREADUMMY,NDUMMY,WIDTHDUMMY,DEPTHDAT(NCOND),
     +  NJUNC1(NCOND),NJUNC2(NCOND)
      IF(DUMMY.NE.NCOND) THEN
        GOTO 10200
      ENDIF
      ZP1DAT(NCOND) = 0
      ZP2DAT(NCOND) = 0
      NCOND = NCOND + 1
      GOTO 10200      
10289 FORMAT(I4,I10,F10.0,A10,F10.2,F10.5,F10.2,F10.2,2I10,2F8.2)
10288 FORMAT(I4,I10,F10.0,A10,F10.2,F10.5,F10.2,F10.2,2I10)
10297 STOP '? Unexpected EOF during Conduit Data Table' 

C///////////////////////////////////////////////////////////////////////
C Process JUNCTION DATA table
C///////////////////////////////////////////////////////////////////////

C Find header of Junction Data Table
10299 NCOND = NCOND - 1  ! Reset to actual number of conduits
      WRITE(*,'(A,I4,A)') '      Processed ',NCOND,' conduits.'
      WRITE(*,*) '     Searching for Junction Data Table...'
      CALL FINDJUNCDATA(INFILE)

C This section reads and processes Junction data table, looking for header
C to Internal connectivity information to denote end of junction data
      WRITE(*,*) '     Processing Junction Data Table...'
      NJUN = 1
10100 READ(INFILE,'(A)',END=10197) INLINE2
      IF(INLINE2(1:50).EQ.EJDAT) GOTO 10199  ! Bail out at end of junction data
      READ(INLINE2,10198,ERR=10100) DUMMY,AJUNDAT(NJUN),
     +  GROUNDDAT(NJUN),CROWNDAT(NJUN),INVERTDAT(NJUN),QINSTDAT(NJUN),
     +  YDAT(NJUN)
      IF(DUMMY.NE.NJUN) THEN
        GOTO 10100
      ENDIF
      NJUN = NJUN + 1
      GOTO 10100
10198 FORMAT(I4,I10,5F10.2)
10197 STOP '? Unexpected EOF during Junction Data Table'
              
C Find header of Junction Statistics Table
10199 NJUN = NJUN - 1  ! Reset to actual number of junctions
      WRITE(*,'(A,I4,A)') '      Processed ',NJUN,' junctions.'
      WRITE(*,*) '     Searching for Junction Summary Table...'
      CALL FINDJUNC(INFILE)
      
C///////////////////////////////////////////////////////////////////////
C Process JUNCTION SUMMARY table
C This section reads and processes Junction table, looking for header
C   to Conduit table to denote end of Junction table
C///////////////////////////////////////////////////////////////////////
          
      WRITE(*,*) '     Processing Junction Summary Table...'

      IF (WantMap) THEN
        WRITE (junction,590)	
      ELSE
        WRITE (junction,690)	
      ENDIF
      
      !Do not print units if user wants DB or map format
      IF (.NOT.(WantPlain.OR.WantMap)) THEN   
        WRITE (junction,691)                    
      ENDIF
      
      I = 1	
 110  READ (INFILE,'(A)',END=98) INLINE2
      IF(INLINE2(1:50).EQ.CHDR) GOTO 15  ! Bail out when Conduit table comes up
      READ (INLINE2,762,ERR=110) AJUN,GRELEV,ZCROWN,
     1                   YTOT,YDEV,DEPMAX,IDHR,IDMIN,
     1                   SURMAX,FTBLG,XID,SURLEN,FLDLEN,ASMAXX
      
C Calculate actual pipe crown by iterating through the conduits and
C   calculating the maximum pipe crown of all connecting conduits.  We
C   need to loop through both the upstream and downstream connections
C   to make sure we've covered all connections.      
      REALCROWN = INVERTDAT(I)
      DO 10300 J=1,NCOND
        IF (AJUN.EQ.NJUNC1(J)) THEN
          REALCROWN = MAX(REALCROWN,INVERTDAT(I)+ZP1DAT(J)+DEPTHDAT(J))
        ENDIF
10300 CONTINUE            
      DO 10305 J=1,NCOND
        IF (AJUN.EQ.NJUNC2(J)) THEN
          REALCROWN = MAX(REALCROWN,INVERTDAT(I)+ZP2DAT(J)+DEPTHDAT(J))
        ENDIF
10305 CONTINUE            
      
      IF (AJUN.GT.0) THEN
        IF (WantMap) THEN
          WRITE (JUNCTION,600) INLINE1,AJUN,IDHR,IDMIN,SURLEN,FLDLEN,
     +       DEPMAX,FTBLG,INVERTDAT(I),REALCROWN,GROUNDDAT(I)-REALCROWN
        ELSE
          WRITE (JUNCTION,700) AJUN,IDHR,IDMIN,SURLEN,FLDLEN,DEPMAX,
     +      FTBLG,INVERTDAT(I),REALCROWN,GROUNDDAT(I)-REALCROWN
        ENDIF
*       WRITE (*,700) AJUN,IDHR,IDMIN,SURLEN,FLDLEN,DEPMAX,FTBLG !Debug
        I = I + 1      
      ENDIF
      GOTO 110                                     
* AMM 3/27/96 Reformatted 690, 700; added 691
* 690  FORMAT(1X,'JUNC',7X,'HR',1X,'MIN',2X,'SURCHRG LEN',2X,
*     2    'FLOOD LEN',4X,'MAX DEPTH',6X,'CROWN',5X,'FT BELOW GRND')  
* 700  FORMAT(1X,A8, ',' ,I4,' ',I2, 5(',',1PE12.5))
 689  FORMAT('Output File: ',A65)
C Format for JUNCTION.SUM header
 690  FORMAT('  JUNCTION, HR,MIN, SURCH LEN,',1X,
     2    'FLOOD LEN,','MAX HGL,HGL<GRD,',1X,'INVERT,',
     3    2X,'CROWN,CRN<GRD')
 590  FORMAT(1X,'"Basin","Junction","Hour","Minute","Length of ',
     2       'Surcharge","Length of Flooding","Max Junction Elev",',
     2       '"Depth Below Ground Elev","Invert Elevation",',
     2       '"Crown Elevation","Crown Depth Below Ground Elev"')
 691  FORMAT(9X,'-,',3X,'-,',1X,'-,',7X,'min,',7X,'min,',2X,'el.ft,',5X,
     2  'ft,',2X,'el.ft,',2X,'el.ft,',5X,'ft')
C Format for writing junction data
 700  FORMAT(I10,',',I4,',',I2.2,2(',',F10.2),5(',',F7.2))                ! No Map
 600  FORMAT('"',A2,'",',I10,',',I4,',',I2.2,2(',',F10.2),5(',',F7.2))    ! Map
C Format for reading in junction summary table 
 762  FORMAT(1X,I9,1X,F9.2,2X,F9.2,F10.2,1X,F9.4,F9.2,I6,I5,3X,F8.2,
     2       4X,F10.2,A1,1X,F9.1,2X,F8.1,1PE11.3)
C///////////////////////////////////////////////////////////////////////
C Process CONDUIT SUMMARY table
C   This section reads and processes Conduit table
C   Looks for next table header to denote end of table
C   Record types come in two forms, second is mostly undefined
C///////////////////////////////////////////////////////////////////////


15    WRITE(*,*) '     Processing Conduit Summary Table...'
      I = 1
      IF (WantPipeEnds) THEN
        IF (WantMap) THEN
          WRITE(pipe,480)
        ELSE
          WRITE(pipe,780)
          IF (.NOT.(WantPlain.OR.WantMap)) THEN
            WRITE(pipe,781)
          ENDIF
        ENDIF
      ELSE
        IF (WantMap) THEN
          WRITE(pipe,490)
        ELSE
          WRITE(pipe,790)
          IF (.NOT.(WantPlain.OR.WantMap)) THEN
            WRITE(pipe,791)
          ENDIF
        ENDIF
      ENDIF
20    READ (INFILE,'(A)',END=97) INLINE2
      IF(INLINE2(1:50).EQ.EHDR)GOTO 50  ! Bail out when Conduit table ends
      IF(INLINE2(15:19).NE.'UNDEF') THEN
        READ(INLINE2,812,ERR=20)
     2    ACOND,QFULL,VDSGN,VHGHT,
     2    QMAXX,IQHR,IQMIN,VMAXX,
     2    IVHR,IVMIN,QRATIO,DMAXNL,DMAXNH,
     2    SUPLEN,SLOPE
        IF(ACOND.GT.0) THEN
* AMM 10/23/95 Added test for surcharged conduits
* AMM 3/20/96 Added peak velocity times
*
* AMM 6/12/97 Added conduit end inverts, grounds, crowns
          DO 10400 J=1,NCOND
            IF (ACOND.EQ.ACONDDAT(J)) THEN
              DO 10401 K=1,NJUN
                IF (NJUNC1(J).EQ.AJUNDAT(K)) THEN
                  UPCONDGRD = GROUNDDAT(K)
                  UPCONDCRN = INVERTDAT(K)+ZP1DAT(J)+DEPTHDAT(J)
                  UPCONDINV = INVERTDAT(K)+ZP1DAT(J)
                  EXIT
                ENDIF
10401         CONTINUE
              DO 10402 K=1,NJUN
                IF (NJUNC2(J).EQ.AJUNDAT(K)) THEN
                  DNCONDGRD = GROUNDDAT(K)
                  DNCONDCRN = INVERTDAT(K)+ZP2DAT(J)+DEPTHDAT(J)
                  DNCONDINV = INVERTDAT(K)+ZP2DAT(J)
                  EXIT
                ENDIF
10402         CONTINUE
              EXIT
            ENDIF
10400     CONTINUE            

* AMM 8/4/97 Fixed slope calculation; bug in SWMM's output routines
          DO 10600 J=1,NCOND
            IF (ACOND.EQ.ACONDDAT(J)) THEN
              PIPELENGTH = LENGTH(J)
              EXIT
            ENDIF
10600     CONTINUE            
          SLOPE = (UPCONDINV-DNCONDINV)/PIPELENGTH
      
          IF (((DMAXNL*12).GT.VHGHT).OR.((DMAXNH*12).GT.VHGHT)) THEN
            IF (WantPipeEnds) THEN
              IF (WantMap) THEN
                WRITE(PIPE,452) INLINE1,ACOND,IQHR,IQMIN,QFULL,QMAXX,
     +            IVHR,IVMIN,
     +            VMAXX,SLOPE*100,DEPTHDAT(I),(-(VHGHT/12-DMAXNL)),
     +            (-(VHGHT/12-DMAXNH)),UPCONDINV,UPCONDCRN,UPCONDGRD,
     +            DNCONDINV,DNCONDCRN,DNCONDGRD
              ELSE
                WRITE(PIPE,752) ACOND,IQHR,IQMIN,QFULL,QMAXX,IVHR,IVMIN,
     +            VMAXX,SLOPE*100,DEPTHDAT(I),(-(VHGHT/12-DMAXNL)),
     +            (-(VHGHT/12-DMAXNH)),UPCONDINV,UPCONDCRN,UPCONDGRD,
     +            DNCONDINV,DNCONDCRN,DNCONDGRD
              ENDIF
            ELSE
              IF (WantMap) THEN
                WRITE(PIPE,402) INLINE1,ACOND,IQHR,IQMIN,QFULL,QMAXX,
     +            IVHR,IVMIN,
     +            VMAXX,SLOPE*100,DEPTHDAT(I),(-(VHGHT/12-DMAXNL)),
     +            (-(VHGHT/12-DMAXNH))
              ELSE
                WRITE(PIPE,802) ACOND,IQHR,IQMIN,QFULL,QMAXX,IVHR,IVMIN,
     +            VMAXX,SLOPE*100,DEPTHDAT(I),(-(VHGHT/12-DMAXNL)),
     +            (-(VHGHT/12-DMAXNH))
              ENDIF
            ENDIF
          ELSE
            IF (WantPipeEnds) THEN
              IF (WantMap) THEN
                WRITE(PIPE,450) INLINE1,ACOND,IQHR,IQMIN,QFULL,QMAXX,
     +            IVHR,IVMIN,VMAXX,SLOPE*100,DEPTHDAT(I),UPCONDINV,
     +            UPCONDCRN,UPCONDGRD,DNCONDINV,DNCONDCRN,DNCONDGRD
              ELSE
                WRITE(PIPE,750) ACOND,IQHR,IQMIN,QFULL,QMAXX,IVHR,IVMIN,
     +            VMAXX,SLOPE*100,DEPTHDAT(I),UPCONDINV,UPCONDCRN,
     +            UPCONDGRD,DNCONDINV,DNCONDCRN,DNCONDGRD
              ENDIF
            ELSE
              IF (WantMap) THEN
                WRITE(PIPE,400) INLINE1,ACOND,IQHR,IQMIN,QFULL,QMAXX,
     +            IVHR,IVMIN,VMAXX,SLOPE*100,DEPTHDAT(I)
              ELSE
                WRITE(PIPE,800) ACOND,IQHR,IQMIN,QFULL,QMAXX,IVHR,IVMIN,
     +            VMAXX,SLOPE*100,DEPTHDAT(I)
              ENDIF
            ENDIF
          ENDIF
          I = I + 1
        ENDIF
      ELSE
        READ(INLINE2,813,ERR=20) ACOND,QMAXX,IQHR,IQMIN
        IF (WantMap) THEN
          WRITE(PIPE,401) INLINE1,ACOND,IQHR,IQMIN,QMAXX
        ELSE
          WRITE(PIPE,801) ACOND,IQHR,IQMIN,QMAXX
        ENDIF
      ENDIF
      GOTO 20
C Format for PIPE.SUM headers
790   FORMAT(1X,'  CONDUIT,QHR,MIN,',3X,'DES Q,',3X,'MAX Q,',
     2       'VHR,MIN, MAX V,',2X,'SLOPE,DEPTH, UPSUR, DNSUR')
791   FORMAT(9X,'-,',3X,'-, -,',5X,'cfs,',5X,'cfs,',3X,'-,',1X,'-,',
     2       3X,'fps,',6X,'%,',3X,'ft,',4X,'ft,',4X,
     2       'ft')
780   FORMAT(1X,'  CONDUIT,QHR,MIN,',3X,'DES Q,',3X,'MAX Q,',
     2       'VHR,MIN, MAX V,',2X,'SLOPE,DEPTH, UPSUR, DNSUR, UPINV,', 
     2       ' UPCRN, UPGRD, DNINV, DNCRN, DNGRD')
781   FORMAT(9X,'-,',3X,'-, -,',5X,'cfs,',5X,'cfs,',3X,'-,',1X,'-,',
     2       3X,'fps,',6X,'%,',3X,'ft,',4X,'ft,',4X,
     2       'ft',6(', el.ft'))
490   FORMAT(1X,'"Basin","Conduit","QHour","QMinute","Design Flow",',
     2       '"Max Computed Flow","VHour","VMinute",',
     2       '"Max Computed Velocity","Conduit Slope",',
     2       '"Pipe Vert Depth",',
     2       '"Max US Surcharge","Max DS Surcharge"')
480   FORMAT(1X,'"Basin","Conduit","QHour","QMinute","Design Flow",',
     2       '"Max Computed Flow","VHour","VMinute",',
     2       '"Max Computed Velocity","Conduit Slope",',
     2       '"Pipe Vert Depth",',
     2       '"Max US Surcharge","Max DS Surcharge",',
     2       '"Upstream Invert","Upstream Crown","Upstream Ground",'
     2       '"Downstream Invert","Downstream Crown",',
     2       '"Downstream Ground"')
C Format for writing conduits without surcharge
800   FORMAT(I10,',',I4,',',I2.2,2(',',F8.3),',',I4,',',I2.2,',',F6.2,  ! No pipe ends
     2       ',',F7.4,',',F5.2,2(',',6X))
750   FORMAT(I10,',',I4,',',I2.2,2(',',F8.3),',',I4,',',I2.2,',',F6.2,  ! Pipe ends
     2       ',',F7.4,',',F5.2,2(',',6X),6(',',F6.2))
400   FORMAT('"',A2,'",',I10,',',I4,',',I2.2,2(',',F8.3),',',I4,',',I2.2,  ! Map, No pipe ends
     2       ',',F6.2,',',F7.4,',',F5.2,2(',',6X))
450   FORMAT('"',A2,'",',I10,',',I4,',',I2.2,2(',',F8.3),',',I4,',',I2.2,  ! Map, Pipe ends
     2       ',',F6.2,',',F7.4,',',F5.2,2(',',6X),6(',',F6.2))
C Format for writing SWMM's auto-created conduits
801   FORMAT(I10,',',I4,',',I2.2,',   -----,' ,F8.3)                    ! No map
401   FORMAT('"',A2,'",',I10,',',I4,',',I2.2,',   -----,' ,F8.3)           ! Map
C Format for writing conduits with surcharge
802   FORMAT(I10,',',I4,',',I2.2,2(',',F8.3),',',I4,',',I2.2,',',F6.2,  ! No pipe ends
     2       ',',F7.4,',',F5.2,2(',',F6.2))
752   FORMAT(I10,',',I4,',',I2.2,2(',',F8.3),',',I4,',',I2.2,',',F6.2,  ! Pipe ends
     2       ',',F7.4,',',F5.2,2(',',F6.2),6(',',F6.2))
402   FORMAT('"',A2,'",',I10,',',I4,',',I2.2,2(',',F8.3),',',I4,',',I2.2,  ! Map, No pipe ends
     2       ',',F6.2,',',F7.4,',',F5.2,2(',',F6.2))
452   FORMAT('"',A2,'",',I10,',',I4,',',I2.2,2(',',F8.3),',',I4,',',I2.2,  ! Map, Pipe ends
     2       ',',F6.2,',',F7.4,',',F5.2,2(',',F6.2),6(',',F6.2))
C Format for reading conduit summary table
812   FORMAT(1X,I9,1PE9.2,2X,0PF8.2,1X,F9.2,1X,1PE9.2,I6,I6,2X,
     +             0PF8.2,I6,I6,2(1X,F8.2),4X,F8.2,3X,F7.1,2X,F7.5)
C Format for reading SWMM's auto-created conduits
813   FORMAT(1X,I9,29X,1PE10.2,I6,I6)

C Normal End
50    Continue
      STOP 'SWMMSum Complete.'
C Abnormal ends
98    STOP '? Unexpected EOF during Junction Table'
97    STOP '? Unexpected EOF during Conduit Table'
      END                
      
C///////////////////////////////////////////////////////////////////////
C FIND Subroutines
C AMM 5/19/97 Added following subroutines FINDTITLE, FINDCONDDATA, FINDJUNCDATA
C///////////////////////////////////////////////////////////////////////
      Subroutine FINDTITLE(FILE)
      Implicit None
      INTEGER FILE
      CHARACTER*59 LINE, HDR
      HDR='                                             ****         AN'
      REWIND FILE
12000 READ(FILE,'(A)',END=12999) LINE
      IF (LINE .EQ. HDR) RETURN
      GOTO 12000
12999 STOP '? Unexpected EOF while looking for Titles'
      RETURN
      END

      Subroutine FINDCONDDATA(FILE)
      Implicit None
      INTEGER FILE
      CHARACTER*50 LINE, HDR
      HDR=' *                     Conduit Data               '
      REWIND FILE
11000 READ(FILE,'(A)',END=11999) LINE
      IF (LINE .EQ. HDR) RETURN
      GOTO 11000
11999 STOP '? Unexpected EOF while looking for Conduit Data Table'
      RETURN
      END
      
      Subroutine FINDJUNCDATA(FILE)
      Implicit None
      INTEGER FILE
      CHARACTER*50 LINE, HDR
      HDR=' *                  Junction Data                 '
      REWIND FILE
10000 READ(FILE,'(A)',END=10999) LINE
      IF (LINE .EQ. HDR) RETURN
      GOTO 10000
10999 STOP '? Unexpected EOF while looking for Junction Data Table'
      RETURN
      END
      
C Subroutine to ignore input until Junction Table header (OR EOF) found
      Subroutine FINDJUNC(FILE)
      Implicit None
      INTEGER FILE                     !File to look through
      CHARACTER*55 LINE,HDR            !Character temps

      HDR=' *   J U N C T I O N   S U M M A R Y   S T A T I S T I C S'
      REWIND FILE
10    READ(FILE,'(A)',END=98) LINE
      IF(LINE .EQ. HDR) RETURN
      GOTO 10
98    STOP '? Unexpected EOF during Junction Summary Table'
99    RETURN
      END
