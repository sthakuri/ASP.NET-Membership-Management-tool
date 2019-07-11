<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMain.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AccessControlTool.Applications.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="page-header">
        <h1 class="page-title">Applications
        </h1>
        <div class="page-subtitle">
            Count:
            <asp:Label ID="lblCount" runat="server">0</asp:Label>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScript" runat="server">
    
</asp:Content>