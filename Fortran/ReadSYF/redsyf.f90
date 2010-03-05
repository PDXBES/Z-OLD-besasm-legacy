	PROGRAM READSYF

	IMPLICIT NONE

!SYF File variables:
    INTEGER jce
    INTEGER metric
    INTEGER ntcyc
    INTEGER nj
    INTEGER ntl
    INTEGER ntc
	REAL*4  timestep
    INTEGER icyc
    INTEGER jhr
    INTEGER minute
    INTEGER jsec

!SYF File variables (Arrays):
    CHARACTER*12 AJUN[ALLOCATABLE](:)
    CHARACTER*12 ACOND[ALLOCATABLE](:)
    CHARACTER*12 KJUNC[ALLOCATABLE](:,:)
    REAL*4 GRELEV[ALLOCATABLE](:)
    REAL*4 ZCROWN[ALLOCATABLE](:)
    REAL*4 Y[ALLOCATABLE](:)
    REAL*4 Z[ALLOCATABLE](:)
    REAL*4 Q[ALLOCATABLE](:)
    REAL*4 V[ALLOCATABLE](:)
    REAL*4 A[ALLOCATABLE](:)
    REAL*4 YO[ALLOCATABLE](:)
    REAL*4 H[ALLOCATABLE](:,:)

!Local:
	INTEGER ios
	INTEGER unitno
	INTEGER debugunit
	INTEGER recno
	INTEGER rsize
	INTEGER hdrsize
	INTEGER endhdrsize
	INTEGER	ERRALLOC
	INTEGER j
	INTEGER i
	LOGICAL dbg
	CHARACTER*128 fname
	CHARACTER*128 fnamed

!Start:

	dbg = .true.

	rsize = 1
	unitno = 1
	debugunit = 2

	fname  = 'C:\XPS\XP-SWMM\PDX\WSNWNoMJ25.syf'
	fnamed = 'C:\XPS\XP-SWMM\PDX\WSNWNoMJ25.txt'
!	fname  = 'C:\XPS\XP-SWMM\WORK\testext1.syf'
!	fnamed = 'C:\XPS\XP-SWMM\WORK\testext1.txt'

	OPEN (FILE = fname, UNIT = unitno, FORM = 'BINARY', ACCESS = 'DIRECT', RECL = 4, IOSTAT = ios)

	IF (dbg) OPEN (FILE = fnamed, UNIT = debugunit, FORM = 'FORMATTED')

	recno = 1
	read (unitno, REC = recno, IOSTAT = ios) jce
	recno = recno + rsize
	read (unitno, REC = recno, IOSTAT = ios) metric
	recno = recno + rsize
	read (unitno, REC = recno, IOSTAT = ios) ntcyc
	recno = recno + rsize
	read (unitno, REC = recno, IOSTAT = ios) nj
	recno = recno + rsize
	read (unitno, REC = recno, IOSTAT = ios) ntl
	recno = recno + rsize
	read (unitno, REC = recno, IOSTAT = ios) ntc
	recno = recno + rsize
	read (unitno, REC = recno, IOSTAT = ios) timestep
	recno = recno + rsize
	hdrsize = rsize * 7

	if (dbg) then
		write (debugunit,*) 'jce     ', jce
		write (debugunit,*) 'metric  ', metric
		write (debugunit,*) 'ntcyc   ', ntcyc
		write (debugunit,*) 'nj      ', nj
		write (debugunit,*) 'ntl     ', ntl
		write (debugunit,*) 'ntc     ', ntc 
		write (debugunit,*) 'timestep', timestep
	end if

