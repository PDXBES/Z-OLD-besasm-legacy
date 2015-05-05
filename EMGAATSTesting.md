# Introduction #

This page describes how to set up an EMGAATS environment offline for testing.


# Details #

  1. Copy SAMaster\_22 to another directory.
  1. Relink the tables in portal\masterportal.mdb.
  1. Create a Tools subdirectory underneath SAMaster\_22.
  1. Copy the W:\Model\_Programs\EMGAATS\binv2.2 files to the Tools subdirectory.
  1. Edit EMGAATSSystem.ini in the Tools subdirectory so that the following are correctly pathed:
    * [Admin](Admin.md)MasterFilesConfigFileName
    * [MasterFiles](MasterFiles.md)root
    * [MDBs](MDBs.md)root
    * [Code](Code.md)MIWorkbench
    * [Code](Code.md)QCWorkspace