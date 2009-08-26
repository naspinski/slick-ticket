<%--
Slick-Ticket v1.0 - 2008
http://slick-ticket.com
Developed by Stan Naspinski - stan@naspinski.net
http://naspinski.net
--%>
<%@ Page Title="My Issues" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="my_issues.aspx.cs" Inherits="my_issues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <asp:TextBox ID="txtSubmitter" runat="server" Visible="false" />
    <asp:TextBox ID="txtGroup" runat="server" Visible="false" />
    
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
        
            <h2 class="header">
                <span class="title_header">
                    My Tickets
                    <a href='javascript:void();' class='tooltip limited'><img src='images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>All open tickets that I have participated in.</q></span></a>
                </span>
                <span class="clear"></span>
            <span class="smaller"><asp:Label ID="lblreportMy" runat="server" /></span>
            </h2>
            <fieldset class="inner_color">
                <asp:GridView ID="gvMy" runat="server"
                    AutoGenerateColumns="False" PageSize="5" CssClass="list" GridLines="None" AllowSorting="True"  OnSorting="gv_Sorting" 
                    EmptyDataText="You are not participating in any open tickets" >
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
                                        <div><b>Last Action: </b><%# Convert.ToDateTime(Eval("last_action")).ToString() %></div>
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
            </fieldset>
            <div class="divider"></div>
            <h2 class="header">
                <span class="title_header"><asp:Label ID="lblMyGroup" runat="server" /> Tickets
                    <a href='javascript:void();' class='tooltip limited'><img src='images/info.png' alt='explanation' /><span class='border_color'><q class='inner_color base_text'>All open tickets either started by, or assigned to my group that I have not yet participated in.</q></span></a>
                </span>
                <span class="clear"></span>
            <span class="smaller"><asp:Label ID="lblReportGroup" runat="server" /></span>
            </h2>
            <fieldset class="inner_color">
                <asp:GridView ID="gvGroup" runat="server" 
                    AutoGenerateColumns="False"  CssClass="list" GridLines="None" AllowSorting="True"  OnSorting="gv_Sorting" 
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
                                        <div><b>Last Action: </b><%# Convert.ToDateTime(Eval("last_action")).ToString() %></div>
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
            </fieldset>
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sidebar" Runat="Server">
</asp:Content>

