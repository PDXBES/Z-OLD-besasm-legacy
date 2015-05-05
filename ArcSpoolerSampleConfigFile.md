
```
<?xml version="1.0" encoding="utf-8"?>
<configuration xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="file:SpoolerConfigSchema.xsd">
  <template filename="\\cassio\ASM_Projects\E08667_TGD22\gis\spool\Data\Arc\Mxd\Dev\TGD_Spooler0321.mxd">
    <masterDataFrame name="Defects Map" layerToShift="SpoolData" layerKeyField="MAPINFO_ID" zoomToObject="true" rotateMap="false" highlightLayer="Spool_HiLite" highlightField="MAPINFO_ID" scaleInterval="25">
      <!--highlightLayer hide="true" layerName="SpoolMasker" layerField="ProjectArea" /-->
      <highlightLayer hide="false" layerName="Spool_HiLite" layerField="MAPINFO_ID" />
    </masterDataFrame>
    <dataFrame name="Overview" matchMasterZoom="false" layerName="SpoolData" layerField="rhbSID">
      <highlightLayer hide="false" layerName="Spool_HiLite" layerField="MAPINFO_ID" />
    </dataFrame>
    <dataFrame name="Condition Map" matchMasterZoom="true" layerName="SpoolData" layerField="rhbSID">
      <highlightLayer hide="false" layerName="Spool_HiLite" layerField="MAPINFO_ID" />
    </dataFrame>
  </template>
  <sourceDatabase>
    <!--connectionString>Provider=SQLOLEDB;Server=gissqlprod1.rose.portland.local\gis92;Database=EGH_PUBLIC;User ID=arcmap_user;Password=arc8;</connectionString-->
    <connectionString>Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Development\besasm-legacy\DotNet\ArcSpooler\UI\v93.mdb;User Id=admin;Password=;</connectionString>
    <sourceTable>Query1</sourceTable>
    <sourceField>rhbSID</sourceField>
  </sourceDatabase>
  <output createMXD="false" createPDF="true" pdfResolution="288">
    <path>C:\Users\arnelm\Temp</path>
    <baseName />
  </output>
  <selection name="ErrorPending">
    <objectID>1</objectID>
  </selection>
</configuration>
```

# Configuration section #

The `Configuration` section requires at least three sections:
  * a `template` section, which defines the MXD to use for creating spooled MXDs and PDFs
  * a `sourceDatabase` section, which defines the data to be used for merging into the MXD
  * an `output` section, which defines what products will be spooled

## template section ##
  * `filename` attribute: specify the path (accessible to the user) of the source MXD

### masterDataFrame section ###
  * `name` attribute: `Name` property of main map frame in MXD
  * `layerToShift` attribute: `Name` property of layer that will be used to shift the map (this layer contains the actual values retrieved from the source database) to a particular object
  * `layerKeyField` attribute: `Name` property of field of `layerToShift` that contains the actual value retrieved from the source database. This value is then used to search for an object with the exact value for the `layerKeyField`.
  * `zoomToObject` attribute: `true` if spooled map should be zoomed in as close as possible and still encompass the found object.
  * `rotateMap` attribute: `true` if spooled map should be rotated ninety degrees if aspect ratio of object fits the rotated orientation better
  * `scaleInterval` attribute: used in conjunction with `zoomToObject`, this tells the spooler to change the scale to a multiple of `scaleInterval` (e.g., if `"scaleInterval"=200`, the final scale of the spooled map will be something like 1 inch = 1200 ft)

### dataFrame section ###
  * `name` attribute: `Name` property of a "slave" map frame that will also shift as the master data frame shifts
  * `matchMasterZoom` attribute: `true` if scale of data frame will match the scale used for the master data frame

#### highlightLayer element ####
  * `hide` attribute: `true` if the object being highlighted should be turned off (and all other objects left on) -- as in the case for opening a hole through a "canopy" layer -- versus turning on only the matching object
  * `layerName` attribute: `Name` property of layer to be used for highlighting objects
  * `layerField` attribute: `Field` property of `layerName` that will be used to match value against value found in sourceDatabase

## sourceDatabase section ##

### connectionString element ###

### sourceTable element ###

### sourceField element ###

## Output section ##
  * `createMXD` attribute:
  * `createPDF` attribute:
  * `pdfResolution` attribute:

### path element ###

### basename element ###

## _Optional_ selection section ##

There can be multiple `selection` sections.

  * `name` attribute: the name of the selection set. It will appear in the list of selections provided in the UI.

### objectID element ###
There can be multiple `objectID` elements provided in this section. Supply a single value for each element. The value corresponds to the field specified in the `sourceDatabase` section's `sourceField` element. These values are used to the check on (select) the values in the UI for those elements that match the values in this list.