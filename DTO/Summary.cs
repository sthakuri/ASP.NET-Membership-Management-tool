using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Summary
    {
        public int UsersCount { get; set; }
        public int UsersWithMultipleRoleCount { get; set; }
        public int RolesCount { get; set; }
        public int ApplicationsCount { get; set; }

    }
}
