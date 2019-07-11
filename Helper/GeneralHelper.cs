using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Helper
{
    public class GeneralHelper
    {
        public static string GetInitialsFromFullName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return "--";

            Regex initials = new Regex(@"(\b[a-zA-Z])[a-zA-Z]* ?");
            string init = initials.Replace(fullName, "$1");

            return init.ToUpper();
        }
    }
}
