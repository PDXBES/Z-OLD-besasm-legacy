!**----------------------------------------------------------------------------
!**
!**           file : sequnf.for
!**
!**           date : April 20, 1999
!**
!**       function : The program is used to convert Lahey sequential
!**                  unformatted files to Lahey-Fujitsu style.
!**
!**  compatibility : This FORTRAN file can be compiled and run with the Lahey
!**                  LF95 language system.  Note that the code in this file
!**                  makes use of extensions to the FORTRAN 77 standard.
!**
!**  added to SWMM 4.05 file by Merlin Vilhauer
!**
!**----------------------------------------------------------------------------
      SUBROUTINE FILE_CONVERSION(II_TYPE)
      INCLUDE 'TAPES.INC'
      INCLUDE 'STIMER.INC'
      INCLUDE 'TIMER.INC'
      INCLUDE 'INTER.INC'
      INCLUDE 'DETAIL.INC'
      INCLUDE 'SUBCAT.INC'
      INCLUDE 'QUALTY.INC'
      INCLUDE 'GRWTR.INC'
      INCLUDE 'NEW88.INC'
C
cc        implicit none

      external iswhite, abort
      logical*1 iswhite
      logical first_read
      integer, parameter :: BUFSIZE=16384    ! Adjust size for optimum speed
      integer*2 i, j, trlsize
      integer len, k
      character(len=BUFSIZE) :: buf
      character*128 dosbuf
      character*60 infile, outfile
      character type*7, byte, yesno
      first_read = .TRUE.
CC      WRITE(N6,*)'N6 in file conversion '
CC      WRITE(6,*)'6 in file conversion '
CC      WRITE(*,*)'* in file conversion '
      dosbuf = ' '
cc        call getcl( dosbuf )
cc        if (index( dosbuf, '?' ) .ne. 0)                                  &
cc     & stop 'Usage: sequnf [? | {<LF90 filename> [<LF95 filename>]}]'
cc
cc        if (dosbuf .ne. ' ') then               ! if there's a command line
cc                infile = ' '
cc                outfile = ' '
cc                i = 1
cc    1           if (iswhite( dosbuf(i:i) )) then        ! turf white space
cc                        i = i+1
cc                        goto 1
cc                endif
cc                j = 1
cc    2           if (.not. iswhite( dosbuf(i:i) )) then  ! parse input filename
cc                        infile(j:j) = dosbuf(i:i)
cc                        i = i+1
cc                        j = j+1
cc                        goto 2
cc                endif
cc                if (dosbuf(i:) .ne. ' ') then           ! if output filename was typed
cc                        i = i+1                                 ! seek it
cc    3                   if (iswhite( dosbuf(i:i) )) then
cc                                i = i+1
cc                                goto 3
cc                        endif
cc                        outfile = dosbuf(i:)                    ! and get it
cc                else                                    ! else prompt for it
cc                        print *, 'Please enter LF95 output-filename: '
cc                        read(5,*) outfile
cc                endif
cc        else                                    ! else prompt for both filenames
cc                print *, 'Please enter LF90 input-filename: '
cc                read(5,*) infile
cc                print *, '   and the LF95 output-filename: '
cc                read(5,*) outfile
cc        endif
      outfile = 'newfile.dat'
 
!*  now let Fortran check for existence/validity of files
      type = 'new'
      infile = FFNAME(51)
      write(*,*)' infile ',infile
      write(*,9899)infile
 9899 format(' infile >',A,'<')
      if( infile.eq.'SCRT1.UF')then
        write(*,*)' match found for scratch file'
        return
        end if
cc      write(*,888)FFNAME(51)
cc 888  format(' Ffname >',A,'<')
cc      pause
      open( 11, infile, status='old', access='transparent', err=50 )
cc      open( 12, outfile, status=type, access='transparent', err=60 )

	read( 11 ) byte
        if (ichar(byte) .ne. 253) then                      ! decimal 253 is LF90 BOF code
          print *,' file exist for new format '
          rewind(11)
          read(11)nn_type
          write(*,*)' nn_type ',nn_type
cc          pause
          close(11)
          close(12)
        OPEN(NSCRAT(1),
     +      FILE=FFNAME(51),FORM='UNFORMATTED',STATUS='UNKNOWN')
