using System;
using System.Collections;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;

namespace SystemsAnalysis.Modeling
{
  /*
    /// <summary>
    /// Strongly typed collection of DiversionStructure objects.
    /// </summary>
    public class FlowEstimationCatchments : DictionaryBase
    {

    /// <summary>
    /// Creates a collection of FlowEstimationCatchments objects containing all
    /// flow estimation catchments.
    /// </summary>
    public FlowEstimationCatchments()
    {
    DataAccess.FlowEstimationDataSet feDS;
    feDS = new DataAccess.FlowEstimationDataSet();

    DataAccess.FlowEstimationDataSetTableAdapters.FlowEstCatchmentsTableAdapter feTA;
    feTA = new DataAccess.FlowEstimationDataSetTableAdapters.FlowEstCatchmentsTableAdapter();
    feTA.Fill(feDS.FlowEstCatchments);

    foreach (FlowEstimationDataSet.FlowEstCatchmentsRow row in feDS.FlowEstCatchments)
    {
    this.Add(new FlowEstimationCatchment(row));
    }
    }

    /// <summary>
    /// Creates a FlowEstimationCatchments collection containing all FlowEstimationCatchment objects
    /// that connect to the provided Links collection.
    /// </summary>
    /// <param name="links">A Links collection connecting to the FlowEstimationCatchments of interest</param>
    public FlowEstimationCatchments(Links links)
    {
    DataAccess.FlowEstimationDataSet.FlowEstCatchmentsDataTable feDT;
    feDT = new DataAccess.FlowEstimationDataSet.FlowEstCatchmentsDataTable();

    DataAccess.FlowEstimationDataSetTableAdapters.FlowEstCatchmentsTableAdapter feTA;
    feTA = new DataAccess.FlowEstimationDataSetTableAdapters.FlowEstCatchmentsTableAdapter();
    try
    {
    feTA.Fill(feDT);
    }
    catch (Exception ex)
    {
    System.Data.DataRow[] rows;
    rows = feDT.GetErrors();
    foreach (System.Data.DataRow row in rows)
    {
    throw new Exception(row.RowError);
    }
    }

    foreach (FlowEstimationDataSet.FlowEstCatchmentsRow row in feDT)
    {
    if (links.FindByMLinkID(row.MLinkID) != null)
    {
    this.Add(new FlowEstimationCatchment(row));
    }
    }
    }      

    #region Overriden methods from DictionaryBase
    /// <summary>
    /// Gets the FlowEstimationCatchment object with the specified fecID. 		
    /// </summary>        
    public FlowEstimationCatchment this[int fecID]
    {
    [System.Diagnostics.DebuggerStepThroughAttribute]
    get { return (FlowEstimationCatchment)this.Dictionary[fecID]; }
    set { this.Dictionary[fecID] = value; }
    }
    /// <summary>
    /// Adds a DiversionStructure object to this collection.
    /// </summary>
    /// <param name="node">The DiversionStructure object to add to this collection.</param>
    public void Add(FlowEstimationCatchment fec)
    {
    this.Dictionary.Add(fec.FecID, fec);
    }
    /// <summary>
    /// Removes a FlowEstimationCatchment object from this FlowEstimationCatchments collection.
    /// </summary>
    /// <param name="fecID">The fecID of the FlowEstimationCatchment object to remove 
    /// from this FlowEstimationCatchments collection. </param>
    public void Remove(int fecID)
    {
    this.Dictionary.Remove(fecID);
    }
    /// <summary>
    /// Determines whether this FlowEstimationCatchments collection contains a FlowEstimationCatchment 
    /// object with the specified fecID.
    /// </summary>
    /// <param name="fecID"></param>
    /// <returns>true if the specified fecID was found, otherwise false.</returns>
    public bool Contains(int fecID)
    {
    return this.Dictionary.Contains(fecID);
    }
    /// <summary>
    /// Gets an ICollection containing a collection of FlowEstimationCatchmentName strings contained
    /// in the Nodes collection.
    /// </summary>
    public ICollection Keys
    {
    [System.Diagnostics.DebuggerStepThroughAttribute]
    get { return this.Dictionary.Keys; }
    }
    /// <summary>
    /// Gets an ICollection containing a collection of FlowEstimationCatchment objects
    /// contained in this FlowEstimationCatchments collection. Use this method to enumerate
    /// through the FlowEstimationCatchments using the "foreach" enumerator.
    /// </summary>
    public ICollection Values
    {
    [System.Diagnostics.DebuggerStepThroughAttribute]
    get { return this.Dictionary.Values; }
    }

    protected override void OnValidate(object key, object value)
    {
    base.OnValidate(key, value);
    if (!(value is FlowEstimationCatchment))
    {
    throw new ArgumentException("FlowEstimationCatchments collection only supports FlowEstimationCatchment objects.");
    }
    }
    #endregion

    }*/
}
