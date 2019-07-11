namespace AccessControlTool.App_Code
{
    public class AppConfig
    {
        public static string CurrentDBServerConfigName
        {
            get { return string.Format("{0}",System.Web.HttpContext.Current.Session["connectionStringName"]); }
            set { System.Web.HttpContext.Current.Session["connectionStringName"] = value; }
        }
    }
}