!Allocate Arrays:

	IF(NJ.GT.0) THEN
		ALLOCATE(AJUN(NJ), STAT=ERRALLOC) 
		ALLOCATE(GRELEV(NJ), STAT=ERRALLOC) 
		ALLOCATE(ZCROWN(NJ), STAT=ERRALLOC) 
		ALLOCATE(Y(NJ), STAT=ERRALLOC) 
		ALLOCATE(Z(NJ), STAT=ERRALLOC) 
	END IF 
	IF(NTL.GT.0) THEN
		ALLOCATE(ACOND(NTL), STAT=ERRALLOC) 
		ALLOCATE(KJUNC(NTL,2), STAT=ERRALLOC) 
		ALLOCATE(Q(NTL), STAT=ERRALLOC) 
	END IF 
	IF(NTC.GT.0) THEN
		ALLOCATE(V(NTC), STAT=ERRALLOC) 
		ALLOCATE(A(NTC), STAT=ERRALLOC) 
		ALLOCATE(YO(NTC), STAT=ERRALLOC) 
		ALLOCATE(H(NTL,2), STAT=ERRALLOC) 
	END IF 

	do j = 1,nj
		recno = 1 + hdrsize + (rsize * 3 * (j - 1))
		read (unitno, REC = recno, IOSTAT = ios) AJUN(J)
        if (dbg) write (debugunit,*) 'J, AJUN(J)  ', recno, j, '   ', AJUN(J)
	enddo
    do j = 1,ntl
		recno = 1 + hdrsize + rsize * 3 * nj + (rsize * 3 * (j - 1))
        read (unitno, REC = recno, IOSTAT = ios) ACOND(J)
		recno = recno + rsize * 3
        if (dbg) write (debugunit,*) 'J, ACOND(J) ', recno, j, '   ',  ACOND(J)
	enddo
    do j = 1,ntl
		recno = 1 + hdrsize + rsize * 3 * (nj + ntl) + (rsize * 6 * (j - 1))
        read (unitno, REC = recno, IOSTAT = ios) KJUNC(J,1)
		recno = recno + rsize * 3
        read (unitno, REC = recno, IOSTAT = ios) KJUNC(J,2)
        if(dbg) write (debugunit,*)'J, KJUNC(J,1..2) ', recno, j, '   ', kjunc(j,1), kjunc(j,2)
	enddo
    do j = 1,nj
		recno = 1 + hdrsize + rsize * 3 * (nj + ntl + 2 * ntl) + (rsize * (j - 1))
        read (unitno, REC = recno, IOSTAT = ios) GRELEV(J)
        if (dbg) write (debugunit,*) 'J, GRELEV(J)  ', recno, j, GRELEV(J)
	enddo
    do j = 1,nj
		recno = 1 + hdrsize + (rsize * 3 * (nj + ntl + 2 * ntl)) + (rsize * nj) + (rsize * (j - 1))
        read (unitno, REC = recno, IOSTAT = ios) ZCROWN(J)
        if (dbg) write (debugunit,*) 'J, ZCROWN(J)  ', recno, j, ZCROWN(J)
	enddo
    do j = 1,nj
		recno = 1 + hdrsize + (rsize * 3 * (nj + ntl + 2 * ntl)) + (rsize * nj * 2) + (rsize * (j - 1))
        read (unitno, REC = recno, IOSTAT = ios) Y(J)
		if (dbg) write (debugunit,*) 'J, Y(J)  ', recno, j, Y(J)
	enddo
    do j = 1,nj
		recno = 1 + hdrsize + (rsize * 3 * (nj + ntl + 2 * ntl)) + (rsize * nj * 3) + (rsize * (j - 1))
		read (unitno, REC = recno, IOSTAT = ios) Z(J)
		if (dbg) write (debugunit,*) 'J, Z(J)  ', recno, j, Z(J)
	enddo
	do j = 1,ntl
		recno = 1 + hdrsize + (rsize * 3 * (nj + ntl + 2 * ntl)) + (rsize * nj * 4) + (rsize * (j - 1))
		read (unitno, REC = recno, IOSTAT = ios) Q(J)
		if (dbg) write (debugunit,*) 'J, Q(J)  ', recno, j, Q(J)
	enddo
	do j = 1,ntc
		recno = 1 + hdrsize + (rsize * 3 * (nj + ntl + 2 * ntl)) + (rsize * nj * 4) + (rsize * ntl) + (rsize * (j - 1))
		read (unitno, REC = recno, IOSTAT = ios) V(J)
		if (dbg) write (debugunit,*) 'J, V(J)  ', recno, j, V(J)
	enddo
	do j = 1,ntc
		recno = 1 + hdrsize + (rsize * 3 * (nj + ntl + 2 * ntl)) + (rsize * (nj * 4 + ntl + ntc)) + (rsize * (j - 1))
		read (unitno, REC = recno, IOSTAT = ios) A(J)
		if (dbg) write (debugunit,*) 'J, A(J)  ', recno, j, A(J)
	enddo
	do j = 1,ntc
		recno = 1 + hdrsize + (rsize * 3 * (nj + ntl + 2 * ntl)) + (rsize * (nj * 4 + ntl + ntc * 2)) + (rsize * (j - 1))
		read (unitno, REC = recno, IOSTAT = ios) YO(J)
		if (dbg) write (debugunit,*) 'J, YO(J)  ', recno, j, YO(J)
	enddo
	do j = 1,ntc
		recno = 1 + hdrsize + (rsize * 3 * (nj + ntl + 2 * ntl)) + (rsize * (nj * 4 + ntl + ntc * 3)) + (rsize * (2 * (j - 1)))
		read (unitno, REC = recno, IOSTAT = ios) H(J,1)
		recno = recno + rsize
		read (unitno, REC = recno, IOSTAT = ios) H(J,2)
		if (dbg) write (debugunit,*) 'J, H(J,1..2)  ', recno, j, H(J,1), H(J,2)
	enddo

	endhdrsize = hdrsize + (rsize * 3 * (nj + ntl + 2 * ntl)) + (rsize * (nj * 4 + ntl + ntc * 5))

