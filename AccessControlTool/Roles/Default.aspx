<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMain.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AccessControlTool.Roles.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 class="page-title">Roles
        </h1>
        <div class="page-subtitle">
            Count:
            <asp:Label ID="lblCount" runat="server"></asp:Label>
        </div>
        <div class="page-options">
             <a href="/Roles/NewRole.aspx" class="btn btn-sm btn-outline-primary" target="_blank"><i class="fe fe-check-square"></i>New Role</a>
        </div>
    </div>
    <% if (ViewState["Error"] != null)
        { %>
    <div class="alert alert-danger">
        <%= ViewState["Error"] %>
    </div>
    <% } %>
    <div class="row">

        <asp:Repeater ID="rptRoles" runat="server">
            <ItemTemplate>
                <div class="col-md-6 col-xl-4">
                    <div class="card">
                        <div class="card-status <%# Eval("UsersCount").ToString() == "0"?"bg-yellow":"bg-green" %>"></div>
                        <div class="card-header">
                            <h3 class="card-title"><a href="/Roles/Details.aspx?RoleId=<%# Eval("RoleID") %>"><%# Eval("RoleName") %></a></h3>
                            <div class="card-options">
                                Users: <%# Eval("UsersCount") %>
                            </div>
                        </div>
                        <div class="card-body">

                            <div style="display: <%# Eval("Desc") == ""?"none":"block" %>;">
                                No description available for display.
                            </div>
                            <%#  Eval("Desc") %>
                        </div>
                        <div class="card-footer">
                            <a href="/Applications/"><%# Eval("ApplicationName") %></a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScript" runat="server">
</asp:Content>
