namespace DataLayer
{
    public class DBFactory
    {
        public static DBConnection GetDBConnection(string ConnectionString)
        {
            DBConnection dBConnection = null;
            var providerName = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionString].ProviderName;
            switch (providerName)
            {
                case "Oracle.DataAccess.Client":
                    dBConnection = new OracleDBConnection(ConnectionString);
                    break;
                case "System.Data.SqlClient":
                    dBConnection = new SqlDBConnection(ConnectionString);
                    break;
                default:
                    break;
            }
            return dBConnection;
        }
    }
}
