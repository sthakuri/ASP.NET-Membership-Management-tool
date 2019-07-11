using DataLayer;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BusinessLayer
{
    public class ApplicationService
    {
        private DBConnection _db = null;
        public ApplicationService(string serverConnectionString)
        {
            _db = DBFactory.GetDBConnection(serverConnectionString);
        }
        public Summary GetSummary()
        {
            UserService userService = new UserService(_db.ConnectionName);
            RoleService roleService = new RoleService(_db.ConnectionName);

            Summary summary = new Summary
            {
                UsersCount = userService.GetUsersCount(),
                UsersWithMultipleRoleCount = userService.GetUsersWithMultipleRoleCount(),
                ApplicationsCount = this.GetApplicationsCount(),
                RolesCount = roleService.GetRolesCount()
            };
            return summary;
        }

        public List<Application> ToApplications(DataTable dt)
        {
            List<Application> items = dt.AsEnumerable().Select(row =>
                new Application
                {
                    ApplicationID = new Guid(row.Field<string>("APPLICATIONID")),
                    ApplicationName = row.Field<string>("APPLICATIONNAME"),
                    Desc = row.Field<string>("DESCRIPTION"),
                    RolesCount = row.Field<decimal>("ROLESCOUNT"),
                    UsersCount = row.Field<decimal>("USERSCOUNT")
                }).ToList();

            return items.OrderBy(x => x.ApplicationName).ToList();
        }
        public int GetApplicationsCount()
        {
            string sql_ORA = "SELECT COUNT(*) FROM ORA_ASPNET_APPLICATIONS A ";

            return _db.ExecuteScalar(sql_ORA);
        }

        public List<Application> GetApplications()
        {
            string sql_ORA = "SELECT RAWTOHEX(A.APPLICATIONID) AS APPLICATIONID, A.APPLICATIONNAME, A.DESCRIPTION, " +
                            "(SELECT COUNT(*) FROM " +
                                "ORA_ASPNET_ROLES R " +
                                "WHERE A.APPLICATIONID = R.APPLICATIONID) AS ROLESCOUNT, " +
                            "(SELECT COUNT(*) FROM " +
                                "ORA_ASPNET_USERS U " +
                                "INNER JOIN ORA_ASPNET_MEMBERSHIP M ON U.USERID = M.USERID " +
                                "WHERE M.APPLICATIONID = A.APPLICATIONID) AS USERSCOUNT " +
                            "FROM ORA_ASPNET_APPLICATIONS A ";

            DataTable dt = _db.ExecuteReader(sql_ORA);

            return ToApplications(dt);
        }
    }
}
