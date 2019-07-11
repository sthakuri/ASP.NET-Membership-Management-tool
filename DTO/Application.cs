using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Application
    {
        public Guid ApplicationID { get; set; }
        public string ApplicationName { get; set; }
        public string Desc { get; set; }

        public decimal RolesCount { get; set; }
        public decimal UsersCount { get; set; }

    }
}
