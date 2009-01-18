<%--
Slick-Ticket v1.0 - 2008
http://slick-ticket.com
Developed by Stan Naspinski - stan@naspinski.net
http://naspinski.net
--%>
<%@ Page Title="Search" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <fieldset class="inner_color">
        
        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
            
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                    <table class="by2">
                        <tr>
                            <td style="width:50%">
                                <h3><span class="title_header">Title</span>&nbsp;</h3>
                                    <asp:TextBox ID="txtTitle" runat="server" cssclass="half_table" />
                            </td>
                            <td style="width:50%">
                                <h3><span class="title_header">User</span>&nbsp;</h3>
                                <asp:TextBox ID="txtUser" runat="server" CssClass="half_table" />
                                <ajax:AutoCompleteExtender runat="server" ID="aceUser" TargetControlID="txtUser" ServiceMethod="getUsers" ServicePath="~/web_services/services.asmx" MinimumPrefixLength="1" 
                                    EnableCaching="true" CompletionListItemCssClass="autoSuggest" CompletionListHighlightedItemCssClass="autoSuggest autoSuggestSelect" CompletionInterval="1000" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h3><span class="title_header">Group</span>&nbsp;</h3>
                                <asp:DropDownList ID="ddlUnit" runat="server"  CssClass="half_table"  onselectedindexchanged="ddlUnit_SelectedIndexChanged" AutoPostBack="true"  />
                            </td>
                            <td>
                                <h3><span class="title_header">Sub-Group</span>&nbsp;</h3>
                                <asp:DropDownList ID="ddlSubUnit" runat="server" CssClass="half_table" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h3><span class="title_header">From</span>&nbsp;</h3>
                                <asp:TextBox ID="txtFrom" runat="server"  CssClass="half_table"  />
                                <ajax:CalendarExtender ID="calFrom" runat="server" TargetControlID="txtFrom" />
                            </td>
                            <td>
                                <h3><span class="title_header">To</span>&nbsp;</h3>
                                <asp:TextBox ID="txtTo" runat="server" CssClass="half_table" />
                                <ajax:CalendarExtender ID="calTo" runat="server" TargetControlID="txtTo" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h3><span class="title_header">Priority</span>&nbsp;</h3>
                                <asp:DropDownList ID="ddlPriority" runat="server"  CssClass="half_table" DataSourceID="ldsPriority" DataTextField="priority_name" DataValueField="id"   AppendDataBoundItems="true" >
                                    <asp:ListItem Value="0">Any</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <h3><span class="title_header">Status</span>&nbsp;</h3>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="half_table" DataSourceID="ldsStatus" DataTextField="status_name" DataValueField="id" AppendDataBoundItems="true" >
                                    <asp:ListItem Value="0">Any</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
                        <tr>
                            <td>
                                <h3><span class="title_header"><asp:CheckBox ID="chkOpenOnly" Checked="true" runat="server" Text=" Search  only open tickets" /></span></h3>
                            </td>
                            <td style="text-align:center;">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" CssClass="button" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div style="text-align:center;">
                    </div>
                    <br />
                    <asp:GridView ID="gvResults" runat="server" 
                        AutoGenerateColumns="False"  CssClass="list" GridLines="None" AllowSorting="true" OnSorting="gv_Sorting"
                        EmptyDataText="Your group does not have any tickets opened or assigned to them that you are not participating in" >
                        
                        <Columns>
                            <asp:TemplateField HeaderText="Priority" SortExpression="priority">
                                <ItemTemplate>
                                    <div class="color_it" style="background:<%# urgency[Int32.Parse(Eval("priority").ToString())] %>"><%# Eval("priority1.priority_name") %></div>
                                </ItemTemplate>
                                <HeaderStyle Width="60px"></HeaderStyle>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Ticket" SortExpression="title">
                                <ItemTemplate>
                                    <a class="tooltip large" href="ticket.aspx?ticketID=<%# Eval("id") %>">
                                        <em><%# utils.trimForSideBar(Eval("title").ToString(), 50) %></em>
                                        <span class='border_color'><q class='inner_color base_text'>
                                            <div><b>Last Action: </b><%# Eval("last_action") %></div>
                                            <div><b>Ticket Number: </b><%# Eval("id") %></div>
                                            <div><b>Submitter: </b><%# Eval("user.userName")%></div>
                                            <div><b>Assigned To: </b><%# Eval("sub_unit.unit.unit_name") %> - <%# Eval("sub_unit.sub_unit_name") %></div>
                                            <div><b>Originating Group: </b><%# Eval("user.sub_unit1.unit.unit_name") %> - <%# Eval("user.sub_unit1.sub_unit_name") %></div>
                                        </q></span>
                                    </a>
                                </ItemTemplate>
                                <HeaderStyle Width="400px"></HeaderStyle>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="submitted" HeaderText="Submitted" SortExpression="submitted" DataFormatString="{0:MM/dd/yy}" HtmlEncode="false"
                                HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
                            
                            <asp:TemplateField HeaderText="<div style='text-align:center;'>Status</div>" SortExpression="status" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <div class="color_it" style="width:83px;text-align:center;"><%# Eval("statuse.status_name") %></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    
    <asp:LinqDataSource ID="ldsPriority" runat="server" 
        ContextTypeName="dbDataContext" OrderBy="level" 
        Select="new (id, priority_name)" TableName="priorities" />
    <asp:LinqDataSource ID="ldsStatus" runat="server" 
        ContextTypeName="dbDataContext" OrderBy="status_order" 
        Select="new (id, status_name)" TableName="statuses" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sidebar" Runat="Server">
</asp:Content>

