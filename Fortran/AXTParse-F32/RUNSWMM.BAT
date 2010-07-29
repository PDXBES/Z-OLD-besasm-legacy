@Echo Off
c:
ChDir c:\swmenu\fortran\axtparsr
Call AXTParse.Exe
Pkzip AB6XC.zip AB6XC.HGL AB6XC.FLO AB6XC.05I
Del AB6XC.hgl
Del AB6XC.flo
Del AB6XC.05I
