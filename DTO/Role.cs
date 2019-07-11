using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Role
    {
        public Guid ApplicationID { get; set; }
        public string ApplicationName { get; set; }

        public Guid RoleID { get; set; }
        public string RoleName { get; set; }
        public string Desc { get; set; }

        public decimal UsersCount { get; set; }


    }
}
