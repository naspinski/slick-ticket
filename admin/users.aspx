<%--
Slick-Ticket v1.0 - 2008
http://slick-ticket.com
Developed by Stan Naspinski - stan@naspinski.net
http://naspinski.net
--%>
<%@ Page Title="Administration - Users" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="users.aspx.cs" Inherits="admin_users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="adminBody" Runat="Server">

    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            
            <h4 class="header">
                <span class="title_header">Users</span>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="smaller" />&nbsp;
                <asp:Button ID="btnSearch" runat="server" CssClass="button smaller" Text="Search" OnClick="btnSearch_Click" />
                <span class="clear"></span>
                <asp:Label ID="lblReport" runat="server" />
            </h4>
            <div style="text-align:left;" class="header">
                Here you can view and remove users from the system as well as set their administration privileges; hover over UserName for more information on a user.
            </div>
            <div class="divider"></div>
            <fieldset class="inner_color">
                <div style="text-align:center;">
                    <asp:Label ID="lblSearchBar" runat="server" />
                </div>
                <br />
                <asp:GridView ID="gvUsers" runat="server" CssClass="list" AllowSorting="True" GridLines="None"
                    AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="ldsUsers" 
                    EmptyDataText="No Users match your search" AllowPaging="True" 
                    onrowdeleted="gvUsers_RowDeleted"  >
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to delete this User?');" 
                                    CommandName="Delete" Text="Delete"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UserName" SortExpression="userName">
                            <ItemTemplate>
                                <a href="javascript:void();" class="tooltip limited">
                                    <%# Eval("userName") %>
                                    <span class='border_color'><q class='inner_color base_text'>
                                       <div class="bold"><%# Eval("userName") %></div>
                                       <%# Eval("sub_unit1.unit.unit_name") %><br />
                                       <%# Eval("sub_unit1.sub_unit_name") %>
                                    </q></span>
                                </a>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="phone" HeaderText="Phone" SortExpression="phone" />
                        <asp:TemplateField HeaderText="Email" SortExpression="email">
                            <ItemTemplate>
                                <a href="mailto:<%# Eval("email") %>"><%# Eval("email") %></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Admin" SortExpression="is_admin">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("is_admin") %>' Enabled='<%# !Eval("userName").ToString().Equals(userName) %>'
                                    AutoPostBack="True" oncheckedchanged="CheckBox1_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="ldsUsers" runat="server" 
                    ContextTypeName="dbDataContext" EnableDelete="True" OrderBy="userName" TableName="users" 
                    Where="userName.StartsWith(@startsWith) && userName.Contains(@contains)">
                    <WhereParameters>
                        <asp:SessionParameter  Name="startsWith" SessionField="startswith"  Type="String" DefaultValue="" ConvertEmptyStringToNull="false" />
                        <asp:SessionParameter  Name="contains" SessionField="contains"  Type="String" DefaultValue="" ConvertEmptyStringToNull="false" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </fieldset>
        
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="adminSidebar" Runat="Server">
</asp:Content>

