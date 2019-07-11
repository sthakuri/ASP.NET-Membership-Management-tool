using DataLayer;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BusinessLayer
{
    public class UserService
    {   
        private DBConnection _db = null;
        public UserService(string serverConnectionString)
        {
            _db = DBFactory.GetDBConnection(serverConnectionString);
        }
        private List<User> ToUsers(DataTable dt)
        {
            List<User> items = dt.AsEnumerable().Select(row =>
                new User
                {
                    ApplicationID = new Guid(row.Field<string>("APPLICATIONID")),
                    ApplicationName = row.Field<string>("APPLICATIONNAME"),
                    UserID = new Guid(row.Field<string>("USERID")),
                    UserName = row.Field<string>("USERNAME"),
                    Password = row.Field<string>("PASSWORD"),
                    PasswordFormat = row.Field<decimal>("PASSWORDFORMAT"),
                    PasswordSalt = row.Field<string>("PASSWORDSALT"),
                    MobilePin = row.Field<decimal?>("MOBILEPIN"),
                    PasswordQuestion = row.Field<string>("PASSWORDQUESTION"),
                    PasswordAnswer = row.Field<string>("PASSWORDANSWER"),
                    IsApproved = row.Field<decimal?>("ISAPPROVED") == 1,
                    IsLockedOut = row.Field<decimal?>("ISLOCKEDOUT") == 1,
                    CreatedDate = row.Field<DateTime>("CREATEDATE")
                }).ToList();

            return items.OrderBy(x => x.UserName).ToList();
        }

        public int GetUsersCount()
        {
            string sql_ORA = "SELECT COUNT(*) FROM ORA_ASPNET_USERS U INNER JOIN ORA_ASPNET_MEMBERSHIP M ON U.USERID = M.USERID";

            return _db.ExecuteScalar(sql_ORA);
        }

        public int GetUsersWithMultipleRoleCount()
        {
            string sql_ORA = "SELECT COUNT(*) FROM (SELECT USERID, COUNT(ROLEID) FROM ORA_ASPNET_USERSINROLES GROUP BY USERID HAVING COUNT(ROLEID)>1)";

            return _db.ExecuteScalar(sql_ORA);
        }
        public List<User> GetUsers()
        {
            string sql_ORA = "SELECT " +
                            "RAWTOHEX(U.APPLICATIONID) AS APPLICATIONID, " +
                            "A.APPLICATIONNAME, " +
                            "RAWTOHEX(U.USERID) AS USERID, " +
                            "U.USERNAME, " +
                            "M.PASSWORD, " +
                            "M.PASSWORDFORMAT, " +
                            "M.PASSWORDSALT, " +
                            "M.MOBILEPIN, " +
                            "M.PASSWORDQUESTION, " +
                            "M.PASSWORDANSWER, " +
                            "M.ISAPPROVED, " +
                            "M.ISLOCKEDOUT, " +
                            "M.CREATEDATE " +
                        "FROM ORA_ASPNET_USERS U " +
                        "INNER JOIN ORA_ASPNET_MEMBERSHIP M " +
                        "ON U.USERID = M.USERID " +
                        "INNER JOIN ORA_ASPNET_APPLICATIONS A " +
                        "ON A.APPLICATIONID = M.APPLICATIONID " +
                        "ORDER BY U.USERNAME DESC ";

            DataTable dt = _db.ExecuteReader(sql_ORA);
            return ToUsers(dt);
        }
        public List<User> GetUsers(Guid ApplicationID)
        {
            string sql_ORA = "SELECT " +
                            "RAWTOHEX(U.APPLICATIONID) AS APPLICATIONID, " +
                            "A.APPLICATIONNAME, " +
                            "RAWTOHEX(U.USERID) AS USERID, " +
                            "U.USERNAME, " +
                            "M.PASSWORD, " +
                            "M.PASSWORDFORMAT, " +
                            "M.PASSWORDSALT, " +
                            "M.MOBILEPIN, " +
                            "M.PASSWORDQUESTION, " +
                            "M.PASSWORDANSWER, " +
                            "M.ISAPPROVED, " +
                            "M.ISLOCKEDOUT, " +
                            "M.CREATEDATE " +
                        "FROM ORA_ASPNET_USERS U " +
                        "INNER JOIN ORA_ASPNET_MEMBERSHIP M " +
                        "ON U.USERID = M.USERID " +
                        "INNER JOIN ORA_ASPNET_APPLICATIONS A " +
                        "ON A.APPLICATIONID = M.APPLICATIONID " +
                        "WHERE A.APPLICATIONID = \"" + ApplicationID.ToString().Replace("-", "").ToUpper() + "\"" +
                        "ORDER BY U.USERNAME DESC ";

            DataTable dt = _db.ExecuteReader(sql_ORA);
            return ToUsers(dt);
        }

        public User GetUser(Guid UserID)
        {
            string sql_ORA = "SELECT " +
                            "RAWTOHEX(U.APPLICATIONID) AS APPLICATIONID, " +
                            "A.APPLICATIONNAME, " +
                            "RAWTOHEX(U.USERID) AS USERID, " +
                            "U.USERNAME, " +
                            "M.PASSWORD, " +
                            "M.PASSWORDFORMAT, " +
                            "M.PASSWORDSALT, " +
                            "M.MOBILEPIN, " +
                            "M.PASSWORDQUESTION, " +
                            "M.PASSWORDANSWER, " +
                            "M.ISAPPROVED, " +
                            "M.ISLOCKEDOUT, " +
                            "M.CREATEDATE " +
                        "FROM ORA_ASPNET_USERS U " +
                        "INNER JOIN ORA_ASPNET_MEMBERSHIP M " +
                        "ON U.USERID = M.USERID " +
                        "INNER JOIN ORA_ASPNET_APPLICATIONS A " +
                        "ON A.APPLICATIONID = M.APPLICATIONID " +
                        "WHERE M.USERID = \'" + UserID.ToString().Replace("-", "").ToUpper() + "\'";

            DataTable dt = _db.ExecuteReader(sql_ORA);
            return ToUsers(dt).FirstOrDefault();
        }

        public bool AssignRole(Guid UserID, Guid RoleID)
        {
            string sql_ORA = $"INSERT INTO ORA_ASPNET_USERSINROLES (USERID, ROLEID) VALUES('{UserID.ToString().Replace("-", "").ToUpper()}', '{RoleID.ToString().Replace("-", "").ToUpper()}')";
            return _db.ExecuteNonQuery(sql_ORA) > 0;
        }

        public bool RevokeRole(Guid UserID, Guid RoleID)
        {
            string sql_ORA = $"DELETE ORA_ASPNET_USERSINROLES WHERE ROLEID='{RoleID.ToString().Replace("-", "").ToUpper()}' AND USERID='{UserID.ToString().Replace("-", "").ToUpper()}'";
            return _db.ExecuteNonQuery(sql_ORA) > 0;
        }
    }
}
