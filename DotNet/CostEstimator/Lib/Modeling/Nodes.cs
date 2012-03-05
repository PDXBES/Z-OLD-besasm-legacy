using System;
using System.Collections;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Utils.Events;

namespace SystemsAnalysis.Modeling
{
  /// <summary>
  /// A strongly-typed collection of Node objects.
  /// </summary>
  public class Nodes : DictionaryBase
  {

    /// <summary>
    /// Creates an empty Nodes collection.
    /// </summary>
    public Nodes()
    {
    }

    public int Load(Links links)
    {
      throw new Exception("This method has been replaced by Nodes.LoadFromMaster(Links links)");
    }
    /// <summary>
    /// Loader for a Nodes collection. This collection will contain a subset
    /// of master nodes which exist as the up- or downstream nodes in the
    /// provided Links collection.
    /// </summary>
    /// <param name="links">A Links collection whose up- and downstream
    /// nodes should be added to the this Nodes collection.  </param>
    /// <returns>The number of Node objects loaded into this collection.</returns>
    /// <remarks>This collection is filled via Load() rather than in the constructor
    /// because it may be a time consuming operation to create a Nodes collection. By
    /// loading the Nodes collection here it is possible for the calling method to listen
    /// for the StatusChanged event to monitor loading progress.  </remarks>
    public int LoadFromMaster(Links links)
    {
      DataAccess.SAMasterDataSetTableAdapters.MstNodesTableAdapter mstNodeTA;
      mstNodeTA = new DataAccess.SAMasterDataSetTableAdapters.MstNodesTableAdapter();

      //Table loaded through direct SQL statement rather than TableAdapter due to performance constraints
      string sql = "SELECT * FROM Mst_Nodes";
      string connString = mstNodeTA.Connection.ConnectionString;
      System.Data.SqlClient.SqlDataReader mstNodeReader;
      System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connString);

      System.Data.SqlClient.SqlCommand selCmd = new System.Data.SqlClient.SqlCommand(sql, conn);
      selCmd.CommandType = System.Data.CommandType.Text;
      conn.Open();
      mstNodeReader = selCmd.ExecuteReader();

      while (mstNodeReader.Read())
      {
        string nodeName = (string)mstNodeReader["Node"];
        if ((links.ContainsDSNode(nodeName)) ||
        (links.ContainsUSNode(nodeName)))
        {
          if (!this.Contains(nodeName))
          {
            Node node = new Node(mstNodeReader);
            this.Add(node);
              int quo, rem;
            quo = Math.DivRem(this.Count, 500, out rem);
            if (rem == 0)
            {
              this.OnStatusChanged(new StatusChangedArgs("Created Node #" +
              this.Count.ToString() + "."));
            }
          }
        }
      }
      mstNodeReader.Close();
      GC.Collect();
      //agMasterDataAccess.MstNodesDataAdapter.SelectCommand.Connection.Close();
      return this.Count;
    }

    /// <summary>
    /// A constructor for a Nodes collection that will contain all the Node
    /// objects in a model nodes table. 
    /// </summary>
    /// <param name="mdlNodesTable">A strongly typed DataSet containing
    /// a mdl_nodes_ac table.  </param>
    /// <returns>The number of Node objects loaded into this collection.</returns>
    /// <remarks>This collection is filled via Load() rather than in the constructor
    /// because it may be a time consuming operation to create a Nodes collection. By
    /// loading the Nodes collection here it is possible for the calling method to listen
    /// for the StatusChanged event to monitor loading progress.  </remarks>
    public int LoadFromModel(string modelPath)
    {
      ModelDataSet.MdlNodesDataTable mdlNodesDT = new ModelDataSet.MdlNodesDataTable();
      DataAccess.ModelDataSetTableAdapters.MdlNodesTableAdapter mdlNodesAdapter;
      mdlNodesAdapter = new DataAccess.ModelDataSetTableAdapters.MdlNodesTableAdapter(modelPath);
      mdlNodesAdapter.Fill(mdlNodesDT);
      foreach (ModelDataSet.MdlNodesRow mdlNodeRow in mdlNodesDT)
      {
        this.Add(new Node(mdlNodeRow));
          int quo, rem;
        quo = Math.DivRem(this.Count, 1000, out rem);
        if (rem == 0)
        {
          this.OnStatusChanged(new StatusChangedArgs("Created Node #" +
          this.Count.ToString() + "."));
        }
      }
      return this.Count;
    }

    public int Load(ModelDataSet.MdlNodesDataTable mdlNodesTable)
    {
      throw new Exception("This method has been replaced by LoadFromModel(string modelPath)");
    }
    #region Overriden methods from DictionaryBase
    /// <summary>
    /// Gets the Node object with the specified nodeName. 		
    /// </summary>
    public Node this[string nodeName]
    {
      get
      {
        return (Node)this.Dictionary[nodeName];
      }
      set
      {
        this.Dictionary[nodeName] = value;
      }
    }
    /// <summary>
    /// Adds a Node object to this collection.
    /// </summary>
    /// <param name="node">The Node object to add to this collection.</param>
    public void Add(Node node)
    {
      this.Dictionary.Add(node.Name, node);
    }
    /// <summary>
    /// Removes a Node object from this Nodes collection.
    /// </summary>
    /// <param name="nodeName">The nodeName of the Node object to remove 
    /// from this Nodes collection.   </param>
    public void Remove(string nodeName)
    {
      this.Dictionary.Remove(nodeName);
    }
    /// <summary>
    /// Determines whether this Nodes collection contains a Node 
    /// object with the specified nodeName.
    /// </summary>
    /// <param name="nodeName"></param>
    /// <returns>true if the specified nodeName was found, otherwise false.</returns>
    public bool Contains(string nodeName)
    {
      return this.Dictionary.Contains(nodeName);
    }
    /// <summary>
    /// Gets an ICollection containing a collection of NodeName strings contained
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
    /// Gets an ICollection containing a collection of Node objects
    /// contained in this Nodes collection. Use this method to enumerate
    /// through the Nodes using the "foreach" enumerator.
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
      if (!(value is Node))
      {
        throw new ArgumentException("Nodes only supports Node objects.");
      }
    }
    #endregion

    /// <summary>
    /// Event that occurs when Nodes collection reports that it's status has changed.
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
