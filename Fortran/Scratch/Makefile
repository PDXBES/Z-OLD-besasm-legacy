#
# MKMF template makefile for protected mode executables.
#
FC        = f77l3
LINKER    = 386link
PROGRAM   = 32SCRAT.exe
DEST      = .
EXTHDRS         =
FFLAGS    =
HDRS            =
LDFLAGS   = -SYMBOLS -FULLWARN -80486 -STUB RUNB
LDMAP     = nul
LIBS      =
MAKEFILE  = Makefile
OBJS            = basnrain.obj hydrain.obj idate.obj infaceir.obj \
                  infread.obj ldate.obj mdate.obj rainbasn.obj rainrd.obj \
                  readnsc7.obj scratch.obj tsursum.obj writeint.obj \
                  convrd.obj
SRCS            = basnrain.for hydrain.for idate.for infaceir.for \
                  infread.for ldate.for mdate.for rainbasn.for rainrd.for \
                  readnsc7.for scratch.for tsursum.for writeint.for \
                  convrd.for
###   For make to take advantage of compiler response files, uncomment
###   the next four lines, and the additional three lines listed after
###   the '$(PROGRAM):   $(OBJS) $(LIBS)' line.
###.BEFORE:
###       DEL F77L3.RSP
###.for.obj:
###       ECHO $<,$@ $(FFLAGS) >>F77L3.RSP


$(PROGRAM):     $(OBJS) $(LIBS)
###       !IF -e F77L3.RSP
###           $(FC) @F77L3.RSP
###       !ENDIF
          $(LINKER) $(OBJS) -EXE $@ -MAP $(LDMAP) -LIB $(LIBS) $(LDFLAGS)

clean:;         @del -f $(OBJS)

depend:;   @mkmf -f $(MAKEFILE) PROGRAM=$(PROGRAM) DEST=$(DEST)

install:   $(PROGRAM)
                @echo Installing $(PROGRAM) in $(DEST)
                @if not $(DEST)x==.x copy $(PROGRAM) $(DEST)
### OPUS MKMF:  Do not remove this line!  Automatic dependencies follow.

infaceir.obj: INTER.INC TAPESIR.INC

infread.obj: INTER.INC STIMER.INC TAPESIR.INC

writeint.obj: INTER.INC TAPESIR.INC
