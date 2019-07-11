namespace AccessControlTool.App_Code
{
    public class AppConfig
    {
        public static string CurrentDBServer
        {
            get { return string.Format("{0}",System.Web.HttpContext.Current.Session["server"]); }
            set { System.Web.HttpContext.Current.Session["server"] = value; }
        }
    }
}