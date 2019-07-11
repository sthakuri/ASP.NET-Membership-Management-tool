using BusinessLayer;
using DTO;
using System;
using System.Collections.Generic;

namespace AccessControlTool.Users
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<User> users = GetUsers();
                rptUsers.DataSource = users;
                rptUsers.DataBind();
                lblCount.Text = users != null ? users.Count.ToString() : "0";
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                UserService userService = new UserService(App_Code.AppConfig.CurrentDBServer);
                return userService.GetUsers();
            }
            catch (Exception ex)
            {

                ViewState["Error"] = ex.Message;
            }
            return null;
        }
    }
}