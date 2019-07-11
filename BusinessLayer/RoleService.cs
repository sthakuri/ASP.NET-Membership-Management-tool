using DataLayer;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BusinessLayer
{
    public class RoleService
    {
        private DBConnection _db = null;
        public RoleService(string serverConnectionString)
        {
            _db = DBFactory.GetDBConnection(serverConnectionString);
        }
        private List<Role> ToRoles(DataTable dt)
        {
            List<Role> items = dt.AsEnumerable().Select(row =>
                new Role
                {
                    ApplicationID = new Guid(row.Field<string>("APPLICATIONID")),
                    ApplicationName = row.Field<string>("APPLICATIONNAME"),
                    RoleID = new Guid(row.Field<string>("ROLEID")),
                    RoleName = row.Field<string>("ROLENAME"),
                    Desc = row.Field<string>("DESCRIPTION"),
                    UsersCount = row.Field<decimal>("USERSCOUNT")
                }).ToList();

            return items.OrderBy(x => x.RoleName).ToList();
        }
        public int GetRolesCount()
        {
            string sql_ORA = "SELECT  COUNT(*) FROM ORA_ASPNET_ROLES R ";

            return _db.ExecuteScalar(sql_ORA);
        }

        /// <summary>
        /// Get Roles along with number of users assigned to each roles
        /// </summary>
        /// <returns></returns>

        public List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();
            string sql_ORA = "SELECT RAWTOHEX(A.APPLICATIONID) AS APPLICATIONID, A.APPLICATIONNAME, RAWTOHEX(R.ROLEID) AS ROLEID, R.ROLENAME,R.DESCRIPTION, " +
                "(SELECT COUNT(*) FROM ORA_ASPNET_USERS U INNER JOIN ORA_ASPNET_MEMBERSHIP M ON U.USERID = M.USERID " +
                "INNER JOIN ORA_ASPNET_USERSINROLES UR ON U.USERID = UR.USERID  " +
                "WHERE UR.ROLEID = R.ROLEID) AS USERSCOUNT " +
                 "FROM ORA_ASPNET_ROLES R " +
                 "INNER JOIN ORA_ASPNET_APPLICATIONS A ON R.APPLICATIONID = A.APPLICATIONID";

            DataTable dt = _db.ExecuteReader(sql_ORA);
            return ToRoles(dt);
        }

        /// <summary>
        /// Get Roles along with number of users assigned to each roles in particular application
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoles(Guid ApplicationID)
        {
            List<Role> roles = new List<Role>();
            string sql_ORA = "SELECT RAWTOHEX(A.APPLICATIONID) AS APPLICATIONID, A.APPLICATIONNAME, RAWTOHEX(R.ROLEID) AS ROLEID, R.ROLENAME,R.DESCRIPTION, " +
                "(SELECT COUNT(*) FROM ORA_ASPNET_USERS U INNER JOIN ORA_ASPNET_MEMBERSHIP M ON U.USERID = M.USERID " +
                "INNER JOIN ORA_ASPNET_USERSINROLES UR ON U.USERID = UR.USERID  " +
                "WHERE UR.ROLEID = R.ROLEID) AS USERSCOUNT " +
                 "FROM ORA_ASPNET_ROLES R " +
                 "INNER JOIN ORA_ASPNET_APPLICATIONS A ON R.APPLICATIONID = A.APPLICATIONID " +
                 "WHERE A.APPLICATIONID = '" + ApplicationID.ToString().Replace("-", "").ToUpper() + "' ";

            DataTable dt = _db.ExecuteReader(sql_ORA);
            return ToRoles(dt);
        }

        /// <summary>
        /// Get Roles assigned to user
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRolesByUser(Guid UserID)
        {
            List<Role> roles = new List<Role>();
            string sql_ORA = "SELECT RAWTOHEX(A.APPLICATIONID) AS APPLICATIONID, A.APPLICATIONNAME, RAWTOHEX(R.ROLEID) AS ROLEID, R.ROLENAME,R.DESCRIPTION, " +
                "(SELECT COUNT(*) FROM ORA_ASPNET_USERS U INNER JOIN ORA_ASPNET_MEMBERSHIP M ON U.USERID = M.USERID " +
                "INNER JOIN ORA_ASPNET_USERSINROLES UR ON U.USERID = UR.USERID  " +
                "WHERE UR.ROLEID = R.ROLEID) AS USERSCOUNT " +
                 "FROM ORA_ASPNET_ROLES R " +
                 "INNER JOIN ORA_ASPNET_APPLICATIONS A ON R.APPLICATIONID = A.APPLICATIONID " +
                 "INNER JOIN ORA_ASPNET_USERSINROLES UR ON R.ROLEID = UR.ROLEID  " +
                 "WHERE UR.USERID = '" + UserID.ToString().Replace("-", "").ToUpper() + "' ";

            DataTable dt = _db.ExecuteReader(sql_ORA);
            return ToRoles(dt);
        }
    }
}
