      SUBROUTINE CONVERT(STATION,IST_NO)
C======================================================================
C ROUTINE TO CONVERT INTERGERS TO CHARACTERS
C
      CHARACTER*8 STATION
*      CHARACTER*10 KAN(200)
      CHARACTER*1 CH

C======================================================================
      write(6,100)IST_NO
 100  format('  station no >',I10,'<---')
      J = 8
      STATION = '        '
      DO I = 1,8
      call CHAR_CONV(IST_NO,CH)
      STATION(J:J) = CH
      write(*,*)' station ',STATION
      IF( IST_NO .LE. 0 )THEN
        GO TO 110
        END IF
      J = J -1
      END DO
 110  CONTINUE
      END

      SUBROUTINE CHAR_CONV(IST_NO,CH)
      CHARACTER*10 ACT_NUM
      DATA ACT_NUM/'0123456789'/
      CHARACTER*1 CH
      NEW_NUM = mod(IST_NO,10)
      IF( NEW_NUM .EQ. 0 )THEN
          CH = ACT_NUM(1:1)
        ELSE
          CH = ACT_NUM(NEW_NUM+1:NEW_NUM+1)
      end if
      IST_NO = IST_NO/10
      write(*,*)' ch  stno ',CH,IST_NO
C      pause
      END

      SUBROUTINE CONVERT_CH(STATION,NUM)
C======================================================================
C ROUTINE TO CONVERT CHARACTERS TO INTERGERS
C
cc      INCLUDE 'TAPES.INC'
      CHARACTER*8 STATION
      CHARACTER*1 CH,CH2
C======================================================================
cc      write(N6,100)STATION
cc      write(*,100)STATION
cc 100  format('  station no >',A,'<---')
cc      pause
CC      J = 8
      J = 8
      NUM = 0
      MULT = 1
      DO I = 1,8
        CH = STATION(J:J)
        IX = ICHAR(CH)
cc        write(*,*)'  ch  i   IX ',CH,I,IX
cc        IF( CH .EQ. ' ' )GOTO 222
        IF( CH .NE. ' ' )then
          IX = ICHAR(CH)-48
          IVAL = IX
          NUM = NUM + IVAL * MULT
          MULT = MULT * 10
cc          write(*,*)'  num J IVAL MULT ',NUM,J,IVAL,MULT
          end if
        J = J - 1
        END DO
 222  CONTINUE
cc      WRITE(N6,*)'  NUM ',NUM
cc      WRITE(*,*)'  NUM ',NUM
cc      pause
      END

