using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DataLayer
{
    public class OracleDBConnection : DBConnection
    {
        public OracleDBConnection(string ConnectionString) : base(ConnectionString)
        {

        }
        public override int ExecuteNonQuery(string SQL)
        {
            int rows = 0;
            using (OracleConnection oracleConnection = new OracleConnection(this.ConnectionString))
            {
                using (OracleCommand oracleCommand = new OracleCommand(SQL, oracleConnection))
                {
                    if (oracleConnection.State != ConnectionState.Open)
                        oracleConnection.Open();

                    rows = oracleCommand.ExecuteNonQuery();
                }
            }
            return rows;
        }

        public override DataTable ExecuteReader(string SQL)
        {
            DataTable dt = new DataTable();
            using (OracleConnection oracleConnection = new OracleConnection(this.ConnectionString))
            {
                using (OracleCommand oracleCommand = new OracleCommand(SQL, oracleConnection))
                {
                    if (oracleConnection.State != ConnectionState.Open)
                        oracleConnection.Open();

                    OracleDataReader dr = oracleCommand.ExecuteReader();
                    if (dr.HasRows)
                        dt.Load(dr);
                }
            }
            return dt;
        }

        public override int ExecuteScalar(string SQL)
        {
            int value = 0;
            using (OracleConnection oracleConnection = new OracleConnection(this.ConnectionString))
            {
                using (OracleCommand oracleCommand = new OracleCommand(SQL, oracleConnection))
                {
                    if (oracleConnection.State != ConnectionState.Open)
                        oracleConnection.Open();

                    var obj = oracleCommand.ExecuteScalar();
                    if (obj != null)
                        value = int.Parse(obj.ToString());
                }
            }
            return value;
        }

    }
}
