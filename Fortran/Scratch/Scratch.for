       PROGRAM SCRATCH
C ROUTINE TO READ SWMM4 BINARY FILES BY TYPE
C
C
      WRITE(*,500)
      WRITE(*,1000)
      READ(*,*)ITYPE
      IF(ITYPE.EQ.0)STOP
*	
*	CLEAR HALF OF SCREEN
*
*	DO 10 J = 1, 24
*           WRITE(*,*)
*   10   CONTINUE
*
*
      IF(ITYPE.EQ.1)CALL INFREAD
      IF(ITYPE.EQ.2)CALL WRITEINT
      IF(ITYPE.EQ.3)CALL TSURSUM
      IF(ITYPE.EQ.4)CALL READNSC7
      IF(ITYPE.EQ.5)CALL RAINRD
      IF(ITYPE.EQ.6)CALL HYDRAIN
      IF(ITYPE.EQ.7)CALL RAINBASN
      IF(ITYPE.EQ.8)CALL BASNRAIN
      STOP
C======================================================================
 500  FORMAT( ////,15X,' *****>>>>   S C R A T C H   <<<<*****'//,
     +10X,'   By CH2M HILL/Brown & Caldwell, April, 1995'/,
     +10X,'   For City of Portland, OR CSO Facilities Project'//)
C======================================================================
1000  FORMAT(4X,'***  SWMM 4.2/PDX  UTILITY  FOR  SCRATCH  AND',
     +'  INTERFACE  FILES  ***',//
     +5X,'   Available  Functions  Are:'/
     +'          Access Standard Interface Files, JOUT...............1'/
     +'          Create Standard Interface Files, JIN................2'/
     +'          Access TRANSPORT Surcharge Summary, NSCRAT(6).......3'/
     +'          Access TRANSPORT Conduit Depths, NSCRAT(7)..........4'/
     +'          Access Rainfall Interface File, NSCRAT(1).......... 5'/
     +'          Convert HYDRA Rainfall File.........................6'/
     +'          Convert Virtual Basin Raingage File.................7'/
     +'          Create Virtual Basin Raingage File (BASNRAIN).......8'/
     +'          QUIT................................................0'/
     +//,'        PLEASE ENTER SELECTION > '\)
C======================================================================
      END
