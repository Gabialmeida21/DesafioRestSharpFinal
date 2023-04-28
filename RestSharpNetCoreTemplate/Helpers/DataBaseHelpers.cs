using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RestSharpNetCoreTemplate.Helpers
{
    public class DataBaseHelpers
    {
        private static SqlConnection GetDBConnection()
        {
            string connectionString = "Data Source=" + JsonHelpers.GetParameterAppSettings("DB_URL") + ","
                + JsonHelpers.GetParameterAppSettings("DB_PORT") + ";" +
                                      "Initial Catalog=" + JsonHelpers.GetParameterAppSettings("DB_NAME") + ";" +
                                      "User ID=" + JsonHelpers.GetParameterAppSettings("DB_USER") + "; " +
                                      "Password=" + JsonHelpers.GetParameterAppSettings("DB_PASSWORD") + ";";

            SqlConnection connection = new SqlConnection(connectionString);

            return connection;
        }

        public static void ExecuteQuery(string query)
        {
            using (SqlCommand cmd = new SqlCommand(query, GetDBConnection()))
            {
                cmd.CommandTimeout = Int32.Parse(JsonHelpers.GetParameterAppSettings("DB_CONNECTION_TIMEOUT"));
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public static List<string> GetQueryResult(string query)
        {
            DataSet ds = new DataSet();
            List<string> lista = new List<string>();

            using (SqlCommand cmd = new SqlCommand(query, GetDBConnection()))
            {
                cmd.CommandTimeout = Int32.Parse(JsonHelpers.GetParameterAppSettings("DB_CONNECTION_TIMEOUT"));
                cmd.Connection.Open();

                DataTable table = new DataTable();
                table.Load(cmd.ExecuteReader());
                ds.Tables.Add(table);

                cmd.Connection.Close();
            }

            if (ds.Tables[0].Columns.Count == 0)
            {
                return null;
            }

            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        lista.Add(ds.Tables[0].Rows[i][j].ToString());
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            return lista;
        }

    }
}
