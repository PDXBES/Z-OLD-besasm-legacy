      PROGRAM WWSTATS2
*=======================================================================
* The purpose of this program is to determine wet weather events and
* their statistics. It is derived from WWFACS, which simulates
* varying storage and treatment rates for wet weather facility. A CSO
* overflow event is determined by the minimum interevent time specified
* in the input file.
*=======================================================================
* This program is derived from the original WWSTATS program created by
* Virgil Adderley, CH2M Hill/City of Portland.
*
* Program has been rewritten to batch process multiple interfaces.
*=======================================================================
*
* Variable Declarations
*
*   SWMM interface variables and seasonal trackers
      INTEGER julday, locats, poll(4,100), npoll, cnov, ntype,
     +  mseasn(12), mit, ovcon(100), jce, idatez, nloops,
     +  nloc(100), ndim(4), nc2, ovcol(100), month, nerr
      REAL q(100), qo, qallow, triba, tzero, qconv, Vcso,
     +  dt, delta, tmday
      CHARACTER title(4)*80, source*20, basin*40,
     +  label1*80, label2*80, junklabel, pname(4)*8, punit(4)*8

      COMMON julday, tmday, delta, npoll, locats, q, poll, qo, cnov,
     +  ntype, nloops, month, mseasn, mit, qallow, ovcon, source, triba,
     +  jce, idatez, title, tzero, nloc, pname, punit, ndim, qconv, nc2,
     +  vcso, dt, basin, ovcol, nerr

      DIMENSION aloc(15), sumvol(15), nevnts(15), sumdur(15),
     +  sumcdy(15), xqo3(15), xqo1(15), xqo2(15), period(15), nprd(3)

      DOUBLE PRECISION delta
      REAL evol1(15), evol2(15), evol3(15), evol4(15), pk4hrq,
     +  pk4hr1(15), pk4hr2(15), pk4hr3(15), vso4hr(49), vsomin(13),
     +  vsosum, v4hsum
      INTEGER*1 nleaps, nyr, nhour, nqtr, nseasn, nbseasn, nbmonth,
      INTEGER*2 nday, lday
      INTEGER*4 tqtrs, deltaq, calday, n, m, i, lq
      CHARACTER*10 period
      CHARACTER*250 filein, fileout, filenami, infile, aloc

      CALL ReadIn
      CALL EVStats
      CALL Output

      SUBROUTINE ReadIn
        WRITE(*,10010)
10010   FORMAT(/,10X,'Please type the input file name: ',\)
        READ(*,20)
      END
              	