<%@ Page Title="Home" Language="C#" MasterPageFile="~/SiteMain.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AccessControlTool.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-header">
        <h1 class="page-title">Home
        </h1>
    </div>
    <% if (ViewState["Error"] != null)
        { %>
    <div class="alert alert-danger">
        <%= ViewState["Error"] %>
    </div>
    <% } %>

    <div class="row">
        <div class="col-sm-6 col-lg-4">
            <div class="card">
                <div class="card-status bg-green"></div>
                <div class="card-body text-center">
                    <div class="card-category">Users</div>
                    <div class="display-3 my-4"><%= ViewState["UsersCount"].ToString() %></div>
                    <ul class="list-unstyled leading-loose">
                        <li><strong><%= ViewState["UsersCount"].ToString() %></strong> Users</li>
                        <li><strong><%= ViewState["UsersWithMultipleRoleCount"].ToString() %></strong> Users with multiple Role</li>
                        <li><i class="fe fe-check text-success mr-2" aria-hidden="true"></i>Add New User</li>
                        <li><i class="fe fe-check text-success mr-2" aria-hidden="true"></i>Assign New Role</li>
                        <li><i class="fe fe-check text-success mr-2" aria-hidden="true"></i>Revoke Role</li>

                    </ul>
                    <div class="text-center mt-6">
                        <a href="/Users/" class="btn btn-green btn-block">Go to Users Page</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-6 col-lg-4">
            <div class="card">
                <div class="card-status bg-green"></div>
                <div class="card-body text-center">
                    <div class="card-category">Roles</div>
                    <div class="display-3 my-4"><%= ViewState["RolesCount"].ToString() %></div>
                    <ul class="list-unstyled leading-loose">
                        <li><strong><%= ViewState["RolesCount"].ToString() %></strong> Roles</li>
                        <li><i class="fe fe-check text-success mr-2" aria-hidden="true"></i>Add New Role</li>
                        <li><i class="fe fe-check text-success mr-2" aria-hidden="true"></i>Edit Role</li>
                        <li><i class="fe fe-check text-success mr-2" aria-hidden="true"></i>Assign Role to User</li>
                        <li><i class="fe fe-check text-success mr-2" aria-hidden="true"></i>Remove User from Role</li>
                    </ul>
                    <div class="text-center mt-6">
                        <a href="/Roles/" class="btn btn-green btn-block">Go to Roles Page</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-6 col-lg-4">
            <div class="card">
                <div class="card-status bg-green"></div>
                <div class="card-body text-center">
                    <div class="card-category">Applications</div>
                    <div class="display-3 my-4"><%= ViewState["ApplicationsCount"].ToString() %></div>
                    <ul class="list-unstyled leading-loose">
                        <li><strong><%= ViewState["ApplicationsCount"].ToString() %></strong> Applications</li>
                        <li><i class="fe fe-check text-success mr-2" aria-hidden="true"></i>Add New Application</li>
                        <li><i class="fe fe-check text-success mr-2" aria-hidden="true"></i>Modify Application</li>
                        <li><i class="fe fe-check text-success mr-2" aria-hidden="true"></i>Remove User from Application</li>
                        <li><i class="fe fe-check text-success mr-2" aria-hidden="true"></i>Remove Application</li>
                    </ul>
                    <div class="text-center mt-6">
                        <a href="/Applications/" class="btn btn-green btn-block">Go to Applications Page</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
