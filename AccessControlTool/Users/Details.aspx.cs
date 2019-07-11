using BusinessLayer;
using System;
using System.Web.Services;

namespace AccessControlTool.Users
{
    public partial class Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userId = Request.QueryString["UserId"];
                if (!string.IsNullOrEmpty(userId))
                {
                    ApplicationService applicationService = new ApplicationService(App_Code.AppConfig.CurrentDBServerConfigName);
                    var apps = applicationService.GetApplications();
                    ddlApplications.DataSource = apps;
                    ddlApplications.DataValueField = "ApplicationID";
                    ddlApplications.DataTextField = "ApplicationName";
                    ddlApplications.DataBind();

                    UserService userService = new UserService(App_Code.AppConfig.CurrentDBServerConfigName);
                    Guid _userId = new Guid();
                    if (Guid.TryParse(userId, out _userId))
                    {
                        var user = userService.GetUser(_userId);
                        if (user != null)
                        {
                            valueUserId.Value = user.UserID.ToString();
                            txtUserName.Text = user.UserName;
                            chkIsLockedOut.Checked = user.IsLockedOut.Value;
                            txtDateCreated.Text = user.CreatedDate.ToString("MM/dd/yyyy");
                            ddlApplications.SelectedValue = user.ApplicationID.ToString();

                            RoleService roleService = new RoleService(App_Code.AppConfig.CurrentDBServerConfigName);

                            var userRoles = roleService.GetRolesByUser(user.UserID);
                            foreach (var item in userRoles)
                            {
                                ViewState[item.RoleID.ToString()] = "checked";
                            }

                            BindRoles(user.ApplicationID);
                        }
                    }
                }
            }
        }
        private void BindRoles(Guid ApplicationID)
        {
            RoleService roleService = new RoleService(App_Code.AppConfig.CurrentDBServerConfigName);
            var roles = roleService.GetRoles(ApplicationID);
            rptRoles.DataSource = roles;
            rptRoles.DataBind();
        }

        [WebMethod]
        public static bool AssignRole(string userId, string roleId)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(roleId))
            {
                UserService userService = new UserService(App_Code.AppConfig.CurrentDBServerConfigName);

                return userService.AssignRole(Guid.Parse(userId), Guid.Parse(roleId));
            }

            return false;
        }

        [WebMethod]
        public static bool RevokeRole(string userId, string roleId)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(roleId))
            {
                UserService userService = new UserService(App_Code.AppConfig.CurrentDBServerConfigName);

                return userService.RevokeRole(Guid.Parse(userId), Guid.Parse(roleId));
            }

            return false;
        }
    }
}