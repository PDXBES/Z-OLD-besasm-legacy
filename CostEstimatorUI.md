# Introduction #

This section details how the user interface is designed.

# Details #

## The UI Project ##

The application framework uses a main form with a tabbed area.  As the user switches from "screen" to "screen," the application actually does this by switching tabs.  These tabs are invisible to the user, effectively creating the illusion of pages within the application.  The content of the tabs are separate forms, which are loaded in dynamically as necessary.

  * The main form is located in [Main.cs](CostEstimatorMainForm.md).
  * The template for the tab forms is located in [ChildFormTemplate.cs](CostEstimatorChildFormTemplate.md)
    * The Batch page is located in [BatchPage.cs](CostEstimatorBatch.md).  This is currently not implemented.
    * The Cost Factor Pool page is located in [CostFactorPoolPage.cs](CostEstimatorCostFactorPoolPage.md).
    * The Cost Item Pool page is located in [CostItemPoolPage.cs](CostEstimatorCostItemPoolPage.md).
    * The page which displays the costing grid is located in [CostsPage.cs](CostEstimatorCostsPage.md)
    * The reporting page is located in [FinalPage.cs](CostEstimatorFinalPage.md).
      * The report definition is located in [FormAProjectInfo.rdlc](CostEstimatorReportDefinition.md).
    * The Options page is located in [OptionsPage.cs](CostEstimatorOptionsPage.md).
    * The Project Information page is located in [ProjectInfoPage.cs](CostEstimatorProjectInfoPage.md).
    * The Select Alternative page is located in [SelectAlternativePage.cs](CostEstimatorSelectAlternativePage.md).
    * The Select Estimate Items page is located in [CostEstimatorSelectEstimatItemsPage.cs].
    * The Splash Screen is located in [SplashScreen.cs](CostEstimatorSplashScreen.md)
  * [DirectConstructionSelection.cs](CostEstimatorDirectConstructionSelection.md)
  * [EstimateItemSelection.cs](CostEstimatorEstimateItemSelection.md)
  * [LoadModelPackage.cs](CostEstimatorLoadModelPackage.md)
  * [Program.cs](CostEstimatorProgram.md)
  * [SelectedAlternative.cs](CostEstimatorSelectedAlternative.md)
  * [UserSettings.cs](CostEstimatorUserSettings.md)
  * Other files
    * Direct Construction Items definition is in [DirectConstructionItems.xml](CostEstimatorDirectConstructionItemsDefinition.md).
    * Inflow Control Cost Reference is in [InflowControlCostReference.xml](CostEstimatorInflowControlCostReference.md).
    * Pipe Cost Reference is in [PipeCostReference.xml](CostEstimatorPipeCostReference.md).
    * AltPipXP Helper application is in [AltPipXP.MBX](CostEstimatorAltPipXP.md)
      * [ListOfTbls.\*](CostEstimatorAltPipXPHelperMapInfoTable.md)
      * [QaQc\_PipXP.WOR](CostEstimatorAltPipXPQaQCWorkspace.md)