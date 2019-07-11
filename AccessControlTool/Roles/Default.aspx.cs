using BusinessLayer;
using System;
using System.Collections.Generic;

namespace AccessControlTool.Roles
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<DTO.Role> roles = LoadRoles();

                lblCount.Text = roles != null ? roles.Count.ToString() : "0";
            }
        }

        private List<DTO.Role> LoadRoles()
        {
            try
            {
                RoleService roleService = new RoleService(App_Code.AppConfig.CurrentDBServerConfigName);
                var roles = roleService.GetRoles();
                rptRoles.DataSource = roles;
                rptRoles.DataBind();
                return roles;
            }
            catch (Exception ex)
            {
                ViewState["Error"] = ex.Message;
            }
            return null;
        }
    }
}