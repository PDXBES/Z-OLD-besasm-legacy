using System;
using System.Collections;
using System.Data;
using SystemsAnalysis;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Types;

namespace SystemsAnalysis.Modeling.Alternatives
{
  //public delegate void OnAddLinkEventHandler(object sender, AltLinks.LinkChangedEventArgs e);
  //public delegate void OnRemoveLinkEventHandler(object sender, AltLinks.LinkChangedEventArgs e);

  /// <summary>
  /// A strongly-typed collection of Link objects.
  /// </summary>
  public class AltLinks : DictionaryBase
  {
    /* This class maintains hash tables/sorted lists of LinkIDs.  This allows retrieval of AltLink objects
        * by this field to occur rapidly.  The AltLinks class will maintain the integrity
        * of these lists as AltLinks objects are added and removed from the collection.
        */
    private Hashtable linkList;

    public AltLinks(string alternativePath)
    {
      AlternativeDataSet.AltLinksDataTable altLinksTable = new AlternativeDataSet.AltLinksDataTable();
      DataAccess.AlternativeDataSetTableAdapters.AltLinksTableAdapter altLinksTA;
      altLinksTA = new DataAccess.AlternativeDataSetTableAdapters.AltLinksTableAdapter(alternativePath);
      try
      {
        altLinksTA.Fill(altLinksTable);
        foreach (AlternativeDataSet.AltLinksRow altLinkRow in altLinksTable)
        {
          this.Add(new AltLink(altLinkRow));
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Error loading alt_links table '" + alternativePath + "': " + ex.Message, ex);
      }
    }

    /// <summary>
    /// Returns an AltLink object contained within the AltLinks collection, or null if not found.
    /// </summary>
    /// <param name="linkID">A LinkID integer that may/may not exist in AltLinks.</param>
    /// <returns>An AltLink with a corresponding LinkID, or null if not found.</returns>
    public AltLink FindByLinkID(int linkID)
    {
      if (this.linkList == null)
      {
        linkList = new Hashtable();
        foreach (AltLink l in this.Values)
        {
          if (l.LinkID != 0)
          {
            linkList.Add(l.LinkID, l);
          }
        }
      }
      return (AltLink)linkList[linkID];
    }

    #region Overriden methods from DictionaryBase
    /// <summary>
    /// Gets the Link object with the specified altLinkID. 		
    /// </summary>
    public AltLink this[int altLinkID]
    {
      get
      {
        return (AltLink)this.Dictionary[altLinkID];
      }
      set
      {
        this.Dictionary[altLinkID] = value;
      }
    }
    /// <summary>
    /// Adds a AltLink object to this collection.
    /// </summary>
    /// <param name="altLink">The AltLink object to add to this collection.</param>
    public void Add(AltLink altLink)
    {
      if (!this.Contains(altLink.AltLinkID))
      {
        this.linkList = null;
        this.Dictionary.Add(altLink.AltLinkID, altLink);
        //this.AddedLink(new LinkChangedEventArgs(link));
      }
    }
    /// <summary>
    /// Removes an AltLink object from this AltLinks collection.
    /// </summary>
    /// <param name="altLinkID">The AltLinkID of the AltLink object to remove 
    /// from this AltLinks collection.   </param>
    public void Remove(int altLinkID)
    {
      if (this.Contains(altLinkID))
      {
        //this.RemovedLink(new LinkChangedEventArgs(this[altLinkID]));
        this.linkList = null;
        this.Dictionary.Remove(altLinkID);
      }
    }
    /// <summary>
    /// Determines whether this LinkHydraulics collection contains a LinkHydraulic 
    /// object with the specified altLinkID.
    /// </summary>
    /// <param name="altLinkID"></param>
    /// <returns>true if the specified altLinkID was found, otherwise false.</returns>
    public bool Contains(int altLinkID)
    {
      return this.Dictionary.Contains(altLinkID);
    }
    /// <summary>
    /// Gets an ICollection containing a collection of AltLinkID integers contained
    /// in the Links collection.
    /// </summary>
    public ICollection Keys
    {
      get
      {
        return this.Dictionary.Keys;
      }
    }
    /// <summary>
    /// Gets an ICollection containing a collection of Link objects
    /// contained in the Links collection. Use this method to enumerate
    /// through the Links collection using the "foreach" enumerator.
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
      if (!(value is AltLink))
      {
        throw new ArgumentException("AltLinks only supports AltLink objects.");
      }
    }
    #endregion
  }
}
