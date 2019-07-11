<%@ Page Title="Users" Language="C#" MasterPageFile="~/SiteMain.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AccessControlTool.Users.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1 class="page-title">Users
        </h1>
        <div class="page-subtitle">
            Count:
            <asp:Label ID="lblCount" runat="server"></asp:Label>
        </div>
        <div class="page-options">
             <a href="/Users/NewUser.aspx" class="btn btn-sm btn-outline-primary" target="_blank"><i class="fe fe-user"></i>New User</a>
        </div>
    </div>
    <% if (ViewState["Error"] != null)
        { %>
    <div class="alert alert-danger">
        <%= ViewState["Error"] %>
    </div>
    <% } %>
    <div class="row row-cards row-deck">
        <div class="col-12">
            <input type="text" id="myInput" class="form-control" placeholder="Enter User Name or Application Name...">
            <div class="card">

                <div class="table-responsive">
                    <asp:Repeater ID="rptUsers" runat="server">
                        <HeaderTemplate>

                            <table class="table card-table table-vcenter text-nowrap" id="mytable">
                                <thead>
                                    <tr>
                                        <th class="w-1">SN</th>
                                        <th>User Name</th>
                                        <th>Application Name</th>
                                        <th>Created On</th>
                                        <th>Is Locked</th>

                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>

                        <ItemTemplate>
                            <tr>
                                <td><span class="text-muted">
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" /></span></td>
                                <td><a href="Details.aspx?UserId=<%# Eval("UserID")%>" class="text-inherit"><%# Eval("UserName")%></a></td>
                                <td><a href="/Applications/" class="text-inherit"><%# Eval("ApplicationName")%></a></td>
                                <td><%# Eval("CreatedDate", "{0:MM/dd/yyyy}")%></a></td>
                                <td>

                                    <label class="custom-switch">
                                        <input type="checkbox" name="custom-switch-checkbox" class="custom-switch-input" disabled="disabled" <%# (Eval("IsLockedOut").ToString()=="True")?"checked":"" %>>
                                        <span class="custom-switch-indicator " style="cursor: no-drop"></span>
                                    </label>
                                </td>

                            </tr>
                        </ItemTemplate>

                        <FooterTemplate>
                            </tbody>
                    </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScript" runat="server">
    <script type="text/javascript">
        require(['jquery'], function ($) {
            $(document).ready(function () {
                $("#myInput").on("keyup", function () {
                    var value = $(this).val().toLowerCase();
                    $("#mytable tbody tr").filter(function () {
                        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                    });
                });
            });
        });
    </script>
</asp:Content>
