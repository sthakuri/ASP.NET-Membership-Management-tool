using Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccessControlTool
{
    public partial class SiteMain : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;                
                lblUserFullName.Text = userName;
                //lblInitials.Text = GeneralHelper.GetInitialsFromFullName(lblUserFullName.Text);
                Navigation();
                LoadServersInfo();
            }
        }

        private void Navigation()
        {
            var currentURL = Request.RawUrl;
            if (currentURL == "/")
                navHome.Attributes["class"] = "nav-link active";
            if (currentURL.Contains("Users"))
                navUsers.Attributes["class"] = "nav-link active";
            if (currentURL.Contains("Roles"))
                navRoles.Attributes["class"] = "nav-link active";
            if (currentURL.Contains("Applications"))
                navApplications.Attributes["class"] = "nav-link active";
        }

        private void LoadServersInfo()
        {
            Dictionary<string, string> _ConnectionStringDictionary = ConfigurationManager.ConnectionStrings
                .Cast<ConnectionStringSettings>()
                .ToDictionary(v => v.Name, v => v.ConnectionString);

            ddlServers.DataSource = _ConnectionStringDictionary.ToList();
            ddlServers.DataValueField = "Key";
            ddlServers.DataTextField = "Key";
            ddlServers.DataBind();

            if (!string.IsNullOrEmpty(App_Code.AppConfig.CurrentDBServerConfigName))
                ddlServers.SelectedValue = App_Code.AppConfig.CurrentDBServerConfigName;
            else
                App_Code.AppConfig.CurrentDBServerConfigName = _ConnectionStringDictionary.FirstOrDefault().Key;

        }

        protected void ddlServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            App_Code.AppConfig.CurrentDBServerConfigName = ddlServers.SelectedItem.Text;
            Response.Redirect(Request.RawUrl, true);
        }
    }
}