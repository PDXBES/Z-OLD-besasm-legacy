# Introduction #
Details on how the Batch Conversion .ini file is built.  A sample .ini file is [here](InterfaceMasterBatchConversionSample.md).


# Details #

`[IM_Commands]` is the header section that specifies how many commands (`NumCommands`) are in the batch, and what type each command is (Command1, Command2, etc.; you must specify at least `NumCommands` lines).  A `CommandX` line can either be "Convert" or "MultiCombine" (without quotes).

Next, specify the commands in `[CommandX]` and `[CommandXFileX]` sections.


---


The `[Command1]` section represents settings for the first command.  In this sample file, `Command1` is a Convert command.  There's only one setting for this command, `NumFiles`, that specifies how many files will be converted for the command.

For each file in a Convert command, specify the following in a `[CommandXFileX]` section:

> `SourceFile`: path to the original interface file you want to convert

> `SourceFormat`: DHI\_MOUSE\_PRF, SWMM\_XP, SWMM\_F32, SWMM\_F95, or SWMM\_TEXT.  You will most likely be using SWMM\_F95 and SWMM\_XP.

> `DestFile`: path to the new interface filename.

> `DestFormat`: same choices as `SourceFormat`

`Filtered` 1 if you want to specify what nodes to include, 0 if you just want to convert everything to another format and keep the same names and numbers.

If you set Filtered=1 for the file, you also have to follow up with a `[CommandXFileXFilters]` section:

> `NumFilters`: number of nodes you want to convert

> `FilterX`: specify as many of these as you have `NumFilters`.  Format is four items separated by semicolons: `OriginalID;NewID;Include`("True" or "False")`;Expression`

> `Expression` is an arithmetic expression using the IDs from the original interface file.  You can use +, -, `*`, / and however many terms you wish.  The terms are the original node IDs or numeric constants.  So, if you wanted to double the flow of node ABC123 and stick it into node XYZ456, you'd specify the filter as Filter1=ABC123;XYZ456;True;ABC123\*2.  Note that for DHI/PRF files, you'll need to specify links for the `OriginalID`s because those are the elements that have flow.

> `DefaultFilter`: Same format as FilterX.  Generally you only specify the Include part for this.  If you want to include all the nodes but specify only a few filter changes, use `DefaultFilter=;;True;`.  If you want to only include a few, filtered nodes, use `DefaultFilter=;;False;`.


---


The `[Command2]` section in the sample file is a MultiCombine command.  There are three settings:

> `NumFiles`: number of files to combine into a single interface

> `DestFile`: path to the single, combined interface

> `DestFormat`: DHI\_MOUSE\_PRF, SWMM\_XP, SWMM\_F32, SWMM\_F95, or SWMM\_TEXT

Follow up with `[CommandXFileX]` sections, as many as you have `NumFiles`.

> `SourceFile`: path to the file you wish to use.

> `SourceFormat`: see DestFormat above for possible entries.

> `StartDate`: start date within the source file's valid range

> `EndDate`: end date within the source file's valid range

> `NewStartDate`: if you want to offset the `StartDate` to another date, specify this line; otherwise, don't specify it.

Lastly, specify the `[CommandXDest]` section that arranges the nodes as you wish.

> `NumNodes`: the number of nodes that will be in the single, combined interface.

> `NodeX`: specify as many NodeX lines as you have NumNodes.  The format is NodeX=NewID;Expression.  NewID is the node ID you want in the combined interface.  Expression uses +, -, `*`, and / and the terms for it are of the form NodeID@NumFile or numeric constants.  For example, if you have a node ABC123 in the third file you specified (CommandXFile3), the term would be ABC123@3.