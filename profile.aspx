<%--
Slick-Ticket v1.0 - 2008
http://slick-ticket.com
Developed by Stan Naspinski - stan@naspinski.net
http://naspinski.net
--%>
<%@ Page Title="Profile" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">

    <asp:UpdatePanel ID="up" runat="server">
    <ContentTemplate>
  
        <asp:Panel ID="pnlInput" runat="server" DefaultButton="btnSubmit">
        
            <fieldset class="inner_color"> 
                <span class="larger bold"><asp:Label ID="lblProfileHeader" runat="server" /></span>
                <table class="by2">
                    <tr>
                        <td style="width:50%;">
                            <h3>
                                <span class="title_header">User Name</span>&nbsp;
                                <span class="clear"></span>
                            </h3>
                            <asp:TextBox ID="txtUserName" runat="server" Width="100%" Enabled="false" />
                        </td>
                        <td style="width:50%;">
                            <h3>
                                <span class="title_header">Phone</span>&nbsp;
                                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" display="Dynamic" ErrorMessage="Phone Number Required"
                                    ControlToValidate="txtPhone" ForeColor="" CssClass="error" ValidationGroup="profile" />
                                <asp:RegularExpressionValidator ID="regPhone" runat="server" ErrorMessage="Invalid Phone Number&nbsp;&nbsp;&nbsp;&nbsp;" 
                                    ControlToValidate="txtPhone" ForeColor=""  ValidationGroup="profile"
                                    CssClass="error" display="Dynamic" ValidationExpression="^([\(]{1}[0-9]{3}[\)]{1}[\.| |\-]{0,1}|^[0-9]{3}[\.|\-| ]?)?[0-9]{3}(\.|\-| )?[0-9]{4}$" />
                            </h3>
                            <asp:TextBox runat="server" ID="txtPhone" CssClass="half_table" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h3 style="text-align:right;">
                                <span class="title_header">Email Prefix</span>&nbsp;
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="<span style='padding-right:32px;'>Email Required</span>" display="Dynamic" 
                                    ControlToValidate="txtEmail" ForeColor="" CssClass="error" style="margin-right:0;"  ValidationGroup="profile" />
                            </h3>
                            <div>
                                <div style="float:right;width:8%; font-weight:bold; font-size:1.2em; text-align:center;">@</div>
                                <asp:TextBox runat="server" ID="txtEmail" style="width:90%;text-align:right;" />
                            </div>
                            
                        </td>
                        <td>
                            <h3 style="text-align:right;">
                                <span class="title_header">Domain</span>&nbsp;
                                <asp:RequiredFieldValidator ID="rfvDomain" runat="server" ErrorMessage="Domain Required" Display="Dynamic"
                                    ControlToValidate="txtDomain" ForeColor="" CssClass="error" ValidationGroup="profile" />
                                <asp:RegularExpressionValidator ID="regDomain" runat="server" ErrorMessage="Invalid Domain" Display="Dynamic" 
                                    ForeColor="" CssClass="error"  ValidationGroup="profile"
                                    ControlToValidate="txtDomain" ValidationExpression="((\[([0-9]{1,3}\.){3}[0-9]{1,3}\])|(([\w\-]+\.)+)([a-zA-Z]{2,4}))$" />
                            </h3>
                            <asp:TextBox runat="server" ID="txtDomain" CssClass="half_table" />
                            <asp:DropDownList ID="ddlDomain" runat="server" DataSourceID="ldsDomains" 
                                DataTextField="domain" DataValueField="domain"  Visible="False"  
                                CssClass="half_table" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h3>
                                <span class="title_header">Group</span>
                                <asp:RequiredFieldValidator runat="server" ID="rfvUnit" ControlToValidate="ddlUnit" InitialValue="0" ErrorMessage="Must Select Group"  
                                    ForeColor="" CssClass="error"  ValidationGroup="profile"/>
                            </h3>
                            <asp:DropDownList ID="ddlUnit" runat="server"  CssClass="half_table"  onselectedindexchanged="ddlUnit_SelectedIndexChanged" AutoPostBack="true"  />
                        </td>
                        <td>
                            <h3>
                                <span class="title_header">Sub-Group</span>
                                <asp:RequiredFieldValidator runat="server" ID="rfvSubUnit" ControlToValidate="ddlSubUnit" InitialValue="0" ErrorMessage="Must Select Sub-Group"  
                                    ForeColor="" CssClass="error" ValidationGroup="profile" />
                            </h3>
                            <asp:DropDownList ID="ddlSubUnit" runat="server" CssClass="half_table" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <asp:LinkButton ID="lbGroups" runat="server" Text="What AD groups do I belong to?" CausesValidation="false" />
                        </td>
                        <td>
                            <br />
                            <div style="text-align:center;">
                                <asp:Button runat="server" ID="btnSubmit" Text="Save Changes" CssClass="button" onclick="btnSubmit_Click" ValidationGroup="profile" />
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:LinqDataSource ID="ldsDomains" runat="server" 
                    ContextTypeName="dbDataContext" OrderBy="domain" 
                    TableName="allowed_email_domains">
                </asp:LinqDataSource>
            </fieldset>
            
            <ajax:ModalPopupExtender ID="mpeGroups" runat="server" TargetControlID="lbGroups" BackgroundCssClass="modal_background"
                PopupControlID="pnlGroups" CancelControlID="btnCancel" />

            <asp:Panel ID="pnlGroups" runat="server"  CssClass="modal_popup" style="display:none;" DefaultButton="btnCancel" >
                
                <div class="large_container border_color">
                    <fieldset class="inner_color">
                        <div>
                            <h3>
                                <span class="title_header">Active Directory Groups</span>
                            </h3>
                        </div>
                        <div style="clear:both; ">
                        <asp:Label ID="lblGroups" runat="server" />
                        </div>
                        
                        <div style="text-align:center;" class="clear">
                            <br />
                            <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="button" CausesValidation="false" />
                        </div>
                    </fieldset>
                </div>
                
            </asp:Panel>
        </asp:Panel>  
        
        
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>

