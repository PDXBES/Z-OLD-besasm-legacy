      SUBROUTINE T23CHECK(JSTOP,AGEOM)
C=======================================================================
C     ROUTINE TO CHECK THE CONNECTIONS IN TYPE 23 ELEMENTS
C     At this point, the downstream element is in GEOM3 (or KGEOM), and
c      the control element is in RULEC(k,18) (or AGEOM).  Need to 
c      1) check for existance of all elements, and 2) load RULEV(k,18)
c      with the internal element for control.
c
C=======================================================================
      INCLUDE 'TAPES.INC'
      INCLUDE 'INTER.INC'
      INCLUDE 'STIMER.INC'
      INCLUDE 'DRWF.INC'
      INCLUDE 'TABLES.INC'
      INCLUDE 'NAMES.INC'
      INCLUDE 'TST.INC'
      INCLUDE 'NEW81.INC'
      INCLUDE 'HUGO.INC'
      INCLUDE 'FLODAT.INC'
      INCLUDE 'NEWTR.INC'
CPDX--SM, 7/12/91-------------------------------------------------------
      INCLUDE 'VARY.INC'
      character bmj*10, ageom*10
      dimension ageom(net)
C=======================================================================      
      ks=0
      DO 100 K=1,NE

      IF(NTYPE(K).EQ.23)THEN
         ks=ks+1

C Ck for existance of downstream element

       ncnode=geom3(k)
       bmj=kgeom(k)
       L=nin(ncnode,bmj)
       if(L.le.0)then
         if(jce.eq.0)Write(n6,1000)ncnode,noe(k)
         if(jce.eq.1)write(n6,1001)kgeom(k),koe(k)
         jstop=1
         endif

c Ck for existance of control element different than downstream element

      If(Rulev(kstore(k),18).gt.0 .or. Ageom(kstore(k)).ne.' ')then

           ncnode   = Rulev(kstore(k),18)
           bmj      = ageom(kstore(k))
           L        = NIN(ncnode,bmj)
           if(L.le.0)then
            if(jce.eq.0)write(n6,1003)ncnode,noe(k)
            if(jce.eq.1)write(n6,1004)ageom(kstore(k)),koe(k)
            jstop=1
            else
             rulev(kstore(k),18)=L
             endif

           endif

      ENDIF

100   continue
      return
C=======================================================================      
1000  FORMAT(/' ===>ERROR!  Downstream Element ',i10,' not found for'
     +' Type 23 Flow Divider ',i10) 
1001  FORMAT(/' ===>ERROR!  Downstream Element ',a10,' not found for'
     +' Type 23 Flow Divider ',a10) 
1003  FORMAT(/' ===>ERROR!  Control Element ',i10,' not found for'
     +' Type 23 Flow Divider ',i10) 
1004  FORMAT(/' ===>ERROR!  Control Element ',a10,' not found for'
     +' Type 23 Flow Divider ',a10) 
C=======================================================================      
      end
