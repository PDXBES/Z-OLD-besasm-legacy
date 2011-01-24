#
# MKMF template makefile for protected mode executables.
#
FC        = f77l3
LINKER    = 386link
PROGRAM   = default.exe
DEST      = .
EXTHDRS   =
FFLAGS    =
HDRS      =
LDFLAGS   =
LDMAP     = nul
LIBS      =
MAKEFILE  = Makefile
OBJS      =
SRCS      =

$(PROGRAM):     $(OBJS) $(LIBS)
          $(LINKER) $(OBJS) -EXE $@ -MAP $(LDMAP) -LIB $(LIBS) $(LDFLAGS)

clean:;         @del -f $(OBJS)

depend:;   @mkmf -f $(MAKEFILE) PROGRAM=$(PROGRAM) DEST=$(DEST)

install:   $(PROGRAM)
                @echo Installing $(PROGRAM) in $(DEST)
                @if not $(DEST)x==.x copy $(PROGRAM) $(DEST)
