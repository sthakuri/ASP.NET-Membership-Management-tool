<%@ Page Title="User Details" Language="C#" MasterPageFile="~/SiteMain.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="AccessControlTool.Users.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 class="page-title">User Details
        </h1>

    </div>
    <div class="row">
        <div class="col-md-6 col-sm-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">Edit User</h3>
                    <div class="card-options">
                        <asp:HiddenField ID="valueUserId" runat="server" />
                        <label class="custom-switch">
                            <input runat="server" id="chkIsLockedOut" type="checkbox" name="custom-switch-input" class="custom-switch-input" disabled >
                            <span class="custom-switch-indicator" style="cursor:no-drop"></span>
                            <span class="custom-switch-description">Is Locked Out?</span>
                        </label>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="form-label">User Name</label>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" Enabled="false" Text="Creative Code Inc."></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <label class="form-label">Password</label>
                                <input type="email" class="form-control" disabled placeholder="***">
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <label class="form-label">Email address</label>
                                <input type="email" class="form-control" placeholder="Email" disabled>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <label class="form-label">Created Date</label>
                                <asp:TextBox ID="txtDateCreated" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-footer text-right">
                    <%--<button type="submit" class="btn btn-primary">Update</button>--%>
                </div>
            </div>
        </div>
        <div class="col-md-6 col-sm-12">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">Assign Roles</h3>
                </div>
                <div class="card-body">
                    <div class="row">

                        <div class="col-sm-12 col-md-12">
                            <div class="form-group">
                                <label class="form-label">Application</label>
                                <asp:DropDownList ID="ddlApplications" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="form-group">
                                <label class="form-label">Roles</label>
                                <asp:Repeater ID="rptRoles" runat="server">
                                    <HeaderTemplate>
                                        <ul class="list-group">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li class="list-group-item">
                                            <div class="row">
                                                <div class="col-md-9 col-sm-6"><%# Eval("RoleName") %></div>
                                                <div class="col-md-3 col-sm-6 text-right">
                                                    <label class="custom-switch">
                                                        <input id="<%# Eval("RoleID") %>" type="checkbox" class="custom-switch-input" value="<%# Eval("RoleID") %>" <%# ViewState[Eval("RoleID").ToString()]!=null?"checked":"" %>>
                                                        <span class="custom-switch-indicator" style="cursor:pointer"></span>
                                                    </label>
                                                </div>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="card-footer text-right">
                   <%-- <button type="submit" class="btn btn-primary">Update</button>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScript" runat="server">
    <script type="text/javascript">
        require(['jquery', 'bootstrap-notify'], function ($, nfy) {
            $(document).ready(function () {
                $('input[type="checkbox"]').on("click", function () {
                    var userid = $("#ContentPlaceHolder1_valueUserId").val();
                    var roleid = $(this).val();
                    var params = { 'userId': userid, 'roleId': roleid };

                    var posturl = "Details.aspx/RevokeRole";

                    //if checked, assign role
                    if ($(this).is(':checked')) {                        
                        posturl = "Details.aspx/AssignRole";
                    }

                    $.ajax(
                        {
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: posturl,
                            data: JSON.stringify(params),
                            datatype: "json",
                            success: function (result) {                                
                                if (result.d) {
                                    $.notify("Changes saved successfully.", "success");
                                }
                                else {
                                    $.notify("Changes failed to saved.", "warn");
                                }

                            },
                            error: function () {

                                $.notify("Unexpected error.", "error");
                            }
                        });

                });
            });
        });
    </script>
</asp:Content>
