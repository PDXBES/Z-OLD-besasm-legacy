C     TAPES.INC
C=======================================================================
C     THIS IS THE PRIMARY COMMON BLOCK FOR SETTING BROAD SWMM ARRAY
C       DIMENSIONS USING PARAMETER STATEMENT.
C     SEE ALSO SUB. UNIT.FOR FOR NO. S/T PLUGS AND PRECIP.INC FOR
C       RAIN BLOCK ARRAYS (MOVED HERE, 7/30/97).
C     PARAMETER STATEMENTS ARE ALSO USED A FEW OTHER LOCATIONS:
C       INCLUDE FILES: FSPLIT, HUGO, FDII, STCOM.
C       SUBROUTINES: RDIIRES, G9RAIN, GETCUR.  
C     THIS FILE ALSO INCLUDES PRIMARY INPUT/OUTPUT FILE NAMES AND
C       UNIT NUMBERS.
C#### WCH (CDM), 8/93.  Additional parameter NTSP for type 26 flow divider
C                       in TRANSPORT block.
C#### WCH, 8/4/95.      Add parameter NCHN for number of channels
C                       connecting to any junction in EXTRAN block.
C#### CIM, 5/1/97.      Add number of rainfall gages MAXRG.
C#### CIM, 5/1/97.      Add number of VRATE/PRATE points for pump input.
C#### WCH, 7/30/97.     Add NVORF, NVTOIM from ORF.INC.
C#### WCH, 7/30,97.     Add LIMRN, LSTORM from PRECIP.INC.
C#### WCH, 7/30/97.     Add NPLUG from Subroutine UNIT.  
C=======================================================================
C     NW    = Number of subcatchments in the RUNOFF block
C     NG    = Number of channel/pipes in the RUNOFF block
C     NRQ   = Number of quality parameters in the RUNOFF block
C     NLU   = Number of different land uses in the RUNOFF block
C     NCP   = Number of RUNOFF connecting channel/pipes of subcatchments
C     NET   = Number of elements in the TRANSPORT block
C     NTSE  = Number of storage elements in the TRANSPORT block
C     NTH   = Number of input hydrographs in TRANSPORT
C     NTSP  = Number of tabular flow splits (NTYPE 26) in TRANSPORT block
C     NEE   = Number of elements in EXTRAN block
C     NGW   = Number of subcatchments with groundwater compartments
C             in RUNOFF block
C     NIE   = Number of interface locations for all blocks
C     NEP   = Number of pumps in EXTRAN block
C     NEO   = Number of orifices in EXTRAN block
C     NTG   = Number of tide gates or free outfalls in EXTRAN block
C     NEW   = Number of weirs in EXTRAN block
C     NPO   = Number of printout locations in EXTRAN block
C     NTE   = Number of tide elements in EXTRAN block
C     NNC   = Number of natural channels in EXTRAN and TRANSPORT blocks
C     NVSE  = Number of storage junctions in EXTRAN block
C     NTVAL = Number of tidal or time history points in Extran.
C     NVST  = Number of data points for variable storage elements
C             in EXTRAN block
C     NEH   = Number of input hydrographs in the EXTRAN block
C     NCHN  = Number of connecting channels to a junction in EXTRAN block
C     MAXRG = Maximum number of rainfall gages in RAIN and RUNOFF blocks
C     MAXPRA= Maximum number of PRATE/VRATE points on pump input in
C             EXTRAN block
C     NVORF = Number of variable orifices in EXTRAN
C     NVOTIM= Number of data points for variable orifices in EXTRAN
CIM  WRITING RESULTS TO ASCII FILE
C     MXFLOW= Maximum number of conduits to write EXTRAN results to 
C             ASCII file
C     LIMRN = Number of allowable precip. values/yr in RAIN
C     LSTORM= Number of storm events to be analyzed in RAIN
C     NPLUG = Number of plugs for plug-flow in S/T
C
C
C     INSTRUCTIONS - Increase dimensions of subcatchments etc.
C                    by modifying the parameter statement
C                    and recompiling your program.
C=======================================================================
* AMM 11/21/2000 Changed NCP from 6 to 11 to enable up to 10 connecting
*   subcatchments
* AMM 12/30/2002 Changed NCP from 11 to 21 to enable up to 20 connecting
*   subcatchments
      PARAMETER(NW=20000,NG=20000,NRQ=10,NLU=10,NGW=20000,NCP=20,
     +         NET=20000,
     +         NTSE=20000,NEE=1400,NTH=20000,NIE=20000,NTE=20,NEW=60,
     +         NEO=200,
     +         NEP=75,NTG=200,NPO=30,NVSE=300,NVST=25,NNC=200,
     +         NEH=400,NTVAL=500,NTSP=50,NCHN=15,MAXRG=400,MAXPRA=10,
     +         MXFLOW=150,NVORF=50,NVOTIM=50,
     +         LIMRN=5000,LSTORM=5000,NPLUG=3000)
      CHARACTER*2 CC,FFNAME*60
      COMMON /TAPES/INCNT,IOUTCT,JIN(25),JOUT(25),JCE,NOQ,
     +              NSCRAT(8),N5,N6,JKP(58),CMET(11,2),FFNAME(58),CC
