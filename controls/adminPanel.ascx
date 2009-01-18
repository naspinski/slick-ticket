<%--
Slick-Ticket v1.0 - 2008
http://slick-ticket.com
Developed by Stan Naspinski - stan@naspinski.net
http://naspinski.net
--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="adminPanel.ascx.cs" Inherits="controls_adminPanel" %>


    <table class="by2">
        <tr>
            <td>
                <a class="big_button" ID="hlPermissions" runat="server" href="~/admin/permissions.aspx">
                    <img runat="server" style="float:left;" src="~/images/icons/lock.png" alt="" />
                    <span>Permissions</span>
                    <q class="base_text">Grant your users access based on AD groups</q>
                </a>
            </td>
            <td>
                <a class="big_button" ID="hlGroups" runat="server" href="~/admin/groups.aspx">
                    <img runat="server" style="float:left;" src="../images/icons/promotion.png" alt="" />
                    <span>Groups</span>
                    <q class="base_text">Set groups and sub-groups</q>
                </a>
            </td>
        </tr>
        <tr>
            <td>
                <a class="big_button" ID="hlUsers" runat="server" href="~/admin/users.aspx">
                    <img runat="server" style="float:left;" src="../images/icons/users.png" alt="" />
                    <span>Users</span>
                    <q class="base_text">View users and set admin privileges</q>
                </a>
            </td>
            <td>
                <a class="big_button" ID="hlSettings" runat="server" href="~/admin/settings.aspx">
                    <img runat="server" style="float:left;" src="../images/icons/process.png" alt="" />
                    <span>Settings</span>
                    <q class="base_text">Miscellaneous system settings</q>
                </a>
            </td>
        </tr>
        <tr>
            <td>
                <a class="big_button" ID="hlInfo" runat="server" href="~/admin/info.aspx">
                    <img runat="server" style="float:left;" src="../images/icons/info.png" alt="" />
                    <span>Information</span>
                    <q class="base_text">Admin information/instructions</q>
                </a>
            </td>
            <td>
                <a class="big_button" ID="hlStats" runat="server" href="~/admin/stats.aspx">
                    <img runat="server" style="float:left;" src="../images/icons/chart.png" alt="" />
                    <span>Statistics</span>
                    <q class="base_text">Quick overview of ticket status</q>
                </a>
            </td>
        </tr>
    </table>