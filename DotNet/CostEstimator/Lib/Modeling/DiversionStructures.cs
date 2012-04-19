using System;
using System.Collections;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;

namespace SystemsAnalysis.Modeling
{
  /// <summary>
  /// Strongly typed collection of DiversionStructure objects.
  /// </summary>
  public class DiversionStructures : DictionaryBase
  {
    /// <summary>
    /// Creates a collection of DiversionStructure objects containing all
    /// diversion structres in the BOM-maintaned diversion survey database.
    /// </summary>
    public DiversionStructures()
    {
      DataAccess.DiversionStructureDataSet diversionStructureDataSet;
      diversionStructureDataSet = new DataAccess.DiversionStructureDataSet();

      DataAccess.DiversionStructureDataSetTableAdapters.StructuresTableAdapter dsTA;
      dsTA = new DataAccess.DiversionStructureDataSetTableAdapters.StructuresTableAdapter();
      dsTA.Fill(diversionStructureDataSet.Structures);

      foreach (DiversionStructureDataSet.StructuresRow row in diversionStructureDataSet.Structures)
      {
        this.Add(new DiversionStructure(row));
      }
    }
    /// <summary>
    /// Creates a collection of DiversionStructure objects containing all
    /// diversion structres in the BOM-maintaned diversion survey database
    /// that exist in the provided Nodes collection.
    /// </summary>
    /// <param name="nodes">The Nodes collection that contains the list
    /// of node names to select from the BOM-maintaned survey database.  </param>
    public DiversionStructures(Nodes nodes)
    {
      DataAccess.DiversionStructureDataSet diversionStructureDataSet;
      diversionStructureDataSet = new DataAccess.DiversionStructureDataSet();

      DataAccess.DiversionStructureDataSetTableAdapters.StructuresTableAdapter dsTA;
      dsTA = new DataAccess.DiversionStructureDataSetTableAdapters.StructuresTableAdapter();
      dsTA.Fill(diversionStructureDataSet.Structures);

      foreach (DiversionStructureDataSet.StructuresRow row in diversionStructureDataSet.Structures)
      {
        if (nodes.Contains(row.HansenUnitID))
        {
          this.Add(new DiversionStructure(row));
        }
      }
    }

    #region Overriden methods from DictionaryBase
    /// <summary>
    /// Gets the DiversionStructure object with the specified divName. 		
    /// </summary>
    public DiversionStructure this[string divName]
    {
      get
      {
        return (DiversionStructure)this.Dictionary[divName];
      }
      set
      {
        this.Dictionary[divName] = value;
      }
    }
    /// <summary>
    /// Adds a DiversionStructure object to this collection.
    /// </summary>
    /// <param name="node">The DiversionStructure object to add to this collection.</param>
    public void Add(DiversionStructure div)
    {
      this.Dictionary.Add(div.DiversionName, div);
    }
    /// <summary>
    /// Removes a DiversionStructure object from this DiversionStructures collection.
    /// </summary>
    /// <param name="divName">The divName of the DiversionStructure object to remove 
    /// from this DiversionStructures collection.   </param>
    public void Remove(string divName)
    {
      this.Dictionary.Remove(divName);
    }
    /// <summary>
    /// Determines whether this DiversionStructures collection contains a DiversionStructure 
    /// object with the specified divName.
    /// </summary>
    /// <param name="divName"></param>
    /// <returns>true if the specified divName was found, otherwise false.</returns>
    public bool Contains(string divName)
    {
      return this.Dictionary.Contains(divName);
    }
    /// <summary>
    /// Gets an ICollection containing a collection of DiversionStructureName strings contained
    /// in the Nodes collection.
    /// </summary>
    public ICollection Keys
    {
      get
      {
        return this.Dictionary.Keys;
      }
    }
    /// <summary>
    /// Gets an ICollection containing a collection of DiversionStructure objects
    /// contained in this DiversionStructures collection. Use this method to enumerate
    /// through the DiversionStructures using the "foreach" enumerator.
    /// </summary>
    public ICollection Values
    {
      get
      {
        return this.Dictionary.Values;
      }
    }

    protected override void OnValidate(object key, object value)
    {
      base.OnValidate(key, value);
      if (!(value is DiversionStructure))
      {
        throw new ArgumentException("DiversionStructures collection only supports DiversionStructure objects.");
      }
    }
    #endregion
  }
}
