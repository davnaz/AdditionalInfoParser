using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using AdditionalInfoParser.Components;
using System.Data.Common;

namespace AdditionalInfoParser.DataProviders
{
    public class DataProvider : SingleTone<DataProvider>
    {

        /// <summary>
        /// returns default Connection
        /// </summary>
        public SqlConnection Connection
        {
            
            get
            {

                SqlConnection sqlConnection = null;
                if (sqlConnection == null)
                {
                    sqlConnection = new SqlConnection(Resources.DbConnectionString);
                }
                if(string.IsNullOrEmpty(sqlConnection.ConnectionString))
                {
                    sqlConnection.ConnectionString = Resources.DbConnectionString;
                }
                return sqlConnection;
            }
        }

        public void ExecureSP(SqlCommand sqlCommand)
        {
            bool needCloseConnection = true;
            int numberOfRowsAffected = 0;
            if(sqlCommand.CommandType != CommandType.StoredProcedure)
            {
                throw new Exception("Not StoredProcedure");
            }
            try
            {
                //If connection is already opened it means that it is a transaction and we must not close 
                //connection after this command execution, because next command in this transaction uses 
                //the same connection.
                if(sqlCommand.Connection.State != ConnectionState.Open)
                {
                    sqlCommand.Connection.Open();
                }
                else
                {
                    needCloseConnection = false;
                }

                numberOfRowsAffected = sqlCommand.ExecuteNonQuery();//return the number of rows affected
                //TODO: check numberOfRowsAffected?
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if(needCloseConnection)
                {
                    sqlCommand.Connection.Close();
                }
            }           
        }








        #region Public Methods

        /// <summary>
        /// Create SQL command for stored procedure
        /// </summary>    
        /// <param name="spName">name of the stored procedure</param>
        /// <returns>SQL command</returns>
        /// <remarks></remarks>
        public SqlCommand CreateSQLCommandForInsertSP()
        {
            SqlCommand command = new SqlCommand(Resources.InsertProcedure, new SqlConnection(Resources.DbConnectionString));
            command.CommandType = CommandType.StoredProcedure;
            // command.Connection.Open();
            return command;
        }

        /// <summary>
        /// Create SQL command for string query
        /// </summary>    
        /// <param name="spName">name of the stored procedure</param>
        /// <returns>SQL command</returns>
        /// <remarks></remarks>
        public SqlCommand CreateSQLCommand(string query)
        {
            SqlCommand command = new SqlCommand(query, new SqlConnection(Resources.DbConnectionString));
            command.CommandType = CommandType.Text;
            
            return command;
        }


        /// <summary>
        /// Create input SQL parametet, its name is @ and column name
        /// </summary>
        /// <param name="columnName">Column name which matches with parameter</param>
        /// <param name="dbType">Parameter type</param>
        /// <param name="value">Parameter value</param>
        /// <returns>Filled SQL parameter</returns>
        /// <remarks></remarks>
        public SqlParameter CreateSqlParameter(string columnName, SqlDbType dbType, object value)
        {
            return CreateSqlParameter(columnName, dbType, value, ParameterDirection.Input);
        }

        /// <summary>
        /// Create SQL parametet, its name is @ and column name
        /// </summary>
        /// <param name="columnName">Column name which matches with parameter</param>
        /// <param name="dbType">Parameter type</param>
        /// <param name="value">Parameter value</param>
        /// <param name="direction">Parameter direction</param>
        /// <returns>Filled SQL parameter</returns>
        /// <remarks></remarks>
        public SqlParameter CreateSqlParameter(string columnName, SqlDbType dbType, object value, ParameterDirection direction)
        {
            // Add parametors
            SqlParameter param = new SqlParameter(string.Format("@{0}", columnName), dbType);

            param.Direction = direction;
            param.Value = value;

            return param;
        }

        /// <summary>
        /// Returns Dataset from database assosiated with query in method
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataSet GetDataset(long begin, long end)
        {
            SqlConnection conn = new SqlConnection(Resources.DbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"EXEC [dbo].[GET_RANGE] @BEGIN = {begin}, @END = {end}";
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();

            conn.Open();
            da.Fill(ds);
            conn.Close();

            return ds;
        }

        /// <summary>
        /// Makes parameterName satisfying t-sql syntax (parameterName - > @parameterName)
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public string SqlParameterName(string parameterName)
        {
            return string.Format("@{0}", parameterName);
        }

        internal long GetCount()
        {
            SqlConnection conn = new SqlConnection(Resources.DbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT MAX(ID) FROM Offerstable";
            conn.Open();
            
            SqlDataReader rd = cmd.ExecuteReader();
            long colsCount = -1;
            if (rd.HasRows)
            {
                rd.Read(); // read first row                
                colsCount =  rd.GetInt64(0);
            }
            conn.Close();
            return colsCount;
        }





        #endregion


    }
}