cc          PAUSE
          II_TYPE = -ichar(byte)
          II_TYPE = nn_type
          return
        endif
      open( 12, outfile, status=type, access='transparent', err=60 )
      write(N6,*)' opened outfile '
cc   20   call get_rl(11, len)                                ! get record length
   20   continue                                            ! get record length
CC        write(N6,1977)len
CC 1977   format(' before get_rl len ',I10)
        call get_rl(11, len)                                ! get record length
        if( FIRST_READ)then
          FIRST_READ = .FALSE.
          II_TYPE = len
CC          write(N6,*)' after first read ',II_TYPE
          end if
CC        write(N6,1988)len
CC 1988   format(' after get_rl  len ',I10)
cc        if (len .lt. 0) stop 'Conversion complete'
cc        write(*,*)' after statement 20  len ',len
        if (len .lt. 0)then
          close(11)
          close(12)
cc          write(*,*)' nscrat(1) ',NSCRAT(1)
cc          pause
        OPEN(NSCRAT(1),
     +             FILE=outfile,FORM='UNFORMATTED',STATUS='SCRATCH')
         write(*,*)' returning back to main file '
         return
         end if
        write(12) len                                       ! write rec. length ind.
        do k = BUFSIZE, len, BUFSIZE                        ! (start of LF95 logical rec.)
          read(11) buf                                      ! read LF90 record
          write(12) buf                                     ! write LF95 record
        end do
cc        write(*,*)' after do loop'
        k = modulo(len,BUFSIZE)
        read(11) buf(1:k)                                   ! read/write remainder
        write(12) buf(1:k)                                  ! of record
        write(12) len                                       ! write rec. length ind.
        select case (len)                                   ! (end of LF95 logical rec.)
          case (:62)
            trlsize = 1                                     ! the trailing record length
          case (63:16381)                                   ! indicator is sometimes one
            trlsize = 2                                     ! byte larger than the leading
          case (16382:4194300)                              ! one, so we must calculate 
            trlsize = 3                                     ! it's size
          case default
            trlsize = 4
        end select
        read(11) buf(1:trlsize)                             ! read past trailing rec. length ind.
        goto 20

   50	call abort( 'input file does not exist' )

   60	call abort( 'output file already exists' )

   70   print *, "Error: '", infile(1:nblank( infile )),
     & "' is not LF90 sequential-unformatted"
	call exit( 4 )

   80 stop 'Conversion complete'  !hkj
      close(12)
      IF(NSCRAT(1).GT.0.AND.FFNAME(51).NE.'SCRT1.UF') OPEN(NSCRAT(1),
     +             FILE=FFNAME(51),FORM='UNFORMATTED',STATUS='UNKNOWN')

	end
!*****		Return TRUE if character is blank or tab (white space)
	logical*1 function iswhite( arg )
	character arg
      integer tab
      parameter (TAB=9)

	if (arg .eq. ' ' .or. arg .eq. char(TAB)) then
		iswhite = .TRUE.
	else
		iswhite = .FALSE.
	endif
	return
	end
!*****		Scram routine
	subroutine abort( msg )
	character msg*(*)
        print *, 'Error: ', msg
	call exit( 4 )
	end

        subroutine get_rl(unit,rl)              ! calculate length of fortran
                                                ! sequential record
        INCLUDE 'TAPES.INC'
        integer unit, rl, shiftbuf, i, rl_size
        character byte
        character*256 msg
        msg = " "
cc        write(N6,*)' in get_rl '
        read(unit, iostat=i) byte
        if (i < 0 .or. i == 80) then            ! If end of file detected,
          goto 100                              ! then task is complete
        elseif (i > 0) then
          write(*,*) 'iostat = ',i
          call iostat_msg(i,msg)
          write (*,*) msg
          stop
        endif
        rl=iand(ichar(byte),252)                ! Mask off upper 6 bits
        rl_size = iand(ichar(byte), 3)          ! Mask off lower 3 bits
cc        WRITE(*,9088)rl,rl_size
cc        WRITE(N6,9088)rl,rl_size
cc 9088   FORMAT(' rl rl_size ',2I10)
cc        PAUSE
        do i=1, rl_size
          read(unit) byte
          shiftbuf=ichar(byte)
          shiftbuf=ishft(shiftbuf,i*8)
          rl=ior(rl,shiftbuf)
        end do
        rl = ishft(rl, -2)
        return
  100   rl = -1                                 ! Neg. value indicates eof
        return
        end subroutine


