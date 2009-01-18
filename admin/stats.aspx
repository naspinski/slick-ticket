<%--
Slick-Ticket v1.0 - 2008
http://slick-ticket.com
Developed by Stan Naspinski - stan@naspinski.net
http://naspinski.net
--%>
<%@ Page Title="Administration - Stats" Language="C#" EnableEventValidation="false" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="stats.aspx.cs" Inherits="admin_stats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="adminHead" Runat="Server">
    <link rel="Stylesheet" type="text/css" href="../css/stats.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="adminBody" Runat="Server">

    <h4 class="header">
        <span class="title_header">Statistics</span>
        <span class="clear"></span>
        <span class="smaller"><asp:Label ID="lblReport" runat="server" /></span>
    </h4>
    <div style="text-align:left;" class="header">
        Quick overview of ticket status.
    </div>
    <br />
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <fieldset class="inner_color">
                <div style="width:95%;">
                    <asp:Panel ID="pnlGroups" runat="server">
                        <asp:Repeater ID="rptGroups" runat="server">
                            <HeaderTemplate>
                                <h2>
                                    <%= setTopLevel(utils.settings.get("title")) %>
                                </h2>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <span class='base_text number'>
                                    <%# Eval("count") %>
                                </span>
                                <asp:Button ID="btnGrp" runat="server" Text='<%# Eval("name") %>' CommandArgument='<%# Eval("id") %>'
                                    OnClick="btnGrp_Click" CssClass="button bar" style='<%# getWidth(Eval("count")) %>'
                                 /><br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                    <asp:Panel ID="pnlSubGroups" runat="server">
                        <asp:Repeater ID="rptSubGroups" runat="server">
                            <HeaderTemplate>
                                <h2>
                                    <asp:LinkButton runat="server" ID="btnHome" OnClick="btnHome_Click" ><%= setTopLevel(utils.settings.get("title")) %></asp:LinkButton><br />
                                    <asp:Label CssClass="smaller" ID="lblThisUnit" runat="server" ><%# getThisUnit() %></asp:Label>
                                </h2>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <span class='base_text number'>
                                    <%# Eval("count") %>
                                </span>
                                <asp:Button ID="btnGrp" runat="server" Text='<%# Eval("name") %>' CommandArgument='<%# Eval("id") %>'
                                    OnClick="btnSubGrp_Click" CssClass="button bar" style='<%# getWidth(Eval("count")) %>'
                                 /><br />
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Label ID="lblSubGroupDetails" runat="server">
                            <h2>
                                <asp:LinkButton runat="server" ID="btnHome" OnClick="btnHome_Click" ><%= setTopLevel(utils.settings.get("title")) %></asp:LinkButton><br />
                                <span class="smaller">
                                    <asp:LinkButton ID="lbGroup" runat="server" OnClick="btnSubHome_Click" CommandArgument='<%# getThisUnit() %>'>
                                        <%= thisUnit %>
                                    </asp:LinkButton>
                                    <span class="smaller" style="display:block;">
                                        <%= thisSubUnit %> 
                                    </span>
                                </span>  
                            </h2>
                        </asp:Label>
                    </asp:Panel>
                </div>
                <br />
                <asp:Repeater ID="rptDetails" runat="server">
                    <AlternatingItemTemplate>
                        <h3>
                            <span class='title_header'>
                                <%# Eval("header") %>
                            </span>
                            <%# Eval("data") %>
                        </h3>
                    </AlternatingItemTemplate>
                    <ItemTemplate>
                        <h3 class="alt_row">
                            <span class='title_header'>
                                <%# Eval("header") %>
                            </span>
                            <%# Eval("data") %>
                        </h3>
                    </ItemTemplate>
                </asp:Repeater>
            </fieldset>
            <asp:Label ID="currentGroup" runat="server" Visible="false" />
            <asp:Label ID="currentSubGroup" runat="server" Visible="false" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="adminSidebar" Runat="Server">
</asp:Content>

