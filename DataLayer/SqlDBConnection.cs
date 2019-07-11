using System;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class SqlDBConnection: DBConnection
    {
        public SqlDBConnection(string ConnectionString) : base(ConnectionString)
        {

        }
        
        public override int ExecuteNonQuery(string SQL)
        {
            int rows = 0;
            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(SQL, sqlConnection))
                {
                    if (sqlConnection.State != ConnectionState.Open)
                        sqlConnection.Open();
                    rows = sqlCommand.ExecuteNonQuery();
                }
            }
            return rows;
        }

        public override DataTable ExecuteReader(string SQL)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(this.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(SQL, sqlConnection))
                {
                    if (sqlConnection.State != ConnectionState.Open)
                        sqlConnection.Open();
                    SqlDataReader dr = sqlCommand.ExecuteReader();
                    if (dr.HasRows)
                        dt.Load(dr);
                }
            }
            return dt;
        }

        public override int ExecuteScalar(string SQL)
        {
            throw new NotImplementedException();
        }
    }
}
