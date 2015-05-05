# Introduction #
Implementation details for the Virtual Gage calculation databases (BldVirt20010614.mdb and VirtRAINDATA.mdb) can be found here.

# Details #


The InterfaceMaster application uses access VBA code in the BldVirt20010614.mdb database to compute virtual rain fall for selected quarter sections.

BldVirt20010614.mdb has two "interfaces" to InterfaceMaster

1) the virtmain function located in the VirtGage module
2) the _QSSelect table_

## OVERVIEW OF VIRTMAIN ##

virtmain computes virtual rainfall from the nearest 3 gages using the Reciprocal Distance-Squared method (See RDS function in the code)

virtmain dynamically allocates memory in a 2-D array of bytes.  The size of the array is VGID x

The array s dimentioned by LastGage x steps, where LastGage is the maximum VGID in the _RAINGAGES table and steps is the number of 5-minute time steps in the time period of interest.  Thus, the maximum number of gages is 255.  Fortunately we only have 65 so far._

## DATA MAINTENANCE ##

The actual rain data is located in the VirtRAINDATA

I have recently adapted VirtRAINDATA to extract all data in all gages from NEPTUNE to VirtRAINDATA.

The process has the following steps that are all performed from within VirtRAINDATA:
  1. Update the `_RAINGAGES` table.  (More to follow here - this is not yet automated)The `_RAINGAGES` table only needs to be updated if a gage is added or terminated
  1. Delete all rain and downtime tables
  1. Create new tables
  1. Create link tables to the new tables in BldVirt20010614
  1. Load rain data and down times.


I ran steps 2 - 5 and it require 15-minutes.  Since the update time is so fast I see no need to incrementally update the gages.  We should just wipe out the old tables and reload all data.

Note that the 999\_DOWN tables may have duplicate data.  According to Ivan Chen, it is ok for Neptune to have overlapping downtime records.

## The actual process (assuming no gages are added or removed) ##
  1. Make a backup copy of VirtRainData
  1. Run the _Maint01\_InitializeRainTables_ Macro
  1. Run the _Maint02\_AppendAllRainAndDownTables_ Macro
  1. Run the _EstablishLinksToBuildVirt_ Macro



## NEXT STEPS - LONG TERM ##
Automate the updating of the `_RAINGAGES` table
Create a simple GUI to allow updating of the data from NEPTUNE.