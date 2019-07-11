using System.Data;

namespace DataLayer
{
    public abstract class DBConnection
    {
        public string ConnectionName { get; set; }
        public string ConnectionString { get; }
        public DBConnection(string ConnectionName)
        {
            this.ConnectionName = ConnectionName;
            this.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
        }
        public abstract int ExecuteNonQuery(string SQL);
        public abstract DataTable ExecuteReader(string SQL);
        public abstract int ExecuteScalar(string SQL);


    }
}
