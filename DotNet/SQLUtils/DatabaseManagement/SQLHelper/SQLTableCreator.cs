using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;


namespace SQLHelper
{
    public class SqlTableCreator
    {
        #region Instance Variables

        private SqlConnection _connection;

        public SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        private SqlTransaction _transaction;

        public SqlTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }

        private string _tableName;

        public string DestinationTableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
        #endregion

        #region Constructor

        public SqlTableCreator() { }

        public SqlTableCreator(SqlConnection connection) : this(connection, null) { }

        public SqlTableCreator(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }
        #endregion

        #region Instance Methods

        public object Create(System.Data.DataTable schema)
        {
            return Create(schema, null);
        }

        public object Create(System.Data.DataTable schema, int numKeys)
        {
            int[] primaryKeys = new int[numKeys];

            for (int i = 0; i < numKeys; i++)
            {
                primaryKeys[i] = i;
            }
            return Create(schema, primaryKeys);
        }

        public object Create(System.Data.DataTable schema, int[] primaryKeys)
        {
            string sql = GetCreateSQL(_tableName, schema, primaryKeys);
            SqlCommand cmd;

            if (_transaction != null && _transaction.Connection != null)
                cmd = new SqlCommand(sql, _connection, _transaction);
            else
                cmd = new SqlCommand(sql, _connection);

            return cmd.ExecuteNonQuery();
        }

        public object CreateFromDataTable(System.Data.DataTable table)
        {
            string sql = GetCreateFromDataTableSQL(/*_tableName*/table.TableName, table);
            SqlCommand cmd;

            if (_transaction != null && _transaction.Connection != null)
                cmd = new SqlCommand(sql, _connection, _transaction);
            else
                cmd = new SqlCommand(sql, _connection);

            return cmd.ExecuteNonQuery();
        }

        public object CreateFromDataTable(System.Data.DataTable table, String tableName)
        {
            string sql = GetCreateFromDataTableSQL(/*_tableName*/tableName, table);
            SqlCommand cmd;

            if (_transaction != null && _transaction.Connection != null)
                cmd = new SqlCommand(sql, _connection, _transaction);
            else
                cmd = new SqlCommand(sql, _connection);

            return cmd.ExecuteNonQuery();
        }
        #endregion

        #region Static Methods

        public static string GetCreateSQL(string tableName, System.Data.DataTable schema, int[] primaryKeys)
        {
            string sql = "CREATE TABLE " + tableName + " (\n";

            // columns
            foreach (System.Data.DataRow column in schema.Rows)
            {
                if (!(schema.Columns.Contains("IsHidden") && (bool)column["IsHidden"]))
                    sql += column["ColumnName"].ToString() + " " + SQLGetType(column) + ",\n";
            }

            sql = sql.TrimEnd(new char[] { ',', '\n' }) + "\n";

            // primary keys
            string pk = "CONSTRAINT PK_" + tableName + " PRIMARY KEY CLUSTERED (";
            bool hasKeys = (primaryKeys != null && primaryKeys.Length > 0);

            if (hasKeys)
            {
                // user defined keys
                foreach (int key in primaryKeys)
                {
                    pk += schema.Rows[key]["ColumnName"].ToString() + ", ";
                }
            }

            else
            {
                // check schema for keys
                string keys = string.Join(", ", GetPrimaryKeys(schema));
                pk += keys;
                hasKeys = keys.Length > 0;
            }
            pk = pk.TrimEnd(new char[] { ',', ' ', '\n' }) + ")\n";

            if (hasKeys) sql += pk;
            sql += ")";

            return sql;
        }

        public static string GetCreateFromDataTableSQL(string tableName, System.Data.DataTable table)
        {
            string sql = "CREATE TABLE [" + tableName + "] (\n";

            // columns
            foreach (System.Data.DataColumn column in table.Columns)
            {
                sql += "[" + column.ColumnName + "] " + SQLGetType(column) + ",\n";
            }

            sql = sql.TrimEnd(new char[] { ',', '\n' }) + "\n";

            // primary keys
            if (table.PrimaryKey.Length > 0)
            {
                sql += "CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED (";

                foreach (System.Data.DataColumn column in table.PrimaryKey)
                {
                    sql += "[" + column.ColumnName + "],";
                }
                sql = sql.TrimEnd(new char[] { ',' }) + "))\n";
            }
            else
            {
                sql += ")";
            }
            return sql;
        }

        public static string[] GetPrimaryKeys(System.Data.DataTable schema)
        {
            List<string> keys = new List<string>();

            foreach (System.Data.DataRow column in schema.Rows)
            {
                if (schema.Columns.Contains("IsKey") && (bool)column["IsKey"])
                    keys.Add(column["ColumnName"].ToString());
            }
            return keys.ToArray();
        }

