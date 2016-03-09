using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Cube_Utility
{
    public class SQLQuery
    {
        public static string ExecuteSQL_SingleString(string ConnectionString, string Sqlquery)
        {
            IDataReader reader = ExecuteSQL(ConnectionString, Sqlquery);
            reader.Read();
            return reader.GetValue(0).ToString();
            
        }
        public static IDataReader ExecuteSQL(string ConnectionString, string Sqlquery)
        {
            using (OleDbConnection con = new OleDbConnection(ConnectionString))
            {
                con.Open();
                using(OleDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = Sqlquery;
                    IDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    return dt.CreateDataReader();
                }
            }
        }


    }
}
