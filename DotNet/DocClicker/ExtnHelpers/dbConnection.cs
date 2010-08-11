using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ExtnHelpers
{
    class dbConnection
    {
        private SqlConnection IndexConnection;
        // private SqlConnection TrimConnection;

        public void IndexConn(bool bConnect)
        {

            if (bConnect)
            {
                string sCon = "Data Source=Mosier;Initial Catalog=DocClicker;Integrated Security=SSPI"; //"Data Source=LT0008344\\SQLEXPRESS;Initial Catalog=DocClicker;Integrated Security=SSPI";
                IndexConnection = new SqlConnection(sCon);
                IndexConnection.Open();
            }
            else
            {
                if (IndexConnection != null)
                {
                    IndexConnection.Close();
                }
            }

        }
        #region "DataSet"
        public DataSet sqlDataSet()
        {
            // string sCon = "Data Source=(Mosier);Initial Catalog=DocClicker;Integrated Security=SSPI";
            // SqlConnection conn = new SqlConnection(sCon);
            if ((IndexConnection == null) || (IndexConnection.State == ConnectionState.Closed))
            {
                IndexConn(true);
            }
            

            DataSet myDataSet = new DataSet();
            myDataSet.CaseSensitive = true;
            
            try
            {
                // Get the first table.
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = IndexConnection;
                myCommand.CommandText = "Select * from SpatialIndexLayerCatalog";
                SqlDataAdapter DataAdapter = new SqlDataAdapter();
                DataAdapter.SelectCommand = myCommand;
                DataAdapter.TableMappings.Add("Table", "SpatialIndexLayerCatalog");
                DataAdapter.Fill(myDataSet);
                // Get the Second table.
                SqlCommand myCommand2 = new SqlCommand();
                myCommand2.Connection = IndexConnection;
                myCommand2.CommandText = "Select * from DocumentSpatialRelations";
                SqlDataAdapter DataAdapter2 = new SqlDataAdapter();
                DataAdapter2.SelectCommand = myCommand2;
                DataAdapter2.TableMappings.Add("Table", "DocumentSpatialRelations");
                DataAdapter2.Fill(myDataSet);

                // Establish the relationship between tables
                DataRelation datarelation;
                DataColumn dataColumn1;
                DataColumn dataColumn2;
                dataColumn1 = myDataSet.Tables["SpatialIndexLayerCatalog"].Columns["SICID"];
                dataColumn2 = myDataSet.Tables["DocumentSpatialRelations"].Columns["SICID_FK"];
                datarelation = new DataRelation("IndextoDoc", dataColumn1, dataColumn2);
                myDataSet.Relations.Add(datarelation);
                return myDataSet;
            }
            finally
            {
                // 5. Close the connection
                if (IndexConnection != null)
                {
                    IndexConnection.Close();
                }
            }

        }
        #endregion
        #region "Reader"
        public SqlDataReader sqlReader()
        {
            // 1. Instantiate the connection
            SqlConnection conn = new SqlConnection(
                "Data Source=(Mosier);Initial Catalog=DocClicker;Integrated Security=SSPI");
                            //"Data Source=(LT0008344\\SQLEXPRESS);Initial Catalog=DocClicker;Integrated Security=SSPI");


            SqlDataReader rdr = null;

            try
            {
                // Query String
                string sql = "select SP.* , DOC.* from " +
                        "SpatialIndexLayerCatalog SP, DocumentSpatialRelations DOC " +
                        "where   SP.SICID = DOC.SICID_FK";
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                SqlCommand cmd = new SqlCommand(sql, conn);

                //
                // 4. Use the connection
                //

                // get query results
                rdr = cmd.ExecuteReader();

                // return the reader

                return rdr;


            }
            finally
            {

                // 5. Close the connection
                if (conn != null)
                {
                    conn.Close();
                }



            }
        }
#endregion
        #region "Insert an record in DocumentSpatialRelations table"
        public void InsertLinkIndex(string DocumentURI, string DocumentURL, string SICID_FK, string SICKeyvalue)
        {
            try
            {
                // Open the connection
                if ((IndexConnection == null) || (IndexConnection.State == ConnectionState.Closed))
                {
                    IndexConn(true);
                }
                // prepare command string
                string insertString = @"
                 insert into DocumentSpatialRelations
                 (DocumentURI, DocumentURL, SICID_FK, SICKeyvalue )
                 values (" + DocumentURI + ", '" + DocumentURL + "', " + SICID_FK + ", '" + SICKeyvalue + "')" ;

                // 1. Instantiate a new command with a query and connection
                SqlCommand cmd = new SqlCommand(insertString, IndexConnection);

                // 2. Call ExecuteNonQuery to send command
                cmd.ExecuteNonQuery();
            }
            finally
            {
                // Close the connection
                if (IndexConnection != null)
                {
                    IndexConnection.Close();
                }
            } 

        }

        #endregion

    }
}