        // Return T-SQL data type definition, based on schema definition for a column
        public static string SQLGetType(object type, int columnSize, int numericPrecision, int numericScale)
        {
            switch (type.ToString())
            {
                case "System.String":
                    return "VARCHAR(" + ((columnSize == -1) ? 255 : columnSize) + ")";

                case "System.Decimal":
                    if (numericScale > 0)
                        return "REAL";
                    else if (numericPrecision > 10)
                        return "BIGINT";
                    else
                        return "INT";

                case "System.Double":
                case "System.Single":
                    return "REAL";

                case "System.Int64":
                    return "BIGINT";

                case "System.Int16":
                case "System.Int32":
                    return "INT";

                case "System.DateTime":
                    return "DATETIME";

                //Please do not add support for boolean[] or bit[]
                //or whatever because those fields are from tables that
                //have been transferred to SQL server from Access and
                //have likely not yet been given a primary key and has 
                //not yet had the timestamp field removed.
                case "System.Boolean":
                    return "BIT";
                default:
                    throw new Exception(type.ToString() + " not implemented.");
            }
        }
        // Overload based on row from schema table 
        public static string SQLGetType(System.Data.DataRow schemaRow)
        {
            return SQLGetType(schemaRow["DataType"],
                int.Parse(schemaRow["ColumnSize"].ToString()),
                int.Parse(schemaRow["NumericPrecision"].ToString()),
                int.Parse(schemaRow["NumericScale"].ToString()));

        }
        // Overload based on DataColumn from DataTable type
        public static string SQLGetType(System.Data.DataColumn column)
        {
            return SQLGetType(column.DataType, column.MaxLength, 10, 2);
        }

        public static void SQLCreateVIEW(string queryName, string queryText)
        {
            SQLDeleteVIEW(queryName);
            string CREATEsql = "CREATE VIEW " + queryName + " AS  " + queryText;
            SqlCommand cmd = new SqlCommand(CREATEsql, CurrentSQLDB);
            cmd.CommandType = System.Data.CommandType.Text;
            try
            {
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //Handle this
            }
            return;
        }

        public static void SQLCreatePROCEDURE(string queryName, string queryText)
        {
            SQLDeletePROCEDURE(queryName);
            string CREATEsql = "CREATE PROCEDURE " + queryName + " AS  BEGIN " + queryText + " END";
            SqlCommand cmd = new SqlCommand(CREATEsql, CurrentSQLDB);
            cmd.CommandType = System.Data.CommandType.Text;
            try
            {
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //Handle this
            }
            return;
        }

        public void SQLDeleteVIEW(string queryName)
        {
            string DROPsql = "DROP VIEW " + queryName;
            SqlCommand cmd = new SqlCommand(DROPsql, CurrentSQLDB);
            try
            {
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //Could not drop table
            }
            return;
        }

        public void SQLDeletePROCEDURE(string queryName)
        {
            string DROPsql = "DROP PROCEDURE " + queryName;
            SqlCommand cmd = new SqlCommand(DROPsql, CurrentSQLDB);
            try
            {
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //Could not drop table
            }
            return;
        }

        public void SQLExecuteActionQuery(string queryName)
        {
            SqlCommand cmd = new SqlCommand();
            Int32 rowsAffected;

            cmd.CommandText = queryName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Connection = CurrentSQLDB;

            cmd.CommandTimeout = 0;
            try
            {
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //Error message here
            }
        }

        public void SQLExecuteActionQuery(string queryName, string parameterName, int parameter)
        {
            SqlCommand cmd = new SqlCommand();
            Int32 rowsAffected;

            cmd.CommandText = queryName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Connection = CurrentSQLDB;

            cmd.Parameters.Add(new SqlParameter(parameterName, OleDbType.Integer)).Value = parameter;

            /*string EXECUTEsql = queryName;
            SqlCommand cmd = new SqlCommand(EXECUTEsql, CurrentSQLDB);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;*/
            try
            {
                cmd.CommandTimeout = 0;
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //Could not drop table
            }
        }

        public void SQLExecuteActionQuery(string queryName, string parameterName, int parameter, string parameterName2, int parameter2)
        {
            SqlCommand cmd = new SqlCommand();
            Int32 rowsAffected;

            cmd.CommandText = queryName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Connection = CurrentSQLDB;

            cmd.Parameters.Add(new SqlParameter(parameterName, OleDbType.Integer)).Value = parameter;
            cmd.Parameters.Add(new SqlParameter(parameterName2, OleDbType.Integer)).Value = parameter2;

            /*string EXECUTEsql = queryName;
            SqlCommand cmd = new SqlCommand(EXECUTEsql, CurrentSQLDB);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;*/
            try
            {
                cmd.CommandTimeout = 0;
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException ae)
            {
                //Could not drop table
            }
        }

        #endregion
    }
}