!===============================================================================================
!End of Header Data now read flow etc.
!===============================================================================================

	recno = 1 * rsize + endhdrsize

	do i = 1, ntcyc
		recno = 1 + endhdrsize + ((4 + 2 * nj + ntl + ntc) * (i - 1) * rsize)
        read (unitno, REC = recno, IOSTAT = ios) icyc
        if (dbg) write (debugunit,*) 'recno, icyc   ', recno, icyc
        recno = recno + 1 * rsize
        read (unitno, REC = recno, IOSTAT = ios) jhr
        if (dbg) write (debugunit,*) 'recno, jhr    ', recno, jhr
        recno = recno + 1 * rsize
        read (unitno, REC = recno, IOSTAT = ios) minute
        if (dbg) write (debugunit,*) 'recno, minute ', recno, minute
        recno = recno + 1 * rsize
        read (unitno, REC = recno, IOSTAT = ios) jsec
        if (dbg) write (debugunit,*) 'recno, jsec   ', recno, jsec

		do j = 1,nj
			recno = 1 + endhdrsize + (4 * rsize) + ((4 + 2 * nj + ntl + ntc) * (i - 1) * rsize) + (rsize * (j - 1))
			read (unitno, REC = recno, IOSTAT = ios) Y(J)
			if (dbg) write (debugunit,*) 'J, Y(J)  ', recno, J, Y(J)
		enddo

		do j = 1,nj
			recno = 1 + endhdrsize + (rsize * nj + 4 * rsize) + ((4 + 2 * nj + ntl + ntc) * (i - 1) * rsize) + (rsize * (j - 1))
			read (unitno, REC = recno, IOSTAT = ios) Z(J)
			if (dbg) write (debugunit,*) 'J, Z(J)  ', recno, J, Z(J)
		enddo

		do j = 1,ntl
			recno = 1 + endhdrsize + (rsize * 2 * nj + 4 * rsize) + ((4 + 2 * nj + ntl + ntc) * (i - 1) * rsize) + (rsize * (j - 1))
			read (unitno, REC = recno, IOSTAT = ios) Q(J)
			if (dbg) write (debugunit,*) 'J, Q(J)  ', recno, J, Q(J)
		enddo

		do j = 1,ntc
			recno = 1 + endhdrsize + (rsize * 2 * nj + 4 * rsize + rsize * ntl) + ((4 + 2 * nj + ntl + ntc) * (i - 1) * rsize) + (rsize * (j - 1))
			read (unitno, REC = recno, IOSTAT = ios) V(J)
			if (dbg) write (debugunit,*) 'J, V(J)  ', recno, J, V(J)
		enddo
	enddo


!Clean up:

	CLOSE (unitno)
	if (dbg) CLOSE (debugunit)

    IF (ALLOCATED(AJUN))   DEALLOCATE (AJUN)
    IF (ALLOCATED(ACOND))  DEALLOCATE (ACOND)
    IF (ALLOCATED(KJUNC))  DEALLOCATE (KJUNC)
    IF (ALLOCATED(GRELEV)) DEALLOCATE (GRELEV)
    IF (ALLOCATED(ZCROWN)) DEALLOCATE (ZCROWN)
    IF (ALLOCATED(Y))      DEALLOCATE (Y)
    IF (ALLOCATED(Z))      DEALLOCATE (Z)
    IF (ALLOCATED(Q))      DEALLOCATE (Q)
    IF (ALLOCATED(V))      DEALLOCATE (V)
    IF (ALLOCATED(YO))     DEALLOCATE (YO)
    IF (ALLOCATED(H))      DEALLOCATE (H)

    end
