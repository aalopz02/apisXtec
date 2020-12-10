using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ApiSQL.Models
{
    public class SqlProvider
    {
        private static string urlExcel = "D://OneDrive//Escritorio//repo//cvsTemp//";
        private static string tempTableName = "tablaTemp";
        private static string conectionStr = "Driver={SQL Server};Server=ELPONQUI;UID=adminApi;PWD='adminApi';Database=BikeStores;Trusted_Connection=yes";
       
        public static OdbcConnection GetConnection() {
            OdbcConnection connect;
            connect = new OdbcConnection(conectionStr);
            try
            {
                connect.Open();
                connect.Close();
            }
            catch (Exception ex)
            {
            }
            return connect;
        }

        private static void createTempTable(DataTable dt)
        {
            string sqlQuery = "CREATE TABLE " + tempTableName + "(";
            string sqlDBType = "";
            string dataType = "";
            int maxLength = 0;
            
            sqlQuery = sqlQuery.Trim().TrimEnd(',');
            sqlQuery += " )";
            OdbcConnection database = GetConnection();
            database.Open();
            OdbcCommand command = new OdbcCommand(sqlQuery, database);
            command.ExecuteNonQuery();
        }

        private static void loadCsvToTable(string delimeter)
        {
            string sqlQuery = "BULK INSERT " + tempTableName;
            sqlQuery += " FROM '" + urlExcel + tempTableName + ".csv'";
            sqlQuery += " WITH ( FIELDTERMINATOR = '" + delimeter + "', ROWTERMINATOR = '\n' )";
            OdbcConnection database = GetConnection();
            database.Open();
            OdbcCommand command = new OdbcCommand(sqlQuery, database);
            command.ExecuteNonQuery();
        }

        public static void processFile()
        {
            string queryString = "SELECT * FROM [" + tempTableName + ".csv" + "]";
            string strCSVConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + urlExcel + ";" + "Extended Properties='text;HDR=YES;'";
            DataTable dtCSV = new DataTable();
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(queryString, strCSVConnString))
            {
                adapter.FillSchema(dtCSV, SchemaType.Mapped);
                adapter.Fill(dtCSV);
            }

            if (dtCSV.Rows.Count > 0)
            {
                //createTempTable(dtCSV);
                loadCsvToTable(",");
            }

        }

    }
}