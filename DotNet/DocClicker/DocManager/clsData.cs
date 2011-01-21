using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;



namespace DocManager
{
    public class clsData
    {
        /// <summary>
        /// CreateDataTable - Create a data table to display on the Results GRID.
        /// </summary>
        public DataTable CreateDataTable()
        {
            DataTable myDataTable = new DataTable();

            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "URI";
            
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Name";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.DateTime");
            myDataColumn.ColumnName = "Date";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Geoinfo";
            myDataTable.Columns.Add(myDataColumn);

            return myDataTable;
        }

        /// <summary>
        /// AddDataToTable - Add a row in data table to display on the Results GRID.
        /// </summary>
        public void AddDataToTable(string URI, string Filename, DateTime  Date, DataTable TrimTable, string geoinfo)
        {
            
            
            DataRow row;
            try
            {
                row = TrimTable.NewRow();

                // row["id"] = Guid.NewGuid().ToString();
                row["URI"] = URI;
                row["Name"] = Filename;
                row["Date"] = Date;
                row["Geoinfo"] = geoinfo;
                TrimTable.Rows.Add(row);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 
    }
}