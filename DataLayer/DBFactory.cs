namespace DataLayer
{
    public class DBFactory
    {
        public static DBConnection GetDBConnection(string ConnectionString)
        {
            return new OracleDBConnection(ConnectionString);
        }
    }
}
