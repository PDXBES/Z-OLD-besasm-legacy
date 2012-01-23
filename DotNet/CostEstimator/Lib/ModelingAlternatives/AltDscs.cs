using System;
using System.Collections;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Types;
using SystemsAnalysis.Utils.Events;

namespace SystemsAnalysis.Modeling.Alternatives
{
  /// <summary>
  /// A strongly-typed collection of DSC objects.
  /// </summary>
  public class AltDscs : DictionaryBase
  {
    /// <summary>
    /// Creates an empty DSCs collection.
    /// </summary>
    public AltDscs()
    {
    }

    public AltDscs(string alternativePath)
    {
      AlternativeDataSet.AltDscDataTable altDscTable = new AlternativeDataSet.AltDscDataTable();
      DataAccess.AlternativeDataSetTableAdapters.AltDscTableAdapter altDscTA;
      altDscTA = new DataAccess.AlternativeDataSetTableAdapters.AltDscTableAdapter(alternativePath);
      try
      {
        altDscTA.Fill(altDscTable);
        foreach (AlternativeDataSet.AltDscRow altDscRow in altDscTable)
        {
          this.Add(new AltDsc(altDscRow));
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Error loading alt_dscs table '" + alternativePath + "': " + ex.Message, ex);
      }
    }

    #region Overrided methods from DictionaryBase
    /// <summary>
    /// Gets the AltDsc object with the specified altDscID. 		
    /// </summary>
    public AltDsc this[int altDscID]
    {
      get
      {
        return (AltDsc)this.Dictionary[altDscID];
      }
      set
      {
        this.Dictionary[altDscID] = value;
      }
    }
    /// <summary>
    /// Adds a AltDsc object to this collection.
    /// </summary>
    /// <param name="altDsc">The AltDsc object to add to this collection.</param>
    public void Add(AltDsc altDsc)
    {
      this.Dictionary.Add(altDsc.AltDscID, altDsc);
    }
    /// <summary>
    /// Removes an AltDsc object from this AltDscs collection.
    /// </summary>
    /// <param name="altDscID">The altDscID of the AltDsc object to remove 
    /// from this AltDscs collection.   </param>
    public void Remove(int altDscID)
    {
      this.Dictionary.Remove(altDscID);
    }
    /// <summary>
    /// Determines whether this AltDscs collection contains an AltDsc 
    /// object with the specified altDscID.
    /// </summary>
    /// <param name="altDscID"></param>
    /// <returns>true if the specified altDscID was found, otherwise false.</returns>
    public bool Contains(int key)
    {
      return this.Dictionary.Contains(key);
    }
    /// <summary>
    /// Gets an ICollection containing a collection of altDscID integers contained
    /// in the AltDscs collection.
    /// </summary>
    public ICollection Keys
    {
      get
      {
        return this.Dictionary.Keys;
      }
    }
    /// <summary>
    /// Gets an ICollection containing a collection of AltDsc objects
    /// contained in this AltDscs collection. Use this method to enumerate
    /// through the AltDscs collection using the "foreach" enumerator.
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
      if (!(value is AltDsc))
      {
        throw new ArgumentException("AltDscs only supports AltDsc objects.");
      }
    }
    #endregion

    /// <summary>
    /// Event that occurs when AltDscs collection reports that it's status has changed.
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
