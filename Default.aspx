<%--
Slick-Ticket v1.0 - 2008
http://slick-ticket.com
Developed by Stan Naspinski - stan@naspinski.net
http://naspinski.net
--%>
<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <fieldset class="inner_color">
        <h2>Navigation</h2>
        <table class="by2">
            <tr>
                <td>
                    <a class="big_button" href="new_ticket.aspx">
                        <img style="float:left;" src="images/icons/add_page.png" alt="" />
                        <span>New Ticket</span>
                        <q class="base_text">Enter a new ticket into the system</q>
                    </a>
                </td>
                <td>
                    <a class="big_button" href="ticket.aspx">
                        <img style="float:left;" src="images/icons/search_page.png" alt="" />
                        <span>View Ticket</span>
                        <q class="base_text">View or Contribute to a ticket</q>
                    </a>
                </td>
            </tr>
            <tr>
                <td>
                    <a class="big_button" href="my_issues.aspx">
                        <img style="float:left;" src="images/icons/folder_full.png" alt="" />
                        <span>My Issues</span>
                        <q class="base_text">You and your group's open tickets</q>
                    </a>
                </td>
                <td>
                    <a class="big_button" href="profile.aspx">
                        <img style="float:left;" src="images/icons/user.png" alt="" />
                        <span>Profile</span>
                        <q class="base_text">Change your profile settings</q>
                    </a>
                </td>
            </tr>
            <tr>
                <td>
                    <a class="big_button" href="search.aspx">
                        <img style="float:left;" src="images/icons/search.png" alt="" />
                        <span>Search</span>
                        <q class="base_text">Search tickets</q>
                    </a>
                </td>
                <td>
                    <a class="big_button" href="help.aspx">
                        <img style="float:left;" src="images/icons/help.png" alt="" />
                        <span>Help/FAQ</span>
                        <q class="base_text">Have some questions? Get them answered</q>
                    </a>
                </td>
            </tr>
            <tr>
                <td>
                    <a class="big_button" href="contact.aspx">
                        <img style="float:left;" src="images/icons/comment.png" alt="" />
                        <span>Contact Administrator</span>
                        <q class="base_text">Question, problem or comment?</q>
                    </a>
                </td>
                <td></td>
            </tr>
        </table>
    </fieldset>
    <div class="divider"></div>
    <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
        <fieldset class="inner_color">
            <h2>Administration</h2>
            <ctrl:AdminPanel ID="admPanel" runat="server" />
        </fieldset>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sidebar" Runat="Server">
</asp:Content>

