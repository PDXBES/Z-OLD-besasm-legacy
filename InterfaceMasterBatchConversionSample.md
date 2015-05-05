See InterfaceMasterBatchConversion for an explanation.

```
[IM_Commands]
NumCommands=2
Command1=Convert
Command2=MultiCombine

;------------------------------------------------------------------------------
; Convert: Convert multiple sources to destinations, 1-for-1
;------------------------------------------------------------------------------
[Command1]
NumFiles=2

;Possible Formats: DHI_MOUSE_PRF, SWMM_XP, SWMM_F32, SWMM_F95, SWMM_TEXT
;------------------
[Command1File1]
SourceFile=W:\Projects\7509_FannoII\MouseRDII\RDIImodels\ACF923\ACF923.PRF
SourceFormat=DHI_MOUSE_PRF
DestFile=W:\Projects\7509_FannoII\MouseRDII\RDIImodels\MultiCombined\ACF923.INT
DestFormat=SWMM_XP
Filtered=1

;FilterX=OriginalID;NewID;Include (True or False);Expression (e.g., =OriginalID+2, =OriginalID*0.2, = OriginalID1+OriginalID2)
[Command1File1Filters]
NumFilters=1
DefaultFilter=;;False;
Filter1=Link1;ACF923;True;Link1

;------------------
[Command1File2]
SourceFile=W:\Projects\7509_FannoII\MouseRDII\RDIImodels\ACF923\ACF923.PRF
SourceFormat=DHI_MOUSE_PRF
DestFile=W:\Projects\7509_FannoII\MouseRDII\RDIImodels\MultiCombined\ACF923_2.INT
DestFormat=SWMM_XP
Filtered=1

[Command1File2Filters]
NumFilters=1
DefaultFilter=;;False;
Filter1=Link1;ACF923;True;0.5*Link1

;------------------------------------------------------------------------------
; Combine; Convert multiple sources to one destination
;------------------------------------------------------------------------------
[Command2]
NumFiles=2
DestFile=P:\7900_SanitaryFacPlan\SkeletonModel\INTFiles\CombinedINT\test.INT
DestFormat=SWMM_XP

;------------------
[Command2File1]
SourceFile=P:\7900_SanitaryFacPlan\SkeletonModel\INTFiles\ABX702.INT
SourceFormat=SWMM_XP
StartDate=1/1/2003 0:00
EndDate=1/31/2004 23:59
NewStartDate=1/1/1902 0:00

;------------------
[Command2File2]
SourceFile=P:\7900_SanitaryFacPlan\SkeletonModel\INTFiles\ACG740.INT
SourceFormat=SWMM_XP
StartDate=1/1/2003 0:00
EndDate=1/31/2004 23:59
NewStartDate=1/5/1902 12:00

;NodeX=NewID;Expression
;------------------
[Command2Dest]
NumNodes=3
Node1=AAA000;ABX702@1
Node2=AAA001;ACG740@2
Node3=AAA002;ABX702@1+ACG740@2
```