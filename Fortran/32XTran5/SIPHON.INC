C       FILE: SIPHON.INC
C========================================================================
C       THIS FILE DEFINES AND DIMENSIONS THE VARIABLES 
C           DESCRIBING THE SIPHON WEIR GEOMETRY
C========================================================================
      INTEGER SYF
      DIMENSION LSYFON(NSYF),LSYTPE(NSYF),NSIPHN(NSYF),SYAREA(NSYF,4),
     +          SYLENG(NSYF,4),SYCRST(NSYF,4),SYFOFF(NSYF,4),
     +       SYCOEF(NSYF,4),SLPRIM(NSYF,4),NSFLOW(NSYF,4),SYF(NSYF,4)
      COMMON /SIPHON/ IS,NSYFON,LSYFON,LSYTPE,NSIPHN,SYAREA,SYCRST,
     +                SYLENG,SYFOFF,SYCOEF,NSFLOW,SLPRIM,SYF,NSYFLG
C=========================================================================
