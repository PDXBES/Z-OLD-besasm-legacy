# Introduction #

The Main Form acts as the switchboard and container for most activities in the Cost Estimator.

# Details #

![http://wiki.besasm-legacy.googlecode.com/hg/images/CostEstimatorOpeningScreen.png](http://wiki.besasm-legacy.googlecode.com/hg/images/CostEstimatorOpeningScreen.png)

## Major UI Objects ##

### The Ribbon ###

The Ribbon is handled by the toolbarManager (an [UltraToolbarsManager](http://help.infragistics.com/NetAdvantage/WinForms/2010.2/CLR2.0/?page=Infragistics2.Win.UltraWinToolbars.v10.2~Infragistics.Win.UltraWinToolbars.UltraToolbarsManager.html)).

### The Tabs ###
The tabMain control is an [UltraTabControl](http://help.infragistics.com/NetAdvantage/WinForms/2010.2/CLR2.0/?page=Infragistics2.Win.UltraWinTabControl.v10.2~Infragistics.Win.UltraWinTabControl.UltraTabControl.html).  Most of the pages in the tab control make use of the [UltraGridBagLayoutPanel](http://help.infragistics.com/NetAdvantage/WinForms/2010.2/CLR2.0/?page=Infragistics2.Win.Misc.v10.2~Infragistics.Win.Misc.UltraGridBagLayoutPanel.html), which handles many variable form size issues.

**Home**. (pageHome) Provides an easy switchboard that directs users to the type of estimate they want to perform.

**Costs**. (pageCosts) Houses the [CostsPage](CostEstimatorCostsPage.md), where users will spend most of the time.

**Project Info**.

**Cost Factor Pool**. (pageCostFactorPool) Houses the [CostFactorPoolPage](CostEstimatorCostFactorPoolPage.md).  This page lists all the individual and reusable cost factors in play with the current estimate.

**Cost Item Pool**. (pageCostItemPool) Houses the [CostItemPoolPage](CostEstimatorCostItemPoolPage.md).  This page lists all the individual and reusable cost items in play with the current estimate.

**Select Alternative**. (pageSelectAlternative) After the user selects a model directory, this page appears to allow the user to select an alternative (if any) from the model.  _This is not a clean implementation since the program jumps out to a Browse Folder dialog and then back in with this page.  Ideally the UI would also allow selection of the model directory from the same general area_

**Global Settings**. (pageGlobalSettings)

**Options**. (pageOptions) This page controls global user settings for the application.

**Progress**. (pageProgress) This page is used to display progress from the background processes when an alternative or model is being loaded for and during estimation.

**Batch**. (pageBatch - placeholder for BatchPage)

**Final**. (pageFinal - placeholder for FinalPage) This page is used to display the final cost estimate reports for export to PDF or printing.

**Select Estimate Items**.

**Export**.

### The Background Workers ###

Background workers are used to separate actual processing threads from the main UI thread, allowing for more apparent responsiveness while the processing is being performed.

**bkgWorkerLoadModel**. Runs the loading of models in a separate thread.

**bkgWorkerLoadAlternative**. Runs the loading of alternatives in a separate thread.

## Miscellaneous UI Objects ##

  * ultraCalcManager1: A helper object that allows the gridGlobalPipeSettings to perform calculations.
  * dlgOpen, dlgSave, and dlgBrowseFolder: Standard dialog UI objects for getting file and directory info from the user.
  * dsGlobalPipeSettings
  * dsGlobalInflowControlSettings

# Improvement Plans #