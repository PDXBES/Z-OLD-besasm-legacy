using System;
using System.Collections;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Utils.Events;

namespace SystemsAnalysis.Modeling.Alternatives
{
  /// <summary>
  /// A strongly-typed collection of Node objects.
  /// </summary>
  public class AltNodes : DictionaryBase
  {

    /// <summary>
    /// Creates an empty Nodes collection.
    /// </summary>
    public AltNodes()
    {
    }

    public AltNodes(string alternativePath)
    {
      AlternativeDataSet.AltNodesDataTable altNodesTable = new AlternativeDataSet.AltNodesDataTable();
      DataAccess.AlternativeDataSetTableAdapters.AltNodesTableAdapter altNodesTA;
      altNodesTA = new DataAccess.AlternativeDataSetTableAdapters.AltNodesTableAdapter(alternativePath);
      try
      {
        altNodesTA.Fill(altNodesTable);
        foreach (AlternativeDataSet.AltNodesRow altNodeRow in altNodesTable)
        {
          this.Add(new AltNode(altNodeRow));
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Error loading alt_nodes table '" + alternativePath + "': " + ex.Message, ex);
      }
    }

    #region Overriden methods from DictionaryBase
    /// <summary>
    /// Gets the AltNode object with the specified nodeName. 		
    /// </summary>
    public AltNode this[string nodeName]
    {
      get { return (AltNode)this.Dictionary[nodeName]; }
      set { this.Dictionary[nodeName] = value; }
    }
    /// <summary>
    /// Adds a AltNode object to this collection.
    /// </summary>
    /// <param name="node">The AltNode object to add to this collection.</param>
    public void Add(AltNode altNode)
    {
      this.Dictionary.Add(altNode.Name, altNode);
    }
    /// <summary>
    /// Removes a AltNode object from this AltNodes collection.
    /// </summary>
    /// <param name="nodeName">The nodeName of the AltNode object to remove 
    /// from this AltNodes collection. </param>
    public void Remove(string nodeName)
    {
      this.Dictionary.Remove(nodeName);
    }
    /// <summary>
    /// Determines whether this AltNodes collection contains a Node 
    /// object with the specified nodeName.
    /// </summary>
    /// <param name="nodeName"></param>
    /// <returns>true if the specified nodeName was found, otherwise false.</returns>
    public bool Contains(string nodeName)
    {
      return this.Dictionary.Contains(nodeName);
    }
    /// <summary>
    /// Gets an ICollection containing a collection of AltNodes strings contained
    /// in the AltNodes collection.
    /// </summary>
    public ICollection Keys
    {
      get { return this.Dictionary.Keys; }
    }
    /// <summary>
    /// Gets an ICollection containing a collection of Node objects
    /// contained in this Nodes collection. Use this method to enumerate
    /// through the Nodes using the "foreach" enumerator.
    /// </summary>
    public ICollection Values
    {
      get { return this.Dictionary.Values; }
    }

    protected override void OnValidate(object key, object value)
    {
      base.OnValidate(key, value);
      if (!(value is AltNode))
      {
        throw new ArgumentException("AltNodes only supports AltNode objects.");
      }
    }
    #endregion

    /// <summary>
    /// Event that occurs when AltNodes collection reports that it's status has changed.
    /// </summary>
    public event OnStatusChangedEventHandler StatusChanged;

    /// <summary>
    /// Internally called function that fires the OnStatusChanged event.
    /// </summary>
    /// <param name="e">Parameter that contains the string describing the new state.</param>
    protected virtual void OnStatusChanged(StatusChangedArgs e)
    {
      if (StatusChanged != null)
        StatusChanged(e);
    }
  }
}
