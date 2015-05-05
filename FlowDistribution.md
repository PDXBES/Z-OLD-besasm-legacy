# Introduction #

Instructions for using the Flow Distribution Tool with InterfaceMaster

# Details #

## Changes ##

| By | Date | Comments |
|:---|:-----|:---------|
| Arnel Mandilag | 8/8/2008 | Clarified example and specs |
| John Hall | 1/20/2010 | Resolved minor editing questions |

## Summary ##

The purpose of the flow distribution tool is to distribute a hydrograph that represents the flow at a down¬stream point in a basin to multiple locations in the basin.  The distribution assumes that flow enters the system proportional to an attribute of each tax lot.  The inputs to the tool are as follows:

  * **EMGAATS model network:**  A standard Explicit Model Generation and Analysis Tool Set (EMGAATS) model specifies the network for flows to be distributed to.
  * Hydrographs: One or more hydrographs can be distributed.  The hydrographs are delimited text files with three columns.  The order of the columns is date, time, and flow.  The user can specify the format of the date and time columns.  A multiplier may be applied to the hydrograph.  The multiplier could be used for the following purposes:
    * A conversion factor, such as 35.3147 to convert from cubic meters per second to cubic feet per second
    * An area ratio, such as when a model from one basin is used to generate flows for another basin
    * A calibration factor
> Any combination of these possibilities should be multiplied together to arrive at the entry provided in the configuration file (item 3 below).
> A velocity factor is also provided that slows down or speeds up the lag as the hydrograph is distributed up the network.  Velocity factors higher than 1 speed up the lag (i.e., decrease the time between when the most upstream point starts generating flow and the point where the hydrograph is presumably extracted).  A velocity factor of 1 uses the full flow velocity of all pipes.  Velocity factors lower than 1 slow down the lag.  In all cases, the velocity factor is multiplied against the full flow velocity of each pipe to determine the amount of lag.
  * **Distribution configuration (ini) file:**  This is a text file that describes inputs and formatting to the tool.  The configuration file format is shown in the following examples.  Note that the file is case-sensitive.

> Example Distribution Configuration
```
[main]
modelpath=(directory path where model.ini is located)
ndistribs=(number of hydrographs to distribute)
linkidfield=LinkID (name of the field in mdl_Links_ac that should be used for determining the root and stoplinks. Default is ‘LinkID’.  ‘MLinkID’ can also be used.  This entry is optional.)
nodefield=NGTOSan (name of the field in distributionsource.distributiontable that links the parcel to the node)
distributionsource=(filename of MDB containing table/query to be used for distribution; include extension; path must be relative to modelpath; optional, but if provided, also provide distributiontable; if not provided, defaults to \dsc\mdl_dirsc_ac.mdb; if the file is at the root of the model, lead with a backslash, e.g., \flowdistribution.mdb)
distributiontable=(table/query in distributionsource to be used for calculating distribution; optional, but if provided, also provide distributionsource; if not provided, defaults to mdl_dirsc_ac)
distributionfield=Areaft  (name of the field in distributionsource.distributiontable that is the basis for flow distribution; distribution is based on (sum(distributionfield.value) for each nodefield)/sum(distributionfield.value))) 
starttime=MM/DD/YYYY HH:MM:SS (start time for output interface; optional)
endtime=MM/DD/YYYY HH:MM:SS (end time for output interface; optional) 
timestep=(seconds between each time step in output interface)

Repeat [ndistrib#] and [stoplinks#] sections below ndistribs times. 

[ndistrib#]
hydrographpath = (full path of hydrograph file)
multiplier = (numeric)
velocityfactor = (numeric)
nheaderlines= (number of header lines)
dateformat=YYYY-MM-DD (quote the format specifications)
timeformat=HH:MM:SS
delimiter=” “ (space or comma would be typical; surround with quotes; for tabs, use ‘#9’; can use multiple characters, with each one counting as a delimiter)
rootlink=(LinkID where hydrograph file was extracted from; i.e., most downstream link)

[stoplinks###] (Optional; if no stop links are provided, will trace as far up as possible )
555=
666=

Example Distribution Configuration Using the AreaFt Field in ..\dsc\mdl_dirsc_ac.mdb
[main]
modelpath=C:\Blahblah_Model
ndistribs=1
nodefield=NGTOSan
distributionfield=AreaFt
;starttime=12/14/04 11:00
timestep=60

[ndistrib1]
hydrographpath=C:\Blahblah_Model\Flows\AAS791.txt
multiplier=1
velocity=0.5
nheaderlines=1
dateformat=m/d/yy
timeformat=h:mm
delimiter=' #9'
rootlink=769

[stoplinks1]
772=
27=
```

