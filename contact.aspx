<%--
Slick-Ticket v1.0 - 2008
http://slick-ticket.com
Developed by Stan Naspinski - stan@naspinski.net
http://naspinski.net
--%>
<%@ Page Title="Contact Administrator" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="contact.aspx.cs" Inherits="contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <fieldset class="inner_color">
        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlInput" runat="server">
                    <asp:Label ID="lblInReport" runat="server" />
                    <h3>
                        <span class="title_header">Question/Comment</span>
                        <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ErrorMessage="Required" ControlToValidate="txtSubject" />
                    </h3>
                    <h2><asp:TextBox ID="txtSubject" runat="server" CssClass="full_window" /></h2>
                    <h3>
                        <span class="title_header">Details</span>
                        <asp:RequiredFieldValidator ID="rfvBody" runat="server" ErrorMessage="Required" ControlToValidate="txtBody" />
                    </h3>
                    <asp:TextBox ID="txtBody" runat="server" CssClass="full_window" TextMode="MultiLine" Rows="5" />
                    <br />
                    <div style="text-align:center;">
                        <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="button" 
                            onclick="btnSend_Click" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlOutput" runat="server" Visible="false">
                    <h2><asp:Label ID="lblReport" runat="server" /></h2>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sidebar" Runat="Server">
</asp:Content>

