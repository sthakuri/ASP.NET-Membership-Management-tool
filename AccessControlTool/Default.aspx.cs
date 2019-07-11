using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccessControlTool
{
    public partial class Default : System.Web.UI.Page
    {
        private string _ServerConString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _ServerConString = App_Code.AppConfig.CurrentDBServer;
                LoadSummary();
            }
        }

        private void LoadSummary()
        {
            //var roles= System.Web.Security.Roles.Providers["Agdf"].GetAllRoles();
            try
            {
                ApplicationService applicationService = new ApplicationService(_ServerConString);
                var summary = applicationService.GetSummary();
                if (summary != null)
                {
                    ViewState["UsersCount"] = summary.UsersCount;
                    ViewState["RolesCount"] = summary.RolesCount;
                    ViewState["ApplicationsCount"] = summary.ApplicationsCount;
                    ViewState["UsersWithMultipleRoleCount"] = summary.UsersWithMultipleRoleCount;
                }
            }
            catch (Exception ex)
            {
                ViewState["Error"] = ex.Message;
                ViewState["UsersCount"] = 0;
                ViewState["RolesCount"] = 0;
                ViewState["ApplicationsCount"] = 0;
                ViewState["UsersWithMultipleRoleCount"] = 0;
            }

            
        }
    }
}