> Example Flow File
```
DATE	TIME	RDII
12/14/04 11:00	0.228375062
12/14/04 12:00	0.03933334
12/14/04 13:00	0.210031256
12/14/04 14:00	0.124156263
12/14/04 15:00	0.174187467
12/14/04 16:00	0.157833344
12/14/04 17:00	0.334083286
12/14/04 18:00	0.62361105
12/14/04 19:00	0.052166644
12/14/04 20:00	0.017874926
12/14/04 21:00	0.012694448
12/14/04 22:00	0.346388804
12/14/04 23:00	0.246833301
```

## Using the Interface ##

Run InterfaceMaster and click the Distribute Flows to Model task on the left side.  The following screen will appear:

![http://wiki.besasm-legacy.googlecode.com/hg/images/DistributeFlowsToModelUI.png](http://wiki.besasm-legacy.googlecode.com/hg/images/DistributeFlowsToModelUI.png)

  1. Click on the folder in the Distribution Configuration File box to browse for your configuration file.
  1. After reading the file and analyzing the network, the main configuration settings are shown and a list of the distributions will appear in the box labeled Distributions.  Clicking on one of them will show its properties on the right side.
  1. Click on the folder in the Save Interface File with Distributed Flows to locate where you want the output interface file to be saved.
  1. Click Run.  The tool is finished when the status boxes disappear and you can click on Close Task.

## Tool Reference ##

### Theory ###

The flow distributor performs the following [tasks? Calculations?] when it analyzes the configuration:
  1. It determines which part of the input model’s network is associated with a flow file by tracing—starting from the rootlink and as far up as possible until either the end of the network is reached or a stoplink is reached.
    1. As each link is traced, travel time is calculated using the pipe’s full flow velocity.  This velocity is multiplied by the velocityfactor before determining the travel time.
      1. For flat (zero slope) pipes, an arbitrary velocity of 1 ft/s is used to calculate travel time. [Dangles; can it be added to “a.” above?]
    1. A temporary table, IM\_DistributionNodes, is created in mdl\_links\_ac.mdb.  Temporary views (SELECT queries), named mdl\_DirSC\_ac (or distributiontable if provided) and IM\_LinkAreas, are also created.
  1. Only the nodes specified in the nodefield of each tax lot will be assigned a proportion.  Therefore, not all nodes that get traced will have a flow assigned to them.  Using NGTOSan from the mdl\_DirSC\_ac table, flows are assigned only to those nodes that have sanitary flow assigned to them. [I change or misinterpret your meaning here?](Did.md)
  1. The proportion is determined by taking each node and summing up the distributionfield values for it (let’s call this value n).  These sums are then totaled up with a grand total, Σn.  The proportion is calculated for each node as n/Σn.
  1. The tool then starts reading the flow files, scanning them until the starttime is reached.  If no starttime is provided, the earliest start time for any flow file specified in all ndistrib# sections is determined and used for the output interface file.
  1. Because the tool distributes flows upstream from the rootlink, the most upstream nodes have to receive their flow distributions earlier than when the actual flow occurs at the rootlink.  Therefore, when specifying starttime, keep in mind that this early distribution is occurring.  Depending on the size and slope of the network, as well as the velocityfactor, it can take multiple hours for the rootlink to see [?] flow from the most upstream nodes.
  1. Flows are read, distributed, and applied at the appropriate times, stepping by timestep seconds in each cycle.  Since the actual output time for any timestep may not match the time of distribution, flow is interpolated at the output time.
  1. Flows are read until either the endtime (if provided) is reached or the end of the flow files is reached, whichever comes first.
  1. The output file is in XP-binary interface file format.

### Usage ###

  * If a linkidfield is specified, the program converts it to an equivalent LinkID.  Therefore, if you choose a field that has a potential for duplicates, the equivalent LinkID can be unpredictable and so will the traces of nodes affected by the distributions.  MLinkIDs in a model can be duplicated where special links (e.g., pumps, orifices, and weirs) are located.  Therefore, if you use MLinkID, do not specify a root or stop link that sits on or is immediately next to a special link.
  * NGTOSan is the usual nodefield of choice.  However, nodes can be reassigned by adding another column in the mdl\_DirSC\_ac table that specifies how each tax lot contributes to a node in the model.  This procedure might be done to lump the distribution into fewer nodes, thereby decreasing the file size.
  * AreaFt is the usual distributionfield of choice.  However, the distribution parameter can be redeveloped by adding another column in the mdl\_DirSC\_ac table that specifies the relative contribution of a tax lot to the entire distribution area.  Another, cleaner method is to provide a distributionsource and a distributiontable, with the distributiontable having at least two fields:  a field by the same name as that provided in nodefield, and a distributionfield.  The distributiontable can be a query.
  * Subtracting flow files ...

### Upcoming Features ###

The following are planned features:
  * Ability to edit and save configurations within InterfaceMaster
  * Ability to view the results