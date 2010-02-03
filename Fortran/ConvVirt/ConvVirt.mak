# Microsoft Developer Studio Generated NMAKE File, Based on ConvVirt.dsp
!IF "$(CFG)" == ""
CFG=ConvVirt - Win32 Debug
!MESSAGE No configuration specified. Defaulting to ConvVirt - Win32 Debug.
!ENDIF 

!IF "$(CFG)" != "ConvVirt - Win32 Release" && "$(CFG)" !=\
 "ConvVirt - Win32 Debug"
!MESSAGE Invalid configuration "$(CFG)" specified.
!MESSAGE You can specify a configuration when running NMAKE
!MESSAGE by defining the macro CFG on the command line. For example:
!MESSAGE 
!MESSAGE NMAKE /f "ConvVirt.mak" CFG="ConvVirt - Win32 Debug"
!MESSAGE 
!MESSAGE Possible choices for configuration are:
!MESSAGE 
!MESSAGE "ConvVirt - Win32 Release" (based on\
 "Win32 (x86) Console Application")
!MESSAGE "ConvVirt - Win32 Debug" (based on "Win32 (x86) Console Application")
!MESSAGE 
!ERROR An invalid configuration is specified.
!ENDIF 

!IF "$(OS)" == "Windows_NT"
NULL=
!ELSE 
NULL=nul
!ENDIF 

!IF  "$(CFG)" == "ConvVirt - Win32 Release"

OUTDIR=.\Release
INTDIR=.\Release
# Begin Custom Macros
OutDir=.\Release
# End Custom Macros

!IF "$(RECURSE)" == "0" 

ALL : "$(OUTDIR)\ConvVirt.exe"

!ELSE 

ALL : "$(OUTDIR)\ConvVirt.exe"

!ENDIF 

CLEAN :
	-@erase "$(INTDIR)\ConvVirt.obj"
	-@erase "$(OUTDIR)\ConvVirt.exe"

"$(OUTDIR)" :
    if not exist "$(OUTDIR)/$(NULL)" mkdir "$(OUTDIR)"

F90=df.exe
F90_PROJ=/include:"$(INTDIR)\\" /compile_only /nologo /warn:nofileopt\
 /module:"Release/" /object:"Release/" 
F90_OBJS=.\Release/

.SUFFIXES: .fpp

.for{$(F90_OBJS)}.obj:
   $(F90) $(F90_PROJ) $<  

.f{$(F90_OBJS)}.obj:
   $(F90) $(F90_PROJ) $<  

.f90{$(F90_OBJS)}.obj:
   $(F90) $(F90_PROJ) $<  

.fpp{$(F90_OBJS)}.obj:
   $(F90) $(F90_PROJ) $<  

RSC=rc.exe
BSC32=bscmake.exe
BSC32_FLAGS=/nologo /o"$(OUTDIR)\ConvVirt.bsc" 
BSC32_SBRS= \
	
LINK32=link.exe
LINK32_FLAGS=kernel32.lib /nologo /subsystem:console /incremental:no\
 /pdb:"$(OUTDIR)\ConvVirt.pdb" /machine:I386 /out:"$(OUTDIR)\ConvVirt.exe" 
LINK32_OBJS= \
	"$(INTDIR)\ConvVirt.obj"

"$(OUTDIR)\ConvVirt.exe" : "$(OUTDIR)" $(DEF_FILE) $(LINK32_OBJS)
    $(LINK32) @<<
  $(LINK32_FLAGS) $(LINK32_OBJS)
<<

!ELSEIF  "$(CFG)" == "ConvVirt - Win32 Debug"

OUTDIR=.\Debug
INTDIR=.\Debug
# Begin Custom Macros
OutDir=.\Debug
# End Custom Macros

!IF "$(RECURSE)" == "0" 

ALL : "$(OUTDIR)\ConvVirt.exe"

!ELSE 

ALL : "$(OUTDIR)\ConvVirt.exe"

!ENDIF 

CLEAN :
	-@erase "$(INTDIR)\ConvVirt.obj"
	-@erase "$(OUTDIR)\ConvVirt.exe"
	-@erase "$(OUTDIR)\ConvVirt.ilk"
	-@erase "$(OUTDIR)\ConvVirt.pdb"

"$(OUTDIR)" :
    if not exist "$(OUTDIR)/$(NULL)" mkdir "$(OUTDIR)"

F90=df.exe
F90_PROJ=/include:"$(INTDIR)\\" /compile_only /nologo /debug:full /optimize:0\
 /warn:nofileopt /module:"Debug/" /object:"Debug/" /pdbfile:"Debug/DF50.PDB" 
F90_OBJS=.\Debug/

.SUFFIXES: .fpp

.for{$(F90_OBJS)}.obj:
   $(F90) $(F90_PROJ) $<  

.f{$(F90_OBJS)}.obj:
   $(F90) $(F90_PROJ) $<  

.f90{$(F90_OBJS)}.obj:
   $(F90) $(F90_PROJ) $<  

.fpp{$(F90_OBJS)}.obj:
   $(F90) $(F90_PROJ) $<  

RSC=rc.exe
BSC32=bscmake.exe
BSC32_FLAGS=/nologo /o"$(OUTDIR)\ConvVirt.bsc" 
BSC32_SBRS= \
	
LINK32=link.exe
LINK32_FLAGS=kernel32.lib /nologo /subsystem:console /incremental:yes\
 /pdb:"$(OUTDIR)\ConvVirt.pdb" /debug /machine:I386\
 /out:"$(OUTDIR)\ConvVirt.exe" /pdbtype:sept 
LINK32_OBJS= \
	"$(INTDIR)\ConvVirt.obj"

"$(OUTDIR)\ConvVirt.exe" : "$(OUTDIR)" $(DEF_FILE) $(LINK32_OBJS)
    $(LINK32) @<<
  $(LINK32_FLAGS) $(LINK32_OBJS)
<<

!ENDIF 


!IF "$(CFG)" == "ConvVirt - Win32 Release" || "$(CFG)" ==\
 "ConvVirt - Win32 Debug"
SOURCE=.\ConvVirt.f90

"$(INTDIR)\ConvVirt.obj" : $(SOURCE) "$(INTDIR)"



!ENDIF 

