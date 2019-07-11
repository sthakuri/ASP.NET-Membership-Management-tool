using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class User
    {
        public Guid ApplicationID { get; set; }
        public string ApplicationName { get; set; }
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal PasswordFormat { get; set; }
        public string PasswordSalt { get; set; }
        public decimal? MobilePin { get; set; }
        public string Email { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsLockedOut { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
